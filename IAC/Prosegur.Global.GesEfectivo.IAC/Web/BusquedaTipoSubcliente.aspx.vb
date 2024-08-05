Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Tipo Cliente 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 11/04/13 - Criado </history>
Public Class BusquedaTipoSubcliente
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
      
    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodTipoSubcliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtCodigoTipoSubcliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoSubcliente.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipSubcliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipSubcliente.ClientID & "').focus();return false;}} else {return true}; ")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Tipo SubCliente")
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

                RealizarBusca()
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
    ''' [pgoncalves] 11/04/2013 Criado
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
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("032_titulo_pagina_sub")
        lblCodTipoSubcliente.Text = Traduzir("032_codigo_tipoSubCliente")
        lblDescripcion.Text = Traduzir("032_descricao_tipoSub")
        lblVigente.Text = Traduzir("032_Vigente")
        lblResultadoTipoSubCliente.Text = Traduzir("032_TipoSubCliente")
        lblCriteriosBusqueda.Text = Traduzir("gen_lbl_CriterioBusca")
        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Botões
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("031_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("031_lbl_grd_descricao")
        GdvResultado.Columns(3).HeaderText = Traduzir("031_lbl_grd_Ativo")

        'Formulario
        lblCodTipoSubclienteForm.Text = Traduzir("032_codigo_tipoSubCliente")
        lblDescripcionForm.Text = Traduzir("032_descricao_tipoSub")
        lblVigenteForm.Text = Traduzir("032_Vigente")
        lblTituloTipoCliente.Text = Traduzir("032_TipoSubCliente")
        csvCodigoTipoCliente.ErrorMessage = Traduzir("032_lbl_codigo_Tbcliente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("031_lbl_decricao_Tbcliente")
        csvCodigoExistente.ErrorMessage = Traduzir("032_msg_codigTipoSubExistente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
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

    Property FiltroCodigoTipoCliente() As String
        Get
            Return ViewState("FiltroCodigoTipoCliente")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoTipoCliente") = value
        End Set
    End Property

    Property TipoSubClienteCollection() As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion
        Get
            Return DirectCast(ViewState("TipoSubClienteCollection"), ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion)
        End Get
        Set(value As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion)
            ViewState("TipoSubClienteCollection") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca os tipos subclientes de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Public Function getTipoSubcliente() As IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta

        Dim objProxy As New Comunicacion.ProxyTipoSubCliente
        Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta

        'Recebe os valores do filtro
        objPeticion.bolActivo = FiltroVigente
        objPeticion.codTipoSubcliente = FiltroCodigo.ToString()
        objPeticion.desTipoSubcliente = FiltroDescripcion.ToString()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposSubclientes(objPeticion)
        TipoSubClienteCollection = objRespuesta.TipoSubCliente
        Return objRespuesta

    End Function

    Private Sub RealizarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As String
        Dim strListDescricao As String

        strListCodigo = txtCodTipoSubcliente.Text
        strListDescricao = txtDescricao.Text.Trim

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherTipoSubCliente()
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
    ''' Metodo popula o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Public Sub PreencherTipoSubCliente()

        Dim objRespuesta As IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta

        'Busca as divisas
        objRespuesta = getTipoSubcliente()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespuesta.TipoSubCliente.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.TipoSubCliente.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.TipoSubCliente)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codTipoSubcliente ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " codTipoSubcliente ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespuesta.TipoSubCliente)
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
    ''' [pgoncalves] 11/04/2013 Criado
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
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getTipoSubcliente().TipoSubCliente)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " codTipoSubcliente ASC "
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
    ''' [pgoncalves] 11/04/2013 Criado
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
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Vigente

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
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated
        If e.Row.RowType = DataControlRowType.Header Then
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("031_lbl_grd_codigo")
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 6
            'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 7
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("031_lbl_grd_descricao")
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 8
            'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 9
            'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("031_lbl_grd_Ativo")
        End If
    End Sub
#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            txtCodTipoSubcliente.Text = String.Empty
            txtDescricao.Text = String.Empty
            chkVigente.Checked = True
            pnlSemRegistro.Visible = False

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            btnCancelar_Click(sender, e)

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
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            RealizarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    
    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncales] 11/04/2013 Criado
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
                'Retorna o valor da linha selecionada no grid

                Dim resultado As New ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente

                resultado = (From item In TipoSubClienteCollection
                             Where item.codTipoSubcliente = strCodigo
                             Select item).FirstOrDefault

                If resultado.codTipoSubcliente.Equals("B") OrElse resultado.codTipoSubcliente.Equals("b") Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_excluir", "alert('" & Traduzir("031_lbl_msg_banco") & "');", True)
                    Exit Sub
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyTipoSubCliente As New Comunicacion.ProxyTipoSubCliente
                Dim objRespuesta As IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta

                'Criando um Termino para exclusão
                Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion
                objPeticion.codTipoSubcliente = resultado.codTipoSubcliente
                objPeticion.bolActivo = False
                objPeticion.desTipoSubcliente = resultado.desTipoSubcliente

                objPeticion.desUsuarioCreacion = resultado.desUsuarioCreacion
                objPeticion.gmtCreacion = resultado.gmtCreacion

                objPeticion.desUsuarioModificacion = MyBase.LoginUsuario
                objPeticion.gmtModificacion = System.DateTime.Now

                objPeticion.oidTipoSubcliente = resultado.oidTipoSubcliente

                objRespuesta = objProxyTipoSubCliente.setTiposSubclientes(objPeticion)

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    RealizarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)
                Else

                    If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuesta.MensajeError)
                    Else
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
    ''' [pgoncalves] 11/04/2013 - Criado
    ''' </history>
    Public Sub ControleBotoes()

        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

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
                   

                Case Aplicacao.Util.Utilidad.eAcao.Busca

                  
            End Select
        Else
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.NoAction
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial

                Case Aplicacao.Util.Utilidad.eAcao.Busca


            End Select
        End If


    End Sub

#End Region
#Region "[PROPRIEDADES FORMULARIO]"

    Private Property OidTipoSubCliente As String
        Get
            Return ViewState("OidTipoSubCliente")
        End Get
        Set(value As String)
            ViewState("OidTipoSubCliente") = value
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

    Private Property TipoSubCliente As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente
        Get
            Return DirectCast(ViewState("TipoSubCliente"), ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente)
        End Get
        Set(value As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente)
            ViewState("TipoSubCliente") = value
        End Set
    End Property

#End Region

#Region "Métodos Formulário"
    Private Sub LimparCampos()
        txtCodigoTipoSubcliente.Text = String.Empty
        txtDescricaoTipSubcliente.Text = String.Empty
        chkVigenteForm.Checked = True
        OidTipoSubCliente = String.Empty
    End Sub
    Public Sub ExecutarGrabar()
        Try

            Dim objProxy As New Comunicacion.ProxyTipoSubCliente
            Dim objRespuesta As IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta


            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objPeticion.codTipoSubcliente = txtCodigoTipoSubcliente.Text.Trim
            objPeticion.desTipoSubcliente = txtDescricaoTipSubcliente.Text
            objPeticion.oidTipoSubcliente = If(String.IsNullOrEmpty(OidTipoSubCliente), Nothing, OidTipoSubCliente)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoSubCliente.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoSubCliente.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxy.setTiposSubclientes(objPeticion)

            Dim url As String = "BusquedaTipoSubcliente.aspx"

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                RealizarBusca()
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
                If txtCodigoTipoSubcliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoCliente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoCliente.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoSubcliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoCliente.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipSubcliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipSubcliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoTipoSubcliente.Text) AndAlso ExisteCodigoTipoSubCliente(txtCodigoTipoSubcliente.Text.Trim()) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoSubcliente.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoTipoSubCliente(codigo As String) As Boolean

        Try

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoSubCliente

            objPeticion.codTipoSubcliente = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposSubclientes(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoSubCliente.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Public Sub CarregaDados(codigo As String)

        Dim objPeticion As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoSubCliente

        objPeticion.codTipoSubcliente = codigo
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposSubclientes(objPeticion)

        TipoSubCliente = objRespuesta.TipoSubCliente(0)

        If objRespuesta.TipoSubCliente.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoSubcliente.Text = objRespuesta.TipoSubCliente(0).codTipoSubcliente
            txtCodigoTipoSubcliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSubCliente(0).codTipoSubcliente, String.Empty)

            txtDescricaoTipSubcliente.Text = objRespuesta.TipoSubCliente(0).desTipoSubcliente
            txtDescricaoTipSubcliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSubCliente(0).desTipoSubcliente, String.Empty)

            If objRespuesta.TipoSubCliente(0).bolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            chkVigenteForm.Checked = objRespuesta.TipoSubCliente(0).bolActivo

            EsVigente = objRespuesta.TipoSubCliente(0).bolActivo
            OidTipoSubCliente = objRespuesta.TipoSubCliente(0).oidTipoSubcliente

        End If

    End Sub
#End Region
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
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
            txtCodigoTipoSubcliente.Enabled = True
            txtCodigoTipoSubcliente.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
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
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoTipoSubcliente.Enabled = False
                txtDescricaoTipSubcliente.Focus()

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
                pnForm.Enabled = False
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
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
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = False
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                chkVigenteForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class