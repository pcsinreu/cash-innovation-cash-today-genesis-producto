Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Collections.ObjectModel
Imports System.Text
Imports System.Threading.Tasks

Namespace GenesisSaldos.Abono
    Public Class Comun

        Public Shared Function RecuperarAbonos(filtro As Clases.Transferencias.FiltroConsultaAbono) As List(Of Clases.Abono.Abono)

            Dim objAbono As Clases.Abono.Abono = Nothing
            Dim abonos As List(Of Clases.Abono.Abono) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoRecuperarAbonos)
                cmd.CommandType = CommandType.Text

                If String.IsNullOrEmpty(filtro.IdentificadorAbono) Then

                    If Not String.IsNullOrEmpty(filtro.IdentificadorDelegacion) Then

                        cmd.CommandText += " AND A.OID_DELEGACION = []OID_DELEGACION "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, filtro.IdentificadorDelegacion))

                    End If

                    If Not String.IsNullOrEmpty(filtro.IdentificadorBanco) Then
                        cmd.CommandText += " AND A.OID_BANCO = []OID_BANCO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO", ProsegurDbType.Objeto_Id, filtro.IdentificadorBanco))
                    End If

                    If Not String.IsNullOrEmpty(filtro.IdentificadorCliente) Then
                        cmd.CommandText += " AND AV.OID_CLIENTE = []OID_CLIENTE "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Objeto_Id, filtro.IdentificadorCliente))
                    End If

                    If filtro.FechaAbonoDesde <> Date.MinValue AndAlso filtro.FechaAbonoHasta <> Date.MinValue Then
                        cmd.CommandText += " AND A.FYH_ABONO >= []FYH_ABONO_DESDE AND A.FYH_ABONO <= []FYH_ABONO_HASTA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ABONO_DESDE", ProsegurDbType.Data_Hora, filtro.FechaAbonoDesde))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ABONO_HASTA", ProsegurDbType.Data_Hora, filtro.FechaAbonoHasta))
                    ElseIf filtro.FechaAbonoDesde <> Date.MinValue Then
                        cmd.CommandText += " AND A.FYH_ABONO >= []FYH_ABONO_DESDE "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ABONO_DESDE", ProsegurDbType.Data_Hora, filtro.FechaAbonoDesde))
                    ElseIf filtro.FechaAbonoHasta <> Date.MinValue Then
                        cmd.CommandText += " AND A.FYH_ABONO <= []FYH_ABONO_HASTA "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_ABONO_HASTA", ProsegurDbType.Data_Hora, filtro.FechaAbonoHasta))
                    End If

                    If Not String.IsNullOrEmpty(filtro.CodigoEstado) Then
                        cmd.CommandText += " AND A.COD_ESTADO = []COD_ESTADO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, filtro.CodigoEstado))
                    End If

                    If filtro.IdentificadoresDivisas IsNot Nothing AndAlso filtro.IdentificadoresDivisas.Count > 0 Then
                        cmd.CommandText += Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.IdentificadoresDivisas, "OID_DIVISA", cmd, "AND", "AV")
                    End If

                    If Not String.IsNullOrEmpty(filtro.CodigoTipo) Then
                        cmd.CommandText += " AND A.COD_TIPO_ABONO = []COD_TIPO_ABONO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_TIPO_ABONO", ProsegurDbType.Identificador_Alfanumerico, filtro.CodigoTipo))
                    End If

                    If Not String.IsNullOrEmpty(filtro.NumeroExterno) Then
                        cmd.CommandText += " AND R.COD_EXTERNO = []COD_EXTERNO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, filtro.NumeroExterno))
                    End If

                    If Not String.IsNullOrEmpty(filtro.Codigo) Then
                        cmd.CommandText += " AND A.COD_ABONO = []COD_ABONO "
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ABONO", ProsegurDbType.Identificador_Alfanumerico, filtro.Codigo))
                    End If

                Else

                    cmd.CommandText += " AND A.OID_ABONO = []OID_ABONO "
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, filtro.IdentificadorAbono))

                End If

                cmd.CommandText += " ORDER BY A.FYH_ABONO DESC "
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    abonos = New List(Of Clases.Abono.Abono)

                    For Each dr In dt.Rows

                        objAbono = New Clases.Abono.Abono
                        With objAbono

                            .Identificador = Util.AtribuirValorObj(dr("OID_ABONO"), GetType(String))
                            .Bancos = New List(Of Clases.Abono.AbonoInformacion)
                            .Bancos.Add(New Clases.Abono.AbonoInformacion With {.Identificador = Util.AtribuirValorObj(dr("OID_BANCO"), GetType(String)), .Codigo = Util.AtribuirValorObj(dr("COD_CLIENTE"), GetType(String)), .Descripcion = Util.AtribuirValorObj(dr("DES_CLIENTE"), GetType(String))})
                            .Delegacion = New Prosegur.Genesis.Comon.Clases.Delegacion With {.Identificador = Util.AtribuirValorObj(dr("OID_DELEGACION"), GetType(String)),
                                                                                             .Codigo = Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String)),
                                                                                             .Descripcion = Util.AtribuirValorObj(dr("DES_DELEGACION"), GetType(String))}
                            .Codigo = Util.AtribuirValorObj(dr("COD_ABONO"), GetType(String))
                            .CodigoEstado = RecuperarEnum(Of Enumeradores.EstadoAbono)(Util.AtribuirValorObj(dr("COD_ESTADO"), GetType(String)))
                            .TipoAbono = RecuperarEnum(Of Enumeradores.TipoAbono)(Util.AtribuirValorObj(dr("COD_TIPO_ABONO"), GetType(String)))
                            .TipoValor = RecuperarEnum(Of Clases.Abono.TipoValorAbono)(Util.AtribuirValorObj(dr("COD_TIPO_VALORES"), GetType(String)))
                            .UsuarioCreacion = Util.AtribuirValorObj(dr("DES_USUARIO_CREACION"), GetType(String))
                            .UsuarioModificacion = Util.AtribuirValorObj(dr("DES_USUARIO_MODIFICACION"), GetType(String))
                            .Fecha = Util.AtribuirValorObj(dr("FYH_ABONO"), GetType(Date))
                            .FechaHoraCreacion = Util.AtribuirValorObj(dr("GMT_CREACION"), GetType(DateTime))
                            .FechaHoraModificacion = Util.AtribuirValorObj(dr("GMT_MODIFICACION"), GetType(DateTime))
                            .IdenficadorGrupoDocumento = Util.AtribuirValorObj(dr("OID_GRUPO_DOCUMENTO"), GetType(String))
                        End With

                        abonos.Add(objAbono)

                    Next

                End If

            End Using

            Return abonos

        End Function

        Public Shared Sub CambiarEstadoAbono(identificadorAbono As String, codigoEstadoAbono As Enumeradores.EstadoAbono,
                                             usuario As String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoCambiarEstado)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbono))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, codigoEstadoAbono.RecuperarValor()))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, usuario))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)

            End Using

            If codigoEstadoAbono = Enumeradores.EstadoAbono.Anulado Then

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbono))
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoCambiarEstadoRemesa)
                    cmd.CommandType = CommandType.Text
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
                End Using

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbono))
                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoCamibarEstadoBulto)
                    cmd.CommandType = CommandType.Text
                    AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
                End Using

            End If

        End Sub
        Public Shared Function RecuperarReportesGenerados(identificadoresAbonos As List(Of String)) As ObservableCollection(Of Clases.Abono.ReporteAbono)

            Dim reportes As New ObservableCollection(Of Clases.Abono.ReporteAbono)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoRecuperarAchivosGenerados)
                cmd.CommandType = CommandType.Text

                If identificadoresAbonos IsNot Nothing AndAlso identificadoresAbonos.Count > 0 Then
                    cmd.CommandText += Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadoresAbonos, "OID_ENTIDAD_REPORTE", cmd, "AND", "RR")
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows

                        Dim reporte As New Clases.Abono.ReporteAbono
                        reporte.Identificador = Util.AtribuirValorObj(dr("OID_RESULTADO_REPORTE"), GetType(String))
                        reporte.Tipo = RecuperarEnum(Of Enumeradores.TipoReporte)(Util.AtribuirValorObj(dr("NEL_TIPO_REPORTE"), GetType(String)))
                        reporte.IdentificadorAbono = Util.AtribuirValorObj(dr("OID_ENTIDAD_REPORTE"), GetType(String))
                        reporte.CodigoSituacion = Util.AtribuirValorObj(dr("COD_SITUACION_REPORTE"), GetType(String))
                        reporte.DesErrorEjecucion = Util.AtribuirValorObj(dr("DES_ERROR_EJECUCION"), GetType(String))
                        reporte.NombreArchivo = Util.AtribuirValorObj(dr("DES_NOMBRE_ARCHIVO"), GetType(String))

                        reportes.Add(reporte)
                    Next
                End If

            End Using

            Return reportes

        End Function

        Public Shared Function InserirAbono(objAbono As Clases.Abono.Abono,
                                            delegacion As Clases.Delegacion,
                                            ByRef transaccion As DataBaseHelper.Transaccion) As String

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.AbonoInserir), True, CommandType.Text)

            Dim IdentificadorAbono As String = Guid.NewGuid.ToString

            wrapper.AgregarParam("OID_ABONO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, IdentificadorAbono)
            wrapper.AgregarParam("OID_BANCO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.Bancos(0).Identificador)
            wrapper.AgregarParam("OID_DELEGACION", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.Delegacion.Identificador)
            wrapper.AgregarParam("FYH_ABONO", DbHelper.ProsegurDbType.Data_Hora, objAbono.Fecha.QuieroGrabarGMTZeroEnLaBBDD(delegacion))
            wrapper.AgregarParam("COD_TIPO_ABONO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.TipoAbono.RecuperarValor)
            wrapper.AgregarParam("COD_ESTADO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.CodigoEstado.RecuperarValor)
            wrapper.AgregarParam("COD_ABONO", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.Codigo)
            wrapper.AgregarParam("DES_USUARIO_CREACION", DbHelper.ProsegurDbType.Descricao_Curta, objAbono.UsuarioCreacion)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.UsuarioModificacion)
            wrapper.AgregarParam("COD_TIPO_VALORES", DbHelper.ProsegurDbType.Identificador_Alfanumerico, objAbono.TipoValor.RecuperarValor)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

            Return IdentificadorAbono
        End Function

        Public Shared Sub ActualizarAbono(objAbono As Clases.Abono.Abono,
                                          delegacion As Clases.Delegacion,
                                          ByRef transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(My.Resources.AbonoActualizar, True, CommandType.Text)

            Dim query As New StringBuilder

            wrapper.AgregarParam("OID_ABONO", ProsegurDbType.Objeto_Id, objAbono.Identificador)
            wrapper.AgregarParam("OID_BANCO", ProsegurDbType.Objeto_Id, objAbono.Bancos(0).Identificador)

            If Not Date.MinValue.Equals(objAbono.Fecha) Then
                query.AppendLine(",FYH_ABONO = []FYH_ABONO")
                wrapper.AgregarParam("FYH_ABONO", ProsegurDbType.Data_Hora, objAbono.Fecha.QuieroGrabarGMTZeroEnLaBBDD(delegacion))
            End If

            If Not String.IsNullOrEmpty(objAbono.Codigo) Then
                query.AppendLine(",COD_ABONO = []COD_ABONO")
                wrapper.AgregarParam("COD_ABONO", ProsegurDbType.Descricao_Longa, objAbono.Codigo)
            End If

            wrapper.AgregarParam("COD_TIPO_ABONO", ProsegurDbType.Identificador_Alfanumerico, objAbono.TipoAbono.RecuperarValor)
            wrapper.AgregarParam("COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, objAbono.CodigoEstado.RecuperarValor)
            wrapper.AgregarParam("DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, objAbono.UsuarioModificacion)
            wrapper.AgregarParam("COD_TIPO_VALORES", ProsegurDbType.Identificador_Alfanumerico, objAbono.TipoValor.RecuperarValor)

            If query.Length > 0 Then
                wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(wrapper.SP, query.ToString))
            Else
                wrapper.SP = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(wrapper.SP, String.Empty))
            End If


            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub

        Public Shared Function ObtenerDivisasAbonarSaldos(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)

            Dim abonosValores As New List(Of Clases.Abono.AbonoValor)

            If filtro IsNot Nothing Then

                Dim dtSaldos As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim QueryEfectivo As String = My.Resources.AbonoObtenerSaldosEfectivoCuentas
                    Dim QueryMedioPago As String = My.Resources.AbonoObtenerSaldosMedioPagoCuentas
                    Dim QueryWithDatoBancario As String = String.Empty

                    Dim QueryWhereCuenta As New StringBuilder
                    Dim QueryWhereDatoBancario As New StringBuilder
                    Dim QueryWhereEfectivo As New StringBuilder
                    Dim QueryWhereMedioPago As New StringBuilder
                    Dim QueryWhereNotInCuentasSaldoEfectivo As New StringBuilder
                    Dim QueryWhereNotInCuentasSaldoMedioPago As New StringBuilder

                    Dim QueryInner As New StringBuilder
                    Dim QuerySelect As New StringBuilder
                    Dim QueryOrder As New StringBuilder

                    Dim hayFiltroEfectivo As Boolean = False
                    Dim tiposMediosPago As New List(Of String)
                    Dim hayFiltroCliente As Boolean = True

                    If filtro.TipoAbono = Enumeradores.TipoAbono.Saldos AndAlso (filtro.Clientes Is Nothing OrElse filtro.Clientes.Count = 0) Then

                        'Apenas saldos que têm relação(dado bancario) com o banco/solicitante e que abona por total
                        QueryWithDatoBancario = " WITH Q as (SELECT DISTINCT DATB.OID_CLIENTE,DATB.OID_DIVISA "
                        QueryWithDatoBancario += " FROM SAPR_TDATO_BANCARIO DATB WHERE DATB.BOL_ACTIVO = 1 "
                        QueryWithDatoBancario += " AND DATB.OID_BANCO = []OID_BANCO_WITH  AND DATB.OID_CLIENTE IS NOT NULL) "

                        If abono.Bancos IsNot Nothing AndAlso abono.Bancos.Count > 0 Then
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO_WITH", ProsegurDbType.Objeto_Id, abono.Bancos.FirstOrDefault.Identificador))
                        Else
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_BANCO_WITH", ProsegurDbType.Objeto_Id, String.Empty))
                        End If

                        QueryInner.AppendLine(" INNER JOIN GEPR_TCLIENTE CLI ON CLI.OID_CLIENTE = C.OID_CLIENTE ")
                        QueryInner.AppendLine(" INNER JOIN Q ON Q.OID_CLIENTE = C.OID_CLIENTE AND Q.OID_DIVISA = SAL.OID_DIVISA ")
                        QueryWhereEfectivo.Append(" AND CLI.BOL_ABONA_POR_TOTAL = 1 ")
                        QueryWhereMedioPago.Append(" AND CLI.BOL_ABONA_POR_TOTAL = 1 ")

                        hayFiltroCliente = False
                    End If

                    Util.PreencherQueryCuentaSaldo(filtro, QueryWhereCuenta, cmd)

                    QueryWhereEfectivo.Append(QueryWhereCuenta.ToString())
                    QueryWhereMedioPago.Append(QueryWhereCuenta.ToString())

                    If Not filtro.ConsiderarTodosLosNiveles OrElse (filtro.Sectores Is Nothing OrElse filtro.Sectores.Count = 0) Then
                        QueryInner.AppendLine(" INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = C.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION AND DEL.OID_DELEGACION = []OID_DELEGACION ")
                    Else
                        QueryInner.AppendLine(" JOIN (SELECT SECT.OID_SECTOR, SECT.OID_SECTOR_PADRE, LEVEL AS NIVEL, ROWNUM AS LINHA, ")
                        QueryInner.AppendLine(" SECT.COD_SECTOR, SECT.COD_MIGRACION, SECT.DES_SECTOR, SECT.BOL_ACTIVO, SECT.BOL_CENTRO_PROCESO, SECT.BOL_CONTEO, ")
                        QueryInner.AppendLine(" SECT.BOL_TESORO, SECT.GMT_CREACION, SECT.GMT_MODIFICACION, ")
                        QueryInner.AppendLine(" SECT.BOL_PERMITE_DISPONER_VALOR, SECT.OID_TIPO_SECTOR, SECT.OID_PLANTA ")
                        QueryInner.AppendLine("         FROM GEPR_TSECTOR SECT ")
                        QueryInner.AppendLine("         START WITH 1 = 1 ")
                        QueryInner.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.Sectores.Select(Function(r) r.Identificador).ToList, "OID_SECTOR",
                                                   cmd, " AND ", "SECT"))
                        QueryInner.AppendLine("         CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SE ON SE.OID_SECTOR = C.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION AND DEL.OID_DELEGACION = []OID_DELEGACION ")
                    End If

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, filtro.IdentificadorDelegacion))

                    If filtro.ClientesConSoloUnDatoBancario Then

                        QueryInner.AppendLine(" INNER JOIN SAPR_TDATO_BANCARIO DB ON DB.OID_CLIENTE = C.OID_CLIENTE ")

                        QueryWhereDatoBancario.Append(IIf(QueryWhereDatoBancario.ToString <> " WHERE ", " AND DB.OID_DATO_BANCARIO IN ( ", " DB.OID_DATO_BANCARIO IN ( "))
                        QueryWhereDatoBancario.Append(" SELECT DABA.OID_DATO_BANCARIO FROM SAPR_TDATO_BANCARIO DABA ")
                        QueryWhereDatoBancario.Append(" WHERE DABA.OID_CLIENTE NOT IN (SELECT DABA_INT.OID_CLIENTE FROM SAPR_TDATO_BANCARIO DABA_INT ")
                        QueryWhereDatoBancario.Append(" WHERE DABA_INT.BOL_ACTIVO = 1 ")
                        If filtro.Clientes IsNot Nothing AndAlso filtro.Clientes.Count > 0 Then
                            QueryWhereDatoBancario.Append(" AND DABA_INT.OID_CLIENTE = []OID_CLIENTE ")

                            If filtro.SubClientes IsNot Nothing AndAlso filtro.SubClientes.Count > 0 Then
                                QueryWhereDatoBancario.Append(" AND DABA_INT.OID_SUBCLIENTE = []OID_SUBCLIENTE ")

                                If filtro.PuntosServicio IsNot Nothing AndAlso filtro.PuntosServicio.Count > 0 Then
                                    QueryWhereDatoBancario.Append(" AND DABA_INT.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                                End If
                            End If
                        End If

                        QueryWhereDatoBancario.Append(" GROUP BY DABA_INT.OID_CLIENTE,DABA_INT.OID_DIVISA HAVING COUNT(DABA_INT.OID_DIVISA) > 1) ")
                        QueryWhereDatoBancario.Append(" AND DABA.BOL_ACTIVO = 1 ")

                        If filtro.Clientes IsNot Nothing AndAlso filtro.Clientes.Count > 0 Then
                            QueryWhereDatoBancario.Append(" AND DABA.OID_CLIENTE = []OID_CLIENTE ")

                            If filtro.SubClientes IsNot Nothing AndAlso filtro.SubClientes.Count > 0 Then
                                QueryWhereDatoBancario.Append(" AND DABA.OID_SUBCLIENTE = []OID_SUBCLIENTE ")

                                If filtro.PuntosServicio IsNot Nothing AndAlso filtro.PuntosServicio.Count > 0 Then
                                    QueryWhereDatoBancario.Append(" AND DABA.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                                End If
                            End If
                        End If

                        QueryWhereDatoBancario.Append(" ) ")

                        QueryWhereEfectivo.Append(QueryWhereDatoBancario.ToString())
                        QueryWhereMedioPago.Append(QueryWhereDatoBancario.ToString())

                    End If

                    'Filtro Divisa
                    If filtro.IdentificadoresDivisas IsNot Nothing AndAlso filtro.IdentificadoresDivisas.Count > 0 Then

                        Dim QueryWhereAux As New StringBuilder
                        QueryWhereAux.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.IdentificadoresDivisas, "OID_DIVISA", cmd, "AND", "SAL"))
                        QueryWhereEfectivo.Append(QueryWhereAux.ToString())
                        QueryWhereMedioPago.Append(QueryWhereAux.ToString())

                    End If

                    If filtro.IdentificadoresValores IsNot Nothing AndAlso filtro.IdentificadoresValores.Count > 0 Then

                        hayFiltroEfectivo = (From i In filtro.IdentificadoresValores Where i = "codefec").Count()
                        tiposMediosPago = (From i In filtro.IdentificadoresValores Where i <> "codefec").ToList()

                        If tiposMediosPago.Count > 0 Then
                            QueryWhereMedioPago.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, tiposMediosPago, "COD_TIPO_MEDIO_PAGO", cmd, "AND", "SAL"))
                        End If

                    End If

                    Dim QueryMultiplesCuentasBancarias = "0"
                    If abono.Bancos IsNot Nothing AndAlso abono.Bancos.Count > 0 Then
                        QueryMultiplesCuentasBancarias = _
                        "(SELECT COUNT(1) FROM SAPR_TDATO_BANCARIO DATB WHERE DATB.BOL_ACTIVO = 1 AndAlso DATB.OID_CLIENTE = C.OID_CLIENTE " &
                        "AND ((C.OID_SUBCLIENTE IS NULL AND DATB.OID_SUBCLIENTE IS NULL) OR DATB.OID_SUBCLIENTE = C.OID_SUBCLIENTE) " &
                        "AND ((C.OID_PTO_SERVICIO IS NULL AND DATB.OID_PTO_SERVICIO IS NULL) OR DATB.OID_PTO_SERVICIO = C.OID_PTO_SERVICIO) " &
                        "AND DATB.OID_DIVISA = SAL.OID_DIVISA {0})"
                        If (abono.TipoAbono = Enumeradores.TipoAbono.Saldos) Then
                            QueryMultiplesCuentasBancarias = String.Format(QueryMultiplesCuentasBancarias, "AND DATB.OID_BANCO = []DATB_OID_BANCO")
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DATB_OID_BANCO", ProsegurDbType.Objeto_Id, abono.Bancos(0).Identificador))
                        Else
                            QueryMultiplesCuentasBancarias = String.Format(QueryMultiplesCuentasBancarias, String.Empty)
                        End If
                    End If

                    If abono.AbonosValor IsNot Nothing AndAlso abono.AbonosValor.Count > 0 AndAlso abono.TipoAbono <> Enumeradores.TipoAbono.Elemento Then

                        Dim querytIn As New List(Of String)()

                        For i As Integer = 0 To abono.SnapshotsAbonoSaldo.Count - 1
                            querytIn.Clear()

                            Dim abonoValor = abono.SnapshotsAbonoSaldo(i)
                            For Each saldoCuenta In abonoValor.AbonoSaldo.ListaSaldoCuenta
                                querytIn.Add(saldoCuenta.IdentificadorCuenta)
                            Next

                            If hayFiltroEfectivo AndAlso tiposMediosPago.Count = 0 Then
                                QueryWhereNotInCuentasSaldoEfectivo.Append(" AND SAL.OID_SALDO_EFECTIVO NOT IN (SELECT SAL_EFEC.OID_SALDO_EFECTIVO FROM SAPR_TSALDO_EFECTIVO SAL_EFEC ")
                                QueryWhereNotInCuentasSaldoEfectivo.Append("WHERE SAL_EFEC.OID_DIVISA = []SAL_EFEC_OID_DIVISA" & i.ToString())
                                QueryWhereNotInCuentasSaldoEfectivo.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, querytIn, "OID_CUENTA_SALDO", cmd, "AND", "SAL_EFEC", , , "EFEC_"))
                                QueryWhereNotInCuentasSaldoEfectivo.Append(")")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "SAL_EFEC_OID_DIVISA" & i.ToString(), ProsegurDbType.Objeto_Id, abonoValor.AbonoSaldo.Divisa.Identificador))

                            ElseIf Not hayFiltroEfectivo AndAlso tiposMediosPago.Count > 0 Then
                                QueryWhereNotInCuentasSaldoMedioPago.Append(" AND SAL.OID_SALDO_MEDIO_PAGO NOT IN (SELECT SAL_MP.OID_SALDO_MEDIO_PAGO FROM SAPR_TSALDO_MEDIO_PAGO SAL_MP ")
                                QueryWhereNotInCuentasSaldoMedioPago.Append("WHERE SAL_MP.OID_DIVISA = []SAL_MP_OID_DIVISA" & i.ToString())
                                QueryWhereNotInCuentasSaldoMedioPago.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, querytIn, "OID_CUENTA_SALDO", cmd, "AND", "SAL_MP", , , "MP_"))
                                QueryWhereNotInCuentasSaldoMedioPago.Append(")")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "SAL_MP_OID_DIVISA" & i.ToString(), ProsegurDbType.Objeto_Id, abonoValor.AbonoSaldo.Divisa.Identificador))
                            Else
                                QueryWhereNotInCuentasSaldoEfectivo.Append(" AND SAL.OID_SALDO_EFECTIVO NOT IN (SELECT SAL_EFEC.OID_SALDO_EFECTIVO FROM SAPR_TSALDO_EFECTIVO SAL_EFEC ")
                                QueryWhereNotInCuentasSaldoEfectivo.Append("WHERE SAL_EFEC.OID_DIVISA = []SAL_EFEC_OID_DIVISA" & i.ToString())
                                QueryWhereNotInCuentasSaldoEfectivo.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, querytIn, "OID_CUENTA_SALDO", cmd, "AND", "SAL_EFEC", , , "EFEC_"))
                                QueryWhereNotInCuentasSaldoEfectivo.Append(")")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "SAL_EFEC_OID_DIVISA" & i.ToString(), ProsegurDbType.Objeto_Id, abonoValor.AbonoSaldo.Divisa.Identificador))

                                QueryWhereNotInCuentasSaldoMedioPago.Append(" AND SAL.OID_SALDO_MEDIO_PAGO NOT IN (SELECT SAL_MP.OID_SALDO_MEDIO_PAGO FROM SAPR_TSALDO_MEDIO_PAGO SAL_MP ")
                                QueryWhereNotInCuentasSaldoMedioPago.Append("WHERE SAL_MP.OID_DIVISA = []SAL_MP_OID_DIVISA" & i.ToString())
                                QueryWhereNotInCuentasSaldoMedioPago.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, querytIn, "OID_CUENTA_SALDO", cmd, "AND", "SAL_MP", , , "MP_"))
                                QueryWhereNotInCuentasSaldoMedioPago.Append(")")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "SAL_MP_OID_DIVISA" & i.ToString(), ProsegurDbType.Objeto_Id, abonoValor.AbonoSaldo.Divisa.Identificador))
                            End If
                            
                        Next

                    End If

                    If hayFiltroEfectivo AndAlso tiposMediosPago.Count > 0 Then

                        QueryEfectivo = String.Format(QueryEfectivo, QueryInner, QueryWhereEfectivo, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoEfectivo)
                        QueryMedioPago = String.Format(QueryMedioPago, QueryInner, QueryWhereMedioPago, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoMedioPago)

                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo.ToString & " UNION " & QueryMedioPago.ToString)

                    ElseIf hayFiltroEfectivo Then

                        QueryEfectivo = String.Format(QueryEfectivo, QueryInner, QueryWhereEfectivo, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoEfectivo)
                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo.ToString)

                    ElseIf tiposMediosPago.Count > 0 Then

                        QueryMedioPago = String.Format(QueryMedioPago, QueryInner, QueryWhereMedioPago, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoMedioPago)
                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryMedioPago.ToString)

                    Else
                        QueryEfectivo = String.Format(QueryEfectivo, QueryInner, QueryWhereEfectivo, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoEfectivo)
                        QueryMedioPago = String.Format(QueryMedioPago, QueryInner, QueryWhereMedioPago, QueryMultiplesCuentasBancarias, QueryWhereNotInCuentasSaldoMedioPago)

                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo.ToString & " UNION " & QueryMedioPago.ToString)
                    End If

                    If Not hayFiltroCliente Then
                        cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryWithDatoBancario & cmd.CommandText)
                    End If

                    cmd.CommandType = CommandType.Text

                    dtSaldos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                    If dtSaldos IsNot Nothing AndAlso dtSaldos.Rows.Count > 0 Then

                        CargarDivisasAbonarSaldos(abonosValores, dtSaldos, filtro.TipoAbono, filtro.ValoresCeroYNegativos)

                    End If


                End Using

            End If

            Return abonosValores

        End Function

        Public Shared Function ObtenerDivisasAbonarElementos(filtro As Clases.Transferencias.FiltroConsultaValoresAbono, abono As Clases.Abono.Abono) As List(Of Clases.Abono.AbonoValor)

            Dim abonosValores As New List(Of Clases.Abono.AbonoValor)

            If filtro IsNot Nothing Then

                Dim dtRemesas As DataTable = Nothing

                Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim QueryInner As New StringBuilder
                    Dim QueryWhere As New StringBuilder
                    QueryWhere.Append(" WHERE ")
                    Dim QuerySelect As New StringBuilder
                    Dim QueryOrder As New StringBuilder
                    cmd.CommandText = My.Resources.ObtenerRemesasAbono

                    Util.PreencherQueryCuentaElemento(filtro, abono, QueryWhere, cmd)

                    If (filtro.ObtenerParaCalcularDiferencas) Then
                        QueryInner.AppendLine(" INNER JOIN SAPR_TABONO_ELEMENTO ABEL ON ABEL.OID_REMESA = R.OID_REMESA ")
                    End If

                    If Not filtro.ConsiderarTodosLosNiveles OrElse (filtro.Sectores Is Nothing OrElse filtro.Sectores.Count = 0) Then
                        QueryInner.AppendLine(" INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION AND  DEL.OID_DELEGACION = []OID_DELEGACION ")
                    Else
                        QuerySelect.AppendLine(" ,SE.NIVEL ")
                        QuerySelect.AppendLine(" ,SE.OID_SECTOR_PADRE ")
                        QuerySelect.AppendLine(" ,SE.LINHA ")

                        QueryInner.AppendLine(" JOIN (SELECT SECT.OID_SECTOR, SECT.OID_SECTOR_PADRE, LEVEL AS NIVEL, ROWNUM AS LINHA, ")
                        QueryInner.AppendLine(" SECT.COD_SECTOR, SECT.COD_MIGRACION, SECT.DES_SECTOR, SECT.BOL_ACTIVO, SECT.BOL_CENTRO_PROCESO, SECT.BOL_CONTEO, ")
                        QueryInner.AppendLine(" SECT.BOL_TESORO, SECT.GMT_CREACION, SECT.GMT_MODIFICACION, ")
                        QueryInner.AppendLine(" SECT.BOL_PERMITE_DISPONER_VALOR, SECT.OID_TIPO_SECTOR, SECT.OID_PLANTA ")
                        QueryInner.AppendLine("         FROM GEPR_TSECTOR SECT ")
                        QueryInner.AppendLine("         START WITH 1 = 1 ")
                        QueryInner.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.Sectores.Select(Function(r) r.Identificador).ToList, "OID_SECTOR",
                                                   cmd, " AND ", "SECT"))
                        QueryInner.AppendLine("         CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TTIPO_SECTOR TSE ON TSE.OID_TIPO_SECTOR = SE.OID_TIPO_SECTOR ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TPLANTA PT ON PT.OID_PLANTA = SE.OID_PLANTA ")
                        QueryInner.AppendLine(" INNER JOIN GEPR_TDELEGACION DEL ON DEL.OID_DELEGACION = PT.OID_DELEGACION AND  DEL.OID_DELEGACION = []OID_DELEGACION ")

                        QueryOrder.AppendLine(" ORDER BY SE.NIVEL, SE.LINHA")
                    End If

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Objeto_Id, filtro.IdentificadorDelegacion))

                    If abono IsNot Nothing AndAlso abono.Delegacion IsNot Nothing AndAlso
                        Not String.IsNullOrEmpty(abono.Delegacion.Identificador) Then
                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND CU.OID_DELEGACION = []OID_DELEGACION ", " CU.OID_DELEGACION = []OID_DELEGACION "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, abono.Delegacion.Identificador))
                    End If

                    If filtro.ClientesConSoloUnDatoBancario Then

                        QueryInner.AppendLine(" INNER JOIN SAPR_TDATO_BANCARIO DB ON DB.OID_CLIENTE = CU.OID_CLIENTE ")

                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND DB.OID_DATO_BANCARIO IN ( ", " DB.OID_DATO_BANCARIO IN ( "))
                        QueryWhere.Append(" SELECT DABA.OID_DATO_BANCARIO FROM SAPR_TDATO_BANCARIO DABA ")
                        QueryWhere.Append(" WHERE DABA.OID_CLIENTE NOT IN (SELECT DABA_INT.OID_CLIENTE FROM SAPR_TDATO_BANCARIO DABA_INT ")
                        QueryWhere.Append(" WHERE DABA_INT.BOL_ACTIVO = 1 ")
                        If filtro.Clientes IsNot Nothing AndAlso filtro.Clientes.Count > 0 Then
                            QueryWhere.Append(" AND DABA_INT.OID_CLIENTE = []OID_CLIENTE ")

                            If filtro.SubClientes IsNot Nothing AndAlso filtro.SubClientes.Count > 0 Then
                                QueryWhere.Append(" AND DABA_INT.OID_SUBCLIENTE = []OID_SUBCLIENTE ")

                                If filtro.PuntosServicio IsNot Nothing AndAlso filtro.PuntosServicio.Count > 0 Then
                                    QueryWhere.Append(" AND DABA_INT.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                                End If
                            End If
                        End If

                        QueryWhere.Append(" GROUP BY DABA_INT.OID_CLIENTE,DABA_INT.OID_DIVISA HAVING COUNT(DABA_INT.OID_DIVISA) > 1) ")
                        QueryWhere.Append(" AND DABA.BOL_ACTIVO = 1 ")

                        If filtro.Clientes IsNot Nothing AndAlso filtro.Clientes.Count > 0 Then
                            QueryWhere.Append(" AND DABA.OID_CLIENTE = []OID_CLIENTE ")

                            If filtro.SubClientes IsNot Nothing AndAlso filtro.SubClientes.Count > 0 Then
                                QueryWhere.Append(" AND DABA.OID_SUBCLIENTE = []OID_SUBCLIENTE ")

                                If filtro.PuntosServicio IsNot Nothing AndAlso filtro.PuntosServicio.Count > 0 Then
                                    QueryWhere.Append(" AND DABA.OID_PTO_SERVICIO = []OID_PTO_SERVICIO ")
                                End If
                            End If
                        End If

                        QueryWhere.Append(" ) ")

                    End If

                    If Not String.IsNullOrEmpty(filtro.NumeroExterno) Then

                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_EXTERNO = []COD_EXTERNO ", " R.COD_EXTERNO = []COD_EXTERNO "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_EXTERNO", ProsegurDbType.Identificador_Alfanumerico, filtro.NumeroExterno))

                    End If

                    If Not String.IsNullOrEmpty(filtro.Precinto) Then

                        QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.COD_PRECINTO = []COD_PRECINTO ", " B.COD_PRECINTO = []COD_PRECINTO "))
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PRECINTO", ProsegurDbType.Identificador_Alfanumerico, filtro.Precinto))

                    End If

                    QueryWhere.Append(If(QueryWhere.ToString <> " WHERE ", "AND", ""))
                    QueryWhere.Append(" B.OID_BULTO IN")
                    QueryWhere.Append("(SELECT LVL.OID_BULTO FROM SAPR_TLISTA_VALORXELEMENTO LVL " & Environment.NewLine & _
                    " INNER JOIN GEPR_TLISTA_VALOR LV ON LV.OID_LISTA_VALOR = LVL.OID_LISTA_VALOR " & Environment.NewLine & _
                    " AND LV.OID_LISTA_TIPO = LVL.OID_LISTA_TIPO  " & Environment.NewLine & _
                    " INNER JOIN GEPR_TLISTA_TIPO LT ON LT.OID_LISTA_TIPO = LV.OID_LISTA_TIPO " & Environment.NewLine & _
                    " AND LT.COD_TIPO = '01' " & Environment.NewLine & _
                    " AND LV.COD_VALOR = '01')")

                    ' Não é permitido Remesas sem Bultos
                    QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND (B.OID_BULTO IS NOT NULL OR B.OID_BULTO <> '') ", " (B.OID_BULTO IS NOT NULL OR B.OID_BULTO <> '') "))

                    'Elementos ya seleccionados (abonados) en la pantalla de abono no deberán ser mostrados (considerados) en la consulta
                    If filtro.IdentificadoresElementosSeleccionados IsNot Nothing AndAlso filtro.IdentificadoresElementosSeleccionados.Count > 0 Then
                        QueryWhere.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, filtro.IdentificadoresElementosSeleccionados, "OID_REMESA", cmd, If(QueryWhere.ToString <> " WHERE ", "AND", ""), "R", "", True))
                    End If

                    'Apenas NO ABONADO e ABONADO CON DIFERENCIAS
                    QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_ESTADO_ABONO IN ('NA','AD') AND B.COD_ESTADO_ABONO IN ('NA','AD') ",
                                  " R.COD_ESTADO_ABONO IN ('NA','AD') AND B.COD_ESTADO_ABONO IN ('NA','AD') "))


                    'Esse IF é para Abono Elemento [Declarado X Contado] PERU
                    'If Not filtro.ObtenerParaCalcularDiferencas Then
                    '    QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_ESTADO = []COD_ESTADO_R  AND R.COD_ESTADO NOT IN ('SU', 'AN', 'EE', 'EC') ", " R.COD_ESTADO = []COD_ESTADO_R  AND R.COD_ESTADO NOT IN ('SU', 'AN', 'EE', 'EC') "))
                    '    QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.COD_ESTADO = []COD_ESTADO_B ", " B.COD_ESTADO = []COD_ESTADO_B "))
                    'End If

                    'Se declarado
                    If abono.TipoValor = Enumeradores.TipoValor.Declarado Then
                        'Esse IF é para Abono Elemento [Declarado X Contado] PERU
                        If Not filtro.ObtenerParaCalcularDiferencas Then

                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_ESTADO IN ('PE', 'PR', 'ET') ", " R.COD_ESTADO = []COD_ESTADO_R "))
                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.COD_ESTADO IN ('CE', 'AB', 'ET') ", " B.COD_ESTADO = []COD_ESTADO_B "))



                            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_R", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoRemesa.Pendiente.RecuperarValor()))
                            'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_B", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoBulto.Cerrado.RecuperarValor()))
                        End If
                        QueryInner.AppendLine(" LEFT JOIN SAPR_TDECLARADO_EFECTIVO DEF ON R.OID_REMESA = DEF.OID_REMESA ")
                        QueryInner.AppendLine(" LEFT JOIN SAPR_TDECLARADO_MEDIO_PAGO DMP ON R.OID_REMESA = DMP.OID_REMESA ")
                        QueryWhere.AppendLine(IIf(QueryWhere.ToString <> " WHERE ", " AND (DEF.OID_REMESA IS NOT NULL OR DMP.OID_REMESA IS NOT NULL) ", " (DEF.OID_REMESA IS NOT NULL OR DMP.OID_REMESA IS NOT NULL) "))

                    Else
                        'Se contado
                        'Esse IF é para Abono Elemento [Declarado X Contado] PERU
                        If Not filtro.ObtenerParaCalcularDiferencas Then
                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND R.COD_ESTADO = []COD_ESTADO_R  AND R.COD_ESTADO NOT IN ('SU', 'AN', 'EE', 'EC') ", " R.COD_ESTADO = []COD_ESTADO_R  AND R.COD_ESTADO NOT IN ('SU', 'AN', 'EE', 'EC') "))
                            QueryWhere.Append(IIf(QueryWhere.ToString <> " WHERE ", " AND B.COD_ESTADO = []COD_ESTADO_B ", " B.COD_ESTADO = []COD_ESTADO_B "))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_R", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoRemesa.Procesada.RecuperarValor()))
                            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ESTADO_B", ProsegurDbType.Identificador_Alfanumerico, Enumeradores.EstadoBulto.Aberto.RecuperarValor()))
                        End If
                        QueryInner.AppendLine(" LEFT JOIN SAPR_TCONTADO_EFECTIVO CEF ON R.OID_REMESA = CEF.OID_REMESA ")
                        QueryInner.AppendLine(" LEFT JOIN SAPR_TCONTADO_MEDIO_PAGO CMP ON R.OID_REMESA = CMP.OID_REMESA ")
                        QueryWhere.AppendLine(IIf(QueryWhere.ToString <> " WHERE ", " AND (CEF.OID_REMESA IS NOT NULL OR CMP.OID_REMESA IS NOT NULL) ", " (CEF.OID_REMESA IS NOT NULL OR CMP.OID_REMESA IS NOT NULL) "))

                    End If


                    If QueryWhere.ToString = " WHERE " Then
                        QueryWhere = New StringBuilder
                    End If

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, QueryInner.ToString(), QueryWhere.ToString(), QuerySelect.ToString(), QueryOrder.ToString()))

                    cmd.CommandType = CommandType.Text

                    dtRemesas = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                End Using

                If dtRemesas IsNot Nothing AndAlso dtRemesas.Rows.Count > 0 Then

                    CargarAbonosValoresElementos(abonosValores, dtRemesas)

                    Dim identificadoresRemesa As List(Of String) = dtRemesas.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_REMESA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_REMESA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_REMESA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    CargarValoresAbonosElementos(abonosValores, identificadoresRemesa, abono.TipoValor)

                End If

            End If

            Return abonosValores
        End Function

        Public Shared Function ObtenerAbono(identificadorAbono As String, identificadorDelegacion As String) As Clases.Abono.Abono

            Dim abono As New Clases.Abono.Abono
            Dim abonosValores As List(Of Clases.Abono.AbonoValor) = Nothing

            Dim filtro As New Prosegur.Genesis.Comon.Clases.Transferencias.FiltroConsultaAbono
            filtro.IdentificadorAbono = identificadorAbono
            filtro.IdentificadorDelegacion = identificadorDelegacion
            Dim abonos = RecuperarAbonos(filtro)

            If abonos IsNot Nothing AndAlso abonos.Count > 0 Then

                Dim dtAbonoValores As DataTable = Nothing
                abono = abonos.FirstOrDefault

                'Buscar en la base abonos de acuerdo con el tipo
                If abono.TipoAbono = Enumeradores.TipoAbono.Elemento Then
                    ObtenerAbonosElemento(abono, abonosValores, dtAbonoValores, identificadorAbono)
                Else
                    ObtenerAbonosSaldo(abono, abonosValores, dtAbonoValores, identificadorAbono)

                    'Carregar SnapShots se existirem
                    abono.SnapshotsAbonoSaldo = CargarSnapShotsAbonosSaldos(identificadorAbono)

                    If abono.SnapshotsAbonoSaldo IsNot Nothing AndAlso abono.SnapshotsAbonoSaldo.Count > 0 _
                        AndAlso abonosValores IsNot Nothing AndAlso abonosValores.Count > 0 Then
                        For Each abonoValor In abonosValores
                            Dim snapShotSaldo = abono.SnapshotsAbonoSaldo _
                                                .FirstOrDefault(Function(s) s.AbonoSaldo.IdentificadorSnapshot = abonoValor.AbonoSaldo.IdentificadorSnapshot)

                            If snapShotSaldo IsNot Nothing Then
                                abonoValor.AbonoSaldo.Divisa = snapShotSaldo.AbonoSaldo.Divisa.Clonar
                                abonoValor.AbonoSaldo.ListaSaldoCuenta.Clear()
                            End If
                        Next

                    End If
                End If

                abono.AbonosValor = abonosValores

            End If

            Return abono

        End Function

        Private Shared Sub ObtenerAbonosSaldo(ByRef abono As Clases.Abono.Abono, ByRef abonosValores As List(Of Clases.Abono.AbonoValor), _
                                                      ByRef dtAbonoValores As DataTable, identificadorAbono As String)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbono))

                cmd.CommandText = My.Resources.RecuperarAbonosSaldos

                cmd.CommandType = CommandType.Text

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dtAbonoValores = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dtAbonoValores IsNot Nothing AndAlso dtAbonoValores.Rows.Count > 0 Then

                    'Preencher Abonos Valores
                    CargarAbonosValoresSaldos(abonosValores, dtAbonoValores)

                    'Carrega os valores dos abonos valores
                    CargarValoresAbonosValores(abonosValores, identificadorAbono)

                End If
            End Using

        End Sub

        Private Shared Sub CargarTerminosSaldos(ByRef abonosValores As List(Of Clases.Abono.AbonoValor))
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                Dim listaIndentificadoresAbonosSaldos = (From abonoValor In abonosValores Select abonoValor.AbonoSaldo.Identificador Distinct).ToList()

                Dim clausulaIn = listaIndentificadoresAbonosSaldos.FirstOrDefault()
                For index = 1 To listaIndentificadoresAbonosSaldos.Count()
                    clausulaIn += ("," + listaIndentificadoresAbonosSaldos(index))
                Next

                cmd.CommandText = String.Format(My.Resources.RecuperarTerminosSaldos, clausulaIn)
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dtSaldoTerminos = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dtSaldoTerminos.Rows.Count > 0 Then

                    For Each rowSnapShot In dtSaldoTerminos.Rows

                        Dim abonoValorSelecionado = (From av In abonosValores Where av.AbonoSaldo.Identificador = Util.AtribuirValorObj(rowSnapShot("OID_ABONO_SALDO"), GetType(String))).FirstOrDefault()

                        If abonoValorSelecionado IsNot Nothing Then
                            Dim terminoIAC As New Clases.TerminoIAC()
                            With terminoIAC
                                .Identificador = Util.AtribuirValorObj(rowSnapShot("OID_TERMINO"), GetType(String))
                                .Valor = Util.AtribuirValorObj(rowSnapShot("DES_VALOR"), GetType(String))
                                .Longitud = Util.AtribuirValorObj(rowSnapShot("NEC_LONGITUD"), GetType(Integer))
                                .TieneValoresPosibles = Util.AtribuirValorObj(rowSnapShot("BOL_VALORES_POSIBLES"), GetType(Boolean))
                                .AceptarDigitacion = Util.AtribuirValorObj(rowSnapShot("BOL_ACEPTAR_DIGITACION"), GetType(Boolean))
                            End With

                            abonoValorSelecionado.AbonoSaldo.ListaTerminoIAC.Add(terminoIAC)
                        End If
                    Next

                End If
            End Using
        End Sub

        Private Shared Function CargarSnapShotsAbonosSaldos(identificadorAbono As String) As List(Of Clases.Abono.AbonoValor)

            Dim abonosValores As List(Of Clases.Abono.AbonoValor) = Nothing

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                cmd.CommandType = CommandType.Text
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarAbonosSnapShot)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Objeto_Id, identificadorAbono))

                Dim dtAbonosValoresSnapShot = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dtAbonosValoresSnapShot IsNot Nothing AndAlso dtAbonosValoresSnapShot.Rows.Count > 0 Then

                    CargarDivisasAbonarSaldos(abonosValores, dtAbonosValoresSnapShot)

                End If

            End Using

            Return abonosValores

        End Function

        Private Shared Sub ObtenerAbonosElemento(ByRef abono As Clases.Abono.Abono, ByRef abonosValores As List(Of Clases.Abono.AbonoValor), _
                                                      ByRef dtAbonoValores As DataTable, identificadorAbono As String)
            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ABONO", ProsegurDbType.Identificador_Alfanumerico, identificadorAbono))

                cmd.CommandText = My.Resources.AbonoRecuperarAbonoValores
                cmd.CommandType = CommandType.Text

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)

                dtAbonoValores = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dtAbonoValores IsNot Nothing AndAlso dtAbonoValores.Rows.Count > 0 Then

                    CargarAbonosValoresElemento(abonosValores, dtAbonoValores)

                    'Carrega os valores dos abonos valores
                    CargarValoresAbonosValores(abonosValores, identificadorAbono)

                    'Completa os dados da divisa dos abonos elementos
                    abonosValores.ForEach(Sub(a)
                                              If a.Divisa IsNot Nothing AndAlso a.AbonoElemento IsNot Nothing AndAlso
                                                  a.AbonoElemento.Divisa IsNot Nothing Then
                                                  a.AbonoElemento.Divisa.CodigoISO = a.Divisa.CodigoISO
                                                  a.AbonoElemento.Divisa.Descripcion = a.Divisa.Descripcion
                                                  a.AbonoElemento.Divisa.Color = a.Divisa.Color
                                              End If
                                          End Sub)

                    Dim identificadoresRemesa As List(Of String) = dtAbonoValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_REMESA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_REMESA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_REMESA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    Dim tipoValor As Enumeradores.TipoValor
                    If abono.TipoValor = Clases.Abono.TipoValorAbono.Declarados Then
                        tipoValor = Enumeradores.TipoValor.Declarado
                    Else
                        tipoValor = Enumeradores.TipoValor.Contado
                    End If

                    'Carrega os valores das remessa (AbonoElemento)
                    CargarValoresAbonosElementos(abonosValores, identificadoresRemesa, tipoValor)

                End If

            End Using
        End Sub

        Private Shared Sub CargarValoresAbonosValores(ByRef abonosValores As List(Of Clases.Abono.AbonoValor), identificadorAbono As String)

            If abonosValores IsNot Nothing AndAlso abonosValores.Count > 0 AndAlso Not String.IsNullOrEmpty(identificadorAbono) Then

                Dim dtAbonosValoresValores = Genesis.Denominacion.ObtenerValoresAbonosValores(identificadorAbono)

                If dtAbonosValoresValores IsNot Nothing AndAlso dtAbonosValoresValores.Rows.Count > 0 Then

                    Dim identificadoresDivisas As List(Of String) = dtAbonosValoresValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    Dim identificadoresDenominaciones As List(Of String) = dtAbonosValoresValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DEN_MP") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DEN_MP")) AndAlso _
                                                                                    r.Field(Of String)("TIPO_MP") = "EF") _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DEN_MP")) _
                                                                         .Distinct() _
                                                                         .ToList()

                    Dim identificadoresMediosPagos As List(Of String) = dtAbonosValoresValores.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DEN_MP") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DEN_MP")) AndAlso _
                                                                                    r.Field(Of String)("TIPO_MP") = "MP") _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DEN_MP")) _
                                                                         .Distinct() _
                                                                         .ToList()


                    Dim divisas = Genesis.Divisas.ObtenerDivisasPorIdentificadores_v2(identificadoresDivisas,
                                                                                  identificadoresDenominaciones,
                                                                                  identificadoresMediosPagos)
                    If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                        For Each row In dtAbonosValoresValores.Rows

                            Dim abonoValor = abonosValores.FirstOrDefault(Function(a) a.Identificador = Util.AtribuirValorObj(row("OID_ABONO_VALOR"), GetType(String)))

                            If abonoValor IsNot Nothing Then

                                If abonoValor.Divisa IsNot Nothing Then

                                    With abonoValor.Divisa

                                        'recupera a divisa com todas as informações para preencher o objeto divisa do abono valor
                                        Dim objDivisa = divisas.FirstOrDefault(Function(d) d.Identificador = abonoValor.Divisa.Identificador)

                                        If objDivisa IsNot Nothing Then

                                            .CodigoISO = objDivisa.CodigoISO
                                            .Color = objDivisa.Color
                                            .Descripcion = objDivisa.Descripcion

                                            'Se for efectivo
                                            If Util.AtribuirValorObj(row("TIPO_MP"), GetType(String)) = "EF" Then

                                                'Efectivo detallado
                                                If Util.AtribuirValorObj(row("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then

                                                    If abonoValor.Divisa.ListaEfectivo Is Nothing Then abonoValor.Divisa.ListaEfectivo = New List(Of Clases.Abono.EfectivoAbono)

                                                    Dim detalle = abonoValor.Divisa.ListaEfectivo.FirstOrDefault(Function(l) l.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                                    If detalle Is Nothing Then
                                                        If objDivisa.Denominaciones IsNot Nothing AndAlso objDivisa.Denominaciones.Count > 0 Then

                                                            Dim denominacion = objDivisa.Denominaciones.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))
                                                            If denominacion IsNot Nothing Then
                                                                detalle = New Clases.Abono.EfectivoAbono()
                                                                With detalle
                                                                    .Identificador = denominacion.Identificador
                                                                    .Codigo = denominacion.Codigo
                                                                    .Descripcion = denominacion.Descripcion
                                                                    .EsBillete = denominacion.EsBillete
                                                                    .Peso = denominacion.Peso
                                                                    .Valor = denominacion.Valor
                                                                    Select Case Util.AtribuirValorObj(row("COD_TIPO_VALORES"), GetType(String))
                                                                        Case Clases.Abono.TipoValorAbono.Declarados.RecuperarValor()
                                                                            .TipoValor = Enumeradores.TipoValor.Declarado
                                                                        Case Clases.Abono.TipoValorAbono.Declarados.RecuperarValor()
                                                                            .TipoValor = Enumeradores.TipoValor.Contado
                                                                    End Select
                                                                    .Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                                    .Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                                                    .TipoNivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado
                                                                End With
                                                                abonoValor.Divisa.ListaEfectivo.Add(detalle)
                                                            End If
                                                        End If
                                                    End If

                                                Else
                                                    'Se efectivo total
                                                    If abonoValor.Divisa.Totales Is Nothing Then
                                                        abonoValor.Divisa.Totales = New Clases.Abono.TotalesAbono
                                                        abonoValor.Divisa.Totales.CodigoTipoEfectivoTotal = Util.AtribuirValorObj(row("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
                                                    End If
                                                    abonoValor.Divisa.Totales.TotalEfectivo += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                End If

                                            Else
                                                'Se for Medio Pago
                                                If Util.AtribuirValorObj(row("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then

                                                    If abonoValor.Divisa.ListaMedioPago Is Nothing Then abonoValor.Divisa.ListaMedioPago = New List(Of Clases.Abono.MedioPagoAbono)

                                                    Dim detalleMp = abonoValor.Divisa.ListaMedioPago.FirstOrDefault(Function(l) l.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                                    If detalleMp Is Nothing Then

                                                        If objDivisa.MediosPago IsNot Nothing AndAlso objDivisa.MediosPago.Count > 0 Then

                                                            Dim medioPago = objDivisa.MediosPago.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                                            If medioPago IsNot Nothing Then
                                                                detalleMp = New Clases.Abono.MedioPagoAbono
                                                                With detalleMp
                                                                    .Identificador = medioPago.Identificador
                                                                    .Codigo = medioPago.Codigo
                                                                    .Descripcion = medioPago.Descripcion
                                                                    .TipoMedioPago = medioPago.Tipo
                                                                    Select Case Util.AtribuirValorObj(row("COD_TIPO_VALORES"), GetType(String))
                                                                        Case Clases.Abono.TipoValorAbono.Declarados.RecuperarValor()
                                                                            .TipoValor = Enumeradores.TipoValor.Declarado
                                                                        Case Clases.Abono.TipoValorAbono.Declarados.RecuperarValor()
                                                                            .TipoValor = Enumeradores.TipoValor.Contado
                                                                    End Select
                                                                    .Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                                    .Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                                                    .TipoNivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado
                                                                End With
                                                                abonoValor.Divisa.ListaMedioPago.Add(detalleMp)
                                                            End If
                                                        End If
                                                    End If

                                                Else
                                                    'Se medio pago total
                                                    If abonoValor.Divisa.Totales Is Nothing Then abonoValor.Divisa.Totales = New Clases.Abono.TotalesAbono

                                                    Select Case Util.AtribuirValorObj(row("COD_TIPO_MEDIO_PAGO"), GetType(String))
                                                        Case Enumeradores.TipoMedioPago.Cheque.RecuperarValor()
                                                            abonoValor.Divisa.Totales.TotalCheque += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                        Case Enumeradores.TipoMedioPago.OtroValor.RecuperarValor()
                                                            abonoValor.Divisa.Totales.TotalOtroValor += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                        Case Enumeradores.TipoMedioPago.Tarjeta.RecuperarValor()
                                                            abonoValor.Divisa.Totales.TotalTarjeta += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                        Case Enumeradores.TipoMedioPago.Ticket.RecuperarValor()
                                                            abonoValor.Divisa.Totales.TotalTicket += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                        Case Enumeradores.TipoMedioPago.NoDefinido.RecuperarValor()

                                                    End Select
                                                End If

                                            End If

                                        End If

                                    End With

                                End If

                            End If

                        Next

                    End If
                End If

            End If

        End Sub

        Private Shared Sub CargarTerminosAbonoSaldo(ByRef abonosValores As List(Of Clases.Abono.AbonoValor), identificadorAbono As String)

        End Sub

        Public Shared Sub CargarAbonosValoresElemento(ByRef abonosValores As List(Of Clases.Abono.AbonoValor), dtAbonosValores As DataTable)

            If dtAbonosValores IsNot Nothing AndAlso dtAbonosValores.Rows.Count > 0 Then

                If abonosValores Is Nothing Then abonosValores = New List(Of Clases.Abono.AbonoValor)

                For Each rowAbonoValorElemento In dtAbonosValores.Rows

                    If abonosValores.Find(Function(r) r.Identificador = rowAbonoValorElemento("OID_ABONO_VALOR")) Is Nothing Then

                        Dim abonoValor As Clases.Abono.AbonoValor = CargarAbonoValor(rowAbonoValorElemento)

                        abonoValor.AbonoElemento = New Clases.Abono.AbonoElemento()
                        With abonoValor.AbonoElemento
                            .IdentificadorRemesa = Util.AtribuirValorObj(rowAbonoValorElemento("OID_REMESA"), GetType(String))
                            .IdentificadorBulto = Util.AtribuirValorObj(rowAbonoValorElemento("OID_BULTO"), GetType(String))
                            .CodigoElemento = Util.AtribuirValorObj(rowAbonoValorElemento("R_COD_EXTERNO"), GetType(String))
                            .Codigo = Util.AtribuirValorObj(rowAbonoValorElemento("COD_ABONO_ELEMENTO"), GetType(String))
                            .Divisa = New Clases.Abono.DivisaAbono()
                            .Divisa.Identificador = Util.AtribuirValorObj(rowAbonoValorElemento("OID_DIVISA"), GetType(String))
                        End With

                        abonosValores.Add(abonoValor)
                    End If
                Next
            End If

        End Sub

        Public Shared Sub CargarAbonosValoresSaldos(ByRef abonosValores As List(Of Clases.Abono.AbonoValor), dtAbonosValores As DataTable)
            If dtAbonosValores IsNot Nothing AndAlso dtAbonosValores.Rows.Count > 0 Then

                If abonosValores Is Nothing Then abonosValores = New List(Of Clases.Abono.AbonoValor)

                For Each rowAbonoValor In dtAbonosValores.Rows

                    If abonosValores.Find(Function(r) r.Identificador = rowAbonoValor("OID_ABONO_VALOR")) Is Nothing Then

                        Dim abonoValor As Clases.Abono.AbonoValor = CargarAbonoValor(rowAbonoValor)

                        abonoValor.AbonoSaldo = New Clases.Abono.AbonoSaldo()
                        With abonoValor.AbonoSaldo
                            .Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_ABONO_SALDO"), GetType(String))
                            .IdentificadorDocumento = Util.AtribuirValorObj(rowAbonoValor("OID_DOCUMENTO"), GetType(String))
                            .IdentificadorSnapshot = Util.AtribuirValorObj(rowAbonoValor("OID_ABONO_SNAPSHOT"), GetType(String))
                            '.Divisa = New Clases.Abono.DivisaAbono()
                            '.Divisa.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_DIVISA"), GetType(String))
                        End With

                        abonosValores.Add(abonoValor)
                    End If
                Next
            End If
        End Sub

        Private Shared Function CargarAbonoValor(rowAbonoValor As System.Data.DataRow) As Clases.Abono.AbonoValor
            Dim abonoValor As New Clases.Abono.AbonoValor()

            Dim bancoCuenta As New Clases.Abono.BancoInformacion()
            bancoCuenta.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_BANCO"), GetType(String))

            With abonoValor

                .Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_ABONO_VALOR"), GetType(String))
                .BancoCuenta = bancoCuenta
                .Cuenta.CodigoTipoCuentaBancaria = Util.AtribuirValorObj(rowAbonoValor("COD_TIPO_CUENTA_BANCARIA"), GetType(String))
                .Cuenta.CodigoCuentaBancaria = Util.AtribuirValorObj(rowAbonoValor("COD_CUENTA_BANCARIA"), GetType(String))
                .Cuenta.CodigoDocumento = Util.AtribuirValorObj(rowAbonoValor("COD_DOCUMENTO"), GetType(String))
                .Cuenta.DescripcionTitularidad = Util.AtribuirValorObj(rowAbonoValor("DES_TITULARIDAD"), GetType(String))
                .Cuenta.Observaciones = Util.AtribuirValorObj(rowAbonoValor("DES_OBSERVACIONES"), GetType(String))
                .Observaciones = Util.AtribuirValorObj(rowAbonoValor("DES_OBSERVACIONES"), GetType(String))
                .Importe = Util.AtribuirValorObj(rowAbonoValor("NUM_IMPORTE"), GetType(Double))

                .Divisa = New Clases.Abono.DivisaAbono()
                .Divisa.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_DIVISA"), GetType(String))

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValor("OID_CLIENTE"), GetType(String))) Then
                    .Cliente = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_CLIENTE"), GetType(String)),
                                                           .Codigo = Util.AtribuirValorObj(rowAbonoValor("COD_CLIENTE"), GetType(String)),
                                                           .Descripcion = Util.AtribuirValorObj(rowAbonoValor("DES_CLIENTE"), GetType(String))}
                End If
                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValor("OID_SUBCLIENTE"), GetType(String))) Then
                    .SubCliente = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_SUBCLIENTE"), GetType(String)),
                                                                  .Codigo = Util.AtribuirValorObj(rowAbonoValor("COD_SUBCLIENTE"), GetType(String)),
                                                                  .Descripcion = Util.AtribuirValorObj(rowAbonoValor("DES_SUBCLIENTE"), GetType(String))}
                End If

                If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValor("OID_PTO_SERVICIO"), GetType(String))) Then
                    .PtoServicio = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValor("OID_PTO_SERVICIO"), GetType(String)),
                                                                   .Codigo = Util.AtribuirValorObj(rowAbonoValor("COD_PTO_SERVICIO"), GetType(String)),
                                                                   .Descripcion = Util.AtribuirValorObj(rowAbonoValor("DES_PTO_SERVICIO"), GetType(String))}
                End If
            End With

            Return abonoValor
        End Function

        Public Shared Sub CargarDivisasAbonarSaldos(ByRef abonosValores As List(Of Clases.Abono.AbonoValor),
                                                    dtSaldos As DataTable,
                                                    Optional tipoAbono As Enumeradores.TipoAbono = Enumeradores.TipoAbono.NoDefinido,
                                                    Optional valoresCeroYNegativos As Boolean = False)

            If abonosValores Is Nothing Then abonosValores = New List(Of Clases.Abono.AbonoValor)

            'Agrupa por cuenta e divisa, assim obtem a quantidade de abonos valores para retornar
            Dim resultsAbonosValores = (From row In dtSaldos.AsEnumerable()
                                        Where (Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal)) <> 0 OrElse Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64)) <> 0)
                                        Group row By IdCliente = row.Field(Of String)("OID_CLIENTE"), IdSubCliente = row.Field(Of String)("OID_SUBCLIENTE"), IdPtoServicio = row.Field(Of String)("OID_PTO_SERVICIO"), IdSubCanal = row.Field(Of String)("OID_SUBCANAL"), IdDivisa = row.Field(Of String)("OID_DIVISA") Into Group
                                        Select IdCliente, IdSubCliente, IdPtoServicio, IdSubCanal, IdDivisa, Group)

            If resultsAbonosValores IsNot Nothing AndAlso resultsAbonosValores.Count > 0 Then

                Dim identificadoresDivisas As List(Of String) = dtSaldos.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresDenominaciones As List(Of String) = dtSaldos.AsEnumerable() _
                                                                     .Where(Function(r) r.Field(Of String)("OID_DEN_MP") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DEN_MP")) AndAlso _
                                                                                r.Field(Of String)("OID_DEN_MP") = "EFECTIVO") _
                                                                     .Select(Function(r) r.Field(Of String)("OID_DEN_MP")) _
                                                                     .Distinct() _
                                                                     .ToList()

                Dim identificadoresMediosPagos As List(Of String) = dtSaldos.AsEnumerable() _
                                                                     .Where(Function(r) r.Field(Of String)("OID_DEN_MP") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DEN_MP")) AndAlso _
                                                                                r.Field(Of String)("OID_DEN_MP") <> "EFECTIVO") _
                                                                     .Select(Function(r) r.Field(Of String)("OID_DEN_MP")) _
                                                                     .Distinct() _
                                                                     .ToList()

                Dim divisas = Genesis.Divisas.ObtenerDivisasPorIdentificadores_v2(identificadoresDivisas, identificadoresDenominaciones, identificadoresMediosPagos)

                If divisas IsNot Nothing AndAlso divisas.Count > 0 Then

                    For Each av In resultsAbonosValores
                        Dim abonoValor = New Clases.Abono.AbonoValor()
                        abonoValor.MultiplesCuentas = Util.AtribuirValorObj(av.Group(0)("CUANT_CUENTAS"), GetType(Integer)) > 1
                        abonoValor.Cliente.Identificador = av.IdCliente ' Util.AtribuirValorObj(row("OID_CLIENTE"), GetType(String))
                        abonoValor.Cliente.Codigo = Util.AtribuirValorObj(av.Group(0)("COD_CLIENTE"), GetType(String))
                        abonoValor.Cliente.Descripcion = Util.AtribuirValorObj(av.Group(0)("DES_CLIENTE"), GetType(String))
                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(av.Group(0)("OID_SUBCLIENTE"), GetType(String))) Then
                            abonoValor.SubCliente.Identificador = av.IdSubCliente ' Util.AtribuirValorObj(row("OID_SUBCLIENTE"), GetType(String))
                            abonoValor.SubCliente.Codigo = Util.AtribuirValorObj(av.Group(0)("COD_SUBCLIENTE"), GetType(String))
                            abonoValor.SubCliente.Descripcion = Util.AtribuirValorObj(av.Group(0)("DES_SUBCLIENTE"), GetType(String))
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(av.Group(0)("OID_PTO_SERVICIO"), GetType(String))) Then
                                abonoValor.PtoServicio.Identificador = av.IdPtoServicio ' Util.AtribuirValorObj(row("OID_PTO_SERVICIO"), GetType(String))
                                abonoValor.PtoServicio.Codigo = Util.AtribuirValorObj(av.Group(0)("COD_PTO_SERVICIO"), GetType(String))
                                abonoValor.PtoServicio.Descripcion = Util.AtribuirValorObj(av.Group(0)("DES_PTO_SERVICIO"), GetType(String))
                            End If
                        End If


                        If av.Group(0).Table.Columns.Contains("OID_ABONO_SNAPSHOT") Then
                            abonoValor.AbonoSaldo.IdentificadorSnapshot = Util.AtribuirValorObj(av.Group(0)("OID_ABONO_SNAPSHOT"), GetType(String))
                        Else
                            abonoValor.AbonoSaldo.IdentificadorSnapshot = Guid.NewGuid().ToString()
                        End If


                        abonoValor.AbonoSaldo.SubCanal.Identificador = av.IdSubCanal ' Util.AtribuirValorObj(row("OID_SUBCANAL"), GetType(String))
                        abonoValor.AbonoSaldo.SubCanal.Codigo = Util.AtribuirValorObj(av.Group(0)("COD_SUBCANAL"), GetType(String))
                        abonoValor.AbonoSaldo.SubCanal.Descripcion = Util.AtribuirValorObj(av.Group(0)("DES_SUBCANAL"), GetType(String))

                        abonoValor.AbonoSaldo.Canal.Identificador = Util.AtribuirValorObj(av.Group(0)("OID_CANAL"), GetType(String))
                        abonoValor.AbonoSaldo.Canal.Codigo = Util.AtribuirValorObj(av.Group(0)("COD_CANAL"), GetType(String))
                        abonoValor.AbonoSaldo.Canal.Descripcion = Util.AtribuirValorObj(av.Group(0)("DES_CANAL"), GetType(String))

                        'Seleciona a divisa da lista para preencher dados
                        Dim divisa = divisas.Find(Function(d) d.Identificador = av.IdDivisa)
                        abonoValor.AbonoSaldo.Divisa.CodigoISO = divisa.CodigoISO
                        abonoValor.AbonoSaldo.Divisa.Identificador = divisa.Identificador
                        abonoValor.AbonoSaldo.Divisa.Descripcion = divisa.Descripcion
                        abonoValor.AbonoSaldo.Divisa.Color = divisa.Color
                        abonosValores.Add(abonoValor)

                        For Each row In av.Group
                            Dim abonoSaldoCuenta = (From a In abonoValor.AbonoSaldo.ListaSaldoCuenta
                                                   Where a.IdentificadorCuenta = row.Field(Of String)("OID_CUENTA_SALDO") AndAlso
                                                   a.Divisa.Identificador = av.IdDivisa).FirstOrDefault()
                            If (abonoSaldoCuenta Is Nothing) Then
                                abonoSaldoCuenta = New Clases.Abono.SnapshotSaldo()
                                abonoSaldoCuenta.IdentificadorCuenta = row.Field(Of String)("OID_CUENTA_SALDO")
                                abonoSaldoCuenta.Divisa = abonoValor.AbonoSaldo.Divisa.Clonar()
                                abonoValor.AbonoSaldo.ListaSaldoCuenta.Add(abonoSaldoCuenta)
                            End If

                            'Se efetivo
                            If Util.AtribuirValorObj(row("TIPO_MEDIO_PAGO"), GetType(String)) = "EFECTIVO" Then
                                'Se detalhado
                                If Util.AtribuirValorObj(row("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then
                                    If abonoSaldoCuenta.Divisa.ListaEfectivo Is Nothing Then abonoSaldoCuenta.Divisa.ListaEfectivo = New List(Of Clases.Abono.EfectivoAbono)

                                    Dim efectivo = abonoSaldoCuenta.Divisa.ListaEfectivo.Find(Function(e) e.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                    If efectivo Is Nothing Then

                                        Dim denominacion = divisa.Denominaciones.Find(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                        If denominacion IsNot Nothing Then
                                            efectivo = New Clases.Abono.EfectivoAbono()

                                            With efectivo
                                                .Identificador = denominacion.Identificador
                                                .Codigo = denominacion.Codigo
                                                .Descripcion = denominacion.Descripcion
                                                .EsBillete = denominacion.EsBillete
                                                .Peso = denominacion.Peso
                                                .Valor = denominacion.Valor
                                                .TipoNivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado
                                                .TipoValor = Enumeradores.TipoValor.Disponible
                                                .Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                .Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                                .IdentificadorCalidad = Util.AtribuirValorObj(row("OID_CALIDAD"), GetType(String))
                                                .IdentificadorUnidadeMedida = Util.AtribuirValorObj(row("OID_UNIDAD_MEDIDA"), GetType(String))
                                            End With
                                            abonoSaldoCuenta.Divisa.ListaEfectivo.Add(efectivo)
                                        End If

                                    Else
                                        efectivo.Cantidad += Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                        efectivo.Importe += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                    End If

                                Else
                                    'Se Total
                                    If abonoSaldoCuenta.Divisa.Totales Is Nothing Then abonoSaldoCuenta.Divisa.Totales = New Clases.Abono.TotalesAbono

                                    With abonoSaldoCuenta.Divisa.Totales
                                        .TotalEfectivo += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                        .CodigoTipoEfectivoTotal = Util.AtribuirValorObj(row("COD_TIPO_EFECTIVO_TOTAL"), GetType(String))
                                    End With

                                End If

                            Else

                                'Se Medio Pago e detalhado
                                If Util.AtribuirValorObj(row("COD_NIVEL_DETALLE"), GetType(String)) = Enumeradores.TipoNivelDetalhe.Detalhado.RecuperarValor() Then

                                    If abonoSaldoCuenta.Divisa.ListaMedioPago Is Nothing Then abonoSaldoCuenta.Divisa.ListaMedioPago = New List(Of Clases.Abono.MedioPagoAbono)

                                    Dim mp = abonoSaldoCuenta.Divisa.ListaMedioPago.Find(Function(e) e.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                    If mp Is Nothing Then

                                        Dim medioPago = divisa.MediosPago.Find(Function(d) d.Identificador = Util.AtribuirValorObj(row("OID_DEN_MP"), GetType(String)))

                                        If medioPago IsNot Nothing Then
                                            mp = New Clases.Abono.MedioPagoAbono()

                                            With mp
                                                .Identificador = medioPago.Identificador
                                                .Codigo = medioPago.Codigo
                                                .Descripcion = medioPago.Descripcion
                                                Select Case Util.AtribuirValorObj(row("TIPO_MEDIO_PAGO"), GetType(String))
                                                    Case Enumeradores.TipoMedioPago.Cheque.RecuperarValor()
                                                        .TipoMedioPago = Enumeradores.TipoMedioPago.Cheque
                                                        .DescripcionTipoMedioPago = Traduzir("071_Comon_Valores_TipoMedioPago_codtipob").ToUpper()
                                                    Case Enumeradores.TipoMedioPago.OtroValor.RecuperarValor()
                                                        .TipoMedioPago = Enumeradores.TipoMedioPago.OtroValor
                                                        .DescripcionTipoMedioPago = Traduzir("071_Comon_Valores_TipoMedioPago_codtipo").ToUpper()
                                                    Case Enumeradores.TipoMedioPago.Tarjeta.RecuperarValor()
                                                        .TipoMedioPago = Enumeradores.TipoMedioPago.Tarjeta
                                                        .DescripcionTipoMedioPago = Traduzir("071_Comon_Valores_TipoMedioPago_codtipoc").ToUpper()
                                                    Case Enumeradores.TipoMedioPago.Ticket.RecuperarValor()
                                                        .TipoMedioPago = Enumeradores.TipoMedioPago.Ticket
                                                        .DescripcionTipoMedioPago = Traduzir("071_Comon_Valores_TipoMedioPago_codtipoa").ToUpper()
                                                End Select
                                                .TipoValor = Enumeradores.TipoValor.Disponible
                                                .Importe = Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                                .Cantidad = Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                                .IdentificadorUnidadeMedida = Util.AtribuirValorObj(row("OID_UNIDAD_MEDIDA"), GetType(String))
                                                .TipoNivelDetalle = Enumeradores.TipoNivelDetalhe.Detalhado
                                            End With
                                            abonoSaldoCuenta.Divisa.ListaMedioPago.Add(mp)
                                        End If

                                    Else
                                        mp.Cantidad += Util.AtribuirValorObj(row("CANTIDAD"), GetType(Int64))
                                        mp.Importe += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                    End If

                                Else
                                    'Se Total
                                    If abonoSaldoCuenta.Divisa.Totales Is Nothing Then abonoSaldoCuenta.Divisa.Totales = New Clases.Abono.TotalesAbono

                                    With abonoSaldoCuenta.Divisa.Totales

                                        Select Case Util.AtribuirValorObj(row("TIPO_MEDIO_PAGO"), GetType(String))
                                            Case Enumeradores.TipoMedioPago.Cheque.RecuperarValor()
                                                .TotalCheque += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                            Case Enumeradores.TipoMedioPago.OtroValor.RecuperarValor()
                                                .TotalOtroValor += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                            Case Enumeradores.TipoMedioPago.Tarjeta.RecuperarValor()
                                                .TotalTarjeta += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                            Case Enumeradores.TipoMedioPago.Ticket.RecuperarValor()
                                                .TotalTicket += Util.AtribuirValorObj(row("IMPORTE"), GetType(Decimal))
                                        End Select
                                    End With
                                End If
                            End If
                        Next
                        'Preenche a Divisa do AbonoSaldo (Agrupação que não considera a conta)
                        'Preenche o importe total do histórico e abono saldo
                        abonoValor.AbonoSaldo.ListaSaldoCuenta _
                            .ForEach(Sub(asv)
                                         If asv.Divisa.ListaEfectivo IsNot Nothing Then
                                             For Each efec In asv.Divisa.ListaEfectivo
                                                 asv.Importe += efec.Importe
                                                 Dim efecS = abonoValor.AbonoSaldo.Divisa.ListaEfectivo.FirstOrDefault(Function(e) e.Codigo = efec.Codigo)
                                                 If efecS Is Nothing Then
                                                     efecS = efec.Clonar()
                                                     abonoValor.AbonoSaldo.Divisa.ListaEfectivo.Add(efecS)
                                                 Else
                                                     efecS.Importe += efec.Importe
                                                     efecS.Cantidad += efec.Cantidad
                                                 End If
                                             Next
                                         End If
                                         If asv.Divisa.ListaMedioPago IsNot Nothing Then
                                             For Each mp In asv.Divisa.ListaMedioPago
                                                 asv.Importe += mp.Importe
                                                 Dim mpS = abonoValor.AbonoSaldo.Divisa.ListaMedioPago.FirstOrDefault(Function(e) e.Codigo = mp.Codigo)
                                                 If mpS Is Nothing Then
                                                     mpS = mp.Clonar()
                                                     abonoValor.AbonoSaldo.Divisa.ListaMedioPago.Add(mpS)
                                                 Else
                                                     mpS.Importe += mp.Importe
                                                     mp.Cantidad += mp.Cantidad
                                                 End If
                                             Next
                                         End If
                                         If asv.Divisa.Totales IsNot Nothing Then
                                             asv.Importe += asv.Divisa.Totales.TotalCheque
                                             asv.Importe += asv.Divisa.Totales.TotalOtroValor
                                             asv.Importe += asv.Divisa.Totales.TotalTarjeta
                                             asv.Importe += asv.Divisa.Totales.TotalTicket
                                             asv.Importe += asv.Divisa.Totales.TotalEfectivo
                                             abonoValor.AbonoSaldo.Divisa.Totales.TotalCheque += asv.Divisa.Totales.TotalCheque
                                             abonoValor.AbonoSaldo.Divisa.Totales.TotalOtroValor += asv.Divisa.Totales.TotalOtroValor
                                             abonoValor.AbonoSaldo.Divisa.Totales.TotalTarjeta += asv.Divisa.Totales.TotalTarjeta
                                             abonoValor.AbonoSaldo.Divisa.Totales.TotalTicket += asv.Divisa.Totales.TotalTicket
                                             abonoValor.AbonoSaldo.Divisa.Totales.TotalEfectivo += asv.Divisa.Totales.TotalEfectivo
                                             If (String.IsNullOrEmpty(abonoValor.AbonoSaldo.Divisa.Totales.CodigoTipoEfectivoTotal) OrElse
                                                 (abonoValor.AbonoSaldo.Divisa.Totales.CodigoTipoEfectivoTotal <> Enumeradores.TipoDetalleEfectivo.Mezcla.RecuperarValor() AndAlso
                                                  asv.Divisa.Totales.CodigoTipoEfectivoTotal = Enumeradores.TipoDetalleEfectivo.Mezcla.RecuperarValor())) Then
                                                 abonoValor.AbonoSaldo.Divisa.Totales.CodigoTipoEfectivoTotal = asv.Divisa.Totales.CodigoTipoEfectivoTotal
                                             End If
                                         End If
                                         abonoValor.AbonoSaldo.Importe += asv.Importe
                                     End Sub)
                    Next

                    ' se tipo abono é SALDOS ou PEDIDO e opção na tela de detallhe do abono (valores 0 e negativo) estiver desmarcada, limpar os abonos <= 0
                    If (tipoAbono = Enumeradores.TipoAbono.Saldos OrElse tipoAbono = Enumeradores.TipoAbono.Pedido) AndAlso Not valoresCeroYNegativos Then

                        If abonosValores IsNot Nothing AndAlso abonosValores.Count > 0 Then
                            abonosValores.RemoveAll(Function(x) x.AbonoSaldo Is Nothing OrElse (x.AbonoSaldo IsNot Nothing AndAlso x.AbonoSaldo.Importe <= 0.0))
                        End If

                    End If

                End If
            End If

        End Sub

        Public Shared Sub CargarAbonosValoresElementos(ByRef abonosValores As List(Of Clases.Abono.AbonoValor), dtAbonos As DataTable)

            If dtAbonos IsNot Nothing AndAlso dtAbonos.Rows.Count > 0 Then

                If abonosValores Is Nothing Then abonosValores = New List(Of Clases.Abono.AbonoValor)

                For Each rowAbonoValorElemento In dtAbonos.Rows

                    Dim remesaAnadida As Boolean = False
                    abonosValores.ForEach(Sub(a)
                                              If a.AbonoElemento IsNot Nothing AndAlso
                                                  a.AbonoElemento.IdentificadorRemesa = Util.AtribuirValorObj(rowAbonoValorElemento("OID_REMESA"), GetType(String)) Then
                                                  remesaAnadida = True
                                                  Exit Sub
                                              End If
                                          End Sub)

                    If Not remesaAnadida Then
                        Dim abonoValor As New Clases.Abono.AbonoValor()

                        With abonoValor
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValorElemento("OID_CLIENTE"), GetType(String))) Then
                                .Cliente = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValorElemento("OID_CLIENTE"), GetType(String)),
                                                                       .Codigo = Util.AtribuirValorObj(rowAbonoValorElemento("COD_CLIENTE"), GetType(String)),
                                                                       .Descripcion = Util.AtribuirValorObj(rowAbonoValorElemento("DES_CLIENTE"), GetType(String))}
                            End If
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValorElemento("OID_SUBCLIENTE"), GetType(String))) Then
                                .SubCliente = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValorElemento("OID_SUBCLIENTE"), GetType(String)),
                                                                              .Codigo = Util.AtribuirValorObj(rowAbonoValorElemento("COD_SUBCLIENTE"), GetType(String)),
                                                                              .Descripcion = Util.AtribuirValorObj(rowAbonoValorElemento("DES_SUBCLIENTE"), GetType(String))}
                            End If

                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(rowAbonoValorElemento("OID_PTO_SERVICIO"), GetType(String))) Then
                                .PtoServicio = New Clases.Abono.AbonoInformacion() With {.Identificador = Util.AtribuirValorObj(rowAbonoValorElemento("OID_PTO_SERVICIO"), GetType(String)),
                                                                               .Codigo = Util.AtribuirValorObj(rowAbonoValorElemento("COD_PTO_SERVICIO"), GetType(String)),
                                                                               .Descripcion = Util.AtribuirValorObj(rowAbonoValorElemento("DES_PTO_SERVICIO"), GetType(String))}
                            End If

                        End With

                        abonoValor.MultiplesCuentas = Util.AtribuirValorObj(rowAbonoValorElemento("CUANT_CUENTAS"), GetType(Integer)) > 1
                        abonoValor.AbonoElemento = New Clases.Abono.AbonoElemento()

                        With abonoValor.AbonoElemento
                            .IdentificadorRemesa = Util.AtribuirValorObj(rowAbonoValorElemento("OID_REMESA"), GetType(String))
                            .IdentificadorBulto = Util.AtribuirValorObj(rowAbonoValorElemento("OID_BULTO"), GetType(String))
                            .CodigoElemento = Util.AtribuirValorObj(rowAbonoValorElemento("R_COD_EXTERNO"), GetType(String))
                            '.Codigo = Util.AtribuirValorObj(rowAbonoValorElemento("COD_ABONO_ELEMENTO"), GetType(String))
                        End With

                        abonosValores.Add(abonoValor)
                    End If
                Next
            End If

        End Sub

        Private Shared Sub CargarValoresAbonosElementos(ByRef abonosValores As List(Of Clases.Abono.AbonoValor),
                                                        identificadoresRemesa As List(Of String),
                                                        tipoValor As Enumeradores.TipoValor)

            If abonosValores IsNot Nothing AndAlso abonosValores.Count > 0 AndAlso identificadoresRemesa IsNot Nothing AndAlso identificadoresRemesa.Count > 0 Then

                Dim dtValoresDenominaciones As DataTable = Nothing
                Dim dtValoresMedioPago As DataTable = Nothing
                Dim dtValoresTotales As DataTable = Nothing
                Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

                Dim TDenominaciones As New Task(Sub()
                                                    dtValoresDenominaciones = Genesis.Denominacion.ObtenerValoresElementoAbono(identificadoresRemesa, tipoValor)
                                                End Sub)
                TDenominaciones.Start()

                Dim TMedioPago As New Task(Sub()
                                               dtValoresMedioPago = Genesis.MedioPago.ObtenerValoresElementoAbono(identificadoresRemesa, tipoValor)
                                           End Sub)
                TMedioPago.Start()

                Dim TTotales As New Task(Sub()
                                             If tipoValor = Enumeradores.TipoValor.Declarado Then
                                                 dtValoresTotales = Genesis.Totales.ObtenerValoresElementoAbono(identificadoresRemesa, tipoValor)
                                             End If
                                         End Sub)
                TTotales.Start()

                Task.WaitAll(New Task() {TDenominaciones, TMedioPago, TTotales})

                Dim identificadoresDivisas As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList()

                identificadoresDivisas.AddRange(dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                         .Distinct() _
                                                                         .ToList())

                If (dtValoresTotales IsNot Nothing) Then
                    identificadoresDivisas.AddRange(dtValoresTotales.AsEnumerable() _
                                                                             .Where(Function(r) r.Field(Of String)("OID_DIVISA") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DIVISA"))) _
                                                                             .Select(Function(r) r.Field(Of String)("OID_DIVISA")) _
                                                                             .Distinct() _
                                                                             .ToList())
                End If

                Dim identificadoresDenominaciones As List(Of String) = dtValoresDenominaciones.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_DENOMINACION") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_DENOMINACION"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_DENOMINACION")) _
                                                                         .Distinct() _
                                                                         .ToList()

                Dim identificadoresMediosPagos As List(Of String) = dtValoresMedioPago.AsEnumerable() _
                                                                         .Where(Function(r) r.Field(Of String)("OID_MEDIO_PAGO") IsNot Nothing AndAlso Not String.IsNullOrEmpty(r.Field(Of String)("OID_MEDIO_PAGO"))) _
                                                                         .Select(Function(r) r.Field(Of String)("OID_MEDIO_PAGO")) _
                                                                         .Distinct() _
                                                                         .ToList()

                divisas = Genesis.Divisas.ObtenerDivisasPorIdentificadores_v2(identificadoresDivisas,
                                                                              identificadoresDenominaciones,
                                                                              identificadoresMediosPagos)



                If abonosValores Is Nothing Then abonosValores = New List(Of Clases.Abono.AbonoValor)

                For Each identificadorRemesa In identificadoresRemesa
                    For Each divisa In divisas
                        CargarValoresDivisaAbono(abonosValores, divisa, dtValoresDenominaciones, dtValoresMedioPago, dtValoresTotales, identificadorRemesa)
                    Next
                Next

                'Preenche o importe total do elemento
                abonosValores.ForEach(Sub(a)
                                          If a.AbonoElemento IsNot Nothing AndAlso a.AbonoElemento.Divisa IsNot Nothing Then
                                              If a.AbonoElemento.Divisa.ListaEfectivo IsNot Nothing Then

                                                  'Se os totais vier preenchido quando o tipo valor for contado
                                                  'quer dizer que teve diferença a nivel de total e não deve considerar valores contados
                                                  If tipoValor = Enumeradores.TipoValor.Contado AndAlso
                                                      a.AbonoElemento.Divisa.Totales IsNot Nothing AndAlso
                                                      a.AbonoElemento.Divisa.Totales.TotalEfectivo <> 0 Then

                                                      a.AbonoElemento.Divisa.ListaEfectivo.RemoveAll(Function(e) e.TipoValor = Enumeradores.TipoValor.Contado)
                                                  End If

                                                  a.AbonoElemento.Importe = (From i In a.AbonoElemento.Divisa.ListaEfectivo
                                                                             Select i.Importe).Sum()
                                              End If
                                              If a.AbonoElemento.Divisa.ListaMedioPago IsNot Nothing Then

                                                  'Se os totais vier preenchido quando o tipo valor for contado
                                                  'quer dizer que teve diferença a nivel de total e não deve considerar valores contados
                                                  If tipoValor = Enumeradores.TipoValor.Contado AndAlso
                                                      a.AbonoElemento.Divisa.Totales IsNot Nothing AndAlso
                                                      (a.AbonoElemento.Divisa.Totales.TotalCheque <> 0 OrElse
                                                      a.AbonoElemento.Divisa.Totales.TotalOtroValor <> 0 OrElse
                                                      a.AbonoElemento.Divisa.Totales.TotalTarjeta <> 0 OrElse
                                                      a.AbonoElemento.Divisa.Totales.TotalTicket <> 0) Then

                                                      a.AbonoElemento.Divisa.ListaMedioPago.RemoveAll(Function(mp) mp.TipoValor = Enumeradores.TipoValor.Contado)
                                                  End If


                                                  a.AbonoElemento.Importe += (From i In a.AbonoElemento.Divisa.ListaMedioPago
                                                                             Select i.Importe).Sum()
                                              End If
                                              If a.AbonoElemento.Divisa.Totales IsNot Nothing Then
                                                  a.AbonoElemento.Importe += a.AbonoElemento.Divisa.Totales.TotalCheque
                                                  a.AbonoElemento.Importe += a.AbonoElemento.Divisa.Totales.TotalOtroValor
                                                  a.AbonoElemento.Importe += a.AbonoElemento.Divisa.Totales.TotalTarjeta
                                                  a.AbonoElemento.Importe += a.AbonoElemento.Divisa.Totales.TotalTicket
                                                  a.AbonoElemento.Importe += a.AbonoElemento.Divisa.Totales.TotalEfectivo
                                              End If
                                          End If
                                      End Sub)

                abonosValores.RemoveAll(Function(a) a.AbonoElemento.Divisa Is Nothing)

            End If

        End Sub

        Private Shared Sub CargarValoresDivisaAbono(ByRef abonos As List(Of Clases.Abono.AbonoValor),
                                               divisa As Clases.Divisa,
                                               ByRef dtValoresDenominaciones As DataTable,
                                               ByRef dtValoresMedioPago As DataTable,
                                               ByRef dtValoresTotales As DataTable,
                                               identificadorRemesa As String)

            If Not String.IsNullOrEmpty(identificadorRemesa) AndAlso divisa IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(divisa.Identificador) Then

                Dim objDenominacion As Clases.Denominacion = Nothing
                Dim objMedioPago As Clases.MedioPago = Nothing
                Dim abonoValor As New Clases.Abono.AbonoValor

                ' Crea filtro
                Dim consulta As String = "OID_REMESA = '" & identificadorRemesa & "' AND OID_DIVISA = '" & divisa.Identificador & "'"

                ' Valores Denominaciones
                If dtValoresDenominaciones IsNot Nothing AndAlso dtValoresDenominaciones.Rows.Count > 0 Then

                    Dim _ValoresDenominaciones = dtValoresDenominaciones.Select(consulta)

                    If _ValoresDenominaciones IsNot Nothing Then

                        For Each _valor In _ValoresDenominaciones

                            abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso av.AbonoElemento.Divisa IsNot Nothing AndAlso
                                                                   av.AbonoElemento.Divisa.Identificador = divisa.Identificador AndAlso
                                                                   av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)

                            If abonoValor Is Nothing Then
                                abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso
                                                                       av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)

                            End If

                            If abonoValor Is Nothing Then
                                abonoValor = New Clases.Abono.AbonoValor()
                                abonos.Add(abonoValor)
                            End If

                            If abonoValor.AbonoElemento Is Nothing Then
                                abonoValor.AbonoElemento = New Clases.Abono.AbonoElemento()
                                abonoValor.AbonoElemento.IdentificadorRemesa = identificadorRemesa
                            End If

                            If abonoValor.AbonoElemento.Divisa Is Nothing OrElse
                               abonoValor.AbonoElemento.Divisa.Identificador <> divisa.Identificador Then

                                'Se já existe o elemento é porque ele está com outra divisa
                                'e deve-se criar outro abono valor, porém com a divisa do filtro
                                If abonoValor.AbonoElemento.Divisa IsNot Nothing Then
                                    'clona o objeto para ser utilizado novamente com outra divisa
                                    Dim objAbonoValorClone = abonoValor.Clonar()
                                    'objAbonoValorClone.Divisa = Nothing
                                    abonoValor = objAbonoValorClone
                                    abonos.Add(abonoValor)

                                End If

                                abonoValor.AbonoElemento.Divisa = New Clases.Abono.DivisaAbono() With {.Identificador = divisa.Identificador,
                                                                               .CodigoISO = divisa.CodigoISO,
                                                                               .Descripcion = divisa.Descripcion,
                                                                               .Color = divisa.Color}
                            End If


                            If _valor.Table.Columns.Contains("OID_DENOMINACION") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))) AndAlso _
                                       _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) AndAlso _
                                       divisa.Denominaciones IsNot Nothing AndAlso divisa.Denominaciones.Count > 0 Then

                                objDenominacion = (From den In divisa.Denominaciones Where den.Identificador = Util.AtribuirValorObj(_valor("OID_DENOMINACION"), GetType(String))).FirstOrDefault

                                If objDenominacion IsNot Nothing Then

                                    If abonoValor.AbonoElemento.Divisa.ListaEfectivo Is Nothing Then abonoValor.AbonoElemento.Divisa.ListaEfectivo = New List(Of Clases.Abono.EfectivoAbono)

                                    Dim efectivo As New Clases.Abono.EfectivoAbono
                                    With efectivo
                                        efectivo.Identificador = objDenominacion.Identificador
                                        efectivo.Codigo = objDenominacion.Codigo
                                        efectivo.Descripcion = objDenominacion.Descripcion
                                        efectivo.EsBillete = objDenominacion.EsBillete
                                        efectivo.Importe = Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal))
                                        efectivo.Cantidad = Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64))
                                        efectivo.Peso = objDenominacion.Peso
                                        efectivo.Valor = objDenominacion.Valor
                                        If _valor.Table.Columns.Contains("TIPO") Then
                                            Select Case _valor("TIPO")
                                                Case "CONTADO_EFECTIVO"
                                                    efectivo.TipoValor = Enumeradores.TipoValor.Contado
                                                Case "DIFERENCIA_EFECTIVO"
                                                    efectivo.TipoValor = Enumeradores.TipoValor.Diferencia
                                                Case "DECLARADO_EFECTIVO"
                                                    efectivo.TipoValor = Enumeradores.TipoValor.Declarado
                                            End Select
                                        End If
                                    End With

                                    'Se já existir um valor contado para essa denominação e o valor a ser inserido é de diferença
                                    'deve-se considerar apenas os valores de diferença
                                    If efectivo.TipoValor = Enumeradores.TipoValor.Diferencia Then
                                        abonoValor.AbonoElemento.Divisa.ListaEfectivo.RemoveAll(Function(e) e.TipoValor = Enumeradores.TipoValor.Contado)
                                    End If

                                    abonoValor.AbonoElemento.Divisa.ListaEfectivo.Add(efectivo)

                                End If
                            End If
                        Next
                    End If
                End If

                ' Valores MedioPago
                If dtValoresMedioPago IsNot Nothing AndAlso dtValoresMedioPago.Rows.Count > 0 Then

                    Dim _ValoresMedioPago = dtValoresMedioPago.Select(consulta)

                    If _ValoresMedioPago IsNot Nothing Then

                        For Each _valor In _ValoresMedioPago

                            abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso av.AbonoElemento.Divisa IsNot Nothing AndAlso
                                                                   av.AbonoElemento.Divisa.Identificador = divisa.Identificador AndAlso
                                                                   av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)


                            If abonoValor Is Nothing Then
                                abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso
                                                                       av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)
                            End If

                            If abonoValor Is Nothing Then
                                abonoValor = New Clases.Abono.AbonoValor()
                                abonos.Add(abonoValor)
                            End If

                            If abonoValor.AbonoElemento Is Nothing Then
                                abonoValor.AbonoElemento = New Clases.Abono.AbonoElemento()
                            End If
                            abonoValor.AbonoElemento.IdentificadorRemesa = identificadorRemesa

                            If abonoValor.AbonoElemento.Divisa Is Nothing OrElse
                               abonoValor.AbonoElemento.Divisa.Identificador <> divisa.Identificador Then

                                'Se já existe o elemento é porque ele está com outra divisa
                                'e deve-se criar outro abono valor, porém com a divisa do filtro
                                If abonoValor.AbonoElemento.Divisa IsNot Nothing Then
                                    'clona o objeto para ser utilizado novamente com outra divisa
                                    Dim objAbonoValorClone = abonoValor.Clonar()
                                    'objAbonoValorClone.Divisa = Nothing

                                    abonoValor = objAbonoValorClone
                                    abonos.Add(abonoValor)

                                End If

                                abonoValor.AbonoElemento.Divisa = New Clases.Abono.DivisaAbono() With {.Identificador = divisa.Identificador,
                                                                               .CodigoISO = divisa.CodigoISO,
                                                                               .Descripcion = divisa.Descripcion,
                                                                               .Color = divisa.Color}
                            End If

                            If _valor.Table.Columns.Contains("OID_MEDIO_PAGO") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))) AndAlso _
                                                               _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) AndAlso _
                                                               divisa.MediosPago IsNot Nothing AndAlso divisa.MediosPago.Count > 0 Then

                                objMedioPago = (From MP In divisa.MediosPago Where MP.Identificador = Util.AtribuirValorObj(_valor("OID_MEDIO_PAGO"), GetType(String))).FirstOrDefault

                                If objMedioPago IsNot Nothing Then

                                    If abonoValor.AbonoElemento.Divisa.ListaMedioPago Is Nothing Then abonoValor.AbonoElemento.Divisa.ListaMedioPago = New List(Of Clases.Abono.MedioPagoAbono)

                                    Dim medioPago As New Clases.Abono.MedioPagoAbono
                                    With medioPago
                                        .Identificador = objMedioPago.Identificador
                                        .Codigo = objMedioPago.Codigo
                                        .Descripcion = objMedioPago.Descripcion
                                        .Cantidad = Util.AtribuirValorObj(_valor("CANTIDAD"), GetType(Int64))
                                        .Importe = Util.AtribuirValorObj(_valor("IMPORTE"), GetType(Decimal))
                                        If _valor.Table.Columns.Contains("TIPO") Then
                                            Select Case _valor("TIPO")
                                                Case "CONTADO_MEDIO_PAGO"
                                                    .TipoValor = Enumeradores.TipoValor.Contado
                                                Case "DIFERENCIA_MEDIO_PAGO"
                                                    .TipoValor = Enumeradores.TipoValor.Diferencia
                                                Case "DECLARADO_MEDIO_PAGO"
                                                    .TipoValor = Enumeradores.TipoValor.Declarado
                                            End Select
                                        End If

                                    End With

                                    'Se já existir um valor contado para esse medio pago e o valor a ser inserido é de diferença
                                    'deve-se considerar apenas os valores de diferença
                                    If medioPago.TipoValor = Enumeradores.TipoValor.Diferencia Then
                                        abonoValor.AbonoElemento.Divisa.ListaMedioPago.RemoveAll(Function(e) e.TipoValor = Enumeradores.TipoValor.Contado)
                                    End If

                                    abonoValor.AbonoElemento.Divisa.ListaMedioPago.Add(medioPago)
                                End If

                            End If

                        Next

                    End If

                End If

                ' Valores Totales
                If dtValoresTotales IsNot Nothing AndAlso dtValoresTotales.Rows.Count > 0 Then

                    Dim _ValoresTotales = dtValoresTotales.Select(consulta)

                    If _ValoresTotales IsNot Nothing Then

                        For Each _valor In _ValoresTotales

                            abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso av.AbonoElemento.Divisa IsNot Nothing AndAlso
                                                               av.AbonoElemento.Divisa.Identificador = divisa.Identificador AndAlso
                                                               av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)
                            If abonoValor Is Nothing Then
                                abonoValor = abonos.FirstOrDefault(Function(av) av.AbonoElemento IsNot Nothing AndAlso
                                                                       av.AbonoElemento.IdentificadorRemesa = identificadorRemesa)
                            End If

                            If abonoValor Is Nothing Then
                                abonoValor = New Clases.Abono.AbonoValor()
                                abonos.Add(abonoValor)
                            End If

                            If abonoValor.AbonoElemento Is Nothing Then
                                abonoValor.AbonoElemento = New Clases.Abono.AbonoElemento()
                                abonoValor.AbonoElemento.IdentificadorRemesa = identificadorRemesa
                            End If

                            If abonoValor.AbonoElemento.Divisa Is Nothing OrElse
                               abonoValor.AbonoElemento.Divisa.Identificador <> divisa.Identificador Then

                                'Se já existe o elemento é porque ele está com outra divisa
                                'e deve-se criar outro abono valor, porém com a divisa do filtro
                                If abonoValor.AbonoElemento.Divisa IsNot Nothing Then
                                    'clona o objeto para ser utilizado novamente com outra divisa
                                    Dim objAbonoValorClone = abonoValor.Clonar()
                                    'objAbonoValorClone.Divisa = Nothing

                                    abonoValor = objAbonoValorClone
                                    abonos.Add(abonoValor)

                                End If

                                abonoValor.AbonoElemento.Divisa = New Clases.Abono.DivisaAbono() With {.Identificador = divisa.Identificador,
                                                                               .CodigoISO = divisa.CodigoISO,
                                                                               .Descripcion = divisa.Descripcion,
                                                                               .Color = divisa.Color}
                            End If

                            If _valor.Table.Columns.Contains("IMPORTE") AndAlso Not String.IsNullOrEmpty(Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))) Then

                                If abonoValor.AbonoElemento.Divisa.Totales Is Nothing Then abonoValor.AbonoElemento.Divisa.Totales = New Clases.Abono.TotalesAbono()

                                If (Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DECLARADO_EFECTIVO" OrElse Util.AtribuirValorObj(_valor("TIPO"), GetType(String)) = "DIFERENCIA_EFECTIVO") Then

                                    Dim objValor As Clases.Valor = Nothing
                                    Dim nivelDetalle As Enumeradores.TipoNivelDetalhe = Extenciones.RecuperarEnum(Of Enumeradores.TipoNivelDetalhe)(Util.AtribuirValorObj(_valor("COD_NIVEL_DETALLE"), GetType(String)))

                                    abonoValor.AbonoElemento.Divisa.Totales.TotalEfectivo += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))

                                Else

                                    Select Case Extenciones.RecuperarEnum(Of Enumeradores.TipoMedioPago)(Util.AtribuirValorObj(_valor("COD_TIPO_MEDIO_PAGO"), GetType(String)))
                                        Case Enumeradores.TipoMedioPago.Cheque
                                            abonoValor.AbonoElemento.Divisa.Totales.TotalCheque += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))
                                        Case Enumeradores.TipoMedioPago.OtroValor
                                            abonoValor.AbonoElemento.Divisa.Totales.TotalOtroValor += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))
                                        Case Enumeradores.TipoMedioPago.Tarjeta
                                            abonoValor.AbonoElemento.Divisa.Totales.TotalTarjeta += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))
                                        Case Enumeradores.TipoMedioPago.Ticket
                                            abonoValor.AbonoElemento.Divisa.Totales.TotalTicket += Util.AtribuirValorObj(_valor("IMPORTE"), GetType(String))
                                    End Select
                                End If
                            End If
                        Next

                    End If

                End If

            End If
        End Sub

        Shared Sub ActualizarIdentificadorGrupoDocumento(objAbono As Clases.Abono.Abono, transaccion As DataBaseHelper.Transaccion)

            Dim wrapper As New DataBaseHelper.SPWrapper(Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ActualizarIdentificadorGrupoDocumento), True, CommandType.Text)


            wrapper.AgregarParam("OID_GRUPO_DOCUMENTO", ProsegurDbType.Objeto_Id, objAbono.IdentificadorGrupoDocumento)
            wrapper.AgregarParam("OID_ABONO", ProsegurDbType.Objeto_Id, objAbono.Identificador)

            DataBaseHelper.AccesoDB.EjecutarSP(wrapper, Constantes.CONEXAO_GENESIS, False, transaccion)

        End Sub



    End Class
End Namespace