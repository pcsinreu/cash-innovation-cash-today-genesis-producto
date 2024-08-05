Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Conteo

    Public Class BultoParcializado

        Public Shared Sub ActualizarBultoPadre(identificadorBultoPadre As String,
                                               identificadoresBultoParcializado As List(Of String),
                                               ByRef transacion As DbHelper.Transacao)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_CONTEO)
                cmd.CommandText = My.Resources.ConteoBultoParcializadoActualizarBultoPadre
                cmd.CommandType = CommandType.Text
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "OID_BULTO_PADRE", ProsegurDbType.Identificador_Alfanumerico, identificadorBultoPadre))
                cmd.CommandText = String.Format(cmd.CommandText, Util.MontarClausulaIn(Constantes.CONEXAO_CONTEO, identificadoresBultoParcializado, "OID_BULTO_PARCIALIZADO", cmd, "WHERE", "BP"))
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_CONTEO, cmd.CommandText)

                If transacion Is Nothing Then
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_CONTEO, cmd)
                Else
                    transacion.AdicionarItemTransacao(cmd)
                End If

            End Using

        End Sub

    End Class

End Namespace