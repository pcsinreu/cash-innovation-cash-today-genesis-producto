Imports System.Web.UI
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucOperario
    Inherits UcBase

#Region "[PROPRIEDADES]"

    Public Property Operarios As ContractoServ.GetUsuariosDetail.UsuarioColeccion
        Get
            If ViewState(ID & "_Operarios") Is Nothing Then
                ViewState(ID & "_Operarios") = New ContractoServ.GetUsuariosDetail.UsuarioColeccion
            End If
            Return ViewState(ID & "_Operarios")
        End Get
        Set(value As ContractoServ.GetUsuariosDetail.UsuarioColeccion)
            ViewState(ID & "_Operarios") = value
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

    Private WithEvents _ucOperario As ucHelperBusquedaDatosOperario
    Public Property ucOperario() As ucHelperBusquedaDatosOperario
        Get
            If _ucOperario Is Nothing Then
                _ucOperario = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatosOperario.ascx"))
                _ucOperario.ID = "ucOperario"
                _ucOperario.Obrigatorio = Me.OperarioObrigatorio
                AddHandler _ucOperario.Erro, AddressOf ErroControles
                If phOperario.Controls.Count = 0 Then
                    phOperario.Controls.Add(_ucOperario)
                End If
            End If
            Return _ucOperario
        End Get
        Set(value As ucHelperBusquedaDatosOperario)
            _ucOperario = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _OperarioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property OperardioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _OperarioFiltro Is Nothing Then
                ' Seta valor defector
                _OperarioFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Delegacion}, _
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("COD_DELEGACION", If(Session("DelegacaoEcolhida") Is Nothing, String.Empty, Session("DelegacaoEcolhida").ToString()))}
                    }
                }
            End If
            Return _OperarioFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _OperarioFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _OperarioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property OperarioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            If _OperarioJuncao Is Nothing Then
                Dim join As New UtilHelper.JoinSQL() With {
                                          .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Puesto},
                                          .TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Delegacion},
                                          .CampoComumTabDireita = "OID_DELEGACION",
                                          .CampoComumTabEsq = "OID_DELEGACION"
                                      }
                _OperarioJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)
                _OperarioJuncao.Add("Delegacao", join)

                join = New UtilHelper.JoinSQL() With {
                                         .TabelaDireita = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Puesto},
                                         .TabelaEsquerda = New UtilHelper.Tabela With {.Tabela = Helper.Enumeradores.Tabelas.TabelaHelper.Aplicacion},
                                         .CampoComumTabDireita = "OID_APLICACION",
                                         .CampoComumTabEsq = "OID_APLICACION"
                                     }
                _OperarioJuncao.Add("Aplicacion", join)

            End If

            Return _OperarioJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _OperarioJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _OperarioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property OperardioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            'If _PuestoOrden Is Nothing Then
            '    ' Seta valor defector
            '    _PuestoOrden = New Dictionary(Of String, OrderSQL) From {{"COD_CLIENTE", New OrderSQL("COD_CLIENTE")}}
            'End If
            Return _OperarioOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _OperarioOrden = value
        End Set
    End Property

    Public Property OperarioHabilitado As Boolean = False
    Public Property OperarioObrigatorio As Boolean = False

    Public Property OperarioQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirOperario As Boolean? = Nothing

    Private _OperarioVisible As Boolean = True
    Private Property PuestoVisible As Boolean
        Get
            If NoExhibirOperario IsNot Nothing Then
                Return Not NoExhibirOperario
            Else
                Return _OperarioVisible
            End If
        End Get
        Set(value As Boolean)
            _OperarioVisible = value
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
        ucOperario.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucOperario_OnControleAtualizado() Handles _ucOperario.UpdatedControl
        Try
            If ucOperario.RegistrosSelecionados IsNot Nothing AndAlso ucOperario.RegistrosSelecionados.Count > 0 Then
                If Operarios Is Nothing OrElse Operarios.Count = 0 Then

                    For Each objDatosRespuesta In ucOperario.RegistrosSelecionados

                        Operarios.Add(New ContractoServ.GetUsuariosDetail.Usuario() With {.Nombre = objDatosRespuesta.Nombre, _
                                                      .Apellido1 = objDatosRespuesta.Apellido1, _
                                                      .Login = objDatosRespuesta.Login, _
                                                        .Activo = objDatosRespuesta.Activo, .Delegacion = objDatosRespuesta.Delegacion
                                                      })

                    Next
                    ucOperario.FocusControle()
                End If
            ElseIf Operarios IsNot Nothing Then

                ucOperario.LimparViewState()
                OperardioFiltro = Nothing
                OperarioJuncao = Nothing
                OperardioOrden = Nothing
                OperarioQueryDefecto = Nothing
                Operarios = New ContractoServ.GetUsuariosDetail.UsuarioColeccion
                Me.OperarioHabilitado = True
                ucOperario = Nothing
                ucOperario.ExibirDados(False)
                ucOperario.FocusControle()
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
            ucOperario.Titulo = Traduzir("022_lbl_subtitulosoperario")
            ucOperario.Popup_Titulo = Traduzir("022_titulo_busqueda_operario")
            ucOperario.Popup_Resultado = Traduzir("022_Resultado_busqueda_Operarios")
            ucOperario.Popup_Filtro = Traduzir("022_lbl_subtituloscriteriosbusqueda")
        End If

    End Sub

    ''' <summary>
    ''' Carrega Cliente e SubCliente utilizando os parâmetros definidos pelo usuário.
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If PuestoVisible Then
            ConfigurarControle_Operario()
        End If

    End Sub

    Protected Sub ConfigurarControle_Operario()

        Me.ucOperario.FiltroConsulta = Me.OperardioFiltro
        Me.ucOperario.OrdenacaoConsulta = Me.OperardioOrden
        Me.ucOperario.JoinConsulta = Me.OperarioJuncao
        Me.ucOperario.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucOperario.Tabela = New Tabela With {.Tabela = TabelaHelper.Puesto}
        Me.ucOperario.MultiSelecao = Me.SelecaoMultipla
        Me.ucOperario.ControleHabilitado = Me.OperarioHabilitado
        Me.ucOperario.QueryDefault = Me.OperarioQueryDefecto

        If Operarios IsNot Nothing AndAlso Operarios.Count > 0 Then


            Dim dadosOperario As New ContractoServ.GetUsuariosDetail.UsuarioColeccion

            For Each c As ContractoServ.GetUsuariosDetail.Usuario In dadosOperario
                If Not String.IsNullOrEmpty(c.Login) Then
                    Dim DadosExibir As New ContractoServ.GetUsuariosDetail.Usuario
                    With DadosExibir
                        .Login = c.Login
                        .Nombre = c.Nombre
                        .Apellido1 = c.Apellido1
                        .Delegacion = c.Delegacion
                    End With
                    dadosOperario.Add(DadosExibir)
                End If
            Next
            ucOperario.RegistrosSelecionados = dadosOperario
            ucOperario.ExibirDados()
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