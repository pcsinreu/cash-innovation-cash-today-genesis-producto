Imports System.Xml.Serialization

Namespace PuntoServicio.GetPuntoServicioDetalle

    <Serializable()> _
    <XmlType(Namespace:="urn:GetPuntoServicioDetalle")> _
    <XmlRoot(Namespace:="urn:GetPuntoServicioDetalle")> _
    Public Class PuntoServicio
        Inherits GetPuntoServicio.PuntoServicio

        Public Property CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelSaldo As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase

    End Class

End Namespace