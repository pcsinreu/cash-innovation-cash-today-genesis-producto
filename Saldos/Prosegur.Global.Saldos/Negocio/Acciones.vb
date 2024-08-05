Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Text

<Serializable()> _
Public Class Acciones
    Inherits List(Of Accion)

#Region "[METODOS]"

    Public Sub Realizar()

        Dim filtros As New StringBuilder 'criar filtros
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.AccionesRealizar.ToString()
        ' tipo da query
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto Accion
            Dim objBanco As Accion = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto banco
                objBanco = New Accion
                objBanco.Id = dr("Id")
                objBanco.Descripcion = dr("Descripcion")
                objBanco.Codigo = dr("Codigo")

                ' adicionar para colecao
                Me.Add(objBanco)

            Next

        End If

    End Sub

#End Region

End Class