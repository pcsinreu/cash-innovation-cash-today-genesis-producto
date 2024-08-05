Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.GenesisSaldos.Saldo.ActualizarSaldosHistoricoCliente

    <XmlType(Namespace:="urn:ActualizarSaldosHistoricoCliente")>
    <XmlRoot(Namespace:="urn:ActualizarSaldosHistoricoCliente")>
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion
        Public Property IdentificadorCliente As String = String.Empty
        Public Property IdentificadorPais As String = String.Empty
        Public Property Cultura As String = String.Empty
        Public Property Usuario As String = String.Empty


    End Class
End Namespace
