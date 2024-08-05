Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Delegacion
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property CodigosAjenos As List(Of CodigoAjeno)
    End Class
End Namespace