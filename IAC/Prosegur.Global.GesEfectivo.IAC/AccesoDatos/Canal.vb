Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports System.Data

Public Class Canal

#Region "[CONSTRUTORES]"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Função Selecionar, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' [pgoncalves] 13/05/2013 Alterado
    ''' </history>
    Public Shared Function GetCanales(objPeticion As ContractoServicio.Canal.GetCanales.Peticion) As ContractoServicio.Canal.GetCanales.CanalColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetCanales.ToString()

        Dim filtros As New System.Text.StringBuilder

        filtros.Append(MontaClausulaCanal(objPeticion, comando))

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaCanais As New ContractoServicio.Canal.GetCanales.CanalColeccion

        'Percorre o dt e retorna uma coleção de canais.
        objRetornaCanais = RetornaColecaoCanais(dt)

        ' retornar objeto
        Return objRetornaCanais

    End Function

    ''' <summary>
    ''' Função PopularCanalbyCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function PopularSubCanal(dr As DataRow) As ContractoServicio.Canal.GetSubCanalesByCanal.Canal

        Dim objcanal As New ContractoServicio.Canal.GetSubCanalesByCanal.Canal

        Util.AtribuirValorObjeto(objcanal.OidCanal, dr("OID_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.codigo, dr("COD_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.descripcion, dr("DES_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.observaciones, dr("OBS_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objcanal.FyhActualizacion, dr("FYH_ACTUALIZACION"), GetType(Date))
        Util.AtribuirValorObjeto(objcanal.CodigoUsuario, dr("COD_USUARIO"), GetType(String))
        objcanal.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_CANAL").ToString())
        ' retornar objeto
        Return objcanal

    End Function

    ''' <summary>
    ''' Busca o oid do Canal
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function BuscaOidCanal(codigo As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidCanal.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim oid As String = String.Empty

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            oid = dt.Rows(0)("OID_CANAL").ToString
        End If

        Return oid

    End Function

    ''' <summary>
    ''' Faz a verificação se o canal possui algum processo vigente.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function verificarSiPoseeProcesoVigente(codigo As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.CanalVerificaProcesoVigente.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se o canal existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function VerificarCodigoCanal(codigo As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidCanal.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se a descrição informada existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="descricao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function VerificarDescripcionCanal(descricao As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionCanal.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CANAL", ProsegurDbType.Descricao_Longa, descricao))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Retorna todos os canais vigentes 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/02/2009 Criado
    ''' </history>
    Public Shared Function GetComboCanales() As ContractoServicio.Utilidad.GetComboCanales.CanalColeccion

        ' criar objeto cliente
        Dim objColCanal As New ContractoServicio.Utilidad.GetComboCanales.CanalColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboCanales.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtCliente As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Retorna coleção de canais
        objColCanal = PercorreDtCanal(dtCliente)

        ' retornar coleção de termino
        Return objColCanal
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna todos os clientes vigentes
    ''' </summary>
    ''' <param name="dtCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </sumary>
    Private Shared Function PercorreDtCanal(dtCliente As DataTable) As ContractoServicio.Utilidad.GetComboCanales.CanalColeccion

        Dim objColCanal As New ContractoServicio.Utilidad.GetComboCanales.CanalColeccion

        If dtCliente IsNot Nothing _
            AndAlso dtCliente.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtCliente.Rows
                ' adicionar para coleção
                objColCanal.Add(PopulaCanalGetComboCanales(dr))
            Next

        End If

        Return objColCanal
    End Function

    ''' <summary>
    ''' Popula Canal GetComboCanales
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaCanalGetComboCanales(dr As DataRow) As ContractoServicio.Utilidad.GetComboCanales.Canal

        Dim objCanal As New ContractoServicio.Utilidad.GetComboCanales.Canal

        Util.AtribuirValorObjeto(objCanal.Codigo, dr("COD_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objCanal.Identificador, dr("OID_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objCanal.Descripcion, dr("DES_CANAL"), GetType(String))

        Return objCanal
    End Function

    ''' <summary>
    ''' Popula Canal GetComboCanales
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaCanalByGetComboSubCanalbyCanal(dr As DataRow) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal

        Dim objCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal

        Util.AtribuirValorObjeto(objCanal.Codigo, dr("COD_CANAL"), GetType(String))

        Return objCanal
    End Function

    ''' <summary>
    ''' Retorna uma coleção de subclientes
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> 
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function RetornaColCanal(dt As DataTable) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion

        Dim objColCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion
        Dim objCanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal
        Dim codigoCanal As String = String.Empty

        For Each dr As DataRow In dt.Rows

            codigoCanal = dr("COD_CANAL")

            If SelectColCanal(objColCanal, codigoCanal) = False Then
                objCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal
                objCanal = PopulaGetComboSubcanalesByCanal(dr)
                objColCanal.Add(objCanal)
            End If

        Next

        Return objColCanal
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function SelectColCanal(objCanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion, codigo As String) As Boolean

        Dim retorno = From c In objCanal Where c.Codigo = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Popula SubCanal GetComboSubcanalesByCanal
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Public Shared Function PopulaGetComboSubcanalesByCanal(dr As DataRow) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal

        Dim objCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal

        Util.AtribuirValorObjeto(objCanal.Codigo, dr("COD_CANAL"), GetType(String))

        Return objCanal
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna uma coleção de canais
    ''' </summary>
    ''' <param name="dtCanais"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaColecaoCanais(dtCanais As DataTable) As ContractoServicio.Canal.GetCanales.CanalColeccion

        Dim objRetornaCanais As New ContractoServicio.Canal.GetCanales.CanalColeccion

        If dtCanais IsNot Nothing AndAlso dtCanais.Rows.Count > 0 Then

            Dim objCanal As ContractoServicio.Canal.GetCanales.Canal = Nothing

            For Each dr As DataRow In dtCanais.Rows
                objCanal = New ContractoServicio.Canal.GetCanales.Canal()

                Util.AtribuirValorObjeto(objCanal.codigo, dr("COD_CANAL"), GetType(String))
                Util.AtribuirValorObjeto(objCanal.descripcion, dr("DES_CANAL"), GetType(String))
                Util.AtribuirValorObjeto(objCanal.observaciones, dr("OBS_CANAL"), GetType(String))
                Util.AtribuirValorObjeto(objCanal.vigente, dr("BOL_VIGENTE"), GetType(Boolean))
                Util.AtribuirValorObjeto(objCanal.FyhActualizacion, dr("FYH_ACTUALIZACION"), GetType(Date))
                Util.AtribuirValorObjeto(objCanal.CodigoUsuario, dr("COD_USUARIO"), GetType(String))

                objCanal.CodigoAjeno = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                objCanal.CodigoAjeno = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_CANAL").ToString())

                ' adicionar para objeto
                objRetornaCanais.Add(objCanal)
            Next
        End If

        Return objRetornaCanais
    End Function

    ''' <summary>
    ''' Monta Query Canal
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Criado
    ''' </history>
    Private Shared Function MontaClausulaCanal(objpeticion As ContractoServicio.Canal.GetCanales.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtros As New System.Text.StringBuilder

        filtros.Append(" WHERE CAN.BOL_VIGENTE = []BOL_VIGENTE")

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objpeticion.bolVigente))

        ' setar parametros
        filtros.Append(Util.MontarClausulaLikeUpper(objpeticion.codigoCanal, "COD_CANAL", comando, "AND", "CAN"))

        filtros.Append(Util.MontarClausulaLikeUpper(objpeticion.descripcionCanal, "DES_CANAL", comando, "AND", "CAN"))

        If (filtros.Length > 0) Then
            comando.CommandText &= filtros.ToString & " ORDER BY CAN.COD_CANAL"
        End If

        Return filtros
    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o canal no DB.
    ''' </summary>
    ''' <param name="objCanal"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function AltaCanal(objCanal As ContractoServicio.Canal.SetCanal.Canal, codigoUsuario As String,
                                     ByRef retornoOidSubCanal As List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal)),
                                            ByRef objTransacion As Transacao) As String
        Dim objRespuesta As New ContractoServicio.Canal.SetCanal.RespuestaCanal
        Dim oidCanal As String

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaCanal.ToString())
            comando.CommandType = CommandType.Text

            oidCanal = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CANAL", ProsegurDbType.Objeto_Id, oidCanal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, objCanal.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CANAL", ProsegurDbType.Descricao_Longa, objCanal.Descripcion))
            If objCanal.Observaciones <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_CANAL", ProsegurDbType.Observacao_Longa, objCanal.Observaciones))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_CANAL", ProsegurDbType.Observacao_Longa, String.Empty))
            End If
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objCanal.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objTransacion.AdicionarItemTransacao(comando)

            'If objCanal.CodigoAjeno IsNot Nothing Then
            '    For Each item In objCanal.CodigoAjeno
            '        Dim codigoTipoTabla As String
            '        item.OidTablaGenesis = oidCanal
            '        item.GmtModificacion = objCanal.gmtModificacion
            '        item.GmtCreacion = objCanal.gmtCreacion
            '        item.DesUsuarioCreacion = objCanal.desUsuarioCreacion
            '        item.DesUsuarioModificacion = objCanal.desUsuarioModificacion
            '        codigoTipoTabla = item.CodTipoTablaGenesis
            '        item.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
            '                                    Where iten.CodTipoTablaGenesis = codigoTipoTabla
            '                                    Select iten.Entidade).FirstOrDefault()
            '        CodigoAjeno.SetCodigosAjenos(item, objTransacion)
            '    Next
            'End If

            If objCanal.SubCanales IsNot Nothing Then
                If retornoOidSubCanal Is Nothing Then
                    retornoOidSubCanal = New List(Of KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal))
                End If
                For Each sc As ContractoServicio.Canal.SetCanal.SubCanal In objCanal.SubCanales
                    Dim oid = SubCanal.AltaSubCanal(sc, codigoUsuario, oidCanal, objTransacion)
                    Dim item As New KeyValuePair(Of String, ContractoServicio.Canal.SetCanal.SubCanal)(oid, sc)
                    retornoOidSubCanal.Add(item)
                Next
            End If

            'objTransacion.RealizarTransacao()
            Return oidCanal
        Catch ex As Exception

            If ex.Message.Contains("AK_AK_GEPR_TSUBCANAL__GEPR_TSU") OrElse ex.Message.Contains("AK_GEPR_TSUBCANAL_1") Then
                Excepcion.Util.Tratar(ex, Traduzir("001_msg_Erro_UKSubCanal"))
            ElseIf ex.Message.Contains("AK_AK_GEPR_TCANAL_1_GEPR_TCA") OrElse ex.Message.Contains("AK_AK_GEPR_TCANAL_2_GEPR_TCA") Then
                Excepcion.Util.Tratar(ex, Traduzir("001_msg_Erro_UKCanal"))
            Else
                Excepcion.Util.Tratar(ex, String.Empty)
            End If

        End Try

        Return Nothing
    End Function

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do Canal do DB.
    ''' </summary>
    ''' <param name="objCanal"></param>
    ''' <param name="codigoUsuario"></param>
    ''' <param name="objtransacion"></param>
    ''' <param name="oidCanal"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Shared Sub ActualizarCanal(objCanal As ContractoServicio.Canal.SetCanal.Canal, _
                                      codigoUsuario As String, ByRef objtransacion As Transacao, _
                                      oidCanal As String)
        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter query
            Dim query As New StringBuilder
            query.Append("UPDATE GEPR_TCANAL SET")

            query.Append(Util.AdicionarCampoQuery(" COD_USUARIO = []COD_USUARIO,", "COD_USUARIO", comando, codigoUsuario, ProsegurDbType.Identificador_Alfanumerico))
            query.Append(Util.AdicionarCampoQuery(" FYH_ACTUALIZACION = []FYH_ACTUALIZACION,", "FYH_ACTUALIZACION", comando, DateTime.Now, ProsegurDbType.Data))
            query.Append(Util.AdicionarCampoQuery(" DES_CANAL = []DES_CANAL,", "DES_CANAL", comando, objCanal.Descripcion, ProsegurDbType.Descricao_Longa))
            query.Append(Util.AdicionarCampoQuery(" OBS_CANAL = []OBS_CANAL,", "OBS_CANAL", comando, objCanal.Observaciones, ProsegurDbType.Observacao_Longa))
            query.Append(Util.AdicionarCampoQuery(" BOL_VIGENTE = []BOL_VIGENTE", "BOL_VIGENTE", comando, objCanal.Vigente, ProsegurDbType.Logico))

            ' adicionar clausula where
            query.Append(" WHERE OID_CANAL = []OID_CANAL")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CANAL", ProsegurDbType.Objeto_Id, oidCanal))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            ' adicionar comando para transação
            objtransacion.AdicionarItemTransacao(comando)

            'If objCanal.CodigoAjeno IsNot Nothing Then
            '    For Each item In objCanal.CodigoAjeno
            '        Dim codigoTipoTabla As String
            '        item.OidTablaGenesis = oidCanal
            '        item.GmtModificacion = objCanal.gmtModificacion
            '        item.GmtCreacion = objCanal.gmtCreacion
            '        item.DesUsuarioCreacion = objCanal.desUsuarioCreacion
            '        item.DesUsuarioModificacion = objCanal.desUsuarioModificacion
            '        codigoTipoTabla = item.CodTipoTablaGenesis
            '        item.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
            '                                    Where iten.CodTipoTablaGenesis = codigoTipoTabla
            '                                    Select iten.Entidade).FirstOrDefault()
            '        CodigoAjeno.SetCodigosAjenos(item, objtransacion)
            '    Next
            'End If

        Catch ex As Exception
            ' Utilizado para tratar o erro de Unique Constrant
            Excepcion.Util.Tratar(ex, Traduzir("001_msg_Erro_UKCanal"))
        End Try

    End Sub

#End Region

    'Retirar da Classe

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Private Shared Function PopularCanal(dr As DataRow) As ContractoServicio.Canal.GetCanales.Canal
        Dim objcanal As New ContractoServicio.Canal.GetCanales.Canal

        Util.AtribuirValorObjeto(objcanal.codigo, dr("COD_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.descripcion, dr("DES_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.observaciones, dr("OBS_CANAL"), GetType(String))
        Util.AtribuirValorObjeto(objcanal.vigente, dr("BOL_VIGENTE"), GetType(Boolean))
        ' retornar objeto
        Return objcanal

    End Function

End Class