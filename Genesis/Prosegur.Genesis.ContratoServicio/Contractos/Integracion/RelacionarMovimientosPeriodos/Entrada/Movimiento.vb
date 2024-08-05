Imports System.ComponentModel
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RelacionarMovimientosPeriodos.Entrada

    <XmlType(Namespace:="urn:RelacionarMovimientosPeriodos.Entrada")>
    <XmlRoot(Namespace:="urn:RelacionarMovimientosPeriodos.Entrada")>
    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        <DefaultValue(0)>
        Public Property Accion As Entrada.Enumeradores.Accion
        <XmlAttributeAttribute()>
        <DefaultValue(0)>
        Public Property TipoCodigo As Entrada.Enumeradores.TipoCodigo
        Public Property Codigo As String

    End Class

End Namespace
