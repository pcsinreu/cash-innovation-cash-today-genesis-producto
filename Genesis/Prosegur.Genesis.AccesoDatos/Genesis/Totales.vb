Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports System.Collections.ObjectModel

Namespace Genesis

    ''' <summary>
    ''' Totales
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Totales

        Shared Function ObtenerValoresElemento_v2(ByRef identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValoresTotalesElemento_v2
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

        Shared Function ObtenerValoresElemento_v3(ByRef identificadoresRemesas As List(Of String)) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String = My.Resources.ValoresTotalesElemento_v3
                Dim filtro As String = ""

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function


        Shared Function ObtenerValoresElementoAbono(ByRef identificadoresRemesas As List(Of String), tipoValor As Enumeradores.TipoValor) As DataTable

            Dim dt As DataTable

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim query As String
                Dim filtro As String = ""

                If tipoValor = Enumeradores.TipoValor.Declarado Then
                    query = My.Resources.ValoresTotalesDeclaradosElemento
                Else
                    query = My.Resources.ValoresTotalesContadosElemento
                End If

                If identificadoresRemesas IsNot Nothing Then
                    If identificadoresRemesas.Count = 1 Then
                        filtro &= " AND R.OID_REMESA = []OID_REMESA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_REMESA", ProsegurDbType.Descricao_Curta, identificadoresRemesas(0)))
                    ElseIf identificadoresRemesas.Count > 0 Then
                        filtro &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresRemesas, "OID_REMESA", cmd, "AND", "R", , False)
                    End If
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(query, filtro))
                cmd.CommandType = CommandType.Text

                dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            Return dt

        End Function

    End Class

End Namespace

