Imports Prosegur.DbHelper

Namespace Reportes

    Public Class InterfazResultado

        Public Shared Function ExecutarQuery(query As String) As DataTable
            Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_REPORTES)
            Dim cmd As IDbCommand = conexao.CreateCommand
            Dim dt As DataTable

            Try
                cmd.CommandText = query
                cmd.CommandType = CommandType.Text
                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_REPORTES, cmd)
            Finally
                AcessoDados.Desconectar(conexao)
            End Try

            Return dt
        End Function

    End Class

End Namespace