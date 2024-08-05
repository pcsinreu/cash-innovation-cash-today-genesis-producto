Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAcreditacion.Salida

    <XmlType(Namespace:="urn:AltaMovimientosAcreditacion.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAcreditacion.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace