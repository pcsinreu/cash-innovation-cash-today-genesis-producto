Imports System.Data.OracleClient
Imports Prosegur.DbHelper
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Text
Imports System.IO
Imports System.Xml.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Configuration.ConfigurationManager
Imports Prosegur.Global
Imports System.Web.Caching
Imports System.Web

''' <summary>
''' Classe com utilidades comuns
''' </summary>
''' <remarks></remarks>
''' <history>
''' [cbotmempo] 09/02/2009 Criado
''' </history>
Public Class Util

    'Private Shared _Delegaciones As Seguridad.ContractoServicio.ObtenerDelegaciones.DelegacionColeccion
    Private Shared _Versao As String

    Public Shared Property Delegaciones() As Seguridad.ContractoServicio.ObtenerDelegaciones.DelegacionColeccion
        Get
            Dim c As Cache = HttpRuntime.Cache
            Return c("Delegaciones")
        End Get
        Set(value As Seguridad.ContractoServicio.ObtenerDelegaciones.DelegacionColeccion)
            Dim c As Cache = HttpRuntime.Cache
            c.Add("Delegaciones", value, Nothing, DateTime.Now.AddHours(24), Cache.NoSlidingExpiration, CacheItemPriority.High, Nothing)
        End Set
    End Property


    ''' <summary>
    ''' Trata erros da base.
    ''' </summary>
    ''' <param name="Ex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 21/05/2009 Criado
    ''' </history>
    Public Shared Function TratarErroBanco(Ex As Exception) As String

        If TypeOf (Ex) Is OracleClient.OracleException Then

            ' O erro é do Oracle
            Select Case DirectCast(Ex, OracleClient.OracleException).Code

                Case 1 ' erro ao tentar incluir informações já existentes
                    Return Traduzir("lbl_erro_chave_unica")

                Case 2292 ' erro ao tentar excluir registros que possuem filhos
                    Return Traduzir("lbl_erro_excluir_registro_pai")

            End Select

        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' Valida unicode
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [msoliveira] 27/05/2009 Criado
    ''' </history>
    Public Shared Function ValidarUNICODE(valor As String) As Integer

        Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
        comando.CommandType = CommandType.StoredProcedure
        comando.CommandText = "PD_ValidarUNICODE"

        ' parametros de verificação
        comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "Valor", ProsegurDbType.Descricao_Longa, valor))

        Dim dt As DataTable = AcessoDados.ExecutarDataTable(Constantes.CONEXAO_SALDOS, comando)
        Dim retorno As Integer = 0

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            retorno = Convert.ToInt32(dt.Rows(0)(0))
        End If

        Return retorno

    End Function

    ''' <summary>
    ''' Grava erro no banco informando a exception e o usuario, caso o banco esteja offline, o erro será gravado no event viewer
    ''' </summary>
    ''' <param name="Erro"></param>
    ''' <param name="Usuario"></param>
    ''' <remarks></remarks>
    Public Shared Sub GravarErroNoBanco(Erro As Exception, _
                                        Usuario As String)

        ' Se o objeto não está vazio
        If Erro IsNot Nothing Then

            ' Se o objeto não está vazio
            If (Erro.InnerException IsNot Nothing) Then

                ' Grava o Erro no banco
                GravarErroNoBanco(Erro.InnerException.ToString, Usuario)

            Else

                ' Grava o Erro no banco
                GravarErroNoBanco(Erro.ToString, Usuario)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Grava erro no banco, caso o banco esteja offline, o erro será gravado no event viewer
    ''' </summary>
    ''' <param name="Erro"></param>
    ''' <param name="Usuario"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/05/2009 Criado
    ''' </history>
    Public Shared Sub GravarErroNoBanco(Erro As String, _
                                        Usuario As String)

        Try

            Dim comando As IDbCommand = AcessoDados.CriarComando(Constantes.CONEXAO_SALDOS)
            comando.CommandText = My.Resources.GravarErroNoBanco.ToString
            comando.CommandType = CommandType.Text

            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "oid_log_error", ProsegurDbType.Objeto_Id, Guid.NewGuid.ToString))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "des_error", ProsegurDbType.Descricao_Longa, Erro))
            comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, "cod_usuario", ProsegurDbType.Identificador_Alfanumerico, Usuario))

            AcessoDados.ExecutarNonQuery(Constantes.CONEXAO_SALDOS, comando)

        Catch ex As Exception

            ' caso o banco não esteja online, gravar no event viewer
            GravarErroEventViewer(Erro.ToString)

        End Try

    End Sub

    ''' <summary>
    ''' Grava erro no event viewer
    ''' </summary>
    ''' <param name="DescricaoErro"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 28/05/2009 Criado
    ''' </history>
    Private Shared Sub GravarErroEventViewer(DescricaoErro As String)

        Try

            ' criar objeto
            Dim objEventLog As New EventLog

            'Register the Application as an Event Source
            If Not EventLog.SourceExists(Constantes.NOME_LOG_EVENTOS) Then
                EventLog.CreateEventSource(Constantes.NOME_LOG_EVENTOS, Constantes.NOME_LOG_EVENTOS)
            End If

            'log the entry
            objEventLog.Source = Constantes.NOME_LOG_EVENTOS
            objEventLog.WriteEntry(DescricaoErro, EventLogEntryType.Error)

        Catch ex As Exception

            'Verifica se a pasta de logs dos erros existe.
            Dim caminhoLog As String = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) & "\\logs\\"
            If Not Directory.Exists(caminhoLog) Then

                'Se não existir a pasta é criada.
                Directory.CreateDirectory(caminhoLog)

            End If

            ' gravar log de erro na pasta logs do site
            Dim logFile As New StreamWriter(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath) & "/logs/" & DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss").ToString() & ".txt", True)
            logFile.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") & " - " & DescricaoErro)
            logFile.Close()

        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="coluna"></param>
    ''' <param name="direcao"></param>
    ''' <param name="valor"></param>
    ''' <param name="tipo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CriarParametroOracle(coluna As String, _
                                                direcao As ParameterDirection, _
                                                ByRef valor As Object, _
                                                tipo As OracleType, _
                                                Optional Tamanho As Integer = -1) As OracleParameter

        ' inicializar variáveis

        Dim p As OracleParameter = New OracleClient.OracleParameter

        ' montar parameter
        p.ParameterName = coluna
        p.Direction = direcao
        p.Value = valor
        p.OracleType = tipo

        If Tamanho <> -1 Then
            p.Size = Tamanho
        End If

        ' retorna o parameter preenchido
        Return p

    End Function

    ''' <summary>
    ''' Monta a clausula in através de uma coleção de itens e o nome do campo
    ''' </summary>
    ''' <param name="Itens"></param>
    ''' <param name="Campo"></param>
    ''' <param name="Comando"></param>
    ''' <param name="TipoClausula">Utilizado para informar se a clausula é WHERE, AND ou OR.</param>
    ''' <param name="Alias">Insere um alias para o campo de consulta.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 03/02/2009 Criado
    ''' </history>
    Public Shared Function MontarClausulaIn(Itens As Object, _
                                            Campo As String, _
                                            ByRef Comando As IDbCommand, _
                                            Optional TipoClausula As String = "", _
                                            Optional [Alias] As String = "") As String

        ' clausula in
        Dim clausulaIn As New StringBuilder

        ' se alias não for vazio
        If Not [Alias].Equals(String.Empty) Then
            [Alias] &= "."
        End If

        ' se foram informados itens para pesquisa
        If Itens IsNot Nothing AndAlso Itens.Count > 0 Then

            ' criar flag
            Dim addIn As Boolean = True

            ' percorrer todos os itens
            For i As Integer = 0 To Itens.Count - 1

                If Itens(i) IsNot Nothing AndAlso Not Itens(i).Equals(String.Empty) AndAlso addIn Then

                    ' concatenar filtro na query
                    clausulaIn.Append(" " & TipoClausula & " " & [Alias] & Campo & " IN (")

                    ' alterar flag 
                    addIn = False

                ElseIf Itens(i) Is Nothing OrElse Itens(i).Equals(String.Empty) Then
                    Continue For
                End If

                ' concatenar parametro na query
                clausulaIn.Append("[]" & Campo & i)

                ' se ainda existirem codigos
                If i <> Itens.Count - 1 Then
                    clausulaIn.Append(",")
                End If

                ' setar parameter
                If TypeOf Itens(i) Is String Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, Campo & i, ProsegurDbType.Descricao_Curta, Itens(i)))
                ElseIf TypeOf Itens(i) Is Int16 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, Campo & i, ProsegurDbType.Inteiro_Curto, Itens(i)))
                ElseIf TypeOf Itens(i) Is Int32 Then
                    Comando.Parameters.Add(AcessoDados.CriarParametroProsegurDbType(Constantes.CONEXAO_SALDOS, Campo & i, ProsegurDbType.Inteiro_Longo, Itens(i)))
                End If

            Next

            ' se adicionou IN deve fechar parenteses
            If Not addIn Then

                ' fechar comando do filtro
                clausulaIn.Append(")")

            End If

        End If

        Return clausulaIn.ToString

    End Function

    ''' <summary>
    ''' Retorna o nome do servidor de banco de dados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaNomeServidorBD() As String

        ' Retorna o nome do servidor de banco de dados
        Return AcessoDados.RecuperarConexao(Constantes.CONEXAO_SALDOS).Servidor

    End Function

    ''' <summary>
    ''' Serializa o datatable em byte
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SerializarDT(dt As DataTable) As Byte()

        'Caso não haja registro no datatable, não faz nada
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return Nothing

        Dim bformatter As New BinaryFormatter
        Dim stream As New MemoryStream
        bformatter.Serialize(stream, dt)
        Dim b() As Byte = stream.ToArray()
        stream.Close()

        Return b

    End Function

    ''' <summary>
    ''' Deserializa o datatable
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 04/10/2010 - Criado
    ''' </history>
    Public Shared Function DeserializarDT(obj As Array) As DataTable

        Dim dt As New DataTable
        Dim bformatter As New BinaryFormatter
        Dim stream As New MemoryStream

        stream = New MemoryStream(obj)
        dt = bformatter.Deserialize(stream)
        stream.Close()

        Return dt

    End Function

    ''' <summary>
    ''' Atribui o valor ao objeto passado, faz a conversão do tipo do banco para o tipo da propriedade.
    ''' </summary>
    ''' <param name="Valor"></param>
    ''' <param name="TipoCampo"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 18/11/2009 Criado
    ''' </history>
    Public Shared Function AtribuirValorObj(Valor As Object, _
                                               TipoCampo As System.Type) As Object

        Dim Campo As New Object

        If Valor IsNot DBNull.Value AndAlso Not String.IsNullOrEmpty(Valor) Then
            If TipoCampo Is Nothing Then
                Campo = Valor
            ElseIf TipoCampo Is GetType(String) Then
                Campo = Convert.ToString(Valor)
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = Convert.ToInt16(Valor)
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = Convert.ToInt32(Valor)
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = Convert.ToInt64(Valor)
            ElseIf TipoCampo Is GetType(Decimal) Then
                Campo = Convert.ToDecimal(Valor)
            ElseIf TipoCampo Is GetType(Double) Then
                Campo = Convert.ToDouble(Valor)
            ElseIf TipoCampo Is GetType(Boolean) Then
                Campo = Convert.ToBoolean(Convert.ToInt16(Valor.ToString.Trim))
            ElseIf TipoCampo Is GetType(DateTime) Then
                Campo = Convert.ToDateTime(Valor)
            End If
        Else

            If TipoCampo Is GetType(Decimal) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int16) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int32) Then
                Campo = 0
            ElseIf TipoCampo Is GetType(Int64) Then
                Campo = 0
            Else
                Campo = Nothing
            End If

        End If

        Return Campo
    End Function

    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <param name="mensagem"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMensagemEmDisco(mensagem As String)

        LogMensagemEmDisco(mensagem, "AUTOMATAS_LOG.txt")

    End Sub

    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <param name="mensagem"></param>
    ''' <remarks></remarks>
    Public Shared Sub LogMensagemEmDisco(mensagem As String, nomeArquivo As String)

        ' Verifica se é para gerar o log
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("BOL_GENERAR_TRACE_LOG") IsNot Nothing AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("BOL_GENERAR_TRACE_LOG") = 1) OrElse (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("GrabarLogMensajesEnDiscoDuro") IsNot Nothing AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("GrabarLogMensajesEnDiscoDuro").ToString().ToLower() = "true") Then

            ' Gera o arquivo
            Dim arquivo As StreamWriter = Nothing

            Try

                ' grava o arquivo de log em disco (mesmo nível em que os assemblys se encontram
                arquivo = New StreamWriter(AppDomain.CurrentDomain.BaseDirectory() + "\" + nomeArquivo, True)

                ' trata a mensagem para que ele tenha uma identação correta no arquivo txt (caso haja quebra de linha)
                mensagem = mensagem.Replace(vbCrLf, vbCrLf + "                    ")

                ' adiciona a linha no arquivo
                arquivo.WriteLine(Now.ToString("dd-MM-yyyy hh:mm:ss") + " " + mensagem)

            Finally

                If (arquivo IsNot Nothing) Then
                    arquivo.Dispose()
                    arquivo.Close()
                End If

            End Try

        End If

    End Sub

    Private Shared Sub cachingDelegaciones()
        Dim objPeticion As New Seguridad.ContractoServicio.ObtenerDelegaciones.Peticion()
        Dim objRespuesta As Seguridad.ContractoServicio.ObtenerDelegaciones.Respuesta
        Dim objProxyLogin As New LoginGlobal.Seguridad()

        objRespuesta = objProxyLogin.ObtenerDelegaciones(objPeticion)

        Delegaciones = objRespuesta.Delegaciones
    End Sub

    Public Shared Function cachingGetDelegacion(codDeleacion As String) As Seguridad.ContractoServicio.ObtenerDelegaciones.Delegacion
        If (IsNothing(Delegaciones)) Then
            cachingDelegaciones()
        End If

        Return If(Delegaciones IsNot Nothing, Delegaciones.FirstOrDefault(Function(f) f.Codigo = codDeleacion), Nothing)

    End Function

    Public Shared Function GetDateTime(codDeleacion As String) As DateTime
        Dim deleg = cachingGetDelegacion(codDeleacion)
        Dim dt As DateTime
        dt = If(deleg IsNot Nothing, DateTime.UtcNow.AddMinutes(deleg.GMT), DateTime.Now)
        If deleg IsNot Nothing AndAlso deleg.VeranoAjuste > 0 AndAlso
            (dt.Ticks > deleg.VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < deleg.VeranoFechaHoraFin.Ticks) AndAlso
            (deleg.VeranoFechaHoraIni.Ticks <> deleg.VeranoFechaHoraFin.Ticks) Then
            dt = dt.AddMinutes(deleg.VeranoAjuste)
        End If
        Return dt

    End Function

    Public Shared Function GetGMTVeranoAjuste(codDeleacion As String) As Short
        Dim deleg = cachingGetDelegacion(codDeleacion)
        Dim dt As DateTime
        dt = If(deleg IsNot Nothing, DateTime.UtcNow.AddMinutes(deleg.GMT), DateTime.Now)
        If deleg IsNot Nothing AndAlso deleg.VeranoAjuste > 0 AndAlso
            (dt.Ticks > deleg.VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < deleg.VeranoFechaHoraFin.Ticks) AndAlso
            (deleg.VeranoFechaHoraIni.Ticks <> deleg.VeranoFechaHoraFin.Ticks) Then
            Return deleg.GMT + deleg.VeranoAjuste
        End If
        Return If(deleg IsNot Nothing, deleg.GMT, 0)

    End Function

    Private Shared Function RetornarPrefixoParametro() As String

        ' verificar o banco que está sendo usado
        Select Case AcessoDados.RecuperarProvider(Constantes.CONEXAO_SALDOS)

            Case Provider.MsOracle

                Return ":"

            Case Provider.SqlServer

                Return "@"

        End Select

        Return String.Empty

    End Function

    Public Shared Function PrepararQuery(sql As String) As String
        Return sql.Replace("[]", RetornarPrefixoParametro)

    End Function

End Class
