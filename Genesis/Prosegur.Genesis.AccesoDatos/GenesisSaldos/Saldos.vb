Imports System.Globalization
Imports System.Text
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper

Namespace GenesisSaldos
    Public Class Saldos
        Public Shared Sub Recuperar(peticion As RecuperarSaldos.Peticion,
                            ByRef respuesta As RecuperarSaldos.Respuesta,
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

                'If peticion.Paginacion IsNot Nothing Then
                '    respuesta.Paginacion = peticion.Paginacion
                '    If String.IsNullOrEmpty(peticion.Paginacion.RegistroPorPagina) Then
                '        respuesta.Paginacion.Indice = 0
                '    End If
                'Else
                '    respuesta.Paginacion.Indice = 0
                'End If


                PoblarRespuesta(ds, peticion, respuesta)

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

        Private Shared Function ColectarPeticion(peticion As RecuperarSaldos.Peticion) As SPWrapper

            Dim SP As String = String.Format("SAPR_PSALDOS_{0}.srecuperar_saldos", Prosegur.Genesis.Comon.Util.Version)
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

            If peticion.Opciones IsNot Nothing Then

                If String.IsNullOrEmpty(peticion.Opciones.TransaccionesDetallar) Then
                    spw.AgregarParam("par$bol_detallar_trans", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.TransaccionesDetallar = "1" OrElse peticion.Opciones.TransaccionesDetallar.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_detallar_trans", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_detallar_trans", ProsegurDbType.Logico, 0, , False)
                End If


                If String.IsNullOrEmpty(peticion.Opciones.ValoresDetallar) Then
                    spw.AgregarParam("par$bol_detallar_valores", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.ValoresDetallar = "1" OrElse peticion.Opciones.ValoresDetallar.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_detallar_valores", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_detallar_valores", ProsegurDbType.Logico, 0, , False)
                End If


                If String.IsNullOrEmpty(peticion.Opciones.BolsasDetallar) Then
                    spw.AgregarParam("par$bol_detallar_bolsas", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.BolsasDetallar = "1" OrElse peticion.Opciones.BolsasDetallar.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_detallar_bolsas", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_detallar_bolsas", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.Opciones.InfoAdicionales) Then
                    spw.AgregarParam("par$bol_info_adicional", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.InfoAdicionales = "1" OrElse peticion.Opciones.InfoAdicionales.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_info_adicional", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_info_adicional", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.Opciones.Disponible) Then
                    spw.AgregarParam("par$bol_disponible", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.Disponible = "1" OrElse peticion.Opciones.Disponible.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_disponible", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_disponible", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.Opciones.Acreditado) Then
                    spw.AgregarParam("par$bol_acreditado", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.Acreditado = "1" OrElse peticion.Opciones.Acreditado.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_acreditado", ProsegurDbType.Logico, 1, , False)
                Else
                    spw.AgregarParam("par$bol_acreditado", ProsegurDbType.Logico, 0, , False)
                End If

                If String.IsNullOrEmpty(peticion.Opciones.Notificado) Then
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, Nothing, , False)
                ElseIf peticion.Opciones.Notificado = "1" OrElse peticion.Opciones.Notificado.ToUpper = "TRUE" Then
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, 1, , False)

                Else
                    spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, 0, , False)
                End If



                spw.AgregarParam("par$bol_maqu_vigente", ProsegurDbType.Logico, 1, , False)
                'If String.IsNullOrEmpty(peticion.Opciones.MaquinasVigente) Then
                '    spw.AgregarParam("par$bol_maqu_vigente", ProsegurDbType.Logico, Nothing, , False)
                'ElseIf peticion.Opciones.MaquinasVigente = "1" OrElse peticion.Opciones.MaquinasVigente.ToUpper = "TRUE" Then
                '    spw.AgregarParam("par$bol_maqu_vigente", ProsegurDbType.Logico, 1, , False)
                'Else
                '    spw.AgregarParam("par$bol_maqu_vigente", ProsegurDbType.Logico, 0, , False)
                'End If


            Else

                spw.AgregarParam("par$bol_detallar_trans", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$bol_detallar_valores", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$bol_disponible", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$bol_info_adicional", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$bol_acreditado", ProsegurDbType.Logico, Nothing, , False)
                spw.AgregarParam("par$bol_notificado", ProsegurDbType.Logico, Nothing, , False)

            End If

            spw.AgregarParam("par$fyh_gestion", ProsegurDbType.Data_Hora, peticion.FechaGestion, , False)

            If peticion.FechaCreacion Is Nothing OrElse peticion.FechaCreacion = DateTime.MinValue Then
                spw.AgregarParam("par$fyh_creacion", ProsegurDbType.Data_Hora, Nothing, , False)
            Else
                spw.AgregarParam("par$fyh_creacion", ProsegurDbType.Data_Hora, peticion.FechaCreacion, , False)
            End If

            'GMT HORA
            'spw.AgregarParam("par$nel_gmt_minuto", ProsegurDbType.Identificador_Alfanumerico, GMTMinutoLocalCalculado, , False)
            spw.AgregarParam("par$cod_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoCliente, , False)
            spw.AgregarParam("par$cod_sub_cliente", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoSubCliente, , False)
            spw.AgregarParam("par$cod_punto_servicio", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPtoServicio, , False)
            spw.AgregarParam("par$cod_delegacion", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDelegacion, , False)
            spw.AgregarParam("par$cod_planta", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoPlanta, , False)
            spw.AgregarParam("par$cod_divisa", ProsegurDbType.Identificador_Alfanumerico, peticion.CodigoDivisa, , False)
            spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            'spw.AgregarParam("par$info_ejecucion", ProsegurDbType.Identificador_Alfanumerico, "peticion.info_ejecucion", , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)


            spw.AgregarParam("par$cod_maquinas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_canales", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$cod_subcanales", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)

            spw.AgregarParamInfo("par$info_ejecucion")

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
            spw.AgregarParam("par$rc_saldos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "saldos")
            spw.AgregarParam("par$rc_bolsas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "bolsas")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")

            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")


            If peticion.CodigoMaquinas IsNot Nothing AndAlso peticion.CodigoMaquinas.Count > 0 Then
                spw.Param("par$cod_maquinas").AgregarValorArray("")
                For Each maquina In peticion.CodigoMaquinas
                    If Not String.IsNullOrEmpty(maquina) Then
                        spw.Param("par$cod_maquinas").AgregarValorArray(maquina)
                    End If
                Next
            End If

            If peticion.CodigoCanales IsNot Nothing AndAlso peticion.CodigoCanales.Count > 0 Then
                spw.Param("par$cod_canales").AgregarValorArray("")
                For Each canal In peticion.CodigoCanales
                    If Not String.IsNullOrEmpty(canal) Then
                        spw.Param("par$cod_canales").AgregarValorArray(canal)
                    End If
                Next
            End If


            If peticion.CodigoSubCanales IsNot Nothing AndAlso peticion.CodigoSubCanales.Count > 0 Then
                spw.Param("par$cod_subcanales").AgregarValorArray("")
                For Each canal In peticion.CodigoSubCanales
                    If Not String.IsNullOrEmpty(canal) Then
                        spw.Param("par$cod_subcanales").AgregarValorArray(canal)
                    End If
                Next
            End If

            Return spw

        End Function

        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           peticion As RecuperarSaldos.Peticion,
                                           ByRef respuesta As RecuperarSaldos.Respuesta)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then



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

                Dim _divisasPosibles As New Dictionary(Of String, RecuperarSaldos.Divisa)
                If ds.Tables.Contains("divisas") AndAlso ds.Tables("divisas").Rows.Count > 0 Then
                    For Each rowDivisa As DataRow In ds.Tables("divisas").Rows
                        _divisasPosibles.Add(rowDivisa("OID_DIVISA"), PoblarDivisa(rowDivisa))
                    Next
                End If

                Dim _denominacionesPosibles As New Dictionary(Of String, RecuperarSaldos.Denominacion)
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

                If ds.Tables.Contains("cuentas") AndAlso ds.Tables("cuentas").Rows.Count > 0 Then

                    If respuesta.Maquinas Is Nothing Then respuesta.Maquinas = New List(Of RecuperarSaldos.Maquina)

                    For Each rowCuenta As DataRow In ds.Tables("cuentas").Rows

                        Dim maquina = respuesta.Maquinas.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SECTOR"), GetType(String)))
                        If maquina Is Nothing Then
                            maquina = PoblarMaquina(rowCuenta)
                            respuesta.Maquinas.Add(maquina)
                        End If

                        Dim canal = maquina.Canales.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CANAL"), GetType(String)))
                        If canal Is Nothing Then
                            canal = PoblarCanal(rowCuenta)
                            maquina.Canales.Add(canal)
                        End If

                        Dim subCanal = canal.SubCanales.FirstOrDefault(Function(ct) ct.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCANAL"), GetType(String)))
                        If subCanal Is Nothing Then
                            subCanal = PoblarSubCanal(rowCuenta)
                            canal.SubCanales.Add(subCanal)
                        End If

                        Dim saldo = subCanal.Saldos.FirstOrDefault(Function(ct) ct.Cliente.Codigo = Util.AtribuirValorObj(rowCuenta("COD_CLIENTE"), GetType(String)) AndAlso ct.SubCliente.Codigo = Util.AtribuirValorObj(rowCuenta("COD_SUBCLIENTE"), GetType(String)) AndAlso ct.PuntoServicio.Codigo = Util.AtribuirValorObj(rowCuenta("COD_PTO_SERVICIO"), GetType(String)))
                        If saldo Is Nothing Then
                            saldo = PoblarSaldo(rowCuenta)
                            subCanal.Saldos.Add(saldo)
                        End If

                        If ds.Tables.Contains("movimientos") AndAlso ds.Tables("movimientos").Rows.Count > 0 AndAlso ds.Tables("movimientos").Select("OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ") IsNot Nothing Then

                            For Each rowMovimiento As DataRow In ds.Tables("movimientos").Select("OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ")

                                Dim movimiento = New RecuperarSaldos.Movimiento
                                movimiento = PoblarMovimiento(rowMovimiento, rowCuenta)
                                movimiento.Formulario = _formulariosPosibles.FirstOrDefault(Function(f) f.Key = rowMovimiento("OID_FORMULARIO")).Value

                                If ds.Tables.Contains("efectivos") AndAlso ds.Tables("efectivos").Rows.Count > 0 AndAlso ds.Tables("efectivos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "'  AND COD_NIVEL_DETALLE = 'D' ") IsNot Nothing Then

                                    For Each rowEfetivo As DataRow In ds.Tables("efectivos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ")


                                        Dim _divisa As RecuperarSaldos.Divisa = _divisasPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DIVISA")).Value

                                        If movimiento.Valores Is Nothing Then movimiento.Valores = New List(Of RecuperarSaldos.Divisa)

                                        Dim valorDivisaEfectivo As RecuperarSaldos.Divisa = movimiento.Valores.FirstOrDefault(Function(d) d.Codigo = _divisa.Codigo)

                                        If valorDivisaEfectivo Is Nothing Then
                                            valorDivisaEfectivo = _divisa.Clonar
                                            movimiento.Valores.Add(valorDivisaEfectivo)
                                        End If

                                        If Util.AtribuirValorObj(rowEfetivo("BOL_DISPONIBLE"), GetType(String)) = 1 Then
                                            movimiento.Disponible = True
                                        End If

                                        If Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "T" Then
                                            valorDivisaEfectivo.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))

                                        ElseIf Util.AtribuirValorObj(rowEfetivo("COD_NIVEL_DETALLE"), GetType(String)) = "D" AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowEfetivo("OID_DENOMINACION"), GetType(String))) Then

                                            If valorDivisaEfectivo.Denominaciones Is Nothing Then valorDivisaEfectivo.Denominaciones = New List(Of RecuperarSaldos.Denominacion)
                                            Dim _denominacion As RecuperarSaldos.Denominacion = _denominacionesPosibles.FirstOrDefault(Function(d) d.Key = rowEfetivo("OID_DENOMINACION")).Value


                                            Dim valorDenominacion As RecuperarSaldos.Denominacion = valorDivisaEfectivo.Denominaciones.FirstOrDefault(Function(d) d.Codigo = _denominacion.Codigo)

                                            If valorDenominacion Is Nothing Then
                                                valorDenominacion = _denominacion.Clonar
                                                valorDivisaEfectivo.Denominaciones.Add(valorDenominacion)
                                            End If


                                            valorDenominacion.Cantidad = Util.AtribuirValorObj(rowEfetivo("NEL_CANTIDAD"), GetType(String))
                                            valorDenominacion.Importe = Util.AtribuirValorObj(rowEfetivo("NUM_IMPORTE"), GetType(String))



                                        End If

                                    Next

                                End If

                                If peticion IsNot Nothing AndAlso peticion.Opciones IsNot Nothing AndAlso peticion.Opciones.InfoAdicionales IsNot Nothing AndAlso (peticion.Opciones.InfoAdicionales = "1" OrElse peticion.Opciones.InfoAdicionales.ToUpper = "TRUE") Then

                                    If ds.Tables.Contains("terminos") AndAlso ds.Tables("terminos").Rows.Count > 0 AndAlso ds.Tables("terminos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ") IsNot Nothing Then
                                        movimiento.CamposAdicionales = New List(Of RecuperarSaldos.CampoAdicional)
                                        For Each rowTerminos As DataRow In ds.Tables("terminos").Select("OID_DOCUMENTO = '" & rowMovimiento("OID_DOCUMENTO") & "' ")
                                            movimiento.CamposAdicionales.Add(PoblarCampoAdcional(rowTerminos))
                                        Next
                                    End If

                                End If

                                saldo.Movimientos.Add(movimiento)

                            Next

                        End If

                        If ds.Tables.Contains("saldos") AndAlso ds.Tables("saldos").Rows.Count > 0 AndAlso ds.Tables("saldos").Select("OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ") IsNot Nothing Then

                            For Each rowSaldo As DataRow In ds.Tables("saldos").Select("OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ")

                                Dim _divisa As RecuperarSaldos.Divisa = _divisasPosibles.FirstOrDefault(Function(d) d.Key = rowSaldo("OID_DIVISA")).Value

                                If saldo.Divisas Is Nothing Then saldo.Divisas = New List(Of RecuperarSaldos.Divisa)

                                Dim valorDivisaSaldos As RecuperarSaldos.Divisa = saldo.Divisas.FirstOrDefault(Function(d) d.Codigo = _divisa.Codigo)

                                If valorDivisaSaldos Is Nothing Then
                                    valorDivisaSaldos = _divisa.Clonar
                                    saldo.Divisas.Add(valorDivisaSaldos)
                                End If

                                If Util.AtribuirValorObj(rowSaldo("BOL_DISPONIBLE"), GetType(String)) = 1 Then
                                    valorDivisaSaldos.Disponible = True
                                End If


                                If Util.AtribuirValorObj(rowSaldo("COD_NIVEL_DETALLE"), GetType(String)) = "T" Then
                                    valorDivisaSaldos.Importe = Util.AtribuirValorObj(rowSaldo("NUM_IMPORTE"), GetType(String))

                                    If ds.Tables.Contains("bolsas") AndAlso ds.Tables("bolsas").Rows.Count > 0 AndAlso ds.Tables("bolsas").Select("OID_DIVISA = '" & rowSaldo("OID_DIVISA") & "' AND  OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ") IsNot Nothing Then
                                        If valorDivisaSaldos.Bolsas Is Nothing Then valorDivisaSaldos.Bolsas = New List(Of RecuperarSaldos.Bolsa)
                                        For Each rowBolsa As DataRow In ds.Tables("bolsas").Select("OID_DIVISA = '" & rowSaldo("OID_DIVISA") & "' AND OID_CUENTA_SALDO = '" & rowCuenta("OID_CUENTA") & "' ")
                                            Dim bolsa = PoblarBolsa(rowBolsa)
                                            If (valorDivisaSaldos.Bolsas.FirstOrDefault(Function(d) d.SafeBag = bolsa.SafeBag) Is Nothing) Then
                                                valorDivisaSaldos.Bolsas.Add(bolsa)
                                            End If
                                        Next

                                    End If


                                ElseIf Util.AtribuirValorObj(rowSaldo("COD_NIVEL_DETALLE"), GetType(String)) = "D" AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowSaldo("OID_DENOMINACION"), GetType(String))) Then

                                    If valorDivisaSaldos.Denominaciones Is Nothing Then valorDivisaSaldos.Denominaciones = New List(Of RecuperarSaldos.Denominacion)
                                    'Dim denominacion As RecuperarSaldos.Denominacion = _denominacionesPosibles.FirstOrDefault(Function(d) d.Key = rowSaldo("OID_DENOMINACION")).Value
                                    Dim _denominacion As RecuperarSaldos.Denominacion = _denominacionesPosibles.FirstOrDefault(Function(d) d.Key = rowSaldo("OID_DENOMINACION")).Value
                                    Dim denominacion As RecuperarSaldos.Denominacion = _denominacion.Clonar()

                                    denominacion.Cantidad = Util.AtribuirValorObj(rowSaldo("NEL_CANTIDAD"), GetType(String))
                                    denominacion.Importe = Util.AtribuirValorObj(rowSaldo("NUM_IMPORTE"), GetType(String))
                                    valorDivisaSaldos.Denominaciones.Add(denominacion)
                                End If





                            Next

                        End If


                    Next

                End If

                _divisasPosibles = Nothing
                _denominacionesPosibles = Nothing
                _formulariosPosibles = Nothing

            End If

            ' Validaciones
            If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of ContractoServicio.Contractos.Integracion.Comon.Detalle)
                For Each row As DataRow In ds.Tables("validaciones").Rows
                    respuesta.Resultado.Detalles.Add(New ContractoServicio.Contractos.Integracion.Comon.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                Next

            End If

        End Sub

        Private Shared Function PoblarMaquina(row As DataRow) As RecuperarSaldos.Maquina
            Dim maquina = New RecuperarSaldos.Maquina

            With maquina
                .Codigo = Util.AtribuirValorObj(row("COD_IDENTIFICACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_SECTOR"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .Canales = New List(Of RecuperarSaldos.Canal)
            End With

            Return maquina
        End Function

        Private Shared Function PoblarCanal(row As DataRow) As RecuperarSaldos.Canal
            Dim canal = New RecuperarSaldos.Canal

            With canal
                .Codigo = Util.AtribuirValorObj(row("COD_CANAL"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_CANAL"), GetType(String))
                .SubCanales = New List(Of RecuperarSaldos.SubCanal)
            End With

            Return canal
        End Function

        Private Shared Function PoblarCampoAdcional(row As DataRow) As RecuperarSaldos.CampoAdicional
            Dim campoAdicional = New RecuperarSaldos.CampoAdicional

            With campoAdicional
                .Descripcion = Util.AtribuirValorObj(row("COD_TERMINO"), GetType(String))
                .Valor = Util.AtribuirValorObj(row("DES_VALOR"), GetType(String))
            End With

            Return campoAdicional
        End Function

        Private Shared Function PoblarSubCanal(row As DataRow) As RecuperarSaldos.SubCanal
            Dim subCanal = New RecuperarSaldos.SubCanal

            With subCanal
                .Codigo = Util.AtribuirValorObj(row("COD_SUBCANAL"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_SUBCANAL"), GetType(String))
                .Saldos = New List(Of RecuperarSaldos.Saldo)
            End With

            Return subCanal
        End Function


        Private Shared Function PoblarSaldo(row As DataRow) As RecuperarSaldos.Saldo
            Dim saldo = New RecuperarSaldos.Saldo

            With saldo

                .Cliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad
                .SubCliente = New ContractoServicio.Contractos.Integracion.Comon.Entidad
                .PuntoServicio = New ContractoServicio.Contractos.Integracion.Comon.Entidad

                With .Cliente

                    .Codigo = Util.AtribuirValorObj(row("COD_CLIENTE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_CLIENTE"), GetType(String))
                End With
                With .SubCliente
                    .Codigo = Util.AtribuirValorObj(row("COD_SUBCLIENTE"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_SUBCLIENTE"), GetType(String))
                End With
                With .PuntoServicio
                    .Codigo = Util.AtribuirValorObj(row("COD_PTO_SERVICIO"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(row("DES_PTO_SERVICIO"), GetType(String))
                End With
                .Movimientos = New List(Of RecuperarSaldos.Movimiento)
                .Divisas = New List(Of RecuperarSaldos.Divisa)

            End With

            Return saldo
        End Function

        Private Shared Function PoblarMovimiento(row As DataRow,
                                                 rowCuenta As DataRow) As RecuperarSaldos.Movimiento
            Dim movimiento = New RecuperarSaldos.Movimiento

            With movimiento
                .Codigo = Util.AtribuirValorObj(row("COD_EXTERNO"), GetType(String))
                .Notificado = Util.AtribuirValorObj(row("BOL_NOTIFICADO"), GetType(Boolean))
                .Acreditado = Util.AtribuirValorObj(row("BOL_ACREDITADO"), GetType(Boolean))
                .Disponible = Util.AtribuirValorObj(row("BOL_DISPONIBLE"), GetType(Boolean))

                Dim dateGestion = Util.AtribuirValorObj(row("FYH_GESTION"), GetType(String))
                .FechaGestion = DateTime.ParseExact(dateGestion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture)

                Dim FechaRealizacion = Util.AtribuirValorObj(row("GMT_CREACION"), GetType(String))
                .FechaRealizacion = DateTime.ParseExact(FechaRealizacion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture)

                If .Acreditado Then
                    Dim FechaAcreditacion = Util.AtribuirValorObj(row("FYH_ACREDITACION"), GetType(String))
                    .FechaAcreditacion = DateTime.ParseExact(FechaAcreditacion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture).ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz")
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

        Private Shared Function PoblarDivisa(row As DataRow) As RecuperarSaldos.Divisa
            Dim divisa = New RecuperarSaldos.Divisa

            With divisa
                .Codigo = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DIVISA"), GetType(String))
            End With

            Return divisa
        End Function

        Private Shared Function PoblarDenominacion(row As DataRow) As RecuperarSaldos.Denominacion
            Dim denominacion = New RecuperarSaldos.Denominacion

            With denominacion
                .Codigo = Util.AtribuirValorObj(row("COD_DENOMINACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DENOMINACION"), GetType(String))
            End With

            Return denominacion
        End Function


        Private Shared Function PoblarBolsa(row As DataRow) As RecuperarSaldos.Bolsa
            Dim bolsa = New RecuperarSaldos.Bolsa

            With bolsa
                .SafeBag = Util.AtribuirValorObj(row("DES_VALOR"), GetType(String))
                .Importe = Util.AtribuirValorObj(row("NUM_IMPORTE"), GetType(String))
            End With

            Return bolsa
        End Function


    End Class
End Namespace