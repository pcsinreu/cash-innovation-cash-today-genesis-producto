Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class TiposCopia
    Inherits List(Of TipoCopia)


#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.TiposCopiaRealizar.ToString()
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' declarar objeto tipo copia
            Dim objTipoCopia As TipoCopia = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto tipo copia
                objTipoCopia = New TipoCopia()

                objTipoCopia.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objTipoCopia.Descripcion = dr("Descripcion")
                Else
                    objTipoCopia.Descripcion = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objTipoCopia)

            Next
        End If

    End Sub

#End Region

End Class