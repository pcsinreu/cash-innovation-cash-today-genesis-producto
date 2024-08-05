Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Reportes.GrabarRecepcionRuta

    <XmlType(Namespace:="urn:GrabarRecepcionRuta")> _
    <XmlRoot(Namespace:="urn:GrabarRecepcionRuta")> _
    <Serializable()>
    Public NotInheritable Class GrabarRecepcionRutaPeticion
        Inherits BasePeticion

        Public Property RecepcionesRuta As List(Of RecepcionRuta)

    End Class
End Namespace

