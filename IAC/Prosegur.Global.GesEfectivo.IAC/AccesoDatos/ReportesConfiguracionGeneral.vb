Imports Prosegur.DbHelper

Public Class ReportesConfiguracionGeneral

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta as configurações gerais do relatório
    ''' </summary>
    ''' <param name="IdentificadorConfiguracion"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeletarReportesConfiguracion(IdentificadorConfiguracion As String, ByRef objTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ReporteConfiguracionDeletar)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, IdentificadorConfiguracion))

        objTransacion.AdicionarItemTransacao(cmd)

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere as configurações gerais para o reporte da configuração.
    ''' </summary>
    ''' <param name="IdentificadorConfiguracion"></param>
    ''' <param name="IdentificadorReporte"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub InserirReportesConfiguracion(IdentificadorConfiguracion As String, IdentificadorReporte As String, _
                                                   ByRef objTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ReportesConfiguracionInserir)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_REPORTE_CONFIGURACION", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, IdentificadorConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION_GENERAL", ProsegurDbType.Objeto_Id, IdentificadorReporte))

        objTransacion.AdicionarItemTransacao(cmd)
    End Sub

#End Region

End Class
