Namespace Preferencias
    ''' <summary>
    ''' Representa el valor de preferencia para un componente.
    ''' </summary>
    Public Class ComponenteFuncionalidad

        ''' <summary>
        ''' Colección de propiedades de una funcionalidad.
        ''' </summary>
        Private _propriedades As New Dictionary(Of String, PropriedadFuncionalidad)

        ''' <summary>
        ''' Constructor de la clase <see cref="ComponenteFuncionalidad"/>.
        ''' </summary>
        ''' <remarks>Accesible sólo dentro del mismo assembly.</remarks>
        Friend Sub New()

        End Sub

        ''' <summary>
        ''' Realiza una búsqueda de una propriedad determinada.
        ''' </summary>
        ''' <param name="codigo">Código de la propriedad</param>
        ''' <returns>Objecto de la propriedad</returns>
        Public Function Propriedad(codigo As String) As PropriedadFuncionalidad
            Dim prop As PropriedadFuncionalidad

            If _propriedades.ContainsKey(codigo) Then
                prop = _propriedades(codigo)
            Else
                prop = New PropriedadFuncionalidad()
                _propriedades.Add(codigo, prop)
            End If

            Return prop

        End Function

        ''' <summary>
        ''' Todas las propriedades
        ''' </summary>
        ''' <returns>Colección de propriedades</returns>
        Protected Friend Function Propriedad() As Dictionary(Of String, PropriedadFuncionalidad)
            Return _propriedades
        End Function
    End Class
End Namespace