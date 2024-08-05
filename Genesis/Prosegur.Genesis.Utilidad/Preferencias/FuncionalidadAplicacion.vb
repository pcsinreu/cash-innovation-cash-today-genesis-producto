Imports System.Runtime.Caching

Namespace Preferencias
    ''' <summary>
    ''' Representa una funcionalidad de la aplicación y todos los elementos que han registrado las preferencias.
    ''' </summary>
    Public Class FuncionalidadAplicacion
        Public ReadOnly CodigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion
        Public ReadOnly CodigoUsuario As String
        Public ReadOnly CodigoPais As String

        ''' <summary>
        ''' Colección de propiedades de una funcionalidad.
        ''' </summary>
        Private _propriedades As New Dictionary(Of String, PropriedadFuncionalidad)

        ''' <summary>
        ''' Colección de componentes de una funcionalidad.
        ''' </summary>
        Private _componentes As New Dictionary(Of String, ComponenteFuncionalidad)

        ''' <summary>
        ''' Constructor de la clase <see cref="FuncionalidadAplicacion"/>.
        ''' </summary>
        ''' <param name="codigoAplicacion">Código de la aplicación</param>
        ''' <param name="codigoUsuario">Código del usuário</param>
        ''' <param name="codigoFuncionalidad">Código de la funcionalidad</param>
        ''' <remarks>Accesible sólo dentro del mismo assembly.</remarks>
        Friend Sub New(codigoPais As String, codigoAplicacion As Prosegur.Genesis.Comon.Enumeradores.CodigoAplicacion, codigoUsuario As String, codigoFuncionalidad As String)
            Me.CodigoAplicacion = codigoAplicacion
            Me.CodigoUsuario = codigoUsuario
            Me.Codigo = codigoFuncionalidad
            Me.CodigoPais = codigoPais
        End Sub

        ''' <summary>
        ''' Código de la funcionalidad.
        ''' </summary>
        Public Property Codigo() As String

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


        ''' <summary>
        ''' Realiza una búsqueda de un componente determinado.
        ''' </summary>
        ''' <param name="codigo">Código del componente</param>
        ''' <returns>Objecto del componente</returns>
        Public Function Componente(codigo As String) As ComponenteFuncionalidad
            Dim comp As ComponenteFuncionalidad

            If _componentes.ContainsKey(codigo) Then
                comp = _componentes(codigo)
            Else
                comp = New ComponenteFuncionalidad()
                _componentes.Add(codigo, comp)
            End If

            Return comp

        End Function

        ''' <summary>
        ''' Todos los componentes
        ''' </summary>
        ''' <returns>Colección de componentes</returns>
        Protected Friend Function Componente() As Dictionary(Of String, ComponenteFuncionalidad)
            Return _componentes
        End Function
    End Class
End Namespace