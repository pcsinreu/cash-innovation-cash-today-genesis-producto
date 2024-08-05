Namespace Contractos.Infraestructura.RecuperarDatosEntradaMovimientos.Salida
    Public Class Movimiento
        Public Property CodigoPais As String
        Public Property ActualID As String
        Public Property IdentificadorLlamada As String
        Public Property FechaHoraLlamadaInicio As DateTime
        Public Property FechaHoraLlamadaFin As DateTime

        Public Property Llamadas As List(Of Salida.Llamada)

    End Class
End Namespace
