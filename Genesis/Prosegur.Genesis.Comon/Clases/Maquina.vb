Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Interfaces.Helper

Namespace Clases

    <Serializable()>
    Public Class Maquina
        Inherits BindableBase
        Implements IEntidadeHelper

#Region "Variaveis"
        Private _Identificador As String
        Private _Codigo As String

        Private _Sector As Sector
        Private _Delegacion As Clases.Delegacion
        Private _Modelo As Modelo
        Private _TipoMaquina As TipoMaquina
        Private _PlanMaquina As List(Of PlanXmaquina)
        Private _Cliente As Cliente
        Private _SubCliente As SubCliente
        Private _PtoServicio As PuntoServicio
        Private _PuntosServicio As ObservableCollection(Of PuntoServicio)
        Private _ConsideraRecuentos As Boolean
        Private _BolActivo As Boolean
        Private _DesUsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _DesUsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime
        Private _MultiClientes As Boolean
        Private _PorcComisionMaquina As Nullable(Of Decimal)
        Private _BancoTesoreria As Comon.Clases.SubCliente
        Private _planes As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)


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

        Public Property Delegacion As Comon.Clases.Delegacion
            Get
                Return _Delegacion
            End Get
            Set(value As Comon.Clases.Delegacion)
                SetProperty(_Delegacion, value, "Delegacion")
            End Set
        End Property

        Public Property Sector As Sector
            Get
                Return _Sector
            End Get
            Set(value As Sector)
                SetProperty(_Sector, value, "Sector")
            End Set
        End Property

        Public Property TipoMaquina As TipoMaquina
            Get
                Return _TipoMaquina
            End Get
            Set(value As TipoMaquina)
                SetProperty(_TipoMaquina, value, "TipoMaquina")
            End Set
        End Property

        Public Property Descripcion As String Implements IEntidadeHelper.Descripcion
            Get
                If _Sector IsNot Nothing Then
                    Return _Sector.Descripcion
                End If
                Return Nothing
            End Get
            Set(value As String)
                If Sector Is Nothing Then
                    Sector = New Sector With {.Descripcion = value}
                Else
                    Sector.Descripcion = value
                End If
            End Set
        End Property

        Public Property Modelo As Modelo
            Get
                Return _Modelo
            End Get
            Set(value As Modelo)
                SetProperty(_Modelo, value, "Modelo")
            End Set
        End Property

        Public Property PlanMaquina As List(Of PlanXmaquina)
            Get
                Return _PlanMaquina
            End Get
            Set(value As List(Of PlanXmaquina))
                SetProperty(_PlanMaquina, value, "PlanMaquina")
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

        Public Property Cliente As Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Cliente)
                SetProperty(_Cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As SubCliente
            Get
                Return _SubCliente
            End Get
            Set(value As SubCliente)
                SetProperty(_SubCliente, value, "SubCliente")
            End Set
        End Property

        Public Property PtoServicio As PuntoServicio
            Get
                Return _PtoServicio
            End Get
            Set(value As PuntoServicio)
                SetProperty(_PtoServicio, value, "PtoServicio")
            End Set
        End Property

        Public Property PuntosServicio As ObservableCollection(Of PuntoServicio)
            Get
                Return _PuntosServicio
            End Get
            Set(value As ObservableCollection(Of PuntoServicio))
                SetProperty(_PuntosServicio, value, "PuntosServicio")
            End Set
        End Property
        Public Property ConsideraRecuentos() As Boolean
            Get
                Return _ConsideraRecuentos
            End Get
            Set(value As Boolean)
                _ConsideraRecuentos = value
            End Set
        End Property

        Public Property MultiClientes() As Boolean
            Get
                Return _MultiClientes
            End Get
            Set(value As Boolean)
                _MultiClientes = value
            End Set
        End Property


        Public Property PorcComisionMaquina() As Nullable(Of Decimal)
            Get
                Return _PorcComisionMaquina
            End Get
            Set(value As Nullable(Of Decimal))
                _PorcComisionMaquina = value
            End Set
        End Property

        Public Property BancoTesoreria() As Comon.Clases.SubCliente
            Get
                Return _BancoTesoreria
            End Get
            Set(value As Comon.Clases.SubCliente)
                _BancoTesoreria = value
            End Set
        End Property

        Public Property Planes() As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto)
            Get
                Return _planes
            End Get
            Set(ByVal value As ObservableCollection(Of PlanMaqPorCanalSubCanalPunto))
                _planes = value
            End Set
        End Property
#End Region

    End Class

End Namespace
