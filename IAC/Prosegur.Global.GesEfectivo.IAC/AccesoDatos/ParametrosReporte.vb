Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Public Class ParametrosReporte

#Region "[CONSULTAR]"

    ' ''' <summary>
    ' ''' Recupera os parametros dos relatorios.
    ' ''' </summary>
    ' ''' <param name="OidConfiguracion"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Shared Function RecuperarParametrosReporte(OidConfiguracion As String) As Reportes.ParametroReporteColeccion

    '    Dim objColParametrosReporte As Reportes.ParametroReporteColeccion = Nothing

    '    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

    '    cmd.CommandText = Util.PrepararQuery(My.Resources.ParametrosReporteRecuperar.ToString)
    '    cmd.CommandType = CommandType.Text

    '    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, OidConfiguracion))

    '    Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

    '        objColParametrosReporte = New Reportes.ParametroReporteColeccion

    '        For Each dr As DataRow In dt.Rows

    '            objColParametrosReporte.Add(New Reportes.ParametroReporte With { _
    '                                        .CodParametro = Util.AtribuirValorObj(dr("COD_PARAMETRO"), GetType(String)), _
    '                                        .DesParametro = Util.AtribuirValorObj(dr("DES_PARAMETRO"), GetType(String)), _
    '                                        .DesValorParametro = Util.AtribuirValorObj(dr("DES_VALOR_PARAMETRO"), GetType(String))})
    '        Next

    '    End If

    '    Return objColParametrosReporte
    'End Function

    ''' <summary>
    ''' Retorna o Oid do parametro.
    ''' </summary>
    ''' <param name="OidConfiguracion"></param>
    ''' <param name="CodNombreParametro"></param>
    ''' <param name="CodParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornarOidParametro(OidConfiguracion As String, CodNombreParametro As String, CodParametro As String) As String

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.ParametroReporteRetornarOid)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, OidConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NOMBRE_PARAMETRO", ProsegurDbType.Descricao_Longa, CodNombreParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Descricao_Longa, CodParametro))

        Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GE, cmd)
    End Function

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere os parametros do reporte.
    ''' </summary>
    ''' <param name="IdentificadorConfiguracion"></param>
    ''' <param name="objParametro"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Function InserirParametros(IdentificadorConfiguracion As String, objParametro As Reportes.ParametroReporte, ByRef objTransacion As Transacao) As String

        Dim oidParametro As String = Guid.NewGuid.ToString

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.ParametroReporteInserir)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, oidParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, IdentificadorConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_NOMBRE_PARAMETRO", ProsegurDbType.Descricao_Longa, objParametro.CodNombreParametro.ToUpper))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_PARAMETRO", ProsegurDbType.Descricao_Longa, objParametro.CodParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PARAMETRO", ProsegurDbType.Observacao_Curta, If(String.IsNullOrEmpty(objParametro.DesParametro), DBNull.Value, objParametro.DesParametro)))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "ES_TODOS", ProsegurDbType.Logico, objParametro.EsTodos))

        objTransacion.AdicionarItemTransacao(cmd)

        Return oidParametro
    End Function

#End Region

#Region "[DELETAR]"

    ''' <summary>
    ''' Deleta todos os parametros .
    ''' </summary>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub DeletarParametros(IdentificadorConfiguracion As String, ByRef objTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = My.Resources.ParametrosReporteDeletar
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, IdentificadorConfiguracion))
        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        objTransacion.AdicionarItemTransacao(cmd)
    End Sub

#End Region

#Region "[ACTUALIZAR]"

    ''' <summary>
    ''' Atualiza a descrição do parametro
    ''' </summary>
    ''' <param name="OidParametro"></param>
    ''' <param name="DesParametro"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub ActualizarParametro(OidParametro As String, DesParametro As String, ByRef objTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = My.Resources.ParametroReporteActualizar
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_PARAMETRO", ProsegurDbType.Objeto_Id, OidParametro))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_PARAMETRO", ProsegurDbType.Observacao_Curta, DesParametro))

        objTransacion.AdicionarItemTransacao(cmd)
    End Sub

#End Region

End Class
