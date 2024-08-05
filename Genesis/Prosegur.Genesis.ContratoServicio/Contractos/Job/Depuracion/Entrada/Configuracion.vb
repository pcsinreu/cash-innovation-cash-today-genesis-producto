Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Job.Depuracion.Entrada
    <XmlType(Namespace:="urn:Depuracion.Entrada")>
    <XmlRoot(Namespace:="urn:Depuracion.Entrada")>
    <Serializable()>
    Public Class Configuracion

        <XmlAttribute(DataType:="string", AttributeName:="Token")>
        <DefaultValue("")>
        Public Property Token As String

        <XmlAttribute(DataType:="boolean", AttributeName:="LogDetallar")>
        <DefaultValue(False)>
        Public Property LogDetallar As Boolean

        <XmlAttribute(DataType:="boolean", AttributeName:="RespuestaDetallar")>
        <DefaultValue(True)>
        Public Property RespuestaDetallar As Boolean

        <XmlAttribute(DataType:="string", AttributeName:="Usuario")>
        <DefaultValue("")>
        Public Property Usuario As String

        <XmlAttribute(DataType:="string", AttributeName:="IdentificadorAjeno")>
        <DefaultValue("")>
        Public Property IdentificadorAjeno As String
    End Class
End Namespace