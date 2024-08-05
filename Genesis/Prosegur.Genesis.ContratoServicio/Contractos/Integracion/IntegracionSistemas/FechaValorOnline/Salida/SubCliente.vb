Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class SubCliente
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property DatosBancarios As List(Of DatoBancario)
        Public Property Direccion As Direccion
    End Class
End Namespace