Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.RecuperarTerminosIACRemesa

    <XmlType(Namespace:="urn:RecuperarTerminosIACRemesa")> _
    <XmlRoot(Namespace:="urn:RecuperarTerminosIACRemesa")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property IdentificadorRemesa As String

    End Class

End Namespace