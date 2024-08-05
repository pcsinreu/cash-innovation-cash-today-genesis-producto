Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class Formularios

        Public Shared Function ObtenerPlanxMovimientos(oidPlanificacion As String) As List(Of Clases.Formulario)


            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)


            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerPlanXMovimientos)
            cmd.CommandType = CommandType.Text


            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, oidPlanificacion))


            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Dim objMovimientos As New List(Of Clases.Formulario)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows

                    Dim objMovimiento As New Clases.Formulario

                    ' preencher a coleção com objetos divisa
                    objMovimientos.Add(PopularPlanxMovimiento(row))

                Next row

            End If

            Return objMovimientos

        End Function

        Private Shared Function PopularPlanxMovimiento(row As DataRow) As Clases.Formulario
            Dim formulario = New Clases.Formulario
            Util.AtribuirValorObjeto(formulario.Identificador, row("OID_FORMULARIO"), GetType(String))
            Util.AtribuirValorObjeto(formulario.Codigo, row("COD_FORMULARIO"), GetType(String))
            Util.AtribuirValorObjeto(formulario.Descripcion, row("DES_FORMULARIO"), GetType(String))
            Return formulario
        End Function

    End Class
End Namespace
