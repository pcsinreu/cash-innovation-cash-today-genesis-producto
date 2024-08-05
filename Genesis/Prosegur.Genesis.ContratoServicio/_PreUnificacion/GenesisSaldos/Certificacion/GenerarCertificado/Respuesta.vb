Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.GenerarCertificado

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:GenerarCertificado")> _
    <XmlRoot(Namespace:="urn:GenerarCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

    End Class
End Namespace