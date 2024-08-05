Imports Prosegur.DBHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

''' <summary>
''' Classe Divisa
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 27/01/2009 Criado
''' </history>
Public Class Divisa

#Region "[CONSULTAR]"
    Public Shared Function RecuperarTodasDivisasYDenominaciones() As Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.DivisasColeccion

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.DivisaObterDatosTodas.ToString
        cmd.CommandType = CommandType.Text


        Return PopularRecuperarTodasDivisasYDenominaciones(AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd))


    End Function

    Private Shared Function PopularRecuperarTodasDivisasYDenominaciones(dt As DataTable) As Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.DivisasColeccion

        Dim Divisas As Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.DivisasColeccion = Nothing

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Divisas = New Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.DivisasColeccion
            Dim Divisa As Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Divisa = Nothing

            Dim CodIsoDivisa As String = String.Empty

            For Each dr In dt.Rows

                If String.IsNullOrEmpty(CodIsoDivisa) OrElse CodIsoDivisa <> dr("COD_ISO_DIVISA") Then

                    If Not String.IsNullOrEmpty(CodIsoDivisa) Then
                        Divisas.Add(Divisa)
                    End If

                    Divisa = New Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Divisa
                    Divisa.Denominaciones = New Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.DenominacionColeccion

                    Divisa.CodigoIso = Util.AtribuirValorObj(dr("COD_ISO_DIVISA"), GetType(String))
                    Divisa.OidDivisa = Util.AtribuirValorObj(dr("OID_DIVISA"), GetType(String))
                    Divisa.Descripcion = Util.AtribuirValorObj(dr("DES_DIVISA"), GetType(String))
                    Divisa.CodigoAccesoDivisa = Util.AtribuirValorObj(dr("COD_ACCESO_DIVISA"), GetType(String))
                    Divisa.ColorDivisa = Util.AtribuirValorObj(dr("COD_COLOR"), GetType(String))
                    Divisa.CodigoSimbolo = Util.AtribuirValorObj(dr("COD_SIMBOLO"), GetType(String))
                    Divisa.Vigente = Util.AtribuirValorObj(dr("BOL_VIGENTE"), GetType(Boolean))
                    CodIsoDivisa = Divisa.CodigoIso

                End If

                Divisa.Denominaciones.Add(New Integracion.ContractoServicio.RecuperarTodasDivisasYDenominaciones.Denominacion With { _
                                                                    .Identificador = Util.AtribuirValorObj(dr("OID_DENOMINACION"), GetType(String)), _
                                                                    .Codigo = Util.AtribuirValorObj(dr("COD_DENOMINACION"), GetType(String)), _
                                                                    .CodigoAccesoDenominacion = Util.AtribuirValorObj(dr("COD_ACCESO_DENOMINACION"), GetType(String)), _
                                                                    .EsBillete = Util.AtribuirValorObj(dr("BOL_BILLETE"), GetType(Boolean)), _
                                                                    .Descripcion = Util.AtribuirValorObj(dr("DES_DENOMINACION"), GetType(String)), _
                                                                    .Valor = Util.AtribuirValorObj(dr("NUM_VALOR"), GetType(Decimal))})

            Next

            Divisas.Add(Divisa)

        End If

        Return Divisas
    End Function

    ''' <summary>
    ''' Efetua a consulta para verificar se o codigo da divisa existe
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Shared Function VerificarCodigoDivisa(objPeticion As ContractoServicio.Divisa.VerificarCodigoDivisa.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoIso))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se existe uma divisa através de uma descrição
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Shared Function VerificarDescripcionDivisa(objPeticion As ContractoServicio.Divisa.VerificarDescripcionDivisa.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DIVISA", ProsegurDbType.Descricao_Longa, objPeticion.DescripcionDivisa))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Obtém as divisas através dos filtros, caso não sejam informados, todos os registros serão retornados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function getDivisas(objPeticion As ContractoServicio.Divisa.GetDivisas.Peticion) As ContractoServicio.Divisa.GetDivisas.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetDivisa.ToString)

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))

        ' montar clausula in para COD_ISO_DIVISA
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.CodigoIso, "COD_ISO_DIVISA", comando, "AND"))

        ' montar clausula like para DES_DIVISA
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.Descripcion, "DES_DIVISA", comando, "AND"))

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Divisa.GetDivisas.DivisaColeccion

        ' executar query
        Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtDivisa IsNot Nothing _
            AndAlso dtDivisa.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtDivisa.Rows

                ' preencher a coleção com objetos divisa
                objDivisas.Add(PopularGetDivisasDivisa(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Obtém as divisas através dos filtros, caso não sejam informados, todos os registros serão retornados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function GetDivisasPaginacion(objPeticion As ContractoServicio.Divisa.GetDivisasPaginacion.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Divisa.GetDivisas.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetDivisa.ToString)

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objPeticion.Vigente))

        ' montar clausula in para COD_ISO_DIVISA
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.CodigoIso, "COD_ISO_DIVISA", comando, "AND"))

        ' montar clausula like para DES_DIVISA
        query.Append(Util.MontarClausulaLikeUpper(objPeticion.Descripcion, "DES_DIVISA", comando, "AND"))

        ' preparar query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Divisa.GetDivisas.DivisaColeccion

        ' executar query
        'Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim dtDivisa As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtDivisa IsNot Nothing _
            AndAlso dtDivisa.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtDivisa.Rows

                ' preencher a coleção com objetos divisa
                objDivisas.Add(PopularGetDivisasDivisa(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Popula o objeto divisa através de datarows
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Private Shared Function PopularDivisa(dr As DataRow) As ContractoServicio.Divisa.Divisa

        ' criar objeto divisa
        Dim objDivisa As New ContractoServicio.Divisa.Divisa

        Util.AtribuirValorObjeto(objDivisa.Identificador, dr("OID_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoIso, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoSimbolo, dr("COD_SIMBOLO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDivisa.CodigoUsuario, dr("COD_USUARIO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.FechaActualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDivisa.ColorDivisa, dr("COD_COLOR"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoAccesoDivisa, dr("COD_ACCESO"), GetType(String))

        ' criar objeto coleção de denominações
        objDivisa.Denominaciones = Denominacion.getDenominacionesByDivisaObterDenominacion(dr("OID_DIVISA"))

        ' retornar objeto divisa preenchido
        Return objDivisa

    End Function

    ''' <summary>
    ''' Popula o objeto divisa através de datarows
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Private Shared Function PopularGetDivisasDivisa(dr As DataRow) As ContractoServicio.Divisa.GetDivisas.Divisa

        ' criar objeto divisa
        Dim objDivisa As New ContractoServicio.Divisa.GetDivisas.Divisa

        Util.AtribuirValorObjeto(objDivisa.CodigoIso, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoSimbolo, dr("COD_SIMBOLO"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDivisa.ColorDivisa, dr("COD_COLOR"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoAccesoDivisa, dr("COD_ACCESO"), GetType(String))

        ' retornar objeto divisa preenchido
        Return objDivisa

    End Function

    ''' <summary>
    ''' Verifica se alguma entidade (Processo, Agrupacion, Medio Pago) esta vigente utilizando a divisa informada
    ''' Retorna True caso encontre alguma entidade vigente que usa a divisa
    ''' </summary>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function VerificarEntidadesVigentesComDivisa(CodigoIsoDivisa As String) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarEntidadesVigentesComDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIsoDivisa))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Obtém a divisa e suas denominações através do codigo iso informado
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function getDenominacionesByDivisaObterDivisa(objPeticion As ContractoServicio.Divisa.GetDenominacionesByDivisa.Peticion) As ContractoServicio.Divisa.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        Dim query As New StringBuilder
        query.Append(My.Resources.GetDenominacionesByDivisaObterDivisa.ToString)

        query.Append(Util.MontarClausulaIn(objPeticion.CodigoIso, "COD_ISO_DIVISA", comando, "WHERE"))

        ' preparar comando
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Divisa.DivisaColeccion

        ' executar query
        Dim dtDivisas As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtDivisas IsNot Nothing _
            AndAlso dtDivisas.Rows.Count > 0 Then

            ' percorrer os registros encontrados
            For Each dr As DataRow In dtDivisas.Rows

                ' adicionar divisa para coleção
                objDivisas.Add(PopularDivisa(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Obtém as divisas através de um codigo agrupacion
    ''' As divisas são retornadas atraves de médio pago e divisa por agrupacion
    ''' </summary>
    ''' <param name="CodigoAgrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function ObterDivisasPorAgrupacion(CodigoAgrupacion As String) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getAgrupacionesDetailObterDivisasPorAgrupaciones.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_AGRUPACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAgrupacion))

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.DivisaColeccion

        ' executar query
        Dim dtDivisa As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtDivisa IsNot Nothing _
            AndAlso dtDivisa.Rows.Count > 0 Then

            ' percorrer registros encontrados
            For Each dr As DataRow In dtDivisa.Rows

                Dim CodigoIso As String = dr("COD_ISO_DIVISA")

                ' verificar se já existe a divisa
                Dim divisaExiste = (From Divisas In objDivisas _
                                   Where Divisas.CodigoIso = CodigoIso).Count

                ' se não encontrou a divisa
                If divisaExiste = 0 Then

                    ' preencher a coleção com objetos divisa
                    objDivisas.Add(PopularObterDivisasPorAgrupacion(dr))

                End If

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Preenche um objeto divisa através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Private Shared Function PopularObterDivisasPorAgrupacion(dr As DataRow) As ContractoServicio.Agrupacion.GetAgrupacionesDetail.Divisa

        Dim objDivisa As New ContractoServicio.Agrupacion.GetAgrupacionesDetail.Divisa

        Util.AtribuirValorObjeto(objDivisa.CodigoIso, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.TieneEfectivo, dr("ES_EFECTIVO"), GetType(Boolean))

        Return objDivisa

    End Function

    ''' <summary>
    ''' Obtém todas as divisas vigente ou não vigente e devolve em um datatable
    ''' </summary>
    ''' <param name="Vigente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 04/02/2009 Criado
    ''' </history>
    Public Shared Function ObterDivisas(Vigente As Boolean) As DataTable

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametro
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, Vigente))

        ' executar query
        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

    ''' <summary>
    ''' Obtém o oid da divisa
    ''' </summary>
    ''' <param name="CodigoIso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 12/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidDivisa(CodigoIso As String) As String

        ' caso o parametro seja nothing, deve retornar string.empty.
        ' isto é necessário pois ao efetuar uma baja o codigo é passado com valor nothing.
        If CodigoIso Is Nothing Then
            Return String.Empty
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIso))

        ' executar query e retornar resultado
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("OID_DIVISA")
        Else
            Return String.Empty
        End If

    End Function

    ''' <summary>
    ''' Obtém as divisas vigentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboDivisas() As ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Utilidad.GetComboDivisas.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboDivisas.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objDivisas.Add(PopularComboDivisas(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Popula um objeto divisa com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Private Shared Function PopularComboDivisas(dr As DataRow) As ContractoServicio.Utilidad.GetComboDivisas.Divisa

        ' criar objeto formato
        Dim objDivisa As New ContractoServicio.Utilidad.GetComboDivisas.Divisa

        Util.AtribuirValorObjeto(objDivisa.Identificador, dr("OID_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.CodigoIso, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))

        ' retorna objeto preenchido
        Return objDivisa

    End Function

    ''' <summary>
    ''' Obtém combo de divisas através de um tipo medio pago
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Public Shared Function getComboDivisasByTipoMedioPago(objPeticion As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Peticion) As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.DivisaColeccion

        ' criar objeto divisa coleccion
        Dim objDivisas As New ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboDivisasByTipoMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_MEDIO_PAGO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoTipoMedioPago))

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objDivisas.Add(PopularComboDivisasByTipoMedioPago(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDivisas

    End Function

    ''' <summary>
    ''' Verifica se o codigo de acesso existe.
    ''' </summary>
    ''' <param name="CodigoAcesso"></param>
    ''' <param name="IdentificadorDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarCodAccesoDivisaExiste(CodigoAcesso As String, IdentificadorDivisa As String) As Boolean

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.VerificarCodigoAccsoDivisaExiste
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, CodigoAcesso))

        If Not String.IsNullOrEmpty(IdentificadorDivisa) Then

            cmd.CommandText &= " AND OID_DIVISA <> []OID_DIVISA "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))

        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

    ''' <summary>
    ''' Popula um objeto divisa com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 02/02/2009 Criado
    ''' </history>
    Private Shared Function PopularComboDivisasByTipoMedioPago(dr As DataRow) As ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Divisa

        ' criar objeto formato
        Dim objDivisa As New ContractoServicio.Utilidad.GetComboDivisasByTipoMedioPago.Divisa

        Util.AtribuirValorObjeto(objDivisa.CodigoIso, dr("COD_ISO_DIVISA"), GetType(String))
        Util.AtribuirValorObjeto(objDivisa.Descripcion, dr("DES_DIVISA"), GetType(String))

        ' retorna objeto preenchido
        Return objDivisa

    End Function

    ''' <summary>
    ''' Obtém divisas, tipos medio pago e medio pago. Todas as divisas e médio pago são vigentes = true
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 06/02/2009 Criado
    ''' [pda] 30/03/2009 Alterado
    ''' </history>
    Public Shared Function GetDivisaMedioPago() As ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' criar objeto respuesta
        Dim objDivisas As New ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = My.Resources.GetDivisaMedioPago.ToString
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            Dim CodigoIsoDivisa As String = String.Empty
            Dim CodigoTipoMedioPago As String = String.Empty
            Dim CodigoMedioPago As String = String.Empty

            ' percorrer registros encontrados
            For Each dr As DataRow In dtQuery.Rows

                Util.AtribuirValorObjeto(CodigoIsoDivisa, dr("COD_ISO_DIVISA"), GetType(String))
                Util.AtribuirValorObjeto(CodigoTipoMedioPago, dr("COD_TIPO_MEDIO_PAGO"), GetType(String))
                Util.AtribuirValorObjeto(CodigoMedioPago, dr("COD_MEDIO_PAGO"), GetType(String))

                ' verificar se existe divisa
                ' --------------------------------------
                If CodigoIsoDivisa IsNot Nothing Then
                    Dim DivisasExistentes = From Divisas In objDivisas _
                                        Where Divisas.CodigoIso = CodigoIsoDivisa

                    ' se não existir divisa
                    If DivisasExistentes.Count = 0 Then
                        ' adicionar
                        Dim objDivisa As New ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa
                        objDivisa.CodigoIso = dr("COD_ISO_DIVISA")
                        objDivisa.Descripcion = dr("DES_DIVISA")
                        objDivisa.TiposMedioPago = New ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPagoColeccion
                        objDivisas.Add(objDivisa)
                    End If
                End If
                ' --------------------------------------

                ' verificar se existe tipos medio pago
                '---------------------------------------
                If CodigoIsoDivisa IsNot Nothing AndAlso CodigoTipoMedioPago IsNot Nothing Then

                    Dim PesDivisas As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa = _
                    objDivisas.Find(New Predicate(Of ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa)(Function(s) s.CodigoIso = CodigoIsoDivisa))

                    If PesDivisas IsNot Nothing Then

                        Dim TiposExistentes = From TpMedioPago In PesDivisas.TiposMedioPago _
                                              Where TpMedioPago.Codigo = CodigoTipoMedioPago

                        If TiposExistentes.Count = 0 Then
                            Dim objTiposMedioPago As New ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago
                            objTiposMedioPago.Codigo = dr("cod_tipo_medio_pago")

                            If Not String.IsNullOrEmpty(dr("cod_tipo_medio_pago")) Then
                                objTiposMedioPago.Descripcion = TipoMedioPago.ObterTipoMedioPagoDescripcion(dr("cod_tipo_medio_pago"))
                            End If

                            objTiposMedioPago.MediosPago = New ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPagoColeccion
                            PesDivisas.TiposMedioPago.Add(objTiposMedioPago)
                        End If

                    End If

                End If
                ' ---------------------------------------

                ' verificar se existe medios pago
                ' ---------------------------------------
                If CodigoIsoDivisa IsNot Nothing _
                    AndAlso CodigoTipoMedioPago IsNot Nothing _
                    AndAlso CodigoMedioPago IsNot Nothing Then

                    Dim PesDivisas1 As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa = _
                    objDivisas.Find(New Predicate(Of ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa)(Function(s) s.CodigoIso = CodigoIsoDivisa))

                    Dim PesTipos As ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago = _
                        PesDivisas1.TiposMedioPago.Find(New Predicate(Of ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago)(Function(s) s.Codigo = CodigoTipoMedioPago))

                    Dim MediosExistentes = From MedioPago In PesTipos.MediosPago _
                                           Where MedioPago.Codigo = CodigoMedioPago


                    If MediosExistentes.Count = 0 Then

                        If Not dr("MP_VIGENTE").Equals(DBNull.Value) AndAlso CBool(dr("MP_VIGENTE")) Then

                            Dim objMedioPago As New ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPago
                            objMedioPago.Codigo = dr("cod_medio_pago")
                            objMedioPago.Descripcion = dr("des_medio_pago")
                            PesTipos.MediosPago.Add(objMedioPago)

                        End If
                        
                    End If

                    'Se não foi incluida nenhum médio de pago então remove o tipo médio de pago da divisa
                    If PesTipos.MediosPago.Count = 0 Then
                        PesDivisas1.TiposMedioPago.Remove(PesTipos)
                    End If

                End If
                ' ---------------------------------------

            Next

        End If





        ' retornar coleção de divisas
        Return objDivisas

    End Function

#Region "[GETPROCESO]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Oid_agrupacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaDivisasAgrupacion(Oid_agrupacion As String) As Integracion.ContractoServicio.GetProceso.DivisaColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'RETORNA DIVISAS
        comando = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaDivisaAgrupacion.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_AGRUPACION", ProsegurDbType.Objeto_Id, Oid_agrupacion))

        Dim dtDivisaAgrupacion As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verificase o dtDivisaAgrupacion retornou algum registro.
        If dtDivisaAgrupacion IsNot Nothing AndAlso dtDivisaAgrupacion.Rows.Count > 0 Then

            Return PopularDivisasDenominaciones(dtDivisaAgrupacion)

        Else

            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dtDivisasDenominaciones"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularDivisasDenominaciones(dtDivisasDenominaciones As DataTable) As GetProceso.DivisaColeccion

        Dim objDivisaDenominaciones As New GetProceso.DivisaColeccion
        Dim cod_divisa As String = String.Empty
        Dim objDivisa As New GetProceso.Divisa

        'Percorre o dtDivisaAgrupacion
        For Each drDivisaAgrupacion As DataRow In dtDivisasDenominaciones.Rows

            'Verifica para saber se o COD_ISO_DIVISA ja foi armazenado
            If drDivisaAgrupacion("COD_ISO_DIVISA") <> cod_divisa Then

                cod_divisa = drDivisaAgrupacion("COD_ISO_DIVISA")

                objDivisa = New GetProceso.Divisa

                'Cria e Popula uma nova divisa
                objDivisa = PopulaDivisasDenominaciones(drDivisaAgrupacion)

                'Cria e insere a denominação.
                objDivisa.Denominaciones = New GetProceso.DenominacionColeccion

                objDivisa.Denominaciones.Add(PopulaDenominacoesDivisa(drDivisaAgrupacion))

                objDivisaDenominaciones.Add(objDivisa)

                objDivisa = Nothing
            Else

                'Obtem a ultima divisa incluida e inclui a denominação na mesma.
                objDivisaDenominaciones(objDivisaDenominaciones.Count - 1).Denominaciones.Add(PopulaDenominacoesDivisa(drDivisaAgrupacion))

            End If

        Next

        Return objDivisaDenominaciones

    End Function

    ''' <summary>
    ''' Popula o objeto divisa
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaDivisasDenominaciones(dr As DataRow) As GetProceso.Divisa

        Dim objDivisa As New GetProceso.Divisa()

        If dr("COD_ISO_DIVISA") IsNot DBNull.Value Then
            objDivisa.CodigoISO = dr("COD_ISO_DIVISA")
        End If

        If dr("DES_DIVISA") IsNot DBNull.Value Then
            objDivisa.Descripcion = dr("DES_DIVISA")
        End If

        Return objDivisa

    End Function

    ''' <summary>
    ''' Popula o objeto divisa
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaDivisaProceso(dr As DataRow) As GetProceso.DivisaProceso

        Dim objDivisa As New GetProceso.DivisaProceso()

        If dr("COD_ISO_DIVISA") IsNot DBNull.Value Then
            objDivisa.CodigoISO = dr("COD_ISO_DIVISA")
        End If

        If dr("DES_DIVISA") IsNot DBNull.Value Then
            objDivisa.Descripcion = dr("DES_DIVISA")
        End If

        If dr("NEC_NUM_ORDEN") IsNot DBNull.Value Then
            objDivisa.NumOrden = Integer.Parse(dr("NEC_NUM_ORDEN").ToString())
        End If

        If dr("NUM_TOLERANCIA_PARCIAL_MIN") IsNot DBNull.Value Then
            objDivisa.ToleranciaParcialMin = dr("NUM_TOLERANCIA_PARCIAL_MIN")
        End If

        If dr("NUM_TOLERANCIA_PARCIAL_MAX") IsNot DBNull.Value Then
            objDivisa.TolerenciaParcialMax = dr("NUM_TOLERANCIA_PARCIAL_MAX")
        End If

        If dr("NUM_TOLERANCIA_BULTO_MIN") IsNot DBNull.Value Then
            objDivisa.ToleranciaBultoMin = dr("NUM_TOLERANCIA_BULTO_MIN")
        End If
        If dr("NUM_TOLERANCIA_BULTO_MAX") IsNot DBNull.Value Then
            objDivisa.ToleranciaBultoMax = dr("NUM_TOLERANCIA_BULTO_MAX")
        End If

        If dr("NUM_TOLERANCIA_REMESA_MIN") IsNot DBNull.Value Then
            objDivisa.ToleranciaRemesaMin = dr("NUM_TOLERANCIA_REMESA_MIN")
        End If

        If dr("NUM_TOLERANCIA_REMESA_MAX") IsNot DBNull.Value Then
            objDivisa.ToleranciaRemesaMax = dr("NUM_TOLERANCIA_REMESA_MAX")
        End If

        Return objDivisa

    End Function

    ''' <summary>
    ''' Popula o objeto divisa
    ''' </summary>
    ''' <param name="dtDivisasDenominaciones"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaDivisasDenominacionesProceso(dtDivisasDenominaciones As DataTable) As GetProceso.DivisaProcesoColeccion

        Dim objDivisaDenominaciones As New GetProceso.DivisaProcesoColeccion
        Dim cod_divisa As String = String.Empty
        Dim objDivisa As GetProceso.DivisaProceso = Nothing

        'Percorre o dtDivisaAgrupacion
        For Each drDivisaAgrupacion As DataRow In dtDivisasDenominaciones.Rows

            cod_divisa = drDivisaAgrupacion("COD_ISO_DIVISA")

            'Verifica para saber se o COD_ISO_DIVISA ja foi armazenado
            If (From Div In objDivisaDenominaciones Where Div.CodigoISO = cod_divisa).Count = 0 Then

                'Cria e Popula uma nova divisa
                objDivisa = PopulaDivisaProceso(drDivisaAgrupacion)

                'Cria e insere a denominação.
                objDivisa.Denominaciones = New GetProceso.DenominacionColeccion
                objDivisa.Denominaciones.Add(PopulaDenominacoesDivisa(drDivisaAgrupacion))

                objDivisaDenominaciones.Add(objDivisa)

            Else

                'Obtem a ultima divisa incluida e inclui a denominação na mesma.
                Dim objDivDen = (From Div In objDivisaDenominaciones Where Div.CodigoISO = cod_divisa).FirstOrDefault
                objDivDen.Denominaciones.Add(PopulaDenominacoesDivisa(drDivisaAgrupacion))

            End If

        Next

        Return objDivisaDenominaciones

    End Function

    ''' <summary>
    ''' Popula o objDenominacoes
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 11/03/2009 Criado
    ''' </history>
    Private Shared Function PopulaDenominacoesDivisa(dr As DataRow) As GetProceso.Denominacion

        Dim objDenominacion As New GetProceso.Denominacion

        If dr("COD_DENOMINACION") IsNot DBNull.Value Then
            objDenominacion.Codigo = dr("COD_DENOMINACION")
        End If

        If dr("DES_DENOMINACION") IsNot DBNull.Value Then
            objDenominacion.Descripcion = dr("DES_DENOMINACION")
        End If

        If dr("BOL_BILLETE") IsNot DBNull.Value Then
            objDenominacion.EsBillete = dr("BOL_BILLETE")
        End If

        If dr("NUM_VALOR") IsNot DBNull.Value Then
            objDenominacion.Valor = dr("NUM_VALOR")
        End If

        If dr("NUM_PESO") IsNot DBNull.Value Then
            objDenominacion.Peso = dr("NUM_PESO")
        End If

        Return objDenominacion

    End Function

    ''' <summary>
    ''' Retorna um objEfectivo
    ''' </summary>
    ''' <param name="oidProceso"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] Criado 11/03/2009
    ''' </history>
    Public Shared Function RetornaDivisa(oidProceso As String) As GetProceso.DivisaProcesoColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim objDivisasProceso As New GetProceso.DivisaProcesoColeccion

        comando.CommandText = Util.PrepararQuery(My.Resources.GetProcesoBuscaDivisaEfectivo.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO", ProsegurDbType.Objeto_Id, oidProceso))

        Dim dtDivisaEfectivo As New DataTable

        dtDivisaEfectivo = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Verifica se o dtIac retornou algum registro.
        If dtDivisaEfectivo IsNot Nothing AndAlso dtDivisaEfectivo.Rows.Count > 0 Then

            Return PopulaDivisasDenominacionesProceso(dtDivisaEfectivo)

        Else

            Return Nothing

        End If

    End Function


#End Region

#End Region

#Region "[DELETAR]"



#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Efetua a inserção de uma divisa no banco
    ''' </summary>
    ''' <param name="objDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub AltaDivisa(objDivisa As ContractoServicio.Divisa.Divisa, _
                                 CodigoUsuario As String)

        Try

            ' criar transacao
            Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter query
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaDivisa.ToString)
            comando.CommandType = CommandType.Text

            ' gerar guid
            objDivisa.Identificador = Guid.NewGuid().ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, objDivisa.Identificador))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, objDivisa.CodigoIso))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DIVISA", ProsegurDbType.Descricao_Longa, objDivisa.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SIMBOLO", ProsegurDbType.Identificador_Alfanumerico, objDivisa.CodigoSimbolo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objDivisa.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, objDivisa.CodigoAccesoDivisa))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_COLOR", ProsegurDbType.Identificador_Alfanumerico, objDivisa.ColorDivisa))

            ' adicionar comando para transação
            objTransacao.AdicionarItemTransacao(comando)

            ' caso existam denominaciones para divisa
            If objDivisa.Denominaciones IsNot Nothing _
                AndAlso objDivisa.Denominaciones.Count > 0 Then

                ' para cada denominacao da divisa
                For Each objDenominacion As ContractoServicio.Divisa.Denominacion In objDivisa.Denominaciones

                    ' verificar se o codigo da denominacion foi enviado
                    If String.IsNullOrEmpty(objDenominacion.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_CodigoDenominacionVazio"))
                    End If

                    ' verificar se a descrição da denominacion foi enviada
                    If String.IsNullOrEmpty(objDenominacion.Descripcion) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_DescricaoDenominacionVazio"))
                    End If

                    ' Verifica se existem medios pagos com o codigo denominacao informado
                    If AccesoDatos.MedioPago.VerificarSeHayMediosPagosConElCodigo(objDenominacion.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("002_msg_CodigoDenominacionMedioPago"), objDenominacion.Codigo))
                    End If

                    ' efetuar inserção da denominacion
                    Denominacion.AltaDenominacion(objDenominacion, objDivisa.Identificador, CodigoUsuario, objTransacao)

                Next

            End If

            ' realizar a transação
            objTransacao.RealizarTransacao()

        Catch ex As Exception

            Excepcion.Util.Tratar(ex, Traduzir("002_msg_Erro_UKDivisa"))

        End Try

    End Sub

#End Region

#Region "[ATUALIZAR]"

    ''' <summary>
    ''' Atualiza a divisa
    ''' </summary>
    ''' <param name="objDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub ActualizarDivisa(objDivisa As ContractoServicio.Divisa.Divisa, _
                                       CodigoUsuario As String, _
                                       oidDivisa As String)

        ' criar transacao
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

        ' atualizar os dados da divisa
        ActualizarDivisa(objDivisa, CodigoUsuario, oidDivisa, objTransacao)

        ' caso existam denominaciones para divisa
        If objDivisa.Denominaciones IsNot Nothing _
            AndAlso objDivisa.Denominaciones.Count > 0 Then

            ' buscar todas as denominaciones da divisa
            Dim denominaciones As List(Of String) = Denominacion.BuscarTodasDenominacionesDaDivisa(oidDivisa)

            ' para cada denominacao da divisa
            For Each objDenominacion As ContractoServicio.Divisa.Denominacion In objDivisa.Denominaciones

                ' verificar se o codigo da denominacion foi enviado
                If String.IsNullOrEmpty(objDenominacion.Codigo) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_CodigoDenominacionVazio"))
                End If

                'Verifica se o codigo de acesso da divisa existe.
                If AccesoDatos.Denominacion.VerificarCodAccesoDenominacionExiste(objDenominacion.CodigoAccesoDenominacion, objDenominacion.Codigo, oidDivisa) Then
                    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("002_msg_CodigoAccesoExiste"))
                End If

                ' verificar se a denominacion existe
                If denominaciones.Contains(objDenominacion.Codigo) Then
                    ' efetuar atualização da denominacion
                    Denominacion.ActualizarDenominacion(objDenominacion, CodigoUsuario, objTransacao)
                Else

                    ' Verifica se existem medios pagos com o codigo denominacao informado
                    If AccesoDatos.MedioPago.VerificarSeHayMediosPagosConElCodigo(objDenominacion.Codigo) Then
                        Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, String.Format(Traduzir("002_msg_CodigoDenominacionMedioPago"), objDenominacion.Codigo))
                    End If

                    ' efetuar inserção da denominacion
                    Denominacion.AltaDenominacion(objDenominacion, oidDivisa, CodigoUsuario, objTransacao)
                End If

            Next

        End If

        ' se a divisa não for vigente
        If Not objDivisa.Vigente Then
            ' efetuar a baixa para todas as denominacoes
            Denominacion.BajaDenominacion(objDivisa.CodigoIso, CodigoUsuario, objTransacao)
        End If

        ' realizar a transação
        objTransacao.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Atualiza a divisa na base de dados.
    ''' </summary>
    ''' <param name="objDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="oidDivisa"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Private Shared Sub ActualizarDivisa(objDivisa As ContractoServicio.Divisa.Divisa, _
                                        CodigoUsuario As String, _
                                        oidDivisa As String, _
                                        ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' preparar query
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_tdivisa SET ")

        query.Append(Util.AdicionarCampoQuery("des_divisa = []des_divisa,", "des_divisa", comando, objDivisa.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("cod_simbolo = []cod_simbolo,", "cod_simbolo", comando, objDivisa.CodigoSimbolo, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objDivisa.Vigente, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("cod_color = []cod_color,", "cod_color", comando, objDivisa.ColorDivisa, ProsegurDbType.Identificador_Alfanumerico))
        query.Append(Util.AdicionarCampoQuery("cod_acceso = []cod_acceso,", "cod_acceso", comando, objDivisa.CodigoAccesoDivisa, ProsegurDbType.Identificador_Alfanumerico))

        query.Append(" cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

        query.Append(" fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        ' adicionar clausula where
        query.Append(" WHERE oid_divisa = []oid_divisa ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_divisa", ProsegurDbType.Objeto_Id, oidDivisa))

        ' obter query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Efetua alta ou baixa de uma divisa e suas denominaciones
    ''' </summary>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub BajaDivisaDenominacion(CodigoIsoDivisa As String, _
                                             CodigoUsuario As String)

        ' criar transacao
        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)

        ' efetuar a baixa da divisa
        BajaDivisa(CodigoIsoDivisa, CodigoUsuario, objTransacao)

        ' baixar todas as denominaciones da divisa
        Denominacion.BajaDenominacion(CodigoIsoDivisa, CodigoUsuario, objTransacao)

        ' realizar a transacao
        objTransacao.RealizarTransacao()

    End Sub

    ''' <summary>
    ''' Efetua alta ou baixa de uma divisa
    ''' </summary>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub BajaDivisa(CodigoIsoDivisa As String, _
                                 CodigoUsuario As String, _
                                 ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parameters
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ISO_DIVISA", ProsegurDbType.Identificador_Alfanumerico, CodigoIsoDivisa))

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class