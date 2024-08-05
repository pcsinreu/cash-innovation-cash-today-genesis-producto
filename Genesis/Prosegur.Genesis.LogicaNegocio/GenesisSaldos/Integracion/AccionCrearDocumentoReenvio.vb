Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Transactions
Imports System.Threading.Tasks
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.Elemento

Namespace Integracion

    Public Class AccionCrearDocumentoReenvio

        Public Shared Function ejecutar(peticion As crearDocumentoReenvio.Peticion) As crearDocumentoReenvio.Respuesta

            ' Variables para log de tiempo, ayudar en la analise de performance
            Dim TiempoInicial As DateTime = Now
            Dim TiempoParcial As DateTime = Now
            Dim log As New StringBuilder

            ' Inicializa objeto de respuesta
            Dim respuesta As New crearDocumentoReenvio.Respuesta
            respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO
            respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_INTEGRACION_SEM_ERRO
            respuesta.ValidacionesError = New List(Of ValidacionError)

            ' Inicializa objeto Documento
            Dim _grupoDocumentos As Clases.GrupoDocumentos = Nothing

            Try

                ' Validar el token
                validarToken(peticion)

            Catch ex As Excepcion.NegocioExcepcion

                respuesta.ValidacionesError.Add(New ValidacionError With {.codigo = "VAL101", .descripcion = ex.Message})
                ' Respuesta defecto para Excepciones de Negocio
                respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_FUNCIONAL
                respuesta.descripcionResultado = Excepcion.Constantes.CONST_DESCRICION_ERRO_INTEGRACION_FUNCIONAL

            End Try

            If respuesta.ValidacionesError.Count = 0 Then

                Try

                    ' *** Inicio del primero acto: Validaciones (indispensables) y la creacion del Documento
                    TiempoParcial = Now

                    ' Crea grupo documento
                    crearGrupoDocumentos(peticion, _grupoDocumentos, respuesta.ValidacionesError)

                    If respuesta.ValidacionesError IsNot Nothing AndAlso respuesta.ValidacionesError.Count > 0 Then
                        'respuesta.ValidacionesError = Nothing
                        Throw New Excepcion.NegocioExcepcion("ValidacionesError")
                    End If

                    ' Grabar el log del primero acto
                    log.AppendLine("Tiempo de Crear Grupo Documentos: " & Now.Subtract(TiempoParcial).ToString() & "; ")

                    ' Si fue posible crear documento 
                    If _grupoDocumentos IsNot Nothing Then

                        ' *** Inicio del segundo acto: Guardar en nuevo documento
                        TiempoParcial = Now

                        AccesoDatos.GenesisSaldos.GrupoDocumentos.GrabarGrupoDocumentoReenvio(_grupoDocumentos,
                                                                                              peticion.movimiento.gestionaPorBulto,
                                                                                              recuperar_caracteristicas_integracion(peticion.movimiento.tipoIntegracion),
                                                                                              If(peticion.movimiento.accion <> Enumeradores.Accion.EnCurso, True, False),
                                                                                              If(peticion.movimiento.accion = Enumeradores.Accion.Aceptado, True, False),
                                                                                              log)

                        ' Grabar el log del segundo acto
                        log.AppendLine("Tiempo para Guardar Documento: " & Now.Subtract(TiempoParcial).ToString() & "; " & vbNewLine)

                        ' Graba en el objecto de respuesta las informanciones del nuevo documento
                        respuesta.codigoComprobante = _grupoDocumentos.CodigoComprobante

                        ' *** Inicio del: integracion
                        TiempoParcial = Now

                        'Dim grupoDocumento As Clases.GrupoDocumentos = LogicaNegocio.GenesisSaldos.MaestroGrupoDocumentos.recuperarGrupoDocumentos(_grupoDocumentos.Identificador, peticion.movimiento.usuario, Nothing)
                        'If grupoDocumento IsNot Nothing AndAlso grupoDocumento.Documentos IsNot Nothing AndAlso grupoDocumento.Documentos.Count > 0 Then
                        '    LogicaNegocio.GenesisSaldos.MaestroDocumentos.EjecutarIntegraciones(grupoDocumento.Documentos)
                        'End If

                        ' Grabar el log
                        log.AppendLine("Tiempo para integracion: " & Now.Subtract(TiempoParcial).ToString() & "; " & vbNewLine)
                    End If

                Catch ex As Exception
                    Util.TratarErroBugsnag(ex)

                    respuesta.codigoResultado = Excepcion.Constantes.CONST_CODIGO_ERROR_INTEGRACION_APLICACION
                    respuesta.descripcionResultado = ex.Message

                End Try

            End If
            ' Graba en el LOG el tiempo total de la ejecucion del proceso
            log.AppendLine("Tiempo total: " & Now.Subtract(TiempoInicial).ToString() & vbNewLine & "; ")

            ' Añadir el log en la respuesta del servicio
            respuesta.TiempoDeEjecucion = log.ToString()

            ' Respuesta
            Return respuesta

        End Function

#Region "[Validaciones]"

        ''' <summary>
        ''' Validar las informaciones basicas
        ''' </summary>
        ''' <param name="peticion">Petición del Servicio</param>
        ''' <remarks></remarks>
        Public Shared Sub validarToken(peticion As crearDocumentoReenvio.Peticion)

            'If peticion Is Nothing Then
            '    Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_TOKEN_PETICION_OBLIGATORIA"))
            'Else
            '    If AppSettings("Token") IsNot Nothing AndAlso Not String.IsNullOrEmpty(AppSettings("Token")) Then
            '        If String.IsNullOrEmpty(peticion.tokenAcceso) OrElse Not AppSettings("Token").Equals(peticion.tokenAcceso) Then
            '            Throw New Excepcion.NegocioExcepcion(String.Format(Traduzir("NUEVO_SALDOS_TOKEN_TOKEN_INVALIDO"), peticion.tokenAcceso))
            '        End If
            '    End If
            'End If
        End Sub

        ''' <summary>
        ''' Define si la Exception es de InfraEstructura o de Aplicacion
        ''' </summary>
        ''' <param name="ex"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ValidarException(ex As Exception) As Boolean
            If ex.Message.ToUpper() = "TIMEOUT" Then
                ' Si ocurre un TIMEOUT, retorna un error de InfraEstructura
                Return True
            Else
                Return False
            End If
        End Function

#End Region

#Region "[Crear Grupo de Documentos]"

        ''' <summary>
        ''' Metodo responsable por Crear el grupo de los documento
        ''' </summary>
        ''' <remarks></remarks>
        Private Shared Sub crearGrupoDocumentos(_peticion As crearDocumentoReenvio.Peticion,
                                                ByRef _grupoDocumentos As Clases.GrupoDocumentos,
                                                ByRef _validaciones As List(Of ValidacionError))

            Try

                Dim gestionaPorBulto As Boolean
                If _peticion.movimiento.gestionaPorBulto Is Nothing Then
                    gestionaPorBulto = AccesoDatos.Genesis.Remesa.verificaGestionPorBulto(_peticion.movimiento.remesas(0).identificador, _peticion.movimiento.remesas(0).codigoExterno)
                Else
                    gestionaPorBulto = _peticion.movimiento.gestionaPorBulto
                End If

                _grupoDocumentos = New Clases.GrupoDocumentos

                ' Preenche las informaciones necesarias, de acuerdo con la peticion recebida
                With _grupoDocumentos

                    .Identificador = System.Guid.NewGuid.ToString()
                    .Estado = Comon.Enumeradores.EstadoDocumento.EnCurso
                    .UsuarioCreacion = _peticion.movimiento.usuario
                    .UsuarioModificacion = _peticion.movimiento.usuario
                    .Documentos = New ObservableCollection(Of Clases.Documento)

                    ' FormularioReenvio
                    .Formulario = New Clases.Formulario
                    .Formulario.Caracteristicas = New List(Of Comon.Enumeradores.CaracteristicaFormulario)
                    .Formulario.Caracteristicas.Add(Comon.Enumeradores.CaracteristicaFormulario.Reenvios)
                    .Formulario.Caracteristicas.Add(recuperar_caracteristicas_integracion(_peticion.movimiento.tipoIntegracion))

                End With

                ' Documentos
                For Each _remesaPeticion In _peticion.movimiento.remesas

                    ' Documento
                    Dim _documento As New Clases.Documento
                    With _documento
                        .Identificador = System.Guid.NewGuid.ToString()
                        .Estado = Comon.Enumeradores.EstadoDocumento.EnCurso
                        .UsuarioCreacion = _peticion.movimiento.usuario
                        .UsuarioModificacion = _peticion.movimiento.usuario
                        .FechaHoraGestion = DateTime.UtcNow
                        .Formulario = _grupoDocumentos.Formulario
                        .CuentaOrigen = New Clases.Cuenta With {
                             .Identificador = _remesaPeticion.cuentaOrigen.identificador,
                             .Cliente = New Clases.Cliente With {.Codigo = _remesaPeticion.cuentaOrigen.codigoCliente, .Identificador = _remesaPeticion.cuentaOrigen.identificadorCliente},
                             .SubCliente = New Clases.SubCliente With {.Codigo = _remesaPeticion.cuentaOrigen.codigoSubCliente, .Identificador = _remesaPeticion.cuentaOrigen.identificadorSubCliente},
                             .PuntoServicio = New Clases.PuntoServicio With {.Codigo = _remesaPeticion.cuentaOrigen.codigoPuntoServicio, .Identificador = _remesaPeticion.cuentaOrigen.identificadorPuntoServicio},
                             .Canal = New Clases.Canal With {.Codigo = _remesaPeticion.cuentaOrigen.codigoCanal, .Identificador = _remesaPeticion.cuentaOrigen.identificadorCanal},
                             .SubCanal = New Clases.SubCanal With {.Codigo = _remesaPeticion.cuentaOrigen.codigoSubCanal, .Identificador = _remesaPeticion.cuentaOrigen.identificadorSubCanal},
                             .Sector = New Clases.Sector With {.Codigo = _remesaPeticion.cuentaOrigen.codigoSector, .Identificador = _remesaPeticion.cuentaOrigen.identificadorSector,
                             .Delegacion = New Clases.Delegacion With {.Codigo = _remesaPeticion.cuentaOrigen.codigoDelegacion, .Identificador = _remesaPeticion.cuentaOrigen.identificadorDelegacion},
                             .Planta = New Clases.Planta With {.Codigo = _remesaPeticion.cuentaOrigen.codigoPlanta, .Identificador = _remesaPeticion.cuentaOrigen.identificadorPlanta}}}
                        .CuentaDestino = New Clases.Cuenta With {
                             .Identificador = _remesaPeticion.cuentaDestino.identificador,
                             .Cliente = New Clases.Cliente With {.Codigo = _remesaPeticion.cuentaDestino.codigoCliente, .Identificador = _remesaPeticion.cuentaDestino.identificadorCliente},
                             .SubCliente = New Clases.SubCliente With {.Codigo = _remesaPeticion.cuentaDestino.codigoSubCliente, .Identificador = _remesaPeticion.cuentaDestino.identificadorSubCliente},
                             .PuntoServicio = New Clases.PuntoServicio With {.Codigo = _remesaPeticion.cuentaDestino.codigoPuntoServicio, .Identificador = _remesaPeticion.cuentaDestino.identificadorPuntoServicio},
                             .Canal = New Clases.Canal With {.Codigo = _remesaPeticion.cuentaDestino.codigoCanal, .Identificador = _remesaPeticion.cuentaDestino.identificadorCanal},
                             .SubCanal = New Clases.SubCanal With {.Codigo = _remesaPeticion.cuentaDestino.codigoSubCanal, .Identificador = _remesaPeticion.cuentaDestino.identificadorSubCanal},
                             .Sector = New Clases.Sector With {.Codigo = _remesaPeticion.cuentaDestino.codigoSector, .Identificador = _remesaPeticion.cuentaDestino.identificadorSector,
                             .Delegacion = New Clases.Delegacion With {.Codigo = _remesaPeticion.cuentaDestino.codigoDelegacion, .Identificador = _remesaPeticion.cuentaDestino.identificadorDelegacion},
                             .Planta = New Clases.Planta With {.Codigo = _remesaPeticion.cuentaDestino.codigoPlanta, .Identificador = _remesaPeticion.cuentaDestino.identificadorPlanta}}}
                    End With

                    ' Remesa
                    Dim _remesa As New Clases.Remesa
                    With _remesa
                        .Identificador = _remesaPeticion.identificador
                        .CodigoExterno = _remesaPeticion.codigoExterno
                        .Rowver = _remesaPeticion.rowver
                        .Bultos = New ObservableCollection(Of Clases.Bulto)
                    End With


                    If _peticion.movimiento.gestionaPorBulto Then

                        For Each _bultoPeticion In _remesaPeticion.bultos

                            Dim _bulto As New Clases.Bulto
                            _bulto.Identificador = _bultoPeticion.identificador
                            If Not String.IsNullOrEmpty(_bultoPeticion.codigoExterno) Then
                                _bulto.Precintos = New ObservableCollection(Of String)
                                _bulto.Precintos.Add(_bultoPeticion.codigoExterno)
                                _bulto.Rowver = _bultoPeticion.rowver
                            End If

                            Dim _documentoBulto As Clases.Documento = _documento.Clonar
                            Dim _remesaBulto As Clases.Remesa = _remesa.Clonar
                            _remesaBulto.Bultos.Add(_bulto)
                            _documentoBulto.Elemento = _remesaBulto
                            _grupoDocumentos.Documentos.Add(_documentoBulto)

                        Next

                    Else

                        If _remesaPeticion.bultos IsNot Nothing AndAlso _remesaPeticion.bultos.Count > 0 Then

                            ' Gestion por Remesas
                            For Each _bultoPeticion In _remesaPeticion.bultos

                                Dim _bulto As New Clases.Bulto
                                _bulto.Identificador = _bultoPeticion.identificador
                                _bulto.Rowver = _bultoPeticion.rowver
                                If Not String.IsNullOrEmpty(_bultoPeticion.codigoExterno) Then
                                    _bulto.Precintos = New ObservableCollection(Of String)
                                    _bulto.Precintos.Add(_bultoPeticion.codigoExterno)
                                End If
                                _remesa.Bultos.Add(_bulto)

                            Next

                        End If

                        _documento.Elemento = _remesa
                        _grupoDocumentos.Documentos.Add(_documento)

                    End If

                Next

            Catch ex As Exception
                Throw
            End Try

        End Sub

        Private Shared Function recuperar_caracteristicas_integracion(integracion As Comon.Enumeradores.TipoIntegracion) As Comon.Enumeradores.CaracteristicaFormulario
            Select Case integracion
                Case Comon.Enumeradores.TipoIntegracion.Conteo
                    Return Comon.Enumeradores.CaracteristicaFormulario.IntegracionConteo
                Case Comon.Enumeradores.TipoIntegracion.RecepcionEnvio
                    Return Comon.Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio
                Case Comon.Enumeradores.TipoIntegracion.Salidas
                    Return Comon.Enumeradores.CaracteristicaFormulario.IntegracionSalidas
                Case Else
                    Return Comon.Enumeradores.CaracteristicaFormulario.IntegracionLegado
            End Select
        End Function

#End Region

#Region "[Servicio Antiguo]"

        Public Shared Function ejecutarReenvioAntiguo(peticion As Reenvio.ReenvioPeticion) As Reenvio.ReenvioRespuesta

            Dim respuesta As New Reenvio.ReenvioRespuesta()

            Try

                Validar(peticion)

                If peticion.Accion = Reenvio.ReenvioPeticionAcciones.Rechazar Then

                    ' TO DO: Rechazar

                Else

                    Dim peticionNuevo As New crearDocumentoReenvio.Peticion
                    peticionNuevo.validarPostError = False
                    peticionNuevo.movimiento = New crearDocumentoReenvio.Movimiento
                    peticionNuevo.movimiento.tipoIntegracion = Comon.Enumeradores.TipoIntegracion.Legado
                    peticionNuevo.movimiento.usuario = peticion.CodigoUsuario
                    peticionNuevo.movimiento.remesas = New List(Of crearDocumentoReenvio.Remesa)
                    peticionNuevo.movimiento.gestionaPorBulto = False

                    Select Case peticion.Accion
                        Case Reenvio.ReenvioPeticionAcciones.Crear
                            peticionNuevo.movimiento.accion = Enumeradores.Accion.Confirmado
                        Case Reenvio.ReenvioPeticionAcciones.Aceptar
                            peticionNuevo.movimiento.accion = Enumeradores.Accion.Aceptado
                        Case Else
                            peticionNuevo.movimiento.accion = Enumeradores.Accion.EnCurso
                    End Select

                    For Each elemento In peticion.Elementos.Where(Function(e) e IsNot Nothing)

                        If TypeOf elemento Is Reenvio.Remesa Then

                            Dim remesaPeticion As Reenvio.Remesa = DirectCast(elemento, Reenvio.Remesa)
                            Dim remesa As New crearDocumentoReenvio.Remesa

                            remesa.codigoExterno = remesaPeticion.CodigoExterno
                            remesa.cuentaOrigen = New crearDocumentoReenvio.Cuenta With {.codigoSector = peticion.Origen.CodigoSector,
                                                                                         .codigoPlanta = peticion.Origen.CodigoPlanta,
                                                                                         .codigoDelegacion = peticion.Origen.CodigoDelegacion}
                            remesa.cuentaDestino = New crearDocumentoReenvio.Cuenta With {.codigoSector = peticion.Destino.CodigoSector,
                                                                                          .codigoPlanta = peticion.Destino.CodigoPlanta,
                                                                                          .codigoDelegacion = peticion.Destino.CodigoDelegacion}
                            peticionNuevo.movimiento.remesas.Add(remesa)


                            respuesta.ReenviosOK = New Reenvio.ReenviosOK()
                            respuesta.ReenviosOK.Elementos = New List(Of Reenvio.Elemento)
                            respuesta.ReenviosOK.Elementos.Add(New Reenvio.Remesa With {.CodigoExterno = remesa.codigoExterno})

                        End If

                    Next

                    If peticionNuevo IsNot Nothing AndAlso peticionNuevo.movimiento IsNot Nothing AndAlso peticionNuevo.movimiento.remesas IsNot Nothing AndAlso peticionNuevo.movimiento.remesas.Count > 0 Then
                        Dim respuestaNuevo As crearDocumentoReenvio.Respuesta = ejecutar(peticionNuevo)

                        If respuestaNuevo.codigoResultado <> Excepcion.Constantes.CONST_CODIGO_INTEGRACION_SEM_ERRO Then
                            Throw New Excepcion.NegocioExcepcion(respuestaNuevo.descripcionResultado)
                        End If

                    Else

                        For Each elemento In peticion.Elementos.Where(Function(e) e IsNot Nothing)

                            respuesta.ReenviosER = New Reenvio.ReenviosER()
                            respuesta.ReenviosER.Elementos = New List(Of Reenvio.ElementoError)
                            Dim _elemento As Reenvio.Elemento = Nothing
                            If TypeOf elemento Is Reenvio.Remesa Then
                                _elemento = New Reenvio.Remesa With {.CodigoExterno = DirectCast(elemento, Reenvio.Remesa).CodigoExterno}
                            End If
                            respuesta.ReenviosER.Elementos.Add(New Reenvio.ElementoError() With {.Elemento = _elemento,
                                                                           .DatosError = New Reenvio.ReenvioError() With {
                                                                           .Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                                           .Descripcion = String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_ERROR_CREACION_DOCUMENTO"), "No es una Remesa.")}})

                        Next

                    End If


                End If

            Catch ex As Excepcion.NegocioExcepcion
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                respuesta.MensajeError = ex.Descricao

                respuesta.ReenviosER = New Reenvio.ReenviosER()
                respuesta.ReenviosER.Elementos = New List(Of Reenvio.ElementoError)

                If respuesta.ReenviosOK IsNot Nothing AndAlso respuesta.ReenviosOK.Elementos IsNot Nothing AndAlso respuesta.ReenviosOK.Elementos.Count > 0 Then
                    For Each r In respuesta.ReenviosOK.Elementos
                        respuesta.ReenviosER.Elementos.Add(New Reenvio.ElementoError() With {.Elemento = r,
                                                               .DatosError = New Reenvio.ReenvioError() With {
                                                               .Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                               .Descripcion = String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_ERROR_CREACION_DOCUMENTO"), ex.Message)}})
                    Next
                Else
                    respuesta.ReenviosER.Elementos.Add(New Reenvio.ElementoError() With {.Elemento = Nothing,
                                               .DatosError = New Reenvio.ReenvioError() With {
                                               .Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                               .Descripcion = String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_ERROR_CREACION_DOCUMENTO"), "No es una Remesa.")}})
                End If

                respuesta.ReenviosOK = Nothing

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                respuesta.MensajeError = ex.ToString()

                respuesta.ReenviosER = New Reenvio.ReenviosER()
                respuesta.ReenviosER.Elementos = New List(Of Reenvio.ElementoError)

                If respuesta.ReenviosOK IsNot Nothing AndAlso respuesta.ReenviosOK.Elementos IsNot Nothing AndAlso respuesta.ReenviosOK.Elementos.Count > 0 Then
                    For Each r In respuesta.ReenviosOK.Elementos
                        respuesta.ReenviosER.Elementos.Add(New Reenvio.ElementoError() With {.Elemento = r, _
                                                               .DatosError = New Reenvio.ReenvioError() With { _
                                                               .Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                                               .Descripcion = String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_ERROR_CREACION_DOCUMENTO"), ex.ToString)}})
                    Next
                Else
                    respuesta.ReenviosER.Elementos.Add(New Reenvio.ElementoError() With {.Elemento = Nothing, _
                                               .DatosError = New Reenvio.ReenvioError() With { _
                                               .Codigo = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, _
                                               .Descripcion = String.Format(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_ERROR_CREACION_DOCUMENTO"), "No es una Remesa.")}})
                End If

                respuesta.ReenviosOK = Nothing

            End Try

            Return respuesta

        End Function

        Public Shared Sub Validar(peticion As Reenvio.ReenvioPeticion)

            ' valida o token passado na petição
            Util.VerificaInformacionesToken(peticion)

            ' valida se a petição existe
            If peticion Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_PETICION_OBLIGATORIA"))
            End If

            ' verifca se foram informados elementos ou documentos para que seja feito reenvios
            If peticion.Elementos Is Nothing OrElse peticion.Elementos.Count() = 0 Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_DATOS_ELEMENTOS_DOCUMENTOS_OBLIGATORIOS"))
            ElseIf peticion.Elementos.FirstOrDefault(Function(x) TypeOf x Is Reenvio.Remesa) Is Nothing Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_DATOS_ELEMENTOS_DOCUMENTOS_OBLIGATORIOS"))
            End If

            ' verifica se os dados de origem e destino foram informados
            If peticion.Origen Is Nothing OrElse peticion.Destino Is Nothing OrElse String.IsNullOrEmpty(peticion.Origen.CodigoDelegacion) OrElse String.IsNullOrEmpty(peticion.Origen.CodigoPlanta) OrElse String.IsNullOrEmpty(peticion.Origen.CodigoSector) OrElse String.IsNullOrEmpty(peticion.Destino.CodigoDelegacion) OrElse String.IsNullOrEmpty(peticion.Destino.CodigoPlanta) OrElse String.IsNullOrEmpty(peticion.Destino.CodigoSector) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_DATOS_ORIGEN_DESTINO_OBLIGATORIOS"))
            End If

            ' verifica se os dados de origem e destino sao iguais, se são, não deve permitir fazer um reenvio para uma origem e destino iguais
            If peticion.Origen.CodigoDelegacion.Equals(peticion.Destino.CodigoDelegacion) AndAlso peticion.Origen.CodigoPlanta.Equals(peticion.Destino.CodigoPlanta) AndAlso peticion.Origen.CodigoSector.Equals(peticion.Destino.CodigoSector) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_DATOS_ORIGEN_DESTINO_IGUALES"))
            End If

            ' verifica se o movimento de origem e destino é de um tipo permito, ou seja, os dados de delegaçao e planta iguais e os sectores diferentes ou os sectores iguais e os dados delegação e planta diferentes
            If (Not peticion.Origen.CodigoDelegacion.Equals(peticion.Destino.CodigoDelegacion) OrElse Not peticion.Origen.CodigoPlanta.Equals(peticion.Destino.CodigoPlanta)) AndAlso Not peticion.Origen.CodigoSector.Equals(peticion.Destino.CodigoSector) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("NUEVO_SALDOS_SERVICIO_REENVIO_DATOS_ORIGEN_DESTINO_INCOMPATIBLES"))
            End If

        End Sub


#End Region

    End Class

End Namespace

