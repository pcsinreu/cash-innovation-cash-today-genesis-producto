Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Enumeradores.Salidas

Namespace Contractos.NuevoSalidas.Remesa.CerrarPreparacion

    <XmlType(Namespace:="urn:CerrarPreparacion")> _
    <XmlRoot(Namespace:="urn:CerrarPreparacion")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion
        Public Property Remesa As Clases.Remesa
        Public Property CodigoUsuario As String

    End Class

End Namespace