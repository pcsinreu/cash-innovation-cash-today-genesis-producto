Imports System.Globalization
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports System.Linq

Namespace GenesisSaldos
    Public Class SaldosPorPeriodo

        Public Shared Sub Recuperar(identificadorLlamada As String, peticion As RecuperarSaldosPeriodos.Peticion,
                            ByRef respuesta As RecuperarSaldosPeriodos.Respuesta,
                   Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ColectarPeticion(identificadorLlamada, peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                PoblarRespuesta(ds, respuesta)

                spw = Nothing
                ds.Dispose()

                log.AppendLine("Tiempo de acceso a datos (Poblar objecto de respuesta): " & Now.Subtract(TiempoParcial).ToString() & "; ")

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

        Private Shared Sub PoblarRespuesta(ds As DataSet, respuesta As RecuperarSaldosPeriodos.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("planificaciones") AndAlso ds.Tables("planificaciones").Rows.Count > 0 Then
                    Dim listaMaquinas As New List(Of String)
                    For Each unaFila In ds.Tables("planificaciones").Rows
                        If Not listaMaquinas.Contains(unaFila("DEVICEID").ToString()) Then
                            listaMaquinas.Add(unaFila("DEVICEID").ToString())
                        End If
                    Next


                    For Each deviceId In listaMaquinas
                        Dim maquina As New RecuperarSaldosPeriodos.Maquina
                        maquina.DeviceId = deviceId
                        'planificaciones
                        For Each rowPlanificacion In ds.Tables("planificaciones").Select("DEVICEID = '" & maquina.DeviceId & "'")
                            Dim planificacion = New RecuperarSaldosPeriodos.Planificacion

                            Util.AtribuirValorObjeto(planificacion.Codigo, rowPlanificacion("COD_PLAN"), GetType(String))
                            Util.AtribuirValorObjeto(planificacion.CodigoBanco, rowPlanificacion("COD_BANCO"), GetType(String))
                            Util.AtribuirValorObjeto(planificacion.CodigoTipoPlanificacion, rowPlanificacion("COD_TIPO"), GetType(String))
                            Util.AtribuirValorObjeto(planificacion.Descripcion, rowPlanificacion("DES_PLAN"), GetType(String))
                            Util.AtribuirValorObjeto(planificacion.DescripcionBanco, rowPlanificacion("DES_BANCO"), GetType(String))

                            'periodos
                            If ds.Tables.Contains("periodos") AndAlso ds.Tables("periodos").Rows.Count > 0 Then
                                For Each rowPeriodo In ds.Tables("periodos").Select("DEVICEID = '" & maquina.DeviceId & "' AND COD_PLAN = '" & planificacion.Codigo & "'")
                                    Dim periodo = New RecuperarSaldosPeriodos.Periodo
                                    Util.AtribuirValorObjeto(periodo.Identificador, rowPeriodo("IDENTIFICADOR"), GetType(String))
                                    Util.AtribuirValorObjeto(periodo.CodigoEstado, rowPeriodo("COD_ESTADO"), GetType(String))
                                    Util.AtribuirValorObjeto(periodo.Acreditado, rowPeriodo("BOL_ACREDITADO"), GetType(Boolean))
                                    Util.AtribuirValorObjeto(periodo.FechaHoraInicio, rowPeriodo("FYH_INICIO"), GetType(Date))
                                    Util.AtribuirValorObjeto(periodo.FechaHoraFin, rowPeriodo("FYH_FIN"), GetType(Date))

                                    'saldos
                                    If ds.Tables.Contains("saldos") AndAlso ds.Tables("saldos").Rows.Count > 0 Then
                                        For Each rowSaldo In ds.Tables("saldos").Select("IDENTIFICADOR = '" & periodo.Identificador & "'")
                                            Dim saldo = New RecuperarSaldosPeriodos.Saldo

                                            Util.AtribuirValorObjeto(saldo.CodigoDivisa, rowSaldo("COD_DIVISA"), GetType(String))
                                            Util.AtribuirValorObjeto(saldo.Importe, rowSaldo("IMPORTE"), GetType(Decimal))

                                            'saldos detalles
                                            If ds.Tables.Contains("saldosDetalles") AndAlso ds.Tables("saldosDetalles").Rows.Count > 0 Then
                                                For Each rowSaldoDetalle In ds.Tables("saldosDetalles").Select("IDENTIFICADOR = '" & periodo.Identificador & "' AND COD_DIVISA = '" & saldo.CodigoDivisa & "'")
                                                    Dim saldoDetalle = New RecuperarSaldosPeriodos.SaldoDetalle

                                                    Util.AtribuirValorObjeto(saldoDetalle.Cantidad, rowSaldoDetalle("CANTIDAD"), GetType(Integer))
                                                    Util.AtribuirValorObjeto(saldoDetalle.CodigoDenominacion, rowSaldoDetalle("COD_DENOMINACION"), GetType(String))
                                                    Util.AtribuirValorObjeto(saldoDetalle.CodigoPuntoServicio, rowSaldoDetalle("COD_PTO_SERVICIO"), GetType(String))
                                                    Util.AtribuirValorObjeto(saldoDetalle.CodigoSubCanal, rowSaldoDetalle("COD_SUBCANAL"), GetType(String))
                                                    Util.AtribuirValorObjeto(saldoDetalle.FechaContable, rowSaldoDetalle("FYH_CONTABLE"), GetType(Date))
                                                    Util.AtribuirValorObjeto(saldoDetalle.Importe, rowSaldoDetalle("IMPORTE"), GetType(Decimal))
                                                    saldo.Detalles.Add(saldoDetalle)
                                                Next
                                            End If
                                            periodo.Saldos.Add(saldo)
                                        Next
                                    End If
                                    planificacion.Periodos.Add(periodo)
                                Next
                            End If
                            maquina.Planificaciones.Add(planificacion)
                        Next
                        respuesta.Maquinas.Add(maquina)
                    Next

                End If

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next
                Else
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                    Dim unDetalle = New ContractoServicio.Contractos.Integracion.Comon.Detalle

                    Util.resultado(unDetalle.Codigo,
                       unDetalle.Descripcion,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Exito,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.RecuperarSaldosPeriodos,
                       "0000", "", True)

                    respuesta.Resultado.Detalles.Add(unDetalle)
                End If

            End If
        End Sub

        Private Shared Function ColectarPeticion(identificadorLlamada As String, peticion As RecuperarSaldosPeriodos.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecuperar_saldos_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)
            spw.AgregarParam("par$cod_banco", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoBanco, , False)
            spw.AgregarParam("par$cod_plan", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanificacion, , False)
            spw.AgregarParam("par$fyh_periodo", ProsegurDbType.Data_Hora, peticion.FechaHoraPeriodo, , False)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_est_periodo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$rc_plan", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificaciones")
            spw.AgregarParam("par$rc_periodos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "periodos")
            spw.AgregarParam("par$rc_saldos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "saldos")
            spw.AgregarParam("par$rc_saldos_det", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "saldosDetalles")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())

            spw.Param("par$acod_device_id").AgregarValorArray(String.Empty)
            spw.Param("par$acod_est_periodo").AgregarValorArray(String.Empty)


            If peticion.FechaHoraPeriodo <> DateTime.MinValue AndAlso Not String.IsNullOrEmpty(peticion.FechaHoraPeriodo.ToString("%K")) Then

                Dim GMTHoraLocalCalculado As String = Convert.ToInt32(peticion.FechaHoraPeriodo.ToString("zzz").Split(":")(0))
                Dim GMTMinutoLocalCalculado As String = Convert.ToInt32(peticion.FechaHoraPeriodo.ToString("zzz").Split(":")(1))
                GMTMinutoLocalCalculado += GMTHoraLocalCalculado * 60

                spw.AgregarParam("par$nel_gmt_minuto", ProsegurDbType.Inteiro_Curto, GMTMinutoLocalCalculado, , False)

            Else
                spw.AgregarParam("par$nel_gmt_minuto", ProsegurDbType.Inteiro_Curto, DBNull.Value, , False)

            End If


            For Each deviceId In peticion.DeviceIds.Distinct
                spw.Param("par$acod_device_id").AgregarValorArray(deviceId)

            Next

            For Each estadoPeriodo In peticion.CodigosEstadosPeriodo.Distinct
                spw.Param("par$acod_est_periodo").AgregarValorArray(estadoPeriodo)
            Next


            Return spw

        End Function
    End Class
End Namespace

