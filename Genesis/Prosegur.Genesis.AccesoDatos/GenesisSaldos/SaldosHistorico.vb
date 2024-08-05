Imports System.Globalization
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.ContractoServicio.Contractos.GenesisSaldos.Saldo
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports System.Linq
Imports Prosegur.Genesis.ContractoServicio

Namespace GenesisSaldos
    Public Class SaldosHistorico

        Public Shared Sub Recuperar(identificadorLlamada As String,
                                   peticion As RecuperarSaldosHistorico.Peticion,
                            ByRef respuesta As RecuperarSaldosHistorico.Respuesta,
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

        Private Shared Sub PoblarRespuesta(ds As DataSet, ByRef respuesta As RecuperarSaldosHistorico.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                If ds.Tables.Contains("cuentas") AndAlso ds.Tables("cuentas").Rows.Count > 0 Then
                    Dim listaMaquinas As New List(Of RecuperarSaldosHistorico.Maquina)

                    For Each unaFila In ds.Tables("cuentas").Select("", "OID_MAQUINA, OID_CLIENTE, OID_SUBCLIENTE, OID_PTO_SERVICIO, OID_CANAL, OID_SUBCANAL")
                        Dim maquina As New RecuperarSaldosHistorico.Maquina

                        Util.AtribuirValorObjeto(maquina.Identificador, unaFila("OID_MAQUINA"), GetType(String))
                        Util.AtribuirValorObjeto(maquina.DeviceID, unaFila("COD_MAQUINA"), GetType(String))
                        Util.AtribuirValorObjeto(maquina.Descripcion, unaFila("DES_MAQUINA"), GetType(String))

                        'Busco si ya existe la máquina en la lista de respuesta
                        Dim maquinaExistente = listaMaquinas.FirstOrDefault(Function(x) x.Identificador = maquina.Identificador)

                        'Si no existe la agrego a la lista
                        If maquinaExistente Is Nothing Then
                            listaMaquinas.Add(maquina)
                            maquinaExistente = maquina
                        End If

                        'Cargo los datos para el SALDO
                        Dim saldo As New RecuperarSaldosHistorico.Saldo
                        Util.AtribuirValorObjeto(saldo.Cliente.Identificador, unaFila("OID_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.Cliente.Codigo, unaFila("COD_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.Cliente.Descripcion, unaFila("DES_CLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.SubCliente.Identificador, unaFila("OID_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.SubCliente.Codigo, unaFila("COD_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.SubCliente.Descripcion, unaFila("DES_SUBCLIENTE"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.PuntoServicio.Identificador, unaFila("OID_PTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.PuntoServicio.Codigo, unaFila("COD_PTO_SERVICIO"), GetType(String))
                        Util.AtribuirValorObjeto(saldo.PuntoServicio.Descripcion, unaFila("DES_PTO_SERVICIO"), GetType(String))

                        'Busco si ya existe el saldo (cliente, subcliente y punto) para la maquina
                        Dim saldoExistente = maquinaExistente.Saldos.FirstOrDefault(Function(x) x.Cliente.Identificador = saldo.Cliente.Identificador AndAlso
                                                                   x.SubCliente.Identificador = saldo.SubCliente.Identificador AndAlso
                                                                   x.PuntoServicio.Identificador = saldo.PuntoServicio.Identificador)

                        'Si no existe la agrego a la lista de saldo
                        If saldoExistente Is Nothing Then
                            maquinaExistente.Saldos.Add(saldo)
                            saldoExistente = saldo
                        End If

                        'Cargo los datos para el CANAL
                        Dim canal As New RecuperarSaldosHistorico.Canal
                        Util.AtribuirValorObjeto(canal.Identificador, unaFila("OID_CANAL"), GetType(String))
                        Util.AtribuirValorObjeto(canal.Codigo, unaFila("COD_CANAL"), GetType(String))
                        Util.AtribuirValorObjeto(canal.Descripcion, unaFila("DES_CANAL"), GetType(String))

                        'Busco si ya existe el canal
                        Dim canalExistente = saldoExistente.Canales.FirstOrDefault(Function(x) x.Identificador = canal.Identificador)

                        'Si no existe la agrego a la lista de canales
                        If canalExistente Is Nothing Then
                            saldoExistente.Canales.Add(canal)
                            canalExistente = canal
                        End If

                        'Cargo los datos para el SUBCANAL
                        Dim subCanal As New RecuperarSaldosHistorico.SubCanal
                        Util.AtribuirValorObjeto(subCanal.OidCuenta, unaFila("OID_CUENTA"), GetType(String))
                        Util.AtribuirValorObjeto(subCanal.Identificador, unaFila("OID_SUBCANAL"), GetType(String))
                        Util.AtribuirValorObjeto(subCanal.Codigo, unaFila("COD_SUBCANAL"), GetType(String))
                        Util.AtribuirValorObjeto(subCanal.Descripcion, unaFila("DES_SUBCANAL"), GetType(String))

                        'Busco si ya existe el subcanal
                        Dim subCanalExistente = canalExistente.SubCanales.FirstOrDefault(Function(x) x.Identificador = subCanal.Identificador)

                        'Si no existe la agrego a la lista de subcanales
                        If subCanalExistente Is Nothing Then
                            canalExistente.SubCanales.Add(subCanal)
                            subCanalExistente = subCanal
                        End If

                        'Recorro las divisas
                        If ds.Tables.Contains("divisas") AndAlso ds.Tables("divisas").Rows.Count > 0 Then
                            For Each filaDivisa In ds.Tables("divisas").Select($"OID_CUENTA = '{subCanalExistente.OidCuenta}'", "OID_DIVISA")
                                'Cargo los datos para la DIVISA
                                Dim divisa = New RecuperarSaldosHistorico.Divisa
                                Util.AtribuirValorObjeto(divisa.Identificador, filaDivisa("OID_DIVISA"), GetType(String))
                                Util.AtribuirValorObjeto(divisa.Codigo, filaDivisa("COD_ISO_DIVISA"), GetType(String))
                                Util.AtribuirValorObjeto(divisa.Descripcion, filaDivisa("DES_DIVISA"), GetType(String))
                                Util.AtribuirValorObjeto(divisa.Importe, filaDivisa("NUM_IMPORTE"), GetType(Decimal))
                                Util.AtribuirValorObjeto(divisa.Disponible, filaDivisa("BOL_DISPONIBLE"), GetType(Boolean))
                                Util.AtribuirValorObjeto(divisa.ImporteDia, filaDivisa("NUM_IMPORTE_DIA"), GetType(Decimal))
                                Util.AtribuirValorObjeto(divisa.ImporteAnterior, filaDivisa("NUM_IMPORTE_ANTERIOR"), GetType(Decimal))

                                'Busco si ya existe la divisa
                                Dim divisaExistente = subCanalExistente.Divisas.FirstOrDefault(Function(x) x.Identificador = divisa.Identificador)

                                'Si no existe la agrego a la lista de divisas
                                If divisaExistente Is Nothing Then
                                    subCanalExistente.Divisas.Add(divisa)
                                    divisaExistente = divisa
                                End If

                                'Recorro las denominaciones
                                If ds.Tables.Contains("denominaciones") AndAlso ds.Tables("denominaciones").Rows.Count > 0 Then
                                    For Each filaDenominacion In ds.Tables("denominaciones").Select($"OID_CUENTA = '{subCanalExistente.OidCuenta}' AND OID_DIVISA = '{divisaExistente.Identificador}'", "OID_DENOMINACION")
                                        'Cargo los datos para la DENOMINACION



                                        If divisaExistente.Denominaciones Is Nothing Then divisaExistente.Denominaciones = New List(Of RecuperarSaldosHistorico.Denominacion)

                                        Dim denominacion = New RecuperarSaldosHistorico.Denominacion
                                        Util.AtribuirValorObjeto(denominacion.Identificador, filaDenominacion("OID_DENOMINACION"), GetType(String))
                                        Util.AtribuirValorObjeto(denominacion.Codigo, filaDenominacion("COD_DENOMINACION"), GetType(String))
                                        Util.AtribuirValorObjeto(denominacion.Descripcion, filaDenominacion("DES_DENOMINACION"), GetType(String))
                                        Util.AtribuirValorObjeto(denominacion.Cantidad, filaDenominacion("NEL_CANTIDAD"), GetType(Integer))
                                        Util.AtribuirValorObjeto(denominacion.Importe, filaDenominacion("NUM_IMPORTE"), GetType(Decimal))

                                        Util.AtribuirValorObjeto(denominacion.ImporteDia, filaDenominacion("NUM_IMPORTE_DIA"), GetType(Decimal))
                                        Util.AtribuirValorObjeto(denominacion.ImporteAnterior, filaDenominacion("NUM_IMPORTE_ANTERIOR"), GetType(Decimal))

                                        'Busco si ya existe la denominacion
                                        Dim denominacionExistente = divisaExistente.Denominaciones.FirstOrDefault(Function(x) x.Identificador = denominacion.Identificador)

                                        'Si no existe la agrego a la lista de divisas
                                        If denominacionExistente Is Nothing Then
                                            divisaExistente.Denominaciones.Add(denominacion)
                                            denominacionExistente = denominacion
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next

                    respuesta.Maquinas = listaMaquinas
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
                       Prosegur.Genesis.Comon.Enumeradores.Mensajes.Funcionalidad.RecuperarSaldosHistorico,
                       "0000", "", True)

                    respuesta.Resultado.Detalles.Add(unDetalle)
                End If

            End If
        End Sub

        Private Shared Function ArmarWrapper(identificadorLlamada As String, peticion As RecuperarSaldosHistorico.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecuperar_saldos_historico", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

#Region "Parametros de entrada"
            spw.AgregarParam("par$oid_llamada", ProsegurDbType.Identificador_Alfanumerico, identificadorLlamada, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            spw.AgregarParam("par$cod_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPais, , False)

            spw.AgregarParam("par$fecha", ProsegurDbType.Identificador_Alfanumerico, peticion.Fecha.Value.ToString("dd/MM/yyyy HH:mm:ss zzz"), , False)

            If peticion.Opciones IsNot Nothing Then
                If String.IsNullOrEmpty(peticion.Opciones.ValoresDetallar) Then
                    spw.AgregarParam("par$bol_valor_detallar", ProsegurDbType.Logico, 0, , False)
                ElseIf peticion.Opciones.ValoresDetallar = "1" OrElse peticion.Opciones.ValoresDetallar.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_valor_detallar", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_valor_detallar", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.Opciones.DeviceIDCompleto) Then
                    spw.AgregarParam("par$bol_device_id_completo", ProsegurDbType.Logico, 0, , False)
                ElseIf peticion.Opciones.DeviceIDCompleto = "1" OrElse peticion.Opciones.DeviceIDCompleto.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_device_id_completo", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_device_id_completo", ProsegurDbType.Logico, 0, , False)
                End If

            Else
                spw.AgregarParam("par$bol_valor_detallar", ProsegurDbType.Logico, 0, , False)
                spw.AgregarParam("par$bol_device_id_completo", ProsegurDbType.Logico, 0, , False)
            End If


            spw.AgregarParam("par$cod_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente, , False)
            spw.AgregarParam("par$cod_sub_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubCliente, , False)
            spw.AgregarParam("par$cod_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPuntoServicio, , False)

            spw.AgregarParam("par$acod_device_id", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.DeviceIDs IsNot Nothing AndAlso peticion.DeviceIDs.Count > 0 Then
                spw.Param("par$acod_device_id").AgregarValorArray(String.Empty)
                For Each item In peticion.DeviceIDs.Distinct
                    If Not String.IsNullOrEmpty(item) Then
                        spw.Param("par$acod_device_id").AgregarValorArray(item)
                    End If
                Next
            End If

            spw.AgregarParam("par$acod_canal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.CodigosCanales IsNot Nothing AndAlso peticion.CodigosCanales.Count > 0 Then
                spw.Param("par$acod_canal").AgregarValorArray(String.Empty)
                For Each item In peticion.CodigosCanales.Distinct
                    If Not String.IsNullOrEmpty(item) Then
                        spw.Param("par$acod_canal").AgregarValorArray(item)
                    End If
                Next
            End If

            spw.AgregarParam("par$acod_subcanal", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.CodigosSubCanales IsNot Nothing AndAlso peticion.CodigosSubCanales.Count > 0 Then
                spw.Param("par$acod_subcanal").AgregarValorArray(String.Empty)
                For Each item In peticion.CodigosSubCanales.Distinct
                    If Not String.IsNullOrEmpty(item) Then
                        spw.Param("par$acod_subcanal").AgregarValorArray(item)
                    End If
                Next
            End If

            spw.AgregarParam("par$acod_divisa", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.CodigosDivisas IsNot Nothing AndAlso peticion.CodigosDivisas.Count > 0 Then
                spw.Param("par$acod_divisa").AgregarValorArray(String.Empty)
                For Each item In peticion.CodigosDivisas.Distinct
                    If Not String.IsNullOrEmpty(item) Then
                        spw.Param("par$acod_divisa").AgregarValorArray(item)
                    End If
                Next
            End If

            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())
            spw.AgregarParamInfo("par$info_ejecucion")

#End Region
#Region "Parametros de salida"
            'spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)
            spw.AgregarParam("par$rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cuentas")
            spw.AgregarParam("par$rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "divisas")
            spw.AgregarParam("par$rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "denominaciones")
            spw.AgregarParam("par$rc_validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
#End Region
            Return spw

        End Function


        Public Shared Sub BorrarSaldosHistoricoCliente(peticion As BorrarSaldosHistoricoCliente.Peticion,
                                                        Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ColectarPeticionBorrarSaldosHistoricoCliente(peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now

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

        Private Shared Function ColectarPeticionBorrarSaldosHistoricoCliente(peticion As BorrarSaldosHistoricoCliente.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.sborrar_saldos_hist_cli", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorCliente, , False)
            spw.AgregarParam("par$oid_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorPais, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())

            Return spw

        End Function

        Public Shared Sub ActualizarSaldosHistoricoCliente(peticion As ActualizarSaldosHistoricoCliente.Peticion,
                            Optional ByRef log As StringBuilder = Nothing)
            Try
                If log Is Nothing Then log = New StringBuilder
                Dim TiempoParcial As DateTime = Now

                Dim ds As DataSet = Nothing
                Dim spw As SPWrapper = Nothing

                TiempoParcial = Now
                spw = ArmarWrapperActualizarSaldosHistoricoCliente(peticion)
                log.AppendLine("Tiempo de acceso a datos (Parametros para procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now
                ds = AccesoDB.EjecutarSP(spw, Constantes.CONEXAO_GENESIS, False, Nothing)
                log.AppendLine("Tiempo de acceso a datos (Ejecutar procedure): " & Now.Subtract(TiempoParcial).ToString() & "; ")

                TiempoParcial = Now

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

        Private Shared Function ArmarWrapperActualizarSaldosHistoricoCliente(peticion As ActualizarSaldosHistoricoCliente.Peticion) As SPWrapper
            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.sactualizar_saldos_hist_cli", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParam("par$oid_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorCliente, , False)
            spw.AgregarParam("par$oid_pais", ProsegurDbType.Identificador_Alfanumerico, peticion.IdentificadorPais, , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Usuario, , False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, Util.GetCultureUser())

            Return spw

        End Function
    End Class
End Namespace

