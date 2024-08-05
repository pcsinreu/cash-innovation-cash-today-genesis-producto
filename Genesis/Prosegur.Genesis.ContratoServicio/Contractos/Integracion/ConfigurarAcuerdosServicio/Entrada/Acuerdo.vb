Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarAcuerdosServicio.Entrada

    <XmlType(Namespace:="urn:ConfigurarAcuerdosServicio.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarAcuerdosServicio.Entrada")>
    <Serializable()>
    Public Class Acuerdo

        <XmlAttributeAttribute()>
        Public Property Accion As String
        Public Property ContractId As String
        Public Property ServiceOrderId As String
        Public Property ServiceOrderCode As String
        Public Property ProductCode As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaVigenciaInicio As DateTime
        Public Property FechaVigenciaFin As DateTime
    End Class

End Namespace
