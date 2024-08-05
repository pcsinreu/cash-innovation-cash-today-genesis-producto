Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports System.Text

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe TransaccionMedioPago
    ''' </summary>
    Public Class TransaccionMedioPago

#Region "[CONSULTAR]"

        Public Shared Function HayTransacciones(Peticion As CSCertificacion.DatosCertificacion.Peticion) As Boolean

            Dim respuesta As Boolean = False
            Dim estadosPosibles As New List(Of String)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Rechazado.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Sustituido.RecuperarValor)
            estadosPosibles.Add(Enumeradores.EstadoDocumento.Confirmado.RecuperarValor)

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoHayTransacciones)
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
        ''' Retorna a transação do medio pago
        ''' </summary>
        ''' <param name="IdentificadorTransaccion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RetornarTransaccionMedioPago(IdentificadorTransaccion As String) As CSCertificacion.TransaccionMedioPago

            Dim transaccion As CSCertificacion.TransaccionMedioPago = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoRecuperar)
                    command.CommandType = CommandType.Text

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdentificadorTransaccion))

                    Dim dtTransaccion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dtTransaccion IsNot Nothing AndAlso dtTransaccion.Rows.Count > 0 Then

                        transaccion = New CSCertificacion.TransaccionMedioPago

                        With transaccion
                            .BolDisponible = Util.AtribuirValorObj(dtTransaccion.Rows(0)("BOL_DISPONIBLE"), GetType(Boolean))
                            .CodEstadoDocumento = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_ESTADO_DOCUMENTO"), GetType(String))
                            .CodMedioPago = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_MEDIO_PAGO"), GetType(String))
                            .CodNivelDetalle = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_NIVEL_DETALLE"), GetType(String))
                            .CodTipoMedioPago = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_TIPO_MEDIO_PAGO"), GetType(String))
                            .CodTipoMovimiento = Util.AtribuirValorObj(dtTransaccion.Rows(0)("COD_TIPO_MOVIMIENTO"), GetType(String))
                            .NelCantidad = Util.AtribuirValorObj(dtTransaccion.Rows(0)("NEL_CANTIDAD"), GetType(Decimal))
                            .NumImporte = Util.AtribuirValorObj(dtTransaccion.Rows(0)("NUM_IMPORTE"), GetType(Decimal))
                            .IdentificadorTransaccionMedioPago = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_TRANSACCION_MEDIO_PAGO"), GetType(String))
                            .IdentificadorDivisa = Util.AtribuirValorObj(dtTransaccion.Rows(0)("OID_DIVISA"), GetType(String))
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
        Public Shared Function RecuperarTransaccionMedioPagoParaEstorno(identificadorDocumento As String, estadoDocumento As String) As List(Of Clases.Transaccion)

            Dim transacciones As List(Of Clases.Transaccion) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoRecuperarParaEstorno)
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
                                .Identificador = Util.AtribuirValorObj(dr("OID_TRANSACCION_MEDIO_PAGO"), GetType(String))
                                .Documento = New Clases.Documento() With {.Identificador = Util.AtribuirValorObj(dr("OID_DOCUMENTO"), GetType(String)), .FechaHoraPlanificacionCertificacion = Util.AtribuirValorObj(dtTransaccion.Rows(0)("FYH_PLAN_CERTIFICACION"), GetType(DateTime))}
                                .TipoSitio = RecuperarEnum(Of Enumeradores.TipoSitio)(dr("COD_TIPO_SITIO").ToString)
                                .TipoSaldo = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean))
                                .EstadoDocumento = RecuperarEnum(Of Enumeradores.EstadoDocumento)(dr("COD_ESTADO_DOCUMENTO").ToString)
                                .TipoMovimiento = RecuperarEnum(Of Enumeradores.TipoMovimiento)(dr("COD_TIPO_MOVIMIENTO").ToString)
                                .Observaciones = Util.AtribuirValorObj(dr("OBS_TRANSACCION"), GetType(String))
                                .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
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
        Public Shared Sub DesassociarTransacionCertificado(IdentificadorCertificado As String, GmtActualizacion As DateTime,
                                                           DesUsuario As String, Objtransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoActualizarOidCertificado)
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

                    command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPago_ActualizarFechaHoraPlanCertificacion)
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

        Public Shared Sub TransaccionMedioPagoInserir_V2(permiteLlegarSaldoNegativo As Boolean, _
                                                        objTransaccion As Clases.Transaccion, _
                                                        identificadorDocumento As String, _
                                                        identificadorCuentaMovimento As String, _
                                                        identificadorCuentaSaldo As String, _
                                                        FechaHoraPlanificacionCertificacion As DateTime, _
                                                        identificadorDivisa As String, _
                                                        identificadorMedioPago As String, _
                                                        codigoNivelDetalhe As String, _
                                                        codigoTipoMedioPago As String, _
                                                        identificadorCalidad As String, _
                                                        numImporte As Decimal, _
                                                        cantidad As Int64, _
                                                        codigoMigracion As String, _
                                                        usuario As String, _
                                               Optional anulado As Boolean = False)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            objTransaccion.Identificador = Guid.NewGuid.ToString

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MEDIO_PAGO", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
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
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, codigoTipoMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, codigoNivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Numero_Decimal, cantidad))
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
            SaldoMedioPago.SaldoMedioPagoAtualizar(objTransaccion.Identificador, usuario, permiteLlegarSaldoNegativo)

        End Sub

        ''' <summary>
        ''' Insere um registro de TransaccionMedioPago.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub TransaccionMedioPagoInserir(permiteLlegarSaldoNegativo As Boolean, _
                                                    objTransaccion As Clases.Transaccion, _
                                                    identificadorDivisa As String, _
                                                    identificadorMedioPago As String, _
                                                    codigoNivelDetalhe As String, _
                                                    codigoTipoMedioPago As String, _
                                                    numImporte As Decimal, _
                                                    cantidad As Decimal, _
                                                    codigoMigracion As String, _
                                                    usuario As String, _
                                                    Optional anulado As Boolean = False)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            objTransaccion.Identificador = Guid.NewGuid.ToString

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MEDIO_PAGO", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
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
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Descricao_Curta, codigoTipoMedioPago))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_NIVEL_DETALLE", ProsegurDbType.Descricao_Curta, codigoNivelDetalhe))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NUM_IMPORTE", ProsegurDbType.Numero_Decimal, numImporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_CANTIDAD", ProsegurDbType.Numero_Decimal, cantidad))
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
            SaldoMedioPago.SaldoMedioPagoAtualizar(objTransaccion.Identificador, usuario, permiteLlegarSaldoNegativo)

        End Sub

        ''' <summary>
        ''' Insere uma nova transação de estorno
        ''' </summary>
        ''' <param name="objTransaccion"></param>
        ''' <remarks></remarks>
        Public Shared Function TransaccionMedioPagoEstornoSustituicion(objTransaccion As Clases.Transaccion) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim novaTransaccionEstorno As String = Guid.NewGuid.ToString
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.TransaccionMedioPagoEstornoSustituicion)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MP_ATUAL", ProsegurDbType.Objeto_Id, objTransaccion.Identificador))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MEDIO_PAGO", ProsegurDbType.Objeto_Id, novaTransaccionEstorno))
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