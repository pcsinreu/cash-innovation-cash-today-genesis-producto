Imports System.Xml.Serialization

Namespace SubCliente.SetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetSubClientes")> _
    <XmlRoot(Namespace:="urn:SetSubClientes")> _
    Public Class ConfigNivelSaldo
        Inherits Utilidad.GetConfigNivel.ConfigNivelSaldo

        Public Property OidSubCanal As String

    End Class
End Namespace

