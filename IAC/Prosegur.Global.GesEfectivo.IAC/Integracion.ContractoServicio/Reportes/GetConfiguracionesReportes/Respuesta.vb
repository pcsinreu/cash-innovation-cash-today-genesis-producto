Imports System.Xml.Serialization

Namespace Reportes.GetConfiguracionesReportes

    <XmlType(Namespace:="urn:GetConfiguracionesReportes")> _
    <XmlRoot(Namespace:="urn:GetConfiguracionesReportes")> _
    <Serializable()> _
    Public Class Respuesta
        Inherits RespuestaGenerico

#Region "[PROPRIEDADES]"

        Public Property ConfiguracionesReportes As ConfiguracionReporteColeccion

#End Region

    End Class

End Namespace