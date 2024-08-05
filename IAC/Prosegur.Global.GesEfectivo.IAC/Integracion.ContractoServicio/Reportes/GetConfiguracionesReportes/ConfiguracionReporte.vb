Imports System.Xml.Serialization

Namespace Reportes.GetConfiguracionesReportes


    ''' <summary>
    ''' Classe ConfiguracionReporte
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 30/04/2013 - Criado
    ''' </history>
    <Serializable()> _
    Public Class ConfiguracionReporte

#Region "[PROPRIEDADES]"

        Public Property IdentificadorConfiguracion As String
        Public Property DesConfiguracion As String
        Public Property DesReporte As List(Of String)
        Public Property Delegaciones As List(Of ParametroDelegaciones)
        <XmlIgnore()>
        Public Property Marcada As String = "1"
#End Region

    End Class

End Namespace