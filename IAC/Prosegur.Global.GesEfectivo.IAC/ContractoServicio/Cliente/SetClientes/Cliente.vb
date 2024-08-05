Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio

Namespace Cliente.SetClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:SetClientes")> _
    <XmlRoot(Namespace:="urn:SetClientes")> _
    Public Class Cliente
        Inherits RespuestaGenerico

        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property oidTipoCliente As String
        Public Property CodTipoCliente As String
        Public Property DesTipoCliente As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime
        Public Property BolClienteTotSaldo As Boolean
        Public Property BolAbonaPorTotalSaldo As Boolean
        Public Property CodBancario As String
        Public Property PorcComisionCliente As Nullable(Of Decimal)
        Public Property BolBancoComision As Boolean
        Public Property BolBancoCapital As Boolean
        Public Property BolGrabaSaldoHistorico As Boolean
        Public Property CodFechaSaldoHistorico As String
        Public Property CodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Public Property ConfigNivelSaldo As ConfigNivelMovColeccion
        Public Property Direcciones As Direccion.DireccionColeccionBase
        Public Property PeticionDatosBancarios As Contractos.Integracion.ConfigurarDatosBancarios.Peticion
    End Class

End Namespace