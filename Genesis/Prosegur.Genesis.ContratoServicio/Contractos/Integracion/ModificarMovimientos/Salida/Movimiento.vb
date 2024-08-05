Imports System.ComponentModel
Imports System.Xml.Serialization
Namespace Contractos.Integracion.ModificarMovimientos.Salida


    <XmlType(Namespace:="urn:ModificarMovimientos.Salida")>
    <XmlRoot(Namespace:="urn:ModificarMovimientos.Salida")>
    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        Public Property Codigo As String

        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Detalle)

    End Class
End Namespace