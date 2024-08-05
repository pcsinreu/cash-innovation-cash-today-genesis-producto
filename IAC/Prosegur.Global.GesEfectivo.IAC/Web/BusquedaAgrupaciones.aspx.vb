Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Agrupações 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 09/02/09 - Created</history>
Partial Public Class BusquedaAgrupaciones
    Inherits Base

#Region "[CONSTANTES]"

    Const TreeViewNodeEfectivo As String = "008_lbl_efectivo"

#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona o java script
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigoAgrupacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricaoAgrupacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlCheques.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlDivisaCheques.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlDivisaEfectivo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlDivisaOtrosValores.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlDivisaTicket.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlOtrosValores.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTicket.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        txtCodigoAgrupacionForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoAgrupacion.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoAgrupacionForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")


    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' [pda] 10/02/2009 Criado
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Definição de parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES
        ' desativar validação de ação
        MyBase.ValidarAcao = False

    End Sub

    ''' <summary>
    ''' Metodo inicial e chamado quando a pagina e inicializada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                'Preenche Combos
                PreencherComboDivisa()

                PreencherComboDivisaTicket()
                PreencherComboTicket()


                PreencherComboDivisaOtros()
                PreencherComboOtros()

                PreencherComboDivisaCheques()
                PreencherComboCheques()

                'Seta o foco no campo código
                txtCodigoAgrupacion.Focus()

                ExecutarBusca()
                UpdatePanelGrid.Update()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False


                'Formulario
                CarregaTreeViewDividasPossiveis()
            End If

            ' TrataFoco()

        Catch ex As Exception

            Throw New InicializarException(ex.ToString)

        End Try

    End Sub

    ''' <summary>
    ''' Metodo pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            TrvDivisas.Attributes.Add("style", "margin:0px !Important;")
            TrvAgrupaciones.Attributes.Add("style", "margin:0px !Important;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("008_titulo_busqueda_agrupaciones")
        lblCodigoAgrupacion.Text = Traduzir("008_lbl_codigoagrupacion")
        lblDescricaoAgrupacion.Text = Traduzir("008_lbl_descricaoagrupacion")
        lblDivisaEfectivo.Text = Traduzir("008_lbl_divisa_efetivo")
        lblVigente.Text = Traduzir("008_lbl_vigente")
        lblDivisaTicket.Text = Traduzir("008_lbl_divisa_ticket")
        lblTicket.Text = Traduzir("008_lbl_ticket")
        lblDivisaOtrosValores.Text = Traduzir("008_lbl_divisa_otros_valores")
        lblOtrosValores.Text = Traduzir("008_lbl_otros_valores")
        lblDivisaCheques.Text = Traduzir("008_lbl_divisa_cheques")
        lblCheques.Text = Traduzir("008_lbl_cheques")
        lblSubTitulosAgrupaciones.Text = Traduzir("008_lbl_subtitulosagrupaciones")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("008_lbl_subtituloscriteriosbusqueda")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnNovo.Text = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("008_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("008_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("008_lbl_grd_observacion")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("008_lbl_grd_vigente")

        'Formulario

        lblCodigoAgrupacionForm.Text = Traduzir("008_lbl_codigoagrupacion")
        lblDescricaoAgrupacionForm.Text = Traduzir("008_lbl_descricaoagrupacion")
        lblVigenteForm.Text = Traduzir("008_lbl_vigente")
        lblTituloAgrupaciones.Text = Traduzir("008_lbl_titulos_mantenimento_agrupaciones")
        lblSubTitulosAgrupacionesForm.Text = Traduzir("008_lbl_subtitulos_mantenimento_agrupaciones")
        lblObservaciones.Text = Traduzir("008_lbl_observaciones")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("008_msg_agrupacioncodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("008_msg_agrupaciondescripcionobligatorio")
        csvDescripcionExistente.ErrorMessage = Traduzir("008_msg_descricaoagrupacionexistente")
        csvCodigoAgrupacionExistente.ErrorMessage = Traduzir("008_msg_codigoagrupacionexistente")
        csvTrvAgrupaciones.ErrorMessage = Traduzir("008_msg_treeviewnodesobligatorio")

    End Sub

#End Region

#Region "[CONFIGURACION PANTALAS]"

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da Pagina
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroDescripcion() As List(Of String)
        Get
            Return ViewState("FiltroDescripcion")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcion") = value
        End Set
    End Property

    Property FiltroCodigo() As List(Of String)
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigo") = value
        End Set
    End Property

    Property FiltroDivisaEfectivo() As List(Of String)
        Get
            Return ViewState("FiltroDivisaEfectivo")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDivisaEfectivo") = value
        End Set
    End Property

    Property FiltroDivisaTicket() As List(Of String)
        Get
            Return ViewState("FiltroDivisaTicket")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDivisaTicket") = value
        End Set
    End Property

    Property FiltroTicket() As List(Of String)
        Get
            Return ViewState("FiltroTicket")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroTicket") = value
        End Set
    End Property


    Property FiltroDivisaOtrosValores() As List(Of String)
        Get
            Return ViewState("FiltroDivisaOtrosValores")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDivisaOtrosValores") = value
        End Set
    End Property

    Property FiltroOtrosValores() As List(Of String)
        Get
            Return ViewState("FiltroOtrosValores")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroOtrosValores") = value
        End Set
    End Property

    Property FiltroDivisaCheques() As List(Of String)
        Get
            Return ViewState("FiltroDivisaCheques")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDivisaCheques") = value
        End Set
    End Property

    Property FiltroCheques() As List(Of String)
        Get
            Return ViewState("FiltroCheques")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCheques") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboDivisa()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        objRespuesta = objProxyUtilida.GetComboDivisas

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDivisaEfectivo.AppendDataBoundItems = True
        ddlDivisaEfectivo.Items.Clear()
        ddlDivisaEfectivo.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlDivisaEfectivo.DataTextField = "Descripcion"
        ddlDivisaEfectivo.DataValueField = "CodigoIso"
        ddlDivisaEfectivo.DataSource = objRespuesta.Divisas.OrderBy(Function(x) x.Descripcion)
        ddlDivisaEfectivo.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboDivisaTicket()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TICKET

        objRespuesta = objProxyUtilida.GetComboDivisasByTipoMedioPago(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDivisaTicket.AppendDataBoundItems = True
        ddlDivisaTicket.Items.Clear()
        ddlDivisaTicket.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlDivisaTicket.DataTextField = "Descripcion"
        ddlDivisaTicket.DataValueField = "CodigoIso"
        ddlDivisaTicket.DataSource = objRespuesta.Divisas.OrderBy(Function(x) x.Descripcion)
        ddlDivisaTicket.DataBind()


    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboTicket()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_TICKET

        If ddlDivisaTicket.SelectedItem IsNot Nothing AndAlso Not ddlDivisaTicket.SelectedItem.Value.Equals(String.Empty) Then
            objPeticion.CodigoIsoDivisa = ddlDivisaTicket.SelectedItem.Value
        End If

        objRespuesta = objProxyUtilida.GetComboMediosPagoByTipoAndDivisa(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlTicket.AppendDataBoundItems = True
        ddlTicket.Items.Clear()
        ddlTicket.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlTicket.DataTextField = "Descripcion"
        ddlTicket.DataValueField = "Codigo"
        ddlTicket.DataSource = objRespuesta.MediosPago.OrderBy(Function(x) x.Descripcion)
        ddlTicket.DataBind()

        ddlTicket.ToolTip = String.Empty

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboDivisaOtros()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES

        objRespuesta = objProxyUtilida.GetComboDivisasByTipoMedioPago(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDivisaOtrosValores.AppendDataBoundItems = True
        ddlDivisaOtrosValores.Items.Clear()
        ddlDivisaOtrosValores.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlDivisaOtrosValores.DataTextField = "Descripcion"
        ddlDivisaOtrosValores.DataValueField = "CodigoIso"
        ddlDivisaOtrosValores.DataSource = objRespuesta.Divisas.OrderBy(Function(x) x.Descripcion)
        ddlDivisaOtrosValores.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboOtros()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES

        If ddlDivisaOtrosValores.SelectedItem IsNot Nothing AndAlso Not ddlDivisaOtrosValores.SelectedItem.Value.Equals(String.Empty) Then
            objPeticion.CodigoIsoDivisa = ddlDivisaOtrosValores.SelectedItem.Value
        End If

        objRespuesta = objProxyUtilida.GetComboMediosPagoByTipoAndDivisa(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlOtrosValores.AppendDataBoundItems = True
        ddlOtrosValores.Items.Clear()
        ddlOtrosValores.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlOtrosValores.DataTextField = "Descripcion"
        ddlOtrosValores.DataValueField = "Codigo"
        ddlOtrosValores.DataSource = objRespuesta.MediosPago.OrderBy(Function(x) x.Descripcion)
        ddlOtrosValores.DataBind()

        ddlOtrosValores.ToolTip = String.Empty

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherComboDivisaCheques()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_CHEQUE
        objRespuesta = objProxyUtilida.GetComboDivisasByTipoMedioPago(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDivisaCheques.AppendDataBoundItems = True
        ddlDivisaCheques.Items.Clear()
        ddlDivisaCheques.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlDivisaCheques.DataTextField = "Descripcion"
        ddlDivisaCheques.DataValueField = "CodigoIso"
        ddlDivisaCheques.DataSource = objRespuesta.Divisas.OrderBy(Function(x) x.Descripcion)
        ddlDivisaCheques.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboCheques()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMediosPagoByTipoAndDivisa.Peticion
        objPeticion.CodigoTipoMedioPago = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_CHEQUE

        If ddlDivisaCheques.SelectedItem IsNot Nothing AndAlso Not ddlDivisaCheques.SelectedItem.Value.Equals(String.Empty) Then
            objPeticion.CodigoIsoDivisa = ddlDivisaCheques.SelectedItem.Value
        End If

        objRespuesta = objProxyUtilida.GetComboMediosPagoByTipoAndDivisa(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlCheques.AppendDataBoundItems = True
        ddlCheques.Items.Clear()
        ddlCheques.Items.Add(New ListItem(Traduzir("008_ddl_selecione"), String.Empty))
        ddlCheques.DataTextField = "Descripcion"
        ddlCheques.DataValueField = "Codigo"
        ddlCheques.DataSource = objRespuesta.MediosPago.OrderBy(Function(x) x.Descripcion)
        ddlCheques.DataBind()

        ddlCheques.ToolTip = String.Empty

    End Sub

    ''' <summary>
    ''' Busca os registros passados como parametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Function getAgrupacion() As IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta

        Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
        Dim objPeticionAgrupacion As New IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Peticion
        Dim objRespuestaAgrupacion As IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta

        'Recebe os valores do filtro

        objPeticionAgrupacion.CodigoAgrupacion = FiltroCodigo
        objPeticionAgrupacion.DescripcionAgrupacion = FiltroDescripcion

        objPeticionAgrupacion.DivisaEfectivo = FiltroDivisaEfectivo
        objPeticionAgrupacion.VigenteAgrupacion = FiltroVigente

        objPeticionAgrupacion.DivisaTicket = FiltroDivisaTicket
        objPeticionAgrupacion.CodigoTicket = FiltroTicket

        objPeticionAgrupacion.DivisaOtroValor = FiltroDivisaOtrosValores
        objPeticionAgrupacion.CodigoOtroValor = FiltroOtrosValores

        objPeticionAgrupacion.DivisaCheque = FiltroDivisaCheques
        objPeticionAgrupacion.CodigoCheque = FiltroCheques


        objRespuestaAgrupacion = objProxyAgrupacion.GetAgrupaciones(objPeticionAgrupacion)

        Return objRespuestaAgrupacion

    End Function

    ''' <summary>
    ''' Preenche o grid com os valores obtidos na coleção getAgrupacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherAgrupaciones()

        Dim objRespostaAgrupacion As IAC.ContractoServicio.Agrupacion.GetAgrupaciones.Respuesta

        'Busca os canais
        objRespostaAgrupacion = getAgrupacion()

        If Not Master.ControleErro.VerificaErro(objRespostaAgrupacion.CodigoError, objRespostaAgrupacion.NombreServidorBD, objRespostaAgrupacion.MensajeError) Then

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            MyBase.MostraMensagem(objRespostaAgrupacion.MensajeError)
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaAgrupacion.Agrupaciones.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaAgrupacion.Agrupaciones.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaAgrupacion.Agrupaciones)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigo ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " Codigo ASC "
                    Else
                        objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

                ProsegurGridView1.CarregaControle(objDt)

            Else

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento ordenar as colunas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Dim objDT As DataTable

        objDT = ProsegurGridView1.ConvertListToDataTable(getAgrupacion().Agrupaciones)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " Codigo ASC "
        Else
            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
        End If

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Evento de estilo do grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

        'Configura o estilo dos controles presentes no pager
        CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
        CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
        CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

        CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
        CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
        CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 129
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


    End Sub

    ''' <summary>
    ''' Configuração da colunas do grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Observación
            '4 - Vigente

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If Not e.Row.DataItem("observacion") Is DBNull.Value AndAlso e.Row.DataItem("observacion").Length > NumeroMaximoLinha Then
                e.Row.Cells(3).Text = e.Row.DataItem("observacion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If CBool(e.Row.DataItem("vigente")) Then
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If

    End Sub

    ''' <summary>
    ''' Traduz o cabecalho do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' [pda] 10/02/2009 Criado
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then

            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("008_lbl_grd_codigo")

            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 121
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 122

            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("008_lbl_grd_descripcion")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 123
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 124

            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("008_lbl_grd_observacion")

            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("008_lbl_grd_vigente")
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 127
            'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 128

        End If

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' [octavio.piramo] 13/03/2009 Alterado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Preenche Combos Novamente
            PreencherComboDivisa()

            PreencherComboDivisaTicket()
            PreencherComboTicket()

            PreencherComboDivisaOtros()
            PreencherComboOtros()

            PreencherComboDivisaCheques()
            PreencherComboCheques()

            ' limpar os campos código e descrição
            txtCodigoAgrupacion.Text = String.Empty
            txtDescricaoAgrupacion.Text = String.Empty

            LimparCamposForm()
            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            txtCodigoAgrupacion.Focus()
            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento botão buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)
        Dim strListDivisaEfectivo As New List(Of String)

        Dim strListDivisaTicket As New List(Of String)
        Dim strListTicket As New List(Of String)


        Dim strListDivisaOtrosValores As New List(Of String)
        Dim strListOtrosValores As New List(Of String)

        Dim strListDivisaCheques As New List(Of String)
        Dim strListCheques As New List(Of String)


        'Campos Texto
        strListCodigo.Add(txtCodigoAgrupacion.Text.Trim.ToUpper)
        strListDescricao.Add(txtDescricaoAgrupacion.Text.Trim.ToUpper)
        'Combos
        strListDivisaEfectivo.Add(ddlDivisaEfectivo.SelectedValue)

        strListDivisaTicket.Add(ddlDivisaTicket.SelectedValue)
        strListTicket.Add(ddlTicket.SelectedValue)

        strListDivisaOtrosValores.Add(ddlDivisaOtrosValores.SelectedValue)
        strListOtrosValores.Add(ddlOtrosValores.SelectedValue)

        strListDivisaCheques.Add(ddlDivisaCheques.SelectedValue)
        strListCheques.Add(ddlCheques.SelectedValue)

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao

        FiltroDivisaEfectivo = strListDivisaEfectivo
        FiltroVigente = chkVigente.Checked

        FiltroDivisaTicket = strListDivisaTicket
        FiltroTicket = strListTicket

        FiltroDivisaOtrosValores = strListDivisaOtrosValores
        FiltroOtrosValores = strListOtrosValores

        FiltroDivisaCheques = strListDivisaCheques
        FiltroCheques = strListCheques


        'Retorna os canais de acordo com o filtro aciam
        PreencherAgrupaciones()
        
    End Sub

    ''' <summary>
    ''' Ação ao clicar no botão baja.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' [anselmo.gois] 20/07/2009 - Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Peticion
            Dim objRespuestaAgrupacion As IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta

            Dim strCodigo As String = String.Empty
            'Retorna o valor da linha selecionada no grid
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then

                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    strCodigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    strCodigo = hiddenCodigo.Value.ToString()
                End If
            End If

            Dim PeticionAgrupacionDetail As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion
            Dim RespostaAgrupacionDetail As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta

            'Criando um Agrupacion para exclusão
            Dim objAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion
            objAgrupacion.Codigo = Server.UrlDecode(strCodigo)
            objAgrupacion.Vigente = False

            'Preenche o objeto PeticionAgrupacionDetail
            PeticionAgrupacionDetail.CodigoAgrupacion = New List(Of String)
            PeticionAgrupacionDetail.CodigoAgrupacion.Add(strCodigo)

            'Chama o serviço GetAgrupacionDetail
            RespostaAgrupacionDetail = objProxyAgrupacion.GetAgrupacionesDetail(PeticionAgrupacionDetail)

            'Verifica se retornou erro.
            If Not Master.ControleErro.VerificaErro(RespostaAgrupacionDetail.CodigoError, RespostaAgrupacionDetail.NombreServidorBD, RespostaAgrupacionDetail.MensajeError) Then
                MyBase.MostraMensagem(RespostaAgrupacionDetail.MensajeError)
                Exit Sub
            End If

            'Verifica se retornou divisas.
            If RespostaAgrupacionDetail.Agrupaciones(0).Divisas IsNot Nothing AndAlso RespostaAgrupacionDetail.Agrupaciones(0).Divisas.Count > 0 Then

                'Istancia novas coleções de medio pago e efectivo.
                objAgrupacion.Efectivos = New ContractoServicio.Agrupacion.SetAgrupaciones.EfetivoColeccion
                objAgrupacion.MediosPago = New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPagoColeccion

                'Percorre a coleção de divisas
                For Each objdivisa In RespostaAgrupacionDetail.Agrupaciones(0).Divisas

                    'Verifica se tem efectivo
                    If objdivisa.TieneEfectivo Then
                        'Adicona na coleção de efectivo
                        objAgrupacion.Efectivos.Add(New ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo With { _
                                                                       .CodigoIsoDivisa = objdivisa.CodigoIso})
                    End If

                    'Verifica se tem tipoMeidoPago
                    If objdivisa.TiposMedioPago IsNot Nothing AndAlso objdivisa.TiposMedioPago.Count > 0 Then

                        'Percorre a coleção de tipoMedioPago
                        For Each objTipoMp In objdivisa.TiposMedioPago

                            'Verifica se tem MedioPago
                            If objTipoMp.MediosPago IsNot Nothing AndAlso objTipoMp.MediosPago.Count > 0 Then

                                'Percorre a coleção de MedioPago.
                                For Each objMp In objTipoMp.MediosPago
                                    'Adiciona o objeto MedioPago na coleção de MedioPago
                                    objAgrupacion.MediosPago.Add(New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPago With { _
                                                                 .CodigoIsoDivisa = objdivisa.CodigoIso, _
                                                                 .CodigoMedioPago = objMp.Codigo, _
                                                                 .CodigoTipoMedioPago = objTipoMp.Codigo})
                                Next
                            End If
                        Next
                    End If
                Next
            End If

            'Incluindo na coleção
            Dim objAgrupacionCol As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.AgrupacionColeccion
            objAgrupacionCol.Add(objAgrupacion)

            'Associa a coleção de canais criado para a petição
            objPeticionAgrupacion.Agrupaciones = objAgrupacionCol
            objPeticionAgrupacion.CodigoUsuario = MyBase.LoginUsuario

            'Exclui a petição
            objRespuestaAgrupacion = objProxyAgrupacion.SetAgrupaciones(objPeticionAgrupacion)

            If Master.ControleErro.VerificaErro(objRespuestaAgrupacion.CodigoError, objRespuestaAgrupacion.NombreServidorBD, objRespuestaAgrupacion.MensajeError) Then

                If objRespuestaAgrupacion.RespuestaAgrupaciones.Count > 0 Then

                    If objRespuestaAgrupacion.RespuestaAgrupaciones(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError)
                        Exit Sub
                    End If

                End If

                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))

                btnCancelar_Click(Nothing, Nothing)
                ExecutarBusca()
                UpdatePanelGrid.Update()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS DROPDOWNLIST]"

    ''' <summary>
    ''' Chama o metodo para preencher o dropdown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' [pda] 10/02/2009 Criado
    Private Sub ddlDivisaTicket_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDivisaTicket.SelectedIndexChanged
        PreencherComboTicket()
        ddlDivisaTicket.ToolTip = ddlDivisaTicket.SelectedItem.Text
    End Sub

    ''' <summary>
    ''' Chama o metodo para preencher o dropdown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' [pda] 10/02/2009 Criado
    Private Sub ddlDivisaOtrosValores_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDivisaOtrosValores.SelectedIndexChanged
        PreencherComboOtros()
        ddlDivisaOtrosValores.ToolTip = ddlDivisaOtrosValores.SelectedItem.Text
    End Sub

    ''' <summary>
    ''' Chama o metodo para preencher o dropdown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' [pda] 10/02/2009 Criado
    Private Sub ddlDivisaCheques_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDivisaCheques.SelectedIndexChanged
        PreencherComboCheques()
        ddlDivisaCheques.ToolTip = ddlDivisaCheques.SelectedItem.Text
    End Sub

    Private Sub ddlTicket_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTicket.SelectedIndexChanged
        ddlTicket.ToolTip = ddlTicket.SelectedItem.Text
    End Sub

    Private Sub ddlOtrosValores_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlOtrosValores.SelectedIndexChanged
        ddlOtrosValores.ToolTip = ddlOtrosValores.SelectedItem.Text
    End Sub

    Private Sub ddlCheques_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCheques.SelectedIndexChanged
        ddlCheques.ToolTip = ddlCheques.SelectedItem.Text
    End Sub

    Private Sub ddlDivisaEfectivo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDivisaEfectivo.SelectedIndexChanged
        ddlDivisaEfectivo.ToolTip = ddlDivisaEfectivo.SelectedItem.Text
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração do controle de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Private Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction


            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                chkVigente.Checked = True
                pnlSemRegistro.Visible = False

                If Not Page.IsPostBack Then
                    txtCodigoAgrupacion.Text = String.Empty
                    txtDescricaoAgrupacion.Text = String.Empty
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select
    End Sub

#End Region
#Region "TreeView Dados"
    Public Sub CarregaTreeViewDividasPossiveis()

        CarregaTreeview(TrvDivisas, getDivisasPosiveis, eExpadirNivel.Primeiro)

    End Sub
    Public Function getDivisasPosiveis() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyUtilidad
        'Dim lnAccionAgrupacion As New LogicaNegocio.AccionAgrupacion
        Dim objRespuesta As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta = objProxy.GetDivisasMedioPago()

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Divisas
        Else
            Return Nothing
        End If

    End Function

    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion, pNaoExpandirAPartirNivel As eExpadirNivel)

        pobjTreeView.Nodes.Clear()

        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                'Adiciona o nó efetivo
                objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)


                For Each TipoMedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    'Não expande o nó 
                    If pNaoExpandirAPartirNivel = eExpadirNivel.Segundo Then
                        objTreeNodeTipoMedioPago.Expanded = False
                    Else
                        objTreeNodeTipoMedioPago.Expanded = True
                    End If

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                If pNaoExpandirAPartirNivel = eExpadirNivel.Primeiro Then
                    objTreeNodeDivisa.Expanded = False
                Else
                    objTreeNodeDivisa.Expanded = True
                End If

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next

        End If

    End Sub
    Public Enum eExpadirNivel As Integer

        Primeiro = 0
        Segundo = 1
        Nenhum = 2

    End Enum
#End Region
#Region "[ÁRVORE BINÁRIA]"

    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyNode(objNode As TreeNode) As TreeNode

        Dim TempNode As New TreeNode
        TempNode.Text = objNode.Text
        TempNode.Value = objNode.Value
        TempNode.Selected = objNode.Selected
        TempNode.Expanded = objNode.Expanded
        TempNode.ImageUrl = objNode.ImageUrl
        TempNode.ToolTip = objNode.ToolTip

        Return TempNode

    End Function

    ''' <summary>
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getChilds(objTreeNode As TreeNode) As TreeNode

        Dim objTreeNodeRetorno As TreeNode = CopyNode(objTreeNode)

        If objTreeNode.ChildNodes.Count > 0 Then
            Dim objFilhoRetorno As TreeNode
            For Each objFilho As TreeNode In objTreeNode.ChildNodes
                objFilhoRetorno = getChilds(objFilho)
                objTreeNodeRetorno.ChildNodes.Add(objFilhoRetorno)
            Next
        End If

        Return objTreeNodeRetorno

    End Function

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParent(ByRef objTreeNode As TreeNode)

        Dim objTreeNodeCorrente As TreeNode = CopyNode(objTreeNode)
        If objTreeNode.Parent IsNot Nothing Then
            Dim objPai As TreeNode = getParent(objTreeNode.Parent)
            objPai.ChildNodes.Add(objTreeNodeCorrente)
            Return objPai.ChildNodes(0)
        Else
            Return objTreeNodeCorrente
        End If

    End Function

    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaArvoreSelecionada(pObjSelecionado As TreeNode) As TreeNode

        'Se for o nível de raiz,inclui os filhos
        If pObjSelecionado.Depth = 0 Then

            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)
            Return objNoFilhos

        Else

            'Dado um objeto nó selecionado, retorna os pais e filhos a serem inseridos na coleção
            Dim objNoSelecionado As TreeNode = getParent(pObjSelecionado)
            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            'Adiciona os filhos
            If objNoSelecionado IsNot Nothing Then
                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                While objTreeNodeChildCol.Count > 0
                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                End While
            End If

            'Suspende para o nível Root
            Dim objGetFather As TreeNode = objNoSelecionado
            While objGetFather.Parent IsNot Nothing
                objGetFather = objGetFather.Parent
            End While

            'Retorna o nó selecionado de forma hierárquica
            Return objGetFather
        End If

    End Function

    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Public Sub RemoveNode(ByRef pObjTreeView As TreeView)

        Dim objPai As TreeNode = pObjTreeView.SelectedNode.Parent
        Dim objDelete As TreeNode = pObjTreeView.SelectedNode

        While objPai IsNot Nothing
            objPai.ChildNodes.Remove(objDelete)

            If objPai.ChildNodes.Count = 0 Then
                objDelete = objPai
                objPai = objPai.Parent
            Else
                Exit While
            End If
        End While

        If objDelete IsNot Nothing Then
            pObjTreeView.Nodes.Remove(objDelete)
        End If

    End Sub

    ''' <summary>
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Public Sub InsereNaArvoreDinamica(pObjTreeView As TreeNodeCollection, pObjSelecionado As TreeNode)

        Dim objExiste As TreeNode = pObjSelecionado
        Dim ObjColCorrente As TreeNodeCollection = pObjTreeView

        'Caso não exista nenhum nó na árvore adiciona o primeiro
        If ObjColCorrente.Count = 0 Then
            ObjColCorrente.Add(objExiste)
            Exit Sub
        End If

        While objExiste IsNot Nothing

            Dim addNo As Boolean = True
            'Verifica na árvore de destino se o objeto selecionado existe
            For Each pObjSelecao As TreeNode In ObjColCorrente
                If pObjSelecao.Text = objExiste.Text Then
                    'Se existe filho então continua o processamento
                    If pObjSelecao.ChildNodes.Count > 0 Then
                        'Se selecionou um nó pai inclui novamente os filhos a partir da seleção efetuada
                        If objExiste.ChildNodes.Count > 0 AndAlso objExiste.Selected Then
                            pObjSelecao.ChildNodes.Clear()
                            Dim objNoSelecionado As TreeNode = pObjSelecao
                            'Seleciona o nó
                            objNoSelecionado.Selected = True

                            Dim objNoFilhos As TreeNode = getChilds(objExiste)
                            If objNoSelecionado IsNot Nothing Then
                                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                                While objTreeNodeChildCol.Count > 0
                                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                                End While
                            End If
                            Exit Sub
                        End If
                        'Passa o próximo filho do objeto selecionado para ser verificado
                        objExiste = objExiste.ChildNodes(0)
                        'Passa a próxima coleção da árvore de destino para ser verificada
                        ObjColCorrente = pObjSelecao.ChildNodes
                        addNo = False
                        Exit For
                    Else
                        'Seleciona o nó
                        pObjSelecao.Selected = True
                        Exit Sub
                    End If
                End If

            Next

            'Adiciona na árvore de forma ordenada
            If addNo Then
                ObjColCorrente.AddAt(IndiceOrdenacao(ObjColCorrente, objExiste), objExiste)
                Exit While
            End If

        End While

    End Sub

    ''' <summary>
    ''' Retorna o índice antes de inserir o nó na coleção passada
    ''' </summary>
    ''' <param name="TreeNodeCol">Coleção a ser verificada a posição</param>
    ''' <param name="treenode">Nó para inclusão</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndiceOrdenacao(TreeNodeCol As TreeNodeCollection, treenode As TreeNode) As Integer

        Dim treeNodeColTemp As New TreeNodeCollection

        For Each obj As TreeNode In TreeNodeCol
            treeNodeColTemp.Add(CopyNode(obj))
        Next

        If treeNodeColTemp.Count > 0 Then
            treeNodeColTemp.Add(CopyNode(treenode))

            Dim retorno = From objTree In treeNodeColTemp Order By objTree.Text Ascending

            Dim i As Integer = 0
            For Each objRetorno As Object In retorno
                If objRetorno.text = treenode.Text Then
                    Return i
                End If
                i += 1
            Next
        End If

        Return 0

    End Function

#End Region

#Region "Botoes treeview"
    Protected Sub imgBtnIncluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnIncluir.Click

        Try

            If TrvDivisas.SelectedNode IsNot Nothing Then
                InsereNaArvoreDinamica(TrvAgrupaciones.Nodes, MontaArvoreSelecionada(TrvDivisas.SelectedNode))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão excluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgBtnExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnExcluir.Click

        Try
            If TrvAgrupaciones.SelectedNode IsNot Nothing Then
                RemoveNode(TrvAgrupaciones)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region

#Region "[PROPRIEDADES]"
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = False
            End If
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region


    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub
#Region "Persistencia Dados"
    Private Sub ExecutarGrabar()
        Try

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Peticion
            Dim objRespuestaAgrupacion As IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Respuesta

            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.Agrupacion

            'Recebe os valores do formulário
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objAgrupacion.Vigente = True
            Else
                objAgrupacion.Vigente = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked
            objAgrupacion.Codigo = txtCodigoAgrupacionForm.Text.Trim
            objAgrupacion.Descripcion = txtDescricaoAgrupacionForm.Text.Trim
            objAgrupacion.Observacion = txtObservaciones.Text
            objAgrupacion.Efectivos = New ContractoServicio.Agrupacion.SetAgrupaciones.EfetivoColeccion
            objAgrupacion.MediosPago = New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPagoColeccion

            '############################################# 
            'Obter os dados da Treeview de Agrupação

            For Each objTreeNodeDividas As TreeNode In TrvAgrupaciones.Nodes

                For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDividas.ChildNodes

                    If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                        Dim objEfectivo As New ContractoServicio.Agrupacion.SetAgrupaciones.Efectivo
                        objEfectivo.CodigoIsoDivisa = objTreeNodeTipoMedioPago.Value
                        objAgrupacion.Efectivos.Add(objEfectivo)

                    Else

                        For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                            Dim objMedioPago As New ContractoServicio.Agrupacion.SetAgrupaciones.MedioPago
                            objMedioPago.CodigoIsoDivisa = objTreeNodeDividas.Value
                            objMedioPago.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPago.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objAgrupacion.MediosPago.Add(objMedioPago)

                        Next
                    End If

                Next
            Next

            '#############################################            

            'Cria a coleção para envio
            Dim objColAgrupacion As New IAC.ContractoServicio.Agrupacion.SetAgrupaciones.AgrupacionColeccion
            objColAgrupacion.Add(objAgrupacion)

            'Passa a coleção para a agrupação
            objPeticionAgrupacion.Agrupaciones = objColAgrupacion
            objPeticionAgrupacion.CodigoUsuario = MyBase.LoginUsuario

            'Obtem o objeto de resposta para validação
            objRespuestaAgrupacion = objProxyAgrupacion.SetAgrupaciones(objPeticionAgrupacion)

            If Master.ControleErro.VerificaErro(objRespuestaAgrupacion.CodigoError, objRespuestaAgrupacion.NombreServidorBD, objRespuestaAgrupacion.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaAgrupacion.RespuestaAgrupaciones(0).CodigoError, objRespuestaAgrupacion.NombreServidorBD, objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))

                    btnCancelar_Click(Nothing, Nothing)
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                Else
                    MyBase.MostraMensagem(objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError)
                End If

            Else

                If objRespuestaAgrupacion.RespuestaAgrupaciones IsNot Nothing _
                    AndAlso objRespuestaAgrupacion.RespuestaAgrupaciones.Count > 0 _
                    AndAlso objRespuestaAgrupacion.RespuestaAgrupaciones(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    MyBase.MostraMensagem(objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuestaAgrupacion.RespuestaAgrupaciones(0).MensajeError)
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoAgrupacionForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoAgrupacionForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoAgrupacionForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoAgrupacionForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If TrvAgrupaciones.Nodes.Count = 0 Then

                    strErro.Append(csvTrvAgrupaciones.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTrvAgrupaciones.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        TrvAgrupaciones.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTrvAgrupaciones.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoAgrupacion(txtCodigoAgrupacionForm.Text.Trim()) Then

                strErro.Append(csvCodigoAgrupacionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAgrupacionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAgrupacion.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAgrupacionExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If ExisteDescricaoAgrupacion(txtDescricaoAgrupacionForm.Text.Trim()) Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoAgrupacion.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código da agrupacion já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo] 01/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoAgrupacion(codigoAgrupacion As String) As Boolean

        Dim objRespostaVerificarCodigoAgrupacion As IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionVerificarCodigoAgrupacion As New IAC.ContractoServicio.Agrupacion.VerificarCodigoAgrupacion.Peticion

            'Verifica se o código do Agrupacion existe no BD
            objPeticionVerificarCodigoAgrupacion.CodigoAgrupacion = codigoAgrupacion
            objRespostaVerificarCodigoAgrupacion = objProxyAgrupacion.VerificarCodigoAgrupacion(objPeticionVerificarCodigoAgrupacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAgrupacion.CodigoError, objRespostaVerificarCodigoAgrupacion.NombreServidorBD, objRespostaVerificarCodigoAgrupacion.MensajeError) Then
                Return objRespostaVerificarCodigoAgrupacion.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarCodigoAgrupacion.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição da agrupacion já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoAgrupacion(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoAgrupacion As IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

                If descricao.Trim.Equals(DescricaoAtual) Then
                    Return False
                End If

            End If

            Dim objProxyAgrupacion As New Comunicacion.ProxyAgrupacion
            Dim objPeticionVerificarDescricaoAgrupacion As New IAC.ContractoServicio.Agrupacion.VerificarDescripcionAgrupacion.Peticion

            'Verifica se o código do Agrupacion existe no BD
            objPeticionVerificarDescricaoAgrupacion.DescripcionAgrupacion = txtDescricaoAgrupacion.Text.Trim
            objRespostaVerificarDescricaoAgrupacion = objProxyAgrupacion.VerificarDescripcionAgrupacion(objPeticionVerificarDescricaoAgrupacion)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoAgrupacion.CodigoError, objRespostaVerificarDescricaoAgrupacion.NombreServidorBD, objRespostaVerificarDescricaoAgrupacion.MensajeError) Then
                Return objRespostaVerificarDescricaoAgrupacion.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarDescricaoAgrupacion.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Sub CarregaDados(codAgrupacion As String)

        Dim objColAgrupacion As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion
        objColAgrupacion = getAgrupacion(codAgrupacion)

        If objColAgrupacion IsNot Nothing AndAlso objColAgrupacion.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoAgrupacionForm.Text = objColAgrupacion(0).Codigo
            txtCodigoAgrupacionForm.ToolTip = objColAgrupacion(0).Codigo

            txtDescricaoAgrupacionForm.Text = objColAgrupacion(0).Descripcion
            txtDescricaoAgrupacionForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColAgrupacion(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColAgrupacion(0).Observacion
            chkVigenteForm.Checked = objColAgrupacion(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColAgrupacion(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoAgrupacionForm.Text
            End If

            If objColAgrupacion(0).Divisas IsNot Nothing AndAlso objColAgrupacion(0).Divisas.Count > 0 Then
                CarregaTreeview(TrvAgrupaciones, objColAgrupacion(0).Divisas, eExpadirNivel.Segundo)
            End If

        End If

    End Sub
    Private Function getAgrupacion(pstrCodAgrupacao As String) As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.AgrupacionColeccion

        Dim objPeticion As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Peticion
        objPeticion.CodigoAgrupacion = New List(Of String)
        objPeticion.CodigoAgrupacion.Add(pstrCodAgrupacao)

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyAgrupacion
        'Dim lnAccionAgrupacion As New LogicaNegocio.AccionAgrupacion
        Dim objRespuesta As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Respuesta = objProxy.GetAgrupacionesDetail(objPeticion)

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Agrupaciones
        Else
            Return Nothing
        End If


    End Function
    Private Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.DivisaColeccion, pNaoExpandirAPartirNivel As eExpadirNivel)

        pobjTreeView.Nodes.Clear()
        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                If objDivisa.TieneEfectivo Then
                    'Adiciona o nó efetivo
                    objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)
                End If

                For Each TipoMedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Agrupacion.GetAgrupacionesDetail.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    If Not (pNaoExpandirAPartirNivel = eExpadirNivel.Segundo) Then
                        objTreeNodeTipoMedioPago.Expanded = False 'Não expande o nó 
                    Else
                        objTreeNodeTipoMedioPago.Expanded = True 'Expande o nó 
                    End If

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                If pNaoExpandirAPartirNivel = eExpadirNivel.Primeiro Then
                    objTreeNodeDivisa.Expanded = False
                Else
                    objTreeNodeDivisa.Expanded = True
                End If

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next
        End If

    End Sub
#End Region

#Region "Botoes Formulario"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCamposForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

            txtCodigoAgrupacionForm.Focus()
            Acao = Utilidad.eAcao.Alta

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

    Private Sub LimparCamposForm()
        txtCodigoAgrupacionForm.Text = String.Empty
        txtCodigoAgrupacionForm.Enabled = True
        txtDescricaoAgrupacionForm.Text = String.Empty
        txtObservaciones.Text = String.Empty
        chkVigenteForm.Checked = True
        DescricaoAtual = String.Empty
        TrvDivisas.Nodes.Clear()
        TrvAgrupaciones.Nodes.Clear()
        CarregaTreeViewDividasPossiveis()
    End Sub


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCamposForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False

            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                CarregaDados(Server.UrlDecode(codigo))

                CarregaTreeViewDividasPossiveis()

                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True

                Acao = Utilidad.eAcao.Consulta
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                CarregaDados(Server.UrlDecode(codigo))
                CarregaTreeViewDividasPossiveis()
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoAgrupacionForm.Enabled = False
                txtDescricaoAgrupacionForm.Focus()
                Acao = Utilidad.eAcao.Modificacion

            End If

            If chkVigenteForm.Checked AndAlso EsVigente Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                CarregaDados(Server.UrlDecode(codigo))
                CarregaTreeViewDividasPossiveis()
                btnBajaConfirm.Visible = True
                btnBajaConfirm.Enabled = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True

                Acao = Utilidad.eAcao.Baja
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class