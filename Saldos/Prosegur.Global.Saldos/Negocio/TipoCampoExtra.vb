Imports Prosegur.DbHelper

<Serializable()> _
Public Class TipoCampoExtra

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Descripcion As String
    Private _Codigo As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Codigo() As String
        Get
            Codigo = _Codigo
        End Get
        Set(Value As String)
            _Codigo = Value
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

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.TipoCampoExtraRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdTipoCampoExtra", ProsegurDbType.Inteiro_Longo, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.Codigo = dt.Rows(0)("Codigo")

        End If

    End Sub

#End Region

End Class