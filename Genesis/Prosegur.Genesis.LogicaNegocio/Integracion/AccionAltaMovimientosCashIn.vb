Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.AltaMovimientosCashIn
Imports System.Data
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.crearDocumentoFondos
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.Movimientos

Namespace Integracion
    Public Class AccionAltaMovimientosCashIn

        Public Shared Function Ejecutar(peticion As AltaMovimientosCashIn.Peticion) As AltaMovimientosCashIn.Respuesta
            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializo el objeto de respuesta
            Dim respuesta As New AltaMovimientosCashIn.Respuesta

            AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Tipo.Exito,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosCashIn,
                           "0000", "",
                           True)

            'Logueo la peticion de entrada
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty
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
            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "AltaMovimientosCashIn", identificadorLlamada)
            If Not String.IsNullOrWhiteSpace(identificadorLlamada) Then


                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "AltaMovimientosCashIn", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            ' Validar campos obligatorios
            If ValidarPeticion(peticion, respuesta) Then
                Try
                    If String.IsNullOrEmpty(peticion.Configuracion.Usuario) Then peticion.Configuracion.Usuario = "SERVICIO_ALTA_MOVIMIENTOS_CASHIN"

                    TiempoParcial = Now

                    AccesoDatos.GenesisSaldos.Movimientos.CashIn.AltaMovimientosCashIn(identificadorLlamada, peticion, respuesta, log)

                    log.AppendLine("Tiempo de acceso a datos:  " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TratarRespuesta(peticion, respuesta)

                    Try
                        Dim listaActualIds = New List(Of String)
                        If respuesta.Movimientos IsNot Nothing Then
                            For Each movimiento In respuesta.Movimientos.Where(Function(a) a.TipoResultado.Equals("0"))
                                If (Not String.IsNullOrEmpty(movimiento.ActualId)) Then
                                    listaActualIds.Add(movimiento.ActualId)
                                End If
                            Next

                            If (listaActualIds.Any) Then
                                Dim peticionEnviarDocumentoFVO = New EnviarDocumentos.Peticion
                                ' Dim listaActualIdsValidadas = AccesoDatos.GenesisSaldos.Integracion.FechaValorOnline.ValidarIntegracion(listaActualIds, peticion.Configuracion.Usuario)
                                ' If listaActualIdsValidadas.Any Then
                                peticionEnviarDocumentoFVO.Configuracion = New EnviarDocumentos.Entrada.Configuracion
                                    peticionEnviarDocumentoFVO.ActualIds = listaActualIds
                                    peticionEnviarDocumentoFVO.Configuracion.Token = peticion.Configuracion.Token
                                    peticionEnviarDocumentoFVO.Configuracion.Usuario = peticion.Configuracion.Usuario
                                    peticionEnviarDocumentoFVO.CodigoPais = peticion.CodigoPais
                                    System.Threading.ThreadPool.QueueUserWorkItem(Sub() AccionEnviarDocumentos.Ejecutar(peticionEnviarDocumentoFVO))
                                'End If
                            End If
                        End If

                    Catch ex As Exception
                        'No tratamos excepción
                    End Try

                Catch ex As Excepcion.NegocioExcepcion
                    ' Respuesta defecto para Excepciones de Negocio
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosCashIn,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = ex.Codigo, .Descripcion = ex.Descricao})
                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosCashIn,
                               "0000", "",
                               True)

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    Dim detalle As New ContractoServicio.Contractos.Integracion.Comon.Detalle
                    AccesoDatos.Util.resultado(detalle.Codigo,
                               detalle.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosCashIn,
                               "0001", Util.RecuperarMensagemTratada(ex),
                               True)
                    respuesta.Resultado.Detalles.Add(detalle)
                End Try
            End If

            'Logueo la respuesta
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.Resultado.Codigo, respuesta.Resultado.Descripcion, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            ' Tipo de respuesta
            respuesta.Resultado.Tipo = respuesta.Resultado.Codigo.Substring(0, 1)

            ' Tiempo de ejecucion
            respuesta.Resultado.TiempoDeEjecucion = Now.Subtract(TiempoInicial).ToString()

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                ' Graba en el LOG el tiempo total de la ejecucion del proceso
                log.AppendLine("Tiempo total: " & respuesta.Resultado.TiempoDeEjecucion & ";")

                ' Añadir el log en la respuesta del servicio
                respuesta.Resultado.Log = log.ToString().Trim()
            End If

            Return respuesta
        End Function

        Private Shared Sub obtenerDelegAndPlantaDeUnaMAE(deviceID As String, ds As DataSet, ByRef codigoDeleg As String, ByRef codigoPlanta As String)
            If ds IsNot Nothing AndAlso ds.Tables IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("maquinas") Then
                Dim dt As DataTable = ds.Tables("maquinas")
                Dim fila As DataRow() = dt.Select(String.Format("cod_sector = '{0}'", deviceID))

                codigoDeleg = Util.AtribuirValorObj(fila(0)("COD_DELEGACION"), GetType(String))
                codigoPlanta = Util.AtribuirValorObj(fila(0)("COD_PLANTA"), GetType(String))
            End If
        End Sub

        Private Shared Sub TratarRespuesta(peticion As AltaMovimientosCashIn.Peticion, respuesta As AltaMovimientosCashIn.Respuesta)
            
            If respuesta.Resultado.Detalles IsNot Nothing AndAlso respuesta.Resultado.Detalles.Count > 0 Then
                If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                    respuesta.Resultado.Descripcion,
                                    Tipo.Error_Aplicacion,
                                    Contexto.Integraciones,
                                    Funcionalidad.AltaMovimientosCashIn,
                                    "0000", "",
                                    True)
                ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Tipo.Error_Negocio) Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                            respuesta.Resultado.Descripcion,
                            Tipo.Error_Negocio,
                            Contexto.Integraciones,
                            Funcionalidad.AltaMovimientosCashIn,
                            "0000", "",
                            True)
                End If
            End If
            
            If respuesta.Movimientos IsNot Nothing AndAlso respuesta.Movimientos.Count > 0 Then
                For Each movimiento In respuesta.Movimientos
                    If movimiento.Detalles IsNot Nothing AndAlso movimiento.Detalles.Count > 0 Then
                        For Each _detalle In movimiento.Detalles
                            If _detalle.Codigo.Substring(0, 1) = Tipo.Error_Negocio Then

                                movimiento.TipoResultado = _detalle.Codigo.Substring(0, 1)

                                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                            respuesta.Resultado.Descripcion,
                                            Tipo.Error_Negocio,
                                            Contexto.Integraciones,
                                            Funcionalidad.AltaMovimientosCashIn,
                                            "0000", "",
                                            True)
                            ElseIf _detalle.Codigo.Substring(0, 1) = Tipo.Error_Aplicacion Then

                                movimiento.TipoResultado = _detalle.Codigo.Substring(0, 1)

                                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                                            respuesta.Resultado.Descripcion,
                                            Tipo.Error_Aplicacion,
                                            Contexto.Integraciones,
                                            Funcionalidad.AltaMovimientosCashIn,
                                            "0000", "",
                                            True)

                            End If
                        Next
                    End If

                    If peticion Is Nothing OrElse peticion.Configuracion Is Nothing OrElse Not peticion.Configuracion.RespuestaDetallar Then
                        movimiento.Detalles = Nothing
                    End If
                Next
            End If

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not peticion.Configuracion.RespuestaDetallar Then
                respuesta.Resultado.Detalles = Nothing
            End If
        End Sub

        Private Shared Function ValidarPeticion(ByRef peticion As AltaMovimientosCashIn.Peticion, ByRef respuesta As AltaMovimientosCashIn.Respuesta) As Boolean
            Dim resp As Boolean = True

            ' /* Validar obyecto peticion y valor del token */
            If ValidarToken(peticion, respuesta) Then

                ' /* Valido que la petición contenga al menos un movimiento. */
                If peticion.Movimientos Is Nothing OrElse peticion.Movimientos.Count = 0 Then
                    AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                         respuesta.Resultado.Descripcion,
                         Tipo.Error_Negocio,
                         Contexto.Integraciones,
                         Funcionalidad.AltaMovimientosCashIn,
                         "0003", "", True)
                    If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso peticion.Configuracion.LogDetallar Then
                        If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                        Dim d As New ContractoServicio.Contractos.Integracion.Comon.Detalle
                        AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosCashIn,
                           "0001", "", True)
                        respuesta.Resultado.Detalles.Add(d)
                    End If

                    resp = False

                End If
            Else
                'No valida Token
                resp = False
            End If

            Return resp
        End Function
        Public Shared Function ValidarToken(ByVal peticion As AltaMovimientosCashIn.Peticion,
                                    ByRef respuesta As AltaMovimientosCashIn.Respuesta) As Boolean

            Try

                If peticion Is Nothing Then
                    Throw New Excepcion.NegocioExcepcion("01", "")
                Else
                    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                        If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) OrElse Not AppSettings("Token").Equals(peticion.Configuracion.Token) Then
                            If peticion.Configuracion Is Nothing OrElse String.IsNullOrEmpty(peticion.Configuracion.Token) Then
                                Throw New Excepcion.NegocioExcepcion("02", String.Empty)
                            Else
                                Throw New Excepcion.NegocioExcepcion("02", peticion.Configuracion.Token)
                            End If
                        End If
                    End If
                End If

            Catch ex As Excepcion.NegocioExcepcion

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Negocio,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosCashIn,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                Dim d As New ContractoServicio.Contractos.Integracion.Comon.Detalle
                AccesoDatos.Util.resultado(d.Codigo,
                           d.Descripcion,
                           Tipo.Error_Negocio,
                           Contexto.Integraciones,
                           Funcionalidad.AltaMovimientosCashIn,
                           "00" & ex.Codigo.ToString.PadLeft(2, "0"c), ex.Descricao, True)
                respuesta.Resultado.Detalles.Add(d)

                Return False

            Catch ex As Exception

                AccesoDatos.Util.resultado(respuesta.Resultado.Codigo,
                               respuesta.Resultado.Descripcion,
                               Tipo.Error_Aplicacion,
                               Contexto.Integraciones,
                               Funcionalidad.AltaMovimientosCashIn,
                               "0000", "",
                               True)

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                Dim detalle As New ContractoServicio.Contractos.Integracion.Comon.Detalle
                AccesoDatos.Util.resultado(detalle.Codigo,
                                   detalle.Descripcion,
                                   Tipo.Error_Aplicacion,
                                   Contexto.Integraciones,
                                   Funcionalidad.AltaMovimientosCashIn,
                                   "0001", Util.RecuperarMensagemTratada(ex),
                                   True)
                respuesta.Resultado.Detalles.Add(detalle)

                Return False

            End Try

            Return True

        End Function

    End Class
End Namespace

