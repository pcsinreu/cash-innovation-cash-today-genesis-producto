Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucPuesto
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Puestos As ObservableCollection(Of Puesto)
        Get
            If ViewState(ID & "_Puestos") Is Nothing Then
                ViewState(ID & "_Puestos") = New ObservableCollection(Of Puesto)
            End If
            Return ViewState(ID & "_Puestos")
        End Get
        Set(value As ObservableCollection(Of Puesto))
            ViewState(ID & "_Puestos") = value
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

#Region "[ucCliente]"

    Private WithEvents _ucPuesto As ucHelperBusquedaDatosPuesto
    Public Property ucPuesto() As ucHelperBusquedaDatosPuesto
        Get
            If _ucPuesto Is Nothing Then
                _ucPuesto = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatosPuesto.ascx"))
                _ucPuesto.ID = "ucPuesto"
                _ucPuesto.Obrigatorio = Me.PuestoObrigatorio
                AddHandler _ucPuesto.Erro, AddressOf ErroControles
                If phPuesto.Controls.Count = 0 Then
                    phPuesto.Controls.Add(_ucPuesto)
                End If
            End If
            Return _ucPuesto
        End Get
        Set(value As ucHelperBusquedaDatosPuesto)
            _ucPuesto = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PuestoFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property PuestoFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _PuestoFiltro Is Nothing Then
                ' Seta valor defector
                _PuestoFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro))
                _PuestoFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Delegacion}, _
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_DELEGACION", If(Session("DelegacaoEcolhida") Is Nothing, String.Empty, Session("DelegacaoEcolhida").ToString()))})
                _PuestoFiltro.Add(New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Aplicacion}, _
                       New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_APLICACION", If(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodAplicacionConteo") Is Nothing, String.Empty, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodAplicacionConteo").ToUpper()))})
            End If
            Return _PuestoFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _PuestoFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _PuestoJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property PuestoJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            If _PuestoJuncao Is Nothing Then
                Dim join As New UtilHelper.JoinSQL() With {
                                          .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Puesto},
                                          .TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Delegacion},
                                          .CampoComumTabDireita = "OID_DELEGACION",
                                          .CampoComumTabEsq = "OID_DELEGACION"
                                      }
                _PuestoJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)
                _PuestoJuncao.Add("Delegacao", join)

                join = New UtilHelper.JoinSQL() With {
                                         .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Puesto},
                                         .TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Aplicacion},
                                         .CampoComumTabDireita = "OID_APLICACION",
                                         .CampoComumTabEsq = "OID_APLICACION"
                                     }
                _PuestoJuncao.Add("Aplicacion", join)

            End If

            Return _PuestoJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _PuestoJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _PuestoOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property PuestoOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            'If _PuestoOrden Is Nothing Then
            '    ' Seta valor defector
            '    _PuestoOrden = New Dictionary(Of String, OrderSQL) From {{"COD_CLIENTE", New OrderSQL("COD_CLIENTE")}}
            'End If
            Return _PuestoOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _PuestoOrden = value
        End Set
    End Property

    Public Property PuestoHabilitado As Boolean = False
    Public Property PuestoObrigatorio As Boolean = False

    Public Property PuestoQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirPuesto As Boolean? = Nothing

    Private _ClienteVisible As Boolean = True
    Private Property PuestoVisible As Boolean
        Get
            If NoExhibirPuesto IsNot Nothing Then
                Return Not NoExhibirPuesto
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
        ucPuesto.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucPuesto_OnControleAtualizado() Handles _ucPuesto.UpdatedControl
        Try
            If ucPuesto.RegistrosSelecionados IsNot Nothing AndAlso ucPuesto.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucPuesto.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                If Puestos Is Nothing OrElse Puestos.Count = 0 Then

                    For Each objDatosRespuesta In ucPuesto.RegistrosSelecionados.DatosRespuesta

                        Puestos.Add(New Clases.Puesto() With {.Identificador = objDatosRespuesta.Identificador, _
                                                      .Codigo = objDatosRespuesta.Codigo, _
                                                      .CodigoHost = objDatosRespuesta.CodigoHost
                                                      })

                    Next
                    ucPuesto.FocusControle()
                End If
            ElseIf Puestos IsNot Nothing Then

                ucPuesto.LimparViewState()
                PuestoFiltro = Nothing
                PuestoJuncao = Nothing
                PuestoOrden = Nothing
                PuestoQueryDefecto = Nothing
                Puestos = New ObservableCollection(Of Puesto)
                Me.PuestoHabilitado = True
                ucPuesto = Nothing
                ucPuesto.ExibirDados(False)
                ucPuesto.FocusControle()
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
        If PuestoVisible Then
            ucPuesto.Titulo = Traduzir("008_lbl_puesto")
            ucPuesto.Popup_Titulo = Traduzir("021_titulo_busqueda_puestos")
            ucPuesto.Popup_Resultado = Traduzir("021_Resultado_busqueda_puestos")
            ucPuesto.Popup_Filtro = Traduzir("021_lbl_subtituloscriteriosbusqueda")
        End If
        
    End Sub

    ''' <summary>
    ''' Carrega Cliente e SubCliente utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If PuestoVisible Then
            ConfigurarControle_Puesto()
        End If
        
    End Sub

    Protected Sub ConfigurarControle_Puesto()

        Me.ucPuesto.FiltroConsulta = Me.PuestoFiltro
        Me.ucPuesto.OrdenacaoConsulta = Me.PuestoOrden
        Me.ucPuesto.JoinConsulta = Me.PuestoJuncao
        Me.ucPuesto.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucPuesto.Tabela = New Tabela With {.Tabela = TabelaHelper.Puesto}
        Me.ucPuesto.MultiSelecao = Me.SelecaoMultipla
        Me.ucPuesto.ControleHabilitado = Me.PuestoHabilitado
        Me.ucPuesto.QueryDefault = Me.PuestoQueryDefecto

        If Puestos IsNot Nothing AndAlso Puestos.Count > 0 Then


            Dim dadosCliente As New Comon.RespuestaHelperPuesto
            dadosCliente.DatosRespuesta = New List(Of Comon.Helper.RespuestaPuesto)

            For Each c As Clases.Puesto In Puestos
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.RespuestaPuesto
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .CodigoHost = c.CodigoHost
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucPuesto.RegistrosSelecionados = dadosCliente
            ucPuesto.ExibirDados(False)
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