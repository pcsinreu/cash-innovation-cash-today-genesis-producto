Namespace Dashboard

    Public Class ElementosProcesados

        Shared Sub InsertarActualizar(FechaHoraFinConteo As DateTime, CodigoDelegacion As String, DescripcionDelegacion As String, CodigoSector As String, DescripcionSector As String,
                                      CodigoCliente As String, DescripcionCliente As String, CodigoProducto As String, DescripcionProducto As String,
                                      NelCantidadTotalRemesas As Long, NelCantidadTotalBultos As Long, NelCantidadTotalParciales As Long)


            AccesoDatos.Dashboard.ElementosProcesados.InsertarActualizar(FechaHoraFinConteo, CodigoDelegacion, DescripcionDelegacion, CodigoSector, DescripcionSector,
                                                                         CodigoCliente, DescripcionCliente, CodigoProducto, DescripcionProducto, "PR", "Procesada",
                                                                         NelCantidadTotalRemesas, NelCantidadTotalBultos, NelCantidadTotalParciales)


        End Sub

    End Class

End Namespace