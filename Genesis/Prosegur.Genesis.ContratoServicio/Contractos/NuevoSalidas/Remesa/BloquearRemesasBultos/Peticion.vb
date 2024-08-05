Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.BloquearRemesasBultos

    <XmlType(Namespace:="urn:BloquearRemesasBultos")> _
    <XmlRoot(Namespace:="urn:BloquearRemesasBultos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)
        Public Property Bultos As ObservableCollection(Of Clases.Bulto)
        Public Property CodigoUsuarioBloqueio As String
    End Class

End Namespace