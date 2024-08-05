Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Paginacion
Imports System.Collections.ObjectModel


Namespace Genesis

    Public Class Planificacion

#Region "Grabar datos"

        Enum DiaDeLaSemana
            Lunes = 1
            Martes = 2
            Miercoles = 3
            Jueves = 4
            Viernes = 5
            Sabado = 6
            Domingo = 7
        End Enum

        Public Shared Sub BusquedaHorarioFinPeriodoActualIntensiva(planificacion As Clases.Planificacion, horarioActual As Integer, ByRef horaFin As Date?, ByRef horarioInicio As Integer, ByRef horarioFinal As Integer, numeroDeDia As Integer, ByRef proximoDiaConPlanificacion As Integer)
            If proximoDiaConPlanificacion.Equals(DiaDeLaSemana.Domingo) Then
                proximoDiaConPlanificacion = DiaDeLaSemana.Lunes
            Else
                proximoDiaConPlanificacion += 1
            End If

            While proximoDiaConPlanificacion <> numeroDeDia AndAlso Not horaFin.HasValue
                BuscarHorarioFinPeriodoActual(planificacion, horarioActual, horaFin, horarioInicio, horarioFinal, proximoDiaConPlanificacion, False)
                If proximoDiaConPlanificacion.Equals(DiaDeLaSemana.Domingo) Then
                    proximoDiaConPlanificacion = DiaDeLaSemana.Lunes
                Else
                    proximoDiaConPlanificacion += 1
                End If
            End While
        End Sub


        Public Shared Sub BuscarHorarioFinPeriodoActual(planificacion As Clases.Planificacion, horarioActual As Integer, ByRef horaFin As Date?, ByRef horarioInicio As Integer, ByRef horarioFinal As Integer, numeroDeDia As Integer, esPrimerDia As Boolean)
            For Each planhorario In planificacion.Programacion.Where(Function(x) x.NecDiaFin = numeroDeDia)
                horarioInicio = planhorario.FechaHoraInicio.ToString("HHmm")
                horarioFinal = planhorario.FechaHoraFin.ToString("HHmm")
                If esPrimerDia Then
                    If horarioActual <= horarioFinal AndAlso horarioActual >= horarioInicio Then
                        horaFin = planhorario.FechaHoraFin
                    End If
                Else
                    If Not horaFin.HasValue Then
                        horaFin = planhorario.FechaHoraFin
                    Else
                        If horaFin > planhorario.FechaHoraFin Then
                            horaFin = planhorario.FechaHoraFin
                        End If
                    End If

                End If

            Next
        End Sub

        Public Shared Sub GrabarPlanificacion(identificadorLlamada As String,
                                             ByRef planificacion As Clases.Planificacion,
                                              codigoUsuario As String,
                                              Optional pTransaccion As Transaccion = Nothing)

            Try

                Dim spw As SPWrapper = Nothing

                spw = ColectarPlanificacion(identificadorLlamada, planificacion, codigoUsuario)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, pTransaccion)

                '  planificacion.Identificador = If(spw.Param("par$oid_cod_ajeno").Valor IsNot Nothing, spw.Param("par$oid_cod_ajeno").Valor.ToString, If(planificacion.CodigoAjenoCliente IsNot Nothing, planificacion.CodigoAjenoCliente.Identificador, String.Empty))

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

        Private Shared Function ColectarPlanificacion(identificadorLlamada As String, ByRef planificacion As Clases.Planificacion,
                                                      codigoUsuario As String) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.sgrabar_planificacion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Objeto_Id, identificadorLlamada, , False)
            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Objeto_Id, planificacion.Identificador, ParameterDirection.InputOutput, False)
            spw.AgregarParam("par$cod_planificacion", ProsegurDbType.Descricao_Curta, planificacion.Codigo, , False)
            spw.AgregarParam("par$des_planificacion", ProsegurDbType.Descricao_Longa, planificacion.Descripcion, , False)
            spw.AgregarParam("par$oid_tipo_planificacion", ProsegurDbType.Objeto_Id, planificacion.TipoPlanificacion.Identificador, , False)
            spw.AgregarParam("par$cod_tipo_confirmacion", ProsegurDbType.Descricao_Curta, planificacion.PatronConfirmacion, , False)
            spw.AgregarParam("par$oid_cliente", ProsegurDbType.Objeto_Id, planificacion.Cliente.Identificador, , False)
            spw.AgregarParam("par$oid_delegacion", ProsegurDbType.Objeto_Id, planificacion.Delegacion.Identificador, , False)
            spw.AgregarParam("par$fyh_vigencia_inicio", ProsegurDbType.Data_Hora, planificacion.FechaHoraVigenciaInicio, , False)
            spw.AgregarParam("par$fyh_vigencia_fin", ProsegurDbType.Data_Hora, planificacion.FechaHoraVigenciaFin, , False)
            spw.AgregarParam("par$nec_contigencia", ProsegurDbType.Inteiro_Curto, planificacion.NecContigencia, , False)
            spw.AgregarParam("par$bol_activo", ProsegurDbType.Logico, If(planificacion.BolActivo, 1, 0), , False)
            spw.AgregarParam("par$bol_cambio_horario", ProsegurDbType.Logico, If(planificacion.BolCambioHorario, 1, 0), , False)
            spw.AgregarParam("par$bol_cambio_program", ProsegurDbType.Logico, If(planificacion.BolCambioHorarioProgramacion, 1, 0), , False)

            spw.AgregarParam("par$bol_controlafacturacion", ProsegurDbType.Logico, If(planificacion.BolControlaFacturacion, 1, 0), , False)
            If planificacion.BancoComision IsNot Nothing AndAlso planificacion.BancoComision.Identificador IsNot Nothing Then
                spw.AgregarParam("par$oid_banco_comision", ProsegurDbType.Objeto_Id, planificacion.BancoComision.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_banco_comision", ProsegurDbType.Objeto_Id, Nothing, , False)
            End If


            If planificacion.PorcComisionPlanificacion IsNot Nothing Then
                spw.AgregarParam("par$por_comision_plan", ProsegurDbType.Numero_Decimal, planificacion.PorcComisionPlanificacion.Value, , False)
            Else
                spw.AgregarParam("par$por_comision_plan", ProsegurDbType.Numero_Decimal, Nothing, , False)
            End If



            If planificacion.DiaCierreFacturacion IsNot Nothing Then
                spw.AgregarParam("par$dia_cierre_facturacion", ProsegurDbType.Inteiro_Curto, planificacion.DiaCierreFacturacion.Value, , False)
            Else
                spw.AgregarParam("par$dia_cierre_facturacion", ProsegurDbType.Inteiro_Curto, Nothing, , False)
            End If

            spw.AgregarParam("par$aoid_cod_ajeno", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aoid_tabla_genesis", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$acod_identificador", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_ajeno", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ades_ajeno", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abol_defecto", ProsegurDbType.Logico, Nothing, , True)

            For Each codAjeno In planificacion.CodigosAjeno

                spw.Param("par$aoid_cod_ajeno").AgregarValorArray(If(codAjeno IsNot Nothing AndAlso Not String.IsNullOrEmpty(codAjeno.Identificador), codAjeno.Identificador, DBNull.Value))
                spw.Param("par$aoid_tabla_genesis").AgregarValorArray(If(codAjeno IsNot Nothing AndAlso Not String.IsNullOrEmpty(codAjeno.IdentificadorTablaGenesis), codAjeno.IdentificadorTablaGenesis, DBNull.Value))
                spw.Param("par$acod_identificador").AgregarValorArray(If(codAjeno IsNot Nothing, codAjeno.CodigoIdentificador, String.Empty))
                spw.Param("par$acod_ajeno").AgregarValorArray(If(codAjeno IsNot Nothing AndAlso Not String.IsNullOrEmpty(codAjeno.Codigo), codAjeno.Codigo, DBNull.Value))
                spw.Param("par$ades_ajeno").AgregarValorArray(If(codAjeno IsNot Nothing AndAlso Not String.IsNullOrEmpty(codAjeno.Descripcion), codAjeno.Descripcion, DBNull.Value))
                spw.Param("par$abol_defecto").AgregarValorArray(If(codAjeno IsNot Nothing AndAlso codAjeno.EsDefecto, 1, 0))
            Next
            'lista dia fin , hora fin y maquinas
            spw.AgregarParam("par$aprog_num_dia_fin", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aprog_fyh_fechahora_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$amaq_oid_maquina", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$acan_oid_canal", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$ascan_oid_subcanal", ProsegurDbType.Objeto_Id, Nothing, , True)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, codigoUsuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser())
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Curto, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")
            spw.AgregarParam("par$bol_divide_subcanal", ProsegurDbType.Logico, If(planificacion.BolDividePorSubcanal, 1, 0), , False)
            spw.AgregarParam("par$bol_divide_divisa", ProsegurDbType.Logico, If(planificacion.BolDividePorDivisa, 1, 0), , False)
            spw.AgregarParam("par$bol_agrupa_subcanal", ProsegurDbType.Logico, If(planificacion.BolAgrupaPorSubCanal, 1, 0), , False)
            spw.AgregarParam("par$bol_agrupa_puntoservicio", ProsegurDbType.Logico, If(planificacion.BolAgrupaPorPuntoServicio, 1, 0), , False)
            spw.AgregarParam("par$bol_agrupa_fechacontable", ProsegurDbType.Logico, If(planificacion.BolAgrupaPorFechaContable, 1, 0), , False)


            If planificacion.Maquinas IsNot Nothing AndAlso planificacion.Maquinas.Count > 0 Then
                For Each maquina In planificacion.Maquinas
                    spw.Param("par$amaq_oid_maquina").AgregarValorArray(maquina.Identificador)
                Next
            End If

            If planificacion.Canales IsNot Nothing AndAlso planificacion.Canales.Count > 0 Then
                For Each canal In planificacion.Canales
                    spw.Param("par$acan_oid_canal").AgregarValorArray(canal.Identificador)

                    If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                        For Each subcanal In canal.SubCanales
                            spw.Param("par$ascan_oid_subcanal").AgregarValorArray(subcanal.Identificador)
                        Next
                    End If

                Next
            End If

            'Ordena os registros para salvar os horarios corretamente
            Dim listaOrdenada = planificacion.Programacion.OrderBy(Function(e) e.NecDiaFin).ThenBy(Function(e) e.FechaHoraFin).ToList

            If planificacion.Programacion IsNot Nothing AndAlso planificacion.Programacion.Count > 0 Then
                For Each prog In listaOrdenada
                    spw.Param("par$aprog_num_dia_fin").AgregarValorArray(prog.NecDiaFin)
                    spw.Param("par$aprog_fyh_fechahora_fin").AgregarValorArray(prog.FechaHoraFin)
                Next
            End If

            'TerminosXPlan_Patron
            spw.AgregarParam("par$aoid_iac_patron", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aoid_termino_patron", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avalor_termino_patron", ProsegurDbType.Descricao_Longa, Nothing, , True)

            If planificacion.CamposExtrasPatrones IsNot Nothing AndAlso planificacion.CamposExtrasPatrones.CamposExtras IsNot Nothing Then
                'Validar si tienen Valor 
                For Each patron In planificacion.CamposExtrasPatrones.CamposExtras.Where(Function(x) Not String.IsNullOrEmpty(x.Valor))
                    spw.Param("par$aoid_iac_patron").AgregarValorArray(planificacion.CamposExtrasPatrones.IdentificadorIAC)
                    spw.Param("par$aoid_termino_patron").AgregarValorArray(patron.Identificador)
                    spw.Param("par$avalor_termino_patron").AgregarValorArray(patron.Valor)
                Next
            End If

            'TerminosXPlan_Dinamico
            spw.AgregarParam("par$aoid_iac_dinam", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$aoid_termino_dinam", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$avalor_termino_dinam", ProsegurDbType.Descricao_Longa, Nothing, , True)

            If planificacion.CamposExtrasDinamicos IsNot Nothing AndAlso planificacion.CamposExtrasDinamicos.CamposExtras IsNot Nothing Then
                'Validar si tienen Valor 
                For Each Dinamico In planificacion.CamposExtrasDinamicos.CamposExtras.Where(Function(x) Not String.IsNullOrEmpty(x.Valor))
                    spw.Param("par$aoid_iac_dinam").AgregarValorArray(planificacion.CamposExtrasDinamicos.IdentificadorIAC)
                    spw.Param("par$aoid_termino_dinam").AgregarValorArray(Dinamico.Identificador)
                    spw.Param("par$avalor_termino_dinam").AgregarValorArray(Dinamico.Valor)
                Next
            End If

            'Limite
            spw.AgregarParam("par$a_lim_cod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_lim_num", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$a_lim_bol_baja", ProsegurDbType.Logico, Nothing, , True)
            If planificacion.Limites IsNot Nothing Then
                For Each limite In planificacion.Limites
                    spw.Param("par$a_lim_cod_divisa").AgregarValorArray(limite.Divisa.CodigoISO)
                    spw.Param("par$a_lim_num").AgregarValorArray(limite.NumLimite)
                    If Not String.IsNullOrWhiteSpace(limite.Accion) AndAlso limite.Accion.ToUpper = "BAJA" Then
                        spw.Param("par$a_lim_bol_baja").AgregarValorArray(1)
                    Else
                        spw.Param("par$a_lim_bol_baja").AgregarValorArray(0)
                    End If
                Next
            End If

            'Divisas
            spw.AgregarParam("par$a_per_div_codigo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_per_div_baja", ProsegurDbType.Logico, Nothing, , True)
            If planificacion.Divisas IsNot Nothing Then
                For Each divisa In planificacion.Divisas
                    spw.Param("par$a_per_div_codigo").AgregarValorArray(divisa.CodigoISO)
                    If Not String.IsNullOrWhiteSpace(divisa.Accion) AndAlso divisa.Accion.ToUpper = "BAJA" Then
                        spw.Param("par$a_per_div_baja").AgregarValorArray(1)
                    Else
                        spw.Param("par$a_per_div_baja").AgregarValorArray(0)
                    End If
                Next
            End If

            'Movimientos
            spw.AgregarParam("par$a_per_mov_codigo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_per_mov_baja", ProsegurDbType.Logico, Nothing, , True)
            If planificacion.Movimientos IsNot Nothing Then
                For Each movimiento In planificacion.Movimientos
                    spw.Param("par$a_per_mov_codigo").AgregarValorArray(movimiento.Identificador)
                    If Not String.IsNullOrWhiteSpace(movimiento.Accion) AndAlso movimiento.Accion.ToUpper = "BAJA" Then
                        spw.Param("par$a_per_mov_baja").AgregarValorArray(1)
                    Else
                        spw.Param("par$a_per_mov_baja").AgregarValorArray(0)
                    End If
                Next
            End If
            'Mensajes
            spw.AgregarParam("par$a_cod_mensaje", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_des_mensaje", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$a_tipo_mensaje", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$a_tipop_mensaje", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_sin_reintentos_mensaje", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            If planificacion.Mensajes IsNot Nothing Then
                For Each msg In planificacion.Mensajes
                    spw.Param("par$a_cod_mensaje").AgregarValorArray(msg.Codigo)
                    spw.Param("par$a_des_mensaje").AgregarValorArray(msg.Descripcion)
                    spw.Param("par$a_tipo_mensaje").AgregarValorArray(msg.Tipo)
                    spw.Param("par$a_tipop_mensaje").AgregarValorArray(msg.TipoPeriodo)
                    If msg.SinReintentos Then
                        spw.Param("par$a_sin_reintentos_mensaje").AgregarValorArray(1)
                    Else
                        spw.Param("par$a_sin_reintentos_mensaje").AgregarValorArray(0)
                    End If
                Next
            End If

            'Procesos
            spw.AgregarParam("par$aoid_proceso", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If planificacion.Procesos IsNot Nothing Then
                For Each Proc In planificacion.Procesos
                    spw.Param("par$aoid_proceso").AgregarValorArray(Proc.Identificador)
                Next
            End If


            Return spw

        End Function

        Public Shared Sub AtualizarFechaInicioVigencia(oidMaquina As String)
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.supd_planxmaquina_fecha_incio", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, oidMaquina, , False)
            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
        End Sub

        Public Shared Sub GrabarPlanPorCanal(ByRef pPlanificacion As Clases.Planificacion,
                                                ByRef pPlanes As ObservableCollection(Of Clases.PlanMaqPorCanalSubCanalPunto),
                                             ByRef pMaquina As Clases.Maquina,
                                                pCodigoUsuario As String,
                                             objTransaccion As Transaccion)

            Dim SP As String = String.Format("sapr_pplanificacion_{0}.supd_planxcanales", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            Dim strOidMaquina As String = pMaquina.Identificador

            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Identificador_Alfanumerico, pPlanificacion.Identificador, , False)
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, strOidMaquina, , False)
            spw.AgregarParam("par$apto_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$apto_oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acan_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$acan_oid_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, pCodigoUsuario, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$inserts", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            If pPlanes IsNot Nothing AndAlso pPlanes.Count > 0 Then
                'Inicializo el indice en 1
                Dim indice As Integer = 1

                For Each unPlan As Clases.PlanMaqPorCanalSubCanalPunto In pPlanes
                    If unPlan.PuntosServicios IsNot Nothing AndAlso unPlan.PuntosServicios.Count > 0 Then

                        For Each punto In unPlan.PuntosServicios
                            spw.Param("par$apto_nel_index").AgregarValorArray(indice)
                            spw.Param("par$apto_oid_pto_servicio").AgregarValorArray(punto.Identificador)
                        Next
                    Else
                        spw.Param("par$apto_nel_index").AgregarValorArray(indice)
                        spw.Param("par$apto_oid_pto_servicio").AgregarValorArray(DBNull.Value)
                    End If

                    If unPlan.Canales IsNot Nothing AndAlso unPlan.Canales.Count > 0 Then


                        For Each canal In unPlan.Canales
                            spw.Param("par$acan_nel_index").AgregarValorArray(indice)
                            spw.Param("par$acan_oid_canal").AgregarValorArray(canal.Identificador)
                        Next

                    Else
                        spw.Param("par$acan_nel_index").AgregarValorArray(indice)
                        spw.Param("par$acan_oid_canal").AgregarValorArray(DBNull.Value)
                    End If

                    indice += 1
                Next
                If objTransaccion IsNot Nothing Then
                    AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, objTransaccion)
                Else
                    AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                End If
                GrabarPlanPorSubCanal(pPlanificacion, pPlanes, pMaquina, pCodigoUsuario)
            End If
        End Sub

        Public Shared Sub GrabarPlanPorSubCanal(ByRef pPlanificacion As Clases.Planificacion,
                                                ByRef pPlanes As ObservableCollection(Of Clases.PlanMaqPorCanalSubCanalPunto),
                                             ByRef pMaquina As Clases.Maquina,
                                                pCodigoUsuario As String)
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.supd_planxsubcanales", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            Dim strOidMaquina As String = pMaquina.Identificador

            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Identificador_Alfanumerico, pPlanificacion.Identificador, , False)
            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, strOidMaquina, , False)
            spw.AgregarParam("par$apto_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$apto_oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ascan_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$ascan_oid_subcanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, pCodigoUsuario, , False)
            spw.AgregarParam("par$selects", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$inserts", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$updates", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            If pPlanes IsNot Nothing AndAlso pPlanes.Count > 0 Then
                'Inicializo el indice en 1
                Dim indice As Integer = 1

                For Each unPlan As Clases.PlanMaqPorCanalSubCanalPunto In pPlanes

                    If unPlan.PuntosServicios IsNot Nothing AndAlso unPlan.PuntosServicios.Count > 0 Then

                        For Each punto In unPlan.PuntosServicios
                            spw.Param("par$apto_nel_index").AgregarValorArray(indice)
                            spw.Param("par$apto_oid_pto_servicio").AgregarValorArray(punto.Identificador)
                        Next
                    Else
                        spw.Param("par$apto_nel_index").AgregarValorArray(indice)
                        spw.Param("par$apto_oid_pto_servicio").AgregarValorArray(DBNull.Value)
                    End If

                    If unPlan.Canales IsNot Nothing AndAlso unPlan.Canales.Count > 0 Then

                        For Each canal In unPlan.Canales

                            If canal.SubCanales IsNot Nothing AndAlso canal.SubCanales.Count > 0 Then
                                For Each subcanal In canal.SubCanales
                                    spw.Param("par$ascan_nel_index").AgregarValorArray(indice)
                                    spw.Param("par$ascan_oid_subcanal").AgregarValorArray(subcanal.Identificador)
                                Next
                            End If
                        Next
                    End If

                    indice += 1
                Next

                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
            End If


        End Sub

        '/*reponsable por quitar quando cambia planificacion*/
        Public Shared Sub DelPlanX_CanalMAE(ByRef pPlanificacion As Clases.Planificacion,
                                            ByRef pMaquina As Clases.Maquina,
                                            ByRef pCodUsuario As String,
                                            Optional pTransaccion As Transaccion = Nothing)
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.sdel_planxcan_maqu", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            If pPlanificacion IsNot Nothing AndAlso String.IsNullOrWhiteSpace(pPlanificacion.Identificador) Then
                spw.AgregarParam("par$oid_plan_nueva", ProsegurDbType.Identificador_Alfanumerico, pPlanificacion.Identificador, , False)
            Else
                spw.AgregarParam("par$oid_plan_nueva", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value, , False)
            End If

            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, pMaquina.Identificador, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, pCodUsuario, , False)


            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, pTransaccion)

        End Sub

        '/*reponsable por quitar quando cambia punto servicio*/
        Public Shared Sub DelPlanX_CanalPuntos(ByRef pPuntosServicios As ObservableCollection(Of Clases.PuntoServicio),
                                              ByRef pMaquina As Clases.Maquina,
                                               ByRef pCodUsuario As String,
                                            Optional pTransaccion As Transaccion = Nothing)
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.sdel_planxcan_ptos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, True)

            spw.AgregarParam("par$oid_maquina", ProsegurDbType.Identificador_Alfanumerico, pMaquina.Identificador, , False)
            spw.AgregarParam("par$aoid_pto_servicios", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, pCodUsuario, , False)

            If pPuntosServicios IsNot Nothing AndAlso pPuntosServicios.Count > 0 Then
                For Each punto In pPuntosServicios
                    spw.Param("par$aoid_pto_servicios").AgregarValorArray(punto.Identificador)
                Next
            End If


            AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, pTransaccion)
        End Sub

#End Region

#Region "Consulta"

        Public Shared Sub Recuperar(peticion As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Peticion,
                                    ByRef respuesta As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Respuesta)

            Try
                'srecuperar_maquina
                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarPeticion(peticion)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                PoblarRespuesta(ds, respuesta)

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

        Private Shared Function ColectarPeticion(peticion As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PPLANIFICACION_{0}.srecuperar_planificacion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$cods_planificacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_tipo", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoTipo, , False)
            spw.AgregarParam("par$cod_banco", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoBanco, , False)
            spw.AgregarParam("par$cod_vigente", ProsegurDbType.Identificador_Alfanumerico, peticion.Vigente.RecuperarValor.ToString, , False)
            spw.AgregarParam("par$bol_recuperar_maquinas", ProsegurDbType.Logico, peticion.RecuperarMaquinas, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificaciones")
            spw.AgregarParam("par$rc_canales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "canales")
            spw.AgregarParam("par$rc_programaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "programaciones")
            spw.AgregarParam("par$rc_maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "maquinas")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_limites", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "limites")
            spw.AgregarParamInfo("par$info_ejecucion")

            For Each codigo In peticion.Codigos
                spw.Param("par$cods_planificacion").AgregarValorArray(codigo)
            Next

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw
        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           ByRef respuesta As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                ' Planificaciones
                If ds.Tables.Contains("planificaciones") AndAlso ds.Tables("planificaciones").Rows.Count > 0 Then

                    If respuesta.Planificaciones Is Nothing Then respuesta.Planificaciones = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Planificacione)

                    For Each rowPlanificacion As DataRow In ds.Tables("planificaciones").Rows

                        Dim planificacion As New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Planificacione

                        With planificacion

                            .Identificador = Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_PLANIFICACION"), GetType(String))
                            .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_PLANIFICACION"), GetType(String))
                            .FechaHoraVigenciaInicio = Util.AtribuirValorObj(rowPlanificacion("FYH_VIGENCIA_INICIO"), GetType(String))
                            .FechaHoraVigenciaFin = Util.AtribuirValorObj(rowPlanificacion("FYH_VIGENCIA_FIN"), GetType(String))
                            .Vigente = Util.AtribuirValorObj(rowPlanificacion("BOL_ACTIVO"), GetType(String))
                            .MinutosAcreditacion = Util.AtribuirValorObj(rowPlanificacion("NEC_CONTINGENCIA"), GetType(String))

                            .Tipo = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_TIPO_PLANIFICACION"), GetType(String)),
                                                                                                     .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_TIPO_PLANIFICACION"), GetType(String))}

                            .Banco = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_CLIENTE"), GetType(String)),
                                                                                                      .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_CLIENTE"), GetType(String))}

                            .Deleagacion = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_DELEGACION"), GetType(String)),
                                                                                                            .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_DELEGACION"), GetType(String))}

                            ' canales
                            If ds.Tables.Contains("canales") AndAlso ds.Tables("canales").Rows.Count > 0 Then

                                Dim rCanales As DataRow() = ds.Tables("canales").Select("OID_PLANIFICACION ='" & .Identificador & "'")

                                If rCanales IsNot Nothing AndAlso rCanales.Count > 0 Then

                                    .Canales = New List(Of ContractoServicio.Contractos.Integracion.Comon.Entidad)

                                    For Each rowCanal As DataRow In rCanales

                                        Dim canal As New ContractoServicio.Contractos.Integracion.Comon.Entidad
                                        canal.Codigo = Util.AtribuirValorObj(rowCanal("COD_CANAL"), GetType(String))
                                        canal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_CANAL"), GetType(String))

                                        .Canales.Add(canal)
                                    Next

                                End If

                            End If

                            ' programaciones
                            If ds.Tables.Contains("programaciones") AndAlso ds.Tables("programaciones").Rows.Count > 0 Then

                                Dim rProgramaciones As DataRow() = ds.Tables("programaciones").Select("OID_PLANIFICACION ='" & .Identificador & "'")

                                If rProgramaciones IsNot Nothing AndAlso rProgramaciones.Count > 0 Then

                                    .Programaciones = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Programacion)

                                    For Each rowProgramacion As DataRow In rProgramaciones

                                        Dim programacion As New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Programacion

                                        With programacion

                                            .HoraInicio = Util.AtribuirValorObj(rowProgramacion("FYH_HORA_INICIO"), GetType(DateTime))
                                            .DiaInicio = Util.AtribuirValorObj(rowProgramacion("NEC_DIA_INICIO"), GetType(Integer))
                                            .HoraFin = Util.AtribuirValorObj(rowProgramacion("FYH_HORA_FIN"), GetType(DateTime))
                                            .DiaFin = Util.AtribuirValorObj(rowProgramacion("NEC_DIA_FIN"), GetType(Integer))

                                        End With

                                        .Programaciones.Add(programacion)

                                    Next

                                End If

                            End If

                            ' maquinas
                            If ds.Tables.Contains("maquinas") AndAlso ds.Tables("maquinas").Rows.Count > 0 Then

                                Dim rMaquinas As DataRow() = ds.Tables("maquinas").Select("OID_PLANIFICACION ='" & .Identificador & "'")

                                If rMaquinas IsNot Nothing AndAlso rMaquinas.Count > 0 Then

                                    .Maquinas = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Maquina)

                                    For Each rowMaquina As DataRow In rMaquinas

                                        Dim maquina As New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Maquina

                                        With maquina

                                            .DeviceID = Util.AtribuirValorObj(rowMaquina("COD_IDENTIFICACION"), GetType(String))
                                            .Vigente = Util.AtribuirValorObj(rowMaquina("BOL_ACTIVO"), GetType(Boolean))

                                            .PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_PTO_SERVICIO"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_PTO_SERVICIO"), GetType(String))}

                                            .SubCliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_SUBCLIENTE"), GetType(String)),
                                                                                                                            .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_SUBCLIENTE"), GetType(String))}

                                            .Cliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_CLIENTE"), GetType(String)),
                                                                                                                            .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_CLIENTE"), GetType(String))}


                                        End With

                                        .Maquinas.Add(maquina)

                                    Next

                                End If

                            End If

                            ' limites
                            If ds.Tables.Contains("limites") AndAlso ds.Tables("limites").Rows.Count > 0 Then
                                Dim rLimites As DataRow() = ds.Tables("limites").Select("OID_PLANIFICACION ='" & .Identificador & "'")
                                If rLimites IsNot Nothing AndAlso rLimites.Count > 0 Then
                                    .Limites = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Limite)()
                                    Dim objLimite As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Limite
                                    For Each rowLimite As DataRow In rLimites
                                        objLimite = New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Limite()

                                        objLimite.Valor = Util.AtribuirValorObj(rowLimite("VALOR"), GetType(Decimal))

                                        objLimite.Divisa = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {
                                        .Codigo = Util.AtribuirValorObj(rowLimite("COD_ISO_DIVISA"), GetType(String)),
                                        .Descripcion = Util.AtribuirValorObj(rowLimite("DES_DIVISA"), GetType(String))
                                        }

                                        Dim codigoPtoServicio As String = Util.AtribuirValorObj(rowLimite("COD_PTO_SERVICIO"), GetType(String))
                                        Dim descriPtoServicio As String = Util.AtribuirValorObj(rowLimite("COD_PTO_SERVICIO"), GetType(String))
                                        If Not String.IsNullOrWhiteSpace(codigoPtoServicio) OrElse Not String.IsNullOrWhiteSpace(descriPtoServicio) Then
                                            objLimite.PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {
                                            .Codigo = codigoPtoServicio,
                                            .Descripcion = descriPtoServicio
                                            }
                                        End If

                                        Dim codigoMaquina As String = Util.AtribuirValorObj(rowLimite("DEVICEID"), GetType(String))
                                        Dim descriMaquina As String = Util.AtribuirValorObj(rowLimite("DES_SECTOR"), GetType(String))
                                        If Not String.IsNullOrWhiteSpace(codigoMaquina) OrElse Not String.IsNullOrWhiteSpace(descriMaquina) Then
                                            objLimite.PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {
                                                .Codigo = codigoMaquina,
                                                .Descripcion = descriMaquina
                                               }
                                        End If

                                        .Limites.Add(objLimite)
                                    Next
                                End If
                            End If

                            ' divisas
                            If ds.Tables.Contains("divisas") AndAlso ds.Tables("divisas").Rows.Count > 0 Then
                                Dim rDivisas As DataRow() = ds.Tables("divisas").Select("OID_PLANIFICACION ='" & .Identificador & "'")
                                If rDivisas IsNot Nothing AndAlso rDivisas.Count > 0 Then
                                    .Divisas = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Divisa)()
                                    Dim objDivisa As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Divisa
                                    For Each rowDivisa As DataRow In rDivisas
                                        objDivisa = New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Divisa() With {
                                            .CodigoISO = Util.AtribuirValorObj(rowDivisa("COD_ISO_DIVISA"), GetType(String)),
                                            .Descripcion = Util.AtribuirValorObj(rowDivisa("DES_DIVISA"), GetType(String))
                                            }
                                        .Divisas.Add(objDivisa)
                                    Next
                                End If
                            End If

                            ' movimientos
                            If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 Then
                                Dim rMovimientos As DataRow() = ds.Tables("movimientos").Select("OID_PLANIFICACION ='" & .Identificador & "'")
                                If rMovimientos IsNot Nothing AndAlso rMovimientos.Count > 0 Then
                                    .Movimientos = New List(Of ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Formulario)()
                                    Dim objMovimiento As ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Formulario
                                    For Each rowMovimiento As DataRow In rMovimientos
                                        objMovimiento = New ContractoServicio.Contractos.Integracion.RecuperarPlanificaciones.Formulario() With {
                                            .Identificador = Util.AtribuirValorObj(rowMovimiento("OID_FORMULARIO"), GetType(String)),
                                            .Codigo = Util.AtribuirValorObj(rowMovimiento("COD_FORMULARIO"), GetType(String)),
                                            .Descripcion = Util.AtribuirValorObj(rowMovimiento("DES_FORMULARIO"), GetType(String))
                                            }
                                        .Movimientos.Add(objMovimiento)
                                    Next
                                End If
                            End If
                        End With

                        respuesta.Planificaciones.Add(planificacion)

                    Next

                End If

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next

                End If


            End If
        End Sub

        Public Shared Function PesquisarPlanificacion(peticion As PeticionHelper, ByRef paramRespuestaPaginacion As ParametrosRespuestaPaginacion) As List(Of Helper.Respuesta)

            Dim query As New StringBuilder
            Dim dtResultado As DataTable

            ' Obtem Query.
            query.Append(My.Resources.BuscaPlanificacion.ToString())

            ' Realiza Pesquisa.
            dtResultado = HelperBuscaDatos.BuscaDatos(query, peticion, paramRespuestaPaginacion, "COD_PLANIFICACION", "DES_PLANIFICACION", "COD_PLANIFICACION")

            ' Retorna lista contendo dados de Respuesta Planificacion.
            Return HelperBuscaDatos.ListaDatosRespuesta(dtResultado)

        End Function

        Public Shared Function RecuperarPlanificacionDetalle(oidPlanificacion As String, codigoUsuario As String, CodigoIdentificadorAjeno As String) As Clases.Planificacion

            Dim _planificaciones As Clases.Planificacion = Nothing

            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColectarPlanificacionRecuperar(oidPlanificacion, codigoUsuario, CodigoIdentificadorAjeno)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                _planificaciones = poblarPlanificaciones(ds)

            Catch ex As Exception
                Throw ex
            End Try

            Return _planificaciones
        End Function


        Public Shared Function ColectarPlanificacionRecuperar(oidPlanificacion As String,
                                                       usuario As String,
                                                       CodigoIdentificadorAjeno As String) As SPWrapper

            Dim SP As String = String.Format("sapr_pplanificacion_{0}.sbusqueda_planificacion", Prosegur.Genesis.Comon.Util.Version)

            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Objeto_Id, oidPlanificacion, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_identificador_ajeno", ParamTypes.String, CodigoIdentificadorAjeno, ParameterDirection.Input, False)
            spw.AgregarParam("par$ele_rc_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_planificacion")
            spw.AgregarParam("par$ele_rc_maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_maquinas")
            spw.AgregarParam("par$ele_rc_programaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_programaciones")
            spw.AgregarParam("par$ele_rc_canales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_canales")
            spw.AgregarParam("par$ele_rc_subcanales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_subcanales")
            spw.AgregarParam("par$aje_rc_codigo_ajeno", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "aje_rc_codigo_ajeno")
            spw.AgregarParam("par$ele_rc_terminos_patron", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_patron")
            spw.AgregarParam("par$ele_rc_terminos_dinam", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_terminos_dinam")
            spw.AgregarParam("par$ele_rc_mensajes", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_mensajes")
            spw.AgregarParam("par$ele_rc_procesos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ele_rc_procesos")

            spw.AgregarParam("par$cod_usuario", ParamTypes.String, usuario, ParameterDirection.Input, False)

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$selects", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")



            Return spw
        End Function

        Public Shared Function poblarPlanificaciones(ds As DataSet) As Clases.Planificacion

            Dim objPlanificacion As New Clases.Planificacion

            If ds.Tables.Contains("ele_rc_planificacion") AndAlso ds.Tables("ele_rc_planificacion").Rows.Count > 0 AndAlso
                ds.Tables.Contains("ele_rc_programaciones") AndAlso ds.Tables("ele_rc_programaciones").Rows.Count > 0 Then

                Dim dtPlanificacion As DataTable = ds.Tables("ele_rc_planificacion")
                Dim dtMaquinas As DataTable = ds.Tables("ele_rc_maquinas")
                Dim dtProgramaciones As DataTable = ds.Tables("ele_rc_programaciones")
                Dim dtCanales As DataTable = ds.Tables("ele_rc_canales")
                Dim dtSubCanales As DataTable = ds.Tables("ele_rc_subcanales")
                Dim dtAjenos As DataTable = ds.Tables("aje_rc_codigo_ajeno")
                Dim dtCodigosExtrasDinamicos As DataTable = ds.Tables("ele_rc_terminos_dinam")
                Dim dtCodigosExtrasPatron As DataTable = ds.Tables("ele_rc_terminos_patron")
                Dim objTermino As Clases.Termino

                'Planificacion
                With objPlanificacion
                    .Identificador = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_PLANIFICACION"), GetType(String))
                    .NecContigencia = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("NEC_CONTINGENCIA"), GetType(Integer))
                    .Descripcion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("DES_PLANIFICACION"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("COD_PLANIFICACION"), GetType(String))
                    .BolActivo = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    .FechaHoraVigenciaFin = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("FYH_VIGENCIA_FIN"), GetType(DateTime))
                    .FechaHoraVigenciaInicio = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("FYH_VIGENCIA_INICIO"), GetType(DateTime))
                    .TipoPlanificacion = New Clases.TipoPlanificacion
                    .TipoPlanificacion.Identificador = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_TIPO_PLANIFICACION"), GetType(String))
                    .TipoPlanificacion.Descripcion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("DES_TIPO_PLANIFICACION"), GetType(String))
                    .PatronConfirmacion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("COD_TIPO_CONFIRMACION"), GetType(String))
                    .Delegacion = New Clases.Delegacion With {
                        .Identificador = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_DELEGACION"), GetType(String))
                    }
                    .Cliente = New Clases.Cliente With {
                        .Codigo = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("COD_CLIENTE"), GetType(String)),
                        .Identificador = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_CLIENTE"), GetType(String)),
                        .Descripcion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("DES_CLIENTE"), GetType(String))
                    }

                    .BolControlaFacturacion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_CONTROLA_FACTURACION"), GetType(Boolean))
                    .BolAgrupaPorSubCanal = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_AGR_SUBCANAL"), GetType(Boolean))
                    .BolAgrupaPorPuntoServicio = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_AGR_PTO_SERVICIO"), GetType(Boolean))
                    .BolAgrupaPorFechaContable = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_AGR_FEC_CONTABLE"), GetType(Boolean))
                    .BolDividePorSubcanal = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_PERIODO_SUBCANAL"), GetType(Boolean))
                    .BolDividePorDivisa = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_PERIODO_DIVISA"), GetType(Boolean))
                    .BolDividePorPto = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("BOL_PERIODO_PTO_SERVICIO"), GetType(Boolean))
                    .Mensajes = New List(Of Clases.MensajeDePlanificacion)()
                    .Procesos = New List(Of Clases.Proceso)()
                    If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dtPlanificacion.Rows(0)("NUM_PORCENT_COMISION"), GetType(String))) Then

                        .PorcComisionPlanificacion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("NUM_PORCENT_COMISION"), GetType(Decimal))
                    End If

                    If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dtPlanificacion.Rows(0)("NEC_DIA_CIERRE"), GetType(String))) Then

                        .DiaCierreFacturacion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("NEC_DIA_CIERRE"), GetType(Integer))
                    End If

                    If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_CLIENTE_COMISION"), GetType(String))) Then
                        .BancoComision = New Clases.Cliente
                        .BancoComision.Codigo = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("COD_CLIENTE_COMISION"), GetType(String))
                        .BancoComision.Identificador = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("OID_CLIENTE_COMISION"), GetType(String))
                        .BancoComision.Descripcion = Util.AtribuirValorObj(dtPlanificacion.Rows(0)("DES_CLIENTE_COMISION"), GetType(String))
                    End If

                End With



                'Programaciones
                objPlanificacion.Programacion = New List(Of Clases.PlanXProgramacion)
                objPlanificacion.ProgramacionOriginal = New List(Of Clases.PlanXProgramacion)

                For Each dr In dtProgramaciones.Rows

                    Dim planProg As New Clases.PlanXProgramacion

                    Util.AtribuirValorObjeto(planProg.Identificador, dr("OID_PLANXPROGRAMACION"), GetType(String))
                    Util.AtribuirValorObjeto(planProg.FechaHoraFin, dr("FYH_HORA_FIN"), GetType(DateTime))
                    Util.AtribuirValorObjeto(planProg.NecDiaFin, dr("NEC_DIA_FIN"), GetType(Integer))
                    objPlanificacion.Programacion.Add(planProg)
                    objPlanificacion.ProgramacionOriginal.Add(planProg.Clonar)

                Next
                objPlanificacion.NecQtdeProgramaciones = dtProgramaciones.Rows.Count

                'Maquinas
                objPlanificacion.Maquinas = New List(Of Clases.Maquina)

                For Each dr In dtMaquinas.Rows

                    Dim objMaquina As New Clases.Maquina

                    Util.AtribuirValorObjeto(objMaquina.Identificador, dr("OID_MAQUINA"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.Codigo, dr("COD_IDENTIFICACION"), GetType(String))

                    objMaquina.Sector = New Clases.Sector
                    Util.AtribuirValorObjeto(objMaquina.Sector.Identificador, dr("OID_SECTOR"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.Sector.Descripcion, dr("DES_SECTOR"), GetType(String))

                    'objMaquina.Modelo = New Clases.Modelo
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Identificador, dr("OID_MODELO"), GetType(String))
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Codigo, dr("COD_MODELO"), GetType(String))
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Descripcion, dr("DES_MODELO"), GetType(String))

                    'objMaquina.Modelo.Fabricante = New Clases.Fabricante
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Identificador, dr("OID_FABRICANTE"), GetType(String))
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Codigo, dr("COD_FABRICANTE"), GetType(String))
                    'Util.AtribuirValorObjeto(objMaquina.Modelo.Fabricante.Descripcion, dr("DES_FABRICANTE"), GetType(String))


                    objMaquina.Cliente = New Clases.Cliente
                    Util.AtribuirValorObjeto(objMaquina.Cliente.Identificador, dr("OID_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.Cliente.Codigo, dr("COD_CLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.Cliente.Descripcion, dr("DES_CLIENTE"), GetType(String))

                    objMaquina.SubCliente = New Clases.SubCliente
                    Util.AtribuirValorObjeto(objMaquina.SubCliente.Identificador, dr("OID_SUBCLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.SubCliente.Codigo, dr("COD_SUBCLIENTE"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.SubCliente.Descripcion, dr("DES_SUBCLIENTE"), GetType(String))

                    objMaquina.PtoServicio = New Clases.PuntoServicio
                    Util.AtribuirValorObjeto(objMaquina.PtoServicio.Identificador, dr("OID_PTO_SERVICIO"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.PtoServicio.Codigo, dr("COD_PTO_SERVICIO"), GetType(String))
                    Util.AtribuirValorObjeto(objMaquina.PtoServicio.Descripcion, dr("DES_PTO_SERVICIO"), GetType(String))


                    Util.AtribuirValorObjeto(objMaquina.BolActivo, dr("BOL_ACTIVO"), GetType(String))

                    'If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(String))) Then

                    '    'objMaquina.NumPorcentComision = Util.AtribuirValorObj(dr("NUM_PORCENT_COMISION"), GetType(Decimal))
                    'End If

                    objPlanificacion.Maquinas.Add(objMaquina)

                Next

                'Canales
                objPlanificacion.Canales = New List(Of Clases.Canal)

                For Each dr In dtCanales.Rows
                    Dim canal As New Clases.Canal
                    Util.AtribuirValorObjeto(canal.Identificador, dr("OID_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(canal.EstaActivo, dr("BOL_ACTIVO"), GetType(String))
                    Util.AtribuirValorObjeto(canal.Descripcion, dr("DES_CANAL"), GetType(String))
                    Util.AtribuirValorObjeto(canal.Codigo, dr("COD_CANAL"), GetType(String))
                    canal.SubCanales = New ObjectModel.ObservableCollection(Of Clases.SubCanal)
                    For Each drSub In dtSubCanales.Select("OID_CANAL = '" & canal.Identificador & "'")

                        Dim Subcanal As New Clases.SubCanal

                        Util.AtribuirValorObjeto(Subcanal.Identificador, drSub("OID_SUBCANAL"), GetType(String))
                        Util.AtribuirValorObjeto(Subcanal.EstaActivo, drSub("BOL_ACTIVO"), GetType(String))
                        Util.AtribuirValorObjeto(Subcanal.Codigo, drSub("COD_SUBCANAL"), GetType(String))
                        Util.AtribuirValorObjeto(Subcanal.Descripcion, drSub("DES_SUBCANAL"), GetType(String))

                        canal.SubCanales.Add(Subcanal)
                    Next

                    objPlanificacion.Canales.Add(canal)



                Next

                If ds.Tables.Contains("ele_rc_mensajes") Then
                    Dim dtMensajes As DataTable = ds.Tables("ele_rc_mensajes")
                    For Each filaMensaje As DataRow In dtMensajes.Rows
                        objPlanificacion.Mensajes.Add(New Clases.MensajeDePlanificacion() With {
                            .Codigo = Util.AtribuirValorObj(filaMensaje("COD_MENSAJE"), GetType(String)),
                            .Descripcion = Util.AtribuirValorObj(filaMensaje("DES_MENSAJE"), GetType(String)),
                            .Tipo = Util.AtribuirValorObj(filaMensaje("COD_TIPO_MENSAJE"), GetType(String)),
                            .SinReintentos = Util.AtribuirValorObj(filaMensaje("BOL_SIN_REINTENTOS"), GetType(Boolean)),
                            .TipoPeriodo = Util.AtribuirValorObj(filaMensaje("OID_TIPO_PERIODO"), GetType(String)),
                            .DesTipoPeriodo = Util.AtribuirValorObj(filaMensaje("DES_TIPO_PERIODO"), GetType(String))})
                    Next
                End If

                If ds.Tables.Contains("ele_rc_procesos") Then
                    Dim dtProcesos As DataTable = ds.Tables("ele_rc_procesos")
                    For Each filaProceso As DataRow In dtProcesos.Rows
                        objPlanificacion.Procesos.Add(New Clases.Proceso() With {
                            .Identificador = Util.AtribuirValorObj(filaProceso("OID_PROCESO"), GetType(String))})
                    Next
                End If

                If dtAjenos IsNot Nothing AndAlso dtAjenos.Rows.Count > 0 Then

                    objPlanificacion.CodigoAjenoCliente = New Clases.CodigoAjeno

                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.IdentificadorTablaGenesis, dtAjenos.Rows(0)("OID_TABLA_GENESIS"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.CodigoTipoTablaGenesis, dtAjenos.Rows(0)("COD_TIPO_TABLA_GENESIS"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.Identificador, dtAjenos.Rows(0)("OID_CODIGO_AJENO"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.Codigo, dtAjenos.Rows(0)("COD_AJENO"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.Descripcion, dtAjenos.Rows(0)("DES_AJENO"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.EsDefecto, dtAjenos.Rows(0)("BOL_DEFECTO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.EsActivo, dtAjenos.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.EsMigrado, dtAjenos.Rows(0)("BOL_MIGRADO"), GetType(Boolean))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.FechaHoraCreacion, dtAjenos.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.FechaHoraModificacion, dtAjenos.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.UsuarioCreacion, dtAjenos.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CodigoAjenoCliente.UsuarioModificacion, dtAjenos.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))

                End If

                ' Campos extras Patrones
                objPlanificacion.CamposExtrasPatrones = New Clases.CamposExtrasDeIAC()
                objPlanificacion.CamposExtrasPatrones.CamposExtras = New List(Of Clases.Termino)()
                If dtCodigosExtrasPatron.Rows.Count > 0 Then
                    Util.AtribuirValorObjeto(objPlanificacion.CamposExtrasPatrones.CodigoIAC, dtCodigosExtrasPatron.Rows(0)("COD_IAC"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CamposExtrasPatrones.IdentificadorIAC, dtCodigosExtrasPatron.Rows(0)("OID_IAC"), GetType(String))
                End If

                For Each fila As DataRow In dtCodigosExtrasPatron.Rows
                    objTermino = New Clases.Termino

                    Util.AtribuirValorObjeto(objTermino.Codigo, fila("COD_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Identificador, fila("OID_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Descripcion, fila("DES_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Valor, fila("DES_VALOR"), GetType(String))

                    objPlanificacion.CamposExtrasPatrones.CamposExtras.Add(objTermino)
                Next

                ' Campos extras Dinamicos
                objPlanificacion.CamposExtrasDinamicos = New Clases.CamposExtrasDeIAC()
                objPlanificacion.CamposExtrasDinamicos.CamposExtras = New List(Of Clases.Termino)()
                If dtCodigosExtrasDinamicos.Rows.Count > 0 Then
                    Util.AtribuirValorObjeto(objPlanificacion.CamposExtrasDinamicos.CodigoIAC, dtCodigosExtrasDinamicos.Rows(0)("COD_IAC"), GetType(String))
                    Util.AtribuirValorObjeto(objPlanificacion.CamposExtrasDinamicos.IdentificadorIAC, dtCodigosExtrasDinamicos.Rows(0)("OID_IAC"), GetType(String))
                End If

                For Each fila As DataRow In dtCodigosExtrasDinamicos.Rows
                    objTermino = New Clases.Termino

                    Util.AtribuirValorObjeto(objTermino.Codigo, fila("COD_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Identificador, fila("OID_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Descripcion, fila("DES_TERMINO"), GetType(String))
                    Util.AtribuirValorObjeto(objTermino.Valor, fila("DES_VALOR"), GetType(String))

                    objPlanificacion.CamposExtrasDinamicos.CamposExtras.Add(objTermino)
                Next

            End If

            Return objPlanificacion
        End Function

        Public Shared Function VerificarMaquinasVinculadas(oidsMaquinas As List(Of String)) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim query = My.Resources.VerificaMaquinaVinculada

            If oidsMaquinas IsNot Nothing AndAlso oidsMaquinas.Count > 0 Then
                query += Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, oidsMaquinas, "OID_MAQUINA", cmd, "AND", "PM")
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, query)
            cmd.CommandType = CommandType.Text

            Dim result As Integer = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd), Integer)

            Return result > 0

        End Function

        Public Shared Function VerificarPeriodosVinculados(lstMaquinas As List(Of Clases.Maquina)) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim query = String.Empty

            If lstMaquinas IsNot Nothing AndAlso lstMaquinas.Count > 0 Then
                Dim oidsMaquinas = lstMaquinas.Select(Function(e) e.Identificador).ToList

                query += Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, oidsMaquinas, "OID_MAQUINA", cmd, "AND", "P")


                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.VerificaPeriodosVinculados, query, query))
                cmd.CommandType = CommandType.Text


                Dim result As Integer = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd), Integer)

                Return result > 0

            Else : Return False
            End If

        End Function

        Public Shared Function VerificarPlanificacionExistente(planificacion As Clases.Planificacion) As Boolean
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim query = My.Resources.VerificaPlanificacionExistente

            query += " AND PM.COD_PLANIFICACION = []COD_PLANIFICACION "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, planificacion.Codigo))

            If Not String.IsNullOrEmpty(planificacion.Identificador) Then
                query += " AND PM.OID_PLANIFICACION <> []OID_PLANIFICACION "
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANIFICACION", ProsegurDbType.Identificador_Alfanumerico, planificacion.Identificador))

            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, query)
            cmd.CommandType = CommandType.Text


            Dim result As Integer = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd), Integer)

            Return result > 0

        End Function

        Public Shared Function RecuperarVigenciaPlanificacionMAE(IdentificadorMaquina As String) As DataTable
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MAQUINA", ProsegurDbType.Identificador_Alfanumerico, IdentificadorMaquina))
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarVigenciaPlanificacionMAE)
            cmd.CommandType = CommandType.Text
            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function
#End Region

#Region "Baja"

        Public Shared Sub BajaPlanificacion(ByRef planificacion As Clases.Planificacion, codigoUsuario As String)


            Try

                Dim spw As SPWrapper = Nothing

                spw = ColectarPlanificacionBaja(planificacion, codigoUsuario)
                AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False, Nothing)


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

        Private Shared Function ColectarPlanificacionBaja(ByRef planificacion As Clases.Planificacion, codigoUsuario As String) As SPWrapper

            'stored procedure
            Dim SP As String = String.Format("sapr_pplanificacion_{0}.sdel_planificacion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_planificacion", ProsegurDbType.Objeto_Id, planificacion.Identificador, ParameterDirection.Input, False)

            spw.AgregarParam("par$amaq_oid_maquina", ProsegurDbType.Objeto_Id, Nothing, , True)

            spw.AgregarParam("par$cod_usuario", ParamTypes.String, codigoUsuario, ParameterDirection.Input, False)


            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Curto, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            If planificacion.Maquinas IsNot Nothing AndAlso planificacion.Maquinas.Count > 0 Then
                For Each maquina In planificacion.Maquinas
                    spw.Param("par$amaq_oid_maquina").AgregarValorArray(maquina.Identificador)
                Next
            End If

            Return spw

        End Function


#End Region
    End Class

End Namespace

