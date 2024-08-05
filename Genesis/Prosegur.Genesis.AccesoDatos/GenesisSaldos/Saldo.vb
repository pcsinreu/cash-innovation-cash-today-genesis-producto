Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports System.Threading.Tasks
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario

Namespace GenesisSaldos
    Public Class Saldo

#Region "[CONSULTAS]"

        Public Shared Function ObtenerSaldoPorSector(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector),
                                                     ByRef _respuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of DataRow)),
                                                     _filtros As Clases.Transferencias.FiltroConsultaSaldo) As List(Of DataRow)

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoRecuperarPorSector
            CargarConsulta(_filtros, _command, False)
            Return EjecutarQuery(_peticion, _respuesta, _command)

        End Function

        Public Shared Function ObtenerSaldoPorClienteyCanal(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector),
                                                            ByRef _respuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of DataRow)),
                                                            _filtros As Clases.Transferencias.FiltroConsultaSaldo) As List(Of DataRow)

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoRecuperarPorClienteCanal
            CargarConsulta(_filtros, _command, True)
            Return EjecutarQuery(_peticion, _respuesta, _command)

        End Function

        Public Shared Function ObtenerSaldoPorCuenta(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector),
                                                     ByRef _respuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of DataRow)),
                                                     _filtros As Clases.Transferencias.FiltroConsultaSaldo) As List(Of DataRow)

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoRecuperarPorCuenta
            CargarConsulta(_filtros, _command, True)
            Return EjecutarQuery(_peticion, _respuesta, _command)

        End Function

        Public Shared Function ObtenerSaldoTotal(_filtros As Clases.Transferencias.FiltroConsultaSaldo) As DataTable

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoRecuperarTotal
            CargarConsulta(_filtros, _command, False)
            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

        End Function

        Public Shared Function ObtenerSaldoTotalSinCanalSF(_filtros As Clases.Transferencias.FiltroConsultaSaldo) As DataTable

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoRecuperarTotalSinCanalSF
            CargarConsulta(_filtros, _command, False)
            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

        End Function

        Private Shared Function EjecutarQuery(_peticion As Prosegur.Genesis.Comon.Peticion(Of Clases.Sector),
                                              ByRef _respuesta As Prosegur.Genesis.Comon.Respuesta(Of List(Of DataRow)),
                                              _command As IDbCommand) As List(Of DataRow)

            _respuesta.ParametrosPaginacion = New Prosegur.Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion()

            Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GENESIS, _command, _peticion.ParametrosPaginacion, _respuesta.ParametrosPaginacion)

            If dt Is Nothing OrElse dt.Rows.Count < 1 Then
                Return Nothing
            End If

            Return dt.AsEnumerable().ToList()
        End Function

        Private Shared Sub CargarConsulta(_filtros As Clases.Transferencias.FiltroConsultaSaldo,
                                          ByRef _command As IDbCommand,
                                          _esPorCuenta As Boolean)

            _command.CommandType = CommandType.Text

            Dim QueryPlantasSectores As New StringBuilder
            Dim QueryDetallarHijos As New StringBuilder
            Dim Query As New StringBuilder
            Dim QueryDetallarHijosSelect As New StringBuilder
            Dim QuerySelect As New StringBuilder
            Dim QueryInner As New StringBuilder
            Dim QueryInner1 As New StringBuilder

            ' Plantas e Sectores são parametros obrigatorios
            'Filtro Planta
            QueryPlantasSectores.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresPlantas, "OID_PLANTA", _command, "AND", "SECT"))
            'Filtro Sectores
            QueryPlantasSectores.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresSectores, "OID_SECTOR", _command, "AND", "SECT"))


            If _filtros.DetallarSaldoSectoresHijos Then
                QueryDetallarHijos.AppendLine(" SECT.OID_SECTOR, SECT.COD_SECTOR || ' - ' || SECT.DES_SECTOR DES_SECTOR, SECT.OID_SECTOR_PADRE ")
                QueryDetallarHijosSelect.AppendLine(" ,SEC.OID_SECTOR, SEC.OID_SECTOR_PADRE ")
                QuerySelect.AppendLine(" ,OID_SECTOR, OID_SECTOR_PADRE ")
                QueryInner.AppendLine(" RIGHT ")
                QueryInner1.AppendLine(" LEFT ")
            Else
                QueryDetallarHijos.AppendLine(String.Format(" FN_SECTOR_PADRE_{0}(LEVEL,SECT.OID_SECTOR_PADRE,SECT.COD_SECTOR, SECT.DES_SECTOR)AS DES_SECTOR, SECT.OID_SECTOR ", _filtros.Version))
                QueryDetallarHijosSelect.AppendLine("")
                QuerySelect.AppendLine("")
                QueryInner.AppendLine(" INNER ")
                QueryInner1.AppendLine(" INNER ")
            End If

            'Filtro Cliente
            If _filtros.identificadoresClientes IsNot Nothing AndAlso _filtros.identificadoresClientes.Count > 0 Then
                Query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresClientes, "OID_CLIENTE", _command, "AND", "CU"))
            End If

            'If _esPorCuenta Then
            'Filtro Sub-Cliente
            If _filtros.identificadoresSubClientes IsNot Nothing AndAlso _filtros.identificadoresSubClientes.Count > 0 Then
                Query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresSubClientes, "OID_SUBCLIENTE", _command, "AND", "CU"))
            Else
                Query.AppendLine(" AND CU.OID_SUBCLIENTE IS NULL")
            End If

            'Filtro Punto Servicio
            If _filtros.identificadoresPtoServicios IsNot Nothing AndAlso _filtros.identificadoresPtoServicios.Count > 0 Then
                Query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresPtoServicios, "OID_PTO_SERVICIO", _command, "AND", "CU"))
            Else
                Query.AppendLine(" AND CU.OID_PTO_SERVICIO IS NULL")
            End If
            'End If

            'Filtro Canal
            If _filtros.identificadoresCanales IsNot Nothing AndAlso _filtros.identificadoresCanales.Count > 0 Then
                Query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresCanales, "OID_CANAL", _command, "AND", "CN"))
            End If

            'Filtro SubCanal
            If _filtros.identificadoresSubCanales IsNot Nothing AndAlso _filtros.identificadoresSubCanales.Count > 0 Then
                Query.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, _filtros.identificadoresSubCanales, "OID_SUBCANAL", _command, "AND", "CU"))
            End If

            If _filtros.Disponibilidad = Enumeradores.Disponibilidad.Disponible Then
                Query.AppendLine(" AND BOL_DISPONIBLE = 1 ")
            ElseIf _filtros.Disponibilidad = Enumeradores.Disponibilidad.NoDisponible Then
                Query.AppendLine(" AND BOL_DISPONIBLE = 0 ")
            End If

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(_command.CommandText, QueryPlantasSectores.ToString, Query.ToString, QueryDetallarHijos.ToString, QueryDetallarHijosSelect.ToString(), QuerySelect.ToString(), QueryInner.ToString(), QueryInner1.ToString()))

        End Sub

        Public Shared Function obtenerSaldoCuentas_v2(Filtro As Clases.Transferencias.Filtro,
                                             Optional ByRef log As StringBuilder = Nothing) As ObservableCollection(Of Clases.SaldoCuenta)

            Dim _log As New StringBuilder
            Dim TiempoInicial As DateTime = Now
            Dim Tiempo As DateTime = Now

            Dim objSaldoCuentasDetalle As New ObservableCollection(Of Clases.SaldoCuentaDetalle)
            Dim objSaldoCuentas As New ObservableCollection(Of Clases.SaldoCuenta)

            Try
                Dim ds As DataSet = Nothing

                Tiempo = Now
                Dim spw As SPWrapper = ColectarSaldoCuentasRecuperar(Filtro)
                _log.AppendLine("____Tiempo 'BBDD': " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                ds = AccesoDB.EjecutarSP(spw, Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, False)
                _log.AppendLine("____Tiempo 'BBDD': " & Now.Subtract(Tiempo).ToString() & "; ")

                Tiempo = Now
                objSaldoCuentas = PoblarSaldoCuentas(ds)
                _log.AppendLine("____Tiempo 'BBDD': " & Now.Subtract(Tiempo).ToString() & "; ")

            Catch ex As Exception
                Throw ex
            End Try

            Return objSaldoCuentas

        End Function

        Public Shared Function ColectarSaldoCuentasRecuperar(Filtro As Clases.Transferencias.Filtro) As SPWrapper

            Dim SP As String = String.Format("sapr_psaldo_{0}.srecuperar_saldo_cuentas", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            If Filtro.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(Filtro.Cliente.Identificador) Then
                spw.AgregarParam("par$oid_cliente", ProsegurDbType.Objeto_Id, Filtro.Cliente.Identificador, ParameterDirection.Input, False)

                If Filtro.Cliente.SubClientes IsNot Nothing AndAlso Filtro.Cliente.SubClientes.Count > 0 AndAlso Not String.IsNullOrEmpty(Filtro.Cliente.SubClientes(0).Identificador) Then
                    spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Objeto_Id, Filtro.Cliente.SubClientes(0).Identificador, ParameterDirection.Input, False)
                    If Filtro.Cliente.SubClientes(0).PuntosServicio IsNot Nothing AndAlso Filtro.Cliente.SubClientes(0).PuntosServicio.Count > 0 AndAlso Not String.IsNullOrEmpty(Filtro.Cliente.SubClientes(0).PuntosServicio(0).Identificador) Then
                        spw.AgregarParam("par$oid_ptoservicio", ProsegurDbType.Objeto_Id, Filtro.Cliente.SubClientes(0).PuntosServicio(0).Identificador, ParameterDirection.Input, False)
                    Else
                        spw.AgregarParam("par$oid_ptoservicio", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
                    End If
                Else
                    spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
                    spw.AgregarParam("par$oid_ptoservicio", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
                End If
            Else
                spw.AgregarParam("par$oid_cliente", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
                spw.AgregarParam("par$oid_subcliente", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
                spw.AgregarParam("par$oid_ptoservicio", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
            End If

            If Filtro.Canais IsNot Nothing AndAlso Filtro.Canais.Count > 0 AndAlso Filtro.Canais(0).SubCanales IsNot Nothing AndAlso Filtro.Canais(0).SubCanales.Count > 0 AndAlso
                Not String.IsNullOrEmpty(Filtro.Canais(0).SubCanales(0).Identificador) Then
                spw.AgregarParam("par$oid_subcanal", ProsegurDbType.Objeto_Id, Filtro.Canais(0).SubCanales(0).Identificador, ParameterDirection.Input, False)
            Else
                spw.AgregarParam("par$oid_subcanal", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
            End If

            If Filtro.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Filtro.Sector.Identificador) Then
                spw.AgregarParam("par$oid_sector", ProsegurDbType.Objeto_Id, Filtro.Sector.Identificador, ParameterDirection.Input, False)
            Else
                spw.AgregarParam("par$oid_sector", ProsegurDbType.Objeto_Id, DBNull.Value, ParameterDirection.Input, False)
            End If

            spw.AgregarParam("par$oid_divisas", ProsegurDbType.Objeto_Id, Nothing, , True)
            spw.AgregarParam("par$tipos_medio_pago", ProsegurDbType.Objeto_Id, Nothing, , True)

            spw.AgregarParam("par$excluir_sectores_hijos", ParamTypes.Integer, If(Filtro.ExcluirSectoresHijos, 1, 0), ParameterDirection.Input, False)

            If (String.IsNullOrEmpty(Filtro.TipoValores) OrElse (Not String.IsNullOrEmpty(Filtro.TipoValores) AndAlso Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_EFECTIVO))) Then
                spw.AgregarParam("par$recuperar_efectivos", ParamTypes.Integer, 1, ParameterDirection.Input, False)
            Else
                spw.AgregarParam("par$recuperar_efectivos", ParamTypes.Integer, 0, ParameterDirection.Input, False)
            End If

            spw.AgregarParam("par$rc_saldo", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "rc_saldo")
            spw.AgregarParam("par$doc_rc_divisas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_divisas")
            spw.AgregarParam("par$doc_rc_denominaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_denominaciones")
            spw.AgregarParam("par$doc_rc_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_mediospago")
            spw.AgregarParam("par$doc_rc_terminos_mediospago", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_terminos_mediospago")
            spw.AgregarParam("par$doc_rc_unidades_medida", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_unidades_medida")
            spw.AgregarParam("par$doc_rc_calidades", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_calidades")
            spw.AgregarParam("par$doc_rc_cuentas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_cuentas")
            spw.AgregarParam("par$doc_rc_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_sectores")
            spw.AgregarParam("par$doc_rc_tipos_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sectores")
            spw.AgregarParam("par$doc_rc_caract_tp_sectores", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_caract_tp_sectores")
            spw.AgregarParam("par$doc_rc_plantas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_plantas")
            spw.AgregarParam("par$doc_rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_delegaciones")
            spw.AgregarParam("par$doc_rc_tipos_sector_planta", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "doc_rc_tipos_sector_planta")
            spw.AgregarParam("par$usuario", ParamTypes.String, Filtro.Usuario, ParameterDirection.Input, False)
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Descricao_Longa, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_ejecucion", ParamTypes.Long, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")


            If Filtro.Divisas IsNot Nothing AndAlso Filtro.Divisas.Count > 0 Then
                For Each divisa In Filtro.Divisas
                    If Not String.IsNullOrEmpty(divisa.Identificador) Then
                        spw.Param("par$oid_divisas").AgregarValorArray(divisa.Identificador)
                    End If
                Next
            Else
                spw.Param("par$oid_divisas").AgregarValorArray(DBNull.Value)
            End If

            If Filtro.TipoValores IsNot Nothing AndAlso Filtro.TipoValores <> ContractoServicio.Constantes.COD_TIPO_EFECTIVO Then
                For Each objTipo In Filtro.TipoValores.Split(",")
                    If Not String.IsNullOrEmpty(objTipo) AndAlso objTipo <> ContractoServicio.Constantes.COD_TIPO_EFECTIVO Then
                        spw.Param("par$tipos_medio_pago").AgregarValorArray(objTipo)
                    End If
                Next
            Else
                spw.Param("par$tipos_medio_pago").AgregarValorArray(DBNull.Value)
            End If

            Return spw
        End Function

        Public Shared Function PoblarSaldoCuentas(ds As DataSet) As ObservableCollection(Of Clases.SaldoCuenta)
            Dim objSaldoCuentas As New ObservableCollection(Of Clases.SaldoCuenta)

            'validar el dataset con el SP wrapper
            If ds IsNot Nothing Then
                'objSaldoCuentas = CargarGrupoDocumentos(ds)
            End If

            Return objSaldoCuentas
        End Function

        Public Shared Function obtenerSaldoCuentas(Filtro As Clases.Transferencias.Filtro,
                                                     Optional esDisponibleNoDefinido As Boolean = False,
                                                     Optional BuscarValoresDisponibles As Nullable(Of Boolean) = Nothing) As DataTable
            'As List(Of DataRow) 

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim QueryEfectivo As String = My.Resources.DivisaRecuperarValoresPorCuentaEfectivo
            Dim QueryMedioPago As String = My.Resources.DivisaRecuperarValoresPorCuentaMedioPago


            cmd.CommandType = CommandType.Text
            Dim QueryWhereSector As New StringBuilder
            If Not Filtro.ExcluirSectoresHijos AndAlso Filtro.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Filtro.Sector.Identificador) Then
                QueryWhereSector.Append(" INNER JOIN (SELECT SECT.OID_SECTOR, SECT.COD_SECTOR, SECT.DES_SECTOR, SECT.BOL_ACTIVO, SECT.OID_TIPO_SECTOR, SECT.OID_PLANTA FROM GEPR_TSECTOR SECT START WITH SECT.OID_SECTOR = :OID_SECTOR CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SE ON CU.OID_SECTOR = SE.OID_SECTOR ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Filtro.Sector.Identificador))
            Else
                QueryWhereSector.Append(" INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
            End If

            Dim QueryWhereCuenta As New StringBuilder
            Dim QueryWhereEfectivo As New StringBuilder
            Dim QueryWhereMedioPago As New StringBuilder
            QueryWhereEfectivo.Append(" WHERE SF.NUM_IMPORTE <> 0 ")
            QueryWhereMedioPago.Append(" WHERE SMP.NUM_IMPORTE <> 0 ")
            Dim objQueryDenominaciones As String = String.Empty
            Dim objQueryMedioPago As String = String.Empty

            ' Monta query Cuenta
            Util.PreencherQueryCuenta(Filtro, QueryWhereCuenta, cmd)
            QueryWhereEfectivo.Append(QueryWhereCuenta.ToString())
            QueryWhereMedioPago.Append(QueryWhereCuenta.ToString())

            If BuscarValoresDisponibles IsNot Nothing Then
                QueryWhereEfectivo.Append(" AND BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                QueryWhereMedioPago.Append(" AND BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, BuscarValoresDisponibles))
            End If

            If Filtro.TipoValores IsNot Nothing Then

                Dim objTipoMedioPago As String = ""
                For Each objTipo In Filtro.TipoValores.Split(",")
                    If Not String.IsNullOrEmpty(objTipo) AndAlso objTipo <> ContractoServicio.Constantes.COD_TIPO_EFECTIVO Then
                        If Not String.IsNullOrEmpty(objTipoMedioPago) Then
                            objTipoMedioPago &= ","
                        End If
                        objTipoMedioPago &= "'" & objTipo & "'"
                    End If
                Next

                If Not String.IsNullOrEmpty(objTipoMedioPago) Then
                    QueryWhereMedioPago.Append(" AND COD_TIPO_MEDIO_PAGO IN (" & objTipoMedioPago & ") ")
                    If Not Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_EFECTIVO) Then
                        QueryEfectivo = String.Empty
                    End If
                Else
                    QueryMedioPago = String.Empty
                End If

            End If

            'Precorre a coleção de divisas e monta a clausula.
            If Filtro.Divisas IsNot Nothing AndAlso Filtro.Divisas.Count > 0 Then

                Dim IdentificadoresDivisas As List(Of String) = Nothing
                Dim IdentificadoresDenominaciones As List(Of String) = Nothing
                Dim IdentificadoresMedioPago As List(Of String) = Nothing

                IdentificadoresDivisas = (From Div In Filtro.Divisas Select Div.Identificador).ToList

                Dim QueryWhereAux As New StringBuilder
                QueryWhereAux.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDivisas, "OID_DIVISA", cmd, "AND"))
                QueryWhereEfectivo.Append(QueryWhereAux.ToString())
                QueryWhereMedioPago.Append(QueryWhereAux.ToString())

                For Each Divisa In Filtro.Divisas


                    If Not String.IsNullOrEmpty(QueryEfectivo) Then
                        If (String.IsNullOrEmpty(Filtro.TipoValores) OrElse (Not String.IsNullOrEmpty(Filtro.TipoValores) AndAlso Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_EFECTIVO))) AndAlso
                        Divisa.Denominaciones IsNot Nothing AndAlso Divisa.Denominaciones.Count > 0 Then

                            If IdentificadoresDenominaciones Is Nothing Then IdentificadoresDenominaciones = New List(Of String)

                            IdentificadoresDenominaciones.AddRange((From Den In Divisa.Denominaciones Select Den.Identificador).ToList)

                            objQueryDenominaciones = " AND (OID_DENOMINACION IS NULL "
                            objQueryDenominaciones &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDenominaciones, "OID_DENOMINACION", cmd, "OR")
                            objQueryDenominaciones &= ")"

                        End If
                    End If

                    If Not String.IsNullOrEmpty(QueryMedioPago) AndAlso Divisa.MediosPago IsNot Nothing AndAlso Divisa.MediosPago.Count > 0 Then

                        If IdentificadoresMedioPago Is Nothing Then IdentificadoresMedioPago = New List(Of String)

                        If String.IsNullOrEmpty(Filtro.TipoValores) Then
                            IdentificadoresMedioPago.AddRange((From MP In Divisa.MediosPago Select MP.Identificador).ToList)
                        Else
                            For Each objMedioPago In Divisa.MediosPago
                                If objMedioPago.Tipo = Enumeradores.TipoMedioPago.Cheque OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.OtroValor OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.Tarjeta OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TARJETAS) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.Ticket OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TICKET) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                End If
                            Next
                        End If

                        objQueryMedioPago = " AND (OID_MEDIO_PAGO IS NULL "
                        objQueryMedioPago &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresMedioPago, "OID_MEDIO_PAGO", cmd, "OR")
                        objQueryMedioPago &= ")"

                    End If

                Next

            End If

            If Not String.IsNullOrEmpty(QueryEfectivo) Then
                QueryEfectivo = String.Format(QueryEfectivo, If(Not String.IsNullOrEmpty(QueryWhereSector.ToString()), QueryWhereSector.ToString(), String.Empty),
                                                             If(Not String.IsNullOrEmpty(QueryWhereEfectivo.ToString()), QueryWhereEfectivo.ToString(), String.Empty),
                                                             If(Not String.IsNullOrEmpty(objQueryDenominaciones), objQueryDenominaciones, String.Empty), String.Empty)

            End If

            If Not String.IsNullOrEmpty(QueryMedioPago) Then
                QueryMedioPago = String.Format(QueryMedioPago, If(Not String.IsNullOrEmpty(QueryWhereSector.ToString()), QueryWhereSector.ToString(), String.Empty),
                                                               If(Not String.IsNullOrEmpty(QueryWhereMedioPago.ToString()), QueryWhereMedioPago.ToString(), String.Empty),
                                                               If(Not String.IsNullOrEmpty(objQueryMedioPago), objQueryMedioPago, String.Empty), String.Empty)

            End If

            If Not String.IsNullOrEmpty(QueryEfectivo) AndAlso Not String.IsNullOrEmpty(QueryMedioPago) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo & " UNION " & QueryMedioPago)
            ElseIf Not String.IsNullOrEmpty(QueryEfectivo) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo)
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryMedioPago)
            End If

            'cmd.CommandText = "SELECT * FROM (" & cmd.CommandText & ") ORDER BY COD_CLIENTE, DES_SECTOR"


            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function RecuperarSaldoCuentasDetallado(Filtro As Clases.Transferencias.Filtro,
                                                     Optional esDisponibleNoDefinido As Boolean = False,
                                                     Optional BuscarValoresDisponibles As Nullable(Of Boolean) = Nothing) As DataTable
            'As List(Of DataRow) 

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim QueryEfectivo As String = My.Resources.DivisaRecuperarValoresPorCuentaEfectivo
            Dim QueryMedioPago As String = My.Resources.DivisaRecuperarValoresPorCuentaMedioPago
            Dim QuerySelect As New StringBuilder()
            Dim QuerySelectGroupBy As New StringBuilder()
            Dim QueryGroupBy As New StringBuilder()

            QuerySelect.AppendLine(", SE.DES_SECTOR,CAN.COD_CANAL,CAN.DES_CANAL,SBC.COD_SUBCANAL,SBC.DES_SUBCANAL,PTO.COD_PTO_SERVICIO,PTO.DES_PTO_SERVICIO,SCL.COD_SUBCLIENTE,SCL.DES_SUBCLIENTE,CL.COD_CLIENTE,CL.DES_CLIENTE,NIVEL,LINHA")

            QuerySelectGroupBy.AppendLine("SELECT ")
            QuerySelectGroupBy.AppendLine("COD_CLIENTE, ")
            QuerySelectGroupBy.AppendLine("DES_CLIENTE, ")
            QuerySelectGroupBy.AppendLine("COD_SUBCLIENTE, ")
            QuerySelectGroupBy.AppendLine("DES_SUBCLIENTE, ")
            QuerySelectGroupBy.AppendLine("COD_PTO_SERVICIO, ")
            QuerySelectGroupBy.AppendLine("DES_PTO_SERVICIO, ")
            QuerySelectGroupBy.AppendLine("COD_CANAL, ")
            QuerySelectGroupBy.AppendLine("DES_CANAL, ")
            QuerySelectGroupBy.AppendLine("COD_SUBCANAL, ")
            QuerySelectGroupBy.AppendLine("DES_SUBCANAL,  ")
            QuerySelectGroupBy.AppendLine("OID_DIVISA, ")
            QuerySelectGroupBy.AppendLine("DES_SECTOR, ")
            QuerySelectGroupBy.AppendLine("SUM((CASE BOL_DISPONIBLE WHEN 1 THEN IMPORTE END))NUM_IMPORTE_DISP ")
            QuerySelectGroupBy.AppendLine(",NIVEL, LINHA ")
            QuerySelectGroupBy.AppendLine("FROM ( ")

            QueryGroupBy.AppendLine(") GROUP BY COD_CLIENTE, DES_CLIENTE, COD_SUBCLIENTE, DES_SUBCLIENTE, COD_PTO_SERVICIO, DES_PTO_SERVICIO, COD_CANAL, DES_CANAL, COD_SUBCANAL, DES_SUBCANAL, OID_DIVISA, DES_SECTOR, NIVEL, LINHA ")

            cmd.CommandType = CommandType.Text
            Dim QueryWhereSector As New StringBuilder
            If Not Filtro.ExcluirSectoresHijos AndAlso Filtro.Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Filtro.Sector.Identificador) Then
                QueryWhereSector.Append(" INNER JOIN (SELECT SECT.OID_SECTOR, SECT.COD_SECTOR, SECT.OID_SECTOR_PADRE, SECT.COD_MIGRACION, SECT.DES_SECTOR, SECT.BOL_ACTIVO, SECT.BOL_CENTRO_PROCESO, SECT.BOL_CONTEO, SECT.BOL_TESORO, SECT.GMT_CREACION, SECT.GMT_MODIFICACION, SECT.BOL_PERMITE_DISPONER_VALOR, SECT.OID_TIPO_SECTOR, SECT.OID_PLANTA, LEVEL AS NIVEL, ROWNUM AS LINHA FROM GEPR_TSECTOR SECT START WITH SECT.OID_SECTOR = :OID_SECTOR CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE) SE ON CU.OID_SECTOR = SE.OID_SECTOR ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, Filtro.Sector.Identificador))
            Else
                QueryWhereSector.Append(" INNER JOIN GEPR_TSECTOR SE ON SE.OID_SECTOR = CU.OID_SECTOR ")
            End If

            Dim QueryWhereCuenta As New StringBuilder
            Dim QueryWhereEfectivo As New StringBuilder
            Dim QueryWhereMedioPago As New StringBuilder
            QueryWhereEfectivo.Append(" WHERE SF.NUM_IMPORTE <> 0 ")
            QueryWhereMedioPago.Append(" WHERE SMP.NUM_IMPORTE <> 0 ")
            Dim objQueryDenominaciones As String = String.Empty
            Dim objQueryMedioPago As String = String.Empty

            ' Monta query Cuenta
            Util.PreencherQueryCuenta(Filtro, QueryWhereCuenta, cmd)

            If Filtro.Cliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(Filtro.Cliente.Codigo) Then
                If Filtro.Cliente.SubClientes IsNot Nothing AndAlso Filtro.Cliente.SubClientes.Count > 0 Then
                    'Recupera os codigos dos pontos de serviço
                    Dim CodigosPuntoServicio As List(Of String) = (From SubCliente In Filtro.Cliente.SubClientes.FindAll(Function(sc) sc.PuntosServicio IsNot Nothing AndAlso sc.PuntosServicio.Count > 0),
                                            Pto In SubCliente.PuntosServicio Select Pto.Codigo).ToList

                    'Se não foi informado um ponto de servicio
                    If CodigosPuntoServicio Is Nothing OrElse CodigosPuntoServicio.Count = 0 Then
                        QueryWhereCuenta.Append(" AND CU.OID_PTO_SERVICIO IS NULL")
                    End If
                Else
                    QueryWhereCuenta.Append(" AND CU.OID_SUBCLIENTE IS NULL")
                    QueryWhereCuenta.Append(" AND CU.OID_PTO_SERVICIO IS NULL")
                End If
            End If

            QueryWhereEfectivo.Append(QueryWhereCuenta.ToString())
            QueryWhereMedioPago.Append(QueryWhereCuenta.ToString())

            If BuscarValoresDisponibles IsNot Nothing Then
                QueryWhereEfectivo.Append(" AND BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                QueryWhereMedioPago.Append(" AND BOL_DISPONIBLE = []BOL_DISPONIBLE ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, BuscarValoresDisponibles))
            End If

            If Filtro.TipoValores IsNot Nothing Then

                Dim objTipoMedioPago As String = ""
                For Each objTipo In Filtro.TipoValores.Split(",")
                    If Not String.IsNullOrEmpty(objTipo) AndAlso objTipo <> ContractoServicio.Constantes.COD_TIPO_EFECTIVO Then
                        If Not String.IsNullOrEmpty(objTipoMedioPago) Then
                            objTipoMedioPago &= ","
                        End If
                        objTipoMedioPago &= "'" & objTipo & "'"
                    End If
                Next

                If Not String.IsNullOrEmpty(objTipoMedioPago) Then
                    QueryWhereMedioPago.Append(" AND COD_TIPO_MEDIO_PAGO IN (" & objTipoMedioPago & ") ")
                    If Not Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_EFECTIVO) Then
                        QueryEfectivo = String.Empty
                    End If
                Else
                    QueryMedioPago = String.Empty
                End If

            End If

            'Precorre a coleção de divisas e monta a clausula.
            If Filtro.Divisas IsNot Nothing AndAlso Filtro.Divisas.Count > 0 Then

                Dim IdentificadoresDivisas As List(Of String) = Nothing
                Dim IdentificadoresDenominaciones As List(Of String) = Nothing
                Dim IdentificadoresMedioPago As List(Of String) = Nothing

                IdentificadoresDivisas = (From Div In Filtro.Divisas Select Div.Identificador).ToList

                Dim QueryWhereAux As New StringBuilder
                QueryWhereAux.Append(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDivisas, "OID_DIVISA", cmd, "AND"))
                QueryWhereEfectivo.Append(QueryWhereAux.ToString())
                QueryWhereMedioPago.Append(QueryWhereAux.ToString())

                For Each Divisa In Filtro.Divisas


                    If Not String.IsNullOrEmpty(QueryEfectivo) Then
                        If (String.IsNullOrEmpty(Filtro.TipoValores) OrElse (Not String.IsNullOrEmpty(Filtro.TipoValores) AndAlso Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_EFECTIVO))) AndAlso
                        Divisa.Denominaciones IsNot Nothing AndAlso Divisa.Denominaciones.Count > 0 Then

                            If IdentificadoresDenominaciones Is Nothing Then IdentificadoresDenominaciones = New List(Of String)

                            IdentificadoresDenominaciones.AddRange((From Den In Divisa.Denominaciones Select Den.Identificador).ToList)

                            objQueryDenominaciones = " AND (OID_DENOMINACION IS NULL "
                            objQueryDenominaciones &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresDenominaciones, "OID_DENOMINACION", cmd, "OR")
                            objQueryDenominaciones &= ")"

                        End If
                    End If

                    If Not String.IsNullOrEmpty(QueryMedioPago) AndAlso Divisa.MediosPago IsNot Nothing AndAlso Divisa.MediosPago.Count > 0 Then

                        If IdentificadoresMedioPago Is Nothing Then IdentificadoresMedioPago = New List(Of String)

                        If String.IsNullOrEmpty(Filtro.TipoValores) Then
                            IdentificadoresMedioPago.AddRange((From MP In Divisa.MediosPago Select MP.Identificador).ToList)
                        Else
                            For Each objMedioPago In Divisa.MediosPago
                                If objMedioPago.Tipo = Enumeradores.TipoMedioPago.Cheque OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_CHEQUE) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.OtroValor OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_OTROSVALORES) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.Tarjeta OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TARJETAS) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                ElseIf objMedioPago.Tipo = Enumeradores.TipoMedioPago.Ticket OrElse Filtro.TipoValores.Contains(ContractoServicio.Constantes.COD_TIPO_MEDIO_PAGO_TICKET) Then
                                    IdentificadoresMedioPago.Add(objMedioPago.Identificador)
                                End If
                            Next
                        End If

                        objQueryMedioPago = " AND (OID_MEDIO_PAGO IS NULL "
                        objQueryMedioPago &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, IdentificadoresMedioPago, "OID_MEDIO_PAGO", cmd, "OR")
                        objQueryMedioPago &= ")"

                    End If

                Next

            End If


            If Not String.IsNullOrEmpty(QueryEfectivo) Then
                QueryEfectivo = String.Format(QueryEfectivo, If(Not String.IsNullOrEmpty(QueryWhereSector.ToString()), QueryWhereSector.ToString(), String.Empty),
                                                             If(Not String.IsNullOrEmpty(QueryWhereEfectivo.ToString()), QueryWhereEfectivo.ToString(), String.Empty),
                                                             If(Not String.IsNullOrEmpty(objQueryDenominaciones), objQueryDenominaciones, String.Empty),
                                                             If(Not String.IsNullOrEmpty(QuerySelect.ToString()), QuerySelect.ToString(), String.Empty))

            End If

            If Not String.IsNullOrEmpty(QueryMedioPago) Then
                QueryMedioPago = String.Format(QueryMedioPago, If(Not String.IsNullOrEmpty(QueryWhereSector.ToString()), QueryWhereSector.ToString(), String.Empty),
                                                               If(Not String.IsNullOrEmpty(QueryWhereMedioPago.ToString()), QueryWhereMedioPago.ToString(), String.Empty),
                                                               If(Not String.IsNullOrEmpty(objQueryMedioPago), objQueryMedioPago, String.Empty),
                                                               If(Not String.IsNullOrEmpty(QuerySelect.ToString()), QuerySelect.ToString(), String.Empty))

            End If

            If Not String.IsNullOrEmpty(QueryEfectivo) AndAlso Not String.IsNullOrEmpty(QueryMedioPago) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo & " UNION " & QueryMedioPago)
            ElseIf Not String.IsNullOrEmpty(QueryEfectivo) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryEfectivo)
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, QueryMedioPago)
            End If

            cmd.CommandText = "SELECT * FROM (" & QuerySelectGroupBy.ToString() & cmd.CommandText & QueryGroupBy.ToString() & ") ORDER BY NIVEL, LINHA, COD_CLIENTE, DES_SECTOR"

            Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

        End Function

        Public Shared Function ConsultarSaldos(peticion As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Peticion,
                                               ValidacionesError As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError),
                                      Optional codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing) As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Saldo

            If ValidacionesError Is Nothing Then ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandType = CommandType.Text
                cmd.CommandText = My.Resources.SaldoConsultarSaldos
                Dim strWhere As New StringBuilder
                Dim strSectores As New StringBuilder
                Dim strGroupBy As String = " GROUP BY OID_CUENTA,OID_DIVISA,COD_ISO_DIVISA,DES_DIVISA,OID_SECTOR,COD_SECTOR,DES_SECTOR,OID_CLIENTE,COD_CLIENTE,DES_CLIENTE,OID_SUBCLIENTE,COD_SUBCLIENTE,DES_SUBCLIENTE, OID_PTO_SERVICIO, COD_PTO_SERVICIO, DES_PTO_SERVICIO, OID_CANAL, COD_CANAL, DES_CANAL, OID_SUBCANAL, COD_SUBCANAL, DES_SUBCANAL, COD_COLOR, BOL_DISPONIBLE "
                Dim strOrder As String = " ORDER BY DES_CLIENTE,DES_CANAL,DES_DIVISA, DES_SECTOR "
                Dim strSaldoDetGP As String = ",OID_DENOMINACION,COD_DENOMINACION, DES_DENOMINACION, COD_TIPO_MEDIO_PAGO, OID_MEDIO_PAGO, COD_MEDIO_PAGO, DES_MEDIO_PAGO "
                Dim selectDet As String = ""
                Dim selectEfectivo As String = ""
                Dim selectMedioPago As String = ""
                Dim innerDetEfectivo As String = ""
                Dim innerDetMedioPago As String = ""
                Dim innerSector As String = " INNER JOIN GEPR_TSECTOR SEC ON SEC.OID_SECTOR = CU.OID_SECTOR " &
                                            " INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = SEC.OID_PLANTA " &
                                            " INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION "

                If codigosAjenos Is Nothing Then

                    If Not String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                        If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                            strWhere.AppendLine(" AND D.COD_DELEGACION = []COD_DELEGACION")
                        Else
                            strSectores.AppendLine(" AND D.COD_DELEGACION = []COD_DELEGACION")
                        End If
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, peticion.CodigoDelegacion))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoPlanta) Then
                        If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                            strWhere.AppendLine(" AND P.COD_PLANTA = []COD_PLANTA")
                        Else
                            strSectores.AppendLine(" AND P.COD_PLANTA = []COD_PLANTA")
                        End If
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PLANTA", ProsegurDbType.Descricao_Longa, peticion.CodigoPlanta))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoIsoDivisa) Then
                        strWhere.AppendLine("AND  DIV.COD_ISO_DIVISA = []COD_ISO_DIVISA")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_ISO_DIVISA", ProsegurDbType.Descricao_Longa, peticion.CodigoIsoDivisa))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then
                        strWhere.AppendLine("AND  CL.COD_CLIENTE = []COD_CLIENTE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_CLIENTE", ProsegurDbType.Descricao_Longa, peticion.CodigoCliente))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoSubCliente) Then
                        strWhere.AppendLine("AND  SUBCL.COD_SUBCLIENTE = []COD_SUBCLIENTE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SUBCLIENTE", ProsegurDbType.Descricao_Longa, peticion.CodigoSubCliente))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoPtoServicio) Then
                        strWhere.AppendLine("AND  PTO.COD_PTO_SERVICIO = []COD_PTO_SERVICIO")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, peticion.CodigoPtoServicio))
                    End If

                    If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then
                        strWhere.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, peticion.Canales.Select(Function(a) a.CodigoCanal).ToList(), "COD_CANAL", cmd, "AND", "CN"))
                        Dim subCanales = peticion.Canales.Where(Function(a) Not String.IsNullOrEmpty(a.CodigoSubCanal)).Select(Function(b) b.CodigoSubCanal)
                        If subCanales.Count > 0 Then
                            strWhere.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, subCanales.ToList(), "COD_SUBCANAL", cmd, "AND", "SUBCN"))
                        End If
                    End If

                    If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 AndAlso (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                        strWhere.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, peticion.Sectores.Select(Function(a) a.CodigoSector).ToList(), "COD_SECTOR", cmd, "AND", "SEC"))
                    ElseIf peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 AndAlso peticion.Filtros IsNot Nothing AndAlso peticion.Filtros.IncluirSubSectores Then
                        strSectores.AppendLine(Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, peticion.Sectores.Select(Function(a) a.CodigoSector).ToList(), "COD_SECTOR", cmd, "AND", "SECT"))
                    End If

                Else

                    If Not String.IsNullOrEmpty(peticion.CodigoDelegacion) Then
                        If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                            strWhere.AppendLine(" AND D.OID_DELEGACION = []OID_DELEGACION")
                        Else
                            strSectores.AppendLine(" AND D.OID_DELEGACION = []OID_DELEGACION")
                        End If

                        Dim _delegacion As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoDelegacion AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDELEGACION")
                        If _delegacion Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoDelegacion)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DELEGACION", ProsegurDbType.Descricao_Longa, _delegacion.IdentificadorTablaGenesis))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoPlanta) Then
                        If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                            strWhere.AppendLine(" AND P.OID_PLANTA = []OID_PLANTA")
                        Else
                            strSectores.AppendLine(" AND P.OID_PLANTA = []OID_PLANTA")
                        End If

                        Dim _planta As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoPlanta AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPLANTA")
                        If _planta Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoPlanta)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PLANTA", ProsegurDbType.Descricao_Longa, _planta.IdentificadorTablaGenesis))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoIsoDivisa) Then

                        Dim _IsoDivisa As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoIsoDivisa AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDIVISA")
                        If _IsoDivisa Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoIsoDivisa)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        strWhere.AppendLine("AND  DIV.OID_DIVISA = []OID_DIVISA")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DIVISA", ProsegurDbType.Descricao_Longa, _IsoDivisa.IdentificadorTablaGenesis))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoCliente) Then

                        Dim _cliente As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoCliente AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                        If _cliente Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoCliente)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        strWhere.AppendLine("AND  CL.OID_CLIENTE = []OID_CLIENTE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Descricao_Longa, _cliente.IdentificadorTablaGenesis))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoSubCliente) Then

                        Dim _subCliente As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoSubCliente AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE")
                        If _subCliente Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoSubCliente)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        strWhere.AppendLine("AND  SUBCL.OID_SUBCLIENTE = []OID_SUBCLIENTE")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Descricao_Longa, _subCliente.IdentificadorTablaGenesis))
                    End If

                    If Not String.IsNullOrEmpty(peticion.CodigoPtoServicio) Then

                        Dim _ptoServicio As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.CodigoPtoServicio AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO")
                        If _ptoServicio Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.CodigoPtoServicio)})
                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                        End If

                        strWhere.AppendLine("AND  PTO.OID_PTO_SERVICIO = []OID_PTO_SERVICIO")
                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Descricao_Longa, _ptoServicio.IdentificadorTablaGenesis))
                    End If

                    If peticion.Canales IsNot Nothing AndAlso peticion.Canales.Count > 0 Then

                        If peticion.Canales.Count = 1 Then

                            If Not String.IsNullOrEmpty(peticion.Canales(0).CodigoCanal) Then
                                Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.Canales(0).CodigoCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                                If _canalAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.Canales(0).CodigoCanal)})
                                    Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                End If

                                strWhere.AppendLine(" AND CN.OID_CANAL = []OID_CANAL ")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL", ProsegurDbType.Descricao_Longa, _canalAjeno.IdentificadorTablaGenesis))

                            End If

                            If Not String.IsNullOrEmpty(peticion.Canales(0).CodigoSubCanal) Then
                                Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.Canales(0).CodigoSubCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                                If _subCanalAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.Canales(0).CodigoSubCanal)})
                                    Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                End If

                                strWhere.AppendLine(" AND SUBCN.OID_SUBCANAL = []OID_SUBCANAL ")
                                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Descricao_Longa, _subCanalAjeno.IdentificadorTablaGenesis))

                            End If

                        Else

                            strWhere.Append(" AND CN.OID_CANAL IN (")

                            Dim i As Integer = 0
                            For Each _canal In peticion.Canales

                                If Not String.IsNullOrEmpty(_canal.CodigoCanal) Then
                                    Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = _canal.CodigoCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                                    If _canalAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _canal.CodigoCanal)})
                                        Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                    End If

                                    If i > 0 Then
                                        strWhere.Append(", ")
                                    End If

                                    strWhere.Append("[]OID_CANAL_" & i.ToString())
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CANAL_" & i.ToString(), ProsegurDbType.Descricao_Longa, _canalAjeno.IdentificadorTablaGenesis))

                                    i = i + 1

                                End If

                            Next

                            strWhere.Append(") ")

                            Dim subCanales = peticion.Canales.Where(Function(a) Not String.IsNullOrEmpty(a.CodigoSubCanal)).Select(Function(b) b.CodigoSubCanal)
                            If subCanales.Count > 0 Then

                                strWhere.Append(" AND SUBCN.OID_SUBCANAL IN (")

                                Dim j As Integer = 0
                                For Each _subCanal In subCanales

                                    If Not String.IsNullOrEmpty(_subCanal) Then
                                        Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = _subCanal AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                                        If _subCanalAjeno Is Nothing Then
                                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _subCanal)})
                                            Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                        End If

                                        If j > 0 Then
                                            strWhere.Append(", ")
                                        End If

                                        strWhere.Append("[]OID_SUBCANAL_" & j.ToString())
                                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL_" & j.ToString(), ProsegurDbType.Descricao_Longa, _subCanalAjeno.IdentificadorTablaGenesis))

                                        j = j + 1

                                    End If

                                Next

                                strWhere.Append(") ")

                            End If

                        End If

                    End If

                    If peticion.Sectores IsNot Nothing AndAlso peticion.Sectores.Count > 0 Then

                        If peticion.Sectores.Count = 1 Then

                            If Not String.IsNullOrEmpty(peticion.Sectores(0).CodigoSector) Then
                                Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = peticion.Sectores(0).CodigoSector AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                                If _sectorAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), peticion.Sectores(0).CodigoSector)})
                                    Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                End If

                                If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                                    strWhere.AppendLine(" AND SEC.OID_SECTOR = []OID_SECTOR ")
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Longa, _sectorAjeno.IdentificadorTablaGenesis))

                                ElseIf peticion.Filtros IsNot Nothing AndAlso peticion.Filtros.IncluirSubSectores Then
                                    strSectores.AppendLine(" AND SECT.OID_SECTOR = []OID_SECTOR ")
                                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Descricao_Longa, _sectorAjeno.IdentificadorTablaGenesis))

                                End If
                            End If

                        Else

                            If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                                strWhere.Append(" AND SEC.OID_SECTOR IN (")
                            ElseIf peticion.Filtros IsNot Nothing AndAlso peticion.Filtros.IncluirSubSectores Then
                                strSectores.Append(" AND SECT.OID_SECTOR IN (")
                            End If

                            Dim i As Integer = 0
                            For Each _sector In peticion.Sectores

                                If Not String.IsNullOrEmpty(_sector.CodigoSector) Then
                                    Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.Codigo = _sector.CodigoSector AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                                    If _sectorAjeno Is Nothing Then
                                        ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL120", .descripcion = String.Format(Traduzir("VAL120"), _sector.CodigoSector)})
                                        Throw New Excepcion.NegocioExcepcion("codigoAjeno")
                                    End If

                                    If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then

                                        If i > 0 Then
                                            strWhere.Append(", ")
                                        End If

                                        strWhere.Append("[]OID_SECTOR_" & i.ToString())
                                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_" & i.ToString(), ProsegurDbType.Descricao_Longa, _sectorAjeno.IdentificadorTablaGenesis))

                                    ElseIf peticion.Filtros IsNot Nothing AndAlso peticion.Filtros.IncluirSubSectores Then

                                        If i > 0 Then
                                            strSectores.Append(", ")
                                        End If

                                        strSectores.Append("[]OID_SECTOR_" & i.ToString())
                                        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR_" & i.ToString(), ProsegurDbType.Descricao_Longa, _sectorAjeno.IdentificadorTablaGenesis))

                                    End If

                                    i = i + 1
                                End If

                            Next

                            If (peticion.Filtros Is Nothing OrElse Not peticion.Filtros.IncluirSubSectores) Then
                                strWhere.Append(") ")
                            ElseIf peticion.Filtros IsNot Nothing AndAlso peticion.Filtros.IncluirSubSectores Then
                                strSectores.Append(") ")
                            End If

                        End If

                    End If
                End If

                If peticion.Filtros IsNot Nothing Then
                    If Not peticion.Filtros.IncluirMediosPago Then
                        cmd.CommandText = cmd.CommandText.Replace(cmd.CommandText.Substring(cmd.CommandText.LastIndexOf("UNION ALL")), ")")
                    End If
                    If peticion.Filtros.IncluirSubSectores Then
                        innerSector = " INNER JOIN (" &
                                      " SELECT DISTINCT  SECT.COD_SECTOR, SECT.DES_SECTOR, SECT.OID_SECTOR " &
                                      " FROM GEPR_TSECTOR SECT " &
                                      " INNER JOIN GEPR_TPLANTA P ON P.OID_PLANTA = SECT.OID_PLANTA " &
                                      " INNER JOIN GEPR_TDELEGACION D ON D.OID_DELEGACION = P.OID_DELEGACION " &
                                      " START WITH 1 = 1  {0} " &
                                      " CONNECT BY NOCYCLE PRIOR SECT.OID_SECTOR = SECT.OID_SECTOR_PADRE " &
                                      " ) SEC ON SEC.OID_SECTOR = CU.OID_SECTOR"
                        innerSector = String.Format(innerSector, strSectores)
                    End If
                    If peticion.Filtros.SaldoDetallado Then
                        selectDet = ",OID_DENOMINACION,COD_DENOMINACION,DES_DENOMINACION,COD_TIPO_MEDIO_PAGO,OID_MEDIO_PAGO,COD_MEDIO_PAGO,DES_MEDIO_PAGO"
                        selectEfectivo += ",DENO.COD_DENOMINACION,DENO.DES_DENOMINACION,NULL COD_TIPO_MEDIO_PAGO,NULL COD_MEDIO_PAGO,NULL DES_MEDIO_PAGO"
                        selectMedioPago += ",NULL COD_DENOMINACION,NULL DES_DENOMINACION,SALDO.COD_TIPO_MEDIO_PAGO,MP.COD_MEDIO_PAGO,MP.DES_MEDIO_PAGO"
                        innerDetEfectivo = " LEFT JOIN GEPR_TDENOMINACION DENO ON DENO.OID_DENOMINACION = SALDO.OID_DENOMINACION "
                        innerDetMedioPago = " LEFT JOIN GEPR_TMEDIO_PAGO MP ON MP.OID_MEDIO_PAGO = SALDO.OID_MEDIO_PAGO "
                        strGroupBy += strSaldoDetGP
                    End If
                    strWhere.AppendLine("AND  BOL_DISPONIBLE = []BOL_DISPONIBLE")
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, peticion.Filtros.SaldoDisponible))
                End If

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, strWhere, selectDet, selectEfectivo, selectMedioPago, innerDetEfectivo + innerSector, innerDetMedioPago + innerSector) + strGroupBy + strOrder)

                Dim dtResult = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
                Return cargarConsultaSaldos(dtResult, ValidacionesError, codigosAjenos)

            End Using

        End Function

        Public Shared Function cargarConsultaSaldos(tdConsultaSaldos As DataTable,
                                                    ValidacionesError As List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError),
                                           Optional codigosAjenos As ObservableCollection(Of Clases.CodigoAjeno) = Nothing) As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Saldo

            If ValidacionesError Is Nothing Then ValidacionesError = New List(Of ContractoServicio.Contractos.Integracion.Comon.ValidacionError)

            Dim objSaldo As New ContractoServicio.Contractos.Integracion.ConsultarSaldos.Saldo
            objSaldo.Sectores = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.SectorSaldo)

            If tdConsultaSaldos IsNot Nothing AndAlso tdConsultaSaldos.Rows.Count > 0 Then

                Dim identificadorAjeno As String = ""
                Dim trabajaConAjeno As Boolean = False

                If codigosAjenos IsNot Nothing AndAlso codigosAjenos.Count > 0 Then

                    identificadorAjeno = codigosAjenos(0).CodigoIdentificador
                    trabajaConAjeno = True

                    codigosAjenos = New ObservableCollection(Of Clases.CodigoAjeno)

                    For Each objRow As DataRow In tdConsultaSaldos.Rows

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CLIENTE"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCLIENTE"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_PTO_SERVICIO"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PTO_SERVICIO"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_CANAL"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SUBCANAL"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL"), GetType(String))})
                        End If

                        If objRow.Table.Columns.Contains("OID_MEDIO_PAGO") Then
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))) Then
                                codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TMEDIO_PAGO", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))})
                            End If
                        End If

                        If objRow.Table.Columns.Contains("OID_DENOMINACION") Then
                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))) Then
                                codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDENOMINACION", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))})
                            End If
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TSECTOR", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String))})
                        End If

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))) Then
                            codigosAjenos.Add(New Clases.CodigoAjeno With {.CodigoTipoTablaGenesis = "GEPR_TDIVISA", .IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String))})
                        End If

                    Next

                    AccesoDatos.Genesis.CodigoAjeno.ObtenerCodigosAjenos(identificadorAjeno, codigosAjenos)

                End If

                For Each objRow As DataRow In tdConsultaSaldos.Rows
                    Dim codSector As String = Util.AtribuirValorObj(objRow("COD_SECTOR"), GetType(String))
                    Dim desSector = Util.AtribuirValorObj(objRow("DES_SECTOR"), GetType(String))

                    If trabajaConAjeno Then
                        Dim _sectorAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSECTOR")
                        If _sectorAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SECTOR"), GetType(String)), "GEPR_TSECTOR")})
                            Exit For
                        End If
                        codSector = _sectorAjeno.Codigo
                        desSector = _sectorAjeno.Descripcion
                    End If

                    Dim objSector As ContractoServicio.Contractos.Integracion.ConsultarSaldos.SectorSaldo = objSaldo.Sectores.Find(Function(a) a.Codigo = codSector)

                    If objSector Is Nothing Then
                        objSector = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.SectorSaldo

                        With objSector

                            .Codigo = codSector
                            .Descripcion = desSector
                            .Clientes = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.Cliente)

                        End With
                        objSaldo.Sectores.Add(objSector)

                    End If

                    Dim codCliente As String = Util.AtribuirValorObj(objRow("COD_CLIENTE"), GetType(String))
                    Dim desCliente = Util.AtribuirValorObj(objRow("DES_CLIENTE"), GetType(String))
                    Dim codSubCliente As String = Util.AtribuirValorObj(objRow("COD_SUBCLIENTE"), GetType(String))
                    Dim desSubCliente = Util.AtribuirValorObj(objRow("DES_SUBCLIENTE"), GetType(String))
                    Dim codPuntoServicio As String = Util.AtribuirValorObj(objRow("COD_PTO_SERVICIO"), GetType(String))
                    Dim desPuntoServicio = Util.AtribuirValorObj(objRow("DES_PTO_SERVICIO"), GetType(String))

                    'Procura os dados do cliente ajeno no menor nível
                    If trabajaConAjeno Then
                        Dim _clienteAjeno As Clases.CodigoAjeno = Nothing

                        If Not String.IsNullOrEmpty(codPuntoServicio) Then
                            _clienteAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_PTO_SERVICIO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TPUNTO_SERVICIO")
                        ElseIf Not String.IsNullOrEmpty(codSubCliente) Then
                            _clienteAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCLIENTE"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCLIENTE")
                        Else
                            _clienteAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CLIENTE"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCLIENTE")
                        End If

                        If _clienteAjeno Is Nothing Then
                            If Not String.IsNullOrEmpty(codPuntoServicio) Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_PTO_SERVICIO"), GetType(String)), "GEPR_TPUNTO_SERVICIO")})
                            ElseIf Not String.IsNullOrEmpty(codSubCliente) Then
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCLIENTE"), GetType(String)), "GEPR_TSUBCLIENTE")})
                            Else
                                ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CLIENTE"), GetType(String)), "GEPR_TCLIENTE")})
                            End If
                            Exit For
                        End If

                        'Prenche na variável os dados do código ajeno
                        codCliente = _clienteAjeno.Codigo
                        desCliente = _clienteAjeno.Descripcion
                    End If

                    Dim objCliente As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Cliente = Nothing
                    'Localiza nos clientes do setor se já existe
                    'Se for código ajeno localiza com o código preenchido acima, se não localiza com os códigos genesis no menor nível
                    If trabajaConAjeno Then
                        objCliente = objSector.Clientes.Find(Function(a) a.Codigo = codCliente)
                    Else
                        'localiza com os códigos genesis no menor nível
                        If Not String.IsNullOrEmpty(codPuntoServicio) Then
                            objCliente = objSector.Clientes.Find(Function(a) a.Codigo = codPuntoServicio)
                        ElseIf Not String.IsNullOrEmpty(codSubCliente) Then
                            objCliente = objSector.Clientes.Find(Function(a) a.Codigo = codSubCliente)
                        Else
                            objCliente = objSector.Clientes.Find(Function(a) a.Codigo = codCliente)
                        End If
                    End If

                    If objCliente Is Nothing Then
                        objCliente = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.Cliente

                        With objCliente

                            'Se for código ajeno preenche com os dados localizados acima, se não preenche com os códigos genesis no menor nível
                            If trabajaConAjeno Then
                                .Codigo = codCliente
                                .Descripcion = desCliente
                            Else
                                'Preenche com os códigos genesis no menor nível
                                If Not String.IsNullOrEmpty(codPuntoServicio) Then
                                    .Codigo = codPuntoServicio
                                    .Descripcion = desPuntoServicio
                                ElseIf Not String.IsNullOrEmpty(codSubCliente) Then
                                    .Codigo = codSubCliente
                                    .Descripcion = desSubCliente
                                Else
                                    .Codigo = codCliente
                                    .Descripcion = desCliente
                                End If
                            End If


                            .Canales = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.Canal)

                        End With
                        objSector.Clientes.Add(objCliente)
                    End If

                    Dim codCanal As String = Util.AtribuirValorObj(objRow("COD_CANAL"), GetType(String))
                    Dim desCanal = Util.AtribuirValorObj(objRow("DES_CANAL"), GetType(String))

                    If trabajaConAjeno Then
                        Dim _canalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_CANAL"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TCANAL")
                        If _canalAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_CANAL"), GetType(String)), "GEPR_TCANAL")})
                            Exit For
                        End If
                        codCanal = _canalAjeno.Codigo
                        desCanal = _canalAjeno.Descripcion
                    End If

                    Dim objCanal As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Canal = objCliente.Canales.Find(Function(a) a.Codigo = codCanal)

                    If objCanal Is Nothing Then
                        objCanal = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.Canal

                        With objCanal

                            .Codigo = codCanal
                            .Descripcion = desCanal
                            .Subcanales = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.SubCanal)

                        End With
                        objCliente.Canales.Add(objCanal)
                    End If

                    Dim codSubCanal As String = Util.AtribuirValorObj(objRow("COD_SUBCANAL"), GetType(String))
                    Dim desSubCanal = Util.AtribuirValorObj(objRow("DES_SUBCANAL"), GetType(String))

                    If trabajaConAjeno Then
                        Dim _subCanalAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_SUBCANAL"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TSUBCANAL")
                        If _subCanalAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_SUBCANAL"), GetType(String)), "GEPR_TSUBCANAL")})
                            Exit For
                        End If
                        codSubCanal = _subCanalAjeno.Codigo
                        desSubCanal = _subCanalAjeno.Descripcion
                    End If

                    Dim objSubCanal As ContractoServicio.Contractos.Integracion.ConsultarSaldos.SubCanal = objCanal.Subcanales.Find(Function(a) a.Codigo = codSubCanal)

                    If objSubCanal Is Nothing Then
                        objSubCanal = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.SubCanal

                        With objSubCanal

                            .Codigo = codSubCanal
                            .Descripcion = desSubCanal
                            .Divisas = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.Divisa)

                        End With
                        objCanal.Subcanales.Add(objSubCanal)
                    End If

                    Dim codIsoDivisa As String = Util.AtribuirValorObj(objRow("COD_ISO_DIVISA"), GetType(String))
                    Dim desDivisa As String = Util.AtribuirValorObj(objRow("DES_DIVISA"), GetType(String))

                    If trabajaConAjeno Then
                        Dim _divisaAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDIVISA")
                        If _divisaAjeno Is Nothing Then
                            ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DIVISA"), GetType(String)), "GEPR_TDIVISA")})
                            Exit For
                        End If
                        codIsoDivisa = _divisaAjeno.Codigo
                        desDivisa = _divisaAjeno.Descripcion
                    End If

                    Dim disponible As String = Util.AtribuirValorObj(objRow("BOL_DISPONIBLE"), GetType(Boolean))

                    Dim objDivisa As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Divisa = objSubCanal.Divisas.Find(Function(a) a.Codigo = codIsoDivisa AndAlso a.Disponible = disponible)

                    If objDivisa Is Nothing Then
                        objDivisa = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.Divisa

                        With objDivisa

                            .Codigo = codIsoDivisa
                            .Descripcion = desDivisa
                            .Disponible = disponible
                            .Denominaciones = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.Denominacion)
                            .ImporteTotalEfectivo = Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO"), GetType(Decimal))
                            .ImporteTotalMedioPago = Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP"), GetType(Decimal))
                            .ImporteEfectivo = Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO_DET"), GetType(Decimal))
                            .ImporteMedioPago = Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP_DET"), GetType(Decimal))
                            .MediosPago = New List(Of ContractoServicio.Contractos.Integracion.ConsultarSaldos.MedioPago)

                        End With
                        objSubCanal.Divisas.Add(objDivisa)
                    Else
                        With objDivisa

                            .ImporteTotalEfectivo += Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO"), GetType(Decimal))
                            .ImporteTotalMedioPago += Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP"), GetType(Decimal))
                            .ImporteEfectivo += Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO_DET"), GetType(Decimal))
                            .ImporteMedioPago += Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP_DET"), GetType(Decimal))

                        End With
                    End If

                    Dim codDenominacion As String = String.Empty
                    Dim desDenominacion As String = String.Empty

                    If objRow.Table.Columns.Contains("COD_DENOMINACION") Then

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String))) Then
                            desDenominacion = Util.AtribuirValorObj(objRow("DES_DENOMINACION"), GetType(String))
                            codDenominacion = Util.AtribuirValorObj(objRow("COD_DENOMINACION"), GetType(String))

                            If trabajaConAjeno Then
                                Dim _denominacionAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TDENOMINACION")
                                If _denominacionAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_DENOMINACION"), GetType(String)), "GEPR_TDENOMINACION")})
                                    Exit For
                                End If
                                codDenominacion = _denominacionAjeno.Codigo
                                desDenominacion = _denominacionAjeno.Descripcion
                            End If
                        End If

                    End If

                    If Not String.IsNullOrEmpty(codDenominacion) Then

                        Dim objDenominacion As ContractoServicio.Contractos.Integracion.ConsultarSaldos.Denominacion = objDivisa.Denominaciones.Find(Function(a) a.Codigo = codDenominacion)

                        If objDenominacion Is Nothing Then
                            objDenominacion = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.Denominacion

                            With objDenominacion

                                .Codigo = codDenominacion
                                .DescripcionDenominacion = desDenominacion
                                .Importe = Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO"), GetType(Decimal))
                                .Cantidad = Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))
                            End With
                            objDivisa.Denominaciones.Add(objDenominacion)
                        Else
                            With objDenominacion

                                .Importe += Util.AtribuirValorObj(objRow("NUM_IMPORTE_EFECTIVO"), GetType(Decimal))
                                .Cantidad += Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))

                            End With
                        End If

                    End If

                    Dim codMedioPago As String = String.Empty
                    Dim desMedioPago As String = String.Empty

                    If objRow.Table.Columns.Contains("COD_MEDIO_PAGO") Then

                        If Not String.IsNullOrEmpty(Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String))) Then
                            codMedioPago = Util.AtribuirValorObj(objRow("COD_MEDIO_PAGO"), GetType(String))
                            desMedioPago = Util.AtribuirValorObj(objRow("DES_MEDIO_PAGO"), GetType(String))

                            If trabajaConAjeno Then
                                Dim _medioPagoAjeno As Clases.CodigoAjeno = codigosAjenos.FirstOrDefault(Function(x) x.IdentificadorTablaGenesis = Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String)) AndAlso x.CodigoTipoTablaGenesis = "GEPR_TMEDIO_PAGO")
                                If _medioPagoAjeno Is Nothing Then
                                    ValidacionesError.Add(New ContractoServicio.Contractos.Integracion.Comon.ValidacionError With {.codigo = "VAL119", .descripcion = String.Format(Traduzir("VAL119"), Util.AtribuirValorObj(objRow("OID_MEDIO_PAGO"), GetType(String)), "GEPR_TMEDIO_PAGO")})
                                    Exit For
                                End If
                                codMedioPago = _medioPagoAjeno.Codigo
                                desMedioPago = _medioPagoAjeno.Descripcion
                            End If
                        End If

                    End If

                    If Not String.IsNullOrEmpty(codMedioPago) Then

                        Dim objMedioPago As ContractoServicio.Contractos.Integracion.ConsultarSaldos.MedioPago = objDivisa.MediosPago.Find(Function(a) a.CodigoMedioPago = codMedioPago)

                        If objMedioPago Is Nothing Then
                            objMedioPago = New ContractoServicio.Contractos.Integracion.ConsultarSaldos.MedioPago

                            With objMedioPago

                                .CodigoIsoDivisa = codIsoDivisa
                                .CodigoMedioPago = codMedioPago
                                .CodigoTipoMedioPago = Util.AtribuirValorObj(objRow("COD_TIPO_MEDIO_PAGO"), GetType(String))
                                .DescripcionMedioPago = desMedioPago
                                .Importe = Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP"), GetType(Decimal))
                                .Unidades = Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))
                            End With
                            objDivisa.MediosPago.Add(objMedioPago)
                        Else
                            With objMedioPago

                                .Importe += Util.AtribuirValorObj(objRow("NUM_IMPORTE_MP"), GetType(Decimal))
                                .Unidades += Util.AtribuirValorObj(objRow("NEL_CANTIDAD"), GetType(Long))

                            End With
                        End If

                    End If

                Next

            End If

            If ValidacionesError IsNot Nothing AndAlso ValidacionesError.Count > 0 Then
                Throw New Excepcion.NegocioExcepcion("codigoAjeno")
            End If

            Return objSaldo
        End Function

        Public Shared Function ObtenerSaldoModificar(cuenta As Clases.Cuenta, Optional utilizarClienteTotalizador As Boolean = True) As ObservableCollection(Of Clases.Divisa)

            Dim cuentaSaldo As New Clases.Cuenta
            Dim clienteSaldo As Clases.Cliente

            If (utilizarClienteTotalizador) Then
                clienteSaldo = AccesoDatos.Genesis.Cliente.ObtenerClienteTotalizadorSaldo(cuenta)
            Else
                clienteSaldo = cuenta.Cliente
            End If

            If clienteSaldo IsNot Nothing Then

                cuentaSaldo.Cliente = clienteSaldo
                cuentaSaldo.SubCanal = cuenta.SubCanal
                cuentaSaldo.Sector = cuenta.Sector

                If clienteSaldo.SubClientes IsNot Nothing AndAlso clienteSaldo.SubClientes.Count > 0 Then
                    cuentaSaldo.SubCliente = clienteSaldo.SubClientes.First

                    If clienteSaldo.SubClientes.First.PuntosServicio IsNot Nothing AndAlso clienteSaldo.SubClientes.First.PuntosServicio.Count > 0 Then
                        cuentaSaldo.PuntoServicio = clienteSaldo.SubClientes.First.PuntosServicio.First
                    End If

                End If

                Dim tfEfectivos As Task
                Dim tfMediosPago As Task

                Dim divisasEfectivos As ObservableCollection(Of Clases.Divisa) = Nothing
                Dim divisasMediosPago As ObservableCollection(Of Clases.Divisa) = Nothing

                tfEfectivos = New Task(Sub()
                                           divisasEfectivos = ObtenerSaldoEfectivoModificar(cuentaSaldo)
                                       End Sub)

                tfMediosPago = New Task(Sub()
                                            divisasMediosPago = ObtenerSaldoMedioPagoModificar(cuentaSaldo)
                                        End Sub)

                tfEfectivos.Start()
                tfMediosPago.Start()

                Task.WaitAll(New Task() {tfEfectivos, tfMediosPago})

                Dim divisas As New ObservableCollection(Of Clases.Divisa)

                If divisasEfectivos IsNot Nothing AndAlso divisasEfectivos.Count > 0 Then
                    divisas.AddRange(divisasEfectivos)
                End If

                If divisasMediosPago IsNot Nothing AndAlso divisasMediosPago.Count > 0 Then
                    divisas.AddRange(divisasMediosPago)
                End If

                Return divisas

            End If

            Return Nothing

        End Function

        Private Shared Function ObtenerSaldoEfectivoModificar(cuenta As Clases.Cuenta) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoEfectivoModificar

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, cuenta.Cliente.Identificador))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, cuenta.Sector.Identificador))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, cuenta.SubCanal.Identificador))

            Dim SQL As New StringBuilder
            If cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Identificador) Then
                SQL.Append(" AND C.OID_SUBCLIENTE =[]OID_SUBCLIENTE")
                _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, cuenta.SubCliente.Identificador))

                If cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Identificador) Then
                    SQL.Append(" AND C.OID_PTO_SERVICIO =[]OID_PTO_SERVICIO")
                    _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, cuenta.PuntoServicio.Identificador))

                Else
                    SQL.Append(" AND C.OID_PTO_SERVICIO IS NULL")
                End If

            Else
                SQL.Append(" AND C.OID_SUBCLIENTE IS NULL")
                SQL.Append(" AND C.OID_PTO_SERVICIO IS NULL")
            End If

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(_command.CommandText, SQL.ToString))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))


                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorEfectivo As New Clases.ValorEfectivo
                            valorEfectivo.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            valorEfectivo.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(dr("COD_TIPO_EFECTIVO_TOTAL").ToString)
                            valorEfectivo.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorEfectivo.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            divisa.ValoresTotalesEfectivo.Add(valorEfectivo)
                        Else
                            Dim denominacion As New Clases.Denominacion
                            denominacion.Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                            denominacion.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                            denominacion.Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                            denominacion.EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean))
                            denominacion.Valor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Decimal))

                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                            Dim valorDenominacion As New Clases.ValorDenominacion
                            valorDenominacion.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                            valorDenominacion.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))

                            If dr("COD_CALIDAD") IsNot DBNull.Value Then
                                valorDenominacion.Calidad = New Clases.Calidad With {.Codigo = Util.AtribuirValorObj(dr("COD_CALIDAD"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_CALIDAD"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))}
                            End If

                            If dr("OID_UNIDAD_MEDIDA") IsNot DBNull.Value Then
                                valorDenominacion.UnidadMedida = New Clases.UnidadMedida With {.Codigo = Util.AtribuirValorObj(dr("COD_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .EsPadron = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean)),
                                                                                 .ValorUnidad = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(Decimal)),
                                                                                 .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(dr("COD_TIPO_UNIDAD_MEDIDA"), GetType(Boolean)))}
                            End If
                            denominacion.ValorDenominacion.Add(valorDenominacion)
                            divisa.Denominaciones.Add(denominacion)
                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Private Shared Function ObtenerSaldoMedioPagoModificar(cuenta As Clases.Cuenta)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoMedioPagoModificar

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, cuenta.Cliente.Identificador))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SECTOR", ProsegurDbType.Identificador_Alfanumerico, cuenta.Sector.Identificador))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, cuenta.SubCanal.Identificador))

            Dim SQL As New StringBuilder
            If cuenta.SubCliente IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.SubCliente.Identificador) Then
                SQL.Append(" AND C.OID_SUBCLIENTE =[]OID_SUBCLIENTE")
                _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, cuenta.SubCliente.Identificador))

                If cuenta.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cuenta.PuntoServicio.Identificador) Then
                    SQL.Append(" AND C.OID_PTO_SERVICIO =[]OID_PTO_SERVICIO")
                    _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, cuenta.PuntoServicio.Identificador))

                Else
                    SQL.Append(" AND C.OID_PTO_SERVICIO IS NULL")
                End If

            Else
                SQL.Append(" AND C.OID_SUBCLIENTE IS NULL")
                SQL.Append(" AND C.OID_PTO_SERVICIO IS NULL")
            End If

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(_command.CommandText, SQL.ToString))
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))

                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorTipoMedioPago As New Clases.ValorTipoMedioPago
                            valorTipoMedioPago.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            valorTipoMedioPago.TipoMedioPago = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                            valorTipoMedioPago.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorTipoMedioPago.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTipoMedioPago)
                        Else
                            Dim mediopago As Clases.MedioPago = divisa.MediosPago.Find(Function(mp) mp.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String)))

                            If mediopago Is Nothing Then
                                mediopago = New Clases.MedioPago
                                mediopago.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))
                                mediopago.Codigo = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String))
                                mediopago.Descripcion = Util.AtribuirValorObj(dr("DES_MEDIO_PAGO"), GetType(String))
                                mediopago.Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                                mediopago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                Dim valorMedioPago As New Clases.ValorMedioPago
                                valorMedioPago.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                                valorMedioPago.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                                mediopago.Valores.Add(valorMedioPago)
                                divisa.MediosPago.Add(mediopago)
                            Else
                                mediopago.Valores.FirstOrDefault.Cantidad += Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                                mediopago.Valores.FirstOrDefault.Importe += Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            End If

                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Public Shared Function ObtenerSaldoAnterior(identificadorDocumento As String, modo As Enumeradores.Modo) As ObservableCollection(Of Clases.Divisa)

            Dim tfEfectivos As Task
            Dim tfMediosPago As Task

            Dim divisasEfectivoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim divisasMediosPagoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing

            tfEfectivos = New Task(Sub()
                                       divisasEfectivoAnterior = ObteneSaldoEfectivoAnterior(identificadorDocumento, modo)
                                   End Sub)

            tfMediosPago = New Task(Sub()
                                        divisasMediosPagoAnterior = ObteneSaldoMedioPagoAnterior(identificadorDocumento, modo)
                                    End Sub)

            tfEfectivos.Start()
            tfMediosPago.Start()

            Task.WaitAll(New Task() {tfEfectivos, tfMediosPago})

            Dim divisas As New ObservableCollection(Of Clases.Divisa)

            If divisasEfectivoAnterior IsNot Nothing AndAlso divisasEfectivoAnterior.Count > 0 Then
                divisas.AddRange(divisasEfectivoAnterior)
            End If

            If divisasMediosPagoAnterior IsNot Nothing AndAlso divisasMediosPagoAnterior.Count > 0 Then
                divisas.AddRange(divisasMediosPagoAnterior)
            End If

            Return divisas

        End Function

        Public Shared Function ObtenerSaldoAnteriorModificar(identificadorDocumento As String) As ObservableCollection(Of Clases.Divisa)

            Dim tfEfectivos As Task
            Dim tfMediosPago As Task

            Dim divisasEfectivoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim divisasMediosPagoAnterior As ObservableCollection(Of Clases.Divisa) = Nothing

            tfEfectivos = New Task(Sub()
                                       divisasEfectivoAnterior = ObteneSaldoEfectivoAnteriorModificar(identificadorDocumento)
                                   End Sub)

            tfMediosPago = New Task(Sub()
                                        divisasMediosPagoAnterior = ObteneSaldoMedioPagoAnteriorModificar(identificadorDocumento)
                                    End Sub)

            tfEfectivos.Start()
            tfMediosPago.Start()

            Task.WaitAll(New Task() {tfEfectivos, tfMediosPago})

            Dim divisas As New ObservableCollection(Of Clases.Divisa)

            If divisasEfectivoAnterior IsNot Nothing AndAlso divisasEfectivoAnterior.Count > 0 Then
                divisas.AddRange(divisasEfectivoAnterior)
            End If

            If divisasMediosPagoAnterior IsNot Nothing AndAlso divisasMediosPagoAnterior.Count > 0 Then
                divisas.AddRange(divisasMediosPagoAnterior)
            End If

            Return divisas

        End Function

        Private Shared Function ObteneSaldoEfectivoAnterior(identificadorDocumento As String, modo As Enumeradores.Modo) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoEfectivoAnterior

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))


                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorEfectivo As New Clases.ValorEfectivo
                            valorEfectivo.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            valorEfectivo.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(dr("COD_TIPO_EFECTIVO_TOTAL").ToString)
                            valorEfectivo.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorEfectivo.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            valorEfectivo.Detallar = modo = Enumeradores.Modo.Consulta
                            divisa.ValoresTotalesEfectivo.Add(valorEfectivo)
                        Else
                            Dim denominacion As New Clases.Denominacion
                            denominacion.Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                            denominacion.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                            denominacion.Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                            denominacion.EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean))
                            denominacion.Valor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Decimal))

                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                            Dim valorDenominacion As New Clases.ValorDenominacion
                            valorDenominacion.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                            valorDenominacion.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            valorDenominacion.Detallar = modo = Enumeradores.Modo.Consulta

                            If dr("COD_CALIDAD") IsNot DBNull.Value Then
                                valorDenominacion.Calidad = New Clases.Calidad With {.Codigo = Util.AtribuirValorObj(dr("COD_CALIDAD"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_CALIDAD"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))}
                            End If

                            If dr("OID_UNIDAD_MEDIDA") IsNot DBNull.Value Then
                                valorDenominacion.UnidadMedida = New Clases.UnidadMedida With {.Codigo = Util.AtribuirValorObj(dr("COD_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .EsPadron = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean)),
                                                                                 .ValorUnidad = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(Decimal)),
                                                                                 .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(dr("COD_TIPO_UNIDAD_MEDIDA"), GetType(Boolean)))}
                            End If
                            denominacion.ValorDenominacion.Add(valorDenominacion)
                            divisa.Denominaciones.Add(denominacion)
                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Private Shared Function ObteneSaldoMedioPagoAnterior(identificadorDocumento As String, modo As Enumeradores.Modo)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoMedioPagoAnterior

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))

                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorTipoMedioPago As New Clases.ValorTipoMedioPago
                            valorTipoMedioPago.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                            valorTipoMedioPago.TipoMedioPago = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                            valorTipoMedioPago.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorTipoMedioPago.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            valorTipoMedioPago.Detallar = modo = Enumeradores.Modo.Consulta
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTipoMedioPago)
                        Else

                            Dim mediopago As Clases.MedioPago = divisa.MediosPago.Find(Function(mp) mp.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String)))

                            If mediopago Is Nothing Then
                                mediopago = New Clases.MedioPago
                                mediopago.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))
                                mediopago.Codigo = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String))
                                mediopago.Descripcion = Util.AtribuirValorObj(dr("DES_MEDIO_PAGO"), GetType(String))
                                mediopago.Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                                mediopago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                Dim valorMedioPago As New Clases.ValorMedioPago
                                valorMedioPago.Cantidad = Util.AtribuirValorObj(dr("NEL_CANTIDAD"), GetType(Int64))
                                valorMedioPago.Importe = Util.AtribuirValorObj(dr("NUM_IMPORTE"), GetType(Decimal))
                                valorMedioPago.Detallar = modo = Enumeradores.Modo.Consulta
                                mediopago.Valores.Add(valorMedioPago)
                                divisa.MediosPago.Add(mediopago)
                            End If

                            If Not String.IsNullOrEmpty(Util.AtribuirValorObj(dr("T_OID_TERMINO"), GetType(String))) Then
                                Dim termino = New Clases.Termino
                                mediopago.Terminos = New ObservableCollection(Of Clases.Termino)

                                With termino
                                    .Identificador = If(dr.Table.Columns.Contains("T_OID_TERMINO"), Util.AtribuirValorObj(dr("T_OID_TERMINO"), GetType(String)), Nothing)
                                    .Codigo = If(dr.Table.Columns.Contains("T_COD_TERMINO"), Util.AtribuirValorObj(dr("T_COD_TERMINO"), GetType(String)), Nothing)
                                    .Descripcion = If(dr.Table.Columns.Contains("T_DES_TERMINO"), Util.AtribuirValorObj(dr("T_DES_TERMINO"), GetType(String)), Nothing)
                                    .Observacion = If(dr.Table.Columns.Contains("T_OBS_TERMINO"), Util.AtribuirValorObj(dr("T_OBS_TERMINO"), GetType(String)), Nothing)
                                    .ValorInicial = If(dr.Table.Columns.Contains("T_DES_VALOR_INICIAL"), Util.AtribuirValorObj(dr("T_DES_VALOR_INICIAL"), GetType(String)), Nothing)
                                    .Longitud = If(dr.Table.Columns.Contains("T_NEC_LONGITUD"), Util.AtribuirValorObj(dr("T_NEC_LONGITUD"), GetType(Integer)), Nothing)
                                    .MostrarDescripcionConCodigo = If(dr.Table.Columns.Contains("T_BOL_MOSTRAR_CODIGO"), Util.AtribuirValorObj(dr("T_BOL_MOSTRAR_CODIGO"), GetType(Int16)), Nothing)
                                    .Orden = If(dr.Table.Columns.Contains("T_NEC_ORDEN"), Util.AtribuirValorObj(dr("T_NEC_ORDEN"), GetType(Integer)), Nothing)
                                    .EstaActivo = If(dr.Table.Columns.Contains("T_BOL_VIGENTE"), Util.AtribuirValorObj(dr("T_BOL_VIGENTE"), GetType(Int16)), Nothing)
                                    .CodigoUsuario = If(dr.Table.Columns.Contains("T_COD_USUARIO"), Util.AtribuirValorObj(dr("T_COD_USUARIO"), GetType(String)), Nothing)
                                    .FechaHoraActualizacion = If(dr.Table.Columns.Contains("T_FYH_ACTUALIZACION"), Util.AtribuirValorObj(dr("T_FYH_ACTUALIZACION"), GetType(DateTime)), Nothing)
                                    .Valor = If(dr.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)), Nothing)

                                    If dr.Table.Columns.Contains("OID_FORMATO") Then
                                        .Formato = New Clases.Formato
                                        .Formato.Identificador = If(dr.Table.Columns.Contains("OID_FORMATO"), Util.AtribuirValorObj(dr("OID_FORMATO"), GetType(String)), Nothing)
                                        .Formato.Codigo = If(dr.Table.Columns.Contains("COD_FORMATO"), Util.AtribuirValorObj(dr("COD_FORMATO"), GetType(String)), Nothing)
                                        .Formato.Descripcion = If(dr.Table.Columns.Contains("DES_FORMATO"), Util.AtribuirValorObj(dr("DES_FORMATO"), GetType(String)), Nothing)
                                    End If

                                    If dr.Table.Columns.Contains("OID_MASCARA") Then
                                        .Mascara = New Clases.Mascara
                                        .Mascara.Identificador = If(dr.Table.Columns.Contains("OID_MASCARA"), Util.AtribuirValorObj(dr("OID_MASCARA"), GetType(String)), Nothing)
                                        .Mascara.Codigo = If(dr.Table.Columns.Contains("COD_MASCARA"), Util.AtribuirValorObj(dr("COD_MASCARA"), GetType(String)), Nothing)
                                        .Mascara.Descripcion = If(dr.Table.Columns.Contains("DES_MASCARA"), Util.AtribuirValorObj(dr("DES_MASCARA"), GetType(String)), Nothing)
                                        .Mascara.ExpresionRegular = If(dr.Table.Columns.Contains("DES_EXP_REGULAR"), Util.AtribuirValorObj(dr("DES_EXP_REGULAR"), GetType(String)), Nothing)
                                    End If

                                    If dr.Table.Columns.Contains("OID_ALGORITMO_VALIDACION") Then
                                        .AlgoritmoValidacion = New Clases.AlgoritmoValidacion
                                        .AlgoritmoValidacion.Identificador = If(dr.Table.Columns.Contains("OID_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(dr("OID_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                        .AlgoritmoValidacion.Codigo = If(dr.Table.Columns.Contains("COD_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(dr("COD_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                        .AlgoritmoValidacion.Descripcion = If(dr.Table.Columns.Contains("DES_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(dr("DES_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                        .AlgoritmoValidacion.Observacion = If(dr.Table.Columns.Contains("OBS_ALGORITMO_VALIDACION"), Util.AtribuirValorObj(dr("OBS_ALGORITMO_VALIDACION"), GetType(String)), Nothing)
                                    End If

                                    If dr.Table.Columns.Contains("OID_VALOR") Then
                                        .ValoresPosibles = New ObservableCollection(Of Clases.TerminoValorPosible)
                                        Dim valor As New Clases.TerminoValorPosible
                                        valor.Identificador = If(dr.Table.Columns.Contains("OID_VALOR"), Util.AtribuirValorObj(dr("OID_VALOR"), GetType(String)), Nothing)
                                        valor.Codigo = If(dr.Table.Columns.Contains("COD_VALOR"), Util.AtribuirValorObj(dr("COD_VALOR"), GetType(String)), Nothing)
                                        valor.Descripcion = If(dr.Table.Columns.Contains("DES_VALOR"), Util.AtribuirValorObj(dr("DES_VALOR"), GetType(String)), Nothing)
                                        valor.EstaActivo = If(dr.Table.Columns.Contains("VT_BOL_VIGENTE"), Util.AtribuirValorObj(dr("VT_BOL_VIGENTE"), GetType(String)), Nothing)
                                        .ValoresPosibles.Add(valor)
                                    End If

                                    If Not String.IsNullOrEmpty(.Valor) Then

                                        If mediopago.Valores Is Nothing OrElse mediopago.Valores.Count = 0 Then
                                            mediopago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                            mediopago.Valores.FirstOrDefault.Terminos = New ObservableCollection(Of Clases.Termino)
                                            mediopago.Valores.FirstOrDefault.Terminos.Add(termino)
                                        Else
                                            If mediopago.Valores.Last.Terminos Is Nothing OrElse mediopago.Valores.Last.Terminos.Count = 0 Then
                                                mediopago.Valores.Last.Terminos = New ObservableCollection(Of Clases.Termino)
                                                mediopago.Valores.FirstOrDefault.Terminos.Add(termino)
                                            Else
                                                mediopago.Valores.Last.Terminos.Add(termino)
                                            End If
                                        End If

                                    End If

                                End With

                                mediopago.Terminos.Add(termino)
                            End If

                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Private Shared Function ObteneSaldoEfectivoAnteriorModificar(identificadorDocumento As String) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoEfectivoAnterior

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each idDivisa In (From tab In dt.AsEnumerable
                                      Group tab By CodigoIsoDivisa = tab.Field(Of String)("OID_DIVISA")
                                  Into Group
                                      Order By CodigoIsoDivisa
                                      Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    For Each dr In dt.Select(String.Format("OID_DIVISA ='{0}'", idDivisa))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))

                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorEfectivo As New Clases.ValorEfectivo
                            valorEfectivo.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(dr("COD_TIPO_EFECTIVO_TOTAL").ToString)
                            valorEfectivo.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorEfectivo.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            divisa.ValoresTotalesEfectivo.Add(valorEfectivo)
                        Else
                            Dim denominacion As New Clases.Denominacion
                            denominacion.Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))

                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                            Dim valorDenominacion As New Clases.ValorDenominacion

                            If dr("COD_CALIDAD") IsNot DBNull.Value Then
                                valorDenominacion.Calidad = New Clases.Calidad With {.Identificador = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))}
                            End If

                            If dr("OID_UNIDAD_MEDIDA") IsNot DBNull.Value Then
                                valorDenominacion.UnidadMedida = New Clases.UnidadMedida With {.Identificador = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String))}
                            End If

                            denominacion.ValorDenominacion.Add(valorDenominacion)
                            divisa.Denominaciones.Add(denominacion)

                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Private Shared Function ObteneSaldoMedioPagoAnteriorModificar(identificadorDocumento As String)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.SaldoMedioPagoAnteriorModificacion

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each idDivisa In (From tab In dt.AsEnumerable
                                      Group tab By CodigoIsoDivisa = tab.Field(Of String)("OID_DIVISA")
                                  Into Group
                                      Order By CodigoIsoDivisa
                                      Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                    For Each dr In dt.Select(String.Format("OID_DIVISA ='{0}'", idDivisa))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))


                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorTipoMedioPago As New Clases.ValorTipoMedioPago
                            valorTipoMedioPago.TipoMedioPago = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                            valorTipoMedioPago.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorTipoMedioPago.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTipoMedioPago)
                        Else

                            Dim mediopago As Clases.MedioPago = divisa.MediosPago.Find(Function(mp) mp.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String)))

                            If mediopago Is Nothing Then
                                mediopago = New Clases.MedioPago
                                mediopago.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))
                                mediopago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                                Dim valorMedioPago As New Clases.ValorMedioPago
                                mediopago.Valores.Add(valorMedioPago)
                                divisa.MediosPago.Add(mediopago)
                            End If

                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Public Shared Function ObtenerCuadreSaldoAtualXAnterior(identificadorDocumento As String, identificadorCuenta As String, cuenta As Clases.Cuenta) As ObservableCollection(Of Clases.Divisa)

            Dim tfEfectivos As Task
            Dim tfMediosPago As Task

            Dim divisasCuadreSaldoEfectivoAnteriorXAtual As ObservableCollection(Of Clases.Divisa) = Nothing
            Dim divisasCuadreMediosPagoAnteriorXAtual As ObservableCollection(Of Clases.Divisa) = Nothing

            tfEfectivos = New Task(Sub()
                                       divisasCuadreSaldoEfectivoAnteriorXAtual = ObtenerCuadreSaldoEfectivoAnteriorXAtual(identificadorDocumento, identificadorCuenta, cuenta)
                                   End Sub)

            tfMediosPago = New Task(Sub()
                                        divisasCuadreMediosPagoAnteriorXAtual = ObtenerCuadreSaldoMedioPagoAnteriorXAtual(identificadorDocumento, identificadorCuenta, cuenta)
                                    End Sub)

            tfEfectivos.Start()
            tfMediosPago.Start()

            Task.WaitAll(New Task() {tfEfectivos, tfMediosPago})

            Dim divisas As New ObservableCollection(Of Clases.Divisa)

            If divisasCuadreSaldoEfectivoAnteriorXAtual IsNot Nothing AndAlso divisasCuadreSaldoEfectivoAnteriorXAtual.Count > 0 Then
                divisas.AddRange(divisasCuadreSaldoEfectivoAnteriorXAtual)
            End If

            If divisasCuadreMediosPagoAnteriorXAtual IsNot Nothing AndAlso divisasCuadreMediosPagoAnteriorXAtual.Count > 0 Then
                divisas.AddRange(divisasCuadreMediosPagoAnteriorXAtual)
            End If

            Return divisas

        End Function

        Public Shared Function validarSaldoEfectivoAnteriorXAtual(identificadorDocumento As String, identificadorCuentaSaldos As String) As String

            Dim respuesta As String = ""

            If Not String.IsNullOrEmpty(identificadorDocumento) AndAlso Not String.IsNullOrEmpty(identificadorCuentaSaldos) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorCuentaSaldos))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValidarSaldoEfectivoAnteriorXAtual)
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each _row In dt.Rows
                                If _row.Table.Columns.Contains("BOL_DIFERENCIA") AndAlso Util.AtribuirValorObj(_row("BOL_DIFERENCIA"), GetType(Boolean)) Then
                                    If Not respuesta.Contains(Util.AtribuirValorObj(_row("DES_DIVISA"), GetType(String))) Then
                                        If Not String.IsNullOrEmpty(respuesta) Then
                                            respuesta &= ", "
                                        End If
                                        respuesta &= Util.AtribuirValorObj(_row("DES_DIVISA"), GetType(String))
                                    End If

                                End If
                            Next
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            End If

            Return respuesta

        End Function

        Public Shared Function validarSaldoMedioPagoAnteriorXAtual(identificadorDocumento As String, identificadorCuentaSaldos As String) As String

            Dim respuesta As String = ""

            If Not String.IsNullOrEmpty(identificadorDocumento) AndAlso Not String.IsNullOrEmpty(identificadorCuentaSaldos) Then

                Try
                    Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorCuentaSaldos))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

                        command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ValidarSaldoMedioPagoAnteriorXAtual)
                        command.CommandType = CommandType.Text

                        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                        If dt IsNot Nothing AndAlso dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each _row In dt.Rows
                                If _row.Table.Columns.Contains("BOL_DIFERENCIA") AndAlso Util.AtribuirValorObj(_row("BOL_DIFERENCIA"), GetType(Boolean)) Then

                                    Dim aux As String = ""
                                    Select Case Util.AtribuirValorObj(_row("COD_TIPO_MEDIO_PAGO"), GetType(String))
                                        Case "codtipob"
                                            aux &= "Cheque/"
                                        Case "codtipo"
                                            aux &= "OtroValor/"
                                        Case "codtipoc"
                                            aux &= "Tarjeta/"
                                        Case "codtipoa"
                                            aux &= "Ticket/"
                                    End Select
                                    aux &= Util.AtribuirValorObj(_row("DES_DIVISA"), GetType(String))

                                    If Not respuesta.Contains(aux) Then
                                        If Not String.IsNullOrEmpty(respuesta) Then
                                            respuesta &= ", "
                                        End If

                                        respuesta &= aux
                                    End If

                                End If
                            Next
                        End If

                    End Using

                Catch ex As Exception
                    Throw
                Finally
                    GC.Collect()
                End Try

            End If

            Return respuesta

        End Function

        Private Shared Function ObtenerCuadreSaldoEfectivoAnteriorXAtual(identificadorDocumento As String, identificadorCuenta As String, cuenta As Clases.Cuenta) As ObservableCollection(Of Clases.Divisa)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.CuadreSaldoEfectivoAnteriorXAtual

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorCuenta))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesEfectivo = New ObservableCollection(Of Clases.ValorEfectivo)
                    divisa.Denominaciones = New ObservableCollection(Of Clases.Denominacion)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))


                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorEfectivo As New Clases.ValorEfectivo
                            valorEfectivo.Importe = Util.AtribuirValorObj(dr("SALDO_ACTUAL"), GetType(Decimal))
                            valorEfectivo.TipoDetalleEfectivo = RecuperarEnum(Of Enumeradores.TipoDetalleEfectivo)(dr("COD_TIPO_EFECTIVO_TOTAL").ToString)
                            valorEfectivo.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorEfectivo.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            valorEfectivo.Diferencia = Util.AtribuirValorObj(dr("BOL_DIFERENCIA"), GetType(Boolean))
                            divisa.ValoresTotalesEfectivo.Add(valorEfectivo)
                        Else
                            Dim denominacion As New Clases.Denominacion
                            denominacion.Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String))
                            denominacion.Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String))
                            denominacion.Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String))
                            denominacion.EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean))
                            denominacion.Valor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Decimal))

                            denominacion.ValorDenominacion = New ObservableCollection(Of Clases.ValorDenominacion)
                            Dim valorDenominacion As New Clases.ValorDenominacion
                            valorDenominacion.Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_ACTUAL"), GetType(Int64))
                            valorDenominacion.Importe = Util.AtribuirValorObj(dr("SALDO_ACTUAL"), GetType(Decimal))
                            valorDenominacion.Diferencia = Util.AtribuirValorObj(dr("BOL_DIFERENCIA"), GetType(Boolean))

                            If dr("COD_CALIDAD") IsNot DBNull.Value Then
                                valorDenominacion.Calidad = New Clases.Calidad With {.Codigo = Util.AtribuirValorObj(dr("COD_CALIDAD"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_CALIDAD"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_CALIDAD"), GetType(String))}
                            End If

                            If dr("OID_UNIDAD_MEDIDA") IsNot DBNull.Value Then
                                valorDenominacion.UnidadMedida = New Clases.UnidadMedida With {.Codigo = Util.AtribuirValorObj(dr("COD_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Descripcion = Util.AtribuirValorObj(dr("DES_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .Identificador = Util.AtribuirValorObj(dr("OID_UNIDAD_MEDIDA"), GetType(String)),
                                                                                .EsPadron = Util.AtribuirValorObj(dr("BOL_DEFECTO"), GetType(Boolean)),
                                                                                 .ValorUnidad = Util.AtribuirValorObj(dr("NUM_VALOR_UNIDAD"), GetType(Decimal)),
                                                                                 .TipoUnidadMedida = RecuperarEnum(Of Enumeradores.TipoUnidadMedida)(Util.AtribuirValorObj(dr("COD_TIPO_UNIDAD_MEDIDA"), GetType(Boolean)))}
                            End If
                            denominacion.ValorDenominacion.Add(valorDenominacion)
                            divisa.Denominaciones.Add(denominacion)
                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

        Private Shared Function ObtenerCuadreSaldoMedioPagoAnteriorXAtual(identificadorDocumento As String, identificadorCuenta As String, cuenta As Clases.Cuenta)

            Dim divisas As ObservableCollection(Of Clases.Divisa) = Nothing

            Dim _command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            _command.CommandText = My.Resources.CuadreSaldoMedioPagoAnteriorXAtual

            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CUENTA_SALDO", ProsegurDbType.Identificador_Alfanumerico, identificadorCuenta))
            _command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_DOCUMENTO", ProsegurDbType.Identificador_Alfanumerico, identificadorDocumento))

            _command.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, _command.CommandText)
            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, _command)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                divisas = New ObservableCollection(Of Clases.Divisa)

                For Each codigoIso In (From tab In dt.AsEnumerable
                                       Group tab By CodigoIsoDivisa = tab.Field(Of String)("COD_ISO_DIVISA")
                                   Into Group
                                       Order By CodigoIsoDivisa
                                       Select CodigoIsoDivisa)

                    Dim divisa As New Clases.Divisa
                    divisa.ValoresTotalesTipoMedioPago = New ObservableCollection(Of Clases.ValorTipoMedioPago)
                    divisa.MediosPago = New ObservableCollection(Of Clases.MedioPago)

                    For Each dr In dt.Select(String.Format("COD_ISO_DIVISA ='{0}'", codigoIso))
                        divisa.Identificador = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                        divisa.CodigoISO = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                        divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))

                        If dr("COD_NIVEL_DETALLE") = "T" Then
                            Dim valorTipoMedioPago As New Clases.ValorTipoMedioPago
                            valorTipoMedioPago.Importe = Util.AtribuirValorObj(dr("SALDO_ACTUAL"), GetType(Decimal))
                            valorTipoMedioPago.TipoMedioPago = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)
                            valorTipoMedioPago.TipoValor = Enumeradores.TipoValor.NoDefinido
                            valorTipoMedioPago.InformadoPor = Enumeradores.TipoContado.NoDefinido
                            valorTipoMedioPago.Diferencia = Util.AtribuirValorObj(dr("BOL_DIFERENCIA"), GetType(Boolean))
                            divisa.ValoresTotalesTipoMedioPago.Add(valorTipoMedioPago)
                        Else
                            Dim mediopago As New Clases.MedioPago

                            mediopago.Identificador = Util.AtribuirValorObj(dr("OID_MEDIO_PAGO"), GetType(String))
                            mediopago.Codigo = Util.AtribuirValorObj(dr("COD_MEDIO_PAGO"), GetType(String))
                            mediopago.Descripcion = Util.AtribuirValorObj(dr("DES_MEDIO_PAGO"), GetType(String))
                            mediopago.Tipo = RecuperarEnum(Of Enumeradores.TipoMedioPago)(dr("COD_TIPO_MEDIO_PAGO").ToString)

                            mediopago.Valores = New ObservableCollection(Of Clases.ValorMedioPago)
                            Dim valorMedioPago As New Clases.ValorMedioPago
                            valorMedioPago.Cantidad = Util.AtribuirValorObj(dr("CANTIDAD_ACTUAL"), GetType(Int64))
                            valorMedioPago.Importe = Util.AtribuirValorObj(dr("SALDO_ACTUAL"), GetType(Decimal))
                            valorMedioPago.Diferencia = Util.AtribuirValorObj(dr("BOL_DIFERENCIA"), GetType(Boolean))
                            mediopago.Valores.Add(valorMedioPago)

                            divisa.MediosPago.Add(mediopago)
                        End If

                    Next

                    divisas.Add(divisa)

                Next

            End If

            Return divisas

        End Function

#End Region

#Region "Dasboard"

        Public Shared Function RetornaSaldoDisponivelEfetivo(codigoDelegacao As List(Of String), identificadorDivisa As List(Of String), disponible As Boolean, codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.SaldoDisponivelEfetivo
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND CUEN.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "CUEN")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "SAEF")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "CUEN")
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, disponible))

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaSaldoDisponivelMedioPago(codigoDelegacao As List(Of String), identificadorDivisa As List(Of String), disponible As Boolean) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.SaldoDisponivelMedioPago
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND CUEN.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "CUEN")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "SAMP")
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "BOL_DISPONIBLE", ProsegurDbType.Logico, disponible))

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaSaldosCliente(identificadorCliente As List(Of String), identificadorDivisa As List(Of String), codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.SaldosCliente
            comando.CommandType = CommandType.Text

            If (identificadorCliente IsNot Nothing AndAlso identificadorCliente.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorCliente, "OID_CLIENTE", comando, "AND", "CUEN")
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "SAEF")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "CUEN")
            End If

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaBilletajexSector(codigoSector As List(Of String), identificadorDivisa As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.SaldoBilletajexSector
            comando.CommandType = CommandType.Text

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "CUEN")
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "SAEF")
            End If

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

        Public Shared Function RetornaRankingClientes(codigoDelegacao As List(Of String), identificadorDivisa As List(Of String), codigoSector As List(Of String)) As DataTable
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim query As String = ""

            comando.CommandText = My.Resources.SaldoRankingClientes
            comando.CommandType = CommandType.Text

            If (codigoDelegacao IsNot Nothing AndAlso codigoDelegacao.Count > 0) Then
                If (codigoDelegacao.Count = 1) Then
                    query = " AND DELE.COD_DELEGACION = []COD_DELEGACION"
                    comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacao(0)))
                Else
                    query = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoDelegacao, "COD_DELEGACION", comando, "AND", "DELE")
                End If
            End If

            If (identificadorDivisa IsNot Nothing AndAlso identificadorDivisa.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, identificadorDivisa, "OID_DIVISA", comando, "AND", "S_EFE")
            End If

            If (codigoSector IsNot Nothing AndAlso codigoSector.Count > 0) Then
                query &= Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigoSector, "COD_SECTOR", comando, "AND", "SECT")
            End If

            comando.CommandText = String.Format(comando.CommandText, query)
            comando.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, comando.CommandText)

            Dim dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, comando)
            Return dt
        End Function

#End Region

    End Class

End Namespace
