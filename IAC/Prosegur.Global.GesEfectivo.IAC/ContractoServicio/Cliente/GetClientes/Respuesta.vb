Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClientes

    <XmlType(Namespace:="urn:GetClientes")> _
    <XmlRoot(Namespace:="urn:GetClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property Clientes As ClienteColeccion(Of Cliente)
        Public Property Resultado As String

    End Class

End Namespace