Imports Microsoft.Web.Services3.Referral
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Médios de Pago 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 04/03/09 - Created</history>
Partial Public Class BusquedaMediosPago
    Inherits Base

#Region "[OVERRIDES]"
    Private Property ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        Get
            Return CType(ViewState("ParametroMantenimientoClientesDivisasPorPantalla"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("ParametroMantenimientoClientesDivisasPorPantalla") = value
        End Set
    End Property
    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando prescionar o enter.
        txtCodigoMedioPago.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricaoMedioPago.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlDivisa.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkListTipoMedioPago.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        txtCodigoMedioPagoForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoMedioPago.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")


    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then
                ParametroMantenimientoClientesDivisasPorPantalla = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

                'Preenche Combo
                PreencherComboDivisa()
                'Preenche CheckBoxlist
                PreencherCheckBoxListTipoMedioPago()


                'Formulário
                PreencherComboDivisaForm()
                PreencherComboTipoMedioPagoForm()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                ExecutarBusca()
                UpdatePanelGrid.Update()

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

            End If

            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            chkListTipoMedioPago.Attributes.Add("style", "margin: 0px !Important;")

            If pnForm.Visible Then
                btnNovo.Text = Traduzir("btnInserirTerminos")
                btnNovo.ToolTip = Traduzir("btnInserirTerminos")
            Else
                btnNovo.Text = Traduzir("btnAlta")
                btnNovo.ToolTip = Traduzir("btnAlta")
            End If

            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("014_titulo_pagina")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("014_lbl_subtituloscriteriosbusqueda")
        lblSubTitulosMediosPago.Text = Traduzir("014_lbl_subtitulosmediopago")
        lblCodigoMedioPago.Text = Traduzir("014_lbl_codigomediopago")
        lblDescricaoMedioPago.Text = Traduzir("014_lbl_descricaomediopago")
        lblDivisa.Text = Traduzir("014_lbl_MPdivisa")
        lblMercancia.Text = Traduzir("014_lbl_mercancia")
        lblVigente.Text = Traduzir("014_lbl_vigente")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")
        lblTipoMedioPago.Text = Traduzir("014_lbl_tipo_medio_pago")

        'Botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")

        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")


        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("014_lbl_grd_tipo_medio_pago")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("014_lbl_grd_codigo")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("014_lbl_grd_descripcion")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("014_lbl_grd_divisa")
        ProsegurGridView1.Columns(5).HeaderText = Traduzir("014_lbl_grd_vigente")

        'Formulario
        lblTituloMediosPago.Text = Traduzir("014_lbl_subtitulosmediospago")
        'Subtitulo do Formulário

        lblSubTitulosMediosPagoForm.Text = Traduzir("014_lbl_subtitulosterminomediospago")

        'Campos do formulário

        lblDivisaForm.Text = Traduzir("014_lbl_divisa")
        lblTipoMedioPagoForm.Text = Traduzir("014_lbl_tipo_medio_pago")
        lblCodigoMedioPagoForm.Text = Traduzir("014_lbl_codigomediopago")
        lblDescricaoMedioPagoForm.Text = Traduzir("014_lbl_descricaomediopago")
        lblMercanciaForm.Text = Traduzir("014_lbl_mercancia")
        lblVigenteForm.Text = Traduzir("014_lbl_vigente")
        lblObservaciones.Text = Traduzir("014_lbl_observaciones")
        lblCodigoAcceso.Text = Traduzir("014_lbl_codigoacceso")

        'GridView
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagocodigoobligatorio")
        csvTipoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvDivisaMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvCodigoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_codigomediopagoexistente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagodescripcionobligatorio")
        csvTipoMedioPagoObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagotipoobligatorio")
        csvDivisaObrigatorio.ErrorMessage = Traduzir("014_msg_mediopagodivisaobligatorio")
        csvCodigoAccesoObligatorio.ErrorMessage = Traduzir("014_msg_codigoaccesoobligatorio")
        csvCodigoAccesoExistente.ErrorMessage = Traduzir("014_msg_codigoaccesoexistente")

        'GridFormulário
        GridForm.Columns(1).HeaderText = Traduzir("014_lbl_grd_mantenimiento_codigo")
        GridForm.Columns(2).HeaderText = Traduzir("014_lbl_grd_mantenimiento_descripcion")
        GridForm.Columns(3).HeaderText = Traduzir("014_lbl_grd_mantenimiento_formato")
        GridForm.Columns(4).HeaderText = Traduzir("014_lbl_grd_mantenimiento_vigente")

    End Sub

#End Region

#Region "[PAGE EVENTS]"

#End Region

#Region "[CONFIGURACION PANTALLA]"

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroMercancia() As String
        Get
            Return ViewState("FiltroMercancia")
        End Get
        Set(value As String)
            ViewState("FiltroMercancia") = value
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

    Property FiltroCodigoDivisa() As List(Of String)
        Get
            Return ViewState("FiltroCodigoDivisa")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoDivisa") = value
        End Set
    End Property

    Property FiltroDescripcionDivisa() As List(Of String)
        Get
            Return ViewState("FiltroDescripcionDivisa")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcionDivisa") = value
        End Set
    End Property

    Property FiltroCodigoTipoMedioPago() As List(Of String)
        Get
            Return ViewState("FiltroCodigoTipoMedioPago")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigoTipoMedioPago") = value
        End Set
    End Property

    Property FiltroDescricaoTipoMedioPago() As List(Of String)
        Get
            Return ViewState("FiltroDescricaoTipoMedioPago")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescricaoTipoMedioPago") = value
        End Set
    End Property


#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboDivisa()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        objRespuesta = objProxyUtilida.GetComboDivisas

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlDivisa.AppendDataBoundItems = True
        ddlDivisa.Items.Clear()
        ddlDivisa.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlDivisa.DataTextField = "Descripcion"
        ddlDivisa.DataValueField = "CodigoIso"
        ddlDivisa.DataSource = objRespuesta.Divisas.OrderBy(Function(x) x.Descripcion)
        ddlDivisa.DataBind()
        'ddlDivisa.OrdenarPorDesc()

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

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Created
    ''' </history>
    Public Sub PreencherCheckBoxListTipoMedioPago()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposMedioPago

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        chkListTipoMedioPago.AppendDataBoundItems = True
        chkListTipoMedioPago.Items.Clear()
        chkListTipoMedioPago.DataTextField = "Descripcion"
        chkListTipoMedioPago.DataValueField = "Codigo"
        chkListTipoMedioPago.CssClass = "Lbl2"
        'chkListTipoMedioPago.Width = Unit.Percentage(100)


        chkListTipoMedioPago.DataSource = objRespuesta.TiposMedioPago
        chkListTipoMedioPago.DataBind()
        chkListTipoMedioPago.Focus()

    End Sub

    ''' <summary>
    ''' Busca os medios pago de acordo com os parametros informados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Public Function getMedioPago() As IAC.ContractoServicio.MedioPago.GetMedioPagos.Respuesta

        Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
        Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagos.Peticion
        Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagos.Respuesta

        'Recebe os valores do filtro

        'Medios de Pago
        objPeticionMedioPago.Codigo = FiltroCodigo
        objPeticionMedioPago.Descripcion = FiltroDescripcion
        objPeticionMedioPago.Vigente = FiltroVigente
        objPeticionMedioPago.EsMercancia = FiltroMercancia

        'Divisa
        objPeticionMedioPago.CodigoDivisa = FiltroCodigoDivisa
        objPeticionMedioPago.DescripcionDivisa = FiltroDescripcionDivisa

        'Tipos de Medio de Pago
        objPeticionMedioPago.CodigoTipoMedioPago = FiltroCodigoTipoMedioPago

        'Efetua a consulta
        objRespuestaMedioPago = objProxyMedioPago.GetMedioPagos(objPeticionMedioPago)

        Return objRespuestaMedioPago


    End Function

    ''' <summary>
    ''' Preenche o grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Public Sub PreencherMediosPago()
        Dim objRespostaMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagos.Respuesta

        'Busca os canais
        objRespostaMedioPago = getMedioPago()

        If Not Master.ControleErro.VerificaErro(objRespostaMedioPago.CodigoError, objRespostaMedioPago.NombreServidorBD, objRespostaMedioPago.MensajeError) Then
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaMedioPago.MedioPagos.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaMedioPago.MedioPagos.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaMedioPago.MedioPagos)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " DescripcionTipoMedioPago ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " DescripcionTipoMedioPago ASC "
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

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Adiciona javascript a linha do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript
        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função responsavel pela ordenação do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados


        Dim objDT As DataTable

        objDT = ProsegurGridView1.ConvertListToDataTable(getMedioPago().MedioPagos)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " DescripcionTipoMedioPago ASC "
        Else
            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
        End If

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Configuração do estilo do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
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
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 18

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
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Tipo de Medio Pago
            '2 - Código
            '3 - Descripción            
            '4 - Divisa
            '5 - Vigente

            Dim valor As String = Server.UrlEncode(e.Row.DataItem("codigo").ToString) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoDivisa").ToString) & "$#"
            valor &= Server.UrlEncode(e.Row.DataItem("CodigoTipoMedioPago").ToString)

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).ToolTip = Traduzir("btnDuplicar")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If Acao = Utilidad.eAcao.Busca AndAlso Not ParametroMantenimientoClientesDivisasPorPantalla Then
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Enabled = False
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Visible = False
                CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).Enabled = False
                CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).Visible = False
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Enabled = False
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).Visible = False
            End If

            If CBool(e.Row.DataItem("vigente")) Then
                CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

        End If
    End Sub


#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Preenche Combo e Checkboxlist novamente
            PreencherComboDivisa()
            PreencherCheckBoxListTipoMedioPago()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            btnCancelar_Click(Nothing, Nothing)

            ddlDivisa.ToolTip = ddlDivisa.SelectedItem.Text
            chkListTipoMedioPago.Focus()

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
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
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub
    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)

        Dim strListCodigoDivisa As New List(Of String)
        Dim strListDescripcionDivisa As New List(Of String)

        Dim strListCodigoTipoMedioPago As New List(Of String)
        Dim strListDescripcionTipoMedioPago As New List(Of String)

        '#### Monta as listas de filtro ###

        'Codigo Medio Pago
        strListCodigo.Add(txtCodigoMedioPago.Text.Trim.ToUpper)
        'Descrição Medio Pago
        strListDescricao.Add(txtDescricaoMedioPago.Text.Trim.ToUpper)

        'Divisa (Código e Descrição)
        If ddlDivisa.SelectedItem.Value <> String.Empty Then
            strListCodigoDivisa.Add(ddlDivisa.SelectedItem.Value)
            strListDescripcionDivisa.Add(ddlDivisa.SelectedItem.Text)
        End If

        'Tipo de Medio Pago (Código e Descrição)
        If chkListTipoMedioPago.Items.Count > 0 Then
            For Each item As ListItem In chkListTipoMedioPago.Items
                'Adiciona no lista
                If item.Selected Then
                    strListCodigoTipoMedioPago.Add(item.Value)
                    strListDescripcionTipoMedioPago.Add(item.Text)
                End If
            Next
        End If

        'Associando os Filtros nas propriedade
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroMercancia = chkMercancia.Checked
        FiltroVigente = chkVigente.Checked

        'Divisa
        FiltroCodigoDivisa = strListCodigoDivisa
        FiltroDescripcionDivisa = strListDescripcionDivisa

        'Tipo Medio Pago
        FiltroCodigoTipoMedioPago = strListCodigoTipoMedioPago
        FiltroDescricaoTipoMedioPago = strListDescripcionTipoMedioPago


        'Retorna os medio de pago de acordo com o filtro acima
        PreencherMediosPago()

        chkListTipoMedioPago.Focus()

       
    End Sub


    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
            Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.SetMedioPago.Respuesta

            'Retorna o valor da linha selecionada no grid

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            '[0] - Codigo Médio Pago
            '[1] - Codigo Divisa
            '[2] - Codigo TipoMédio Pago
            Dim strCodigo As String = Codigos(0)
            Dim strCodigoDivisa As String = Codigos(1)
            Dim strCodigoTipoMedioPago As String = Codigos(2)

            'Criando um MedioPago para exclusão
            Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.Peticion
            Dim objMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.MedioPago
            objMedioPago.Codigo = strCodigo
            objMedioPago.CodigoDivisa = strCodigoDivisa
            objMedioPago.CodigoTipoMedioPago = strCodigoTipoMedioPago
            objMedioPago.Vigente = False

            objPeticionMedioPago.MedioPagos = New ContractoServicio.MedioPago.SetMedioPago.MedioPagoColeccion

            'Passando para Petição
            objPeticionMedioPago.MedioPagos.Add(objMedioPago)
            objPeticionMedioPago.CodigoUsuario = MyBase.LoginUsuario

            'Exclui a petição
            objRespuestaMedioPago = objProxyMedioPago.SetMedioPago(objPeticionMedioPago)

            If Master.ControleErro.VerificaErro(objRespuestaMedioPago.CodigoError, objRespuestaMedioPago.NombreServidorBD, objRespuestaMedioPago.MensajeError) Then

                If objRespuestaMedioPago.RespuestaMedioPagos.Count > 0 Then

                    If objRespuestaMedioPago.RespuestaMedioPagos(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError)
                        Exit Sub
                    End If

                End If

                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                UpdatePanelGrid.Update()
                btnCancelar_Click(sender, e)
                ExecutarBusca()
                btnSalvar.Visible = True

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    '''Concatenação de parametros da linha do grid selecionada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Public Function FormataParametros(pValorLinhaSeleciona As String) As String

        Dim Retorno As String() = pValorLinhaSeleciona.Replace("$#", "|").Split("|")
        Dim strParamretorno As String = String.Empty

        If Retorno.Length >= 3 Then
            strParamretorno = String.Format("codMedioPago={0}&codDivisaMedioPago={1}&codTipoMedioPago={2}", Server.UrlEncode(Retorno(0)), Server.UrlEncode(Retorno(1)), Server.UrlEncode(Retorno(2)))
        End If

        Return strParamretorno


    End Function

#End Region

    Private Sub ddlDivisa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDivisa.SelectedIndexChanged

        ddlDivisa.ToolTip = ddlDivisa.SelectedItem.Text
        ddlDivisa.Focus()

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração dos estados da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 04/03/2009 - Criado
    ''' </history>
    Public Sub ControleBotoes()
        ' parámetro MantenimientoClientesDivisasPorPantalla = Falso –, la pantalla permitirá solamente:
        '-	Consulta y 
        '-	Modificación solamente de los códigos ajenos, totalizadores de saldo y el tipo del Cliente.
        If ParametroMantenimientoClientesDivisasPorPantalla Then
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


                    chkMercancia.Checked = False
                    chkVigente.Checked = True
                    'chkListTipoMedioPago.Focus()
                    pnlSemRegistro.Visible = False
                    txtCodigoMedioPago.Text = String.Empty
                    txtDescricaoMedioPago.Text = String.Empty

                Case Aplicacao.Util.Utilidad.eAcao.Busca



                    ' Define o foco para o controle Tipo Medio Pago
                    'chkListTipoMedioPago.Focus()

            End Select
        Else
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.NoAction


                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial


                    chkMercancia.Checked = False
                    chkVigente.Checked = True
                    'chkListTipoMedioPago.Focus()
                    pnlSemRegistro.Visible = False
                    txtCodigoMedioPago.Text = String.Empty
                    txtDescricaoMedioPago.Text = String.Empty

                Case Aplicacao.Util.Utilidad.eAcao.Busca


                    ' Define o foco para o controle Tipo Medio Pago
                    'chkListTipoMedioPago.Focus()

            End Select
        End If



    End Sub

#End Region
#Region "[Propriedades Fomulario]"
    Public Property TerminoMediosPagoTemporario() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
        Get
            If ViewState("TerminoMediosPagoTemporario") Is Nothing Then
                ViewState("TerminoMediosPagoTemporario") = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
            End If

            Return DirectCast(ViewState("TerminoMediosPagoTemporario"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
            ViewState("TerminoMediosPagoTemporario") = value
        End Set
    End Property
    Private Sub SetTerminosMedioPagoColecaoPopUp()

        'Passa a coleção com o objeto temporario de Terminos
        Session("colTerminosMedioPago") = TerminoMediosPagoTemporario

    End Sub
    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
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
    Private Property CodigoAccesoExistente() As Boolean
        Get
            Return ViewState("CodigoAccesoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoAccesoExistente") = value
        End Set
    End Property
    Public Property TerminoMediosPagoClone() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
        Get
            If ViewState("TerminoMediosPagoClone") Is Nothing Then
                ViewState("TerminoMediosPagoClone") = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
            End If

            Return DirectCast(ViewState("TerminoMediosPagoClone"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
            ViewState("TerminoMediosPagoClone") = value
            ViewState("TerminoMediosPagoTemporario") = value
        End Set
    End Property
    Private Property EsMercancia() As Boolean
        Get
            If ViewState("EsMercancia") Is Nothing Then
                ViewState("EsMercancia") = False
            End If
            Return ViewState("EsMercancia")
        End Get
        Set(value As Boolean)
            ViewState("EsMercancia") = value
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
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Private Property CodigoAccesoAtual() As String
        Get
            Return ViewState("CodigoAccesoAtual")
        End Get
        Set(value As String)
            ViewState("CodigoAccesoAtual") = value.Trim
        End Set
    End Property
#End Region
#Region "[Métodos do formulário]"
    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboDivisaForm()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        objRespuesta = objProxyUtilida.GetComboDivisas

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlDivisaForm.AppendDataBoundItems = True
        ddlDivisaForm.Items.Clear()
        ddlDivisaForm.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlDivisaForm.DataTextField = "Descripcion"
        ddlDivisaForm.DataValueField = "CodigoIso"
        ddlDivisaForm.DataSource = objRespuesta.Divisas
        ddlDivisaForm.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboTipoMedioPagoForm()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposMedioPago.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposMedioPago

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlTipoMedioPago.AppendDataBoundItems = True
        ddlTipoMedioPago.Items.Clear()
        ddlTipoMedioPago.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlTipoMedioPago.DataTextField = "Descripcion"
        ddlTipoMedioPago.DataValueField = "Codigo"
        ddlTipoMedioPago.DataSource = objRespuesta.TiposMedioPago
        ddlTipoMedioPago.DataBind()

    End Sub

#End Region
#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridForm_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridForm.EOnClickRowClientScript
        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridForm_EPreencheDados(sender As Object, e As EventArgs) Handles GridForm.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView1.ConvertListToDataTable(TerminoMediosPagoTemporario)

            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand

            ProsegurGridView1.ControleDataSource = objDT

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' Configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridForm_EPager_SetCss(sender As Object, e As EventArgs) Handles GridForm.EPager_SetCss
        Try
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

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try


    End Sub

    ''' <summary>
    ''' RowDataBound do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridForm_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridForm.RowDataBound

        Try

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Formato
            '4 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigoForm.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & _btnBajaTermino.ClientID & "');"
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).ToolTip = Traduzir("btnBaja")


                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then
                    CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).Enabled = False
                    CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).Enabled = False
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCreated do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GridForm_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridForm.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region
#Region "[Eventos Formulario]"
    Public Sub ExecutarAlta()
        Try


            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codMedioPago=" & txtCodigoMedioPago.Text.Trim()

            'Seta a session com a coleção de TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminosMedioPagoColecaoPopUp()

            ' limpar sessao das telas seguintes
            Session("objColValorTerminoMedioPago") = Nothing

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("014_titulo_mantenimiento_termino_mediospago"), 400, 900, False, True, btnConsomeTerminoMedioPago.ClientID)


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub
#End Region
#Region "[Metodos apos poup]"
    Public Sub ConsomeTerminoMedioPago()

        If Session("objTerminoMedioPago") IsNot Nothing Then

            Dim objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
            objTerminoMedioPago = DirectCast(Session("objTerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)

            'Se existe o TerminoMedioPago na coleção
            If Not VerificarTerminoMedioPagoExiste(TerminoMediosPagoTemporario, objTerminoMedioPago.Codigo) Then
                TerminoMediosPagoTemporario.Add(objTerminoMedioPago)
            Else
                ModificaTerminoMedioPago(TerminoMediosPagoTemporario, objTerminoMedioPago)
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If

            'cria um objeto temporario somente para preencher o grid, pois o metodo que faz a conversão da coleção de terminos para um datatable
            'não aceita variavel inteira como nothing.
            Dim objColTerminos As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion

            objColTerminos = TerminoMediosPagoTemporario

            If objColTerminos IsNot Nothing AndAlso objColTerminos.Count > 0 Then

                For Each objTermino In objColTerminos
                    If Not objTermino.Longitud.HasValue Then
                        objTermino.Longitud = 0
                    End If
                Next

            End If

            'Carrega os TerminosMedioPago no GridView
            Dim objDT As DataTable
            objDT = GridForm.ConvertListToDataTable(objColTerminos)
            GridForm.CarregaControle(objDT)

            If objColTerminos.Count > 0 Then
                pnBotoesGrid.Visible = True
            Else
                pnBotoesGrid.Visible = False
            End If

            Session("objTerminoMedioPago") = Nothing

        End If

    End Sub
    Private Function VerificarTerminoMedioPagoExiste(objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, codigoTerminoMedioPago As String) As Boolean

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = codigoTerminoMedioPago

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' ' Retorna uma coleção de terminos que tem a mais no objeto temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
    ''' </summary>
    ''' <param name="objTerminoMediosPagoTemporario"></param>
    ''' <param name="objTerminoMediosPagoClone"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaTerminosMediosPagoEnvio(objTerminoMediosPagoTemporario As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, objTerminoMediosPagoClone As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion) As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        Dim retorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion = objTerminoMediosPagoTemporario

        Dim objTerminoMedioPagoCol As New IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPagoColeccion

        Dim objTerminoMedioPagoEnvio As IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago
        For Each objRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In retorno

            'Cria o objeto de termino de envio
            objTerminoMedioPagoEnvio = New IAC.ContractoServicio.MedioPago.SetMedioPago.TerminoMedioPago
            objTerminoMedioPagoEnvio.Codigo = objRetorno.Codigo
            objTerminoMedioPagoEnvio.Descripcion = objRetorno.Descripcion
            objTerminoMedioPagoEnvio.Observacion = objRetorno.Observacion
            objTerminoMedioPagoEnvio.Vigente = objRetorno.Vigente

            objTerminoMedioPagoEnvio.Longitud = objRetorno.Longitud
            objTerminoMedioPagoEnvio.MostrarCodigo = objRetorno.MostrarCodigo
            objTerminoMedioPagoEnvio.ValorInicial = objRetorno.ValorInicial
            objTerminoMedioPagoEnvio.OrdenTermino = objRetorno.OrdenTermino

            objTerminoMedioPagoEnvio.CodigoFormato = objRetorno.CodigoFormato
            objTerminoMedioPagoEnvio.CodigoMascara = objRetorno.CodigoMascara
            objTerminoMedioPagoEnvio.CodigoAlgoritmo = objRetorno.CodigoAlgoritmo

            If objRetorno.ValoresTermino IsNot Nothing Then
                objTerminoMedioPagoEnvio.ValoresTermino = New ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion

                Dim objValorEnvio As ContractoServicio.MedioPago.SetMedioPago.ValorTermino
                For Each objValor As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTermino In objRetorno.ValoresTermino
                    'Cria o objeto de valor de envio
                    objValorEnvio = New ContractoServicio.MedioPago.SetMedioPago.ValorTermino
                    objValorEnvio.Codigo = objValor.Codigo
                    objValorEnvio.Descripcion = objValor.Descripcion
                    objValorEnvio.Vigente = objValor.Vigente

                    'Adiciona no termino de envio
                    objTerminoMedioPagoEnvio.ValoresTermino.Add(objValorEnvio)
                Next

            End If

            'Adiciona na coleção
            objTerminoMedioPagoCol.Add(objTerminoMedioPagoEnvio)
        Next

        Return objTerminoMedioPagoCol

    End Function

    ''' <summary>
    ''' Modifica um Medio de Pago existe na coleção informada
    ''' </summary>
    ''' <param name="objTerminoMediosPago"></param>
    ''' <param name="objTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModificaTerminoMedioPago(ByRef objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago) As Boolean

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = objTerminoMedioPago.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objTerminoMedioPagoTmp As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)

            'Campos Texto
            objTerminoMedioPagoTmp.Descripcion = objTerminoMedioPago.Descripcion
            objTerminoMedioPagoTmp.Observacion = objTerminoMedioPago.Observacion
            objTerminoMedioPagoTmp.ValorInicial = objTerminoMedioPago.ValorInicial
            objTerminoMedioPagoTmp.Longitud = objTerminoMedioPago.Longitud
            'Campos CheckBox            
            objTerminoMedioPagoTmp.MostrarCodigo = objTerminoMedioPago.MostrarCodigo
            objTerminoMedioPagoTmp.Vigente = objTerminoMedioPago.Vigente
            'Campos DropDownList
            objTerminoMedioPagoTmp.CodigoFormato = objTerminoMedioPago.CodigoFormato
            objTerminoMedioPagoTmp.CodigoMascara = objTerminoMedioPago.CodigoMascara
            objTerminoMedioPagoTmp.CodigoAlgoritmo = objTerminoMedioPago.CodigoAlgoritmo
            objTerminoMedioPagoTmp.DescripcionFormato = objTerminoMedioPago.DescripcionFormato
            objTerminoMedioPagoTmp.DescripcionAlgoritmo = objTerminoMedioPago.DescripcionAlgoritmo
            objTerminoMedioPagoTmp.DescripcionMascara = objTerminoMedioPago.DescripcionMascara
            'Demais Campos
            objTerminoMedioPagoTmp.OrdenTermino = objTerminoMedioPago.OrdenTermino

            'Valores de Termino
            objTerminoMedioPagoTmp.ValoresTermino = objTerminoMedioPago.ValoresTermino

            Return True
        End If

    End Function

    ''' <summary>
    ''' Retorna um TerminoMedioPago da coleção informada    
    ''' </summary>
    ''' <param name="objTerminoMediosPago"></param>
    ''' <param name="codigoTerminoMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornaTerminoMedioPagoExiste(ByRef objTerminoMediosPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion, codigoTerminoMedioPago As String) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago

        Dim retorno = From c In objTerminoMediosPago Where c.Codigo = codigoTerminoMedioPago

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function
    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum
    Private Sub btnConsomeTerminoMedioPago_Click(sender As Object, e As EventArgs) Handles btnConsomeTerminoMedioPago.Click
        Try
            ConsomeTerminoMedioPago()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[Metodos/Eventos Grid Denominações]"

    Private Sub SetTerminoMedioPagoSelecionadoPopUp()
        If Not String.IsNullOrEmpty(GridForm.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
            Dim strCodigoSelecionado As String = String.Empty
            If Not String.IsNullOrEmpty(GridForm.getValorLinhaSelecionada) Then
                strCodigoSelecionado = GridForm.getValorLinhaSelecionada
            Else
                strCodigoSelecionado = hiddenCodigoForm.Value.ToString()
            End If
            'Cria o MedioPago para ser consumido na página de TerminosMedioPago
            Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
            objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, Server.UrlDecode(strCodigoSelecionado))

            'Envia o TerminoMedioPago para ser consumido pela PopUp de Mantenimento de TerminoMedioPago
            Session("setTerminoMedioPago") = objTerminoMedioPago
        End If
    End Sub
    Public Sub ExecutarConsulta()
        Try
            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            'Seta a session com o TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminoMedioPagoSelecionadoPopUp()

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("014_titulo_mantenimiento_termino_mediospago"), 400, 900, False, True, btnConsomeTerminoMedioPago.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do Botão Modificacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarModificacion()
        Try

            Dim url As String = "MantenimientoTerminoMediosPago.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion

            'Seta a session com o TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminoMedioPagoSelecionadoPopUp()

            'Seta a session com a coleção de TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetTerminosMedioPagoColecaoPopUp()

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("014_titulo_mantenimiento_termino_mediospago"), 400, 900, False, True, btnConsomeTerminoMedioPago.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Public Sub ExecutarBaja()
        Try
            If Not String.IsNullOrEmpty(GridForm.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim strCodigoSelecionado As String = String.Empty
                If Not String.IsNullOrEmpty(GridForm.getValorLinhaSelecionada) Then
                    strCodigoSelecionado = GridForm.getValorLinhaSelecionada
                Else
                    strCodigoSelecionado = hiddenCodigoForm.Value.ToString()
                End If

                'Retorna o valor da linha selecionada no grid
                Dim strCodigo As String = Server.UrlDecode(strCodigoSelecionado)

                'Modifica o Termino Medio de Pago para exclusão
                Dim objTerminoRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, strCodigo)
                objTerminoRetorno.Vigente = False

                'Carrega os Terminos no GridView
                TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
                Dim objDT As DataTable
                objDT = GridForm.ConvertListToDataTable(TerminoMediosPagoTemporario)
                GridForm.CarregaControle(objDT)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Public Function OrdenaColecao(objColecao As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion

        Dim retorno = From objeto In objColecao Order By objeto.OrdenTermino
        Dim objTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim objColTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion = Nothing

        For Each objRetorno As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In retorno


            'Cria o objeto de termino de envio
            objTerminoMedioPago = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
            objTerminoMedioPago.Codigo = objRetorno.Codigo
            objTerminoMedioPago.Descripcion = objRetorno.Descripcion
            objTerminoMedioPago.Observacion = objRetorno.Observacion
            objTerminoMedioPago.Vigente = objRetorno.Vigente

            objTerminoMedioPago.Longitud = objRetorno.Longitud
            objTerminoMedioPago.MostrarCodigo = objRetorno.MostrarCodigo
            objTerminoMedioPago.ValorInicial = objRetorno.ValorInicial
            objTerminoMedioPago.OrdenTermino = objRetorno.OrdenTermino

            objTerminoMedioPago.CodigoFormato = objRetorno.CodigoFormato
            objTerminoMedioPago.DescripcionFormato = objRetorno.DescripcionFormato

            objTerminoMedioPago.CodigoMascara = objRetorno.CodigoMascara
            objTerminoMedioPago.DescripcionMascara = objRetorno.DescripcionMascara

            objTerminoMedioPago.CodigoAlgoritmo = objRetorno.CodigoAlgoritmo
            objTerminoMedioPago.DescripcionAlgoritmo = objRetorno.DescripcionAlgoritmo

            objTerminoMedioPago.ValoresTermino = objRetorno.ValoresTermino

            If objColTerminoMedioPago Is Nothing Then
                objColTerminoMedioPago = New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
                objColTerminoMedioPago.Add(objTerminoMedioPago)
            Else
                objColTerminoMedioPago.Add(objTerminoMedioPago)
            End If
        Next

        Return objColTerminoMedioPago

    End Function
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
            Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.Peticion
            Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.SetMedioPago.Respuesta


            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If


            Dim objMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.MedioPago

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objMedioPago.Vigente = True
            Else
                objMedioPago.Vigente = chkVigenteForm.Checked
            End If

            objMedioPago.Codigo = txtCodigoMedioPagoForm.Text.Trim
            objMedioPago.Descripcion = txtDescricaoMedioPagoForm.Text.Trim
            objMedioPago.Observaciones = txtObservaciones.Text
            objMedioPago.CodigoDivisa = ddlDivisaForm.SelectedValue
            objMedioPago.CodigoTipoMedioPago = ddlTipoMedioPago.SelectedValue
            objMedioPago.CodigoAccesoMedioPago = txtCodigoAcceso.Text.Trim()
            objMedioPago.TerminosMedioPago = RetornaTerminosMediosPagoEnvio(TerminoMediosPagoTemporario, TerminoMediosPagoClone)
            objMedioPago.EsMercancia = chkMercanciaForm.Checked

            'Cria a coleção
            Dim objColMedioPago As New IAC.ContractoServicio.MedioPago.SetMedioPago.MedioPagoColeccion
            objColMedioPago.Add(objMedioPago)

            objPeticionMedioPago.MedioPagos = objColMedioPago
            objPeticionMedioPago.CodigoUsuario = MyBase.LoginUsuario

            objRespuestaMedioPago = objProxyMedioPago.SetMedioPago(objPeticionMedioPago)

            'Define a ação de busca somente se houve retorno de MediosPago 
            If Master.ControleErro.VerificaErro(objRespuestaMedioPago.CodigoError, objRespuestaMedioPago.NombreServidorBD, objRespuestaMedioPago.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaMedioPago.RespuestaMedioPagos(0).CodigoError, objRespuestaMedioPago.NombreServidorBD, objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError)
                End If

            Else

                If objRespuestaMedioPago.RespuestaMedioPagos IsNot Nothing _
                    AndAlso objRespuestaMedioPago.RespuestaMedioPagos.Count > 0 _
                    AndAlso objRespuestaMedioPago.RespuestaMedioPagos(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    MyBase.MostraMensagem(objRespuestaMedioPago.RespuestaMedioPagos(0).MensajeError)

                ElseIf objRespuestaMedioPago.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                    MyBase.MostraMensagem(objRespuestaMedioPago.MensajeError)

                Else
                    MyBase.MostraMensagem(objRespuestaMedioPago.MensajeError)
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                'Verifica se o campo é obrigatório
                'quando o botão salvar é acionado
                'Verifica o tipo medio pago
                If ddlTipoMedioPago.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoMedioPagoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoMedioPagoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoMedioPagoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If ddlDivisaForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDivisaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDivisaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDivisaForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDivisaObrigatorio.IsValid = True
                End If

                'Verifica se o código do canal é obrigatório
                If txtCodigoMedioPagoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoMedioPagoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoMedioPagoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoMedioPagoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtCodigoAcceso.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoAccesoObligatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoAccesoObligatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoAcceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoAccesoObligatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoMedioPago(txtCodigoMedioPagoForm.Text.Trim(), ddlDivisaForm.SelectedValue.Trim(), ddlTipoMedioPago.SelectedValue.Trim()) Then

                strErro.Append(csvCodigoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoMedioPagoExistente.IsValid = False
                csvDivisaMedioPagoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoMedioPagoForm.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoMedioPagoExistente.IsValid = True
                csvTipoMedioPagoExistente.IsValid = True
                csvDivisaMedioPagoExistente.IsValid = True
            End If

            'Verifica se o código existe
            If ExisteCodigoAccesoMedioPago(txtCodigoAcceso.Text.Trim(), ddlDivisaForm.SelectedValue.Trim()) Then

                strErro.Append(csvCodigoAccesoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAccesoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAcceso.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAccesoExistente.IsValid = True
            End If
            'If CodigoExistente Then

            '    strErro.Append(csvTipoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)


            '    'Seta o foco no primeiro controle que deu erro
            '    If SetarFocoControle AndAlso Not focoSetado Then
            '        ddlTipoMedioPago.Focus()
            '        focoSetado = True
            '    End If

            'Else

            'End If

            'If CodigoExistente Then

            '    strErro.Append(csvDivisaMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)


            '    'Seta o foco no primeiro controle que deu erro
            '    If SetarFocoControle AndAlso Not focoSetado Then
            '        ddlDivisa.Focus()
            '        focoSetado = True
            '    End If

            'Else

            'End If

        End If

        Return strErro.ToString
    End Function

    Private Function ExisteCodigoMedioPago(codigoMedioPago As String, divisa As String, tipoMedioPago As String) As Boolean

        If Acao = Utilidad.eAcao.Modificacion Then
            Return False
        End If
        ' Se o tipo medio pago deve ser considerado como obrigatório
        Dim blTipoMedioPagoPreenchido As Boolean = True
        If tipoMedioPago IsNot Nothing Then
            blTipoMedioPagoPreenchido = Not String.IsNullOrEmpty(ddlTipoMedioPago.SelectedValue)
        End If

        If Not String.IsNullOrEmpty(txtCodigoMedioPagoForm.Text.Trim()) AndAlso _
          Not String.IsNullOrEmpty(ddlDivisaForm.SelectedValue) AndAlso _
          blTipoMedioPagoPreenchido Then

            Dim objRespostaVerificarCodigoMedioPago As IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Respuesta
            Try

                Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
                Dim objPeticionVerificarCodigoMedioPago As New IAC.ContractoServicio.MedioPago.VerificarCodigoMedioPago.Peticion

                'Verifica se o código do MedioPago existe no BD
                objPeticionVerificarCodigoMedioPago.Codigo = codigoMedioPago.Trim
                objPeticionVerificarCodigoMedioPago.Divisa = divisa.Trim

                'Verifica se o tipo medio pago vai fazer parte da chave de busca
                If tipoMedioPago IsNot Nothing Then
                    objPeticionVerificarCodigoMedioPago.Tipo = tipoMedioPago.Trim
                End If

                objRespostaVerificarCodigoMedioPago = objProxyMedioPago.VerificarCodigoMedioPago(objPeticionVerificarCodigoMedioPago)

                If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoMedioPago.CodigoError, objRespostaVerificarCodigoMedioPago.NombreServidorBD, objRespostaVerificarCodigoMedioPago.MensajeError) Then
                    Return objRespostaVerificarCodigoMedioPago.Existe
                Else
                    MyBase.MostraMensagem(objRespostaVerificarCodigoMedioPago.MensajeError)
                    Return False
                End If


            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If

    End Function
    Private Function ExisteCodigoAccesoMedioPago(codigoAccesoMedioPago As String, divisa As String) As Boolean

        If Acao = Utilidad.eAcao.Modificacion Then
            Return False
        End If

        If Not String.IsNullOrEmpty(txtCodigoAcceso.Text.Trim()) AndAlso _
          Not String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then

            Dim objRespostaVerificarCodigoAccesoMedioPago As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarCodigoAccesoMedioPago As New IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoMedioPago.Peticion

                'Verifica se o código do MedioPago existe no BD
                objPeticionVerificarCodigoAccesoMedioPago.CodigoAcceso = codigoAccesoMedioPago.Trim
                objPeticionVerificarCodigoAccesoMedioPago.CodigoDivisa = divisa.Trim

                objRespostaVerificarCodigoAccesoMedioPago = objProxyUtilidad.VerificarCodigoAccesoMedioPago(objPeticionVerificarCodigoAccesoMedioPago)

                If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAccesoMedioPago.CodigoError, objRespostaVerificarCodigoAccesoMedioPago.NombreServidorBD, objRespostaVerificarCodigoAccesoMedioPago.MensajeError) Then
                    Return objRespostaVerificarCodigoAccesoMedioPago.Existe
                Else
                    MyBase.MostraMensagem(objRespostaVerificarCodigoAccesoMedioPago.MensajeError)
                    Return False
                End If


            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        End If

    End Function

    Public Sub CarregaDadosForm(codMedioPago As String, codDivisaMedioPago As String, codTipoMedioPago As String)
        Dim itemSelecionado As ListItem
        Dim objColMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion
        objColMedioPago = getMedioPagoDetail(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

        If objColMedioPago.Count > 0 Then

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then
                txtCodigoMedioPagoForm.Text = objColMedioPago(0).Codigo
                txtCodigoMedioPagoForm.ToolTip = objColMedioPago(0).Codigo
                txtCodigoAcceso.Text = objColMedioPago.First.CodigoAccesoMedioPago
                txtCodigoAcceso.ToolTip = objColMedioPago.First.CodigoAccesoMedioPago
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                txtCodigoMedioPagoForm.Text = String.Empty
                txtCodigoMedioPagoForm.Text = String.Empty
            End If

            txtDescricaoMedioPagoForm.Text = objColMedioPago(0).Descripcion
            txtDescricaoMedioPagoForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColMedioPago(0).Descripcion, String.Empty)

            txtObservaciones.Text = objColMedioPago(0).Observaciones
            chkMercanciaForm.Checked = objColMedioPago(0).EsMercancia
            chkVigenteForm.Checked = objColMedioPago(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColMedioPago(0).Vigente
            EsMercancia = objColMedioPago(0).EsMercancia

            'Seleciona o valor divisa
            itemSelecionado = ddlDivisaForm.Items.FindByValue(objColMedioPago(0).CodigoDivisa)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlDivisaForm.ToolTip = itemSelecionado.ToString
            End If

            'Seleciona o valor tipo de medio de pago
            itemSelecionado = ddlTipoMedioPago.Items.FindByValue(objColMedioPago(0).CodigoTipoMedioPago)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoMedioPago.ToolTip = itemSelecionado.ToString
            End If

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoMedioPagoForm.Text
                CodigoAccesoAtual = txtCodigoAcceso.Text
            End If

            'Faz um clone da coleção de MedioPago e Cria o objeto temporario no mesmo instante
            TerminoMediosPagoClone = OrdenaColecao(objColMedioPago(0).TerminosMedioPago)

            'Carrega os TerminosMedioPago no GridView            
            Dim objDT As DataTable
            objDT = GridForm.ConvertListToDataTable(TerminoMediosPagoClone)
            GridForm.CarregaControle(objDT)

            If TerminoMediosPagoClone.Count > 0 AndAlso (Acao <> Utilidad.eAcao.Consulta AndAlso Acao <> Utilidad.eAcao.Baja) Then

                pnBotoesGrid.Visible = True
                btnAcima.Visible = True
                btnAbaixo.Visible = True

                If Acao <> Utilidad.eAcao.Consulta AndAlso Acao <> Utilidad.eAcao.Baja Then
                    btnAcima.Enabled = True            '8
                    btnAbaixo.Enabled = True
                Else
                    btnAcima.Enabled = False            '8
                    btnAbaixo.Enabled = False
                End If

            Else
                pnBotoesGrid.Visible = False
                btnAcima.Visible = False
                btnAbaixo.Visible = False
            End If
        End If


    End Sub
#End Region
#Region "[DADOS]"

    Public Function getMedioPagoDetail(codigoMedioPago As String, codigoDivisa As String, codigoTipoMedioPago As String) As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.MedioPagoColeccion

        Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
        Dim objPeticionMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion
        Dim objRespuestaMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

        'Recebe os valores do filtro
        Dim listaCodigoMedioPago As New List(Of String)
        listaCodigoMedioPago.Add(codigoMedioPago)

        Dim listaCodigoDivisaMedioPago As New List(Of String)
        listaCodigoDivisaMedioPago.Add(codigoDivisa)

        Dim listaCodigoTipoMedioPago As New List(Of String)
        listaCodigoTipoMedioPago.Add(codigoTipoMedioPago)

        objPeticionMedioPago.CodigoMedioPago = listaCodigoMedioPago
        objPeticionMedioPago.CodigoIsoDivisa = listaCodigoDivisaMedioPago
        objPeticionMedioPago.CodigoTipoMedioPago = listaCodigoTipoMedioPago

        objRespuestaMedioPago = objProxyMedioPago.GetMedioPagoDetail(objPeticionMedioPago)

        Return objRespuestaMedioPago.MedioPagos

    End Function

#End Region
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            If pnForm.Visible Then
                ExecutarAlta()
                ' BloquearDesbloquearCamposForm(True)
            Else

                Acao = Utilidad.eAcao.Alta
                LimparCamposForm()
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                btnCancelar.Visible = True
                btnSalvar.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitarCamposForm(True)
                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False
                ddlTipoMedioPago.Focus()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try

            Acao = Utilidad.eAcao.Inicial
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
            HabilitarDesabilitarCamposForm(True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub LimparCamposForm()

        GridForm.DataSource = Nothing
        GridForm.DataBind()
        pnBotoesGrid.Visible = False
        TerminoMediosPagoTemporario = Nothing
        PreencherComboDivisaForm()
        PreencherComboTipoMedioPagoForm()
        txtCodigoMedioPagoForm.Text = String.Empty
        txtDescricaoMedioPagoForm.Text = String.Empty
        txtObservaciones.Text = String.Empty
        txtCodigoAcceso.Text = String.Empty
        chkMercanciaForm.Checked = False
        chkVigenteForm.Checked = True

    End Sub

    Private Sub HabilitarDesabilitarCamposForm(habilitar As Boolean)
        ddlDivisaForm.Enabled = habilitar
        ddlTipoMedioPago.Enabled = habilitar
        txtCodigoMedioPagoForm.Enabled = habilitar
        txtDescricaoMedioPagoForm.Enabled = habilitar
        txtObservaciones.Enabled = habilitar
        txtCodigoAcceso.Enabled = habilitar
        chkMercanciaForm.Enabled = habilitar
        chkVigenteForm.Enabled = habilitar
        btnNovo.Enabled = habilitar
    End Sub


    Protected Sub imgEditarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        ExecutarModificacion()
    End Sub

    Protected Sub imgConsultarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        ExecutarConsulta()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnBajaTermino_Click(sender As Object, e As EventArgs) Handles btnBajaTermino.Click
        ExecutarBaja()
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            MyBase.Acao = Utilidad.eAcao.Consulta
            LimparCamposForm()

            Dim valores As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            Dim strParamretorno As String = String.Empty

            Dim codMedioPago As String = Server.UrlDecode(valores(0))
            Dim codDivisaMedioPago As String = Server.UrlDecode(valores(1))
            Dim codTipoMedioPago As String = Server.UrlDecode(valores(2))

            CarregaDadosForm(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = False
            HabilitarDesabilitarCamposForm(False)
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)

        Try
            MyBase.Acao = Utilidad.eAcao.Baja
            LimparCamposForm()

            Dim valores As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            Dim strParamretorno As String = String.Empty

            Dim codMedioPago As String = Server.UrlDecode(valores(0))
            Dim codDivisaMedioPago As String = Server.UrlDecode(valores(1))
            Dim codTipoMedioPago As String = Server.UrlDecode(valores(2))

            CarregaDadosForm(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = True
            btnCancelar.Enabled = True
            btnSalvar.Visible = True
            btnSalvar.Enabled = False
            HabilitarDesabilitarCamposForm(False)
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            MyBase.Acao = Utilidad.eAcao.Modificacion
            LimparCamposForm()

            Dim valores As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            Dim strParamretorno As String = String.Empty

            Dim codMedioPago As String = Server.UrlDecode(valores(0))
            Dim codDivisaMedioPago As String = Server.UrlDecode(valores(1))
            Dim codTipoMedioPago As String = Server.UrlDecode(valores(2))

            CarregaDadosForm(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            btnCancelar.Visible = True
            btnSalvar.Visible = True
            HabilitarDesabilitarCamposForm(True)
            pnForm.Enabled = True
            pnForm.Visible = True

            chkVigenteForm.Visible = True
            lblVigenteForm.Visible = True

            If chkVigenteForm.Checked AndAlso EsVigente Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            If chkMercancia.Checked AndAlso EsMercancia Then
                chkMercancia.Enabled = False
            Else
                chkMercancia.Enabled = True
            End If

            ddlDivisaForm.Enabled = False
            ddlTipoMedioPago.Enabled = False
            Me.txtDescricaoMedioPagoForm.Enabled = ParametroMantenimientoClientesDivisasPorPantalla
            txtDescricaoMedioPagoForm.Focus()
            txtCodigoMedioPagoForm.Enabled = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub imgDuplicar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            MyBase.Acao = Utilidad.eAcao.Duplicar
            LimparCamposForm()

            Dim valores As String() = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            Dim strParamretorno As String = String.Empty

            Dim codMedioPago As String = Server.UrlDecode(valores(0))
            Dim codDivisaMedioPago As String = Server.UrlDecode(valores(1))
            Dim codTipoMedioPago As String = Server.UrlDecode(valores(2))

            CarregaDadosForm(codMedioPago, codDivisaMedioPago, codTipoMedioPago)

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            HabilitarDesabilitarCamposForm(True)
            pnForm.Enabled = True
            pnForm.Visible = True

            chkVigenteForm.Visible = False
            chkVigenteForm.Checked = True
            lblVigenteForm.Visible = False

            chkMercancia.Checked = False
            chkMercancia.Enabled = Me.ddlTipoMedioPago.SelectedValue = ContractoServicio.Constantes.COD_TIPO_MEDIO_PG_OTROSVALORES


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Clique botão Acima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAcima_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Dim strCodigoTerminoSelecionado As String = GridForm.getValorLinhaSelecionada

        Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, strCodigoTerminoSelecionado)

        Dim objPosterior As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        For Each objLinha In TerminoMediosPagoTemporario
            If objLinha.Codigo.Equals(strCodigoTerminoSelecionado) AndAlso objPosterior IsNot Nothing Then
                Dim auxSelecionado As Integer = objLinha.OrdenTermino
                objLinha.OrdenTermino = objPosterior.OrdenTermino
                objPosterior.OrdenTermino = auxSelecionado
            End If
            objPosterior = objLinha
        Next

        'Carrega os Terminos no GridView        
        TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
        Dim objDT As DataTable
        objDT = GridForm.ConvertListToDataTable(TerminoMediosPagoTemporario)
        GridForm.CarregaControle(objDT, False, False)

    End Sub

    ''' <summary>
    ''' Clique botão Abaixo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAbaixo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click

        Dim strCodigoTerminoSelecionado As String = GridForm.getValorLinhaSelecionada

        Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        objTerminoMedioPago = RetornaTerminoMedioPagoExiste(TerminoMediosPagoTemporario, strCodigoTerminoSelecionado)

        Dim objInferior As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim objCorrente As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago = Nothing
        Dim bolSairProximo As Boolean = False
        For Each objLinha In TerminoMediosPagoTemporario

            If bolSairProximo Then
                objInferior = objLinha
                Exit For
            End If

            If objLinha.Codigo.Equals(strCodigoTerminoSelecionado) Then
                objCorrente = objLinha
                bolSairProximo = True
            End If
        Next

        If objCorrente IsNot Nothing AndAlso objInferior IsNot Nothing Then
            Dim auxSelecionado As Integer = objCorrente.OrdenTermino
            objCorrente.OrdenTermino = objInferior.OrdenTermino
            objInferior.OrdenTermino = auxSelecionado
        End If

        'Carrega os Terminos no GridView
        TerminoMediosPagoTemporario = OrdenaColecao(TerminoMediosPagoTemporario)
        Dim objDT As DataTable
        objDT = GridForm.ConvertListToDataTable(TerminoMediosPagoTemporario)
        GridForm.CarregaControle(objDT, False, False)

    End Sub
End Class
