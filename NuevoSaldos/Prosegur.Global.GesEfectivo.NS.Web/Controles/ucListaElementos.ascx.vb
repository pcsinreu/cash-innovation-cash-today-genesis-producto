Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports System.Collections.ObjectModel

Public Class ucListaElementos
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Private _Elementos As IEnumerable(Of Clases.Elemento) = New ObservableCollection(Of Clases.Elemento)
    Public Property Elementos As IEnumerable(Of Clases.Elemento)
        Get
            Return _Elementos
        End Get
        Set(value As IEnumerable(Of Clases.Elemento))
            _Elementos = value
        End Set
    End Property

    Public Property Modo() As Enumeradores.Modo
    Public Property mensajeVacio As String

    Public Property esGestionBulto As Boolean
    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.Declarado
    Public Property TipoElemento As Enumeradores.TipoElemento

    Public Property ConfiguracionNivelSaldosElementroPadre As Enumeradores.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Ambos

    Private TotalDivisas As New List(Of Tuple(Of Decimal, Clases.Divisa))()

    Public Property ExhibirSeleccion As Boolean = True

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()

        lblSeleccionar.Text = Traduzir("058_Seleccionar")

    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ConfigurarControles()
        ActualizaGrid()
    End Sub

    Private Sub ConfigurarControles()
        dvSeleccionar.Visible = ExhibirSeleccion
    End Sub

    Private Sub rptElementos_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles rptElementos.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaElemento As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaElemento"), HtmlContainerControl)
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim lblTituloFormatoObjeto As Label = DirectCast(e.Item.FindControl("lblTituloFormatoObjeto"), Label)
            Dim lblTituloTipoValor As Label = DirectCast(e.Item.FindControl("lblTituloTipoValor"), Label)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)

            ' Traduz os controles e controla exibição
            Select Case TipoElemento
                Case Enumeradores.TipoElemento.Remesa
                    Dim lblTituloTipoServicio As Label = DirectCast(e.Item.FindControl("lblTituloTipoServicio"), Label)
                    Dim lblTituloCantidad As Label = DirectCast(e.Item.FindControl("lblTituloCantidadObjeto"), Label)
                    lblTituloTipoServicio.Text = Traduzir("058_TituloTipoServicio")
                    lblTituloCantidad.Text = Traduzir("058_TituloCantidadObjeto_" & TipoElemento.ToString())
                Case Enumeradores.TipoElemento.Bulto
                    Dim dvTipoServicio As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTipoServicio"), HtmlContainerControl)
                    Dim dvCantidad As HtmlContainerControl = DirectCast(e.Item.FindControl("dvCantidad"), HtmlContainerControl)
                    dvTipoServicio.Visible = False
                    dvCantidad.Visible = False
            End Select
            lblTituloFormatoObjeto.Text = Traduzir("058_TituloFormatoObjeto")
            lblTituloTipoValor.Text = Traduzir("058_TituloTipoValor_" & TipoValor.ToString())

            If Modo = Enumeradores.Modo.Alta OrElse Modo = Enumeradores.Modo.Modificacion Then
                btnQuitar.AlternateText = Traduzir("058_quitar")
                btnQuitar.Visible = True
            Else
                btnQuitar.Visible = False
            End If

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaElemento.Style.Add("background-color", "#ffffff")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            Else
                dvListaElemento.Style.Add("background-color", "#fffbeb")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim dvTotales As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotales"), HtmlContainerControl)

            dvTotales.InnerText = Traduzir("058_Totales")

        End If
    End Sub

    Private Sub rptElementos_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptElementos.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then

            TotalDivisas.Clear()

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Busca o Elemento
            Dim Elemento As Clases.Elemento = DirectCast(e.Item.DataItem, Clases.Elemento)

            ' Referencia aos objetos da tela
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim lblCodigoBolsa As Label = DirectCast(e.Item.FindControl("lblCodigoBolsa"), Label)
            Dim lblTipoServicio As Label = DirectCast(e.Item.FindControl("lblTipoServicio"), Label)
            Dim lblFormatoObjeto As Label = DirectCast(e.Item.FindControl("lblFormatoObjeto"), Label)
            Dim lblCantidad As Label = DirectCast(e.Item.FindControl("lblCantidad"), Label)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)

            lblCodigoBolsa.Text = ""
            Dim objDivisasElemento As New ObservableCollection(Of Clases.Divisa)

            Select Case True
                Case TypeOf Elemento Is Clases.Contenedor
                    If Elemento.Precintos IsNot Nothing AndAlso Elemento.Precintos.Count > 0 Then
                        btnPrecinto.Text = String.Join(", ", Elemento.Precintos.ToArray())
                    End If

                Case TypeOf Elemento Is Clases.Remesa
                    Dim ElementoRemesa As Clases.Remesa = DirectCast(Elemento, Clases.Remesa)
                    If ElementoRemesa.CodigoExterno IsNot Nothing Then
                        btnPrecinto.Text = ElementoRemesa.CodigoExterno
                    End If
                    lblCantidad.Text = If(ElementoRemesa.Bultos Is Nothing, 0, ElementoRemesa.Bultos.Count)
                    objDivisasElemento = ElementoRemesa.Divisas.Clonar()

                    Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasElemento, TipoValor)
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisasElemento, TipoValor)

                    If objDivisasElemento Is Nothing OrElse objDivisasElemento.Count < 1 Then
                        If ElementoRemesa.Bultos IsNot Nothing AndAlso ElementoRemesa.Bultos.Count > 0 Then
                            For Each b In ElementoRemesa.Bultos
                                '  Divisas Parcial
                                If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then
                                    For Each p In b.Parciales
                                        If p.Divisas IsNot Nothing AndAlso p.Divisas.Count > 0 Then
                                            If objDivisasElemento Is Nothing Then
                                                objDivisasElemento = New ObservableCollection(Of Clases.Divisa)
                                            End If
                                            objDivisasElemento.AddRange(p.Divisas.Clonar())
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                    Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasElemento, TipoValor)
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisasElemento, TipoValor)

                    Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(objDivisasElemento, ElementoRemesa.ConfiguracionNivelSaldos)

                Case TypeOf Elemento Is Clases.Bulto
                    Dim ElementoBulto As Clases.Bulto = DirectCast(Elemento, Clases.Bulto)
                    If Elemento.Precintos IsNot Nothing AndAlso Elemento.Precintos.Count > 0 Then
                        btnPrecinto.Text = String.Join(", ", Elemento.Precintos.ToArray())
                    End If
                    lblCodigoBolsa.Text = ElementoBulto.CodigoBolsa
                    If ElementoBulto.TipoServicio IsNot Nothing Then
                        lblTipoServicio.Text = ElementoBulto.TipoServicio.Descripcion
                    End If
                    If ElementoBulto.TipoFormato IsNot Nothing Then
                        lblFormatoObjeto.Text = ElementoBulto.TipoFormato.Descripcion
                    End If
                    lblCantidad.Text = ElementoBulto.CantidadParciales
                    objDivisasElemento = ElementoBulto.Divisas.Clonar()

                    Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasElemento, TipoValor)
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisasElemento, TipoValor)

                    If objDivisasElemento Is Nothing OrElse objDivisasElemento.Count < 1 Then
                        '  Divisas Parcial
                        If ElementoBulto.Parciales IsNot Nothing AndAlso ElementoBulto.Parciales.Count > 0 Then
                            For Each p In ElementoBulto.Parciales
                                If p.Divisas IsNot Nothing AndAlso p.Divisas.Count > 0 Then
                                    If objDivisasElemento Is Nothing Then
                                        objDivisasElemento = New ObservableCollection(Of Clases.Divisa)
                                    End If
                                    objDivisasElemento.AddRange(p.Divisas.Clonar())
                                End If
                            Next
                        End If
                    End If

                    Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasElemento, TipoValor)
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisasElemento, TipoValor)

                    Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(objDivisasElemento, ElementoBulto.ConfiguracionNivelSaldos)

                Case TypeOf Elemento Is Clases.Parcial
                    Dim ElementoParcial As Clases.Parcial = DirectCast(Elemento, Clases.Parcial)

                    If ElementoParcial.Precintos IsNot Nothing AndAlso ElementoParcial.Precintos.Count > 0 AndAlso Not String.IsNullOrEmpty(ElementoParcial.Precintos(0)) Then
                        btnPrecinto.Text = String.Join(", ", Elemento.Precintos.ToArray())
                    Else
                        btnPrecinto.Text = ElementoParcial.Secuencia.ToString()
                    End If
                    If ElementoParcial.TipoFormato IsNot Nothing Then
                        lblFormatoObjeto.Text = ElementoParcial.TipoFormato.Descripcion
                    End If
                    objDivisasElemento = ElementoParcial.Divisas.Clonar()
                    Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasElemento, TipoValor)
                    Comon.Util.BorrarItemsDivisasDiferentesTipoValor(objDivisasElemento, TipoValor)

                    Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(objDivisasElemento, ConfiguracionNivelSaldosElementroPadre)
            End Select

            Dim divisas As List(Of Tuple(Of Decimal, Clases.Divisa)) = Comon.Util.RetornaTotalImporteDivisas(objDivisasElemento, TipoValor)
            TotalDivisas.AddRange(divisas)
            rptDivisas.DataSource = divisas
            rptDivisas.DataBind()

            btnPrecinto.CommandArgument = e.Item.ItemIndex
            btnQuitar.CommandArgument = e.Item.ItemIndex

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim rptTotalDivisas As Repeater = DirectCast(e.Item.FindControl("rptTotalDivisas"), Repeater)

            rptTotalDivisas.DataSource = (From d In
                                                TotalDivisas
                                            Group d By
                                                d.Item2.Identificador
                                            Into Group
                                            Select
                                                New Tuple(Of Decimal, Clases.Divisa)(
                                                    Group.Sum(Function(p) p.Item1),
                                                    TotalDivisas.Find(Function(t) t.Item2.Identificador = Identificador).Item2)).ToList()
            rptTotalDivisas.DataBind()

        End If
    End Sub

    Protected Sub rptDivisas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim item As Tuple(Of Decimal, Clases.Divisa) = DirectCast(e.Item.DataItem, Tuple(Of Decimal, Clases.Divisa))

            Dim dvDivisa As HtmlContainerControl = DirectCast(e.Item.FindControl("dvDivisa"), HtmlContainerControl)

            Dim cor As String = Drawing.ColorTranslator.ToHtml(item.Item2.Color)
            dvDivisa.InnerText = String.Format("{0} {1:N}", item.Item2.CodigoSimbolo, item.Item1)
            dvDivisa.Style.Add("border", "1px solid " & cor)
            dvDivisa.Style.Add("color", cor)

        End If
    End Sub

    Protected Sub rptTotalDivisas_ItemCreated(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Footer Then

            ' Referencia aos objetos da tela
            Dim lblTituloDivisas As Label = DirectCast(e.Item.FindControl("lblTituloDivisas"), Label)
            Dim lblTituloCantidad As Label = DirectCast(e.Item.FindControl("lblTituloCantidad"), Label)

            ' Traduz os controles
            lblTituloDivisas.Text = Traduzir("058_TituloDivisas")
            lblTituloCantidad.Text = Traduzir("058_TituloCantidadItems")

        End If
    End Sub

    Protected Sub rptTotalDivisas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim item As Tuple(Of Decimal, Clases.Divisa) = DirectCast(e.Item.DataItem, Tuple(Of Decimal, Clases.Divisa))

            Dim dvTotalDivisa As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotalDivisa"), HtmlContainerControl)

            Dim cor As String = Drawing.ColorTranslator.ToHtml(item.Item2.Color)
            dvTotalDivisa.InnerText = String.Format("{0} {1:N};", item.Item2.CodigoSimbolo, item.Item1)
            dvTotalDivisa.Style.Add("color", cor)

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            ' Referencia aos objetos da tela
            Dim lblDivisas As Label = DirectCast(e.Item.FindControl("lblDivisas"), Label)
            Dim lblCantidad As Label = DirectCast(e.Item.FindControl("lblCantidad"), Label)

            Dim listaDivisas As List(Of Tuple(Of Decimal, Clases.Divisa)) = DirectCast(DirectCast(sender, Repeater).DataSource, List(Of Tuple(Of Decimal, Clases.Divisa)))

            If listaDivisas.Count > 0 Then
                lblDivisas.Text = String.Format(" <b>{0}</b>", listaDivisas.Aggregate(Function(resultado, item) New Tuple(Of Decimal, Clases.Divisa)(0, New Clases.Divisa() With {.Descripcion = resultado.Item2.Descripcion & ", " & item.Item2.Descripcion})).Item2.Descripcion)
            End If
            lblCantidad.Text = String.Format(" <b>{0}</b>", Elementos.Count)

        End If
    End Sub

    Protected Sub btnPrecinto_Click(sender As Object, e As System.EventArgs)
        RaiseEvent DetallarElemento(Me, New DetallarElementoEventArgs() With {.Elemento = Elementos.OrderBy(Function(el) String.Join(",", el.Precintos))(DirectCast(sender, Button).CommandArgument)})
    End Sub

    Protected Sub btnQuitar_Click(sender As Object, e As ImageClickEventArgs)
        Dim tipo As Type = Elementos.GetType().GetGenericArguments()(0)
        Dim ElementosOrdenados = Elementos.OrderBy(Function(el) String.Join(",", el.Precintos))
        Select Case True
            Case tipo Is GetType(Clases.Contenedor)
                DirectCast(Elementos, ObservableCollection(Of Clases.Contenedor)).Remove(ElementosOrdenados(Convert.ToInt32(DirectCast(sender, ImageButton).CommandArgument)))
            Case tipo Is GetType(Clases.Remesa)
                DirectCast(Elementos, ObservableCollection(Of Clases.Remesa)).Remove(ElementosOrdenados(Convert.ToInt32(DirectCast(sender, ImageButton).CommandArgument)))
            Case tipo Is GetType(Clases.Bulto)
                DirectCast(Elementos, ObservableCollection(Of Clases.Bulto)).Remove(ElementosOrdenados(Convert.ToInt32(DirectCast(sender, ImageButton).CommandArgument)))
            Case tipo Is GetType(Clases.Parcial)
                DirectCast(Elementos, ObservableCollection(Of Clases.Parcial)).Remove(ElementosOrdenados(Convert.ToInt32(DirectCast(sender, ImageButton).CommandArgument)))
        End Select
        ActualizaGrid()
    End Sub

#End Region

#Region "[DELEGATE]"

    Public Class DetallarElementoEventArgs
        Inherits System.EventArgs
        Public Property Elemento As Clases.Elemento
        Public Property identificadorContenedor As String
        Public Property identificadorRemesa As String
        Public Property identificadorBulto As String
        Public Property identificadorParcial As String
    End Class

    Public Event DetallarElemento(sender As Object, e As DetallarElementoEventArgs)

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
        rptElementos.DataSource = Elementos.OrderBy(Function(e) String.Join(",", e.Precintos))
        rptElementos.DataBind()

        If Elementos IsNot Nothing AndAlso Elementos.Count > 0 Then
            lblmensajeVacio.Visible = False
            rptElementos.Visible = True
            dvSeleccionar.Visible = ExhibirSeleccion AndAlso Modo <> Enumeradores.Modo.Consulta
        Else
            lblmensajeVacio.Text = mensajeVacio
            lblmensajeVacio.Visible = True
            rptElementos.Visible = False
            dvSeleccionar.Visible = False
        End If

    End Sub

#End Region

End Class