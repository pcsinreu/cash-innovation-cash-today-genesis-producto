Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class TipoProcedencia

#Region "[CONSULTAS]"

    ''' <summary>
    ''' OBTÉM TODOS OS TIPOS PROCEDENCIA
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    Public Shared Function getTiposProcedencia(objPeticion As ContractoServicio.TipoProcedencia.GetTiposProcedencias.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedenciaColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTiposProcedencias)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPeticion.codTipoProcedencia) OrElse _
            Not String.IsNullOrEmpty(objPeticion.desTipoProcedencia) OrElse _
            objPeticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " WHERE"
        End If

        If Not String.IsNullOrEmpty(objPeticion.codTipoProcedencia) Then
            comando.CommandText &= " UPPER(COD_TIPO_PROCEDENCIA) = :COD_TIPO_PROCEDENCIA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.codTipoProcedencia.ToUpper))
        End If

        If Not String.IsNullOrEmpty(objPeticion.desTipoProcedencia) AndAlso String.IsNullOrEmpty(objPeticion.codTipoProcedencia) Then
            comando.CommandText &= " UPPER(DES_TIPO_PROCEDENCIA) LIKE :DES_TIPO_PROCEDENCIA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.desTipoProcedencia.ToUpper & "%"))
        ElseIf Not String.IsNullOrEmpty(objPeticion.desTipoProcedencia) Then
            comando.CommandText &= " AND UPPER(DES_TIPO_PROCEDENCIA) LIKE :DES_TIPO_PROCEDENCIA "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, "%" & objPeticion.desTipoProcedencia.ToUpper & "%"))
        End If

        If (Not String.IsNullOrEmpty(objPeticion.codTipoProcedencia) OrElse Not String.IsNullOrEmpty(objPeticion.desTipoProcedencia)) AndAlso _
            objPeticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
        ElseIf objPeticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
        End If

        comando.CommandText &= " ORDER BY COD_TIPO_PROCEDENCIA "

        Dim objTipoProcedenciaCol As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedenciaColeccion

        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        If dt.Rows.Count > 0 AndAlso _
            dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                objTipoProcedenciaCol.Add(PopularTipoProcedencia(dr))
            Next
        End If

        Return objTipoProcedenciaCol

    End Function

    ''' <summary>
    ''' Obtém todas tipos de procedencia ativos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function GetComboTiposProcedencia() As ContractoServicio.Utilidad.GetComboTiposProcedencia.TipoProcedenciaColeccion

        'cria commando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboTiposProcedencia)
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornoTipoProcedencia As New ContractoServicio.Utilidad.GetComboTiposProcedencia.TipoProcedenciaColeccion

        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                objRetornoTipoProcedencia.Add(PopulaComboTipoProcedencia(dr))
            Next
        End If
        Return objRetornoTipoProcedencia
    End Function

#End Region

#Region "[ALTA]"

    ''' <summary>
    ''' INSERE OS DADOS DE TIPOS DE PROCEDENCIA
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    Public Shared Function setTiposProcedencia(objPeticion As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.InsertTipoProcedencias)
        comando.CommandType = CommandType.Text

        Dim oidTipoProcedencia As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, oidTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.codTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objPeticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objPeticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desUsuarioModificacion))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return objPeticion.codTipoProcedencia
    End Function

#End Region

#Region "[BAJA]"

    ''' <summary>
    ''' ATUALIZA OS DADOS DE TIPOS DE PROCEDENCIA
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    Public Shared Function AtualizaTipoProcedencia(ObjPeticion As ContractoServicio.TipoProcedencia.SetTiposProcedencias.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaTipoProcedencia)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PROCEDENCIA", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.codTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PROCEDENCIA", ProsegurDbType.Descricao_Longa, ObjPeticion.desTipoProcedencia))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPeticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, ObjPeticion.oidTipoProcedencia))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return ObjPeticion.codTipoProcedencia
    End Function
#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Preenche a coleção de tipo de procedencia através do DataTable.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Shared Function PopulaComboTipoProcedencia(dr As DataRow) As Utilidad.GetComboTiposProcedencia.TipoProcedencia

        Dim objTipoSubCliente As New ContractoServicio.Utilidad.GetComboTiposProcedencia.TipoProcedencia

        Util.AtribuirValorObjeto(objTipoSubCliente.Oid, dr("OID_TIPO_PROCEDENCIA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Codigo, dr("COD_TIPO_PROCEDENCIA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Descripcion, dr("DES_TIPO_PROCEDENCIA"), GetType(String))

        Return objTipoSubCliente

    End Function

    ''' <summary>
    ''' POPULA TIPO PROCEDENCIA
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [danielnunes] 14/05/2013 Criado
    ''' </history>
    Private Shared Function PopularTipoProcedencia(dr As DataRow) As ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia

        Dim objTipoProcedencia As New ContractoServicio.TipoProcedencia.GetTiposProcedencias.TipoProcedencia

        Util.AtribuirValorObjeto(objTipoProcedencia.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTipoProcedencia.codTipoProcedencia, dr("COD_TIPO_PROCEDENCIA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.desTipoProcedencia, dr("DES_TIPO_PROCEDENCIA"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.gmtCreacion, dr("GMT_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.gmtModificacion, dr("GMT_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoProcedencia.oidTipoProcedencia, dr("OID_TIPO_PROCEDENCIA"), GetType(String))

        Return objTipoProcedencia

    End Function

    'Verifica um tipo de procedência esta sendo utilizado para um ou mais procedências
    'Pgoncalves - Created 11/06/2013
    Public Shared Function VerificaVinculoProcendenciaTpProcedencia(oidTipoProcedencia As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaVinculoTipoProcedencia)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PROCEDENCIA", ProsegurDbType.Objeto_Id, oidTipoProcedencia))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True
    End Function

#End Region

End Class
