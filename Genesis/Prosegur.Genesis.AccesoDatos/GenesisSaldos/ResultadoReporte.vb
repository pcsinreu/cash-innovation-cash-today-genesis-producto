Imports Prosegur.DbHelper
Imports System.Text
Imports Prosegur.Genesis.ContractoServicio.GenesisSaldos
Imports Prosegur.Genesis.Comon
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Framework.Dicionario
Imports System.Transactions

Namespace GenesisSaldos

    Public Class ResultadoReporte

#Region "[ACTUALIZAR]"

        Public Shared Sub ActualizarResultadoReporte(IdentificadorConfiguracionReporte As String,
                                                     CodigoEstadoReporte As String,
                                                     FechaInicio As Nullable(Of DateTime),
                                                     FechaFin As Nullable(Of DateTime),
                                                     DescripcionErroEjecucion As String,
                                                     IdentificadorCertificado As String,
                                                     IdentificadorSubCanal As String,
                                                     ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.ResultadoReporteAcutalizar
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SITUACION_REPORTE", ProsegurDbType.Identificador_Alfanumerico, CodigoEstadoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ENTIDAD_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorCertificado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, IdentificadorSubCanal))

            Dim query As New System.Text.StringBuilder

            If FechaInicio IsNot Nothing Then
                query.Append(" ,FYH_INICIO_EJECUCION = []FYH_INICIO_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_INICIO_EJECUCION", ProsegurDbType.Data_Hora, FechaInicio))
            End If

            If FechaFin IsNot Nothing Then
                query.Append(" ,FYH_FIN_EJECUCION = []FYH_FIN_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_FIN_EJECUCION", ProsegurDbType.Data_Hora, FechaFin))
            Else
                query.Append(" ,FYH_FIN_EJECUCION = []FYH_FIN_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_FIN_EJECUCION", ProsegurDbType.Data_Hora, DBNull.Value))
            End If

            If Not String.IsNullOrEmpty(DescripcionErroEjecucion) Then
                query.Append(" ,DES_ERROR_EJECUCION = []DES_ERROR_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ERROR_EJECUCION", ProsegurDbType.Observacao_Longa, DescripcionErroEjecucion))
            Else
                query.Append(" ,DES_ERROR_EJECUCION = []DES_ERROR_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ERROR_EJECUCION", ProsegurDbType.Observacao_Longa, DBNull.Value))
            End If

            If query.Length > 0 Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, String.Empty))
            End If


            If objTransacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            Else
                objTransacion.AdicionarItemTransacao(cmd)
            End If

        End Sub

        Public Shared Sub ActualizarResultadoReporte(IdentificadorResultadoReporte As String,
                                                     CodigoEstadoReporte As String,
                                                     FechaInicio As Nullable(Of DateTime),
                                                     FechaFin As Nullable(Of DateTime),
                                                     DescripcionErroEjecucion As String,
                                                     ByRef objTransacion As Transacao,
                                                     Optional NombreArchivoReporte As String = Nothing)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = My.Resources.ResultadoReporteActualizarConIdentificador
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_RESULTADO_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorResultadoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SITUACION_REPORTE", ProsegurDbType.Identificador_Alfanumerico, CodigoEstadoReporte))

            Dim query As New System.Text.StringBuilder

            If FechaInicio IsNot Nothing Then
                query.Append(" ,FYH_INICIO_EJECUCION = []FYH_INICIO_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_INICIO_EJECUCION", ProsegurDbType.Data_Hora, FechaInicio))
            End If

            If FechaFin IsNot Nothing Then
                query.Append(" ,FYH_FIN_EJECUCION = []FYH_FIN_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_FIN_EJECUCION", ProsegurDbType.Data_Hora, FechaFin))
            Else
                query.Append(" ,FYH_FIN_EJECUCION = []FYH_FIN_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_FIN_EJECUCION", ProsegurDbType.Data_Hora, DBNull.Value))
            End If

            If Not String.IsNullOrEmpty(DescripcionErroEjecucion) Then
                query.Append(" ,DES_ERROR_EJECUCION = []DES_ERROR_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ERROR_EJECUCION", ProsegurDbType.Observacao_Longa, DescripcionErroEjecucion))
            Else
                query.Append(" ,DES_ERROR_EJECUCION = []DES_ERROR_EJECUCION ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_ERROR_EJECUCION", ProsegurDbType.Observacao_Longa, DBNull.Value))
            End If

            If NombreArchivoReporte IsNot Nothing Then
                query.Append(" ,DES_NOMBRE_ARCHIVO = []DES_NOMBRE_ARCHIVO ")
                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_NOMBRE_ARCHIVO", ProsegurDbType.Descricao_Longa, NombreArchivoReporte))
            End If

            If query.Length > 0 Then
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, query.ToString))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, String.Empty))
            End If


            If objTransacion Is Nothing Then
                AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_GENESIS, cmd)
            Else
                objTransacion.AdicionarItemTransacao(cmd)
            End If

        End Sub


#End Region

#Region "[INSERIR]"

        Public Shared Function InserirResultadoReporte(IdentificadorConfiguracionReporte As String,
                                                  CodigoEstadoReporte As String,
                                                  FechaInicio As DateTime,
                                                  IdentificadorCertificado As String,
                                                  IdentificadorSubCanal As String,
                                                  ByRef objTransacion As Transacao) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ResultadoReporteInserir)
            cmd.CommandType = CommandType.Text
            Dim IdentificadorResultadoReporte As String = Guid.NewGuid.ToString

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_RESULTADO_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorResultadoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "COD_SITUACION_REPORTE", ProsegurDbType.Identificador_Alfanumerico, CodigoEstadoReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ENTIDAD_REPORTE", ProsegurDbType.Objeto_Id, If(String.IsNullOrEmpty(IdentificadorCertificado),
                                                                                                                                                        DBNull.Value, IdentificadorCertificado)))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "FYH_INICIO_EJECUCION", ProsegurDbType.Data_Hora, FechaInicio))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, If(String.IsNullOrEmpty(IdentificadorSubCanal),
                                                                                                                                                 DBNull.Value, IdentificadorSubCanal)))

            objTransacion.AdicionarItemTransacao(cmd)

            Return IdentificadorResultadoReporte
        End Function

#End Region

#Region "[SELECIONAR]"

        Public Shared Function RecuperarIdentificadorResultadoReporte(IdentificadorConfiguracionReporte As String,
                                                                      IdentificadorCertificado As String,
                                                                      IdentificadorSubCanal As String) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ResultadoReporteRecuperarIdentificador)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionReporte))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_ENTIDAD_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorCertificado))
            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_SUBCANAL", ProsegurDbType.Objeto_Id, IdentificadorSubCanal))

            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function

        Public Shared Function RecuperarIdentificadorResultadoReporteConParametros(IdentificadorConfiguracionReporte As String,
                                                                                   objParametros As ObservableCollection(Of Clases.ParametroReporte)) As String

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            Dim QueryInner As New StringBuilder
            Dim QueryWhere As New StringBuilder

            cmd.CommandText = My.Resources.ResultadoReporteRecuperarIdentificadorConParametros
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionReporte))

            If objParametros IsNot Nothing AndAlso objParametros.Count > 0 Then

                Dim Indice As Integer = 0

                For Each objParametro In objParametros.FindAll(Function(p) Not String.IsNullOrEmpty(p.DescripcionValor))

                    Indice += 1

                    QueryInner.AppendLine(String.Format(" INNER JOIN SAPR_TRESULTADO_RPXPARAMETRO RRPXP{0} ON RRPXP{0}.OID_RESULTADO_REPORTE = RP.OID_RESULTADO_REPORTE ", Indice.ToString))
                    QueryInner.AppendLine(String.Format(" INNER JOIN SAPR_TTIPO_REPORTEXPARAMETRO TRXP{0} ON TRXP{0}.OID_TIPO_REPORTEXPARAMETRO = RRPXP{0}.OID_TIPO_REPORTEXPARAMETRO ", Indice.ToString))

                    QueryWhere.AppendLine(String.Format(" AND RRPXP{0}.DES_VALOR_PARAMETRO = []DES_VALOR_PARAMETRO{0} AND TRXP{0}.COD_PARAMETRO = []COD_PARAMETRO{0}", Indice.ToString))

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, String.Format("DES_VALOR_PARAMETRO{0}", Indice.ToString), ProsegurDbType.Observacao_Longa, objParametro.DescripcionValor))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, String.Format("COD_PARAMETRO{0}", Indice.ToString), ProsegurDbType.Descricao_Longa, objParametro.Codigo))

                Next

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, QueryInner.ToString, QueryWhere.ToString))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, String.Empty, String.Empty))
            End If



            Return AcessoDados.ExecutarScalar(Constantes.CONEXAO_GENESIS, cmd)
        End Function
        Public Shared Function ObtenerResultadosReportesAbono(IdentificadorConfiguracionReporte As String,
                                                                                   objParametros As ObservableCollection(Of Clases.ParametroReporte)) As List(Of Clases.Abono.ReporteAbono)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As New DataTable
            Dim reportes As New List(Of Clases.Abono.ReporteAbono)

            Dim QueryInner As New StringBuilder
            Dim QueryWhere As New StringBuilder

            cmd.CommandText = My.Resources.ResultadoReporteRecuperarConParametros
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorConfiguracionReporte))

            If objParametros IsNot Nothing AndAlso objParametros.Count > 0 Then

                Dim Indice As Integer = 0

                For Each objParametro In objParametros.FindAll(Function(p) Not String.IsNullOrEmpty(p.DescripcionValor))

                    Indice += 1

                    QueryInner.AppendLine(String.Format(" INNER JOIN SAPR_TRESULTADO_RPXPARAMETRO RRPXP{0} ON RRPXP{0}.OID_RESULTADO_REPORTE = RP.OID_RESULTADO_REPORTE ", Indice.ToString))
                    QueryInner.AppendLine(String.Format(" INNER JOIN SAPR_TTIPO_REPORTEXPARAMETRO TRXP{0} ON TRXP{0}.OID_TIPO_REPORTEXPARAMETRO = RRPXP{0}.OID_TIPO_REPORTEXPARAMETRO ", Indice.ToString))

                    QueryWhere.AppendLine(String.Format(" AND RRPXP{0}.DES_VALOR_PARAMETRO = []DES_VALOR_PARAMETRO{0} AND TRXP{0}.COD_PARAMETRO = []COD_PARAMETRO{0}", Indice.ToString))

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, String.Format("DES_VALOR_PARAMETRO{0}", Indice.ToString), ProsegurDbType.Observacao_Longa, objParametro.DescripcionValor))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, String.Format("COD_PARAMETRO{0}", Indice.ToString), ProsegurDbType.Descricao_Longa, objParametro.Codigo))

                Next

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, QueryInner.ToString, QueryWhere.ToString))
            Else
                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, String.Format(cmd.CommandText, String.Empty, String.Empty))
            End If

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                For Each dr In dt.Rows
                    Dim reporte As New Clases.Abono.ReporteAbono
                    reporte.Identificador = Util.AtribuirValorObj(dr("OID_RESULTADO_REPORTE"), GetType(String))
                    reporte.Tipo = RecuperarEnum(Of Enumeradores.TipoReporte)(Util.AtribuirValorObj(dr("NEL_TIPO_REPORTE"), GetType(String)))
                    reporte.IdentificadorAbono = Util.AtribuirValorObj(dr("OID_ENTIDAD_REPORTE"), GetType(String))
                    reporte.CodigoSituacion = Util.AtribuirValorObj(dr("COD_SITUACION_REPORTE"), GetType(String))
                    reporte.DesErrorEjecucion = Util.AtribuirValorObj(dr("DES_ERROR_EJECUCION"), GetType(String))
                    If reporte.CodigoSituacion <> "PE" Then
                        reporte.NombreArchivo = Util.AtribuirValorObj(dr("DES_NOMBRE_ARCHIVO"), GetType(String))
                    End If

                    If reportes.Count = 0 Then
                        reportes.Add(reporte)
                    ElseIf reportes.Count > 0 AndAlso reportes(0).CodigoSituacion = "PE" Then
                        reportes.Clear()
                        reportes.Add(reporte)
                    End If

                Next

            End If

            Return reportes

        End Function
#End Region

#Region "[DELETAR]"

#End Region

    End Class

End Namespace