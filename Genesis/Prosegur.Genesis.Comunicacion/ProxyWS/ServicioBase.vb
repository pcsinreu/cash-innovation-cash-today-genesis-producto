Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Web
Imports System.Net

Namespace ProxyWS

    Public Class ServicioBase
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol

        Protected Sub New()
            MyBase.New()
        End Sub

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

        Public ReadOnly Property UrlServicio() As String
            Get
                If AppSettings IsNot Nothing Then
                    Return AppSettings("UrlServicio")
                Else
                    Return ConfigurationManager.AppSettings("UrlServicio")
                End If
            End Get
        End Property

        Public ReadOnly Property Ws_TimeOut() As String
            Get
                If AppSettings IsNot Nothing Then
                    Return AppSettings("WS_TIMEOUT")
                Else
                    Return ConfigurationManager.AppSettings("WS_TIMEOUT")
                End If
            End Get
        End Property

        ''' <summary>
        ''' Overide method to enter the AcceptLanguage header to the HTTP header
        ''' </summary>
        ''' <param name="uri">Current uri</param>
        ''' <returns>The WebRequest created</returns>
        ''' <remarks></remarks>
        Protected Overloads Overrides Function GetWebRequest(uri As Uri) As WebRequest
            Dim wr As HttpWebRequest = TryCast(MyBase.GetWebRequest(uri), HttpWebRequest)
            If System.Web.HttpContext.Current IsNot Nothing AndAlso System.Web.HttpContext.Current.Request.UserLanguages IsNot Nothing Then
                wr.Headers.Add(System.Net.HttpRequestHeader.AcceptLanguage, String.Join(", ", System.Web.HttpContext.Current.Request.UserLanguages()))
            Else
                wr.Headers.Add(System.Net.HttpRequestHeader.AcceptLanguage, System.Globalization.CultureInfo.CurrentCulture.Name)
            End If
            Return (wr)
        End Function

    End Class

End Namespace