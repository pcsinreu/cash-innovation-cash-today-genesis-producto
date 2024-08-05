Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class Fabricante
        Inherits BindableBase
        Implements IEntidadeHelper

#Region "Variaveis"
        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Direccion As String
        Private _BolActivo As Boolean
        Private _DesUsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _DesUsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime

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

        Public Property Codigo As String Implements IEntidadeHelper.Codigo
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property Direccion As String
            Get
                Return _Direccion
            End Get
            Set(value As String)
                SetProperty(_Direccion, value, "Direccion")
            End Set
        End Property

        Public Property BolActivo As Boolean
            Get
                Return _BolActivo
            End Get
            Set(value As Boolean)
                SetProperty(_BolActivo, value, "BolActivo")
            End Set
        End Property

        Public Property DesUsuarioCreacion As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_DesUsuarioCreacion, value, "DesUsuarioCreacion")
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

        Public Property DesUsuarioModificacion As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_DesUsuarioModificacion, value, "DesUsuarioModificacion")
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

#End Region

    End Class

End Namespace
