Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization

Namespace Contractos.Integracion.GrabarReciboTransporteManual

    <XmlType(Namespace:="urn:GrabarReciboTransporteManual")>
    <XmlRoot(Namespace:="urn:GrabarReciboTransporteManual")>
    <Serializable()>
    Public Class Peticion
        Inherits BasePeticion

        Public Property Remesas As List(Of Clases.Remesa)

    End Class

End Namespace
