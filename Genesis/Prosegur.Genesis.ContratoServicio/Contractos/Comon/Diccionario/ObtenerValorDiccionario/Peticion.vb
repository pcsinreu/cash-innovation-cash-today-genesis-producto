Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Diccionario.ObtenerValorDiccionario
    <XmlType(Namespace:="urn:ObtenerValorDiccionario")> _
    <XmlRoot(Namespace:="urn:ObtenerValorDiccionario")> _
    <Serializable()>
    Public Class Peticion
        Public Property Cultura As String
        Public Property CodigoFuncionalidad As String
        Public Property CodigoExpresion As String
    End Class
End Namespace

