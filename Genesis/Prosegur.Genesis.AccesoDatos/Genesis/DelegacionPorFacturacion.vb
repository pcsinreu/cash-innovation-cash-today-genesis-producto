Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports System.Text
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Namespace Genesis
    Public Class DelegacionPorFacturacion

        Shared Function ObtenerPorDelegacion(identificadorDelegacion As String) As String

            Dim codigo As String = ""

            If Not String.IsNullOrEmpty(identificadorDelegacion) Then
                Using cmd As IDbCommand = AcessoDados.CriarComando(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS)
                    Dim dtDados As DataTable
                    Dim sqlResource As String = My.Resources.Delegacion_ObtenerCodigoPorIdentificador

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Curta, identificadorDelegacion))
                    cmd.CommandText = Prosegur.Genesis.AccesoDatos.Util.PrepararQuery(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, sqlResource)

                    dtDados = AcessoDados.ExecutarDataTable(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, cmd)

                    If dtDados IsNot Nothing AndAlso dtDados.Rows.Count > 0 Then
                        codigo = dtDados.Rows(0)("COD_DELEGACION")
                    End If
                End Using
            End If

            Return codigo
        End Function
    End Class

End Namespace
