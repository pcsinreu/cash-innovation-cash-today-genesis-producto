Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class BusquedaMAEPopup
    Inherits Base

    <Serializable()> _
    Public Class PlanxMaquina

        Public Property Maquinas As List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)

    End Class


#Region "[PROPRIEDADES]"
    'Selecionados da tela principal
    Public Property ViewStateMaquinaEntrada() As PlanxMaquina
        Get
            Return ViewState("MaquinaEntrada")
        End Get
        Set(value As PlanxMaquina)
            ViewState("MaquinaEntrada") = value
        End Set
    End Property

    'Nova seleção realizada
    Public Property ViewStateMaquinasSelecionadas() As PlanxMaquina
        Get
            Return ViewState("MaquinasSelecionadas")
        End Get
        Set(value As PlanxMaquina)
            ViewState("MaquinasSelecionadas") = value
        End Set
    End Property

    Private _MaquinasGrid As PlanxMaquina
    Public Property MaquinasGrid() As PlanxMaquina
        Get
            Return ViewState("MaquinasGrid")
        End Get
        Set(value As PlanxMaquina)
            ViewState("MaquinasGrid") = value
        End Set
    End Property

    Private _CantidadHorasPeriodosFuturos As String
    Public Property CantidadHorasPeriodosFuturos As String
        Get
            Return Session("CantidadHorasPeriodosFuturos")
        End Get
        Set(value As String)
            Session("CantidadHorasPeriodosFuturos") = value
        End Set
    End Property

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

    Public Property MaquinasRespuesta() As DataTable
        Get
            Return Session("_MaquinasRespuesta")
        End Get
        Set(value As DataTable)
            Session("_MaquinasRespuesta") = value
        End Set
    End Property

#End Region

#Region "[HelpersCliente]"
    Public Property ClientesMAE As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesMAE.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesMAE.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesMAE As ucCliente
    Public Property ucClientesMAE() As ucCliente
        Get
            If _ucClientesMAE Is Nothing Then
                _ucClientesMAE = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesMAE.ID = Me.ID & "_ucClientesMAE"
                AddHandler _ucClientesMAE.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientesMAE)
            End If
            Return _ucClientesMAE
        End Get
        Set(value As ucCliente)
            _ucClientesMAE = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub ConfigurarControle_Cliente()

        Me.ucClientesMAE.SelecaoMultipla = False
        Me.ucClientesMAE.ClienteHabilitado = True
        Me.ucClientesMAE.ClienteObrigatorio = False

        Me.ucClientesMAE.SubClienteHabilitado = True
        Me.ucClientesMAE.SubClienteObrigatorio = False
        Me.ucClientesMAE.ucSubCliente.MultiSelecao = False

        Me.ucClientesMAE.PtoServicioHabilitado = True
        Me.ucClientesMAE.PtoServicoObrigatorio = False
        Me.ucClientesMAE.ucPtoServicio.MultiSelecao = False

        If ClientesMAE IsNot Nothing Then
            Me.ucClientesMAE.Clientes = ClientesMAE
        End If

    End Sub

    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientesMAE.UpdatedControl
        Try
            If ucClientesMAE.Clientes IsNot Nothing Then
                ClientesMAE = ucClientesMAE.Clientes

            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()
        'Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"
        'btnCancelar.OnClientClick = jsScript
        txtDeviceID.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
        txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

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
        MyBase.CodFuncionalidad = "ABM_PLANIFICACION"
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            ConfigurarControle_Cliente()

            If Not Page.IsPostBack Then

                ClientesMAE = Nothing

                ''Consome os ojetos passados
                ConsomeObjeto()
                PreencherddlMarca()
                InicializarMaquinas()

            End If

            ' trata o foco dos campos
            'TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = MyBase.RecuperarValorDic("mod_planificacion_titulo")

        GdvResultado.Columns(1).HeaderText = MyBase.RecuperarValorDic("lbl_grd_cliente")
        GdvResultado.Columns(2).HeaderText = MyBase.RecuperarValorDic("lbl_grd_subcliente")
        GdvResultado.Columns(3).HeaderText = MyBase.RecuperarValorDic("lbl_grd_ptoservicio")
        GdvResultado.Columns(4).HeaderText = MyBase.RecuperarValorDic("lbl_grd_deviceID")
        GdvResultado.Columns(5).HeaderText = MyBase.RecuperarValorDic("lbl_grd_descripcion")
        GdvResultado.Columns(6).HeaderText = MyBase.RecuperarValorDic("lbl_grd_marca")
        GdvResultado.Columns(7).HeaderText = MyBase.RecuperarValorDic("lbl_grd_modelo")
        GdvResultado.Columns(8).HeaderText = MyBase.RecuperarValorDic("lbl_grd_vigente")

        ddlEstado.Items(0).Text = MyBase.RecuperarValorDic("lblTodos")
        ddlEstado.Items(1).Text = MyBase.RecuperarValorDic("lbl_vigente")
        ddlEstado.Items(2).Text = MyBase.RecuperarValorDic("lbl_no_vigente")

        lblDeviceID.Text = MyBase.RecuperarValorDic("lbl_deviceID")
        lblDescripcion.Text = MyBase.RecuperarValorDic("lbl_descripcion")
        lblMarca.Text = MyBase.RecuperarValorDic("lbl_marca")
        lblModelo.Text = MyBase.RecuperarValorDic("lbl_modelo")
        lblEstado.Text = MyBase.RecuperarValorDic("lbl_estado")
        lblModelo.Text = MyBase.RecuperarValorDic("lbl_modelo")

        'Botoes

        btnSeleccionar.Text = MyBase.RecuperarValorDic("btn_selecionar_todos")
        btnSeleccionar.ToolTip = MyBase.RecuperarValorDic("btn_selecionar_todos")
        btnCancelar.Text = MyBase.RecuperarValorDic("btn_cancelar")
        btnCancelar.ToolTip = MyBase.RecuperarValorDic("btn_cancelar")

        btnBuscar.Text = MyBase.RecuperarValorDic("btn_buscar")
        btnAceptar.Text = MyBase.RecuperarValorDic("btn_aceptar")
        btnLimpar.Text = MyBase.RecuperarValorDic("btn_limpiar")

        btnAceptar.ToolTip = MyBase.RecuperarValorDic("btn_aceptar")
        btnBuscar.ToolTip = MyBase.RecuperarValorDic("btn_buscar")
        btnLimpar.ToolTip = MyBase.RecuperarValorDic("btn_limpiar")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Confirma seleção de todas as MAES do grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeleccionarTodos_Click(sender As Object, e As System.EventArgs) Handles btnSeleccionar.Click

        Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeSelecionar.ClientID & Chr(34) & ");"

        MyBase.ExibirMensagemSimNao(RecuperarValorDic("MSG_INFO_SELECCIONAR_TODAS_MAES"), jsScript)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try

            InicializarMaquinas()

            Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub btnConsomeSelecionarTodos_Click(sender As Object, e As System.EventArgs) Handles btnConsomeSelecionar.Click

        Try

            SelecionarMAES(True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            SelecionarMAES(False)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim strErros As String = MontaMensagensErro(True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            BuscarDados()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            GdvResultado.CarregaControle(Nothing, True, True)
            pnlSemRegistro.Visible = False

            txtDeviceID.Text = String.Empty
            txtDescricao.Text = String.Empty
            ddlMarca.SelectedValue = String.Empty
            ddlModelo.SelectedValue = String.Empty
            ddlEstado.SelectedValue = String.Empty

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            ClientesMAE.Clear()
            ClientesMAE.Add(objCliente)
            AtualizaDadosHelperCliente(ClientesMAE, ucClientesMAE)

            txtDeviceID.Focus()
            btnSeleccionar.Enabled = False
            btnAceptar.Enabled = False
            ' ViewStateMaquinaEntrada = Nothing
            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

    Protected Sub ddlMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarca.SelectedIndexChanged
        If Not ddlMarca.SelectedValue.Equals(String.Empty) Then
            PreencherddlModelo(ddlMarca.SelectedValue)
        Else
            PreencherddlModelo(Nothing)
        End If
        ddlModelo.Enabled = ddlModelo.Items.Count > 1
    End Sub

    Protected Sub ProsegurGridViewProcesso_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        For Each dtRow As GridViewRow In GdvResultado.Rows
            Dim rdbSelecionado As CheckBox = dtRow.Cells(0).FindControl("rdbSelecionado")
            Dim lblOidMaquina As Label = dtRow.Cells(0).FindControl("lblOidMaquina")
            MaquinasRespuesta.Select("OidMaquina = '" & lblOidMaquina.Text & "'")(0)("Seleccionado") = rdbSelecionado.Checked

        Next

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            MaquinasRespuesta.DefaultView.Sort = " deviceID ASC "
        Else
            MaquinasRespuesta.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = MaquinasRespuesta

    End Sub

    Private Sub ProsegurGridViewResultadp_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                If CBool(e.Row.DataItem("BolActivo")) Then
                    CType(e.Row.Cells(7).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(7).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

                If CBool(e.Row.DataItem("Seleccionado")) Then
                    CType(e.Row.Cells(7).FindControl("rdbSelecionado"), CheckBox).Checked = True
                Else
                    CType(e.Row.Cells(7).FindControl("rdbSelecionado"), CheckBox).Checked = False
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

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

        btnSeleccionar.Enabled = False
        btnSeleccionar.Visible = False

    End Sub

#End Region

#Region "[MÉTODOS]"

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

    Private Sub InicializarMaquinas()
        ViewStateMaquinasSelecionadas = New PlanxMaquina
        ViewStateMaquinasSelecionadas.Maquinas = New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)
        Session("objBusquedaMAE") = ViewStateMaquinasSelecionadas
    End Sub
    Public Sub ConsomeObjeto()

        If Session("objBusquedaMAE") IsNot Nothing Then
            ViewStateMaquinaEntrada = CType(Session("objBusquedaMAE"), PlanxMaquina)
        Else
            ViewStateMaquinaEntrada = Nothing
        End If

    End Sub


    Private Sub SelecionarMAES(bolSelecionarTodos As Boolean)

        For Each dtRow As GridViewRow In GdvResultado.Rows
            Dim rdbSelecionado As CheckBox = dtRow.Cells(0).FindControl("rdbSelecionado")
            Dim lblOidMaquina As Label = dtRow.Cells(0).FindControl("lblOidMaquina")
            Dim lblOidPunto As Label = dtRow.Cells(0).FindControl("lblOidPunto")
            MaquinasRespuesta.Select("OidMaquina = '" & lblOidMaquina.Text & "' AND OidPtoServicio = '" & lblOidPunto.Text & "'")(0)("Seleccionado") = rdbSelecionado.Checked
        Next

        If (bolSelecionarTodos) Then
            ViewStateMaquinasSelecionadas = MaquinasGrid

        Else

            For Each dtRow In MaquinasRespuesta.Rows
                Dim rdbSelecionado As Boolean = dtRow("Seleccionado")
                Dim lblOidMaquina As String = dtRow("OidMaquina")
                Dim lblOidPunto As String = dtRow("OidPtoServicio")

                If rdbSelecionado Then
                    Dim objMaquina = MaquinasGrid.Maquinas.FirstOrDefault(Function(a) a.OidMaquina = lblOidMaquina AndAlso a.OidPtoServicio = lblOidPunto)

                    Dim objMaquinaSelecionado As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina
                    With objMaquinaSelecionado
                        .OidMaquina = objMaquina.OidMaquina
                        .DeviceID = objMaquina.DeviceID
                        .Descripcion = objMaquina.Descripcion
                        .BolActivo = objMaquina.BolActivo
                        .DesModelo = objMaquina.DesModelo
                        .DesFabricante = objMaquina.DesFabricante
                        .CodigoCliente = objMaquina.CodigoCliente
                        .Cliente = objMaquina.Cliente
                        .CodigoSubCliente = objMaquina.CodigoSubCliente
                        .SubCliente = objMaquina.SubCliente
                        .OidPtoServicio = objMaquina.OidPtoServicio
                        .CodigoPtoServicio = objMaquina.CodigoPtoServicio
                        .PtoServicio = objMaquina.PtoServicio
                    End With

                    ViewStateMaquinasSelecionadas.Maquinas.Add(objMaquinaSelecionado)
                End If
            Next

        End If

        Session("objBusquedaMAE") = Me.ViewStateMaquinasSelecionadas
        ViewStateMaquinaEntrada = Nothing
        Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

        If VerificaMAESVinculadas() Then
            Dim mensagem = String.Format(RecuperarValorDic("MSG_INFO_MAES_JA_VINCULADAS"), CantidadHorasPeriodosFuturos)
            MyBase.ExibirMensagemSimNao(mensagem, jsScript)
        Else
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "MantenimientoCamposExtras", jsScript, True)
        End If

    End Sub

    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUserControl.ucCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucCliente.ExibirDados(True)
    End Sub


    Private Sub BuscarDados()
        Try

            Dim MensagemErro As String = MontaMensagensErro()

            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If

            ' setar ação de busca
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            If ViewStateMaquinaEntrada IsNot Nothing AndAlso ViewStateMaquinaEntrada.Maquinas IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtDeviceID.Text) AndAlso _
                ViewStateMaquinaEntrada.Maquinas.FirstOrDefault(Function(b) b.DeviceID = txtDeviceID.Text) IsNot Nothing Then

                btnSeleccionar.Enabled = False
                btnAceptar.Enabled = False
                GdvResultado.DataSource = Nothing
                GdvResultado.DataBind()
                lblSemRegistro.Text = String.Format(Traduzir("deviceIDasociado"), txtDeviceID.Text)
                pnlSemRegistro.Visible = True
                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            Else

                Dim objRespuesta As New IAC.ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta

                objRespuesta = GetBusqueda()

                If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    Exit Sub
                End If

                ' define a ação de busca somente se houve retorno
                If objRespuesta.Maquinas IsNot Nothing AndAlso objRespuesta.Maquinas.Count > 0 Then

                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable

                    MaquinasRespuesta = GdvResultado.ConvertListToDataTable(objRespuesta.Maquinas)

                    If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                        MaquinasRespuesta.DefaultView.Sort = " deviceId ASC"
                    ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                        If GdvResultado.SortCommand.Equals(String.Empty) Then
                            MaquinasRespuesta.DefaultView.Sort = " deviceId ASC "
                        Else
                            MaquinasRespuesta.DefaultView.Sort = GdvResultado.SortCommand
                        End If

                    Else
                        MaquinasRespuesta.DefaultView.Sort = GdvResultado.SortCommand
                    End If

                    MaquinasGrid = New PlanxMaquina
                    MaquinasGrid.Maquinas = objRespuesta.Maquinas


                    ' carregar controle
                    GdvResultado.DataSource = MaquinasRespuesta
                    GdvResultado.DataBind()

                    btnSeleccionar.Enabled = True
                    btnAceptar.Enabled = True


                Else
                    btnSeleccionar.Enabled = False
                    btnAceptar.Enabled = False
                    'Limpa a consulta
                    GdvResultado.DataSource = Nothing
                    GdvResultado.DataBind()

                    lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)

                    pnlSemRegistro.Visible = True

                    Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function VerificaMAESVinculadas() As Boolean
        If ViewStateMaquinasSelecionadas.Maquinas.Count > 0 Then
            Dim lstMaquinas As New List(Of String)

            For Each item In ViewStateMaquinasSelecionadas.Maquinas
                lstMaquinas.Add(item.OidMaquina)
            Next

            Return Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.VerificaMaquinaVinculada(lstMaquinas)
        Else
            Return False
        End If
    End Function
    Private Function GetBusqueda() As IAC.ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta
        Dim objAccionMaquina As New LogicaNegocio.AccionMaquina
        Dim objPeticion As New IAC.ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Respuesta

        If ucClientesMAE.Clientes IsNot Nothing AndAlso ucClientesMAE.Clientes.Count > 0 Then
            objPeticion.OidClientes = ucClientesMAE.Clientes.Select(Function(c) c.Identificador).ToList()
            If ucClientesMAE.Clientes.Any(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0) Then
                objPeticion.OidSubClientes = ucClientesMAE.Clientes.SelectMany(Function(c) c.SubClientes).Select(Function(sc) sc.Identificador).ToList()
                If ucClientesMAE.Clientes.SelectMany(Function(c) c.SubClientes).Any(Function(sc) sc.PuntosServicio IsNot Nothing AndAlso sc.PuntosServicio.Count > 0) Then
                    objPeticion.OidPuntoServicio = ucClientesMAE.Clientes.SelectMany(Function(c) c.SubClientes).SelectMany(Function(sc) sc.PuntosServicio).Select(Function(pto) pto.Identificador).ToList()
                End If
            End If
        End If

        objPeticion.DeviceID = txtDeviceID.Text
        objPeticion.Descripcion = txtDescricao.Text
        objPeticion.OidFabricante = ddlMarca.SelectedValue
        objPeticion.OidModelo = ddlModelo.SelectedValue

        If Not String.IsNullOrEmpty(ddlEstado.SelectedValue) Then
            objPeticion.BolVigente = IIf(ddlEstado.SelectedValue = "0", False, True)
        End If

        If ViewStateMaquinaEntrada IsNot Nothing AndAlso ViewStateMaquinaEntrada.Maquinas IsNot Nothing Then
            objPeticion.OidMaquinasSelecionadas = ViewStateMaquinaEntrada.Maquinas.Select(Function(b) b.OidMaquina).ToList
        End If

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objAccionMaquina.GetMaquinasSinPlanificacion(objPeticion)

        Return objRespuesta
    End Function


    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If txtDeviceID.Text.Trim.Equals(String.Empty) Then
            If ClientesMAE Is Nothing OrElse ClientesMAE.Count = 0 Then
                strErro.Append(RecuperarValorDic("msg_csvFiltroCliente") & Aplicacao.Util.Utilidad.LineBreak)
            End If
        End If
        Return strErro.ToString
    End Function

    Public Sub PreencherddlMarca()

        Dim objPeticion As New ContractoServicio.Fabricante.GetFabricante.Peticion
        Dim objRespuesta As New ContractoServicio.Fabricante.GetFabricante.Respuesta
        Dim objAccionFabricante As New LogicaNegocio.AccionFabricante

        objPeticion.BolVigente = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objAccionFabricante.GetFabricantes(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Fabricante.Count = 0 Then
            LimpiarDropDownList(ddlMarca)
            Exit Sub
        End If

        If objRespuesta.Fabricante.Count > 0 Then
            Dim lista = objRespuesta.Fabricante.Select(Function(a) New With {.OidFabricante = a.Identificador, _
                                                                             .DesFabricante = a.Descripcion, _
                                                                             .CodDesFabricante = a.Codigo + " - " + a.Descripcion}).OrderBy(Function(b) b.CodDesFabricante)
            ddlMarca.AppendDataBoundItems = True
            ddlMarca.Items.Clear()
            ddlMarca.Items.Add(New ListItem(MyBase.RecuperarValorDic("btnSeleccionar"), String.Empty))
            ddlMarca.DataTextField = "CodDesFabricante"
            ddlMarca.DataValueField = "OidFabricante"
            ddlMarca.DataSource = lista.ToList()
            ddlMarca.DataBind()

        End If

    End Sub

    Private Sub LimpiarDropDownList(ddl As DropDownList)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(MyBase.RecuperarValorDic("btnSeleccionar"), String.Empty))
    End Sub
    Public Sub PreencherddlModelo(OidFabricante As String)
        Try
            Dim objAccionModelo As New LogicaNegocio.AccionModelo
            Dim objPeticion As New ContractoServicio.Modelo.GetModelo.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Modelo.GetModelo.Respuesta

            objPeticion.OidFabricante = OidFabricante
            objPeticion.BolVigente = True
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            If String.IsNullOrEmpty(OidFabricante) Then
                LimpiarDropDownList(ddlModelo)
                Exit Sub
            Else
                objPeticion.OidFabricante = OidFabricante
            End If

            objRespuesta = objAccionModelo.GetModelos(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                Exit Sub
            End If

            If objRespuesta.Modelo.Count = 0 Then
                LimpiarDropDownList(ddlModelo)
                Exit Sub
            Else
                Dim lista = objRespuesta.Modelo.Select(Function(a) New With {.OidModelo = a.Identificador, _
                                                                            .DesModelo = a.Descripcion, _
                                                                           .CodDesModelo = a.Codigo + " - " + a.Descripcion}).OrderBy(Function(b) b.CodDesModelo)

                ddlModelo.AppendDataBoundItems = True
                ddlModelo.Items.Clear()
                ddlModelo.Items.Add(New ListItem(MyBase.RecuperarValorDic("btnSeleccionar"), String.Empty))
                ddlModelo.DataTextField = "CodDesModelo"
                ddlModelo.DataValueField = "OidModelo"
                ddlModelo.DataSource = lista
                ddlModelo.DataBind()

            End If

            ddlModelo.Enabled = ddlModelo.Items.Count > 1

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region


End Class