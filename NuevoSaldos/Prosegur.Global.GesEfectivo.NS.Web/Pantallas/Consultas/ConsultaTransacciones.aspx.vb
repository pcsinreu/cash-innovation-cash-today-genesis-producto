Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxEditors
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports DevExpress.Web.ASPxGridView
Imports System.IO
Imports DevExpress.Web.ASPxGridView.Export.Helper
Imports DevExpress.XtraPrinting
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarTransacciones
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.ObtenerPreferencias
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.GuardarPreferencias
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Web.Login
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Preferencia.BorrarPreferenciasAplicacion
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPivotGrid
Imports System.Globalization
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis
Imports Prosegur.Genesis.LogeoEntidades.Log.Movimiento

Public Class ConsultaTransacciones
    Inherits Base

    Private Const TIPO_PLANIFICACION_ACREDITACION As String = "ACCREDITATION"


#Region "[PROPRIEDADES]"

    Public Property Peticion() As RecuperarTransacciones.Peticion

        Get
            Return Session("Peticion")
        End Get
        Set(ByVal value As RecuperarTransacciones.Peticion)
            Session("Peticion") = value
        End Set
    End Property

    Public Property PreferenciasOriginal() As String

        Get
            Return Session("PreferenciasOriginal")
        End Get
        Set(ByVal value As String)
            Session("PreferenciasOriginal") = value
        End Set
    End Property

    Public Property Expand() As Boolean

        Get
            Return Session("Expand")
        End Get
        Set(ByVal value As Boolean)


            TraduzirExpand(value)
            Session("Expand") = value
        End Set
    End Property

    Public Sub TraduzirExpand(value As Boolean)
        If value Then
            ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Retraer")
        Else
            ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Expandir")
        End If
    End Sub

    Public Property Preferencias() As MemoryStream

        Get
            Return Session("Preferencias")
        End Get
        Set(ByVal value As MemoryStream)
            Session("Preferencias") = value
        End Set
    End Property

    Public Property Delegaciones() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Delegaciones")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Delegaciones") = value
        End Set
    End Property

    Public Property Canales() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Canales")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Canales") = value
        End Set
    End Property

    Public Property Tipo_Planificaciones() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Tipo_Planificaciones")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Tipo_Planificaciones") = value
        End Set
    End Property

    Public Property Tipo_Transacciones() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Tipo_Transacciones")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Tipo_Transacciones") = value
        End Set
    End Property

    Public Property Maquinas() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Maquinas")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Maquinas") = value
        End Set
    End Property

    Public Property PuntosServicios() As List(Of PtoServicio)
        Get
            Return Session("Comon_PuntosServicios")
        End Get
        Set(ByVal value As List(Of PtoServicio))
            Session("Comon_PuntosServicios") = value
        End Set
    End Property

    Public Property Planificaciones() As Dictionary(Of String, String)
        Get
            Return Session("Comon_Planificaciones")
        End Get
        Set(ByVal value As Dictionary(Of String, String))
            Session("Comon_Planificaciones") = value
        End Set
    End Property

    Private SelecionarTipoPlanificacion As Boolean

    Public Property Clientes As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl("~\Controles\ucCliente.ascx")
                _ucClientes.ID = "ucCliente"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property


    Private WithEvents _ucCampoExtra As UcCampoExtra
    Public Property ucCampoExtra() As UcCampoExtra
        Get
            If _ucCampoExtra Is Nothing Then
                _ucCampoExtra = LoadControl("~\Controles\ucCampoExtra.ascx")
                _ucCampoExtra.ID = "ucCampoExtra"
                AddHandler _ucCampoExtra.Erro, AddressOf ErroControles

                phCampoExtra.Controls.Add(_ucCampoExtra)
            End If

            Return _ucCampoExtra
        End Get
        Set(ByVal value As UcCampoExtra)
            _ucCampoExtra = value
        End Set
    End Property



    Public Property CampoExtraTermino As Termino
        Get
            Return ucCampoExtra.CampoExtraTermino
        End Get
        Set(value As Termino)
            ucCampoExtra.CampoExtraTermino = value
        End Set
    End Property

    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

    Private WithEvents _ucSectores As UcDatosSector
    Public Property ucSectores() As UcDatosSector
        Get
            If _ucSectores Is Nothing Then
                _ucSectores = LoadControl("~\Controles\ucDatosSector.ascx")
                _ucSectores.ID = "ucDatosSector"
                AddHandler _ucSectores.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSectores)
            End If
            Return _ucSectores
        End Get
        Set(value As UcDatosSector)
            _ucSectores = _ucSectores
        End Set
    End Property



    Public Property BancosComision As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucBancoComision.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucBancoComision.Clientes = value
        End Set
    End Property

    Private WithEvents _ucBancoComision As ucCliente
    Public Property ucBancoComision() As ucCliente
        Get
            If _ucBancoComision Is Nothing Then
                _ucBancoComision = LoadControl("~\Controles\ucCliente.ascx")
                _ucBancoComision.ID = "uc_BancoComision"
                AddHandler _ucBancoComision.Erro, AddressOf ErroControles
                phBancoComision.Controls.Add(_ucBancoComision)
            End If
            Return _ucBancoComision
        End Get
        Set(value As ucCliente)
            _ucBancoComision = value
        End Set
    End Property




    Public Property lstPlanificaciones As ObservableCollection(Of Clases.Planificacion)
        Get
            Return ucPlanificacion.Planificaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Planificacion))
            ucPlanificacion.Planificaciones = value
        End Set
    End Property

    Private WithEvents _ucPlanificacion As ucPlanificacion
    Public Property ucPlanificacion() As ucPlanificacion
        Get
            If _ucPlanificacion Is Nothing Then
                _ucPlanificacion = LoadControl("~\Controles\ucPlanificacion.ascx")
                _ucPlanificacion.ID = "ucPlanificacion"
                AddHandler _ucPlanificacion.Erro, AddressOf ErroControles
                phPlanificacion.Controls.Add(_ucPlanificacion)
            End If
            Return _ucPlanificacion
        End Get
        Set(value As ucPlanificacion)
            _ucPlanificacion = value
        End Set
    End Property

    Public Property Bancos As ObservableCollection(Of Clases.Cliente)
        Get
            Return ucBancos.Clientes
        End Get
        Set(value As ObservableCollection(Of Clases.Cliente))
            ucBancos.Clientes = value
        End Set
    End Property

    Private WithEvents _ucBancos As ucCliente
    Public Property ucBancos() As ucCliente
        Get
            If _ucBancos Is Nothing Then
                _ucBancos = LoadControl("~\Controles\ucCliente.ascx")
                _ucBancos.ID = "ucBancos"
                AddHandler _ucBancos.Erro, AddressOf ErroControles
                phBanco.Controls.Add(_ucBancos)
                _ucBancos.Attributes.Add("selecionados", "0")
            End If
            Return _ucBancos
        End Get
        Set(value As ucCliente)
            _ucBancos = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub TraduzirControles()
        Me.Master.Titulo = Traduzir("079_lblTitulo")


        Me.Titulo_Filtro.InnerHtml = MyBase.RecuperarValorDic("Titulo_Filtro")
        Me.Titulo_FiltroGeneral.InnerHtml = MyBase.RecuperarValorDic("Titulo_FiltroGeneral")
        Me.Titulo_FiltroMaquinas.InnerHtml = MyBase.RecuperarValorDic("Titulo_FiltroMaquinas")
        Me.Titulo_FiltroPlanificacion.InnerHtml = MyBase.RecuperarValorDic("Titulo_FiltroPlanificacion")

        lblTipoTransacciones.Text = MyBase.RecuperarValorDic("lblTipoTransacciones")
        lblModalidad.Text = MyBase.RecuperarValorDic("lblModalidad")
        lblNotificacion.Text = MyBase.RecuperarValorDic("lblNotificacion")
        lblDelegaciones.Text = MyBase.RecuperarValorDic("lblDelegacion")
        lblCanales.Text = MyBase.RecuperarValorDic("lblCanales")
        lblFecha.Text = MyBase.RecuperarValorDic("lblFecha")
        lblA.Text = "à"
        lblFechaGestion.Text = MyBase.RecuperarValorDic("lblFechaGestion")
        ckbShipOut.Text = MyBase.RecuperarValorDic("dvShipOut")
        'lblMaquinas.Text = MyBase.RecuperarValorDic("lblMaquinas")
        'lblPuntos.Text = MyBase.RecuperarValorDic("lblPuntos")
        lblCampoExtraValor.Text = MyBase.RecuperarChavesDic("lblCampoExtraValor")
        lblTipoPlanificacion.Text = MyBase.RecuperarValorDic("lblTipoPlanificacion")
        'lblPlanificaciones.Text = MyBase.RecuperarValorDic("lblPlanificaciones")
        lblAcreditacion.Text = MyBase.RecuperarValorDic("lblAcreditacion")
        ckbImporteInformativo.Text = MyBase.RecuperarValorDic("dvIncluirImportesInformativos")

        gvHoraGestion.Caption = MyBase.RecuperarValorDic("gvHoraGestion")
        gvHoraAcreditacion.Caption = MyBase.RecuperarValorDic("gvHoraAcreditacion")
        gvHoraNotificacion.Caption = MyBase.RecuperarValorDic("gvHoraNotificacion")
        gvHoraCreacion.Caption = MyBase.RecuperarValorDic("gvHoraCreacion")

        gvfechaGestao.Caption = MyBase.RecuperarValorDic("gvFechaGestion")
        gvFechaAcreditacion.Caption = MyBase.RecuperarValorDic("gvFechaAcreditacion")
        gvFechaNotificacion.Caption = MyBase.RecuperarValorDic("gvFechaNotificacion")
        gvFechaCreacion.Caption = MyBase.RecuperarValorDic("gvFechaCreacion")

        gvCodExternoBase.Caption = MyBase.RecuperarValorDic("gvCodExternoBase")
        gvCodExterno.Caption = MyBase.RecuperarValorDic("gvCodExterno")
        gvMaquina.Caption = MyBase.RecuperarValorDic("gvMaquina")
        gvPuntoServicio.Caption = MyBase.RecuperarValorDic("gvPuntoServicio")
        gvCliente.Caption = MyBase.RecuperarValorDic("gvCliente")
        gvDelegacion.Caption = MyBase.RecuperarValorDic("gvDelegacion")
        gvCanal.Caption = MyBase.RecuperarValorDic("gvCanal")
        gvFormulario.Caption = MyBase.RecuperarValorDic("gvFormulario")
        gvDivisa.Caption = MyBase.RecuperarValorDic("gvDivisa")
        gvImporteContado.Caption = MyBase.RecuperarValorDic("gvImporte")
        gvImporteInformativo.Caption = MyBase.RecuperarValorDic("gvInformativo")


        gvCodResponsable.Caption = MyBase.RecuperarValorDic("gvCodResponsable")
        gvNombreResponsable.Caption = MyBase.RecuperarValorDic("gvNombreResponsable")
        gvReceiptNumber.Caption = MyBase.RecuperarValorDic("gvReceiptNumber")
        gvBarcode.Caption = MyBase.RecuperarValorDic("gvBarcode")
        gvTipoTransaccion.Caption = MyBase.RecuperarValorDic("gvTipoTransacion")

        gvModalidad.Caption = MyBase.RecuperarValorDic("gvModalidad")
        gvNotificacion.Caption = MyBase.RecuperarValorDic("gvNotificacion")
        gvCampoExtraValor.Caption = MyBase.RecuperarValorDic("gvCampoExtraValor")
        gvCantidad.Caption = MyBase.RecuperarValorDic("gvCantidad")
        gvBaseDeviceId.Caption = MyBase.RecuperarValorDic("gvBaseDeviceId")


        Dim a = ASPxPopupMenu1.Items

        AssignNavigationUrl(ASPxPopupMenu1.RootItem)

        Me.btnBuscar.Text = MyBase.RecuperarValorDic("btnBuscar")

        Me.btnLimpar.Text = MyBase.RecuperarValorDic("btnLimpar")

        hfTitleCustomization.Value = MyBase.RecuperarValorDic("hfTitleCustomization")
        'grid.ResetViewStateStoringFlag()
        lblSemDatos.InnerText = Traduzir("lblSemRegistro")

        'If Expand Then
        '    ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Expandir")

        'Else
        '    ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Retraer")
        'End If
    End Sub

    Private Sub AssignNavigationUrl(item As DevExpress.Web.ASPxMenu.MenuItem)
        item.Text = MyBase.RecuperarValorDic(item.Name)

        For Each childItem As DevExpress.Web.ASPxMenu.MenuItem In item.Items
            AssignNavigationUrl(childItem)
        Next

    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaDesde.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaHasta.ClientID, "True")
        script &= String.Format("AbrirCalendario('{0}','{1}');", txtFechaGestion.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub

    Protected Overrides Sub Inicializar()

        MyBase.Inicializar()
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Consulta Transacciones")
        SelecionarTipoPlanificacion = False
        If ucBancos IsNot Nothing AndAlso ucBancos.Clientes IsNot Nothing AndAlso ucBancos.Clientes.Count = 0 Then
            SelecionarTipoPlanificacion = True
        End If
        If Not IsPostBack Then
            isSessionExpired()

            Delegaciones = Nothing
            Canales = Nothing
            Tipo_Planificaciones = Nothing
            PreferenciasOriginal = String.Empty

            Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Pantallas.Transacciones.RecuperarInformaciones(Delegaciones, Canales, Tipo_Planificaciones, Tipo_Transacciones, Parametros.Permisos.Usuario.Login)
            grid.Visible = False
            dvExportGrid.Visible = False

            hfAccordion1.Value = 0
            hfAccordion2.Value = "[0,1,1]"
        End If

        ConfigurarControles()
        dvSemDatos.Visible = False

    End Sub

    Private Sub CarregarPreferencias()
        If String.IsNullOrWhiteSpace(PreferenciasOriginal) Then
            PreferenciasOriginal = grid.SaveLayoutToString()
        End If

        Dim respuesta As New ObtenerPreferenciasRespuesta
        Dim proxy As New ProxyPreferencias()

        '  peticion
        Dim peticion As New ObtenerPreferenciasPeticion With {
                                    .codigoAplicacion = Enumeradores.CodigoAplicacion.SaldosNuevo,
                                    .CodigoUsuario = Parametros.Permisos.Usuario.Login,
                                    .CodigoFuncionalidad = Me.CodFuncionalidad
                                }

        respuesta = proxy.ObtenerPreferencias(peticion)
        If respuesta IsNot Nothing And respuesta.Preferencias IsNot Nothing Then

            Dim preferenciaGrid = respuesta.Preferencias.FirstOrDefault(Function(x) x.CodigoComponente.Equals("pvGrid"))

            If preferenciaGrid IsNot Nothing Then

                Dim prefer As String = preferenciaGrid.Valor

                Dim valor = DirectCast(preferenciaGrid.ValorBinario, Byte())


                Preferencias = New MemoryStream(valor)

                grid.LoadLayoutFromStream(Preferencias)


            End If

        End If




    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Expand = True
        'ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Retraer")

        CarregarPreferencias()
        If Parametros.Permisos.Usuario.Login Is Nothing Then
            Return
        End If



        PoblarPeticion()
        Dim mensagem As String = ValidarPeticion(Peticion)

        If Not String.IsNullOrEmpty(mensagem) Then

            MyBase.MostraMensagemErro(mensagem)

        Else

            Dim _NombreRecurso As String = Prosegur.Genesis.LogeoEntidades.Log.Movimiento.Recurso.SALDOSConsultaTransacciones
            Dim pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDefault("")
            Dim _CodPais As String = pais.Codigo
            Dim _IdentificadorLlamada As String = String.Empty
            Dim _ObjJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(Peticion)

            Dim resp = New RecuperarTransacciones.Respuesta
            Try
                Logeo.Log.Movimiento.Logger.GenerarIdentificador(_CodPais, _NombreRecurso, _IdentificadorLlamada)
                If _IdentificadorLlamada IsNot Nothing Then
                    Logeo.Log.Movimiento.Logger.IniciaLlamada(_IdentificadorLlamada, _NombreRecurso, Comon.Util.VersionCompleta, _ObjJson, _CodPais, _ObjJson.GetHashCode)
                End If
                Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarTransacciones(Peticion, Parametros.Permisos.Usuario.Login, resp)

            Catch ex As Exception
                Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada,
                                                      "Prosegur.Global.GesEfectivo.NuevoSaldos.Web",
                                                      Comon.Util.VersionCompleta.ToString(),
                                                      $"Excepcion: {ex.Message} InnerException: {ex.InnerException}", "")
            End Try

            grid.Visible = True
            dvExportGrid.Visible = True

            Session("_Transacciones") = resp.Transacciones
            If (resp.Transacciones IsNot Nothing AndAlso resp.Transacciones.Count > 0) Then

                grid.Visible = True
                dvExportGrid.Visible = True

                dvSemDatos.Visible = False
                grid.DataSource = Transacciones()
                'Para mostrar o no el campo CampoExtra
                Try
                    Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)

                    If listBoxTipoTransacciones.SelectedItems.Count = 1 Then
                        If listBoxTipoTransacciones.SelectedValues(0).Equals(TIPO_PLANIFICACION_ACREDITACION) Then
                            grid.Fields("gvCampoExtraValor").Visible = True
                            gvCampoExtraValor.Caption = ucCampoExtra.CampoExtraTermino.Codigo
                        Else
                            grid.Fields("gvCampoExtraValor").Visible = False
                        End If
                    Else
                        grid.Fields("gvCampoExtraValor").Visible = False
                    End If
                Catch ex As Exception
                    Logeo.Log.Movimiento.Logger.AgregaDetalle(_IdentificadorLlamada,
                                                       "Prosegur.Global.GesEfectivo.NuevoSaldos.Web",
                                                       Comon.Util.VersionCompleta.ToString(),
                                                       $"Excepcion: {ex.Message} InnerException: {ex.InnerException}", "")
                End Try

                grid.Fields("FechaGestion").SortMode = PivotSortMode.Custom
                grid.Fields("FechaAcreditacion").SortMode = PivotSortMode.Custom
                grid.Fields("FechaNotificacion").SortMode = PivotSortMode.Custom
                grid.Fields("FechaCreacion").SortMode = PivotSortMode.Custom

                grid.DataBind()
            Else
                dvSemDatos.Visible = True
                grid.Visible = False
                dvExportGrid.Visible = False
            End If
            If _IdentificadorLlamada IsNot Nothing Then
                Dim obj As Object = New With {.TotalRegistros = resp.Transacciones?.Count, .Codigo = resp.Resultado.Codigo, .Descripcion = resp.Resultado.Descripcion}
                Dim _RespJson = Newtonsoft.Json.JsonConvert.SerializeObject(obj)
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(_IdentificadorLlamada, _RespJson, resp.Resultado.Codigo, resp.Resultado.Descripcion, _RespJson.GetHashCode)
            End If
        End If
        ConfiguraGrid()
    End Sub

    Private Sub pivotGridControl1_CustomFieldSort(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomFieldSortEventArgs) Handles grid.CustomFieldSort

        Try
            If (e.Field.FieldName = "FechaGestion" OrElse e.Field.FieldName = "FechaAcreditacion" OrElse e.Field.FieldName = "FechaNotificacion" OrElse e.Field.FieldName = "FechaCreacion") Then

                Dim dia1 As String
                Dim mes1 As String
                Dim anio1 As String
                Dim dia2 As String
                Dim mes2 As String
                Dim anio2 As String

                dia1 = e.Value1.ToString.Split("/")(0)
                mes1 = e.Value1.ToString.Split("/")(1)
                anio1 = e.Value1.ToString.Split("/")(2)

                dia2 = e.Value2.ToString.Split("/")(0)
                mes2 = e.Value2.ToString.Split("/")(1)
                anio2 = e.Value2.ToString.Split("/")(2)

                Dim fecha1 As String = anio1 + mes1 + dia1
                Dim fecha2 As String = anio2 + mes2 + dia2

                e.Result = Comparer.Default.Compare(fecha1, fecha2)
                e.Handled = True
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Function Transacciones() As List(Of RecuperarTransacciones.Transaccion)
        Return DirectCast(Session("_Transacciones"), List(Of RecuperarTransacciones.Transaccion))
    End Function

    Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grid = TryCast(sender, ASPxGridView)
        grid.ForceDataRowType(GetType(RecuperarTransacciones.Transaccion))
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        hfAccordion1.Value = 0
        hfAccordion2.Value = "[0,1,1]"
        ckbShipOut.Checked = False
        ckbImporteInformativo.Checked = False

        Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)
        listBoxTipoTransacciones.SelectAll()
        listBoxTipoTransacciones.DataBind()
        dropDownTipoTransacciones.Text = GetTextList(listBoxTipoTransacciones.SelectedItems)


        Dim listBoxModalidad As ASPxListBox = CType(dropDownModalidad.FindControl("listBoxModalidad"), ASPxListBox)
        listBoxModalidad.SelectAll()
        listBoxModalidad.DataBind()
        dropDownModalidad.Text = GetTextList(listBoxModalidad.SelectedItems)


        Dim listBoxNotificacion As ASPxListBox = CType(dropDownNotificacion.FindControl("listBoxNotificacion"), ASPxListBox)

        listBoxNotificacion.SelectAll()
        listBoxNotificacion.DataBind()
        dropDownNotificacion.Text = GetTextList(listBoxNotificacion.SelectedItems)


        Dim listBoxAcreditacion As ASPxListBox = CType(dropDownAcreditacion.FindControl("listBoxAcreditacion"), ASPxListBox)

        listBoxAcreditacion.SelectAll()
        listBoxAcreditacion.DataBind()
        dropDownAcreditacion.Text = GetTextList(listBoxAcreditacion.SelectedItems)



        ConfigurarDelegaciones(True)
        ConfigurarCanales(True)
        ConfigurarFechas(True)

        ucClientes.Clientes.Clear()
        ucClientes.ucCliente.LimparCampos()
        ucClientes.ucCliente.LimparViewState()

        ucClientes.ucSubCliente.LimparCampos()
        ucClientes.ucSubCliente.LimparViewState()

        ucClientes.ucPtoServicio.LimparCampos()
        ucClientes.ucPtoServicio.LimparViewState()

        ucClientes.ucCliente.LimparCampos()
        ucClientes.ucCliente.LimparViewState()

        ucClientes.ucCliente_OnControleAtualizado()

        ucSectores.Sectores.Clear()
        ucSectores.ucMaquina.LimparCampos()
        ucSectores.ucMaquina.LimparViewState()


        ucPlanificacion.Planificaciones.Clear()
        ucPlanificacion.ucPlanificaciones.LimparCampos()
        ucPlanificacion.ucPlanificaciones.LimparViewState()


        ucBancos.Clientes.Clear()
        ucBancos.ucCliente.LimparCampos()
        ucBancos.ucCliente.LimparViewState()


        ucBancos.ucSubCliente.LimparCampos()
        ucBancos.ucSubCliente.LimparViewState()
        ucBancos.ucSubCliente.Visible = False

        ucBancos.ucPtoServicio.LimparCampos()
        ucBancos.ucPtoServicio.LimparViewState()
        ucBancos.ucPtoServicio.Visible = False


        ucBancoComision.Clientes.Clear()
        ucBancoComision.ucCliente.LimparCampos()
        ucBancoComision.ucCliente.LimparViewState()

        ucCampoExtra.ucCampoExtra.LimparCampos()
        ucCampoExtra.ucCampoExtra.LimparViewState()
        'HacerVisibleCampoExtra(False)


        ConfigurarControles()

        dvSemDatos.Visible = False
        grid.DataSource = Nothing
        grid.DataBind()


        Dim listBoxTipoPlanificacion As ASPxListBox = CType(dropDownTipoPlanificacion.FindControl("listBoxTipoPlanificacion"), ASPxListBox)
        listBoxTipoPlanificacion.UnselectAll()

        grid.Visible = False
        dvExportGrid.Visible = False

    End Sub

#End Region

#Region "[EVENTOS]"


    Public Sub ucCampoExtra_OnControleAtualizado() Handles _ucCampoExtra.UpdatedControl
        Try
            If ucCampoExtra.CampoExtraTermino IsNot Nothing AndAlso String.IsNullOrEmpty(ucCampoExtra.CampoExtraTermino.Codigo) Then
                txtCampoExtra.Enabled = False
                txtCampoExtra.Text = String.Empty
            Else
                txtCampoExtra.Enabled = True
            End If
            ConfigurarControles()
            upCampoExtra2.Update()
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try
            If ucSectores.Sectores IsNot Nothing Then
                Sectores = ucSectores.Sectores
                ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
                ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Public Sub ucBancoComision_OnControleAtualizado() Handles _ucBancoComision.UpdatedControl
        Try
            If ucBancoComision.Clientes IsNot Nothing Then
                BancosComision = ucBancoComision.Clientes
                ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub



    Public Sub ucPlanificacion_OnControleAtualizado() Handles _ucPlanificacion.UpdatedControl
        Try
            If ucPlanificacion.Planificaciones IsNot Nothing Then
                lstPlanificaciones = ucPlanificacion.Planificaciones
                'ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub



    Public Sub ucBancos_OnControleAtualizado() Handles _ucBancos.UpdatedControl
        Try
            If ucBancos.Clientes IsNot Nothing Then
                Bancos = ucBancos.Clientes
                ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Sub listBoxMaquinas_ValueChanged(sender As Object, e As System.EventArgs)
        ConfigurarControles()
    End Sub

    Protected Sub listBoxTipoPlanificacion_ValueChanged(sender As Object, e As System.EventArgs)
        ConfigurarControles()
    End Sub

#End Region

#Region "[METODOS]"

    Public Sub ConfigurarControles()


        ConfigurarTipoTransacciones(Not IsPostBack)
        ConfigurarModalidad(Not IsPostBack)
        ConfigurarNotificacion(Not IsPostBack)
        ConfigurarDelegaciones(Not IsPostBack)
        ConfigurarCanales(Not IsPostBack)
        ConfigurarFechas(Not IsPostBack)
        ConfigurarAcreditado(Not IsPostBack)
        ConfigurarBanco()
        ConfigurarBancoComision()
        ConfigurarSectores()
        ConfigurarCliente()
        ConfigurarPlanificacion()
        ConfigurarTipoPlanificacion(Not IsPostBack)
        ConfigurarCampoExtra()

        PoblarPeticion()

        If Planificaciones IsNot Nothing Then
            Planificaciones.Clear()
        End If
        If Maquinas IsNot Nothing Then
            Maquinas.Clear()
        End If

        If PuntosServicios IsNot Nothing Then
            PuntosServicios.Clear()
        End If



        Genesis.LogicaNegocio.GenesisSaldos.Pantallas.Transacciones.RecuperarInformacionesDinamico(Maquinas, PuntosServicios, Planificaciones, Peticion, Parametros.Permisos.Usuario.Login)



        If Not ckbShipOut.Checked Then
            dvFechaGestion.Style.Item("display") = "block"
            dvFecha.Style.Item("display") = "block"
            dvFechaDesde.Style.Item("display") = "block"
            dvFechaHasta.Style.Item("display") = "block"
            dvFechaGestion.Style.Item("display") = "none"
        Else
            dvFechaGestion.Style.Item("display") = "none"
            dvFecha.Style.Item("display") = "none"
            dvFechaDesde.Style.Item("display") = "none"
            dvFechaHasta.Style.Item("display") = "none"
            dvFechaGestion.Style.Item("display") = "block"
        End If
    End Sub

    Private Sub ConfigurarCampoExtra()
        If CampoExtraTermino IsNot Nothing Then
            CampoExtraTermino.Valor = txtCampoExtra.Text
            Me.ucCampoExtra.CampoExtraTermino = CampoExtraTermino
        End If
    End Sub

    Public Function GetTextList(lista As SelectedItemCollection) As String

        If lista.Count = 0 Then
            Return ""
        End If
        Dim result As String = String.Empty

        For i = 0 To lista.Count - 1
            If Not String.IsNullOrWhiteSpace(lista(i).Value.ToString()) AndAlso "TODOS" <> lista(i).Value.ToString().ToUpper() Then
                result += lista(i).Text + ";"
            End If
        Next
        If result.Count > 0 Then
            result = result.Substring(0, result.Count - 1)
        End If
        Return result
    End Function

    Public Function GetTextList(lista As ListEditItemCollection) As String
        If lista.Count = 0 Then
            Return ""
        End If
        Dim result As String = String.Empty

        For i = 0 To lista.Count - 1
            If Not String.IsNullOrWhiteSpace(lista(i).Value.ToString()) AndAlso "TODOS" <> lista(i).Value.ToString().ToUpper() Then
                result += lista(i).Text + ";"
            End If
        Next

        If result.Count > 0 Then
            result = result.Substring(0, result.Count - 1)
        End If
        Return result

    End Function

    Public Sub ConfigurarTipoTransacciones(valoriniciales As Boolean)

        Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)
        If valoriniciales Then

            listBoxTipoTransacciones.DataSource = Tipo_Transacciones
            listBoxTipoTransacciones.TextField = "Value"
            listBoxTipoTransacciones.ValueField = "Key"
            listBoxTipoTransacciones.DataBind()
            listBoxTipoTransacciones.SelectAll()
            dropDownTipoTransacciones.Text = GetTextList(listBoxTipoTransacciones.SelectedItems)

            Dim btnTipoTransacciones As ASPxButton = CType(dropDownTipoTransacciones.FindControl("btnTipoTransacciones"), ASPxButton)
            btnTipoTransacciones.Text = MyBase.RecuperarValorDic("btnCerrar")
        Else


            listBoxTipoTransacciones.Items.Insert(listBoxTipoTransacciones.Items.Count, New ListEditItem("-1", "-1"))
            listBoxTipoTransacciones.Items.RemoveAt(listBoxTipoTransacciones.Items.Count - 1)

        End If
    End Sub

    Public Sub ConfigurarModalidad(valoriniciales As Boolean)

        Dim listBoxModalidad As ASPxListBox = CType(dropDownModalidad.FindControl("listBoxModalidad"), ASPxListBox)
        If valoriniciales Then
            listBoxModalidad.Items.Clear()
            listBoxModalidad.Items.Insert(0, New ListEditItem("Todos", ""))
            listBoxModalidad.Items.Insert(1, New ListEditItem("Con Fecha Valor", "1")) 'Modalidad_Fecha_Valor = "Con Fecha Valor"
            listBoxModalidad.Items.Insert(2, New ListEditItem("Sin Fecha Valor", "0")) 'Modalidad_Sin_Fecha_Valor = "Sin Fecha Valor"


            listBoxModalidad.SelectAll()

            listBoxModalidad.DataBind()

            dropDownModalidad.Text = GetTextList(listBoxModalidad.SelectedItems)

            Dim btnModalidad As ASPxButton = CType(dropDownModalidad.FindControl("btnModalidad"), ASPxButton)
            btnModalidad.Text = MyBase.RecuperarValorDic("btnCerrar")

        Else
            listBoxModalidad.Items.Insert(3, New ListEditItem("", ""))
            listBoxModalidad.Items.RemoveAt(3)
        End If
    End Sub

    Public Sub ConfigurarNotificacion(valoriniciales As Boolean)

        Dim listBoxNotificacion As ASPxListBox = CType(dropDownNotificacion.FindControl("listBoxNotificacion"), ASPxListBox)

        If valoriniciales Then
            listBoxNotificacion.Items.Clear()
            listBoxNotificacion.Items.Insert(0, New ListEditItem("Todos", ""))
            listBoxNotificacion.Items.Insert(1, New ListEditItem(MyBase.RecuperarValorDic("Notificado"), "1"))
            listBoxNotificacion.Items.Insert(2, New ListEditItem(MyBase.RecuperarValorDic("No_Notificado"), "0"))
            listBoxNotificacion.SelectAll()
        Else
            listBoxNotificacion.Items.Insert(3, New ListEditItem("", ""))
            listBoxNotificacion.Items.RemoveAt(3)
        End If

        listBoxNotificacion.DataBind()


        dropDownNotificacion.Text = GetTextList(listBoxNotificacion.SelectedItems)

        Dim btnNotificacion As ASPxButton = CType(dropDownNotificacion.FindControl("btnNotificacion"), ASPxButton)
        btnNotificacion.Text = MyBase.RecuperarValorDic("btnCerrar")

    End Sub

    Public Sub ConfigurarAcreditado(valoriniciales As Boolean)

        Dim listBoxAcreditacion As ASPxListBox = CType(dropDownAcreditacion.FindControl("listBoxAcreditacion"), ASPxListBox)

        If valoriniciales Then
            listBoxAcreditacion.Items.Clear()
            listBoxAcreditacion.Items.Insert(0, New ListEditItem("Todos", ""))
            listBoxAcreditacion.Items.Insert(1, New ListEditItem(MyBase.RecuperarValorDic("ConAcreditacion"), "1"))
            listBoxAcreditacion.Items.Insert(2, New ListEditItem(MyBase.RecuperarValorDic("SinAcreditacion"), "0"))
            listBoxAcreditacion.SelectAll()
        Else
            listBoxAcreditacion.Items.Insert(3, New ListEditItem("", ""))
            listBoxAcreditacion.Items.RemoveAt(3)
        End If

        listBoxAcreditacion.DataBind()


        dropDownAcreditacion.Text = GetTextList(listBoxAcreditacion.SelectedItems)

        Dim btnAcreditacion As ASPxButton = CType(dropDownAcreditacion.FindControl("btnAcreditacion"), ASPxButton)
        btnAcreditacion.Text = MyBase.RecuperarValorDic("btnCerrar")

    End Sub

    Public Sub ConfigurarDelegaciones(valoriniciales As Boolean)

        If Delegaciones IsNot Nothing AndAlso Delegaciones.Count > 0 Then

            Dim listBoxDelegaciones As ASPxListBox = CType(dropDownDelegaciones.FindControl("listBoxDelegaciones"), ASPxListBox)

            listBoxDelegaciones.DataSource = Delegaciones
            listBoxDelegaciones.TextField = "Value"
            listBoxDelegaciones.ValueField = "Key"
            listBoxDelegaciones.DataBind()

            If valoriniciales AndAlso
                InformacionUsuario.DelegacionSeleccionada IsNot Nothing AndAlso
                InformacionUsuario.DelegacionSeleccionada.Identificador IsNot Nothing Then

                listBoxDelegaciones.Items.FindByValue(InformacionUsuario.DelegacionSeleccionada.Identificador).Selected = True
                dropDownDelegaciones.Text = Delegaciones(InformacionUsuario.DelegacionSeleccionada.Identificador)
            Else
                dropDownDelegaciones.Text = GetTextList(listBoxDelegaciones.SelectedItems)
            End If

            Dim btnDelegaciones As ASPxButton = CType(dropDownDelegaciones.FindControl("btnDelegaciones"), ASPxButton)
            btnDelegaciones.Text = MyBase.RecuperarValorDic("btnCerrar")
        Else
            dropDownDelegaciones.Enabled = False
        End If


        If valoriniciales AndAlso
                InformacionUsuario.DelegacionSeleccionada IsNot Nothing AndAlso
                InformacionUsuario.DelegacionSeleccionada.Identificador IsNot Nothing Then

            ' ucClientes.Delegaciones.Add(InformacionUsuario.DelegacionSeleccionada.Identificador)
        End If

    End Sub

    Public Sub ConfigurarCanales(valoriniciales As Boolean)

        If Canales IsNot Nothing AndAlso Canales.Count > 0 Then

            Dim listBoxCanales As ASPxListBox = CType(dropDownCanales.FindControl("listBoxCanales"), ASPxListBox)

            listBoxCanales.DataSource = Canales
            listBoxCanales.TextField = "Value"
            listBoxCanales.ValueField = "Key"
            listBoxCanales.DataBind()

            If valoriniciales Then

                listBoxCanales.SelectAll()
                dropDownCanales.Text = GetTextList(listBoxCanales.Items)
            Else
                dropDownCanales.Text = GetTextList(listBoxCanales.SelectedItems)

            End If

            Dim btnCanales As ASPxButton = CType(dropDownCanales.FindControl("btnCanales"), ASPxButton)
            btnCanales.Text = MyBase.RecuperarValorDic("btnCerrar")

        Else
            dropDownCanales.Enabled = False
        End If

    End Sub

    Public Sub ConfigurarFechas(valoriniciales As Boolean)

        comboFecha.Items.Clear()
        comboFecha.Items.Add(New ListEditItem(MyBase.RecuperarValorDic("Tipo_Fecha_Gestion"), "1"))
        comboFecha.Items.Add(New ListEditItem(MyBase.RecuperarValorDic("Tipo_Fecha_Creacion"), "2"))
        comboFecha.Items.Add(New ListEditItem(MyBase.RecuperarValorDic("Tipo_Fecha_Credito"), "3"))
        comboFecha.Items.Add(New ListEditItem(MyBase.RecuperarValorDic("Tipo_Fecha_Notificado"), "4"))


        If valoriniciales AndAlso
            InformacionUsuario IsNot Nothing AndAlso
            InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then

            txtFechaDesde.Text = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).AddDays(-1).ToString("dd/MM/yyyy HH:mm:ss")
            txtFechaHasta.Text = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).ToString("dd/MM/yyyy HH:mm:ss")
            txtFechaGestion.Text = Prosegur.Genesis.LogicaNegocio.Util.GetDateTime(Base.InformacionUsuario.DelegacionSeleccionada).ToString("dd/MM/yyyy HH:mm:ss")
            comboFecha.SelectedIndex = 0


        End If


    End Sub

    Public Sub ConfigurarTipoPlanificacion(valoriniciales As Boolean)

        dropDownTipoPlanificacion.Enabled = True

        If Tipo_Planificaciones IsNot Nothing AndAlso Tipo_Planificaciones.Count > 0 Then

            Dim listBoxTipoPlanificacion As ASPxListBox = CType(dropDownTipoPlanificacion.FindControl("listBoxTipoPlanificacion"), ASPxListBox)

            listBoxTipoPlanificacion.DataSource = Tipo_Planificaciones
            listBoxTipoPlanificacion.TextField = "Value"
            listBoxTipoPlanificacion.ValueField = "Key"
            listBoxTipoPlanificacion.DataBind()

            If valoriniciales Then
                listBoxTipoPlanificacion.UnselectAll()
            Else
                dropDownTipoPlanificacion.Text = GetTextList(listBoxTipoPlanificacion.SelectedItems)

            End If



            Dim btnTipoPlanificacion As ASPxButton = CType(dropDownTipoPlanificacion.FindControl("btnTipoPlanificacion"), ASPxButton)
            btnTipoPlanificacion.Text = MyBase.RecuperarValorDic("btnCerrar")
        End If
    End Sub

    Protected Sub ConfigurarCliente()

        Me.ucClientes.SelecaoMultipla = True
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.PtoServicioHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Protected Sub ConfigurarSectores()

        Me.ucSectores.SelecaoMultipla = True
        Me.ucSectores.SectorHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If

    End Sub

    Protected Sub ConfigurarPlanificacion()

        Me.ucPlanificacion.SelecaoMultipla = True
        Me.ucPlanificacion.ClienteHabilitado = True

        If lstPlanificaciones IsNot Nothing Then
            Me.ucPlanificacion.Planificaciones = lstPlanificaciones
        End If

    End Sub


    Protected Sub ConfigurarBanco()

        Me.ucBancos.SelecaoMultipla = True
        Me.ucBancos.ClienteHabilitado = True
        Me.ucBancos.SubClienteHabilitado = True
        Me.ucBancos.PtoServicioHabilitado = True

        Me.ucBancos.EsBancoCapital = True

        Dim listBoxTipoPlanificacion As ASPxListBox = CType(dropDownTipoPlanificacion.FindControl("listBoxTipoPlanificacion"), ASPxListBox)

        If Me.ucBancos.Attributes.Item("selecionados") = "0" And Bancos.Count > 0 Then
            Me.ucBancos.Attributes.Item("selecionados") = Bancos.Count.ToString()

        ElseIf Bancos.Count = 0 Then

            Me.ucBancos.Attributes.Item("selecionados") = 0
        End If



        If Bancos IsNot Nothing Then
            Me.ucBancos.Clientes = Bancos
        End If

    End Sub


    Protected Sub ConfigurarBancoComision()

        Me.ucBancoComision.SelecaoMultipla = True
        Me.ucBancoComision.ClienteHabilitado = True
        Me.ucBancoComision.SubClienteHabilitado = False
        Me.ucBancoComision.PtoServicioHabilitado = False

        Me.ucBancoComision.NoExhibirSubCliente = True
        Me.ucBancoComision.NoExhibirPtoServicio = True

        Me.ucBancoComision.EsBancoComision = True


        If BancosComision IsNot Nothing Then
            Me.ucBancoComision.Clientes = BancosComision
        End If

    End Sub

    Public Function ValidarPeticion(peticion As RecuperarTransacciones.Peticion) As String

        Dim sbMensagens As New StringBuilder

        Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)
        If listBoxTipoTransacciones.SelectedValues Is Nothing OrElse listBoxTipoTransacciones.SelectedValues.Count = 0 Then
            sbMensagens.Append(String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), MyBase.RecuperarValorDic("lblTipoTransacciones")))
            'erro
        End If


        Dim listBoxModalidad As ASPxListBox = CType(dropDownModalidad.FindControl("listBoxModalidad"), ASPxListBox)
        If listBoxModalidad.SelectedValues Is Nothing OrElse listBoxModalidad.SelectedValues.Count = 0 Then
            sbMensagens.Append(String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), MyBase.RecuperarValorDic("lblModalidad")))
            'erro
        End If

        Dim listBoxNotificacion As ASPxListBox = CType(dropDownNotificacion.FindControl("listBoxNotificacion"), ASPxListBox)
        If listBoxNotificacion.SelectedValues Is Nothing OrElse listBoxNotificacion.SelectedValues.Count = 0 Then
            sbMensagens.Append(String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), MyBase.RecuperarValorDic("lblNotificacion")))
            'erro
        End If


        If ckbShipOut.Checked Then

            If peticion.FechaGestion Is Nothing OrElse peticion.FechaGestion = DateTime.MinValue Then
                sbMensagens.Append(String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), MyBase.RecuperarValorDic("lblFechaGestion")))
                'erro
            End If
        Else
            If peticion.FechaDesde Is Nothing OrElse peticion.FechaDesde = DateTime.MinValue OrElse peticion.FechaHasta Is Nothing Or peticion.FechaHasta = DateTime.MinValue Then
                sbMensagens.Append(String.Format(MyBase.RecuperarValorDic("filtroObligatorios"), MyBase.RecuperarValorDic("lblFecha")))

                'erro
            End If
        End If


        Return sbMensagens.ToString()

    End Function

    Protected Sub PoblarPeticion()

        'If Peticion Is Nothing Then Peticion = New RecuperarTransacciones.Peticion
        Peticion = New RecuperarTransacciones.Peticion

        ' Tipos de Transacciones
        Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)
        If listBoxTipoTransacciones IsNot Nothing AndAlso listBoxTipoTransacciones.SelectedValues IsNot Nothing AndAlso listBoxTipoTransacciones.SelectedValues.Count > 0 Then
            If Peticion.TipoTransacciones Is Nothing Then Peticion.TipoTransacciones = New List(Of String)
            For Each Tipo In listBoxTipoTransacciones.SelectedValues
                If Not Tipo.ToString().ToUpper.Equals("TODOS") Then
                    Peticion.TipoTransacciones.Add(Tipo)
                End If

            Next
        End If

        ' Modalidad
        Dim listBoxModalidad As ASPxListBox = CType(dropDownModalidad.FindControl("listBoxModalidad"), ASPxListBox)
        Peticion.Modalidad = String.Empty
        If listBoxModalidad IsNot Nothing AndAlso listBoxModalidad.SelectedValues IsNot Nothing AndAlso listBoxModalidad.SelectedValues.Count > 0 Then
            If listBoxModalidad.SelectedValues.Count = 0 OrElse listBoxModalidad.SelectedValues.Count = 2 Then
                Peticion.Modalidad = String.Empty
            ElseIf listBoxModalidad.SelectedValues(0).ToString() = "1" Then
                Peticion.Modalidad = "1"
            ElseIf listBoxModalidad.SelectedValues(0).ToString() = "0" Then
                Peticion.Modalidad = "0"
            End If

        End If

        ' Notificación
        Dim listBoxNotificacion As ASPxListBox = CType(dropDownNotificacion.FindControl("listBoxNotificacion"), ASPxListBox)
        Peticion.Notificacion = String.Empty
        If listBoxNotificacion IsNot Nothing AndAlso listBoxNotificacion.SelectedValues IsNot Nothing AndAlso listBoxNotificacion.SelectedValues.Count > 0 Then
            If listBoxNotificacion.SelectedValues.Count = 0 OrElse listBoxNotificacion.SelectedValues.Count = 2 Then
                Peticion.Notificacion = String.Empty
            ElseIf listBoxNotificacion.SelectedValues(0).ToString() = "1" Then
                Peticion.Notificacion = "1"
            ElseIf listBoxNotificacion.SelectedValues(0).ToString() = "0" Then
                Peticion.Notificacion = "0"
            End If
        End If

        ' Acreditacion
        Dim listBoxAcreditacion As ASPxListBox = CType(dropDownAcreditacion.FindControl("listBoxAcreditacion"), ASPxListBox)
        Peticion.Acreditacion = String.Empty
        If listBoxAcreditacion IsNot Nothing AndAlso listBoxAcreditacion.SelectedValues IsNot Nothing AndAlso listBoxAcreditacion.SelectedValues.Count > 0 Then
            If listBoxAcreditacion.SelectedValues.Count = 0 OrElse listBoxAcreditacion.SelectedValues.Count = 2 Then
                Peticion.Acreditacion = String.Empty
            ElseIf listBoxAcreditacion.SelectedValues(0).ToString() = "1" Then
                Peticion.Acreditacion = "1"
            ElseIf listBoxAcreditacion.SelectedValues(0).ToString() = "0" Then
                Peticion.Acreditacion = "0"
            End If
        End If

        ' Delegaciones
        Dim listBoxDelegaciones As ASPxListBox = CType(dropDownDelegaciones.FindControl("listBoxDelegaciones"), ASPxListBox)
        If listBoxDelegaciones IsNot Nothing AndAlso listBoxDelegaciones.SelectedValues IsNot Nothing AndAlso listBoxDelegaciones.SelectedValues.Count > 0 Then
            If Peticion.Delegaciones Is Nothing Then Peticion.Delegaciones = New List(Of String)
            For Each Delegacion In listBoxDelegaciones.SelectedValues
                If Not Delegacion.ToString().ToUpper.Equals("TODOS") Then
                    Peticion.Delegaciones.Add(Delegacion)
                End If
            Next
        End If

        ' Canales
        Dim listBoxCanales As ASPxListBox = CType(dropDownCanales.FindControl("listBoxCanales"), ASPxListBox)
        If listBoxCanales IsNot Nothing AndAlso listBoxCanales.SelectedValues IsNot Nothing AndAlso listBoxCanales.SelectedValues.Count > 0 Then
            If Peticion.Canales Is Nothing Then Peticion.Canales = New List(Of String)
            For Each canal In listBoxCanales.SelectedValues
                If Not canal.ToString().ToUpper.Equals("TODOS") Then
                    Peticion.Canales.Add(canal)
                End If
            Next
        End If

        ' Fechas

        Peticion.Fecha = Prosegur.Genesis.Comon.Extenciones.RecuperarEnum(Of Enumeradores.TipoFechas)(comboFecha.Value(0).ToString)

        Peticion.Oid_delegacionGMT = InformacionUsuario.DelegacionSeleccionada.Identificador

        Peticion.ImporteInformativo = ckbImporteInformativo.Checked

        If ckbShipOut.Checked Then
            ' Fecha Gestion
            If Not String.IsNullOrEmpty(txtFechaGestion.Text) Then
                Peticion.FechaGestion = DateTime.ParseExact(txtFechaGestion.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True) ' Convert.ToDateTime(txtFechaGestion.Text).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True)
            End If
        Else

            ' .FechaGestion = DateTime.ParseExact(dateGestion, "yyyy-MM-dd HH:mm:sszzz", CultureInfo.CurrentCulture)
            ' Fecha Desde
            If Not String.IsNullOrEmpty(txtFechaDesde.Text) Then
                Peticion.FechaDesde = DateTime.ParseExact(txtFechaDesde.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True) '  Convert.ToDateTime(txtFechaDesde.Text).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True)
            End If
            ' Fecha Hasta
            If Not String.IsNullOrEmpty(txtFechaHasta.Text) Then
                Peticion.FechaHasta = DateTime.ParseExact(txtFechaHasta.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.CurrentCulture).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True) ' Convert.ToDateTime(txtFechaHasta.Text).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True)
            End If
        End If

        ' Maquinas

        If Me.ucSectores IsNot Nothing AndAlso Me.ucSectores.Sectores IsNot Nothing AndAlso Me.ucSectores.Sectores.Count > 0 Then
            If Peticion.Sectores Is Nothing Then Peticion.Sectores = New List(Of String)
            For Each objMaquina In Sectores
                Peticion.Sectores.Add(objMaquina.Identificador)
            Next
        End If



        ' Clientes
        If Me.ucClientes IsNot Nothing AndAlso Me.ucClientes.Clientes IsNot Nothing AndAlso Me.ucClientes.Clientes.Count > 0 Then
            If Peticion.Clientes Is Nothing Then Peticion.Clientes = New List(Of String)
            For Each cliente In Me.ucClientes.Clientes
                Peticion.Clientes.Add(cliente.Identificador)

                If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then
                    If Peticion.Subclientes Is Nothing Then Peticion.Subclientes = New List(Of String)
                    For Each objSubClientes In cliente.SubClientes
                        Peticion.Subclientes.Add(objSubClientes.Identificador)

                        If objSubClientes.PuntosServicio IsNot Nothing AndAlso objSubClientes.PuntosServicio.Count > 0 Then
                            If Peticion.PuntosServicios Is Nothing Then Peticion.PuntosServicios = New List(Of String)

                            For Each objPunto In objSubClientes.PuntosServicio
                                Peticion.PuntosServicios.Add(objPunto.Identificador)

                            Next
                        End If
                    Next
                End If
            Next
        End If

        ' Banco Capital
        If Me.ucBancos IsNot Nothing AndAlso Me.ucBancos.Clientes IsNot Nothing AndAlso Me.ucBancos.Clientes.Count > 0 Then
            If Peticion.BancosCapital Is Nothing Then Peticion.BancosCapital = New List(Of String)
            For Each banco In Me.ucBancos.Clientes
                Peticion.BancosCapital.Add(banco.Identificador)

                If banco.SubClientes IsNot Nothing AndAlso banco.SubClientes.Count > 0 Then
                    If Peticion.BancosTesoreria Is Nothing Then Peticion.BancosTesoreria = New List(Of String)

                    For Each bancosTesoreria In banco.SubClientes
                        Peticion.BancosTesoreria.Add(bancosTesoreria.Identificador)

                        If bancosTesoreria.PuntosServicio IsNot Nothing AndAlso bancosTesoreria.PuntosServicio.Count > 0 Then
                            If Peticion.CuentaTesoreria Is Nothing Then Peticion.CuentaTesoreria = New List(Of String)

                            For Each cuentaTesoreria In bancosTesoreria.PuntosServicio
                                Peticion.CuentaTesoreria.Add(cuentaTesoreria.Identificador)

                            Next
                        End If
                    Next
                End If
            Next
        End If

        ' Banco Comision
        If Me.ucBancoComision IsNot Nothing AndAlso Me.ucBancoComision.Clientes IsNot Nothing AndAlso Me.ucBancoComision.Clientes.Count > 0 Then
            If Peticion.BancosComision Is Nothing Then Peticion.BancosComision = New List(Of String)
            For Each banco In Me.ucBancoComision.Clientes
                Peticion.BancosComision.Add(banco.Identificador)
            Next
        End If

        ' Tipos Planificaciones
        Dim listBoxTipoPlanificacion As ASPxListBox = CType(dropDownTipoPlanificacion.FindControl("listBoxTipoPlanificacion"), ASPxListBox)
        If listBoxTipoPlanificacion IsNot Nothing AndAlso listBoxTipoPlanificacion.SelectedValues IsNot Nothing AndAlso listBoxTipoPlanificacion.SelectedValues.Count > 0 Then
            If Peticion.TipoPlanificaciones Is Nothing Then Peticion.TipoPlanificaciones = New List(Of String)
            For Each Tipo In listBoxTipoPlanificacion.SelectedValues
                If Not Tipo.ToString().ToUpper.Equals("TODOS") Then
                    Peticion.TipoPlanificaciones.Add(Tipo)
                End If
            Next
        End If


        'Planificaciones
        If Me.ucPlanificacion IsNot Nothing AndAlso Me.ucPlanificacion.Planificaciones IsNot Nothing AndAlso Me.ucPlanificacion.Planificaciones.Count > 0 Then
            If Peticion.Planificaciones Is Nothing Then Peticion.Planificaciones = New List(Of String)
            For Each plan In Me.ucPlanificacion.Planificaciones
                Peticion.Planificaciones.Add(plan.Identificador)
            Next
        End If

        'Campo Extra
        If ucCampoExtra.Visible Then
            If CampoExtraTermino.Codigo IsNot Nothing Then
                Peticion.CampoExtra = CampoExtraTermino.Codigo
                Peticion.CampoExtraValor = txtCampoExtra.Text

            End If
        End If

    End Sub

#End Region

    Private Sub HacerVisibleCampoExtra(pYesOrNo As Boolean)
        ucCampoExtra.Visible = pYesOrNo
        lblCampoExtraValor.Visible = pYesOrNo
        txtCampoExtra.Visible = pYesOrNo
    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'HacerVisibleCampoExtra(False)


        grid.DataSource = Transacciones()
        grid.DataBind()
    End Sub

#Region "[Popup]"

    Private WithEvents _ucPopUp As Popup
    Public Property ucPopup() As Popup
        Get
            If _ucPopUp Is Nothing Then
                _ucPopUp = LoadControl("~\Controles\Popup.ascx")
                _ucPopUp.ID = Me.ID & "_ucPopup"
                _ucPopUp.Height = 560
                _ucPopUp.EsModal = True
                _ucPopUp.AutoAbrirPopup = False
                _ucPopUp.PopupBase = ucDetalleTransaccion
                phUcPopUp.Controls.Add(_ucPopUp)
            End If
            Return _ucPopUp
        End Get
        Set(value As Popup)
            _ucPopUp = value
        End Set
    End Property

    Private WithEvents _ucDetalleTransaccion As ucDetalleTransaccion

    Public Property ucDetalleTransaccion() As ucDetalleTransaccion
        Get
            If _ucDetalleTransaccion Is Nothing Then
                _ucDetalleTransaccion = LoadControl("~\Controles\ucDetalleTransaccion.ascx")
                _ucDetalleTransaccion.ID = Me.ID & "_ucDetalleTransaccion"
            End If
            Return _ucDetalleTransaccion
        End Get
        Set(value As ucDetalleTransaccion)
            _ucDetalleTransaccion = value
        End Set
    End Property

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SALDOS_CONSULTAR_TRANSACCIONES
        'MyBase.ValidarAcesso = False
        MyBase.CodFuncionalidad = "SALDOS_CONSULTAR_TRANSACCIONES"
    End Sub

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

#End Region

    Private Sub btnDetalle_Click(sender As Object, e As System.EventArgs) Handles btnDetalle.Click
        Dim CodExternoBase = Request.Params("__EVENTARGUMENT")


        ucDetalleTransaccion.Attributes.Add("CodExternoBase", CodExternoBase)

        Try
            Dim url As String = String.Empty

            url = "ConsultaDetalleTransacciones.aspx?CodExternoBase=" + CodExternoBase

            Master.ExibirModal(url, "Detalle", 530, 900, False, True, "")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnBorrarPreferencias_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBorrarPreferencias.Click
        ' TraduzirControles()
        Expand = Expand
        grid.LoadLayoutFromString(PreferenciasOriginal)

        Dim respuesta As New BorrarPreferenciasAplicacionRespuesta
        Dim proxy As New ProxyPreferencias()

        Dim peticion As New BorrarPreferenciasAplicacionPeticion() With {
                                .CodigoAplicacion = Enumeradores.CodigoAplicacion.SaldosNuevo,
                                .CodigoUsuario = Parametros.Permisos.Usuario.Login,
                                .CodigoFuncionalidad = Me.CodFuncionalidad
                            }

        respuesta = proxy.BorrarPreferenciasAplicacion(peticion)
    End Sub

    Protected Sub btnChangeMaquina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeMaquina.Click
        ConfigurarControles()
    End Sub
    Protected Sub btnChangeDelegaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeDelegaciones.Click

        'ucClientes.Delegaciones = New List(Of String)
        'Dim listBoxDelegaciones As ASPxListBox = CType(dropDownDelegaciones.FindControl("listBoxDelegaciones"), ASPxListBox)
        'If listBoxDelegaciones IsNot Nothing AndAlso listBoxDelegaciones.SelectedValues IsNot Nothing AndAlso listBoxDelegaciones.SelectedValues.Count > 0 Then
        '    For Each Delegacion In listBoxDelegaciones.SelectedValues
        '        If Not Delegacion.ToString().ToUpper.Equals("TODOS") Then
        '            ucClientes.Delegaciones.Add(Delegacion)
        '        End If
        '    Next
        'End If

        '  ConfigurarControles()
    End Sub

    Protected Sub btnExpand_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExpand.Click

        If Expand Then
            Expand = False
            grid.CollapseAll()
            'ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Expandir")

        Else
            Expand = True
            grid.ExpandAll()
            'ASPxPopupMenu1.Items(3).Text = MyBase.RecuperarValorDic("Retraer")
        End If

        ConfiguraGrid()

    End Sub

    Private Sub btnPopUp_Click(sender As Object, e As System.EventArgs) Handles btnPopUp.Click
    End Sub

    Private Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim tipo = Request.Params("__EVENTARGUMENT")

        Dim options As XlsxExportOptions = New XlsxExportOptions()
        options.TextExportMode = TextExportMode.Value
        'grid.ExpandAll()
        gvDivisa.Visible = True
        grid.ExpandAll()
        Select Case tipo
            Case "PDF"

                ASPxPivotGridExporter1.ExportPdfToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"))' WritePdfToResponse()
            Case "XLS"
                ASPxPivotGridExporter1.ExportXlsToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"))
            Case "XLSX"
                ASPxPivotGridExporter1.ExportXlsxToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"), options)
            Case "CSV"
                ASPxPivotGridExporter1.ExportCsvToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"))
            Case "RTF"
                ASPxPivotGridExporter1.ExportRtfToResponse(Traduzir("079_lblTitulo") + " " + DateTime.Now.ToString("yyyyMMddHHmmss"))
        End Select

    End Sub

    Private Sub GuardarConfigGrid()
        Preferencias = New System.IO.MemoryStream()
        grid.SaveLayoutToStream(Preferencias)

        Dim Respuesta As New GuardarPreferenciasRespuesta
        Dim proxy As New ProxyPreferencias()
        Dim peticion As New GuardarPreferenciasPeticion()
        peticion.Preferencias = New ObjectModel.ObservableCollection(Of PreferenciaUsuario)

        Dim preferencia As New PreferenciaUsuario() With {
                                .CodigoAplicacion = Enumeradores.CodigoAplicacion.SaldosNuevo,
                                .CodigoUsuario = Parametros.Permisos.Usuario.Login,
                                .CodigoFuncionalidad = Me.CodFuncionalidad,
                                .CodigoComponente = "pvGrid",
                                .ValorBinario = Preferencias.ToArray()
                            }
        peticion.Preferencias.Add(preferencia)
        Respuesta = proxy.GuardarPreferencias(peticion)
    End Sub

    Protected Sub grid_ClientLayout(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.GridLayout
        If grid.Visible Then
            GuardarConfigGrid()
        End If
    End Sub

    Protected Sub grid_ClientLayout(sender As Object, e As DevExpress.Web.ASPxClasses.ASPxClientLayoutArgs)
        If grid.Visible Then
            GuardarConfigGrid()
        End If
    End Sub

    Protected Sub grid_Init(ByVal sender As Object, ByVal e As System.EventArgs)
        DefinirParametrosBase()
        TraduzirControles()
    End Sub

    Private Sub ConsultaTransacciones_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ConfiguraGrid()

        If ucCampoExtra.CampoExtraTermino Is Nothing Then
            ucCampoExtra.CampoExtraTermino = New Termino
        End If

        Me.ucCampoExtra.AtualizarRegistrosCamposExtras()
        Me.ucCampoExtra.CampoExtraHabilitado = True
        TraduzirExpand(Expand)
    End Sub

    Private Sub ConfiguraGrid()
        grid.FieldValueTemplate = New FieldValueTemplate(btnDetalle.UniqueID, Expand)
        grid.CellTemplate = New CellTemplate(btnDetalle.UniqueID)
    End Sub

    Private Class FieldValueTemplate
        Implements ITemplate
        Dim idButton As String
        Dim Expand As Boolean

        Public Sub New()
        End Sub

        Public Sub New(idButton As String, Expand As Boolean)
            Me.idButton = idButton
            Me.Expand = Expand
        End Sub

        Public Sub InstantiateIn(ByVal container As UI.Control) Implements ITemplate.InstantiateIn
            Dim c As PivotGridFieldValueTemplateContainer = CType(container, PivotGridFieldValueTemplateContainer)
            Dim cell As PivotGridFieldValueHtmlCell = c.CreateFieldValue()
            Dim valueItem As DevExpress.XtraPivotGrid.Data.PivotFieldValueItem = c.ValueItem
            Dim ds As PivotDrillDownDataSource = valueItem.CreateDrillDownDataSource()

            If ds.RowCount > 0 Then

                Dim codExternoBase As String = String.Empty
                If SomenteUmDocumento(ds, Convert.ToString(ds(0)("CodExternoBase"))) Then
                    codExternoBase = Convert.ToString(ds(0)("CodExternoBase"))
                End If

                If String.IsNullOrWhiteSpace(c.Text) AndAlso Expand Then
                    cell.Controls.Clear()
                End If

                cell.Controls.AddAt(cell.Controls.IndexOf(cell.TextControl), New MyLink(c.Text, codExternoBase, idButton))
                cell.Controls.Remove(cell.TextControl)
                c.Controls.Add(cell)




            End If
            cell.BackColor = System.Drawing.Color.Red
        End Sub

        Private Function SomenteUmDocumento(ds As PivotDrillDownDataSource, codigoExterno As String) As Boolean
            For index = 0 To ds.RowCount - 1
                If ds(index)("CodExternoBase") <> codigoExterno Then
                    Return False
                End If
            Next
            Return True
        End Function

    End Class

    Private Class CellTemplate
        Implements ITemplate

        Dim idButton As String
        Public Sub New()
        End Sub

        Public Sub New(idButton As String)
            Me.idButton = idButton
        End Sub

        Public Sub InstantiateIn(ByVal container As UI.Control) Implements ITemplate.InstantiateIn
            Dim c As PivotGridCellTemplateContainer = TryCast(container, PivotGridCellTemplateContainer)
            Dim ds As PivotDrillDownDataSource = c.Item.CreateDrillDownDataSource()
            Dim codExternoBase As String = String.Empty


            If ds IsNot Nothing AndAlso ds.RowCount > 0 Then
                If SomenteUmDocumento(ds, Convert.ToString(ds(0)("CodExternoBase"))) Then
                    codExternoBase = Convert.ToString(ds(0)("CodExternoBase"))
                End If

            End If

            Dim text = String.Empty
            If "|ImporteContado|ImporteDeclarado|".Contains(c.Item.DataField.FieldName) Then
                Dim divisas As New Dictionary(Of String, Double)

                For Each row As DevExpress.XtraPivotGrid.PivotDrillDownDataRow In ds
                    Dim Sibol As String = ""
                    If Not String.IsNullOrWhiteSpace(row("Simbolo")) Then
                        Sibol = row("Simbolo")
                    End If
                    If Sibol IsNot Nothing Then
                        If divisas.ContainsKey(Sibol) Then
                            divisas.Item(Sibol) += Convert.ToDouble(row("ImporteContado"))
                        Else
                            If Convert.ToDouble(row("ImporteContado")) <> 0 Then

                                divisas.Add(Sibol, Convert.ToDouble(row("ImporteContado")))
                            End If
                        End If
                    End If
                Next

                For Each kvp As KeyValuePair(Of String, Double) In divisas
                    text += "</br> " + kvp.Key + " " + kvp.Value.ToString("N2")
                Next
            End If

            If text.Length > 6 Then
                text = text.Substring(6)
                c.Controls.Add(New MyLink(text, codExternoBase, idButton))
            End If

        End Sub

        Private Function SomenteUmDocumento(ds As PivotDrillDownDataSource, codigoExterno As String) As Boolean
            For index = 0 To ds.RowCount - 1
                If ds(index)("CodExternoBase") <> codigoExterno Then
                    Return False
                End If
            Next
            Return True
        End Function

    End Class

    Public Class MyLink
        Inherits Label

        Public Sub New(ByVal text As String, ByVal id As String, idButton As String)
            MyBase.New()
            Me.Text = text

            Me.CssClass = "CssStyleNotClick"
            If Not String.IsNullOrEmpty(id) Then
                Me.CssClass = "CssStyleClick"

                ' Me.Font.Italic = True
                Attributes("onclick") = " __doPostBack('" + idButton + "','" + id + "');"
            End If

        End Sub

    End Class

    Private Sub btnChangedTipoTransacciones_Click(sender As Object, e As System.EventArgs) Handles btnChangedTipoTransacciones.Click
        Try
            Dim listBoxTipoTransacciones As ASPxListBox = CType(dropDownTipoTransacciones.FindControl("listBoxTipoTransacciones"), ASPxListBox)
            If listBoxTipoTransacciones.SelectedItems.Count = 1 Then
                If listBoxTipoTransacciones.SelectedValues(0).Equals(TIPO_PLANIFICACION_ACREDITACION) Then
                    ucCampoExtra.Visible = True
                    lblCampoExtraValor.Visible = True
                    txtCampoExtra.Visible = True
                Else
                    ucCampoExtra.Visible = False
                    lblCampoExtraValor.Visible = False
                    txtCampoExtra.Visible = False
                End If
            Else
                ucCampoExtra.Visible = False
                lblCampoExtraValor.Visible = False
                txtCampoExtra.Visible = False
            End If
        Catch ex As Exception
            'No hace nada.
        End Try
    End Sub
End Class