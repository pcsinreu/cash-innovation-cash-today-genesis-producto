Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfigurarMAEs
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace GenesisSaldos

    Public Class Maquina

        Public Shared Function ConfigurarMaquinas(identificadorLlamada As String, peticion As Peticion,
                                                  ByRef log As StringBuilder) As List(Of Salida.Maquina)

            Dim TiempoParcial As DateTime
            Dim maquinas As New List(Of Salida.Maquina)

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                Dim maquinasAltaModificar = peticion.Maquinas.FindAll(Function(x) x.Accion.ToUpper <> "BAJA")
                Dim maquinasBajar = peticion.Maquinas.FindAll(Function(x) x.Accion.ToUpper = "BAJA")
                log.AppendLine("AccesoDatos - Tiempo separar maquinas por accion: " & Now.Subtract(TiempoParcial).ToString() & ";")

                If maquinasAltaModificar IsNot Nothing AndAlso maquinasAltaModificar.Count > 0 Then

                    log.AppendLine("AccesoDatos - Alta y modificar. Cantindad: " & maquinasAltaModificar.Count.ToString() & ";")

                    TiempoParcial = Now
                    spw = sconfigurar_maquinas(identificadorLlamada, peticion, maquinasAltaModificar)
                    log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                    log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    PoblarMaquinas(ds, maquinas)
                    log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                End If

                If maquinasBajar IsNot Nothing AndAlso maquinasBajar.Count > 0 Then

                    log.AppendLine("AccesoDatos - Bajar. Cantindad: " & maquinasBajar.Count.ToString() & ";")

                    TiempoParcial = Now
                    spw = sbajar_maquinas(identificadorLlamada, peticion, maquinasBajar)
                    log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                    log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    PoblarMaquinas(ds, maquinas)
                    log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                End If

                If maquinas IsNot Nothing AndAlso maquinas.Count > 0 Then
                    ' Valida si hubo algun error y cambia el Tipo de Resultado
                    For Each mae In maquinas
                        If mae.Detalles IsNot Nothing AndAlso mae.Detalles.FirstOrDefault(Function(x) x.Codigo.Substring(0, 1) <> "0") IsNot Nothing Then
                            mae.TipoResultado = "2"
                        End If
                    Next
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

            Return maquinas

        End Function

        Private Shared Function sconfigurar_maquinas(identificadorLlamada As String, peticion As Peticion, maquinas As List(Of Entrada.Maquina)) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMAQUINA_{0}.sconfigurar_maquinas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$abol_alta", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$ades_descripcion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_delegacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_modelo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acod_fabricante", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$abol_cons_recuentos", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abol_multiclientes", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$a_plan_bol_baja", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$a_plan_cod_planificacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_plan_nel_gmt_minuto", ProsegurDbType.Inteiro_Longo, Nothing, , True)
            spw.AgregarParam("par$a_plan_fyh_inicio", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$a_plan_fyh_fin", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$a_puntos_cod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_puntos_bol_baja", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$a_puntos_cod_punto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_lim_cod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_lim_cod_pto", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$a_lim_num", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$a_lim_bol_baja", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            For Each maquina In maquinas

                If maquina.Accion IsNot Nothing AndAlso maquina.Accion.ToUpper = "MODIFICAR" Then
                    spw.Param("par$abol_alta").AgregarValorArray(0)
                Else
                    maquina.Accion = "ALTA"
                    spw.Param("par$abol_alta").AgregarValorArray(1)
                End If

                spw.Param("par$acod_device_id").AgregarValorArray(maquina.DeviceID)
                spw.Param("par$ades_descripcion").AgregarValorArray(maquina.Descripcion)
                spw.Param("par$acod_delegacion").AgregarValorArray(maquina.CodigoDelegacion)

                spw.Param("par$acod_modelo").AgregarValorArray(If(maquina.Accion.ToUpper <> "MODIFICAR" AndAlso String.IsNullOrEmpty(maquina.CodigoModelo), "DEFAULT", maquina.CodigoModelo))
                spw.Param("par$acod_fabricante").AgregarValorArray(If(maquina.Accion.ToUpper <> "MODIFICAR" AndAlso String.IsNullOrEmpty(maquina.CodigoFabricante), "DEFAULT", maquina.CodigoFabricante))

                If maquina.ConsideraRecuentos Is Nothing Then
                    If maquina.Accion <> "MODIFICAR" Then
                        spw.Param("par$abol_cons_recuentos").AgregarValorArray(0)
                    Else
                        spw.Param("par$abol_cons_recuentos").AgregarValorArray(DBNull.Value)
                    End If
                ElseIf maquina.ConsideraRecuentos = "1" OrElse maquina.ConsideraRecuentos.ToUpper = "TRUE" Then
                    spw.Param("par$abol_cons_recuentos").AgregarValorArray(1)
                Else
                    spw.Param("par$abol_cons_recuentos").AgregarValorArray(0)
                End If

                If maquina.Multicliente Is Nothing Then
                    If maquina.Accion <> "MODIFICAR" Then
                        spw.Param("par$abol_multiclientes").AgregarValorArray(0)
                    Else
                        spw.Param("par$abol_multiclientes").AgregarValorArray(DBNull.Value)
                    End If
                ElseIf maquina.Multicliente = "1" OrElse maquina.Multicliente.ToUpper = "TRUE" Then
                    spw.Param("par$abol_multiclientes").AgregarValorArray(1)
                Else
                    spw.Param("par$abol_multiclientes").AgregarValorArray(0)
                End If

                If maquina.Planificacion IsNot Nothing Then


                    If maquina.Planificacion.Accion IsNot Nothing AndAlso maquina.Planificacion.Accion.ToUpper = "BAJA" Then
                        spw.Param("par$a_plan_bol_baja").AgregarValorArray(1)
                    Else
                        spw.Param("par$a_plan_bol_baja").AgregarValorArray(0)
                    End If


                    spw.Param("par$a_plan_cod_planificacion").AgregarValorArray(maquina.Planificacion.Codigo)
                    spw.Param("par$a_plan_fyh_inicio").AgregarValorArray(maquina.Planificacion.FechaHoraVigenciaInicio)
                    spw.Param("par$a_plan_fyh_fin").AgregarValorArray(maquina.Planificacion.FechaHoraVigenciaFin)

                    If maquina.Planificacion.FechaHoraVigenciaInicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(maquina.Planificacion.FechaHoraVigenciaInicio.Value.ToString("%K")) Then

                        Dim GMTHoraLocalCalculado = Convert.ToInt32(maquina.Planificacion.FechaHoraVigenciaInicio.Value.ToString("zzz").Split(":")(0))
                        Dim GMTMinutoLocalCalculado = Convert.ToInt32(maquina.Planificacion.FechaHoraVigenciaInicio.Value.ToString("zzz").Split(":")(1))
                        GMTMinutoLocalCalculado += GMTHoraLocalCalculado * 60
                        spw.Param("par$a_plan_nel_gmt_minuto").AgregarValorArray(GMTMinutoLocalCalculado)

                    Else
                        spw.Param("par$a_plan_nel_gmt_minuto").AgregarValorArray(DBNull.Value)
                    End If



                Else
                    spw.Param("par$a_plan_nel_gmt_minuto").AgregarValorArray(DBNull.Value)
                    spw.Param("par$a_plan_bol_baja").AgregarValorArray(DBNull.Value)
                    spw.Param("par$a_plan_cod_planificacion").AgregarValorArray(DBNull.Value)
                    spw.Param("par$a_plan_fyh_inicio").AgregarValorArray(DBNull.Value)
                    spw.Param("par$a_plan_fyh_fin").AgregarValorArray(DBNull.Value)
                End If

                If maquina.PuntosServicio IsNot Nothing Then

                    For Each punto In maquina.PuntosServicio

                        If Not String.IsNullOrEmpty(punto.Codigo) Then
                            spw.Param("par$a_puntos_cod_device_id").AgregarValorArray(maquina.DeviceID)
                            spw.Param("par$a_puntos_cod_punto").AgregarValorArray(punto.Codigo)

                            If punto.Accion IsNot Nothing AndAlso punto.Accion.ToUpper = "BAJA" Then
                                spw.Param("par$a_puntos_bol_baja").AgregarValorArray(1)
                            Else
                                spw.Param("par$a_puntos_bol_baja").AgregarValorArray(0)
                            End If

                        End If

                    Next

                End If

                If maquina.Limites IsNot Nothing Then

                    For Each limite In maquina.Limites

                        spw.Param("par$a_lim_cod_divisa").AgregarValorArray(limite.CodigoDivisa)
                        spw.Param("par$a_lim_cod_pto").AgregarValorArray(limite.CodigoPuntoServicio)
                        spw.Param("par$a_lim_num").AgregarValorArray(limite.NumLimite)
                        If Not String.IsNullOrWhiteSpace(limite.Accion) AndAlso limite.Accion.ToUpper = "BAJA" Then
                            spw.Param("par$a_lim_bol_baja").AgregarValorArray(1)
                        Else
                            spw.Param("par$a_lim_bol_baja").AgregarValorArray(0)
                        End If
                    Next
                End If
            Next

            Return spw

        End Function

        Private Shared Function sbajar_maquinas(identificadorLlamada As String, peticion As Peticion, maquinas As List(Of Entrada.Maquina)) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMAQUINA_{0}.sbajar_maquinas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            For Each maquina In maquinas
                spw.Param("par$acod_device_id").AgregarValorArray(maquina.DeviceID)
            Next

            Return spw

        End Function

        Private Shared Sub PoblarMaquinas(ds As DataSet, ByRef maquinas As List(Of Salida.Maquina))

            If maquinas Is Nothing Then maquinas = New List(Of Salida.Maquina)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("validaciones").Rows

                        Dim maquina = maquinas.FirstOrDefault(Function(x) x.DeviceID = Util.AtribuirValorObj(row("DEVICE_ID"), GetType(String)))

                        If maquina Is Nothing Then
                            maquina = New Salida.Maquina
                            maquina.DeviceID = Util.AtribuirValorObj(row("DEVICE_ID"), GetType(String))
                            maquina.TipoResultado = "0"
                            maquinas.Add(maquina)
                        End If

                        If maquina.Detalles Is Nothing Then maquina.Detalles = New List(Of Salida.Detalle)

                        Dim detalle As New Salida.Detalle
                        detalle.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                        detalle.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        maquina.Detalles.Add(detalle)

                    Next

                End If

            End If

        End Sub

    End Class

End Namespace

