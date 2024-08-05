Imports System.Xml.Serialization

Namespace Cliente.GetClientesDetalle

    <Serializable()> _
    <XmlType(Namespace:="urn:GetClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetClientesDetalle")> _
    Public Class Cliente
        Inherits GetClientes.Cliente

        Public Property CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelMov As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase

    End Class

End Namespace