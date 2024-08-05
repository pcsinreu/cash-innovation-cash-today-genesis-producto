Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Genesis.Puesto.ObtenerPuestos

    <XmlType(Namespace:="urn:ObtenerPuestos")> _
    <XmlRoot(Namespace:="urn:ObtenerPuestos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty
        Public Property BolVigente As Boolean?
        Public Property Aplicacion As Enumeradores.Aplicacion

    End Class

End Namespace