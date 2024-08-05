Imports System.Xml.Serialization

Namespace Contractos.Integracion.EnviarDocumentos.Salida

    <XmlType(Namespace:="urn:EnviarDocumentos.Salida")>
    <XmlRoot(Namespace:="urn:EnviarDocumentos.Salida")>
    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Log As String
        Public Property Detalles As List(Of Salida.Detalle)

    End Class

End Namespace