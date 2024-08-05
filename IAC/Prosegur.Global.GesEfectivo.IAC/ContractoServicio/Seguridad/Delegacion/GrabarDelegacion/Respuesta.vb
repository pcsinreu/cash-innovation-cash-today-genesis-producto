Imports System.Xml.Serialization

Namespace Seguridad.Delegacion.GrabarDelegacion

    ''' <summary>
    ''' Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes]  30/08/2013  Criado
    ''' </history>
    <Serializable()> _
    <XmlType(Namespace:="urn:GrabarDelegacion")> _
    <XmlRoot(Namespace:="urn:GrabarDelegacion")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class

End Namespace