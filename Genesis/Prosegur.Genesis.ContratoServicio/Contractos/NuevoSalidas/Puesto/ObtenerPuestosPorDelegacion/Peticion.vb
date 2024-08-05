Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Puesto.ObtenerPuestosPorDelegacion

    <XmlType(Namespace:="urn:ObtenerPuestosPorDelegacion")> _
    <XmlRoot(Namespace:="urn:ObtenerPuestosPorDelegacion")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigoDelegacion As String = String.Empty

        Public Property CodigoPlanta As String = String.Empty

        Public Property CodigoSectorPadre As String = String.Empty

    End Class

End Namespace