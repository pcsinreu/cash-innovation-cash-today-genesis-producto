Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Puesto.ActualizarSaldoPuesto

    <XmlType(Namespace:="urn:ActualizarSaldoPuesto")> _
    <XmlRoot(Namespace:="urn:ActualizarSaldoPuesto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String
        Public Property CodigoPuesto As String
        Public Property Divisas As ObservableCollection(Of Clases.Divisa)

    End Class

End Namespace