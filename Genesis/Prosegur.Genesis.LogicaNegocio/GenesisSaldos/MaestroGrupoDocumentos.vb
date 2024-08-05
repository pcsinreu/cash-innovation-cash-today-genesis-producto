Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Transactions
Imports Prosegur.Genesis.Comon.Enumeradores
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports System.Text

Namespace GenesisSaldos


    Public Class MaestroGrupoDocumentos

#Region "CrearGrupoDocumentos"

        ''' <summary>
        ''' Crear grupo de documentos
        ''' </summary>
        ''' <param name="formulario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CrearGrupoDocumentos(formulario As Clases.Formulario) As Clases.GrupoDocumentos
            Dim GrupoDocumentos As New Clases.GrupoDocumentos
            Try

                ' ao criar um GrupoDocumento, o estado inicial deverá ser "Nuevo"
                GrupoDocumentos.Estado = Enumeradores.EstadoDocumento.Nuevo

                ' define o formulário que está dando origem ao GrupoDocumento
                GrupoDocumentos.Formulario = formulario

                ' preenche de anti mao a lista de Terminos Grupo.
                GrupoDocumentos.GrupoTerminosIAC = formulario.GrupoTerminosIACGrupo

                GrupoDocumentos.Documentos = New ObservableCollection(Of Clases.Documento)

                ' gera o identificador do novo GrupoDocumento
                GrupoDocumentos.Identificador = System.Guid.NewGuid.ToString()

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return GrupoDocumentos
        End Function

#End Region

#Region "GuardarGrupoDocumentos"

        ''' <summary>
        ''' Guardar las informaciones en la BBDD
        ''' </summary>
        ''' <param name="grupodocumento"></param>
        ''' <remarks></remarks>
        Public Shared Sub GuardarGrupoDocumentos(ByRef GrupoDocumento As Clases.GrupoDocumentos,
                                                 hacer_commit As Boolean,
                                                 confirmar_doc As Boolean,
                                                 bol_gestion_bulto As Boolean,
                                                 caracteristica_integracion As Enumeradores.CaracteristicaFormulario?,
                                                 ByRef TransaccionActual As DataBaseHelper.Transaccion)

            Try

                If GrupoDocumento.Estado <> Enumeradores.EstadoDocumento.Nuevo AndAlso GrupoDocumento.Estado <> Enumeradores.EstadoDocumento.EnCurso Then
                    AccesoDatos.GenesisSaldos.GrupoDocumentos.TransacionesGrupoDocumentos(GrupoDocumento, hacer_commit, TransaccionActual)

                    ' TO DO 1: Mejorar performance
                    If GrupoDocumento.Estado = Enumeradores.EstadoDocumento.Aceptado AndAlso _
                        (GrupoDocumento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) OrElse _
                         GrupoDocumento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) OrElse _
                         GrupoDocumento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeContenedores)) Then
                        GrupoDocumento = recuperarGrupoDocumentos(GrupoDocumento.Identificador, GrupoDocumento.UsuarioModificacion, TransaccionActual)

                    End If

                Else

                    ' TO DO 2: Mejorar performance
                    For Each Documento In GrupoDocumento.Documentos
                        If (Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeRemesas) OrElse _
                            Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos)) Then
                            GenesisSaldos.Documento.calcularValorDelDocumentoPorElElemento(Documento, TransaccionActual)
                        End If
                    Next

                    AccesoDatos.GenesisSaldos.GrupoDocumentos.GrabarGrupoDocumento(GrupoDocumento, bol_gestion_bulto, hacer_commit, confirmar_doc, caracteristica_integracion, TransaccionActual)

                End If

                '' Integracion con Conteo y Salidas
                'MaestroDocumentos.EjecutarIntegraciones(GrupoDocumento.Documentos)

            Catch ex As Excepcion.NegocioExcepcion
                Throw
            Catch ex As Exception
                Throw
            End Try

        End Sub

#End Region

#Region "ObtenerGrupoDocumentos"

        Public Shared Function recuperarGrupoDocumentos(identificador As String,
                                                        usuario As String,
                                                        ByRef transaccion As DataBaseHelper.Transaccion) As Clases.GrupoDocumentos
            Return AccesoDatos.GenesisSaldos.GrupoDocumentos.recuperarGrupoDocumentos(identificador, usuario, transaccion)
        End Function

        ''' <summary>
        ''' Método que obtém o GrupoDocumentos pelo seu oid.
        ''' </summary>
        ''' <param name="oid">OID utilizado na consulta.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerGrupoDocumentosPorIdentificador(oid As String) As Clases.GrupoDocumentos
            Dim grupoDocumentos As Clases.GrupoDocumentos

            grupoDocumentos = AccesoDatos.GenesisSaldos.GrupoDocumentos.ObtenerPorOid(oid)

            If (grupoDocumentos IsNot Nothing) Then
                If (grupoDocumentos.Formulario IsNot Nothing) Then
                    grupoDocumentos.Formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormulario(grupoDocumentos.Formulario.Identificador)

                    If grupoDocumentos.Formulario IsNot Nothing Then
                        grupoDocumentos.GrupoTerminosIAC = If(grupoDocumentos.Formulario.GrupoTerminosIACGrupo IsNot Nothing, AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIAC(grupoDocumentos.Formulario.GrupoTerminosIACGrupo.Identificador), Nothing)

                        If grupoDocumentos.Formulario.GrupoTerminosIACGrupo IsNot Nothing AndAlso grupoDocumentos.Formulario.GrupoTerminosIACGrupo.TerminosIAC IsNot Nothing AndAlso grupoDocumentos.Formulario.GrupoTerminosIACGrupo.TerminosIAC.Count > 0 Then

                            'Recupera os valores de termino para o grupo
                            Dim objValoresTermino As List(Of Clases.TerminoIAC) = AccesoDatos.Genesis.ValorTerminoGrupoDocumento.ValorTerminoGrupoDocumentoRecuperar(grupoDocumentos.Identificador)

                            If objValoresTermino IsNot Nothing AndAlso objValoresTermino.Count > 0 Then

                                For Each Termino In grupoDocumentos.GrupoTerminosIAC.TerminosIAC
                                    Termino.Valor = (From VT In objValoresTermino Where VT.Identificador = Termino.Identificador Select VT.Valor).FirstOrDefault
                                Next

                            End If
                        End If
                    End If
                End If

                If (grupoDocumentos.CuentaOrigen IsNot Nothing) Then
                    grupoDocumentos.CuentaOrigen.Sector = AccesoDatos.Genesis.Sector.ObtenerPorOid(grupoDocumentos.CuentaOrigen.Sector.Identificador)
                End If

                If (grupoDocumentos.CuentaDestino IsNot Nothing) Then
                    grupoDocumentos.CuentaDestino.Sector = AccesoDatos.Genesis.Sector.ObtenerPorOid(grupoDocumentos.CuentaDestino.Sector.Identificador)
                End If

                grupoDocumentos.Documentos = AccesoDatos.GenesisSaldos.Documento.ObtenerDocumentosPorIdentificadorGrupo(grupoDocumentos.Identificador)

            End If

            Return grupoDocumentos
        End Function

        ''' <summary>
        ''' Recupera as caracteriticas do formulario do grupo de documento
        ''' </summary>
        ''' <param name="Peticion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarCaracteristicasPorCodigoComprobante(Peticion As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Peticion) As Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta
            Dim Respuesta As New Contractos.GenesisSaldos.GrupoDocumento.RecuperarCaracteristicasPorCodigoComprobante.Respuesta()

            Try
                Respuesta.GrupoDocumento = AccesoDatos.GenesisSaldos.GrupoDocumentos.RecuperarCaracteristicasPorCodigoComprobante(Peticion.CodigoComprobante)

            Catch ex As Excepcion.NegocioExcepcion
                Respuesta.Mensajes.Add(ex.Descricao)
            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                Respuesta.Excepciones.Add(ex.Message)
            End Try

            Return Respuesta

        End Function

        ''' <summary>
        ''' Recupera o identificador do grupo documento pelo codigo comprobante.
        ''' </summary>
        ''' <param name="CodigoComprobante"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarIdentificadorPorCodigoComprobante(CodigoComprobante As String) As String
            Return AccesoDatos.GenesisSaldos.GrupoDocumentos.RecuperarIdentificadorPorCodigoComprobante(CodigoComprobante)
        End Function

#End Region

#Region "ActualizaBolImpreso"
        Public Shared Function ActualizaBolImpreso(identificadorGrupoDocumento As String, codigoComprobante As String,
                                                   impreso As Boolean) As Integer

            Try
                If String.IsNullOrEmpty(identificadorGrupoDocumento) AndAlso String.IsNullOrEmpty(codigoComprobante) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("036_Identificador_CodCompro_Grupo_Documento"))
                End If

                AccesoDatos.GenesisSaldos.GrupoDocumentos.ActualizarBolImpreso(identificadorGrupoDocumento, codigoComprobante, impreso)

                Return AccesoDatos.GenesisSaldos.GrupoDocumentos.RecuperarRowVerGrupoDocumento(identificadorGrupoDocumento, codigoComprobante)

            Catch ex As Excepcion.CampoObrigatorioException
                Throw ex
            Catch ex As Excepcion.NegocioExcepcion
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try

            Return 0
        End Function
#End Region

    End Class


End Namespace