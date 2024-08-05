Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosMoveOut.Salida

    <XmlType(Namespace:="urn:AltaMovimientosMoveOut.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveOut.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace
