Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class TipoSubCliente

#Region "[CONSULTAS]"

    ''' <summary>
    ''' OBTÉM TODOS OS TIPOS SUB CLIENTES
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Shared Function getTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTipoSubCliente)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(Peticion.codTipoSubcliente) OrElse _
            Not String.IsNullOrEmpty(Peticion.desTipoSubcliente) OrElse _
            Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " WHERE "
        End If

        If Not String.IsNullOrEmpty(Peticion.codTipoSubcliente) Then
            comando.CommandText &= " UPPER(COD_TIPO_SUBCLIENTE) = :COD_TIPO_SUBCLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoSubcliente.ToUpper))
        End If

        If Not String.IsNullOrEmpty(Peticion.desTipoSubcliente) AndAlso String.IsNullOrEmpty(Peticion.codTipoSubcliente) Then
            comando.CommandText &= " UPPER(DES_TIPO_SUBCLIENTE) LIKE :DES_TIPO_SUBCLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & Peticion.desTipoSubcliente.ToUpper & "%"))
        ElseIf Not String.IsNullOrEmpty(Peticion.desTipoSubcliente) Then
            comando.CommandText &= " AND UPPER(DES_TIPO_SUBCLIENTE) LIKE :DES_TIPO_SUBCLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, "%" & Peticion.desTipoSubcliente.ToUpper & "%"))
        End If

        If (Not String.IsNullOrEmpty(Peticion.codTipoSubcliente) OrElse Not String.IsNullOrEmpty(Peticion.desTipoSubcliente)) AndAlso _
            Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        ElseIf Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        End If

        Dim objTipoCliente As New ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion

        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, Peticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        If dt.Rows.Count > 0 AndAlso _
            dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                objTipoCliente.Add(PopularTipoSubCliente(dr))
            Next
        End If

        Return objTipoCliente
    End Function

    ''' <summary>
    ''' Obtém todas tipos de subcliente ativos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function GetComboTiposSubCliente() As ContractoServicio.Utilidad.GetComboTiposSubCliente.TipoSubClienteColeccion

        'cria commando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboTiposSubCliente)
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornoTipoSubCliente As New ContractoServicio.Utilidad.GetComboTiposSubCliente.TipoSubClienteColeccion

        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                objRetornoTipoSubCliente.Add(PopulaComboTipoSubCliente(dr))
            Next
        End If
        Return objRetornoTipoSubCliente
    End Function

    ''' <summary>
    ''' Verifica se o tipo sub cliente esta sendo usando em GEPR_TSUBCLIENTE
    ''' </summary>
    ''' <param name="OidSubCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 16/04/2013 Criado
    ''' </history>
    Public Shared Function VerificaSubTipoCliente(OidSubCliente As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaUtilizacaoSubCliente)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, OidSubCliente))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function VerificarTipoSubCliente(codTipoSubCliente As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaTipoSubClienteExiste)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, codTipoSubCliente))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function BuscaTipoSubClientePorCodigo(codSubCliente As String) As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTipoSubClientePorCodigo)
        comando.CommandType = CommandType.Text
        Dim tipoSubCliente As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente = Nothing

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, codSubCliente))

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            tipoSubCliente = PopularTipoSubCliente(dtQuery.Rows(0))
        End If

        Return tipoSubCliente
        
    End Function

    ''' <summary>
    ''' Verifica se o tipo sub cliente esta sendo usando em GEPR_TPROCEDENCIA
    ''' </summary>
    ''' <param name="oidTipoSubcliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/06/2013 Criado
    ''' </history>
    Public Shared Function VerificaTipoSubClienteProcedencia(oidTipoSubcliente As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaTipoSubClienteProcedencia)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidTipoSubcliente))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True

    End Function


#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' INSERE OS DADOS DE TIPOS SUB CLIENTES
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Shared Function setTiposSubclientes(Peticion As ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.insertTipoSubCliente)
        comando.CommandType = CommandType.Text

        Dim oidTipoSubCliente As String = Guid.NewGuid.ToString()

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_SUBCLIENTE", ProsegurDbType.Objeto_Id, oidTipoSubCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoSubcliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SUBCLIENTE", ProsegurDbType.Descricao_Longa, Peticion.desTipoSubcliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, Peticion.desUsuarioModificacion))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return Peticion.codTipoSubcliente
    End Function

#End Region

#Region "[ATUALIZAR]"

    ''' <summary>
    ''' ATUALIZA OS TIPOS SUB CLIENTES
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Shared Function AtualizaSubCliente(Peticion As ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaTipoSubcliente)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_SUBCLIENTE", ProsegurDbType.Descricao_Curta, Peticion.codTipoSubcliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_SUBCLIENTE", ProsegurDbType.Descricao_Longa, Peticion.desTipoSubcliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Descricao_Curta, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Descricao_Curta, Peticion.desUsuarioModificacion))
        
        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return Peticion.codTipoSubcliente
    End Function

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Preenche a coleção de tipo de subcliente através do DataTable.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Shared Function PopulaComboTipoSubCliente(dr As DataRow) As Utilidad.GetComboTiposSubCliente.TipoSubCliente

        Dim objTipoSubCliente As New ContractoServicio.Utilidad.GetComboTiposSubCliente.TipoSubCliente

        Util.AtribuirValorObjeto(objTipoSubCliente.Oid, dr("OID_TIPO_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Codigo, dr("COD_TIPO_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Descripcion, dr("DES_TIPO_SUBCLIENTE"), GetType(String))

        Return objTipoSubCliente

    End Function

    ''' <summary>
    ''' POPULA OS TIPOS SUB CLIENTES
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Private Shared Function PopularTipoSubCliente(dr As DataRow) As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente

        Dim objTipoSubCliente As New ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente

        Util.AtribuirValorObjeto(objTipoSubCliente.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTipoSubCliente.codTipoSubcliente, dr("COD_TIPO_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.desTipoSubcliente, dr("DES_TIPO_SUBCLIENTE"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.gmtCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTipoSubCliente.gmtModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTipoSubCliente.oidTipoSubcliente, dr("OID_TIPO_SUBCLIENTE"), GetType(String))

        Return objTipoSubCliente
    End Function

#End Region
End Class
