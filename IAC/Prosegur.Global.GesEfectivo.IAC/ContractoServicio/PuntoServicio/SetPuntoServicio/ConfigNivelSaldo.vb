Imports System.Xml.Serialization

Namespace PuntoServicio.SetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    Public Class ConfigNivelSaldo
        Inherits Utilidad.GetConfigNivel.ConfigNivelSaldo

        Public Property OidSubCanal As String

    End Class
End Namespace

