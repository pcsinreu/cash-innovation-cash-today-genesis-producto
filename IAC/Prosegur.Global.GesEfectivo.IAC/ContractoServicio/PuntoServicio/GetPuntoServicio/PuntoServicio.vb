Imports System.Xml.Serialization

Namespace PuntoServicio.GetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:GetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicio")> _
    Public Class PuntoServicio

        Public Property OidPuntoServicio As String

        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String

        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String

        Public Property CodPuntoServicio As String
        Public Property DesPuntoServicio As String
        Public Property OidTipoPuntoServicio As String
        Public Property CodTipoPuntoServicio As String
        Public Property DesTipoPuntoServicio As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime

    End Class

End Namespace