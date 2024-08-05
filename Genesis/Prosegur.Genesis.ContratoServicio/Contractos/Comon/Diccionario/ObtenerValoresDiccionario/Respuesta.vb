Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Diccionario.ObtenerValoresDiccionario

    <XmlType(Namespace:="urn:ObtenerValoresDiccionario")> _
    <XmlRoot(Namespace:="urn:ObtenerValoresDiccionario")> _
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Valores As Prosegur.Genesis.Comon.SerializableDictionary(Of String, String)

    End Class
End Namespace

