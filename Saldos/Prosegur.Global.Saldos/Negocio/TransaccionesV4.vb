Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Data

<Serializable()> _
Public Class TransaccionesV4

#Region "[VARIÁVEIS]"

    Private _FechaDesde As DateTime
    Private _FechaHasta As DateTime
    Private _IdsCentroProcesso As String
    Private _IdsFormularios As String
    Private _TodosFormularios As Boolean
    Private _IdsCanais As String
    Private _TodosCanais As Boolean
    Private _IdsClientes As String
    Private _TodosClientes As Boolean
    Private _IdsMoedas As String
    Private _IdsEspecie As String
    Private _DiscriminarEspecie As Boolean
    Private _IntegrarCentroProceso As Boolean
    Private _SoloSaldoDisponible As Boolean
    Private _Col As DataTable
    Private _Realizado As Boolean
    Private Shared _Versao As String

#End Region

#Region "[PROPRIEDADES]"

    'FechaDesde
    Public Property FechaDesde() As DateTime
        Get
            FechaDesde = _FechaDesde
        End Get
        Set(Value As DateTime)
            _FechaDesde = Value
        End Set
    End Property

    'FechaHasta
    Public Property FechaHasta() As DateTime
        Get
            FechaHasta = _FechaHasta
        End Get
        Set(Value As DateTime)
            _FechaHasta = Value
        End Set
    End Property

    'IdsCentroProcesso
    Public Property IdsCentroProcesso() As String
        Get
            IdsCentroProcesso = _IdsCentroProcesso
        End Get
        Set(Value As String)
            _IdsCentroProcesso = Value
        End Set
    End Property

    'IdsFormularios
    Public Property IdsFormularios() As String
        Get
            IdsFormularios = _IdsFormularios
        End Get
        Set(Value As String)
            _IdsFormularios = Value
        End Set
    End Property

    'TodosFormularios
    Public Property TodosFormularios() As Boolean
        Get
            TodosFormularios = _TodosFormularios
        End Get
        Set(Value As Boolean)
            _TodosFormularios = Value
        End Set
    End Property

    'IdsCanais
    Public Property IdsCanais() As String
        Get
            IdsCanais = _IdsCanais
        End Get
        Set(Value As String)
            _IdsCanais = Value
        End Set
    End Property

    '_TodosCanais
    Public Property TodosCanais() As Boolean
        Get
            TodosCanais = _TodosCanais
        End Get
        Set(Value As Boolean)
            _TodosCanais = Value
        End Set
    End Property

    '_IdsClientes
    Public Property IdsClientes() As String
        Get
            IdsClientes = _IdsClientes
        End Get
        Set(Value As String)
            _IdsClientes = Value
        End Set
    End Property

    '_TodosClientes 
    Public Property TodosClientes() As Boolean
        Get
            TodosClientes = _TodosClientes
        End Get
        Set(Value As Boolean)
            _TodosClientes = Value
        End Set
    End Property

    '_IdsMoedas 
    Public Property IdsMoedas() As String
        Get
            IdsMoedas = _IdsMoedas
        End Get
        Set(Value As String)
            _IdsMoedas = Value
        End Set
    End Property

    '_IdsEspecie 
    Public Property IdsEspecie() As String
        Get
            IdsEspecie = _IdsEspecie
        End Get
        Set(Value As String)
            _IdsEspecie = Value
        End Set
    End Property

    '_DiscriminarEspecie 
    Public Property DiscriminarEspecie() As Boolean
        Get
            DiscriminarEspecie = _DiscriminarEspecie
        End Get
        Set(Value As Boolean)
            _DiscriminarEspecie = Value
        End Set
    End Property

    '_IntegrarCentroProceso 
    Public Property IntegrarCentroProceso() As Boolean
        Get
            IntegrarCentroProceso = _IntegrarCentroProceso
        End Get
        Set(Value As Boolean)
            _IntegrarCentroProceso = Value
        End Set
    End Property

    '_SoloSaldoDisponible 
    Public Property SoloSaldoDisponible() As Boolean
        Get
            SoloSaldoDisponible = _SoloSaldoDisponible
        End Get
        Set(Value As Boolean)
            _SoloSaldoDisponible = Value
        End Set
    End Property

    'RS
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
        comando.CommandText = "TransaccionesV4_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FD", ProsegurDbType.Data_Hora, Me.FechaDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FH", ProsegurDbType.Data_Hora, Me.FechaHasta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Observacao_Longa, Me.IdsCentroProcesso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdFormulario", ProsegurDbType.Observacao_Longa, Me.IdsFormularios))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdBanco", ProsegurDbType.Observacao_Longa, Me.IdsCanais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCliente", ProsegurDbType.Observacao_Longa, Me.IdsClientes))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdMoneda", ProsegurDbType.Observacao_Longa, Me.IdsMoedas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdEspecie", ProsegurDbType.Observacao_Longa, Me.IdsEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_DiscriminarEspecies", ProsegurDbType.Logico, Me.DiscriminarEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IntegrarCentrosProceso", ProsegurDbType.Logico, Me.IntegrarCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosBancos", ProsegurDbType.Logico, Me.TodosCanais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosClientes", ProsegurDbType.Logico, Me.TodosClientes))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_SoloSaldoDisponible", ProsegurDbType.Logico, Me.SoloSaldoDisponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosFormulario", ProsegurDbType.Logico, Me.TodosFormularios))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        ' executar comando
        Me.RS = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Sub

#End Region

End Class
