Imports DevExpress.XtraRichEdit.Model
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

''' <summary>
''' Página de Busca de Procedencias 
''' </summary>
''' <remarks></remarks>
''' <history>[MAOLIVEIRA] 07/06/13 - Criado</history>
Partial Public Class BusquedaProcedencias
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona java script aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        ddlTipoSubCliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoPuntoServicio.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlTipoProcedencia.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        ddlTipoSubCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & ddlTipoPuntoServicio.ClientID & "').focus();return false;}} else {return true}; ")
        ddlTipoSubCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & ddlTipoProcedencia.ClientID & "').focus();return false;}} else {return true}; ")

    End Sub

    ''' <summary>
    ''' Seta Tab Index
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Protected Overrides Sub ConfigurarTabIndex()
        
    End Sub

    ''' <summary>
    ''' Define os parametors iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try

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
                btnSalvar.Enabled = False

                'Preenche Combos
                PreencherComboTipoSubCliente()
                PreencherComboTipoPuntoServicio()
                PreencherComboTipoProcedencia()
                ExecutarBusca()
                UpdatePanelGrid.Update()

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

            End If

            TrataFoco()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre Render
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Protected Overrides Sub TraduzirControles()


        Master.Titulo = Traduzir("055_lbl_titulo")
        lblTipoSubCliente.Text = Traduzir("055_lbl_tiposubcliente")
        lblTipoPuntoServicio.Text = Traduzir("055_lbl_tipopuntoservicio")
        lblTipoProcedencia.Text = Traduzir("055_lbl_tipoprocedencia")
        lblVigente.Text = Traduzir("055_lbl_vigencia")
        lblSubTitulosProcedencias.Text = Traduzir("055_lbl_subtitulosprocedencias")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("055_lbl_subtituloscriteriosbusqueda")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

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
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("055_lbl_gdr_tiposubcliente")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("055_lbl_gdr_tipopuntoservicio")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("055_lbl_gdr_tipoprocedencia")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("055_lbl_gdr_vigencia")

        'Form
        lblTipoSubClienteForm.Text = Traduzir("055_titulo_mantenimiento_procedencia")
        lblTipoPuntoServicioForm.Text = Traduzir("055_lbl_tipopuntoservicio")
        lblTipoProcedenciaForm.Text = Traduzir("055_lbl_tipoprocedencia")
        lblVigenteForm.Text = Traduzir("055_lbl_vigencia")
        lblTituloProcedencia.Text = Traduzir("055_titulo_procedencia")

        csvTipoSubClienteObrigatorio.ErrorMessage = Traduzir("055_msg_tiposubclienteobligatorio")
        csvTipoPuntoServicioObrigatorio.ErrorMessage = Traduzir("055_msg_tipopuntoservicioobligatorio")
        csvTipoProcedenciaObrigatorio.ErrorMessage = Traduzir("055_msg_tipoprocedenciaobligatorio")
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Property Procedencia() As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion
        Get
            Return ViewState("Procedencia")
        End Get
        Set(value As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion)
            ViewState("Procedencia") = value
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

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroTipoSubcliente() As String
        Get
            Return ViewState("FiltroTipoSubcliente")
        End Get
        Set(value As String)
            ViewState("FiltroTipoSubcliente") = value
        End Set
    End Property

    Property FiltroTipoPuntoServicio() As String
        Get
            Return ViewState("FiltroTipoPuntoServicio")
        End Get
        Set(value As String)
            ViewState("FiltroTipoPuntoServicio") = value
        End Set
    End Property

    Property FiltroTipoProcedencia() As String
        Get
            Return ViewState("FiltroTipoProcedencia")
        End Get
        Set(value As String)
            ViewState("FiltroTipoProcedencia") = value
        End Set
    End Property

    Public Property TiposPuntoServicio() As GetComboTiposPuntoServicio.TipoPuntoServicioColeccion
        Get
            Return ViewState("TiposPuntoServicio")
        End Get
        Set(value As GetComboTiposPuntoServicio.TipoPuntoServicioColeccion)
            ViewState("TiposPuntoServicio") = value
        End Set
    End Property

    Public Property TiposSubCliente() As GetComboTiposSubCliente.TipoSubClienteColeccion
        Get
            Return ViewState("TiposSubCliente")
        End Get
        Set(value As GetComboTiposSubCliente.TipoSubClienteColeccion)
            ViewState("TiposSubCliente") = value
        End Set
    End Property

    Public Property TiposProcedencia() As GetComboTiposProcedencia.TipoProcedenciaColeccion
        Get
            Return ViewState("TiposProcedencia")
        End Get
        Set(value As GetComboTiposProcedencia.TipoProcedenciaColeccion)
            ViewState("TiposProcedencia") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoSubCliente()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposSubCliente

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposSubCliente = objRespuesta.TiposSubCliente
        End If

        ddlTipoSubCliente.AppendDataBoundItems = True
        ddlTipoSubCliente.Items.Clear()
        ddlTipoSubCliente.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoSubCliente.DataTextField = "Descripcion"
        ddlTipoSubCliente.DataValueField = "Codigo"
        ddlTipoSubCliente.DataSource = TiposSubCliente.OrderBy(Function(x) x.Descripcion)
        ddlTipoSubCliente.DataBind()


        ddlTipoSubClienteForm.AppendDataBoundItems = True
        ddlTipoSubClienteForm.Items.Clear()
        ddlTipoSubClienteForm.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoSubClienteForm.DataTextField = "Descripcion"
        ddlTipoSubClienteForm.DataValueField = "Codigo"
        ddlTipoSubClienteForm.DataSource = TiposSubCliente.OrderBy(Function(x) x.Descripcion)
        ddlTipoSubClienteForm.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoPuntoServicio()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposPuntoServicio

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposPuntoServicio = objRespuesta.TiposPuntoServicio
        End If

        ddlTipoPuntoServicio.AppendDataBoundItems = True
        ddlTipoPuntoServicio.Items.Clear()
        ddlTipoPuntoServicio.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoPuntoServicio.DataTextField = "Descripcion"
        ddlTipoPuntoServicio.DataValueField = "Codigo"
        ddlTipoPuntoServicio.DataSource = TiposPuntoServicio.OrderBy(Function(x) x.Descripcion)
        ddlTipoPuntoServicio.DataBind()

        ddlTipoPuntoServicioForm.AppendDataBoundItems = True
        ddlTipoPuntoServicioForm.Items.Clear()
        ddlTipoPuntoServicioForm.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoPuntoServicioForm.DataTextField = "Descripcion"
        ddlTipoPuntoServicioForm.DataValueField = "Codigo"
        ddlTipoPuntoServicioForm.DataSource = TiposPuntoServicio.OrderBy(Function(x) x.Descripcion)
        ddlTipoPuntoServicioForm.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoProcedencia()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposProcedencia

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposProcedencia = objRespuesta.TiposProcedencia
        End If

        ddlTipoProcedencia.AppendDataBoundItems = True
        ddlTipoProcedencia.Items.Clear()
        ddlTipoProcedencia.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoProcedencia.DataTextField = "Descripcion"
        ddlTipoProcedencia.DataValueField = "Codigo"
        ddlTipoProcedencia.DataSource = TiposProcedencia.OrderBy(Function(x) x.Descripcion)
        ddlTipoProcedencia.DataBind()

        ddlTipoProcedenciaForm.AppendDataBoundItems = True
        ddlTipoProcedenciaForm.Items.Clear()
        ddlTipoProcedenciaForm.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoProcedenciaForm.DataTextField = "Descripcion"
        ddlTipoProcedenciaForm.DataValueField = "Codigo"
        ddlTipoProcedenciaForm.DataSource = TiposProcedencia.OrderBy(Function(x) x.Descripcion)
        ddlTipoProcedenciaForm.DataBind()


    End Sub

    ''' <summary>
    ''' Faz a busca das procedencias com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Public Function getProcedencias() As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta

        Dim objProxyProcedencia As New Comunicacion.ProxyProcedencia
        Dim objPeticionProcedencia As New IAC.ContractoServicio.Procedencia.GetProcedencias.Peticion
        Dim objRespuestaProcedencia As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta

        'Recebe os valores do filtro
        objPeticionProcedencia.Activo = FiltroVigente
        objPeticionProcedencia.CodigoTipoSubCliente = FiltroTipoSubcliente
        objPeticionProcedencia.CodigoTipoPuntoServicio = FiltroTipoPuntoServicio
        objPeticionProcedencia.CodigoTipoProcedencia = FiltroTipoProcedencia

        objRespuestaProcedencia = objProxyProcedencia.GetProcedencias(objPeticionProcedencia)

        Return objRespuestaProcedencia

    End Function

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
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
    ''' Preenche do grid com a coleção de procedencias
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Public Sub PreencherProcedencias()

        Dim objRespostaProcedencia As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta

        'Busca os canais
        objRespostaProcedencia = getProcedencias()

        If Not Master.ControleErro.VerificaErro(objRespostaProcedencia.CodigoError, objRespostaProcedencia.NombreServidorBD, objRespostaProcedencia.MensajeError) Then

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaProcedencia.Procedencias.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaProcedencia.Procedencias.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaProcedencia.Procedencias)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then

                    objDt.DefaultView.Sort = " DescripcionTipoSubCliente ASC "

                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " DescripcionTipoSubCliente ASC "
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

            'Verifica se a ação de preencher foi acionada pela baixa de um canal("Atualizar o GridView" - Não exibe o painel informativo de "sem registros")
            'ou se foi aciona por outra ação (ex:botão buscar - exibe o painel informativo de "sem registros").

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

#Region "Eventos GridView"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("Activo").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Faz a ordenação do grid. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Dim objDT As DataTable

        objDT = ProsegurGridView1.ConvertListToDataTable(getProcedencias().Procedencias)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then

            objDT.DefaultView.Sort = " DescripcionTipoSubCliente ASC "

        Else
            objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
        End If

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Responsavel peldo estilo do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
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
        CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12
        CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

        CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
        CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
        CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
        CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


    End Sub

    ''' <summary>
    ''' Configruação das colunas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '0 - DescripcionTipoSubCliente
            '1 - DescripcionTipoPuntoServicio
            '2 - DescripcionTipoProcedencia
            '3 - Activo

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("OidProcedencia").ToString()) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


            If Not e.Row.DataItem("DescripcionTipoSubCliente") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTipoSubCliente").Length > NumeroMaximoLinha Then
                e.Row.Cells(1).Text = e.Row.DataItem("DescripcionTipoSubCliente").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If Not e.Row.DataItem("DescripcionTipoPuntoServicio") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTipoPuntoServicio").Length > NumeroMaximoLinha Then
                e.Row.Cells(2).Text = e.Row.DataItem("DescripcionTipoPuntoServicio").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If Not e.Row.DataItem("DescripcionTipoProcedencia") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTipoProcedencia").Length > NumeroMaximoLinha Then
                e.Row.Cells(3).Text = e.Row.DataItem("DescripcionTipoProcedencia").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            End If

            If CBool(e.Row.DataItem("Activo")) Then
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Tradução dos cabeçalhos do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then


        End If

    End Sub

    Private Sub grvClientes_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript
        Try
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("Activo").ToString.ToLower & ");"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click botão limpar
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ExecutarBusca()

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        'Filtros
        FiltroTipoSubcliente = ddlTipoSubCliente.SelectedValue
        FiltroTipoPuntoServicio = ddlTipoPuntoServicio.SelectedValue
        FiltroTipoProcedencia = ddlTipoProcedencia.SelectedValue
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherProcedencias()

        pnForm.Visible = False
        btnNovo.Enabled = True
        btnBajaConfirm.Visible = False
        btnCancelar.Enabled = False
        btnSalvar.Enabled = False
    End Sub


    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    strCodigo = Server.UrlDecode(ProsegurGridView1.getValorLinhaSelecionada)
                Else
                    strCodigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyProcedencia As New Comunicacion.ProxyProcedencia
                Dim objPeticionProcedencia As New IAC.ContractoServicio.Procedencia.SetProcedencia.Peticion
                Dim objRespuestaProcedencia As IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta


                Dim objRespuestaCon As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta = objProxyProcedencia.GetProcedencias(New IAC.ContractoServicio.Procedencia.GetProcedencias.Peticion With {.OidProcedencia = strCodigo})

                If Master.ControleErro.VerificaErro(objRespuestaCon.CodigoError, objRespuestaCon.NombreServidorBD, objRespuestaCon.MensajeError) Then

                    Dim objProcedenciaCon As IAC.ContractoServicio.Procedencia.GetProcedencias.Procedencia = objRespuestaCon.Procedencias.FirstOrDefault()

                    'Criando um Procedencia para exclusão
                    Dim objProcedencia As New IAC.ContractoServicio.Procedencia.SetProcedencia.Procedencia
                    objProcedencia.OidProcedencia = strCodigo
                    objProcedencia.Activo = False
                    objProcedencia.OidTipoSubCliente = TiposSubCliente.Where(Function(t) t.Codigo = objProcedenciaCon.CodigoTipoSubCliente).Select(Function(t) t.Oid).FirstOrDefault()
                    objProcedencia.OidTipoPuntoServicio = TiposPuntoServicio.Where(Function(t) t.Codigo = objProcedenciaCon.CodigoTipoPuntoServicio).Select(Function(t) t.Oid).FirstOrDefault()
                    objProcedencia.OidTipoProcedencia = TiposProcedencia.Where(Function(t) t.Codigo = objProcedenciaCon.CodigoTipoProcedencia).Select(Function(t) t.Oid).FirstOrDefault()
                    objProcedencia.FyhActualizacion = DateTime.Now
                    objProcedencia.CodigoUsuarioActualizacion = MyBase.LoginUsuario

                    'Associa a coleção de canais criado para a petição
                    objPeticionProcedencia.Procedencia = objProcedencia

                    'Exclui a petição
                    objRespuestaProcedencia = objProxyProcedencia.ActualizaProcedencia(objPeticionProcedencia)

                    If Master.ControleErro.VerificaErro(objRespuestaProcedencia.CodigoError, objRespuestaProcedencia.NombreServidorBD, objRespuestaProcedencia.MensajeError) Then

                        MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                        'Atualiza o GridView
                        ExecutarBusca()
                        UpdatePanelGrid.Update()
                        btnCancelar_Click(Nothing, Nothing)

                    Else
                        MyBase.MostraMensagem(objRespuestaProcedencia.MensajeError)
                    End If
                Else
                    MyBase.MostraMensagem(objRespuestaCon.MensajeError)
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração controle de estado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
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
                btnBaja.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                'Controles
                btnBaja.Visible = False
                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                ddlTipoSubCliente.SelectedValue = String.Empty
                ddlTipoPuntoServicio.SelectedValue = String.Empty
                ddlTipoProcedencia.SelectedValue = String.Empty
                ddlTipoSubCliente.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

                btnBaja.Visible = True

        End Select

    End Sub
    Private Sub CarregaDados(OidProcedencia As String)

        Dim objProxy As New Comunicacion.ProxyProcedencia
        Dim objRespuesta As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Procedencia.GetProcedencias.Peticion
        objPeticion.OidProcedencia = OidProcedencia
        objRespuesta = objProxy.GetProcedencias(objPeticion)

        Procedencia = objRespuesta.Procedencias

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Procedencias.Count > 0 Then

            ddlTipoSubClienteForm.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoSubCliente
            ddlTipoPuntoServicioForm.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoPuntoServicio
            ddlTipoProcedenciaForm.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoProcedencia
            chkVigenteForm.Checked = objRespuesta.Procedencias.First().Activo

            ' preenche a propriedade da tela
            EsVigente = objRespuesta.Procedencias.First().Activo

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                'DescricaoAtual = txtDescricaoCanal.Text
            End If

        End If

    End Sub
#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                PreencherComboTipoSubCliente()
                PreencherComboTipoPuntoServicio()
                PreencherComboTipoProcedencia()

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True

                If chkVigenteForm.Checked AndAlso EsVigente Then
                    chkVigenteForm.Enabled = False
                Else
                    chkVigenteForm.Enabled = True
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            PreencherComboTipoSubCliente()
            PreencherComboTipoPuntoServicio()
            PreencherComboTipoProcedencia()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            Procedencia = Nothing
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            PreencherComboTipoSubCliente()
            PreencherComboTipoPuntoServicio()
            PreencherComboTipoProcedencia()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            Procedencia = Nothing
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False

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
                PreencherComboTipoSubCliente()
                PreencherComboTipoPuntoServicio()
                PreencherComboTipoProcedencia()

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True
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
                PreencherComboTipoSubCliente()
                PreencherComboTipoPuntoServicio()
                PreencherComboTipoProcedencia()

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                btnBajaConfirm.Enabled = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ExecutarGravar()

        Try

            Dim objProxyProcedencia As New Comunicacion.ProxyProcedencia
            Dim objPeticion As New IAC.ContractoServicio.Procedencia.SetProcedencia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta
            Dim strErro As String = String.Empty
            Dim OidProcedencia As String = String.Empty

            ValidarCamposObrigatorios = True

            strErro = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objProcedencia As New IAC.ContractoServicio.Procedencia.SetProcedencia.Procedencia

            objProcedencia.Activo = chkVigenteForm.Checked

            If Procedencia IsNot Nothing AndAlso Procedencia.Count > 0 Then
                OidProcedencia = Procedencia.FirstOrDefault().OidProcedencia
            End If


            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            ' Preenche o objeto Procedencia
            objProcedencia.OidProcedencia = OidProcedencia
            objProcedencia.OidTipoSubCliente = TiposSubCliente.Where(Function(t) t.Codigo = ddlTipoSubClienteForm.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            objProcedencia.OidTipoPuntoServicio = TiposPuntoServicio.Where(Function(t) t.Codigo = ddlTipoPuntoServicioForm.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            objProcedencia.OidTipoProcedencia = TiposProcedencia.Where(Function(t) t.Codigo = ddlTipoProcedenciaForm.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            If String.IsNullOrEmpty(OidProcedencia) Then
                objProcedencia.FyhCreacion = DateTime.Now
                objProcedencia.CodigoUsuarioCreacion = MyBase.LoginUsuario
            Else
                objProcedencia.FyhCreacion = Procedencia.First().GmtCreacion
                objProcedencia.CodigoUsuarioCreacion = Procedencia.First().DesUsuarioCreacion
            End If

            objProcedencia.FyhActualizacion = DateTime.Now
            objProcedencia.CodigoUsuarioActualizacion = MyBase.LoginUsuario

            objPeticion.Procedencia = objProcedencia

            ' Carrega a petição para verificar se a Procedencia já existe
            Dim objPeticionV As New IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion
            objPeticionV.OidProcedencia = OidProcedencia
            objPeticionV.OidTipoSubCliente = objProcedencia.OidTipoSubCliente
            objPeticionV.OidTipoPuntoServicio = objProcedencia.OidTipoPuntoServicio
            objPeticionV.OidTipoProcedencia = objProcedencia.OidTipoProcedencia

            ' Verifica se a procedencia já existe
            Dim objRespuestaV As IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta = objProxyProcedencia.VerificaExisteProcedencia(objPeticionV)

            ' Verifica se aconteceu algum erro na execução ao executar a verificação
            If Master.ControleErro.VerificaErro(objRespuestaV.CodigoError, objRespuestaV.NombreServidorBD, objRespuestaV.MensajeError) Then

                ' Se a procedencia já existe
                If Not objRespuestaV.Existe Then

                    ' Se não foi informado o código de Procedencia
                    If String.IsNullOrEmpty(OidProcedencia) Then

                        objRespuesta = objProxyProcedencia.AltaProcedencia(objPeticion)

                        'Define a ação de busca somente se houve retorno de canais
                        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                            MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                            ExecutarBusca()
                            UpdatePanelGrid.Update()
                            btnCancelar_Click(Nothing, Nothing)

                        Else
                            MyBase.MostraMensagem(objRespuesta.MensajeError)
                        End If

                    Else
                        objRespuesta = objProxyProcedencia.ActualizaProcedencia(objPeticion)

                        'Define a ação de busca somente se houve retorno de canais
                        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                            MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                            ExecutarBusca()
                            UpdatePanelGrid.Update()
                            btnCancelar_Click(Nothing, Nothing)

                        Else
                            MyBase.MostraMensagem(objRespuesta.MensajeError)
                        End If

                    End If

                Else
                    MyBase.MostraMensagem(Traduzir("055_msg_erro_ProcedenciaExistente"))
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

                'Verifica se o tipo do subcliente foi informado
                If ddlTipoSubClienteForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSubClienteObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSubClienteForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoSubClienteObrigatorio.IsValid = True
                End If

                'Verifica se o tipo do punto de servicio foi informado
                If ddlTipoPuntoServicioForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoPuntoServicioObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoPuntoServicioForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoPuntoServicioObrigatorio.IsValid = True
                End If

                'Verifica se o tipo da procedencia foi informado
                If ddlTipoProcedenciaForm.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoProcedenciaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoProcedenciaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoProcedenciaForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoProcedenciaObrigatorio.IsValid = True
                End If


            End If

        End If

        Return strErro.ToString

    End Function


    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGravar()
    End Sub
End Class