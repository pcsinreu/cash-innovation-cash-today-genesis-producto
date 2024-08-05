Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Remesa.ActualizarRemesaBultos

    <XmlType(Namespace:="urn:ActualizarRemesaBultos")> _
    <XmlRoot(Namespace:="urn:ActualizarRemesaBultos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoUsuario As String

        Property Remesa As Clases.Remesa

    End Class

End Namespace