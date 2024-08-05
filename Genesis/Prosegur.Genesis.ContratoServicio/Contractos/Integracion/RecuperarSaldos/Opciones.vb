Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldos

    <Serializable()>
    Public Class Opciones

        <XmlAttribute(DataType:="string", AttributeName:="TransaccionesDetallar")>
        <DefaultValue("")>
        Public Property TransaccionesDetallar As String


        <XmlAttribute(DataType:="string", AttributeName:="ValoresDetallar")>
        <DefaultValue("")>
        Public Property ValoresDetallar As String

        <XmlAttribute(DataType:="string", AttributeName:="BolsasDetallar")>
        <DefaultValue("")>
        Public Property BolsasDetallar As String

        <XmlAttribute(DataType:="string", AttributeName:="Disponible")>
        <DefaultValue("")>
        Public Property Disponible As String

        <XmlAttribute(DataType:="string", AttributeName:="InfoAdicionales")>
        <DefaultValue("")>
        Public Property InfoAdicionales As String

        <XmlAttribute(DataType:="string", AttributeName:="Acreditado")>
        <DefaultValue("")>
        Public Property Acreditado As String

        <XmlAttribute(DataType:="string", AttributeName:="Notificado")>
        <DefaultValue("")>
        Public Property Notificado As String

    End Class
End Namespace
