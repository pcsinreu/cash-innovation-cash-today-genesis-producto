Imports System.Xml.Serialization

Namespace Configuracion.General

    <Serializable()>
    Public Class ConfiguracionGeneral
        Public Property OIDConfiguracionGeneral As String
        Public Property DesReporte As String
        Public Property CodReporte As String
        Public Property FormatoArchivo As String
        Public Property ExtensionArchivo As String
        Public Property Separador As String
        Public Property CodUsuario As String
        Public Property FechaAtualizacion As DateTime

        <XmlIgnore()> _
        Public Property Path As String
    End Class

End Namespace
