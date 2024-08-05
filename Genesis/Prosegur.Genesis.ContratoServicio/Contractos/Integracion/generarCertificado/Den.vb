Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Den

        <XmlAttributeAttribute()> _
        Public CodDen As String
        <XmlAttributeAttribute()> _
        Public Can As Integer
        <XmlAttributeAttribute()> _
        Public Imp As Decimal

    End Class
End Namespace
