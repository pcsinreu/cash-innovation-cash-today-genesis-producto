Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.DbHelper
Imports System.Text
Imports System.Text.RegularExpressions
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Data
Imports Prosegur.Genesis.Comon.Paginacion.AccesoDatos
Imports Prosegur.Genesis

Public Class Fabricante

#Region "[CONSULTAS]"

    Public Shared Function GetFabricante(objPetion As ContractoServicio.Fabricante.GetFabricante.Peticion, ByRef parametrosRespuestaPaginacion As Genesis.Comon.Paginacion.ParametrosRespuestaPaginacion) As List(Of Comon.Clases.Fabricante)
        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.BuscaFabricante)
        cmd.CommandType = CommandType.Text

        ' Se o codigo fabricante for informado
        If Not String.IsNullOrEmpty(objPetion.CodFabricante) Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(COD_FABRICANTE) = :COD_FABRICANTE "
            Else
                cmd.CommandText &= " WHERE UPPER(COD_FABRICANTE) = :COD_FABRICANTE "
            End If

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FABRICANTE", ProsegurDbType.Identificador_Alfanumerico, objPetion.CodFabricante.ToUpper()))
        End If

        'Se a descricao for informada
        If objPetion.DesFabricante <> Nothing Then
            If cmd.CommandText.Contains("WHERE") Then
                cmd.CommandText &= " AND UPPER(DES_FABRICANTE) LIKE :DES_FABRICANTE "
            Else
                cmd.CommandText &= " WHERE UPPER(DES_FABRICANTE) LIKE :DES_FABRICANTE "
            End If
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_FABRICANTE", ProsegurDbType.Identificador_Alfanumerico, "%" & objPetion.DesFabricante.ToUpper() & "%"))
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
        'cria o objeto de Fabricante
        Dim objFabricante As New List(Of Comon.Clases.Fabricante)

        Dim dtFabricante As DataTable = PaginacionHelper.EjecutarPaginacion(Constantes.CONEXAO_GE, cmd, objPetion.ParametrosPaginacion, parametrosRespuestaPaginacion)

        ' caso encontre algum registro
        If dtFabricante IsNot Nothing _
            AndAlso dtFabricante.Rows.Count > 0 Then
            ' percorrer registros encontrados
            For Each dr As DataRow In dtFabricante.Rows
                ' preencher a coleção com objetos divisa
                objFabricante.Add(PopularGetFabricante(dr))
            Next
        End If

        Return objFabricante

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
    ''' Responsável por popular a classe de Fabricante 
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Shared Function PopularGetFabricante(dr As DataRow) As Comon.Clases.Fabricante

        ' criar objeto Fabricante
        Dim objFabricante As New Comon.Clases.Fabricante

        Util.AtribuirValorObjeto(objFabricante.Identificador, dr("OID_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objFabricante.Codigo, dr("COD_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objFabricante.Descripcion, dr("DES_FABRICANTE"), GetType(String))
        Util.AtribuirValorObjeto(objFabricante.Direccion, dr("DIRECCION"), GetType(String))
        Util.AtribuirValorObjeto(objFabricante.DesUsuarioCreacion, dr("DES_USUARIO_CREACION"), GetType(String))
        Util.AtribuirValorObjeto(objFabricante.FechaHoraCreacion, dr("GMT_CREACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objFabricante.FechaHoraModificacion, dr("GMT_MODIFICACION"), GetType(DateTime))
        Util.AtribuirValorObjeto(objFabricante.DesUsuarioModificacion, dr("DES_USUARIO_MODIFICACION"), GetType(String))
        ' retornar objeto divisa preenchido
        Return objFabricante

    End Function

#End Region

End Class
