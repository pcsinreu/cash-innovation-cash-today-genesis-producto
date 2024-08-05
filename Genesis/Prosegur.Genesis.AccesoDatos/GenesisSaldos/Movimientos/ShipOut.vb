﻿Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.AltaMovimientosShipOut
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.AltaMovimientosShipOut.Salida
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace GenesisSaldos.Movimientos
    Public Class ShipOut

        Public Shared Sub AltaMovimientosShipOut(identificadorLlamada As String, peticion As Peticion, respuesta As Respuesta,
                                                ByRef log As StringBuilder)

            Dim TiempoParcial As DateTime

            Try

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                If peticion.Movimientos IsNot Nothing AndAlso peticion.Movimientos.Count > 0 Then

                    TiempoParcial = Now
                    Dim movimientosIndex As New Dictionary(Of Integer, Entrada.MovimientoShipOut)
                    spw = sconfigurarMovimientosShipOut(identificadorLlamada, peticion, movimientosIndex)
                    log.AppendLine("AccesoDatos - Tiempo rellenar parametros: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                    log.AppendLine("AccesoDatos - Tiempo ejecutar procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

                    TiempoParcial = Now
                    PoblarMovimientosShipOut(ds, respuesta, movimientosIndex)
                    log.AppendLine("AccesoDatos - Tiempo de tratar respuesta de la procedure: " & Now.Subtract(TiempoParcial).ToString() & ";")

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
        Private Shared Function sconfigurarMovimientosShipOut(identificadorLlamada As String, peticion As Peticion, ByRef movimientosIndex As Dictionary(Of Integer, Entrada.MovimientoShipOut)) As SPWrapper

            Dim SP As String = String.Format("SAPR_PSERVICIO_{0}.sgrabar_mov_shipout", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Descricao_Curta, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$anel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$afyh_contable", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$afyh_gestion", ProsegurDbType.Data_Hora, Nothing, , True)
            spw.AgregarParam("par$avtdoc_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$avtdoc_cod_termino", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$avtdoc_des_valor", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acndoc_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$acndoc_det_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$acndoc_cod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acndoc_cod_sub_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aefdoc_det_index", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_sub_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_cod_denominacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aefdoc_nel_cantidad", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$aefdoc_num_importe", ProsegurDbType.Numero_Decimal, Nothing, , True)
            spw.AgregarParam("par$anel_gmt_minuto", ProsegurDbType.Inteiro_Curto, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, Util.GetCultureUser())
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Descricao_Curta, peticion.CodigoPais)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$rc_planificaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "planificaciones")
            spw.AgregarParam("par$rc_documentos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "documentos")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$acod_actual_id", ProsegurDbType.Descricao_Longa, Nothing, , True)
            spw.AgregarParam("par$acndoc_collection_id", ProsegurDbType.Descricao_Longa, Nothing, , True)


            Dim index As Integer = 0

            For Each mov In peticion.Movimientos

                If movimientosIndex Is Nothing Then movimientosIndex = New Dictionary(Of Integer, Entrada.MovimientoShipOut)
                movimientosIndex.Add(index, mov)
                spw.Param("par$anel_index").AgregarValorArray(index)
                spw.Param("par$acod_device_id").AgregarValorArray(mov.DeviceID)
                spw.Param("par$afyh_gestion").AgregarValorArray(mov.FechaHora)
                spw.Param("par$acod_actual_id").AgregarValorArray(mov.ActualId)

                If mov.FechaContable IsNot Nothing AndAlso mov.FechaContable.HasValue Then
                    spw.Param("par$afyh_contable").AgregarValorArray(mov.FechaContable.Value)
                Else
                    spw.Param("par$afyh_contable").AgregarValorArray(DBNull.Value)
                End If

                If mov.FechaHora <> DateTime.MinValue AndAlso Not String.IsNullOrEmpty(mov.FechaHora.ToString("%K")) Then

                    Dim GMTHoraLocalCalculado = Convert.ToInt32(mov.FechaHora.ToString("zzz").Split(":")(0))
                    Dim GMTMinutoLocalCalculado = Convert.ToInt32(mov.FechaHora.ToString("zzz").Split(":")(1))
                    GMTMinutoLocalCalculado += GMTHoraLocalCalculado * 60
                    spw.Param("par$anel_gmt_minuto").AgregarValorArray(GMTMinutoLocalCalculado)

                Else
                    spw.Param("par$anel_gmt_minuto").AgregarValorArray(DBNull.Value)
                End If

                If mov.CamposExtras IsNot Nothing Then
                    For Each campoExtra In mov.CamposExtras
                        If Not String.IsNullOrEmpty(campoExtra.Codigo) Then
                            spw.Param("par$avtdoc_nel_index").AgregarValorArray(index)
                            spw.Param("par$avtdoc_cod_termino").AgregarValorArray(campoExtra.Codigo)
                            spw.Param("par$avtdoc_des_valor").AgregarValorArray(campoExtra.Valor)
                        End If
                    Next
                End If

                If mov.Detalles IsNot Nothing Then
                    Dim indexDet = 0
                    For Each det In mov.Detalles
                        If det.Importes IsNot Nothing Then
                            spw.Param("par$acndoc_nel_index").AgregarValorArray(index)
                            spw.Param("par$acndoc_det_index").AgregarValorArray(indexDet)
                            spw.Param("par$acndoc_cod_canal").AgregarValorArray(det.CodigoCanal)
                            spw.Param("par$acndoc_cod_sub_canal").AgregarValorArray(det.CodigoSubCanal)
                            spw.Param("par$acndoc_collection_id").AgregarValorArray(det.CollectionId)

                            For Each importe In det.Importes
                                spw.Param("par$aefdoc_nel_index").AgregarValorArray(index)
                                spw.Param("par$aefdoc_det_index").AgregarValorArray(indexDet)
                                spw.Param("par$aefdoc_cod_sub_canal").AgregarValorArray(det.CodigoSubCanal)
                                spw.Param("par$aefdoc_cod_divisa").AgregarValorArray(importe.CodigoDivisa)
                                spw.Param("par$aefdoc_cod_denominacion").AgregarValorArray(importe.CodigoDenominacion)
                                spw.Param("par$aefdoc_nel_cantidad").AgregarValorArray(importe.Cantidad)
                                spw.Param("par$aefdoc_num_importe").AgregarValorArray(importe.Importe)
                            Next
                        End If
                        indexDet += 1
                    Next
                End If
                index = index + 1

            Next

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function
        Private Shared Sub PoblarMovimientosShipOut(ds As DataSet, respuesta As Respuesta, movimientosIndex As Dictionary(Of Integer, Entrada.MovimientoShipOut))

            If respuesta.Movimientos Is Nothing Then respuesta.Movimientos = New List(Of MovimientoShipOut)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then
                    Dim index As Integer = -1

                    ds.Tables("validaciones").DefaultView.Sort = "NEL_INDEX ASC"

                    For Each row As DataRow In ds.Tables("validaciones").DefaultView.ToTable().Rows

                        Dim rowIndex = Util.AtribuirValorObj(row("NEL_INDEX"), GetType(Integer))
                        If Not (rowIndex = 2040010027 Or rowIndex = 2040010026) Then
                            If rowIndex <> index AndAlso movimientosIndex IsNot Nothing AndAlso movimientosIndex.ContainsKey(rowIndex) Then
                                index = rowIndex

                                Dim Movimiento = New MovimientoShipOut
                                Movimiento.DeviceID = movimientosIndex(rowIndex).DeviceID
                                Movimiento.FechaHora = movimientosIndex(rowIndex).FechaHora
                                Movimiento.FechaContable = movimientosIndex(rowIndex).FechaContable
                                Movimiento.ActualId = movimientosIndex(rowIndex).ActualId
                                Movimiento.CollectionId = ObtenerCollectionIDs(movimientosIndex(rowIndex).Detalles)
                                Movimiento.TipoResultado = "0"

                                If ds.Tables.Contains("planificaciones") AndAlso ds.Tables("planificaciones").Rows.Count > 0 Then
                                    For Each rPlanificacion As DataRow In ds.Tables("planificaciones").DefaultView.ToTable().Select(String.Format("NEL_INDEX = {0}", rowIndex))
                                        Movimiento.CodigoBancoCapital = Util.AtribuirValorObj(rPlanificacion("CODIGO_BANCO"), GetType(String))
                                        Movimiento.CodigoPlanificacion = Util.AtribuirValorObj(rPlanificacion("CODIGO_PLANIFICACION"), GetType(String))
                                    Next
                                End If

                                If ds.Tables.Contains("documentos") AndAlso ds.Tables("documentos").Rows.Count > 0 Then
                                    For Each rDocumento As DataRow In ds.Tables("documentos").DefaultView.ToTable().Select(String.Format("NEL_INDEX = {0}", rowIndex))
                                        If Movimiento.Documentos Is Nothing Then Movimiento.Documentos = New List(Of String)
                                        Movimiento.Documentos.Add(Util.AtribuirValorObj(rDocumento("CODIGO_EXTERNO"), GetType(String)))
                                    Next
                                End If

                                respuesta.Movimientos.Add(Movimiento)
                            End If

                            Dim mov = respuesta.Movimientos.Last()

                            If mov.Detalles Is Nothing Then mov.Detalles = New List(Of Detalle)

                            Dim detalle As New Detalle
                            detalle.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                            detalle.Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
                            If Not mov.Detalles.Any(Function(x) x.Codigo = detalle.Codigo) Then
                                mov.Detalles.Add(detalle)
                            End If
                        Else
                            'Validaciones de país
                            Dim codigo = Util.AtribuirValorObj(row("NEL_INDEX"), GetType(Integer))
                            Dim descripcion = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                            If respuesta.Resultado.Detalles Is Nothing Then
                                respuesta.Resultado.Detalles = New List(Of Salida.Detalle)
                            End If
                            If Not respuesta.Resultado.Detalles.Any(Function(x) x.Codigo = codigo) Then
                                respuesta.Resultado.Detalles.Add(New Salida.Detalle() With {.Codigo = codigo, .Descripcion = descripcion})
                            End If
                        End If
                    Next
                End If
            End If
        End Sub

        Private Shared Function ObtenerCollectionIDs(detalles As List(Of Entrada.Detalle)) As List(Of String)
            Dim listaRetorno As New List(Of String)
            If detalles IsNot Nothing Then
                For Each det In detalles
                    If Not String.IsNullOrWhiteSpace(det.CollectionId) AndAlso Not listaRetorno.Contains(det.CollectionId) Then
                        listaRetorno.Add(det.CollectionId)
                    End If
                Next
            End If

            Return listaRetorno
        End Function
    End Class

End Namespace
