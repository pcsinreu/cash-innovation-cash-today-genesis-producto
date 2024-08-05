Imports System.Xml.Serialization
Imports System.Runtime.Serialization
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel

Namespace Contractos.Comon.Delegacion.ObtenerDelegacionesDelUsuario

    <Serializable()> _
    <XmlType(Namespace:="urn:ObtenerDelegacionesDelUsuario")> _
    <XmlRoot(Namespace:="urn:ObtenerDelegacionesDelUsuario")> _
    <DataContract()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

        Public Property Delegaciones As ObservableCollection(Of Clases.Delegacion)

    End Class

End Namespace

