Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DBHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Classe SubCanal
''' </summary>
''' <remarks></remarks>
''' <history>
''' [anselmo.gois] 14/01/2009 Created
''' </history>
Public Class SubCanal

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
    ''' Função PesquisarSubCanal, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function GetSubCanalesByCanal(objpeticion As ContractoServicio.Canal.GetSubCanalesByCanal.Peticion) As ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = My.Resources.GetSubCanalesByCanal.ToString()

        Dim filtro As New Text.StringBuilder

        filtro.Append(Util.MontarClausulaIn(objpeticion.codigoCanal, "COD_CANAL", comando, "WHERE", "CAN"))

        filtro.Append(" ORDER BY CAN.COD_CANAL ")

        comando.CommandText &= filtro.ToString()

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        Dim objCanales As New ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        'Percorre o dt e retorna uma coleção de canales.
        objCanales = RetornaSubCanalByCanal(dt)

        ' retornar objeto
        Return objCanales

    End Function

    ''' <summary>
    ''' Busca Todos os SubCanais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function BuscaTodosSubCanales(codigo As String) As ContractoServicio.Canal.SetCanal.SubCanalColeccion

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTodosSubCanales.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornaSubCanais As New ContractoServicio.Canal.SetCanal.SubCanalColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                ' adicionar para objeto
                objRetornaSubCanais.Add(PopularBuscaSubCanal(dr))
            Next
        End If
        Return objRetornaSubCanais
    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function PopularSubCanal(dr As DataRow) As ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

        Dim objSubcanal As New ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

        Util.AtribuirValorObjeto(objSubcanal.Codigo, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Descripcion, dr("DES_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Observaciones, dr("OBS_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Vigente, dr("BOL_VIGENTE_SUBCANAL"), GetType(Boolean))
        Util.AtribuirValorObjeto(objSubcanal.OidSubCanal, dr("OID_SUBCANAL"), GetType(String))
        objSubcanal.CodigosAjenos = CodigoAjeno.RecuperaCodigoAjenoBase(dr("OID_SUBCANAL").ToString())
        Return objSubcanal

    End Function

    ''' <summary>
    ''' Função PopularCanal cria e preenche um objeto 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function PopularBuscaSubCanal(dr As DataRow) As ContractoServicio.Canal.SetCanal.SubCanal

        Dim objSubcanal As New ContractoServicio.Canal.SetCanal.SubCanal

        Util.AtribuirValorObjeto(objSubcanal.Codigo, dr("COD_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Descripcion, dr("DES_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Observaciones, dr("OBS_SUBCANAL"), GetType(String))
        Util.AtribuirValorObjeto(objSubcanal.Vigente, dr("BOL_VIGENTE"), GetType(Boolean))

        Return objSubcanal

    End Function

    ''' <summary>
    ''' Verifica se o SubCanal possui processo vigente.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 14/01/2008 Created
    ''' </history>
    Public Shared Function VerificarSiPoseerProcesoVigente(codigo As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.SubCanalVerificaProcessoVigente.ToString())
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se o subcanal existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function VerificarCodigoSubCanal(codigo As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarCodigoSubCanal.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codigo))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se a descrição de subcanal informada existe no BD retornando verdadeiro ou falso.
    ''' </summary>
    ''' <param name="descricao"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function VerificarDescripcionSubCanal(descricao As String) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificarDescripcionSubCanal.ToString())

        ' setar parametros
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCANAL", ProsegurDbType.Descricao_Longa, descricao))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Retorna os subcanais dos canais informados
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/03/2009 Criado
    ''' </history>
    Public Shared Function GetComboSubcanalesByCanal(objPeticion As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion

        ' criar objeto coleçãode clientes
        Dim objColCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim filtro As New StringBuilder

        ' obter query
        comando.CommandText = My.Resources.GetComboSubCanalByCanal.ToString
        comando.CommandType = CommandType.Text

        'chama o metodo para montar a query
        filtro = MontaQueryGetComboSubCanalByCanal(objPeticion, comando)

        comando.CommandText = comando.CommandText & filtro.ToString

        comando.CommandText = Util.PrepararQuery(comando.CommandText)

        ' executar query
        Dim dtSubCanal As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        objColCanal = RetornaColecaoSubCanalbyCanal(dtSubCanal)

        Return objColCanal
    End Function

    ''' <summary>
    ''' Monta query metodo GetComboSubCanalByCanal
    ''' </summary>
    ''' <param name="peticion"></param>
    ''' <param name="comando"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function MontaQueryGetComboSubCanalByCanal(peticion As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion, ByRef comando As IDbCommand) As StringBuilder

        Dim filtro As New StringBuilder

        'monta a query de acordo com os parametros informados
        If peticion.Codigo IsNot Nothing _
        AndAlso peticion.Codigo.Count > 0 _
        AndAlso peticion.Codigo.Item(0).ToString <> String.Empty Then

            filtro.Append(Util.MontarClausulaIn(peticion.Codigo, "COD_CANAL", comando, "AND", "CAN"))

        End If

        Return filtro
    End Function

    ''' <summary>
    ''' Percorre o dt e retorna o cliente com seus subclientes e punto de servicio
    ''' </summary>
    ''' <param name="dtSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <sumary>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </sumary>
    Private Shared Function RetornaColecaoSubCanalbyCanal(dtSubCanal As DataTable) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion

        Dim objCanales As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.CanalColeccion
        Dim objsubCliente As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.SubCliente
        Dim codigoSubCliente As String = String.Empty

        'Verifica se o dt contem subcanal
        If dtSubCanal IsNot Nothing AndAlso dtSubCanal.Rows.Count > 0 Then

            objCanales = Canal.RetornaColCanal(dtSubCanal)

            Dim codCanal As New List(Of String)

            For Each sc As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Canal In objCanales

                sc.SubCanales = RetornaSubCanal(dtSubCanal, sc.Codigo, codCanal)

            Next

            codCanal = Nothing

        End If

        ' retornar objeto
        Return objCanales
    End Function

    ''' <summary>
    ''' Retorna uma coleção de punto servicio
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="codigo"></param>
    ''' <param name="codCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaSubCanal(dt As DataTable, codigo As String, ByRef codCanal As List(Of String)) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        Dim objSubCanal As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Dim objColSubCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        For Each dr As DataRow In dt.Rows

            If SelectColSubCanal(codCanal, dr("COD_SUBCANAL")) = False Then

                If codigo = dr("COD_CANAL") Then

                    If VerificaSubCanal(dr) Then

                        objSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                        objSubCanal = PopulaCanalGetComboCanales(dr)
                        objColSubCanal.Add(objSubCanal)
                        codCanal.Add(dr("COD_SUBCANAL"))

                    End If

                End If

            End If
        Next

        Return objColSubCanal
    End Function

    Public Shared Function GetDatosSubCanalDefecto() As List(Of String)

        ' criar objeto
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' preparar query
        comando.CommandText = Util.PrepararQuery(My.Resources.GetDatosSubCanalDefecto.ToString)
        comando.CommandType = CommandType.Text

        ' executar e obter registros
        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)
        Dim CodigosSubCanales As New List(Of String)

        ' caso tenha encontrado algum registro
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                CodigosSubCanales.Add(dr("cod_subcanal"))
            Next

        End If

        Return CodigosSubCanales
    End Function

    ''' <summary>
    ''' Função PesquisarSubCanal, faz a pesquisa e preenche do datatable
    ''' </summary>
    ''' <param name="objpeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Claudioniz.Pereira] 06/06/2013 Created
    ''' </history>
    Public Shared Function GetSubCanalesByCertificado(objpeticion As ContractoServicio.Canal.GetSubCanalesByCertificado.Peticion) As ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetSubCanalByCertificado.ToString)
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CERTIFICADO", ProsegurDbType.Identificador_Alfanumerico, objpeticion.codigoCertificado))
        comando.CommandType = CommandType.Text

        Dim objSubCanales As New ContractoServicio.Canal.GetSubCanalesByCertificado.SubCanalColeccion

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' preencher objeto de retorno
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objSubCanales = New ContractoServicio.Canal.GetSubCanalesByCertificado.SubCanalColeccion

            ' para cada item do datatable
            For Each dr As DataRow In dt.Rows

                ' criar subCanal
                Dim objSubCanal As New ContractoServicio.Canal.GetSubCanalesByCertificado.SubCanal
                Util.AtribuirValorObjeto(objSubCanal.CodigoSubCanal, dr("COD_SUBCANAL"), GetType(String))
                Util.AtribuirValorObjeto(objSubCanal.Descripcion, dr("DES_SUBCANAL"), GetType(String))
                Util.AtribuirValorObjeto(objSubCanal.OidSubCanal, dr("OID_SUBCANAL"), GetType(String))

                ' adicionar SubCanal para colecao
                objSubCanales.Add(objSubCanal)
            Next

        End If

        Dim resposta As New ContractoServicio.Canal.GetSubCanalesByCertificado.Respuesta
        resposta.SubCanales = objSubCanales

        ' retornar coleção de subCanales
        Return resposta

    End Function

    ''' <summary>
    ''' Verifica se o subcanal tem algum punto de servicio.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/02/2009 Created
    ''' </history>
    Public Shared Function VerificaSubCanal(dr As DataRow) As Boolean
        If dr("COD_SUBCANAL") IsNot DBNull.Value Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Função responsável por fazer um select e verificar se o codigo informado existe na coleção retornando true or false.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/03/2009 Criado
    ''' </history>
    Private Shared Function SelectColSubCanal(objColPunto As List(Of String), codigo As String) As Boolean

        Dim retorno = From c In objColPunto Where c = codigo

        If retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If
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
    Public Shared Function PopulaCanalGetComboCanales(dr As DataRow) As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal

        Dim objSubCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal

        Util.AtribuirValorObjeto(objSubCanal.OidSubCanal, dr("OID_SUBCANAL"), GetType(String))

        Util.AtribuirValorObjeto(objSubCanal.Codigo, dr("COD_SUBCANAL"), GetType(String))

        Util.AtribuirValorObjeto(objSubCanal.Descripcion, dr("DES_SUBCANAL"), GetType(String))

        Return objSubCanal
    End Function

    ''' <summary>
    ''' Retorna uma coleção de canales.
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 12/02/2009 Criado
    ''' </history>
    Private Shared Function RetornaSubCanalByCanal(dt As DataTable) As ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        Dim objCanales As New ContractoServicio.Canal.GetSubCanalesByCanal.CanalColeccion

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            Dim codigoCanal As String = dt.Rows(0)("COD_CANAL")

            Dim objcanal As ContractoServicio.Canal.GetSubCanalesByCanal.Canal = Canal.PopularSubCanal(dt.Rows(0))
            objcanal.SubCanales = New ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion

            For Each dr As DataRow In dt.Rows

                ' Como a consulta retorna tanto os canais como os subcanais, por questão de performance
                ' precisamos saber se o canal corrente é igual ao anterior, se é diferente, adicionamos o canal
                ' na coleção de canais

                If dr("COD_CANAL") <> codigoCanal Then

                    objCanales.Add(objcanal)

                    ' Criar outro canal
                    objcanal = Canal.PopularSubCanal(dr)
                    objcanal.SubCanales = New ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion

                    If VerificaSubCanal(dr) Then
                        objcanal.SubCanales.Add(PopularSubCanal(dr))
                    End If

                Else
                    ' Se o canal é igual ao corrente, significa que a linha representa outro SubCanal
                    If VerificaSubCanal(dr) Then
                        objcanal.SubCanales.Add(PopularSubCanal(dr))
                    End If

                End If
            Next
            objCanales.Add(objcanal)
        End If

        ' retornar objeto
        Return objCanales
    End Function

    ''' <summary>
    ''' Retorna OidSubCanal
    ''' </summary>
    ''' <param name="codSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 23/03/2009 Created
    ''' </history>
    Public Shared Function BuscaOidSubCanal(codSubCanal As String) As String

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaOidSubCanal.ToString())

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, codSubCanal))

        Dim oidSubCanal As String = String.Empty

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dt.Rows.Count > 0 Then

            oidSubCanal = dt.Rows(0)("OID_SUBCANAL")

        End If

        ' retornar objeto
        Return oidSubCanal

    End Function

    ''' <summary>
    ''' Preencher Subcanal - GetProcesos
    ''' </summary>
    ''' <param name="Oid_Proceso_Servicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [rafael.nasorri] 31/03/2009 Criado
    ''' </history>
    Public Shared Function PopularSubCanal(Oid_Proceso_Servicio As String) As GetProcesos.SubcanalColeccion

        'Cria objetos 
        Dim objSubcanal As GetProcesos.Subcanal = Nothing
        Dim objSubcanalColeccion As GetProcesos.SubcanalColeccion = Nothing

        'Cria novo comando a partir da conexão
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Define texto do comando
        comando.CommandText = Util.PrepararQuery(My.Resources.GetSubcanalesPorProcesoServicio.ToString())

        'Adiciona parâmetros necessários
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PROCESO_POR_PSERVICIO", ProsegurDbType.Objeto_Id, Oid_Proceso_Servicio))

        'Preenche DataTable com o resultado da consulta
        Dim Subcanales As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If Subcanales IsNot Nothing AndAlso Subcanales.Rows.Count > 0 Then

            'Instancia objeto SubcanalColeccion
            objSubcanalColeccion = New GetProcesos.SubcanalColeccion

            'Para cada registro do DataTable
            For Each row As DataRow In Subcanales.Rows

                'Instancia objeto Subcanal
                objSubcanal = New GetProcesos.Subcanal

                'Preenche propriedades Subcanal
                With objSubcanal

                    Util.AtribuirValorObjeto(.Subcanal, row("COD_SUBCANAL"), GetType(String))

                    'Preenche coleção de medio pago
                    .MedioPago = AccesoDatos.ProcesoPorPServicio.PopularMedioPagoProcesoServicio( _
                        Oid_Proceso_Servicio, row("OID_SUBCANAL").ToString())

                End With

                'Adciona objeto Subcanal à coleção de Subcanal
                objSubcanalColeccion.Add(objSubcanal)

            Next

        End If

        Return objSubcanalColeccion

    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Responsável por inserir o SubCanal no DB.
    ''' </summary>
    ''' <param name="objSuBCanal"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function AltaSubCanal(objSuBCanal As ContractoServicio.Canal.SetCanal.SubCanal, _
                                   CodigoUsuario As String, _
                                   oidCanal As String, _
                                   ByRef objtransacion As Transacao) As String

        Dim OidSubCanal As String = String.Empty

        Try
            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            ' obter query
            comando.CommandText = Util.PrepararQuery(My.Resources.AltaSubCanal.ToString())
            comando.CommandType = CommandType.Text
            OidSubCanal = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, OidSubCanal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, objSuBCanal.Codigo))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CANAL", ProsegurDbType.Objeto_Id, oidCanal))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCANAL", ProsegurDbType.Descricao_Longa, objSuBCanal.Descripcion))
            If objSuBCanal.Observaciones <> String.Empty Then
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_SUBCANAL", ProsegurDbType.Observacao_Longa, objSuBCanal.Observaciones))
            Else
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_SUBCANAL", ProsegurDbType.Observacao_Longa, String.Empty))
            End If
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objSuBCanal.Vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            objtransacion.AdicionarItemTransacao(comando)

            'If objSuBCanal.CodigoAjeno IsNot Nothing Then
            '    For Each item In objSuBCanal.CodigoAjeno
            '        Dim codigoTablaGenesis As String
            '        item.OidTablaGenesis = OidSubCanal
            '        item.GmtModificacion = DateTime.Now
            '        item.GmtCreacion = DateTime.Now
            '        item.DesUsuarioCreacion = CodigoUsuario
            '        item.DesUsuarioModificacion = CodigoUsuario
            '        codigoTablaGenesis = item.CodTipoTablaGenesis
            '        item.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
            '                                    Where iten.CodTipoTablaGenesis = codigoTablaGenesis
            '                                    Select iten.Entidade).FirstOrDefault()
            '        CodigoAjeno.SetCodigosAjenos(item, objtransacion)
            '        codigoTablaGenesis = String.Empty
            '    Next
            'End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("001_msg_Erro_UKSubCanal"))
        End Try

        Return OidSubCanal

    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta Todos os SubCanais do Canal Informado
    ''' </summary>
    ''' <param name="oidCanal"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' [octavio.piramo] 10/03/2009 Alterado
    ''' </history>
    Public Shared Sub BajaSubCanalNivelCanal(oidCanal As String, _
                                             CodigoUsuario As String, _
                                             ByRef objTransacion As DbHelper.Transacao)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.BajaSubCanal.ToString())
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CANAL", ProsegurDbType.Objeto_Id, oidCanal))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ' adicionar item para transação
        objTransacion.AdicionarItemTransacao(comando)

    End Sub

#End Region

#Region "[UPDATE]"

    ''' <summary>
    ''' Responsável por fazer a atualização do SubCanais do DB.
    ''' </summary>
    ''' <param name="objSuBCanal"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 15/01/2009 Created
    ''' </history>
    Public Shared Function ActualizarSubCanal(objSuBCanal As ContractoServicio.Canal.SetCanal.SubCanal, _
                                         CodigoUsuario As String, _
                                         ByRef objtransacion As Transacao) As String

        Dim oidSubCanal As String = String.Empty


        Try

            ' criar comando
            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
            oidSubCanal = BuscaOidSubCanal(objSuBCanal.Codigo)

            ' obter query
            Dim query As New StringBuilder

            query.Append("UPDATE GEPR_TSUBCANAL SET ")

            query.Append("COD_USUARIO = []COD_USUARIO,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodigoUsuario))

            query.Append(" FYH_ACTUALIZACION = []FYH_ACTUALIZACION, ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            query.Append(" DES_SUBCANAL = []DES_SUBCANAL,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_SUBCANAL", ProsegurDbType.Descricao_Longa, objSuBCanal.Descripcion))

            query.Append(" OBS_SUBCANAL = []OBS_SUBCANAL,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_SUBCANAL", ProsegurDbType.Observacao_Longa, objSuBCanal.Observaciones))

            query.Append(" BOL_VIGENTE = []BOL_VIGENTE,")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, objSuBCanal.Vigente))

            query.Append(" COD_SUBCANAL = []COD_SUBCANAL")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCANAL", ProsegurDbType.Identificador_Alfanumerico, objSuBCanal.Codigo))

            ' adicionar clausula where
            query.Append(" WHERE OID_SUBCANAL = []OID_SUBCANAL")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, oidSubCanal))

            comando.CommandText = Util.PrepararQuery(query.ToString)
            comando.CommandType = CommandType.Text

            objtransacion.AdicionarItemTransacao(comando)

            'If objSuBCanal.CodigoAjeno IsNot Nothing Then
            '    For Each item In objSuBCanal.CodigoAjeno
            '        Dim codigoTablaGenesis As String
            '        item.GmtModificacion = objSuBCanal.gmtModificacion
            '        item.GmtCreacion = objSuBCanal.gmtCreacion
            '        item.DesUsuarioCreacion = objSuBCanal.desUsuarioCreacion
            '        item.DesUsuarioModificacion = objSuBCanal.desUsuarioModificacion
            '        codigoTablaGenesis = item.CodTipoTablaGenesis
            '        item.CodTipoTablaGenesis = (From iten In Constantes.MapeoEntidadesCodigoAjeno
            '                                    Where iten.CodTipoTablaGenesis = codigoTablaGenesis
            '                                    Select iten.Entidade).FirstOrDefault()
            '        CodigoAjeno.SetCodigosAjenos(item, objtransacion)
            '        codigoTablaGenesis = String.Empty
            '    Next
            'End If

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("001_msg_Erro_UKSubCanal"))
        End Try

        Return oidSubCanal
    End Function

#End Region

End Class
