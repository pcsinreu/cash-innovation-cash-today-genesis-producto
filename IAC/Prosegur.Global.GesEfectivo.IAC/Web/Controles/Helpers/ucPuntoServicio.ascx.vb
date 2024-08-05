Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucPuntoServicio
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property PuntoServicios As ObservableCollection(Of PuntoServicio)
        Get
            If Session(ID & "_PuntoServicios") Is Nothing Then
                Session(ID & "_PuntoServicios") = New ObservableCollection(Of PuntoServicio)
            End If
            Return Session(ID & "_PuntoServicios")
        End Get
        Set(value As ObservableCollection(Of PuntoServicio))
            Session(ID & "_PuntoServicios") = value
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

#Region "[ucPuntoServicio]"

    Private WithEvents _ucPuntoServicio As ucHelperAvanzadoBusquedaDatos
    Public Property ucPuntoServicio() As ucHelperAvanzadoBusquedaDatos
        Get
            If _ucPuntoServicio Is Nothing Then
                _ucPuntoServicio = LoadControl("~\Controles\Helpers\ucHelperAvanzadoBusquedaDatos.ascx")
                _ucPuntoServicio.ID = "ucPuntoServicio"
                AddHandler _ucPuntoServicio.Erro, AddressOf ErroControles
                If phPuntoServicio.Controls.Count = 0 Then
                    phPuntoServicio.Controls.Add(_ucPuntoServicio)
                End If
            End If
            Return _ucPuntoServicio
        End Get
        Set(value As ucHelperAvanzadoBusquedaDatos)
            _ucPuntoServicio = value
        End Set
    End Property

    Public Property OidPuntosFiltro As List(Of String)
        Get
            If Session(ID & "_OidPuntosFiltro") Is Nothing Then
                Session(ID & "_OidPuntosFiltro") = New List(Of String)
            End If
            Return Session(ID & "_OidPuntosFiltro")
        End Get
        Set(value As List(Of String))
            Session(ID & "_OidPuntosFiltro") = value
        End Set

    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PuntoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PuntoServicioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PuntoServicioFiltro Is Nothing Then
                ' Seta valor defector
                _PuntoServicioFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.PuntoServicio},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_VIGENTE", "1")}
                    }
                }

                Dim objValorCampoChave As String = ""
                If OidPuntosFiltro IsNot Nothing Then

                    For Each d As String In OidPuntosFiltro
                        If Not String.IsNullOrEmpty(d) Then
                            objValorCampoChave &= ",'" & d & "'"
                        End If
                    Next
                    If Not String.IsNullOrEmpty(objValorCampoChave) Then

                        _PuntoServicioFiltro(New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.PuntoServicio}).Add(
                        New ArgumentosFiltro("OID_PTO_SERVICIO", "in (" + objValorCampoChave.Substring(1) + ")", Prosegur.Genesis.Comon.Helper.Enumeradores.EnumHelper.TipoCondicion.Avancado))
                    End If
                End If
            End If
            Return _PuntoServicioFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PuntoServicioFiltro = value
        End Set
    End Property
    Private _PuntoServicioBuscaAvanzada As Dictionary(Of String, String)
    Public Property PuntoServicioBuscaAvanzada As Dictionary(Of String, String)
        Get
            If _PuntoServicioBuscaAvanzada Is Nothing Then
                ' Seta valor defector
                _PuntoServicioBuscaAvanzada = New Dictionary(Of String, String)

                _PuntoServicioBuscaAvanzada.Add("{0}", " AND PTO.COD_PTO_SERVICIO &CODIGO ")
                _PuntoServicioBuscaAvanzada.Add("{1}", " AND COAJ.COD_AJENO &CODIGO ")

            End If
            Return _PuntoServicioBuscaAvanzada
        End Get
        Set(value As Dictionary(Of String, String))
            _PuntoServicioBuscaAvanzada = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de PuntoServicio.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _PuntoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PuntoServicioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _PuntoServicioJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _PuntoServicioJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PuntoServicioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property PuntoServicioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _PuntoServicioOrden Is Nothing Then
                ' Seta valor defector
                _PuntoServicioOrden = New Dictionary(Of String, OrderSQL) From {{"COD_PTO_SERVICIO", New OrderSQL("COD_PTO_SERVICIO")}}
            End If
            Return _PuntoServicioOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _PuntoServicioOrden = value
        End Set
    End Property

    Public Property PuntoServicioHabilitado As Boolean = False
    Public Property PuntoServicioQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirPuntoServicio As Boolean? = Nothing

    Private _PuntoServicioVisible As Boolean = True
    Private Property PuntoServicioVisible As Boolean
        Get
            If NoExhibirPuntoServicio IsNot Nothing Then
                Return Not NoExhibirPuntoServicio
            Else
                Return _PuntoServicioVisible
            End If
        End Get
        Set(value As Boolean)
            _PuntoServicioVisible = value
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
        ucPuntoServicio.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"


    Public Sub ucCliente_OnControleAtualizado() Handles _ucPuntoServicio.UpdatedControl
        Try
            If ucPuntoServicio.RegistrosSelecionados IsNot Nothing AndAlso ucPuntoServicio.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPuntoServicio.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If PuntoServicios Is Nothing OrElse PuntoServicios.Count = 0 Then

                    For Each objDatosRespuesta In ucPuntoServicio.RegistrosSelecionados.DatosRespuesta

                        PuntoServicios.Add(New Clases.PuntoServicio With {.Identificador = objDatosRespuesta.Identificador,
                                                      .Codigo = objDatosRespuesta.Codigo,
                                                      .Descripcion = objDatosRespuesta.Descricao
                                                      })

                    Next

                ElseIf PuntoServicios IsNot Nothing Then

                    For Each objCliente As Clases.PuntoServicio In PuntoServicios.Clonar()
                        Dim objClienteLocal = objCliente
                        Dim aux = ucPuntoServicio.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objClienteLocal.Identificador)
                        If aux Is Nothing Then
                            PuntoServicios.RemoveAll(Function(c) c.Identificador = objClienteLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucPuntoServicio.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = PuntoServicios.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            PuntoServicios.Add(New Clases.PuntoServicio With {.Identificador = objDatosRespuesta.Identificador,
                                                                .Codigo = objDatosRespuesta.Codigo,
                                                                .Descripcion = objDatosRespuesta.Descricao
                                                                })
                        End If
                    Next

                End If




            Else

                ucPuntoServicio.LimparViewState()
                PuntoServicioFiltro = Nothing
                PuntoServicioJuncao = Nothing
                PuntoServicioOrden = Nothing
                PuntoServicioQueryDefecto = Nothing
                PuntoServicios = New ObservableCollection(Of PuntoServicio)
                Me.PuntoServicioHabilitado = True
                ucPuntoServicio = Nothing
                ucPuntoServicio.ExibirDados(False)

                ucPuntoServicio.FocusControle()
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
        If PuntoServicioVisible Then
            ucPuntoServicio.Titulo = Traduzir("024_PuntoServicio_Titulo")
            ucPuntoServicio.Popup_Titulo = Traduzir("024_PuntoServicio_Popup_Titulo")
            ucPuntoServicio.Popup_Resultado = Traduzir("024_PuntoServicio_Popup_Resultado")
            ucPuntoServicio.Popup_Filtro = Traduzir("024_PuntoServicio_Popup_Filtro")
        End If


    End Sub

    ''' <summary>
    ''' Carrega PuntoServicio e SubPuntoServicio utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If PuntoServicioVisible Then
            ConfigurarControle_PuntoServicio()
        End If


    End Sub

    Protected Sub ConfigurarControle_PuntoServicio()

        Me.ucPuntoServicio.FiltroConsulta = Me.PuntoServicioFiltro
        Me.ucPuntoServicio.FiltroAvanzado = Me.PuntoServicioBuscaAvanzada
        Me.ucPuntoServicio.OrdenacaoConsulta = Me.PuntoServicioOrden
        Me.ucPuntoServicio.JoinConsulta = Me.PuntoServicioJuncao
        Me.ucPuntoServicio.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucPuntoServicio.Tabela = New Tabela With {.Tabela = TabelaHelper.PuntoServicio}
        Me.ucPuntoServicio.MultiSelecao = Me.SelecaoMultipla
        Me.ucPuntoServicio.ControleHabilitado = Me.PuntoServicioHabilitado
        Me.ucPuntoServicio.QueryDefault = Me.PuntoServicioQueryDefecto

        'Me.ucPuntoServicio.FiltroConsulta.Add(New )
        If PuntoServicios IsNot Nothing AndAlso PuntoServicios.Count > 0 Then

            Dim dadosPuntoServicio As New Comon.RespuestaHelper
            dadosPuntoServicio.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each c As Clases.PuntoServicio In PuntoServicios
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosPuntoServicio.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucPuntoServicio.RegistrosSelecionados = dadosPuntoServicio
            'Me.PuntoServicioHabilitado = (Modo <> Enumeradores.Modo.Consulta)
            ucPuntoServicio.ExibirDados(False)

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