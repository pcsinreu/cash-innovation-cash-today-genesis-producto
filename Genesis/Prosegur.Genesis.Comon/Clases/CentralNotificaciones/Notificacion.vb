Namespace Clases.CentralNotificacion

    <Serializable>
    Public Class Notificacion
        Inherits BindableBase

        Public Property Identificador As String
        Public Property TipoNotificacion As TipoNotificacion
        Public Property ObservacionNotificacion As String
        Public Property ObservacionParametros As String
        Public Property BolActivo As Boolean
        Public Property FechaCreacion As DateTime
        Public Property UsuarioCreacion As String
        Public Property FechaModificacion As DateTime
        Public Property UsuarioModificacion As String
        Public Property DestinosNotificacion As List(Of DestinoNotificacion)

        Public ReadOnly Property LidoPor As DestinoNotificacion
            Get
                If Me.DestinosNotificacion IsNot Nothing Then
                    Dim leidoPor = Me.DestinosNotificacion.Where(Function(a) a.BolLida = True).OrderBy(Function(b) b.FechaModificacion)
                    If leidoPor.Count > 0 Then
                        Return leidoPor(0)
                    End If
                End If

                Return Nothing
            End Get
        End Property
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

        Public ReadOnly Property Privada As Boolean
            Get
                If Me.DestinosNotificacion IsNot Nothing Then
                    Return Me.DestinosNotificacion.Any(Function(a) Not String.IsNullOrEmpty(a.IdentificadorUsuario))
                End If

                Return False
            End Get
        End Property

        Public ReadOnly Property Lida As Boolean
            Get
                If Me.DestinosNotificacion IsNot Nothing Then
                    Return (Me.DestinosNotificacion.Where(Function(a) a.BolLida).Count > 0)
                End If

                Return False
            End Get
        End Property

    End Class
End Namespace