Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class Limites
        Public Shared Function ObtenerLimites(oidPlanificacion As String, oidMaquina As String, oidPuntoServicio As String) As List(Of Clases.Limite)

            Try
                Return AccesoDatos.Genesis.Limites.ObtenerLimites(oidPlanificacion, oidMaquina, oidPuntoServicio)

            Catch ex As Exception
                Throw

            End Try

        End Function
    End Class
End Namespace
