Namespace Clases.CentralNotificacion

    <Serializable>
    Public Class DestinoNotificacion
        Inherits BindableBase

        Public Property Identificador As String
        Public Property IdentificadorNotificacion As String
        Public Property IdentificadorUsuario As String
        Public Property IdentificadorSector As String
        Public Property IdentificadorTipoSectorPlanta As String
        Public Property IdentificadorPlanta As String
        Public Property IdentificadorDelegacion As String
        Public Property BolLida As Boolean
        Public Property FechaCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public ReadOnly Property FechaCreacionFormatada As String
            Get
                Return FechaCreacion.ToString()
            End Get
        End Property
        Public ReadOnly Property FechaModificacionFormatada As String
            Get
                Return FechaModificacion.ToString()
            End Get
        End Property

    End Class
End Namespace
