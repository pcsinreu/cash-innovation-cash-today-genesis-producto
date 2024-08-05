Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio.Dinamico

Namespace Genesis

    Public Class DataSetReportes

        Public Shared Function Consultar(Peticion As Prosegur.Genesis.ContractoServicio.Dinamico.Peticion) As List(Of Prosegur.Genesis.ContractoServicio.Valor)
            Dim conexao As String
            If String.IsNullOrEmpty(Peticion.Conexao) Then
                conexao = Constantes.CONEXAO_GENESIS
            Else
                conexao = Peticion.Conexao
            End If

            Dim cmd As IDbCommand = AcessoDados.CriarComando(conexao)

            Dim SQL As String = Peticion.CommandText

            For Each parametro In Peticion.Parametros
                If parametro.MultiValue Then
                    SQL = SQL.Replace(":" & parametro.Codigo, Util.MontarParametroIn(conexao, parametro.Valores, parametro.Codigo, cmd))
                Else
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(conexao, parametro.Codigo, ProsegurDbType.Observacao_Curta, parametro.Valores(0)))
                End If
            Next

            SQL = Util.PrepararQuery(conexao, SQL)

            cmd.CommandText = SQL
            If Peticion.StoredProcedure Then
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(Util.CriarParametroOracle("cv_1", ParameterDirection.ReturnValue, DBNull.Value, OracleClient.OracleType.Cursor))
            Else
                cmd.CommandType = CommandType.Text
            End If

            ' executar consulta
            Dim dr As IDataReader = AcessoDados.ExecutarDataReader(conexao, cmd)

            Dim resposta As New List(Of Prosegur.Genesis.ContractoServicio.Valor)

            ' Limpa o objeto da memória quando termina de usá-lo
            Using dr
                Try
                    While dr.Read()
                        resposta.Add(New Prosegur.Genesis.ContractoServicio.Valor() With {.Codigo = dr(Peticion.ValueField), .Descripcion = dr(Peticion.LabelField)})
                    End While

                    If Not SQL.ToUpper().Contains("ORDER BY") Then
                        resposta = resposta.OrderBy(Function(x) x.Descripcion).ToList()
                    End If
                Finally

                    ' Fecha a conexão do Data Reader
                    If dr IsNot Nothing Then
                        dr.Close()
                        dr.Dispose()
                    End If

                    ' Fecha a conexão do banco
                    AcessoDados.Desconectar(cmd.Connection)
                End Try
            End Using

            Return resposta
        End Function
    End Class

End Namespace