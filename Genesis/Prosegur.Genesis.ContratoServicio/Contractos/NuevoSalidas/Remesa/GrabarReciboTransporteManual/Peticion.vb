Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.NuevoSalidas.Remesa.GrabarReciboTransporteManual

    <XmlType(Namespace:="urn:GrabarReciboTransporteManual")> _
    <XmlRoot(Namespace:="urn:GrabarReciboTransporteManual")> _
    <Serializable()> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

#Region "[VARIÁVEIS]"

        Public Property remesas As List(Of Clases.Remesa)

#End Region

    End Class

End Namespace