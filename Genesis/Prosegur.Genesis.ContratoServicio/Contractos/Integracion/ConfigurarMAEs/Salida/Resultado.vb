Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Salida

    <XmlType(Namespace:="urn:ConfigurarMAEs.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Salida")>
    <Serializable()>
    Public Class Resultado

        <XmlAttributeAttribute()>
        Public TiempoDeEjecucion As String
        Public Property Tipo As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Detalles As List(Of Salida.Detalle)
        Public Property Log As String

    End Class

End Namespace