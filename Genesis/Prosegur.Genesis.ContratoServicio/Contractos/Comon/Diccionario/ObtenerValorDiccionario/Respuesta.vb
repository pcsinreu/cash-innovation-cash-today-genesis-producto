Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Diccionario.ObtenerValorDiccionario

    <XmlType(Namespace:="urn:ObtenerValorDiccionario")> _
    <XmlRoot(Namespace:="urn:ObtenerValorDiccionario")> _
    <Serializable()>
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Valor As String

    End Class
End Namespace

