Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion.ConfirmarPeriodos

Namespace GenesisSaldos
    Public Class ConfirmarPeriodos

        Public Shared Sub AcreditarFechaValorConfirmacion(identificadorLlamada As String,
                                       peticion As Peticion,
                                       ByRef respuesta As Respuesta,
                                       ByRef periodosNoConfirmados As List(Of PeriodoNoConfirmado),
                                       Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ArmarWrapper(identificadorLlamada, peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                PoblarRespuesta(ds, respuesta, periodosNoConfirmados)

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

        Private Shared Function ArmarWrapper(identificadorLlamada As String, peticion As ContractoServicio.Contractos.Integracion.ConfirmarPeriodos.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSERVICIO_{0}.sconfirmar_periodos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

#Region "Parametros de entrada"

            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada)

            'Arrays
            spw.AgregarParam("par$adevice_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aidentificador", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$acodigo", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$aconfirmacion", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$afecha_hora", ProsegurDbType.Data_Hora, Nothing, , True)


            If peticion.Periodos IsNot Nothing AndAlso peticion.Periodos.Count > 0 Then
                For Each item In peticion.Periodos
                    spw.Param("par$adevice_id").AgregarValorArray(item.DeviceID.Trim())
                    spw.Param("par$aidentificador").AgregarValorArray(item.Identificador.Trim())
                    spw.Param("par$acodigo").AgregarValorArray(item.Codigo.Trim())
                    spw.Param("par$aconfirmacion").AgregarValorArray(item.Confirmacion.Trim())
                    spw.Param("par$afecha_hora").AgregarValorArray(item.FechaHora)
                Next
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)


#End Region
#Region "Parametros de salida"
            spw.AgregarParam("par$rc_errores_confirma", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "sinacreditar")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
#End Region
            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet, ByRef respuesta As Respuesta, ByRef periodosNoConfirmados As List(Of PeriodoNoConfirmado))

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String)), .Descripcion = Util.AtribuirValorObj(row("MENSAJE"), GetType(String))})
                    Next
                    If respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Aplicacion) Then
                        'Ver si hay error de aplicacion 
                        Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Aplicacion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)
                    ElseIf respuesta.Resultado.Detalles.Any(Function(x) x.Codigo.Substring(0, 1) = Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio) Then
                        'Ver si hay error de negocio
                        Util.resultado(respuesta.Resultado.Codigo,
                           respuesta.Resultado.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Error_Negocio,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)
                    End If
                Else
                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)

                    Dim unDetalle = New ContractoServicio.Contractos.Integracion.Comon.Detalle

                    Util.resultado(unDetalle.Codigo,
                           unDetalle.Descripcion,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Tipo.Exito,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Contexto.Integraciones,
                           Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.ConfirmarPeriodos,
                           "0000", "", True)

                    respuesta.Resultado.Detalles.Add(unDetalle)
                End If

                If ds.Tables.Contains("sinacreditar") AndAlso ds.Tables("sinacreditar").Rows.Count > 0 Then
                    Dim listaNoConfirmados As New List(Of PeriodoNoConfirmado)
                    For Each unaFila In ds.Tables("sinacreditar").Rows

                        Dim periodoNoConfirmado As New PeriodoNoConfirmado
                        Util.AtribuirValorObjeto(periodoNoConfirmado.OidPeriodo, unaFila("OID_PERIODO"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.DeviceId, unaFila("COD_IDENTIFICACION"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.MaquinaDesc, unaFila("DES_SECTOR"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.PeriodoIdentificador, unaFila("COD_PERIODO_CONFIRMACION"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.PeriodoCodigoMensaje, unaFila("COD_MENSAJE_CONFIRMACION"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.DescripcionMensaje, unaFila("DES_MENSAJE"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.ReintentosDisponibles, unaFila("REINTENTOS"), GetType(Double))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.EstadoPeriodo, unaFila("COD_CALIFICADOR"), GetType(String))
                        Util.AtribuirValorObjeto(periodoNoConfirmado.TipoPeriodo, unaFila("COD_TIPO_PERIODO"), GetType(String))

                        Dim periodoExistente = listaNoConfirmados.FirstOrDefault(Function(x) x.OidPeriodo = periodoNoConfirmado.OidPeriodo)

                        If periodoExistente IsNot Nothing Then
                            periodoExistente.ValoresAcreditados.Add(New Valor With {
                                .CodigoDivisa = unaFila("COD_ISO_DIVISA").ToString,
                                .Importe = Convert.ToDouble(unaFila("NUM_IMPORTE"))
                            })
                        Else
                            periodoNoConfirmado.ValoresAcreditados.Add(New Valor With {
                              .CodigoDivisa = unaFila("COD_ISO_DIVISA").ToString,
                              .Importe = Convert.ToDouble(unaFila("NUM_IMPORTE"))
                            })
                            listaNoConfirmados.Add(periodoNoConfirmado)
                        End If
                    Next

                    periodosNoConfirmados = listaNoConfirmados

                End If
            End If
        End Sub


    End Class
End Namespace