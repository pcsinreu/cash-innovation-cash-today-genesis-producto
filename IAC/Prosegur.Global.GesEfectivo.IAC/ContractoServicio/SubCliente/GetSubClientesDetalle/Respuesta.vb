Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubClientesDetalle

    <XmlType(Namespace:="urn:GetSubClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetSubClientesDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property SubClientes As SubClienteColeccion(Of ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
        Public Property Resultado As String


    End Class

End Namespace