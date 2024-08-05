Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Planificacion
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property FechaHoraVigenciaInicio As DateTimeOffset?
        Public Property FechaHoraVigenciaFin As DateTimeOffset?
        Public Property Vigente As String
        Public Property MinutosAcreditacion As String
        Public Property AgrupacionSubcanales As Boolean
        Public Property AgrupacionFechaContable As Boolean
        Public Property AgrupacionPtoServicio As Boolean
        Public Property Tipo As ContractoServicio.Contractos.Integracion.Comon.Entidad
        Public Property Banco As ContractoServicio.Contractos.Integracion.Comon.Entidad
        Public Property Delegacion As ContractoServicio.Contractos.Integracion.Comon.Entidad
        Public Property Canales As List(Of Canal)
        Public Property Programaciones As List(Of Programacion)

    End Class
End Namespace