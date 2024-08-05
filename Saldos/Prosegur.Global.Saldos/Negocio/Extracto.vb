Imports Prosegur.DbHelper

<Serializable()> _
Public Class Extracto

#Region "[VARIAVEIS]"

    Private _ListaIdCentroProceso As String
    Private _ListaIdBanco As String
    Private _ListaIdCliente As String
    Private _ListaIdMoneda As String
    Private _FechaDesde As Date
    Private _FechaHasta As Date
    Private _Transacciones As Transacciones
    Private _TransaccionesDeArrastre As Transacciones
    Private _SaldoInicial As Saldos
    Private _SaldoFinal As Saldos
    Private _Correlativo As Boolean
    Private _Planta As Planta
    Private _Banco As Banco
    Private _Cliente As Cliente
    Private _Documento As Documento
    Private _Id As Integer
    Private _Moneda As Moneda
    Private _Anterior As Extracto
    Private _Documentos As Documentos
    Private _VistaCliente As Boolean
    Private Shared _Versao As String

#End Region

#Region "[PROPRIEDADES]"

    Public Property VistaCliente() As Boolean
        Get
            VistaCliente = _VistaCliente
        End Get
        Set(Value As Boolean)
            _VistaCliente = Value
        End Set
    End Property

    Public Property Anterior() As Extracto
        Get
            If _Anterior Is Nothing Then
                _Anterior = New Extracto()
            End If
            Return _Anterior
        End Get
        Set(Value As Extracto)
            _Anterior = Value
        End Set
    End Property

    Public Property Moneda() As Moneda
        Get
            If _Moneda Is Nothing Then
                _Moneda = New Moneda()
            End If
            Return _Moneda
        End Get
        Set(Value As Moneda)
            _Moneda = Value
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

    Public Property Documento() As Documento
        Get
            If _Documento Is Nothing Then
                _Documento = New Documento()
            End If
            Return _Documento
        End Get
        Set(Value As Documento)
            _Documento = Value
        End Set
    End Property

    Public Property Cliente() As Cliente
        Get
            If _Cliente Is Nothing Then
                _Cliente = New Cliente()
            End If
            Return _Cliente
        End Get
        Set(Value As Cliente)
            _Cliente = Value
        End Set
    End Property

    Public Property Banco() As Banco
        Get
            If _Banco Is Nothing Then
                _Banco = New Banco()
            End If
            Return _Banco
        End Get
        Set(Value As Banco)
            _Banco = Value
        End Set
    End Property

    Public Property Planta() As Planta
        Get
            If _Planta Is Nothing Then
                _Planta = New Planta()
            End If
            Return _Planta
        End Get
        Set(Value As Planta)
            _Planta = Value
        End Set
    End Property

    Public Property Correlativo() As Boolean
        Get
            Return _Correlativo
        End Get
        Set(Value As Boolean)
            _Correlativo = Value
        End Set
    End Property

    Public Property Documentos() As Documentos
        Get
            If _Documentos Is Nothing Then
                _Documentos = New Documentos()
            End If
            Return _Documentos
        End Get
        Set(Value As Documentos)
            _Documentos = Value
        End Set
    End Property

    Public Property FechaDesde() As Date
        Get
            Return _FechaDesde
        End Get
        Set(Value As Date)
            _FechaDesde = Value
        End Set
    End Property

    Public Property FechaHasta() As Date
        Get
            Return _FechaHasta
        End Get
        Set(Value As Date)
            _FechaHasta = Value
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

    Public Property SaldoFinal() As Saldos
        Get
            If _SaldoFinal Is Nothing Then
                _SaldoFinal = New Saldos()
            End If
            Return _SaldoFinal
        End Get
        Set(Value As Saldos)
            _SaldoFinal = Value
        End Set
    End Property

    Public Property SaldoInicial() As Saldos
        Get
            If _SaldoInicial Is Nothing Then
                _SaldoInicial = New Saldos()
            End If
            Return _SaldoInicial
        End Get
        Set(Value As Saldos)
            _SaldoInicial = Value
        End Set
    End Property

    Public Property Transacciones() As Transacciones
        Get
            If _Transacciones Is Nothing Then
                _Transacciones = New Transacciones()
            End If
            Return _Transacciones
        End Get
        Set(Value As Transacciones)
            _Transacciones = Value
        End Set
    End Property

    Public Property TransaccionesDeArrastre() As Transacciones
        Get
            Return Me.Anterior.Transacciones
        End Get
        Set(Value As Transacciones)
            Me.Anterior.Transacciones = Value
        End Set
    End Property



#End Region

#Region "[MÉTODOS]"

    Public Sub Registrar(Optional ByRef objTransacao As Transacao = Nothing)

        If Me.Id = 0 Then
            Me.Id = Extracto.ObterIdExtracto()
        End If
        RegistrarInsert(objTransacao)

    End Sub

    Public Sub FechaDesdeRealizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.ExtractoCorrelativoFechaDesdeRealizar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Identificador_Numerico, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Numerico, Me.Moneda.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Identificador_Numerico, Me.Cliente.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Identificador_Numerico, Me.Banco.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            If dt.Rows(0)("FechaHasta") IsNot DBNull.Value Then
                Me.FechaDesde = dt.Rows(0)("FechaHasta")
            End If

            If dt.Rows(0)("IdExtracto") IsNot DBNull.Value Then
                Me.Anterior.Id = dt.Rows(0)("IdExtracto")
            Else
                Me.Anterior.Id = 0
            End If

            If dt.Rows(0)("IdDocumento") IsNot DBNull.Value Then
                Me.Anterior.Documento.Id = dt.Rows(0)("IdDocumento")
            Else
                Me.Anterior.Documento.Id = 0
            End If

        End If

    End Sub

    Public Function PorDocumentoSaldoFinalRealizar() As Short

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ExtractoPorDocumentoSaldoFinalRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdExtracto", ProsegurDbType.Identificador_Numerico, Me.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim objSaldo As Saldo = Nothing

            For Each dr As DataRow In dt.Rows

                objSaldo = New Saldo

                objSaldo.IdCentroProceso = 0
                objSaldo.IdBanco = Me.Banco.Id
                objSaldo.IdCliente = Me.Cliente.Id
                objSaldo.IdMoneda = Me.Moneda.Id

                If dr("Importe") IsNot DBNull.Value Then
                    objSaldo.Importe = dr("Importe")
                Else
                    objSaldo.Importe = Convert.ToDecimal(dr("Importe"))
                End If

                If dr("Disponible") IsNot DBNull.Value Then
                    objSaldo.Disponible = dr("Disponible")
                Else
                    objSaldo.Disponible = Convert.ToBoolean(dr("Disponible"))
                End If

                Me.SaldoFinal.Add(objSaldo)

            Next

        End If

        Me.SaldoFinal.IntegrarSobreCentrosProceso()

    End Function

    Private Sub TransaccionesRealizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = "PD_EXTRACTOTRANSREAL_" & Prosegur.Genesis.Comon.Util.Version
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCentroProceso", ProsegurDbType.Observacao_Longa, Me.ListaIdCentroProceso))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdBanco", ProsegurDbType.Observacao_Longa, Me.ListaIdBanco))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdCliente", ProsegurDbType.Observacao_Longa, Me.ListaIdCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_ListaIdMoneda", ProsegurDbType.Observacao_Longa, Me.ListaIdMoneda))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_FechaHasta", ProsegurDbType.Data_Hora, Me.FechaHasta))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' adicionar para transaciones
            Me.Transacciones.RS = dt

        End If

    End Sub

    Public Sub RegistrarInsert(Optional ByRef objTransacao As Transacao = Nothing)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.ExtractoRegistrar.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdExtracto", ProsegurDbType.Identificador_Numerico, Me.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaDesde", ProsegurDbType.Data_Hora, Me.FechaDesde))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FechaHasta", ProsegurDbType.Data_Hora, Me.FechaHasta))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdPlanta", ProsegurDbType.Identificador_Numerico, Me.Planta.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdMoneda", ProsegurDbType.Identificador_Numerico, Me.Moneda.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdCliente", ProsegurDbType.Identificador_Numerico, Me.Cliente.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdBanco", ProsegurDbType.Identificador_Numerico, Me.Banco.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdAnterior", ProsegurDbType.Identificador_Numerico, Me.Anterior.Id))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "VistaCliente", ProsegurDbType.Logico, Me.VistaCliente))

        If objTransacao IsNot Nothing Then
            objTransacao.AdicionarItemTransacao(comando)
        Else
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)
        End If

        SaldoFinalRegistrar(objTransacao)
        DocumentosRegistrar(objTransacao)

    End Sub

    Public Sub DocumentosRegistrar(Optional ByRef objTransacao As Transacao = Nothing)

        Dim comando As IDbCommand = Nothing

        For Each objDocumento As Documento In Me.Documentos

            comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.ExtractoDocumentoRegistrar.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdExtracto", ProsegurDbType.Identificador_Numerico, Me.Id))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, objDocumento.Id))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Accion", ProsegurDbType.Descricao_Longa, objDocumento.NumComprobante))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "DeArrastre", ProsegurDbType.Logico, objDocumento.Exportado))

            If objTransacao IsNot Nothing Then
                objTransacao.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)
            End If
        Next

    End Sub

    Public Sub SaldoFinalRegistrar(Optional ByRef objTransacao As Transacao = Nothing)

        Dim comando As IDbCommand = Nothing
        Dim IdSaldoFinal As Integer = 0

        For Each Saldo In Me.SaldoFinal

            ' obter id saldo final
            IdSaldoFinal = ObterIdExtractoSaldoFinal()

            comando = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
            comando.CommandType = CommandType.Text
            comando.CommandText = My.Resources.ExtractoSaldoFinalRegistrar.ToString()

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Idsaldoaextracto", ProsegurDbType.Identificador_Numerico, IdSaldoFinal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdExtracto", ProsegurDbType.Identificador_Numerico, Me.Id))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Importe", ProsegurDbType.Numero_Decimal, Saldo.Importe))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Disponible", ProsegurDbType.Logico, Saldo.Disponible))

            If objTransacao IsNot Nothing Then
                objTransacao.AdicionarItemTransacao(comando)
            Else
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)
            End If

        Next

    End Sub

    Public Sub Eliminar(ByRef objTransacao As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_ExtractoEliminar"
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Id))

        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    Public Sub PorDocumentoRealizar()

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.ExtractoPorDocumentoRealizar.ToString()
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IdDocumento", ProsegurDbType.Identificador_Numerico, Me.Documento.Id))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            If dt.Rows(0)("IdExtracto") IsNot DBNull.Value Then
                Me.Id = dt.Rows(0)("IdExtracto")
            Else
                Me.Id = 0
            End If

            If dt.Rows(0)("FechaDesde") IsNot DBNull.Value Then
                Me.FechaDesde = Convert.ToDateTime(dt.Rows(0)("FechaDesde"))
            Else
                Me.FechaDesde = DateTime.MinValue
            End If

            If dt.Rows(0)("FechaHasta") IsNot DBNull.Value Then
                Me.FechaHasta = Convert.ToDateTime(dt.Rows(0)("FechaHasta"))
            Else
                Me.FechaHasta = DateTime.MinValue
            End If

            If dt.Rows(0)("IdPlanta") IsNot DBNull.Value Then
                Me.Planta.Id = Convert.ToInt32(dt.Rows(0)("IdPlanta"))
            Else
                Me.Planta.Id = 0
            End If

            If dt.Rows(0)("IdMoneda") IsNot DBNull.Value Then
                Me.Moneda.Id = Convert.ToInt32(dt.Rows(0)("IdMoneda"))
            Else
                Me.Moneda.Id = 0
            End If

            If dt.Rows(0)("IdCliente") IsNot DBNull.Value Then
                Me.Cliente.Id = Convert.ToInt32(dt.Rows(0)("IdCliente"))
            Else
                Me.Cliente.Id = 0
            End If

            If dt.Rows(0)("IdBanco") IsNot DBNull.Value Then
                Me.Banco.Id = Convert.ToInt32(dt.Rows(0)("IdBanco"))
            Else
                Me.Banco.Id = 0
            End If

            If dt.Rows(0)("IdAnterior") IsNot DBNull.Value Then
                Me.Anterior.Id = Convert.ToInt32(dt.Rows(0)("IdAnterior"))
            Else
                Me.Anterior.Id = 0
            End If

            PorDocumentoTransaccionesRealizar()
            PorDocumentoSaldoFinalRealizar()
            PorDocumentoSaldoInicialRealizar()

        End If

    End Sub

    ''' <summary>
    ''' Realiza transação por documento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 19/06/2009 Criado
    ''' </history>
    Private Sub PorDocumentoTransaccionesRealizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        ' obter query
        comando.CommandText = "PD_ExtractoPorDocumentoTransac"
        comando.CommandType = CommandType.StoredProcedure

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "v_IdDocumento", ProsegurDbType.Inteiro_Longo, Me.Documento.Id))
        comando.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.Output, DBNull.Value, OracleClient.OracleType.Cursor))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' adicionar para transaciones
            Me.Transacciones.RS = dt

        End If

    End Sub

    Public Sub PorDocumentoSaldoInicialRealizar()

        Dim ImporteDisponible As Decimal = 0
        Dim ImporteNoDisponible As Decimal = 0

        If Me.Anterior.Id <> 0 Then

            Me.Anterior.PorDocumentoSaldoFinalRealizar()
            Me.SaldoInicial = Me.Anterior.SaldoFinal

        Else

            Dim objTransaciones As Transacciones = Me.Transacciones

            For Each Saldo In Me.SaldoFinal.SumasSobreCPs
                If Saldo.Disponible Then
                    ImporteDisponible += Saldo.Importe
                Else
                    ImporteNoDisponible += Saldo.Importe
                End If
            Next

            ' percorrer o datatable que está na memória de transaciones
            For Each dr As DataRow In objTransaciones.RS.Rows

                If dr("Disponible") Then
                    ImporteDisponible -= dr("Importe")
                Else
                    ImporteNoDisponible -= dr("Importe")
                End If

            Next

            ' limpar saldo inicial
            Me.SaldoInicial.Clear()

            Dim objSaldoDisp As New Saldo
            objSaldoDisp.IdCentroProceso = 0
            objSaldoDisp.IdBanco = Me.Banco.Id
            objSaldoDisp.IdCliente = Me.Cliente.Id
            objSaldoDisp.IdMoneda = Me.Moneda.Id
            objSaldoDisp.Importe = ImporteDisponible
            objSaldoDisp.Disponible = True
            Me.SaldoInicial.Add(objSaldoDisp)

            Dim objSaldoNaoDisp As New Saldo
            objSaldoNaoDisp.IdCentroProceso = 0
            objSaldoNaoDisp.IdBanco = Me.Banco.Id
            objSaldoNaoDisp.IdCliente = Me.Cliente.Id
            objSaldoNaoDisp.IdMoneda = Me.Moneda.Id
            objSaldoNaoDisp.Importe = ImporteNoDisponible
            objSaldoNaoDisp.Disponible = False
            Me.SaldoInicial.Add(objSaldoDisp)

            Me.SaldoInicial.IntegrarSobreCentrosProceso()

        End If

    End Sub

    Private Function TransaccionesDeArrastreRealizar() As Short

        ' executar método de transaciones
        Me.Anterior.PorDocumentoRealizar()

    End Function

    Public Sub Realizar()

        If Me.Correlativo Then

            FechaDesdeRealizar()

            If Me.Anterior.Id > 0 Then

                TransaccionesDeArrastreRealizar()

            End If

        End If

        TransaccionesRealizar()
        SaldoFinalRealizar()

    End Sub

    Public Function SaldoFinalRealizar() As Short

        Me.SaldoFinal.Actual = False
        Me.SaldoFinal.Fecha = Me.FechaHasta
        Me.SaldoFinal.ConSaldosCero = False
        Me.SaldoFinal.DiscriminarEspecies = False
        Me.SaldoFinal.IntegrarCentrosProceso = True
        Me.SaldoFinal.ListaIdBanco = Me.ListaIdBanco
        Me.SaldoFinal.ListaIdCentroProceso = Me.ListaIdCentroProceso
        Me.SaldoFinal.ListaIdCliente = Me.ListaIdCliente
        Me.SaldoFinal.ListaIdEspecie = String.Empty
        Me.SaldoFinal.ListaIdMoneda = Me.ListaIdMoneda
        Me.SaldoFinal.SoloSaldoDisponible = False
        Me.SaldoFinal.Realizar()

    End Function

    Public Shared Function ObterIdExtracto() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.SExtracto.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

    Public Shared Function ObterIdExtractoSaldoFinal() As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)

        comando.CommandText = My.Resources.SSaldoFinal.ToString()
        comando.CommandType = CommandType.Text

        Return CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando), Integer)

    End Function

#End Region

End Class