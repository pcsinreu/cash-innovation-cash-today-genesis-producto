Imports System.Xml.Serialization
Imports System.Xml

Namespace ContadoPuesto.ListarContadoPuesto

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 13/08/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:ContadoPuesto")> _
    <XmlRoot(Namespace:="urn:ContadoPuesto")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoDelegacion As String
        Private _CodPuesto As String
        Private _CodOperario As String
        Private _HoraInicio As String
        Private _HoraFin As String
        Private _NumRemesa As String
        Private _NumPrecinto As String
        Private _CodigoCliente As String
        Private _CodSubcliente As String
        Private _TipoFecha As Integer
        Private _FechaDesde As Date
        Private _FechaHasta As Date
        Private _BolIncidencia As Boolean
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

        Public Property CodPuesto() As String
            Get
                Return _CodPuesto
            End Get
            Set(value As String)
                _CodPuesto = value
            End Set
        End Property

        Public Property CodOperario() As String
            Get
                Return _CodOperario
            End Get
            Set(value As String)
                _CodOperario = value
            End Set
        End Property

        Public Property HoraInicio() As String
            Get
                Return _HoraInicio
            End Get
            Set(value As String)
                _HoraInicio = value
            End Set
        End Property

        Public Property HoraFin() As String
            Get
                Return _HoraFin
            End Get
            Set(value As String)
                _HoraFin = value
            End Set
        End Property

        Public Property NumRemesa() As String
            Get
                Return _NumRemesa
            End Get
            Set(value As String)
                _NumRemesa = value
            End Set
        End Property

        Public Property NumPrecinto() As String
            Get
                Return _NumPrecinto
            End Get
            Set(value As String)
                _NumPrecinto = value
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

        Public Property CodSubcliente() As String
            Get
                Return _CodSubcliente
            End Get
            Set(value As String)
                _CodSubcliente = value
            End Set
        End Property

        Public Property TipoFecha() As Integer
            Get
                Return _TipoFecha
            End Get
            Set(value As Integer)
                _TipoFecha = value
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

        Public Property BolIncidencia() As Boolean
            Get
                Return _BolIncidencia
            End Get
            Set(value As Boolean)
                _BolIncidencia = value
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
