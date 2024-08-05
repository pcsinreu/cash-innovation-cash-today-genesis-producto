Imports System.Xml.Serialization
Imports System.Xml

Namespace CodigoAjeno.GetAjenoByClienteSubClientePuntoServicio

    ''' <summary>
    ''' Classe peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 19/07/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:GetAjenoByClienteSubClientePuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetAjenoByClienteSubClientePuntoServicio")> _
    <Serializable()> _
    Public Class Peticion

#Region "[Variáveis]"

        Private _CodigoCliente As String
        Private _CodigoSubCliente As String
        Private _CodigoPuntoServicio As String
        Private _ValorPadron As Boolean

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

        Public Property ValorPadron() As Boolean
            Get
                Return _ValorPadron
            End Get
            Set(value As Boolean)
                _ValorPadron = value
            End Set
        End Property

#End Region

    End Class

End Namespace