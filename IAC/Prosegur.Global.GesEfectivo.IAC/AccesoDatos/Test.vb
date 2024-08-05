Imports Prosegur.DbHelper

Public Class Test

    ''' <summary>
    ''' Testar Conexao
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub TestarConexao()

        ' inicializar variáveis
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando
        comando.CommandText = Util.PrepararQuery(My.Resources.Test)
        comando.CommandType = CommandType.Text

        AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)
    End Sub

End Class
