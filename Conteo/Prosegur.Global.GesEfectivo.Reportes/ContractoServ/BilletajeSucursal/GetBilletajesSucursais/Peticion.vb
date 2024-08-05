Imports System.Xml.Serialization
Imports System.Xml

Namespace BilletajeSucursal.GetBilletajesSucursais

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:BilletajeSucursal")> _
    <XmlRoot(Namespace:="urn:BilletajeSucursal")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoCliente As String
        Private _CodigoDelegacion As String
        Private _FechaTransporteDesde As Date = Date.MinValue
        Private _FechaTransporteHasta As Date = Date.MinValue
        Private _FechaDesde As DateTime = DateTime.MinValue
        Private _FechaHasta As DateTime = DateTime.MinValue
        Private _FechaDesdeFinConteo As DateTime = DateTime.MinValue
        Private _FechaHastaFinConteo As DateTime = DateTime.MinValue
        Private _CodigoTipoMovimiento As String
        Private _FormatoSalida As Enumeradores.eFormatoSalida
        Private _Canales As List(Of String)

#End Region

#Region " Propriedades "

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
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

        Public Property FechaDesde() As DateTime
            Get
                Return _FechaDesde
            End Get
            Set(value As DateTime)
                _FechaDesde = value
            End Set
        End Property

        Public Property FechaHasta() As DateTime
            Get
                Return _FechaHasta
            End Get
            Set(value As DateTime)
                _FechaHasta = value
            End Set
        End Property

        Public Property FechaDesteFinConteo() As DateTime
            Get
                Return _FechaDesdeFinConteo
            End Get
            Set(value As DateTime)
                _FechaDesdeFinConteo = value
            End Set
        End Property

        Public Property FechaHastaFinConteo() As DateTime
            Get
                Return _FechaHastaFinConteo
            End Get
            Set(value As DateTime)
                _FechaHastaFinConteo = value
            End Set
        End Property

        Public Property CodigoTipoMovimiento() As String
            Get
                Return _CodigoTipoMovimiento
            End Get
            Set(value As String)
                _CodigoTipoMovimiento = value
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

        Public Property Canales() As List(Of String)
            Get
                Return _Canales
            End Get
            Set(value As List(Of String))
                _Canales = value
            End Set
        End Property

#End Region

    End Class

End Namespace
