Imports System.Net.Http
Imports System.Web
Imports System.IO
Imports System.Threading.Tasks
Imports Prosegur.Genesis.Excepcion
Imports System.Text
Imports Newtonsoft.Json


Namespace ProxyWS.WebApi
    Public Class HttpUtil
        Public Sub New()
        End Sub

        Public Sub New(codPais As String, codRecurso As String)
            _codPais = codPais
            _codRecurso = codRecurso
        End Sub

        Private Shared _UrlBaseApi As String

        Private _codPais As String
        Private _codRecurso As String

        Private Const UserAgent As String = "User-Agent"
        Private Const UserAgentValue As String = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36"
        Public Shared ReadOnly Property UrlBaseApi As String
            Get

                If String.IsNullOrEmpty(_UrlBaseApi) Then
                    _UrlBaseApi = "" 'ParametrosManager.Instancia.RecuperarParametroValor(Of String)(Codigos.URL_Api_Automatizador, Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoPais)
                End If

                Return _UrlBaseApi
            End Get
        End Property

        Public Function Post(Of T)(identificadorLlamada As String, ByVal urlApi As String, ByVal parametros As Object) As RespuestaHttp(Of T)
            Return Post(Of T)(identificadorLlamada, urlApi, parametros, Nothing)
        End Function

        Public Function Post(Of T)(identificadorLlamada As String, ByVal urlApi As String, ByVal parametrosUri As Dictionary(Of String, String)) As RespuestaHttp(Of T)
            Return Post(Of T)(identificadorLlamada, urlApi, Nothing, parametrosUri)
        End Function

        Public Function PostWithHeaders(Of T)(identificadorLlamada As String, ByVal urlApi As String, ByVal parametros As Object, ByVal headers As Dictionary(Of String, String)) As RespuestaHttp(Of T)
            Dim _urlApi As String = Path.Combine(UrlBaseApi, urlApi)
            Dim builder = New UriBuilder(_urlApi)
            Dim identificadorLlamadaInterno = String.Empty

            If _urlApi.Split(":"c).Length > 2 Then
                Dim porta As String = _urlApi.Split(":"c)(2)
                porta = porta.Substring(0, porta.IndexOf("/"c))
                builder.Port = Int32.Parse(porta)
            End If

            Dim query = HttpUtility.ParseQueryString(builder.Query)

            Try
                Using httpClient = New HttpClient()
                    httpClient.DefaultRequestHeaders.Add(UserAgent, UserAgentValue)
                    For Each header In headers
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value)
                    Next
                    Dim objJson = JsonConvert.SerializeObject(parametros)

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(_codPais, _codRecurso, identificadorLlamadaInterno)
                    If Not String.IsNullOrWhiteSpace(identificadorLlamadaInterno) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamadaInterno, _codRecurso, Comon.Util.VersionCompleta, objJson, _codPais, objJson.GetHashCode)

                        If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                    "Prosegur.Genesis.Comunicacion.HttpUtil.PostWithheaders",
                                                    Comon.Util.VersionCompleta,
                                                    $"Llamada interna a logueo de recurso {_codRecurso} con el identificadorLlamada: {identificadorLlamadaInterno}", identificadorLlamadaInterno)
                        End If
                    End If

                    Dim response = httpClient.PostAsync(builder.Uri, New StringContent(objJson, Encoding.UTF8, "application/json"))
                    response.Wait()
                    If Not response.Result.IsSuccessStatusCode Then
                        If DirectCast(response.Result.StatusCode, Int32).ToString().Substring(0, 1).Equals("4") Then

                            Dim res = response.Result.Content.ReadAsStringAsync().Result

                            If res = String.Empty Then
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase)
                            Else
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                               "Result: " & res)
                            End If
                        Else
                            Throw New HttpRequestException("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                                   "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                                   "Result: " & response.Result.Content.ReadAsStringAsync().Result)
                        End If
                    End If
                    Dim resultContent = response.Result.Content.ReadAsStringAsync().Result
                    Dim respuesta = New RespuestaHttp(Of T)
                    respuesta.StatusCode = response.Result.StatusCode
                    respuesta.ReasonPhrase = response.Result.ReasonPhrase
                    respuesta.Result = JsonConvert.DeserializeObject(Of T)(resultContent)
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, resultContent, respuesta.StatusCode, respuesta.ReasonPhrase, resultContent.GetHashCode)
                    End If
                    Return respuesta

                End Using
            Catch ex As Exception
                If ex.InnerException IsNot Nothing Then
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, ex.Message, ex.HResult, ex.InnerException.Message, ex.GetHashCode)
                    End If
                Else
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, ex.Message, ex.HResult, ex.Message, ex.GetHashCode)
                    End If
                End If

                Throw
            End Try
        End Function

        Public Function GetWithHeaders(Of T)(ByVal urlApi As String, ByVal parametrosUri As Dictionary(Of String, String), ByVal headers As Dictionary(Of String, String)) As RespuestaHttp(Of T)
            Dim _urlApi As String = Path.Combine(UrlBaseApi, urlApi)
            Dim builder = New UriBuilder(_urlApi)

            If _urlApi.Split(":"c).Length > 2 Then
                Dim porta As String = _urlApi.Split(":"c)(2)
                porta = porta.Substring(0, porta.IndexOf("/"c))
                builder.Port = Int32.Parse(porta)
            End If

            Dim query = HttpUtility.ParseQueryString(builder.Query)

            If parametrosUri IsNot Nothing Then

                For Each param As KeyValuePair(Of String, String) In parametrosUri
                    query(param.Key) = param.Value
                Next

                builder.Query = query.ToString()
            End If
            Try
                Using httpClient = New HttpClient()
                    httpClient.DefaultRequestHeaders.Add(UserAgent, UserAgentValue)
                    For Each header In headers
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value)
                    Next
                    Dim response = httpClient.GetAsync(builder.Uri)
                    response.Wait()
                    If Not response.Result.IsSuccessStatusCode Then
                        If DirectCast(response.Result.StatusCode, Int32).ToString().Substring(0, 1).Equals("4") Then
                            Dim res = response.Result.Content.ReadAsStringAsync().Result

                            If res = String.Empty Then
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase)
                            Else
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                               "Result: " & res)
                            End If
                        Else
                            Throw New HttpRequestException("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                                   "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                                   "Result: " & response.Result.Content.ReadAsStringAsync().Result)
                        End If
                    End If
                    Dim resultContent = response.Result.Content.ReadAsStringAsync().Result
                    Dim respuesta = New RespuestaHttp(Of T)
                    respuesta.StatusCode = response.Result.StatusCode
                    respuesta.ReasonPhrase = response.Result.ReasonPhrase
                    respuesta.Result = JsonConvert.DeserializeObject(Of T)(resultContent)
                    Return respuesta

                End Using
            Catch ex As Exception

                If ex.InnerException IsNot Nothing Then

                    If ex.InnerException.[GetType]() = GetType(TaskCanceledException) Then
                        Throw New TimeoutException("Timeout")
                    ElseIf ex.InnerException.[GetType]() = GetType(HttpRequestException) Then
                        Throw New HttpRequestException("HttpRequestException")
                    End If
                End If

                Throw
            End Try
        End Function


        Private Function Post(Of T)(identificadorLlamada As String, ByVal urlApi As String, ByVal parametros As Object, ByVal parametrosUri As Dictionary(Of String, String)) As RespuestaHttp(Of T)
            Dim identificadorLlamadaInterno = String.Empty
            Try
                Dim _urlApi As String = Path.Combine(UrlBaseApi, urlApi)
                Dim builder = New UriBuilder(_urlApi)

                If _urlApi.Split(":"c).Length > 2 Then
                    Dim porta As String = _urlApi.Split(":"c)(2)
                    porta = porta.Substring(0, porta.IndexOf("/"c))
                    builder.Port = Int32.Parse(porta)
                End If

                Dim query = HttpUtility.ParseQueryString(builder.Query)

                If parametrosUri IsNot Nothing Then

                    For Each param As KeyValuePair(Of String, String) In parametrosUri
                        query(param.Key) = param.Value
                    Next

                    builder.Query = query.ToString()
                End If

                Using httpClient = New HttpClient()
                    httpClient.DefaultRequestHeaders.Add(UserAgent, UserAgentValue)
                    Dim objJson = JsonConvert.SerializeObject(parametros)

                    Logeo.Log.Movimiento.Logger.GenerarIdentificador(_codPais, _codRecurso, identificadorLlamadaInterno)
                    If Not String.IsNullOrWhiteSpace(identificadorLlamadaInterno) Then
                        Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamadaInterno, _codRecurso, Comon.Util.VersionCompleta, objJson, _codPais, objJson.GetHashCode)

                        If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                    "Prosegur.Genesis.Comunicacion.HttpUtil.Post",
                                                    Comon.Util.VersionCompleta,
                                                    $"Llamada interna a logueo de recurso {_codRecurso} con el identificadorLlamada: {identificadorLlamadaInterno}", identificadorLlamadaInterno)
                        End If
                    End If

                    Dim response = httpClient.PostAsync(builder.Uri, New StringContent(objJson, Encoding.UTF8, "application/json"))
                    response.Wait()

                    If Not response.Result.IsSuccessStatusCode Then
                        If DirectCast(response.Result.StatusCode, Int32).ToString().Substring(0, 1).Equals("4") Then

                            Dim res = response.Result.Content.ReadAsStringAsync().Result

                            If res = String.Empty Then
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase)
                            Else
                                Throw New NegocioExcepcion("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                               "Result: " & res)
                            End If
                        Else
                            Throw New HttpRequestException("Status: " & response.Result.StatusCode & Environment.NewLine &
                                                                                               "Reason:" & response.Result.ReasonPhrase & Environment.NewLine &
                                                                                               "Result: " & response.Result.Content.ReadAsStringAsync().Result)
                        End If
                    End If
                    Dim resultContent = response.Result.Content.ReadAsStringAsync().Result
                    Dim respuesta = New RespuestaHttp(Of T)
                    respuesta.StatusCode = response.Result.StatusCode
                    respuesta.ReasonPhrase = response.Result.ReasonPhrase
                    respuesta.Result = JsonConvert.DeserializeObject(Of T)(resultContent)
                    If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then
                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                    "Prosegur.Genesis.Comunicacion.HttpUtil.Post",
                                                    Comon.Util.VersionCompleta,
                                                    $"Se imprime RespuestaHttp respuesta: {JsonConvert.SerializeObject(respuesta)}", identificadorLlamadaInterno)
                    End If
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, resultContent, respuesta.StatusCode, respuesta.ReasonPhrase, resultContent.GetHashCode)
                    End If
                    Return respuesta

                End Using
            Catch ex As Exception

                If ex.InnerException IsNot Nothing Then
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, ex.Message, ex.HResult, ex.InnerException.Message, ex.GetHashCode)
                    End If
                    If ex.InnerException.[GetType]() = GetType(TaskCanceledException) Then
                        Throw New TimeoutException("Timeout")
                    ElseIf ex.InnerException.[GetType]() = GetType(HttpRequestException) Then
                        Throw New HttpRequestException($"HttpRequestException: {ex.InnerException.Message}")
                    End If
                Else
                    'Logueo la respuesta
                    If identificadorLlamadaInterno IsNot Nothing Then
                        Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamadaInterno, ex.Message, ex.HResult, ex.Message, ex.GetHashCode)
                    End If
                End If

                Throw
            End Try
        End Function
        Public Class RespuestaHttp(Of T)
            Public Property StatusCode As String
            Public Property ReasonPhrase As String
            Public Property Result As T
        End Class

        Public Function PostUrlEncoded(Of T)(ByVal url As String, ByVal parametros As Dictionary(Of String, String), identificadorLlamada As String) As RespuestaHttp(Of T)
            Try
                Dim client = New HttpClient()
                client.BaseAddress = New Uri(url)
                Dim request = New HttpRequestMessage(HttpMethod.Post, url)
                request.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded")
                request.Content = New FormUrlEncodedContent(parametros)

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                          "Prosegur.Genesis.Comunicacion.PostUrlEncoded",
                                                          Comon.Util.VersionCompleta,
                                                          $"Llamada PostUrlEncoded, a url: {url} con parámetros: {String.Join(Environment.NewLine, parametros)}",
                                                          "")


                Dim response = client.SendAsync(request)
                response.Wait()
                Dim respuesta = New RespuestaHttp(Of T)
                respuesta.StatusCode = response.Result.StatusCode
                respuesta.ReasonPhrase = response.Result.ReasonPhrase
                respuesta.Result = JsonConvert.DeserializeObject(Of T)(response.Result.Content.ReadAsStringAsync().Result)


                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                          "Prosegur.Genesis.Comunicacion.PostUrlEncoded",
                                                          Comon.Util.VersionCompleta,
                                                          $"respuesta PostUrlEncoded, respuesta.StatusCode : {respuesta.StatusCode} respuesta.ReasonPhrase: {respuesta.ReasonPhrase} response.Result: {response.Result.Content.ReadAsStringAsync().Result}",
                                                          "")

                Return respuesta
            Catch ex As Exception
                Dim innerExc = String.Empty
                Dim Iexcepcion As Exception

                Iexcepcion = ex.InnerException

                While Iexcepcion IsNot Nothing
                    innerExc += $"InnerException: {Iexcepcion.Message} {Environment.NewLine} StackTrace: {Iexcepcion.StackTrace}"
                    Iexcepcion = Iexcepcion.InnerException
                End While


                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                  "Prosegur.Genesis.Comunicacion.PostUrlEncoded",
                                                  Comon.Util.VersionCompleta.ToString(),
                                                  $"Excepcion: {ex.Message} {Environment.NewLine} StackTrace: {ex.StackTrace} 
                                                  {Environment.NewLine} {innerExc}", "")
                Throw ex
            End Try
        End Function


    End Class

End Namespace




