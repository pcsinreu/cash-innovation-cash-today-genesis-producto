Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosAjuste.Salida

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace