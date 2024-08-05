Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class TipoPlanificacion

#Region "[CONSULTAS]"

    ''' <summary>
    ''' OBTÉM TODOS OS TIPOS DE PLANIFICAÇÕES
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mprates] 04/07/2017 Criado
    ''' </history>
    Public Shared Function getTiposPlanificaciones(objPeticion As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta

        Dim respuesta As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.Respuesta

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.BuscaTiposPlanificaciones)
        comando.CommandType = CommandType.Text

        Dim listaTiposPlanificaciones As New List(Of ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion)

        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        If dt.Rows.Count > 0 AndAlso _
            dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                listaTiposPlanificaciones.Add(PopularTipoCliente(dr))
            Next
        End If

        respuesta.TiposPlanificaciones = listaTiposPlanificaciones

        Return respuesta

    End Function


#End Region

#Region "[ALTA]"

    ''' <summary>
    ''' INSERE OS DADOS DE TIPOS DE CLIENTES
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    Public Shared Function setTiposClientes(objPeticion As ContractoServicio.TipoCliente.SetTiposClientes.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        comando.CommandText = Util.PrepararQuery(My.Resources.InsertTipoClientes)
        comando.CommandType = CommandType.Text

        Dim oidTipoCliente As String = Guid.NewGuid().ToString

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, oidTipoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.codTipoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desTipoCliente))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.bolActivo))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, objPeticion.gmtCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desUsuarioCreacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, objPeticion.gmtModificacion))
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.desUsuarioModificacion))

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return objPeticion.codTipoCliente
    End Function

#End Region

#Region "[BAJA]"

    ''' <summary>
    ''' ATUALIZA OS DADOS DE TIPOS DE CLIENTES
    ''' </summary>
    ''' <param name="ObjPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 05/04/2013 Criado
    ''' </history>
    Public Shared Function AtualizaTipoSector(ObjPeticion As ContractoServicio.TipoCliente.SetTiposClientes.Peticion)

        Dim objTransacao As New Transacao(Constantes.CONEXAO_GE)
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        Dim query As New StringBuilder

        query.AppendLine("UPDATE GEPR_TTIPO_CLIENTE ")

        query.AppendLine("SET COD_TIPO_CLIENTE = []COD_TIPO_CLIENTE, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_TIPO_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.codTipoCliente))

        query.AppendLine("DES_TIPO_CLIENTE = []DES_TIPO_CLIENTE, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_TIPO_CLIENTE", ProsegurDbType.Descricao_Longa, ObjPeticion.desTipoCliente))

        query.AppendLine("BOL_ACTIVO = []BOL_ACTIVO, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, ObjPeticion.bolActivo))

        query.AppendLine("GMT_CREACION = []GMT_CREACION, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_CREACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtCreacion))

        query.AppendLine("DES_USUARIO_CREACION = []DES_USUARIO_CREACION, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_CREACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioCreacion))

        query.AppendLine("GMT_MODIFICACION = []GMT_MODIFICACION, ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "GMT_MODIFICACION", ProsegurDbType.Data_Hora, ObjPeticion.gmtModificacion))

        query.AppendLine("DES_USUARIO_MODIFICACION = []DES_USUARIO_MODIFICACION ")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_USUARIO_MODIFICACION", ProsegurDbType.Identificador_Alfanumerico, ObjPeticion.desUsuarioModificacion))

        query.AppendLine("WHERE OID_TIPO_CLIENTE = []OID_TIPO_CLIENTE")
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_TIPO_CLIENTE", ProsegurDbType.Objeto_Id, ObjPeticion.oidTipoCliente))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        comando.CommandType = CommandType.Text

        objTransacao.AdicionarItemTransacao(comando)
        objTransacao.RealizarTransacao()

        Return ObjPeticion.codTipoCliente
    End Function
#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' POPULA TIPO PLANIFICACION
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [mprates] 04/07/2017 Criado
    ''' </history>
    Private Shared Function PopularTipoCliente(dr As DataRow) As ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion

        Dim objTipoPlanificacion As New ContractoServicio.TipoPlanificacion.GetTiposPlanificaciones.TipoPlanificacion

        ' Util.AtribuirValorObjeto(objTipoCliente.bolActivo, dr("BOL_ACTIVO"), GetType(Boolean))
        Util.AtribuirValorObjeto(objTipoPlanificacion.codTipoPlanificacion, dr("COD_TIPO_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoPlanificacion.desTipoPlanificacion, dr("DES_TIPO_PLANIFICACION"), GetType(String))
        Util.AtribuirValorObjeto(objTipoPlanificacion.oidTipoPlanificacion, dr("OID_TIPO_PLANIFICACION"), GetType(String))
      

        Return objTipoPlanificacion

    End Function

#End Region

End Class
