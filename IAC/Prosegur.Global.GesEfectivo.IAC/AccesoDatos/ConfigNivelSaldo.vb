Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports Prosegur.Global.GesEfectivo.IAC
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos

Public Class ConfigNivelSaldo

    ''' <summary>
    ''' Obtém Niveis
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <param name="parametrosRespuestaPaginacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getConfigNivelSaldo(objPeticion As ContractoServicio.Utilidad.GetConfigNivel.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        comando.CommandText = Util.PrepararQuery(My.Resources.GetConfigNivelSaldo)
        comando.CommandType = CommandType.Text

        Dim objNivelColeccion As New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMovColeccion

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_CLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodCliente))

        If objPeticion.BolActivo.HasValue Then
            comando.CommandText &= " AND MOV.BOL_ACTIVO = :BOL_ACTIVO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPeticion.BolActivo.Value))
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodSubCliente) Then
            comando.CommandText &= " AND UPPER(SUB_CLI_MOV.COD_SUBCLIENTE) = :COD_SUBCLIENTE "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_SUBCLIENTE", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodSubCliente.ToUpper))
        Else
            comando.CommandText &= " AND SUB_CLI_MOV.COD_SUBCLIENTE IS NULL "
        End If

        If Not String.IsNullOrEmpty(objPeticion.CodPtoServicio) Then
            comando.CommandText &= " AND UPPER(PS_MOV.COD_PTO_SERVICIO) = :COD_PTO_SERVICIO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PTO_SERVICIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodPtoServicio.ToUpper))
        Else
            comando.CommandText &= " AND PS_MOV.COD_PTO_SERVICIO IS NULL "
        End If

        If objPeticion.CodSubCanal IsNot Nothing AndAlso objPeticion.CodSubCanal.Count > 0 Then
            Dim lista As New List(Of String)
            For Each canal In objPeticion.CodSubCanal
                If Not String.IsNullOrEmpty(canal) Then
                    lista.Add(canal)
                End If
            Next

            If (lista.Count > 0) Then
                comando.CommandText &= " AND UPPER(SUB_CAN.COD_SUBCANAL) IN (" & String.Join(", ", lista.ToArray).ToUpper & ") "
            End If
        End If

        comando.CommandText &= " ORDER BY CLI_MOV.COD_CLIENTE"

        Dim dt As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, comando, objPeticion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        If dt.Rows.Count > 0 AndAlso _
           dt IsNot Nothing Then
            For Each dr As DataRow In dt.Rows
                objNivelColeccion.Add(PopularNivelMov(dr))
            Next
        End If

        Return objNivelColeccion

    End Function


    ''' <summary>
    '''  Popula Nivel
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function PopularNivelMov(dr As DataRow) As ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov

        Dim objNivelMov As New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelMov

        Util.AtribuirValorObjeto(objNivelMov.oidConfigNivelMovimiento, dr("OID_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.oidCliente, dr("OID_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.codCliente, dr("COD_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.desCliente, dr("DES_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.oidSubcliente, dr("OID_SUB_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.codSubcliente, dr("COD_SUB_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.desSubcliente, dr("DES_SUB_CLI_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.oidPtoServicio, dr("OID_PTO_SERV_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.codPtoServicio, dr("COD_PTO_SERV_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.desPtoServicio, dr("DES_PTO_SERV_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.oidSubcanal, dr("OID_SUB_CANAL_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.codSubcanal, dr("COD_SUB_CANAL_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.desSubcanal, dr("DES_SUB_CANAL_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.fyhVigencia, dr("FYH_VIGENCIA_MOV"), GetType(Date))
        Util.AtribuirValorObjeto(objNivelMov.bolActivo, dr("BOL_ACTIVO_MOV"), GetType(Boolean))
        Util.AtribuirValorObjeto(objNivelMov.desUsuarioCreacion, dr("DES_USUARIO_CREACION_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION_MOV"), GetType(String))
        Util.AtribuirValorObjeto(objNivelMov.gmtCreacion, dr("GMT_CREACION_MOV"), GetType(Date))
        Util.AtribuirValorObjeto(objNivelMov.gmtModificacion, dr("GMT_MODIFICACION_MOV"), GetType(Date))

        Dim objNivelSaldo As New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelSaldo

        Util.AtribuirValorObjeto(objNivelSaldo.oidConfigNivelSaldo, dr("OID_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.oidCliente, dr("OID_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.codCliente, dr("COD_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.desCliente, dr("DES_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.oidSubcliente, dr("OID_SUB_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.codSubcliente, dr("COD_SUB_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.desSubcliente, dr("DES_SUB_CLI_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.oidPtoServicio, dr("OID_PTO_SERV_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.codPtoServicio, dr("COD_PTO_SERV_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.desPtoServicio, dr("DES_PTO_SERV_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.gmtCreacion, dr("GMT_CREACION_SALDO"), GetType(Date))
        Util.AtribuirValorObjeto(objNivelSaldo.desUsuarioCreacion, dr("DES_USUARIO_CREACION_SALDO"), GetType(String))
        Util.AtribuirValorObjeto(objNivelSaldo.gmtModificacion, dr("GMT_MODIFICACION_SALDO"), GetType(Date))
        Util.AtribuirValorObjeto(objNivelSaldo.desUsuarioModificacion, dr("DES_USUARIO_MODIFICACION_SALDO"), GetType(String))

        objNivelMov.configNivelSaldo = objNivelSaldo

        Return objNivelMov

    End Function

End Class