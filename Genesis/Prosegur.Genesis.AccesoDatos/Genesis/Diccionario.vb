Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class Diccionario

        Public Shared Function ObtenerValorDicionario(codIdioma As String, codFuncionalidad As String, codExpresion As String) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerValorDiccionario)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDIOMA", ProsegurDbType.Descricao_Curta, codIdioma))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Descricao_Curta, codFuncionalidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXPRESION", ProsegurDbType.Descricao_Curta, codExpresion))

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Public Shared Function ObtenerValorDicionarioSimples(codIdioma As String, codFuncionalidad As String, codExpresion As String) As String

            Dim valor As String = String.Empty

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerValorDiccionarioConGenericos)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDIOMA", ProsegurDbType.Descricao_Curta, codIdioma))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Descricao_Curta, codFuncionalidad))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXPRESION", ProsegurDbType.Descricao_Curta, codExpresion))

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count >= 1 Then
                    valor = dt(0)("VALOR")
                End If

            End Using

            Return valor

        End Function

        Public Shared Function ObtenerValorDicionario(codIdioma As String, codFuncionalidad As String) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, My.Resources.ObtenerValoresDiccionario)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDIOMA", ProsegurDbType.Descricao_Curta, codIdioma))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_FUNCIONALIDAD", ProsegurDbType.Descricao_Curta, codFuncionalidad))

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

    End Class
End Namespace

