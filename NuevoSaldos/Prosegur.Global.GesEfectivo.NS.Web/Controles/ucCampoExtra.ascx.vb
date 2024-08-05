Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper

Public Class UcCampoExtra
    Inherits UcBase

#Region "[PROPRIEDADES]"
    Public Property CampoExtraTermino As Termino
        Get
            If ViewState(ID & "CampoExtraTermino") Is Nothing Then
                ViewState(ID & "CampoExtraTermino") = New Termino
            End If
            Return ViewState(ID & "CampoExtraTermino")
        End Get
        Set(value As Termino)
            ViewState(ID & "CampoExtraTermino") = value
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

    Public identificadorFormulario As String

    Public Property SelecaoMultipla As Boolean = False

    Public Event UpdatedControl(sender As Object)

    Private joinSQL As UtilHelper.JoinSQL



#Region "[ucCampoExtra]"

    Private WithEvents _ucCampoExtra As ucHelperBusquedaDatos
    Public Property ucCampoExtra() As ucHelperBusquedaDatos
        Get
            If _ucCampoExtra Is Nothing Then
                _ucCampoExtra = LoadControl("~\Controles\ucHelperBusquedaDatos.ascx")
                _ucCampoExtra.ID = "ucCampoExtra"
                AddHandler _ucCampoExtra.Erro, AddressOf ErroControles

                If phCampoExtra.Controls.Count = 0 Then
                    phCampoExtra.Controls.Add(_ucCampoExtra)
                End If
            End If
            Return _ucCampoExtra
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucCampoExtra = value
        End Set
    End Property

    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de SubClientes.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _CampoExtraFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property CampoExtraFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            Return _CampoExtraFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _CampoExtraFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de SubCliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _CampoExtraJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property CampoExtraJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            Return _CampoExtraJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _CampoExtraJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' 
    ''' </summary>
    Private _CampoExtraOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property CampoExtraOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _CampoExtraOrden Is Nothing Then
                ' Seta valor defector
                _CampoExtraOrden = New Dictionary(Of String, OrderSQL) From {{"COD_TERMINO", New OrderSQL("COD_TERMINO")}}
            End If
            Return _CampoExtraOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _CampoExtraOrden = value
        End Set
    End Property

    Public Property CampoExtraHabilitado() As Boolean = False
    Public Property CampoExtraQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirCampoExtra As Boolean? = Nothing

    Private _CampoExtraVisible As Boolean = True
    Private Property CampoExtraVisible As Boolean
        Get
            If NoExhibirCampoExtra IsNot Nothing Then
                Return Not NoExhibirCampoExtra
            Else
                Return _CampoExtraVisible
            End If
        End Get
        Set(value As Boolean)
            _CampoExtraVisible = value
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
        ucCampoExtra.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucCampoExtra_OnControleAtualizado() Handles _ucCampoExtra.UpdatedControl
        Try
            If ucCampoExtra.RegistrosSelecionados IsNot Nothing AndAlso ucCampoExtra.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucCampoExtra.RegistrosSelecionados.DatosRespuesta.Count > 0 Then
                Dim identificadorCampoExtra As String = String.Empty
                If CampoExtraTermino IsNot Nothing Then
                    'Verifica si fue seleccionado un campo extra
                    If ucCampoExtra.RegistrosSelecionados.DatosRespuesta.Count = 1 Then
                        identificadorCampoExtra = ucCampoExtra.RegistrosSelecionados.DatosRespuesta(0).Identificador
                    End If
                End If



                Dim objCampoExtra As Clases.Termino = New Clases.Termino With {.Identificador = ucCampoExtra.RegistrosSelecionados.DatosRespuesta(0).Identificador,
                                                                                         .Codigo = ucCampoExtra.RegistrosSelecionados.DatosRespuesta(0).Codigo,
                                                                                         .Descripcion = ucCampoExtra.RegistrosSelecionados.DatosRespuesta(0).Descricao}

                Me.CampoExtraTermino = objCampoExtra
            Else

                ucCampoExtra.LimparViewState()
                CampoExtraVisible = False
                CampoExtraFiltro = Nothing
                CampoExtraJuncao = Nothing
                CampoExtraOrden = Nothing
                CampoExtraQueryDefecto = Nothing
                CampoExtraTermino = Nothing
                ucCampoExtra = Nothing
                ucCampoExtra.ExibirDados(False)

                ucCampoExtra.FocusControle()
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
        If CampoExtraVisible Then
            ucCampoExtra.Titulo = Traduzir("080_CampoExtra_Titulo")
            ucCampoExtra.Popup_Titulo = Traduzir("080_CampoExtra_Popup_Titulo")
            ucCampoExtra.Popup_Resultado = Traduzir("080_CampoExtra_Popup_Resultado")
            ucCampoExtra.Popup_Filtro = Traduzir("080_CampoExtra_Popup_Filtro")
            ucCampoExtra.HeightPopUp = 550
        End If

    End Sub

    ''' <summary>
    ''' Carrega  
    ''' </summary>
    Protected Sub ConfigurarControles()

        TraduzirControle()

        If CampoExtraVisible Then
            ConfigurarControle_CampoExtra()
        End If
    End Sub

    Protected Sub ConfigurarControle_CampoExtra()

        Me.ucCampoExtra.FiltroConsulta = Me.CampoExtraFiltro
        Me.ucCampoExtra.OrdenacaoConsulta = Me.CampoExtraOrden
        Me.ucCampoExtra.JoinConsulta = Me.CampoExtraJuncao
        Me.ucCampoExtra.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucCampoExtra.Tabela = New Tabela With {.Tabela = TabelaHelper.Termino}
        Me.ucCampoExtra.MultiSelecao = Me.SelecaoMultipla
        Me.ucCampoExtra.ControleHabilitado = Me.CampoExtraHabilitado
        Me.ucCampoExtra.QueryDefault = Me.CampoExtraQueryDefecto

        Me.AtualizarRegistrosCamposExtras()
    End Sub

    Public Sub AtualizarRegistrosCamposExtras()
        If CampoExtraTermino IsNot Nothing Then
            Dim datos As New Comon.RespuestaHelper
            datos.DatosRespuesta = New List(Of Comon.Helper.Respuesta)


            Dim DadosExibir As New Comon.Helper.Respuesta
            With DadosExibir
                .IdentificadorPai = Nothing
                .Identificador = CampoExtraTermino.Identificador
                .Codigo = CampoExtraTermino.Codigo
                .Descricao = CampoExtraTermino.Descripcion
            End With

            'Para não exibir registros duplicados
            If Not datos.DatosRespuesta.Exists(Function(s) s.Identificador = DadosExibir.Identificador) Then
                datos.DatosRespuesta.Add(DadosExibir)
            End If

            Me.ucCampoExtra.RegistrosSelecionados = datos
            Me.ucCampoExtra.ExibirDados(False)
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