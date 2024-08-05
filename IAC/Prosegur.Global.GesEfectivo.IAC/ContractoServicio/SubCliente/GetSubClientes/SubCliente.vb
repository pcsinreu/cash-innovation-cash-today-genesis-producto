Imports System.Xml.Serialization

Namespace SubCliente.GetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:GetSubClientes")> _
    <XmlRoot(Namespace:="urn:GetSubClientes")> _
    Public Class SubCliente

        Public Property OidSubCliente As String
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidTipoSubCliente As String
        Public Property CodTipoSubCliente As String
        Public Property DesTipoSubCliente As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime
    End Class

End Namespace