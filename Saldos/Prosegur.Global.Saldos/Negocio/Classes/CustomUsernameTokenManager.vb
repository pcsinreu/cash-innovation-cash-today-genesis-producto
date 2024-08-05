Imports System.Security.Permissions
Imports Microsoft.Web.Services3.Security.Tokens
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' CustomUsernameTokenManager
''' </summary>
Public Class CustomUsernameTokenManager
    Inherits UsernameTokenManager

    ''' <summary>
    ''' Sobrescreve o padrão do framewrok e retorna a senha correta do usuario informado para que o WSE possa fazer a validação.
    ''' </summary>
    ''' <param name="token"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function AuthenticateToken(token As UsernameToken) As String
        ' Garante que a mensagem SOAP contenha um UsernameToken.
        If token Is Nothing Then
            Throw New ArgumentNullException
        End If

        'Valida o usuario informado
        If token.Username = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UsuarioWSLogin") Then
            Return Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("PasswordWSLogin")
        Else
            Throw New Exception(Traduzir("gen_srv_msg_errocabecalho"))
        End If
    End Function
End Class
