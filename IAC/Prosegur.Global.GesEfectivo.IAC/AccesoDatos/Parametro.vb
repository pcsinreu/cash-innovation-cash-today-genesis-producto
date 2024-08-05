Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Parametro
Imports System.Linq

Public Class Parametro

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Recupera todos os parametros da aplicação
    ''' </summary>
    ''' <param name="OidAplicacion"></param>
    ''' <param name="TipoComponente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 21/09/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarTodosParametros(OidAplicacion As String, TipoComponente As Integer) As List(Of String)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.RecuperarTodosParametros.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Identificador_Alfanumerico, OidAplicacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_TIPO_COMPONENTE", ProsegurDbType.Inteiro_Curto, TipoComponente))

        Dim listaParametros As New List(Of String)
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                listaParametros.Add(dr("COD_PARAMETRO"))
            Next

        End If

        Return listaParametros
    End Function

    ''' <summary>
    ''' Obtém os postos
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/08/2011 Criado
    ''' </history>
    Public Shared Function GetParametros(codigoAplicacion As String, codigoNivel As String, descripcionCortaAgrupacion As String, codigoParametro As String, permisos As List(Of String), Optional aplicaciones As List(Of ContractoServicio.Aplicacion) = Nothing) As ContractoServicio.Parametro.GetParametros.ParametroColeccion

        ' criar objeto Parametro coleccion
        Dim objParametros As New ContractoServicio.Parametro.GetParametros.ParametroColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetParametros.ToString)
        ' adicionar filtros
        Dim filtro As New StringBuilder
        filtro.Append(" WHERE 1=1 ")

        If Not String.IsNullOrEmpty(codigoAplicacion) Then

            If aplicaciones IsNot Nothing AndAlso aplicaciones.FirstOrDefault(Function(x) x.CodigoAplicacion = codigoAplicacion) Is Nothing Then
                Return objParametros
            End If

            filtro.Append(" AND GEPR_TAPLICACION.COD_APLICACION = []COD_APLICACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        Else
            If aplicaciones IsNot Nothing Then
                query.Append(Util.MontarClausulaIn(aplicaciones.Select(Function(A) A.CodigoAplicacion).ToList, "COD_APLICACION", comando, "AND", "GEPR_TAPLICACION", ""))
            ElseIf permisos IsNot Nothing AndAlso permisos.Count > 0 Then
                filtro.Append(Util.MontarClausulaIn(permisos, "COD_PERMISO", comando, "AND", "GEPR_TAPLICACION", ""))
            End If
        End If

        If Not String.IsNullOrEmpty(codigoNivel) Then
            filtro.Append(" AND GEPR_TNIVEL_PARAMETRO.COD_NIVEL_PARAMETRO = []COD_NIVEL_PARAMETRO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoNivel))
        End If

        If Not String.IsNullOrEmpty(descripcionCortaAgrupacion) Then
            filtro.Append(" AND upper(GEPR_TGRUPO_PARAMETRO.DES_DESCRIPCION_CORTO) LIKE []DES_DESCRIPCION_CORTO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Identificador_Alfanumerico, "%" & descripcionCortaAgrupacion.Trim().ToUpper() & "%"))
        End If

        If Not String.IsNullOrEmpty(codigoParametro) Then
            filtro.Append(" AND upper(GEPR_TPARAMETRO.COD_PARAMETRO) LIKE []COD_PARAMETRO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, "%" & codigoParametro.Trim().ToUpper() & "%"))
        End If

        query.Append(filtro)

        query.Append(" ORDER BY GEPR_TAPLICACION.COD_APLICACION ,  GEPR_TGRUPO_PARAMETRO.NEC_ORDEN , GEPR_TPARAMETRO.NEC_ORDEN ")

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objParametros.Add(PopularComboParametro(dr))
            Next
        End If
        ' retornar coleção de Nivel
        Return objParametros
    End Function

    Public Shared Function GetParametroDetail(codigoAplicacion As String, codigoParametro As String) As ContractoServicio.Parametro.GetParametroDetail.Parametro

        ' criar objeto Parametro coleccion
        Dim objParametro As New ContractoServicio.Parametro.GetParametroDetail.Parametro

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetParametroDetail.ToString)
        ' adicionar filtros
        Dim filtro As New StringBuilder
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objParametro = PopularComboParametroDetail(dr)
            Next
        End If
        ' retornar coleção de Nivel
        Return objParametro
    End Function

    Public Shared Function ListarOIDParametrosValues(codigoPais As String, oidDelegacion As String, oidPlanta As String, codigoAplicacion As String, oidPuesto As String) As List(Of ParametroValueVO)
        Dim objParametrosValues As List(Of ParametroValueVO) = Nothing

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PAIS", ProsegurDbType.Inteiro_Curto, CType(TipoNivel.Pais, Integer)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ID_PAIS", ProsegurDbType.Identificador_Alfanumerico, codigoPais))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_DELEGACION", ProsegurDbType.Inteiro_Curto, CType(TipoNivel.Delegacion, Integer)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ID_DELEGACION", ProsegurDbType.Objeto_Id, oidDelegacion))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PUESTO", ProsegurDbType.Inteiro_Curto, CType(TipoNivel.Puesto, Integer)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ID_PUESTO", ProsegurDbType.Objeto_Id, oidPuesto))

        comando.CommandText = Util.PrepararQuery(My.Resources.GetParametrosValues.ToString)
        comando.CommandType = CommandType.Text

        Dim drQuery As IDataReader = Nothing

        Try
            drQuery = AcessoDados.ExecutarDataReader(Constantes.CONEXAO_GE, comando)
        Finally
            If drQuery IsNot Nothing Then
                objParametrosValues = TransformarParaParametroValueVO(drQuery)
            End If
            drQuery.Close()
            drQuery.Dispose()
            AcessoDados.Desconectar(comando.Connection)
        End Try

        Return objParametrosValues
    End Function

    Public Shared Function GetParametroOpciones(codigoAplicacion As String, codigoParametro As String) As ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion

        ' criar objeto Parametro coleccion
        Dim objOpciones As New ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetParametroOpciones.ToString)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objOpciones.Add(PopularParametroOpcion(dr))
            Next
        End If
        ' retornar coleção de Nivel
        Return objOpciones
    End Function

    Public Shared Sub SetParametroValue(parametrosValues As List(Of ParametroValueVO), oidDelegacion As String, oidPuesto As String, codigoPais As String, codigoUsuario As String)
        Dim objTransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        For Each parametro As ParametroValueVO In parametrosValues
            If String.IsNullOrEmpty(parametro.OIDParametroValue) Then
                Select Case parametro.TipoNivel
                    Case TipoNivel.Pais
                        ParametroValue.AltaParametroValue(parametro.OIDParametro, codigoPais, parametro.Valor, codigoUsuario, objTransacion)
                        Exit Select
                    Case TipoNivel.Delegacion
                        ParametroValue.AltaParametroValue(parametro.OIDParametro, oidDelegacion, parametro.Valor, codigoUsuario, objTransacion)
                        Exit Select
                    Case TipoNivel.Puesto
                        ParametroValue.AltaParametroValue(parametro.OIDParametro, oidPuesto, parametro.Valor, codigoUsuario, objTransacion)
                        Exit Select
                End Select
            Else
                ParametroValue.ActualizarParametroValue(parametro.OIDParametroValue, parametro.Valor, codigoUsuario, objTransacion)
            End If
        Next
        objTransacion.RealizarTransacao()
    End Sub

    Public Shared Sub ActualizarParametro(oidAplicacion As String, oidAgrupacion As String, codigoParametro As String, descripcionCortoParametro As String, descripcionLargaParametro As String, necOrgem As Nullable(Of Integer), codigoUsuario As String)
        Try
            'criar(transação)
            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            Dim query As New StringBuilder()
            query.Append("UPDATE GEPR_TPARAMETRO")
            query.Append(" SET ")

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            Dim oidPuesto As String = Guid.NewGuid.ToString
            If Not String.IsNullOrEmpty(descripcionCortoParametro) Then
                query.Append("DES_DESCRIPCION_CORTO =  []DES_DESCRIPCION_CORTO,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Descricao_Curta, descripcionCortoParametro))
            End If

            If Not String.IsNullOrEmpty(descripcionLargaParametro) Then
                query.Append("DES_DESCRIPCION_LARGA =  []DES_DESCRIPCION_LARGA,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_LARGA", ProsegurDbType.Descricao_Longa, descripcionLargaParametro))
            End If

            If Not String.IsNullOrEmpty(oidAgrupacion) Then
                query.Append("OID_GRUPO_PARAMETRO =  []OID_GRUPO_PARAMETRO,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_PARAMETRO", ProsegurDbType.Objeto_Id, oidAgrupacion))
            End If

            If necOrgem.HasValue Then
                query.Append("NEC_ORDEN =  []NEC_ORDEN,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Descricao_Longa, necOrgem.Value))
            End If

            query.Append(" COD_USUARIO =  []COD_USUARIO, FYH_ACTUALIZACION =  []FYH_ACTUALIZACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            query.Append(" WHERE COD_PARAMETRO = []COD_PARAMETRO AND OID_APLICACION =  []OID_APLICACION")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Objeto_Id, oidAplicacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))


            comando.CommandText = Util.PrepararQuery(query.ToString())
            comando.CommandType = CommandType.Text
            ' adicionar item para transação
            objtransacion.AdicionarItemTransacao(comando)

            ' realiza a transação
            objtransacion.RealizarTransacao()
        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("030_msg_Erro_UKParametro"))
        End Try
    End Sub

    ''' <summary>
    ''' Retorna o oid e o nivel do parametro
    ''' </summary>
    ''' <param name="CodParametro"></param>
    ''' <param name="oidAplicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function RecuperarOidYNivelParametro(CodParametro As String, oidAplicacion As String) As Dictionary(Of String, String)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.RecuperarOidYNivelParametro.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, CodParametro.ToUpper))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Objeto_Id, oidAplicacion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        Dim objOidYNivelParametro As Dictionary(Of String, String) = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objOidYNivelParametro = New Dictionary(Of String, String)

            objOidYNivelParametro.Add(Util.AtribuirValorObj(dt.Rows(0)("OID_PARAMETRO"), GetType(String)), Util.AtribuirValorObj(dt.Rows(0)("COD_NIVEL_PARAMETRO"), GetType(String)))

        End If

        Return objOidYNivelParametro
    End Function

    ''' <summary>
    ''' Recupera os Parametros do Posto de acordo com os filtros aplicados.
    ''' </summary>
    ''' <param name="codigoPuesto"></param>
    ''' <param name="codigoHostPuesto"></param>
    ''' <param name="codigoAplicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RecuperarParametros(codigoPuesto As String, codigoHostPuesto As String, codigoAplicacion As String, CodigoDelegacion As String) As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto

        ' criar objeto DatosPuesto coleccion
        Dim objDatosPuesto As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        If String.IsNullOrEmpty(CodigoDelegacion) Then
            CodigoDelegacion = RecuperarCodDelegacion(codigoAplicacion, codigoPuesto, codigoHostPuesto)
        End If

        query.Append(String.Format(My.Resources.GetParametrosAplicacionPuesto.ToString, " UPPER(DEL.COD_DELEGACION) = UPPER([]COD_DELEGACION) and APL.COD_APLICACION = []COD_APLICACION"))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoHostPuesto))

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing AndAlso dtQuery.Rows.Count > 0 Then

            ' verifica se o objeto foi instanciado
            If objDatosPuesto Is Nothing Then

                ' inicializa um novo objeto do tipo DatosPuesto
                objDatosPuesto = New Integracion.ContractoServicio.RecuperarParametros.DatosPuesto()

                objDatosPuesto.Parametros = New Integracion.ContractoServicio.RecuperarParametros.ParametroColeccion()

                Dim drPais = From dr As DataRow In dtQuery.Rows Where Util.AtribuirValorObj(dr("COD_NIVEL_PARAMETRO"), GetType(Integer)) = ContractoServicio.Enumeradores.NivelParametro.Pais
                Dim drDelegacion = From dr As DataRow In dtQuery.Rows Where Util.AtribuirValorObj(dr("COD_NIVEL_PARAMETRO"), GetType(Integer)) = ContractoServicio.Enumeradores.NivelParametro.Delegacion
                Dim drPuesto = From dr As DataRow In dtQuery.Rows Where Util.AtribuirValorObj(dr("COD_NIVEL_PARAMETRO"), GetType(Integer)) = ContractoServicio.Enumeradores.NivelParametro.Puesto


                If drPais.Count > 0 Then
                    Util.AtribuirValorObjeto(objDatosPuesto.CodigoPais, drPais.First()("COD_PAIS"), GetType(String))
                End If

                If drPuesto.Count > 0 Then
                    ' popula as propriedades do objeto DatosPuesto
                    Util.AtribuirValorObjeto(objDatosPuesto.CodigoPuesto, drPuesto.First()("COD_PUESTO"), GetType(String))
                    Util.AtribuirValorObjeto(objDatosPuesto.CodigoHostPuesto, drPuesto.First()("COD_HOST_PUESTO"), GetType(String))
                Else
                    ' retorna um erro caso não tenha encontrado nenhum dado
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("032_msg_parametro_no_encontrado"))
                End If

                If drDelegacion.Count > 0 Then
                    Util.AtribuirValorObjeto(objDatosPuesto.CodigoDelegacion, drDelegacion.First()("COD_DELEGACION"), GetType(String))
                    Util.AtribuirValorObjeto(objDatosPuesto.DescripcionDelegacion, drDelegacion.First()("DES_DELEGACION"), GetType(String))
                End If

            End If

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                PopularRecuperarParametros(dr, objDatosPuesto)

            Next

        End If

        ' retornar coleção de Nivel
        Return objDatosPuesto

    End Function

    Private Shared Function RecuperarCodDelegacion(codigoAplicacion As String, codigoPuesto As String, codigoHostPuesto As String)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoHostPuesto))

        ' obter query
        Dim query As New StringBuilder

        query.Append(String.Format(My.Resources.GetParamCodDelegacion.ToString))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing AndAlso dtQuery.Rows.Count > 0 Then

            Return dtQuery.Rows(0)(0)

        End If

        Return Nothing
    End Function


    ''' <summary>
    ''' Recupera os Parametros do Posto de acordo com os filtros aplicados.
    ''' </summary>
    ''' <param name="codigoParametro"></param>
    ''' <param name="codigoPuesto"></param>
    ''' <param name="codigoAplicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function obtenerParametro(codigoParametro As String, codigoPuesto As String, codigoAplicacion As String, CodigoDelegacion As String) As Integracion.ContractoServicio.RecuperarParametros.Parametro

        Dim objParametro As Integracion.ContractoServicio.RecuperarParametros.Parametro = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(String.Format(My.Resources.ObtenerParametros.ToString, " UPPER(DEL.COD_DELEGACION) = UPPER([]COD_DELEGACION) and APL.COD_APLICACION = []COD_APLICACION"))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDelegacion))

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing AndAlso dtQuery.Rows.Count > 0 Then

            ' inicializa um objeto do tipo Parametro
            objParametro = New Integracion.ContractoServicio.RecuperarParametros.Parametro()

            ' verifica se existem valores no campos COD_OPCION e DES_OPCION
            ' caso exista, indica que o Parametro deverá conter uma lista de valores possíveis
            If dtQuery.Rows(0)("COD_OPCION") IsNot DBNull.Value AndAlso dtQuery.Rows(0)("DES_OPCION") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dtQuery.Rows(0)("COD_OPCION")) AndAlso Not String.IsNullOrEmpty(dtQuery.Rows(0)("DES_OPCION")) Then
                ' inicializa um objeto do tipo ValorPosibleColeccion
                objParametro.ValoresPosibles = New Integracion.ContractoServicio.RecuperarParametros.ValorPosibleColeccion()
            End If

            ' popula as propriedades do objeto Parametro
            Util.AtribuirValorObjeto(objParametro.CodigoParametro, dtQuery.Rows(0)("COD_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.EsObligatorio, dtQuery.Rows(0)("BOL_OBLIGATORIO"), GetType(Boolean))
            Util.AtribuirValorObjeto(objParametro.DescripcionCortaParametro, dtQuery.Rows(0)("DES_DESCRIPCION_CORTO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.DescripcionLargaParametro, dtQuery.Rows(0)("DES_DESCRIPCION_LARGA"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.ValorParametro, dtQuery.Rows(0)("DES_VALOR_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.ListaValores, dtQuery.Rows(0)("BOL_LISTA_VALORES"), GetType(Boolean))

            ' verifica se existem valores no campos COD_OPCION e DES_OPCION
            ' caso exista, indica que o Parametro deverá conter uma lista de valores possíveis
            If dtQuery.Rows(0)("COD_OPCION") IsNot DBNull.Value AndAlso dtQuery.Rows(0)("DES_OPCION") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dtQuery.Rows(0)("COD_OPCION")) AndAlso Not String.IsNullOrEmpty(dtQuery.Rows(0)("DES_OPCION")) Then

                ' verifica se a coleção de ValoresPosibles já foi criada anteriormente
                If objParametro.ValoresPosibles Is Nothing Then
                    ' inicializa um objeto do tipo ValorPosibleColeccion
                    objParametro.ValoresPosibles = New Integracion.ContractoServicio.RecuperarParametros.ValorPosibleColeccion()
                End If

                ' inicializa um objeto do tipo ValorPosible
                Dim objValorPosible As New Integracion.ContractoServicio.RecuperarParametros.ValorPosible()

                ' popula as propriedades do objeto ValorPosible
                Util.AtribuirValorObjeto(objValorPosible.CodigoValor, dtQuery.Rows(0)("COD_OPCION"), GetType(String))
                Util.AtribuirValorObjeto(objValorPosible.Valor, dtQuery.Rows(0)("DES_OPCION"), GetType(String))

                ' adiciona o objeto na coleção de ValoresPosibles
                objParametro.ValoresPosibles.Add(objValorPosible)

            End If

            ' garante que, se não foi preenchido o valor do parâmetro e tenha sido retornado um valor válido
            ' preenche a propriedade
            If String.IsNullOrEmpty(objParametro.ValorParametro) AndAlso dtQuery.Rows(0)("DES_VALOR_PARAMETRO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dtQuery.Rows(0)("DES_VALOR_PARAMETRO")) Then
                Util.AtribuirValorObjeto(objParametro.ValorParametro, dtQuery.Rows(0)("DES_VALOR_PARAMETRO"), GetType(String))
            End If

        End If

        Return objParametro

    End Function
#End Region

#Region "[POPULAR]"

    Private Shared Function PopularComboParametro(dr As DataRow) As ContractoServicio.Parametro.GetParametros.Parametro
        ' criar objeto aplicacion
        Dim objParametro As New ContractoServicio.Parametro.GetParametros.Parametro
        Util.AtribuirValorObjeto(objParametro.CodParametro, dr("COD_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.DesCortaParametro, dr("DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.DesLargaParametro, dr("DES_DESCRIPCION_LARGA"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.NecOrden, dr("NEC_ORDEN"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.BolObligatorio, dr("BOL_OBLIGATORIO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objParametro.NecTipoComponente, dr("NEC_TIPO_COMPONENTE"), GetType(TipoComponente))
        Util.AtribuirValorObjeto(objParametro.NecTipoDato, dr("NEC_TIPO_DATO"), GetType(TipoDato))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.DescripcionCorta, dr("GRUPO_DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.DescripcionLarga, dr("GRUPO_DES_DESCRIPCION_LARGA"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.NecOrden, dr("GRUPO_NEC_ORDEN"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.Nivel.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.Nivel.DescripcionNivel, dr("DES_NIVEL_PARAMETRO"), GetType(String))

        Return objParametro
    End Function

    Private Shared Function PopularComboParametroDetail(dr As DataRow) As ContractoServicio.Parametro.GetParametroDetail.Parametro
        ' criar objeto aplicacion
        Dim objParametro As New ContractoServicio.Parametro.GetParametroDetail.Parametro
        Util.AtribuirValorObjeto(objParametro.CodParametro, dr("COD_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.DesCortaParametro, dr("DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.DesLargaParametro, dr("DES_DESCRIPCION_LARGA"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.NecOrden, dr("NEC_ORDEN"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.BolObligatorio, dr("BOL_OBLIGATORIO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objParametro.NecTipoComponente, dr("NEC_TIPO_COMPONENTE"), GetType(TipoComponente))
        Util.AtribuirValorObjeto(objParametro.NecTipoDato, dr("NEC_TIPO_DATO"), GetType(TipoDato))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Aplicacion.PermisoAplicacion, dr("COD_PERMISO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.DescripcionCorta, dr("GRUPO_DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.DescripcionLarga, dr("GRUPO_DES_DESCRIPCION_LARGA"), GetType(String))
        Util.AtribuirValorObjeto(objParametro.Agrupacion.NecOrden, dr("GRUPO_NEC_ORDEN"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.Nivel.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(Integer))
        Util.AtribuirValorObjeto(objParametro.Nivel.DescripcionNivel, dr("DES_NIVEL_PARAMETRO"), GetType(String))
        Return objParametro
    End Function

    Private Shared Function PopularParametroOpcion(dr As DataRow) As ContractoServicio.Parametro.GetParametroOpciones.Opcion
        ' criar objeto Opcion
        Dim objOpcion As New ContractoServicio.Parametro.GetParametroOpciones.Opcion
        Util.AtribuirValorObjeto(objOpcion.CodigoOpcion, dr("COD_OPCION"), GetType(String))
        Util.AtribuirValorObjeto(objOpcion.DescripcionOpcion, dr("DES_OPCION"), GetType(String))
        Util.AtribuirValorObjeto(objOpcion.CodDelegacion, dr("COD_DELEGACION"), GetType(String))

        'Descomentar essa parte ao subir todas alterações da demanda 5838
        Util.AtribuirValorObjeto(objOpcion.EsVigente, dr("BOL_VIGENTE"), GetType(Int32))
        'Util.AtribuirValorObjeto(objOpcion.Parametro.CodParametro, dr("COD_PARAMETRO"), GetType(String))
        'Util.AtribuirValorObjeto(objOpcion.Parametro.DesCortaParametro, dr("Des_Descripcion_Corto"), GetType(String))

        Util.AtribuirValorObjeto(objOpcion.Parametro.NecTipoComponente, dr("Nec_Tipo_Dato"), GetType(Int32))
        Util.AtribuirValorObjeto(objOpcion.Parametro.NecTipoDato, dr("Nec_Tipo_Dato"), GetType(Int32))

        Return objOpcion
    End Function

    Public Shared Function ValidarParametroExiste(codigoAplicacion As String, codigoParametro As String) As Boolean
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ValidarParametroExiste.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))
        comando.CommandText = Util.PrepararQuery(query.ToString)
        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)
        Return Not String.IsNullOrEmpty(result)
    End Function

    Public Shared Function ValidarParametroPermiso(codigoAplicacion As String, codigoParametro As String, permisos As List(Of String)) As Boolean
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ValidarParametroPermiso.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoParametro))
        query.Append(Util.MontarClausulaIn(permisos, "COD_PERMISO", comando, "AND", "GEPR_TAPLICACION", ""))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)
        Return Not String.IsNullOrEmpty(result)
    End Function

    Private Shared Function TransformarParaParametroValueVO(drQuery As IDataReader) As List(Of ParametroValueVO)
        ' criar objeto aplicacion
        Dim objParametros As New List(Of ParametroValueVO)
        While drQuery.Read
            objParametros.Add(New ParametroValueVO() With { _
              .OIDParametro = drQuery.GetString("OID_PARAMETRO"), _
            .OIDParametroValue = drQuery.GetString("OID_PARAMETRO_VALOR") _
            })
        End While
        Return objParametros
    End Function

    Private Shared Sub PopularRecuperarParametros(dr As DataRow, ByRef datosPuesto As Integracion.ContractoServicio.RecuperarParametros.DatosPuesto)

        ' tenta localizar um obejto Parametro na coleção
        Dim objParametro As Integracion.ContractoServicio.RecuperarParametros.Parametro = datosPuesto.Parametros.FirstOrDefault(Function(p) p.CodigoParametro.Equals(dr("COD_PARAMETRO")))

        ' se não encotra, cria-se um
        If objParametro Is Nothing Then

            ' inicializa um objeto do tipo Parametro
            objParametro = New Integracion.ContractoServicio.RecuperarParametros.Parametro()

            ' verifica se existem valores no campos COD_OPCION e DES_OPCION
            ' caso exista, indica que o Parametro deverá conter uma lista de valores possíveis
            If dr("COD_OPCION") IsNot DBNull.Value AndAlso dr("DES_OPCION") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_OPCION")) AndAlso Not String.IsNullOrEmpty(dr("DES_OPCION")) Then
                ' inicializa um objeto do tipo ValorPosibleColeccion
                objParametro.ValoresPosibles = New Integracion.ContractoServicio.RecuperarParametros.ValorPosibleColeccion()
            End If

            ' popula as propriedades do objeto Parametro
            Util.AtribuirValorObjeto(objParametro.CodigoParametro, dr("COD_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.EsObligatorio, dr("BOL_OBLIGATORIO"), GetType(Boolean))
            Util.AtribuirValorObjeto(objParametro.DescripcionCortaParametro, dr("DES_DESCRIPCION_CORTO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.DescripcionLargaParametro, dr("DES_DESCRIPCION_LARGA"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.ValorParametro, dr("DES_VALOR_PARAMETRO"), GetType(String))
            Util.AtribuirValorObjeto(objParametro.ListaValores, dr("BOL_LISTA_VALORES"), GetType(Boolean))

            ' adiciona o objeto na coleção de Parametros
            datosPuesto.Parametros.Add(objParametro)

        End If

        ' verifica se existem valores no campos COD_OPCION e DES_OPCION
        ' caso exista, indica que o Parametro deverá conter uma lista de valores possíveis
        If dr("COD_OPCION") IsNot DBNull.Value AndAlso dr("DES_OPCION") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("COD_OPCION")) AndAlso Not String.IsNullOrEmpty(dr("DES_OPCION")) Then

            ' verifica se a coleção de ValoresPosibles já foi criada anteriormente
            If objParametro.ValoresPosibles Is Nothing Then
                ' inicializa um objeto do tipo ValorPosibleColeccion
                objParametro.ValoresPosibles = New Integracion.ContractoServicio.RecuperarParametros.ValorPosibleColeccion()
            End If

            ' inicializa um objeto do tipo ValorPosible
            Dim objValorPosible As New Integracion.ContractoServicio.RecuperarParametros.ValorPosible()

            ' popula as propriedades do objeto ValorPosible
            Util.AtribuirValorObjeto(objValorPosible.CodigoValor, dr("COD_OPCION"), GetType(String))
            Util.AtribuirValorObjeto(objValorPosible.Valor, dr("DES_OPCION"), GetType(String))

            ' adiciona o objeto na coleção de ValoresPosibles
            objParametro.ValoresPosibles.Add(objValorPosible)

        End If

        ' garante que, se não foi preenchido o valor do parâmetro e tenha sido retornado um valor válido
        ' preenche a propriedade
        If String.IsNullOrEmpty(objParametro.ValorParametro) AndAlso dr("DES_VALOR_PARAMETRO") IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(dr("DES_VALOR_PARAMETRO")) Then
            Util.AtribuirValorObjeto(objParametro.ValorParametro, dr("DES_VALOR_PARAMETRO"), GetType(String))
        End If

    End Sub

#End Region

    ''' <summary>
    ''' Obtém o OidParametro através do codigo do parametro
    ''' </summary>
    ''' <param name="CodParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 28/03/2012 Criado
    ''' </history>
    Public Shared Function ObterOidParametro(CodParametro As String, OIDAplicacion As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidParametro.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, CodParametro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, OIDAplicacion))

        Dim OidParametro As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidParametro = dtQuery.Rows(0)("OID_PARAMETRO")
        End If

        Return OidParametro

    End Function

#Region "[PARAMETRO OPCION]"

    ''' <summary>
    ''' Obtém o oidparametroopcion através do codigo do parametro e codigo da opcion
    ''' </summary>
    ''' <param name="CodigoOpcion"></param>
    ''' <param name="CodParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 28/03/2012 Criado
    ''' </history>
    Public Shared Function ObterOidParametroOpcion(CodigoOpcion As String, CodParametro As String, OIDAplicacion As String) As String

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidParametroOpcion.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, CodParametro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPCION", ProsegurDbType.Identificador_Alfanumerico, CodigoOpcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Identificador_Alfanumerico, OIDAplicacion))

        Dim OidParametroOpcion As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidParametroOpcion = dtQuery.Rows(0)("OID_PARAMETRO_OPCION")
        End If

        Return OidParametroOpcion

    End Function

    ''' <summary>
    ''' Verifica se o Codigo Opção existe no banco de dados.
    ''' </summary>
    ''' <param name="codigoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificaCodigoOpcaoMemoria(codigoOpcao As String, codParametro As String, oidAplicacion As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoParametroOpcion.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPCION", ProsegurDbType.Identificador_Alfanumerico, codigoOpcao))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codParametro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Identificador_Alfanumerico, oidAplicacion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se a Descrição Opção existe no banco de dados.
    ''' </summary>
    ''' <param name="descricaoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificaDescricaoOpcaoMemoria(descricaoOpcao As String, codParametro As String, oidAplicacion As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescricaoParametroOpcion.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_OPCION", ProsegurDbType.Identificador_Alfanumerico, descricaoOpcao))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codParametro))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Identificador_Alfanumerico, oidAplicacion))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

#Region "DELETE"

    ''' <summary>
    ''' Responsável por fazer a atualização do ParametroOpcion no DB.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/03/2012 Created
    ''' </history>
    Public Shared Sub DeleteParametroOpcion(objParametroOpcion As ContractoServicio.Parametro.GetParametroOpciones.Opcion, _
                                                 ByRef objtransacion As Transacao)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append("UPDATE GEPR_TPARAMETRO_OPCION SET ")

        ' adicionar campos
        query.Append(Util.AdicionarCampoQuery("BOL_VIGENTE = []BOL_VIGENTE,", "BOL_VIGENTE", comando, objParametroOpcion.EsVigente, ProsegurDbType.Logico))

        ' criar clausula where
        query.Append("WHERE COD_OPCION = []COD_OPCION")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPCION", ProsegurDbType.Identificador_Alfanumerico, objParametroOpcion.CodigoOpcion))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        objtransacion.AdicionarItemTransacao(comando)
        objtransacion.RealizarTransacao()

    End Sub

#End Region

#Region "INSERIR"

    ''' <summary>
    ''' Responsável por inserir o ParametroOpcion no DB.
    ''' </summary>
    ''' <param name="objParametroOpcion">Objeto ParametroOpcion</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/03/2012 Created
    ''' </history>    
    Public Shared Sub AltaParametroOpcion(objParametroOpcion As ContractoServicio.Parametro.GetParametroOpciones.Opcion, CodParametro As String, oidAplicacion As String)

        'criar(transação)
        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        ' Obtêm o comando
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaParametroOpcion.ToString())
        comando.CommandType = CommandType.Text

        Dim strOidParametroOpcion As String = Guid.NewGuid.ToString

        'Termino
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO_OPCION", ProsegurDbType.Objeto_Id, strOidParametroOpcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, ObterOidParametro(CodParametro, oidAplicacion)))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPCION", ProsegurDbType.Identificador_Alfanumerico, objParametroOpcion.CodigoOpcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_OPCION", ProsegurDbType.Descricao_Longa, objParametroOpcion.DescripcionOpcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objParametroOpcion.EsVigente))

        If String.IsNullOrEmpty(objParametroOpcion.CodDelegacion) Then
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Objeto_Id, DBNull.Value))
        Else
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Objeto_Id, objParametroOpcion.CodDelegacion))
        End If

        objtransacion.AdicionarItemTransacao(comando)
        objtransacion.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Monda o parameter
    ''' </summary>
    ''' <param name="campo"></param>
    ''' <param name="objeto"></param>
    ''' <param name="comando"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 20/03/2009 - Criado
    ''' </history>
    Private Shared Sub MontaParameter(campo As String, objeto As String, ByRef comando As IDbCommand)

        If objeto IsNot Nothing AndAlso objeto <> String.Empty Then

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, objeto))

        Else

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, campo, ProsegurDbType.Identificador_Alfanumerico, DBNull.Value))

        End If


    End Sub

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' Responsável por fazer a atualização do ParametroOpcion no DB.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 05/03/2012 Created
    ''' </history>
    Public Shared Sub ActualizarParametroOpcion(objParametroOpcion As ContractoServicio.Parametro.GetParametroOpciones.Opcion)

        'criar(transação)
        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        'query.Append("UPDATE GEPR_TPARAMETRO_OPCION SET DES_OPCION = '" + objParametroOpcion.DescripcionOpcion + "' WHERE COD_OPCION = '" + objParametroOpcion.CodigoOpcion + "' AND OID_PARAMETRO IN ( SELECT OID_PARAMETRO FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO = '" + objParametroOpcion.Parametro.CodParametro + "' )")

        query.Append("UPDATE GEPR_TPARAMETRO_OPCION SET DES_OPCION = '")
        query.Append(objParametroOpcion.DescripcionOpcion)
        query.Append("', BOL_VIGENTE = '")
        query.Append(Convert.ToInt32(objParametroOpcion.EsVigente))

        If objParametroOpcion.CodDelegacion IsNot Nothing Then

            query.Append("', COD_DELEGACION = '")
            query.Append(objParametroOpcion.CodDelegacion)

        End If

        query.Append("' WHERE COD_OPCION = '")
        query.Append(objParametroOpcion.CodigoOpcion)
        query.Append("' AND OID_PARAMETRO IN ( SELECT OID_PARAMETRO FROM GEPR_TPARAMETRO WHERE COD_PARAMETRO = '")
        query.Append(objParametroOpcion.Parametro.CodParametro)
        query.Append("' )")

        '' adicionar campos
        'query.Append(Util.AdicionarCampoQuery("DES_OPCION = []DES_OPCION,", "DES_OPCION", comando, objParametroOpcion.DescripcionOpcion, ProsegurDbType.Identificador_Alfanumerico))

        '' criar clausula where
        'query.Append("WHERE COD_OPCION = []COD_OPCION")
        'comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_OPCION", ProsegurDbType.Identificador_Alfanumerico, objParametroOpcion.CodigoOpcion))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        objtransacion.AdicionarItemTransacao(comando)
        objtransacion.RealizarTransacao()

    End Sub

#End Region

#End Region

End Class