Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarAcuerdosServicio.Salida

    <XmlType(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarAcuerdosServicio.Salida")>
    <Serializable()>
    Public Class Acuerdo
        Public Property ContractId As String
        Public Property ServiceOrderId As String
        Public Property ServiceOrderCode As String
        Public Property ProductId As String
        Public Property ProductCode As String
        Public Property CodigoCliente As String
        Public Property CodigoSubCliente As String
        Public Property CodigoPuntoServicio As String
        Public Property FechaVigenciaInicio As DateTime
        Public Property FechaVigenciaFin As DateTime
        Public Property Detalles As List(Of Salida.Detalle)

    End Class
End Namespace

