Namespace Contractos.Infraestructura.RecuperarDatosEntradaMovimientos.Salida
    Public Class Llamada
        Public Property Identificador As String
        Public Property Origen As String
        Public Property Version As String
        Public Property DatosEntrada As String
        Public Property DatosSalida As String
        Public Property FechaHoraInicio As DateTime
        Public Property FechaHoraFin As DateTime
        Public Property CodigoResultado As String
        Public Property DescripcionResultado As String

        Public Property Detalles As List(Of Salida.DetalleLlamada)

    End Class
End Namespace
