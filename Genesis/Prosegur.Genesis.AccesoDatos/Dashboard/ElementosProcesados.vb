Imports Prosegur.DbHelper

Namespace Dashboard

    Public Class ElementosProcesados

#Region "[INSERIR]"

        Shared Sub InsertarActualizar(FechaHoraFinConteo As DateTime, CodigoDelegacion As String, DescripcionDelegacion As String, CodigoSector As String, DescripcionSector As String, _
                                      CodigoCliente As String, DescripcionCliente As String, CodigoProducto As String, DescripcionProducto As String, CodigoEstado As String, DescripcionEstado As String, _
                                      NelCantidadTotalRemesas As Long, NelCantidadTotalBultos As Long, NelCantidadTotalParciales As Long)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_DASHBOARD)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_DASHBOARD, My.Resources.Dashboard_ElementosProcesados_InserirActualizar)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "OID_ELEMENTOS_PROCESADOS", ProsegurDbType.Objeto_Id, Guid.NewGuid().ToString()))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "FECHA_HORA", ProsegurDbType.Data_Hora, FechaHoraFinConteo))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, CodigoDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_DELEGACION", ProsegurDbType.Descricao_Longa, DescripcionDelegacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_SECTOR", ProsegurDbType.Descricao_Longa, CodigoSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_SECTOR", ProsegurDbType.Descricao_Longa, DescripcionSector))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, CodigoCliente))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_CLIENTE", ProsegurDbType.Descricao_Longa, DescripcionCliente))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_PRODUCTO", ProsegurDbType.Descricao_Longa, CodigoProducto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_PRODUCTO", ProsegurDbType.Descricao_Longa, DescripcionProducto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "COD_ESTADO", ProsegurDbType.Descricao_Curta, CodigoEstado))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "DES_ESTADO", ProsegurDbType.Descricao_Longa, DescripcionEstado))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_REMESAS", ProsegurDbType.Inteiro_Longo, NelCantidadTotalRemesas))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_BULTOS", ProsegurDbType.Inteiro_Longo, NelCantidadTotalBultos))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_DASHBOARD, "NEL_CANTIDAD_TOTAL_PARCIALES", ProsegurDbType.Inteiro_Longo, NelCantidadTotalParciales))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_DASHBOARD, cmd)

            End Using

        End Sub

#End Region

    End Class

End Namespace
