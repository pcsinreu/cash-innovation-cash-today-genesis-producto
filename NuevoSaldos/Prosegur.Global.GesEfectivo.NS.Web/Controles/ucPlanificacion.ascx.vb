Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Public Class ucPlanificacion
    Inherits UcBase



#Region "[PROPRIEDADES]"
    Public Property Delegaciones As List(Of String)
        Get
            If Session(ID & "_Delegaciones") Is Nothing Then
                Session(ID & "_Delegaciones") = New List(Of String)
            End If
            Return Session(ID & "_Delegaciones")
        End Get
        Set(value As List(Of String))
            Session(ID & "_Delegaciones") = value
        End Set
    End Property

    Public Property Planificaciones As ObservableCollection(Of Planificacion)
        Get
            If ViewState(ID & "_Planificaciones") Is Nothing Then
                ViewState(ID & "_Planificaciones") = New ObservableCollection(Of Planificacion)
            End If
            Return ViewState(ID & "_Planificaciones")
        End Get
        Set(value As ObservableCollection(Of Planificacion))
            ViewState(ID & "_Planificaciones") = value
        End Set
    End Property

    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    ''' <summary>
    ''' Define o número máximo de registros a serem retornados no Grid por paginação.
    ''' * Obs: El valor defecto es 0 (cero). Enviando 0 (cero) para ucHelperBusquedaAvanzada, el componente exibe 10 registros por pagina
    ''' </summary>
    Private _MaxRegistroPorPagina As Integer = 10
    Public Property MaxRegistroPorPagina As Integer
        Get
            Return _MaxRegistroPorPagina
        End Get
        Set(value As Integer)
            _MaxRegistroPorPagina = value
        End Set
    End Property

    Public Property SelecaoMultipla As Boolean = False

    ' Eventos.
    Public Event UpdatedControl(sender As Object)

    Private joinSQL As UtilHelper.JoinSQL

    Public Property TotalizadorSaldo As Boolean = False

    Public Property EsBanco As Boolean = False
    Public Property EsBancoCapital As Boolean = False

#Region "[ucCliente]"

    Private WithEvents _ucPlanificaciones As ucHelperBusquedaDatos
    Public Property ucPlanificaciones() As ucHelperBusquedaDatos
        Get
            If _ucPlanificaciones Is Nothing Then
                _ucPlanificaciones = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucPlanificaciones.ID = "ucPlanificaciones"
                _ucPlanificaciones.HeightPopUp = 700
                AddHandler _ucPlanificaciones.Erro, AddressOf ErroControles
                If phCliente.Controls.Count = 0 Then
                    phCliente.Controls.Add(_ucPlanificaciones)
                End If
            End If
            Return _ucPlanificaciones
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucPlanificaciones = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property ClienteFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _ClienteFiltro Is Nothing Then
                ' Seta valor defector
                _ClienteFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Planificacion},
                        New List(Of ArgumentosFiltro)
                    }
                }




            End If
            Return _ClienteFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _ClienteFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property ClienteJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _ClienteJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)

            Return _ClienteJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _ClienteJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property ClienteOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _ClienteOrden Is Nothing Then
                ' Seta valor defector
                _ClienteOrden = New Dictionary(Of String, OrderSQL) From {{"COD_planificacion", New OrderSQL("COD_planificacion")}}
            End If
            Return _ClienteOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _ClienteOrden = value
        End Set
    End Property

    Public Property ClienteHabilitado As Boolean = False
    Public Property ClienteQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirCliente As Boolean? = Nothing

    Private _ClienteVisible As Boolean = True
    Private Property ClienteVisible As Boolean
        Get
            If NoExhibirCliente IsNot Nothing Then
                Return Not NoExhibirCliente
            Else
                Return _ClienteVisible
            End If
        End Get
        Set(value As Boolean)
            _ClienteVisible = value
        End Set
    End Property


#End Region


#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            Me.ConfigurarControles()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configura foco nos UserControls 
    ''' </summary>
    Public Overrides Sub Focus()
        MyBase.Focus()
        ucPlanificaciones.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucPlanificaciones_OnControleAtualizado() Handles _ucPlanificaciones.UpdatedControl
        Try
            If ucPlanificaciones.RegistrosSelecionados IsNot Nothing AndAlso ucPlanificaciones.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPlanificaciones.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Planificaciones Is Nothing OrElse Planificaciones.Count = 0 Then

                    For Each objDatosRespuesta In ucPlanificaciones.RegistrosSelecionados.DatosRespuesta

                        Planificaciones.Add(New Clases.Planificacion With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao,
                                                      .Maquinas = Nothing})

                    Next

                ElseIf Planificaciones IsNot Nothing Then

                    For Each objCliente As Clases.Planificacion In Planificaciones.Clonar()
                        Dim objClienteLocal = objCliente
                        Dim aux = ucPlanificaciones.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objClienteLocal.Identificador)
                        If aux Is Nothing Then
                            Planificaciones.RemoveAll(Function(c) c.Identificador = objClienteLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucPlanificaciones.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Planificaciones.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Planificaciones.Add(New Clases.Planificacion With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao,
                                                                .Maquinas = Nothing})
                        End If
                    Next

                End If




            Else

                ucPlanificaciones.LimparViewState()
                ClienteFiltro = Nothing
                ClienteJuncao = Nothing
                ClienteOrden = Nothing
                ClienteQueryDefecto = Nothing
                Planificaciones = New ObservableCollection(Of Planificacion)
                Me.ClienteHabilitado = True
                ucPlanificaciones = Nothing
                ucPlanificaciones.ExibirDados(False)


                ucPlanificaciones.FocusControle()
            End If

            RaiseEvent UpdatedControl(Me)

            TraduzirControle()
            Inicializar()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configura Controles do Controle. Ignorando o Overrides.
    ''' </summary>
    Protected Sub TraduzirControle()
        If ClienteVisible Then

            ucPlanificaciones.Titulo = Traduzir("021_Planificacion_Titulo")
            ucPlanificaciones.Popup_Titulo = Traduzir("021_Planificacion_Popup_Titulo")
            ucPlanificaciones.Popup_Resultado = Traduzir("021_Planificacion_Popup_Resultado")
            ucPlanificaciones.Popup_Filtro = Traduzir("021_Planificacion_Popup_Filtro")

        End If


    End Sub

    ''' <summary>
    ''' Carrega Cliente e Maquina utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If ClienteVisible Then
            ConfigurarControle_Cliente()
        End If


    End Sub

    Protected Sub ConfigurarControle_Cliente()

        Me.ucPlanificaciones.FiltroConsulta = Me.ClienteFiltro
        Me.ucPlanificaciones.OrdenacaoConsulta = Me.ClienteOrden
        Me.ucPlanificaciones.JoinConsulta = Me.ClienteJuncao
        Me.ucPlanificaciones.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucPlanificaciones.Tabela = New Tabela With {.Tabela = TabelaHelper.Planificacion}
        Me.ucPlanificaciones.MultiSelecao = Me.SelecaoMultipla
        Me.ucPlanificaciones.ControleHabilitado = Me.ClienteHabilitado
        Me.ucPlanificaciones.QueryDefault = Me.ClienteQueryDefecto
        Me.ucPlanificaciones.HeightPopUp = 600
        ucPlanificaciones.ucPopup.Height = 560



        If Planificaciones IsNot Nothing AndAlso Planificaciones.Count > 0 Then


            Dim dadosCliente As New Comon.RespuestaHelper
            dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.Planificacion In Planificaciones
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
            ucPlanificaciones.RegistrosSelecionados = dadosCliente
            ucPlanificaciones.ExibirDados(False)

        End If

    End Sub


    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

#End Region


End Class