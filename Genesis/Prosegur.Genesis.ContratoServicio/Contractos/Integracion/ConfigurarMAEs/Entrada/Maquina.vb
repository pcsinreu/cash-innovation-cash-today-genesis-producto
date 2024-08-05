Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Entrada

    <XmlType(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <Serializable()>
    Public Class Maquina

        <XmlAttributeAttribute()>
        Public Property Accion As String
        Public Property DeviceID As String
        Public Property Descripcion As String
        Public Property CodigoDelegacion As String
        Public Property CodigoModelo As String
        Public Property CodigoFabricante As String
        Public Property Multicliente As String
        Public Property ConsideraRecuentos As String
        Public Property PuntosServicio As List(Of PuntoServicio)
        Public Property Planificacion As Planificacion
        Public Property Limites As List(Of Limite)

    End Class

End Namespace
