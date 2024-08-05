Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConsultarSaldos

    <XmlType(Namespace:="urn:ConsultarSaldos")> _
    <XmlRoot(Namespace:="urn:ConsultarSaldos")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseRespuesta

        Public Property Saldo As Saldo

    End Class

End Namespace
