Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Public Class BusquedaInformeResultadoContaje
    Inherits Base

#Region "[VARIÁVEIS]"

    Dim Valida As New List(Of String)
    '  Dim OIDRemessaSelecionada As String

#End Region

#Region "[PROPRIEDADES]"

    Private Property OIDRemessaSelecionada As String
        Get
            Return CType(ViewState("OIDRemessaSelecionada"), String)
        End Get
        Set(value As String)
            ViewState("OIDRemessaSelecionada") = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteSelecionado() As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto subcliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubClienteSelecionado() As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
        Get
            Return ViewState("SubClienteSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente)
            ViewState("SubClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto puntoservicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PuntoServicioSelecionado() As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Get
            Return ViewState("PuntoServicioSelecionado")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio)
            ViewState("PuntoServicioSelecionado") = value
        End Set
    End Property


    ''' <summary>
    ''' Objeto resultado de las contajes (remesas)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResultadoContajes() As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion
        Get
            Return Session("ResultadoContajes")
        End Get
        Set(value As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.RemesaColeccion)
            Session("ResultadoContajes") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto resultado de contaje selecionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Remesa() As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa
        Get
            Return Session("InformeResultadoContaje")
        End Get
        Set(value As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Remesa)
            Session("InformeResultadoContaje") = value
        End Set
    End Property

    ''' <summary>
    ''' Nome Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NomeDocumento() As String
        Get
            Return Session("ResultadoContajeNomeDocumento")
        End Get
        Set(value As String)
            Session("ResultadoContajeNomeDocumento") = value
        End Set
    End Property

#End Region

#Region "[METODOS BASE]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        pbo = New PostBackOptions(btnBuscar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBuscar.OnClientClick = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnBuscar.ClientID & "," & btnLimpar.ClientID & "');"

        pbo = New PostBackOptions(btnLimpar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnLimpar.OnClientClick = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnBuscar.ClientID & "," & btnLimpar.ClientID & "');"

        'Adiciona a Precedencia ao Buscar
        scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnBuscar.ClientID & "';", True)

        ' Adiciona Scripts aos controles
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtPrecintoRemesa.Enabled = True
                Me.txtPrecintoBulto.Enabled = True
                Me.txtNumAlbaran.Enabled = True
                'Me.txtCliente.Enabled = False
                'Me.txtSubcliente.Enabled = False
                'Me.txtPuntoServicio.Enabled = False
                Me.txtFechaConteoDesde.Enabled = True
                Me.txtFechaConteoHasta.Enabled = True
                Me.txtFechaTransporteDesde.Enabled = True
                Me.txtFechaTransporteHasta.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.INFORME_RESULTADO_CONTAJE
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try
            ASPxGridView.RegisterBaseScript(Page)
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("019_titulo_pagina")

            ConfigurarControle_Cliente()

            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                ' Carrega os dados iniciais dos controles
                CarregarControles()

                ' Inicializa os campos
                LimparCampos()

            End If

            ' setar foco no campo codigo
            Me.ddlDelegacion.Focus()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()

        Try
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaTransporteDesde.ClientID, "True", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaTransporteHasta.ClientID, "True", 2)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaConteoDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaConteoHasta.ClientID, "true", 2)


            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("019_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("019_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("019_lbl_delegacion")
        Me.lblPrecintoRemesa.Text = Traduzir("019_lbl_precinto_remesa")
        Me.lblPrecintoBulto.Text = Traduzir("019_lbl_precinto_bulto")
        Me.lblNumAlbaran.Text = Traduzir("019_lbl_numero_albaran")
        Me.lblFechaConteo.Text = Traduzir("019_lbl_fecha_conteo")
        Me.lblFechaConteoDesde.Text = Traduzir("019_lbl_fecha_conteo_desde")
        Me.lblFechaConteoHasta.Text = Traduzir("019_lbl_fecha_conteo_hasta")
        Me.lblFechaTransporte.Text = Traduzir("019_lbl_fecha_transporte")
        Me.lblFechaTransporteDesde.Text = Traduzir("019_lbl_fecha_transporte_desde")
        Me.lblFechaTransporteHasta.Text = Traduzir("019_lbl_fecha_transporte_hasta")
        Me.lblTituloGrid.Text = Traduzir("019_titulo_grid")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.btnLimpar.Text = Traduzir("btnLimpiar")
        Me.btnBuscar.ToolTip = Traduzir("btnBuscar")
        Me.btnLimpar.ToolTip = Traduzir("btnLimpiar")
        Me.btnVisualizar.Text = Traduzir("016_btn_visualizar")
        Me.btnVoltar.Text = Traduzir("016_btn_voltar")
        Me.btnVisualizar.ToolTip = Traduzir("016_btn_visualizar")
        Me.btnVoltar.ToolTip = Traduzir("016_btn_voltar")

        Me.gvResultadoContaje.Columns(1).Caption = Traduzir("019_col_fecha_conteo")
        Me.gvResultadoContaje.Columns(2).Caption = Traduzir("019_col_precinto_remesa")
        Me.gvResultadoContaje.Columns(3).Caption = Traduzir("019_col_codigo_transporte")
        Me.gvResultadoContaje.Columns(4).Caption = Traduzir("019_col_fecha_transporte")
        Me.gvResultadoContaje.Columns(5).Caption = Traduzir("019_col_cliente")
        Me.gvResultadoContaje.Columns(6).Caption = Traduzir("019_col_subcliente")
        Me.gvResultadoContaje.Columns(7).Caption = Traduzir("019_col_punto_servicio")

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Limpa os campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimparCampos()

        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        Me.txtPrecintoRemesa.Text = String.Empty
        Me.txtPrecintoBulto.Text = String.Empty
        Me.txtNumAlbaran.Text = String.Empty

        Me.txtFechaConteoDesde.Text = String.Empty
        Me.txtFechaConteoHasta.Text = String.Empty
        Me.txtFechaTransporteDesde.Text = String.Empty
        Me.txtFechaTransporteHasta.Text = String.Empty

        OIDRemessaSelecionada = Nothing

        Clientes = Nothing
        Session("ClienteSelecionado") = Nothing
        Session("SubClienteSelecionado") = Nothing
        Session("PuntoServicioSelecionado") = Nothing

    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)
        ' Define a mascara do período inicial digitado
        Me.txtFechaConteoDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaConteoHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período inicial digitado
        Me.txtFechaTransporteDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaTransporteHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")



    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarControles()

        ' carregar delegações do xml para o combo
        Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

        ' Inicializa o controle de delegação com a mesma delegação selecionada na tela de login
        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidarControles()

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("019_lbl_delegacion")))
        End If

        ' Verifica se o número remessa foi preenchido
        If (txtPrecintoRemesa.Text = String.Empty AndAlso _
           (txtPrecintoBulto.Text = String.Empty AndAlso txtNumAlbaran.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("019_lbl_precinto_remesa")))
        End If

        ' Verifica se o número precinto foi preenchido
        If (txtPrecintoBulto.Text = String.Empty AndAlso _
           (txtPrecintoRemesa.Text = String.Empty AndAlso txtNumAlbaran.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("019_lbl_precinto_bulto")))
        End If

        ' Verifica se o número transporte foi preenchido
        If (txtNumAlbaran.Text = String.Empty AndAlso _
           (txtPrecintoRemesa.Text = String.Empty AndAlso txtPrecintoBulto.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("019_lbl_numero_albaran")))
        End If

        ' Verifica se a delegacion foi preenchida
        If Clientes Is Nothing OrElse Clientes.Count = 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("019_lbl_cliente")))
        End If

        ' Verifica se a data de processo inicial é uma data válida
        If txtFechaConteoDesde.Text <> String.Empty AndAlso (txtFechaConteoDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaConteoDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("019_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaConteoDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaConteoDesde.Text = txtFechaConteoDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de processo final é uma data válida
        If txtFechaConteoHasta.Text <> String.Empty AndAlso (txtFechaConteoHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaConteoHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("019_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaConteoHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo final não foi informada
            ' coloca a hora final do dia
            txtFechaConteoHasta.Text = txtFechaConteoHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de transporte inicial é uma data válida
        If txtFechaTransporteDesde.Text <> String.Empty AndAlso (txtFechaTransporteDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("019_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaConteoDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de transporte inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaTransporteDesde.Text = txtFechaTransporteDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de transporte final é uma data válida
        If txtFechaTransporteHasta.Text <> String.Empty AndAlso (txtFechaTransporteHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("019_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaConteoHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de transporte final não foi informada
            ' coloca a hora final do dia
            txtFechaTransporteHasta.Text = txtFechaTransporteHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de processo inicial é maior do que a data de processo inicial
        If (IsDate(txtFechaConteoDesde.Text) AndAlso IsDate(txtFechaConteoHasta.Text)) AndAlso _
        Not Date.Compare(Convert.ToDateTime(txtFechaConteoDesde.Text.Substring(0, 10)), Convert.ToDateTime(txtFechaConteoHasta.Text.Substring(0, 10))) = 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_dias_distintos"), Traduzir("019_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta"), Traduzir("019_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de transporte final é maior do que a data inicial
        If (IsDate(txtFechaTransporteDesde.Text) AndAlso IsDate(txtFechaTransporteHasta.Text)) AndAlso _
        Not Date.Compare(Convert.ToDateTime(txtFechaTransporteDesde.Text.Substring(0, 10)), Convert.ToDateTime(txtFechaTransporteHasta.Text.Substring(0, 10))) = 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_dias_distintos"), Traduzir("019_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta"), Traduzir("019_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

    End Sub


    ''' <summary>
    ''' Consulta os dados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultarDados()

        Try
            ' Valida os controles usados no filtro
            Me.ValidarControles()

            ' Se não existe erro
            If Valida.Count = 0 Then

                Dim objProxyInformeResultado As New ListadosConteo.ProxyInformeResultadoContaje

                ' Define os valores dos filtros da consulta
                Dim objPeticion As New ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Peticion

                objPeticion.CodDelegacion = ddlDelegacion.SelectedValue
                objPeticion.CodPrecintoRemesa = If(String.IsNullOrEmpty(txtPrecintoRemesa.Text.Trim()), String.Empty, txtPrecintoRemesa.Text)
                objPeticion.CodPrecintoBulto = If(String.IsNullOrEmpty(txtPrecintoBulto.Text.Trim()), String.Empty, txtPrecintoBulto.Text)
                objPeticion.CodTransporte = If(String.IsNullOrEmpty(txtNumAlbaran.Text.Trim()), String.Empty, txtNumAlbaran.Text)
                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    objPeticion.CodCliente = Clientes.FirstOrDefault().Codigo
                    If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                        objPeticion.CodSubCliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                        If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                            objPeticion.CodPuntoServicio = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
                        End If
                    End If
                End If

                ' Verifica se a data do sistema foi preenchida
                If (txtFechaConteoDesde.Text <> String.Empty AndAlso txtFechaConteoHasta.Text <> String.Empty) Then
                    objPeticion.FechaInicio = Convert.ToDateTime(txtFechaConteoDesde.Text)
                    objPeticion.FechaFin = Convert.ToDateTime(txtFechaConteoHasta.Text).AddSeconds(59)
                    objPeticion.EsFechaTransporte = False
                End If

                ' Verifica se a data de tranporte foi preenchida
                If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
                    objPeticion.EsFechaTransporte = True
                    objPeticion.FechaInicio = Convert.ToDateTime(txtFechaTransporteDesde.Text)
                    objPeticion.FechaFin = Convert.ToDateTime(txtFechaTransporteHasta.Text).AddSeconds(59)
                End If

                ' Recupera os detalles parciales para popular o relatório
                Dim objRespuesta As ContractoServ.InformeResultadoContaje.BuscarResultadoContaje.Respuesta = objProxyInformeResultado.BuscarResultadoContaje(objPeticion)

                ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório validar retorno do serviço
                Dim msgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                    MyBase.MostraMensagem(msgErro)
                    Exit Sub
                End If

                ' Verifica o formato do relatório e existe dados
                If objRespuesta.Remesas IsNot Nothing AndAlso objRespuesta.Remesas.Count > 0 Then

                    If objRespuesta.Remesas.Count = 1 Then

                        Me.pnlFiltros.Visible = True
                        Me.pnlGrid.Visible = False

                        OIDRemessaSelecionada = objRespuesta.Remesas(0).OidRemsa

                        VisualizarRelatorio()

                    Else

                        Me.pnlFiltros.Visible = False
                        Me.pnlGrid.Visible = True
                        Me.pnBotoesPesquisa.Visible = False
                        Me.pnBotoesGrid.Visible = True

                        ' Atribui os dados recuperados para a sessão
                        ResultadoContajes = objRespuesta.Remesas

                        ' converte o objeto retornado para um datatable
                        Dim objDT As DataTable = Util.ConverterListParaDataTable(objRespuesta.Remesas)


                        gvResultadoContaje.DataSource = objDT
                        gvResultadoContaje.DataBind()

                    End If

                Else

                    Me.pnlFiltros.Visible = True
                    Me.pnlGrid.Visible = False
                    Me.pnBotoesPesquisa.Visible = True
                    Me.pnBotoesGrid.Visible = False

                    gvResultadoContaje.DataSource = Nothing
                    gvResultadoContaje.DataBind()

                    ' Define a mensagem de registros não encontrados
                    Valida.Add(Traduzir("lbl_nenhum_registro_encontrado"))
                    MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                    Exit Sub

                End If

            Else
                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Recupera as informações do relatório e chama a página que irá gerará-lo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisualizarRelatorio()

        Try

            Dim objProxyInformeResultado As New ListadosConteo.ProxyInformeResultadoContaje
            Dim objPeticionListaResultado As New ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Peticion

            objPeticionListaResultado.OidRemesa = OIDRemessaSelecionada

            Dim objRespuestaListarResultado As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.Respuesta = objProxyInformeResultado.ListarResultadoContaje(objPeticionListaResultado)

            ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório validar retorno do serviço
            Dim msgErro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(objRespuestaListarResultado.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuestaListarResultado.MensajeError, True) Then
                MyBase.MostraMensagem(msgErro)
                Exit Sub
            End If

            Remesa = objRespuestaListarResultado.Remesa

            ' gera o nome dos arquivos
            GeraNomeArquivo()

            ' Define a url que será aberta
            Dim url As String = "InformeResultadoContajeMostrar.aspx?acao=" & Enumeradores.eAcao.Consulta

            'AbrirPopupModal
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_informeresultadocontaje", "AbrirJanela('" & url & "', '_blank', true);", True)

            ' Chama a página que exibirá o relatório
            scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_informeresultadocontaje", "window.open('" & url & "') ;", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Gera o nome dos documentos pdf
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GeraNomeArquivo()

        Dim nome As String = String.Empty

        ' a)	Delegación: Se utilizan los dos caracteres iniciales correspondientes al nombre de la Delegación.
        nome = ddlDelegacion.SelectedValue.Substring(0, 2).ToUpper() & "_"

        ' b)	Nombre del Cliente: Se utilizan los caracteres que conforman el nombre del Cliente.
        nome &= If((Clientes IsNot Nothing AndAlso Clientes.Count > 0), Clientes.FirstOrDefault().Descripcion, String.Empty).ToUpper().Substring(0, 4)

        ' c)	Tipo: recibe la descripción ‘Contaje’. 
        nome &= "CONTAJE"

        ' d)	Fecha: Se incluye únicamente en el caso en que se filtre por Fecha de Transporte para lo cual se utilizarán cuatro caracteres indicando mes – día (mmdd)
        If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
            nome &= Convert.ToDateTime(txtFechaTransporteDesde.Text).ToString("mmdd")
        End If

        NomeDocumento = nome

    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Ação ao clicar no botão de buscar dados do relatório
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Me.ConsultarDados()
    End Sub

    ''' <summary>
    ''' Ação ao limpar os valores dos filtros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Me.LimparCampos()
        Response.Redireccionar("BusquedaInformeResultadoContaje.aspx")
    End Sub

    ''' <summary>
    ''' Ação ao clicar no botão de visualizar os dados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnVisualizar_Click(sender As Object, e As System.EventArgs) Handles btnVisualizar.Click

        If Not String.IsNullOrEmpty(OIDRemessaSelecionada) Then

            Me.VisualizarRelatorio()

        Else

            ' Define a mensagem de registros não encontrados
            Valida.Add(Traduzir("019_msg_remesa_obligatoria"))
            MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))

        End If

    End Sub

    ''' <summary>
    ''' Ação ao clicar no botão de voltar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVoltar_Click(sender As Object, e As System.EventArgs) Handles btnVoltar.Click

        Me.pnlFiltros.Visible = True
        Me.pnlGrid.Visible = False

        ' reinicia o grid
        ResultadoContajes = Nothing
        gvResultadoContaje.DataSource = Nothing
        gvResultadoContaje.DataBind()

    End Sub

#End Region

#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False


        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Exception, e.Erro.Message, e.Erro.ToString()))
    End Sub
#End Region

    Protected Sub gvResultadoContaje_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                'Seta as propriedades do radio button
                Dim rbSelecionado As HtmlInputRadioButton = CType(gvResultadoContaje.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "rbSelecionado"), HtmlInputRadioButton)
                rbSelecionado.Value = gvResultadoContaje.GetRowValues(e.VisibleIndex, gvResultadoContaje.KeyFieldName).ToString()
                Dim jsScript As String = "javascript: $('.radio_selecao').not(this).each(function () { this.checked = false; }); AddRemovIdSelect(this,'" & hdResultadoContaje.ClientID & "',true,'" & btnConfEscogido.ClientID & "');"
                rbSelecionado.Attributes.Add("onclick", jsScript)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConfEscogido_Click(sender As Object, e As EventArgs) Handles btnConfEscogido.Click
        Try
            If Not String.IsNullOrEmpty(hdResultadoContaje.Value) Then
                Dim separador As String() = {"|"}
                Dim arrIdentificador As String() = hdResultadoContaje.Value.Split(separador, StringSplitOptions.RemoveEmptyEntries)

                If arrIdentificador IsNot Nothing Then
                    OIDRemessaSelecionada = arrIdentificador(0)
                End If
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class