Imports Prosegur.Genesis.Comon
Imports Prosegur.DbHelper
Imports System.Collections.ObjectModel

Namespace GenesisSaldos
    Public Class ParametroReporte

        Public Shared Function RecuperarParametrosPorTipo(tipoReporte As Enumeradores.TipoReporte) As List(Of Clases.ParametroReporte)

            Dim lstParametroReporte As New List(Of Clases.ParametroReporte)

            Using cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.RecuperarParametrosPorTipo)
                cmd.CommandType = CommandType.Text

                cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "NEL_TIPO_REPORTE", ProsegurDbType.Inteiro_Longo, Convert.ToInt32(tipoReporte)))

                cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, cmd.CommandText)
                Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    For Each dr In dt.Rows

                        Dim objParametroReporte As New Clases.ParametroReporte With { _
                                                                  .Identificador = Util.AtribuirValorObj(dr("OID_TIPO_REPORTEXPARAMETRO"), GetType(String)), _
                                                                  .Codigo = Util.AtribuirValorObj(dr("COD_PARAMETRO"), GetType(String)), _
                                                                  .Descripcion = Util.AtribuirValorObj(dr("DES_PARAMETRO"), GetType(String))}

                        lstParametroReporte.Add(objParametroReporte)

                    Next

                End If

            End Using

            Return lstParametroReporte

        End Function

        Public Shared Sub InserirParametrosConfigCertificado(IdentificadorConfigReporte As String, parametros As ObservableCollection(Of Clases.ParametroReporte), ByRef objTransacion As Transacao)

            If parametros IsNot Nothing Then
                For Each objParametro As Clases.ParametroReporte In parametros
                    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    Dim OidConfigXParametro As String = Guid.NewGuid.ToString

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InserirParametrosConfigCertificado)
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIGRPXPARAMETRO", ProsegurDbType.Objeto_Id, OidConfigXParametro))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Identificador_Alfanumerico, IdentificadorConfigReporte))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_REPORTEXPARAMETRO", ProsegurDbType.Identificador_Alfanumerico, objParametro.Identificador))

                    objTransacion.AdicionarItemTransacao(cmd)
                Next
            End If

        End Sub

        Public Shared Sub InserirValorParametrosConfigCertificado(IdentificadorResultadoReporte As String,
                                                                  parametros As ObservableCollection(Of Clases.ParametroReporte),
                                                                  ByRef objTransacion As Transacao)

            If parametros IsNot Nothing Then
                For Each objParametro As Clases.ParametroReporte In parametros
                    Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
                    Dim OidConfigXParametro As String = Guid.NewGuid.ToString

                    cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParametroReporteInserirValor)
                    cmd.CommandType = CommandType.Text

                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_RESULTADO_RPXPARAMETRO", ProsegurDbType.Objeto_Id, OidConfigXParametro))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_RESULTADO_REPORTE", ProsegurDbType.Identificador_Alfanumerico, IdentificadorResultadoReporte))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_TIPO_REPORTEXPARAMETRO", ProsegurDbType.Identificador_Alfanumerico, objParametro.Identificador))
                    cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "DES_VALOR_PARAMETRO", ProsegurDbType.Identificador_Alfanumerico, objParametro.DescripcionValor))

                    objTransacion.AdicionarItemTransacao(cmd)
                Next
            End If

        End Sub

        Public Shared Sub ExcluirParametrosConfigCertificado(OidConfigReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ExcluirParametrosConfigCertificado)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_CONFIG_REPORTE", ProsegurDbType.Objeto_Id, OidConfigReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub

        Public Shared Sub ExcluirValorParametrosConfigCertificado(IdentificadorResultadoReporte As String, ByRef objTransacion As Transacao)

            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.ParametroReporteExcluirValor)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_GENESIS, "OID_RESULTADO_REPORTE", ProsegurDbType.Objeto_Id, IdentificadorResultadoReporte))

            objTransacion.AdicionarItemTransacao(cmd)

        End Sub


    End Class
End Namespace

