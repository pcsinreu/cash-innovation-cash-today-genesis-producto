Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarMAEs
Imports Common = Prosegur.Genesis.Comon
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace Genesis
    Public Class Maquina
        Private Const dsNameTableCodigoAjeno As String = "codigo_ajeno"
        Public Shared Sub Recuperar(identificadorLlamada As String,
                                    peticion As Peticion,
                                    ByRef respuesta As Respuesta)

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                spw = ColectarPeticion(identificadorLlamada, peticion)
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta.Paginacion = New ContractoServicio.Contractos.Integracion.Comon.Paginacion
                If peticion.Paginacion IsNot Nothing Then
                    respuesta.Paginacion = peticion.Paginacion
                    If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) Then
                        respuesta.Paginacion.Indice = 0
                    End If
                Else
                    respuesta.Paginacion.Indice = 0
                End If

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

        Private Shared Function ColectarPeticion(identificadorLlamada As String, peticion As Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMAQUINA_{0}.srecuperar_maquina", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            If peticion.Paginacion IsNot Nothing Then
                If Not String.IsNullOrEmpty(peticion.Paginacion.Indice) OrElse IsNumeric(peticion.Paginacion.Indice) Then
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, peticion.Paginacion.Indice, , False)
                ElseIf Not String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) AndAlso IsNumeric(peticion.Paginacion.RegistroPorPagina) Then
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, 0, , False)
                Else
                    spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                End If
                If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) OrElse Not IsNumeric(peticion.Paginacion.RegistroPorPagina) Then
                    spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                Else
                    spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, peticion.Paginacion.RegistroPorPagina, , False)
                End If

            Else
                spw.AgregarParam("par$pag_indice", ProsegurDbType.Inteiro_Curto, Nothing, , False)
                spw.AgregarParam("par$pag_reg_pagina", ProsegurDbType.Inteiro_Curto, Nothing, , False)
            End If

            spw.AgregarParam("par$device_id", ProsegurDbType.Identificador_Alfanumerico, peticion.DeviceID, , False)
            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion, , False)
            spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta, , False)
            spw.AgregarParam("par$cod_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente, , False)
            spw.AgregarParam("par$cod_sub_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubCliente, , False)
            spw.AgregarParam("par$cod_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPuntoServicio, , False)
            spw.AgregarParam("par$cod_modelo", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoModelo, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)

            spw.AgregarParam("par$maquinas_vigente", ProsegurDbType.Logico, Common.ParameterToConverter.ToBoolean(peticion.MaquinasVigente), , False)
            spw.AgregarParam("par$recuperar_cod_ajeno", ProsegurDbType.Logico, Common.ParameterToConverter.ToBoolean(peticion.RecuperarCodigosAjenos), , False)
            spw.AgregarParam("par$con_planificacion", ProsegurDbType.Logico, Common.ParameterToConverter.ToBoolean(peticion.ConPlanificacion), , False)

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_maquinas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "maquinas")
            spw.AgregarParam("par$rc_puntos_servicio", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "puntos_servicios")
            spw.AgregarParam("par$rc_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificacion")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_limites", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "limites")
            spw.AgregarParam("par$rc_codigo_ajeno", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "codigo_ajeno")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Public Shared Function GetMaquinasDetalle(maquinasNuevas As List(Of Prosegur.Genesis.Comon.Clases.Maquina)) As List(Of Prosegur.Genesis.Comon.Clases.Maquina)
            Dim query As New Text.StringBuilder
            Dim ds As DataSet
            query.AppendLine("select")
            query.AppendLine("    maq.oid_maquina,")
            query.AppendLine("    maq.cod_identificacion DeviceID,")
            query.AppendLine("    casector.cod_ajeno COD_SECTOR,")
            query.AppendLine("    caplanta.cod_ajeno COD_PLANTA,")
            query.AppendLine("    cadeleg.cod_ajeno COD_DELEGACION")
            query.AppendLine("from    sapr_tmaquina maq")
            query.AppendLine("inner join gepr_tsector sector on sector.oid_sector = maq.oid_sector")
            query.AppendLine("inner join gepr_tplanta planta on planta.oid_planta = sector.oid_planta")
            query.AppendLine("inner join gepr_tdelegacion deleg on deleg.oid_delegacion = planta.oid_delegacion")
            query.AppendLine("inner join  gepr_tcodigo_ajeno casector on sector.oid_sector = casector.OID_TABLA_GENESIS and casector.cod_identificador = 'MAE' and casector.cod_tipo_tabla_genesis = 'GEPR_TSECTOR'")
            query.AppendLine("inner join  gepr_tcodigo_ajeno caplanta on planta.oid_planta = caplanta.OID_TABLA_GENESIS and caplanta.cod_identificador = 'MAE' and caplanta.cod_tipo_tabla_genesis = 'GEPR_TPLANTA'")
            query.AppendLine("inner join  gepr_tcodigo_ajeno cadeleg on deleg.oid_delegacion = cadeleg.OID_TABLA_GENESIS and cadeleg.cod_identificador = 'MAE' and cadeleg.cod_tipo_tabla_genesis = 'GEPR_TDELEGACION'")

            If maquinasNuevas IsNot Nothing AndAlso maquinasNuevas.Count > 0 Then
                query.AppendLine("where")
                query.AppendLine("  maq.oid_maquina in (''")
                For Each maq In maquinasNuevas
                    query.AppendLine("  ,'" & maq.Identificador & "'")
                Next
                query.Append(")")
            End If

            ds = AcessoDados.ExecutarDataSet(AccesoDatos.Constantes.CONEXAO_GENESIS, query.ToString())

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Dim identificador As String = String.Empty
                Dim maquina As Prosegur.Genesis.Comon.Clases.Maquina
                For Each fila As DataRow In ds.Tables(0).Rows
                    identificador = Util.AtribuirValorObj(fila("OID_MAQUINA"), GetType(String))
                    maquina = maquinasNuevas.Where(Function(x) x.Identificador = identificador).FirstOrDefault
                    If maquina IsNot Nothing Then
                        maquina.Codigo = Util.AtribuirValorObj(fila("DEVICEID"), GetType(String))
                        maquina.Sector = New Prosegur.Genesis.Comon.Clases.Sector
                        maquina.Sector.Planta = New Prosegur.Genesis.Comon.Clases.Planta
                        maquina.Delegacion = New Prosegur.Genesis.Comon.Clases.Delegacion
                        maquina.Sector.Delegacion = New Prosegur.Genesis.Comon.Clases.Delegacion

                        maquina.Sector.Codigo = Util.AtribuirValorObj(fila("COD_SECTOR"), GetType(String))
                        maquina.Sector.Planta.Codigo = Util.AtribuirValorObj(fila("COD_PLANTA"), GetType(String))
                        maquina.Delegacion.Codigo = Util.AtribuirValorObj(fila("COD_DELEGACION"), GetType(String))
                        maquina.Sector.Delegacion = maquina.Delegacion

                    End If


                Next
            End If

            Return maquinasNuevas
        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           ByRef respuesta As Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                ' Maquinas
                If ds.Tables.Contains("maquinas") AndAlso ds.Tables("maquinas").Rows.Count > 0 Then

                    If respuesta.MAEs Is Nothing Then respuesta.MAEs = New List(Of ContractoServicio.Contractos.Integracion.RecuperarMAEs.Maquina)

                    respuesta.Paginacion.RegistroPorPagina = ds.Tables("maquinas").Rows.Count
                    For Each rowMaquina As DataRow In ds.Tables("maquinas").Rows

                        Dim maquina As New ContractoServicio.Contractos.Integracion.RecuperarMAEs.Maquina

                        With maquina

                            .Identificador = Util.AtribuirValorObj(rowMaquina("OID_MAQUINA"), GetType(String))
                            .DeviceID = Util.AtribuirValorObj(rowMaquina("COD_IDENTIFICACION"), GetType(String))
                            .Vigente = Util.AtribuirValorObj(rowMaquina("BOL_ACTIVO"), GetType(Boolean))
                            .ConsideraRecuentos = Util.AtribuirValorObj(rowMaquina("BOL_CONSIDERA_RECUENTOS"), GetType(Boolean))
                            .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_SECTOR"), GetType(String))

                            .Modelo = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_MODELO"), GetType(String)),
                                                                                                       .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_MODELO"), GetType(String))}

                            .Marca = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_FABRICANTE"), GetType(String)),
                                                                                                       .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_FABRICANTE"), GetType(String))}

                            .Delegacion = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_DELEGACION"), GetType(String)),
                                                                                                       .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_DELEGACION"), GetType(String))}

                            .Planta = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowMaquina("COD_PLANTA"), GetType(String)),
                                                                                                       .Descripcion = Util.AtribuirValorObj(rowMaquina("DES_PLANTA"), GetType(String))}

                            ' Puntos de Servicios
                            If ds.Tables.Contains("puntos_servicios") AndAlso ds.Tables("puntos_servicios").Rows.Count > 0 Then

                                Dim rPuntos As DataRow() = ds.Tables("puntos_servicios").Select("OID_MAQUINA ='" & .Identificador & "'")

                                If rPuntos IsNot Nothing AndAlso rPuntos.Count > 0 Then

                                    If maquina.PuntosServicio Is Nothing Then maquina.PuntosServicio = New List(Of ContractoServicio.Contractos.Integracion.RecuperarMAEs.PuntosServicio)

                                    For Each rowPunto As DataRow In rPuntos

                                        Dim punto As New ContractoServicio.Contractos.Integracion.RecuperarMAEs.PuntosServicio

                                        With punto

                                            .PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPunto("COD_PTO_SERVICIO"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rowPunto("DES_PTO_SERVICIO"), GetType(String))}

                                            .SubCliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPunto("COD_SUBCLIENTE"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rowPunto("DES_SUBCLIENTE"), GetType(String))}

                                            .Cliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPunto("COD_CLIENTE"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rowPunto("DES_CLIENTE"), GetType(String))}


                                        End With

                                        maquina.PuntosServicio.Add(punto)

                                    Next

                                End If


                            End If

                            ' ´Código ajeno
                            If ds.Tables.Contains(dsNameTableCodigoAjeno) AndAlso ds.Tables(dsNameTableCodigoAjeno).Rows.Count > 0 Then

                                Dim rCodigos As DataRow() = ds.Tables(dsNameTableCodigoAjeno).Select("OID_MAQUINA ='" & .Identificador & "'")

                                If rCodigos IsNot Nothing AndAlso rCodigos.Count > 0 Then

                                    If maquina.CodigosAjenos Is Nothing Then maquina.CodigosAjenos = New List(Of ContractoServicio.Contractos.Integracion.RecuperarMAEs.CodigoAjeno)

                                    For Each rowCodigoAjeno As DataRow In rCodigos

                                        Dim codajeno As New ContractoServicio.Contractos.Integracion.RecuperarMAEs.CodigoAjeno

                                        With codajeno

                                            .CodigoIdentificador = Util.AtribuirValorObj(rowCodigoAjeno("COD_IDENTIFICADOR"), GetType(String))

                                            .Codigo = Util.AtribuirValorObj(rowCodigoAjeno("COD_AJENO"), GetType(String))

                                            .Descripcion = Util.AtribuirValorObj(rowCodigoAjeno("DES_AJENO"), GetType(String))


                                        End With

                                        maquina.CodigosAjenos.Add(codajeno)

                                    Next

                                End If


                            End If

                            ' Planificaciones
                            If ds.Tables.Contains("planificacion") AndAlso ds.Tables("planificacion").Rows.Count > 0 Then

                                Dim rPlanificacion As DataRow() = ds.Tables("planificacion").Select("OID_MAQUINA ='" & .Identificador & "'")

                                If rPlanificacion IsNot Nothing AndAlso rPlanificacion.Count > 0 Then

                                    If maquina.Planificacion Is Nothing Then maquina.Planificacion = New ContractoServicio.Contractos.Integracion.RecuperarMAEs.Planificacion

                                    With maquina.Planificacion

                                        .Codigo = Util.AtribuirValorObj(rPlanificacion(0)("COD_PLANIFICACION"), GetType(String))
                                        .Descripcion = Util.AtribuirValorObj(rPlanificacion(0)("DES_PLANIFICACION"), GetType(String))

                                        Dim dVigenciaInicio As DateTime = Util.AtribuirValorObj(rPlanificacion(0)("FYH_VIGENCIA_INICIO"), GetType(DateTime))
                                        If dVigenciaInicio <> DateTime.MinValue Then
                                            .FechaHoraVigenciaInicio = dVigenciaInicio.ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz")
                                        End If

                                        Dim dVigenciaFin As DateTime = Util.AtribuirValorObj(rPlanificacion(0)("FYH_VIGENCIA_FIN"), GetType(DateTime))
                                        If dVigenciaFin <> DateTime.MinValue Then
                                            .FechaHoraVigenciaFin = dVigenciaFin.ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz")
                                        End If

                                        .Vigente = Util.AtribuirValorObj(rPlanificacion(0)("BOL_ACTIVO"), GetType(Boolean))
                                        .MinutosAcreditacion = Util.AtribuirValorObj(rPlanificacion(0)("NEC_CONTINGENCIA"), GetType(Integer))
                                        .Tipo = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rPlanificacion(0)("COD_TIPO_PLANIFICACION"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rPlanificacion(0)("DES_TIPO_PLANIFICACION"), GetType(String))}
                                        .Banco = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rPlanificacion(0)("COD_CLIENTE"), GetType(String)),
                                                                                                                              .Descripcion = Util.AtribuirValorObj(rPlanificacion(0)("DES_CLIENTE"), GetType(String))}
                                    End With

                                End If

                            End If


                            ' Limites

                            If ds.Tables.Contains("limites") AndAlso ds.Tables("limites").Rows.Count > 0 Then
                                .Limites = New List(Of Limite)
                                Dim objLimite As Limite
                                For Each fila As DataRow In ds.Tables("limites").Select("DEVICEID = '" & .DeviceID & "'")
                                    objLimite = New Limite

                                    objLimite.Divisa = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {
                                        .Codigo = Util.AtribuirValorObj(fila("COD_ISO_DIVISA"), GetType(String)),
                                        .Descripcion = Util.AtribuirValorObj(fila("DES_DIVISA"), GetType(String))
                                    }

                                    objLimite.Valor = Util.AtribuirValorObj(fila("VALOR"), GetType(Decimal))
                                    Dim codigoPuntoServicio As String = Util.AtribuirValorObj(fila("COD_PTO_SERVICIO"), GetType(String))
                                    Dim descripPuntoServicio As String = Util.AtribuirValorObj(fila("DES_PTO_SERVICIO"), GetType(String))

                                    If Not String.IsNullOrWhiteSpace(codigoPuntoServicio) OrElse Not String.IsNullOrEmpty(descripPuntoServicio) Then
                                        objLimite.PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {
                                        .Codigo = codigoPuntoServicio,
                                        .Descripcion = descripPuntoServicio
                                        }

                                    End If


                                    .Limites.Add(objLimite)
                                Next
                            End If
                        End With

                        respuesta.MAEs.Add(maquina)

                    Next
                Else
                    respuesta.Paginacion.RegistroPorPagina = 0
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


        Public Shared Sub RecuperarBancoTesoreriaEComission(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Peticion, ByRef respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta)



            Try
                Dim ds As DataSet = Nothing

                Dim spw As SPWrapper = ColectarPlanificacionRecuperar(Peticion)
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)

                poblarRespuestaComision(ds, respuesta)

            Catch ex As Exception
                Throw ex
            End Try

            '   Return _planificaciones
        End Sub

        Private Shared Sub poblarRespuestaComision(ds As DataSet, ByRef respuesta As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta)

            respuesta = New ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Respuesta()
            If ds.Tables.Contains("comision") AndAlso ds.Tables("comision").Rows.Count > 0 Then

                If respuesta.MAEs Is Nothing Then respuesta.MAEs = New List(Of ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Maquina)


                For Each rowMaquina As DataRow In ds.Tables("comision").Rows

                    Dim mae As New ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Maquina

                    With mae
                        .OID_MAQUINA = Util.AtribuirValorObj(rowMaquina("OID_MAQUINA"), GetType(String))
                        .OID_SUBCLIENTE = Util.AtribuirValorObj(rowMaquina("OID_SUBCLIENTE"), GetType(String))
                        .COD_SUBCLIENTE = Util.AtribuirValorObj(rowMaquina("COD_SUBCLIENTE"), GetType(String))
                        .DES_SUBCLIENTE = Util.AtribuirValorObj(rowMaquina("DES_SUBCLIENTE"), GetType(String))
                        .OID_PTO_SERVICIO = Util.AtribuirValorObj(rowMaquina("OID_PTO_SERVICIO"), GetType(String))
                        .COD_PTO_SERVICIO = Util.AtribuirValorObj(rowMaquina("COD_PTO_SERVICIO"), GetType(String))
                        .DES_PTO_SERVICIO = Util.AtribuirValorObj(rowMaquina("DES_PTO_SERVICIO"), GetType(String))
                        If Not String.IsNullOrWhiteSpace(Util.AtribuirValorObj(rowMaquina("NUM_PORCENT_COMISION"), GetType(String))) Then

                            .NUM_PORCENT_COMISION = Util.AtribuirValorObj(rowMaquina("NUM_PORCENT_COMISION"), GetType(Decimal))
                        End If


                    End With
                    respuesta.MAEs.Add(mae)

                Next
            End If


        End Sub

        Public Shared Function ColectarPlanificacionRecuperar(Peticion As Prosegur.Genesis.ContractoServicio.Contractos.Integracion.RecuperarComissionDatosBancarios.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMAQUINA_{0}.srecuperar_comission_dtbanca", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_maquinas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            For Each oid In Peticion.IdentificadorMaquina
                spw.Param("par$oid_maquinas").AgregarValorArray(oid)
            Next


            spw.AgregarParam("par$rc_comision", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "comision")




            Return spw
        End Function

    End Class

End Namespace
