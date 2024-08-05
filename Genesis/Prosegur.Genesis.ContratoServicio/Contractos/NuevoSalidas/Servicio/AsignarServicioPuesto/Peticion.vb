Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Servicio.AsignarServicioPuesto

    <XmlType(Namespace:="urn:AsignarServicioPuesto")> _
    <XmlRoot(Namespace:="urn:AsignarServicioPuesto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty

        Public Property Servicios As ObservableCollection(Of Clases.Remesa)

    End Class

End Namespace