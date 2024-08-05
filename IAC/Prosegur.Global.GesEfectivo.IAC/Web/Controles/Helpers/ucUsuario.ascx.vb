Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Public Class ucUsuario
    Inherits UcBase


#Region "[PROPIEDADES]"

    Public Property Usuarios As ObservableCollection(Of Usuario)
        Get
            If Session(ID & "_Usuarios") Is Nothing Then
                Session(ID & "_Usuarios") = New ObservableCollection(Of Usuario)
            End If
            Return Session(ID & "_Usuarios")
        End Get
        Set(value As ObservableCollection(Of Usuario))
            Session(ID & "_Usuarios") = value
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

#Region "[ucUsuario]"

    Private WithEvents _ucUsuario As ucHelperBusquedaDatos
    Public Property ucUsuario() As ucHelperBusquedaDatos
        Get
            If _ucUsuario Is Nothing Then
                _ucUsuario = LoadControl(ResolveUrl("~\Controles\Helpers\ucHelperBusquedaDatos.ascx"))
                _ucUsuario.ID = Me.ID & "_ucUsuario"
                _ucUsuario.Obrigatorio = Me.UsuarioObrigatorio
                AddHandler _ucUsuario.Erro, AddressOf ErroControles
                If phUsuario.Controls.Count = 0 Then
                    phUsuario.Controls.Add(_ucUsuario)
                End If
            End If
            Return _ucUsuario
        End Get
        Set(value As ucHelperBusquedaDatos)
            _ucUsuario = value
        End Set
    End Property


    ''' <summary>
    ''' Define o Filtro a ser incluído na pesquisa de Canais.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _UsuarioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
    Public Property UsuarioFiltro As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro))
        Get
            If _UsuarioFiltro Is Nothing Then
                ' Seta valor defector
                _UsuarioFiltro = New Dictionary(Of UtilHelper.Tabela, List(Of ArgumentosFiltro)) From
                {
                    {
                        New UtilHelper.Tabela With {.Tabela = Prosegur.Genesis.Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Usuario},
                        New List(Of ArgumentosFiltro) From {New ArgumentosFiltro("BOL_ACTIVO", "1")}
                    }
                }
            End If

            Return _UsuarioFiltro
        End Get
        Set(value As Dictionary(Of UtilHelper.Tabela, List(Of UtilHelper.ArgumentosFiltro)))
            _UsuarioFiltro = value
        End Set
    End Property

    ''' <summary>
    ''' Define as junções a serem incluídas para a pesquisa de Cliente.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>    
    Private _UsuarioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
    Public Property UsuarioJuncao As Dictionary(Of String, UtilHelper.JoinSQL)
        Get
            _UsuarioJuncao = New Dictionary(Of String, UtilHelper.JoinSQL)
            Dim strJoinTs As New StringBuilder

            If JoinTabla = TabelaHelper.DatoBancarioCambio Then
                strJoinTs.AppendLine(" JOIN SAPR_TDATO_BANCARIO_CAMBIO CAM ")
                strJoinTs.AppendLine(" ON CAM.OID_USUARIO = U.OID_USUARIO AND CAM.BOL_ACTIVO = '1' ")

                joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = strJoinTs.ToString()}
                _UsuarioJuncao.Add(("Join_Cambio"), joinSQL)
            ElseIf JoinTabla = TabelaHelper.DatoBancarioAprobacion Then
                strJoinTs.AppendLine(" JOIN SAPR_TDATO_BANCARIO_APROBACION AP ")
                strJoinTs.AppendLine(" ON AP.USUARIO_APROBACION = U.OID_USUARIO")

                joinSQL = New UtilHelper.JoinSQL With {.JoinPersonalizado = strJoinTs.ToString()}
                _UsuarioJuncao.Add(("Join_Aprobacion"), joinSQL)
            End If
            Return _UsuarioJuncao
        End Get
        Set(value As Dictionary(Of String, UtilHelper.JoinSQL))
            _UsuarioJuncao = value
        End Set
    End Property

    ''' <summary>
    ''' Define como os dados retornados da consulta de Canais deverão ser ordenados.
    ''' * Obs: O Preenchimento desta propriedade é opcional.
    ''' </summary>
    Private _UsuarioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
    Public Property UsuarioOrden As Dictionary(Of String, UtilHelper.OrderSQL)
        Get
            If _UsuarioOrden Is Nothing Then
                ' Seta valor defector
                _UsuarioOrden = New Dictionary(Of String, OrderSQL) From {{"DES_LOGIN", New OrderSQL("DES_LOGIN")}}
            End If
            Return _UsuarioOrden
        End Get
        Set(value As Dictionary(Of String, UtilHelper.OrderSQL))
            _UsuarioOrden = value
        End Set
    End Property

    Public Property UsuarioHabilitado As Boolean = False
    Public Property UsuarioObrigatorio As Boolean = False

    Public Property UsuarioTitulo As String

    Public Property UsuarioQueryDefecto As UtilHelper.QueryHelperControl = Nothing
    Public Property NoExhibirUsuario As Boolean? = Nothing

    Private _UsuarioVisible As Boolean = True
    Private Property UsuarioVisible As Boolean
        Get
            If NoExhibirUsuario IsNot Nothing Then
                Return Not NoExhibirUsuario
            Else
                Return _UsuarioVisible
            End If
        End Get
        Set(value As Boolean)
            _UsuarioVisible = value
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

            If UsuarioVisible Then
                ConfigurarControle_Usuario()
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
        ucUsuario.Focus()
    End Sub

#End Region

#Region "[EVENTOS]"

    Public Sub ucUsuario_OnControleAtualizado() Handles _ucUsuario.UpdatedControl
        Try
            If ucUsuario.RegistrosSelecionados IsNot Nothing AndAlso ucUsuario.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso ucUsuario.RegistrosSelecionados.DatosRespuesta.Count > 0 Then

                If Usuarios Is Nothing OrElse Usuarios.Count = 0 Then

                    For Each objDatosRespuesta In ucUsuario.RegistrosSelecionados.DatosRespuesta

                        Usuarios.Add(New Clases.Usuario With {
                                     .Identificador = objDatosRespuesta.Identificador,
                                     .Login = objDatosRespuesta.Codigo,
                                     .Nombre = objDatosRespuesta.Descricao})
                    Next

                ElseIf Usuarios IsNot Nothing Then

                    For Each objUsuario As Clases.Usuario In Usuarios.Clonar()
                        Dim objUsuarioLocal = objUsuario
                        Dim aux = ucUsuario.RegistrosSelecionados.DatosRespuesta.Find(Function(x) x.Identificador = objUsuarioLocal.Identificador)
                        If aux Is Nothing Then
                            Usuarios.RemoveAll(Function(c) c.Identificador = objUsuarioLocal.Identificador)
                        End If
                    Next

                    For Each objDatosRespuesta In ucUsuario.RegistrosSelecionados.DatosRespuesta
                        Dim objDatosRespuestaLocal = objDatosRespuesta
                        Dim aux = Usuarios.Find(Function(x) x.Identificador = objDatosRespuestaLocal.Identificador)
                        If aux Is Nothing Then
                            Usuarios.Add(New Clases.Usuario With {
                                         .Identificador = objDatosRespuesta.Identificador,
                                         .Login = objDatosRespuesta.Codigo,
                                         .Nombre = objDatosRespuesta.Descricao})
                        End If
                    Next

                End If
            Else

                ucUsuario.LimparViewState()
                UsuarioFiltro = Nothing
                UsuarioJuncao = Nothing
                UsuarioOrden = Nothing
                UsuarioQueryDefecto = Nothing
                Usuarios = New ObservableCollection(Of Usuario)
                Me.UsuarioHabilitado = True
                ucUsuario = Nothing
                ucUsuario.ExibirDados(False)
                ucUsuario.FocusControle()
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

        If UsuarioVisible Then
            ucUsuario.Titulo = Me.UsuarioTitulo
            ucUsuario.Popup_Titulo = Traduzir("045_Usuario_Popup_Titulo")
            ucUsuario.Popup_Resultado = Traduzir("045_Usuario_Popup_Resultado")
            ucUsuario.Popup_Filtro = Traduzir("045_Usuario_Popup_Filtro")
        End If

    End Sub

    Protected Sub ConfigurarControle_Usuario()
        Me.ucUsuario.FiltroConsulta = Me.UsuarioFiltro
        Me.ucUsuario.OrdenacaoConsulta = Me.UsuarioOrden
        Me.ucUsuario.JoinConsulta = Me.UsuarioJuncao
        Me.ucUsuario.MaxRegistroPorPagina = MaxRegistroPorPagina
        Me.ucUsuario.Tabela = New Tabela With {.Tabela = TabelaHelper.Usuario}
        Me.ucUsuario.MultiSelecao = Me.SelecaoMultipla
        Me.ucUsuario.ControleHabilitado = Me.UsuarioHabilitado
        Me.ucUsuario.QueryDefault = Me.UsuarioQueryDefecto


        If Usuarios IsNot Nothing AndAlso Usuarios.Count > 0 Then
            Dim dadosUsuario As New Comon.RespuestaHelper
            dadosUsuario.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

            For Each u As Clases.Usuario In Usuarios
                If Not String.IsNullOrEmpty(u.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = Nothing
                        .Identificador = u.Identificador
                        .Codigo = u.Login
                        .Descricao = u.Nombre
                    End With
                    dadosUsuario.DatosRespuesta.Add(DadosExibir)
                End If
            Next
            ucUsuario.RegistrosSelecionados = dadosUsuario
            ucUsuario.ExibirDados(False)
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