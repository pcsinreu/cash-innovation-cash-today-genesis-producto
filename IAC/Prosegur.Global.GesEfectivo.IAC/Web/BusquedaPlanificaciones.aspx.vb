Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Clases
Imports Newtonsoft.Json
Imports System.Globalization
Imports DevExpress.Web.Data
Imports DevExpress.Web
Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class BusquedaPlanificaciones
    Inherits Base

#Region "[PROPS/VARS]"

    Dim script As String = String.Empty
    Private Property DatosBancarios As Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
        Get
            Return ViewState("DatosBancariosPLAN")
        End Get
        Set(value As Dictionary(Of String, List(Of Comon.Clases.DatoBancario)))
            ViewState("DatosBancariosPLAN") = value
        End Set
    End Property
    Private Property objRespuestaCanales As IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta
        Get
            Return Session("objRespuestaCanalesPLAN")
        End Get
        Set(value As IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta)
            Session("objRespuestaCanalesPLAN") = value
        End Set
    End Property

    Private Property respuestaComissionDatosBancarios As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta
        Get
            Return Session("respuestaComissionDatosBancariosPLAN")
        End Get
        Set(value As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta)
            Session("respuestaComissionDatosBancariosPLAN") = value
        End Set
    End Property

    Public Property CanalesOriginal As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Canal)
        Get
            Return Session("CanalesOriginalPLAN")
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Canal))
            Session("CanalesOriginalPLAN") = value
        End Set
    End Property

    Private Property CodigoAjenoBanco As ContractoServicio.CodigoAjeno.CodigoAjenoBase
        Get
            Return ViewState("CodigoAjenoBancoPLAN")
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoBase)
            ViewState("CodigoAjenoBancoPLAN") = value
        End Set
    End Property

    Private Property CodigoAjenoBancoComision As ContractoServicio.CodigoAjeno.CodigoAjenoBase
        Get
            Return ViewState("CodigoAjenoBancoComisionPLAN")
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoBase)
            ViewState("CodigoAjenoBancoComisionPLAN") = value
        End Set
    End Property

    Private Property Parametros() As List(Of Comon.Clases.Parametro)
        Get
            Return ViewState("Parametros")
        End Get
        Set(value As List(Of Comon.Clases.Parametro))
            ViewState("Parametros") = value
        End Set
    End Property

    Private Property ExibeValidacionCanal As Boolean
        Get
            Return ViewState("ExibeValidacionCanal")
        End Get
        Set(value As Boolean)
            ViewState("ExibeValidacionCanal") = value
        End Set
    End Property

    Private Property ExibeValidacionHorario As Boolean
        Get
            Return ViewState("ExibeValidacionHorario")
        End Get
        Set(value As Boolean)
            ViewState("ExibeValidacionHorario") = value
        End Set
    End Property
    Private ReadOnly Property HayPeriodosAbiertos As Boolean
        Get
            If ViewState("HayPeriodosAbiertos") Is Nothing Then
                ViewState("HayPeriodosAbiertos") = Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.VerificaPeriodosVinculados(GetMaquinasSelecionadas())
            End If

            Return ViewState("HayPeriodosAbiertos")
        End Get

    End Property

    'Variavél de Ação para ser acessado via JavaScript para habilitação do grid de horarios devido ao PostBack
    Private Property AcaoPagina As String
        Get
            Return hdfAcao.Value
        End Get
        Set(value As String)
            hdfAcao.Value = value
        End Set
    End Property

    Private Property PropPlanificacion As Comon.Clases.Planificacion
        Get
            Return Session("Planificacion")
        End Get
        Set(value As Comon.Clases.Planificacion)
            Session("Planificacion") = value
        End Set
    End Property

    Private Property Maquinas As List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)
        Get
            If Session("Maquinas") IsNot Nothing Then
                Return Session("Maquinas")
            Else
                Return New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)
            End If

        End Get
        Set(value As List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina))
            Session("Maquinas") = value
        End Set
    End Property

    Private Property Delegaciones As List(Of ContractoServicio.Delegacion.GetDelegacion.Delegacion)
        Get
            Return ViewState("Delegaciones")

        End Get
        Set(value As List(Of ContractoServicio.Delegacion.GetDelegacion.Delegacion))
            ViewState("Delegaciones") = value
        End Set
    End Property

    Public Property CodigosAjenosBancoPlanificacion As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples
        Get
            Return Session("objMantenimientoCodigosAjenosBancoPlanificacion")
        End Get
        Set(value As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples)
            Session("objMantenimientoCodigosAjenosBancoPlanificacion") = value
        End Set
    End Property


    Public Property CodigosAjenosBancoComisionPlanificacion As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples
        Get
            Return Session("objMantenimientoCodigosAjenosBancoPlanificacion")
        End Get
        Set(value As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples)
            Session("objMantenimientoCodigosAjenosBancoPlanificacion") = value
        End Set
    End Property

    Private Property BancoComision As Cliente
        Get
            Return ViewState("BancoComision")
        End Get
        Set(value As Cliente)
            ViewState("BancoComision") = value
        End Set
    End Property

    Private Property PorComisionPlanificacion As String
        Get
            Return ViewState("PorComisionPlanificacion")
        End Get
        Set(value As String)
            ViewState("PorComisionPlanificacion") = value
        End Set
    End Property

    Private Property DiaCierreFacturacion As String
        Get
            Return ViewState("DiaCierreFacturacion")
        End Get
        Set(value As String)
            ViewState("DiaCierreFacturacion") = value
        End Set
    End Property

    Private Property TiposPlanificacion As List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion)
        Get
            Return ViewState("TiposPlanificacion")
        End Get
        Set(value As List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion))
            ViewState("TiposPlanificacion") = value
        End Set
    End Property

#End Region

#Region "[HelpersCliente]"


    Private _ucDatosBancarios As ucDatosBanc
    Public ReadOnly Property ucDatosBanc As ucDatosBanc
        Get
            If _ucDatosBancarios Is Nothing Then
                _ucDatosBancarios = LoadControl("~\Controles\ucDatosBanc.ascx")
                _ucDatosBancarios.ID = "ucDatosBanc"
                _ucDatosBancarios.strBtnExecutar = btnConsomeTotalizador.ClientID
                If phDatosBanc.Controls.Count = 0 Then
                    phDatosBanc.Controls.Add(_ucDatosBancarios)
                End If
            End If
            Return _ucDatosBancarios
        End Get
    End Property
    Private Sub btnConsomeTotalizador_Click(sender As Object, e As EventArgs) Handles btnConsomeTotalizador.Click
        Try
            Threading.Thread.Sleep(10)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientesPlan"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property



    Public Property Canales As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Canal)
        Get
            Return ucCanales.Canales
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Canal))
            ucCanales.Canales = value
        End Set
    End Property

    Private WithEvents _ucCanales As ucCanal
    Public Property ucCanales() As ucCanal
        Get
            If _ucCanales Is Nothing Then
                _ucCanales = LoadControl(ResolveUrl("~\Controles\Helpers\ucCanal.ascx"))
                _ucCanales.ID = Me.ID & "_ucCanalesPlan"
                AddHandler _ucCanales.Erro, AddressOf ErroControles
                phCanal.Controls.Add(_ucCanales)
            End If
            Return _ucCanales
        End Get
        Set(value As ucCanal)
            _ucCanales = value
        End Set
    End Property



    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = False
        Me.ucClientes.SubClienteHabilitado = False
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.ucSubCliente.MultiSelecao = False

        Me.ucClientes.TipoBanco = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub

    Private Sub ConfigurarControle_Canal()

        Me.ucCanales.SelecaoMultipla = True
        Me.ucCanales.CanalHabilitado = True
        Me.ucCanales.SubCanalHabilitado = True
        Me.ucCanales.ucSubCanal.MultiSelecao = True

        If Canales IsNot Nothing Then
            Me.ucCanales.Canales = Canales
        End If

    End Sub

    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub ucCanales_OnControleAtualizado() Handles _ucCanales.UpdatedControl
        Try
            If ucCanales.Canales IsNot Nothing Then
                Canales = ucCanales.Canales
            End If

            If ucCanales.Canales IsNot Nothing AndAlso CanalesOriginal IsNot Nothing AndAlso
                CanalesOriginal.Count = ucCanales.Canales.Count Then

                For Each objCanal As Canal In ucCanales.Canales

                    If CanalesOriginal.FirstOrDefault(Function(x) x.Identificador = objCanal.Identificador) Is Nothing Then
                        ValidarPeriodoAbierto()
                    End If
                Next
            ElseIf ucCanales.Canales IsNot Nothing AndAlso CanalesOriginal IsNot Nothing AndAlso
                CanalesOriginal.Count <> ucCanales.Canales.Count Then
                ValidarPeriodoAbierto()
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[HelpersCliente FOrmulario]"
    Public Property ClientesForm As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesForm.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesForm.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientesForm As ucCliente
    Public Property ucClientesForm() As ucCliente
        Get
            If _ucClientesForm Is Nothing Then
                _ucClientesForm = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesForm.ID = Me.ID & "_ucClientesForm"
                AddHandler _ucClientesForm.Erro, AddressOf ErroControles
                phClientesForm.Controls.Add(_ucClientesForm)
            End If
            Return _ucClientesForm
        End Get
        Set(value As ucCliente)
            _ucClientesForm = value
        End Set
    End Property


    Public Property BancoComisionForm As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucBancoComisionForm.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucBancoComisionForm.Clientes = value
        End Set
    End Property


    Private WithEvents _ucBancoComisionForm As ucCliente
    Public Property ucBancoComisionForm() As ucCliente
        Get
            If _ucBancoComisionForm Is Nothing Then
                _ucBancoComisionForm = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucBancoComisionForm.ID = Me.ID & "_ucBancoComisionForm"
                AddHandler _ucBancoComisionForm.Erro, AddressOf ErroControles
                phBancoComisionform.Controls.Add(_ucBancoComisionForm)
            End If
            Return _ucBancoComisionForm
        End Get
        Set(value As ucCliente)
            _ucBancoComisionForm = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClienteFormulario()

        Me.ucClientesForm.SelecaoMultipla = False
        Me.ucClientesForm.ClienteHabilitado = True
        Me.ucClientesForm.ClienteObrigatorio = True

        Me.ucClientesForm.TipoBanco = True

        If ClientesForm IsNot Nothing Then
            Me.ucClientesForm.Clientes = ClientesForm
        End If


        Me.ucBancoComisionForm.SelecaoMultipla = False
        Me.ucBancoComisionForm.ClienteHabilitado = True
        Me.ucBancoComisionForm.ClienteObrigatorio = True
        Me.ucBancoComisionForm.EsBancoComision = True

        Me.ucBancoComisionForm.TipoBanco = True

        If BancoComisionForm IsNot Nothing Then
            Me.ucBancoComisionForm.Clientes = BancoComisionForm
        End If

    End Sub
    Private Sub ucClientesForm_OnControleAtualizado() Handles _ucClientesForm.UpdatedControl
        Try

            If ucClientesForm.Clientes IsNot Nothing Then
                ClientesForm = ucClientesForm.Clientes
                If ClientesForm.Count > 0 Then
                    btnCodigoAjenoBanco.Enabled = True
                    imgBancoCapital.Enabled = True
                Else
                    btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain_disabled.png"
                    btnCodigoAjenoBanco.Enabled = False
                    imgBancoCapital.Enabled = False
                    CodigoAjenoBanco = Nothing
                End If
            Else
                btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain_disabled.png"
                btnCodigoAjenoBanco.Enabled = False
                imgBancoCapital.Enabled = False
                CodigoAjenoBanco = Nothing
            End If
            BuscarCodigosAjenos()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub ucBancoComisionForm_OnControleAtualizado() Handles _ucBancoComisionForm.UpdatedControl
        Try

            If ucBancoComisionForm.Clientes IsNot Nothing Then
                BancoComisionForm = ucBancoComisionForm.Clientes
                If BancoComisionForm.Count > 0 Then
                    btnCodigoAjenoBancoBancoComision.Enabled = True
                Else
                    btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain_disabled.png"
                    btnCodigoAjenoBancoBancoComision.Enabled = False
                    CodigoAjenoBancoComision = Nothing
                End If
            Else
                btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain_disabled.png"
                btnCodigoAjenoBancoBancoComision.Enabled = False
                CodigoAjenoBancoComision = Nothing
            End If
            BuscarCodigosAjenosBancoComision()


        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "ucLimitePlanificacion"
    Private WithEvents _ucLimitePlanificacion As ucLimite
    Public Property ucLimitePlanificacion() As ucLimite
        Get
            If _ucLimitePlanificacion Is Nothing Then
                _ucLimitePlanificacion = LoadControl(ResolveUrl("~\Controles\ucLimite.ascx"))
                _ucLimitePlanificacion.ID = Me.ID & "_ucLimitePlanificacion"
                phLimitePlanificacion.Controls.Add(_ucLimitePlanificacion)
            End If
            Return _ucLimitePlanificacion
        End Get
        Set(value As ucLimite)
            _ucLimitePlanificacion = value
        End Set
    End Property
    Public Sub LimpiarUcLimitePlanificacion()
        ucLimitePlanificacion.Planificacion = Nothing
        ucLimitePlanificacion.CargaRegistrosDeBase()
    End Sub
#End Region

#Region "ucDivisaPlanificacion"
    Private WithEvents _ucDivisaPlanificacion As ucDivisa
    Public Property ucDivisaPlanificacion() As ucDivisa
        Get
            If _ucDivisaPlanificacion Is Nothing Then
                _ucDivisaPlanificacion = LoadControl(ResolveUrl("~\Controles\ucDivisa.ascx"))
                _ucDivisaPlanificacion.ID = Me.ID & "_ucDivisaPlanificacion"
                phDivisaPlanificacion.Controls.Add(_ucDivisaPlanificacion)
            End If
            Return _ucDivisaPlanificacion
        End Get
        Set(value As ucDivisa)
            _ucDivisaPlanificacion = value
        End Set
    End Property
    Public Sub LimpiarUcDivisaPlanificacion()
        ucDivisaPlanificacion.Planificacion = Nothing
        ucDivisaPlanificacion.CargaRegistrosDeBase()
    End Sub
#End Region
#Region "ucMovimientoPlanificacion"
    Private WithEvents _ucMovimientoPlanificacion As ucMovimiento
    Public Property ucMovimientoPlanificacion() As ucMovimiento
        Get
            If _ucMovimientoPlanificacion Is Nothing Then
                _ucMovimientoPlanificacion = LoadControl(ResolveUrl("~\Controles\ucMovimiento.ascx"))
                _ucMovimientoPlanificacion.ID = Me.ID & "_ucMovimientoPlanificacion"
                phMovimientoPlanificacion.Controls.Add(_ucMovimientoPlanificacion)
            End If
            Return _ucMovimientoPlanificacion
        End Get
        Set(value As ucMovimiento)
            _ucMovimientoPlanificacion = value
        End Set
    End Property
    Public Sub LimpiarUcMovimientoPlanificacion()
        ucMovimientoPlanificacion.Planificacion = Nothing
        ucMovimientoPlanificacion.CargaRegistrosDeBase()
    End Sub
#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        Try
            MyBase.AdicionarScripts()
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaInicio.ClientID, "True")
            script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaFin.ClientID, "True")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            txtNombrePlanificacion.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

            txtComisionClienteForm.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal4(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
            'txtComisionClienteForm.Attributes.Add("onkeypress", "bloqueialetrasAceitaNegativo(event,this);")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PLANIFICACION
        ' desativar validação de ação
        MyBase.ValidarAcao = True
        MyBase.CodFuncionalidad = "ABM_PLANIFICACION"

    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Planificaciones")
            ASPxGridView.RegisterBaseScript(Page)

            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = True
            Master.HabilitarMenu = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.MenuGrande = True

            script = String.Empty

            If Not Page.IsPostBack Then

                Clientes = Nothing
                Canales = Nothing

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False
                btnAddMAE.Enabled = False
                btnCamposExtrasModal.Enabled = False

                PreencherddlDelegacion()
                PreencherTiposPlanificaciones(True, True)
                PreencherdllPatronConfirmacion()
                PreencherddlProcesoSapr()

                ValidarFechaValorConConfirmacion()

                Canales.Clear()
                Canales.Add(New Prosegur.Genesis.Comon.Clases.Canal)
                '  AtualizaDadosHelperCanal(Canales, ucCanales)

                If Not MyBase.ValidarAcaoPagina(MyBase.PaginaAtual, Utilidad.eAcao.Alta) Then
                    btnNovo.Enabled = False
                    btnNovo.Visible = False
                    btnCamposExtrasModal.Enabled = False
                End If

                CargarParametros()

            End If
            'Limite
            Me.ucLimitePlanificacion.ConfigurarControles()
            'Divisa
            Me.ucDivisaPlanificacion.ConfigurarControles()
            'Movimiento
            Me.ucMovimientoPlanificacion.ConfigurarControles()

            ConfigurarControle_Cliente()
            ConfigurarControle_ClienteFormulario()

            ConfigurarControle_Canal()

            'ddlDelegacionForm_SelectedIndexChanged(Nothing, Nothing)

            If Acao = Utilidad.eAcao.Alta OrElse Acao = Utilidad.eAcao.Modificacion Then
                HabilitarDiasSemana()
            End If

            TrataFoco()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub PreencherdllPatronConfirmacion()
        Dim lista = New List(Of String)
        lista.Add("Totalizado")
        ' lista.Add("Transaccional")

        ddlPatronConfirmacion.AppendDataBoundItems = True
        ddlPatronConfirmacion.Items.Clear()
        ddlPatronConfirmacion.DataSource = lista.ToList()
        ddlPatronConfirmacion.DataBind()
    End Sub

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
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        Try
            Master.Titulo = MyBase.RecuperarValorDic("lbl_titulo_busqueda")

            lblSubTituloPlanificaciones.Text = MyBase.RecuperarValorDic("lbl_sub_planificaciones")
            lblSubTitulosCriteriosBusqueda.Text = MyBase.RecuperarValorDic("lbl_criterios_busqueda")

            ddlEstado.Items(0).Text = MyBase.RecuperarValorDic("lblTodos")
            ddlEstado.Items(1).Text = MyBase.RecuperarValorDic("lbl_vigente")
            ddlEstado.Items(2).Text = MyBase.RecuperarValorDic("lbl_no_vigente")

            'Botoes
            btnBuscar.Text = MyBase.RecuperarValorDic("btn_buscar")
            btnLimpar.Text = MyBase.RecuperarValorDic("btn_limpiar")
            btnNovo.Text = MyBase.RecuperarValorDic("btn_alta")
            btnCancelar.Text = MyBase.RecuperarValorDic("btn_cancelar")
            btnGrabar.Text = MyBase.RecuperarValorDic("btn_grabar")
            btnGrabarConfirm.Text = MyBase.RecuperarValorDic("btn_grabar")
            btnAddMAE.Text = MyBase.RecuperarValorDic("lbl_agregar_maes")
            btnCamposExtrasModal.Text = MyBase.RecuperarValorDic("btnCamposExtrasModal")

            btnBuscar.ToolTip = MyBase.RecuperarValorDic("btn_buscar")
            btnLimpar.ToolTip = MyBase.RecuperarValorDic("btn_limpiar")
            btnNovo.ToolTip = MyBase.RecuperarValorDic("btn_alta")
            btnCancelar.ToolTip = MyBase.RecuperarValorDic("btn_cancelar")
            btnGrabar.ToolTip = MyBase.RecuperarValorDic("btn_grabar")
            btnAddMAE.ToolTip = MyBase.RecuperarValorDic("lbl_agregar_maes")
            btnGrabarConfirm.ToolTip = MyBase.RecuperarValorDic("btn_grabar")
            btnBajaConfirm.Text = Tradutor.Traduzir("btnBaja")
            btnBajaConfirm.ToolTip = Tradutor.Traduzir("btnBaja")
            btnCamposExtrasModal.ToolTip = MyBase.RecuperarValorDic("btnCamposExtrasModal")

            'Grid Pesquisa
            GdvResultado.Columns(1).HeaderText = MyBase.RecuperarValorDic("lbl_grd_banco")
            GdvResultado.Columns(2).HeaderText = MyBase.RecuperarValorDic("lbl_grd_nombre")
            GdvResultado.Columns(3).HeaderText = MyBase.RecuperarValorDic("lbl_grd_tipo_planificacion")
            GdvResultado.Columns(4).HeaderText = MyBase.RecuperarValorDic("lbl_grd_vigente")
            GdvResultado.Columns(5).HeaderText = MyBase.RecuperarValorDic("lbl_grd_lunes")
            GdvResultado.Columns(6).HeaderText = MyBase.RecuperarValorDic("lbl_grd_martes")
            GdvResultado.Columns(7).HeaderText = MyBase.RecuperarValorDic("lbl_grd_miercoles")
            GdvResultado.Columns(8).HeaderText = MyBase.RecuperarValorDic("lbl_grd_jueves")
            GdvResultado.Columns(9).HeaderText = MyBase.RecuperarValorDic("lbl_grd_viernes")
            GdvResultado.Columns(10).HeaderText = MyBase.RecuperarValorDic("lbl_grd_sabado")
            GdvResultado.Columns(11).HeaderText = MyBase.RecuperarValorDic("lbl_grd_domingo")

            'GRID Programaciones
            GdvHorarios.Columns(0).HeaderText = MyBase.RecuperarValorDic("lbl_grd_lunes")
            GdvHorarios.Columns(1).HeaderText = MyBase.RecuperarValorDic("lbl_grd_martes")
            GdvHorarios.Columns(2).HeaderText = MyBase.RecuperarValorDic("lbl_grd_miercoles")
            GdvHorarios.Columns(3).HeaderText = MyBase.RecuperarValorDic("lbl_grd_jueves")
            GdvHorarios.Columns(4).HeaderText = MyBase.RecuperarValorDic("lbl_grd_viernes")
            GdvHorarios.Columns(5).HeaderText = MyBase.RecuperarValorDic("lbl_grd_sabado")
            GdvHorarios.Columns(6).HeaderText = MyBase.RecuperarValorDic("lbl_grd_domingo")

            'Filtros
            lblNombrePlanificacion.Text = MyBase.RecuperarValorDic("lbl_nombre")
            ucClientes.ClienteTitulo = MyBase.RecuperarValorDic("lbl_banco")
            lblTipoPlanificacion.Text = MyBase.RecuperarValorDic("lbl_tipo_planificacion")
            lblEstado.Text = MyBase.RecuperarValorDic("lbl_estado")

            'Formulario            
            lblTituloForm.Text = RecuperarValorDic("lbl_tit_pantalla_form")
            ucClientes.ClienteTitulo = RecuperarValorDic("lbl_banco")

            'Limite
            lblTituloLimite.Text = MyBase.RecuperarValorDic("lblLimites")
            'Procesos
            lblProcesos.Text = MyBase.RecuperarValorDic("lblProcesos")


            If chkControlaFacturacionForm.Checked Then
                ucClientesForm.ClienteTitulo = RecuperarValorDic("lbl_banco_capital")
            Else
                ucClientesForm.ClienteTitulo = RecuperarValorDic("lbl_banco")
            End If

            ucBancoComisionForm.ClienteTitulo = RecuperarValorDic("lbl_banco_comision")

            lblNombreForm.Text = RecuperarValorDic("lbl_grd_nombre")
            lblPatronConfirmacion.Text = RecuperarValorDic("lblPatronConfirmacion")
            btnMensajes.Text = RecuperarValorDic("btnMensajes")
            lblCodigoForm.Text = RecuperarValorDic("lbl_codigo_planificacion")
            lblTipoForm.Text = RecuperarValorDic("lbl_tipo_planificacion")
            lblVigenteForm.Text = RecuperarValorDic("lbl_vigente")
            lblFechaInicio.Text = RecuperarValorDic("lbl_fecha_inicio_vigencia")
            lblFechaFin.Text = RecuperarValorDic("lbl_fecha_fin_vigencia")
            lblDelegacionForm.Text = RecuperarValorDic("lbl_fecha_delegacion")
            lblSubTituloMAES.Text = RecuperarValorDic("lbl_subtitulo_mae")
            lblMinutosAcreditacionForm.Text = RecuperarValorDic("lbl_minutos_acreditacion")
            lblControlaFacturacionForm.Text = RecuperarValorDic("lblControlaFacturacionForm")
            lblComisionPlanificacionForm.Text = MyBase.RecuperarValorDic("lblComisionPlanificacionForm")
            lblFaturacion.Text = MyBase.RecuperarValorDic("lblFacturacion")
            lblDiaCierreForm.Text = MyBase.RecuperarValorDic("lblDiaCierreForm")
            csvNombreForm.ErrorMessage = RecuperarValorDic("msg_nombre_obrigatorio")
            csvCodigoForm.ErrorMessage = RecuperarValorDic("msg_codigo_obrigatorio")
            csvTipoForm.ErrorMessage = RecuperarValorDic("msg_tipo_obrigatorio")
            csvFechaInicio.ErrorMessage = RecuperarValorDic("msg_fyh_ini_obrigatorio")
            csvMinutosAcreditacionForm.ErrorMessage = RecuperarValorDic("msg_minutos_acreditacion_obrigatorio")
            lblFiltroAsociacionMovimientos.Text = RecuperarValorDic("lblFiltroAsociacionMovimientos")
            lblDivisionPeriodos.Text = RecuperarValorDic("lblDivisionPeriodos")
            lblDivisionSubcanal.Text = RecuperarValorDic("lblDivisionSubcanal")
            lblDivisionDivisa.Text = RecuperarValorDic("lblDivisionDivisa")
            lblAgrupValores.Text = RecuperarValorDic("lblAgrupValores")
            lblPorSubCanal.Text = RecuperarValorDic("lblPorSubCanal")
            lblPorPuntoServicio.Text = RecuperarValorDic("lblPorPuntoServicio")
            lblPorFechaContable.Text = RecuperarValorDic("lblPorFechaContable")
            lbl_horarios.Text = RecuperarValorDic("lbl_horarios")
            grid.Columns(0).Caption = MyBase.RecuperarValorDic("DeviceID")
            grid.Columns(1).Caption = MyBase.RecuperarValorDic("Descripcion")
            grid.Columns(2).Caption = MyBase.RecuperarValorDic("PorComision")
            grid.Columns(3).Caption = MyBase.RecuperarValorDic("BancoTesoreria")
            grid.Columns(4).Caption = MyBase.RecuperarValorDic("Datosbancarios")
            grid.Columns(5).Caption = MyBase.RecuperarValorDic("Vigente")
            grid.Columns(6).Caption = MyBase.RecuperarValorDic("Cambiar")
            grid.Columns(7).Caption = MyBase.RecuperarValorDic("Quitar")

            'Variavel para ser acessada via javascript
            Dim _Diccionario As New Dictionary(Of String, String)() From {
           {"btnSim", Tradutor.Traduzir("gen_opcion_si")},
           {"btnNao", Tradutor.Traduzir("gen_opcion_no")},
           {"titulo_msg", Tradutor.Traduzir("aplicacao")},
           {"msg_horario_cambiado", MyBase.RecuperarValorDic("MSG_INFO_MODIFICACION_HORARIOS")}}

            litDicionario.Text = "<script> var _Diccionario = JSON.parse('" & JsonConvert.SerializeObject(_Diccionario) & "'); </script>"


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


#End Region

#Region "[METODOS]"

    Private Sub BuscarCodigosAjenos()

        Try

            If _ucClientesForm.Clientes IsNot Nothing AndAlso _ucClientesForm.Clientes.Count > 0 Then

                btnCodigoAjenoBanco.Enabled = True

                Dim respCliente = GetCodigosAjenos(_ucClientesForm.Clientes.First.Identificador, "GEPR_TCLIENTE")
                If respCliente.EntidadCodigosAjenos IsNot Nothing _
                        AndAlso respCliente.EntidadCodigosAjenos.Count > 0 _
                            AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos IsNot Nothing _
                                AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos.Count > 0 Then


                    CodigoAjenoBanco = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.NoDefinido.RecuperarValor(),
                                                   .CodAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.CodAjeno,
                                                   .DesAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.DesAjeno,
                                                   .OidCodigoAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.OidCodigoAjeno}


                    btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain01.png"
                    imgBancoCapital.Enabled = True

                Else
                    btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/nocontain01.png"
                    imgBancoCapital.Enabled = True
                    CodigoAjenoBanco = Nothing
                End If
            Else
                btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain_disabled.png"
                btnCodigoAjenoBanco.Enabled = False
                imgBancoCapital.Enabled = False
                CodigoAjenoBanco = Nothing
            End If

            ValidarCodigoAjeno()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub BuscarCodigosAjenosBancoComision()


        Try

            If _ucBancoComisionForm.Clientes IsNot Nothing AndAlso _ucBancoComisionForm.Clientes.Count > 0 Then

                btnCodigoAjenoBancoBancoComision.Enabled = True

                Dim respCliente = GetCodigosAjenos(_ucBancoComisionForm.Clientes.First.Identificador, "GEPR_TCLIENTE")
                If respCliente.EntidadCodigosAjenos IsNot Nothing _
                        AndAlso respCliente.EntidadCodigosAjenos.Count > 0 _
                            AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos IsNot Nothing _
                                AndAlso respCliente.EntidadCodigosAjenos.First.CodigosAjenos.Count > 0 Then


                    CodigoAjenoBancoComision = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.NoDefinido.RecuperarValor(),
                                                   .CodAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.CodAjeno,
                                                   .DesAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.DesAjeno,
                                                   .OidCodigoAjeno = respCliente.EntidadCodigosAjenos.First.CodigosAjenos.First.OidCodigoAjeno}


                    btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain01.png"
                    imgBancoComisionDatosBancariosForm.Enabled = True

                Else
                    btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/nocontain01.png"
                    imgBancoComisionDatosBancariosForm.Enabled = True
                    CodigoAjenoBancoComision = Nothing
                End If
            Else
                btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain_disabled.png"
                btnCodigoAjenoBancoBancoComision.Enabled = False
                imgBancoComisionDatosBancariosForm.Enabled = False
                CodigoAjenoBancoComision = Nothing
            End If

            ValidarCodigoAjenoBancoComision()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub ValidarCodigoAjeno()

        If _ucClientesForm.Clientes IsNot Nothing AndAlso _ucClientesForm.Clientes.Count > 0 Then
            If CodigoAjenoBanco IsNot Nothing AndAlso Not String.IsNullOrEmpty(CodigoAjenoBanco.CodAjeno) AndAlso Not String.IsNullOrEmpty(CodigoAjenoBanco.DesAjeno) Then

                btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain01.png"
                imgBancoCapital.Enabled = True
            Else
                btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/nocontain01.png"
                imgBancoCapital.Enabled = True
                CodigoAjenoBanco = Nothing

            End If
        Else
            btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain_disabled.png"
            btnCodigoAjenoBanco.Enabled = False
            imgBancoCapital.Enabled = False
            CodigoAjenoBanco = Nothing
        End If

    End Sub

    Private Sub ValidarCodigoAjenoBancoComision()


        If _ucBancoComisionForm.Clientes IsNot Nothing AndAlso _ucBancoComisionForm.Clientes.Count > 0 Then
            If CodigoAjenoBancoComision IsNot Nothing AndAlso Not String.IsNullOrEmpty(CodigoAjenoBancoComision.CodAjeno) AndAlso Not String.IsNullOrEmpty(CodigoAjenoBancoComision.DesAjeno) Then

                btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain01.png"
                imgBancoComisionDatosBancariosForm.Enabled = True
            Else
                btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/nocontain01.png"
                imgBancoComisionDatosBancariosForm.Enabled = True
                CodigoAjenoBancoComision = Nothing

            End If
        Else
            btnCodigoAjenoBancoBancoComision.ImageUrl = "~/Imagenes/contain_disabled.png"
            btnCodigoAjenoBancoBancoComision.Enabled = False
            imgBancoComisionDatosBancariosForm.Enabled = False
            CodigoAjenoBancoComision = Nothing

        End If


    End Sub

    ''' <summary>
    ''' Obtém os dados do codigo ajeno por oidTablaGenesis
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 26/04/2013 Criado
    ''' </history>
    Private Function GetCodigosAjenos(idTablaGenesis As String, codTipoTablaGenesis As String) As ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = codTipoTablaGenesis
        objPeticion.CodigosAjeno.OidTablaGenesis = idTablaGenesis
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno

        ' chamar servicio
        Return objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

    End Function

    ''' Preencher a dropdownbox de delegaciones
    Private Sub PreencherddlDelegacion()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objAccionDelegacion.GetDelegaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Delegacion.Count = 0 Then
            ddlDelegacionForm.Items.Clear()
            Exit Sub
        End If

        If objRespuesta.Delegacion.Count > 0 Then
            Delegaciones = objRespuesta.Delegacion
            Dim lista = objRespuesta.Delegacion.Select(Function(a) New With {.OidDelegacion = a.OidDelegacion,
                                                                             .DesDelegacion = a.DesDelegacion,
                                                                             .CodDesDelegacion = a.CodDelegacion + " - " + a.DesDelegacion}).OrderBy(Function(b) b.CodDesDelegacion)

            ddlDelegacionForm.AppendDataBoundItems = True
            ddlDelegacionForm.Items.Clear()
            ddlDelegacionForm.DataTextField = "CodDesDelegacion"
            ddlDelegacionForm.DataValueField = "OidDelegacion"
            ddlDelegacionForm.DataSource = lista.ToList()
            ddlDelegacionForm.DataBind()

            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            Dim delegacion = objRespuesta.Delegacion.Find(Function(d) d.CodDelegacion = usuario.CodigoDelegacion)
            ddlDelegacionForm.SelectedValue = delegacion.OidDelegacion

        End If


    End Sub

    Private Function GetDelegacionSelecionada(oidDelegacion As String) As Comon.Clases.Delegacion
        Dim delegacaoSelecionada As New Comon.Clases.Delegacion
        Dim delegacion = Delegaciones.Find(Function(d) d.OidDelegacion = oidDelegacion)
        If (delegacion IsNot Nothing) Then
            delegacaoSelecionada.AjusteHorarioVerano = delegacion.NecVeranoAjuste
            delegacaoSelecionada.Codigo = delegacion.CodDelegacion
            delegacaoSelecionada.HusoHorarioEnMinutos = delegacion.NecGmtMinutos
            delegacaoSelecionada.FechaHoraVeranoInicio = delegacion.FyhVeranoInicio
            delegacaoSelecionada.FechaHoraVeranoFin = delegacion.FyhVeranoFin
            delegacaoSelecionada.Descripcion = delegacion.DesDelegacion
        End If

        Return delegacaoSelecionada
    End Function

    ''' Preencher a dropdownbox de procesos
    Private Sub PreencherddlProcesoSapr()
        Dim objPeticion As New ContractoServicio.Proceso.GetProceso.Peticion
        Dim objRespuesta As New ContractoServicio.Proceso.GetProceso.Respuesta
        Dim objAccionProceso As New LogicaNegocio.AccionProceso

        objRespuesta = objAccionProceso.GetProcesoSapr(objPeticion)
        chkbxlstProcesos.DataTextField = "DescripcionProceso"
        chkbxlstProcesos.DataValueField = "OidProceso"
        chkbxlstProcesos.DataSource = objRespuesta.ProcesosSapr
        chkbxlstProcesos.DataBind()
    End Sub

    ''' <summary>
    ''' Gera a informação de GMT por delegação
    ''' </summary>
    ''' <param name="delegacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerarInfoGmt(delegacion As Comon.Clases.Delegacion)
        Dim dadosDelegacion As String = ""
        Dim juncao As String = ": "
        Dim separator As String = Aplicacao.Util.Utilidad.LineBreak

        dadosDelegacion = RecuperarChavesDic("lbl_gmt_minutos") + juncao + delegacion.HusoHorarioEnMinutos.ToString + separator
        dadosDelegacion += RecuperarChavesDic("lbl_ajuste_verano") + juncao + delegacion.AjusteHorarioVerano.ToString

        If delegacion.FechaHoraVeranoInicio <> DateTime.MinValue AndAlso delegacion.FechaHoraVeranoInicio.Year > 2000 Then
            dadosDelegacion += separator + RecuperarChavesDic("lbl_fecha_inicio_verano") + juncao + delegacion.FechaHoraVeranoInicio.ToString("dd/MM/yyyy")
        End If

        If delegacion.FechaHoraVeranoFin <> DateTime.MinValue AndAlso delegacion.FechaHoraVeranoInicio.Year > 2000 Then
            dadosDelegacion += separator + RecuperarChavesDic("lbl_fecha_fin_verano") + juncao + delegacion.FechaHoraVeranoFin.ToString("dd/MM/yyyy")
        End If

        Return dadosDelegacion
    End Function
    Protected Sub ddlDelegacionForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacionForm.SelectedIndexChanged

        Dim delegacion = GetDelegacionSelecionada(ddlDelegacionForm.SelectedValue)
        If delegacion IsNot Nothing Then

            Dim info = GenerarInfoGmt(delegacion)

            Dim fechaHora = DateTime.UtcNow

            Dim fecha = fechaHora.QuieroExibirEstaFechaEnLaPatalla(delegacion)

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ActualizarData", "ActualizarData('" + fecha.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + info + "');", True)
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
        pUserControl.ucCliente.ExibirDados(False)
    End Sub

    Private Sub LimpiarForm()
        txtNombreForm.Text = String.Empty
        txtCodigoForm.Text = String.Empty
        txtMinutosAcreditacionForm.Text = String.Empty
        txtFechaFin.Text = String.Empty
        txtFechaInicio.Text = String.Empty
        hdfCambioHorario.Value = String.Empty

        PreencherTiposPlanificaciones(True, False)
        Canales.Clear()
        CanalesOriginal = Nothing
        chkVigenteForm.Checked = False
        PropPlanificacion = New Planificacion
        Session("objBusquedaMAE") = Nothing
        Maquinas = New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)

        GenerarListaHorarios()

        ClientesForm.Clear()
        ClientesForm.Add(New Prosegur.Genesis.Comon.Clases.Cliente)

        BancoComisionForm.Clear()
        AtualizaDadosHelperCliente(BancoComisionForm, ucBancoComisionForm)
        txtComisionClienteForm.Text = String.Empty
        txtDiaCierreForm.Text = String.Empty
        chkControlaFacturacionForm.Checked = False
        chkPorSubCanal.Checked = False
        chkPorPuntoServicio.Checked = False
        chkPorFechaContable.Checked = False
        chkDivisionSubcanal.Checked = False
        chkDivisionDivisa.Checked = False

        Dim controls = ucClientesForm.ucCliente.Controls
        script = "$('#" + ucClientesForm.ucCliente.ClientID + "_lblTitulo').text('" + RecuperarValorDic("lbl_banco") + "');"

        DatosBancarios = Nothing
        BancoComision = Nothing
        PorComisionPlanificacion = String.Empty
        DiaCierreFacturacion = String.Empty

        CamposExtrasDinamicosDatos = Nothing
        CamposExtrasPatronesDatos = Nothing

        grid.DataSource = Me.Maquinas
        grid.DataBind()

        'Limite
        LimpiarUcLimitePlanificacion()
        'DIvisa
        LimpiarUcDivisaPlanificacion()
        'Movimiento
        LimpiarUcMovimientoPlanificacion()
    End Sub

    Public Sub PreencherTiposPlanificaciones(form As Boolean, filtro As Boolean)

        Dim objPeticion As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Peticion
        Dim objRespuesta As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta
        Dim logicaNegocio As New LogicaNegocio.AccionTipoPlanificacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = logicaNegocio.getTiposPlanificaciones(objPeticion)

        TiposPlanificacion = objRespuesta.TiposPlanificaciones

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.TiposPlanificaciones.Count = 0 Then
            If filtro Then
                LimpiarTiposPlanificaciones(ddlTipoPlanificacion)
            End If
            If form Then
                LimpiarTiposPlanificaciones(ddlTipoPlanificacionForm)
            End If
            Exit Sub
        End If

        If objRespuesta.TiposPlanificaciones.Count > 0 Then

            If filtro Then
                CargarComboPlanificaciones(ddlTipoPlanificacion, objRespuesta)
            End If

            If form Then
                CargarComboPlanificaciones(ddlTipoPlanificacionForm, objRespuesta)
            End If
        End If

    End Sub

    Private Sub CargarComboPlanificaciones(ddl As DropDownList, objRespuesta As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta)
        ddl.AppendDataBoundItems = True
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(MyBase.RecuperarValorDic("btnSeleccionar"), String.Empty))
        ddl.DataTextField = "desTipoPlanificacion"
        ddl.DataValueField = "oidTipoPlanificacion"
        ddl.DataSource = objRespuesta.TiposPlanificaciones
        ddl.DataBind()
    End Sub

    Private Sub LimpiarTiposPlanificaciones(ddl As DropDownList)
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem(MyBase.RecuperarValorDic("btnSeleccionar"), String.Empty))
    End Sub

    Private Sub LimparCamposFiltro()
        Try

            txtNombrePlanificacion.Text = String.Empty
            ddlEstado.SelectedIndex = 0
            ddlTipoPlanificacion.SelectedIndex = 0

            PreencherTiposPlanificaciones(False, True)

            Clientes.Clear()
            Clientes.Add(New Prosegur.Genesis.Comon.Clases.Cliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub GetHorarioInformado(txtCorrente As TextBox, diaDaSemana As Integer, Planificaciones As List(Of Comon.Clases.PlanXProgramacion))
        Dim planificacion As Comon.Clases.PlanXProgramacion = Nothing

        If Not String.IsNullOrEmpty(txtCorrente.Text) Then
            planificacion = New Comon.Clases.PlanXProgramacion
            planificacion.NecDiaFin = diaDaSemana

            '  La fecha será grabada con el menor valor permitido: 01/01/0001 
            Dim horaInformada = DateTime.Parse(txtCorrente.Text)
            planificacion.FechaHoraFin = New Date(1, 1, 1, horaInformada.Hour, horaInformada.Minute, horaInformada.Second)

        End If

        If planificacion IsNot Nothing Then
            Planificaciones.Add(planificacion)
        End If

    End Sub

    ''' <summary>
    ''' Genera lineas para informar las programaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerarListaHorarios()

        Dim listaHorarios As New List(Of Comon.Clases.PlanXProgramacion)

        For index = 1 To Comon.Constantes.TOTAL_LINEAS
            Dim programacion As New Comon.Clases.PlanXProgramacion
            programacion.FyhLunes = String.Empty
            programacion.FyhMartes = String.Empty
            programacion.FyhMiercoles = String.Empty
            programacion.FyhJueves = String.Empty
            programacion.FyhViernes = String.Empty
            programacion.FyhSabado = String.Empty
            programacion.FyhDomingo = String.Empty

            programacion.LunesHabilitado = IIf(index = 1, True, False)
            programacion.MartesHabilitado = IIf(index = 1, True, False)
            programacion.MiercolesHabilitado = IIf(index = 1, True, False)
            programacion.JuevesHabilitado = IIf(index = 1, True, False)
            programacion.ViernesHabilitado = IIf(index = 1, True, False)
            programacion.SabadoHabilitado = IIf(index = 1, True, False)
            programacion.DomingoHabilitado = IIf(index = 1, True, False)

            listaHorarios.Add(programacion)
        Next

        CarregarProgramaciones(listaHorarios)
    End Sub

    Private Sub GenerarListaHorariosLinhas(linhas As Int32)

        Dim listaHorarios As New List(Of Comon.Clases.PlanXProgramacion)

        For index = 1 To linhas
            Dim programacion As New Comon.Clases.PlanXProgramacion
            programacion.FyhLunes = String.Empty
            programacion.FyhMartes = String.Empty
            programacion.FyhMiercoles = String.Empty
            programacion.FyhJueves = String.Empty
            programacion.FyhViernes = String.Empty
            programacion.FyhSabado = String.Empty
            programacion.FyhDomingo = String.Empty

            programacion.LunesHabilitado = IIf(index = 1, True, False)
            programacion.MartesHabilitado = IIf(index = 1, True, False)
            programacion.MiercolesHabilitado = IIf(index = 1, True, False)
            programacion.JuevesHabilitado = IIf(index = 1, True, False)
            programacion.ViernesHabilitado = IIf(index = 1, True, False)
            programacion.SabadoHabilitado = IIf(index = 1, True, False)
            programacion.DomingoHabilitado = IIf(index = 1, True, False)

            listaHorarios.Add(programacion)
        Next

        CarregarProgramaciones(listaHorarios)
    End Sub

    Public Class Retorno
        Public Property HayModificacion As Boolean
        Public Property Valor As String
    End Class

    <System.Web.Services.WebMethod(True)>
    Public Shared Function VerificaModificacionHorario(valor As String, campo As String, numero_campo As Integer) As String

        Try

            Dim retorno As New Retorno()

            Dim hayModificacion As Boolean
            Dim valorAnterior As String = ""
            ' Dim colunaRepetida As Boolean
            If HttpContext.Current.Session("programaciones") IsNot Nothing Then
                Dim programaciones = DirectCast(HttpContext.Current.Session("programaciones"), List(Of Comon.Clases.PlanXProgramacion))

                Dim indice = numero_campo - 1

                If (programaciones IsNot Nothing AndAlso programaciones.Count > 0) Then

                    Select Case campo
                        Case "txtLunes"
                            hayModificacion = IIf(programaciones(indice).FyhLunes = valor, False, True)
                            valorAnterior = programaciones(indice).FyhLunes
                            ' colunaRepetida = programaciones.Where(Function(e) e.FyhLunes = valor).Count > 0
                        Case "txtMartes"
                            hayModificacion = IIf(programaciones(indice).FyhMartes = valor, False, True)
                            valorAnterior = programaciones(indice).FyhMartes
                        Case "txtMiercoles"
                            hayModificacion = IIf(programaciones(indice).FyhMiercoles = valor, False, True)
                            valorAnterior = programaciones(indice).FyhMiercoles
                        Case "txtJueves"
                            hayModificacion = IIf(programaciones(indice).FyhJueves = valor, False, True)
                            valorAnterior = programaciones(indice).FyhJueves
                        Case "txtViernes"
                            hayModificacion = IIf(programaciones(indice).FyhViernes = valor, False, True)
                            valorAnterior = programaciones(indice).FyhViernes
                        Case "txtSabado"
                            hayModificacion = IIf(programaciones(indice).FyhSabado = valor, False, True)
                            valorAnterior = programaciones(indice).FyhSabado
                        Case Else
                            hayModificacion = IIf(programaciones(indice).FyhDomingo = valor, False, True)
                            valorAnterior = programaciones(indice).FyhDomingo

                    End Select

                End If

            End If

            retorno.HayModificacion = hayModificacion
            retorno.Valor = valorAnterior

            Return JsonConvert.SerializeObject(retorno, New Converters.StringEnumConverter())

        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GenerarProgramaciones() As List(Of Comon.Clases.PlanXProgramacion)
        'Percorrer o grid lendo os horarios de fim e preenchendo a lista de programações
        Dim Programaciones As New List(Of Comon.Clases.PlanXProgramacion)

        For Each dtRow As GridViewRow In GdvHorarios.Rows

            Dim planificacion As New Comon.Clases.PlanXProgramacion

            Dim txtLunes As TextBox = dtRow.Cells(0).FindControl("txtLunes")
            Dim txtMartes As TextBox = dtRow.Cells(0).FindControl("txtMartes")
            Dim txtMiercoles As TextBox = dtRow.Cells(0).FindControl("txtMiercoles")
            Dim txtJueves As TextBox = dtRow.Cells(0).FindControl("txtJueves")
            Dim txtViernes As TextBox = dtRow.Cells(0).FindControl("txtViernes")
            Dim txtSabado As TextBox = dtRow.Cells(0).FindControl("txtSabado")
            Dim txtDomingo As TextBox = dtRow.Cells(0).FindControl("txtDomingo")

            GetHorarioInformado(txtLunes, Comon.Constantes.LUNES, Programaciones)
            GetHorarioInformado(txtMartes, Comon.Constantes.MARTES, Programaciones)
            GetHorarioInformado(txtMiercoles, Comon.Constantes.MIERCOLES, Programaciones)
            GetHorarioInformado(txtJueves, Comon.Constantes.JUEVES, Programaciones)
            GetHorarioInformado(txtViernes, Comon.Constantes.VIERNES, Programaciones)
            GetHorarioInformado(txtSabado, Comon.Constantes.SABADO, Programaciones)
            GetHorarioInformado(txtDomingo, Comon.Constantes.DOMINGO, Programaciones)
        Next

        Return Programaciones
    End Function

    Private Sub SetarCampo(txt As TextBox)
        If Not txt.Enabled Then
            txt.Enabled = Not String.IsNullOrEmpty(txt.Text)
        End If
        'Armazena o valor atual para comparar em caso de edição do grid de horarios
        txt.Attributes.Add("valor_anterior", txt.Text)
    End Sub

    ''' <summary>
    ''' Percorre o grid de horarios para manter o estado dos campos 
    ''' após o postback da tela 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HabilitarDiasSemana()

        Dim jsScript As String = ""

        For Each dtRow As GridViewRow In GdvHorarios.Rows

            Dim planificacion As New Comon.Clases.PlanXProgramacion

            Dim txtLunes As TextBox = dtRow.Cells(0).FindControl("txtLunes")
            Dim txtMartes As TextBox = dtRow.Cells(0).FindControl("txtMartes")
            Dim txtMiercoles As TextBox = dtRow.Cells(0).FindControl("txtMiercoles")
            Dim txtJueves As TextBox = dtRow.Cells(0).FindControl("txtJueves")
            Dim txtViernes As TextBox = dtRow.Cells(0).FindControl("txtViernes")
            Dim txtSabado As TextBox = dtRow.Cells(0).FindControl("txtSabado")
            Dim txtDomingo As TextBox = dtRow.Cells(0).FindControl("txtDomingo")

            SetarCampo(txtLunes)
            SetarCampo(txtMartes)
            SetarCampo(txtMiercoles)
            SetarCampo(txtJueves)
            SetarCampo(txtViernes)
            SetarCampo(txtSabado)
            SetarCampo(txtDomingo)

            jsScript += "HabilitaTxt(" & Chr(34) & txtLunes.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtMartes.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtMiercoles.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtViernes.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtJueves.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtSabado.UniqueID & Chr(34) & ");"
            jsScript += "HabilitaTxt(" & Chr(34) & txtDomingo.UniqueID & Chr(34) & ");"

        Next
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "HabilitaTxt", jsScript, True)

    End Sub

    Private Function GetMaquinasSelecionadas() As List(Of Comon.Clases.Maquina)
        Dim lstMaquinas As New List(Of Comon.Clases.Maquina)

        For Each item In Maquinas
            If lstMaquinas.FirstOrDefault(Function(x) x.Identificador = item.OidMaquina) Is Nothing Then
                Dim maquina As New Comon.Clases.Maquina

                maquina.Identificador = item.OidMaquina
                'Almacenamos el deviceID para crear los periodos en caso de que cambie el horario de la programacion de la planificación.
                maquina.Codigo = item.DeviceID
                lstMaquinas.Add(maquina)
            End If
        Next
        Return lstMaquinas
    End Function

    Private Function ConvertDataHoraGMT(data As String)
        Dim delegacion = GetDelegacionSelecionada(ddlDelegacionForm.SelectedValue)
        Dim culturaActual As CultureInfo = CultureInfo.CurrentCulture.Clone


        If culturaActual.Name.Equals("en-US") Then

            Dim culturaArgentina As CultureInfo = New CultureInfo("es-AR")

            culturaActual.DateTimeFormat = culturaArgentina.DateTimeFormat
            Threading.Thread.CurrentThread.CurrentCulture = culturaActual
            Threading.Thread.CurrentThread.CurrentUICulture = culturaActual

        End If


        Dim dtGmt = Convert.ToDateTime(data).QuieroGrabarGMTZeroEnLaBBDD(IIf(delegacion IsNot Nothing, delegacion, MyBase.DelegacionLogada))


        Return IIf(dtGmt = DateTime.MinValue, Nothing, dtGmt)
    End Function

    Private Sub CargarParametros()
        Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion
        Dim respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Respuesta
        peticion.codigoAplicacion = Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS
        peticion.codigoDelegacion = MyBase.DelegacionLogada.Codigo
        peticion.codigosParametro = New List(Of String)

        peticion.codigosParametro.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_PERIODO_FUTUROS)
        peticion.codigosParametro.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_INICIO_VIGENCIA)

        respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

        If respuesta IsNot Nothing Then
            Parametros = respuesta.parametros
        End If

    End Sub

    Public Sub ExecutarGrabar()
        Try

            Dim objPlanificacion As New Comon.Clases.Planificacion
            Dim Maquinas As New List(Of Comon.Clases.Maquina)

            objPlanificacion.Cliente = New Comon.Clases.Cliente
            objPlanificacion.Cliente.Identificador = ucClientesForm.Clientes.FirstOrDefault().Identificador
            objPlanificacion.TipoPlanificacion = New Comon.Clases.TipoPlanificacion
            objPlanificacion.TipoPlanificacion.Identificador = ddlTipoPlanificacionForm.SelectedValue
            objPlanificacion.Codigo = txtCodigoForm.Text
            objPlanificacion.Descripcion = txtNombreForm.Text
            objPlanificacion.Delegacion = New Delegacion
            objPlanificacion.Delegacion.Identificador = ddlDelegacionForm.SelectedValue
            objPlanificacion.Canales = Canales.ToList()
            objPlanificacion.NecContigencia = Integer.Parse(txtMinutosAcreditacionForm.Text)

            objPlanificacion.BolControlaFacturacion = chkControlaFacturacionForm.Checked

            If ucBancoComisionForm.Clientes IsNot Nothing AndAlso ucBancoComisionForm.Clientes.Count > 0 Then
                objPlanificacion.BancoComision = ucBancoComisionForm.Clientes.FirstOrDefault()
            End If

            If Not String.IsNullOrWhiteSpace(txtComisionClienteForm.Text) Then
                objPlanificacion.PorcComisionPlanificacion = Double.Parse(txtComisionClienteForm.Text)
            End If

            If Not String.IsNullOrWhiteSpace(txtDiaCierreForm.Text) Then
                If Integer.Parse(txtDiaCierreForm.Text) > 0 And Integer.Parse(txtDiaCierreForm.Text) <= 31 Then
                    objPlanificacion.DiaCierreFacturacion = Integer.Parse(txtDiaCierreForm.Text)
                Else
                    Dim mensajeExcepcion As String = String.Empty
                    mensajeExcepcion = String.Format(RecuperarChavesDic("MSG_WARN_DAY_MONTH_INVALID"), Me.lblDiaCierreForm.Text)
                    Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(mensajeExcepcion)
                End If


            End If


            objPlanificacion.Programacion = GenerarProgramaciones()
            If objPlanificacion.Programacion Is Nothing OrElse objPlanificacion.Programacion.Count = 0 Then

                Dim txtHoraPatron As New TextBox
                txtHoraPatron.Text = "00:00:00"

                GetHorarioInformado(txtHoraPatron, Comon.Constantes.LUNES, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.MARTES, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.MIERCOLES, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.JUEVES, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.VIERNES, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.SABADO, objPlanificacion.Programacion)
                GetHorarioInformado(txtHoraPatron, Comon.Constantes.DOMINGO, objPlanificacion.Programacion)

            End If


            objPlanificacion.Maquinas = GetMaquinasSelecionadas()
            objPlanificacion.BolActivo = chkVigenteForm.Checked

            objPlanificacion.CodigosAjeno = New List(Of CodigoAjeno)
            objPlanificacion.BolAgrupaPorSubCanal = chkPorSubCanal.Checked
            objPlanificacion.BolAgrupaPorPuntoServicio = chkPorPuntoServicio.Checked
            objPlanificacion.BolAgrupaPorFechaContable = chkPorFechaContable.Checked





            If CodigoAjenoBanco IsNot Nothing Then

                Dim obj = New CodigoAjeno With {
                .Codigo = CodigoAjenoBanco.CodAjeno,
                .Descripcion = CodigoAjenoBanco.DesAjeno,
                .Identificador = CodigoAjenoBanco.OidCodigoAjeno,
                .IdentificadorTablaGenesis = ucClientesForm.Clientes.First.Identificador,
                .CodigoIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                .EsActivo = True
                }
                objPlanificacion.CodigoAjenoCliente = obj
                objPlanificacion.CodigosAjeno.Add(obj)

            End If

            If CodigoAjenoBancoComision IsNot Nothing Then

                Dim obj = New CodigoAjeno With {
                .Codigo = CodigoAjenoBancoComision.CodAjeno,
                .Descripcion = CodigoAjenoBancoComision.DesAjeno,
                .Identificador = CodigoAjenoBancoComision.OidCodigoAjeno,
                .IdentificadorTablaGenesis = ucBancoComisionForm.Clientes.First.Identificador,
                .CodigoIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                .EsActivo = True
                }

                objPlanificacion.CodigoAjenoBancoComision = obj
                objPlanificacion.CodigosAjeno.Add(obj)

            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPlanificacion.BolCambioHorario = True
            Else
                objPlanificacion.BolCambioHorario = IIf(hdfCambioHorario.Value = "HorarioCambiado", True, False)
                objPlanificacion.Identificador = PropPlanificacion.Identificador

            End If

            Dim delegacion = GetDelegacionSelecionada(ddlDelegacionForm.SelectedValue)

            objPlanificacion.FechaHoraVigenciaInicio = ConvertDataHoraGMT(txtFechaInicio.Text)

            Dim fechaFim As DateTime? = Nothing

            If Not String.IsNullOrEmpty(txtFechaFin.Text) Then
                objPlanificacion.FechaHoraVigenciaFin = ConvertDataHoraGMT(txtFechaFin.Text)
                fechaFim = Convert.ToDateTime(txtFechaFin.Text)
            End If

            objPlanificacion.BolCambioHorario = False

            If Convert.ToDateTime(txtFechaInicio.Text) <> PropPlanificacion.FechaHoraVigenciaInicio OrElse
                  (PropPlanificacion.FechaHoraVigenciaFin.HasValue <> fechaFim.HasValue OrElse
                fechaFim <> PropPlanificacion.FechaHoraVigenciaFin) Then

                objPlanificacion.BolCambioHorario = True
            End If


            'TODO CompararHorario con el almacendao
            objPlanificacion.BolCambioHorarioProgramacion = False

            If PropPlanificacion.ProgramacionOriginal.Count <> objPlanificacion.Programacion.Count Then
                objPlanificacion.BolCambioHorarioProgramacion = True
            End If


            If PropPlanificacion IsNot Nothing AndAlso objPlanificacion.BolCambioHorarioProgramacion = False Then

                For Each programacionActual In objPlanificacion.Programacion
                    Dim existe = PropPlanificacion.ProgramacionOriginal.Any(Function(x) x.NecDiaFin = programacionActual.NecDiaFin AndAlso x.FechaHoraFin = programacionActual.FechaHoraFin)
                    If Not existe Then
                        objPlanificacion.BolCambioHorarioProgramacion = True
                        Exit For
                    End If
                Next

            End If


            objPlanificacion.CamposExtrasDinamicos = CamposExtrasDinamicosDatos
            objPlanificacion.CamposExtrasPatrones = CamposExtrasPatronesDatos

            'Limite
            objPlanificacion.Limites = ucLimitePlanificacion.BuscarPeticionLimite()


            '---INICIO DATOS FECHA VALOR CON CONFIRMACION
            Dim tipoPlanificacionSeleccionada = TiposPlanificacion.FirstOrDefault(Function(x) x.oidTipoPlanificacion = ddlTipoPlanificacionForm.SelectedValue)
            If tipoPlanificacionSeleccionada IsNot Nothing AndAlso tipoPlanificacionSeleccionada.codTipoPlanificacion = "FECHA_VALOR_CONFIR" Then

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion AndAlso PropPlanificacion.TipoPlanificacion.Identificador <> objPlanificacion.TipoPlanificacion.Identificador Then
                    objPlanificacion.BolCambioTipoPlaniFVC = True
                End If

                objPlanificacion.PatronConfirmacion = ddlPatronConfirmacion.SelectedValue.ToUpper()

                objPlanificacion.BolDividePorSubcanal = chkDivisionSubcanal.Checked
                objPlanificacion.BolDividePorDivisa = chkDivisionDivisa.Checked
                If (chkDivisionDivisa.Checked) Then
                    Dim resultadoValidacionDivisa As String = ""
                    ValidarParametroDivisaDefecto(resultadoValidacionDivisa)
                    'If (resultadoValidacionDivisa <> "") Then MyBase.MostraMensagem(resultadoValidacionDivisa)
                    If (resultadoValidacionDivisa <> "") Then Throw New Prosegur.Genesis.Excepcion.NegocioExcepcion(MyBase.RecuperarValorDic(resultadoValidacionDivisa))

                End If

                'Divisa
                objPlanificacion.Divisas = ucDivisaPlanificacion.BuscarPeticionDivisa()
                'Movimiento
                objPlanificacion.Movimientos = ucMovimientoPlanificacion.BuscarPeticionMovimiento()
                objPlanificacion.Mensajes = PropPlanificacion.Mensajes
                'Proceso
                objPlanificacion.Procesos = GetProcesosListSelectedValue()
            End If
            '--- FIN DATOS FECHA VALOR CON CONFIRMACION

            Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.GrabarPlanificacion(objPlanificacion, DatosBancarios, MyBase.LoginUsuario, objPlanificacion.FechaHoraVigenciaInicio)

            If objPlanificacion.CodigoAjenoCliente IsNot Nothing Then
                CodigoAjenoBanco.OidCodigoAjeno = objPlanificacion.CodigoAjenoCliente.Identificador
            End If

            Dim mensagem = IIf(Acao = Utilidad.eAcao.Alta, "MSG_INFO_MAE_EXITO_ALTA", "MSG_INFO_PLANIFICACION_EXITO_MOFICACION")

            MyBase.MostraMensagem(String.Format(MyBase.RecuperarValorDic(mensagem), objPlanificacion.Codigo, objPlanificacion.Descripcion))

            Cancelar()

            btnBuscar_Click(Nothing, Nothing)

        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            Dim mensagemTratada = IIf(ex.Descricao.Contains("MSG_INFO_PLANIFICACION_EXISTENTE"), RecuperarChavesDic("MSG_INFO_PLANIFICACION_EXISTENTE"), ex.Descricao)
            MyBase.MostraMensagem(mensagemTratada)

        Catch ex As Exception
            MyBase.MostraMensagem(ex.ToString())
        End Try

    End Sub

    ''' <summary>
    ''' Converte as maquinas retornadas do banco de dados para o objeto da tela
    ''' </summary>
    ''' <param name="objListaMaquina"></param>
    ''' <remarks></remarks>
    Private Function ConverterMaquinas(objListaMaquina As List(Of Comon.Clases.Maquina))

        Dim maquinasSelecionadas As New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)


        Dim peticion = New Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Peticion()

        peticion.IdentificadorMaquina = objListaMaquina.Select(Function(x) x.Identificador).ToList()
        respuestaComissionDatosBancarios = New Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta()

        Genesis.LogicaNegocio.Genesis.Pantallas.Planificaciones.RecuperarBancoTesoreriaEComission(peticion, respuestaComissionDatosBancarios)

        For Each item In objListaMaquina
            Dim maquina As New ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina
            maquina.OidMaquina = item.Identificador
            maquina.DeviceID = item.Codigo
            maquina.Descripcion = item.Descripcion
            maquina.BolActivo = item.BolActivo


            maquina.OidCliente = item.Cliente.Identificador
            maquina.CodigoCliente = item.Cliente.Codigo
            maquina.Cliente = item.Cliente.Descripcion

            maquina.OidSubCliente = item.SubCliente.Identificador
            maquina.CodigoSubCliente = item.SubCliente.Codigo
            maquina.SubCliente = item.SubCliente.Descripcion

            maquina.OidPtoServicio = item.PtoServicio.Identificador
            maquina.PtoServicio = item.PtoServicio.Descripcion
            maquina.CodigoPtoServicio = item.PtoServicio.Codigo

            Dim PorcComis = respuestaComissionDatosBancarios.MAEs.FirstOrDefault(Function(x) x.OID_MAQUINA = item.Identificador)
            If PorcComis.NUM_PORCENT_COMISION.HasValue Then

                maquina.NumPorcentComision = String.Format("{0:N4}", PorcComis.NUM_PORCENT_COMISION) 'PorcComis.NUM_PORCENT_COMISION.ToString("D4")
            End If
            Dim lblBancoTesoreria = String.Empty

            If Not String.IsNullOrWhiteSpace(PorcComis.COD_SUBCLIENTE) Then
                lblBancoTesoreria = PorcComis.COD_SUBCLIENTE + " - " + PorcComis.DES_SUBCLIENTE
            End If


            If Not String.IsNullOrWhiteSpace(PorcComis.COD_PTO_SERVICIO) Then
                If Not String.IsNullOrWhiteSpace(lblBancoTesoreria) Then
                    lblBancoTesoreria += " / "
                End If
                lblBancoTesoreria += PorcComis.COD_PTO_SERVICIO + " - " + PorcComis.DES_PTO_SERVICIO
            End If

            maquina.BancoTesoreria = lblBancoTesoreria

            maquinasSelecionadas.Add(maquina)
        Next
        Maquinas = maquinasSelecionadas
        Return maquinasSelecionadas
    End Function

    Private Sub CarregarMaquinas(Maquinas As List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina))

        Me.Maquinas = Maquinas
        grid.DataSource = Me.Maquinas
        grid.DataBind()
    End Sub




    Protected Sub imgCapitalDBancariosForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Dim item = ucClientesForm.Clientes(0)

        ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID
        ucDatosBanc.Cliente.Identificador = item.Identificador

        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

            tablaGenesis.OidCliente = item.Identificador
            tablaGenesis.CodCliente = item.Codigo
            tablaGenesis.DesCliente = item.Descripcion

            If DatosBancarios Is Nothing Then
                DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
            End If
            If DatosBancarios.ContainsKey("BANCO_CAPITAL") Then
                tablaGenesis.DatosBancarios = DatosBancarios("BANCO_CAPITAL")
            End If

            Session("objMantenimientoCamposExtras") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtrasCliente.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub



    Protected Sub imgDBancariosForm_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)

        Dim item = ucBancoComisionForm.Clientes(0)

        ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID
        ucDatosBanc.Cliente.Identificador = item.Identificador

        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

            tablaGenesis.OidCliente = item.Identificador
            tablaGenesis.CodCliente = item.Codigo
            tablaGenesis.DesCliente = item.Descripcion

            If DatosBancarios Is Nothing Then
                DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
            End If
            If DatosBancarios.ContainsKey("CLIENTE") Then
                tablaGenesis.DatosBancarios = DatosBancarios("CLIENTE")
            End If

            Session("objMantenimientoCamposExtras") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtrasCliente.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub




    Private Sub btnCamposExtras_Click(sender As Object, e As EventArgs) Handles btnCamposExtras.Click
        Try
            If Session("objMantenimientoCamposExtras") IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCamposExtra.CamposExtras = Session("objMantenimientoCamposExtras")

                If tablaGenesis IsNot Nothing Then
                    If DatosBancarios Is Nothing Then
                        DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
                    End If
                    If DatosBancarios.ContainsKey(tablaGenesis.OidPuntoServicio) Then
                        DatosBancarios(tablaGenesis.OidPuntoServicio) = tablaGenesis.DatosBancarios
                    Else
                        DatosBancarios.Add(tablaGenesis.OidPuntoServicio, tablaGenesis.DatosBancarios)
                    End If
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnMae_Click(sender As Object, e As EventArgs) Handles btnMae.Click


        If Not String.IsNullOrWhiteSpace(Session("objResultModalMae")) Then
            MyBase.MostraMensagem(Session("objResultModalMae"))
        End If

        Dim maquinasSelecionadas As New List(Of ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina)
        Dim peticion = New Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Peticion()

        peticion.IdentificadorMaquina = respuestaComissionDatosBancarios.MAEs.Select(Function(x) x.OID_MAQUINA).ToList()
        Prosegur.Genesis.LogicaNegocio.Genesis.Pantallas.Planificaciones.RecuperarBancoTesoreriaEComission(peticion, respuestaComissionDatosBancarios)

        For Each maquina In Maquinas


            Dim PorcComis = respuestaComissionDatosBancarios.MAEs.FirstOrDefault(Function(x) x.OID_MAQUINA = maquina.OidMaquina)


            If PorcComis Is Nothing Then
                Continue For
            End If

            If PorcComis.NUM_PORCENT_COMISION.HasValue Then

                maquina.NumPorcentComision = String.Format("{0:N4}", PorcComis.NUM_PORCENT_COMISION) 'PorcComis.NUM_PORCENT_COMISION.ToString("D4")
            Else
                maquina.NumPorcentComision = String.Empty
            End If
            Dim lblBancoTesoreria = String.Empty

            If Not String.IsNullOrWhiteSpace(PorcComis.COD_SUBCLIENTE) Then
                lblBancoTesoreria = PorcComis.COD_SUBCLIENTE + " - " + PorcComis.DES_SUBCLIENTE
            End If


            If Not String.IsNullOrWhiteSpace(PorcComis.COD_PTO_SERVICIO) Then
                If Not String.IsNullOrWhiteSpace(lblBancoTesoreria) Then
                    lblBancoTesoreria += " / "
                End If
                lblBancoTesoreria += PorcComis.COD_PTO_SERVICIO + " - " + PorcComis.DES_PTO_SERVICIO
            End If

            maquina.BancoTesoreria = lblBancoTesoreria

        Next
        grid.DataSource = Me.Maquinas
        grid.DataBind()
    End Sub
    Private Sub btnCamposExtrasCliente_Click(sender As Object, e As EventArgs) Handles btnCamposExtrasCliente.Click
        Try
            If Session("objMantenimientoCamposExtras") IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCamposExtra.CamposExtras = Session("objMantenimientoCamposExtras")

                If tablaGenesis IsNot Nothing Then
                    If DatosBancarios Is Nothing Then
                        DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
                    End If
                    If DatosBancarios.ContainsKey("CLIENTE") Then
                        DatosBancarios("CLIENTE") = tablaGenesis.DatosBancarios
                    Else
                        DatosBancarios.Add("CLIENTE", tablaGenesis.DatosBancarios)
                    End If

                    If DatosBancarios.ContainsKey("BANCO_CAPITAL") Then
                        DatosBancarios("BANCO_CAPITAL") = tablaGenesis.DatosBancarios
                    Else
                        DatosBancarios.Add("BANCO_CAPITAL", tablaGenesis.DatosBancarios)
                    End If

                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Property CamposExtrasPatronesDatos() As Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC
        Get
            If Session("camposExtrasPatronesDatos") Is Nothing Then
                Session("camposExtrasPatronesDatos") = New Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC()
            End If
            Return Session("camposExtrasPatronesDatos")
        End Get
        Set(ByVal value As Prosegur.Genesis.Comon.Clases.CamposExtrasDeIAC)
            Session("camposExtrasPatronesDatos") = value
        End Set
    End Property

    Public Property CamposExtrasDinamicosDatos() As CamposExtrasDeIAC
        Get
            If Session("camposExtrasDinamicosDatos") Is Nothing Then
                Session("camposExtrasDinamicosDatos") = New CamposExtrasDeIAC()
            End If
            Return Session("camposExtrasDinamicosDatos")
        End Get
        Set(ByVal value As CamposExtrasDeIAC)
            Session("camposExtrasDinamicosDatos") = value
        End Set
    End Property

    Private Sub CarregaDados(oidPlanificacion As String)
        Try

            Dim objPlanificacion = Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.RecuperarPlanificacionDetalle(oidPlanificacion, MyBase.LoginUsuario, Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor())

            If objPlanificacion IsNot Nothing Then

                'Preencher Planificacion
                txtNombreForm.Text = objPlanificacion.Descripcion
                txtFechaFin.Text = objPlanificacion.FechaHoraVigenciaFin.ToString
                txtCodigoForm.Text = objPlanificacion.Codigo
                ddlTipoPlanificacionForm.SelectedValue = objPlanificacion.TipoPlanificacion.Identificador
                ddlPatronConfirmacion.SelectedValue = "Totalizado"
                txtMinutosAcreditacionForm.Text = objPlanificacion.NecContigencia.ToString
                chkVigenteForm.Checked = objPlanificacion.BolActivo
                ddlDelegacionForm.SelectedValue = objPlanificacion.Delegacion.Identificador
                ddlDelegacionForm_SelectedIndexChanged(Nothing, Nothing)
                chkPorSubCanal.Checked = objPlanificacion.BolAgrupaPorSubCanal
                chkPorPuntoServicio.Checked = objPlanificacion.BolAgrupaPorPuntoServicio
                chkPorFechaContable.Checked = objPlanificacion.BolAgrupaPorFechaContable
                chkDivisionSubcanal.Checked = objPlanificacion.BolDividePorSubcanal
                chkDivisionDivisa.Checked = objPlanificacion.BolDividePorDivisa

                'A datas já estão convertidas para GMT da delegação
                If objPlanificacion.FechaHoraVigenciaInicio = DateTime.MinValue Then
                    txtFechaFin.Text = String.Empty
                Else
                    txtFechaInicio.Text = objPlanificacion.FechaHoraVigenciaInicio.ToString("dd/MM/yyyy HH:mm:ss")
                End If

                If (objPlanificacion.FechaHoraVigenciaFin IsNot Nothing) Then
                    If objPlanificacion.FechaHoraVigenciaInicio = DateTime.MinValue Then
                        txtFechaFin.Text = String.Empty
                    Else
                        txtFechaFin.Text = objPlanificacion.FechaHoraVigenciaFin.Value.ToString("dd/MM/yyyy HH:mm:ss")
                    End If
                End If

                objPlanificacion.FechaVigenciaFinGmt = txtFechaFin.Text
                objPlanificacion.FechaVigenciaInicioGmt = txtFechaInicio.Text

                'Banco               
                ClientesForm.Clear()
                ClientesForm.Add(objPlanificacion.Cliente)
                AtualizaDadosHelperCliente(ClientesForm, ucClientesForm)

                BancoComisionForm.Clear()

                If objPlanificacion.BancoComision IsNot Nothing Then
                    BancoComisionForm.Add(objPlanificacion.BancoComision)
                End If

                AtualizaDadosHelperCliente(BancoComisionForm, ucBancoComisionForm)

                chkControlaFacturacionForm.Checked = objPlanificacion.BolControlaFacturacion

                Dim termoDic As String = String.Empty

                If chkControlaFacturacionForm.Checked Then
                    termoDic = RecuperarValorDic("lbl_banco_capital")
                Else
                    termoDic = RecuperarValorDic("lbl_banco")
                End If

                Dim controls = ucClientesForm.ucCliente.Controls
                script = "$('#" + ucClientesForm.ucCliente.ClientID + "_lblTitulo').text('" + RecuperarValorDic(termoDic) + "');"

                If objPlanificacion.DiaCierreFacturacion IsNot Nothing Then
                    txtDiaCierreForm.Text = objPlanificacion.DiaCierreFacturacion.ToString()
                End If

                If objPlanificacion.PorcComisionPlanificacion IsNot Nothing Then
                    txtComisionClienteForm.Text = objPlanificacion.PorcComisionPlanificacion.ToString()
                End If

                'Preencher maquinas
                CarregarMaquinas(ConverterMaquinas(objPlanificacion.Maquinas))

                'Preencher horarios                
                CarregarProgramacionesLinhas(objPlanificacion.Programacion)

                'Preencher canales
                CarregarCanales(objPlanificacion.Canales)

                CamposExtrasPatronesDatos = objPlanificacion.CamposExtrasPatrones
                CamposExtrasDinamicosDatos = objPlanificacion.CamposExtrasDinamicos

                PropPlanificacion = objPlanificacion

                If PropPlanificacion IsNot Nothing AndAlso PropPlanificacion.CodigoAjenoCliente IsNot Nothing Then


                    CodigoAjenoBanco = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                   .CodIdentificador = Comon.Enumeradores.CodigoAjeno.NoDefinido.RecuperarValor(),
                                                   .CodAjeno = PropPlanificacion.CodigoAjenoCliente.Codigo,
                                                   .DesAjeno = PropPlanificacion.CodigoAjenoCliente.Descripcion,
                                                   .OidCodigoAjeno = PropPlanificacion.CodigoAjenoCliente.Identificador}


                End If
                BuscarCodigosAjenos()
                BuscarCodigosAjenosBancoComision()
                ValidarCodigoAjeno()
                ValidarCodigoAjenoBancoComision()

                ExibeValidacionCanal = True
                ExibeValidacionHorario = True
                HabilitaDesabilitaFacturacion()

                'Limite
                ucLimitePlanificacion.Planificacion = objPlanificacion
                ucLimitePlanificacion.CargaRegistrosDeBase()

                'Divisa
                ucDivisaPlanificacion.Planificacion = objPlanificacion
                ucDivisaPlanificacion.CargaRegistrosDeBase()
                'Movimiento
                ucMovimientoPlanificacion.Planificacion = objPlanificacion
                ucMovimientoPlanificacion.CargaRegistrosDeBase()
                'Proceso
                For i As Integer = 0 To chkbxlstProcesos.Items.Count - 1
                    chkbxlstProcesos.Items(i).Selected = False
                    If objPlanificacion.Procesos.Any(Function(x) x.Identificador = chkbxlstProcesos.Items(i).Value) Then
                        chkbxlstProcesos.Items(i).Selected = True
                    End If
                Next

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub CarregarCanales(lstCanal As List(Of Comon.Clases.Canal))

        Canales.Clear()

        For Each item In lstCanal
            Canales.Add(item)
        Next

        CanalesOriginal = Canales.Clonar

    End Sub
    Private Sub CarregarProgramaciones(objListaProgramaciones As List(Of Comon.Clases.PlanXProgramacion))
        Sequencial = 0

        GdvHorarios.CarregaControle(GdvHorarios.ConvertListToDataTable(objListaProgramaciones))

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "formataHora", "formataHora();", True)

    End Sub

    Private Sub CarregarProgramacionesLinhas(objListaProgramaciones As List(Of Comon.Clases.PlanXProgramacion))
        Sequencial = 0

        If objListaProgramaciones.Count > Comon.Constantes.TOTAL_LINEAS Then
            GenerarListaHorariosLinhas(objListaProgramaciones.Count)
        Else
            GenerarListaHorariosLinhas(Comon.Constantes.TOTAL_LINEAS)
        End If

        GdvHorarios.CarregaControle(GdvHorarios.ConvertListToDataTable(objListaProgramaciones))

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "formataHora", "formataHora();", True)

    End Sub

    Private Function GetCanaisSelecionados() As List(Of Comon.Clases.Canal)
        Return Canales.ToList()
    End Function

    Private Sub HabilitarDesabilitaForm(habilita As Boolean)
        pnUcClienteform.Enabled = habilita
        txtCodigoForm.Enabled = habilita
        txtFechaFin.Enabled = habilita
        txtFechaInicio.Enabled = habilita
        txtMinutosAcreditacionForm.Enabled = habilita
        ddlTipoPlanificacionForm.Enabled = habilita
        ddlDelegacionForm.Enabled = habilita
        ddlPatronConfirmacion.Enabled = habilita
        'btnMensajes.Enabled = habilita
        chkVigenteForm.Enabled = habilita
        txtNombreForm.Enabled = habilita
        pnUcCanales.Enabled = habilita
        GdvHorarios.Enabled = habilita
        btnRemLinha.Enabled = habilita
        btnAdcLinha.Enabled = habilita
        pnUcClienteform.Enabled = habilita
        chkControlaFacturacionForm.Enabled = habilita
        pnUcBancoComisionform.Enabled = habilita
        txtComisionClienteForm.Enabled = habilita
        txtDiaCierreForm.Enabled = habilita
        chkPorSubCanal.Enabled = habilita
        chkPorPuntoServicio.Enabled = habilita
        chkPorFechaContable.Enabled = habilita
        chkDivisionSubcanal.Enabled = habilita
        chkDivisionDivisa.Enabled = habilita
        HabilitaDesabilitaFacturacion()

        grid.Columns(6).Visible = habilita
    End Sub

    ''' <summary>
    ''' Validacion de fechas de inicio e fim de la vigencia
    ''' La “Fecha y Hora Inicio Vigencia” no podrá ser igual o mayor que el valor de la “Fecha y Hora Fin Vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarFechaVigencia() As String
        Dim strErro As New Text.StringBuilder(String.Empty)

        Dim dtInicio As DateTime
        Dim dtFim As DateTime
        dtInicio = ConvertDataHoraGMT(txtFechaInicio.Text)

        If Not String.IsNullOrEmpty(txtFechaFin.Text) Then

            dtFim = ConvertDataHoraGMT(txtFechaFin.Text)

            If (dtInicio >= dtFim) Then
                strErro.Append(RecuperarValorDic("MSG_INFO_FYHS_VIGENCIA_INVALIDAS") & Aplicacao.Util.Utilidad.LineBreak)
            End If

        End If
        Return strErro.ToString
    End Function


    ''' <summary>
    ''' Validacion de fechas de inicio e fim de la vigencia
    ''' La “Fecha y Hora Inicio Vigencia” no podrá ser menor que el valor de la fecha y hora actual + la suma de las horas configuradas en el parámetro “CantidadHorasInicioVigencia 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidarHoraInicioVigencia() As String
        Dim strErro As New Text.StringBuilder(String.Empty)

        Dim dtInicio As DateTime

        dtInicio = ConvertDataHoraGMT(txtFechaInicio.Text)

        Dim CantidadHorasInicioVigencia = GetValorParametro(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_INICIO_VIGENCIA)

        If Not String.IsNullOrEmpty(CantidadHorasInicioVigencia) Then
            Dim horasInicio = Integer.Parse(CantidadHorasInicioVigencia)

            Dim dataInicioVigencia = DateTime.UtcNow.AddHours(horasInicio)

            If dtInicio < dataInicioVigencia Then
                strErro.Append(String.Format(RecuperarValorDic("MSG_INFO_FYH_VIGENCIA_INICIO_INVALIDA"), CantidadHorasInicioVigencia) & Aplicacao.Util.Utilidad.LineBreak)
            End If
        End If
        Return strErro.ToString
    End Function

    Public Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ClientesForm Is Nothing OrElse ClientesForm.Count = 0 OrElse String.IsNullOrEmpty(ClientesForm.FirstOrDefault().Codigo) Then
                strErro.Append(RecuperarValorDic("msg_csvFiltroBanco") & Aplicacao.Util.Utilidad.LineBreak)
            End If

            'Verifica se o tipo de planificação foi selecionado
            If ddlTipoPlanificacionForm.Visible AndAlso ddlTipoPlanificacionForm.SelectedValue.Equals(String.Empty) Then

                strErro.Append(csvTipoForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvTipoForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    ddlTipoPlanificacionForm.Focus()
                    focoSetado = True
                End If
            Else
                csvTipoForm.IsValid = True
            End If

            If txtCodigoForm.Text.Trim.Equals(String.Empty) Then

                strErro.Append(csvCodigoForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoForm.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoForm.IsValid = True
            End If

            If txtNombreForm.Text.Trim.Equals(String.Empty) Then

                strErro.Append(csvNombreForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvNombreForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtNombreForm.Focus()
                    focoSetado = True
                End If
            Else
                csvNombreForm.IsValid = True
            End If

            Dim arrCanales = GetCanaisSelecionados()

            If arrCanales.Count <= 0 Then

                strErro.Append(RecuperarValorDic("msg_canal_obrigatorio") & Aplicacao.Util.Utilidad.LineBreak)

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    focoSetado = True
                End If
            Else

                Dim incompleteSubCanal = arrCanales.Any(Function(x) x.SubCanales Is Nothing OrElse x.SubCanales.Count = 0)
                If incompleteSubCanal Then
                    strErro.Append(RecuperarValorDic("MSG_SUBCANAL_OBRIGATORIO") & Aplicacao.Util.Utilidad.LineBreak)
                End If

            End If

            If txtMinutosAcreditacionForm.Text.Trim.Equals(String.Empty) Then

                strErro.Append(csvMinutosAcreditacionForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvMinutosAcreditacionForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtMinutosAcreditacionForm.Focus()
                    focoSetado = True
                End If
            Else
                csvMinutosAcreditacionForm.IsValid = True
            End If

            If String.IsNullOrEmpty(txtFechaInicio.Text) Then
                strErro.Append(RecuperarValorDic("msg_fyh_ini_obrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
            Else
                If Acao = Utilidad.eAcao.Modificacion Then
                    If txtFechaInicio.Text <> PropPlanificacion.FechaVigenciaInicioGmt Then
                        strErro.Append(ValidarHoraInicioVigencia())
                    End If
                Else
                    strErro.Append(ValidarHoraInicioVigencia())
                End If
                strErro.Append(ValidarFechaVigencia())
            End If


            Dim lstProg = GenerarProgramaciones()

            If lstProg.Count > 0 Then

                Dim groupTipo = (From row In lstProg
                                 Group row By
                                     DiaSemana = row.NecDiaFin Into Group
                                 Select
                                     DiaSemana, Group).ToList


                For Each tipo In groupTipo

                    Dim repetidos = tipo.Group.Where(Function(x) tipo.Group.Where(Function(y) x.FechaHoraFin = y.FechaHoraFin).Count() > 1).Distinct().Count

                    If repetidos > 0 Then

                        Dim desDiaSemana As String = ""
                        Select Case tipo.DiaSemana
                            Case 1
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_lunes")
                            Case 2
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_martes")
                            Case 3
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_miercoles")
                            Case 4
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_jueves")
                            Case 5
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_viernes")
                            Case 6
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_sabado")
                            Case Else
                                desDiaSemana = MyBase.RecuperarValorDic("lbl_grd_domingo")

                        End Select

                        strErro.Append(String.Format(RecuperarValorDic("MSG_INFO_HORARIO_REPETIDO"), desDiaSemana) & Aplicacao.Util.Utilidad.LineBreak)

                        Exit For
                    End If

                Next
            ElseIf ddlTipoPlanificacionForm.Visible AndAlso Not ddlTipoPlanificacionForm.SelectedItem.Text.Equals("Online") Then
                strErro.Append(RecuperarValorDic("MSG_INFO_HORARIO_NO_INFORMADO") & Aplicacao.Util.Utilidad.LineBreak)
            End If

            If CodigoAjenoBanco IsNot Nothing Then

                If String.IsNullOrWhiteSpace(CodigoAjenoBanco.OidCodigoAjeno) Then
                    Try
                        If PropPlanificacion.CodigoAjenoCliente IsNot Nothing Then

                            Dim ajenoExistente2 = GetCodigosAjenos(ucClientesForm.Clientes.First.Identificador, PropPlanificacion.CodigoAjenoCliente.CodigoTipoTablaGenesis)
                            If ajenoExistente2 IsNot Nothing AndAlso ajenoExistente2.EntidadCodigosAjenos IsNot Nothing AndAlso ajenoExistente2.EntidadCodigosAjenos.Count > 0 Then
                                strErro.Append(String.Format(Tradutor.Traduzir("035_msg_004"), CodigoAjenoBanco.CodIdentificador, CodigoAjenoBanco.CodAjeno, PropPlanificacion.CodigoAjenoCliente.CodigoTipoTablaGenesis))
                            End If
                        End If

                    Catch ex As Exception
                        'nada
                    End Try


                End If
            End If



            If chkControlaFacturacionForm.Checked AndAlso BancoComisionForm.Count = 0 Then
                strErro.Append(RecuperarValorDic("MSG_INFO_SIN_BANCO_COMISION"))
            End If


            If chkDivisionDivisa.Checked AndAlso ucDivisaPlanificacion.DivisasPlanificacion().Count = 0 Then
                strErro.Append(RecuperarValorDic("MSG_INFO_SELECCIONAR_DIVISA"))
            End If
        End If

        Return strErro.ToString

    End Function


    Private Function GetBusqueda() As IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta
        Dim objLogicaNegocio As New LogicaNegocio.AccionPlanificacion
        Dim objPeticion As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        If Not String.IsNullOrEmpty(txtNombrePlanificacion.Text) Then
            objPeticion.DesPlanificacion = txtNombrePlanificacion.Text
        End If

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            objPeticion.OidBanco = Clientes.FirstOrDefault().Identificador
        End If

        If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) Then
            objPeticion.OidTipoPlanificacion = ddlTipoPlanificacion.SelectedValue
        End If

        If Not String.IsNullOrEmpty(ddlEstado.SelectedValue) Then
            objPeticion.Vigente = IIf(ddlEstado.SelectedValue = "0", False, True)
        End If

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objLogicaNegocio.GetPlanificaciones(objPeticion)

        Return objRespuesta
    End Function
    Private Function GetValorParametro(codParametro As String)
        Try
            If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then
                Dim parametro = Parametros.Where(Function(r) r.codigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_PERIODO_FUTUROS).FirstOrDefault
                Return IIf(parametro IsNot Nothing, parametro.valorParametro, "0")
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(New Exception(String.Format(Tradutor.Traduzir("014_parametro_obrigatorio"), codParametro)))
        End Try
        Return "0"
    End Function

    Private Function DadosModificados() As Boolean

        If PropPlanificacion IsNot Nothing AndAlso PropPlanificacion.TipoPlanificacion IsNot Nothing Then
            If ddlTipoPlanificacionForm.SelectedValue <> PropPlanificacion.TipoPlanificacion.Identificador Then
                Return True
            End If
            If txtCodigoForm.Text <> PropPlanificacion.Codigo Then
                Return True
            End If
            If txtNombreForm.Text <> PropPlanificacion.Descripcion Then
                Return True
            End If
            If txtFechaInicio.Text <> PropPlanificacion.FechaVigenciaInicioGmt Then
                Return True
            End If
            If txtFechaFin.Text <> PropPlanificacion.FechaVigenciaFinGmt.ToString Then
                Return True
            End If
            If chkVigenteForm.Checked <> PropPlanificacion.BolActivo Then
                Return True
            End If
            If ddlDelegacionForm.SelectedValue <> PropPlanificacion.Delegacion.Identificador Then
                Return True
            End If

            'Horarios
            If hdfCambioHorario.Value = "HorarioCambiado" Then
                Return True
            End If

            If GenerarProgramaciones().Count <> PropPlanificacion.NecQtdeProgramaciones Then
                Return True
            End If

            'Maquinas
            If GetMaquinasSelecionadas().Count <> PropPlanificacion.Maquinas.Count Then
                Return True
            Else
                Dim cambioMaquina As Boolean = False
                Dim maquinasSelecionadas = Maquinas.OrderBy(Function(f) f.OidMaquina).ToList()
                Dim maquinasPlanificacion = PropPlanificacion.Maquinas.OrderBy(Function(f) f.Identificador).ToList

                Dim indice As Integer = 0
                For Each item In maquinasSelecionadas
                    If item.OidMaquina <> maquinasPlanificacion(indice).Identificador Then
                        cambioMaquina = True
                        Exit For
                    End If
                    indice = indice + 1
                Next
                If cambioMaquina Then
                    Return True
                End If
            End If

            'Canales
            If GetCanaisSelecionados().Count <> PropPlanificacion.Canales.Where(Function(x) x.EstaActivo).ToList().Count Then
                Return True
            End If

            Dim banco = ucClientesForm.Clientes.FirstOrDefault()
            If banco IsNot Nothing Then
                Return banco.Identificador <> PropPlanificacion.Cliente.Codigo
            End If

        End If

        Return False
    End Function

#End Region

#Region "[EVENTOS]"


    Private Sub BusquedaMAE_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        'AtualizaDadosHelperCanal(Canales, ucCanal)
    End Sub

    Private Sub AtualizaDadosHelperCanal(observableCollection As ObservableCollection(Of Comon.Clases.Canal), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCanal)
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

        pUserControl.ucCanal.RegistrosSelecionados = dadosCliente
        pUserControl.ucCanal.ExibirDados(True)
    End Sub

    Protected Sub imgCodigoAjeno_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If ucClientesForm.Clientes IsNot Nothing AndAlso ucClientesForm.Clientes.Count > 0 Then

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples

                tablaGenesis.OidCliente = ucClientesForm.Clientes.First.Identificador
                tablaGenesis.CodCliente = ucClientesForm.Clientes.First.Codigo
                tablaGenesis.DesCliente = ucClientesForm.Clientes.First.Descripcion

                If CodigoAjenoBanco IsNot Nothing Then

                    tablaGenesis.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBanco.CodAjeno), CodigoAjenoBanco.CodAjeno, String.Empty),
                                                            .DesAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBanco.DesAjeno), CodigoAjenoBanco.DesAjeno, String.Empty),
                                                            .OidCodigoAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBanco.OidCodigoAjeno), CodigoAjenoBanco.OidCodigoAjeno, String.Empty)}

                End If

                CodigosAjenosBancoPlanificacion = tablaGenesis

                If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                    url = "MantenimientoCodigosAjenosBancoPlanificacion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                Else
                    url = "MantenimientoCodigosAjenosBancoPlanificacion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_cod_ajeno_titulo"), 250, 800, False, True, btnConsomeCodigoAjeno.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If CodigosAjenosBancoPlanificacion IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples = CodigosAjenosBancoPlanificacion

                If tablaGenesis IsNot Nothing AndAlso tablaGenesis.CodigoAjenoCliente IsNot Nothing Then

                    If CodigoAjenoBanco Is Nothing Then CodigoAjenoBanco = New ContractoServicio.CodigoAjeno.CodigoAjenoBase()

                    With CodigoAjenoBanco
                        .CodAjeno = tablaGenesis.CodigoAjenoCliente.CodAjeno
                        .DesAjeno = tablaGenesis.CodigoAjenoCliente.DesAjeno
                        .OidCodigoAjeno = tablaGenesis.CodigoAjenoCliente.OidCodigoAjeno
                    End With

                    ValidarCodigoAjeno()

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgCodigoAjenoBancoComision_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If ucBancoComisionForm.Clientes IsNot Nothing AndAlso ucBancoComisionForm.Clientes.Count > 0 Then

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples

                tablaGenesis.OidCliente = ucBancoComisionForm.Clientes.First.Identificador
                tablaGenesis.CodCliente = ucBancoComisionForm.Clientes.First.Codigo
                tablaGenesis.DesCliente = ucBancoComisionForm.Clientes.First.Descripcion

                If CodigoAjenoBancoComision IsNot Nothing Then

                    tablaGenesis.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBancoComision.CodAjeno), CodigoAjenoBancoComision.CodAjeno, String.Empty),
                                                            .DesAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBancoComision.DesAjeno), CodigoAjenoBancoComision.DesAjeno, String.Empty),
                                                            .OidCodigoAjeno = If(Not String.IsNullOrEmpty(CodigoAjenoBancoComision.OidCodigoAjeno), CodigoAjenoBancoComision.OidCodigoAjeno, String.Empty)}

                End If

                CodigosAjenosBancoComisionPlanificacion = tablaGenesis

                If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                    url = "MantenimientoCodigosAjenosBancoPlanificacion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                Else
                    url = "MantenimientoCodigosAjenosBancoPlanificacion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_cod_ajeno_titulo"), 250, 800, False, True, btnConsomeCodigoAjenoBancoComision.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCodigoAjenoBancoComision_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjenoBancoComision.Click
        Try
            If CodigosAjenosBancoComisionPlanificacion IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCodigosAjenosBancoPlanificacion.CodigoAjenoSimples = CodigosAjenosBancoPlanificacion

                If tablaGenesis IsNot Nothing AndAlso tablaGenesis.CodigoAjenoCliente IsNot Nothing Then

                    If CodigoAjenoBancoComision Is Nothing Then CodigoAjenoBancoComision = New ContractoServicio.CodigoAjeno.CodigoAjenoBase()

                    With CodigoAjenoBancoComision
                        .CodAjeno = tablaGenesis.CodigoAjenoCliente.CodAjeno
                        .DesAjeno = tablaGenesis.CodigoAjenoCliente.DesAjeno
                        .OidCodigoAjeno = tablaGenesis.CodigoAjenoCliente.OidCodigoAjeno
                    End With

                    ValidarCodigoAjenoBancoComision()
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub btnGrabarConfirm_Click(sender As Object, e As EventArgs) Handles btnGrabarConfirm.Click
        Try
            If ddlTipoPlanificacionForm.SelectedItem.Text.Equals("Online") AndAlso GenerarProgramaciones().Count = 0 Then
                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnGrabarOnline.ClientID & Chr(34) & ");"
                MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("MSG_INFO_ONLINE_SIN_PROGRAMACION "), jsScript)
            Else
                ExecutarGrabar()
            End If
        Catch ex As Exception
            MyBase.MostraMensagem(If(TypeOf ex Is Prosegur.Genesis.Excepcion.NegocioExcepcion, ex.Message, ex.ToString()))
        End Try
    End Sub

    Private Sub btnGrabarOnline_Click(sender As Object, e As EventArgs) Handles btnGrabarOnline.Click
        Try
            ExecutarGrabar()
        Catch ex As Exception
            MyBase.MostraMensagem(If(TypeOf ex Is Prosegur.Genesis.Excepcion.NegocioExcepcion, ex.Message, ex.ToString()))
        End Try
    End Sub

    ''' <summary>
    ''' Preenche do grid com a coleção de Planificações
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BusquedaDados()

        Dim objRespuesta As New ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

        objRespuesta = GetBusqueda()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Planificacion.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.Planificacion.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then

                pnlSemRegistro.Visible = False

                Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.Planificacion)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " DesPlanificacion ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If GdvResultado.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " OidPlanificacion ASC "
                    Else
                        objDt.DefaultView.Sort = GdvResultado.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

                ' carregar controle
                GdvResultado.CarregaControle(objDt)

            Else

                'Limpa a consulta
                GdvResultado.DataSource = Nothing
                GdvResultado.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Tradutor.Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
                AcaoPagina = Acao.ToString
            End If

        Else

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Tradutor.Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try
            btnCancelar_Click(sender, e)

            ' setar ação de busca
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca
            AcaoPagina = Acao.ToString

            BusquedaDados()
        Catch ex As Exception
            MyBase.MostraMensagem(If(TypeOf ex Is Prosegur.Genesis.Excepcion.NegocioExcepcion, ex.Message, ex.ToString()))

        End Try
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init, grid.Init
        DefinirParametrosBase()
        CarregarMaquinas(Maquinas)
        TraduzirControles()
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            LimparCamposFiltro()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            AcaoPagina = Acao.ToString

            pnlSemRegistro.Visible = False

            btnCancelar_Click(sender, e)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            If Acao = Utilidad.eAcao.Modificacion Then

                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeCancelar.ClientID & Chr(34) & ");"

                If DadosModificados() Then
                    MyBase.ExibirMensagemSimNao(RecuperarValorDic("MSG_INFO_DESECHAR_CAMBIOS"), jsScript)
                Else
                    btnConsomeCancelar_Click(Nothing, Nothing)
                End If
            Else
                btnConsomeCancelar_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub Cancelar()
        LimpiarForm()
        btnNovo.Enabled = True
        btnBajaConfirm.Visible = False
        btnCancelar.Enabled = False
        btnGrabar.Enabled = False
        btnAddMAE.Enabled = False
        btnCamposExtrasModal.Enabled = False
        pnForm.Enabled = False
        pnForm.Visible = False
        HabilitarDesabilitaForm(True)

        'Limpio los datos de los campos extras.
        CamposExtrasDinamicosDatos = Nothing
        CamposExtrasPatronesDatos = Nothing
    End Sub
    Private Sub btnConsomeCancelar_Click(sender As Object, e As EventArgs) Handles btnConsomeCancelar.Click
        Try
            Cancelar()
            Acao = Utilidad.eAcao.Inicial
            AcaoPagina = Acao.ToString
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Acao = Utilidad.eAcao.Alta
            AcaoPagina = Acao.ToString

            btnNovo.Enabled = False
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            btnAddMAE.Enabled = True
            btnCamposExtrasModal.Enabled = False 'Es necesario que para que este habilitado este seleccionado un tipo de planificación.
            pnForm.Enabled = True
            pnForm.Visible = True
            btnCodigoAjenoBanco.Enabled = False
            btnCodigoAjenoBanco.ImageUrl = "~/Imagenes/contain_disabled.png"
            ddlTipoPlanificacionForm.Enabled = True
            txtCodigoForm.Enabled = True
            txtNombreForm.Enabled = True
            chkVigenteForm.Enabled = True
            ddlDelegacionForm_SelectedIndexChanged(Nothing, Nothing)
            GenerarListaHorarios()
            LimpiarForm()
            HabilitarDesabilitaForm(True)
            CamposExtrasDinamicosDatos = Nothing
            CamposExtrasPatronesDatos = Nothing

            'Limite
            ucLimitePlanificacion.Modo = Comon.Enumeradores.Modo.Alta
            ucLimitePlanificacion.ConfigurarControles()

            'Divisa
            ucDivisaPlanificacion.Modo = Comon.Enumeradores.Modo.Alta
            ucDivisaPlanificacion.ConfigurarControles()

            'Movimiento
            ucMovimientoPlanificacion.Modo = Comon.Enumeradores.Modo.Alta
            ucMovimientoPlanificacion.ConfigurarControles()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnAddMAE_Click(sender As Object, e As EventArgs) Handles btnAddMAE.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New BusquedaMAEPopup.PlanxMaquina

            tablaGenesis.Maquinas = Maquinas

            Session("objBusquedaMAE") = tablaGenesis

            Session("CantidadHorasPeriodosFuturos") = GetValorParametro(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CANTIDAD_HORAS_PERIODO_FUTUROS)

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "BusquedaMAEPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "BusquedaMAEPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, RecuperarValorDic("mod_titulo_busca_mae"), 550, 1300, False, True, btnConsomePlanificacion.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCamposExtrasModal_Click(sender As Object, e As EventArgs) Handles btnCamposExtrasModal.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New BusquedaCamposExtraModal.CamposExtras

            tablaGenesis.TerminosDinamicos = CamposExtrasDinamicosDatos
            tablaGenesis.TerminosPatron = CamposExtrasPatronesDatos

            If Acao = Utilidad.eAcao.Alta Then
                'Al ser alta, pasamos como CodigoIAC el seleccionado en el tipo de planificación.
                tablaGenesis.TerminosPatron.CodigoIAC = ddlTipoPlanificacionForm.SelectedItem.Text

                Dim codigoParametro As String = String.Empty
                Dim codigoDelegacion As String = Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoDelegacion
                Dim codigoAplicacion As String = String.Empty

                If ddlTipoPlanificacionForm.SelectedItem.Text.ToUpper().Contains("ONLINE") Then
                    codigoParametro = Prosegur.Genesis.Comon.Constantes.CODIGO_PARAMETRO_TIPO_PLANIFIACION_FV_ONLINE
                Else
                    codigoParametro = Prosegur.Genesis.Comon.Constantes.CODIGO_PARAMETRO_TIPO_PLANIFICACION_FECHA_VALOR
                End If

                codigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS


                Dim resultado = LogicaNegocio.AccionTipoPlanificacion.getParametrosTipoPlanificacion(codigoParametro, codigoAplicacion, codigoDelegacion)
                If resultado IsNot Nothing Then
                    'Al ser alta, pasamos como CodigoIAC el seleccionado en el tipo de planificación.
                    tablaGenesis.TerminosPatron.CodigoIAC = resultado(0).valorParametro
                    CamposExtrasPatronesDatos.CodigoIAC = resultado(0).valorParametro
                End If
            End If

            Session("objBusquedaCamposExtras") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "BusquedaCamposExtraModal.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "BusquedaCamposExtraModal.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, RecuperarValorDic("mod_titulo_busca_campos_extra"), 550, 1300, False, True, String.Empty)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomePlanificacion_Click(sender As Object, e As EventArgs) Handles btnConsomePlanificacion.Click
        Try
            If Session("objBusquedaMAE") IsNot Nothing Then

                Dim tablaGenesis As BusquedaMAEPopup.PlanxMaquina = Session("objBusquedaMAE")
                If tablaGenesis IsNot Nothing AndAlso tablaGenesis.Maquinas IsNot Nothing Then

                    Dim maquinasSelecionadas = Maquinas

                    For Each itemMaq As ContractoServicio.Maquina.GetMaquinaSinPlanificacion.Maquina In tablaGenesis.Maquinas

                        If respuestaComissionDatosBancarios IsNot Nothing Then

                            Dim PorcComis As Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Maquina = Nothing

                            If respuestaComissionDatosBancarios.MAEs IsNot Nothing Then
                                PorcComis = respuestaComissionDatosBancarios.MAEs.FirstOrDefault(Function(x) x.OID_MAQUINA = itemMaq.OidMaquina)
                            End If

                            If PorcComis IsNot Nothing Then

                                Dim lblBancoTesoreria = String.Empty

                                If Not String.IsNullOrWhiteSpace(PorcComis.COD_SUBCLIENTE) Then
                                    lblBancoTesoreria = PorcComis.COD_SUBCLIENTE + " - " + PorcComis.DES_SUBCLIENTE
                                End If


                                If Not String.IsNullOrWhiteSpace(PorcComis.COD_PTO_SERVICIO) Then
                                    If Not String.IsNullOrWhiteSpace(lblBancoTesoreria) Then
                                        lblBancoTesoreria += " / "
                                    End If
                                    lblBancoTesoreria += PorcComis.COD_PTO_SERVICIO + " - " + PorcComis.DES_PTO_SERVICIO
                                End If

                                itemMaq.BancoTesoreria = lblBancoTesoreria

                                If PorcComis.NUM_PORCENT_COMISION IsNot Nothing Then
                                    itemMaq.NumPorcentComision = PorcComis.NUM_PORCENT_COMISION
                                End If
                            Else
                                Dim objLogicaNegocio As New LogicaNegocio.AccionPlanificacion
                                Dim objPeticionP As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Peticion
                                Dim objRespuestaP As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta
                                objPeticionP.ParametrosPaginacion.RealizarPaginacion = False
                                objPeticionP.OidPlanificacion = PropPlanificacion.Identificador

                                If ddlDelegacionForm.SelectedValue IsNot Nothing AndAlso Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then
                                    objPeticionP.OidDelegacion = ddlDelegacionForm.SelectedValue
                                End If

                                objRespuestaP = objLogicaNegocio.GetPlanificacionFacturacion(objPeticionP)
                                Dim lblBancoTesoreria = String.Empty


                                If objRespuestaP IsNot Nothing AndAlso objRespuestaP.Planificacion IsNot Nothing Then

                                    If objRespuestaP.Planificacion.BancoTesoreriaDelegacion IsNot Nothing Then
                                        lblBancoTesoreria = objRespuestaP.Planificacion.BancoTesoreriaDelegacion.Codigo + " - " + objRespuestaP.Planificacion.BancoTesoreriaDelegacion.Descripcion
                                    End If

                                    If objRespuestaP.Planificacion.CuentaTesoreriaDelegacion IsNot Nothing Then
                                        If Not String.IsNullOrWhiteSpace(lblBancoTesoreria) Then
                                            lblBancoTesoreria += " / "
                                        End If
                                        lblBancoTesoreria += objRespuestaP.Planificacion.CuentaTesoreriaDelegacion.Codigo + " - " + objRespuestaP.Planificacion.CuentaTesoreriaDelegacion.Descripcion
                                    End If
                                    itemMaq.BancoTesoreria = ""
                                End If
                            End If
                        End If
                    Next
                    maquinasSelecionadas.AddRange(tablaGenesis.Maquinas)
                    Maquinas = maquinasSelecionadas
                    CarregarMaquinas(Maquinas)
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Public Function VisibleBtnMae(oidMaquina As String) As Boolean
        If respuestaComissionDatosBancarios Is Nothing OrElse respuestaComissionDatosBancarios.MAEs Is Nothing Then
            Return False
        End If
        Return respuestaComissionDatosBancarios.MAEs.Exists(Function(x) x.OID_MAQUINA = oidMaquina)
    End Function

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            Dim strErro As String = MontaMensagensErroForm(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnGrabarConfirm.ClientID & Chr(34) & ");"
            MyBase.ExibirMensagemSimNao(String.Format(MyBase.RecuperarValorDic("MSG_INFO_CONFIRMA_GRABACION"), MyBase.RecuperarValorDic("mod_cod_ajeno_titulo")), jsScript)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnCambioCanalSim_Click(sender As Object, e As EventArgs) Handles btnCambioCanalSim.Click
        'Esto mensaje deberá ser exhibido todas las veces que el usuario intentar cambiar hasta que seleccione la opción “Sí”, 
        'después de seleccionar esta opción, no será necesario alertarlo más.
        ExibeValidacionCanal = False
    End Sub

    Private Sub btnCambioCanalNao_Click(sender As Object, e As EventArgs) Handles btnCambioCanalNao.Click
        Try
            'Setar os canais selecionados anteriormente 
            CarregarCanales(PropPlanificacion.Canales)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ValidarPeriodoAbierto()
        If Acao = Utilidad.eAcao.Modificacion AndAlso ExibeValidacionCanal Then

            If HayPeriodosAbiertos Then

                Dim acaoNao As String = "ExecutarClick(" & Chr(34) & btnCambioCanalNao.ClientID & Chr(34) & ");"
                Dim acaoSim As String = "ExecutarClick(" & Chr(34) & btnCambioCanalSim.ClientID & Chr(34) & ");"
                MyBase.ExibirMensagemNaoSim(MyBase.RecuperarValorDic("MSG_INFO_CANALES_CAMBIADOS"), acaoSim, acaoNao)

            End If
        End If
    End Sub


#Region "GRID PLANIFICACIONES"

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    id = GdvResultado.getValorLinhaSelecionada
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                Acao = Utilidad.eAcao.Baja
                AcaoPagina = Acao.ToString

                LimpiarForm()
                CarregaDados(id)
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnAddMAE.Enabled = False
                btnCamposExtrasModal.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitaForm(False)

                'Fecha Valor con Confirmacion
                ValidarFechaValorConConfirmacion()

                'Limite
                ucLimitePlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucLimitePlanificacion.ConfigurarControles()

                'Divisa
                ucDivisaPlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucDivisaPlanificacion.ConfigurarControles()

                'Movimiento
                ucMovimientoPlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucMovimientoPlanificacion.ConfigurarControles()

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try

            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    id = GdvResultado.getValorLinhaSelecionada
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                LimpiarForm()
                Acao = Utilidad.eAcao.Modificacion
                AcaoPagina = Acao.ToString
                CarregaDados(Server.UrlDecode(id))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                btnAddMAE.Enabled = True
                btnCamposExtrasModal.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                HabilitarDesabilitaForm(True)

                'Fecha Valor con Confirmacion
                ValidarFechaValorConConfirmacion()

                'Limite
                ucLimitePlanificacion.Modo = Comon.Enumeradores.Modo.Alta
                ucLimitePlanificacion.ConfigurarControles()

                'Divisa
                ucDivisaPlanificacion.Modo = Comon.Enumeradores.Modo.Alta
                ucDivisaPlanificacion.ConfigurarControles()

                'Movimiento
                ucMovimientoPlanificacion.Modo = Comon.Enumeradores.Modo.Alta
                ucMovimientoPlanificacion.ConfigurarControles()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    id = GdvResultado.getValorLinhaSelecionada
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                LimpiarForm()
                Acao = Utilidad.eAcao.Consulta
                AcaoPagina = Acao.ToString
                CarregaDados(Server.UrlDecode(id))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                btnAddMAE.Enabled = False
                btnCamposExtrasModal.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True

                'Fecha Valor con Confirmacion
                ValidarFechaValorConConfirmacion()

                'Limite
                ucLimitePlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucLimitePlanificacion.ConfigurarControles()

                'Divisa
                ucDivisaPlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucDivisaPlanificacion.ConfigurarControles()

                'Movimiento
                ucMovimientoPlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
                ucMovimientoPlanificacion.ConfigurarControles()

                HabilitarDesabilitaForm(False)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaConfirm_Click(sender As Object, e As EventArgs) Handles btnBajaConfirm.Click
        Try
            If PropPlanificacion IsNot Nothing Then

                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeBaja.ClientID & Chr(34) & ");"

                MyBase.ExibirMensagemSimNao(String.Format(RecuperarValorDic("MSG_INFO_BAJAR_PLANIFICACION"), PropPlanificacion.Descripcion), jsScript)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeBaja_Click(sender As Object, e As EventArgs) Handles btnConsomeBaja.Click
        Try
            If PropPlanificacion IsNot Nothing Then

                Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.BajaPlanificacion(PropPlanificacion, MyBase.LoginUsuario)
                Cancelar()
                BusquedaDados()

                Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
                AcaoPagina = Acao.ToString
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub ProsegurGridViewProcesso_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(GetBusqueda().Planificacion)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " DesPlanificacion ASC "
        Else
            objDT.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = objDT

    End Sub

    Private Sub ProsegurGridViewProcesso_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim imgEditar = CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton)
                Dim imgConsultar = CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton)
                Dim imgExcluir = CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton)

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("OidPlanificacion")) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Tradutor.Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Tradutor.Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Tradutor.Traduzir("btnBaja")

                If CBool(e.Row.DataItem("BolActivo")) Then
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"

                    imgExcluir.Enabled = True
                Else
                    imgExcluir.Enabled = False
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region

#Region "GRID PROGRAMACION"
    Dim Sequencial As Integer = 0

    Private Sub SetarClasse(txt As TextBox)
        Dim id_componente = txt.ID + Sequencial.ToString

        txt.Attributes.Add("id_componente", id_componente)
        txt.Attributes.Add("numero_campo", Sequencial.ToString)
        txt.Attributes.Add("valor_anterior", txt.Text)
        txt.Attributes.Add("onblur", "ValidarHoraInformada(this);")
    End Sub
    Private Sub ProsegurGridViewProgramacion_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvHorarios.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Sequencial = Sequencial + 1

                Dim txtLunes As TextBox = e.Row.FindControl("txtLunes")
                Dim txtMartes As TextBox = e.Row.FindControl("txtMartes")
                Dim txtMiercoles As TextBox = e.Row.FindControl("txtMiercoles")
                Dim txtJueves As TextBox = e.Row.FindControl("txtJueves")
                Dim txtViernes As TextBox = e.Row.FindControl("txtViernes")
                Dim txtSabado As TextBox = e.Row.FindControl("txtSabado")
                Dim txtDomingo As TextBox = e.Row.FindControl("txtDomingo")

                SetarClasse(txtLunes)
                SetarClasse(txtMartes)
                SetarClasse(txtMiercoles)
                SetarClasse(txtJueves)
                SetarClasse(txtViernes)
                SetarClasse(txtSabado)
                SetarClasse(txtDomingo)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region
#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
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

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub

    Protected Sub GdvHorarios_SelectedIndexChanged(sender As Object, e As EventArgs)
        'nada
    End Sub

    Protected Sub btnAdcLinha_Click(sender As Object, e As ImageClickEventArgs) Handles btnAdcLinha.Click

        Dim listaHorarios As New List(Of Comon.Clases.PlanXProgramacion)
        listaHorarios = GenerarProgramaciones()

        Dim obj = New Planificacion()
        obj.Programacion = listaHorarios

        Genesis.LogicaNegocio.Genesis.Planificacion.ProcessarProgramaciones(obj, GdvHorarios.Rows.Count + 1)

        Dim dt = GdvHorarios.ConvertListToDataTable(listaHorarios)

        CarregarProgramacionesLinhas(obj.Programacion)

    End Sub

    Protected Sub btnRemLinha_Click(sender As Object, e As ImageClickEventArgs) Handles btnRemLinha.Click

        Dim listaHorarios As New List(Of Comon.Clases.PlanXProgramacion)
        listaHorarios = GenerarProgramaciones()

        Dim obj = New Planificacion()
        obj.Programacion = listaHorarios

        Genesis.LogicaNegocio.Genesis.Planificacion.ProcessarProgramaciones(obj, GdvHorarios.Rows.Count - 1)

        Dim dt = GdvHorarios.ConvertListToDataTable(listaHorarios)

        CarregarProgramacionesLinhas(obj.Programacion)

    End Sub

    Protected Sub chkControlaFacturacionForm_CheckedChanged(sender As Object, e As EventArgs) Handles chkControlaFacturacionForm.CheckedChanged
        HabilitaDesabilitaFacturacion()
    End Sub

    Private Sub HabilitaDesabilitaFacturacion()
        If chkControlaFacturacionForm.Checked Then
            grid.Columns("NumPorcentComisionTxt").Visible = True
            grid.Columns("BancoTesoreria").Visible = True
            pnlFacturacion.Enabled = True
            If BancoComision IsNot Nothing Then
                _ucBancoComisionForm.Clientes.Add(BancoComision)
                AtualizaDadosHelperCliente(BancoComisionForm, ucBancoComisionForm)
                BuscarCodigosAjenosBancoComision()
            End If
            If Not String.IsNullOrEmpty(PorComisionPlanificacion) Then
                txtComisionClienteForm.Text = PorComisionPlanificacion
            End If
            If Not String.IsNullOrEmpty(DiaCierreFacturacion) Then
                txtDiaCierreForm.Text = DiaCierreFacturacion
            End If

        Else
            If _ucBancoComisionForm.Clientes.Count > 0 Then
                BancoComision = _ucBancoComisionForm.Clientes.FirstOrDefault
                BancoComisionForm.Clear()
                AtualizaDadosHelperCliente(BancoComisionForm, ucBancoComisionForm)
            End If

            If Not String.IsNullOrEmpty(txtComisionClienteForm.Text) Then
                PorComisionPlanificacion = txtComisionClienteForm.Text
                txtComisionClienteForm.Text = String.Empty
            End If

            If Not String.IsNullOrEmpty(txtDiaCierreForm.Text) Then
                DiaCierreFacturacion = txtDiaCierreForm.Text
                txtDiaCierreForm.Text = String.Empty
            End If

            grid.Columns("NumPorcentComisionTxt").Visible = False
            grid.Columns("BancoTesoreria").Visible = False
            pnlFacturacion.Enabled = False
        End If
        ValidarCodigoAjenoBancoComision()
    End Sub

    Private Sub BusquedaPlanificaciones_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        If Not String.IsNullOrWhiteSpace(script) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "_lblTitulo", script, True)
        End If


        AtualizaDadosHelperCanal(Canales, ucCanales)
    End Sub

    Private Sub ddlTipoPlanificacionForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoPlanificacionForm.SelectedIndexChanged
        Try
            If ddlTipoPlanificacionForm.SelectedItem.Text <> MyBase.RecuperarValorDic("btnSeleccionar") Then
                btnCamposExtrasModal.Enabled = True
            Else
                btnCamposExtrasModal.Enabled = False
            End If

            ValidarFechaValorConConfirmacion()

        Catch ex As Exception
            'nada
        End Try
    End Sub
#End Region


    Protected Function BuscaPostbackGrid(accion As String, identificador As String) As String
        Dim strPostBack As String = String.Empty

        strPostBack = "__doPostBack('" + btnGrid.UniqueID + "', '" + accion + "|" + identificador + "')"

        Return strPostBack
    End Function


    Protected Sub btnGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrid.Click
        Dim p = Page.Request.Params.Get("__EVENTARGUMENT")

        Dim params = p.Split("|")

        Select Case params(0)
            Case "BORRAR"
                Borrar_Grid(params(1))
            Case "MAE"
                MAE_Grid(params(1))
            Case "DATOSBANCARIOS"
                DatosBancarios_Grid(params(1))


        End Select
    End Sub

    Protected Sub MAE_Grid(identificador As String)
        Dim url As String = String.Empty
        Dim item = Maquinas.FirstOrDefault(Function(x) x.OidMaquina = identificador)
        Dim AcaoCodigo As String = String.Empty
        If Aplicacao.Util.Utilidad.eAcao.Modificacion = MyBase.Acao Then
            AcaoCodigo = "4"
        Else
            AcaoCodigo = "5"
        End If

        url = "BusquedaMAE.aspx?modo=modal&AcaoCodigo=" + AcaoCodigo + "&oidMaquina=" + item.OidMaquina
        Session("objResultModalMae") = Nothing

        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "AbrirModal", "AbrirModal('" + AcaoCodigo + "','" + item.OidMaquina + "');", True)

    End Sub

    Protected Sub Borrar_Grid(identificador As String)
        '' e.com

        Me.Maquinas.RemoveAll(Function(x) x.OidMaquina = identificador)
        CarregarMaquinas(Me.Maquinas)
    End Sub

    Protected Sub DatosBancarios_Grid(identificador As String)


        Dim item = Maquinas.FirstOrDefault(Function(x) x.OidMaquina = identificador)
        ucDatosBanc.PuntoServicio.Identificador = item.OidPtoServicio
        ucDatosBanc.strBtnExecutar = btnConsomeTotalizador.ClientID
        ucDatosBanc.Cliente.Identificador = item.OidCliente
        ucDatosBanc.SubCliente.Identificador = item.OidSubCliente
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

            tablaGenesis.OidCliente = item.OidCliente
            tablaGenesis.CodCliente = item.CodigoCliente
            tablaGenesis.DesCliente = item.Cliente
            tablaGenesis.OidSubcliente = item.OidSubCliente
            tablaGenesis.CodSubcliente = item.CodigoSubCliente
            tablaGenesis.DesSubcliente = item.SubCliente
            tablaGenesis.OidPuntoServicio = item.OidPtoServicio
            tablaGenesis.CodPuntoServicio = item.CodigoPtoServicio
            tablaGenesis.DesPuntoServicio = item.PtoServicio
            If DatosBancarios Is Nothing Then
                DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
            End If
            If DatosBancarios.ContainsKey(ID) Then
                tablaGenesis.DatosBancarios = DatosBancarios(ID)
            End If

            Session("objMantenimientoCamposExtras") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtras.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnMensajes_Click(sender As Object, e As EventArgs)
        Try
            Dim url As String = String.Empty
            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoMensajes.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "MantenimientoMensajes.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, RecuperarValorDic("btnMensajes"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeBaja_Load(sender As Object, e As EventArgs) Handles btnConsomeBaja.Load

    End Sub
    Public Function GetProcesosListSelectedValue() As List(Of Comon.Clases.Proceso)

        Dim Procesos As List(Of Proceso) = New List(Of Proceso)

        For i As Integer = 0 To chkbxlstProcesos.Items.Count - 1

            If chkbxlstProcesos.Items(i).Selected Then
                Dim ProcesoObj = New Proceso()
                ProcesoObj.Identificador = chkbxlstProcesos.Items(i).Value
                Procesos.Add(ProcesoObj)
            End If
        Next

        Return Procesos
    End Function

    Private Sub ValidarFechaValorConConfirmacion()
        Dim tipoPlanificacionSeleccionada = TiposPlanificacion.FirstOrDefault(Function(x) x.oidTipoPlanificacion = ddlTipoPlanificacionForm.SelectedValue)
        If tipoPlanificacionSeleccionada IsNot Nothing AndAlso tipoPlanificacionSeleccionada.codTipoPlanificacion = "FECHA_VALOR_CONFIR" Then
            lblPatronConfirmacion.Visible = True
            ddlPatronConfirmacion.Visible = True
            btnMensajes.Visible = True
            chkbxlstProcesos.Visible = True
            lblProcesos.Visible = True
            chkPorPuntoServicio.Checked = True
            chkPorPuntoServicio.Enabled = False
            divDivisasPlanificacion.Visible = True
            divDivisionPeriodos.Visible = True

            'Si se trata de una edición y el check Division por divisa está chequeado, no permito modificación
            If PropPlanificacion IsNot Nothing AndAlso PropPlanificacion.BolDividePorDivisa Then
                chkDivisionDivisa.Enabled = False
            Else
                chkDivisionDivisa.Enabled = True
            End If

            'Si se trata de una edición y el check Division por subcanal está chequeado, no permito modificación
            If PropPlanificacion IsNot Nothing AndAlso PropPlanificacion.BolDividePorSubcanal Then
                chkDivisionSubcanal.Enabled = False
            Else
                chkDivisionSubcanal.Enabled = True
            End If

            'Si se trata de una edición y el check Division por subcanal no está chequeado, indico en el check 
            'de agrupamiento por subcanal lo que tenía originalmente en la base de datos
            If PropPlanificacion IsNot Nothing AndAlso Not chkDivisionSubcanal.Checked Then
                chkPorSubCanal.Checked = PropPlanificacion.BolAgrupaPorSubCanal
            Else
                chkPorSubCanal.Checked = chkDivisionSubcanal.Checked
            End If

            chkPorSubCanal.Enabled = Not chkDivisionSubcanal.Checked
        Else
            lblPatronConfirmacion.Visible = False
            ddlPatronConfirmacion.Visible = False
            btnMensajes.Visible = False
            chkbxlstProcesos.Visible = False
            lblProcesos.Visible = False
            chkPorPuntoServicio.Enabled = True
            divDivisasPlanificacion.Visible = False
            divDivisionPeriodos.Visible = False
        End If
    End Sub

    Protected Sub chkDivisionSubcanal_CheckedChanged(sender As Object, e As EventArgs) Handles chkDivisionSubcanal.CheckedChanged

        If chkDivisionSubcanal.Checked Then
            MyBase.MostraMensagem(RecuperarValorDic("MSG_INFO_DIVISION_POR_SUBCANAL"))
        End If


        'Si se trata de una edición y el check Division por subcanal no está chequeado, indico en el check 
        'de agrupamiento por subcanal lo que tenía originalmente en la base de datos
        If PropPlanificacion IsNot Nothing AndAlso Not chkDivisionSubcanal.Checked Then
            chkPorSubCanal.Checked = PropPlanificacion.BolAgrupaPorSubCanal
        Else
            chkPorSubCanal.Checked = chkDivisionSubcanal.Checked
        End If

        chkPorSubCanal.Enabled = Not chkDivisionSubcanal.Checked
    End Sub

    Protected Sub chkDivisionDivisa_CheckedChanged(sender As Object, e As EventArgs) Handles chkDivisionDivisa.CheckedChanged
        If chkDivisionDivisa.Checked Then
            MyBase.MostraMensagem(RecuperarValorDic("MSG_INFO_DIVISION_POR_DIVISA"))
        End If
    End Sub

    Private Sub ValidarParametroDivisaDefecto(ByRef resultado As String)
        Dim peticion As New Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion
        Dim respuesta As Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Respuesta
        peticion.codigoAplicacion = Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS
        peticion.codigoDelegacion = MyBase.DelegacionLogada.Codigo
        peticion.codigosParametro = New List(Of String)

        peticion.codigosParametro.Add(Comon.Constantes.COD_CODIGO_ISO_DIVISA_DEFECTO)

        respuesta = Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

        If respuesta IsNot Nothing AndAlso respuesta.parametros IsNot Nothing Then
            If (respuesta.parametros.Count > 0) Then
                Parametros = respuesta.parametros
                Dim codigoDivisa As String = Parametros(0).valorParametro
                Dim respuestaDivisa As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta = VerificarCodigoDivisa(codigoDivisa)

                If respuestaDivisa IsNot Nothing Then
                    If Not respuestaDivisa.Existe Then resultado = "MSG_INFO_CONFIGURACION_DIVISA_INVALIDO"
                End If
            Else
                resultado = "MSG_INFO_CONFIGURACION_DIVISA_VACIO"
            End If
        Else
            resultado = "MSG_INFO_CONFIGURACION_DIVISA_VACIO"
        End If
    End Sub

    Public Function VerificarCodigoDivisa(codigoDivisa As String) As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

        Dim objProxyDivisa As New Comunicacion.ProxyDivisa
        Dim objPeticionDivisa As New ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion
        Dim objRespuestaDivisa As ContractoServicio.Divisa.VerificarCodigoDivisa.Respuesta

        'Recebe os valores do filtro
        objPeticionDivisa.CodigoIso = codigoDivisa

        objRespuestaDivisa = objProxyDivisa.VerificarCodigoDivisa(objPeticionDivisa)

        Return objRespuestaDivisa
    End Function
End Class
