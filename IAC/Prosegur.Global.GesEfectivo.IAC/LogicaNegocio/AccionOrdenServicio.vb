Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio


Public Class AccionOrdenServicio

    Public Function GetOrdenesServicio(pPeticion As Contractos.Integracion.RecuperarOrdenesServicio.Peticion) As List(Of Comon.Clases.OrdenServicio)
        Try
            Return AccesoDatos.OrdenServicio.RecuperarOrdenesServicio(pPeticion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOrdenesServicioDetalles(pPeticion As Contractos.Integracion.RecuperarDetallesOrdenesServicio.Peticion) As List(Of Comon.Clases.OrdenServicioDetalle)
        Try
            Return AccesoDatos.OrdenServicio.RecuperarDetallesOrdenesServicio(pPeticion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOrdenesServicioNotificaciones(pPeticion As Contractos.Integracion.RecuperarNotificacionesOrdenesServicio.Peticion) As List(Of Comon.Clases.OrdenServicioNotificacion)
        Try
            Return AccesoDatos.OrdenServicio.RecuperarNotificacionesOrdenesServicio(pPeticion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOrdenesServicioDetNotificaciones(pPeticion As Contractos.Integracion.RecuperarDetNotificacionesOrdenesServicio.Peticion) As List(Of Comon.Clases.OrdenServicioDetNotificacion)
        Try
            Return AccesoDatos.OrdenServicio.RecuperarDetNotificacionesOrdenesServicio(pPeticion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetNotificacionesApiGlobal(IdentificadorLlamada As String, OidIntegracion As String, peticion As Contractos.Job.EnviarNotificacion.Peticion) As Contractos.Notification.Nilo.Request
        Try
            Dim log As New Text.StringBuilder()
            Dim objPeticion As New Contractos.Job.EnviarNotificacion.Peticion With {
                .CodigoPais = peticion.CodigoPais,
                .Configuracion = New Contractos.Job.EnviarNotificacion.Entrada.Configuracion With {
                .Usuario = peticion.Configuracion.Usuario,
                .IdentificadorAjeno = peticion.Configuracion.IdentificadorAjeno
                }
            }

            Return Genesis.AccesoDatos.RecuperarNotificaciones.Ejecutar(IdentificadorLlamada, objPeticion, OidIntegracion, log)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As Boolean
        Try
            Return AccesoDatos.OrdenServicio.RecalcularSaldoAcuerdo(oid_saldo_acuerdo_ref, cod_usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function NotificarSaldoAcuerdo(oid_saldo_acuerdo_ref As String, cod_usuario As String) As Boolean
        Try
            Return AccesoDatos.OrdenServicio.NotificarSaldoAcuerdo(oid_saldo_acuerdo_ref, cod_usuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
