Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransaccionesFechas

    <XmlType(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <XmlRoot(Namespace:="urn:RecuperarTransaccionesFechas")> _
    <Serializable()>
    Public Class Transacion

        <XmlIgnore>
        Public Property Identificador As String
        Public Property CodigoExterno As String
        Public Property CodigoComprobante As String
        Public Property FechaGestion As DateTime
        Public Property FechaRealizacion As DateTime
        Public Property IdFormulario As String
        Public Property NombreFormulario As String
        Public Property Certificado As Boolean
        Public Property Notificado As Boolean
        Public Property Acreditado As Boolean
        Public Property FechaAcreditacion As String
        Public Property Disponible As Boolean
        Public Property CuentaOrigen As Cuenta
        Public Property CuentaDestino As Cuenta
        <XmlElement(Namespace:="urn:Comon", Type:=GetType(List(Of Comon.Divisa)))>
        Public Property Divisas As List(Of Comon.Divisa)
        Public Property MediosPago As List(Of MedioPago)
        Public Property CamposAdicionales As List(Of Comon.CampoAdicional)
        Public Property TimeZone As String

    End Class

End Namespace