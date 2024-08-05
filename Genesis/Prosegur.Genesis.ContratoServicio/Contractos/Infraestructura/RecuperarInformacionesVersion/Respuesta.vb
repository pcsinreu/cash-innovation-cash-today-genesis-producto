Namespace Contractos.Infraestructura.RecuperarInformacionesVersion
    Public Class Respuesta
        Public Sub New()
            Resultado = New Salida.Resultado()
            Assemblies = New List(Of Prosegur.Genesis.Comon.Clases.Assembly)()
        End Sub

        Public Property Resultado() As Salida.Resultado
        Public Property Assemblies As List(Of Prosegur.Genesis.Comon.Clases.Assembly)
    End Class
End Namespace

