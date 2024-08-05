Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace NuevoSalidas.TipoBulto.ObtenerTiposBulto

    <XmlType(Namespace:="urn:ObtenerTiposBulto")> _
    <XmlRoot(Namespace:="urn:ObtenerTiposBulto")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty

    End Class

End Namespace