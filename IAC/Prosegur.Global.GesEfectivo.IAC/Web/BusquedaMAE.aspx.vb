Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class BusquedaMAE
    Inherits Base

    <Serializable()>
    Class PuntoServicioGrid
        Inherits ContractoServicio.Paginacion.PeticionPaginacionBase

        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property desCliente As String
        Public Property oidCodigoAjenoCliente As String
        Public Property codCodigoAjenoCliente As String
        Public Property desCodigoAjenoCliente As String
        Public Property bolDefectoCodigoAjenoCliente As Boolean
        Public Property oidSubcliente As String
        Public Property codSubcliente As String
        Public Property desSubcliente As String
        Public Property oidCodigoAjenoSubcliente As String
        Public Property codCodigoAjenoSubcliente As String
        Public Property desCodigoAjenoSubcliente As String
        Public Property bolDefectoCodigoAjenoSubcliente As Boolean
        Public Property oidPtoServicio As String
        Public Property codPtoServicio As String
        Public Property desPtoServicio As String
        Public Property oidCodigoAjenoPtoServicio As String
        Public Property codCodigoAjenoPtoServicio As String
        Public Property desCodigoAjenoPtoServicio As String
        Public Property bolDefectoCodigoAjenoPuntoServicio As Boolean
        Public Property oidSectorMAE As String
        Public Property codSectorMAE As String
        Public Property completo As Boolean
        Public Property desplazado As Boolean

        Public ReadOnly Property cliente As String
            Get
                Return codCliente + " " + desCliente
            End Get
        End Property

        Public ReadOnly Property subCliente As String
            Get
                Return codSubcliente + " " + desSubcliente
            End Get
        End Property

        Public ReadOnly Property PtoServicio As String
            Get
                Return codPtoServicio + " " + desPtoServicio
            End Get
        End Property

        Public Sub New()
            MyBase.ParametrosPaginacion.RealizarPaginacion = False
        End Sub

        Public Property PorcComisionCliente As Nullable(Of Decimal)
        Public ReadOnly Property PorcComisionClienteText As String
            Get
                Return PorcComisionCliente.ToString()
            End Get
        End Property

        Public Property PorcComisionMaquina As Nullable(Of Decimal)
        Public ReadOnly Property PorcComisionMaquinaText As String
            Get
                Return PorcComisionMaquina.ToString()
            End Get
        End Property

        Public Property PorcComisionPlanificacion As Nullable(Of Decimal)
        Public ReadOnly Property PorcComisionPlanificacionText As String
            Get
                Return PorcComisionPlanificacion.ToString()
            End Get
        End Property
    End Class

#Region "[PROPS/VARS]"

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

    Private ReadOnly Property Modo() As String
        Get
            Return Request.QueryString("modo")
        End Get
    End Property
    Private ReadOnly Property EsModal() As Boolean
        Get
            Return Modo = "modal"
        End Get
    End Property
    Private ReadOnly Property AcaoCodigo() As String
        Get
            Return Request.QueryString("AcaoCodigo")
        End Get
    End Property

    Private Property Parametros() As List(Of Comon.Clases.Parametro)
        Get
            Return ViewState("Parametros")
        End Get
        Set(value As List(Of Comon.Clases.Parametro))
            ViewState("Parametros") = value
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

    Private Property puntosServicioGrid As List(Of PuntoServicioGrid)
        Get
            Return Session("puntosServicioGrid")
        End Get
        Set(value As List(Of PuntoServicioGrid))
            Session("puntosServicioGrid") = value
        End Set
    End Property


    Private Property listaUCAtualiza As List(Of String)
        Get
            Return Session("listaUCAtualiza")
        End Get
        Set(value As List(Of String))
            Session("listaUCAtualiza") = value
        End Set
    End Property



    Private Property Planificacion As Comon.Clases.Planificacion
        Get
            Return ViewState("Planificacion")
        End Get
        Set(value As Comon.Clases.Planificacion)
            ViewState("Planificacion") = value
        End Set
    End Property

    Private Property TiposPlanifiacion As List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion)
        Get
            Return DirectCast(ViewState("TiposPlanifiacion"), List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion))
        End Get
        Set(value As List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion))
            ViewState("TiposPlanifiacion") = value
        End Set
    End Property


    Private Property HandlerUCBanco As Boolean
        Get
            Return ViewState("HandlerUCBanco")
        End Get
        Set(value As Boolean)
            ViewState("HandlerUCBanco") = value
        End Set
    End Property

    Private Property DatosBancarios As Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
        Get
            Return ViewState("DatosBancarios")
        End Get
        Set(value As Dictionary(Of String, List(Of Comon.Clases.DatoBancario)))
            ViewState("DatosBancarios") = value
        End Set
    End Property
    Private Property PeticionesDatoBancario As Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
        Get
            Return Session("PeticionesDatoBancario")
        End Get
        Set(value As Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion))
            Session("PeticionesDatoBancario") = value
        End Set
    End Property

    Private Property Maquina As IAC.ContractoServicio.Maquina.GetMaquinaDetalle.Maquina
        Get
            Return DirectCast(ViewState("Maquina"), IAC.ContractoServicio.Maquina.GetMaquinaDetalle.Maquina)
        End Get
        Set(value As IAC.ContractoServicio.Maquina.GetMaquinaDetalle.Maquina)
            ViewState("Maquina") = value
        End Set
    End Property

    Private Property puntosServicioGridMaquina As List(Of PuntoServicioGrid)
        Get
            Return ViewState("puntosServicioGridMaquina")
        End Get
        Set(value As List(Of PuntoServicioGrid))
            ViewState("puntosServicioGridMaquina") = value
        End Set
    End Property

    Private Property MaquinasResultado As List(Of IAC.ContractoServicio.Maquina.GetMaquina.Maquina)
        Get
            Return DirectCast(ViewState("MaquinasResultado"), List(Of IAC.ContractoServicio.Maquina.GetMaquina.Maquina))
        End Get
        Set(value As List(Of IAC.ContractoServicio.Maquina.GetMaquina.Maquina))
            ViewState("MaquinasResultado") = value
        End Set
    End Property

    Private Property MaquinaPuntoServicio As Comon.Clases.Maquina
        Get
            Return DirectCast(ViewState("MaquinaPuntoServicio"), Comon.Clases.Maquina)
        End Get
        Set(value As Comon.Clases.Maquina)
            ViewState("MaquinaPuntoServicio") = value
        End Set
    End Property

    Private Property PodeModificar As Boolean
        Get
            Return ViewState("PodeModificar")
        End Get
        Set(value As Boolean)
            ViewState("PodeModificar") = value
        End Set
    End Property

    Private Property PodeConsultar As Boolean
        Get
            Return ViewState("PodeConsultar")
        End Get
        Set(value As Boolean)
            ViewState("PodeConsultar") = value
        End Set
    End Property

    Private Property PodeDeletar As Boolean
        Get
            Return ViewState("PodeDeletar")
        End Get
        Set(value As Boolean)
            ViewState("PodeDeletar") = value
        End Set
    End Property

    Private Property OidMaquina As String
        Get
            Return ViewState("OidMaquina")
        End Get
        Set(value As String)
            ViewState("OidMaquina") = value
        End Set
    End Property
    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property


    Public ReadOnly Property identificadorSectorMae() As String
        Get
            If puntosServicioGrid IsNot Nothing AndAlso puntosServicioGrid.Count > 0 Then
                Return puntosServicioGrid.FirstOrDefault.oidSectorMAE
            End If
            Return String.Empty
        End Get
    End Property

    Public ReadOnly Property codigoSectorMae() As String
        Get
            If puntosServicioGrid IsNot Nothing AndAlso puntosServicioGrid.Count > 0 Then
                Return puntosServicioGrid.FirstOrDefault.codSectorMAE
            End If
            Return String.Empty
        End Get
    End Property

    Private Property PlanesMAExCanalesSubCanalesPuntos As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
        Get
            If Session("PlanesMAExCanalesSubCanalesPuntos") Is Nothing Then
                Session("PlanesMAExCanalesSubCanalesPuntos") = New ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
            End If
            Return DirectCast(Session("PlanesMAExCanalesSubCanalesPuntos"), ObservableCollection(Of PlanMaqPorCanalSubCanalPunto))

        End Get
        Set(value As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto))
            Session("PlanesMAExCanalesSubCanalesPuntos") = value
        End Set
    End Property

#End Region

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


    Private WithEvents _ucClientesTesoreria As ucCliente
    Public Property ucClientesTesoreria() As ucCliente
        Get
            If _ucClientesTesoreria Is Nothing Then
                _ucClientesTesoreria = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesTesoreria.ID = Me.ID & "_ucClientesTesoreria"
                AddHandler _ucClientesTesoreria.Erro, AddressOf ErroControles
                phClientesTesoreria.Controls.Add(_ucClientesTesoreria)
            End If
            Return _ucClientesTesoreria
        End Get
        Set(value As ucCliente)
            _ucClientesTesoreria = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClientesTesoreria()

        Me.ucClientesTesoreria.EsBancoCapital = True

        Me.ucClientesTesoreria.SelecaoMultipla = False
        Me.ucClientesTesoreria.ClienteHabilitado = False
        Me.ucClientesTesoreria.ClienteObrigatorio = False

        Me.ucClientesTesoreria.SubClienteHabilitado = True
        Me.ucClientesTesoreria.SubClienteObrigatorio = True
        Me.ucClientesTesoreria.ucSubCliente.MultiSelecao = False
        Me.ucClientesTesoreria.SubClienteTitulo = MyBase.RecuperarValorDic("lblBancoTesoreria")

        Me.ucClientesTesoreria.PtoServicioHabilitado = True
        Me.ucClientesTesoreria.PtoServicoObrigatorio = True
        Me.ucClientesTesoreria.ucPtoServicio.MultiSelecao = False
        Me.ucClientesTesoreria.PtoServicioTitulo = MyBase.RecuperarValorDic("lblCuentaTesoreria")



        If Bancos IsNot Nothing Then
            'Me.ucClientesTesoreria.Clientes = Bancos
        End If


    End Sub

    Private Sub ucClientesTesoreria_OnControleAtualizado() Handles _ucClientesTesoreria.UpdatedControl
        Try
            If ucClientesTesoreria.Clientes IsNot Nothing Then
                ClientesTesoreria = ucClientesTesoreria.Clientes
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
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

#End Region

#Region "[HelpersCliente-PtoServicio]"

    Public Property ClientesPtoServ As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesPtoServ.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesPtoServ.Clientes = value
        End Set
    End Property



    Public Property ClientesPtoServMono As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientesPtoServMono.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesPtoServMono.Clientes = value
        End Set
    End Property


    Private WithEvents _ucClientesPtoServ As ucCliente
    Public Property ucClientesPtoServ() As ucCliente
        Get
            If _ucClientesPtoServ Is Nothing Then
                _ucClientesPtoServ = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesPtoServ.ID = Me.ID & "_ucClientesPtoServ"
                AddHandler _ucClientesPtoServ.Erro, AddressOf ErroControles
                phClientesPtoServ.Controls.Add(_ucClientesPtoServ)
            End If
            Return _ucClientesPtoServ
        End Get
        Set(value As ucCliente)
            _ucClientesPtoServ = value
        End Set
    End Property


    Private WithEvents _ucClientesPtoServMono As ucCliente
    Public Property ucClientesPtoServMono() As ucCliente
        Get
            If _ucClientesPtoServMono Is Nothing Then
                _ucClientesPtoServMono = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientesPtoServMono.ID = Me.ID & "_ucClientesPtoServMono"
                AddHandler _ucClientesPtoServMono.Erro, AddressOf ErroControles
                phClientesPtoServMono.Controls.Add(_ucClientesPtoServMono)
            End If
            Return _ucClientesPtoServMono
        End Get
        Set(value As ucCliente)
            _ucClientesPtoServMono = value
        End Set
    End Property

    Private Sub ConfigurarControle_ClientePtoServ()

        Me.ucClientesPtoServ.SelecaoMultipla = False
        Me.ucClientesPtoServ.ClienteHabilitado = True
        Me.ucClientesPtoServ.ClienteObrigatorio = True

        Me.ucClientesPtoServ.SubClienteHabilitado = True
        Me.ucClientesPtoServ.SubClienteObrigatorio = True
        Me.ucClientesPtoServ.ucSubCliente.MultiSelecao = False

        Me.ucClientesPtoServ.PtoServicioHabilitado = True
        Me.ucClientesPtoServ.PtoServicoObrigatorio = True
        Me.ucClientesPtoServ.ucPtoServicio.MultiSelecao = False


        Me.ucClientesPtoServMono.SelecaoMultipla = False
        Me.ucClientesPtoServMono.ClienteHabilitado = True
        Me.ucClientesPtoServMono.ClienteObrigatorio = True

        Me.ucClientesPtoServMono.SubClienteHabilitado = True
        Me.ucClientesPtoServMono.SubClienteObrigatorio = True
        Me.ucClientesPtoServMono.ucSubCliente.MultiSelecao = False

        Me.ucClientesPtoServMono.PtoServicioHabilitado = True
        Me.ucClientesPtoServMono.PtoServicoObrigatorio = True
        Me.ucClientesPtoServMono.ucPtoServicio.MultiSelecao = False

        If ClientesPtoServ IsNot Nothing Then
            Me.ucClientesPtoServ.Clientes = ClientesPtoServ
        End If

        If ClientesPtoServMono IsNot Nothing Then
            Me.ucClientesPtoServMono.Clientes = ClientesPtoServMono
        End If

    End Sub




    Private Sub ucClientesPtoServ_OnControleAtualizado() Handles _ucClientesPtoServ.UpdatedControl
        Try
            If ucClientesPtoServ.Clientes IsNot Nothing Then
                ClientesPtoServ = ucClientesPtoServ.Clientes
            End If



        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub



    Private Sub ucClientesPtoServMono_OnControleAtualizado() Handles _ucClientesPtoServMono.UpdatedControl
        Try
            If ucClientesPtoServMono.Clientes IsNot Nothing Then
                ClientesPtoServMono = ucClientesPtoServMono.Clientes
            End If


            If ucClientesPtoServMono.Clientes IsNot Nothing AndAlso
              ucClientesPtoServMono.Clientes.FirstOrDefault() IsNot Nothing AndAlso
              ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso
              ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault() IsNot Nothing AndAlso
              ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso
              ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then


                Dim objPuntoServicio = ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.First
                MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPuntoServicio.Identificador)

                If MaquinaPuntoServicio IsNot Nothing Then
                    If ValidaSaldoPtoServicio(ucClientesPtoServMono.Clientes.FirstOrDefault().Identificador,
                                                        ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador,
                                                        ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Identificador,
                                                        MaquinaPuntoServicio.Sector.Identificador,
                                                        MaquinaPuntoServicio.Delegacion.Plantas.First.Identificador, False) Then

                        divDesplazado.Visible = False
                    Else

                        divDesplazado.Visible = True
                    End If


                End If

                If BuscarClienteECompleto(ucClientesPtoServMono.Clientes.FirstOrDefault()) Then
                    imgCodigoAjenoMono.ImageUrl = "~/Imagenes/contain01.png"
                    'CType(e.Row.Cells(3).FindControl("imgCodigoAjeno"), ImageButton).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    imgCodigoAjenoMono.ImageUrl = "~/Imagenes/nocontain01.png"
                    'CType(e.Row.Cells(3).FindControl("imgCodigoAjeno"), ImageButton).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
                imgCodigoAjenoMono.Enabled = True

            Else
                imgCodigoAjenoMono.ImageUrl = "~/Imagenes/contain_disabled.png"
                imgCodigoAjenoMono.Enabled = False
                divDesplazado.Visible = False
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[HelpersBanco]"
    Public Property ClientesTesoreria As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get

            Return ucClientesTesoreria.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientesTesoreria.Clientes = value
        End Set
    End Property

    Public Property Bancos As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucBanco.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucBanco.Clientes = value
        End Set
    End Property


    Private _bancosSelecionados As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
    Public Property bancosSelecionados As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            If _bancosSelecionados Is Nothing Then
                _bancosSelecionados = New ObservableCollection(Of Cliente)
            End If
            Return _bancosSelecionados
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            _bancosSelecionados = value
        End Set
    End Property



    Private WithEvents _ucBanco As ucCliente
    Public Property ucBanco() As ucCliente
        Get
            If _ucBanco Is Nothing Then
                _ucBanco = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucBanco.ID = Me.ID & "_ucBancoMAE"

                AddHandler _ucBanco.Erro, AddressOf ErroControles
                phBanco.Controls.Add(_ucBanco)


            End If
            Return _ucBanco
        End Get
        Set(value As ucCliente)
            _ucBanco = value
        End Set
    End Property

    Private Sub ConfigurarControle_Banco()

        Me.ucBanco.SelecaoMultipla = False
        Me.ucBanco.ClienteHabilitado = True
        Me.ucBanco.ClienteObrigatorio = False
        Me.ucBanco.SubClienteHabilitado = False
        Me.ucBanco.SubClienteObrigatorio = False
        Me.ucBanco.ucSubCliente.MultiSelecao = False
        Me.ucBanco.TipoPlanificacion = ddlTipoPlanificacion.Text
        Me.ucBanco.TipoBanco = True
        If Bancos IsNot Nothing Then
            Me.ucBanco.Clientes = Bancos
            BancoToTesoreria(Bancos)

            If Planificacion IsNot Nothing Then
                BuscarFacturacion(Planificacion.Identificador)
                CargaPlanificacionPatron(Planificacion)
            End If
        End If

    End Sub

    Private Sub BancoToTesoreria(ClienteOrigem As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))

        Dim oidClienteOrigemSelecionado As String = String.Empty
        Dim oidTesoreriaSelecionado As String = String.Empty


        If ClienteOrigem.Count > 0 Then
            oidClienteOrigemSelecionado = ClienteOrigem.FirstOrDefault().Identificador
        End If

        If ClientesTesoreria.Count > 0 Then
            oidTesoreriaSelecionado = ClientesTesoreria.FirstOrDefault().Identificador
        End If

        If oidClienteOrigemSelecionado <> oidTesoreriaSelecionado Then
            If ClienteOrigem.Count > 0 Then
                ClientesTesoreria.Add(ClienteOrigem.First().Clonar())

                AddLista("ucClientesTesoreria")
            End If
            'AtualizaDadosHelperCliente(ClientesTesoreria, ucClientesTesoreria)
        End If



    End Sub

    Public Sub ucBanco_OnControleAtualizado(sender As Object) Handles _ucBanco.UpdatedControl

        Try

            '  VERIFICAR Planificacion ADD
            If ucBanco.Clientes IsNot Nothing Then

                Bancos = ucBanco.Clientes

                If ucBanco.Clientes.Count > 0 Then
                    ' If Bancos.Count = 0 OrElse ucBanco.Clientes.FirstOrDefault().Identificador <> Bancos.FirstOrDefault().Identificador Then

                    Planificacion = New Comon.Clases.Planificacion
                    AddPlanificacion(ucBanco.Clientes)
                    'End If

                    If ucClientesTesoreria.Clientes.FirstOrDefault(Function(x) x.Identificador = ucBanco.Clientes.FirstOrDefault().Identificador) Is Nothing Then

                        BancoToTesoreria(ucBanco.Clientes)

                    End If

                    btnTransacciones.Enabled = True
                Else
                    LimpiaPlanificacion()
                End If

                BuscarFacturacion(Planificacion.Identificador)
                CargaPlanificacionPatron(Planificacion)
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub AtualizaBancoSelecionado(bancosSelecionados As ObservableCollection(Of Comon.Clases.Cliente))
        Me.bancosSelecionados = bancosSelecionados
        RemoveHandler ucBanco.UpdatedControl, AddressOf ucBanco_OnControleAtualizado
        AtualizaDadosHelperCliente(bancosSelecionados, ucBanco)
        'AtualizaDadosHelperCliente(ClientesTesoreria, ucClientesTesoreria)
        AddHandler ucBanco.UpdatedControl, AddressOf ucBanco_OnControleAtualizado
    End Sub

    Private Sub btnConsomePlanificacion_Click(sender As Object, e As EventArgs) Handles btnConsomePlanificacion.Click
        Try
            If Session("objBusquedaPlanificacion") IsNot Nothing Then

                Dim tablaGenesis As BusquedaPlanificacionPopup.Planificacion = Session("objBusquedaPlanificacion")
                If tablaGenesis IsNot Nothing Then

                    Planificacion = tablaGenesis.Planificacion
                    AtualizaBancoSelecionado(tablaGenesis.BancoSelecionados)

                    'Se vuelve a asignar el resultado de Session("objBusquedaPlanificacion") en tablaGenesis por probable cambio en BusquedaPlanificacionPopup
                    tablaGenesis = Session("objBusquedaPlanificacion")

                    If tablaGenesis.Planificacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(tablaGenesis.Planificacion.Descripcion) Then
                        BuscarFacturacion(Planificacion.Identificador)
                        CargaPlanificacionPatron(Planificacion)
                        'tablaGenesis.Planificacion.Delegacion.
                        HoraDelegacion(tablaGenesis.Planificacion)
                        txtPlanificacion.Text = tablaGenesis.Planificacion.Descripcion.ToUpper()

                        If Maquina IsNot Nothing AndAlso Planificacion.Codigo = "FECHA_VALOR_CONFIR" Then
                            If Maquina.Planificacion.Identificador <> Planificacion.Identificador Then
                                txtFechaInicio.Enabled = True
                            Else
                                txtFechaInicio.Enabled = False
                            End If
                        End If


                        chkVigentePlan.Checked = tablaGenesis.Planificacion.BolActivo
                        chkControlaFacturacion.Checked = tablaGenesis.Planificacion.BolControlaFacturacion

                        'Carga limites de la planificacion
                        ucLimitePlanificacion.Planificacion = tablaGenesis.Planificacion
                        ucLimitePlanificacion.CargaRegistrosDeBase()
                    Else
                        LimpiaPlanificacion()
                    End If
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub LimpiaPlanificacion()
        Planificacion = New Comon.Clases.Planificacion
        txtPlanificacion.Text = String.Empty
        chkVigentePlan.Checked = False
        chkControlaFacturacion.Checked = False
        txtBancoTesoreriaPadron.Text = String.Empty
        txtCuentaTesoreriaPadron.Text = String.Empty

        LimpiarUcLimitePlanificacion()
        ucClientesTesoreria.Clientes.Clear()

        AddLista("ucClientesTesoreria")

        btnTransacciones.Enabled = False
    End Sub

    Private Sub CargaPlanificacionPatron(planificacion As Planificacion)
        lboxCanalPatron.Items.Clear()
        lboxSubCanalPatron.Items.Clear()

        If planificacion.Canales IsNot Nothing AndAlso planificacion.Canales.Count > 0 Then
            lboxCanalPatron.DataSource = planificacion.Canales.Select(Function(x) x.Codigo + " - " + x.Descripcion).ToList()

            lboxCanalPatron.DataBind()
            lboxSubCanalPatron.DataSource = DevolverSubCanalesPatron(planificacion.Canales)
            lboxSubCanalPatron.DataBind()
        End If
    End Sub

#End Region


#Region "[HelpersPuntoServicio]"
    Public Property PuntoServicios As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio)
        Get
            Return ucPuntoServicio.PuntoServicios
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio))
            ucPuntoServicio.PuntoServicios = value
        End Set
    End Property

    Private WithEvents _ucPuntoServicio As ucPuntoServicio
    Public Property ucPuntoServicio() As ucPuntoServicio
        Get
            If _ucPuntoServicio Is Nothing Then
                _ucPuntoServicio = LoadControl(ResolveUrl("~\Controles\Helpers\ucPuntoServicio.ascx"))
                _ucPuntoServicio.ID = Me.ID & "_ucPuntoServicioPlan"
                AddHandler _ucPuntoServicio.Erro, AddressOf ErroControles
                phPuntoServicio.Controls.Add(_ucPuntoServicio)
            End If
            Return _ucPuntoServicio
        End Get
        Set(value As ucPuntoServicio)
            _ucPuntoServicio = value
        End Set
    End Property

    Private Sub ConfigurarControle_PuntoServicio()

        Me.ucPuntoServicio.SelecaoMultipla = True

        Me.ucPuntoServicio.SelecaoMultipla = True
        Me.ucPuntoServicio.PuntoServicioHabilitado = True
        Me.ucPuntoServicio.OidPuntosFiltro.Clear()

        Dim tmpClientes As ObservableCollection(Of Cliente)
        If (Me.chkMultiClientes.Checked) Then
            tmpClientes = ucClientesPtoServ.Clientes
        Else
            tmpClientes = ucClientesPtoServMono.Clientes
        End If
        If tmpClientes IsNot Nothing Then
            For Each objCli In tmpClientes
                If objCli.SubClientes IsNot Nothing Then
                    For Each objSubCli In objCli.SubClientes
                        If objSubCli.PuntosServicio IsNot Nothing Then
                            For Each objPtoServ In objSubCli.PuntosServicio
                                Me.ucPuntoServicio.OidPuntosFiltro.Add(objPtoServ.Identificador)
                            Next
                        End If
                    Next
                End If
            Next
        End If

        If puntosServicioGrid IsNot Nothing Then
            For Each objCanal In puntosServicioGrid
                Me.ucPuntoServicio.OidPuntosFiltro.Add(objCanal.oidPtoServicio)
            Next
        End If

        ' Me.ucCanales.TipoBanco = True

        If PuntoServicios IsNot Nothing Then
            Me.ucPuntoServicio.PuntoServicios = PuntoServicios
        End If

    End Sub


#End Region

#Region "[HelpersCanales]"
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
                _ucCanales.ID = Me.ID & "_ucCanalesPlanMAE"
                AddHandler _ucCanales.Erro, AddressOf ErroControles
                phCanal.Controls.Add(_ucCanales)
            End If
            Return _ucCanales
        End Get
        Set(value As ucCanal)
            _ucCanales = value
        End Set
    End Property

    Private Sub ConfigurarControle_Canal()

        Me.ucCanales.SelecaoMultipla = True
        Me.ucCanales.CanalHabilitado = True
        ' Me.ucCanales.CanalObrigatorio = False
        Me.ucCanales.SubCanalHabilitado = True
        Me.ucCanales.ucSubCanal.MultiSelecao = True

        ' Me.ucCanales.TipoBanco = True

        If Canales IsNot Nothing Then
            Me.ucCanales.Canales = Canales
        End If

    End Sub


#End Region

#Region "ucLimiteMae"
    Private WithEvents _ucLimiteMae As ucLimite
    Public Property ucLimiteMae() As ucLimite
        Get
            If _ucLimiteMae Is Nothing Then
                _ucLimiteMae = LoadControl(ResolveUrl("~\Controles\ucLimite.ascx"))
                _ucLimiteMae.ID = Me.ID & "_ucLimiteMae"
                phLimiteMae.Controls.Add(_ucLimiteMae)
            End If
            Return _ucLimiteMae
        End Get
        Set(value As ucLimite)
            _ucLimiteMae = value
        End Set
    End Property

    Public Sub LimpiarUcLimiteMae()
        ucLimiteMae.Maquina = Nothing
        ucLimiteMae.PuntoServicio = Nothing
        ucLimiteMae.CargaRegistrosDeBase()
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
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        Try
            MyBase.AdicionarScripts()
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}',0);", txtFechaInicio.ClientID, "True")
            script &= String.Format("AbrirCalendario('{0}','{1}',0);", txtFechaFin.ClientID, "True")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            txtPorComisionCliente.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal4(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
            ' txtPorComisionCliente.Attributes.Add("onkeypress", "bloqueialetras(event,this);")


            txtPorComisionMaquina.Attributes.Add("onblur", String.Format("VerificarNumeroDecimal4(this,'{0}','{1}');", MyBase._DecimalSeparador, MyBase._MilharSeparador))
            'txtPorComisionMaquina.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MAE
        ' desativar validação de ação
        MyBase.ValidarAcao = True
        MyBase.CodFuncionalidad = "ABM_MAE"
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda MAE")
            ASPxGridView.RegisterBaseScript(Page)

            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = True
            Master.HabilitarMenu = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True
            Master.MenuGrande = True

            If Not Page.IsPostBack Then


                Clientes = Nothing
                ClientesPtoServ = Nothing
                ClientesPtoServMono = Nothing
                Bancos = Nothing
                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                PreencherddlPlanta(Nothing, True, True)
                PreencherddlDelegacion(True, True)
                PreencherddlMarca(True, True)
                PreencherddlPlanificacao()

                ddlDelegacion.Focus()

                If Not MyBase.ValidarAcaoPagina(MyBase.PaginaAtual, Utilidad.eAcao.Alta) Then
                    btnNovo.Enabled = False
                    btnNovo.Visible = False
                    btnTransacciones.Enabled = False
                End If


                PodeModificar = True
                PodeConsultar = True
                PodeDeletar = True
                If Not MyBase.ValidarAcaoPagina(MyBase.PaginaAtual, Utilidad.eAcao.Modificacion) Then
                    PodeModificar = False
                    btnTransacciones.Enabled = True
                End If

                If Not MyBase.ValidarAcaoPagina(MyBase.PaginaAtual, Utilidad.eAcao.Baja) Then
                    PodeDeletar = False
                End If

                If Not MyBase.ValidarAcaoPagina(MyBase.PaginaAtual, Utilidad.eAcao.Consulta) Then
                    PodeConsultar = False
                End If

                CargarParametros()

            End If

            'Limite
            Me.ucLimiteMae.ConfigurarControles()
            Me.ucLimitePlanificacion.Modo = Comon.Enumeradores.Modo.Consulta
            Me.ucLimitePlanificacion.ConfigurarControles()

            ConfigurarControle_Cliente()
            ConfigurarControle_ClientePtoServ()
            ConfigurarControle_Banco()
            ConfigurarControle_ClientesTesoreria()
            ConfigurarControle_PuntoServicio()


            ConfigurarControle_Canal()


            If Modo = "modal" Then
                UpdatePanelGeralFiltro.Visible = False
                Master.MostrarCabecalho = False
                If Not Page.IsPostBack Then
                    hiddenCodigo.Value = Request.QueryString("oidMaquina")
                    If AcaoCodigo = "4" Then
                        imgEditar_OnClick(Nothing, Nothing)
                    Else
                        imgConsultar_OnClick(Nothing, Nothing)
                    End If
                End If

                btnGrabar.Visible = True
                btnNovo.Visible = False
                btnBajaConfirm.Visible = False


            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function DevolverSubCanalesPatron(lst As List(Of Comon.Clases.Canal)) As List(Of String)
        Dim result As List(Of String) = New List(Of String)

        For Each objCanal In lst

            If objCanal.SubCanales IsNot Nothing And objCanal.SubCanales.Count > 0 Then

                result.AddRange(objCanal.SubCanales.Where(Function(x) Not String.IsNullOrWhiteSpace(x.Codigo)).Select(Function(x) x.Codigo + " - " + x.Descripcion).ToList())
            End If
        Next
        Return result


    End Function


    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        Try
            Master.Titulo = MyBase.RecuperarValorDic("lbl_titulo_busqueda")
            lblDeviceID.Text = MyBase.RecuperarValorDic("lbl_DeviceID")
            lblDescripcion.Text = MyBase.RecuperarValorDic("lbl_Descripcion")
            lblPlanta.Text = MyBase.RecuperarValorDic("lbl_planta")
            lblSubTitulosCriteriosBusqueda.Text = Tradutor.Traduzir("gen_lbl_CriterioBusca")
            lblSubTituloMAE.Text = MyBase.RecuperarValorDic("lbl_tiulo_Resultados")
            lblDelegacion.Text = MyBase.RecuperarValorDic("lbl_delegacion")
            lblMarca.Text = MyBase.RecuperarValorDic("lbl_marca")
            lblModelo.Text = MyBase.RecuperarValorDic("lbl_modelo")
            lblTipoPlanificacion.Text = MyBase.RecuperarValorDic("lbl_tipo_planificacion")

            'Botoes
            btnBuscar.Text = Tradutor.Traduzir("btnBuscar")
            btnLimpar.Text = Tradutor.Traduzir("btnLimpiar")
            btnBuscar.ToolTip = Tradutor.Traduzir("btnBuscar")
            btnLimpar.ToolTip = Tradutor.Traduzir("btnLimpiar")
            btnCancelar.Text = Tradutor.Traduzir("btnCancelar")
            btnCancelar.ToolTip = Tradutor.Traduzir("btnCancelar")
            btnCancelarPlanificacionMAE.ToolTip = Tradutor.Traduzir("btnCancelar")
            btnCancelarPlanificacionMAE.Text = Tradutor.Traduzir("btnCancelar")
            btnAgregarPlanificacionMAE.ToolTip = Tradutor.Traduzir("btnAnadir")
            btnAgregarPlanificacionMAE.Text = Tradutor.Traduzir("btnAnadir")
            btnNovo.Text = Tradutor.Traduzir("btnAlta")
            btnNovo.ToolTip = Tradutor.Traduzir("btnAlta")
            btnGrabar.Text = Tradutor.Traduzir("btnGrabar")
            btnGrabar.ToolTip = Tradutor.Traduzir("btnGrabar")
            btnBajaConfirm.Text = Tradutor.Traduzir("btnBaja")
            btnBajaConfirm.ToolTip = Tradutor.Traduzir("btnBaja")
            btnAddPtoServ.Text = Tradutor.Traduzir("btnAnadir")
            btnAddPlanificacion.Text = MyBase.RecuperarValorDic("btnAnadirPlanificacion")
            btnTransacciones.Text = Tradutor.Traduzir("029_btn_transaciones")
            btnAjeno.Text = Traduzir("btnCodigoAjeno")
            btnAjeno.ToolTip = Traduzir("btnCodigoAjeno")



            'Grid
            GdvResultado.Columns(2).HeaderText = MyBase.RecuperarValorDic("lbl_grd_cliente")
            GdvResultado.Columns(4).HeaderText = MyBase.RecuperarValorDic("lbl_grd_subcliente")
            GdvResultado.Columns(6).HeaderText = MyBase.RecuperarValorDic("lbl_grd_ptoservicio")
            GdvResultado.Columns(7).HeaderText = MyBase.RecuperarValorDic("lbl_grd_deviceID")
            GdvResultado.Columns(8).HeaderText = MyBase.RecuperarValorDic("lbl_grd_descripcion")
            GdvResultado.Columns(9).HeaderText = MyBase.RecuperarValorDic("lbl_gr_vigente")
            GdvResultado.Columns(10).HeaderText = MyBase.RecuperarValorDic("lbl_gr_fechavalor")
            GdvResultado.Columns(11).HeaderText = MyBase.RecuperarValorDic("lbl_grd_planificacion")

            'Grid Pto Servicio
            grid.Columns(0).Caption = MyBase.RecuperarValorDic("lbl_grd_cliente")
            grid.Columns(1).Caption = MyBase.RecuperarValorDic("lblPorComisionCliente")
            grid.Columns(2).Caption = MyBase.RecuperarValorDic("lbl_grd_subcliente")
            grid.Columns(3).Caption = MyBase.RecuperarValorDic("lbl_grd_ptoservicio")
            grid.Columns(4).Caption = MyBase.RecuperarValorDic("lbl_grd_codajeno")
            grid.Columns(5).Caption = MyBase.RecuperarValorDic("lbl_grd_camposext")

            'Formulario
            lblTituloForm.Text = MyBase.RecuperarValorDic("lbl_tit_pantalla_form")
            lblDelegacionForm.Text = MyBase.RecuperarValorDic("lbl_delegacion")
            lblPlantaForm.Text = MyBase.RecuperarValorDic("lbl_planta")
            lblDeviceIDForm.Text = MyBase.RecuperarValorDic("lbl_DeviceID")
            lblDescripcionForm.Text = MyBase.RecuperarValorDic("lbl_Descripcion")
            lblMarcaForm.Text = MyBase.RecuperarValorDic("lbl_marca")
            lblModeloForm.Text = MyBase.RecuperarValorDic("lbl_modelo")
            lblVigenteForm.Text = MyBase.RecuperarValorDic("lbl_vigente")
            lblConsideraRecuentos.Text = MyBase.RecuperarValorDic("lbl_ConsideraRecuentos")
            lblMultiClientes.Text = MyBase.RecuperarValorDic("lblMultiClientes")
            lblFechaInicio.Text = MyBase.RecuperarValorDic("lbl_fecha_inicio")
            lblFechaFin.Text = MyBase.RecuperarValorDic("lbl_fecha_fin")
            lblTituloPtoServ.Text = MyBase.RecuperarValorDic("lbl_tit_pto_serv")
            lblPlanificacion.Text = MyBase.RecuperarValorDic("lbl_planificacion")
            ucBanco.ClienteTitulo = MyBase.RecuperarValorDic("lbl_banco")
            lblTituloPlanificacion.Text = MyBase.RecuperarValorDic("lbl_planificacion")
            csvPlantaForm.ErrorMessage = MyBase.RecuperarValorDic("msg_planta_obrigatorio")
            csvDelegacionForm.ErrorMessage = MyBase.RecuperarValorDic("msg_delegacion_obrigatorio")
            csvDescricaoForm.ErrorMessage = MyBase.RecuperarValorDic("msg_descricao_obrigatorio")
            csvMarcaForm.ErrorMessage = MyBase.RecuperarValorDic("msg_marca_obrigatorio")
            csvModeloForm.ErrorMessage = MyBase.RecuperarValorDic("msg_modelo_obrigatorio")
            csvFechaInicio.ErrorMessage = MyBase.RecuperarValorDic("msg_fyh_ini_obrigatorio")
            csvAddPtoServ.ErrorMessage = MyBase.RecuperarValorDic("msg_pto_serv_obrigatorio")
            csvAddPlanificacion.ErrorMessage = MyBase.RecuperarValorDic("msg_planificacion_obrigatorio")
            lblVigentePlan.Text = MyBase.RecuperarValorDic("lbl_vigente")
            lblDesplazado.Text = MyBase.RecuperarValorDic("lblDesplazado")

            lblFacturacion.Text = MyBase.RecuperarValorDic("lblFacturacion")
            lblBancoTesoreriaPadron.Text = MyBase.RecuperarValorDic("lblBancoTesoreriaPadron")
            lblCuentaTesoreriaPadron.Text = MyBase.RecuperarValorDic("lblCuentaTesoreriaPadron")

            lblPorComisionMaquina.Text = MyBase.RecuperarValorDic("lblPorComisionMaquina")
            lblPorComisionPlanificacion.Text = MyBase.RecuperarValorDic("lblPorComisionPlanificacion")
            lblPorComisionCliente.Text = MyBase.RecuperarValorDic("lblPorComisionCliente")

            'Limite
            lblTituloLimiteMae.Text = MyBase.RecuperarValorDic("lblLimites")
            lblTituloLimitePlanificacion.Text = MyBase.RecuperarValorDic("lblPlanificacionLimite")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub CargarParametros()
        Try
            Dim peticion As New Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion
            Dim respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Respuesta
            peticion.codigoAplicacion = Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS
            peticion.codigoDelegacion = MyBase.DelegacionLogada.Codigo
            peticion.codigosParametro = New List(Of String)

            peticion.codigosParametro.Add(Comon.Constantes.COD_MAE_LIMITE_DIAS_ANTERIORES_FECHA_INICIO_VIGENCIA)

            respuesta = Prosegur.Genesis.LogicaNegocio.Genesis.Parametros.obtenerParametros(peticion)

            If respuesta IsNot Nothing Then
                Parametros = respuesta.parametros
            End If

            peticion = Nothing
            respuesta = Nothing
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub HabilitarDesabilitaForm(habilita As Boolean)
        txtDescricaoForm.Enabled = habilita
        txtDeviceIDForm.Enabled = habilita
        ddlMarcaForm.Enabled = habilita
        ddlModeloForm.Enabled = habilita
        ddlDelegacionForm.Enabled = habilita
        ddlPlantaForm.Enabled = habilita
        If Acao = Utilidad.eAcao.Modificacion Then
            ddlDelegacionForm.Enabled = True
            ddlPlantaForm.Enabled = True
        End If

        If habilita Then
            divAddPtoServ.Style.Add("display", "block")
        Else
            divAddPtoServ.Style.Add("display", "none")
            txtFechaFin.Enabled = habilita
            txtFechaInicio.Enabled = habilita
            dvFechaInicio.Style.Add("display", "block")
            dvFechaFin.Style.Add("display", "block")
            pnUcClientesTesoreria.Enabled = habilita
            txtPorComisionMaquina.Enabled = habilita

        End If
        chkMultiClientes.Enabled = habilita
        pnUcClientesPtoServMono.Enabled = habilita
        divDesplazado.Visible = False
        chkConsideraRecuentos.Enabled = habilita
        pnUcBancoform.Enabled = habilita
        ddlTipoPlanificacion.Enabled = habilita


        If Not chkMultiClientes.Checked AndAlso Acao = Utilidad.eAcao.Modificacion Then
            If puntosServicioGrid.Count > 0 AndAlso puntosServicioGrid(0).desplazado Then
                pnUcClientesPtoServMono.Enabled = False
                divDesplazado.Visible = True
            End If

        End If



        'btnAddPlanificacion.Visible = habilita
    End Sub

    Private Sub HabilitarDesabilitaPlanificacion()
        Dim habilitado As Boolean
        habilitado = If(String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue), False, True)

        txtFechaInicio.Enabled = habilitado
        txtFechaFin.Enabled = habilitado
        pnUcBancoform.Enabled = habilitado

        txtPorComisionMaquina.Enabled = habilitado
        pnUcClientesTesoreria.Enabled = habilitado

        divPlanificacionPatron.Visible = habilitado
        divPlanificacionMAE.Visible = False

        divFacturacion.Visible = habilitado

        If Not habilitado Then
            PlanesMAExCanalesSubCanalesPuntos.Clear()
            gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
            gridPlanes.DataBind()
        End If

        btnTransacciones.Enabled = habilitado AndAlso (Acao <> Utilidad.eAcao.Alta)

        If Planificacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(Planificacion.Descripcion) Then
            'Planificacion.Delegacion.Codigo
            HoraDelegacion(Planificacion)
            txtPlanificacion.Text = Planificacion.Descripcion.ToUpper()
            chkVigentePlan.Checked = Planificacion.BolActivo
            chkControlaFacturacion.Checked = Planificacion.BolControlaFacturacion
        Else
            btnTransacciones.Enabled = False
        End If

        dvFechaInicio.Style.Add("display", "none")
        dvFechaFin.Style.Add("display", "none")

        txtPlanificacion.Text = String.Empty
        chkVigentePlan.Checked = False
        chkControlaFacturacion.Checked = False
        'divTooltip.Visible = False
        'lblReloj.Visible = False

        txtFechaInicio.Text = String.Empty
        txtFechaFin.Text = String.Empty
        txtPorComisionMaquina.Text = String.Empty
        txtPorComisionPlanificacion.Text = String.Empty
        txtPorComisionCliente.Text = String.Empty
        DirectCast(_ucBanco.ucCliente.FindControl("txtCodigo"), System.Web.UI.WebControls.TextBox).[Text] = ""
        DirectCast(_ucBanco.ucCliente.FindControl("txtDescripcion"), System.Web.UI.WebControls.TextBox).[Text] = ""

        'Limite
        divPlanificacionLimite.Visible = habilitado
        LimpiarUcLimitePlanificacion()
    End Sub

    ''' <summary>
    ''' Preenche o dropdownbox de Planta
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherddlPlanta(OidDelegacion As String, form As Boolean, filtro As Boolean)
        Try
            Dim objAccionPlanta As New LogicaNegocio.AccionPlanta
            Dim objPeticion As New ContractoServicio.Planta.GetPlanta.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta

            objPeticion.oidDelegacion = OidDelegacion
            objPeticion.BolActivo = True
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            If String.IsNullOrEmpty(OidDelegacion) Then
                If filtro Then
                    ddlPlanta.Items.Clear()
                    ddlPlanta.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                If form Then
                    ddlPlantaForm.Items.Clear()
                    ddlPlantaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                Exit Sub
            Else
                objPeticion.oidDelegacion = OidDelegacion
            End If

            objRespuesta = objAccionPlanta.GetPlantas(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                Exit Sub
            End If

            If objRespuesta.Planta.Count = 0 Then
                If filtro Then
                    ddlPlanta.Items.Clear()
                    ddlPlanta.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                If form Then
                    ddlPlantaForm.Items.Clear()
                    ddlPlantaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                Exit Sub
            Else
                Dim lista = objRespuesta.Planta.Select(Function(a) New With {.OidPlanta = a.OidPlanta,
                                                                            .DesPlanta = a.DesPlanta,
                                                                            .CodDesPlanta = a.CodPlanta + " - " + a.DesPlanta}).OrderBy(Function(b) b.CodDesPlanta)
                If filtro Then
                    ddlPlanta.AppendDataBoundItems = True
                    ddlPlanta.Items.Clear()
                    ddlPlanta.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                    ddlPlanta.DataTextField = "CodDesPlanta"
                    ddlPlanta.DataValueField = "OidPlanta"
                    ddlPlanta.DataSource = lista
                    ddlPlanta.DataBind()
                End If
                If form Then
                    ddlPlantaForm.AppendDataBoundItems = True
                    ddlPlantaForm.Items.Clear()
                    ddlPlantaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                    ddlPlantaForm.DataTextField = "CodDesPlanta"
                    ddlPlantaForm.DataValueField = "OidPlanta"
                    ddlPlantaForm.DataSource = lista
                    ddlPlantaForm.DataBind()
                End If
                Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
                If objRespuesta.Planta.FirstOrDefault().CodDelegacion = usuario.CodigoDelegacion Then
                    Dim planta = objRespuesta.Planta.Find(Function(d) d.CodPlanta = usuario.CodigoPlanta)
                    If planta IsNot Nothing Then
                        If filtro Then
                            SeleccionarPlantaLogada(planta.OidPlanta, ddlPlanta)
                        End If
                        If form Then
                            SeleccionarPlantaLogada(planta.OidPlanta, ddlPlantaForm)
                        End If
                    End If
                End If

                If lista.Count = 1 Then
                    If filtro Then
                        SeleccionarPlantaLogada(lista.FirstOrDefault().OidPlanta, ddlPlanta)
                    End If
                    If form Then
                        SeleccionarPlantaLogada(lista.FirstOrDefault().OidPlanta, ddlPlantaForm)
                    End If
                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Preencher a dropdownbox de delegaciones
    ''' </summary>
    Private Sub PreencherddlDelegacion(form As Boolean, filtro As Boolean)

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objAccionDelegacion.GetDelegaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Delegacion.Count = 0 Then
            If filtro Then
                ddlPlanta.Items.Clear()
                ddlPlanta.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            End If
            If form Then
                ddlPlantaForm.Items.Clear()
                ddlPlantaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            End If
            Exit Sub
        End If

        If objRespuesta.Delegacion.Count > 0 Then
            Dim lista = objRespuesta.Delegacion.Select(Function(a) New With {.OidDelegacion = a.OidDelegacion,
                                                                             .CodDelegacion = a.CodDelegacion,
                                                                             .DesDelegacion = a.DesDelegacion,
                                                                             .CodDesDelegacion = a.CodDelegacion + " - " + a.DesDelegacion}).OrderBy(Function(b) b.CodDesDelegacion)

            If filtro Then
                ddlDelegacion.AppendDataBoundItems = True
                ddlDelegacion.Items.Clear()
                ddlDelegacion.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                ddlDelegacion.DataTextField = "CodDesDelegacion"
                ddlDelegacion.DataValueField = "OidDelegacion"
                ddlDelegacion.DataSource = lista.ToList()
                ddlDelegacion.DataBind()
            End If

            If form Then
                ddlDelegacionForm.AppendDataBoundItems = True
                ddlDelegacionForm.Items.Clear()
                ddlDelegacionForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                ddlDelegacionForm.DataTextField = "CodDesDelegacion"
                ddlDelegacionForm.DataValueField = "OidDelegacion"
                ddlDelegacionForm.DataSource = lista.ToList()
                ddlDelegacionForm.DataBind()
            End If

            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            If (usuario IsNot Nothing AndAlso
            Not String.IsNullOrEmpty(usuario.CodigoDelegacion)) Then
                Dim delegacion = objRespuesta.Delegacion.Find(Function(d) d.CodDelegacion = usuario.CodigoDelegacion)
                If delegacion IsNot Nothing Then
                    If filtro Then
                        SeleccionarDelegacionLogada(delegacion.OidDelegacion, ddlDelegacion, form, filtro)
                    End If

                End If
            End If

        End If

    End Sub

    Private Sub SeleccionarDelegacionLogada(oidDelegacion As String, ByRef ddlDelegacionControl As DropDownList, form As Boolean, filtro As Boolean)
        Dim delegacionLogada = Nothing
        delegacionLogada = ddlDelegacionControl.Items.FindByValue(oidDelegacion)
        If delegacionLogada IsNot Nothing Then
            ddlDelegacionControl.SelectedIndex = ddlDelegacionControl.Items.IndexOf(delegacionLogada)
            If ddlDelegacionControl Is ddlDelegacion Then
                PreencherddlPlanta(ddlDelegacionControl.SelectedValue, form, filtro)
            End If
        End If
    End Sub

    Private Sub LimpiarForm(Optional editar As Boolean = False)
        txtDescricaoForm.Text = String.Empty
        txtDeviceIDForm.Text = String.Empty
        CodigosAjenosPeticion = Nothing

        PreencherddlMarca(True, False)
        ddlMarcaForm_SelectedIndexChanged(Nothing, Nothing)
        PreencherddlDelegacion(True, False)
        ddlDelegacionForm_SelectedIndexChanged(Nothing, Nothing)

        chkVigenteForm.Checked = False
        chkConsideraRecuentos.Checked = False

        txtFechaFin.Text = String.Empty
        txtFechaInicio.Text = String.Empty

        Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
        ClientesPtoServ.Clear()
        ClientesPtoServ.Add(objCliente)
        ClientesPtoServMono.Clear()

        puntosServicioGrid = New List(Of PuntoServicioGrid)
        ConfigurarControle_ClientePtoServ()
        AtualizaDadosHelperCliente(ClientesPtoServ, ucClientesPtoServ)

        ClientesPtoServMono.Add(objCliente)
        AddLista("ucClientesPtoServMono")

        ConfigurarControle_ClientesTesoreria()
        ClientesTesoreria.Clear()

        AddLista("ucClientesTesoreria")


        lboxCanalPatron.DataSource = Nothing
        lboxCanalPatron.DataBind()
        lboxSubCanalPatron.DataSource = Nothing
        lboxSubCanalPatron.DataBind()

        grid.DataSource = Nothing
        grid.DataBind()

        PlanesMAExCanalesSubCanalesPuntos.Clear()
        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()

        DatosBancarios = Nothing
        Maquina = Nothing
        OidMaquina = String.Empty
        puntosServicioGridMaquina = Nothing
        Planificacion = Nothing
        txtPlanificacion.Text = String.Empty
        chkVigentePlan.Checked = False
        chkControlaFacturacion.Checked = False

        btnAddPlanificacion.Enabled = False
        chkVigenteForm.Checked = True
        chkConsideraRecuentos.Checked = True

        txtBancoTesoreriaPadron.Text = String.Empty
        txtCuentaTesoreriaPadron.Text = String.Empty

        txtPorComisionMaquina.Text = String.Empty
        txtPorComisionPlanificacion.Text = String.Empty
        txtPorComisionCliente.Text = String.Empty

        If chkMultiClientes.Checked Then
            divMonocliente.Visible = False
            divMulticliente.Visible = True
            divPorComisionCliente.Visible = False

        Else
            divMonocliente.Visible = True
            divMulticliente.Visible = False
            divPorComisionCliente.Visible = True
        End If



        Canales.Clear()
        PuntoServicios.Clear()


        lboxCanalPatron.Items.Clear()
        lboxSubCanalPatron.Items.Clear()

        lboxCanalPatron.DataBind()
        lboxSubCanalPatron.DataBind()

        upCanalesPatron.Update()

        'Limite
        LimpiarUcLimiteMae()
        LimpiarUcLimitePlanificacion()

    End Sub

    Private Sub AddLista(uc As String)
        If Not listaUCAtualiza.Contains(uc) Then

            listaUCAtualiza.Add(uc)
        End If
    End Sub
    Private Sub SeleccionarPlantaLogada(oidPlanta As String, ByRef ddlPlanta As DropDownList)
        Dim plantaLogada = Nothing
        plantaLogada = ddlPlanta.Items.FindByValue(oidPlanta)
        If plantaLogada IsNot Nothing Then
            ddlPlanta.SelectedIndex = ddlPlanta.Items.IndexOf(plantaLogada)
        End If
    End Sub

    Private Sub PreencherddlMarca(form As Boolean, filtro As Boolean)

        Dim objPeticion As New ContractoServicio.Fabricante.GetFabricante.Peticion
        Dim objRespuesta As New ContractoServicio.Fabricante.GetFabricante.Respuesta
        Dim objAccionFabricante As New LogicaNegocio.AccionFabricante

        objPeticion.BolVigente = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objAccionFabricante.GetFabricantes(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Fabricante.Count = 0 Then
            If filtro Then
                ddlMarca.Items.Clear()
                ddlMarca.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            End If
            If form Then
                ddlMarcaForm.Items.Clear()
                ddlMarcaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            End If
            Exit Sub
        End If

        If objRespuesta.Fabricante.Count > 0 Then
            Dim lista = objRespuesta.Fabricante.Select(Function(a) New With {.OidFabricante = a.Identificador,
                                                                             .DesFabricante = a.Descripcion,
                                                                             .CodDesFabricante = a.Codigo + " - " + a.Descripcion}).OrderBy(Function(b) b.CodDesFabricante)

            If filtro Then
                ddlMarca.AppendDataBoundItems = True
                ddlMarca.Items.Clear()
                ddlMarca.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                ddlMarca.DataTextField = "CodDesFabricante"
                ddlMarca.DataValueField = "OidFabricante"
                ddlMarca.DataSource = lista.ToList()
                ddlMarca.DataBind()
            End If
            If form Then
                ddlMarcaForm.AppendDataBoundItems = True
                ddlMarcaForm.Items.Clear()
                ddlMarcaForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                ddlMarcaForm.DataTextField = "CodDesFabricante"
                ddlMarcaForm.DataValueField = "OidFabricante"
                ddlMarcaForm.DataSource = lista.ToList()
                ddlMarcaForm.DataBind()
            End If
        End If

    End Sub

    Private Sub PreencherddlModelo(OidFabricante As String, form As Boolean, filtro As Boolean)
        Try
            Dim objAccionModelo As New LogicaNegocio.AccionModelo
            Dim objPeticion As New ContractoServicio.Modelo.GetModelo.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Modelo.GetModelo.Respuesta

            objPeticion.OidFabricante = OidFabricante
            objPeticion.BolVigente = True
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            If String.IsNullOrEmpty(OidFabricante) Then
                If filtro Then
                    ddlModelo.Items.Clear()
                    ddlModelo.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                If form Then
                    ddlModeloForm.Items.Clear()
                    ddlModeloForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                Exit Sub
            Else
                objPeticion.OidFabricante = OidFabricante
            End If

            objRespuesta = objAccionModelo.GetModelos(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                Exit Sub
            End If

            If objRespuesta.Modelo.Count = 0 Then
                If filtro Then
                    ddlModelo.Items.Clear()
                    ddlModelo.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                If form Then
                    ddlModeloForm.Items.Clear()
                    ddlModeloForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                End If
                Exit Sub
            Else
                Dim lista = objRespuesta.Modelo.Select(Function(a) New With {.OidModelo = a.Identificador,
                                                                            .DesModelo = a.Descripcion,
                                                                            .CodDesModelo = a.Codigo + " - " + a.Descripcion}).OrderBy(Function(b) b.CodDesModelo)

                If filtro Then
                    ddlModelo.AppendDataBoundItems = True
                    ddlModelo.Items.Clear()
                    ddlModelo.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                    ddlModelo.DataTextField = "CodDesModelo"
                    ddlModelo.DataValueField = "OidModelo"
                    ddlModelo.DataSource = lista
                    ddlModelo.DataBind()
                End If
                If form Then
                    ddlModeloForm.AppendDataBoundItems = True
                    ddlModeloForm.Items.Clear()
                    ddlModeloForm.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
                    ddlModeloForm.DataTextField = "CodDesModelo"
                    ddlModeloForm.DataValueField = "OidModelo"
                    ddlModeloForm.DataSource = lista
                    ddlModeloForm.DataBind()
                End If
            End If
            ddlModelo.Enabled = ddlModelo.Items.Count > 1
            ddlModeloForm.Enabled = ddlModeloForm.Items.Count > 1
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Private Sub PreencherddlPlanificacao()

        Dim objPeticion As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Peticion
        Dim objRespuesta As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta
        Dim objAccionTipoPlanificao As New LogicaNegocio.AccionTipoPlanificacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objAccionTipoPlanificao.getTiposPlanificaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.TiposPlanificaciones.Count = 0 Then
            ddlTipoPlanificacion.Items.Clear()
            ddlTipoPlanificacion.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If objRespuesta.TiposPlanificaciones.Count > 0 Then
            TiposPlanifiacion = objRespuesta.TiposPlanificaciones

            Dim lista = objRespuesta.TiposPlanificaciones.Select(Function(a) New With {.OidTipoPlanificacao = a.oidTipoPlanificacion,
                                                                                        .DesTipoPlanificacao = a.desTipoPlanificacion
                                                                                        }).OrderBy(Function(b) b.DesTipoPlanificacao)

            ddlTipoPlanificacion.AppendDataBoundItems = True
            ddlTipoPlanificacion.Items.Clear()
            ddlTipoPlanificacion.Items.Add(New ListItem(Tradutor.Traduzir("gen_ddl_selecione"), String.Empty))
            ddlTipoPlanificacion.DataTextField = "DesTipoPlanificacao"
            ddlTipoPlanificacion.DataValueField = "OidTipoPlanificacao"
            ddlTipoPlanificacion.DataSource = lista.ToList()
            ddlTipoPlanificacion.DataBind()
        End If

    End Sub


    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String
        Try
            Dim strErro As New Text.StringBuilder(String.Empty)
            Dim focoSetado As Boolean = False

            If Page.IsPostBack Then

                If ValidarCamposObrigatorios Then
                    Dim erroFiltro As Boolean = True

                    If (Clientes IsNot Nothing AndAlso Clientes.Count > 0) Then
                        erroFiltro = False
                    End If
                    If ddlDelegacion.SelectedIndex > 0 Then
                        erroFiltro = False
                    End If
                    If ddlPlanta.SelectedIndex > 0 Then
                        erroFiltro = False
                    End If
                    If Not String.IsNullOrEmpty(txtDeviceID.Text) Then
                        erroFiltro = False
                    End If
                    If Not String.IsNullOrEmpty(txtDescricao.Text) Then
                        erroFiltro = False
                    End If
                    If ddlMarca.SelectedIndex > 0 Then
                        erroFiltro = False
                    End If
                    If ddlModelo.SelectedIndex > 0 Then
                        erroFiltro = False
                    End If

                    If erroFiltro Then
                        strErro.Append(MyBase.RecuperarValorDic("MSG_ERROR_FILTROS_VACIOS") & Aplicacao.Util.Utilidad.LineBreak)
                    End If

                End If

            End If

            Return strErro.ToString
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
            Return Nothing
        End Try
    End Function

    Private Function MontaMensagensErroPtoServ(objClientes As ObservableCollection(Of Comon.Clases.Cliente)) As String
        Try
            If objClientes Is Nothing OrElse objClientes.Count = 0 OrElse String.IsNullOrEmpty(objClientes.First.Identificador) Then
                Return MyBase.RecuperarValorDic("msg_csvFiltroCliente")
            End If

            Dim objCliente = objClientes.First

            If objCliente.SubClientes Is Nothing OrElse objCliente.SubClientes.Count = 0 OrElse String.IsNullOrEmpty(objCliente.SubClientes.First.Identificador) Then
                Return MyBase.RecuperarValorDic("msg_csvFiltroSubCliente")
            End If
            If objCliente.SubClientes.First.PuntosServicio Is Nothing OrElse objCliente.SubClientes.First.PuntosServicio.Count = 0 OrElse String.IsNullOrEmpty(objCliente.SubClientes.First.PuntosServicio.First.Identificador) Then
                Return MyBase.RecuperarValorDic("msg_csvFiltroPtoServ")
            End If

            If puntosServicioGrid IsNot Nothing AndAlso puntosServicioGrid.Count > 0 Then

                Dim lstPtoServ = puntosServicioGrid.Where(Function(a) a.oidPtoServicio = objCliente.SubClientes.First.PuntosServicio.First.Identificador)

                If lstPtoServ.Count > 0 Then
                    Return String.Format(MyBase.RecuperarValorDic("MSG_INFO_COMBINATORIA_EXISTENTE"), lstPtoServ.First.codCliente, lstPtoServ.First.codSubcliente, lstPtoServ.First.codPtoServicio)
                End If

            End If

            Return Nothing

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
            Return Nothing
        End Try
    End Function

    Private Sub LimparCamposFiltro()
        Try
            txtDescricao.Text = String.Empty
            txtDeviceID.Text = String.Empty

            PreencherddlMarca(False, True)
            ddlMarca_SelectedIndexChanged(Nothing, Nothing)
            PreencherddlDelegacion(False, True)
            ddlDelegacion_SelectedIndexChanged(Nothing, Nothing)
            PreencherddlPlanificacao()

            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes, ucClientes)
            AtualizaDadosHelperCliente(Clientes, ucBanco)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function BuscarClienteECompleto(objCliente As Comon.Clases.Cliente) As Boolean
        Dim objSubcliente = objCliente.SubClientes.First
        Dim objPtoServicio = objSubcliente.PuntosServicio.First


        Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno

        Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion
        Dim objRespuesta As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TCLIENTE"
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.CodigosAjeno.OidTablaGenesis = objCliente.Identificador

        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        ' chamar servicio
        objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

        If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then

            Return False
        End If

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TSUBCLIENTE"
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.CodigosAjeno.OidTablaGenesis = objSubcliente.Identificador
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        ' chamar servicio
        objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

        If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then

            Return False
        End If

        'Preenche Codigo Ajeno 
        objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO"
        objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
        objPeticion.CodigosAjeno.OidTablaGenesis = objPtoServicio.Identificador
        objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        ' chamar servicio
        objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

        If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub AddPuntoServicio(objCliente As Comon.Clases.Cliente)
        Try
            Dim objSubcliente = objCliente.SubClientes.First
            Dim objPtoServicio = objSubcliente.PuntosServicio.First

            If puntosServicioGrid Is Nothing Then
                puntosServicioGrid = New List(Of PuntoServicioGrid)
            End If
            If puntosServicioGrid.FirstOrDefault(Function(x) x.oidPtoServicio = objPtoServicio.Identificador) IsNot Nothing Then
                Return
            End If


            Dim objCodigoAjenoCliente As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjenoRespuesta
            Dim objCodigoAjenoSubcliente As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjenoRespuesta
            Dim objCodigoAjenoPtoServicio As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjenoRespuesta
            Dim completo As Boolean = True
            Dim pendiente As Boolean = False
            Dim objAccionCodigoAjeno As New LogicaNegocio.AccionCodigoAjeno

            Dim objPeticion As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion
            Dim objRespuesta As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta

            'Preenche Codigo Ajeno de cliente
            'If Not objCliente.Codigo.Contains("*") Then
            objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
            objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TCLIENTE"
            objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
            objPeticion.CodigosAjeno.OidTablaGenesis = objCliente.Identificador

            objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            ' chamar servicio
            objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

            If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then
                completo = False
            Else
                objCodigoAjenoCliente = objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.First
            End If
            'End If


            'Preenche Codigo Ajeno de subcliente
            'If Not objSubcliente.Codigo.Contains("*") Then
            objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
            objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TSUBCLIENTE"
            objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
            objPeticion.CodigosAjeno.OidTablaGenesis = objSubcliente.Identificador
            objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            ' chamar servicio
            objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

            If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then
                completo = False
            Else
                objCodigoAjenoSubcliente = objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.First
            End If
            'End If


            'Preenche Codigo Ajeno de Punto de Servicio
            'If Not objPtoServicio.Codigo.Contains("*") Then
            objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
            objPeticion.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO"
            objPeticion.CodigosAjeno.CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor()
            objPeticion.CodigosAjeno.OidTablaGenesis = objPtoServicio.Identificador
            objPeticion.ParametrosPaginacion = New ParametrosPeticionPaginacion
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            ' chamar servicio
            objRespuesta = objAccionCodigoAjeno.GetCodigosAjenos(objPeticion)

            If objRespuesta.EntidadCodigosAjenos Is Nothing _
                    OrElse objRespuesta.EntidadCodigosAjenos.Count = 0 _
                        OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos Is Nothing _
                            OrElse objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.Count = 0 Then
                completo = False
            Else
                objCodigoAjenoPtoServicio = objRespuesta.EntidadCodigosAjenos.First.CodigosAjenos.First
            End If
            'End If

            If objPtoServicio IsNot Nothing AndAlso objPtoServicio.Identificador IsNot Nothing Then
                Dim objRespDel As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
                Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion

                objRespDel = objAccionDelegacion.GetDelegacionByIdentificador(ddlDelegacionForm.SelectedValue.ToString, "MAE")

                Dim codDelegacionMaquina = ""
                If objRespDel IsNot Nothing And objRespDel.Delegacion IsNot Nothing And objRespDel.Delegacion(0) IsNot Nothing Then
                    If objRespDel.Delegacion.FirstOrDefault IsNot Nothing Then
                        If String.IsNullOrWhiteSpace(objRespDel.Delegacion(0).CodDelegacionAjeno) Then
                            MyBase.MostraMensagemErro(MyBase.RecuperarValorDic("MSG_DELEGACION_SIN_CODIGO_AJENO_MAE"), String.Empty)
                            Return
                        End If

                        If Not String.IsNullOrWhiteSpace(objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno) Then
                            codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno
                        Else
                            codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacion
                        End If
                    End If
                End If
                MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(txtDeviceIDForm.Text)

                'Dim objPuntoServicio = ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.First
                MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPtoServicio.Identificador)

                Dim desplazado As Boolean = False
                If MaquinaPuntoServicio IsNot Nothing Then
                    desplazado = Not ValidaSaldoPtoServicio(objCliente.Identificador, objSubcliente.Identificador, objPtoServicio.Identificador, MaquinaPuntoServicio.Sector.Identificador, MaquinaPuntoServicio.Delegacion.Plantas.First.Identificador, False)
                End If
                puntosServicioGrid.Add(New PuntoServicioGrid With {
                               .oidCliente = objCliente.Identificador,
                               .codCliente = objCliente.Codigo,
                               .desCliente = objCliente.Descripcion,
                               .PorcComisionCliente = objCliente.PorcComisionCliente,
                               .oidCodigoAjenoCliente = objCodigoAjenoCliente.OidCodigoAjeno,
                               .codCodigoAjenoCliente = objCodigoAjenoCliente.CodAjeno,
                               .desCodigoAjenoCliente = objCodigoAjenoCliente.DesAjeno,
                               .bolDefectoCodigoAjenoCliente = objCodigoAjenoCliente.BolDefecto,
                               .oidSubcliente = objSubcliente.Identificador,
                               .codSubcliente = objSubcliente.Codigo,
                               .desSubcliente = objSubcliente.Descripcion,
                               .oidCodigoAjenoSubcliente = objCodigoAjenoSubcliente.OidCodigoAjeno,
                               .codCodigoAjenoSubcliente = objCodigoAjenoSubcliente.CodAjeno,
                               .desCodigoAjenoSubcliente = objCodigoAjenoSubcliente.DesAjeno,
                               .bolDefectoCodigoAjenoSubcliente = objCodigoAjenoSubcliente.BolDefecto,
                               .oidPtoServicio = objPtoServicio.Identificador,
                               .codPtoServicio = objPtoServicio.Codigo,
                               .desPtoServicio = objPtoServicio.Descripcion,
                               .oidCodigoAjenoPtoServicio = objCodigoAjenoPtoServicio.OidCodigoAjeno,
                               .codCodigoAjenoPtoServicio = objCodigoAjenoPtoServicio.CodAjeno,
                               .desCodigoAjenoPtoServicio = objCodigoAjenoPtoServicio.DesAjeno,
                               .bolDefectoCodigoAjenoPuntoServicio = objCodigoAjenoPtoServicio.BolDefecto,
                               .codSectorMAE = If(Maquina Is Nothing OrElse String.IsNullOrEmpty(Maquina.DeviceID), MantenimientoCodigosAjenosMAE.CalculaCodigoSectorMAE(
                                                                codDelegacionMaquina,
                                                                .codCodigoAjenoCliente,
                                                                .codCodigoAjenoSubcliente,
                                                                .codCodigoAjenoPtoServicio), Maquina.DeviceID),
                               .completo = completo,
                               .desplazado = desplazado
                               })

                txtPorComisionCliente.Text = RetornaDecimal(objCliente.PorcComisionCliente)
            End If

            If String.IsNullOrEmpty(txtDeviceIDForm.Text.Trim) Then
                txtDeviceIDForm.Text = puntosServicioGrid.LastOrDefault.codSectorMAE
            End If

            If Not String.IsNullOrEmpty(objPtoServicio.Codigo) Then
                grid.DataSource = puntosServicioGrid
                grid.DataBind()
            End If
            'ucClientesPtoServMono.Clientes.add()

        Catch exNeg As Excepcion.NegocioExcepcion
            Throw exNeg
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub DelPuntoServicio(oidPuntoServicio As String)
        Try

            If puntosServicioGrid IsNot Nothing Then

                Dim objPuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = oidPuntoServicio)



                'MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPuntoServicioGrid.oidPuntoServicio)



                If puntosServicioGridMaquina Is Nothing OrElse Not puntosServicioGridMaquina.Any(Function(a) a.oidPtoServicio = oidPuntoServicio) _
                    OrElse ValidaSaldoPtoServicio(objPuntoServicioGrid.oidCliente,
                                                objPuntoServicioGrid.oidSubcliente,
                                                objPuntoServicioGrid.oidPtoServicio,
                                                 Maquina.OidSector,
                                                Maquina.OidPlanta) Then

                    puntosServicioGrid.Remove(objPuntoServicioGrid)


                    grid.DataSource = puntosServicioGrid
                    grid.DataBind()

                    If DatosBancarios IsNot Nothing Then
                        DatosBancarios.Remove(oidPuntoServicio)
                    End If

                    'Limpiar helper el punto de servicio que se quita
                    'y quitarlo de la grilla de la planificacion x mae x canal x subcanal
                    Dim planes As New ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)

                    For Each unPlan In PlanesMAExCanalesSubCanalesPuntos
                        If unPlan.PuntosServicios IsNot Nothing AndAlso unPlan.PuntosServicios.Count > 0 Then
                            'puntos.Clear()
                            Dim bMantener As Boolean = True
                            Dim objPuntoQuitar As PuntoServicio

                            For Each unPunto In unPlan.PuntosServicios
                                If unPunto.Identificador.Equals(oidPuntoServicio) Then
                                    objPuntoQuitar = unPunto
                                    bMantener = False
                                End If
                            Next

                            If bMantener Then
                                planes.Add(unPlan)
                            Else
                                unPlan.PuntosServicios.Remove(objPuntoQuitar)
                                If unPlan.PuntosServicios.Count > 0 Then
                                    planes.Add(unPlan)
                                End If
                            End If
                        End If
                    Next

                    'Limpiamos los helpers
                    PuntoServicios.Clear()
                    Canales.Clear()

                    'Asociadmos los planes sin el punto de servicio que se borro...
                    PlanesMAExCanalesSubCanalesPuntos = planes
                    gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
                    gridPlanes.DataBind()
                    pnUcClientesPtoServMono.Visible = Not puntosServicioGrid.Any
                    btnAddPtoServ.Visible = Not puntosServicioGrid.Any
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ExecutarGrabar()
        Dim identificadorLlamada As String = String.Empty
        Dim objRespuestaMae = New Prosegur.Genesis.Comon.Pantallas.Planificacion.Respuesta()
        Try
            Dim objAccionMAE As New LogicaNegocio.AccionMAE
            Dim objRespuesta As IAC.ContractoServicio.MAE.SetMAE.Respuesta
            Dim objPeticion As New IAC.ContractoServicio.MAE.SetMAE.Peticion
            Dim origen As String = "Prosegur.Global.GesEfectivo.IAC.Web.BusquedaMae"

            Genesis.LogicaNegocio.Genesis.Log.GenerarIdentificador("IACBusquedaMAE", identificadorLlamada)
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                Dim objJson As String = Newtonsoft.Json.JsonConvert.SerializeObject(objPeticion)
                Genesis.LogicaNegocio.Genesis.Log.Iniciar("IACBusquedaMAE", objJson, identificadorLlamada)
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Accion Alta.", "")
                objPeticion.gmtCreacion = DateTime.UtcNow
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
                objPeticion.Vigente = True
            Else
                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Accion Modificación.", Maquina.OidMaquina)
                objPeticion.OidMaquina = Maquina.OidMaquina
                objPeticion.Vigente = chkVigenteForm.Checked
                objPeticion.gmtCreacion = Maquina.FechaHoraCreacion
                objPeticion.desUsuarioCreacion = Maquina.DesUsuarioCreacion
                objPeticion.DeviceIDAnterior = Maquina.DeviceID
                objPeticion.OidPlantaAnterior = Maquina.OidPlanta
            End If

            objPeticion.ConsideraRecuentos = chkConsideraRecuentos.Checked
            objPeticion.MultiClientes = chkMultiClientes.Checked
            objPeticion.gmtModificacion = DateTime.UtcNow
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objPeticion.OidPlanta = ddlPlantaForm.SelectedValue

            objPeticion.DeviceID = txtDeviceIDForm.Text.Trim

            If String.IsNullOrEmpty(objPeticion.DeviceID) Then
                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "DeviceID es no nulo.", puntosServicioGrid.First.codSectorMAE)
                objPeticion.DeviceID = puntosServicioGrid.First.codSectorMAE
            End If

            objPeticion.Descripcion = txtDescricaoForm.Text.Trim
            objPeticion.OidModelo = ddlModeloForm.SelectedValue

            If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) Then
                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "TipoPlanificacion es no nulo.", ddlTipoPlanificacion.SelectedValue)

                objPeticion.FechaValorInicio = Convert.ToDateTime(txtFechaInicio.Text)
                objPeticion.OidPlanificacion = Planificacion.Identificador

                If Planificacion.FechaHoraVigenciaFin IsNot Nothing AndAlso Planificacion.FechaHoraVigenciaFin <> DateTime.MinValue Then
                    objPeticion.FechaValorFin = Planificacion.FechaHoraVigenciaFin
                    Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Planificacion.FechaHoraVigenciaFin", Planificacion.FechaHoraVigenciaFin)
                End If

                If Not String.IsNullOrEmpty(txtFechaFin.Text) AndAlso Convert.ToDateTime(txtFechaFin.Text) <> DateTime.MinValue Then
                    objPeticion.FechaValorFin = Convert.ToDateTime(txtFechaFin.Text)
                    Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "txtFechaFin.Text", txtFechaFin.Text)
                End If
            End If

            objPeticion.PuntosServicio = New List(Of Comon.Clases.Cliente)
            objPeticion.CodigosAjeno = New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoColeccion

            'INICIO Codigos ajenos MAE
            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Llama al método CargarCodigoAjenoMae().", "")
            CargarCodigoAjenoMae()

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Inicia recorrido de Maquina.CodigosAjenos .", "")
            For Each codigoAjenoMAE In Maquina.CodigosAjenos
                'Actualizo el Codigo y la descripción del Codigo con identificador MAE
                If codigoAjenoMAE.CodIdentificador = "MAE" Then
                    codigoAjenoMAE.CodAjeno = txtDeviceIDForm.Text
                    codigoAjenoMAE.DesAjeno = txtDescricaoForm.Text
                End If

                objPeticion.CodigosAjeno.Add(New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno With {
                .OidCodigoAjeno = codigoAjenoMAE.OidCodigoAjeno,
                .CodIdentificador = codigoAjenoMAE.CodIdentificador,
                .CodTipoTablaGenesis = ContractoServicio.Constantes.COD_MAQUINA,
                .OidTablaGenesis = Nothing,
                .CodAjeno = codigoAjenoMAE.CodAjeno,
                .DesAjeno = codigoAjenoMAE.DesAjeno,
                .BolActivo = codigoAjenoMAE.BolActivo,
                .BolDefecto = codigoAjenoMAE.BolDefecto,
                .DesUsuarioCreacion = MyBase.LoginUsuario,
                .GmtCreacion = DateTime.Now,
                .DesUsuarioModificacion = MyBase.LoginUsuario,
                .GmtModificacion = DateTime.Now
            })
            Next
            'FIN Codigos ajenos MAE

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Inicia recorrido de puntosServicioGrid .", "")
            For Each objPuntoServicioGrid As PuntoServicioGrid In puntosServicioGrid
                Dim cliente As New Comon.Clases.Cliente With {
                                               .Identificador = objPuntoServicioGrid.oidCliente,
                                               .Codigo = objPuntoServicioGrid.codCliente,
                                               .Descripcion = objPuntoServicioGrid.desCliente,
                .SubClientes = New ObservableCollection(Of Comon.Clases.SubCliente)
                                               }
                Dim subcliente As New Comon.Clases.SubCliente With {
                                        .Identificador = objPuntoServicioGrid.oidSubcliente,
                                               .Codigo = objPuntoServicioGrid.codSubcliente,
                                               .Descripcion = objPuntoServicioGrid.desSubcliente,
                .PuntosServicio = New ObservableCollection(Of Comon.Clases.PuntoServicio)
                                        }
                Dim ptoServicio As New Comon.Clases.PuntoServicio With {
                                        .Identificador = objPuntoServicioGrid.oidPtoServicio,
                                        .Codigo = objPuntoServicioGrid.codPtoServicio,
                                        .Descripcion = objPuntoServicioGrid.desPtoServicio
                                        }
                subcliente.PuntosServicio.Add(ptoServicio)
                cliente.SubClientes.Add(subcliente)
                objPeticion.PuntosServicio.Add(cliente)

                If objPeticion.CodigosAjeno.FirstOrDefault(Function(f) f.OidTablaGenesis = objPuntoServicioGrid.oidCliente _
                                                    AndAlso f.CodTipoTablaGenesis = ContractoServicio.Constantes.COD_CLIENTE _
                                                    AndAlso f.CodAjeno = objPuntoServicioGrid.codCodigoAjenoCliente _
                                                    AndAlso f.BolDefecto = True) Is Nothing Then
                    Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Valida CodigosAjeno Cliente.", objPuntoServicioGrid.codCodigoAjenoCliente)

                    objPeticion.CodigosAjeno.Add(New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno With {
                                             .OidCodigoAjeno = objPuntoServicioGrid.oidCodigoAjenoCliente,
                                                        .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                        .CodTipoTablaGenesis = ContractoServicio.Constantes.COD_CLIENTE,
                                                        .OidTablaGenesis = objPuntoServicioGrid.oidCliente,
                                                        .CodAjeno = objPuntoServicioGrid.codCodigoAjenoCliente,
                                                        .DesAjeno = objPuntoServicioGrid.desCodigoAjenoCliente,
                                                        .BolActivo = True,
                                                        .BolDefecto = objPuntoServicioGrid.bolDefectoCodigoAjenoCliente,
                                                        .DesUsuarioCreacion = MyBase.LoginUsuario,
                                                        .GmtCreacion = DateTime.Now,
                                                        .DesUsuarioModificacion = MyBase.LoginUsuario,
                                                        .GmtModificacion = DateTime.Now})

                End If

                If objPeticion.CodigosAjeno.FirstOrDefault(Function(f) f.OidTablaGenesis = objPuntoServicioGrid.oidSubcliente _
                                                    AndAlso f.CodTipoTablaGenesis = ContractoServicio.Constantes.COD_SUBCLIENTE _
                                                    AndAlso f.CodAjeno = objPuntoServicioGrid.codCodigoAjenoSubcliente _
                                                    AndAlso f.BolDefecto = True) Is Nothing Then
                    Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Valida CodigosAjeno Cliente.", objPuntoServicioGrid.codCodigoAjenoSubcliente)

                    objPeticion.CodigosAjeno.Add(New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno With {
                                       .OidCodigoAjeno = objPuntoServicioGrid.oidCodigoAjenoSubcliente,
                                        .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                        .CodTipoTablaGenesis = ContractoServicio.Constantes.COD_SUBCLIENTE,
                                        .OidTablaGenesis = objPuntoServicioGrid.oidSubcliente,
                                        .CodAjeno = objPuntoServicioGrid.codCodigoAjenoSubcliente,
                                        .DesAjeno = objPuntoServicioGrid.desCodigoAjenoSubcliente,
                                        .BolActivo = True,
                                        .BolDefecto = objPuntoServicioGrid.bolDefectoCodigoAjenoSubcliente,
                                        .DesUsuarioCreacion = MyBase.LoginUsuario,
                                        .GmtCreacion = DateTime.Now,
                                        .DesUsuarioModificacion = MyBase.LoginUsuario,
                                        .GmtModificacion = DateTime.Now})
                End If

                If objPeticion.CodigosAjeno.FirstOrDefault(Function(f) f.OidTablaGenesis = objPuntoServicioGrid.oidPtoServicio _
                                                    AndAlso f.CodTipoTablaGenesis = ContractoServicio.Constantes.COD_PUNTOSERVICIO _
                                                    AndAlso f.CodAjeno = objPuntoServicioGrid.codCodigoAjenoPtoServicio _
                                                    AndAlso f.BolDefecto = True) Is Nothing Then
                    Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Valida CodigosAjeno Cliente.", objPuntoServicioGrid.codCodigoAjenoPtoServicio)

                    objPeticion.CodigosAjeno.Add(New ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjeno With {
                                        .OidCodigoAjeno = objPuntoServicioGrid.oidCodigoAjenoPtoServicio,
                                        .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                        .CodTipoTablaGenesis = ContractoServicio.Constantes.COD_PUNTOSERVICIO,
                                        .OidTablaGenesis = objPuntoServicioGrid.oidPtoServicio,
                                        .CodAjeno = objPuntoServicioGrid.codCodigoAjenoPtoServicio,
                                        .DesAjeno = objPuntoServicioGrid.desCodigoAjenoPtoServicio,
                                        .BolDefecto = objPuntoServicioGrid.bolDefectoCodigoAjenoPuntoServicio,
                                        .BolActivo = True,
                                        .DesUsuarioCreacion = MyBase.LoginUsuario,
                                        .GmtCreacion = DateTime.Now,
                                        .DesUsuarioModificacion = MyBase.LoginUsuario,
                                        .GmtModificacion = DateTime.Now})
                End If
            Next

            objPeticion.DatosBancario = New List(Of Comon.Clases.DatoBancario)
            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Valida DatosBancarios.", "")

            If DatosBancarios IsNot Nothing Then
                For Each objDatoBancario In DatosBancarios

                    If objDatoBancario.Value IsNot Nothing AndAlso objDatoBancario.Value.Count > 0 Then
                        For Each datoBancAdd In objDatoBancario.Value
                            objPeticion.DatosBancario.Add(datoBancAdd)
                        Next
                    Else
                        Dim objPuntoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = objDatoBancario.Key)

                        objPeticion.DatosBancario.Add(New Comon.Clases.DatoBancario With {
                                                      .Cliente = New Comon.Clases.Cliente With {
                                                          .Identificador = objPuntoServicio.oidCliente
                                                          },
                                                      .SubCliente = New Comon.Clases.SubCliente With {
                                                          .Identificador = objPuntoServicio.oidSubcliente
                                                          },
                                                        .PuntoServicio = New Comon.Clases.PuntoServicio With {
                                                            .Identificador = objPuntoServicio.oidPtoServicio
                                                            }
                                                                                      })
                    End If

                Next
            End If

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Validar PeticionesDatoBancario.", "")

            If PeticionesDatoBancario IsNot Nothing Then
                For Each item In PeticionesDatoBancario
                    If objPeticion.PeticionDatosBancarios Is Nothing Then
                        objPeticion.PeticionDatosBancarios = New Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion
                        objPeticion.PeticionDatosBancarios.CodigoPais = item.Value.CodigoPais
                        objPeticion.PeticionDatosBancarios.Cultura = item.Value.Cultura
                        objPeticion.PeticionDatosBancarios.DatosBancarios = New List(Of Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Entrada.DatoBancario)
                    End If
                    objPeticion.PeticionDatosBancarios.DatosBancarios.AddRange(item.Value.DatosBancarios)
                Next

                For Each itemDB In objPeticion.PeticionDatosBancarios.DatosBancarios.Where(Function(r) r.Accion = Genesis.ContractoServicio.Contractos.Integracion.Comon.Enumeradores.AccionABM.Baja)
                    For Each objDatoBancarioSer In DatosBancarios
                        Dim objPtoSv As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = objDatoBancarioSer.Key)
                        itemDB.IdentificadorCliente = objPtoSv.oidCliente
                        itemDB.IdentificadorSubCliente = objPtoSv.oidSubcliente
                        itemDB.IdentificadorPuntoDeServicio = objPtoSv.oidPtoServicio
                    Next
                Next

                objPeticion.PeticionDatosBancarios.Usuario = MyBase.LoginUsuario
            End If

            'verificar
            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Verificar txtPorComisionMaquina.", txtPorComisionMaquina.Text)

            If Not String.IsNullOrWhiteSpace(txtPorComisionMaquina.Text) Then
                objPeticion.PorcComisionMaquina = txtPorComisionMaquina.Text
            End If

            If ClientesTesoreria IsNot Nothing AndAlso ClientesTesoreria.FirstOrDefault() IsNot Nothing AndAlso ClientesTesoreria.FirstOrDefault().SubClientes IsNot Nothing Then
                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Asignar BancoTesoreria.", "")
                objPeticion.BancoTesoreria = ClientesTesoreria.FirstOrDefault().SubClientes.FirstOrDefault()
            End If

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Validar ValidarControlaFacuracion.", "")
            If Not ValidarControlaFacuracion(objPeticion) Then
                MostraMensagem(MyBase.RecuperarValorDic("valCuentaTesoreria"))
                Return
            End If

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Llamar RecuperarChavesDic.", "")
            objPeticion.dicionario = MyBase.RecuperarChavesDic()
            objPeticion.PlanesCanalesSubcanalesPtos = PlanesMAExCanalesSubCanalesPuntos

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Llamar ucLimiteMae.BuscarPeticionLimite.", "")
            objPeticion.Limites = ucLimiteMae.BuscarPeticionLimite()


            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Iniciar registro de MAE", objPeticion.OidMaquina)
            objRespuesta = objAccionMAE.SetMAE(objPeticion)

            Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Validar respuesta de registro de MAE", objPeticion.OidMaquina)
            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.AsociaPlan AndAlso Not String.IsNullOrEmpty(txtFechaInicio.Text) Then
                    Try
                        Genesis.LogicaNegocio.Integracion.Periodo.GenerarPeriodos(objRespuesta.OidMaquina, MyBase.LoginUsuario, identificadorLlamada)
                        Genesis.LogicaNegocio.Integracion.Periodo.RelacionarDocumentosMAE(objPeticion.DeviceID, Convert.ToDateTime(txtFechaInicio.Text).QuieroGrabarGMTZeroEnLaBBDD(MyBase.DelegacionLogada), identificadorLlamada)
                        If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                            objRespuestaMae.Codigo = "200"
                            objRespuestaMae.Descripcion = "Ok"
                            Genesis.LogicaNegocio.Genesis.Log.Finalizar(objRespuestaMae, identificadorLlamada)
                        End If
                    Catch ex As Exception
                        If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                            objRespuestaMae.Codigo = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_404A
                            objRespuestaMae.Descripcion = ex.Message
                            Genesis.LogicaNegocio.Genesis.Log.Finalizar(objRespuestaMae, identificadorLlamada)
                        End If
                        MyBase.MostraMensagem(ex.Message)
                        Exit Sub
                    End Try
                End If

                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Validar Modo.", Modo)
                If Modo = "modal" Then


                    Session("objResultModalMae") = String.Format(MyBase.RecuperarValorDic("MSG_INFO_MAE_EXITO_MOD"), objPeticion.DeviceID)

                    ' Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                    Dim jsScript As String = "fecharModal();"

                    ' fechar janela
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaMaeModal", jsScript, True)
                    Return
                End If

                Genesis.LogicaNegocio.Genesis.Log.AgregaDetalle(identificadorLlamada, origen, "Mostrar mensaje registro.", Acao.ToString())
                If Acao = Utilidad.eAcao.Modificacion Then
                    MyBase.MostraMensagem(String.Format(MyBase.RecuperarValorDic("MSG_INFO_MAE_EXITO_MOD"), objPeticion.DeviceID))
                Else
                    MyBase.MostraMensagem(String.Format(MyBase.RecuperarValorDic("MSG_INFO_MAE_EXITO_ALTA"), objPeticion.DeviceID))
                End If


                ExecutarCancelar()
                btnBuscar_Click(Nothing, Nothing)
            Else
                MyBase.MostraMensagem(objRespuesta.MensajeError)
            End If

            If identificadorLlamada IsNot Nothing Then
                Dim respuestaJson = Newtonsoft.Json.JsonConvert.SerializeObject(objRespuesta)
                objRespuestaMae.Codigo = objRespuesta.CodigoError
                objRespuestaMae.Descripcion = objRespuesta.MensajeError
                Genesis.LogicaNegocio.Genesis.Log.Finalizar(objRespuestaMae, identificadorLlamada)
            End If
        Catch ex As Exception
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                objRespuestaMae.Codigo = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Constantes.CONST_ERRO_404A
                objRespuestaMae.Descripcion = ex.Message
                Genesis.LogicaNegocio.Genesis.Log.Finalizar(objRespuestaMae, identificadorLlamada)
            End If
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub



    Private Function ValidarControlaFacuracion(objPeticion As ContractoServicio.MAE.SetMAE.Peticion) As Boolean
        If chkControlaFacturacion.Checked Then

            Dim oidPtoServicioTesoreria As String = String.Empty
            Dim oidSubclienteTesoreria As String = String.Empty

            If objPeticion.BancoTesoreria IsNot Nothing Then
                oidSubclienteTesoreria = objPeticion.BancoTesoreria.Identificador

                If objPeticion.BancoTesoreria.PuntosServicio IsNot Nothing AndAlso objPeticion.BancoTesoreria.PuntosServicio IsNot Nothing AndAlso objPeticion.BancoTesoreria.PuntosServicio.Count > 0 Then
                    oidPtoServicioTesoreria = objPeticion.BancoTesoreria.PuntosServicio.FirstOrDefault().Identificador
                End If
            End If

            If String.IsNullOrWhiteSpace(oidPtoServicioTesoreria) Then

                Dim objLogicaNegocio As New LogicaNegocio.AccionPlanificacion
                Dim objPeticionPlan As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Peticion
                Dim objRespuestaPlan As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta
                objPeticionPlan.ParametrosPaginacion.RealizarPaginacion = False
                objPeticion.OidPlanificacion = Planificacion.Identificador

                If ddlDelegacionForm.SelectedValue IsNot Nothing AndAlso Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then
                    objPeticionPlan.OidDelegacion = ddlDelegacionForm.SelectedValue
                End If

                objRespuestaPlan = objLogicaNegocio.GetPlanificacionFacturacion(objPeticionPlan)

                If objRespuestaPlan IsNot Nothing AndAlso objRespuestaPlan.Planificacion IsNot Nothing Then
                    If objRespuestaPlan.Planificacion.BancoTesoreriaDelegacion IsNot Nothing Then
                        oidSubclienteTesoreria = objRespuestaPlan.Planificacion.BancoTesoreriaDelegacion.Identificador
                    End If
                    If objRespuestaPlan.Planificacion.CuentaTesoreriaDelegacion IsNot Nothing Then
                        oidPtoServicioTesoreria = objRespuestaPlan.Planificacion.CuentaTesoreriaDelegacion.Identificador
                    End If
                End If
            End If

            If String.IsNullOrWhiteSpace(oidSubclienteTesoreria) OrElse String.IsNullOrWhiteSpace(oidPtoServicioTesoreria) Then
                Return False

            End If

        End If
        Return True
    End Function

    Private Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String
        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False
        Dim fechaActual = DateTime.Now

        If Page.IsPostBack Then

            'Verifica se a delegação foi selecionada
            If ddlDelegacionForm.Visible AndAlso ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then

                strErro.Append(csvDelegacionForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDelegacionForm.IsValid = False

                'Setar o foco no primeiro campo que deu erro
                If focoSetado AndAlso Not focoSetado Then
                    ddlDelegacionForm.Focus()
                    focoSetado = True
                End If
            Else
                csvDelegacionForm.IsValid = True
            End If

            'Verifica se a planta foi enviada
            If ddlPlantaForm.Visible AndAlso ddlPlantaForm.SelectedValue.Equals(String.Empty) Then

                strErro.Append(csvPlantaForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvPlantaForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    ddlPlantaForm.Focus()
                    focoSetado = True
                End If
            Else
                csvPlantaForm.IsValid = True
            End If

            If txtDescricaoForm.Text.Trim.Equals(String.Empty) Then

                strErro.Append(csvDescricaoForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoForm.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoForm.Focus()
                    focoSetado = True
                End If
            Else
                csvDescricaoForm.IsValid = True
            End If

            If ddlMarcaForm.Visible AndAlso ddlMarcaForm.SelectedValue.Equals(String.Empty) Then

                strErro.Append(csvMarcaForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvMarcaForm.IsValid = False

                'Setar o foco no primeiro que controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    ddlMarcaForm.Focus()
                    focoSetado = True
                End If
            Else
                csvMarcaForm.IsValid = True
            End If

            If ddlModeloForm.Visible AndAlso ddlModeloForm.SelectedValue.Equals(String.Empty) Then

                strErro.Append(csvModeloForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvModeloForm.IsValid = False

                'Setar o foco no primeiro que controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    ddlModeloForm.Focus()
                    focoSetado = True
                End If
            Else
                csvModeloForm.IsValid = True
            End If

            If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) Then
                If txtFechaInicio.Text.Trim.Equals(String.Empty) OrElse Not IsDate(txtFechaInicio.Text) Then

                    strErro.Append(csvFechaInicio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvFechaInicio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtFechaInicio.Focus()
                        focoSetado = True
                    End If
                Else
                    csvFechaInicio.IsValid = True
                End If
            End If

            If puntosServicioGrid Is Nothing OrElse puntosServicioGrid.Count = 0 Then

                strErro.Append(csvAddPtoServ.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvAddPtoServ.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    ucClientesPtoServ.Focus()
                    focoSetado = True
                End If
            ElseIf puntosServicioGrid.Count > 0 Then
                csvAddPtoServ.IsValid = True

                For Each objPuntoServicioGrid As PuntoServicioGrid In puntosServicioGrid

                    Dim completo As Boolean = objPuntoServicioGrid.completo
                    Dim codCliente As String = objPuntoServicioGrid.codCliente
                    Dim codSubcliente As String = objPuntoServicioGrid.codSubcliente
                    Dim codPtoServicio As String = objPuntoServicioGrid.codPtoServicio
                    If Not completo Then
                        strErro.Append(String.Format(MyBase.RecuperarValorDic("MSG_INFO_COMBINATORIA_SIN_CODIGO_AJENO"), codCliente, codSubcliente, codPtoServicio) & Aplicacao.Util.Utilidad.LineBreak)
                        csvAddPtoServ.IsValid = False
                        Exit For
                    End If
                Next

            End If

            If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) Then

                If Planificacion Is Nothing OrElse String.IsNullOrEmpty(Planificacion.Identificador) Then
                    strErro.Append(csvAddPlanificacion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvAddPlanificacion.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        btnAddPlanificacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvAddPlanificacion.IsValid = True
                End If
            Else
                csvAddPlanificacion.IsValid = True
            End If

            If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) AndAlso csvFechaInicio.IsValid AndAlso csvAddPlanificacion.IsValid Then


                Dim planificacion_fecha_ini As DateTime? = Nothing
                Dim planificacion_fecha_fin As DateTime? = Nothing
                Dim pantalla_mae_fecha_ini As DateTime? = Nothing
                Dim pantalla_mae_fecha_fin As DateTime? = Nothing

                planificacion_fecha_ini = Planificacion.FechaHoraVigenciaInicio
                pantalla_mae_fecha_ini = Convert.ToDateTime(txtFechaInicio.Text)

                If pantalla_mae_fecha_ini Is Nothing OrElse pantalla_mae_fecha_ini = DateTime.MinValue OrElse
                    planificacion_fecha_ini Is Nothing OrElse planificacion_fecha_ini = DateTime.MinValue Then
                    strErro.Append(RecuperarValorDic("MSG_INFO_FYHS_VALOR_INVALIDAS") & Aplicacao.Util.Utilidad.LineBreak)
                End If

                'Validamos la fecha de vigencia de inicio para fecha valor con confirmación
                Dim planificacionSeleccionada = TiposPlanifiacion.FirstOrDefault(Function(x) x.oidTipoPlanificacion = ddlTipoPlanificacion.SelectedValue)
                If planificacionSeleccionada IsNot Nothing AndAlso planificacionSeleccionada.codTipoPlanificacion = "FECHA_VALOR_CONFIR" Then
                    'Maquina existente
                    If Maquina.OidMaquina IsNot Nothing Then
                        'No tenía planificación
                        If Maquina.Planificacion Is Nothing Then
                            If pantalla_mae_fecha_ini < fechaActual Then
                                strErro.Append(RecuperarValorDic("MSG_INFO_FYH_INICIO_VIGENCIA") & Aplicacao.Util.Utilidad.LineBreak)
                            End If
                        Else
                            'Cambió de planificación
                            If Maquina.Planificacion.Identificador <> Planificacion.Identificador Then
                                If pantalla_mae_fecha_ini < fechaActual Then
                                    strErro.Append(RecuperarValorDic("MSG_INFO_FYH_INICIO_VIGENCIA") & Aplicacao.Util.Utilidad.LineBreak)
                                End If
                            Else
                                If pantalla_mae_fecha_ini <> Maquina.FechaValorInicio Then
                                    strErro.Append(RecuperarValorDic("MSG_INFO_FYH_INICIO_VIGENCIA_2") & Aplicacao.Util.Utilidad.LineBreak)
                                End If
                            End If
                        End If
                    Else 'Maquina nueva
                        If pantalla_mae_fecha_ini < fechaActual Then
                            strErro.Append(RecuperarValorDic("MSG_INFO_FYH_INICIO_VIGENCIA") & Aplicacao.Util.Utilidad.LineBreak)
                        End If
                    End If
                End If

                If String.IsNullOrEmpty(strErro.ToString) Then

                    If Planificacion.FechaHoraVigenciaFin IsNot Nothing AndAlso Planificacion.FechaHoraVigenciaFin <> DateTime.MinValue Then
                        planificacion_fecha_fin = Planificacion.FechaHoraVigenciaFin
                    End If

                    If Not String.IsNullOrEmpty(txtFechaFin.Text) AndAlso Convert.ToDateTime(txtFechaFin.Text) <> DateTime.MinValue Then
                        pantalla_mae_fecha_fin = Convert.ToDateTime(txtFechaFin.Text)

                        If pantalla_mae_fecha_ini >= pantalla_mae_fecha_fin Then
                            strErro.Append(RecuperarValorDic("MSG_INFO_FYHS_VALOR_INVALIDAS") & Aplicacao.Util.Utilidad.LineBreak)
                        End If
                    End If

                    If String.IsNullOrEmpty(strErro.ToString) Then

                        If pantalla_mae_fecha_fin Is Nothing AndAlso planificacion_fecha_fin IsNot Nothing Then
                            pantalla_mae_fecha_fin = planificacion_fecha_fin
                            txtFechaFin.Text = planificacion_fecha_fin
                        End If

                        'Validar inicio
                        If pantalla_mae_fecha_ini < planificacion_fecha_ini Then
                            csvFechaInicio.IsValid = False
                        End If

                        'Validar fin
                        ' 1) Si hay problemas con la fecha inicio, no es necesario validar la fecha fin
                        ' 2) Si la fecha fin de la planificacion es null, cualquier data fin de la mae es valida
                        ' 3) Si la fecha fin de la mae es null, es necesario solamente la validacion de la fecha inicio
                        ' 4) Si tenemos fecha fin de planificacion y de mae, entonces la de MAE tiene que ser menor que la de planificacion 
                        If csvFechaInicio.IsValid AndAlso planificacion_fecha_fin IsNot Nothing AndAlso
                            pantalla_mae_fecha_fin IsNot Nothing AndAlso planificacion_fecha_fin < pantalla_mae_fecha_fin Then
                            csvFechaFin.IsValid = False
                        End If

                        If Not csvFechaInicio.IsValid OrElse Not csvFechaFin.IsValid Then

                            Dim aux As DateTime
                            Dim s_planificacion_fecha_ini As String = String.Empty
                            Dim s_planificacion_fecha_fin As String = String.Empty
                            Dim s_pantalla_mae_fecha_ini As String = String.Empty
                            Dim s_pantalla_mae_fecha_fin As String = String.Empty

                            If planificacion_fecha_ini IsNot Nothing AndAlso planificacion_fecha_ini <> DateTime.MinValue Then
                                aux = planificacion_fecha_ini
                                s_planificacion_fecha_ini = aux.ToString("dd/MM/yyyy HH:mm:ss")
                            End If
                            If planificacion_fecha_fin IsNot Nothing AndAlso planificacion_fecha_fin <> DateTime.MinValue Then
                                aux = planificacion_fecha_fin
                                s_planificacion_fecha_fin = aux.ToString("dd/MM/yyyy HH:mm:ss")
                            Else
                                s_planificacion_fecha_fin = MyBase.RecuperarValorDic("MSG_INFO_SIN_FYH_FIN_VIGENCIA")
                            End If
                            If pantalla_mae_fecha_ini IsNot Nothing AndAlso pantalla_mae_fecha_ini <> DateTime.MinValue Then
                                aux = pantalla_mae_fecha_ini
                                s_pantalla_mae_fecha_ini = aux.ToString("dd/MM/yyyy HH:mm:ss")
                            End If
                            If pantalla_mae_fecha_fin IsNot Nothing AndAlso pantalla_mae_fecha_fin <> DateTime.MinValue Then
                                aux = pantalla_mae_fecha_fin
                                s_pantalla_mae_fecha_fin = aux.ToString("dd/MM/yyyy HH:mm:ss")
                            Else
                                s_pantalla_mae_fecha_fin = MyBase.RecuperarValorDic("MSG_INFO_SIN_FYH_FIN_VIGENCIA")
                            End If

                            strErro.Append(String.Format(MyBase.RecuperarValorDic("MSG_INFO_PERIODOS_INVALIDOS"),
                                                         "'" & s_pantalla_mae_fecha_ini & "'",
                                                         "'" & s_pantalla_mae_fecha_fin & "'",
                                                         "'" & s_planificacion_fecha_ini & "'",
                                                         "'" & s_planificacion_fecha_fin & "'") & Aplicacao.Util.Utilidad.LineBreak)
                        End If

                    End If

                End If


                If String.IsNullOrEmpty(strErro.ToString) AndAlso pantalla_mae_fecha_ini IsNot Nothing Then
                    'verifica se a data está na quantidade de dias permitidas
                    Dim dias As Int16 = 30
                    If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then
                        Dim parametro = Parametros.Where(Function(p) p.codigoParametro = Comon.Constantes.COD_MAE_LIMITE_DIAS_ANTERIORES_FECHA_INICIO_VIGENCIA).FirstOrDefault
                        If parametro IsNot Nothing Then
                            Int16.TryParse(parametro.valorParametro, dias)

                            If dias = 0 Then
                                dias = 30
                            End If
                        End If
                    End If

                    If Maquina IsNot Nothing Then
                        'verfica se a data inicio foi alterada
                        If pantalla_mae_fecha_ini <> Maquina.FechaValorInicio Then
                            If pantalla_mae_fecha_ini < DateTime.Now.AddDays(-dias) Then
                                strErro.Append(String.Format(RecuperarValorDic("MSG_PERIODO_MENOR"), DateTime.Now.AddDays(-dias).ToString("dd/MM/yyyy HH:mm:ss")) & Aplicacao.Util.Utilidad.LineBreak)
                            Else
                                'Verifica se já existe um periodo gerado, se existir não poderá alterar a data
                                Dim dtVigenciaPlanMae = Prosegur.Genesis.LogicaNegocio.Genesis.Planificacion.RecuperarVigenciaPlanificacionMAE(Maquina.OidMaquina)
                                If dtVigenciaPlanMae IsNot Nothing AndAlso dtVigenciaPlanMae.Rows.Count > 0 Then
                                    If dtVigenciaPlanMae.Rows(0)("FYH_ULTIMO_PERIODO") IsNot DBNull.Value Then
                                        strErro.Append(RecuperarValorDic("MSG_PERIODO_GENERADO") & Aplicacao.Util.Utilidad.LineBreak)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            If divDesplazado.Visible AndAlso pnUcClientesPtoServMono.Enabled Then
                strErro.Append(MyBase.RecuperarValorDic("MSG_INFO_CUENTA_SALDO_DESPLAZADO"))
            End If
        End If

        Return strErro.ToString

    End Function

    Private Sub ExecutarCancelar()

        If Modo = "modal" Then
            Session("objResultModalMae") = Nothing
            Return
        End If
        btnNovo.Enabled = True
        btnBajaConfirm.Visible = False
        btnCancelar.Enabled = False
        btnGrabar.Enabled = False
        pnForm.Enabled = False
        pnForm.Visible = False
        Acao = Utilidad.eAcao.Inicial
        LimpiarForm()
        HabilitarDesabilitaForm(True)
    End Sub

    Private Function DadosModificados() As Boolean
        If Maquina IsNot Nothing Then
            If ddlDelegacionForm.SelectedValue <> Maquina.OidDelegacion Then
                Return True
            End If
            If ddlPlantaForm.SelectedValue <> Maquina.OidPlanta Then
                Return True
            End If
            If txtDeviceIDForm.Text <> Maquina.DeviceID Then
                Return True
            End If
            If txtDescricaoForm.Text <> Maquina.Descripcion Then
                Return True
            End If
            If ddlMarcaForm.SelectedValue <> Maquina.OidFabricante Then
                Return True
            End If
            If ddlModeloForm.SelectedValue <> Maquina.OidModelo Then
                Return True
            End If
            If Not String.IsNullOrEmpty(ddlTipoPlanificacion.SelectedValue) <> Maquina.FechaValor Then
                Return True
            End If
            If chkConsideraRecuentos.Checked <> Maquina.ConsideraRecuentos Then
                Return True
            End If
            If (String.IsNullOrEmpty(txtFechaInicio.Text) AndAlso Maquina.FechaValorInicio <> DateTime.MinValue) _
                OrElse (Not String.IsNullOrEmpty(txtFechaInicio.Text) AndAlso Maquina.FechaValorInicio = DateTime.MinValue) Then
                Return True
            End If
            If (String.IsNullOrEmpty(txtFechaFin.Text) AndAlso Maquina.FechaValorFin <> DateTime.MinValue) _
                OrElse (Not String.IsNullOrEmpty(txtFechaFin.Text) AndAlso Maquina.FechaValorFin = DateTime.MinValue) Then
                Return True
            End If
            If Not String.IsNullOrEmpty(txtFechaInicio.Text) AndAlso txtFechaInicio.Text <> Maquina.FechaValorInicio Then
                Return True
            End If
            If Not String.IsNullOrEmpty(txtFechaFin.Text) AndAlso txtFechaFin.Text <> Maquina.FechaValorFin Then
                Return True
            End If
            If ((Maquina.Planificacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(Maquina.Planificacion.Identificador)) AndAlso Planificacion Is Nothing) _
                OrElse ((Maquina.Planificacion Is Nothing OrElse String.IsNullOrEmpty(Maquina.Planificacion.Identificador)) AndAlso Planificacion IsNot Nothing) Then
                Return True
            End If
            If Maquina.Planificacion IsNot Nothing AndAlso Planificacion IsNot Nothing Then
                If Maquina.Planificacion.Identificador <> Planificacion.Identificador Then
                    Return True
                End If
            End If
            If DatosBancarios IsNot Nothing AndAlso DatosBancarios.Count > 0 Then
                Return True
            End If
            If ((Maquina.PuntosServicio IsNot Nothing OrElse Maquina.PuntosServicio.Count > 0) AndAlso (puntosServicioGrid Is Nothing OrElse puntosServicioGrid.Count = 0)) _
                OrElse ((Maquina.PuntosServicio Is Nothing OrElse Maquina.PuntosServicio.Count = 0) AndAlso (puntosServicioGrid IsNot Nothing OrElse puntosServicioGrid.Count > 0)) Then
                Return True
            End If
            If Maquina.PuntosServicio IsNot Nothing AndAlso puntosServicioGrid IsNot Nothing Then
                If Aplicacao.Util.Utilidad.HayModificaciones(puntosServicioGridMaquina, puntosServicioGrid) Then
                    Return True
                End If
            End If
        End If
    End Function

    Private Sub CarregaDados(oidMaquina As String)
        Try
            Dim objRespuesta As New IAC.ContractoServicio.Maquina.GetMaquinaDetalle.Respuesta
            objRespuesta = GetBusquedaDetalle(oidMaquina)

            ' tratar retorno
            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Return
            End If

            If objRespuesta.Maquina IsNot Nothing Then
                Maquina = objRespuesta.Maquina

                ddlTipoPlanificacion.SelectedValue = Maquina.Planificacion.TipoPlanificacion.Identificador
                tipoPlanificacionSelectedValue = ddlTipoPlanificacion.SelectedValue

                ddlDelegacionForm.SelectedValue = Maquina.OidDelegacion
                ddlDelegacionForm_SelectedIndexChanged(Nothing, Nothing)
                ddlPlantaForm.SelectedValue = Maquina.OidPlanta
                txtDeviceIDForm.Text = Maquina.DeviceID
                txtDescricaoForm.Text = Maquina.Descripcion
                ddlMarcaForm.SelectedValue = Maquina.OidFabricante
                ddlMarcaForm_SelectedIndexChanged(Nothing, Nothing)
                ddlModeloForm.SelectedValue = Maquina.OidModelo

                HabilitarDesabilitaPlanificacion()

                chkVigenteForm.Checked = Maquina.BolActivo
                chkConsideraRecuentos.Checked = Maquina.ConsideraRecuentos
                chkMultiClientes.Checked = Maquina.MultiClientes
                divPorComisionCliente.Visible = Not Maquina.MultiClientes
                txtFechaInicio.Text = IIf(Maquina.FechaValorInicio = DateTime.MinValue, String.Empty, Maquina.FechaValorInicio.ToString("dd/MM/yyyy HH:mm:ss"))
                txtFechaFin.Text = IIf(Maquina.FechaValorFin = DateTime.MinValue, String.Empty, Maquina.FechaValorFin.ToString("dd/MM/yyyy HH:mm:ss"))

                If Maquina.PuntosServicio IsNot Nothing Then
                    For Each objCliente In Maquina.PuntosServicio
                        AddPuntoServicio(objCliente)
                    Next
                    puntosServicioGridMaquina = puntosServicioGrid
                End If

                If Maquina.Planificacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(Maquina.Planificacion.Identificador) Then
                    Planificacion = Maquina.Planificacion

                    'Se oculta el botón transacciones y se deshabilita la fecha de inicio de vigencia para planificacion de 
                    'fecha valor con confirmación
                    btnTransacciones.Visible = Maquina.Planificacion.TipoPlanificacion IsNot Nothing AndAlso Maquina.Planificacion.TipoPlanificacion.Codigo <> "FECHA_VALOR_CONFIR"
                    txtFechaInicio.Enabled = Maquina.Planificacion.TipoPlanificacion IsNot Nothing AndAlso Maquina.Planificacion.TipoPlanificacion.Codigo <> "FECHA_VALOR_CONFIR"

                    ddlTipoPlanificacion.SelectedValue = Maquina.Planificacion.TipoPlanificacion.Identificador
                    tipoPlanificacionSelectedValue = ddlTipoPlanificacion.SelectedValue

                    HoraDelegacion(Planificacion)
                    txtPlanificacion.Text = Planificacion.Descripcion.ToUpper()
                    chkVigentePlan.Checked = Planificacion.BolActivo
                    chkControlaFacturacion.Checked = Planificacion.BolControlaFacturacion
                Else
                    Maquina.Planificacion = Nothing
                End If



                Dim bancosSelecionados As New ObservableCollection(Of Comon.Clases.Cliente)
                If Maquina.Planificacion IsNot Nothing Then

                    If Maquina.BancoTesoreriaPlanxMaquina IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Maquina.BancoTesoreriaPlanxMaquina.Identificador) Then

                        If Maquina.CuentaTesoreriaPlanxMaquina IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Maquina.CuentaTesoreriaPlanxMaquina.Identificador) Then
                            Maquina.BancoTesoreriaPlanxMaquina.PuntosServicio = New ObservableCollection(Of PuntoServicio)
                            Maquina.BancoTesoreriaPlanxMaquina.PuntosServicio.Add(Maquina.CuentaTesoreriaPlanxMaquina)
                        End If
                        Maquina.Planificacion.Cliente.SubClientes = New ObservableCollection(Of SubCliente)
                        Maquina.Planificacion.Cliente.SubClientes.Add(Maquina.BancoTesoreriaPlanxMaquina)

                    End If

                    bancosSelecionados.Add(Maquina.Planificacion.Cliente)
                    BancoToTesoreria(bancosSelecionados)

                    AtualizaBancoSelecionado(bancosSelecionados)
                Else
                    AtualizaBancoSelecionado(bancosSelecionados)
                End If


                If chkMultiClientes.Checked Then
                    divMonocliente.Visible = False
                    divMulticliente.Visible = True
                Else
                    divMonocliente.Visible = True
                    divMulticliente.Visible = False
                    PuntosServicio()


                    If Acao = Utilidad.eAcao.Modificacion Then
                        If puntosServicioGrid.Count > 0 AndAlso puntosServicioGrid(0).desplazado Then
                            pnUcClientesPtoServMono.Enabled = False
                            divDesplazado.Visible = True
                        End If

                    End If
                End If

                ConfigurarControle_PuntoServicio()

                If Planificacion IsNot Nothing Then
                    BuscarFacturacion(Planificacion.Identificador)
                    CargaPlanificacionPatron(Planificacion)

                    'Limite
                    ucLimitePlanificacion.Planificacion = Planificacion
                    ucLimitePlanificacion.CargaRegistrosDeBase()
                End If

                upCanalesPatron.Update()
                txtPorComisionMaquina.Text = RetornaDecimal(Maquina.PorcComisionMaquina)

                'buscar PlanxMaquina
                PlanesMAExCanalesSubCanalesPuntos = Maquina.Planes
                gridPlanes.DataSource = Maquina.Planes
                gridPlanes.DataBind()
                LimpiarPlanificacionMAE()

                'Limite
                ucLimiteMae.Maquina = New Maquina With {.Identificador = Maquina.OidMaquina}
                ucLimiteMae.CargaRegistrosDeBase()

            End If
        Catch exNeg As Excepcion.NegocioExcepcion

            If exNeg.Descricao.Equals("msgDelegacionSinCodigoAjenoMAE") Then
                MyBase.MostraMensagem(MyBase.RecuperarValorDic("msgDelegacionSinCodigoAjenoMAE"))
            Else
                MyBase.MostraMensagem(String.Format("{0} - {1}", exNeg.Codigo, exNeg.Descricao))
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



    Private Sub BuscarFacturacion(oidPlanificacion As String)

        If Not String.IsNullOrWhiteSpace(oidPlanificacion) Then

            Dim objLogicaNegocio As New LogicaNegocio.AccionPlanificacion
            Dim objPeticion As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Planificacion.GetPlanificacionFacturacion.Respuesta
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False
            objPeticion.OidPlanificacion = Planificacion.Identificador

            If ddlDelegacionForm.SelectedValue IsNot Nothing AndAlso Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then
                objPeticion.OidDelegacion = ddlDelegacionForm.SelectedValue
            End If

            objRespuesta = objLogicaNegocio.GetPlanificacionFacturacion(objPeticion)

            If objRespuesta IsNot Nothing AndAlso objRespuesta.Planificacion IsNot Nothing Then

                If objRespuesta.Planificacion.BancoTesoreriaDelegacion IsNot Nothing Then
                    txtBancoTesoreriaPadron.Text = objRespuesta.Planificacion.BancoTesoreriaDelegacion.Codigo + " " + objRespuesta.Planificacion.BancoTesoreriaDelegacion.Descripcion
                End If

                If objRespuesta.Planificacion.CuentaTesoreriaDelegacion IsNot Nothing Then
                    txtCuentaTesoreriaPadron.Text = objRespuesta.Planificacion.CuentaTesoreriaDelegacion.Codigo + " " + objRespuesta.Planificacion.CuentaTesoreriaDelegacion.Descripcion
                End If
            End If
            If objRespuesta.Planificacion IsNot Nothing Then
                txtPorComisionPlanificacion.Text = RetornaDecimal(objRespuesta.Planificacion.PorcComisionPlanificacion)
            Else
                txtPorComisionPlanificacion.Text = ""
            End If

            Dim objPlan As New LogicaNegocio.AccionPlanificacion
            Dim objPeticionPlan As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Peticion
            Dim objRespuestaPlan As New IAC.ContractoServicio.Planificacion.GetPlanificaciones.Respuesta

            objPeticionPlan.OidPlanificacion = oidPlanificacion


            objPeticionPlan.ParametrosPaginacion.RealizarPaginacion = False
            objRespuestaPlan = objPlan.GetPlanificaciones(objPeticionPlan)

            If objRespuestaPlan IsNot Nothing AndAlso objRespuestaPlan.Planificacion IsNot Nothing AndAlso objRespuestaPlan.Planificacion.Count > 0 Then
                chkControlaFacturacion.Checked = objRespuestaPlan.Planificacion(0).BolControlaFacturacion


            End If

        Else
            txtBancoTesoreriaPadron.Text = String.Empty
            txtCuentaTesoreriaPadron.Text = String.Empty

        End If


    End Sub

    Private Function RetornaDecimal(numero As Decimal?) As String

        If numero Is Nothing Then
            Return ""
        End If
        Return numero.ToString()
    End Function

    Private Sub HoraDelegacion(planificacion As Planificacion)

        Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
        Dim objPeticion As New IAC.ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Delegacion.GetDelegacionByPlanificacion.Respuesta

        objPeticion.OID_PLANIFICACION = planificacion.Identificador

        objRespuesta = objAccionDelegacion.GetDelegacionByPlanificacion(objPeticion)

        If objRespuesta IsNot Nothing Then

            Dim info = GenerarInfoGmt(objRespuesta.Delegacion)

            Dim fechaHora = DateTime.UtcNow

            Dim delegacion = objRespuesta

            Dim fecha = fechaHora.QuieroExibirEstaFechaEnLaPatalla(objRespuesta.Delegacion)

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "ActualizarData", " ActualizarData('" + fecha.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + info + "');", True)
        End If

    End Sub

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

    Private Function ValidaSaldoPtoServicio(oidCliente As String, oidSubcliente As String, oidPtoServicio As String, oidSector As String, oidPlanta As String, Optional exibeMensagem As Boolean = True) As Boolean
        Dim filtro As New Comon.Clases.Transferencias.FiltroConsultaSaldo

        filtro.identificadoresClientes.Add(oidCliente)
        filtro.identificadoresSubClientes.Add(oidSubcliente)
        filtro.identificadoresPtoServicios.Add(oidPtoServicio)
        filtro.identificadoresSectores.Add(oidSector)
        filtro.identificadoresPlantas.Add(oidPlanta)

        Dim dtSaldo = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Saldo.ObtenerSaldoTotalSinCanalSF(filtro)
        If dtSaldo IsNot Nothing AndAlso dtSaldo.Rows.Count > 0 Then
            Dim soma = dtSaldo.Compute("SUM(NUM_IMPORTE)", String.Empty)
            If soma <> 0 Then
                If exibeMensagem Then
                    MyBase.MostraMensagemErro(MyBase.RecuperarValorDic("MSG_INFO_CUENTA_SALDO_DESPLAZADO"), Nothing)
                End If
                Return False
            End If
        End If
        Return True
    End Function

#End Region

#Region "[EVENTOS]"

    Private Sub btnHabilitarDeshabilitarPlanificacion_Click(sender As Object, e As EventArgs) Handles btnHabilitarDeshabilitarPlanificacion.Click
        Try
            tipoPlanificacionSelectedValue = ddlTipoPlanificacion.SelectedValue
            HabilitarDesabilitaPlanificacion()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Property tipoPlanificacionSelectedValue() As String
        Get
            Return Session("tipoPlanificacionSelectedValue")
        End Get
        Set(ByVal value As String)
            Session("tipoPlanificacionSelectedValue") = value
        End Set
    End Property

    Protected Sub ddlTipoPlanificacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoPlanificacion.SelectedIndexChanged
        Try
            ucBanco.TipoPlanificacion = ddlTipoPlanificacion.Text
            If Maquina IsNot Nothing AndAlso Maquina.Planificacion IsNot Nothing AndAlso ddlTipoPlanificacion.SelectedValue <> Maquina.Planificacion.TipoPlanificacion.Identificador AndAlso Not String.IsNullOrEmpty(Maquina.Planificacion.TipoPlanificacion.Identificador) Then

                Dim jsScriptSi As String = "ExecutarClick(" & Chr(34) & btnHabilitarDeshabilitarPlanificacion.ClientID & Chr(34) & ");"
                Dim jsScriptNo As String = "document.getElementById(" & Chr(34) & ddlTipoPlanificacion.ClientID & Chr(34) & ").value = " & Chr(34) & tipoPlanificacionSelectedValue & Chr(34) & ";"

                MyBase.ExibirMensagemNaoSim(MyBase.RecuperarValorDic("MSG_WARN_VACIA_PLANIFICACION"), jsScriptSi, jsScriptNo)

            Else
                HabilitarDesabilitaPlanificacion()
            End If

            'Ocultamos el botón de transacciones en caso de tratarse de una planificacion de Fecha Valor con Confirmación
            Dim planificacionSeleccionada = TiposPlanifiacion.FirstOrDefault(Function(x) x.oidTipoPlanificacion = ddlTipoPlanificacion.SelectedValue)
            If planificacionSeleccionada IsNot Nothing AndAlso planificacionSeleccionada.codTipoPlanificacion = "FECHA_VALOR_CONFIR" Then
                btnTransacciones.Visible = False
            Else
                btnTransacciones.Visible = True
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacion.SelectedIndexChanged
        If Not ddlDelegacion.SelectedValue.Equals(String.Empty) Then
            PreencherddlPlanta(ddlDelegacion.SelectedValue, False, True)
        Else
            PreencherddlPlanta(Nothing, False, True)
        End If
    End Sub

    Protected Sub ddlDelegacionForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacionForm.SelectedIndexChanged
        If Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then
            PreencherddlPlanta(ddlDelegacionForm.SelectedValue, True, False)

            If Acao.Equals(Utilidad.eAcao.Modificacion) AndAlso Maquina IsNot Nothing AndAlso Maquina.CumpleNombrePatron Then
                Dim oidDelegacion As String = ddlDelegacionForm.SelectedValue
                If Not String.IsNullOrEmpty(oidDelegacion) Then
                    Dim objDeleg As ContractoServicio.Delegacion.GetDelegacion.Respuesta
                    objDeleg = New LogicaNegocio.AccionDelegacion().GetDelegacionByIdentificador(oidDelegacion, Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor())
                    Dim codAjenoDeleg As String = objDeleg.Delegacion(0).CodDelegacionAjeno
                    Dim codDeviceID As String = Maquina.DeviceID.Substring(codAjenoDeleg.Length)
                    txtDeviceIDForm.Text = String.Format("{0}{1}", codAjenoDeleg, codDeviceID)
                End If


            End If
        Else
            PreencherddlPlanta(Nothing, True, False)
        End If


    End Sub

    Protected Sub ddlMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarca.SelectedIndexChanged
        If Not ddlMarca.SelectedValue.Equals(String.Empty) Then
            PreencherddlModelo(ddlMarca.SelectedValue, False, True)
        Else
            PreencherddlModelo(Nothing, False, True)
        End If
        ddlModelo.Enabled = ddlModelo.Items.Count > 1
    End Sub

    Protected Sub ddlMarcaForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarcaForm.SelectedIndexChanged
        If Not ddlMarcaForm.SelectedValue.Equals(String.Empty) Then
            PreencherddlModelo(ddlMarcaForm.SelectedValue, True, False)
        Else
            PreencherddlModelo(Nothing, True, False)
        End If
        ddlModeloForm.Enabled = ddlModeloForm.Items.Count > 1
    End Sub

    Private Function GetBusqueda() As IAC.ContractoServicio.Maquina.GetMaquina.Respuesta
        Dim objAccionMaquina As New LogicaNegocio.AccionMaquina
        Dim objPeticion As New IAC.ContractoServicio.Maquina.GetMaquina.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Maquina.GetMaquina.Respuesta

        objPeticion.OidDelegacion = ddlDelegacion.SelectedValue
        objPeticion.OidPlanta = ddlPlanta.SelectedValue

        If ucClientes.Clientes IsNot Nothing AndAlso ucClientes.Clientes.Count > 0 Then
            objPeticion.OidClientes = ucClientes.Clientes.Select(Function(c) c.Identificador).ToList()
            If ucClientes.Clientes.Any(Function(c) c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0) Then
                objPeticion.OidSubClientes = ucClientes.Clientes.SelectMany(Function(c) c.SubClientes).Select(Function(sc) sc.Identificador).ToList()
                If ucClientes.Clientes.SelectMany(Function(c) c.SubClientes).Any(Function(sc) sc.PuntosServicio IsNot Nothing AndAlso sc.PuntosServicio.Count > 0) Then
                    objPeticion.OidPuntoServicio = ucClientes.Clientes.SelectMany(Function(c) c.SubClientes).SelectMany(Function(sc) sc.PuntosServicio).Select(Function(pto) pto.Identificador).ToList()
                End If
            End If
        End If

        objPeticion.DeviceID = txtDeviceID.Text
        objPeticion.Descripcion = txtDescricao.Text
        objPeticion.OidFabricante = ddlMarca.SelectedValue
        objPeticion.OidModelo = ddlModelo.SelectedValue
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objAccionMaquina.GetMaquinas(objPeticion)

        Return objRespuesta
    End Function

    Private Function GetBusquedaDetalle(Optional oidMaquina As String = Nothing) As IAC.ContractoServicio.Maquina.GetMaquinaDetalle.Respuesta
        Dim objAccionMaquina As New LogicaNegocio.AccionMaquina
        Return objAccionMaquina.GetMaquinaDetalle(oidMaquina)
    End Function

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try

            Dim MensagemErro As String = MontaMensagensErro()

            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If

            ' setar ação de busca
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            Dim objRespuesta As New IAC.ContractoServicio.Maquina.GetMaquina.Respuesta

            objRespuesta = GetBusqueda()

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                MyBase.MostraMensagem(objRespuesta.MensajeError)
                Exit Sub
            End If

            ' define a ação de busca somente se houve retorno
            If objRespuesta.Maquinas IsNot Nothing AndAlso objRespuesta.Maquinas.Count > 0 Then
                'Verifica se a consulta não retornou mais registros que o permitido
                If objRespuesta.Maquinas.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                    MaquinasResultado = objRespuesta.Maquinas
                    pnlSemRegistro.Visible = False

                    ' converter objeto para datatable
                    Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.Maquinas)

                    If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                        objDt.DefaultView.Sort = " deviceId ASC"
                    ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                        If GdvResultado.SortCommand.Equals(String.Empty) Then
                            objDt.DefaultView.Sort = " deviceId ASC "
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

                End If

            Else
                MaquinasResultado = Nothing
                'Limpa a consulta
                GdvResultado.DataSource = Nothing
                GdvResultado.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Tradutor.Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub ProsegurGridViewProcesso_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(GetBusqueda().Maquinas)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " deviceID ASC "
        Else
            objDT.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = objDT

    End Sub



    Private Sub GdvResultado_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                Dim imgEditar = CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton)
                Dim imgConsultar = CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton)
                Dim imgExcluir = CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton)

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("oidMaquina")) & "');"
                imgEditar.OnClientClick = jsScript
                imgConsultar.OnClientClick = jsScript
                imgExcluir.OnClientClick = jsScript
                imgEditar.ToolTip = Tradutor.Traduzir("btnModificacion")
                imgConsultar.ToolTip = Tradutor.Traduzir("btnConsulta")
                imgExcluir.ToolTip = Tradutor.Traduzir("btnBaja")

                If CBool(e.Row.DataItem("bolActivo")) Then
                    CType(e.Row.Cells(9).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                    imgExcluir.Enabled = True
                Else
                    imgExcluir.Enabled = False
                    CType(e.Row.Cells(9).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

                If CBool(e.Row.DataItem("fechaValor")) Then
                    CType(e.Row.Cells(10).FindControl("imgFechaValor"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(10).FindControl("imgFechaValor"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

                imgEditar.Visible = PodeModificar
                imgConsultar.Visible = PodeConsultar
                imgExcluir.Visible = PodeDeletar
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

                Acao = Utilidad.eAcao.Modificacion
                LimpiarForm(True)
                CarregaDados(Server.UrlDecode(id))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                pnForm.Enabled = True
                pnForm.Visible = True
                chkVigenteForm.Enabled = Not Maquina.BolActivo
                chkConsideraRecuentos.Enabled = True
                HabilitarDesabilitaForm(True)
                ddlDelegacionForm.Enabled = True
                ddlPlantaForm.Enabled = True
                'btnTransacciones.Enabled = True
                ddlTipoPlanificacion.Enabled = True
                ucBanco.HabilitaDesabilitaCampos(True)
                If EsModal Then
                    ddlTipoPlanificacion.Enabled = False
                    pnUcBancoform.Enabled = False
                End If

                'Limite
                ucLimiteMae.Modo = Comon.Enumeradores.Modo.Alta
                ucLimiteMae.ConfigurarControles()

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

                Acao = Utilidad.eAcao.Consulta
                LimpiarForm(True)

                CarregaDados(Server.UrlDecode(id))
                btnBajaConfirm.Visible = False
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                pnForm.Enabled = True
                pnForm.Visible = True
                chkVigenteForm.Enabled = False
                chkConsideraRecuentos.Enabled = False
                btnTransacciones.Enabled = False
                HabilitarDesabilitaForm(False)
                ddlTipoPlanificacion.Enabled = False
                ucBanco.HabilitaDesabilitaCampos(False)
                If EsModal Then
                    ddlTipoPlanificacion.Enabled = False
                    pnUcBancoform.Enabled = False
                End If

                'Limite
                ucLimiteMae.Modo = Comon.Enumeradores.Modo.Consulta
                ucLimiteMae.ConfigurarControles()
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

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

                LimpiarForm()
                CarregaDados(id)
                btnBajaConfirm.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                chkConsideraRecuentos.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                HabilitarDesabilitaForm(False)
                btnTransacciones.Enabled = False

                'Limite
                ucLimiteMae.Modo = Comon.Enumeradores.Modo.Consulta
                ucLimiteMae.ConfigurarControles()
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgCodigoAjenoMono_OnClick(sender As Object, e As ImageClickEventArgs)

        Try
            If ucClientesPtoServMono.Clientes IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault() IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault() IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then

                Dim id As String = ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Identificador

                Dim objCliente = ucClientesPtoServMono.Clientes.First
                MaquinaPuntoServicio = Nothing
                AddPuntoServicio(objCliente)



                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCodigosAjenosMAE.CodigoAjenoSimples


                Dim objRespDel As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
                Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
                objRespDel = objAccionDelegacion.GetDelegacionByIdentificador(ddlDelegacionForm.SelectedValue.ToString, "MAE")

                Dim codDelegacionMaquina = ""
                If objRespDel IsNot Nothing And objRespDel.Delegacion IsNot Nothing And objRespDel.Delegacion(0) IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno) Then
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno
                    Else
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacion
                    End If
                End If

                tablaGenesis.CodDelegacion = codDelegacionMaquina
                tablaGenesis.OidCliente = objPtoServicio.oidCliente
                tablaGenesis.CodCliente = objPtoServicio.codCliente
                tablaGenesis.DesCliente = objPtoServicio.desCliente
                tablaGenesis.OidSubcliente = objPtoServicio.oidSubcliente
                tablaGenesis.CodSubcliente = objPtoServicio.codSubcliente
                tablaGenesis.DesSubcliente = objPtoServicio.desSubcliente
                tablaGenesis.OidPuntoServicio = objPtoServicio.oidPtoServicio
                tablaGenesis.CodPuntoServicio = objPtoServicio.codPtoServicio
                tablaGenesis.DesPuntoServicio = objPtoServicio.desPtoServicio
                tablaGenesis.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoCliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoCliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoCliente}
                tablaGenesis.CodigoAjenoSubcliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoSubcliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoSubcliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoSubcliente}
                tablaGenesis.CodigoAjenoPuntoServicio = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoPtoServicio,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoPtoServicio,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoPtoServicio}
                tablaGenesis.OidSectorMAE = identificadorSectorMae
                tablaGenesis.CodigoSectorMAE = codigoSectorMae


                Session("objMantenimientoCodigosAjenosMAE") = tablaGenesis

                url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & MyBase.Acao
                If (Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                    url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_cod_ajeno_titulo"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


    End Sub


    Protected Sub imgCapitalDBancariosForm_OnClick(sender As Object, e As ImageClickEventArgs)

        Try
            If ucClientesPtoServMono.Clientes IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault() IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault() IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso
                 ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then

                Dim id As String = ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Identificador

                Dim objCliente = ucClientesPtoServMono.Clientes.First
                MaquinaPuntoServicio = Nothing
                AddPuntoServicio(objCliente)


                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

                tablaGenesis.OidCliente = objPtoServicio.oidCliente
                tablaGenesis.CodCliente = objPtoServicio.codCliente
                tablaGenesis.DesCliente = objPtoServicio.desCliente
                tablaGenesis.OidSubcliente = objPtoServicio.oidSubcliente
                tablaGenesis.CodSubcliente = objPtoServicio.codSubcliente
                tablaGenesis.DesSubcliente = objPtoServicio.desSubcliente
                tablaGenesis.OidPuntoServicio = objPtoServicio.oidPtoServicio
                tablaGenesis.CodPuntoServicio = objPtoServicio.codPtoServicio
                tablaGenesis.DesPuntoServicio = objPtoServicio.desPtoServicio
                If DatosBancarios Is Nothing Then
                    DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
                End If
                If DatosBancarios.ContainsKey(id) Then
                    tablaGenesis.DatosBancarios = DatosBancarios(id)
                End If

                Session("objMantenimientoCamposExtras") = tablaGenesis


                url = "MantenimientoCamposExtra.aspx?acao=" & MyBase.Acao
                If (Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                    url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                End If



                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtras.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Protected Sub imgCodigoAjeno2_OnClick(sender As Object, e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            If Not String.IsNullOrEmpty(e.CommandArgument) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(e.CommandArgument) Then
                    id = e.CommandArgument
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCodigosAjenosMAE.CodigoAjenoSimples


                Dim objRespDel As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
                Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
                objRespDel = objAccionDelegacion.GetDelegacionByIdentificador(ddlDelegacionForm.SelectedValue.ToString, "MAE")

                Dim codDelegacionMaquina = ""
                If objRespDel IsNot Nothing And objRespDel.Delegacion IsNot Nothing And objRespDel.Delegacion(0) IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno) Then
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno
                    Else
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacion
                    End If
                End If

                tablaGenesis.CodDelegacion = codDelegacionMaquina
                tablaGenesis.OidCliente = objPtoServicio.oidCliente
                tablaGenesis.CodCliente = objPtoServicio.codCliente
                tablaGenesis.DesCliente = objPtoServicio.desCliente
                tablaGenesis.OidSubcliente = objPtoServicio.oidSubcliente
                tablaGenesis.CodSubcliente = objPtoServicio.codSubcliente
                tablaGenesis.DesSubcliente = objPtoServicio.desSubcliente
                tablaGenesis.OidPuntoServicio = objPtoServicio.oidPtoServicio
                tablaGenesis.CodPuntoServicio = objPtoServicio.codPtoServicio
                tablaGenesis.DesPuntoServicio = objPtoServicio.desPtoServicio
                tablaGenesis.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoCliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoCliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoCliente}
                tablaGenesis.CodigoAjenoSubcliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoSubcliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoSubcliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoSubcliente}
                tablaGenesis.CodigoAjenoPuntoServicio = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoPtoServicio,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoPtoServicio,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoPtoServicio}
                tablaGenesis.OidSectorMAE = identificadorSectorMae
                tablaGenesis.CodigoSectorMAE = codigoSectorMae


                Session("objMantenimientoCodigosAjenosMAE") = tablaGenesis

                If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                    url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                Else
                    url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_cod_ajeno_titulo"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub





    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try

            ValidarCamposObrigatorios = False

            MaquinasResultado = Nothing
            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            LimparCamposFiltro()

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            pnlSemRegistro.Visible = False

            btnCancelar_Click(sender, e)

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeCancelar.ClientID & Chr(34) & ");"

            If DadosModificados() Then
                MyBase.ExibirMensagemSimNao(RecuperarValorDic("MSG_INFO_DESECHAR_CAMBIOS"), jsScript)
            Else
                btnConsomeCancelar_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeNovo.ClientID & Chr(34) & ");"

            If DadosModificados() Then
                MyBase.ExibirMensagemSimNao(RecuperarValorDic("MSG_INFO_DESECHAR_CAMBIOS"), jsScript)
            Else
                btnConsomeNovo_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnAddPtoServ_Click(sender As Object, e As EventArgs) Handles btnAddPtoServ.Click
        Try

            ddlTipoPlanificacion.Enabled = True
            ucBanco.HabilitaDesabilitaCampos(True)
            Dim MensagemErro As String = String.Empty
            If chkMultiClientes.Checked Then
                MensagemErro = MontaMensagensErroPtoServ(ucClientesPtoServ.Clientes)
            Else
                MensagemErro = MontaMensagensErroPtoServ(ucClientesPtoServMono.Clientes)
            End If
            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If
            If chkMultiClientes.Checked Then
                If ucClientesPtoServ.Clientes.Count > 0 Then
                    'Valida se ponto de serviço já está vinculado a alguma máquina
                    Dim objCliente = ucClientesPtoServ.Clientes.First
                    Dim objSubcliente = objCliente.SubClientes.First
                    Dim objPuntoServicio = objSubcliente.PuntosServicio.First
                    MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPuntoServicio.Identificador)

                    If MaquinaPuntoServicio IsNot Nothing AndAlso (Maquina Is Nothing OrElse Maquina.OidMaquina <> MaquinaPuntoServicio.Identificador) Then
                        If ValidaSaldoPtoServicio(objCliente.Identificador,
                                                objSubcliente.Identificador,
                                                objPuntoServicio.Identificador,
                                                MaquinaPuntoServicio.Sector.Identificador,
                                                MaquinaPuntoServicio.Delegacion.Plantas.First.Identificador) Then
                            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeAddPtoServ.ClientID & Chr(34) & ");"

                            MyBase.ExibirMensagemSimNao(String.Format(RecuperarValorDic("MSG_INFO_PUNTO_SERVICIO_RELACIONADO"), objPuntoServicio.Codigo, objPuntoServicio.Descripcion, MaquinaPuntoServicio.Codigo), jsScript)
                        End If
                    Else
                        btnConsomeAddPtoServ_Click(Nothing, Nothing)
                    End If
                End If

            Else
                If ucClientesPtoServMono.Clientes.Count > 0 Then
                    'Valida se ponto de serviço já está vinculado a alguma máquina
                    Dim objCliente = ucClientesPtoServMono.Clientes.First
                    Dim objSubcliente = objCliente.SubClientes.First
                    Dim objPuntoServicio = objSubcliente.PuntosServicio.First
                    MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPuntoServicio.Identificador)

                    If MaquinaPuntoServicio IsNot Nothing AndAlso (Maquina Is Nothing OrElse Maquina.OidMaquina <> MaquinaPuntoServicio.Identificador) Then
                        If ValidaSaldoPtoServicio(objCliente.Identificador,
                                                objSubcliente.Identificador,
                                                objPuntoServicio.Identificador,
                                                MaquinaPuntoServicio.Sector.Identificador,
                                                MaquinaPuntoServicio.Delegacion.Plantas.First.Identificador) Then
                            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeAddPtoServ.ClientID & Chr(34) & ");"

                            MyBase.ExibirMensagemSimNao(String.Format(RecuperarValorDic("MSG_INFO_PUNTO_SERVICIO_RELACIONADO"), objPuntoServicio.Codigo, objPuntoServicio.Descripcion, MaquinaPuntoServicio.Codigo), jsScript)
                        End If
                    Else
                        btnConsomeAddPtoServ_Click(Nothing, Nothing)
                    End If
                End If
            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
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

    Private Sub AtualizaDadosHelperCliente2(observableCollection As ObservableCollection(Of Comon.Clases.Cliente), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucCliente)
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



    Protected Sub btnAddPlanificacion_Click(sender As Object, e As EventArgs) Handles btnAddPlanificacion.Click
        Try

            Dim url As String = String.Empty
            Dim tablaGenesis As New BusquedaPlanificacionPopup.Planificacion

            tablaGenesis.Planificacion = Planificacion

            Session("objBusquedaPlanificacion") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "BusquedaPlanificacionPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "BusquedaPlanificacionPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_planificacion_titulo"), 500, 850, False, True, btnConsomePlanificacion.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnTransacciones_Click(sender As Object, e As EventArgs) Handles btnTransacciones.Click
        Try
            If Not String.IsNullOrWhiteSpace(Me.txtDeviceIDForm.Text) Then
                Dim url As String = "BusquedaTransaccionesPopup.aspx?MAE=" & Me.txtDeviceIDForm.Text
                Master.ExibirModal(url, Tradutor.Traduzir("029_titulo_transaciones"), 500, 950, False, True, btnConsomeTransacciones.ClientID)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnConsomeTransacciones_Click(sender As Object, e As EventArgs) Handles btnConsomeTransacciones.Click
        Try
            If Session("FechaGestion") IsNot Nothing Then
                If Not String.IsNullOrWhiteSpace(Session("FechaGestion")) Then
                    Me.txtFechaInicio.Text = Session("FechaGestion")
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AddPlanificacion(bancoSelecionado As ObservableCollection(Of Comon.Clases.Cliente))
        Try

            Dim url As String = String.Empty
            Dim tablaGenesis As New BusquedaPlanificacionPopup.Planificacion

            tablaGenesis.BancoSelecionados = bancoSelecionado
            tablaGenesis.Planificacion = Planificacion
            tablaGenesis.OidTipoPlanificacao = ddlTipoPlanificacion.SelectedValue

            Session("objBusquedaPlanificacion") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                url = "BusquedaPlanificacionPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            Else
                url = "BusquedaPlanificacionPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
            End If

            Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_planificacion_titulo"), 500, 850, False, True, btnConsomePlanificacion.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespMantenimientoCodigosAjenosMAE") IsNot Nothing Then

                Dim tablaGenesis As MantenimientoCodigosAjenosMAE.CodigoAjenoSimples = Session("objRespMantenimientoCodigosAjenosMAE")

                Dim objRespDel As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
                Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
                objRespDel = objAccionDelegacion.GetDelegacionByIdentificador(ddlDelegacionForm.SelectedValue.ToString, "MAE")

                Dim codDelegacionMaquina = ""
                If objRespDel IsNot Nothing And objRespDel.Delegacion IsNot Nothing And objRespDel.Delegacion(0) IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno) Then
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno
                    Else
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacion
                    End If
                End If

                If tablaGenesis IsNot Nothing Then
                    Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = tablaGenesis.OidPuntoServicio)
                    If objPtoServicio IsNot Nothing Then
                        objPtoServicio.codCodigoAjenoCliente = tablaGenesis.CodigoAjenoCliente.CodAjeno
                        objPtoServicio.desCodigoAjenoCliente = tablaGenesis.CodigoAjenoCliente.DesAjeno
                        objPtoServicio.codCodigoAjenoSubcliente = tablaGenesis.CodigoAjenoSubcliente.CodAjeno
                        objPtoServicio.desCodigoAjenoSubcliente = tablaGenesis.CodigoAjenoSubcliente.DesAjeno
                        objPtoServicio.codCodigoAjenoPtoServicio = tablaGenesis.CodigoAjenoPuntoServicio.CodAjeno
                        objPtoServicio.desCodigoAjenoPtoServicio = tablaGenesis.CodigoAjenoPuntoServicio.DesAjeno
                        objPtoServicio.codSectorMAE = tablaGenesis.CodigoSectorMAE
                        objPtoServicio.completo = tablaGenesis.Completo


                        grid.DataSource = puntosServicioGrid
                        grid.DataBind()

                        If String.IsNullOrEmpty(txtDeviceIDForm.Text.Trim) Then
                            txtDeviceIDForm.Text = objPtoServicio.codSectorMAE
                        End If

                    End If
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init, grid.Init
        If listaUCAtualiza Is Nothing Then
            listaUCAtualiza = New List(Of String)
        End If
        listaUCAtualiza.Clear()
        DefinirParametrosBase()
        If Modo = "modal" Then

            Page.Header.Controls.Add(
New LiteralControl(
"<style type='text/css'>
#pnlConteudo{
top: 0px !important;
   padding-top: 0px !important;
height: 525px !important;
        }

.layout-center{
    min-width: 1px !important;
    }
</style>
    "
))
        End If



        btnCancelarPlanificacionMAE.Visible = False
        grid.DataSource = puntosServicioGrid
        grid.DataBind()
        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
        TraduzirControles()
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

                    If PeticionesDatoBancario Is Nothing Then PeticionesDatoBancario = New Dictionary(Of String, Genesis.ContractoServicio.Contractos.Integracion.ConfigurarDatosBancarios.Peticion)
                    If PeticionesDatoBancario.ContainsKey(tablaGenesis.OidPuntoServicio) Then
                        PeticionesDatoBancario(tablaGenesis.OidPuntoServicio) = tablaGenesis.Peticion
                    Else
                        PeticionesDatoBancario.Add(tablaGenesis.OidPuntoServicio, tablaGenesis.Peticion)
                    End If

                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub



    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            HandlerUCBanco = True

            Dim strErro As String = MontaMensagensErroForm(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnValidarDeviceID.ClientID & Chr(34) & ");"

            MyBase.ExibirMensagemSimNao(String.Format(MyBase.RecuperarValorDic("MSG_INFO_CONFIRMA_GRABACION"), MyBase.RecuperarValorDic("mod_cod_ajeno_titulo")), jsScript)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        Finally

        End Try
    End Sub

    Private Sub btnValidarDeviceID_Click(sender As Object, e As EventArgs) Handles btnValidarDeviceID.Click
        Try
            If String.IsNullOrEmpty(txtDeviceIDForm.Text) Then
                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeGrabar.ClientID & Chr(34) & ");"
                MyBase.ExibirMensagemSimNao(String.Format(MyBase.RecuperarValorDic("MSG_INFO_DEVICEID_NO_INFORMADO"), puntosServicioGrid.First.codSectorMAE), jsScript)
            Else
                ExecutarGrabar()
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeGrabar_Click(sender As Object, e As EventArgs) Handles btnConsomeGrabar.Click
        Try
            ExecutarGrabar()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCancelar_Click(sender As Object, e As EventArgs) Handles btnConsomeCancelar.Click
        Try
            ExecutarCancelar()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnBajaConfirm_Click(sender As Object, e As EventArgs) Handles btnBajaConfirm.Click
        Try
            If Maquina IsNot Nothing Then

                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnConsomeBaja.ClientID & Chr(34) & ");"

                MyBase.ExibirMensagemSimNao(String.Format(RecuperarValorDic("MSG_INFO_BAJAR_MAE"), Maquina.DeviceID), jsScript)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeBaja_Click(sender As Object, e As EventArgs) Handles btnConsomeBaja.Click
        Try
            If Maquina IsNot Nothing Then

                Dim objAccionMAE As New LogicaNegocio.AccionMAE
                Dim oidPlanificacion As String = String.Empty
                If Maquina.Planificacion IsNot Nothing Then
                    oidPlanificacion = Maquina.Planificacion.Identificador
                End If

                Dim objRespuesta = objAccionMAE.BajaMAE(Maquina.OidMaquina, oidPlanificacion, MyBase.LoginUsuario)

                If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    ExecutarCancelar()
                    btnBuscar_Click(Nothing, Nothing)
                Else
                    MyBase.MostraMensagem(objRespuesta.MensajeError)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeNovo_Click(sender As Object, e As EventArgs) Handles btnConsomeNovo.Click
        Try
            btnNovo.Enabled = False
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            ddlDelegacionForm.Focus()
            chkVigenteForm.Enabled = False
            chkVigenteForm.Checked = True
            chkConsideraRecuentos.Enabled = True
            chkConsideraRecuentos.Checked = True
            chkMultiClientes.Checked = False

            ddlTipoPlanificacion.SelectedValue = ""
            tipoPlanificacionSelectedValue = ddlTipoPlanificacion.SelectedValue

            Acao = Utilidad.eAcao.Alta
            LimpiarForm()

            imgCodigoAjenoMono.ImageUrl = "~/Imagenes/contain_disabled.png"
            imgCodigoAjenoMono.Enabled = False
            divDesplazado.Visible = False
            ClientesPtoServMono.Clear()
            'AtualizaDadosHelperCliente(ClientesPtoServMono, ucClientesPtoServMono)
            AddLista("ucClientesPtoServMono")


            Dim bancosSelecionados As New ObservableCollection(Of Comon.Clases.Cliente)
            AtualizaBancoSelecionado(bancosSelecionados)

            HabilitarDesabilitaForm(True)
            pnUcBancoform.Enabled = False
            btnTransacciones.Enabled = False

            HabilitarDesabilitaPlanificacion()

            CargarCodigoAjenoMae()
            pnUcClientesPtoServMono.Visible = Not puntosServicioGrid.Any
            btnAddPtoServ.Visible = Not puntosServicioGrid.Any

            'Limite
            ucLimiteMae.Modo = Comon.Enumeradores.Modo.Alta
            ucLimiteMae.ConfigurarControles()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeAddPtoServ_Click(sender As Object, e As EventArgs) Handles btnConsomeAddPtoServ.Click
        Try
            Dim objCliente = New Cliente
            If chkMultiClientes.Checked Then
                objCliente = ucClientesPtoServ.Clientes.First
            Else
                objCliente = ucClientesPtoServMono.Clientes.First
            End If


            MaquinaPuntoServicio = Nothing
            AddPuntoServicio(objCliente)

            Dim objClientePto As New Prosegur.Genesis.Comon.Clases.Cliente

            If chkMultiClientes.Checked Then
                ClientesPtoServ.Clear()
                ClientesPtoServ.Add(objClientePto)
                AtualizaDadosHelperCliente(ClientesPtoServ, ucClientesPtoServ)
            Else
                ClientesPtoServMono.Clear()
                ClientesPtoServMono.Add(objClientePto)
                AtualizaDadosHelperCliente(ClientesPtoServMono, ucClientesPtoServMono)
            End If

            pnUcClientesPtoServMono.Visible = Not puntosServicioGrid.Any
            btnAddPtoServ.Visible = (Not puntosServicioGrid.Any) Or chkMultiClientes.Checked
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeFechaValor_Click(sender As Object, e As EventArgs) Handles btnConsomeFechaValor.Click
        Try
            Planificacion = Nothing
            btnAddPlanificacion.Enabled = False
            txtPlanificacion.Text = String.Empty
            chkVigentePlan.Checked = False
            chkControlaFacturacion.Checked = False
            dvFechaInicio.Style.Add("display", "block")
            dvFechaFin.Style.Add("display", "block")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub chkMultiClientes_CheckedChanged(sender As Object, e As EventArgs) Handles chkMultiClientes.CheckedChanged

        If chkMultiClientes.Checked Then

            If ucClientesPtoServMono.Clientes IsNot Nothing AndAlso
             ucClientesPtoServMono.Clientes.FirstOrDefault() IsNot Nothing AndAlso
             ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso
             ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault() IsNot Nothing AndAlso
             ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso
             ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then


                Dim objPuntoServicio = ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.First
                MaquinaPuntoServicio = LogicaNegocio.AccionMaquina.GetMaquinaPuntoServicio(objPuntoServicio.Identificador)

                If MaquinaPuntoServicio IsNot Nothing AndAlso pnUcClientesPtoServMono.Enabled Then
                    If Not ValidaSaldoPtoServicio(ucClientesPtoServMono.Clientes.FirstOrDefault().Identificador,
                                                        ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Identificador,
                                                        ucClientesPtoServMono.Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Identificador,
                                                        MaquinaPuntoServicio.Sector.Identificador,
                                                        MaquinaPuntoServicio.Delegacion.Plantas.First.Identificador, False) Then

                        chkMultiClientes.Checked = False
                        MyBase.MostraMensagemErro(MyBase.RecuperarValorDic("MSG_INFO_CUENTA_SALDO_DESPLAZADO"), Nothing)
                        Return
                    End If
                End If

                Dim objCliente = ucClientesPtoServMono.Clientes.First

                MaquinaPuntoServicio = Nothing
                AddPuntoServicio(objCliente)

            Else
                divPorComisionCliente.Visible = False
            End If
            divMonocliente.Visible = False
            divMulticliente.Visible = True


            grid.DataSource = puntosServicioGrid
            grid.DataBind()
            btnAddPtoServ.Visible = True

        Else

            If puntosServicioGrid.Count > 1 Then

                If puntosServicioGrid.Where(Function(x) x.desplazado).Count > 1 Then

                    chkMultiClientes.Checked = True
                    MyBase.MostraMensagemErro(MyBase.RecuperarValorDic("ErrorDesplazadoMonocliente"), Nothing)
                Else

                    Dim acaonao As String = "ExecutarClick(" & Chr(34) & btnAlertaNao.ClientID & Chr(34) & ");"
                    Dim acaosim As String = "ExecutarClick(" & Chr(34) & btnAlertaSim.ClientID & Chr(34) & ");"

                    MyBase.ExibirMensagemNaoSim(MyBase.RecuperarValorDic("2020010001"), acaosim, acaonao)
                End If
            Else
                pnUcClientesPtoServMono.Visible = Not puntosServicioGrid.Any
                btnAddPtoServ.Visible = Not puntosServicioGrid.Any
                divMonocliente.Visible = True
                divMulticliente.Visible = False
                PuntosServicio()
                divPorComisionCliente.Visible = True
            End If
        End If
    End Sub

    Protected Sub btnAlertaNao_Click(sender As Object, e As EventArgs) Handles btnAlertaNao.Click
        Try
            chkMultiClientes.Checked = True
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Protected Sub btnAlertaSim_Click(sender As Object, e As EventArgs) Handles btnAlertaSim.Click
        Try

            divMonocliente.Visible = True
            divMulticliente.Visible = False
            PuntosServicio()
            divPorComisionCliente.Visible = True
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub PuntosServicio()




        ClientesPtoServMono.Clear()
        If puntosServicioGrid.Count > 0 Then

            Dim pto = puntosServicioGrid.FirstOrDefault()

            If puntosServicioGrid.Count > 1 Then
                puntosServicioGrid.RemoveAll(Function(x) Not x.desplazado)

                If puntosServicioGrid.Count <> 0 Then
                    pto = puntosServicioGrid.FirstOrDefault()

                End If

            End If


            Dim cli = New Cliente With {.Identificador = pto.oidCliente,
                                        .Codigo = pto.codCliente,
                                        .Descripcion = pto.desCliente,
                                        .SubClientes = New ObservableCollection(Of SubCliente)
                                        }

            Dim subCli = New SubCliente With {.Identificador = pto.oidSubcliente,
                                             .Codigo = pto.codSubcliente,
                                             .Descripcion = pto.desSubcliente,
                                             .PuntosServicio = New ObservableCollection(Of PuntoServicio)
                                            }

            Dim ptoCli = New PuntoServicio With {.Identificador = pto.oidPtoServicio,
                                                .Codigo = pto.codPtoServicio,
                                                .Descripcion = pto.desPtoServicio
                                               }
            subCli.PuntosServicio.Add(ptoCli)
            cli.SubClientes.Add(subCli)



            ClientesPtoServMono.Add(cli)





            If Acao = Utilidad.eAcao.Modificacion Then
                If pto.desplazado Then
                    divDesplazado.Visible = True
                    pnUcClientesPtoServMono.Enabled = False
                Else
                    divDesplazado.Visible = False
                    pnUcClientesPtoServMono.Enabled = True
                End If
            End If
        End If

        'AtualizaDadosHelperCliente(ClientesPtoServMono, ucClientesPtoServMono)
        AddLista("ucClientesPtoServMono")

        pnUcClientesPtoServMono.Visible = Not puntosServicioGrid.Any
        btnAddPtoServ.Visible = Not puntosServicioGrid.Any

        grid.DataSource = puntosServicioGrid
        grid.DataBind()
    End Sub

    Private Sub BusquedaMAE_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete


        AtualizaDadosHelperCliente(Clientes, ucClientes)
        For Each obj In listaUCAtualiza

            If obj = "ucClientesPtoServMono" Then
                AtualizaDadosHelperCliente(ClientesPtoServMono, ucClientesPtoServMono)
            End If
            If obj = "ucClientesTesoreria" Then
                AtualizaDadosHelperCliente(ClientesTesoreria, ucClientesTesoreria)

            End If

        Next
        AtualizaDadosHelperCanal(Canales, ucCanales)
        AtualizaDadosHelperPunto(PuntoServicios, ucPuntoServicio)
    End Sub

    Protected Sub btnAgregarPlanificacionMAE_Click(sender As Object, e As EventArgs) Handles btnAgregarPlanificacionMAE.Click
        Try

            If (Canales IsNot Nothing AndAlso Canales.Count > 0) Then
                If gridPlanes.Enabled Then
                    AgregarPlanificacionMAE()
                Else
                    EditarPlanificacionMAE()
                End If
            Else
                MyBase.MostraMensagemErro(MyBase.RecuperarValorDic("MSG_ERROR_SIN_CANAL_ASOCIADO"), Nothing)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub AgregarPlanificacionMAE()
        Dim objPlanificacionMaquinaPorCanalSubCanalPunto As PlanMaqPorCanalSubCanalPunto = New PlanMaqPorCanalSubCanalPunto()

        objPlanificacionMaquinaPorCanalSubCanalPunto.Canales = Canales.Clonar
        objPlanificacionMaquinaPorCanalSubCanalPunto.PuntosServicios = PuntoServicios.Clonar

        Dim lista As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)

        lista = PlanesMAExCanalesSubCanalesPuntos
        lista.Add(objPlanificacionMaquinaPorCanalSubCanalPunto)
        PlanesMAExCanalesSubCanalesPuntos = lista


        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
        LimpiarPlanificacionMAE()
    End Sub

    Private Sub EditarPlanificacionMAE()
        Dim lista As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
        lista = New ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
        Dim objPlan As PlanMaqPorCanalSubCanalPunto

        For Each plan As PlanMaqPorCanalSubCanalPunto In PlanesMAExCanalesSubCanalesPuntos
            If plan.Indice.Equals(ItemEditadoEnLaGrillaDePlanificacionMAE) Then
                objPlan = New PlanMaqPorCanalSubCanalPunto
                objPlan.Canales = Canales.Clonar
                objPlan.PuntosServicios = PuntoServicios.Clonar

                lista.Add(objPlan)
            Else
                lista.Add(plan)
            End If

        Next

        PlanesMAExCanalesSubCanalesPuntos = lista
        btnAgregarPlanificacionMAE.Text = Tradutor.Traduzir("btnAnadir")
        btnCancelarPlanificacionMAE.Visible = False
        gridPlanes.Enabled = True
        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
        LimpiarPlanificacionMAE()
    End Sub

    Private Sub LimpiarPlanificacionMAE()
        Canales.Clear()
        PuntoServicios.Clear()

        updUcPuntoServicioUc.Update()
        upCanales.Update()
        gridPlanes.FocusedRowIndex = -1 ' la grilla queda sin fila seleccionada
        gridPlanes.Selection.UnselectAll()
    End Sub

    Private Property ItemEditadoEnLaGrillaDePlanificacionMAE() As String
        Get
            Return Session("elementoEditadoEnLaGrilla")
        End Get
        Set(ByVal value As String)
            Session("elementoEditadoEnLaGrilla") = value
        End Set
    End Property




#End Region

    Private Sub AtualizaDadosHelperPunto(observableCollection As ObservableCollection(Of Comon.Clases.PuntoServicio), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucPuntoServicio)
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

        pUserControl.ucPuntoServicio.RegistrosSelecionados = dadosCliente
        pUserControl.ucPuntoServicio.ExibirDados(True)
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

    Protected Sub btnCancelarPlanificacionMAE_Click(sender As Object, e As EventArgs) Handles btnCancelarPlanificacionMAE.Click
        btnCancelarPlanificacionMAE.Visible = False
        gridPlanes.Enabled = True
        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
        LimpiarPlanificacionMAE()
    End Sub

    Private Sub btnAjeno_Click(sender As Object, e As EventArgs) Handles btnAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtDeviceIDForm.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoForm.Text
            tablaGenesis.OidTablaGenesis = OidMaquina

            If Maquina IsNot Nothing AndAlso Maquina.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Maquina.CodigosAjenos

                'Busca codigo ajeno MAE para actualizar los datos
                Dim codigoMae = tablaGenesis.CodigosAjenos.FirstOrDefault(Function(x) x.CodIdentificador = "MAE")
                If codigoMae IsNot Nothing Then
                    codigoMae.CodAjeno = txtDeviceIDForm.Text
                    codigoMae.DesAjeno = txtDescricaoForm.Text
                End If
            End If


            Session("objSAPR_TMAQUINA") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=SAPR_TMAQUINA"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=SAPR_TMAQUINA"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjenoMae.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeCodigoAjenoMae_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjenoMae.Click
        Try
            If Session("objRespuestaSAPR_TMAQUINA") IsNot Nothing Then

                If Maquina Is Nothing Then
                    Maquina = New ContractoServicio.Maquina.GetMaquinaDetalle.Maquina
                End If
                Maquina.CodigosAjenos = Session("objRespuestaSAPR_TMAQUINA")
                Session.Remove("objRespuestaSAPR_TMAQUINA")


                Dim iCodigoAjeno = (From item In Maquina.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If Maquina.CodigosAjenos IsNot Nothing Then
                    CodigosAjenosPeticion = Maquina.CodigosAjenos
                Else
                    CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub CargarCodigoAjenoMae()
        If Maquina Is Nothing Then
            Maquina = New ContractoServicio.Maquina.GetMaquinaDetalle.Maquina
            Maquina.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        End If

        If Maquina.CodigosAjenos Is Nothing OrElse Maquina.CodigosAjenos.Count = 0 Then
            Maquina.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            Dim codigoAjenoMae = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With
            {
             .CodIdentificador = "MAE",
             .CodAjeno = txtDeviceIDForm.Text,
             .DesAjeno = txtDescricaoForm.Text,
             .BolDefecto = True,
             .BolActivo = True,
             .BolMigrado = False
            }
            Maquina.CodigosAjenos.Add(codigoAjenoMae)
        End If

    End Sub

    Protected Function BolVisibleDatoBancario(oidPuntoServicio As String) As Boolean
        Try



            If Not String.IsNullOrEmpty(oidPuntoServicio) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(oidPuntoServicio) Then
                    id = oidPuntoServicio
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)


                If objPtoServicio IsNot Nothing Then
                    Dim datos_bancarios_busqueda = ucDatosBanc.RecuperarDatosBancarios(objPtoServicio.oidCliente, objPtoServicio.oidSubcliente, objPtoServicio.oidPtoServicio)
                    Return datos_bancarios_busqueda.DatosBancarios.Any(Function(x) x.Pendiente)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

        Return False
    End Function

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
            Case "DATOSBANCARIOS"
                DatosBancarios_Grid(params(1))
            Case "EDITAR_PLAN"
                EditarPLAN_Grid(params(1))
            Case "QUITAR_PLAN"
                QuitarPLAN_Grid(params(1))
            Case "AJENO"
                AJENO_Grid(params(1))

        End Select
    End Sub

    Protected Sub DatosBancarios_Grid(identificador As String)
        Try
            If Not String.IsNullOrEmpty(identificador) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(identificador) Then
                    id = identificador
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCamposExtra.CamposExtras

                tablaGenesis.OidCliente = objPtoServicio.oidCliente
                tablaGenesis.CodCliente = objPtoServicio.codCliente
                tablaGenesis.DesCliente = objPtoServicio.desCliente
                tablaGenesis.OidSubcliente = objPtoServicio.oidSubcliente
                tablaGenesis.CodSubcliente = objPtoServicio.codSubcliente
                tablaGenesis.DesSubcliente = objPtoServicio.desSubcliente
                tablaGenesis.OidPuntoServicio = objPtoServicio.oidPtoServicio
                tablaGenesis.CodPuntoServicio = objPtoServicio.codPtoServicio
                tablaGenesis.DesPuntoServicio = objPtoServicio.desPtoServicio
                If DatosBancarios Is Nothing Then
                    DatosBancarios = New Dictionary(Of String, List(Of Comon.Clases.DatoBancario))
                End If
                If DatosBancarios.ContainsKey(id) Then
                    tablaGenesis.DatosBancarios = DatosBancarios(id)
                End If

                Session("objMantenimientoCamposExtras") = tablaGenesis

                If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                    url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                Else
                    url = "MantenimientoCamposExtra.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_campos_extras_titulo"), 400, 800, False, True, btnCamposExtras.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub Borrar_Grid(identificador As String)
        Try
            If Not String.IsNullOrEmpty(identificador) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(identificador) Then
                    id = identificador
                Else
                    id = hiddenCodigo.Value.ToString()
                End If
                If Not String.IsNullOrEmpty(id) Then
                    DelPuntoServicio(id)
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub EditarPLAN_Grid(identificador As String)
        Dim item = identificador

        Dim planAEditar As PlanMaqPorCanalSubCanalPunto = Nothing
        For Each plan As PlanMaqPorCanalSubCanalPunto In PlanesMAExCanalesSubCanalesPuntos
            If plan.Indice.Equals(item) Then
                planAEditar = plan
            End If
        Next

        If planAEditar IsNot Nothing Then
            PuntoServicios = planAEditar.PuntosServicios.Clonar
            Canales = planAEditar.Canales.Clonar
            gridPlanes.Enabled = False
            btnAgregarPlanificacionMAE.Text = Tradutor.Traduzir("btnGrabar")
            btnAgregarPlanificacionMAE.ToolTip = Tradutor.Traduzir("btnGrabar")
            ItemEditadoEnLaGrillaDePlanificacionMAE = planAEditar.Indice
            btnCancelarPlanificacionMAE.Visible = True
            '    PlanesMAExCanalesSubCanalesPuntos.Remove(planAEditar)
        End If

        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
    End Sub


    Protected Sub QuitarPLAN_Grid(identificador As String)
        Dim item = identificador
        Dim planABorrar As PlanMaqPorCanalSubCanalPunto = Nothing
        For Each plan As PlanMaqPorCanalSubCanalPunto In PlanesMAExCanalesSubCanalesPuntos
            If plan.Indice.Equals(item) Then
                planABorrar = plan
            End If
        Next

        If planABorrar IsNot Nothing Then
            PlanesMAExCanalesSubCanalesPuntos.Remove(planABorrar)
        End If

        gridPlanes.DataSource = PlanesMAExCanalesSubCanalesPuntos
        gridPlanes.DataBind()
    End Sub


    Protected Sub AJENO_Grid(identificador As String)
        Try
            If Not String.IsNullOrEmpty(identificador) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim id As String = String.Empty
                If Not String.IsNullOrEmpty(identificador) Then
                    id = identificador
                Else
                    id = hiddenCodigo.Value.ToString()
                End If

                Dim objPtoServicio As PuntoServicioGrid = puntosServicioGrid.Find(Function(a) a.oidPtoServicio = id)

                Dim url As String = String.Empty
                Dim tablaGenesis As New MantenimientoCodigosAjenosMAE.CodigoAjenoSimples


                Dim objRespDel As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
                Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion
                objRespDel = objAccionDelegacion.GetDelegacionByIdentificador(ddlDelegacionForm.SelectedValue.ToString, "MAE")

                Dim codDelegacionMaquina = ""
                If objRespDel IsNot Nothing And objRespDel.Delegacion IsNot Nothing And objRespDel.Delegacion(0) IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno) Then
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacionAjeno
                    Else
                        codDelegacionMaquina = objRespDel.Delegacion.FirstOrDefault.CodDelegacion
                    End If
                End If

                tablaGenesis.CodDelegacion = codDelegacionMaquina
                tablaGenesis.OidCliente = objPtoServicio.oidCliente
                tablaGenesis.CodCliente = objPtoServicio.codCliente
                tablaGenesis.DesCliente = objPtoServicio.desCliente
                tablaGenesis.OidSubcliente = objPtoServicio.oidSubcliente
                tablaGenesis.CodSubcliente = objPtoServicio.codSubcliente
                tablaGenesis.DesSubcliente = objPtoServicio.desSubcliente
                tablaGenesis.OidPuntoServicio = objPtoServicio.oidPtoServicio
                tablaGenesis.CodPuntoServicio = objPtoServicio.codPtoServicio
                tablaGenesis.DesPuntoServicio = objPtoServicio.desPtoServicio
                tablaGenesis.CodigoAjenoCliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoCliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoCliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoCliente}
                tablaGenesis.CodigoAjenoSubcliente = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoSubcliente,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoSubcliente,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoSubcliente}
                tablaGenesis.CodigoAjenoPuntoServicio = New ContractoServicio.CodigoAjeno.CodigoAjenoBase With {
                                                            .CodIdentificador = Comon.Enumeradores.CodigoAjeno.MAE.RecuperarValor(),
                                                            .CodAjeno = objPtoServicio.codCodigoAjenoPtoServicio,
                                                            .DesAjeno = objPtoServicio.desCodigoAjenoPtoServicio,
                                                            .OidCodigoAjeno = objPtoServicio.oidCodigoAjenoPtoServicio}
                tablaGenesis.OidSectorMAE = identificadorSectorMae
                tablaGenesis.CodigoSectorMAE = codigoSectorMae


                Session("objMantenimientoCodigosAjenosMAE") = tablaGenesis

                If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
                    url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
                Else
                    url = "MantenimientoCodigosAjenosMAE.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta
                End If

                Master.ExibirModal(url, MyBase.RecuperarValorDic("mod_cod_ajeno_titulo"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

End Class