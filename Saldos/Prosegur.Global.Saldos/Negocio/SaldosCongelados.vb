Imports Prosegur.DbHelper

<Serializable()> _
Public Class SaldosCongelados
    Inherits List(Of SaldoCongelado)

    Private _idSaldoCongelado As Integer
    Private _idsMonedas As List(Of Integer)
    Private _tipoRelatorio As Integer

#Region "[PROPRIEDADES]"

    Public Property IdSaldoCongelado() As Integer
        Get
            Return _idSaldoCongelado
        End Get
        Set(value As Integer)
            _idSaldoCongelado = value
        End Set
    End Property

    Public Property IdsMonedas() As List(Of Integer)
        Get
            Return _idsMonedas
        End Get
        Set(value As List(Of Integer))
            _idsMonedas = value
        End Set
    End Property

    Public Property TipoRelatorio() As Integer
        Get
            Return _tipoRelatorio
        End Get
        Set(value As Integer)
            _tipoRelatorio = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Recupera o saldo congelado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/09/2010 - Criado
    ''' </history>
    Public Sub Realizar()

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandText = My.Resources.SaldosCongeladosSeleccionarById.ToString
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "IDSALDOCONGELADO", ProsegurDbType.Inteiro_Curto, IdSaldoCongelado))

        ' executar comando
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim dtSaldosCongelados As DataTable = Nothing

            dtSaldosCongelados = Util.DeserializarDT(dt.Rows(0)("SALDOCONGELADO"))

            If dtSaldosCongelados IsNot Nothing AndAlso dtSaldosCongelados.Rows.Count > 0 Then

                Select Case TipoRelatorio

                    Case 1

                        PreencherObjetosPorTipo(dtSaldosCongelados)

                    Case 2

                        PreencherObjetosPorSucursal(dtSaldosCongelados)

                    Case 3

                        PreencherObjetosPorDetalle(dtSaldosCongelados)

                End Select

            End If

        End If

    End Sub

    Private Sub PreencherObjetosPorSucursal(dt As DataTable)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim SaldoPorTipo As Negocio.SaldoCongelado
            Dim DetalleDoSaldo As Negocio.SaldoCongeladoDetalle
            Dim Planta As String
            Dim TipoCliente As String

            For Each dr In dt.Rows

                If IdsMonedas.Contains(dr("IdMoneda")) Then

                    Planta = dr("Planta")
                    TipoCliente = dr("Tipo")

                    SaldoPorTipo = (From saldo In Me _
                                    Where saldo.Planta = Planta _
                                    AndAlso saldo.Tipo = TipoCliente).FirstOrDefault

                    If SaldoPorTipo Is Nothing Then

                        SaldoPorTipo = New Negocio.SaldoCongelado

                        With SaldoPorTipo
                            .Planta = Planta
                            .Tipo = TipoCliente
                            .DetalleImporte = New Negocio.SaldosCongeladosDetalle

                        End With

                        Me.Add(SaldoPorTipo)

                    End If

                    DetalleDoSaldo = (From det In SaldoPorTipo.DetalleImporte _
                            Where det.IdMoneda = dr("IdMoneda")).FirstOrDefault

                    If DetalleDoSaldo IsNot Nothing Then
                        DetalleDoSaldo.Importe += dr("Importe")
                    Else
                        DetalleDoSaldo = New Negocio.SaldoCongeladoDetalle

                        With DetalleDoSaldo
                            .IdMoneda = dr("IdMoneda")
                            .Importe = dr("Importe")
                            .Moneda = dr("Moneda")
                        End With

                        SaldoPorTipo.DetalleImporte.Add(DetalleDoSaldo)
                    End If

                End If

            Next

        End If

    End Sub

    Private Sub PreencherObjetosPorTipo(dt As DataTable)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim SaldoPorTipo As Negocio.SaldoCongelado
            Dim DetalleDoSaldo As Negocio.SaldoCongeladoDetalle
            Dim Planta As String
            Dim TipoCliente As String
            Dim Canal As String

            For Each dr In dt.Rows

                If IdsMonedas.Contains(dr("IdMoneda")) Then

                    Planta = dr("Planta")
                    TipoCliente = dr("Tipo")
                    Canal = dr("Canal")

                    SaldoPorTipo = (From saldo In Me _
                                    Where saldo.Planta = Planta _
                                    AndAlso saldo.Tipo = TipoCliente _
                                    AndAlso saldo.Canal = Canal).FirstOrDefault

                    If SaldoPorTipo Is Nothing Then

                        SaldoPorTipo = New Negocio.SaldoCongelado

                        With SaldoPorTipo
                            .Planta = Planta
                            .Tipo = TipoCliente
                            .Canal = Canal
                            .DetalleImporte = New Negocio.SaldosCongeladosDetalle
                        End With

                        Me.Add(SaldoPorTipo)

                    End If

                    DetalleDoSaldo = (From det In SaldoPorTipo.DetalleImporte _
                            Where det.IdMoneda = dr("IdMoneda")).FirstOrDefault

                    If DetalleDoSaldo IsNot Nothing Then
                        DetalleDoSaldo.Importe += dr("Importe")
                    Else
                        DetalleDoSaldo = New Negocio.SaldoCongeladoDetalle

                        With DetalleDoSaldo
                            .IdMoneda = dr("IdMoneda")
                            .Importe = dr("Importe")
                            .Moneda = dr("Moneda")
                        End With

                        SaldoPorTipo.DetalleImporte.Add(DetalleDoSaldo)
                    End If

                End If

            Next

        End If
    End Sub

    Private Sub PreencherObjetosPorDetalle(dt As DataTable)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim SaldoPorTipo As Negocio.SaldoCongelado
            Dim DetalleDoSaldo As Negocio.SaldoCongeladoDetalle
            Dim Planta As String
            Dim TipoCliente As String
            Dim Cliente As String
            Dim canal As String

            For Each dr In dt.Rows

                If IdsMonedas.Contains(dr("IdMoneda")) Then

                    Planta = dr("Planta")
                    TipoCliente = dr("Tipo")
                    Cliente = dr("Cliente")
                    canal = dr("Canal")

                    SaldoPorTipo = (From saldo In Me _
                                    Where saldo.Planta = Planta _
                                    AndAlso saldo.Tipo = TipoCliente AndAlso saldo.Cliente = Cliente AndAlso saldo.Canal = canal).FirstOrDefault

                    If SaldoPorTipo Is Nothing Then

                        SaldoPorTipo = New Negocio.SaldoCongelado

                        With SaldoPorTipo
                            .Planta = Planta
                            .Tipo = TipoCliente
                            .Cliente = Cliente
                            .IdPS = dr("IdPS")
                            .Canal = canal
                            .DetalleImporte = New Negocio.SaldosCongeladosDetalle
                        End With

                        Me.Add(SaldoPorTipo)

                    End If

                    DetalleDoSaldo = (From det In SaldoPorTipo.DetalleImporte _
                            Where det.IdMoneda = dr("IdMoneda")).FirstOrDefault

                    If DetalleDoSaldo IsNot Nothing Then
                        DetalleDoSaldo.Importe += dr("Importe")
                    Else
                        DetalleDoSaldo = New Negocio.SaldoCongeladoDetalle

                        With DetalleDoSaldo
                            .IdMoneda = dr("IdMoneda")
                            .Importe = dr("Importe")
                            .Moneda = dr("Moneda")
                        End With

                        SaldoPorTipo.DetalleImporte.Add(DetalleDoSaldo)
                    End If

                End If

            Next

        End If

    End Sub
End Class
