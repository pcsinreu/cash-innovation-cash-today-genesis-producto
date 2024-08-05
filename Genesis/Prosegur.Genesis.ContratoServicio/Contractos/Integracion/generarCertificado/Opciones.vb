Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Opciones

        <XmlAttributeAttribute()> _
        Public Certificado As Prosegur.Genesis.Comon.Enumeradores.TipoCertificado
        <XmlAttributeAttribute()> _
        Public ValidarPostError As Boolean
        <XmlAttributeAttribute()> _
        Public DetallarDesgloses As Boolean
        <XmlAttributeAttribute()> _
        Public CamposAdicionales As Boolean

    End Class
End Namespace
