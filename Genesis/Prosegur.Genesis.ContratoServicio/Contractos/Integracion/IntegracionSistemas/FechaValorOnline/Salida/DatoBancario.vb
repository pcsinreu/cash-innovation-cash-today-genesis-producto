Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida

    <XmlType(Namespace:="urn:FechaValorOnline.Salida")>
    <XmlRoot(Namespace:="urn:FechaValorOnline.Salida")>
    <Serializable()>
    Public Class DatoBancario
        Public Property Banco As String
        Public Property DescripcionBanco As String
        Public Property NroDocumento As String
        Public Property Agencia As String
        Public Property NroCuenta As String
        Public Property Titularidad As String
        Public Property Divisa As String
        Public Property Tipo As String
        Public Property Observaciones As String
        Public Property DatosAdicionales As DatosAdicionales
    End Class
End Namespace