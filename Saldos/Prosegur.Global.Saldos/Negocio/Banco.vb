Imports Prosegur.DBHelper
Imports Prosegur
Imports System.Data.SqlClient

<Serializable()> _
Public Class Banco

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String
    Private strInstancia As String
    Private _Clientes As Clientes
    Private _CodClienteGenesis As String
    Private _CodSubClienteGenesis As String
    Private _CodPuntoServicioGenesis As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property clientes() As Clientes
        Get
            Return _Clientes
        End Get
        Set(Value As Clientes)
            _Clientes = Value
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

    Public Property CodClienteGenesis() As String
        Get
            Return _CodClienteGenesis
        End Get
        Set(value As String)
            _CodClienteGenesis = value
        End Set
    End Property

    Public Property CodSubClienteGenesis() As String
        Get
            Return _CodSubClienteGenesis
        End Get
        Set(value As String)
            _CodSubClienteGenesis = value
        End Set
    End Property

    Public Property CodPuntoServicioGenesis() As String
        Get
            Return _CodPuntoServicioGenesis
        End Get
        Set(value As String)
            _CodPuntoServicioGenesis = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    Public Sub Registrar()

        If Me.Id = 0 Then
            Dim IdCliente As Integer = Cliente.ObterIdCliente()
            Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)
            InserirCliente(IdCliente, objTransacao)
            InserirBanco(IdCliente, objTransacao)
            objTransacao.RealizarTransacao()
        Else
            AlterarCliente()
        End If

    End Sub

    Private Sub InserirCliente(IdCliente As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.BancoRegistrarClienteInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, IdCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub InserirBanco(IdCliente As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.BancoRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, IdCliente))
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub AlterarCliente()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.BancoRegistrarClienteUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Curta, Me.IdPS))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub Eliminar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        EliminarBanco(objTransacao) ' remover banco

        EliminarCliente(objTransacao) ' remover cliente

        objTransacao.RealizarTransacao() ' realizar transacao

    End Sub

    Protected Sub EliminarBanco(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.BancoEliminarClienteBanco.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Protected Sub EliminarCliente(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.BancoEliminarCliente.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.BancoRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            If dt.Rows(0)("Descripcion") IsNot DBNull.Value Then
                Me.Descripcion = dt.Rows(0)("Descripcion")
            Else
                Me.Descripcion = String.Empty
            End If

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("CodClienteGenesis") IsNot DBNull.Value Then
                Me.CodClienteGenesis = dt.Rows(0)("CodClienteGenesis")
            Else
                Me.CodClienteGenesis = String.Empty
            End If

            If dt.Rows(0)("CodSubClienteGenesis") IsNot DBNull.Value Then
                Me.CodSubClienteGenesis = dt.Rows(0)("CodSubClienteGenesis")
            Else
                Me.CodSubClienteGenesis = String.Empty
            End If

            If dt.Rows(0)("CodPuntoServicioGenesis") IsNot DBNull.Value Then
                Me.CodPuntoServicioGenesis = dt.Rows(0)("CodPuntoServicioGenesis")
            Else
                Me.CodPuntoServicioGenesis = String.Empty
            End If

        End If

    End Sub

    Public Sub RealizarPorCanalGenesis()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.BancoRealizarPorCanalGenesis.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodClienteGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodSubClienteGenesis))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            If dt.Rows(0)("Descripcion") IsNot DBNull.Value Then
                Me.Descripcion = dt.Rows(0)("Descripcion")
            Else
                Me.Descripcion = String.Empty
            End If

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("CodClienteGenesis") IsNot DBNull.Value Then
                Me.CodClienteGenesis = dt.Rows(0)("CodClienteGenesis")
            Else
                Me.CodClienteGenesis = String.Empty
            End If

            If dt.Rows(0)("CodSubClienteGenesis") IsNot DBNull.Value Then
                Me.CodSubClienteGenesis = dt.Rows(0)("CodSubClienteGenesis")
            Else
                Me.CodSubClienteGenesis = String.Empty
            End If

            If dt.Rows(0)("CodPuntoServicioGenesis") IsNot DBNull.Value Then
                Me.CodPuntoServicioGenesis = dt.Rows(0)("CodPuntoServicioGenesis")
            Else
                Me.CodPuntoServicioGenesis = String.Empty
            End If

        End If

    End Sub

#End Region

End Class