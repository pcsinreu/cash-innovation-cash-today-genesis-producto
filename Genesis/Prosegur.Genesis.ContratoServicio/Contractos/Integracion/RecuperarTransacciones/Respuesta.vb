
Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarTransacciones

    <XmlType(Namespace:="urn:RecuperarTransacciones")>
    <XmlRoot(Namespace:="urn:RecuperarTransacciones")>
    <Serializable()>
    Public Class Respuesta
        Inherits Comon.BaseResponse

        Public Property Transacciones As List(Of Transaccion)
    End Class
End Namespace
