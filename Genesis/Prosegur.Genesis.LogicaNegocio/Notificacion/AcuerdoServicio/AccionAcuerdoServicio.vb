Imports System.Net
Imports System.Text
Imports Prosegur.Genesis.Comunicacion.ProxyWS.WebApi
Imports Prosegur.Genesis.ContractoServicio.Contractos.Notification
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.Excepcion
Imports Prosegur.Genesis.Utilidad

Namespace Notificacion
    Public Class AccionAcuerdoServicio
        Public Shared Function Ejecutar(peticion As Nilo.Request, identificadorLlamada As String) As Nilo.Response

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializo el objeto de respuesta
            Dim respuesta As New Nilo.Response With {.StatusCode = "200", .StatusDescription = "Success"}

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta) Then
                Try
                    'Logueo la peticion de entrada
                    Dim pais = Genesis.Pais.ObtenerPaisPorCodigo(peticion.Context.Country)
                    Dim codigoPais = String.Empty
                    If pais IsNot Nothing Then
                        codigoPais = pais.Codigo
                    End If

                    'Cargo parametros de nilo
                    Dim urlAutenticacionNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_URL_AUTENTICACION_NILO)
                    Dim credencialNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_CREDENCIAL_NILO)
                    Dim urlAcuerdoServicioNilo = GetParametro(Comon.Constantes.CODIGO_PARAMETRO_URL_ACUERDO_SERVICIO_NILO)

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                    Dim http = New HttpUtil

                    Dim tokenAlmacenado = TokensModule.BuscarTokenJWTConCredencial(urlAutenticacionNilo, credencialNilo, identificadorLlamada, "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar")

                    If Not String.IsNullOrWhiteSpace(tokenAlmacenado) Then
                        'Obtengo el acuerdo servicio usando el token
                        Dim headers = New Dictionary(Of String, String)
                        headers.Add("AUTHORIZATION", String.Format("Bearer {0}", tokenAlmacenado))
                        http = New HttpUtil

                        Dim urlWithId = String.Format("{0}/{1}", urlAcuerdoServicioNilo, peticion.Object.SourceId)

                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              $"Obteniendo Acuerdo de servicio de Nilo con la url: {urlWithId}", "")

                        Dim acuerdoResponse = http.GetWithHeaders(Of AcuerdoServicio.Agreement)(urlWithId, Nothing, headers)

                        Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              $"Código de respuesta de Acuerdo de servicio: {acuerdoResponse.StatusCode}", "")

                        If acuerdoResponse.StatusCode = 200 Then
                            Dim agreement = acuerdoResponse.Result
                            'Con la info de agreement crear peticion para acuerdo servicios
                            Dim peticionAcServ = New ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio.Peticion With {
                                .CodigoPais = agreement.countryCode,
                                .Configuracion = New ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio.Entrada.Configuracion
                            }
                            peticionAcServ.Configuracion.Token = AppSettings("Token")
                            peticionAcServ.Configuracion.LogDetallar = True
                            peticionAcServ.Configuracion.RespuestaDetallar = True
                            peticionAcServ.Configuracion.Usuario = "Integración Nilo"

                            Dim acuerdos = New List(Of ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio.Entrada.Acuerdo)
                            Dim productos = New List(Of String) From {
                                Comon.Constantes.CODIGO_PRODUCTO_FECHA_VALOR,
                                Comon.Constantes.CODIGO_PRODUCTO_TRANSACCION
                            }

                            'Validamos que el contrato posea al menos una orden de servicio con los códigos utilizados por Génesis Producto
                            If agreement.serviceOrders.Any(Function(x) productos.Contains(x.productCode)) Then
                                'Armo una petición para dar de baja los acuerdos que no se reciben de NILO
                                Dim acuerdoBaja = New ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio.Entrada.Acuerdo
                                acuerdoBaja.Accion = "BAJA-AUTO"
                                acuerdoBaja.ContractId = agreement.contract.id
                                acuerdoBaja.CodigoCliente = agreement.entityUniqueCode
                                acuerdoBaja.CodigoSubCliente = agreement.centerUniqueCode
                                acuerdoBaja.CodigoPuntoServicio = agreement.servicePointUniqueCode
                                acuerdos.Add(acuerdoBaja)

                                For Each serviceOrder In agreement.serviceOrders.Where(Function(x) productos.Contains(x.productCode))
                                    Dim acuerdo = New ContractoServicio.Contractos.Integracion.ConfigurarAcuerdosServicio.Entrada.Acuerdo
                                    If peticion.Object.Operation.ToUpper = "B" Then
                                        acuerdo.Accion = "BAJA"
                                    Else
                                        acuerdo.Accion = "ALTA"
                                    End If
                                    acuerdo.ContractId = agreement.contract.id
                                    acuerdo.CodigoCliente = agreement.entityUniqueCode
                                    acuerdo.CodigoSubCliente = agreement.centerUniqueCode
                                    acuerdo.CodigoPuntoServicio = agreement.servicePointUniqueCode
                                    acuerdo.ProductCode = serviceOrder.productCode
                                    acuerdo.ServiceOrderCode = serviceOrder.code
                                    acuerdo.ServiceOrderId = serviceOrder.id
                                    acuerdo.FechaVigenciaInicio = serviceOrder.operationalConfigs(0).startDate
                                    acuerdo.FechaVigenciaFin = serviceOrder.operationalConfigs(0).endDate
                                    acuerdos.Add(acuerdo)
                                Next
                            Else
                                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "No se encuentran órdenes de servicio con códigos de producto manejados por Génesis Producto", "")
                            End If


                            'Validar si está vacio no llamar al configurar acuerdos y devolver success
                            If acuerdos.Count > 0 Then
                                peticionAcServ.Acuerdos = acuerdos

                                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "Llamada a Prosegur.Genesis.LogicaNegocio.Integracion.AccionConfigurarAcuerdosServicio.Ejecutar", "")

                                Dim respuestaAcuerdoServicio = Integracion.AccionConfigurarAcuerdosServicio.Ejecutar(peticionAcServ, identificadorLlamada)
                                If respuestaAcuerdoServicio.Resultado.Tipo = Tipo.Exito Then
                                    respuesta.StatusCode = 200
                                ElseIf respuestaAcuerdoServicio.Resultado.Tipo = Tipo.Error_Negocio Then
                                    respuesta.StatusCode = 400
                                ElseIf respuestaAcuerdoServicio.Resultado.Tipo = Tipo.Error_Aplicacion Then
                                    respuesta.StatusCode = 500
                                End If

                                Dim descripcion As String = respuestaAcuerdoServicio.Resultado.Descripcion

                                'Concatenar detalles de resultado en caso de que exista
                                If respuestaAcuerdoServicio.Resultado.Detalles IsNot Nothing AndAlso respuestaAcuerdoServicio.Resultado.Detalles.Count > 0 Then
                                    For Each detalle In respuestaAcuerdoServicio.Resultado.Detalles
                                        descripcion += $"{vbCrLf}{detalle.Codigo} - {detalle.Descripcion}"
                                    Next
                                End If

                                respuesta.StatusDescription = descripcion
                            Else
                                Logeo.Log.Movimiento.Logger.AgregaDetalle(identificadorLlamada,
                                                              "Prosegur.Genesis.LogicaNegocio.AccionAcuerdoServicio.Ejecutar",
                                                              Comon.Util.VersionCompleta,
                                                              "No se encontraron acuerdos de servicio para los códigos de producto de Genesis", "")
                            End If


                        End If

                    End If
                Catch ex As NegocioExcepcion
                    respuesta.StatusCode = "400"
                    respuesta.StatusDescription = ex.Message
                Catch ex As Exception
                    respuesta.StatusCode = "500"
                    respuesta.StatusDescription = ex.Message
                End Try
            End If

            Return respuesta
        End Function

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

End Namespace
