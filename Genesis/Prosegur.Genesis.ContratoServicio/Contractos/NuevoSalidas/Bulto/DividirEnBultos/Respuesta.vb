Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Collections.ObjectModel

Namespace Contractos.NuevoSalidas.Bulto.DividirEnBultos

    <XmlType(Namespace:="urn:DividirEnBultos")> _
    <XmlRoot(Namespace:="urn:DividirEnBultos")> _
    <Serializable()>
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

        Public Property Remesa As Clases.Remesa
    End Class

End Namespace
