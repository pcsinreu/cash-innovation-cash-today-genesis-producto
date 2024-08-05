Imports Prosegur.DbHelper

Public Class Log

#Region "[INSERIR]"

    ''' <summary>
    ''' Método responsável por persistir o log no banco
    ''' </summary>
    ''' <param name="objPeticion"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Shared Sub InserirLog(objPeticion As ContractoServ.Log.Peticion)

        ' criar comando
        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        ' Limpa o objeto da memória quando termina de usá-lo
        Using comando
            Try
                ' obter query
                comando.CommandText = Util.PrepararQuery(My.Resources.InserirLog.ToString)
                comando.CommandType = CommandType.Text

                ' setar parametros
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_LOG_ERROR", ProsegurDbType.Objeto_Id, Guid.NewGuid().ToString))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_ERROR", ProsegurDbType.Observacao_Longa, objPeticion.DescricionErro))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_OTRO", ProsegurDbType.Observacao_Longa, objPeticion.Otros))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ERROR", ProsegurDbType.Data, objPeticion.FYHErro))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Identificador_Alfanumerico, objPeticion.LoginUsuario))
                comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Identificador_Alfanumerico, objPeticion.Delegacion))

                ' executar inserção
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GE, comando)

            Finally
                ' Fecha a conexão do banco
                AcessoDados.Desconectar(comando.Connection)
            End Try

        End Using

    End Sub

#End Region

End Class