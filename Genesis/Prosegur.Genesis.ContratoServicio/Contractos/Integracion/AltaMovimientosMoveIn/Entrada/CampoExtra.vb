Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.AltaMovimientosMoveIn.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveIn.Entrada")>
    <Serializable()>
    Public Class CampoExtra

        Public Property Codigo As String
        Public Property Valor As String

    End Class
End Namespace