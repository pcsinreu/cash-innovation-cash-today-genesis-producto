Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarClientes.Salida

    <XmlType(Namespace:="urn:RecuperarClientes.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarClientes.Salida")>
    <Serializable()>
    Public Class SubCliente
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Vigente As Boolean
        Public Property DatosBancarios As List(Of DatoBancario)
        Public Property CodigosAjenos As List(Of CodigoAjeno)
        Public Property PuntosServicio As List(Of PuntoServicio)

    End Class
End Namespace