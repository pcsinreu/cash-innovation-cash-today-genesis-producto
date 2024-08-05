Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxEditors
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Enumeradores
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.LogicaNegocio.Integracion
Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos
Imports System.Globalization
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports Prosegur.Genesis.Comon.Clases

Public Class BusquedaPeriodosAcreditacion
    Inherits Base

#Region "Métodos sobreescritos (overrides)"
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PANTALLA_PERIODOS
        MyBase.ValidarAcao = True
        MyBase.ValidarPemissaoAD = False
    End Sub
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda de periodos de acreditación y desbloqueo")
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.MenuRodapeVisivel = True
            Master.MostrarRodape = True


            If Not Page.IsPostBack Then
                Me.EstadosPeriodos = Nothing
                Me.RecuperarEstados(EstadosPeriodos)
                Me.ClientesBancosForm = Nothing
                Me.DeviceIDs = Nothing
                Me.grid.DataSource = Nothing
                Me.grid.DataBind()
                Me.divGrilla.Visible = False
                dvExportGrid.Visible = False
            End If



            btnDesbloq.Visible = ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PANTALLA_PERIODOS,
                                                      Aplicacao.Util.Utilidad.eAcao.Modificacion)


            ConfigurarControl_Banco()
            ConfigurarControl_EstadosPeriodo(Not IsPostBack)
            ConfigurarControl_DeviceID()
            ConfigurarControle_Planificacion()

        Catch ex As Exception
            MyBase.MostraMensagem(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Adiciona javascript a los controles
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

    Protected Overrides Sub TraduzirControles()
        CodFuncionalidad = "PERIODOS_ACREDITACION"
        CarregaDicinario()


        Master.Titulo = MyBase.RecuperarValorDic("lblPeriodos")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("041_lbl_SubTitulosCriteriosBusqueda")
        lblTituloPeriodos.Text = MyBase.RecuperarValorDic("lblPeriodos")
        lblEstado.Text = MyBase.RecuperarValorDic("lblEstado")
        lblFechaPeriodo.Text = MyBase.RecuperarValorDic("lblFechaPeriodo")
        lblDesde.Text = MyBase.RecuperarValorDic("lblDesde")
        lblHasta.Text = MyBase.RecuperarValorDic("lblHasta")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")

        'Grid 
        grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblBanco")
        grid.Columns(2).Caption = MyBase.RecuperarValorDic("lblPlanificacion")
        grid.Columns(3).Caption = MyBase.RecuperarValorDic("lblDeviceId")
        grid.Columns(4).Caption = MyBase.RecuperarValorDic("lblFyhInicio")
        grid.Columns(5).Caption = MyBase.RecuperarValorDic("lblFyhFin")
        grid.Columns(6).Caption = MyBase.RecuperarValorDic("lblDivisa")
        grid.Columns(7).Caption = MyBase.RecuperarValorDic("lblTipoLimite")
        grid.Columns(8).Caption = MyBase.RecuperarValorDic("lblLimiteConfigurado")
        grid.Columns(9).Caption = MyBase.RecuperarValorDic("lblValorActual")
        grid.Columns(10).Caption = MyBase.RecuperarValorDic("lblEstado")
        grid.Columns(11).Caption = MyBase.RecuperarValorDic("lblAcreditado")

        btnDesbloq.Text = MyBase.RecuperarValorDic("btnDesbloquear")

        ucClienteTipoBancoForm.ClienteTitulo = MyBase.RecuperarValorDic("lbl_banco")
        ucDeviceIDs.DeviceIDTitulo = MyBase.RecuperarValorDic("Device ID")

    End Sub
#End Region

#Region "Eventos y métodos propios"
    Private Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim tipo = Request.Params("__EVENTARGUMENT")

        Dim options As XlsxExportOptions = New XlsxExportOptions()
        options.TextExportMode = TextExportMode.Value

        gridExport.Landscape = True
        gridExport.LeftMargin = 0
        gridExport.RightMargin = 0
        grid.ExpandAll()
        Select Case tipo
            Case "PDF"

                gridExport.WritePdfToResponse(Traduzir("mn_periodosAcreditacion") & " " & DateTime.Now.ToString("yyyyMMddHHmmss"))' WritePdfToResponse()
            Case "XLS"
                gridExport.WriteXlsToResponse(Traduzir("mn_periodosAcreditacion") & " " & DateTime.Now.ToString("yyyyMMddHHmmss"))
            Case "XLSX"
                gridExport.WriteXlsxToResponse(Traduzir("mn_periodosAcreditacion") & " " & DateTime.Now.ToString("yyyyMMddHHmmss"), options)
            Case "CSV"
                gridExport.WriteCsvToResponse(Traduzir("mn_periodosAcreditacion") & " " & DateTime.Now.ToString("yyyyMMddHHmmss"))
                '    Case "RTF"
                '        gridExport.WriteRtfToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"))
        End Select

    End Sub
    Private Sub BusquedaPeriodosAcreditacion_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ActualizaDatosHelperBanco(ClientesBancosForm, ucClienteTipoBancoForm)
        ActualizaDatosHelperDeviceID(DeviceIDs, ucDeviceIDs)
        ActualizaDatosHelperPlanificacion(Planificaciones, ucPlanificaciones)
    End Sub

    Private Sub ActualizaDatosHelperPlanificacion(planificaciones As ObservableCollection(Of Planificacion), ucPlanificaciones As ucPlanificacion)
        Dim datos As New Comon.RespuestaHelper
        datos.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In planificaciones
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                datos.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        ucPlanificaciones.ucPlanificacion.RegistrosSelecionados = datos
        ucPlanificaciones.ucPlanificacion.ExibirDados(True)
    End Sub


    Private Sub ActualizaDatosHelperDeviceID(pDeviceID As ObservableCollection(Of Clases.Maquina), pUcDeviceID As ucDeviceID)
        Dim datosMaquinas As New Comon.RespuestaHelper
        datosMaquinas.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In pDeviceID
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                datosMaquinas.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUcDeviceID.ucDeviceID.RegistrosSelecionados = datosMaquinas
        pUcDeviceID.ucDeviceID.ExibirDados(True)
    End Sub

    Private Sub ActualizaDatosHelperBanco(pBancos As ObservableCollection(Of Clases.Cliente), pUcClienteTipoBancoForm As ucCliente)

        Dim datosBanco As New Comon.RespuestaHelper
        datosBanco.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In pBancos
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                datosBanco.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        pUcClienteTipoBancoForm.ucCliente.RegistrosSelecionados = datosBanco
        pUcClienteTipoBancoForm.ucCliente.ExibirDados(True)

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            LimpiarCamposFiltro()

            grid.DataSource = Nothing
            grid.DataBind()
            dvExportGrid.Visible = False
            divGrilla.Visible = False
            updForm.Update()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub LimpiarCamposFiltro()
        Me.DeviceIDs.Clear()
        Me.DeviceIDs.Add(New Comon.Clases.Maquina())
        ActualizaDatosHelperDeviceID(Me.DeviceIDs, Me.ucDeviceIDs)

        ClientesBancosForm.Clear()
        ClientesBancosForm.Add(New Comon.Clases.Cliente())
        ActualizaDatosHelperBanco(Me.ClientesBancosForm, Me.ucClienteTipoBancoForm)


        Planificaciones.Clear()
        Planificaciones.Add(New Planificacion())
        ActualizaDatosHelperPlanificacion(Planificaciones, ucPlanificaciones)

        CType(ddlEstadoPeriodo.FindControl("listEstadoPeriodo"), ASPxListBox).UnselectAll()
        ddlEstadoPeriodo.Text = String.Empty

        txtFechaDesde.Text = String.Empty
        txtFechaHasta.Text = String.Empty
    End Sub

    Private Property PeriodosAcreditacion As ObservableCollection(Of Clases.PeriodoAcreditacionGrilla)
        Get
            If Session("PeriodosAcreditacion") Is Nothing Then
                Session("PeriodosAcreditacion") = New ObservableCollection(Of Clases.PeriodoAcreditacionGrilla)
            End If
            Return DirectCast(Session("PeriodosAcreditacion"), ObservableCollection(Of Clases.PeriodoAcreditacionGrilla))

        End Get
        Set(value As ObservableCollection(Of Clases.PeriodoAcreditacionGrilla))
            Session("PeriodosAcreditacion") = value
        End Set
    End Property

    Private Sub CargarGrilla(pPeticion As Pantallas.Limites.Peticion)
        Dim respuesta As Pantallas.Limites.Respuesta = Pantallas.Limites.Periodos.Ejecutar(pPeticion)

        If respuesta.PeriodosAcreditacion IsNot Nothing AndAlso respuesta.PeriodosAcreditacion.Count > 0 Then
            PeriodosAcreditacion = respuesta.PeriodosAcreditacion.ToObservableCollection()
            dvExportGrid.Visible = True
        Else
            PeriodosAcreditacion = Nothing
            dvExportGrid.Visible = False
        End If


        grid.DataSource = PeriodosAcreditacion
        grid.Selection.UnselectAll()
        grid.DataBind()
        divGrilla.Visible = (respuesta.PeriodosAcreditacion IsNot Nothing AndAlso respuesta.PeriodosAcreditacion.Count > 0)
        updForm.Update()
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init, grid.Init
        grid.DataSource = PeriodosAcreditacion
        grid.DataBind()
    End Sub

    Public Const ConstanteFormatoFecha As String = "dd/MM/yyyy HH:mm:ss"
    Private Function DevolverPeticionBusquedaPeriodosAcreditacion() As Comon.Pantallas.Limites.Peticion
        Dim objPeticion As New Comon.Pantallas.Limites.Peticion

        If ClientesBancosForm IsNot Nothing AndAlso ClientesBancosForm.Count > 0 Then
            For Each oBanco In ClientesBancosForm
                objPeticion.OidBancos.Add(oBanco.Identificador)
            Next
        End If

        If DeviceIDs IsNot Nothing AndAlso DeviceIDs.Count > 0 Then
            For Each oDeviceID In DeviceIDs
                objPeticion.OidDeviceIDs.Add(oDeviceID.Identificador)
            Next
        End If

        If Planificaciones IsNot Nothing AndAlso Planificaciones.Count > 0 Then
            For Each oPlanificacion In Planificaciones
                objPeticion.OidPlanificaciones.Add(oPlanificacion.Identificador)
            Next
        End If

        If Not String.IsNullOrEmpty(txtFechaDesde.Text) Then

            objPeticion.FechaInicio = DateTime.ParseExact(txtFechaDesde.Text, ConstanteFormatoFecha, CultureInfo.CurrentCulture).DataHoraGMT(DelegacionLogada, True)
        End If
        If Not String.IsNullOrEmpty(txtFechaHasta.Text) Then
            objPeticion.FechaFin = DateTime.ParseExact(txtFechaHasta.Text, ConstanteFormatoFecha, CultureInfo.CurrentCulture).DataHoraGMT(DelegacionLogada, True)
        End If

        objPeticion.CodUsuario = LoginUsuario


        objPeticion.CodCultura = CultureInfo.CurrentCulture.Name

        If ddlEstadoPeriodo.Value IsNot Nothing Then
            '
        End If

        objPeticion.OidEstadosPeriodos = GetObtenerOidsEstadosPeriodios()

        Return objPeticion
    End Function

    Private Function GetObtenerOidsEstadosPeriodios() As List(Of String)
        Dim listaRetorno As New List(Of String)
        Try
            Dim listEstado As ASPxListBox = CType(ddlEstadoPeriodo.FindControl("listEstadoPeriodo"), ASPxListBox)
            If listEstado.SelectedItems IsNot Nothing AndAlso listEstado.SelectedItems.Count > 0 Then
                'Recorre los estados seleccionados omitiendo ALL (Todos)
                For Each elemento In listEstado.Items
                    If elemento.selected AndAlso elemento.value <> "TODOS" Then
                        listaRetorno.Add(elemento.value.ToString())
                    End If
                Next
            Else
                'Informamos todos los estados
                For Each elemento In listEstado.Items
                    If elemento.value <> "TODOS" Then
                        listaRetorno.Add(elemento.value.ToString())
                    End If
                Next
            End If

        Catch ex As Exception
            'No tratamos excepción
        End Try

        Return listaRetorno
    End Function

#End Region



    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

#Region "Control DeviceID"
    Private Sub ConfigurarControl_DeviceID()
        Me.ucDeviceIDs.SelecaoMultipla = True
        Me.ucDeviceIDs.DeviceIDHabilitado = True

        If DeviceIDs IsNot Nothing Then
            Me.ucDeviceIDs.DeviceIDs = DeviceIDs
        End If
    End Sub

    Public Property DeviceIDs As ObservableCollection(Of Clases.Maquina)
        Get
            Return ucDeviceIDs.DeviceIDs
        End Get
        Set(value As ObservableCollection(Of Clases.Maquina))
            ucDeviceIDs.DeviceIDs = value
        End Set
    End Property

    Private WithEvents _ucDeviceIDs As ucDeviceID
    Public Property ucDeviceIDs() As ucDeviceID
        Get
            If _ucDeviceIDs Is Nothing Then
                _ucDeviceIDs = LoadControl("~\Controles\Helpers\ucDeviceID.ascx")
                _ucDeviceIDs.ID = "ucDeviceIDs"
                AddHandler _ucDeviceIDs.Erro, AddressOf ErroControles
                phDeviceID.Controls.Add(_ucDeviceIDs)
            End If
            Return _ucDeviceIDs
        End Get
        Set(value As ucDeviceID)
            _ucDeviceIDs = value
        End Set
    End Property

    Private Sub ConfigurarControle_Planificacion()

        Me.ucPlanificaciones.SelecaoMultipla = True
        Me.ucPlanificaciones.PlanificacionHabilitado = True


        If Planificaciones IsNot Nothing Then
            Me.ucPlanificaciones.Planificaciones = Planificaciones
        End If

    End Sub

    Public Property Planificaciones As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Planificacion)
        Get
            Return ucPlanificaciones.Planificaciones
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Planificacion))
            ucPlanificaciones.Planificaciones = value
        End Set
    End Property

    Private WithEvents _ucPlanificaciones As ucPlanificacion
    Public Property ucPlanificaciones() As ucPlanificacion
        Get
            If _ucPlanificaciones Is Nothing Then
                _ucPlanificaciones = LoadControl(ResolveUrl("~\Controles\Helpers\ucPlanificacion.ascx"))
                _ucPlanificaciones.ID = Me.ID & "_ucPlanificaciones"
                AddHandler _ucPlanificaciones.Erro, AddressOf ErroControles
                phPlanificacion.Controls.Add(_ucPlanificaciones)
            End If
            Return _ucPlanificaciones
        End Get
        Set(value As ucPlanificacion)
            _ucPlanificaciones = value
        End Set
    End Property

#End Region

#Region "Control de EstadosPeriodos"
    Private Sub RecuperarEstados(ByRef estadosPeriodos As Dictionary(Of String, String))
        Periodo.RecuperarEstadosPeriodos(estadosPeriodos)
    End Sub

    Public Property EstadosPeriodos() As Dictionary(Of String, String)
        Get
            Return Session("EstadosPeriodos")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("EstadosPeriodos") = value
        End Set
    End Property

    Private Sub ConfigurarControl_EstadosPeriodo(valorInicial As Boolean)

        Try
            Dim listEstadosPeriodos As ASPxListBox = CType(ddlEstadoPeriodo.FindControl("listEstadoPeriodo"), ASPxListBox)

            If EstadosPeriodos IsNot Nothing AndAlso EstadosPeriodos.Count > 0 AndAlso valorInicial Then

                listEstadosPeriodos.DataSource = DevolverEstadosPeriodosTraducidos(EstadosPeriodos)
                listEstadosPeriodos.TextField = "Value"
                listEstadosPeriodos.ValueField = "Key"

                listEstadosPeriodos.DataBind()

                Dim btnEstadosPeriodos As ASPxButton = CType(ddlEstadoPeriodo.FindControl("btnEstadosPeriodos"), ASPxButton)
                btnEstadosPeriodos.Text = MyBase.RecuperarValorDic("btnCerrar")
            Else
                listEstadosPeriodos.Items.Insert(listEstadosPeriodos.Items.Count, New ListEditItem("", ""))
                listEstadosPeriodos.Items.RemoveAt(listEstadosPeriodos.Items.Count - 1)
            End If
            ddlEstadoPeriodo.DataBind()
        Catch ex As Exception
            'No hacer nada
        End Try

    End Sub

    Private Function DevolverEstadosPeriodosTraducidos(ByRef estadosPeriodos As Dictionary(Of String, String)) As Dictionary(Of String, String)
        Dim diccionarioTraducido As New Dictionary(Of String, String)()

        If estadosPeriodos IsNot Nothing AndAlso estadosPeriodos.Count > 0 Then

            diccionarioTraducido.Add("TODOS", MyBase.RecuperarValorDic("lblTodos"))
            For Each elemento In estadosPeriodos
                Select Case elemento.Value
                    Case EstadoPeriodo.Bloqueado.RecuperarValor
                        diccionarioTraducido.Add(elemento.Key, MyBase.RecuperarValorDic("lblBloqueado"))

                    Case EstadoPeriodo.Desbloqueado.RecuperarValor
                        diccionarioTraducido.Add(elemento.Key, MyBase.RecuperarValorDic("lblDesbloqueado"))

                End Select
            Next

        End If

        Return diccionarioTraducido
    End Function

#End Region

#Region "Control Banco"
    Private WithEvents _ucClienteTipoBancoForm As ucCliente
    Public Property ucClienteTipoBancoForm() As ucCliente
        Get
            If _ucClienteTipoBancoForm Is Nothing Then
                _ucClienteTipoBancoForm = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClienteTipoBancoForm.ID = Me.ID & "_ucClientesForm"
                AddHandler _ucClienteTipoBancoForm.Erro, AddressOf ErroControles
                phClientesForm.Controls.Add(_ucClienteTipoBancoForm)
            End If
            Return _ucClienteTipoBancoForm
        End Get
        Set(value As ucCliente)
            _ucClienteTipoBancoForm = value
        End Set
    End Property

    Public Property ClientesBancosForm As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return _ucClienteTipoBancoForm.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClienteTipoBancoForm.Clientes = value
        End Set
    End Property

    Private Sub ucClientesTipoBancoForm_OnControleAtualizado() Handles _ucClienteTipoBancoForm.UpdatedControl
        Try
            If ucClienteTipoBancoForm.Clientes IsNot Nothing Then
                ClientesBancosForm = ucClienteTipoBancoForm.Clientes
            End If
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ucPlanificaciones_OnControleAtualizado() Handles _ucPlanificaciones.UpdatedControl
        Try
            If ucPlanificaciones.Planificaciones IsNot Nothing Then
                Planificaciones = ucPlanificaciones.Planificaciones
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub ConfigurarControl_Banco()

        Me.ucClienteTipoBancoForm.SelecaoMultipla = True
        Me.ucClienteTipoBancoForm.ClienteHabilitado = True
        Me.ucClienteTipoBancoForm.ClienteObrigatorio = False
        Me.ucClienteTipoBancoForm.TipoBanco = True

        If ClientesBancosForm IsNot Nothing Then
            Me.ucClienteTipoBancoForm.Clientes = ClientesBancosForm
        End If

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        CargarGrilla(DevolverPeticionBusquedaPeriodosAcreditacion())

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
    End Sub

    Private Sub btnDesbloquear_Click(sender As Object, e As System.EventArgs) Handles btnDesbloq.Click
        Dim mensaje As String = String.Empty
        Dim otroMensaje As New StringBuilder()
        If Me.HayElementosSeleccionadosEnLaGrilla Then
            Dim respuesta = AccionModificarPeriodos.Ejecutar(DevolverPeticionDesbloquearPeriodo())

            If respuesta.Resultado.Tipo = "0" Then
                mensaje = MyBase.RecuperarValorDic("PeriodosDesbloqueados")
            Else
                otroMensaje.AppendLine(String.Format("{0} - {1}", respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion))
                If respuesta.Periodos IsNot Nothing Then
                    For Each elemento In respuesta.Periodos
                        otroMensaje.AppendLine(String.Format("{0} - {1} ({2})", elemento.CodigoRespuesta, elemento.DescripcionRespuesta, elemento.Oid_Periodo))
                    Next
                End If

            End If
            CargarGrilla(DevolverPeticionBusquedaPeriodosAcreditacion())
        Else
            mensaje = MyBase.RecuperarValorDic("noHayElementoSeleccionados")
        End If

        MyBase.MostraMensagemErro(mensaje, String.Empty)


    End Sub

    Private Function DevolverPeticionDesbloquearPeriodo() As Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos.Peticion
        Dim oPeticion As New Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos.Peticion

        If Me.HayElementosSeleccionadosEnLaGrilla Then
            oPeticion.Accion = Genesis.ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionesModificarPeriodo.Desbloquear
            oPeticion.Configuracion = New Genesis.ContractoServicio.Contractos.Comon.ContratoBase.Configuracion()
            oPeticion.Configuracion.Usuario = LoginUsuario
            oPeticion.Configuracion.LogDetallar = True
            oPeticion.Configuracion.IdentificadorAjeno = String.Empty



            oPeticion.Periodos = New List(Of Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos.Entrada.Periodo)()
            Dim oPeriodo As Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos.Entrada.Periodo

            For Each oid_periodo In grid.GetSelectedFieldValues("OidPeriodo")
                oPeriodo = New Genesis.ContractoServicio.Contractos.Integracion.ModificarPeriodos.Entrada.Periodo

                Dim unPeriodo = PeriodosAcreditacion.Find(Function(x) x.OidPeriodo = oid_periodo)

                oPeriodo.Oid_Periodo = oid_periodo
                oPeriodo.DeviceID = unPeriodo.DeviceId
                oPeriodo.Cod_Pais = unPeriodo.CodigoPais

                oPeticion.Periodos.Add(oPeriodo)
            Next

        End If
        Return oPeticion
    End Function
    Private Function HayElementosSeleccionadosEnLaGrilla()
        Dim retorno As Boolean = False
        Try
            If grid.Selection.Count > 0 Then
                retorno = True
            End If
        Catch ex As Exception
            retorno = False
        End Try

        Return retorno
    End Function
#End Region





End Class