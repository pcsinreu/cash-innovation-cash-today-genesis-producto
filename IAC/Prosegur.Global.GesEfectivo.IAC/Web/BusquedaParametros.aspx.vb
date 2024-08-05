Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

Public Class BusquedaParametros
    Inherits Base

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>

    Property CodigoAplicacion() As String
        Get
            Return ViewState("CodigoAplicacion")
        End Get
        Set(value As String)
            ViewState("CodigoAplicacion") = value
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

    Property CodigoParametro() As String
        Get
            Return ViewState("CodigoParametro")
        End Get
        Set(value As String)
            ViewState("CodigoParametro") = value
        End Set
    End Property

    Property DesCortaAgrupacion() As String
        Get
            Return ViewState("DesCortaAgrupacion")
        End Get
        Set(value As String)
            ViewState("DesCortaAgrupacion") = value
        End Set
    End Property

    Property PantallaLlamadora() As String
        Get
            Return ViewState("PantallaLlamadora")
        End Get
        Set(value As String)
            ViewState("PantallaLlamadora") = value
        End Set
    End Property

    Property Parametros() As ContractoServicio.Parametro.GetParametros.ParametroColeccion
        Get
            If ViewState("Parametros") Is Nothing Then
                ViewState("Parametros") = New ContractoServicio.Parametro.GetParametros.ParametroColeccion
            End If
            Return ViewState("Parametros")
        End Get
        Set(value As ContractoServicio.Parametro.GetParametros.ParametroColeccion)
            ViewState("Parametros") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        ''Aciona o botão buscar quando prescionar o enter.
        ddlAplicacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlNivel.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricaoAgrupacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtCodigoParametro.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        txtDescripcionLarga.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PARAMETRO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            PantallaLlamadora = Request.QueryString("llamadora")

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then
                If Not String.IsNullOrEmpty(PantallaLlamadora) Then
                    btnCancelar.Visible = True
                Else
                    btnCancelar.Visible = False
                End If

                btnSalvar.Visible = False
                'Preenche Combo Aplicaciones
                PreencherListBoxAplicaciones()

                'Preenche Niveles 
                PreencherListBoxNiveles()

                ExecutarBusca()
                UpdatePanelGrid.Update()

                pnForm.Visible = False
                btnNovo.Visible = False
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
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            'ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("026_titulo_busqueda_parametros")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("026_lbl_subtitulo_filtros")
        lblParametros.Text = Traduzir("026_lbl_subtitulo_paramestros")
        lblAplicacion.Text = Traduzir("026_lbl_aplicacion")
        lblNivel.Text = Traduzir("026_lbl_nivel")
        lblDescricaoAgrupacion.Text = Traduzir("026_lbl_agrupacion")

        lblParametros.Text = Traduzir("026_col_aplicacion")
        lblParametros.Text = Traduzir("026_col_nivel")
        lblParametros.Text = Traduzir("026_col_agrupacion")
        lblParametros.Text = Traduzir("026_col_parametro")
        lblCodigoParametro.Text = Traduzir("026_col_Codparametro")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnABMAgrapaciones.Text = Traduzir("026_btn_abm_agrupaciones")
        btnABMAgrapaciones.ToolTip = Traduzir("026_btn_abm_agrupaciones")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnSalvar.ToolTip = Traduzir("btnGrabar")

        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("026_col_aplicacion")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("026_col_nivel")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("026_col_agrupacion")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("026_col_Codparametro")
        ProsegurGridView1.Columns(5).HeaderText = Traduzir("026_col_parametro")


        'Form
        lblSubTitulosCriteriosBusquedaForm.Text = Traduzir("026_titulo_mantenimento_parametros")
        lblAplicacionForm.Text = Traduzir("026_lbl_aplicacion_mant")
        lblNivelForm.Text = Traduzir("026_lbl_nivel_mant")
        lblCodParametro.Text = Traduzir("026_lbl_codparametro_mant")
        lblDescripcionLarga.Text = Traduzir("026_lbl_DescripcionLarga_mant")
        lblTipoComponent.Text = Traduzir("026_lbl_tipocomponent_mant")
        lblObligatorio.Text = Traduzir("026_lbl_obligatorio_mant")
        lblTipoDado.Text = Traduzir("026_lbl_tipodato_mant")
        lblAgrupacion.Text = Traduzir("026_lbl_agrupacion_mant")
        lblDescripcionCorto.Text = Traduzir("026_lbl_descripcioncorto_mant")
        lblOrden.Text = Traduzir("026_lbl_orden_mant")
        lblSubTituloOpciones.Text = Traduzir("026_titulo_lista_opciones")

        csvDdlAgrupacionObrigatorio.ErrorMessage = Traduzir("026_msg_agrupacionobligatorio")
        csvTxtDescripcionCortoObrigatorio.ErrorMessage = Traduzir("026_msg_descripcioncortoobligatorio")
        csvTxtDescripcionLargaObrigatorio.ErrorMessage = Traduzir("026_msg_DescripcionLargaobligatorio")
        csvTxtOrden.ErrorMessage = Traduzir("026_msg_Dordenobligatorio")

        'GridView
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'GridView Formulario
        ProsegurGridView2.Columns(1).HeaderText = Traduzir("014_lbl_grd_mantenimiento_codigo")
        ProsegurGridView2.Columns(2).HeaderText = Traduzir("014_lbl_grd_mantenimiento_descripcion")
        ProsegurGridView2.Columns(3).HeaderText = Traduzir("014_lbl_grd_mantenimiento_vigente")
        ProsegurGridView2.PagerSummary = Traduzir("grid_lbl_pagersummary")

    End Sub

#End Region

#Region "[METODOS]"

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
    ''' Função responsavel por preencher o dropdownlist Aplicaciones.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>
    Public Sub PreencherListBoxAplicaciones()

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



    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist Nível.
    ''' </summary>
    Public Sub PreencherListBoxNiveles()

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

    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher o grid parametros.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>
    Public Function getParametros() As IAC.ContractoServicio.Parametro.GetParametros.Respuesta
        Dim objProxyParametro As New ProxyParametro
        Dim objRespuestaParametros As New IAC.ContractoServicio.Parametro.GetParametros.Respuesta
        Dim objPeticionParametros As New IAC.ContractoServicio.Parametro.GetParametros.Peticion

        objPeticionParametros.CodigoAplicacion = CodigoAplicacion
        objPeticionParametros.CodigoNivel = CodigoNivel
        objPeticionParametros.DesCortaAgrupacion = DesCortaAgrupacion
        'objPeticionParametros.Permisos = InformacionUsuario.Permisos
        objPeticionParametros.Aplicaciones = InformacionUsuario.Aplicaciones
        objPeticionParametros.CodParametro = CodigoParametro

        objRespuestaParametros = objProxyParametro.GetParametros(objPeticionParametros)

        Return objRespuestaParametros
    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid parametros.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 08/09/2011 - Criado
    ''' </history>
    Public Sub PreencherDataGridParametros()

        Dim objRespuestaParametros As New IAC.ContractoServicio.Parametro.GetParametros.Respuesta

        objRespuestaParametros = getParametros()

        If Not Master.ControleErro.VerificaErro(objRespuestaParametros.CodigoError, objRespuestaParametros.NombreServidorBD, objRespuestaParametros.MensajeError) Then
            MyBase.MostraMensagem(objRespuestaParametros.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de Parametros
        If objRespuestaParametros.Parametros.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuestaParametros.Parametros.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                ' armazenar coleção de Parametros
                Parametros = objRespuestaParametros.Parametros

                Dim paramList = (From p In Parametros
                                Select p.Aplicacion.DescripcionAplicacion, p.Aplicacion.CodigoAplicacion,
                                p.Nivel.DescripcionNivel, p.Nivel.CodigoNivel,
                                p.Agrupacion.DescripcionCorta, p.Agrupacion.DescripcionLarga,
                                p.DesCortaParametro, p.DesLargaParametro, p.CodParametro,
                                Chave = p.Aplicacion.CodigoAplicacion & "|" & p.CodParametro).ToList

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(paramList)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " DescripcionAplicacion ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " DescripcionAplicacion ASC "
                    Else
                        objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

                ProsegurGridView1.CarregaControle(objDt)

            Else

                ' limpar coleção de Parametros
                Parametros = New ContractoServicio.Parametro.GetParametros.ParametroColeccion

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            ' limpar coleção de Parametros
            Parametros = New ContractoServicio.Parametro.GetParametros.ParametroColeccion

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

    Private Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try


            Dim objDT As DataTable

            Dim paramList = (From p In Parametros
                            Select p.Aplicacion.DescripcionAplicacion, p.Aplicacion.CodigoAplicacion,
                            p.Nivel.DescripcionNivel, p.Nivel.CodigoNivel,
                            p.Agrupacion.DescripcionCorta, p.Agrupacion.DescripcionLarga,
                            p.DesCortaParametro, p.DesLargaParametro, p.CodParametro,
                            Chave = p.Aplicacion.CodigoAplicacion & "|" & p.CodParametro).ToList

            If Parametros.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                objDT = ProsegurGridView1.ConvertListToDataTable(paramList)

                If ProsegurGridView1.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " DescripcionAplicacion asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridView1.SortCommand
                End If

                ProsegurGridView1.ControleDataSource = objDT

            Else
                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss
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

    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

        End If
    End Sub
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("Chave").ToString()) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("026_btn_modificar_parametros")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


#End Region

#Region "[EVENTOS BOTOES]"

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            btnSalvar.Visible = False
            btnCancelar.Visible = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        'Filtros
        CodigoAplicacion = ddlAplicacion.SelectedItem.Value
        CodigoNivel = ddlNivel.SelectedItem.Value
        DesCortaAgrupacion = txtDescricaoAgrupacion.Text.ToUpper
        CodigoParametro = txtCodigoParametro.Text.ToUpper

        'Retorna os Parametros de acordo com o filtro aciam
        PreencherDataGridParametros()

     

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            ProsegurGridView1.CarregaControle(Nothing, True, True)

            txtDescricaoAgrupacion.Text = String.Empty
            txtDescricaoAgrupacion.ToolTip = String.Empty

            txtCodigoParametro.Text = String.Empty
            txtCodigoParametro.ToolTip = String.Empty

            ddlAplicacion.ToolTip = String.Empty
            ddlNivel.ToolTip = String.Empty

            'Preenche Combo Aplicaciones novamente
            PreencherListBoxAplicaciones()

            'Preenche Niveles novamente
            PreencherListBoxNiveles()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ddlAplicacion.Focus()

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCamposForm()
            btnNovo.Enabled = True
            btnSalvar.Visible = False

            pnForm.Enabled = False
            pnForm.Visible = False
            btnCancelar.Visible = False
            btnNovo.Visible = False
            btnABMAgrapaciones.Visible = True

            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnABMAgrapaciones_Click(sender As Object, e As EventArgs) Handles btnABMAgrapaciones.Click
        Try
            If (Me.PossuiPermissao("AGRUPACIONES_PARAMETRO_CONSULTAR")) Then
                Response.Redirect("~/BusquedaAgrupacionesParametros.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Busca, False)
            Else
                Response.Redirect("~/Default.aspx")
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
#Region "[PROPRIEDADES Formulario]"

    Property CodAplicacion() As String
        Get
            Return ViewState("CodAplicacion")
        End Get
        Set(value As String)
            ViewState("CodAplicacion") = value
        End Set
    End Property

    Property CodParametro() As String
        Get
            Return ViewState("CodParametro")
        End Get
        Set(value As String)
            ViewState("CodParametro") = value
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

    Property DescripcionCortaParametro() As String
        Get
            Return ViewState("DescripcionCortaParametro")
        End Get
        Set(value As String)
            ViewState("DescripcionCortaParametro") = value
        End Set
    End Property

    Property DescripcionLargaParametro() As String
        Get
            Return ViewState("DescripcionLargaParametro")
        End Get
        Set(value As String)
            ViewState("DescripcionLargaParametro") = value
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

    Public Property ParametroOpcionTemporario() As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion
        Get
            If ViewState("ParametroOpcionTemporario") Is Nothing Then
                ViewState("ParametroOpcionTemporario") = New IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion
            End If

            Return ViewState("ParametroOpcionTemporario") 'DirectCast(ViewState("ParametroOpcionTemporario"), IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion)
            ViewState("ParametroOpcionTemporario") = value
        End Set
    End Property

    Public Property TipoControle As ContractoServicio.Parametro.TipoComponente
        Get
            Return ViewState("TipoControle")
        End Get
        Set(value As ContractoServicio.Parametro.TipoComponente)
            ViewState("TipoControle") = value
        End Set
    End Property

    Public Property Parametro As ContractoServicio.Parametro.GetParametroDetail.Parametro
        Get
            Return ViewState("Parametro")
        End Get
        Set(value As ContractoServicio.Parametro.GetParametroDetail.Parametro)
            ViewState("Parametro") = value
        End Set
    End Property

#End Region



#Region "[DADOS FORMULARIO]"
    Public Function getParametroDetail() As IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetParametroDetail As New IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta
        Dim objPeticionGetParametroDetail As New IAC.ContractoServicio.Parametro.GetParametroDetail.Peticion

        objPeticionGetParametroDetail.CodigoAplicacion = CodAplicacion
        objPeticionGetParametroDetail.CodigoParametro = CodParametro

        objRespuestaGetParametroDetail = objProxyParametro.GetParametroDetail(objPeticionGetParametroDetail)

        Return objRespuestaGetParametroDetail
    End Function
    Public Function getParametroOpcion() As IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta
        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetOpciones As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta
        Dim objPeticionGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Peticion

        objPeticionGetAgrupaciones.CodigoAplicacion = CodAplicacion
        objPeticionGetAgrupaciones.CodigoParametro = CodParametro

        objRespuestaGetOpciones = objProxyParametro.GetParametroOpciones(objPeticionGetAgrupaciones)
        For index = 0 To objRespuestaGetOpciones.Opciones.Count - 1
            objRespuestaGetOpciones.Opciones(index).Parametro = Parametro
        Next

        Return objRespuestaGetOpciones
    End Function
    Private Sub PreencherGridParametroOpciones(objRespuestaGetOpcion As ContractoServicio.Parametro.GetParametroOpciones.Respuesta)
        If objRespuestaGetOpcion.Opciones.Count > 0 Then
            'Carrega os TerminosMedioPago no GridView            
            Dim objDT As DataTable
            objDT = ProsegurGridView2.ConvertListToDataTable(objRespuestaGetOpcion.Opciones)
            ProsegurGridView2.CarregaControle(objDT)
            ParametroOpcionTemporario = objRespuestaGetOpcion.Opciones
        End If
    End Sub
    Private Sub PreencherTela()

        Dim objRespuestaGetParametroDetail As IAC.ContractoServicio.Parametro.GetParametroDetail.Respuesta
        Dim objRespuestaGetOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Respuesta

        objRespuestaGetParametroDetail = getParametroDetail()
        Parametro = objRespuestaGetParametroDetail.Parametro

        If Not Master.ControleErro.VerificaErro(objRespuestaGetParametroDetail.CodigoError, objRespuestaGetParametroDetail.NombreServidorBD, objRespuestaGetParametroDetail.MensajeError) Then
            MyBase.MostraMensagem(objRespuestaGetParametroDetail.MensajeError)
            Exit Sub
        End If

        'Armazena o tipo de componente.
        TipoControle = objRespuestaGetParametroDetail.Parametro.NecTipoComponente

        If (objRespuestaGetParametroDetail.Parametro.NecTipoComponente = ContractoServicio.Parametro.TipoComponente.Combobox) Then

            objRespuestaGetOpcion = getParametroOpcion()

            If Not Master.ControleErro.VerificaErro(objRespuestaGetOpcion.CodigoError, objRespuestaGetOpcion.NombreServidorBD, objRespuestaGetOpcion.MensajeError) Then
                MyBase.MostraMensagem(objRespuestaGetOpcion.MensajeError)
                Exit Sub
            End If

            PreencherGridParametroOpciones(objRespuestaGetOpcion)

            pnlOpciones.Visible = True
            btnNovo.Visible = True
            btnNovo.Enabled = True
        End If


        CodigoNivel = objRespuestaGetParametroDetail.Parametro.Nivel.CodigoNivel
        'preenche combo agrupaciones
        PreencherComboBoxAgrupaciones()

        ddlAgrupacion.SelectedIndex = ddlAgrupacion.Items.IndexOf(ddlAgrupacion.Items.FindByValue(objRespuestaGetParametroDetail.Parametro.Agrupacion.DescripcionCorta))
        ddlAgrupacion.ToolTip = ddlAgrupacion.SelectedItem.Text

        txtAplicacion.Text = objRespuestaGetParametroDetail.Parametro.Aplicacion.CodigoAplicacion
        txtAplicacion.ToolTip = objRespuestaGetParametroDetail.Parametro.Aplicacion.CodigoAplicacion

        txtNivel.Text = objRespuestaGetParametroDetail.Parametro.Nivel.DescripcionNivel
        txtNivel.ToolTip = objRespuestaGetParametroDetail.Parametro.Nivel.DescripcionNivel

        txtCodParametro.Text = objRespuestaGetParametroDetail.Parametro.CodParametro
        txtCodParametro.ToolTip = objRespuestaGetParametroDetail.Parametro.CodParametro

        txtDescripcionCorto.Text = objRespuestaGetParametroDetail.Parametro.DesCortaParametro
        txtDescripcionLarga.Text = objRespuestaGetParametroDetail.Parametro.DesLargaParametro

        txtTipoComponent.Text = objRespuestaGetParametroDetail.Parametro.NecTipoComponente.ToString
        txtTipoComponent.ToolTip = objRespuestaGetParametroDetail.Parametro.NecTipoComponente.ToString

        lblValueTipoDado.Text = objRespuestaGetParametroDetail.Parametro.NecTipoDato.ToString
        lblValueObligatorio.Text = If(objRespuestaGetParametroDetail.Parametro.BolObligatorio, Traduzir(Aplicacao.Util.Utilidad.GenOpcionSi), Traduzir(Aplicacao.Util.Utilidad.GenOpcionNo))
        txtOrden.Text = objRespuestaGetParametroDetail.Parametro.NecOrden

    End Sub
    Public Function getAgrupaciones() As IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta
        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta
        Dim objPeticionGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Peticion

        objPeticionGetAgrupaciones.CodigoAplicacion = CodAplicacion
        objPeticionGetAgrupaciones.CodigoNivel = CodigoNivel

        objRespuestaGetAgrupaciones = objProxyParametro.GetAgrupaciones(objPeticionGetAgrupaciones)

        Return objRespuestaGetAgrupaciones
    End Function

    Private Sub SetParamentroOpcionColecaoPopUp()

        'Passa a coleção com o objeto de opciones de parametro
        Session("colOpcionParametro") = ParametroOpcionTemporario

    End Sub
    Public Sub ConsomeParametroOpcion()

        If Session("objParametroOpcion") IsNot Nothing Then

            Dim objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion
            objParametroOpcion = DirectCast(Session("objParametroOpcion"), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)

            'Se existe o TerminoMedioPago na coleção
            If Not VerificarParametroOpcionExiste(ParametroOpcionTemporario, objParametroOpcion.CodigoOpcion) Then
                ParametroOpcionTemporario.Add(objParametroOpcion)
            Else
                ModificaParametroOpcion(ParametroOpcionTemporario, objParametroOpcion)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If

            'Carrega os TerminosMedioPago no GridView
            Dim objDT As DataTable
            objDT = ProsegurGridView2.ConvertListToDataTable(ParametroOpcionTemporario)
            ProsegurGridView2.CarregaControle(objDT)

            Session("objParametroOpcion") = Nothing

        End If
    End Sub
    Private Function ModificaParametroOpcion(ByRef objParametroOpcionColeccion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion) As Boolean

        Dim retorno = From c In objParametroOpcionColeccion Where c.CodigoOpcion = objParametroOpcion.CodigoOpcion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objParametroOpcionTmp As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)

            'Campos Texto
            objParametroOpcionTmp.DescripcionOpcion = objParametroOpcion.DescripcionOpcion
            objParametroOpcionTmp.EsVigente = objParametroOpcion.EsVigente

            objParametroOpcionTmp.CodDelegacion = objParametroOpcion.CodDelegacion

            Return True
        End If

    End Function
    Private Function VerificarParametroOpcionExiste(objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, codigoParametroOpcion As String) As Boolean

        Dim retorno = From c In objParametroOpcion Where c.CodigoOpcion = codigoParametroOpcion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Sub SetParamentroOpcionSelecionadoPopUp(Codigo As String)

        'Cria o ParamentroOpcion para ser consumido na página de ParamentroOpcion
        Dim objParametroOpcion As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion
        objParametroOpcion = RetornaobjParamentroOpcionExiste(ParametroOpcionTemporario, Codigo)

        'Envia o TerminoMedioPago para ser consumido pela PopUp de Mantenimento de TerminoMedioPago
        Session("setParametroOpcion") = objParametroOpcion

    End Sub
    Private Function RetornaobjParamentroOpcionExiste(ByRef objParametroOpcion As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion, codigoParametroOpcion As String) As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion

        Dim retorno = From c In objParametroOpcion Where c.CodigoOpcion = codigoParametroOpcion

        If retorno Is Nothing OrElse retorno.Count = 0.0R Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function
#End Region
#Region "[Métodos Formulario]"
    Private Sub PreencherComboBoxAgrupaciones()
        Dim objRespuestaGetAgrupaciones As New IAC.ContractoServicio.Parametro.GetAgrupaciones.Respuesta

        objRespuestaGetAgrupaciones = getAgrupaciones()

        If Not Master.ControleErro.VerificaErro(objRespuestaGetAgrupaciones.CodigoError, objRespuestaGetAgrupaciones.NombreServidorBD, objRespuestaGetAgrupaciones.MensajeError) Then
            MyBase.MostraMensagem(objRespuestaGetAgrupaciones.MensajeError)
            Exit Sub
        End If

        ddlAgrupacion.DataSource = Nothing

        ' Caso exista apenas um registro a mesmo será aplicado como default
        If objRespuestaGetAgrupaciones.Agrupaciones.Count > 1 Then
            ddlAgrupacion.AppendDataBoundItems = True
            ddlAgrupacion.Items.Clear()
            ddlAgrupacion.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
        End If
        ddlAgrupacion.DataTextField = "DescripcionCorta"
        ddlAgrupacion.DataValueField = "DescripcionCorta"
        ddlAgrupacion.DataSource = objRespuestaGetAgrupaciones.Agrupaciones
        ddlAgrupacion.DataBind()

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

    Public Function setParametro() As IAC.ContractoServicio.Parametro.SetParametro.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaSetParametro As New IAC.ContractoServicio.Parametro.SetParametro.Respuesta
        Dim objPeticionSetParametro As New IAC.ContractoServicio.Parametro.SetParametro.Peticion

        objPeticionSetParametro.CodigoAplicacion = CodAplicacion
        objPeticionSetParametro.CodigoParametro = CodParametro
        objPeticionSetParametro.CodigoNivel = CodigoNivel
        objPeticionSetParametro.DescripcionAgrupacion = DescripcionAgrupacion
        objPeticionSetParametro.DescripcionCortaParametro = DescripcionCortaParametro
        objPeticionSetParametro.DescripcionLargaParametro = DescripcionLargaParametro
        objPeticionSetParametro.NecOrden = If(String.IsNullOrEmpty(NecOrden), "0", NecOrden)
        objPeticionSetParametro.CodigoUsuario = MyBase.LoginUsuario

        ' ParametroOpcionTemporario
        objPeticionSetParametro.ParametroOpciones = ParametroOpcionTemporario
        objRespuestaSetParametro = objProxyParametro.SetParametro(objPeticionSetParametro)

        Return objRespuestaSetParametro

    End Function
    Private Sub LimparCamposForm()

        ddlAgrupacion.Items.Clear()
        txtAplicacion.Text = String.Empty
        txtNivel.Text = String.Empty
        txtCodParametro.Text = String.Empty
        txtDescripcionCorto.Text = String.Empty
        txtTipoComponent.Text = String.Empty
        lblValueTipoDado.Text = String.Empty
        lblValueObligatorio.Text = String.Empty
        txtOrden.Text = String.Empty

        ProsegurGridView2.DataSource = Nothing
        ProsegurGridView2.DataBind()

        CodAplicacion = Nothing
        CodParametro = Nothing
        DescripcionAgrupacion = Nothing
        DescripcionCortaParametro = Nothing
        DescripcionLargaParametro = Nothing
        NecOrden = Nothing
        ValidarCamposObrigatorios = Nothing
        ParametroOpcionTemporario = Nothing
        TipoControle = Nothing
        Parametro = Nothing

    End Sub

#End Region
#Region "[Eventos GridView Form]"
    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView2_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.EOnClickRowClientScript
        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("esvigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ProsegurGridView2_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView2.EPager_SetCss
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
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ProsegurGridView2_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView2.EPreencheDados

        Try
            Dim objDT As DataTable

            objDT = ProsegurGridView2.ConvertListToDataTable(ParametroOpcionTemporario)

            objDT.DefaultView.Sort = ProsegurGridView2.SortCommand

            ProsegurGridView2.ControleDataSource = objDT

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub

    Protected Sub ProsegurGridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_codigo")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_descripcion")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_mantenimiento_vigente")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ProsegurGridView2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowDataBound

        Try
            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigoForm.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditarForm"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultarForm"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBajaOpcion.ClientID & "');"
                CType(e.Row.Cells(0).FindControl("imgExcluirForm"), ImageButton).ToolTip = Traduzir("btnBaja")

                If Not e.Row.DataItem("descripcionopcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcionopcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("descripcionopcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("esvigente")) Then
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
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
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Utilidad.eAcao.Modificacion

                Dim codigos = codigo.Split("|")

                CodAplicacion = codigos(0)
                CodParametro = codigos(1)
                PreencherTela()
                pnForm.Visible = True
                pnForm.Enabled = True
                btnCancelar.Visible = True
                btnCancelar.Enabled = True
                btnABMAgrapaciones.Visible = True
                btnSalvar.Visible = True
                btnSalvar.Enabled = True

            End If
            ' Response.Redirect("~/MantenimientoParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&codaplicacion=" & Server.UrlEncode(codigos(0)) & "&codparametro=" & Server.UrlEncode(codigos(1)), False)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[Botoes da tela]"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codparametro=" & CodParametro & "&codaplicacion=" & CodAplicacion

            'Seta a session com a coleção de Opciones que será consumida na abertura da PopUp de Mantenimiento de Opciones
            SetParamentroOpcionColecaoPopUp()

            ' limpar sessao das telas seguintes
            Session("objColOpcionParametro") = Nothing

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("029_titulo_mantenimiento_parametroopcion"), 180, 700, False, True, btnConsomeParametroOpcion.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    Private Sub btnConsomeParametroOpcion_Click(sender As Object, e As EventArgs) Handles btnConsomeParametroOpcion.Click
        Try
            ConsomeParametroOpcion()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView2.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView2.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView2.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

                'Seta a session com o ParamentroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParamentroOpcion
                SetParamentroOpcionSelecionadoPopUp(codigo)

                'AbrirPopupModal
                Master.ExibirModal(url, Traduzir("029_titulo_mantenimiento_parametroopcion"), 180, 700, False, True, btnConsomeParametroOpcion.ClientID)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditarForm_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView2.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView2.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView2.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                Dim url As String = "MantenimientoOpcionesParametro.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&codparametro=" & CodParametro

                'Seta a session com o ParamentroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParamentroOpcion
                SetParamentroOpcionSelecionadoPopUp(codigo)

                'Seta a session com a coleção de ParametroOpcion que será consmida na abertura da PopUp de Mantenimiento de ParametroOpcion
                SetParamentroOpcionColecaoPopUp()

                'AbrirPopupModal
                Master.ExibirModal(url, Traduzir("029_titulo_mantenimiento_parametroopcion"), 180, 700, False, True, btnConsomeParametroOpcion.ClientID)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaOpcion_Click(sender As Object, e As EventArgs) Handles btnBajaOpcion.Click
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView2.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView2.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigoForm.Value.ToString())
                End If

                'Modifica o Termino Medio de Pago para exclusão
                Dim objParametroOpcionRetorno As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion = RetornaobjParamentroOpcionExiste(ParametroOpcionTemporario, codigo)
                objParametroOpcionRetorno.EsVigente = False

                'Carrega os Terminos no GridView
                Dim objDT As DataTable
                objDT = ProsegurGridView2.ConvertListToDataTable(ParametroOpcionTemporario)
                ProsegurGridView2.CarregaControle(objDT)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            ValidarCamposObrigatorios = True

            Dim erros As String = MontaMensagensErro(True)
            If Not String.IsNullOrEmpty(erros) Then
                MyBase.MostraMensagem(erros)
                Exit Sub
            End If

            DescripcionAgrupacion = ddlAgrupacion.SelectedValue
            DescripcionCortaParametro = txtDescripcionCorto.Text
            DescripcionLargaParametro = txtDescripcionLarga.Text

            If String.IsNullOrEmpty(txtOrden.Text) OrElse IsNumeric(txtOrden.Text) Then
                NecOrden = txtOrden.Text
            Else
                ' exibir mensagem ao usuário
                MyBase.MostraMensagem(Traduzir("err_passagem_parametro") & " - " & lblOrden.Text)
                txtOrden.Focus()
                Exit Sub
            End If

            Dim objRespuestaSetParametro As IAC.ContractoServicio.Parametro.SetParametro.Respuesta

            objRespuestaSetParametro = setParametro()

            If Not Master.ControleErro.VerificaErro(objRespuestaSetParametro.CodigoError, objRespuestaSetParametro.NombreServidorBD, objRespuestaSetParametro.MensajeError) Then
                MyBase.MostraMensagem(objRespuestaSetParametro.MensajeError)
                Exit Sub
            End If

            MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))

            btnCancelar_Click(Nothing, Nothing)
            ExecutarBusca()
            UpdatePanelGrid.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class

