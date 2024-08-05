Imports Prosegur.DbHelper

Public Class Test

    ''' <summary>
    ''' Testar Conexao
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub TestarConexao()

        ' inicializar variáveis
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

        ' obter comando
        comando.CommandText = My.Resources.Test
        comando.CommandType = CommandType.Text

        AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, comando)
    End Sub

End Class
