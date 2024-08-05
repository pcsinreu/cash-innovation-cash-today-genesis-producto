Namespace Preferencias
    ''' <summary>
    ''' Representa los valores binarios almacenados en la base de datos.
    ''' </summary>
    Public Class Binario
        ''' <summary>
        ''' Valor binario
        ''' </summary>
        Property Valor As Object

        ''' <summary>
        ''' Indica el tipo de información en el objeto binario.
        ''' </summary>
        Property Tipo As String

        ''' <summary>
        ''' Constructor de la clase <see cref="Binario"/>.
        ''' </summary>
        ''' <remarks>Accesible sólo dentro del mismo assembly.</remarks>
        Friend Sub New()

        End Sub

    End Class
End Namespace