Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarClientes.Salida

    <XmlType(Namespace:="urn:RecuperarClientes.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarClientes.Salida")>
    <Serializable()>
    Public Class Cliente
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Tipo As String
        Public Property CodigoBancario As String
        Public Property Vigente As Boolean
        Public Property DatosBancarios As List(Of DatoBancario)
        Public Property CodigosAjenos As List(Of CodigoAjeno)
        Public Property SubClientes As List(Of SubCliente)

    End Class
End Namespace