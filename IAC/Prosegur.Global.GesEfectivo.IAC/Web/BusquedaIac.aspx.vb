Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Classe Busqueda Iac
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 30/01/2009 Criado
''' </history>
Partial Public Class BusquedaIac
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

        ' MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._DAR_BAJA.ToString, btnBaja)

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando prescionado enter.
        txtCodigoIac.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoIac.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        lstTerminos.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo caregado na inicialização da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda IAC")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            AdicionarScripts()

            If Not Page.IsPostBack Then
                'Preenche combobox de maquinas.
                PreencherListBoxTerminos()
                ExecutarBusca()
                UpdatePanelGrid.Update()

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnSalvar.Enabled = False

                chkVigenteForm.Checked = True
                chkVigenteForm.Enabled = False
                chkVigenteForm.Visible = False
                lblVigenteForm.Visible = False

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
    ''' [anselmo.gois] 30/01/2009 Criado
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
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        lblCodigoIac.Text = Traduzir("006_lbl_codigoiac")
        lblDescricaoIac.Text = Traduzir("006_lbl_descripcioniac")
        lblTerminos.Text = Traduzir("006_lbl_terminos")

        lblVigente.Text = Traduzir("006_chk_vigente")
        chkVigente.Text = String.Empty

        lblTituloIac.Text = Traduzir("006_titulo_iac")
        lblSubTituloIac.Text = Traduzir("006_subtitulo_iac")
        grdIac.PagerSummary = Traduzir("grid_lbl_pagersummary")
        Master.Titulo = Traduzir("006_title_iac")

        'botoes
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

        'GridBusca
        grdIac.Columns(1).HeaderText = Traduzir("006_lbl_grd_codigo")
        grdIac.Columns(2).HeaderText = Traduzir("006_lbl_grd_descripcion")
        grdIac.Columns(3).HeaderText = Traduzir("006_lbl_grd_vigente")

        ProsegurGridView1.Columns(0).HeaderText = Traduzir("006_lbl_grd_checkbox")
        ProsegurGridView1.Columns(1).HeaderText = Traduzir("006_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("006_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("006_lbl_grd_observaciones")

        ProsegurGridView2.Columns(0).HeaderText = Traduzir("006_lbl_grd_checkbox")
        ProsegurGridView2.Columns(1).HeaderText = Traduzir("006_lbl_grd_codigo")
        ProsegurGridView2.Columns(2).HeaderText = Traduzir("006_lbl_grd_descripcion")
        ProsegurGridView2.Columns(3).HeaderText = Traduzir("006_lbl_grd_obligatorio")
        ProsegurGridView2.Columns(4).HeaderText = Traduzir("006_lbl_grd_busqueda")
        ProsegurGridView2.Columns(5).HeaderText = Traduzir("006_lbl_grd_campoclave")
        ProsegurGridView2.Columns(6).HeaderText = Traduzir("006_lbl_grd_copiarTermino")
        ProsegurGridView2.Columns(7).HeaderText = Traduzir("006_lbl_grd_Protegido")
        ProsegurGridView2.Columns(9).HeaderText = Traduzir("006_lbl_grd_InvisibleRpt")
        ProsegurGridView2.Columns(10).HeaderText = Traduzir("006_lbl_grd_IdMecanizado")

        'Formulario

        lblCodigoIacForm.Text = Traduzir("006_lbl_codigoiac")
        lblDescricaoIacForm.Text = Traduzir("006_lbl_descripcioniac")

        lblVigenteForm.Text = Traduzir("006_chk_vigente")
        chkVigenteForm.Text = String.Empty
        lblInvisible.Text = Traduzir("006_chk_invisible")
        chkInvisible.Text = String.Empty

        lblCopiarDeclarados.Text = Traduzir("006_lbl_copiardeclarado")
        chkCopiarDeclarados.Text = String.Empty

        lblDisponivelNuevoSaldos.Text = Traduzir("006_lbl_disponivelnuevosaldos")
        chkDisponivelNuevoSaldos.Text = String.Empty

        lblTituloIacForm.Text = Traduzir("006_titulo_matenimentoiac")
        lblObservacionesIac.Text = Traduzir("006_lbl_observacion")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("006_msg_iaccodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("006_msg_iacdescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("006_msg_codigoiacexistente")
        csvDescricaoExistente.ErrorMessage = Traduzir("006_msg_descricaoiacexistente")
        csvTerminoObligatorio.ErrorMessage = Traduzir("006_msg_iacTerminoObligatorio")

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
    ''' [anselmo.gois] 30/01/2009 Criado
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

    Property FiltroTerminos() As ContractoServicio.Iac.GetIac.TerminosIacColeccion
        Get
            Return ViewState("FiltroTerminos")
        End Get
        Set(value As ContractoServicio.Iac.GetIac.TerminosIacColeccion)
            ViewState("FiltroTerminos") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena as informações adicionais encontradas na pesquisa.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InformacoesAdicionaisCliente() As ContractoServicio.Iac.GetIac.IacColeccion
        Get
            If ViewState("InformacoesAdicionaisCliente") Is Nothing Then
                ViewState("InformacoesAdicionaisCliente") = New ContractoServicio.Iac.GetIac.IacColeccion
            End If
            Return ViewState("InformacoesAdicionaisCliente")
        End Get
        Set(value As ContractoServicio.Iac.GetIac.IacColeccion)
            ViewState("InformacoesAdicionaisCliente") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"
    Private Sub LimparCampos()
        txtCodigoIacForm.Text = String.Empty
        txtDescricaoIacForm.Text = String.Empty
        txtObservacionesIac.Text = String.Empty
        chkVigenteForm.Checked = True
        chkCopiarDeclarados.Checked = False
        chkDisponivelNuevoSaldos.Checked = False
        chkInvisible.Checked = False

        hidTerminoIacSelecionado.Value = String.Empty
        hidTerminoSelecionado.Value = String.Empty
        hidObligatorio.Value = String.Empty
        hidObligatorioFalse.Value = String.Empty
        hidObligatorioValorTrue.Value = String.Empty
        hidObligatorioValorFalse.Value = String.Empty
        hidBusqueda.Value = String.Empty
        hidBusquedaFalse.Value = String.Empty
        hidCampoClave.Value = String.Empty
        hidOrden.Value = String.Empty
        hidCampoClaveFalse.Value = String.Empty
        hidBusquedaValorTrue.Value = String.Empty
        hidBusquedaValorFalse.Value = String.Empty
        hidClaveValorVerdadeiro.Value = String.Empty
        hidClaveValorFalso.Value = String.Empty
        hdnObjeto.Value = String.Empty
        hidCopiarTermino.Value = String.Empty
        hidCopiarTerminoFalse.Value = String.Empty
        hidCopiarTerminoValorVerdadeiro.Value = String.Empty
        hidCopiarTerminoValorFalso.Value = String.Empty
        hidProtegido.Value = String.Empty
        hidProtegidoFalse.Value = String.Empty
        hidProtegidoValorTrue.Value = String.Empty
        hidProtegidoValorFalse.Value = String.Empty
        hidInvisibleReporte.Value = String.Empty
        hidInvisibleReporteFalse.Value = String.Empty
        hidInvisibleReporteValorTrue.Value = String.Empty
        hidInvisibleReporteValorFalse.Value = String.Empty
        hidIdMecanizado.Value = String.Empty
        hidIdMecanizadoFalse.Value = String.Empty
        hidIdMecanizadoValorTrue.Value = String.Empty
        hidIdMecanizadoValorFalse.Value = String.Empty

        TerminosIacCompletos.Clear()
        TerminosIacTemPorario.Clear()
        TerminosTemPorario.Clear()

    End Sub

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Function getIac() As IAC.ContractoServicio.Iac.GetIac.Respuesta

        Dim objProxyIac As New Comunicacion.ProxyIac
        Dim objPeticionIac As New IAC.ContractoServicio.Iac.GetIac.Peticion
        Dim objRespuestaIac As New IAC.ContractoServicio.Iac.GetIac.Respuesta


        objPeticionIac.vigente = FiltroVigente
        objPeticionIac.DescripcionIac = FiltroDescripcion
        objPeticionIac.CodidoIac = FiltroCodigo
        objPeticionIac.TerminosIac = FiltroTerminos
        objRespuestaIac = objProxyIac.GetIac(objPeticionIac)

        Return objRespuestaIac
    End Function

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        'If (Not IsPostBack) Then
        '    Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        'Else
        '    If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
        '        Page.SetFocus(Request("__LASTFOCUS"))
        '    End If
        'End If

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub PreencherIac()
        Dim objRespostaIac As IAC.ContractoServicio.Iac.GetIac.Respuesta

        'Busca os canais
        objRespostaIac = getIac()

        If Not Master.ControleErro.VerificaErro(objRespostaIac.CodigoError, objRespostaIac.NombreServidorBD, objRespostaIac.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaIac.Iacs.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaIac.Iacs.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                ' armazenar informacoes adicionais cliente encontrada na pesquisa.
                InformacoesAdicionaisCliente = objRespostaIac.Iacs

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = grdIac.ConvertListToDataTable(objRespostaIac.Iacs)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodidoIac ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If grdIac.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodidoIac ASC "
                    Else
                        objDt.DefaultView.Sort = grdIac.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = grdIac.SortCommand
                End If

                grdIac.CarregaControle(objDt)

            Else

                ' limpar informacoes adicionais cliente.
                InformacoesAdicionaisCliente = New ContractoServicio.Iac.GetIac.IacColeccion

                'Limpa a consulta
                grdIac.DataSource = Nothing
                grdIac.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Else

            ' limpar informacoes adicionais cliente.
            InformacoesAdicionaisCliente = New ContractoServicio.Iac.GetIac.IacColeccion

            'Limpa a consulta
            grdIac.DataSource = Nothing
            grdIac.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Function getComboTerminos() As IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta
        Dim objProxyTerminos As New Comunicacion.ProxyUtilidad
        Dim objRespuestaTerminos As New IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta
        Dim objPeticionIac As New IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Peticion

        objPeticionIac.EsVigente = True

        objRespuestaTerminos = objProxyTerminos.GetComboTerminosIAC(objPeticionIac)

        Return objRespuestaTerminos
    End Function

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub PreencherListBoxTerminos()

        Dim objRepuestaTerminos As New IAC.ContractoServicio.Utilidad.GetComboTerminosIAC.Respuesta
        Dim obj As New Object
        Dim objDT As New DataTable

        objRepuestaTerminos = getComboTerminos()

        If Not Master.ControleErro.VerificaErro(objRepuestaTerminos.CodigoError, objRepuestaTerminos.NombreServidorBD, objRepuestaTerminos.MensajeError) Then
            Exit Sub
        End If

        lstTerminos.DataTextField = "Descripcion"
        lstTerminos.DataValueField = "Codigo"
        lstTerminos.DataSource = objRepuestaTerminos.Terminos
        lstTerminos.DataBind()

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
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub grdIac_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdIac.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            grdIac.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub grdIac_EPreencheDados(sender As Object, e As EventArgs) Handles grdIac.EPreencheDados

        Try


            Dim objDT As DataTable

            Dim objRespoustaIac As IAC.ContractoServicio.Iac.GetIac.Respuesta

            objRespoustaIac = getIac()

            If objRespoustaIac.Iacs.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False


                objDT = grdIac.ConvertListToDataTable(objRespoustaIac.Iacs)

                If grdIac.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " CodidoIac asc"
                Else
                    objDT.DefaultView.Sort = grdIac.SortCommand
                End If



                grdIac.ControleDataSource = objDT

            Else
                'Limpa a consulta
                grdIac.DataSource = Nothing
                grdIac.DataBind()

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
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub grdIac_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdIac.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción
                '2 - Observaciones
                '3 - Cuenta en Ciego
                '4 - Admiete IAC 
                '5 - Vigente

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

                If Not e.Row.DataItem("DescripcionIac") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionIac").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionIac").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If


                If CBool(e.Row.DataItem("vigente")) Then
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
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Protected Sub grdIac_EPager_SetCss(sender As Object, e As EventArgs) Handles grdIac.EPager_SetCss
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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 17
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
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub grdIac_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdIac.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then

                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_codigo")
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 11
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 12

                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_descripcion")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 13
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 14

                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("006_lbl_grd_vigente")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 15
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 16

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Evento click do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
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

        Dim strColTerminos As New ContractoServicio.Iac.GetIac.TerminosIacColeccion


        If txtCodigoIac.Text.Trim <> String.Empty Then
            FiltroCodigo = txtCodigoIac.Text.Trim.ToUpper
        Else
            FiltroCodigo = String.Empty
        End If

        If txtDescricaoIac.Text.Trim <> String.Empty Then
            FiltroDescripcion = txtDescricaoIac.Text.Trim.ToUpper
        Else
            FiltroDescripcion = String.Empty
        End If

        'Filtros
        FiltroVigente = chkVigente.Checked

        Dim strTerminos As ContractoServicio.Iac.GetIac.TerminosIac
        For Each objLinha As ListItem In lstTerminos.Items

            If objLinha.Selected Then

                strTerminos = New ContractoServicio.Iac.GetIac.TerminosIac
                strTerminos.CodigoTermino = objLinha.Value
                strColTerminos.Add(strTerminos)

            End If

        Next

        FiltroTerminos = strColTerminos


        'Retorna os canais de acordo com o filtro aciam
        PreencherIac()

    End Sub

    ''' <summary>
    ''' Evento click do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            grdIac.DataSource = Nothing
            grdIac.DataBind()

            LimparCampos()
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
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionIac As New IAC.ContractoServicio.Iac.SetIac.Peticion
            Dim objRespuestaIac As IAC.ContractoServicio.Iac.SetIac.Respuesta
            Dim objPeticionIacDetail As New ContractoServicio.Iac.GetIacDetail.Peticion
            Dim objRespuestaIacDetail As New ContractoServicio.Iac.GetIacDetail.Respuesta

            'Retorna o valor da linha selecionada no grid
            Dim strCodigo As String = grdIac.getValorLinhaSelecionada
            objPeticionIac.CodidoIac = strCodigo

            ' obter a descrição.
            objPeticionIac.DescripcionIac = (From IACS In InformacoesAdicionaisCliente _
                                             Where IACS.CodidoIac = strCodigo _
                                             Select IACS.DescripcionIac).First

            objPeticionIac.vigente = False
            objPeticionIac.CodUsuario = MyBase.LoginUsuario
            'Preenche o objeto objPeticionIacDetail
            objPeticionIacDetail.CodidoIac = New List(Of String)
            objPeticionIacDetail.CodidoIac.Add(strCodigo)

            'Chama o serviço GetIacDetail
            objRespuestaIacDetail = objProxyIac.GetIacDetail(objPeticionIacDetail)

            'Verifca se retornou erro.
            If Not Master.ControleErro.VerificaErro(objRespuestaIacDetail.CodigoError, objRespuestaIacDetail.NombreServidorBD, objRespuestaIacDetail.MensajeError) Then
                MyBase.MostraMensagem(objRespuestaIacDetail.MensajeError)
                Exit Sub
            End If

            objPeticionIac.EsDeclaradoCopia = objRespuestaIacDetail.Iacs(0).EsDeclaradoCopia

            'Verifica se retornou TerminosIac
            If objRespuestaIacDetail.Iacs(0).TerminosIac IsNot Nothing AndAlso objRespuestaIacDetail.Iacs(0).TerminosIac.Count > 0 Then

                'Istancia uma coleção de terminosIac
                objPeticionIac.TerminosIac = New ContractoServicio.Iac.SetIac.TerminosIacColeccion

                'Percorre a coleção de terminosIac
                For Each objTermino In objRespuestaIacDetail.Iacs(0).TerminosIac

                    'Adiciona o objTermino na coleção de terminosIac.
                    objPeticionIac.TerminosIac.Add(New ContractoServicio.Iac.SetIac.TerminosIac With { _
                                                   .CodigoTermino = objTermino.CodigoTermino, _
                                                   .DescripcionTermino = objTermino.DescripcionTermino, _
                                                   .EsBusquedaParcial = objTermino.EsBusquedaParcial, _
                                                   .EsCampoClave = objTermino.EsCampoClave, _
                                                   .EsObligatorio = objTermino.EsObligatorio, _
                                                   .OrdenTermino = objTermino.OrdenTermino, _
                                                   .EsTerminoCopia = objTermino.EsTerminoCopia})
                Next

            End If

            'Exclui a petição
            objRespuestaIac = objProxyIac.SetIac(objPeticionIac)

            If Master.ControleErro.VerificaErro(objRespuestaIac.CodigoError, objRespuestaIac.NombreServidorBD, objRespuestaIac.MensajeError, True, False) Then

                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                'Atualiza o GridView
                ExecutarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                MyBase.MostraMensagem(objRespuestaIac.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS PANELS]"

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Private Sub ControleBotoes()

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


                '    btnBaja.Visible = False

                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                ' btnBaja.Visible = False

                chkVigente.Checked = True
                pnlSemRegistro.Visible = False
                txtCodigoIac.Text = String.Empty
                txtDescricaoIac.Text = String.Empty
                lstTerminos.ClearSelection()
                txtCodigoIac.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca
                '  btnBaja.Visible = True

        End Select

    End Sub

#End Region

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub TamanhoLinhas()

        ProsegurGridView1.RowStyle.Height = 20

        ProsegurGridView2.RowStyle.Height = 20
    End Sub

    ''' <summary>
    '''Adiciona um evento java script na linha do grid, quando clicar na linha o checkbox sera checado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Dim chkAdiciona As System.Web.UI.WebControls.CheckBox
        chkAdiciona = e.Row.Cells(0).Controls(1)
        'Se for consulta desabilita o checkbox
        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            ProsegurGridView1.OnClickRowClientScript = "SelecionaCheckBox('" & chkAdiciona.ClientID & "','" & e.Row.DataItem("Codigo") & "','" & hidTerminoSelecionado.ClientID & "')"
        End If

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
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
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    '''Cabelho do Grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub CabecalhoVazioTerminos()

        ProsegurGridView1.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView1.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

    End Sub

    ''' <summary>
    '''Cabelho do Grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub CabecalhoVazioTerminosIac()

        ProsegurGridView2.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(4).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(5).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(6).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(7).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(8).HeaderStyle.CssClass = "CabecalhoQuandoVazio"
        ProsegurGridView2.Columns(9).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

    End Sub

    ''' <summary>
    ''' Verifica se existe itens checados
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirpatrick.santos] 25/07/2011 Criado
    ''' </history>
    Private Function esItemChecado(item As String) As Boolean
        'Return hidTerminosSelecionados.Value.IndexOf("|" & item & "|") >= 0
        Dim valor = hidTerminoSelecionado.Value
        Dim valores() As String = valor.Split("|")
        Return valores.FirstOrDefault(Function(f) f.Equals(item)) IsNot Nothing

    End Function

    ''' <summary>
    '''Adiciona um evento java script na linha do grid, quando clicar na linha o checkbox sera checado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView2_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.EOnClickRowClientScript

        Dim chkAdicionaTerminos As System.Web.UI.WebControls.CheckBox
        chkAdicionaTerminos = e.Row.Cells(0).Controls(1)
        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            ProsegurGridView2.OnClickRowClientScript = "SelecionaCheckBoxTerminosIac('" & chkAdicionaTerminos.ClientID & "','" & e.Row.DataItem("CodigoTermino") & "','" & hidTerminoIacSelecionado.ClientID & "','" & e.Row.DataItem("OrdenTermino") & "','" & hidOrden.ClientID & "')"
        End If

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Protected Sub ProsegurGridView2_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView2.EPager_SetCss

        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"
            ProsegurGridView2.Columns(0).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            ProsegurGridView2.Columns(1).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(2).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(3), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(3), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(3), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(3), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(3).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(4), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(4), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(4), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(4), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(4).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(5), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(5), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(5), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(5), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(5).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(6), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(6), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(6), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(6), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(6).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(7), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(7), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(7), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(7), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(7).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(8), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(8), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(8), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(8), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(8).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(9), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(9), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(9), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(9), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(9).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

            CType(CType(sender, ArrayList)(10), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(10), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(10), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(10), Object).Font.Name = "Verdana"
            ProsegurGridView2.Columns(10).HeaderStyle.CssClass = "CabecalhoQuandoVazio"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '0 - Checkbox
                '1 - Codigo
                '2 - Descripcion
                '3 - Observaciones

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim objChk As CheckBox = CType(e.Row.Cells(0).FindControl("chkAdicionaGridTerminos"), CheckBox)
                objChk.Attributes.Add("ValorChkSelecionado", e.Row.DataItem("Codigo"))

                If esItemChecado(e.Row.DataItem("Codigo").ToString) Then
                    objChk.Checked = True
                End If

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If


                If Not e.Row.DataItem("Observacion") Is DBNull.Value AndAlso e.Row.DataItem("Observacion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("Observacion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(0).Controls(1)
                chk.Attributes.Add("onclick", "executarlinha=false;")

                'Se for consulta desabilita o checkbox
                If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    objChk.Enabled = False
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configuração das colunas do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowDataBound

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - checkbox
                '1 - Código
                '2 - Descripción
                '3 - Obligatorio
                '4 - Busqueda
                '5 - Campo Clave
                '6 - Protegido
                '7 - Orden Termino(coluna oculta)
                '8 - Invisible Report 
                '9 - Id Mecanizado

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha


                If Not e.Row.DataItem("DescripcionTermino") Is DBNull.Value AndAlso e.Row.DataItem("DescripcionTermino").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("DescripcionTermino").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("EsObligatorio")) Then
                    CType(e.Row.Cells(3).FindControl("chkObligatorio"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(3).FindControl("chkObligatorio"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsBusquedaParcial")) Then
                    CType(e.Row.Cells(4).FindControl("chkBusqueda1"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(4).FindControl("chkBusqueda1"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsCampoClave")) Then
                    CType(e.Row.Cells(5).FindControl("chkAdicionaGridTerminosIac"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(5).FindControl("chkAdicionaGridTerminosIac"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsTerminoCopia")) Then
                    CType(e.Row.Cells(6).FindControl("chkCopiaTermino"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(6).FindControl("chkCopiaTermino"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("EsProtegido")) Then
                    CType(e.Row.Cells(7).FindControl("chkProtegido"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(7).FindControl("chkProtegido"), CheckBox).Checked = False
                End If

                'celula nove pois existe uma oculta antes (8)
                If CBool(e.Row.DataItem("esInvisibleRpte")) Then
                    CType(e.Row.Cells(9).FindControl("chkInvisibleRpte"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(9).FindControl("chkInvisibleRpte"), CheckBox).Checked = False
                End If

                If CBool(e.Row.DataItem("esIdMecanizado")) Then
                    CType(e.Row.Cells(10).FindControl("chkIdMecanizado"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(10).FindControl("chkIdMecanizado"), CheckBox).Checked = False
                End If

                Dim chkCopiar As System.Web.UI.WebControls.CheckBox
                chkCopiar = e.Row.Cells(6).Controls(1)

                Dim chkProt As System.Web.UI.WebControls.CheckBox
                chkProt = e.Row.Cells(7).Controls(1)

                Dim chkInvis As System.Web.UI.WebControls.CheckBox
                chkInvis = e.Row.Cells(9).Controls(1)

                Dim chkIdMecanizado As System.Web.UI.WebControls.CheckBox
                chkIdMecanizado = e.Row.Cells(10).Controls(1)

                Dim chk As System.Web.UI.WebControls.CheckBox
                chk = e.Row.Cells(0).Controls(1)
                chk.Attributes.Add("onclick", "executarlinha=false;")

                Dim chkObligatorio As System.Web.UI.WebControls.CheckBox
                chkObligatorio = e.Row.Cells(3).Controls(1)
                chkObligatorio.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidObligatorio.ClientID & "','" & hidObligatorioFalse.ClientID & "','" & hidObligatorioValorTrue.ClientID & "','" & hidObligatorioValorFalse.ClientID & "')")

                Dim chkBusqueda As System.Web.UI.WebControls.CheckBox
                chkBusqueda = e.Row.Cells(4).Controls(1)
                chkBusqueda.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidBusqueda.ClientID & "','" & hidBusquedaFalse.ClientID & "','" & hidBusquedaValorTrue.ClientID & "','" & hidBusquedaValorFalse.ClientID & "')")

                Dim chkClave As System.Web.UI.WebControls.CheckBox
                chkClave = e.Row.Cells(5).Controls(1)

                Dim striptClave As String = "BloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "','" & chkCopiar.ClientID & "','" & _
                                        hidCopiarTermino.ClientID & "','" & hidCopiarTerminoFalse.ClientID & "','" & hidCopiarTerminoValorVerdadeiro.ClientID & "','" & hidCopiarTerminoValorFalso.ClientID & "');"

                striptClave += "DesbloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "','" & chkIdMecanizado.ClientID & "','" & _
                                        hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                chkClave.Attributes.Add("onclick", striptClave)

                chkCopiar.Attributes.Add("onclick", "BloqueiaCheckBoxResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidCopiarTermino.ClientID & "','" & hidCopiarTerminoFalse.ClientID & "','" & hidCopiarTerminoValorVerdadeiro.ClientID & "','" & hidCopiarTerminoValorFalso.ClientID & "','" & chkClave.ClientID & "','" & _
                                         hidCampoClave.ClientID & "','" & hidCampoClaveFalse.ClientID & "','" & hidClaveValorVerdadeiro.ClientID & "','" & hidClaveValorFalso.ClientID & "')")

                chkProt.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidProtegido.ClientID & "','" & hidProtegidoFalse.ClientID & "','" & hidProtegidoValorTrue.ClientID & "','" & hidProtegidoValorFalse.ClientID & "')")

                chkInvis.Attributes.Add("onclick", "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidInvisibleReporte.ClientID & "','" & hidInvisibleReporteFalse.ClientID & "','" & hidInvisibleReporteValorTrue.ClientID & "','" & hidInvisibleReporteValorFalse.ClientID & "')")

                'Dim scriptIdMecanizado As String = "ResultadosTrueOrFalse(this,'" & e.Row.DataItem("CodigoTermino") & "','" & hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                Dim scriptIdMecanizado As String = "DesmarcaTodosCheckBoxExceto(this,'" & ProsegurGridView2.ClientID & "','9','" & e.Row.DataItem("CodigoTermino") & "','" & hidIdMecanizado.ClientID & "','" & hidIdMecanizadoFalse.ClientID & "','" & hidIdMecanizadoValorTrue.ClientID & "','" & hidIdMecanizadoValorFalse.ClientID & "');"

                chkIdMecanizado.Attributes.Add("onclick", scriptIdMecanizado)
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
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView2.RowCreated

        Try

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados
        Try

            Dim objDT As DataTable


            Dim objColTerminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            objColTerminos = TerminosTemPorario


            If objColTerminos IsNot Nothing _
                AndAlso objColTerminos.Count > 0 Then

                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridView1.ConvertListToDataTable(objColTerminos)

                objDT.DefaultView.Sort = " Codigo ASC"

                ProsegurGridView1.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridView1.DataSource = Nothing
                ProsegurGridView1.DataBind()

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[PROPRIEDADES] FORMULARIO"

    ''' <summary>
    ''' Propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history> 
    Private Property Iac() As IAC.ContractoServicio.Iac.GetIac.Iac
        Get
            Return DirectCast(ViewState("Iac"), IAC.ContractoServicio.Iac.GetIac.Iac)
        End Get
        Set(value As IAC.ContractoServicio.Iac.GetIac.Iac)
            ViewState("Iac") = value
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

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property TerminosIacTemPorario() As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        Get
            If ViewState("TerminosIacTemPorario") Is Nothing Then
                ViewState("TerminosIacTemPorario") = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
            End If

            Return DirectCast(ViewState("TerminosIacTemPorario"), IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
            ViewState("TerminosIacTemPorario") = value
        End Set
    End Property

    Private Property TerminosTemPorario() As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        Get
            If ViewState("TerminosTemPorario") Is Nothing Then
                ViewState("TerminosTemPorario") = New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            End If

            Return DirectCast(ViewState("TerminosTemPorario"), IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion)
            ViewState("TerminosTemPorario") = value
        End Set
    End Property

    Private Property TerminosIacCompletos() As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        Get
            If ViewState("TerminosIacCompletos") Is Nothing Then
                ViewState("TerminosIacCompletos") = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
            End If

            Return DirectCast(ViewState("TerminosIacCompletos"), IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
        End Get

        Set(value As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion)
            ViewState("TerminosIacCompletos") = value
        End Set
    End Property

    Private Property ValorDescricao() As String
        Get
            Return ViewState("ValorDescricao")
        End Get
        Set(value As String)
            ViewState("ValorDescricao") = value
        End Set
    End Property

    Private Property ValorCodigo() As String
        Get
            Return ViewState("ValorCodigo")
        End Get
        Set(value As String)
            ViewState("ValorCodigo") = value
        End Set
    End Property

    Private Property ValorVigente() As Boolean
        Get
            Return ViewState("ValorVigente")
        End Get
        Set(value As Boolean)
            ViewState("ValorVigente") = value
        End Set
    End Property

    Private Property ValorInvisible() As Boolean
        Get
            Return ViewState("ValorInvisible")
        End Get
        Set(value As Boolean)
            ViewState("ValorInvisible") = value
        End Set
    End Property

    Private Property Salvar() As Boolean
        Get
            Return ViewState("Salvar")
        End Get
        Set(value As Boolean)
            ViewState("Salvar") = value
        End Set
    End Property

    Private Property ValorObservaciones() As String
        Get
            Return ViewState("ValorObservaciones")
        End Get
        Set(value As String)
            ViewState("ValorObservaciones") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = value
            End If
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

    ''' <summary>
    '''  Armazena a descrição atual.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

#End Region

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta
            LimparCampos()

            CabecalhoVazioTerminos()
            CabecalhoVazioTerminosIac()
            CarregaGridTerminosIac()
            getTerminos()
            CarregaGrid()

            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnSalvar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            txtCodigoIacForm.Enabled = True
            SetFocus(txtCodigoIacForm)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub CarregaGrid()

        Dim objColTerminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
        objColTerminos = TerminosTemPorario

        If objColTerminos.Count > 0 Then

            'Preenche os controles do formulario

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoIacForm.Text
            End If


            ProsegurGridView1.PageSize = Aplicacao.Util.Utilidad.getMaximoRegistroGrid
            'Verifica se a consulta não retornou mais registros que o permitido
            If objColTerminos.Count > Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                ProsegurGridView1.AllowPaging = True
                ProsegurGridView1.PaginacaoAutomatica = True
            End If

            Dim objDt As DataTable
            objDt = ProsegurGridView1.ConvertListToDataTable(objColTerminos)
            objDt.DefaultView.Sort = " Codigo ASC"

            ProsegurGridView1.CarregaControle(objDt)

        Else


            'Limpa a consulta

            ProsegurGridView1.ExibirCabecalhoQuandoVazio = True
            ProsegurGridView1.EmptyDataText = Traduzir("info_msg_grd_vazio")
            ProsegurGridView1.CarregaControle(Nothing)

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        End If

    End Sub
    Private Sub CarregaGridTerminosIac()

        Dim objColTerminosIacs As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
        objColTerminosIacs = TerminosIacCompletos

        If objColTerminosIacs.Count > 0 Then

            'Preenche os controles do formulario

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoIacForm.Text
            End If

            If objColTerminosIacs IsNot Nothing AndAlso objColTerminosIacs.Count > 0 Then

                'pnlSemRegistro.Visible = False
                Dim objDt As DataTable
                objDt = ProsegurGridView2.ConvertListToDataTable(objColTerminosIacs)

                'If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " OrdenTermino ASC"
                'Else
                'objDt.DefaultView.Sort = ProsegurGridView1.SortCommand
                'End If

                ProsegurGridView2.CarregaControle(objDt)

                Dim esCampoClave As Boolean
                Dim esTerminoCopia As Boolean

                For Each row As GridViewRow In ProsegurGridView2.Rows
                    Dim rowLocal = row
                    esCampoClave = (From t In TerminosIacCompletos Where t.CodigoTermino = rowLocal.Cells(1).Text _
                                    AndAlso t.EsCampoClave = True).Count > 0

                    If esCampoClave Then
                        'Registro gravado com sucesso
                        DirectCast(row.FindControl("chkCopiaTermino"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    Else
                        DirectCast(row.FindControl("chkIdMecanizado"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    End If

                    esTerminoCopia = (From t In TerminosIacCompletos Where t.CodigoTermino = rowLocal.Cells(1).Text _
                                      AndAlso t.EsTerminoCopia = True).Count > 0

                    If esTerminoCopia Then
                        DirectCast(row.FindControl("chkAdicionaGridTerminosIac"), CheckBox).InputAttributes.Add("disabled", "disabled")
                    End If

                    If hidTerminoIacSelecionado.Value <> String.Empty Then
                        If row.Cells(1).Text = hidTerminoIacSelecionado.Value Then
                            Dim objCheck As CheckBox = DirectCast(row.FindControl("chkBusqueda"), CheckBox)
                            'objCheck.Checked = True
                            objCheck.Attributes.Add("onchange", "ResultadosTrueOrFalse(this,'" & row.Cells(1).Text & "','" & hidBusqueda.ClientID & "','" & hidBusquedaFalse.ClientID & "','" & hidBusquedaValorTrue.ClientID & "','" & hidBusquedaValorFalse.ClientID & "')")
                            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Click", "SelecionaCheckBoxTerminosIac('" & objCheck.ClientID & "','" & row.Cells(1).Text & "','" & hidTerminoIacSelecionado.ClientID & "','" & row.RowIndex + 1 & "','" & hidOrden.ClientID & "')", True)
                                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Click", "SelecionaCheckBoxTerminosIac('" & objCheck.ClientID & "','" & row.Cells(1).Text & "','" & hidTerminoIacSelecionado.ClientID & "','" & row.Cells(8).Text & "','" & hidOrden.ClientID & "')", True)
                            End If
                        End If
                    End If

                Next

            Else

                'Limpa a consulta
                'ProsegurGridView2.DataSource = Nothing
                'ProsegurGridView2.DataBind()
                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If
        Else

            ProsegurGridView2.ExibirCabecalhoQuandoVazio = True
            ProsegurGridView2.EmptyDataText = Traduzir("info_msg_grd_vazio")

            ProsegurGridView2.CarregaControle(Nothing)
        End If

    End Sub
#Region "[DADOS]"

    ''' <summary>
    ''' Metodo faz a chamada do metodo getIacDetail do acesso datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Function getIacDetail(codigoCanal As String) As IAC.ContractoServicio.Iac.GetIacDetail.IacColeccion

        Dim objProxyIac As New Comunicacion.ProxyIac
        Dim objPeticionIac As New IAC.ContractoServicio.Iac.GetIacDetail.Peticion
        Dim objRespuestaIac As New IAC.ContractoServicio.Iac.GetIacDetail.Respuesta
        Dim objTerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac


        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoCanal)

        objPeticionIac.CodidoIac = lista

        objRespuestaIac = objProxyIac.GetIacDetail(objPeticionIac)
        If objRespuestaIac.Iacs.Count > 0 Then
            TerminosIacTemPorario = objRespuestaIac.Iacs.Item(0).TerminosIac
            TerminosIacCompletos = objRespuestaIac.Iacs.Item(0).TerminosIac
            Return objRespuestaIac.Iacs
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Metodo faz a chamada do metodo getterminos do acesso datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Sub getTerminos()

        Dim objProxyTermino As New Comunicacion.ProxyTermino
        Dim objPeticionTermino As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.Peticion
        Dim objRespuestaTermino As IAC.ContractoServicio.TerminoIac.GetTerminoIac.Respuesta

        'Passa os parametros
        objPeticionTermino.MostrarCodigo = Nothing
        objPeticionTermino.VigenteTermino = True

        'chama o proxytermino
        objRespuestaTermino = objProxyTermino.getTerminos(objPeticionTermino)

        'faz a validaçãos dos dados recebidos pelo metodo gettermino e so mostra os dados que não existem nos terminos iac
        Dim objTerminos As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        If TerminosIacTemPorario.Count > 0 Then
            For Each objTerminos In objRespuestaTermino.TerminosIac

                If SelectTerminosIac(TerminosIacTemPorario, objTerminos.Codigo) = False Then

                    TerminosTemPorario.Add(objTerminos)

                End If
            Next
        Else
            TerminosTemPorario = objRespuestaTermino.TerminosIac
        End If

    End Sub
    Private Function SelectTerminosIac(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As Boolean

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ExisteCodigoIac(codigoIac As String) As Boolean
        Dim objRespostaVerificarCodigoIac As IAC.ContractoServicio.Iac.VerificarCodigoIac.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionVerificarCodigoIac As New IAC.ContractoServicio.Iac.VerificarCodigoIac.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoIac.CodigoTerminoIac = codigoIac
            objRespostaVerificarCodigoIac = objProxyIac.VerificarCodigoIac(objPeticionVerificarCodigoIac)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoIac.CodigoError, objRespostaVerificarCodigoIac.NombreServidorBD, objRespostaVerificarCodigoIac.MensajeError) Then
                Return objRespostaVerificarCodigoIac.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoIac.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    ''' <summary>
    ''' Informa se a descrição do iac já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/07/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoIac(descricao As String) As Boolean
        Dim objRespostaVerificarDescricaoIac As IAC.ContractoServicio.Iac.VerificarDescripcionIac.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionVerificarDescricaoIac As New IAC.ContractoServicio.Iac.VerificarDescripcionIac.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoIac.DescripcionTerminoIac = descricao
            objRespostaVerificarDescricaoIac = objProxyIac.VerificarDescripcionIac(objPeticionVerificarDescricaoIac)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoIac.CodigoError, objRespostaVerificarDescricaoIac.NombreServidorBD, objRespostaVerificarDescricaoIac.MensajeError) Then
                Return objRespostaVerificarDescricaoIac.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoIac.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
    Private Function SelectInformacoesTerminosIac(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino

        Dim objTerminos As New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.Codigo = objRetorno.CodigoTermino
            objTerminos.Descripcion = objRetorno.DescripcionTermino
            objTerminos.Observacion = objRetorno.ObservacionesTermino

        End If

        Return objTerminos
    End Function
    Private Function SelectInformacoesBusqueda(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In TerminosIac Where c.CodigoTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino, c.AdmiteValoresPosibles, c.CodigoAlgoritmoTermino, _
                      c.CodigoFormatoTermino, c.CodigoMascaraTermino, c.DescripcionAlgoritmoTermino, c.DescripcionFormatoTermino, c.DescripcionMascaraTermino, c.EsObligatorio, c.EsBusquedaParcial, _
                      c.EsCampoClave, c.LongitudTermino, c.MostarCodigo, c.OrdenTermino, c.VigenteTermino, c.EsTerminoCopia, c.esProtegido, c.esInvisibleRpte, c.esIdMecanizado

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = objRetorno.CodigoTermino
            objTerminos.DescripcionTermino = objRetorno.DescripcionTermino
            objTerminos.ObservacionesTermino = objRetorno.ObservacionesTermino
            objTerminos.AdmiteValoresPosibles = objRetorno.AdmiteValoresPosibles
            objTerminos.CodigoAlgoritmoTermino = objRetorno.CodigoAlgoritmoTermino
            objTerminos.CodigoFormatoTermino = objRetorno.CodigoFormatoTermino
            objTerminos.CodigoMascaraTermino = objRetorno.CodigoMascaraTermino
            objTerminos.DescripcionAlgoritmoTermino = objRetorno.DescripcionAlgoritmoTermino
            objTerminos.DescripcionFormatoTermino = objRetorno.DescripcionFormatoTermino
            objTerminos.DescripcionMascaraTermino = objRetorno.DescripcionMascaraTermino
            objTerminos.EsObligatorio = objRetorno.EsObligatorio
            objTerminos.EsBusquedaParcial = objRetorno.EsBusquedaParcial
            objTerminos.EsCampoClave = objRetorno.EsCampoClave
            objTerminos.LongitudTermino = objRetorno.LongitudTermino
            objTerminos.MostarCodigo = objRetorno.MostarCodigo
            objTerminos.OrdenTermino = objRetorno.OrdenTermino
            objTerminos.VigenteTermino = objRetorno.VigenteTermino
            objTerminos.EsTerminoCopia = objRetorno.EsTerminoCopia
            objTerminos.esProtegido = objRetorno.esProtegido
            objTerminos.esInvisibleRpte = objRetorno.esInvisibleRpte
            objTerminos.esIdMecanizado = objRetorno.esIdMecanizado

        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo da ordem informado existe na coleção desejada, se existir
    ''' retorna os dados expecificados nos select da função.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Function SelectInformacoesOrdenTermino(TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As Integer) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In TerminosIac Where c.OrdenTermino = codigo Select c.CodigoTermino, c.DescripcionTermino, c.ObservacionesTermino, c.AdmiteValoresPosibles, c.CodigoAlgoritmoTermino, _
                      c.CodigoFormatoTermino, c.CodigoMascaraTermino, c.DescripcionAlgoritmoTermino, c.DescripcionFormatoTermino, c.DescripcionMascaraTermino, c.EsObligatorio, c.EsBusquedaParcial, _
                      c.EsCampoClave, c.LongitudTermino, c.MostarCodigo, c.OrdenTermino, c.VigenteTermino, c.EsTerminoCopia, c.esProtegido, c.esInvisibleRpte, c.esIdMecanizado

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = objRetorno.CodigoTermino
            objTerminos.DescripcionTermino = objRetorno.DescripcionTermino
            objTerminos.ObservacionesTermino = objRetorno.ObservacionesTermino
            objTerminos.AdmiteValoresPosibles = objRetorno.AdmiteValoresPosibles
            objTerminos.CodigoAlgoritmoTermino = objRetorno.CodigoAlgoritmoTermino
            objTerminos.CodigoFormatoTermino = objRetorno.CodigoFormatoTermino
            objTerminos.CodigoMascaraTermino = objRetorno.CodigoMascaraTermino
            objTerminos.DescripcionAlgoritmoTermino = objRetorno.DescripcionAlgoritmoTermino
            objTerminos.DescripcionFormatoTermino = objRetorno.DescripcionFormatoTermino
            objTerminos.DescripcionMascaraTermino = objRetorno.DescripcionMascaraTermino
            objTerminos.EsObligatorio = objRetorno.EsObligatorio
            objTerminos.EsBusquedaParcial = objRetorno.EsBusquedaParcial
            objTerminos.EsCampoClave = objRetorno.EsCampoClave
            objTerminos.LongitudTermino = objRetorno.LongitudTermino
            objTerminos.MostarCodigo = objRetorno.MostarCodigo
            objTerminos.OrdenTermino = objRetorno.OrdenTermino
            objTerminos.VigenteTermino = objRetorno.VigenteTermino
            objTerminos.EsTerminoCopia = objRetorno.EsTerminoCopia
            objTerminos.esProtegido = objRetorno.esProtegido
            objTerminos.esInvisibleRpte = objRetorno.esInvisibleRpte
            objTerminos.esIdMecanizado = objRetorno.esIdMecanizado
        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select verificando se o codigo informado existe na coleção desejada, se existir
    ''' retorna o codigo, descrição e observação.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function SelectInformacoesTerminos(Terminos As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion, codigo As String) As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim retorno = From c In Terminos Where c.Codigo = codigo Select c.Descripcion, c.Observacion

        Dim objTerminos As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTerminos.CodigoTermino = codigo
            objTerminos.DescripcionTermino = objRetorno.Descripcion
            objTerminos.ObservacionesTermino = objRetorno.Observacion


        End If

        Return objTerminos
    End Function

    ''' <summary>
    ''' Ordena a coleção de string informada de forma crescente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function Orderacao(colecao As List(Of String)) As String()

        Dim colecaoConvertida As New List(Of Integer)

        For Each item In colecao
            colecaoConvertida.Add(CInt(item))
        Next

        Dim retorno = From c In colecaoConvertida Order By c

        Dim colecaoOrdenada() As String = Nothing

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        While en.MoveNext

            objRetorno = en.Current

            If colecaoOrdenada Is Nothing Then
                ReDim colecaoOrdenada(0)
            Else
                ReDim Preserve colecaoOrdenada(colecaoOrdenada.Length)
            End If

            colecaoOrdenada(colecaoOrdenada.Length - 1) = objRetorno

        End While

        Return colecaoOrdenada
    End Function


    ''' <summary>
    ''' Ordena a coleção de string informada de forma decrescente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Function OrderacaoDesc(colecao As List(Of String)) As String()

        Dim colecaoConvertida As New List(Of Integer)

        For Each item In colecao
            colecaoConvertida.Add(CInt(item))
        Next

        Dim retorno = From c In colecaoConvertida Order By c Descending

        Dim colecaoOrdenada() As String = Nothing

        Dim en As IEnumerator = retorno.GetEnumerator()

        Dim objRetorno As New Object

        While en.MoveNext

            objRetorno = en.Current

            If colecaoOrdenada Is Nothing Then
                ReDim colecaoOrdenada(0)
            Else
                ReDim Preserve colecaoOrdenada(colecaoOrdenada.Length)
            End If

            colecaoOrdenada(colecaoOrdenada.Length - 1) = objRetorno

        End While

        Return colecaoOrdenada
    End Function

    ''' <summary>
    ''' Deleta o item da coleção de terminos iac que contem o codigo informado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub SelectDeleteTerminosIac(ByRef TerminosIac As IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion, codigo As String)

        Dim objTermino As New ContractoServicio.Iac.GetIacDetail.TerminosIac

        For Each item As ContractoServicio.Iac.GetIacDetail.TerminosIac In TerminosIac

            'Item que sera removido
            If item.CodigoTermino.Equals(codigo) Then
                objTermino = item
                Exit For
            End If

        Next


        If objTermino IsNot Nothing AndAlso TerminosIac IsNot Nothing Then
            If TerminosIac.Count > 0 Then
                TerminosIac.Remove(objTermino)
            End If
        End If



    End Sub

    ''' <summary>
    ''' Deleta o item da coleção de terminos que contem o codigo informado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub SelectDeleteTerminos(ByRef TerminosIac As IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion, codigo As String)

        Dim objTermino As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac

        For Each item As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac In TerminosIac

            'Item que sera removido
            If item.Codigo.Equals(codigo) Then
                objTermino = item
                Exit For
            End If

        Next


        If objTermino IsNot Nothing AndAlso TerminosIac IsNot Nothing Then
            If TerminosIac.Count > 0 Then
                TerminosIac.Remove(objTermino)
            End If
        End If



    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxBusqueda()
        Dim busquedaVerdadeira As String
        Dim busquedasTrue() As String

        Dim busquedaFalsa As String
        Dim busquedasFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        busquedaVerdadeira = hidBusqueda.Value
        busquedasTrue = busquedaVerdadeira.Split("|")

        busquedaFalsa = hidBusquedaFalse.Value
        busquedasFalse = busquedaFalsa.Split("|")
        If busquedasFalse IsNot Nothing Then

            For Each itemFalso As String In busquedasFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsBusquedaParcial = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(busquedasFalse, 0, busquedasFalse.Length - 1)
        End If

        If busquedasTrue IsNot Nothing Then

            For Each item As String In busquedasTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsBusquedaParcial = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(busquedasTrue, 0, busquedasTrue.Length - 1)
        End If

        hidBusqueda.Value = Nothing
        hidBusquedaFalse.Value = Nothing
        busquedaFalsa = String.Empty
        busquedaVerdadeira = String.Empty

    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 28/05/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxObligatorio()
        Dim obligatorioVerdadeira As String
        Dim obligatoriosTrue() As String

        Dim obligatorioFalso As String
        Dim obligatoriosFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        obligatorioVerdadeira = hidObligatorio.Value
        obligatoriosTrue = obligatorioVerdadeira.Split("|")

        obligatorioFalso = hidObligatorioFalse.Value
        obligatoriosFalse = obligatorioFalso.Split("|")
        If obligatoriosFalse IsNot Nothing Then

            For Each itemFalso As String In obligatoriosFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsObligatorio = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(obligatoriosFalse, 0, obligatoriosFalse.Length - 1)
        End If

        If obligatoriosTrue IsNot Nothing Then

            For Each item As String In obligatoriosTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsObligatorio = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(obligatoriosTrue, 0, obligatoriosTrue.Length - 1)
        End If

        hidObligatorio.Value = Nothing
        hidObligatorioFalse.Value = Nothing
        obligatorioFalso = String.Empty
        obligatorioVerdadeira = String.Empty

    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxCampoClave()
        Dim claveVerdadeira As String
        Dim clavesTrue() As String

        Dim claveFalsa As String
        Dim clavesFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        claveVerdadeira = hidCampoClave.Value
        clavesTrue = claveVerdadeira.Split("|")

        claveFalsa = hidCampoClaveFalse.Value
        clavesFalse = claveFalsa.Split("|")

        If clavesFalse IsNot Nothing AndAlso clavesFalse.Length > 0 Then

            For Each itemFalso As String In clavesFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsCampoClave = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)


                    End If
                End If
            Next
            Array.Clear(clavesFalse, 0, clavesFalse.Length - 1)
        End If

        If clavesTrue IsNot Nothing AndAlso clavesTrue.Length > 0 Then

            For Each item As String In clavesTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsCampoClave = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If

                End If

            Next

            Array.Clear(clavesTrue, 0, clavesTrue.Length - 1)
        End If

        hidCampoClave.Value = Nothing
        hidCampoClaveFalse.Value = Nothing
        claveFalsa = String.Empty
        claveVerdadeira = String.Empty
    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub VerificaCheckBoxCopiarTermino()
        Dim CopiarTerminoVerdadeiro As String
        Dim CopiarTerminoTrue() As String

        Dim copiarTerminoFalso As String
        Dim CopiarTerminoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        CopiarTerminoVerdadeiro = hidCopiarTermino.Value
        CopiarTerminoTrue = CopiarTerminoVerdadeiro.Split("|")

        copiarTerminoFalso = hidCopiarTerminoFalse.Value
        CopiarTerminoFalse = copiarTerminoFalso.Split("|")

        If CopiarTerminoFalse IsNot Nothing AndAlso CopiarTerminoFalse.Length > 0 Then

            For Each itemFalso As String In CopiarTerminoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.EsTerminoCopia = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)


                    End If
                End If
            Next
            Array.Clear(CopiarTerminoFalse, 0, CopiarTerminoFalse.Length - 1)
        End If

        If CopiarTerminoTrue IsNot Nothing AndAlso CopiarTerminoTrue.Length > 0 Then

            For Each item As String In CopiarTerminoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.EsTerminoCopia = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If

                End If

            Next

            Array.Clear(CopiarTerminoTrue, 0, CopiarTerminoTrue.Length - 1)
        End If

        hidCopiarTermino.Value = Nothing
        hidCopiarTerminoFalse.Value = Nothing
        copiarTerminoFalso = String.Empty
        CopiarTerminoVerdadeiro = String.Empty
    End Sub

    ''' <summary>
    ''' Faz a verificação do campo hiden trazendo dos os codigos existentes no campo, depois com os codigos existentes ele verifica
    ''' se o codigo esta no hiden de campos falsos ou verdadeiros e gravando na coleção se o campo e verdadeiro ou falso.
    ''' </summary>
    Private Sub VerificaCheckBoxProtegido()
        Dim ProtegidoVerdadeira As String
        Dim ProtegidoTrue() As String

        Dim ProtegidoFalsa As String
        Dim ProtegidoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        ProtegidoVerdadeira = hidProtegido.Value
        ProtegidoTrue = ProtegidoVerdadeira.Split("|")

        ProtegidoFalsa = hidProtegidoFalse.Value
        ProtegidoFalse = ProtegidoFalsa.Split("|")
        If ProtegidoFalse IsNot Nothing Then

            For Each itemFalso As String In ProtegidoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esProtegido = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(ProtegidoFalse, 0, ProtegidoFalse.Length - 1)
        End If

        If ProtegidoTrue IsNot Nothing Then

            For Each item As String In ProtegidoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esProtegido = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(ProtegidoTrue, 0, ProtegidoTrue.Length - 1)
        End If

        hidProtegido.Value = Nothing
        hidProtegidoFalse.Value = Nothing
        ProtegidoFalsa = String.Empty
        ProtegidoVerdadeira = String.Empty
    End Sub

    Private Sub VerificaCheckBoxInvisibleReporte()
        Dim InvisibleReporteVerdadeira As String
        Dim InvisibleReporteTrue() As String

        Dim InvisibleReporteFalsa As String
        Dim InvisibleReporteFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        InvisibleReporteVerdadeira = hidInvisibleReporte.Value()
        InvisibleReporteTrue = InvisibleReporteVerdadeira.Split("|")

        InvisibleReporteFalsa = hidInvisibleReporteFalse.Value
        InvisibleReporteFalse = InvisibleReporteFalsa.Split("|")
        If InvisibleReporteFalse IsNot Nothing Then

            For Each itemFalso As String In InvisibleReporteFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esInvisibleRpte = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(InvisibleReporteFalse, 0, InvisibleReporteFalse.Length - 1)
        End If

        If InvisibleReporteTrue IsNot Nothing Then

            For Each item As String In InvisibleReporteTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esInvisibleRpte = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(InvisibleReporteTrue, 0, InvisibleReporteTrue.Length - 1)
        End If

        hidInvisibleReporte.Value = Nothing
        hidInvisibleReporteFalse.Value = Nothing
        InvisibleReporteFalsa = String.Empty
        InvisibleReporteVerdadeira = String.Empty
    End Sub

    Private Sub VerificaCheckBoxIdMecanizado()
        Dim IdMecanizadoVerdadeira As String
        Dim IdMecanizadoTrue() As String

        Dim IdMecanizadoFalsa As String
        Dim IdMecanizadoFalse() As String

        Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac

        IdMecanizadoVerdadeira = hidIdMecanizado.Value()
        IdMecanizadoTrue = IdMecanizadoVerdadeira.Split("|")

        IdMecanizadoFalsa = hidIdMecanizadoFalse.Value
        IdMecanizadoFalse = IdMecanizadoFalsa.Split("|")
        If IdMecanizadoFalse IsNot Nothing Then

            For Each itemFalso As String In IdMecanizadoFalse
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then
                    If itemFalso <> String.Empty AndAlso itemFalso <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, itemFalso)

                        If objTerminosIac.CodigoTermino.Equals(itemFalso) Then
                            objTerminosIac.esIdMecanizado = False
                        End If

                        SelectDeleteTerminosIac(TerminosIacCompletos, itemFalso)
                        TerminosIacCompletos.Add(objTerminosIac)
                    End If
                End If
            Next
            Array.Clear(IdMecanizadoFalse, 0, IdMecanizadoFalse.Length - 1)
        End If

        If IdMecanizadoTrue IsNot Nothing Then

            For Each item As String In IdMecanizadoTrue
                objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                If TerminosIacCompletos.Count > 0 Then

                    If item <> String.Empty AndAlso item <> Nothing Then
                        objTerminosIac = SelectInformacoesBusqueda(TerminosIacCompletos, item)

                        If objTerminosIac.CodigoTermino.Equals(item) Then
                            objTerminosIac.esIdMecanizado = True
                        End If


                        SelectDeleteTerminosIac(TerminosIacCompletos, item)
                        TerminosIacCompletos.Add(objTerminosIac)

                    End If

                End If

            Next

            Array.Clear(IdMecanizadoTrue, 0, IdMecanizadoTrue.Length - 1)
        End If

        hidIdMecanizado.Value = Nothing
        hidIdMecanizadoFalse.Value = Nothing
        IdMecanizadoFalsa = String.Empty
        IdMecanizadoVerdadeira = String.Empty
    End Sub
#End Region
#Region "[EVENTOS BOTOES FORM]"
    Private Sub btnremove_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnremove.Click
        Try

            Dim valor As String
            Dim valores() As String
            Dim ListaValores As New List(Of String)
            Dim ordemTerminoIac As Integer = 0

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            valor = hidTerminoIacSelecionado.Value

            valores = valor.Split("|")

            ListaValores = VerificaValorRepetido(valores)

            Dim TerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim ColTerminosIac As New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion

            Dim objTerminos As ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac
            Dim objColTerminos As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion

            If hidTerminoIacSelecionado.Value <> String.Empty AndAlso hidTerminoIacSelecionado.Value <> Nothing Then

                For Each item As String In ListaValores
                    If item <> Nothing AndAlso item <> String.Empty Then

                        objTerminos = New IAC.ContractoServicio.TerminoIac.GetTerminoIac.TerminoIac
                        objTerminos = SelectInformacoesTerminosIac(TerminosIacCompletos, item)

                        objColTerminos.Add(objTerminos)

                        TerminosTemPorario.Add(objTerminos)

                        SelectDeleteTerminosIac(TerminosIacCompletos, item)

                        Salvar = True

                    End If
                Next

            End If

            If TerminosIacCompletos.Count > 0 Then

                TerminosIacCompletos.Sort(Function(a, b) a.OrdenTermino.CompareTo(b.OrdenTermino))

                For i = 0 To TerminosIacCompletos.Count - 1
                    TerminosIacCompletos.Item(i).OrdenTermino = i + 1
                Next

            End If

            CarregaGrid()
            CarregaGridTerminosIac()
            valor = Nothing
            valores = Nothing
            ListaValores = Nothing
            hidTerminoIacSelecionado.Value = Nothing
            hidTerminoSelecionado.Value = Nothing
            hidOrden.Value = Nothing

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Adiciona todos os terminos selecionados no grid e na coleção de terminos iac, e os remove da coleção de terminos.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnAdiciona_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAdiciona.Click
        Try


            Dim valor As String
            Dim valores() As String
            Dim ListaValores As New List(Of String)

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            valor = hidTerminoSelecionado.Value

            valores = valor.Split("|")

            ListaValores = VerificaValorRepetido(valores)

            Dim objTerminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim objColTerminosIac As New ContractoServicio.Iac.GetIacDetail.TerminosIacColeccion
            Dim objColTerminos As New ContractoServicio.TerminoIac.GetTerminoIac.TerminoIacColeccion
            If hidTerminoSelecionado.Value <> String.Empty AndAlso hidTerminoSelecionado.Value <> Nothing Then

                For Each item As String In ListaValores
                    If item <> Nothing AndAlso item <> String.Empty Then
                        objTerminosIac = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                        objTerminosIac = SelectInformacoesTerminos(TerminosTemPorario, item)
                        objTerminosIac.OrdenTermino = TerminosIacCompletos.Count + 1

                        If TerminosIacCompletos.FindAll(Function(t) t.DescripcionTermino = objTerminosIac.DescripcionTermino).Count > 0 Then
                            MyBase.MostraMensagem(Traduzir("006_msg_terminos_misma_descripcion"))
                            Exit Sub
                        End If

                        objColTerminosIac.Add(objTerminosIac)
                        If TerminosIacTemPorario.Count > 0 Then
                            If SelectTerminosIac(TerminosIacTemPorario, objTerminosIac.CodigoTermino) Then
                                TerminosIacCompletos.Add(objTerminosIac)
                                SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                            Else
                                TerminosIacCompletos.Add(objTerminosIac)
                                SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                            End If
                        Else
                            TerminosIacCompletos.Add(objTerminosIac)
                            SelectDeleteTerminos(TerminosTemPorario, objTerminosIac.CodigoTermino)
                        End If
                    End If


                Next


            End If
            Salvar = True
            CarregaGridTerminosIac()
            CarregaGrid()
            valor = Nothing
            valores = Nothing
            ListaValores = Nothing
            hidTerminoSelecionado.Value = Nothing
            hidTerminoIacSelecionado.Value = Nothing
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Function VerificaValorRepetido(valores() As String) As List(Of String)

        Dim retorno = From c In valores Distinct

        Dim valoresRetorno As New List(Of String)

        If retorno.Count > 0 Then

            For Each item In retorno
                valoresRetorno.Add(item)
            Next
        End If

        Return valoresRetorno
    End Function

    Private Sub btnAcima_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Try

            Dim valor As String
            Dim valores() As String
            Dim ListaValores As New List(Of String)

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            valor = hidOrden.Value

            valores = valor.Split("|")

            ListaValores = VerificaValorRepetido(valores)

            Dim objTerminosIacSuperior As ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim objTerminosIacInferior As ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim contador As Integer = 0

            If valor <> String.Empty Then

                For Each item As String In Orderacao(ListaValores)
                    objTerminosIacSuperior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                    objTerminosIacInferior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

                    contador = contador + 1
                    If item <> String.Empty AndAlso item <> Nothing Then

                        If valores.Count <> TerminosIacCompletos.Count Then

                            If item <> 1 Then

                                If item <> contador Then

                                    Dim NumeroOrden As Integer
                                    NumeroOrden = 0
                                    NumeroOrden = CType(item, Integer)

                                    objTerminosIacSuperior = SelectInformacoesOrdenTermino(TerminosIacCompletos, NumeroOrden)
                                    objTerminosIacSuperior.OrdenTermino = NumeroOrden - 1

                                    objTerminosIacInferior = SelectInformacoesOrdenTermino(TerminosIacCompletos, objTerminosIacSuperior.OrdenTermino)
                                    objTerminosIacInferior.OrdenTermino = NumeroOrden

                                    SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacSuperior.CodigoTermino)
                                    SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacInferior.CodigoTermino)

                                    TerminosIacCompletos.Add(objTerminosIacInferior)
                                    TerminosIacCompletos.Add(objTerminosIacSuperior)

                                End If
                            End If
                        End If
                    End If
                Next
            End If

            Salvar = True
            CarregaGridTerminosIac()

            valor = Nothing
            valores = Nothing
            ListaValores = Nothing
            hidOrden.Value = Nothing
            hidTerminoIacSelecionado.Value = Nothing

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Altera a ordem dos registro, sendo que o registro selecionado decresce um nivel abaixo.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/02/2009 Criado
    ''' </history>
    Private Sub btnAbaixo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click
        Try

            Dim valor As String
            Dim valores() As String
            Dim ListaValores As New List(Of String)

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            valor = hidOrden.Value

            valores = valor.Split("|")

            ListaValores = VerificaValorRepetido(valores)

            Dim objTerminosIacSuperior As ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim objTerminosIacInferior As ContractoServicio.Iac.GetIacDetail.TerminosIac
            Dim contador As Integer = TerminosIacCompletos.Count

            If valor <> String.Empty Then

                For Each item As String In OrderacaoDesc(ListaValores)
                    objTerminosIacSuperior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac
                    objTerminosIacInferior = New IAC.ContractoServicio.Iac.GetIacDetail.TerminosIac

                    If item <> String.Empty AndAlso item <> Nothing Then

                        If valores.Count <> TerminosIacCompletos.Count Then

                            If item <> TerminosIacCompletos.Count Then

                                If item <> contador Then

                                    Dim NumeroOrden As Integer
                                    NumeroOrden = 0
                                    NumeroOrden = CType(item, Integer)

                                    objTerminosIacSuperior = SelectInformacoesOrdenTermino(TerminosIacCompletos, NumeroOrden)
                                    objTerminosIacSuperior.OrdenTermino = NumeroOrden + 1

                                    objTerminosIacInferior = SelectInformacoesOrdenTermino(TerminosIacCompletos, objTerminosIacSuperior.OrdenTermino)
                                    objTerminosIacInferior.OrdenTermino = NumeroOrden


                                    SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacSuperior.CodigoTermino)
                                    SelectDeleteTerminosIac(TerminosIacCompletos, objTerminosIacInferior.CodigoTermino)

                                    TerminosIacCompletos.Add(objTerminosIacInferior)
                                    TerminosIacCompletos.Add(objTerminosIacSuperior)
                                End If
                            End If
                        End If
                    End If

                    contador = contador - 1
                Next

            End If

            Salvar = True
            CarregaGridTerminosIac()
            valor = Nothing
            valores = Nothing
            ListaValores = Nothing
            hidOrden.Value = Nothing
            hidTerminoIacSelecionado.Value = Nothing
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
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
            txtCodigoIacForm.Enabled = True
            Acao = Utilidad.eAcao.Inicial
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub

    Public Sub CarregaDados(codigo As String)

        Dim objColIac As IAC.ContractoServicio.Iac.GetIacDetail.IacColeccion
        If codigo <> String.Empty AndAlso codigo <> Nothing Then

            objColIac = getIacDetail(codigo)
            If objColIac IsNot Nothing AndAlso objColIac.Count > 0 Then

                'Preenche os controles do formulario
                txtCodigoIacForm.Text = objColIac(0).CodidoIac
                txtCodigoIacForm.ToolTip = objColIac(0).CodidoIac

                txtDescricaoIacForm.Text = objColIac(0).DescripcionIac
                txtDescricaoIacForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColIac(0).DescripcionIac, String.Empty)

                txtObservacionesIac.Text = objColIac(0).ObservacionesIac
                chkVigenteForm.Checked = objColIac(0).vigente
                chkInvisible.Checked = objColIac(0).EsInvisible
                chkCopiarDeclarados.Checked = objColIac(0).EsDeclaradoCopia
                chkDisponivelNuevoSaldos.Checked = objColIac(0).EspecificoSaldos

                ValorCodigo = objColIac(0).CodidoIac
                ValorDescricao = objColIac(0).DescripcionIac
                ValorObservaciones = objColIac(0).ObservacionesIac
                ValorVigente = objColIac(0).vigente
                ValorInvisible = objColIac(0).EsInvisible

                ' preenche a propriedade da tela
                EsVigente = objColIac(0).vigente

                'Se for modificação então guarda a descriçaõ atual para validação
                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    DescricaoAtual = txtDescricaoIacForm.Text
                End If
            End If
        End If

    End Sub
    Private Sub ExecutarGrabar()
        Try

            Dim objProxyIac As New Comunicacion.ProxyIac
            Dim objPeticionIac As New IAC.ContractoServicio.Iac.SetIac.Peticion
            Dim objRespuestaIac As IAC.ContractoServicio.Iac.SetIac.Respuesta
            Dim strErro As String = String.Empty

            ValidarCamposObrigatorios = True

            strErro = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If


            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionIac.vigente = True
            Else
                objPeticionIac.vigente = chkVigenteForm.Checked
            End If

            EsVigente = chkVigenteForm.Checked

            objPeticionIac.CodidoIac = txtCodigoIacForm.Text
            objPeticionIac.DescripcionIac = txtDescricaoIacForm.Text
            objPeticionIac.ObservacionesIac = txtObservacionesIac.Text
            objPeticionIac.CodUsuario = MyBase.LoginUsuario
            objPeticionIac.EsDeclaradoCopia = chkCopiarDeclarados.Checked
            objPeticionIac.EsInvisible = chkInvisible.Checked
            objPeticionIac.EspecificoSaldos = chkDisponivelNuevoSaldos.Checked

            VerificaCheckBoxObligatorio()

            VerificaCheckBoxBusqueda()

            VerificaCheckBoxCampoClave()

            VerificaCheckBoxCopiarTermino()

            VerificaCheckBoxProtegido()

            VerificaCheckBoxInvisibleReporte()

            VerificaCheckBoxIdMecanizado()

            Dim objColTerminosIac As New ContractoServicio.Iac.SetIac.TerminosIacColeccion
            Dim objTerminos As ContractoServicio.Iac.SetIac.TerminosIac

            For Each terminosIac As ContractoServicio.Iac.GetIacDetail.TerminosIac In TerminosIacCompletos
                objTerminos = New ContractoServicio.Iac.SetIac.TerminosIac

                objTerminos.CodigoTermino = terminosIac.CodigoTermino
                objTerminos.DescripcionTermino = terminosIac.DescripcionTermino
                objTerminos.EsBusquedaParcial = terminosIac.EsBusquedaParcial
                objTerminos.EsObligatorio = terminosIac.EsObligatorio
                objTerminos.EsCampoClave = terminosIac.EsCampoClave
                objTerminos.OrdenTermino = terminosIac.OrdenTermino
                objTerminos.EsTerminoCopia = terminosIac.EsTerminoCopia
                objTerminos.EsProtegido = terminosIac.esProtegido
                objTerminos.esInvisibleRpte = terminosIac.esInvisibleRpte
                objTerminos.esIdMecanizado = terminosIac.esIdMecanizado

                objColTerminosIac.Add(objTerminos)

            Next

            If TerminosIacCompletos.Count > 0 Then
                objPeticionIac.TerminosIac = objColTerminosIac
            End If

            objRespuestaIac = objProxyIac.SetIac(objPeticionIac)

            'Define a ação de busca somente se houve retorno de canais

            If Master.ControleErro.VerificaErro(objRespuestaIac.CodigoError, objRespuestaIac.NombreServidorBD, objRespuestaIac.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaIac.CodigoError, objRespuestaIac.NombreServidorBD, objRespuestaIac.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("001_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaIac.MensajeError)
                    Exit Sub
                End If

            Else

                If objRespuestaIac.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaIac.MensajeError)
                Else
                    MyBase.MostraMensagem(objRespuestaIac.MensajeError)
                End If
                Exit Sub

            End If

            ValorCodigo = txtCodigoIacForm.Text
            ValorDescricao = txtDescricaoIacForm.Text
            ValorVigente = chkVigenteForm.Checked
            ValorInvisible = chkInvisible.Checked
            ValorObservaciones = txtObservacionesIac.Text
            Salvar = False

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

                'Verifica se o código do canal é obrigatório
                If txtCodigoIacForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoIacForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True

                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoIacForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoIacForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True

                End If

                'Verifica se existem términos selecionados.
                If ProsegurGridView2.Rows.Count <= 0 Then
                    strErro.Append(csvTerminoObligatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTerminoObligatorio.IsValid = False
                Else
                    csvTerminoObligatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoIac(txtCodigoIacForm.Text.Trim) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoIacForm.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If ExisteDescricaoIac(txtDescricaoIacForm.Text.Trim) Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoIacForm.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) Then
                    codigo = grdIac.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                Acao = Utilidad.eAcao.Modificacion
                LimparCampos()

                CabecalhoVazioTerminos()
                CabecalhoVazioTerminosIac()
                CarregaDados(Server.UrlDecode(codigo).Replace("&#250;", "ú"))
                CarregaGridTerminosIac()
                getTerminos()
                CarregaGrid()

                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoIacForm.Enabled = False

                If chkVigenteForm.Checked AndAlso EsVigente Then
                    chkVigenteForm.Enabled = False
                Else
                    chkVigenteForm.Enabled = True
                End If

                SetFocus(txtDescricaoIacForm)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) Then
                    codigo = grdIac.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                LimparCampos()
                Acao = Utilidad.eAcao.Consulta

                CabecalhoVazioTerminos()
                CabecalhoVazioTerminosIac()
                CarregaDados(Server.UrlDecode(codigo).Replace("&#250;", "ú"))
                CarregaGridTerminosIac()
                getTerminos()
                CarregaGrid()

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
            If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(grdIac.getValorLinhaSelecionada) Then
                    codigo = grdIac.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                LimparCampos()
                Acao = Utilidad.eAcao.Baja

                CabecalhoVazioTerminos()
                CabecalhoVazioTerminosIac()
                CarregaDados(Server.UrlDecode(codigo).Replace("&#250;", "ú"))
                CarregaGridTerminosIac()
                getTerminos()
                CarregaGrid()

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

End Class