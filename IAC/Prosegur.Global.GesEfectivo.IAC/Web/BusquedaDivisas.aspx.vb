Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Busca de Divisas 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 01/02/09 - Criado </history>
Partial Public Class BusquedaDivisas
    Inherits Base

    Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando precionado enter.
        txtCodigoIso.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoDivisa.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")


    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoIso.TabIndex = 1
        txtDescricaoDivisa.TabIndex = 2
        chkVigente.TabIndex = 3
        btnBuscar.TabIndex = 4
        btnLimpar.TabIndex = 5
        btnBaja.TabIndex = 14

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DIVISAS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Divisa")
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            TrataFoco()
            If Not IsPostBack Then

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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()

            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            If pnForm.Visible Then
                btnNovo.Text = Traduzir("btnInserirDenominacion")
            Else
                btnNovo.Text = Traduzir("btnAlta")
            End If

            Dim strBuider As New StringBuilder
            ' Script para mudar a cor de fundo do controle
            strBuider.Append("function colorChanged(sender)")
            strBuider.Append("{")
            strBuider.Append("  sender.get_element().style.backgroundColor = '#' + sender.get_selectedColor();")
            strBuider.Append("}")

            'Adiciona o script na página
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ColorChanged", strBuider.ToString, True)

            txtColor.Attributes.Add("onKeyDown", "return false;")

            If Not String.IsNullOrWhiteSpace(txtColor.Text) Then
                txtColor.BackColor = Drawing.ColorTranslator.FromHtml(String.Format("#{0}", txtColor.Text))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("005_titulo_busqueda_divisas")
        lblCodigoIso.Text = Traduzir("005_lbl_codigoiso")
        lblDescricaoDivisa.Text = Traduzir("005_lbl_descricaodivisa")
        lblVigente.Text = Traduzir("005_lbl_vigente")
        lblSubTitulosDivisas.Text = Traduzir("005_lbl_subtitulosdivisas")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("005_lbl_subtituloscriteriosbusqueda")
        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("005_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("005_lbl_grd_descripcion")
        GdvResultado.Columns(3).HeaderText = Traduzir("005_lbl_grd_simbolo")
        GdvResultado.Columns(4).HeaderText = Traduzir("005_lbl_grd_vigente")

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

        btnAltaAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAltaAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        'Formulário

        lblCodigoDivisa.Text = Traduzir("005_lbl_codigoiso")
        lblDescricaoDivisaForm.Text = Traduzir("005_lbl_descricaoDivisa")
        lblVigenteForm.Text = Traduzir("005_lbl_vigente")
        lblTituloDivisas.Text = Traduzir("005_titulo_mantenimiento_divisas")
        lblSubTitulosDivisasForm.Text = Traduzir("005_lbl_subtitulosDenominaciones")
        lblSimbolo.Text = Traduzir("005_lbl_simbolo")
        lblColor.Text = Traduzir("005_lbl_cor")
        lblCodigoAcesso.Text = Traduzir("005_lbl_Codigoacceso")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")


        csvCodigoObrigatorio.ErrorMessage = Traduzir("005_msg_divisacodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("005_msg_divisadescripcionobligatorio")
        csvCodigoColorObrigatorio.ErrorMessage = Traduzir("005_msg_codigocolorobligatorio")
        csvCodigoDivisaExistente.ErrorMessage = Traduzir("005_msg_codigodivisaexistente")
        csvDescricaoDivisaExistente.ErrorMessage = Traduzir("005_msg_descripciondivisaexistente")
        csvCodigoAcessoExiste.ErrorMessage = Traduzir("005_msg_codigoaccesoexistente")


        'GridFormulario
        GdvDenominaciones.PagerSummary = Traduzir("grid_lbl_pagersummary")
        GdvDenominaciones.Columns(1).HeaderText = Traduzir("005_lbl_grd_mantenimiento_codigo")
        GdvDenominaciones.Columns(2).HeaderText = Traduzir("005_lbl_grd_mantenimiento_descripcion")
        GdvDenominaciones.Columns(3).HeaderText = Traduzir("005_lbl_grd_mantenimiento_valor")
        GdvDenominaciones.Columns(4).HeaderText = Traduzir("005_lbl_grd_mantenimiento_peso")
        GdvDenominaciones.Columns(5).HeaderText = Traduzir("005_lbl_grd_mantenimiento_bilhete")
        GdvDenominaciones.Columns(6).HeaderText = Traduzir("005_lbl_grd_mantenimiento_vigente")

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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Property FiltroDescripcion() As List(Of String)
        Get
            Return ViewState("FiltroDescripcion")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroDescripcion") = value
        End Set
    End Property

    Property FiltroCodigo() As List(Of String)
        Get
            Return ViewState("FiltroCodigo")
        End Get
        Set(value As List(Of String))
            ViewState("FiltroCodigo") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Metodo busca as divisa de acordo com os parametros passados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Public Function getDivisas() As IAC.ContractoServicio.Divisa.GetDivisas.Respuesta

        Dim objProxyDivisa As New Comunicacion.ProxyDivisa
        Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.GetDivisas.Peticion
        Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.GetDivisas.Respuesta

        'Recebe os valores do filtro
        objPeticionDivisa.Vigente = FiltroVigente
        objPeticionDivisa.CodigoIso = FiltroCodigo
        objPeticionDivisa.Descripcion = FiltroDescripcion

        objRespuestaDivisa = objProxyDivisa.getDivisas(objPeticionDivisa)

        Return objRespuestaDivisa


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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Public Sub PreencherDivisas()
        Dim objRespostaDivisa As IAC.ContractoServicio.Divisa.GetDivisas.Respuesta

        'Busca as divisas
        objRespostaDivisa = getDivisas()

        If Not Master.ControleErro.VerificaErro(objRespostaDivisa.CodigoError, objRespostaDivisa.NombreServidorBD, objRespostaDivisa.MensajeError) Then
            MyBase.MostraMensagem(objRespostaDivisa.MensajeError)
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de divisas
        If objRespostaDivisa.Divisas.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaDivisa.Divisas.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespostaDivisa.Divisas)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodigoIso ASC "
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodigoIso ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If
                Else
                    objDt = GdvResultado.ConvertListToDataTable(objRespostaDivisa.Divisas)
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                    'ProsegurGridView1.CarregaControle(objDt)
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
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvResultado.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função faz a ordenção do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados


        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getDivisas().Divisas)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " CodigoIso ASC "
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
    ''' [pda] 01/02/2009 Criado
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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then


            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Vigente


            Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
            CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
            CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
            CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")

            Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha


            If CBool(e.Row.DataItem("vigente")) Then
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
            Else
                CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
            End If

        End If
    End Sub

    ''' <summary>
    ''' Traduz o cabeçalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()
            LimparCamposForm()

            pnForm.Visible = False

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
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
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

        Dim strListCodigo As New List(Of String)
        Dim strListDescricao As New List(Of String)

        strListCodigo.Add(txtCodigoIso.Text.Trim.ToUpper)
        strListDescricao.Add(txtDescricaoDivisa.Text.Trim.ToUpper)

        'Filtros
        FiltroCodigo = strListCodigo
        FiltroDescripcion = strListDescricao
        FiltroVigente = chkVigente.Checked

        'Retorna os canais de acordo com o filtro aciam
        PreencherDivisas()

      

    End Sub

    ''' <summary>
    ''' Evento click do botão baja.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
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

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxyDivisa As New Comunicacion.ProxyDivisa
                Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion
                Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta

                'Retorna o valor da linha selecionada no grid

                'Criando uma divisa para exclusão
                Dim objDivisa As New IAC.ContractoServicio.Divisa.Divisa
                objDivisa.CodigoIso = Server.UrlDecode(strCodigo)
                objDivisa.Vigente = False

                'Incluindo na coleção
                Dim objDivisaCol As New IAC.ContractoServicio.Divisa.DivisaColeccion
                objDivisaCol.Add(objDivisa)

                'Associa a coleção de divisas criado para a petição
                objPeticionDivisa.Divisas = objDivisaCol
                objPeticionDivisa.CodigoUsuario = MyBase.LoginUsuario

                'Exclui a petição
                objRespuestaDivisa = objProxyDivisa.setDivisaDenominaciones(objPeticionDivisa)

                If Master.ControleErro.VerificaErro(objRespuestaDivisa.CodigoError, objRespuestaDivisa.NombreServidorBD, objRespuestaDivisa.MensajeError) Then

                    If objRespuestaDivisa.RespuestaDivisas IsNot Nothing AndAlso
                        objRespuestaDivisa.RespuestaDivisas.Count > 0 Then

                        ' se for um erro de negocio
                        If objRespuestaDivisa.RespuestaDivisas(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                            ' mostrar o erro pro usuário
                            MyBase.MostraMensagem(objRespuestaDivisa.RespuestaDivisas(0).MensajeError)
                            Exit Sub

                        End If

                    End If

                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaDivisa.MensajeError)
                End If
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

    ''' <summary>
    ''' Configuração de estado da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 01/02/2009 Criado
    ''' </history>
    Public Sub ControleBotoes()

        ParametroMantenimientoClientesDivisasPorPantalla = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

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
                    '    btnBaja.Visible = False
                    'txtCodigoIso.Focus()
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial
                    '   btnBaja.Visible = False

                    chkVigente.Checked = True
                    pnlSemRegistro.Visible = False
                    pnlSemRegistroForm.Visible = False
                    txtCodigoIso.Text = String.Empty
                    txtDescricaoDivisa.Text = String.Empty
                    txtCodigoIso.Focus()


                Case Aplicacao.Util.Utilidad.eAcao.Busca


                    '  btnBaja.Visible = True

                    'txtCodigoIso.Focus()

            End Select
        Else
            Select Case MyBase.Acao
                Case Aplicacao.Util.Utilidad.eAcao.NoAction
                    '   btnBaja.Visible = False

                    'txtCodigoIso.Focus()
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial
                    '   btnBaja.Visible = False

                    chkVigente.Checked = True
                    pnlSemRegistro.Visible = False
                    pnlSemRegistroForm.Visible = False
                    txtCodigoIso.Text = String.Empty
                    txtDescricaoDivisa.Text = String.Empty
                    txtCodigoIso.Focus()

                Case Aplicacao.Util.Utilidad.eAcao.Busca


                    '   btnBaja.Visible = False

                    'txtCodigoIso.Focus()

            End Select
        End If

        'DesabilitarCampos(ParametroMantenimientoClientesDivisasPorPantalla)
    End Sub

#End Region

#Region "[PROPRIEDADES FORMULARIO]"
    Private Property OidDivisa() As String
        Get
            Return ViewState("OidDivisa")
        End Get
        Set(value As String)
            ViewState("OidDivisa") = value
        End Set
    End Property
    Public ReadOnly Property DenominacionesTemporario() As IAC.ContractoServicio.Divisa.DenominacionColeccion
        Get
            If ViewState("DenominacionesTemporario") Is Nothing Then
                ViewState("DenominacionesTemporario") = New IAC.ContractoServicio.Divisa.DenominacionColeccion
            End If

            Return DirectCast(ViewState("DenominacionesTemporario"), IAC.ContractoServicio.Divisa.DenominacionColeccion)
        End Get
    End Property

    Private Property Divisas As ContractoServicio.Divisa.DivisaColeccion
        Get
            Return DirectCast(ViewState("Divisas"), ContractoServicio.Divisa.DivisaColeccion)
        End Get
        Set(value As ContractoServicio.Divisa.DivisaColeccion)
            ViewState("Divisas") = value
        End Set
    End Property

    Public Property DenominacionesClone() As IAC.ContractoServicio.Divisa.DenominacionColeccion
        Get
            If ViewState("DenominacionesClone") Is Nothing Then
                ViewState("DenominacionesClone") = New IAC.ContractoServicio.Divisa.DenominacionColeccion
            End If

            Return DirectCast(ViewState("DenominacionesClone"), IAC.ContractoServicio.Divisa.DenominacionColeccion)
        End Get
        Set(value As IAC.ContractoServicio.Divisa.DenominacionColeccion)
            ViewState("DenominacionesClone") = value
            ViewState("DenominacionesTemporario") = value
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

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property CodigoAccesoExistente() As Boolean
        Get
            Return ViewState("CodigoAccesoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoAccesoExistente") = value
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
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Private Property CodigoAccesoAtual() As String
        Get
            Return ViewState("CodigoAccesoAtual")
        End Get
        Set(value As String)
            ViewState("CodigoAccesoAtual") = value.Trim
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

    Private Property CodigosAjenosPeticion As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property
#End Region
#Region "[EVENTOS GRIDVIEW FORMULARIO]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 05/03/2009 Criado
    ''' </history>
    Private Sub GdvDenominaciones_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvDenominaciones.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("Vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GdvDenominaciones_EPreencheDados(sender As Object, e As EventArgs) Handles GdvDenominaciones.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = Prosegur.Genesis.Comon.CustomProsegurGridView.ConvertListToDataTable(DenominacionesTemporario)

            objDT.DefaultView.Sort = GdvDenominaciones.SortCommand

            GdvDenominaciones.ControleDataSource = objDT

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GdvDenominaciones_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvDenominaciones.EPager_SetCss

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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 16

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try



    End Sub

    ''' <summary>
    ''' RowDataBound do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GdvDenominaciones_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.RowDataBound

        Try
            'Índice das celulas do GridView Configuradas
            '0 - Código
            '1 - Descripción
            '2 - Valor
            '3 - Peso
            '4 - Bilhete
            '5 - Vigente

            If e.Row.DataItem IsNot Nothing Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigoForm.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar1"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar1"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir1"), ImageButton).OnClientClick = jsScript & "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBajaDenominacion.ClientID & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar1"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar1"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir1"), ImageButton).ToolTip = Traduzir("btnBaja")

                If Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then
                    CType(e.Row.Cells(0).FindControl("imgEditar1"), ImageButton).Enabled = False
                    CType(e.Row.Cells(0).FindControl("imgExcluir1"), ImageButton).Enabled = False
                Else
                    CType(e.Row.Cells(0).FindControl("imgEditar1"), ImageButton).Enabled = True
                    CType(e.Row.Cells(0).FindControl("imgExcluir1"), ImageButton).Enabled = True
                End If

                If Not e.Row.DataItem("descripcion") Is DBNull.Value AndAlso e.Row.DataItem("descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("EsBillete")) Then
                    CType(e.Row.Cells(5).FindControl("imgBillete"), Image).ImageUrl = "~/Imagenes/money.gif"
                Else
                    CType(e.Row.Cells(5).FindControl("imgBillete"), Image).ImageUrl = "~/Imagenes/coins.gif"
                End If

                If CBool(e.Row.DataItem("Vigente")) Then
                    CType(e.Row.Cells(6).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(6).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCreated do GridView
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GdvDenominaciones_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvDenominaciones.RowCreated

        Try
            If e.Row.RowType = DataControlRowType.Header Then
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[PERSISTENCIA DADOS FORMULARIO]"
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion
            Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Respuesta

            ValidarCamposObrigatorios = True
            Dim strErro As String = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objDivisa As New IAC.ContractoServicio.Divisa.Divisa
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                objDivisa.Vigente = True
            Else
                objDivisa.Vigente = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objDivisa.CodigoIso = txtCodigoDivisa.Text.Trim
            objDivisa.Descripcion = txtDescricaoDivisaForm.Text.Trim
            objDivisa.CodigoSimbolo = txtSimbolo.Text.Trim
            objDivisa.ColorDivisa = txtColor.Text.Trim
            objDivisa.CodigoAccesoDivisa = txtCodigoAcesso.Text.Trim

            If Acao = Utilidad.eAcao.Alta Then
                objDivisa.Denominaciones = RetornaDenominacionesEnvio(DenominacionesTemporario, DenominacionesClone)
            Else
                objDivisa.Denominaciones = DenominacionesTemporario
            End If


            'Cria a coleção
            Dim objColDivisa As New IAC.ContractoServicio.Divisa.DivisaColeccion
            objColDivisa.Add(objDivisa)

            objPeticionDivisa.Divisas = objColDivisa
            objPeticionDivisa.CodigoUsuario = MyBase.LoginUsuario

            objPeticionDivisa.CodigoAjeno = CodigosAjenosPeticion
            objRespuestaDivisa = objProxyDivisa.setDivisaDenominaciones(objPeticionDivisa)

            'Define a ação de busca somente se houve retorno de canais

            If Master.ControleErro.VerificaErro(objRespuestaDivisa.CodigoError, objRespuestaDivisa.NombreServidorBD, objRespuestaDivisa.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaDivisa.RespuestaDivisas(0).CodigoError, objRespuestaDivisa.NombreServidorBD, objRespuestaDivisa.RespuestaDivisas(0).MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()

                    btnCancelar_Click(Nothing, Nothing)

                Else
                    MyBase.MostraMensagem(objRespuestaDivisa.RespuestaDivisas(0).MensajeError)
                End If


            Else

                If objRespuestaDivisa.RespuestaDivisas IsNot Nothing _
                    AndAlso objRespuestaDivisa.RespuestaDivisas.Count > 0 _
                    AndAlso objRespuestaDivisa.RespuestaDivisas(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then

                    MyBase.MostraMensagem(objRespuestaDivisa.RespuestaDivisas(0).MensajeError)

                ElseIf objRespuestaDivisa.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then

                    MyBase.MostraMensagem(objRespuestaDivisa.RespuestaDivisas(0).MensajeError)

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Function RetornaDenominacionesEnvio(objDenominacionesTemporario As IAC.ContractoServicio.Divisa.DenominacionColeccion, objDenominacionesClone As IAC.ContractoServicio.Divisa.DenominacionColeccion) As IAC.ContractoServicio.Divisa.DenominacionColeccion

        ' Retorna o que tem a mais no temporario em relação ao clone e o que é foi modificado no temporario em relação ao clone
        Dim retorno = (From c As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesTemporario Join d As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesClone On c.Codigo Equals d.Codigo _
                            Where (c.Descripcion <> d.Descripcion OrElse c.Valor <> d.Valor OrElse c.Peso <> d.Peso OrElse c.EsBillete <> d.EsBillete OrElse c.Vigente <> d.Vigente OrElse c.CodigoAccesoDenominacion <> d.CodigoAccesoDenominacion) _
                            Select c.Codigo, c.Descripcion, c.Valor, c.Peso, c.EsBillete, c.Vigente, c.CodigoAccesoDenominacion, c.CodigosAjenos) _
                            .Union(From x As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesTemporario Where Not (From o As IAC.ContractoServicio.Divisa.Denominacion In objDenominacionesClone Select o.Codigo).Contains(x.Codigo) _
                            Select x.Codigo, x.Descripcion, x.Valor, x.Peso, x.EsBillete, x.Vigente, x.CodigoAccesoDenominacion, x.CodigosAjenos)


        Dim objDenominacionesCol As New IAC.ContractoServicio.Divisa.DenominacionColeccion

        Dim objDenominacionEnvio As IAC.ContractoServicio.Divisa.Denominacion
        For Each objRetorno As Object In retorno
            objDenominacionEnvio = New IAC.ContractoServicio.Divisa.Denominacion
            objDenominacionEnvio.Codigo = objRetorno.Codigo
            objDenominacionEnvio.Descripcion = objRetorno.Descripcion
            objDenominacionEnvio.Valor = objRetorno.Valor
            objDenominacionEnvio.Peso = objRetorno.Peso
            objDenominacionEnvio.EsBillete = objRetorno.EsBillete
            objDenominacionEnvio.Vigente = objRetorno.Vigente
            objDenominacionEnvio.CodigoAccesoDenominacion = objRetorno.CodigoAccesoDenominacion
            objDenominacionEnvio.CodigosAjenos = objRetorno.CodigosAjenos
            'Adiciona na coleção
            objDenominacionesCol.Add(objDenominacionEnvio)
        Next

        Return objDenominacionesCol

    End Function
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoDivisa.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoDivisa.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoDivisaForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoDivisaForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtColor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoColorObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoColorObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtColor.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoColorObrigatorio.IsValid = True
                End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoDivisaExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoDivisaExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoDivisa.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoDivisaExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoDivisaExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoDivisaExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoDivisa.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoDivisaExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If CodigoAccesoExistente Then

                strErro.Append(csvCodigoAcessoExiste.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoAcessoExiste.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoAcesso.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoAcessoExiste.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    Public Sub ConsomeDenominacion()

        If Session("objDenominacion") IsNot Nothing Then

            Dim objDenominacion As IAC.ContractoServicio.Divisa.Denominacion
            objDenominacion = DirectCast(Session("objDenominacion"), IAC.ContractoServicio.Divisa.Denominacion)

            'Se existe o denominação na coleção
            If Not VerificarDenominacionExiste(DenominacionesTemporario, objDenominacion.Codigo) Then
                DenominacionesTemporario.Add(objDenominacion)
            Else
                ModificaDenominacion(DenominacionesTemporario, objDenominacion)
            End If


            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                'Seta o estado da página corrente para modificação
                Acao = eAcaoEspecifica.AltaConRegistro
            End If


            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = Prosegur.Genesis.Comon.CustomProsegurGridView.ConvertListToDataTable(DenominacionesTemporario)
            GdvDenominaciones.CarregaControle(objDT)


            Session("objDenominacion") = Nothing
        End If

    End Sub

    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TDIVISA") IsNot Nothing Then
            Dim objDivisaPeticion As New IAC.ContractoServicio.Divisa.SetDivisasDenominaciones.Peticion
            objDivisaPeticion.CodigoAjeno = Session("objRespuestaGEPR_TDIVISA")

            Session.Remove("objRespuestaGEPR_TDIVISA")

            Dim iCodigoAjeno = (From item In objDivisaPeticion.CodigoAjeno
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If objDivisaPeticion.CodigoAjeno IsNot Nothing Then
                CodigosAjenosPeticion = objDivisaPeticion.CodigoAjeno
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If


            Session("objRespuestaGEPR_TDIVISA") = objDivisaPeticion.CodigoAjeno

        End If

    End Sub

    Private Function VerificarDenominacionExiste(objDenominaciones As IAC.ContractoServicio.Divisa.DenominacionColeccion, codigoDenominacion As String) As Boolean

        Dim retorno = From c In objDenominaciones Where c.Codigo = codigoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Private Function ModificaDenominacion(ByRef objDenominacoes As IAC.ContractoServicio.Divisa.DenominacionColeccion, objDenominacion As IAC.ContractoServicio.Divisa.Denominacion) As Boolean

        Dim retorno = From c In objDenominacoes Where c.Codigo = objDenominacion.Codigo

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else

            Dim objDenominacionTmp As IAC.ContractoServicio.Divisa.Denominacion = DirectCast(retorno.ElementAt(0), IAC.ContractoServicio.Divisa.Denominacion)

            objDenominacionTmp.Descripcion = objDenominacion.Descripcion
            objDenominacionTmp.Peso = objDenominacion.Peso
            objDenominacionTmp.Valor = objDenominacion.Valor
            objDenominacionTmp.Vigente = objDenominacion.Vigente
            objDenominacionTmp.CodigoAccesoDenominacion = objDenominacion.CodigoAccesoDenominacion
            objDenominacionTmp.EsBillete = objDenominacion.EsBillete
            objDenominacionTmp.CodigosAjenos = objDenominacion.CodigosAjenos
            Return True
        End If
    End Function
    Public Sub CarregaDadosForm(codDivisa As String)

        Dim objColDivisa As IAC.ContractoServicio.Divisa.DivisaColeccion
        objColDivisa = getDenominacionByDivisa(codDivisa)

        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty

        Session("objRespuestaGEPR_TDIVISA") = Nothing

        If objColDivisa.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoDivisa.Text = objColDivisa(0).CodigoIso
            txtCodigoDivisa.ToolTip = objColDivisa(0).CodigoIso
            txtDescricaoDivisaForm.Text = objColDivisa(0).Descripcion
            txtDescricaoDivisaForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColDivisa(0).Descripcion, String.Empty)
            txtSimbolo.Text = objColDivisa(0).CodigoSimbolo
            txtSimbolo.ToolTip = objColDivisa(0).CodigoSimbolo
            txtColor.Text = objColDivisa.First.ColorDivisa
            txtCodigoAcesso.Text = objColDivisa.First.CodigoAccesoDivisa
            ' AJENO
            Dim iCodigoAjeno = (From item In objColDivisa(0).CodigosAjenos
                  Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            Session("objRespuestaGEPR_TDIVISA") = objColDivisa(0).CodigosAjenos

            If Not String.IsNullOrEmpty(objColDivisa.First.ColorDivisa) Then
                txtColor.BackColor = Drawing.ColorTranslator.FromHtml(String.Format("#{0}", objColDivisa.First.ColorDivisa))
            End If

            chkVigenteForm.Checked = objColDivisa(0).Vigente

            ' preenche a propriedade da tela
            EsVigente = objColDivisa(0).Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = txtDescricaoDivisaForm.Text
                CodigoAccesoAtual = txtCodigoAcesso.Text
            End If

            'Faz um clone da coleção de Divisa
            DenominacionesClone = objColDivisa(0).Denominaciones

            'Carrega os SubCanais no GridView
            Dim objDT As DataTable
            objDT = Prosegur.Genesis.Comon.CustomProsegurGridView.ConvertListToDataTable(objColDivisa(0).Denominaciones)
            GdvDenominaciones.CarregaControle(objDT)

        End If

    End Sub
    Public Function getDenominacionByDivisa(codigoDivisa As String) As IAC.ContractoServicio.Divisa.DivisaColeccion

        Dim objProxyDivisa As New Comunicacion.ProxyDivisa
        Dim objPeticionDivisa As New IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion
        Dim objRespuestaDivisa As IAC.ContractoServicio.Divisa.GetDenominacionesByDivisa.Respuesta

        'Recebe os valores do filtro
        Dim lista As New List(Of String)
        lista.Add(codigoDivisa)

        objPeticionDivisa.CodigoIso = lista

        objRespuestaDivisa = objProxyDivisa.getDenominacionesByDivisa(objPeticionDivisa)

        Return objRespuestaDivisa.Divisas


    End Function

#End Region
#Region "[EVENTOS DENOMINACOES POPUP]"
    Private Sub SetDenominacionesTemporarioCollectionPopUp()

        'Envia a coleção denominação para ser consumido pela PopUp de Mantenimento de Denominações
        Session("colDenominacionesTemporario") = DenominacionesTemporario

    End Sub
    Public Sub ExecutarAlta()
        Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&CodigoDivisa =" & txtCodigoDivisa.Text.Trim

        'Passa a coleção com o objeto temporario de denominacoes
        SetDenominacionesTemporarioCollectionPopUp()

        Master.ExibirModal(url, Traduzir("005_titulo_mantenimiento_denominaciones"), 400, 800, False, True, btnConsomeDenominaciones.ClientID)

    End Sub
    Public Sub ExecutarConsulta()
        Try
            Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&CodigoDivisa =" & txtCodigoDivisa.Text.Trim

            'Seta a session com o Denominacion que será consmida na abertura da PopUp de Mantenimiento de Denominaciones
            SetDenominacionSelecionadoPopUp()

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("005_titulo_mantenimiento_denominaciones"), 400, 800, False, True, btnConsomeDenominaciones.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Sub ExecutarModificacion()
        Try
            Dim url As String = "MantenimientoDenominaciones.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Modificacion & "&CodigoDivisa=" & txtCodigoDivisa.Text.Trim

            'Seta a session com o Denominação que será consmida na abertura da PopUp de Mantenimiento de Denominacion
            SetDenominacionSelecionadoPopUp()

            'Passa a coleção com o objeto temporario de denominações
            SetDenominacionesTemporarioCollectionPopUp()

            'AbrirPopupModal
            Master.ExibirModal(url, Traduzir("005_titulo_mantenimiento_denominaciones"), 400, 800, False, True, btnConsomeDenominaciones.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Public Sub ExecutarBaja()
        Try
            If Not String.IsNullOrEmpty(GdvDenominaciones.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then

                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvDenominaciones.getValorLinhaSelecionada) Then
                    codigo = GdvDenominaciones.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigoForm.Value.ToString()
                End If

                'Criando uma denominação para exclusão
                Dim objDenominacion As IAC.ContractoServicio.Divisa.Denominacion = RetornaDenominacionExiste(DenominacionesTemporario, Server.UrlDecode(codigo))
                objDenominacion.Vigente = False

                'Carrega os SubCanais no GridView
                Dim objDT As DataTable
                objDT = Prosegur.Genesis.Comon.CustomProsegurGridView.ConvertListToDataTable(DenominacionesTemporario)
                GdvDenominaciones.CarregaControle(objDT)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub SetDenominacionSelecionadoPopUp()

        'Cria o Divisa para ser consumido na página de Denominaciones
        Dim objDenominacion As New IAC.ContractoServicio.Divisa.Denominacion
        If Not String.IsNullOrEmpty(GdvDenominaciones.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigoForm.Value) Then

            Dim codigo As String = String.Empty
            If Not String.IsNullOrEmpty(GdvDenominaciones.getValorLinhaSelecionada) Then
                codigo = GdvDenominaciones.getValorLinhaSelecionada
            Else
                codigo = hiddenCodigoForm.Value.ToString()
            End If
            objDenominacion = RetornaDenominacionExiste(DenominacionesTemporario, Server.UrlDecode(codigo))
            Session("setDenominacion") = objDenominacion

        End If

    End Sub
    Private Function RetornaDenominacionExiste(ByRef objDenominaciones As IAC.ContractoServicio.Divisa.DenominacionColeccion, codigoDenominacion As String) As IAC.ContractoServicio.Divisa.Denominacion

        Dim retorno = From c In objDenominaciones Where c.Codigo = codigoDenominacion

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return Nothing
        Else
            Return retorno.ElementAt(0)
        End If

    End Function
#End Region
#Region "[EVENTOS TEXTBOX FORMULARIO]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoDivisa_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoDivisa.TextChanged
        Try

            If ExisteCodigoDivisa(txtCodigoDivisa.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Verifica se o codigo acesso existe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoAcesso_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoAcesso.TextChanged

        Try

            If ExisteCodigoAccesoDivisa(txtCodigoAcesso.Text) Then
                CodigoAccesoExistente = True
            Else
                CodigoAccesoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento de mudança de texto do campo Descriçao Divisa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoDivisaForm_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoDivisaForm.TextChanged
        Try

            If ExisteDescricaoDivisa(txtDescricaoDivisaForm.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoDivisa(CodigoDivisa As String) As Boolean

        Dim objRespostaVerificarCodigoDivisa As IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

        Try

            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionVerificarCodigoDivisa As New IAC.ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarCodigoDivisa.CodigoIso = CodigoDivisa.Trim
            objRespostaVerificarCodigoDivisa = objProxyDivisa.VerificarCodigoDivisa(objPeticionVerificarCodigoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoDivisa.CodigoError, objRespostaVerificarCodigoDivisa.NombreServidorBD, objRespostaVerificarCodigoDivisa.MensajeError) Then
                Return objRespostaVerificarCodigoDivisa.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarCodigoDivisa.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoDivisa(descricao As String) As Boolean

        Dim objRespostaVerificarDescricaoDivisa As IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Respuesta
        Try


            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If txtDescricaoDivisa.Text.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If


            Dim objProxyDivisa As New Comunicacion.ProxyDivisa
            Dim objPeticionVerificarDescricaoDivisa As New IAC.ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarDescricaoDivisa.DescripcionDivisa = txtDescricaoDivisa.Text
            objRespostaVerificarDescricaoDivisa = objProxyDivisa.VerificarDescripcionDivisa(objPeticionVerificarDescricaoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoDivisa.CodigoError, objRespostaVerificarDescricaoDivisa.NombreServidorBD, objRespostaVerificarDescricaoDivisa.MensajeError) Then
                Return objRespostaVerificarDescricaoDivisa.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarDescricaoDivisa.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoAccesoDivisa(CodigoAcceso As String) As Boolean

        Dim objRespostaVerificarCodigoAccesoDivisa As IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Respuesta

        Try


            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If txtCodigoAcesso.Text.Trim.Equals(CodigoAccesoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If


            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
            Dim objPeticionVerificarCodigoAccesoDivisa As New IAC.ContractoServicio.Utilidad.VerificarCodigoAccesoDivisa.Peticion

            'Verifica se o código do Divisa existe no BD
            objPeticionVerificarCodigoAccesoDivisa.CodigoAcceso = txtCodigoAcesso.Text
            objRespostaVerificarCodigoAccesoDivisa = objProxyUtilidad.VerificarCodigoAccesoDivisa(objPeticionVerificarCodigoAccesoDivisa)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoAccesoDivisa.CodigoError, objRespostaVerificarCodigoAccesoDivisa.NombreServidorBD, objRespostaVerificarCodigoAccesoDivisa.MensajeError) Then
                Return objRespostaVerificarCodigoAccesoDivisa.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarCodigoAccesoDivisa.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Private Sub DesabilitarCampos(HabilitarTodosCampos As Boolean)

        If HabilitarTodosCampos Then
            txtDescricaoDivisaForm.Enabled = True
            txtCodigoDivisa.Enabled = True
            txtCodigoAcesso.Enabled = True
            txtColor.Enabled = True
            txtSimbolo.Enabled = True
            chkVigente.Enabled = True
        Else
            'Permite el mantenimiento solamente del color y del código de acceso de las divisas.
            'Permite el mantenimiento solamente del código de acceso y del peso de las denominaciones.
            'Permite consultar divisas y denominaciones.
            'Permite el mantenimiento de divisas a través del servicio Divisa.
            txtDescricaoDivisaForm.Enabled = False
            txtCodigoDivisa.Enabled = False
            txtCodigoAcesso.Enabled = True
            txtColor.Enabled = True
            txtSimbolo.Enabled = False
            chkVigente.Enabled = False
        End If


    End Sub
#End Region

#Region "[EVENTOS BOTOES FORMULARIO]"

    Private Sub LimparCamposForm()
        txtCodigoDivisa.Text = String.Empty
        txtDescricaoDivisaForm.Text = String.Empty
        txtSimbolo.Text = String.Empty
        chkVigenteForm.Checked = True
        txtColor.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDesCodigoAjeno.Text = String.Empty
        txtCodigoAcesso.Text = String.Empty
        GdvDenominaciones.DataSource = Nothing
        GdvDenominaciones.DataBind()
        BloquearDesbloquearCamposForm(True)
        ViewState("DenominacionesTemporario") = Nothing
        Divisas = Nothing
        Session("objRespuestaGEPR_TDIVISA") = Nothing

    End Sub
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            If pnForm.Visible Then
                ExecutarAlta()
                BloquearDesbloquearCamposForm(True)

            Else

                Acao = Utilidad.eAcao.Alta

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

                txtColor.BackColor = Drawing.ColorTranslator.FromHtml(String.Format("#{0}", "FFFFFF"))
                LimparCamposForm()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try

            Acao = Utilidad.eAcao.Inicial
            LimparCamposForm()
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
            txtCodigoDivisa.Enabled = True


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnConsomeDenominaciones_Click(sender As Object, e As EventArgs) Handles btnConsomeDenominaciones.Click
        Try
            ConsomeDenominacion()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar1_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            ExecutarConsulta()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditar1_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            ExecutarModificacion()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaDenominacion_Click(sender As Object, e As EventArgs) Handles btnBajaDenominacion.Click
        Try
            ExecutarBaja()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAltaAjeno_Click(sender As Object, e As EventArgs) Handles btnAltaAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoDivisa.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoDivisaForm.Text
            tablaGenesis.OidTablaGenesis = OidDivisa
            If Divisas IsNot Nothing AndAlso Divisas.Count > 0 AndAlso Divisas.FirstOrDefault IsNot Nothing AndAlso Divisas.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Divisas.FirstOrDefault.CodigosAjenos
            End If

            If Session("objRespuestaGEPR_TDIVISA") IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Session("objRespuestaGEPR_TDIVISA")
            End If

            Session("objPeticionGEPR_TDIVISA") = tablaGenesis.CodigosAjenos
            Session("objGEPR_TDIVISA") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) OrElse (Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TDIVISA"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TDIVISA"
            End If

            '   ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Ajeno", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjenoDenominacion');", True)
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

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                BloquearDesbloquearCamposForm(True)

                Acao = Utilidad.eAcao.Modificacion
                CarregaDadosForm(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnSalvar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoDivisa.Enabled = False

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

                CarregaDadosForm(Server.UrlDecode(codigo))

                btnBajaConfirm.Visible = False
                btnNovo.Enabled = False
                btnCancelar.Enabled = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Visible = True
                pnForm.Enabled = True
                BloquearDesbloquearCamposForm(False)
                btnAltaAjeno.Enabled = True
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub BloquearDesbloquearCamposForm(ativo As Boolean)
        txtCodigoDivisa.Enabled = ativo
        txtDescricaoDivisaForm.Enabled = ativo
        txtSimbolo.Enabled = ativo
        txtColor.Enabled = ativo
        txtCodigoAcesso.Enabled = ativo
        txtCodigoAjeno.Enabled = ativo
        txtDesCodigoAjeno.Enabled = ativo
        chkVigenteForm.Enabled = ativo
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
                Acao = Utilidad.eAcao.Baja

                CarregaDadosForm(Server.UrlDecode(codigo))

                btnBajaConfirm.Visible = False
                btnNovo.Enabled = False
                btnCancelar.Enabled = True
                btnBajaConfirm.Visible = True
                btnSalvar.Enabled = False
                chkVigenteForm.Visible = True
                lblVigenteForm.Visible = True
                pnForm.Visible = True
                pnForm.Enabled = True
                btnAltaAjeno.Enabled = True
                BloquearDesbloquearCamposForm(False)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class