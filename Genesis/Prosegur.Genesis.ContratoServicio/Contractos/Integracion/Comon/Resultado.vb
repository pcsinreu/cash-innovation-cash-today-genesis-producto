Imports System.Xml.Serialization

Namespace Contractos.Integracion.Comon

    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String

        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Detalles As List(Of Detalle)
        Public Property Log As String

    End Class

End Namespace