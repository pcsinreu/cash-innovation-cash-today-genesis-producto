Imports System.Xml.Serialization
Imports System.Xml

Namespace Cliente.GetClientesDetalle

    <XmlType(Namespace:="urn:GetClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetClientesDetalle")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits Paginacion.RespuestaPaginacionBase

        Public Property Clientes As ClienteColeccion(Of ContractoServicio.Cliente.GetClientesDetalle.Cliente)
        Public Property Resultado As String


    End Class

End Namespace