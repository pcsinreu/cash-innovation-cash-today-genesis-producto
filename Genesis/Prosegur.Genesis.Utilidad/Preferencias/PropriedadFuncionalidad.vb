Namespace Preferencias
    ''' <summary>
    ''' Representa el valor de preferencia para una propriedad.
    ''' </summary>
    Public Class PropriedadFuncionalidad

        ''' <summary>
        ''' Constructor de la clase <see cref="PropriedadFuncionalidad"/>.
        ''' </summary>
        ''' <remarks>Accesible sólo dentro del mismo assembly.</remarks>
        Friend Sub New()
            Me.Binario = New Binario
        End Sub
        Public Property Valor As String

        ''' <summary>
        ''' Representa los valores binarios almacenados en la base de datos.
        ''' </summary>
        Public Property Binario() As Binario

    End Class
End Namespace