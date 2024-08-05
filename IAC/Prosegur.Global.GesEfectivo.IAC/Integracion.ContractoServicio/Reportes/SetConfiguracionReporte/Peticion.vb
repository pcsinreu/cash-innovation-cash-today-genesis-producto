Imports System.Xml.Serialization
Imports System.Xml

Namespace Reportes.SetConfiguracionReporte

    <XmlType(Namespace:="urn:SetConfiguracionReporte")> _
    <XmlRoot(Namespace:="urn:SetConfiguracionReporte")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property EsExclusion As Boolean
        Public Property ConfiguracionesReportes As ConfiguracionReporteColeccion

#End Region

    End Class

End Namespace