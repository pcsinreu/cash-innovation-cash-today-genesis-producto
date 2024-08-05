Imports System.DirectoryServices
Imports Prosegur.Framework.Dicionario
Imports System.Configuration

Public Class UtilAD
    Public Shared Function VerificarLoginAD(ByVal login As String, ByVal password As String) As String
        Dim retorno As String = String.Empty
        Dim imacLoginUrl As String = ConfigurationManager.AppSettings("URLServicioAuthAD")
        Dim user As String = ConfigurationManager.AppSettings("UserWSAuthAD")
        Dim pass As String = ConfigurationManager.AppSettings("PassWSAuthAD")
        Dim servidorAD As String = ConfigurationManager.AppSettings("ServidorAD")
        Dim concatenarDominioUsuario As Boolean = If(String.IsNullOrEmpty(ConfigurationManager.AppSettings("ConcatenarDominioUsuario")), False, CBool(ConfigurationManager.AppSettings("ConcatenarDominioUsuario")))

        'TODO: cambiar el dominio Configuracoes.Parametros.Dominio
        Dim dominio As String = "latam1"
        Using objDirEnt As New DirectoryEntry(String.Format("LDAP://{0}", servidorAD), If(concatenarDominioUsuario, String.Format("{0}\{1}", dominio, login), login), password)
            Using search As DirectorySearcher = New DirectorySearcher(objDirEnt)
                search.Filter = "(&(objectClass=user)(sAMAccountName=" & login & "))" ' Identificador unico dos objetos no AD
                search.PropertiesToLoad.Add("cn") 'Comon Name
                search.SearchScope = SearchScope.Subtree
                Dim result As SearchResult = search.FindOne()
                If result Is Nothing Then
                    Throw New Exception()
                Else
                    Return result.GetDirectoryEntry.Guid.ToString()
                End If
            End Using
        End Using
    End Function

    Public Shared Sub VerificarLoginImac(login As String, password As String)
        Dim imacLoginUrl As String = ConfigurationManager.AppSettings("URLServicioAuthAD")
        Dim user As String = ConfigurationManager.AppSettings("UserWSAuthAD")
        Dim pass As String = ConfigurationManager.AppSettings("PassWSAuthAD")
        Dim dominio As String = ConfigurationManager.AppSettings("DefaultDomainWSAuthAD")

        'Se preencheu dominio então separa nas variáveis
        If login.Contains("\") Then
            dominio = login.Split("\")(0)
            login = login.Split("\")(1)
        End If

        'Instancia o objeto passando a URL, usuario e senha do serviço
        Dim wsLogin As New ImacLogin.Authentication(imacLoginUrl, user, pass)
        'Recupera domínios aceitos para login
        Dim dominiosAceitos As String() = wsLogin.getDomains()
        'Verifica se dominio recebido está na lista de domínios aceitos
        dominio = dominiosAceitos.FirstOrDefault(Function(a) a.ToUpper().Contains(dominio.ToUpper()))
        If String.IsNullOrEmpty(dominio) Then
            Throw New Exception("Dominio não encontrado")
        End If

        'Faz o login, se não conseguir retorna um erro
        Dim response = wsLogin.Login(dominio, login, password)
    End Sub

End Class
