Imports Prosegur.DbHelper
Imports System.Text

Public Class Mascara

#Region "[CONSULTAR]"

    ''' <summary>
    ''' Obtém o oid Mascara através do codigo da máscara
    ''' </summary>
    ''' <param name="CodigoMascara"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidMascara(CodigoMascara As String) As String

        ' isto é necessário pois ao efetuar uma baja o codigo é passado com valor nothing e não pode retornar string.empty.
        If CodigoMascara Is Nothing Then
            Return Nothing
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidMascara.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_MASCARA", ProsegurDbType.Identificador_Alfanumerico, CodigoMascara))

        Dim OidRetorno As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidRetorno = dtQuery.Rows(0)("OID_MASCARA")
        End If

        Return OidRetorno

    End Function

    ''' <summary>
    ''' Obtém as mascaras
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' [octavio.piramo] 25/03/2009 Alterado
    ''' </history>
    Public Shared Function GetComboMascaras(objPeticion As ContractoServicio.Utilidad.GetComboMascaras.Peticion) As ContractoServicio.Utilidad.GetComboMascaras.MascaraColeccion

        ' criar objeto mascara coleccion
        Dim objMascaras As New ContractoServicio.Utilidad.GetComboMascaras.MascaraColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.getComboMascaras.ToString)

        ' adicionar filtros
        Dim filtro As New StringBuilder

        If objPeticion.AplicaTerminosMediosPago IsNot Nothing _
            OrElse objPeticion.AplicaTerminosIac IsNot Nothing Then

            If objPeticion.AplicaTerminosMediosPago IsNot Nothing Then
                filtro.Append("BOL_APLICA_TERM_MEDIO_PAGO = []BOL_APLICA_TERM_MEDIO_PAGO")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_APLICA_TERM_MEDIO_PAGO", ProsegurDbType.Logico, objPeticion.AplicaTerminosMediosPago))
            End If

            If objPeticion.AplicaTerminosIac IsNot Nothing Then

                ' se adicionou um filtro antes
                If filtro.Length > 0 Then
                    filtro.Append(" AND ")
                End If

                filtro.Append("BOL_APLICA_TERM_IAC = []BOL_APLICA_TERM_IAC")
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "BOL_APLICA_TERM_IAC", ProsegurDbType.Logico, objPeticion.AplicaTerminosIac))
            End If

            ' se adicionou um filtro antes
            If filtro.Length > 0 Then
                query.Append(" WHERE " & filtro.ToString)
            End If

        End If

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
                objMascaras.Add(PopularComboMascaras(dr))

            Next

        End If

        ' retornar coleção de mascaras
        Return objMascaras

    End Function

    ''' <summary>
    ''' Popula um objeto mascara com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PopularComboMascaras(dr As DataRow) As ContractoServicio.Utilidad.GetComboMascaras.Mascara

        ' criar objeto mascara
        Dim objMascara As New ContractoServicio.Utilidad.GetComboMascaras.Mascara

        Util.AtribuirValorObjeto(objMascara.Codigo, dr("cod_mascara"), GetType(String))
        Util.AtribuirValorObjeto(objMascara.Descripcion, dr("des_mascara"), GetType(String))

        ' retorna objeto preenchido
        Return objMascara

    End Function

#End Region

End Class