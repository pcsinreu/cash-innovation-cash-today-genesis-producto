Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Partial Public Class BusquedaContadoPorPuesto
    Inherits Base

#Region "[VARIÁVEIS]"

    Dim Valida As New List(Of String)

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Os dados do relatório são armazenados numa sessão
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DadosRelatorio() As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto)
        Get

            If Session("objContadoPuesto") Is Nothing Then
                Return Nothing
            Else
                Return DirectCast(Session("objContadoPuesto"), List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))
            End If

        End Get
        Set(value As List(Of ContractoServ.ContadoPuesto.ListarContadoPuesto.ContadoPuesto))

            Session("objContadoPuesto") = value

        End Set
    End Property

    Public Property DelegacaoEcolhida() As String
        Get
            Return CType(Session("DelegacaoEcolhida"), String)
        End Get
        Set(value As String)
            Session("DelegacaoEcolhida") = value
        End Set
    End Property


    ''' <summary>
    ''' Peticao utilizada na consulta do relatório
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 20/08/2009 Criado
    ''' </history>
    Private Property Peticion() As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion
        Get
            Return ViewState("objPeticion")
        End Get
        Set(value As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion)
            ViewState("objPeticion") = value
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

    Private WithEvents _ucPuesto As ucPuesto
    Public Property ucPuesto() As ucPuesto
        Get
            If _ucPuesto Is Nothing Then
                _ucPuesto = LoadControl(ResolveUrl("~\Controles\Helpers\ucPuesto.ascx"))
                _ucPuesto.ID = Me.ID & "_ucPuestos"
                AddHandler _ucPuesto.Erro, AddressOf ErroControles
                phPuesto.Controls.Add(_ucPuesto)
            End If
            Return _ucPuesto
        End Get
        Set(value As ucPuesto)
            _ucPuesto = value
        End Set
    End Property

    Public Property Puestos As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Puesto)
        Get
            Return ucPuesto.Puestos
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Puesto))
            ucPuesto.Puestos = value
        End Set
    End Property

    Private WithEvents _ucOperario As ucOperario
    Public Property ucOperario() As ucOperario
        Get
            If _ucOperario Is Nothing Then
                _ucOperario = LoadControl(ResolveUrl("~\Controles\Helpers\ucOperario.ascx"))
                _ucOperario.ID = Me.ID & "_ucOperario"
                AddHandler _ucPuesto.Erro, AddressOf ErroControles
                phOperario.Controls.Add(_ucOperario)
            End If
            Return _ucOperario
        End Get
        Set(value As ucOperario)
            _ucOperario = value
        End Set
    End Property

    Public Property Operarios As ContractoServ.GetUsuariosDetail.UsuarioColeccion
        Get
            Return ucOperario.Operarios
        End Get
        Set(value As ContractoServ.GetUsuariosDetail.UsuarioColeccion)
            ucOperario.Operarios = value
        End Set
    End Property

#End Region

#Region "[METODOS BASE]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
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
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtFechaDesde.Enabled = True
                Me.txtFechaHasta.Enabled = True
                Me.txtFechaTransporteDesde.Enabled = True
                Me.txtFechaTransporteHasta.Enabled = True
                Me.txtHoraFin.Enabled = True
                Me.txtHoraInicio.Enabled = True
                Me.txtNumPrecinto.Enabled = True
                Me.txtNumRemesa.Enabled = True
                Me.chkConDenominacion.Enabled = True
                Me.chkConIncidencia.Enabled = True
                Me.rblFormatoSaida.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()
        Me.DefinirRetornoFoco(btnLimpar, ddlDelegacion)
    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta

        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.CONTADO_PUESTO

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
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()
        ASPxGridView.RegisterBaseScript(Page)
        Try
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("008_titulo_pagina")

            ConfigurarControle_Cliente()
            ConfigurarControle_Puesto()
            ConfigurarControle_Operario()

            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                ' Carrega os dados iniciais dos controles
                CarregarControles()

                ' limpa campos
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
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaTransporteDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaTransporteHasta.ClientID, "true", 2)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                      txtFechaHasta.ClientID, "true", 2)

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
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("008_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("008_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("008_lbl_delegacion")
        Me.lblFecha.Text = Traduzir("008_lbl_fecha")
        Me.lblFechaDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaHasta.Text = Traduzir("lbl_hasta")
        Me.lblFechaTransporte.Text = Traduzir("008_lbl_fecha_transporte")
        Me.lblFechaTransporteDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaTransporteHasta.Text = Traduzir("lbl_hasta")
        Me.lblFechaConteo.Text = Traduzir("008_lbl_fecha_conteo")
        Me.lblFechaConteoDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaConteoHasta.Text = Traduzir("lbl_hasta")
        Me.lblHora.Text = Traduzir("008_lbl_hora")
        Me.lblHoraInicio.Text = Traduzir("008_lbl_inicio")
        Me.lblHoraFin.Text = Traduzir("008_lbl_finalizacion")
        Me.lblNumRemesa.Text = Traduzir("008_lbl_numero_remesa")
        Me.lblNumPrecinto.Text = Traduzir("008_lbl_numero_precinto")
        Me.lblConDenominacion.Text = Traduzir("008_lbl_con_denominacion")
        Me.lblConIncidencia.Text = Traduzir("008_lbl_con_incidencia")
        Me.lblFormatoSaida.Text = Traduzir("008_lbl_formato")
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
    ''' [bruno.costa] 12/08/2009 Criado
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
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Private Sub LimparCampos()

        ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        txtFechaDesde.Text = String.Empty
        txtFechaHasta.Text = String.Empty
        txtFechaTransporteDesde.Text = String.Empty
        txtFechaTransporteHasta.Text = String.Empty
        txtFechaConteoDesde.Text = String.Empty
        txtFechaConteoHasta.Text = String.Empty
        txtHoraInicio.Text = String.Empty
        txtHoraFin.Text = String.Empty
        txtNumRemesa.Text = String.Empty
        txtNumPrecinto.Text = String.Empty

        chkConDenominacion.Checked = False
        chkConIncidencia.Checked = False
        rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV

        Clientes = Nothing
        Puestos = Nothing
        Operarios = Nothing

    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()

        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        ' Define a mascara do período inicial digitado
        Me.txtFechaDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara do período final digitado
        Me.txtFechaHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara do período inicial digitado
        Me.txtFechaTransporteDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara do período final digitado
        Me.txtFechaTransporteHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara do período inicial digitado
        Me.txtFechaConteoDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara do período final digitado
        Me.txtFechaConteoHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")

        ' Define a mascara da hora inicial digitada
        Me.txtHoraInicio.Attributes.Add("onkeypress", "return mask(true, event, this, '##:##:##');")

        ' Define a mascara da hora final digitada
        Me.txtHoraFin.Attributes.Add("onkeypress", "return mask(true, event, this, '##:##:##');")

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Private Sub CarregarControles()

        ' carregar delegações do xml para o combo
        Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

        ' Inicializa o controle de delegação com a mesma delegação selecionada na tela de login
        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)

        DelegacaoEcolhida = MyBase.DelegacionConectada.Keys(0)
        ' Carrega os dados do controle de formato de saída
        CarregarFormatoSalida()

    End Sub

    ''' <summary>
    ''' Carrega os dados de entrada da opção formato de saida
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Private Sub CarregarFormatoSalida()

        rblFormatoSaida.Items.Clear()

        rblFormatoSaida.Items.Add(New ListItem(Traduzir("lbl_formato_salida_csv"), ContractoServ.Enumeradores.eFormatoSalida.CSV))
        rblFormatoSaida.Items.Add(New ListItem(Traduzir("lbl_formato_salida_pdf"), ContractoServ.Enumeradores.eFormatoSalida.PDF))

        rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles delecacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 02/10/2012 Criado
    ''' </history>
    Private Sub ValidarControleDelegacion()

        ' Verifica se a delegação foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_delegacion")))

            ' Mostra as mensagens de erro 
            MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
            UpdatePanelGeral.Update()
        End If

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 12/08/2009 Criado
    ''' </history>
    Private Sub ValidarControles()

        ' Verifica se a delegação foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_delegacion")))
        End If

        ' verifica se a data de processo foi informada
        Dim informouFecha As Boolean = Not String.IsNullOrEmpty(txtFechaDesde.Text) OrElse _
            Not String.IsNullOrEmpty(txtFechaHasta.Text)

        ' verifica se a data de transporte foi informada
        Dim informouFechaTransporte As Boolean = Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) OrElse _
        Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text)

        ' verifica se a data de transporte foi informada
        Dim informouFechaConteo As Boolean = Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) OrElse _
        Not String.IsNullOrEmpty(txtFechaConteoHasta.Text)

        ' verifica se os campos puesto ou operário foram preenchidos.
        Dim informouPuestoOperario As Boolean = (Operarios IsNot Nothing AndAlso Operarios.Count > 0) OrElse (Puestos IsNot Nothing AndAlso Puestos.Count > 0)

        ' verifica se a data de transporte foi informada
        Dim informouHora As Boolean = Not String.IsNullOrEmpty(txtHoraInicio.Text) OrElse _
        Not String.IsNullOrEmpty(txtHoraFin.Text)

        If Not informouFecha AndAlso Not informouFechaTransporte AndAlso Not informouFechaConteo Then
            ' usuário deve preencher um dos campos: Fecha, Fecha transporte ou Fecha conteo
            Valida.Add(String.Format(Traduzir("008_msg_datas"), Traduzir("008_lbl_fecha"), Traduzir("008_lbl_fecha_transporte"), Traduzir("008_lbl_fecha_conteo")))

        ElseIf (informouFecha AndAlso informouFechaTransporte) OrElse (informouFecha AndAlso informouFechaConteo) OrElse (informouFechaTransporte AndAlso informouFechaConteo) Then
            ' usuário deve preencher apenas um dos campos: Fecha, Fecha transporte ou Fecha conteo
            Valida.Add(String.Format(Traduzir("008_msg_uma_data"), Traduzir("008_lbl_fecha"), Traduzir("008_lbl_fecha_transporte"), Traduzir("008_lbl_fecha_conteo")))
        End If

        ValidarFecha(informouFecha)

        ValidarFechaTransporte(informouFechaTransporte)

        ValidarFechaConteo(informouFechaConteo)

        ValidarHora()

        ' Usuário deve preencher pelo menos um dos campos: posto, operário, num. remesa, num. precinto, cliente e subcliente 
        ' ou o intervalo das datas devem ser inferior a 3 dias
        Dim ClienteInformado As Boolean = Clientes IsNot Nothing AndAlso Clientes.Count > 0
        Dim SubClienteInformado As Boolean = False
        If ClienteInformado Then
            SubClienteInformado = Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0
        End If
        Dim PuestoInformado As Boolean = Puestos IsNot Nothing AndAlso Puestos.Count > 0
        Dim OperarioInformado As Boolean = Operarios IsNot Nothing AndAlso Operarios.Count > 0

        'TODO: validar cliente e subcliente
        If Not PuestoInformado AndAlso _
        Not OperarioInformado AndAlso _
        String.IsNullOrEmpty(txtNumRemesa.Text) AndAlso _
        String.IsNullOrEmpty(txtNumPrecinto.Text) AndAlso _
        Not ClienteInformado AndAlso Not SubClienteInformado AndAlso _
        Not (informouFecha OrElse informouFechaTransporte OrElse informouFechaConteo) Then

            ' Verifica se posto foi preenchido
            If Not PuestoInformado Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_puesto")))
            End If

            ' Verifica se operário foi preenchido
            If Not OperarioInformado Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_operario")))
            End If

            If String.IsNullOrEmpty(txtNumRemesa.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_numero_remesa")))
            End If

            If String.IsNullOrEmpty(txtNumPrecinto.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_numero_precinto")))
            End If

            If Not ClienteInformado Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_cliente")))
            End If

            If Not SubClienteInformado Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_subcliente")))
            End If

        ElseIf Not PuestoInformado AndAlso _
        Not OperarioInformado AndAlso _
        String.IsNullOrEmpty(txtNumRemesa.Text) AndAlso _
        String.IsNullOrEmpty(txtNumPrecinto.Text) AndAlso
         Not ClienteInformado AndAlso Not SubClienteInformado AndAlso _
        (informouFecha OrElse informouFechaTransporte OrElse informouFechaConteo) Then
            ' verifica se as datas possuem intervalo maior que 3 dias.
            Dim dias As TimeSpan
            Dim textLblFecha As String = String.Empty
            Dim data As DateTime = DateTime.MinValue
            If informouFecha Then
                If Date.TryParse(txtFechaDesde.Text, data) Then
                    If DateTime.TryParse(txtFechaHasta.Text, data) Then
                        dias = Date.Parse(txtFechaDesde.Text) - Date.Parse(txtFechaHasta.Text)
                        textLblFecha = Traduzir("008_lbl_fecha")
                    End If
                End If
            ElseIf informouFechaTransporte Then
                If Date.TryParse(txtFechaTransporteDesde.Text, data) Then
                    If DateTime.TryParse(txtFechaTransporteHasta.Text, data) Then
                        dias = Date.Parse(txtFechaTransporteDesde.Text) - Date.Parse(txtFechaTransporteHasta.Text)
                        textLblFecha = Traduzir("008_lbl_fecha_transporte")
                    End If
                End If
            ElseIf informouFechaConteo Then

                If Date.TryParse(txtFechaConteoDesde.Text, data) Then
                    If DateTime.TryParse(txtFechaTransporteHasta.Text, data) Then
                        dias = Date.Parse(txtFechaConteoDesde.Text) - Date.Parse(txtFechaConteoHasta.Text)
                        textLblFecha = Traduzir("008_lbl_fecha_conteo")
                    End If
                End If

            End If

            If Math.Abs(dias.Days) > 3 Then
                Valida.Add(String.Format(Traduzir("008_msg_soloamete_fecha_3dias"), Traduzir("008_lbl_delegacion"), textLblFecha, Traduzir("008_lbl_hora")))
            End If
        End If

        If informouPuestoOperario AndAlso Not informouHora Then
            Valida.Add(String.Format(Traduzir("008_msg_OperarioPuestoHora"), Traduzir("008_lbl_puesto"), Traduzir("008_lbl_operario"), Traduzir("008_lbl_hora")))
        End If
    End Sub

    Private Sub ValidarHora()

        ' hora é obrigatorio se os campos posto ou operário estão preenchidos.
        If (Puestos IsNot Nothing AndAlso Puestos.Count > 0) OrElse _
        (Operarios IsNot Nothing AndAlso Operarios.Count > 0) Then

            ' Verifica se a hora inicio foi preenchida
            If String.IsNullOrEmpty(txtHoraInicio.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_inicio")))
            End If

            ' Verifica se a hora fim foi preenchida
            If String.IsNullOrEmpty(txtHoraFin.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_finalizacion")))
            End If

        End If

        '' se preencheu data inicio, data fim é obrigatório
        'If Not String.IsNullOrEmpty(txtHoraInicio.Text) AndAlso String.IsNullOrEmpty(txtHoraFin.Text) Then
        '    Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_finalizacion")))
        '    Me.csvHoraFin.IsValid = False
        'End If

        '' se preencheu data fim, data início é obrigatório
        'If Not String.IsNullOrEmpty(txtHoraFin.Text) AndAlso String.IsNullOrEmpty(txtHoraInicio.Text) Then
        '    Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_inicio")))
        '    Me.csvHoraInicio.IsValid = False
        'End If

        ' se informou hora início, valida
        If Not String.IsNullOrEmpty(txtHoraInicio.Text) Then

            ' Verifica se a hora inicio é válida
            If Not ValidarHora(txtHoraInicio.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_hora_invalida"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_inicio")))
            End If

        End If

        ' se informou hora fim, valida
        If Not String.IsNullOrEmpty(txtHoraFin.Text) Then

            ' Verifica se a hora fim é válida
            If Not ValidarHora(txtHoraFin.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_hora_invalida"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_finalizacion")))
            End If

        End If

        'If Not String.IsNullOrEmpty(txtHoraInicio.Text) AndAlso Not String.IsNullOrEmpty(txtHoraFin.Text) _
        'AndAlso csvHoraInicio.IsValid AndAlso csvHoraFin.IsValid Then

        '    ' verifica se hora início é < que hora fim
        '    If Not ValidarIntervaloHoras(txtHoraInicio.Text, txtHoraFin.Text) Then

        '        Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_finalizacion"), Traduzir("008_lbl_hora") & " " & Traduzir("008_lbl_inicio")))

        '    End If

        'End If

    End Sub

    Private Sub ValidarFecha(InformouFecha As Boolean)

        If InformouFecha Then

            ' se não informou a parte da hora, completa
            If txtFechaDesde.Text.Length = 10 Then
                txtFechaDesde.Text = txtFechaDesde.Text.TrimEnd & " 00:00:00"
            End If

            ' se não informou a parte da hora, completa
            If txtFechaHasta.Text.Length = 10 Then
                txtFechaHasta.Text = txtFechaHasta.Text.TrimEnd & " 23:59:59"
            End If

            ' é obrigatório informa fecha desde e fecha hasta
            If String.IsNullOrEmpty(txtFechaDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_desde")))
            End If

            If String.IsNullOrEmpty(txtFechaHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' se informou data início, valida
        If Not String.IsNullOrEmpty(txtFechaDesde.Text) Then

            ' Verifica se a data de processo inicial é uma data válida
            If Not ValidarDataHora(txtFechaDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_desde")))
            End If

        End If

        ' se informou data fim, valida
        If Not String.IsNullOrEmpty(txtFechaHasta.Text) Then

            ' Verifica se a data de processo final é uma data válida
            If Not ValidarDataHora(txtFechaHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' valida se data inicio é <= do que a data fim
        If (Not String.IsNullOrEmpty(txtFechaDesde.Text) AndAlso _
        Not String.IsNullOrEmpty(txtFechaHasta.Text)) AndAlso _
        Not ValidarIntervaloDatas(txtFechaDesde.Text, txtFechaHasta.Text) Then

            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_hasta"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_desde")))

        End If

    End Sub

    Private Sub ValidarFechaTransporte(InformouFechaTransporte As Boolean)

        If InformouFechaTransporte Then

            ' se não informou a parte da hora, completa
            If txtFechaTransporteDesde.Text.Length = 10 Then
                txtFechaTransporteDesde.Text = txtFechaTransporteDesde.Text.TrimEnd & " 00:00:00"
            End If

            ' se não informou a parte da hora, completa
            If txtFechaTransporteHasta.Text.Length = 10 Then
                txtFechaTransporteHasta.Text = txtFechaTransporteHasta.Text.TrimEnd & " 23:59:59"
            End If

            ' é obrigatório informa fecha transporte desde e fecha transporte hasta
            If String.IsNullOrEmpty(txtFechaTransporteDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
            End If

            If String.IsNullOrEmpty(txtFechaTransporteHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' se informou data de transporte inicial, valida
        If Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) Then

            ' Verifica se a data de transporte inicial é uma data válida
            If Not ValidarDataHora(txtFechaTransporteDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
            End If

        End If

        ' se informou data de transpor final, valida
        If Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text) Then

            ' Verifica se a data de processo final é uma data válida
            If Not ValidarDataHora(txtFechaTransporteHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' valida se data de transporte inicio é <= do que a data fim
        If (Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) AndAlso _
        Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text)) AndAlso _
        Not ValidarIntervaloDatas(txtFechaTransporteDesde.Text, txtFechaTransporteHasta.Text) Then

            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_hasta"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_desde")))

        End If

    End Sub

    Private Sub ValidarFechaConteo(InformouFechaConteo As Boolean)

        If InformouFechaConteo Then

            ' se não informou a parte da hora, completa
            If txtFechaConteoDesde.Text.Length = 10 Then
                txtFechaConteoDesde.Text = txtFechaConteoDesde.Text.TrimEnd & " 00:00:00"
            End If

            ' se não informou a parte da hora, completa
            If txtFechaConteoHasta.Text.Length = 10 Then
                txtFechaConteoHasta.Text = txtFechaConteoHasta.Text.TrimEnd & " 23:59:59"
            End If

            ' é obrigatório informa fecha transporte desde e fecha transporte hasta
            If String.IsNullOrEmpty(txtFechaConteoDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
            End If

            If String.IsNullOrEmpty(txtFechaConteoHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("008_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' se informou data de transporte inicial, valida
        If Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) Then

            ' Verifica se a data de processo inicial é uma data válida
            If Not ValidarDataHora(txtFechaConteoDesde.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
            End If

        End If

        ' se informou data de transpor final, valida
        If Not String.IsNullOrEmpty(txtFechaConteoHasta.Text) Then

            ' Verifica se a data de processo final é uma data válida
            If Not ValidarDataHora(txtFechaConteoHasta.Text) Then
                Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("008_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta")))
            End If

        End If

        ' valida se data de transporte inicio é <= do que a data fim
        If (Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) AndAlso _
        Not String.IsNullOrEmpty(txtFechaConteoHasta.Text)) AndAlso _
        Not ValidarIntervaloDatas(txtFechaConteoDesde.Text, txtFechaConteoHasta.Text) Then

            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_hasta"), Traduzir("008_lbl_fecha") & " " & Traduzir("lbl_desde")))

        End If

    End Sub

    ''' <summary>
    ''' Valida intervalo de datas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/08/2010  criado
    ''' </history>
    Private Function ValidarIntervaloDatas(DataInicio As String, DataFim As String) As Boolean

        ' Verifica se a data de transporte final é maior do que a data inicial
        If (IsDate(DataInicio) AndAlso IsDate(DataFim)) AndAlso _
        Date.Compare(Convert.ToDateTime(DataInicio), Convert.ToDateTime(DataFim)) > 0 Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' valida se a data é uma data válida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/08/2010  criado
    ''' </history>
    Private Function ValidarData(Valor As String) As Boolean

        If Not String.IsNullOrEmpty(Valor) AndAlso (Valor.Length <> 10 OrElse Not IsDate(Valor)) Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' valida se a data é uma data válida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/08/2010  criado
    ''' </history>
    Private Function ValidarDataHora(Valor As String) As Boolean

        If Not String.IsNullOrEmpty(Valor) AndAlso (Valor.Length <> 19 OrElse Not IsDate(Valor)) Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' valida intervalo de horas
    ''' </summary>
    ''' <param name="HoraInicio"></param>
    ''' <param name="HoraFim"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/08/2010  criado
    ''' </history>
    Private Function ValidarIntervaloHoras(HoraInicio As String, HoraFim As String) As Boolean

        Try

            Dim arrHoraInicio As String() = HoraInicio.Split(":")
            Dim arrHoraFim As String() = HoraFim.Split(":")

            Dim dtHoraInicio As New DateTime(Date.MinValue.Year, Date.MinValue.Month, Date.MinValue.Day, _
                                             arrHoraInicio(0), arrHoraInicio(1), arrHoraInicio(2))

            Dim dtHoraFim As New DateTime(Date.MinValue.Year, Date.MinValue.Month, Date.MinValue.Day, _
                                             arrHoraFim(0), arrHoraFim(1), arrHoraFim(2))

            If DateTime.Compare(dtHoraInicio, dtHoraFim) > 0 Then

                Return False

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

        Return True

    End Function

    ''' <summary>
    ''' valida se a data é uma data válida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  12/08/2010  criado
    ''' </history>
    Private Function ValidarHora(Valor As String) As Boolean

        If Not String.IsNullOrEmpty(Valor) Then

            If Valor.Length <> 8 Then

                Return False

            End If

            Try

                Dim arr As String() = Valor.Split(":")

                If arr.Count <> 3 Then
                    Return False
                End If

                Dim hora As Integer = arr(0)
                Dim minutos As Integer = arr(1)
                Dim segundos As Integer = arr(2)

                If hora >= 24 OrElse minutos >= 60 OrElse segundos >= 60 Then
                    Return False
                End If

            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
                Return False

            End Try

        End If

        Return True

    End Function

    ''' <summary>
    ''' Recupera os dados do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Sub ConsultarDados()

        ' Valida os controles usados no filtro
        Me.ValidarControles()

        ' Se não existe erro
        If Valida.Count = 0 Then

            ' realiza consulta
            Dim respuesta As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta = RealizarBuscaDados()

            ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório
            Dim strMsgErro As String = String.Empty
            If Not Master.ControleErro.VerificaErro2(respuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, respuesta.MensajeError, True) Then
                ' se ocorreu algum erro, traba o erro e finaliza
                Me.MostraMensagem(strMsgErro)
                Exit Sub
            End If

            ' Verifica o formato do relatório e se existe dados
            If rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV AndAlso _
               (respuesta.ContadoPuestoCSV IsNot Nothing AndAlso respuesta.ContadoPuestoCSV.Count > 0) Then

                ' armazena dados do relatório
                DadosRelatorio = respuesta.ContadoPuestoCSV

                ' exibe relatório
                ExibirRelatorio()

            ElseIf rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.PDF AndAlso _
               (respuesta.ContadoPuestoPDF IsNot Nothing AndAlso respuesta.ContadoPuestoPDF.Count > 0) Then

                ' Atribui os dados recuperados para a sessão
                DadosRelatorio = respuesta.ContadoPuestoPDF

                ' exibe relatório
                ExibirRelatorio()

            Else

                ' Define a mensagem de registros não encontrados
                Valida.Add(Traduzir("lbl_nenhum_registro_encontrado"))
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                Exit Sub

            End If

        Else

            ' Mostra as mensagens de erro 
            MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))

        End If

    End Sub

    ''' <summary>
    ''' Realiza a consulta dos dados para popular o relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <returns></returns>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Function RealizarBuscaDados() As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta

        ' Cria proxy para acessar webservice
        Dim proxyWS As New ListadosConteo.ProxyContadoPuesto

        ' Define os valores dos filtros da consulta
        Dim peticion As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion = PreencherPeticion()

        ' realiza consulta via WS
        Dim respuesta As ContractoServ.ContadoPuesto.ListarContadoPuesto.Respuesta = proxyWS.ListarContadoPuesto(peticion)

        Return respuesta

    End Function

    Private Function PreencherPeticion() As ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion

        ' Define os valores dos filtros da consulta
        Dim peticion As New ContractoServ.ContadoPuesto.ListarContadoPuesto.Peticion

        With peticion

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                .CodigoCliente = Clientes.FirstOrDefault().Codigo
                If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                    .CodSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                End If
            End If

            .BolIncidencia = chkConIncidencia.Checked
            .CodigoDelegacion = ddlDelegacion.SelectedValue
            .FormatoSalida = rblFormatoSaida.SelectedValue
            .HoraFin = txtHoraFin.Text
            .HoraInicio = txtHoraInicio.Text
            .NumPrecinto = txtNumPrecinto.Text
            .NumRemesa = txtNumRemesa.Text

            If Operarios IsNot Nothing AndAlso Operarios.Count > 0 Then
                .CodOperario = Operarios.FirstOrDefault().Login
            End If
            If Puestos IsNot Nothing AndAlso Puestos.Count > 0 Then
                .CodPuesto = Puestos.FirstOrDefault().Codigo
            End If

            ' Define filtro data
            If Not String.IsNullOrEmpty(txtFechaDesde.Text) Then
                ' usuário preencheu fecha de processo
                .TipoFecha = Constantes.TipoFecha.PROCESO
                .FechaDesde = txtFechaDesde.Text
                .FechaHasta = txtFechaHasta.Text
            ElseIf Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) Then
                ' usuário preencheu fecha de transporte
                .TipoFecha = Constantes.TipoFecha.TRANSPORTE
                .FechaDesde = txtFechaTransporteDesde.Text
                .FechaHasta = txtFechaTransporteHasta.Text
            ElseIf Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) Then
                ' usuário preencheu fecha de transporte
                .TipoFecha = Constantes.TipoFecha.CONTEO
                .FechaDesde = txtFechaConteoDesde.Text
                .FechaHasta = txtFechaConteoHasta.Text
            End If

        End With

        ' armazena numa viewstate
        Me.Peticion = peticion

        Return peticion

    End Function


    ''' <summary>
    ''' Chama form que trata a exibição do relatório, de acordo com o formato selecionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/08/2009 Criado
    ''' </history>
    Private Sub ExibirRelatorio()

        Dim codCliente As String = If(Clientes.Count = 0, String.Empty, Clientes.FirstOrDefault().Codigo)
        Dim desCliente As String = If(Clientes.Count = 0, String.Empty, Clientes.FirstOrDefault().Descripcion)
        ' Variável onde será montada o nome do arquivo
        Dim nomeArquivo As String = Util.RecuperarNomeArquivo(ddlDelegacion.SelectedValue, _
                                                         codCliente & " - " & desCliente, _
                                                            Constantes.TipoInformacaoRelatorio.CONTADO_PUESTO, _
                                                            txtFechaTransporteHasta.Text)

        Dim conIncidencias As String = chkConIncidencia.Checked.ToString()

        Dim conDenominacion As String = chkConDenominacion.Checked.ToString()

        ' Define os parametros que serão passados para a exibição do relatório
        Dim parametros As String = "?Exibir=" & rblFormatoSaida.SelectedValue & _
                                    "&NomeArquivo=" & nomeArquivo


        ' envia atributos do popup
        Session("AtributosContadoPuesto") = PreencherAtributosPopup()

        ' Chama a página que exibirá o relatório
        scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('ContadoPorPuestoMostrar.aspx" & parametros.Replace("'", "\'") & "');", True)

    End Sub

    Private Function PreencherAtributosPopup() As ContadoPorPuestoMostrar.Atributos

        Dim atributosForm As New ContadoPorPuestoMostrar.Atributos

        With atributosForm

            .ConDenominacion = chkConDenominacion.Checked
            .ConIncidencias = chkConIncidencia.Checked
            .Delegacion = ddlDelegacion.SelectedItem.Value & " - " & ddlDelegacion.SelectedItem.Text
            .HoraIni = Peticion.HoraInicio
            .HoraFin = Peticion.HoraFin
            .NumPrecinto = Peticion.NumPrecinto
            .NumRemesa = Peticion.NumRemesa
            .Operario = Peticion.CodOperario
            .Puesto = Peticion.CodPuesto
            .TipoFecha = Peticion.TipoFecha

            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                .CodCliente = Clientes.FirstOrDefault().Codigo
                .NombreCliente = Clientes.FirstOrDefault().Descripcion
                If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                    .CodSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                    .NombreSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Descripcion
                End If
            End If

            Select Case Peticion.TipoFecha
                Case Constantes.TipoFecha.TRANSPORTE
                    .FechaTransporteIni = Peticion.FechaDesde
                    .FechaTransporteFin = Peticion.FechaHasta
                Case Constantes.TipoFecha.PROCESO
                    .FechaProcesoIni = Peticion.FechaDesde
                    .FechaProcesoFin = Peticion.FechaHasta
            End Select

        End With

        Return atributosForm

    End Function

#End Region

#Region "[EVENTOS]"

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            Me.ConsultarDados()
        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try

            Me.LimparCampos()
            Me.Response.Redireccionar(Me.ResolveUrl("~/BusquedaContadoPorPuesto.aspx"))
        Catch ex As Exception

            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region
#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = True
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

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ConfigurarControle_Puesto()

        Me.ucPuesto.SelecaoMultipla = False
        Me.ucPuesto.PuestoHabilitado = True
        Me.ucPuesto.PuestoObrigatorio = True

        If Puestos IsNot Nothing Then
            Me.ucPuesto.Puestos = Puestos
        End If

    End Sub
    Public Sub ucPuestos_OnControleAtualizado() Handles _ucPuesto.UpdatedControl
        Try
            If ucPuesto.Puestos IsNot Nothing Then
                Puestos = ucPuesto.Puestos
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ConfigurarControle_Operario()

        Me.ucOperario.SelecaoMultipla = False
        Me.ucOperario.OperarioHabilitado = True
        Me.ucOperario.OperarioObrigatorio = True

        If Puestos IsNot Nothing Then
            Me.ucOperario.Operarios = Operarios
        End If

    End Sub
    Public Sub ucOperario_OnControleAtualizado() Handles _ucOperario.UpdatedControl
        Try
            If ucOperario.Operarios IsNot Nothing Then
                Operarios = ucOperario.Operarios
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

    Private Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacion.SelectedIndexChanged
        DelegacaoEcolhida = ddlDelegacion.SelectedValue
    End Sub
End Class