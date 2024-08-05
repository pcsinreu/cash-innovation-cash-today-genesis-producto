Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.ActualizarRemesasBultosFusion

    <XmlType(Namespace:="urn:ActualizarRemesasBultosFusion")> _
    <XmlRoot(Namespace:="urn:ActualizarRemesasBultosFusion")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String

        Property Remesas As ObservableCollection(Of Clases.Remesa)

    End Class

End Namespace