Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Busca de tipo procesado
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 30/01/2009 - Criado
''' </history>
Partial Public Class BusquedaTiposProcesado
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona scripts aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim s As String = String.Empty

        'Aciona o botão buscar quando o enter é prescionado.
        txtCodigoTipoProcesado.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtDescricaoTipoProcesado.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        lstCaracteristicas.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtCodigoTiposProcesado.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTiposProcesado.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTiposProcesado.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")

    End Sub

    ''' <summary>
    ''' configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
        
    End Sub

    ''' <summary>
    ''' Parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
    End Sub

    ''' <summary>
    ''' Metodo chamado ao carregar a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not Page.IsPostBack Then

                'Preenche a lista de características
                PreencherListaCaracteristicas()
                PreencherListaCaracteristicasDisponiveis()
                ExecutarBusca()

                pnForm.Visible = False
                'setando os campos inciais
                btnNovo.Enabled = True
                btnSalvar.Enabled = False
                btnCancelar.Enabled = False
                btnBajaConfirm.Enabled = False
                btnBajaConfirm.Visible = False
                ckVigenteForm.Checked = True
                ckVigenteForm.Enabled = False
                ckVigenteForm.Visible = False
                lblVigenteForm.Visible = False

            End If

            'chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

            'TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
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
    ''' Tradução dos contoles da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        lblCodigoTipoProcesado.Text = Traduzir("004_lbl_codigotiposprocesado")
        lblDescricaoTipoProcesado.Text = Traduzir("004_lbl_descripciontiposprocesado")

        lblCaracteristicas.Text = Traduzir("004_lst_caracteristicas")

        lblVigente.Text = Traduzir("004_chk_vigente")
        chkVigente.Text = String.Empty

        lblTitulosTiposProcesado.Text = Traduzir("004_titulo_tiposprocesado")
        lblSubTitulosTiposProcesado.Text = Traduzir("004_subtitulo_tiposprocesado")
        ProsegurGridView1.PagerSummary = Traduzir("grid_lbl_pagersummary")
        Master.Titulo = Traduzir("004_title_tiposprocesado")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        btnNovo.Text = Traduzir("btnAlta")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBaja.Text = Traduzir("btnBaja")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")

        'Grid

        ProsegurGridView1.Columns(1).HeaderText = Traduzir("004_lbl_grd_codigo")
        ProsegurGridView1.Columns(2).HeaderText = Traduzir("004_lbl_grd_descripcion")
        ProsegurGridView1.Columns(3).HeaderText = Traduzir("004_lbl_grd_observacion")
        ProsegurGridView1.Columns(4).HeaderText = Traduzir("004_lbl_grd_vigente")


        'Form
        lblCodigoTiposProcesado.Text = Traduzir("004_lbl_codigotiposprocesado")
        lblDescricaoTiposProcesado.Text = Traduzir("004_lbl_descripciontiposprocesado")
        lblVigenteForm.Text = Traduzir("004_chk_vigente")
        ckVigenteForm.Text = String.Empty
        lblTituloCaracteristicas.Text = Traduzir("004_titulo_caracteristicas_matenimentotiposprocesado")
        lblObservaciones.Text = Traduzir("004_lbl_observacion")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("004_msg_tipoprocesadocodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("004_msg_tipoprocesadodescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("004_msg_codigotiposprocesadoexistente")
        csvDescricaoExistente.ErrorMessage = Traduzir("004_msg_descricaotiposprocesadoexistente")
        lblTituloTiposProcesadoForm.Text = Traduzir("004_title_matenimentotiposprocesado")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
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

    Private Property TipoProcesado() As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado
        Get
            Return DirectCast(ViewState("TipoProcesado"), IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado)
        End Get
        Set(value As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado)
            ViewState("TipoProcesado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propriedades da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2009 - Criado
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

    Property FiltroCaracteristicas() As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaColeccion
        Get
            Return ViewState("FiltroCaracteristicas")
        End Get
        Set(value As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaColeccion)
            ViewState("FiltroCaracteristicas") = value
        End Set
    End Property

    Public Property TiposProcesadosTemPorario() As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion
        Get
            If ViewState("TiposProcesadosTemPorario") Is Nothing Then
                ViewState("TiposProcesadosTemPorario") = New IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion
            End If

            Return DirectCast(ViewState("TiposProcesadosTemPorario"), IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion)
        End Get

        Set(value As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion)
            ViewState("TiposProcesadosTemPorario") = value
        End Set
    End Property

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Function getTiposProcesado() As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

        Dim objProxyTiposProcesado As New Comunicacion.ProxyTiposProcesado
        Dim objPeticionTiposProcesado As New IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Peticion
        Dim objRespuestaTiposProcesado As New IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

        objPeticionTiposProcesado.Vigente = FiltroVigente
        objPeticionTiposProcesado.Caracteristicas = FiltroCaracteristicas
        objPeticionTiposProcesado.Descripcion = FiltroDescripcion
        objPeticionTiposProcesado.Codigo = FiltroCodigo

        objRespuestaTiposProcesado = objProxyTiposProcesado.GetTiposProcesado(objPeticionTiposProcesado)

        TiposProcesadosTemPorario = objRespuestaTiposProcesado.TiposProcessados

        Return objRespuestaTiposProcesado
    End Function

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Public Sub PreencherTiposProcesado()

        Dim objRespostaTiposProcesado As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

        'Busca os canais
        objRespostaTiposProcesado = getTiposProcesado()

        If Not Master.ControleErro.VerificaErro(objRespostaTiposProcesado.CodigoError, objRespostaTiposProcesado.NombreServidorBD, objRespostaTiposProcesado.MensajeError) Then
            Exit Sub
        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaTiposProcesado.TiposProcessados.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespostaTiposProcesado.TiposProcessados.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable
                objDt = ProsegurGridView1.ConvertListToDataTable(objRespostaTiposProcesado.TiposProcessados)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigo ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView1.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " Codigo ASC "
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
    ''' Envia o produto selecionado para a sessão.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub SetTipoProcesadoSelecionado()

        Dim objTiposProcesado As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado
        objTiposProcesado = SelectTiposProcesado(TiposProcesadosTemPorario, Server.UrlEncode(ProsegurGridView1.getValorLinhaSelecionada))

        Session("setTiposProcesados") = objTiposProcesado

    End Sub

    ''' <summary>
    ''' Função responsável por fazer um select na hora que e selecionado um registro no grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Function SelectTiposProcesado(TiposProcesado As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesadoColeccion, codigo As String) As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado

        Dim retorno = From c In TiposProcesado Where c.Codigo = codigo Select c.Descripcion, c.Observaciones, c.Vigente, c.Caracteristicas

        Dim objTipoProcesadoEnvio As New IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado

        Dim en As IEnumerator = retorno.GetEnumerator()
        Dim objRetorno As Object

        If en.MoveNext Then
            objRetorno = en.Current

            objTipoProcesadoEnvio.Codigo = codigo
            objTipoProcesadoEnvio.Descripcion = objRetorno.Descripcion
            objTipoProcesadoEnvio.Observaciones = objRetorno.Observaciones
            objTipoProcesadoEnvio.Vigente = objRetorno.Vigente
            objTipoProcesadoEnvio.Caracteristicas = objRetorno.Caracteristicas
        End If

        Return objTipoProcesadoEnvio
    End Function

    ''' <summary>
    ''' Função responsável por fazer o tratamento do foco.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
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
    ''' [carlos.bomtempo] 26/05/2009 Created
    ''' </history>
    Public Sub PreencherListaCaracteristicas()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaracteristicas()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Caracteristica In objRespuesta.Caracteristicas
            objCaracteristica.Descripcion = objCaracteristica.Codigo & " - " & objCaracteristica.Descripcion
        Next

        lstCaracteristicas.AppendDataBoundItems = True
        lstCaracteristicas.Items.Clear()
        lstCaracteristicas.DataTextField = "Descripcion"
        lstCaracteristicas.DataValueField = "Codigo"
        lstCaracteristicas.DataSource = objRespuesta.Caracteristicas
        lstCaracteristicas.DataBind()

    End Sub
    Public Sub PreencherListaCaracteristicasDisponiveis()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaracteristicas()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Caracteristica In objRespuesta.Caracteristicas
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
    Private Sub ProsegurGridView1_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            ProsegurGridView1.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("vigente").ToString.ToLower & ");"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Responsavel pela ordenação do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView1.EPreencheDados

        Try

            Dim objDT As DataTable

            Dim objRespoustaTiposProcesado As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.Respuesta

            objRespoustaTiposProcesado = getTiposProcesado()

            If objRespoustaTiposProcesado.TiposProcessados.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False


                objDT = ProsegurGridView1.ConvertListToDataTable(objRespoustaTiposProcesado.TiposProcessados)

                If ProsegurGridView1.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigo asc"
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
    ''' [anselmo.gois] 30/01/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView1.RowStyle.Height = 20
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
    ''' [anselmo.gois] 30/01/2008 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción
                '2 - Observaciones
                '3 - Vigente

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("Observaciones") Is DBNull.Value AndAlso e.Row.DataItem("Observaciones").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("Observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
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
    ''' [anselmo.gois] 30/01/2008 Created
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView1.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

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
    ''' [anselmo.gois] 02/02/2008 Created
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            'Limpa a consulta
            ProsegurGridView1.DataSource = Nothing
            ProsegurGridView1.DataBind()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            txtCodigoTipoProcesado.Text = String.Empty
            txtDescricaoTipoProcesado.Text = String.Empty
            lstCaracteristicas.ClearSelection()
            chkVigente.Checked = True

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

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
    ''' [anselmo.gois] 02/02/2008 Created
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            ExecutarBusca()
            btnCancelar_Click(sender, e)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try
    End Sub
    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        If txtCodigoTipoProcesado.Text.Trim <> String.Empty Then
            FiltroCodigo = txtCodigoTipoProcesado.Text.Trim.ToUpper
        Else
            FiltroCodigo = String.Empty
        End If

        If txtDescricaoTipoProcesado.Text.Trim <> String.Empty Then
            FiltroDescripcion = txtDescricaoTipoProcesado.Text.Trim.ToUpper
        Else
            FiltroDescripcion = String.Empty
        End If

        'Filtros
        FiltroVigente = chkVigente.Checked
        Dim indices As Integer() = lstCaracteristicas.GetSelectedIndices()
        FiltroCaracteristicas = New ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaColeccion()
        Dim objCaracteristica As ContractoServicio.TiposProcesado.GetTiposProcesado.Caracteristica
        For Each indice As Integer In indices
            objCaracteristica = New ContractoServicio.TiposProcesado.GetTiposProcesado.Caracteristica()
            objCaracteristica.Codigo = lstCaracteristicas.Items(indice).Value
            FiltroCaracteristicas.Add(objCaracteristica)
        Next

        'Retorna os canais de acordo com o filtro aciam
        PreencherTiposProcesado()
    End Sub

    ''' <summary>
    ''' Evento do botão deletar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 02/02/2008 Created
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try

            Acao = Aplicacao.Util.Utilidad.eAcao.Baja
            Dim objProxyTipoProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionTipoProcesado As New IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion
            Dim objRespuestaTipoProcesado As IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta
            Dim codigo As String = String.Empty
            'Retorna a linha selecionada no grid
            Dim objTiposProcesado As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado

            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then

                If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView1.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
            End If

            objTiposProcesado = SelectTiposProcesado(TiposProcesadosTemPorario, codigo)
            objPeticionTipoProcesado.Codigo = objTiposProcesado.Codigo
            objPeticionTipoProcesado.Descripcion = objTiposProcesado.Descripcion
            objPeticionTipoProcesado.Vigente = False
            objPeticionTipoProcesado.CodUsuario = MyBase.LoginUsuario
            If objTiposProcesado.Caracteristicas IsNot Nothing AndAlso objTiposProcesado.Caracteristicas.Count > 0 Then
                objPeticionTipoProcesado.Caracteristicas = New ContractoServicio.TiposProcesado.SetTiposProcesado.CaracteristicaColeccion()
                For Each objCaracteristica As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuesta In objTiposProcesado.Caracteristicas
                    objPeticionTipoProcesado.Caracteristicas.Add(New ContractoServicio.TiposProcesado.SetTiposProcesado.Caracteristica() With {.Codigo = objCaracteristica.Codigo})
                Next
            End If

            'Exclui a petição
            objRespuestaTipoProcesado = objProxyTipoProcesado.SetTiposProcesado(objPeticionTipoProcesado)

            If Master.ControleErro.VerificaErro(objRespuestaTipoProcesado.CodigoError, objRespuestaTipoProcesado.NombreServidorBD, objRespuestaTipoProcesado.MensajeError, True, False) Then

                'Registro excluido com sucesso
                MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))

                'Atualiza o GridView
                ExecutarBusca()
                UpdatePanelGrid.Update()
                btnCancelar_Click(Nothing, Nothing)

            Else
                MyBase.MostraMensagem(objRespuestaTipoProcesado.MensajeError)
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
    ''' [anselmo.gois] 30/01/2009 - Criado
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
#Region "Botoes listbox"
    Private Sub imbAdicionarTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarTodasCaracteristicas.Click

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

    Private Sub imbAdicionarCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarCaracteristicasSelecionadas.Click

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

    Private Sub imbRemoverCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverCaracteristicasSelecionadas.Click

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

    Private Sub imbRemoverTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverTodasCaracteristicas.Click

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
#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Acao = Utilidad.eAcao.Modificacion
            SetFocus(txtDescricaoTiposProcesado)
            CarregaDados()

            DescricaoAtual = txtDescricaoTiposProcesado.Text

            pnForm.Enabled = True
            pnForm.Visible = True

            btnSalvar.Enabled = True
            btnCancelar.Enabled = True
            btnBajaConfirm.Enabled = False
            btnBajaConfirm.Visible = False

            txtCodigoTiposProcesado.Enabled = False
            ckVigenteForm.Visible = True

            If ckVigenteForm.Checked AndAlso EsVigente Then
                ckVigenteForm.Enabled = False
            Else
                ckVigenteForm.Enabled = True
            End If

            txtDescricaoTiposProcesado.Focus()
            
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Acao = Utilidad.eAcao.Consulta
            CarregaDados()

            'Configurandos os campos
            pnForm.Enabled = False
            pnForm.Visible = True

            lstCaracteristicasDisponiveis.Visible = False

            'configurando os botões
            btnSalvar.Enabled = False
            btnCancelar.Enabled = True
            btnNovo.Enabled = True
            btnBajaConfirm.Enabled = False
            btnBajaConfirm.Visible = False
            TipoProcesado = Nothing
            ckVigenteForm.Visible = True
            lblVigenteForm.Visible = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            Acao = Utilidad.eAcao.Baja
            CarregaDados()

            'Configurandos os campos
            pnForm.Enabled = False
            pnForm.Visible = True

            lstCaracteristicasDisponiveis.Visible = False

            'configurando os botões
            btnSalvar.Enabled = False
            btnNovo.Enabled = True
            btnCancelar.Enabled = True
            btnBajaConfirm.Enabled = True
            btnBajaConfirm.Visible = True
            ckVigenteForm.Visible = True
            lblVigenteForm.Visible = True

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub CarregaDados()

        If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
            Dim codigo As String = String.Empty
            If Not String.IsNullOrEmpty(ProsegurGridView1.getValorLinhaSelecionada) Then
                codigo = ProsegurGridView1.getValorLinhaSelecionada
            Else
                codigo = hiddenCodigo.Value.ToString()
            End If

            TipoProcesado = SelectTiposProcesado(TiposProcesadosTemPorario, Server.UrlDecode(codigo))
            If TipoProcesado IsNot Nothing Then

                Dim objColCaracteristicas As New ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuestaColeccion

                txtCodigoTiposProcesado.Text = TipoProcesado.Codigo
                txtCodigoTiposProcesado.ToolTip = TipoProcesado.Codigo

                txtDescricaoTiposProcesado.Text = TipoProcesado.Descripcion
                txtDescricaoTiposProcesado.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TipoProcesado.Descripcion, String.Empty)

                txtObservaciones.Text = TipoProcesado.Observaciones
                txtObservaciones.ToolTip = TipoProcesado.Observaciones

                ckVigenteForm.Checked = TipoProcesado.Vigente
                EsVigente = TipoProcesado.Vigente

                If TipoProcesado.Caracteristicas IsNot Nothing Then

                    For Each objCarac As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuesta In TipoProcesado.Caracteristicas
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
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try

            txtCodigoTiposProcesado.Text = String.Empty
            txtCodigoTipoProcesado.Enabled = True
            txtDescricaoTiposProcesado.Text = String.Empty
            txtObservaciones.Text = String.Empty
            ckVigenteForm.Checked = True
            TipoProcesado = Nothing
            pnForm.Enabled = True
            pnForm.Visible = False
            lstCaracteristicasDisponiveis.Visible = True

            btnSalvar.Enabled = False
            btnCancelar.Enabled = False
            btnNovo.Enabled = True
            btnBajaConfirm.Enabled = False
            btnBajaConfirm.Visible = False

            ckVigenteForm.Checked = True
            ckVigenteForm.Enabled = False
            ckVigenteForm.Visible = False
            ckVigenteForm.Visible = False

            PreencherListaCaracteristicasDisponiveis()
            lstCaracteristicasSelecionadas.Items.Clear()

            Acao = Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        SetFocus(txtCodigoTiposProcesado)
        btnCancelar_Click(sender, e)
        pnForm.Visible = True
        lblCodigoTiposProcesado.Enabled = True
        btnSalvar.Enabled = True
        btnCancelar.Enabled = True
        btnNovo.Enabled = True
        txtCodigoTiposProcesado.Enabled = True
        txtCodigoTiposProcesado.Focus()
        Acao = Utilidad.eAcao.Inicial
    End Sub

    Private Sub ExecutarGrabar()
        Try

            Dim objProxyTipoProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionTipoProcesado As New IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion
            Dim objRespuestaTipoProcesado As IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta
            Dim strErro As String = String.Empty

            ValidarCamposObrigatorios = True

            strErro = MontaMensagensErro()
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If


            objPeticionTipoProcesado.Vigente = chkVigente.Checked

            ' atualizar propriedade
            EsVigente = chkVigente.Checked

            objPeticionTipoProcesado.Codigo = txtCodigoTiposProcesado.Text
            objPeticionTipoProcesado.Descripcion = txtDescricaoTiposProcesado.Text
            objPeticionTipoProcesado.Observaciones = txtObservaciones.Text
            objPeticionTipoProcesado.CodUsuario = MyBase.LoginUsuario
            objPeticionTipoProcesado.Caracteristicas = New ContractoServicio.TiposProcesado.SetTiposProcesado.CaracteristicaColeccion()

            For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                objPeticionTipoProcesado.Caracteristicas.Add(New ContractoServicio.TiposProcesado.SetTiposProcesado.Caracteristica() With {.Codigo = objItem.Value})
            Next

            objRespuestaTipoProcesado = objProxyTipoProcesado.SetTiposProcesado(objPeticionTipoProcesado)

            'Define a ação de busca somente se houve retorno de canais

            Dim msgErro As String = String.Empty
            If Master.ControleErro.VerificaErro2(objRespuestaTipoProcesado.CodigoError, objRespuestaTipoProcesado.NombreServidorBD, msgErro, objRespuestaTipoProcesado.MensajeError) Then

                If Master.ControleErro.VerificaErro2(objRespuestaTipoProcesado.CodigoError, objRespuestaTipoProcesado.NombreServidorBD, msgErro, objRespuestaTipoProcesado.MensajeError) Then

                    MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    ExecutarBusca()
                    UpdatePanelGrid.Update()
                    btnCancelar_Click(Nothing, Nothing)
                Else
                    If msgErro.Length > 0 Then
                        MyBase.MostraMensagem(msgErro)
                    End If
                    Exit Sub
                End If

            Else
                If objRespuestaTipoProcesado.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    If objRespuestaTipoProcesado.MensajeError <> String.Empty Then
                        MyBase.MostraMensagem(objRespuestaTipoProcesado.MensajeError)
                    End If
                End If

                If msgErro.Length > 0 Then
                    MyBase.MostraMensagem(msgErro)
                End If

                Exit Sub

            End If

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
                If txtCodigoTiposProcesado.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTiposProcesado.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoTiposProcesado.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTiposProcesado.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoTiposProcesado.Text.Trim()) AndAlso ExisteCodigoTipoProcesado(txtCodigoTiposProcesado.Text.Trim()) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTiposProcesado.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If Not String.IsNullOrEmpty(txtDescricaoTiposProcesado.Text.Trim()) AndAlso ExisteDescricaoTipoProcesado(txtDescricaoTiposProcesado.Text.Trim()) Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoTiposProcesado.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function
    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoTipoProcesado(codigoTipoProcesado As String) As Boolean

        Dim objRespostaVerificarCodigoTiposProcesado As IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxyTiposProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionVerificarCodigoTiposProcesado As New IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoTiposProcesado.Codigo = codigoTipoProcesado.Trim
            objRespostaVerificarCodigoTiposProcesado = objProxyTiposProcesado.VerificarCodigoTipoProcesado(objPeticionVerificarCodigoTiposProcesado)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTiposProcesado.CodigoError, objRespostaVerificarCodigoTiposProcesado.NombreServidorBD, objRespostaVerificarCodigoTiposProcesado.MensajeError) Then
                Return objRespostaVerificarCodigoTiposProcesado.Existe
            Else

                MyBase.MostraMensagem(objRespostaVerificarCodigoTiposProcesado.MensajeError)
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
    Private Function ExisteDescricaoTipoProcesado(descricao As String) As Boolean

        Dim objRespostaVerificarDescricaoTiposProcesado As IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Return False
                End If
            End If

            Dim objProxyTiposProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionVerificarDescricaoTiposProcesado As New IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoTiposProcesado.Descripcion = txtDescricaoTiposProcesado.Text
            objRespostaVerificarDescricaoTiposProcesado = objProxyTiposProcesado.VerificarDescripcionTipoProcesado(objPeticionVerificarDescricaoTiposProcesado)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoTiposProcesado.CodigoError, objRespostaVerificarDescricaoTiposProcesado.NombreServidorBD, objRespostaVerificarDescricaoTiposProcesado.MensajeError) Then
                Return objRespostaVerificarDescricaoTiposProcesado.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarDescricaoTiposProcesado.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub
End Class