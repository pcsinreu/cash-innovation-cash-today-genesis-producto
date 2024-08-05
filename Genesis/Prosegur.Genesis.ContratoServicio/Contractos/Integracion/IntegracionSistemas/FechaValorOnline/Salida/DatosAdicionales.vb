Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class DatosAdicionales
        Public Property CampoAdicional1 As String
        Public Property CampoAdicional2 As String
        Public Property CampoAdicional3 As String
        Public Property CampoAdicional4 As String
        Public Property CampoAdicional5 As String
        Public Property CampoAdicional6 As String
        Public Property CampoAdicional7 As String
        Public Property CampoAdicional8 As String
    End Class
End Namespace