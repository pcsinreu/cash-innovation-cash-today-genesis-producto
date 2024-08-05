Imports System.Xml.Serialization

Namespace SubCliente.GetSubclienteByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetSubclienteByCodigoAjeno")> _
    Public Class SubCliente

        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property OidTipoSubCliente As String
        Public Property CodTipoSubCliente As String
        Public Property DesTipoSubCliente As String
        Public Property BolTotalizadorSaldo As Boolean

    End Class

End Namespace