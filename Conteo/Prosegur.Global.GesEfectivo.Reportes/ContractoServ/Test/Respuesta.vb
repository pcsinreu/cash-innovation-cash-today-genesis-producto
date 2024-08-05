Imports System.Xml.Serialization
Imports System.Xml

Namespace Test

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 08/02/2010 Criado
    ''' </history>
    <XmlType(Namespace:="urn:Test")> _
    <XmlRoot(Namespace:="urn:Test")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace