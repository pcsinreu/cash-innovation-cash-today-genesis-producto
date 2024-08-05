Imports System.Xml.Serialization
Imports System.Xml

Namespace ValorPosible.GetValoresPosibles

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetValoresPosibles")> _
    <XmlRoot(Namespace:="urn:GetValoresPosibles")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CodigoPuntoServicio As String
        Private _CodigoTermino As String

#End Region

#Region "[Propriedades]"

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

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace