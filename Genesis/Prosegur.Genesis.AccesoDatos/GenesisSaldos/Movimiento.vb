Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.Comon.Enumeradores.Mensajes
Imports System.Text
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Globalization
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.AltaMovimientosCashIn
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.Comon
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.crearDocumentoFondos
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas.FechaValorOnline.Salida
Imports System.Linq

Namespace GenesisSaldos

    Public Class Movimiento
        Private Shared Sub ActualizarMovimientoRespuesta(ByRef movimientos As List(Of ActualizarMovimientos.Movimiento),
                                                     codigoExterno As String,
                                                     accion As Prosegur.Genesis.Comon.Enumeradores.AccionActualizarMovimiento,
                                                     recuperarMensaje As Boolean,
                                                     codigomensaje As String,
                                                     mensaje As String)

            Dim movi As ActualizarMovimientos.Movimiento
            If movimientos IsNot Nothing AndAlso movimientos.Count > 0 AndAlso movimientos.FirstOrDefault(Function(x) x.CodigoExterno = codigoExterno) IsNot Nothing Then
                movi = movimientos.FirstOrDefault(Function(x) x.CodigoExterno = codigoExterno)
            Else
                movi = New ActualizarMovimientos.Movimiento
                If movimientos Is Nothing Then
                    movimientos = New List(Of ActualizarMovimientos.Movimiento)()
                End If
                movi.CodigoExterno = codigoExterno
                movimientos.Add(movi)
            End If

            Select Case accion
                Case Prosegur.Genesis.Comon.Enumeradores.AccionActualizarMovimiento.Notificar
                    If movi.Notificar Is Nothing Then movi.Notificar = New ActualizarMovimientos.Proceso()

                    If recuperarMensaje Then
                        Util.resultado(movi.Notificar.Codigo,
                                movi.Notificar.Descripcion,
                                Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                                Contexto.Integraciones,
                                Funcionalidad.MarcarMovimientos,
                                codigomensaje,
                                "",
                                True)
                    Else
                        movi.Notificar.Codigo = codigomensaje
                        movi.Notificar.Descripcion = mensaje
                    End If

                    movi.Notificar.Tipo = movi.Notificar.Codigo.Substring(0, 1)

                Case Prosegur.Genesis.Comon.Enumeradores.AccionActualizarMovimiento.Acreditar

                    If movi.Acreditar Is Nothing Then movi.Acreditar = New ActualizarMovimientos.Proceso()
                    If recuperarMensaje Then
                        Util.resultado(movi.Acreditar.Codigo,
                                movi.Acreditar.Descripcion,
                                Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                                Contexto.Integraciones,
                                Funcionalidad.MarcarMovimientos,
                                codigomensaje,
                                "",
                                True)
                    Else
                        movi.Acreditar.Codigo = codigomensaje
                        movi.Acreditar.Descripcion = mensaje
                    End If

                    movi.Acreditar.Tipo = movi.Acreditar.Codigo.Substring(0, 1)

            End Select


        End Sub

        Public Shared Sub ActualizarMovimentos(identificadorLlamada As String, peticion As ActualizarMovimientos.Peticion,
                                           ByRef respuesta As ActualizarMovimientos.Respuesta,
                                           ByRef log As StringBuilder)

            Dim TiempoParcial As DateTime

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = smarcar_movimiento(identificadorLlamada, peticion)
                log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                PoblarRespuesta(ds, peticion.Accion, respuesta)
                log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

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

        Public Shared Function RecuperarActualIDs(identificadorLlamada As String,
                                                 codigosExternos As List(Of String),
                                             collectionIDs As List(Of String),
                                             actualIDS As List(Of String),
                                             dicActualByCollectionID As Dictionary(Of String, List(Of String)),
                                             dicActualByCodigoExterno As Dictionary(Of String, List(Of String))) As List(Of String)

            Dim ds As DataSet = Nothing
            Dim spw As SPWrapper = Nothing
            Dim codigo_actual_id As String = String.Empty
            Dim codigo_collection_id As String = String.Empty
            Dim codigo_externo As String = String.Empty
            Dim listaRetorno As New List(Of String)
            Try

                spw = srecuperar_movimientos_actual_ids(identificadorLlamada, codigosExternos, collectionIDs, actualIDS)

                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                Return PoblarRespuestaRecuperarMovimientosActualIds(dicActualByCollectionID, dicActualByCodigoExterno, ds, codigo_actual_id, codigo_collection_id, codigo_externo)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try
            Return Nothing
        End Function

        Private Shared Function PoblarRespuestaRecuperarMovimientosActualIds(ByRef dicActualByCollectionID As Dictionary(Of String, List(Of String)), ByRef dicActualByCodigoExterno As Dictionary(Of String, List(Of String)), ds As DataSet, ByRef codigo_actual_id As String, ByRef codigo_collection_id As String, ByRef codigo_externo As String) As List(Of String)
            Dim listaRetorno As New List(Of String)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables.Contains("actual_ids") Then
                For Each fila As DataRow In ds.Tables("actual_ids").Rows

                    codigo_actual_id = Util.AtribuirValorObj(fila("cod_actual_id"), GetType(String))
                    codigo_externo = Util.AtribuirValorObj(fila("COD_EXTERNO"), GetType(String))
                    codigo_collection_id = Util.AtribuirValorObj(fila("COD_COLLECTION_ID"), GetType(String))

                    If Not listaRetorno.Contains(codigo_actual_id) Then
                        listaRetorno.Add(codigo_actual_id)
                    End If

                    If Not String.IsNullOrEmpty(codigo_externo) Then
                        If dicActualByCodigoExterno.ContainsKey(codigo_actual_id) Then
                            Dim lista As New List(Of String)
                            lista = dicActualByCodigoExterno(codigo_actual_id)
                            lista.Add(codigo_externo)

                            dicActualByCodigoExterno(codigo_actual_id) = lista
                        Else
                            Dim lista As New List(Of String)
                            lista.Add(codigo_externo)
                            dicActualByCodigoExterno.Add(codigo_actual_id, lista)
                        End If
                    End If

                    If Not String.IsNullOrEmpty(codigo_collection_id) Then
                        If dicActualByCollectionID.ContainsKey(codigo_actual_id) Then
                            Dim lista As New List(Of String)
                            lista = dicActualByCollectionID(codigo_actual_id)
                            lista.Add(codigo_collection_id)

                            dicActualByCollectionID(codigo_actual_id) = lista
                        Else
                            Dim lista As New List(Of String)
                            lista.Add(codigo_collection_id)
                            dicActualByCollectionID.Add(codigo_actual_id, lista)
                        End If
                    End If
                Next
            End If
            Return listaRetorno
        End Function

        Private Shared Function srecuperar_movimientos_actual_ids(identificadorLlamada As String, codigosExternos As List(Of String), collectionIDs As List(Of String), actualIDs As List(Of String)) As SPWrapper
            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_actual_ids", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$acod_externo", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$acollection", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$aactual_id", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$rc_datos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "actual_ids")

            spw.Param("par$acod_externo").AgregarValorArray("")
            spw.Param("par$acollection").AgregarValorArray("")
            spw.Param("par$aactual_id").AgregarValorArray("")

            For Each codigo_externo In codigosExternos
                spw.Param("par$acod_externo").AgregarValorArray(codigo_externo)
            Next

            For Each collection_id In collectionIDs
                spw.Param("par$acollection").AgregarValorArray(collection_id)
            Next

            For Each actual_id In actualIDs
                spw.Param("par$aactual_id").AgregarValorArray(actual_id)
            Next


            Return spw
        End Function

        Private Shared Function smarcar_movimiento(identificadorLlamada As String, peticion As ActualizarMovimientos.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.smarcar_movimiento", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$movimientos", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$tipo_movimiento", ProsegurDbType.Descricao_Curta, Nothing, , True)
            spw.AgregarParam("par$accion", ProsegurDbType.Identificador_Alfanumerico, peticion.Accion, , False)
            spw.AgregarParam("par$fecha_hora", ProsegurDbType.Data_Hora, peticion.FechaHora.ToUniversalTime(), , False)
            spw.AgregarParam("par$sis_origen", ProsegurDbType.Descricao_Longa, peticion.SistemaOrigen, , False)
            spw.AgregarParam("par$sis_destino", ProsegurDbType.Descricao_Longa, peticion.SistemaDestino, , False)
            spw.AgregarParam("par$mensaje", ProsegurDbType.Observacao_Longa, peticion.Mensaje, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_codigosexternos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "codigos_externos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)



            For Each movimiento In peticion.Movimientos
                spw.Param("par$movimientos").AgregarValorArray(movimiento.Valor)
                spw.Param("par$tipo_movimiento").AgregarValorArray(movimiento.Tipo)
            Next

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, accion As Prosegur.Genesis.Comon.Enumeradores.AccionActualizarMovimiento,
                                           ByRef respuesta As ActualizarMovimientos.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                'Codigos Externos
                If ds.Tables.Contains("codigos_externos") AndAlso ds.Tables("codigos_externos").Rows.Count > 0 Then
                    Dim movi As ActualizarMovimientos.Movimiento
                    If respuesta.Movimientos Is Nothing Then
                        respuesta.Movimientos = New List(Of ActualizarMovimientos.Movimiento)()
                    End If

                    For Each row As DataRow In ds.Tables("codigos_externos").Rows
                        movi = New ActualizarMovimientos.Movimiento
                        movi.CodigoExterno = Util.AtribuirValorObj(row(0), GetType(String))
                        respuesta.Movimientos.Add(movi)
                    Next

                End If

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        Dim _codigo As String = Util.AtribuirValorObj(row(0), GetType(String))
                        Dim _codigoMensaje As String = Util.AtribuirValorObj(row(1), GetType(String))
                        Dim _mensaje As String = Util.AtribuirValorObj(row(2), GetType(String))

                        ActualizarMovimientoRespuesta(respuesta.Movimientos, _codigo, accion, False, _codigoMensaje, _mensaje)
                    Next
                End If
            End If

        End Sub


        Public Shared Sub Recuperar(peticion As RecuperarMovimientos.Peticion,
                                    GMTHoraLocalCalculado As Double,
                                    GMTMinutoLocalCalculado As Double,
                                    ByRef respuesta As RecuperarMovimientos.Respuesta,
                           Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now

                spw = ColectarPeticion(peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                respuesta.Paginacion = New ContractoServicio.Contractos.Integracion.Comon.Paginacion
                If peticion.Paginacion IsNot Nothing Then
                    respuesta.Paginacion = peticion.Paginacion
                    If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) Then
                        respuesta.Paginacion.Indice = 0
                    End If
                Else
                    respuesta.Paginacion.Indice = 0
                End If

                PoblarRespuesta(ds, GMTHoraLocalCalculado, GMTMinutoLocalCalculado, respuesta)

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

        Private Shared Function ColectarPeticion(peticion As RecuperarMovimientos.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_movimientos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

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

            If peticion.FiltrosAdicionales IsNot Nothing Then

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.ValoresDetallar) Then
                    spw.AgregarParam("par$filtro_val_detallar", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.ValoresDetallar = "1" OrElse peticion.FiltrosAdicionales.ValoresDetallar.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_val_detallar", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_val_detallar", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.Disponible) Then
                    spw.AgregarParam("par$filtro_disponible", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.Disponible = "1" OrElse peticion.FiltrosAdicionales.Disponible.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_disponible", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_disponible", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.InfoAdicionales) Then
                    spw.AgregarParam("par$fltro_info_adic", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.InfoAdicionales = "1" OrElse peticion.FiltrosAdicionales.InfoAdicionales.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$fltro_info_adic", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$fltro_info_adic", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.Acreditado) Then
                    spw.AgregarParam("par$filtro_acreditado", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.Acreditado = "1" OrElse peticion.FiltrosAdicionales.Acreditado.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_acreditado", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_acreditado", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.Notificado) Then
                    spw.AgregarParam("par$filtro_notificado", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.Notificado = "1" OrElse peticion.FiltrosAdicionales.Notificado.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_notificado", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_notificado", ProsegurDbType.Logico, 0, , False)
                End If


                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.MaquinasVigente) Then
                    spw.AgregarParam("par$filtro_maqu_vigente", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.MaquinasVigente = "1" OrElse peticion.FiltrosAdicionales.MaquinasVigente.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_maqu_vigente", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_maqu_vigente", ProsegurDbType.Logico, 0, , False)
                End If


                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.IncluirValoresInformativos) Then
                    spw.AgregarParam("par$filtro_valores_inform", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.IncluirValoresInformativos = "1" OrElse peticion.FiltrosAdicionales.IncluirValoresInformativos.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_valores_inform", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_valores_inform", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.FiltrosAdicionales.IncluirCollectionID) Then
                    spw.AgregarParam("par$filtro_collectionid", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.FiltrosAdicionales.IncluirCollectionID = "1" OrElse peticion.FiltrosAdicionales.IncluirCollectionID.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$filtro_collectionid", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$filtro_collectionid", ProsegurDbType.Logico, 0, , False)
                End If


            Else

                spw.AgregarParam("par$filtro_val_detallar", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$filtro_disponible", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$fltro_info_adic", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$filtro_acreditado", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$filtro_notificado", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$filtro_maqu_vigente", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$filtro_valores_inform", ProsegurDbType.Logico, Nothing, , False)

            End If

            If peticion.FechaGestion Is Nothing OrElse peticion.FechaGestion.Desde = DateTime.MinValue Then
                spw.AgregarParam("par$fg_desde", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fg_desde", ProsegurDbType.Data_Hora, peticion.FechaGestion.Desde, , False)
            End If
            If peticion.FechaGestion Is Nothing OrElse peticion.FechaGestion.Hasta = DateTime.MinValue Then
                spw.AgregarParam("par$fg_hasta", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fg_hasta", ProsegurDbType.Data_Hora, peticion.FechaGestion.Hasta, , False)
            End If
            If peticion.FechaCreacion Is Nothing OrElse peticion.FechaCreacion.Desde = DateTime.MinValue Then
                spw.AgregarParam("par$fc_desde", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fc_desde", ProsegurDbType.Data_Hora, peticion.FechaCreacion.Desde, , False)
            End If
            If peticion.FechaCreacion Is Nothing OrElse peticion.FechaCreacion.Hasta = DateTime.MinValue Then
                spw.AgregarParam("par$fc_hasta", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fc_hasta", ProsegurDbType.Data_Hora, peticion.FechaCreacion.Hasta, , False)
            End If


            If peticion.FechaAcreditacion Is Nothing OrElse peticion.FechaAcreditacion.Desde = DateTime.MinValue Then
                spw.AgregarParam("par$fa_desde", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fa_desde", ProsegurDbType.Data_Hora, peticion.FechaAcreditacion.Desde, , False)
            End If
            If peticion.FechaAcreditacion Is Nothing OrElse peticion.FechaAcreditacion.Hasta = DateTime.MinValue Then
                spw.AgregarParam("par$fa_hasta", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fa_hasta", ProsegurDbType.Data_Hora, peticion.FechaAcreditacion.Hasta, , False)
            End If



            spw.AgregarParam("par$cod_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente, , False)
            spw.AgregarParam("par$cod_sub_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubCliente, , False)
            spw.AgregarParam("par$cod_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPuntoServicio, , False)
            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion, , False)
            spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta, , False)
            spw.AgregarParam("par$cod_maquinas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_canales", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_formularios", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_divisas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            If peticion.Configuracion IsNot Nothing Then
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            Else
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, Nothing,  , False)
            End If

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)

            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")
            spw.AgregarParam("par$rc_formularios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "formularios")
            spw.AgregarParam("par$rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cuentas")
            spw.AgregarParam("par$rc_terminos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "terminos")
            spw.AgregarParam("par$rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "divisas")
            spw.AgregarParam("par$rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "denominaciones")
            spw.AgregarParam("par$rc_efectivos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "efectivos")
            spw.AgregarParam("par$rc_mediopagos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "mediopagos")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            If peticion.CodigosMaquinas IsNot Nothing AndAlso peticion.CodigosMaquinas.Count > 0 Then
                spw.Param("par$cod_maquinas").AgregarValorArray("")
                For Each maquina In peticion.CodigosMaquinas
                    If Not String.IsNullOrEmpty(maquina) Then
                        spw.Param("par$cod_maquinas").AgregarValorArray(maquina)
                    End If
                Next
            End If
            If peticion.CodigosCanales IsNot Nothing AndAlso peticion.CodigosCanales.Count > 0 Then
                spw.Param("par$cod_canales").AgregarValorArray("")
                For Each canal In peticion.CodigosCanales
                    If Not String.IsNullOrEmpty(canal) Then
                        spw.Param("par$cod_canales").AgregarValorArray(canal)
                    End If
                Next
            End If
            If peticion.CodigosFormularios IsNot Nothing AndAlso peticion.CodigosFormularios.Count > 0 Then
                spw.Param("par$cod_formularios").AgregarValorArray("")
                For Each formulario In peticion.CodigosFormularios
                    If Not String.IsNullOrEmpty(formulario) Then
                        spw.Param("par$cod_formularios").AgregarValorArray(formulario)
                    End If
                Next
            End If
            If peticion.CodigosDivisas IsNot Nothing AndAlso peticion.CodigosDivisas.Count > 0 Then
                spw.Param("par$cod_divisas").AgregarValorArray("")
                For Each divisa In peticion.CodigosDivisas
                    If Not String.IsNullOrEmpty(divisa) Then
                        spw.Param("par$cod_divisas").AgregarValorArray(divisa)
                    End If
                Next
            End If

            Return spw

        End Function


        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           GMTHoraLocalCalculado As Double,
                                           GMTMinutoLocalCalculado As Double,
                                           ByRef respuesta As RecuperarMovimientos.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                Dim valido As Boolean = False

                If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 AndAlso
                    ds.Tables.Contains("cuentas") AndAlso ds.Tables("cuentas").Rows.Count > 0 Then

                    ' PrimaryKey
                    Dim primaryKeyMovimientos(ds.Tables("movimientos").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("movimientos").PrimaryKey
                        primaryKeyMovimientos(primaryKeyMovimientos.Length - 1) = _key
                    Next
                    primaryKeyMovimientos(primaryKeyMovimientos.Length - 1) = ds.Tables("movimientos").Columns("OID_CUENTA_ORIGEN")
                    ds.Tables("movimientos").PrimaryKey = primaryKeyMovimientos

                    Dim primaryKeyEfectivos(ds.Tables("efectivos").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("efectivos").PrimaryKey
                        primaryKeyEfectivos(primaryKeyEfectivos.Length - 1) = _key
                    Next
                    primaryKeyEfectivos(primaryKeyEfectivos.Length - 1) = ds.Tables("efectivos").Columns("OID_DOCUMENTO")
                    ds.Tables("efectivos").PrimaryKey = primaryKeyEfectivos

                    Dim primaryKeyTerminos(ds.Tables("terminos").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("terminos").PrimaryKey
                        primaryKeyTerminos(primaryKeyTerminos.Length - 1) = _key
                    Next
                    primaryKeyTerminos(primaryKeyTerminos.Length - 1) = ds.Tables("terminos").Columns("OID_DOCUMENTO")
                    ds.Tables("terminos").PrimaryKey = primaryKeyTerminos

                    Dim _divisasPosibles As New Dictionary(Of String, RecuperarMovimientos.Divisa)
                    If ds.Tables.Contains("divisas") AndAlso ds.Tables("divisas").Rows.Count > 0 Then
                        For Each rowDivisa As DataRow In ds.Tables("divisas").Rows
                            _divisasPosibles.Add(rowDivisa("OID_DIVISA"), PoblarDivisa(rowDivisa))
                        Next
                    End If

                    Dim _denominacionesPosibles As New Dictionary(Of String, RecuperarMovimientos.Denominacion)
                    If ds.Tables.Contains("denominaciones") AndAlso ds.Tables("denominaciones").Rows.Count > 0 Then
                        For Each rowDenominacion As DataRow In ds.Tables("denominaciones").Rows
                            _denominacionesPosibles.Add(rowDenominacion("OID_DENOMINACION"), PoblarDenominacion(rowDenominacion))
                        Next
                    End If

                    Dim _formulariosPosibles As New Dictionary(Of String, ContractoServicio.Contractos.Integracion.Comon.Entidad)
                    If ds.Tables.Contains("formularios") AndAlso ds.Tables("formularios").Rows.Count > 0 Then
                        For Each rowFormulario As DataRow In ds.Tables("formularios").Rows
                            _formulariosPosibles.Add(rowFormulario("OID_FORMULARIO"), PoblarFormulario(rowFormulario))
                        Next
                    End If

                    respuesta.Paginacion.RegistroPorPagina = ds.Tables("movimientos").Rows.Count

                    If ds.Tables.Contains("cuentas") AndAlso ds.Tables("cuentas").Rows.Count > 0 Then

                        If respuesta.Delegaciones Is Nothing Then respuesta.Delegaciones = New List(Of RecuperarMovimientos.Delegacion)

                        For Each rowCuenta As DataRow In ds.Tables("cuentas").Rows

                            Dim delegacion As RecuperarMovimientos.Delegacion = respuesta.Delegaciones.FirstOrDefault(Function(dl) dl.Codigo = Util.AtribuirValorObj(rowCuenta("COD_DELEGACION"), GetType(String)))

                            If delegacion Is Nothing Then
                                delegacion = PoblarDelegacion(rowCuenta)
                                respuesta.Delegaciones.Add(delegacion)
                            End If

                            Dim planta = delegacion.Plantas.FirstOrDefault(Function(pt) pt.Codigo = Util.AtribuirValorObj(rowCuenta("COD_PLANTA"), GetType(String)))

                            If planta Is Nothing Then
                                planta = PoblarPlanta(rowCuenta)
                                delegacion.Plantas.Add(planta)
                            End If

                            Dim maquina = planta.Maquinas.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SECTOR"), GetType(String)))

                            If maquina Is Nothing Then
                                maquina = PoblarMaquina(rowCuenta)
                                planta.Maquinas.Add(maquina)
                            End If

                            Dim cliente = maquina.Clientes.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)))

                            If cliente Is Nothing Then
                                cliente = PoblarCliente(rowCuenta)
                                maquina.Clientes.Add(cliente)
                            End If

                            Dim subCliente = cliente.SubClientes.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)))

                            If subCliente Is Nothing Then
                                subCliente = PoblarSubCliente(rowCuenta)
                                cliente.SubClientes.Add(subCliente)
                            End If

                            Dim ptoServicio = subCliente.PuntosServicio.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)))

                            If ptoServicio Is Nothing Then
                                ptoServicio = PoblarPuntoServicio(rowCuenta)
                                subCliente.PuntosServicio.Add(ptoServicio)
                            End If

                            Dim canal = ptoServicio.Canales.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)))

                            If canal Is Nothing Then
                                canal = PoblarCanal(rowCuenta)
                                ptoServicio.Canales.Add(canal)
                            End If

                            Dim subCanal = canal.SubCanales.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)))

                            If subCanal Is Nothing Then
                                subCanal = PoblarSubCanal(rowCuenta)
                                canal.SubCanales.Add(subCanal)
                            End If

                            If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 AndAlso ds.Tables("movimientos").Select("OID_CUENTA_ORIGEN = '" & rowCuenta("OID_CUENTA") & "' ") IsNot Nothing Then

                                For Each rowMovimiento As DataRow In ds.Tables("movimientos").Select("OID_CUENTA_ORIGEN = '" & rowCuenta("OID_CUENTA") & "' ")

                                    Dim movimiento = New RecuperarMovimientos.Movimiento
                                    movimiento = PoblarMovimiento(rowMovimiento, GMTHoraLocalCalculado, GMTMinutoLocalCalculado)

                                    If (Util.AtribuirValorObj(rowMovimiento("OID_CUENTA_ORIGEN"), GetType(String)) <> Util.AtribuirValorObj(rowMovimiento("OID_CUENTA_DESTINO"), GetType(String))) Then

                                        Dim rowCuentaDestino As DataRow = ds.Tables("cuentas").Select("OID_CUENTA = '" & rowMovimiento("OID_CUENTA_ORIGEN") & "' ").FirstOrDefault()
                                        movimiento.Destino = PoblarCuentaDestino(rowCuentaDestino)

                                    End If

                                    movimiento.Formulario = _formulariosPosibles.FirstOrDefault(Function(f) f.Key = rowMovimiento("OID_FORMULARIO")).Value

                                    If ds.Tables.Contains("efectivos") AndAlso ds.Tables("efectivos").Rows.Count > 0 AndAlso ds.Tables("efectivos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ") IsNot Nothing Then

                                        For Each rowEfetivo As DataRow In ds.Tables("efectivos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ")

                                            Dim _divisa As RecuperarMovimientos.Divisa = _divisasPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DIVISA")).Value

                                            If movimiento.Valores Is Nothing Then movimiento.Valores = New List(Of RecuperarMovimientos.Divisa)

                                            Dim valor As RecuperarMovimientos.Divisa = movimiento.Valores.FirstOrDefault(Function(d) d.Codigo = _divisa.Codigo)

                                            If valor Is Nothing Then
                                                valor = _divisa.Clonar
                                                movimiento.Valores.Add(valor)
                                            End If

                                            If Util.AtribuirValorObj(rowEfetivo("BOL_DISPONIBLE"), GetType(String)) = 1 Then
                                                movimiento.Disponible = True
                                            End If

                                            If Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "T" Then
                                                valor.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))

                                            ElseIf Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "D" AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowEfetivo("OID_DENOMINACION"), GetType(String))) Then


                                                If valor.Denominaciones Is Nothing Then valor.Denominaciones = New List(Of RecuperarMovimientos.Denominacion)
                                                Dim _denominacion As RecuperarMovimientos.Denominacion = _denominacionesPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DENOMINACION")).Value
                                                Dim valorDenominacion As RecuperarMovimientos.Denominacion = valor.Denominaciones.FirstOrDefault(Function(d) d.Codigo = _denominacion.Codigo)
                                                If valorDenominacion Is Nothing Then
                                                    valorDenominacion = _denominacion.Clonar
                                                    valor.Denominaciones.Add(valorDenominacion)
                                                End If

                                                valorDenominacion.Cantidad = Util.AtribuirValorObj(rowEfetivo("NEL_CANTIDAD"), GetType(String))
                                                valorDenominacion.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))

                                            End If

                                        Next

                                    End If

                                    If ds.Tables.Contains("terminos") AndAlso ds.Tables("terminos").Rows.Count > 0 AndAlso ds.Tables("terminos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ") IsNot Nothing Then
                                        movimiento.CamposAdicionales = New List(Of RecuperarMovimientos.CampoAdicional)
                                        For Each rowTerminos As DataRow In ds.Tables("terminos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ")
                                            movimiento.CamposAdicionales.Add(PoblarCampoAdcional(rowTerminos))
                                        Next
                                    End If

                                    subCanal.Movimientos.Add(movimiento)

                                Next

                            End If

                        Next

                    End If

                    primaryKeyMovimientos = Nothing
                    primaryKeyEfectivos = Nothing
                    primaryKeyTerminos = Nothing
                    _divisasPosibles = Nothing
                    _denominacionesPosibles = Nothing
                    _formulariosPosibles = Nothing

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

        Private Shared Function PoblarDelegacion(row As DataRow) As RecuperarMovimientos.Delegacion
            Dim delegacion = New RecuperarMovimientos.Delegacion

            With delegacion
                .Codigo = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DELEGACION"), GetType(String))
                .Plantas = New List(Of RecuperarMovimientos.Planta)
            End With

            Return delegacion
        End Function

        Private Shared Function PoblarPlanta(row As DataRow) As RecuperarMovimientos.Planta
            Dim planta = New RecuperarMovimientos.Planta

            With planta
                .Codigo = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String))
                .Maquinas = New List(Of RecuperarMovimientos.Maquina)
            End With

            Return planta
        End Function

        Private Shared Function PoblarMaquina(row As DataRow) As RecuperarMovimientos.Maquina
            Dim maquina = New RecuperarMovimientos.Maquina

            With maquina
                .Codigo = Util.AtribuirValorObj(row("COD_IDENTIFICACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_SECTOR"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .Clientes = New List(Of RecuperarMovimientos.Cliente)
            End With

            Return maquina
        End Function

        Private Shared Function PoblarCliente(row As DataRow) As RecuperarMovimientos.Cliente
            Dim cliente = New RecuperarMovimientos.Cliente

            With cliente
                .Codigo = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String))
                .SubClientes = New List(Of RecuperarMovimientos.SubCliente)
            End With

            Return cliente
        End Function

        Private Shared Function PoblarSubCliente(row As DataRow) As RecuperarMovimientos.SubCliente
            Dim subCliente = New RecuperarMovimientos.SubCliente

            With subCliente
                .Codigo = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String))
                .PuntosServicio = New List(Of RecuperarMovimientos.PuntoServicio)
            End With

            Return subCliente
        End Function

        Private Shared Function PoblarPuntoServicio(row As DataRow) As RecuperarMovimientos.PuntoServicio
            Dim puntoServicio = New RecuperarMovimientos.PuntoServicio

            With puntoServicio
                .Codigo = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String))
                .Canales = New List(Of RecuperarMovimientos.Canal)
            End With

            Return puntoServicio
        End Function

        Private Shared Function PoblarCanal(row As DataRow) As RecuperarMovimientos.Canal
            Dim canal = New RecuperarMovimientos.Canal

            With canal
                .Codigo = Util.AtribuirValorObj(row("COD_CANAL"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_CANAL"), GetType(String))
                .SubCanales = New List(Of RecuperarMovimientos.SubCanal)
            End With

            Return canal
        End Function

        Private Shared Function PoblarSubCanal(row As DataRow) As RecuperarMovimientos.SubCanal
            Dim subCanal = New RecuperarMovimientos.SubCanal

            With subCanal
                .Codigo = Util.AtribuirValorObj(row("COD_SUBCANAL"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_SUBCANAL"), GetType(String))
                .Movimientos = New List(Of RecuperarMovimientos.Movimiento)
            End With

            Return subCanal
        End Function


        Private Shared Function PoblarMovimiento(row As DataRow,
                                            ByVal GMTHoraLocalCalculado As Double,
                                            ByVal GMTMinutoLocalCalculado As Double) As RecuperarMovimientos.Movimiento
            Dim movimiento = New RecuperarMovimientos.Movimiento

            With movimiento
                .Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                .Notificado = Util.AtribuirValorObj(row("BOL_NOTIFICADO"), GetType(Boolean))
                .Acreditado = Util.AtribuirValorObj(row("BOL_ACREDITADO"), GetType(Boolean))
                .CodigoCollectionId = Util.AtribuirValorObj(row("COD_COLLECTION_ID"), GetType(String))
                Dim dateGestion = Util.AtribuirValorObj(row("FYH_GESTION"), GetType(String))
                .FechaGestion = DateTime.ParseExact(dateGestion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture)

                Dim FechaRealizacion = Util.AtribuirValorObj(row("FECHA_REALIZACION"), GetType(String))
                .FechaRealizacion = DateTime.ParseExact(FechaRealizacion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture)
                If .Acreditado Then
                    Dim d As String = Util.AtribuirValorObj(row("FYH_ACREDITACION"), GetType(String))
                    If Not String.IsNullOrWhiteSpace(d) Then
                        Dim FechaAcreditacion = Util.AtribuirValorObj(row("FYH_ACREDITACION"), GetType(String))
                        .FechaAcreditacion = DateTime.ParseExact(FechaAcreditacion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture).ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz")
                    End If
                End If
            End With

            Return movimiento
        End Function

        Private Shared Function PoblarFormulario(row As DataRow) As ContractoServicio.Contractos.Integracion.Comon.Entidad
            Dim formulario = New ContractoServicio.Contractos.Integracion.Comon.Entidad

            With formulario
                .Codigo = Util.AtribuirValorObj(row("COD_FORMULARIO"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_FORMULARIO"), GetType(String))
            End With

            Return formulario
        End Function

        Private Shared Function PoblarDivisa(row As DataRow) As RecuperarMovimientos.Divisa
            Dim divisa = New RecuperarMovimientos.Divisa

            With divisa
                .Codigo = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
            End With

            Return divisa
        End Function

        Private Shared Function PoblarDenominacion(row As DataRow) As RecuperarMovimientos.Denominacion
            Dim denominacion = New RecuperarMovimientos.Denominacion

            With denominacion
                .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
            End With

            Return denominacion
        End Function

        Private Shared Function PoblarMedioPago(row As DataRow) As RecuperarMovimientos.MedioPago
            Dim medioPago = New RecuperarMovimientos.MedioPago

            With medioPago
                .CodigoTipoMedioPago = Util.AtribuirValorObj(row("COD_TIPO_MEDIO_PAGO"), GetType(String))
                .CodigoMedioPago = Util.AtribuirValorObj(row("COD_MEDIO_PAGO"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_MEDIO_PAGO"), GetType(String))
                .Unidades = Util.AtribuirValorObj(row("UNIDADES"), GetType(String))
                .Importe = Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(String))
            End With

            Return medioPago
        End Function

        Private Shared Function PoblarCampoAdcional(row As DataRow) As RecuperarMovimientos.CampoAdicional
            Dim campoAdicional = New RecuperarMovimientos.CampoAdicional

            With campoAdicional
                .Descripcion = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                .Valor = Util.AtribuirValorObj(row("DES_VALOR"), GetType(String))
            End With

            Return campoAdicional
        End Function

        Private Shared Function PoblarCuentaDestino(row As DataRow) As RecuperarMovimientos.Cuenta
            Dim cuenta = New RecuperarMovimientos.Cuenta

            With cuenta
                cuenta.Cliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .Cliente
                    .Codigo = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String))
                End With

                cuenta.SubCliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .SubCliente
                    .Codigo = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String))
                End With

                cuenta.PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .PuntoServicio
                    .Codigo = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String))
                End With

                cuenta.Delegacion = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .Delegacion
                    .Codigo = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_DELEGACION"), GetType(String))
                End With

                cuenta.Planta = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .Planta
                    .Codigo = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String))
                End With

                cuenta.Sector = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .Sector
                    .Codigo = Util.AtribuirValorObj(row("COD_SECTOR"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_SECTOR"), GetType(String))
                End With

                cuenta.Canal = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .Canal
                    .Codigo = Util.AtribuirValorObj(row("COD_CANAL"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_CANAL"), GetType(String))
                End With

                cuenta.SubCanal = New ContractoServicio.Contractos.Integracion.Comon.Entidad()
                With .SubCanal
                    .Codigo = Util.AtribuirValorObj(row("COD_SUBCANAL"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_SUBCANAL"), GetType(String))
                End With

            End With

            Return cuenta
        End Function


#Region "[RelacionarMovimientosPeriodos]"

        Public Shared Function RelacionarMovimientosPeriodos(oidLlamada As String, peticion As RelacionarMovimientosPeriodos.Peticion,
                                                             ByRef log As StringBuilder) As List(Of RelacionarMovimientosPeriodos.Salida.Movimiento)

            Dim TiempoParcial As DateTime
            Dim movimientos As New List(Of RelacionarMovimientosPeriodos.Salida.Movimiento)

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = srelacionar_mov_periodo(oidLlamada, peticion)
                log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                PoblarMovimientosPeriodos(ds, movimientos)
                log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                If movimientos IsNot Nothing AndAlso movimientos.Count > 0 Then
                    ' Valida si hubo algun error y cambia el Tipo de Resultado
                    For Each m In movimientos
                        If m.Detalles IsNot Nothing AndAlso m.Detalles.FirstOrDefault(Function(x) x.Codigo.Substring(0, 1) <> "0") IsNot Nothing Then
                            m.TipoResultado = "2"
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

            Return movimientos

        End Function

        Private Shared Function srelacionar_mov_periodo(oidLlamada As String, peticion As RelacionarMovimientosPeriodos.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PPERIODO_{0}.srelacionar_mov_periodo_baja", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, oidLlamada, , False)
            spw.AgregarParam("par$abol_baja", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$abol_codigo_simple", ProsegurDbType.Logico, Nothing, , True)
            spw.AgregarParam("par$acod_movimiento", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            For Each movimiento In peticion.Movimientos
                spw.Param("par$acod_movimiento").AgregarValorArray(movimiento.Codigo)
                If movimiento.Accion = 1 Then
                    spw.Param("par$abol_baja").AgregarValorArray(1)
                Else
                    spw.Param("par$abol_baja").AgregarValorArray(0)
                End If

                If movimiento.TipoCodigo = 1 Then
                    spw.Param("par$abol_codigo_simple").AgregarValorArray(0)
                Else
                    spw.Param("par$abol_codigo_simple").AgregarValorArray(1)
                End If

            Next

            Return spw

        End Function

        Private Shared Sub PoblarMovimientosPeriodos(ds As DataSet, ByRef movimientos As List(Of RelacionarMovimientosPeriodos.Salida.Movimiento))

            If movimientos Is Nothing Then movimientos = New List(Of RelacionarMovimientosPeriodos.Salida.Movimiento)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("validaciones").Rows

                        Dim movimiento = movimientos.FirstOrDefault(Function(x) x.Codigo = Util.AtribuirValorObj(row("COD_MOVIMIENTO"), GetType(String)))

                        If movimiento Is Nothing Then
                            movimiento = New RelacionarMovimientosPeriodos.Salida.Movimiento
                            movimiento.Codigo = Util.AtribuirValorObj(row("COD_MOVIMIENTO"), GetType(String))
                            movimiento.TipoResultado = "0"
                            movimientos.Add(movimiento)
                        End If

                        If movimiento.Detalles Is Nothing Then movimiento.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                        Dim detalle As New ContractoServicio.Contractos.Integracion.Comon.Detalle
                        detalle.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                        detalle.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                        movimiento.Detalles.Add(detalle)

                    Next

                End If

            End If

        End Sub

#End Region


#Region "RecuperarMovimientos por actual id"
        Public Shared Function RecuperarMovimientosPorActualID(identificadorLlamada As String, ActualId As String, codigoUsuario As String, codigoPais As String,
                                                             ByRef log As StringBuilder, ByRef listSaldoPeriodoExcedido As List(Of FechaValorOnline.SaldoPeriodo)) As List(Of FechaValorOnline.Salida.EnvioMovimientoOnline)

            Dim TiempoParcial As DateTime
            Dim movimientos As New List(Of EnvioMovimientoOnline)

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                listSaldoPeriodoExcedido = New List(Of FechaValorOnline.SaldoPeriodo)

                TiempoParcial = Now
                spw = srecuperar_por_actual_id(identificadorLlamada, ActualId, codigoUsuario, codigoPais)
                log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                PoblarMovimientosActualId(ds, movimientos, listSaldoPeriodoExcedido)
                log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT,
                                                         MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return movimientos

        End Function


        Private Shared Function srecuperar_por_actual_id(identificadorLlamada As String, ActualId As String, codigoUsuario As String, codigoPais As String) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_doc_actual_id", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, codigoPais, , False)
            spw.AgregarParam("par$cod_actual_id", ProsegurDbType.Identificador_Alfanumerico, ActualId, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario, , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")
            spw.AgregarParam("par$rc_limites_maq", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "limites_maquina")
            spw.AgregarParam("par$rc_saldos_periodo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "saldos_periodo")
            spw.AgregarParam("par$rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cuentas")
            spw.AgregarParam("par$rc_valores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "valores")
            spw.AgregarParam("par$rc_terminos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "terminos")
            spw.AgregarParam("par$rc_dato_bancario", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datobancario")
            spw.AgregarParam("par$rc_direccion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "direccion")
            spw.AgregarParam("par$rc_planificacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificacion")
            spw.AgregarParam("par$rc_canales", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "canales")
            spw.AgregarParam("par$rc_programacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "programacion")
            spw.AgregarParam("par$rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "divisas")
            spw.AgregarParam("par$rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "denominaciones")
            spw.AgregarParam("par$rc_formularios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "formularios")
            spw.AgregarParam("par$rc_codigos_ajenos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ajeno")
            spw.AgregarParam("par$rc_planificacion_maquina", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificacion_maquina")

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function



        Private Shared Sub PoblarMovimientosActualId(ds As DataSet, ByRef movimientos As List(Of FechaValorOnline.Salida.EnvioMovimientoOnline), ByRef listSaldoPeriodoExcedido As List(Of FechaValorOnline.SaldoPeriodo))

            If movimientos Is Nothing Then movimientos = New List(Of FechaValorOnline.Salida.EnvioMovimientoOnline)

            Dim movimiento As FechaValorOnline.Salida.EnvioMovimientoOnline
            Dim listLimiteMaquina As List(Of FechaValorOnline.LimiteMaquina)
            Dim limiteMaquina As FechaValorOnline.LimiteMaquina
            Dim listSaldoPeriodo As List(Of FechaValorOnline.SaldoPeriodo)
            Dim saldoPeriodo As FechaValorOnline.SaldoPeriodo
            Dim listaValoresMovimiento As List(Of FechaValorOnline.ValoresMovimiento)
            Dim valorMovimiento As Double = 0

            Try

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                    If ds.Tables.Contains("cuentas") AndAlso ds.Tables("cuentas").Rows.Count > 0 Then
                        movimiento = New EnvioMovimientoOnline()
                        For Each row As DataRow In ds.Tables("cuentas").Rows
                            'Inicio Cargar Delegacion
                            movimiento.Delegacion = New FechaValorOnline.Salida.Delegacion()
                            movimiento.Delegacion.Codigo = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                            movimiento.Delegacion.Descripcion = Util.AtribuirValorObj(row("DES_DELEGACION"), GetType(String))
                            'Carregar Codigos Ajenos Dlegacion
                            movimiento.Delegacion.CodigosAjenos = PoblarCodigoAjeno(ds, Util.AtribuirValorObj(row("OID_DELEGACION"), GetType(String)), "GEPR_TDELEGACION")
                            'Fim Cargar Delegacion

                            'Inicio Cargar Planta
                            movimiento.Planta = New FechaValorOnline.Salida.Planta()
                            movimiento.Planta.Codigo = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                            movimiento.Planta.Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String))
                            'Carregar Codigos Ajenos Planta
                            movimiento.Planta.CodigosAjenos = PoblarCodigoAjeno(ds, Util.AtribuirValorObj(row("OID_PLANTA"), GetType(String)), "GEPR_TPLANTA")
                            'Fim Cargar Planta

                            'Inicio Cargar Maquina
                            movimiento.Maquina = New FechaValorOnline.Salida.Maquina()
                            movimiento.Maquina.Codigo = Util.AtribuirValorObj(row("COD_MAQUINA"), GetType(String))
                            movimiento.Maquina.Descripcion = Util.AtribuirValorObj(row("DES_MAQUINA"), GetType(String))
                            movimiento.Maquina.Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                            'Carregar Codigos Ajenos Dlegacion
                            movimiento.Maquina.CodigosAjenos = PoblarCodigoAjeno(ds, Util.AtribuirValorObj(row("OID_SECTOR"), GetType(String)), "GEPR_TSECTOR")
                            'Fim Cargar Maquina


                            'Inicio Cargar Cliente
                            movimiento.Cliente = New FechaValorOnline.Salida.Cliente()
                            movimiento.Cliente.Codigo = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String))
                            movimiento.Cliente.Descripcion = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String))

                            movimiento.Cliente.Direccion = PoblarDireccion(ds, "GEPR_TCLIENTE", row("OID_CLIENTE"))
                            movimiento.Cliente.DatosBancarios = PoblarDatoBancario(ds, row("OID_CLIENTE"), Nothing, Nothing)
                            'Fim Cargar Cliente

                            'Inicio Cargar SubCliente
                            movimiento.SubCliente = New FechaValorOnline.Salida.SubCliente()
                            movimiento.SubCliente.Codigo = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String))
                            movimiento.SubCliente.Descripcion = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String))


                            movimiento.SubCliente.Direccion = PoblarDireccion(ds, "GEPR_TSUBCLIENTE", Util.AtribuirValorObj(row("OID_SUBCLIENTE"), GetType(String)))
                            movimiento.SubCliente.DatosBancarios = PoblarDatoBancario(ds, Util.AtribuirValorObj(row("OID_CLIENTE"), GetType(String)),
                                                                                   Util.AtribuirValorObj(row("OID_SUBCLIENTE"), GetType(String)),
                                                                                   Nothing)
                            'Fim Cargar SubCliente

                            'Inicio Cargar Punto Servicio
                            movimiento.PuntoServicio = New FechaValorOnline.Salida.PuntoServicio()
                            movimiento.PuntoServicio.Codigo = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
                            movimiento.PuntoServicio.Descripcion = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String))

                            movimiento.PuntoServicio.Direccion = PoblarDireccion(ds, "GEPR_TPUNTO_SERVICIO", row("OID_PTO_SERVICIO"))

                            movimiento.PuntoServicio.DatosBancarios = PoblarDatoBancario(ds,
                                                                                   Util.AtribuirValorObj(row("OID_CLIENTE"), GetType(String)),
                                                                                   Util.AtribuirValorObj(row("OID_SUBCLIENTE"), GetType(String)),
                                                                                   Util.AtribuirValorObj(row("OID_PTO_SERVICIO"), GetType(String)))
                            'Fim Cargar Punto Servicio

                        Next

                        'Cargar lista limites_maq
                        If ds.Tables.Contains("limites_maquina") AndAlso ds.Tables("limites_maquina").Rows.Count > 0 Then
                            For Each rowMovimiento As DataRow In ds.Tables("limites_maquina").Rows
                                If listLimiteMaquina Is Nothing Then listLimiteMaquina = New List(Of FechaValorOnline.LimiteMaquina)

                                limiteMaquina = New FechaValorOnline.LimiteMaquina()
                                limiteMaquina.NUM_LIMITE = Util.AtribuirValorObj(rowMovimiento("NUM_LIMITE"), GetType(String))
                                limiteMaquina.OID_DIVISA = Util.AtribuirValorObj(rowMovimiento("OID_DIVISA"), GetType(String))
                                listLimiteMaquina.Add(limiteMaquina)
                            Next
                        End If

                        'Carga lista saldo periodo
                        If ds.Tables.Contains("saldos_periodo") AndAlso ds.Tables("saldos_periodo").Rows.Count > 0 Then
                            For Each rowMovimiento As DataRow In ds.Tables("saldos_periodo").Rows

                                If listSaldoPeriodo Is Nothing Then listSaldoPeriodo = New List(Of FechaValorOnline.SaldoPeriodo)

                                saldoPeriodo = New FechaValorOnline.SaldoPeriodo()
                                saldoPeriodo.OID_DIVISA = Util.AtribuirValorObj(rowMovimiento("OID_DIVISA"), GetType(String))
                                saldoPeriodo.OID_MAQUINA = Util.AtribuirValorObj(rowMovimiento("OID_MAQUINA"), GetType(String))
                                saldoPeriodo.IMPORTE = Util.AtribuirValorObj(rowMovimiento("IMPORTE"), GetType(String))
                                saldoPeriodo.COD_MAQUINA = Util.AtribuirValorObj(rowMovimiento("COD_MAQUINA"), GetType(String))

                                listSaldoPeriodo.Add(saldoPeriodo)

                            Next
                        End If

                        'PlanificacionMaquina
                        If ds.Tables.Contains("planificacion_maquina") AndAlso ds.Tables("planificacion_maquina").Rows.Count > 0 Then
                            movimiento.Maquina.Planificacion = New PlanificacionMaquina() With
                            {
                                .PuntoServicios = New List(Of PuntoServicioPlanificacion)
                            }

                            For Each rowPlanificacionMaquina As DataRow In ds.Tables("planificacion_maquina").Rows
                                Dim oid_maquina = Util.AtribuirValorObj(rowPlanificacionMaquina("OID_MAQUINA"), GetType(String))
                                Dim cod_maquina = Util.AtribuirValorObj(rowPlanificacionMaquina("COD_IDENTIFICACION"), GetType(String))
                                Dim oid_punto = Util.AtribuirValorObj(rowPlanificacionMaquina("OID_PTO_SERVICIO"), GetType(String))
                                Dim cod_punto = Util.AtribuirValorObj(rowPlanificacionMaquina("COD_PTO_SERVICIO"), GetType(String))

                                If movimiento.Maquina.Codigo = cod_maquina AndAlso
                                   Not movimiento.Maquina.Planificacion.PuntoServicios.Exists(Function(x) x.Codigo = cod_punto) Then

                                    Dim puntoServicioPlanificacion = New PuntoServicioPlanificacion() With
                                    {
                                        .Codigo = cod_punto,
                                        .Descripcion = Util.AtribuirValorObj(rowPlanificacionMaquina("DES_PTO_SERVICIO"), GetType(String))
                                    }

                                    movimiento.Maquina.Planificacion.PuntoServicios.Add(puntoServicioPlanificacion)


                                    If ds.Tables.Contains("canales") AndAlso ds.Tables("canales").Rows.Count > 0 Then
                                        ' CANALES DEL PUNTO
                                        Dim rCanalesPunto() As DataRow = ds.Tables("canales").Select("CANAL_OID_PUNTO ='" & oid_punto & "' AND TIPO_CANAL = 'PUNTO'")
                                        If rCanalesPunto IsNot Nothing AndAlso rCanalesPunto.Count > 0 Then

                                            Dim canales = New List(Of FechaValorOnline.Salida.Canal)
                                            For Each rowCanal As DataRow In rCanalesPunto
                                                Dim codCanal = Util.AtribuirValorObj(rowCanal("COD_CANAL"), GetType(String))
                                                Dim codSubcanal = Util.AtribuirValorObj(rowCanal("COD_SUBCANAL"), GetType(String))

                                                Dim canal = canales.FirstOrDefault(Function(x) x.Codigo = codCanal)

                                                If canal Is Nothing Then
                                                    canal = New FechaValorOnline.Salida.Canal
                                                    canal.Codigo = codCanal
                                                    canal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_CANAL"), GetType(String))
                                                    canal.SubCanal = New List(Of FechaValorOnline.Salida.SubCanal)
                                                    canales.Add(canal)
                                                End If

                                                If Not String.IsNullOrWhiteSpace(codSubcanal) AndAlso Not canal.SubCanal.Exists(Function(x) x.Codigo = codSubcanal) Then
                                                    Dim subCanal = New FechaValorOnline.Salida.SubCanal
                                                    subCanal.Codigo = codSubcanal
                                                    subCanal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_SUBCANAL"), GetType(String))
                                                    canal.SubCanal.Add(subCanal)
                                                End If
                                            Next

                                            puntoServicioPlanificacion.Canales = canales

                                        End If

                                        ' CANALES DE LA MAQUINA
                                        Dim rCanalesMaq() As DataRow = ds.Tables("canales").Select("CANAL_OID_MAQUINA ='" & oid_maquina & "' AND TIPO_CANAL = 'MAQUINA'")

                                        If rCanalesMaq IsNot Nothing AndAlso rCanalesMaq.Count > 0 Then

                                            Dim canales = New List(Of FechaValorOnline.Salida.Canal)
                                            For Each rowCanal As DataRow In rCanalesMaq
                                                Dim codCanal = Util.AtribuirValorObj(rowCanal("COD_CANAL"), GetType(String))
                                                Dim codSubcanal = Util.AtribuirValorObj(rowCanal("COD_SUBCANAL"), GetType(String))

                                                Dim canal = canales.FirstOrDefault(Function(x) x.Codigo = codCanal)

                                                If canal Is Nothing Then
                                                    canal = New FechaValorOnline.Salida.Canal
                                                    canal.Codigo = codCanal
                                                    canal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_CANAL"), GetType(String))
                                                    canal.SubCanal = New List(Of FechaValorOnline.Salida.SubCanal)
                                                    canales.Add(canal)
                                                End If

                                                If Not String.IsNullOrWhiteSpace(codSubcanal) AndAlso Not canal.SubCanal.Exists(Function(x) x.Codigo = codSubcanal) Then
                                                    Dim subCanal = New FechaValorOnline.Salida.SubCanal
                                                    subCanal.Codigo = codSubcanal
                                                    subCanal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_SUBCANAL"), GetType(String))
                                                    canal.SubCanal.Add(subCanal)
                                                End If
                                            Next

                                            movimiento.Maquina.Planificacion.Canales = canales
                                        End If
                                    End If
                                End If
                            Next
                        End If

                        'Movimientos
                        If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 Then

                            'Asignamos el ActualId del primer Documento
                            movimiento.ActualID = ds.Tables("movimientos").Rows(0)("COD_ACTUAL_ID").ToString()

                            For Each rowMovimiento As DataRow In ds.Tables("movimientos").Rows
                                If movimiento.Movimientos Is Nothing Then movimiento.Movimientos = New List(Of FechaValorOnline.Salida.Movimiento)

                                Dim mov As FechaValorOnline.Salida.Movimiento = New FechaValorOnline.Salida.Movimiento()
                                For Each rowCuenta As DataRow In ds.Tables("cuentas").Select("OID_CUENTA = '" & Util.AtribuirValorObj(rowMovimiento("OID_CUENTA_ORIGEN"), GetType(String)) & "' ")

                                    mov.Canal = New FechaValorOnline.Salida.Canal()
                                    mov.Canal.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String))
                                    mov.Canal.Descripcion = Util.AtribuirValorObj(rowCuenta("DES_CANAL"), GetType(String))

                                    mov.Canal.SubCanal = New List(Of FechaValorOnline.Salida.SubCanal) From {
                                        New FechaValorOnline.Salida.SubCanal() With
                                        {
                                            .Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)),
                                            .Descripcion = Util.AtribuirValorObj(rowCuenta("DES_SUBCANAL"), GetType(String))
                                        }
                                    }


                                Next

                                mov.Codigo = Util.AtribuirValorObj(rowMovimiento("CODIGO"), GetType(String))
                                mov.CollectionID = Util.AtribuirValorObj(rowMovimiento("COD_COLLECTION_ID"), GetType(String))

                                mov.Notificado = Util.AtribuirValorObj(rowMovimiento("BOL_NOTIFICADO"), GetType(String))
                                mov.Acreditado = Util.AtribuirValorObj(rowMovimiento("BOL_ACREDITADO"), GetType(String))
                                mov.Disponible = Util.AtribuirValorObj(rowMovimiento("DISPONIBLE"), GetType(String))

                                Dim fyhGestion = Util.AtribuirValorObj(rowMovimiento("FYH_GESTION"), GetType(DateTime))
                                Dim fyhGestionGmtDelegacion = Util.AtribuirValorObj(rowMovimiento("GMTDELE_FYH_GESTION"), GetType(String))
                                If fyhGestion IsNot Nothing AndAlso fyhGestionGmtDelegacion IsNot Nothing Then
                                    mov.FechaGestion = Util.ConvertToDateTimeOffset(fyhGestion, fyhGestionGmtDelegacion)
                                Else
                                    mov.FechaGestion = fyhGestion
                                End If

                                Dim fyhRealizacion = Util.AtribuirValorObj(rowMovimiento("FECHA_REALIZACION"), GetType(DateTime))
                                Dim fyhRealizacionGmtDelegacion = Util.AtribuirValorObj(rowMovimiento("GMTDELE_FECHA_REALIZACION"), GetType(String))
                                If fyhRealizacion IsNot Nothing AndAlso fyhRealizacionGmtDelegacion IsNot Nothing Then
                                    mov.FechaRealizacion = Util.ConvertToDateTimeOffset(fyhRealizacion, fyhRealizacionGmtDelegacion)
                                Else
                                    mov.FechaRealizacion = fyhRealizacion
                                End If

                                Dim fyhContable = Util.AtribuirValorObj(rowMovimiento("FEC_CONTABLE"), GetType(DateTime))
                                Dim fyhContableGmtDelegacion = Util.AtribuirValorObj(rowMovimiento("GMTDELE_FEC_CONTABLE"), GetType(String))
                                If fyhContable IsNot Nothing AndAlso fyhContableGmtDelegacion IsNot Nothing Then
                                    mov.FechaContable = Util.ConvertToDateTimeOffset(fyhContable, fyhContableGmtDelegacion)
                                Else
                                    mov.FechaContable = fyhContable
                                End If

                                mov.Formulario = New FechaValorOnline.Salida.Formulario
                                mov.Formulario.Codigo = Util.AtribuirValorObj(rowMovimiento("COD_FORMULARIO"), GetType(String))
                                mov.Formulario.Descripcion = Util.AtribuirValorObj(rowMovimiento("DES_FORMULARIO"), GetType(String))
                                'mov.Valores As List(Of Valores)

                                If ds.Tables.Contains("valores") AndAlso ds.Tables("valores").Rows.Count > 0 AndAlso ds.Tables("valores").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ") IsNot Nothing Then

                                    For Each rowEfetivo As DataRow In ds.Tables("valores").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ")

                                        If mov.Valores Is Nothing Then mov.Valores = New List(Of FechaValorOnline.Salida.Divisa)

                                        'Dim _divisa As RecuperarMovimientos.Divisa = _divisasPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DIVISA")).Value

                                        Dim valor As FechaValorOnline.Salida.Divisa = mov.Valores.FirstOrDefault(Function(d) d.Codigo = Util.AtribuirValorObj(rowEfetivo("COD_DIVISA"), GetType(String)))

                                        If valor Is Nothing Then
                                            valor = New FechaValorOnline.Salida.Divisa
                                            valor.Codigo = Util.AtribuirValorObj(rowEfetivo("COD_DIVISA"), GetType(String))
                                            valor.Descripcion = Util.AtribuirValorObj(rowEfetivo("DES_DIVISA"), GetType(String))
                                            mov.Valores.Add(valor)
                                        End If

                                        If Util.AtribuirValorObj(rowEfetivo("BOL_DISPONIBLE"), GetType(String)) = 1 Then
                                            mov.Disponible = True
                                        End If

                                        If Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "T" Then
                                            valor.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))

                                        ElseIf Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "D" AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowEfetivo("OID_DENOMINACION"), GetType(String))) Then

                                            If valor.Denominaciones Is Nothing Then valor.Denominaciones = New List(Of FechaValorOnline.Salida.Denominacion)
                                            'Dim _denominacion As RecuperarMovimientos.Denominacion = _denominacionesPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DENOMINACION")).Value
                                            Dim valorDenominacion As FechaValorOnline.Salida.Denominacion = valor.Denominaciones.FirstOrDefault(Function(d) d.Codigo = Util.AtribuirValorObj(rowEfetivo("COD_DENOMINACION"), GetType(String)))
                                            If valorDenominacion Is Nothing Then
                                                valorDenominacion = New FechaValorOnline.Salida.Denominacion()
                                                valorDenominacion.Codigo = Util.AtribuirValorObj(rowEfetivo("COD_DENOMINACION"), GetType(String))
                                                valorDenominacion.Descripcion = Util.AtribuirValorObj(rowEfetivo("DES_DENOMINACION"), GetType(String))
                                                valor.Denominaciones.Add(valorDenominacion)
                                            End If

                                            valorDenominacion.Cantidad = Util.AtribuirValorObj(rowEfetivo("NEL_CANTIDAD"), GetType(String))
                                            valorDenominacion.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))

                                        End If

                                        If listaValoresMovimiento Is Nothing Then listaValoresMovimiento = New List(Of FechaValorOnline.ValoresMovimiento)

                                        Dim valorMov As FechaValorOnline.ValoresMovimiento = listaValoresMovimiento.FirstOrDefault(Function(d) d.COD_DIVISA = Util.AtribuirValorObj(rowEfetivo("COD_DIVISA"), GetType(String)))

                                        If valorMov Is Nothing Then
                                            valorMov = New FechaValorOnline.ValoresMovimiento
                                            valorMov.COD_DIVISA = Util.AtribuirValorObj(rowEfetivo("COD_DIVISA"), GetType(String))
                                            valorMov.OID_DIVISA = Util.AtribuirValorObj(rowEfetivo("OID_DIVISA"), GetType(String))
                                            valorMov.IMPORTE = valor.Importe
                                            listaValoresMovimiento.Add(valorMov)
                                        End If
                                    Next
                                End If

                                mov.CamposAdicionales = New List(Of FechaValorOnline.Salida.CampoAdicional)

                                For Each rowTermino As DataRow In ds.Tables("terminos").Select("OID_DOCUMENTO = '" & Util.AtribuirValorObj(rowMovimiento("OID_DOCUMENTO"), GetType(String)) & "' ")

                                    If mov.CamposAdicionales Is Nothing Then mov.CamposAdicionales = New List(Of FechaValorOnline.Salida.CampoAdicional)

                                    Dim objCampoAdicional = New FechaValorOnline.Salida.CampoAdicional()

                                    objCampoAdicional.Descripcion = Util.AtribuirValorObj(rowTermino("COD_TERMINO"), GetType(String))
                                    objCampoAdicional.Valor = Util.AtribuirValorObj(rowTermino("DES_VALOR"), GetType(String))
                                    mov.CamposAdicionales.Add(objCampoAdicional)

                                Next

                                movimiento.Movimientos.Add(mov)

                            Next
                        End If
                        ' Planificaciones
                        If ds.Tables.Contains("planificacion") AndAlso ds.Tables("planificacion").Rows.Count > 0 Then


                            For Each rowPlanificacion As DataRow In ds.Tables("planificacion").Rows

                                movimiento.Planificacion = New Planificacion

                                Dim FechaHoraVigenciaInicio As DateTimeOffset?
                                Dim fyhVigenciaInicio = Util.AtribuirValorObj(rowPlanificacion("FYH_VIGENCIA_INICIO"), GetType(DateTime))
                                Dim fyhVigenciaInicioGmtDelegacion = Util.AtribuirValorObj(rowPlanificacion("GMTDELE_FYH_VIGENCIA_INICIO"), GetType(String))
                                If fyhVigenciaInicio IsNot Nothing AndAlso fyhVigenciaInicioGmtDelegacion IsNot Nothing Then
                                    FechaHoraVigenciaInicio = Util.ConvertToDateTimeOffset(fyhVigenciaInicio, fyhVigenciaInicioGmtDelegacion)
                                Else
                                    FechaHoraVigenciaInicio = Util.ConvertToDateTimeOffset(fyhVigenciaInicio, "00:00")
                                End If

                                Dim FechaHoraVigenciaFin As DateTimeOffset?
                                Dim fyhVigenciaFin = Util.AtribuirValorObj(rowPlanificacion("FYH_VIGENCIA_FIN"), GetType(DateTime))
                                Dim fyhVigenciaFinGmtDelegacion = Util.AtribuirValorObj(rowPlanificacion("GMTDELE_FYH_VIGENCIA_FIN"), GetType(String))
                                If fyhVigenciaFin IsNot Nothing AndAlso fyhVigenciaFinGmtDelegacion IsNot Nothing Then
                                    FechaHoraVigenciaFin = Util.ConvertToDateTimeOffset(fyhVigenciaFin, fyhVigenciaFinGmtDelegacion)
                                Else
                                    FechaHoraVigenciaFin = Util.ConvertToDateTimeOffset(fyhVigenciaFin, "00:00")
                                End If


                                With movimiento.Planificacion

                                    .Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_PLANIFICACION"), GetType(String))
                                    .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_PLANIFICACION"), GetType(String))
                                    .FechaHoraVigenciaInicio = FechaHoraVigenciaInicio
                                    .FechaHoraVigenciaFin = FechaHoraVigenciaFin
                                    .Vigente = Util.AtribuirValorObj(rowPlanificacion("BOL_ACTIVO"), GetType(String))
                                    .AgrupacionSubcanales = Util.AtribuirValorObj(rowPlanificacion("BOL_AGR_SUBCANAL"), GetType(Boolean))
                                    .AgrupacionFechaContable = Util.AtribuirValorObj(rowPlanificacion("BOL_AGR_SUBCANAL"), GetType(Boolean))
                                    .AgrupacionPtoServicio = Util.AtribuirValorObj(rowPlanificacion("BOL_AGR_SUBCANAL"), GetType(Boolean))
                                    .MinutosAcreditacion = Util.AtribuirValorObj(rowPlanificacion("NEC_CONTINGENCIA"), GetType(String))
                                    .Tipo = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_TIPO_PLANIFICACION"), GetType(String)),
                                                                                                         .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_TIPO_PLANIFICACION"), GetType(String))}

                                    .Banco = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_CLIENTE"), GetType(String)),
                                                                                                          .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_CLIENTE"), GetType(String))}

                                    .Delegacion = New ContractoServicio.Contractos.Integracion.Comon.Entidad With {.Codigo = Util.AtribuirValorObj(rowPlanificacion("COD_DELEGACION"), GetType(String)),
                                                                                                                .Descripcion = Util.AtribuirValorObj(rowPlanificacion("DES_DELEGACION"), GetType(String))}

                                    ' canales
                                    If ds.Tables.Contains("canales") AndAlso ds.Tables("canales").Rows.Count > 0 Then

                                        Dim rCanales() As DataRow = ds.Tables("canales").Select("OID_PLANIFICACION ='" & Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String)) & "' AND TIPO_CANAL = 'PLANIFICACION'")

                                        If rCanales IsNot Nothing AndAlso rCanales.Count > 0 Then

                                            .Canales = New List(Of FechaValorOnline.Salida.Canal)

                                            For Each rowCanal As DataRow In rCanales
                                                Dim codCanal = Util.AtribuirValorObj(rowCanal("COD_CANAL"), GetType(String))
                                                Dim codSubcanal = Util.AtribuirValorObj(rowCanal("COD_SUBCANAL"), GetType(String))

                                                Dim canal = .Canales.FirstOrDefault(Function(x) x.Codigo = codCanal)

                                                If canal Is Nothing Then
                                                    canal = New FechaValorOnline.Salida.Canal
                                                    canal.Codigo = codCanal
                                                    canal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_CANAL"), GetType(String))
                                                    canal.SubCanal = New List(Of FechaValorOnline.Salida.SubCanal)

                                                    .Canales.Add(canal)
                                                End If

                                                If Not String.IsNullOrWhiteSpace(codSubcanal) AndAlso Not canal.SubCanal.Exists(Function(x) x.Codigo = codSubcanal) Then
                                                    Dim subCanal = New FechaValorOnline.Salida.SubCanal
                                                    subCanal.Codigo = codSubcanal
                                                    subCanal.Descripcion = Util.AtribuirValorObj(rowCanal("DES_SUBCANAL"), GetType(String))
                                                    canal.SubCanal.Add(subCanal)
                                                End If
                                            Next
                                        End If
                                    End If

                                    ' programaciones
                                    If ds.Tables.Contains("programacion") AndAlso ds.Tables("programacion").Rows.Count > 0 Then

                                        Dim rProgramaciones() As DataRow = ds.Tables("programacion").Select("OID_PLANIFICACION ='" & Util.AtribuirValorObj(rowPlanificacion("OID_PLANIFICACION"), GetType(String)) & "'")

                                        If rProgramaciones IsNot Nothing AndAlso rProgramaciones.Count > 0 Then

                                            .Programaciones = New List(Of Programacion)

                                            For Each rowProgramacion As DataRow In rProgramaciones

                                                Dim programacion As New Programacion

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


                                End With

                            Next

                        End If

                    End If

                    'Cargo Movimientos excedidos de limite
                    If listSaldoPeriodo IsNot Nothing AndAlso listLimiteMaquina IsNot Nothing Then
                        For Each lm As FechaValorOnline.LimiteMaquina In listLimiteMaquina
                            For Each sp As FechaValorOnline.SaldoPeriodo In listSaldoPeriodo
                                valorMovimiento = 0
                                If listaValoresMovimiento IsNot Nothing Then
                                    For Each valMov As FechaValorOnline.ValoresMovimiento In listaValoresMovimiento
                                        If (sp.OID_DIVISA = valMov.OID_DIVISA) Then valorMovimiento += valMov.IMPORTE
                                    Next
                                End If

                                If (sp.OID_DIVISA = lm.OID_DIVISA) And (Convert.ToDecimal(sp.IMPORTE) + valorMovimiento > Convert.ToDecimal(lm.NUM_LIMITE)) Then
                                    listSaldoPeriodoExcedido.Add(sp)
                                End If
                            Next
                        Next
                    End If

                    If Not (listSaldoPeriodoExcedido IsNot Nothing AndAlso listSaldoPeriodoExcedido.Count > 0) Then
                        If movimiento IsNot Nothing Then
                            movimientos.Add(movimiento)
                        End If
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

        Private Shared Function PoblarCodigoAjeno(ds As DataSet, IdentificadorTabla As String, CodigoTipoTabla As String) As List(Of CodigoAjeno)
            Dim lstAjeno As List(Of CodigoAjeno) = Nothing
            If ds.Tables.Contains("ajeno") AndAlso ds.Tables("ajeno").Rows.Count > 0 Then
                For Each rowAjeno As DataRow In ds.Tables("ajeno").Select("COD_TIPO_TABLA_GENESIS = '" & CodigoTipoTabla & "' AND OID_TABLA_GENESIS = '" & IdentificadorTabla & "' ")
                    If lstAjeno Is Nothing Then lstAjeno = New List(Of CodigoAjeno)

                    Dim objAjeno = New CodigoAjeno With {
                        .Identificador = Util.AtribuirValorObj(rowAjeno("COD_IDENTIFICADOR"), GetType(String)),
                        .Codigo = Util.AtribuirValorObj(rowAjeno("COD_AJENO"), GetType(String)),
                        .Descripcion = Util.AtribuirValorObj(rowAjeno("DES_AJENO"), GetType(String))
                    }
                    lstAjeno.Add(objAjeno)
                Next
            End If
            Return lstAjeno
        End Function

        Private Shared Function PoblarDatoBancario(ds As DataSet, IdentificadorCliente As String, IdentificadorSubcliente As String, IdentificadorPuntoServicio As String) As List(Of FechaValorOnline.Salida.DatoBancario)

            Dim respuesta = New List(Of FechaValorOnline.Salida.DatoBancario)

            Dim qry = String.Empty

            qry = "OID_CLIENTE = '" & IdentificadorCliente & "' "
            If String.IsNullOrWhiteSpace(IdentificadorSubcliente) Then
                qry = qry + " AND OID_SUBCLIENTE IS NULL  "
            Else
                qry = qry + " AND OID_SUBCLIENTE = '" & IdentificadorSubcliente & "'  "
            End If


            If String.IsNullOrWhiteSpace(IdentificadorPuntoServicio) Then
                qry = qry + " AND OID_PTO_SERVICIO IS NULL  "
            Else
                qry = qry + " AND OID_PTO_SERVICIO = '" & IdentificadorPuntoServicio & "'  "
            End If

            If ds.Tables.Contains("datobancario") AndAlso ds.Tables("datobancario").Rows.Count > 0 AndAlso ds.Tables("datobancario").Select(qry) IsNot Nothing Then

                For Each rowDatoBancario As DataRow In ds.Tables("datobancario").Select(qry)
                    Dim dban = New FechaValorOnline.Salida.DatoBancario()
                    dban.Banco = Util.AtribuirValorObj(rowDatoBancario("COD_CLIENTE"), GetType(String))
                    dban.DescripcionBanco = Util.AtribuirValorObj(rowDatoBancario("DES_CLIENTE"), GetType(String))
                    dban.NroDocumento = Util.AtribuirValorObj(rowDatoBancario("NUMERO_DOCUMENTO"), GetType(String))
                    dban.Agencia = Util.AtribuirValorObj(rowDatoBancario("AGENCIA"), GetType(String))
                    dban.NroCuenta = Util.AtribuirValorObj(rowDatoBancario("NUMERO_CUENTA"), GetType(String))
                    dban.Titularidad = Util.AtribuirValorObj(rowDatoBancario("TITULARIDAD"), GetType(String))
                    dban.Divisa = Util.AtribuirValorObj(rowDatoBancario("DIVISA"), GetType(String))
                    dban.Tipo = Util.AtribuirValorObj(rowDatoBancario("TIPO"), GetType(String))
                    dban.Observaciones = Util.AtribuirValorObj(rowDatoBancario("OBSERVACIONES"), GetType(String))
                    dban.DatosAdicionales = New DatosAdicionales
                    dban.DatosAdicionales.CampoAdicional1 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_1"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional2 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_2"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional3 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_3"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional4 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_4"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional5 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_5"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional6 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_6"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional7 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_7"), GetType(String))
                    dban.DatosAdicionales.CampoAdicional8 = Util.AtribuirValorObj(rowDatoBancario("DES_CAMPO_ADICIONAL_8"), GetType(String))
                    respuesta.Add(dban)
                Next
            End If

            Return respuesta
        End Function

        Private Shared Function PoblarDireccion(ds As DataSet, CodigoCalificador As String, IdentificadorTablaGenesis As String) As Direccion

            Dim direccion = New FechaValorOnline.Salida.Direccion()
            If ds.Tables.Contains("Direccion") AndAlso ds.Tables("Direccion").Rows.Count > 0 AndAlso ds.Tables("Direccion").Select("COD_CALIFICADOR = '" & CodigoCalificador & "' AND OID_TABLA_GENESIS = '" & IdentificadorTablaGenesis & "' ") IsNot Nothing Then

                For Each rowDireccion As DataRow In ds.Tables("Direccion").Select("COD_CALIFICADOR = '" & CodigoCalificador & "' AND OID_TABLA_GENESIS = '" & IdentificadorTablaGenesis & "' ")

                    direccion.Pais = Util.AtribuirValorObj(rowDireccion("DES_PAIS"), GetType(String))
                    direccion.ProvinciaEstado = Util.AtribuirValorObj(rowDireccion("DES_PROVINCIA"), GetType(String))
                    direccion.Telefono = Util.AtribuirValorObj(rowDireccion("DES_NUMERO_TELEFONO"), GetType(String))
                    direccion.Ciudad = Util.AtribuirValorObj(rowDireccion("DES_CIUDAD"), GetType(String))
                    direccion.NIF = Util.AtribuirValorObj(rowDireccion("COD_FISCAL"), GetType(String))
                    direccion.Email = Util.AtribuirValorObj(rowDireccion("DES_EMAIL"), GetType(String))
                    direccion.CodigoPostal = Util.AtribuirValorObj(rowDireccion("COD_POSTAL"), GetType(String))
                    direccion.Direccion1 = Util.AtribuirValorObj(rowDireccion("DES_DIRECCION_LINEA_1"), GetType(String))
                    direccion.Direccion2 = Util.AtribuirValorObj(rowDireccion("DES_DIRECCION_LINEA_2"), GetType(String))
                    direccion.DatosAdicionales = New FechaValorOnline.Salida.DatoAdicionalDireccion

                    direccion.DatosAdicionales.CampoAdicional1 = Util.AtribuirValorObj(rowDireccion("DES_CAMPO_ADICIONAL_1"), GetType(String))
                    direccion.DatosAdicionales.CampoAdicional2 = Util.AtribuirValorObj(rowDireccion("DES_CAMPO_ADICIONAL_2"), GetType(String))
                    direccion.DatosAdicionales.CampoAdicional3 = Util.AtribuirValorObj(rowDireccion("DES_CAMPO_ADICIONAL_3"), GetType(String))

                    direccion.DatosAdicionales.CategoriaAdicional1 = Util.AtribuirValorObj(rowDireccion("DES_CATEGORIA_ADICIONAL_1"), GetType(String))
                    direccion.DatosAdicionales.CategoriaAdicional2 = Util.AtribuirValorObj(rowDireccion("DES_CATEGORIA_ADICIONAL_2"), GetType(String))
                    direccion.DatosAdicionales.CategoriaAdicional3 = Util.AtribuirValorObj(rowDireccion("DES_CATEGORIA_ADICIONAL_3"), GetType(String))

                Next
            End If

            Return direccion
        End Function

#End Region


#Region "srecuperar_informaciones"

        ''' <summary>
        ''' No Esta totalmente desarrolado.
        ''' </summary>
        ''' <param name="peticion"></param>
        ''' <param name="log"></param>
        ''' <returns></returns>
        Public Shared Function RecuperarInformaciones(peticion As RecuperarInformaciones.Peticion,
                                                             ByRef log As StringBuilder) As List(Of RecuperarInformaciones.Salida.Movimiento)

            Dim TiempoParcial As DateTime
            Dim movimientos As New List(Of RecuperarInformaciones.Salida.Movimiento)

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = srecuperar_informaciones(peticion)
                log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                TiempoParcial = Now
                PoblarRecuperarInformaciones(ds, movimientos)
                log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                If movimientos IsNot Nothing AndAlso movimientos.Count > 0 Then
                    ' Valida si hubo algun error y cambia el Tipo de Resultado
                    For Each m In movimientos
                        If m.Detalles IsNot Nothing AndAlso m.Detalles.FirstOrDefault(Function(x) x.Codigo.Substring(0, 1) <> "0") IsNot Nothing Then
                            m.TipoResultado = "2"
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

            Return movimientos

        End Function

        Private Shared Function srecuperar_informaciones(peticion As RecuperarInformaciones.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PMOVIMIENTO_{0}.srecuperar_informaciones", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)


            spw.AgregarParam("par$movimientos", ProsegurDbType.Descricao_Longa, Nothing, , True)

            spw.AgregarParam("par$tipo_movimiento", ProsegurDbType.Descricao_Curta, Nothing, , True)
            If peticion IsNot Nothing AndAlso peticion.Movimientos IsNot Nothing Then

            End If
            For Each movi In peticion.Movimientos
                If movi IsNot Nothing Then
                    spw.Param("par$movimientos").AgregarValorArray(movi.Valor)
                    spw.Param("par$tipo_movimiento").AgregarValorArray(movi.Tipo)


                End If
            Next


            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Longa, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)


            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)

            spw.AgregarParam("par$rc_movimientos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "movimientos")
            spw.AgregarParam("par$rc_codigos_ajenos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ajeno")
            spw.AgregarParam("par$rc_periodos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "periodo")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")


            Return spw

        End Function

        Private Shared Sub PoblarRecuperarInformaciones(ds As DataSet, ByRef movimientos As List(Of RecuperarInformaciones.Salida.Movimiento))

            If movimientos Is Nothing Then movimientos = New List(Of RecuperarInformaciones.Salida.Movimiento)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 Then

                    For Each row As DataRow In ds.Tables("movimientos").Rows


                        Dim movimiento = movimientos.FirstOrDefault(Function(x) x.CodigoExterno = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String)))

                        If movimiento Is Nothing Then
                            movimiento = New RecuperarInformaciones.Salida.Movimiento
                            movimiento.CodigoExterno = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))
                            movimiento.CodigoDelegacion = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                            movimiento.CodigoPlanta = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                            movimiento.CodigoSector = Util.AtribuirValorObj(row("COD_SECTOR"), GetType(String))
                            movimiento.TipoResultado = "0"

                            movimiento.HayPeriodos = False
                            For Each rowPeriodo As DataRow In ds.Tables("periodo").Select("OID_DOCUMENTO = '" & row("OID_DOCUMENTO") & "' ")

                                If Util.AtribuirValorObj(rowPeriodo("OID_PERIODO"), GetType(String)).ToString().Trim() <> "-" Then
                                    movimiento.HayPeriodos = True
                                Else
                                    If Util.AtribuirValorObj(rowPeriodo("OID_PLANIFICACION"), GetType(String)).ToString().Trim() = "-" Then
                                        movimiento.HayPeriodos = True

                                    End If

                                End If

                            Next

                            movimientos.Add(movimiento)
                        End If

                    Next

                End If

            End If

        End Sub

#End Region
    End Class

End Namespace

