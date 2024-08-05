Imports Prosegur.Genesis.Comon
Imports System.Xml.Serialization
Imports System.Xml

Namespace Contractos.NuevoSalidas.Remesa.ValidarReciboTransporteManual

    <XmlType(Namespace:="urn:ValidarReciboTransporteManual")> _
    <XmlRoot(Namespace:="urn:ValidarReciboTransporteManual")> _
    <Serializable()> _
    Public NotInheritable Class Respuesta
        Inherits BaseRespuesta

#Region "[PROPRIEDADES]"

        Public Property RecibosRepetidos As List(Of String)

#End Region

    End Class

End Namespace