Imports System.Xml.Serialization

Namespace Login.ConsumirTokenAcceso

    <Serializable()> _
    <XmlType(Namespace:="urn:ConsumirTokenAcceso")> _
    <XmlRoot(Namespace:="urn:ConsumirTokenAcceso")> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Permisos As CrearTokenAcceso.Permisos
        Public Property Configuraciones As SerializableDictionary(Of String, String)
        Public Property TokenValida As Boolean = False
        
    End Class

End Namespace