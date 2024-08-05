Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class TiposCentroProceso
    Inherits List(Of TipoCentroProceso)

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.TiposCentroProcesoRealizar.ToString()
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto automata
            Dim objTipoCentroProceso As TipoCentroProceso = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto automata
                objTipoCentroProceso = New TipoCentroProceso

                objTipoCentroProceso.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objTipoCentroProceso.Descripcion = dr("Descripcion")
                Else
                    objTipoCentroProceso.Descripcion = String.Empty
                End If

                If dr("IdPS") IsNot DBNull.Value Then
                    objTipoCentroProceso.IdPS = dr("IdPS")
                Else
                    objTipoCentroProceso.IdPS = String.Empty
                End If

                Me.Add(objTipoCentroProceso)

            Next

        End If

    End Sub

#End Region

End Class