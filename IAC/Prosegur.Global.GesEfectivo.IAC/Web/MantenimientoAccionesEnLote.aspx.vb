Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio

Public Class MantenimientoAccionesEnLote
    Inherits Base


    Private Property respuesta As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        Get
            If Session(ID & "_respuesta") Is Nothing Then
                Session(ID & "_respuesta") = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
            End If
            Return Session(ID & "_respuesta")
        End Get
        Set(value As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta))
            Session(ID & "_respuesta") = value
        End Set
    End Property

    Private Property identificadoresSinRecuento As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        Get
            If Session(ID & "_identificadoresSinRecuento") Is Nothing Then
                Session(ID & "_identificadoresSinRecuento") = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
            End If
            Return Session(ID & "_identificadoresSinRecuento")
        End Get
        Set(value As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta))
            Session(ID & "_identificadoresSinRecuento") = value
        End Set
    End Property

    Private Property identificadoresConRecuento As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        Get
            If Session(ID & "_identificadoresConRecuento") Is Nothing Then
                Session(ID & "_identificadoresConRecuento") = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
            End If
            Return Session(ID & "_identificadoresConRecuento")
        End Get
        Set(value As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta))
            Session(ID & "_identificadoresConRecuento") = value
        End Set
    End Property
    Private ReadOnly Property totalConRecuento As Double
        Get
            If identificadoresConRecuento IsNot Nothing Then
                Return identificadoresConRecuento.Count
            End If
            Return 0
        End Get
    End Property
    Private ReadOnly Property totalSinRecuento As Double
        Get
            If identificadoresSinRecuento IsNot Nothing Then
                Return identificadoresSinRecuento.Count
            End If
            Return 0
        End Get
    End Property

    Private Property identificadorDelegacion As String
        Get
            If Session(ID & "_identificadorDelegacion") Is Nothing Then
                Session(ID & "_identificadorDelegacion") = String.Empty
            End If
            Return Session(ID & "_identificadorDelegacion")
        End Get
        Set(value As String)
            Session(ID & "_identificadorDelegacion") = value
        End Set
    End Property

    Private Property identificadorBanco As String
        Get
            If Session(ID & "_identificadorBanco") Is Nothing Then
                Session(ID & "_identificadorBanco") = String.Empty
            End If
            Return Session(ID & "_identificadorBanco")
        End Get
        Set(value As String)
            Session(ID & "_identificadorBanco") = value
        End Set
    End Property

    Private Property identificadorCliente As String
        Get
            If Session(ID & "_identificadorCliente") Is Nothing Then
                Session(ID & "_identificadorCliente") = String.Empty
            End If
            Return Session(ID & "_identificadorCliente")
        End Get
        Set(value As String)
            Session(ID & "_identificadorCliente") = value
        End Set
    End Property

    Private Property identificadorPlanificacion As String
        Get
            If Session(ID & "_identificadorPlanificacion") Is Nothing Then
                Session(ID & "_identificadorPlanificacion") = String.Empty
            End If
            Return Session(ID & "_identificadorPlanificacion")
        End Get
        Set(value As String)
            Session(ID & "_identificadorPlanificacion") = value
        End Set
    End Property


#Region "[HelpersCliente]"

    Public Property Clientes As ObservableCollection(Of Comon.Clases.Cliente)
        Get
            If Session(ID & "_Clientes") Is Nothing Then
                Session(ID & "_Clientes") = New ObservableCollection(Of Comon.Clases.Cliente)
            End If
            Return Session(ID & "_Clientes")
        End Get
        Set(value As ObservableCollection(Of Comon.Clases.Cliente))
            Session(ID & "_Clientes") = value
        End Set
    End Property

    Private WithEvents _ucCliente As ucHelperAvanzadoBusquedaDatos
    Public Property ucCliente() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucCliente Is Nothing Then
                _ucCliente = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperAvanzadoBusquedaDatos.ascx"))
                _ucCliente.ID = Me.ID & "_ucCliente"
                _ucCliente.Obrigatorio = False
                AddHandler _ucCliente.Erro, AddressOf ErroControles
                If phCliente.Controls.Count = 0 Then
                    phCliente.Controls.Add(_ucCliente)
                End If
            End If
            Return _ucCliente
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucCliente = value
        End Set
    End Property

    Private _ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _ClienteFiltro Is Nothing Then
                ' Seta valor defector
                _ClienteFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                        New List(Of UtilHelper.ArgumentosFiltro) From {New UtilHelper.ArgumentosFiltro("VIGENTE", "1")}
                    }
                }
            End If
            Return _ClienteFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _ClienteFiltro = value
        End Set
    End Property

    Private _ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _ClienteJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _ClienteJuncao = value
        End Set
    End Property

    Private _ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _ClienteOrden Is Nothing Then
                ' Seta valor defector
                _ClienteOrden = New Dictionary(Of String, UtilHelper.OrderSQL) From {{"COD_CLIENTE", New UtilHelper.OrderSQL("COD_CLIENTE")}}
            End If
            Return _ClienteOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _ClienteOrden = value
        End Set
    End Property

    Private Sub ConfigurarControle_Cliente()

        Me.ucCliente.FiltroConsulta = Me.ClienteFiltro
        Me.ucCliente.FiltroAvanzado = Me.ClienteBuscaAvanzada
        Me.ucCliente.OrdenacaoConsulta = Me.ClienteOrden
        Me.ucCliente.JoinConsulta = Me.ClienteJuncao
        Me.ucCliente.MaxRegistroPorPagina = 10
        Me.ucCliente.Tabela = New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}
        Me.ucCliente.MultiSelecao = False
        Me.ucCliente.ControleHabilitado = True
        Me.ucCliente.QueryDefault = Nothing

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

            Dim dadosCliente As New Comon.RespuestaHelper
            dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Cliente In Clientes
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
            ucCliente.RegistrosSelecionados = dadosCliente
            ucCliente.ExibirDados(False)

        End If

    End Sub
    Private _ClienteBuscaAvanzada As Dictionary(Of String, String)
    Public Property ClienteBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _ClienteBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _ClienteBuscaAvanzada = New Dictionary(Of String, String)

                _ClienteBuscaAvanzada.Add("{0}", " AND CLIE.COD_CLIENTE &CODIGO ")
                _ClienteBuscaAvanzada.Add("{1}", " AND COAJ.COD_AJENO &CODIGO ")

            End If
            Return _ClienteBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _ClienteBuscaAvanzada = value
        End Set
    End Property
#End Region

#Region "[HelpersPlanificacion]"

    Public Property Planificaciones As ObservableCollection(Of Comon.Clases.Planificacion)
        Get
            If Session(ID & "_Planificaciones") Is Nothing Then
                Session(ID & "_Planificaciones") = New ObservableCollection(Of Comon.Clases.Planificacion)
            End If
            Return Session(ID & "_Planificaciones")
        End Get
        Set(value As ObservableCollection(Of Comon.Clases.Planificacion))
            Session(ID & "_Planificaciones") = value
        End Set
    End Property

    Private WithEvents _ucPlanificacion As ucHelperBusquedaDatos
    Public Property ucPlanificacion() As ucHelperBusquedaDatos
        Get
            If _ucPlanificacion Is Nothing Then
                _ucPlanificacion = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx"))
                _ucPlanificacion.ID = Me.ID & "_ucPlanificacion"
                _ucPlanificacion.Obrigatorio = False
                AddHandler _ucPlanificacion.Erro, AddressOf ErroControles
                If phPlanificacion.Controls.Count = 0 Then
                    phPlanificacion.Controls.Add(_ucPlanificacion)
                End If
            End If
            Return _ucPlanificacion
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucPlanificacion = value
        End Set
    End Property

    Private _PlanificacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PlanificacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PlanificacionFiltro Is Nothing Then
                ' Seta valor defector
                _PlanificacionFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Planificacion},
                        New List(Of UtilHelper.ArgumentosFiltro) From {New UtilHelper.ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If
            Return _PlanificacionFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PlanificacionFiltro = value
        End Set
    End Property

    Private _PlanificacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PlanificacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _PlanificacionJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _PlanificacionJuncao = value
        End Set
    End Property

    Private _PlanificacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property PlanificacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _PlanificacionOrden Is Nothing Then
                _PlanificacionOrden = New Dictionary(Of String, UtilHelper.OrderSQL) From {{"COD_PLANIFICACION", New UtilHelper.OrderSQL("COD_PLANIFICACION")}}
            End If
            Return _PlanificacionOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _PlanificacionOrden = value
        End Set
    End Property

    Private Sub ConfigurarControle_Planificacion()

        Me.ucPlanificacion.FiltroConsulta = Me.PlanificacionFiltro
        Me.ucPlanificacion.OrdenacaoConsulta = Me.PlanificacionOrden
        Me.ucPlanificacion.JoinConsulta = Me.PlanificacionJuncao
        Me.ucPlanificacion.MaxRegistroPorPagina = 10
        Me.ucPlanificacion.Tabela = New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Planificacion}
        Me.ucPlanificacion.MultiSelecao = False
        Me.ucPlanificacion.ControleHabilitado = True
        Me.ucPlanificacion.QueryDefault = Nothing

        If Planificaciones IsNot Nothing AndAlso Planificaciones.Count > 0 Then

            Dim dadosPlanificacion As New Comon.RespuestaHelper
            dadosPlanificacion.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Planificacion In Planificaciones
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosPlanificacion.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucPlanificacion.RegistrosSelecionados = dadosPlanificacion
            ucPlanificacion.ExibirDados(False)

        End If

    End Sub

#End Region

#Region "[HelpersBanco]"

    Public Property Bancos As ObservableCollection(Of Comon.Clases.Cliente)
        Get
            If Session(ID & "_Bancos") Is Nothing Then
                Session(ID & "_Bancos") = New ObservableCollection(Of Comon.Clases.Cliente)
            End If
            Return Session(ID & "_Bancos")
        End Get
        Set(value As ObservableCollection(Of Comon.Clases.Cliente))
            Session(ID & "_Bancos") = value
        End Set
    End Property

    Private WithEvents _ucBanco As ucHelperAvanzadoBusquedaDatos
    Public Property ucBanco() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucBanco Is Nothing Then
                _ucBanco = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperAvanzadoBusquedaDatos.ascx"))
                _ucBanco.ID = Me.ID & "_ucBanco"
                _ucBanco.Obrigatorio = False
                AddHandler _ucBanco.Erro, AddressOf ErroControles
                If phBanco.Controls.Count = 0 Then
                    phBanco.Controls.Add(_ucBanco)
                End If
            End If
            Return _ucBanco
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucBanco = value
        End Set
    End Property

    Private _BancoFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property BancoFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _BancoFiltro Is Nothing Then
                ' Seta valor defector
                _BancoFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                        New List(Of UtilHelper.ArgumentosFiltro) From {New UtilHelper.ArgumentosFiltro("VIGENTE", "1")}
                    }
                }
            End If
            Return _BancoFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _BancoFiltro = value
        End Set
    End Property

    Private _BancoJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property BancoJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _BancoJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            Return _BancoJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _BancoJuncao = value
        End Set
    End Property

    Private _BancoOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property BancoOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _BancoOrden Is Nothing Then
                ' Seta valor defector
                _BancoOrden = New Dictionary(Of String, UtilHelper.OrderSQL) From {{"COD_CLIENTE", New UtilHelper.OrderSQL("COD_CLIENTE")}}
            End If
            Return _BancoOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _BancoOrden = value
        End Set
    End Property

    Private Sub ConfigurarControle_Banco()

        Me.ucBanco.FiltroConsulta = Me.BancoFiltro
        Me.ucBanco.FiltroAvanzado = Me.BancoBuscaAvanzada
        Me.ucBanco.OrdenacaoConsulta = Me.BancoOrden
        Me.ucBanco.JoinConsulta = Me.BancoJuncao
        Me.ucBanco.MaxRegistroPorPagina = 10
        Me.ucBanco.Tabela = New UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente}
        Me.ucBanco.MultiSelecao = False
        Me.ucBanco.ControleHabilitado = True
        Me.ucBanco.QueryDefault = Nothing

        If Bancos IsNot Nothing AndAlso Bancos.Count > 0 Then

            Dim dadosBanco As New Comon.RespuestaHelper
            dadosBanco.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Cliente In Bancos
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosBanco.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucBanco.RegistrosSelecionados = dadosBanco
            ucBanco.ExibirDados(False)

        End If

    End Sub

    Private _BancoBuscaAvanzada As Dictionary(Of String, String)
    Public Property BancoBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _BancoBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _BancoBuscaAvanzada = New Dictionary(Of String, String)

                _BancoBuscaAvanzada.Add("{0}", " AND CLIE.COD_CLIENTE &CODIGO ")
                _BancoBuscaAvanzada.Add("{1}", " AND COAJ.COD_AJENO &CODIGO ")

            End If
            Return _BancoBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _BancoBuscaAvanzada = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.ACCIONESLOTE
        MyBase.ValidarAcao = True
        MyBase.CodFuncionalidad = "ABM_ACCIONES_EN_LOTE"
    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            GoogleAnalyticsHelper.TrackAnalytics(Me, "Mantenimiento Acciones En Lotes")
            Master.MostrarCabecalho = True
            Master.HabilitarHistorico = True
            Master.HabilitarMenu = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = False
            Master.MenuGrande = True

            If Not Page.IsPostBack Then

                PreencherddlDelegacion(True)
                ddlDelegacion.Focus()
                LimpiarValores()
                PreencherddlAccion()
            End If

            ConfigurarControle_Cliente()
            ConfigurarControle_Planificacion()
            ConfigurarControle_Banco()

            '  lblValorTotalConRecuento.Text = totalConRecuento.ToString
            '  lblValorTotalSinRecuento.Text = totalSinRecuento.ToString

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

            ' Titulos
            Master.Titulo = MyBase.RecuperarValorDic("lbl_titulo_busqueda")
            lblSubTitulosCriteriosBusqueda.Text = MyBase.RecuperarValorDic("lbl_criterio_busqueda")
            lblSubTituloMAE.Text = MyBase.RecuperarValorDic("lbl_titulo_Resultados")

            lblDelegacion.Text = MyBase.RecuperarValorDic("lbl_delegacion")
            ucCliente.Titulo = MyBase.RecuperarValorDic("lbl_cliente")
            ucCliente.Popup_Titulo = MyBase.RecuperarValorDic("lbl_Popup_Titulo") & MyBase.RecuperarValorDic("lbl_cliente")
            ucCliente.Popup_Resultado = MyBase.RecuperarValorDic("lbl_Popup_Resultado") & MyBase.RecuperarValorDic("lbl_cliente")
            ucCliente.Popup_Filtro = MyBase.RecuperarValorDic("lbl_criterio_busqueda")
            ucPlanificacion.Titulo = MyBase.RecuperarValorDic("lbl_planificacion")
            ucPlanificacion.Popup_Titulo = MyBase.RecuperarValorDic("lbl_Popup_Titulo") & MyBase.RecuperarValorDic("lbl_planificacion")
            ucPlanificacion.Popup_Resultado = MyBase.RecuperarValorDic("lbl_Popup_Resultado") & MyBase.RecuperarValorDic("lbl_planificacion")
            ucPlanificacion.Popup_Filtro = MyBase.RecuperarValorDic("lbl_criterio_busqueda")
            ucBanco.Titulo = MyBase.RecuperarValorDic("lbl_banco")
            ucBanco.Popup_Titulo = MyBase.RecuperarValorDic("lbl_Popup_Titulo") & MyBase.RecuperarValorDic("lbl_banco")
            ucBanco.Popup_Resultado = MyBase.RecuperarValorDic("lbl_Popup_Resultado") & MyBase.RecuperarValorDic("lbl_banco")
            ucBanco.Popup_Filtro = MyBase.RecuperarValorDic("lbl_criterio_busqueda")



            lblTotalSinRecuento.Text = MyBase.RecuperarValorDic("lbl_totalSinRecuento")
            lblTotalConRecuento.Text = MyBase.RecuperarValorDic("lbl_totalConRecuento")
            lblSinValores.Text = MyBase.RecuperarValorDic("sin_registro")
            lblAccion.Text = MyBase.RecuperarValorDic("lbl_accion")
            ' Button
            btnBuscar.Text = MyBase.RecuperarValorDic("btnBuscar")
            btnBuscar.ToolTip = btnBuscar.Text
            btnLimpar.Text = MyBase.RecuperarValorDic("btnLimpiar")
            btnLimpar.ToolTip = btnLimpar.Text
            btnTotalSinRecuento.ToolTip = MyBase.RecuperarValorDic("btnTotalSinRecuento")
            btnAsignar.Text = MyBase.RecuperarValorDic("btnAsignar")
            btnAsignar.ToolTip = btnAsignar.Text
            btnTotalConRecuento.ToolTip = MyBase.RecuperarValorDic("btnTotalConRecuento")
            btnQuitar.Text = MyBase.RecuperarValorDic("btnQuitar")
            btnQuitar.ToolTip = btnQuitar.Text

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"



    ''' <summary>
    ''' Preencher a dropdownbox de delegaciones
    ''' </summary>
    Private Sub PreencherddlDelegacion(filtro As Boolean)

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objAccionDelegacion As New LogicaNegocio.AccionDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objAccionDelegacion.GetDelegaciones(objPeticion)


        If objRespuesta.Delegacion.Count > 0 Then
            Dim lista = objRespuesta.Delegacion.Select(Function(a) New With {.OidDelegacion = a.OidDelegacion,
                                                                             .DesDelegacion = a.DesDelegacion,
                                                                             .CodDesDelegacion = a.CodDelegacion + " - " + a.DesDelegacion}).OrderBy(Function(b) b.CodDesDelegacion)

            If filtro Then
                ddlDelegacion.AppendDataBoundItems = True
                ddlDelegacion.Items.Clear()
                ddlDelegacion.DataTextField = "CodDesDelegacion"
                ddlDelegacion.DataValueField = "OidDelegacion"
                ddlDelegacion.DataSource = lista.ToList()
                ddlDelegacion.DataBind()
            End If

            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            Dim delegacion = objRespuesta.Delegacion.Find(Function(d) d.CodDelegacion = usuario.CodigoDelegacion)
            If delegacion IsNot Nothing Then
                If filtro Then
                    SeleccionarDelegacionLogada(delegacion.OidDelegacion, ddlDelegacion, filtro)
                End If
            End If
        End If

    End Sub

    Private Sub PreencherddlAccion()
        Dim accion = New Dictionary(Of String, String)

        'accion.Add("1", "Proceso de Recuento")
        'accion.Add("2", "Multiclientes")


        ddlAccion.Items.Add("Proceso de Recuento")
        ddlAccion.Items.Add("Multiclientes")
    End Sub

    Private Sub SeleccionarDelegacionLogada(oidDelegacion As String, ByRef ddlDelegacionControl As DropDownList, filtro As Boolean)
        Dim delegacionLogada = Nothing
        delegacionLogada = ddlDelegacionControl.Items.FindByValue(oidDelegacion)
        If delegacionLogada IsNot Nothing Then
            ddlDelegacionControl.SelectedIndex = ddlDelegacionControl.Items.IndexOf(delegacionLogada)
        End If
    End Sub

    Private Sub SeleccionarPlantaLogada(oidPlanta As String, ByRef ddlPlanta As DropDownList)
        Dim plantaLogada = Nothing
        plantaLogada = ddlPlanta.Items.FindByValue(oidPlanta)
        If plantaLogada IsNot Nothing Then
            ddlPlanta.SelectedIndex = ddlPlanta.Items.IndexOf(plantaLogada)
        End If
    End Sub

    Private Sub LimpiarValores()

        Clientes.Clear()
        ucCliente.LimparCampos()

        Planificaciones.Clear()
        ucPlanificacion.LimparCampos()

        Bancos.Clear()
        ucBanco.LimparCampos()

        respuesta = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        identificadorDelegacion = String.Empty
        identificadorBanco = String.Empty
        identificadorCliente = String.Empty
        identificadorPlanificacion = String.Empty

        dvSubTituloMAE.Style.Item("display") = "none"
        dvResultados.Style.Item("display") = "none"
        dvSinValores.Style.Item("display") = "none"

        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

    End Sub

    Private Sub RecuperarValores()

        respuesta = New List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)

        LogicaNegocio.AccionMAE.consultaAccionesEnLote(identificadorDelegacion, identificadorBanco, identificadorCliente, identificadorPlanificacion, respuesta)
        If ddlAccion.SelectedIndex = 0 Then
            identificadoresSinRecuento = respuesta.Where(Function(p) Not p.considerarecuento).ToList()
            identificadoresConRecuento = respuesta.Where(Function(p) p.considerarecuento).ToList()

        Else
            identificadoresSinRecuento = respuesta.Where(Function(p) Not p.multicliente).ToList()
            identificadoresConRecuento = respuesta.Where(Function(p) p.multicliente).ToList()

        End If


        lblValorTotalConRecuento.Text = totalConRecuento.ToString + "/" + totalConRecuento.ToString
        lblValorTotalSinRecuento.Text = totalSinRecuento.ToString + "/" + totalSinRecuento.ToString

        If totalSinRecuento > 0 Then
            btnAsignar.Enabled = True
            btnTotalSinRecuento.Enabled = True
        Else
            btnAsignar.Enabled = False
            btnTotalSinRecuento.Enabled = False
        End If

        If totalConRecuento > 0 Then
            btnQuitar.Enabled = True
            btnTotalConRecuento.Enabled = True
        Else
            btnQuitar.Enabled = False
            btnTotalConRecuento.Enabled = False
        End If

        If totalConRecuento = 0 AndAlso totalSinRecuento = 0 Then
            dvSubTituloMAE.Style.Item("display") = "none"
            dvResultados.Style.Item("display") = "none"
            dvSinValores.Style.Item("display") = "block"
        Else
            dvSubTituloMAE.Style.Item("display") = "block"
            dvResultados.Style.Item("display") = "block"
            dvSinValores.Style.Item("display") = "none"
        End If




        Dim objDTConRecuento As DataTable
        objDTConRecuento = gvConRecuento.ConvertListToDataTable(MaeConRecuento)
        gvConRecuento.CarregaControle(objDTConRecuento)


        Dim objDTSinRecuento As DataTable
        objDTSinRecuento = gvSinRecuento.ConvertListToDataTable(MaeSinRecuento)
        gvSinRecuento.CarregaControle(objDTSinRecuento)

    End Sub


    Protected Function BuscarMaeSelecionada(grid As GridView) As List(Of String)

        Dim lst = New List(Of String)
        For Each row As GridViewRow In grid.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkRow"), CheckBox)
                If chkRow.Checked Then
                    Dim hfOidMaquina As HiddenField = TryCast(row.Cells(0).FindControl("hfOidMaquina"), HiddenField)
                    lst.Add(hfOidMaquina.Value)

                End If
            End If
        Next
        Return lst
    End Function


    Public Function MaeConRecuento() As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        Return identificadoresConRecuento
    End Function
    Public Function MaeSinRecuento() As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)
        Return identificadoresSinRecuento
    End Function

    Private Function BuscarIdentificadores(lst As List(Of ContractoServicio.MAE.MantenimientoAccionesEnLote.Respuesta)) As List(Of String)

        Dim result = lst.Select(Function(i) i.oid_maquina).ToList
        Return result

    End Function
#End Region

#Region "[EVENTOS]"

    Private Sub ddlAccion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAccion.SelectedIndexChanged

        Try

            dvSubTituloMAE.Style.Item("display") = "none"
            dvResultados.Style.Item("display") = "none"
            dvSinValores.Style.Item("display") = "none"

            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            'inser

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Public Sub chckchangedConRecuento(sender As Object, e As System.EventArgs)
        Dim chckheader As CheckBox = TryCast(gvConRecuento.HeaderRow.FindControl("chkHeader"), CheckBox)

        For Each row As GridViewRow In gvConRecuento.Rows
            Dim chckrw As CheckBox = TryCast(row.FindControl("chkRow"), CheckBox)
            chckrw.Checked = chckheader.Checked
        Next

    End Sub


    Public Sub chckchangedSinRecuento(sender As Object, e As System.EventArgs)
        Dim chckheader As CheckBox = TryCast(gvSinRecuento.HeaderRow.FindControl("chkHeader"), CheckBox)

        For Each row As GridViewRow In gvSinRecuento.Rows
            Dim chckrw As CheckBox = TryCast(row.FindControl("chkRow"), CheckBox)
            chckrw.Checked = chckheader.Checked
        Next

    End Sub


    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Prosegur.Genesis.Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub



    Public Sub ucCliente_OnControleAtualizado() Handles _ucCliente.UpdatedControl
        Try
            If ucCliente.RegistrosSelecionados IsNot Nothing AndAlso ucCliente.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucCliente.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Clientes Is Nothing OrElse Clientes.Count = 0 Then

                    For Each objDatosRespuesta In ucCliente.RegistrosSelecionados.DatosRespuesta

                        Clientes.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao,
                                                      .SubClientes = Nothing})

                    Next

                ElseIf Clientes IsNot Nothing Then

                    For Each objCliente As Clases.Cliente In Clientes.Clonar()
                        Dim objClienteLocal = objCliente
                        Dim aux = ucCliente.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objClienteLocal.Identificador)
                        If aux Is Nothing Then
                            Clientes.RemoveAll(Function(c) c.Identificador = objClienteLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucCliente.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Clientes.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Clientes.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao,
                                                                .SubClientes = Nothing})
                        End If
                    Next

                End If

            Else
                ucCliente.LimparViewState()
                ClienteFiltro = Nothing
                ClienteJuncao = Nothing
                ClienteOrden = Nothing
                Clientes = New ObservableCollection(Of Comon.Clases.Cliente)
                ucCliente = Nothing
                ucCliente.ExibirDados(False)
                ucCliente.FocusControle()
            End If

            TraduzirControles()
            Inicializar()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ucBanco_OnControleAtualizado() Handles _ucBanco.UpdatedControl
        Try
            If ucBanco.RegistrosSelecionados IsNot Nothing AndAlso ucBanco.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucBanco.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Bancos Is Nothing OrElse Bancos.Count = 0 Then

                    For Each objDatosRespuesta In ucBanco.RegistrosSelecionados.DatosRespuesta

                        Bancos.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao,
                                                      .SubClientes = Nothing})

                    Next

                ElseIf Bancos IsNot Nothing Then

                    For Each objBanco As Clases.Cliente In Bancos.Clonar()
                        Dim objBancoLocal = objBanco
                        Dim aux = ucBanco.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objBancoLocal.Identificador)
                        If aux Is Nothing Then
                            Bancos.RemoveAll(Function(c) c.Identificador = objBancoLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucBanco.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Bancos.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Bancos.Add(New Clases.Cliente With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao,
                                                                .SubClientes = Nothing})
                        End If
                    Next

                End If

            Else
                ucBanco.LimparViewState()
                BancoFiltro = Nothing
                BancoJuncao = Nothing
                BancoOrden = Nothing
                Bancos = New ObservableCollection(Of Comon.Clases.Cliente)
                ucBanco = Nothing
                ucBanco.ExibirDados(False)
                ucBanco.FocusControle()
            End If

            TraduzirControles()
            Inicializar()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Public Sub ucPlanificacion_OnControleAtualizado() Handles _ucPlanificacion.UpdatedControl
        Try
            If ucPlanificacion.RegistrosSelecionados IsNot Nothing AndAlso ucPlanificacion.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Planificaciones Is Nothing OrElse Planificaciones.Count = 0 Then

                    For Each objDatosRespuesta In ucPlanificacion.RegistrosSelecionados.DatosRespuesta

                        Planificaciones.Add(New Clases.Planificacion With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao})

                    Next

                ElseIf Planificaciones IsNot Nothing Then

                    For Each objPlanificacion As Clases.Planificacion In Planificaciones.Clonar()
                        Dim objPlanificacionLocal = objPlanificacion
                        Dim aux = ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objPlanificacionLocal.Identificador)
                        If aux Is Nothing Then
                            Planificaciones.RemoveAll(Function(c) c.Identificador = objPlanificacionLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucPlanificacion.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Planificaciones.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Planificaciones.Add(New Clases.Planificacion With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If

            Else
                ucPlanificacion.LimparViewState()
                PlanificacionFiltro = Nothing
                PlanificacionJuncao = Nothing
                PlanificacionOrden = Nothing
                Planificaciones = New ObservableCollection(Of Comon.Clases.Planificacion)
                ucPlanificacion = Nothing
                ucPlanificacion.ExibirDados(False)
                ucPlanificacion.FocusControle()
            End If

            TraduzirControles()
            Inicializar()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try
            PreencherddlDelegacion(True)
            LimpiarValores()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try

            TraduzirAccion()
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            identificadorDelegacion = ddlDelegacion.SelectedValue
            identificadorBanco = If(Bancos IsNot Nothing AndAlso Bancos.Count > 0, Bancos.FirstOrDefault.Identificador, String.Empty)
            identificadorCliente = If(Clientes IsNot Nothing AndAlso Clientes.Count > 0, Clientes.FirstOrDefault.Identificador, String.Empty)
            identificadorPlanificacion = If(Planificaciones IsNot Nothing AndAlso Planificaciones.Count > 0, Planificaciones.FirstOrDefault.Identificador, String.Empty)

            RecuperarValores()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub TraduzirAccion()
        If ddlAccion.SelectedIndex = 0 Then
            lblSinRecuento.Text = MyBase.RecuperarValorDic("lbl_sinRecuento")
            lblConRecuento.Text = MyBase.RecuperarValorDic("lbl_conRecuento")
        Else
            lblSinRecuento.Text = MyBase.RecuperarValorDic("lbl_sinMulticliente")
            lblConRecuento.Text = MyBase.RecuperarValorDic("lbl_conMulticliente")
        End If
    End Sub

    Private Sub btnAsignar_Click(sender As Object, e As System.EventArgs) Handles btnAsignar.Click

        Try
            LogicaNegocio.AccionMAE.AccionesEnLote(BuscarMaeSelecionada(gvSinRecuento), True, ddlAccion.SelectedIndex) 'BuscarIdentificadores(identificadoresSinRecuento)
            RecuperarValores()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As System.EventArgs) Handles btnQuitar.Click

        Try
            If ddlAccion.SelectedIndex = 1 Then


                Dim jsScript As String = "ExecutarClick(" & Chr(34) & btnQuitarOculto.ClientID & Chr(34) & ");"

                MyBase.ExibirMensagemSimNao(MyBase.RecuperarValorDic("2020020001"), jsScript)
            Else
                Quitar()
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub btnConsomeBaja_Click(sender As Object, e As System.EventArgs) Handles btnQuitarOculto.Click

        Try

            Quitar()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub Quitar()
        LogicaNegocio.AccionMAE.AccionesEnLote(BuscarMaeSelecionada(gvConRecuento), False, ddlAccion.SelectedIndex) 'BuscarIdentificadores(identificadoresConRecuento)
        RecuperarValores()
        BuscarMaeSelecionada(gvConRecuento)
    End Sub



    Private Sub btnTotalSinRecuento_Click(sender As Object, e As ImageClickEventArgs) Handles btnTotalSinRecuento.Click
        Try
            Dim archivo As String = LogicaNegocio.AccionMAE.consultaAccionesEnLoteExportar(BuscarIdentificadores(identificadoresSinRecuento))
            Dim nombreArchivo As String = "MAEs_SinRecuento_" & Now.ToString("yyyymmddhhMMss") & ".CSV"
            Session(nombreArchivo) = archivo
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('Downloads.aspx?archivo=" & nombreArchivo & "') ;", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnTotalConRecuento_Click(sender As Object, e As ImageClickEventArgs) Handles btnTotalConRecuento.Click
        Try
            Dim archivo As String = LogicaNegocio.AccionMAE.consultaAccionesEnLoteExportar(BuscarIdentificadores(identificadoresConRecuento))
            Dim nombreArchivo As String = "MAEs_ConRecuento_" & Now.ToString("yyyymmddhhMMss") & ".CSV"
            Session(nombreArchivo) = archivo
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_pesquisa", "window.open('Downloads.aspx?archivo=" & nombreArchivo & "') ;", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

End Class