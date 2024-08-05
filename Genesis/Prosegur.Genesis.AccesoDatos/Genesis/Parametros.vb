Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Threading.Tasks
Imports Prosegur.Genesis.DataBaseHelper

Namespace Genesis

    Public Class Parametros

        Public Shared Function ObtenerParametrosDelegacionPais(codigoDelegacion As String, CodAplicacion As String, parametros As List(Of String), Optional IdentificadorAjeno As String = "") As List(Of Clases.Parametro)

            If Not String.IsNullOrEmpty(IdentificadorAjeno) Then
                codigoDelegacion = AccesoDatos.Genesis.Delegacion.ObtenerCodigoDelegacionPorCodigoAjeno(codigoDelegacion, IdentificadorAjeno)
            End If

            If Not String.IsNullOrEmpty(codigoDelegacion) Then
                Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = My.Resources.ObtenerParametrosDelegacionPais.ToString
                cmd.CommandType = CommandType.Text

                Dim query As String = ""
                If parametros IsNot Nothing AndAlso parametros.Count > 0 Then

                    query = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, parametros, "COD_PARAMETRO", cmd, "", "P")

                End If

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion))
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query))

                Return cargarParametros(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd))

            End If

            Return Nothing
        End Function

        Public Shared Function ObtenerParametrosDelegacionPais(codigoDelegacion As String, CodAplicacion As String, parametros As List(Of String), ByRef transaccion As DataBaseHelper.Transaccion) As List(Of Clases.Parametro)

            If Not String.IsNullOrEmpty(codigoDelegacion) Then
                Dim query As String = String.Empty
                If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                    query = " COD_PARAMETRO IN ('" & String.Join("','", parametros.ToArray()) & "') "
                End If

                Dim sql = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerParametrosDelegacionPais, query))

                Dim sp As New SPWrapper(sql, False, CommandType.Text)
                sp.AgregarParam("COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion)
                sp.AgregarParam("COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion)

                Dim ds As DataSet = AccesoDB.EjecutarSP(sp, Constantes.CONEXAO_GENESIS, False, transaccion)

                If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                    Return cargarParametros(ds.Tables(0))
                End If

            End If

            Return Nothing
        End Function

        Private Shared Function cargarParametros(dt As DataTable) As List(Of Clases.Parametro)

            Dim parametros As List(Of Clases.Parametro) = Nothing

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                parametros = New List(Of Clases.Parametro)

                For Each dr As DataRow In dt.Rows

                    parametros.Add(New Clases.Parametro With { _
                                     .codigoParametro = Util.AtribuirValorObj(dr("COD_PARAMETRO"), GetType(String)), _
                                     .valorParametro = Util.AtribuirValorObj(dr("DES_VALOR_PARAMETRO"), GetType(String)), _
                                     .esObligatorio = Util.AtribuirValorObj(dr("BOL_OBLIGATORIO"), GetType(Boolean))})

                Next

            End If

            Return parametros
        End Function






        Public Shared Function obtenerParametros_v2(peticion As ContractoServicio.Contractos.Comon.Parametro.obtenerParametros.Peticion) As List(Of Clases.Parametro)

            Dim parametros As List(Of Clases.Parametro) = Nothing

            Dim parametrosEspecificos As List(Of Clases.Parametro) = Nothing
            Dim parametrosAplicacion As List(Of Clases.Parametro) = Nothing
            Dim parametrosGenesis As List(Of Clases.Parametro) = Nothing
            Dim parametrosReportes As List(Of Clases.Parametro) = Nothing

            Try

                If peticion.codigosParametro IsNot Nothing AndAlso peticion.codigosParametro.Count > 0 Then

                    ' Este flujo es para obtener un o más parametros especificos

                    parametrosEspecificos = obtenerparametrosEspecificos_v2(peticion.codigoAplicacion,
                                                                            peticion.codigoDelegacion,
                                                                            peticion.codigosParametro,
                                                                            peticion.codigoHostPuesto,
                                                                            peticion.codigoPuesto,
                                                                            peticion.codigoPais)

                Else

                    Dim T1 As New Task(Sub()
                                           ' Este flujo es para obtener todos los parametros de una aplicación solicitada
                                           parametrosAplicacion = obtenerParametrosPuestoDelegacionPais_v2(peticion.codigoAplicacion,
                                                                                                           peticion.codigoDelegacion,
                                                                                                           peticion.codigoHostPuesto,
                                                                                                           peticion.codigoPuesto,
                                                                                                           peticion.codigoPais)
                                       End Sub)
                    T1.Start()

                    Dim T2 As New Task(Sub()
                                           If peticion.obtenerParametrosGenesis AndAlso Not String.IsNullOrEmpty(peticion.codigoAplicacion) Then
                                               ' Este flujo es para obtener todos los parametros Globales - Aplicación Genesis
                                               parametrosGenesis = obtenerParametrosPuestoDelegacionPais_v2(Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS,
                                                                                                            peticion.codigoDelegacion,
                                                                                                            peticion.codigoHostPuesto,
                                                                                                            peticion.codigoPuesto,
                                                                                                            peticion.codigoPais)
                                           End If
                                       End Sub)
                    T2.Start()

                    Dim T3 As New Task(Sub()
                                           If peticion.obtenerParametrosReportes AndAlso Not String.IsNullOrEmpty(peticion.codigoAplicacion) Then
                                               ' Este flujo es para obtener todos los parametros Globales - Aplicación Reportes
                                               parametrosReportes = obtenerParametrosPuestoDelegacionPais_v2(Prosegur.Genesis.Comon.Constantes.CODIGO_APLICACION_GENESIS_REPORTES,
                                                                                                             peticion.codigoDelegacion,
                                                                                                             peticion.codigoHostPuesto,
                                                                                                             peticion.codigoPuesto,
                                                                                                             peticion.codigoPais)
                                           End If
                                       End Sub)
                    T3.Start()

                    Task.WaitAll(New Task() {T1, T2, T3})

                End If

                If parametrosEspecificos IsNot Nothing AndAlso parametrosEspecificos.Count > 0 Then
                    If parametros Is Nothing Then parametros = New List(Of Clases.Parametro)
                    For Each _parametro In parametrosEspecificos
                        If parametros.FirstOrDefault(Function(x) x.codigoParametro = _parametro.codigoParametro AndAlso x.nivelParametro = _parametro.nivelParametro AndAlso x.codigoAplicacion = _parametro.codigoAplicacion) Is Nothing Then
                            parametros.Add(_parametro)
                        End If
                    Next
                End If

                If parametrosAplicacion IsNot Nothing AndAlso parametrosAplicacion.Count > 0 Then
                    If parametros Is Nothing Then parametros = New List(Of Clases.Parametro)
                    For Each _parametro In parametrosAplicacion
                        If parametros.FirstOrDefault(Function(x) x.codigoParametro = _parametro.codigoParametro AndAlso x.nivelParametro = _parametro.nivelParametro AndAlso x.codigoAplicacion = _parametro.codigoAplicacion) Is Nothing Then
                            parametros.Add(_parametro)
                        End If
                    Next
                End If

                If parametrosGenesis IsNot Nothing AndAlso parametrosGenesis.Count > 0 Then
                    If parametros Is Nothing Then parametros = New List(Of Clases.Parametro)
                    For Each _parametro In parametrosGenesis
                        If parametros.FirstOrDefault(Function(x) x.codigoParametro = _parametro.codigoParametro AndAlso x.nivelParametro = _parametro.nivelParametro AndAlso x.codigoAplicacion = _parametro.codigoAplicacion) Is Nothing Then
                            parametros.Add(_parametro)
                        End If
                    Next
                End If

                If parametrosReportes IsNot Nothing AndAlso parametrosReportes.Count > 0 Then
                    If parametros Is Nothing Then parametros = New List(Of Clases.Parametro)
                    For Each _parametro In parametrosReportes
                        If parametros.FirstOrDefault(Function(x) x.codigoParametro = _parametro.codigoParametro AndAlso x.nivelParametro = _parametro.nivelParametro AndAlso x.codigoAplicacion = _parametro.codigoAplicacion) Is Nothing Then
                            parametros.Add(_parametro)
                        End If
                    Next
                End If

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return parametros
        End Function

        Public Shared Function obtenerParametrosPuestoDelegacionPais_v2(codigoAplicacion As String,
                                                                        codigoDelegacion As String,
                                                                        codigoHostPuesto As String,
                                                                        codigoPuesto As String,
                                                               Optional codigoPais As String = "") As List(Of Clases.Parametro)

            Dim parametros As List(Of Clases.Parametro) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtroPais As String = "" '{0}
                    Dim filtroNivilPuesto As String = "" '{1}
                    Dim filtroNivilDelegacion As String = "" '{2}
                    Dim filtroNivilPais As String = "" '{3}

                    Dim queryPuesto As String = ""
                    Dim queryDelegacionPais As String = ""

                    If Not String.IsNullOrEmpty(codigoPais) Then
                        filtroPais = "[]COD_PAIS"
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, codigoPais))
                    Else
                        filtroPais = "(SELECT D.COD_PAIS FROM GEPR_TDELEGACION D WHERE D.COD_DELEGACION = []COD_DELEGACION)"
                    End If

                    If Not String.IsNullOrEmpty(codigoAplicacion) Then
                        filtroNivilDelegacion &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        filtroNivilPais &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        filtroNivilPuesto &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

                    If Not String.IsNullOrEmpty(codigoPuesto) AndAlso Not String.IsNullOrEmpty(codigoHostPuesto) Then
                        queryPuesto = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerParametrosPuesto_v2, filtroNivilPuesto))

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoHostPuesto))

                        queryPuesto &= vbNewLine & " UNION " & vbNewLine

                    End If

                    queryDelegacionPais = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerParametrosDelegacionPais_v2, filtroPais, filtroNivilDelegacion, filtroNivilPais))

                    command.CommandText = queryPuesto & queryDelegacionPais
                    command.CommandType = CommandType.Text

                    parametros = cargarParametros_V2(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command))

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return parametros

        End Function

        Private Shared Function obtenerparametrosEspecificos_v2(codigoAplicacion As String,
                                                                codigoDelegacion As String,
                                                                codigosParametro As List(Of String),
                                                       Optional codigoHostPuesto As String = "",
                                                       Optional codigoPuesto As String = "",
                                                       Optional codigoPais As String = "") As List(Of Clases.Parametro)

            Dim parametros As List(Of Clases.Parametro) = Nothing

            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                    Dim filtroParametros As String = Util.MontarClausulaIn(Constantes.CONEXAO_GENESIS, codigosParametro, "COD_PARAMETRO", command, "AND", "P")
                    Dim filtroPais As String = "" '{0}
                    Dim filtroNivilPuesto As String = filtroParametros '{1}
                    Dim filtroNivilDelegacion As String = filtroParametros '{2}
                    Dim filtroNivilPais As String = filtroParametros '{3}

                    Dim queryPuesto As String = ""
                    Dim queryDelegacionPais As String = ""

                    If Not String.IsNullOrEmpty(codigoPais) Then
                        filtroPais = "[]COD_PAIS"
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PAIS", ProsegurDbType.Identificador_Alfanumerico, codigoPais))
                    Else
                        filtroPais = "(SELECT D.COD_PAIS FROM GEPR_TDELEGACION D WHERE D.COD_DELEGACION = []COD_DELEGACION)"
                    End If

                    If Not String.IsNullOrEmpty(codigoAplicacion) Then
                        filtroNivilDelegacion &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        filtroNivilPais &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        filtroNivilPuesto &= " AND AP.COD_APLICACION = []COD_APLICACION "
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
                    End If

                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))

                    If Not String.IsNullOrEmpty(codigoPuesto) AndAlso Not String.IsNullOrEmpty(codigoHostPuesto) Then
                        queryPuesto = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerParametrosPuesto_v2, filtroNivilPuesto))

                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
                        command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoHostPuesto))

                        queryPuesto &= vbNewLine & " UNION " & vbNewLine

                    End If

                    queryDelegacionPais = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(My.Resources.ObtenerParametrosDelegacionPais_v2, filtroPais, filtroNivilDelegacion, filtroNivilPais))

                    command.CommandText = queryPuesto & queryDelegacionPais
                    command.CommandType = CommandType.Text

                    parametros = cargarParametros_V2(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command))

                End Using

            Catch ex As Exception
                Throw
            Finally
                GC.Collect()
            End Try

            Return parametros

        End Function

        Public Shared Function GetParametros(codigoAplicacion As String, codigoParametro As String) As List(Of ContractoServicio.Parametro)
            Dim parametros As List(Of ContractoServicio.Parametro) = Nothing
            Try
                Using command As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    Dim query As Text.StringBuilder = New Text.StringBuilder()
                    Dim queryFinal As String = String.Empty


                    query.AppendLine("select  parametro.oid_parametro, aplicacion.oid_aplicacion, aplicacion.cod_aplicacion, parametro_valor.oid_parametro_valor, parametro_valor.des_valor_parametro")
                    query.AppendLine("from    gepr_tparametro parametro")
                    query.AppendLine("inner join gepr_tparametro_valor parametro_valor on parametro.oid_parametro = parametro_valor.oid_parametro")
                    query.AppendLine("inner join gepr_taplicacion aplicacion on aplicacion.oid_aplicacion =   parametro.oid_aplicacion")
                    query.AppendLine("where")
                    query.AppendLine("parametro.cod_parametro = []COD_PARAMETRO")
                    query.AppendLine(" and     aplicacion.cod_aplicacion = []COD_APLICACION")
                    query.AppendLine(" order by parametro_valor.fyh_actualizacion desc ")

                    queryFinal = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, query.ToString())
                    command.CommandText = queryFinal
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))
                    command.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))

                    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, command)

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        parametros = New List(Of ContractoServicio.Parametro)
                        Dim unParametro As New ContractoServicio.Parametro
                        unParametro.Codigo = codigoParametro

                        unParametro.MultiValue = (dt.Rows.Count > 1)
                        unParametro.Valores = New List(Of String)

                        For Each fila As DataRow In dt.Rows
                            unParametro.Valores.Add(Util.AtribuirValorObj(fila("des_valor_parametro"), GetType(String)))
                        Next
                        parametros.Add(unParametro)
                    End If

                End Using
            Catch ex As Exception
                Throw ex
            Finally
                GC.Collect()
            End Try

            Return parametros
        End Function

        Private Shared Function cargarParametros_V2(dtParametros As DataTable) As List(Of Clases.Parametro)

            Dim parametros As List(Of Clases.Parametro) = Nothing

            If dtParametros IsNot Nothing AndAlso dtParametros.Rows.Count > 0 Then

                parametros = New List(Of Clases.Parametro)

                For Each dr As DataRow In dtParametros.Rows
                    Dim parametro As New Clases.Parametro

                    With parametro
                        .nivelParametro = If(dr.Table.Columns.Contains("NIVEL"), Util.AtribuirValorObj(dr("NIVEL"), GetType(Integer)), 1)
                        .codigoPais = If(dr.Table.Columns.Contains("COD_PAIS"), Util.AtribuirValorObj(dr("COD_PAIS"), GetType(String)), "")
                        .codigoPuesto = If(dr.Table.Columns.Contains("COD_PUESTO"), Util.AtribuirValorObj(dr("COD_PUESTO"), GetType(String)), "")
                        .codigoHostPuesto = If(dr.Table.Columns.Contains("COD_HOST_PUESTO"), Util.AtribuirValorObj(dr("COD_HOST_PUESTO"), GetType(String)), "")
                        .codigoDelegacion = If(dr.Table.Columns.Contains("COD_DELEGACION"), Util.AtribuirValorObj(dr("COD_DELEGACION"), GetType(String)), "")
                        .codigoAplicacion = If(dr.Table.Columns.Contains("COD_APLICACION"), Util.AtribuirValorObj(dr("COD_APLICACION"), GetType(String)), "")
                        .codigoParametro = If(dr.Table.Columns.Contains("COD_PARAMETRO"), Util.AtribuirValorObj(dr("COD_PARAMETRO"), GetType(String)), "")
                        .esObligatorio = If(dr.Table.Columns.Contains("BOL_OBLIGATORIO"), Util.AtribuirValorObj(dr("BOL_OBLIGATORIO"), GetType(Boolean)), False)
                        .tipoParametro = If(dr.Table.Columns.Contains("NEC_TIPO_COMPONENTE"), Util.AtribuirValorObj(dr("NEC_TIPO_COMPONENTE"), GetType(Integer)), 1)
                        .valorParametro = If(dr.Table.Columns.Contains("DES_VALOR_PARAMETRO"), Util.AtribuirValorObj(dr("DES_VALOR_PARAMETRO"), GetType(String)), "")
                    End With
                    parametros.Add(parametro)
                Next

            End If

            Return parametros
        End Function


    End Class

End Namespace

