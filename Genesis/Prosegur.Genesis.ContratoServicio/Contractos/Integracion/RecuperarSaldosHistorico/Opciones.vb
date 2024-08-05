Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosHistorico
    <XmlType(Namespace:="urn:RecuperarSaldosHistorico")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosHistorico")>
    <Serializable()>
    Public Class Opciones

        <XmlAttribute(DataType:="string", AttributeName:="ValoresDetallar")>
        <DefaultValue("")>
        Public Property ValoresDetallar As String

        <XmlAttribute(DataType:="string", AttributeName:="DeviceIDCompleto")>
        <DefaultValue("")>
        Public Property DeviceIDCompleto As String
    End Class

End Namespace