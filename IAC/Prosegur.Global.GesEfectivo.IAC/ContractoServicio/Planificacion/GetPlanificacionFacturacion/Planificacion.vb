
Namespace Planificacion.GetPlanificacionFacturacion
    <Serializable()>
    Public Class Planificacion

        Public Property OidPlanificacion As String
        Public Property BancoTesoreriaDelegacion As Prosegur.Genesis.Comon.Clases.SubCliente
        Public Property CuentaTesoreriaDelegacion As Prosegur.Genesis.Comon.Clases.PuntoServicio
        Public Property PorcComisionPlanificacion As Nullable(Of Decimal)
        Public Property BolControlaFacturacion As Boolean

    End Class
End Namespace
