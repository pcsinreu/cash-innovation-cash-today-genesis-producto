Imports System.Xml.Serialization

Namespace Contractos.Integracion.RecuperarSaldosAcuerdo
    <XmlType(Namespace:="urn:RecuperarSaldosAcuerdo")>
    <XmlRoot(Namespace:="urn:RecuperarSaldosAcuerdo")>
    <Serializable()>
    Public Class Peticion
        Inherits Comon.BaseRequest
        Public Property CodigoPais As String
        Public Property Fecha As DateTime
        Public Property SourceReferenceId As String

    End Class
End Namespace
