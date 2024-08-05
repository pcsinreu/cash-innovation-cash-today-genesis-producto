Imports System.Xml
Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.DatosCertificacion

    ''' <summary>
    ''' Classe Respuesta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 27/05/2013 - Criado
    ''' </history>
    <XmlType(Namespace:="urn:DatosCertificacion")> _
    <XmlRoot(Namespace:="urn:DatosCertificacion")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property Certificado As GenesisSaldos.Certificacion.Certificado

#End Region

    End Class
End Namespace