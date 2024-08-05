Imports System.Net
Imports Prosegur.Genesis.Comunicacion.ProxyWS.WebApi
Imports Prosegur.Genesis.ContractoServicio.Contractos.Notification
Imports Prosegur.Genesis.Utilidad

Public Class AccionDeliveredMessages

    Public Shared Sub Ejecutar(peticion As Nilo.Request, respuesta As Nilo.Response, identificadorLlamada As String)
        'Cargo parametros de nilo
        Dim urlAutenticacionNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_NILO)
        Dim credencialNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_CREDENCIAL_NILO)
        Dim urlMessagesNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_URL_DELIVERED_MESSAGES_NILO)
        If Not String.IsNullOrEmpty(urlAutenticacionNilo) Then

            ServicePointManager.SecurityProtocol = CType((&HC0 Or &H300 Or &HC00), SecurityProtocolType) ' Esta magia es para que use las credenciales tls 1.2

            'Buscamos el token de autenticación
            Dim tokenAlmacenado = TokensModule.BuscarTokenJWTConCredencial(urlAutenticacionNilo, credencialNilo, identificadorLlamada, "Prosegur.Genesis.LogicaNegocio.AccionDeliveredMessages.Ejecutar")

            If Not String.IsNullOrWhiteSpace(tokenAlmacenado) Then

                Dim headers = New Dictionary(Of String, String)
                headers.Add("AUTHORIZATION", String.Format("Bearer {0}", tokenAlmacenado))
                Dim http As New HttpUtil

                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                      "Prosegur.Genesis.LogicaNegocio.AccionDeliveredMessages.Ejecutar",
                                                      Comon.Util.VersionCompleta,
                                                      $"Enviando notificacion a Nilo con la url: {urlMessagesNilo}", "")
                Dim agreement = New DeliveredMessages.Agreement
                agreement.transactionId = peticion.IdTran
                agreement.system = "genesis producto"
                If respuesta.StatusCode = "200" Then

                    agreement.state = 0
                Else
                    agreement.state = 1
                    agreement.errorLog = respuesta.StatusDescription
                End If
                Dim fechaStr = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                Dim TimeZone = DateTime.Now.ToString("zzz")
                TimeZone = TimeZone.Replace(":", "")
                agreement.eventDateTime = fechaStr + TimeZone
                Try

                    Dim response = http.PostWithHeaders(Of DeliveredMessages.Response)(identificadorLlamada, urlMessagesNilo, agreement, headers)
                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                    "Prosegur.Genesis.LogicaNegocio.AccionDeliveredMessages.Ejecutar",
                                                    Comon.Util.VersionCompleta,
                                                    $"Código de respuesta de Delivered Messages: {response.StatusCode}", "")
                Catch ex As Exception
                    Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                  "Prosegur.Genesis.LogicaNegocio.AccionDeliveredMessages.Ejecutar",
                                                  Comon.Util.VersionCompleta,
                                                  $"Excepcion: {ex.Message} InnerException: {ex.InnerException}", "")
                End Try




            End If
        End If

    End Sub

    Public Shared Function GetParametro(nombreParametro As String) As String
        Dim url As String = String.Empty
        Dim lista = Util.GetParametros(Comon.Constantes.CODIGO_APLICACION_GENESIS, nombreParametro)
        If lista IsNot Nothing AndAlso lista.Count > 0 Then
            If Not lista.ElementAt(0).MultiValue AndAlso lista.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                url = lista.ElementAt(0).Valores.ElementAt(0)
            Else
                If lista.ElementAt(0).Valores.ElementAt(0) IsNot Nothing Then
                    url = lista.ElementAt(0).Valores.ElementAt(0)
                End If
            End If
        End If
        Return url
    End Function

    Private Shared Function ValidarPeticion(ByRef request As Nilo.Request, ByRef response As Nilo.Response) As Boolean
        Dim resp As Boolean = True
        If request Is Nothing OrElse request.Context Is Nothing OrElse String.IsNullOrWhiteSpace(request.Context.Country) Then
            resp = False
            response.StatusCode = "400"
            response.StatusDescription = "Bad request"
        End If
        'Valido el pais
        Dim pais = Genesis.Pais.ObtenerPaisPorCodigo(request.Context.Country)
        If pais Is Nothing Then
            resp = False
            response.StatusCode = "200"
            response.StatusDescription = "Ok"
        End If
        Return resp
    End Function


End Class
