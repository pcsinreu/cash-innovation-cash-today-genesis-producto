Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Plantas 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 20/02/13 - Criado </history>
Public Class BusquedaPlanta
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
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        ddlDelegciones.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtCodigoPlanta.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        txtCodigoPlantaForm.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoPlanta.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoPlanta.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoPlanta.ClientID & "').focus();return false;}} else {return true}; ")
        ddlDelegacion.Attributes.Add("onKeyDown", "BloquearColar();")

        
    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
        
    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DELEGACION
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Planta")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnImporteMaximo.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                'Preenche Combos
                PreencherComboDelegaciones()

            End If

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
                ControleBotoes()
                btnAjeno.Attributes.Add("style", "margin-left: 15px;")
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

        Master.Titulo = Traduzir("014_titulo_busqueda_delegaciones")
        lblCodigoPlanta.Text = Traduzir("014_lbl_codigo_planta")
        lblDelegacion.Text = Traduzir("014_lbl_delegaciones")
        lblDescricao.Text = Traduzir("014_lbl_descricao")
        lblVigente.Text = Traduzir("014_lbl_vigente")
        lblSubTitulosPlantas.Text = Traduzir("014_lbl_Plantas")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("014_lbl_criterio")
        csvDelegacion.ErrorMessage = Traduzir("msg_erro_campo_delegacion")
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
        btnAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnImporteMaximo.Text = Traduzir("btnImporteMaximo")
        btnImporteMaximo.ToolTip = Traduzir("btnImporteMaximo")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("014_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("014_lbl_grd_descricao")
        GdvResultado.Columns(3).HeaderText = Traduzir("014_lbl_grd_Ativo")

        'Formulario
        lblCodigoPlantaForm.Text = Traduzir("014_lbl_codigoPlanta")
        lblDelegacionForm.Text = Traduzir("014_lbl_Delegacion")
        lblDescricaoPlanta.Text = Traduzir("014_lbl_DescPlanta")
        lblTituloPlanta.Text = Traduzir("014_titulo_mantenimiento_Planta")
        lblVigenteForm.Text = Traduzir("014_lbl_Vigente")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("019_lbl_descricaoAjeno")
        csvCodigoPlanta.ErrorMessage = Traduzir("014_msg_CodigoPlanta")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("014_msg_PlantaDescripcion")
        csvDelegacionForm.ErrorMessage = Traduzir("014_msg_Delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("053_msg_erro_PlantaExistente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
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

    Property FiltroCodigo() As String
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As String)
            ViewState("FiltroCodigo") = value
        End Set
    End Property

    Property FiltroCodigoPlaneta() As String
        Get
            Return ViewState("FiltroCodigoPlanta")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoPlanta") = value
        End Set
    End Property

    Property Delegaciones() As ContractoServicio.Planta.GetPlanta.PlantaColeccion
        Get
            If ViewState("Planta") Is Nothing Then
                ViewState("Planta") = New ContractoServicio.Planta.GetPlanta.PlantaColeccion
            End If
            Return ViewState("Planta")
        End Get
        Set(value As ContractoServicio.Planta.GetPlanta.PlantaColeccion)
            ViewState("Planta") = value
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

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca as plantas de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Public Function getPlantas() As IAC.ContractoServicio.Planta.GetPlanta.Respuesta

        Dim objProxyPlanta As New Comunicacion.ProxyPlanta
        Dim objPeticionPlanta As New IAC.ContractoServicio.Planta.GetPlanta.Peticion
        Dim objRespuestaPlanta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta

        'Recebe os valores do filtro
        objPeticionPlanta.BolActivo = FiltroVigente
        objPeticionPlanta.CodPlanta = FiltroCodigoPlaneta.ToString()
        objPeticionPlanta.oidDelegacion = FiltroCodigo.ToString()
        objPeticionPlanta.DesPlanta = FiltroDescripcion.ToString()
        objPeticionPlanta.ParametrosPaginacion.RealizarPaginacion = False

        objRespuestaPlanta = objProxyPlanta.GetPlantas(objPeticionPlanta)

        Return objRespuestaPlanta

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
        Dim objRespostaPlantas As IAC.ContractoServicio.Planta.GetPlanta.Respuesta

        'Busca as divisas
        objRespostaPlantas = getPlantas()

        If Not Master.ControleErro.VerificaErro(objRespostaPlantas.CodigoError, objRespostaPlantas.NombreServidorBD, objRespostaPlantas.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespostaPlantas.Planta.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaPlantas.Planta.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespostaPlantas.Planta)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodPlanta ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodPlanta ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespostaPlantas.Planta)
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

        Dim objProxyDelegacion As New ProxyDelegacion
        Dim objRespuesta As New IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Delegacion.GetDelegacion.Peticion

        Dim objProxyPais As New ProxyPaises
        Dim objRespuestaPais As New IAC.ContractoServicio.Pais.GetPais.Respuesta

        objRespuestaPais = objProxyPais.GetPais()
        objPeticion.BolVigente = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxyDelegacion.GetDelegaciones(objPeticion)

        For Each delegacion In objRespuesta.Delegacion
            Dim dx = delegacion
            Dim pais = objRespuestaPais.Pais.FirstOrDefault(Function(f) f.OidPais = dx.OidPais)
            If pais IsNot Nothing Then
                dx.DesDelegacion = pais.DesPais & " - " & dx.DesDelegacion
            End If
        Next

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlDelegciones.AppendDataBoundItems = True
        ddlDelegciones.Items.Clear()
        ddlDelegciones.Items.Add(New ListItem(Traduzir("014_ddl_Selecionar"), String.Empty))
        ddlDelegciones.DataTextField = "DesDelegacion"
        ddlDelegciones.DataValueField = "OidDelegacion"
        ddlDelegciones.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)
        ddlDelegciones.DataBind()

    End Sub

    Public Function getPlantaDetail(codigoPlanta As String) As IAC.ContractoServicio.Planta.GetPlantaDetail.Planta

        Dim objProxyPlanta As New ProxyPlanta
        Dim objPeticionPlanta As New IAC.ContractoServicio.Planta.GetPlantaDetail.Peticion
        Dim objRespuestaPlanta As New IAC.ContractoServicio.Planta.GetPlantaDetail.Respuesta

        objPeticionPlanta.OidPlanta = codigoPlanta

        objRespuestaPlanta = objProxyPlanta.GetPlantaDetail(objPeticionPlanta)

        Return objRespuestaPlanta.Planta

    End Function

    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        ValidarCamposObrigatorios = True

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                If ddlDelegciones.Visible AndAlso ddlDelegciones.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacion.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDelegciones.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacion.IsValid = True
                End If

            End If

        End If

        Return strErro.ToString()

    End Function
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
    ''' [pgoncalves] 20/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

        Try
            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvResultado.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("BolActivo").ToString.ToLower & ");"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função faz a ordenção do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getPlantas().Planta)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " CodPlanta ASC "
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
    ''' [pgoncalves] 20/02/2013 Criado
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
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("OidPlanta")) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If CBool(e.Row.DataItem("BolActivo")) Then
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_codigo")
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_descricao")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("014_lbl_grd_Ativo")
        End If
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            txtCodigoPlanta.Text = String.Empty
            txtDescricao.Text = String.Empty
            chkVigente.Checked = True

         'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            PreencherComboDelegaciones()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            btnCancelar_Click(sender, e)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 20/02/2013 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            btnCancelar_Click(Nothing, Nothing)
            ValidarCamposObrigatorios = True

            Dim MensagemErro As String = MontaMensagensErro()

            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If

            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            Dim strListCodPlanta As String
            Dim strListCodigo As String
            Dim strListDescricao As String

            strListCodigo = ddlDelegciones.SelectedValue
            strListCodPlanta = txtCodigoPlanta.Text
            strListDescricao = txtDescricao.Text.Trim

            'Filtros
            FiltroCodigo = strListCodigo
            FiltroCodigoPlaneta = strListCodPlanta
            FiltroDescripcion = strListDescricao
            FiltroVigente = chkVigente.Checked

            'Retorna os canais de acordo com o filtro aciam
            PreencherDelegaciones()

            UpdatePanelGeral.Update()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnImporteMaximo.Visible = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncales] 20/02/2013 Criado
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
                Dim objProxyPlanta As New Comunicacion.ProxyPlanta
                Dim objRespuestaPlanta As IAC.ContractoServicio.Planta.SetPlanta.Respuesta

                Dim objColPlanta As IAC.ContractoServicio.Planta.GetPlantaDetail.Planta
                objColPlanta = getPlantaDetail(strCodigo)

                'Criando um Termino para exclusão
                Dim objPeticionPlanta As New IAC.ContractoServicio.Planta.SetPlanta.Peticion
                objPeticionPlanta.CodPlanta = objColPlanta.CodPlanta
                objPeticionPlanta.BolActivo = False
                objPeticionPlanta.DesPlanta = objColPlanta.DesPlanta
                objPeticionPlanta.DesUsuarioCreacion = objColPlanta.DesUsuarioCreacion
                objPeticionPlanta.DesUsuarioModificacion = MyBase.LoginUsuario
                objPeticionPlanta.OidDelegacion = objColPlanta.OidDelegacion
                objPeticionPlanta.OidPlanta = objColPlanta.OidPlanta

                objRespuestaPlanta = objProxyPlanta.SetPlantas(objPeticionPlanta)

                If Master.ControleErro.VerificaErro(objRespuestaPlanta.CodigoError, objRespuestaPlanta.NombreServidorBD, objRespuestaPlanta.MensajeError) Then
                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    If GdvResultado.Rows.Count > 0 Then
                        btnBuscar_Click(Nothing, Nothing)
                    End If
                    btnCancelar_Click(Nothing, Nothing)
                Else

                    If objRespuestaPlanta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaPlanta.MensajeError)
                    End If

                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


#End Region

    'Limpa o Grid após da seleção do item na combobox
    Protected Sub ddlDelegciones_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegciones.SelectedIndexChanged
        ddlDelegciones.ToolTip = ddlDelegciones.SelectedItem.Text

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
                ddlDelegciones.SelectedIndex = -1
                txtCodigoPlanta.Text = String.Empty
                txtDescricao.Text = String.Empty

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub

#End Region

#Region "Propriedades Formulario"

#End Region
#Region "Métodos Formulario"
    Public Sub PreencherComboDelegacione()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objProxyDelegacione As New ProxyDelegacion
        Dim objRespuesta As New IAC.ContractoServicio.Delegacion.GetDelegacion.Respuesta

        Dim objProxyPais As New Comunicacion.ProxyPaises
        Dim objRespuestaPais As New IAC.ContractoServicio.Pais.GetPais.Respuesta

        objRespuestaPais = objProxyPais.GetPais()
        objPeticion.BolVigente = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxyDelegacione.GetDelegaciones(objPeticion)

        For Each delegacion In objRespuesta.Delegacion
            Dim dx = delegacion
            Dim pais = objRespuestaPais.Pais.FirstOrDefault(Function(f) f.OidPais = dx.OidPais)
            If pais IsNot Nothing Then
                dx.DesDelegacion = pais.DesPais & " - " & dx.DesDelegacion
            End If
        Next

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlDelegacion.AppendDataBoundItems = True
        ddlDelegacion.Items.Clear()
        ddlDelegacion.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlDelegacion.DataTextField = "DesDelegacion"
        ddlDelegacion.DataValueField = "OidDelegacion"
        ddlDelegacion.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)

        ddlDelegacion.DataBind()
    End Sub
    Private Sub LimparCampos()
        PreencherComboDelegacione()
        txtCodigoPlantaForm.Text = String.Empty
        txtDescricaoPlanta.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        'CodigosAjenosPeticion = Nothing
        'ImportesMaximoPeticion = Nothing
        'OidPlanta = String.Empty
        'Planta = Nothing
    End Sub
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyPlanta As New Comunicacion.ProxyPlanta
            Dim objRespuestaPlanta As IAC.ContractoServicio.Planta.SetPlanta.Respuesta

            Dim objPlanta As New IAC.ContractoServicio.Planta.SetPlanta.Peticion

            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErroForm(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPlanta.BolActivo = True
            Else
                objPlanta.BolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objPlanta.CodPlanta = txtCodigoPlantaForm.Text
            objPlanta.DesPlanta = txtDescricaoPlanta.Text.Trim
            objPlanta.OidDelegacion = ddlDelegacion.SelectedValue
            objPlanta.OidPlanta = OidPlanta

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPlanta.GmtCreacion = DateTime.Now
                objPlanta.DesUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPlanta.DesUsuarioCreacion = Planta.DesUsuarioCreacion
            End If

            objPlanta.GmtModificacion = DateTime.Now
            objPlanta.DesUsuarioModificacion = MyBase.LoginUsuario

            objPlanta.CodigosAjenos = CodigosAjenosPeticion

            objPlanta.ImporteMaximo = ImportesMaximoPeticion

            objRespuestaPlanta = objProxyPlanta.SetPlantas(objPlanta)

            Dim url As String = "BusquedaPlanta.aspx"

            Session.Remove("objRespuestaGEPR_PLANTA")
            Session.Remove("ImporteMaximoEditar")

            If Master.ControleErro.VerificaErro(objRespuestaPlanta.CodigoError, objRespuestaPlanta.NombreServidorBD, objRespuestaPlanta.MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                If GdvResultado.Rows.Count > 0 Then
                    btnBuscar_Click(Nothing, Nothing)
                End If
                btnCancelar_Click(Nothing, Nothing)
            Else
                If objRespuestaPlanta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaPlanta.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o codigo da planta foi enviado
                If txtCodigoPlantaForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoPlanta.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoPlanta.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoPlantaForm.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoPlanta.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoPlanta.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoPlanta.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a delegação foi selecionado
                If ddlDelegacion.Visible AndAlso ddlDelegacion.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacionForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacionForm.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacionForm.IsValid = True
                End If

            End If


            'Verifica se o código existe
            If (Not String.IsNullOrEmpty(txtCodigoPlantaForm.Text) AndAlso Not String.IsNullOrEmpty(ddlDelegacion.SelectedValue)) AndAlso ExisteCodigoPlanta(txtCodigoPlantaForm.Text.Trim(), ddlDelegacion.SelectedValue) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    csvCodigoExistente.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoPlanta(codigo As String, delegacao As String) As Boolean

        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objPeticion As New IAC.ContractoServicio.Planta.GetPlanta.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta
            Dim objProxyPlanta As New Comunicacion.ProxyPlanta

            objPeticion.oidDelegacion = delegacao
            objPeticion.CodPlanta = codigo
            objPeticion.BolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxyPlanta.GetPlantas(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Planta.Count > 0 Then
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Public Sub CarregaDados(codPlanta As String)
        Dim objColPlanta As IAC.ContractoServicio.Planta.GetPlantaDetail.Planta
        Dim itemSelecionado As ListItem

        objColPlanta = getPlantaDetail(codPlanta)

        Planta = objColPlanta

        If objColPlanta IsNot Nothing Then

            Dim iCodigoAjeno = (From item In objColPlanta.CodigosAjenos
                               Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                txtDesCodigoAjeno.ToolTip = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            txtCodigoPlantaForm.Text = objColPlanta.CodPlanta
            txtCodigoPlantaForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColPlanta.CodPlanta, String.Empty)

            txtDescricaoPlanta.Text = objColPlanta.DesPlanta
            txtDescricaoPlanta.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColPlanta.DesPlanta, String.Empty)

            If objColPlanta.BolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            chkVigenteForm.Checked = objColPlanta.BolActivo

            EsVigente = objColPlanta.BolActivo
            OidPlanta = objColPlanta.OidPlanta

            Session("ImporteMaximoEditar") = objColPlanta.ImportesMaximos
            ddlDelegacion.SelectedValue = objColPlanta.OidDelegacion

            'Seleciona o valor
            itemSelecionado = ddlDelegacion.Items.FindByValue(objColPlanta.OidDelegacion)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

        End If

    End Sub
#End Region
#Region "Eventos Formulario"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            Session("ImporteMaximoEditar") = Nothing
            Planta = Nothing
            ImportesMaximoPeticion = Nothing
            OidPlanta = String.Empty
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            'btnImporteMaximo.Visible = True
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Alta
            ddlDelegacion.Focus()
            txtCodigoPlantaForm.Enabled = True
            txtDescricao.Enabled = True
            ddlDelegacion.Enabled = True
            txtDescricaoPlanta.Enabled = True
            txtCodigoAjeno.Enabled = True
            txtDesCodigoAjeno.Enabled = True
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnImporteMaximo.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Session.Remove("ImporteMaximoEditar")
            CodigosAjenosPeticion = Nothing
            ImportesMaximoPeticion = Nothing
            OidPlanta = String.Empty
            Planta = Nothing

            Acao = Utilidad.eAcao.Inicial
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



    Private Sub btnAjeno_Click(sender As Object, e As EventArgs) Handles btnAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoPlantaForm.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoPlanta.Text
            tablaGenesis.OidTablaGenesis = OidPlanta
            If Planta IsNot Nothing AndAlso Planta.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Planta.CodigosAjenos
            End If

            Session("objGEPR_TPLANTA") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPLANTA"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPLANTA"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TPLANTA") IsNot Nothing Then

                If Planta Is Nothing Then
                    Planta = New ContractoServicio.Planta.GetPlantaDetail.Planta
                End If

                Planta.CodigosAjenos = Session("objRespuestaGEPR_TPLANTA")
                Session.Remove("objRespuestaGEPR_TPLANTA")

                Dim iCodigoAjeno = (From item In Planta.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If Planta.CodigosAjenos IsNot Nothing Then
                    CodigosAjenosPeticion = Planta.CodigosAjenos
                Else
                    CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnImporteMaximo.Click
        Try
            Dim url As String = String.Empty
            Dim oidImporteMaximo As String = ""

            If Planta IsNot Nothing AndAlso Planta.ImportesMaximos IsNot Nothing AndAlso Planta.ImportesMaximos.Count > 0 Then
                oidImporteMaximo = Planta.ImportesMaximos.First.OidImporteMaximo
                If Session("ImporteMaximoEditar") Is Nothing Then
                    Session("ImporteMaximoEditar") = Planta.ImportesMaximos
                End If
            End If

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&codimporte=" & txtCodigoPlantaForm.Text & "&descImporte=" & txtDescricaoPlanta.Text & "&oidimporte=" & oidImporteMaximo
            ElseIf (Aplicacao.Util.Utilidad.eAcao.Modificacion = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoPlantaForm.Text & "&descImporte=" & txtDescricaoPlanta.Text & "&oidimporte=" & oidImporteMaximo
            ElseIf (Aplicacao.Util.Utilidad.eAcao.Alta = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoPlantaForm.Text & "&descImporte=" & txtDescricaoPlanta.Text
            End If
            Master.ExibirModal(url, Traduzir("043_lbl_titulo"), 550, 900, False, True, btnConsomeImporteMaximo.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnConsomeImporteMaximo.Click
        Try
            If Session("objRespuestaImporte") IsNot Nothing Then

                If Planta Is Nothing Then
                    Planta = New ContractoServicio.Planta.GetPlantaDetail.Planta
                End If

                Planta.ImportesMaximos = Session("objRespuestaImporte")
                Session.Remove("objRespuestaImporte")
                Session.Remove("ImporteMaximoEditar")
                If Planta.ImportesMaximos IsNot Nothing Then
                    ImportesMaximoPeticion = Planta.ImportesMaximos
                Else
                    ImportesMaximoPeticion = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
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
                'btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoPlantaForm.Enabled = False
                ddlDelegacion.Enabled = True
                txtDescricaoPlanta.Enabled = True
                txtDescricaoPlanta.Focus()

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
                ' btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
                txtCodigoPlantaForm.Enabled = False
                txtDescricaoPlanta.Enabled = False
                ddlDelegacion.Enabled = False

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
                ' btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
                txtCodigoPlantaForm.Enabled = False
                ddlDelegacion.Enabled = False

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region
#Region "[PROPRIEDADES FORMULARIO]"

    Private Property OidPlanta As String
        Get
            Return ViewState("OidPlanta")
        End Get
        Set(value As String)
            ViewState("OidPlanta") = value
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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property Planta() As ContractoServicio.Planta.GetPlantaDetail.Planta
        Get
            Return DirectCast(ViewState("Planta"), ContractoServicio.Planta.GetPlantaDetail.Planta)
        End Get
        Set(value As ContractoServicio.Planta.GetPlantaDetail.Planta)
            ViewState("Planta") = value
        End Set
    End Property

    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

    Private Property ImportesMaximoPeticion() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Get
            Return DirectCast(ViewState("ImportesMaximoPeticion"), ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
            ViewState("ImportesMaximoPeticion") = value
        End Set

    End Property



#End Region


    
End Class