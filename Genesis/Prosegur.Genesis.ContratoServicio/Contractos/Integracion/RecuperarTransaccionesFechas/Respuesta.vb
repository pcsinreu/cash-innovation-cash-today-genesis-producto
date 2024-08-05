Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransaccionesFechas

    <XmlType(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseRespuesta

        Public Property Transacciones As List(Of Transacion)

    End Class

End Namespace
