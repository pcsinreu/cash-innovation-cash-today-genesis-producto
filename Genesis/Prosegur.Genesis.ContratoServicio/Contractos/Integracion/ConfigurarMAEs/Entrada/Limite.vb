Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Entrada

    <XmlType(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <Serializable()>
    Public Class Limite
        <XmlAttributeAttribute()>
        Public Property Accion As String
        Public Property CodigoDivisa As String
        Public Property CodigoPuntoServicio As String
        Public Property NumLimite As Decimal

    End Class

End Namespace
