Imports Prosegur.DbHelper
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.ContractoServicio.Contractos.Integracion
Imports System.Text
Imports Prosegur.Genesis.DataBaseHelper
Imports Prosegur.Genesis.DataBaseHelper.ParamWrapper
Imports Prosegur.Framework.Dicionario

Namespace Genesis
    Public Class Pais

#Region "CONSULTAS"

        ''' <summary>
        ''' Recuepra o pais pelo código da delegação
        ''' </summary>
        ''' <param name="codigoDelegacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPaisPorDelegacion(codigoDelegacion As String, Optional identificadorAjeno As String = "") As Clases.Pais

            Dim pais As Clases.Pais = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            If String.IsNullOrEmpty(identificadorAjeno) Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PaisObtenerPorDelegacion)
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PaisObtenerPorDelegacionPorCodigoAjeno)
            End If
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Descricao_Curta, codigoDelegacion))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                pais = New Clases.Pais

                With pais
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_PAIS"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_PAIS"), GetType(String))
                    .EsActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                End With

            End If

            Return pais
        End Function

        Public Shared Function ObtenerPaises() As ContractoServicio.Login.ObtenerPaises.PaisColeccion
            Dim paises As New ContractoServicio.Login.ObtenerPaises.PaisColeccion
            Dim pais As ContractoServicio.Login.ObtenerPaises.Pais
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ObtenerPaises)
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each fila As DataRow In dt.Rows
                    pais = New ContractoServicio.Login.ObtenerPaises.Pais()
                    With pais
                        .Identificador = Util.AtribuirValorObj(fila("OID_PAIS"), GetType(String))
                        .CodigoPais = Util.AtribuirValorObj(fila("COD_PAIS"), GetType(String))
                        .DescripcionPais = Util.AtribuirValorObj(fila("DES_PAIS"), GetType(String))
                        .EsActivo = Util.AtribuirValorObj(fila("BOL_ACTIVO"), GetType(Boolean))
                    End With

                    paises.Add(pais)
                Next
            End If
            Return paises
        End Function

        Public Shared Function ObtenerPaisPorCodigoAjeno(codigoPais As String, identificadorAjeno As String) As Clases.Pais
            Dim pais As Clases.Pais = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PaisObtenerAjeno)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_AJENO", ProsegurDbType.Identificador_Alfanumerico, codigoPais))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_IDENTIFICADOR", ProsegurDbType.Descricao_Curta, identificadorAjeno))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                pais = New Clases.Pais

                With pais
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_PAIS"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_PAIS"), GetType(String))
                    .EsActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                End With

            End If

            Return pais
        End Function

        ''' <summary>
        ''' Recuepra o pais pelo código da delegação
        ''' </summary>
        ''' <param name="CodigoPais"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerPais(CodigoPais As String) As Clases.Pais

            Dim pais As Clases.Pais = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PaisObtener)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, CodigoPais))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                pais = New Clases.Pais

                With pais
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_PAIS"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_PAIS"), GetType(String))
                    .EsActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                End With

            End If

            Return pais
        End Function

        Public Shared Function ObtenerPaisPorDefault(CodigoPais As String) As Clases.Pais

            Dim pais As Clases.Pais = Nothing

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.PaisObtenerDefault)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, CodigoPais))

            Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                pais = New Clases.Pais

                With pais
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_PAIS"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_PAIS"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_PAIS"), GetType(String))
                    .EsActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_ACTIVO"), GetType(Boolean))
                    .FechaHoraCreacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_CREACION"), GetType(DateTime))
                    .UsuarioCreacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_CREACION"), GetType(String))
                    .FechaHoraModificacion = Util.AtribuirValorObj(dt.Rows(0)("GMT_MODIFICACION"), GetType(DateTime))
                    .UsuarioModificacion = Util.AtribuirValorObj(dt.Rows(0)("DES_USUARIO_MODIFICACION"), GetType(String))
                End With

            End If

            Return pais
        End Function

        Public Shared Sub RecuperarPaises(peticion As RecuperarPaises.Peticion,
                                               ByRef respuesta As RecuperarPaises.Respuesta,
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
        Private Shared Function ColectarPeticion(peticion As RecuperarPaises.Peticion) As SPWrapper

            Dim SP As String = String.Format("GEPR_PPAIS_{0}.srecuperar_datos", Prosegur.Genesis.Comon.Util.Version)
            Dim spw As New SPWrapper(SP, False)

            spw.AgregarParamInfo("par$info_ejecucion")
            spw.AgregarParam("par$cod_cultura", ProsegurDbType.Identificador_Alfanumerico, If(Tradutor.CulturaSistema IsNot Nothing AndAlso
                                                                    Not String.IsNullOrEmpty(Tradutor.CulturaSistema.Name),
                                                                    Tradutor.CulturaSistema.Name,
                                                                    If(Tradutor.CulturaPadrao IsNot Nothing, Tradutor.CulturaPadrao.Name, String.Empty)), , False)
            spw.AgregarParam("par$cod_usuario", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.Usuario, , False)

            If peticion.Configuracion IsNot Nothing Then
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, peticion.Configuracion.IdentificadorAjeno, , False)
            Else
                spw.AgregarParam("par$cod_identificador_ajeno", ProsegurDbType.Identificador_Alfanumerico, Nothing,  , False)
            End If

            spw.AgregarParam("par$cod_paises", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_paises", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.Paises IsNot Nothing AndAlso peticion.Paises.Count > 0 Then
                spw.Param("par$cod_paises").AgregarValorArray("")
                spw.Param("par$des_paises").AgregarValorArray("")
                For Each pais In peticion.Paises
                    If pais IsNot Nothing AndAlso Not String.IsNullOrEmpty(pais.Codigo) Then
                        spw.Param("par$cod_paises").AgregarValorArray(pais.Codigo)
                    End If
                    If pais IsNot Nothing AndAlso Not String.IsNullOrEmpty(pais.Descripcion) Then
                        spw.Param("par$des_paises").AgregarValorArray(pais.Descripcion)
                    End If
                Next
            End If

            spw.AgregarParam("par$cod_delegaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_delegaciones", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.Delegaciones IsNot Nothing AndAlso peticion.Delegaciones.Count > 0 Then
                spw.Param("par$cod_delegaciones").AgregarValorArray("")
                spw.Param("par$des_delegaciones").AgregarValorArray("")
                For Each delegacion In peticion.Delegaciones
                    If delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(delegacion.Codigo) Then
                        spw.Param("par$cod_delegaciones").AgregarValorArray(delegacion.Codigo)
                    End If
                    If delegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(delegacion.Descripcion) Then
                        spw.Param("par$des_delegaciones").AgregarValorArray(delegacion.Descripcion)
                    End If
                Next
            End If

            spw.AgregarParam("par$cod_plantas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            spw.AgregarParam("par$des_plantas", ProsegurDbType.Identificador_Alfanumerico, Nothing, , True)
            If peticion.Plantas IsNot Nothing AndAlso peticion.Plantas.Count > 0 Then
                spw.Param("par$cod_plantas").AgregarValorArray("")
                spw.Param("par$des_plantas").AgregarValorArray("")
                For Each planta In peticion.Plantas
                    If planta IsNot Nothing AndAlso Not String.IsNullOrEmpty(planta.Codigo) Then
                        spw.Param("par$cod_plantas").AgregarValorArray(planta.Codigo)
                    End If
                    If planta IsNot Nothing AndAlso Not String.IsNullOrEmpty(planta.Descripcion) Then
                        spw.Param("par$des_plantas").AgregarValorArray(planta.Descripcion)
                    End If
                Next
            End If

            If peticion.RecuperarCodigosAjenos IsNot Nothing AndAlso (peticion.RecuperarCodigosAjenos = "1" OrElse peticion.RecuperarCodigosAjenos.Trim().ToUpper = "TRUE") Then
                spw.AgregarParam("par$rec_codigos_ajenos", ProsegurDbType.Logico, 1, , False)
            Else
                spw.AgregarParam("par$rec_codigos_ajenos", ProsegurDbType.Logico, 0, , False)
            End If

            If peticion.RecuperarHistoricoCambios IsNot Nothing AndAlso (peticion.RecuperarHistoricoCambios = "1" OrElse peticion.RecuperarHistoricoCambios.Trim().ToUpper = "TRUE") Then
                spw.AgregarParam("par$rec_historico_cambios", ProsegurDbType.Logico, 1, , False)
            Else
                spw.AgregarParam("par$rec_historico_cambios", ProsegurDbType.Logico, 0, , False)
            End If

            If peticion.RecuperarDatosFacturacion IsNot Nothing AndAlso (peticion.RecuperarDatosFacturacion = "1" OrElse peticion.RecuperarDatosFacturacion.Trim().ToUpper = "TRUE") Then
                spw.AgregarParam("par$rec_datos_facturacion", ProsegurDbType.Logico, 1, , False)
            Else
                spw.AgregarParam("par$rec_datos_facturacion", ProsegurDbType.Logico, 0, , False)
            End If

            spw.AgregarParam("par$rc_paises", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "paises")
            spw.AgregarParam("par$rc_delegaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "delegaciones")
            spw.AgregarParam("par$rc_delegaciones_historico", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "delegaciones_historico")
            spw.AgregarParam("par$rc_plantas", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "plantas")
            spw.AgregarParam("par$rc_codigos_ajenos", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "codigos_ajenos")
            spw.AgregarParam("par$rc_cuentas_facturacion", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "cuentas_facturacion")
            spw.AgregarParam("par$rc_datos_bancarios", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "datos_bancarios")
            spw.AgregarParam("par$validaciones", ParamTypes.RefCursor, Nothing, ParameterDirection.Output, False, "validaciones")
            spw.AgregarParam("par$cod_ejecucion", ProsegurDbType.Identificador_Alfanumerico, Nothing, ParameterDirection.Output, False)

            spw.RegistrarEjecucionExterna(String.Format("gepr_putilidades_{0}.supd_tlog_ejecucion_trn_ex", Prosegur.Genesis.Comon.Util.Version),
                              "par$cod_ejecucion", "par$cod_ejecucion", "par$num_duracion_trans_ext_seg",
                              "par$cod_transaccion", "par$cod_resultado")

            Return spw

        End Function
        Private Shared Sub PoblarRespuesta(ds As DataSet,
                                           ByRef respuesta As RecuperarPaises.Respuesta)
            
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then

                If ds.Tables.Contains("paises") AndAlso ds.Tables("paises").Rows.Count > 0 Then
                    Dim primaryKeyPaises(ds.Tables("paises").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("paises").PrimaryKey
                        primaryKeyPaises(primaryKeyPaises.Length - 1) = _key
                    Next
                    primaryKeyPaises(primaryKeyPaises.Length - 1) = ds.Tables("paises").Columns("OID_PAIS")
                    ds.Tables("paises").PrimaryKey = primaryKeyPaises
                End If
                If ds.Tables.Contains("delegaciones") AndAlso ds.Tables("delegaciones").Rows.Count > 0 Then
                    Dim primaryKeyDelegaciones(ds.Tables("delegaciones").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("delegaciones").PrimaryKey
                        primaryKeyDelegaciones(primaryKeyDelegaciones.Length - 1) = _key
                    Next
                    primaryKeyDelegaciones(primaryKeyDelegaciones.Length - 1) = ds.Tables("delegaciones").Columns("OID_DELEGACION")
                    ds.Tables("delegaciones").PrimaryKey = primaryKeyDelegaciones
                End If
                If ds.Tables.Contains("delegaciones_historico") AndAlso ds.Tables("delegaciones_historico").Rows.Count > 0 Then
                    Dim primaryKeyDelegacionesHistorico(ds.Tables("delegaciones_historico").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("delegaciones_historico").PrimaryKey
                        primaryKeyDelegacionesHistorico(primaryKeyDelegacionesHistorico.Length - 1) = _key
                    Next
                    primaryKeyDelegacionesHistorico(primaryKeyDelegacionesHistorico.Length - 1) = ds.Tables("delegaciones_historico").Columns("OID_HIST_DELEGACION")
                    ds.Tables("delegaciones_historico").PrimaryKey = primaryKeyDelegacionesHistorico
                End If
                If ds.Tables.Contains("plantas") AndAlso ds.Tables("plantas").Rows.Count > 0 Then
                    Dim primaryKeyPlantas(ds.Tables("plantas").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("plantas").PrimaryKey
                        primaryKeyPlantas(primaryKeyPlantas.Length - 1) = _key
                    Next
                    primaryKeyPlantas(primaryKeyPlantas.Length - 1) = ds.Tables("plantas").Columns("OID_PLANTA")
                    ds.Tables("plantas").PrimaryKey = primaryKeyPlantas
                End If
                If ds.Tables.Contains("codigos_ajenos") AndAlso ds.Tables("codigos_ajenos").Rows.Count > 0 Then
                    Dim primaryKeyCodigosAjenos(ds.Tables("codigos_ajenos").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("plantas").PrimaryKey
                        primaryKeyCodigosAjenos(primaryKeyCodigosAjenos.Length - 1) = _key
                    Next
                    primaryKeyCodigosAjenos(primaryKeyCodigosAjenos.Length - 1) = ds.Tables("codigos_ajenos").Columns("OID_CODIGO_AJENO")
                    ds.Tables("codigos_ajenos").PrimaryKey = primaryKeyCodigosAjenos
                End If
                If ds.Tables.Contains("cuentas_facturacion") AndAlso ds.Tables("cuentas_facturacion").Rows.Count > 0 Then
                    Dim primaryKeyCuentasFacturacion(ds.Tables("cuentas_facturacion").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("cuentas_facturacion").PrimaryKey
                        primaryKeyCuentasFacturacion(primaryKeyCuentasFacturacion.Length - 1) = _key
                    Next
                    primaryKeyCuentasFacturacion(primaryKeyCuentasFacturacion.Length - 1) = ds.Tables("cuentas_facturacion").Columns("OID_DELEGACION")
                    ds.Tables("cuentas_facturacion").PrimaryKey = primaryKeyCuentasFacturacion
                End If
                If ds.Tables.Contains("datos_bancarios") AndAlso ds.Tables("datos_bancarios").Rows.Count > 0 Then
                    Dim primaryKeyDatosBancarios(ds.Tables("datos_bancarios").PrimaryKey.Length + 1) As DataColumn
                    For Each _key In ds.Tables("datos_bancarios").PrimaryKey
                        primaryKeyDatosBancarios(primaryKeyDatosBancarios.Length - 1) = _key
                    Next
                    primaryKeyDatosBancarios(primaryKeyDatosBancarios.Length - 1) = ds.Tables("datos_bancarios").Columns("OID_PTO_SERVICIO")
                    ds.Tables("datos_bancarios").PrimaryKey = primaryKeyDatosBancarios
                End If


                ' Paises
                If ds.Tables.Contains("paises") AndAlso ds.Tables("paises").Rows.Count > 0 Then
                    For Each rowPais As DataRow In ds.Tables("paises").Rows

                        If respuesta.Paises Is Nothing Then respuesta.Paises = New List(Of RecuperarPaises.Salida.Pais)

                        Dim pais As RecuperarPaises.Salida.Pais = respuesta.Paises.FirstOrDefault(Function(p) p.Identificador = Util.AtribuirValorObj(rowPais("OID_PAIS"), GetType(String)))
                        If pais Is Nothing Then

                            pais = PoblarPais(rowPais)

                            ' Codigos Ajenos - Pais
                            If ds.Tables.Contains("codigos_ajenos") AndAlso ds.Tables("codigos_ajenos").Rows.Count > 0 Then
                                For Each rowCodigoAjeno As DataRow In ds.Tables("codigos_ajenos").Select(String.Format("COD_TIPO_TABLA_GENESIS = 'GEPR_TPAIS' AND OID_TABLA_GENESIS = '{0}'", pais.Identificador))
                                    pais.CodigosAjenos.Add(PoblarCodigoAjeno(rowCodigoAjeno))
                                Next
                            End If

                            ' Delegaciones
                            If ds.Tables.Contains("delegaciones") AndAlso ds.Tables("delegaciones").Rows.Count > 0 Then
                                For Each rowDelegacion As DataRow In ds.Tables("delegaciones").Select(String.Format("OID_PAIS = '{0}'", pais.Identificador))

                                    Dim delegacion As RecuperarPaises.Salida.Delegacion = pais.Delegaciones.FirstOrDefault(Function(d) d.Identificador = Util.AtribuirValorObj(rowDelegacion("OID_DELEGACION"), GetType(String)))
                                    If delegacion Is Nothing Then

                                        delegacion = PoblarDelegacion(rowDelegacion)

                                        ' Codigos Ajenos - Delegacion
                                        If ds.Tables.Contains("codigos_ajenos") AndAlso ds.Tables("codigos_ajenos").Rows.Count > 0 Then
                                            For Each rowCodigoAjeno As DataRow In ds.Tables("codigos_ajenos").Select(String.Format("COD_TIPO_TABLA_GENESIS = 'GEPR_TDELEGACION' AND OID_TABLA_GENESIS = '{0}'", delegacion.Identificador))
                                                delegacion.CodigosAjenos.Add(PoblarCodigoAjeno(rowCodigoAjeno))
                                            Next
                                        End If

                                        ' Historico
                                        If ds.Tables.Contains("delegaciones_historico") AndAlso ds.Tables("delegaciones_historico").Rows.Count > 0 Then
                                            For Each rowDelegacionHistorico As DataRow In ds.Tables("delegaciones_historico").Select(String.Format("OID_DELEGACION = '{0}'", delegacion.Identificador))
                                                delegacion.Historico.Add(PoblarDelegacionHistorico(rowDelegacionHistorico))
                                            Next
                                        End If

                                        ' Cuentas Facturacion
                                        If ds.Tables.Contains("cuentas_facturacion") AndAlso ds.Tables("cuentas_facturacion").Rows.Count > 0 Then
                                            For Each rowCuentaFacturacion As DataRow In ds.Tables("cuentas_facturacion").Select(String.Format("OID_DELEGACION = '{0}'", delegacion.Identificador))

                                                Dim cuentaFacturacion As RecuperarPaises.Salida.CuentaFacturacion = delegacion.CuentasFacturacion.FirstOrDefault(Function(c) c.Identificador = Util.AtribuirValorObj(rowCuentaFacturacion("OID_DELEGACIONXCONFIG_FACTUR"), GetType(String)))
                                                If cuentaFacturacion Is Nothing Then

                                                    cuentaFacturacion = PoblarCuentaFacturacion(rowCuentaFacturacion)

                                                    ' Datos Bancarios
                                                    If ds.Tables.Contains("datos_bancarios") AndAlso ds.Tables("datos_bancarios").Rows.Count > 0 Then
                                                        For Each rowDatoBancario As DataRow In ds.Tables("datos_bancarios").Select(String.Format("OID_COMBINATORIA = '{0}#{1}#{2}' AND OID_DELEGACION = '{3}'", cuentaFacturacion.CodigoBancoCapital, cuentaFacturacion.CodigoBancoTesoreria, cuentaFacturacion.CodigoCuentaTesoreria, delegacion.Identificador))
                                                            cuentaFacturacion.DatosBancarios.Add(PoblarDatoBancario(rowDatoBancario))
                                                        Next
                                                    End If

                                                    delegacion.CuentasFacturacion.Add(cuentaFacturacion)

                                                End If

                                            Next
                                        End If

                                        ' Plantas
                                        If ds.Tables.Contains("plantas") AndAlso ds.Tables("plantas").Rows.Count > 0 Then
                                            For Each rowPlanta As DataRow In ds.Tables("plantas").Select(String.Format("OID_DELEGACION = '{0}'", delegacion.Identificador))

                                                Dim planta As RecuperarPaises.Salida.Planta = delegacion.Plantas.FirstOrDefault(Function(p) p.Identificador = Util.AtribuirValorObj(rowPlanta("OID_PLANTA"), GetType(String)))
                                                If planta Is Nothing Then

                                                    planta = PoblarPlanta(rowPlanta)

                                                    ' Codigos Ajenos - Planta
                                                    If ds.Tables.Contains("codigos_ajenos") AndAlso ds.Tables("codigos_ajenos").Rows.Count > 0 Then
                                                        For Each rowCodigoAjeno As DataRow In ds.Tables("codigos_ajenos").Select(String.Format("COD_TIPO_TABLA_GENESIS = 'GEPR_TPLANTA' AND OID_TABLA_GENESIS = '{0}'", planta.Identificador))
                                                            planta.CodigosAjenos.Add(PoblarCodigoAjeno(rowCodigoAjeno))
                                                        Next
                                                    End If

                                                    delegacion.Plantas.Add(planta)

                                                End If

                                            Next
                                        End If

                                        pais.Delegaciones.Add(delegacion)

                                    End If

                                Next
                            End If

                            respuesta.Paises.Add(pais)

                        End If

                    Next
                End If

                ' Validaciones
                If ds.Tables.Contains("validaciones") AndAlso ds.Tables("validaciones").Rows.Count > 0 Then

                    If respuesta.Resultado.Detalles Is Nothing Then respuesta.Resultado.Detalles = New List(Of RecuperarPaises.Salida.Detalle)
                    For Each row As DataRow In ds.Tables("validaciones").Rows
                        respuesta.Resultado.Detalles.Add(New RecuperarPaises.Salida.Detalle With {.Codigo = Util.AtribuirValorObj(row(0), GetType(String)), .Descripcion = Util.AtribuirValorObj(row(1), GetType(String))})
                    Next

                End If

            End If

        End Sub
        Private Shared Function PoblarPais(row As DataRow) As RecuperarPaises.Salida.Pais
            Dim pais = New RecuperarPaises.Salida.Pais

            With pais
                .Identificador = Util.AtribuirValorObj(row("OID_PAIS"), GetType(String))
                .Codigo = Util.AtribuirValorObj(row("COD_PAIS"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_PAIS"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .CodigosAjenos = New List(Of RecuperarPaises.Salida.CodigoAjeno)
                .Delegaciones = New List(Of RecuperarPaises.Salida.Delegacion)
            End With

            Return pais
        End Function
        Private Shared Function PoblarDelegacion(row As DataRow) As RecuperarPaises.Salida.Delegacion
            Dim delegacion = New RecuperarPaises.Salida.Delegacion

            With delegacion
                .Identificador = Util.AtribuirValorObj(row("OID_DELEGACION"), GetType(String))
                .Codigo = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DELEGACION"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                .GMT = Util.AtribuirValorObj(row("NEC_GMT_MINUTOS"), GetType(Integer))
                .VeranoFechaInicio = Util.AtribuirValorObj(row("FYH_VERANO_INICIO"), GetType(Date))
                .VeranoFechaFin = Util.AtribuirValorObj(row("FYH_VERANO_FIN"), GetType(Date))
                .VeranoMinutosAjuste = Util.AtribuirValorObj(row("NEC_VERANO_AJUSTE"), GetType(Integer))
                .Zona = Util.AtribuirValorObj(row("DES_ZONA"), GetType(String))
                .CodigosAjenos = New List(Of RecuperarPaises.Salida.CodigoAjeno)
                .Historico = New List(Of RecuperarPaises.Salida.DelegacionHistorico)
                .CuentasFacturacion = New List(Of RecuperarPaises.Salida.CuentaFacturacion)
                .Plantas = New List(Of RecuperarPaises.Salida.Planta)
            End With

            Return delegacion
        End Function
        Private Shared Function PoblarCodigoAjeno(row As DataRow) As RecuperarPaises.Salida.CodigoAjeno
            Dim codigoAjeno = New RecuperarPaises.Salida.CodigoAjeno

            With codigoAjeno
                .Identificador = Util.AtribuirValorObj(row("OID_CODIGO_AJENO"), GetType(String))
                .Codigo = Util.AtribuirValorObj(row("COD_AJENO"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_AJENO"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .CodigoIdentificador = Util.AtribuirValorObj(row("COD_IDENTIFICADOR"), GetType(String))
                .Patron = Util.AtribuirValorObj(row("BOL_DEFECTO"), GetType(Boolean))
            End With

            Return codigoAjeno
        End Function
        Private Shared Function PoblarDelegacionHistorico(row As DataRow) As RecuperarPaises.Salida.DelegacionHistorico
            Dim delegacionHistorico = New RecuperarPaises.Salida.DelegacionHistorico

            With delegacionHistorico
                .Identificador = Util.AtribuirValorObj(row("OID_HIST_DELEGACION"), GetType(String))
                .Codigo = Util.AtribuirValorObj(row("COD_DELEGACION"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_DELEGACION"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                .GMT = Util.AtribuirValorObj(row("NEC_GMT_MINUTOS"), GetType(Integer))
                .VeranoFechaInicio = Util.AtribuirValorObj(row("FYH_VERANO_INICIO"), GetType(Date))
                .VeranoFechaFin = Util.AtribuirValorObj(row("FYH_VERANO_FIN"), GetType(Date))
                .VeranoMinutosAjuste = Util.AtribuirValorObj(row("NEC_VERANO_AJUSTE"), GetType(Integer))
                .Zona = Util.AtribuirValorObj(row("DES_ZONA"), GetType(String))
                .FechaHora = Util.AtribuirValorObj(row("FYH_ACCION"), GetType(DateTime))
            End With

            Return delegacionHistorico
        End Function
        Private Shared Function PoblarCuentaFacturacion(row As DataRow) As RecuperarPaises.Salida.CuentaFacturacion
            Dim cuentaFacturacion = New RecuperarPaises.Salida.CuentaFacturacion

            With cuentaFacturacion
                .Identificador = Util.AtribuirValorObj(row("OID_DELEGACIONXCONFIG_FACTUR"), GetType(String))
                .CodigoBancoCapital = Util.AtribuirValorObj(row("COD_CLIENTE_CAPITAL"), GetType(String))
                .DescripcionBancoCapital = Util.AtribuirValorObj(row("DES_CLIENTE_CAPITAL"), GetType(String))
                .CodigoBancoTesoreria = Util.AtribuirValorObj(row("COD_SUBCLIENTE_TESORERIA"), GetType(String))
                .DescripcionBancoTesoreria = Util.AtribuirValorObj(row("DES_SUBCLIENTE_TESORERIA"), GetType(String))
                .CodigoCuentaTesoreria = Util.AtribuirValorObj(row("COD_PTO_SERVICIO_TESORERIA"), GetType(String))
                .DescripcionCuentaTesoreria = Util.AtribuirValorObj(row("DES_PTO_SERVICIO_TESORERIA"), GetType(String))
                .DatosBancarios = New List(Of RecuperarPaises.Salida.DatoBancario)
            End With

            Return cuentaFacturacion
        End Function
        Private Shared Function PoblarDatoBancario(row As DataRow) As RecuperarPaises.Salida.DatoBancario
            Dim datoBancario = New RecuperarPaises.Salida.DatoBancario

            With datoBancario
                .Identificador = Util.AtribuirValorObj(row("OID_DATO_BANCARIO"), GetType(String))
                .CodigoBanco = Util.AtribuirValorObj(row("COD_BANCO"), GetType(String))
                .CodigoAgencia = Util.AtribuirValorObj(row("COD_AGENCIA"), GetType(String))
                .NumeroCuenta = Util.AtribuirValorObj(row("COD_CUENTA_BANCARIA"), GetType(String))
                .Tipo = Util.AtribuirValorObj(row("COD_TIPO_CUENTA_BANCARIA"), GetType(String))
                .NumeroDocumento = Util.AtribuirValorObj(row("COD_DOCUMENTO"), GetType(String))
                .Titularidad = Util.AtribuirValorObj(row("DES_TITULARIDAD"), GetType(String))
                .CodigoDivisa = Util.AtribuirValorObj(row("COD_ISO_DIVISA"), GetType(String))
                .Observaciones = Util.AtribuirValorObj(row("DES_OBSERVACIONES"), GetType(String))
                .Patron = Util.AtribuirValorObj(row("BOL_DEFECTO"), GetType(Boolean))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .CampoAdicional1 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_1"), GetType(String))
                .CampoAdicional2 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_2"), GetType(String))
                .CampoAdicional3 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_3"), GetType(String))
                .CampoAdicional4 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_4"), GetType(String))
                .CampoAdicional5 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_5"), GetType(String))
                .CampoAdicional6 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_6"), GetType(String))
                .CampoAdicional7 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_7"), GetType(String))
                .CampoAdicional8 = Util.AtribuirValorObj(row("DES_CAMPO_ADICIONAL_8"), GetType(String))
            End With

            Return datoBancario
        End Function
        Private Shared Function PoblarPlanta(row As DataRow) As RecuperarPaises.Salida.Planta
            Dim planta = New RecuperarPaises.Salida.Planta

            With planta
                .Identificador = Util.AtribuirValorObj(row("OID_PLANTA"), GetType(String))
                .Codigo = Util.AtribuirValorObj(row("COD_PLANTA"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DES_PLANTA"), GetType(String))
                .Vigente = Util.AtribuirValorObj(row("BOL_ACTIVO"), GetType(Boolean))
                .CodigosAjenos = New List(Of RecuperarPaises.Salida.CodigoAjeno)
            End With

            Return planta
        End Function


        Private Shared Function PoblarValidacion(row As DataRow) As RecuperarPaises.Salida.Detalle
            Dim detalle = New RecuperarPaises.Salida.Detalle

            With detalle
                .Codigo = Util.AtribuirValorObj(row("CODIGO"), GetType(String))
                .Descripcion = Util.AtribuirValorObj(row("DESCRIPCION"), GetType(String))
            End With

            Return detalle
        End Function
#End Region

    End Class
End Namespace
