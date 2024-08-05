Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager

''' <summary>
''' Classe ValorTerminoMedioPago
''' </summary>
''' <remarks></remarks>
Public Class ValorTerminoMedioPago

#Region "CONSULTAR"

    ''' <summary>
    ''' Busca Todos os Valores de um Termino
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Function BuscaTodosValoresTerminos(oidTermino As String) As ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTodosValoresTerminos.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oidTermino))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaValoresTerminos As New ContractoServicio.MedioPago.SetMedioPago.ValorTerminoColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaValoresTerminos.Add(PopularBuscaValoresTerminos(dr))
            Next
        End If
        Return objRetornaValoresTerminos
    End Function

    ''' <summary>
    ''' Popula um objeto valor de termino através de um datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 25/02/2009 Criado
    ''' </history>
    Private Shared Function PopularBuscaValoresTerminos(dr As DataRow) As ContractoServicio.MedioPago.SetMedioPago.ValorTermino

        ' criar objeto valor termino Iac
        Dim objValorTerminoMedioPago As New ContractoServicio.MedioPago.SetMedioPago.ValorTermino

        'Valor de Termino
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Codigo, dr("COD_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Descripcion, dr("DES_VALOR"), GetType(String))
        Util.AtribuirValorObjeto(objValorTerminoMedioPago.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objValorTerminoMedioPago

    End Function

    ''' <summary>
    ''' Obtem valor terminio por oid do termino
    ''' </summary>
    ''' <param name="OidTermino"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ObterValorTerminoPorOidTermino(OidTermino As String) As DataTable

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.ObterValorTerminoPorOidTermino.ToString)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, OidTermino))

        ' executar query Terminos
        Return AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

    End Function

#End Region

#Region "DELETAR"

    ''' <summary>
    ''' Apagar todos os registros de valores de um termino vinculados a um Médio de Pago
    ''' </summary>
    ''' <param name="OidMedioPago"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Criado
    ''' </history>
    Public Shared Sub BajaValorTerminoMediosPagoPorMedioPago(OidMedioPago As String, _
                                                             CodUsuario As String, _
                                                             ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaValorTerminoMedioPagoPorMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_MEDIO_PAGO", ProsegurDbType.Objeto_Id, OidMedioPago))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

    ''' <summary>
    ''' Apagar todos os registros de valores de um termino vinculados a um Médio de Pago
    ''' </summary>
    ''' <param name="objValorTerminoPago"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="oidTermino"></param>
    ''' <param name="objTransacao"></param>
    ''' <remarks></remarks>
    Public Shared Sub BajaValorTerminoMediosPago(objValorTerminoPago As ContractoServicio.MedioPago.SetMedioPago.ValorTermino, _
                                                             CodUsuario As String, _
                                                             oidTermino As String, _
                                                             ByRef objTransacao As Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaValorTerminoMedioPago.ToString)
        comando.CommandType = CommandType.Text

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oidTermino))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, False))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, objValorTerminoPago.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar query para transacao
        objTransacao.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "INSERIR"

    ''' <summary>
    ''' Responsável por inserir o Termino do Medio de Pago no DB.
    ''' </summary>
    ''' <param name="objValorTermino">Objeto Valor de Termino</param>
    ''' <param name="CodigoUsuario">Usuário responsável</param>
    ''' <param name="oidTermino">Oid do Termino referente ao Medio de Pago referenciado</param>
    ''' <param name="objtransacion">Transação corrente</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Sub AltaValorTerminoMedioPago(objValorTermino As ContractoServicio.MedioPago.SetMedioPago.ValorTermino, _
                                           CodigoUsuario As String, _
                                           oidTermino As String, _
                                           ByRef objtransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        ' Obtêm o comando
        comando.CommandText = Util.PrepararQuery(My.Resources.AltaValorTerminoMedioPago.ToString())
        comando.CommandType = CommandType.Text

        Dim strOidValor As String = Guid.NewGuid.ToString

        'Valor de Termino
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_VALOR", ProsegurDbType.Objeto_Id, strOidValor))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TERMINO", ProsegurDbType.Objeto_Id, oidTermino))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_VALOR", ProsegurDbType.Identificador_Alfanumerico, objValorTermino.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_VALOR", ProsegurDbType.Descricao_Longa, objValorTermino.Descripcion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objValorTermino.Vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        objtransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "UPDATE"

    ''' <summary>
    ''' Responsável por fazer a atualização do Valor de Termino de Medio de Pago no DB.
    ''' </summary>
    ''' <param name="objValorTerminoMedioPago"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 26/02/2009 Created
    ''' </history>
    Public Shared Sub ActualizarValorTerminoMedioPago(objValorTerminoMedioPago As ContractoServicio.MedioPago.SetMedioPago.ValorTermino, _
                                                      codigoUsuario As String, _
                                                      oidTermino As String, _
                                                      ByRef objtransacion As Transacao)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder
        query.Append("UPDATE gepr_tvalor_termino_medio_pago SET ")

        ' adicionar campos
        query.Append(Util.AdicionarCampoQuery("des_valor = []des_valor,", "des_valor", comando, objValorTerminoMedioPago.Descripcion, ProsegurDbType.Descricao_Longa))
        query.Append(Util.AdicionarCampoQuery("bol_vigente = []bol_vigente,", "bol_vigente", comando, objValorTerminoMedioPago.Vigente, ProsegurDbType.Logico))

        query.Append("cod_usuario = []cod_usuario, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))

        query.Append("fyh_actualizacion = []fyh_actualizacion ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "fyh_actualizacion", ProsegurDbType.Data, DateTime.Now))

        ' adicionar clausula where
        query.Append("WHERE cod_valor = []cod_valor AND oid_termino = []oid_termino ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "cod_valor", ProsegurDbType.Identificador_Alfanumerico, objValorTerminoMedioPago.Codigo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "oid_termino", ProsegurDbType.Objeto_Id, oidTermino))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        objtransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

End Class