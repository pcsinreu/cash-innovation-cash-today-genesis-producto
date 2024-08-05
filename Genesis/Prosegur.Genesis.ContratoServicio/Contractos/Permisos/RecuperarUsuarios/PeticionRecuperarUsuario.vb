
Imports Prosegur.Genesis.Comon
Namespace Contractos.Permisos
    Public Class PeticionRecuperarUsuario
        Public Property DesLogin As String
        Public Property CodigoPais As String
        Public Property CodigoRole As String
        Public Sub New()
            DesLogin = String.Empty
            CodigoPais = String.Empty
            CodigoRole = String.Empty
        End Sub
        Public Sub New(pDesLogin As String, pCodigoPais As String, pCodigoRole As String)
            DesLogin = pDesLogin
            CodigoPais = pCodigoPais
            CodigoRole = pCodigoRole
        End Sub
    End Class
End Namespace

