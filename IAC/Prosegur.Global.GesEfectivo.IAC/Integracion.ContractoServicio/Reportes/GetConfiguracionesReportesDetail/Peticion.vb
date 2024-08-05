Imports System.Xml.Serialization
Imports System.Xml

Namespace Reportes.GetConfiguracionesReportesDetail

    <XmlType(Namespace:="urn:GetConfiguracionesReportesDetail")> _
    <XmlRoot(Namespace:="urn:GetConfiguracionesReportesDetail")> _
    <Serializable()> _
    Public Class Peticion

#Region "[PROPRIEDADES]"

        Public Property IdentificadoresConfiguracion As List(Of String)

#End Region

    End Class

End Namespace