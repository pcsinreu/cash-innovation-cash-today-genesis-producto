Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace