Imports Prosegur.DbHelper

Public Class Formato

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém o oid Formato através do codigo do Formato
    ''' </summary>
    ''' <param name="CodigoFormato"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidFormato(CodigoFormato As String) As String

        ' isto é necessário pois ao efetuar uma baja o codigo é passado com valor nothing e não pode retornar string.empty.
        If CodigoFormato Is Nothing Then
            Return Nothing
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidFormato.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_FORMATO", ProsegurDbType.Identificador_Alfanumerico, CodigoFormato))

        Dim OidRetorno As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidRetorno = dtQuery.Rows(0)("OID_FORMATO")
        End If

        Return OidRetorno

    End Function

    ''' <summary>
    ''' Obtém os formatos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Public Shared Function GetComboFormatos() As ContractoServicio.Utilidad.GetComboFormatos.FormatoColeccion

        ' criar objeto formato coleccion
        Dim objFormatos As New ContractoServicio.Utilidad.GetComboFormatos.FormatoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        comando.CommandText = Util.PrepararQuery(My.Resources.getComboFormatos.ToString)
        comando.CommandType = CommandType.Text

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        ' se encontrou algum registro
        If dtQuery IsNot Nothing _
            AndAlso dtQuery.Rows.Count > 0 Then

            ' percorrer todos os registros
            For Each dr As DataRow In dtQuery.Rows

                ' adicionar para coleção
                objFormatos.Add(PopularComboFormatos(dr))

            Next

        End If

        ' retornar coleção de formatos
        Return objFormatos

    End Function

    ''' <summary>
    ''' Popula um objeto formato com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PopularComboFormatos(dr As DataRow) As ContractoServicio.Utilidad.GetComboFormatos.Formato

        ' criar objeto formato
        Dim objFormato As New ContractoServicio.Utilidad.GetComboFormatos.Formato

        Util.AtribuirValorObjeto(objFormato.Codigo, dr("cod_formato"), GetType(String))
        Util.AtribuirValorObjeto(objFormato.Descripcion, dr("des_formato"), GetType(String))

        ' retorna objeto preenchido
        Return objFormato

    End Function

#End Region

End Class