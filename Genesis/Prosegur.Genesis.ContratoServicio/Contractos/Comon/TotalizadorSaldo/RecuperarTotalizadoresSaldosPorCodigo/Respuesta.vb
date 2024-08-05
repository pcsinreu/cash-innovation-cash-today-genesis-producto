Imports System.Xml.Serialization
Imports Prosegur.Genesis.Comon

Namespace Contractos.Comon.TotalizadorSaldo.RecuperarTotalizadoresSaldosPorCodigo

    <Serializable()> _
    <XmlType(Namespace:="urn:RecuperarTotalizadoresSaldosPorCodigo")> _
    <XmlRoot(Namespace:="urn:RecuperarTotalizadoresSaldosPorCodigo")> _
    Public Class Respuesta
        Inherits Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.BaseRespuesta

        Public Property TotalizadoresSaldos As List(Of Clases.TotalizadorSaldo)

    End Class

End Namespace

