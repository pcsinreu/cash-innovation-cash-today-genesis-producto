Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosMoveIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

    End Class

End Namespace