Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Postos
''' </summary>
''' <remarks></remarks>
''' <history>[lmsantana] 05/09/11 - Criado</history>
Partial Public Class BusquedaPuestos
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Faz a validação dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [lmsantana] 05/09/11 Criado
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
        txtCodigoPuesto.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtHostPuesto.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        cblVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        ddlAplicacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PUESTOS
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


            If Not IsPostBack Then
                ConfigurarControles()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnConfigurarParametros.Visible = True
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                Dim ConfigurarParametro As String = Request.QueryString("AcessoNegadoConfigurarParametro")
                If Not String.IsNullOrEmpty(ConfigurarParametro) AndAlso ConfigurarParametro.ToUpper = Boolean.TrueString.ToUpper Then
                    MyBase.MostraMensagem(Traduzir("027_no_permiso_configurar_permiso"))
                Else
                    RealizarBusca()
                End If
                
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
            cblVigente.Attributes.Add("style", "margin:0px !important;")
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

        Master.Titulo = Traduzir("025_titulo_busqueda_puestos")
        lblAplicacion.Text = Traduzir("025_lbl_aplicacion")
        lblCodigoPuesto.Text = Traduzir("025_lbl_codigopuesto")
        lblHostPuesto.Text = Traduzir("025_lbl_hostpuesto")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("025_lbl_subtituloscriteriosbusqueda")
        lblSubTitulosPuestos.Text = Traduzir("025_lbl_subtitulospuestos")
        lblSemRegistro.Text = Traduzir("025_lbl_sem_registro")
        'lblVigente.Text = Traduzir("025_lbl_vigente")
        cblVigente.Items(0).Text = Traduzir("025_chk_vigente")
        cblVigente.Items(1).Text = Traduzir("025_chk_no_vigente")
        cblVigente.Items(2).Text = Traduzir("025_chk_cualquiera")

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
        btnConfigurarParametros.Text = Traduzir("025_btnConfigurarParametros")
        btnConfigurarParametros.ToolTip = Traduzir("025_btnConfigurarParametros")


        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("025_lbl_delegacion")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("025_lbl_codigopuesto")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("025_lbl_hostpuesto")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("025_lbl_aplicacion")
        ProsegurGridView1.Columns(5).HeaderText = Traduzir("001_lbl_grd_vigente")

        'Formulario
        lblDelegacion.Text = Traduzir("025_lbl_man_delegacion")
        lblAplicacionForm.Text = Traduzir("025_lbl_man_aplicacion")
        lblCodigoPuestoForm.Text = Traduzir("025_lbl_man_codigopuesto")
        lblHostPuestoForm.Text = Traduzir("025_lbl_man_hostpuesto")
        lblVigente.Text = Traduzir("001_lbl_vigente")
        lblTituloPuesto.Text = Traduzir("025_titulo_mantenimiento_puestos")
        rfvAplicacion.ErrorMessage = Traduzir("025_msg_aplicacionobligatorio")
        rfvCodigoPuesto.ErrorMessage = Traduzir("025_msg_codigopuestoobligatorio")
        rfvhostPuesto.ErrorMessage = Traduzir("025_msg_hostpuestoobligatorio")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Property FiltroPuesto() As String
        Get
            Return ViewState("FiltroPuesto")
        End Get
        Set(value As String)
            ViewState("FiltroPuesto") = value
        End Set
    End Property

    Property FiltroAplicacion() As String
        Get
            Return ViewState("FiltroAplicacion")
        End Get
        Set(value As String)
            ViewState("FiltroAplicacion") = value
        End Set
    End Property

    Property FiltroHostPuesto() As String
        Get
            Return ViewState("FiltroHostPuesto")
        End Get
        Set(value As String)
            ViewState("FiltroHostPuesto") = value
        End Set
    End Property

    Property FiltroVigente() As Nullable(Of Boolean)
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Nullable(Of Boolean))
            ViewState("FiltroVigente") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Faz a busca dos Postos com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Function getPuestos() As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        Dim objProxyPuesto As New ProxyPuesto
        Dim objPeticionPuesto As New IAC.ContractoServicio.Puesto.GetPuestos.Peticion
        Dim objRespuestaPuesto As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        'Recebe os valores do filtro
        objPeticionPuesto.BolVigente = FiltroVigente
        objPeticionPuesto.CodigoAplicacion = FiltroAplicacion
        objPeticionPuesto.CodigoDelegacion = MyBase.DelegacionConectada.Keys(0)
        objPeticionPuesto.CodigoPuesto = FiltroPuesto
        objPeticionPuesto.HostPuesto = FiltroHostPuesto
        'objPeticionPuesto.Permisos = InformacionUsuario.Permisos
        objPeticionPuesto.Aplicaciones = InformacionUsuario.Aplicaciones

        objRespuestaPuesto = objProxyPuesto.GetPuestos(objPeticionPuesto)

        Return objRespuestaPuesto


    End Function

    ''' <summary>
    ''' transforma o contrato do Posto em DataTable
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 09/09/11 Criado
    Public Function TransformarContractoPuestoEmDataTable(puestos As IAC.ContractoServicio.Puesto.GetPuestos.PuestoColeccion) As DataTable
        Dim dt As DataTable

        Dim grid As New Prosegur.Web.ProsegurGridView
        dt = grid.ConvertListToDataTable(puestos)
        dt.Columns.Add("DescripcionAplicacion", GetType([String]))
        dt.Columns.Add("CodigoAplicacion", GetType([String]))
        For Each dr As DataRow In dt.Rows
            Dim aplicacion As IAC.ContractoServicio.Aplicacion = CType(dr("Aplicacion"), IAC.ContractoServicio.Aplicacion)
            dr("DescripcionAplicacion") = aplicacion.DescripcionAplicacion
            dr("CodigoAplicacion") = aplicacion.CodigoAplicacion
        Next

        Return dt

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
    ''' Preenche do grid com a coleção de Postos
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Sub PreencherPuestos()

        Dim objRespostaPuesto As IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

        'Busca os canais
        objRespostaPuesto = getPuestos()

        If Not Master.ControleErro.VerificaErro(objRespostaPuesto.CodigoError, objRespostaPuesto.NombreServidorBD, objRespostaPuesto.MensajeError) Then

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaPuesto.Puestos.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaPuesto.Puestos.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = TransformarContractoPuestoEmDataTable(objRespostaPuesto.Puestos)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then

                    objDt.DefaultView.Sort = " CodigoPuesto ASC "

                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodigoPuesto ASC "
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

    Private Sub ConfigurarControles()
        PreencherComboAplicaciones()
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
    Private Sub RealizarBusca()
        'Threading.Thread.Sleep(2000)
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca


        'Filtros
        FiltroAplicacion = ddlAplicacion.SelectedValue
        FiltroHostPuesto = txtHostPuesto.Text
        FiltroPuesto = txtCodigoPuesto.Text
        If cblVigente.SelectedValue = "1" Then
            FiltroVigente = True
        ElseIf cblVigente.SelectedValue = "0" Then
            FiltroVigente = False
        Else
            FiltroVigente = Nothing
        End If

        'Retorna os canais de acordo com o filtro aciam
        PreencherPuestos()
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
    ''' [lmsantana] 05/09/11 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            'adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("PuestoVigente").ToString.ToLower & ");"

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
    ''' [lmsantana] 05/09/11 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial Then
            Return
        End If

        Dim objDT As DataTable

        Dim objRespoustaPuestos As Object

        objRespoustaPuestos = getPuestos().Puestos

        objDT = TransformarContractoPuestoEmDataTable(objRespoustaPuestos)

        If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " CodigoPuesto ASC "
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
    ''' [pda] 27/01/2009 Criado
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
    ''' [lmsantana] 05/09/11 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            'Índice das celulas do GridView Configuradas
            '1 - Código
            '2 - Descripción
            '3 - Observación
            '4 - Vigente

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

            'If Not e.Row.DataItem("observaciones") Is DBNull.Value AndAlso e.Row.DataItem("observaciones").Length > NumeroMaximoLinha Then
            '    e.Row.Cells(2).Text = e.Row.DataItem("observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
            'End If
            Dim valor As String = Server.UrlEncode(e.Row.DataItem("CodigoPuesto")) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoHostPuesto")) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoDelegacion")) & "$#" & Server.UrlEncode(e.Row.DataItem("CodigoAplicacion"))
            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & valor & "');"

            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgDuplicar"), ImageButton).ToolTip = Traduzir("025_btnCopiarPuesto")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            If CBool(e.Row.DataItem("PuestoVigente")) Then
                CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(5).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

            Dim aplicacionItem As ContractoServicio.Aplicacion = e.Row.DataItem("Aplicacion")

            If aplicacionItem IsNot Nothing Then
                e.Row.Cells(4).Text = aplicacionItem.DescripcionAplicacion
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
    ''' [lmsantana] 05/09/11 Criado
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
    ''' [lmsantana] 05/09/11 Criado
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta        
            ProsegurGridView1.CarregaControle(Nothing, True, True)


            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ddlAplicacion.ClearSelection()
            ddlAplicacion.ToolTip = String.Empty
            txtCodigoPuesto.Text = String.Empty
            txtHostPuesto.Text = String.Empty
            cblVigente.SelectedIndex = 0

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            RealizarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnConfigurarParametros.Visible = True
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento click botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Protected Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim arrCodigo As String()
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    arrCodigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")
                Else
                    arrCodigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyPuesto As New ProxyPuesto
                Dim objPeticionPuesto As New IAC.ContractoServicio.Puesto.SetPuesto.Peticion
                Dim objRespuestaPuesto As IAC.ContractoServicio.Puesto.SetPuesto.Respuesta

                Dim Codigos() As String = arrCodigo
                objPeticionPuesto.PuestoVigente = False
                objPeticionPuesto.CodigoUsuario = MyBase.LoginUsuario
                objPeticionPuesto.CodigoAplicacion = FiltroAplicacion
                objPeticionPuesto.CodigoPuesto = Codigos(0)
                objPeticionPuesto.CodigoHostPuesto = Codigos(1)
                objPeticionPuesto.CodigoDelegacion = Codigos(2)
                objPeticionPuesto.CodigoAplicacion = Codigos(3)
                objPeticionPuesto.Accion = ContractoServicio.Enumeradores.Accion.Modificacion

                objRespuestaPuesto = objProxyPuesto.SetPuesto(objPeticionPuesto)

                If Master.ControleErro.VerificaErro(objRespuestaPuesto.CodigoError, objRespuestaPuesto.NombreServidorBD, objRespuestaPuesto.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    RealizarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    If objRespuestaPuesto.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        ' mostrar o erro pro usuário
                        MyBase.MostraMensagem(objRespuestaPuesto.MensajeError)
                    End If
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

   ''' <summary>
    ''' Evento click botão consulta.
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Protected Sub btnConfigurarParametros_Click(sender As Object, e As EventArgs) Handles btnConfigurarParametros.Click
        Try
            Dim url As String = ""
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim arrCodigo As String()
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    arrCodigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")
                Else
                    arrCodigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")
                End If

                Dim codigoAplicacion As String = "CodigoAplicacion=" & Server.UrlEncode(arrCodigo(3))
                Dim codigoPuesto As String = "&CodigoPuesto=" & Server.UrlEncode(arrCodigo(0))
                Dim hostPuesto As String = "&HostPuesto=" & Server.UrlEncode(arrCodigo(1))

                url = String.Format("~/MantenimientoConfiguracionParametro.aspx?{0}{1}{2}", codigoAplicacion, codigoPuesto, hostPuesto)
            Else
                url = "~/MantenimientoConfiguracionParametro.aspx?"
                If String.IsNullOrEmpty(ddlAplicacion.SelectedValue) Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_aplicacion_configurar_parametro", "alert('" & Traduzir("025_msg_aplicacion_no_rellenada") & "');", True)

                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
                    Return
                End If
                url = url & "CodigoAplicacion=" & Server.UrlEncode(ddlAplicacion.SelectedValue)
            End If

            Response.Redirect(url, False)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

    Private Sub ddlAplicacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAplicacion.SelectedIndexChanged
        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text
        'ddlAplicacion.Focus()
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Configuração controle de estado.
    ''' </summary>s
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

            Case Aplicacao.Util.Utilidad.eAcao.Baja

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                pnlSemRegistro.Visible = False
                txtCodigoPuesto.Text = String.Empty
                txtHostPuesto.Text = String.Empty
                'ddlAplicacion.ClearSelection()
                cblVigente.SelectedValue = "1"
            Case Aplicacao.Util.Utilidad.eAcao.Busca
               
        End Select

    End Sub

#End Region

#Region "Métodos Formulário"
    Public Sub ExecutarGravar()

        Try
            Dim erroMensage As String = MontaMensagensErro(True)
            If Not String.IsNullOrEmpty(erroMensage) Then
                MyBase.MostraMensagem(erroMensage)
                Exit Sub
            End If

            Dim objProxyPuesto As New ProxyPuesto
            Dim objRespuestaPuesto As IAC.ContractoServicio.Puesto.SetPuesto.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            Dim objPeticion As New IAC.ContractoServicio.Puesto.SetPuesto.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objPeticion.PuestoVigente = True
                objPeticion.Accion = ContractoServicio.Enumeradores.Accion.Alta
            Else
                objPeticion.PuestoVigente = chkVigente.Checked
                objPeticion.Accion = ContractoServicio.Enumeradores.Accion.Modificacion
            End If

            objPeticion.CodigoAplicacion = ddlAplicacionForm.SelectedValue
            objPeticion.CodigoDelegacion = DelegacionConectada.Keys(0)
            objPeticion.CodigoHostPuesto = Trim(txtHostPuestoForm.Text)
            objPeticion.CodigoPuesto = Trim(txtCodigoPuestoForm.Text)
            objPeticion.CodigoUsuario = MyBase.LoginUsuario


            objRespuestaPuesto = objProxyPuesto.SetPuesto(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuestaPuesto.CodigoError, objRespuestaPuesto.NombreServidorBD, objRespuestaPuesto.MensajeError) Then
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                    Dim objRespuestaParametro As ContractoServicio.Parametro.SetParametrosValues.Respuesta = DuplicarParametrosValues(objPeticion)
                    If Master.ControleErro.VerificaErro(objRespuestaPuesto.CodigoError, objRespuestaPuesto.NombreServidorBD, objRespuestaPuesto.MensajeError) Then
                        MyBase.MostraMensagem(Traduzir("025_msg_grabado_suceso"))
                        RealizarBusca()
                        UpdatePanelGrid.Update()
                        btnCancelar_Click(Nothing, Nothing)
                    Else
                        MyBase.MostraMensagem(objRespuestaParametro.MensajeError)
                    End If
                Else
                    MyBase.MostraMensagem(Traduzir("025_msg_grabado_suceso"))
                    RealizarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)
                End If
            Else
                MyBase.MostraMensagem(objRespuestaPuesto.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Function DuplicarParametrosValues(objPeticion As ContractoServicio.Puesto.SetPuesto.Peticion) As ContractoServicio.Parametro.SetParametrosValues.Respuesta

        Dim strCodPuesto As String = CodPuesto
        Dim strCodDelegacion As String = CodDelegacion
        Dim strCodAplicacion As String = CodAplicacion

        Dim parametrosValues As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
        Dim peticion As New ContractoServicio.Parametro.SetParametrosValues.Peticion
        If Not String.IsNullOrEmpty(strCodPuesto) AndAlso Not String.IsNullOrEmpty(strCodDelegacion) AndAlso Not String.IsNullOrEmpty(strCodAplicacion) Then
            parametrosValues = getParametrosValue(strCodAplicacion, strCodDelegacion, strCodPuesto)
            If parametrosValues.Count > 0 Then
                Dim objProxy As New Comunicacion.ProxyParametro
                peticion.CodigoAplicacion = objPeticion.CodigoAplicacion
                peticion.CodigoDelegacion = objPeticion.CodigoDelegacion
                peticion.CodigoPuesto = objPeticion.CodigoPuesto
                peticion.CodigoUsuario = MyBase.LoginUsuario
                peticion.Parametros = New ContractoServicio.Parametro.SetParametrosValues.ParametroColeccion

                For Each parametroValue As ContractoServicio.Parametro.GetParametrosValues.Nivel In parametrosValues
                    If parametroValue.CodigoNivel = ContractoServicio.Parametro.TipoNivel.Puesto Then
                        For Each agrupacion In parametroValue.Agrupaciones
                            For Each parametro In agrupacion.Parametros
                                Dim parametroCopia As New ContractoServicio.Parametro.SetParametrosValues.Parametro
                                parametroCopia.CodigoParametro = parametro.CodigoParametro
                                parametroCopia.ValorParametro = parametro.ValorParametro
                                peticion.Parametros.Add(parametroCopia)
                            Next
                        Next
                    End If
                Next
                Return objProxy.SetParametrosValues(peticion)
            End If
        End If
        Return New ContractoServicio.Parametro.SetParametrosValues.Respuesta
    End Function

    Private Function getParametrosValue(codAplicacion As String, codDelegacion As String, codPuesto As String) As ContractoServicio.Parametro.GetParametrosValues.NivelColeccion

        Dim objProxy As New Comunicacion.ProxyParametro
        Dim objRespuesta As New IAC.ContractoServicio.Parametro.GetParametrosValues.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Parametro.GetParametrosValues.Peticion
        objPeticion.CodigoAplicacion = codAplicacion
        objPeticion.CodigoDelegacion = codDelegacion
        objPeticion.CodigoPuesto = codPuesto

        objRespuesta = objProxy.GetParametrosValues(objPeticion)
        If objRespuesta.CodigoError <> 0 Then
            Return New ContractoServicio.Parametro.GetParametrosValues.NivelColeccion
        End If

        Return objRespuesta.Niveles

    End Function

    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New StringBuilder
        Me.Validate("Gravar")
        For Each validator As IValidator In Me.Validators
            If validator IsNot Nothing AndAlso Not validator.IsValid Then
                strErro.Append(validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            End If
        Next

        Return strErro.ToString

    End Function


    Public Sub PreencherComboAplicacionForm()
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
    Private Sub LimparCampos()
        PreencherComboAplicacionForm()
        txtCodigoPuestoForm.Text = String.Empty
        txtHostPuestoForm.Text = String.Empty
        chkVigente.Checked = True
        CodAplicacion = String.Empty
        CodPuesto = String.Empty
        HostPuesto = String.Empty
        CodDelegacion = String.Empty
    End Sub

    Public Sub CarregaDados(codAplicacion As String, hostPuesto As String, codPuesto As String, esEdicion As Boolean)

        Dim objPuesto As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Puesto
        objPuesto = getParametroDetail(codAplicacion, hostPuesto, codPuesto)

        'Preenche os controles do formulario
        txtDelegacion.Text = objPuesto.DescripcionDelegacion
        txtDelegacion.ToolTip = objPuesto.DescripcionDelegacion

        ddlAplicacionForm.SelectedValue = objPuesto.Aplicacion.CodigoAplicacion
        ddlAplicacionForm.ToolTip = ddlAplicacion.SelectedItem.Text

        If esEdicion Then
            txtCodigoPuestoForm.Text = objPuesto.CodigoPuesto
            txtCodigoPuestoForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion, objPuesto.CodigoPuesto, String.Empty)

            txtHostPuestoForm.Text = objPuesto.CodigoHostPuesto
        End If

        chkVigente.Checked = objPuesto.PuestoVigente

    End Sub
    Private Function getParametroDetail(codAplicacion As String, hostPuesto As String, codPuesto As String) As ContractoServicio.Puesto.GetPuestoDetail.Puesto
        Dim objProxyPuesto As New ProxyPuesto
        Dim objRespuesta As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Puesto.GetPuestoDetail.Peticion
        objPeticion.CodigoAplicacion = codAplicacion
        objPeticion.HostPuesto = hostPuesto
        objPeticion.CodigoPuesto = codPuesto

        objRespuesta = objProxyPuesto.GetPuestoDetail(objPeticion)
        Return objRespuesta.Puesto

    End Function
#End Region
#Region "Propriedades Form"
    Private Property CodPuesto() As String
        Get
            Return ViewState("PropCodPuesto")
        End Get
        Set(value As String)
            ViewState("PropCodPuesto") = value
        End Set
    End Property
    Private Property HostPuesto() As String
        Get
            Return ViewState("PropHostPuesto")
        End Get
        Set(value As String)
            ViewState("PropHostPuesto") = value
        End Set
    End Property

    Private Property CodAplicacion() As String
        Get
            Return ViewState("PropCodAplicacion")
        End Get
        Set(value As String)
            ViewState("PropCodAplicacion") = value
        End Set
    End Property
    Private Property CodDelegacion() As String
        Get
            Return ViewState("PropCodDelegacion")
        End Get
        Set(value As String)
            ViewState("PropCodDelegacion") = value
        End Set
    End Property
#End Region
#Region "Eventos Formulario"
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnConfigurarParametros.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigente.Checked = True
            chkVigente.Enabled = False
            chkVigente.Visible = True
            lblVigente.Visible = True
            Acao = Utilidad.eAcao.Alta
            txtDelegacion.Text = DelegacionConectada(DelegacionConectada.Keys(0))
            txtDelegacion.ToolTip = DelegacionConectada(DelegacionConectada.Keys(0))

            ddlAplicacionForm.Focus()
            txtCodigoPuestoForm.Enabled = True
            ddlAplicacionForm.Enabled = True
            txtHostPuestoForm.Enabled = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnConfigurarParametros.Visible = True
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigente.Checked = True
            chkVigente.Enabled = False
            chkVigente.Visible = True
            lblVigente.Visible = True
            Acao = Utilidad.eAcao.Inicial
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGravar()
    End Sub
#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim arrCodigo As String()
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    arrCodigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")
                Else
                    arrCodigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Modificacion

                CodPuesto = Server.UrlDecode(arrCodigo(0))
                HostPuesto = Server.UrlDecode(arrCodigo(1))
                CodAplicacion = Server.UrlDecode(arrCodigo(3))
                CodDelegacion = Server.UrlDecode(arrCodigo(2))

                CarregaDados(CodAplicacion, HostPuesto, CodPuesto, True)

                btnBajaConfirm.Visible = False
                btnConfigurarParametros.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                ddlAplicacionForm.Enabled = False
                txtCodigoPuestoForm.Enabled = False
                txtHostPuestoForm.Focus()
                chkVigente.Enabled = True

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgDuplicar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim arrCodigo As String()
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    arrCodigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")
                Else
                    arrCodigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Duplicar

                CodPuesto = Server.UrlDecode(arrCodigo(0))
                HostPuesto = Server.UrlDecode(arrCodigo(1))
                CodAplicacion = Server.UrlDecode(arrCodigo(3))
                CodDelegacion = Server.UrlDecode(arrCodigo(2))

                CarregaDados(CodAplicacion, HostPuesto, CodPuesto, False)

                btnBajaConfirm.Visible = False
                btnConfigurarParametros.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                ddlAplicacionForm.Enabled = False
                txtCodigoPuestoForm.Enabled = True
                txtCodigoPuestoForm.Focus()
                chkVigente.Enabled = True

                txtCodigoPuestoForm.Text = String.Empty
                txtHostPuestoForm.Text = String.Empty
                ddlAplicacionForm.Enabled = False
                txtCodigoPuestoForm.Focus()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim arrCodigo As String()
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    arrCodigo = ProsegurGridView1.getValorLinhaSelecionada.Replace("$#", "|").Split("|")
                Else
                    arrCodigo = hiddenCodigo.Value.ToString().Replace("$#", "|").Split("|")
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Baja

                CodPuesto = Server.UrlDecode(arrCodigo(0))
                HostPuesto = Server.UrlDecode(arrCodigo(1))
                CodAplicacion = Server.UrlDecode(arrCodigo(3))
                CodDelegacion = Server.UrlDecode(arrCodigo(2))

                CarregaDados(CodAplicacion, HostPuesto, CodPuesto, True)

                btnBajaConfirm.Visible = True
                btnConfigurarParametros.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                pnForm.Enabled = False
                pnForm.Visible = True
              

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class