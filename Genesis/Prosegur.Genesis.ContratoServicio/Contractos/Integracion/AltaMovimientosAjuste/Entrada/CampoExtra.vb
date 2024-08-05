Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosAjuste.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosAjuste.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace