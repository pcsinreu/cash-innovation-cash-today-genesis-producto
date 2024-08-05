Imports System.Xml.Serialization
Imports System.Xml

Namespace DetalleParciales.GetDetalleParciales

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    <XmlType(Namespace:="urn:DetalleParciales")> _
    <XmlRoot(Namespace:="urn:DetalleParciales")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoDelegacion As String
        Private _FechaDesde As Date
        Private _FechaHasta As Date
        Private _NumeroRemesa As String
        Private _NumeroPrecinto As String
        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _ConDenominacion As Integer
        Private _ConIncidencia As Integer
        Private _EsFechaProceso As Integer
        Private _FormatoSalida As Enumeradores.eFormatoSalida

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

        Public Property FechaDesde() As Date
            Get
                Return _FechaDesde
            End Get
            Set(value As Date)
                _FechaDesde = value
            End Set
        End Property

        Public Property FechaHasta() As Date
            Get
                Return _FechaHasta
            End Get
            Set(value As Date)
                _FechaHasta = value
            End Set
        End Property

        Public Property NumeroRemesa() As String
            Get
                Return _NumeroRemesa
            End Get
            Set(value As String)
                _NumeroRemesa = value
            End Set
        End Property

        Public Property NumeroPrecinto() As String
            Get
                Return _NumeroPrecinto
            End Get
            Set(value As String)
                _NumeroPrecinto = value
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

        Public Property ConDenominacion() As Integer
            Get
                Return _ConDenominacion
            End Get
            Set(value As Integer)
                _ConDenominacion = value
            End Set
        End Property

        Public Property ConIncidencia() As Integer
            Get
                Return _ConIncidencia
            End Get
            Set(value As Integer)
                _ConIncidencia = value
            End Set
        End Property

        Public Property EsFechaProceso() As Integer
            Get
                Return _EsFechaProceso
            End Get
            Set(value As Integer)
                _EsFechaProceso = value
            End Set
        End Property

        Public Property FormatoSalida() As Enumeradores.eFormatoSalida
            Get
                Return _FormatoSalida
            End Get
            Set(value As Enumeradores.eFormatoSalida)
                _FormatoSalida = value
            End Set
        End Property

#End Region

    End Class

End Namespace
