Imports Prosegur.DbHelper

<Serializable()> _
Public Class Moneda

#Region "[CONSTANTES]"

    Public Const C_ISOSALDOS As String = "ISOSALDOS"
    Public Const C_ISOGENESIS As String = "ISOGENESIS"
    Public Const C_IDMONEDA As String = "IDMONEDA"
    Public Const C_IDPS As String = "IDPS"
    Public Const T_PD_MONEDA As String = "PD_MONEDA"


#End Region

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _Simbolo As String
    Private _Visible As Boolean
    Private _IsoGenesis As String
    Private _IdGenesis As String
    Private _IsoSaldos As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Visible() As Boolean
        Get
            Visible = _Visible
        End Get
        Set(Value As Boolean)
            _Visible = Value
        End Set
    End Property

    Public Property Simbolo() As String
        Get
            Simbolo = _Simbolo
        End Get
        Set(Value As String)
            _Simbolo = Value
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

    Public Property IsoGenesis() As String
        Get
            Return _IsoGenesis
        End Get
        Set(value As String)
            _IsoGenesis = value
        End Set
    End Property

    Public Property IdGenesis() As String
        Get
            Return _IdGenesis
        End Get
        Set(value As String)
            _IdGenesis = value
        End Set
    End Property

    Public Property IsoSaldos() As String
        Get
            Return _IsoSaldos
        End Get
        Set(value As String)
            _IsoSaldos = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Registrar()

        If Me.Id = 0 Then
            Me.Id = Moneda.ObterIdMoneda
            InserirMoneda()
        Else
            AlterarMoneda()
        End If

    End Sub

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.MonedaRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.Simbolo = dt.Rows(0)("Simbolo")
            Me.Visible = dt.Rows(0)("Visible")

            If dt.Rows(0)("IsoGenesis") IsNot DBNull.Value Then
                Me.IsoGenesis = dt.Rows(0)("IsoGenesis")
            Else
                Me.IsoGenesis = String.Empty
            End If

            If dt.Rows(0)("IdGenesis") IsNot DBNull.Value Then
                Me.IdGenesis = dt.Rows(0)("IdGenesis")
            Else
                Me.IdGenesis = String.Empty
            End If

            If dt.Rows(0)("IsoSaldos") IsNot DBNull.Value Then
                Me.IsoSaldos = dt.Rows(0)("IsoSaldos")
            Else
                Me.IsoSaldos = String.Empty
            End If

        End If

    End Sub

    Public Sub Eliminar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.MonedaEliminar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Id))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub InserirMoneda()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.MonedaRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Simbolo", ProsegurDbType.Identificador_Alfanumerico, Me.Simbolo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Visible", ProsegurDbType.Logico, Me.Visible))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub AlterarMoneda()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.MonedaRegistrarUpdate.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Inteiro_Longo, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Descripcion", ProsegurDbType.Descricao_Longa, Me.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Simbolo", ProsegurDbType.Identificador_Alfanumerico, Me.Simbolo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Visible", ProsegurDbType.Logico, Me.Visible))
        'alterando dados mapeo
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IsoGenesis", ProsegurDbType.Descricao_Curta, Me.IsoGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGenesis", ProsegurDbType.Descricao_Curta, Me.IdGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IsoSaldos", ProsegurDbType.Descricao_Curta, Me.IsoSaldos))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Public Shared Function ObterIdMoneda() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SMoneda.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    ''' <summary>
    ''' Recupera o Id da Moneda. Utilizado no web service de integracao com o conteo
    ''' </summary>
    ''' <param name="IsoGenesis">Iso Genesis da moeda</param>
    ''' <param name="IsoSaldos">Identificacao do tipo de Importe</param>
    ''' <returns>Id da moeda</returns>
    ''' <history>
    ''' [vinicius.gama] 28/07/09 Criado
    ''' </history>
    Public Shared Function MonedaGenesisRealizar(IsoGenesis As String, _
                                                 IsoSaldos As String, _
                                                 IdGenesis As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.MonedaGenesisRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IsoGenesis", ProsegurDbType.Identificador_Alfanumerico, IsoGenesis))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IsoSaldos", ProsegurDbType.Identificador_Alfanumerico, IsoSaldos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdGenesis", ProsegurDbType.Identificador_Alfanumerico, IdGenesis))

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Public Function MonedaDescripcion(IdMoneda As Integer, IdMonedaVar As String, Busqueda As Integer) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text

        If (Busqueda = 0) Then
            comando.CommandText = My.Resources.MonedaDescricaoID.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Alfanumerico, IdMoneda))
        Else
            comando.CommandText = My.Resources.MonedaDescricaoIDVar.ToString()
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Alfanumerico, IdMonedaVar))
        End If

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

    End Function

    ''' <summary>
    ''' Recupera o Id da Moneda e idps. 
    ''' </summary>
    ''' <param name="ListaCodIsoDivisa">Codigos Iso Divisa)</param>
    ''' <returns>DataTable</returns>
    ''' <history>
    ''' [abueno] 14/07/2010 Criado
    ''' </history>
    Public Shared Function ListarIdMonedaIdps(ListaCodIsoDivisa As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        'Adiciona a coleção de Divisas
        comando.CommandText = String.Format(My.Resources.ListarIdMonedaYIdps, ListaCodIsoDivisa)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, C_ISOSALDOS, ProsegurDbType.Identificador_Alfanumerico, "E"))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class