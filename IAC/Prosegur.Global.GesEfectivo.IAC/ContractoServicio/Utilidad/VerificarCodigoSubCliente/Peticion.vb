Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoSubCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danienines] 13/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoSubCliente")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoSubCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _CodCliente As String
        Private _CodSubCliente As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

#End Region
    End Class
End Namespace