Imports System.Xml.Serialization

Namespace Login.CrearTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:CrearTokenAcceso")> _
    <XmlRoot(Namespace:="urn:CrearTokenAcceso")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property TokenAcceso As TokenAcceso

    End Class

End Namespace