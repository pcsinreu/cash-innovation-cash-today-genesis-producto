Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion

Public Class MantenimientoCamposExtra
    Inherits Base

    <Serializable()>
    Public Class CamposExtras

        Public Property OidCliente As String

        Public Property CodCliente As String

        Public Property DesCliente As String

        Public Property OidSubcliente As String

        Public Property CodSubcliente As String

        Public Property DesSubcliente As String

        Public Property OidPuntoServicio As String

        Public Property CodPuntoServicio As String

        Public Property DesPuntoServicio As String

        Public Property DatosBancarios As List(Of Comon.Clases.DatoBancario)
        Public Property Peticion As Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion

    End Class

#Region "[Propriedades]"

    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

#End Region

#Region "[PROPRIEDADES]"

    Private _ucDatosBancarios As ucDatosBanc
    Private ReadOnly Property ucDatosBanc As ucDatosBanc
        Get
            If _ucDatosBancarios Is Nothing Then
                _ucDatosBancarios = LoadControl("~\Controles\ucDatosBanc.ascx")
                _ucDatosBancarios.ID = "ucDatosBanc"
                _ucDatosBancarios.strBtnExecutar = btnConsomeDatosBancarios.ClientID
                If phDatosBanc.Controls.Count = 0 Then
                    phDatosBanc.Controls.Add(_ucDatosBancarios)
                End If
            End If
            Return _ucDatosBancarios
        End Get
    End Property

    Public Property ViewStateCamposExtrasEntrada() As CamposExtras
        Get
            Return Session("CamposExtrasEntrada")
        End Get
        Set(value As CamposExtras)
            Session("CamposExtrasEntrada") = value
        End Set
    End Property

    Public Property ViewStateCamposExtrasOriginal() As CamposExtras
        Get
            Return Session("CamposExtrasEntradaOriginal")
        End Get
        Set(value As CamposExtras)
            Session("CamposExtrasEntradaOriginal") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        'DefinirRetornoFoco(btnGrabar, txtIdentificador)
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        'MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CODIGO_AJENO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
        MyBase.CodFuncionalidad = "ABM_MAE"
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                'Consome os ojetos passados
                ConsomeObjeto()

                If (ViewStateCamposExtrasEntrada Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If (ViewStateCamposExtrasEntrada IsNot Nothing) Then
                    txtCliente.Text = ViewStateCamposExtrasEntrada.CodCliente + " " + ViewStateCamposExtrasEntrada.DesCliente
                    txtSubcliente.Text = ViewStateCamposExtrasEntrada.CodSubcliente + " " + ViewStateCamposExtrasEntrada.DesSubcliente
                    txtPtoServicio.Text = ViewStateCamposExtrasEntrada.CodPuntoServicio + " " + ViewStateCamposExtrasEntrada.DesPuntoServicio
                End If

                ConfigurarUsersControls()

                AddHandler ucDatosBanc.DadosCarregados, AddressOf ucDatosBanc_DadosCarregados

            End If

            ucDatosBanc.ConsomeDatoBancario()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            'UpdatePanelIdentificador.Attributes.Add("style", "margin:0px !important;")
            'UpdatePanel2.Attributes.Add("style", "margin:0px !important;")
            'txtDescripcionAjena.Attributes.Add("style", "margin-left:2px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = MyBase.RecuperarValorDic("mod_campos_extras_titulo")
        lblSubTitulo.Text = MyBase.RecuperarValorDic("mod_campos_extras_titulo")
        lblCliente.Text = MyBase.RecuperarValorDic("lbl_cliente")
        lblSucliente.Text = MyBase.RecuperarValorDic("lbl_subcliente")
        lblPtoServicio.Text = MyBase.RecuperarValorDic("lbl_pto_servicio")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnAddDatosBancarios.Text = MyBase.RecuperarValorDic("btnAddDatosBancarios")
        btnAddDatosBancarios.ToolTip = MyBase.RecuperarValorDic("btnAddDatosBancarios")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    Private Sub btnAddDatosBancarios_Click(sender As Object, e As System.EventArgs) Handles btnAddDatosBancarios.Click
        Try
            Me.ucDatosBanc.Cambiar(-1)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try

            Me.ViewStateCamposExtrasEntrada.DatosBancarios = Me.ucDatosBanc.DatosBancarios
            Me.ViewStateCamposExtrasEntrada.Peticion = Me.ucDatosBanc.BuscarPeticion()

            ' aqui deve gravar na sessão
            Session("objMantenimientoCamposExtras") = Me.ViewStateCamposExtrasEntrada

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try
            Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

            If Aplicacao.Util.Utilidad.HayModificaciones(ViewStateCamposExtrasEntrada.DatosBancarios, Me.ucDatosBanc.DatosBancarios) Then
                MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("MSG_INFO_CERRAR_PANTALLA_CAMPOS_EXTRAS"), jsScript)
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

    Private Sub ucDatosBanc_DadosCarregados(sender As Object, e As System.EventArgs)
        If ViewStateCamposExtrasEntrada.DatosBancarios IsNot Nothing AndAlso ViewStateCamposExtrasEntrada.DatosBancarios.Count > 0 Then
            If ucDatosBanc.DatosBancarios Is Nothing OrElse ucDatosBanc.DatosBancarios.Count = 0 Then
                ucDatosBanc.DatosBancarios = ViewStateCamposExtrasEntrada.DatosBancarios
                ucDatosBanc.AtualizaGrid()
            Else
                'Remove todos recebidos pelo parâmetro de entrada
                For Each objDatoBanc In ViewStateCamposExtrasEntrada.DatosBancarios
                    If Not String.IsNullOrEmpty(objDatoBanc.Identificador) _
                        AndAlso ucDatosBanc.DatosBancarios.Exists(Function(a) a.Identificador = objDatoBanc.Identificador) Then
                        ucDatosBanc.DatosBancarios.RemoveAll(Function(a) a.Identificador = objDatoBanc.Identificador)
                    End If
                Next

                ucDatosBanc.DatosBancarios.AddRange(ViewStateCamposExtrasEntrada.DatosBancarios)
                ucDatosBanc.AtualizaGrid()
            End If
        End If
        ViewStateCamposExtrasEntrada.DatosBancarios = ucDatosBanc.DatosBancarios
        If ViewStateCamposExtrasOriginal Is Nothing Then
            ViewStateCamposExtrasOriginal = New CamposExtras
        End If
        ViewStateCamposExtrasOriginal.DatosBancarios = ucDatosBanc.DatosBancarios
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Public Sub ControleBotoes()

        MyBase.Acao = Request.QueryString("acao")

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setConsultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion


            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial


        End Select

    End Sub

    Private Sub setConsultar()

        btnGrabar.Enabled = False
        btnAddDatosBancarios.Enabled = False
        btnGrabar.Visible = False

    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Sub ConfigurarUsersControls()
        ucDatosBanc.EsPopup = True
        ucDatosBanc.Cliente.Identificador = ViewStateCamposExtrasEntrada.OidCliente
        ucDatosBanc.SubCliente.Identificador = ViewStateCamposExtrasEntrada.OidSubcliente
        ucDatosBanc.PuntoServicio.Identificador = ViewStateCamposExtrasEntrada.OidPuntoServicio
        'ucDatosBanc.CarregaDados()
        'ucDatosBanc.AtualizaGrid()

    End Sub

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

    Public Sub ConsomeObjeto()

        If Session("objMantenimientoCamposExtras") IsNot Nothing Then

            ViewStateCamposExtrasEntrada = CType(Session("objMantenimientoCamposExtras"), CamposExtras)
            ViewStateCamposExtrasOriginal = CType(Session("objMantenimientoCamposExtras"), CamposExtras)

            'Remove da sessão
            Session("objMantenimientoCamposExtras") = Nothing

        End If

    End Sub

#End Region


End Class