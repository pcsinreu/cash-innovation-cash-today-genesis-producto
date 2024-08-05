Imports System.Xml.Serialization
Imports System.Xml

Namespace ValorPosible.SetValoresPosibles

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 16/02/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:SetValoresPosibles")> _
    <XmlRoot(Namespace:="urn:SetValoresPosibles")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CodigoPuntoServicio As String
        Private _CodigoUsuario As String
        Private _Termino As Termino

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

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
            End Set
        End Property

        Public Property Termino() As Termino
            Get
                Return _Termino
            End Get
            Set(value As Termino)
                _Termino = value
            End Set
        End Property

#End Region

    End Class

End Namespace