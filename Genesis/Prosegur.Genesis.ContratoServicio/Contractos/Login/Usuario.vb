Namespace Entidades.Login
    Public Class Usuario
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

        Public Property DesLogin As String
        Public Property DesNombre As String
        Public Property DesApellido As String
        Public Property DesIdiomaPreferido As String
        Public Property DesUsuarioModificacion As String
        Public Property DesUsuarioCreacion As String
        Public Property Roles As List(Of Rol)
        Public Sub New()
            Me.Roles = New List(Of Rol)()
        End Sub

        Public Sub New(pIdentificador As String, pDesLogin As String, pDesNombre As String, pDesApellido As String, pDesIdiomaPreferido As String, pDesUsuarioCreador As String, pDesUsuarioModificacion As String)
            Me.New()
            Me.Identificador = pIdentificador
            Me.DesLogin = pDesLogin
            Me.DesNombre = pDesNombre
            Me.DesApellido = pDesApellido
            Me.DesIdiomaPreferido = pDesIdiomaPreferido
            Me.DesUsuarioCreacion = pDesUsuarioCreador
            Me.DesUsuarioModificacion = pDesUsuarioModificacion
        End Sub

    End Class
End Namespace

