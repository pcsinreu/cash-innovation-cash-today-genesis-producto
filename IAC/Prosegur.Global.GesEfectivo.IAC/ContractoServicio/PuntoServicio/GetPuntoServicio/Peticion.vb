Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.GetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:GetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicio")> _
    Public Class Peticion
        Inherits Paginacion.PeticionPaginacionBase

        Public Property CodCliente As String
        Public Property CodSubcliente As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property OidTipoPuntoServicio As String
        Public Property BolTotalizadorSaldo As Boolean?
        Public Property BolVigente As Boolean?
        Public Property OidPuntoServicio As String

    End Class

End Namespace
