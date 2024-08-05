Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Public Class ConfiguracionReportes

#Region "[CONSULTAR]"

    Public Shared Function RetornarConfiguracionesReportes(Peticion As Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion) As Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion
        Dim objConfiguracionesReportes As Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion = Nothing

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        'Configurações
        cmd.CommandText = My.Resources.ConfiguracionesReportesRecuperar
        cmd.CommandType = CommandType.Text

        If Peticion IsNot Nothing AndAlso Peticion.DesConfiguracion IsNot Nothing AndAlso Not String.IsNullOrEmpty(Peticion.DesConfiguracion) Then
            cmd.CommandText = cmd.CommandText & " WHERE UPPER(DES_CONFIGURACION) LIKE :DES_CONFIGURACION "
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, Peticion.DesConfiguracion.ToUpper() & "%"))
        End If


        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cmd.Parameters.Clear()
            objConfiguracionesReportes = New Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion
            For Each dr As DataRow In dt.Rows
                'DES_REPORTE
                cmd.CommandText = "SELECT G.DES_REPORTE FROM IAPR_TCONFIG_GENERAL_REPORTES G "
                cmd.CommandText = cmd.CommandText & "WHERE G.OID_CONFIGURACION_GENERAL IN (SELECT OID_CONFIGURACION_GENERAL FROM IAPR_TREPORTES_CONFIGURACION "
                cmd.CommandText = cmd.CommandText & "WHERE OID_CONFIGURACION = '" & dr("OID_CONFIGURACION") & "')"
                Dim dtDesReporte As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
                Dim reportes As New List(Of String)
                If dtDesReporte IsNot Nothing AndAlso dtDesReporte.Rows.Count > 0 Then
                    For Each dr2 As DataRow In dtDesReporte.Rows
                        reportes.Add(dr2("DES_REPORTE"))
                    Next
                End If

                'Recupera os parametros de delegações (P_CON_DELEGACION_USUARIO,P_CON_DELEGACION) da configuração
                cmd.CommandText = "SELECT COD_PARAMETRO,COD_NOMBRE_PARAMETRO FROM IAPR_TPARAMETROS_REPORTES "
                cmd.CommandText = cmd.CommandText & "WHERE OID_CONFIGURACION = '" & dr("OID_CONFIGURACION") & "' AND (COD_NOMBRE_PARAMETRO = 'P_COM_DELEGACION' "
                cmd.CommandText = cmd.CommandText & "OR COD_NOMBRE_PARAMETRO = 'P_COM_DELEGACION_USUARIO')"
                Dim dtDelegaciones As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
                Dim delegaciones As New List(Of Reportes.GetConfiguracionesReportes.ParametroDelegaciones)
                If dtDelegaciones IsNot Nothing AndAlso dtDelegaciones.Rows.Count > 0 Then
                    For Each dr2 As DataRow In dtDelegaciones.Rows
                        Dim delegacion As New Reportes.GetConfiguracionesReportes.ParametroDelegaciones
                        delegacion.codDelegacion = dr2("COD_NOMBRE_PARAMETRO")
                        delegacion.ParametroDelegacion = dr2("COD_PARAMETRO")
                        delegaciones.Add(delegacion)
                    Next
                End If

                objConfiguracionesReportes.Add(New Reportes.GetConfiguracionesReportes.ConfiguracionReporte With { _
                                               .DesConfiguracion = Util.AtribuirValorObj(dr("DES_CONFIGURACION"), GetType(String)), _
                                               .DesReporte = reportes, _
                                               .Delegaciones = delegaciones, _
                                               .IdentificadorConfiguracion = Util.AtribuirValorObj(dr("OID_CONFIGURACION"), GetType(String))})
            Next

        End If

        Return objConfiguracionesReportes
    End Function


    ''' <summary>
    ''' Recupera as configurações
    ''' </summary>
    ''' <param name="OidConfiguracion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 29/04/2013 - Criado
    ''' [victor.ramos] 16/07/2013 - Modificado
    ''' </history>
    Public Shared Function RetornarConfiguracion(OidConfiguracion As List(Of String)) As Reportes.ConfiguracionReporteColeccion

        Dim objConfiguracionesReportes As Reportes.ConfiguracionReporteColeccion = Nothing

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.ConfiguracionReportesRecuperar
        cmd.CommandType = CommandType.Text
        Dim query As New StringBuilder

        query.Append(Util.MontarClausulaIn(OidConfiguracion, "OID_CONFIGURACION", cmd, "WHERE"))

        cmd.CommandText &= query.ToString
        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

            objConfiguracionesReportes = New Reportes.ConfiguracionReporteColeccion

            'Para cada configuração
            For Each dr As DataRow In dt.Rows
                cmd.Parameters.Clear()
                'Reportes
                Dim reportes As New Reportes.ReportesColeccion
                'TODO - ADD o arquivo .sql no Resource
                'cmd.CommandText = My.Resources.repor
                cmd.CommandText = "SELECT G.COD_REPORTE, G.OID_CONFIGURACION_GENERAL, G.DES_REPORTE,G.NEC_FORMATO_ARCHIVO,G.DES_EXTENSION_ARCHIVO,G.COD_SEPARADOR FROM IAPR_TCONFIG_GENERAL_REPORTES G "
                cmd.CommandText = cmd.CommandText & "WHERE G.OID_CONFIGURACION_GENERAL IN (SELECT OID_CONFIGURACION_GENERAL FROM IAPR_TREPORTES_CONFIGURACION "
                cmd.CommandText = cmd.CommandText & "WHERE OID_CONFIGURACION = '" & dr("OID_CONFIGURACION") & "')"

                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, dr("OID_CONFIGURACION")))
                Dim dtReportes As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
                If dtReportes IsNot Nothing AndAlso dtReportes.Rows.Count > 0 Then
                    For Each rep In dtReportes.Rows
                        Dim reporte As New Reportes.Reportes
                        reporte.IdReporte = Util.AtribuirValorObj(rep("COD_REPORTE"), GetType(String))
                        reporte.DesReporte = Util.AtribuirValorObj(rep("DES_REPORTE"), GetType(String))
                        reporte.ExtensionArchivo = Util.AtribuirValorObj(rep("DES_EXTENSION_ARCHIVO"), GetType(String))
                        reporte.NecFormatoArchivo = Util.AtribuirValorObj(rep("NEC_FORMATO_ARCHIVO"), GetType(String))
                        reporte.Separador = Util.AtribuirValorObj(rep("COD_SEPARADOR"), GetType(String))
                        reporte.IdentificadorConfiguracionGeneral = Util.AtribuirValorObj(rep("OID_CONFIGURACION_GENERAL"), GetType(String))
                        reportes.Add(reporte)
                    Next
                End If

                'Parametro da configuração
                cmd.Parameters.Clear()
                Dim parametros As New Reportes.ParametroReporteColeccion
                'TODO - ADD o arquivo .sql no Resource
                'cmd.CommandText = My.Resources.GetParametrosConfiguracoes
                'cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Identificador_Alfanumerico, dr("OID_CONFIGURACION")))
                cmd.CommandText = "SELECT COD_PARAMETRO,COD_NOMBRE_PARAMETRO,DES_PARAMETRO,ES_TODOS FROM IAPR_TPARAMETROS_REPORTES "
                cmd.CommandText = cmd.CommandText & "WHERE OID_CONFIGURACION = '" & dr("OID_CONFIGURACION") & "'"
                Dim dtParametros As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GE, cmd)
                If dtParametros IsNot Nothing AndAlso dtParametros.Rows.Count > 0 Then
                    For Each param In dtParametros.Rows
                        Dim parametro As New Reportes.ParametroReporte
                        parametro.CodParametro = Util.AtribuirValorObj(param("COD_PARAMETRO"), GetType(String))
                        parametro.CodNombreParametro = Util.AtribuirValorObj(param("COD_NOMBRE_PARAMETRO"), GetType(String))
                        parametro.DesParametro = Util.AtribuirValorObj(param("DES_PARAMETRO"), GetType(String))
                        parametro.EsTodos = Util.AtribuirValorObj(param("ES_TODOS"), GetType(Integer))
                        parametros.Add(parametro)
                    Next
                End If

                objConfiguracionesReportes.Add(New Reportes.ConfiguracionReporte With { _
                                               .IdentificadorConfiguracion = Util.AtribuirValorObj(dr("OID_CONFIGURACION"), GetType(String)), _
                                               .DesConfiguracion = Util.AtribuirValorObj(dr("DES_CONFIGURACION"), GetType(String)), _
                                               .DesRuta = Util.AtribuirValorObj(dr("DES_RUTA"), GetType(String)), _
                                               .CodUsuario = Util.AtribuirValorObj(dr("COD_USUARIO"), GetType(String)), _
                                               .NombreArchivo = Util.AtribuirValorObj(dr("DES_NOMBRE_ARCHIVO"), GetType(String)), _
                                               .FyhActualizacion = Util.AtribuirValorObj(dr("FYH_ACTUALIZACION"), GetType(DateTime)), _
                                               .Reportes = reportes, _
                                               .ParametrosReporte = parametros})
            Next

        End If

        Return objConfiguracionesReportes
    End Function

#End Region

#Region "[EXCLUIR]"

    ''' <summary>
    ''' Exclui as configurações informadas.
    ''' </summary>
    ''' <param name="IdentificadoresConfiguracion"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    ''' [anselmo.gois] 06/05/2013 - Criado
    Public Shared Sub ExcluirConfiguracionReporte(IdentificadoresConfiguracion As List(Of String), ByRef objTransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = My.Resources.ConfiguracionReporteExcluir
        cmd.CommandType = CommandType.Text

        cmd.CommandText &= Util.MontarClausulaIn(IdentificadoresConfiguracion, "OID_CONFIGURACION", cmd, "WHERE")
        cmd.CommandText = Util.PrepararQuery(cmd.CommandText)

        objTransacion.AdicionarItemTransacao(cmd)

    End Sub

#End Region

#Region "[INSERIR]"

    ''' <summary>
    ''' Insere as configurações
    ''' </summary>
    ''' <param name="ConfiguracionReporte"></param>
    ''' <param name="objTransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub InserirConfiguracionReporte(ByRef ConfiguracionReporte As Reportes.ConfiguracionReporte, ByRef objTransacion As Transacao)

        ConfiguracionReporte.IdentificadorConfiguracion = Guid.NewGuid.ToString

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)
        cmd.CommandText = Util.PrepararQuery(My.Resources.ConfiguracionReporteInserir)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, ConfiguracionReporte.IdentificadorConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CONFIGURACION", ProsegurDbType.Descricao_Longa, ConfiguracionReporte.DesConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_RUTA", ProsegurDbType.Observacao_Curta, ConfiguracionReporte.DesRuta))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Curta, ConfiguracionReporte.CodUsuario))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, ConfiguracionReporte.FyhActualizacion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_NOMBRE_ARCHIVO", ProsegurDbType.Observacao_Curta, ConfiguracionReporte.NombreArchivo))

        objTransacion.AdicionarItemTransacao(cmd)
    End Sub

#End Region

#Region "[MODIFICAR]"

    ''' <summary>
    ''' Atualiza as configurações.
    ''' </summary>
    ''' <param name="objConfiguracion"></param>
    ''' <param name="objtransacion"></param>
    ''' <remarks></remarks>
    Public Shared Sub ActualizarConfiguracion(objConfiguracion As Reportes.ConfiguracionReporte, ByRef objtransacion As Transacao)

        Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GE)

        cmd.CommandText = Util.PrepararQuery(My.Resources.ConfiguracionReporteActualizar)
        cmd.CommandType = CommandType.Text

        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "OID_CONFIGURACION", ProsegurDbType.Objeto_Id, objConfiguracion.IdentificadorConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_RUTA", ProsegurDbType.Observacao_Curta, objConfiguracion.DesRuta))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "COD_USUARIO", ProsegurDbType.Descricao_Curta, objConfiguracion.CodUsuario))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "FYH_ACTUALIZACION", ProsegurDbType.Data_Hora, DateTime.Now))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_CONFIGURACION", ProsegurDbType.Descricao_Longa, objConfiguracion.DesConfiguracion))
        cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GE, "DES_NOMBRE_ARCHIVO", ProsegurDbType.Observacao_Curta, objConfiguracion.NombreArchivo))

        objtransacion.AdicionarItemTransacao(cmd)
    End Sub
#End Region

End Class

