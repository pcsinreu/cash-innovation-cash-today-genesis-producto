Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class ValorIACRemesa

        ''' <summary>
        ''' Insere o valor do IAC para a Remesa.
        ''' </summary>
        ''' <param name="identificadorRemesa">identificador da Remesa.</param>
        ''' <param name="termino">Termino com o valor.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACRemesaInserir(identificadorRemesa As String, termino As Clases.TerminoIAC, usuario As String)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACRemesaInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_VALOR_IACXREMESA", ProsegurDbType.Objeto_Id, System.Guid.NewGuid.ToString))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TERMINO", ProsegurDbType.Descricao_Longa, termino.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR_IAC", ProsegurDbType.Descricao_Longa, termino.Valor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
        End Sub

        ''' <summary>
        ''' Exclui o valor do termino para Remesa.
        ''' </summary>
        ''' <param name="identificadorRemesa">Identificador da remesa que será utilizado na exclusão.</param>
        ''' <remarks></remarks>
        Public Shared Sub ValorIACRemesaExcluir(identificadorRemesa As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValorIACRemesaExcluir)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            End Using
        End Sub
    End Class
End Namespace

