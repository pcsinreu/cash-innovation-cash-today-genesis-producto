Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.DatoBancario
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.DBHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports System.Transactions

Public Class DatoBancario

    Public Function SetDatosBancarios(Peticion As ContractoServicio.DatoBancario.SetDatosBancarios.Peticion,
                                            Optional ByRef _objTransacion As Transacao = Nothing) As ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        ' criar objeto respuesta
        Dim objRespuesta As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

        Try
            Dim objTransacion As Transacao
            If _objTransacion IsNot Nothing Then
                objTransacion = _objTransacion
            Else
                objTransacion = New Prosegur.DbHelper.Transacao(AccesoDatos.Constantes.CONEXAO_GENESIS)
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
            ' objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

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
            '  objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

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
            ' objRespuesta.NombreServidorBD = AccesoDatos.Util.RetornaNomeServidorBD

        End Try

        Return objRespuesta

    End Function

End Class