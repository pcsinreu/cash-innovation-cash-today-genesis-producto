Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosProvisionEfectivo.Salida

    <XmlType(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <XmlRoot(Namespace:="urn:AltaMovimientosProvisionEfectivo.Salida")>
    <Serializable()>
    Public Class MovimientoProvisionEfectivo

        Public Property CodigoBancoCapital As String
        Public Property FechaHora As DateTime
        Public Property FechaContable As DateTime?
        Public Property ActualId As String
        Public Property CollectionId As String
        Public Property Detalles As List(Of Detalle)

        <XmlAttributeAttribute()>
        Public Property TipoResultado As String

        Public Property Documentos As List(Of String)

    End Class
End Namespace