Imports System.Text
Imports Prosegur.DbHelper
Imports System.Collections.Generic
Imports System.Data
Imports System.Configuration.ConfigurationManager

<Serializable()> _
Public Class TransaccionesV5

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
    Private _Cabecera As String
    Private _SaldoAtual As Decimal
    Private _Col As DataTable
    Private _Realizado As Boolean
    Private _NumComprobante As String
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

    'Cabecera
    Public Property Cabecera() As String
        Get
            Cabecera = _Cabecera
        End Get
        Set(Value As String)
            _Cabecera = Value
        End Set
    End Property

    'Saldo
    Public Property SaldoAtual() As Decimal
        Get
            SaldoAtual = _SaldoAtual
        End Get
        Set(Value As Decimal)
            _SaldoAtual = Value
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

    'NumComprobante
    Public Property NumComprobante() As String
        Get
            Return _NumComprobante
        End Get
        Set(Value As String)
            _NumComprobante = Value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    Public Sub TransaccionesListarConSaldoV5()

        'VARIAVEIS DE CONTROLE
        Dim IdGrupo As Integer = 0
        Dim ApelidoNombre As String
        Dim Saldo As Decimal = 0
        Dim dtAux As DataTable = Nothing

        dtAux = SeleccionarIdGrupo(Me.FechaDesde, Me.IdsCentroProcesso)

        If dtAux IsNot Nothing AndAlso dtAux.Rows.Count > 0 Then

            For Each dr As DataRow In dtAux.Rows

                If dr(0) IsNot Nothing Then

                    Select Case Convert.ToInt32(dr(0))

                        Case 0

                            'Me.Cabecera = " LOS CENTROS DE PROCESO ELEGIDOS NO SON TESORO Y NO CONTINEN CIERRE DE TESORO"
                            Me.FechaDesde = Me.FechaDesde.AddMinutes(-30)
                            Me.RS = TransaccionesListarV5()
                            Exit Sub

                        Case 1

                            'Me.Cabecera = " NO SE ENCONTRO CIERRE DE TESORO PARA LA FECHA SELECCIONADA"
                            Me.SaldoAtual = 0
                            Me.RS = New DataTable
                            Exit Sub

                        Case Else

                            IdGrupo = dr("IdGrupo")
                            Me.NumComprobante = dr("NumComprobante")
                            ApelidoNombre = dr("ApellidoNombre")

                            If dr("FechaResuelve") IsNot Nothing Then
                                Me.FechaDesde = Convert.ToDateTime(dr("FechaResuelve"))
                            Else
                                Me.FechaDesde = DateTime.MinValue
                            End If

                            Dim objSaldos As New Saldos
                            Dim dtSaldos As DataTable = objSaldos.SaldosAnterior(IdGrupo, Me.IdsCanais, Me.IdsClientes, Me.IdsMoedas)

                            If dtSaldos IsNot Nothing AndAlso dtSaldos.Rows.Count > 0 Then

                                For Each drs As DataRow In dtSaldos.Rows

                                    Select Case objSaldos.Retorno

                                        Case 0

                                            'Me.Cabecera = "NO EXISTE SALDO PARA EL CLIENTE/CANAL EN EL CIERRE DE TESORO SOLICITADO SALDO = 0"
                                            Me.RS = TransaccionesListarV5()
                                            Me.SaldoAtual = 0
                                            Exit Sub

                                        Case 1

                                            'Me.Cabecera = "EL CLIENTE/CANAL NO CONTIENE SALDO PARA LA MONEDA SOLICITADA"
                                            Me.RS = TransaccionesListarV5()
                                            Me.SaldoAtual = 0
                                            Exit Sub

                                        Case Else

                                            Me.SaldoAtual = drs("Importe")
                                            'Me.Cabecera = String.Empty
                                            Me.RS = TransaccionesListarV5()
                                            Exit Sub

                                    End Select

                                Next

                            End If

                    End Select

                End If

            Next

        End If

    End Sub

    Public Function SeleccionarIdGrupo(FD As DateTime, _
                                       ListaIdCentroProceso As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = "SeleccionarIdGrupo_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FD", ProsegurDbType.Data, FD))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Descricao_Longa, ListaIdCentroProceso))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Function

    Public Function TransaccionesListarV5() As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "TransaccionesV5_" & Prosegur.Genesis.Comon.Util.Version

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FDN", ProsegurDbType.Data_Hora, Me.FechaDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FHN", ProsegurDbType.Data_Hora, Me.FechaHasta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Descricao_Longa, Me.IdsCentroProcesso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdFormulario", ProsegurDbType.Descricao_Longa, Me.IdsFormularios))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdBanco", ProsegurDbType.Descricao_Longa, Me.IdsCanais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCliente", ProsegurDbType.Descricao_Longa, Me.IdsClientes))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdMoneda", ProsegurDbType.Descricao_Longa, Me.IdsMoedas))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdEspecie", ProsegurDbType.Descricao_Longa, Me.IdsEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_DiscriminarEspecies", ProsegurDbType.Numero_Decimal, Me.DiscriminarEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IntegrarCentrosProceso", ProsegurDbType.Numero_Decimal, Me.IntegrarCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosBancos", ProsegurDbType.Numero_Decimal, Me.TodosCanais))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosClientes", ProsegurDbType.Numero_Decimal, Me.TodosClientes))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_SoloSaldoDisponible", ProsegurDbType.Numero_Decimal, Me.SoloSaldoDisponible))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosFormulario", ProsegurDbType.Numero_Decimal, Me.TodosFormularios))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

    End Function

#End Region

End Class