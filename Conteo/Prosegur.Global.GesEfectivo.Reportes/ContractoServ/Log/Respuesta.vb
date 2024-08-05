Imports System.Xml.Serialization
Imports System.Xml

Namespace Log

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    <XmlType(Namespace:="urn:Log")> _
    <XmlRoot(Namespace:="urn:Log")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class

End Namespace