Imports System.Xml.Serialization
Imports System.Xml


Namespace ImportarParametros

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:ImportarParametros")> _
    <XmlRoot(Namespace:="urn:ImportarParametros")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace