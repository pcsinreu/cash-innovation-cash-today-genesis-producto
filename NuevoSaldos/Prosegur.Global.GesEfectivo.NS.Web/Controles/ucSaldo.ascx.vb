Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxTabControl
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Threading.Tasks

Public Class ucSaldo
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Modo() As Enumeradores.Modo
    Public Property Divisas As ObservableCollection(Of Clases.Divisa)
        Get
            Return ViewState("Divisas")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState("Divisas") = value
        End Set
    End Property

    Public Property DivisasAnterior As ObservableCollection(Of Clases.Divisa)
        Get
            Return ViewState("DivisasAnterior")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState("DivisasAnterior") = value
        End Set
    End Property

    Public Property lsthdfTotalesEfectivoDetallar As List(Of HiddenField)
        Get
            If ViewState("lsthdfTotalesEfectivoDetallar") Is Nothing Then
                ViewState("lsthdfTotalesEfectivoDetallar") = New List(Of HiddenField)
            End If
            Return ViewState("lsthdfTotalesEfectivoDetallar")
        End Get
        Set(value As List(Of HiddenField))
            ViewState("lsthdfTotalesEfectivoDetallar") = value
        End Set
    End Property

    Public Property lsthdfTotalesMedioPagoDetallar As List(Of HiddenField)
        Get
            If ViewState("lsthdfTotalesMedioPagoDetallar") Is Nothing Then
                ViewState("lsthdfTotalesMedioPagoDetallar") = New List(Of HiddenField)
            End If
            Return ViewState("lsthdfTotalesMedioPagoDetallar")
        End Get
        Set(value As List(Of HiddenField))
            ViewState("lsthdfTotalesMedioPagoDetallar") = value
        End Set
    End Property

    Public Property lsthdfTotalesEfectivoModificar As List(Of HiddenField)
        Get
            If ViewState("lsthdfTotalesEfectivoModificar") Is Nothing Then
                ViewState("lsthdfTotalesEfectivoModificar") = New List(Of HiddenField)
            End If
            Return ViewState("lsthdfTotalesEfectivoModificar")
        End Get
        Set(value As List(Of HiddenField))
            ViewState("lsthdfTotalesEfectivoModificar") = value
        End Set
    End Property

    Public Property lsthdfTotalesMedioPagoModificar As List(Of HiddenField)
        Get
            If ViewState("lsthdfTotalesMedioPagoModificar") Is Nothing Then
                ViewState("lsthdfTotalesMedioPagoModificar") = New List(Of HiddenField)
            End If
            Return ViewState("lsthdfTotalesMedioPagoModificar")
        End Get
        Set(value As List(Of HiddenField))
            ViewState("lsthdfTotalesMedioPagoModificar") = value
        End Set
    End Property

    Public Property ControlesAdicionadosDetallar As List(Of Object)
        Get
            If ViewState("ControlesAdicionadosDetallar") Is Nothing Then
                ViewState("ControlesAdicionadosDetallar") = New List(Of Object)
            End If
            Return ViewState("ControlesAdicionadosDetallar")
        End Get
        Set(value As List(Of Object))
            ViewState("ControlesAdicionadosDetallar") = value
        End Set
    End Property

    Public Property ControlesAdicionadosModificar As List(Of Object)
        Get
            If ViewState("ControlesAdicionadosModificar") Is Nothing Then
                ViewState("ControlesAdicionadosModificar") = New List(Of Object)
            End If
            Return ViewState("ControlesAdicionadosModificar")
        End Get
        Set(value As List(Of Object))
            ViewState("ControlesAdicionadosModificar") = value
        End Set
    End Property

    Public ReadOnly Property dvUcSaldoEfectivoMPModificar As String
        Get
            Return divUcSaldoEfectivoMPModificar.ClientID
        End Get
    End Property

    Public ReadOnly Property pnlSaldoEfectivoModificar As String
        Get
            Return pnlUcSaldoEfectivoModificar.ClientID
        End Get
    End Property

    Public ReadOnly Property pnlSaldoMedioPagoModificar As String
        Get
            Return pnlUcSaldoMPModificar.ClientID
        End Get
    End Property

    Public ReadOnly Property pnlSaldoEfectivoDetallar As String
        Get
            Return pnlUcSaldoEfectivoDetallar.ClientID
        End Get
    End Property

    Public ReadOnly Property pnlSaldoMedioPagoDetallar As String
        Get
            Return pnlUcSaldoMPDetallar.ClientID
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
        For Each hdfTotalesMedioPagoDetallar In lsthdfTotalesMedioPagoDetallar
            pnlUcSaldoMPDetallar.Controls.Add(hdfTotalesMedioPagoDetallar)
        Next
        For Each hdfTotalesMedioPagoModificar In lsthdfTotalesMedioPagoModificar
            pnlUcSaldoMPModificar.Controls.Add(hdfTotalesMedioPagoModificar)
        Next
        For Each hdfTotalesEfectivoDetallar In lsthdfTotalesEfectivoDetallar
            pnlUcSaldoEfectivoDetallar.Controls.Add(hdfTotalesEfectivoDetallar)
        Next
        For Each hdfTotalesEfectivoModificar In lsthdfTotalesEfectivoModificar
            pnlUcSaldoEfectivoModificar.Controls.Add(hdfTotalesEfectivoModificar)
        Next
    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)

        Return New List(Of String)

    End Function

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTituloSaldoModificar.Text = Traduzir("072_elegirsaldo")
        lblTituloSaldoDetallar.Text = Traduzir("072_saldomodificado")
    End Sub

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.Page.IsPostBack Then
            ASPxPageControl.RegisterBaseScript(Me.Page)
        End If
    End Sub

    Public Sub AtualizarDivisas()

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ucSaldoEfectivoMedioPago_Inicializar_" & Me.ClientID, "InicializarSaldo();", True)

        Select Case Modo

            Case Enumeradores.Modo.Alta

                CargarControlesAlta()

            Case Enumeradores.Modo.Consulta, Enumeradores.Modo.Modificacion

                CargarControlesConsultaYModificacion()

        End Select

    End Sub

    Private Sub CargarControlesAlta()

        If Me.Divisas IsNot Nothing AndAlso Me.Divisas.Count > 0 Then

            ' controle da esquerda
            pageControlEfectivoMPModificar.TabPages.Clear()

            ' controle da direita
            pageControlEfectivoMPDetallar.TabPages.Clear()

            pnlUcSaldoEfectivoModificar.Controls.Clear()
            lsthdfTotalesEfectivoModificar = New List(Of HiddenField)

            pnlUcSaldoMPModificar.Controls.Clear()
            lsthdfTotalesMedioPagoModificar = New List(Of HiddenField)

            pnlUcSaldoEfectivoDetallar.Controls.Clear()
            lsthdfTotalesEfectivoDetallar = New List(Of HiddenField)

            pnlUcSaldoMPDetallar.Controls.Clear()
            lsthdfTotalesMedioPagoDetallar = New List(Of HiddenField)

            Dim divisasPorTipoMedioPago As List(Of Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))) = RecuperarDivisasPorTipoMedioPago(Me.Divisas)

            Dim tfSaldoAnterior As task
            Dim tfSaldoDetallar As task

            tfSaldoAnterior = New Task(Sub()

                                           For Each div In Divisas
                                               If (div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count > 0 AndAlso div.ValoresTotalesEfectivo.Exists(Function(e) e.Importe <> 0)) OrElse (div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso div.Denominaciones.Exists(Function(e) e.ValorDenominacion IsNot Nothing AndAlso e.ValorDenominacion.Count > 0 AndAlso e.ValorDenominacion.Exists(Function(ee) ee.Importe <> 0))) Then
                                                   AnadirTabPageSaldoEfectivoModificar(div)
                                               End If
                                           Next

                                           If divisasPorTipoMedioPago IsNot Nothing AndAlso divisasPorTipoMedioPago.Count > 0 Then
                                               For Each divTipoMedioPago In divisasPorTipoMedioPago
                                                   AnadirTabPageSaldoMPModificar(divTipoMedioPago)
                                               Next
                                           End If

                                       End Sub)

            tfSaldoDetallar = New Task(Sub()

                                           For Each div In Divisas
                                               If (div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count > 0 AndAlso div.ValoresTotalesEfectivo.Exists(Function(e) e.Importe <> 0)) OrElse (div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso div.Denominaciones.Exists(Function(e) e.ValorDenominacion IsNot Nothing AndAlso e.ValorDenominacion.Count > 0 AndAlso e.ValorDenominacion.Exists(Function(ee) ee.Importe <> 0))) Then
                                                   AnadirTabPageSaldoEfectivoDetallar(div)
                                               End If
                                           Next

                                           If divisasPorTipoMedioPago IsNot Nothing AndAlso divisasPorTipoMedioPago.Count > 0 Then
                                               For Each divTipoMedioPago In divisasPorTipoMedioPago
                                                   AnadirTabPageSaldoMPDetallar(divTipoMedioPago)
                                               Next
                                           End If

                                       End Sub)

            tfSaldoAnterior.Start()
            tfSaldoDetallar.Start()

            Task.WaitAll(New Task() {tfSaldoAnterior, tfSaldoDetallar})

            ' controle da esquerda
            divUcSaldoEfectivoMPModificar.Style("display") = "block"

            ' controle da direita
            divUcSaldoEfectivoMPDetallar.Style("display") = "block"


        Else
            ' controle da esquerda
            pageControlEfectivoMPModificar.TabPages.Clear()
            divUcSaldoEfectivoMPModificar.Style("display") = "none"

            ' controle da direita
            pageControlEfectivoMPDetallar.TabPages.Clear()
            divUcSaldoEfectivoMPDetallar.Style("display") = "none"

        End If

    End Sub

    Private Sub CargarControlesConsultaYModificacion()

        If Me.DivisasAnterior IsNot Nothing AndAlso Me.DivisasAnterior.Count > 0 AndAlso
           Me.Divisas IsNot Nothing AndAlso Me.Divisas.Count > 0 Then

            ' controle da esquerda
            pageControlEfectivoMPModificar.TabPages.Clear()

            ' controle da direita
            pageControlEfectivoMPDetallar.TabPages.Clear()

            ' limpar os controles
            pnlUcSaldoEfectivoModificar.Controls.Clear()
            lsthdfTotalesEfectivoModificar = New List(Of HiddenField)

            pnlUcSaldoMPModificar.Controls.Clear()
            lsthdfTotalesMedioPagoModificar = New List(Of HiddenField)

            pnlUcSaldoEfectivoDetallar.Controls.Clear()
            lsthdfTotalesEfectivoDetallar = New List(Of HiddenField)

            pnlUcSaldoMPDetallar.Controls.Clear()
            lsthdfTotalesMedioPagoDetallar = New List(Of HiddenField)

            Dim divisasAdicionadas As New List(Of String)

            For Each div In DivisasAnterior


                If (div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count > 0 AndAlso div.ValoresTotalesEfectivo.Exists(Function(e) e.Importe <> 0)) OrElse
                 (div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0) Then

                    If Modo = Enumeradores.Modo.Consulta Then
                        If (div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso div.Denominaciones.Exists(Function(e) e.ValorDenominacion IsNot Nothing AndAlso e.ValorDenominacion.Count > 0 AndAlso e.ValorDenominacion.Exists(Function(ee) ee.Importe <> 0)) OrElse
                            div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count > 0 AndAlso div.ValoresTotalesEfectivo.Exists(Function(e) e.Importe <> 0)) Then
                            ' controle da esquerda
                            AnadirTabPageSaldoEfectivoModificar(div)
                        End If
                    Else
                        If Not divisasAdicionadas.Contains(div.CodigoISO) Then
                            divisasAdicionadas.Add(div.CodigoISO)
                        End If
                        ' controle da esquerda
                        AnadirTabPageSaldoEfectivoModificar(div)
                    End If

                End If

            Next

            For Each div In Divisas

                If Modo = Enumeradores.Modo.Modificacion Then

                    If divisasAdicionadas.Contains(div.CodigoISO) Then
                        ' controle da direita
                        AnadirTabPageSaldoEfectivoDetallar(div)

                    End If

                Else

                    If (div.ValoresTotalesEfectivo IsNot Nothing AndAlso div.ValoresTotalesEfectivo.Count > 0 AndAlso div.ValoresTotalesEfectivo.Exists(Function(e) e.Importe <> 0)) OrElse
                       (div.Denominaciones IsNot Nothing AndAlso div.Denominaciones.Count > 0 AndAlso div.Denominaciones.Exists(Function(e) e.ValorDenominacion IsNot Nothing AndAlso e.ValorDenominacion.Count > 0 AndAlso e.ValorDenominacion.Exists(Function(ee) ee.Importe <> 0))) Then

                        ' controle da direita
                        AnadirTabPageSaldoEfectivoDetallar(div)

                    End If

                End If

            Next

            ' controle da esquerda
            divUcSaldoEfectivoMPModificar.Style("display") = "block"

            ' controle da direita
            divUcSaldoEfectivoMPDetallar.Style("display") = "block"

            Dim divisasAnteriorPorTipoMedioPago As List(Of Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))) = RecuperarDivisasPorTipoMedioPago(Me.DivisasAnterior)

            If divisasAnteriorPorTipoMedioPago IsNot Nothing AndAlso divisasAnteriorPorTipoMedioPago.Count > 0 Then

                For Each divTipoMedioPago In divisasAnteriorPorTipoMedioPago

                    ' Controle da esquerda
                    AnadirTabPageSaldoMPModificar(divTipoMedioPago)

                Next

            End If

            Dim divisasPorTipoMedioPago As List(Of Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))) = RecuperarDivisasPorTipoMedioPago(Me.Divisas)

            If divisasPorTipoMedioPago IsNot Nothing AndAlso divisasPorTipoMedioPago.Count > 0 Then

                For Each divTipoMedioPago In divisasPorTipoMedioPago

                    ' Controle da direita
                    AnadirTabPageSaldoMPDetallar(divTipoMedioPago)

                Next

            End If

        Else
            ' controle da esquerda
            pageControlEfectivoMPModificar.TabPages.Clear()
            divUcSaldoEfectivoMPModificar.Style("display") = "none"

            ' controle da direita
            pageControlEfectivoMPDetallar.TabPages.Clear()
            divUcSaldoEfectivoMPDetallar.Style("display") = "none"

        End If

    End Sub

    Private Function RecuperarDivisasMPDetallar() As ObservableCollection(Of Clases.Divisa)

        Dim lstTotalesMedioPago As New List(Of Newtonsoft.Json.Linq.JArray)

        Dim divisasRetorno As New ObservableCollection(Of Clases.Divisa)

        For Each hdfTotalesMedioPagoDetallar In pnlUcSaldoMPDetallar.Controls

            If TypeOf hdfTotalesMedioPagoDetallar Is HiddenField Then
                Dim ixValor As String() = hdfTotalesMedioPagoDetallar.Value.ToString().Split(";")

                Dim indice As Integer = ixValor(0)
                Dim valor As String = ixValor(1)
                If Not String.IsNullOrEmpty(valor) AndAlso Not valor = "[]" Then
                    Dim totalesMedioPago As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(valor, New Newtonsoft.Json.JsonSerializerSettings() With {.Converters = {New Newtonsoft.Json.Converters.StringEnumConverter()}})
                    lstTotalesMedioPago.Add(totalesMedioPago)

                    Dim controle As ucSaldoMedioPagoDetallar = Nothing

                    Dim itemsUtilizados As New List(Of String)

                    For Each totales In totalesMedioPago

                        Dim identificadorDivisa As String = If(totales.Item("Divisa") Is Nothing, String.Empty, totales.Item("Divisa").Value(Of String)("Identificador"))
                        Dim identificadorMedioPago As String = If(totales.Item("MedioPago") Is Nothing, String.Empty, totales.Item("MedioPago").Value(Of String)("Identificador"))

                        If Not String.IsNullOrEmpty(identificadorDivisa) Then

                            If Not itemsUtilizados.Contains(identificadorDivisa) Then
                                itemsUtilizados.Add(identificadorDivisa)
                            Else
                                Continue For
                            End If

                            For Each ucSaldoMedioPago As ucSaldoMedioPagoDetallar In ControlesAdicionadosDetallar.Where(Function(e) TypeOf e Is ucSaldoMedioPagoDetallar)

                                If ucSaldoMedioPago.verificarTipoMedioPago(identificadorDivisa, identificadorMedioPago) Then
                                    controle = ucSaldoMedioPago
                                    Exit For
                                End If

                            Next

                            If controle IsNot Nothing Then

                                Dim divisa As Clases.Divisa = controle.RecuperarDivisa(totalesMedioPago, identificadorDivisa)
                                If divisa IsNot Nothing Then
                                    divisasRetorno.Add(divisa.Clonar())
                                End If

                            End If

                        End If

                    Next

                End If
            End If

        Next

        Return divisasRetorno

    End Function

    Private Function RecuperarDivisasEfectivoDetallar() As ObservableCollection(Of Clases.Divisa)

        Dim lstTotalesEfectivo As New List(Of Newtonsoft.Json.Linq.JArray)

        Dim divisasRetorno As New ObservableCollection(Of Clases.Divisa)

        For Each hdfTotalesEfectivoDetallar In pnlUcSaldoEfectivoDetallar.Controls

            If TypeOf hdfTotalesEfectivoDetallar Is HiddenField Then
                Dim ixValor As String() = hdfTotalesEfectivoDetallar.Value.ToString().Split(";")

                Dim indice As Integer = ixValor(0)
                Dim valor As String = ixValor(1)

                If Not String.IsNullOrEmpty(valor) AndAlso Not valor = "[]" Then

                    Dim totalesEfectivo As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(valor, New Newtonsoft.Json.JsonSerializerSettings() With {.Converters = {New Newtonsoft.Json.Converters.StringEnumConverter()}})
                    lstTotalesEfectivo.Add(totalesEfectivo)

                    Dim controle As ucSaldoEfectivoDetallar = Nothing

                    For Each ucSaldoEfectivo As ucSaldoEfectivoDetallar In ControlesAdicionadosDetallar.Where(Function(e) TypeOf e Is ucSaldoEfectivoDetallar)

                        If ucSaldoEfectivo.verificarDivisa(totalesEfectivo(0).Item("Divisa").Value(Of String)("Identificador")) Then
                            controle = ucSaldoEfectivo
                            Exit For
                        End If

                    Next

                    If controle IsNot Nothing Then

                        Dim divisa As Clases.Divisa = controle.RecuperarDivisa(totalesEfectivo)
                        If Not divisasRetorno.Exists(Function(e) e.Identificador = divisa.Identificador) Then
                            divisasRetorno.Add(divisa.Clonar)
                        End If

                    End If

                End If

            End If

        Next

        Return divisasRetorno

    End Function

    Private Function RecuperarDivisasMPModificar() As ObservableCollection(Of Clases.Divisa)

        Dim lstTotalesMedioPago As New List(Of Newtonsoft.Json.Linq.JArray)
        Dim divisasRetorno As New ObservableCollection(Of Clases.Divisa)

        For Each hdfTotalesMedioPagoModificar In pnlUcSaldoMPModificar.Controls

            If TypeOf hdfTotalesMedioPagoModificar Is HiddenField Then
                Dim ixValor As String() = hdfTotalesMedioPagoModificar.Value.ToString().Split(";")

                Dim indice As Integer = ixValor(0)
                Dim valor As String = ixValor(1)

                If Not String.IsNullOrEmpty(valor) AndAlso Not valor = "[]" Then

                    Dim totalesMedioPago As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(valor, New Newtonsoft.Json.JsonSerializerSettings() With {.Converters = {New Newtonsoft.Json.Converters.StringEnumConverter()}})
                    lstTotalesMedioPago.Add(totalesMedioPago)

                    Dim controle As ucSaldoMedioPagoModificar = Nothing

                    Dim itemsUtilizados As New List(Of String)

                    For Each totales In totalesMedioPago

                        Dim identificadorDivisa As String = If(totales.Item("Divisa") Is Nothing, String.Empty, totales.Item("Divisa").Value(Of String)("Identificador"))
                        Dim identificadorMedioPago As String = If(totales.Item("MedioPago") Is Nothing, String.Empty, totales.Item("MedioPago").Value(Of String)("Identificador"))
                        Dim tipoMedioPago As String = totales.Item("TipoMedioPago")

                        If Not String.IsNullOrEmpty(identificadorDivisa) Then

                            If Not itemsUtilizados.Contains(identificadorDivisa) Then
                                itemsUtilizados.Add(identificadorDivisa)
                            Else
                                Continue For
                            End If

                            For Each ucSaldoMedioPago As ucSaldoMedioPagoModificar In ControlesAdicionadosModificar.Where(Function(e) TypeOf e Is ucSaldoMedioPagoModificar)

                                If ucSaldoMedioPago.verificarTipoMedioPago(identificadorDivisa, identificadorMedioPago, tipoMedioPago) Then
                                    controle = ucSaldoMedioPago
                                    Exit For
                                End If

                            Next

                            If controle IsNot Nothing Then

                                Dim divisa As Clases.Divisa = controle.RecuperarDivisa(totalesMedioPago, identificadorDivisa)
                                If divisa IsNot Nothing Then
                                    divisasRetorno.Add(divisa.Clonar())
                                End If

                            End If

                        End If

                    Next

                End If

            End If

        Next

        Return divisasRetorno

    End Function

    Private Function RecuperarDivisasEfectivoModificar() As ObservableCollection(Of Clases.Divisa)

        Dim lstTotalesEfectivo As New List(Of Newtonsoft.Json.Linq.JArray)
        Dim divisasRetorno As New ObservableCollection(Of Clases.Divisa)

        For Each hdfTotalesEfectivoModificar In pnlUcSaldoEfectivoModificar.Controls

            If TypeOf hdfTotalesEfectivoModificar Is HiddenField Then
                Dim ixValor As String() = hdfTotalesEfectivoModificar.Value.ToString().Split(";")

                Dim indice As Integer = ixValor(0)
                Dim valor As String = ixValor(1)
                If Not String.IsNullOrEmpty(valor) AndAlso Not valor = "[]" Then
                    Dim totalesEfectivo As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(valor, New Newtonsoft.Json.JsonSerializerSettings() With {.Converters = {New Newtonsoft.Json.Converters.StringEnumConverter()}})
                    lstTotalesEfectivo.Add(totalesEfectivo)

                    Dim controle As ucSaldoEfectivoModificar = Nothing

                    For Each ucSaldoEfectivo As ucSaldoEfectivoModificar In ControlesAdicionadosModificar.Where(Function(e) TypeOf e Is ucSaldoEfectivoModificar)

                        If ucSaldoEfectivo.verificarDivisa(totalesEfectivo(0).Item("Divisa").Value(Of String)("Identificador")) Then
                            controle = ucSaldoEfectivo
                            Exit For
                        End If

                    Next

                    If controle IsNot Nothing Then

                        Dim divisa As Clases.Divisa = controle.RecuperarDivisa(totalesEfectivo)
                        divisasRetorno.Add(divisa)

                    End If

                End If

            End If

        Next

        Return divisasRetorno

    End Function

    ''' <summary>
    ''' Recuperar os valores informados para as divisas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarDivisas() As ObservableCollection(Of Clases.Divisa)

        ' recuperar os valores efetivos
        Dim divisaEfectivoDetallar As ObservableCollection(Of Clases.Divisa) = RecuperarDivisasEfectivoDetallar()
        ' recuperar os valores meios pagamento
        Dim divisaMedioPagoDetallar As ObservableCollection(Of Clases.Divisa) = RecuperarDivisasMPDetallar()

        Dim divisasGenerales As New ObservableCollection(Of Clases.Divisa)

        ' adicionar valores encontrados na coleção
        If divisaEfectivoDetallar IsNot Nothing AndAlso divisaEfectivoDetallar.Count > 0 Then
            divisasGenerales.AddRange(divisaEfectivoDetallar)
        End If

        If divisaMedioPagoDetallar IsNot Nothing AndAlso divisaMedioPagoDetallar.Count > 0 Then
            divisasGenerales.AddRange(divisaMedioPagoDetallar)
        End If

        ' unificar as divisas caso tenha divisas duplicadas nas coleções
        Util.UnificaItemsDivisas(divisasGenerales)
        ' excluir os objetos sem valores
        Util.BorrarItemsDivisasSinValoresCantidades(divisasGenerales)

        Return divisasGenerales

    End Function

    ''' <summary>
    ''' Recuperar os valores do saldo anterior
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RecuperarDivisasSaldoAnterior() As ObservableCollection(Of Clases.Divisa)

        ' recuperar os valores efetivos
        Dim divisaEfectivoModificar As ObservableCollection(Of Clases.Divisa) = RecuperarDivisasEfectivoModificar()
        ' recuperar os valores meios pagamento
        Dim divisaMedioPagoModificar As ObservableCollection(Of Clases.Divisa) = RecuperarDivisasMPModificar()

        Dim divisasGenerales As New ObservableCollection(Of Clases.Divisa)

        ' adicionar valores encontrados na coleção
        If divisaEfectivoModificar IsNot Nothing AndAlso divisaEfectivoModificar.Count > 0 Then
            divisasGenerales.AddRange(divisaEfectivoModificar)
        End If

        If divisaMedioPagoModificar IsNot Nothing AndAlso divisaMedioPagoModificar.Count > 0 Then
            divisasGenerales.AddRange(divisaMedioPagoModificar)
        End If

        ' unificar as divisas caso tenha divisas duplicadas nas coleções
        Util.UnificaItemsDivisas(divisasGenerales)
        ' excluir os objetos sem valores
        Util.BorrarItemsDivisasSinValoresCantidades(divisasGenerales)

        Return divisasGenerales

    End Function

    Private Sub AnadirTabPageSaldoEfectivoModificar(div As Clases.Divisa)

        Dim tab As New DevExpress.Web.ASPxTabControl.TabPage
        tab.Text = div.Descripcion
        tab.Name = div.Identificador

        Dim _ucSaldoEfectivoModificar As ucSaldoEfectivoModificar = LoadControl("~/Controles/ucSaldoEfectivoModificar.ascx")

        _ucSaldoEfectivoModificar.ID = "ucSaldoEfectivoModificar_" & div.CodigoISO
        _ucSaldoEfectivoModificar.Modo = Enumeradores.Modo.Consulta
        _ucSaldoEfectivoModificar.HabilitaCheckBox = Modo <> Enumeradores.Modo.Consulta
        _ucSaldoEfectivoModificar.Divisas = New ObservableCollection(Of Clases.Divisa)
        _ucSaldoEfectivoModificar.Divisas.Add(div)
        _ucSaldoEfectivoModificar.TrabajarConUnidadMedida = True
        _ucSaldoEfectivoModificar.TrabajarConCalidad = True
        _ucSaldoEfectivoModificar.TrabajarConNivelDetalle = True
        _ucSaldoEfectivoModificar.TipoValor = Enumeradores.TipoValor.NoDefinido

        tab.Controls.Add(_ucSaldoEfectivoModificar)
        pageControlEfectivoMPModificar.TabPages.Add(tab)

        'Adicionar hiddenfield para guardar valores do controle
        Dim hdftotalesEfectivo As New HiddenField
        hdftotalesEfectivo.ID = pageControlEfectivoMPModificar.ID & "_" & _ucSaldoEfectivoModificar.ID & "_hdftotalesEfectivo"
        hdftotalesEfectivo.Value = (pageControlEfectivoMPModificar.TabPages.Count - 1).ToString() + ";" + hdftotalesEfectivo.Value

        pnlUcSaldoEfectivoModificar.Controls.Add(hdftotalesEfectivo)
        lsthdfTotalesEfectivoModificar.Add(hdftotalesEfectivo)

        If Not ControlesAdicionadosModificar.Exists(Function(e) If(TypeOf e Is ucSaldoMedioPagoModificar, DirectCast(e, ucSaldoMedioPagoModificar).ID = _ucSaldoEfectivoModificar.ID, String.Empty = _ucSaldoEfectivoModificar.ID)) Then

            ControlesAdicionadosModificar.Add(_ucSaldoEfectivoModificar)

        End If

    End Sub

    Private Sub AnadirTabPageSaldoMPModificar(divTipoMedioPago As Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa)))

        Dim divisasMP As ObservableCollection(Of Clases.Divisa) = divTipoMedioPago.Item2.Clonar

        For Each div In divisasMP
            If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count > 0 Then
                div.ValoresTotalesTipoMedioPago = div.ValoresTotalesTipoMedioPago.RemoveAll(Function(r) r.TipoMedioPago <> divTipoMedioPago.Item1)
            End If
        Next

        Dim tab As New DevExpress.Web.ASPxTabControl.TabPage

        Select Case divTipoMedioPago.Item1
            Case Enumeradores.TipoMedioPago.Cheque
                tab.Text = Traduzir("055_cheque")
            Case Enumeradores.TipoMedioPago.OtroValor
                tab.Text = Traduzir("055_otrovalor")
            Case Enumeradores.TipoMedioPago.Tarjeta
                tab.Text = Traduzir("055_tarjeta")
            Case Enumeradores.TipoMedioPago.Ticket
                tab.Text = Traduzir("055_ticket")
        End Select

        tab.Name = divTipoMedioPago.Item1.RecuperarValor

        Dim _ucSaldoMedioPagoModificar As ucSaldoMedioPagoModificar = LoadControl("~/Controles/ucSaldoMedioPagoModificar.ascx")

        _ucSaldoMedioPagoModificar.ID = "ucSaldoMedioPagoModificar_" & tab.Name
        _ucSaldoMedioPagoModificar.Modo = Enumeradores.Modo.Consulta
        _ucSaldoMedioPagoModificar.HabilitaCheckBox = Modo <> Enumeradores.Modo.Consulta
        _ucSaldoMedioPagoModificar.Divisas = New ObservableCollection(Of Clases.Divisa)
        _ucSaldoMedioPagoModificar.Divisas.AddRange(divisasMP)
        _ucSaldoMedioPagoModificar.TipoValor = Enumeradores.TipoValor.NoDefinido
        _ucSaldoMedioPagoModificar.TipoMedioPago = divTipoMedioPago.Item1.ToString

        tab.Controls.Add(_ucSaldoMedioPagoModificar)
        pageControlEfectivoMPModificar.TabPages.Add(tab)

        'Adicionar hiddenfield para guardar valores do controle
        Dim hdftotalesMedioPago As New HiddenField
        hdftotalesMedioPago.ID = pageControlEfectivoMPModificar.ID & "_ucSaldoMedioPagoModificar_" & tab.Name & "_hdftotalesMedioPago"
        hdftotalesMedioPago.Value = (pageControlEfectivoMPModificar.TabPages.Count - 1).ToString() + ";" + hdftotalesMedioPago.Value
        pnlUcSaldoMPModificar.Controls.Add(hdftotalesMedioPago)
        lsthdfTotalesMedioPagoModificar.Add(hdftotalesMedioPago)

        If Not ControlesAdicionadosModificar.Exists(Function(e) If(TypeOf e Is ucSaldoMedioPagoModificar, DirectCast(e, ucSaldoMedioPagoModificar).ID = _ucSaldoMedioPagoModificar.ID, String.Empty = _ucSaldoMedioPagoModificar.ID)) Then

            ControlesAdicionadosModificar.Add(_ucSaldoMedioPagoModificar)

        End If

    End Sub

    Private Sub AnadirTabPageSaldoEfectivoDetallar(div As Clases.Divisa)

        Dim divisa As Clases.Divisa = div.Clonar

        If Modo = Enumeradores.Modo.Alta Then
            Util.ZerarValoresDivisa(divisa)
        End If

        Dim tab As New DevExpress.Web.ASPxTabControl.TabPage
        tab.Text = div.Descripcion
        tab.Name = div.Identificador

        Dim _ucSaldoEfectivoDetallar As ucSaldoEfectivoDetallar = LoadControl("~/Controles/ucSaldoEfectivoDetallar.ascx")

        _ucSaldoEfectivoDetallar.ID = "ucSaldoEfectivoDetallar_" & div.CodigoISO
        _ucSaldoEfectivoDetallar.Modo = Modo
        _ucSaldoEfectivoDetallar.Divisas = New ObservableCollection(Of Clases.Divisa)
        _ucSaldoEfectivoDetallar.Divisas.Add(divisa)
        _ucSaldoEfectivoDetallar.TrabajarConUnidadMedida = True
        _ucSaldoEfectivoDetallar.TrabajarConCalidad = True
        _ucSaldoEfectivoDetallar.TrabajarConNivelDetalle = False
        _ucSaldoEfectivoDetallar.TipoValor = Enumeradores.TipoValor.NoDefinido

        tab.Controls.Add(_ucSaldoEfectivoDetallar)
        pageControlEfectivoMPDetallar.TabPages.Add(tab)

        'Adicionar hiddenfield para guardar valores do controle
        Dim hdftotalesEfectivo As New HiddenField
        hdftotalesEfectivo.ID = pageControlEfectivoMPDetallar.ID & "_" & _ucSaldoEfectivoDetallar.ID & "_hdftotalesEfectivo"
        hdftotalesEfectivo.Value = (pageControlEfectivoMPDetallar.TabPages.Count - 1).ToString() + ";" + hdftotalesEfectivo.Value
        pnlUcSaldoEfectivoDetallar.Controls.Add(hdftotalesEfectivo)
        lsthdfTotalesEfectivoDetallar.Add(hdftotalesEfectivo)

        If Not ControlesAdicionadosDetallar.Exists(Function(e) If(TypeOf e Is ucSaldoEfectivoDetallar, DirectCast(e, ucSaldoEfectivoDetallar).ID = _ucSaldoEfectivoDetallar.ID, String.Empty = _ucSaldoEfectivoDetallar.ID)) Then

            ControlesAdicionadosDetallar.Add(_ucSaldoEfectivoDetallar)

        End If

    End Sub

    Private Sub AnadirTabPageSaldoMPDetallar(divTipoMedioPago As Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa)))

        Dim divisas As ObservableCollection(Of Clases.Divisa) = divTipoMedioPago.Item2.Clonar

        If Modo = Enumeradores.Modo.Alta Then
            Util.ZerarValoresDivisas(divisas)
        End If


        Dim tab As New DevExpress.Web.ASPxTabControl.TabPage

        Select Case divTipoMedioPago.Item1
            Case Enumeradores.TipoMedioPago.Cheque
                tab.Text = Traduzir("055_cheque")
            Case Enumeradores.TipoMedioPago.OtroValor
                tab.Text = Traduzir("055_otrovalor")
            Case Enumeradores.TipoMedioPago.Tarjeta
                tab.Text = Traduzir("055_tarjeta")
            Case Enumeradores.TipoMedioPago.Ticket
                tab.Text = Traduzir("055_ticket")
        End Select

        tab.Name = divTipoMedioPago.Item1.RecuperarValor

        Dim _ucSaldoMedioPagoDetallar As ucSaldoMedioPagoDetallar = LoadControl("~/Controles/ucSaldoMedioPagoDetallar.ascx")

        _ucSaldoMedioPagoDetallar.ID = "ucSaldoMedioPagDetallar_" & tab.Name
        _ucSaldoMedioPagoDetallar.Modo = Modo
        _ucSaldoMedioPagoDetallar.Divisas = New ObservableCollection(Of Clases.Divisa)
        _ucSaldoMedioPagoDetallar.Divisas.AddRange(divisas)
        _ucSaldoMedioPagoDetallar.TipoMedioPago = divTipoMedioPago.Item1.ToString
        _ucSaldoMedioPagoDetallar.TipoValor = Enumeradores.TipoValor.NoDefinido

        tab.Controls.Add(_ucSaldoMedioPagoDetallar)
        pageControlEfectivoMPDetallar.TabPages.Add(tab)

        'Adicionar hiddenfield para guardar valores do controle
        Dim hdftotalesMedioPago As New HiddenField
        hdftotalesMedioPago.ID = pageControlEfectivoMPDetallar.ID & "_ucSaldoMedioPagDetallar_" & tab.Name & "_hdftotalesMedioPago"
        hdftotalesMedioPago.Value = (pageControlEfectivoMPDetallar.TabPages.Count - 1).ToString() + ";" + hdftotalesMedioPago.Value
        pnlUcSaldoMPDetallar.Controls.Add(hdftotalesMedioPago)
        lsthdfTotalesMedioPagoDetallar.Add(hdftotalesMedioPago)

        If Not ControlesAdicionadosDetallar.Exists(Function(e) If(TypeOf e Is ucSaldoMedioPagoDetallar, DirectCast(e, ucSaldoMedioPagoDetallar).ID = _ucSaldoMedioPagoDetallar.ID, String.Empty = _ucSaldoMedioPagoDetallar.ID)) Then

            ControlesAdicionadosDetallar.Add(_ucSaldoMedioPagoDetallar)

        End If

    End Sub

    Private Function RecuperarDivisasPorTipoMedioPago(divisasMedioPago As ObservableCollection(Of Clases.Divisa)) As List(Of Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa)))

        Dim divisasPorTipoMedioPago As New List(Of Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa)))

        Dim divisasPorCheque As New ObservableCollection(Of Clases.Divisa)
        Dim divisasPorOtroValor As New ObservableCollection(Of Clases.Divisa)
        Dim divisasPorTarjeta As New ObservableCollection(Of Clases.Divisa)
        Dim divisasPorTicket As New ObservableCollection(Of Clases.Divisa)

        For Each div In divisasMedioPago

            Dim divisa As Clases.Divisa = div.Clonar

            divisa.Denominaciones = Nothing
            divisa.ValoresTotalesEfectivo = Nothing
            divisa.ValoresTotalesDivisa = Nothing
            divisa.MediosPago = Nothing
            divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)
            divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

            If div.MediosPago IsNot Nothing AndAlso div.MediosPago.Count > 0 Then

                If div.MediosPago.Exists(Function(e) e.Tipo = Enumeradores.TipoMedioPago.Cheque) Then
                    Dim mpCheque = div.MediosPago.Where(Function(r) r.Tipo = Enumeradores.TipoMedioPago.Cheque).ToObservableCollection

                    If divisasPorCheque.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Cheque)) Then
                        Dim divMPCheque = divisasPorCheque.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Cheque))
                        divMPCheque.MediosPago.AddRange(mpCheque)
                    ElseIf divisasPorCheque.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divCheque = divisasPorCheque.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divCheque.MediosPago.AddRange(mpCheque)
                    Else
                        divisa.MediosPago.AddRange(mpCheque)
                        divisasPorCheque.Add(divisa)
                    End If

                End If

                If div.MediosPago.Exists(Function(e) e.Tipo = Enumeradores.TipoMedioPago.OtroValor) Then
                    Dim mpOtroValor = div.MediosPago.Where(Function(r) r.Tipo = Enumeradores.TipoMedioPago.OtroValor).ToObservableCollection

                    If divisasPorOtroValor.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.OtroValor)) Then
                        Dim divOtroValor = divisasPorOtroValor.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.OtroValor))
                        divOtroValor.MediosPago.AddRange(mpOtroValor)
                    ElseIf divisasPorOtroValor.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divOtroValor = divisasPorOtroValor.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divOtroValor.MediosPago.AddRange(mpOtroValor)
                    Else
                        divisa.MediosPago.AddRange(mpOtroValor)
                        divisasPorOtroValor.Add(divisa)
                    End If

                End If

                If div.MediosPago.Exists(Function(e) e.Tipo = Enumeradores.TipoMedioPago.Tarjeta) Then
                    Dim mpTarjeta = div.MediosPago.Where(Function(r) r.Tipo = Enumeradores.TipoMedioPago.Tarjeta).ToObservableCollection

                    If divisasPorTarjeta.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Tarjeta)) Then
                        Dim divTarjeta = divisasPorTarjeta.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Tarjeta))
                        divTarjeta.MediosPago.AddRange(mpTarjeta)
                    ElseIf divisasPorTarjeta.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divTarjeta = divisasPorTarjeta.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divTarjeta.MediosPago.AddRange(mpTarjeta)
                    Else
                        divisa.MediosPago.AddRange(mpTarjeta)
                        divisasPorTarjeta.Add(divisa)
                    End If

                End If

                If div.MediosPago.Exists(Function(e) e.Tipo = Enumeradores.TipoMedioPago.Ticket) Then
                    Dim mpTicket = div.MediosPago.Where(Function(r) r.Tipo = Enumeradores.TipoMedioPago.Ticket).ToObservableCollection

                    If divisasPorTicket.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Ticket)) Then
                        Dim divTicket = divisasPorTicket.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.MediosPago.Exists(Function(mp) mp.Tipo = Enumeradores.TipoMedioPago.Ticket))
                        divTicket.MediosPago.AddRange(mpTicket)
                    ElseIf divisasPorTicket.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divTicket = divisasPorTarjeta.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divTicket.MediosPago.AddRange(mpTicket)
                    Else
                        divisa.MediosPago.AddRange(mpTicket)
                        divisasPorTicket.Add(divisa)
                    End If

                End If

            End If

            If div.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso div.ValoresTotalesTipoMedioPago.Count > 0 Then

                If div.ValoresTotalesTipoMedioPago.Exists(Function(e) e.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque) Then
                    Dim mpCheque = div.ValoresTotalesTipoMedioPago.Where(Function(r) r.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque).ToObservableCollection

                    If divisasPorCheque.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque)) Then
                        Dim divCheque = divisasPorCheque.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Cheque))
                        divCheque.ValoresTotalesTipoMedioPago.AddRange(mpCheque)
                    ElseIf divisasPorCheque.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divCheque = divisasPorCheque.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divCheque.ValoresTotalesTipoMedioPago.AddRange(mpCheque)
                    Else
                        divisa.ValoresTotalesTipoMedioPago.AddRange(mpCheque)
                        divisasPorCheque.Add(divisa)
                    End If

                End If

                If div.ValoresTotalesTipoMedioPago.Exists(Function(e) e.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor) Then
                    Dim mpOtroValor = div.ValoresTotalesTipoMedioPago.Where(Function(r) r.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor).ToObservableCollection

                    If divisasPorOtroValor.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor)) Then
                        Dim divOtroValor = divisasPorOtroValor.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor))
                        divOtroValor.ValoresTotalesTipoMedioPago.AddRange(mpOtroValor)
                    ElseIf divisasPorOtroValor.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divOtroValor = divisasPorOtroValor.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divOtroValor.ValoresTotalesTipoMedioPago.AddRange(mpOtroValor)
                    Else
                        divisa.ValoresTotalesTipoMedioPago.AddRange(mpOtroValor)
                        divisasPorOtroValor.Add(divisa)
                    End If

                End If

                If div.ValoresTotalesTipoMedioPago.Exists(Function(e) e.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta) Then
                    Dim mpTarjeta = div.ValoresTotalesTipoMedioPago.Where(Function(r) r.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta).ToObservableCollection

                    If divisasPorTarjeta.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta)) Then
                        Dim divTarjeta = divisasPorTarjeta.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta))
                        divTarjeta.ValoresTotalesTipoMedioPago.AddRange(mpTarjeta)
                    ElseIf divisasPorTarjeta.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divTarjeta = divisasPorTarjeta.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divTarjeta.ValoresTotalesTipoMedioPago.AddRange(mpTarjeta)
                    Else
                        divisa.ValoresTotalesTipoMedioPago.AddRange(mpTarjeta)
                        divisasPorTarjeta.Add(divisa)
                    End If

                End If

                If div.ValoresTotalesTipoMedioPago.Exists(Function(e) e.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket) Then
                    Dim mpTicket = div.ValoresTotalesTipoMedioPago.Where(Function(r) r.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket).ToObservableCollection

                    If divisasPorTicket.Exists(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket)) Then
                        Dim divTicket = divisasPorTicket.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO AndAlso e.ValoresTotalesTipoMedioPago.Exists(Function(mp) mp.TipoMedioPago = Enumeradores.TipoMedioPago.Ticket))
                        divTicket.ValoresTotalesTipoMedioPago.AddRange(mpTicket)
                    ElseIf divisasPorTicket.Exists(Function(e) e.CodigoISO = div.CodigoISO) Then
                        Dim divTicket = divisasPorTicket.FirstOrDefault(Function(e) e.CodigoISO = div.CodigoISO)
                        divTicket.ValoresTotalesTipoMedioPago.AddRange(mpTicket)
                    Else
                        divisa.ValoresTotalesTipoMedioPago.AddRange(mpTicket)
                        divisasPorTicket.Add(divisa)
                    End If

                End If

            End If

        Next

        If divisasPorCheque.Count > 0 Then
            Util.UnificaItemsDivisas(divisasPorCheque)
            divisasPorCheque.Foreach(Sub(dc)
                                         dc.MediosPago = dc.MediosPago.RemoveAll(Function(mp) mp.Tipo <> Enumeradores.TipoMedioPago.Cheque)
                                     End Sub)
            divisasPorTipoMedioPago.Add(New Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))(Enumeradores.TipoMedioPago.Cheque, divisasPorCheque))
        End If

        If divisasPorOtroValor.Count > 0 Then
            Util.UnificaItemsDivisas(divisasPorOtroValor)
            divisasPorOtroValor.Foreach(Sub(dc)
                                            dc.MediosPago = dc.MediosPago.RemoveAll(Function(mp) mp.Tipo <> Enumeradores.TipoMedioPago.OtroValor)
                                        End Sub)
            divisasPorTipoMedioPago.Add(New Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))(Enumeradores.TipoMedioPago.OtroValor, divisasPorOtroValor))
        End If

        If divisasPorTarjeta.Count > 0 Then
            Util.UnificaItemsDivisas(divisasPorTarjeta)
            divisasPorTarjeta.Foreach(Sub(dc)
                                          dc.MediosPago = dc.MediosPago.RemoveAll(Function(mp) mp.Tipo <> Enumeradores.TipoMedioPago.Tarjeta)
                                      End Sub)
            divisasPorTipoMedioPago.Add(New Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))(Enumeradores.TipoMedioPago.Tarjeta, divisasPorTarjeta))
        End If

        If divisasPorTicket.Count > 0 Then
            Util.UnificaItemsDivisas(divisasPorTicket)
            divisasPorTicket.Foreach(Sub(dc)
                                         dc.MediosPago = dc.MediosPago.RemoveAll(Function(mp) mp.Tipo <> Enumeradores.TipoMedioPago.Ticket)
                                     End Sub)
            divisasPorTipoMedioPago.Add(New Tuple(Of Enumeradores.TipoMedioPago, ObservableCollection(Of Clases.Divisa))(Enumeradores.TipoMedioPago.Ticket, divisasPorTicket))
        End If

        Return divisasPorTipoMedioPago

    End Function


End Class