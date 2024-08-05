Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor


<Serializable()> _
Public Class TransaccionNoMigrada

    ''' <summary>
    ''' Ejecuta una busqueda en Saldos I para validar se hay transacciones para los filtros informados que aún no fueran migradas a Saldos II.
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function HayTransaccionNoMigrada(objPeticion As ContractoServicio.RecuperarTransaccionNoMigrada.Peticion) As Boolean
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.Text
        comando.CommandText = My.Resources.TransaccionNoMigrada
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "FYH_TRANSACCION", ProsegurDbType.Data_Hora, objPeticion.Fecha))
        Dim InnerSQL As String = String.Empty
        Dim WherePlantas As String = String.Empty
        Dim WhereCliente As String = String.Empty

        If objPeticion.Plantas IsNot Nothing AndAlso objPeticion.Plantas.Count > 0 Then
            InnerSQL = " INNER JOIN PD_CENTROPROCESO P ON T.COD_CENTRO_PROCESO = P.IDCENTROPROCESO "
            WherePlantas = Util.MontarClausulaIn(objPeticion.Plantas, "IDPLANTA", comando, " AND", "P")

        Else
            comando.CommandText = comando.CommandText.Replace("{0}", "")

        End If

        If Not String.IsNullOrEmpty(objPeticion.CodigoClienteSaldo) Then
            WhereCliente = " AND COD_CLIENTE_SALDO = []COD_CLIENTE_SALDO "
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "COD_CLIENTE_SALDO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.CodigoClienteSaldo))
        End If

        If Not String.IsNullOrEmpty(InnerSQL) AndAlso Not String.IsNullOrEmpty(WhereCliente) Then
            comando.CommandText = String.Format(comando.CommandText, InnerSQL, WhereCliente & WherePlantas)

        ElseIf Not String.IsNullOrEmpty(InnerSQL) AndAlso String.IsNullOrEmpty(WhereCliente) Then
            comando.CommandText = String.Format(comando.CommandText, InnerSQL, WherePlantas)

        ElseIf String.IsNullOrEmpty(InnerSQL) AndAlso Not String.IsNullOrEmpty(WhereCliente) Then
            comando.CommandText = String.Format(comando.CommandText, InnerSQL, WhereCliente)

        Else
            comando.CommandText = comando.CommandText.Replace("{1}", "")

        End If
        comando.CommandText = Util.PrepararQuery(comando.CommandText)
        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_SALDOS, comando)
    End Function

End Class