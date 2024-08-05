Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Div

        <XmlAttributeAttribute()> _
        Public CodDiv As String
        <XmlAttributeAttribute()> _
        Public Imp As String
        Public Dens As List(Of Den)
        Public MedPags As List(Of MedPag)
        <XmlAttributeAttribute()> _
        Public Tot As String

    End Class
End Namespace
