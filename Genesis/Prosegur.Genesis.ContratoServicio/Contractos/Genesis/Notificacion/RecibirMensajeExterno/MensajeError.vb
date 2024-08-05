Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.RecibirMensajeExterno

    <XmlType(Namespace:="urn:RecibirMensajeExterno")> _
    <XmlRoot(Namespace:="urn:RecibirMensajeExterno")> _
    <Serializable()>
    Public NotInheritable Class MensajeError

        Public Property TipoMensaje As String
        Public Property Identificador As String
        Public Property DescripcionError As String

    End Class

End Namespace