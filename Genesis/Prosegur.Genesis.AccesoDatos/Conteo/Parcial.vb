Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Conteo

    Public Class Parcial

        Public Shared Function RecuperarParcialesBulto(identificadorRemesa As String) As String

            Using comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_CONTEO)

                comando.CommandText = My.Resources.ConteoRecuperarParcialesBulto
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_CONTEO, "OID_REMESA", ProsegurDbType.Objeto_Id, identificadorRemesa))
                comando.CommandType = CommandType.Text
                comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_CONTEO, comando.CommandText)
                Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_CONTEO, comando)

            End Using

        End Function

    End Class

End Namespace