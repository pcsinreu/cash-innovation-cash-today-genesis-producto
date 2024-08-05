Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace NuevoSalidas

    Public Class TipoModulo

        ''' <summary>
        ''' Função que retorna o OidTipoModulo conforme o código de tipo módulo passado.
        ''' </summary>
        ''' <param name="codTipoModulo"></param>
        ''' <returns></returns>
        ''' <history>[cbomtempo]	29/04/2014	Creado</history>
        ''' <remarks></remarks>
        Public Shared Function RecuperarOidTipoModulo(codTipoModulo As String) As String

            'Cria o comando SQL
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALIDAS)
            With cmd
                .CommandType = CommandType.Text
                .Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALIDAS, "COD_TIPO_MODULO", ProsegurDbType.Descricao_Curta, codTipoModulo))
                .CommandText = Util.PrepararQuery(Constantes.CONEXAO_SALIDAS, My.Resources.Salidas_TipoModulo_RecuperarOidTipoModulo)
            End With
            'executa o sql e retorna um valor
            Return Util.ValidarValor(DbHelper.AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALIDAS, cmd).Rows("OID_TIPO_MODULO"))
        End Function

    End Class

End Namespace
