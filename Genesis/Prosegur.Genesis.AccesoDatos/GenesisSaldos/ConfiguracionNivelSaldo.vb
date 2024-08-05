Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe ConfiguracionNivelSaldo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013
    ''' </history>
    Public Class ConfiguracionNivelSaldo

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Recupera o identificador da configuração nivel saldo.
        ''' </summary>
        ''' <param name="CodCliente"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarIdentificadorNivelSaldo(CodCliente As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ConfiguracionNivelSaldoRetornarOid)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodCliente))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

#End Region

    End Class

End Namespace