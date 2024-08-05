Namespace Clases

    ''' <summary>
    ''' Classe que represanta as informações da entidade de inventario.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public Class Inventario
        Inherits BaseClase

        Public Property Identificador As String
        Public Property Sector As Sector
        Public Property Codigo As String
        Public Property Estado As String
        Public Property UsuarioCreacion As String
        Public Property FechaCreacion As DateTime

    End Class
End Namespace


