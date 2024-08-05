Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

Public Class ucListaContenedoresGrp
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property DocumentosSeleccionados() As ObservableCollection(Of Clases.Documento)
    Public Property mensajeVacio As String
    Public Property Titulo As String

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

#Region "[DELEGATE]"

    Public Class DetallarDocumentoEventArgs
        Public Property IdentificadorDocumento As String
    End Class

    Public Event DetallarDocumento(sender As Object, e As DetallarDocumentoEventArgs)

#End Region

#Region "[EVENTOS]"


    Private Sub rptDocumentos_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDocumentos.ItemCreated

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            ' Referencia aos objetos da tela
            Dim dvListaValores As HtmlContainerControl = DirectCast(e.Item.FindControl("dvListaValores"), HtmlContainerControl)
            Dim btnDetalles As Button = DirectCast(e.Item.FindControl("btnDetalles"), Button)

            'Seta as configurações dependentes de Item/AlternatingItem
            If (e.Item.ItemIndex Mod 2) = 0 Then
                dvListaValores.Style.Add("background-color", "#ffffff")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#ffffff');")
            Else
                dvListaValores.Style.Add("background-color", "#fffbeb")
                btnDetalles.Attributes.Add("onblur", "javascript:seleccionarElemento(this, '#fffbeb');")
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
            Dim _contenedor As Clases.Contenedor = Nothing

            If _documento.Elemento IsNot Nothing AndAlso _documento.Elemento.GetType() Is GetType(Clases.Contenedor) Then
                _contenedor = DirectCast(_documento.Elemento, Clases.Contenedor)
            End If

            ' Referencia aos objetos da tela
            Dim btnDetalles As Button = DirectCast(e.Item.FindControl("btnDetalles"), Button)
            Dim lblCliente As Label = DirectCast(e.Item.FindControl("lblCliente"), Label)
            Dim lblCanal As Label = DirectCast(e.Item.FindControl("lblCanal"), Label)
            Dim lblSector As Label = DirectCast(e.Item.FindControl("lblSector"), Label)
            Dim litPrecintos As Literal = DirectCast(e.Item.FindControl("litPrecintos"), Literal)
            Dim lblTituloCliente As Label = DirectCast(e.Item.FindControl("lblTituloCliente"), Label)
            Dim lblTituloCanal As Label = DirectCast(e.Item.FindControl("lblTituloCanal"), Label)
            Dim lblTituloSector As Label = DirectCast(e.Item.FindControl("lblTituloSector"), Label)
            Dim lblTituloPrecintos As Label = DirectCast(e.Item.FindControl("lblTituloPrecintos"), Label)
            Dim lblDivisas As Label = DirectCast(e.Item.FindControl("lblDivisas"), Label)
            Dim rptDivisas As Repeater = DirectCast(e.Item.FindControl("rptDivisas"), Repeater)

            Dim _Totales As New ObservableCollection(Of Totales)

            lblTituloCliente.Text = Traduzir("032_lista_contenedor_titulo_cliente")
            lblTituloCanal.Text = Traduzir("032_lista_contenedor_titulo_Canal")
            lblTituloSector.Text = Traduzir("032_lista_contenedor_titulo_Sector")
            lblTituloPrecintos.Text = Traduzir("032_lista_contenedor_titulo_Precinto")
            lblDivisas.Text = Traduzir("032_lista_contenedor_titulo_Valores")

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
            If _contenedor IsNot Nothing Then
                btnDetalles.Text = _contenedor.Codigo

                If _contenedor.Cuenta IsNot Nothing Then

                    ' Cliente - SubCliente - PuntoServicio
                    lblCliente.Text = ""
                    If _contenedor.Cuenta.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.Cliente.Descripcion) Then
                        lblCliente.Text &= _contenedor.Cuenta.Cliente.Codigo & " - " & _contenedor.Cuenta.Cliente.Descripcion
                    End If
                    If _contenedor.Cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.SubCliente.Descripcion) Then
                        If Not String.IsNullOrEmpty(lblCliente.Text) Then
                            lblCliente.Text &= " | "
                        End If
                        lblCliente.Text &= _contenedor.Cuenta.SubCliente.Codigo & " - " & _contenedor.Cuenta.SubCliente.Descripcion
                    End If
                    If _contenedor.Cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.PuntoServicio.Descripcion) Then
                        If Not String.IsNullOrEmpty(lblCliente.Text) Then
                            lblCliente.Text &= " | "
                        End If
                        lblCliente.Text &= _contenedor.Cuenta.PuntoServicio.Codigo & " - " & _contenedor.Cuenta.PuntoServicio.Descripcion
                    End If

                    ' Canal - SubCanal
                    lblCanal.Text = ""
                    If _contenedor.Cuenta.Canal IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.Canal.Descripcion) Then
                        lblCanal.Text &= _contenedor.Cuenta.Canal.Codigo & " - " & _contenedor.Cuenta.Canal.Descripcion
                    End If
                    If _contenedor.Cuenta.SubCanal IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.SubCanal.Descripcion) Then
                        If Not String.IsNullOrEmpty(lblCanal.Text) Then
                            lblCanal.Text &= " | "
                        End If
                        lblCanal.Text &= _contenedor.Cuenta.SubCanal.Codigo & " - " & _contenedor.Cuenta.SubCanal.Descripcion
                    End If

                    ' Sector
                    lblSector.Text = ""
                    If _contenedor.Cuenta.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(_contenedor.Cuenta.Sector.Descripcion) Then
                        lblSector.Text &= _contenedor.Cuenta.Sector.Codigo & " - " & _contenedor.Cuenta.Sector.Descripcion
                    End If

                End If

                If _contenedor.Precintos IsNot Nothing AndAlso _contenedor.Precintos.Count > 0 Then
                    For Each precinto In _contenedor.Precintos
                        If Not String.IsNullOrEmpty(precinto) Then
                            litPrecintos.Text &= "<div id='item_" & precinto & "'>" & precinto & "</div>"
                        End If
                    Next
                End If

            Else
                btnDetalles.Text = _documento.Identificador
            End If

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

    Protected Sub btnDetalles_Click(sender As Object, e As System.EventArgs)
        Dim args As New DetallarDocumentoEventArgs()
        args.IdentificadorDocumento = DirectCast(sender, Button).CommandArgument
        RaiseEvent DetallarDocumento(Me, args)
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Carrega Canal e SubCanal utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
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