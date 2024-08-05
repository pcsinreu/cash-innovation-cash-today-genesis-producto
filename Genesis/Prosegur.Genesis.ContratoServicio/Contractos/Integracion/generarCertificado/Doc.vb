Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.generarCertificado

    <XmlType(Namespace:="urn:generarCertificado")> _
    <XmlRoot(Namespace:="urn:generarCertificado")> _
    <Serializable()>
    Public Class Doc

        <XmlAttributeAttribute()> _
        Public CodExt As String

        <XmlAttributeAttribute()> _
        Public CodCom As String

        <XmlAttributeAttribute()> _
        Public FecGes As DateTime

        <XmlAttributeAttribute()> _
        Public FecRea As DateTime

        <XmlAttributeAttribute()> _
        Public CodFor As String

        <XmlAttributeAttribute()> _
        Public NomFor As String

        Public CueOri As Cue
        Public CueDes As Cue
        Public Divs As List(Of Div)
        Public CamAdis As List(Of CamAdi)

    End Class
End Namespace
