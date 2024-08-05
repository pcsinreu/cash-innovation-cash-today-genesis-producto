Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Partial Public Class BusquedaDetalleParciales
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
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtFechaDesde.Enabled = True
                Me.txtFechaHasta.Enabled = True
                Me.txtFechaTransporteDesde.Enabled = True
                Me.txtFechaTransporteHasta.Enabled = True
                Me.txtNumeroRemesa.Enabled = True
                Me.txtNumeroPrecinto.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()
        Me.DefinirRetornoFoco(btnLimpar, ddlDelegacion)
    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.DETALLE_PARCIALES
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
        ASPxGridView.RegisterBaseScript(Page)
        Try
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("007_titulo_pagina")

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
                                     txtFechaTransporteDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaTransporteHasta.ClientID, "true", 2)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaDesde.ClientID, "true", 1)

            script &= String.Format(" AbrirCalendario('{0}','{1},{2}');", _
                                      txtFechaHasta.ClientID, "true", 2)

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

        Me.Page.Title = Traduzir("007_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("007_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("007_lbl_delegacion")
        Me.lblFecha.Text = Traduzir("007_lbl_fecha")
        Me.lblFechaDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaHasta.Text = Traduzir("lbl_hasta")
        Me.lblFechaTransporte.Text = Traduzir("007_lbl_fecha_transporte")
        Me.lblFechaTransporteDesde.Text = Traduzir("lbl_desde")
        Me.lblFechaTransporteHasta.Text = Traduzir("lbl_hasta")
        Me.lblNumeroRemesa.Text = Traduzir("007_lbl_numero_remesa")
        Me.lblNumeroPrecinto.Text = Traduzir("007_lbl_numero_precinto")
        Me.lblConDenominacion.Text = Traduzir("007_lbl_con_denominacion")
        Me.lblConIncidencia.Text = Traduzir("007_lbl_con_incidencia")
        Me.lblFormatoSaida.Text = Traduzir("007_lbl_formato_salida")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.btnLimpar.Text = Traduzir("btnLimpiar")
        Me.btnBuscar.ToolTip = Traduzir("btnBuscar")
        Me.btnLimpar.ToolTip = Traduzir("btnLimpiar")
    End Sub

#End Region

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

        ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        txtFechaDesde.Text = String.Empty
        txtFechaHasta.Text = String.Empty
        txtFechaTransporteDesde.Text = String.Empty
        txtFechaTransporteHasta.Text = String.Empty
        txtNumeroRemesa.Text = String.Empty
        txtNumeroPrecinto.Text = String.Empty
        chkConDenominacion.Checked = False
        chkConIncidencia.Checked = False
        rblFormatoSaida.SelectedValue = ContractoServ.Enumeradores.eFormatoSalida.CSV

        Session("ClienteSelecionado") = Nothing
        Session("SubClienteSelecionado") = Nothing

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
        Me.txtFechaDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##:##');")
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

        ' Carrega os dados do controle de formato de saída
        CarregarFormatoSalida()

    End Sub

    ''' <summary>
    ''' Carrega os dados de entrada da opção formato de saida
    ''' </summary>
    ''' <remarks></remarks>
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
    Private Sub ValidarControles()

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_delegacion")))
        End If

        ' Verifica se a data de processo inicial foi preenchida
        If txtFechaDesde.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de processo final foi preenchida
        If txtFechaHasta.Text = String.Empty AndAlso _
           (txtFechaTransporteDesde.Text = String.Empty OrElse txtFechaTransporteHasta.Text = String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se a data de transporte inicial foi preenchida 
        If (txtFechaTransporteDesde.Text = String.Empty AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de transporte final foi preenchida
        If (txtFechaTransporteHasta.Text = String.Empty AndAlso _
           (txtFechaDesde.Text = String.Empty OrElse txtFechaHasta.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
        End If

        ' Verifica se o número remessa foi preenchido
        If (txtNumeroRemesa.Text = String.Empty AndAlso _
           (txtNumeroPrecinto.Text = String.Empty AndAlso Clientes.Count = 0)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_numero_remesa")))
        End If

        ' Verifica se o número precinto foi preenchido
        If (txtNumeroPrecinto.Text = String.Empty AndAlso _
           (txtNumeroRemesa.Text = String.Empty AndAlso Clientes.Count = 0)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_numero_precinto")))
        End If

        ' Verifica se o cliente foi preenchido
        If (Clientes.Count = 0 AndAlso _
           (txtNumeroRemesa.Text = String.Empty AndAlso txtNumeroPrecinto.Text = String.Empty)) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("007_lbl_cliente")))
        End If

        ' Verifica se a data de processo inicial é uma data válida
        If txtFechaDesde.Text <> String.Empty AndAlso (txtFechaDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaDesde.Text = txtFechaDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de processo final é uma data válida
        If txtFechaHasta.Text <> String.Empty AndAlso (txtFechaHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo final não foi informada
            ' coloca a hora final do dia
            txtFechaHasta.Text = txtFechaHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de transporte inicial é uma data válida
        If txtFechaTransporteDesde.Text <> String.Empty AndAlso (txtFechaTransporteDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        ElseIf txtFechaDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de transporte inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaTransporteDesde.Text = txtFechaTransporteDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de transporte final é uma data válida
        If txtFechaTransporteHasta.Text <> String.Empty AndAlso (txtFechaTransporteHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaTransporteHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta")))
        ElseIf txtFechaHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de transporte final não foi informada
            ' coloca a hora final do dia
            txtFechaTransporteHasta.Text = txtFechaTransporteHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de processo inicial é maior do que a data de processo inicial
        If (IsDate(txtFechaDesde.Text) AndAlso IsDate(txtFechaHasta.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_hasta"), Traduzir("007_lbl_fecha") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se a data de transporte final é maior do que a data inicial
        If (IsDate(txtFechaTransporteDesde.Text) AndAlso IsDate(txtFechaTransporteHasta.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaTransporteDesde.Text), Convert.ToDateTime(txtFechaTransporteHasta.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_hasta"), Traduzir("007_lbl_fecha_transporte") & " " & Traduzir("lbl_desde")))
        End If

        ' Verifica se somente uma das datas foi informada
        If (txtFechaTransporteDesde.Text <> String.Empty OrElse txtFechaTransporteHasta.Text <> String.Empty) AndAlso _
           (txtFechaDesde.Text <> String.Empty OrElse txtFechaHasta.Text <> String.Empty) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_ja_informado_2"), Traduzir("007_lbl_fecha_transporte"), Traduzir("007_lbl_fecha")))
        End If

    End Sub

    ''' <summary>
    ''' Recupera os dados do relatório
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultarDados()
        Try
            ' Valida os controles usados no filtro
            Me.ValidarControles()

            ' Se não existe erro
            If Valida.Count = 0 Then

                ' Cria uma nova coleção de bultos
                Dim objDetalleParciales As New ListadosConteo.ProxyDetalleParciales

                ' Define os valores dos filtros da consulta
                Dim objPeticion As New ContractoServ.DetalleParciales.GetDetalleParciales.Peticion

                objPeticion.CodigoDelegacion = ddlDelegacion.SelectedValue

                ' Verifica se a data do sistema foi preenchida
                If (txtFechaDesde.Text <> String.Empty AndAlso txtFechaHasta.Text <> String.Empty) Then
                    objPeticion.FechaDesde = Convert.ToDateTime(txtFechaDesde.Text)
                    objPeticion.FechaHasta = Convert.ToDateTime(txtFechaHasta.Text).AddSeconds(59)
                    objPeticion.EsFechaProceso = Constantes.TipoFecha.PROCESO
                End If

                ' Verifica se a data de tranporte foi preenchida
                If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
                    objPeticion.FechaDesde = Convert.ToDateTime(txtFechaTransporteDesde.Text)
                    objPeticion.FechaHasta = Convert.ToDateTime(txtFechaTransporteHasta.Text).AddSeconds(59)
                    objPeticion.EsFechaProceso = Constantes.TipoFecha.TRANSPORTE
                End If

                objPeticion.NumeroRemesa = txtNumeroRemesa.Text
                objPeticion.NumeroPrecinto = txtNumeroPrecinto.Text
                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    objPeticion.CodigoCliente = If(Clientes.Count = 0, String.Empty, Clientes.FirstOrDefault().Codigo)
                    If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                        objPeticion.CodigoSubCliente = If(Clientes.Count = 0, String.Empty, If(Clientes.FirstOrDefault().SubClientes.Count = 0, String.Empty, Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo))
                    Else
                        objPeticion.CodigoSubCliente = String.Empty
                    End If
                Else
                    objPeticion.CodigoCliente = String.Empty
                    objPeticion.CodigoSubCliente = String.Empty
                End If

                objPeticion.ConDenominacion = IIf(chkConDenominacion.Checked, 1, 0)
                objPeticion.ConIncidencia = IIf(chkConIncidencia.Checked, 1, 0)
                objPeticion.FormatoSalida = rblFormatoSaida.SelectedValue

                ' Recupera os detalles parciales para popular o relatório
                Dim objRespuesta As ContractoServ.DetalleParciales.GetDetalleParciales.Respuesta = objDetalleParciales.ListarDetalleParciales(objPeticion)

                ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório
                ' validar retorno do serviço
                Dim strMsgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, strMsgErro, objRespuesta.MensajeError, True) Then
                    Me.MostraMensagem(strMsgErro)
                    Exit Sub
                End If

                ' Verifica o formato do relatório e existe dados
                If (objRespuesta.DetalleParciales IsNot Nothing AndAlso objRespuesta.DetalleParciales.Count > 0) Then

                    ' Atribui os dados recuperados para a sessão
                    Session("objDetalleParciales") = objRespuesta.DetalleParciales

                Else
                    ' Define a mensagem de registros não encontrados
                    Valida.Add(Traduzir("lbl_nenhum_registro_encontrado"))
                    ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                    MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                    Exit Sub
                End If

                'Variável onde será montada o nome do arquivo
                Dim nomeArquivo As String = Util.RecuperarNomeArquivo(ddlDelegacion.SelectedValue, _
                                                                     Clientes.FirstOrDefault().Codigo, _
                                                                      Constantes.TipoInformacaoRelatorio.DETALLE_PARCIALES, _
                                                                      txtFechaTransporteHasta.Text)

                ' Define os parametros que serão passados para a exibição do relatório
                Dim parametros As String = "?Exibir=" & rblFormatoSaida.SelectedValue & _
                                           "&NomeArquivo=" & nomeArquivo

                ' Se a Delegação foi informado
                If Not String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
                    ' Atribui o nome da delegação
                    parametros &= "&Delegacion=" & ddlDelegacion.SelectedValue & " - " & ddlDelegacion.SelectedItem.Text
                End If

                ' Verifica se a data da contagem foi informada
                If (txtFechaDesde.Text <> String.Empty AndAlso txtFechaHasta.Text <> String.Empty) Then
                    ' Atribui ao parametro a data final do processo
                    parametros &= "&FechaDesde=" & txtFechaDesde.Text
                    ' Atribui ao parametro a data inicial do processo
                    parametros &= "&FechaHasta=" & txtFechaHasta.Text
                    ' Atribui ao parametro o tipo de data selecionado
                    parametros &= "&TipoData=" & Constantes.TipoFecha.PROCESO
                End If

                If (txtFechaTransporteDesde.Text <> String.Empty AndAlso txtFechaTransporteHasta.Text <> String.Empty) Then
                    ' Atribui ao parametro a data inicial do tranporte
                    parametros &= "&FechaDesde=" & txtFechaTransporteDesde.Text
                    ' Atribui ao parametro a data final do transporte
                    parametros &= "&FechaHasta=" & txtFechaTransporteHasta.Text
                    ' Atribui ao parametro o tipo de data selecionado
                    parametros &= "&TipoData=" & Constantes.TipoFecha.TRANSPORTE
                End If

                ' Se o Número Remesa
                If (txtNumeroRemesa.Text <> String.Empty) Then
                    ' Atribui o número remesa
                    parametros &= "&NumeroRemesa=" & txtNumeroRemesa.Text
                End If

                ' Se o Número Precinto
                If (txtNumeroPrecinto.Text <> String.Empty) Then
                    ' Atribui o número precinto
                    parametros &= "&NumeroPrecinto=" & txtNumeroPrecinto.Text
                End If

                ' Se o Cliente foi informado

                If Clientes IsNot Nothing AndAlso Clientes.Count = 0 Then
                    ' Atribui o nome do cliente
                    parametros &= "&Cliente=" & Clientes.FirstOrDefault().Descripcion.Replace("&", "%26")
                    parametros &= "&CodigoCliente=" & Clientes.FirstOrDefault().Codigo
                    If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count = 0 Then
                        ' Atribui o nome do subcliente
                        parametros &= "&SubCliente=" & Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Descripcion.Replace("&", "%26")
                        parametros &= "&CodigoSubCliente=" & Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                    End If

                End If

                ' denominação
                parametros &= "&ConDenominacion=" & chkConDenominacion.Checked

                'incidência
                parametros &= "&ConIncidencia=" & chkConIncidencia.Checked

                ' Chama a página que exibirá o relatório
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('DetalleParcialesMostrar.aspx" & parametros.Replace("'", "\'") & "'); ", True)

            Else
                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#Region "[EVENTOS]"

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Me.ConsultarDados()
    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Me.LimparCampos()
        Me.Response.Redireccionar(Me.ResolveUrl("BusquedaDetalleParciales.aspx"))
    End Sub

#End Region
#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.SubClienteHabilitado = True
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
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Exception, e.Erro.Message, e.Erro.ToString()))
    End Sub
#End Region
End Class