Imports System.Xml.Serialization

Namespace Contractos.Integracion.ConfigurarMAEs.Salida

    <XmlType(Namespace:="urn:ConfigurarMAEs.Salida")>
    <XmlRoot(Namespace:="urn:ConfigurarMAEs.Salida")>
    <Serializable()>
    Public Class Maquina

        <XmlAttributeAttribute()>
        Public Property DeviceID As String
        <XmlAttributeAttribute()>
        Public Property TipoResultado As String
        Public Property Detalles As List(Of Salida.Detalle)

    End Class

End Namespace