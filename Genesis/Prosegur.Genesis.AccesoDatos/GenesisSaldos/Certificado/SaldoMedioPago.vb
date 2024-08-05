Imports Prosegur.DbHelper
Imports System.Text

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    ''' <summary>
    ''' Classe CertificadoSaldoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SaldoMedioPago

#Region "[CONSULTAR]"

        ''' <summary>
        ''' Recupera o saldo do certificado
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 28/05/2013 - Criado
        ''' </history>
        Public Shared Function RecuperarSaldoCertificado(IdentificadorCertificado As String) As CSCertificacion.CuentaColeccion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoMedioPagoRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, IdentificadorCertificado))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            Dim cuentas As CSCertificacion.CuentaColeccion = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                cuentas = New CSCertificacion.CuentaColeccion
                Dim Cuenta As New CSCertificacion.Cuenta
                Dim OidCuenta As String = String.Empty

                For Each dr In dt.Rows

                    OidCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))

                    Cuenta = (From cn In cuentas Where cn.IdentificadorCuenta = OidCuenta).FirstOrDefault

                    If Cuenta Is Nothing Then

                        cuentas.Add(New CSCertificacion.Cuenta With { _
                                    .IdentificadorCuenta = Util.AtribuirValorObj(OidCuenta, GetType(String)), _
                                    .SaldosMedioPagos = New CSCertificacion.CertificadoSaldoMedioPagoColeccion})

                        Cuenta = (From cn In cuentas Where cn.IdentificadorCuenta = OidCuenta).FirstOrDefault

                    End If

                    Dim objCertificadoSaldoMedioPago As New CSCertificacion.CertificadoSaldoMedioPago

                    objCertificadoSaldoMedioPago.BolDisponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean))
                    objCertificadoSaldoMedioPago.CodigoNivelDetalle = Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String))
                    objCertificadoSaldoMedioPago.NelCantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                    objCertificadoSaldoMedioPago.NelCantidadInicial = Util.AtribuirValorObj(dr("NEL_CANTIDAD_ACUMULADO"), GetType(Int64))
                    objCertificadoSaldoMedioPago.NelCantidadFinal = objCertificadoSaldoMedioPago.NelCantidad + objCertificadoSaldoMedioPago.NelCantidadInicial
                    objCertificadoSaldoMedioPago.NumImporte = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                    objCertificadoSaldoMedioPago.NumImporteInicial = Util.AtribuirValorObj(dr("NUM_IMPORTE_ACUMULADO"), GetType(Decimal))
                    objCertificadoSaldoMedioPago.CodigoMedioPago = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String))
                    objCertificadoSaldoMedioPago.IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    objCertificadoSaldoMedioPago.CodigoTipoMedioPago = Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    objCertificadoSaldoMedioPago.IdentificadorCertificadoAnterior = Util.AtribuirValorObj(dr("OID_CERTIFICADO_ANTERIOR"), GetType(String))
                    objCertificadoSaldoMedioPago.NumImporteFinal = objCertificadoSaldoMedioPago.NumImporte + objCertificadoSaldoMedioPago.NumImporteInicial
                    objCertificadoSaldoMedioPago.IdentificadorUnidadMedida = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))

                    Cuenta.SaldosMedioPagos.Add(objCertificadoSaldoMedioPago)
                Next

            End If

            Return cuentas
        End Function

        ''' <summary>
        ''' Recupera o saldo
        ''' </summary>
        ''' <param name="IdentificadorSaldo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarSaldo(IdentificadorSaldo As String) As CSCertificacion.CertificadoSaldoMedioPago

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoMedioPagoByOidSaldo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERT_SALDO_MEDIO_PAGO", ProsegurDbType.Objeto_Id, IdentificadorSaldo))

            Dim objSaldo As CSCertificacion.CertificadoSaldoMedioPago = Nothing

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objSaldo = New CSCertificacion.CertificadoSaldoMedioPago

                With objSaldo
                    .BolDisponible = Util.AtribuirValorObj(dt.Rows(0)("BOL_DISPONIBLE"), GetType(Boolean))
                    .CodigoMedioPago = Util.AtribuirValorObj(dt.Rows(0)("COD_MEDIO_PAGO"), GetType(String))
                    .CodigoNivelDetalle = Util.AtribuirValorObj(dt.Rows(0)("COD_NIVEL_DETALLE"), GetType(String))
                    .NelCantidad = Util.AtribuirValorObj(dt.Rows(0)("NEL_CANTIDAD"), GetType(Int64))
                    .NelCantidadInicial = Util.AtribuirValorObj(dt.Rows(0)("NEL_CANTIDAD_ACUMULADO"), GetType(Int64))
                    .NelCantidadFinal = .NelCantidad + .NelCantidadInicial
                    .NumImporteInicial = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE_ACUMULADO"), GetType(Decimal))
                    .NumImporte = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE"), GetType(Decimal))
                    .NumImporteFinal = .NumImporte + .NumImporteInicial
                    .IdentificadorSaldoMedioPago = Util.AtribuirValorObj(dt.Rows(0)("OID_CERT_SALDO_MEDIO_PAGO"), GetType(String))
                    .IdentificadorDivisa = Util.AtribuirValorObj(dt.Rows(0)("OID_DIVISA"), GetType(String))
                    .IdentificadorCertificadoAnterior = Util.AtribuirValorObj(dt.Rows(0)("OID_CERTIFICADO_ANTERIOR"), GetType(String))
                    .IdentificadorUnidadMedida = Util.AtribuirValorObj(dt.Rows(0)("OID_UNIDAD_MEDIDA"), GetType(String))
                End With

            End If

            Return objSaldo
        End Function

#End Region

#Region "[DELETAR]"

        ''' <summary>
        ''' Deleta o saldo do certificado
        ''' </summary>
        ''' <param name="OidCertificado"></param>
        ''' <param name="objTransacion"></param>
        ''' <remarks></remarks>
        Public Shared Sub DeletarSaldo(OidCertificado As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoMedioPagoDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, OidCertificado))

            objTransacion.AdicionarItemTransacao(cmd)
        End Sub

#End Region

    End Class

End Namespace