Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports System.Web.UI.WebControls
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucDivisaMedioPago
    Inherits UcBase

#Region "Variaveis"

    ''' <summary>
    ''' Divisas del Cliente
    ''' </summary>
    ''' <remarks></remarks>
    Private _DivisasIAC As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    ''' Divisas selecionadas no Popup Agregar Divisa.
    ''' </summary>
    ''' <remarks></remarks>
    Private DivisasSelecionadas As ObservableCollection(Of Clases.Divisa)

    ''' <summary>
    ''' Recibe el retorno del controle ucAgregarDivisa.
    ''' </summary>
    ''' <remarks></remarks>
    Private ListaDivisasSelecionadas As New ObservableCollection(Of Clases.Divisa)

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
    ''' Expresión regular para validación del termino del medio pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property RegExpr As Dictionary(Of String, String)
        Get
            If ViewState("RegExpr") Is Nothing Then
                ViewState("RegExpr") = New Dictionary(Of String, String)
            End If
            Return ViewState("RegExpr")
        End Get
        Set(value As Dictionary(Of String, String))
            ViewState("RegExpr") = value
        End Set
    End Property

    ''' <summary>
    ''' Flag para validar sy ya fue ejecutado el DataBind
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EsDataBind As Boolean
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
    ''' Divisas que podrán ser cambiadas en tiempo de ejecución.
    ''' Solamente será sustituido en el objeto principal de la pagina si el usuario clicar en el boton grabar.
    ''' </summary>
    Public Property DivisasIAC As ObservableCollection(Of Clases.Divisa)
        Get
            Return _DivisasIAC
        End Get
        Set(value As ObservableCollection(Of Clases.Divisa))
            _DivisasIAC = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento Load do Controle ucMedioPago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            DivisasActualizadas = DivisasIAC
        End If

        If Not IsPostBack OrElse Not EsDataBind Then
            If DivisasActualizadas IsNot Nothing Then
                Aplicacao.Util.Utilidad.OrdenarItemsDivisas(DivisasActualizadas)
                If DivisasActualizadas.Exists(Function(f) f.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso f.ValoresTotalesTipoMedioPago.Count > 0) OrElse _
                   DivisasActualizadas.Exists(Function(f) f.MediosPago IsNot Nothing AndAlso f.MediosPago.Count > 0) Then

                    rptDivisa.DataSource = DivisasActualizadas
                    rptDivisa.DataBind()
                    EsDataBind = True
                Else
                    If DivisasActualizadas.Exists(Function(f) (f.MediosPago IsNot Nothing AndAlso f.MediosPago.Count = 0) OrElse f.MediosPago Is Nothing) Then DivisasActualizadas = Nothing
                End If
            End If
        End If

    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Dim itemDivisaAux As Clases.Divisa = Nothing

    '    ' loop en los itens del repeater de divisas, o sea, cada divisa rellenada en el controle
    '    For Each _itemBound As RepeaterItem In rptDivisa.Items
    '        ' recupera la divisa currente
    '        itemDivisaAux = DivisasActualizadas(_itemBound.ItemIndex)
    '        Dim repeaterItem As Repeater = _itemBound.FindControl("rptDetalle")

    '        Dim ListaAgrupada As New List(Of List(Of Clases.MedioPago))
    '        If itemDivisaAux IsNot Nothing AndAlso itemDivisaAux.MediosPago IsNot Nothing Then
    '            Dim _ListaMediosPago As New List(Of Clases.MedioPago)
    '            _ListaMediosPago.AddRange(itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion))
    '            _ListaMediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList)))
    '        End If

    '        Aplicacao.Util.Utilidad.ConfigurarTabIndex(_itemBound.FindControl("btnBorrarDivisa"))

    '        ' loop en los itens de la divisa. {Tipo de MediosPago} 
    '        For Each _itemBoundMedioPago As RepeaterItem In repeaterItem.Items

    '            If _itemBoundMedioPago.ItemType = ListItemType.Item OrElse _itemBoundMedioPago.ItemType = ListItemType.AlternatingItem Then

    '                Dim hdfIdentificadorTipo As HiddenField = _itemBoundMedioPago.FindControl("hdfIdentificadorTipo")
    '                Dim Tipo() As String = hdfIdentificadorTipo.Value.Split("_")

    '                Dim ListaPorTipo As New List(Of Clases.MedioPago)
    '                For Each itens In ListaAgrupada
    '                    For Each item In itens
    '                        If item.Tipo.ToString = Tipo(1) Then
    '                            ListaPorTipo.Add(item)
    '                        End If
    '                    Next
    '                Next

    '                Dim chkMostrarTerminos As CheckBox = Nothing

    '                ' loop en los mediospago de la divisa currente
    '                For Each _itemMedioPago In ListaPorTipo.OrderBy(Function(f) f.Descripcion) 'itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion)
    '                    ' copia del itemMedioPago
    '                    Dim itemMedioPagoAux As Clases.MedioPago = _itemMedioPago

    '                    ' recupera los controles de repeateritem
    '                    Dim txtImporteTpMedioPago As TextBox = _itemBoundMedioPago.FindControl("txtImporteTpMedioPago_" & itemDivisaAux.Identificador & "_" & itemMedioPagoAux.Identificador)
    '                    Dim txtImporte As TextBox = _itemBoundMedioPago.FindControl("txtImporte_" & itemMedioPagoAux.Identificador)
    '                    Dim txtCantidad As TextBox = _itemBoundMedioPago.FindControl("txtCantidad_" & itemMedioPagoAux.Identificador)

    '                    Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtImporteTpMedioPago)

    '                    If chkMostrarTerminos Is Nothing Then
    '                        chkMostrarTerminos = _itemBoundMedioPago.FindControl("chkMostrarTerminos_" & itemMedioPagoAux.Identificador)
    '                        Aplicacao.Util.Utilidad.ConfigurarTabIndex(chkMostrarTerminos)
    '                    End If

    '                    Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtImporte, txtCantidad)

    '                    If chkMostrarTerminos IsNot Nothing AndAlso chkMostrarTerminos.Checked Then

    '                        If itemMedioPagoAux.Terminos IsNot Nothing AndAlso itemMedioPagoAux.Terminos.Count > 0 Then

    '                            ' loop en los terminos del medio pago currente
    '                            For Each _itemTtermino In _itemMedioPago.Terminos

    '                                Dim itemTerminoAux As Clases.Termino = _itemTtermino

    '                                Dim valor As String = String.Empty
    '                                ' recupera el controle correspondiente al termino
    '                                Dim txtControl As WebControl = _itemBoundMedioPago.FindControl("fTermino_" & itemMedioPagoAux.Identificador & "_" & itemTerminoAux.Identificador)
    '                                Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtControl)
    '                            Next _itemTtermino

    '                        End If

    '                    End If

    '                Next _itemMedioPago
    '            End If
    '        Next _itemBoundMedioPago

    '    Next _itemBound
    'End Sub

    ''' <summary>
    ''' ItemCreated Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptDivisa_ItemCreated(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptDivisa.ItemCreated
        Try

            Dim repeaterDetalle As Repeater = e.Item.FindControl("rptDetalle")

            Dim hdfIdentificador As HiddenField = e.Item.FindControl("hdfIdentificador")
            Dim lblFiltroDivisa As Label = e.Item.FindControl("lblFiltroDivisa")
            Dim imgDivisa As System.Web.UI.WebControls.Image = e.Item.FindControl("imgDivisa")
            Dim lblDivisa As Label = e.Item.FindControl("lblDivisa")
            Dim lblCodIso As Label = e.Item.FindControl("lblCodIso")
            Dim txtDivisa As TextBox = e.Item.FindControl("txtDivisa")
            Dim txtCodIso As TextBox = e.Item.FindControl("txtCodIso")
            Dim btnBorrarDivisa As Button = e.Item.FindControl("btnBorrarDivisa")

            lblDivisa.Text = Traduzir("012_descripcion")
            lblCodIso.Text = Traduzir("012_codigo")
            btnBorrarDivisa.Text = Traduzir("012_borrardivisa")

            Dim itemDivisa As Clases.Divisa = If(e.Item.DataItem IsNot Nothing, e.Item.DataItem, DivisasActualizadas(e.Item.ItemIndex)) 'DivisasActualizadas(e.Item.ItemIndex)
            txtDivisa.ID = "txtDivisa_" & itemDivisa.Identificador
            hdfIdentificador.Value = itemDivisa.Identificador
            lblFiltroDivisa.Text = itemDivisa.Descripcion ' & " - " & itemDivisa.CodigoISO
            txtDivisa.Text = itemDivisa.Descripcion
            txtCodIso.Text = itemDivisa.CodigoISO

            Aplicacao.Util.Utilidad.CargarImagenDivisa(itemDivisa, imgDivisa)

            ActualizarEstadoCampo(btnBorrarDivisa)

            RemoveHandler repeaterDetalle.ItemDataBound, AddressOf repeaterDetalle_ItemDataBound
            AddHandler repeaterDetalle.ItemDataBound, AddressOf repeaterDetalle_ItemDataBound
            RemoveHandler btnBorrarDivisa.Click, AddressOf btnBorrarDivisa_Click
            AddHandler btnBorrarDivisa.Click, AddressOf btnBorrarDivisa_Click

            ' Lista de medios pago por tipo
            ' Ex: Cheque
            '         0
            '         1
            '     Tarjeta
            '         0
            Dim ListaAgrupada As New List(Of List(Of Clases.MedioPago))

            Dim ListaMediosPago As List(Of Clases.MedioPago)
            ' Lista de todos los mediospago da divisa ordenado por descripción
            If itemDivisa.MediosPago IsNot Nothing Then
                ListaMediosPago = New List(Of Clases.MedioPago)(itemDivisa.MediosPago.OrderBy(Function(f) f.Descripcion))
                ' Crear una lista de medios pagos agrupados por tipo.
                ' itemDivisa.MediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList)))
                ListaMediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList)))
            End If

            ' Adapatación para exibir solamente el total del mediopago.
            If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.Baja Then
                ExibirSolamenteTotal(itemDivisa, ListaAgrupada)
            End If

            repeaterDetalle.DataSource = ListaAgrupada
            repeaterDetalle.DataBind()
        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try
    End Sub

    ''' <summary>
    ''' ItemDataBound Tipos del medio pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub repeaterDetalle_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            ' recuperar los controles en la pagina.
            Dim hdfIdentificadorTipo As HiddenField = e.Item.FindControl("hdfIdentificadorTipo")
            Dim lblFiltroTipoMedioPago As Label = e.Item.FindControl("lblFiltroTipoMedioPago")
            Dim lblCodTpMedioPago As Label = e.Item.FindControl("lblCodTpMedioPago")
            Dim lblImporteTpMedioPago As Label = e.Item.FindControl("lblImporteTpMedioPago")
            Dim txtCodTpMedioPago As TextBox = e.Item.FindControl("txtCodTpMedioPago")
            Dim imgTpMedioPago As Image = e.Item.FindControl("imgTpMedioPago")
            Dim txtImporteTpMedioPago As TextBox = e.Item.FindControl("txtImporteTpMedioPago")
            Dim lblchkMostrarTerminos As Label = e.Item.FindControl("lblchkMostrarTerminos")
            Dim chkMostrarTerminos As CheckBox = e.Item.FindControl("chkMostrarTerminos")
            Dim lblMedioPago As Label = e.Item.FindControl("lblMedioPago")
            Dim fieldsetMedioPago As HtmlGenericControl = e.Item.FindControl("fieldsetMedioPago")

            ' Panel donde se quedan los detalles del mediopago
            Dim pnlDetallesMedioPago As Panel = e.Item.FindControl("pnlDetallesMedioPago")

            ' traduzir controles
            lblchkMostrarTerminos.Text = Traduzir("012_InfoDetalle")
            lblCodTpMedioPago.Text = Traduzir("012_descripcion")
            lblImporteTpMedioPago.Text = Traduzir("012_importe")

            ' Recupera el item del repeater para busqueda del identificador de la divisa
            Dim repeaterItemDivisa As RepeaterItem = DirectCast(sender, Repeater).NamingContainer

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificador As HiddenField = repeaterItemDivisa.FindControl("hdfIdentificador")
            hdfIdentificadorTipo.Value = hdfIdentificador.Value

            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificador.Value).FirstOrDefault

            ' Define el label con el titulo MedioPago. Se hay solamente uno registro, se queda en singular, se hay más, se queda en plural
            lblMedioPago.Text = If(itemDivisa.MediosPago IsNot Nothing, If(itemDivisa.MediosPago.Count > 1, Traduzir("012_mediospago"), Traduzir("012_mediopago")), "")

            ' Lista del itemRepeater
            Dim ListaMediosPago As ObservableCollection(Of Clases.MedioPago) = e.Item.DataItem

            ' añadir valores en los campos
            lblFiltroTipoMedioPago.Text = ListaMediosPago(0).Tipo.ToString()
            txtCodTpMedioPago.Text = ListaMediosPago(0).Tipo.ToString()
            imgTpMedioPago.ToolTip = txtCodTpMedioPago.Text

            ' añadir el tipo del mediopago en el identificador para recuperar avante esto valor por tipo
            hdfIdentificadorTipo.Value = hdfIdentificador.Value & "_" & ListaMediosPago(0).Tipo.ToString

            ' insertar la imagen del tipo del mediopago
            Select Case ListaMediosPago(0).Tipo
                Case Enumeradores.TipoMedioPago.Cheque
                    imgTpMedioPago.ImageUrl = "../Imagenes/ICO_TIPO_MEDIO_PAGO_CHEQUE_32x32.png"
                Case Enumeradores.TipoMedioPago.OtroValor
                    imgTpMedioPago.ImageUrl = "../Imagenes/ICO_TIPO_MEDIO_PAGO_OTROVALOR_32x32.png"
                Case Enumeradores.TipoMedioPago.Tarjeta
                    imgTpMedioPago.ImageUrl = "../Imagenes/ICO_TIPO_MEDIO_PAGO_TARJETA_32x32.png"
                Case Enumeradores.TipoMedioPago.Ticket
                    imgTpMedioPago.ImageUrl = "../Imagenes/ICO_TIPO_MEDIO_PAGO_TICKET_32x32.png"
            End Select
            imgTpMedioPago.DataBind()

            ' servir como referencia no evento Changed
            txtImporteTpMedioPago.ID = "txtImporteTpMedioPago_" & itemDivisa.Identificador & "_" & ListaMediosPago(0).Identificador

            ' recuperar los valores de la clase ValorTipoMedioPago
            Dim valoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = itemDivisa.ValoresTotalesTipoMedioPago

            ' valor del tipomediopago
            Dim valorTipoMedioPago As Clases.ValorTipoMedioPago = Nothing
            If valoresTipoMedioPago IsNot Nothing AndAlso valoresTipoMedioPago.Count > 0 Then
                valorTipoMedioPago = valoresTipoMedioPago.Where(Function(f) f.TipoValor = TipoValor AndAlso f.TipoMedioPago = ListaMediosPago(0).Tipo).FirstOrDefault

            End If

            ' se hay valor, carga lo campo
            txtImporteTpMedioPago.Text = If(valorTipoMedioPago IsNot Nothing, String.Format("{0:N2}", valorTipoMedioPago.Importe), "")

            ' La condicion abajo fue insertada para el componente suportar exibir solamente el total del mediopago.
            ' Dantes ya tenia solamente esto línea '''GenerarDetallesMedioPago(pnlDetallesMedioPago, ListaMediosPago, itemDivisa, lblchkMostrarTerminos, chkMostrarTerminos)'''
            If Not String.IsNullOrEmpty(ListaMediosPago(0).Descripcion) Then
                ' generar los detalles y terminos del medio pago.

                'Dim Identificador As String = itemDivisa.Identificador & "_" & ListaMediosPago(0).Tipo.ToString
                'Dim ExibirTerminos As Boolean? = MostrarTerminos.Where(Function(f) f.Key = Identificador).FirstOrDefault.Value

                'If (ExibirTerminos Is Nothing OrElse ExibirTerminos) AndAlso ListaMediosPago.Exists(Function(f) f.Terminos IsNot Nothing) Then
                GenerarDetallesMedioPagoConTitulos(pnlDetallesMedioPago, ListaMediosPago, itemDivisa, lblchkMostrarTerminos, chkMostrarTerminos)
                'Else
                '   GenerarDetallesMedioPagoSinTitulos(pnlDetallesMedioPago, ListaMediosPago, itemDivisa, lblchkMostrarTerminos, chkMostrarTerminos)
                'End If

                fieldsetMedioPago.Visible = True

            Else
                fieldsetMedioPago.Visible = False

            End If

            ' actualiza el estado do campo. { enable o desable }
            ActualizarEstadoCampo(txtImporteTpMedioPago)

            RemoveHandler chkMostrarTerminos.CheckedChanged, AddressOf chkMostrarTerminos_CheckedChanged
            AddHandler chkMostrarTerminos.CheckedChanged, AddressOf chkMostrarTerminos_CheckedChanged

            ' añadir script de mascara
            Aplicacao.Util.Utilidad.CargarScripts(txtImporteTpMedioPago, MyBase._DecimalSeparador, MyBase._MilharSeparador, "I")

            ' validação para desabilitar Total quando tipovalor for Contado.
            If TipoValor = Enumeradores.TipoValor.Contado Then
                lblImporteTpMedioPago.Enabled = False
                txtImporteTpMedioPago.Enabled = False
            Else
                ' Desabilitar em modo consulta
                lblImporteTpMedioPago.Enabled = Not (Modo = Enumeradores.Modo.Consulta AndAlso (itemDivisa.ValoresTotalesTipoMedioPago Is Nothing OrElse itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing))
                txtImporteTpMedioPago.Enabled = Not (Modo = Enumeradores.Modo.Consulta AndAlso (itemDivisa.ValoresTotalesTipoMedioPago Is Nothing OrElse itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing))
            End If
        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Click do botao BorrarDivisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBorrarDivisa_Click(sender As Object, e As System.EventArgs)
        Try

            Dim btnBorrarDivisaMedioPago As Button = sender
            Dim repeaterItemDivisa As RepeaterItem = btnBorrarDivisaMedioPago.NamingContainer

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificador As HiddenField = repeaterItemDivisa.FindControl("hdfIdentificador")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificador.Value).First

            ActualizarControles(False)
            Me.RechazarDivisaModificada(itemDivisa)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Changed do campo txtImporteTpMedioPago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtImporteTpMedioPago_TextChanged(sender As Object, e As System.EventArgs)

        Dim txtImporteTpMedioPago As TextBox = sender

        ' lista que recebe la division del ID del campo TXT. Ejemplo: txtImporteTpMedioPago_09idk399fjfrnr84nf8n4f48hnnd82
        ' Identificadores(0) = txtCantidad
        ' Identificadores(1) = 09idk399fjfrnr84nf8n4f48hnnd82 = OID_MEDIO_PAGO
        Dim Identificadores() As String = {}
        If txtImporteTpMedioPago IsNot Nothing Then
            Identificadores = txtImporteTpMedioPago.ID.Split("_")

        End If

        Dim repeaterItemMedioPago As RepeaterItem = txtImporteTpMedioPago.NamingContainer

        ' campo HIDDEN para almacenar el Identificador de la divisa.
        Dim hdfIdentificadorTipo As HiddenField = repeaterItemMedioPago.FindControl("hdfIdentificadorTipo")

        ' busqueda de la divisa por el campo HIDDEN
        Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault
        Dim itemMedioPago As Clases.MedioPago = itemDivisa.MediosPago.Where(Function(f) f.Identificador = Identificadores(2)).FirstOrDefault

        ActualizarValoresTipoMedioPago(itemDivisa, txtImporteTpMedioPago, itemMedioPago.Tipo)

        ActualizarDivisaModificada(itemDivisa)

    End Sub

    ''' <summary>
    ''' Evento Changed dos campos de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCantidad_TextChanged(sender As Object, e As System.EventArgs)
        Try
            ' campo cambiado
            Dim txtCantidad As TextBox = sender

            ' lista que recebe la division del ID del campo TXT. Ejemplo: txtCantidad_09idk399fjfrnr84nf8n4f48hnnd82
            ' Identificadores(0) = txtCantidad
            ' Identificadores(1) = 09idk399fjfrnr84nf8n4f48hnnd82 = OID_MEDIO_PAGO
            Dim Identificadores() As String = {}
            If txtCantidad IsNot Nothing Then
                Identificadores = txtCantidad.ID.Split("_")

            End If

            Dim repeaterItemMedioPago As RepeaterItem = txtCantidad.NamingContainer
            Dim txtImporte As TextBox = repeaterItemMedioPago.FindControl("txtImporte_" & Identificadores(1))
            Dim txtImporteTpMedioPago As TextBox = repeaterItemMedioPago.FindControl("txtImporteTpMedioPago")

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemMedioPago.FindControl("hdfIdentificadorTipo")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault

            Dim ItemMedioPago As Clases.MedioPago = itemDivisa.MediosPago.Where(Function(f) f.Identificador = Identificadores(1)).FirstOrDefault

            ActualizarValoresMedioPago(itemDivisa, txtCantidad, txtImporte, txtImporteTpMedioPago, repeaterItemMedioPago, ItemMedioPago)

            ActualizarDivisaModificada(itemDivisa)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Changed dos campos de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtImporte_TextChanged(sender As Object, e As System.EventArgs)
        Try
            ' campo cambiado
            Dim txtImporte As TextBox = sender

            ' lista que recebe la division del ID del campo TXT. Ejemplo: txtCantidad_09idk399fjfrnr84nf8n4f48hnnd82
            ' Identificadores(0) = txtCantidad
            ' Identificadores(1) = 09idk399fjfrnr84nf8n4f48hnnd82 = OID_MEDIO_PAGO
            Dim Identificadores() As String = {}
            If txtImporte IsNot Nothing Then
                Identificadores = txtImporte.ID.Split("_")

            End If

            Dim repeaterItemMedioPago As RepeaterItem = txtImporte.NamingContainer
            Dim txtCantidad As TextBox = repeaterItemMedioPago.FindControl("txtCantidad_" & Identificadores(1))
            Dim txtImporteTpMedioPago As TextBox = repeaterItemMedioPago.FindControl("txtImporteTpMedioPago")

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemMedioPago.FindControl("hdfIdentificadorTipo")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisa As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault

            Dim ItemMedioPago As Clases.MedioPago = itemDivisa.MediosPago.Where(Function(f) f.Identificador = Identificadores(1)).FirstOrDefault

            ActualizarValoresMedioPago(itemDivisa, txtImporte, txtCantidad, txtImporteTpMedioPago, repeaterItemMedioPago, ItemMedioPago)

            ActualizarDivisaModificada(itemDivisa)

        Catch ex As Exception
            MyBase.NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento CheckedChanged del CheckBox de informar detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkMostrarTerminos_CheckedChanged(sender As Object, e As System.EventArgs)
        Try
            Dim chkMostrarTerminos As CheckBox = sender

            Dim Identificadores() As String = chkMostrarTerminos.ID.Split("_")
            Dim repeaterItemTipoMedioPago As RepeaterItem = chkMostrarTerminos.NamingContainer

            Dim txtCodTpMedioPago As TextBox = repeaterItemTipoMedioPago.FindControl("txtCodTpMedioPago")

            Dim TipoMedioPago As Comon.Enumeradores.TipoMedioPago

            ' recupera el enum del tipo de mediopago
            Select Case txtCodTpMedioPago.Text

                Case "Ticket"
                    TipoMedioPago = Enumeradores.TipoMedioPago.Ticket
                Case "Cheque"
                    TipoMedioPago = Enumeradores.TipoMedioPago.Cheque
                Case "OtroValor"
                    TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor
                Case "Tarjeta"
                    TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta

            End Select

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemTipoMedioPago.FindControl("hdfIdentificadorTipo")
            Dim IdentificadorTipo() As String = hdfIdentificadorTipo.Value.Split("_")

            ' busqueda de la divisa por el campo HIDDEN
            Dim itemDivisaModificada As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = IdentificadorTipo(0)).FirstOrDefault
            Dim itemDivisaIAC As Clases.Divisa = Nothing
            If DivisasIAC IsNot Nothing AndAlso DivisasIAC.Count > 0 Then
                itemDivisaIAC = DivisasIAC.Where(Function(f) f.Identificador = IdentificadorTipo(0)).FirstOrDefault
            End If
            If itemDivisaIAC Is Nothing Then
                ' caso hay divisas agregadas
                itemDivisaIAC = DivisasActualizadas.Where(Function(f) f.Identificador = IdentificadorTipo(0)).FirstOrDefault
            End If

            ' recupera os OIDs de determinado tipo de medio pagos que possuem terminos.
            Dim ListaIdentificadoresMedioPago = (From _item In itemDivisaModificada.MediosPago
                                                Where _item.Terminos IsNot Nothing AndAlso
                                                      _item.Tipo = TipoMedioPago
                                               Select _item.Identificador).ToList

            If chkMostrarTerminos.Checked Then

                ' recupera os controles de mediospago de determinado tipo de medio pago
                For Each _IdentMedioPago In ListaIdentificadoresMedioPago
                    Dim _IdentMedioPagoLocal = _IdentMedioPago
                    Dim txtCantidad As TextBox = repeaterItemTipoMedioPago.FindControl("txtCantidad_" & _IdentMedioPago)
                    Dim txtImporte As TextBox = repeaterItemTipoMedioPago.FindControl("txtImporte_" & _IdentMedioPago)
                    Dim itemMedioPagoIAC = itemDivisaIAC.MediosPago.Where(Function(f) f.Identificador = _IdentMedioPagoLocal).FirstOrDefault
                    Dim itemValorMedioPagoIAC = If(itemMedioPagoIAC.Valores IsNot Nothing, itemMedioPagoIAC.Valores.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault, Nothing)

                    txtCantidad.Text = If(itemValorMedioPagoIAC IsNot Nothing, itemValorMedioPagoIAC.Cantidad, Nothing)
                    txtImporte.Text = If(itemValorMedioPagoIAC IsNot Nothing, String.Format("{0:N2}", itemValorMedioPagoIAC.Importe), Nothing)

                    CargarValoresIniciaisMedioPago(itemMedioPagoIAC, Nothing, txtImporte, Nothing, "I")
                    CargarValoresIniciaisMedioPago(itemMedioPagoIAC, Nothing, txtCantidad, Nothing, "C")

                    Dim Terminos = itemDivisaModificada.MediosPago.Where(Function(m) m.Identificador = _IdentMedioPagoLocal).FirstOrDefault.Terminos
                    Dim bolValorCantidad As Boolean = True
                    For Each _termino In Terminos
                        Dim ControlLabelTermino As Control = repeaterItemTipoMedioPago.FindControl("lbltermino_" & _IdentMedioPago & "_" & _termino.Identificador)
                        Dim ControlTermino As Control = repeaterItemTipoMedioPago.FindControl("ftermino_" & _IdentMedioPago & "_" & _termino.Identificador)
                        If ControlLabelTermino IsNot Nothing Then ControlLabelTermino.Visible = True
                        If ControlTermino IsNot Nothing Then ControlTermino.Visible = True

                        If bolValorCantidad Then
                            txtCantidad.Text = "1"
                            txtCantidad.Enabled = False
                            bolValorCantidad = False

                        End If

                        CargarValoresIniciaisMedioPago(Nothing, _termino, txtCantidad, ControlTermino, "C")

                    Next _termino
                Next _IdentMedioPago

            Else ' borrar valores

                ' recupera os controles de mediospago de determinado tipo de medio pago
                For Each _IdentMedioPago In ListaIdentificadoresMedioPago
                    Dim _IdentMedioPagoLocal = _IdentMedioPago
                    ' recuperar los controles correspondientes a esto mediopago.
                    Dim txtCantidad As TextBox = repeaterItemTipoMedioPago.FindControl("txtCantidad_" & _IdentMedioPago)
                    Dim txtImporte As TextBox = repeaterItemTipoMedioPago.FindControl("txtImporte_" & _IdentMedioPago)

                    txtCantidad.Text = String.Empty
                    txtImporte.Text = String.Empty
                    txtCantidad.Enabled = True
                    ' recupera los datos originais.
                    Dim itemMedioPagoPagina = itemDivisaIAC.MediosPago.Where(Function(f) f.Identificador = _IdentMedioPagoLocal).FirstOrDefault
                    ' recuperar los datos actualizados en memoria.
                    Dim itemMedioPagoModificado = itemDivisaModificada.MediosPago.Where(Function(f) f.Identificador = _IdentMedioPagoLocal).FirstOrDefault
                    itemMedioPagoModificado = itemMedioPagoPagina

                    Dim itemTermino As List(Of Clases.Termino) = Nothing

                    ' terminos
                    Dim Terminos = itemMedioPagoModificado.Terminos.Where(Function(t) t IsNot Nothing).ToList
                    For Each _termino In Terminos
                        Dim ControlLabel As Control = repeaterItemTipoMedioPago.FindControl("lbltermino_" & _IdentMedioPago & "_" & _termino.Identificador)
                        Dim ControlCampo As Control = repeaterItemTipoMedioPago.FindControl("ftermino_" & _IdentMedioPago & "_" & _termino.Identificador)

                        LimpiarTermino(ControlLabel, ControlCampo)

                    Next _termino

                Next _IdentMedioPago

            End If

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Changed del DropDownList de terminos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub fterminoD_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try

            Dim ddlDropDown As DropDownList = sender
            ' Identificadores del termino devuelve 3 itens:
            ' (0) Nombre del campo, más
            ' (1) Identificador del medio pago, más
            ' (2) Identificador del termino.
            Dim Identificadores() As String = ddlDropDown.ID.Split("_")
            Dim repeaterItemTipoMedioPago As RepeaterItem = ddlDropDown.NamingContainer

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemTipoMedioPago.FindControl("hdfIdentificadorTipo")
            Dim txtImporte As TextBox = repeaterItemTipoMedioPago.FindControl("txtImporte_" & Identificadores(1))

            ' busqueda de la divisa por el campo HIDDEN.
            Dim itemDivisaModificada As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault
            ' recuperar el mediopago desto termino.
            Dim itemMedioPagoModificado = itemDivisaModificada.MediosPago.Where(Function(f) f.Identificador = Identificadores(1)).FirstOrDefault
            'recuperar el termino.
            Dim itemTerminoModificado = itemMedioPagoModificado.Terminos.Where(Function(f) f.Identificador = Identificadores(2)).FirstOrDefault

            ' rellenar la propriedad con el valor cambiado.
            itemTerminoModificado.Valor = ddlDropDown.SelectedItem.Text
            If String.IsNullOrEmpty(txtImporte.Text) OrElse CDbl(txtImporte.Text) = 0 Then
                txtImporte.Text = 1
            End If

            ' Actualiza en el item del mediopago los cambios hechos en un determinado termino.
            ActualizarItemTermino(itemMedioPagoModificado, itemTerminoModificado)
            ' Actualiza en la divisa los cambios hechos en un determinado mediopago.
            ActualizarItemMedioPago(itemDivisaModificada, itemMedioPagoModificado)
            ' Actualiza el objeto completo de divisa.
            ActualizarDivisaModificada(itemDivisaModificada)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Changed del TextBox de terminos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub fterminot_TextChanged(sender As Object, e As System.EventArgs)
        Try

            Dim textbox As TextBox = sender

            ' Identificadores del termino devuelve 3 itens:
            ' (0) Nombre del campo, más
            ' (1) Identificador del medio pago, más
            ' (2) Identificador del termino.
            Dim Identificadores() As String = textbox.ID.Split("_")
            ' Identificador del termino
            Dim reg As String = Identificadores(2)

            If RegExpr IsNot Nothing AndAlso RegExpr.Count > 0 Then
                ' recupera a expressão regular
                Dim _regExpr = RegExpr.Where(Function(f) f.Key = reg).FirstOrDefault.Value

                If Not String.IsNullOrEmpty(_regExpr) Then

                    If Not Regex.IsMatch(textbox.Text, _regExpr) Then
                        Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("012_msg_mascara"), textbox.Text))

                    End If

                End If

            End If

            Dim repeaterItemTipoMedioPago As RepeaterItem = textbox.NamingContainer

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemTipoMedioPago.FindControl("hdfIdentificadorTipo")
            Dim txtImporte As TextBox = repeaterItemTipoMedioPago.FindControl("txtImporte_" & Identificadores(1))

            ' busqueda de la divisa por el campo HIDDEN.
            Dim itemDivisaModificada As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault
            ' recuperar el mediopago desto termino.
            Dim itemMedioPagoModificado = itemDivisaModificada.MediosPago.Where(Function(f) f.Identificador = Identificadores(1)).FirstOrDefault
            'recuperar el termino.
            Dim itemTerminoModificado = itemMedioPagoModificado.Terminos.Where(Function(f) f.Identificador = Identificadores(2)).FirstOrDefault

            ' rellenar la propriedad con el valor cambiado.
            itemTerminoModificado.Valor = textbox.Text
            If String.IsNullOrEmpty(txtImporte.Text) OrElse CDbl(txtImporte.Text) = 0 Then
                txtImporte.Text = 1

            End If

            ' Actualiza en el item del mediopago los cambios hechos en un determinado termino.
            ActualizarItemTermino(itemMedioPagoModificado, itemTerminoModificado)
            ' Actualiza en la divisa los cambios hechos en un determinado mediopago.
            ActualizarItemMedioPago(itemDivisaModificada, itemMedioPagoModificado)
            ' Actualiza el objeto completo de divisa.
            ActualizarDivisaModificada(itemDivisaModificada)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Evento Changed del CheckBox de terminos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub fterminoC_CheckedChanged(sender As Object, e As System.EventArgs)
        Try

            Dim checkbox As CheckBox = sender
            checkbox.AutoPostBack = True
            ' Identificadores del termino devuelve 3 itens:
            ' (0) Nombre del campo, más
            ' (1) Identificador del medio pago, más
            ' (2) Identificador del termino.
            Dim Identificadores() As String = checkbox.ID.Split("_")
            Dim repeaterItemTipoMedioPago As RepeaterItem = checkbox.NamingContainer

            ' campo HIDDEN para almacenar el Identificador de la divisa.
            Dim hdfIdentificadorTipo As HiddenField = repeaterItemTipoMedioPago.FindControl("hdfIdentificadorTipo")
            Dim txtImporte As TextBox = repeaterItemTipoMedioPago.FindControl("txtImporte_" & Identificadores(1))

            ' busqueda de la divisa por el campo HIDDEN.
            Dim itemDivisaModificada As Clases.Divisa = DivisasActualizadas.Where(Function(f) f.Identificador = hdfIdentificadorTipo.Value).FirstOrDefault
            ' recuperar el mediopago desto termino.
            Dim itemMedioPagoModificado = itemDivisaModificada.MediosPago.Where(Function(f) f.Identificador = Identificadores(1)).FirstOrDefault
            'recuperar el termino.
            Dim itemTerminoModificado = itemMedioPagoModificado.Terminos.Where(Function(f) f.Identificador = Identificadores(2)).FirstOrDefault

            ' rellenar la propriedad con el valor cambiado.
            itemTerminoModificado.Valor = checkbox.Checked
            If String.IsNullOrEmpty(txtImporte.Text) OrElse CDbl(txtImporte.Text) = 0 Then
                txtImporte.Text = 1
            End If

            ' Actualiza en el item del mediopago los cambios hechos en un determinado termino.
            ActualizarItemTermino(itemMedioPagoModificado, itemTerminoModificado)
            ' Actualiza en la divisa los cambios hechos en un determinado mediopago.
            ActualizarItemMedioPago(itemDivisaModificada, itemMedioPagoModificado)
            ' Actualiza el objeto completo de divisa.
            ActualizarDivisaModificada(itemDivisaModificada)

        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try

    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Actualizar el objeto de divisas original con los cambios ejecutados.
    ''' </summary>
    ''' <param name="EsValidacion">Define se el metodo ActualizarControels irá llamar la validación de los datos rellenados</param>
    ''' <remarks></remarks>
    Public Sub GuardarDatos(EsValidacion As Boolean)
        Try
            ActualizarControles(EsValidacion)
            If EsValidacion Then
                DivisasIAC = DivisasActualizadas
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para actualizar los valores rellenados en la pantalla para el objeto
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ActualizarControles(EsValidacion As Boolean)
        Try
            ValidacionControles(EsValidacion)

            Dim itemDivisaAux As Clases.Divisa = Nothing

            Dim ListaDivisas As New ObservableCollection(Of Clases.Divisa)
            Dim Mesajes As New StringBuilder

            ' loop en los itens del repeater de divisas, o sea, cada divisa rellenada en el controle
            For Each _itemBound As RepeaterItem In rptDivisa.Items
                ' recupera la divisa currente
                itemDivisaAux = DivisasActualizadas(_itemBound.ItemIndex)
                Dim repeaterItem As Repeater = _itemBound.FindControl("rptDetalle")

                Dim ListaAgrupada As New ObservableCollection(Of List(Of Clases.MedioPago))
                Dim _ListaMediosPago As New ObservableCollection(Of Clases.MedioPago)
                _ListaMediosPago.AddRange(itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion))
                _ListaMediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList())))

                Dim ListaMediosPago As New ObservableCollection(Of Clases.MedioPago)
                ' loop en los itens de la divisa. {Tipo de MediosPago} 
                For Each _itemBoundMedioPago As RepeaterItem In repeaterItem.Items

                    If _itemBoundMedioPago.ItemType = ListItemType.Item OrElse _itemBoundMedioPago.ItemType = ListItemType.AlternatingItem Then

                        Dim hdfIdentificadorTipo As HiddenField = _itemBoundMedioPago.FindControl("hdfIdentificadorTipo")
                        Dim Tipo() As String = hdfIdentificadorTipo.Value.Split("_")

                        Dim ListaPorTipo As New List(Of Clases.MedioPago)
                        For Each itens In ListaAgrupada
                            For Each item In itens
                                If item.Tipo.ToString = Tipo(1) Then
                                    ListaPorTipo.Add(item)
                                End If
                            Next
                        Next

                        Dim chkMostrarTerminos As CheckBox = Nothing
                        Dim Flag As Boolean = False 'Flag para permitir busca do checkboxMostrarTerminos uma unica vez

                        ' loop en los mediospago de la divisa currente
                        For Each _itemMedioPago In ListaPorTipo.OrderBy(Function(f) f.Descripcion) 'itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion)

                            ' copia del itemMedioPago
                            Dim itemMedioPagoAux As Clases.MedioPago = _itemMedioPago
                            If Not Flag Then
                                chkMostrarTerminos = _itemBoundMedioPago.FindControl("chkMostrarTerminos_" & itemMedioPagoAux.Identificador)
                                Flag = True
                            End If
                            ' recupera los controles de repeateritem
                            Dim txtImporteTpMedioPago As TextBox = _itemBoundMedioPago.FindControl("txtImporteTpMedioPago_" & itemDivisaAux.Identificador & "_" & itemMedioPagoAux.Identificador)
                            Dim txtImporte As TextBox = _itemBoundMedioPago.FindControl("txtImporte_" & itemMedioPagoAux.Identificador)
                            Dim txtCantidad As TextBox = _itemBoundMedioPago.FindControl("txtCantidad_" & itemMedioPagoAux.Identificador)

                            Dim ListaTerminos As ObservableCollection(Of Clases.Termino) = Nothing

                            ' solamente valida los terminos si el checkbox 'MostraTerminos' estuver marcado
                            If chkMostrarTerminos IsNot Nothing AndAlso chkMostrarTerminos.Checked Then

                                If itemMedioPagoAux.Terminos IsNot Nothing AndAlso itemMedioPagoAux.Terminos.Count > 0 Then

                                    ListaTerminos = New ObservableCollection(Of Clases.Termino)

                                    ' loop en los terminos del medio pago currente
                                    For Each _itemTtermino In _itemMedioPago.Terminos

                                        Dim itemTerminoAux As Clases.Termino = _itemTtermino

                                        Dim valor As String = String.Empty
                                        ' recupera el controle correspondiente al termino
                                        Dim txtControl As Control = _itemBoundMedioPago.FindControl("fTermino_" & itemMedioPagoAux.Identificador & "_" & itemTerminoAux.Identificador)

                                        If TypeOf txtControl Is TextBox Then
                                            itemTerminoAux.Valor = DirectCast(txtControl, TextBox).Text

                                        ElseIf TypeOf txtControl Is CheckBox Then
                                            itemTerminoAux.Valor = If(Not String.IsNullOrEmpty(txtImporte.Text), DirectCast(txtControl, CheckBox).Checked, String.Empty)

                                        ElseIf TypeOf txtControl Is DropDownList Then
                                            itemTerminoAux.Valor = DirectCast(txtControl, DropDownList).SelectedItem.Text
                                        End If

                                        ListaTerminos.Add(itemTerminoAux)

                                    Next _itemTtermino

                                End If

                            Else ' si el checkbox de terminos se queda desmarcado, entonces no hay valores para terminos
                                ListaTerminos = _itemMedioPago.Terminos

                            End If

                            ' ejecuta los cambios de valores
                            'itemMedioPagoAux.Terminos = ListaTerminos
                            CambiarValoresTipoMedioPago(itemDivisaAux, txtImporteTpMedioPago, itemMedioPagoAux.Tipo)
                            CambiarValoresMedioPago(itemMedioPagoAux, txtImporte, txtCantidad, ListaTerminos)

                            ListaMediosPago.Add(itemMedioPagoAux)
                            itemDivisaAux.MediosPago = ListaMediosPago

                        Next _itemMedioPago
                    End If
                Next _itemBoundMedioPago
                ListaDivisas.Add(itemDivisaAux)

            Next _itemBound

            ' actualizar objeto
            DivisasActualizadas.Clear()
            DivisasActualizadas.AddRange(ListaDivisas)

        Catch ex As Exception
            Throw

        End Try

    End Sub

    ''' <summary>
    ''' Ejecuta validacion de los controles
    ''' </summary>
    ''' <param name="EsValidacion">Bool para verificar si es para ejecutar la validacion</param>
    ''' <remarks></remarks>
    Public Sub ValidacionControles(EsValidacion As Boolean)
        If EsValidacion Then
            Dim itemDivisaAux As Clases.Divisa = Nothing

            Dim Mesajes As New StringBuilder

            ' loop en los itens del repeater de divisas, o sea, cada divisa rellenada en el controle
            For Each _itemBound As RepeaterItem In rptDivisa.Items
                ' recupera la divisa currente
                itemDivisaAux = DivisasActualizadas(_itemBound.ItemIndex)
                Dim repeaterItem As Repeater = _itemBound.FindControl("rptDetalle")

                Dim ListaAgrupada As New List(Of List(Of Clases.MedioPago))
                Dim _ListaMediosPago As New List(Of Clases.MedioPago)
                _ListaMediosPago.AddRange(itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion))
                _ListaMediosPago.GroupBy(Function(f) f.Tipo).ToList.ForEach(Sub(s) ListaAgrupada.Add(New List(Of Clases.MedioPago)(s.ToList)))

                ' loop en los itens de la divisa. {Tipo de MediosPago} 
                For Each _itemBoundMedioPago As RepeaterItem In repeaterItem.Items

                    If _itemBoundMedioPago.ItemType = ListItemType.Item OrElse _itemBoundMedioPago.ItemType = ListItemType.AlternatingItem Then

                        Dim hdfIdentificadorTipo As HiddenField = _itemBoundMedioPago.FindControl("hdfIdentificadorTipo")
                        Dim Tipo() As String = hdfIdentificadorTipo.Value.Split("_")

                        Dim ListaPorTipo As New List(Of Clases.MedioPago)
                        For Each itens In ListaAgrupada
                            For Each item In itens
                                If item.Tipo.ToString = Tipo(1) Then
                                    ListaPorTipo.Add(item)
                                End If
                            Next
                        Next

                        Dim chkMostrarTerminos As CheckBox = Nothing
                        Dim Flag As Boolean = False 'Flag para permitir busca do checkboxMostrarTerminos uma unica vez

                        ' loop en los mediospago de la divisa currente
                        For Each _itemMedioPago In ListaPorTipo.OrderBy(Function(f) f.Descripcion) 'itemDivisaAux.MediosPago.OrderBy(Function(f) f.Descripcion)

                            ' copia del itemMedioPago
                            Dim itemMedioPagoAux As Clases.MedioPago = _itemMedioPago
                            If Not Flag Then
                                chkMostrarTerminos = _itemBoundMedioPago.FindControl("chkMostrarTerminos_" & itemMedioPagoAux.Identificador)
                                Flag = True
                            End If
                            ' recupera los controles de repeateritem
                            Dim txtImporteTpMedioPago As TextBox = _itemBoundMedioPago.FindControl("txtImporteTpMedioPago_" & itemDivisaAux.Identificador & "_" & itemMedioPagoAux.Identificador)
                            Dim txtImporte As TextBox = _itemBoundMedioPago.FindControl("txtImporte_" & itemMedioPagoAux.Identificador)
                            Dim txtCantidad As TextBox = _itemBoundMedioPago.FindControl("txtCantidad_" & itemMedioPagoAux.Identificador)

                            ' solamente valida los terminos si el checkbox 'MostraTerminos' estuver marcado
                            'If chkMostrarTerminos IsNot Nothing AndAlso chkMostrarTerminos.Checked Then

                            '    If itemMedioPagoAux.Terminos IsNot Nothing AndAlso itemMedioPagoAux.Terminos.Count > 0 Then

                            '        ' loop en los terminos del medio pago currente
                            '        For Each _itemTtermino In _itemMedioPago.Terminos

                            '            Dim itemTerminoAux As Clases.Termino = _itemTtermino

                            '            Dim valor As String = String.Empty
                            '            ' recupera el controle correspondiente al termino
                            '            Dim txtControl As Control = _itemBoundMedioPago.FindControl("fTermino_" & itemMedioPagoAux.Identificador & "_" & itemTerminoAux.Identificador)

                            '            If TypeOf txtControl Is TextBox Then
                            '                If ((Not String.IsNullOrEmpty(DirectCast(txtControl, TextBox).Text)) AndAlso (String.IsNullOrEmpty(txtImporte.Text)) OrElse
                            '                    (Not String.IsNullOrEmpty(DirectCast(txtControl, TextBox).Text)) AndAlso (Not String.IsNullOrEmpty(txtImporte.Text)) AndAlso CDbl(txtImporte.Text) = 0.0) Then
                            '                    Mesajes.Append(String.Format(Traduzir("012_validacionTermino"), itemTerminoAux.Descripcion, itemMedioPagoAux.Descripcion) & vbCrLf & vbCrLf)
                            '                End If

                            '            ElseIf TypeOf txtControl Is DropDownList Then

                            '                If ((Not String.IsNullOrEmpty(DirectCast(txtControl, DropDownList).SelectedItem.Text)) AndAlso (String.IsNullOrEmpty(txtImporte.Text)) OrElse
                            '                    (Not String.IsNullOrEmpty(DirectCast(txtControl, DropDownList).SelectedItem.Text)) AndAlso (Not String.IsNullOrEmpty(txtImporte.Text)) AndAlso CDbl(txtImporte.Text) = 0.0) Then
                            '                    Mesajes.Append(String.Format(Traduzir("012_validacionTermino"), itemTerminoAux.Descripcion, itemMedioPagoAux.Descripcion) & vbCrLf & vbCrLf)
                            '                End If

                            '            End If

                            '        Next _itemTtermino

                            '    End If

                            'End If

                            ' validacion del preenchimento
                            Validacion(itemDivisaAux, itemMedioPagoAux, txtImporte, txtCantidad, Mesajes)

                        Next _itemMedioPago
                    End If
                Next _itemBoundMedioPago

            Next _itemBound
            ' si hay errores, es generada una excepción con todos.
            If Mesajes.Length > 0 Then
                Throw New Excepcion.NegocioExcepcion(Mesajes.ToString)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Validación de la obligatoriedad de los campos cantidad y valor
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="itemMedioPago">Clase MedioPago</param>
    ''' <param name="txtImporte">Controle Importe</param>
    ''' <param name="txtCantidad">Controle Cantidad</param>
    ''' <remarks></remarks>
    Private Function Validacion(itemDivisa As Clases.Divisa, _
                                itemMedioPago As Clases.MedioPago, _
                                txtImporte As TextBox, _
                                txtCantidad As TextBox, _
                                ByRef Mensajes As StringBuilder) As StringBuilder

        If txtCantidad IsNot Nothing AndAlso txtImporte IsNot Nothing Then

            Dim Cantidad As Integer = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, 0)
            Dim Importe As Decimal = If(Not String.IsNullOrEmpty(txtImporte.Text) AndAlso Not CDbl(txtImporte.Text) = 0.0, txtImporte.Text, 0)

            ' si cantidad rellenado y importe nulo
            ' OBS: Proxima sprint permitir gravar 0 para mediospago.
            If (((Not Cantidad = 0 AndAlso Not Cantidad = 1 AndAlso Importe = 0)) OrElse
                ((Cantidad = 0 AndAlso Not Importe = 0))) Then

                Return Mensajes.Append(String.Format(Traduzir("012_validacion"), itemMedioPago.Descripcion, itemDivisa.Descripcion) & vbCrLf & vbCrLf)

            End If

        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Actualización de los valores de mediopago
    ''' </summary>
    ''' <param name="itemMedioPago">Clase mediopago</param>
    ''' <param name="txtImporte">Controle Importe</param>
    ''' <param name="txtCantidad">Controle Cantidad</param>
    ''' <remarks></remarks>
    Private Sub CambiarValoresMedioPago(ByRef itemMedioPago As Clases.MedioPago, _
                                        txtImporte As TextBox, _
                                        txtCantidad As TextBox, _
                                        Terminos As ObservableCollection(Of Clases.Termino))

        If txtImporte IsNot Nothing AndAlso txtCantidad IsNot Nothing Then

            Dim ValoresMedioPago As ObservableCollection(Of Clases.ValorMedioPago) = itemMedioPago.Valores
            Dim objValorMedioPago_DeclaradoOContado As Clases.ValorMedioPago = Nothing

            ' si Cantidad <> Nulo E Cantidad <> 0 E Importe <> Nulo E Importe <> 0
            If (Not String.IsNullOrEmpty(txtCantidad.Text) AndAlso Not CInt(txtCantidad.Text) = 0) AndAlso _
               (Not String.IsNullOrEmpty(txtImporte.Text) AndAlso Not CDbl(txtImporte.Text) = 0.0) Then

                Dim objValorMedioPago As New Clases.ValorMedioPago
                ' anadir valores en el objeto
                objValorMedioPago.Cantidad = txtCantidad.Text
                objValorMedioPago.Importe = txtImporte.Text
                objValorMedioPago.Terminos = New ObservableCollection(Of Clases.Termino)
                If Terminos IsNot Nothing AndAlso Terminos.Count > 0 Then
                    For Each termino In Terminos
                        If Not String.IsNullOrEmpty(termino.Valor) Then
                            objValorMedioPago.Terminos.Add(termino)
                        End If
                    Next termino
                End If

                If ValoresMedioPago IsNot Nothing AndAlso ValoresMedioPago.Count > 0 Then
                    ' recuperar el valor mediopago. {Declarado o Contado}
                    Dim objValorMedioPagoDC As Clases.ValorMedioPago = ValoresMedioPago.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                    If objValorMedioPagoDC IsNot Nothing Then
                        ValoresMedioPago.Remove(objValorMedioPagoDC)
                        objValorMedioPago.TipoValor = objValorMedioPagoDC.TipoValor
                        objValorMedioPago.Terminos = objValorMedioPagoDC.Terminos
                        objValorMedioPagoDC = objValorMedioPago
                        ValoresMedioPago.Add(objValorMedioPagoDC)

                    Else
                        objValorMedioPago.TipoValor = TipoValor
                        ValoresMedioPago.Add(objValorMedioPago)

                    End If

                Else
                    objValorMedioPago.TipoValor = TipoValor
                    ValoresMedioPago = New ObservableCollection(Of Clases.ValorMedioPago)
                    ValoresMedioPago.Add(objValorMedioPago)

                End If

            Else
                If itemMedioPago.Valores IsNot Nothing Then
                    For index = 0 To itemMedioPago.Valores.Count - 1

                        If itemMedioPago.Valores(index).TipoValor = TipoValor Then

                            itemMedioPago.Valores.RemoveAt(index)
                            Exit For

                        End If

                    Next index

                    ValoresMedioPago = itemMedioPago.Valores

                End If

            End If

            If ValoresMedioPago IsNot Nothing AndAlso ValoresMedioPago.Count = 0 Then
                ValoresMedioPago = Nothing

            End If
            itemMedioPago.Valores = ValoresMedioPago

        End If

    End Sub

    ''' <summary>
    ''' Actualización de los valores del tipo de mediopago
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="txtImporteTpMedioPago">Controle ImporteTotalPorTipoMedioPago</param>
    ''' <param name="EnumTipoMedioPago">Enumerador del TipoMedioPago</param>
    ''' <remarks></remarks>
    Private Sub CambiarValoresTipoMedioPago(ByRef itemDivisa As Clases.Divisa, _
                                            txtImporteTpMedioPago As TextBox, _
                                            EnumTipoMedioPago As Enumeradores.TipoMedioPago)

        If txtImporteTpMedioPago IsNot Nothing Then

            ' Lista de valores tipo medio pago. { Ticket, Cheque, Tarjeta, OtroValor }
            Dim ListaValoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = itemDivisa.ValoresTotalesTipoMedioPago
            Dim objValorTipoMedioPago_Tipo As Clases.ValorTipoMedioPago = Nothing

            If Not String.IsNullOrEmpty(txtImporteTpMedioPago.Text) AndAlso Not CDbl(txtImporteTpMedioPago.Text) = 0 Then
                Dim objValorTipoMedioPago As New Clases.ValorTipoMedioPago
                objValorTipoMedioPago.Importe = txtImporteTpMedioPago.Text
                objValorTipoMedioPago.TipoMedioPago = EnumTipoMedioPago
                objValorTipoMedioPago.TipoValor = TipoValor

                If ListaValoresTipoMedioPago IsNot Nothing AndAlso ListaValoresTipoMedioPago.Count > 0 Then
                    objValorTipoMedioPago_Tipo = ListaValoresTipoMedioPago.Where(Function(f) f.TipoValor = TipoValor AndAlso f.TipoMedioPago = EnumTipoMedioPago).FirstOrDefault

                    If objValorTipoMedioPago_Tipo IsNot Nothing Then
                        ListaValoresTipoMedioPago.Remove(objValorTipoMedioPago_Tipo)
                        objValorTipoMedioPago.TipoValor = objValorTipoMedioPago_Tipo.TipoValor
                        objValorTipoMedioPago.TipoMedioPago = objValorTipoMedioPago_Tipo.TipoMedioPago
                        objValorTipoMedioPago_Tipo = objValorTipoMedioPago
                        ListaValoresTipoMedioPago.Add(objValorTipoMedioPago_Tipo)

                    Else
                        ListaValoresTipoMedioPago.Add(objValorTipoMedioPago)

                    End If

                Else
                    ListaValoresTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                    ListaValoresTipoMedioPago.Add(objValorTipoMedioPago)

                End If

            Else
                If itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                    For index = 0 To itemDivisa.ValoresTotalesTipoMedioPago.Count - 1

                        If itemDivisa.ValoresTotalesTipoMedioPago(index).TipoValor = TipoValor AndAlso itemDivisa.ValoresTotalesTipoMedioPago(index).TipoMedioPago = EnumTipoMedioPago Then
                            itemDivisa.ValoresTotalesTipoMedioPago.RemoveAt(index)
                            Exit For

                        End If

                    Next index

                    ListaValoresTipoMedioPago = itemDivisa.ValoresTotalesTipoMedioPago

                End If

            End If

            If ListaValoresTipoMedioPago IsNot Nothing AndAlso ListaValoresTipoMedioPago.Count = 0 Then
                ListaValoresTipoMedioPago = Nothing
            End If

            itemDivisa.ValoresTotalesTipoMedioPago = ListaValoresTipoMedioPago

        End If

    End Sub

    ''' <summary>
    ''' Actualizar los valores en la coleccion del medio pago.
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="txtImporte">Controle importe</param>
    ''' <param name="txtCantidad">Controle cantidad</param>
    ''' <param name="repeaterItemMedioPago">RepeaterItem MedioPago</param>
    ''' <param name="itemMedioPago">Clase MedioPago</param>
    ''' <remarks></remarks>
    Private Sub ActualizarValoresMedioPago(ByRef itemDivisa As Clases.Divisa, _
                                           ByRef txtImporte As TextBox, _
                                           ByRef txtCantidad As TextBox, _
                                           ByRef txtImporteTpMedioPago As TextBox, _
                                           repeaterItemMedioPago As RepeaterItem, _
                                           ByRef itemMedioPago As Clases.MedioPago)

        Dim IListValorMedioPago As ObservableCollection(Of Clases.ValorMedioPago) = itemMedioPago.Valores


        Dim valorMedioPago As Clases.ValorMedioPago = Nothing
        If IListValorMedioPago IsNot Nothing AndAlso IListValorMedioPago.Count > 0 Then
            valorMedioPago = IListValorMedioPago.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

        End If

        Dim ValoresMedioPago As ObservableCollection(Of Clases.ValorMedioPago) = Nothing
        Dim objValorMedioPago_DeclaradoOContado As Clases.ValorMedioPago = Nothing

        If (Not String.IsNullOrEmpty(txtImporte.Text) AndAlso Not CDbl(txtImporte.Text) = 0) AndAlso _
           String.IsNullOrEmpty(txtCantidad.Text) Then

            txtCantidad.Text = "1"

        End If

        If (Not String.IsNullOrEmpty(txtCantidad.Text) AndAlso Not CInt(txtCantidad.Text) = 0) AndAlso _
           (Not String.IsNullOrEmpty(txtImporte.Text) AndAlso Not CDbl(txtImporte.Text) = 0) Then

            Dim objValorMedioPago As New Clases.ValorMedioPago
            objValorMedioPago.Cantidad = txtCantidad.Text
            objValorMedioPago.Importe = txtImporte.Text

            If itemDivisa.MediosPago(repeaterItemMedioPago.ItemIndex).Valores IsNot Nothing AndAlso itemDivisa.MediosPago(repeaterItemMedioPago.ItemIndex).Valores.Count > 0 Then
                ValoresMedioPago = itemDivisa.MediosPago(repeaterItemMedioPago.ItemIndex).Valores
                Dim objValorMedioPagoDC As Clases.ValorMedioPago = ValoresMedioPago.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

                If objValorMedioPagoDC IsNot Nothing Then
                    ValoresMedioPago.Remove(objValorMedioPagoDC)
                    objValorMedioPago.TipoValor = objValorMedioPagoDC.TipoValor
                    objValorMedioPagoDC = objValorMedioPago
                    ValoresMedioPago.Add(objValorMedioPagoDC)

                Else
                    objValorMedioPago.TipoValor = TipoValor
                    ValoresMedioPago.Add(objValorMedioPago)

                End If

            Else
                objValorMedioPago.TipoValor = TipoValor
                ValoresMedioPago = New ObservableCollection(Of Clases.ValorMedioPago)
                ValoresMedioPago.Add(objValorMedioPago)

            End If

        Else
            If itemMedioPago.Valores IsNot Nothing Then
                For index = 0 To itemMedioPago.Valores.Count - 1

                    If itemMedioPago.Valores(index).TipoValor = TipoValor Then

                        itemMedioPago.Valores.RemoveAt(index)
                        Exit For

                    End If

                Next index

                ValoresMedioPago = itemMedioPago.Valores

            End If

        End If

        If ValoresMedioPago IsNot Nothing AndAlso ValoresMedioPago.Count = 0 Then
            ValoresMedioPago = Nothing

        End If
        itemMedioPago.Valores = ValoresMedioPago

        ' actualiza en la divisa los cambios hechos en un determinado mediopago
        ActualizarItemMedioPago(itemDivisa, itemMedioPago)

    End Sub

    ''' <summary>
    ''' Actualiza en la divisa los cambios hechos en un determinado mediopago
    ''' </summary>
    ''' <param name="itemDivisa">Clase divisa</param>
    ''' <param name="itemMedioPago">Clase mediopago</param>
    ''' <remarks></remarks>
    Private Sub ActualizarItemMedioPago(ByRef itemDivisa As Clases.Divisa, _
                                        ByRef itemMedioPago As Clases.MedioPago)

        Dim objMedioPagoAux As Clases.MedioPago = Nothing
        For Each _medioPago In itemDivisa.MediosPago
            If _medioPago.Identificador = itemMedioPago.Identificador Then
                objMedioPagoAux = itemMedioPago
                itemDivisa.MediosPago.Remove(_medioPago)
                Exit For
            End If

        Next _medioPago

        ' recarregar lista de medios pago ordenada.
        Dim IListMediosPago As New List(Of Clases.MedioPago)
        IListMediosPago.AddRange(itemDivisa.MediosPago.OrderBy(Function(f) f.Descripcion))
        ' añadir objeto cambiado en el proceso.
        IListMediosPago.Add(objMedioPagoAux)
        ' limpiar lista actual de medios pago.
        itemDivisa.MediosPago.Clear()
        ' recargar lista actualizada en la divisa.
        itemDivisa.MediosPago.AddRange(IListMediosPago.OrderBy(Function(f) f.Descripcion))

    End Sub

    ''' <summary>
    ''' Actualiza en la divisa los cambios hechos en un determinado mediopago
    ''' </summary>
    ''' <param name="itemMedioPago">objeto MedioPago</param>
    ''' <param name="itemTermino">objeto Termino</param>
    ''' <remarks></remarks>
    Private Sub ActualizarItemTermino(ByRef itemMedioPago As Clases.MedioPago, _
                                      ByRef itemTermino As Clases.Termino)

        Dim objTerminoAux As Clases.Termino = Nothing
        For Each _termino In itemMedioPago.Terminos
            If _termino.Identificador = itemTermino.Identificador Then
                objTerminoAux = itemTermino
                itemMedioPago.Terminos.Remove(itemTermino)
                Exit For
            End If

        Next _termino

        ' recarregar lista de medios pago ordenada.
        Dim IListTermino As New ObservableCollection(Of Clases.Termino)
        IListTermino.AddRange(itemMedioPago.Terminos.OrderBy(Function(f) f.Descripcion))
        ' añadir objeto cambiado en el proceso.
        IListTermino.Add(objTerminoAux)
        ' limpiar lista actual de medios pago.
        itemMedioPago.Terminos.Clear()
        ' recargar lista actualizada en la divisa.
        itemMedioPago.Terminos.AddRange(IListTermino.OrderBy(Function(f) f.Descripcion))

    End Sub

    ''' <summary>
    ''' Actualizar los valores del tipo medio pago de la divisa. 
    ''' </summary>
    ''' <descripcion>
    ''' Posibilidad:
    ''' { Declarado o Contado y Ticket, Tarjeta, OtroValor, Cheque }
    ''' </descripcion>
    ''' <param name="itemDivisa">clase Divisa</param>
    ''' <param name="txtImporteTpMedioPago">Controle ImporteTipoMedioPago</param>
    ''' <param name="CodTipoMedioPago">String Codigo TipoMedioPago</param>  
    ''' <remarks></remarks>
    Private Sub ActualizarValoresTipoMedioPago(ByRef itemDivisa As Clases.Divisa, _
                                               txtImporteTpMedioPago As TextBox, _
                                               CodTipoMedioPago As String)


        ' Lista de valores tipo medio pago. { Ticket, Cheque, Tarjeta, OtroValor }
        Dim IListValoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = itemDivisa.ValoresTotalesTipoMedioPago

        Dim valorTipoMedioPago As Clases.ValorTipoMedioPago = Nothing
        If IListValoresTipoMedioPago IsNot Nothing AndAlso IListValoresTipoMedioPago.Count > 0 Then
            ' recupera el objeto de un determinado TipoValor y CodTipo. { Contado y Declarado o Ticket, Tarjeta, OtroValor, Cheque }.
            valorTipoMedioPago = IListValoresTipoMedioPago.Where(Function(f) f.TipoValor = TipoValor AndAlso f.TipoMedioPago = CodTipoMedioPago).FirstOrDefault

        End If

        Dim ValoresTipoMedioPago As ObservableCollection(Of Clases.ValorTipoMedioPago) = Nothing
        Dim objValorTipoMedioPago_Tipo As Clases.ValorTipoMedioPago = Nothing

        If Not String.IsNullOrEmpty(txtImporteTpMedioPago.Text) AndAlso Not CInt(txtImporteTpMedioPago.Text) = 0 Then
            Dim objValorTipoMedioPago As New Clases.ValorTipoMedioPago
            objValorTipoMedioPago.Importe = txtImporteTpMedioPago.Text
            objValorTipoMedioPago.TipoMedioPago = CodTipoMedioPago
            objValorTipoMedioPago.TipoValor = TipoValor

            If itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso itemDivisa.ValoresTotalesTipoMedioPago.Count > 0 Then
                ValoresTipoMedioPago = itemDivisa.ValoresTotalesTipoMedioPago
                objValorTipoMedioPago_Tipo = ValoresTipoMedioPago.Where(Function(f) f.TipoValor = TipoValor AndAlso f.TipoMedioPago = CodTipoMedioPago).FirstOrDefault

                If objValorTipoMedioPago_Tipo IsNot Nothing Then

                    ValoresTipoMedioPago.Remove(objValorTipoMedioPago_Tipo)
                    objValorTipoMedioPago.TipoValor = objValorTipoMedioPago_Tipo.TipoValor
                    objValorTipoMedioPago.TipoMedioPago = objValorTipoMedioPago_Tipo.TipoMedioPago
                    objValorTipoMedioPago_Tipo = objValorTipoMedioPago
                    ValoresTipoMedioPago.Add(objValorTipoMedioPago_Tipo)

                Else
                    ValoresTipoMedioPago.Add(objValorTipoMedioPago)

                End If

            Else
                ValoresTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                ValoresTipoMedioPago.Add(objValorTipoMedioPago)

            End If

        Else
            If itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                For index = 0 To itemDivisa.ValoresTotalesTipoMedioPago.Count - 1

                    If itemDivisa.ValoresTotalesTipoMedioPago(index).TipoValor = TipoValor AndAlso itemDivisa.ValoresTotalesTipoMedioPago(index).TipoMedioPago = CodTipoMedioPago Then
                        itemDivisa.ValoresTotalesTipoMedioPago.RemoveAt(index)
                        Exit For

                    End If

                Next index

                ValoresTipoMedioPago = itemDivisa.ValoresTotalesTipoMedioPago

            End If

        End If

        If ValoresTipoMedioPago IsNot Nothing AndAlso ValoresTipoMedioPago.Count = 0 Then
            ValoresTipoMedioPago = Nothing
        End If

        itemDivisa.ValoresTotalesTipoMedioPago = ValoresTipoMedioPago

    End Sub

    ''' <summary>
    ''' Generar dinamicamente los detalles del medio pago
    ''' </summary>
    ''' <param name="pnlDetallesMedioPago">panel donde será añadido la tabla container, conteniendo os itens e los terminos de medio pago.</param>
    ''' <param name="ListaMediosPago">lista de medio pagos por tipo.</param>
    ''' <param name="itemDivisa">divisa actual de los medio pagos</param>
    ''' <param name="lblchkMostrarTerminos">label de titulo del checkbox que indica si mostrará los terminos de medio pago o no.</param>
    ''' <param name="chkMostrarTerminos">controle checkbox que indica si mostrará los terminos de medio pago o no.</param>
    ''' <remarks></remarks>
    Private Sub GenerarDetallesMedioPagoConTitulos(pnlDetallesMedioPago As Panel, _
                                                   ListaMediosPago As ObservableCollection(Of Clases.MedioPago), _
                                                   itemDivisa As Clases.Divisa, _
                                                   ByRef lblchkMostrarTerminos As Label, _
                                                   ByRef chkMostrarTerminos As CheckBox)

        '############### Containers para los itens del medio pago. ###############
        ' tabla que irá añadir los itens del tipo de medio pago.
        Dim tableItensMedioPago As HtmlTable = New HtmlTable
        tableItensMedioPago.ID = "tblItensMedioPago"
        Dim cellItensMedioPago As HtmlTableCell = Nothing
        Dim rowItensMedioPago As HtmlTableRow = Nothing
        '############### Containers para los itens del medio pago. ###############

        Dim lblTitulo As Label = Nothing
        Dim txtDescripcion As TextBox = Nothing
        Dim txtImporte As TextBox = Nothing
        Dim txtCantidad As TextBox = Nothing

        Dim bolMostrarUnico As Boolean = True
        Dim bolTituloUnico As Boolean = True
        For Each _itemMedioPago In ListaMediosPago
            Dim _itemMedioPagoLocal = _itemMedioPago

            If bolMostrarUnico Then
                ' CheckBox mostrar los terminos
                chkMostrarTerminos.Checked = If(ListaMediosPago.Exists(Function(f) f.Terminos IsNot Nothing), True, False)
                chkMostrarTerminos.Visible = chkMostrarTerminos.Checked
                lblchkMostrarTerminos.Visible = chkMostrarTerminos.Checked
                bolMostrarUnico = False

                chkMostrarTerminos.ID = "chkMostrarTerminos_" & _itemMedioPago.Identificador

                ActualizarEstadoCampo(chkMostrarTerminos)

            End If

            ' recebe el identificador del mediopago
            Dim hdfIdentificadorMedioPago As HiddenField = New HiddenField
            hdfIdentificadorMedioPago.ID = "hdfIdentificadorMedioPago_" & _itemMedioPago.Identificador
            hdfIdentificadorMedioPago.Value = _itemMedioPago.Identificador

            If Not chkMostrarTerminos.Checked Then
                If bolTituloUnico Then
                    InserirTitulosMedioPago(rowItensMedioPago, cellItensMedioPago, lblTitulo, _itemMedioPago, tableItensMedioPago)
                    bolTituloUnico = False
                End If
            Else
                InserirTitulosMedioPago(rowItensMedioPago, cellItensMedioPago, lblTitulo, _itemMedioPago, tableItensMedioPago)
            End If


            Dim bolTerminosValor As Boolean = False
            Dim Terminos As ObservableCollection(Of Clases.Termino) = Nothing
            If _itemMedioPago.Valores IsNot Nothing AndAlso _itemMedioPago.Valores.Count > 0 Then
                Dim Valores As Clases.ValorMedioPago = _itemMedioPago.Valores.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault
                Terminos = If(Valores IsNot Nothing, Valores.Terminos, Nothing)
            End If
            If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.Baja Then
                If Terminos IsNot Nothing AndAlso Terminos.Count > 0 Then
                    AnadirTituloTermino(_itemMedioPago.Identificador, Terminos, rowItensMedioPago, cellItensMedioPago, tableItensMedioPago, chkMostrarTerminos.Checked)
                    bolTerminosValor = True
                End If
            Else
                If _itemMedioPago.Terminos IsNot Nothing AndAlso _itemMedioPago.Terminos.Count > 0 Then
                    AnadirTituloTermino(_itemMedioPago.Identificador, _itemMedioPago.Terminos, rowItensMedioPago, cellItensMedioPago, tableItensMedioPago, chkMostrarTerminos.Checked)

                End If
            End If


            '###################### crear textos (campos) ######################
            ' nueva línea.
            rowItensMedioPago = New HtmlTableRow

            '' nueva célula para el campo Descripción.
            cellItensMedioPago = New HtmlTableCell
            txtDescripcion = New TextBox
            txtDescripcion.ID = "txtDescripcion_" & _itemMedioPago.Identificador
            txtDescripcion.SkinID = "filter-textbox"
            txtDescripcion.Text = _itemMedioPago.Descripcion
            txtDescripcion.Enabled = False
            cellItensMedioPago.Controls.Add(txtDescripcion)
            rowItensMedioPago.Cells.Add(cellItensMedioPago)

            ' recibe el objeto MedioPago currente en la coleccion.
            Dim ItemMedioPago As Clases.MedioPago = itemDivisa.MediosPago.Where(Function(f) f.Identificador = _itemMedioPagoLocal.Identificador).FirstOrDefault
            ' recibe la lista de valores de medio pago.
            Dim valoresTermino As ObservableCollection(Of Clases.ValorMedioPago) = ItemMedioPago.Valores
            Dim valorMedioPago As Clases.ValorMedioPago = Nothing
            If valoresTermino IsNot Nothing AndAlso valoresTermino.Count > 0 Then
                valorMedioPago = valoresTermino.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

            End If

            ' nueva célula para el campo Importe.
            cellItensMedioPago = New HtmlTableCell
            txtImporte = New TextBox
            txtImporte.ID = "txtImporte_" & _itemMedioPago.Identificador
            txtImporte.SkinID = "filter-textbox"
            txtImporte.AutoPostBack = False
            txtImporte.MaxLength = 26
            txtImporte.Text = If(valorMedioPago IsNot Nothing, String.Format("{0:N2}", valorMedioPago.Importe), "")
            'RemoveHandler txtImporte.TextChanged, AddressOf txtImporte_TextChanged
            'AddHandler txtImporte.TextChanged, AddressOf txtImporte_TextChanged
            cellItensMedioPago.Controls.Add(txtImporte)
            rowItensMedioPago.Cells.Add(cellItensMedioPago)

            Aplicacao.Util.Utilidad.CargarScripts(txtImporte, MyBase._DecimalSeparador, MyBase._MilharSeparador, "I")

            ' nueva célula para el campo Cantidad.
            cellItensMedioPago = New HtmlTableCell
            txtCantidad = New TextBox
            txtCantidad.ID = "txtCantidad_" & _itemMedioPago.Identificador
            txtCantidad.SkinID = "filter-textbox"
            txtCantidad.AutoPostBack = False
            txtCantidad.MaxLength = 14
            txtCantidad.Text = If(valorMedioPago IsNot Nothing, valorMedioPago.Cantidad, "")
            If _itemMedioPago.Terminos IsNot Nothing AndAlso _itemMedioPago.Terminos.Count > 0 Then
                txtCantidad.Text = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, "1")
                txtCantidad.Enabled = False

            End If
            'RemoveHandler txtCantidad.TextChanged, AddressOf txtCantidad_TextChanged
            'AddHandler txtCantidad.TextChanged, AddressOf txtCantidad_TextChanged
            cellItensMedioPago.Controls.Add(txtCantidad)
            rowItensMedioPago.Cells.Add(cellItensMedioPago)
            'txtCantidad.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
            Aplicacao.Util.Utilidad.CargarScripts(txtCantidad, MyBase._DecimalSeparador, MyBase._MilharSeparador, "C")

            ' añadir línea en la tabla 'tableDetalles'.
            tableItensMedioPago.Rows.Add(rowItensMedioPago)

            If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.Baja Then
                If bolTerminosValor Then
                    AnadirCampoTermino(_itemMedioPago.Identificador, Terminos, rowItensMedioPago, cellItensMedioPago, tableItensMedioPago, chkMostrarTerminos.Checked)
                    ActualizarEstadoCampo(txtCantidad)
                End If

            Else
                If _itemMedioPago.Terminos IsNot Nothing AndAlso _itemMedioPago.Terminos.Count > 0 Then
                    AnadirCampoTermino(_itemMedioPago.Identificador, _itemMedioPago.Terminos, rowItensMedioPago, cellItensMedioPago, tableItensMedioPago, chkMostrarTerminos.Checked, Terminos)

                End If
            End If
            If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.Baja Then
                txtCantidad.Enabled = False
            End If
            ActualizarEstadoCampo(txtImporte)

        Next _itemMedioPago

        ' añadir la ''tableItensMedioPago'' en el panel ''pnlDetallesMedioPago''.
        pnlDetallesMedioPago.Controls.Add(tableItensMedioPago)

    End Sub

    Private Sub InserirTitulosMedioPago(ByRef rowItensMedioPago As HtmlTableRow, _
                                        ByRef cellItensMedioPago As HtmlTableCell, _
                                        ByRef lblTitulo As Label, _
                                        ByRef _itemMedioPago As Clases.MedioPago, _
                                        ByRef tableItensMedioPago As HtmlTable)

        '###################### crear labels (titulos) de los campos ######################
        ' nueva línea.
        rowItensMedioPago = New HtmlTableRow

        ' nueva célula para el titulo Descripción.
        cellItensMedioPago = New HtmlTableCell
        lblTitulo = New Label
        lblTitulo.ID = "lblDescripcion_" & _itemMedioPago.Identificador
        lblTitulo.SkinID = "filter-label"
        lblTitulo.Text = Traduzir("012_descripcion")
        cellItensMedioPago.Controls.Add(lblTitulo)
        rowItensMedioPago.Cells.Add(cellItensMedioPago)

        ' nueva célula para el título Importe.
        cellItensMedioPago = New HtmlTableCell
        lblTitulo = New Label
        lblTitulo.ID = "lblImporte_" & _itemMedioPago.Identificador
        lblTitulo.SkinID = "filter-label"
        lblTitulo.Text = Traduzir("012_importe")
        cellItensMedioPago.Controls.Add(lblTitulo)
        rowItensMedioPago.Cells.Add(cellItensMedioPago)

        ' nueva célula para el título Cantidad.
        cellItensMedioPago = New HtmlTableCell
        lblTitulo = New Label
        lblTitulo.ID = "lblCantidad_" & _itemMedioPago.Identificador
        lblTitulo.SkinID = "filter-label"
        lblTitulo.Text = Traduzir("012_cantidad")
        cellItensMedioPago.Controls.Add(lblTitulo)
        rowItensMedioPago.Cells.Add(cellItensMedioPago)
        tableItensMedioPago.Rows.Add(rowItensMedioPago)

    End Sub

    ' ''' <summary>
    ' ''' Generar dinamicamente los detalles del medio pago
    ' ''' </summary>
    ' ''' <param name="pnlDetallesMedioPago">panel donde será añadido la tabla container, conteniendo os itens e los terminos de medio pago.</param>
    ' ''' <param name="ListaMediosPago">lista de medio pagos por tipo.</param>
    ' ''' <param name="itemDivisa">divisa actual de los medio pagos</param>
    ' ''' <param name="lblchkMostrarTerminos">label de titulo del checkbox que indica si mostrará los terminos de medio pago o no.</param>
    ' ''' <param name="chkMostrarTerminos">controle checkbox que indica si mostrará los terminos de medio pago o no.</param>
    ' ''' <remarks></remarks>
    'Private Sub GenerarDetallesMedioPagoSinTitulos(pnlDetallesMedioPago As Panel, _
    '                                               ListaMediosPago As List(Of Clases.MedioPago), _
    '                                               itemDivisa As Clases.Divisa, _
    '                                               ByRef lblchkMostrarTerminos As Label, _
    '                                               ByRef chkMostrarTerminos As CheckBox)

    '    '############### Containers para los itens del mediopago. ###############
    '    ' tabla que irá añadir los itens del tipo de medio pago.
    '    Dim tableItensMedioPago As HtmlTable = New HtmlTable
    '    tableItensMedioPago.ID = "tblItensMedioPago"
    '    Dim cellItensMedioPago As HtmlTableCell = Nothing
    '    Dim rowItensMedioPago As HtmlTableRow = Nothing
    '    '############### Containers para los itens del mediopago. ###############

    '    Dim lblTitulo As Label = Nothing
    '    Dim txtDescripcion As TextBox = Nothing
    '    Dim txtImporte As TextBox = Nothing
    '    Dim txtCantidad As TextBox = Nothing

    '    Dim bolMostrarUnico As Boolean = True
    '    For Each _itemMedioPago In ListaMediosPago

    '        If bolMostrarUnico Then
    '            ' CheckBox mostrar los terminos
    '            'chkMostrarTerminos.Checked = If(ListaMediosPago.Exists(Function(f) f.Terminos IsNot Nothing), True, False)
    '            'chkMostrarTerminos.Visible = chkMostrarTerminos.Checked
    '            'lblchkMostrarTerminos.Visible = chkMostrarTerminos.Checked
    '            bolMostrarUnico = False
    '            chkMostrarTerminos.ID = "chkMostrarTerminos_" & _itemMedioPago.Identificador
    '            'ActualizarEstadoCampo(chkMostrarTerminos)

    '            '###################### crear labels (titulos) de los campos ######################
    '            ' nueva línea.
    '            rowItensMedioPago = New HtmlTableRow

    '            ' nueva célula para el titulo Descripción.
    '            cellItensMedioPago = New HtmlTableCell
    '            lblTitulo = New Label
    '            lblTitulo.ID = "lblDescripcion_" & _itemMedioPago.Identificador
    '            lblTitulo.SkinID = "filter-label"
    '            lblTitulo.Text = Traduzir("012_descripcion")
    '            cellItensMedioPago.Controls.Add(lblTitulo)
    '            rowItensMedioPago.Cells.Add(cellItensMedioPago)

    '            ' nueva célula para el título Importe.
    '            cellItensMedioPago = New HtmlTableCell
    '            lblTitulo = New Label
    '            lblTitulo.ID = "lblImporte_" & _itemMedioPago.Identificador
    '            lblTitulo.SkinID = "filter-label"
    '            lblTitulo.Text = Traduzir("012_importe")
    '            cellItensMedioPago.Controls.Add(lblTitulo)
    '            rowItensMedioPago.Cells.Add(cellItensMedioPago)

    '            ' nueva célula para el título Cantidad.
    '            cellItensMedioPago = New HtmlTableCell
    '            lblTitulo = New Label
    '            lblTitulo.ID = "lblCantidad_" & _itemMedioPago.Identificador
    '            lblTitulo.SkinID = "filter-label"
    '            lblTitulo.Text = Traduzir("012_cantidad")
    '            cellItensMedioPago.Controls.Add(lblTitulo)
    '            rowItensMedioPago.Cells.Add(cellItensMedioPago)
    '            tableItensMedioPago.Rows.Add(rowItensMedioPago)

    '        End If

    '        ' recebe el identificador del mediopago
    '        Dim hdfIdentificadorMedioPago As HiddenField = New HiddenField
    '        hdfIdentificadorMedioPago.ID = "hdfIdentificadorMedioPago_" & _itemMedioPago.Identificador
    '        hdfIdentificadorMedioPago.Value = _itemMedioPago.Identificador

    '        '###################### crear textos (campos) ######################
    '        ' nueva línea.
    '        rowItensMedioPago = New HtmlTableRow

    '        '' nueva célula para el campo Descripción.
    '        cellItensMedioPago = New HtmlTableCell
    '        txtDescripcion = New TextBox
    '        txtDescripcion.ID = "txtDescripcion_" & _itemMedioPago.Identificador
    '        txtDescripcion.SkinID = "filter-textbox"
    '        txtDescripcion.Text = _itemMedioPago.Descripcion
    '        txtDescripcion.Enabled = False
    '        cellItensMedioPago.Controls.Add(txtDescripcion)
    '        rowItensMedioPago.Cells.Add(cellItensMedioPago)

    '        ' recibe el objeto MedioPago currente en la coleccion.
    '        Dim ItemMedioPago As Clases.MedioPago = itemDivisa.MediosPago.Where(Function(f) f.Identificador = _itemMedioPago.Identificador).FirstOrDefault
    '        ' recibe la lista de valores de medio pago.
    '        Dim valoresTermino As List(Of Clases.ValorMedioPago) = ItemMedioPago.Valores
    '        Dim valorMedioPago As Clases.ValorMedioPago = Nothing
    '        If valoresTermino IsNot Nothing AndAlso valoresTermino.Count > 0 Then
    '            valorMedioPago = valoresTermino.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

    '        End If

    '        ' nueva célula para el campo Importe.
    '        cellItensMedioPago = New HtmlTableCell
    '        txtImporte = New TextBox
    '        txtImporte.ID = "txtImporte_" & _itemMedioPago.Identificador
    '        txtImporte.SkinID = "filter-textbox"
    '        txtImporte.AutoPostBack = False
    '        txtImporte.MaxLength = 26
    '        txtImporte.Text = If(valorMedioPago IsNot Nothing, valorMedioPago.Importe, "")

    '        cellItensMedioPago.Controls.Add(txtImporte)
    '        rowItensMedioPago.Cells.Add(cellItensMedioPago)

    '        Aplicacao.Util.Utilidad.CargarScripts(txtImporte, MyBase._DecimalSeparador, MyBase._MilharSeparador, "I")

    '        ' nueva célula para el campo Cantidad.
    '        cellItensMedioPago = New HtmlTableCell
    '        txtCantidad = New TextBox
    '        txtCantidad.ID = "txtCantidad_" & _itemMedioPago.Identificador
    '        txtCantidad.SkinID = "filter-textbox"
    '        txtCantidad.AutoPostBack = False
    '        txtCantidad.MaxLength = 14
    '        txtCantidad.Text = If(valorMedioPago IsNot Nothing, valorMedioPago.Cantidad, "")
    '        If _itemMedioPago.Terminos IsNot Nothing AndAlso _itemMedioPago.Terminos.Count > 0 Then
    '            txtCantidad.Text = If(Not String.IsNullOrEmpty(txtCantidad.Text), txtCantidad.Text, "1")
    '            txtCantidad.Enabled = False

    '        End If

    '        cellItensMedioPago.Controls.Add(txtCantidad)
    '        rowItensMedioPago.Cells.Add(cellItensMedioPago)

    '        Aplicacao.Util.Utilidad.CargarScripts(txtCantidad, MyBase._DecimalSeparador, MyBase._MilharSeparador, "C")

    '        ' añadir línea en la tabla 'tableDetalles'.
    '        tableItensMedioPago.Rows.Add(rowItensMedioPago)

    '        If Modo = Enumeradores.Modo.Consulta OrElse Modo = Enumeradores.Modo.Baja Then
    '            txtCantidad.Enabled = False
    '        End If
    '        ActualizarEstadoCampo(txtImporte)

    '    Next _itemMedioPago

    '    ' añadir la ''tableItensMedioPago'' en el panel ''pnlDetallesMedioPago''.
    '    pnlDetallesMedioPago.Controls.Add(tableItensMedioPago)

    'End Sub

    ''' <summary>
    ''' Cargar valores iniciais del mediopago
    ''' </summary>
    ''' <param name="itemMedioPago">objeto MedioPago</param>
    ''' <param name="itemTermino">objeto Termino</param>
    ''' <param name="controlPadron">Controle Padrao TextBox. {Importe o Cantidad}</param>
    ''' <param name="controlTermino">Controle Termino. {TextBox, CheckBox, DropDownList</param>
    ''' <param name="identificionCampo">String Identificador del campo padron. {I = Importe, C = Cantidad}</param>
    ''' <remarks></remarks>
    Private Sub CargarValoresIniciaisMedioPago(Optional itemMedioPago As Clases.MedioPago = Nothing, _
                                               Optional itemTermino As Clases.Termino = Nothing, _
                                               Optional ByRef controlPadron As Control = Nothing, _
                                               Optional ByRef controlTermino As Control = Nothing, _
                                               Optional identificionCampo As Char = "")

        If itemMedioPago IsNot Nothing Then
            ' recupera los valores del mediopago
            Dim ValoresMedioPago As ObservableCollection(Of Clases.ValorMedioPago) = itemMedioPago.Valores
            Dim valorMedioPago As Clases.ValorMedioPago = Nothing
            If ValoresMedioPago IsNot Nothing AndAlso ValoresMedioPago.Count > 0 Then
                ' recupera el tipo especifico del valor mediopago. {Declarado o Contado}
                valorMedioPago = ValoresMedioPago.Where(Function(f) f.TipoValor = TipoValor).FirstOrDefault

            End If

            ' Cheque en cual campo es ejecutado la validación. {Importe o Cantidad}
            Select Case identificionCampo
                Case "I"
                    DirectCast(controlPadron, TextBox).Text = If(valorMedioPago IsNot Nothing, valorMedioPago.Importe, "")
                Case "C"
                    DirectCast(controlPadron, TextBox).Text = If(valorMedioPago IsNot Nothing, valorMedioPago.Cantidad, "")
            End Select

        End If

        If itemTermino IsNot Nothing Then

            If TypeOf controlTermino Is TextBox Then

                If controlPadron IsNot Nothing Then
                    Select Case identificionCampo
                        Case "C"
                            DirectCast(controlPadron, TextBox).Text = If(Not String.IsNullOrEmpty(DirectCast(controlPadron, TextBox).Text), DirectCast(controlPadron, TextBox).Text, "1")
                            DirectCast(controlPadron, TextBox).Enabled = False

                    End Select

                End If

                DirectCast(controlTermino, TextBox).Text = If(itemTermino.ValoresPosibles IsNot Nothing AndAlso itemTermino.ValoresPosibles.Count = 1, itemTermino.ValoresPosibles(0).Descripcion, If(Not String.IsNullOrEmpty(itemTermino.Valor), itemTermino.Valor, itemTermino.ValorInicial))

            ElseIf TypeOf controlTermino Is CheckBox Then
                DirectCast(controlTermino, CheckBox).Checked = If(itemTermino.Valor = "1", True, False)

            ElseIf TypeOf controlTermino Is DropDownList Then

                If Not String.IsNullOrEmpty(itemTermino.Valor) Then
                    Dim Identificador As String = itemTermino.ValoresPosibles.Where(Function(f) f.Descripcion = If(Not String.IsNullOrEmpty(itemTermino.Valor), itemTermino.Valor, itemTermino.ValorInicial)).FirstOrDefault.Identificador
                    If Not String.IsNullOrEmpty(Identificador) Then
                        DirectCast(controlTermino, DropDownList).SelectedValue = Identificador

                    End If
                Else
                    DirectCast(controlTermino, DropDownList).SelectedIndex = 0
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Añadir los titulos de terminos de uno determinado mediopago
    ''' </summary>
    ''' <param name="IdentificadorMedioPago">Objeto MedioPago</param>
    ''' <param name="rowItensMedioPago">Controle Row do HTMLTable</param>
    ''' <param name="cellItensMedioPago">Controle Cell do HTMLTable</param>
    ''' <param name="tableItensMedioPago">Controle Table do HTMLTable</param>
    ''' <param name="bolMostrarterminos">Booleano que indica si el CheckBox mostrarterminos está activo o no.</param>
    ''' <remarks></remarks>
    Private Sub AnadirTituloTermino(IdentificadorMedioPago As String, _
                                    Terminos As ObservableCollection(Of Clases.Termino), _
                                    ByRef rowItensMedioPago As HtmlTableRow, _
                                    ByRef cellItensMedioPago As HtmlTableCell, _
                                    ByRef tableItensMedioPago As HtmlTable, _
                                    bolMostrarterminos As Boolean)

        '###################### crear labels (titulos) de los campos de termino de medio pago ######################
        For Each _termino In Terminos
            cellItensMedioPago = New HtmlTableCell
            Dim lbltermino = New Label
            lbltermino.ID = "lbltermino_" & IdentificadorMedioPago & "_" & _termino.Identificador
            lbltermino.Text = _termino.Descripcion
            lbltermino.Visible = bolMostrarterminos
            If _termino.Formato IsNot Nothing AndAlso _termino.Formato.Codigo = 6 Then
                cellItensMedioPago.Align = "center"
                lbltermino.SkinID = "filtro-label-aux"
            Else
                lbltermino.SkinID = "filtro-label"
            End If
            cellItensMedioPago.Controls.Add(lbltermino)
            rowItensMedioPago.Cells.Add(cellItensMedioPago)

        Next _termino
        tableItensMedioPago.Rows.Add(rowItensMedioPago)

    End Sub

    ''' <summary>
    ''' Añadir los campos de terminos de uno determinado mediopago
    ''' </summary>
    ''' <param name="IdentificadorMedioPago">Identificador del medio pago</param>
    ''' <param name="rowItensMedioPago">Controle Row do HTMLTable</param>
    ''' <param name="cellItensMedioPago">Controle Cell do HTMLTable</param>
    ''' <param name="tableItensMedioPago">Controle Table do HTMLTable</param>
    ''' <param name="bolMostrarTerminos">Booleano que indica si el CheckBox mostrarterminos está activo o no.</param>
    ''' <remarks></remarks>
    Private Sub AnadirCampoTermino(IdentificadorMedioPago As String, _
                                   Terminos As ObservableCollection(Of Clases.Termino), _
                                   ByRef rowItensMedioPago As HtmlTableRow, _
                                   ByRef cellItensMedioPago As HtmlTableCell, _
                                   ByRef tableItensMedioPago As HtmlTable, _
                                   bolMostrarTerminos As Boolean, _
                          Optional TerminosConValor As ObservableCollection(Of Clases.Termino) = Nothing)

        ' generar terminos de medio pago
        For Each _termino In Terminos
            Dim _terminoLocal = _termino
            cellItensMedioPago = New HtmlTableCell

            Dim Termino As Clases.Termino = Nothing
            If TerminosConValor IsNot Nothing AndAlso TerminosConValor.Count > 0 Then
                Termino = TerminosConValor.Where(Function(f) f.Identificador = _terminoLocal.Identificador).FirstOrDefault
                If Termino Is Nothing Then
                    Termino = _termino
                End If
            Else
                Termino = _termino
            End If

            ' cheque si hay formato para el termino currente
            If Termino.Formato IsNot Nothing Then

                Dim fterminoD As DropDownList = Nothing
                Dim fterminoT As TextBox = Nothing
                Dim fterminoC As CheckBox = Nothing

                ' case para definir el tipo de termino del medio pago
                Select Case Termino.Formato.Codigo
                    Case 1, 2, 3, 4, 5 'Texto

                        If Termino.ValoresPosibles IsNot Nothing AndAlso Termino.ValoresPosibles.Count > 1 Then
                            fterminoD = New DropDownList
                            fterminoD.ID = "ftermino_" & IdentificadorMedioPago & "_" & Termino.Identificador
                            fterminoD.SkinID = "form-dropdownlist-mandatory"
                            fterminoD.AutoPostBack = False
                            fterminoD.Visible = bolMostrarTerminos

                            fterminoD.DataSource = Termino.ValoresPosibles
                            fterminoD.DataTextField = "Descripcion"
                            fterminoD.DataValueField = "Identificador"
                            fterminoD.DataBind()


                            If Not String.IsNullOrEmpty(Termino.Valor) Then
                                Dim Identificador As String = String.Empty 'Termino.ValoresPosibles.Where(Function(f) f.Descripcion = If(Not String.IsNullOrEmpty(Termino.Valor), Termino.Valor, Termino.ValorInicial)).FirstOrDefault.Identificador
                                For Each valorPosible In Termino.ValoresPosibles
                                    If valorPosible.Descripcion = Termino.Valor OrElse valorPosible.Descripcion = Termino.ValorInicial Then
                                        Identificador = valorPosible.Identificador
                                        Exit For
                                    End If
                                Next valorPosible
                                If Not String.IsNullOrEmpty(Identificador) Then
                                    fterminoD.SelectedValue = Identificador
                                End If

                            End If

                            ActualizarEstadoCampo(fterminoD)

                        Else
                            fterminoT = New TextBox
                            fterminoT.ID = "ftermino_" & IdentificadorMedioPago & "_" & Termino.Identificador
                            fterminoT.AutoPostBack = False
                            fterminoT.Text = If(Termino.ValoresPosibles IsNot Nothing AndAlso Termino.ValoresPosibles.Count = 1, Termino.ValoresPosibles(0).Descripcion, If(Not String.IsNullOrEmpty(Termino.Valor), Termino.Valor, Termino.ValorInicial))
                            fterminoT.SkinID = "filter-textbox"
                            fterminoT.Visible = bolMostrarTerminos
                            If Termino.Mascara IsNot Nothing AndAlso Not String.IsNullOrEmpty(Termino.Mascara.ExpresionRegular) Then
                                If Not RegExpr.ContainsKey(Termino.Identificador) Then
                                    RegExpr.Add(Termino.Identificador, Termino.Mascara.ExpresionRegular)
                                Else
                                    RegExpr(Termino.Identificador) = Termino.Mascara.ExpresionRegular
                                End If
                            End If

                            Select Case Termino.Formato.Codigo
                                Case 1 'Texto
                                    fterminoT.MaxLength = If(Not IsDBNull(Termino.Longitud), Termino.Longitud, 255)
                                Case 2 'Entero
                                    fterminoT.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
                                    fterminoT.MaxLength = If(Not IsDBNull(Termino.Longitud), Termino.Longitud + 4, 255)
                                Case 3 'Decimal
                                    fterminoT.Attributes.Add("onkeypress", "return decimais(event);")
                                    fterminoT.MaxLength = If(Not IsDBNull(Termino.Longitud), Termino.Longitud, 26)
                                Case 4 'Fecha
                                    fterminoT.Attributes.Add("onkeypress", "DataHora(this,event);")
                                    fterminoT.Attributes.Add("onBlur", "validacionFechaHora(this,'" & String.Format(Traduzir("err_campo_data_invalida"), Termino.Descripcion) & "','" & String.Format(Traduzir("err_campo_hora_invalida"), Termino.Descripcion) & "','" & Traduzir("aplicacao") & "','" & Traduzir("btnFechar") & "');")
                                    fterminoT.MaxLength = 10
                                Case 5 'Fecha y Hora
                                    fterminoT.Attributes.Add("onkeypress", "DataHora(this,event);")
                                    fterminoT.Attributes.Add("onBlur", "validacionFechaHora(this,'" & String.Format(Traduzir("err_campo_data_invalida"), Termino.Descripcion) & "','" & String.Format(Traduzir("err_campo_hora_invalida"), Termino.Descripcion) & "','" & Traduzir("aplicacao") & "','" & Traduzir("btnFechar") & "');")
                                    fterminoT.MaxLength = 19
                            End Select

                            ActualizarEstadoCampo(fterminoT)

                        End If

                    Case 6 'Booleano
                        fterminoC = New CheckBox
                        fterminoC.ID = "ftermino_" & IdentificadorMedioPago & "_" & Termino.Identificador
                        fterminoC.SkinID = "form-checkbox"
                        fterminoC.AutoPostBack = False
                        cellItensMedioPago.Align = "center"
                        fterminoC.Checked = If(Termino.Valor = "True", True, False)
                        fterminoC.Visible = bolMostrarTerminos

                        ActualizarEstadoCampo(fterminoC)

                End Select

                If fterminoT IsNot Nothing Then
                    cellItensMedioPago.Controls.Add(fterminoT)

                ElseIf fterminoD IsNot Nothing Then
                    cellItensMedioPago.Controls.Add(fterminoD)

                Else
                    cellItensMedioPago.Controls.Add(fterminoC)
                End If

            End If

            rowItensMedioPago.Cells.Add(cellItensMedioPago)

        Next _termino

        tableItensMedioPago.Rows.Add(rowItensMedioPago)

    End Sub

    ''' <summary>
    ''' Actualizar divisas en la coleccion de divisas
    ''' </summary>
    ''' <param name="itemDivisa">objeto Divisa</param>
    ''' <remarks></remarks>
    Private Sub ActualizarDivisaModificada(ByRef itemDivisa As Clases.Divisa)

        Dim DivisaModificadas_Aux = DivisasActualizadas

        For index = 0 To DivisaModificadas_Aux.Count - 1

            If DivisaModificadas_Aux(index).Identificador = itemDivisa.Identificador Then

                DivisaModificadas_Aux.RemoveAt(index)
                'añadir nuevamente el objeto (DivisasSelecionadas) con nuevos valores de la divisa actual DivisaIAC(0)
                DivisaModificadas_Aux.Add(itemDivisa)
                Exit For

            End If

        Next index

        ' Adapatación para conversión: Al converter un IEnumerable para un List(of Type) es generado un error de conversión.
        Dim DivisaEnDivisaIAC As New ObservableCollection(Of Clases.Divisa)
        DivisaEnDivisaIAC.AddRange(DivisaModificadas_Aux.OrderBy(Function(f) f.Descripcion))

        DivisaModificadas_Aux.Clear()
        ' Adapatación para conversión: Al converter un IEnumerable para un List(of Type) es generado un error de conversión.
        DivisasActualizadas.AddRange(DivisaEnDivisaIAC.OrderBy(Function(f) f.Descripcion))

    End Sub

    ''' <summary>
    ''' Metodo para cargar la sesión con todas las divisas contenidas en el pagina, o sea, todas las divisas del cliente(IAC).
    ''' </summary>
    ''' <param name="itemDivisa">Objeto Divisa</param>
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

        rptDivisa.DataSource = DivisasActualizadas
        rptDivisa.DataBind()

        'Dim ucMedioPago As ucMedioPago = Me.NamingContainer
        'ucMedioPago.OnControleAtualizado(Nothing, Nothing)

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

        ElseIf TypeOf sender Is CheckBox Then
            CheckBox = DirectCast(sender, CheckBox)
            CheckBox.Enabled = bol

        ElseIf TypeOf sender Is GridView Then
            GridView = DirectCast(sender, GridView)
            GridView.Enabled = bol

        End If
    End Sub

    ''' <summary>
    ''' Limpiar el campo termino al desmarcar el checkbox de terminos.
    ''' </summary>
    ''' <param name="ControlLabel"></param>
    ''' <param name="ControlCampo"></param>
    ''' <remarks></remarks>
    Private Sub LimpiarTermino(ControlLabel As Label, _
                               ControlCampo As Control)

        ControlLabel.Visible = False
        ControlCampo.Visible = False

        If TypeOf ControlCampo Is TextBox Then
            DirectCast(ControlCampo, TextBox).Text = String.Empty

        ElseIf TypeOf ControlCampo Is CheckBox Then
            If DirectCast(ControlCampo, CheckBox).Checked Then
                DirectCast(ControlCampo, CheckBox).Checked = False
            Else
                DirectCast(ControlCampo, CheckBox).Checked = True
            End If

        ElseIf TypeOf ControlCampo Is DropDownList Then
            DirectCast(ControlCampo, DropDownList).DataSource = Nothing

        End If
    End Sub

    ''' <summary>
    ''' Exibir solamente el total del mediopago
    ''' </summary>
    ''' <param name="itemDivisa"></param>
    ''' <param name="ListaAgrupada"></param>
    ''' <remarks></remarks>
    Private Sub ExibirSolamenteTotal(itemDivisa As Clases.Divisa, _
                                     ByRef ListaAgrupada As List(Of List(Of Clases.MedioPago)))


        Dim ListaListaMedioPago As New List(Of List(Of Clases.MedioPago))
        If itemDivisa.ValoresTotalesTipoMedioPago IsNot Nothing AndAlso itemDivisa.ValoresTotalesTipoMedioPago.Count > 0 Then

            For Each valorTipoMedioPago In itemDivisa.ValoresTotalesTipoMedioPago
                Dim valorTipoMedioPagoLocal = valorTipoMedioPago
                Dim bolMismoTipo As Boolean = False
                For Each itemLista In ListaAgrupada
                    If itemLista.Exists(Function(f) f.Tipo = valorTipoMedioPagoLocal.TipoMedioPago) Then
                        bolMismoTipo = True
                        Exit For
                    End If
                Next itemLista

                If Not bolMismoTipo Then
                    Dim ListaMedioPago As New List(Of Clases.MedioPago)
                    Dim objMedioPago As New Clases.MedioPago
                    objMedioPago.Identificador = Guid.NewGuid.ToString
                    objMedioPago.Tipo = valorTipoMedioPago.TipoMedioPago
                    ListaMedioPago.Add(objMedioPago)
                    ListaListaMedioPago.Add(ListaMedioPago)

                End If

            Next valorTipoMedioPago

        End If
        If ListaListaMedioPago.Count > 0 Then
            ListaAgrupada.AddRange(ListaListaMedioPago)
        End If
    End Sub

#End Region

#Region "SobreCargas"

    ''' <summary>
    ''' OnError en ucDivisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overloads Sub OnError(sender As Object, e As ErroEventArgs)
        If e IsNot Nothing Then
            MyBase.NotificarErro(e.Erro)
        End If

    End Sub

#End Region

End Class


