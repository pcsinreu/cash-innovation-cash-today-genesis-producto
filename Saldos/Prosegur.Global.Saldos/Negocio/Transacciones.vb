Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Data

<Serializable()> _
Public Class Transacciones
    Inherits List(Of Transaccion)

#Region "[VARIÁVEIS]"

    Private _ListaIdCentroProceso As String
    Private _ListaIdBanco As String
    Private _ListaIdCliente As String
    Private _ListaIdMoneda As String
    Private _ListaIdEspecie As String
    Private _DiscriminarEspecies As Boolean
    Private _FechaDesde As Date
    Private _Realizado As Boolean
    Private _Col As DataTable
    Private Shared _Versao As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property FechaDesde() As Date
        Get
            FechaDesde = _FechaDesde
        End Get
        Set(Value As Date)
            _FechaDesde = Value
        End Set
    End Property

    Public Property DiscriminarEspecies() As Boolean
        Get
            DiscriminarEspecies = _DiscriminarEspecies
        End Get
        Set(Value As Boolean)
            _DiscriminarEspecies = Value
        End Set
    End Property

    Public Property ListaIdEspecie() As String
        Get
            ListaIdEspecie = _ListaIdEspecie
        End Get
        Set(Value As String)
            _ListaIdEspecie = Value
        End Set
    End Property

    Public Property ListaIdMoneda() As String
        Get
            ListaIdMoneda = _ListaIdMoneda
        End Get
        Set(Value As String)
            _ListaIdMoneda = Value
        End Set
    End Property

    Public Property ListaIdCliente() As String
        Get
            ListaIdCliente = _ListaIdCliente
        End Get
        Set(Value As String)
            _ListaIdCliente = Value
        End Set
    End Property

    Public Property ListaIdBanco() As String
        Get
            ListaIdBanco = _ListaIdBanco
        End Get
        Set(Value As String)
            _ListaIdBanco = Value
        End Set
    End Property

    Public Property ListaIdCentroProceso() As String
        Get
            ListaIdCentroProceso = _ListaIdCentroProceso
        End Get
        Set(Value As String)
            _ListaIdCentroProceso = Value
        End Set
    End Property

    Public Property RS() As DataTable
        Get
            If _Col Is Nothing Then
                _Col = New DataTable
            End If
            Return _Col
        End Get
        Set(Value As DataTable)
            _Col = Value
            SeaRealizado()
        End Set
    End Property

    Private Sub SeaRealizado()
        If Not _Col Is Nothing Then
            _Realizado = True
        Else
            _Realizado = False
        End If
    End Sub

#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = "PD_TransaccionesReal_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Observacao_Longa, Me.ListaIdCentroProceso, 4000))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdBanco", ProsegurDbType.Observacao_Longa, Me.ListaIdBanco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCliente", ProsegurDbType.Observacao_Longa, Me.ListaIdCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdMoneda", ProsegurDbType.Observacao_Longa, Me.ListaIdMoneda))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdEspecie", ProsegurDbType.Observacao_Longa, Me.ListaIdEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_DiscriminarEspecies", ProsegurDbType.Logico, Me.DiscriminarEspecies))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        ' executar comando
        Me.RS = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Sub

#End Region

End Class