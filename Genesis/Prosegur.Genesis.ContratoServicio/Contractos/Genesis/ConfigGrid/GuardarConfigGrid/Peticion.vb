Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.Genesis.ConfigGrid.GuardarConfigGrid
    <XmlType(Namespace:="urn:GuardarConfigGrid")> _
    <XmlRoot(Namespace:="urn:GuardarConfigGrid")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String
        Public Property CodigoFuncionalidade As String
        Public Property ConfigGrid As ObservableCollection(Of Clases.ConfigGrid)

    End Class
End Namespace

