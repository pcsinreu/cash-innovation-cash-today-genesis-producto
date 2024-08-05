Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosShipIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosShipIn.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

    End Class

End Namespace