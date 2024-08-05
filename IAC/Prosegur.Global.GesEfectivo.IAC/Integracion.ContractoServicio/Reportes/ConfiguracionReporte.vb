Namespace Reportes

    ''' <summary>
    ''' Classe ConfiguracionReporte
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/04/2013 - Criado
    ''' [victor.ramos] 17/07/2013 - Modificado
    ''' </history>
    <Serializable()> _
    Public Class ConfiguracionReporte

#Region "[PROPRIEDADES]"

        Public Property IdentificadorConfiguracion As String
        Public Property DesConfiguracion As String
        Public Property DesRuta As String
        Public Property CodUsuario As String
        Public Property FyhActualizacion As DateTime
        Public Property Reportes As ReportesColeccion
        Public Property ParametrosReporte As ParametroReporteColeccion
        Public Property NombreArchivo As String

#End Region

    End Class

End Namespace