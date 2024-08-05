Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarClientes.Salida

    <XmlType(Namespace:="urn:ConfigurarClientes.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Salida")>
    <Serializable()>
    Public Class Cliente
        <XmlIgnore()>
        Public Property Indice As Integer
        <XmlAttributeAttribute()>
        Public Property Codigo As String
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Detalle)
        Public Property SubClientes As List(Of Salida.SubCliente)
    End Class
End Namespace