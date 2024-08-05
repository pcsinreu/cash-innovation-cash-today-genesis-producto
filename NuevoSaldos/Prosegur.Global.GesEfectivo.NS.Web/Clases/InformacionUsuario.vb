Imports System.Xml.Serialization
Imports System.Xml
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon

''' <summary>
''' InformacionUsuario
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kasantos] 17/10/2012 Criado
''' </history>
<Serializable()> _
Public Class InformacionUsuario

#Region "Construtor"

    Public Sub New()

        _Rol = New List(Of String)
        _Permisos = New List(Of String)
        _TiposSector = New ObservableCollection(Of Clases.TipoSector)
        _SectorSeleccionado = New Clases.Sector
        _Delegaciones = New ObservableCollection(Of Clases.Delegacion)
        _DelegacionSeleccionada = New Clases.Delegacion()

    End Sub

#End Region

#Region " Variáveis "

    Private _Nombre As String
    Private _Apelido As String
    Private _Rol As List(Of String)
    Private _Permisos As List(Of String)
    Private _CodigoDelegacion As String
    Private _NombreDelegacion As String
    Private _Delegaciones As ObservableCollection(Of Clases.Delegacion)
    Private _TiposSector As ObservableCollection(Of Clases.TipoSector)
    Private _SectorSeleccionado As Clases.Sector

    Private _DelegacionSeleccionada As Clases.Delegacion


#End Region

#Region " Propriedades "

    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(value As String)
            _Nombre = value
        End Set
    End Property

    Public Property Apelido() As String
        Get
            Return _Apelido
        End Get
        Set(value As String)
            _Apelido = value
        End Set
    End Property

    Public Property Rol() As List(Of String)
        Get
            Return _Rol
        End Get
        Set(value As List(Of String))
            _Rol = value
        End Set
    End Property

    Public Property Permisos() As List(Of String)
        Get
            Return _Permisos
        End Get
        Set(value As List(Of String))
            _Permisos = value
        End Set
    End Property

    Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)
        Get
            Return _Delegaciones
        End Get
        Set(value As ObservableCollection(Of Clases.Delegacion))
            _Delegaciones = value
        End Set
    End Property

    Public Property DelegacionSeleccionada As Prosegur.Genesis.Comon.Clases.Delegacion
        Get
            Return _DelegacionSeleccionada
        End Get
        Set(value As Prosegur.Genesis.Comon.Clases.Delegacion)
            _DelegacionSeleccionada = value
        End Set
    End Property
#End Region

End Class

Public Class SessionManager
    Public Shared Property InformacionUsuario() As InformacionUsuario
        Get
            If HttpContext.Current.Session("BaseInformacoesUsuario") Is Nothing Then
                HttpContext.Current.Session("BaseInformacoesUsuario") = New InformacionUsuario()
            End If
            Return HttpContext.Current.Session("BaseInformacoesUsuario")
        End Get
        Set(value As InformacionUsuario)
            HttpContext.Current.Session("BaseInformacoesUsuario") = value
        End Set
    End Property
End Class