Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosAcreditacion.Entrada

    '<XmlType(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
    '<XmlRoot(Namespace:="urn:AltaMovimientosAcreditacion.Entrada")>
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
        Acreditacion = 0
        Comision = 1
        Total = 2
    End Enum

End Namespace