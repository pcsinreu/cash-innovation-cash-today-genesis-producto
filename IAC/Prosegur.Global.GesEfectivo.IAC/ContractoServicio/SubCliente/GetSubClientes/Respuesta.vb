Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubClientes

    <XmlType(Namespace:="urn:GetSubClientes")> _
    <XmlRoot(Namespace:="urn:GetSubClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property SubClientes As SubClienteColeccion(Of SubCliente)
        Public Property Resultado As String

    End Class

End Namespace