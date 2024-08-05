Imports System.Xml.Serialization
Imports System.Xml

Namespace GetATMByRegistrarTira

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 17/03/2011 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetATMByRegistrarTira")> _
    <XmlRoot(Namespace:="urn:GetATMByRegistrarTira")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"        

        Private _codigoDelegacion As String
        Private _codigoCajero As String
        Private _codigoCliente As String        
        Private _codigoSubcliente As String
        Private _codigoPuntoServicio As String

#End Region

#Region "[Propriedades]"

        Public Property CodigoDelegacion() As String
            Get
                Return _codigoDelegacion
            End Get
            Set(value As String)
                _codigoDelegacion = value
            End Set
        End Property

        Public Property CodigoCajero() As String
            Get
                Return _codigoCajero
            End Get
            Set(value As String)
                _codigoCajero = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _codigoCliente
            End Get
            Set(value As String)
                _codigoCliente = value
            End Set
        End Property

        Public Property CodigoSubcliente() As String
            Get
                Return _codigoSubcliente
            End Get
            Set(value As String)
                _codigoSubcliente = value
            End Set
        End Property

        Public Property CodigoPuntoServicio() As String
            Get
                Return _codigoPuntoServicio
            End Get
            Set(value As String)
                _codigoPuntoServicio = value
            End Set
        End Property


#End Region

    End Class

End Namespace