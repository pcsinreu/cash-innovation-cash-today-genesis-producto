Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio
Public Class AccionObtenerInformacionLogin
    Public Shared Function Ejecutar(Peticion As Login.ObtenerInformacionLogin.Peticion) As Login.EjecutarLogin.Respuesta
        Dim respuesta As New Login.EjecutarLogin.Respuesta
        Try
            respuesta = AccesoDatos.Login.ObtenerInformacionLogin(Peticion)
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
            respuesta.MensajeError = ex.ToString()
        End Try
        Return respuesta
    End Function
End Class
