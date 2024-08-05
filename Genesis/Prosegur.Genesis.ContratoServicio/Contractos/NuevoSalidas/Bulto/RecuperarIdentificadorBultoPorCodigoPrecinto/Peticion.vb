Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.NuevoSalidas.Bulto.RecuperarIDsBultosPorCodigosPrecintos

    <XmlType(Namespace:="urn:RecuperarIDsBultosPorCodigosPrecintos")> _
    <XmlRoot(Namespace:="urn:RecuperarIDsBultosPorCodigosPrecintos")> _
    <Serializable()>
    Public NotInheritable Class Peticion
        Inherits BasePeticion

        Public Property CodigosPrecintos As List(Of String)

    End Class

End Namespace