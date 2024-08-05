Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.Remesa.ObtenerSituacionRemesas

    <XmlType(Namespace:="urn:ObtenerSituacionRemesas")> _
    <XmlRoot(Namespace:="urn:ObtenerSituacionRemesas")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty

    End Class

End Namespace