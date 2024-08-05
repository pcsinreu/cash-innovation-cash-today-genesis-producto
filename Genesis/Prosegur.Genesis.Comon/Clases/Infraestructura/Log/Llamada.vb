Namespace Infraestructura.Log
    Public Class Llamada
        Public Property Identificador As String
        Public Property Origen As String
        Public Property Version As String
        Public Property DatosEntrada As String
        Public Property DatosSalida As String
        Public Property FechaHoraInicio As DateTime
        Public Property FechaHoraFin As DateTime
        Public Property DescripcionResultado As String
        Public Property Detalles As List(Of DetalleLlamada)
        Public Sub New()
            Detalles = New List(Of DetalleLlamada)()
        End Sub
    End Class
End Namespace
