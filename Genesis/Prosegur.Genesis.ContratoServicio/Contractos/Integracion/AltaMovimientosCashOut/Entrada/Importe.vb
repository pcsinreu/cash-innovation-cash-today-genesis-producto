Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashOut.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

    End Class

End Namespace