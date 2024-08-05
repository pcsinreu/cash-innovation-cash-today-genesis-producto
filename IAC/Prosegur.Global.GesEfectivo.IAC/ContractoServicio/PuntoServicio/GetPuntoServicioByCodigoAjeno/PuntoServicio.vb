Imports System.Xml.Serialization

Namespace PuntoServicio.GetPuntoServicioByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    Public Class PuntoServicio

        Public Property OidPuntoServicio As String
        Public Property CodPuntoServicio As String
        Public Property DesPuntoServicio As String
        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property OidTipoPuntoServicio As String
        Public Property CodTipoPuntoServicio As String
        Public Property DesTipoPuntoServicio As String
        Public Property BolTotalizadorSaldo As Boolean

    End Class

End Namespace