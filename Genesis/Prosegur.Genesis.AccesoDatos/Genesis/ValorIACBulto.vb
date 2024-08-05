Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ValorIACBulto

        ''' <summary>
        ''' Insere o valor do termino para bulto.
        ''' </summary>
        ''' <param name="identificadorBulto">Identificador do bulto.</param>
        ''' <param name="termino">Termino com valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACBultoInserir(identificadorBulto As String, termino As Clases.TerminoIAC, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACBultoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_IACXBULTO", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Descricao_Longa, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR_IAC", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para bulto.
        ''' </summary>
        ''' <param name="identificadorBulto">Identificador do bulto que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACBultoExcluir(identificadorBulto As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACBultoExcluir)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BULTO", ProsegurDbType.Objeto_Id, identificadorBulto))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub
    End Class

End Namespace
