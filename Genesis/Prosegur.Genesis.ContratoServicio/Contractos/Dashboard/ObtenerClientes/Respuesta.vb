Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.Dashboard.ObtenerClientes

    <XmlType(Namespace:="urn:ObtenerClientes")> _
    <XmlRoot(Namespace:="urn:ObtenerClientes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property ListaCliente As List(Of Cliente)

#End Region

    End Class

    <XmlType(Namespace:="urn:ObtenerClientes")> _
    <XmlRoot(Namespace:="urn:ObtenerClientes")> _
    <Serializable()> _
    Public Class Cliente
        Public Property CodCliente As String
        Public Property DesCliente As String

    End Class

End Namespace