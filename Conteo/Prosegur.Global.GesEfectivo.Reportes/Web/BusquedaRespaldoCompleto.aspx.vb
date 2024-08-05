Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Partial Public Class BusquedaRespaldoCompleto
    Inherits Base

#Region "[VARIÁVEIS]"
    Dim Valida As New List(Of String)
#End Region

#Region "[PROPRIEDADES]"

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
    ''' Objeto Procesos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProcessosSelecionados() As List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso)
        Get
            Return ViewState("ProcessosSelecionados")
        End Get
        Set(value As List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso))
            ViewState("ProcessosSelecionados") = value
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
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnBuscar.ClientID & "';", True)

        ' Adiciona Scripts aos controles
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtSector.Enabled = True
                'Me.listProceso.Enabled = True
                Me.txtFechaTransporteDesde.Enabled = True
                Me.txtFechaTransporteHasta.Enabled = True
                Me.txtFechaDesde.Enabled = True
                Me.txtFechaHasta.Enabled = True
                Me.txtFechaDesdeFinConteo.Enabled = True
                Me.txtFechaHastaFinConteo.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.RESPALDO_COMPLETO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()
        ASPxGridView.RegisterBaseScript(Page)
        Try
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("005_titulo_pagina")

            Me.ConfigurarControle_Cliente()

            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                LimparCampos()

                ' Carrega os dados iniciais dos controles
                CarregarControles()

            End If

            ' setar foco no campo codigo
            Me.ddlDelegacion.Focus()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaTransporteDesde.ClientID, "false", 0)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaTransporteHasta.ClientID, "false", 0)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaHasta.ClientID, "true", 2)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaDesdeFinConteo.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaHastaFinConteo.ClientID, "true", 2)

            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("005_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("005_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("005_lbl_delegacion")
        Me.lblSector.Text = Traduzir("005_lbl_sector")
        Me.lblCanal.Text = Traduzir("004_lbl_canal")
        Me.lblFechaTransporte.Text = Traduzir("005_lbl_fecha_transporte")
        Me.lblFechaTransporteDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaTransporteHasta.Text = Traduzir("lbl_hasta")
        Me.lblFecha.Text = Traduzir("005_lbl_fecha")
        Me.lblFechaDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaHasta.Text = Traduzir("lbl_hasta")
        Me.lblFechaFinConteo.Text = Traduzir("005_lbl_fecha_fin_conteo")
        Me.lblFechaDesdeFinConteo.Text = Traduzir("lbl_desde")
        Me.lblFechaHastaFinConteo.Text = Traduzir("lbl_hasta")
        Me.lblFormatoSaida.Text = Traduzir("005_lbl_formato_salida")
        Me.lblRemesasEnCurso.Text = Traduzir("005_lbl_remesas_en_curso")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.btnLimpar.Text = Traduzir("btnLimpiar")
        Me.btnBuscar.ToolTip = Traduzir("btnBuscar")
        Me.btnLimpar.ToolTip = Traduzir("btnLimpiar")

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
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
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Private Sub LimparCampos()

        ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        txtSector.Text = String.Empty
        listCanal.Items.Clear()
        txtFechaTransporteDesde.Text = String.Empty
        txtFechaTransporteHasta.Text = String.Empty
        txtSector.Text = String.Empty
        txtFechaDesde.Text = String.Empty
        txtFechaHasta.Text = String.Empty
        txtFechaDesdeFinConteo.Text = String.Empty
        txtFechaHastaFinConteo.Text = String.Empty
        rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV
        chkRemesasEnCurso.Checked = False

        ClienteSelecionado = Nothing
        ProcessosSelecionados = Nothing
        Clientes = Nothing

        Session("ClienteSelecionado") = Nothing
        Session("ProcessosSelecionados") = Nothing

    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        ' Define a mascara do período inicial digitado
        Me.txtFechaTransporteDesde.Attributes.Add("onkeypress", "return mascaraData(this);")
        ' Define a mascara do período final digitado
        Me.txtFechaTransporteHasta.Attributes.Add("onkeypress", "return mascaraData(this);")
        ' Define a mascara do período inicial digitado
        Me.txtFechaDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período inicial digitado
        Me.txtFechaDesdeFinConteo.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaHastaFinConteo.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        'selecionar múltiplos itens em uma ListBox
        listCanal.SelectionMode = ListSelectionMode.Multiple

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Private Sub CarregarControles()

        ' carregar delegações da coleção para o combo
        Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

        ' Inicializa o controle de delegação com a mesma delegação selecionada na tela de login
        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)

        ' Carrega os dados do controle de formato de saída
        CarregarFormatoSalida()

    End Sub

    ''' <summary>
    ''' Carrega os dados de entrada da opção formato de saida
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Private Sub CarregarFormatoSalida()

        rblFormatoSaida.Items.Clear()

        rblFormatoSaida.Items.Add(New ListItem(Traduzir("lbl_formato_salida_csv"), ContractoServ.Enumeradores.eFormatoSalida.CSV))
        rblFormatoSaida.Items.Add(New ListItem(Traduzir("lbl_formato_salida_pdf"), ContractoServ.Enumeradores.eFormatoSalida.PDF))

        rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' </history>
    Private Sub ValidarControles()

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_delegacion")))
        End If

        ' Verifica se o cliente foi preenchido
        If Clientes Is Nothing OrElse Clientes.Count = 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("003_lbl_cliente")))
        End If

        ' Verifica se a data de transporte inicial foi preenchida 
        If (txtFechaTransporteDesde.Text = String.Empty AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty) AndAlso _
           (txtFechaDesdeFinConteo.Text = String.Empty OrElse txtFechaHastaFinConteo.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de transporte final foi preenchida
        If (txtFechaTransporteHasta.Text = String.Empty AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty) AndAlso _
           (txtFechaDesdeFinConteo.Text = String.Empty OrElse txtFechaHastaFinConteo.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de processo inicial foi preenchida
        If txtFechaDesde.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) AndAlso _
           (txtFechaDesdeFinConteo.Text = String.Empty OrElse txtFechaHastaFinConteo.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de processo final foi preenchida
        If txtFechaHasta.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) AndAlso _
           (txtFechaDesdeFinConteo.Text = String.Empty OrElse txtFechaHastaFinConteo.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de contagem inicial foi preenchida
        If txtFechaDesdeFinConteo.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de contagem final foi preenchida
        If txtFechaHastaFinConteo.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de transporte inicial é uma data válida
        If txtFechaTransporteDesde.Text <> String.Empty AndAlso (txtFechaTransporteDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de transporte final é uma data válida
        If txtFechaTransporteHasta.Text <> String.Empty AndAlso (txtFechaTransporteHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de processo inicial é uma data válida
        If txtFechaDesde.Text <> String.Empty AndAlso (txtFechaDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaDesde.Text = txtFechaDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de processo final é uma data válida
        If txtFechaHasta.Text <> String.Empty AndAlso (txtFechaHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo final não foi informada
            ' coloca a hora final do dia
            txtFechaHasta.Text = txtFechaHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de contagem inicial é uma data válida
        If txtFechaDesdeFinConteo.Text <> String.Empty AndAlso (txtFechaDesdeFinConteo.Text.Length < 10 OrElse Not (IsDate(txtFechaDesdeFinConteo.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaDesdeFinConteo.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de contagem inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaDesdeFinConteo.Text = txtFechaDesdeFinConteo.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de contagem final é uma data válida
        If txtFechaHastaFinConteo.Text <> String.Empty AndAlso (txtFechaHastaFinConteo.Text.Length < 10 OrElse Not (IsDate(txtFechaHastaFinConteo.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaHastaFinConteo.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de contagem final não foi informada
            ' coloca a hora final do dia
            txtFechaHastaFinConteo.Text = txtFechaHastaFinConteo.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de transporte final é maior do que a data inicial
        If (IsDate(txtFechaTransporteDesde.Text) AndAlso IsDate(txtFechaTransporteHasta.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaTransporteDesde.Text), Convert.ToDateTime(txtFechaTransporteHasta.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta"), Traduzir("005_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de processo inicial é maior do que a data de processo inicial
        If (IsDate(txtFechaDesde.Text) AndAlso IsDate(txtFechaHasta.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_hasta"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de contagem inicial é maior do que a data de contagem inicial
        If (IsDate(txtFechaDesdeFinConteo.Text) AndAlso IsDate(txtFechaHastaFinConteo.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaDesdeFinConteo.Text), Convert.ToDateTime(txtFechaHastaFinConteo.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("005_lbl_fecha_fin_conteo") & " " & Traduzir("lbl_hasta"), Traduzir("005_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se somente uma das datas foi informada
        If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty AndAlso _
           (txtFechaDesde.Text <> String.Empty OrElse txtFechaHasta.Text <> String.Empty OrElse _
           txtFechaDesdeFinConteo.Text <> String.Empty OrElse txtFechaHastaFinConteo.Text <> String.Empty)) OrElse _
           (txtFechaDesde.Text <> String.Empty AndAlso txtFechaHasta.Text <> String.Empty AndAlso _
           (txtFechaTransporteDesde.Text <> String.Empty OrElse txtFechaTransporteHasta.Text <> String.Empty OrElse _
           txtFechaDesdeFinConteo.Text <> String.Empty OrElse txtFechaHastaFinConteo.Text <> String.Empty)) OrElse _
           (txtFechaDesdeFinConteo.Text <> String.Empty AndAlso txtFechaHastaFinConteo.Text <> String.Empty AndAlso _
           (txtFechaTransporteDesde.Text <> String.Empty OrElse txtFechaTransporteHasta.Text <> String.Empty OrElse _
           txtFechaDesde.Text <> String.Empty OrElse txtFechaHasta.Text <> String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_ja_informado"), Traduzir("005_lbl_fecha_transporte"), Traduzir("005_lbl_fecha"), Traduzir("005_lbl_fecha_fin_conteo")))
        End If

    End Sub

    ''' <summary>
    ''' Recupera os dados do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' [magnum.oliveira] 27/10/2009 Alterado
    ''' [magnum.oliveira] 04/11/2009 Alterado
    ''' </history>
    Private Sub ConsultarDados()
        Try
            ' Valida os controles usados no filtro
            Me.ValidarControles()

            ' Se não existe erro
            If Valida.Count = 0 Then

                ' Cria uma nova coleção de bultos
                Dim objRespaldoCompleto As New ListadosConteo.ProxyRespaldoCompleto

                ' Define os valores dos filtros da consulta
                Dim objPeticion As New ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Peticion
                objPeticion.CodigoDelegacion = ddlDelegacion.SelectedValue
                objPeticion.CodigoCliente = Clientes.FirstOrDefault().Codigo

                If listCanal.Items IsNot Nothing AndAlso listCanal.Items.Count > 0 Then
                    Dim listaValores As New List(Of String)

                    For x = 0 To listCanal.Items.Count - 1

                        ' Caso o item esteja selecionado
                        If listCanal.Items(x).Selected = True Then

                            If Not listaValores.Contains(listCanal.Items(x).Text.Split(" - ")(0).Trim()) Then
                                ' Inseri item na listaValores
                                listaValores.Add(listCanal.Items(x).Text.Split(" - ")(0).Trim())
                            End If

                        End If

                    Next x
                    objPeticion.Procesos = listaValores
                End If

                objPeticion.FormatoSalida = rblFormatoSaida.SelectedValue

                ' Verifica se a data de tranporte foi preenchida
                If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
                    objPeticion.FechaDesde = Convert.ToDateTime(txtFechaTransporteDesde.Text)
                    objPeticion.FechaHasta = Convert.ToDateTime(txtFechaTransporteHasta.Text)
                    objPeticion.EsFechaProceso = Constantes.TipoFecha.TRANSPORTE
                End If

                ' Verifica se a data do sistema foi preenchida
                If (txtFechaDesde.Text <> String.Empty AndAlso txtFechaHasta.Text <> String.Empty) Then
                    objPeticion.FechaDesde = Convert.ToDateTime(txtFechaDesde.Text)
                    objPeticion.FechaHasta = Convert.ToDateTime(txtFechaHasta.Text).AddSeconds(59)
                    objPeticion.EsFechaProceso = Constantes.TipoFecha.PROCESO
                End If

                ' Verifica se a data da contagem foi preenchida
                If (txtFechaDesdeFinConteo.Text <> String.Empty AndAlso txtFechaHastaFinConteo.Text <> String.Empty) Then
                    objPeticion.FechaDesde = Convert.ToDateTime(txtFechaDesdeFinConteo.Text)
                    objPeticion.FechaHasta = Convert.ToDateTime(txtFechaHastaFinConteo.Text).AddSeconds(59)
                    objPeticion.EsFechaProceso = Constantes.TipoFecha.CONTEO
                End If

                ' Verifica se o checkbox Remesa en Curso está marcado
                If chkRemesasEnCurso.Checked Then

                    objPeticion.EstadoRemesa = Constantes.CONST_ESTADO_REMESSA_EN_CURSO

                End If

                ' Recupera a bilhetagem por sucursal para popular o relatório
                Dim objRespuesta As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.Respuesta = objRespaldoCompleto.ListarRespaldoCompleto(objPeticion)

                ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório
                ' validar retorno do serviço
                Dim msgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                    MyBase.MostraMensagem(msgErro)
                    Exit Sub
                End If


                ' Verifica o formato do relatório e existe dados
                If (rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV AndAlso objRespuesta.RespaldosCompletosCSV IsNot Nothing AndAlso objRespuesta.RespaldosCompletosCSV.Count > 0) Then

                    ' Atribui os dados recuperados para a sessão
                    Session("objRespaldosCompletos") = objRespuesta.RespaldosCompletosCSV

                ElseIf (rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.PDF AndAlso objRespuesta.RespaldoCompletoPDF IsNot Nothing _
                        AndAlso objRespuesta.RespaldoCompletoPDF.Detalles IsNot Nothing AndAlso objRespuesta.RespaldoCompletoPDF.Detalles.Count > 0) Then

                    ' Atribui os dados recuperados para a sessão
                    Session("objRespaldosCompletos") = objRespuesta.RespaldoCompletoPDF

                Else
                    ' Define a mensagem de registros não encontrados
                    Valida.Add(Traduzir("lbl_nenhum_registro_encontrado"))
                    MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                    Exit Sub
                End If

                ' Variável onde será montada o nome do arquivo
                Dim nomeArquivo As String = Util.RecuperarNomeArquivo(ddlDelegacion.SelectedValue, _
                                                                    Clientes.FirstOrDefault().Codigo & " - " & Clientes.FirstOrDefault().Descripcion, _
                                                                    Constantes.TipoInformacaoRelatorio.RESPALDO_COMPLETO, _
                                                                    txtFechaTransporteHasta.Text)

                ' Define os parametros que serão passados para a exibição do relatório
                Dim parametros As String = "?Exibir=" & rblFormatoSaida.SelectedValue & _
                                            "&NomeArquivo=" & nomeArquivo

                ' Verifica se a data da contagem foi informada
                If (txtFechaDesdeFinConteo.Text <> String.Empty AndAlso txtFechaHastaFinConteo.Text <> String.Empty) Then
                    ' Atribui ao parametro, a data final da contagem
                    parametros &= "&DataInicial=" & txtFechaDesdeFinConteo.Text
                    ' Atribui ao parametro a data inicial da contagem
                    parametros &= "&DataFinal=" & txtFechaHastaFinConteo.Text
                    ' Atribui ao parametro o tipo de data selecionado
                    parametros &= "&TipoData=" & Constantes.TipoFecha.CONTEO
                    ' Verifica se a data do processo foi informada
                ElseIf (txtFechaDesde.Text <> String.Empty AndAlso txtFechaHasta.Text <> String.Empty) Then
                    ' Atribui ao parametro a data final do processo
                    parametros &= "&DataInicial=" & txtFechaDesde.Text
                    ' Atribui ao parametro a data inicial do processo
                    parametros &= "&DataFinal=" & txtFechaHasta.Text
                    ' Atribui ao parametro o tipo de data selecionado
                    parametros &= "&TipoData=" & Constantes.TipoFecha.PROCESO
                    ' Verifica se a data do tranporte foi informada
                ElseIf (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
                    ' Atribui ao parametro a data inicial do tranporte
                    parametros &= "&DataInicial=" & txtFechaTransporteDesde.Text
                    ' Atribui ao parametro a data final do transporte
                    parametros &= "&DataFinal=" & txtFechaTransporteHasta.Text
                    ' Atribui ao parametro o tipo de data selecionado
                    parametros &= "&TipoData=" & Constantes.TipoFecha.TRANSPORTE
                End If

                '' Se o Processo foi informado
                'If (txtProceso.Text <> String.Empty) Then
                '    ' Atribui o nome do processo
                '    parametros &= "&Processo=" & txtProceso.Text
                'End If

                ' Se a Delegação foi informado
                If Not String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
                    ' Atribui o nome da delegação
                    parametros &= "&Planta=" & ddlDelegacion.SelectedValue & " - " & ddlDelegacion.SelectedItem.Text
                End If

                ' Verifica se o cliente foi informado
                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    parametros &= "&Cliente=" & Clientes.FirstOrDefault().Codigo & " - " & Clientes.FirstOrDefault().Descripcion
                End If

                ' Chama a página que exibirá o relatório
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('RespaldoCompletoMostrar.aspx" & parametros.Replace("'", "\'") & "'); ", True)

            Else
                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub


#Region "[DEMANDA:4760-REMOVIDO-MÉTODO-ConsomeProcessos]"
    ' <summary>
    ' Consome a sessão da tela de busca processos.
    ' </summary>
    ' <remarks></remarks>
    'Private Sub ConsomeProcessos()

    '    If Session("ProcessosSelecionados") IsNot Nothing Then

    '        ' Define o objeto que recebe os dados dos processos
    '        Dim objProcesos As New List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso)

    '        ' Rebece os dados dos processos selecionados
    '        objProcesos = TryCast(Session("ProcessosSelecionados"), List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso))

    '        ' Se existem processos selecionados
    '        If objProcesos IsNot Nothing Then

    '            ' Adiciona os processos selecionados a propriedade
    '            ProcessosSelecionados = objProcesos

    '            listProceso.Items.Clear()

    '            Dim descricao As String = ""

    '            For Each processo As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso In objProcesos

    '                descricao = processo.DescripcionProceso & " - " & _
    '                            processo.DescripcionCliente & " - " & _
    '                            processo.DescripcionSubcliente & " - " & _
    '                            processo.DescripcionPuntoServicio & " - "

    '                For Each subCanal As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.SubCanal In processo.SubCanales

    '                    descricao += subCanal.DescripcionCanal & " - " & _
    '                                 subCanal.DescripcionSubCanal

    '                    If listProceso.Items.FindByValue(processo.IdentificadorProceso) Is Nothing Then
    '                        listProceso.Items.Add(New ListItem(processo.DescripcionProceso, processo.IdentificadorProceso))
    '                    End If

    '                Next

    '            Next

    '        End If

    '    End If

    'End Sub
#End Region

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherListBoxCanal(codCliente As String)

        ' lista temporária
        Dim listaTemp As New ListBox()

        ' Define o objeto que recebe os dados dos processos
        Dim objProcesos As New List(Of IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Proceso)

        Dim objRespuesta As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta = GetProcesos(codCliente)


        ' percorre procesos
        For Each proceso In objRespuesta.Procesos

            For Each subCanal In proceso.SubCanales
                If proceso IsNot Nothing Then
                    ' adiciona item na lista de proceso
                    objProcesos.Add(proceso)
                End If
                ' adiciona item na lista
                listaTemp.Items.Add(New ListItem(subCanal.CodigoCanal & " - " & subCanal.DescripcionCanal))
            Next

        Next

        ' atribui lista de processos a propriedade
        'ListaProcesos = objProcesos

        ' limpa os itens do objeto
        listCanal.Items.Clear()

        ' caso possui itens
        If listaTemp.Items IsNot Nothing AndAlso listaTemp.Items.Count > 0 Then
            ' armazena listaTemp em listCanal
            listCanal.DataSource = (From p In listaTemp.Items Select p).Distinct()
            listCanal.DataBind()
        End If

        ' atribui nulo para o objeto
        listaTemp = Nothing

    End Sub

    ''' <summary>
    ''' Busca procesos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProcesos(codCliente As String) As IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New IAC.Integracion.ContractoServicio.GetProcesosPorDelegacion.Peticion
        objPeticion.EstadoVigencia = True
        objPeticion.CodigoDelegacion = Me.ddlDelegacion.SelectedValue
        objPeticion.CodigoCliente = codCliente

        ' criar objeto proxy
        Dim objProxy As New ProxyIacIntegracion

        ' chamar servicio
        Return objProxy.GetProcesosPorDelegacion(objPeticion)
    End Function

#End Region

#Region "[EVENTOS]"

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Me.ConsultarDados()
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Me.LimparCampos()
        Me.Response.Redireccionar(Me.ResolveUrl("BusquedaRespaldoCompleto.aspx"))
    End Sub



#Region "[DEMANDA:4760-REMOVIDO-EVENTO-btnProceso_Click]"
    ' <summary>
    ' Ação ao clicar no botão buscar processo
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    ' <remarks></remarks>
    'Protected Sub btnProceso_Click(sender As Object, e As EventArgs) Handles btnProceso.Click

    '    Try

    '        Session("DelegacionSelecionada") = Me.ddlDelegacion.SelectedItem

    '        ' Define a url que será aberta
    '        Dim url As String = "BusquedaProcesos.aspx?acao=" & Enumeradores.eAcao.Consulta

    '        'AbrirPopupModal
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_procesos", "AbrirPopupModal('" & url & "', 550, 788);", True)

    '    Catch ex As Exception
    '        Master.ControleErro.TratarErroException(ex)
    '    End Try

    'End Sub

#End Region

#End Region
#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.PtoServicioHabilitado = False
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
            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                PreencherListBoxCanal(Clientes.FirstOrDefault().Codigo)
            Else
                listCanal.Items.Clear()
            End If
        Catch ex As Exception
            MyBase.MostraMensagem(ex.ToString())
        End Try
    End Sub
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Exception, e.Erro.Message, e.Erro.ToString()))
    End Sub
#End Region
End Class