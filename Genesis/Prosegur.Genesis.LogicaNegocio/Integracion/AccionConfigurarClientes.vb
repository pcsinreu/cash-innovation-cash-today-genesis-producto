Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager

Namespace Integracion

    Public Class AccionConfigurarClientes

        Public Shared Function Ejecutar(peticion As ConfigurarClientes.Peticion) As ConfigurarClientes.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder
            Dim identificadorLlamada As String = String.Empty

            ' Inicializar obyecto de respuesta
            Dim respuesta As New ConfigurarClientes.Respuesta
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.ConfigurarClientes,
                           "0000", "",
                           True)

            ' Validar campos obligatorios

            'Logueo
            Dim pais As Clases.Pais
            If String.IsNullOrWhiteSpace(peticion.CodigoPais) Then
                pais = Genesis.Pais.ObtenerPaisPorDefault(peticion.CodigoPais)
            Else
                pais = Genesis.Pais.ObtenerPaisPorCodigo(peticion.CodigoPais, peticion.Configuracion.IdentificadorAjeno)
            End If

            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If

            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "ConfigurarClientes", identificadorLlamada)

            If Not String.IsNullOrEmpty(identificadorLlamada) Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "ConfigurarClientes", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            If validarPeticion(peticion, respuesta) Then

                Try

                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERV_CONF_CLIENTES"

                    TiempoParcial = Now

                    respuesta.Clientes = AccesoDatos.GenesisSaldos.Clientes.ConfigurarClientes(identificadorLlamada, peticion, log, respuesta)
                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")


                Catch ex As Excepcion.NegocioExcepcion

                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarClientes,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarClientes.Salida.Detalle)
                    respuesta.Resultado.Detalles.Add(New ConfigurarClientes.Salida.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.General,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarClientes.Salida.Detalle)
                    Dim detalle As New ConfigurarClientes.Salida.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.General,
                               "0000", "",
                               True)

                    detalle.Descripcion = detalle.Descripcion + " " + Util.RecuperarMensagemTratada(ex)
                    respuesta.Resultado.Detalles.Add(detalle)

                End Try

            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)
            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               respuesta.Resultado.Tipo,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarClientes,
                               "0000", "",
                               True)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing Then
                If peticion.Configuracion.LogDetallar Then
                    ' Añadir el log en la respuesta del servicio
                    respuesta.Resultado.Log = log.ToString().Trim()
                End If

                If Not peticion.Configuracion.RespuestaDetallar Then
                    respuesta.Resultado.Detalles = Nothing
                End If
            End If

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            ' Respuesta
            Return respuesta

        End Function

        Private Shared Function validarPeticion(ByVal peticion As ConfigurarClientes.Peticion,
                                                ByRef respuesta As ConfigurarClientes.Respuesta) As Boolean

            Dim resp As Boolean = True

            ' Validar obyecto peticion y valor del token
            If ValidarToken(peticion, respuesta) Then
                'If peticion.Clientes IsNot Nothing Then
                '    peticion.Clientes.RemoveAll(Function(x) String.IsNullOrEmpty(x.Codigo))
                'End If

                ''Es obligatorio informar el código del cliente
                'If peticion.Clientes Is Nothing OrElse peticion.Clientes.Count = 0 Then
                '    resp = False

                '    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                '           respuesta.Resultado.Descripcion,
                '           Tipo.Error_Negocio,
                '           Contexto.Integraciones,
                '           Funcionalidad.ConfigurarClientes,
                '           "0000", "", True)

                '    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.RespuestaDetallar Then
                '        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarClientes.Salida.Detalle)
                '        Dim d As New ConfigurarClientes.Salida.Detalle
                '        AccesoDatos.Util.resultado(d.Codigo,
                '           d.Descripcion,
                '           Tipo.Error_Negocio,
                '           Contexto.Integraciones,
                '           Funcionalidad.ConfigurarClientes,
                '           "0004", "", True)

                '        respuesta.Resultado.Detalles.Add(d)
                '    End If
                'End If
            Else
                resp = False
            End If

            Return resp

        End Function

        Public Shared Function ValidarToken(ByVal peticion As ConfigurarClientes.Peticion,
                                            ByRef respuesta As ConfigurarClientes.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("1", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("2", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("2", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarClientes,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarClientes.Salida.Detalle)
                Dim d As New ConfigurarClientes.Salida.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.ConfigurarClientes,
                           "000" & ex.Codigo, ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.ConfigurarClientes,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ConfigurarClientes.Salida.Detalle)
                Dim detalle As New ConfigurarClientes.Salida.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.ConfigurarClientes,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function


    End Class

End Namespace