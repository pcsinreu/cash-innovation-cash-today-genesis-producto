Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

Public Class ucListaValores
    Inherits UcBase

#Region "[PROPRIEDADES]"


    Private _IdentificadorSector As String = Nothing
    Public ReadOnly Property IdentificadorSector() As String
        Get
            If String.IsNullOrEmpty(_IdentificadorSector) Then
                _IdentificadorSector = Request.QueryString("IdentificadorSector")


            End If
            Return _IdentificadorSector
        End Get
    End Property


    Private _SectorSelecionado As Clases.Sector = Nothing
    Public ReadOnly Property SectorSelecionado() As Clases.Sector
        Get
            If _SectorSelecionado Is Nothing Then
                _SectorSelecionado = Prosegur.Genesis.LogicaNegocio.Genesis.Sector.ObtenerPorOid(IdentificadorSector)
            End If
            Return _SectorSelecionado
        End Get
    End Property


    Public Property Titulo As String
    
    Private _SaldoCuentas As New ObservableCollection(Of Clases.SaldoCuenta)
    Public Property SaldoCuentas As ObservableCollection(Of Clases.SaldoCuenta)
        Get
            If _SaldoCuentas Is Nothing Then
                _SaldoCuentas = New ObservableCollection(Of Clases.SaldoCuenta)
            End If
            Return _SaldoCuentas
        End Get
        Set(value As ObservableCollection(Of Clases.SaldoCuenta))
            _SaldoCuentas = value
        End Set
    End Property


    Public Property Modo() As Enumeradores.Modo
    Public Property mensajeVacio As String
    Public Property esResultadoFiltro As Boolean = False
    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.NoDefinido

    'Private TotalDivisas As New List(Of Tuple(Of Decimal, Clases.Divisa))()

    Private objDivisasElemento As New ObservableCollection(Of Totales)

    Public Property ExhibirSeleccion As Boolean = True

    Public identificadorSectorActual As String

    Public Property ExcluirSectoresHijos As Boolean

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()

        If String.IsNullOrEmpty(Titulo) Then
            dvTitulo.Style.Item("display") = "none"
        End If
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTituloValores.Text = Titulo

        If esResultadoFiltro Then
            btnAgregarTodos.Text = Traduzir("016_AgregarTodos")
        Else
            btnAgregarTodos.Text = Traduzir("016_QuitarTodos")
        End If

    End Sub

    ''' <summary>
    ''' Método que valida os campos obrigatorios do controle.
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Function ValidarControl() As List(Of String)
        Dim retorno As New List(Of String)
        Return retorno
    End Function

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento ItemCreated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptSaldoCuenta_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptValores.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaValores As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaValores"), HtmlContainerControl)
            Dim btnAgregar As ImageButton = DirectCast(e.Item.FindControl("btnAgregar"), ImageButton)
            Dim btnDetalles As ImageButton = DirectCast(e.Item.FindControl("btnDetalles"), ImageButton)
            Dim btnSaldoCuenta As ImageButton = DirectCast(e.Item.FindControl("btnSaldoCuenta"), ImageButton)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)
            Dim btnSaldoHijos As ImageButton = DirectCast(e.Item.FindControl("btnSaldoHijos"), ImageButton)

            If (Modo = Enumeradores.Modo.Alta OrElse Modo = Enumeradores.Modo.Modificacion) AndAlso Not esResultadoFiltro Then
                btnQuitar.AlternateText = Traduzir("058_quitar")
                btnQuitar.Visible = True
            Else
                btnQuitar.Visible = False
            End If

            If esResultadoFiltro Then
                btnSaldoCuenta.Visible = False
            Else
                btnAgregar.Visible = False

                ' Enquanto não for definido corretamente a funcionalidade, o botão ficara oculto.
                btnSaldoCuenta.Visible = False
                btnDetalles.Style.Item("margin-left") = "15px;"

            End If

            btnSaldoHijos.Visible = Not Me.ExcluirSectoresHijos

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaValores.Style.Add("background-color", "#ffffff")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnSaldoCuenta.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnSaldoHijos.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
            Else
                dvListaValores.Style.Add("background-color", "#fffbeb")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnSaldoCuenta.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnSaldoHijos.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
            End If



        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim dvTotales As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotales"), HtmlContainerControl)

            dvTotales.InnerText = Traduzir("058_Totales")

        End If

    End Sub

    ''' <summary>
    ''' Evento ItemDataBound
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptSaldoCuenta_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptValores.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then

            objDivisasElemento.Clear()

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Busca o Elemento
            Dim Valores As Clases.SaldoCuenta = DirectCast(e.Item.DataItem, Clases.SaldoCuenta)

            ' Referencia aos objetos da tela
            Dim btnAgregar As ImageButton = DirectCast(e.Item.FindControl("btnAgregar"), ImageButton)
            Dim btnDetalles As ImageButton = DirectCast(e.Item.FindControl("btnDetalles"), ImageButton)
            Dim btnSaldoCuenta As ImageButton = DirectCast(e.Item.FindControl("btnSaldoCuenta"), ImageButton)
            Dim lblCuenta As Label = DirectCast(e.Item.FindControl("lblCuenta"), Label)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)
            Dim btnSaldoHijos As ImageButton = DirectCast(e.Item.FindControl("btnSaldoHijos"), ImageButton)
            Dim rptComprobante As Repeater = DirectCast(e.Item.FindControl("rptComprobante"), Repeater)

            lblCuenta.Text = Aplicacao.Util.Utilidad.PreencheCuenta(Valores.Cuenta, False)

            Dim objTotales As New ObservableCollection(Of Totales)

            For Each div In Valores.Divisas.Clonar()

                If div.ValoresTotales IsNot Nothing AndAlso div.ValoresTotales.Count > 0 Then
                    If objTotales.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor) Is Nothing Then

                        objTotales.Add(New Totales With {.CodigoSimbolo = div.CodigoSimbolo,
                                                         .ColorTotales = div.Color,
                                                         .Descripcion = div.Descripcion,
                                                         .IdentificadorDivisas = div.Identificador,
                                                         .Importe = div.ValoresTotales(0).Importe,
                                                         .TipoValor = div.ValoresTotales(0).TipoValor})
                    Else
                        objTotales.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor).Importe += div.ValoresTotales(0).Importe
                    End If

                    If objDivisasElemento.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor) Is Nothing Then

                        objDivisasElemento.Add(New Totales With {.CodigoSimbolo = div.CodigoSimbolo,
                                                         .ColorTotales = div.Color,
                                                         .Descripcion = div.Descripcion,
                                                         .IdentificadorDivisas = div.Identificador,
                                                         .Importe = div.ValoresTotales(0).Importe,
                                                         .TipoValor = div.ValoresTotales(0).TipoValor})
                    Else
                        objDivisasElemento.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor).Importe += div.ValoresTotales(0).Importe
                    End If

                End If
            Next

            rptDivisas.DataSource = objTotales

            rptDivisas.DataBind()

            btnAgregar.CommandArgument = e.Item.ItemIndex
            btnDetalles.CommandArgument = e.Item.ItemIndex
            btnQuitar.CommandArgument = e.Item.ItemIndex
            btnSaldoCuenta.CommandArgument = e.Item.ItemIndex
            btnSaldoHijos.CommandArgument = e.Item.ItemIndex

            If Valores.CodigosComprobantes Is Nothing OrElse Valores.CodigosComprobantes.Count = 0 Then
                btnDetalles.Visible = True
            Else
                btnDetalles.Visible = False
            End If

            If Valores.CodigosComprobantes IsNot Nothing AndAlso Valores.CodigosComprobantes.Count > 0 Then
                rptComprobante.DataSource = Valores.SaldoCuentaDetalle
                rptComprobante.DataBind()
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim rptTotalDivisas As Repeater = DirectCast(e.Item.FindControl("rptTotalDivisas"), Repeater)

            rptTotalDivisas.DataSource = objDivisasElemento
            rptTotalDivisas.DataBind()

        End If

    End Sub

    Protected Sub rptDivisas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim item As Totales = DirectCast(e.Item.DataItem, Totales)

            Dim dvDivisa As HtmlContainerControl = DirectCast(e.Item.FindControl("dvDivisa"), HtmlContainerControl)

            Dim cor As String = Drawing.ColorTranslator.ToHtml(item.ColorTotales)
            dvDivisa.InnerText = String.Format("{0} {1:N}", item.CodigoSimbolo, item.Importe)
            dvDivisa.Style.Add("border", "1px solid " & cor)
            dvDivisa.Style.Add("color", cor)

        End If
    End Sub

    Protected Sub rptTotalDivisas_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Footer Then
            Dim lblTituloDivisas As Label = DirectCast(e.Item.FindControl("lblTituloDivisas"), Label)
            lblTituloDivisas.Text = Traduzir("058_TituloDivisas")
        End If
    End Sub

    Protected Sub rptTotalDivisas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim item As Totales = DirectCast(e.Item.DataItem, Totales)

            Dim dvTotalDivisa As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotalDivisa"), HtmlContainerControl)

            Dim cor As String = Drawing.ColorTranslator.ToHtml(item.ColorTotales)
            dvTotalDivisa.InnerText = String.Format("{0} {1:N};", item.CodigoSimbolo, item.Importe)
            dvTotalDivisa.Style.Add("color", cor)

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            ' Referencia aos objetos da tela
            Dim lblDivisas As Label = DirectCast(e.Item.FindControl("lblDivisas"), Label)

            Dim listaDivisas As ObservableCollection(Of Totales) = DirectCast(DirectCast(sender, Repeater).DataSource, ObservableCollection(Of Totales))

            If listaDivisas.Count > 0 Then
                lblDivisas.Text = String.Format(" <b>{0}</b>", listaDivisas.Aggregate(Function(resultado, item)
                                                                                          resultado.Descripcion &= ", " & item.Descripcion
                                                                                          Return resultado
                                                                                      End Function).Descripcion)
            End If

        End If
    End Sub

    Protected Sub btnQuitar_Click(sender As Object, e As ImageClickEventArgs)
        Dim args As New DetallarValoresEventArgs()
        args.SaldoCuentas.Add(SaldoCuentas(DirectCast(sender, ImageButton).CommandArgument))
        RaiseEvent QuitarValores(Me, args)
    End Sub

    Protected Sub btnDetalles_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New DetallarValoresEventArgs()

        Dim objSaldoCuenta As Comon.Clases.SaldoCuenta = SaldoCuentas(DirectCast(sender, ImageButton).CommandArgument)

        If objSaldoCuenta IsNot Nothing Then
            objSaldoCuenta = objSaldoCuenta.Clonar
            objSaldoCuenta.Cuenta.Sector = SectorSelecionado
            'Me.Attributes("identificadorSector").ToString()
            args.SaldoCuentas.Add(objSaldoCuenta)
            RaiseEvent DetallarValores(Me, args)
        End If

    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New DetallarValoresEventArgs()
        args.SaldoCuentas.Add(SaldoCuentas(DirectCast(sender, ImageButton).CommandArgument))
        RaiseEvent AgregarValores(Me, args)
    End Sub

    Protected Sub btnSaldoCuenta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New DetallarValoresEventArgs()
        args.SaldoCuentas.Add(SaldoCuentas(DirectCast(sender, ImageButton).CommandArgument))
        RaiseEvent SaldoCuenta(Me, args)
    End Sub

    Private Sub btnAgregarTodos_Click(sender As Object, e As System.EventArgs) Handles btnAgregarTodos.Click
        Dim args As New DetallarValoresEventArgs()
        args.SaldoCuentas = SaldoCuentas
        If esResultadoFiltro Then
            RaiseEvent AgregarValores(Me, args)
        Else
            RaiseEvent QuitarValores(Me, args)
        End If
    End Sub

    Protected Sub btnSaldoHijos_Click(sender As Object, e As ImageClickEventArgs)
        Dim args As New DetallarValoresEventArgs()
        Dim objSaldoCuenta As Comon.Clases.SaldoCuenta = SaldoCuentas(DirectCast(sender, ImageButton).CommandArgument)

        If objSaldoCuenta IsNot Nothing Then
            objSaldoCuenta = objSaldoCuenta.Clonar
            objSaldoCuenta.Cuenta.Sector = SectorSelecionado
            'Me.Attributes("identificadorSector").ToString()
            args.SaldoCuentas.Add(objSaldoCuenta)
            RaiseEvent SaldoHijos(Me, args)
        End If

    End Sub
#End Region

#Region "[DELEGATE]"

    Public Class DetallarValoresEventArgs
        Public Property SaldoCuentas As New ObservableCollection(Of Clases.SaldoCuenta)
        Public Property IdentificadorCuenta As String
    End Class

    Public Event AgregarValores(sender As Object, e As DetallarValoresEventArgs)

    Public Event DetallarValores(sender As Object, e As DetallarValoresEventArgs)

    Public Event QuitarValores(sender As Object, e As DetallarValoresEventArgs)

    Public Event SaldoCuenta(sender As Object, e As DetallarValoresEventArgs)

    Public Event SaldoHijos(sender As Object, e As DetallarValoresEventArgs)

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

    Public Sub ActualizaGrid()
        SaldoCuentas = SaldoCuentas.OrderBy(Function(c As Clases.SaldoCuenta) c.Cuenta.Cliente.Codigo). _
        ThenBy(Function(c As Clases.SaldoCuenta) c.Cuenta.Sector.Descripcion).ToObservableCollection()

        rptValores.DataSource = SaldoCuentas
        rptValores.DataBind()

        If SaldoCuentas IsNot Nothing AndAlso SaldoCuentas.Count > 0 Then
            lblmensajeVacio.Text = String.Empty
            rptValores.Visible = True
            dvSeleccionarTodos.Visible = ExhibirSeleccion
        Else
            lblmensajeVacio.Text = mensajeVacio
            rptValores.Visible = False
            dvSeleccionarTodos.Visible = False
        End If
    End Sub

#End Region

    Public Class Totales
        Inherits Clases.ImporteTotal

        Public IdentificadorDivisas As String
        Public CodigoSimbolo As String
        Public Descripcion As String
        Public ColorTotales As Drawing.Color

    End Class

    Protected Sub rptComprobante_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim linkComprobante As LinkButton = DirectCast(e.Item.FindControl("linkComprobante"), LinkButton)
            Dim objSaldoCuentaDetalle As Comon.Clases.SaldoCuentaDetalle = DirectCast(e.Item.DataItem, Comon.Clases.SaldoCuentaDetalle)
            If objSaldoCuentaDetalle IsNot Nothing AndAlso objSaldoCuentaDetalle.Cuenta IsNot Nothing AndAlso objSaldoCuentaDetalle.Cuenta.Sector IsNot Nothing Then
                linkComprobante.Text = objSaldoCuentaDetalle.Cuenta.Sector.Descripcion
                linkComprobante.CommandArgument = objSaldoCuentaDetalle.Cuenta.Sector.Descripcion
                linkComprobante.ID = objSaldoCuentaDetalle.Cuenta.Identificador
            End If
            
        End If
    End Sub

    Protected Sub linkComprobante_Command(sender As Object, e As CommandEventArgs)
        Dim args As New DetallarValoresEventArgs()
        Dim lnkComprobante As LinkButton = DirectCast(sender, LinkButton)
        Dim comprobante As String = lnkComprobante.CommandArgument
        Dim IdentificadorCuenta As String = lnkComprobante.ID
        args.SaldoCuentas = New ObservableCollection(Of Clases.SaldoCuenta)
        args.SaldoCuentas.Add(Me.SaldoCuentas.Where(Function(s) s.SaldoCuentaDetalle.Exists(Function(sc) sc.Cuenta.Identificador = IdentificadorCuenta)).FirstOrDefault)
        args.IdentificadorCuenta = IdentificadorCuenta
        RaiseEvent DetallarValores(Me, args)
    End Sub
End Class