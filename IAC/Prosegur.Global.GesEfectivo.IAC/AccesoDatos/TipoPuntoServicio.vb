Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class TipoPuntoServicio

#Region "[CONSULTAS]"

    ''' <summary>
    ''' OBTÉM TODOS OS TIPOS PUNTOS SERVICIOS
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 08/04/2013 Criado
    ''' </history>
    Public Shared Function getTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicioColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTipoPuntoSerivicio)
        comando.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(Peticion.codTipoPuntoServicio) OrElse _
            Not String.IsNullOrEmpty(Peticion.desTipoPuntoServicio) OrElse _
            Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " WHERE "
        End If

        If Not String.IsNullOrEmpty(Peticion.codTipoPuntoServicio) Then
            comando.CommandText &= " UPPER(COD_TIPO_PUNTO_SERVICIO) = :COD_TIPO_PUNTO_SERVICIO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, Peticion.codTipoPuntoServicio.ToUpper))
        End If

        If Not String.IsNullOrEmpty(Peticion.desTipoPuntoServicio) AndAlso String.IsNullOrEmpty(Peticion.codTipoPuntoServicio) Then
            comando.CommandText &= " UPPER(DES_TIPO_PUNTO_SERVICIO) LIKE :DES_TIPO_PUNTO_SERVICIO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, "%" & Peticion.desTipoPuntoServicio.ToUpper & "%"))
        ElseIf Not String.IsNullOrEmpty(Peticion.desTipoPuntoServicio) Then
            comando.CommandText &= " AND UPPER(DES_TIPO_PUNTO_SERVICIO) LIKE :DES_TIPO_PUNTO_SERVICIO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PUNTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, "%" & Peticion.desTipoPuntoServicio.ToUpper & "%"))
        End If

        If (Not String.IsNullOrEmpty(Peticion.codTipoPuntoServicio) OrElse Not String.IsNullOrEmpty(Peticion.desTipoPuntoServicio)) AndAlso _
            Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        ElseIf Peticion.bolActivo IsNot Nothing Then
            comando.CommandText &= " BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        End If

        Dim objTipoPuntoServicioCol As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicioColeccion

        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, Peticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        If dt.Rows.Count > 0 AndAlso _
            dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                objTipoPuntoServicioCol.Add(PopularTipoPuntoServicio(dr))
            Next
        End If

        Return objTipoPuntoServicioCol

    End Function

    ''' <summary>
    ''' Obtém todas tipos de puntos de servicio ativos.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Shared Function GetComboTiposPuntoServicio() As ContractoServicio.Utilidad.GetComboTiposPuntoServicio.TipoPuntoServicioColeccion

        'cria commando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetComboTiposPuntoServicio)
        comando.CommandType = CommandType.Text

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        Dim objRetornoTipoPuntoServicio As New ContractoServicio.Utilidad.GetComboTiposPuntoServicio.TipoPuntoServicioColeccion

        If dt IsNot Nothing _
            AndAlso dt.Rows.Count > 0 Then

            For Each dr As DataRow In dt.Rows
                objRetornoTipoPuntoServicio.Add(PopulaComboTipoPuntoServicio(dr))
            Next
        End If
        Return objRetornoTipoPuntoServicio
    End Function

    ''' <summary>
    ''' Verifica se o tipo punto servicio esta sendo usado em GEPR_TPUNTO_SERVICIO
    ''' </summary>
    ''' <param name="OidTipoPuntoServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 16/04/2013 Criado
    ''' </history>
    Public Shared Function VerificaTipoPuntoServicio(OidTipoPuntoServicio As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaUtilizacaoPuntoServicio)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, OidTipoPuntoServicio))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Verifica se o tipo punto servicio esta sendo usado em GEPR_TPROCEDENCIA
    ''' </summary>
    ''' <param name="oidTipoPuntoSercicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/06/2013 Criado
    ''' </history>
    Public Shared Function VerificaTipoPuntoProcedencia(oidTipoPuntoSercicio As String) As Boolean

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.VerificaTipoPuntoServicioProcedencia)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, oidTipoPuntoSercicio))

        Dim retorno As Integer = AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando)

        If retorno > 0 Then
            Return False
        End If
        Return True
    End Function

    Public Shared Function BuscaTipoPuntoServicioPorCodigo(codTipoPuntoServicio As String) As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTipoPuntoServicioPorCodigo)
        comando.CommandType = CommandType.Text
        Dim tipoPuntoServicio As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio = Nothing

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, codTipoPuntoServicio))

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            tipoPuntoServicio = PopularTipoPuntoServicio(dtQuery.Rows(0))
        End If

        Return tipoPuntoServicio

    End Function

#End Region

#Region "[INSERT]"

    ''' <summary>
    ''' Insere registro na Tabela
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 16/04/2013 Criado
    ''' </history>
    Public Shared Function setTiposPuntosServicio(Peticion As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion)

        Dim ObjTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.InsertTipoPuntoServicio)
        comando.CommandType = CommandType.Text


        Dim oidTipoPuntoServicio As String = Guid.NewGuid.ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, oidTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PUNTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.codTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PUNTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.desTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MAQUINA", ProsegurDbType.Logico, Peticion.bolMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MAE", ProsegurDbType.Logico, Peticion.bolMae))

        ObjTransacao.AdicionarItemTransacao(comando)
        ObjTransacao.RealizarTransacao()

        Return Peticion.codTipoPuntoServicio
    End Function

#End Region

#Region "[ATUALIZAÇÃO]"

    ''' <summary>
    ''' Atualiza registro na Tabela
    ''' </summary>
    ''' <param name="Peticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 16/04/2013 Criado
    ''' </history>
    Public Shared Function AtualizaTipoPuntoServicio(Peticion As ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.AtualizaTipoPuntoServicio)
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_PUNTO_SERVICIO", ProsegurDbType.Descricao_Curta, Peticion.codTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_PUNTO_SERVICIO", ProsegurDbType.Descricao_Longa, Peticion.desTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, Peticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, Peticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, Peticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.desUsuarioModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_PUNTO_SERVICIO", ProsegurDbType.Objeto_Id, Peticion.oidTipoPuntoServicio))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MAQUINA", ProsegurDbType.Logico, Peticion.bolMaquina))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_MAE", ProsegurDbType.Logico, Peticion.bolMae))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return Peticion.codTipoPuntoServicio
    End Function

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Preenche a coleção de tipo punto de servicio através do DataTable.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Private Shared Function PopulaComboTipoPuntoServicio(dr As DataRow) As Utilidad.GetComboTiposPuntoServicio.TipoPuntoServicio

        Dim objTipoSubCliente As New ContractoServicio.Utilidad.GetComboTiposPuntoServicio.TipoPuntoServicio

        Util.AtribuirValorObjeto(objTipoSubCliente.Oid, dr("OID_TIPO_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Codigo, dr("COD_TIPO_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objTipoSubCliente.Descripcion, dr("DES_TIPO_PUNTO_SERVICIO"), GetType(String))

        Return objTipoSubCliente

    End Function

    ''' <summary>
    ''' Popula o Tipo Punto de Servicio
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 16/04/2013 Criado
    ''' </history>
    Private Shared Function PopularTipoPuntoServicio(dr As DataRow) As TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio

        Dim objTpoPuntoServicio As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio

        Util.AtribuirValorObjeto(objTpoPuntoServicio.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.codTipoPuntoServicio, dr("COD_TIPO_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.desTipoPuntoServicio, dr("DES_TIPO_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.desUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.gmtCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.gmtModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.oidTipoPuntoServicio, dr("OID_TIPO_PUNTO_SERVICIO"), GetType(String))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.bolMaquina, dr("BOL_MAQUINA"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTpoPuntoServicio.bolMae, dr("BOL_MAE"), GetType(Boolean))

        Return objTpoPuntoServicio

    End Function

#End Region
End Class
