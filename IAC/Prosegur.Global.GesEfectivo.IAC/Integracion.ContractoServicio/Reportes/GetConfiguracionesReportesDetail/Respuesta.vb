Imports System.Xml.Serialization
Imports System.Xml

Namespace Reportes.GetConfiguracionesReportesDetail

    <XmlType(Namespace:="urn:GetConfiguracionesReportesDetail")> _
    <XmlRoot(Namespace:="urn:GetConfiguracionesReportesDetail")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property ConfiguracionesReportes As ConfiguracionReporteColeccion

#End Region

    End Class

End Namespace