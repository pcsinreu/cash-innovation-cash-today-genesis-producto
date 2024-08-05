Imports System.Xml.Serialization
Imports System.Xml

Namespace CorteParcial.GetCortesParciais

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 17/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:CorteParcial")> _
    <XmlRoot(Namespace:="urn:CorteParcial")> _
    <Serializable()> _
    Public Class Peticion

#Region " Variáveis "

        Private _CodigoCliente As String
        Private _CodigoDelegacion As String
        Private _FechaDesde As Date
        Private _FechaHasta As Date
        Private _EsFechaProceso As Integer
        Private _EsRemesaPendiente As Boolean
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

        Public Property EsFechaProceso() As Integer
            Get
                Return _EsFechaProceso
            End Get
            Set(value As Integer)
                _EsFechaProceso = value
            End Set
        End Property

        Public Property EsRemesaPendiente() As Boolean
            Get
                Return _EsRemesaPendiente
            End Get
            Set(value As Boolean)
                _EsRemesaPendiente = value
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
