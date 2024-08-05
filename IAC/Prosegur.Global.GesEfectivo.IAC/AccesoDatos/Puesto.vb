Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Genesis

Public Class Puesto

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém os postos
    ''' </summary>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/08/2011 Criado
    ''' </history>
    Public Shared Function GetPuestos(codigoDelegacion As String, codigoAplicacion As String, codigoPuesto As String, codigoHostPuesto As String, PuestoVigente As Nullable(Of Boolean), permisos As List(Of String), SoloPuestoMecanizado As Nullable(Of Boolean), Optional Aplicaciones As List(Of ContractoServicio.Aplicacion) = Nothing) As ContractoServicio.Puesto.GetPuestos.PuestoColeccion

        ' criar objeto Puesto coleccion
        Dim objPuestos As New ContractoServicio.Puesto.GetPuestos.PuestoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetPuestos.ToString)

        ' obter query

        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
        If Not String.IsNullOrEmpty(codigoAplicacion) Then

            If Aplicaciones IsNot Nothing AndAlso Aplicaciones.FirstOrDefault(Function(x) x.CodigoAplicacion = codigoAplicacion) Is Nothing Then
                Return objPuestos
            End If

            query.Append(" and GEPR_TAPLICACION.COD_APLICACION = []COD_APLICACION ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        Else
            If Aplicaciones IsNot Nothing Then
                query.Append(Util.MontarClausulaIn(Aplicaciones.Select(Function(A) A.CodigoAplicacion).ToList, "COD_APLICACION", comando, "and", String.Empty))
            ElseIf permisos IsNot Nothing AndAlso permisos.Count > 0 Then
                query.Append(Util.MontarClausulaIn(permisos, "COD_PERMISO", comando, "and", String.Empty))
            End If
        End If
        If Not String.IsNullOrEmpty(codigoPuesto) Then
            query.Append(" and GEPR_TPUESTO.COD_PUESTO = UPPER([]COD_PUESTO)  ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        End If
        If Not String.IsNullOrEmpty(codigoHostPuesto) Then
            query.Append(" and upper(GEPR_TPUESTO.COD_HOST_PUESTO) Like []COD_HOST_PUESTO  ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, "%" & codigoHostPuesto.Trim().ToUpper() & "%"))
        End If
        If PuestoVigente.HasValue Then
            query.Append(" and GEPR_TPUESTO.BOL_VIGENTE =  []BOL_VIGENTE ")
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, PuestoVigente.Value))
        End If






        comando.CommandText = Util.PrepararQuery(query.ToString)

        'Si el parámetro bolEsMecanizado esté true, deberá recuperar solamente los puestos que están marcados como mecanizado en el parámetro de puesto, 
        If SoloPuestoMecanizado.GetHashCode Then
            comando.CommandText = comando.CommandText.Replace("{0}", " INNER JOIN GEPR_TPARAMETRO_VALOR PV ON PV.COD_IDENTIFICADOR_NIVEL = GEPR_TPUESTO.OID_PUESTO INNER JOIN GEPR_TPARAMETRO P ON P.OID_PARAMETRO = PV.OID_PARAMETRO AND UPPER(P.COD_PARAMETRO) = 'ESMECANIZADO' AND PV.DES_VALOR_PARAMETRO = '1'")
        Else
            comando.CommandText = comando.CommandText.Replace("{0}", String.Empty)
        End If

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objPuestos.Add(PopularComboPuestos(dr))
            Next
        End If
        ' retornar coleção de aplicacion
        Return objPuestos
    End Function

    Public Shared Function GetPuestoDetail(codigoAplicacion As String, hostPuesto As String, codigoPuesto As String) As ContractoServicio.Puesto.GetPuestoDetail.Puesto

        ' criar objeto Puesto coleccion
        Dim objPuesto As ContractoServicio.Puesto.GetPuestoDetail.Puesto = Nothing

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetPuestoDetail.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, hostPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows
                ' adicionar para coleção
                objPuesto = PopularComboPuestosDetail(dr)
            Next
        End If
        ' retornar coleção de aplicacion
        Return objPuesto
    End Function

    Public Shared Sub AltaPuesto(codigoPuesto As String, oidAplicacion As String, codigoHostPuesto As String, vigente As Boolean, codigoUsuario As String, oidDelegacion As String)

        Try
            ' criar transação
            Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)

            Dim comando As IDbCommand = AcessoDados.CriarComando(AccesoDatos.Constantes.CONEXAO_GE)

            comando.CommandText = Util.PrepararQuery(My.Resources.AltaPuesto)
            comando.CommandType = CommandType.Text

            Dim oidPuesto As String = Guid.NewGuid.ToString

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PUESTO", ProsegurDbType.Objeto_Id, oidPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, oidDelegacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Objeto_Id, oidAplicacion))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoHostPuesto))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, vigente))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

            ' adicionar item para transação
            objtransacion.AdicionarItemTransacao(comando)

            ' realiza a transação
            objtransacion.RealizarTransacao()

        Catch ex As Exception
            Excepcion.Util.Tratar(ex, Traduzir("029_msg_Erro_UKPuesto"))
        End Try

    End Sub

    Public Shared Sub ActualizarPuesto(oidPuesto As String, codigoHostPuesto As String, vigente As Boolean, codigoUsuario As String)

        ' criar transação
        Dim objtransacion As New Transacao(AccesoDatos.Constantes.CONEXAO_GE)


        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Objeto_Id, codigoHostPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, vigente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, codigoUsuario))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PUESTO", ProsegurDbType.Identificador_Alfanumerico, oidPuesto))

        comando.CommandText = Util.PrepararQuery(My.Resources.ActualizarPuesto)
        comando.CommandType = CommandType.Text
        ' adicionar item para transação
        objtransacion.AdicionarItemTransacao(comando)

        ' realiza a transação
        objtransacion.RealizarTransacao()
    End Sub

    Private Shared Function PopularComboPuestosDetail(dr As DataRow) As ContractoServicio.Puesto.GetPuestoDetail.Puesto
        ' criar objeto aplicacion
        If dr.IsNull("COD_PUESTO") Then
            Return Nothing
        End If
        Dim objPuesto As New ContractoServicio.Puesto.GetPuestoDetail.Puesto
        Util.AtribuirValorObjeto(objPuesto.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.DescripcionDelegacion, dr("DES_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.CodigoPuesto, dr("COD_PUESTO"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.CodigoHostPuesto, dr("COD_HOST_PUESTO"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.PuestoVigente, dr("BOL_VIGENTE"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
        Return objPuesto
    End Function

    Private Shared Function PopularComboPuestos(dr As DataRow) As ContractoServicio.Puesto.GetPuestos.Puesto
        ' criar objeto aplicacion
        Dim objPuesto As New ContractoServicio.Puesto.GetPuestos.Puesto
        Util.AtribuirValorObjeto(objPuesto.CodigoDelegacion, dr("COD_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.DescripcionDelegacion, dr("DES_DELEGACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.CodigoPuesto, dr("COD_PUESTO"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.CodigoHostPuesto, dr("COD_HOST_PUESTO"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.PuestoVigente, dr("BOL_VIGENTE"), GetType(Boolean))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.CodigoAplicacion, dr("COD_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.DescripcionAplicacion, dr("DES_APLICACION"), GetType(String))
        Util.AtribuirValorObjeto(objPuesto.Aplicacion.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
        Return objPuesto
    End Function

    Public Shared Function ValidarPuestoExiste(hostPuesto As String) As Boolean

        ' criar objeto Puesto coleccion
        Dim objPuesto As New ContractoServicio.Puesto.GetPuestoDetail.Puesto

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.VerificarPuestoExiste.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, hostPuesto))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return Not String.IsNullOrEmpty(result)
    End Function

    Public Shared Function ValidarExistePuesto(codigoPuesto As String, codigoAplicacion As String, hostPuesto As String) As Boolean

        ' criar objeto Puesto coleccion
        Dim objPuesto As New ContractoServicio.Puesto.GetPuestoDetail.Puesto

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.VerificarPuestoExisteByAplicacionPuestoPlantaYHost.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, hostPuesto))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return Not String.IsNullOrEmpty(result)
    End Function

    Public Shared Function ObterOIDPuesto(codigoDelegacion As String, codigoAplicacion As String, codigoPuesto As String) As String

        ' criar objeto Puesto coleccion
        Dim objPuesto As New ContractoServicio.Puesto.GetPuestoDetail.Puesto

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ObterOIDPuesto.ToString)

        ' obter query
        comando.CommandType = CommandType.Text
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, codigoPuesto))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, codigoAplicacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, codigoDelegacion))
        comando.CommandText = Util.PrepararQuery(query.ToString)

        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)

        Return result
    End Function

    ''' <summary>
    ''' Verifica se um posto existe informando o codPuesto e o Host.
    ''' </summary>
    ''' <param name="CodPuesto"></param>
    ''' <param name="HostPuesto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/09/2011 - Criado
    ''' </history>
    Public Shared Function ValidarPuestoExiste(CodPuesto As String, HostPuesto As String, CodAplicacion As String) As Boolean

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.VerificarPuestoExistePorCodPuestoYCodHost.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodPuesto.ToUpper))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, HostPuesto.ToUpper))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_APLICACION", ProsegurDbType.Identificador_Alfanumerico, CodAplicacion.ToUpper))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere o posto no BD
    ''' </summary>
    ''' <param name="OidDelegacion"></param>
    ''' <param name="OidAplicacion"></param>
    ''' <param name="CodPuesto"></param>
    ''' <param name="HostPuesto"></param>
    ''' <param name="CodUsuario"></param>
    ''' <param name="ObjTransacion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/09/2011 - Criado
    ''' </history>
    Public Shared Function InserirPuesto(OidDelegacion As String, OidAplicacion As String, CodPuesto As String, HostPuesto As String, CodUsuario As String, ByRef ObjTransacion As Transacao) As String

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.InserirPuesto.ToString)
        cmd.CommandType = CommandType.Text

        Dim OidPuesto As String = Guid.NewGuid.ToString

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PUESTO", ProsegurDbType.Identificador_Alfanumerico, CodPuesto))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PUESTO", ProsegurDbType.Objeto_Id, OidPuesto))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_DELEGACION", ProsegurDbType.Objeto_Id, OidDelegacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_APLICACION", ProsegurDbType.Objeto_Id, OidAplicacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_HOST_PUESTO", ProsegurDbType.Identificador_Alfanumerico, HostPuesto))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_VIGENTE", ProsegurDbType.Logico, True))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, CodUsuario))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data, DateTime.Now))

        ObjTransacion.AdicionarItemTransacao(cmd)

        Return OidPuesto
    End Function

#End Region

End Class