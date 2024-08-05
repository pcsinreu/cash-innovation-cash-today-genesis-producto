Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Entrada

    <XmlType(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <Serializable()>
    Public Class PuntoServicio

        <XmlAttributeAttribute()>
        Public Property Accion As String
        Public Property Codigo As String

    End Class

End Namespace