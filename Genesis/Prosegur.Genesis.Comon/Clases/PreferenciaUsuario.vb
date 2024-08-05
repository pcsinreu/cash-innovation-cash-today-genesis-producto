Namespace Clases
    ''' <summary>
    ''' Representa cada preferencia ajustada por el usuario.
    ''' </summary>
    Public Class PreferenciaUsuario
        ''' <summary>
        ''' Código del usuário.
        ''' </summary>
        Public Property CodigoUsuario As String

        ''' <summary>
        ''' Identificador de la preferencia del usuário.
        ''' </summary>
        Public Property OidPreferenciaUsuario As String

        ''' <summary>
        ''' Identificador de la aplicación.
        ''' </summary>
        Public Property CodigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion

        ''' <summary>
        ''' Código de la funcionalidad.
        ''' </summary>
        Public Property CodigoFuncionalidad As String

        ''' <summary>
        ''' Código del componente.
        ''' </summary>
        Public Property CodigoComponente As String

        ''' <summary>
        ''' Código de la propriedad.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CodigoPropriedad As String

        ''' <summary>
        ''' Valor de la preferência.
        ''' </summary>
        Public Property Valor As String

        ''' <summary>
        ''' Valor de la preferência (cuando es binário).
        ''' </summary>
        Public Property ValorBinario As Object

        ''' <summary>
        ''' Tipo de datos almacenados (cuando binario).
        ''' </summary>
        Public Property TipoValorBinario As String
    End Class
End Namespace