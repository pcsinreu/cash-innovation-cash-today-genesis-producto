Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports System.Transactions
Imports Prosegur.Genesis.ContractoServicio

Public Class AccionDatoBancario
    Implements ContractoServicio.IDatoBancario

    Public Function GetDatosBancarios(Peticion As ContractoServicio.DatoBancario.GetDatosBancarios.Peticion) As ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta Implements ContractoServicio.IDatoBancario.GetDatosBancarios

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.GetDatosBancarios.Respuesta

        Try
            If Not String.IsNullOrEmpty(Peticion.IdentificadorCliente) Then

                ' obter
                objRespuesta.DatosBancarios = AccesoDatos.DatoBancario.GetDatosBancarios(Peticion)
                If Peticion.ObtenerSubNiveis AndAlso objRespuesta.DatosBancarios.Count = 0 Then
                    'Tenta buscar sem o ponto de serviço
                    If Not String.IsNullOrEmpty(Peticion.IdentificadorPuntoServicio) Then
                        Peticion.IdentificadorPuntoServicio = String.Empty
                        objRespuesta.DatosBancarios = AccesoDatos.DatoBancario.GetDatosBancarios(Peticion)
                    End If
                    'Tenta buscar sem o sub cliente
                    If Not String.IsNullOrEmpty(Peticion.IdentificadorSubCliente) Then
                        Peticion.IdentificadorSubCliente = String.Empty
                        objRespuesta.DatosBancarios = AccesoDatos.DatoBancario.GetDatosBancarios(Peticion)
                    End If
                End If

            End If

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function


    Public Function GetDatosBancariosDefectos(datosClientes As List(Of ContractoServicio.DatoBancario.GetDatosBancarios.Peticion)) As List(Of Comon.Clases.DatoBancario)

        Dim datosBancarios As New List(Of Comon.Clases.DatoBancario)

        If datosClientes IsNot Nothing AndAlso datosClientes.Count > 0 Then
            datosBancarios = AccesoDatos.DatoBancario.GetDatosBancariosDefectos(datosClientes)
        End If

        Return datosBancarios

    End Function

    Public Function AprobarRechazar(listaOidDatosBancariosCambio As List(Of String), usuario As String, accion As String, comentario As String, tester_aprobacion As Boolean, codigoPais As String) As ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta
        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        Try
            AccesoDatos.DatoBancario.AprobarRechazar(listaOidDatosBancariosCambio, usuario, accion, comentario, tester_aprobacion, codigoPais)

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

    Public Function GetDatosBancariosCambio(pPeticion As Contractos.Integracion.RecuperarDatosBancarios.Peticion) As List(Of Comon.Clases.DatoBancarioGrilla)
        Try
            Return AccesoDatos.DatoBancario.RecuperarDatosBancariosCambio(pPeticion)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Sub SetComentario(pPeticion As Genesis.ContractoServicio.DatoBancario.SetComentario.Peticion)
        Genesis.AccesoDatos.DatoBancario.SetComentario(pPeticion)
    End Sub

    Public Shared Function GetComentarios(pIdentificador As String) As Genesis.ContractoServicio.DatoBancario.GetComentario.Respuesta
        Dim retorno As New Genesis.ContractoServicio.DatoBancario.GetComentario.Respuesta
        retorno = Genesis.AccesoDatos.DatoBancario.GetComentarios(pIdentificador)
        Return retorno
    End Function

    Public Function GetDatoBancarioComparativo(identificador As String) As Comon.Clases.DatoBancarioComparativo
        Try
            Return New AccesoDatos.DatoBancario().RecuperarDatoBancarioComparativo(identificador)
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function SetDatosBancarios(Peticion As Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion,
                                            Optional ByRef _objTransacion As DataBaseHelper.Transaccion = Nothing) As ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        Try
            AccesoDatos.DatoBancario.SetDatosBancarios(Peticion, _objTransacion)


            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    <Obsolete("Esta Funcion es obsoleta. Se deberá utilizar la peticion Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion ")>
    Public Function SetDatosBancarios(Peticion As ContractoServicio.DatoBancario.SetDatosBancarios.Peticion,
                                            Optional ByRef _objTransacion As Transacao = Nothing) As ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta


        Try
            Dim objTransacion As Transacao
            If _objTransacion IsNot Nothing Then
                objTransacion = _objTransacion
            Else
                objTransacion = New Prosegur.DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            End If

            AccesoDatos.DatoBancario.BajaDatosBancarios(Peticion.IdentificadorCliente, Peticion.IdentificadorSubCliente, Peticion.IdentificadorPuntoServicio, objTransacion)

            If Peticion.DatosBancarios IsNot Nothing AndAlso Peticion.DatosBancarios.Count > 0 Then
                AccesoDatos.DatoBancario.SetDatosBancarios(Peticion.DatosBancarios, Peticion.IdentificadorCliente, Peticion.IdentificadorSubCliente, Peticion.IdentificadorPuntoServicio, Peticion.CodigoUsuario, objTransacion)
            End If



            If _objTransacion Is Nothing Then
                objTransacion.RealizarTransacao()
            End If

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function
    Public Function SetDatosBancariosSenBaja(Peticion As ContractoServicio.DatoBancario.SetDatosBancarios.Peticion) As ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        Try
            If Peticion.DatosBancarios IsNot Nothing AndAlso Peticion.DatosBancarios.Count > 0 Then
                AccesoDatos.DatoBancario.SetDatosBancarios(Peticion.DatosBancarios, Peticion.IdentificadorCliente, Peticion.IdentificadorSubCliente, Peticion.IdentificadorPuntoServicio, Peticion.CodigoUsuario, Nothing)
            End If

            ' preparar codigos e mensagens do respuesta
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Function AlterarCuentaEstandar(peticion As ContractoServicio.DatoBancario.AlterarCuentaEstandar.Peticion) As ContractoServicio.DatoBancario.AlterarCuentaEstandar.Respuesta

        Dim objRespuesta As New ContractoServicio.DatoBancario.AlterarCuentaEstandar.Respuesta

        Try

            If (peticion.Identificador IsNot Nothing) Then
                AccesoDatos.DatoBancario.AlterarBolDefectoDatoBancario(peticion.Identificador, peticion.BolDefecto, Nothing)
            End If

        Catch ex As Excepcion.NegocioExcepcion

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada
            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao

        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            ' caso ocorra alguma exceção, trata o objeto Respuesta da forma adequada            
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString()
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

    Public Function Test() As ContractoServicio.Test.Respuesta Implements ContractoServicio.IDatoBancario.Test
        Dim objRespuesta As New ContractoServicio.Test.Respuesta

        Try

            AccesoDatos.Test.TestarConexao()

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = Traduzir("021_SemErro")

        Catch ex As Excepcion.NegocioExcepcion

            objRespuesta.CodigoError = ex.Codigo
            objRespuesta.MensajeError = ex.Descricao


        Catch ex As Exception
            Util.TratarErroBugsnag(ex)

            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            objRespuesta.MensajeError = ex.ToString
            objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta
    End Function

End Class