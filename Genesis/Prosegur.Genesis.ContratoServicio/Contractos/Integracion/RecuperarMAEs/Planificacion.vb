
Namespace Contractos.Integracion.RecuperarMAEs

    <Serializable()>
    Public Class Planificacion

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property FechaHoraVigenciaInicio As String
        Public Property FechaHoraVigenciaFin As String
        Public Property Vigente As Boolean
        Public Property MinutosAcreditacion As Integer
        Public Property Tipo As Comon.Entidad
        Public Property Banco As Comon.Entidad
    End Class

End Namespace
