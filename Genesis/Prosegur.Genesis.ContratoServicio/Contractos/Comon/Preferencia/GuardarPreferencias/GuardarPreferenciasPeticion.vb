Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Clases
Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Comon.Preferencia.GuardarPreferencias
    <XmlType(Namespace:="urn:GuardarPreferencias")> _
    <XmlRoot(Namespace:="urn:GuardarPreferencias")> _
    <Serializable()>
    Public Class GuardarPreferenciasPeticion
        Inherits BasePeticion

        Public Property Preferencias As ObservableCollection(Of PreferenciaUsuario)

    End Class
End Namespace