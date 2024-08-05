Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Entrada

    <XmlType(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Entrada")>
    <Serializable()>
    Public Class Planificacion

        <XmlAttributeAttribute()>
        Public Property Accion As String
        Public Property Codigo As String
        Public Property FechaHoraVigenciaInicio As DateTime?
        Public Property FechaHoraVigenciaFin As DateTime?

    End Class

End Namespace