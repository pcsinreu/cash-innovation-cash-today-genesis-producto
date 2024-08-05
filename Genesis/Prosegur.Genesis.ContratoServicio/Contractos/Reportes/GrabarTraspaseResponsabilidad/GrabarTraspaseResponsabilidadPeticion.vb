Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Reportes.GrabarTraspaseResponsabilidad

    <XmlType(Namespace:="urn:GrabarTraspaseResponsabilidad")> _
    <XmlRoot(Namespace:="urn:GrabarTraspaseResponsabilidad")> _
    <Serializable()>
    Public NotInheritable Class GrabarTraspaseResponsabilidadPeticion
        Inherits BasePeticion

        Public Property RecepcionesRuta As List(Of RecepcionRuta)
        Public Property GenerarPrecintoModuloAutomatico As Boolean

    End Class
End Namespace

