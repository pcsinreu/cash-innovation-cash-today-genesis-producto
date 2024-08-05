Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Divisa
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Denominaciones As List(Of Denominacion)
        Public Property Importe As Double
    End Class
End Namespace