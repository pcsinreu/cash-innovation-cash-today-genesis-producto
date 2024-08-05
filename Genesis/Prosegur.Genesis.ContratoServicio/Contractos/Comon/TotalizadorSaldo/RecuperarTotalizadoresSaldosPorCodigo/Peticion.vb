Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTotalizadoresSaldosPorCodigo")> _
    <XmlRoot(Namespace:="urn:RecuperarTotalizadoresSaldosPorCodigo")> _
    Public Class Peticion
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BasePeticion

        Public Property CodigoClienteSaldo As String
        Public Property CodigoSubClienteSaldo As String
        Public Property CodigoPuntoServicioSaldo As String
        Public Property CodigoClienteMovimiento As String
        Public Property CodigoSubClienteMovimiento As String
        Public Property CodigoPuntoServicioMovimiento As String
        Public Property CodigoSubCanal As String
    End Class

End Namespace
