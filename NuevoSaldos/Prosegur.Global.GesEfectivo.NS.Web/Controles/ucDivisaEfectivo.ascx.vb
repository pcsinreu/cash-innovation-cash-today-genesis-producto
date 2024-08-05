Imports System.IO
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports System.Web.Script.Serialization
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel

''' <summary>
''' Clases ucDivisa
''' </summary>
''' <remarks></remarks>
<Serializable()>
Public Class ucDivisaEfectivo
    Inherits UcBase

#Region "Variaveis"

    ''' <summary>
    ''' Divisas del cliente rellenadas en el controle ucEfectivo.
    ''' </summary>
    Private _DivisaIAC As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    ''' Divisas selecionadas en el Popup AgregarDivisa.ascx
    ''' </summary>
    ''' <remarks></remarks>
    Private DivisasSelecionadas As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    ''' Lista de UnidadMedida
    ''' </summary>
    ''' <remarks></remarks>
    Public UnidadMedida As ObservableCollection(Of Clases.UnidadMedida)

    ''' <summary>
    ''' Lista de Calidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Calidad As ObservableCollection(Of Clases.Calidad)

    ''' <summary>
    ''' Modo de operación del controle {Alta, Modificacion, Consulta}
    ''' </summary>
    ''' <remarks></remarks>
    Public Modo As Comon.Enumeradores.Modo

    ''' <summary>
    ''' Tipo de valor: {Declarado, Contado}
    ''' </summary>
    ''' <remarks></remarks>
    Public TipoValor As Comon.Enumeradores.TipoValor

#End Region

#Region "ViewStates"

    ''' <summary>
    ''' Divisas cambiadas en tiempo de ejecución. Atraves desto objeto será actualizado el objeto DivisasIAC
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DivisasActualizadas As ObservableCollection(Of Clases.Divisa)
        Get
            If ViewState("DivisasActualizadas") Is Nothing Then
                ViewState("DivisasActualizadas") = New ObservableCollection(Of Clases.Divisa)
            End If
            Return ViewState("DivisasActualizadas")
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            ViewState("DivisasActualizadas") = value
        End Set
    End Property

    ''' <summary>
    ''' Suma total de la divisa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ImporteTotalDivisa As Decimal
        Get
            If ViewState("ImporteTotalDivisa") Is Nothing Then
                ViewState("ImporteTotalDivisa") = 0
            End If
            Return ViewState("ImporteTotalDivisa")
        End Get
        Set(value As Decimal)
            ViewState("ImporteTotalDivisa") = value
        End Set
    End Property

    ''' <summary>
    ''' Suma total de la divisa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ImporteTotalDivisaDIC As Dictionary(Of String, Decimal)
        Get
            If ViewState("ImporteTotalDivisaDIC") Is Nothing Then
                ViewState("ImporteTotalDivisaDIC") = New Dictionary(Of String, Decimal)
            End If
            Return ViewState("ImporteTotalDivisaDIC")
        End Get
        Set(value As Dictionary(Of String, Decimal))
            ViewState("ImporteTotalDivisaDIC") = value
        End Set
    End Property

    ''' <summary>
    ''' Suma total de las denominaciones de una divisa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ImporteTotalDenominacion As Decimal
        Get
            If ViewState("ImporteTotalDenominacion") Is Nothing Then
                ViewState("ImporteTotalDenominacion") = 0
            End If
            Return ViewState("ImporteTotalDenominacion")
        End Get
        Set(value As Decimal)
            ViewState("ImporteTotalDenominacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Validar si ya fue ejecutado el DataBind
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property EsDataBind As Boolean
        Get
            If ViewState("EsDataBind") Is Nothing Then
                ViewState("EsDataBind") = False
            End If
            Return ViewState("EsDataBind")
        End Get
        Set(value As Boolean)
            ViewState("EsDataBind") = value
        End Set
    End Property

#End Region

#Region "Propriedades"

    ''' <summary>
    ''' Divisas Originais da pagina, solamente serán cambiadas al pussar el botón grabar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DivisaIAC As ObservableCollection(Of Clases.Divisa)
        Get
            If _DivisaIAC Is Nothing Then
                _DivisaIAC = New ObservableCollection(Of Clases.Divisa)
            End If
            Return _DivisaIAC
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            _DivisaIAC = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento Load da pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                DivisasActualizadas = DivisaIAC
                If ltParametrosJSON.Text = String.Empty Then
                    Dim lista As New List(Of Unidade)
                    For Each unidade In UnidadMedida
                        lista.Add(New Unidade() With {.Codigo = unidade.Codigo, .Valor = unidade.ValorUnidad})
                    Next unidade

                    Dim jss As New JavaScriptSerializer
                    Dim par As New ParametrosJSON
                    par.MensagemDenominacionSemValor = Traduzir("012_valordenominacion")
                    par.MensagemDivisioninvalida = Traduzir("012_divisioninvalida").Replace("'{0}'", "[X]").Replace("'{1}'", "[Y]").Replace("'{2}'", "[Z]")
                    par.SeparadorDecimal = MyBase._DecimalSeparador
                    par.SeparadorMilhar = MyBase._MilharSeparador
                    par.UnidadesMedidas = lista
                    Me.ltParametrosJSON.Text = jss.Serialize(par)
                End If
            End If

            If Not IsPostBack OrElse Not EsDataBind Then
                If DivisasActualizadas IsNot Nothing Then
                    Aplicacao.Util.Utilidad.OrdenarItemsDivisas(DivisasActualizadas)
                    If DivisasActualizadas.Exists(Function(f) f.ValoresTotalesEfectivo IsNot Nothing AndAlso f.ValoresTotalesEfectivo.Count > 0) OrElse _
                       DivisasActualizadas.Exists(Function(f) f.Denominaciones IsNot Nothing AndAlso f.Denominaciones.Count > 0) Then

                        rptDivisaEfectivo.DataSource = DivisasActualizadas
                        rptDivisaEfectivo.DataBind()
                        EsDataBind = True
                    Else
                        If DivisasActualizadas.Exists(Function(f) (f.Denominaciones IsNot Nothing AndAlso f.Denominaciones.Count = 0) OrElse f.Denominaciones Is Nothing) Then DivisasActualizadas = Nothing
                    End If
                End If
            End If

            ' Crear Dicionario con el valor total de la divisa e el identificador de la divisa.
            ImporteTotalDivisaDIC.Clear()

            For Each _divisa In DivisasActualizadas

                If _divisa.ValoresTotalesEfectivo IsNot Nothing Then
                    ' recupera el valor del importe de la divisa.
                    Dim ValorDivisa As Decimal = 0 ' _divisa.ValoresTotalesEfectivo.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault.Importe
                    For Each valor In _divisa.ValoresTotalesEfectivo
                        If valor.TipoValor = TipoValor Then
                            ValorDivisa = valor.Importe
                        End If
                    Next

                    ' si no hay esto divisa en la lista, carga esto divisa con su valor.
                    If Not ImporteTotalDivisaDIC.ContainsKey(_divisa.Identificador) Then
                        ImporteTotalDivisaDIC.Add(_divisa.Identificador, ValorDivisa)

                    End If

                Else
                    ' sy no hay ValorDivisa, carga esto divisa con valor 0
                    If Not ImporteTotalDivisaDIC.ContainsKey(_divisa.Identificador) Then
                        ImporteTotalDivisaDIC.Add(_divisa.Identificador, 0)

                    End If

                End If

            Next _divisa
        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' ItemCreated Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptDivisaEfectivo_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDivisaEfectivo.ItemDataBound
        'Private Sub rptDivisaEfectivo_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDivisaEfectivo.ItemCreated

        ' recupera la divisa
        Dim itemDivisa As Clases.Divisa = If(e.Item.DataItem IsNot Nothing, e.Item.DataItem, DivisasActualizadas(e.Item.ItemIndex)) 'DivisasActualizadas(e.Item.ItemIndex)

        ' recupera los controles del repeater
        Dim repeaterDetalle As Repeater = e.Item.FindControl("rptDetalle")
        Dim hdfIdentificador As HiddenField = e.Item.FindControl("hdfIdentificador")
        Dim lblDivisa As Label = e.Item.FindControl("lblDivisa")
        Dim lblCodIso As Label = e.Item.FindControl("lblCodIso")
        Dim lblImporteDivisa As Label = e.Item.FindControl("lblImporteDivisa")
        Dim lblImporteTotal As Label = e.Item.FindControl("lblImporteTotal")
        Dim lblFiltroDivisaEfectivo As Label = e.Item.FindControl("lblFiltroDivisaEfectivo")
        Dim lblDenominaciones As Label = e.Item.FindControl("lblDenominaciones")
        Dim btnBorrarDivisa As Button = e.Item.FindControl("btnBorrarDivisa")

        Dim imgDivisa As System.Web.UI.WebControls.Image = e.Item.FindControl("imgDivisa")
        Dim txtDivisa As TextBox = e.Item.FindControl("txtDivisa")
        Dim txtCodIso As TextBox = e.Item.FindControl("txtCodIso")
        Dim txtImporteDivisa As TextBox = e.Item.FindControl("txtImporteDivisa")
        Dim txtImporteTotal As TextBox = e.Item.FindControl("txtImporteTotal")

        txtImporteDivisa.Attributes.Add("onBlur", "totalDivisa(this);")
        txtImporteDivisa.Attributes.Add("onFocus", "AtualizarImporteDivisaAtual(this);")
        Aplicacao.Util.Utilidad.CargarScripts(txtImporteDivisa, MyBase._DecimalSeparador, MyBase._MilharSeparador, "I")

        Dim fieldsetDenominacion As HtmlGenericControl = e.Item.FindControl("fieldsetDenominacion")

        ' traduzir controles
        lblDivisa.Text = Traduzir("012_descripcion")
        lblCodIso.Text = Traduzir("012_codigo")
        lblImporteDivisa.Text = Traduzir("012_importe")
        lblImporteTotal.Text = Traduzir("012_importetotal")
        btnBorrarDivisa.Text = Traduzir("012_borrardivisa")

        lblDenominaciones.Text = If(itemDivisa.Denominaciones IsNot Nothing, If(itemDivisa.Denominaciones.Count > 1, Traduzir("012_denominaciones"), Traduzir("012_denominacion")), "")
        hdfIdentificador.Value = itemDivisa.Identificador

        ' repor valores
        txtDivisa.Text = itemDivisa.Descripcion
        txtCodIso.Text = itemDivisa.CodigoISO

        If itemDivisa.ValoresTotalesEfectivo IsNot Nothing AndAlso itemDivisa.ValoresTotalesEfectivo.Count > 0 Then
            Dim valorDivisa_DeclaradoOContado = itemDivisa.ValoresTotalesEfectivo.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault
            txtImporteDivisa.Text = If(valorDivisa_DeclaradoOContado IsNot Nothing, valorDivisa_DeclaradoOContado.Importe, "")
            ImporteTotalDivisa = If(Not String.IsNullOrEmpty(txtImporteDivisa.Text), txtImporteDivisa.Text, 0)

        End If

        ImporteTotalDivisa = If(Not String.IsNullOrEmpty(txtImporteDivisa.Text), txtImporteDivisa.Text, 0)
        ImporteTotalDivisaDIC(hdfIdentificador.Value) = ImporteTotalDivisa

        Aplicacao.Util.Utilidad.CargarImagenDivisa(itemDivisa, imgDivisa)

        ' titulo del fieldset
        lblFiltroDivisaEfectivo.Text = itemDivisa.Descripcion

        ActualizarEstadoCampo(txtImporteDivisa)
        ActualizarEstadoCampo(btnBorrarDivisa)

        ' calcular valor total de las denominaciones
        If itemDivisa.Denominaciones IsNot Nothing AndAlso itemDivisa.Denominaciones.Count > 0 Then
            fieldsetDenominacion.Visible = True
            Dim objDenominacion As ObservableCollection(Of Clases.Denominacion) = itemDivisa.Denominaciones.Where(Function(D) D.ValorDenominacion IsNot Nothing).ToObservableCollection

            ImporteTotalDenominacion = (From _denominacion In objDenominacion
                                        From _valorDenominacion In _denominacion.ValorDenominacion
                                        Where _valorDenominacion.TipoValor = TipoValor
                                        Select _valorDenominacion.Importe).Sum

            ImporteTotalDivisa = ImporteTotalDivisaDIC.Where(Function(f) f.Key = hdfIdentificador.Value).FirstOrDefault.Value()

            ' handles
            AddHandler repeaterDetalle.ItemDataBound, AddressOf repeaterDetalle_ItemDataBound

            Dim denominaciones As New ObservableCollection(Of Clases.Denominacion)

            ' Codigo feito para suplir uma necesidade do formulario de Cierre
            ' Na proxima versão deste componente não havera necessidade disto
            If Modo = Enumeradores.Modo.Consulta Then
                For Each den In itemDivisa.Denominaciones
                    For Each valores In den.ValorDenominacion
                        If valores.TipoValor = TipoValor Then
                            Dim denominacao As Clases.Denominacion = den.Clonar
                            denominacao.ValorDenominacion.Clear()
                            denominacao.ValorDenominacion.Add(valores)
                            denominaciones.Add(denominacao)
                        End If
                    Next
                Next
            Else
                denominaciones = itemDivisa.Denominaciones
            End If

            ' DataBind denominaciones
            repeaterDetalle.DataSource = denominaciones
            repeaterDetalle.DataBind()

        End If

        Dim valorTotal As Decimal = ImporteTotalDivisa + ImporteTotalDenominacion
        txtImporteTotal.Text = String.Format("{0:N2}", valorTotal)

        ' validação para desabilitar Total quando tipovalor for Contado.
        If TipoValor = Enumeradores.TipoValor.Contado Then
            txtImporteDivisa.Enabled = False
        Else
            ' Desabilitar em modo consulta
            txtImporteDivisa.Enabled = Not (Modo = Enumeradores.Modo.Consulta AndAlso (itemDivisa.ValoresTotalesEfectivo Is Nothing OrElse itemDivisa.ValoresTotalesEfectivo IsNot Nothing))
        End If

    End Sub

    ''' <summary>
    ''' ItemDataBound Denominacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub repeaterDetalle_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)

        Dim lblCantidad As Label = Nothing
        Dim lblUnidad As Label = Nothing
        Dim lblDenominacion As Label = Nothing
        Dim lblCodDenominacion As Label = Nothing
        Dim lblImporteDenominacion As Label = Nothing
        Dim lblBilleteMoneda As Label = Nothing
        Dim lblCalidad As Label = Nothing

        Try
            If e.Item.ItemType = ListItemType.Header Then
                lblCantidad = DirectCast(e.Item.FindControl("lblCantidad"), Label)
                lblUnidad = DirectCast(e.Item.FindControl("lblUnidad"), Label)
                lblDenominacion = DirectCast(e.Item.FindControl("lblDenominacion"), Label)
                lblCodDenominacion = DirectCast(e.Item.FindControl("lblCodDenominacion"), Label)
                lblImporteDenominacion = DirectCast(e.Item.FindControl("lblImporteDenominacion"), Label)
                lblBilleteMoneda = DirectCast(e.Item.FindControl("lblBilleteMoneda"), Label)
                lblCalidad = DirectCast(e.Item.FindControl("lblCalidad"), Label)

                lblUnidad.Text = Traduzir("012_unidad")
                lblCantidad.Text = Traduzir("012_cantidad")
                lblDenominacion.Text = Traduzir("012_descripcion")
                lblCodDenominacion.Text = Traduzir("012_codigo")
                lblImporteDenominacion.Text = Traduzir("012_importe")
                lblCalidad.Text = Traduzir("012_calidad")
                lblCalidad.Visible = TipoValor = Enumeradores.TipoValor.Contado OrElse TipoValor = Enumeradores.TipoValor.NoDefinido

            ElseIf e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
                ' campo HIDDEN para almacenar el Identificador de la divisa.
                Dim repeaterItemDivisa As RepeaterItem = DirectCast(sender, Repeater).NamingContainer
                Dim hdfIdentificador As HiddenField = repeaterItemDivisa.FindControl("hdfIdentificador")
                Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificador.Value).FirstOrDefault
                Dim itemDenominacion As Clases.Denominacion = e.Item.DataItem
                Dim txtImporteTotal As TextBox = repeaterItemDivisa.FindControl("txtImporteTotal")
                If txtImporteTotal IsNot Nothing Then
                    Dim ddlUnidad As DropDownList = e.Item.FindControl("ddlUnidad")
                    Dim txtCantidad As TextBox = e.Item.FindControl("txtCantidad")
                    Dim txtDenominacion As TextBox = e.Item.FindControl("txtDenominacion")
                    Dim txtCodDenominacion As TextBox = e.Item.FindControl("txtCodDenominacion")
                    Dim imgBilleteMoneda As System.Web.UI.WebControls.Image = e.Item.FindControl("imgBilleteMoneda")
                    Dim txtImporteDenominacion As TextBox = e.Item.FindControl("txtImporteDenominacion")
                    Dim ddlCalidad As DropDownList = e.Item.FindControl("ddlCalidad")
                    Dim hdfIdentificadorDenominacion As HiddenField = e.Item.FindControl("hdfIdentificadorDenominacion")
                    hdfIdentificadorDenominacion.Value = itemDenominacion.Identificador

                    Dim ValoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = itemDenominacion.ValorDenominacion
                    Dim ValorDenominacion As Clases.ValorDenominacion = Nothing
                    If ValoresDenominacion IsNot Nothing AndAlso ValoresDenominacion.Count > 0 Then
                        ValorDenominacion = ValoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                    End If

                    ' repor valores existentes
                    txtCantidad.Text = If(ValorDenominacion IsNot Nothing, ValorDenominacion.Cantidad, "")
                    txtImporteDenominacion.Text = If(ValorDenominacion IsNot Nothing, String.Format("{0:N2}", ValorDenominacion.Importe), "")
                    txtDenominacion.Text = itemDenominacion.Descripcion
                    txtCodDenominacion.Text = itemDenominacion.Codigo

                    If lblBilleteMoneda IsNot Nothing Then lblBilleteMoneda.Text = If(itemDenominacion.EsBillete, Traduzir("012_billete"), Traduzir("012_moneda"))
                    imgBilleteMoneda.ImageUrl = If(itemDenominacion.EsBillete, "../Imagenes/Money.gif", "../Imagenes/Coins.gif")

                    ActualizarEstadoCampo(ddlUnidad)
                    ActualizarEstadoCampo(txtCantidad)
                    ActualizarEstadoCampo(txtImporteDenominacion)
                    ActualizarEstadoCampo(ddlCalidad)

                    ddlCalidad.Visible = TipoValor = Enumeradores.TipoValor.Contado OrElse TipoValor = Enumeradores.TipoValor.NoDefinido
                    TratamentosModoEnItemDataBound(ddlUnidad, ddlCalidad, ValorDenominacion, If(itemDenominacion.EsBillete, Enumeradores.TipoUnidadMedida.Billete, Enumeradores.TipoUnidadMedida.Moneda))

                    txtCantidad.Attributes.Add("onkeypress", "javascript:return bloqueialetras(event,this);")
                    If itemDenominacion.Valor = 0 Then
                        ddlUnidad.Attributes.Add("onChange", "javascript:return DenominacionSemValor(this, true);")
                        txtCantidad.Attributes.Add("onChange", "javascript:return DenominacionSemValor(this, false);")
                        txtImporteDenominacion.Attributes.Add("onChange", "javascript:return DenominacionSemValor(this, false);")
                    Else
                        Dim idLinha As String = e.Item.ClientID.Substring(e.Item.ClientID.LastIndexOf("_") + 1)
                        ddlUnidad.Attributes.Add("onChange", String.Format("javascript:return totalDenominacion(this,'ddlUnidad','{0}','{1}');", idLinha, itemDenominacion.Valor.ToString.Replace(",", ".")))
                        txtCantidad.Attributes.Add("onChange", String.Format("javascript:return totalDenominacion(this,'txtCantidad','{0}','{1}');", idLinha, itemDenominacion.Valor.ToString.Replace(",", ".")))
                        txtImporteDenominacion.Attributes.Add("onBlur", String.Format("javascript:return totalDenominacion(this,'txtImporteDenominacion','{0}','{1}');", idLinha, itemDenominacion.Valor.ToString.Replace(",", ".")))
                        txtImporteDenominacion.Attributes.Add("onFocus", "AtualizarImporteDenominacion(this);")
                    End If

                    ' Se TipoValor igual a 'Diferencia', deve aceitar valor negativos
                    Aplicacao.Util.Utilidad.CargarScripts(txtCantidad, MyBase._DecimalSeparador, MyBase._MilharSeparador, "C", (TipoValor = Enumeradores.TipoValor.Diferencia))
                    Aplicacao.Util.Utilidad.CargarScripts(txtImporteDenominacion, MyBase._DecimalSeparador, MyBase._MilharSeparador, "I", (TipoValor = Enumeradores.TipoValor.Diferencia))

                    CalcularTotalDivisaPorDenominaciones(itemDivisa, ValorDenominacion, ImporteTotalDivisaDIC, hdfIdentificador, txtCantidad, txtImporteDenominacion, txtImporteTotal, "")
                End If
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    Protected Sub rptDivisaEfectivo_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptDivisaEfectivo.ItemCommand
        Try
            If e.CommandName = "btnBorrarDivisa" Then
                Me.ActualizarValoresDivisas()
                Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = e.CommandArgument).First
                Me.RechazarDivisaModificada(itemDivisa)
            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento SelectedChanged do componente Unidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        Try
            Dim ddlUnidad As DropDownList = sender
            Dim repeaterItemDenominacion As RepeaterItem = ddlUnidad.NamingContainer
            Dim repeaterItemDivisa As RepeaterItem = repeaterItemDenominacion.NamingContainer.NamingContainer

            Dim ddlCalidad As DropDownList = repeaterItemDenominacion.FindControl("ddlCalidad")
            Dim txtCantidad As TextBox = repeaterItemDenominacion.FindControl("txtCantidad")
            Dim txtImporteDenominacion As TextBox = repeaterItemDenominacion.FindControl("txtImporteDenominacion")

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificador As HiddenField = repeaterItemDivisa.FindControl("hdfIdentificador")
            Dim txtImporteTotal As TextBox = repeaterItemDivisa.FindControl("txtImporteTotal")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificador.Value).First

            ActualizarValoresDivisaPorUnidadMedida(itemDivisa, repeaterItemDenominacion, txtCantidad, txtImporteDenominacion, ddlUnidad, ddlCalidad)
            CalcularTotalDivisaPorDenominaciones(itemDivisa, Nothing, ImporteTotalDivisaDIC, hdfIdentificador, txtCantidad, txtImporteDenominacion, txtImporteTotal, "A")

            ActualizarDivisas(itemDivisa)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento SelectedChanged do componente Unidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ddlCalidad_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try

            Dim ddlCalidad As DropDownList = sender
            Dim repeaterItemDenominacion As RepeaterItem = ddlCalidad.NamingContainer
            Dim repeaterItemDivisa As RepeaterItem = repeaterItemDenominacion.NamingContainer.NamingContainer

            Dim ddlUnidad As DropDownList = repeaterItemDenominacion.FindControl("ddlUnidad")
            Dim txtCantidad As TextBox = repeaterItemDenominacion.FindControl("txtCantidad")
            Dim txtImporteDenominacion As TextBox = repeaterItemDenominacion.FindControl("txtImporteDenominacion")

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificador As HiddenField = repeaterItemDivisa.FindControl("hdfIdentificador")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificador.Value).FirstOrDefault

            ActualizarValoresDivisaPorCalidad(itemDivisa, repeaterItemDenominacion, txtCantidad, txtImporteDenominacion, ddlUnidad, ddlCalidad)
            ActualizarDivisas(itemDivisa)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Guardar datos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GuardarDatos()
        Try
            ActualizarValoresDivisas()
            RechazarCalidad(DivisasActualizadas)
            DivisaIAC.Clear()
            DivisaIAC = DivisasActualizadas
        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try

    End Sub

    ''' <summary>
    ''' ' Borrar del objeto valorDenominacion las Calidads, cuando no se utiliza.
    ''' </summary>
    ''' <param name="DivisasActualizadas"></param>
    ''' <remarks></remarks>
    Private Sub RechazarCalidad(ByRef DivisasActualizadas As ObservableCollection(Of Clases.Divisa))
        If Not (TipoValor = Enumeradores.TipoValor.Contado OrElse TipoValor = Enumeradores.TipoValor.NoDefinido) Then
            If DivisasActualizadas IsNot Nothing AndAlso DivisasActualizadas.Count > 0 Then
                For Each divisa In DivisasActualizadas
                    If divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then
                        For Each denominacion In divisa.Denominaciones
                            If denominacion.ValorDenominacion IsNot Nothing AndAlso denominacion.ValorDenominacion.Count > 0 Then
                                For Each valorDenominacion In denominacion.ValorDenominacion
                                    If valorDenominacion.TipoValor = TipoValor Then
                                        valorDenominacion.Calidad = Nothing
                                    End If
                                Next valorDenominacion
                            End If
                        Next denominacion
                    End If
                Next divisa
            End If
        End If
    End Sub

    ''' <summary>
    ''' Actualizar divisas en la coleccion de divisas
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarDivisas(itemDivisa As Clases.Divisa)

        For index = 0 To DivisasActualizadas.Count - 1
            If DivisasActualizadas(index).Identificador = itemDivisa.Identificador Then
                DivisasActualizadas(index) = itemDivisa
                Exit For
            End If
        Next index

    End Sub

    ''' <summary>
    ''' Borrar divisa 
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <remarks></remarks>
    Private Sub RechazarDivisaModificada(itemDivisa As Clases.Divisa)

        ' cheques si a sesión no es vacío
        If DivisasActualizadas IsNot Nothing AndAlso DivisasActualizadas.Count > 0 Then
            ' agregar al objeto DivisasSelecionadas el valor contenido en la sesión
            DivisasSelecionadas = DivisasActualizadas

        End If

        ' index contador del loop de datos
        Dim indexDivisa As Integer = 0
        For indexDivisa = 0 To DivisasSelecionadas.Count - 1
            ' si el identificador de la divisa en el index(DivisasSelecionadas(indexDivisa)) es iguale al identificador de la divisa actual DivisaIAC(repeaterItemDivisa.ItemIndex)
            If DivisasSelecionadas(indexDivisa).Identificador = itemDivisa.Identificador Then
                ' borrar la divisa encuentrada, para que pueda añadir nuevamente con nuevos valores.
                DivisasSelecionadas.RemoveAt(indexDivisa)
                Exit For

            End If

        Next indexDivisa

        ' Adapatación para conversión: Al converter un IEnumerable para un List(of Type) es generado un error de conversión.
        Dim DivisaEnDivisaModificadas As New ObservableCollection(Of Clases.Divisa)
        DivisaEnDivisaModificadas.AddRange(DivisasSelecionadas.OrderBy(Function(f) f.Descripcion))

        ' añadir objeto nuevo en la sesión
        DivisasActualizadas.Clear()
        DivisasActualizadas.AddRange(DivisaEnDivisaModificadas)

        rptDivisaEfectivo.DataSource = DivisasActualizadas
        rptDivisaEfectivo.DataBind()

    End Sub

    ''' <summary>
    ''' Cargar componente DropDownList
    ''' </summary>
    ''' <param name="_combo"></param>
    ''' <param name="_ListaUnidad"></param>
    ''' <param name="_ListaCalidad"></param>
    ''' <remarks></remarks>
    Private Sub PreencherCombos(ByRef _combo As DropDownList, _
                       Optional _ListaUnidad As ObservableCollection(Of Clases.UnidadMedida) = Nothing, _
                       Optional _ListaCalidad As ObservableCollection(Of Clases.Calidad) = Nothing)

        With _combo

            If _ListaCalidad IsNot Nothing AndAlso _ListaCalidad.Count > 0 Then
                .DataSource = _ListaCalidad.OrderBy(Function(f) f.Descripcion)
                .DataTextField = "Descripcion"
                .DataValueField = "Codigo"
                .DataBind()

                If _ListaCalidad.Count = 1 Then _combo.Enabled = False Else _combo.Enabled = True

            ElseIf _ListaUnidad IsNot Nothing AndAlso _ListaUnidad.Count > 0 Then

                .DataSource = _ListaUnidad.OrderBy(Function(f) f.Descripcion)
                .DataTextField = "Descripcion"
                .DataValueField = "Codigo"
                .DataBind()

                If _ListaUnidad.Count = 1 Then _combo.Enabled = False Else _combo.Enabled = True

            End If

        End With

    End Sub

    ''' <summary>
    ''' Metodo para actualizar controles(TextBox o DropDownList)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarEstadoCampo(sender As Object)

        Select Case Modo
            Case Enumeradores.Modo.Alta, Enumeradores.Modo.Modificacion
                ActivarDesactivar(sender, True)

            Case Enumeradores.Modo.Consulta
                ActivarDesactivar(sender, False)

        End Select

    End Sub

    ''' <summary>
    ''' Actualizar Estado Campo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="bol"></param>
    ''' <remarks></remarks>
    Private Sub ActivarDesactivar(sender As Object, _
                                  bol As Boolean)

        Dim TextBox As TextBox = Nothing
        Dim CheckBox As CheckBox = Nothing
        Dim DropDownList As DropDownList = Nothing
        Dim Button As Button = Nothing
        Dim GridView As GridView = Nothing

        If TypeOf sender Is TextBox Then
            TextBox = DirectCast(sender, TextBox)
            TextBox.Enabled = bol

        ElseIf TypeOf sender Is DropDownList Then
            DropDownList = DirectCast(sender, DropDownList)
            DropDownList.Enabled = bol

        ElseIf TypeOf sender Is Button Then
            Button = DirectCast(sender, Button)
            Button.Visible = bol

        End If
    End Sub

    ''' <summary>
    ''' Calcular el valor total de la divisa por denominaciones
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="valorDenominacion"></param>
    ''' <param name="ImporteTotalDivisaDIC"></param>
    ''' <param name="hdfIdentificador"></param>
    ''' <param name="txtImporteTotal"></param>
    ''' <param name="TipoCalculo"></param>
    ''' <remarks></remarks> 
    Private Sub CalcularTotalDivisaPorDenominaciones(itemDivisa As Clases.Divisa, _
                                                     valorDenominacion As Clases.ValorDenominacion, _
                                                     ImporteTotalDivisaDIC As Dictionary(Of String, Decimal), _
                                                     hdfIdentificador As HiddenField, _
                                                     ByRef txtCantidad As TextBox, _
                                                     ByRef txtImporte As TextBox, _
                                                     ByRef txtImporteTotal As TextBox, _
                                            Optional TipoCalculo As Char = "")

        ' ejecuta suma total del importes de todas denominaciones de una divisa
        Dim Denominaciones As ObservableCollection(Of Clases.Denominacion) = itemDivisa.Denominaciones.Where(Function(D) D.ValorDenominacion IsNot Nothing).ToObservableCollection

        ImporteTotalDenominacion = (From _denominacion In Denominaciones
                                    From _valorDenominacion In _denominacion.ValorDenominacion
                                   Where _valorDenominacion.TipoValor = TipoValor
                                  Select _valorDenominacion.Importe).Sum

        ImporteTotalDivisa = ImporteTotalDivisaDIC.Where(Function(f) f.Key = hdfIdentificador.Value).FirstOrDefault.Value()

        Dim valorTotal As Decimal = ImporteTotalDivisa + ImporteTotalDenominacion

        Select Case TipoCalculo
            Case "S"
                txtCantidad.Text = If(valorDenominacion IsNot Nothing, valorDenominacion.Cantidad, "")
                txtImporte.Text = If(valorDenominacion IsNot Nothing, String.Format("{0:N2}", valorDenominacion.Importe), "")
        End Select

        ' rellenar el campo txtImporteTotal con a suma de (ImporteTotalDivisa + ImporteTotalDenominacion)

        txtImporteTotal.Text = String.Format("{0:N2}", valorTotal)

    End Sub

    ''' <summary>
    ''' Calcular el valor total de la divisa por divisa
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="hdfIdentificador"></param>
    ''' <param name="txtImporteDivisa"></param>
    ''' <param name="txtImporteTotal"></param>
    ''' <remarks></remarks>
    Private Sub CalcularTotalDivisaPorDivisa(itemDivisa As Clases.Divisa, _
                                             hdfIdentificador As HiddenField, _
                                             txtImporteDivisa As TextBox, _
                                             ByRef txtImporteTotal As TextBox)

        ' ejecuta suma total del importes de todas denominaciones de una divisa
        Dim Denominaciones As ObservableCollection(Of Clases.Denominacion) = itemDivisa.Denominaciones.Where(Function(D) D.ValorDenominacion IsNot Nothing).ToObservableCollection

        ImporteTotalDenominacion = (From _denominacion In Denominaciones
                                    From _valorDenominacion In _denominacion.ValorDenominacion
                                    Where _valorDenominacion.TipoValor = TipoValor
                                    Select _valorDenominacion.Importe).Sum

        ImporteTotalDivisa = If(Not String.IsNullOrEmpty(txtImporteDivisa.Text), txtImporteDivisa.Text, 0)
        ImporteTotalDivisaDIC(hdfIdentificador.Value) = ImporteTotalDivisa

        ' rellenar el campo txtImporteTotal con a suma de (ImporteTotalDivisa + ImporteTotalDenominacion)
        Dim valorTotal As Decimal = ImporteTotalDivisa + ImporteTotalDenominacion
        txtImporteTotal.Text = String.Format("{0:N2}", valorTotal)

    End Sub

    ''' <summary>
    ''' Actualizar valores de la divisa por el evento Cantidad
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="repeaterItemDenominacion"></param>
    ''' <param name="txtCantidad"></param>
    ''' <param name="txtImporteDenominacion"></param>
    ''' <param name="ddlUnidad"></param>
    ''' <param name="ddlCalidad"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresDivisaPorCantidad(ByRef itemDivisa As Clases.Divisa, _
                                                   repeaterItemDenominacion As RepeaterItem, _
                                                   ByRef txtCantidad As TextBox, _
                                                   ByRef txtImporteDenominacion As TextBox, _
                                                   ddlUnidad As DropDownList, _
                                                   ddlCalidad As DropDownList)

        Dim ValoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
        Dim ValorTotalDenominaciones As Decimal = 0
        Dim denominacion As Clases.Denominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex)

        If Not String.IsNullOrEmpty(txtCantidad.Text) Then
            If denominacion.Valor = 0 Then
                txtCantidad.Text = String.Empty
                txtImporteDenominacion.Text = String.Empty
                itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion = Nothing
                Throw New Excepcion.NegocioExcepcion(Traduzir("012_valordenominacion"))
            End If
            'Recupera o fator de multiplicação da UnidadMedida
            Dim _Fator As Integer = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First.ValorUnidad
            ' Ejecuta el calculo del importe
            txtImporteDenominacion.Text = txtCantidad.Text * _Fator * denominacion.Valor

            ' Añadir nuevos valores en el objeto
            Dim objValorDenominacion As New Clases.ValorDenominacion
            objValorDenominacion.Cantidad = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, 0)
            objValorDenominacion.Importe = If(Not String.IsNullOrEmpty(txtImporteDenominacion.Text), txtImporteDenominacion.Text, 0)
            objValorDenominacion.UnidadMedida = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).FirstOrDefault
            objValorDenominacion.Calidad = Calidad.Where(Function(f) f.Codigo = ddlCalidad.SelectedValue).FirstOrDefault

            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing AndAlso itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count > 0 Then
                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion
                Dim objValorDenominacionDC As Clases.ValorDenominacion = ValoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                If objValorDenominacionDC IsNot Nothing Then
                    ValoresDenominacion.Remove(objValorDenominacionDC)
                    objValorDenominacion.TipoValor = objValorDenominacionDC.TipoValor
                    objValorDenominacionDC = objValorDenominacion
                    ValoresDenominacion.Add(objValorDenominacionDC)

                Else
                    objValorDenominacion.TipoValor = TipoValor
                    ValoresDenominacion.Add(objValorDenominacion)

                End If

            Else
                objValorDenominacion.TipoValor = TipoValor
                ValoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                ValoresDenominacion.Add(objValorDenominacion)

            End If

        Else
            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing Then
                For index = 0 To itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count - 1

                    If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion(index).TipoValor = TipoValor Then

                        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.RemoveAt(index)
                        Exit For

                    End If

                Next index

                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion

            End If
            txtImporteDenominacion.Text = String.Empty
        End If

        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion = ValoresDenominacion

    End Sub

    ''' <summary>
    ''' Actualizar valores de la divisa por el evento Importe de denominacion
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="RepeaterItemDenominacion"></param>
    ''' <param name="Fator"></param>
    ''' <param name="txtImporteDenominacion"></param>
    ''' <param name="txtCantidad"></param>
    ''' <param name="txtImporteTotal"></param>
    ''' <param name="ddlUnidad"></param>
    ''' <param name="ddlCalidad"></param>
    ''' <param name="hdfIdentificador"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresDivisaPorImporte(ByRef itemDivisa As Clases.Divisa, _
                                                  RepeaterItemDenominacion As RepeaterItem, _
                                                  Fator As Integer, _
                                                  ByRef txtImporteDenominacion As TextBox, _
                                                  ByRef txtCantidad As TextBox, _
                                                  ByRef txtImporteTotal As TextBox, _
                                                  ddlUnidad As DropDownList, _
                                                  ddlCalidad As DropDownList, _
                                                  hdfIdentificador As HiddenField)

        Dim ValoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
        Dim ValorTotalDenominaciones As Decimal = 0
        Dim denominacion As Clases.Denominacion = itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex)

        If Not String.IsNullOrEmpty(txtImporteDenominacion.Text) AndAlso Not CDbl(txtImporteDenominacion.Text) = 0.0 Then
            If denominacion.Valor = 0 Then
                txtCantidad.Text = String.Empty
                txtImporteDenominacion.Text = String.Empty
                itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion = Nothing
                Throw New Excepcion.NegocioExcepcion(Traduzir("012_valordenominacion"))
            End If
            If (CDbl(txtImporteDenominacion.Text) / denominacion.Valor) Mod Fator <> 0 Then
                Dim _valorDigitado As String = txtImporteDenominacion.Text

                Dim valorDenominacion = itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion
                Dim objValorDenominacion As Clases.ValorDenominacion = Nothing
                If valorDenominacion IsNot Nothing Then
                    objValorDenominacion = valorDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault()
                    CalcularTotalDivisaPorDenominaciones(itemDivisa, objValorDenominacion, ImporteTotalDivisaDIC, hdfIdentificador, txtCantidad, txtImporteDenominacion, txtImporteTotal, "S")
                Else
                    txtCantidad.Text = String.Empty
                    txtImporteDenominacion.Text = String.Empty
                End If
                Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("012_divisioninvalida"), _valorDigitado, Fator, denominacion.Valor))

            Else
                txtCantidad.Text = txtImporteDenominacion.Text / Fator / denominacion.Valor
                Dim objValorDenominacion As New Clases.ValorDenominacion
                objValorDenominacion.Cantidad = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, 0)
                objValorDenominacion.Importe = If(Not String.IsNullOrEmpty(txtImporteDenominacion.Text), txtImporteDenominacion.Text, 0)
                objValorDenominacion.UnidadMedida = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).FirstOrDefault
                objValorDenominacion.Calidad = Calidad.Where(Function(f) f.Codigo = ddlCalidad.SelectedValue).FirstOrDefault

                If itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing AndAlso itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion.Count > 0 Then
                    ValoresDenominacion = itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion
                    Dim objValorDenominacionDC As Clases.ValorDenominacion = ValoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                    If objValorDenominacionDC IsNot Nothing Then
                        ValoresDenominacion.Remove(objValorDenominacionDC)
                        objValorDenominacion.TipoValor = objValorDenominacionDC.TipoValor
                        objValorDenominacionDC = objValorDenominacion
                        ValoresDenominacion.Add(objValorDenominacionDC)

                    Else
                        objValorDenominacion.TipoValor = TipoValor
                        ValoresDenominacion.Add(objValorDenominacion)

                    End If

                Else
                    objValorDenominacion.TipoValor = TipoValor
                    ValoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                    ValoresDenominacion.Add(objValorDenominacion)

                End If

            End If
        Else
            If itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing Then
                For index = 0 To itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion.Count - 1
                    If itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion(index).TipoValor = TipoValor Then
                        itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion.RemoveAt(index)
                        Exit For

                    End If

                Next index

                ValoresDenominacion = itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion

            End If
            txtCantidad.Text = String.Empty
        End If

        itemDivisa.Denominaciones(RepeaterItemDenominacion.ItemIndex).ValorDenominacion = ValoresDenominacion

    End Sub

    ''' <summary>
    ''' Actualizar valores de la divisa por el evento UnidadMedida
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="repeaterItemDenominacion"></param>
    ''' <param name="txtCantidad"></param>
    ''' <param name="txtImporteDenominacion"></param>
    ''' <param name="ddlUnidad"></param>
    ''' <param name="ddlCalidad"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresDivisaPorUnidadMedida(ByRef itemDivisa As Clases.Divisa, _
                                                       repeaterItemDenominacion As RepeaterItem, _
                                                       ByRef txtCantidad As TextBox, _
                                                       ByRef txtImporteDenominacion As TextBox, _
                                                       ddlUnidad As DropDownList, _
                                                       ddlCalidad As DropDownList)

        Dim itemDenominacion As Clases.Denominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex)
        Dim ValoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
        Dim ValorTotalDenominaciones As Decimal = 0

        'Dim _Fator As Integer = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First.ValorUnidad
        'lblCantidad.Text = "" & Traduzir("012_cantidad") & " (x" & UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First.ValorUnidad & ")"

        If Not String.IsNullOrEmpty(txtCantidad.Text) AndAlso Not CInt(txtCantidad.Text) = 0 Then

            txtImporteDenominacion.Text = txtCantidad.Text * UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First.ValorUnidad * itemDenominacion.Valor

            Dim objValorDenominacion As New Clases.ValorDenominacion
            objValorDenominacion.Cantidad = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, 0)
            objValorDenominacion.UnidadMedida = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First
            objValorDenominacion.Calidad = Calidad.Where(Function(f) f.Codigo = ddlCalidad.SelectedValue).First
            objValorDenominacion.Importe = If(Not String.IsNullOrEmpty(txtImporteDenominacion.Text), txtImporteDenominacion.Text, 0)

            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing AndAlso itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count > 0 Then
                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion
                Dim objValorDenominacionDC As Clases.ValorDenominacion = ValoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                If objValorDenominacionDC IsNot Nothing Then
                    ValoresDenominacion.Remove(objValorDenominacionDC)
                    objValorDenominacion.TipoValor = objValorDenominacionDC.TipoValor
                    objValorDenominacionDC = objValorDenominacion
                    ValoresDenominacion.Add(objValorDenominacionDC)

                Else
                    objValorDenominacion.TipoValor = TipoValor
                    ValoresDenominacion.Add(objValorDenominacion)

                End If

            Else
                objValorDenominacion.TipoValor = TipoValor
                ValoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                ValoresDenominacion.Add(objValorDenominacion)

            End If

        Else
            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing Then
                For index = 0 To itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count - 1

                    If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion(index).TipoValor = TipoValor Then

                        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.RemoveAt(index)

                        Exit For

                    End If

                Next index

                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion

            End If

        End If

        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion = ValoresDenominacion
    End Sub

    ''' <summary>
    ''' Actualizar valores de la divisa por el evento Calidad
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="repeaterItemDenominacion"></param>
    ''' <param name="txtCantidad"></param>
    ''' <param name="txtImporteDenominacion"></param>
    ''' <param name="ddlUnidad"></param>
    ''' <param name="ddlCalidad"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresDivisaPorCalidad(ByRef itemDivisa As Clases.Divisa, _
                                                       repeaterItemDenominacion As RepeaterItem, _
                                                       ByRef txtCantidad As TextBox, _
                                                       ByRef txtImporteDenominacion As TextBox, _
                                                       ddlUnidad As DropDownList, _
                                                       ddlCalidad As DropDownList)

        Dim ValoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion) = Nothing
        Dim ValorTotalDenominaciones As Decimal = 0

        'Dim _Fator As Integer = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First.ValorUnidad
        'lblCantidad.Text = "" & Traduzir("012_cantidad") & " (x" & UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).FirstOrDefault.ValorUnidad & ")"

        If Not String.IsNullOrEmpty(txtCantidad.Text) AndAlso Not CInt(txtCantidad.Text) = 0 Then

            Dim objValorDenominacion As New Clases.ValorDenominacion
            objValorDenominacion.Cantidad = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, 0)
            objValorDenominacion.UnidadMedida = UnidadMedida.Where(Function(f) f.Codigo = ddlUnidad.SelectedValue).First
            objValorDenominacion.Calidad = Calidad.Where(Function(f) f.Codigo = ddlCalidad.SelectedValue).First
            objValorDenominacion.Importe = If(Not String.IsNullOrEmpty(txtImporteDenominacion.Text), txtImporteDenominacion.Text, 0)

            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing AndAlso itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count > 0 Then
                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion
                Dim objValorDenenominacionDC As Clases.ValorDenominacion = ValoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                If objValorDenenominacionDC IsNot Nothing Then

                    ValoresDenominacion.Remove(objValorDenenominacionDC)
                    objValorDenominacion.TipoValor = objValorDenenominacionDC.TipoValor
                    objValorDenenominacionDC = objValorDenominacion
                    ValoresDenominacion.Add(objValorDenenominacionDC)

                Else
                    objValorDenominacion.TipoValor = TipoValor
                    ValoresDenominacion.Add(objValorDenominacion)

                End If

            Else
                objValorDenominacion.TipoValor = TipoValor
                ValoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                ValoresDenominacion.Add(objValorDenominacion)

            End If

        Else
            If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion IsNot Nothing Then
                For index = 0 To itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.Count - 1

                    If itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion(index).TipoValor = TipoValor Then

                        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion.RemoveAt(index)

                        Exit For

                    End If

                Next index

                ValoresDenominacion = itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion

            End If

        End If

        itemDivisa.Denominaciones(repeaterItemDenominacion.ItemIndex).ValorDenominacion = ValoresDenominacion

    End Sub

    ''' <summary>
    ''' Actualizar valores de la divisa por el Importe de divisa
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="txtImporteDivisa"></param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresDivisaPorImporteDivisa(ByRef itemDivisa As Clases.Divisa, _
                                                        ByRef txtImporteDivisa As TextBox)

        Dim ValoresDivisa = itemDivisa.ValoresTotalesEfectivo
        Dim objValorDivisa As New Clases.ValorEfectivo

        If Not String.IsNullOrEmpty(txtImporteDivisa.Text) AndAlso Not CDbl(txtImporteDivisa.Text) = 0.0 Then
            ImporteTotalDivisa = txtImporteDivisa.Text
            objValorDivisa.Importe = txtImporteDivisa.Text


            If itemDivisa.ValoresTotalesEfectivo IsNot Nothing AndAlso itemDivisa.ValoresTotalesEfectivo.Count > 0 Then
                ValoresDivisa = itemDivisa.ValoresTotalesEfectivo
                Dim objValorDivisaDC = ValoresDivisa.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                If objValorDivisaDC IsNot Nothing Then
                    ValoresDivisa.Remove(objValorDivisaDC)
                    objValorDivisa.TipoValor = objValorDivisaDC.TipoValor
                    objValorDivisaDC = objValorDivisa
                    ValoresDivisa.Add(objValorDivisaDC)

                Else
                    objValorDivisa.TipoValor = TipoValor
                    ValoresDivisa.Add(objValorDivisa)

                End If

            Else
                objValorDivisa.TipoValor = TipoValor
                ValoresDivisa = New ObservableCollection(Of Clases.ValorEfectivo)
                ValoresDivisa.Add(objValorDivisa)

            End If

        Else
            If itemDivisa.ValoresTotalesEfectivo IsNot Nothing Then
                For index = 0 To itemDivisa.ValoresTotalesEfectivo.Count - 1

                    If itemDivisa.ValoresTotalesEfectivo(index).TipoValor = TipoValor Then

                        itemDivisa.ValoresTotalesEfectivo.RemoveAt(index)

                        Exit For

                    End If

                Next index

                ValoresDivisa = itemDivisa.ValoresTotalesEfectivo

            End If

        End If

        itemDivisa.ValoresTotalesEfectivo = ValoresDivisa

    End Sub

    ''' <summary>
    ''' Tratamentos na pagina de acordo com o Modo
    ''' </summary>
    ''' <param name="ddlUnidad">Controle ddlUnidadMedida</param>
    ''' <param name="ddlCalidad">Controle ddlCalidad</param>
    ''' <param name="ValorDenominacion">Clase ValorDenominacion</param>
    ''' <remarks></remarks>
    Private Sub TratamentosModoEnItemDataBound(ByRef ddlUnidad As DropDownList, _
                                               ByRef ddlCalidad As DropDownList, _
                                               ValorDenominacion As Clases.ValorDenominacion,
                                               TipoDenominacion As Enumeradores.TipoUnidadMedida)

        Dim UnidadMedidaActualizada As ObservableCollection(Of Clases.UnidadMedida) = UnidadMedida.Where(Function(f) f.TipoUnidadMedida = TipoDenominacion).ToObservableCollection

        Select Case Modo
            Case Comon.Enumeradores.Modo.Alta, Enumeradores.Modo.Modificacion
                PreencherCombos(ddlUnidad, UnidadMedidaActualizada)
                PreencherCombos(ddlCalidad, , Calidad)

                ' caso es modificacion, cheques si el objeto ValorDenominacion está rellenado, si no, el valor selecionado es el padron del objeto UnidadMedida
                If ValorDenominacion IsNot Nothing Then

                    If ValorDenominacion.UnidadMedida IsNot Nothing Then
                        ddlUnidad.SelectedValue = UnidadMedidaActualizada.Where(Function(c) c.Identificador = ValorDenominacion.UnidadMedida.Identificador).FirstOrDefault.Codigo
                    Else
                        ddlUnidad.SelectedValue = UnidadMedidaActualizada.Where(Function(c) c.EsPadron = True).FirstOrDefault.Codigo
                    End If

                    If ValorDenominacion.Calidad IsNot Nothing Then
                        ddlCalidad.SelectedValue = Calidad.Where(Function(c) c.Identificador = ValorDenominacion.Calidad.Identificador).FirstOrDefault.Codigo
                    End If

                Else
                    If UnidadMedidaActualizada.Exists(Function(x) x.EsPadron = True) Then
                        ddlUnidad.SelectedValue = UnidadMedidaActualizada.Where(Function(f) f.EsPadron = True).FirstOrDefault.Codigo
                    End If

                End If

            Case Comon.Enumeradores.Modo.Consulta

                ' caso es modificacion, cheques si el objeto ValorDenominacion está rellenado, si no, el valor selecionado es el padron del objeto UnidadMedida
                If ValorDenominacion IsNot Nothing Then

                    If ValorDenominacion.UnidadMedida IsNot Nothing Then
                        If UnidadMedidaActualizada IsNot Nothing Then
                            UnidadMedidaActualizada.Clear()
                            UnidadMedidaActualizada.Add(ValorDenominacion.UnidadMedida)
                            PreencherCombos(ddlUnidad, UnidadMedidaActualizada)
                            ddlUnidad.SelectedValue = UnidadMedidaActualizada.Where(Function(c) c.Identificador = ValorDenominacion.UnidadMedida.Identificador).FirstOrDefault.Codigo
                        End If

                    End If

                    If ValorDenominacion.Calidad IsNot Nothing Then
                        Calidad.Clear()
                        Calidad.Add(ValorDenominacion.Calidad)
                        PreencherCombos(ddlCalidad, , Calidad)
                        ddlCalidad.SelectedValue = Calidad.Where(Function(c) c.Identificador = ValorDenominacion.Calidad.Identificador).FirstOrDefault.Codigo

                    End If

                End If

        End Select

    End Sub


    ''' <summary>
    ''' Metodo para actualizar los valores rellenados en la pantalla para el objeto.
    ''' Esto método percorre todos los controles de la pantalla para recuperar sus valores.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ActualizarValoresDivisas()

        If DivisasActualizadas IsNot Nothing AndAlso DivisasActualizadas.Count > 0 Then
            Dim Divisas As New ObservableCollection(Of Clases.Divisa)

            For Each _itemBound As RepeaterItem In rptDivisaEfectivo.Items
                Dim itemDivisaAux As Clases.Divisa = DivisasActualizadas(_itemBound.ItemIndex)

                Dim txtImporteDivisa As TextBox = DirectCast(_itemBound.FindControl("txtImporteDivisa"), TextBox)
                Dim repeaterItem As Repeater = _itemBound.FindControl("rptDetalle")
                Dim Denominaciones As New ObservableCollection(Of Clases.Denominacion)

                For Each _itemBoundEfectivo As RepeaterItem In repeaterItem.Items
                    If _itemBoundEfectivo.ItemType = ListItemType.Item OrElse _itemBoundEfectivo.ItemType = ListItemType.AlternatingItem Then

                        Dim hdfIdentificadorDenominacion As HiddenField = DirectCast(_itemBoundEfectivo.FindControl("hdfIdentificadorDenominacion"), HiddenField)
                        Dim itemDenominacionAux As Clases.Denominacion = itemDivisaAux.Denominaciones.Where(Function(f) f.Identificador = hdfIdentificadorDenominacion.Value).FirstOrDefault

                        Dim ddlUnidad As DropDownList = DirectCast(_itemBoundEfectivo.FindControl("ddlUnidad"), DropDownList)
                        Dim txtCantidad As TextBox = DirectCast(_itemBoundEfectivo.FindControl("txtCantidad"), TextBox)
                        Dim txtImporte As TextBox = DirectCast(_itemBoundEfectivo.FindControl("txtImporteDenominacion"), TextBox)
                        Dim ddlCalidad As DropDownList = DirectCast(_itemBoundEfectivo.FindControl("ddlCalidad"), DropDownList)

                        Dim UnidadeMedida As Clases.UnidadMedida = UnidadMedida.Where(Function(u) u.Codigo = ddlUnidad.SelectedValue).FirstOrDefault
                        Dim Calidade As Clases.Calidad = Nothing
                        If ddlCalidad.Visible Then
                            Calidade = Calidad.Where(Function(c) c.Codigo = ddlCalidad.SelectedValue).FirstOrDefault()
                        End If

                        If Not String.IsNullOrEmpty(txtCantidad.Text) Then
                            CargarValoresDenominacion(itemDenominacionAux, txtCantidad.Text, txtImporte.Text, UnidadeMedida, Calidade)
                            Denominaciones.Add(itemDenominacionAux)
                        End If
                    End If

                Next

                If itemDivisaAux.Denominaciones IsNot Nothing Then
                    itemDivisaAux.Denominaciones.Clear()
                End If
                If itemDivisaAux.ValoresTotalesEfectivo IsNot Nothing Then
                    itemDivisaAux.ValoresTotalesEfectivo.Clear()
                End If

                itemDivisaAux.Denominaciones.AddRange(OrdenarDenominaciones(Denominaciones))
                If Not String.IsNullOrEmpty(txtImporteDivisa.Text) Then
                    CargarValoresTotalesEfectivo(itemDivisaAux, txtImporteDivisa.Text)
                End If
                Divisas.Add(itemDivisaAux)

            Next
            DivisasActualizadas = Divisas
        End If
    End Sub


    ''' <summary>
    ''' Cargar en el objeto los valores totales de la divisa rellenados en la pantalla.
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="ImporteTotal">valor del importe total</param>
    ''' <remarks></remarks>
    Private Sub CargarValoresTotalesEfectivo(ByRef itemDivisa As Clases.Divisa, _
                                           ByRef ImporteTotal As String)

        Dim TotalesEfectivo = itemDivisa.ValoresTotalesEfectivo
        Dim objValorEfectivo As New Clases.ValorEfectivo

        objValorEfectivo.Importe = If(Not String.IsNullOrEmpty(ImporteTotal), ImporteTotal, Nothing)

        If itemDivisa.ValoresTotalesDivisa IsNot Nothing AndAlso itemDivisa.ValoresTotalesDivisa.Count > 0 Then
            TotalesEfectivo = itemDivisa.ValoresTotalesEfectivo
            Dim objValorEfectivoDC = TotalesEfectivo.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

            If objValorEfectivoDC IsNot Nothing Then
                TotalesEfectivo.Remove(objValorEfectivoDC)
                objValorEfectivo.TipoValor = objValorEfectivoDC.TipoValor
                objValorEfectivoDC = objValorEfectivo
                TotalesEfectivo.Add(objValorEfectivoDC)

            Else
                objValorEfectivo.TipoValor = TipoValor
                TotalesEfectivo.Add(objValorEfectivo)

            End If

        Else
            objValorEfectivo.TipoValor = TipoValor
            TotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
            TotalesEfectivo.Add(objValorEfectivo)

        End If

        itemDivisa.ValoresTotalesEfectivo = TotalesEfectivo

    End Sub

    ''' <summary>
    ''' Cargar en el objeto los valores totales de la divisa rellenados en la pantalla.
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="ImporteTotal">valor del importe total</param>
    ''' <remarks></remarks>
    Private Sub CargarValoresTotalesDivisa(ByRef itemDivisa As Clases.Divisa, _
                                           ByRef ImporteTotal As String)

        Dim ValoresDivisa = itemDivisa.ValoresTotalesDivisa
        Dim objValorDivisa As New Clases.ValorDivisa

        objValorDivisa.Importe = If(Not String.IsNullOrEmpty(ImporteTotal), ImporteTotal, Nothing)

        If itemDivisa.ValoresTotalesDivisa IsNot Nothing AndAlso itemDivisa.ValoresTotalesDivisa.Count > 0 Then
            ValoresDivisa = itemDivisa.ValoresTotalesDivisa
            Dim objValorDivisaDC = ValoresDivisa.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

            If objValorDivisaDC IsNot Nothing Then
                ValoresDivisa.Remove(objValorDivisaDC)
                objValorDivisa.TipoValor = objValorDivisaDC.TipoValor
                objValorDivisaDC = objValorDivisa
                ValoresDivisa.Add(objValorDivisaDC)

            Else
                objValorDivisa.TipoValor = TipoValor
                ValoresDivisa.Add(objValorDivisa)

            End If

        Else
            objValorDivisa.TipoValor = TipoValor
            ValoresDivisa = New ObservableCollection(Of Clases.ValorDivisa)
            ValoresDivisa.Add(objValorDivisa)

        End If

        itemDivisa.ValoresTotalesDivisa = ValoresDivisa

    End Sub

    ''' <summary>
    ''' Ejecutar ordenación de las denominaciones
    ''' </summary>
    ''' <param name="Denominaciones">Lista de denominaciones</param>
    ''' <returns>Lista de denominaciones</returns>
    ''' <remarks></remarks>
    Private Function OrdenarDenominaciones(Denominaciones As ObservableCollection(Of Clases.Denominacion)) As ObservableCollection(Of Clases.Denominacion)

        Return (From den In Denominaciones
                Order By den.EsBillete Descending, den.Valor Descending
                Select den)

    End Function

    ''' <summary>
    ''' Cargar en la denominacion los valores de las denominaciones rellenados en la pantalla.
    ''' </summary>
    ''' <param name="itemDenominacion">Clase denominacion</param>
    ''' <param name="Cantidad">valor de la cantidad</param>
    ''' <param name="Importe">valor del importe</param>
    ''' <param name="UnidadeMedida">Clase unidad medida</param>
    ''' <param name="Calidade">Clase calidad</param>
    ''' <remarks></remarks>
    Private Sub CargarValoresDenominacion(ByRef itemDenominacion As Clases.Denominacion, _
                                          Cantidad As String, _
                                          Importe As String, _
                                          UnidadeMedida As Clases.UnidadMedida, _
                                          Calidade As Clases.Calidad)

        Dim valoresDenominacion As ObservableCollection(Of Clases.ValorDenominacion)
        Dim objValorDenominacion As New Clases.ValorDenominacion
        objValorDenominacion.Cantidad = If(Not String.IsNullOrEmpty(Cantidad), Cantidad, Nothing)
        objValorDenominacion.Importe = If(Not String.IsNullOrEmpty(Importe), Importe, Nothing)
        objValorDenominacion.UnidadMedida = UnidadeMedida
        objValorDenominacion.Calidad = Calidade

        If itemDenominacion.ValorDenominacion IsNot Nothing AndAlso itemDenominacion.ValorDenominacion.Count > 0 Then
            valoresDenominacion = itemDenominacion.ValorDenominacion
            Dim objValorDenominacionDC As Clases.ValorDenominacion = valoresDenominacion.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

            If objValorDenominacionDC IsNot Nothing Then
                valoresDenominacion.Remove(objValorDenominacionDC)
                objValorDenominacion.TipoValor = objValorDenominacionDC.TipoValor
                objValorDenominacionDC = objValorDenominacion
                valoresDenominacion.Add(objValorDenominacionDC)

            Else
                objValorDenominacion.TipoValor = TipoValor
                valoresDenominacion.Add(objValorDenominacion)

            End If

        Else
            objValorDenominacion.TipoValor = TipoValor
            valoresDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
            valoresDenominacion.Add(objValorDenominacion)

        End If

        itemDenominacion.ValorDenominacion = valoresDenominacion

    End Sub

    ''' <summary>
    ''' Classe privada para passar Jason para javascript.
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ParametrosJSON
        Public MensagemDenominacionSemValor As String
        Public MensagemDivisioninvalida As String
        Public SeparadorDecimal As String
        Public SeparadorMilhar As String
        Public UnidadesMedidas As List(Of Unidade)
    End Class

    Public Class Unidade
        ''' <summary>
        ''' Codigo
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Codigo As String

        ''' <summary>
        ''' Valor
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property Valor As String
    End Class
#End Region

End Class