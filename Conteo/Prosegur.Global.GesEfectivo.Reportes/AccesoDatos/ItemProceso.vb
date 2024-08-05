Imports Prosegur.DbHelper

Public Class ItemProceso


#Region "[CONSULTAR]"

    Public Shared Function BuscarItemProcesoPorParametro(codProceso As String,
                                                         codDelegacion As String,
                                                         parametros As String) As DataTable

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        Dim cmd As IDbCommand = conexao.CreateCommand
        Dim dt As New DataTable()

        Try

            cmd.CommandText = Util.PrepararQuery(My.Resources.BuscarItemProcesoPorParametro.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PROCESO", ProsegurDbType.Descricao_Longa, codProceso))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, "PE"))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, codDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_PARAMETROS", ProsegurDbType.Observacao_Longa, parametros))

            ' executar consulta            
            Dim drItensProceso As IDataReader = cmd.ExecuteReader()

            ' carrega DataTable com os dados do IDataReader
            dt.Load(drItensProceso)

            ' Fecha a conexão do Data Reader
            drItensProceso.Close()
            drItensProceso.Dispose()

        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return dt

    End Function

    Public Shared Function RecuperarUltimoItemProceso(codProceso As String,
                                                      codDelegacion As String,
                                                      fechaHasta As Date) As DataTable

        Dim conexao As IDbConnection = AcessoDados.Conectar(Constantes.CONEXAO_GE)

        Dim cmd As IDbCommand = conexao.CreateCommand
        Dim dt As New DataTable()

        Try

            cmd.CommandText = Util.PrepararQuery(My.Resources.BuscarUltimoItemProceso.ToString)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PROCESO", ProsegurDbType.Descricao_Longa, codProceso))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, codDelegacion))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_CREACION", ProsegurDbType.Data_Hora, fechaHasta))

            ' executar consulta            
            Dim drItensProceso As IDataReader = cmd.ExecuteReader()

            ' carrega DataTable com os dados do IDataReader
            dt.Load(drItensProceso)

            ' Fecha a conexão do Data Reader
            drItensProceso.Close()
            drItensProceso.Dispose()

        Finally

            ' Fecha a conexão do banco
            AcessoDados.Desconectar(conexao)

        End Try

        Return dt

    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Grava os documentos no bd
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 10/07/2012 - Criado
    ''' </history>
    Public Shared Sub GravarItemProcesoConteo(codProceso As String,
                                              fechaCreacion As Date,
                                              codigoUsuario As String,
                                              codDelegacion As String,
                                              parametros As String,
                                              ByRef objTransacion As Transacao)


        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.GuardarItemProcesoConteo.ToString)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_ITEM_PROCESO", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PROCESO", ProsegurDbType.Descricao_Longa, codProceso))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OBS_PARAMETROS", ProsegurDbType.Observacao_Longa, parametros))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_ESTADO", ProsegurDbType.Identificador_Alfanumerico, "PE"))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "NEL_INTENTOS_ENVIO", ProsegurDbType.Inteiro_Longo, 0))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_DELEGACION", ProsegurDbType.Descricao_Longa, codDelegacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Curta, codigoUsuario))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_CREACION", ProsegurDbType.Data_Hora, fechaCreacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, fechaCreacion))

        objTransacion.AdicionarItemTransacao(cmd)

    End Sub

#End Region

End Class
