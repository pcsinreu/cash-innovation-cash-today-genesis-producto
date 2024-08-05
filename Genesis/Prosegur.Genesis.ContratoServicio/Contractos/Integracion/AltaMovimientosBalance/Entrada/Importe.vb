Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosBalance.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosBalance.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

    End Class

End Namespace