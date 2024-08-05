Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Text

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion
Imports System.Globalization

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe Transacion
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TransaccionEfectivo

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Retorna os identificadores das transações
        ''' Foi inserido um inner join com a tabela de tipo documento com documento. 
        ''' Inserido uma nova condição where valendo para a tabela tipo_documento onde bol_certificacion é igual a true
        ''' </summary>
        ''' <param name="Peticion"></param>
        Public Shared Function HayTransacciones(Peticion As CSCertificacion.DatosCertificacion.Peticion) As Boolean

            Dim respuesta As Boolean = False
            Dim estadosPosibles As New List(Of String)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Rechazado.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Sustituido.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Confirmado.RecuperarValor)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoHayTransacciones)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, False))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))

                    command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", command, "AND", "DL")
                    command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSectores, "COD_SECTOR", command, "AND", "S")
                    command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSubCanales, "COD_SUBCANAL", command, "AND", "SC")
                    command.CommandText &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, estadosPosibles, "COD_ESTADO_DOCUMENTO", command, "AND", "TE")

                    respuesta = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return respuesta

        End Function

        ''' <summary>
        ''' Obtener TransaccionEfection
        ''' </summary>
        ''' <param name="identificadorTransaccion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTransaccionEfectivo(identificadorTransaccion As String) As CSCertificacion.TransaccionEfectivo

            Dim transaccion As CSCertificacion.TransaccionEfectivo = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoRecuperar)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO", ProsegurDbType.Objeto_Id, identificadorTransaccion))

                    Dim dtTransaccion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dtTransaccion IsNot Nothing AndAlso dtTransaccion.Rows.Count > 0 Then

                        transaccion = New CSCertificacion.TransaccionEfectivo

                        With transaccion
                            .BolDisponible = Util.AtribuirValorObj(dtTransaccion.Rows(0)("BOL_DISPONIBLE"), GetType(Boolean))
                            .CodDenominacion = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_DENOMINACION"), GetType(String))
                            .CodEstadoDocumento = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_ESTADO_DOCUMENTO"), GetType(String))
                            .CodIsoDivisa = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_ISO_DIVISA"), GetType(String))
                            .CodNivelDetalle = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_NIVEL_DETALLE"), GetType(String))
                            .CodTipoEfectivoTotal = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
                            .CodTipoMovimiento = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_TIPO_MOVIMIENTO"), GetType(String))
                            .NelCantidad = Util.AtribuirValorObj(dtTransaccion.Rows(0)("NEL_CANTIDAD"), GetType(Integer))
                            .NumImporte = Util.AtribuirValorObj(dtTransaccion.Rows(0)("NUM_IMPORTE"), GetType(Decimal))
                            .OidTransaccionEfectivo = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_TRANSACCION_EFECTIVO"), GetType(String))
                            .OidDivisa = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_DIVISA"), GetType(String))
                            .IdentificadorCalidad = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_CALIDAD"), GetType(String))
                            .IdentificadorUnidadMedida = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_UNIDAD_MEDIDA"), GetType(String))
                            .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(dtTransaccion.Rows(0)("FYH_PLAN_CERTIFICACION"), GetType(DateTime))
                        End With
                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return transaccion
        End Function

        ''' <summary>
        ''' Recupera as transacciones que deve ser estornadas do documento.
        ''' </summary>
        ''' <param name="identificadorDocumento">Identificador do documento.</param>
        ''' <param name="estadoDocumento">Estado do documento.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerTransaccionesEfectivoParaEstorno(identificadorDocumento As String, estadoDocumento As String) As List(Of Clases.Transaccion)

            Dim transacciones As List(Of Clases.Transaccion) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoRecuperarParaEstorno)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCUMENTO", ProsegurDbType.Descricao_Curta, estadoDocumento))

                    Dim dtTransaccion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dtTransaccion IsNot Nothing AndAlso dtTransaccion.Rows.Count > 0 Then

                        transacciones = New List(Of Clases.Transaccion)

                        Dim identificadoresCuenta As New List(Of String)

                        For Each dr In dtTransaccion.Rows

                            Dim transaccion As New Clases.Transaccion

                            With transaccion
                                .Identificador = Util.AtribuirValorObj(dr("OID_TRANSACCION_EFECTIVO"), GetType(String))
                                .Documento = New Clases.Documento() With {.Identificador = Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)), .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(dtTransaccion.Rows(0)("FYH_PLAN_CERTIFICACION"), GetType(DateTime))}
                                .TipoSitio = RecuperarEnum(Of Enumeradores.TipoSitio)(dr("COD_TIPO_SITIO").ToString)
                                .TipoSaldo = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean))
                                .EstadoDocumento = RecuperarEnum(Of Enumeradores.EstadoDocumento)(dr("COD_ESTADO_DOCUMENTO").ToString)
                                .TipoMovimiento = RecuperarEnum(Of Enumeradores.TipoMovimiento)(dr("COD_TIPO_MOVIMIENTO").ToString)
                                .Observaciones = Util.AtribuirValorObj(dr("OBS_TRANSACCION"), GetType(String))
                                .FechaHoraCreacion = Util.AtribuirValorObj(dr("OBS_TRANSACCION"), GetType(DateTime))
                                .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                                .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                                .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
                                .Divisa = New Clases.Divisa() With {.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))}
                                .Certificado = New Clases.Certificado() With {.Identificador = Util.AtribuirValorObj(dr("OID_CERTIFICADO"), GetType(String))}
                                .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(dr("FYH_PLAN_CERTIFICACION"), GetType(DateTime))

                                If .TipoSitio = Enumeradores.TipoSitio.Origen Then
                                    .Documento.CuentaOrigen = New Clases.Cuenta With {.Identificador = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))}
                                    .Documento.CuentaSaldoOrigen = New Clases.Cuenta With {.Identificador = Util.AtribuirValorObj(dr("OID_CUENTA_SALDO"), GetType(String))}
                                Else
                                    .Documento.CuentaDestino = New Clases.Cuenta With {.Identificador = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))}
                                    .Documento.CuentaSaldoDestino = New Clases.Cuenta With {.Identificador = Util.AtribuirValorObj(dr("OID_CUENTA_SALDO"), GetType(String))}
                                End If

                                If dr.Table.Columns.Contains("OID_CUENTA") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))) Then
                                    If Not identificadoresCuenta.Contains(Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))) Then
                                        identificadoresCuenta.Add(Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String)))
                                    End If
                                End If
                                If dr.Table.Columns.Contains("OID_CUENTA_SALDO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("OID_CUENTA_SALDO"), GetType(String))) Then
                                    If Not identificadoresCuenta.Contains(Util.AtribuirValorObj(dr("OID_CUENTA_SALDO"), GetType(String))) Then
                                        identificadoresCuenta.Add(Util.AtribuirValorObj(dr("OID_CUENTA_SALDO"), GetType(String)))
                                    End If
                                End If

                            End With

                            transacciones.Add(transaccion)
                        Next

                        Dim cuentas As ObservableCollection(Of Clases.Cuenta) = Nothing
                        cuentas = Cuenta.ObtenerCuentasPorIdentificadores(identificadoresCuenta, Enumeradores.TipoCuenta.Saldo, "ObtenerDatosDeLosDocumentos")

                        For Each transaccion In transacciones

                            If transaccion.TipoSitio = Enumeradores.TipoSitio.Origen Then
                                transaccion.Documento.CuentaOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = transaccion.Documento.CuentaOrigen.Identificador)
                                transaccion.Documento.CuentaSaldoOrigen = cuentas.FirstOrDefault(Function(x) x.Identificador = transaccion.Documento.CuentaSaldoOrigen.Identificador)
                            Else
                                transaccion.Documento.CuentaDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = transaccion.Documento.CuentaDestino.Identificador)
                                transaccion.Documento.CuentaSaldoDestino = cuentas.FirstOrDefault(Function(x) x.Identificador = transaccion.Documento.CuentaSaldoDestino.Identificador)
                            End If

                        Next

                    End If

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return transacciones

        End Function

        Public Shared Function ObtenerTransaccionesFechas(Peticion As ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Peticion,
                                                          codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno),
                                                          GMTHoraLocalCalculado As Double,
                                                          GMTMinutoLocalCalculado As Double,
                                                          ByRef ValidacionesError As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)
                                                                   ) As List(Of ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion)
            Try

                If ValidacionesError Is Nothing Then ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

                Dim strWhereDocs As New StringBuilder
                Dim strWhereGeral As New StringBuilder
                Dim strWhereTrans As New StringBuilder
                Dim strGroupTransMedioPago As String = ""
                Dim strGroupTransEfectivo As String = ""
                Dim strSectores As New StringBuilder
                Dim strGroupBy As String = ""
                Dim strSaldoDetGP As String = ",TRANS.OID_DENOMINACION,TRANS.COD_DENOMINACION, TRANS.DES_DENOMINACION, COD_TIPO_MEDIO_PAGO, OID_MEDIO_PAGO, COD_MEDIO_PAGO, DES_MEDIO_PAGO "
                Dim selectDet As String = ""
                Dim selectEfectivo As String = ""
                Dim selectMedioPago As String = ""
                Dim camposDinamicosDocumentos As String = ""
                Dim camposDinamicos As String = ""
                Dim innerDetEfectivo As String = ""
                Dim innerDetMedioPago As String = ""
                Dim ValoresInformativos As String = ""
                Dim innerValoresInformativos As String = ""
                Dim innerValoresInformativos2 As String = ""
                Dim selectValoresInformativos As String = ""

                Dim innerSector As String = " INNER JOIN GEPR_TSECTOR SEC_O ON SEC_O.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN "
                innerSector += " INNER JOIN GEPR_TPLANTA PL_O ON PL_O.OID_PLANTA = SEC_O.OID_PLANTA "
                innerSector += " INNER JOIN GEPR_TDELEGACION DEL_O ON DEL_O.OID_DELEGACION = PL_O.OID_DELEGACION "

                innerSector += " INNER JOIN GEPR_TSECTOR SEC_D ON SEC_D.OID_SECTOR = DOCU.OID_SECTOR_DESTINO "
                innerSector += " INNER JOIN GEPR_TPLANTA PL_D ON PL_D.OID_PLANTA = SEC_D.OID_PLANTA "
                innerSector += " INNER JOIN GEPR_TDELEGACION DEL_D ON DEL_D.OID_DELEGACION = PL_D.OID_DELEGACION "


                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    cmd.CommandText = My.Resources.TransaccionRecuperarTransaccionesFechas
                    cmd.CommandType = CommandType.Text
                    Dim idDelegacion As String = String.Empty

                    If codigosAjenos Is Nothing Then

                        If Not String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                strSectores.AppendLine(" And D.COD_DELEGACION = []COD_DELEGACION")
                            Else
                                strWhereDocs.AppendLine(" AND (DEL_O.COD_DELEGACION = []COD_DELEGACION")
                                strWhereDocs.AppendLine(" OR DEL_D.COD_DELEGACION = []COD_DELEGACION)")
                            End If

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, Peticion.CodigoDelegacion))
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoPlanta) Then
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                strSectores.AppendLine(" AND P.COD_PLANTA = []COD_PLANTA")
                            Else
                                strWhereDocs.AppendLine(" AND (PL_O.COD_PLANTA = []COD_PLANTA")
                                strWhereDocs.AppendLine(" OR PL_D.COD_PLANTA = []COD_PLANTA)")
                            End If
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Longa, Peticion.CodigoPlanta))
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then
                            strWhereDocs.AppendLine(" AND  (CLI_O.COD_CLIENTE = []COD_CLIENTE ")
                            strWhereDocs.AppendLine(" OR  CLI_D.COD_CLIENTE = []COD_CLIENTE) ")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, Peticion.CodigoCliente))
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoSubCliente) Then
                            strWhereDocs.AppendLine(" AND  (SBCLI_O.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                            strWhereDocs.AppendLine(" OR  SBCLI_D.COD_SUBCLIENTE = []COD_SUBCLIENTE) ")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, Peticion.CodigoSubCliente))
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoPtoServicio) Then
                            strWhereDocs.AppendLine(" AND  (PT_O.COD_PTO_SERVICIO = []COD_PTO_SERVICIO")
                            strWhereDocs.AppendLine(" OR  PT_D.COD_PTO_SERVICIO = []COD_PTO_SERVICIO) ")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.CodigoPtoServicio))
                        End If

                        If Peticion.Divisas IsNot Nothing AndAlso Peticion.Divisas.Count > 0 Then
                            Dim filtro_Div As String = String.Empty
                            filtro_Div = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Divisas.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoIsoDivisa)).Select(Function(a) a.CodigoIsoDivisa).ToList(), "COD_ISO_DIVISA", cmd, "", "DIV")
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirValoresInformativos Then

                                filtro_Div = " ( " & filtro_Div & Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Divisas.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoIsoDivisa)).Select(Function(a) a.CodigoIsoDivisa).ToList(), "COD_ISO_DIVISA", cmd, "OR", "informativo") & " )"
                            End If

                            filtro_Div = " AND " & filtro_Div
                            strWhereGeral.AppendLine(filtro_Div)

                        End If

                        If Peticion.Sectores IsNot Nothing AndAlso Peticion.Sectores.Count > 0 Then
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                strSectores.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoSector)).Select(Function(a) a.CodigoSector).ToList(), "COD_SECTOR", cmd, "AND", "SECT"))
                            Else
                                strWhereDocs.AppendLine(" AND (")
                                strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoSector)).Select(Function(a) a.CodigoSector).ToList(), "COD_SECTOR", cmd, "", "SEC_O"))
                                strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoSector)).Select(Function(a) a.CodigoSector).ToList(), "COD_SECTOR", cmd, "OR", "SEC_D"))
                                strWhereDocs.Append(") ")
                            End If
                        End If

                        If Peticion.Canales IsNot Nothing AndAlso Peticion.Canales.Count > 0 Then
                            strWhereDocs.AppendLine(" AND (")
                            strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Canales.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoCanal)).Select(Function(a) a.CodigoCanal).ToList(), "COD_CANAL", cmd, "", "CN_O"))
                            strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Canales.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoCanal)).Select(Function(a) a.CodigoCanal).ToList(), "COD_CANAL", cmd, "OR", "CN_D"))
                            strWhereDocs.Append(") ")
                        End If

                    Else

                        If Not String.IsNullOrEmpty(Peticion.CodigoDelegacion) Then
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                strSectores.AppendLine(" AND D.OID_DELEGACION = []OID_DELEGACION")
                            Else
                                strWhereDocs.AppendLine(" AND (DEL_O.OID_DELEGACION = []OID_DELEGACION")
                                strWhereDocs.AppendLine(" OR DEL_D.OID_DELEGACION = []OID_DELEGACION)")
                            End If

                            Dim _delegacion As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Peticion.CodigoDelegacion AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDELEGACION")
                            If _delegacion Is Nothing Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Peticion.CodigoDelegacion)})
                            Else
                                idDelegacion = _delegacion.IdentificadorTablaGenesis
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Longa, _delegacion.IdentificadorTablaGenesis))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoPlanta) Then
                            If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                strSectores.AppendLine(" AND P.OID_PLANTA = []OID_PLANTA")
                            Else
                                strWhereDocs.AppendLine(" AND (PL_O.OID_PLANTA = []OID_PLANTA")
                                strWhereDocs.AppendLine(" OR PL_D.OID_PLANTA = []OID_PLANTA)")
                            End If

                            Dim _planta As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Peticion.CodigoPlanta AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPLANTA")
                            If _planta Is Nothing Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Peticion.CodigoPlanta)})
                            Else
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Descricao_Longa, _planta.IdentificadorTablaGenesis))
                            End If

                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoCliente) Then
                            strWhereDocs.AppendLine(" AND  (CLI_O.OID_CLIENTE = []OID_CLIENTE ")
                            strWhereDocs.AppendLine(" OR  CLI_D.OID_CLIENTE = []OID_CLIENTE) ")

                            Dim _cliente As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Peticion.CodigoCliente AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                            If _cliente Is Nothing Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Peticion.CodigoCliente)})
                            Else
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Descricao_Longa, _cliente.IdentificadorTablaGenesis))
                            End If
                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoSubCliente) Then
                            strWhereDocs.AppendLine(" AND  (SBCLI_O.OID_SUBCLIENTE = []OID_SUBCLIENTE ")
                            strWhereDocs.AppendLine(" OR  SBCLI_D.OID_SUBCLIENTE = []OID_SUBCLIENTE) ")

                            Dim _subCliente As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Peticion.CodigoSubCliente AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE")
                            If _subCliente Is Nothing Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Peticion.CodigoSubCliente)})
                            Else
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Descricao_Longa, _subCliente.IdentificadorTablaGenesis))
                            End If

                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoPtoServicio) Then
                            strWhereDocs.AppendLine(" AND  (PT_O.OID_PTO_SERVICIO = []OID_PTO_SERVICIO")
                            strWhereDocs.AppendLine(" OR  PT_D.OID_PTO_SERVICIO = []OID_PTO_SERVICIO) ")

                            Dim _ptoServicio As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = Peticion.CodigoPtoServicio AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO")
                            If _ptoServicio Is Nothing Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), Peticion.CodigoPtoServicio)})
                            Else
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, _ptoServicio.IdentificadorTablaGenesis))
                            End If

                        End If

                        If Not String.IsNullOrEmpty(Peticion.CodigoTipoPlanificacion) Then

                            innerSector += Environment.NewLine + " LEFT JOIN SAPR_TPLANXMAQUINA PM ON PM.OID_SECTOR = DOCU.OID_SECTOR_DESTINO "
                            innerSector += Environment.NewLine + " LEFT JOIN SAPR_TPLANIFICACION PLAN ON PLAN.OID_PLANIFICACION = PM.OID_PLANIFICACION "
                            innerSector += Environment.NewLine + " LEFT JOIN SAPR_TTIPO_PLANIFICACION TP ON TP.OID_TIPO_PLANIFICACION = PLAN.OID_TIPO_PLANIFICACION "

                            strWhereDocs.AppendLine(" AND TP.COD_TIPO_PLANIFICACION = []COD_TIPO_PLANIFICACION ")

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_PLANIFICACION", ProsegurDbType.Descricao_Longa, Peticion.CodigoTipoPlanificacion))
                        End If


                        If Peticion.Sectores IsNot Nothing AndAlso Peticion.Sectores.Count > 0 Then

                            Dim _sectores = codigosAjenos.Where(Function(x) Peticion.Sectores.Where(Function(a) a.CodigoSector = x.Codigo).Count > 0 AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                            If _sectores Is Nothing OrElse _sectores.Count <> Peticion.Sectores.Count Then
                                Dim lstSectores = Peticion.Sectores.Clonar
                                lstSectores.RemoveAll(Function(a) _sectores.Where(Function(b) b.Codigo = a.CodigoSector).Count > 0)
                                For Each _sectorError In lstSectores
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _sectorError.CodigoSector)})
                                Next
                            Else
                                If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirSubSectores Then
                                    strSectores.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_SECTOR", cmd, "AND", "SECT"))
                                Else
                                    strWhereDocs.AppendLine(" AND (")
                                    strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_SECTOR", cmd, "", "SEC_O"))
                                    strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _sectores.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_SECTOR", cmd, "OR", "SEC_D"))
                                    strWhereDocs.Append(") ")
                                End If
                            End If

                        End If

                        If Peticion.Canales IsNot Nothing AndAlso Peticion.Canales.Count > 0 Then

                            Dim _canales = codigosAjenos.Where(Function(x) Peticion.Canales.Where(Function(a) a.CodigoCanal = x.Codigo).Count > 0 AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                            If _canales Is Nothing OrElse _canales.Count <> Peticion.Canales.Count Then
                                Dim lstCanales = Peticion.Canales.Clonar
                                lstCanales.RemoveAll(Function(a) _canales.Where(Function(b) b.Codigo = a.CodigoCanal).Count > 0)
                                For Each _canalError In lstCanales
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _canalError.CodigoCanal)})
                                Next
                            Else
                                strWhereDocs.AppendLine(" AND (")
                                strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _canales.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_CANAL", cmd, "", "CN_O"))
                                strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _canales.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_CANAL", cmd, "OR", "CN_D"))
                                strWhereDocs.Append(") ")
                            End If
                        End If

                        If Peticion.Divisas IsNot Nothing AndAlso Peticion.Divisas.Count > 0 Then
                            Dim filtro_Div = String.Empty
                            Dim _divisas = codigosAjenos.Where(Function(x) Peticion.Divisas.Where(Function(a) a.CodigoIsoDivisa = x.Codigo).Count > 0 AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDIVISA")
                            If _divisas Is Nothing OrElse _divisas.Count <> Peticion.Divisas.Count Then
                                Dim lstDivisas = Peticion.Divisas.Clonar
                                lstDivisas.RemoveAll(Function(a) _divisas.Where(Function(b) b.Codigo = a.CodigoIsoDivisa).Count > 0)
                                For Each _divisaError In lstDivisas
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _divisaError.CodigoIsoDivisa)})
                                Next
                            Else

                                filtro_Div = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _divisas.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_DIVISA", cmd, "", "DIV")
                                If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirValoresInformativos Then

                                    filtro_Div = " ( " & filtro_Div & Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _divisas.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.IdentificadorTablaGenesis)).Select(Function(a) a.IdentificadorTablaGenesis).ToList(), "OID_DIVISA", cmd, "OR", "informativo") & " )"
                                End If

                                filtro_Div = " AND " & filtro_Div
                                strWhereGeral.AppendLine(filtro_Div)
                            End If

                        End If

                    End If

                    If (Peticion.FechaCreacion IsNot Nothing AndAlso (Peticion.FechaCreacion.FechaDesde <> DateTime.MinValue OrElse Peticion.FechaCreacion.FechaHasta <> DateTime.MinValue)) _
                            OrElse (Peticion.FechaGestion IsNot Nothing AndAlso (Peticion.FechaGestion.FechaDesde <> DateTime.MinValue OrElse Peticion.FechaGestion.FechaHasta <> DateTime.MinValue)) Then
                        strWhereDocs.Append(" AND (")
                        Dim strFechas As New List(Of String)
                        If Peticion.FechaCreacion IsNot Nothing AndAlso (Peticion.FechaCreacion.FechaDesde <> DateTime.MinValue OrElse Peticion.FechaCreacion.FechaHasta <> DateTime.MinValue) Then
                            strWhereDocs.Append("(")
                            If Peticion.FechaCreacion.FechaDesde <> DateTime.MinValue Then
                                strFechas.Add(" DOCU.GMT_CREACION >= to_timestamp_Tz([]GMT_CREACION_DESDE, 'yyyy-mm-dd hh24:mi:ss TZH:TZM')")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_DESDE", ProsegurDbType.Descricao_Longa, Peticion.FechaCreacion.FechaDesde.ToString("yyyy-MM-dd HH:mm:ss") & " -00:00"))
                            End If
                            If Peticion.FechaCreacion.FechaHasta <> DateTime.MinValue Then
                                strFechas.Add(" DOCU.GMT_CREACION < to_timestamp_Tz([]GMT_CREACION_HASTA, 'yyyy-mm-dd hh24:mi:ss TZH:TZM')")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "GMT_CREACION_HASTA", ProsegurDbType.Descricao_Longa, Peticion.FechaCreacion.FechaHasta.ToString("yyyy-MM-dd HH:mm:ss") & " -00:00"))
                            End If
                            strWhereDocs.Append(String.Join(" AND ", strFechas))
                            strWhereDocs.Append(")")
                        End If

                        If Peticion.FechaGestion IsNot Nothing AndAlso (Peticion.FechaGestion.FechaDesde <> DateTime.MinValue OrElse Peticion.FechaGestion.FechaHasta <> DateTime.MinValue) Then

                            If strFechas.Count > 0 Then
                                strWhereDocs.AppendLine(" OR ")
                            End If

                            strWhereDocs.AppendLine("(")
                            strFechas = New List(Of String)
                            If Peticion.FechaGestion.FechaDesde <> DateTime.MinValue Then
                                strFechas.Add(" DOCU.FYH_GESTION >= []FYH_GESTION_DESDE")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION_DESDE", ProsegurDbType.Data_Hora, Peticion.FechaGestion.FechaDesde))
                            End If
                            If Peticion.FechaGestion.FechaHasta <> DateTime.MinValue Then
                                strFechas.Add(" DOCU.FYH_GESTION <= []FYH_GESTION_HASTA")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_GESTION_HASTA", ProsegurDbType.Data_Hora, Peticion.FechaGestion.FechaHasta))
                            End If
                            strWhereDocs.Append(String.Join(" AND ", strFechas))
                            strWhereDocs.Append(")")
                        End If
                        strWhereDocs.Append(")")
                    End If

                    If Peticion.Formulario IsNot Nothing AndAlso Peticion.Formulario.Count > 0 Then
                        strWhereDocs.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.Formulario.Where(Function(b) b IsNot Nothing AndAlso Not String.IsNullOrEmpty(b.CodigoFormulario)).Select(Function(a) a.CodigoFormulario).ToList(), "COD_FORMULARIO", cmd, "AND", "FORM"))
                    End If
                    Dim obtenerIAC As Boolean = False
                    If Peticion.Filtros IsNot Nothing Then
                        If Not Peticion.Filtros.IncluirMediosPago Then
                            Dim strMedioPago As String = "--[MEDIOPAGO]"
                            cmd.CommandText = cmd.CommandText.Remove(cmd.CommandText.IndexOf(strMedioPago), cmd.CommandText.LastIndexOf(strMedioPago) - (cmd.CommandText.IndexOf(strMedioPago) - strMedioPago.Length))
                        End If
                        If Peticion.Filtros.DetallarSaldos Or Peticion.Filtros.IncluirValoresInformativos Then
                            selectDet = ",OID_DENOMINACION,COD_DENOMINACION,DES_DENOMINACION,COD_TIPO_MEDIO_PAGO,OID_MEDIO_PAGO,COD_MEDIO_PAGO,DES_MEDIO_PAGO"
                            selectEfectivo += ", DENO.OID_DENOMINACION, DENO.COD_DENOMINACION, DENO.DES_DENOMINACION, NULL COD_TIPO_MEDIO_PAGO, NULL OID_MEDIO_PAGO, NULL COD_MEDIO_PAGO, NULL DES_MEDIO_PAGO"
                            selectMedioPago += ",NULL OID_DENOMINACION,NULL COD_DENOMINACION,NULL DES_DENOMINACION,TRANS.COD_TIPO_MEDIO_PAGO,MP.OID_MEDIO_PAGO,MP.COD_MEDIO_PAGO,MP.DES_MEDIO_PAGO"
                            strGroupTransEfectivo = ", DENO.OID_DENOMINACION, DENO.COD_DENOMINACION, DENO.DES_DENOMINACION"
                            strGroupTransMedioPago = ",TRANS.COD_TIPO_MEDIO_PAGO,MP.OID_MEDIO_PAGO,MP.COD_MEDIO_PAGO,MP.DES_MEDIO_PAGO"
                            innerDetEfectivo = " LEFT JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = TRANS.OID_DENOMINACION "
                            innerDetMedioPago = " LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = TRANS.OID_MEDIO_PAGO "
                            strGroupBy += strSaldoDetGP
                        End If
                        If Peticion.Filtros.SaldoAMostrar <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoSaldo.Ambos Then
                            strWhereTrans.AppendLine("AND  TRANS.BOL_DISPONIBLE = []BOL_DISPONIBLE")

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, Convert.ToInt32(Peticion.Filtros.SaldoAMostrar)))
                        End If
                        If Peticion.Filtros.Certificado <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoCertificado.Ambos Then
                            strWhereTrans.AppendLine("AND  TRANS.BOL_CERTIFICADO = []BOL_CERTIFICADO")

                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, Convert.ToInt32(Peticion.Filtros.Certificado)))
                        End If

                        If Peticion.Filtros.Notificado IsNot Nothing AndAlso Peticion.Filtros.Notificado <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoNotificado.Ambos Then
                            strWhereGeral.AppendLine("AND DOCU.BOL_NOTIFICADO = []BOL_NOTIFICADO")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_NOTIFICADO", ProsegurDbType.Logico, Convert.ToInt32(Peticion.Filtros.Notificado)))
                        End If



                        If Peticion.Filtros.IncluirSubSectores Then
                            innerSector += Environment.NewLine + Environment.NewLine + " INNER JOIN ("
                            innerSector += Environment.NewLine + " SELECT  SECT.COD_SECTOR, SECT.DES_SECTOR, SECT.OID_SECTOR, SECT.OID_PLANTA "
                            innerSector += Environment.NewLine + " FROM GEPR_TSECTOR SECT "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = SECT.OID_PLANTA "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION "
                            innerSector += Environment.NewLine + " START WITH 1 = 1  {0} "
                            innerSector += Environment.NewLine + " CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE "
                            innerSector += Environment.NewLine + " ) SEC_O ON SEC_O.OID_SECTOR = DOCU.OID_SECTOR_ORIGEN"
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TPLANTA PL_O ON PL_O.OID_PLANTA = SEC_O.OID_PLANTA "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TDELEGACION DEL_O ON DEL_O.OID_DELEGACION = PL_O.OID_DELEGACION "

                            innerSector += Environment.NewLine + Environment.NewLine + " INNER JOIN ("
                            innerSector += Environment.NewLine + " SELECT  SECT.COD_SECTOR, SECT.DES_SECTOR, SECT.OID_SECTOR, SECT.OID_PLANTA "
                            innerSector += Environment.NewLine + " FROM GEPR_TSECTOR SECT "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = SECT.OID_PLANTA "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION "
                            innerSector += Environment.NewLine + " START WITH 1 = 1  {0} "
                            innerSector += Environment.NewLine + " CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE "
                            innerSector += Environment.NewLine + " ) SEC_D ON SEC_D.OID_SECTOR = DOCU.OID_SECTOR_DESTINO"
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TPLANTA PL_D ON PL_D.OID_PLANTA = SEC_D.OID_PLANTA "
                            innerSector += Environment.NewLine + " INNER JOIN GEPR_TDELEGACION DEL_D ON DEL_D.OID_DELEGACION = PL_D.OID_DELEGACION "
                            innerSector = String.Format(innerSector, strSectores)
                        End If
                        obtenerIAC = Peticion.Filtros.IACs
                    End If


                    camposDinamicosDocumentos += Environment.NewLine + " ESTP.COD_ESTADO_PERIODO, CASE WHEN  DOCU.FYH_ACREDITACION is null THEN ''   ELSE to_char(DOCU.FYH_ACREDITACION, 'YYYY-MM-DD HH24:MI:SS') || '+00:00'  END AS FYH_ACREDITACION, "
                    camposDinamicos += Environment.NewLine + " DOCU.BOL_ACREDITADO, DOCU.FYH_ACREDITACION, "
                    strGroupBy += ",  DOCU.BOL_ACREDITADO, DOCU.FYH_ACREDITACION"

                    innerSector += Environment.NewLine + " LEFT JOIN SAPR_TPERIODOXDOCUMENTO PD ON PD.OID_DOCUMENTO = DOCU.OID_DOCUMENTO "
                    innerSector += Environment.NewLine + " LEFT JOIN SAPR_TPERIODO P ON P.OID_PERIODO = PD.OID_PERIODO "
                    innerSector += Environment.NewLine + " LEFT JOIN SAPR_TESTADO_PERIODO ESTP ON ESTP.OID_ESTADO_PERIODO = P.OID_ESTADO_PERIODO "
                    innerSector += Environment.NewLine + " LEFT JOIN SAPR_TACREDITACION ACRE ON ACRE.OID_ACREDITACION  = P.OID_ACREDITACION  "

                    If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.Acreditado IsNot Nothing AndAlso Peticion.Filtros.Acreditado <> ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoAcreditado.Ambos Then

                        If Peticion.Filtros.Acreditado = ContractoServicio.Contractos.Integracion.Comon.Enumeradores.TipoAcreditado.Acreditado Then
                            strWhereDocs.Append(Environment.NewLine + " AND ESTP.COD_ESTADO_PERIODO IN ('AC', 'IG') ")
                        Else
                            strWhereDocs.Append(Environment.NewLine + " AND ESTP.COD_ESTADO_PERIODO NOT IN ('AC', 'IG') ")
                        End If

                    End If


                    If Peticion.FechaAcreditacion IsNot Nothing AndAlso (Peticion.FechaAcreditacion.FechaDesde <> DateTime.MinValue OrElse Peticion.FechaAcreditacion.FechaHasta <> DateTime.MinValue) Then
                        If Peticion.FechaAcreditacion.FechaDesde <> DateTime.MinValue Then
                            strWhereDocs.Append(" AND DOCU.FYH_ACREDITACION >= TO_TIMESTAMP(TO_CHAR([]FYH_ACREDITACION_DESDE, 'yyyy-mm-dd hh24:mi:ss'), 'yyyy-mm-dd hh24:mi:ss')")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ACREDITACION_DESDE", ProsegurDbType.Data_Hora, Peticion.FechaAcreditacion.FechaDesde))
                        End If
                        If Peticion.FechaAcreditacion.FechaHasta <> DateTime.MinValue Then
                            strWhereDocs.Append(" AND DOCU.FYH_ACREDITACION < TO_TIMESTAMP(TO_CHAR([]FYH_ACREDITACION_HASTA, 'yyyy-mm-dd hh24:mi:ss'), 'yyyy-mm-dd hh24:mi:ss')")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ACREDITACION_HASTA", ProsegurDbType.Data_Hora, Peticion.FechaAcreditacion.FechaHasta))
                        End If
                    End If



                    If Peticion.Filtros IsNot Nothing AndAlso Peticion.Filtros.IncluirValoresInformativos Then
                        ' strWhereTrans.AppendLine(" AND 1 = 2 ")
                        Dim camposDetallarSaldos = ""
                        Dim joinDetallarSaldos = ""
                        Dim groupDetallarSaldos = ""

                        'If Peticion.Filtros.DetallarSaldos Then
                        camposDetallarSaldos = ", DENO.OID_DENOMINACION, DENO.COD_DENOMINACION, DENO.DES_DENOMINACION"
                        joinDetallarSaldos = " Left Join GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = EFDO.OID_DENOMINACION "

                        selectDet = " ,NVL(TRANS.OID_DENOMINACION, informativo.OID_DENOMINACION) OID_DENOMINACION
                                          ,NVL(TRANS.COD_DENOMINACION, informativo.COD_DENOMINACION) COD_DENOMINACION
                                          ,NVL(TRANS.DES_DENOMINACION, informativo.DES_DENOMINACION) DES_DENOMINACION
                                          ,COD_TIPO_MEDIO_PAGO,OID_MEDIO_PAGO,COD_MEDIO_PAGO,DES_MEDIO_PAGO"


                        strGroupBy += "  ,informativo.OID_DENOMINACION
                                             ,informativo.COD_DENOMINACION
                                             ,informativo.DES_DENOMINACION "

                        'End If
                        ValoresInformativos = String.Format(" informativo as (  SELECT EFDO.OID_DOCUMENTO
                                                    , DIVI.COD_ISO_DIVISA
                                                    , SUM(EFDO.NUM_IMPORTE) IMPORTE
                                                    , 'D' DECLARADO
                                                    , DIVI.DES_DIVISA
                                                    , DIVI.OID_DIVISA
                                                    , SUM(EFDO.NEL_CANTIDAD) NEL_CANTIDAD
                                                    {0}
                                                 FROM SAPR_TEFECTIVOXDOCUMENTO EFDO
                                           INNER JOIN GEPR_TDIVISA DIVI ON DIVI.OID_DIVISA = EFDO.OID_DIVISA
                                           INNER JOIN documentos DOCU ON DOCU.OID_DOCUMENTO = EFDO.OID_DOCUMENTO
                                            {1}
                                            WHERE  1 = 1 
                                             GROUP BY DIVI.COD_SIMBOLO, EFDO.OID_DOCUMENTO, DIVI.COD_ISO_DIVISA, DIVI.DES_DIVISA, DIVI.OID_DIVISA {0} ), ", camposDetallarSaldos, joinDetallarSaldos)


                        innerValoresInformativos = "  LEFT JOIN informativo ON  DOCU.OID_DOCUMENTO  = informativo.OID_DOCUMENTO  "

                        innerValoresInformativos2 = " AND  informativo.OID_DIVISA = trans.OID_DIVISA AND   (informativo.OID_DENOMINACION = trans.OID_DENOMINACION or (informativo.OID_DENOMINACION is null and trans.OID_DENOMINACION is null))  "


                        strGroupBy += "  , informativo.IMPORTE
                                      , informativo.NEL_CANTIDAD  
                                      ,informativo.OID_DIVISA
                                      ,informativo.DES_DIVISA
                                      , informativo.COD_ISO_DIVISA "

                        selectValoresInformativos = "  DOCU.BOL_NOTIFICADO,
                                        NVL(DIV.OID_DIVISA, informativo.OID_DIVISA) OID_DIVISA,
                                        NVL(DIV.COD_ISO_DIVISA, informativo.COD_ISO_DIVISA) COD_ISO_DIVISA,
                                        NVL(DIV.DES_DIVISA, informativo.DES_DIVISA) OID_DIVISA,
                                        NVL(SUM(TRANS.NUM_IMPORTE), informativo.IMPORTE) IMPORTE,
                                        NVL(SUM(TRANS.NEL_CANTIDAD), informativo.NEL_CANTIDAD) NEL_CANTIDAD "

                    Else
                        selectValoresInformativos = " DOCU.BOL_NOTIFICADO,
                                                     DIV.OID_DIVISA,
                                                     DIV.COD_ISO_DIVISA,
                                                     DIV.DES_DIVISA,
                                                     SUM(TRANS.NUM_IMPORTE) IMPORTE,
                                                     SUM(TRANS.NEL_CANTIDAD) NEL_CANTIDAD "
                    End If



                    If ValidacionesError Is Nothing OrElse ValidacionesError.Count = 0 Then
                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText,
                                                                                                       strWhereDocs, '{0}
                                                                                                       selectEfectivo,'{1} 
                                                                                                       innerDetEfectivo,'{2} 
                                                                                                       selectMedioPago, '{3}
                                                                                                       innerDetMedioPago, '{4}
                                                                                                       selectDet, '{5}
                                                                                                       strGroupBy, '{6}
                                                                                                       innerSector, '{7}
                                                                                                       strWhereGeral, '{8}
                                                                                                       strWhereTrans, '{9}
                                                                                                       camposDinamicosDocumentos, '{10}
                                                                                                       camposDinamicos, '{11}
                                                                                                       selectValoresInformativos, '{12}
                                                                                                       ValoresInformativos, '{13}
                                                                                                       innerValoresInformativos,'{14} 
                                                                                                       strGroupBy,'{15}
                                                                                                       strGroupTransEfectivo,'{16}
                                                                                                       strGroupTransMedioPago,'{17}
                                                                                                       innerValoresInformativos2'{18}                        
                                                                                                       ))

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                        Return cargarRecuperarTransaccionesFechas(dt, obtenerIAC, codigosAjenos, GMTHoraLocalCalculado, GMTMinutoLocalCalculado, ValidacionesError)

                    End If

                End Using

            Catch ex As Excepcion.NegocioExcepcion
                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = ex.Codigo, .descripcion = ex.Descricao})
                Throw ex
            End Try

            Return New List(Of ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion)

        End Function

        Private Shared Function cargarRecuperarTransaccionesFechas(dtTransaccion As DataTable,
                                                                   obtenerIAC As Boolean,
                                                                   codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno),
                                                                   GMTHoraLocalCalculado As Double,
                                                                   GMTMinutoLocalCalculado As Double,
                                                                   ValidacionesError As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)) As List(Of ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion)

            Dim lstTransaccion As New List(Of ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion)
            If dtTransaccion IsNot Nothing AndAlso dtTransaccion.Rows.Count > 0 Then

                Dim identificadorAjeno As String = ""
                Dim trabajaConAjeno As Boolean = False

                If codigosAjenos IsNot Nothing AndAlso codigosAjenos.Count > 0 Then

                    identificadorAjeno = codigosAjenos(0).CodigoIdentificador
                    trabajaConAjeno = True

                    codigosAjenos = New ObservableCollection(Of Clases.CodigoAjeno)

                    For Each objRow As DataRow In dtTransaccion.Rows

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DELEGACION_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDELEGACION", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DELEGACION_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDELEGACION", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_PLANTA_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPLANTA", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_PLANTA_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPLANTA", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CLIENTE_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CANAL_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CANAL_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCANAL_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_ORIGEN"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_ORIGEN"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_ORIGEN"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_DESTINO"), GetType(String))})
                        End If

                        If objRow.Table.Columns.Contains("OID_MEDIO_PAGO") Then
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))) Then
                                codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TMEDIO_PAGO", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))})
                            End If
                        End If

                        If objRow.Table.Columns.Contains("OID_DENOMINACION") AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))) Then
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))) Then
                                codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDENOMINACION", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))})
                            End If
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SECTOR_ORI"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_ORI"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSECTOR", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_ORI"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSECTOR", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))) AndAlso Not codigosAjenos.Exists(Function(a) a.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDIVISA", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))})
                        End If

                    Next

                    AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(identificadorAjeno, codigosAjenos)

                End If
                Dim cont As Integer = 0
                For Each objRow As DataRow In dtTransaccion.Rows

                    cont = cont + 1
                    Dim Identificador As String = Util.AtribuirValorObj(objRow("IDENTIFICADOR"), GetType(String))
                    Dim Certificado As String = Util.AtribuirValorObj(objRow("BOL_CERTIFICADO"), GetType(Boolean))
                    Dim Disponible As String = Util.AtribuirValorObj(objRow("BOL_DISPONIBLE"), GetType(Boolean))
                    Dim bolCertificado As Boolean = False
                    Dim bolDisponible As Boolean = False

                    If Not String.IsNullOrWhiteSpace(Certificado) AndAlso (Certificado.Trim().ToUpper = "TRUE" OrElse Certificado.Trim() = "1") Then
                        bolCertificado = True
                    End If

                    If Not String.IsNullOrWhiteSpace(Disponible) AndAlso (Disponible.Trim().ToUpper = "TRUE" OrElse Disponible.Trim() = "1") Then
                        bolDisponible = True
                    End If

                    Dim objTransaccion As ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion = lstTransaccion.FirstOrDefault(Function(a) a.Identificador.Trim().ToUpper() = Identificador.Trim().ToUpper() AndAlso a.Certificado = bolCertificado AndAlso a.Disponible = bolDisponible)

                    If objTransaccion Is Nothing Then
                        objTransaccion = New ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Transacion
                        With objTransaccion

                            .Identificador = Identificador
                            .CodigoExterno = Util.AtribuirValorObj(objRow("COD_EXTERNO"), GetType(String))
                            .CodigoComprobante = Util.AtribuirValorObj(objRow("COD_COMPROBANTE"), GetType(String))


                            Dim dateGestion = Util.AtribuirValorObj(objRow("FYH_GESTION"), GetType(String))
                            .FechaGestion = DateTime.ParseExact(dateGestion, "yyyy-MM-dd HH:mm:sszzz", CultureInfo.CurrentCulture)


                            Dim FechaRealizacion = Util.AtribuirValorObj(objRow("FECHA_RESOLUSION"), GetType(String))
                            .FechaRealizacion = DateTime.ParseExact(FechaRealizacion, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.CurrentCulture)

                            .IdFormulario = Util.AtribuirValorObj(objRow("COD_FORMULARIO"), GetType(String))
                            .NombreFormulario = Util.AtribuirValorObj(objRow("DES_FORMULARIO"), GetType(String))
                            .Notificado = Util.AtribuirValorObj(objRow("BOL_NOTIFICADO"), GetType(Boolean))
                            .Acreditado = Util.AtribuirValorObj(objRow("BOL_ACREDITADO"), GetType(Boolean))
                            If .Acreditado Then

                                Dim d As String = Util.AtribuirValorObj(objRow("FYH_ACREDITACION"), GetType(String))

                                If Not String.IsNullOrWhiteSpace(d) Then
                                    Dim FechaAcreditacion = Util.AtribuirValorObj(objRow("FYH_ACREDITACION"), GetType(String))
                                    .FechaAcreditacion = DateTime.ParseExact(FechaAcreditacion, "yyyy-MM-dd HH:mm:sszzz", CultureInfo.CurrentCulture).ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz")
                                End If

                            End If
                            If Certificado IsNot Nothing Then
                                .Certificado = Certificado
                            End If

                            If Disponible IsNot Nothing Then
                                .Disponible = Disponible
                            End If
                            .CuentaOrigen = New ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Cuenta
                            With .CuentaOrigen
                                Dim codCliente As String = Util.AtribuirValorObj(objRow("COD_CLIENTE_ORIGEN"), GetType(String))
                                Dim desCliente = Util.AtribuirValorObj(objRow("DES_CLIENTE_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codCliente) Then
                                    Dim _clienteAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                                    If _clienteAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CLIENTE_ORIGEN"), GetType(String)), "GEPR_TCLIENTE")})
                                        Exit For
                                    End If
                                    codCliente = _clienteAjeno.Codigo
                                    desCliente = _clienteAjeno.Descripcion
                                End If

                                .CodigoCliente = codCliente
                                .DescripcionCliente = desCliente
                                Dim codSubCliente As String = Util.AtribuirValorObj(objRow("COD_SUBCLIENTE_ORIGEN"), GetType(String))
                                Dim desSubCliente = Util.AtribuirValorObj(objRow("DES_SUBCLIENTE_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSubCliente) Then
                                    Dim _subClienteAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE")
                                    If _subClienteAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_ORIGEN"), GetType(String)), "GEPR_TSUBCLIENTE")})
                                        Exit For
                                    End If
                                    codSubCliente = _subClienteAjeno.Codigo
                                    desSubCliente = _subClienteAjeno.Descripcion
                                End If
                                .CodigoSubCliente = codSubCliente
                                .DescripcionSubCliente = desSubCliente
                                Dim codPuntoServicio As String = Util.AtribuirValorObj(objRow("COD_PUNTO_SERVICIO_ORIGEN"), GetType(String))
                                Dim desPuntoServicio = Util.AtribuirValorObj(objRow("DES_PUNTO_SERVICIO_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codPuntoServicio) Then
                                    Dim _puntoServicioAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO")
                                    If _puntoServicioAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_ORIGEN"), GetType(String)), "GEPR_TPUNTO_SERVICIO")})
                                        Exit For
                                    End If
                                    codPuntoServicio = _puntoServicioAjeno.Codigo
                                    desPuntoServicio = _puntoServicioAjeno.Descripcion
                                End If
                                .CodigoPuntoServicio = codPuntoServicio
                                .DescripcionPuntoServicio = desPuntoServicio
                                Dim codDelegacion As String = Util.AtribuirValorObj(objRow("COD_DELEGACION_ORIGEN"), GetType(String))
                                Dim desDelegacion = Util.AtribuirValorObj(objRow("DES_DELEGACION_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codDelegacion) Then
                                    Dim _delegacionAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDELEGACION")
                                    If _delegacionAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DELEGACION_ORIGEN"), GetType(String)), "GEPR_TDELEGACION")})
                                        Exit For
                                    End If
                                    codDelegacion = _delegacionAjeno.Codigo
                                    desDelegacion = _delegacionAjeno.Descripcion
                                End If
                                .CodigoDelegacion = codDelegacion
                                .DescripcionDelegacion = desDelegacion
                                Dim codPlanta As String = Util.AtribuirValorObj(objRow("COD_PLANTA_ORIGEN"), GetType(String))
                                Dim desPlanta = Util.AtribuirValorObj(objRow("DES_PLANTA_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codPlanta) Then
                                    Dim _plantaAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPLANTA")
                                    If _plantaAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_PLANTA_ORIGEN"), GetType(String)), "GEPR_TPLANTA")})
                                        Exit For
                                    End If
                                    codPlanta = _plantaAjeno.Codigo
                                    desPlanta = _plantaAjeno.Descripcion
                                End If
                                .CodigoPlanta = codPlanta
                                .DescripcionPlanta = desPlanta
                                Dim codSector As String = Util.AtribuirValorObj(objRow("COD_SECTOR_ORI"), GetType(String))
                                Dim desSector = Util.AtribuirValorObj(objRow("DES_SECTOR_ORI"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSector) Then
                                    Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_ORI"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                                    If _sectorAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SECTOR_ORI"), GetType(String)), "GEPR_TSECTOR")})
                                        Exit For
                                    End If
                                    codSector = _sectorAjeno.Codigo
                                    desSector = _sectorAjeno.Descripcion
                                End If
                                .CodigoSector = codSector
                                .DescripcionSector = desSector
                                Dim codCanal As String = Util.AtribuirValorObj(objRow("COD_CANAL_ORIGEN"), GetType(String))
                                Dim desCanal = Util.AtribuirValorObj(objRow("DES_CANAL_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codCanal) Then
                                    Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                                    If _canalAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CANAL_ORIGEN"), GetType(String)), "GEPR_TCANAL")})
                                        Exit For
                                    End If
                                    codCanal = _canalAjeno.Codigo
                                    desCanal = _canalAjeno.Descripcion
                                End If
                                .CodigoCanal = codCanal
                                .DescripcionCanal = desCanal
                                Dim codSubCanal As String = Util.AtribuirValorObj(objRow("COD_SUBCANAL_ORIGEN"), GetType(String))
                                Dim desSubCanal = Util.AtribuirValorObj(objRow("DES_SUBCANAL_ORIGEN"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSubCanal) Then
                                    Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_ORIGEN"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                                    If _subCanalAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCANAL_ORIGEN"), GetType(String)), "GEPR_TSUBCANAL")})
                                        Exit For
                                    End If
                                    codSubCanal = _subCanalAjeno.Codigo
                                    desSubCanal = _subCanalAjeno.Descripcion
                                End If
                                .CodigoSubCanal = codSubCanal
                                .DescripcionSubCanal = desSubCanal

                            End With
                            .CuentaDestino = New ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.Cuenta
                            With .CuentaDestino
                                Dim codCliente As String = Util.AtribuirValorObj(objRow("COD_CLIENTE_DESTINO"), GetType(String))
                                Dim desCliente = Util.AtribuirValorObj(objRow("DES_CLIENTE_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codCliente) Then
                                    Dim _clienteAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                                    If _clienteAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CLIENTE_DESTINO"), GetType(String)), "GEPR_TCLIENTE")})
                                        Exit For
                                    End If
                                    codCliente = _clienteAjeno.Codigo
                                    desCliente = _clienteAjeno.Descripcion
                                End If
                                .CodigoCliente = codCliente
                                .DescripcionCliente = desCliente
                                Dim codSubCliente As String = Util.AtribuirValorObj(objRow("COD_SUBCLIENTE_DESTINO"), GetType(String))
                                Dim desSubCliente = Util.AtribuirValorObj(objRow("DES_SUBCLIENTE_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSubCliente) Then
                                    Dim _subClienteAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE")
                                    If _subClienteAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCLIENTE_DESTINO"), GetType(String)), "GEPR_TSUBCLIENTE")})
                                        Exit For
                                    End If
                                    codSubCliente = _subClienteAjeno.Codigo
                                    desSubCliente = _subClienteAjeno.Descripcion
                                End If
                                .CodigoSubCliente = codSubCliente
                                .DescripcionSubCliente = desSubCliente
                                Dim codPuntoServicio As String = Util.AtribuirValorObj(objRow("COD_PUNTO_SERVICIO_DESTINO"), GetType(String))
                                Dim desPuntoServicio = Util.AtribuirValorObj(objRow("DES_PUNTO_SERVICIO_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codPuntoServicio) Then
                                    Dim _puntoServicioAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO")
                                    If _puntoServicioAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_PUNTO_SERVICIO_DESTINO"), GetType(String)), "GEPR_TPUNTO_SERVICIO")})
                                        Exit For
                                    End If
                                    codPuntoServicio = _puntoServicioAjeno.Codigo
                                    desPuntoServicio = _puntoServicioAjeno.Descripcion
                                End If
                                .CodigoPuntoServicio = codPuntoServicio
                                .DescripcionPuntoServicio = desPuntoServicio
                                Dim codDelegacion As String = Util.AtribuirValorObj(objRow("COD_DELEGACION_DESTINO"), GetType(String))
                                Dim desDelegacion = Util.AtribuirValorObj(objRow("DES_DELEGACION_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codDelegacion) Then
                                    Dim _delegacionAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DELEGACION_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDELEGACION")
                                    If _delegacionAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DELEGACION_DESTINO"), GetType(String)), "GEPR_TDELEGACION")})
                                        Exit For
                                    End If
                                    codDelegacion = _delegacionAjeno.Codigo
                                    desDelegacion = _delegacionAjeno.Descripcion
                                End If
                                .CodigoDelegacion = codDelegacion
                                .DescripcionDelegacion = desDelegacion
                                Dim codPlanta As String = Util.AtribuirValorObj(objRow("COD_PLANTA_DESTINO"), GetType(String))
                                Dim desPlanta = Util.AtribuirValorObj(objRow("DES_PLANTA_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codPlanta) Then
                                    Dim _plantaAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PLANTA_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPLANTA")
                                    If _plantaAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_PLANTA_DESTINO"), GetType(String)), "GEPR_TPLANTA")})
                                        Exit For
                                    End If
                                    codPlanta = _plantaAjeno.Codigo
                                    desPlanta = _plantaAjeno.Descripcion
                                End If
                                .CodigoPlanta = codPlanta
                                .DescripcionPlanta = desPlanta
                                Dim codSector As String = Util.AtribuirValorObj(objRow("COD_SECTOR_DESTINO"), GetType(String))
                                Dim desSector = Util.AtribuirValorObj(objRow("DES_SECTOR_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSector) Then
                                    Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                                    If _sectorAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SECTOR_DESTINO"), GetType(String)), "GEPR_TSECTOR")})
                                        Exit For
                                    End If
                                    codSector = _sectorAjeno.Codigo
                                    desSector = _sectorAjeno.Descripcion
                                End If
                                .CodigoSector = codSector
                                .DescripcionSector = desSector
                                Dim codCanal As String = Util.AtribuirValorObj(objRow("COD_CANAL_DESTINO"), GetType(String))
                                Dim desCanal = Util.AtribuirValorObj(objRow("DES_CANAL_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codCanal) Then
                                    Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                                    If _canalAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CANAL_DESTINO"), GetType(String)), "GEPR_TCANAL")})
                                        Exit For
                                    End If
                                    codCanal = _canalAjeno.Codigo
                                    desCanal = _canalAjeno.Descripcion
                                End If
                                .CodigoCanal = codCanal
                                .DescripcionCanal = desCanal
                                Dim codSubCanal As String = Util.AtribuirValorObj(objRow("COD_SUBCANAL_DESTINO"), GetType(String))
                                Dim desSubCanal = Util.AtribuirValorObj(objRow("DES_SUBCANAL_DESTINO"), GetType(String))

                                If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codSubCanal) Then
                                    Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                                    If _subCanalAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCANAL_DESTINO"), GetType(String)), "GEPR_TSUBCANAL")})
                                        Exit For
                                    End If
                                    codSubCanal = _subCanalAjeno.Codigo
                                    desSubCanal = _subCanalAjeno.Descripcion
                                End If
                                .CodigoSubCanal = codSubCanal
                                .DescripcionSubCanal = desSubCanal

                            End With
                            .Divisas = New List(Of ContractoServicio.Contractos.Integracion.Comon.Divisa)
                            .MediosPago = New List(Of ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.MedioPago)
                            .CamposAdicionales = New List(Of ContractoServicio.Contractos.Integracion.Comon.CampoAdicional)

                        End With
                        lstTransaccion.Add(objTransaccion)

                    End If

                    Dim codIsoDivisa As String = Util.AtribuirValorObj(objRow("COD_ISO_DIVISA"), GetType(String))
                    If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codIsoDivisa) Then
                        Dim _divisaAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDIVISA")
                        If _divisaAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String)), "GEPR_TDIVISA")})
                            Exit For
                        End If
                        codIsoDivisa = _divisaAjeno.Codigo
                    End If

                    Dim objDivisa As ContractoServicio.Contractos.Integracion.Comon.Divisa = objTransaccion.Divisas.Find(Function(a) a.codigoDivisa = codIsoDivisa)

                    If codIsoDivisa IsNot Nothing Then

                        If objDivisa Is Nothing Then
                            objDivisa = New ContractoServicio.Contractos.Integracion.Comon.Divisa

                            With objDivisa

                                .codigoDivisa = codIsoDivisa
                                .importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                                .denominaciones = New List(Of ContractoServicio.Contractos.Integracion.Comon.Denominacion)

                            End With
                            objTransaccion.Divisas.Add(objDivisa)
                        Else
                            With objDivisa
                                .importe += Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                            End With
                        End If
                    End If

                    If objDivisa IsNot Nothing Then

                        objDivisa.importe = Double.Parse(objDivisa.importe.ToString("N2"))
                    End If

                    Dim codDenominacion As String = String.Empty
                    If objRow.Table.Columns.Contains("COD_DENOMINACION") Then
                        codDenominacion = Util.AtribuirValorObj(objRow("COD_DENOMINACION"), GetType(String))
                    End If
                    If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codDenominacion) Then
                        Dim _denominacionAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDENOMINACION")
                        If _denominacionAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String)), "GEPR_TDENOMINACION")})
                            Exit For
                        End If
                        codDenominacion = _denominacionAjeno.Codigo
                    End If

                    If Not String.IsNullOrEmpty(codDenominacion) Then

                        Dim objDenominacion As ContractoServicio.Contractos.Integracion.Comon.Denominacion = objDivisa.denominaciones.Find(Function(a) a.codigoDenominacion = codDenominacion)

                        If objDenominacion Is Nothing Then
                            objDenominacion = New ContractoServicio.Contractos.Integracion.Comon.Denominacion

                            With objDenominacion

                                .codigoDenominacion = codDenominacion
                                .importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                                .importe = Double.Parse(.importe.ToString("N2"))
                                .cantidad = Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))
                            End With
                            objDivisa.denominaciones.Add(objDenominacion)
                        Else
                            With objDenominacion

                                .importe += Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                                .importe = Double.Parse(.importe.ToString("N2"))
                                .cantidad += Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))

                            End With
                        End If

                    End If

                    Dim codMedioPago As String = String.Empty
                    If objRow.Table.Columns.Contains("COD_MEDIO_PAGO") Then
                        codMedioPago = Util.AtribuirValorObj(objRow("COD_MEDIO_PAGO"), GetType(String))
                    End If
                    If trabajaConAjeno AndAlso Not String.IsNullOrEmpty(codMedioPago) Then
                        Dim _medioPagoAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TMEDIO_PAGO")
                        If _medioPagoAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String)), "GEPR_TMEDIO_PAGO")})
                            Exit For
                        End If
                        codMedioPago = _medioPagoAjeno.Codigo
                    End If

                    If Not String.IsNullOrEmpty(codMedioPago) Then

                        Dim objMedioPago As ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.MedioPago = objTransaccion.MediosPago.Find(Function(a) a.CodigoMedioPago = codMedioPago)

                        If objMedioPago Is Nothing Then
                            objMedioPago = New ContractoServicio.Contractos.Integracion.RecuperarTransaccionesFechas.MedioPago

                            With objMedioPago

                                .CodigoIsoDivisa = codIsoDivisa
                                .CodigoMedioPago = codMedioPago
                                .CodigoTipoMedioPago = Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String))
                                .DescripcionMedioPago = Util.AtribuirValorObj(objRow("DES_MEDIO_PAGO"), GetType(String))
                                .Importe = Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                                .Importe = Double.Parse(.Importe.ToString("N2"))
                                .Unidades = Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))

                            End With
                            objTransaccion.MediosPago.Add(objMedioPago)
                        Else
                            With objMedioPago

                                .Importe += Util.AtribuirValorObj(objRow("IMPORTE"), GetType(Decimal))
                                .Importe = Double.Parse(.Importe.ToString("N2"))
                                .Unidades += Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))

                            End With
                        End If

                    End If

                Next

                If lstTransaccion IsNot Nothing AndAlso lstTransaccion.Count > 0 AndAlso obtenerIAC Then
                    Dim lstDocumento As List(Of String) = lstTransaccion.Select(Function(a) a.Identificador).ToList()
                    Dim dtTerminos As DataTable = AccesoDatos.Genesis.ValorTerminoDocumento.ObtenerValorTerminoDocumento_v2(lstDocumento)

                    If dtTerminos IsNot Nothing AndAlso dtTerminos.Rows.Count > 0 Then

                        For Each objTransaccion In lstTransaccion

                            If objTransaccion.CamposAdicionales Is Nothing Then
                                objTransaccion.CamposAdicionales = New List(Of ContractoServicio.Contractos.Integracion.Comon.CampoAdicional)
                            End If

                            Dim lstRowsIAC = dtTerminos.Select(String.Format("OID_DOCUMENTO = '{0}'", objTransaccion.Identificador))
                            If lstRowsIAC IsNot Nothing AndAlso lstRowsIAC.Count > 0 Then

                                For Each objRowIAC In lstRowsIAC

                                    Dim desTermino As String = Util.AtribuirValorObj(objRowIAC("COD_TERMINO"), GetType(String))

                                    If Not String.IsNullOrEmpty(desTermino) Then

                                        Dim objCampoAdicional As ContractoServicio.Contractos.Integracion.Comon.CampoAdicional = objTransaccion.CamposAdicionales.Find(Function(a) a.nombre = desTermino)

                                        If objCampoAdicional Is Nothing Then
                                            objCampoAdicional = New ContractoServicio.Contractos.Integracion.Comon.CampoAdicional

                                            With objCampoAdicional

                                                .nombre = desTermino
                                                .valor = Util.AtribuirValorObj(objRowIAC("DES_VALOR"), GetType(String))

                                            End With

                                            objTransaccion.CamposAdicionales.Add(objCampoAdicional)
                                        End If

                                    End If

                                Next

                            End If

                        Next

                    End If

                End If

            End If

            If ValidacionesError IsNot Nothing AndAlso ValidacionesError.Count > 0 Then
                Throw New Excepcion.NegocioExcepcion("codigoAjeno")
            End If
            Return lstTransaccion
        End Function

#End Region

#Region "[ACTUALIZAR]"

        ''' <summary>
        ''' Atualiza a transação
        ''' </summary>
        ''' <param name="IdentificadorCertificado"></param>
        ''' <param name="GmtActualizacion"></param>
        ''' <param name="DesUsuario"></param>
        ''' <param name="Objtransacion"></param>
        ''' <remarks></remarks>
        Public Shared Sub DesasociarTransaccionCertificado(IdentificadorCertificado As String, GmtActualizacion As DateTime,
                                                           DesUsuario As String, Objtransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoActualizarOidCertificado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO_NUEVO", ProsegurDbType.Objeto_Id, DBNull.Value))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, DesUsuario))

            Objtransacion.AdicionarItemTransacao(cmd)
        End Sub

        Public Shared Sub ActualizarFechaHoraPlanCertificacion(identificadorDocumento As String,
                                                               FechaHoraPlanificacionCertificacion As DateTime,
                                                               usuario As String)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivo_ActualizarFechaHoraPlanCertificacion)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, usuario))

                    If FechaHoraPlanificacionCertificacion = Date.MinValue Then
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
                    Else
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, FechaHoraPlanificacionCertificacion))
                    End If

                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, command)

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

        End Sub

#End Region

#Region "[INSERIR]"

        ''' <summary>
        ''' Insere um transação de efectivo.
        ''' La entidad TRANSACCION_EFECTIVO contiene los datos de la Transacciones realizadas, por Efectivo, para una Cuenta.
        ''' </summary>
        ''' <param name="identificadorDivisa">Es una referencia a la entidad de DIVISA</param>
        ''' <param name="identificadorDenominacion">Es una referencia a la entidad de DENOMINACION</param>
        ''' <param name="identificadorUnidadMedida">Es una referencia a la entidad de UNIDAD MEDIDA</param>
        ''' <param name="codigoNivelDetalhe">Indica el NIVEL de DETALLE: D=Detallado; T=total</param>
        ''' <param name="codigoTipoEfectivoTotal">Cuando el NIVEL de DETALLE sea T, será utilizado para indicar lo tipo de Total: B=Billete; M=Moneda</param>
        ''' <param name="identificadorCalidad">Indica la Calidad de la Denominación: E=Excelente; B=Buena; P=Pésimo</param>
        ''' <param name="numImporte">Indica el valor del Saldo</param>
        ''' <param name="cantidad">Cantidad de la denominación</param>
        ''' <param name="codigoMigracion">Identificación, normalmente una clave primaria, en la tabla que originó la migración de datos</param>
        ''' <remarks></remarks>
        Public Shared Sub TransaccionEfectivoInserir(permiteLlegarSaldoNegativo As Boolean,
                                                    objTransaccion As Clases.Transaccion,
                                                    identificadorDivisa As String,
                                                    identificadorDenominacion As String,
                                                    identificadorUnidadMedida As String,
                                                    codigoNivelDetalhe As String,
                                                    codigoTipoEfectivoTotal As String,
                                                    identificadorCalidad As String,
                                                    numImporte As Decimal,
                                                    cantidad As Int64,
                                                    codigoMigracion As String,
                                                    usuario As String,
                                                    Optional anulado As Boolean = False)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            objTransaccion.Identificador = Guid.NewGuid.ToString

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objTransaccion.Documento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, objTransaccion.Cuenta.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, objTransaccion.CuentaSaldo.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, If(objTransaccion.TipoSaldo = Enumeradores.TipoSaldo.Disponible, True, False)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCUMENTO", ProsegurDbType.Descricao_Curta, objTransaccion.EstadoDocumento.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_SITIO", ProsegurDbType.Descricao_Curta, objTransaccion.TipoSitio.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MOVIMIENTO", ProsegurDbType.Descricao_Curta, objTransaccion.TipoMovimiento.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OBS_TRANSACCION", ProsegurDbType.Observacao_Longa, objTransaccion.Observaciones))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadMedida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, codigoNivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, codigoTipoEfectivoTotal))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadorCalidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, codigoMigracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CONTRA_MOVIMIENTO", ProsegurDbType.Logico, anulado))

            If objTransaccion.Certificado IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, objTransaccion.Certificado.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If objTransaccion.Documento.FechaHoraPlanificacionCertificacion <> Date.MinValue AndAlso objTransaccion.Cuenta IsNot Nothing AndAlso objTransaccion.Cuenta.Sector IsNot Nothing AndAlso objTransaccion.Cuenta.Sector.Delegacion IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, objTransaccion.Documento.FechaHoraPlanificacionCertificacion.QuieroGrabarGMTZeroEnLaBBDD(objTransaccion.Cuenta.Sector.Delegacion)))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            'Atualiza o saldo para essa transação.
            SaldoEfectivo.SaldoEfectivoAtualizar(objTransaccion.Identificador, usuario, permiteLlegarSaldoNegativo)

        End Sub

        Public Shared Sub TransaccionEfectivoInserir_v2(permiteLlegarSaldoNegativo As Boolean,
                                                        objTransaccion As Clases.Transaccion,
                                                        identificadorDocumento As String,
                                                        identificadorCuentaMovimento As String,
                                                        identificadorCuentaSaldo As String,
                                                        FechaHoraPlanificacionCertificacion As DateTime,
                                                        identificadorDivisa As String,
                                                        identificadorDenominacion As String,
                                                        identificadorUnidadMedida As String,
                                                        codigoNivelDetalhe As String,
                                                        codigoTipoEfectivoTotal As String,
                                                        identificadorCalidad As String,
                                                        numImporte As Decimal,
                                                        cantidad As Int64,
                                                        codigoMigracion As String,
                                                        usuario As String,
                                               Optional anulado As Boolean = False)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            objTransaccion.Identificador = Guid.NewGuid.ToString

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, identificadorDocumento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA", ProsegurDbType.Objeto_Id, identificadorCuentaMovimento))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Objeto_Id, identificadorCuentaSaldo))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, If(objTransaccion.TipoSaldo = Enumeradores.TipoSaldo.Disponible, True, False)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCUMENTO", ProsegurDbType.Descricao_Curta, objTransaccion.EstadoDocumento.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_SITIO", ProsegurDbType.Descricao_Curta, objTransaccion.TipoSitio.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MOVIMIENTO", ProsegurDbType.Descricao_Curta, objTransaccion.TipoMovimiento.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OBS_TRANSACCION", ProsegurDbType.Observacao_Longa, objTransaccion.Observaciones))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Longa, usuario))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Objeto_Id, identificadorDivisa))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, identificadorDenominacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_UNIDAD_MEDIDA", ProsegurDbType.Objeto_Id, identificadorUnidadMedida))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, codigoNivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_EFECTIVO_TOTAL", ProsegurDbType.Descricao_Curta, codigoTipoEfectivoTotal))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CALIDAD", ProsegurDbType.Descricao_Curta, identificadorCalidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Inteiro_Longo, cantidad))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_MIGRACION", ProsegurDbType.Descricao_Longa, codigoMigracion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CONTRA_MOVIMIENTO", ProsegurDbType.Logico, anulado))

            If objTransaccion.Certificado IsNot Nothing Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, objTransaccion.Certificado.Identificador))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, Nothing))
            End If

            If FechaHoraPlanificacionCertificacion <> Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, FechaHoraPlanificacionCertificacion))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            'Atualiza o saldo para essa transação.
            SaldoEfectivo.SaldoEfectivoAtualizar(objTransaccion.Identificador, usuario, permiteLlegarSaldoNegativo)

        End Sub

        ''' <summary>
        ''' Insere uma nova transação de estorno
        ''' </summary>
        ''' <param name="objTransaccion"></param>
        ''' <remarks></remarks>
        Public Shared Function TransaccionEfectivoEstornoSustituicion(objTransaccion As Clases.Transaccion) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim novaTransaccionEstorno As String = Guid.NewGuid.ToString
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionEfectivoEstornoSustituicion)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO_ATUAL", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO", ProsegurDbType.Objeto_Id, novaTransaccionEstorno))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Objeto_Id, objTransaccion.Documento.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_DOCUMENTO", ProsegurDbType.Descricao_Curta, objTransaccion.EstadoDocumento.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_SITIO", ProsegurDbType.Descricao_Curta, objTransaccion.TipoSitio.RecuperarValor))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "USUARIO", ProsegurDbType.Descricao_Longa, objTransaccion.UsuarioCreacion))

            If objTransaccion.Documento.FechaHoraPlanificacionCertificacion <> Date.MinValue Then
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, objTransaccion.FechaHoraPlanificacionCertificacion))
            Else
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Nothing))
            End If

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            Return novaTransaccionEstorno
        End Function

#End Region

    End Class

End Namespace