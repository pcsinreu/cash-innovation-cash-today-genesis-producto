Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Text

<Serializable()> _
Public Class Bancos
    Inherits List(Of Banco)

#Region "[VARIÁVEIS]"

    Private _Cliente As Cliente
    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Cliente() As Cliente
        Get
            If _Cliente Is Nothing Then
                _Cliente = New Cliente
            End If
            Return _Cliente
        End Get
        Set(Value As Cliente)
            _Cliente = Value
        End Set
    End Property

    Public Property IdPS() As String
        Get
            Return _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Popular a lista de objetos de banco.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReporteRealizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.BancosReportRealizar.ToString()
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' declarar objeto banco
            Dim objBanco As Banco = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto moneda
                objBanco = New Banco
                objBanco.Id = dr("IdCliente")
                objBanco.IdPS = dr("IdPS")
                objBanco.Descripcion = dr("Descripcion")

                If dt.Rows(0)("CodClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodClienteGenesis = dt.Rows(0)("CodClienteGenesis")
                Else
                    objBanco.CodClienteGenesis = String.Empty
                End If

                If dt.Rows(0)("CodSubClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodSubClienteGenesis = dt.Rows(0)("CodSubClienteGenesis")
                Else
                    objBanco.CodSubClienteGenesis = String.Empty
                End If

                If dt.Rows(0)("CodPuntoServicioGenesis") IsNot DBNull.Value Then
                    objBanco.CodPuntoServicioGenesis = dt.Rows(0)("CodPuntoServicioGenesis")
                Else
                    objBanco.CodPuntoServicioGenesis = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objBanco)

            Next

        End If


    End Sub

    Public Sub Realizar()

        If Cliente.Id <> 0 Then
            BancosClienteRealizar()
        Else
            BancosRealizar()
        End If

    End Sub

    Private Sub BancosRealizar()

        Dim filtros As New StringBuilder ' criar filtros
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS) ' criar comando

        If IdPS <> String.Empty Then
            If filtros.Length = 0 Then
                filtros.Append(" WHERE IdPS = :IdPS ")
            Else
                filtros.Append(" AND IdPS = :IdPS ")
            End If
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, IdPS))
        End If

        If Descripcion <> String.Empty Then
            If filtros.Length = 0 Then
                filtros.Append(" WHERE Descripcion LIKE '%" & Descripcion & "%' ")
            Else
                filtros.Append(" AND Descripcion LIKE '%" & Descripcion & "%' ")
            End If
        End If

        ' obter query
        comando.CommandText = My.Resources.BancosRealizar.ToString & filtros.ToString
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' declarar objeto banco
            Dim objBanco As Banco = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto banco
                objBanco = New Banco
                objBanco.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objBanco.Descripcion = dr("Descripcion")
                Else
                    objBanco.Descripcion = String.Empty
                End If

                If dr("IdPS") IsNot DBNull.Value Then
                    objBanco.IdPS = dr("IdPS")
                Else
                    objBanco.IdPS = String.Empty
                End If

                If dr("CodClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodClienteGenesis = dr("CodClienteGenesis")
                Else
                    objBanco.CodClienteGenesis = String.Empty
                End If

                If dr("CodSubClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodSubClienteGenesis = dr("CodSubClienteGenesis")
                Else
                    objBanco.CodSubClienteGenesis = String.Empty
                End If

                If dr("CodPuntoServicioGenesis") IsNot DBNull.Value Then
                    objBanco.CodPuntoServicioGenesis = dr("CodPuntoServicioGenesis")
                Else
                    objBanco.CodPuntoServicioGenesis = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objBanco)

            Next

        End If

    End Sub

    Private Sub BancosClienteRealizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS) ' criar comando

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Cliente.Id))

        comando.CommandText = My.Resources.BancosClienteRealizar.ToString
        comando.CommandType = CommandType.Text

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' declarar objeto banco
            Dim objBanco As Banco = Nothing

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' criar objeto banco
                objBanco = New Banco
                objBanco.Id = dr("Id")

                If dr("Descripcion") IsNot DBNull.Value Then
                    objBanco.Descripcion = dr("Descripcion")
                Else
                    objBanco.Descripcion = String.Empty
                End If

                If dr("IdPS") IsNot DBNull.Value Then
                    objBanco.IdPS = dr("IdPS")
                Else
                    objBanco.IdPS = String.Empty
                End If

                If dr("CodClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodClienteGenesis = dr("CodClienteGenesis")
                Else
                    objBanco.CodClienteGenesis = String.Empty
                End If

                If dr("CodSubClienteGenesis") IsNot DBNull.Value Then
                    objBanco.CodSubClienteGenesis = dr("CodSubClienteGenesis")
                Else
                    objBanco.CodSubClienteGenesis = String.Empty
                End If

                If dr("CodPuntoServicioGenesis") IsNot DBNull.Value Then
                    objBanco.CodPuntoServicioGenesis = dr("CodPuntoServicioGenesis")
                Else
                    objBanco.CodPuntoServicioGenesis = String.Empty
                End If

                ' adicionar para colecao
                Me.Add(objBanco)

            Next
        End If

    End Sub

#End Region

End Class