Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.TiposMercancia.CalcularTipoMercancia

    <XmlType(Namespace:="urn:CalcularTipoMercancia")> _
       <XmlRoot(Namespace:="urn:CalcularTipoMercancia")> _
       <Serializable()> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodDelegacion As String
        Public Property OidRemesaLegado As String
        Public Property CodTipoMercanciaRemesa As List(Of String)

        Public Property TrabajaPorBulto As Boolean

    End Class

End Namespace