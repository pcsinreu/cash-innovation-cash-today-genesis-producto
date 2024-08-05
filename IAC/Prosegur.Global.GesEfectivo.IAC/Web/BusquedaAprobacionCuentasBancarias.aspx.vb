Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.ContractoServicio
Imports DevExpress.Web.ASPxClasses
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Genesis.Comon.Extenciones

Public Class BusquedaAprobacionCuentasBancarias
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

            ConfigurarControle_Cliente()

            If Not Page.IsPostBack Then
                'LimparCamposFiltro()
                LimpiarForm()
                CargarGrilla(obtenerPeticionGrilla)
            End If


        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub
    Protected Overrides Sub TraduzirControles()
        MyBase.CodFuncionalidad = "APROBACION_CUENTAS_BANCARIAS"
        CarregaDicinario()

        Master.Titulo = MyBase.RecuperarValorDic("lblCuentasBancarias")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")

        lblTituloAprobacionCuentaBancaria.Text = MyBase.RecuperarValorDic("lblCambios")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnAprobar.Text = MyBase.RecuperarValorDic("btnAprobar")
        btnRechazar.Text = MyBase.RecuperarValorDic("btnRechazar")

        'Grid 
        grid.Columns(0).Caption = MyBase.RecuperarValorDic("lblCliente")
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblSubcliente")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("lblPuntoServicio")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("lblDivisa")
        grid.Columns(4).Caption = MyBase.RecuperarValorDic("lblNumeroCampos")
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
                _ucClientes.ID = Me.ID & "_ucClientesAprobacion"
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
            LimpiarForm()
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

#Region "[PROPIEDADES]"
    Private Property aprobacionesGrid As List(Of Comon.Clases.DatoBancarioGrilla)
        Get
            Return Session("aprobacionesGrid")
        End Get
        Set(value As List(Of Comon.Clases.DatoBancarioGrilla))
            Session("aprobacionesGrid") = value
        End Set
    End Property
    Private Property aprobacion_OidDatoBancario As String
        Get
            Return Session("aprobacion_OidDatoBancario")
        End Get
        Set(value As String)
            Session("aprobacion_OidDatoBancario") = value
        End Set
    End Property
    Private Property aprobacionesNecesarias As Integer
        Get
            Return ViewState("aprobacionesNecesarias")
        End Get
        Set(value As Integer)
            ViewState("aprobacionesNecesarias") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"
    Private Sub btnAprobar_Click(sender As Object, e As EventArgs) Handles btnAprobar.Click
        Session("EsAprobado") = True
        AprobarRechazarPrevio(sender, e)

    End Sub

    Private Sub AprobarRechazarPrevio(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(Session("Dto_Banc_Aprob")) Then
            btnAlertaSi_Click(sender, e)

        Else
            Dim url As String = "AprobadorDatosBancariosPopup.aspx"
            Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 220, 400, False, False, btnAlertaSi.ClientID)
        End If
    End Sub

    Private Sub btnRechazar_Click(sender As Object, e As EventArgs) Handles btnRechazar.Click
        Session("EsAprobado") = False
        AprobarRechazarPrevio(sender, e)
    End Sub
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            LimparCamposFiltro()

            CargarGrilla(obtenerPeticionGrilla)

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        LimpiarForm()

        CargarGrilla(obtenerPeticionGrilla)

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

    End Sub

    Private Sub btnAlertaNo_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNo.Click

    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click
        Dim comando As String
        If Session("EsAprobado") Then
            comando = "APROBADO"
        Else
            comando = "RECHAZADO"
        End If
        AprobarRechazar(comando)
    End Sub


    Private Sub BusquedaAprobacionCuentasBancarias_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        AtualizaDadosHelperCliente(Clientes, ucClientes)
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        grid.DataSource = aprobacionesGrid
        grid.DataBind()
    End Sub

    Protected Sub grid_RowCommand(sender As Object, e As ASPxGridViewRowCommandEventArgs) Handles grid.RowCommand
        Select Case e.CommandArgs.CommandName
            Case "PopupComparativo"
                PopupComparativo(e.CommandArgs.CommandArgument)
        End Select

    End Sub

    Protected Sub btnComentario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComentario.Click
        PopupComentarioAprobacion(Page.Request.Params.Get("__EVENTARGUMENT"))
    End Sub


#End Region

#Region "[METODOS]"
    ''' <summary>
    ''' Recupera los valores seleccionados en la grilla detalles
    ''' </summary>
    ''' <returns>Lista de OID_DATO_BANCARIO_CAMBIO</returns>
    Private Function RecuperarOidDatoBancarioCambioSeleccionado() As List(Of String)
        'Obtengo el indice del registro seleccionado en la grilla principal
        Dim indice = grid.FindVisibleIndexByKeyValue(aprobacion_OidDatoBancario)
        'Obtengo la grilla de detalles del indice encontrado anteriormente, indicando tambien el Id del ASPxGridView 
        Dim detailGrid = TryCast(grid.FindDetailRowTemplateControl(indice, "detailGrid"), ASPxGridView)

        If (detailGrid IsNot Nothing) Then
            'Valido que tenga items seleccionados
            If (detailGrid.Selection.Count > 0) Then
                'Obengo los valores seleccionados del registro OidDatoBancarioCambio
                Dim seleccionados = detailGrid.GetSelectedFieldValues("OidDatoBancarioCambio")

                If seleccionados.Count > 0 Then
                    'Convierto la seleccion de List (Of Object) a List (Of String)
                    Dim oids = seleccionados.Select(Function(x) x.ToString).ToList

                    Return oids
                End If
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Llama al metodo AprobarRechazar de LogicaNegocio
    ''' </summary>
    ''' <param name="accion">Tipo de accion "APROBADO" o "RECHAZADO" </param>
    Private Sub AprobarRechazar(accion As String)
        Dim oidsDatoBancarioCambio = RecuperarOidDatoBancarioCambioSeleccionado()

        Dim tester_aprobacion = InformacionUsuario.Permisos.Contains("TESTER_APROBACION")

        If tester_aprobacion OrElse ValidacionAprobarRechazar(accion, oidsDatoBancarioCambio) Then

            If (oidsDatoBancarioCambio IsNot Nothing AndAlso oidsDatoBancarioCambio.Count > 0) Then
                Try
                    Dim objAccionDatoBancario As New IAC.LogicaNegocio.AccionDatoBancario


                    Dim listaCodigos As New List(Of String)
                    listaCodigos.Add(DelegacionLogada.Codigo)

                    Dim objDelegacion = Genesis.LogicaNegocio.Genesis.Delegacion.ObtenerDelegaciones(New Contractos.Comon.Delegacion.ObtenerDelegaciones.Peticion With {
                                                                                                     .CodigosDelegaciones = listaCodigos
                                                                                                     }).Delegaciones.FirstOrDefault

                    Dim objRespuestaDatoBancario As New ContractoServicio.DatoBancario.SetDatosBancarios.Respuesta

                    If objDelegacion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(objDelegacion.CodigoPais) Then
                        objRespuestaDatoBancario = objAccionDatoBancario.AprobarRechazar(oidsDatoBancarioCambio, LoginUsuario, accion, Session("Dto_Banc_Aprob"), tester_aprobacion, objDelegacion.CodigoPais)
                    End If


                    'Validar respuesta si hay error
                    If objRespuestaDatoBancario.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                    Else
                        MyBase.MostraMensagem(objRespuestaDatoBancario.MensajeError)
                    End If
                Catch ex As Exception
                    MyBase.MostraMensagemExcecao(ex)
                Finally
                    CargarGrilla(obtenerPeticionGrilla)
                End Try
            End If
        Else

            MyBase.MostraMensagem(MyBase.RecuperarValorDic("lblErrorAprobador"))
        End If
        Session("Dto_Banc_Aprob") = String.Empty
    End Sub

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

        'Se agrega Estado de dato bancario cambio PENDIENTE
        pPeticion.CodigosEstados.Add("PD")

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
        updForm.Update()
    End Sub

    Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        'Almacena en Session la key del registro seleccionado en la grilla principal 
        Dim detailGrid = TryCast(sender, ASPxGridView)
        If detailGrid.GetType() Is GetType(ASPxGridView) Then
            Dim clave = detailGrid.GetMasterRowKeyValue()
            aprobacion_OidDatoBancario = clave

            If aprobacionesGrid.FirstOrDefault(Function(x) x.OidDatoBancario = clave).Detalle.Count >= 10 Then
                detailGrid.Settings.VerticalScrollableHeight = 300
                detailGrid.Settings.VerticalScrollBarMode = ScrollBarMode.Auto
            End If
        End If
    End Sub


    ' Metodo utilizado por la propiedad OnInit en el control ASPxGridView del aspx
    ' al cargar la grilla de detalle para traducir las columnas
    Protected Sub detailGrid_OnInit(ByVal sender As Object, ByVal e As EventArgs)
        TraduzirControles()
        Dim detailGrid = TryCast(sender, ASPxGridView)
        If detailGrid.GetType() Is GetType(ASPxGridView) Then
            detailGrid.Columns(1).Caption = MyBase.RecuperarValorDic("lblCampoModificado")
            detailGrid.Columns(2).Caption = MyBase.RecuperarValorDic("lblValorActual")
            detailGrid.Columns(3).Caption = MyBase.RecuperarValorDic("lblValorModificado")
            detailGrid.Columns(4).Caption = MyBase.RecuperarValorDic("lblUsuarioModificacion")
            detailGrid.Columns(5).Caption = MyBase.RecuperarValorDic("lblFechaCambio")
            detailGrid.Columns(6).Caption = MyBase.RecuperarValorDic("lblComentarios")
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

    ''' <summary>
    ''' Reestablece los valores de los controles por defecto
    ''' </summary>
    Private Sub LimpiarForm()

        btnAprobar.Enabled = False
        btnRechazar.Enabled = False
    End Sub
    Private Sub LimparCamposFiltro()
        Try
            Clientes.Clear()
            Clientes.Add(New Comon.Clases.Cliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Function ValidacionAprobarRechazar(accion As String, oidSelecionados As List(Of String)) As Boolean

        'Obtengo el indice del registro seleccionado en la grilla principal
        Dim indice = grid.FindVisibleIndexByKeyValue(aprobacion_OidDatoBancario)
        'Obtengo la grilla de detalles del indice encontrado anteriormente, indicando tambien el Id del ASPxGridView 
        Dim detailGrid = TryCast(grid.FindDetailRowTemplateControl(indice, "detailGrid"), ASPxGridView)


        Dim dato = aprobacionesGrid.FirstOrDefault(Function(x) x.OidDatoBancario = aprobacion_OidDatoBancario)

        If dato IsNot Nothing Then
            Dim cambioConUsuario As Boolean = False
            Dim aprovacionConUsuario As Boolean = False

            Dim cambios = dato.Detalle.Where(Function(x) oidSelecionados.Contains(x.OidDatoBancarioCambio))
            If cambios IsNot Nothing Then

                cambioConUsuario = cambios.Any(Function(x) x.LoginUsuario = MyBase.LoginUsuario)

                aprovacionConUsuario = cambios.Any(Function(x) x.Aprobaciones IsNot Nothing AndAlso x.Aprobaciones.UsuariosAprobadores IsNot Nothing AndAlso x.Aprobaciones.UsuariosAprobadores.Any(Function(z) z.Login = MyBase.LoginUsuario))

                If accion = "APROBADO" Then
                    If cambioConUsuario Then
                        Return False
                    End If
                    If aprovacionConUsuario Then
                        Return False
                    End If
                End If
            End If
        End If
        Return True
    End Function

    Public Sub PopupComparativo(identificador As String)
        Dim url As String = "BusquedaDatosBancariosPopupComparativo.aspx?identificador=" & identificador
        Master.ExibirModal(url, Traduzir("087_lbl_titulo"), 400, 700, False, False, String.Empty)
    End Sub

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