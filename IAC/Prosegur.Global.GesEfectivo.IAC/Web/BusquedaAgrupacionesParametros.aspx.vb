Imports System.Diagnostics.Eventing.Reader
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Postos
''' </summary>
''' <remarks></remarks>
''' <history>[lmsantana] 05/09/11 - Criado</history>
Partial Public Class BusquedaAgrupacionesParametros
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 15/09/11 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Adiciona java script aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 05/09/11 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        ddlAplicacion.Attributes.Add("onkeydown", "EventoEnter('" & ddlNivel.ID & "_img', event);")
        ddlNivel.Attributes.Add("onkeydown", "EventoEnter('" & txtAgrupacion.ID & "_img', event);")
        txtAgrupacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        txtOrden.Attributes.Add("onkeypress", "return bloqueialetras(event,this);")
        txtOrden.Attributes.Add("onKeyDown", "return BloquearColar();")

        txtDescripcionLarga.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")

    End Sub

    ''' <summary>
    ''' Seta Tab Index
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametors iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            TrataFoco()

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not IsPostBack Then

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                '  btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                'Preenche Combos
                ExecutarBusca()
                UpdatePanelGrid.Update()

                ConfigurarControles()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre Render
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
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
    ''' [lmsantana] 05/09/11 Criado
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("028_titulo_busqueda_puestos")
        lblAplicacion.Text = Traduzir("028_lbl_aplicacion")
        lblNivel.Text = Traduzir("028_lbl_nivel")
        lblAgrupacion.Text = Traduzir("028_lbl_agrupacion")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("028_lbl_subtituloscriteriosbusqueda")
        lblSubTitulosAgrupacionParametro.Text = Traduzir("028_lbl_subtitulosagrupaciones")
        lblSemRegistro.Text = Traduzir("028_lbl_sem_registro")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")

        'botoes
        btnNovo.Text = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("028_col_aplicacion")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("028_col_nivel")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("028_col_agrupacion")

        'Formulario
        lblSubTitulosAgrupacionParametroForm.Text = Traduzir("028_titulo_mantenimento_agrupacion")
        lblAplicacionForm.Text = Traduzir("028_lbl_aplicacion_mant")
        lblNivelForm.Text = Traduzir("028_lbl_nivel_mant")
        lblDescripcionLarga.Text = Traduzir("028_lbl_DescripcionLarga_mant")
        lblDescripcionCorto.Text = Traduzir("028_lbl_descripcioncorto_mant")
        lblOrden.Text = Traduzir("028_lbl_orden_mant")
        csvDdlAplicacionObrigatorio.ErrorMessage = Traduzir("028_msg_aplicacionobligatorio")
        csvDdlNivelObrigatorio.ErrorMessage = Traduzir("028_msg_nivelobligatorio")
        csvTxtDescripcionCortoObrigatorio.ErrorMessage = Traduzir("028_msg_descripcioncortoobligatorio")
        csvTxtDescripcionLargaObrigatorio.ErrorMessage = Traduzir("028_msg_DescripcionLargaobligatorio")
        csvTxtOrden.ErrorMessage = Traduzir("028_msg_Dordenobligatorio")


    End Sub

#End Region

#Region "[PROPRIEDADES]"


    Property FiltroAplicacion() As String
        Get
            Return ViewState("FiltroAplicacion")
        End Get
        Set(value As String)
            ViewState("FiltroAplicacion") = value
        End Set
    End Property

    Property FiltroNivel() As String
        Get
            Return ViewState("FiltroNivel")
        End Get
        Set(value As String)
            ViewState("FiltroNivel") = value
        End Set
    End Property

    Property FiltroAgrupacion() As String
        Get
            Return ViewState("FiltroAgrupacion")
        End Get
        Set(value As String)
            ViewState("FiltroAgrupacion") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Faz a busca das Agrupações de parametro com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Public Function getAgrupaciones() As IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta

        Dim objProxy As New Comunicacion.ProxyParametro
        Dim objPeticion As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Peticion
        Dim objRespuesta As IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta

        'Recebe os valores do filtro        
        objPeticion.CodigoAplicacion = FiltroAplicacion
        objPeticion.CodigoNivel = FiltroNivel
        objPeticion.DesAgrupacion = txtAgrupacion.Text
        'objPeticion.Permisos = InformacionUsuario.Permisos
        objPeticion.Aplicaciones = InformacionUsuario.Aplicaciones

        objRespuesta = objProxy.GetAgrupaciones(objPeticion)

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
    ''' Preenche do grid com a coleção de Agrupações
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Public Sub PreencherAgrupaciones()

        Dim objResposta As IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta

        'Busca os canais
        objResposta = getAgrupaciones()

        If Not Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            MyBase.MostraMensagem(objResposta.MensajeError)
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objResposta.Agrupaciones.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objResposta.Agrupaciones.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable

                objDt = ProsegurGridView1.ConvertListToDataTable(objResposta.Agrupaciones)

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

    Private Sub ConfigurarControles()
        PreencherComboAplicaciones()
        PreencherComboNiveles()
    End Sub

    Private Sub PreencherComboAplicaciones()
        Try
            ddlAplicacion.DataSource = Nothing
            ddlAplicacion.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If InformacionUsuario.Aplicaciones.Count > 1 Then
                ddlAplicacion.AppendDataBoundItems = True
                ddlAplicacion.Items.Clear()
                ddlAplicacion.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlAplicacion.DataTextField = "DescripcionAplicacion"
            ddlAplicacion.DataValueField = "CodigoAplicacion"
            ddlAplicacion.DataSource = InformacionUsuario.Aplicaciones.OrderBy(Function(x) x.DescripcionAplicacion)
            ddlAplicacion.DataBind()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub PreencherComboNiveles()
        Try
            Dim objNivelesParametros As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion
            objNivelesParametros = Aplicacao.Util.Utilidad.getComboNivelesParametros(InformacionUsuario.Permisos)

            ddlNivel.DataSource = Nothing
            ddlNivel.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If objNivelesParametros.Count > 1 Then
                ddlNivel.AppendDataBoundItems = True
                ddlNivel.Items.Clear()
                ddlNivel.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlNivel.DataTextField = "DescripcionNivel"
            ddlNivel.DataValueField = "CodigoNivel"
            ddlNivel.DataSource = objNivelesParametros.OrderBy(Function(x) x.DescripcionNivel)
            ddlNivel.DataBind()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
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
    ''' [lmsantana] 15/09/11 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            'adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            'ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("PuestoVigente").ToString.ToLower & ");"

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
    ''' [lmsantana] 15/09/11 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Dim objDT As DataTable

        objDT = ProsegurGridView1.ConvertListToDataTable(getAgrupaciones().Agrupaciones)

        ProsegurGridView1.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Responsavel peldo estilo do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 15/09/2011 Criado
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
    ''' [lmsantana] 15/09/11 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim valor As String = Server.UrlEncode(e.Row.DataItem("DescripcionCorta").ToString) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoAplicacion").ToString) & "$#"
            valor &= Server.UrlEncode(e.Row.DataItem("CodigoNivel").ToString)

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"

            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            'CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            'CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

        End If
    End Sub

    ''' <summary>
    ''' Tradução dos cabeçalhos do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 15/09/11 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        If e.Row.RowType = DataControlRowType.Header Then

        End If

    End Sub


#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click botão limpar
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ddlAplicacion.ClearSelection()
            ddlAplicacion.ToolTip = String.Empty
            ddlNivel.ClearSelection()
            ddlNivel.ToolTip = String.Empty

            ddlAplicacion.Focus()

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnSalvar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub ExecutarBusca()
        'Threading.Thread.Sleep(2000)
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca


        'Filtros
        FiltroAplicacion = ddlAplicacion.SelectedValue
        FiltroNivel = ddlNivel.SelectedValue
        FiltroAgrupacion = txtAgrupacion.Text

        'Retorna os canais de acordo com o filtro aciam
        PreencherAgrupaciones()
        
    End Sub
    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxy As New Comunicacion.ProxyParametro
            Dim objPeticion As New IAC.ContractoServicio.Parametro.BajarAgrupacion.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Parametro.BajarAgrupacion.Respuesta

            Dim Codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")
            objPeticion.CodigoAplicacion = Server.UrlDecode(Codigos(1))
            objPeticion.DesAgrupacion = Server.UrlDecode(Codigos(0))
            objPeticion.CodigoNivel = Server.UrlDecode(Codigos(2))

            objRespuesta = objProxy.BajarAgrupacion(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                'Registro excluido com sucesso
                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                ExecutarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    ' mostrar o erro pro usuário
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

    Private Sub ddlAplicacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAplicacion.SelectedIndexChanged
        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text
    End Sub

    Private Sub ddlNivel_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNivel.SelectedIndexChanged
        ddlNivel.ToolTip = ddlNivel.SelectedItem.Text
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração controle de estado.
    ''' </summary>s
    ''' <remarks></remarks>
    ''' [lmsantana] 15/09/11 Criado
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

            Case Aplicacao.Util.Utilidad.eAcao.Baja

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                pnlSemRegistro.Visible = False
                txtAgrupacion.Text = String.Empty
            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub

#End Region
#Region "[PROPRIEDADES Formulario]"

    Property IsAlta() As Boolean
        Get
            Return ViewState("IsAlta")
        End Get
        Set(value As Boolean)
            ViewState("IsAlta") = value
        End Set
    End Property

    Property ParamsModificacionOK() As Boolean
        Get
            Return ViewState("ParamsModificacionOK")
        End Get
        Set(value As Boolean)
            ViewState("ParamsModificacionOK") = value
        End Set
    End Property

    Property CodigoAplicacion() As String
        Get
            Return ViewState("CodAplicacion")
        End Get
        Set(value As String)
            ViewState("CodAplicacion") = value
        End Set
    End Property

    Property CodigoNivel() As String
        Get
            Return ViewState("CodigoNivel")
        End Get
        Set(value As String)
            ViewState("CodigoNivel") = value
        End Set
    End Property

    Property DescripcionAgrupacion() As String
        Get
            Return ViewState("DescripcionAgrupacion")
        End Get
        Set(value As String)
            ViewState("DescripcionAgrupacion") = value
        End Set
    End Property

    Property DescripcionCorta() As String
        Get
            Return ViewState("DescripcionCorta")
        End Get
        Set(value As String)
            ViewState("DescripcionCorta") = value
        End Set
    End Property

    Property DescripcionLarga() As String
        Get
            Return ViewState("DescripcionLarga")
        End Get
        Set(value As String)
            ViewState("DescripcionLarga") = value
        End Set
    End Property

    Property NecOrden() As String
        Get
            Return ViewState("NecOrden")
        End Get
        Set(value As String)
            ViewState("NecOrden") = value
        End Set
    End Property

    Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property


#End Region
#Region "[Dados Formulario]"
    Public Sub ExecutarGravar()
        ' Executa controle de validação dos campos
        ValidarCamposObrigatorios = True
        Dim erros As String = MontaMensagensErro(True)
        If Not String.IsNullOrEmpty(erros) Then
            MyBase.MostraMensagem(erros)
            Exit Sub
        End If

        'Preenche viewstates com os dados da tela
        CodigoAplicacion = ddlAplicacionForm.SelectedValue
        CodigoNivel = ddlNivelForm.SelectedValue
        DescripcionCorta = txtDescripcionCorto.Text
        DescripcionLarga = txtDescripcionLarga.Text
        If String.IsNullOrEmpty(txtOrden.Text) OrElse IsNumeric(txtOrden.Text) Then
            NecOrden = txtOrden.Text
        Else
            ' exibir mensagem ao usuário
            MyBase.MostraMensagem(Traduzir("err_passagem_parametro") & " - " & lblOrden.Text)
            txtOrden.Focus()
            Exit Sub
        End If

        Dim objRespuestaSetAgrupacion As IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta

        objRespuestaSetAgrupacion = setAgrupacion()

        If Not Master.ControleErro.VerificaErro(objRespuestaSetAgrupacion.CodigoError, objRespuestaSetAgrupacion.NombreServidorBD, objRespuestaSetAgrupacion.MensajeError) Then
            MyBase.MostraMensagem(objRespuestaSetAgrupacion.MensajeError)
            Exit Sub
        End If

        MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
        ExecutarBusca()
        UpdatePanelGrid.Update()
        btnCancelar_Click(Nothing, Nothing)

    End Sub
    Public Function setAgrupacion() As IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaSetAgrupacion As New IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta
        Dim objPeticionSetAgrupacion As New IAC.ContractoServicio.Parametro.SetAgrupacion.Peticion

        objPeticionSetAgrupacion.CodigoAplicacion = CodigoAplicacion
        objPeticionSetAgrupacion.CodigoNivel = CodigoNivel
        objPeticionSetAgrupacion.DescripcionCorta = DescripcionCorta
        objPeticionSetAgrupacion.DescripcionLarga = DescripcionLarga
        objPeticionSetAgrupacion.NecOrden = If(String.IsNullOrEmpty(NecOrden), "0", NecOrden)
        objPeticionSetAgrupacion.CodigoUsuario = MyBase.LoginUsuario

        objRespuestaSetAgrupacion = objProxyParametro.SetAgrupacion(objPeticionSetAgrupacion)

        Return objRespuestaSetAgrupacion
    End Function
    Public Function getAgrupacionDetail() As IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetAgrupacionDetail As New IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta
        Dim objPeticionGetAgrupacionDetail As New IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Peticion

        objPeticionGetAgrupacionDetail.CodigoAplicacion = CodigoAplicacion
        objPeticionGetAgrupacionDetail.CodigoNivel = CodigoNivel
        objPeticionGetAgrupacionDetail.DesAgrupacion = DescripcionAgrupacion

        objRespuestaGetAgrupacionDetail = objProxyParametro.GetAgrupacionDetail(objPeticionGetAgrupacionDetail)

        Return objRespuestaGetAgrupacionDetail
    End Function

#End Region
#Region "[Métodos Formulario]"
    Private Sub PreencherTela()
        If Not ParamsModificacionOK Then
            TableFields.Visible = False
        End If

        Dim objRespuestaGetAgrupacionDetail As IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta

        objRespuestaGetAgrupacionDetail = getAgrupacionDetail()

        If Not Master.ControleErro.VerificaErro(objRespuestaGetAgrupacionDetail.CodigoError, objRespuestaGetAgrupacionDetail.NombreServidorBD, objRespuestaGetAgrupacionDetail.MensajeError) Then
            MyBase.MostraMensagem(objRespuestaGetAgrupacionDetail.MensajeError)
            Exit Sub
        End If

        ddlAplicacionForm.SelectedIndex = ddlAplicacionForm.Items.IndexOf(ddlAplicacionForm.Items.FindByValue(objRespuestaGetAgrupacionDetail.Agrupaciones.CodigoAplicacion))
        ddlAplicacionForm.ToolTip = ddlAplicacionForm.SelectedItem.Text

        ddlNivelForm.SelectedIndex = ddlNivelForm.Items.IndexOf(ddlNivelForm.Items.FindByValue(objRespuestaGetAgrupacionDetail.Agrupaciones.CodigoNivel))
        ddlNivelForm.ToolTip = ddlNivelForm.SelectedItem.Text

        txtDescripcionCorto.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionCorto
        txtDescripcionCorto.ToolTip = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionCorto

        txtDescripcionLarga.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionLarga
        txtOrden.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.NecOrden
    End Sub

    Private Sub LimparCampos()
        CodigoAplicacion = Nothing
        CodigoNivel = Nothing
        DescripcionAgrupacion = Nothing
        PreencherListBoxAplicaciones()
        PreencherListBoxNiveles()
        txtDescripcionCorto.Text = String.Empty
        txtDescripcionLarga.Text = String.Empty
        txtOrden.Text = String.Empty

    End Sub
    Private Sub PreencherListBoxAplicaciones()
        Try
            ddlAplicacionForm.DataSource = Nothing
            ddlAplicacionForm.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If InformacionUsuario.Aplicaciones.Count > 1 Then
                ddlAplicacionForm.AppendDataBoundItems = True
                ddlAplicacionForm.Items.Clear()
                ddlAplicacionForm.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlAplicacionForm.DataTextField = "DescripcionAplicacion"
            ddlAplicacionForm.DataValueField = "CodigoAplicacion"
            ddlAplicacionForm.DataSource = InformacionUsuario.Aplicaciones.OrderBy(Function(x) x.DescripcionAplicacion)
            ddlAplicacionForm.DataBind()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub PreencherListBoxNiveles()
        Try
            Dim objNivelesParametros As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion
            objNivelesParametros = Aplicacao.Util.Utilidad.getComboNivelesParametros(InformacionUsuario.Permisos)

            ddlNivelForm.DataSource = Nothing
            ddlNivelForm.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If objNivelesParametros.Count > 1 Then
                ddlNivelForm.AppendDataBoundItems = True
                ddlNivelForm.Items.Clear()
                ddlNivelForm.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlNivelForm.DataTextField = "DescripcionNivel"
            ddlNivelForm.DataValueField = "CodigoNivel"
            ddlNivelForm.DataSource = objNivelesParametros.OrderBy(Function(x) x.DescripcionNivel)
            ddlNivelForm.DataBind()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Laço que verifica todos os validators
                For Each validator As BaseValidator In Me.Validators
                    'Checa validator se é ou não válido
                    validator.Validate()
                    If Not validator.IsValid Then
                        'Caso o campo esteja inválido é adicionado uma mensagem de erro a lista
                        strErro.Append(validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        If Not focoSetado Then
                            'Seta o foco no primeiro campo com erro
                            validator.NamingContainer.FindControl(validator.ControlToValidate).Focus()
                            focoSetado = True
                        End If
                    End If
                Next

            End If
        End If

        Return strErro.ToString

    End Function
#End Region

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True

            ' txtCodigoTerminoForm.Enabled = True
            Acao = Utilidad.eAcao.Alta

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            If pnForm.Visible Then
                LimparCampos()
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                pnForm.Enabled = False
                pnForm.Visible = False
                Acao = Utilidad.eAcao.Inicial
            Else
                Response.Redireccionar(Page.ResolveUrl("~/BusquedaParametros.aspx"))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            ExecutarGravar()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    'Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
    '    Try
    '        LimparCampos()
    '        Acao = Utilidad.eAcao.Consulta

    '        Dim codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

    '        CodigoAplicacion = Server.UrlDecode(codigos(1))
    '        CodigoNivel = Server.UrlDecode(codigos(2))
    '        DescripcionAgrupacion = Server.UrlDecode(codigos(0))

    '        ParamsModificacionOK = Not (String.IsNullOrEmpty(CodigoAplicacion) OrElse String.IsNullOrEmpty(CodigoNivel) OrElse String.IsNullOrEmpty(DescripcionAgrupacion))
    '        PreencherTela()

    '        btnBajaConfirm.Visible = False
    '        btnNovo.Enabled = True
    '        btnCancelar.Enabled = True
    '        btnSalvar.Enabled = False
    '        pnForm.Enabled = False
    '        pnForm.Visible = True
    '        ddlAplicacionForm.Enabled = False
    '        ddlAplicacionForm.Enabled = False
    '        txtDescripcionCorto.Enabled = False


    '    Catch ex As Exception
    '        MyBase.MostraMensagemExcecao(ex)
    '    End Try
    'End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            LimparCampos()
            Acao = Utilidad.eAcao.Modificacion

            Dim codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            CodigoAplicacion = Server.UrlDecode(codigos(1))
            CodigoNivel = Server.UrlDecode(codigos(2))
            DescripcionAgrupacion = Server.UrlDecode(codigos(0))

            ParamsModificacionOK = Not (String.IsNullOrEmpty(CodigoAplicacion) OrElse String.IsNullOrEmpty(CodigoNivel) OrElse String.IsNullOrEmpty(DescripcionAgrupacion))
            PreencherTela()

            btnBajaConfirm.Visible = False
            btnNovo.Enabled = True
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            ddlAplicacionForm.Enabled = False
            ddlNivelForm.Enabled = False
            txtDescripcionCorto.Enabled = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            LimparCampos()
            Acao = Utilidad.eAcao.Consulta

            Dim codigos() As String = hiddenCodigo.Value.Replace("$#", "|").Split("|")

            CodigoAplicacion = Server.UrlDecode(codigos(1))
            CodigoNivel = Server.UrlDecode(codigos(2))
            DescripcionAgrupacion = Server.UrlDecode(codigos(0))

            ParamsModificacionOK = Not (String.IsNullOrEmpty(CodigoAplicacion) OrElse String.IsNullOrEmpty(CodigoNivel) OrElse String.IsNullOrEmpty(DescripcionAgrupacion))
            PreencherTela()

            btnBajaConfirm.Visible = True
            btnNovo.Enabled = True
            btnCancelar.Enabled = True
            btnSalvar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class