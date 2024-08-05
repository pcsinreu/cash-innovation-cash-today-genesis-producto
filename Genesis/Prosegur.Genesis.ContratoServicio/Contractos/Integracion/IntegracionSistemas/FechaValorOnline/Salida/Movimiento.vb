Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class Movimiento
        Public Property Canal As Canal
        Public Property Codigo As String
        Public Property CollectionID As String
        Public Property Notificado As String
        Public Property Acreditado As String
        Public Property Disponible As String
        Public Property FechaGestion As DateTimeOffset
        Public Property FechaRealizacion As DateTimeOffset
        Public Property FechaContable As DateTimeOffset?
        Public Property Formulario As Formulario
        Public Property Valores As List(Of Divisa)
        Public Property CamposAdicionales As List(Of CampoAdicional)
    End Class
End Namespace