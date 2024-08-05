Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo

Namespace GenesisSaldos.Integracion

    Public Class CambiaEstadoDocumentoFondosSaldos

        ''' <summary>
        ''' Classe CambiaEstadoDocumento
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [cbomtempo]  16/01/2014  criado
        ''' </history>
        Public Function Ejecutar(Peticion As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion) As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta

            Dim objRespuesta As New Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Respuesta()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

            Try

                'valida a petição
                ValidarDatos(Peticion)


                Dim documento As Clases.Documento = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(Peticion.DatosDocumento.oid_Documento_Saldos, Peticion.Usuario.Login, Nothing)

                Dim estadoNovo As Enumeradores.EstadoDocumento = MapearEstado(Peticion.DatosDocumento.Estado)

                'verifica se a mudança de estado está correta
                If documento.EstadosPosibles.Contains(estadoNovo) Then

                    'verifica se está aceitando um documento recusado
                    If estadoNovo = Enumeradores.EstadoDocumento.Aceptado AndAlso documento.Estado = Enumeradores.EstadoDocumento.Rechazado Then
                        objRespuesta.MensajeError = Traduzir("06_msg_rechazado")
                        'verifica se está cancelando um documento já aceitado.
                    ElseIf estadoNovo = Enumeradores.EstadoDocumento.Rechazado AndAlso documento.Estado = Enumeradores.EstadoDocumento.Aceptado Then
                        objRespuesta.MensajeError = Traduzir("06_msg_aceptado")
                    ElseIf estadoNovo <> documento.Estado Then
                        documento.Estado = estadoNovo

                        'recupera o parâmetro CrearConfiguiracionNivelSaldo do IAC
                        Dim parametros As IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroRespuestaColeccion
                        parametros = Util.ParametrosDelegacionPais(Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS,
                                                                   documento.SectorOrigen.Delegacion.Codigo,
                                                                   New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.ParametroColeccion() From {New IAC.Integracion.ContractoServicio.GetParametrosDelegacionPais.Parametro() With {.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO}})
                        Dim crearConfiguiracionNivelSaldo As Boolean = (parametros(0).ValorParametro <> "0")

                        'grava as alterações do documento
                        documento.UsuarioModificacion = Peticion.Usuario.Login
                        MaestroDocumentos.GuardarDocumento(documento, True, False, False, Nothing, Nothing)

                        'recupera o documento atualizado
                        documento = LogicaNegocio.GenesisSaldos.Documento.recuperarDocumentoPorIdentificador(documento.Identificador, Peticion.Usuario.Login, Nothing)
                    End If

                    'marca o documento como enviado para o salidas.
                    Dim peticionIntegracion As New Clases.Integracion()
                    peticionIntegracion.CodigoEstado = Enumeradores.EstadoIntegracion.Enviado
                    peticionIntegracion.CodigoModuloDestino = Enumeradores.Aplicacion.GenesisSalidas
                    peticionIntegracion.CodigoModuloOrigen = Enumeradores.Aplicacion.GenesisNuevoSaldos
                    peticionIntegracion.CodigoProceso = Enumeradores.CodigoProcesoIntegracion.EnviarMifAceptadoRechazado
                    peticionIntegracion.CodigoTablaIntegracionEnum = Enumeradores.TablaIntegracion.SAPR_TDOCUMENTO
                    peticionIntegracion.IdentificadorTablaIntegracion = documento.Identificador
                    peticionIntegracion.UsuarioCreacion = Peticion.Usuario.Login
                    peticionIntegracion.UsuarioModificacion = Peticion.Usuario.Login
                    Genesis.Integracion.GrabarIntegracion(peticionIntegracion)

                End If

                objRespuesta.codEstado = MapearEstado(Peticion.DatosDocumento.Estado, documento.Estado)
                objRespuesta.cod_comprobante = documento.CodigoComprobante

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                objRespuesta.MensajeError = ex.Descricao
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()
            End Try

            Return objRespuesta

        End Function

        ''' <summary>
        ''' Validação de dados
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <remarks></remarks>
        Private Sub ValidarDatos(Peticion As Prosegur.Global.Saldos.ContractoServicio.CambiaEstadoDocumentoFondosSaldos.Peticion)

            If String.IsNullOrEmpty(Peticion.DatosDocumento.oid_Documento_Saldos) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "Peticion.DatosDocumento.oid_Documento_Saldos"))
            End If

            If String.IsNullOrEmpty(Peticion.DatosDocumento.Estado) Then
                Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo"), "Peticion.DatosDocumento.Estado"))
            Else
                If Not {"A", "R"}.Contains(Peticion.DatosDocumento.Estado.ToUpper()) Then
                    Try
                        RecuperarEnum(Of Enumeradores.EstadoDocumento)(Peticion.DatosDocumento.Estado)
                    Catch ex As NotImplementedException
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("gen_srv_msg_atributo_invalido"), "Peticion.DatosDocumento.Estado"))
                    End Try
                End If
            End If

        End Sub

        ''' <summary>
        ''' Mapeamento do estado do documento do novo saldos para o do saldos antigo quando a chamada for feita pelo salidas.
        ''' </summary>
        ''' <param name="estadoPeticion"></param>
        ''' <param name="estadoDocumento"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MapearEstado(estadoPeticion As String, estadoDocumento As Enumeradores.EstadoDocumento) As String
            Dim retorno As String
            If {"A", "R"}.Contains(estadoPeticion.ToUpper()) Then
                Select Case estadoDocumento
                    Case Enumeradores.EstadoDocumento.Aceptado
                        retorno = "A"
                    Case Enumeradores.EstadoDocumento.Rechazado
                        retorno = "R"
                    Case Enumeradores.EstadoDocumento.Confirmado
                        retorno = "I"
                    Case Enumeradores.EstadoDocumento.EnCurso
                        retorno = "P"
                    Case Else
                        retorno = estadoDocumento.RecuperarValor()
                End Select
            Else
                retorno = estadoDocumento.RecuperarValor()
            End If
            Return retorno
        End Function

        ''' <summary>
        ''' Mapeamento do estado do documento do saldos para o novo saldos quando a chamada do serviço for feita pelo salidas.
        ''' </summary>
        ''' <param name="estadoPeticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function MapearEstado(estadoPeticion As String) As Enumeradores.EstadoDocumento
            Dim retorno As Enumeradores.EstadoDocumento
            Select Case estadoPeticion.ToUpper()
                Case "A"
                    retorno = Enumeradores.EstadoDocumento.Aceptado
                Case "R"
                    retorno = Enumeradores.EstadoDocumento.Rechazado
                Case Else
                    retorno = RecuperarEnum(Of Enumeradores.EstadoDocumento)(estadoPeticion)
            End Select
            Return retorno
        End Function

    End Class

End Namespace