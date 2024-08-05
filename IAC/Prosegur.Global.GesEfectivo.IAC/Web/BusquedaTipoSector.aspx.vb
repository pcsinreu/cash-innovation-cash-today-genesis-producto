Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Busca de tipo Sector
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pgoncalves] 04/03/2013 - Criado
''' </history>
Public Class BusquedaTipoSector
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona scripts aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/05/2013 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando o enter é prescionado.
        txtCodigoTipoSector.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoTipoSector.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        lstCaracteristicas.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

        txtCodigoTipoSetor.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigoTipoSetor.ClientID & "','" & txtCodigoTipoSetor.MaxLength & "');")
        txtDescricaoTiposSetor.Attributes.Add("onblur", "limitaCaracteres('" & txtDescricaoTiposSetor.ClientID & "','" & txtDescricaoTiposSetor.MaxLength & "');")
        txtCodigoTipoSetor.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTiposSetor.ClientID & "').focus();return false;}} else {return true}; ")

    End Sub

    ''' <summary>
    ''' configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()


    End Sub

    ''' <summary>
    ''' Parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo chamado ao carregar a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()
        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Tipo Sector")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                'Preenche a lista de características
                PreencherListaCaracteristicas()
                txtCodigoTipoSector.Focus()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                'Preenche Combos
                ExecutarBusca()
                UpdatePanelGrid.Update()



            End If
            'chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()
            TrataFoco()
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        If Not Page.IsPostBack Then
            Try
                ControleBotoes()
                btnAltaAjeno.Attributes.Add("style", "margin-left:15px;")
                btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"
            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Tradução dos contoles da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        lblCodigoTipoSetor.Text = Traduzir("019_lbl_codigotipossetor")
        lblDescricaoTipoSector.Text = Traduzir("019_lbl_descripciontiposprocesado")
        lblCaracteristicas.Text = Traduzir("019_lbl_caracteristicas")
        lblVigente.Text = Traduzir("019_chk_vigente")
        chkVigente.Text = String.Empty
        lblTitulosTiposSetor.Text = Traduzir("gen_lbl_CriterioBusca")
        lblSubTitulosTiposSetor.Text = Traduzir("019_lbl_subtitulo_tipossetor")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")
        Master.Titulo = Traduzir("019_lbl_titulo_tipossetor")

        'botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnAltaAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAltaAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")


        'Grid
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("004_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("004_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("004_lbl_grd_vigente")

        'Formulario
        lblCodigoTiposSetor.Text = Traduzir("019_lbl_codigo")
        lblDescricaoTiposSetor.Text = Traduzir("019_lbl_descripciontiposSetor")
        lblVigenteForm.Text = Traduzir("019_chk_vigente")
        chkVigenteForm.Text = String.Empty
        lblTituloTiposSetor.Text = Traduzir("019_titulo_matenimentotiposSetor")
        lblTituloCaracteristicas.Text = Traduzir("019_titulo_caracteristicas_matenimentotiposSetor")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescricaoCodeA.Text = Traduzir("019_lbl_descricaoAjeno")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("019_msg_tipoSetorcodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("019_msg_tipoSetordescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("019_msg_codigotiposSetorexistente")
        csvlstCaracteristicas.ErrorMessage = Traduzir("019_msg_CaracteristicaObrigatoria")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
    ''' </history>
    Property FiltroVigente() As Boolean
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Boolean)
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

    Property FiltroCaracteristicas() As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaColeccion
        Get
            Return ViewState("FiltroCaracteristicas")
        End Get
        Set(value As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaColeccion)
            ViewState("FiltroCaracteristicas") = value
        End Set
    End Property

    Public Property TiposSetorTemPorario() As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion
        Get
            If ViewState("TiposSetorTemPorario") Is Nothing Then
                ViewState("TiposSetorTemPorario") = New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion
            End If

            Return DirectCast(ViewState("TiposSetorTemPorario"), IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion)
        End Get

        Set(value As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion)
            ViewState("TiposSetorTemPorario") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Public Function getTiposSetor() As IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta
        Dim objProxyTiposSetor As New Comunicacion.ProxyTipoSetor
        Dim objPeticionTiposSetor As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuestaTiposSetor As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        objPeticionTiposSetor.bolActivo = FiltroVigente
        objPeticionTiposSetor.CaractTipoSector = FiltroCaracteristicas
        objPeticionTiposSetor.desTipoSector = FiltroDescripcion
        objPeticionTiposSetor.codTipoSector = FiltroCodigo
        objPeticionTiposSetor.ParametrosPaginacion.RealizarPaginacion = False

        objRespuestaTiposSetor = objProxyTiposSetor.GetTiposSectores(objPeticionTiposSetor)

        TiposSetorTemPorario = objRespuestaTiposSetor.TipoSetor

        Return objRespuestaTiposSetor
    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Public Sub PreencherTiposSetor()

        Dim objRespostaTiposSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        'Busca os canais
        objRespostaTiposSetor = getTiposSetor()

        If Not Master.ControleErro.VerificaErro(objRespostaTiposSetor.CodigoError, objRespostaTiposSetor.NombreServidorBD, objRespostaTiposSetor.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaTiposSetor.TipoSetor.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaTiposSetor.TipoSetor.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaTiposSetor.TipoSetor)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codTipoSector ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " codTipoSector ASC "
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

    ''' <summary>
    ''' Função responsável por fazer um select na hora que e selecionado um registro no grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Private Function SelectTiposSetor(TiposSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion, codigo As String) As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor

        Dim retorno = From c In TiposSetor Where c.codTipoSector = codigo Select c.desTipoSector, c.bolActivo, c.CaractTipoSector, _
                                                 c.oidTipoSector, c.gmtCreacion, c.desUsuarioCreacion

        Dim objTipoSetor As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor

        Dim en As IEnumerator = retorno.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then
            objRetorno = en.Current
            objTipoSetor.oidTipoSector = retorno(0).oidTipoSector
            objTipoSetor.codTipoSector = codigo
            objTipoSetor.desTipoSector = retorno(0).desTipoSector
            objTipoSetor.bolActivo = retorno(0).bolActivo
            objTipoSetor.gmtCreacion = retorno(0).gmtCreacion
            objTipoSetor.desUsuarioCreacion = retorno(0).desUsuarioCreacion
            objTipoSetor.CaractTipoSector = retorno(0).CaractTipoSector

        End If

        Return objTipoSetor
    End Function

    ''' <summary>
    ''' Função responsável por fazer o tratamento do foco.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
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
    ''' Função responsavel por preencher a lista de características.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/01/2013 Created
    ''' </history>
    Public Sub PreencherListaCaracteristicas()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaractTipoSector()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Caracteristicas.Count = 0 Then
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Caracteristica In objRespuesta.Caracteristicas
            objCaracteristica.Descripcion = objCaracteristica.Codigo & " - " & objCaracteristica.Descripcion
        Next

        lstCaracteristicas.AppendDataBoundItems = True
        lstCaracteristicas.Items.Clear()
        lstCaracteristicas.DataTextField = "Descripcion"
        lstCaracteristicas.DataValueField = "Codigo"
        lstCaracteristicas.DataSource = objRespuesta.Caracteristicas
        lstCaracteristicas.DataBind()
    End Sub

    Private Sub EditarDados()
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Utilidad.eAcao.Modificacion

                PreencherListaCaracteristicasForm()
                CarregaDados(codigo)
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarCamposForm(True)
                txtCodigoTipoSetor.Enabled = False
                txtDescricaoTiposSetor.Focus()
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
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript
        Try
            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("bolactivo").ToString.ToLower & ");"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Responsavel pela ordenação do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try

            Dim objDT As DataTable

            Dim objRespoustaTiposSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

            objRespoustaTiposSetor = getTiposSetor()

            If objRespoustaTiposSetor.TipoSetor.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                objDT = ProsegurGridView1.ConvertListToDataTable(objRespoustaTiposSetor.TipoSetor)

                If ProsegurGridView1.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codTipoSector asc"
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

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView1.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPager_SetCss

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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 15
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

                If CBool(e.Row.DataItem("bolactivo")) Then
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(3).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("004_lbl_grd_codigo")
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8

                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("004_lbl_grd_descripcion")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 9
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 10

                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("004_lbl_grd_vigente")

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try

            txtCodigoTipoSector.Text = String.Empty
            txtDescricaoTipoSector.Text = String.Empty
            chkVigente.Checked = True
            lstCaracteristicas.ClearSelection()

            pnlSemRegistro.Visible = False

            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            btnCancelar_Click(Nothing, Nothing)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        If txtCodigoTipoSector.Text.Trim <> String.Empty Then
            FiltroCodigo = txtCodigoTipoSector.Text.Trim.ToUpper
        Else
            FiltroCodigo = String.Empty
        End If

        If txtDescricaoTipoSector.Text.Trim <> String.Empty Then
            FiltroDescripcion = txtDescricaoTipoSector.Text.Trim.ToUpper
        Else
            FiltroDescripcion = String.Empty
        End If

        'Filtros
        FiltroVigente = chkVigente.Checked
        Dim indices As Integer() = lstCaracteristicas.GetSelectedIndices()
        FiltroCaracteristicas = New ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaColeccion
        Dim objCaracteristica As ContractoServicio.TipoSetor.GetTiposSectores.Caracteristica
        For Each indice As Integer In indices
            objCaracteristica = New ContractoServicio.TipoSetor.GetTiposSectores.Caracteristica
            objCaracteristica.codCaractTipoSector = lstCaracteristicas.Items(indice).Value
            FiltroCaracteristicas.Add(objCaracteristica)
        Next

        'Retorna os canais de acordo com o filtro aciam
        PreencherTiposSetor()
        UpdatePanelGrid.Update()



    End Sub

    ''' <summary>
    ''' Evento do botão deletar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 Created
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
                Dim objPeticionTipoSetor As New IAC.ContractoServicio.TipoSetor.SetTiposSectores.Peticion
                Dim objRespuestaTipoSetor As IAC.ContractoServicio.TipoSetor.SetTiposSectores.Respuesta

                'Retorna a linha selecionada no grid
                Dim objTiposSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor
                objTiposSetor = SelectTiposSetor(TiposSetorTemPorario, Server.UrlDecode(codigo))

                objPeticionTipoSetor.oidTipoSector = objTiposSetor.oidTipoSector
                objPeticionTipoSetor.codTipoSector = objTiposSetor.codTipoSector
                objPeticionTipoSetor.desTipoSector = objTiposSetor.desTipoSector
                objPeticionTipoSetor.bolActivo = False
                objPeticionTipoSetor.gmtCreacion = objTiposSetor.gmtCreacion
                objPeticionTipoSetor.gmtModificacion = DateTime.Now
                objPeticionTipoSetor.desUsuarioCreacion = objTiposSetor.desUsuarioCreacion
                objPeticionTipoSetor.desUsuarioModificacion = MyBase.LoginUsuario

                If objTiposSetor.CaractTipoSector IsNot Nothing AndAlso objTiposSetor.CaractTipoSector.Count > 0 Then
                    objPeticionTipoSetor.codCaractTipoSector = New ContractoServicio.TipoSetor.SetTiposSectores.CaracteristicaColeccion()
                    For Each objCaracteristica As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuesta In objTiposSetor.CaractTipoSector
                        objPeticionTipoSetor.codCaractTipoSector.Add(New ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica() With {.codCaractTipoSector = objCaracteristica.Codigo})
                    Next
                End If

                'Exclui a petição
                objRespuestaTipoSetor = objProxyTipoSetor.SetTiposSectores(objPeticionTipoSetor)

                If Master.ControleErro.VerificaErro(objRespuestaTipoSetor.CodigoError, objRespuestaTipoSetor.NombreServidorBD, objRespuestaTipoSetor.MensajeError, True, False) Then

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaTipoSetor.MensajeError)
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
    ''' Configuração dos controles da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 04/03/2013 - Criado
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
                txtCodigoTipoSector.Text = String.Empty
                txtDescricaoTipoSector.Text = String.Empty
                lstCaracteristicas.ClearSelection()
                txtCodigoTipoSector.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select
    End Sub

#End Region
#Region "[PROPRIEDADES FORMULARIO]"

    ''' <summary>
    ''' Propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/03/2013 Criado
    ''' </history> 
    Public Property TipoSetor() As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor
        Get
            Return DirectCast(ViewState("TipoSetor"), IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor)
        End Get
        Set(value As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor)
            ViewState("TipoSetor") = value
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

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
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

    Private Property OidTipoSetor() As String
        Get
            Return ViewState("OidTipoSetor")
        End Get
        Set(value As String)
            ViewState("OidTipoSetor") = value
        End Set
    End Property


    ''' <summary>
    ''' Armazena em viewstate os códigos ajenos na petição
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/07/2013 - Criado
    ''' </history>
    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

#End Region
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            LimparCampos()
            PreencherListaCaracteristicasForm()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            HabilitarCamposForm(True)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Alta
            txtCodigoTipoSetor.Focus()

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
            btnSalvar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            HabilitarCamposForm(False)
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#Region "[Dados Formulario]"
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TTIPO_SECTOR") IsNot Nothing Then

            If TipoSetor Is Nothing Then
                TipoSetor = New ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor
            End If

            TipoSetor.CodigosAjenos = Session("objRespuestaGEPR_TTIPO_SECTOR")
            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")

            Dim iCodigoAjeno = (From item In TipoSetor.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If TipoSetor.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = TipoSetor.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If
    End Sub
    Private Sub ExecutarGrabar()
        Try

            Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
            Dim objPeticionTipoSetor As New IAC.ContractoServicio.TipoSetor.SetTiposSectores.Peticion
            Dim objRespuestaTipoSetor As IAC.ContractoServicio.TipoSetor.SetTiposSectores.Respuesta


            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionTipoSetor.bolActivo = True
            Else
                objPeticionTipoSetor.bolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade
            EsVigente = chkVigenteForm.Checked

            objPeticionTipoSetor.codTipoSector = txtCodigoTipoSetor.Text
            objPeticionTipoSetor.desTipoSector = txtDescricaoTiposSetor.Text
            objPeticionTipoSetor.oidTipoSector = OidTipoSetor

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionTipoSetor.gmtCreacion = DateTime.Now
                objPeticionTipoSetor.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticionTipoSetor.gmtCreacion = TipoSetor.gmtCreacion
                objPeticionTipoSetor.desUsuarioCreacion = TipoSetor.desUsuarioCreacion
            End If

            objPeticionTipoSetor.gmtModificacion = DateTime.Now
            objPeticionTipoSetor.desUsuarioModificacion = MyBase.LoginUsuario

            objPeticionTipoSetor.codCaractTipoSector = New ContractoServicio.TipoSetor.SetTiposSectores.CaracteristicaColeccion()

            For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                objPeticionTipoSetor.codCaractTipoSector.Add(New ContractoServicio.TipoSetor.SetTiposSectores.Caracteristica() With {.codCaractTipoSector = objItem.Value})
            Next

            objPeticionTipoSetor.CodigosAjenos = CodigosAjenosPeticion

            objRespuestaTipoSetor = objProxyTipoSetor.SetTiposSectores(objPeticionTipoSetor)
            'Limpar o oidTipoSetor
            OidTipoSetor = String.Empty

            Session.Remove("objRespuestaGEPR_TTIPO_SECTOR")
            If Master.ControleErro.VerificaErro(objRespuestaTipoSetor.CodigoError, objRespuestaTipoSetor.NombreServidorBD, objRespuestaTipoSetor.MensajeError) Then
                If Master.ControleErro.VerificaErro(objRespuestaTipoSetor.CodigoError, objRespuestaTipoSetor.NombreServidorBD, objRespuestaTipoSetor.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaTipoSetor.MensajeError)
                End If
            Else
                If objRespuestaTipoSetor.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    If objRespuestaTipoSetor.MensajeError <> String.Empty Then
                        MyBase.MostraMensagem(objRespuestaTipoSetor.MensajeError)
                    End If
                Else
                    MyBase.MostraMensagem(objRespuestaTipoSetor.MensajeError)
                End If
                Exit Sub
            End If
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

                'Verifica se o código do canal é obrigatório
                If txtCodigoTipoSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoSetor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoTiposSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTiposSetor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                If lstCaracteristicasSelecionadas.Items.Count = 0 Then
                    strErro.Append(csvlstCaracteristicas.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvlstCaracteristicas.IsValid = False
                End If

                If SetarFocoControle AndAlso Not focoSetado Then
                    imbAdicionarCaracteristicasSelecionadas.Focus()
                    focoSetado = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoTipoSetor.Text.Trim) AndAlso ExisteCodigoTipoSetor(txtCodigoTipoSetor.Text.Trim) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoSetor.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Private Function ExisteCodigoTipoSetor(codigoTipoSetor As String) As Boolean

        Dim objRespostaVerificarCodigoTipoSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
            Dim objPeticionVerificarCodigoTipoSetor As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Peticion

            'Verifica se o código do Tipo existe no BD
            objPeticionVerificarCodigoTipoSetor.codTipoSector = codigoTipoSetor.Trim
            objPeticionVerificarCodigoTipoSetor.bolActivo = Nothing
            objPeticionVerificarCodigoTipoSetor.ParametrosPaginacion.RealizarPaginacion = False

            objRespostaVerificarCodigoTipoSetor = objProxyTipoSetor.GetTiposSectores(objPeticionVerificarCodigoTipoSetor)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTipoSetor.CodigoError, objRespostaVerificarCodigoTipoSetor.NombreServidorBD, objRespostaVerificarCodigoTipoSetor.MensajeError) Then
                If (objRespostaVerificarCodigoTipoSetor.TipoSetor.Count > 0) Then
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
    Public Sub CarregaDados(codigo As String)

        Dim objProxy As New Comunicacion.ProxyTipoSetor
        Dim objPeticion As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        objPeticion.codTipoSector = codigo
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.GetTiposSectores(objPeticion)
        TipoSetor = objRespuesta.TipoSetor(0)

        If objRespuesta.TipoSetor IsNot Nothing Then
            Dim objColCaracteristicas As New ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuestaColeccion

            Dim iCodigoAjeno = (From item In objRespuesta.TipoSetor(0).CodigosAjenos
                               Where item.bolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.codAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.codAjeno
                txtDescricaoCodigoAjeno.Text = iCodigoAjeno.desAjeno
                txtDescricaoCodigoAjeno.ToolTip = iCodigoAjeno.desAjeno
            End If

            txtCodigoTipoSetor.Text = objRespuesta.TipoSetor(0).codTipoSector
            txtCodigoTipoSetor.ToolTip = objRespuesta.TipoSetor(0).codTipoSector

            txtDescricaoTiposSetor.Text = objRespuesta.TipoSetor(0).desTipoSector
            txtDescricaoTiposSetor.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSetor(0).desTipoSector, String.Empty)

            If objRespuesta.TipoSetor(0).bolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If
            chkVigenteForm.Checked = objRespuesta.TipoSetor(0).bolActivo
            EsVigente = objRespuesta.TipoSetor(0).bolActivo
            OidTipoSetor = objRespuesta.TipoSetor(0).oidTipoSector

            If objRespuesta.TipoSetor(0).CaractTipoSector IsNot Nothing Then

                For Each objCarac As ContractoServicio.TipoSetor.GetTiposSectores.CaracteristicaRespuesta In objRespuesta.TipoSetor(0).CaractTipoSector
                    objCarac.Descripcion = objCarac.Codigo & " - " & objCarac.Descripcion
                    objColCaracteristicas.Add(objCarac)
                Next

                lstCaracteristicasSelecionadas.AppendDataBoundItems = True
                lstCaracteristicasSelecionadas.Items.Clear()
                lstCaracteristicasSelecionadas.DataTextField = "Descripcion"
                lstCaracteristicasSelecionadas.DataValueField = "Codigo"
                lstCaracteristicasSelecionadas.DataSource = objColCaracteristicas
                lstCaracteristicasSelecionadas.DataBind()

                For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                    lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.Items.FindByValue(objItem.Value))
                Next

            End If

        End If

        'Se for modificação então guarda a descriçaõ atual para validação
        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            DescricaoAtual = txtDescricaoTiposSetor.Text
        End If

    End Sub
#End Region
#Region "[Métodos formulario]"
    Private Sub LimparCampos()
        txtCodigoTipoSetor.Text = String.Empty
        txtDescricaoTiposSetor.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDescricaoCodigoAjeno.Text = String.Empty
        chkVigenteForm.Checked = True
        lstCaracteristicasDisponiveis.Items.Clear()
        lstCaracteristicasSelecionadas.Items.Clear()
        CodigosAjenosPeticion = Nothing
        OidTipoSetor = String.Empty
        TipoSetor = Nothing

    End Sub
    Private Sub HabilitarCamposForm(habilitar As Boolean)
        txtCodigoTipoSetor.Enabled = habilitar
        txtDescricaoTiposSetor.Enabled = habilitar
        lstCaracteristicasDisponiveis.Visible = habilitar
        lstCaracteristicasSelecionadas.Enabled = habilitar
        imbAdicionarCaracteristicasSelecionadas.Visible = habilitar
        imbAdicionarTodasCaracteristicas.Visible = habilitar
        imbRemoverCaracteristicasSelecionadas.Visible = habilitar
        imbRemoverTodasCaracteristicas.Visible = habilitar

    End Sub

    Public Sub PreencherListaCaracteristicasForm()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaractTipoSector()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        If objRespuesta.Caracteristicas.Count = 0 Then
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaractTipoSector.Caracteristica In objRespuesta.Caracteristicas
            objCaracteristica.Descripcion = objCaracteristica.Codigo & " - " & objCaracteristica.Descripcion
        Next

        lstCaracteristicasDisponiveis.AppendDataBoundItems = True
        lstCaracteristicasDisponiveis.Items.Clear()
        lstCaracteristicasDisponiveis.DataTextField = "Descripcion"
        lstCaracteristicasDisponiveis.DataValueField = "Codigo"
        lstCaracteristicasDisponiveis.DataSource = objRespuesta.Caracteristicas
        lstCaracteristicasDisponiveis.DataBind()
    End Sub
#End Region
#Region "[Eventos Formulario]"
    Protected Sub imbAdicionarTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarTodasCaracteristicas.Click
        Try
            Dim objListItem As ListItem
            While lstCaracteristicasDisponiveis.Items.Count > 0
                objListItem = lstCaracteristicasDisponiveis.Items(0)
                lstCaracteristicasDisponiveis.Items.Remove(objListItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)
            End While
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imbAdicionarCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarCaracteristicasSelecionadas.Click
        Try
            While lstCaracteristicasDisponiveis.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasDisponiveis.SelectedItem
                lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.SelectedItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)
            End While
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imbRemoverCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverCaracteristicasSelecionadas.Click
        Try
            While lstCaracteristicasSelecionadas.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasSelecionadas.SelectedItem
                lstCaracteristicasSelecionadas.Items.Remove(lstCaracteristicasSelecionadas.SelectedItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)
            End While
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imbRemoverTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverTodasCaracteristicas.Click
        Try
            Dim objListItem As ListItem
            While lstCaracteristicasSelecionadas.Items.Count > 0
                objListItem = lstCaracteristicasSelecionadas.Items(0)
                lstCaracteristicasSelecionadas.Items.Remove(objListItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)
            End While
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnAltaAjeno_Click(sender As Object, e As EventArgs) Handles btnAltaAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoTipoSetor.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoTiposSetor.Text
            tablaGenesis.OidTablaGenesis = OidTipoSetor
            If TipoSetor IsNot Nothing AndAlso TipoSetor.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = TipoSetor.CodigosAjenos
            End If

            Session("objGEPR_TTIPO_SECTOR") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TTIPO_SECTOR"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TTIPO_SECTOR"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            ConsomeCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
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
                Acao = Utilidad.eAcao.Modificacion

                PreencherListaCaracteristicasForm()
                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarCamposForm(True)
                txtCodigoTipoSetor.Enabled = False
                txtDescricaoTiposSetor.Focus()
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
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(codigo)
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarCamposForm(False)
                chkVigenteForm.Enabled = False
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
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarCamposForm(False)
                chkVigenteForm.Enabled = False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class