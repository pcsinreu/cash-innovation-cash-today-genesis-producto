Imports Prosegur.DbHelper
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon

Namespace Genesis
    Public Class InformacaoAdicionalCliente

        ''' <summary>
        ''' Recupera as informações adicionais ativas e do saldos
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerInformacoesAdicionais() As List(Of Clases.InformacaoAdicionalCliente)

            Dim informacoes As New List(Of Clases.InformacaoAdicionalCliente)
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InformacaoAdicionalRecuperarTodos)
            cmd.CommandType = CommandType.Text

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    Dim inf As New Clases.InformacaoAdicionalCliente
                    With inf
                        .Identificador = Util.AtribuirValorObj(row("OID_IAC"), GetType(String))
                        .Codigo = Util.AtribuirValorObj(row("COD_IAC"), GetType(String))
                        .Descripcion = Util.AtribuirValorObj(row("DES_IAC"), GetType(String))
                        .Observacion = Util.AtribuirValorObj(row("OBS_IAC"), GetType(String))
                        .EsActivo = Util.AtribuirValorObj(row("BOL_VIGENTE"), GetType(Boolean))
                        .Usuario = Util.AtribuirValorObj(row("COD_USUARIO"), GetType(String))
                        .FechaHoraActualizacion = Util.AtribuirValorObj(row("FYH_ACTUALIZACION"), GetType(DateTime))
                        .BolCopiaDeclarados = Util.AtribuirValorObj(row("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                        .BolInvisible = Util.AtribuirValorObj(row("BOL_INVISIBLE"), GetType(Boolean))
                        .BolEspecificoSaldos = Util.AtribuirValorObj(row("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                    End With
                    informacoes.Add(inf)
                Next
            End If

            Return informacoes
        End Function

        ''' <summary>
        ''' Recupera informação adicional
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ObtenerInformacaoAdicional(identificador As String) As Clases.InformacaoAdicionalCliente

            Dim informacao As Clases.InformacaoAdicionalCliente
            Dim cmd As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_GENESIS)
            Dim dt As DataTable

            cmd.CommandText = Util.PrepararQuery(Constantes.CONEXAO_GENESIS, My.Resources.InformacaoAdicionalRecuperar)
            cmd.CommandType = CommandType.Text

            cmd.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Prosegur.Genesis.AccesoDatos.Constantes.CONEXAO_GENESIS, "OID_IAC", ProsegurDbType.Objeto_Id, identificador))

            dt = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_GENESIS, cmd)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                informacao = New Clases.InformacaoAdicionalCliente
                With informacao
                    .Identificador = Util.AtribuirValorObj(dt.Rows(0)("OID_IAC"), GetType(String))
                    .Codigo = Util.AtribuirValorObj(dt.Rows(0)("COD_IAC"), GetType(String))
                    .Descripcion = Util.AtribuirValorObj(dt.Rows(0)("DES_IAC"), GetType(String))
                    .Observacion = Util.AtribuirValorObj(dt.Rows(0)("OBS_IAC"), GetType(String))
                    .EsActivo = Util.AtribuirValorObj(dt.Rows(0)("BOL_VIGENTE"), GetType(Boolean))
                    .Usuario = Util.AtribuirValorObj(dt.Rows(0)("COD_USUARIO"), GetType(String))
                    .FechaHoraActualizacion = Util.AtribuirValorObj(dt.Rows(0)("FYH_ACTUALIZACION"), GetType(DateTime))
                    .BolCopiaDeclarados = Util.AtribuirValorObj(dt.Rows(0)("BOL_COPIA_DECLARADOS"), GetType(Boolean))
                    .BolInvisible = Util.AtribuirValorObj(dt.Rows(0)("BOL_INVISIBLE"), GetType(Boolean))
                    .BolEspecificoSaldos = Util.AtribuirValorObj(dt.Rows(0)("BOL_ESPECIFICO_DE_SALDOS"), GetType(Boolean))
                End With
            Else
                Return Nothing
            End If

            Return informacao
        End Function

    End Class
End Namespace
