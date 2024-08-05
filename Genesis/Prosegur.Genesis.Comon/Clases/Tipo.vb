Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Tipo.vb
    '  Descripción: Clase definición Tipo
    ' ***********************************************************************
    <Serializable()>
    Public Class Tipo
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EstaActivo As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _TiposRelacionados As ObservableCollection(Of Tipo)
        Private _TipoFiliacion As Enumeradores.TipoFiliacion

#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property EstaActivo As String
            Get
                Return _EstaActivo
            End Get
            Set(value As String)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property TiposRelacionados As ObservableCollection(Of Tipo)
            Get
                Return _TiposRelacionados
            End Get
            Set(value As ObservableCollection(Of Tipo))
                SetProperty(_TiposRelacionados, value, "TiposRelacionados")
            End Set
        End Property

        Public Property TipoFiliacion As Enumeradores.TipoFiliacion
            Get
                Return _TipoFiliacion
            End Get
            Set(value As Enumeradores.TipoFiliacion)
                SetProperty(_TipoFiliacion, value, "TipoFiliacion")
            End Set
        End Property

#End Region

    End Class

End Namespace
