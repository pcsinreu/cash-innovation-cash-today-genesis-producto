Imports Prosegur.Genesis.Comon
Namespace Contractos.Permisos
    Public Class PeticionGrabarUsuario

        Public Property Usuarios As List(Of RespuestaRecuperarUsuario)
        Public Property Accion As String


#Region "Datos usuario grabación"
        Public Property CodigoUsuario As String
        Public Property CodigoCultura As String
#End Region

        Public Sub New()
            Usuarios = New List(Of RespuestaRecuperarUsuario)
        End Sub
    End Class
End Namespace

