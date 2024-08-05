Imports System.Xml.Serialization

Namespace Login.InserirSesion

    <Serializable()> _
    <XmlType(Namespace:="urn:InserirSesion")> _
    <XmlRoot(Namespace:="urn:InserirSesion")> _
    Public Class Peticion
        Public Property IdentificadorUsuario As String
        Public Property CodigoAplicacion As String
    End Class

End Namespace
