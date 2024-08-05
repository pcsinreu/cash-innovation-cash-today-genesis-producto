Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class TiposCampoExtra
    Inherits List(Of TipoCampoExtra)

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtem uma lista do tipo TipoCampoExtra com todos os registros da tabela PD_TipoCampoExtra
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.TiposCampoExtraRealizar.ToString()
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' declarar objeto tipo copia
            Dim objTipoCampoExtra As TipoCampoExtra = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto tipo copia
                objTipoCampoExtra = New TipoCampoExtra
                objTipoCampoExtra.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objTipoCampoExtra.Descripcion = dr("Descripcion")
                Else
                    objTipoCampoExtra.Descripcion = String.Empty
                End If

                If dr("Codigo") IsNot DBNull.Value Then
                    objTipoCampoExtra.Codigo = dr("Codigo")
                Else
                    objTipoCampoExtra.Codigo = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objTipoCampoExtra)

            Next
        End If

    End Sub

#End Region

End Class