Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class EstadosComprobante
    Inherits List(Of EstadoComprobante)

#Region "[MÉTODOS]"

    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.EstadosComprobanteRealizar.ToString()
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto automata
            Dim objEstadoComprobante As EstadoComprobante = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto automata
                objEstadoComprobante = New EstadoComprobante

                objEstadoComprobante.Id = dr("IdEstadoComprobante")
                objEstadoComprobante.Descripcion = dr("Descripcion")
                objEstadoComprobante.Codigo = dr("Codigo")

                ' adicionar para colecao
                Me.Add(objEstadoComprobante)

            Next

        End If

    End Sub

#End Region

End Class