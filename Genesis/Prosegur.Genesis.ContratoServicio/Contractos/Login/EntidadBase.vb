Namespace Entidades.Login
    Public Class EntidadBase
        Implements IPersistible

        Private _identificador As String

        Public Property Identificador As String Implements IPersistible.Identificador
            Get
                Return _identificador
            End Get
            Set(value As String)
                _identificador = value
            End Set
        End Property

        Public Property Codigo As String
        Public Property Descripcion As String
        Public Property FechaCreacion As Date?
        Public Property FechaModificacion As Date?
        Public Property UsuarioCreacion As String
        Public Property UsuarioModificacion As String

    End Class
End Namespace

