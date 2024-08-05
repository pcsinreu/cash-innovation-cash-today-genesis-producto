Imports System.ComponentModel
Imports System.Xml.Serialization
Namespace Contractos.Integracion.RecuperarMAEsPlanificadas
    <XmlType(Namespace:="urn:RecuperarMAEsPlanificadas")>
    <XmlRoot(Namespace:="urn:RecuperarMAEsPlanificadas")>
    Public Class Maquina

        <XmlAttribute(DataType:="string", AttributeName:="DeviceID")>
        <DefaultValue("")>
        Public Property DeviceID As String

        <XmlAttribute(DataType:="string", AttributeName:="Codigo")>
        <DefaultValue("")>
        Public Property Codigo As String


        <XmlAttribute(DataType:="string", AttributeName:="Resultado")>
        <DefaultValue("")>
        Public Property Resultado As String

    End Class
End Namespace


