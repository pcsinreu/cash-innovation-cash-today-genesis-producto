Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.RecibirMensajeExterno

    <XmlType(Namespace:="urn:RecibirMensajeExterno")> _
    <XmlRoot(Namespace:="urn:RecibirMensajeExterno")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Mensajes As List(Of Mensaje)
        Public Property Usuario As String

    End Class

End Namespace