Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text

Namespace Integracion

    Public Class AccionCrearDocumento

        Public Shared Function crearDocumentoFondos(peticion As crearDocumentoFondos.Peticion,
                                                    Optional EsConteo As Boolean = False) As crearDocumentoFondos.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim respuesta As New crearDocumentoFondos.Respuesta
            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO
            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_INTEGRACION_SEM_ERRO
            respuesta.ValidacionesError = New List(Of ValidacionError)

            ' Inicializa objeto Documento
            Dim documento As Clases.Documento = Nothing
            'Logueo la peticion de entrada
            Dim identificadorLlamada As String
            identificadorLlamada = String.Empty
            Dim pais = Genesis.Pais.ObtenerPaisPorDelegacion(peticion.movimiento.origen.codigoDelegacion, peticion.IdentificadorAjeno)
            Dim codigoPais = String.Empty
            If pais IsNot Nothing Then
                codigoPais = pais.Codigo
            End If
            Logeo.Log.Movimiento.Logger.GenerarIdentificador(codigoPais, "CrearDocumentosFondos", identificadorLlamada)
            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.IniciaLlamada(identificadorLlamada, "CrearDocumentosFondos", Comon.Util.VersionCompleta, AccesoDatos.Util.ToXML(peticion), codigoPais, AccesoDatos.Util.ToXML(peticion).GetHashCode)
            End If

            Try

                ' Validar el token
                validarToken(peticion)

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = ex.Message})
                ' Respuesta defecto para Excepciones de Negocio
                respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)

                ' Validar si es un error de Infraestructura o de Aplicacion
                If ValidarException(ex) Then

                    ' Respuesta defecto para Excepciones de Infraestructura
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                Else

                    ' Respuesta defecto para Excepciones de Aplicaciones
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                End If

                If peticion.validarPostError AndAlso ex.Message <> "ValidacionesError" Then
                    respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL000", .descripcion = Util.RecuperarMensagemTratada(ex)})
                End If

            End Try

            If respuesta.ValidacionesError Is Nothing OrElse respuesta.ValidacionesError.Count = 0 Then

                ' Validar campos obligatorios
                ' Esta validacion es hecha en la petición, validando si fue informado los campos base para crear el documento
                validarPeticion(peticion, respuesta.ValidacionesError)

                If respuesta.ValidacionesError Is Nothing OrElse respuesta.ValidacionesError.Count = 0 Then

                    Try
                        TiempoParcial = Now
                        'Try
                        Dim codigoDelegacionOrigen As String = peticion.movimiento.origen.codigoDelegacion
                        Dim codigoDelegacionDestino As String = ""
                        If peticion.movimiento.destino IsNot Nothing Then
                            codigoDelegacionDestino = peticion.movimiento.destino.codigoDelegacion
                        End If
                        peticion.movimiento.fechaHoraGestionFondos = Util.CalcularGMT(peticion.movimiento.fechaHoraGestionFondos, peticion.IdentificadorAjeno, codigoDelegacionOrigen, codigoDelegacionDestino)
                        If peticion.movimiento.FechaContable IsNot Nothing Then
                            peticion.movimiento.FechaContable = Util.CalcularGMT(peticion.movimiento.FechaContable, peticion.IdentificadorAjeno, codigoDelegacionOrigen, codigoDelegacionDestino)
                        End If

                        'Catch ex As Exception
                        'Util.TratarErroBugsnag(ex)
                        'respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = ex.Message})
                        'End Try
                        log.AppendLine("Tiempo de calcular GMT: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                        Dim codigoUsuario As String = If(String.IsNullOrEmpty(peticion.movimiento.usuario), "SERVICIO_CREAR_DOCUMENTO_FONDOS_SIMPLE", peticion.movimiento.usuario)

                        Dim identificadorMaquina As String = String.Empty
                        Dim identificadorPlanificacion As String = String.Empty
                        Dim identificadorPeriodo As String = String.Empty
                        Dim codigoTipoPlanificacion As String = String.Empty

                        If respuesta.ValidacionesError.Count = 0 Then
                            TiempoParcial = Now
                            AccesoDatos.GenesisSaldos.Documento.GrabarDocumentoFondos(identificadorLlamada, peticion, If(EsConteo, False, True), True, respuesta.ValidacionesError)
                            log.AppendLine("Tiempo de Crear/Guardar/Aceptar Documento: " & Now.Subtract(TiempoParcial).ToString() & "; ")
                        End If

                        If respuesta.ValidacionesError.Count > 0 Then

                            ' Respuesta defecto para Excepciones de Negocio
                            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

                            If Not peticion.validarPostError Then
                                respuesta.ValidacionesError = Nothing
                            End If

                        End If
                        Try
                            Dim listaActualIds = New List(Of String)
                            If peticion.movimiento IsNot Nothing AndAlso Not String.IsNullOrEmpty(peticion.movimiento.ActualId) AndAlso (respuesta.ValidacionesError Is Nothing OrElse respuesta.ValidacionesError.Count = 0) Then
                                listaActualIds.Add(peticion.movimiento.ActualId)
                            End If
                            If (listaActualIds.Any) Then
                                Dim peticionEnviarDocumentoFVO = New EnviarDocumentos.Peticion
                                'Dim listaActualIdsValidadas = AccesoDatos.GenesisSaldos.Integracion.FechaValorOnline.ValidarIntegracion(listaActualIds, codigoUsuario)
                                'If listaActualIdsValidadas.Any Then
                                peticionEnviarDocumentoFVO.Configuracion = New EnviarDocumentos.Entrada.Configuracion
                                peticionEnviarDocumentoFVO.ActualIds = listaActualIds
                                peticionEnviarDocumentoFVO.Configuracion.Token = peticion.tokenAcceso
                                    peticionEnviarDocumentoFVO.CodigoPais = codigoPais
                                    System.Threading.ThreadPool.QueueUserWorkItem(Sub() AccionEnviarDocumentos.Ejecutar(peticionEnviarDocumentoFVO))
                                'End If
                            End If
                        Catch ex As Exception

                        End Try
                    Catch ex As Excepcion.NegocioExcepcion


                        If peticion.validarPostError Then

                            Dim _validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

                            ' Validar campos obligatorios
                            ' Esta validacion es hecha en la petición, validando si fue informado los campos base para crear el documento
                            validarPeticion(peticion, _validaciones)

                            If _validaciones Is Nothing OrElse _validaciones.Count = 0 Then
                                ' Validar integridad del Documento
                                ' Caso no fue encontrado errores (basicos) de campos obligatorios, intenta encontrar errores
                                ' más complexos na creación del documento;
                                ' Ese proceso tarda a ejecutar, pues es hecho una busqueda mas detallada de todos los atributos del documento
                                RevisarIntegridadDocumento(peticion, documento, respuesta.ValidacionesError)

                                If respuesta.ValidacionesError Is Nothing OrElse respuesta.ValidacionesError.Count < 2 Then
                                    respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.codigo = ex.Codigo, .descripcion = ex.Message})
                                End If

                            Else
                                respuesta.ValidacionesError.AddRange(_validaciones)
                            End If

                        Else
                            'respuesta.ValidacionesError = Nothing
                            respuesta.ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                            respuesta.ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError() With {.codigo = ex.Codigo, .descripcion = ex.Descricao})
                        End If

                        ' Respuesta defecto para Excepciones de Negocio
                        respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                        respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

                    Catch ex As Exception
                        Util.TratarErroBugsnag(ex)

                        ' Validar si es un error de Infraestructura o de Aplicacion
                        If ValidarException(ex) Then

                            ' Respuesta defecto para Excepciones de Infraestructura
                            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_INFRAESTRUCTURA
                            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_INFRAESTRUCTURA

                        Else

                            ' Respuesta defecto para Excepciones de Aplicaciones
                            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_APLICACION

                        End If

                        If peticion.validarPostError AndAlso ex.Message <> "ValidacionesError" Then
                            respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL000", .descripcion = Util.RecuperarMensagemTratada(ex)})
                        End If

                        If Not peticion.validarPostError Then
                            respuesta.ValidacionesError = Nothing
                        End If

                    End Try

                Else

                    ' Respuesta defecto para Excepciones de Negocio
                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                    respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

                End If

            End If

            If peticion IsNot Nothing AndAlso Not peticion.validarPostError Then
                respuesta.ValidacionesError = Nothing
            End If

            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Añadir el log en la respuesta del servicio
            respuesta.TiempoDeEjecucion = log.ToString()

            If identificadorLlamada IsNot Nothing Then
                Logeo.Log.Movimiento.Logger.FinalizaLlamada(identificadorLlamada, AccesoDatos.Util.ToXML(respuesta), respuesta.codigoResultado, respuesta.descripcionResultado, AccesoDatos.Util.ToXML(respuesta).GetHashCode)
            End If

            ' Respuesta
            Return respuesta

        End Function


#Region "[Validaciones]"

        ''' <summary>
        ''' Validar las informaciones basicas
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <remarks></remarks>
        Public Shared Sub validarToken(peticion As crearDocumentoFondos.Peticion)

            If peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_TOKEN_PETICION_OBLIGATORIA"))
            Else
                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
                        Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("NUEVO_SALDOS_TOKEN_TOKEN_INVALIDO"), peticion.tokenAcceso))
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Validar los atributos obrigatorios de la petición
        ''' So es llamado si el campo validarPostError es TRUE
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <param name="validaciones">Objeto de Respuesta con las validaciones</param>
        ''' <remarks></remarks>
        Private Shared Sub validarPeticion(peticion As crearDocumentoFondos.Peticion,
                                           ByRef validaciones As List(Of ValidacionError))

            If validaciones Is Nothing Then
                validaciones = New List(Of ValidacionError)
            End If

            If peticion Is Nothing Then
                validaciones.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = Traduzir("VAL101")})

            Else

                If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
                    If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
                        validaciones.Add(New ValidacionError With {.codigo = "VAL102", .descripcion = Traduzir("VAL102")})
                    End If
                End If

                If peticion.validarPostError Then

                    If peticion.movimiento Is Nothing Then
                        validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Movimiento", "Peticion")})

                    Else

                        If String.IsNullOrEmpty(peticion.movimiento.usuario) Then
                            validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Usuario", "Peticion.Movimiento")})
                        End If

                        If String.IsNullOrEmpty(peticion.movimiento.codigoExterno) Then
                            validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoExterno", "Peticion.Movimiento")})
                        End If

                        If String.IsNullOrEmpty(peticion.movimiento.codigoFormulario) Then
                            validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoFormulario", "Peticion.Movimiento")})
                        End If

                        If peticion.movimiento.origen Is Nothing Then
                            validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Origen", "Peticion.Movimiento")})

                        Else

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoCanal) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoCanal", "Peticion.Movimiento.Origen")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoSubCanal) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoSubCanal", "Peticion.Movimiento.Origen")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoDelegacion) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoDelegacion", "Peticion.Movimiento.Origen")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoPlanta) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoPlanta", "Peticion.Movimiento.Origen")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoSector) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoSector", "Peticion.Movimiento.Origen")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.origen.codigoCliente) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoCliente", "Peticion.Movimiento.Origen")})
                            End If

                        End If

                        If peticion.movimiento.destino IsNot Nothing Then

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoCanal) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoCanal", "Peticion.Movimiento.Destino")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoSubCanal) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoSubCanal", "Peticion.Movimiento.Destino")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoDelegacion) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoDelegacion", "Peticion.Movimiento.Destino")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoPlanta) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoPlanta", "Peticion.Movimiento.Destino")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoSector) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoSector", "Peticion.Movimiento.Destino")})
                            End If

                            If String.IsNullOrEmpty(peticion.movimiento.destino.codigoCliente) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "CodigoCliente", "Peticion.Movimiento.Destino")})
                            End If

                        End If

                        If peticion.movimiento.valores Is Nothing Then
                            validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento")})
                        Else

                            If (peticion.movimiento.valores.divisas Is Nothing OrElse peticion.movimiento.valores.divisas.Count = 0) AndAlso
                                (peticion.movimiento.valores.mediosDePago Is Nothing OrElse peticion.movimiento.valores.mediosDePago.Count = 0) Then
                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento")})
                            End If

                            If peticion.movimiento.valores.divisas IsNot Nothing AndAlso peticion.movimiento.valores.divisas.Count > 0 Then

                                For Each _divisa In peticion.movimiento.valores.divisas

                                    If String.IsNullOrEmpty(_divisa.codigoDivisa) Then
                                        validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento.Valores.Divisas")})
                                    End If

                                    If _divisa.denominaciones IsNot Nothing AndAlso _divisa.denominaciones.Count > 0 Then

                                        For Each _denominacion In _divisa.denominaciones

                                            If String.IsNullOrEmpty(_denominacion.codigoDenominacion) Then
                                                validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento.Valores.Divisas.Denominaciones")})
                                            End If

                                        Next

                                    End If

                                Next

                            End If

                            If peticion.movimiento.valores.mediosDePago IsNot Nothing AndAlso peticion.movimiento.valores.mediosDePago.Count > 0 Then

                                For Each _medioDePago In peticion.movimiento.valores.mediosDePago

                                    If String.IsNullOrEmpty(_medioDePago.codigoDivisa) Then
                                        validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento.Valores.MediosDePago")})
                                    End If

                                    If String.IsNullOrEmpty(_medioDePago.codigoMedioDePago) Then
                                        validaciones.Add(New ValidacionError With {.codigo = "VAL104", .descripcion = String.Format(Traduzir("VAL104"), "Valores", "Peticion.Movimiento.Valores.MediosDePago")})
                                    End If

                                Next

                            End If

                        End If

                    End If

                End If

            End If

        End Sub

        ''' <summary>
        ''' Revisar la integridad del documento
        ''' Es llamado cuando todos los campos fue informando, pero por algun motivo no fue posible ejecutar las tareas
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <param name="validaciones"></param>
        ''' <remarks></remarks>
        Private Shared Sub RevisarIntegridadDocumento(peticion As crearDocumentoFondos.Peticion,
                                                      documento As Clases.Documento,
                                                      ByRef validaciones As List(Of ValidacionError))

            If validaciones Is Nothing Then validaciones = New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            ' Parametro IAC 
            Dim listParametros As New List(Of String)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CODIGO_COMPROBANTE_BASADO_REGLAS)
            Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(peticion.movimiento.origen.codigoDelegacion,
                                                                                                                         Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS,
                                                                                                                         listParametros,
                                                                                                                         peticion.IdentificadorAjeno)
            If parametros IsNot Nothing Then
                Dim parametro As Clases.Parametro = parametros.Find(Function(x) x.codigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CODIGO_COMPROBANTE_BASADO_REGLAS)
                If parametro Is Nothing Then
                    validaciones.Add(New ValidacionError With {.codigo = "VAL100", .descripcion = String.Format(Traduzir("VAL100"), Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CODIGO_COMPROBANTE_BASADO_REGLAS)})
                End If
            Else
                validaciones.Add(New ValidacionError With {.codigo = "VAL100", .descripcion = String.Format(Traduzir("VAL100"), Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CODIGO_COMPROBANTE_BASADO_REGLAS)})
            End If

            ' Formulario
            Dim formulario As Clases.Formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.ObtenerFormularioPorCodigo_v2(peticion.movimiento.codigoFormulario)
            If formulario Is Nothing Then
                validaciones.Add(New ValidacionError With {.codigo = "VAL108", .descripcion = Traduzir("VAL108")})
            Else

                ' Caracteristicas
                If formulario.Caracteristicas Is Nothing OrElse
                   Not formulario.Caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.GestiondeFondos) Then
                    validaciones.Add(New ValidacionError With {.codigo = "VAL109", .descripcion = Traduzir("VAL109")})
                End If

                ' Accion Contable
                If formulario.AccionContable Is Nothing Then
                    validaciones.Add(New ValidacionError With {.codigo = "VAL110", .descripcion = Traduzir("VAL110")})
                End If

                ' Tipo Documento
                If formulario.TipoDocumento Is Nothing Then
                    validaciones.Add(New ValidacionError With {.codigo = "VAL111", .descripcion = Traduzir("VAL111")})
                End If

            End If

            Dim _identificadorCliente As String = ""
            Dim _identificadorSubCliente As String = ""
            Dim _identificadorPuntoServicio As String = ""
            Dim _identificadorCanal As String = ""
            Dim _identificadorSubCanal As String = ""
            Dim _identificadorDelegacion As String = ""
            Dim _identificadorPlanta As String = ""
            Dim _identificadorSector As String = ""

            AccesoDatos.GenesisSaldos.Cuenta.ValidarCuenta(peticion.IdentificadorAjeno,
                                                           peticion.movimiento.origen.codigoCliente,
                                                           peticion.movimiento.origen.codigoSubCliente,
                                                           peticion.movimiento.origen.codigoPuntoServicio,
                                                           peticion.movimiento.origen.codigoCanal,
                                                           peticion.movimiento.origen.codigoSubCanal,
                                                           peticion.movimiento.origen.codigoDelegacion,
                                                           peticion.movimiento.origen.codigoPlanta,
                                                           peticion.movimiento.origen.codigoSector,
                                                           Comon.Enumeradores.TipoSitio.Origen,
                                                           _identificadorCliente,
                                                           _identificadorSubCliente,
                                                           _identificadorPuntoServicio,
                                                           _identificadorCanal,
                                                           _identificadorSubCanal,
                                                           _identificadorDelegacion,
                                                           _identificadorPlanta,
                                                           _identificadorSector,
                                                           validaciones)

            If peticion.movimiento.destino IsNot Nothing Then
                AccesoDatos.GenesisSaldos.Cuenta.ValidarCuenta(peticion.IdentificadorAjeno,
                                                              peticion.movimiento.destino.codigoCliente,
                                                              peticion.movimiento.destino.codigoSubCliente,
                                                              peticion.movimiento.destino.codigoPuntoServicio,
                                                              peticion.movimiento.destino.codigoCanal,
                                                              peticion.movimiento.destino.codigoSubCanal,
                                                              peticion.movimiento.destino.codigoDelegacion,
                                                              peticion.movimiento.destino.codigoPlanta,
                                                              peticion.movimiento.destino.codigoSector,
                                                              Comon.Enumeradores.TipoSitio.Destino,
                                                              _identificadorCliente,
                                                              _identificadorSubCliente,
                                                              _identificadorPuntoServicio,
                                                              _identificadorCanal,
                                                              _identificadorSubCanal,
                                                              _identificadorDelegacion,
                                                              _identificadorPlanta,
                                                              _identificadorSector,
                                                              validaciones)
            End If

            If documento IsNot Nothing Then

                ' Es necesario tener valores
                If validaciones.Count = 0 AndAlso (documento.Divisas Is Nothing OrElse documento.Divisas.Count = 0) Then
                    validaciones.Add(New ValidacionError With {.codigo = "VAL114", .descripcion = Traduzir("VAL114")})
                End If

                validaciones.Add(New ValidacionError With {.codigo = "VAL115", .descripcion = String.Format(Traduzir("VAL115"), documento.Estado.ToString())})

            Else
                validaciones.Add(New ValidacionError With {.codigo = "VAL115", .descripcion = String.Format(Traduzir("VAL115"), Comon.Enumeradores.EstadoDocumento.Nuevo.ToString)})
            End If

        End Sub

        ''' <summary>
        ''' Define si la Exception es de InfraEstructura o de Aplicacion
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ValidarException(ex As Exception) As Boolean

            Dim msg As String = ex.ToString.ToUpper()

            If msg.Contains("TIMEOUT") OrElse
                msg.Contains("ORA-25408") OrElse
                msg.Contains("ORA-03113") OrElse
                msg.Contains("ORA-01033") Then
                ' Si ocurre un TIMEOUT, retorna un error de INTEGRACION_APLICACION
                Return False
            Else
                Return True
            End If

        End Function

#End Region

    End Class

End Namespace

