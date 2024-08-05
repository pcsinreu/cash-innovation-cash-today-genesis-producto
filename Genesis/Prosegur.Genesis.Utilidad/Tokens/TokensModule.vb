Imports System.Net
Imports Microsoft.IdentityModel.JsonWebTokens
Imports Prosegur.Genesis.Comunicacion.ProxyWS.WebApi

Public Module TokensModule
    Public Property DiccToken() As New Dictionary(Of String, Tokens.Token)

    Public Function BuscarTokenJWTConCredencial(urlAutenticacion As String, credencial As String, identificadorLlamada As String, origenLlamada As String) As String
        Try
            Dim tokenAlmacenado = New Tokens.Token
            Dim tokenValido = False

            Dim cred = New Tokens.Credential With {
                .credential = credencial
            }

            If DiccToken.TryGetValue(urlAutenticacion, tokenAlmacenado) Then
                'Validamos el token almacenado

                If tokenAlmacenado.ValidTo > Date.UtcNow Then
                    tokenValido = True

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                          origenLlamada,
                                          Comon.Util.VersionCompleta,
                                          $"Token de autenticación válido con fecha de expiración: {tokenAlmacenado.ValidTo.ToLocalTime():dd/MM/yyyy HH:mm:ss zzz}", "")
                End If
            End If

            If Not tokenValido Then
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                             origenLlamada,
                                                             Comon.Util.VersionCompleta,
                                                             $"Obteniendo Token de autenticación de con la url: {urlAutenticacion}", "")

                Dim tokenResponse = New HttpUtil().Post(Of Tokens.Token)(identificadorLlamada, urlAutenticacion, cred)

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                                  origenLlamada,
                                                                  Comon.Util.VersionCompleta,
                                                                  $"Código de respuesta de token: {tokenResponse.StatusCode}", "")

                If tokenResponse.StatusCode = 200 Then
                    Dim jsonToken = New JsonWebTokenHandler().ReadToken(tokenResponse.Result.Token)

                    'Guardamos el token y la validez del token con la fecha actual menos el tiempo de exipración del token menos 5 minutos
                    tokenAlmacenado = New Tokens.Token With {
                        .Token = tokenResponse.Result.Token,
                        .ValidTo = jsonToken.ValidTo.AddSeconds(-300)
                    }

                    If DiccToken.ContainsKey(urlAutenticacion) Then
                        DiccToken(urlAutenticacion) = tokenAlmacenado
                    Else
                        DiccToken.Add(urlAutenticacion, tokenAlmacenado)
                    End If
                End If
            End If

            If Not String.IsNullOrWhiteSpace(tokenAlmacenado.Token) Then
                Return tokenAlmacenado.Token
            End If

            Return Nothing
        Catch ex As Exception
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                                  origenLlamada,
                                                                  Comon.Util.VersionCompleta,
                                                                  $"Se produjo una excepción al BuscarTokenJWTConCredencial, ex.Message: {ex.Message}, ex.InnerException.Message: { IIf(ex.InnerException IsNot Nothing, ex.InnerException.Message, String.Empty) }", "")

            Return Nothing
        End Try
    End Function

    Public Function BuscarTokenBearerConClientCredencial(identificadorAutenticacion As String, urlAutenticacion As String, parametros As Dictionary(Of String, String), identificadorLlamada As String, origenLlamada As String) As String
        Try
            Dim http = New HttpUtil
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            Dim tokenAlmacenado As New Tokens.Token
            Dim tokenValido = False
            Dim codigoAutenticacion As String = identificadorAutenticacion.Trim + urlAutenticacion.Trim

            If TokensModule.DiccToken.TryGetValue(codigoAutenticacion, tokenAlmacenado) Then
                'Validamos el token almacenado
                If tokenAlmacenado.ValidTo > Date.UtcNow Then
                    tokenValido = True

                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                        origenLlamada,
                                        Comon.Util.VersionCompleta,
                                        $"Token de autenticación válido con fecha de expiración: {tokenAlmacenado.ValidTo.ToLocalTime():dd/MM/yyyy HH:mm:ss zzz}", "")
                End If
            End If

            If Not tokenValido Then
                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                             origenLlamada,
                                                             Comon.Util.VersionCompleta,
                                                             $"Obteniendo Token de autenticación con la url: {urlAutenticacion}", "")

                Dim tokenResponse = http.PostUrlEncoded(Of Comon.Clases.Token)(urlAutenticacion, parametros, identificadorLlamada)


                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                                  origenLlamada,
                                                                  Comon.Util.VersionCompleta,
                                                                  $"Código de respuesta de token: {tokenResponse.StatusCode}", "")
                If tokenResponse.StatusCode = 200 Then
                    'Guardamos el token y la validez del token con la fecha actual mas el tiempo de expiración del token menos 5 minutos
                    tokenAlmacenado = New Tokens.Token With {
                        .Token = tokenResponse.Result.access_token,
                        .ValidTo = Date.UtcNow.AddSeconds(tokenResponse.Result.expires_in - 300)
                    }

                    If TokensModule.DiccToken.ContainsKey(codigoAutenticacion) Then
                        TokensModule.DiccToken(codigoAutenticacion) = tokenAlmacenado
                    Else
                        TokensModule.DiccToken.Add(codigoAutenticacion, tokenAlmacenado)
                    End If
                End If


            End If

            If Not String.IsNullOrWhiteSpace(tokenAlmacenado.Token) Then
                Return tokenAlmacenado.Token
            End If
            Return Nothing
        Catch ex As Exception
            Dim innerExc = String.Empty
            If ex.InnerException IsNot Nothing Then
                innerExc = ex.InnerException.Message
            End If
            Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                                  origenLlamada,
                                                                  Comon.Util.VersionCompleta,
                                                                  $"Se produjo una excepción al BuscarTokenBearerConClientCredencial, ex.Message: {ex.Message}, ex.InnerException.Message: { innerExc }", "")

            Return Nothing
        End Try

    End Function
End Module
