Imports Prosegur.Genesis.Comon

Namespace GenesisSaldos

    Public Class ParametroReporte

        Public Shared Function RecuperarParametrosPorTipo(tipoReporte As Enumeradores.TipoReporte) As List(Of Clases.ParametroReporte)

            Dim lstParametroReporte As List(Of Clases.ParametroReporte)

            Try

                lstParametroReporte = AccesoDatos.GenesisSaldos.ParametroReporte.RecuperarParametrosPorTipo(tipoReporte)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Return lstParametroReporte

        End Function

    End Class

End Namespace
