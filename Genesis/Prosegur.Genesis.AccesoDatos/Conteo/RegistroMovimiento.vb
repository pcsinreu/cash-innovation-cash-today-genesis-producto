Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Conteo

    Public Class RegistroMovimiento

        Public Shared Function ActualizarBulto(identificadorBulto As String,
                                   identificadorRemesa As String,
                                   ByRef transacion As DbHelper.Transacao) As Integer

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_CONTEO)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "OID_BULTO", ProsegurDbType.Identificador_Alfanumerico, identificadorBulto))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_CONTEO, My.Resources.ConteoRegistroMovimientoActualizarBulto)

                If transacion Is Nothing Then
                    Return AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_CONTEO, cmd)
                Else
                    transacion.AdicionarItemTransacao(cmd)
                End If

            End Using

        End Function

        Public Shared Function ValidarModificacionRegistroMovimiento(identificadorRemesa As String,
                                                                     fechaMax As DateTime) As Integer

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_CONTEO)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "OID_REMESA", ProsegurDbType.Identificador_Alfanumerico, identificadorRemesa))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "FECHA_MAX", ProsegurDbType.Data_Hora, fechaMax))
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_CONTEO, My.Resources.ConteoRegistroMovimientoValidarModificacion)

                Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_CONTEO, cmd)

            End Using

        End Function

    End Class

End Namespace