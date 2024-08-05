Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace PuntoServicio.SetPuntoServicio

    <Serializable()> _
    <XmlType(Namespace:="urn:SetPuntoServicio")> _
    <XmlRoot(Namespace:="urn:SetPuntoServicio")> _
    Public Class PuntoServicio
        Inherits RespuestaGenerico

        Public Property OidPuntoServicio As String
        Public Property CodCliente As String
        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property CodPuntoServicio As String
        Public Property DesPuntoServicio As String
        Public Property OidTipoPuntoServicio As String
        Public Property CodTipoPuntoServicio As String
        Public Property DesTipoPuntoServicio As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime
        Public Property BolPuntoServicioTotSaldo As Boolean

        Public Property CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelSaldo As ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase
        Public Property PeticionDatosBancarios As Contractos.Integracion.ConfigurarDatosBancarios.Peticion

    End Class

End Namespace