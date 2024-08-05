Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.IntegracionSistemas.Reintentos.Clases
Imports System.Linq

Namespace GenesisSaldos.Integracion
    Public Class FechaValorOnline
        Public Shared Function GetMovimientos(ByRef peticion As Reintentos.Peticion) As Reintentos.Respuesta
            Dim ds As DataSet = Nothing
            Dim spw As SPWrapper = Nothing
            Dim respuesta As New Reintentos.Respuesta

            Try

                spw = buildWrapper(peticion)

                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta = PoblarRespuesta(ds)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try
            Return respuesta
        End Function

        Private Shared Function PoblarRespuesta(ds As DataSet) As Reintentos.Respuesta
            Dim respuesta As New Reintentos.Respuesta
            Dim unActualID As Reintentos.Clases.MovimientoActualID
            Dim index As Integer
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("actualids") Then
                    For Each fila In ds.Tables("actualids").Rows
                        unActualID = New Reintentos.Clases.MovimientoActualID()

                        unActualID.ActualID = Util.AtribuirValorObj(fila("COD_ACTUAL_ID"), GetType(String))
                        unActualID.CodProceso = Util.AtribuirValorObj(fila("COD_PROCESO"), GetType(String))
                        unActualID.Reintentos = Util.AtribuirValorObj(fila("REINTENTOS"), GetType(String))
                        unActualID.CodEstado = Util.AtribuirValorObj(fila("COD_ESTADO"), GetType(String))

                        unActualID.ID = index
                        If ds.Tables.Contains("det_actualids") Then
                            Dim objDocumento As Reintentos.Clases.Documento
                            For Each rowDetalle In ds.Tables("det_actualids").Select(String.Format(" COD_ACTUAL_ID = '{0}'", unActualID.ActualID))
                                objDocumento = New Reintentos.Clases.Documento()

                                objDocumento.CodCanal = Util.AtribuirValorObj(rowDetalle("COD_CANAL"), GetType(String))
                                objDocumento.DesCanal = Util.AtribuirValorObj(rowDetalle("DES_CANAL"), GetType(String))

                                objDocumento.CodSubCanal = Util.AtribuirValorObj(rowDetalle("COD_SUBCANAL"), GetType(String))
                                objDocumento.DesSubCanal = Util.AtribuirValorObj(rowDetalle("DES_SUBCANAL"), GetType(String))

                                objDocumento.FechaGestion = Util.AtribuirValorObj(rowDetalle("FYH_GESTION"), GetType(DateTime))
                                objDocumento.CodExterno = Util.AtribuirValorObj(rowDetalle("COD_EXTERNO"), GetType(String))

                                objDocumento.Formulario.CodFormulario = Util.AtribuirValorObj(rowDetalle("COD_FORMULARIO"), GetType(String))
                                objDocumento.Formulario.DesFormulario = Util.AtribuirValorObj(rowDetalle("DES_FORMULARIO"), GetType(String))

                                unActualID.Maquina.DeviceID = Util.AtribuirValorObj(rowDetalle("COD_MAQUINA"), GetType(String))
                                unActualID.Maquina.Descripcion = Util.AtribuirValorObj(rowDetalle("DES_MAQUINA"), GetType(String))

                                unActualID.Cliente.CodCliente = Util.AtribuirValorObj(rowDetalle("COD_CLIENTE"), GetType(String))
                                unActualID.Cliente.DesCliente = Util.AtribuirValorObj(rowDetalle("DES_CLIENTE"), GetType(String))

                                unActualID.SubCliente.CodSubCliente = Util.AtribuirValorObj(rowDetalle("COD_SUBCLIENTE"), GetType(String))
                                unActualID.SubCliente.DesSubCliente = Util.AtribuirValorObj(rowDetalle("DES_SUBCLIENTE"), GetType(String))

                                unActualID.PuntoServicio.CodPuntoServicio = Util.AtribuirValorObj(rowDetalle("COD_PTO_SERVICIO"), GetType(String))
                                unActualID.PuntoServicio.DesPuntoServicio = Util.AtribuirValorObj(rowDetalle("DES_PTO_SERVICIO"), GetType(String))

                                unActualID.Documentos.Add(objDocumento)
                            Next
                        End If

                        If ds.Tables.Contains("det_integracion") Then
                            Dim objIntegracion As Reintentos.Clases.DetalleIntegracion
                            For Each rowDetalle In ds.Tables("det_integracion").Select(String.Format(" COD_ACTUAL_ID = '{0}'", unActualID.ActualID), "FECHA DESC")
                                objIntegracion = New Reintentos.Clases.DetalleIntegracion()


                                objIntegracion.Comentario = Util.AtribuirValorObj(rowDetalle("COMENTARIO"), GetType(String))
                                objIntegracion.NumeroIntento = Util.AtribuirValorObj(rowDetalle("NRO_REINTENTOS"), GetType(Integer))
                                objIntegracion.Fecha = Util.AtribuirValorObj(rowDetalle("FECHA"), GetType(DateTime))

                                objIntegracion.TipoDeError = Util.AtribuirValorObj(rowDetalle("TIPO_ERROR"), GetType(String))

                                unActualID.DetallesIntegracion.Add(objIntegracion)
                            Next
                        End If
                        unActualID.TipoError = unActualID.DetallesIntegracion.OrderByDescending(Function(x) x.Fecha).FirstOrDefault().TipoDeError
                        respuesta.Movimientos.Add(unActualID)
                        index += 1
                    Next
                End If
            End If

            Return respuesta
        End Function

        Public Shared Function ValidarIntegracion(actualIds As List(Of String), usuario As String) As List(Of String)
            Dim ds As DataSet = Nothing
            Dim spw As SPWrapper = Nothing
            Dim respuesta As New List(Of String)

            Try

                spw = buildWrapperValidacion(actualIds, usuario)

                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)

                respuesta = PoblarRespuestaValidacion(ds)

            Catch ex As Exception
                Dim MsgErroTratado As String = Util.RecuperarMensagemTratada(ex)

                If Not String.IsNullOrEmpty(MsgErroTratado) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, MsgErroTratado)
                Else
                    Throw ex
                End If
            End Try

            Return respuesta
        End Function

        Private Shared Function PoblarRespuestaValidacion(ds As DataSet) As List(Of String)
            Dim resultado = New List(Of String)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("ActualIdsValidados") Then
                    For Each row As DataRow In ds.Tables("ActualIdsValidados").Rows
                        resultado.Add(row.Field(Of String)("COD_ACTUAL_ID"))
                    Next
                End If
            End If
            Return resultado
        End Function

        Private Shared Function buildWrapper(peticion As Reintentos.Peticion) As SPWrapper
            Dim SP As String = String.Format("GEPR_PINTEGRACION_{0}.srecuperar_movs_pendientes", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As SPWrapper = New SPWrapper(SP, False)
            Try

                If peticion.Codigo Is Nothing OrElse String.IsNullOrEmpty(peticion.Codigo) Then
                    spw.AgregarParam("par$codigo", ProsegurDbType.Descricao_Curta, DBNull.Value, ParameterDirection.Input, False)
                    spw.AgregarParam("par$tipo_codigo", ProsegurDbType.Descricao_Curta, DBNull.Value, ParameterDirection.Input, False)
                Else
                    spw.AgregarParam("par$codigo", ProsegurDbType.Descricao_Curta, peticion.Codigo, ParameterDirection.Input, False)
                    spw.AgregarParam("par$tipo_codigo", ProsegurDbType.Descricao_Curta, CType(peticion.TipoCodigo, Int16), ParameterDirection.Input, False)
                End If

                spw.AgregarParam("par$cod_device_id", ProsegurDbType.Descricao_Longa, peticion.DeviceID, ParameterDirection.Input, False)

                If peticion.IdentificadorCliente Is Nothing OrElse String.IsNullOrEmpty(peticion.IdentificadorCliente) Then
                    spw.AgregarParam("par$oid_cliente", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value, ParameterDirection.Input, False)
                Else
                    spw.AgregarParam("par$oid_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorCliente, ParameterDirection.Input, False)
                End If


                If peticion.IdentificadorSubCliente Is Nothing OrElse String.IsNullOrEmpty(peticion.IdentificadorSubCliente) Then
                    spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value, ParameterDirection.Input, False)
                Else
                    spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorSubCliente, ParameterDirection.Input, False)
                End If

                If peticion.IdentificadorPuntoServicio Is Nothing OrElse String.IsNullOrEmpty(peticion.IdentificadorPuntoServicio) Then
                    spw.AgregarParam("par$oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, DBNull.Value, ParameterDirection.Input, False)
                Else
                    spw.AgregarParam("par$oid_pto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorPuntoServicio, ParameterDirection.Input, False)
                End If

                spw.AgregarParam("par$aestado", ProsegurDbType.Inteiro_Curto, Nothing, , True)

                spw.Param("par$aestado").AgregarValorArray(DBNull.Value)
                If peticion.Estados IsNot Nothing AndAlso peticion.Estados.Count > 0 Then
                    For Each estado In peticion.Estados
                        spw.Param("par$aestado").AgregarValorArray(CType(estado, Int16))
                    Next
                End If

                spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, peticion.CodUsuario, , False)
                spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
                If peticion.CodProceso IsNot Nothing Then
                    spw.AgregarParam("par$cod_proceso", ProsegurDbType.Descricao_Longa, peticion.CodProceso, , False)
                Else
                    spw.AgregarParam("par$cod_proceso", ProsegurDbType.Descricao_Longa, DBNull.Value, , False)
                End If

                If peticion.MensajeError IsNot Nothing Then
                    spw.AgregarParam("par$msg_error", ProsegurDbType.Descricao_Longa, peticion.MensajeError, , False)
                Else
                    spw.AgregarParam("par$msg_error", ProsegurDbType.Descricao_Longa, DBNull.Value, , False)
                End If

                spw.AgregarParam("par$rc_actualid", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "actualids")
                spw.AgregarParam("par$rc_actual_id_det", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "det_actualids")
                spw.AgregarParam("par$rc_det_integracion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "det_integracion")

            Catch ex As Exception
                Throw ex
            End Try

            Return spw
        End Function

        Private Shared Function buildWrapperValidacion(actualIds As List(Of String), usuario As String) As SPWrapper
            Dim SP As String = String.Format("GEPR_PINTEGRACION_{0}.svalida_integracion", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As SPWrapper = New SPWrapper(SP, False)
            spw.AgregarParam("par$acod_actual_id", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Descricao_Longa, usuario, , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Inteiro_Longo, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_resultado", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "ActualIdsValidados")
            For Each actualId As String In actualIds
                spw.Param("par$acod_actual_id").AgregarValorArray(actualId)
            Next
            Return spw
        End Function


    End Class
End Namespace

