Imports System.Xml.Serialization

Namespace Cliente.GetClienteByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetClienteByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetClienteByCodigoAjeno")> _
    Public Class Cliente

        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property desCliente As String
        Public Property oidTipoCliente As String
        Public Property codTipoCliente As String
        Public Property desTipoCliente As String
        Public Property bolTotalizadorSaldo As Boolean

    End Class

End Namespace