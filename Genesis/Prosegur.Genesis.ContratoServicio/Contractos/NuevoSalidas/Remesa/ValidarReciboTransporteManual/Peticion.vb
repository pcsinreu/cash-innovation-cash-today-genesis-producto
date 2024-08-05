Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual

    <XmlType(Namespace:="urn:ValidarReciboTransporteManual")> _
    <XmlRoot(Namespace:="urn:ValidarReciboTransporteManual")> _
    <Serializable()> _
    Public NotInheritable Class Peticion
        Inherits BasePeticion

#Region "[VARIÁVEIS]"

        Public Property Remesas As List(Of Clases.Remesa)

#End Region

    End Class

End Namespace