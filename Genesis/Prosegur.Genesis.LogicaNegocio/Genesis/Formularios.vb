Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class Formularios

        Public Shared Function ObtenerPlanxMovimientos(oidPlanificacion As String) As List(Of Clases.Formulario)

            Dim resultado As List(Of Clases.Formulario) = New List(Of Clases.Formulario)

            If Not String.IsNullOrEmpty(oidPlanificacion) Then

                resultado = AccesoDatos.Genesis.Formularios.ObtenerPlanxMovimientos(oidPlanificacion)

            End If

            Return resultado

        End Function

    End Class
End Namespace