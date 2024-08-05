Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Proceso
Imports Prosegur.Genesis

Public Class AgrupacionParametro

#Region "Consulta"
    Public Shared Function ObterOIDAgrupacionParametro(codigoAplicacion As String, codigoNivel As String, descripcionCortoParametro As String) As String
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ObterOIDAgrupacionParametro.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Descricao_Curta, descripcionCortoParametro))
        comando.CommandText = Util.PrepararQuery(query.ToString)
        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)
        Return result
    End Function

    Public Shared Sub BajarAgrupacion(OIDAgrupacion As String)

        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_PARAMETRO", ProsegurDbType.Objeto_Id, OIDAgrupacion))
        comando.CommandText = Util.PrepararQuery(My.Resources.BajarAgrupacion.ToString)
        comando.CommandType = CommandType.Text

        ' adicionar item para transação
        objtransacion.AdicionarItemTransacao(comando)

        ' realiza a transação
        objtransacion.RealizarTransacao()

    End Sub

    Public Shared Function GetAgrupaciones(codigoAplicacion As String, codigoNivel As String, descripcionAgrupacion As String, permisos As List(Of String), Optional aplicaciones As List(Of ContractoServicio.Aplicacion) = Nothing) As ContractoServicio.Parametro.GetAgrupaciones.AgrupacionColeccion

        ' criar objeto Nivel coleccion
        Dim objAgrupaciones As New ContractoServicio.Parametro.GetAgrupaciones.AgrupacionColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetAgrupaciones.ToString)

        ' adicionar filtros
        Dim filtro As New StringBuilder
        filtro.Append(" WHERE 1=1 ")

        If Not String.IsNullOrEmpty(codigoAplicacion) Then
            If aplicaciones IsNot Nothing AndAlso aplicaciones.Count > 0 AndAlso aplicaciones.FirstOrDefault(Function(x) x.CodigoAplicacion = codigoAplicacion) Is Nothing Then
                Return objAgrupaciones
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

        If Not String.IsNullOrEmpty(descripcionAgrupacion) Then
            filtro.Append(" AND upper(GEPR_TGRUPO_PARAMETRO.DES_DESCRIPCION_CORTO) LIKE []DES_DESCRIPCION_CORTO ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Identificador_Alfanumerico, "%" & descripcionAgrupacion.Trim().ToUpper() & "%"))
        End If

        query.Append(filtro)
        query.Append(" ORDER BY GEPR_TAPLICACION.COD_APLICACION , GEPR_TNIVEL_PARAMETRO.NEC_ORDEN , GEPR_TGRUPO_PARAMETRO.NEC_ORDEN ")

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
                objAgrupaciones.Add(PopularAgrupaciones(dr))
            Next
        End If
        ' retornar coleção de Nivel
        Return objAgrupaciones
    End Function

    ''' <summary>
    ''' Obtém a agrupaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function GetAgrupacionDetail(codigoAplicacion As String, codigoNivel As String, descripcionAgrupacion As String) As ContractoServicio.Parametro.GetAgrupacionDetail.Agrupacion

        ' criar objeto agrupaciones
        Dim objAgrupacion As New ContractoServicio.Parametro.GetAgrupacionDetail.Agrupacion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetAgrupacionDetail.ToString())

        ' criar filtro IN
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Descricao_Curta, descripcionAgrupacion))
        ' preparar a query
        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            For Each dr As DataRow In dtQuery.Rows
                objAgrupacion = ObterAgrupacionDetail(dr)
            Next

        End If

        ' retornar objeto preenchido
        Return objAgrupacion

    End Function

    Private Shared Function PopularAgrupaciones(dr As DataRow) As ContractoServicio.Parametro.GetAgrupaciones.Agrupacion
        ' criar objeto aplicacion
        Dim objAgrupacion As New ContractoServicio.Parametro.GetAgrupaciones.Agrupacion
        Util.AtribuirValorObjeto(objAgrupacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.DescripcionNivel, dr("DES_NIVEL_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.DescripcionCorta, dr("DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.NecOrden, dr("NEC_ORDEN"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(String))
        Return objAgrupacion
    End Function

    Private Shared Function ObterAgrupacionDetail(dr As DataRow) As ContractoServicio.Parametro.GetAgrupacionDetail.Agrupacion

        ' criar objeto agrupacion
        Dim objAgrupacion As New ContractoServicio.Parametro.GetAgrupacionDetail.Agrupacion

        Util.AtribuirValorObjeto(objAgrupacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.DescripcionCorto, dr("DES_DESCRIPCION_CORTO"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.DescripcionLarga, dr("DES_DESCRIPCION_LARGA"), GetType(String))
        Util.AtribuirValorObjeto(objAgrupacion.NecOrden, dr("NEC_ORDEN"), GetType(Integer))
        Return objAgrupacion

    End Function

    Public Shared Sub AltaAgrupacionParametro(codigoAgrupacion As String, oidAplicacion As String, descripcionCorto As String, descripcionlarga As String, codigoUsuario As String, necOrden As Integer, oidNivel As String)

        Try
            ' criar transação
            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(AccesoDatos.Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.AltaAgrupacionParametro)
            comando.CommandType = CommandType.Text

            Dim oidAgrupacion As String = Guid.NewGuid.ToString
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Descricao_Curta, descripcionCorto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_LARGA", ProsegurDbType.Descricao_Longa, descripcionlarga))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Inteiro_Longo, necOrden))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Objeto_Id, oidAplicacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, oidAgrupacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, oidNivel))

            ' adicionar item para transação
            objtransacion.AdicionarItemTransacao(comando)

            ' realiza a transação
            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("030_msg_Erro_UKAgrupacion"))
        End Try

    End Sub

    Public Shared Sub ActualizarAgrupacionParametro(oidAgrupacion As String, descripcionLarga As String, codigoUsuario As String, necOrden As Nullable(Of Integer))
        Try
            ' criar transação
            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)
            Dim query As New StringBuilder()
            query.Append("UPDATE GEPR_TGRUPO_PARAMETRO ")
            query.Append(" SET ")

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

            If Not String.IsNullOrEmpty(descripcionLarga) Then
                query.Append(" DES_DESCRIPCION_LARGA =  []DES_DESCRIPCION_LARGA , ")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_LARGA", ProsegurDbType.Objeto_Id, descripcionLarga))
            End If

            If necOrden.HasValue Then
                query.Append(" NEC_ORDEN =  []NEC_ORDEN,")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEC_ORDEN", ProsegurDbType.Identificador_Alfanumerico, necOrden.Value))
            End If

            query.Append(" COD_USUARIO =  []COD_USUARIO, FYH_ACTUALIZACION =  []FYH_ACTUALIZACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            query.Append(" WHERE OID_GRUPO_PARAMETRO =  []OID_GRUPO_PARAMETRO")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_GRUPO_PARAMETRO", ProsegurDbType.Objeto_Id, oidAgrupacion))

            comando.CommandText = Util.PrepararQuery(query.ToString())
            comando.CommandType = CommandType.Text
            ' adicionar item para transação
            objtransacion.AdicionarItemTransacao(comando)

            ' realiza a transação
            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("029_msg_Erro_UKPuesto"))
        End Try
    End Sub

#End Region

#Region "Inserir"

#End Region

#Region "Validar"

    Public Shared Function ValidarAgrupacionConParametroAsociado(codigoAplicacion As String, codigoNivel As String, descripcionCortoParametro As String) As Boolean
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ValidarAgrupacionConParametroAsociado.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoNivel))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_DESCRIPCION_CORTO", ProsegurDbType.Descricao_Curta, descripcionCortoParametro))
        comando.CommandText = Util.PrepararQuery(query.ToString)
        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)
        Return Not String.IsNullOrEmpty(result)
    End Function

#End Region
End Class
