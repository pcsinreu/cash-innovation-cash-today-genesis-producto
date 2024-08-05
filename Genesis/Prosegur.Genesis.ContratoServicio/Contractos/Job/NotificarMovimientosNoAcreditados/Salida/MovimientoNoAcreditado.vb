
Imports System.Xml.Serialization

Namespace Contractos.Job.NotificarMovimientosNoAcreditados.Salida
    <Serializable()>
    Public Class MovimientoNoAcreditado
        Public Property CodTipoPlanificacion As String
        Public Property DesPlanificacion As String
        Public Property CodPlanificacionBanco As String
        Public Property DesPlanificacionBanco As String
        Public Property CodDeviceId As String
        Public Property CodCliente As String
        Public Property DesCliente As String
        Public Property CodPtoServicio As String
        Public Property DesPtoServicio As String
        Public Property HorMovimiento As String
        Public Property CodGrupoMovimiento As String
        Public Property CodMovimiento As String
        Public Property HorasTranscurridas As Integer

    End Class
End Namespace

