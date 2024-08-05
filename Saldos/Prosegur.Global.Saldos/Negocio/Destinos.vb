Imports Prosegur.DBHelper
Imports Prosegur
Imports System.Data.SqlClient
Imports System.Collections.Generic

<Serializable()> _
Public Class Destinos
    Inherits List(Of Destino)

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand ' criar comando
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.DestinosRealizar.ToString()

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto usuario
            Dim objDestino As Negocio.Destino = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                objDestino = New Negocio.Destino()

                objDestino.Id = dr("Id")
                objDestino.Descripcion = dr("Descripcion")

                If dr("IdGenesis") IsNot DBNull.Value Then
                    objDestino.IdGenesis = dr("IdGenesis")
                Else
                    objDestino.IdGenesis = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objDestino)

            Next

        End If

    End Sub

#End Region

End Class