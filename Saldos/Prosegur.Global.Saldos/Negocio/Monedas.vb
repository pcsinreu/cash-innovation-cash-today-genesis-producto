Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Data

<Serializable()> _
Public Class Monedas
    Inherits List(Of Moneda)

#Region "[VARIÁVEIS]"

    Private _Visible As Boolean = True

#End Region

#Region "[PROPRIEDADES]"

    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(value As Boolean)
            _Visible = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Realizar(False)

    End Sub

    ''' <summary>
    ''' Método para buscar os valores da Tabela Moneda e carregar a coleção atual.
    ''' </summary>
    ''' <param name="pBuscarTodosRegistros">Informa se será todos os registros da tabela.</param>
    ''' <remarks></remarks>
    Public Sub Realizar(pBuscarTodosRegistros As Boolean)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandType = CommandType.Text

        If pBuscarTodosRegistros Then
            'Busca todos os registros na tabela e preenche a coleção corrente.
            comando.CommandText = My.Resources.MonedasRealizarTodos.ToString

        Else
            'Busca os registro pelo campo Visible para o Detalhamento no documento
            comando.CommandText = My.Resources.MonedasRealizar.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Visible", ProsegurDbType.Logico, Me.Visible))

        End If

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            ' declarar objeto moneda
            Dim objMoneda As Moneda = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto moneda
                objMoneda = New Moneda
                objMoneda.Id = dr("Id")
                objMoneda.Simbolo = dr("Simbolo")
                objMoneda.Descripcion = dr("Descripcion")
                objMoneda.Visible = dr("Visible")
                If dr("IsoGenesis") IsNot DBNull.Value Then
                    objMoneda.IsoGenesis = dr("IsoGenesis")
                Else
                    objMoneda.IsoGenesis = String.Empty
                End If

                If dr("IdGenesis") IsNot DBNull.Value Then
                    objMoneda.IdGenesis = dr("IdGenesis")
                Else
                    objMoneda.IdGenesis = String.Empty
                End If

                If dr("IsoSaldos") IsNot DBNull.Value Then
                    objMoneda.IsoSaldos = dr("IsoSaldos")
                Else
                    objMoneda.IsoSaldos = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objMoneda)

            Next

        End If

    End Sub

#End Region

End Class