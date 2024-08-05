Imports Prosegur.DbHelper
Imports System.Collections.Generic

<Serializable()> _
Public Class CentrosProceso
    Inherits List(Of CentroProceso)

#Region "VARIAVEIS"

    Private _CentroProcesoActual As CentroProceso
    Private _Usuario As Usuario
    Private _Descripcion As String
    Private _Id As Integer
    Private _Interplantas As Boolean
    Private _DistinguirPorNivel As Boolean
    Private _Matrices As Boolean
    Private _Matriz As CentroProceso
    Private _Planta As Planta
    Private _SoloTesoros As Boolean
    Private _IdPS As String
    Private _TiposCentroProceso As TiposCentroProceso

#End Region

#Region "PROPRIEDADES"

    Public Property TiposCentroProceso() As TiposCentroProceso
        Get
            If _TiposCentroProceso Is Nothing Then
                _TiposCentroProceso = New TiposCentroProceso()
            End If
            Return _TiposCentroProceso
        End Get
        Set(Value As TiposCentroProceso)
            _TiposCentroProceso = Value
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

    Public Property SoloTesoros() As Boolean
        Get
            Return _SoloTesoros
        End Get
        Set(Value As Boolean)
            _SoloTesoros = Value
        End Set
    End Property

    Public Property Interplantas() As Boolean
        Get
            Return _Interplantas
        End Get
        Set(Value As Boolean)
            _Interplantas = Value
        End Set
    End Property

    Public Property DistinguirPorNivel() As Boolean
        Get
            Return _DistinguirPorNivel
        End Get
        Set(Value As Boolean)
            _DistinguirPorNivel = Value
        End Set
    End Property

    Public Property Matrices() As Boolean
        Get
            Return _Matrices
        End Get
        Set(Value As Boolean)
            _Matrices = Value
        End Set
    End Property

    Public Property Usuario() As Usuario
        Get
            If _Usuario Is Nothing Then
                _Usuario = New Usuario()
            End If
            Return _Usuario
        End Get
        Set(Value As Usuario)
            _Usuario = Value
        End Set
    End Property

    Public Property Matriz() As CentroProceso
        Get
            If _Matriz Is Nothing Then
                _Matriz = New CentroProceso()
            End If
            Return _Matriz
        End Get
        Set(Value As CentroProceso)
            _Matriz = Value
        End Set
    End Property

    Public Property CentroProcesoActual() As CentroProceso
        Get
            If _CentroProcesoActual Is Nothing Then
                _CentroProcesoActual = New CentroProceso()
            End If
            Return _CentroProcesoActual
        End Get
        Set(Value As CentroProceso)
            _CentroProcesoActual = Value
        End Set
    End Property

    Public Property Planta() As Planta
        Get
            If _Planta Is Nothing Then
                _Planta = New Planta()
            End If
            Return _Planta
        End Get
        Set(Value As Planta)
            _Planta = Value
        End Set
    End Property

#End Region

#Region "MÉTODOS"

    ''' <summary>
    ''' Método responsável por obter centros de processo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Public Sub Realizar()

        ' selecionar o método a ser executado
        If Me.Usuario.Id = 0 Then

            If Me.IdPS = String.Empty Then ' se não informou idps

                RealizarSemIdpsUsuario()

            Else ' se informou idps

                RealizarPorIdPs()

            End If

        Else ' se informou usuario

            RealizarPorUsuario()

        End If

    End Sub

    ''' <summary>
    ''' Obtém Centros de processo sem informar idps e usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Private Sub RealizarSemIdpsUsuario()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CentrosProcesoRealizar.ToString
        comando.CommandType = CommandType.Text

        Dim strTiposCentroProceso As String = 0
        If Me.TiposCentroProceso.Count > 0 Then strTiposCentroProceso = "|"
        For Each tcp In Me.TiposCentroProceso
            strTiposCentroProceso = strTiposCentroProceso & tcp.Id & "|"
        Next

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoActual", ProsegurDbType.Inteiro_Longo, Me.CentroProcesoActual.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Interplantas", ProsegurDbType.Logico, Me.Interplantas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DistinguirPorNivel", ProsegurDbType.Logico, Me.DistinguirPorNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Matrices", ProsegurDbType.Logico, Me.Matrices))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Inteiro_Longo, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloTesoros", ProsegurDbType.Logico, Me.SoloTesoros))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idTiposCentroProceso", ProsegurDbType.Descricao_Longa, strTiposCentroProceso))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                Me.Add(PopularCentroProceso(dr))

            Next

        End If

    End Sub

    ''' <summary>
    ''' Obtém Centros de Processo por IdPs
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Private Sub RealizarPorIdPs()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CentrosProcesoPorIdps.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPS", ProsegurDbType.Identificador_Alfanumerico, Me.IdPS))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                Me.Add(PopularCentroProceso(dr))

            Next

        End If

    End Sub

    ''' <summary>
    ''' Obtém Centros de Processo por usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Private Sub RealizarPorUsuario()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CentrosProcesoRealizarPorUsuario.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdUsuario", ProsegurDbType.Inteiro_Longo, Me.Usuario.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCentroProcesoMatriz", ProsegurDbType.Inteiro_Longo, Me.Matriz.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SoloTesoros", ProsegurDbType.Logico, Me.SoloTesoros))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows

                Me.Add(PopularCentroProceso(dr))

            Next

        End If

    End Sub

    ''' <summary>
    ''' Recupera uma lista de centros de processo de acordo com o automata
    ''' </summary>
    ''' <param name="IdAutomata">Código do automata</param>
    ''' <remarks></remarks>
    Public Sub RealizarPorAutomata(IdAutomata As Long)

        ' Criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.AutomataCentrosProcesoRealizar()
        comando.CommandType = CommandType.Text

        ' Adicionar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, IdAutomata))

        ' Executar consulta
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' Cria um objeto de centro de processo
        Dim objCentroProceso As CentroProceso = Nothing

        ' Para cada centro de processo encontrado
        For Each dr As DataRow In dt.Rows

            ' Associa os dados do dr ao objeto do centro de custo
            objCentroProceso = New CentroProceso
            objCentroProceso.Id = dr("IdCentroProceso")
            objCentroProceso.Descripcion = dr("Descripcion")
            objCentroProceso.SeDispone = False

            ' Adiciona o centro de processo na lista
            Me.Add(objCentroProceso)

        Next
    End Sub

    ''' <summary>
    ''' Cria e popula objeto centro processo
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 13/05/2009 Criado
    ''' </history>
    Private Function PopularCentroProceso(dr As DataRow) As CentroProceso

        ' preencher planta
        Dim objPlanta As New Planta
        If dr("IdPlanta") IsNot DBNull.Value Then
            objPlanta.Id = dr("IdPlanta")
        End If

        If dr("DescPlanta") IsNot DBNull.Value Then
            objPlanta.Descripcion = dr("DescPlanta")
        End If

        ' preencher centro processo
        Dim objCentroProceso As New CentroProceso
        objCentroProceso.Id = dr("Id")

        If dr("Descripcion") IsNot DBNull.Value Then
            objCentroProceso.Descripcion = dr("Descripcion")
        End If

        If dr("SeDispone") IsNot DBNull.Value Then
            objCentroProceso.SeDispone = Convert.ToBoolean(dr("SeDispone"))
        End If

        objPlanta.Realizar()

        objCentroProceso.Planta = objPlanta

        If dr("IdPS") IsNot DBNull.Value Then
            objCentroProceso.IdPS = dr("IdPS")
        End If

        objCentroProceso.Matriz = Nothing
        objCentroProceso.Motivos = Nothing
        objCentroProceso.TipoCentroProceso = Nothing

        Return objCentroProceso

    End Function

    ''' <summary>
    ''' Apaga todos os centros de processo do automata
    ''' </summary>
    ''' <param name="IdAutomata">Código do Automata</param>
    ''' <param name="transacao">Objeto com a transação</param>
    ''' <remarks></remarks>
    Public Sub CentrosProcesoBorrar(IdAutomata As Long, ByRef transacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.AutomataCentrosProcesoBorrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAutomata", ProsegurDbType.Inteiro_Longo, IdAutomata))

        transacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Grava um cento de processo de acordo com o automata
    ''' </summary>
    ''' <param name="IdAutomata">Identificador do automata</param>
    ''' <param name="transacao">Transação</param>
    ''' <remarks></remarks>
    Public Sub AutomataCentroProcesoRegistrar(IdAutomata As Long, ByRef transacao As Transacao)
        ' Para cada centro de processo
        For Each cp As CentroProceso In Me
            ' Salva os dados do centro do processo associados ao automata
            cp.AutomataCentroProcesoRegistrar(IdAutomata, transacao)
        Next
    End Sub

#End Region

End Class