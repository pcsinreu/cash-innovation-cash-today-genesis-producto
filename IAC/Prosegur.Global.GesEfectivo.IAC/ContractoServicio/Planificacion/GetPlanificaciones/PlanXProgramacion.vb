Namespace Planificacion.GetPlanificaciones
    <Serializable()> _
    Public Class PlanXProgramacion

        Public Property OidPlanificacion As String
        Public Property OidProgramacion As String
        Public Property OidTipoPlanProgramacion As String
        Public Property CodPlanificacion As String
        Public Property DesPlanificacion As String
        Public Property OidBanco As String
        Public Property CodBanco As String
        Public Property OidTipoPlanificacion As String
        Public Property DesTipoPlanificacion As String
        Public Property DesBanco As String
        Public Property FyhLunes As String
        Public Property FyhMartes As String
        Public Property FyhMiercoles As String
        Public Property FyhJueves As String
        Public Property FyhViernes As String
        Public Property FyhSabado As String
        Public Property FyhDomingo As String
        Public Property BolActivo As Boolean
        Public Property BolControlaFacturacion As Boolean
        Public NecDiaInicio As Integer

        Public NecDiaFin As Integer

        Public FechaHoraInicio As DateTime

        Public FechaHoraFin As DateTime

        Public FechaHoraVigenciaInicio As DateTime

        Public FechaHoraVigenciaFin As DateTime

        Public Property Habilitado As Boolean

    End Class
End Namespace
