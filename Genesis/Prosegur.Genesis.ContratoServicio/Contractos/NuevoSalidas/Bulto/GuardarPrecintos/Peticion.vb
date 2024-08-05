Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.GuardarPrecintos

    <XmlType(Namespace:="urn:GuardarPrecintos")> _
    <XmlRoot(Namespace:="urn:GuardarPrecintos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Property Bultos As ObservableCollection(Of Clases.Bulto)
        Property EsPreparar As Boolean
        Property CodigoUsuario As String

    End Class

End Namespace