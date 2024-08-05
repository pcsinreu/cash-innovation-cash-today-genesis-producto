Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text

Public Class Nivel

#Region "[CONSULTAR]"

    Public Shared Function GetComboNivelesParametros(objPeticion As ContractoServicio.Utilidad.GetComboNivelesParametros.Peticion) As ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion

        ' criar objeto Nivel coleccion
        Dim objNiveles As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.GetComboNiveles.ToString)

        ' adicionar filtros
        Dim filtro As New StringBuilder
        If objPeticion.Permisos IsNot Nothing AndAlso objPeticion.Permisos.Count > 0 Then
            filtro.Append(Util.MontarClausulaIn(objPeticion.Permisos, "COD_PERMISO", comando, "WHERE"))
        End If
        query.Append(filtro)

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
                objNiveles.Add(PopularComboNiveles(dr))
            Next
        End If
        ' retornar coleção de Nivel
        Return objNiveles
    End Function

    Public Shared Function ObterOIDNivel(codigoNivel As String) As String
        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.ObterOIDNivel.ToString)

        ' obter query
        comando.CommandType = CommandType.Text

        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NIVEL_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, codigoNivel))

        comando.CommandText = Util.PrepararQuery(query.ToString)
        ' executar query
        Dim result As String = CType(AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, comando), String)
        Return result
    End Function

    Private Shared Function PopularComboNiveles(dr As DataRow) As ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametro
        ' criar objeto nivel
        Dim objNivel As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametro
        Util.AtribuirValorObjeto(objNivel.CodigoNivel, dr("COD_NIVEL_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objNivel.DescripcionNivel, dr("DES_NIVEL_PARAMETRO"), GetType(String))
        Util.AtribuirValorObjeto(objNivel.CodigoPermiso, dr("COD_PERMISO"), GetType(String))
        Return objNivel

    End Function

#End Region

End Class