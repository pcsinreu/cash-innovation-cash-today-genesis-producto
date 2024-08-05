Imports Prosegur.Genesis.Comon

Public Class Rol
    Public Shared Function ObtenerRoles(unRol As Clases.Rol, Optional modoDetallado As Boolean = False) As List(Of Clases.Rol)
        Try
            Return AccesoDatos.Rol.ObtenerRoles(unRol, modoDetallado)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Grabar(unaPeticion As Prosegur.Genesis.ContractoServicio.Contractos.Permisos.PeticionGrabarRol) As ContractoServicio.Contractos.Permisos.RespuestaGrabarRol
        Try
            Return AccesoDatos.Rol.Grabar(unaPeticion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
