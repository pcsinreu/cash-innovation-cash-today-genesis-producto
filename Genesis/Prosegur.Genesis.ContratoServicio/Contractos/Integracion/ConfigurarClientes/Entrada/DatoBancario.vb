Imports System.Xml.Serialization
Imports System.ComponentModel

Namespace Contractos.Integracion.ConfigurarClientes.Entrada

    <XmlType(Namespace:="urn:ConfigurarClientes.Entrada")>
    <XmlRoot(Namespace:="urn:ConfigurarClientes.Entrada")>
    <Serializable()>
    Public Class DatoBancario

        <XmlAttributeAttribute()>
        <DefaultValue(0)>
        Public Property Accion As Comon.Enumeradores.AccionAB
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