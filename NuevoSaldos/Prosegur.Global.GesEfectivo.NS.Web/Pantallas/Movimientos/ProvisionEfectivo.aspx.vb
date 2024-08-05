Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.NuevoSaldos.Web.HelperDocumento
Imports Parametros = Prosegur.Genesis.Web.Login.Parametros
Imports Prosegur.Genesis
Imports System.Collections.ObjectModel
Imports Newtonsoft.Json
Imports System.Threading.Tasks

Public Class ProvisionEfectivo
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property TerminoIAC() As ObservableCollection(Of Clases.TerminoIAC)
        Get
            Return Session("_TerminoIAC")
        End Get
        Set(value As ObservableCollection(Of Clases.TerminoIAC))
            Session("_TerminoIAC") = value
        End Set


    End Property


    Public Property DivisasDisponiveis() As ObservableCollection(Of DivisaJS)
        Get
            Return Session("DivisasDisponiveis")
        End Get
        Set(value As ObservableCollection(Of DivisaJS))
            Session("DivisasDisponiveis") = value
        End Set
    End Property





    Private WithEvents _InfAdicionales As ucInfAdicionales
    Public ReadOnly Property InfAdicionales() As ucInfAdicionales
        Get
            If _InfAdicionales Is Nothing Then
                _InfAdicionales = LoadControl("~\Controles\ucInfAdicionales.ascx")
                _InfAdicionales.ID = "InfAdicionales"
                AddHandler _InfAdicionales.Erro, AddressOf ErroControles
                phInfAdicionales.Controls.Add(_InfAdicionales)
            End If
            Return _InfAdicionales
        End Get
    End Property

    Private Sub _ucInfAdicionales_ControleAtualizado(sender As Object, e As System.EventArgs) Handles _InfAdicionales.ControleAtualizado

    End Sub




    Private Sub BuscarDivisas()


        DivisasDisponiveis = Genesis.LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, Nothing, False, True, True, True, False, False, True).OrderBy(Function(d) d.Descripcion).Select(Function(x) New DivisaJS With {.Divisa = x}).ToObservableCollection()
        grdDivisas.DataSource = DivisasDisponiveis
        grdDivisas.DataBind()




    End Sub

    Private _Acciones As ucAcciones
    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = "Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get


    End Property


    Public Class DivisaJS
        Public Property Divisa As Clases.Divisa
        Public Property Valor As String

        Public Property Identificador As String
            Get
                Return _Divisa.Identificador
            End Get
            Set(value As String)
                _Divisa.Identificador = value
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Divisa.Descripcion
            End Get
            Set(value As String)
                _Divisa.Descripcion = value
            End Set
        End Property



    End Class

#End Region

#Region "[Helpers]"

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


    Public Sub ucBancos_OnControleAtualizado() Handles _ucBancos.UpdatedControl
        Try
            If ucBancos.Clientes IsNot Nothing Then
                Bancos = ucBancos.Clientes
                ConfigurarBanco()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

    Protected Sub ConfigurarBanco()

        Me.ucBancos.SelecaoMultipla = False
        Me.ucBancos.ClienteHabilitado = True
        Me.ucBancos.NoExhibirSubCliente = True
        Me.ucBancos.NoExhibirPtoServicio = True
        Me.ucBancos.EsBancoCapital = True

        If Me.ucBancos.Attributes.Item("selecionados") = "0" And Bancos.Count > 0 Then
            Me.ucBancos.Attributes.Item("selecionados") = Bancos.Count.ToString()

        ElseIf Bancos.Count = 0 Then
            Me.ucBancos.Attributes.Item("selecionados") = 0
        End If

        If Bancos IsNot Nothing Then
            Me.ucBancos.Clientes = Bancos
        End If

    End Sub


    Protected Overrides Sub Inicializar()
        ConfigurarBanco()



        If Not IsPostBack Then
            Bancos.Clear()
            TerminoIAC = Nothing
            DivisasDisponiveis = Nothing


            Dim respuesta = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Pantallas.ProvisionEfectivo.RecuperarFormulario("MAEPEF")
            TerminoIAC = respuesta.GrupoTerminosIACIndividual.TerminosIAC
            BuscarDivisas()
        End If

        InfAdicionales.Modo = Enumeradores.Modo.Alta
        InfAdicionales.Terminos = TerminoIAC
        ConfigurarCampos()
    End Sub

    Private Sub ConfigurarCampos()
        ConfiguraAcciones()

        lblDelegacion.Text = "999 - Central"
        lblSector.Text = "999-1 - Bóveda"
        lblCanal.Text = "CAP - Capital"
        lblSubCanal.Text = "ACR - Acreditación"

    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFechaGestion.ClientID, "True")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
    End Sub



#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SALDOS_CONSULTAR_TRANSACCIONES
        MyBase.ValidarAcesso = True
        MyBase.CodFuncionalidad = "PROVISION_EFECTIVO"
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Me.RecuperarValorDic("lblProvisionEfectivo")
        lblProvisionEfectivo.Text = Me.RecuperarValorDic("lblProvisionEfectivo")
        lblFechaGestion.Text = Me.RecuperarValorDic("lblFechaGestion")
        lblDetalheCuentaCapital.Text = Me.RecuperarValorDic("lblDetalheCuentaCapital")
        lblTituloDelegacion.Text = Me.RecuperarValorDic("lblTituloDelegacion")
        lblTituloSector.Text = Me.RecuperarValorDic("lblTituloSector")
        lblTituloCanal.Text = Me.RecuperarValorDic("lblTituloCanal")
        lblTituloSubCanal.Text = Me.RecuperarValorDic("lblTituloSubCanal")


    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        GoogleAnalyticsHelper.TrackAnalytics(Me, "Saldos Cliente")

        If Not Me.IsPostBack Then


        End If
        If Request("__EVENTTARGET") = Acciones.UniqueIDbtnConfirmar Then
            ' Acciones_onAccionConfirmar()
        End If

    End Sub

#End Region


#Region "     Helpers     "


#End Region

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(e.Erro.Message)
    End Sub

    Private Sub ConfiguraAcciones()
        Acciones.btnGuardarVisible = True
        Acciones.btnCancelarVisible = True

        AddHandler Acciones.onAccionGuardar, AddressOf Acciones_onAccionGuardar

        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar

    End Sub

    Private Sub Acciones_onAccionGuardar()
        Try

            Dim peticion = New ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Peticion

            Dim Movimiento = New ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.MovimientoProvisionEfectivo

            If Bancos IsNot Nothing AndAlso Bancos.Count > 0 Then
                If Bancos(0).Codigo.Contains("*") Then
                    Dim objPeticionCliente As New IAC.ContractoServicio.Cliente.GetClientes.Peticion
                    objPeticionCliente.OidCliente = Bancos.FirstOrDefault().Identificador
                    objPeticionCliente.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
                    objPeticionCliente.ParametrosPaginacion.RealizarPaginacion = False
                    Dim objRespuestaCliente As IAC.ContractoServicio.Cliente.GetClientes.Respuesta
                    objRespuestaCliente = Prosegur.Genesis.LogicaNegocio.Genesis.Cliente.ObtenerClientes(objPeticionCliente)

                    If objRespuestaCliente IsNot Nothing Then
                        Movimiento.CodigoBancoCapital = objRespuestaCliente.Clientes.FirstOrDefault().CodCliente
                    Else
                        Movimiento.CodigoBancoCapital = String.Empty
                    End If
                Else
                    Movimiento.CodigoBancoCapital = Bancos(0).Codigo
                End If
            End If

            If Not String.IsNullOrWhiteSpace(txtFechaGestion.Text) Then

                Movimiento.FechaHora = Convert.ToDateTime(txtFechaGestion.Text).DataHoraGMT(InformacionUsuario.DelegacionSeleccionada, True)
            End If


            InfAdicionales.GuardarDatos()
            If InfAdicionales.Terminos IsNot Nothing AndAlso InfAdicionales.Terminos.Count > 0 Then

                For Each termino As Clases.TerminoIAC In InfAdicionales.Terminos

                    If String.IsNullOrEmpty(termino.Valor) Then
                        Continue For
                    End If
                    If Movimiento.CamposExtras Is Nothing Then Movimiento.CamposExtras = New List(Of ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.CampoExtra)

                    Dim objTermino = New ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.CampoExtra
                    objTermino.Codigo = termino.Codigo
                    objTermino.Valor = termino.Valor

                    Movimiento.CamposExtras.Add(objTermino)
                Next

            End If

            For Each row As GridViewRow In grdDivisas.Rows


                Dim txtValor As TextBox = DirectCast(row.FindControl("txtValor"), TextBox)

                If String.IsNullOrWhiteSpace(txtValor.Text) OrElse txtValor.Text = 0 Then
                    Continue For
                End If

                If Movimiento.Importes Is Nothing Then Movimiento.Importes = New List(Of ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.Importe)

                Dim importe = New ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.Importe
                importe.Importe = txtValor.Text
                Dim hfIdentificador As HiddenField = DirectCast(row.FindControl("hfIdentificador"), HiddenField)
                importe.CodigoDivisa = DivisasDisponiveis.FirstOrDefault(Function(x) x.Identificador = hfIdentificador.Value).Divisa.CodigoISO
                Movimiento.Importes.Add(importe)

            Next

            peticion.Configuracion = New ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.Configuracion
            peticion.Configuracion.RespuestaDetallar = True
            peticion.CodigoPais = InformacionUsuario.DelegacionSeleccionada.CodigoPais
            peticion.Movimientos = New List(Of ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Entrada.MovimientoProvisionEfectivo)
            peticion.Movimientos.Add(Movimiento)

            Dim respuesta = Genesis.LogicaNegocio.Integracion.AccionAltaMovimientosProvisionEfectivo.Ejecutar(peticion)
            Dim msg = String.Empty
            Dim script = String.Empty
            If respuesta.Movimientos IsNot Nothing AndAlso respuesta.Movimientos(0).Detalles IsNot Nothing AndAlso respuesta.Movimientos(0).Detalles.Count > 0 Then
                For Each detal As ContractoServicio.Contractos.Integracion.AltaMovimientosProvisionEfectivo.Salida.Detalle In respuesta.Movimientos(0).Detalles
                    msg += detal.Descripcion + Environment.NewLine

                    If detal.Codigo = "0040110000" Then
                        msg = RecuperarValorDic("0010010000")
                        script = "window.location.href = " & Chr(34) & "../Consultas/ConsultaTransacciones.aspx" & Chr(34)
                    End If
                Next

                MyBase.MostraMensagemErroConScript(msg, script)

            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub
    Private Sub Acciones_onAccionCancelar()
        If Master.Historico.Count > 1 Then

            Dim pagina = Master.Historico(Master.Historico.Count - 2).Key
            Response.Redireccionar(pagina)
        End If

    End Sub


#End Region


End Class