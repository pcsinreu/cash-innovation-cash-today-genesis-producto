Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Resultado_v2

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String

        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property Paginacion As Comon.Paginacion
        Public Property Detalles As List(Of Detalle)

    End Class

End Namespace