Imports System.Xml.Serialization

Namespace GenesisLogin.EjecutarLogin

    <Serializable()> _
    <XmlType(Namespace:="urn:EjecutarLogin")> _
    <XmlRoot(Namespace:="urn:EjecutarLogin")> _
    Public Class Peticion

        Public Property Login() As String

        Public Property Password() As String

        Public Property CodigoPais As String

    End Class

End Namespace