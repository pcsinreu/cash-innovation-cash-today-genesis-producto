Imports System.Text
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports Prosegur.Genesis.ContractoServicio.Contractos.Infraestructura
Imports System.IO
Imports System.Net
Imports Prosegur.Genesis.Comon

Namespace Infraestructura
    Public Class AccionInformacionVersion
        Public Shared Function Ejecutar(pPeticion As RecuperarInformacionesVersion.Peticion) As RecuperarInformacionesVersion.Respuesta
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Lista con los assemblies
            Dim lista As List(Of Comon.Clases.Assembly)
            lista = New List(Of Comon.Clases.Assembly)()
            ' Inicializar objeto de Respuesta
            Dim respuesta As New RecuperarInformacionesVersion.Respuesta

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Infraestructura,
                           Funcionalidad.RecuperarInformacionesVersion,
                           "0000", "",
                           True)

#Region "Información de HostName, IPv4"
            respuesta.Resultado.HostName = DnsHelper.GetHostName()
            respuesta.Resultado.IpAddress = DnsHelper.GetHostNameIp4()
#End Region

            If ValidarPeticion(pPeticion, respuesta) Then

                Try
                    TiempoParcial = Now

                    lista = Comon.Util.RetornaDLLsAssembly(Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin"))
                    log.AppendLine("Tiempo de acceso a assemblies:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now

                    respuesta.Assemblies = lista

                    log.AppendLine("Tiempo confección de respuesta:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperarInformacionesVersion,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarInformacionesVersion.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New RecuperarInformacionesVersion.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception

                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperarInformacionesVersion,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarInformacionesVersion.Salida.Detalle)
                    Dim detalle As New RecuperarInformacionesVersion.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperarInformacionesVersion,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)
                End Try
            End If


            TratarResultado(pPeticion, respuesta)

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso pPeticion.Configuracion.LogDetallar Then
                ' Añadir el log en la respuesta del servicio
                respuesta.Resultado.Log = log.ToString().Trim()
            Else
                respuesta.Resultado.Detalles = Nothing
            End If

            Return respuesta
        End Function

        Private Shared Sub TratarResultado(pPeticion As RecuperarInformacionesVersion.Peticion, respuesta As RecuperarInformacionesVersion.Respuesta)
            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then
                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                            respuesta.Resultado.Descripcion,
                                            Tipo.Error_Negocio,
                                            Contexto.Infraestructura,
                                            Funcionalidad.RecuperarInformacionesVersion,
                                            "0000", "",
                                            True)
            End If

            If pPeticion IsNot Nothing AndAlso pPeticion.Configuracion IsNot Nothing AndAlso Not pPeticion.Configuracion.LogDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If
        End Sub

        Private Shared Function ValidarToken(pPeticion As RecuperarInformacionesVersion.Peticion, pRespuesta As RecuperarInformacionesVersion.Respuesta) As Boolean
            Try

                If pPeticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("01", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If pPeticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(pPeticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(pPeticion.Configuracion.Token) Then
                            If pPeticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(pPeticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("02", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("02", pPeticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(pRespuesta.Resultado.Codigo,
                               pRespuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperarInformacionesVersion,
                               "0000", "",
                               True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of RecuperarInformacionesVersion.Salida.Detalle)()
                Dim d As New RecuperarInformacionesVersion.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Infraestructura,
                           Funcionalidad.RecuperarInformacionesVersion,
                           "000" & ex.Codigo, ex.Descricao, True)
                pRespuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(pRespuesta.Resultado.Codigo,
                               pRespuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Infraestructura,
                               Funcionalidad.RecuperarInformacionesVersion,
                               "0000", "",
                               True)

                If pRespuesta.Resultado.Detalles Is Nothing Then pRespuesta.Resultado.Detalles = New List(Of RecuperarInformacionesVersion.Salida.Detalle)
                Dim detalle As New RecuperarInformacionesVersion.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Infraestructura,
                                   Funcionalidad.RecuperarInformacionesVersion,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                pRespuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True
        End Function

        Private Shared Function ValidarPeticion(pPeticion As RecuperarInformacionesVersion.Peticion, ByRef pRespuesta As RecuperarInformacionesVersion.Respuesta) As Boolean
            Dim valida As Boolean

            If ValidarToken(pPeticion, pRespuesta) Then
                'No tiene validaciones de entrada ya que solo posee Configuracion
                valida = True
            Else
                'No validó el Token
                valida = False
            End If
            Return valida
        End Function


    End Class
End Namespace
