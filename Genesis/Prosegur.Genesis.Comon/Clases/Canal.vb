Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Canal.vb
    '  Descripción: Clase definición Canal
    ' ***********************************************************************
    <Serializable()>
    Public Class Canal
        Inherits BindableBase
        Implements IEntidadeHelper

#Region "Variaveis"
        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _EstaActivo As String
        Private _CodigoUsuario As String
        Private _FechaHoraActualizacion As DateTime
        Private _CodigoMigracion As String
        Private _SubCanales As ObservableCollection(Of SubCanal)

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

        Public Property Observacion As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                SetProperty(_Observacion, value, "Observacion")
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

        Public Property CodigoUsuario As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                SetProperty(_CodigoUsuario, value, "CodigoUsuario")
            End Set
        End Property

        Public Property FechaHoraActualizacion As DateTime
            Get
                Return _FechaHoraActualizacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraActualizacion, value, "FechaHoraActualizacion")
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

        Public Property SubCanales As ObservableCollection(Of SubCanal)
            Get
                Return _SubCanales
            End Get
            Set(value As ObservableCollection(Of SubCanal))
                SetProperty(_SubCanales, value, "SubCanales")
            End Set
        End Property

#End Region

    End Class

End Namespace
