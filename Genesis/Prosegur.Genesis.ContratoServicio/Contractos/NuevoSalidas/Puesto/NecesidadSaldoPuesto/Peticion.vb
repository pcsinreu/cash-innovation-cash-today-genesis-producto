Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Puesto.NecesidadFondoPuesto

    <XmlType(Namespace:="urn:NecesidadFondoPuesto")> _
    <XmlRoot(Namespace:="urn:NecesidadFondoPuesto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty
        Public Property CodigoPuesto As String = String.Empty

        Public Property GestionaSaldoPuesto As Boolean = False

    End Class

End Namespace