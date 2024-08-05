Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCierreFacturacion.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCierreFacturacion.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace