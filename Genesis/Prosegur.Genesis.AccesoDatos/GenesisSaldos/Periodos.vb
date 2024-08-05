Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon.Movimientos
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Text
Imports Prosegur.Genesis.Comon.Enumeradores

Namespace GenesisSaldos

    Public Class Periodos

        Public Shared Function RecuperarPeriodos(peticion As Prosegur.Genesis.Comon.Pantallas.Limites.Peticion) As Prosegur.Genesis.Comon.Pantallas.Limites.Respuesta

            Dim respuesta As Prosegur.Genesis.Comon.Pantallas.Limites.Respuesta = New Prosegur.Genesis.Comon.Pantallas.Limites.Respuesta()
            Try
                Dim ds As New DataSet()
                Dim spw As SPWrapper = armarWrapperRecuperarPeriodos(peticion)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta = PoblarRespuestaRecuperarPeriodos(ds)

            Catch ex As Exception
                Throw ex
            End Try


            Return respuesta
        End Function

        Private Shared Function PoblarRespuestaRecuperarPeriodos(ds As DataSet) As Prosegur.Genesis.Comon.Pantallas.Limites.Respuesta
            Dim respuesta As New Prosegur.Genesis.Comon.Pantallas.Limites.Respuesta
            Dim objPeriodoAcreditacionGrilla As Clases.PeriodoAcreditacionGrilla
            If ds IsNot Nothing Then
                Dim dtValidaciones As DataTable = Nothing
                Dim dtPeriodos As DataTable = Nothing

                If ds.Tables.Contains("periodos") Then
                    dtPeriodos = ds.Tables("periodos")
                End If

                If ds.Tables.Contains("validaciones") Then
                    dtValidaciones = ds.Tables("validaciones")
                End If

                If dtPeriodos IsNot Nothing Then
                    For Each fila As DataRow In dtPeriodos.Rows
                        objPeriodoAcreditacionGrilla = New Clases.PeriodoAcreditacionGrilla()

                        objPeriodoAcreditacionGrilla.Acreditado = Util.AtribuirValorObj(fila("BOL_ACREDITADO"), GetType(Boolean))
                        objPeriodoAcreditacionGrilla.Banco = Util.AtribuirValorObj(fila("BANCO"), GetType(String))
                        objPeriodoAcreditacionGrilla.DeviceId = Util.AtribuirValorObj(fila("DEVICEID"), GetType(String))
                        objPeriodoAcreditacionGrilla.Divisa = Util.AtribuirValorObj(fila("DIVISA"), GetType(String))
                        objPeriodoAcreditacionGrilla.OidPeriodo = Util.AtribuirValorObj(fila("OID_PERIODO"), GetType(String))
                        objPeriodoAcreditacionGrilla.Estado = Util.AtribuirValorObj(fila("ESTADO_PERIODO"), GetType(String))
                        objPeriodoAcreditacionGrilla.FyhInicio = Util.AtribuirValorObj(fila("FYH_INICIO"), GetType(Date))
                        objPeriodoAcreditacionGrilla.FyhFin = Util.AtribuirValorObj(fila("FYH_FIN"), GetType(Date))
                        objPeriodoAcreditacionGrilla.LimiteConfigurado = Util.AtribuirValorObj(fila("LIMITE_CONFIGURADO"), GetType(Decimal))
                        objPeriodoAcreditacionGrilla.Planificacion = Util.AtribuirValorObj(fila("PLANIFICACION"), GetType(String))
                        objPeriodoAcreditacionGrilla.TipoLimite = Util.AtribuirValorObj(fila("TIPO_LIMITE"), GetType(String))
                        objPeriodoAcreditacionGrilla.ValorActual = Util.AtribuirValorObj(fila("VALOR_ACTUAL"), GetType(Decimal))
                        objPeriodoAcreditacionGrilla.CodigoPais = Util.AtribuirValorObj(fila("COD_PAIS"), GetType(String))

                        respuesta.PeriodosAcreditacion.Add(objPeriodoAcreditacionGrilla)
                    Next
                End If

                If dtValidaciones IsNot Nothing Then

                    If Util.AtribuirValorObj(dtValidaciones.Rows(0)("CODIGO"), GetType(String)).ToString().ToUpper() = "ERROR" Then
                        Throw New Excepcion.NegocioExcepcion(Enumeradores.TipoResultado.ErrorAplicacion, "ERROR")
                    End If

                End If

            End If

            Return respuesta
        End Function

        Public Shared Function RecuperarEstadosPeriodos() As Dictionary(Of String, String)

            Dim mEstadosPeriodos As New Dictionary(Of String, String)
            Try

                Dim query As New StringBuilder
                Dim filtroWhere As New StringBuilder

                query.AppendLine("select estper.oid_estado_periodo, estper.cod_estado_periodo")
                query.AppendLine("from sapr_testado_periodo estper")

                filtroWhere.AppendLine(String.Format("where estper.cod_estado_periodo in ('{0}', '{1}')", EstadoPeriodo.Bloqueado.RecuperarValor, EstadoPeriodo.Desbloqueado.RecuperarValor))
                query.AppendLine(filtroWhere.ToString())



                Dim comando As IDbCommand = AcessoDados.CriarComando(AccesoDatos.Constantes.CONEXAO_GENESIS)
                comando.CommandText = query.ToString()

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(AccesoDatos.Constantes.CONEXAO_GENESIS, comando)
                Dim oidTabla As String
                Dim codTabla As String

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each fila As DataRow In dt.Rows
                        oidTabla = fila("OID_ESTADO_PERIODO").ToString
                        codTabla = fila("COD_ESTADO_PERIODO").ToString
                        mEstadosPeriodos.Add(oidTabla, codTabla)
                    Next
                End If
            Catch ex As Exception
                'No tratamos excepcion
            End Try

            Return mEstadosPeriodos
        End Function

        Private Shared Function armarWrapperRecuperarPeriodos(peticion As Prosegur.Genesis.Comon.Pantallas.Limites.Peticion) As SPWrapper

            Dim SP As String = String.Format("sapr_plimite_{0}.srecuperar_periodos_acred", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$aoid_estado_per", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$aoid_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$aoid_planificacion", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$aoid_banco", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)

            If peticion.FechaInicio.HasValue Then
                spw.AgregarParam("par$fyh_per_ini", ProsegurDbType.Identificador_Alfanumerico, peticion.FechaInicio.Value.ToString("yyyy-MM-dd HH:mm:ss"), , False)
            Else
                spw.AgregarParam("par$fyh_per_ini", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            End If

            If peticion.FechaFin.HasValue Then
                spw.AgregarParam("par$fyh_per_fin", ProsegurDbType.Identificador_Alfanumerico, peticion.FechaFin.Value.ToString("yyyy-MM-dd HH:mm:ss"), , False)
            Else
                spw.AgregarParam("par$fyh_per_fin", ProsegurDbType.Identificador_Alfanumerico, Nothing, , False)
            End If


            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.CodCultura, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser(), , False)

            spw.AgregarParam("par$rc_periodos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "periodos")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            If peticion IsNot Nothing Then
                If peticion.OidEstadosPeriodos IsNot Nothing AndAlso peticion.OidEstadosPeriodos.Count > 0 Then

                    spw.Param("par$aoid_estado_per").AgregarValorArray("")
                    For Each oid_estado_periodo In peticion.OidEstadosPeriodos.Distinct
                        spw.Param("par$aoid_estado_per").AgregarValorArray(oid_estado_periodo)
                    Next
                End If

                If peticion.OidBancos IsNot Nothing AndAlso peticion.OidBancos.Count > 0 Then
                    spw.Param("par$aoid_banco").AgregarValorArray("")

                    For Each oid_banco In peticion.OidBancos.Distinct
                        spw.Param("par$aoid_banco").AgregarValorArray(oid_banco)
                    Next
                End If

                If peticion.OidPlanificaciones IsNot Nothing AndAlso peticion.OidPlanificaciones.Count > 0 Then
                    spw.Param("par$aoid_planificacion").AgregarValorArray("")

                    For Each oid_planificacion In peticion.OidPlanificaciones.Distinct
                        spw.Param("par$aoid_planificacion").AgregarValorArray(oid_planificacion)
                    Next
                End If

                If peticion.OidDeviceIDs IsNot Nothing AndAlso peticion.OidDeviceIDs.Count > 0 Then
                    spw.Param("par$aoid_device_id").AgregarValorArray("")

                    For Each oid_maquina In peticion.OidDeviceIDs.Distinct
                        spw.Param("par$aoid_device_id").AgregarValorArray(oid_maquina)
                    Next
                End If
            End If

            Return spw

        End Function

        Public Shared Sub BuscarPeriodosMasivos(pPeticiones As PeticionPeriodosMasivos,
                                            ByRef respuesta As List(Of RespuestaPeriodosMasivos))
            Try
                Dim pDS = New DataSet()
                Dim spw As SPWrapper = coletarPeticion(pPeticiones)
                pDS = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)


                If pDS IsNot Nothing AndAlso pDS.Tables.Count > 0 AndAlso pDS.Tables.Contains("validaciones") Then
                    If pDS.Tables("validaciones").Rows.Count > 0 Then
                        If respuesta Is Nothing Then respuesta = New List(Of RespuestaPeriodosMasivos)


                        For Each objPeticion In pPeticiones.Movimientos
                            Dim objResp = New RespuestaPeriodosMasivos

                            objResp.DeviceID = objPeticion.DeviceID
                            objResp.CodigoCliente = objPeticion.CodigoCliente
                            objResp.CodigoSubCliente = objPeticion.CodigoSubCliente
                            objResp.CodigoPuntoServicio = objPeticion.CodigoPuntoServicio
                            objResp.CodigoCanal = objPeticion.CodigoCanal
                            objResp.CodigoSubCanal = objPeticion.CodigoSubCanal

                            For Each row As DataRow In pDS.Tables("validaciones").Select(String.Format("INDICE = '{0}' ", objPeticion.Index))
                                If objResp.validaciones Is Nothing Then objResp.validaciones = New List(Of Entidad)
                                Dim objEntidad = New Entidad
                                objResp.IdentificadorMaquina = Util.AtribuirValorObj(row("OID_MAQUINA"), GetType(String))
                                objResp.TipoResultado = Util.AtribuirValorObj(row("TIPO_RESULTADO"), GetType(String))

                                objEntidad.Codigo = Util.AtribuirValorObj(row("COD_VALIDACION"), GetType(String))
                                objEntidad.Descripcion = Util.AtribuirValorObj(row("DES_VALIDACION"), GetType(String))
                            Next

                            If pDS IsNot Nothing AndAlso pDS.Tables.Count > 0 AndAlso pDS.Tables.Contains("maquinas") Then
                                If pDS.Tables("maquinas").Rows.Count > 0 Then
                                    For Each row As DataRow In pDS.Tables("maquinas").Select(String.Format("OID_MAQUINA = '{0}' ", objResp.IdentificadorMaquina))
                                        objResp.CodigoSectorGenesis = Util.AtribuirValorObj(row("COD_SECTOR"), GetType(String))
                                        objResp.CodigoCodPlantaGenesis = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                                        objResp.CodigoDelegacionGenesis = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                                    Next
                                End If
                            End If
                            respuesta.Add(objResp)

                        Next
                    End If
                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub


        Private Shared Function coletarPeticion(pPeticiones As PeticionPeriodosMasivos) As SPWrapper
            Dim SP As String = String.Format("sapr_pperiodo_{0}.sverificar_periodo_masivos_ex", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$acod_index", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$acod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$acod_subcanal", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$acod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$acod_sector", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$acod_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$cod_ajeno", ProsegurDbType.Identificador_Alfanumerico, pPeticiones.CodigoAjeno, , False)
            spw.AgregarParam("par$afyh_gestion", ProsegurDbType.Data_Hora, Nothing,, True)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, pPeticiones.CodigoUsuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser(), , False)

            spw.AgregarParamInfo("par$info_ejecucion")

            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "maquinas")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            If pPeticiones IsNot Nothing AndAlso pPeticiones.Movimientos IsNot Nothing Then
                For Each unaPeticion In pPeticiones.Movimientos

                    spw.Param("par$acod_index").AgregarValorArray(unaPeticion.Index)
                    spw.Param("par$acod_canal").AgregarValorArray(unaPeticion.CodigoCanal)
                    spw.Param("par$acod_subcanal").AgregarValorArray(unaPeticion.CodigoSubCanal)
                    spw.Param("par$acod_divisa").AgregarValorArray(unaPeticion.CodigoDivisa)
                    spw.Param("par$acod_sector").AgregarValorArray(unaPeticion.DeviceID)

                    If Not String.IsNullOrWhiteSpace(unaPeticion.CodigoPuntoServicio) Then
                        spw.Param("par$acod_pto_servicio").AgregarValorArray(unaPeticion.CodigoPuntoServicio)
                    End If

                    spw.Param("par$afyh_gestion").AgregarValorArray(unaPeticion.FechaHora)

                Next
            End If

            Return spw
        End Function

        Shared Sub verificarPeriodo(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion,
                                    codigoUsuario As String,
                                    ByRef identificadorMaquina As String,
                                    ByRef identificadorPlanificacion As String,
                                    ByRef identificadorPeriodo As String,
                                    ByRef codigoTipoPlanificacion As String,
                                    ByRef validaciones As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            Try
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = ColectarVerificarPeriodo(identificadorLlamada, peticion, codigoUsuario)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("validaciones") Then
                    Dim rValidaciones As DataRow() = ds.Tables("validaciones").Select()

                    If rValidaciones IsNot Nothing AndAlso rValidaciones.Count > 0 Then

                        If validaciones Is Nothing Then validaciones = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                        For Each row As DataRow In rValidaciones
                            validaciones.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = Util.AtribuirValorObj(row(0), GetType(String)), .descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                        Next
                    End If

                End If

                identificadorMaquina = If(spw.Param("par$oid_maquina") Is DBNull.Value OrElse spw.Param("par$oid_maquina").Valor Is Nothing OrElse spw.Param("par$oid_maquina").Valor.ToString = "null", String.Empty, spw.Param("par$oid_maquina").Valor.ToString)
                identificadorPlanificacion = If(spw.Param("par$oid_planificacion") Is DBNull.Value OrElse spw.Param("par$oid_planificacion").Valor Is Nothing OrElse spw.Param("par$oid_planificacion").Valor.ToString = "null", String.Empty, spw.Param("par$oid_planificacion").Valor.ToString)
                identificadorPeriodo = If(spw.Param("par$oid_periodo") Is DBNull.Value OrElse spw.Param("par$oid_periodo").Valor Is Nothing OrElse spw.Param("par$oid_periodo").Valor.ToString = "null", String.Empty, spw.Param("par$oid_periodo").Valor.ToString)
                codigoTipoPlanificacion = If(spw.Param("par$cod_tipo_planificacion") Is DBNull.Value OrElse spw.Param("par$cod_tipo_planificacion").Valor Is Nothing OrElse spw.Param("par$cod_tipo_planificacion").Valor.ToString = "null", String.Empty, spw.Param("par$cod_tipo_planificacion").Valor.ToString)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try
        End Sub

        Private Shared Function ColectarVerificarPeriodo(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion, codigoUsuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pperiodo_{0}.sverificar_periodo_ex", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            If peticion.movimiento.destino IsNot Nothing AndAlso (peticion.movimiento.destino.codigoCliente = peticion.movimiento.origen.codigoCliente AndAlso
                    peticion.movimiento.destino.codigoSubCliente = peticion.movimiento.origen.codigoSubCliente AndAlso
                    peticion.movimiento.destino.codigoPuntoServicio = peticion.movimiento.origen.codigoPuntoServicio AndAlso
                    peticion.movimiento.destino.codigoDelegacion = peticion.movimiento.origen.codigoDelegacion AndAlso
                    peticion.movimiento.destino.codigoPlanta = peticion.movimiento.origen.codigoPlanta AndAlso
                    peticion.movimiento.destino.codigoSector = peticion.movimiento.origen.codigoSector AndAlso
                    peticion.movimiento.destino.codigoCanal = peticion.movimiento.origen.codigoCanal AndAlso
                    peticion.movimiento.destino.codigoSubCanal = peticion.movimiento.origen.codigoSubCanal) Then

                spw.AgregarParam("par$cod_canal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoCanal, , False)
                spw.AgregarParam("par$cod_subcanal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSubCanal, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSector, , False)
                spw.AgregarParam("par$cod_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoPuntoServicio, , False)

            Else


                spw.AgregarParam("par$cod_canal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoCanal, , False)
                spw.AgregarParam("par$cod_subcanal ", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSubCanal, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSector, , False)
                spw.AgregarParam("par$cod_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPuntoServicio, , False)


            End If

            spw.AgregarParam("par$cod_divisa", ProsegurDbType.Identificador_Alfanumerico, IIf(peticion.movimiento.valores.divisas.FirstOrDefault() IsNot Nothing, peticion.movimiento.valores.divisas.FirstOrDefault().codigoDivisa, String.Empty), , False)
            spw.AgregarParam("par$cod_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, peticion.movimiento.fechaHoraGestionFondos, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser(), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$oid_periodo", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$cod_tipo_planificacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Shared Sub generarPeriodo(peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion,
                                  codigoUsuario As String,
                                  ByRef identificadorPeriodo As String,
                                  ByRef validaciones As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError))

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = ColectarGenerarPeriodo(peticion, codigoUsuario)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("validaciones") Then
                    Dim rValidaciones As DataRow() = ds.Tables("validaciones").Select()

                    If rValidaciones IsNot Nothing AndAlso rValidaciones.Count > 0 Then

                        If validaciones Is Nothing Then validaciones = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                        For Each row As DataRow In rValidaciones
                            validaciones.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = Util.AtribuirValorObj(row(0), GetType(String)), .descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                        Next
                    End If

                End If

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

        End Sub

        Private Shared Function ColectarGenerarPeriodo(peticion As ContractoServicio.Contractos.Integracion.crearDocumentoFondos.Peticion, codigoUsuario As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pperiodo_{0}.sgenerar_periodo", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            If peticion.movimiento.destino IsNot Nothing AndAlso
                    (Not String.IsNullOrEmpty(peticion.movimiento.destino.codigoDelegacion) AndAlso
                     Not String.IsNullOrEmpty(peticion.movimiento.destino.codigoPlanta) AndAlso
                     Not String.IsNullOrEmpty(peticion.movimiento.destino.codigoSector)) Then

                spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoDelegacion, , False)
                spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoPlanta, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.destino.codigoSector, , False)

            Else

                spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoDelegacion, , False)
                spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoPlanta, , False)
                spw.AgregarParam("par$cod_sector", ProsegurDbType.Identificador_Alfanumerico, peticion.movimiento.origen.codigoSector, , False)

            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser(), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                                          "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                                          "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Public Shared Sub RelacionarDocumentosMAE(deviceID As String, fechaGestion As DateTime, Optional oidLlamada As String = Nothing)
            Dim SP As String = String.Format("sapr_pperiodo_{0}.srelacionar_documentos_mae", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, oidLlamada, , False)
            spw.AgregarParam("par$fechagestion", ProsegurDbType.Data_Hora, fechaGestion, , False)
            spw.AgregarParam("par$cod_identificacion", ProsegurDbType.Descricao_Longa, deviceID, , False)
            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
        End Sub

        Shared Sub generarPeriodos(identificadorMaquina As String,
                                   codigoUsuario As String,
                                   Optional oidLlamada As String = Nothing)
            Dim SP As String = String.Format("sapr_pperiodo_{0}.sgenerar_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, oidLlamada, , False)
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, identificadorMaquina, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser(), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, codigoUsuario, , False)
            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

        End Sub

#Region "ModificarPeriodos"
        ''' <summary>
        ''' Se encarga de modificar el estado de los periodos
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <param name="respuesta"></param>
        Public Shared Sub ModificarPeriodos(peticion As ContractoServicio.Contractos.Integracion.ModificarPeriodos.Peticion, respuesta As ContractoServicio.Contractos.Integracion.ModificarPeriodos.Respuesta)
            Try

                Dim objSPW As SPWrapper = armarWrapperModificarPeriodos(peticion)
                Dim objDS As DataSet = AccesoDB.EjecutarSP(objSPW, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)
                PoblarRespuestaModificarPeriodos(respuesta, objDS)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try
        End Sub
        Private Shared Function armarWrapperModificarPeriodos(peticion As ContractoServicio.Contractos.Integracion.ModificarPeriodos.Peticion) As SPWrapper
            Dim SP As String = String.Format("sapr_pperiodo_{0}.smodificar_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As SPWrapper = New SPWrapper(SP)
            Dim codigoUsuario As String = String.Empty

            Dim indice As Integer = 0

            If peticion IsNot Nothing AndAlso peticion.Configuracion IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(peticion.Configuracion.Usuario) Then
                codigoUsuario = peticion.Configuracion.Usuario
            End If


            spw.AgregarParam("par$aoid_periodo", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$aindex", ProsegurDbType.Inteiro_Curto, Nothing,, True)
            spw.AgregarParam("par$aaccion", ProsegurDbType.Descricao_Curta, Nothing,, True)
            spw.AgregarParam("par$acod_pais", ProsegurDbType.Descricao_Curta, Nothing,, True)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing,, True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Curta, codigoUsuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$rc_periodos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "periodos")

            If peticion IsNot Nothing AndAlso peticion.Periodos IsNot Nothing AndAlso peticion.Periodos.Count > 0 Then
                For Each elementoPeriodo In peticion.Periodos

                    spw.Param("par$aoid_periodo").AgregarValorArray(elementoPeriodo.Oid_Periodo)
                    spw.Param("par$aindex").AgregarValorArray(indice)
                    spw.Param("par$aaccion").AgregarValorArray(peticion.Accion.ToString())
                    spw.Param("par$acod_pais").AgregarValorArray(elementoPeriodo.Cod_Pais)
                    spw.Param("par$acod_device_id").AgregarValorArray(elementoPeriodo.DeviceID)

                    indice += 1
                Next
            End If


            Return spw
        End Function
        Private Shared Sub PoblarRespuestaModificarPeriodos(respuesta As ContractoServicio.Contractos.Integracion.ModificarPeriodos.Respuesta, objDS As DataSet)
            Dim unPeriodo As ContractoServicio.Contractos.Integracion.ModificarPeriodos.Salida.Periodo

            respuesta.Periodos = New List(Of ContractoServicio.Contractos.Integracion.ModificarPeriodos.Salida.Periodo)

            If objDS IsNot Nothing AndAlso objDS.Tables.Count > 0 AndAlso objDS.Tables.Contains("periodos") AndAlso objDS.Tables("periodos").Rows.Count > 0 Then
                Dim dt As DataTable = objDS.Tables("periodos")
                For Each fila As DataRow In dt.Rows
                    unPeriodo = New ContractoServicio.Contractos.Integracion.ModificarPeriodos.Salida.Periodo

                    unPeriodo.Oid_Periodo = Util.AtribuirValorObj(fila("OID_PERIODO"), GetType(String))
                    unPeriodo.Cod_Pais = Util.AtribuirValorObj(fila("COD_PAIS"), GetType(String))
                    unPeriodo.DeviceID = Util.AtribuirValorObj(fila("DEVICE_ID"), GetType(String))
                    unPeriodo.CodigoRespuesta = Util.AtribuirValorObj(fila("COD_RESPUESTA"), GetType(String))
                    unPeriodo.DescripcionRespuesta = Util.AtribuirValorObj(fila("DES_RESPUESTA"), GetType(String))


                    respuesta.Periodos.Add(unPeriodo)
                Next

            End If
        End Sub


#End Region

    End Class

End Namespace

