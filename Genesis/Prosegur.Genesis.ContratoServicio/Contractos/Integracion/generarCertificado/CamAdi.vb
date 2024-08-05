Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class CamAdi

        <XmlAttributeAttribute()> _
        Public Nom As String
        <XmlAttributeAttribute()> _
        Public Val As String

    End Class
End Namespace
