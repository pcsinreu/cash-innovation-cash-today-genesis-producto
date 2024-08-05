Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class Modelo
        Inherits BindableBase
        Implements IEntidadeHelper

#Region "Variaveis"
        Private _Identificador As String
        Private _IdentificadorFabricante As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _NelCapacidadBilletes As Long
        Private _NelCapacidadMonedas As Long
        Private _Fabricante As Fabricante
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

        Public Property IdentificadorFabricante As String
            Get
                Return _IdentificadorFabricante
            End Get
            Set(value As String)
                SetProperty(_IdentificadorFabricante, value, "IdentificadorFabricante")
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

        Public Property NelCapacidadBilletes As Long
            Get
                Return _NelCapacidadBilletes
            End Get
            Set(value As Long)
                SetProperty(_NelCapacidadBilletes, value, "NelCapacidadBilletes")
            End Set
        End Property

        Public Property NelCapacidadMonedas As Long
            Get
                Return _NelCapacidadMonedas
            End Get
            Set(value As Long)
                SetProperty(_NelCapacidadMonedas, value, "NelCapacidadMonedas")
            End Set
        End Property

        Public Property Fabricante As Fabricante
            Get
                Return _Fabricante
            End Get
            Set(value As Fabricante)
                SetProperty(_Fabricante, value, "Fabricante")
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
