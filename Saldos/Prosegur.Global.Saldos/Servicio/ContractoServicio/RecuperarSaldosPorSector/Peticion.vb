Imports System.Xml.Serialization
Imports System.Xml

Namespace RecuperarSaldosPorSector

    <XmlType(Namespace:="urn:RecuperarSaldosPorSector")> _
    <XmlRoot(Namespace:="urn:RecuperarSaldosPorSector")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIAVEIS]"

        Private _Sector As String
        Private _FechaSaldo As DateTime
        Private _SoloSaldoDisponible As Boolean = False
        Private _IntegrarCentrosProceso As Boolean = False
        Private _DiscriminarEspecies As Boolean = False
        Private _Monedas As New List(Of String)
        Private _Canales As New List(Of String)
        Private _Clientes As New List(Of String)

#End Region

#Region "[PROPRIEDADES]"

        Public Property Sector() As String
            Get
                Return _Sector
            End Get
            Set(value As String)
                _Sector = value
            End Set
        End Property

        Public Property FechaSaldo() As DateTime
            Get
                Return _FechaSaldo
            End Get
            Set(value As DateTime)
                _FechaSaldo = value
            End Set
        End Property

        Public Property DiscriminarEspecies() As Boolean
            Get
                Return _DiscriminarEspecies
            End Get
            Set(value As Boolean)
                _DiscriminarEspecies = value
            End Set
        End Property

        Public Property SoloSaldoDisponible() As Boolean
            Get
                Return _SoloSaldoDisponible
            End Get
            Set(value As Boolean)
                _SoloSaldoDisponible = value
            End Set
        End Property

        Public Property IntegrarCentrosProceso() As Boolean
            Get
                Return _IntegrarCentrosProceso
            End Get
            Set(value As Boolean)
                _IntegrarCentrosProceso = value
            End Set
        End Property

        Public Property Monedas() As List(Of String)
            Get
                Return _Monedas
            End Get
            Set(value As List(Of String))
                _Monedas = value
            End Set
        End Property

        Public Property Canales() As List(Of String)
            Get
                Return _Canales
            End Get
            Set(value As List(Of String))
                _Canales = value
            End Set
        End Property

        Public Property Clientes() As List(Of String)
            Get
                Return _Clientes
            End Get
            Set(value As List(Of String))
                _Clientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace