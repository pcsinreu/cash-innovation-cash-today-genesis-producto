Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.DesBloquearRemesasBultos

    <XmlType(Namespace:="urn:DesBloquearRemesasBultos")> _
    <XmlRoot(Namespace:="urn:DesBloquearRemesasBultos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)
        Public Property Bultos As ObservableCollection(Of Clases.Bulto)
        Public Property CodigoUsuarioBloqueio As String
    End Class

End Namespace