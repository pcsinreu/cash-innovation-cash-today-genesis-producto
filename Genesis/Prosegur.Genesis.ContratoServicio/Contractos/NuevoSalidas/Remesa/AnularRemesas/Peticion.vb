Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.AnularRemesas

    <XmlType(Namespace:="urn:AnularRemesas")> _
    <XmlRoot(Namespace:="urn:AnularRemesas")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property Remesas As ObservableCollection(Of Clases.Remesa)
        Public Property IdentificadoresRemesasSaldos As List(Of String)
        Public Property CodigoUsuario As String
        Public Property FechaHoraActualizacion As DateTime

    End Class

End Namespace