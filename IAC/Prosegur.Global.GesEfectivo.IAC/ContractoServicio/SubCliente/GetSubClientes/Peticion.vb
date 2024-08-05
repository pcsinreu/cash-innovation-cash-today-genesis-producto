Imports System.Xml.Serialization
Imports System.Xml

Namespace SubCliente.GetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:GetSubClientes")> _
    <XmlRoot(Namespace:="urn:GetSubClientes")> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

        Public Property OidSubCliente As String
        Public Property CodCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidTipoSubCliente As String
        Public Property BolTotalizadorSaldo As Boolean?
        Public Property BolVigente As Boolean?

    End Class

End Namespace
