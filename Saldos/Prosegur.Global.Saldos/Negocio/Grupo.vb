Imports Prosegur.DbHelper
Imports Prosegur
Imports System.Data.SqlClient

<Serializable()> _
Public Class Grupo

#Region "[VARIÁVEIS]"

    Private _Id As Integer
    Private _Codigo As String
    Private _Descripcion As String
    Private _Vigente As Boolean
    Private _Formularios As Dictionary(Of String, Boolean)

#End Region

#Region "[PROPRIEDADES]"

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(Value As Integer)
            _Id = Value
        End Set
    End Property

    Public Property Codigo() As String
        Get
            Return _Codigo
        End Get
        Set(value As String)
            _Codigo = value
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

    Public Property Vigente() As Boolean
        Get
            Return _Vigente
        End Get
        Set(Value As Boolean)
            _Vigente = Value
        End Set
    End Property

    Public Property Formularios() As Dictionary(Of String, Boolean)
        Get
            Return _Formularios
        End Get
        Set(Value As Dictionary(Of String, Boolean))
            _Formularios = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.GrupoRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "COD_GRUPO", ProsegurDbType.Descricao_Curta, Me.Codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Me.Id = Util.AtribuirValorObj(dt.Rows(0)("ID_GRUPO"), GetType(Int32))
            Me.Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_GRUPO"), GetType(String))
            Me.Vigente = Util.AtribuirValorObj(dt.Rows(0)("BOL_GRUPO_VIGENTE"), GetType(Boolean))
            Me.Formularios = New Dictionary(Of String, Boolean)

            For Each item As DataRow In dt.Rows
                Me.Formularios.Add(Util.AtribuirValorObj(item("ID_FOMULARIO"), GetType(String)), Util.AtribuirValorObj(item("FORMULARIO_VALIDO"), GetType(Boolean)))
            Next

        End If

    End Sub

#End Region

End Class