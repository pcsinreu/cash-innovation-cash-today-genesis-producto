Imports System.Xml.Serialization

Namespace Contractos.Integracion.AltaMovimientosMoveOut.Entrada

    <XmlType(Namespace:="urn:AltaMovimientosMoveOut.Entrada")>
    <XmlRoot(Namespace:="urn:AltaMovimientosMoveOut.Entrada")>
    <Serializable()>
    Public Class CampoExtra
        Public Property Codigo As String
        Public Property Valor As String
    End Class
End Namespace