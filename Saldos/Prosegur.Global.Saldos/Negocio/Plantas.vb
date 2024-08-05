Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class Plantas
    Inherits List(Of Planta)

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.PlantasRealizar()
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Dim objPlanta As Planta = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto moneda
                objPlanta = New Planta

                objPlanta.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objPlanta.Descripcion = dr("Descripcion")
                Else
                    objPlanta.Descripcion = String.Empty
                End If

                If dr("IdPS") IsNot DBNull.Value Then
                    objPlanta.IdPS = dr("IdPS")
                Else
                    objPlanta.IdPS = String.Empty
                End If

                If dr("IdPSDescripcion") IsNot DBNull.Value Then
                    objPlanta.IdPSDescripcion = dr("IdPSDescripcion")
                Else
                    objPlanta.IdPSDescripcion = String.Empty
                End If

                If dr("CodDelegacionGenesis") IsNot DBNull.Value Then
                    objPlanta.CodDelegacionGenesis = dr("CodDelegacionGenesis")
                Else
                    objPlanta.CodDelegacionGenesis = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objPlanta)

            Next

        End If

    End Sub

#End Region

End Class