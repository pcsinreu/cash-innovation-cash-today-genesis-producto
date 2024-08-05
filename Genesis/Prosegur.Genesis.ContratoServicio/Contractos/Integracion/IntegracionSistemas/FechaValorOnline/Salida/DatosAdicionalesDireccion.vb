Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class DatoAdicionalDireccion
        Public Property CampoAdicional1 As String
        Public Property CampoAdicional2 As String
        Public Property CampoAdicional3 As String
        Public Property CategoriaAdicional1 As String
        Public Property CategoriaAdicional2 As String
        Public Property CategoriaAdicional3 As String
    End Class
End Namespace