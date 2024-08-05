Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarClientes.Salida

    <XmlType(Namespace:="urn:ConfigurarClientes.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace