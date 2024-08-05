Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.getComboPuntosServiciosByClienteSubcliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:getComboPuntosServiciosByClienteSubcliente")> _
    <XmlRoot(Namespace:="urn:getComboPuntosServiciosByClienteSubcliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _codigoCliente As String
        Private _codigoSubcliente As List(Of String)
        Private _codigoPuntoServicio As List(Of String)
        Private _descripcionPuntoServicio As List(Of String)
        Private _bolATM As Boolean
        Private _TotalizadorSaldo As Boolean
        Private _vigente As Nullable(Of Boolean)

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As List(Of String)
            Get
                Return _codigoSubcliente
            End Get
            Set(value As List(Of String))
                _codigoSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As List(Of String)
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As List(Of String))
                _codigoPuntoServicio = value
            End Set
        End Property

        Public Property DescripcionPuntoServicio() As List(Of String)
            Get
                Return _descripcionPuntoServicio
            End Get
            Set(value As List(Of String))
                _descripcionPuntoServicio = value
            End Set
        End Property

        Public Property BolATM As Boolean
            Get
                Return _bolATM
            End Get
            Set(value As Boolean)
                _bolATM = value
            End Set
        End Property

        Public Property TotalizadorSaldo() As Boolean
            Get
                Return _TotalizadorSaldo
            End Get
            Set(value As Boolean)
                _TotalizadorSaldo = value
            End Set
        End Property

        Public Property vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace