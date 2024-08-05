Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucElementosSelecionar
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Modo() As Enumeradores.Modo
    Public Property mensajeVacio As String
    Public Property esResultadoFiltro As Boolean = False
    Public Property esGestionBulto As Boolean

    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.Declarado

    Private _Elementos As IEnumerable(Of Clases.Elemento) = New ObservableCollection(Of Clases.Elemento)
    Public Property Elementos As IEnumerable(Of Clases.Elemento)
        Get
            Return _Elementos
        End Get
        Set(value As IEnumerable(Of Clases.Elemento))
            _Elementos = value
        End Set
    End Property

    Public Property ExhibirSeleccion As Boolean = True


#End Region

#Region "[DELEGATE]"

    Public Class ElementoEventArgs
        Public Property Elementos As New ObservableCollection(Of Clases.Elemento)
        Public Property ItemIdex As Int32
    End Class

    Public Event Agregar(sender As Object, e As ElementoEventArgs)
    Public Event DetallarPrecinto(sender As Object, e As ElementoEventArgs)
    Public Event DetallarPrecintoPadre(sender As Object, e As ElementoEventArgs)

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()

        ConfigurarControles()
        ActualizaGrid()

        If Request.Params("__EVENTTARGET") = "agregar_elemento" Then
            agregarElemento()
        End If

    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
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

    Private Sub rptElementos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptElementos.ItemCreated
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaElementos As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaElementos"), HtmlContainerControl)
            Dim btnAgregar As ImageButton = DirectCast(e.Item.FindControl("btnAgregar"), ImageButton)
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim btnPrecintoPadre As Button = DirectCast(e.Item.FindControl("btnPrecintoPadre"), Button)

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaElementos.Style.Add("background-color", "#ffffff")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnPrecintoPadre.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnAgregar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            Else
                dvListaElementos.Style.Add("background-color", "#fffbeb")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnPrecintoPadre.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnAgregar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
            End If
        End If
    End Sub

    Private Sub rptElementos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptElementos.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Busca o documento
            Dim objElemento As Clases.Elemento = DirectCast(e.Item.DataItem, Clases.Elemento)
            Dim objRemesa As Clases.Remesa = Nothing
            Dim objBulto As Clases.Bulto = Nothing

            ' Referencia aos objetos da tela
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim btnPrecintoPadre As Button = DirectCast(e.Item.FindControl("btnPrecintoPadre"), Button)
            Dim btnAgregar As ImageButton = DirectCast(e.Item.FindControl("btnAgregar"), ImageButton)
            Dim lblDivisas As Label = DirectCast(e.Item.FindControl("lblDivisas"), Label)
            Dim lblCanal As Label = DirectCast(e.Item.FindControl("lblCanal"), Label)
            Dim lblCliente As Label = DirectCast(e.Item.FindControl("lblCliente"), Label)
            Dim lblSector As Label = DirectCast(e.Item.FindControl("lblSector"), Label)
            Dim litPrecintoPadre As Literal = DirectCast(e.Item.FindControl("litPrecintoPadre"), Literal)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)
            Dim lblObjetos As Label = DirectCast(e.Item.FindControl("lblObjetos"), Label)
            Dim rptObjetos As Repeater = DirectCast(e.Item.FindControl("rptObjetos"), Repeater)
            Dim dvObjetos As HtmlContainerControl = DirectCast(e.Item.FindControl("dvObjetos"), HtmlContainerControl)
            Dim imgHistorico As ImageButton = DirectCast(e.Item.FindControl("imgHistorico"), ImageButton)

            'Recuperar o documento do elemento
            Dim divisasElemento As New ObservableCollection(Of Clases.Divisa)

            litPrecintoPadre.Text = "<span style='font-size:8px;'><br /></span>" & String.Format("{0}: ", Traduzir("059_remesa"))
            lblDivisas.Text = String.Format("{0}: ", Traduzir("059_valores"))
            btnAgregar.CommandArgument = e.Item.ItemIndex

            If TypeOf objElemento Is Clases.Remesa AndAlso Not esGestionBulto Then

                objRemesa = DirectCast(objElemento, Clases.Remesa)

                btnPrecinto.Text = objRemesa.CodigoExterno
                btnPrecinto.CommandArgument = e.Item.ItemIndex

                litPrecintoPadre.Visible = False
                btnPrecintoPadre.Visible = False
                btnPrecinto.Style.Add("padding-top", "10px")

                lblCliente.Text = String.Format("{0}<br/>{1}", Traduzir("059_cliente"), Aplicacao.Util.Utilidad.PreencheCliente(objRemesa.Cuenta))
                lblCanal.Text = String.Format("{0}<br/>{1}", Traduzir("059_canal"), Aplicacao.Util.Utilidad.PreencheCanal(objRemesa.Cuenta))
                lblSector.Text = String.Format("{0}<br/>{1}", Traduzir("059_sector"), If(objRemesa.Cuenta IsNot Nothing AndAlso objRemesa.Cuenta.Sector IsNot Nothing,
                                                                                         objRemesa.Cuenta.Sector.Descripcion, String.Empty))

                If objRemesa.Bultos IsNot Nothing Then

                    For Each b In objRemesa.Bultos
                        b.PrecintosRemesa.Add(objRemesa.CodigoExterno)
                    Next

                    lblObjetos.Text = Traduzir("016_TituloBultos")

                    rptObjetos.DataSource = objRemesa.Bultos
                    rptObjetos.DataBind()

                End If

                divisasElemento = objRemesa.Divisas
                Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(divisasElemento, objRemesa.ConfiguracionNivelSaldos)
                imgHistorico.CommandArgument = e.Item.ItemIndex
            Else

                If TypeOf objElemento Is Clases.Remesa Then
                    objRemesa = DirectCast(objElemento, Clases.Remesa)
                    objBulto = objRemesa.Bultos.FirstOrDefault()
                Else
                    objBulto = DirectCast(objElemento, Clases.Bulto)
                End If

                btnPrecinto.Text = objBulto.Precintos.FirstOrDefault()
                btnPrecinto.CommandArgument = e.Item.ItemIndex
                litPrecintoPadre.Visible = True
                btnPrecintoPadre.Visible = True
                btnPrecintoPadre.Text = objRemesa.CodigoExterno
                btnPrecintoPadre.CommandArgument = e.Item.ItemIndex
                dvObjetos.Visible = False

                lblCliente.Text = String.Format("{0}<br/>{1}", Traduzir("059_cliente"), Aplicacao.Util.Utilidad.PreencheCliente(objBulto.Cuenta))
                lblCanal.Text = String.Format("{0}<br/>{1}", Traduzir("059_canal"), Aplicacao.Util.Utilidad.PreencheCanal(objBulto.Cuenta))
                lblSector.Text = String.Format("{0}<br/>{1}", Traduzir("059_sector"), If(objBulto.Cuenta IsNot Nothing AndAlso objBulto.Cuenta.Sector IsNot Nothing,
                                                                                         objBulto.Cuenta.Sector.Descripcion, String.Empty))

                divisasElemento = objBulto.Divisas
                Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(divisasElemento, objBulto.ConfiguracionNivelSaldos)
                imgHistorico.CommandArgument = e.Item.ItemIndex

            End If

            Dim divisas As List(Of Tuple(Of Decimal, Clases.Divisa)) = Util.RetornaTotalImporteDivisas(divisasElemento, TipoValor)
            rptDivisas.DataSource = divisas
            rptDivisas.DataBind()

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

    Protected Sub rptObjetos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim objElemento As Clases.Elemento = DirectCast(e.Item.DataItem, Clases.Elemento)
            Dim objBulto = DirectCast(objElemento, Clases.Bulto)

            Dim litObjetos As Literal = DirectCast(e.Item.FindControl("litObjetos"), Literal)
            litObjetos.Text = "<div class='disponibleOff'>" & objBulto.Precintos.FirstOrDefault() & "<input type='hidden' value='" & objBulto.PrecintosRemesa.FirstOrDefault() & "' name='" & objBulto.Precintos.FirstOrDefault() & "' /></div>"
        End If
    End Sub

    Protected Sub btnPrecinto_Click(sender As Object, e As System.EventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, Button)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elementos.Add(Me.Elementos(itemIdex))
        args.ItemIdex = itemIdex
        RaiseEvent DetallarPrecinto(Me, args)
    End Sub

    Protected Sub imgHistorico_Click(sender As Object, e As System.EventArgs)
        Dim btn = DirectCast(sender, ImageButton)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        DirectCast(Page, Base).AbrirPopup("~/Pantallas/Consultas/HistoricoElemento.aspx", "esGestionBulto=" & esGestionBulto & "&idElemento=" & Me.Elementos(itemIdex).Identificador)
    End Sub

    Protected Sub btnPrecintoPadre_Click(sender As Object, e As System.EventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, Button)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elementos.Add(Me.Elementos(itemIdex))
        args.ItemIdex = itemIdex
        RaiseEvent DetallarPrecintoPadre(Me, args)
    End Sub

    Protected Sub btnAgregar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, ImageButton)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elementos.Add(Me.Elementos(itemIdex))
        args.ItemIdex = itemIdex
        RaiseEvent Agregar(Me, args)
    End Sub

    Private Sub btnAgregarTodos_Click(sender As Object, e As System.EventArgs) Handles btnAgregarTodos.Click
        Dim args As New ElementoEventArgs()
        args.Elementos = Me.Elementos
        RaiseEvent Agregar(Me, args)
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConfigurarControles()
        dvSeleccionar.Visible = ExhibirSeleccion
    End Sub

    Public Sub ActualizaGrid()
        Me.rptElementos.DataSource = Me.Elementos
        Me.rptElementos.DataBind()

        If Elementos IsNot Nothing AndAlso Elementos.Count > 0 Then
            lblmensajeVacio.Visible = False
            rptElementos.Visible = True
            dvSeleccionar.Visible = ExhibirSeleccion
            Me.lblSeleccionar.Text = Traduzir("059_lblSelecionar")
            dvSeleccionarTodos.Visible = ExhibirSeleccion
            btnAgregarTodos.Text = Traduzir("016_AgregarTodos")
        Else
            lblmensajeVacio.Text = mensajeVacio
            lblmensajeVacio.Visible = True
            rptElementos.Visible = False
            dvSeleccionar.Visible = False
            dvSeleccionarTodos.Visible = False
        End If
    End Sub

    Protected Sub agregarElemento()
        'Verifica se o valor informado existe na lista de elementos.
        Try
            Dim valor = Request.Params("__EVENTARGUMENT")
            If Not String.IsNullOrEmpty(valor) Then

                Dim elemento As Clases.Elemento = Nothing
                'Recupera pelo precinto do elmento.
                If Me.esGestionBulto Then
                    elemento = Me.Elementos.Where(Function(r) DirectCast(r, Clases.Remesa).Bultos.Any(Function(b) b.Precintos IsNot Nothing AndAlso b.Precintos.Contains(valor))).FirstOrDefault
                Else
                    elemento = Me.Elementos.Where(Function(r) DirectCast(r, Clases.Remesa).CodigoExterno = valor).FirstOrDefault
                End If

                If elemento IsNot Nothing Then
                    Dim args As New ElementoEventArgs()
                    args.Elementos.Add(elemento)
                    RaiseEvent Agregar(Me, args)
                End If
            End If

            txtSeleccionar.Text = String.Empty
            txtSeleccionar.Focus()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

#End Region

End Class