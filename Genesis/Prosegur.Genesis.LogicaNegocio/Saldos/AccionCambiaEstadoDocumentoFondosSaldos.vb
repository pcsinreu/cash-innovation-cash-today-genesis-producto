Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration
Imports Prosegur.Global.Saldos
Imports Prosegur.Global.Saldos.ContractoServicio

''' <summary>
''' Classe AccionCambiaEstadoDocumentoFondosSaldos
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 20/
''' </history>
Public Class AccionCambiaEstadoDocumentoFondosSaldos

#Region "[METODOS]'"

    Public Function Ejecutar(Peticion As CambiaEstadoDocumentoFondosSaldos.Peticion) As CambiaEstadoDocumentoFondosSaldos.Respuesta

        Dim objRespuesta As New CambiaEstadoDocumentoFondosSaldos.Respuesta

        Try

            Dim msgErro As String = ValidarDatos(Peticion)

            If Not String.IsNullOrEmpty(msgErro) Then
                objRespuesta.MensajeError = msgErro
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                Return objRespuesta
            End If

            Dim objDocumento As New Negocio.Documento
            objDocumento.Id = Peticion.DatosDocumento.oid_Documento_Saldos
            objDocumento.Realizar()

            If objDocumento.EstadoComprobante IsNot Nothing AndAlso Not String.IsNullOrEmpty(objDocumento.EstadoComprobante.Codigo) Then

                If Peticion.DatosDocumento.Estado.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO) AndAlso
                    objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO) Then

                    Negocio.Documento.AtualizarDocumentoExportadoSalidas(objDocumento.Id)

                    objRespuesta.codEstado = Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO
                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = Traduzir("06_msg_rechazado")
                    Return objRespuesta

                ElseIf Peticion.DatosDocumento.Estado.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO) AndAlso
                           objDocumento.EstadoComprobante.Codigo.Equals(Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO) Then

                    Negocio.Documento.AtualizarDocumentoExportadoSalidas(objDocumento.Id)

                    objRespuesta.codEstado = Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO
                    objRespuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = Traduzir("06_msg_aceptado")
                    Return objRespuesta

                ElseIf objDocumento.EstadoComprobante.Codigo.Equals(Peticion.DatosDocumento.Estado) Then

                    objRespuesta.codEstado = Peticion.DatosDocumento.Estado
                    objRespuesta.cod_comprobante = objDocumento.NumComprobante
                    Return objRespuesta

                End If

            End If

            'Valida a permissão do usuario
            If Not VerificaPermissaoUsuario(objDocumento, Peticion.Usuario, objDocumento.Formulario.Id, Peticion.DatosDocumento.CodCentroProcesoOrigen) Then
                objRespuesta.MensajeError = Traduzir("06_msg_usuario_sin_permiso")
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                Return objRespuesta
            End If

            objDocumento.ExportadoSalidas = True

            Dim campoCP = objDocumento.Formulario.Campos.FirstOrDefault(Function(f) f.Nombre = Prosegur.Global.Saldos.ContractoServicio.Enumeradores.eCampos.CentroProcesoOrigen.ToString)
            Dim cp As Negocio.CentroProceso = Nothing
            If campoCP IsNot Nothing Then
                cp = New Negocio.CentroProceso
                cp.IdPS = campoCP.Valor
                cp.Realizar()
                If cp IsNot Nothing Then
                    objDocumento.GMTVeranoAjuste = If(Not String.IsNullOrEmpty(cp.Planta.CodDelegacionGenesis), CType(Util.GetGMTVeranoAjuste(cp.Planta.CodDelegacionGenesis), Short?), Nothing)
                End If
            End If

            Select Case Peticion.DatosDocumento.Estado

                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO

                    'Aceita o documento
                    objDocumento.Aceptar()
                    objRespuesta.codEstado = Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_ACEPTADO

                Case Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO

                    'Rechaza o documento
                    objDocumento.Rechazar()
                    objRespuesta.codEstado = Prosegur.Global.Saldos.ContractoServicio.Constantes.CONST_ESTADO_COMPROBANTE_RECHAZADO

            End Select

            objRespuesta.cod_comprobante = objDocumento.NumComprobante
            objRespuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            'Caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Prosegur.Genesis.Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()

        End Try

        Return objRespuesta
    End Function

    ''' <summary>
    ''' Verifica a permissão do usuario
    ''' </summary>
    ''' <param name="Documento"></param>
    ''' <param name="Usuario"></param>
    ''' <param name="IdCaracteristica"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/04/2011 - Criado
    ''' </history>
    Private Function VerificaPermissaoUsuario(Documento As Negocio.Documento, _
                                                                                    Usuario As CambiaEstadoDocumentoFondosSaldos.Usuario, _
                                                                                    IdCaracteristica As String, _
                                                                                    IdPsCentroProcesoOrigen As String) As Boolean

        Dim resultCentroProcesoOrigen = From campo In Documento.Formulario.Campos _
                                                                      Where campo.Id = Enumeradores.eCampos.CentroProcesoOrigen


        Dim IdCentroProceso As Integer

        Dim objCentrosProceso As New Negocio.CentrosProceso
        objCentrosProceso.IdPS = IdPsCentroProcesoOrigen
        objCentrosProceso.Realizar()

        If objCentrosProceso.Count > 0 Then

            IdCentroProceso = objCentrosProceso.First.Id

            Dim idUsuario As Integer = Negocio.Usuario.RecuperarIdUsuario(Usuario.Login)

            Dim Formularios As New Negocio.Formularios
            Formularios.CentroProceso.Id = IdCentroProceso
            Formularios.UsuarioActual.Id = idUsuario
            Formularios.Realizar()

            If Formularios.Count > 0 Then

                Dim resultFormularioUsuario = From EFormulario As Negocio.Formulario In Formularios _
                                              Where EFormulario.Id = IdCaracteristica

                If resultFormularioUsuario Is Nothing AndAlso resultFormularioUsuario.Count = 0 Then

                    Return False

                End If

            Else

                Return False

            End If

        Else

            Return False

        End If

        Return True
    End Function

    ''' <summary>
    ''' Executa a validação de dados
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 19/04/2011 - Criado 
    ''' </history>
    Private Function ValidarDatos(Peticion As CambiaEstadoDocumentoFondosSaldos.Peticion) As String

        Dim msgErro As String = String.Empty

        If Peticion.DatosDocumento Is Nothing Then

            msgErro = Traduzir("06_msg_datos_documento")
            Return msgErro
        End If

        If Peticion.Usuario Is Nothing Then

            msgErro = Traduzir("06_msg_datos_Usuario")
            Return msgErro
        End If

        If String.IsNullOrEmpty(Peticion.DatosDocumento.oid_Documento_Saldos) Then

            msgErro = Traduzir("06_msg_IdentificadorDocSaldos")
            Return msgErro
        End If

        If String.IsNullOrEmpty(Peticion.DatosDocumento.Estado) Then

            msgErro = Traduzir("06_msg_Estado")
            Return msgErro
        End If

        If String.IsNullOrEmpty(Peticion.Usuario.Login) Then

            msgErro = Traduzir("06_msg_usuario")
            Return msgErro
        End If

        If String.IsNullOrEmpty(Peticion.Usuario.Clave) Then

            msgErro = Traduzir("06_msg_clave")
            Return msgErro
        End If

        Return String.Empty
    End Function

#End Region

End Class
