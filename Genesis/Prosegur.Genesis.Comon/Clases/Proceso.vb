Imports System.Xml.Serialization

Namespace Clases
    <Serializable()>
    <XmlType(Namespace:="urn:Proceso")>
    <XmlRoot(Namespace:="urn:Proceso")>
    Public Class Proceso
        Public Property Identificador() As String
        Public Property Codigo() As String
        Public Property Descripcion() As String

    End Class

End Namespace