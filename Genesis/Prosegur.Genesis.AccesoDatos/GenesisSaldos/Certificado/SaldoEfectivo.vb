Imports Prosegur.DbHelper
Imports System.Text

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos.Certificacion

    Public Class SaldoEfectivo

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

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoEfectivoRecuperar)
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
                                    .SaldosEfectivos = New CSCertificacion.CertificadoSaldoEfectivoColeccion})

                        Cuenta = (From cn In cuentas Where cn.IdentificadorCuenta = OidCuenta).FirstOrDefault

                    End If

                    Dim certSaldoEfectivo As New CSCertificacion.CertificadoSaldoEfectivo

                    certSaldoEfectivo.BolDisponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean))
                    certSaldoEfectivo.CodigoDenominacion = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                    certSaldoEfectivo.CodigoIsoDivisa = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                    certSaldoEfectivo.DescripcionDenominacion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                    certSaldoEfectivo.CodigoNivelDetalle = Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String))
                    certSaldoEfectivo.IdentificadorCalidad = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))
                    certSaldoEfectivo.IdentificadorUnidadMedida = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))
                    certSaldoEfectivo.CodigoTipoEfectivo = Util.AtribuirValorObj(dr("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
                    certSaldoEfectivo.DescripcionDivisa = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))
                    certSaldoEfectivo.NelCantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                    certSaldoEfectivo.NelCantidadInicial = Util.AtribuirValorObj(dr("NEL_CANTIDAD_ACUMULADO"), GetType(Int64))
                    certSaldoEfectivo.NelCantidadFinal = certSaldoEfectivo.NelCantidad + certSaldoEfectivo.NelCantidadInicial
                    certSaldoEfectivo.NumImporte = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                    certSaldoEfectivo.NumImporteInicial = Util.AtribuirValorObj(dr("NUM_IMPORTE_ACUMULADO"), GetType(Decimal))
                    certSaldoEfectivo.IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    certSaldoEfectivo.NumImporteFinal = certSaldoEfectivo.NumImporte + certSaldoEfectivo.NumImporteInicial
                    certSaldoEfectivo.IdentificadorCertificadoAnterior = Util.AtribuirValorObj(dr("OID_CERTIFICADO_ANTERIOR"), GetType(String))
                    certSaldoEfectivo.IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                    Cuenta.SaldosEfectivos.Add(certSaldoEfectivo)
                Next

            End If

            Return cuentas
        End Function

        ''' <summary>
        ''' Recupera o saldo do efectivo.
        ''' </summary>
        ''' <param name="IdentificadorSaldo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarSaldoEfevtivo(IdentificadorSaldo As String) As CSCertificacion.CertificadoSaldoEfectivo

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoEfectivoRecuperarByOidSaldo)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERT_SALDO_EFECTIVO", ProsegurDbType.Objeto_Id, IdentificadorSaldo))

            Dim objSaldo As CSCertificacion.CertificadoSaldoEfectivo = Nothing

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objSaldo = New CSCertificacion.CertificadoSaldoEfectivo

                With objSaldo
                    .BolDisponible = Util.AtribuirValorObj(dt.Rows(0)("BOL_DISPONIBLE"), GetType(Boolean))
                    .IdentificadorCalidad = Util.AtribuirValorObj(dt.Rows(0)("OID_CALIDAD"), GetType(String))
                    .IdentificadorUnidadMedida = Util.AtribuirValorObj(dt.Rows(0)("OID_UNIDAD_MEDIDA"), GetType(String))
                    .CodigoDenominacion = Util.AtribuirValorObj(dt.Rows(0)("COD_DENOMINACION"), GetType(String))
                    .CodigoIsoDivisa = Util.AtribuirValorObj(dt.Rows(0)("COD_ISO_DIVISA"), GetType(String))
                    .CodigoNivelDetalle = Util.AtribuirValorObj(dt.Rows(0)("COD_NIVEL_DETALLE"), GetType(String))
                    .CodigoTipoEfectivo = Util.AtribuirValorObj(dt.Rows(0)("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
                    .DescripcionDenominacion = Util.AtribuirValorObj(dt.Rows(0)("DES_DENOMINACION"), GetType(String))
                    .DescripcionDivisa = Util.AtribuirValorObj(dt.Rows(0)("DES_DIVISA"), GetType(String))
                    .NelCantidad = Util.AtribuirValorObj(dt.Rows(0)("NEL_CANTIDAD"), GetType(Int64))
                    .NelCantidadInicial = Util.AtribuirValorObj(dt.Rows(0)("NEL_CANTIDAD_ACUMULADO"), GetType(Int64))
                    .NelCantidadFinal = .NelCantidadInicial + .NelCantidad
                    .NumImporte = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE"), GetType(Decimal))
                    .NumImporteInicial = Util.AtribuirValorObj(dt.Rows(0)("NUM_IMPORTE_ACUMULADO"), GetType(Decimal))
                    .NumImporteFinal = .NumImporteInicial + .NumImporte
                    .IdentificadorSaldoEfectivo = Util.AtribuirValorObj(dt.Rows(0)("OID_CERT_SALDO_EFECTIVO"), GetType(String))
                    .IdentificadorDivisa = Util.AtribuirValorObj(dt.Rows(0)("OID_DIVISA"), GetType(String))
                    .IdentificadorCertificadoAnterior = Util.AtribuirValorObj(dt.Rows(0)("OID_CERTIFICADO_ANTERIOR"), GetType(String))
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

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.CertificadoSaldoEfectivoDeletar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CERTIFICADO", ProsegurDbType.Objeto_Id, OidCertificado))

            objTransacion.AdicionarItemTransacao(cmd)
        End Sub

#End Region

    End Class

End Namespace