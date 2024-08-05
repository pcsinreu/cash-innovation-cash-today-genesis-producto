Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Modelo

#Region "[CONSULTAS]"

    Public Shared Function GetModelo(objPetion As ContractoServicio.Modelo.GetModelo.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As List(Of Comon.Clases.Modelo)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaModelo)
        cmd.CommandType = CommandType.Text

        If Not String.IsNullOrEmpty(objPetion.OidFabricante) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(OID_FABRICANTE) = :OID_FABRICANTE "
            Else
                cmd.CommandText &= " WHERE UPPER(OID_FABRICANTE) = :OID_FABRICANTE "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_FABRICANTE", ProsegurDbType.Identificador_Alfanumerico, objPetion.OidFabricante.ToUpper()))
        End If

        ' Se o codigo modelo for informado
        If Not String.IsNullOrEmpty(objPetion.CodModelo) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(COD_MODELO) = :COD_MODELO "
            Else
                cmd.CommandText &= " WHERE UPPER(COD_MODELO) = :COD_MODELO "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MODELO", ProsegurDbType.Identificador_Alfanumerico, objPetion.CodModelo.ToUpper()))
        End If

        'Se a descricao for informada
        If objPetion.DesModelo <> Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(DES_MODELO) LIKE :DES_MODELO "
            Else
                cmd.CommandText &= " WHERE UPPER(DES_MODELO) LIKE :DES_MODELO "
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_MODELO", ProsegurDbType.Identificador_Alfanumerico, "%" & objPetion.DesModelo.ToUpper() & "%"))
        End If

        'Se a validade for informada
        If objPetion.BolVigente IsNot Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND BOL_ACTIVO = :BOL_ACTIVO"
            Else
                cmd.CommandText &= " WHERE BOL_ACTIVO = :BOL_ACTIVO"
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_ACTIVO", ProsegurDbType.Logico, objPetion.BolVigente))
        End If
        'cria o objeto de Modelo
        Dim objModelo As New List(Of Comon.Clases.Modelo)

        Dim dtModelo As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPetion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtModelo IsNot Nothing _
            AndAlso dtModelo.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtModelo.Rows
                ' preencher a coleção com objetos divisa
                objModelo.Add(PopularGetModelo(dr))
            Next
        End If

        Return objModelo

    End Function

#End Region

#Region "[INSERIR]"

#End Region

#Region "[UPDATE]"

#End Region

#Region "[DELETE]"

#End Region

#Region "[DEMAIS]"

    ''' <summary>
    ''' Responsável por popular a classe de Modelo 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Shared Function PopularGetModelo(dr As DataRow) As Comon.Clases.Modelo

        ' criar objeto Modelo
        Dim objModelo As New Comon.Clases.Modelo

        Util.AtribuirValorObjeto(objModelo.Identificador, dr("OID_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objModelo.Codigo, dr("COD_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objModelo.Descripcion, dr("DES_MODELO"), GetType(String))
        Util.AtribuirValorObjeto(objModelo.IdentificadorFabricante, dr("OID_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objModelo.NelCapacidadBilletes, dr("NEL_CAPACIDAD_BILLETES"), GetType(Long))
        Util.AtribuirValorObjeto(objModelo.NelCapacidadMonedas, dr("NEL_CAPACIDAD_MONEDAS"), GetType(Long))
        Util.AtribuirValorObjeto(objModelo.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objModelo.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objModelo.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objModelo.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        ' retornar objeto divisa preenchido
        Return objModelo

    End Function

#End Region

End Class
