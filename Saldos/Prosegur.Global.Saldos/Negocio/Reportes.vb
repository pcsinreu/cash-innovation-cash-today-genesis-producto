Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Reportes
    Inherits List(Of Reporte)

    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.ReportesRealizar.ToString()
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objReporte As Reporte = Nothing

            For Each dr As DataRow In dt.Rows

                objReporte = New Reporte
                objReporte.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objReporte.Descripcion = dr("Descripcion")
                Else
                    objReporte.Descripcion = String.Empty
                End If

                Me.Add(objReporte)

            Next

        End If
    End Sub

End Class