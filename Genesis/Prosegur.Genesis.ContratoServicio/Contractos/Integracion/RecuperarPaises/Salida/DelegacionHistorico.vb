Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarPaises.Salida

    <XmlType(Namespace:="urn:RecuperarPaises.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarPaises.Salida")>
    <Serializable()>
    Public Class DelegacionHistorico
        Public Property Identificador As String
        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property Vigente As Boolean
        Public Property GMT As Integer
        Public Property VeranoFechaInicio As DateTime
        Public Property VeranoFechaFin As DateTime
        Public Property VeranoMinutosAjuste As Integer
        Public Property Zona As String
        Public Property FechaHora As DateTime

    End Class
End Namespace