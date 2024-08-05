Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarClientes.Salida

    <XmlType(Namespace:="urn:RecuperarClientes.Salida")>
    <XmlRoot(Namespace:="urn:RecuperarClientes.Salida")>
    <Serializable()>
    Public Class DatoBancario

        Public Property Identificador As String
        Public Property CodigoBanco As String
        Public Property CodigoAgencia As String
        Public Property NumeroCuenta As String
        Public Property Tipo As String
        Public Property NumeroDocumento As String
        Public Property Titularidad As String
        Public Property CodigoDivisa As String
        Public Property Observaciones As String
        Public Property Patron As String
        Public Property Vigente As Boolean
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