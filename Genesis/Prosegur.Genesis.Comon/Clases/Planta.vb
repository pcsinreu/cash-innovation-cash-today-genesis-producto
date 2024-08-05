Imports System.Collections.ObjectModel

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Planta.vb
    '  Descripción: Clase definición Planta
    ' ***********************************************************************
    <Serializable()>
    Public Class Planta
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoMigracion As String
        Private _EsActivo As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _TiposSector As ObservableCollection(Of TipoSector)
        Private _Sectores As ObservableCollection(Of Sector)

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

        Public Property CodigoMigracion As String
            Get
                Return _CodigoMigracion
            End Get
            Set(value As String)
                SetProperty(_CodigoMigracion, value, "CodigoMigracion")
            End Set
        End Property

        Public Property EsActivo As String
            Get
                Return _EsActivo
            End Get
            Set(value As String)
                SetProperty(_EsActivo, value, "EsActivo")
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

        Public Property TiposSector As ObservableCollection(Of TipoSector)
            Get
                Return _TiposSector
            End Get
            Set(value As ObservableCollection(Of TipoSector))
                SetProperty(_TiposSector, value, "TiposSector")
            End Set
        End Property

        Public Property Sectores As ObservableCollection(Of Sector)
            Get
                Return _Sectores
            End Get
            Set(value As ObservableCollection(Of Sector))
                SetProperty(_Sectores, value, "Sectores")
            End Set
        End Property

#End Region

    End Class

End Namespace

