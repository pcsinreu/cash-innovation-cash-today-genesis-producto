Imports System.Xml.Serialization
Imports System.Xml

Namespace Utilidad.VerificarCodigoCliente

    ''' <summary>
    ''' Classe Peticion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danienines] 13/06/2013 Criado
    ''' </history>
    <XmlType(Namespace:="urn:VerificarCodigoCliente")> _
    <XmlRoot(Namespace:="urn:VerificarCodigoCliente")> _
    <Serializable()> _
    Public Class Peticion

#Region "[VARIÁVEIS]"

        Private _CodCliente As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property codCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

#End Region
    End Class
End Namespace