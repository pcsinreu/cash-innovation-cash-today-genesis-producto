Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldos

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTotalizadoresSaldos")> _
    <XmlRoot(Namespace:="urn:RecuperarTotalizadoresSaldos")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        Public Property IdentificadorClienteSaldo As String
        Public Property IdentificadorSubClienteSaldo As String
        Public Property IdentificadorPuntoServicioSaldo As String
        Public Property IdentificadorClienteMovimiento As String
        Public Property IdentificadorSubClienteMovimiento As String
        Public Property IdentificadorPuntoServicioMovimiento As String
        Public Property IdentificadorSubCanal As String
    End Class

End Namespace
