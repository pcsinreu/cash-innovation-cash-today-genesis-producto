Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente

Public Class BusquedaDatosBancariosPopup
    Inherits Base

#Region "[HelpersCliente]"
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
                _ucClientes.ID = Me.ID & "_ucClientesDatosBancarios"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True
        Me.ucClientes.TipoBanco = True
        Me.ucClientes.ClienteTitulo = Traduzir("087_lbl_banco")

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes

                If Clientes.Count > 0 Then

                    Dim _Proxy As New Comunicacion.ProxyCliente
                    Dim _Peticion As New GetClientesDetalle.Peticion
                    Dim _Respuesta As GetClientesDetalle.Respuesta

                    _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                    _Peticion.CodCliente = Clientes(0).Codigo
                    _Peticion.CodigoExacto = True
                    _Respuesta = _Proxy.GetClientesDetalle(_Peticion)
                    If _Respuesta.Clientes IsNot Nothing AndAlso _Respuesta.Clientes.Count > 0 Then
                        Dim clienteBanco = _Respuesta.Clientes.FirstOrDefault(Function(x) x.CodCliente = _Peticion.CodCliente)
                        If clienteBanco IsNot Nothing Then
                            txtCodigoBancario.Text = clienteBanco.CodBancario
                        Else
                            txtCodigoBancario.Text = String.Empty
                        End If
                    End If

                Else
                    txtCodigoBancario.Text = String.Empty
                End If

            Else
                txtCodigoBancario.Text = String.Empty
            End If

            ConsomeCliente()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[Propriedades Modal]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal")
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal")
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar")
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()

        '   btnCancelar.FuncaoJavascript = "window.close();"

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        'txtCliente.TabIndex = 1
        'btnBuscarCliente.TabIndex = 2
        'txtNroDocumento.TabIndex = 3
        'ddlDivisa.TabIndex = 4
        'txtTitularidad.TabIndex = 5
        'ddlTipo.TabIndex = 6
        'txtObs.TabIndex = 7
        'txtCuenta.TabIndex = 8
        'chkDefecto.TabIndex = 9

        'btnAceptar.TabIndex = 10
        'btnCancelar.TabIndex = 11

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.NIVELESSALDOS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False

        If Request("acao") IsNot Nothing Then
            MyBase.Acao = Request("acao")
        End If

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

            If Not Page.IsPostBack Then
                Clientes = Nothing
                If Peticion Is Nothing Then
                    Throw New Exception(Traduzir("err_passagem_parametro"))
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    'btnBuscarCliente.Focus()
                ElseIf MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ' btnCancelar.Focus()
                End If

                CarregarDivisa()
                CarregarTipos()


                CarregarHelpers()

                CarregarParametroAprovacion()
            End If

            ConfigurarControle_Cliente()

            TrataFoco()

            'ConsomeCliente()
            ' If Session("btnAceptar") Is Nothing Then
            If Not Page.IsPostBack Then
                ConsomeDivisa()

                If Session("Dto_Banc_NroDocumento") IsNot Nothing Then

                    NroDocumentoSelecionado = Nothing
                    txtNroDocumento.Text = String.Empty
                    txtNroDocumento.ToolTip = String.Empty

                    NroDocumentoSelecionado = Session("Dto_Banc_NroDocumento")

                    If Not String.IsNullOrEmpty(NroDocumentoSelecionado) Then
                        txtNroDocumento.Text = NroDocumentoSelecionado
                        txtNroDocumento.ToolTip = NroDocumentoSelecionado
                    End If

                    ' Session("Dto_Banc_NroDocumento") = Nothing

                End If

                If Session("Dto_Banc_Titularidad") IsNot Nothing Then

                    TitularidadSelecionado = Nothing
                    txtTitularidad.Text = String.Empty
                    txtTitularidad.ToolTip = String.Empty

                    TitularidadSelecionado = Session("Dto_Banc_Titularidad")

                    If Not String.IsNullOrEmpty(TitularidadSelecionado) Then
                        txtTitularidad.Text = TitularidadSelecionado
                        txtTitularidad.ToolTip = TitularidadSelecionado
                    End If

                    'Session("Dto_Banc_Titularidad") = Nothing

                End If

                If Session("Dto_Banc_Tipo") IsNot Nothing Then

                    TipoSelecionado = Nothing
                    cbxTipo.Text = String.Empty

                    TipoSelecionado = Session("Dto_Banc_Tipo")

                    If Not String.IsNullOrEmpty(TipoSelecionado) Then
                        cbxTipo.Text = TipoSelecionado
                    Else
                        If ListaTiposDeCuentas IsNot Nothing AndAlso ListaTiposDeCuentas.Count > 0 Then
                            cbxTipo.SelectedIndex = 0
                        End If
                    End If

                    ' Session("Dto_Banc_Tipo") = Nothing

                End If

                If Session("Dto_Banc_Observaciones") IsNot Nothing Then

                    ObservacionesSelecionado = Nothing
                    txtObs.Text = String.Empty
                    txtObs.ToolTip = String.Empty

                    ObservacionesSelecionado = Session("Dto_Banc_Observaciones")

                    If Not String.IsNullOrEmpty(ObservacionesSelecionado) Then
                        txtObs.Text = ObservacionesSelecionado
                        txtObs.ToolTip = ObservacionesSelecionado
                    End If

                    '   Session("Dto_Banc_Observaciones") = Nothing

                End If

                If Session("Dto_Banc_NroCuenta") IsNot Nothing Then

                    NroCuentaSelecionado = Nothing
                    txtCuenta.Text = String.Empty
                    txtCuenta.ToolTip = String.Empty

                    NroCuentaSelecionado = Session("Dto_Banc_NroCuenta")

                    If Not String.IsNullOrEmpty(NroCuentaSelecionado) Then
                        txtCuenta.Text = NroCuentaSelecionado
                        txtCuenta.ToolTip = NroCuentaSelecionado
                    End If

                    '   Session("Dto_Banc_NroCuenta") = Nothing

                End If

                If Session("Dto_Banc_BolDefecto") IsNot Nothing Then

                    BolDefectoSelecionado = Nothing
                    chkDefecto.Checked = False

                    BolDefectoSelecionado = Session("Dto_Banc_BolDefecto")

                    If Not String.IsNullOrEmpty(BolDefectoSelecionado) Then
                        chkDefecto.Checked = BolDefectoSelecionado
                    End If

                    '  Session("Dto_Banc_BolDefecto") = Nothing

                End If



                If Session("Dto_Banc_CodigoAgencia") IsNot Nothing Then

                    CodigoAgenciaSelecionado = Nothing
                    txtAgencia.Text = String.Empty

                    CodigoAgenciaSelecionado = Session("Dto_Banc_CodigoAgencia")

                    If Not String.IsNullOrEmpty(CodigoAgenciaSelecionado) Then
                        txtAgencia.Text = CodigoAgenciaSelecionado
                    End If

                    '  Session("Dto_Banc_CodigoAgencia") = Nothing

                End If



                If Session("Dto_Banc_DescAdicionalCampo1") IsNot Nothing Then

                    DescAdicionalCampo1Selecionado = Nothing
                    txtCampoAdicional1.Text = String.Empty

                    DescAdicionalCampo1Selecionado = Session("Dto_Banc_DescAdicionalCampo1")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo1Selecionado) Then
                        txtCampoAdicional1.Text = DescAdicionalCampo1Selecionado
                    End If

                    '  Session("Dto_Banc_DescAdicionalCampo1") = Nothing

                End If


                If Session("Dto_Banc_DescAdicionalCampo2") IsNot Nothing Then

                    DescAdicionalCampo2Selecionado = Nothing
                    txtCampoAdicional2.Text = String.Empty

                    DescAdicionalCampo2Selecionado = Session("Dto_Banc_DescAdicionalCampo2")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo2Selecionado) Then
                        txtCampoAdicional2.Text = DescAdicionalCampo2Selecionado
                    End If

                    '  Session("Dto_Banc_DescAdicionalCampo2") = Nothing

                End If

                If Session("Dto_Banc_DescAdicionalCampo3") IsNot Nothing Then

                    DescAdicionalCampo3Selecionado = Nothing
                    txtCampoAdicional3.Text = String.Empty

                    DescAdicionalCampo3Selecionado = Session("Dto_Banc_DescAdicionalCampo3")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo3Selecionado) Then
                        txtCampoAdicional3.Text = DescAdicionalCampo3Selecionado
                    End If

                    '  Session("Dto_Banc_DescAdicionalCampo3") = Nothing

                End If

                If Session("Dto_Banc_DescAdicionalCampo4") IsNot Nothing Then

                    DescAdicionalCampo4Selecionado = Nothing
                    txtCampoAdicional4.Text = String.Empty

                    DescAdicionalCampo4Selecionado = Session("Dto_Banc_DescAdicionalCampo4")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo4Selecionado) Then
                        txtCampoAdicional4.Text = DescAdicionalCampo4Selecionado
                    End If

                    ' Session("Dto_Banc_DescAdicionalCampo4") = Nothing

                End If


                If Session("Dto_Banc_DescAdicionalCampo5") IsNot Nothing Then

                    DescAdicionalCampo5Selecionado = Nothing
                    txtCampoAdicional5.Text = String.Empty

                    DescAdicionalCampo5Selecionado = Session("Dto_Banc_DescAdicionalCampo5")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo5Selecionado) Then
                        txtCampoAdicional5.Text = DescAdicionalCampo5Selecionado
                    End If

                    ' Session("Dto_Banc_DescAdicionalCampo5") = Nothing

                End If


                If Session("Dto_Banc_DescAdicionalCampo6") IsNot Nothing Then

                    DescAdicionalCampo6Selecionado = Nothing
                    txtCampoAdicional6.Text = String.Empty

                    DescAdicionalCampo6Selecionado = Session("Dto_Banc_DescAdicionalCampo6")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo6Selecionado) Then
                        txtCampoAdicional6.Text = DescAdicionalCampo6Selecionado
                    End If

                    ' Session("Dto_Banc_DescAdicionalCampo6") = Nothing

                End If


                If Session("Dto_Banc_DescAdicionalCampo7") IsNot Nothing Then

                    DescAdicionalCampo7Selecionado = Nothing
                    txtCampoAdicional7.Text = String.Empty

                    DescAdicionalCampo7Selecionado = Session("Dto_Banc_DescAdicionalCampo7")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo7Selecionado) Then
                        txtCampoAdicional7.Text = DescAdicionalCampo7Selecionado
                    End If

                    ' Session("Dto_Banc_DescAdicionalCampo7") = Nothing

                End If


                If Session("Dto_Banc_DescAdicionalCampo8") IsNot Nothing Then

                    DescAdicionalCampo8Selecionado = Nothing
                    txtCampoAdicional8.Text = String.Empty

                    DescAdicionalCampo8Selecionado = Session("Dto_Banc_DescAdicionalCampo8")

                    If Not String.IsNullOrEmpty(DescAdicionalCampo8Selecionado) Then
                        txtCampoAdicional8.Text = DescAdicionalCampo8Selecionado
                    End If

                    '  Session("Dto_Banc_DescAdicionalCampo8") = Nothing

                End If

                If Session("Dto_Banc_Pendiente") IsNot Nothing AndAlso Session("Dto_Banc_Pendiente") = True Then
                    lblAprobacionPendienteMensaje.Text = RecuperarValorDic("lblAprobacionPendienteMensaje")
                    lblAprobacionPendienteMensaje.Visible = True
                Else
                    lblAprobacionPendienteMensaje.Visible = False
                End If


                If Session("Dto_Banc_CodigoBancario") IsNot Nothing Then

                    CodigoBancarioSelecionado = Nothing
                    txtCodigoBancario.Text = String.Empty

                    CodigoBancarioSelecionado = Session("Dto_Banc_CodigoBancario")

                    If Not String.IsNullOrEmpty(CodigoBancarioSelecionado) Then
                        txtCodigoBancario.Text = CodigoBancarioSelecionado
                    End If


                End If

            End If
            Session("btnAceptar") = Nothing


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Private Sub CarregarParametroAprovacion()

        Dim camposAprobacion As String = String.Empty
        Dim lstCandidadAprovaciones = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_CANTIDAD_APROVACIONES)

        If lstCandidadAprovaciones IsNot Nothing AndAlso lstCandidadAprovaciones.Count > 0 Then
            If Not lstCandidadAprovaciones.ElementAt(0).MultiValue AndAlso lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                CandidadAprobaciones = lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0)
            Else
                If lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    CandidadAprobaciones = lstCandidadAprovaciones.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If

        Dim auxCamposAprobacion = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_CAMPOS_APROVACIONES)

        If (Not String.IsNullOrWhiteSpace(CandidadAprobaciones)) AndAlso CandidadAprobaciones <> "0" AndAlso
            auxCamposAprobacion IsNot Nothing AndAlso auxCamposAprobacion.Count > 0 Then
            If Not auxCamposAprobacion.ElementAt(0).MultiValue AndAlso auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                camposAprobacion = auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0)
            Else
                If auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    camposAprobacion = auxCamposAprobacion.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
            If Not String.IsNullOrWhiteSpace(camposAprobacion) Then
                LstCamposAprobacion = Split(camposAprobacion, ";").Select(Function(x) x.Trim()).ToList()
            End If
        End If


        Dim AprobacionDatosBancariosAlta = Prosegur.Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, Comon.Constantes.CODIGO_PARAMETRO_APROBACION_ALTA)
        If AprobacionDatosBancariosAlta IsNot Nothing AndAlso AprobacionDatosBancariosAlta.Count > 0 Then
            If Not AprobacionDatosBancariosAlta.ElementAt(0).MultiValue AndAlso AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                AprobacionAlta = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
            Else
                If AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    AprobacionAlta = AprobacionDatosBancariosAlta.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If


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

    Protected Overrides Sub TraduzirControles()


        MyBase.CodFuncionalidad = "UCDATOSBANCARIOS"
        MyBase.CarregaDicinario()


        Me.Page.Title = MyBase.RecuperarValorDic("087_lbl_titulo")

        lblCodigoBancario.Text = MyBase.RecuperarValorDic("lbl_codigo_bancario")
        lblDefecto.Text = MyBase.RecuperarValorDic("lbl_patron")
        lblNroDocumento.Text = MyBase.RecuperarValorDic("lbl_numero_documento")
        lblTitularidad.Text = MyBase.RecuperarValorDic("lbl_titularidad")
        lblAgencia.Text = MyBase.RecuperarValorDic("lbl_agencia")
        lblCuenta.Text = MyBase.RecuperarValorDic("lbl_numero_cuenta")
        lblDivisa.Text = MyBase.RecuperarValorDic("lbl_divisa")
        lblTipo.Text = MyBase.RecuperarValorDic("lbl_tipo")
        lblObs.Text = MyBase.RecuperarValorDic("lbl_observaciones")

        lblDatosAdicionales.Text = MyBase.RecuperarValorDic("lbl_datos_adicionales")


        lblCampoAdicional1.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_1")
        lblCampoAdicional2.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_2")
        lblCampoAdicional3.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_3")
        lblCampoAdicional4.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_4")
        lblCampoAdicional5.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_5")
        lblCampoAdicional6.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_6")
        lblCampoAdicional7.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_7")
        lblCampoAdicional8.Text = MyBase.RecuperarValorDic("lbl_campo_adicional_8")





        'Botoes
        btnLimpar.Text = MyBase.RecuperarValorDic("btnLimpiar")
        btnAceptar.Text = MyBase.RecuperarValorDic("btnAceptar")
        btnAceptar.ToolTip = MyBase.RecuperarValorDic("btnAceptar")
        btnLimpar.ToolTip = MyBase.RecuperarValorDic("btnLimpiar")

        '  csvBancoObrigatorio.ErrorMessage = Traduzir("087_msg_csvBancoObrigatorio")
        csvCuentaObrigatorio.ErrorMessage = Traduzir("087_msg_csvCuentaObrigatorio")
        csvDivisaObrigatorio.ErrorMessage = Traduzir("087_msg_csvDivisaObrigatorio")
        csvTipoObrigatorio.ErrorMessage = Traduzir("087_msg_csvTipoObrigatorio")
        csvTitularidadObrigatorio.ErrorMessage = Traduzir("087_msg_csvTitularidadObrigatorio")

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Consulta, Aplicacao.Util.Utilidad.eAcao.Busca
                btnAceptar.Visible = False
                'btnBuscarCliente.Visible = False
                btnLimpar.Visible = False

        End Select

        ' txtCliente.Enabled = False

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        '  Dim MensagemErro As String = MontaMensagensErro()

        'If MensagemErro <> String.Empty Then
        '    Master.ControleErro.ShowError(MensagemErro, False)
        'End If
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property ListaDivisa() As ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion

        Get
            Return ViewState("ListaDivisa")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion)
            ViewState("ListaDivisa") = value
        End Set

    End Property

    Private Property ListaTiposDeCuentas() As ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion

        Get
            Return ViewState("ListaTiposDeCuentas")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion)
            ViewState("ListaTiposDeCuentas") = value
        End Set

    End Property

    Private Property ListaCuenta() As Dictionary(Of Integer, String)

        Get
            Return ViewState("ListaCuenta")
        End Get

        Set(value As Dictionary(Of Integer, String))
            ViewState("ListaCuenta") = value
        End Set

    End Property


    Private Property CandidadAprobaciones() As String

        Get
            Return ViewState("candidadAprovaciones")
        End Get

        Set(value As String)
            ViewState("candidadAprovaciones") = value
        End Set

    End Property
    Private Property AprobacionAlta() As String

        Get
            Return ViewState("aprobacionAlta")
        End Get

        Set(value As String)
            ViewState("aprobacionAlta") = value
        End Set

    End Property


    Private Property LstCamposAprobacion() As List(Of String)

        Get
            Dim AUX As List(Of String) = ViewState("LstCamposAprobacion")
            If AUX Is Nothing Then
                ViewState("LstCamposAprobacion") = New List(Of String)
            End If
            Return ViewState("LstCamposAprobacion")
        End Get

        Set(value As List(Of String))
            ViewState("LstCamposAprobacion") = value
        End Set

    End Property

    Private ReadOnly Property NuevoDatoBancario() As String
        Get
            Return Request.QueryString("nuevoDatoBancario")
        End Get
    End Property
#End Region

#Region "[METODOS]"

    Private Sub CarregarHelpers()
        Try
            If Session("Dto_Banc_BancoSelecionado") IsNot Nothing Then
                Dim oClienteSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente = Session("Dto_Banc_BancoSelecionado")

                Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
                objCliente.Identificador = oClienteSelecionado.OidCliente
                objCliente.Codigo = oClienteSelecionado.Codigo
                objCliente.Descripcion = oClienteSelecionado.Descripcion
                Clientes.Clear()
                Clientes.Add(objCliente)

                AtualizaDadosHelperCliente(Clientes)

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
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

        ucClientes.ucCliente.RegistrosSelecionados = dadosCliente
        ucClientes.ucCliente.ExibirDados(True)
    End Sub


    Private Sub ConsomeCliente()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            LimparCliente()
            'BancoSelecionado = GetComboClientes(Clientes.FirstOrDefault().Codigo).Clientes.FirstOrDefault()
            'Argentina - IAC - Erro ao vincular agência bancária ao cliente
            'Para garantir que os dados retornados sejam a mesma estrutura de objeto que a tela necessitava foi utilizado um método para retornar essa estrutura.
            'O metodo GetComboClientes retorna uma lista de objetos cujo o codigo contenha o valor informado.
            'Foi adicionado um novo metodo para retornar o valor igual ao informado.
            Dim bancos = GetComboClientes(Clientes.FirstOrDefault().Identificador).Clientes
            BancoSelecionado = bancos.FirstOrDefault(Function(x) x.OidCliente = Clientes.FirstOrDefault().Identificador)
        End If

    End Sub
    Private Function GetComboClientes(oidCliente) As ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        If Not String.IsNullOrEmpty(oidCliente) Then
            objPeticion.Identificador = New List(Of String)
            objPeticion.Identificador.Add(oidCliente)
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function

    Private Sub ConsomeDivisa()

        If Session("Dto_Banc_DivisaSelecionado") IsNot Nothing Then

            LimparDivisa()

            DivisaSelecionado = Session("Dto_Banc_DivisaSelecionado")

            If DivisaSelecionado.Identificador IsNot Nothing Then
                ddlDivisa.SelectedValue = DivisaSelecionado.Identificador
            End If

            '  Session("Dto_Banc_DivisaSelecionado") = Nothing

        End If

    End Sub

    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If ValidarCamposObrigatorios Then

            'Validação OK
            If BancoSelecionado Is Nothing Then
                strErro.Append(Traduzir("087_msg_csvBancoObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)
            Else
                ' csvBancoObrigatorio.IsValid = True
            End If

            If String.IsNullOrEmpty(ddlDivisa.SelectedValue) Then
                strErro.Append(csvDivisaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)

                csvDivisaObrigatorio.IsValid = False
                If SetarFocoControle AndAlso Not focoSetado Then
                    ddlDivisa.Focus()
                    focoSetado = True
                End If

            Else
                csvDivisaObrigatorio.IsValid = True
            End If

            If String.IsNullOrEmpty(txtCuenta.Text) Then
                strErro.Append(csvCuentaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)

                csvCuentaObrigatorio.IsValid = False
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCuenta.Focus()
                    focoSetado = True
                End If

            Else
                csvCuentaObrigatorio.IsValid = True
            End If

            If String.IsNullOrEmpty(cbxTipo.Text) OrElse cbxTipo.Text.Equals(Traduzir("gen_ddl_selecione")) Then
                strErro.Append(csvTipoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)

                csvTipoObrigatorio.IsValid = False
                If SetarFocoControle AndAlso Not focoSetado Then
                    cbxTipo.Focus()
                    focoSetado = True
                End If

            Else
                If cbxTipo.Text.Length > 50 Then
                    cbxTipo.Text = cbxTipo.Text.Substring(0, 50) ' PGP-725
                End If

                csvTipoObrigatorio.IsValid = True
            End If

            If String.IsNullOrEmpty(txtTitularidad.Text) Then
                strErro.Append(csvTitularidadObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)

                csvTitularidadObrigatorio.IsValid = False
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtTitularidad.Focus()
                    focoSetado = True
                End If

            Else
                csvTitularidadObrigatorio.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Private Sub LimparDivisa()
        DivisaSelecionado = Nothing
        ddlDivisa.SelectedValue = String.Empty
    End Sub

    Private Sub LimparTipo()
        TipoSelecionado = Nothing
        cbxTipo.SelectedIndex = -1
    End Sub

    Private Sub LimparCliente()
        BancoSelecionado = Nothing
        'txtCliente.Text = String.Empty
        'txtCliente.ToolTip = String.Empty
    End Sub

    Public Sub SetClienteSelecionadoPopUp()
        Session("Dto_Banc_objBanco") = BancoSelecionado
    End Sub

    Public Sub CarregarDivisa()

        Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDivisas.Respuesta
        Dim objDivisa As IAC.ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Dim objDivisaCol As IAC.ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion = Nothing

        objRespuesta = objProxyUtilidad.GetComboDivisas()

        If objRespuesta.Divisas Is Nothing Then
            ddlDivisa.Items.Clear()
            ddlDivisa.Items.Add(New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlDivisa.Items.Clear()
            ddlDivisa.Items.Add(New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        objDivisaCol = New IAC.ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion
        For Each item In objRespuesta.Divisas
            objDivisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa
            objDivisaCol.Add(item)
        Next

        ListaDivisa = New ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion
        ListaDivisa.AddRange(objDivisaCol.OrderBy(Function(s) s.Descripcion))

        ddlDivisa.AppendDataBoundItems = True
        ddlDivisa.Items.Clear()
        ddlDivisa.Items.Add(New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))
        ddlDivisa.DataTextField = "Descripcion"
        ddlDivisa.DataValueField = "Identificador"
        ddlDivisa.DataSource = ListaDivisa
        ddlDivisa.DataBind()

    End Sub

    Public Sub CarregarTipos()

        Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.Respuesta
        Dim objTipoDeCuenta As IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuenta
        Dim objTipoDeCuentaCol As IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion = Nothing

        objRespuesta = objProxyUtilidad.GetComboTiposCuenta()

        If objRespuesta.TiposDeCuentas Is Nothing Then
            cbxTipo.Items.Clear()
            cbxTipo.Items.Add(New DevExpress.Web.ASPxEditors.ListEditItem(Traduzir("gen_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            cbxTipo.Items.Clear()
            cbxTipo.Items.Add(New DevExpress.Web.ASPxEditors.ListEditItem(Traduzir("gen_ddl_selecione"), String.Empty))

            Exit Sub
        End If

        objTipoDeCuentaCol = New IAC.ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion
        For Each item In objRespuesta.TiposDeCuentas

            objTipoDeCuenta = New ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuenta
            objTipoDeCuenta.Indice = item.Indice
            objTipoDeCuenta.Descripcion = Traduzir(item.Descripcion.ToString())
            objTipoDeCuentaCol.Add(item)
        Next

        ListaTiposDeCuentas = New ContractoServicio.Utilidad.GetComboTiposCuenta.TipoDeCuentaColeccion
        ListaTiposDeCuentas.AddRange(objTipoDeCuentaCol)


        cbxTipo.Items.Clear()
        cbxTipo.DataSource = ListaTiposDeCuentas
        'cbxTipo.Items.Add(New DevExpress.Web.ASPxEditors.ListEditItem(Traduzir("gen_ddl_selecione"), String.Empty))

        cbxTipo.ValueField = "Descripcion"
        cbxTipo.TextField = "Descripcion"
        'cbxTipo.SelectedIndex = 0
        cbxTipo.DataBind()

    End Sub


#End Region

#Region "[EVENTOS]"

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            Session("btnAceptar") = True
            ValidarCamposObrigatorios = True

            Dim strErros As String = MontaMensagensErro(True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            If HayModificacion() Then
                'Mostrar Modal de comentario
                Dim url As String = "BusquedaDatosBancariosComentarioPopup.aspx"
                Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 220, 400, False, False, btnAlertaSi.ClientID)

            Else
                btnAlertaSi_Click(sender, e)
            End If



        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnAlertaNo_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNo.Click

    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click

        Try
            Respuesta = New RespuestaBusqueda()
            'Evalua si el dato bancario tiene una modificación o se previamente estaba con cambios pendientes
            Respuesta.Pendiente = HayModificacion() OrElse (Session("Dto_Banc_Pendiente") IsNot Nothing AndAlso Session("Dto_Banc_Pendiente"))
            Respuesta.Peticion = Peticion
            Respuesta.Banco = BancoSelecionado
            Respuesta.BolDefecto = chkDefecto.Checked
            Respuesta.NroCuenta = txtCuenta.Text
            Respuesta.NroDocumento = txtNroDocumento.Text
            Respuesta.Obs = txtObs.Text
            Respuesta.Tipo = cbxTipo.Text
            Respuesta.Titularidad = txtTitularidad.Text

            Respuesta.Divisa = New ContractoServicio.Utilidad.GetComboDivisas.Divisa
            If ListaDivisa IsNot Nothing Then
                Dim objDivisaSelec = ListaDivisa.Find(Function(a) a.Identificador = ddlDivisa.SelectedValue)
                If objDivisaSelec IsNot Nothing Then
                    Respuesta.Divisa = objDivisaSelec
                End If
            End If


            Respuesta.Agencia = txtAgencia.Text

            Respuesta.CampoAdicional1 = txtCampoAdicional1.Text
            Respuesta.CampoAdicional2 = txtCampoAdicional2.Text
            Respuesta.CampoAdicional3 = txtCampoAdicional3.Text
            Respuesta.CampoAdicional4 = txtCampoAdicional4.Text
            Respuesta.CampoAdicional5 = txtCampoAdicional5.Text
            Respuesta.CampoAdicional6 = txtCampoAdicional6.Text
            Respuesta.CampoAdicional7 = txtCampoAdicional7.Text
            Respuesta.CampoAdicional8 = txtCampoAdicional8.Text
            Respuesta.Comentario = Session("Dto_Banc_Comentario")

            If String.IsNullOrEmpty(divModal) AndAlso String.IsNullOrEmpty(ifrModal) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaNivelOk", "FecharAtualizarPaginaPai();", True)
            Else
                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



    Public Function HayModificacion() As Boolean
        Dim aprov As Boolean = False

        'Evalua en caso de que se trate de un Alta (NuevoDatoBancario) y el parametro AprobacionDatosBancariosAlta sea False (0)
        'Devuelve False ya que no pasa por el proceso de aprobación
        If NuevoDatoBancario AndAlso (String.IsNullOrWhiteSpace(AprobacionAlta) OrElse AprobacionAlta = "0") Then
            Return False
        End If

        If String.IsNullOrWhiteSpace(CandidadAprobaciones) OrElse CandidadAprobaciones = "0" Then
            Return False
        End If

        If DivisaSelecionado Is Nothing OrElse ddlDivisa.SelectedValue <> DivisaSelecionado.Identificador Then
            If LstCamposAprobacion.Contains("OID_DIVISA") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If Session("Dto_Banc_BancoSelecionado") IsNot Nothing Then
            Dim oClienteSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente = Session("Dto_Banc_BancoSelecionado")

            If oClienteSelecionado.Codigo <> BancoSelecionado.Codigo Then
                If LstCamposAprobacion.Contains("OID_BANCO") OrElse LstCamposAprobacion.Count = 0 Then
                    aprov = True
                End If
            End If
        Else
            If LstCamposAprobacion.Contains("OID_BANCO") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        'If Clientes(0).Codigo <> BancoSelecionado.Codigo Then
        '    If LstCamposAprobacion.Contains("OID_BANCO") OrElse LstCamposAprobacion.Count = 0 Then
        '        aprov = True
        '    End If
        'End If



        If txtNroDocumento.Text <> NroDocumentoSelecionado Then

            If LstCamposAprobacion.Contains("COD_DOCUMENTO") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtTitularidad.Text <> TitularidadSelecionado Then
            If LstCamposAprobacion.Contains("DES_TITULARIDAD") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If cbxTipo.Text <> TipoSelecionado Then
            If LstCamposAprobacion.Contains("COD_TIPO_CUENTA_BANCARIA") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If

        End If

        If txtObs.Text <> ObservacionesSelecionado Then
            If LstCamposAprobacion.Contains("DES_OBSERVACIONES") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If


        If txtCuenta.Text <> NroCuentaSelecionado Then
            If LstCamposAprobacion.Contains("COD_CUENTA_BANCARIA") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If


        If txtAgencia.Text <> CodigoAgenciaSelecionado Then
            If LstCamposAprobacion.Contains("COD_AGENCIA") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional1.Text <> DescAdicionalCampo1Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_1") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional2.Text <> DescAdicionalCampo2Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_2") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional3.Text <> DescAdicionalCampo3Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_3") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional4.Text <> DescAdicionalCampo4Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_4") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional5.Text <> DescAdicionalCampo5Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_5") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional6.Text <> DescAdicionalCampo6Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_6") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional7.Text <> DescAdicionalCampo7Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_7") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        If txtCampoAdicional8.Text <> DescAdicionalCampo8Selecionado Then
            If LstCamposAprobacion.Contains("DES_CAMPO_ADICIONAL_8") OrElse LstCamposAprobacion.Count = 0 Then
                aprov = True
            End If
        End If

        Return aprov
    End Function


    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            LimparCliente()
            LimparDivisa()
            NroDocumentoSelecionado = Nothing
            txtNroDocumento.Text = String.Empty
            txtNroDocumento.ToolTip = String.Empty
            TitularidadSelecionado = Nothing
            txtTitularidad.Text = String.Empty
            txtTitularidad.ToolTip = String.Empty
            TipoSelecionado = Nothing
            cbxTipo.Text = String.Empty
            ObservacionesSelecionado = Nothing
            txtObs.Text = String.Empty
            txtObs.ToolTip = String.Empty
            NroCuentaSelecionado = Nothing
            txtCuenta.Text = String.Empty
            txtCuenta.ToolTip = String.Empty
            BolDefectoSelecionado = Nothing
            chkDefecto.Checked = False

            txtAgencia.Text = String.Empty
            CodigoAgenciaSelecionado = Nothing

            txtCampoAdicional1.Text = String.Empty
            DescAdicionalCampo1Selecionado = Nothing

            txtCampoAdicional2.Text = String.Empty
            DescAdicionalCampo2Selecionado = Nothing

            txtCampoAdicional3.Text = String.Empty
            DescAdicionalCampo3Selecionado = Nothing

            txtCampoAdicional4.Text = String.Empty
            DescAdicionalCampo4Selecionado = Nothing

            txtCampoAdicional5.Text = String.Empty
            DescAdicionalCampo5Selecionado = Nothing

            txtCampoAdicional6.Text = String.Empty
            DescAdicionalCampo6Selecionado = Nothing

            txtCampoAdicional7.Text = String.Empty
            DescAdicionalCampo7Selecionado = Nothing

            txtCampoAdicional8.Text = String.Empty
            DescAdicionalCampo8Selecionado = Nothing

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes)

            ValidarCamposObrigatorios = False
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Shared Property Peticion As PeticionBusqueda
        Get
            Return HttpContext.Current.Session("Dto_Banc_objEntidade")
        End Get
        Set(value As PeticionBusqueda)
            HttpContext.Current.Session("Dto_Banc_objEntidade") = value
        End Set
    End Property

    Public Shared Property Respuesta As RespuestaBusqueda
        Get
            Return HttpContext.Current.Session("Dto_Banc_objRespuesta")
        End Get
        Set(value As RespuestaBusqueda)
            HttpContext.Current.Session("Dto_Banc_objRespuesta") = value
        End Set
    End Property

    Private Property BancoSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("BancoSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("BancoSelecionado") = value
        End Set
    End Property

    Private Property NroDocumentoSelecionado As String
        Get
            Return ViewState("NroDocumentoSelecionado")
        End Get
        Set(value As String)
            ViewState("NroDocumentoSelecionado") = value
        End Set
    End Property

    Private Property TitularidadSelecionado As String
        Get
            Return ViewState("TitularidadSelecionado")
        End Get
        Set(value As String)
            ViewState("TitularidadSelecionado") = value
        End Set
    End Property

    Private Property TipoSelecionado As String
        Get
            Return ViewState("TipoSelecionado")
        End Get
        Set(value As String)
            ViewState("TipoSelecionado") = value
        End Set
    End Property

    Private Property ObservacionesSelecionado As String
        Get
            Return ViewState("ObservacionesSelecionado")
        End Get
        Set(value As String)
            ViewState("ObservacionesSelecionado") = value
        End Set
    End Property

    Private Property NroCuentaSelecionado As String
        Get
            Return ViewState("NroCuentaSelecionado")
        End Get
        Set(value As String)
            ViewState("NroCuentaSelecionado") = value
        End Set
    End Property

    Private Property BolDefectoSelecionado As Boolean
        Get
            Return ViewState("BolDefectoSelecionado")
        End Get
        Set(value As Boolean)
            ViewState("BolDefectoSelecionado") = value
        End Set
    End Property

    Private Property DivisaSelecionado As ContractoServicio.Utilidad.GetComboDivisas.Divisa
        Get
            Return ViewState("DivisaSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboDivisas.Divisa)
            ViewState("DivisaSelecionado") = value
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


    Private Property CodigoAgenciaSelecionado As String
        Get
            Return ViewState("CodigoAgenciaSelecionado")
        End Get
        Set(value As String)
            ViewState("CodigoAgenciaSelecionado") = value
        End Set
    End Property

    Private Property DescAdicionalCampo1Selecionado As String
        Get
            Return ViewState("DescAdicionalCampo1Selecionado")
        End Get
        Set(value As String)
            ViewState("DescAdicionalCampo1Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo2Selecionado As String
        Get
            Return ViewState("DescAdicionalCampo2Selecionado")
        End Get
        Set(value As String)
            ViewState("DescAdicionalCampo2Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo3Selecionado As String
        Get
            Return ViewState("DescAdicionalCampo3Selecionado")
        End Get
        Set(value As String)
            ViewState("DescAdicionalCampo3Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo4Selecionado As String
        Get
            Return ViewState("DescAdicionalCampo4Selecionado")
        End Get
        Set(value As String)
            ViewState("DescAdicionalCampo4Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo5Selecionado As String
        Get
            Return ViewState("DescAdicionalCampo5Selecionado")
        End Get
        Set(value As String)
            ViewState("DescAdicionalCampo5Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo6Selecionado As String
        Get
            Return Session("DescAdicionalCampo6Selecionado")
        End Get
        Set(value As String)
            Session("DescAdicionalCampo6Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo7Selecionado As String
        Get
            Return Session("DescAdicionalCampo7Selecionado")
        End Get
        Set(value As String)
            Session("DescAdicionalCampo7Selecionado") = value
        End Set
    End Property
    Private Property DescAdicionalCampo8Selecionado As String
        Get
            Return Session("DescAdicionalCampo8Selecionado")
        End Get
        Set(value As String)
            Session("DescAdicionalCampo8Selecionado") = value
        End Set
    End Property

    Private Property CodigoBancarioSelecionado As String
        Get
            Return Session("CodigoBancarioSelecionado")
        End Get
        Set(value As String)
            Session("CodigoBancarioSelecionado") = value
        End Set
    End Property


    <Serializable()>
    Public Class PeticionBusqueda

        'REMOVER
        Public Property CodGenesis As String
        Public Property DesGenesis As String
        Public Property OidGenesis As String
        'REMOVER

        Public Property Identificador As String

    End Class

    <Serializable()>
    Public Class RespuestaBusqueda

        Public Property Peticion As PeticionBusqueda

        Public Property Banco As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Public Property NroDocumento As String
        Public Property Titularidad As String
        Public Property Tipo As String
        Public Property Obs As String
        Public Property NroCuenta As String
        Public Property BolDefecto As Boolean
        Public Property Divisa As ContractoServicio.Utilidad.GetComboDivisas.Divisa

        Public Property Agencia As String
        Public Property CampoAdicional1 As String
        Public Property CampoAdicional2 As String
        Public Property CampoAdicional3 As String
        Public Property CampoAdicional4 As String
        Public Property CampoAdicional5 As String
        Public Property CampoAdicional6 As String
        Public Property CampoAdicional7 As String
        Public Property CampoAdicional8 As String
        Public Property Comentario As String
        Public Property Pendiente As Boolean
    End Class

#End Region
End Class