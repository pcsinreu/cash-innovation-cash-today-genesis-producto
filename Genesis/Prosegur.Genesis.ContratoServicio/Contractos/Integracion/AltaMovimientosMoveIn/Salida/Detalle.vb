Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosMoveIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace