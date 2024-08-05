Namespace Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
    Public Class MovimientoActualID
        Public Property ID As Integer
        Public Property ActualID As String
        Public Property TipoError As String
        Public Property CodProceso As String
        Public Property Reintentos As String
        Public Property CodEstado As String
        Public Property Cliente As Cliente
        Public Property SubCliente As SubCliente
        Public Property PuntoServicio As PuntoServicio
        Public Property Maquina As Maquina
        Public Property Documentos As List(Of Documento)
        Public Property DetallesIntegracion As List(Of DetalleIntegracion)
        Public Property Seleccionada As Boolean
        Public Sub New()

            Me.ActualID = String.Empty
            Me.TipoError = String.Empty
            Me.CodProceso = String.Empty
            Me.Reintentos = String.Empty 'Es string porque devuelve 2 / 5 (quiere decir, intento dos veces de un máximo de 5)
            Me.CodEstado = String.Empty

            Seleccionada = False

            Cliente = New Cliente()
            SubCliente = New SubCliente()
            PuntoServicio = New PuntoServicio()
            Maquina = New Maquina()
            Documentos = New List(Of Documento)
            DetallesIntegracion = New List(Of DetalleIntegracion)

        End Sub
    End Class
End Namespace


