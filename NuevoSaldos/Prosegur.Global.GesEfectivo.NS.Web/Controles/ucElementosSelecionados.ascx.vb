Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucElementosSelecionados
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Titulo As String
    Public Property Modo() As Enumeradores.Modo
    Public Property mensajeVacio As String
    Public Property esResultadoFiltro As Boolean = False
    Public Property esGestionBulto As Boolean
    Public Property Elementos As New ObservableCollection(Of Clases.Elemento)
    Public Property TipoElemento As Enumeradores.TipoElemento

    Private TotalDivisas As New List(Of Tuple(Of Decimal, Clases.Divisa))
    Public Property ExhibirSeleccion As Boolean = True

    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.Declarado

#End Region

#Region "[DELEGATE]"

    Public Class ElementoEventArgs
        Public Property Elemento As New Clases.Elemento
        Public ItemIdex As Int32
    End Class

    Public Event Quitar(sender As Object, e As ElementoEventArgs)
    Public Event DetallarPrecinto(sender As Object, e As ElementoEventArgs)
    Public Event DetallarSaldoCuenta(sender As Object, e As ElementoEventArgs)

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Metodo Inicializar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()

        If Request.Params("__EVENTTARGET") = "remover_elemento" Then
            removerElemento()
        End If

        If String.IsNullOrEmpty(Titulo) Then
            dvTitulo.Style.Item("display") = "none"
        End If

        'ConfigurarControles()
        'ActualizaGrid()

    End Sub

    ''' <summary>
    ''' Método que permite o desenvolvedor traduzir os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTitulo.Text = Titulo
        lblSeleccionar.Text = Traduzir("059_lblEliminar")
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

    Protected Sub btnPrecinto_Click(sender As Object, e As System.EventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, Button)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elemento = Me.Elementos(itemIdex)
        args.ItemIdex = itemIdex
        RaiseEvent DetallarPrecinto(Me, args)
    End Sub

    Protected Sub btnQuitar_Click(sender As Object, e As System.EventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, ImageButton)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elemento = Me.Elementos(itemIdex)
        args.ItemIdex = itemIdex
        RaiseEvent Quitar(Me, args)
    End Sub

    Protected Sub btnSaldoCuenta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New ElementoEventArgs()
        Dim btn = DirectCast(sender, ImageButton)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        args.Elemento = Me.Elementos(itemIdex)
        args.ItemIdex = itemIdex
        RaiseEvent DetallarSaldoCuenta(Me, args)
    End Sub

    Protected Sub imgHistorico_Click(sender As Object, e As System.EventArgs)
        Dim btn = DirectCast(sender, ImageButton)
        Dim itemIdex = Convert.ToInt32(btn.CommandArgument)
        DirectCast(Page, Base).AbrirPopup("~/Pantallas/Consultas/HistoricoElemento.aspx", "esGestionBulto=" & esGestionBulto & "&idElemento=" & Me.Elementos(itemIdex).Identificador)
    End Sub

    Private Sub rptElementos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptElementos.ItemCreated

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaElementos As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaElementos"), HtmlContainerControl)
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)
            Dim btnSaldoCuenta As ImageButton = DirectCast(e.Item.FindControl("btnSaldoCuenta"), ImageButton)

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaElementos.Style.Add("background-color", "#ffffff")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnSaldoCuenta.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            Else
                dvListaElementos.Style.Add("background-color", "#fffbeb")
                btnPrecinto.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnSaldoCuenta.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            End If

            If Modo = Enumeradores.Modo.Consulta Then
                btnQuitar.Visible = False
            End If

        End If
    End Sub

    Private Sub rptElementos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptElementos.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then

            TotalDivisas.Clear()

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Busca o documento
            Dim objElemento As Clases.Elemento = DirectCast(e.Item.DataItem, Clases.Elemento)
            Dim objRemesa As Clases.Remesa = Nothing

            ' Referencia aos objetos da tela
            Dim btnSaldoCuenta As ImageButton = DirectCast(e.Item.FindControl("btnSaldoCuenta"), ImageButton)
            Dim btnPrecinto As Button = DirectCast(e.Item.FindControl("btnPrecinto"), Button)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)
            Dim dvBultos As HtmlContainerControl = DirectCast(e.Item.FindControl("dvBultos"), HtmlContainerControl)
            Dim dvCuenta As HtmlContainerControl = DirectCast(e.Item.FindControl("dvCuenta"), HtmlContainerControl)
            Dim dvSaldoCuenta As HtmlContainerControl = DirectCast(e.Item.FindControl("dvSaldoCuenta"), HtmlContainerControl)
            Dim lblCodigo As Label = DirectCast(e.Item.FindControl("lblCodigo"), Label)
            Dim lblCuenta As Label = DirectCast(e.Item.FindControl("lblCuenta"), Label)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)
            Dim lblDivisas As Label = DirectCast(e.Item.FindControl("lblDivisas"), Label)
            Dim rptObjetos As Repeater = DirectCast(e.Item.FindControl("rptObjetos"), Repeater)
            Dim imgHistorico As ImageButton = DirectCast(e.Item.FindControl("imgHistorico"), ImageButton)
            Dim lblCanal As Label = DirectCast(e.Item.FindControl("lblCanal"), Label)
            Dim lblCliente As Label = DirectCast(e.Item.FindControl("lblCliente"), Label)
            Dim lblSector As Label = DirectCast(e.Item.FindControl("lblSector"), Label)

            'Recuperar o documento do elemento
            Dim objDivisasRemesa As New ObservableCollection(Of Clases.Divisa)
            Dim objDivisasBultos As New ObservableCollection(Of Clases.Divisa)
            Dim objDivisasParcial As New ObservableCollection(Of Clases.Divisa)
            Dim objDivisasElemento As New ObservableCollection(Of Clases.Divisa)
            objRemesa = DirectCast(objElemento, Clases.Remesa)

            dvSaldoCuenta.Style.Item("display") = "none"

            lblCuenta.Text = Traduzir("016_TituloBultos")
            lblDivisas.Text = String.Format("{0}: ", Traduzir("059_valores"))

            ' Copia Divisas por Nivel
            '   Divisas Remesa
            If objRemesa.Divisas IsNot Nothing AndAlso objRemesa.Divisas.Count > 0 Then
                objDivisasRemesa = objRemesa.Divisas.Clonar()
                Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(objDivisasRemesa, objRemesa.ConfiguracionNivelSaldos)
            End If

            '   Divisas Bultos
            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                For Each b In objRemesa.Bultos
                    If b.Divisas IsNot Nothing AndAlso b.Divisas.Count > 0 Then

                        Dim bDivisas As ObservableCollection(Of Clases.Divisa) = b.Divisas.Clonar()
                        Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(bDivisas, b.ConfiguracionNivelSaldos)
                        objDivisasBultos.AddRange(bDivisas)

                    End If

                    '  Divisas Parcial
                    If b.Parciales IsNot Nothing AndAlso b.Parciales.Count > 0 Then
                        For Each p In b.Parciales
                            If p.Divisas IsNot Nothing AndAlso p.Divisas.Count > 0 Then

                                Dim pDivisas As ObservableCollection(Of Clases.Divisa) = p.Divisas.Clonar()
                                Prosegur.Genesis.LogicaNegocio.Genesis.Divisas.configuracionNivelSaldos(pDivisas, b.ConfiguracionNivelSaldos)
                                objDivisasParcial.AddRange(pDivisas)

                            End If
                        Next
                    End If
                Next
            End If

            Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasRemesa, TipoValor)
            Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasBultos, TipoValor)
            Aplicacao.Util.Utilidad.VerificarDivisas(objDivisasParcial, TipoValor)

            If objDivisasBultos IsNot Nothing AndAlso objDivisasBultos.Count > 0 Then
                objDivisasElemento = objDivisasBultos
            ElseIf TipoValor = Enumeradores.TipoValor.Contado AndAlso objDivisasParcial IsNot Nothing AndAlso objDivisasParcial.Count > 0 Then
                objDivisasElemento = objDivisasParcial
            End If

            If esGestionBulto AndAlso esResultadoFiltro Then

                If objRemesa.Bultos IsNot Nothing Then


                    Dim objBulto As Clases.Bulto = objRemesa.Bultos.FirstOrDefault
                    If objBulto IsNot Nothing Then
                        btnPrecinto.Text = objBulto.Precintos.FirstOrDefault()
                        btnPrecinto.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objBulto.Precintos.FirstOrDefault() & "', '" & txtSeleccionar.ClientID & "');")
                        btnQuitar.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objBulto.Precintos.FirstOrDefault() & "', '" & txtSeleccionar.ClientID & "');")
                        btnSaldoCuenta.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objBulto.Precintos.FirstOrDefault() & "', '" & txtSeleccionar.ClientID & "');")

                        lblCodigo.Text = "<br/>" & Traduzir("059_remesa") & ":<br/>" & objRemesa.CodigoExterno

                        lblCliente.Text = String.Format("{0}<br/>{1}", Traduzir("059_cliente"), Aplicacao.Util.Utilidad.PreencheCliente(objBulto.Cuenta))
                        lblCanal.Text = String.Format("{0}<br/>{1}", Traduzir("059_canal"), Aplicacao.Util.Utilidad.PreencheCanal(objBulto.Cuenta))
                        lblSector.Text = String.Format("{0}<br/>{1}", Traduzir("059_sector"), If(objBulto.Cuenta IsNot Nothing AndAlso objBulto.Cuenta.Sector IsNot Nothing,
                                                                                                 objBulto.Cuenta.Sector.Descripcion, String.Empty))

                    End If

                End If

            Else
                dvBultos.Style.Item("display") = "block"
                btnPrecinto.Text = objRemesa.CodigoExterno
                btnPrecinto.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objRemesa.CodigoExterno & "', '" & txtSeleccionar.ClientID & "');")
                btnQuitar.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objRemesa.CodigoExterno & "', '" & txtSeleccionar.ClientID & "');")
                btnSaldoCuenta.Attributes.Add("onkeydown", "javascript:quitarSelecionado('" & objRemesa.CodigoExterno & "', '" & txtSeleccionar.ClientID & "');")

                lblCliente.Text = String.Format("{0}<br/>{1}", Traduzir("059_cliente"), Aplicacao.Util.Utilidad.PreencheCliente(objRemesa.Cuenta))
                lblCanal.Text = String.Format("{0}<br/>{1}", Traduzir("059_canal"), Aplicacao.Util.Utilidad.PreencheCanal(objRemesa.Cuenta))
                lblSector.Text = String.Format("{0}<br/>{1}", Traduzir("059_sector"), If(objRemesa.Cuenta IsNot Nothing AndAlso objRemesa.Cuenta.Sector IsNot Nothing,
                                                                                         objRemesa.Cuenta.Sector.Descripcion, String.Empty))

                If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then
                    For Each b In objRemesa.Bultos
                        b.PrecintosRemesa.Add(objRemesa.CodigoExterno)
                    Next

                    rptObjetos.DataSource = objRemesa.Bultos
                    rptObjetos.DataBind()
                End If

                If Not esGestionBulto Then
                    If objDivisasRemesa IsNot Nothing AndAlso objDivisasRemesa.Count > 0 Then
                        objDivisasElemento = objDivisasRemesa
                    ElseIf TipoValor = Enumeradores.TipoValor.Contado AndAlso objDivisasParcial IsNot Nothing AndAlso objDivisasParcial.Count > 0 Then
                        objDivisasElemento = objDivisasParcial
                    End If
                End If

            End If

            Dim divisas As List(Of Tuple(Of Decimal, Clases.Divisa)) = Util.RetornaTotalImporteDivisas(objDivisasElemento, TipoValor)
            TotalDivisas.AddRange(divisas)

            rptDivisas.DataSource = divisas
            rptDivisas.DataBind()

            btnSaldoCuenta.CommandArgument = e.Item.ItemIndex
            btnPrecinto.CommandArgument = e.Item.ItemIndex
            btnQuitar.CommandArgument = e.Item.ItemIndex
            imgHistorico.CommandArgument = e.Item.ItemIndex

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

            Dim dvTotales As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotales"), HtmlContainerControl)

            dvTotales.InnerText = Traduzir("058_Totales")
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

    Protected Sub rptObjetos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim objElemento As Clases.Elemento = DirectCast(e.Item.DataItem, Clases.Elemento)
            Dim objBulto = DirectCast(objElemento, Clases.Bulto)

            Dim litObjetos As Literal = DirectCast(e.Item.FindControl("litObjetos"), Literal)
            litObjetos.Text = "<div class='disponibleOff'>" & objBulto.Precintos.FirstOrDefault() & "<input type='hidden' value='" & objBulto.PrecintosRemesa.FirstOrDefault() & "' name='" & objBulto.Precintos.FirstOrDefault() & "' /></div>"
        End If
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

    Private Sub ConfigurarControles()
        dvSeleccionar.Visible = ExhibirSeleccion
    End Sub

    Public Sub ActualizaGrid()
        If Me.Elementos IsNot Nothing AndAlso Me.Elementos.Count > 0 Then
            Dim objElemento = Me.Elementos(0)
            If TypeOf objElemento Is Clases.Remesa Then
                TipoElemento = Enumeradores.TipoElemento.Remesa
            ElseIf TypeOf objElemento Is Clases.Bulto Then
                TipoElemento = Enumeradores.TipoElemento.Bulto
            ElseIf TypeOf objElemento Is Clases.Parcial Then
                TipoElemento = Enumeradores.TipoElemento.Parcial
            ElseIf TypeOf objElemento Is Clases.Contenedor Then
                TipoElemento = Enumeradores.TipoElemento.Contenedor
            End If
        End If

        Me.rptElementos.DataSource = Me.Elementos
        Me.rptElementos.DataBind()

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

    Protected Sub removerElemento()
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
                    args.Elemento = elemento
                    RaiseEvent Quitar(Me, args)
                End If
            End If

            txtSeleccionar.Text = String.Empty
            txtSeleccionar.Focus()
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try


    End Sub


    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender

        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "limpar_txtSeleccionar" & Me.ClientID, "limparSeleccionar('" & Me.txtSeleccionar.ClientID & "');", True)

    End Sub
#End Region
End Class