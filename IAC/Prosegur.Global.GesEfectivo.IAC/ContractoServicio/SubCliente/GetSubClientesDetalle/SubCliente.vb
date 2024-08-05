Imports System.Xml.Serialization

Namespace SubCliente.GetSubClientesDetalle

    <Serializable()> _
    <XmlType(Namespace:="urn:GetSubSubClientesDetalle")> _
    <XmlRoot(Namespace:="urn:GetSubSubClientesDetalle")> _
    Public Class SubCliente
        Inherits ContractoServicio.SubCliente.GetSubClientes.SubCliente

        Public Property CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelSaldo As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase

    End Class

End Namespace