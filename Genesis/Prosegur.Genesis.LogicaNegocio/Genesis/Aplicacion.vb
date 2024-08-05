Imports Prosegur.Genesis.Comon

Public Class Aplicacion
    Public Shared Function ObtenerAplicaciones(unaAplicacion As Clases.Aplicacion) As List(Of Clases.Aplicacion)
        Try
            Return AccesoDatos.Genesis.Aplicacion.ObtenerAplicaciones(unaAplicacion)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
