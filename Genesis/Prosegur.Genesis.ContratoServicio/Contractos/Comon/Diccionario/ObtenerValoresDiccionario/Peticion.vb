Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.Comon.Diccionario.ObtenerValoresDiccionario
    <XmlType(Namespace:="urn:ObtenerValoresDiccionario")> _
    <XmlRoot(Namespace:="urn:ObtenerValoresDiccionario")> _
    <Serializable()>
    Public Class Peticion
        Public Property Cultura As String
        Public Property CodigoFuncionalidad As String
    End Class
End Namespace

