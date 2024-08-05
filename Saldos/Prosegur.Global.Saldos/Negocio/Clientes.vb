Imports System.Collections.Generic
Imports Prosegur.DbHelper

<Serializable()> _
Public Class Clientes
    Inherits List(Of Cliente)

#Region "[VARIÁVEIS]"

    Private _Banco As Banco
    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String
    Private _Planta As Planta

#End Region

#Region "[PROPRIEDADES]"

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property IdPS() As String
        Get
            IdPS = _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
        End Set
    End Property

    Public Property Banco() As Banco
        Get
            If _Banco Is Nothing Then
                _Banco = New Banco
            End If
            Banco = _Banco
        End Get
        Set(Value As Banco)
            _Banco = Value
        End Set
    End Property

    Public Property Planta() As Planta
        Get
            If _Planta Is Nothing Then
                _Planta = New Planta()
            End If
            Planta = _Planta
        End Get
        Set(Value As Planta)
            _Planta = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        If Me.Banco.Id = 0 Then

            If Me.Planta.Id = 0 Then
                ClientesRealizarIdPS()
            Else
                ClientesRealizarIdPlanta()
            End If

        Else
            ClientesRealizarIdBanco()
        End If

    End Sub

    ''' <summary>
    ''' Pesquisa os clientes que tenha o IDPS igual ao informado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/01/2013 - Criado
    ''' </history>
    Public Sub RealizarIdPs()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClientesRealizarPorIdPs.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' adicionar para colecao
                Me.Add(PopularCliente(dr))

            Next

        End If

    End Sub

    Private Sub ClientesRealizarIdPS()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClientesRealizarIdPS.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' adicionar para colecao
                Me.Add(PopularCliente(dr))

            Next

        End If

    End Sub

    Private Sub ClientesRealizarIdBanco()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClientesRealizarPorBanco.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Inteiro_Longo, Me.Banco.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' adicionar para colecao
                Me.Add(PopularCliente(dr))

            Next

        End If

    End Sub

    Private Sub ClientesRealizarIdPlanta()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClientesRealizarIdPlanta.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Inteiro_Longo, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' percorrer todos registros encontrados
            For Each dr As DataRow In dt.Rows

                ' adicionar para colecao
                Me.Add(PopularCliente(dr))

            Next

        End If

    End Sub

    Private Function PopularCliente(dr As DataRow) As Negocio.Cliente

        ' criar objeto banco
        Dim objCliente As New Cliente
        objCliente.Id = Convert.ToInt32(dr("Id"))

        If dr("Descripcion") IsNot DBNull.Value Then
            objCliente.Descripcion = dr("Descripcion")
        Else
            objCliente.Descripcion = String.Empty
        End If

        If dr("IdPS") IsNot DBNull.Value Then
            objCliente.IdPS = dr("IdPS")
        Else
            objCliente.IdPS = String.Empty
        End If

        If (dr("DescCorta") IsNot DBNull.Value) Then
            objCliente.DescCorta = dr("DescCorta")
        End If
        If dr("IdMatriz") IsNot DBNull.Value Then
            objCliente.Matriz.Id = dr("IdMatriz")
        Else
            objCliente.Matriz.Id = 0
        End If

        If dr("CodClienteGenesis") IsNot DBNull.Value Then
            objCliente.CodClienteGenesis = dr("CodClienteGenesis")
        Else
            objCliente.CodClienteGenesis = String.Empty
        End If

        If dr("CodSubClienteGenesis") IsNot DBNull.Value Then
            objCliente.CodSubClienteGenesis = dr("CodSubClienteGenesis")
        Else
            objCliente.CodSubClienteGenesis = String.Empty
        End If

        If dr("CodPuntoServicioGenesis") IsNot DBNull.Value Then
            objCliente.CodPuntoServicioGenesis = dr("CodPuntoServicioGenesis")
        Else
            objCliente.CodPuntoServicioGenesis = String.Empty
        End If

        If dr("IdClienteSaldo") IsNot DBNull.Value Then
            objCliente.IdClienteSaldo = Convert.ToInt32(dr("IdClienteSaldo"))
        Else
            objCliente.IdClienteSaldo = 0
        End If

        If dr("CodNivelSaldo") IsNot DBNull.Value Then
            objCliente.CodNivelSaldo = Convert.ToInt32(dr("CodNivelSaldo"))
        Else
            objCliente.CodNivelSaldo = Nothing
        End If

        If dr("SaldosPorSucursal") IsNot DBNull.Value Then
            objCliente.SaldosPorSucursal = Convert.ToBoolean(dr("SaldosPorSucursal"))
        End If

        Return objCliente

    End Function

#End Region

End Class