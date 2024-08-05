Imports Prosegur.DbHelper

Public Class Bulto

    ''' <summary>
    ''' Retorna a divisa local do bulto
    ''' </summary>
    ''' <param name="OidBulto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornarDivisaLocal(OidBulto As String) As String

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BultoRetornarDivisaLocal.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_BULTO", ProsegurDbType.Objeto_Id, OidBulto))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

End Class
