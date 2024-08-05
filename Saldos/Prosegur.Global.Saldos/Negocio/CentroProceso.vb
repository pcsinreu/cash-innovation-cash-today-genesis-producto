Imports Prosegur.DbHelper

<Serializable()> _
Public Class CentroProceso

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Planta As Planta
    Private _Descripcion As String
    Private _Matriz As CentroProceso
    Private _Motivos As Motivos
    Private _TipoCentroProceso As TipoCentroProceso
    Private _SeDispone As Boolean
    Private _EsTesoro As Boolean
    Private _EsConteo As Boolean
    Private _SaldoPorTotal As Boolean
    Private _IdPS As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property IdPS() As String
        Get
            IdPS = _IdPS
        End Get
        Set(Value As String)
            _IdPS = Value
        End Set
    End Property

    Public Property EsTesoro() As Boolean
        Get
            EsTesoro = _EsTesoro
        End Get
        Set(Value As Boolean)
            _EsTesoro = Value
        End Set
    End Property

    ''' <summary>
    ''' Valor do mapeamento EsConteo para a tabela PD_CENTROPROCESSO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EsConteo() As Boolean
        Get
            EsConteo = _EsConteo
        End Get
        Set(Value As Boolean)
            _EsConteo = Value
        End Set
    End Property

    ''' <summary>
    ''' Define se o centro de processo vai manter o saldo agrupado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SaldoPorTotal() As Boolean
        Get
            SaldoPorTotal = _SaldoPorTotal
        End Get
        Set(Value As Boolean)
            _SaldoPorTotal = Value
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

    Public Property TipoCentroProceso() As TipoCentroProceso
        Get
            If _TipoCentroProceso Is Nothing Then
                _TipoCentroProceso = New TipoCentroProceso()
            End If
            TipoCentroProceso = _TipoCentroProceso
        End Get
        Set(Value As TipoCentroProceso)
            _TipoCentroProceso = Value
        End Set
    End Property

    Public Property Motivos() As Motivos
        Get
            If _Motivos Is Nothing Then
                _Motivos = New Motivos()
            End If
            Motivos = _Motivos
        End Get
        Set(Value As Motivos)
            _Motivos = Value
        End Set
    End Property

    Public Property Matriz() As CentroProceso
        Get
            If _Matriz Is Nothing Then
                _Matriz = New CentroProceso()
            End If
            Matriz = _Matriz
        End Get
        Set(Value As CentroProceso)
            _Matriz = Value
        End Set
    End Property

    Public Property SeDispone() As Boolean
        Get
            SeDispone = _SeDispone
        End Get
        Set(Value As Boolean)
            _SeDispone = Value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Descripcion = _Descripcion
        End Get
        Set(Value As String)
            _Descripcion = Value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Id = _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

#End Region

#Region "MÉTODOS"

    Public Sub Registrar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        If Me.Id = 0 Then

            Me.Id = ObterIdCentroProceso()
            Me.InserirCentroProceso(objTransacao)

        Else
            AlterarCentroProceso(objTransacao)
        End If

        If Motivos IsNot Nothing Then

            Me.BorrarMotivoCentroProceso(objTransacao)

            For Each MotivoCp As Motivo In Motivos
                MotivoCentroProcesoRegistrar(MotivoCp.Id, objTransacao)
            Next

        End If

        objTransacao.RealizarTransacao()

    End Sub

    Private Sub InserirCentroProceso(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CentroProcesoregistrarInsert.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Inteiro_Longo, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Observacao_Curta, Me.Descripcion))

        If Me.Matriz.Id = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Inteiro_Longo, Me.TipoCentroProceso.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeDispone", ProsegurDbType.Logico, Me.SeDispone))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsTesoro", ProsegurDbType.Logico, Me.EsTesoro))

        ' Verifica se o saldos está sendo usado em uma multiagencia
        If Not String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG")) AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG") = 1 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SaldoPorTotal", ProsegurDbType.Logico, Me.SaldoPorTotal))
            comando.CommandText = String.Format(comando.CommandText, "SaldoPorTotal,", ":SaldoPorTotal,")
        Else
            comando.CommandText = String.Format(comando.CommandText, String.Empty, String.Empty)
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub AlterarCentroProceso(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CentroProcesoregistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Inteiro_Longo, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Observacao_Curta, Me.Descripcion))

        If Me.Matriz.Id = 0 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCentroProceso", ProsegurDbType.Inteiro_Longo, Me.TipoCentroProceso.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeDispone", ProsegurDbType.Logico, Me.SeDispone))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsTesoro", ProsegurDbType.Logico, Me.EsTesoro))        
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsConteo", ProsegurDbType.Logico, Me.EsConteo))

        ' Verifica se o saldos está sendo usado em uma multiagencia
        If Not String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG")) AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG") = 1 Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SaldoPorTotal", ProsegurDbType.Logico, Me.SaldoPorTotal))
            comando.CommandText = String.Format(comando.CommandText, ", " & vbCrLf & "	    SaldoPorTotal		 = :SaldoPorTotal")
        Else
            comando.CommandText = String.Format(comando.CommandText, String.Empty)
        End If

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub MotivoCentroProcesoRegistrar(idMotivo As Integer, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CentroProcesoMotivoCentroProcesoRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMotivo", ProsegurDbType.Inteiro_Longo, idMotivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Private Sub BorrarMotivoCentroProceso(ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CentroProcesoMotivoCentroProcesoBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Eliminar()

        Dim objTransacao As New Transacao(Constantes.CONEXAO_SALDOS)

        BorrarMotivoCentroProceso(objTransacao)
        EliminarCentroProceso(objTransacao)

        objTransacao.RealizarTransacao()


    End Sub

    Public Sub EliminarCentroProceso(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CentroProcesoEliminar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CentroProcesoRealizar.ToString()
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG")) AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG") = 1 Then
            comando.CommandText = String.Format(comando.CommandText, "CP.SaldoPorTotal,")
        Else
            comando.CommandText = String.Format(comando.CommandText, String.Empty)
        End If

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Planta.Id = dt.Rows(0)("IdPlanta")
            Me.Planta.Realizar()
            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdCentroProcesoMatriz") IsNot DBNull.Value Then

                Me.Matriz.Id = dt.Rows(0)("IdCentroProcesoMatriz")

                If dt.Rows(0)("DescCentroProcesoMatriz") IsNot DBNull.Value Then
                    Me.Matriz.Descripcion = dt.Rows(0)("DescCentroProcesoMatriz")
                End If

            Else
                Me.Matriz.Id = 0
            End If

            Me.TipoCentroProceso.Id = dt.Rows(0)("IdTipoCentroProceso")
            Me.TipoCentroProceso.Descripcion = dt.Rows(0)("DescTipoCentroProceso")
            Me.Motivos.CentroProceso.Id = Me.Id
            Me.Motivos.Realizar()

            If dt.Rows(0)("SeDispone") IsNot Nothing Then
                Me.SeDispone = Convert.ToBoolean(dt.Rows(0)("SeDispone"))
            Else
                Me.SeDispone = False
            End If

            If dt.Rows(0)("EsTesoro") IsNot Nothing Then
                Me.EsTesoro = Convert.ToBoolean(dt.Rows(0)("EsTesoro"))
            Else
                Me.EsTesoro = False
            End If

            'Atributo EsConteo
            If dt.Rows(0)("EsConteo") IsNot Nothing Then
                Me.EsConteo = Convert.ToBoolean(dt.Rows(0)("EsConteo"))
            Else
                Me.EsConteo = False
            End If

            'Atributo SaldosPorTotal quando o saldos está configurado em uma multiagencia
            If Not String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG")) AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("SALDOS_MAG") = 1 AndAlso dt.Rows(0)("SaldoPorTotal") IsNot Nothing Then
                Me.SaldoPorTotal = Convert.ToBoolean(dt.Rows(0)("SaldoPorTotal"))
            Else
                Me.SaldoPorTotal = False
            End If

            If dt.Rows(0)("IdPs") IsNot DBNull.Value Then
                Me.IdPS = dt.Rows(0)("IdPs")
            End If

        End If

    End Sub

    Public Shared Function ObterIdCentroProceso() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SCentroProceso.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    ''' <summary>
    ''' Registra o Automata e o Centro de processo
    ''' </summary>
    ''' <param name="IdAutomata">Código do automata</param>
    ''' <param name="transacao">Objeto com a transação</param>
    ''' <remarks></remarks>
    Public Sub AutomataCentroProcesoRegistrar(IdAutomata As Long, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.AutomataCentroProcesoRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, IdAutomata))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProceso", ProsegurDbType.Inteiro_Longo, Me.Id))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    Public Function CentroProcesoDescripcionV2(IdCentroProceso As Integer, IdCentroProcesoVar As String, Busqueda As Integer) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text

        If (Busqueda = 0) Then
            comando.CommandText = My.Resources.CentroProcesoDescricaoPorId.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Descricao_Longa, IdCentroProceso))
        Else
            comando.CommandText = My.Resources.CentroProcesoDescricaoPorIdVar.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdClienteVar", ProsegurDbType.Descricao_Longa, IdCentroProcesoVar))
        End If

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class