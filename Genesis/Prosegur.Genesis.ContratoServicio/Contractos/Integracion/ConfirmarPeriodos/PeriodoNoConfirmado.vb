Namespace Contractos.Integracion.ConfirmarPeriodos
    Public Class PeriodoNoConfirmado
        Public Sub New()
            ValoresAcreditados = New List(Of Valor)
        End Sub
        Public Property OidPeriodo As String
        Public Property DeviceId As String
        Public Property MaquinaDesc As String
        Public Property PeriodoIdentificador As String
        Public Property ValoresAcreditados As List(Of Valor)
        Public Property PeriodoCodigoMensaje As String
        Public Property DescripcionMensaje As String
        Public Property ReintentosDisponibles As Integer
        Public Property EstadoPeriodo As String
        Public Property TipoPeriodo As String
    End Class
End Namespace
