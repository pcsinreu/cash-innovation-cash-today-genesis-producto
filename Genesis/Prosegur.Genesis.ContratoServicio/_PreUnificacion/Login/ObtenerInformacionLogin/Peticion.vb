Imports System.Xml.Serialization

Namespace Login.ObtenerInformacionLogin

    <Serializable()>
    <XmlType(Namespace:="urn:ObtenerInformacionLogin")>
    <XmlRoot(Namespace:="urn:ObtenerInformacionLogin")>
    Public Class Peticion
        Public Property DesLogin() As String
        Public Property CodigoPais() As String
    End Class
End Namespace
