Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Public Class ucListaDocumentos
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property DocumentosSeleccionados() As ObservableCollection(Of Clases.Documento)
    Public Property Modo() As Enumeradores.Modo
    Public Property mensajeVacio As String
    Public Property Titulo As String
    Public Property esEntreCanales As Boolean

    Private _ValorTotal As New ObservableCollection(Of Totales)

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            Me.ConfigurarControles()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        lblTituloValores.Text = Titulo
    End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub rptDocumentos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDocumentos.ItemCreated

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaValores As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaValores"), HtmlContainerControl)
            Dim btnDetalles As ImageButton = DirectCast(e.Item.FindControl("btnDetalles"), ImageButton)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)

            If (Modo = Enumeradores.Modo.Alta OrElse Modo = Enumeradores.Modo.Modificacion) Then
                btnQuitar.AlternateText = Traduzir("058_quitar")
                btnQuitar.Visible = True
            Else
                btnQuitar.Visible = False
            End If

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaValores.Style.Add("background-color", "#ffffff")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            Else
                dvListaValores.Style.Add("background-color", "#fffbeb")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
                btnQuitar.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim dvTotales As HtmlContainerControl = DirectCast(e.Item.FindControl("dvTotales"), HtmlContainerControl)

            dvTotales.InnerText = Traduzir("058_Totales")

        End If

    End Sub

    Private Sub rptDocumentos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptDocumentos.ItemDataBound

        If e.Item.ItemType = ListItemType.Header Then

            _ValorTotal.Clear()

        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Busca o Elemento
            Dim _documento As Clases.Documento = DirectCast(e.Item.DataItem, Clases.Documento)

            ' Referencia aos objetos da tela
            Dim btnDetalles As ImageButton = DirectCast(e.Item.FindControl("btnDetalles"), ImageButton)
            Dim lblCuentaOrigen As Label = DirectCast(e.Item.FindControl("lblCuentaOrigen"), Label)
            Dim lblCuentaDestino As Label = DirectCast(e.Item.FindControl("lblCuentaDestino"), Label)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)
            Dim btnQuitar As ImageButton = DirectCast(e.Item.FindControl("btnQuitar"), ImageButton)

            btnQuitar.OnClientClick = "return confirm('" & Traduzir("032_EliminarDocumento") & "')"

            lblCuentaOrigen.Text = Aplicacao.Util.Utilidad.PreencheCuenta(_documento.CuentaOrigen, False)

            If esEntreCanales Then
                If _documento.CuentaDestino.Canal IsNot Nothing AndAlso Not String.IsNullOrEmpty(_documento.CuentaDestino.Canal.Descripcion) Then
                    If Not String.IsNullOrEmpty(lblCuentaDestino.Text) Then
                        lblCuentaDestino.Text &= " | "
                    End If
                    lblCuentaDestino.Text &= _documento.CuentaDestino.Canal.Codigo & " - " & _documento.CuentaDestino.Canal.Descripcion
                End If
                If _documento.CuentaDestino.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(_documento.CuentaDestino.SubCanal.Descripcion) Then
                    If Not String.IsNullOrEmpty(lblCuentaDestino.Text) Then
                        lblCuentaDestino.Text &= " | "
                    End If
                    lblCuentaDestino.Text &= _documento.CuentaDestino.SubCanal.Codigo & " - " & _documento.CuentaDestino.SubCanal.Descripcion
                End If
            Else

                If _documento.CuentaDestino.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(_documento.CuentaDestino.Cliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(lblCuentaDestino.Text) Then
                        lblCuentaDestino.Text &= " | "
                    End If
                    lblCuentaDestino.Text &= _documento.CuentaDestino.Cliente.Codigo & " - " & _documento.CuentaDestino.Cliente.Descripcion
                End If

                If _documento.CuentaDestino.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(_documento.CuentaDestino.SubCliente.Descripcion) Then
                    If Not String.IsNullOrEmpty(lblCuentaDestino.Text) Then
                        lblCuentaDestino.Text &= " | "
                    End If
                    lblCuentaDestino.Text &= _documento.CuentaDestino.SubCliente.Codigo & " - " & _documento.CuentaDestino.SubCliente.Descripcion
                End If
                If _documento.CuentaDestino.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(_documento.CuentaDestino.PuntoServicio.Descripcion) Then
                    If Not String.IsNullOrEmpty(lblCuentaDestino.Text) Then
                        lblCuentaDestino.Text &= " | "
                    End If
                    lblCuentaDestino.Text &= _documento.CuentaDestino.PuntoServicio.Codigo & " - " & _documento.CuentaDestino.PuntoServicio.Descripcion
                End If
            End If


            Dim _Totales As New ObservableCollection(Of Totales)

            For Each div In _documento.Divisas.Clonar()

                If div.ValoresTotales IsNot Nothing AndAlso div.ValoresTotales.Count > 0 Then
                    If _Totales.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor) Is Nothing Then

                        _Totales.Add(New Totales With {.CodigoSimbolo = div.CodigoSimbolo,
                                                         .ColorTotales = div.Color,
                                                         .Descripcion = div.Descripcion,
                                                         .IdentificadorDivisas = div.Identificador,
                                                         .Importe = div.ValoresTotales(0).Importe,
                                                         .TipoValor = div.ValoresTotales(0).TipoValor})
                    Else
                        _Totales.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor).Importe += div.ValoresTotales(0).Importe
                    End If

                    If _ValorTotal.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor) Is Nothing Then

                        _ValorTotal.Add(New Totales With {.CodigoSimbolo = div.CodigoSimbolo,
                                                         .ColorTotales = div.Color,
                                                         .Descripcion = div.Descripcion,
                                                         .IdentificadorDivisas = div.Identificador,
                                                         .Importe = div.ValoresTotales(0).Importe,
                                                         .TipoValor = div.ValoresTotales(0).TipoValor})
                    Else
                        _ValorTotal.FirstOrDefault(Function(x) x.IdentificadorDivisas = div.Identificador AndAlso x.TipoValor = div.ValoresTotales(0).TipoValor).Importe += div.ValoresTotales(0).Importe
                    End If

                End If
            Next

            rptDivisas.DataSource = _Totales
            rptDivisas.DataBind()

            btnDetalles.CommandArgument = _documento.Identificador
            btnQuitar.CommandArgument = _documento.Identificador

        ElseIf e.Item.ItemType = ListItemType.Footer Then

            Dim rptTotalDivisas As Repeater = DirectCast(e.Item.FindControl("rptTotalDivisas"), Repeater)

            rptTotalDivisas.DataSource = _ValorTotal
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
        Dim args As New DetallarDocumentoEventArgs()
        args.IdentificadorDocumento = DirectCast(sender, ImageButton).CommandArgument
        RaiseEvent QuitarDocumento(Me, args)
    End Sub

    Protected Sub btnDetalles_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim args As New DetallarDocumentoEventArgs()
        args.IdentificadorDocumento = DirectCast(sender, ImageButton).CommandArgument
        RaiseEvent DetallarDocumento(Me, args)
    End Sub

#End Region

#Region "[DELEGATE]"

    Public Class DetallarDocumentoEventArgs
        Public Property IdentificadorDocumento As String
    End Class

    Public Event DetallarDocumento(sender As Object, e As DetallarDocumentoEventArgs)

    Public Event QuitarDocumento(sender As Object, e As DetallarDocumentoEventArgs)


#End Region

#Region "[METODOS]"

    Public Sub ConfigurarControles()

        rptDocumentos.DataSource = DocumentosSeleccionados
        rptDocumentos.DataBind()

        If DocumentosSeleccionados IsNot Nothing AndAlso DocumentosSeleccionados.Count > 0 Then
            lblmensajeVacio.Text = String.Empty
            rptDocumentos.Visible = True
        Else
            lblmensajeVacio.Text = mensajeVacio
            rptDocumentos.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region

    Public Class Totales
        Inherits Clases.ImporteTotal
        Public IdentificadorDivisas As String
        Public CodigoSimbolo As String
        Public Descripcion As String
        Public ColorTotales As Drawing.Color
    End Class

End Class