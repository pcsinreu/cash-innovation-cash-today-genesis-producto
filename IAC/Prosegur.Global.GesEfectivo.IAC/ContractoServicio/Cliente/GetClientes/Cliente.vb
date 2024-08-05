Imports System.Xml.Serialization

Namespace Cliente.GetClientes

    <Serializable()> _
    <XmlType(Namespace:="urn:GetClientes")> _
    <XmlRoot(Namespace:="urn:GetClientes")> _
    Public Class Cliente

        Public Property OidCliente As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property OidTipoCliente As String
        Public Property CodTipoCliente As String
        Public Property DesTipoCliente As String
        Public Property BolTotalizadorSaldo As Boolean
        Public Property BolVigente As Boolean
        Public Property FyhActualizacion As DateTime
        Public Property BolAbonaPorSaldoTotal As Boolean
        Public Property CodBancario As String
        Public Property PorcComisionCliente As Nullable(Of Decimal)
        Public Property BolBancoComision As Boolean
        Public Property BolBancoCapital As Boolean
        Public Property BolGrabaSaldoHistorico As Boolean
        Public Property CodFechaSaldoHistorico As String

    End Class

End Namespace