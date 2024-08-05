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

    Public Property Planificaciones As ObservableCollection(Of Planificacion)
        Get
            If Session(ID & "_Planificaciones") Is Nothing Then
                Session(ID & "_Planificaciones") = New ObservableCollection(Of Planificacion)
            End If
            Return Session(ID & "_Planificaciones")
        End Get
        Set(value As ObservableCollection(Of Planificacion))
            Session(ID & "_Planificaciones") = value
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

#Region "[ucCanal]"

    Private WithEvents _ucPlanificacion As ucHelperBusquedaDatos
    Public Property ucPlanificacion() As ucHelperBusquedaDatos
        Get
            If _ucPlanificacion Is Nothing Then
                _ucPlanificacion = LoadControl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx")
                _ucPlanificacion.ID = "ucPlanificacion"
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

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PlanificacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PlanificacionFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PlanificacionFiltro Is Nothing Then
                ' Seta valor defector
                _PlanificacionFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Planificacion},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If
            Return _PlanificacionFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PlanificacionFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Planificacion.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _PlanificacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PlanificacionJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _PlanificacionJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _PlanificacionJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PlanificacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property PlanificacionOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _PlanificacionOrden Is Nothing Then
                ' Seta valor defector
                _PlanificacionOrden = New Dictionary(Of String, OrderSQL) From {{"COD_Planificacion", New OrderSQL("COD_Planificacion")}}
            End If
            Return _PlanificacionOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _PlanificacionOrden = value
        End Set
    End Property

    Public Property PlanificacionHabilitado As Boolean = False
    Public Property PlanificacionQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirPlanificacion As Boolean? = Nothing

    Private _PlanificacionVisible As Boolean = True
    Private Property PlanificacionVisible As Boolean
        Get
            If NoExhibirPlanificacion IsNot Nothing Then
                Return Not NoExhibirPlanificacion
            Else
                Return _PlanificacionVisible
            End If
        End Get
        Set(value As Boolean)
            _PlanificacionVisible = value
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
        ucPlanificacion.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucPlanificacion_OnControleAtualizado() Handles _ucPlanificacion.UpdatedControl
        Try
            If ucPlanificacion.RegistrosSelecionados IsNot Nothing AndAlso ucPlanificacion.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                'Verifica se foi selecionado apenas um Planificacion
                Dim identificadorPlanificacion As String = String.Empty

                If Planificaciones Is Nothing OrElse Planificaciones.Count = 0 Then
                    'Verifica se foi selecionado apenas um Planificacion
                    If ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorPlanificacion = ucPlanificacion.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    End If

                    For Each objDatosRespuesta In ucPlanificacion.RegistrosSelecionados.DatosRespuesta



                        Planificaciones.Add(New Clases.Planificacion With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao})

                    Next

                ElseIf Planificaciones IsNot Nothing Then

                    'Verifica se foi selecionado apenas um Planificacion
                    If ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorPlanificacion = ucPlanificacion.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    Else
                        'Verifica se foi selecionado apenas uma Planificacion, verificando os canais selecionados anteriormente e os canais atuais..
                        'se por exemplo existiam 3 canais selecionados e agora foi retornado 4, então foi selecionado apenas um Planificacion.
                        If ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Count - Planificaciones.Count = 1 Then
                            'descobrir qual o novo Planificacion que foi selecionado
                            Dim Planificacion = ucPlanificacion.RegistrosSelecionados.DatosRespuesta.Where(Function(c) Not Planificaciones.Exists(Function(old) old.Identificador = c.Identificador)).FirstOrDefault
                            If Planificacion IsNot Nothing Then
                                identificadorPlanificacion = Planificacion.Identificador
                            End If
                        End If
                    End If

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

                RaiseEvent UpdatedControl(Me)

            Else

                ucPlanificacion.LimparViewState()
                PlanificacionFiltro = Nothing
                PlanificacionJuncao = Nothing
                PlanificacionOrden = Nothing
                PlanificacionQueryDefecto = Nothing
                Planificaciones = New ObservableCollection(Of Planificacion)
                'Me.PlanificacionHabilitado = True
                ucPlanificacion = Nothing
                ucPlanificacion.ExibirDados(False)


                ucPlanificacion.FocusControle()

                RaiseEvent UpdatedControl(Me)
            End If

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
        If PlanificacionVisible Then
            ucPlanificacion.Titulo = Traduzir("024_Planificacion_Titulo")
            ucPlanificacion.Popup_Titulo = Traduzir("024_Planificacion_Popup_Titulo")
            ucPlanificacion.Popup_Resultado = Traduzir("024_Planificacion_Popup_Resultado")
            ucPlanificacion.Popup_Filtro = Traduzir("024_Planificacion_Popup_Filtro")
        End If

    End Sub

    ''' <summary>
    ''' Carrega Planificacion e SubPlanificacion utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If PlanificacionVisible Then
            ConfigurarControle_Planificacion()
        End If

    End Sub

    Protected Sub ConfigurarControle_Planificacion()

        Me.ucPlanificacion.FiltroConsulta = Me.PlanificacionFiltro
        Me.ucPlanificacion.OrdenacaoConsulta = Me.PlanificacionOrden
        Me.ucPlanificacion.JoinConsulta = Me.PlanificacionJuncao
        Me.ucPlanificacion.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucPlanificacion.Tabela = New Tabela With {.Tabela = TabelaHelper.Planificacion}
        Me.ucPlanificacion.MultiSelecao = Me.SelecaoMultipla
        Me.ucPlanificacion.ControleHabilitado = Me.PlanificacionHabilitado
        Me.ucPlanificacion.QueryDefault = Me.PlanificacionQueryDefecto

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
            'Me.PlanificacionHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            ucPlanificacion.ExibirDados(False)

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