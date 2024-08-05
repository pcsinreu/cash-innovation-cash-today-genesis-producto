Imports Prosegur.DbHelper

<Serializable()> _
Public Class Especie


#Region "[CONSTANTES]"

    Public Const C_IDESPECIE As String = "IDESPECIE"
    Public Const C_IDGENESIS As String = "IDGENESIS"
    Public Const T_PD_ESPECIE As String = "PD_ESPECIE"

#End Region

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _Moneda As Moneda
    Private _Uniforme As Boolean
    Private _Orden As Int32
    Private _EnActaProceso As Boolean
    Private _IdSIGII As String
    Private _IdRBO As String
    Private _EsDefaultGenesis As Boolean
    Private _IdGenesis As String
    Private _Calidad As String
    Private _EsBillete As Boolean
    Private _EsMoneda As Boolean

#End Region

#Region "[PROPRIEDADES]"

    Public Property Calidad() As String
        Get
            Return _Calidad
        End Get
        Set(Value As String)
            _Calidad = Value
        End Set
    End Property

    Public Property IdRBO() As String
        Get
            Return _IdRBO
        End Get
        Set(Value As String)
            _IdRBO = Value
        End Set
    End Property

    Public Property IdSIGII() As String
        Get
            Return _IdSIGII
        End Get
        Set(Value As String)
            _IdSIGII = Value
        End Set
    End Property

    Public Property IdGenesis() As String
        Get
            Return _IdGenesis
        End Get
        Set(Value As String)
            _IdGenesis = Value
        End Set
    End Property

    Public Property EnActaProceso() As Boolean
        Get
            Return _EnActaProceso
        End Get
        Set(Value As Boolean)
            _EnActaProceso = Value
        End Set
    End Property

    Public Property Moneda() As Moneda
        Get
            If _Moneda Is Nothing Then
                _Moneda = New Moneda
            End If
            Return _Moneda
        End Get
        Set(Value As Moneda)
            _Moneda = Value
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

    Public Property Orden() As Int32
        Get
            Return _Orden
        End Get
        Set(Value As Int32)
            _Orden = Value
        End Set
    End Property

    Public Property Uniforme() As Boolean
        Get
            Return _Uniforme
        End Get
        Set(Value As Boolean)
            _Uniforme = Value
        End Set
    End Property

    Public Property EsDefaultGenesis() As Boolean
        Get
            Return _EsDefaultGenesis
        End Get
        Set(Value As Boolean)
            _EsDefaultGenesis = Value
        End Set
    End Property

    Public Property EsBillete() As Boolean
        Get
            Return _EsBillete
        End Get
        Set(Value As Boolean)
            _EsBillete = Value
        End Set
    End Property

    Public Property EsMoneda() As Boolean
        Get
            Return _EsMoneda
        End Get
        Set(Value As Boolean)
            _EsMoneda = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Registrar()

        If Me.Id = 0 Then
            Me.Id = Especie.ObterIdEspecie()
            EspecieRegistrarInsert()
        Else
            EspecieRegistrarUpdate()
        End If

    End Sub

    Public Sub EspecieRegistrarInsert()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspecieRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEspecie", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Calidad", ProsegurDbType.Descricao_Longa, Me.Calidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Moneda.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Uniforme", ProsegurDbType.Logico, Me.Uniforme))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Orden", ProsegurDbType.Inteiro_Longo, Me.Orden))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EnActaProceso", ProsegurDbType.Logico, Me.EnActaProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdRBO", ProsegurDbType.Descricao_Longa, Me.IdRBO))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdSIGII", ProsegurDbType.Descricao_Longa, Me.IdSIGII))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsBillete", ProsegurDbType.Logico, Me.EsBillete))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsMoneda", ProsegurDbType.Logico, Me.EsMoneda))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub EspecieRegistrarUpdate()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspecieRegistrarUpdate.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEspecie", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Calidad", ProsegurDbType.Descricao_Longa, Me.Calidad))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Uniforme", ProsegurDbType.Logico, Me.Uniforme))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Orden", ProsegurDbType.Inteiro_Longo, Me.Orden))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EnActaProceso", ProsegurDbType.Logico, Me.EnActaProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdRBO", ProsegurDbType.Descricao_Longa, Me.IdRBO))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdSIGII", ProsegurDbType.Descricao_Longa, Me.IdSIGII))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsDefaultGenesis", ProsegurDbType.Logico, Me.EsDefaultGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGenesis", ProsegurDbType.Descricao_Longa, Me.IdGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsBillete", ProsegurDbType.Logico, Me.EsBillete))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "EsMoneda", ProsegurDbType.Logico, Me.EsMoneda))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Sub Eliminar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspecieEliminar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEspecie", ProsegurDbType.Inteiro_Longo, Me.Id))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Function ObterIdEspecie() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SEspecie.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    ''' <summary>
    ''' Obtém a especie pelo código ou pelo idgenesis (Codigo Denominação)
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/11/2009 Alterado - Adicionado filtro pelo IdGenesis
    ''' </history>
    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.EspecieRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEspecie", ProsegurDbType.Inteiro_Longo, Me.Id))

        ' o filtro poderá ser efetuado pelo idgenesis (Codigo Denominação)
        If Me.IdGenesis <> String.Empty Then
            comando.CommandText &= " or idgenesis = :idgenesis"
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "idgenesis", ProsegurDbType.Descricao_Curta, Me.IdGenesis))
        End If

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")

            If dt.Rows(0)("IdEspecie") IsNot DBNull.Value Then
                Me.Id = Convert.ToInt32(dt.Rows(0)("IdEspecie"))
            Else
                Me.Id = 0
            End If

            If dt.Rows(0)("IdMoneda") IsNot DBNull.Value Then
                Me.Moneda.Id = Convert.ToInt32(dt.Rows(0)("IdMoneda"))
            Else
                Me.Moneda.Id = 0
            End If

            Me.Uniforme = dt.Rows(0)("Uniforme")

            If dt.Rows(0)("Orden") IsNot DBNull.Value Then
                Me.Orden = Convert.ToInt32(dt.Rows(0)("Orden"))
            Else
                Me.Orden = 0
            End If

            If dt.Rows(0)("EnActaProceso") IsNot DBNull.Value Then
                Me.EnActaProceso = Convert.ToBoolean(dt.Rows(0)("EnActaProceso"))
            Else
                Me.EnActaProceso = False
            End If

            If dt.Rows(0)("IdRBO") IsNot DBNull.Value Then
                Me.IdRBO = dt.Rows(0)("IdRBO")
            Else
                Me.IdRBO = String.Empty
            End If

            If dt.Rows(0)("IdSIGII") IsNot DBNull.Value Then
                Me.IdSIGII = dt.Rows(0)("IdSIGII")
            Else
                Me.IdSIGII = String.Empty
            End If

            If dt.Rows(0)("Calidad") IsNot DBNull.Value Then
                Me.Calidad = dt.Rows(0)("Calidad")
            Else
                Me.Calidad = String.Empty
            End If

            If dt.Rows(0)("EsDefaultGenesis") IsNot DBNull.Value Then
                Me.EsDefaultGenesis = Convert.ToBoolean(dt.Rows(0)("EsDefaultGenesis"))
            Else
                Me.EsDefaultGenesis = False
            End If

            If dt.Rows(0)("IdGenesis") IsNot DBNull.Value Then
                Me.IdGenesis = dt.Rows(0)("IdGenesis").ToString()
            Else
                Me.IdGenesis = String.Empty
            End If

            If dt.Rows(0)("Bol_Billete") IsNot DBNull.Value Then
                Me.EsBillete = Convert.ToBoolean(dt.Rows(0)("Bol_Billete"))
            Else
                Me.EsBillete = False
            End If

            If dt.Rows(0)("Bol_Moneda") IsNot DBNull.Value Then
                Me.EsMoneda = Convert.ToBoolean(dt.Rows(0)("Bol_Moneda"))
            Else
                Me.EsMoneda = False
            End If

        End If

    End Sub

    ''' <summary>
    ''' Recupera o Id da Especie Default de uma Moneda
    ''' </summary>
    ''' <param name="IdMoneda">Id da Moneda</param>
    ''' <returns>Id da Especie Default</returns>
    Public Shared Function EspecieMonedaDefaultRealizar(IdMoneda As Integer) As Integer

        Dim IdEspecie As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspecieMonedaDefaultRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, IdMoneda))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            IdEspecie = dt(0)("IdEspecie")

        End If

        Return IdEspecie

    End Function

    ''' <summary>
    ''' Recupera o Id da Especie através do código da denominação que vem do conteo.
    ''' </summary>
    ''' <param name="IdMoneda">Id da Moneda</param>
    ''' <param name="IdDenominacion">Id da denominação no conteo</param>
    ''' <returns>Id da Especie</returns>
    Public Shared Function EspeciePorDenominacionGenesis(IdMoneda As Integer, _
                                                         IdDenominacion As String) As Integer

        Dim IdEspecie As Integer = 0
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.EspecieObtenerPorDenominacion.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, IdMoneda))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGenesis", ProsegurDbType.Descricao_Curta, IdDenominacion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            IdEspecie = dt(0)("IdEspecie")
        End If

        Return IdEspecie

    End Function

    ''' <summary>
    ''' Recupera o Id Especie e IDGenesis
    ''' </summary>
    ''' <param name="ListaCodDenominacion">Codigos Denominaciones)</param>
    ''' <returns>DataTable</returns>
    ''' <history>
    ''' [abueno] 14/07/2010 Criado
    ''' </history>
    Public Shared Function ListarIdEspecieIdGenesis(ListaCodDenominacion As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        'Adiciona a coleção de Divisas
        comando.CommandText = String.Format(My.Resources.ListarIdEspecieYIdGenesis, ListaCodDenominacion)
        comando.CommandType = CommandType.Text

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class