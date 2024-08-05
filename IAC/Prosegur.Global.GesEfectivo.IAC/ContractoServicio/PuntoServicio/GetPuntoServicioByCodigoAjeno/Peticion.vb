Imports System.Xml.Serialization
Imports System.Xml

Namespace PuntoServicio.GetPuntoServicioByCodigoAjeno

    <Serializable()> _
    <XmlType(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicioByCodigoAjeno")> _
    Public Class Peticion

        Public Property identificadorAjeno As String
        Public Property puntoServicioCodigoAjeno As String

    End Class

End Namespace
