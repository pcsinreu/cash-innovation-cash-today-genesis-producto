Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Notificacion.RecibirMensajeExterno

    <XmlType(Namespace:="urn:RecibirMensajeExterno")> _
    <XmlRoot(Namespace:="urn:RecibirMensajeExterno")> _
    <Serializable()>
    Public NotInheritable Class Mensaje

        Public Property Mensaje As String
        Public Property TipoMensaje As String
        Public Property ColeccionCriterio As List(Of Criterio)

    End Class

End Namespace