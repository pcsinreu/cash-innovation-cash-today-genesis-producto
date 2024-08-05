Imports Prosegur.DbHelper

<Serializable()> _
Public Class CampoExtra

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Nombre As String
    Private _Valor As String
    Private _TipoCampoExtra As TipoCampoExtra
    Private _SeValida As Boolean

#End Region

#Region "PROPRIEDADES"

    Public Property SeValida() As Boolean
        Get
            Return _SeValida
        End Get
        Set(Value As Boolean)
            _SeValida = Value
        End Set
    End Property

    Public Property TipoCampoExtra() As TipoCampoExtra
        Get
            If _TipoCampoExtra Is Nothing Then
                _TipoCampoExtra = New TipoCampoExtra()
            End If
            Return _TipoCampoExtra
        End Get
        Set(Value As TipoCampoExtra)
            _TipoCampoExtra = Value
        End Set
    End Property

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(Value As String)
            _Nombre = Value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return _Valor
        End Get
        Set(Value As String)
            _Valor = Value
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

    Public Sub Registrar()

        If Me.Id = 0 Then
            Me.Id = ObterIdCampoExtra()
            InserirCampoExtra()
        Else
            AlterarCampoExtra()
        End If

    End Sub

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CampoExtraRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Nombre = dt.Rows(0)("Nombre")
            Me.TipoCampoExtra.Id = dt.Rows(0)("IdTipoCampoExtra")
            Me.TipoCampoExtra.Realizar()
            Me.SeValida = dt.Rows(0)("SeValida")

        End If

    End Sub

    Public Sub Eliminar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CampoExtraEliminar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Inteiro_Longo, Me.Id))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub InserirCampoExtra()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CampoExtraRegistrarInsert.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Nombre", ProsegurDbType.Descricao_Longa, Me.Nombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCampoExtra", ProsegurDbType.Identificador_Numerico, Me.TipoCampoExtra.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeValida", ProsegurDbType.Logico, Me.SeValida))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Sub AlterarCampoExtra()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.CampoExtraRegistrarUpdate.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Nombre", ProsegurDbType.Descricao_Longa, Me.Nombre))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCampoExtra", ProsegurDbType.Identificador_Numerico, Me.TipoCampoExtra.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "SeValida", ProsegurDbType.Logico, Me.SeValida))

        AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

    End Sub

    Private Shared Function ObterIdCampoExtra() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SCampoExtra.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Public Sub CampoExtraValorRegistrar(idDocumento As Integer, ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.CampoExtraValorRegistrar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, idDocumento))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Valor", ProsegurDbType.Descricao_Longa, Me.Valor))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Function CampoExtraValorValido() As Integer

        ' Comentado por Thiago Castro - Argentina informou que era para desconsiderar a tabela PD_CampoExtraValorValido

        'Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        'comando.CommandType = CommandType.Text
        'comando.CommandText = My.Resources.CampoExtraValorValido.ToString()

        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.Id))
        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Valor", ProsegurDbType.Descricao_Longa, Me.Valor))

        'Dim objRetorno As String = AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)

        'If objRetorno <> String.Empty Then
        '    Return Convert.ToInt32(objRetorno)
        'End If

        Return 0

    End Function

    Public Function SeValidaCampoExtra() As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.SeValidaCampoExtra.ToString()

        ' parametros de verificação
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCampoExtra", ProsegurDbType.Identificador_Numerico, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt
        End If

        Return Nothing

    End Function

#End Region

End Class