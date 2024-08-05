Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosShipIn.Salida

    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Salida")>
    <Serializable()>
    Public Class Detalle

        <XmlAttributeAttribute()>
        Public Codigo As String

        <XmlAttributeAttribute()>
        Public Descripcion As String

    End Class

End Namespace