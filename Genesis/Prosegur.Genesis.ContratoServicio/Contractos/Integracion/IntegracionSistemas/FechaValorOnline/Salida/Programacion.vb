Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Programacion
        Public Property HoraInicio As String
        Public Property DiaInicio As String
        Public Property HoraFin As String
        Public Property DiaFin As String
    End Class
End Namespace