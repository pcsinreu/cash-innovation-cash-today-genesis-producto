Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarDescripcionSubCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danienines] 13/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarDescripcionSubCliente")> _
    <XmlRoot(Namespace:="urn:VerificarDescripcionSubCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _CodCliente As String
        Private _DesSubCliente As String

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

        Public Property DesSubCliente() As String
            Get
                Return _DesSubCliente
            End Get
            Set(value As String)
                _DesSubCliente = value
            End Set
        End Property

#End Region
    End Class
End Namespace