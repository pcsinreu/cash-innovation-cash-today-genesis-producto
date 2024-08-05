Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Tipo Procedencia 
''' </summary>
''' <remarks></remarks>
''' <history>[danielnunes] 15/05/13 - Criado </history>
Public Class BusquedaTipoProcedencia
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
       
    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodTipoProcedencia.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        'Adicionar scripts na página
        txtCodigoTipoProcedencia.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoProcedencia.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipoProcedencia.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipoProcedencia.ClientID & "').focus();return false;}} else {return true}; ")


    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOPROCEDENCIA
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
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

                ExecutarBusca()

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

                txtCodTipoProcedencia.Focus()
            End If

            ' TrataFoco()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("040_lbl_titulo_pagina")
        lblCodTipoProcedencia.Text = Traduzir("040_lbl_codigo_tipoProcedencia")
        lblDescripcion.Text = Traduzir("040_lbl_descricao_tipo")
        lblVigente.Text = Traduzir("040_lbl_Tipo_vigente")
        lblResultadoTipoProcedencia.Text = Traduzir("040_lbl_tipo_procedencia")
        lblCriteriosBusqueda.Text = Traduzir("gen_lbl_CriterioBusca")
        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")

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
        GdvResultado.Columns(1).HeaderText = Traduzir("040_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("040_lbl_grd_descricao")
        GdvResultado.Columns(3).HeaderText = Traduzir("040_lbl_grd_Ativo")

        'Form
        lblCodTipoProcedenciaForm.Text = Traduzir("040_lbl_codigo_tipoProcedencia")
        lblDescripcionForm.Text = Traduzir("040_lbl_descricao_tipo")
        lblVigenteForm.Text = Traduzir("040_lbl_Tipo_vigente")
        lblTituloProcedencia.Text = Traduzir("040_lbl_titulo_pagina")
        csvCodigoTipoProcedencia.ErrorMessage = Traduzir("040_lbl_codigo_procedencia")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("040_lbl_decricao_procedencia")
        csvCodigoExistente.ErrorMessage = Traduzir("040_msg_codigTipoExistente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
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

    Property FiltroCodigoTipoProcedencia() As String
        Get
            Return ViewState("FiltroCodigoTipoProcedencia")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoTipoProcedencia") = value
        End Set
    End Property

    Property TipoProcedencia() As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedenciaColeccion
        Get
            Return DirectCast(ViewState("TipoProcedencia"), ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedenciaColeccion)
        End Get
        Set(value As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedenciaColeccion)
            ViewState("TipoProcedencia") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca os tipos procedencia de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Public Function getTipoProcedencia() As IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta

        Dim objProxyTipoProcedencia As New Comunicacion.ProxyTipoProcedencia
        Dim objPeticion As New IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta

        'Recebe os valores do filtro
        objPeticion.bolActivo = FiltroVigente
        objPeticion.codTipoProcedencia = FiltroCodigo.ToString()
        objPeticion.desTipoProcedencia = FiltroDescripcion.ToString()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyTipoProcedencia.getTiposProcedencia(objPeticion)
        TipoProcedencia = objRespuesta.TipoProcedencia
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
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Public Sub PreencherTipoProcedencia()

        Dim objRespuesta As IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta

        'Busca as divisas
        objRespuesta = getTipoProcedencia()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespuesta.TipoProcedencia.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.TipoProcedencia.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.TipoProcedencia)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codTipoProcedencia ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " codTipoProcedencia ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespuesta.TipoProcedencia)
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                GdvResultado.DataSource = objDt
                GdvResultado.DataBind()

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
    ''' [danielnunes] 15/05/2013 Criado
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
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        ' Dim objRespuesta As Object

        ' objRespuesta = getTipoProcedencia().TipoProcedencia

        objDT = GdvResultado.ConvertListToDataTable(getTipoProcedencia().TipoProcedencia)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " codTipoProcedencia ASC "
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
    ''' [danielnunes] 15/05/2013 Criado
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
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
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
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then

        End If
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            txtCodTipoProcedencia.Text = String.Empty
            txtDescricao.Text = String.Empty
            chkVigente.Checked = True
            pnlSemRegistro.Visible = False
            
            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            btnCancelar_Click(sender, e)
            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
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

        Dim strListCodigo As String
        Dim strListDescricao As String

        strListCodigo = txtCodTipoProcedencia.Text
        strListDescricao = txtDescricao.Text.Trim

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherTipoProcedencia()

        pnForm.Visible = False
        btnNovo.Enabled = True
        btnBajaConfirm.Visible = False
        btnCancelar.Enabled = False
        btnSalvar.Enabled = False
    End Sub

    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
    ''' </history>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    strCodigo = Server.UrlDecode(GdvResultado.getValorLinhaSelecionada)
                Else
                    strCodigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                'Retorna o valor da linha selecionada no grid

                Dim resultado As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia

                resultado = (From item In TipoProcedencia
                Where (item.codTipoProcedencia = strCodigo)
                            Select item).FirstOrDefault


                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyTipoProcedencia As New Comunicacion.ProxyTipoProcedencia
                Dim objRespuesta As IAC.ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta

                'Criando um Termino para exclusão
                Dim objPeticion As New IAC.ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion
                objPeticion.codTipoProcedencia = resultado.codTipoProcedencia
                objPeticion.bolActivo = False
                objPeticion.desTipoProcedencia = resultado.desTipoProcedencia

                objPeticion.desUsuarioCreacion = resultado.desUsuarioCreacion
                objPeticion.gmtCreacion = resultado.gmtCreacion

                objPeticion.desUsuarioModificacion = MyBase.LoginUsuario
                objPeticion.gmtModificacion = System.DateTime.Now

                objPeticion.oidTipoProcedencia = resultado.oidTipoProcedencia

                objRespuesta = objProxyTipoProcedencia.setTiposProcedencia(objPeticion)

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    'Registro excluido com sucesso
                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

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
    
#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração dos estados da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 15/05/2013 Criado
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
                txtCodTipoProcedencia.Text = String.Empty
                txtDescricao.Text = String.Empty

            Case Aplicacao.Util.Utilidad.eAcao.Busca


        End Select

    End Sub

#End Region

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta

            txtCodigoTipoProcedencia.Text = String.Empty
            txtDescricaoTipoProcedencia.Text = String.Empty
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            TipoProcedenciaForm = Nothing
            OidTipoProcedencia = String.Empty
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            txtCodigoTipoProcedencia.Enabled = True
            txtCodigoTipoProcedencia.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            txtCodigoTipoProcedencia.Text = String.Empty
            txtDescricaoTipoProcedencia.Text = String.Empty
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            TipoProcedenciaForm = Nothing
            OidTipoProcedencia = String.Empty
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

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub
#Region "Métodos Formulário"
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTipoProcedencia As New Comunicacion.ProxyTipoProcedencia
            Dim objRespuesta As IAC.ContractoServicio.TipoProcedencia.SetTiposProcedencias.Respuesta

            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro()

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objPeticion.codTipoProcedencia = txtCodigoTipoProcedencia.Text.Trim
            objPeticion.desTipoProcedencia = txtDescricaoTipoProcedencia.Text
            objPeticion.oidTipoProcedencia = OidTipoProcedencia

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoProcedenciaForm.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoProcedenciaForm.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxyTipoProcedencia.setTiposProcedencia(objPeticion)


            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                ExecutarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
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

            'Verifica se o campo é obrigatório quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o codigo da planta foi enviado
                If txtCodigoTipoProcedencia.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoProcedencia.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoProcedencia.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoProcedencia.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoProcedencia.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipoProcedencia.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipoProcedencia.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoTipoProcedencia.Text.Trim) AndAlso ExisteCodigoProcedencia(txtCodigoTipoProcedencia.Text.Trim) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoProcedencia.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoProcedencia(codigo As String) As Boolean

        Try

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoProcedencia

            objPeticion.codTipoProcedencia = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposProcedencia(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoProcedencia.Count > 0 Then
                    Return True
                End If
            Else
                Return False
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    Public Sub CarregaDados(codTipoProcedencia As String)

        Dim objPeticion As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion
        Dim objRespuesta As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoProcedencia

        objPeticion.codTipoProcedencia = codTipoProcedencia
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposProcedencia(objPeticion)
        TipoProcedenciaForm = objRespuesta.TipoProcedencia(0)

        If objRespuesta.TipoProcedencia.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoProcedencia.Text = objRespuesta.TipoProcedencia(0).codTipoProcedencia
            txtCodigoTipoProcedencia.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoProcedencia(0).codTipoProcedencia, String.Empty)

            txtDescricaoTipoProcedencia.Text = objRespuesta.TipoProcedencia(0).desTipoProcedencia
            txtDescricaoTipoProcedencia.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoProcedencia(0).desTipoProcedencia, String.Empty)

            If objRespuesta.TipoProcedencia(0).bolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            chkVigenteForm.Checked = objRespuesta.TipoProcedencia(0).bolActivo

            EsVigente = objRespuesta.TipoProcedencia(0).bolActivo
            OidTipoProcedencia = objRespuesta.TipoProcedencia(0).oidTipoProcedencia

        End If

    End Sub

#End Region
#Region "[PROPRIEDADES FORMULÁRIO]"

    Private Property OidTipoProcedencia As String
        Get
            Return ViewState("OidTipoProcedencia")
        End Get
        Set(value As String)
            ViewState("OidTipoProcedencia") = value
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
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property TipoProcedenciaForm As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia
        Get
            Return DirectCast(ViewState("TipoProcedenciaForm"), ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia)
        End Get
        Set(value As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia)
            ViewState("TipoProcedenciaForm") = value
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
#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = Server.UrlDecode(GdvResultado.getValorLinhaSelecionada)
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Utilidad.eAcao.Modificacion
                txtCodigoTipoProcedencia.Text = String.Empty
                txtDescricaoTipoProcedencia.Text = String.Empty
                TipoProcedenciaForm = Nothing
                OidTipoProcedencia = String.Empty

                CarregaDados(codigo)
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                txtCodigoTipoProcedencia.Enabled = False
                txtDescricaoTipoProcedencia.Focus()
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
                    codigo = Server.UrlDecode(GdvResultado.getValorLinhaSelecionada)
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Utilidad.eAcao.Consulta
                txtCodigoTipoProcedencia.Text = String.Empty
                txtDescricaoTipoProcedencia.Text = String.Empty
                TipoProcedenciaForm = Nothing
                OidTipoProcedencia = String.Empty

                CarregaDados(codigo)
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
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = Server.UrlDecode(GdvResultado.getValorLinhaSelecionada)
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Utilidad.eAcao.Baja
                txtCodigoTipoProcedencia.Text = String.Empty
                txtDescricaoTipoProcedencia.Text = String.Empty
                TipoProcedenciaForm = Nothing
                OidTipoProcedencia = String.Empty

                CarregaDados(codigo)
                btnBajaConfirm.Visible = True
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
End Class