Imports System.Xml.Serialization

Namespace GenesisSaldos.Certificacion.GenerarCodigoCertificado

    <XmlType(Namespace:="urn:GenerarCodigoCertificado")> _
    <XmlRoot(Namespace:="urn:GenerarCodigoCertificado")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property CodCertificado As String

#End Region

    End Class

End Namespace