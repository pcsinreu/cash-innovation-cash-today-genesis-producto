Imports System.Xml.Serialization

Namespace Cliente.SetClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    Public Class ConfigNivelSaldo
        Inherits Utilidad.GetConfigNivel.ConfigNivelSaldo

        Public Property OidSubCanal As String

    End Class
End Namespace

