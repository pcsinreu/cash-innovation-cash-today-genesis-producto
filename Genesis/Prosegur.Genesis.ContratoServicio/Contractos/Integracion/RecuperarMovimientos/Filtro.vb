Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarMovimientos

    <Serializable()>
    Public Class Filtro

        <XmlAttribute(DataType:="string", AttributeName:="ValoresDetallar")>
        <DefaultValue("")>
        Public Property ValoresDetallar As String

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

        <XmlAttribute(DataType:="string", AttributeName:="MaquinasVigente")>
        <DefaultValue("")>
        Public Property MaquinasVigente As String

        <XmlAttribute(DataType:="string", AttributeName:="IncluirValoresInformativos")>
        <DefaultValue("")>
        Public Property IncluirValoresInformativos As String

        <XmlAttribute(DataType:="string", AttributeName:="IncluirCollectionID")>
        <DefaultValue("")>
        Public Property IncluirCollectionID As String
    End Class
End Namespace