Imports System.Xml.Serialization
Imports System.Xml

Namespace Test

    ''' <summary>
    ''' Representa os dados de saida
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 18/06/2010 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:Test")> _
    <XmlRoot(Namespace:="urn:Test")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace