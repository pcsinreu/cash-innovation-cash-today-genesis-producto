Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor

' Alias
Imports CSCertificacion = Prosegur.Genesis.ContractoServicio.GenesisSaldos.Certificacion

Namespace GenesisSaldos

    ''' <summary>
    ''' Classe SaldoEfectivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/05/2013 - Criado
    ''' </history>
    Public Class SaldoEfectivo

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
        Public Shared Function RecuperarSaldoEfectivo(Peticion As CSCertificacion.DatosCertificacion.Peticion) As CSCertificacion.CuentaColeccion

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim query As New StringBuilder

            cmd.CommandText = My.Resources.SaldoEfectivoRecuperar
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_CERTIFICADO", ProsegurDbType.Logico, False))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, If(Peticion.Cliente IsNot Nothing, Peticion.Cliente.Codigo, String.Empty)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_PLAN_CERTIFICACION", ProsegurDbType.Data_Hora, Peticion.FyhCertificado.QuieroGrabarGMTZeroEnLaBBDD(Peticion.DelegacionLogada)))

            Dim objEstados As New List(Of String)
            objEstados.Add(Enumeradores.EstadoDocumento.Aceptado.RecuperarValor)
            objEstados.Add(Enumeradores.EstadoDocumento.Rechazado.RecuperarValor)
            objEstados.Add(Enumeradores.EstadoDocumento.Sustituido.RecuperarValor)
            query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, objEstados, "COD_ESTADO_DOCUMENTO", cmd, "AND", "TE"))

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
                    certSaldoEfectivo.IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    certSaldoEfectivo.NumImporte = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double))
                    certSaldoEfectivo.IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                    'certSaldoEfectivo.OidCertificado = Util.AtribuirValorObj(dr("OID_CERTIFICADO"), GetType(String))

                    Cuenta.SaldosEfectivos.Add(certSaldoEfectivo)
                Next

            End If

            Return cuentas
        End Function

        ''' <summary>
        ''' Recupera os saldo do setor. Saldo total = (saldo geral + saldo total + saldo detalhe)
        ''' </summary>
        ''' <param name="CodigoSectores"></param>
        ''' <param name="CodigoSectorPadre"></param>
        ''' <param name="CodigoPlanta"></param>
        ''' <param name="CodigoDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarSaldoTotalEfectivoSector(CodigoSectores As ObservableCollection(Of String),
                                                                 CodigoSectorPadre As String, _
                                                                 CodigoPlanta As String, _
                                                                 CodigoDelegacion As String, _
                                                                 ObtenerValoresDisponibles As Nullable(Of Boolean),
                                                                 ObtenerValoresMedioPago As Boolean) As ObservableCollection(Of Clases.Saldo)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            If ObtenerValoresMedioPago Then
                cmd.CommandText = My.Resources.SaldoObtenerPorSector
            Else
                cmd.CommandText = My.Resources.SaldoEfectivoRecuperarPorSector
            End If

            cmd.CommandType = CommandType.Text

            Dim query As New StringBuilder

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodigoPlanta))

            'Recupera os setores filhos
            If Not String.IsNullOrEmpty(CodigoSectorPadre) Then

                Dim objSector As Clases.Sector = Nothing
                objSector = Genesis.Sector.ObtenerSector(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)

                If objSector IsNot Nothing Then

                    query.AppendLine(" AND (S.OID_SECTOR_PADRE = []OID_SECTOR_PADRE OR S.OID_SECTOR = []OID_SECTOR_PADRE) ")

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, objSector.Identificador))

                End If

            Else

                query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigoSectores, "COD_SECTOR", cmd, "AND", "S"))

            End If

            If ObtenerValoresDisponibles IsNot Nothing Then

                query.AppendLine(" AND SE.BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, ObtenerValoresDisponibles))

            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            Return PreencherSaldoSector(dt)
        End Function

        ''' <summary>
        ''' Recupera os saldo da conta
        ''' </summary>
        ''' <param name="CodigoSectores"></param>
        ''' <param name="CodigoSectorPadre"></param>
        ''' <param name="CodigoPlanta"></param>
        ''' <param name="CodigoDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RecuperarSaldoPorCuenta(CodigoSectores As ObservableCollection(Of String),
                                                               CodigoCliente As String,
                                                               CodigoSubCliente As String,
                                                               CodigoPuntoServicio As String,
                                                               CodigoSectorPadre As String, _
                                                               CodigoPlanta As String, _
                                                               CodigoDelegacion As String, _
                                                               CodigoSubCanal As String, _
                                                               BolDisponible As Nullable(Of Boolean),
                                                               BolSaldoMedioPago As Boolean) As ObservableCollection(Of Clases.Saldo)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            If BolSaldoMedioPago Then
                cmd.CommandText = My.Resources.SaldoMedioPagoRecuperarDetallePorCuenta
            Else
                cmd.CommandText = My.Resources.SaldoEfectivoRecuperarDetallePorCuenta
            End If

            cmd.CommandType = CommandType.Text

            Dim query As New StringBuilder

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Identificador_Alfanumerico, CodigoPlanta))

            If Not String.IsNullOrEmpty(CodigoCliente) Then

                query.Append(" AND CL.COD_CLIENTE = []COD_CLIENTE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoCliente))

                If Not String.IsNullOrEmpty(CodigoSubCliente) Then

                    query.AppendLine(" AND SCL.COD_SUBCLIENTE = []COD_SUBCLIENTE ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCliente))

                Else
                    query.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL ")
                End If

                If Not String.IsNullOrEmpty(CodigoPuntoServicio) Then

                    query.AppendLine(" AND PS.COD_PTO_SERVICIO = []COD_PTO_SERVICIO ")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, CodigoPuntoServicio))

                Else
                    query.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL ")
                End If

            End If

            If Not String.IsNullOrEmpty(CodigoSubCanal) Then

                query.Append(" AND SC.COD_SUBCANAL = []COD_SUBCANAL ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, CodigoSubCanal))

            End If

            'Recupera os setores filhos
            If Not String.IsNullOrEmpty(CodigoSectorPadre) Then

                Dim objSector As Clases.Sector = Nothing
                objSector = Genesis.Sector.ObtenerSector(CodigoDelegacion, CodigoPlanta, CodigoSectorPadre)

                If objSector IsNot Nothing Then

                    query.AppendLine(" AND (S.OID_SECTOR_PADRE = []OID_SECTOR_PADRE OR S.OID_SECTOR = []OID_SECTOR_PADRE) ")

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_PADRE", ProsegurDbType.Objeto_Id, objSector.Identificador))

                End If

            Else
                query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, CodigoSectores, "COD_SECTOR", cmd, "AND", "S"))
            End If

            If BolDisponible IsNot Nothing Then
                query.AppendLine(" AND BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, BolDisponible))
            End If

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If BolSaldoMedioPago Then
                Return PreencherSaldoMedioPagoCuenta(dt)
            Else
                Return PreencherSaldoCuenta(dt)
            End If

        End Function

        ''' <summary>
        ''' Preenche o saldo total dos efectivos
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherSaldoSector(dt As DataTable) As ObservableCollection(Of Clases.Saldo)

            Dim objSaldoSectores As ObservableCollection(Of Clases.Saldo) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objSaldoSectores = New ObservableCollection(Of Clases.Saldo)
                Dim objSaldoSector As Clases.SaldoSector = Nothing
                Dim objDivisa As Clases.Divisa = Nothing
                Dim IdentificadorSector As String = String.Empty
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty
                Dim IdentificadorMedioPago As String = String.Empty
                Dim NivelDetalle As Enumeradores.TipoNivelDetalhe
                Dim objTipoMedioPago As Nullable(Of Enumeradores.TipoMedioPago)
                Dim CodigoTipoMedioPago As String = String.Empty
                Dim ObjDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing

                For Each dr In dt.Rows

                    IdentificadorSector = Util.AtribuirValorObj(dr("OID_SECTOR"), GetType(String))
                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    'Recupera o objeto de saldo sector atual
                    objSaldoSector = (From ss In objSaldoSectores Where DirectCast(ss, Clases.SaldoSector).Sector IsNot Nothing AndAlso
                                                                        DirectCast(ss, Clases.SaldoSector).Sector.Identificador = IdentificadorSector).FirstOrDefault

                    If objSaldoSector Is Nothing Then

                        objSaldoSectores.Add(New Clases.SaldoSector With {.Sector = New Clases.Sector With {.Identificador = IdentificadorSector, _
                                                                                                            .Codigo = Util.AtribuirValorObj(dr("COD_SECTOR"), GetType(String)), _
                                                                                                            .Descripcion = Util.AtribuirValorObj(dr("DES_SECTOR"), GetType(String))}, _
                                                                          .Divisas = New ObservableCollection(Of Clases.Divisa)})

                        objSaldoSector = (From ss In objSaldoSectores Where DirectCast(ss, Clases.SaldoSector).Sector IsNot Nothing AndAlso
                                                                            DirectCast(ss, Clases.SaldoSector).Sector.Identificador = IdentificadorSector).FirstOrDefault

                    End If

                    'Recupera o objeto da divisa atual
                    objDivisa = (From div In objSaldoSector.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If objDivisa Is Nothing Then

                        objSaldoSector.Divisas.Add(New Clases.Divisa With {.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                   .Identificador = IdentificadorDivisa, _
                                                   .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))})

                        'Recupera o objeto da divisa atual
                        objDivisa = (From div In objSaldoSector.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    NivelDetalle = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String)))

                    CodigoTipoMedioPago = Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
                    objTipoMedioPago = Nothing

                    If Not String.IsNullOrEmpty(CodigoTipoMedioPago) Then
                        objTipoMedioPago = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(CodigoTipoMedioPago)
                    End If


                    'Preenche o saldo de acordo com o nivel de detalhe
                    Select Case NivelDetalle

                        Case Enumeradores.TipoNivelDetalhe.Detalhado

                            If String.IsNullOrEmpty(CodigoTipoMedioPago) Then

                                IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                                If objDivisa.Denominaciones Is Nothing Then objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                                ObjDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                                'Adiciona uma nova denominação
                                If ObjDenominacion Is Nothing Then

                                    objDivisa.Denominaciones.Add(New Clases.Denominacion With {.Identificador = IdentificadorDenominacion, _
                                                                                               .Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                                               .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                                               .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)})


                                    ObjDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                                End If

                                'Atualiza o valdor da denominação
                                ObjDenominacion.ValorDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64)),
                                                                                                        .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)),
                                                                                                        .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                                                                        Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                            Else

                                IdentificadorMedioPago = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))

                                If objDivisa.MediosPago Is Nothing Then objDivisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                                objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdentificadorMedioPago).FirstOrDefault

                                'Adiciona uma nova denominação
                                If objMedioPago Is Nothing Then

                                    objDivisa.MediosPago.Add(New Clases.MedioPago With {.Identificador = IdentificadorMedioPago, _
                                                                                        .Codigo = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String)), _
                                                                                        .Descripcion = Util.AtribuirValorObj(dr("DES_MEDIO_PAGO"), GetType(String)), _
                                                                                        .Tipo = objTipoMedioPago, _
                                                                                        .Valores = New ObservableCollection(Of Clases.ValorMedioPago)})

                                    objMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdentificadorMedioPago).FirstOrDefault

                                End If

                                'Atualiza o valdor da denominação
                                objMedioPago.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64)),
                                                                                         .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)),
                                                                                         .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                                                         Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                            End If

                        Case Enumeradores.TipoNivelDetalhe.Total

                            If String.IsNullOrEmpty(CodigoTipoMedioPago) Then

                                If objDivisa.ValoresTotalesEfectivo Is Nothing Then objDivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                                'Adiciona os valores totais do efectivo.
                                objDivisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                                    .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                     Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})
                            Else

                                If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                                'Adiciona os valores totais do efectivo.
                                objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With {
                                                                          .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                                          .TipoMedioPago = objTipoMedioPago, _
                                                                          .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                           Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})
                            End If


                        Case Enumeradores.TipoNivelDetalhe.TotalGeral

                            If objDivisa.ValoresTotalesDivisa Is Nothing Then objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                            'Adiciona os valores totais da divisa.
                            objDivisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                               .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(String)),
                                                               Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                    End Select

                Next

            End If

            Return objSaldoSectores
        End Function

        ''' <summary>
        ''' Preenche o saldo total dos efectivos
        ''' </summary>
        ''' <param name="dt"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function PreencherSaldoCuenta(dt As DataTable) As ObservableCollection(Of Clases.Saldo)

            Dim objSaldoCuentas As ObservableCollection(Of Clases.Saldo) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objSaldoCuentas = New ObservableCollection(Of Clases.Saldo)()

                Dim objSaldoCuenta As Clases.SaldoCuenta = Nothing
                Dim objDivisa As Clases.Divisa = Nothing
                Dim IdentificadorCuenta As String = String.Empty
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorDenominacion As String = String.Empty
                Dim NivelDetalle As Enumeradores.TipoNivelDetalhe
                Dim ObjDenominacion As Clases.Denominacion = Nothing

                For Each dr In dt.Rows

                    IdentificadorCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))
                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    'Recupera o objeto de saldo sector atual
                    objSaldoCuenta = (From ss In objSaldoCuentas Where DirectCast(ss, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso
                                                                       DirectCast(ss, Clases.SaldoCuenta).Cuenta.Identificador = IdentificadorCuenta).FirstOrDefault

                    If objSaldoCuenta Is Nothing Then

                        objSaldoCuentas.Add(New Clases.SaldoCuenta With {.Cuenta = Cuenta.ObtenerCuentaPorIdentificador(IdentificadorCuenta, Enumeradores.TipoCuenta.Saldo, "RecuperarSaldoPorCuenta"), _
                                                                         .Divisas = New ObservableCollection(Of Clases.Divisa)})

                        objSaldoCuenta = (From ss In objSaldoCuentas Where DirectCast(ss, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso
                                                                           DirectCast(ss, Clases.SaldoCuenta).Cuenta.Identificador = IdentificadorCuenta).FirstOrDefault

                    End If

                    'Recupera o objeto da divisa atual
                    objDivisa = (From div In objSaldoCuenta.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If objDivisa Is Nothing Then

                        objSaldoCuenta.Divisas.Add(New Clases.Divisa With {.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                   .Identificador = IdentificadorDivisa, _
                                                   .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))})

                        'Recupera o objeto da divisa atual
                        objDivisa = (From div In objSaldoCuenta.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    NivelDetalle = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String)))

                    'Preenche o saldo de acordo com o nivel de detalhe
                    Select Case NivelDetalle

                        Case Enumeradores.TipoNivelDetalhe.Detalhado

                            IdentificadorDenominacion = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                            If objDivisa.Denominaciones Is Nothing Then objDivisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                            ObjDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                            'Adiciona uma nova denominação
                            If ObjDenominacion Is Nothing Then

                                objDivisa.Denominaciones.Add(New Clases.Denominacion With {.Identificador = IdentificadorDenominacion, _
                                                                                           .Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                                           .ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)})


                                ObjDenominacion = (From den In objDivisa.Denominaciones Where den.Identificador = IdentificadorDenominacion).FirstOrDefault

                            End If

                            'Atualiza o valdor da denominação
                            ObjDenominacion.ValorDenominacion.Add(New Clases.ValorDenominacion With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64)),
                                                                                                     .Calidad = If(String.IsNullOrWhiteSpace(Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))), Nothing, New Clases.Calidad With {.Identificador = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))}),
                                                                                                     .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(String)),
                                                                                                     .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                                                                    Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                        Case Enumeradores.TipoNivelDetalhe.Total

                            If objDivisa.ValoresTotalesEfectivo Is Nothing Then objDivisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)

                            'Adiciona os valores totais do efectivo.
                            objDivisa.ValoresTotalesEfectivo.Add(New Clases.ValorEfectivo With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                                .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                 Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                        Case Enumeradores.TipoNivelDetalhe.TotalGeral

                            If objDivisa.ValoresTotalesDivisa Is Nothing Then objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                            'Adiciona os valores totais da divisa.
                            objDivisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                               .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(String)),
                                                               Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                    End Select

                Next

            End If

            Return objSaldoCuentas
        End Function
        Private Shared Function PreencherSaldoMedioPagoCuenta(dt As DataTable) As ObservableCollection(Of Clases.Saldo)

            Dim objSaldoCuentas As ObservableCollection(Of Clases.Saldo) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                objSaldoCuentas = New ObservableCollection(Of Clases.Saldo)()

                Dim objSaldoCuenta As Clases.SaldoCuenta = Nothing
                Dim objDivisa As Clases.Divisa = Nothing
                Dim IdentificadorCuenta As String = String.Empty
                Dim IdentificadorDivisa As String = String.Empty
                Dim IdentificadorMedioPago As String = String.Empty
                Dim NivelDetalle As Enumeradores.TipoNivelDetalhe
                Dim ObjMedioPago As Clases.MedioPago = Nothing

                For Each dr In dt.Rows

                    IdentificadorCuenta = Util.AtribuirValorObj(dr("OID_CUENTA"), GetType(String))
                    IdentificadorDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                    'Recupera o objeto de saldo sector atual
                    objSaldoCuenta = (From ss In objSaldoCuentas Where DirectCast(ss, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso
                                                                       DirectCast(ss, Clases.SaldoCuenta).Cuenta.Identificador = IdentificadorCuenta).FirstOrDefault

                    If objSaldoCuenta Is Nothing Then

                        objSaldoCuentas.Add(New Clases.SaldoCuenta With {.Cuenta = Cuenta.ObtenerCuentaPorIdentificador(IdentificadorCuenta, Enumeradores.TipoCuenta.Saldo, "RecuperarSaldoPorCuenta"), _
                                                                         .Divisas = New ObservableCollection(Of Clases.Divisa)})

                        objSaldoCuenta = (From ss In objSaldoCuentas Where DirectCast(ss, Clases.SaldoCuenta).Cuenta IsNot Nothing AndAlso
                                                                           DirectCast(ss, Clases.SaldoCuenta).Cuenta.Identificador = IdentificadorCuenta).FirstOrDefault

                    End If

                    'Recupera o objeto da divisa atual
                    objDivisa = (From div In objSaldoCuenta.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    If objDivisa Is Nothing Then

                        objSaldoCuenta.Divisas.Add(New Clases.Divisa With {.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String)), _
                                                   .Identificador = IdentificadorDivisa, _
                                                   .Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))})

                        'Recupera o objeto da divisa atual
                        objDivisa = (From div In objSaldoCuenta.Divisas Where div.Identificador = IdentificadorDivisa).FirstOrDefault

                    End If

                    NivelDetalle = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(dr("COD_NIVEL_DETALLE"), GetType(String)))

                    'Preenche o saldo de acordo com o nivel de detalhe
                    Select Case NivelDetalle

                        Case Enumeradores.TipoNivelDetalhe.Detalhado

                            IdentificadorMedioPago = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))

                            If objDivisa.MediosPago Is Nothing Then objDivisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                            ObjMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdentificadorMedioPago).FirstOrDefault

                            'Adiciona um novo medio pago
                            If ObjMedioPago Is Nothing Then

                                objDivisa.MediosPago.Add(New Clases.MedioPago With {.Identificador = IdentificadorMedioPago, _
                                                                                           .Codigo = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String)), _
                                                                                           .Descripcion = Util.AtribuirValorObj(dr("DES_MEDIO_PAGO"), GetType(String)), _
                                                                                           .Tipo = Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(dr("COD_TIPO_MEDIO_PAGO"), GetType(String))), _
                                                                                           .Valores = New ObservableCollection(Of Clases.ValorMedioPago)})


                                ObjMedioPago = (From mp In objDivisa.MediosPago Where mp.Identificador = IdentificadorMedioPago).FirstOrDefault

                            End If

                            'Atualiza o valdor do medio pago
                            ObjMedioPago.Valores.Add(New Clases.ValorMedioPago With {.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64)),
                                                                                                     .Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(String)),
                                                                                                     .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                                                                    Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                        Case Enumeradores.TipoNivelDetalhe.Total

                            If objDivisa.ValoresTotalesTipoMedioPago Is Nothing Then objDivisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)

                            'Adiciona os valores totais do efectivo.
                            objDivisa.ValoresTotalesTipoMedioPago.Add(New Clases.ValorTipoMedioPago With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                                .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(Boolean)),
                                                                 Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                        Case Enumeradores.TipoNivelDetalhe.TotalGeral

                            If objDivisa.ValoresTotalesDivisa Is Nothing Then objDivisa.ValoresTotalesDivisa = New ObservableCollection(Of Clases.ValorDivisa)

                            'Adiciona os valores totais da divisa.
                            objDivisa.ValoresTotalesDivisa.Add(New Clases.ValorDivisa With {.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Double)), _
                                                               .TipoValor = If(Util.AtribuirValorObj(dr("BOL_DISPONIBLE"), GetType(String)),
                                                               Enumeradores.TipoValor.Disponible, Enumeradores.TipoValor.NoDisponible)})

                    End Select

                Next

            End If

            Return objSaldoCuentas
        End Function

        Public Shared Function ElSaldoEstaNegativo(identificadorUltimaTransaccion As String) As Boolean

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.VerificaSaldoEfectivoNegativoUltimaTransaccion)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ULTIMA_TRANSACCION", ProsegurDbType.Objeto_Id, identificadorUltimaTransaccion))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)

        End Function

#End Region

        ''' <summary>
        ''' Insere ou atualiza saldoEfecto registro de Saldo efectivo de documento.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub SaldoEfectivoAtualizar(identificadorTransaccion As String, usuario As String, permiteLlegarSaldoNegativo As Boolean)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.SaldoEfectivoInserir)
            cmd.CommandType = CommandType.Text
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TRANSACCION_EFECTIVO", ProsegurDbType.Objeto_Id, identificadorTransaccion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO", ProsegurDbType.Descricao_Curta, usuario))
            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            If Not permiteLlegarSaldoNegativo AndAlso ElSaldoEstaNegativo(identificadorTransaccion) Then
                Throw New Excepcion.NegocioExcepcion(Traduzir("028_msg_el_movimiento_produce_saldo_negativo"))
            End If

        End Sub
    End Class

End Namespace