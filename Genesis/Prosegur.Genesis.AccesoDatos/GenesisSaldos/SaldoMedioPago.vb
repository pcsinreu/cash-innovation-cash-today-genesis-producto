Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario.Tradutor

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe SaldoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SaldoMedioPago


#Region "[CONSULTAR]"

        ''' <summary>
        ''' Recupera o saldo Efectivo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' [anselmo.gois] 28/05/2013 - Criado
        ''' [marcel.espiritosanto] 15/09/2013 Alterado
        ''' </history>
        Public Shared Function RecuperarSaldoMedioPago(Peticion As CSCertificacion.DatosCertificacion.Peticion) As CSCertificacion.CuentaColeccion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim query As New StringBuilder

            cmd.CommandText = My.Resources.SaldoMedioPagoRecuperar
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, False))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))

            Dim objEstados As New List(Of String)
            objEstados.Add(Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)
            objEstados.Add(Enumeradores.EstadoDocumento.Rechazado.RecuperarValor)
            objEstados.Add(Enumeradores.EstadoDocumento.Sustituido.RecuperarValor)
            query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objEstados, "COD_ESTADO_DOCUMENTO", cmd, "AND", "TMP"))

            query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosDelegaciones, "COD_DELEGACION", cmd, "AND", "DL"))
            query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSectores, "COD_SECTOR", cmd, "AND", "S"))
            query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, Peticion.CodigosSubCanales, "COD_SUBCANAL", cmd, "AND", "SC"))

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, If(query.Length > 0, query.ToString, String.Empty)))

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

                    'Esse importe acumulado não estava sendo preenchido
                    '.NumImporteInicial = Util.AtribuirValorObj(dr("NUM_IMPORTE_ACUMULADO"), GetType(Decimal)), _
                    Cuenta.SaldosMedioPagos.Add(New CSCertificacion.CertificadoSaldoMedioPago With { _
                                               .BolDisponible = Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)), _
                                               .CodigoNivelDetalle = Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String)), _
                                               .NelCantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Integer)), _
                                               .NumImporte = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal)), _
                                               .CodigoMedioPago = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String)), _
                                               .CodigoTipoMedioPago = Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String)), _
                                               .IdentificadorUnidadMedida = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)), _
                                               .IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))})

                Next

            End If

            Return cuentas
        End Function

        Public Shared Function ElSaldoEstaNegativo(identificadorUltimaTransaccion As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.VerificaSaldoMedioPagoNegativoUltimaTransaccion)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ULTIMA_TRANSACCION", ProsegurDbType.Objeto_Id, identificadorUltimaTransaccion))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

        End Function

#End Region

        ''' <summary>
        ''' Insere ou atualiza saldoMedioPago registro de Saldo efectivo de documento.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub SaldoMedioPagoAtualizar(identificadorTransaccion As String, usuario As String, permiteLlegarSaldoNegativo As Boolean)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.SaldoMedioPagoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_MEDIO_PAGO", ProsegurDbType.Objeto_Id, identificadorTransaccion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO", ProsegurDbType.Descricao_Curta, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            If Not permiteLlegarSaldoNegativo AndAlso ElSaldoEstaNegativo(identificadorTransaccion) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_msg_el_movimiento_produce_saldo_negativo"))
            End If

        End Sub
    End Class

End Namespace