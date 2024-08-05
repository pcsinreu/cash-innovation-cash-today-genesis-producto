Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.Generic
Imports System.Data.OracleClient

Public Class Saldos
    Inherits List(Of Saldo)

#Region "[VARIÁVEIS]"

    Private _ListaIdCentroProceso As String
    Private _ListaIdBanco As String
    Private _ListaIdCliente As String
    Private _ListaIdMoneda As String
    Private _ListaIdEspecie As String
    Private _DiscriminarEspecies As Boolean
    Private _Actual As Boolean
    Private _Fecha As Date
    Private _IntegrarCentrosProceso As Boolean
    Private _SumasSobreCPs As Saldos
    Private _TodosBancos As Boolean
    Private _TodosClientes As Boolean
    Private _SoloSaldoDisponible As Boolean
    Private _ConSaldosCero As Boolean
    Private _Monedas As Monedas
    Private _Retorno As String
    Private Shared _Versao As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property Monedas() As Monedas
        Get
            If _Monedas Is Nothing Then
                _Monedas = New Monedas()
            End If
            Return _Monedas
        End Get
        Set(Value As Monedas)
            _Monedas = Value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return _Fecha
        End Get
        Set(Value As Date)
            _Fecha = Value
        End Set
    End Property

    Public Property Actual() As Boolean
        Get
            Return _Actual
        End Get
        Set(Value As Boolean)
            _Actual = Value
        End Set
    End Property

    Public Property DiscriminarEspecies() As Boolean
        Get
            Return _DiscriminarEspecies
        End Get
        Set(Value As Boolean)
            _DiscriminarEspecies = Value
        End Set
    End Property

    Public Property ListaIdEspecie() As String
        Get
            Return _ListaIdEspecie
        End Get
        Set(Value As String)
            _ListaIdEspecie = Value
        End Set
    End Property

    Public Property ListaIdMoneda() As String
        Get
            Return _ListaIdMoneda
        End Get
        Set(Value As String)
            _ListaIdMoneda = Value
        End Set
    End Property

    Public Property ListaIdCliente() As String
        Get
            Return _ListaIdCliente
        End Get
        Set(Value As String)
            _ListaIdCliente = Value
        End Set
    End Property

    Public Property ListaIdBanco() As String
        Get
            Return _ListaIdBanco
        End Get
        Set(Value As String)
            _ListaIdBanco = Value
        End Set
    End Property

    Public Property ListaIdCentroProceso() As String
        Get
            Return _ListaIdCentroProceso
        End Get
        Set(Value As String)
            _ListaIdCentroProceso = Value
        End Set
    End Property

    Public Property IntegrarCentrosProceso() As Boolean
        Get
            Return _IntegrarCentrosProceso
        End Get
        Set(Value As Boolean)
            _IntegrarCentrosProceso = Value
        End Set
    End Property

    Public Property SoloSaldoDisponible() As Boolean
        Get
            Return _SoloSaldoDisponible
        End Get
        Set(Value As Boolean)
            _SoloSaldoDisponible = Value
        End Set
    End Property

    Public Property ConSaldosCero() As Boolean
        Get
            Return _ConSaldosCero
        End Get
        Set(Value As Boolean)
            _ConSaldosCero = Value
        End Set
    End Property

    Public Property TodosBancos() As Boolean
        Get
            Return _TodosBancos
        End Get
        Set(Value As Boolean)
            _TodosBancos = Value
        End Set
    End Property

    Public Property TodosClientes() As Boolean
        Get
            Return _TodosClientes
        End Get
        Set(Value As Boolean)
            _TodosClientes = Value
        End Set
    End Property

    Public ReadOnly Property SumasSobreCPs() As Saldos
        Get
            If _SumasSobreCPs Is Nothing Then
                _SumasSobreCPs = New Saldos
            End If
            Return _SumasSobreCPs
        End Get
    End Property

    Public Property Retorno() As String
        Get
            Return _Retorno
        End Get
        Set(Value As String)
            _Retorno = Value
        End Set
    End Property


#End Region

#Region "[MÉTODOS]"

    Public Sub Realizar(Optional ByRef RelatorioSaldos As Boolean = False, Optional ByRef esMiProsegur As Boolean = False)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = "PD_SALDOSREALIZAR_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Observacao_Longa, Me.ListaIdCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdBanco", ProsegurDbType.Observacao_Longa, Me.ListaIdBanco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCliente", ProsegurDbType.Observacao_Longa, Me.ListaIdCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdMoneda", ProsegurDbType.Observacao_Longa, If(RelatorioSaldos AndAlso Me.DiscriminarEspecies, String.Empty, Me.ListaIdMoneda)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdEspecie", ProsegurDbType.Observacao_Longa, Me.ListaIdEspecie))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_DiscriminarEspecies", ProsegurDbType.Logico, Me.DiscriminarEspecies))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_Actual", ProsegurDbType.Logico, Me.Actual))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaValor", ProsegurDbType.Data_Hora, Me.Fecha))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IntegrarCentrosProceso", ProsegurDbType.Logico, Me.IntegrarCentrosProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosBancos", ProsegurDbType.Logico, Me.TodosBancos))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TodosClientes", ProsegurDbType.Logico, Me.TodosClientes))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_SoloSaldoDisponible", ProsegurDbType.Logico, Me.SoloSaldoDisponible))
        If esMiProsegur Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_TrabajarFecha", ProsegurDbType.Observacao_Longa, "FechaGestion"))
        End If
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        ' Se existe moedas selecionadas
        If Me.ListaIdMoneda IsNot Nothing AndAlso Me.ListaIdMoneda.Count > 0 Then

            ' Recupera a lista de moedas
            Dim Moedas() As String = Me.ListaIdMoneda.Split("|")

            For i As Integer = 0 To Moedas.Count - 1
                If Not String.IsNullOrEmpty(Moedas(i)) Then
                    Dim objMoneda As New Moneda
                    objMoneda.Id = Moedas(i)
                    Me.Monedas.Add(objMoneda)
                End If
            Next
        End If

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' Limpa a lista
            Me.Clear()

            Dim objSaldo As Saldo = Nothing
            Dim IdSaldoBasico As Integer = 0
            Dim IdCentroProceso As Integer = 0
            Dim IdBanco As Integer = 0
            Dim IdCliente As Integer = 0
            Dim IdMoneda As Integer = 0
            Dim Disponible As Boolean
            Dim Importe As Decimal

            For Each dr As DataRow In dt.Rows

                If dr("Importe") <> 0 OrElse Me.ConSaldosCero Then

                    objSaldo = New Saldo

                    IdSaldoBasico = dr("IdSaldoBasico")
                    IdCentroProceso = dr("IdCentroProceso")
                    IdBanco = dr("IdBanco")
                    IdCliente = dr("IdCliente")
                    IdMoneda = Convert.ToInt32(dr("IdMoneda"))
                    Disponible = dr("Disponible")
                    Importe = dr("Importe")

                    Dim resultSaldo = From ESaldo As Saldo In Me _
                                      Where ESaldo.IdCentroProceso = IdCentroProceso _
                                      AndAlso ESaldo.IdBanco = IdBanco _
                                      AndAlso ESaldo.IdCliente = IdCliente _
                                      AndAlso ESaldo.IdMoneda = IdMoneda _
                                      AndAlso ESaldo.Disponible = Disponible

                    If resultSaldo IsNot Nothing AndAlso resultSaldo.Count > 0 Then
                        objSaldo = resultSaldo.First
                    Else

                        objSaldo.Id = IdSaldoBasico
                        objSaldo.IdCentroProceso = IdCentroProceso
                        objSaldo.IdBanco = IdBanco
                        objSaldo.IdCliente = IdCliente
                        objSaldo.IdMoneda = IdMoneda
                        objSaldo.Importe += Importe
                        objSaldo.Disponible = Disponible

                    End If

                    If Me.DiscriminarEspecies Then

                        Dim IdEspecie As Integer = dr("IdEspecie")
                        Dim resultFajo = From EFajo In objSaldo.Fajos _
                                         Where EFajo.IdEspecie = IdEspecie

                        If resultFajo IsNot Nothing AndAlso resultFajo.Count > 0 Then
                            resultFajo(0).Cantidad += dr("Cantidad")
                            resultFajo(0).Importe += dr("DetalleImporte")
                        Else

                            Dim objFajo As New Fajo
                            objFajo.IdEspecie = dr("IdEspecie")
                            objFajo.Cantidad = dr("Cantidad")
                            objFajo.Importe = dr("DetalleImporte")

                            objSaldo.Fajos.Add(objFajo)

                        End If

                    End If

                    If Not (resultSaldo IsNot Nothing AndAlso resultSaldo.Count > 0) Then
                        Me.Add(objSaldo)
                    End If

                End If

            Next

        End If

        If Me.IntegrarCentrosProceso Then IntegrarSobreCentrosProceso()

    End Sub

    Public Sub IntegrarSobreCentrosProceso()

        Dim IdBanco As Integer = -1
        Dim IdCliente As Integer = -1
        Dim IdEspecie As Integer = -1
        Dim IdMoneda As Integer = -1
        Dim Disponible As Boolean
        Dim objSaldo As Saldo = Nothing

        For Each OSaldo In Me

            objSaldo = New Saldo

            IdBanco = OSaldo.IdBanco
            IdCliente = OSaldo.IdCliente
            IdMoneda = OSaldo.IdMoneda
            Disponible = OSaldo.Disponible

            Dim resultSaldo = From ESaldo In Me.SumasSobreCPs _
                              Where ESaldo.IdBanco = IdBanco _
                              AndAlso ESaldo.IdCliente = IdCliente _
                              AndAlso ESaldo.IdMoneda = IdMoneda _
                              AndAlso ESaldo.Disponible = Disponible

            If resultSaldo IsNot Nothing AndAlso resultSaldo.Count > 0 Then

                objSaldo = resultSaldo(0)

            Else

                objSaldo.IdCentroProceso = -1
                objSaldo.IdBanco = IdBanco
                objSaldo.IdCliente = IdCliente
                objSaldo.IdMoneda = IdMoneda
                objSaldo.Importe = 0
                objSaldo.Disponible = Disponible

            End If

            objSaldo.Importe += OSaldo.Importe

            If Me.DiscriminarEspecies Then

                For Each OFajo In OSaldo.Fajos

                    IdEspecie = OFajo.IdEspecie

                    Dim resultFajo = From EFajo In objSaldo.Fajos _
                                     Where EFajo.IdEspecie = IdEspecie

                    If resultFajo IsNot Nothing AndAlso resultFajo.Count > 0 Then

                        resultFajo(0).Cantidad += OFajo.Cantidad
                        resultFajo(0).Importe += OFajo.Importe

                    Else

                        Dim objFajo As New Fajo
                        objFajo.IdEspecie = OFajo.IdEspecie
                        objFajo.Cantidad = OFajo.Cantidad
                        objFajo.Importe = OFajo.Importe

                        objSaldo.Fajos.Add(objFajo)

                    End If

                Next

            End If

            If Not (resultSaldo IsNot Nothing AndAlso resultSaldo.Count > 0) Then
                Me.SumasSobreCPs.Add(objSaldo)
            End If

        Next

    End Sub

    Public Function SaldosAnterior(IdGrupo As String, IdBanco As String, IdCliente As String, IdMoneda As String) As DataTable

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        Dim dt As DataTable = Nothing

        comando.CommandText = "SaldoAnterior_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        'Parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdGrupo", ProsegurDbType.Inteiro_Longo, IdGrupo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdBanco", ProsegurDbType.Descricao_Longa, IdBanco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdCliente", ProsegurDbType.Descricao_Longa, IdCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdMoneda", ProsegurDbType.Descricao_Longa, IdMoneda))

        'Saida
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))
        comando.Parameters.Add(Util.CriarParametroOracle("v_Retorno", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.LongVarChar, 3))

        dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        Me.Retorno = CType(comando.Parameters("v_Retorno"), OracleParameter).Value
        Return dt

    End Function

#End Region

End Class