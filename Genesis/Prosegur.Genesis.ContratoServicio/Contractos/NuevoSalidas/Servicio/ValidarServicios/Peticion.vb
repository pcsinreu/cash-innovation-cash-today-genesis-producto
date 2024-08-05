Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Servicio.ValidarServicios

    <XmlType(Namespace:="urn:ValidarServicios")> _
    <XmlRoot(Namespace:="urn:ValidarServicios")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty
        Public Property CodigoUsuario As String = String.Empty
        Public Property Servicios As ObservableCollection(Of Clases.Elemento)

    End Class

End Namespace