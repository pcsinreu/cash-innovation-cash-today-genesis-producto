Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Delegacion.ObtenerDelegaciones

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegaciones")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegaciones")> _
    <DataContract()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)

    End Class

End Namespace

