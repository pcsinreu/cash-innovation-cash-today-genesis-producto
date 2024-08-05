Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosCashIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosCashIn.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

    End Class

End Namespace