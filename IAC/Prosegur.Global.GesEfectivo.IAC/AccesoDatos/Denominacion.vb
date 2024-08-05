Imports Prosegur.DbHelper
Imports Prosegur.Genesis
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Classe Denominacion
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 27/01/2009 Criado
''' </history>
Public Class Denominacion

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Verifica se o codigo de acesso existe.
    ''' </summary>
    ''' <param name="CodigoAcesso"></param>
    ''' <param name="IdentificadorDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function VerificarCodAccesoDenominacionExiste(CodigoAcesso As String, CodigoDenominacion As String, _
                                                                 IdentificadorDivisa As String) As Boolean

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.VerificarCodigoAccesoDenominacionExiste
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, CodigoAcesso))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, IdentificadorDivisa))

        If Not String.IsNullOrEmpty(CodigoDenominacion) Then

            cmd.CommandText &= " AND COD_DENOMINACION <> []COD_DENOMINACION "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDenominacion))

        End If

        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

    ''' <summary>
    ''' Efetua a consulta para verificar se o codigo da denominacion existe
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 27/01/2009 Criado
    ''' </history>
    Public Shared Function VerificarCodigoDenominacion(objPeticion As ContractoServicio.Divisa.VerificarCodigoDenominacion.Peticion) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoDenominacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Codigo))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Verifica se existe alguma denominacao com o codigo informado
    ''' </summary>
    ''' <param name="CodigoDenominacion">codigo da denominacao</param>
    ''' <returns>True ou False</returns>
    ''' <history>
    ''' [vinicius.gama] Criado em 13/08/2010
    ''' </history>
    Public Shared Function VerificarSeHayDenominacionConElCodigo(CodigoDenominacion As String) As Boolean

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarSeHayDenominacionConElCodigo.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDenominacion))

        ' executar query e retornar resultado
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando) > 0

    End Function

    ''' <summary>
    ''' Obtém as denominaciones através de uma divisa
    ''' </summary>
    ''' <param name="OidDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function getDenominacionesByDivisaObterDenominacion(OidDivisa As String) As ContractoServicio.Divisa.DenominacionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDenominacionesByDivisaObterDenominacion.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, OidDivisa))

        ' criar objeto denominacion coleccion
        Dim objDenominaciones As New ContractoServicio.Divisa.DenominacionColeccion

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer os registros encontrados
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar divisa para coleção
                objDenominaciones.Add(PopularDenominaciones(dr))

            Next

        End If

        ' retornar coleção de divisas
        Return objDenominaciones

    End Function

    ''' <summary>
    ''' Popula o objeto denominacion através de datarows
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function PopularDenominaciones(dr As DataRow) As ContractoServicio.Divisa.Denominacion

        ' criar objeto denominaciones
        Dim objDenominacion As New ContractoServicio.Divisa.Denominacion

        Util.AtribuirValorObjeto(objDenominacion.OidDenominacion, dr("OID_DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objDenominacion.Codigo, dr("COD_DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objDenominacion.Descripcion, dr("DES_DENOMINACION"), GetType(String))
        Util.AtribuirValorObjeto(objDenominacion.EsBillete, dr("BOL_BILLETE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDenominacion.Valor, dr("NUM_VALOR"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDenominacion.Peso, dr("NUM_PESO"), GetType(Decimal))
        Util.AtribuirValorObjeto(objDenominacion.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objDenominacion.CodigoUsuario, dr("COD_USUARIO"), GetType(String))
        Util.AtribuirValorObjeto(objDenominacion.FechaActualizacion, dr("FYH_ACTUALIZACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objDenominacion.CodigoAccesoDenominacion, dr("COD_ACCESO"), GetType(String))

        Dim peticionCodigoAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Peticion
        Dim objRespuesta As New ContractoServicio.CodigoAjeno.GetCodigosAjenos.Respuesta
        peticionCodigoAjeno.CodigosAjeno = New ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjeno
        peticionCodigoAjeno.ParametrosPaginacion = New Comon.Paginacion.ParametrosPeticionPaginacion
        peticionCodigoAjeno.ParametrosPaginacion.RegistrosPorPagina = 10
        peticionCodigoAjeno.CodigosAjeno.CodTipoTablaGenesis = "GEPR_TDENOMINACION"
        peticionCodigoAjeno.CodigosAjeno.OidTablaGenesis = objDenominacion.OidDenominacion
        objRespuesta.EntidadCodigosAjenos = AccesoDatos.CodigoAjeno.GetCodigosAjenos(peticionCodigoAjeno, objRespuesta.ParametrosPaginacion)

        If objDenominacion.CodigosAjenos Is Nothing Then
            objDenominacion.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        End If

        If objRespuesta.EntidadCodigosAjenos.Count > 0 Then
            For Each codAjeno In objRespuesta.EntidadCodigosAjenos(0).CodigosAjenos
                objDenominacion.CodigosAjenos.Add(ConstruirCodigoAjeno(codAjeno))
            Next
        End If

        Return objDenominacion

    End Function

    Public Shared Function ConstruirCodigoAjeno(item As ContractoServicio.CodigoAjeno.GetCodigosAjenos.CodigoAjenoRespuesta) As ContractoServicio.CodigoAjeno.CodigoAjenoBase
        Dim codigoAjeno = New ContractoServicio.CodigoAjeno.CodigoAjenoBase
        codigoAjeno.BolActivo = item.BolActivo
        codigoAjeno.BolDefecto = item.BolDefecto
        codigoAjeno.BolMigrado = item.BolMigrado
        codigoAjeno.CodAjeno = item.CodAjeno
        codigoAjeno.CodIdentificador = item.CodIdentificador
        codigoAjeno.DesAjeno = item.DesAjeno
        codigoAjeno.OidCodigoAjeno = item.OidCodigoAjeno
        Return codigoAjeno
    End Function

    ''' <summary>
    ''' Obtém os códigos de todas as denominaciones de uma divisa
    ''' </summary>
    ''' <param name="oidDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/03/2009 Criado
    ''' </history>
    Public Shared Function BuscarTodasDenominacionesDaDivisa(oidDivisa As String) As List(Of String)

        ' criar objeto retorno
        Dim retorno As New List(Of String)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterTodasDenominacionesDaDivisa.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_divisa", ProsegurDbType.Objeto_Id, oidDivisa))

        ' executar query e retornar resultado
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' caso encontre algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            ' para cada registro encontrado
            For Each dr As DataRow In dt.Rows

                If dr("cod_denominacion") IsNot DBNull.Value Then
                    retorno.Add(dr("cod_denominacion"))
                End If

            Next

        End If

        ' retorna lista com os codigos das denominaciones
        Return retorno

    End Function

    ''' <summary>
    ''' Obtém o oid da denominação
    ''' </summary>
    ''' <param name="CodigoDenominacion"></param>
    ''' <param name="OidDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 30/12/2010 Criado
    ''' </history>
    Public Shared Function ObterOidDenominacion(OidDivisa As String, CodigoDenominacion As String) As String

        If String.IsNullOrEmpty(CodigoDenominacion) OrElse String.IsNullOrEmpty(OidDivisa) Then
            Return String.Empty
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.ObtenerOidDenominacion.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, OidDivisa))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, CodigoDenominacion))

        ' executar query e retornar resultado
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("OID_DENOMINACION")
        Else
            Return String.Empty
        End If

    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Efetua a inserção de uma denominação no banco
    ''' </summary>
    ''' <param name="objDenominacion"></param>
    ''' <param name="OidDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Function AltaDenominacion(objDenominacion As ContractoServicio.Divisa.Denominacion, _
                                            OidDivisa As String, _
                                            CodigoUsuario As String, _
                                            ByRef objTransacao As Transacao) As String

        ' inicializar variáveis
        Dim OidDenominacion As String = String.Empty

        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter query
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaDenominacion.ToString)
            comando.CommandType = CommandType.Text

            ' gerar guid
            OidDenominacion = Guid.NewGuid().ToString
            objDenominacion.OidDenominacion = OidDenominacion

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DENOMINACION", ProsegurDbType.Objeto_Id, OidDenominacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DIVISA", ProsegurDbType.Objeto_Id, OidDivisa))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DENOMINACION", ProsegurDbType.Identificador_Alfanumerico, objDenominacion.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DENOMINACION", ProsegurDbType.Descricao_Longa, objDenominacion.Descripcion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_BILLETE", ProsegurDbType.Logico, objDenominacion.EsBillete))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_VALOR", ProsegurDbType.Numero_Decimal, objDenominacion.Valor))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ACCESO", ProsegurDbType.Identificador_Alfanumerico, objDenominacion.CodigoAccesoDenominacion))

            If objDenominacion.Peso >= Decimal.Zero Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PESO", ProsegurDbType.Numero_Decimal, objDenominacion.Peso))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NUM_PESO", ProsegurDbType.Numero_Decimal, DBNull.Value))
            End If

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objDenominacion.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            ' adicionar comando para transação
            objTransacao.AdicionarItemTransacao(comando)

        Catch ex As Exception

            Excepcion.Util.Tratar(ex, Traduzir("002_msg_Erro_UKDenominacion"))

        End Try

        ' retornar o oiddivisa
        Return OidDenominacion

    End Function

#End Region

#Region "[ATUALIZAR]"

    ''' <summary>
    ''' Atualiza a Denominacion
    ''' </summary>
    ''' <param name="objDenominacion"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub ActualizarDenominacion(objDenominacion As ContractoServicio.Divisa.Denominacion, _
                                             CodigoUsuario As String, _
                                             ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_tdenominacion SET ")

        query.Append(Util.AdicionarCampoQuery("des_denominacion = []des_denominacion,", "des_denominacion", comando, objDenominacion.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("bol_billete = []bol_billete,", "bol_billete", comando, objDenominacion.EsBillete, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("num_valor = []num_valor,", "num_valor", comando, objDenominacion.Valor, ProsegurDbType.Numero_Decimal))
        query.Append(Util.AdicionarCampoQuery("num_peso = []num_peso,", "num_peso", comando, objDenominacion.Peso, ProsegurDbType.Numero_Decimal))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objDenominacion.Vigente, ProsegurDbType.Logico))
        query.Append(Util.AdicionarCampoQuery("cod_acceso = []cod_acceso,", "cod_acceso", comando, objDenominacion.CodigoAccesoDenominacion, ProsegurDbType.Identificador_Alfanumerico))

        query.Append("cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

        query.Append("fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        query.Append("WHERE cod_denominacion = []cod_denominacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_denominacion", ProsegurDbType.Identificador_Alfanumerico, objDenominacion.Codigo))

        ' preparar o comando
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' adicionar comando para transação
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Efetua alta ou baixa de uma denominação a partir de um codigo de divisa
    ''' </summary>
    ''' <param name="CodigoIsoDivisa"></param>
    ''' <param name="CodigoUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/01/2009 Criado
    ''' </history>
    Public Shared Sub BajaDenominacion(CodigoIsoDivisa As String, _
                                       CodigoUsuario As String, _
                                       ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaDenominacion.ToString)
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