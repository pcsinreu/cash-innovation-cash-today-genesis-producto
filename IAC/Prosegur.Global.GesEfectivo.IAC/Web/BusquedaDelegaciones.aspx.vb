Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxGridView

''' <summary>
''' Página de Busca de Delegaciones 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 14/02/13 - Criado </history>
Partial Public Class BusquedaDelegaciones
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        ddlPais.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtCodDelegaciones.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        lblDesDelegaciones.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        'Formulario
        txtCodigoDelegacion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoDelegaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoDelegaciones.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtGmtMinutos.ClientID & "').focus();return false;}} else {return true}; ")
        txtCodigoDelegacion.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigoDelegacion.ClientID & "','" & txtCodigoDelegacion.MaxLength & "');")
        txtDescricaoDelegaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtDescricaoDelegaciones.ClientID & "','" & txtDescricaoDelegaciones.MaxLength & "');")
        txtGmtMinutos.Attributes.Add("onkeyup", "limitaCaracteres('" & txtGmtMinutos.ClientID & "','" & txtGmtMinutos.MaxLength & "');")
        txtFechaVeranoInicio.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtFechaVeranoInicio.ClientID & "','" & txtFechaVeranoInicio.MaxLength & "');")
        txtFechaVeranoFim.Attributes.Add("onblur", "limitaCaracteres('" & txtFechaVeranoFim.ClientID & "','" & txtFechaVeranoFim.MaxLength & "');")
        txtCantidadMinAjuste.Attributes.Add("onkeyup", "limitaCaracteres('" & txtCantidadMinAjuste.ClientID & "','" & txtCantidadMinAjuste.MaxLength & "');")

        txtCantidadMinAjuste.Attributes.Add("onKeyDown", "BloquearColar();")
        txtGmtMinutos.Attributes.Add("onKeyDown", "BloquearColar();")

        txtZona.Attributes.Add("onkeyup", "limitaCaracteres('" & txtZona.ClientID & "','" & txtZona.MaxLength & "');")
        ddlPaisForm.Attributes.Add("onKeyDown", "BloquearColar();")

        txtFechaVeranoFim.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFechaVeranoFim.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")

        txtFechaVeranoInicio.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFechaVeranoInicio.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")

        txtGmtMinutos.Attributes.Add("onkeypress", "return ValorNumericoGmt(event);")
        txtCantidadMinAjuste.Attributes.Add("onkeypress", "return ValorNumericoGmt(event);")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DELEGACION
        ' desativar validação de ação
        MyBase.ValidarAcao = False

        MyBase.CodFuncionalidad = "IAC_BUSQUEDA_DELEGACIONES"
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Delegaciones")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                'Preenche Combos
                PreencherComboDelegaciones()
                txtCodDelegaciones.Focus()
            End If

            ConfigurarControle_ClientePtoServ()
            TrataFoco()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        If Not Page.IsPostBack Then
            Try
                'ControleBotoes()
                btnAltaAjeno.Attributes.Add("style", "margin-left: 15px;")
                btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("013_titulo_busqueda_delegaciones")
        lblCodigoPais.Text = Traduzir("013_lbl_codigo_pais")
        lblCodDelegaciones.Text = Traduzir("013_lbl_codigo_delegaciones")
        lblDesDelegaciones.Text = Traduzir("013_lbl_descricaodelegaciones")
        lblVigente.Text = Traduzir("013_lbl_vigente")
        lblSubTitulosDelegaciones.Text = Traduzir("013_lbl_delegaciones")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("013_lbl_criterio")
        csvPais.ErrorMessage = Traduzir("013_msg_PaisObligatorio")
        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnAltaAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAltaAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("013_lbl_grd_codigoDel")
        GdvResultado.Columns(2).HeaderText = Traduzir("013_lbl_grd_descripcionDelegacion")
        GdvResultado.Columns(3).HeaderText = Traduzir("013_lbl_grd_vigenteDelegacion")


        'Formulario
        lblCantidadMinAjuste.Text = Traduzir("013_lbl_cantidadAjuste")
        lblCodigoDelegacion.Text = Traduzir("013_lbl_codigoDelegacion")
        lblDescricaoDelegaciones.Text = Traduzir("013_lbl_descDelegacion")
        lblFechaVeranoFim.Text = Traduzir("013_lbl_FechaVeranoFim")
        lblFechaVeranoInicio.Text = Traduzir("013_lbl_FechaVeranoInicio")
        lblGmtMinutos.Text = Traduzir("013_lbl_gmtMinutos")
        lblPais.Text = Traduzir("013_lbl_pais")
        lblTituloDelegacione.Text = Traduzir("013_titulo_mantenimiento_Delegaciones")
        lblZona.Text = Traduzir("013_lbl_zona")
        lblVigenteForm.Text = Traduzir("013_lbl_vigente")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("013_msg_Delegacioncodigoobligatorio")
        csvPaisForm.ErrorMessage = Traduzir("013_msg_PaisObligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("013_msg_Delegaciondescripcionobligatorio")
        csvFechaVeranoFim.ErrorMessage = Traduzir("013_msg_FechaVeranoFin")
        csvFechaVeranoInicio.ErrorMessage = Traduzir("013_msg_FechaVeranoIncio")
        csvGmtMinutosObrigatorio.ErrorMessage = Traduzir("013_msg_DelegacionGmtMinutos")
        csvZona.ErrorMessage = Traduzir("013_msg_delegacionZona")
        csvCantiAjuste.ErrorMessage = Traduzir("013_msg_Delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("013_msg_Existente")
        csvFechaVeranoInicioInvalida.ErrorMessage = Traduzir("013_msg_erro_Data")

        lblTotasDelegaciones.Text = MyBase.RecuperarValorDic("lblTotasDelegaciones")

        btnAddPtoServ.Text = MyBase.RecuperarValorDic("Anadir")

        grid.Columns(0).Caption = MyBase.RecuperarValorDic("bancoCapital")
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("bancoTesoreria")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("CuentaTesoreria")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("DatosBancarios")
        grid.Columns(4).Caption = MyBase.RecuperarValorDic("Quitar")


    End Sub
#End Region

#Region "[PAGE EVENTS]"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init, grid.Init
        DefinirParametrosBase()
        TraduzirControles()
        grid.DataSource = lstDelegaciones()

        grid.DataBind()
    End Sub
#End Region

#Region "[CONFIGURACION PANTALLA]"
    Private _ucDatosBancarios As ucDatosBanc
    Public ReadOnly Property ucDatosBanc As ucDatosBanc
        Get
            If _ucDatosBancarios Is Nothing Then
                _ucDatosBancarios = LoadControl("~\Controles\ucDatosBanc.ascx")
                _ucDatosBancarios.ID = "ucDatosBanc"
                _ucDatosBancarios.strBtnExecutar = btnConsomeTotalizador.ClientID
                If phDatosBanc.Controls.Count = 0 Then
                    phDatosBanc.Controls.Add(_ucDatosBancarios)
                End If
            End If
            Return _ucDatosBancarios
        End Get
    End Property

    Private Sub btnConsomeTotalizador_Click(sender As Object, e As EventArgs) Handles btnConsomeTotalizador.Click
        Try
            System.Threading.Thread.Sleep(10)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[PROPRIEDADES]"



    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2013 Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroDescripcion() As String
        Get
            Return ViewState("FiltroDescripcion")
        End Get
        Set(value As String)
            ViewState("FiltroDescripcion") = value
        End Set
    End Property

    Property FiltroCodigoPais() As String
        Get
            Return ViewState("FiltroCodigoPais")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoPais") = value
        End Set
    End Property

    Property FiltroCodigoDelegaciones() As String
        Get
            Return ViewState("FiltroCodigoDelegaciones")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoDelegaciones") = value
        End Set
    End Property

    Property Delegaciones() As ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
        Get
            If ViewState("Delegaciones") Is Nothing Then
                ViewState("Delegaciones") = New ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
            End If
            Return ViewState("Delegaciones")
        End Get
        Set(value As ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion)
            ViewState("Delegaciones") = value
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


    Property lstDelegaciones() As List(Of Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones.DelegacionFacturacion)
        Get
            If Session("_lstDelegaciones") Is Nothing Then
                Session("_lstDelegaciones") = New List(Of Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones.DelegacionFacturacion)
            End If
            Return Session("_lstDelegaciones")
        End Get
        Set(value As List(Of Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones.DelegacionFacturacion))
            Session("_lstDelegaciones") = value
        End Set
    End Property



#End Region

#Region "[Helpers]"

    Public Property ClientesPtoServ As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesPtoServ.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesPtoServ.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesPtoServ As ucCliente
    Public Property ucClientesPtoServ() As ucCliente
        Get
            If _ucClientesPtoServ Is Nothing Then
                _ucClientesPtoServ = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesPtoServ.ID = Me.ID & "_ucClientesPtoServ"
                AddHandler _ucClientesPtoServ.Erro, AddressOf ErroControles
                phClientesPtoServ.Controls.Add(_ucClientesPtoServ)
            End If
            Return _ucClientesPtoServ
        End Get
        Set(value As ucCliente)
            _ucClientesPtoServ = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub ConfigurarControle_ClientePtoServ()

        Me.ucClientesPtoServ.EsBancoCapital = True

        Me.ucClientesPtoServ.SelecaoMultipla = False
        Me.ucClientesPtoServ.ClienteHabilitado = True
        Me.ucClientesPtoServ.ClienteObrigatorio = True
        Me.ucClientesPtoServ.ClienteTitulo = MyBase.RecuperarValorDic("bancoCapital")

        Me.ucClientesPtoServ.SubClienteHabilitado = True
        Me.ucClientesPtoServ.SubClienteObrigatorio = True
        Me.ucClientesPtoServ.ucSubCliente.MultiSelecao = False
        Me.ucClientesPtoServ.SubClienteTitulo = MyBase.RecuperarValorDic("bancoTesoreria")

        Me.ucClientesPtoServ.PtoServicioHabilitado = True
        Me.ucClientesPtoServ.PtoServicoObrigatorio = True
        Me.ucClientesPtoServ.ucPtoServicio.MultiSelecao = False
        Me.ucClientesPtoServ.PtoServicioTitulo = MyBase.RecuperarValorDic("CuentaTesoreria")


        If ClientesPtoServ IsNot Nothing Then
            Me.ucClientesPtoServ.Clientes = ClientesPtoServ
        End If


    End Sub

    Private Sub ucClientesPtoServ_OnControleAtualizado() Handles _ucClientesPtoServ.UpdatedControl
        Try
            If ucClientesPtoServ.Clientes IsNot Nothing Then
                ClientesPtoServ = ucClientesPtoServ.Clientes
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUserControl.ucCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucCliente.ExibirDados(True)
    End Sub
#End Region


#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca as delegações de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2013 Criado
    ''' </history>
    Public Function getDelegaciones() As IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

        Dim objProxy As New ProxyDelegacion
        Dim objPeticion As New IAC.ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

        'Recebe os valores do filtro
        objPeticion.BolVigente = FiltroVigente
        objPeticion.OidPais = FiltroCodigoPais
        objPeticion.CodDelegacion = FiltroCodigoDelegaciones
        objPeticion.DesDelegacion = FiltroDescripcion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.GetDelegaciones(objPeticion)

        Delegaciones = objRespuesta.Delegacion

        Return objRespuesta

    End Function

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

    ''' <summary>
    ''' Metodo popula o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Public Sub PreencherDelegaciones()
        Dim objRespostaDelegaciones As IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

        'Busca as divisas
        objRespostaDelegaciones = getDelegaciones()

        If Not Master.ControleErro.VerificaErro(objRespostaDelegaciones.CodigoError, objRespostaDelegaciones.NombreServidorBD, objRespostaDelegaciones.MensajeError) Then
            MyBase.MostraMensagem(objRespostaDelegaciones.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespostaDelegaciones.Delegacion.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaDelegaciones.Delegacion.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespostaDelegaciones.Delegacion)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " coddelegacion ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " coddelegacion ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespostaDelegaciones.Delegacion)
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                GdvResultado.CarregaControle(objDt)


            Else
                'Limpa a consulta
                GdvResultado.DataSource = Nothing
                GdvResultado.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True


            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    Public Sub PreencherComboDelegaciones()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboPais.Respuesta
        objRespuesta = objProxyUtilida.GetComboPais

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlPais.AppendDataBoundItems = True
        ddlPais.Items.Clear()
        ddlPais.Items.Add(New ListItem(Traduzir("013_ddl_selecione"), String.Empty))
        ddlPais.DataTextField = "Description"
        ddlPais.DataValueField = "OidPais"
        ddlPais.DataSource = objRespuesta.Pais.OrderBy(Function(b) b.Description)
        ddlPais.DataBind()

    End Sub

    Public Function getDelegacioneDetail(codigoDelegacione As String) As IAC.ContractoServicio.Delegacion.DelegacionColeccion

        Dim objProxyDelegacion As New ProxyDelegacion
        Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
        Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta

        objPeticionDelegacion.CodigoDelegacione = codigoDelegacione

        objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)

        Return objRespuestaDelegacion.Delegacion

    End Function

    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        ValidarCamposObrigatorios = True

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                If ddlPais.Visible AndAlso ddlPais.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPais.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPais.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPais.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPais.IsValid = True
                End If

            End If

        End If

        Return strErro.ToString()

    End Function
#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

        Try
            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvResultado.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("BolVigente").ToString.ToLower & ");"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função faz a ordenção do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getDelegaciones().Delegacion)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " coddelegacion ASC "
        Else
            objDT.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Configuração do estilo do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvResultado.EPager_SetCss

        'Configura o estilo dos controles presentes no pager
        CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
        CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
        CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

        CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
        CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
        CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 13/02/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("coddelegacion")) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If CBool(e.Row.DataItem("BolVigente")) Then
                CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 01/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("013_lbl_grd_codigoDel")
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("013_lbl_grd_descripcionDelegacion")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("013_lbl_grd_vigenteDelegacion")
        End If
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            txtCodDelegaciones.Text = String.Empty
            txtDesDelegaciones.Text = String.Empty
            chkVigente.Checked = True
            ddlPais.SelectedIndex = -1


            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            PreencherComboDelegaciones()

            btnCancelar_Click(Nothing, Nothing)
            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ValidarCamposObrigatorios = True

            Dim MensagemErro As String = MontaMensagensErro()

            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If

            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            Dim strCodDelegaciones As String
            Dim strCodigoPais As String
            Dim strDescricao As String

            strCodigoPais = ddlPais.SelectedValue
            strCodDelegaciones = txtCodDelegaciones.Text
            strDescricao = txtDesDelegaciones.Text.Trim

            'Filtros
            FiltroCodigoPais = strCodigoPais
            FiltroCodigoDelegaciones = strCodDelegaciones
            FiltroDescripcion = strDescricao
            FiltroVigente = chkVigente.Checked

            'Retorna os canais de acordo com o filtro aciam
            PreencherDelegaciones()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    strCodigo = GdvResultado.getValorLinhaSelecionada
                Else
                    strCodigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxy As New ProxyDelegacion
                Dim objRespuesta As IAC.ContractoServicio.Delegacion.SetDelegacion.Respuesta
                Dim objDelegacion As New ContractoServicio.Delegacion.GetDelegacion.Delegacion

                'Retorna o valor da linha selecionada no grid

                objDelegacion = Delegaciones.Where(Function(b) b.CodDelegacion = strCodigo).FirstOrDefault()

                'Criando um Termino para exclusão
                Dim objPeticion As New IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion
                objPeticion.DesZona = objDelegacion.DesZona
                objPeticion.FyhVeranoFin = objDelegacion.FyhVeranoFin
                objPeticion.FyhVeranoInicio = objDelegacion.FyhVeranoInicio
                objPeticion.CodDelegacion = objDelegacion.CodDelegacion
                objPeticion.BolVigente = False
                objPeticion.DesUsuarioModificacion = MyBase.LoginUsuario
                objPeticion.GmtModificacion = System.DateTime.Now
                objPeticion.NecGmtMinutos = objDelegacion.NecGmtMinutos
                objPeticion.NecVeranoAjuste = objDelegacion.NecVeranoAjuste
                objPeticion.OidPais = objDelegacion.OidPais
                objPeticion.DesDelegacion = objDelegacion.DesDelegacion
                objPeticion.OidDelegacion = objDelegacion.OidDelegacion
                objPeticion.CodPais = objDelegacion.CodPais

                objRespuesta = objProxy.SetDelegaciones(objPeticion)

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    If GdvResultado.Rows.Count > 0 Then
                        btnBuscar_Click(Nothing, Nothing)
                    End If
                    btnCancelar_Click(Nothing, Nothing)
                    UpdatePanelGeral.Update()
                Else

                    If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuesta.MensajeError)
                    End If

                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    'Limpar o Grid conforme seleção na combobox.
    Protected Sub ddlPais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPais.SelectedIndexChanged
        ddlPais.ToolTip = ddlPais.SelectedItem.Text

        GdvResultado.DataSource = Nothing
        GdvResultado.DataBind()

        pnlSemRegistro.Visible = False
    End Sub
#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração dos estados da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 14/02/2013 - Criado
    ''' </history>
    Public Sub ControleBotoes()

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


                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                ddlPais.SelectedIndex = -1
                txtCodDelegaciones.Text = String.Empty
                txtDesDelegaciones.Text = String.Empty

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                btnBaja.Visible = True

        End Select

    End Sub

#End Region

#Region "Métodos Formulário"
    Private Sub LimparVariaveis()
        OidDelegacion = String.Empty
        OidPais = String.Empty
        Delegacion = Nothing
        CodigoPais = String.Empty
        CodigosAjenosPeticion = Nothing
        Session.Remove("objPeticionGEPR_TDELEGACION")
        Session.Remove("_lstDelegaciones")
    End Sub
    Private Sub LimparCampos()
        LimparVariaveis()
        PreencherComboDelegacione()
        txtCodigoAjeno.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        txtCodigoDelegacion.Text = String.Empty
        txtDescricaoDelegaciones.Text = String.Empty
        txtGmtMinutos.Text = String.Empty
        txtFechaVeranoInicio.Text = String.Empty
        txtFechaVeranoFim.Text = String.Empty
        txtCantidadMinAjuste.Text = String.Empty
        txtZona.Text = String.Empty
        chkTodasDelegaciones.Checked = False

        DatosBancarios = Nothing

        lstDelegaciones.Clear()
        grid.DataBind()

        AtualizaDadosHelperCliente(ClientesPtoServ, ucClientesPtoServ)
    End Sub
    Private Sub HabilitarDesabilitarCampos(habilitar As Boolean)
        chkVigenteForm.Enabled = habilitar
        txtCodigoDelegacion.Enabled = habilitar
        txtDescricaoDelegaciones.Enabled = habilitar
        txtGmtMinutos.Enabled = habilitar
        txtFechaVeranoInicio.Enabled = habilitar
        txtFechaVeranoFim.Enabled = habilitar
        txtCantidadMinAjuste.Enabled = habilitar
        txtZona.Enabled = habilitar
        chkTodasDelegaciones.Enabled = habilitar
        divAddPtoServ.Visible = habilitar
        grid.Columns(4).Visible = habilitar
    End Sub
    Private Sub PreencherComboDelegacione()

        Dim objProxyDelegacione As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboPais.Respuesta
        objRespuesta = objProxyDelegacione.GetComboPais

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlPaisForm.AppendDataBoundItems = True
        ddlPaisForm.Items.Clear()
        ddlPaisForm.Items.Add(New ListItem(Traduzir("013_ddl_selecione"), String.Empty))
        ddlPaisForm.DataTextField = "Description"
        ddlPaisForm.DataValueField = "OidPais"
        ddlPaisForm.DataSource = objRespuesta.Pais.OrderBy(Function(b) b.Description)

        ddlPaisForm.DataBind()
    End Sub

    Private Sub CarregarGrid(buscar As Boolean)


        If buscar Then
            Genesis.LogicaNegocio.Genesis.Pantallas.Delegaciones.RecuperarInformacionesDelegacion("", lstDelegaciones, "")
        End If

        grid.DataSource = lstDelegaciones()

        grid.DataBind()

    End Sub



    Public Sub ExecutarGrabar()
        Try
            Dim objPeticion As New ContractoServicio.Pais.GetPaisDetail.Peticion
            Dim objRespuesta As New ContractoServicio.Pais.GetPaisDetail.Respuesta
            Dim objProxyPais As New Comunicacion.ProxyPaises

            Dim objProxyDelegacion As New ProxyDelegacion
            Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.SetDelegacion.Respuesta


            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErroForm(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objDelegacionPeticion As New IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objDelegacionPeticion.BolVigente = True
            Else
                objDelegacionPeticion.BolVigente = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objDelegacionPeticion.OidPais = ddlPaisForm.SelectedValue
            objDelegacionPeticion.CodDelegacion = txtCodigoDelegacion.Text.Trim
            objDelegacionPeticion.CodigoAjeno = CodigosAjenosPeticion
            objDelegacionPeticion.DesDelegacion = txtDescricaoDelegaciones.Text
            objDelegacionPeticion.NecGmtMinutos = txtGmtMinutos.Text
            objDelegacionPeticion.FyhVeranoInicio = Convert.ToDateTime(txtFechaVeranoInicio.Text)
            objDelegacionPeticion.FyhVeranoFin = Convert.ToDateTime(txtFechaVeranoFim.Text)
            objDelegacionPeticion.NecVeranoAjuste = txtCantidadMinAjuste.Text
            objDelegacionPeticion.DesZona = txtZona.Text

            objPeticion.CodigoPais = objDelegacionPeticion.OidPais
            objRespuesta = objProxyPais.GetPaisDetail(objPeticion)
            objDelegacionPeticion.CodPais = objRespuesta.Pais(0).CodPais

            objDelegacionPeticion.OidDelegacion = OidDelegacion

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objDelegacionPeticion.GmtCreacion = DateTime.Now
                objDelegacionPeticion.DesUsuarioCreacion = MyBase.LoginUsuario
            Else
                objDelegacionPeticion.GmtCreacion = Delegacion(0).GmtCreation
                objDelegacionPeticion.DesUsuarioCreacion = Delegacion(0).Des_Usuario_Create
            End If

            objDelegacionPeticion.BolTotasDelegacionesConfigRegionales = chkTodasDelegaciones.Checked
            objDelegacionPeticion.GmtModificacion = DateTime.Now
            objDelegacionPeticion.DesUsuarioModificacion = MyBase.LoginUsuario

            objDelegacionPeticion.LstClienteFacturacion = New List(Of ContractoServicio.Delegacion.SetDelegacion.ClienteFacturacion)

            For Each item In lstDelegaciones

                Dim obj = New ContractoServicio.Delegacion.SetDelegacion.ClienteFacturacion With {
                .OidClienteCapital = item.OID_CLIENTE,
                .OidSubClienteTesoreria = item.OID_SUBCLIENTE,
                .OidPtoServicioTesoreria = item.OID_PTO_SERVICIO
                }

                objDelegacionPeticion.LstClienteFacturacion.Add(obj)

            Next

            If PeticionesDatoBancario IsNot Nothing Then
                For Each item In PeticionesDatoBancario
                    If objDelegacionPeticion.PeticionDatosBancarios Is Nothing Then
                        objDelegacionPeticion.PeticionDatosBancarios = New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion
                        objDelegacionPeticion.PeticionDatosBancarios.CodigoPais = item.Value.CodigoPais
                        objDelegacionPeticion.PeticionDatosBancarios.Cultura = item.Value.Cultura
                        objDelegacionPeticion.PeticionDatosBancarios.Usuario = item.Value.Usuario
                        objDelegacionPeticion.PeticionDatosBancarios.DatosBancarios = New List(Of Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario)
                    End If
                    objDelegacionPeticion.PeticionDatosBancarios.DatosBancarios.AddRange(item.Value.DatosBancarios)

                Next
            End If

            objRespuestaDelegacion = objProxyDelegacion.SetDelegaciones(objDelegacionPeticion)

            Session.Remove("objPeticionGEPR_TDELEGACION")

            If Master.ControleErro.VerificaErro(objRespuestaDelegacion.CodigoError, objRespuestaDelegacion.NombreServidorBD, objRespuestaDelegacion.MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                If GdvResultado.Rows.Count > 0 Then
                    btnBuscar_Click(Nothing, Nothing)
                End If
                btnCancelar_Click(Nothing, Nothing)
                UpdatePanelGeral.Update()
            Else
                If objRespuestaDelegacion.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaDelegacion.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False
        Dim data1, data2 As DateTime

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o país foi enviado
                If ddlPaisForm.Visible AndAlso ddlPaisForm.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPaisForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPaisForm.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPaisForm.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPaisForm.IsValid = True
                End If

                'Verifica se o código da delegação foi enviado
                If txtCodigoDelegacion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição foi enviada
                If txtDescricaoDelegaciones.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False
                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoDelegaciones.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se os minutos para ajustes de horario de verão foi enviada 
                If txtGmtMinutos.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvGmtMinutosObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvGmtMinutosObrigatorio.IsValid = False

                    'Setar o foco no primeiro que controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtGmtMinutos.Focus()
                        focoSetado = True
                    End If
                Else
                    csvGmtMinutosObrigatorio.IsValid = True
                End If

                'Verifica se a data de inicio do horario do verão foi enviado
                If txtFechaVeranoInicio.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvFechaVeranoInicio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvFechaVeranoInicio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtFechaVeranoInicio.Focus()
                        focoSetado = True
                    End If
                Else
                    csvFechaVeranoInicio.IsValid = True
                End If

                'Verifica se a data de termino do horario de verão foi enviada
                If txtFechaVeranoFim.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvFechaVeranoFim.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvFechaVeranoFim.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtFechaVeranoFim.Focus()
                        focoSetado = True
                    End If
                Else
                    csvFechaVeranoFim.IsValid = True
                End If

                'Verifica se os minutos de ajustes foram enviados
                If txtCantidadMinAjuste.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCantiAjuste.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCantiAjuste.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCantidadMinAjuste.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCantiAjuste.IsValid = True
                End If

                'Verifica se a zona foi enviada
                If txtZona.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvZona.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvZona.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtZona.Focus()
                        focoSetado = True
                    End If
                Else
                    csvZona.IsValid = True
                End If

                If Not String.IsNullOrEmpty(txtFechaVeranoFim.Text) _
                     AndAlso Not String.IsNullOrEmpty(txtFechaVeranoInicio.Text) Then

                    data1 = CType(txtFechaVeranoInicio.Text, DateTime)
                    data2 = CType(txtFechaVeranoFim.Text, DateTime)

                    If (data1 > data2) Then
                        strErro.Append(csvFechaVeranoInicioInvalida.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        'strErro.AppendLine(Traduzir("013_msg_erro_Data"))
                        csvFechaVeranoInicioInvalida.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtFechaVeranoFim.Focus()
                            focoSetado = True
                        End If
                    Else
                        csvFechaVeranoInicioInvalida.IsValid = True
                    End If
                End If
            End If

            'Verifica se o código existe
            If (ddlPaisForm.Visible AndAlso Not ddlPaisForm.SelectedValue.Equals(String.Empty) AndAlso Not String.IsNullOrEmpty(txtCodigoDelegacion.Text)) AndAlso ExisteCodigoDelegacion(txtCodigoDelegacion.Text.Trim, ddlPaisForm.SelectedValue) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoDelegacion.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoDelegacion(codigo As String, Pais As String) As Boolean

        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxy As New ProxyDelegacion
            Dim objPeticion As New IAC.ContractoServicio.Delegacion.GetDelegacion.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

            'Verifica se o código do Termino existe no BD
            objPeticion.CodDelegacion = codigo
            objPeticion.OidPais = Pais
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.GetDelegaciones(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Delegacion.Count > 0 Then
                    Return True
                End If
            Else
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Public Sub CarregaDados(codDelegacione As String)

        Dim objDelegacion As IAC.ContractoServicio.Delegacion.DelegacionColeccion
        Dim itemSelecionado As ListItem

        objDelegacion = getDelegacioneDetail(codDelegacione)

        If objDelegacion.Count > 0 Then

            'Coloca na VState
            Delegacion = objDelegacion


            'Preenche os controles do formulario
            txtCodigoDelegacion.Text = objDelegacion(0).CodDelegacion
            txtCodigoDelegacion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).CodDelegacion, String.Empty)

            ' AJENO
            Dim iCodigoAjeno = (From item In objDelegacion(0).CodigosAjenos
                                Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            txtDescricaoDelegaciones.Text = objDelegacion(0).Description
            txtDescricaoDelegaciones.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).Description, String.Empty)

            If objDelegacion(0).Vigente Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            chkVigenteForm.Checked = objDelegacion(0).Vigente

            txtFechaVeranoFim.Text = objDelegacion(0).FhyVeraoFim.ToString("dd/MM/yyyy")
            txtFechaVeranoFim.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).FhyVeraoFim.ToString("dd/MM/yyyy"), String.Empty)

            txtFechaVeranoInicio.Text = objDelegacion(0).FhyVeraoInicio.ToString("dd/MM/yyyy")
            txtFechaVeranoInicio.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).FhyVeraoInicio.ToString("dd/MM/yyyy"), String.Empty)

            txtGmtMinutos.Text = objDelegacion(0).NecGmTminutes.ToString()
            txtGmtMinutos.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).NecGmTminutes.ToString(), String.Empty)

            txtCantidadMinAjuste.Text = objDelegacion(0).NecVeraoAjuste.ToString()
            txtGmtMinutos.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).NecGmTminutes.ToString(), String.Empty)

            txtZona.Text = objDelegacion(0).Zona
            txtZona.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objDelegacion(0).Zona, String.Empty)

            ddlPaisForm.SelectedValue = objDelegacion(0).OidPais

            EsVigente = objDelegacion(0).Vigente
            OidDelegacion = objDelegacion(0).OidDelegacion
            OidPais = objDelegacion(0).OidPais
            CodigoPais = objDelegacion(0).CodPais

            'Seleciona o valor
            itemSelecionado = ddlPaisForm.Items.FindByValue(objDelegacion(0).OidPais)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If



            Genesis.LogicaNegocio.Genesis.Pantallas.Delegaciones.RecuperarInformacionesDelegacion(objDelegacion(0).OidDelegacion, lstDelegaciones, MyBase.LoginUsuario)


            grid.DataSource = lstDelegaciones()

            grid.DataBind()


        End If

    End Sub

    Protected Sub grid_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        DefinirParametrosBase()
        TraduzirControles()
    End Sub
#End Region

#Region "Eventos Formulário"
    Private Sub ddlPaisForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaisForm.SelectedIndexChanged

        If (ddlPaisForm.SelectedValue <> Nothing) Then
            OidPais = ddlPaisForm.SelectedValue
            ddlPaisForm.ToolTip = ddlPaisForm.SelectedItem.Text
        Else
            OidPais = String.Empty
            Exit Sub
        End If

        If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
            HabilitarDesabilitarCampos(ddlPaisForm.SelectedValue <> Nothing)
        Else
            txtCodigoDelegacion.Enabled = False
        End If

    End Sub
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            HabilitarDesabilitarCampos(False)
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Alta
            ddlPaisForm.Enabled = True
            ddlPaisForm.Focus()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            HabilitarDesabilitarCampos(False)
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
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
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

#End Region

#Region "[PROPRIEDADES FORMULARIO]"

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property OidDelegacion() As String
        Get
            Return ViewState("OidDelegacion")
        End Get
        Set(value As String)
            ViewState("OidDelegacion") = value
        End Set
    End Property

    Private Property OidPais As String
        Get
            Return ViewState("OidPais")
        End Get
        Set(value As String)
            ViewState("OidPais") = value
        End Set
    End Property

    Private Property CodigoPais As String
        Get
            Return ViewState("CodigoPais")
        End Get
        Set(value As String)
            ViewState("CodigoPais") = value
        End Set
    End Property

    Private Property Delegacion As ContractoServicio.Delegacion.DelegacionColeccion
        Get
            Return DirectCast(ViewState("Delegacion"), ContractoServicio.Delegacion.DelegacionColeccion)
        End Get
        Set(value As ContractoServicio.Delegacion.DelegacionColeccion)
            ViewState("Delegacion") = value
        End Set
    End Property

    Private Property CodigosAjenosPeticion As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

#End Region


    Private Sub btnAltaAjeno_Click(sender As Object, e As EventArgs) Handles btnAltaAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoDelegacion.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoDelegaciones.Text
            tablaGenesis.OidTablaGenesis = OidDelegacion
            If Delegacion IsNot Nothing AndAlso Delegacion.Count > 0 AndAlso Delegacion.FirstOrDefault IsNot Nothing AndAlso Delegacion.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Delegacion.FirstOrDefault.CodigosAjenos
            End If

            Session("objPeticionGEPR_TDELEGACION") = tablaGenesis.CodigosAjenos
            Session("objGEPR_TDELEGACION") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TDELEGACION"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TDELEGACION"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TDELEGACION") IsNot Nothing Then
                Dim objDelegacionPeticion As New IAC.ContractoServicio.Delegacion.SetDelegacion.Peticion
                objDelegacionPeticion.CodigoAjeno = Session("objRespuestaGEPR_TDELEGACION")

                Session.Remove("objRespuestaGEPR_TDELEGACION")

                Dim iCodigoAjeno = (From item In objDelegacionPeticion.CodigoAjeno
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If objDelegacionPeticion.CodigoAjeno IsNot Nothing Then
                    CodigosAjenosPeticion = objDelegacionPeticion.CodigoAjeno
                Else
                    CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If


                Session("objPeticionGEPR_TDELEGACION") = objDelegacionPeticion.CodigoAjeno

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Modificacion

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                HabilitarDesabilitarCampos(True)
                chkVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                ddlPaisForm.Focus()
                ddlPaisForm.Enabled = True
                txtCodigoDelegacion.Enabled = False

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

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarDesabilitarCampos(False)
                ddlPaisForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Baja

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarDesabilitarCampos(False)
                ddlPaisForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnAddPtoServ_Click(sender As Object, e As EventArgs) Handles btnAddPtoServ.Click

        Try
            If ucClientesPtoServ.Clientes IsNot Nothing AndAlso
                  ucClientesPtoServ.Clientes.FirstOrDefault() IsNot Nothing AndAlso
                  ucClientesPtoServ.Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso
                  ucClientesPtoServ.Clientes.FirstOrDefault().SubClientes.FirstOrDefault() IsNot Nothing AndAlso
                  ucClientesPtoServ.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso
                  ucClientesPtoServ.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                Dim objCliente = ucClientesPtoServ.Clientes.First

                AddPuntoServicio(objCliente)

                grid.DataSource = lstDelegaciones()
                grid.DataBind()
            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


        'AddControleValidarPermissao lstDelegaciones
    End Sub
    Private Property DatosBancarios As Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
        Get
            Return ViewState("DatosBancarios")
        End Get
        Set(value As Dictionary(Of String, List(Of Comon.Clases.DatoBancario)))
            ViewState("DatosBancarios") = value
        End Set
    End Property
    Private Property PeticionesDatoBancario As Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
        Get
            Return Session("PeticionesDatoBancario")
        End Get
        Set(value As Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion))
            Session("PeticionesDatoBancario") = value
        End Set
    End Property
    Protected Sub imgDatosBancariosForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Dim item = lstDelegaciones.FirstOrDefault(Function(x) x.OID_PTO_SERVICIO = e.CommandArgument)
        ucDatosBanc.PuntoServicio.Identificador = item.OID_PTO_SERVICIO
        ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID
        ucDatosBanc.Cliente.Identificador = item.OID_CLIENTE
        ucDatosBanc.SubCliente.Identificador = item.OID_SUBCLIENTE
        'ConfigurarUsersControls()
        'Me.ucDatosBanc.Cambiar(-1)

        Try
            'If Not String.IsNullOrEmpty(GdvPtoServ.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
            'Dim id As String = String.Empty
            'If Not String.IsNullOrEmpty(GdvPtoServ.getValorLinhaSelecionada) Then
            '    id = GdvPtoServ.getValorLinhaSelecionada
            'Else
            '    id = hiddenCodigo.Value.ToString()
            'End If

            'Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

            tablaGenesis.OidCliente = item.OID_CLIENTE
            tablaGenesis.CodCliente = item.COD_CLIENTE
            tablaGenesis.DesCliente = item.DES_CLIENTE
            tablaGenesis.OidSubcliente = item.OID_SUBCLIENTE
            tablaGenesis.CodSubcliente = item.COD_SUBCLIENTE
            tablaGenesis.DesSubcliente = item.DES_SUBCLIENTE
            tablaGenesis.OidPuntoServicio = item.OID_PTO_SERVICIO
            tablaGenesis.CodPuntoServicio = item.COD_PTO_SERVICIO
            tablaGenesis.DesPuntoServicio = item.DES_PTO_SERVICIO
            If DatosBancarios Is Nothing Then
                DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
            End If
            If DatosBancarios.ContainsKey(ID) Then
                tablaGenesis.DatosBancarios = DatosBancarios(ID)
            End If

            Session("objMantenimientoCamposExtras") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtras.ClientID)

            ' End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub

    Private Sub btnCamposExtras_Click(sender As Object, e As EventArgs) Handles btnCamposExtras.Click
        Try
            If Session("objMantenimientoCamposExtras") IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCamposExtra.CamposExtras = Session("objMantenimientoCamposExtras")

                If tablaGenesis IsNot Nothing Then
                    If DatosBancarios Is Nothing Then
                        DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
                    End If
                    If DatosBancarios.ContainsKey(tablaGenesis.OidPuntoServicio) Then
                        DatosBancarios(tablaGenesis.OidPuntoServicio) = tablaGenesis.DatosBancarios
                    Else
                        DatosBancarios.Add(tablaGenesis.OidPuntoServicio, tablaGenesis.DatosBancarios)
                    End If

                    If PeticionesDatoBancario Is Nothing Then PeticionesDatoBancario = New Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
                    If PeticionesDatoBancario.ContainsKey(tablaGenesis.OidPuntoServicio) Then
                        PeticionesDatoBancario(tablaGenesis.OidPuntoServicio) = tablaGenesis.Peticion
                    Else
                        PeticionesDatoBancario.Add(tablaGenesis.OidPuntoServicio, tablaGenesis.Peticion)
                    End If

                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



    Private Sub ConfigurarUsersControls()

        ucDatosBanc.CarregaDados()
        ucDatosBanc.AtualizaGrid()

    End Sub

    Protected Sub imgExcluirForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        ' e.com

        lstDelegaciones.RemoveAll(Function(x) x.OID_PTO_SERVICIO = e.CommandArgument)
        grid.DataSource = lstDelegaciones()
        grid.DataBind()
    End Sub

    Private Sub AddPuntoServicio(objCliente As Comon.Clases.Cliente)
        Try



            Dim objSubcliente = objCliente.SubClientes.First
            Dim objPtoServicio = objSubcliente.PuntosServicio.First

            If lstDelegaciones.FirstOrDefault(Function(x) x.OID_PTO_SERVICIO = objPtoServicio.Identificador) IsNot Nothing Then

                'Return
                Dim cuentaTesoreriaEnUso As String = String.Empty
                cuentaTesoreriaEnUso = String.Format(MyBase.RecuperarValorDic("msgCuentaTesoreriaEnUsoEnDelegacion"), Me.Delegacion(0).Description)
                MyBase.MostraMensagem(cuentaTesoreriaEnUso)
            Else
                lstDelegaciones.Add(New Genesis.ContractoServicio.Contractos.Genesis.Pantallas.Delegaciones.DelegacionFacturacion With {
              .OID_DELEGACIONXCONFIG_FACTUR = Nothing,
              .OID_CLIENTE = objCliente.Identificador,
              .COD_CLIENTE = objCliente.Codigo,
              .DES_CLIENTE = objCliente.Descripcion,
              .OID_SUBCLIENTE = objSubcliente.Identificador,
              .COD_SUBCLIENTE = objSubcliente.Codigo,
              .DES_SUBCLIENTE = objSubcliente.Descripcion,
              .OID_PTO_SERVICIO = objPtoServicio.Identificador,
              .COD_PTO_SERVICIO = objPtoServicio.Codigo,
              .DES_PTO_SERVICIO = objPtoServicio.Descripcion
            })

                ClientesPtoServ.Clear()
                AtualizaDadosHelperCliente(ClientesPtoServ, ucClientesPtoServ)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub chkTodasDelegaciones_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodasDelegaciones.CheckedChanged

        If chkTodasDelegaciones.Checked Then
            Dim acaonao As String = "ExecutarClick(" & Chr(34) & btnAlertaNao.ClientID & Chr(34) & ");"
            '    Dim acaosim As String = "ExecutarClick(" & Chr(34) & btnAlertaSim.ClientID & Chr(34) & ");"

            MyBase.ExibirMensagemNaoSim(MyBase.RecuperarValorDic("alertaConfiguracionesRegionales"), "", acaonao)
        End If

    End Sub

    Protected Sub btnAlertaNao_Click(sender As Object, e As EventArgs) Handles btnAlertaNao.Click
        Try
            chkTodasDelegaciones.Checked = False
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub BusquedaDelegaciones_Load(sender As Object, e As EventArgs) Handles Me.Load
        'TraduzirControles()
    End Sub
End Class