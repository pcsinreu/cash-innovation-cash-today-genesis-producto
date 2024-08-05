Imports Prosegur.DbHelper
Imports System.Text

Public Class Algoritmo

#Region "[CONSUTAR]"

    ''' <summary>
    ''' Obtém o oid Algoritmo através do codigo do Algoritmo
    ''' </summary>
    ''' <param name="CodigoAlgoritmo"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Public Shared Function ObterOidAlgoritmo(CodigoAlgoritmo As String) As String

        ' isto é necessário pois ao efetuar uma baja o codigo é passado com valor nothing e não pode retornar string.empty.
        If CodigoAlgoritmo Is Nothing Then
            Return Nothing
        End If

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter comando sql
        comando.CommandText = Util.PrepararQuery(My.Resources.ObterOidAlgoritmo.ToString)
        comando.CommandType = CommandType.Text

        ' criar parameter
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ALGORITMO_VALIDACION", ProsegurDbType.Identificador_Alfanumerico, CodigoAlgoritmo))

        Dim OidRetorno As String = String.Empty

        ' executar query
        Dim dtQuery As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, comando)

        If dtQuery.Rows.Count > 0 Then
            OidRetorno = dtQuery.Rows(0)("OID_ALGORITMO_VALIDACION")
        End If

        Return OidRetorno

    End Function

    ''' <summary>
    ''' Obtém os algoritmos
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' [octavio.piramo] 25/03/2009 Alterado
    ''' </history>
    Public Shared Function GetComboAlgoritmos(objPeticion As ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion) As ContractoServicio.Utilidad.GetComboAlgoritmos.AlgoritmoColeccion

        ' criar objeto Algoritmo coleccion
        Dim objAlgoritmos As New ContractoServicio.Utilidad.GetComboAlgoritmos.AlgoritmoColeccion

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' obter query
        Dim query As New StringBuilder
        query.Append(My.Resources.getComboAlgoritmos.ToString)

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
                objAlgoritmos.Add(PopularComboAlgoritmos(dr))

            Next

        End If

        ' retornar coleção de algoritmos
        Return objAlgoritmos

    End Function

    ''' <summary>
    ''' Popula um objeto algoritmo com os valores do datarow
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 30/01/2009 Criado
    ''' </history>
    Private Shared Function PopularComboAlgoritmos(dr As DataRow) As ContractoServicio.Utilidad.GetComboAlgoritmos.Algoritmo

        ' criar objeto Algoritmo
        Dim objAlgoritmo As New ContractoServicio.Utilidad.GetComboAlgoritmos.Algoritmo

        Util.AtribuirValorObjeto(objAlgoritmo.Codigo, dr("cod_algoritmo_validacion"), GetType(String))
        Util.AtribuirValorObjeto(objAlgoritmo.Descripcion, dr("des_algoritmo_validacion"), GetType(String))
        Util.AtribuirValorObjeto(objAlgoritmo.Observaciones, dr("obs_algoritmo_validacion"), GetType(String))

        ' retorna objeto preenchido
        Return objAlgoritmo

    End Function

#End Region

End Class