Imports Prosegur.DbHelper

<Serializable()> _
Public Class EstadoComprobante

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

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.EstadoComprobanteRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdEstadoComprobante", ProsegurDbType.Identificador_Numerico, Me.Id))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.Codigo = dt.Rows(0)("Codigo")

        End If

    End Sub

    Public Sub RealizarPorDocumento(idDocumento As Integer)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = My.Resources.EstadoComprobanteRealizarPorDocumento.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, idDocumento))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count Then

            Me.Descripcion = dt.Rows(0)("Descripcion")
            Me.Codigo = dt.Rows(0)("Codigo")
            Me.Id = dt.Rows(0)("Id")

        End If

    End Sub

#End Region

End Class