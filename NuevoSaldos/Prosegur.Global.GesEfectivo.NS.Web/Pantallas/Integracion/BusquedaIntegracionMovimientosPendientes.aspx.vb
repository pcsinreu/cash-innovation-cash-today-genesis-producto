Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxEditors
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.LogicaNegocio.Integracion

Public Class BusquedaIntegracionMovimientosPendientes
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property Sectores As ObservableCollection(Of Clases.Sector)
        Get
            Return ucSectores.Sectores
        End Get
        Set(value As ObservableCollection(Of Clases.Sector))
            ucSectores.Sectores = value
        End Set
    End Property

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
            _ucSectores = value
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

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CONSULTAR_SALDO
        MyBase.CodFuncionalidad = "BUSQUEDA_INTEGRACION"
    End Sub

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()

        Master.Titulo = MyBase.RecuperarValorDic("tituloIntegracion")
        Me.lblFiltros.Text = MyBase.RecuperarValorDic("lblFiltro")
        Me.btnBuscar.Text = MyBase.RecuperarValorDic("btnBuscar")
        btnLimpiar.Text = MyBase.RecuperarValorDic("btnLimpiar")

        lblCodigo.Text = MyBase.RecuperarValorDic("lblCodigo")
        lblTipoCodigo.Text = MyBase.RecuperarValorDic("lblTipoCodigo")

        lblTipoError.Text = MyBase.RecuperarValorDic("lblTipoError")
        lblMensajeError.Text = MyBase.RecuperarValorDic("lblMensajeError")
        ucSectores.ucMaquina.Titulo = MyBase.RecuperarValorDic("DeviceID")
        ucSectores.ucMaquina.FindControl("codigo")


        grilla.Columns(0).Caption = String.Empty
        grilla.Columns(2).Caption = MyBase.RecuperarValorDic("CodActualID")
        grilla.Columns(3).Caption = MyBase.RecuperarValorDic("lblTipoError")
        grilla.Columns(4).Caption = MyBase.RecuperarValorDic("CodProceso")
        grilla.Columns(5).Caption = MyBase.RecuperarValorDic("Reintentos")
        grilla.Columns(6).Caption = MyBase.RecuperarValorDic("lblEstado")

        btnParar.Text = MyBase.RecuperarValorDic("btnParar")
        btnReenviar.Text = MyBase.RecuperarValorDic("btnReenviar")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        GoogleAnalyticsHelper.TrackAnalytics(Me, "Integracion - Movimientos Pendientes")

        Me.dvFiltros.Style.Item("display") = "block"
        Me.dvTituloFiltro.Style.Item("display") = "block"


        Me.ConfigurarControles()
        Me.ucSectores.Focus()
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            grilla.DataSource = GrillaResultado
            grilla.DataBind()
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucSectores_OnControleAtualizado() Handles _ucSectores.UpdatedControl
        Try

            If Me.ucSectores.Sectores IsNot Nothing Then
                Sectores = Me.ucSectores.Sectores
            End If

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click
        Me.LimpiarControles()
    End Sub

    Private Sub LimpiarControles()
        Clientes.Clear()
        txtCodigo.Text = String.Empty
        txtCodigoProceso.Text = String.Empty
        txtError.Text = String.Empty
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

        ucSectores.ucMaquina.LimparCampos()
        ucSectores.ucMaquina.LimparViewState()
        ucSectores.ucSector_OnControleAtualizado()

        Me.ConfigurarControles()

        grilla.DataSource = Nothing
        grilla.DataBind()

    End Sub

#End Region

#Region "[METODOS]"

#Region "     Helpers     "

    Protected Sub ConfigurarControle_Sector()
        Me.ucSectores.ConsiderarPermissoes = True
        Me.ucSectores.SelecaoMultipla = False
        Me.ucSectores.SectorHabilitado = True

        Me.ucSectores.SolamenteSectoresPadre = True


        If Sectores IsNot Nothing Then
            Me.ucSectores.Sectores = Sectores
        End If
    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.PtoServicioHabilitado = True

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ConfigurarControles()
        Me.ConfigurarControle_Sector()
        Me.ConfigurarControle_Cliente()
        Me.ConfigurarControl_TipoCodigo(Not IsPostBack)
        Me.ConfigurarControl_TipoError(Not IsPostBack)
        Me.ConfigurarControl_Estado(Not IsPostBack)
        Me.ConfigurarControl_Grilla(Not IsPostBack)
    End Sub

    Private Sub ConfigurarControl_Grilla(valorInicial As Boolean)
        If valorInicial Then
            grilla.Visible = False
            dvRegistros.Visible = False
        End If
    End Sub

    Private Sub ConfigurarControl_TipoCodigo(valorInicial As Boolean)
        Try
            If valorInicial Then
                ddlTipoCodigo.Items.Clear()
                ddlTipoCodigo.Items.Add(New ListItem(MyBase.RecuperarValorDic("ACTUAL_ID"), "1"))
                ddlTipoCodigo.Items.Add(New ListItem(MyBase.RecuperarValorDic("CODIGO_EXTERNO"), "2"))
                ddlTipoCodigo.Items.Add(New ListItem(MyBase.RecuperarValorDic("COLLECTION_ID"), "3"))
            End If
            ddlTipoCodigo.DataBind()
        Catch ex As Exception
            'No hacer nada
        End Try
    End Sub

    Private Sub ConfigurarControl_TipoError(valorInicial As Boolean)
        Try
            Dim listTipoError As ASPxListBox = CType(ddlTipoError.FindControl("listTipoError"), ASPxListBox)

            If valorInicial Then
                listTipoError.Items.Clear()
                listTipoError.Items.Insert(0, (New ListEditItem(MyBase.RecuperarValorDic("msgTodos"), "1")))
                listTipoError.Items.Insert(1, (New ListEditItem(MyBase.RecuperarValorDic("msgNegocio"), "2")))
                listTipoError.Items.Insert(2, (New ListEditItem(MyBase.RecuperarValorDic("msgInfraestructura"), "3")))
            Else
                listTipoError.Items.Insert(3, New ListEditItem("", ""))
                listTipoError.Items.RemoveAt(3)
            End If
            listTipoError.DataBind()
        Catch ex As Exception
            'No hacer nada
        End Try

    End Sub

    Private Sub ConfigurarControl_Estado(valorInicial As Boolean)
        Try
            Dim listEstado As ASPxListBox = CType(ddlStado.FindControl("listStado"), ASPxListBox)

            If valorInicial Then
                listEstado.Items.Clear()
                listEstado.Items.Insert(0, (New ListEditItem(MyBase.RecuperarValorDic("msgTodos"), "0")))
                listEstado.Items.Insert(1, (New ListEditItem(MyBase.RecuperarValorDic("msgEnviosConExito"), "1")))
                listEstado.Items.Insert(2, (New ListEditItem(MyBase.RecuperarValorDic("msgEnviosSinExito"), "2")))
                listEstado.Items.Insert(3, (New ListEditItem(MyBase.RecuperarValorDic("msgPendientes"), "3")))
            Else
                listEstado.Items.Insert(4, New ListEditItem("", ""))
                listEstado.Items.RemoveAt(4)
            End If
            listEstado.DataBind()
        Catch ex As Exception
            'No hacer nada.
        End Try

    End Sub

#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try

            BuscarMovimientosPendientes()

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub




    Public Property GrillaResultado() As List(Of Reintentos.Clases.MovimientoActualID)
        Get
            Return Session("grillaResultado")
        End Get
        Set(ByVal value As List(Of Reintentos.Clases.MovimientoActualID))
            Session("grillaResultado") = value
        End Set
    End Property

    Private Sub BuscarMovimientosPendientes()
        Dim peticion As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion = ObtenerPeticionPorBusqueda()
        Dim grilla_mostrar = AccionFechaValorOnline.DevolverPendientes(peticion)

        PoblarGrilla(grilla_mostrar)
        peticion = Nothing
    End Sub

    Property lstBusqueda() As List(Of ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Clases.MovimientoActualID)
        Get
            If Session("_lstBusqueda") Is Nothing Then
                Session("_lstBusqueda") = New List(Of ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Clases.MovimientoActualID)
            End If
            Return Session("_lstBusqueda")
        End Get
        Set(value As List(Of ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Clases.MovimientoActualID))
            Session("_lstBusqueda") = value
        End Set
    End Property

    Private Sub PoblarGrilla(unaGrilla As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Respuesta)
        Try
            grilla.Visible = True
            dvRegistros.Visible = True
            lstBusqueda = unaGrilla.Movimientos
            GrillaResultado = unaGrilla.Movimientos
            grilla.DataSource = GrillaResultado
            grilla.DataBind()
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Private Function ObtenerPeticionPorBusqueda() As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion
        Dim peticionBusqueda As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion
        peticionBusqueda = New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion

        peticionBusqueda.IdentificadorPuntoServicio = String.Empty
        peticionBusqueda.IdentificadorSubCliente = String.Empty
        peticionBusqueda.IdentificadorCliente = String.Empty

        peticionBusqueda = getCodigoAndTipoCodigo(peticionBusqueda)
        peticionBusqueda.CodProceso = txtCodigoProceso.Text

        If Sectores.Count > 0 Then
            peticionBusqueda.DeviceID = Sectores.ElementAt(0).Codigo
        End If

        peticionBusqueda = getClientesFiltroBusqueda(peticionBusqueda)
        peticionBusqueda.Estados = getEstadosFiltroBusqueda()
        peticionBusqueda.MensajeError = txtError.Text


        Return peticionBusqueda
    End Function

    Private Function getCodigoAndTipoCodigo(peticionBusqueda As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion) As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion
        peticionBusqueda.Codigo = txtCodigo.Text

        Select Case ddlTipoCodigo.SelectedItem.Value
            Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.ActualID
                peticionBusqueda.TipoCodigo = ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.ActualID
            Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.CollectionID
                peticionBusqueda.TipoCodigo = ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.CollectionID
            Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.CodigoExterno
                peticionBusqueda.TipoCodigo = ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.TipoCodigo.CodigoExterno
        End Select

        Return peticionBusqueda
    End Function

    Private Function getClientesFiltroBusqueda(ByRef peticionBusqueda As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion) As ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Peticion
        If Clientes.Count > 0 Then
            Dim objCliente As Clases.Cliente = Clientes.ElementAt(0)
            peticionBusqueda.IdentificadorCliente = objCliente.Identificador
            If objCliente.SubClientes IsNot Nothing AndAlso objCliente.SubClientes.Count > 0 Then
                peticionBusqueda.IdentificadorSubCliente = objCliente.SubClientes.ElementAt(0).Identificador
                If objCliente.SubClientes.ElementAt(0).PuntosServicio IsNot Nothing AndAlso objCliente.SubClientes.ElementAt(0).PuntosServicio.Count > 0 Then
                    peticionBusqueda.IdentificadorPuntoServicio = objCliente.SubClientes.ElementAt(0).PuntosServicio.ElementAt(0).Identificador
                End If
            End If
        End If

        Return peticionBusqueda
    End Function

    Private Function getEstadosFiltroBusqueda() As List(Of ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados)
        Dim listaRetorno As New List(Of ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados)
        Dim listEstado As ASPxListBox = CType(ddlStado.FindControl("listStado"), ASPxListBox)

        For Each elemento In listEstado.Items
            If elemento.selected Then
                Select Case elemento.value.ToString()
                    Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.EnvioConExito
                        listaRetorno.Add(ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.EnvioConExito)
                    Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.EnvioSinExito
                        listaRetorno.Add(ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.EnvioSinExito)
                    Case ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.Pendiente
                        listaRetorno.Add(ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Estados.Pendiente)
                End Select
            End If
        Next

        Return listaRetorno
    End Function

    Private Sub btnReenviar_Click(sender As Object, e As System.EventArgs) Handles btnReenviar.Click
        Dim mensaje As String = String.Empty
        Dim texto As StringBuilder = New StringBuilder()

        If HayElementosSeleccionados() Then

            Dim seleccionados As List(Of String) = New List(Of String)
            Dim objConfiguracion As New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion

            For Each codigo_actual_id In grilla.GetSelectedFieldValues("ActualID")
                seleccionados.Add(codigo_actual_id)
            Next

            Dim unUsuario As String = String.Empty
            If Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login IsNot Nothing Then
                unUsuario = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login
            End If

            'TODO Ver de enviar el identificador de llamada
            Dim objFVO = New IntegracionFechaValorOnline(unUsuario, Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoPais, String.Empty, Enumeradores.EstadoIntegracionDetalle.ReenvioManual, True, String.Empty, False)
            objFVO.DefinirIdentificadores(seleccionados)
            Dim respuestaFVO = objFVO.Ejecutar()

            For Each elemento_respuesta In respuestaFVO
                If elemento_respuesta.TipoError <> "1" AndAlso Not String.IsNullOrEmpty(elemento_respuesta.TipoError) Then
                    texto.AppendLine(String.Format("{0} - {1}", elemento_respuesta.TipoResultado, elemento_respuesta.Detalle))
                End If
            Next

            mensaje = texto.ToString()

            If String.IsNullOrEmpty(mensaje) Then
                mensaje = MyBase.RecuperarValorDic("exitoReenviarIntegracionMovimientos")
                BuscarMovimientosPendientes()
            End If

        Else
            mensaje = MyBase.RecuperarValorDic("noHayElementoSeleccionados")
        End If
        grilla.Selection.UnselectAll()
        MyBase.MostraMensagemErro(mensaje)
    End Sub

    Private Sub btnParar_Click(sender As Object, e As System.EventArgs) Handles btnParar.Click
        Dim mensaje As String = String.Empty
        Dim texto As StringBuilder = New StringBuilder()

        If HayElementosSeleccionados() Then

            Dim seleccionados As List(Of String) = New List(Of String)
            Dim objConfiguracion As New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Integracion.Configuracion

            For Each codigo_actual_id In grilla.GetSelectedFieldValues("ActualID")
                seleccionados.Add(codigo_actual_id)
            Next

            Dim unUsuario As String = String.Empty
            If Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login IsNot Nothing Then
                unUsuario = Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.Login
            End If

            'TODO Ver de enviar identificador de llamada
            Dim objFVO = New IntegracionFechaValorOnline(unUsuario, Prosegur.Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoPais, String.Empty, Enumeradores.EstadoIntegracionDetalle.ReenvioManual, False, String.Empty, True)
            objFVO.DefinirIdentificadores(seleccionados)
            Dim respuestaFVO = objFVO.Ejecutar()

            For Each elemento_respuesta In respuestaFVO
                If elemento_respuesta.TipoError <> "1" AndAlso Not String.IsNullOrEmpty(elemento_respuesta.TipoError) Then
                    texto.AppendLine(String.Format("{0} - {1}", elemento_respuesta.TipoResultado, elemento_respuesta.Detalle))
                End If
            Next

            mensaje = texto.ToString()

            If String.IsNullOrEmpty(mensaje) Then
                mensaje = MyBase.RecuperarValorDic("exitoPararIntegracionMovimientos")
                BuscarMovimientosPendientes()
            End If

        Else
            mensaje = MyBase.RecuperarValorDic("noHayElementoSeleccionados")
        End If

        grilla.Selection.UnselectAll()
        MyBase.MostraMensagemErro(mensaje)

    End Sub

    Private Function HayElementosSeleccionados() As Boolean
        Dim retorno As Boolean = False
        Try
            If grilla.Selection.Count > 0 Then
                retorno = True
            End If
        Catch ex As Exception
            retorno = False
        End Try

        Return retorno
    End Function

    Protected Sub imgVerDetalle(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            Dim objMovimiento As New ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Clases.MovimientoActualID

            objMovimiento = lstBusqueda.Where(Function(x) x.ID = e.CommandArgument).FirstOrDefault
            'Variable de session que espera la pantalla
            Session("objMovimientoIntegracion") = objMovimiento

            Try
                Dim url As String = "IntegracionDetalle.aspx"
                Master.ExibirModal(url, "Detalle", 600, 1000, False, True, "")
            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try

        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub


#End Region

#End Region


End Class