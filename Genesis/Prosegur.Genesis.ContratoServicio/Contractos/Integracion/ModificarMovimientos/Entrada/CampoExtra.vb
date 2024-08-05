Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ModificarMovimientos.Entrada

    <XmlType(Namespace:="urn:ModificarMovimientos.Salida")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos.Salida")>
    <Serializable()>
    Public Class CampoExtra
        ''' <summary>
        ''' Nombre del campo CampoExtra a ser modificado
        ''' </summary>
        <XmlAttribute(DataType:="string", AttributeName:="Campo")>
        <DefaultValue("")>
        Public Property Campo As String
        ''' <summary>
        ''' Nuevo valor para el campo el campo CampoExtra.
        ''' </summary>
        <XmlAttribute(DataType:="string", AttributeName:="Valor")>
        <DefaultValue("")>
        Public Property Valor As String

    End Class
End Namespace
