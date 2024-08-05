Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarInformaciones.Salida

    <XmlType(Namespace:="urn:RecuperarInformaciones.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarInformaciones.Salida")>
    <Serializable()>
    Public Class Movimiento

        <XmlAttributeAttribute()>
        Public Property Codigo As String
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Comon.Detalle)

        Public Property CodigoExterno As String
        Public Property CodigoDelegacion As String
        Public Property CodigoPlanta As String
        Public Property CodigoSector As String

        Public Property HayPeriodos As Boolean


    End Class

End Namespace