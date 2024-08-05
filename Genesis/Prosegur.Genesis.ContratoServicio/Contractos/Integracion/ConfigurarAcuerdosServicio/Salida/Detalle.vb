Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarAcuerdosServicio.Salida

    <XmlType(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace