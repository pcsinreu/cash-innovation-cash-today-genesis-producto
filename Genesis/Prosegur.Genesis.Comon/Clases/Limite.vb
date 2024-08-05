Imports System.Xml.Serialization

Namespace Clases
    <Serializable()>
    <XmlType(Namespace:="urn:Limite")>
    <XmlRoot(Namespace:="urn:Limite")>
    Public Class Limite
        Inherits BindableBase
        Public Property Accion As String
        Public Identificador As String
        Public Property Divisa As Divisa
        Public Property NumLimite As Decimal


    End Class

End Namespace