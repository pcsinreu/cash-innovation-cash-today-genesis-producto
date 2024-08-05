Namespace Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
    Public Class Formulario
        Public Property CodFormulario As String
        Public Property DesFormulario As String
        Public Overrides Function ToString() As String
            Return String.Format("{0} - {1}", Me.CodFormulario, Me.DesFormulario)
        End Function
    End Class
End Namespace

