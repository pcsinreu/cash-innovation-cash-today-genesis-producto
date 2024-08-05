Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace SubCliente.SetSubClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetSubClientes")> _
    <XmlRoot(Namespace:="urn:SetSubClientes")> _
    Public Class SubCliente
        Inherits RespuestaGenerico

        Public Property OidSubCliente As String
        Public Property CodSubCliente As String
        Public Property DesSubCliente As String
        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property OidTipoSubCliente As String
        Public Property CodTipoSubCliente As String
        Public Property DesTipoSubCliente As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime
        Public Property BolSubClienteTotSaldo As Boolean

        Public Property CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelSaldo As ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase
        Public Property PeticionDatosBancarios As Contractos.Integracion.ConfigurarDatosBancarios.Peticion

    End Class

End Namespace