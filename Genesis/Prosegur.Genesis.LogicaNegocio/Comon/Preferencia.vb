Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports System.Transactions


Public Class Preferencia
    Public Shared Function ObtenerPreferencias(peticion As ObtenerPreferenciasPeticion) As ObtenerPreferenciasRespuesta
        Dim respuesta As New ObtenerPreferenciasRespuesta()

        Try
            respuesta.Preferencias = AccesoDatos.Preferencia.ObtenerPreferencias(
                peticion.CodigoUsuario, peticion.codigoAplicacion, peticion.CodigoFuncionalidad)

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Mensajes.Add(ex.Descricao)
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)
        End Try

        Return respuesta
    End Function

    Public Shared Function GuardarPreferencias(peticion As GuardarPreferenciasPeticion) As GuardarPreferenciasRespuesta
        Dim respuesta As New GuardarPreferenciasRespuesta()

        Try
            AccesoDatos.Preferencia.GuardarPreferencia(peticion.Preferencias)

        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Mensajes.Add(ex.Descricao)
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)
        End Try

        Return respuesta
    End Function

    Public Shared Function BorrarPreferenciasAplicacion(peticion As BorrarPreferenciasAplicacionPeticion) As BorrarPreferenciasAplicacionRespuesta
        Dim respuesta As New BorrarPreferenciasAplicacionRespuesta()

        Try
            AccesoDatos.Preferencia.BorrarPreferenciasAplicacion(peticion.CodigoAplicacion, peticion.CodigoUsuario, peticion.CodigoFuncionalidad)
        Catch ex As Excepcion.NegocioExcepcion
            respuesta.Mensajes.Add(ex.Descricao)
        Catch ex As Exception
            Util.TratarErroBugsnag(ex)
            respuesta.Excepciones.Add(ex.Message)
        End Try

        Return respuesta
    End Function
End Class
