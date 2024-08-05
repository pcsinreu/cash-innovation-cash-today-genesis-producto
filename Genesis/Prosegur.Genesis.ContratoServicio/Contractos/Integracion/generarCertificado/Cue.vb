Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Cue


        <XmlAttributeAttribute()> _
        Public CodCli As String
        <XmlAttributeAttribute()> _
        Public DesCli As String
        <XmlAttributeAttribute()> _
        Public CodSub As String
        <XmlAttributeAttribute()> _
        Public DesSub As String
        <XmlAttributeAttribute()> _
        Public CodPun As String
        <XmlAttributeAttribute()> _
        Public DesPun As String
        <XmlAttributeAttribute()> _
        Public CodDel As String
        <XmlAttributeAttribute()> _
        Public DesDel As String
        <XmlAttributeAttribute()> _
        Public CodPla As String
        <XmlAttributeAttribute()> _
        Public DesPla As String
        <XmlAttributeAttribute()> _
        Public CodSec As String
        <XmlAttributeAttribute()> _
        Public DesSec As String
        <XmlAttributeAttribute()> _
        Public CodCan As String
        <XmlAttributeAttribute()> _
        Public DesCan As String
        <XmlAttributeAttribute()> _
        Public CodSCa As String
        <XmlAttributeAttribute()> _
        Public DesSCa As String

    End Class
End Namespace
