Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient

<Serializable()> _
Public Class Cliente
    Inherits List(Of Cliente)

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _IdPS As String
    Private _Bancos As Bancos
    Private _DescCorta As String
    Private _Matriz As Cliente
    Private _SaldosPorSucursal As Boolean
    Private _CodClienteGenesis As String
    Private _CodSubClienteGenesis As String
    Private _CodPuntoServicioGenesis As String
    Private _CodNivelSaldo As Nullable(Of Integer)
    Private _IdClienteSaldo As Integer
    Private _Logo As String
    Private _ConImagen As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property CodNivelSaldo() As Nullable(Of Integer)
        Get
            Return _CodNivelSaldo
        End Get
        Set(value As Nullable(Of Integer))
            _CodNivelSaldo = value
        End Set
    End Property

    Public Property ConImagen() As Boolean
        Get
            Return _ConImagen
        End Get
        Set(value As Boolean)
            _ConImagen = value
        End Set
    End Property

    Public Property IdClienteSaldo() As Integer
        Get
            Return _IdClienteSaldo
        End Get
        Set(value As Integer)
            _IdClienteSaldo = value
        End Set
    End Property

    Public Property Logo() As String
        Get
            Return _Logo
        End Get
        Set(value As String)
            _Logo = value
        End Set
    End Property

    Public Property SaldosPorSucursal() As Boolean
        Get
            Return _SaldosPorSucursal
        End Get
        Set(Value As Boolean)
            _SaldosPorSucursal = Value
        End Set
    End Property

    Public Property Matriz() As Cliente
        Get
            If _Matriz Is Nothing Then
                _Matriz = New Cliente()
            End If
            Return _Matriz
        End Get
        Set(Value As Cliente)
            _Matriz = Value
        End Set
    End Property

    Public Property DescCorta() As String
        Get
            Return _DescCorta
        End Get
        Set(Value As String)
            _DescCorta = Value
        End Set
    End Property

    Public Property Bancos() As Bancos
        Get
            If _Bancos Is Nothing Then
                _Bancos = New Bancos()
            End If
            Return _Bancos
        End Get
        Set(Value As Bancos)
            _Bancos = Value
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

    Public Property IdPS() As String
        Get
            Return _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
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

#Region "[MÉTODOS]"

    Public Sub ReporteRealizar(IdsCentroProcesso As String)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = String.Format(My.Resources.ClientesReportRealizar.ToString(), IdsCentroProcesso.Replace("|", ",").Substring(0, IdsCentroProcesso.Length - 1))
        comando.CommandType = CommandType.Text
        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesso", ProsegurDbType.Descricao_Longa, IdsCentroProcesso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then

            ' Cria um objeto de centro de processo
            'Dim objCentroProceso As CentroProceso = Nothing
            Dim objClientes As Cliente = Nothing

            ' Para cada centro de processo encontrado
            For Each dr As DataRow In dt.Rows

                ' Associa os dados do dr ao objeto do centro de custo
                objClientes = New Cliente
                objClientes.Id = dr("IdCliente")
                objClientes.Descripcion = dr("Descripcion")

                ' Adiciona o cliente na lista
                Me.Add(objClientes)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Atualiza ou Insere o cliente
    ''' </summary>
    ''' <history>
    ''' [vinicius.gama] 21/09/09 Alterado - altera e insere os campos CodClienteGenesis, CodSubClienteGenesis, CodPuntoServicioGenesis 
    ''' caso sejam informados
    ''' </history>
    Public Sub Registrar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then
            Me.Id = Cliente.ObterIdCliente()
            InserirCliente(objTransacao)
        Else
            AlterarCliente(objTransacao)
        End If

        BorrarBancos(objTransacao)

        If Bancos IsNot Nothing Then

            For Each b As Banco In Bancos
                ClienteBancoRegistrar(b.Id, objTransacao)
            Next

        End If

        objTransacao.RealizarTransacao()

    End Sub

    Sub ClienteBancoRegistrar(IdBanco As Integer, ByRef objTransacao As Transacao)

        'PD_ClienteBancoRegistrar

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClienteBancoRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Inteiro_Longo, IdBanco))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Eliminar()
        Dim transacao As New Transacao(Constantes.CONEXAO_SALDOS)

        BorrarBancos(transacao)
        BorrarCliente(transacao)
        transacao.RealizarTransacao()

    End Sub

    Private Sub InserirCliente(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ClienteRegistrar.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsBanco", ProsegurDbType.Inteiro_Curto, 0))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DescCorta", ProsegurDbType.Identificador_Alfanumerico, Me.DescCorta))

        If Me.IdClienteSaldo = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteSaldo", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteSaldo", ProsegurDbType.Inteiro_Longo, Me.IdClienteSaldo))
        End If

        If Me.Matriz.Id = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMatriz", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        End If

        If Me.CodNivelSaldo IsNot Nothing Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodNivelSaldo", ProsegurDbType.Inteiro_Curto, Me.CodNivelSaldo))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodNivelSaldo", ProsegurDbType.Inteiro_Curto, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SaldosPorSucursal", ProsegurDbType.Logico, Me.SaldosPorSucursal))

        If Me.CodClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodClienteGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        If Me.CodSubClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodSubClienteGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        If Me.CodPuntoServicioGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, Me.CodPuntoServicioGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        If Not String.IsNullOrEmpty(Me.Logo) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Logo", ProsegurDbType.Descricao_Longa, Me.Logo))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Logo", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "ConImagen", ProsegurDbType.Logico, Me.ConImagen))


        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub AlterarCliente(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ClienteUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DescCorta", ProsegurDbType.Identificador_Alfanumerico, Me.DescCorta))

        If Me.IdClienteSaldo = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteSaldo", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteSaldo", ProsegurDbType.Inteiro_Longo, Me.IdClienteSaldo))
        End If

        If Me.Matriz.Id = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMatriz", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        End If

        If Me.CodNivelSaldo IsNot Nothing Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodNivelSaldo", ProsegurDbType.Inteiro_Curto, Me.CodNivelSaldo))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodNivelSaldo", ProsegurDbType.Inteiro_Curto, DBNull.Value))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SaldosPorSucursal", ProsegurDbType.Logico, Me.SaldosPorSucursal))

        If Me.CodClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodClienteGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        If Me.CodSubClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodSubClienteGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        If Me.CodPuntoServicioGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, Me.CodPuntoServicioGenesis))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, DBNull.Value))
        End If

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarBancos(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ClienteEliminarBanco.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub BorrarCliente(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ClienteEliminar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.ClienteRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("CodNivelSaldo") IsNot DBNull.Value Then

                If Me.CodNivelSaldo Is Nothing Then
                    Me.CodNivelSaldo = New Integer
                End If

                Me.CodNivelSaldo = Convert.ToInt32(dt.Rows(0)("CodNivelSaldo"))

            End If

            If dt.Rows(0)("DescCorta") IsNot DBNull.Value Then
                Me.DescCorta = dt.Rows(0)("DescCorta")
            Else
                Me.DescCorta = String.Empty
            End If

            If dt.Rows(0)("IdMatriz") IsNot DBNull.Value Then
                Me.Matriz.Id = dt.Rows(0)("IdMatriz")
            Else
                Me.Matriz.Id = 0
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

            Me.SaldosPorSucursal = dt.Rows(0)("SaldosPorSucursal")
            Me.Bancos.Cliente.Id = Me.Id
            Me.Bancos.Realizar()

        End If

    End Sub

    Public Shared Function ObterIdCliente() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SCliente.ToString()
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

    ''' <summary>
    ''' Retorna um cliente pelos campos do genesis.
    ''' </summary>
    ''' <hitory>
    ''' vinicius.gama 21/09/09 Alterado - Agora o filtro sera somente pelos campos informados
    ''' </hitory>
    Public Sub RealizarPorClienteGenesis()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        Dim filtros As String = String.Empty

        comando.CommandText = My.Resources.ClienteRealizarPorClienteGenesis.ToString()
        comando.CommandType = CommandType.Text

        If Me.CodClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodClienteGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodClienteGenesis = :CodClienteGenesis"
            Else
                filtros &= " WHERE CodClienteGenesis = :CodClienteGenesis"
            End If
        Else
            If filtros.Count > 0 Then
                filtros &= " AND CodClienteGenesis IS NULL"
            Else
                filtros &= " WHERE CodClienteGenesis IS NULL"
            End If
        End If

        If Me.CodSubClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodSubClienteGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodSubClienteGenesis = :CodSubClienteGenesis"
            Else
                filtros &= " WHERE CodSubClienteGenesis = :CodSubClienteGenesis"
            End If
        Else
            If filtros.Count > 0 Then
                filtros &= " AND CodSubClienteGenesis IS NULL"
            Else
                filtros &= " WHERE CodSubClienteGenesis IS NULL"
            End If
        End If

        If Me.CodPuntoServicioGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, Me.CodPuntoServicioGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodPuntoServicioGenesis = :CodPuntoServicioGenesis"
            Else
                filtros &= " WHERE CodPuntoServicioGenesis = :CodPuntoServicioGenesis"
            End If
        Else
            If filtros.Count > 0 Then
                filtros &= " AND CodPuntoServicioGenesis IS NULL"
            Else
                filtros &= " WHERE CodPuntoServicioGenesis IS NULL"
            End If
        End If

        comando.CommandText &= filtros

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("IdCliente")

            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("DescCorta") IsNot DBNull.Value Then
                Me.DescCorta = dt.Rows(0)("DescCorta")
            Else
                Me.DescCorta = String.Empty
            End If

            If dt.Rows(0)("IdMatriz") IsNot DBNull.Value Then
                Me.Matriz.Id = dt.Rows(0)("IdMatriz")
            Else
                Me.Matriz.Id = 0
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

            Me.SaldosPorSucursal = dt.Rows(0)("SaldosPorSucursal")
            Me.Bancos.Cliente.Id = Me.Id
            Me.Bancos.Realizar()

        End If

    End Sub

    Public Function ClienteDescripcion(IdCliente As Integer, IdClienteVar As String, Busqueda As Integer) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text

        If (Busqueda = 0) Then
            comando.CommandText = My.Resources.ClienteDescricaoBuscarPorID.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Descricao_Longa, IdCliente))
        Else
            comando.CommandText = My.Resources.ClienteDescricaoBuscarPorIDVar.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteVar", ProsegurDbType.Descricao_Longa, IdClienteVar))
        End If
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

    Public Shared Function RealizarBusquedaIdSubClientePuntoServicio(Idps As String) As Integer

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        cmd.CommandText = My.Resources.ClienteSelecionarSubCliente
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDPS", ProsegurDbType.Identificador_Alfanumerico, Idps))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, cmd)
    End Function

    ''' <summary>
    ''' Retorna um cliente pelas propriedades preenchidas.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RealizarPorPropriedades()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        Dim filtros As String = String.Empty

        comando.CommandText = My.Resources.ClienteRealizarPorClienteGenesis.ToString()
        comando.CommandType = CommandType.Text

        If Me.Id <> 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Id", ProsegurDbType.Descricao_Longa, Me.Id))
            If filtros.Count > 0 Then
                filtros &= " AND Id = :Id"
            Else
                filtros &= " WHERE Id = :Id"
            End If
        End If

        If Me.Descripcion <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
            If filtros.Count > 0 Then
                filtros &= " AND Descripcion = :Descripcion"
            Else
                filtros &= " WHERE Descripcion = :Descripcion"
            End If
        End If

        If Me.IdPS <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Descricao_Longa, Me.IdPS))
            If filtros.Count > 0 Then
                filtros &= " AND IdPS = :IdPS"
            Else
                filtros &= " WHERE IdPS = :IdPS"
            End If
        End If

        If Me.DescCorta <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DescCorta", ProsegurDbType.Descricao_Longa, Me.DescCorta))
            If filtros.Count > 0 Then
                filtros &= " AND DescCorta = :DescCorta"
            Else
                filtros &= " WHERE DescCorta = :DescCorta"
            End If
        End If

        If Me.CodClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodClienteGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodClienteGenesis = :CodClienteGenesis"
            Else
                filtros &= " WHERE CodClienteGenesis = :CodClienteGenesis"
            End If
        End If

        If Me.CodSubClienteGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodSubClienteGenesis", ProsegurDbType.Descricao_Longa, Me.CodSubClienteGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodSubClienteGenesis = :CodSubClienteGenesis"
            Else
                filtros &= " WHERE CodSubClienteGenesis = :CodSubClienteGenesis"
            End If
        End If

        If Me.CodPuntoServicioGenesis <> String.Empty Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "CodPuntoServicioGenesis", ProsegurDbType.Descricao_Longa, Me.CodPuntoServicioGenesis))
            If filtros.Count > 0 Then
                filtros &= " AND CodPuntoServicioGenesis = :CodPuntoServicioGenesis"
            Else
                filtros &= " WHERE CodPuntoServicioGenesis = :CodPuntoServicioGenesis"
            End If
        End If

        comando.CommandText &= filtros

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = dt.Rows(0)("IdCliente")

            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("DescCorta") IsNot DBNull.Value Then
                Me.DescCorta = dt.Rows(0)("DescCorta")
            Else
                Me.DescCorta = String.Empty
            End If

            If dt.Rows(0)("IdMatriz") IsNot DBNull.Value Then
                Me.Matriz.Id = dt.Rows(0)("IdMatriz")
            Else
                Me.Matriz.Id = 0
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

            Me.SaldosPorSucursal = dt.Rows(0)("SaldosPorSucursal")
            Me.Bancos.Cliente.Id = Me.Id
            Me.Bancos.Realizar()

        End If

    End Sub

    ''' <summary>
    ''' Busca Cliente por Idps
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RealizarPorIdPs()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ClientesRealizarPorIdPs.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")

            Me.Id = dt.Rows(0)("Id")

            If dt.Rows(0)("IdPS") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPS")
            Else
                Me.IdPS = String.Empty
            End If

            If dt.Rows(0)("CodNivelSaldo") IsNot DBNull.Value Then

                If Me.CodNivelSaldo Is Nothing Then
                    Me.CodNivelSaldo = New Integer
                End If

                Me.CodNivelSaldo = Convert.ToInt32(dt.Rows(0)("CodNivelSaldo"))

            End If

            If dt.Rows(0)("DescCorta") IsNot DBNull.Value Then
                Me.DescCorta = dt.Rows(0)("DescCorta")
            Else
                Me.DescCorta = String.Empty
            End If

            If dt.Rows(0)("IdMatriz") IsNot DBNull.Value Then
                Me.Matriz.Id = dt.Rows(0)("IdMatriz")
            Else
                Me.Matriz.Id = 0
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

            Me.SaldosPorSucursal = dt.Rows(0)("SaldosPorSucursal")
            Me.Bancos.Cliente.Id = Me.Id
            Me.Bancos.Realizar()

        End If

    End Sub


#End Region

End Class