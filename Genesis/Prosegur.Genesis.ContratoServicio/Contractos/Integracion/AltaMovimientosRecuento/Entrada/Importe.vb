Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosRecuento.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosRecuento.Entrada")>
    <Serializable()>
    Public Class Importe

        Public Property CodigoDivisa As String
        Public Property CodigoDenominacion As String
        Public Property Cantidad As Integer
        Public Property Importe As Double

        <XmlAttributeAttribute()>
        Public Property Tipo As TipoValor

    End Class

    <Serializable()>
    Public Enum TipoValor
        Contado = 0
        Declarado = 1
        Diferencia = 2
    End Enum

End Namespace