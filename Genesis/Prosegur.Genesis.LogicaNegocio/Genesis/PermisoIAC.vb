Imports Prosegur.Genesis.Comon

Public Class PermisoIAC
    Public Shared Function ObtenerPermisos(unPermiso As Clases.Permiso) As List(Of Clases.Permiso)
        Try
            Return AccesoDatos.Permiso.ObtenerPermisos(unPermiso)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
