Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Tipo Cliente 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 09/04/13 - Criado </history>
Public Class BusquedaTipoCliente
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
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
        txtCodTipoCliente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtCodigoTipoCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoCliente.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipoCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipoCliente.ClientID & "').focus();return false;}} else {return true}; ")

        End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE
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
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Tipo Cliente")
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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("031_lbl_titulo_pagina")
        lblCodTipoCliente.Text = Traduzir("031_lbl_codigo_tipoCliente")
        lblDescripcion.Text = Traduzir("031_lbl_descricao_tipo")
        lblVigente.Text = Traduzir("031_lbl_Tipo_vigente")
        lblResultadoTipoCliente.Text = Traduzir("031_lbl_tipo_cliente")
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
        lblCodTipoClienteForm.Text = Traduzir("031_lbl_codigo_tipoCliente")
        lblDescripcionForm.Text = Traduzir("031_lbl_descricao_tipo")
        lblVigenteForm.Text = Traduzir("031_lbl_Tipo_vigente")
        lblTituloTipoCliente.Text = Traduzir("031_lbl_titulo_pagina")
        csvCodigoTipoCliente.ErrorMessage = Traduzir("031_lbl_codigo_cliente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("031_lbl_decricao_cliente")
        csvCodigoExistente.ErrorMessage = Traduzir("031_msg_codigTipoExistente")
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
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

    Property TipoClienteCollection() As ContractoServicio.TipoCliente.GetTiposClientes.TipoClienteColeccion
        Get
            Return DirectCast(ViewState("TipoClienteCollection"), ContractoServicio.TipoCliente.GetTiposClientes.TipoClienteColeccion)
        End Get
        Set(value As ContractoServicio.TipoCliente.GetTiposClientes.TipoClienteColeccion)
            ViewState("TipoClienteCollection") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca os tipos clientes de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Public Function getTipoCliente() As IAC.ContractoServicio.TipoCliente.GetTiposClientes.Respuesta

        Dim objProxyTipoCliente As New Comunicacion.ProxyTipoCliente
        Dim objPeticion As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Respuesta

        'Recebe os valores do filtro
        objPeticion.bolActivo = FiltroVigente
        objPeticion.codTipoCliente = FiltroCodigo.ToString()
        objPeticion.desTipoCliente = FiltroDescripcion.ToString()
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyTipoCliente.getTiposClientes(objPeticion)
        TipoClienteCollection = objRespuesta.TipoCliente
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
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Public Sub PreencherTipoCliente()

        Dim objRespuesta As IAC.ContractoServicio.TipoCliente.GetTiposClientes.Respuesta

        'Busca as divisas
        objRespuesta = getTipoCliente()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespuesta.TipoCliente.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.TipoCliente.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.TipoCliente)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codTipoCliente ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " codTipoCliente ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespuesta.TipoCliente)
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                GdvResultado.CarregaControle(objDt)

                '  btnConsulta.Visible = True
                btnBaja.Visible = True
                'btnModificacion.Visible = True
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
    Private Sub RealizarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        Dim strListCodigo As String
        Dim strListDescricao As String

        strListCodigo = txtCodTipoCliente.Text
        strListDescricao = txtDescricao.Text.Trim

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherTipoCliente()
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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getTipoCliente().TipoCliente)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " codTipoCliente ASC "
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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncalves] 09/04/2013 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            txtCodTipoCliente.Text = String.Empty
            txtDescricao.Text = String.Empty
            chkVigente.Checked = True
            pnlSemRegistro.Visible = False

            'btnConsulta.Visible = False
            btnBaja.Visible = False
            ' btnModificacion.Visible = False

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
    ''' [pgoncalves] 09/04/2013 Criado
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
    ''' [pgoncales] 09/04/2013 Criado
    ''' </history>
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try

            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    strCodigo = GdvResultado.getValorLinhaSelecionada
                Else
                    strCodigo = hiddenCodigo.Value.ToString()
                End If
                'Retorna o valor da linha selecionada no grid
                Dim resultado As New ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente

                resultado = (From item As ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente In TipoClienteCollection
                             Where item.codTipoCliente = strCodigo
                             Select item).FirstOrDefault()

                If resultado.codTipoCliente.Equals("B") OrElse resultado.codTipoCliente.Equals("b") Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_excluir", "alert('" & Traduzir("031_lbl_msg_banco") & "');", True)
                    Exit Sub
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyTipoCliente As New Comunicacion.ProxyTipoCliente
                Dim objRespuesta As IAC.ContractoServicio.TipoCliente.SetTiposClientes.Respuesta

                'Criando um Termino para exclusão
                Dim objPeticion As New IAC.ContractoServicio.TipoCliente.SetTiposClientes.Peticion
                objPeticion.codTipoCliente = resultado.codTipoCliente
                objPeticion.bolActivo = False
                objPeticion.desTipoCliente = resultado.desTipoCliente

                objPeticion.desUsuarioCreacion = resultado.desUsuarioCreacion
                objPeticion.gmtCreacion = resultado.gmtCreacion

                objPeticion.desUsuarioModificacion = MyBase.LoginUsuario
                objPeticion.gmtModificacion = System.DateTime.Now

                objPeticion.oidTipoCliente = resultado.oidTipoCliente

                objRespuesta = objProxyTipoCliente.setTiposClientes(objPeticion)

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
    ''' [pgoncalves] 09/04/2013 - Criado
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
              

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub

#End Region

#Region "[PROPRIEDADES FORMULARIO]"

    Private Property OidTipoCliente As String
        Get
            Return ViewState("OidTipoCliente")
        End Get
        Set(value As String)
            ViewState("OidTipoCliente") = value
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

    Private Property TipoCliente As ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente
        Get
            Return DirectCast(ViewState("TipoCliente"), ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente)
        End Get
        Set(value As ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente)
            ViewState("TipoCliente") = value
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
#Region "[Métodos Formulario]"
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTipoCliente As New Comunicacion.ProxyTipoCliente
            Dim objRespuesta As IAC.ContractoServicio.TipoCliente.SetTiposClientes.Respuesta

            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoCliente.SetTiposClientes.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objPeticion.codTipoCliente = txtCodigoTipoCliente.Text.Trim
            objPeticion.desTipoCliente = txtDescricaoTipoCliente.Text
            objPeticion.oidTipoCliente = If(String.IsNullOrEmpty(OidTipoCliente), Nothing, OidTipoCliente)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoCliente.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoCliente.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxyTipoCliente.setTiposClientes(objPeticion)

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
                If txtCodigoTipoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoCliente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoCliente.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoCliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoCliente.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipoCliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoTipoCliente.Text) AndAlso ExisteCodigoCliente(txtCodigoTipoCliente.Text) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoCliente.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoCliente(codigo As String) As Boolean

        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoCliente

            objPeticion.codTipoCliente = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposClientes(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoCliente.Count > 0 Then
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

    Private Sub LimparCampos()
        txtCodigoTipoCliente.Text = String.Empty
        txtDescricaoTipoCliente.Text = String.Empty
        chkVigenteForm.Checked = True
        TipoCliente = Nothing
        OidTipoCliente = Nothing
    End Sub
    Public Sub CarregaDados(codTipoCliente As String)

        Dim objPeticion As New ContractoServicio.TipoCliente.GetTiposClientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoCliente.GetTiposClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoCliente

        objPeticion.codTipoCliente = codTipoCliente
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposClientes(objPeticion)
        TipoCliente = objRespuesta.TipoCliente(0)

        If objRespuesta.TipoCliente.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoCliente.Text = objRespuesta.TipoCliente(0).codTipoCliente
            txtCodigoTipoCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoCliente(0).codTipoCliente, String.Empty)

            txtDescricaoTipoCliente.Text = objRespuesta.TipoCliente(0).desTipoCliente
            txtDescricaoTipoCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoCliente(0).desTipoCliente, String.Empty)

            If objRespuesta.TipoCliente(0).bolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            chkVigenteForm.Checked = objRespuesta.TipoCliente(0).bolActivo

            EsVigente = objRespuesta.TipoCliente(0).bolActivo
            OidTipoCliente = objRespuesta.TipoCliente(0).oidTipoCliente

        End If

    End Sub
#End Region
#Region "Eventos Formulario"


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
            txtCodigoTipoCliente.Enabled = True
            txtCodigoTipoCliente.Focus()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    
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
                txtCodigoTipoCliente.Enabled = False
                txtDescricaoTipoCliente.Focus()

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
#End Region
End Class