Imports Prosegur.Genesis.Comon

Namespace Contractos.Integracion.RecuperarPeriodos
    Public Class Respuesta
        Public Property PeriodosAcreditacion As List(Of Clases.PeriodoAcreditacionGrilla)

        Public Sub New()
            PeriodosAcreditacion = New List(Of Clases.PeriodoAcreditacionGrilla)()
        End Sub
    End Class
End Namespace