Namespace Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
    Public Class Canal
        Public Property CodCanal As String
        Public Property DesCanal As String
        Public Property SubCanales As List(Of SubCanal)
        Public Sub New()
            CodCanal = String.Empty
            DesCanal = String.Empty
            SubCanales = New List(Of SubCanal)
        End Sub

        Public Overrides Function ToString() As String
            Dim descripcionCanal As String = String.Format("{0} {1}", Me.CodCanal, Me.DesCanal)
            Dim descripcionSubCanal As Text.StringBuilder = New Text.StringBuilder()
            Dim descripcion As Text.StringBuilder = New Text.StringBuilder()

            For Each subcanal In SubCanales
                descripcionSubCanal = descripcionSubCanal.AppendLine(String.Format("{0} / {1} {2}", descripcionCanal, subcanal.CodSubCanal, subcanal.DesSubCanal))
            Next

            If descripcionSubCanal.ToString().Length > 0 Then
                descripcion.Append(descripcionSubCanal)
            Else
                descripcion.Append(descripcionCanal)
            End If

            Return descripcion.ToString()
        End Function
    End Class
End Namespace

