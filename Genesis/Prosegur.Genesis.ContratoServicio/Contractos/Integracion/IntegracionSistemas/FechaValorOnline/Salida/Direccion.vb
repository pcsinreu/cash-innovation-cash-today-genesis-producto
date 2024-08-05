Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Direccion
        Public Property Pais As String
        Public Property ProvinciaEstado As String
        Public Property Ciudad As String
        Public Property Telefono As String
        Public Property NIF As String
        Public Property Email As String
        Public Property CodigoPostal As String
        Public Property Direccion1 As String
        Public Property Direccion2 As String
        Public Property DatosAdicionales As DatoAdicionalDireccion
    End Class
End Namespace