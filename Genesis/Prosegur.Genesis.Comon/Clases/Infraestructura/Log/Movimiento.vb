Imports Prosegur.Genesis.Comon
Namespace Infraestructura.Log
    Public Class Movimiento
        Public Property CodigoPais As String
        Public Property ActualID As String
        Public Property FechaHoraPrimeraLlamada As DateTime
        Public Property FechaHoraUltimaLlamada As DateTime
        Public Property Llamadas As List(Of Llamada)
        Public Sub New()
            Llamadas = New List(Of Llamada)()
        End Sub
    End Class
End Namespace
