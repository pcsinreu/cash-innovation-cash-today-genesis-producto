Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosRecuento.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace