Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Public Class ucDeviceID
    Inherits UcBase


#Region "[PROPIEDADES]"

    Public Property DeviceIDs As ObservableCollection(Of Maquina)
        Get
            If Session(ID & "_DeviceIDs") Is Nothing Then
                Session(ID & "_DeviceIDs") = New ObservableCollection(Of Maquina)
            End If
            Return Session(ID & "_DeviceIDs")
        End Get
        Set(value As ObservableCollection(Of Maquina))
            Session(ID & "_DeviceIDs") = value
        End Set
    End Property

    Public Property Modo As Genesis.Comon.Enumeradores.Modo

    Private Property _JoinTabla As TabelaHelper
    Public Property JoinTabla As TabelaHelper
        Get
            Return _JoinTabla
        End Get
        Set(value As TabelaHelper)
            _JoinTabla = value
        End Set
    End Property

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

    Public Property SelecaoMultipla As Boolean = True
    ' Eventos.
    Public Event UpdatedControl(sender As Object)
    Private joinSQL As UtilHelper.JoinSQL

#End Region

#Region "[ucDeviceIDs]"

    Private WithEvents _ucDeviceID As ucHelperBusquedaDatos
    Public Property ucDeviceID() As ucHelperBusquedaDatos
        Get
            If _ucDeviceID Is Nothing Then
                _ucDeviceID = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx"))
                _ucDeviceID.ID = Me.ID & "_ucDeviceID"
                _ucDeviceID.Obrigatorio = Me.DeviceIDObligatorio
                AddHandler _ucDeviceID.Erro, AddressOf ErroControles
                If phDeviceID.Controls.Count = 0 Then
                    phDeviceID.Controls.Add(_ucDeviceID)
                End If
            End If
            Return _ucDeviceID
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucDeviceID = value
        End Set
    End Property


    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _DeviceID_Filtro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property DeviceID_Filtro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _DeviceID_Filtro Is Nothing Then
                ' Seta valor defector
                _DeviceID_Filtro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.MaquinaPunto},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If

            Return _DeviceID_Filtro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _DeviceID_Filtro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _DeviceIDJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property DeviceIDJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _DeviceIDJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)
            'Dim strJoinTs As New StringBuilder

            'If JoinTabla = TabelaHelper.DatoBancarioCambio Then
            '    strJoinTs.AppendLine(" JOIN SAPR_TDATO_BANCARIO_CAMBIO CAM ")
            '    strJoinTs.AppendLine(" ON CAM.OID_USUARIO = U.OID_USUARIO AND CAM.BOL_ACTIVO = '1' ")

            '    joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = strJoinTs.ToString()}
            '    _DeviceIDJuncao.Add(("Join_Cambio"), joinSQL)
            'ElseIf JoinTabla = TabelaHelper.DatoBancarioAprobacion Then
            '    strJoinTs.AppendLine(" JOIN SAPR_TDATO_BANCARIO_APROBACION AP ")
            '    strJoinTs.AppendLine(" ON AP.USUARIO_APROBACION = U.OID_USUARIO")

            '    joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = strJoinTs.ToString()}
            '    _DeviceIDJuncao.Add(("Join_Aprobacion"), joinSQL)
            'End If
            Return _DeviceIDJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _DeviceIDJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _DeviceID_Orden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property DeviceIDOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _DeviceID_Orden Is Nothing Then
                ' Seta valor defector
                _DeviceID_Orden = New Dictionary(Of String, OrderSQL) From {{"COD_IDENTIFICACION", New OrderSQL("COD_IDENTIFICACION")}}
            End If
            Return _DeviceID_Orden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _DeviceID_Orden = value
        End Set
    End Property

    Public Property DeviceIDHabilitado As Boolean = False
    Public Property DeviceIDObligatorio As Boolean = False

    Public Property DeviceIDTitulo As String

    Public Property DeviceIDQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirDeviceID As Boolean? = Nothing

    Private _deviceIDVisible As Boolean = True
    Private Property DeviceIDVisible As Boolean
        Get
            If NoExhibirDeviceID IsNot Nothing Then
                Return Not NoExhibirDeviceID
            Else
                Return _deviceIDVisible
            End If
        End Get
        Set(value As Boolean)
            _deviceIDVisible = value
        End Set
    End Property

#End Region


#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa controles na tela.
    ''' </summary>
    Protected Overrides Sub Inicializar()
        Try
            TraduzirControle()

            If DeviceIDVisible Then
                ConfigurarControl_DeviceID()
            End If

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Configura foco nos UserControls 
    ''' </summary>
    Public Overrides Sub Focus()
        MyBase.Focus()
        ucDeviceID.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucDeviceID_OnControleAtualizado() Handles _ucDeviceID.UpdatedControl
        Try
            If ucDeviceID.RegistrosSelecionados IsNot Nothing AndAlso ucDeviceID.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucDeviceID.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If DeviceIDs Is Nothing OrElse DeviceIDs.Count = 0 Then

                    For Each objDatosRespuesta In ucDeviceID.RegistrosSelecionados.DatosRespuesta

                        DeviceIDs.Add(New Clases.Maquina With {
                                     .Identificador = objDatosRespuesta.Identificador,
                                     .Codigo = objDatosRespuesta.Codigo,
                                     .Descripcion = objDatosRespuesta.Descricao})
                    Next

                ElseIf DeviceIDs IsNot Nothing Then

                    For Each objDeviceID As Clases.Maquina In DeviceIDs.Clonar()
                        Dim objDeviceIDLocal = objDeviceID
                        Dim aux = ucDeviceID.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objDeviceIDLocal.Identificador)
                        If aux Is Nothing Then
                            DeviceIDs.RemoveAll(Function(c) c.Identificador = objDeviceIDLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucDeviceID.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = DeviceIDs.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            DeviceIDs.Add(New Clases.Maquina With {
                                         .Identificador = objDatosRespuesta.Identificador,
                                         .Codigo = objDatosRespuesta.Codigo,
                                         .Descripcion = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If
            Else

                ucDeviceID.LimparViewState()
                DeviceID_Filtro = Nothing
                DeviceIDJuncao = Nothing
                DeviceIDOrden = Nothing
                DeviceIDQueryDefecto = Nothing
                DeviceIDs = New ObservableCollection(Of Maquina)
                Me.DeviceIDHabilitado = True
                ucDeviceID = Nothing
                ucDeviceID.ExibirDados(False)
                ucDeviceID.FocusControle()
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

        If DeviceIDVisible Then
            ucDeviceID.Titulo = Me.DeviceIDTitulo


            ucDeviceID.Popup_Titulo = Traduzir("DeviceID_Popup_Titulo")
            ucDeviceID.Popup_Resultado = Traduzir("DeviceID_Popup_Resultado")
            ucDeviceID.Popup_Filtro = Traduzir("DeviceID_Popup_Filtro")

        End If

    End Sub

    Protected Sub ConfigurarControl_DeviceID()
        Me.ucDeviceID.FiltroConsulta = Me.DeviceID_Filtro
        Me.ucDeviceID.OrdenacaoConsulta = Me.DeviceIDOrden
        Me.ucDeviceID.JoinConsulta = Me.DeviceIDJuncao
        Me.ucDeviceID.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucDeviceID.Tabela = New Tabela With {.Tabela = TabelaHelper.MaquinaPunto}
        Me.ucDeviceID.MultiSelecao = Me.SelecaoMultipla
        Me.ucDeviceID.ControleHabilitado = Me.DeviceIDHabilitado
        Me.ucDeviceID.QueryDefault = Me.DeviceIDQueryDefecto


        If DeviceIDs IsNot Nothing AndAlso DeviceIDs.Count > 0 Then
            Dim datosDeviceID As New Comon.RespuestaHelper
            datosDeviceID.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each u As Clases.Maquina In DeviceIDs
                If Not String.IsNullOrEmpty(u.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = u.Identificador
                        .Codigo = u.Codigo
                        .Descricao = u.Descripcion
                    End With
                    datosDeviceID.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucDeviceID.RegistrosSelecionados = datosDeviceID
            ucDeviceID.ExibirDados(False)
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