Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Global.Saldos.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Global.GesEfectivo
Imports Prosegur.Genesis.Comon
Imports System.Configuration

Namespace Integracion

    Public Class AccionIngresoRemesasNuevo

        ''' <summary>
        ''' Realiza o Ingresso de Remessas.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [maoliveira]  17/09/2013  criado
        ''' </history>
        Public Shared Function Ejecutar(objPeticion As IngresoRemesasNuevo.Peticion) As IngresoRemesasNuevo.Respuesta

            Dim TempoInicial As DateTime = Now

            ' Inicializa objeto de resposta
            Dim objRespuesta As New IngresoRemesasNuevo.Respuesta
            objRespuesta.RemesasError = New IngresoRemesasNuevo.RemesasError
            objRespuesta.RemesasOK = New IngresoRemesasNuevo.RemesasOk
            objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
            objRespuesta.MensajeError = String.Empty

            Try

                ' Atribui as remesas aos documentos de Ingreso ou de Substituición
                Dim objDocumentos As ObservableCollection(Of Clases.Documento) = CrearDocumentos(objPeticion, objRespuesta)

                If objDocumentos IsNot Nothing AndAlso objDocumentos.Count > 0 Then

                    For Each doc As Comon.Clases.Documento In objDocumentos

                        Try

                            LogicaNegocio.GenesisSaldos.MaestroDocumentos.GuardarDocumento(doc, True, True, If(doc.Formulario.Caracteristicas.Contains(Comon.Enumeradores.CaracteristicaFormulario.GestiondeBultos), True, False), Comon.Enumeradores.CaracteristicaFormulario.IntegracionLegado, Nothing)

                            ' Recupera o identificador do documento da remessa externa
                            objRespuesta.RemesasOK.FirstOrDefault(Function(f) f.IdentificadorRemesaOriginal = DirectCast(doc.Elemento, Comon.Clases.Remesa).IdentificadorExterno).IdentificadorDocumentoGenerado = doc.Identificador

                        Catch ex As Exception
                            Util.TratarErroBugsnag(ex)
                            objRespuesta.RemesasError.Add(New IngresoRemesasNuevo.RemesaError() With {.IdRemesaError = DirectCast(doc.Elemento, Comon.Clases.Remesa).IdentificadorExterno, .DescRemesaError = ex.Message})
                            objRespuesta.RemesasOK.RemoveAll(Function(x) x.IdentificadorRemesaOriginal = DirectCast(doc.Elemento, Comon.Clases.Remesa).IdentificadorExterno)
                        End Try

                    Next

                    objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                    objRespuesta.MensajeError = String.Empty

                End If

            Catch ex As Excepcion.NegocioExcepcion
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                objRespuesta.MensajeError = ex.Descricao

            Catch ex As Exception
                Util.TratarErroBugsnag(ex)
                objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT
                objRespuesta.MensajeError = ex.ToString()

            End Try

            Dim TempoFinal As TimeSpan = Now.Subtract(TempoInicial)
            objRespuesta.Tiempo = TempoFinal.ToString()
            Return objRespuesta

        End Function

        Private Shared Sub obtenerParametrosIAC(codigoDelegacion As String,
                                                ByRef CrearCuentaAutomatico As Boolean,
                                                ByRef TrabajaPorBulto As Boolean,
                                                ByRef ConfigNivelDetalle As Comon.Enumeradores.ConfiguracionNivelSaldos)

            Dim listParametros As New List(Of String)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO)
            listParametros.Add(Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CONFIG_NIVEL_DETALLE)
            Dim parametros As List(Of Clases.Parametro) = AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(codigoDelegacion, Comon.Constantes.CODIGO_APLICACION_GENESIS_SALDOS, listParametros)

            If parametros IsNot Nothing Then

                For Each parametro In parametros
                    If parametro.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CREAR_CONFIGURACION_NIVEL_SALDO Then
                        CrearCuentaAutomatico = (parametro.ValorParametro <> "0")
                    ElseIf parametro.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_TRABAJA_POR_BULTO Then
                        TrabajaPorBulto = (parametro.ValorParametro <> "0")
                    ElseIf parametro.CodigoParametro = Comon.Constantes.CODIGO_PARAMETRO_IAC_PAIS_CONFIG_NIVEL_DETALLE Then
                        Select Case parametro.ValorParametro
                            Case "T"
                                ConfigNivelDetalle = Comon.Enumeradores.ConfiguracionNivelSaldos.Total
                            Case "D"
                                ConfigNivelDetalle = Comon.Enumeradores.ConfiguracionNivelSaldos.Detalle
                            Case Else
                                ConfigNivelDetalle = Comon.Enumeradores.ConfiguracionNivelSaldos.Ambos
                        End Select
                    End If
                Next
            End If

        End Sub

        ''' <summary>
        ''' Crea los documentos defectos
        ''' </summary>
        ''' <param name="documentoDefectoIngreso"></param>
        ''' <param name="documentoDefectoSustitucion"></param>
        ''' <param name="TrabajaPorBulto"></param>
        ''' <param name="EsIntegracionRecepcionEnvio"></param>
        ''' <remarks></remarks>
        Private Shared Sub CrearDocumentosDefectos(ByRef documentoDefectoIngreso As Clases.Documento, _
                                                   ByRef documentoDefectoSustitucion As Clases.Documento, _
                                                   TrabajaPorBulto As Boolean, _
                                                   EsIntegracionRecepcionEnvio As Boolean)


            Dim CaracteristicasIntegracion As Comon.Enumeradores.CaracteristicaFormulario

            If EsIntegracionRecepcionEnvio Then
                CaracteristicasIntegracion = Comon.Enumeradores.CaracteristicaFormulario.IntegracionRecepcionEnvio
            Else
                CaracteristicasIntegracion = Comon.Enumeradores.CaracteristicaFormulario.IntegracionLegado
            End If

            ' Recupera lista de formulários posiveis
            Dim _FormularioIngreso As Clases.Formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.obtenerFormularioDeAltas(TrabajaPorBulto, "", CaracteristicasIntegracion)
            Dim _FormularioSustitucion As Clases.Formulario = LogicaNegocio.GenesisSaldos.MaestroFormularios.obtenerFormularioDeSustitucion(TrabajaPorBulto, "", CaracteristicasIntegracion)

            documentoDefectoIngreso = LogicaNegocio.GenesisSaldos.MaestroDocumentos.CrearDocumento(_FormularioIngreso, Nothing, False)
            documentoDefectoSustitucion = LogicaNegocio.GenesisSaldos.MaestroDocumentos.CrearDocumento(_FormularioSustitucion, Nothing, False)

        End Sub

        ''' <summary>
        ''' Busca Processo do Iac e armazena numa coleção de procesos
        ''' </summary>
        ''' <param name="objRemesas"></param>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 26/05/2009 - Criado
        ''' </history>
        Private Shared Function ObtenerProcesos(objRemesas As IngresoRemesasNuevo.Remesas) As ObservableCollection(Of GetProceso.Proceso)

            Dim objProcesos As New ObservableCollection(Of GetProceso.Proceso)

            'Percorre todos os bultos de todas as remessas recebidas buscando por dados distintos de filiação
            'para o preenchimento do contrato da operação GetProceso do IAC
            For Each DatosFiliacion In (From Remesa In objRemesas, Bulto In Remesa.Bultos
                                        Select Remesa.CodigoDelegacion, Bulto.CodigoCliente, Bulto.CodigoSubCliente,
                                        Bulto.CodigoPuntoServicio, Bulto.CodigoSubCanal).Distinct

                'Istancia objeto do tipo GetProceso.Peticion
                Dim objPeticionGetProcesoIac As New GetProceso.Peticion

                'Popula objeto peticion da operação GetProceso do IAC
                objPeticionGetProcesoIac.CodigoCliente = DatosFiliacion.CodigoCliente
                objPeticionGetProcesoIac.CodigoSubcliente = DatosFiliacion.CodigoSubCliente
                objPeticionGetProcesoIac.CodigoPuntoServicio = DatosFiliacion.CodigoPuntoServicio
                objPeticionGetProcesoIac.CodigoSubcanal = DatosFiliacion.CodigoSubCanal
                objPeticionGetProcesoIac.CodigoDelegacion = DatosFiliacion.CodigoDelegacion

                'Invoca operação GetProceso do IAC e armazena seu retorno
                Dim objNegocioIAC As New IAC.LogicaNegocio.AccionIntegracion
                Dim objRespuestasGetProcesoIac As GetProceso.Respuesta = objNegocioIAC.GetProceso(objPeticionGetProcesoIac)

                If objRespuestasGetProcesoIac IsNot Nothing AndAlso objRespuestasGetProcesoIac.Proceso IsNot Nothing Then
                    objProcesos.Add(objRespuestasGetProcesoIac.Proceso)
                End If
            Next

            Return objProcesos

        End Function

        ''' <summary>
        ''' Retorna se os Bultos da remesas são validos
        ''' </summary>
        ''' <param name="objRemesa">Remesa Corrente</param>
        ''' <returns></returns>
        ''' <history>
        ''' </history>
        Private Shared Function ValidarRemesa(objRemesa As IngresoRemesasNuevo.Remesa, ByRef objRespuesta As IngresoRemesasNuevo.Respuesta) As Boolean

            If objRemesa.Bultos IsNot Nothing AndAlso objRemesa.Bultos.Count > 0 Then

                Dim objBultos = (From b In objRemesa.Bultos Select b.CodigoCliente, b.CodigoSubCliente, b.CodigoPuntoServicio, b.CodigoCanal, b.CodigoSubCanal).Distinct

                ' Se o resultado da consulta anterior tiver apenas um registro, consideramos que todos os bultos tem a mesma conta e retornamos True
                If objBultos IsNot Nothing AndAlso objBultos.Count = 1 Then
                    Return True
                Else
                    ' Erro ao validar cliente e canal dos bultos
                    objRespuesta.RemesasError.Add(New IngresoRemesasNuevo.RemesaError With {
                                                  .IdRemesaError = objRemesa.IdRemesaOrigen,
                                                  .DescRemesaError = Traduzir("02_msg_error_malotes_cliente_canal")})
                End If
            Else

                ' Erro ao validar bulto
                objRespuesta.RemesasError.Add(New IngresoRemesasNuevo.RemesaError With {
                                                          .IdRemesaError = objRemesa.IdRemesaOrigen,
                                                          .DescRemesaError = Traduzir("02_msg_error_malotes")})
            End If

            Return False

        End Function

        Public Shared Function CrearDocumentos(objPeticion As IngresoRemesasNuevo.Peticion, _
                                               ByRef objRespuesta As IngresoRemesasNuevo.Respuesta) As ObservableCollection(Of Comon.Clases.Documento)

            ' Variaveis
            Dim objDocumentos As New ObservableCollection(Of Clases.Documento)
            Dim objCuentasPosibles As New ObservableCollection(Of Clases.Cuenta)
            Dim objPeticionRemesas As List(Of IngresoRemesasNuevo.Remesa) = objPeticion.Remesas
            Dim CodigoUsuario As String = objPeticion.CodigoUsuario
            Dim CodigoDelegacion As String = objPeticion.Remesas.First.CodigoDelegacion

            ' Busca Parametros IAC
            Dim CrearCuentaAutomatico As Boolean '= ObtenerParametroCrearCuentaAutomatico(CodigoDelegacion)
            Dim TrabajaPorBulto As Boolean '= ObtenerParametroTrabajaPorBulto(CodigoDelegacion)
            Dim ConfigNivelDetalle As Comon.Enumeradores.ConfiguracionNivelSaldos = Comon.Enumeradores.ConfiguracionNivelSaldos.Ambos
            obtenerParametrosIAC(CodigoDelegacion, CrearCuentaAutomatico, TrabajaPorBulto, ConfigNivelDetalle)

            ' Crea documentos defectos de Ingreso e Substituición
            Dim documentoDefectoIngreso As New Clases.Documento
            Dim documentoDefectoSustitucion As New Clases.Documento
            CrearDocumentosDefectos(documentoDefectoIngreso, documentoDefectoSustitucion, TrabajaPorBulto, objPeticion.EsIntegracionRecepcionEnvio)

            ' Busca todos os processos IAC utilizados
            Dim objColeccionProcesos As ObservableCollection(Of GetProceso.Proceso) = ObtenerProcesos(objPeticionRemesas)

            ' Verifica se existe Remesas para processar
            If objPeticionRemesas IsNot Nothing AndAlso objPeticionRemesas.Count > 0 Then

                ' Verificar si documento existe
                Dim peticionAltas As New Clases.Transferencias.FiltroDocumentosAltas
                peticionAltas.remesas = New List(Of Clases.Transferencias.FiltroRemesaAltas)
                For Each objPeticionRemesa As IngresoRemesasNuevo.Remesa In objPeticionRemesas
                    If ValidarRemesa(objPeticionRemesa, objRespuesta) Then
                        peticionAltas.remesas.Add(New Clases.Transferencias.FiltroRemesaAltas With {.CodigoExterno = objPeticionRemesa.NumeroExterno,
                                                                                                   .CodigoDelegacion = objPeticionRemesa.CodigoDelegacion,
                                                                                                   .CodigoPlanta = objPeticionRemesa.CodigoPlanta,
                                                                                                   .CodigoSector = objPeticionRemesa.CodigoSector,
                                                                                                   .CodigoCliente = objPeticionRemesa.Bultos.First.CodigoCliente,
                                                                                                   .CodigoSubCliente = objPeticionRemesa.Bultos.First.CodigoSubCliente,
                                                                                                   .CodigoPuntoServicio = objPeticionRemesa.Bultos.First.CodigoPuntoServicio,
                                                                                                   .CodigoCanal = objPeticionRemesa.Bultos.First.CodigoCanal,
                                                                                                   .CodigoSubCanal = objPeticionRemesa.Bultos.First.CodigoSubCanal})
                        peticionAltas.CodigoUsuario = CodigoUsuario
                    End If
                Next
                Dim documentosExistentes As ObservableCollection(Of Clases.Documento) = AccesoDatos.GenesisSaldos.Documento.verificarDocumentoExiste(peticionAltas)

                For Each objPeticionRemesa As IngresoRemesasNuevo.Remesa In objPeticionRemesas
                    Try

                        ' Verificar se na Remesa existe pelo menos um Bulto e se todos os Bultos tem a mesma Conta
                        If ValidarRemesa(objPeticionRemesa, objRespuesta) Then

                            Dim objDocumento As Comon.Clases.Documento

                            Dim documentoPadre As Clases.Documento = Nothing
                            If documentosExistentes IsNot Nothing AndAlso documentosExistentes.Count > 0 Then
                                documentoPadre = documentosExistentes.FirstOrDefault(Function(x) x.NumeroExterno = objPeticionRemesa.NumeroExterno)
                            End If

                            ' Recupera o CodigoRuta
                            Dim CodigoRuta As String = String.Empty
                            If objPeticionRemesa.Bultos IsNot Nothing AndAlso objPeticionRemesa.Bultos.Count > 0 Then CodigoRuta = objPeticionRemesa.Bultos.First.CodigoRuta

                            ' Recupera fecha Transporte
                            Dim FechaTransporte As DateTime = DateTime.Now
                            If objPeticionRemesa.Bultos IsNot Nothing AndAlso objPeticionRemesa.Bultos.Count > 0 Then FechaTransporte = objPeticionRemesa.Bultos.First.FechaTransporte

                            ' Prepara Cuenta
                            Dim objCuentaOrigen As Clases.Cuenta = Nothing
                            Dim objCuentaDestino As Clases.Cuenta = Nothing
                            Dim objCuentaSaldoOrigen As Clases.Cuenta = Nothing
                            Dim objCuentaSaldoDestino As Clases.Cuenta = Nothing
                            Dim validaciones As New List(Of Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

                            ' Recuperar Cuenta Origen
                            objCuentaOrigen = New Clases.Cuenta With {
                                .Cliente = New Clases.Cliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCliente},
                                .SubCliente = New Clases.SubCliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCliente},
                                .PuntoServicio = New Clases.PuntoServicio With {.Codigo = objPeticionRemesa.Bultos.First.CodigoPuntoServicio},
                                .Canal = New Clases.Canal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCanal},
                                .SubCanal = New Clases.SubCanal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCanal},
                                .Sector = New Clases.Sector With {.Codigo = objPeticionRemesa.CodigoSector,
                                                                  .Delegacion = New Clases.Delegacion With {.Codigo = objPeticionRemesa.CodigoDelegacion},
                                                                  .Planta = New Clases.Planta With {.Codigo = objPeticionRemesa.CodigoPlanta}},
                                .UsuarioModificacion = CodigoUsuario,
                                .TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento}

                            ' Recuperar Cuenta Destino
                            If Not String.IsNullOrEmpty(objPeticionRemesa.CodigoSectorDestino) Then
                                objCuentaDestino = New Clases.Cuenta With {
                                            .Cliente = New Clases.Cliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCliente},
                                            .SubCliente = New Clases.SubCliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCliente},
                                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = objPeticionRemesa.Bultos.First.CodigoPuntoServicio},
                                            .Canal = New Clases.Canal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCanal},
                                            .SubCanal = New Clases.SubCanal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCanal},
                                            .Sector = New Clases.Sector With {.Codigo = objPeticionRemesa.CodigoSectorDestino,
                                                                  .Delegacion = New Clases.Delegacion With {.Codigo = objPeticionRemesa.CodigoDelegacion},
                                                                  .Planta = New Clases.Planta With {.Codigo = objPeticionRemesa.CodigoPlanta}},
                                            .UsuarioModificacion = CodigoUsuario,
                                            .TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento}
                            End If

                            If Not String.IsNullOrEmpty(objPeticionRemesa.Bultos.First.CodigoClienteSaldo) Then

                                objCuentaSaldoOrigen = New Clases.Cuenta With {
                                            .Cliente = New Clases.Cliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoClienteSaldo},
                                            .SubCliente = New Clases.SubCliente With {.Codigo = If(Not String.IsNullOrEmpty(objPeticionRemesa.Bultos.First.CodigoSubClienteSaldo),
                                                                                                                            objPeticionRemesa.Bultos.First.CodigoSubClienteSaldo, "")},
                                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = If(Not String.IsNullOrEmpty(objPeticionRemesa.Bultos.First.CodigoPuntoServicioSaldo),
                                                                                                                            objPeticionRemesa.Bultos.First.CodigoPuntoServicioSaldo, "")},
                                            .Canal = New Clases.Canal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCanal},
                                            .SubCanal = New Clases.SubCanal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCanal},
                                            .Sector = New Clases.Sector With {.Codigo = objPeticionRemesa.CodigoSector,
                                                                  .Delegacion = New Clases.Delegacion With {.Codigo = objPeticionRemesa.CodigoDelegacion},
                                                                  .Planta = New Clases.Planta With {.Codigo = objPeticionRemesa.CodigoPlanta}},
                                            .UsuarioModificacion = CodigoUsuario,
                                            .TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento}

                                If Not String.IsNullOrEmpty(objPeticionRemesa.CodigoSectorDestino) Then

                                    objCuentaSaldoDestino = New Clases.Cuenta With {
                                            .Cliente = New Clases.Cliente With {.Codigo = objPeticionRemesa.Bultos.First.CodigoClienteSaldo},
                                            .SubCliente = New Clases.SubCliente With {.Codigo = If(Not String.IsNullOrEmpty(objPeticionRemesa.Bultos.First.CodigoSubClienteSaldo),
                                                                                                                            objPeticionRemesa.Bultos.First.CodigoSubClienteSaldo, "")},
                                            .PuntoServicio = New Clases.PuntoServicio With {.Codigo = If(Not String.IsNullOrEmpty(objPeticionRemesa.Bultos.First.CodigoPuntoServicioSaldo),
                                                                                                                            objPeticionRemesa.Bultos.First.CodigoPuntoServicioSaldo, "")},
                                            .Canal = New Clases.Canal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoCanal},
                                            .SubCanal = New Clases.SubCanal With {.Codigo = objPeticionRemesa.Bultos.First.CodigoSubCanal},
                                            .Sector = New Clases.Sector With {.Codigo = objPeticionRemesa.CodigoSectorDestino,
                                                                  .Delegacion = New Clases.Delegacion With {.Codigo = objPeticionRemesa.CodigoDelegacion},
                                                                  .Planta = New Clases.Planta With {.Codigo = objPeticionRemesa.CodigoPlanta}},
                                            .UsuarioModificacion = CodigoUsuario,
                                            .TipoCuenta = Comon.Enumeradores.TipoCuenta.Movimiento}
                                End If
                            End If

                            ' Recuperar Cuenta Saldos Origen e Destino
                            If objCuentaDestino Is Nothing Then
                                objCuentaDestino = objCuentaOrigen
                                objCuentaSaldoDestino = objCuentaSaldoOrigen
                            End If

                            ' Recupera FechaHoraPlanificacionCertificacion
                            ' Utiliza la FechaTransporte se estiver dentro do periodo que ainda não foi certificado, senão utiliza o DateTime.Now()
                            Dim objFechaHoraPlanificacionCertificacion As DateTime = DateTime.Now
                            If Not ((Not String.IsNullOrEmpty(objCuentaOrigen.Identificador) AndAlso Not String.IsNullOrEmpty(objCuentaDestino.Identificador)) AndAlso
                                (Not LogicaNegocio.GenesisSaldos.Certificacion.AccionValidaCertificacion _
                                  .EsFechaHoraPlanCertificacionValidaPorCuentaSaldo(objCuentaSaldoOrigen, objCuentaSaldoDestino, FechaTransporte))) Then

                                objFechaHoraPlanificacionCertificacion = FechaTransporte
                            End If

                            ' Se o documento existe, é criado um documento de Substituição
                            If documentoPadre IsNot Nothing Then

                                ' Documento de Substituición
                                objDocumento = documentoDefectoSustitucion.Clonar

                                With objDocumento
                                    .Identificador = System.Guid.NewGuid.ToString()
                                    .DocumentoPadre = documentoPadre
                                    .Elemento = ConverterElemento(objPeticionRemesa,
                                                                  objCuentaDestino,
                                                                  CodigoUsuario,
                                                                  objDocumento,
                                                                  objColeccionProcesos,
                                                                  ConfigNivelDetalle,
                                                                  objRespuesta,
                                                                  DirectCast(documentoPadre.Elemento, Clases.Remesa))
                                    .CuentaOrigen = documentoPadre.CuentaOrigen
                                    .CuentaDestino = documentoPadre.CuentaDestino
                                    .CuentaSaldoOrigen = documentoPadre.CuentaSaldoOrigen
                                    .CuentaSaldoDestino = documentoPadre.CuentaSaldoDestino
                                    .NumeroExterno = objPeticionRemesa.NumeroExterno
                                    .UsuarioCreacion = CodigoUsuario
                                    .UsuarioModificacion = CodigoUsuario
                                    .Estado = Comon.Enumeradores.EstadoDocumento.EnCurso
                                    .FechaHoraGestion = FechaTransporte
                                    .FechaHoraPlanificacionCertificacion = objFechaHoraPlanificacionCertificacion
                                    .DocumentoPadre.IdentificadorSustituto = objDocumento.Identificador
                                End With

                            Else

                                ' Documento de Ingreso
                                objDocumento = documentoDefectoIngreso.Clonar

                                With objDocumento
                                    .Identificador = System.Guid.NewGuid.ToString()
                                    .Elemento = ConverterElemento(objPeticionRemesa,
                                                                  objCuentaDestino,
                                                                  CodigoUsuario,
                                                                  objDocumento,
                                                                  objColeccionProcesos,
                                                                  ConfigNivelDetalle,
                                                                  objRespuesta,
                                                                  Nothing)
                                    .CuentaOrigen = objCuentaOrigen
                                    .CuentaDestino = objCuentaDestino
                                    .CuentaSaldoOrigen = objCuentaSaldoOrigen
                                    .CuentaSaldoDestino = objCuentaSaldoDestino
                                    .NumeroExterno = objPeticionRemesa.NumeroExterno
                                    .UsuarioCreacion = CodigoUsuario
                                    .UsuarioModificacion = CodigoUsuario
                                    .FechaHoraGestion = FechaTransporte
                                    .FechaHoraPlanificacionCertificacion = objFechaHoraPlanificacionCertificacion
                                    .Estado = Comon.Enumeradores.EstadoDocumento.EnCurso
                                End With

                            End If

                            objDocumentos.Add(objDocumento)

                        End If

                    Catch ex As Exception
                        Util.TratarErroBugsnag(ex)
                        ' Adiciona a mensagem de erro
                        objRespuesta.RemesasError.Add(New IngresoRemesasNuevo.RemesaError With {
                                                  .IdRemesaError = objPeticionRemesa.IdRemesaOrigen,
                                                  .DescRemesaError = ex.Message})
                    End Try

                    Dim objRemesaOk As New IngresoRemesasNuevo.RemesaOk
                    objRemesaOk.CodigoDelegacion = objPeticionRemesa.CodigoDelegacion
                    objRemesaOk.CodigoPlanta = objPeticionRemesa.CodigoPlanta
                    objRemesaOk.IdentificadorRemesaOriginal = objPeticionRemesa.IdRemesaOrigen
                    objRemesaOk.Observaciones = String.Empty
                    If objPeticionRemesa.Bultos IsNot Nothing Then
                        For Each objBulto As IngresoRemesasNuevo.Bulto In objPeticionRemesa.Bultos
                            objRemesaOk.Bultos.Add(New IngresoRemesasNuevo.BultoOk With {.IdentificadorBultoOriginal = objBulto.IdBultoOrigen})
                        Next
                    End If
                    objRespuesta.RemesasOK.Add(objRemesaOk)

                Next

            End If

            Return objDocumentos
        End Function

        Private Shared Function ConverterElemento(objPeticionRemesa As IngresoRemesasNuevo.Remesa, _
                                                  objCuenta As Clases.Cuenta, _
                                                  CodigoUsuario As String, _
                                                  ByRef objDocumento As Clases.Documento, _
                                                  ByRef objColeccionProcesos As ObservableCollection(Of GetProceso.Proceso), _
                                                  ConfigNivelDetalle As Comon.Enumeradores.ConfiguracionNivelSaldos, _
                                                  ByRef objRespuesta As IngresoRemesasNuevo.Respuesta,
                                                  elementoPadre As Clases.Remesa) As Clases.Remesa

            ' Cria a variável que recebe os dados a remessa
            Dim objRemesa As New Comon.Clases.Remesa
            Dim objProceso As GetProceso.Proceso = Nothing

            ' Recupera o declarado total da remessa
            ConverterDivisaDeclaradoTotalRemesa(objRemesa.Divisas, objPeticionRemesa.DeclaradosTotalRemesa)

            ' Recupera o declarado detalhado da remessa
            ConverterDivisaDeclaradoDetalladoRemesa(objRemesa.Divisas, objPeticionRemesa.DeclaradosDetalleRemesa)

            ' Recupera o declarado detalhado da remessa
            ConverterDivisaDeclaradoMedioPagoRemesa(objRemesa.Divisas, objPeticionRemesa.DeclaradosMedioPagoRemesa)

            objRemesa.Identificador = System.Guid.NewGuid.ToString()
            objRemesa.IdentificadorDocumento = objDocumento.Identificador
            objRemesa.CodigoExterno = objPeticionRemesa.NumeroExterno
            objRemesa.Cuenta = objCuenta
            objRemesa.Estado = RecuperarEnum(Of Comon.Enumeradores.EstadoRemesa)(objPeticionRemesa.CodigoEstado)
            objRemesa.UsuarioCreacion = CodigoUsuario
            objRemesa.UsuarioModificacion = CodigoUsuario
            objRemesa.UsuarioResponsable = CodigoUsuario
            objRemesa.IdentificadorExterno = objPeticionRemesa.IdRemesaOrigen
            objRemesa.CantidadBultos = objPeticionRemesa.NumBultos
            objRemesa.ConfiguracionNivelSaldos = ConfigNivelDetalle
            objRemesa.ElementoPadre = elementoPadre
            If Not String.IsNullOrEmpty(objPeticionRemesa.CodigoCajero) Then
                objRemesa.DatosATM = New Clases.ATM With {.CodigoCajero = objPeticionRemesa.CodigoCajero}
            End If

            If objPeticionRemesa.Bultos IsNot Nothing AndAlso objPeticionRemesa.Bultos.Count > 0 Then objRemesa.FechaHoraTransporte = objPeticionRemesa.Bultos.First.FechaTransporte
            If objPeticionRemesa.Bultos IsNot Nothing AndAlso objPeticionRemesa.Bultos.Count > 0 Then objRemesa.ReciboTransporte = objPeticionRemesa.Bultos.First.CodigoTransporte
            If objPeticionRemesa.Bultos IsNot Nothing AndAlso objPeticionRemesa.Bultos.Count > 0 Then objRemesa.Ruta = objPeticionRemesa.Bultos.First.CodigoRuta

            ' Preenche objeto GrupoTerminosIAC
            If objPeticionRemesa.CamposExtra IsNot Nothing AndAlso objPeticionRemesa.CamposExtra.Count > 0 Then
                If objDocumento.GrupoTerminosIAC Is Nothing Then
                    objDocumento.GrupoTerminosIAC = objDocumento.Formulario.GrupoTerminosIACIndividual
                End If
                For Each ce As IngresoRemesasNuevo.CampoExtra In objPeticionRemesa.CamposExtra
                    If ce.Nombre = Comon.Extenciones.RecuperarValor(Comon.Enumeradores.TerminosDatosRuta.NumeroSecuencia) Then
                        objRemesa.Parada = ce.Valor
                    End If
                    If objDocumento.GrupoTerminosIAC IsNot Nothing AndAlso objDocumento.GrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso objDocumento.GrupoTerminosIAC.TerminosIAC.Count > 0 Then
                        Dim objTermino As Comon.Clases.TerminoIAC = objDocumento.GrupoTerminosIAC.TerminosIAC.Where(Function(t) t.Codigo = ce.Nombre).FirstOrDefault
                        If objTermino IsNot Nothing Then
                            objTermino.Valor = ce.Valor
                        End If
                    End If
                Next ce
            End If

            ' Se exitem malotes
            If objPeticionRemesa.Bultos IsNot Nothing Then

                ' Cria a lista de malotes
                objRemesa.Bultos = New ObservableCollection(Of Comon.Clases.Bulto)

                ' Para cada malote existente
                For Each bulto As IngresoRemesasNuevo.Bulto In objPeticionRemesa.Bultos

                    If objColeccionProcesos IsNot Nothing Then

                        'Utilizando LAMBDA verifica se existe proceso para o Bulto.
                        objProceso = objColeccionProcesos.Find(Function(s) _
                                                         IIf(String.IsNullOrEmpty(s.Cliente), String.Empty, s.Cliente) = bulto.CodigoCliente AndAlso _
                                                         IIf(String.IsNullOrEmpty(s.SubCliente), String.Empty, s.SubCliente) = bulto.CodigoSubCliente AndAlso _
                                                         IIf(String.IsNullOrEmpty(s.PuntoServicio), String.Empty, s.PuntoServicio) = bulto.CodigoPuntoServicio AndAlso _
                                                         IIf(String.IsNullOrEmpty(s.SubCanal), String.Empty, s.SubCanal) = bulto.CodigoSubCanal AndAlso _
                                                         IIf(String.IsNullOrEmpty(s.Delegacion), String.Empty, s.Delegacion) = objPeticionRemesa.CodigoDelegacion)
                    End If

                    ' Define a petição para a busca do banco ingresso
                    Dim objPeticionHelper As New Comon.PeticionHelper With
                    {
                        .Codigo = bulto.CodigoBancoIngreso,
                        .Tabela = New Comon.UtilHelper.Tabela With {.Tabela = Comon.Helper.Enumeradores.Tabelas.TabelaHelper.Cliente},
                        .ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion With {.RealizarPaginacion = False}
                    }

                    ' Recupera o Banco Ingresso
                    Dim objRespuestaHelper As Comon.RespuestaHelper = Prosegur.Genesis.LogicaNegocio.Classes.Helper.Busqueda(objPeticionHelper)

                    ' Váriavel que recebe a lista de bancos ingressos
                    Dim clienteBancoIngreso As Comon.Clases.Cliente = Nothing

                    ' Verifica se encontrou algum cliente
                    If objRespuesta IsNot Nothing AndAlso objRespuestaHelper.DatosRespuesta IsNot Nothing Then
                        ' Passa os clientes encontrados na resposta para lista de bancos ingressos
                        clienteBancoIngreso = (From r In objRespuestaHelper.DatosRespuesta Where r.Codigo = bulto.CodigoBancoIngreso Select New Comon.Clases.Cliente With {.Identificador = r.Identificador, .Codigo = r.Codigo, .Descripcion = r.Descricao}).FirstOrDefault
                    End If

                    ' Se o malote possui precinto
                    If Not String.IsNullOrEmpty(bulto.CodigoPrecinto) Then

                        Dim objParciales As ObservableCollection(Of Comon.Clases.Parcial) = Nothing

                        ' carregar os parciais do bulto
                        If bulto.Parciales IsNot Nothing AndAlso bulto.Parciales.Count > 0 Then

                            objParciales = New ObservableCollection(Of Comon.Clases.Parcial)
                            Dim objPrecintos As ObservableCollection(Of String) = Nothing
                            Dim objTipoFormato As Comon.Clases.TipoFormato = Nothing

                            For Each _parcial In bulto.Parciales

                                Dim Parcial As New Comon.Clases.Parcial

                                ' Recupera o declarado detalhado da remessa
                                ConverterDivisaDeclaradoDetalladoParcial(Parcial.Divisas, _parcial.DeclaradosDetalleParcial)

                                ' Recupera o declarado total do malote
                                ConverterDivisaDeclaradoTotalParcial(Parcial.Divisas, _parcial.DeclaradoTotaisParcial)

                                If Not String.IsNullOrEmpty(_parcial.CodigoPrecinto) Then
                                    objPrecintos = New ObservableCollection(Of String)
                                    objPrecintos.Add(_parcial.CodigoPrecinto)
                                End If

                                'Recupera o tipo formato
                                objTipoFormato = AccesoDatos.Genesis.TipoFormato.RecuperarTipoFormatoPorCodigo(_parcial.CodigoFormato)

                                With Parcial
                                    .Identificador = System.Guid.NewGuid.ToString()
                                    .IdentificadorExterno = _parcial.IdParcialOrigen
                                    .EsFicticio = False
                                    .Estado = Comon.Enumeradores.EstadoParcial.Pendiente
                                    .GrupoTerminosIAC = If(objProceso IsNot Nothing AndAlso objProceso.Iac IsNot Nothing, ConverterIAC(objProceso.Iac, _parcial.ValoresParcial), Nothing)
                                    .Precintos = objPrecintos
                                    .Secuencia = _parcial.Secuencia
                                    .TipoFormato = objTipoFormato
                                    .UsuarioCreacion = CodigoUsuario
                                    .UsuarioModificacion = CodigoUsuario
                                End With

                                objParciales.Add(Parcial)

                            Next

                        End If
                        ' Adiciona o malote na remessa
                        objRemesa.Bultos.Add(New Comon.Clases.Bulto With
                                                {
                                                    .Identificador = System.Guid.NewGuid.ToString(),
                                                    .IdentificadorExterno = bulto.IdBultoOrigen,
                                                    .IdentificadorDocumento = objDocumento.Identificador,
                                                    .CodigoBolsa = bulto.CodigoBolsa,
                                                    .Cuenta = objRemesa.Cuenta,
                                                    .Estado = Comon.Enumeradores.EstadoBulto.Nuevo,
                                                    .TipoFormato = If(String.IsNullOrEmpty(bulto.CodigoFormato), Nothing, LogicaNegocio.Genesis.TipoFormato.ObtenerTipoFormatoPorCodigo(bulto.CodigoFormato)),
                                                    .TipoServicio = If(String.IsNullOrEmpty(bulto.CodigoTipoTrabajo), Nothing, LogicaNegocio.Genesis.TipoServicio.ObtenerTipoServicioPorCodigo(bulto.CodigoTipoTrabajo)),
                                                    .BancoIngreso = clienteBancoIngreso,
                                                    .BancoIngresoEsBancoMadre = bulto.BolBancoMadre,
                                                    .FechaProcessoLegado = bulto.FechaProceso,
                                                    .FechaHoraTransporte = bulto.FechaTransporte,
                                                    .ReciboTransporte = objRemesa.ReciboTransporte,
                                                    .CantidadParciales = IIf(bulto.NumeroParciales Is Nothing, 0, bulto.NumeroParciales),
                                                    .Precintos = New ObservableCollection(Of String) From {bulto.CodigoPrecinto},
                                                    .GrupoTerminosIAC = If(objProceso IsNot Nothing AndAlso objProceso.IacBulto IsNot Nothing, ConverterIAC(objProceso.IacBulto, bulto.ValoresBulto), Nothing),
                                                    .UsuarioCreacion = objRemesa.UsuarioCreacion,
                                                    .UsuarioModificacion = objRemesa.UsuarioModificacion,
                                                    .UsuarioResponsable = objRemesa.UsuarioResponsable,
                                                    .ConfiguracionNivelSaldos = ConfigNivelDetalle,
                                                    .Parciales = objParciales,
                                                    .ElementoPadre = If(elementoPadre Is Nothing OrElse elementoPadre.Bultos Is Nothing OrElse elementoPadre.Bultos.Count = 0, Nothing, _
                                                                        elementoPadre.Bultos.FirstOrDefault(Function(x) x.Precintos IsNot Nothing AndAlso x.Precintos.Count > 0 AndAlso
                                                                                                                x.Precintos.Contains(bulto.CodigoPrecinto)))
                                                }
                                            )

                        ' Recupera o declarado total do malote
                        ConverterDivisaDeclaradoTotalBulto(objRemesa.Bultos.Last.Divisas, bulto.DeclaradosTotalBulto)

                        ' Recupera o declarado detalhado do malote
                        ConverterDivisaDeclaradoDetalladoBulto(objRemesa.Bultos.Last.Divisas, bulto.DeclaradosDetBulto)

                    End If

                Next bulto

                'Preenche o grupo da remesa.
                objRemesa.GrupoTerminosIAC = If(objProceso IsNot Nothing AndAlso objProceso.Iac IsNot Nothing, ConverterIAC(objProceso.IacRemesa, objPeticionRemesa.ValoresRemesa), Nothing)

            End If

            ' Adiciona a remessa ao documento
            Return objRemesa
        End Function


        ''' <summary>
        ''' Converte os IACs
        ''' </summary>
        ''' <param name="objIAC"></param>
        ''' <param name="objValoresIac"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ConverterIAC(objIAC As GetProceso.Iac, _
                                             objValoresIac As IngresoRemesasNuevo.ValoresRemesa) As Comon.Clases.GrupoTerminosIAC

            Dim objGrupoTerminosIAC As Comon.Clases.GrupoTerminosIAC = Nothing

            'Verifica se o objIAC recuperado o processo de iac está preenchido.
            If objIAC IsNot Nothing AndAlso objIAC.TerminosIac IsNot Nothing AndAlso objIAC.TerminosIac.Count > 0 Then

                'Recupera os grupo de terminos do novo saldos
                objGrupoTerminosIAC = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(objIAC.Codigo)

                'Verifica se o grupo de terminos não é vazio e se contem terminos
                If objGrupoTerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC.Count > 0 Then

                    Dim objValorIac As IngresoRemesasNuevo.ValorRemesa = Nothing

                    'Percorre os terminos do grupo e e recupera o valor do termino
                    For Each objTermino In objGrupoTerminosIAC.TerminosIAC

                        If objValoresIac IsNot Nothing Then objValorIac = objValoresIac.FindAll(Function(t) t.CodTerminoIac = objTermino.Codigo).FirstOrDefault

                        If objValorIac IsNot Nothing Then
                            objTermino.Valor = objValorIac.DesValorTerminoIac
                        End If

                    Next

                End If
            End If

            Return objGrupoTerminosIAC
        End Function

        ''' <summary>
        ''' Converte os IACs
        ''' </summary>
        ''' <param name="objIAC"></param>
        ''' <param name="objValoresIac"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ConverterIAC(objIAC As GetProceso.Iac, _
                                             objValoresIac As IngresoRemesasNuevo.ValoresBulto) As Comon.Clases.GrupoTerminosIAC

            Dim objGrupoTerminosIAC As Comon.Clases.GrupoTerminosIAC = Nothing

            'Verifica se o objIAC recuperado o processo de iac está preenchido.
            If objIAC IsNot Nothing AndAlso objIAC.TerminosIac IsNot Nothing AndAlso objIAC.TerminosIac.Count > 0 Then

                'Recupera os grupo de terminos do novo saldos
                objGrupoTerminosIAC = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(objIAC.Codigo)

                'Verifica se o grupo de terminos não é vazio e se contem terminos
                If objGrupoTerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC.Count > 0 Then

                    Dim objValorIac As IngresoRemesasNuevo.ValorBulto = Nothing

                    'Percorre os terminos do grupo e e recupera o valor do termino
                    For Each objTermino In objGrupoTerminosIAC.TerminosIAC

                        If objValoresIac IsNot Nothing Then objValorIac = objValoresIac.FindAll(Function(t) t.CodTerminoIac = objTermino.Codigo).FirstOrDefault

                        If objValorIac IsNot Nothing Then
                            objTermino.Valor = objValorIac.DesValorTerminoIac
                        End If

                    Next

                End If
            End If

            Return objGrupoTerminosIAC
        End Function

        ''' <summary>
        ''' Converte os IACs
        ''' </summary>
        ''' <param name="objIAC"></param>
        ''' <param name="objValoresIac"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function ConverterIAC(objIAC As GetProceso.Iac, _
                                             objValoresIac As IngresoRemesasNuevo.ValoresParcial) As Comon.Clases.GrupoTerminosIAC

            Dim objGrupoTerminosIAC As Comon.Clases.GrupoTerminosIAC = Nothing

            'Verifica se o objIAC recuperado o processo de iac está preenchido.
            If objIAC IsNot Nothing AndAlso objIAC.TerminosIac IsNot Nothing AndAlso objIAC.TerminosIac.Count > 0 Then

                'Recupera os grupo de terminos do novo saldos
                objGrupoTerminosIAC = AccesoDatos.Genesis.GrupoTerminosIAC.RecuperarGrupoTerminosIACPorCodigo(objIAC.Codigo)

                'Verifica se o grupo de terminos não é vazio e se contem terminos
                If objGrupoTerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC IsNot Nothing AndAlso objGrupoTerminosIAC.TerminosIAC.Count > 0 Then

                    Dim objValorIac As IngresoRemesasNuevo.ValorParcial = Nothing

                    'Percorre os terminos do grupo e e recupera o valor do termino
                    For Each objTermino In objGrupoTerminosIAC.TerminosIAC

                        If objValoresIac IsNot Nothing Then objValorIac = objValoresIac.FindAll(Function(t) t.CodTerminoIac = objTermino.Codigo).FirstOrDefault

                        If objValorIac IsNot Nothing Then
                            objTermino.Valor = objValorIac.DesValorTerminoIac
                        End If

                    Next

                End If
            End If

            Return objGrupoTerminosIAC
        End Function

        Public Shared Sub ConverterDivisaDeclaradoTotalRemesa(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoTotalRemesa As IngresoRemesasNuevo.DeclaradosTotalRemesa)

            ' Verifica se existem informações de declarados
            If declaradoTotalRemesa IsNot Nothing AndAlso declaradoTotalRemesa.Count > 0 Then

                ' Se a lista de divisas está vazia
                If divisas Is Nothing Then
                    ' Cria a lista de divisas
                    divisas = New ObservableCollection(Of Comon.Clases.Divisa)
                End If

                Dim objDivisaRecuperada As Comon.Clases.Divisa = Nothing

                ' Para cada declarado da remessa
                For Each declarado As IngresoRemesasNuevo.DeclaradoTotalRemesa In declaradoTotalRemesa

                    ' Recupera a divisa 
                    Dim divisa As Comon.Clases.Divisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, New ObservableCollection(Of String) From {declarado.CodigoIsoDivisa}).FirstOrDefault

                    ' Se encontrou a divisa
                    If divisa IsNot Nothing Then

                        'Verifica se a coleção de divisas contem a divisa do declarado total
                        objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        'Se não existe adiciona a divisa na coleção de divisas e recupera a divisa para trabalhar com os dados
                        'por referência
                        If objDivisaRecuperada Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(divisa)

                            objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        End If

                        ' Preenche os dados da divisa
                        With objDivisaRecuperada
                            .ValoresTotalesDivisa = New ObservableCollection(Of Comon.Clases.ValorDivisa) From {
                                New Comon.Clases.ValorDivisa With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteTotal.HasValue, declarado.ImporteTotal, 0)}
                            }
                            .ValoresTotalesEfectivo = New ObservableCollection(Of Comon.Clases.ValorEfectivo) From {
                                New Comon.Clases.ValorEfectivo With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteEfectivo.HasValue, declarado.ImporteEfectivo, 0)}
                            }
                            .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Comon.Clases.ValorTipoMedioPago) From {
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Cheque, .Importe = If(declarado.ImporteCheque.HasValue, declarado.ImporteCheque, 0)},
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.OtroValor, .Importe = If(declarado.ImporteOtroValor.HasValue, declarado.ImporteOtroValor, 0)},
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Ticket, .Importe = If(declarado.ImporteTicket.HasValue, declarado.ImporteTicket, 0)}
                            }
                        End With

                        ' Remove os declarados que não possuem valores
                        objDivisaRecuperada.ValoresTotalesEfectivo.RemoveAll(Function(vd) vd.Importe = 0)
                        objDivisaRecuperada.ValoresTotalesTipoMedioPago.RemoveAll(Function(vd) vd.Importe = 0)
                        objDivisaRecuperada.ValoresTotalesDivisa.RemoveAll(Function(vd) vd.Importe = 0)

                    End If

                Next

            End If

        End Sub

        Public Shared Sub ConverterDivisaDeclaradoDetalladoRemesa(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoDetalladoRemesa As IngresoRemesasNuevo.DeclaradosDetalleRemesa)

            ' Verifica se a lista de divisas está vazia
            If divisas Is Nothing Then
                ' cria uma nova lista de divisas
                divisas = New ObservableCollection(Of Comon.Clases.Divisa)
            End If

            ' Verifica se existe declarado detalhado
            If declaradoDetalladoRemesa IsNot Nothing AndAlso declaradoDetalladoRemesa.Count > 0 Then

                ' Para cada declarado detalhado da remessa
                For Each declarado As IngresoRemesasNuevo.DeclaradoDetalleRemesa In declaradoDetalladoRemesa

                    Dim objdivisa As Comon.Clases.Divisa = AccesoDatos.Genesis.Divisas.ObtenerDivisaPorCodigoDenominacion(declarado.CodigoDenominacion, True, False)

                    If objdivisa IsNot Nothing Then

                        ' Recupera a divisa, se ela existe na lista de divisas do documento
                        Dim divisa As Comon.Clases.Divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        ' Se a divisa não existe
                        If divisa Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(objdivisa)

                            divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        End If

                        ' Se não existe a lista de divisa
                        If divisa.Denominaciones Is Nothing Then
                            divisa.Denominaciones = New ObservableCollection(Of Comon.Clases.Denominacion)
                        End If

                        ' Recupera a denominação
                        Dim denominacion As Comon.Clases.Denominacion = divisa.Denominaciones.Where(Function(d) d.Codigo = declarado.CodigoDenominacion).FirstOrDefault

                        ' Se encontrou a denominação
                        If denominacion IsNot Nothing Then

                            ' Atualiza os dados da denominação
                            With denominacion
                                .ValorDenominacion = New ObservableCollection(Of Comon.Clases.ValorDenominacion) From
                                {
                                      New Comon.Clases.ValorDenominacion With
                                      {
                                          .Cantidad = declarado.Unidades,
                                          .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                          .Importe = declarado.Unidades * denominacion.Valor
                                      }
                                }
                            End With

                        End If

                    End If

                Next

                ' Para cada divisa existente
                For Each div As Comon.Clases.Divisa In divisas

                    ' Remove as denominações sem valor
                    div.Denominaciones.RemoveAll(Function(d) d.ValorDenominacion Is Nothing)

                Next

            End If

        End Sub

        Public Shared Sub ConverterDivisaDeclaradoMedioPagoRemesa(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradosMedioPagoRemesa As List(Of IngresoRemesasNuevo.DeclaradoMedioPagoRemesa))

            ' Verifica se a lista de divisas está vazia
            If divisas Is Nothing Then
                ' cria uma nova lista de divisas
                divisas = New ObservableCollection(Of Comon.Clases.Divisa)
            End If

            ' Verifica se existe declarado detalhado
            If declaradosMedioPagoRemesa IsNot Nothing AndAlso declaradosMedioPagoRemesa.Count > 0 Then

                ' Para cada declarado detalhado da remessa
                For Each declarado As IngresoRemesasNuevo.DeclaradoMedioPagoRemesa In declaradosMedioPagoRemesa

                    ' Recupera a divisa, se ela existe na lista de divisas do documento
                    Dim divisa As Comon.Clases.Divisa = divisas.Where(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault()

                    ' Se a divisa não existe
                    If divisa Is Nothing Then

                        ' Cria uma nova divisa
                        divisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, New ObservableCollection(Of String) From {declarado.CodigoIsoDivisa}).FirstOrDefault

                        ' Adiciona os dados de declarado
                        divisas.Add(divisa)
                    End If

                    ' Se a divisa não possui a lista de denominações
                    If divisa.MediosPago Is Nothing Then
                        divisa.MediosPago = New ObservableCollection(Of Comon.Clases.MedioPago)
                    End If

                    ' Recupera o meio de pagamento
                    Dim medioPago As Comon.Clases.MedioPago = divisa.MediosPago.Where(Function(d) d.Codigo = declarado.CodigoMedioPago).FirstOrDefault

                    ' Se encontrou o meio de pagamento
                    If medioPago IsNot Nothing Then

                        ' Atualiza os dados do meio de pagamento
                        With medioPago
                            .Valores = New ObservableCollection(Of Comon.Clases.ValorMedioPago) From
                            {
                                  New Comon.Clases.ValorMedioPago With
                                  {
                                      .Cantidad = declarado.Unidades,
                                      .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                      .Importe = declarado.Importe
                                  }
                            }

                            ' Verifica se existe meios de pagamento
                            If medioPago.Valores.Last.Terminos IsNot Nothing AndAlso medioPago.Valores.Last.Terminos.Count > 0 Then

                                ' Com o valor de meio de pagamento adicionado
                                With medioPago.Valores.Last

                                    ' Adiciona o valor para o termino
                                    .Terminos = New ObservableCollection(Of Comon.Clases.Termino) From
                                    {
                                        New Comon.Clases.Termino With
                                        {
                                            .Codigo = declarado.Terminos.First.CodigoTermino,
                                            .Valor = declarado.Terminos.First.CodigoValorTermino
                                        }
                                    }

                                End With

                            End If

                        End With

                    End If

                Next

                ' Para cada divisa existente
                For Each div As Comon.Clases.Divisa In divisas

                    ' Remove as denominações sem valor
                    div.MediosPago.RemoveAll(Function(mp) mp.Valores Is Nothing)

                Next

            End If

        End Sub

        Private Shared Sub ConverterDivisaDeclaradoTotalParcial(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoTotalParcial As IngresoRemesasNuevo.DeclaradoTotaisParcial)

            ' Verifica se existem informações de declarados
            If declaradoTotalParcial IsNot Nothing AndAlso declaradoTotalParcial.Count > 0 Then

                ' Se a lista de divisas está vazia
                If divisas Is Nothing Then
                    ' Cria a lista de divisas
                    divisas = New ObservableCollection(Of Comon.Clases.Divisa)
                End If

                Dim objDivisaRecuperada As Comon.Clases.Divisa = Nothing

                ' Para cada declarado da remessa
                For Each declarado In declaradoTotalParcial

                    ' Recupera a divisa 
                    Dim divisa As Comon.Clases.Divisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, New ObservableCollection(Of String) From {declarado.CodigoIsoDivisa}).FirstOrDefault

                    ' Se encontrou a divisa
                    If divisa IsNot Nothing Then

                        'Verifica se a coleção de divisas contem a divisa do declarado total
                        objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        'Se não existe adiciona a divisa na coleção de divisas e recupera a divisa para trabalhar com os dados
                        'por referência
                        If objDivisaRecuperada Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(divisa)

                            objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        End If

                        ' Preenche os dados da divisa
                        With objDivisaRecuperada

                            .ValoresTotalesDivisa = New ObservableCollection(Of Comon.Clases.ValorDivisa) From {
                                New Comon.Clases.ValorDivisa With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteTotal.HasValue, declarado.ImporteTotal, 0)}
                            }
                            .ValoresTotalesEfectivo = New ObservableCollection(Of Comon.Clases.ValorEfectivo) From {
                                New Comon.Clases.ValorEfectivo With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteEfectivo.HasValue, declarado.ImporteEfectivo, 0)}
                            }
                            .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Comon.Clases.ValorTipoMedioPago) From {
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Cheque, .Importe = If(declarado.ImporteCheque.HasValue, declarado.ImporteCheque, 0)},
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.OtroValor, .Importe = If(declarado.ImporteOtroValor.HasValue, declarado.ImporteOtroValor, 0)},
                               New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Ticket, .Importe = If(declarado.ImporteTicket.HasValue, declarado.ImporteTicket, 0)}
                            }
                        End With

                        ' Remove os declarados que não possuem valores
                        objDivisaRecuperada.ValoresTotalesEfectivo.RemoveAll(Function(vd) vd.Importe = 0)
                        objDivisaRecuperada.ValoresTotalesTipoMedioPago.RemoveAll(Function(vd) vd.Importe = 0)
                        objDivisaRecuperada.ValoresTotalesDivisa.RemoveAll(Function(vd) vd.Importe = 0)

                    End If

                Next

            End If

        End Sub

        Private Shared Sub ConverterDivisaDeclaradoDetalladoParcial(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoDetalladoParcial As IngresoRemesasNuevo.DeclaradosDetalleParcial)

            ' Verifica se a lista de divisas está vazia
            If divisas Is Nothing Then
                ' cria uma nova lista de divisas
                divisas = New ObservableCollection(Of Comon.Clases.Divisa)
            End If

            ' Verifica se existe declarado detalhado
            If declaradoDetalladoParcial IsNot Nothing AndAlso declaradoDetalladoParcial.Count > 0 Then

                ' Para cada declarado detalhado da remessa
                For Each declarado In declaradoDetalladoParcial

                    Dim objdivisa As Comon.Clases.Divisa = AccesoDatos.Genesis.Divisas.ObtenerDivisaPorCodigoDenominacion(declarado.CodigoDenominacion, True, False)

                    If objdivisa IsNot Nothing Then

                        ' Recupera a divisa, se ela existe na lista de divisas do documento
                        Dim divisa As Comon.Clases.Divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        ' Se a divisa não existe
                        If divisa Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(objdivisa)

                            divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        End If

                        ' Se não existe a lista de divisa
                        If divisa.Denominaciones Is Nothing Then
                            divisa.Denominaciones = New ObservableCollection(Of Comon.Clases.Denominacion)
                        End If

                        ' Recupera a denominação
                        Dim denominacion As Comon.Clases.Denominacion = divisa.Denominaciones.Where(Function(d) d.Codigo = declarado.CodigoDenominacion).FirstOrDefault

                        ' Se encontrou a denominação
                        If denominacion IsNot Nothing Then

                            ' Atualiza os dados da denominação
                            With denominacion
                                .ValorDenominacion = New ObservableCollection(Of Comon.Clases.ValorDenominacion) From
                                {
                                      New Comon.Clases.ValorDenominacion With
                                      {
                                          .Cantidad = declarado.Unidades,
                                          .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                          .Importe = declarado.Unidades * denominacion.Valor
                                      }
                                }
                            End With

                        End If

                    End If
                Next

                ' Para cada divisa existente
                For Each div As Comon.Clases.Divisa In divisas

                    ' Remove as denominações sem valor
                    div.Denominaciones.RemoveAll(Function(d) d.ValorDenominacion Is Nothing)

                Next

            End If

        End Sub

        Public Shared Sub ConverterDivisaDeclaradoTotalBulto(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoTotalBulto As IngresoRemesasNuevo.DeclaradosTotalBulto)

            ' Verifica se existem informações de declarados
            If declaradoTotalBulto IsNot Nothing AndAlso declaradoTotalBulto.Count > 0 Then

                ' Se a lista de divisas está vazia
                If divisas Is Nothing Then
                    ' Cria a lista de divisas
                    divisas = New ObservableCollection(Of Comon.Clases.Divisa)
                End If

                Dim objDivisaRecuperada As Comon.Clases.Divisa = Nothing

                ' Para cada declarado da remessa
                For Each declarado As IngresoRemesasNuevo.DeclaradoTotalBulto In declaradoTotalBulto

                    ' Recupera a divisa 
                    Dim divisa As Comon.Clases.Divisa = Nothing

                    If (Not String.IsNullOrEmpty(declarado.CodigoIsoDivisa)) Then
                        divisa = LogicaNegocio.Genesis.Divisas.ObtenerDivisas(Nothing, New ObservableCollection(Of String) From {declarado.CodigoIsoDivisa}).FirstOrDefault
                    End If

                    ' Se encontrou a divisa
                    If divisa IsNot Nothing Then

                        ' Verifica se a coleção de divisas contem a divisa do declarado total
                        objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        'Se não existe adiciona a divisa na coleção de divisas e recupera a divisa para trabalhar com os dados
                        'por referência
                        If objDivisaRecuperada Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(divisa)

                            objDivisaRecuperada = divisas.FindAll(Function(d) d.CodigoISO = declarado.CodigoIsoDivisa).FirstOrDefault

                        End If

                    End If

                    'Se objDivisaRecuperada é por que o declarado não possui divisa
                    If objDivisaRecuperada Is Nothing Then
                        objDivisaRecuperada = New Clases.Divisa()
                    End If

                    ' Preenche os dados da divisa
                    With objDivisaRecuperada
                        .ValoresTotalesDivisa = New ObservableCollection(Of Comon.Clases.ValorDivisa) From {
                            New Comon.Clases.ValorDivisa With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteTotal.HasValue, declarado.ImporteTotal, 0)}
                        }
                        .ValoresTotalesEfectivo = New ObservableCollection(Of Comon.Clases.ValorEfectivo) From {
                            New Comon.Clases.ValorEfectivo With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .Importe = If(declarado.ImporteEfectivo.HasValue, declarado.ImporteEfectivo, 0)}
                        }
                        .ValoresTotalesTipoMedioPago = New ObservableCollection(Of Comon.Clases.ValorTipoMedioPago) From {
                           New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Cheque, .Importe = If(declarado.ImporteCheque.HasValue, declarado.ImporteCheque, 0)},
                           New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.OtroValor, .Importe = If(declarado.ImporteOtroValor.HasValue, declarado.ImporteOtroValor, 0)},
                           New Comon.Clases.ValorTipoMedioPago With {.TipoValor = Comon.Enumeradores.TipoValor.Declarado, .TipoMedioPago = Comon.Enumeradores.TipoMedioPago.Ticket, .Importe = If(declarado.ImporteTicket.HasValue, declarado.ImporteTicket, 0)}
                        }
                    End With

                    ' Remove os declarados que não possuem valores
                    objDivisaRecuperada.ValoresTotalesEfectivo.RemoveAll(Function(vd) vd.Importe = 0)
                    objDivisaRecuperada.ValoresTotalesTipoMedioPago.RemoveAll(Function(vd) vd.Importe = 0)
                    objDivisaRecuperada.ValoresTotalesDivisa.RemoveAll(Function(vd) vd.Importe = 0)

                Next

            End If

        End Sub

        Public Shared Sub ConverterDivisaDeclaradoDetalladoBulto(ByRef divisas As ObservableCollection(Of Comon.Clases.Divisa), declaradoDetalladoBulto As IngresoRemesasNuevo.DeclaradosDetalleBulto)

            ' Verifica se a lista de divisas está vazia
            If divisas Is Nothing Then
                ' cria uma nova lista de divisas
                divisas = New ObservableCollection(Of Comon.Clases.Divisa)
            End If

            ' Verifica se existe declarado detalhado
            If declaradoDetalladoBulto IsNot Nothing AndAlso declaradoDetalladoBulto.Count > 0 Then

                ' Para cada declarado detalhado da remessa
                For Each declarado As IngresoRemesasNuevo.DeclaradoDetalleBulto In declaradoDetalladoBulto

                    ' Recupera a divisa de acordo com a denominação
                    Dim objdivisa As Comon.Clases.Divisa = AccesoDatos.Genesis.Divisas.ObtenerDivisaPorCodigoDenominacion(declarado.CodigoDenominacion, True, False)

                    ' Verifica divisa foi preenchida
                    If objdivisa IsNot Nothing Then

                        ' Recupera a divisa, se ela existe na lista de divisas do documento
                        Dim divisa As Comon.Clases.Divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        ' Se a divisa não existe
                        If divisa Is Nothing Then

                            ' Adiciona os dados de declarado
                            divisas.Add(objdivisa)

                            divisa = divisas.Where(Function(d) d.CodigoISO = objdivisa.CodigoISO).FirstOrDefault()

                        End If

                        ' Se não existe a lista de divisa
                        If divisa.Denominaciones Is Nothing Then
                            divisa.Denominaciones = New ObservableCollection(Of Comon.Clases.Denominacion)
                        End If
                        ' Recupera a denominação
                        Dim denominacion As Comon.Clases.Denominacion = divisa.Denominaciones.Where(Function(d) d.Codigo = declarado.CodigoDenominacion).FirstOrDefault

                        ' Se encontrou a denominação
                        If denominacion IsNot Nothing Then

                            ' Atualiza os dados da denominação
                            With denominacion
                                .ValorDenominacion = New ObservableCollection(Of Comon.Clases.ValorDenominacion) From
                                {
                                      New Comon.Clases.ValorDenominacion With
                                      {
                                          .Cantidad = declarado.Unidades,
                                          .TipoValor = Comon.Enumeradores.TipoValor.Declarado,
                                          .Importe = declarado.Unidades * denominacion.Valor
                                      }
                                }
                            End With

                        End If

                    End If
                Next

                ' Para cada divisa existente
                For Each div As Comon.Clases.Divisa In divisas

                    ' Remove as denominações sem valor
                    div.Denominaciones.RemoveAll(Function(d) d.ValorDenominacion Is Nothing)

                Next

            End If

        End Sub


    End Class

End Namespace