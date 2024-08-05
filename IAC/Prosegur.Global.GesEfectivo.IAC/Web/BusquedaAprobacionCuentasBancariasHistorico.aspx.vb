Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports DevExpress.Web.ASPxEditors
Imports System.Globalization
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarDatosBancarios

Public Class BusquedaAprobacionCuentasBancariasHistorico
    Inherits Base

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS
        MyBase.ValidarAcao = True
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Aprobación cuentas bancarias")
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True

            If Not Page.IsPostBack Then
                Clientes = Nothing
                UsuariosAprobacion = Nothing
                UsuariosModificacion = Nothing
                ConfigurarControl_FechaEstado()
                aprobacionesGrid = Nothing
                grid.DataSource = Nothing
                grid.DataBind()
                divGrilla.Visible = False
            End If

            ConfigurarControle_Cliente()
            ConfigurarControle_UsuarioModificacion()
            ConfigurarControle_UsuarioAprobacion()
            ConfigurarControl_Estado(Not IsPostBack)
            ConfigurarControl_Campos(Not IsPostBack)

        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub
    Protected Overrides Sub TraduzirControles()
        CodFuncionalidad = "APROBACION_CUENTAS_BANCARIAS"
        CarregaDicinario()


        Master.Titulo = MyBase.RecuperarValorDic("lblCuentasBancarias")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")

        lblTituloHistorialCuentaBancaria.Text = MyBase.RecuperarValorDic("lblHistorial")
        lblEstado.Text = MyBase.RecuperarValorDic("lblEstado")
        lblFechaDe.Text = MyBase.RecuperarValorDic("lblFechaDe")
        lblDesde.Text = MyBase.RecuperarValorDic("lblDesde")
        lblHasta.Text = MyBase.RecuperarValorDic("lblHasta")
        lblCampo.Text = MyBase.RecuperarValorDic("lblCampo")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        'Grid 
        grid.Columns(0).Caption = MyBase.RecuperarValorDic("lblCliente")
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblSubcliente")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("lblPuntoServicio")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("lblDivisa")
        grid.Columns(4).Caption = MyBase.RecuperarValorDic("lblAprobados")
        grid.Columns(5).Caption = MyBase.RecuperarValorDic("lblPendientes")
        grid.Columns(6).Caption = MyBase.RecuperarValorDic("lblRechazados")
    End Sub
    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        Try
            MyBase.AdicionarScripts()
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaDesde.ClientID, "True")
            script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaHasta.ClientID, "True")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
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
                _ucClientes.ID = Me.ID & "_ucClientesHistAprobacion"
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

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = False

        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.ucSubCliente.MultiSelecao = True

        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False
        Me.ucClientes.ucPtoServicio.MultiSelecao = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
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

    Private Sub AtualizaDadosHelperSubClienteForm(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        If observableCollection.FirstOrDefault().SubClientes IsNot Nothing Then
            For Each c In observableCollection.FirstOrDefault().SubClientes
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = observableCollection.FirstOrDefault().Identificador
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
        End If
        pUserControl.Clientes = observableCollection
        pUserControl.ucSubCliente.RegistrosSelecionados = dadosCliente
        pUserControl.ucSubCliente.ExibirDados(True)
    End Sub


#End Region


#Region "[HelpersUsuarioModificacion]"
    Public Property UsuariosModificacion As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Usuario)
        Get
            Return ucUsuariosModificacion.Usuarios
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Usuario))
            ucUsuariosModificacion.Usuarios = value
        End Set
    End Property

    Private WithEvents _ucUsuariosModificacion As ucUsuario
    Public Property ucUsuariosModificacion() As ucUsuario
        Get
            If _ucUsuariosModificacion Is Nothing Then
                _ucUsuariosModificacion = LoadControl(ResolveUrl("~\Controles\Helpers\ucUsuario.ascx"))
                _ucUsuariosModificacion.ID = Me.ID & "_ucUsuariosModificacion"
                AddHandler _ucUsuariosModificacion.Erro, AddressOf ErroControles
                phUsuarioModificacion.Controls.Add(_ucUsuariosModificacion)
            End If
            Return _ucUsuariosModificacion
        End Get
        Set(value As ucUsuario)
            _ucUsuariosModificacion = value
        End Set
    End Property

    Private Sub ConfigurarControle_UsuarioModificacion()
        Me.ucUsuariosModificacion.SelecaoMultipla = True
        Me.ucUsuariosModificacion.UsuarioHabilitado = True
        Me.ucUsuariosModificacion.UsuarioObrigatorio = False
        Me.ucUsuariosModificacion.JoinTabla = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.DatoBancarioCambio
        Me.ucUsuariosModificacion.UsuarioTitulo = MyBase.RecuperarValorDic("lblUsuarioModificacion")

        If UsuariosModificacion IsNot Nothing Then
            Me.ucUsuariosModificacion.Usuarios = UsuariosModificacion
        End If
    End Sub
    Private Sub ucUsuariosModificacion_OnControleAtualizado() Handles _ucUsuariosModificacion.UpdatedControl
        Try
            If ucUsuariosModificacion.Usuarios IsNot Nothing Then
                UsuariosModificacion = ucUsuariosModificacion.Usuarios
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[HelpersUsuarioAprobacion]"
    Public Property UsuariosAprobacion As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Usuario)
        Get
            Return ucUsuariosAprobacion.Usuarios
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Usuario))
            ucUsuariosAprobacion.Usuarios = value
        End Set
    End Property

    Private WithEvents _ucUsuariosAprobacion As ucUsuario
    Public Property ucUsuariosAprobacion() As ucUsuario
        Get
            If _ucUsuariosAprobacion Is Nothing Then
                _ucUsuariosAprobacion = LoadControl(ResolveUrl("~\Controles\Helpers\ucUsuario.ascx"))
                _ucUsuariosAprobacion.ID = Me.ID & "_ucUsuariosAprobacion"
                AddHandler _ucUsuariosAprobacion.Erro, AddressOf ErroControles
                phUsuarioAprobacion.Controls.Add(_ucUsuariosAprobacion)
            End If
            Return _ucUsuariosAprobacion
        End Get
        Set(value As ucUsuario)
            _ucUsuariosAprobacion = value
        End Set
    End Property

    Private Sub ConfigurarControle_UsuarioAprobacion()
        Me.ucUsuariosAprobacion.SelecaoMultipla = True
        Me.ucUsuariosAprobacion.UsuarioHabilitado = True
        Me.ucUsuariosAprobacion.UsuarioObrigatorio = False
        Me.ucUsuariosAprobacion.JoinTabla = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.DatoBancarioAprobacion
        Me.ucUsuariosAprobacion.UsuarioTitulo = MyBase.RecuperarValorDic("lblUsuarioAprobacion")

        If UsuariosAprobacion IsNot Nothing Then
            Me.ucUsuariosAprobacion.Usuarios = UsuariosAprobacion
        End If
    End Sub
    Private Sub ucUsuariosAprobacion_OnControleAtualizado() Handles _ucUsuariosAprobacion.UpdatedControl
        Try
            If ucUsuariosAprobacion.Usuarios IsNot Nothing Then
                UsuariosAprobacion = ucUsuariosAprobacion.Usuarios
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperUsuario(observableCollection As ObservableCollection(Of Comon.Clases.Usuario), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucUsuario)
        Dim dadosUsuario As New Comon.RespuestaHelper
        dadosUsuario.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Login
                    .Descricao = c.Nombre
                End With
                dadosUsuario.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUserControl.ucUsuario.RegistrosSelecionados = dadosUsuario
        pUserControl.ucUsuario.ExibirDados(True)
    End Sub
#End Region

#Region "[PROPIEDADES]"
    Private Property aprobacionesGrid As List(Of Comon.Clases.DatoBancarioGrilla)
        Get
            Return Session("aprobacionesHistoricoGrid")
        End Get
        Set(value As List(Of Comon.Clases.DatoBancarioGrilla))
            Session("aprobacionesHistoricoGrid") = value
        End Set
    End Property
    Private Property aprobacion_OidDatoBancario As String
        Get
            Return Session("aprobacionHistorico_OidDatoBancario")
        End Get
        Set(value As String)
            Session("aprobacionHistorico_OidDatoBancario") = value
        End Set
    End Property
    Private Property aprobacionesNecesarias As Integer
        Get
            Return ViewState("aprobacionesHistoricoNecesarias")
        End Get
        Set(value As Integer)
            ViewState("aprobacionesHistoricoNecesarias") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            LimparCamposFiltro()

            aprobacionesGrid = Nothing
            grid.DataSource = Nothing
            grid.DataBind()
            divGrilla.Visible = False
            updForm.Update()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        CargarGrilla(obtenerPeticionGrilla)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

    End Sub

    Private Sub BusquedaAprobacionCuentasBancarias_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        AtualizaDadosHelperCliente(Clientes, ucClientes)
        AtualizaDadosHelperUsuario(UsuariosModificacion, ucUsuariosModificacion)
        AtualizaDadosHelperUsuario(UsuariosAprobacion, ucUsuariosAprobacion)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        grid.DataSource = aprobacionesGrid
        grid.DataBind()
    End Sub

    Protected Sub btnComentario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComentario.Click
        PopupComentarioAprobacion(Page.Request.Params.Get("__EVENTARGUMENT"))
    End Sub

#End Region

#Region "[METODOS]"

    Private Function obtenerPeticionGrilla() As Contractos.Integracion.RecuperarDatosBancarios.Peticion
        Dim pPeticion As New Contractos.Integracion.RecuperarDatosBancarios.Peticion

        pPeticion.CodigoUsuario = LoginUsuario
        'TODO AgregarCultura
        pPeticion.Cultura = ""

        Dim objProxyDelegacion As New Comunicacion.ProxyWS.IAC.ProxyDelegacion
        Dim objPeticionDelegacion As New IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Peticion
        Dim objRespuestaDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacionDetail.Respuesta
        objPeticionDelegacion.CodigoDelegacione = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Delegacion.Codigo

        objRespuestaDelegacion = objProxyDelegacion.GetDelegacioneDetail(objPeticionDelegacion)
        If objRespuestaDelegacion IsNot Nothing AndAlso objRespuestaDelegacion.Delegacion.Count > 0 Then
            pPeticion.CodigoPais = objRespuestaDelegacion.Delegacion(0).CodPais
        End If

        'Se agrega filtro de estados
        pPeticion.CodigosEstados.AddRange(getEstadosFiltroBusqueda())

        'Se agrega filtro de campos
        pPeticion.CodigosCampos.AddRange(getCamposFiltroBusqueda())

        If (Not String.IsNullOrWhiteSpace(ddlTipoFecha.SelectedValue)) Then
            pPeticion.CodigoTipoFecha = ddlTipoFecha.SelectedValue
            If Not String.IsNullOrEmpty(txtFechaDesde.Text) Then
                pPeticion.FechaDesde = DateTime.ParseExact(txtFechaDesde.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture).DataHoraGMTZero(DelegacionLogada)
            End If
            If Not String.IsNullOrEmpty(txtFechaHasta.Text) Then
                pPeticion.FechaHasta = DateTime.ParseExact(txtFechaHasta.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture).DataHoraGMTZero(DelegacionLogada)
            End If
        End If


        'Agregamos a la peticion los OID de usuarios de modificación seleccionados
        If ucUsuariosModificacion.Usuarios IsNot Nothing AndAlso ucUsuariosModificacion.Usuarios.Count > 0 Then
            For Each usuarioModificacion In ucUsuariosModificacion.Usuarios
                pPeticion.OidUsuariosModificacion.Add(usuarioModificacion.Identificador)
            Next
        End If

        'Agregamos a la peticion los OID de usuarios de aprobación seleccionados
        If ucUsuariosAprobacion.Usuarios IsNot Nothing AndAlso ucUsuariosAprobacion.Usuarios.Count > 0 Then
            For Each usuarioAprobacion In ucUsuariosAprobacion.Usuarios
                pPeticion.OidUsuariosAprobacion.Add(usuarioAprobacion.Identificador)
            Next
        End If

        'Agregamos a la peticion los OID de clientes seleccionados
        If ucClientes.Clientes IsNot Nothing AndAlso ucClientes.Clientes.Count > 0 Then
            For Each cliente In ucClientes.Clientes
                'Agregamos a la peticion los OID de subClientes seleccionados
                If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then
                    For Each subCliente In cliente.SubClientes
                        'Agregamos a la peticion los OID de los puntos de servicio seleccionados
                        If subCliente.PuntosServicio IsNot Nothing AndAlso subCliente.PuntosServicio.Count > 0 Then
                            For Each puntoServicio In subCliente.PuntosServicio
                                pPeticion.OidPuntosServicios.Add(puntoServicio.Identificador)
                            Next
                        Else
                            pPeticion.OidSubClientes.Add(subCliente.Identificador)
                        End If
                    Next
                Else
                    pPeticion.OidClientes.Add(cliente.Identificador)
                End If
            Next
        End If

        Return pPeticion
    End Function
    Protected Sub CargarGrilla(pPeticion As Contractos.Integracion.RecuperarDatosBancarios.Peticion)

        If Not EsPeticionVacia(pPeticion) Then
            'Permite mostrar el detalle de una sola fila por vez
            grid.SettingsDetail.AllowOnlyOneMasterRowExpanded = True

            'Busco los datos para la grilla
            aprobacionesGrid = New LogicaNegocio.AccionDatoBancario().GetDatosBancariosCambio(pPeticion)

            'Actualizo el TimeZone de las fechas
            For Each objDatoBancario In aprobacionesGrid
                For Each objDatoBancarioDetalle In objDatoBancario.Detalle
                    objDatoBancarioDetalle.FechaCambio = objDatoBancarioDetalle.FechaCambio.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)

                    For Each objAprobacion In objDatoBancarioDetalle.Aprobaciones.UsuariosAprobadores
                        objAprobacion.FechaAprobacion = objAprobacion.FechaAprobacion.QuieroExibirEstaFechaEnLaPatalla(DelegacionLogada)
                    Next
                Next
            Next

            grid.DataSource = aprobacionesGrid
            grid.DetailRows.CollapseAllRows()
            grid.DataBind()
            divGrilla.Visible = True
            updForm.Update()
        Else
            MyBase.MostraMensagem(MyBase.RecuperarValorDic("msgDebeIngresarAlMenosUnFiltro"))
        End If


    End Sub

    Private Function EsPeticionVacia(pPeticion As Peticion) As Boolean
        If pPeticion.OidClientes IsNot Nothing AndAlso pPeticion.OidClientes.Count > 0 Then
            Return False
        End If

        If pPeticion.OidSubClientes IsNot Nothing AndAlso pPeticion.OidSubClientes.Count > 0 Then
            Return False
        End If

        If pPeticion.OidPuntosServicios IsNot Nothing AndAlso pPeticion.OidPuntosServicios.Count > 0 Then
            Return False
        End If

        If pPeticion.CodigosEstados IsNot Nothing AndAlso pPeticion.CodigosEstados.Count > 0 Then
            Return False
        End If

        If pPeticion.CodigosCampos IsNot Nothing AndAlso pPeticion.CodigosCampos.Count > 0 Then
            Return False
        End If

        If Not String.IsNullOrEmpty(pPeticion.CodigoTipoFecha) AndAlso (pPeticion.FechaDesde.HasValue OrElse pPeticion.FechaHasta.HasValue) Then
            Return False
        End If

        If pPeticion.OidUsuariosAprobacion IsNot Nothing AndAlso pPeticion.OidUsuariosAprobacion.Count > 0 Then
            Return False
        End If

        If pPeticion.OidUsuariosModificacion IsNot Nothing AndAlso pPeticion.OidUsuariosModificacion.Count > 0 Then
            Return False
        End If

        Return True
    End Function

    Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        'Almacena en Session la key del registro seleccionado en la grilla principal 
        Dim detailGrid = TryCast(sender, ASPxGridView)
        If detailGrid.GetType() Is GetType(ASPxGridView) Then
            Dim clave = detailGrid.GetMasterRowKeyValue()
            aprobacion_OidDatoBancario = clave
        End If
    End Sub


    ' Metodo utilizado por la propiedad OnInit en el control ASPxGridView del aspx
    ' al cargar la grilla de detalle para traducir las columnas
    Protected Sub detailGrid_OnInit(ByVal sender As Object, ByVal e As EventArgs)
        TraduzirControles()
        Dim detailGrid = TryCast(sender, ASPxGridView)
        If detailGrid.GetType() Is GetType(ASPxGridView) Then
            detailGrid.Columns(0).Caption = MyBase.RecuperarValorDic("lblCampoModificado")
            detailGrid.Columns(1).Caption = MyBase.RecuperarValorDic("lblValorAnterior")
            detailGrid.Columns(2).Caption = MyBase.RecuperarValorDic("lblValorModificado")
            detailGrid.Columns(3).Caption = MyBase.RecuperarValorDic("lblUsuarioModificacion")
            detailGrid.Columns(4).Caption = MyBase.RecuperarValorDic("lblFechaCambio")
            detailGrid.Columns(5).Caption = MyBase.RecuperarValorDic("lblComentarios")
            detailGrid.Columns(6).Caption = MyBase.RecuperarValorDic("lblEstado")
            detailGrid.Columns(7).Caption = MyBase.RecuperarValorDic("lblAprobaciones")
        End If
    End Sub

    ''' <summary>
    ''' Metodo utilizado por el Objeto gridDetailDataSource que llena la grilla de detalles
    ''' </summary>
    ''' <returns></returns>
    Public Function GetGrillaDetalle() As List(Of Comon.Clases.DatoBancarioGrillaDetalle)
        If aprobacionesGrid IsNot Nothing AndAlso aprobacionesGrid.Count > 0 Then

            Dim datoBancarioCambio = aprobacionesGrid.FirstOrDefault(Function(x) x.OidDatoBancario = aprobacion_OidDatoBancario)

            If datoBancarioCambio IsNot Nothing Then
                Return datoBancarioCambio.Detalle
            End If
            Return Nothing
        End If
        Return Nothing
    End Function

    Private Sub LimparCamposFiltro()
        Try
            Clientes.Clear()
            Clientes.Add(New Comon.Clases.Cliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)


            UsuariosAprobacion.Clear()
            UsuariosAprobacion.Add(New Comon.Clases.Usuario())
            AtualizaDadosHelperUsuario(UsuariosAprobacion, ucUsuariosAprobacion)


            UsuariosModificacion.Clear()
            UsuariosModificacion.Add(New Comon.Clases.Usuario())
            AtualizaDadosHelperUsuario(UsuariosModificacion, ucUsuariosModificacion)

            ddlTipoFecha.SelectedIndex = 0
            CType(ddlStado.FindControl("listStado"), ASPxListBox).UnselectAll()
            ddlStado.Text = String.Empty
            CType(ddlCampos.FindControl("listCampos"), ASPxListBox).UnselectAll()
            ddlCampos.Text = String.Empty

            txtFechaDesde.Text = String.Empty
            txtFechaHasta.Text = String.Empty


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ConfigurarControl_Estado(valorInicial As Boolean)
        Try
            Dim listEstado As ASPxListBox = CType(ddlStado.FindControl("listStado"), ASPxListBox)

            If valorInicial Then
                listEstado.Items.Clear()
                listEstado.Items.Insert(0, (New ListEditItem(MyBase.RecuperarValorDic("lblTodos"), "ALL")))
                listEstado.Items.Insert(1, (New ListEditItem(MyBase.RecuperarValorDic("lblAprobado"), "AP")))
                listEstado.Items.Insert(2, (New ListEditItem(MyBase.RecuperarValorDic("lblPendiente"), "PD")))
                listEstado.Items.Insert(3, (New ListEditItem(MyBase.RecuperarValorDic("lblRechazado"), "RE")))
            Else
                listEstado.Items.Insert(4, New ListEditItem("", ""))
                listEstado.Items.RemoveAt(4)
            End If
            listEstado.DataBind()
        Catch ex As Exception
            'No hacer nada.
        End Try
    End Sub

    Private Sub ConfigurarControl_FechaEstado()
        Try
            CodFuncionalidad = "APROBACION_CUENTAS_BANCARIAS"
            CarregaDicinario()

            ddlTipoFecha.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            ddlTipoFecha.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblAprobacion"), "AP"))
            ddlTipoFecha.Items.Add(New ListItem(MyBase.RecuperarValorDic("lblModificacion"), "MD"))
            'ddlTipoFecha.DataBind()
        Catch ex As Exception
            'No hacer nada.
        End Try
    End Sub
    Private Sub ConfigurarControl_Campos(valorInicial As Boolean)
        Try
            Dim list As ASPxListBox = CType(ddlCampos.FindControl("listCampos"), ASPxListBox)

            If valorInicial Then
                'Busco el parametro CamposAprobacionRequeridaCuentasBancarias
                Dim param = Genesis.LogicaNegocio.Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, "CamposAprobacionRequeridaCuentasBancarias")
                Dim valor As String = ""

                'Cargo el diccionario para la traduccion de los campos
                CodFuncionalidad = "DATOSBANCARIOS"
                CarregaDicinario()


                list.Items.Clear()
                list.Items.Insert(0, (New ListEditItem(MyBase.RecuperarValorDic("lblTodos"), "ALL")))

                'Guardo el valor del parametro
                If param.Count >= 0 Then
                    valor = param(0).Valores(0)
                End If

                'Si el parametro no tiene valor asigno a la lista todos los campos que pueden requerir aprobación
                If String.IsNullOrEmpty(valor) Then
                    list.Items.Insert(1, (New ListEditItem(MyBase.RecuperarValorDic("OID_BANCO"), "OID_BANCO")))
                    list.Items.Insert(2, (New ListEditItem(MyBase.RecuperarValorDic("OID_DIVISA"), "OID_DIVISA")))
                    list.Items.Insert(3, (New ListEditItem(MyBase.RecuperarValorDic("COD_CUENTA_BANCARIA"), "COD_CUENTA_BANCARIA")))
                    list.Items.Insert(4, (New ListEditItem(MyBase.RecuperarValorDic("COD_TIPO_CUENTA_BANCARIA"), "COD_TIPO_CUENTA_BANCARIA")))
                    list.Items.Insert(5, (New ListEditItem(MyBase.RecuperarValorDic("COD_DOCUMENTO"), "COD_DOCUMENTO")))
                    list.Items.Insert(6, (New ListEditItem(MyBase.RecuperarValorDic("COD_AGENCIA"), "COD_AGENCIA")))
                    list.Items.Insert(7, (New ListEditItem(MyBase.RecuperarValorDic("DES_TITULARIDAD"), "DES_TITULARIDAD")))
                    list.Items.Insert(8, (New ListEditItem(MyBase.RecuperarValorDic("BOL_DEFECTO"), "BOL_DEFECTO")))
                    list.Items.Insert(9, (New ListEditItem(MyBase.RecuperarValorDic("BOL_ACTIVO"), "BOL_ACTIVO")))
                    list.Items.Insert(10, (New ListEditItem(MyBase.RecuperarValorDic("DES_OBSERVACIONES"), "DES_OBSERVACIONES")))
                    list.Items.Insert(11, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_1"), "DES_CAMPO_ADICIONAL_1")))
                    list.Items.Insert(12, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_2"), "DES_CAMPO_ADICIONAL_2")))
                    list.Items.Insert(13, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_3"), "DES_CAMPO_ADICIONAL_3")))
                    list.Items.Insert(14, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_4"), "DES_CAMPO_ADICIONAL_4")))
                    list.Items.Insert(15, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_5"), "DES_CAMPO_ADICIONAL_5")))
                    list.Items.Insert(16, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_6"), "DES_CAMPO_ADICIONAL_6")))
                    list.Items.Insert(17, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_7"), "DES_CAMPO_ADICIONAL_7")))
                    list.Items.Insert(18, (New ListEditItem(MyBase.RecuperarValorDic("DES_CAMPO_ADICIONAL_8"), "DES_CAMPO_ADICIONAL_8")))
                Else
                    'Separo los valores separados por ;
                    Dim valores = valor.Split(";")
                    For Each campo In valores
                        'Inserto en la lista cada uno de los valores configurados en el parametro
                        list.Items.Insert(list.Items.Count, New ListEditItem(MyBase.RecuperarValorDic(campo.Trim()), campo.Trim()))
                    Next
                End If
            Else
                list.Items.Insert(list.Items.Count, New ListEditItem("", ""))
                list.Items.RemoveAt(list.Items.Count - 1)
            End If
            list.DataBind()
        Catch ex As Exception
            'No hacer nada.
        End Try
    End Sub
    Private Function getEstadosFiltroBusqueda() As List(Of String)
        Dim listaRetorno As New List(Of String)
        Dim listEstado As ASPxListBox = CType(ddlStado.FindControl("listStado"), ASPxListBox)

        'Recorre los estados seleccionados omitiendo ALL (Todos)
        For Each elemento In listEstado.Items
            If elemento.selected AndAlso elemento.value <> "ALL" Then
                listaRetorno.Add(elemento.value.ToString())
            End If
        Next

        Return listaRetorno
    End Function
    Private Function getCamposFiltroBusqueda() As List(Of String)
        Dim listaRetorno As New List(Of String)
        Dim list As ASPxListBox = CType(ddlCampos.FindControl("listCampos"), ASPxListBox)

        'Recorre los estados seleccionados omitiendo ALL (Todos)
        For Each elemento In list.Items
            If elemento.selected AndAlso elemento.value <> "ALL" Then
                listaRetorno.Add(elemento.value.ToString())
            End If
        Next

        Return listaRetorno
    End Function

    Public Sub PopupComentarioAprobacion(identificador As String)
        Dim url As String = "BusquedaDatosBancariosComentariosDetallesPopUp.aspx?identificador=" & identificador
        Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 400, 700, False, False, String.Empty)
    End Sub

    Protected Function BuscaPostbackComentario(container As String) As String
        Dim strPostBack As String = String.Empty

        strPostBack = "__doPostBack('" + btnComentario.UniqueID + "', '" + container + "')"

        Return strPostBack
    End Function
#End Region
End Class