Imports System.Web
Imports System.Collections.Specialized

''' <summary>
''' Classe que encapsula as configurações da aplicação vindas do login
''' </summary>
''' <remarks></remarks>
Public Class Configuraciones

    ''' <summary>
    ''' Encapsula as configurações no contexto application da web
    ''' </summary>
    Private Shared Property _appSettings As NameValueCollection
        Get
            If HttpContext.Current.Application("_appSettings") Is Nothing Then
                HttpContext.Current.Application("_appSettings") = New NameValueCollection()
            End If
            Return HttpContext.Current.Application("_appSettings")
        End Get
        Set(value As NameValueCollection)
            HttpContext.Current.Application("_appSettings") = value
        End Set
    End Property

    ''' <summary>
    ''' Retorna as configurações de aplicação, definindo se deve ser retornado o contexto web ou desktop
    ''' </summary>
    Public Shared ReadOnly Property AppSettings As NameValueCollection

        Get
            ' executando pela web devemos retornar as configurações vindas do login
            If HttpContext.Current IsNot Nothing Then
                ' caso exista configuracoes
                If _appSettings.Count > 0 Then
                    Return _appSettings
                End If
            End If

            ' retorna as configurações default
            Return System.Configuration.ConfigurationManager.AppSettings

        End Get

    End Property

    ''' <summary>
    ''' Inicializa as configurações com valores iniciais vindos do login
    ''' </summary>
    Public Shared Sub AplicaConfiguraciones(codAplicacion As String, serializableDictionary As ContractoServicio.SerializableDictionary(Of String, String))

        ' não deve ser chamado pelo desktop
        If HttpContext.Current Is Nothing Then
            Return
        End If

        Dim prefixo = codAplicacion & "_"

        ' pega apenas as configurações da aplicação iniciadas pelo codigo da aplicação
        For Each key In serializableDictionary.Keys.Where(Function(k) k.StartsWith(prefixo))

            Dim realValueKey = key.Substring(prefixo.Length, key.Length - prefixo.Length)
            Dim newValue = serializableDictionary(key)

            ' se já existir alteramos ou adicionamos o valor
            _appSettings(realValueKey) = newValue

        Next

    End Sub

End Class