Imports Prosegur.Genesis.ContractoServicio.Entidades
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel

Public Class UcElemento
    Inherits UcBase


#Region "[VARIABLES]"

    'Private Const _fieldsetInicio As String = "<div class='subtitulobar'><span class='subtitulobar-alternar iconesubtitulo iconesubtitulo-menor'></span><asp:Label ID='Label1' runat='server' SkinID='SubTitulo'>{0}</Label></div><div>"
    'Private Const _fieldsetFim As String = "</div>"
    Private Const _Sonido As String = "<embed src='../Sonidos/{0}' autostart='true' width='0' height='0' name='Sonido' enablejavascript='true' />"
#End Region

    Public Property Modo As Enumeradores.Modo
    Public Property esGestionBulto As Boolean
    Public Property ConfigNivelSaldo As String

    Private WithEvents _objUcListaElementos As ucListaElementos
    Private WithEvents _ucInfAdicionales As ucInfAdicionales

    Private WithEvents _UCValoresDivisasDeclarado As UCValoresDivisas
    Private WithEvents _UCValoresDivisasContado As UCValoresDivisas
    Private WithEvents _UCValoresDivisasDiferencias As UCValoresDivisas

    Private _Documento As Clases.Documento
    Public Property Documento As Clases.Documento
        Get
            Return _Documento
        End Get
        Set(value As Clases.Documento)
            _Documento = value
            ConfigurarTipoElemento()
        End Set
    End Property

    Private ReadOnly Property Elemento As Clases.Elemento
        Get
            If Documento IsNot Nothing Then
                Return Documento.Elemento
            Else
                Return Nothing
            End If
        End Get
    End Property

    Private ReadOnly Property ElementoContenedor As Contenedor
        Get
            Return DirectCast(Elemento, Contenedor)
        End Get
    End Property

    Private ReadOnly Property ElementoRemesa As Remesa
        Get
            Return DirectCast(Elemento, Remesa)
        End Get
    End Property

    Private ReadOnly Property ElementoBulto As Bulto
        Get
            Return DirectCast(Elemento, Bulto)
        End Get
    End Property

    Private ReadOnly Property ElementoParcial As Parcial
        Get
            Return DirectCast(Elemento, Parcial)
        End Get
    End Property

    Public Property TipoValor As Enumeradores.TipoValor = Enumeradores.TipoValor.Declarado

    Public Property EsPrecintoObligatorio As Boolean
    Public Property EsCodigoBolsaObligatorio As Boolean
    Public Property EsTipoServicioObligatorio As Boolean
    Public Property EsFormatoObligatorio As Boolean

    Private _EsDivisaObligatorio As Boolean
    Public Property EsDivisaObligatorio As Boolean
        Get
            Return _EsDivisaObligatorio OrElse _
                EsTotalEfectivoObligatorio OrElse _
                EsTotalChequeObligatorio OrElse _
                EsTotalTicketObligatorio OrElse _
                EsTotalTarjetaObligatorio OrElse _
                EsTotalOtrosObligatorio
        End Get
        Set(value As Boolean)
            _EsDivisaObligatorio = value
        End Set
    End Property

    Public Property EsTotalEfectivoObligatorio As Boolean
    Public Property EsTotalChequeObligatorio As Boolean
    Public Property EsTotalTicketObligatorio As Boolean
    Public Property EsTotalTarjetaObligatorio As Boolean
    Public Property EsTotalOtrosObligatorio As Boolean

    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()
    End Sub

    Private Property _tipoElemento As Enumeradores.TipoElemento
        Get
            Return ViewState("_tipoElemento")
        End Get
        Set(value As Enumeradores.TipoElemento)
            ViewState("_tipoElemento") = value
        End Set
    End Property

    Private Property _cargado As Boolean
        Get
            If ViewState("cargado") Is Nothing Then
                ViewState("cargado") = False
            End If
            Return ViewState("cargado")
        End Get
        Set(value As Boolean)
            ViewState("cargado") = value
        End Set
    End Property

    Public Editable As Boolean = True

    Protected Overrides Sub AdicionarScripts()
        If (Me.Modo <> Enumeradores.Modo.Consulta AndAlso Me.Editable) Then

            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", _
                                                 txtFechaTransporte.ClientID, "False")

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
        End If

        MyBase.AdicionarScripts()
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ConfigurarControles()
            CargarElemento()
        Catch ex As Exception
            MyBase.NotificarErro(ex)

        End Try
    End Sub

    Public Function ValidarCrearElemento(validar As Boolean) As List(Of String)
        Dim retorno As New List(Of String)

        If validar Then

            If EsPrecintoObligatorio Then
                If String.IsNullOrEmpty(txtPrecintoAgregar.Text) Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_Precinto")))
                End If
            End If

            If EsCodigoBolsaObligatorio Then
                If String.IsNullOrEmpty(txtCodigoBolsaAgregar.Text) Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_CodigoBolsa")))
                End If
            End If

            If EsTipoServicioObligatorio Then
                If String.IsNullOrEmpty(ddlTipoServicio.SelectedValue) Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TipoServicio")))
                End If
            End If

            If EsFormatoObligatorio Then
                If String.IsNullOrEmpty(ddlFormato.SelectedValue) Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_Formato")))
                End If
            End If

        End If

        If (validar AndAlso EsDivisaObligatorio) OrElse ((Not String.IsNullOrEmpty(txtTotalEfectivo.Text) AndAlso Decimal.Parse(txtTotalEfectivo.Text) <> 0) OrElse _
                                                      (Not String.IsNullOrEmpty(txtTotalCheque.Text) AndAlso Decimal.Parse(txtTotalCheque.Text) <> 0) OrElse _
                                                      (Not String.IsNullOrEmpty(txtTotalTicket.Text) AndAlso Decimal.Parse(txtTotalTicket.Text) <> 0) OrElse _
                                                      (Not String.IsNullOrEmpty(txtTotalTarjeta.Text) AndAlso Decimal.Parse(txtTotalTarjeta.Text) <> 0) OrElse _
                                                      (Not String.IsNullOrEmpty(txtTotalOtros.Text) AndAlso Decimal.Parse(txtTotalOtros.Text) <> 0)) Then

            If String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then
                retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_Divisa")))
            End If

            If EsTotalEfectivoObligatorio Then
                If String.IsNullOrEmpty(txtTotalEfectivo.Text) OrElse Decimal.Parse(txtTotalEfectivo.Text) = 0 Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TotalEfectivo")))
                End If
            End If

            If EsTotalChequeObligatorio Then
                If String.IsNullOrEmpty(txtTotalCheque.Text) OrElse Decimal.Parse(txtTotalCheque.Text) = 0 Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TotalCheque")))
                End If
            End If

            If EsTotalTicketObligatorio Then
                If String.IsNullOrEmpty(txtTotalTicket.Text) OrElse Decimal.Parse(txtTotalTicket.Text) = 0 Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TotalTicket")))
                End If
            End If

            If EsTotalTarjetaObligatorio Then
                If String.IsNullOrEmpty(txtTotalTarjeta.Text) OrElse Decimal.Parse(txtTotalTarjeta.Text) = 0 Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TotalTarjeta")))
                End If
            End If

            If EsTotalOtrosObligatorio Then
                If String.IsNullOrEmpty(txtTotalOtros.Text) OrElse Decimal.Parse(txtTotalOtros.Text) = 0 Then
                    retorno.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_Agregar_TotalOtros")))
                End If
            End If

        End If

        Return retorno
    End Function

    Private Sub ConfigurarControles()
        TraduzirControlesCrear()
        If Modo = Enumeradores.Modo.Consulta Then
            dvAgregarElemento.Visible = False
        Else
            Aplicacao.Util.Utilidad.CargarComboTipoFormato(ddlFormato, True)
            Aplicacao.Util.Utilidad.CargarComboTipoServicio(ddlTipoServicio, True)
            Select Case _tipoElemento
                Case Enumeradores.TipoElemento.Remesa
                    If Documento.Formulario.Caracteristicas.Contains(Enumeradores.CaracteristicaFormulario.GestiondeBultos) Then
                        Aplicacao.Util.Utilidad.CargarComboDivisas(ddlDivisa, False)
                    Else
                        dvCodigoBolsa.Visible = False
                        dvDivisa.Visible = False
                        dvTotalEfectivo.Visible = False
                        dvTotalCheque.Visible = False
                        dvTotalTicket.Visible = False
                        dvTotalTarjeta.Visible = False
                        dvTotalOtros.Visible = False
                    End If

                Case Enumeradores.TipoElemento.Bulto
                    dvCodigoBolsa.Visible = False
                    dvTipoServicio.Visible = False
                    dvDivisa.Visible = False
                    dvTotalEfectivo.Visible = False
                    dvTotalCheque.Visible = False
                    dvTotalTicket.Visible = False
                    dvTotalTarjeta.Visible = False
                    dvTotalOtros.Visible = False
            End Select
        End If
    End Sub

    Private Sub TraduzirControlesCrear()

        MyBase.TraduzirControles()
        lblTituloElemento.Text = Tradutor.Traduzir("026_Agregar_TituloElemento_" & _tipoElemento.ToString())
        lblTituloAgregar.Text = Tradutor.Traduzir("026_Agregar_TituloAgregar_" & _tipoElemento.ToString())
        lblPrecintoAgregar.Text = Tradutor.Traduzir("026_Agregar_Precinto")
        lblCodigoBolsaAgregar.Text = Tradutor.Traduzir("026_Agregar_CodigoBolsa")
        lblTipoServicio.Text = Tradutor.Traduzir("026_Agregar_TipoServicio")

        If _tipoElemento = Enumeradores.TipoElemento.Bulto Then
            lblFormato.Text = Tradutor.Traduzir("026_Agregar_FormatoParcial")
        Else
            lblFormato.Text = Tradutor.Traduzir("026_Agregar_Formato")
        End If

        btnAgregar.Text = Tradutor.Traduzir("026_Agregar_Agregar")
        btnAvanzado.Text = Tradutor.Traduzir("026_Agregar_Avanzado")
        lblDivisa.Text = Tradutor.Traduzir("026_Agregar_Divisa")
        lblTotalEfectivo.Text = Tradutor.Traduzir("026_Agregar_TotalEfectivo")
        lblTotalCheque.Text = Tradutor.Traduzir("026_Agregar_TotalCheque")
        lblTotalTicket.Text = Tradutor.Traduzir("026_Agregar_TotalTicket")
        lblTotalTarjeta.Text = Tradutor.Traduzir("026_Agregar_TotalTarjeta")
        lblTotalOtros.Text = Tradutor.Traduzir("026_Agregar_TotalOtros")

    End Sub

    Private Function CriarElemento(validar As Boolean, ByRef novoElemento As Clases.Elemento) As List(Of String)
        Dim retornoValidacao As New List(Of String)()

        retornoValidacao = ValidarCrearElemento(validar)
        If retornoValidacao.Count > 0 Then
            Return retornoValidacao
        End If

        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Remesa
                Dim retorno As New Clases.Bulto()
                retorno.Precintos = New ObservableCollection(Of String)() From {txtPrecintoAgregar.Text}
                retorno.CodigoBolsa = txtCodigoBolsaAgregar.Text
                If Not String.IsNullOrEmpty(ddlTipoServicio.SelectedValue) Then
                    retorno.TipoServicio = LogicaNegocio.Genesis.TipoServicio.ObtenerTipoServicioPorIdentificador(ddlTipoServicio.SelectedValue)
                End If

                If Not String.IsNullOrEmpty(ddlFormato.SelectedValue) Then
                    retorno.TipoFormato = LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlFormato.SelectedValue)
                End If

                If Not String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then
                    Dim objDivisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(New Clases.Divisa() With {.Identificador = ddlDivisa.SelectedValue})(0)
                    retorno.Divisas = New ObservableCollection(Of Clases.Divisa)() From {objDivisa}
                    If Not String.IsNullOrEmpty(txtTotalEfectivo.Text) AndAlso Decimal.Parse(txtTotalEfectivo.Text) > 0 Then
                        objDivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)() From {New Clases.ValorEfectivo() With {.Importe = txtTotalEfectivo.Text, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoDetalleEfectivo = Enumeradores.TipoDetalleEfectivo.Mezcla, .TipoValor = TipoValor}}
                    End If
                    If Not String.IsNullOrEmpty(txtTotalCheque.Text) AndAlso Decimal.Parse(txtTotalCheque.Text) > 0 Then
                        If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                            objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                        objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = txtTotalCheque.Text, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoMedioPago = Enumeradores.TipoMedioPago.Cheque, .TipoValor = TipoValor})
                    End If
                    If Not String.IsNullOrEmpty(txtTotalTicket.Text) AndAlso Decimal.Parse(txtTotalTicket.Text) > 0 Then
                        If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                            objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                        objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = txtTotalTicket.Text, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoMedioPago = Enumeradores.TipoMedioPago.Ticket, .TipoValor = TipoValor})
                    End If
                    If Not String.IsNullOrEmpty(txtTotalTarjeta.Text) AndAlso Decimal.Parse(txtTotalTarjeta.Text) > 0 Then
                        If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                            objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                        objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = txtTotalTarjeta.Text, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta, .TipoValor = TipoValor})
                    End If
                    If Not String.IsNullOrEmpty(txtTotalOtros.Text) AndAlso Decimal.Parse(txtTotalOtros.Text) > 0 Then
                        If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then
                            objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)()
                        End If
                        objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago() With {.Importe = txtTotalOtros.Text, .InformadoPor = Enumeradores.TipoContado.NoDefinido, .TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor, .TipoValor = TipoValor})
                    End If
                End If

                Select Case ConfigNivelSaldo
                    Case Comon.Constantes.CODIGO_CONFIGURACION_NIVEL_DETALLE
                        retorno.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Detalle
                    Case Comon.Constantes.CODIGO_CONFIGURACION_NIVEL_TOTAL
                        retorno.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Total
                    Case Else
                        retorno.ConfiguracionNivelSaldos = Enumeradores.ConfiguracionNivelSaldos.Ambos
                End Select

                Dim cantidad As Long
                Long.TryParse(Me.txtCantidadParciales.Text, cantidad)
                retorno.CantidadParciales = cantidad

                novoElemento = retorno

            Case Enumeradores.TipoElemento.Bulto
                Dim retorno As New Clases.Parcial()
                retorno.Precintos = New ObservableCollection(Of String)() From {txtPrecintoAgregar.Text}
                If Not String.IsNullOrEmpty(ddlFormato.SelectedValue) Then
                    retorno.TipoFormato = LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlFormato.SelectedValue)
                End If
                novoElemento = retorno
        End Select
        Return retornoValidacao
    End Function

    Public Sub CargaScriptCodigoExterno(objCodigoextero As String)

        litScriptCodigoExterno.Text = " <script> " _
        & " document.getElementById('" & objCodigoextero & "').onblur = " _
        & " function() { var CodigoExterno = document.getElementById('" & txtCodigoExterno.ClientID & "'); " _
        & " var CodigoExternoRemesa =  document.getElementById('" & objCodigoextero & "'); " _
        & " if (CodigoExterno != undefined && CodigoExternoRemesa != undefined) {CodigoExterno.value = CodigoExternoRemesa.value;} " _
        & " var campo1 = document.getElementById('" & objCodigoextero & "'); " _
        & " var div = document.getElementById('" & divCodigoExterno.ClientID & "'); " _
        & " if (campo1.value == null || campo1.value == '') { div.style.display = 'none';} else { div.style.display = 'block';}" _
        & " var hdnCodigoExterno = $('[id$=hdnCodigoExterno]')[0]; hdnCodigoExterno.value = campo1.value; }" _
        & " </script>"

    End Sub

    Private Sub CargarElemento(Optional forzarActualizacion As Boolean = False)

        If Elemento IsNot Nothing Then

            Me.Visible = True

            If forzarActualizacion OrElse Not IsPostBack OrElse Not _cargado Then

                ActualizarTitulos()
                CargarTipos()
                ConfigurarVisualizacion()
                CargarDatosElemento()
                ConfigurarHabilitacionControles()

                _cargado = True

            End If

            If Not String.IsNullOrEmpty(hdnCodigoExterno.Value) Then
                txtCodigoExterno.Text = hdnCodigoExterno.Value
                divCodigoExterno.Style.Item("display") = "block"

            End If
            CargarControles()

        End If
    End Sub

    Public Sub ActualizarDatos()
        If _cargado Then
            phControleListaElementos.Controls.Remove(_objUcListaElementos)
            _objUcListaElementos = Nothing
            CargarElemento(True)
        End If
    End Sub

    Private Sub ConfigurarTipoElemento()

        Select Case True
            Case TypeOf Elemento Is Contenedor
                _tipoElemento = Enumeradores.TipoElemento.Contenedor
            Case TypeOf Elemento Is Remesa
                _tipoElemento = Enumeradores.TipoElemento.Remesa
            Case TypeOf Elemento Is Bulto
                _tipoElemento = Enumeradores.TipoElemento.Bulto
            Case TypeOf Elemento Is Parcial
                _tipoElemento = Enumeradores.TipoElemento.Parcial
            Case Else
                Throw New NotImplementedException()
        End Select

    End Sub

    Private Sub CargarTipos()
        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Contenedor
                Aplicacao.Util.Utilidad.CargarComboTipoContenedor(ddlTipo, True)
            Case Enumeradores.TipoElemento.Bulto
                Aplicacao.Util.Utilidad.CargarComboTipoServicio(ddlTipo, True)
                Aplicacao.Util.Utilidad.CargarComboTipoFormato(ddlTipoFormato, True)
            Case Enumeradores.TipoElemento.Parcial
                Aplicacao.Util.Utilidad.CargarComboTipoFormato(ddlTipoFormato, True)
        End Select

    End Sub

    Private Sub CargarDatosElemento()

        If Elemento.FechaHoraCreacion <> DateTime.MinValue Then
            lblFechaAltaValor.Text = Elemento.FechaHoraCreacion.ToShortDateString()
        Else
            lblFechaAltaValor.Text = DateTime.Now.ToShortDateString()
        End If

        If Elemento.Precintos IsNot Nothing Then
            txtPrecinto.Text = Elemento.Precintos.FirstOrDefault()
        End If

        dvCantidadParcialesAvancado.Style.Item("display") = "none"

        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Contenedor
                If ElementoContenedor.TipoContenedor IsNot Nothing AndAlso Not IsPostBack Then
                    ddlTipo.SelectedValue = ElementoContenedor.TipoContenedor.Codigo
                End If
                txtCodContenedor.Text = ElementoContenedor.Codigo

            Case Enumeradores.TipoElemento.Remesa

                If (Modo <> Enumeradores.Modo.Consulta) Then
                    txtFechaTransporte.Enabled = Me.Editable

                Else
                    If String.IsNullOrEmpty(ElementoRemesa.Ruta) Then
                        divCodRuta.Style.Item("display") = "none"
                    Else
                        divCodRuta.Style.Item("display") = "block"
                    End If

                    If IsDBNull(ElementoRemesa.Parada) OrElse (ElementoRemesa.Parada = 0) Then
                        divNelParada.Style.Item("display") = "none"
                    Else
                        divNelParada.Style.Item("display") = "block"
                    End If

                    If ElementoRemesa.DatosATM Is Nothing OrElse String.IsNullOrEmpty(ElementoRemesa.DatosATM.CodigoCajero) Then
                        divCodigoATM.Style.Item("display") = "none"
                    Else
                        divCodigoATM.Style.Item("display") = "block"
                    End If

                    If Not IsDBNull(ElementoRemesa.CodigoExterno) Then
                        divCodigoExterno.Style.Item("display") = "block"
                    End If

                End If

                If ElementoRemesa.FechaHoraTransporte <> DateTime.MinValue Then
                    txtFechaTransporte.Text = ElementoRemesa.FechaHoraTransporte.ToShortDateString()
                Else
                    txtFechaTransporte.Text = DateTime.Now.ToShortDateString()
                End If

                txtNelParada.Text = If((Not IsDBNull(ElementoRemesa.Parada) AndAlso Not ElementoRemesa.Parada = 0), ElementoRemesa.Parada, String.Empty)
                txtCodigoATM.Text = If((ElementoRemesa.DatosATM IsNot Nothing AndAlso Not String.IsNullOrEmpty(ElementoRemesa.DatosATM.CodigoCajero)), ElementoRemesa.DatosATM.CodigoCajero, String.Empty)
                txtCodRuta.Text = ElementoRemesa.Ruta
                txtCodigoExterno.Text = If(Not String.IsNullOrEmpty(ElementoRemesa.CodigoExterno), ElementoRemesa.CodigoExterno, If(Documento IsNot Nothing, Documento.NumeroExterno, String.Empty))

                If Not String.IsNullOrEmpty(txtCodigoExterno.Text) Then
                    divCodigoExterno.Style.Item("display") = "block"
                End If

                Me.dvConfiguracionNivelDetalle.Style.Item("display") = "block"
                Me.lblConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_lblConfiguracionNivelDetalle")
                Select Case ElementoRemesa.ConfiguracionNivelSaldos
                    Case Enumeradores.ConfiguracionNivelSaldos.Detalle
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Detalle")
                    Case Enumeradores.ConfiguracionNivelSaldos.Total
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Total")
                    Case Else
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Ambos")
                End Select

            Case Enumeradores.TipoElemento.Bulto
                txtCodigoBolsa.Text = If(ElementoBulto.CodigoBolsa IsNot Nothing, ElementoBulto.CodigoBolsa, String.Empty)
                txtCodigoExterno.Text = ElementoBulto.ReciboTransporte
                txtPrecintoPadre.Text = ElementoBulto.PrecintosRemesa.FirstOrDefault()
                If ElementoBulto.TipoFormato IsNot Nothing AndAlso Not IsPostBack Then
                    ddlTipoFormato.SelectedValue = ElementoBulto.TipoFormato.Identificador
                End If
                If ElementoBulto.TipoServicio IsNot Nothing AndAlso Not IsPostBack Then
                    ddlTipo.SelectedValue = ElementoBulto.TipoServicio.Identificador
                End If

                If (Modo = Enumeradores.Modo.Consulta) Then

                    If String.IsNullOrEmpty(ElementoBulto.CodigoBolsa) Then
                        divCodigoBolsa.Style.Item("display") = "none"
                    Else
                        divCodigoBolsa.Style.Item("display") = "block"
                    End If

                End If

                Me.dvCantidadParcialesAvancado.Style.Item("display") = "block"
                Me.lblCantidadParcialesAvancado.Text = Tradutor.Traduzir("059_quantidade_parcial")
                Me.txtCantidadParcialesAvancado.Text = ElementoBulto.CantidadParciales

                Me.dvConfiguracionNivelDetalle.Style.Item("display") = "block"
                Me.lblConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_lblConfiguracionNivelDetalle")
                Select Case ElementoBulto.ConfiguracionNivelSaldos
                    Case Enumeradores.ConfiguracionNivelSaldos.Detalle
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Detalle")
                    Case Enumeradores.ConfiguracionNivelSaldos.Total
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Total")
                    Case Else
                        Me.txtConfiguracionNivelDetalle.Text = Tradutor.Traduzir("059_ConfiguracionNivel_Ambos")
                End Select

            Case Enumeradores.TipoElemento.Parcial

                If ElementoParcial.TipoFormato IsNot Nothing AndAlso Not IsPostBack Then
                    ddlTipoFormato.SelectedValue = ElementoParcial.TipoFormato.Identificador
                End If

        End Select

    End Sub

    Private Sub CargarControles()

        CargarListaElementos()
        CargarTerminos()
        CargarDivisas()

    End Sub

    Private Sub CargarListaElementos()

        If _tipoElemento <> Enumeradores.TipoElemento.Parcial Then

            _objUcListaElementos = LoadControl("~/Controles/ucListaElementos.ascx")
            _objUcListaElementos.ID = "UcListaElementos_" & Elemento.Identificador.Replace("-", String.Empty)
            _objUcListaElementos.Modo = Modo
            _objUcListaElementos.esGestionBulto = esGestionBulto
            _objUcListaElementos.TipoElemento = _tipoElemento
            _objUcListaElementos.TipoValor = TipoValor

            AddHandler _objUcListaElementos.Erro, AddressOf _ucListaElementos_Error

            divCantidadParciales.Visible = False

            Select Case _tipoElemento

                Case Enumeradores.TipoElemento.Contenedor
                    'litFieldSetInicio.Text = String.Format(_fieldsetInicio, Tradutor.Traduzir("026_ListaRemesas"))
                    _objUcListaElementos.mensajeVacio = Tradutor.Traduzir("026_ListaRemesasVacio")
                    _objUcListaElementos.Elementos = DirectCast(Elemento, Clases.Contenedor).Elementos
                    'litFieldSetFim.Text = _fieldsetFim

                Case Enumeradores.TipoElemento.Remesa

                    EsPrecintoObligatorio = True
                    EsTipoServicioObligatorio = True
                    EsFormatoObligatorio = True
                    _objUcListaElementos.mensajeVacio = Tradutor.Traduzir("026_ListaBultoVacio")
                    If DirectCast(Elemento, Clases.Remesa).Bultos IsNot Nothing Then
                        _objUcListaElementos.Elementos = DirectCast(Elemento, Clases.Remesa).Bultos
                    End If

                    _objUcListaElementos.ConfiguracionNivelSaldosElementroPadre = DirectCast(Elemento, Clases.Remesa).ConfiguracionNivelSaldos

                    divCantidadParciales.Visible = True
                    Me.lblCantidadParciales.Text = Tradutor.Traduzir("059_quantidade_parcial")

                Case Enumeradores.TipoElemento.Bulto
                    Dim objBulto = DirectCast(Elemento, Clases.Bulto)
                    EsPrecintoObligatorio = True
                    EsFormatoObligatorio = True

                    _objUcListaElementos.mensajeVacio = Tradutor.Traduzir("026_ListaParcialesVacio")
                    If objBulto.Parciales IsNot Nothing Then
                        _objUcListaElementos.Elementos = objBulto.Parciales
                    End If

                    _objUcListaElementos.ConfiguracionNivelSaldosElementroPadre = DirectCast(Elemento, Clases.Bulto).ConfiguracionNivelSaldos

            End Select

            phControleListaElementos.Controls.Add(_objUcListaElementos)
            _objUcListaElementos.ActualizaGrid()
            divListaElementos.Visible = True
        Else
            divListaElementos.Visible = False
        End If

    End Sub

    Private Sub CargarTerminos()

        If Elemento.GrupoTerminosIAC IsNot Nothing AndAlso
            Elemento.GrupoTerminosIAC.TerminosIAC IsNot Nothing Then

            If _ucInfAdicionales Is Nothing Then
                _ucInfAdicionales = LoadControl("~/Controles/ucInfAdicionales.ascx")
                _ucInfAdicionales.ID = "ucInfAdicionales_" & Elemento.Identificador.Replace("-", String.Empty)
                _ucInfAdicionales.Modo = Modo
                _ucInfAdicionales.Terminos = Elemento.GrupoTerminosIAC.TerminosIAC.AsEnumerable
                phControleTermino.Controls.Add(_ucInfAdicionales)
                Debug.WriteLine("Load ucInfAdicionales")
            End If

        End If

    End Sub

    Private Sub CargarDivisas()

        Dim DivisasAmbos As ObservableCollection(Of Clases.Divisa) = Nothing

        DivisasAmbos = Elemento.Divisas.Clonar()

        ' ############## Declarados ##############
        If ((DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0)) OrElse Modo <> Enumeradores.Modo.Consulta Then
            ' remove todos os items que não tenham o tipo valor informado no parâmetro filtro
            Comon.Util.BorrarItemsDivisasDiferentesTipoValor(DivisasAmbos, Enumeradores.TipoValor.Declarado)
            If _UCValoresDivisasDeclarado Is Nothing Then
                _UCValoresDivisasDeclarado = LoadControl("~/Controles/UCValoresDivisas.ascx")
                _UCValoresDivisasDeclarado.ID = "UCValoresDivisasDeclarado_" & Elemento.Identificador.Replace("-", String.Empty)
                _UCValoresDivisasDeclarado.Modo = Modo
                _UCValoresDivisasDeclarado.Divisas = DivisasAmbos
                _UCValoresDivisasDeclarado.TipoValor = Enumeradores.TipoValor.Declarado
                _UCValoresDivisasDeclarado.TrabajarConUnidadMedida = False
                _UCValoresDivisasDeclarado.TrabajarConCalidad = False
                phControleEfectivoDeclarado.Controls.Add(_UCValoresDivisasDeclarado)
            End If
        End If

        divValoresDeclarados.Visible = (DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0) OrElse
                                        (Modo <> Enumeradores.Modo.Consulta)

        ' ############## Declarados ##############

        If Documento.Formulario.Caracteristicas.Where(Function(x) x = Enumeradores.CaracteristicaFormulario.Actas).Count > 0 OrElse _
            Documento.Formulario.Caracteristicas.Where(Function(x) x = Enumeradores.CaracteristicaFormulario.Sustitucion).Count > 0 Then
            DivisasAmbos = Elemento.Divisas.Clonar()

            ' ############## Contados ##############
            If ((DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0)) OrElse Modo <> Enumeradores.Modo.Consulta Then
                ' remove todos os items que não tenham o tipo valor informado no parâmetro filtro
                Comon.Util.BorrarItemsDivisasDiferentesTipoValor(DivisasAmbos, Enumeradores.TipoValor.Contado)
                If _UCValoresDivisasContado Is Nothing Then
                    _UCValoresDivisasContado = LoadControl("~/Controles/UCValoresDivisas.ascx")
                    _UCValoresDivisasContado.ID = "UCValoresDivisasContado_" & Elemento.Identificador.Replace("-", String.Empty)
                    _UCValoresDivisasContado.Modo = Modo
                    _UCValoresDivisasContado.Divisas = DivisasAmbos
                    _UCValoresDivisasContado.TipoValor = Enumeradores.TipoValor.Contado
                    _UCValoresDivisasContado.TrabajarConUnidadMedida = True
                    _UCValoresDivisasContado.TrabajarConCalidad = True
                    _UCValoresDivisasContado.ExhibirTotalesDivisa = False
                    phControleEfectivoContado.Controls.Add(_UCValoresDivisasContado)
                End If
            End If

            divValoresContados.Visible = (DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0) OrElse
                                        (Modo <> Enumeradores.Modo.Consulta)

            ' ############## Contados ##############

            ' ############## Diferencias ##############
            DivisasAmbos = Elemento.Divisas.Clonar()

            If ((DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0)) OrElse Modo <> Enumeradores.Modo.Consulta Then
                ' remove todos os items que não tenham o tipo valor informado no parâmetro filtro
                Comon.Util.BorrarItemsDivisasDiferentesTipoValor(DivisasAmbos, Enumeradores.TipoValor.Diferencia)
                If _UCValoresDivisasDiferencias Is Nothing Then
                    _UCValoresDivisasDiferencias = LoadControl("~/Controles/UCValoresDivisas.ascx")
                    _UCValoresDivisasDiferencias.ID = "UCValoresDivisasDiferencias_" & Elemento.Identificador.Replace("-", String.Empty)
                    _UCValoresDivisasDiferencias.Modo = Modo
                    _UCValoresDivisasDiferencias.Divisas = DivisasAmbos
                    _UCValoresDivisasDiferencias.TipoValor = Enumeradores.TipoValor.Diferencia
                    _UCValoresDivisasDiferencias.TrabajarConUnidadMedida = False
                    _UCValoresDivisasDiferencias.TrabajarConCalidad = False
                    phControleEfectivoDiferencias.Controls.Add(_UCValoresDivisasDiferencias)
                End If
            End If

            ' ############## Diferencias ##############

            divDiferencias.Visible = (DivisasAmbos IsNot Nothing AndAlso DivisasAmbos.Count > 0) OrElse
                                        (Modo <> Enumeradores.Modo.Consulta)

        End If

    End Sub

    Private Sub ConfigurarVisualizacion()

        divValoresDeclarados.Visible = True

        If Documento.Formulario.Caracteristicas.Where(Function(x) x = Enumeradores.CaracteristicaFormulario.Actas).Count > 0 OrElse _
            Documento.Formulario.Caracteristicas.Where(Function(x) x = Enumeradores.CaracteristicaFormulario.Sustitucion).Count > 0 Then

            divValoresContados.Visible = True
            divDiferencias.Visible = True

        End If

        Select Case _tipoElemento

            Case Enumeradores.TipoElemento.Contenedor

            Case Enumeradores.TipoElemento.Remesa
                divPrecinto.Style.Item("display") = "none"
                divTipo.Style.Item("display") = "none"
                divTipoFormato.Style.Item("display") = "none"
                divFechaAlta.Style.Item("display") = "block"
                divFechaTransporte.Style.Item("display") = "block"
                divCodRuta.Style.Item("display") = "block"
                divCodigoBolsa.Style.Item("display") = "none"
                divNelParada.Style.Item("display") = "block"
                divCodigoATM.Style.Item("display") = "block"
                'divCodigoExterno.Style.Item("display") = "none"
                txtNelParada.Attributes.Add("onkeypress", "return bloquearletras(event,this);")
                txtNelParada.Attributes.Add("onpaste", "return false;")
                txtNelParada.Attributes.Add("onkeydown", "BloquearColar();")

            Case Enumeradores.TipoElemento.Bulto
                divPrecinto.Style.Item("display") = "block"
                divTipo.Style.Item("display") = "block"
                divTipoFormato.Style.Item("display") = "block"
                divFechaAlta.Style.Item("display") = "block"
                divFechaTransporte.Style.Item("display") = "none"
                divCodigoExterno.Style.Item("display") = "none"
                divCodigoExterno.Visible = False
                divCodRuta.Style.Item("display") = "none"
                divCodigoBolsa.Style.Item("display") = "block"
                divNelParada.Style.Item("display") = "none"

            Case Enumeradores.TipoElemento.Parcial
                divPrecinto.Style.Item("display") = "block"
                divTipo.Style.Item("display") = "none"
                divTipoFormato.Style.Item("display") = "block"
                divFechaAlta.Style.Item("display") = "block"
                divFechaTransporte.Style.Item("display") = "none"
                divCodigoExterno.Style.Item("display") = "none"
                divCodigoExterno.Visible = False
                divCodRuta.Style.Item("display") = "none"
                divCodigoBolsa.Style.Item("display") = "none"
                divNelParada.Style.Item("display") = "none"

        End Select

    End Sub

    Private Sub ConfigurarHabilitacionControles()

        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Contenedor

            Case Enumeradores.TipoElemento.Remesa
                'ActualizarEstadoCampo(txtPrecinto)
                ActualizarEstadoCampo(txtCodRuta)
                ActualizarEstadoCampo(txtNelParada)
                ActualizarEstadoCampo(txtCodigoATM)
                ActualizarEstadoCampo(Me.txtCantidadParciales)

            Case Enumeradores.TipoElemento.Bulto
                ActualizarEstadoCampo(txtPrecinto)
                ActualizarEstadoCampo(ddlTipoFormato)
                ActualizarEstadoCampo(ddlTipo)
                ActualizarEstadoCampo(txtCodigoBolsa)
                ActualizarEstadoCampo(Me.txtCantidadParcialesAvancado)

            Case Enumeradores.TipoElemento.Parcial
                ActualizarEstadoCampo(txtPrecinto)
                ActualizarEstadoCampo(ddlTipoFormato)

        End Select

    End Sub

    Private Sub ActualizarEstadoCampo(sender As Object)
        Select Case Modo
            Case Enumeradores.Modo.Alta, Enumeradores.Modo.Modificacion
                ActivarDesactivar(sender, True)

            Case Enumeradores.Modo.Consulta
                ActivarDesactivar(sender, False)

        End Select
    End Sub

    Private Sub ActivarDesactivar(sender As Object, _
                                 bol As Boolean)

        Dim TextBox As TextBox = Nothing
        Dim DropDownList As DropDownList = Nothing

        If TypeOf sender Is TextBox Then
            TextBox = DirectCast(sender, TextBox)
            TextBox.Enabled = bol

        ElseIf TypeOf sender Is DropDownList Then
            DropDownList = DirectCast(sender, DropDownList)
            DropDownList.Enabled = bol

        End If
    End Sub

    Private Sub ActualizarTitulos()

        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Contenedor
                lblTitulo.Text = Tradutor.Traduzir("026_contenedor")
                lblTipo.Text = Tradutor.Traduzir("026_tipo_contenedor")

            Case Enumeradores.TipoElemento.Remesa
                lblTitulo.Text = Tradutor.Traduzir("026_remesa")
                lblFechaAlta.Text = Tradutor.Traduzir("026_fecha_alta")
                lblFechaTransporte.Text = Tradutor.Traduzir("026_fecha_transporte")
                lblCodRuta.Text = Tradutor.Traduzir("026_cod_ruta")
                lblCodigoExterno.Text = Tradutor.Traduzir("026_num_recibo_transporte")
                lblNelParada.Text = Tradutor.Traduzir("026_nel_parada")
                lblCodigoATM.Text = Tradutor.Traduzir("026_codigo_atm")

            Case Enumeradores.TipoElemento.Bulto
                lblTitulo.Text = Tradutor.Traduzir("026_bulto")
                lblPrecinto.Text = Tradutor.Traduzir("026_precinto")
                lblTipo.Text = Tradutor.Traduzir("026_tipo_remesa")
                lblTipoFormato.Text = Tradutor.Traduzir("026_formato_bulto")
                lblFechaAlta.Text = Tradutor.Traduzir("026_fecha_alta")
                lblCodigoBolsa.Text = Tradutor.Traduzir("026_cod_bolsa")

            Case Enumeradores.TipoElemento.Parcial
                lblTitulo.Text = Tradutor.Traduzir("026_parcial")
                lblPrecinto.Text = Tradutor.Traduzir("026_precinto")
                lblTipoFormato.Text = Tradutor.Traduzir("026_formato_parcial")
                lblFechaAlta.Text = Tradutor.Traduzir("026_fecha_alta")

        End Select

        'Titulo = Tradutor.Traduzir("026_detalhes") & " " & lblTitulo.Text

    End Sub

    Public Sub GuardarDatos()
        Try
            GuardarDatosElemento()
            GuardarDatosControles()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub GuardarDatosControles()

        If _ucInfAdicionales IsNot Nothing Then
            _ucInfAdicionales.GuardarDatos()
        End If

        ' Recupera Efectivos
        Dim DivisasEfectivo As ObservableCollection(Of Clases.Divisa) = Nothing
        ActualizaEfectivo(_UCValoresDivisasDeclarado, Enumeradores.TipoValor.Declarado, DivisasEfectivo)
        ActualizaEfectivo(_UCValoresDivisasContado, Enumeradores.TipoValor.Contado, DivisasEfectivo)
        ActualizaEfectivo(_UCValoresDivisasDiferencias, Enumeradores.TipoValor.Diferencia, DivisasEfectivo)

        ' recupera MedioPago
        Dim DivisasMedioPago As ObservableCollection(Of Clases.Divisa) = Nothing
        ActualizaMedioPago(_UCValoresDivisasDeclarado, Enumeradores.TipoValor.Declarado, DivisasMedioPago)
        ActualizaMedioPago(_UCValoresDivisasContado, Enumeradores.TipoValor.Contado, DivisasMedioPago)
        ActualizaMedioPago(_UCValoresDivisasDiferencias, Enumeradores.TipoValor.Diferencia, DivisasMedioPago)

        Elemento.Divisas = DivisasMedioPago
        'Aplicacao.Util.Utilidad.AgregarDivisas(Elemento.Divisas, DivisasEfectivo, DivisasMedioPago)
        Aplicacao.Util.Utilidad.EliminarDatosSinValores(Elemento.Divisas)
        OnControleAtualizado(New ControleEventArgs With {.Controle = "Elemento"})
    End Sub

    Private Sub ActualizaEfectivo(ByRef objEfectivo As UCValoresDivisas, objTipoValor As Enumeradores.TipoValor, ByRef objDivisasEfectivo As ObservableCollection(Of Clases.Divisa))

        If objEfectivo IsNot Nothing Then
            objEfectivo.ValidarDivisas()
            If objDivisasEfectivo Is Nothing Then
                objDivisasEfectivo = objEfectivo.Divisas
            Else
                For Each div In objEfectivo.Divisas
                    Dim divLocal = div
                    Dim objdivisa = objDivisasEfectivo.FindLast(Function(x) x.Identificador = divLocal.Identificador)
                    If objdivisa Is Nothing Then
                        objDivisasEfectivo.Add(div)
                    Else
                        If div.ValoresTotalesEfectivo IsNot Nothing Then
                            For Each valores In div.ValoresTotalesEfectivo
                                Dim valoresLocal = valores
                                If valores.TipoValor = objTipoValor Then
                                    Dim vNuevo As Clases.ValorEfectivo = Nothing
                                    If objdivisa.ValoresTotalesEfectivo Is Nothing Then
                                        objdivisa.ValoresTotalesEfectivo = New ObservableCollection(Of ValorEfectivo)
                                    Else
                                        vNuevo = objdivisa.ValoresTotalesEfectivo.FindLast(Function(x) x.TipoValor = valoresLocal.TipoValor)
                                    End If

                                    If vNuevo Is Nothing Then
                                        objdivisa.ValoresTotalesEfectivo.Remove(objdivisa.ValoresTotalesEfectivo.FindLast(Function(x) x.TipoValor = objTipoValor))
                                        objdivisa.ValoresTotalesEfectivo.Add(valores)
                                    End If
                                End If
                            Next
                        End If
                        For Each den In div.Denominaciones
                            Dim denLocal = den
                            Dim objdenominacion As Clases.Denominacion = Nothing
                            If objdivisa.Denominaciones Is Nothing Then
                                objdivisa.Denominaciones = New ObservableCollection(Of Denominacion)
                            Else
                                objdenominacion = objdivisa.Denominaciones.FindLast(Function(x) x.Identificador = denLocal.Identificador)
                            End If

                            If objdenominacion Is Nothing Then
                                objdivisa.Denominaciones.Add(den)
                            ElseIf den.ValorDenominacion IsNot Nothing Then
                                Dim ValorNuevo = den.ValorDenominacion.FindLast(Function(x) x.TipoValor = objTipoValor)
                                If ValorNuevo IsNot Nothing Then
                                    objdenominacion.ValorDenominacion.Remove(objdenominacion.ValorDenominacion.FindLast(Function(x) x.TipoValor = objTipoValor))
                                    objdenominacion.ValorDenominacion.Add(ValorNuevo)
                                End If
                            End If
                        Next

                    End If
                Next
            End If
        End If
    End Sub

    Private Sub ActualizaMedioPago(ByRef objMedioPago As UCValoresDivisas, objTipoValor As Enumeradores.TipoValor, ByRef objDivisasMedioPago As ObservableCollection(Of Clases.Divisa))

        If objMedioPago IsNot Nothing Then
            'objMedioPago.GuardarDatos(True)
            If objDivisasMedioPago Is Nothing Then
                objDivisasMedioPago = objMedioPago.Divisas
            Else
                For Each div In objMedioPago.Divisas
                    Dim divLocal = div
                    Dim objdivisa = objDivisasMedioPago.FindLast(Function(x) x.Identificador = divLocal.Identificador)
                    If objdivisa Is Nothing Then
                        objDivisasMedioPago.Add(div)
                    Else

                        Dim TiposMediosPago() As String = {Enumeradores.TipoMedioPago.Cheque.ToString, Enumeradores.TipoMedioPago.OtroValor.ToString,
                                                           Enumeradores.TipoMedioPago.Tarjeta.ToString, Enumeradores.TipoMedioPago.Ticket.ToString}

                        If div.ValoresTotalesTipoMedioPago IsNot Nothing Then
                            For Each valores In div.ValoresTotalesTipoMedioPago
                                Dim valoresLocal = valores
                                For Each enumerador In TiposMediosPago
                                    If valores.TipoValor = objTipoValor AndAlso valores.TipoMedioPago.ToString = enumerador Then
                                        Dim vNuevo As Clases.ValorTipoMedioPago = Nothing
                                        If objdivisa.ValoresTotalesTipoMedioPago IsNot Nothing Then
                                            vNuevo = objdivisa.ValoresTotalesTipoMedioPago.FindLast(Function(x) x.TipoValor = valoresLocal.TipoValor AndAlso x.TipoMedioPago = valoresLocal.TipoMedioPago)
                                        Else
                                            objdivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of ValorTipoMedioPago)
                                        End If

                                        If vNuevo Is Nothing Then
                                            objdivisa.ValoresTotalesTipoMedioPago.Remove(objdivisa.ValoresTotalesTipoMedioPago.FindLast(Function(x) x.TipoValor = objTipoValor AndAlso x.TipoMedioPago = valoresLocal.TipoMedioPago))
                                            objdivisa.ValoresTotalesTipoMedioPago.Add(valores)
                                        End If
                                    End If
                                Next enumerador
                            Next valores
                        End If

                        Dim MediosPagos As New ObservableCollection(Of Clases.MedioPago)
                        For Each mp In div.MediosPago
                            Dim mpLocal = mp
                            If objdivisa.MediosPago IsNot Nothing Then
                                Dim objMP = objdivisa.MediosPago.FindLast(Function(x) x.Identificador = mpLocal.Identificador)
                                If objMP Is Nothing Then
                                    objdivisa.MediosPago.Add(mp)
                                ElseIf mp.Valores IsNot Nothing Then
                                    Dim ValorNuevo = mp.Valores.FindLast(Function(x) x.TipoValor = objTipoValor)
                                    If ValorNuevo IsNot Nothing Then
                                        objMP.Valores.Remove(objMP.Valores.FindLast(Function(x) x.TipoValor = objTipoValor))
                                        objMP.Valores.Add(ValorNuevo)
                                    End If
                                End If
                            End If
                        Next mp
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub GuardarDatosElemento()

        Elemento.Precintos = New ObservableCollection(Of String) From {txtPrecinto.Text}

        ' TODO: IMPLEMENTAR VALIDAÇÃO PREENCHIMENTO CAMPO.

        ' O código abaixo foi alterado para 'mascarar' o erro gerado devido o não preenchimento dos campos no controle.
        ' O código deverá ser corrigido posteriormente para validar o preenchimento dos campos. 
        ' Cristiano Fontes ciente.

        Select Case _tipoElemento
            Case Enumeradores.TipoElemento.Remesa
                ElementoRemesa.CodigoExterno = txtCodigoExterno.Text
                ElementoRemesa.FechaHoraTransporte = txtFechaTransporte.Text
                ElementoRemesa.Ruta = txtCodRuta.Text
                ElementoRemesa.Parada = If(Not String.IsNullOrEmpty(txtNelParada.Text), txtNelParada.Text, Nothing)
                If (Not String.IsNullOrEmpty(txtCodigoATM.Text)) Then
                    ElementoRemesa.DatosATM = New ATM With {.CodigoCajero = txtCodigoATM.Text}
                End If

            Case Enumeradores.TipoElemento.Bulto

                ElementoBulto.ReciboTransporte = txtCodigoExterno.Text
                ElementoBulto.TipoFormato = Prosegur.Genesis.LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlTipoFormato.SelectedValue)
                ElementoBulto.TipoServicio = Prosegur.Genesis.LogicaNegocio.Genesis.TipoServicio.ObtenerTipoServicioPorIdentificador(ddlTipo.SelectedValue)
                ElementoBulto.CodigoBolsa = txtCodigoBolsa.Text
                Dim cantidad As Long
                Long.TryParse(Me.txtCantidadParcialesAvancado.Text, cantidad)
                ElementoBulto.CantidadParciales = cantidad

            Case Enumeradores.TipoElemento.Contenedor
                ElementoContenedor.Codigo = txtCodContenedor.Text
                ElementoContenedor.TipoContenedor = New TipoContenedor() With {.Identificador = ddlTipo.SelectedValue}
            Case Enumeradores.TipoElemento.Parcial
                ElementoParcial.TipoFormato = Prosegur.Genesis.LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorIdentificador(ddlTipoFormato.SelectedValue)
        End Select

    End Sub

    'Public Overrides Sub ConfigurarTabIndexControle()
    '    MyBase.ConfigurarTabIndexControle()

    '    Aplicacao.Util.Utilidad.ConfigurarTabIndex(txtPrecinto, ddlTipo, ddlTipoFormato, lblFechaAltaValor, txtCodigoExterno, txtPrecinto, ddlTipo, _
    '                                               ddlTipoFormato, lblFechaAltaValor, txtFechaTransporte, txtCodContenedor, txtCodRuta, _
    '                                                txtPrecintoPadre, txtCodigoBolsa, txtNelParada, _UCValoresDivisasDeclarado, _
    '                                               _UCValoresDivisasContado, _UCValoresDivisasDiferencias, _ucInfAdicionales)
    'End Sub

    Public Overrides Function ValidarControl() As System.Collections.Generic.List(Of String)
        Dim Mensajes As New List(Of String)
        Select Case _tipoElemento

            Case Enumeradores.TipoElemento.Contenedor

            Case Enumeradores.TipoElemento.Remesa
                'If String.IsNullOrEmpty(txtPrecinto.Text) Then
                '    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_precinto")))
                'End If
                If ElementoRemesa.Bultos Is Nothing OrElse ElementoRemesa.Bultos.Count <= 0 Then
                    Mensajes.Add(Tradutor.Traduzir("028_bulto_obrigatorio"))
                End If
            Case Enumeradores.TipoElemento.Bulto
                If String.IsNullOrEmpty(txtPrecinto.Text) Then
                    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_precinto")))
                End If
                'If String.IsNullOrEmpty(txtCodRuta.Text) Then
                '    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_cod_ruta")))
                'End If
                'If String.IsNullOrEmpty(txtCantElementos.Text) Then
                '    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_cant_elementos_contenedor")))
                'End If
                If String.IsNullOrEmpty(ddlTipo.SelectedValue) Then
                    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_tipo_remesa")))
                End If
                If String.IsNullOrEmpty(ddlTipoFormato.SelectedValue) Then
                    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_formato_bulto")))
                End If
            Case Enumeradores.TipoElemento.Parcial

                If String.IsNullOrEmpty(ddlTipoFormato.SelectedValue) Then
                    Mensajes.Add(String.Format(Tradutor.Traduzir("msg_campo_obrigatorio"), Tradutor.Traduzir("026_formato_parcial")))
                End If
        End Select
        Return Mensajes
    End Function

    Private Sub _ucListaElementos_ControleAtualizado(sender As Object, e As System.EventArgs) Handles _objUcListaElementos.ControleAtualizado

    End Sub

    Private Sub _ucInfAdicionales_ControleAtualizado(sender As Object, e As System.EventArgs) Handles _ucInfAdicionales.ControleAtualizado

    End Sub

    Private Sub _UCValoresDivisasContado_Erro(sender As Object, e As ErroEventArgs) Handles _UCValoresDivisasContado.Erro
        MyBase.NotificarErro(e.Erro)
    End Sub

    Private Sub _UCValoresDivisasDeclarado_Erro(sender As Object, e As ErroEventArgs) Handles _UCValoresDivisasDeclarado.Erro
        MyBase.NotificarErro(e.Erro)
    End Sub

    Private Sub _UCValoresDivisasDiferencias_Erro(sender As Object, e As ErroEventArgs) Handles _UCValoresDivisasDiferencias.Erro
        MyBase.NotificarErro(e.Erro)
    End Sub

    Private Sub _ucListaElementos_Error(sender As Object, e As ErroEventArgs)
        If e IsNot Nothing Then
            MyBase.NotificarErro(e.Erro)
        End If
    End Sub

    Public Class ElementoSeleccionadosElementoEventArgs
        Public Property Elemento As ObservableCollection(Of Clases.Elemento)
    End Class

    Public Event SeleccionarElemento(sender As Object, e As ElementoSeleccionadosElementoEventArgs)

    Public Event DetallarElemento(sender As Object, e As ucListaElementos.DetallarElementoEventArgs)

    Private Sub _ucListaElementos_DetallarElemento(sender As Object, e As ucListaElementos.DetallarElementoEventArgs) Handles _objUcListaElementos.DetallarElemento
        RaiseEvent DetallarElemento(sender, e)
    End Sub

    Private Sub txtCodRuta_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodRuta.TextChanged
        ElementoRemesa.Ruta = txtCodRuta.Text
    End Sub

    Private Sub txtNelParada_TextChanged(sender As Object, e As System.EventArgs) Handles txtNelParada.TextChanged
        ElementoRemesa.Parada = If(Not String.IsNullOrEmpty(txtNelParada.Text), txtNelParada.Text, Nothing)
    End Sub

    Private Sub txtCodigoATM_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoATM.TextChanged
        If (Not String.IsNullOrEmpty(txtCodigoATM.Text)) Then
            ElementoRemesa.DatosATM = New ATM With {.CodigoCajero = txtCodigoATM.Text}
        End If
    End Sub

    Private Sub txtCodigoBolsa_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoBolsa.TextChanged
        ElementoBulto.CodigoBolsa = txtCodigoBolsa.Text
    End Sub

    'Public Sub DesabilitarCampo(IdControle As String, objCodigoextero As String, Inactivo As Boolean)
    '    If Inactivo Then

    '        litScriptCodigoExterno.Text = "<script> " _
    '                        & "window.onload = function() { " _
    '                        & "var txtNumeroExterno = document.getElementById('" & objCodigoextero & "');" _
    '                        & " txtNumeroExterno.disabled = false;}" _
    '                        & "</script>"

    '    Else
    '        litScriptCodigoExterno.Text = "<script> " _
    '                         & "window.load = function() { " _
    '                         & "var txtNumeroExterno = document.getElementById('" & objCodigoextero & "');" _
    '                         & " txtNumeroExterno.disabled = true;}" _
    '                         & "</script>"
    '    End If

    'End Sub

    Private Sub btnAgregar_Click(sender As Object, e As System.EventArgs) Handles btnAgregar.Click
        Dim novoElemento As New Clases.Elemento()
        Dim resultadoValidacao As List(Of String) = CriarElemento(True, novoElemento)
        If resultadoValidacao.Count > 0 Then
            OnErro(New ErroEventArgs(New Excepcion.CampoObrigatorioException(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))))
        Else
            RaiseEvent CrearElemento(Me, New CrearElementoEventArgs(novoElemento, AcaoAgregarEnum.Agregar))
            LimparCamposAgregar()
        End If
    End Sub

    Private Sub btnAvanzado_Click(sender As Object, e As System.EventArgs) Handles btnAvanzado.Click
        Dim novoElemento As New Clases.Elemento()
        Dim resultadoValidacao As List(Of String) = CriarElemento(False, novoElemento)
        If resultadoValidacao.Count > 0 Then
            OnErro(New ErroEventArgs(New Excepcion.CampoObrigatorioException(String.Join(Environment.NewLine, resultadoValidacao.ToArray()))))
        Else
            RaiseEvent CrearElemento(Me, New CrearElementoEventArgs(novoElemento, AcaoAgregarEnum.Avanzado))
            LimparCamposAgregar()
        End If
    End Sub

    Private Sub LimparCamposAgregar()
        txtPrecintoAgregar.Text = String.Empty
        txtCodigoBolsaAgregar.Text = String.Empty
        Me.txtCantidadParciales.Text = String.Empty
        If ddlTipoServicio.Items.Count > 0 Then
            ddlTipoServicio.SelectedIndex = 0
        End If
        If ddlFormato.Items.Count > 0 Then
            ddlFormato.SelectedIndex = 0
        End If
        If ddlDivisa.Items.Count > 0 Then
            ddlDivisa.SelectedIndex = 0
        End If

        txtTotalEfectivo.Text = String.Empty
        txtTotalCheque.Text = String.Empty
        txtTotalTicket.Text = String.Empty
        txtTotalTarjeta.Text = String.Empty
        txtTotalOtros.Text = String.Empty
    End Sub

    Public Enum AcaoAgregarEnum
        Agregar
        Avanzado
    End Enum

    Public Class CrearElementoEventArgs
        Inherits System.EventArgs
        Public Property Elemento As Clases.Elemento
        Public Sub New(elemento As Clases.Elemento, acaoAgregar As AcaoAgregarEnum)
            Me.Elemento = elemento
            Me.AcaoAgregar = acaoAgregar
        End Sub
        Public AcaoAgregar As AcaoAgregarEnum
    End Class

    Public Event CrearElemento(sender As Object, e As CrearElementoEventArgs)

End Class
