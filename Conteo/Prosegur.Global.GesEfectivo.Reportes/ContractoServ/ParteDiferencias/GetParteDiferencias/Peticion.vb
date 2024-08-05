Imports System.Xml.Serialization
Imports System.Xml

Namespace ParteDiferencias.GetParteDiferencias

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:ParteDiferencias")> _
    <XmlRoot(Namespace:="urn:ParteDiferencias")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoDelegacion As String
        Private _PrecintoRemesa As String
        Private _PrecintoBulto As String
        Private _NumeroTransporte As String
        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CodigoPuntoServicio As String
        Private _FechaConteoDesde As Date
        Private _FechaConteoHasta As Date
        Private _FechaTransporteDesde As Date
        Private _FechaTransporteHasta As Date
        Private _Contador As String
        Private _Supervisor As String

#End Region

#Region " Propriedades "

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property
        Public Property PrecintoRemesa() As String
            Get
                Return _PrecintoRemesa
            End Get
            Set(value As String)
                _PrecintoRemesa = value
            End Set
        End Property
        Public Property PrecintoBulto() As String
            Get
                Return _PrecintoBulto
            End Get
            Set(value As String)
                _PrecintoBulto = value
            End Set
        End Property
        Public Property NumeroTransporte() As String
            Get
                Return _NumeroTransporte
            End Get
            Set(value As String)
                _NumeroTransporte = value
            End Set
        End Property
        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property
        Public Property CodigoSubCliente() As String
            Get
                Return _CodigoSubCliente
            End Get
            Set(value As String)
                _CodigoSubCliente = value
            End Set
        End Property
        Public Property CodigoPuntoServicio() As String
            Get
                Return _CodigoPuntoServicio
            End Get
            Set(value As String)
                _CodigoPuntoServicio = value
            End Set
        End Property
        Public Property FechaConteoDesde() As Date
            Get
                Return _FechaConteoDesde
            End Get
            Set(value As Date)
                _FechaConteoDesde = value
            End Set
        End Property
        Public Property FechaConteoHasta() As Date
            Get
                Return _FechaConteoHasta
            End Get
            Set(value As Date)
                _FechaConteoHasta = value
            End Set
        End Property
        Public Property FechaTransporteDesde() As Date
            Get
                Return _FechaTransporteDesde
            End Get
            Set(value As Date)
                _FechaTransporteDesde = value
            End Set
        End Property
        Public Property FechaTransporteHasta() As Date
            Get
                Return _FechaTransporteHasta
            End Get
            Set(value As Date)
                _FechaTransporteHasta = value
            End Set
        End Property
        Public Property Contador() As String
            Get
                Return _Contador
            End Get
            Set(value As String)
                _Contador = value
            End Set
        End Property
        Public Property Supervisor() As String
            Get
                Return _Supervisor
            End Get
            Set(value As String)
                _Supervisor = value
            End Set
        End Property

#End Region

    End Class

End Namespace
