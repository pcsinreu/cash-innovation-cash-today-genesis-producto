Imports System.ComponentModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Globalization
Imports System.Reflection
Imports System.Web.UI.MobileControls
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis

Public Class Util

#Region "Construtores"

    ''' <summary>
    ''' Contrutor privado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub New()

    End Sub

#End Region

#Region "[Logar erro aplicacao]"

    ''' <summary>
    ''' Trata o retorno da chamada de um serviço
    ''' </summary>
    ''' <param name="CodErro"></param>
    ''' <param name="MsgError"></param>
    ''' <param name="MsgRetorno"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Shared Function TratarRetornoServico(CodErro As Integer, _
                                                MsgError As String, _
                                                resultadoOperacion As ContractoServ.Login.ResultadoOperacionLoginLocal, _
                                                Optional ByRef MsgRetorno As String = "", _
                                                Optional ByRef _traduzir As Boolean = True, _
                                                Optional LoginUsuario As String = "", _
                                                Optional DelegacionUsuario As String = "") As Boolean

        Select Case CodErro

            Case Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT
                Return True

            Case Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT

                If resultadoOperacion = ContractoServ.Login.ResultadoOperacionLoginLocal.NoEsValido Then
                    MsgRetorno = Traduzir("002_msg_usuario_invalido")
                Else
                    MsgRetorno = Traduzir("err_codigo_erro_ambiente_default")
                    ' logar erro no banco
                    Util.LogarErroAplicacao(CodErro, MsgError, String.Empty, LoginUsuario, DelegacionUsuario)
                End If

            Case (Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT + 1) To Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT
                'Caso a mensagem de erro do negócio não tenha sido informada, então exibe a padrão de negócio.
                If MsgError <> String.Empty Then
                    MsgRetorno = MsgError
                Else
                    MsgRetorno = Traduzir("err_codigo_erro_negocio_default")
                End If

            Case Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_SESION_INVALIDA
                MsgRetorno = Traduzir("err_codigo_erro_negocio_sesion_invalida")

            Case Else
                MsgRetorno = Traduzir("err_padrao_aplicacao")

        End Select

        _traduzir = False
        Return False

    End Function

    ''' <summary>
    ''' Loga erro passando uma exception
    ''' </summary>
    ''' <param name="Ex"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Shared Sub LogarErroAplicacao(Ex As Exception)

        LogarErroAplicacao(String.Empty, Ex.ToString, String.Empty, String.Empty, String.Empty)

    End Sub

    ''' <summary>
    ''' Metodo genérico para logar os erros retornados pelos webservices.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Public Shared Sub LogarErroAplicacao(CodigoErro As Integer, _
                                         MensErro As String, _
                                         OutrasInformacoes As String, _
                                         LoginUsuario As String, _
                                         Delegacion As String)

        Try

            Dim Peticion As New ContractoServ.Log.Peticion
            Dim RespuestaLog As ContractoServ.Log.Respuesta
            Peticion.FYHErro = DateTime.Now
            ' pegar informações do usuário e delegação
            Peticion.LoginUsuario = LoginUsuario
            Peticion.Delegacion = Delegacion
            ' --------------------------------------------------------
            Peticion.DescricionErro = String.Format("{0} - {1}", CodigoErro, MensErro)
            Peticion.Otros = OutrasInformacoes
            Peticion.CodigoPuesto = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodPuestoSesion")

            ' chama o serviço responsável pela gravação do log e guarda a exceção no event viewer caso ocorra algum problema.
            Dim objProxyLog As New ListadosConteo.ProxyLog
            RespuestaLog = objProxyLog.InserirLog(Peticion)

            If RespuestaLog.CodigoError <> 0 Then
                GravarLogErroEventViewer(CodigoErro, MensErro)
            End If

        Catch ex As Exception

            GravarLogErroEventViewer(0, ex.ToString())

        End Try

    End Sub

    ''' <summary>
    ''' Grava erro no event viewer
    ''' </summary>
    ''' <param name="CodigoErro"></param>
    ''' <param name="DescricaoErro"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/07/2009 Criado
    ''' </history>
    Private Shared Sub GravarLogErroEventViewer(CodigoErro As String, _
                                                DescricaoErro As String)

        Try

            ' criar objeto
            Dim objEventLog As New EventLog

            'Register the Application as an Event Source
            If Not EventLog.SourceExists(Constantes.NOME_LOG_EVENTOS) Then
                EventLog.CreateEventSource(Constantes.NOME_LOG_EVENTOS, Constantes.NOME_LOG_EVENTOS)
            End If

            ' mensagem de erro
            Dim msgErro As String = "Cod. Erro: " & CodigoErro & Environment.NewLine & " - " & DescricaoErro

            'log the entry
            objEventLog.Source = Constantes.NOME_LOG_EVENTOS
            objEventLog.WriteEntry(msgErro, EventLogEntryType.Error)

        Catch ex As Exception
        End Try

    End Sub

    ''' <summary>
    ''' Faz o tratamento do retorno das mensagens de erro.
    ''' </summary>
    ''' <param name="msgErro"></param>
    ''' <param name="msgErroExibicao"></param>
    ''' <param name="nomeServidor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 21/09/2010 - Criado
    ''' </history>
    Public Shared Function RetornarMensagemErro(msgErro As String, msgErroExibicao As String, _
                                                Optional nomeServidor As String = "", _
                                                Optional _traduzir As Boolean = False) As String

        If Not String.IsNullOrEmpty(msgErro) Then

            Dim palavras() As String = msgErro.Split(" ")

            If (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_ACCESO_LDAP.ToLower).Count > 0 Then

                'Retorna erro de acesso ao servidor de ad
                Return If(_traduzir, Traduzir("err_msg_error_03"), "err_msg_error_03")

            ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_BANCO.ToLower OrElse _
                                             p.Trim.ToLower = Constantes.CONST_ERRO_BANCO2.ToLower OrElse _
                                             p.Trim.ToLower = Constantes.CONST_ERRO_BANCO3.ToLower).Count > 0 Then

                'Retorna erro de banco de dados.
                Return If(_traduzir, String.Format(Traduzir("err_msg_error_05"), nomeServidor), String.Format("err_msg_error_05", nomeServidor))

            ElseIf (From p In palavras Where p.Trim.ToLower = Constantes.CONST_ERRO_404B.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_404A.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_DNS.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_URL.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407B.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_407A.ToLower _
                    OrElse p.Trim.ToLower = Constantes.CONST_ERRO_COMUNICACION_LDAP.ToLower).Count > 0 Then

                'Retorna erro de url invalida
                Return If(_traduzir, Traduzir("err_msg_error_02"), "err_msg_error_02")

            End If

        End If

        Return msgErroExibicao
    End Function

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carrega os formatos de arquivos
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function TraduzirFormatoArchivo(formato) As String
        Dim retorno As String = String.Empty
        If formato = Enumeradores.eFormatoArchivo.CSV Then
            retorno = Traduzir("024_rbnCSV")
        ElseIf formato = Enumeradores.eFormatoArchivo.PDF Then
            retorno = Traduzir("024_rbnPDF")
        ElseIf formato = Enumeradores.eFormatoArchivo.EXCEL Then
            retorno = Traduzir("024_rbnEXCEL")
        ElseIf formato = Enumeradores.eFormatoArchivo.[EXCEL2010] Then
            retorno = Traduzir("024_rbnEXCEL2010")
        ElseIf formato = Enumeradores.eFormatoArchivo.MHTML Then
            retorno = Traduzir("024_rbnHTML")
        End If

        Return retorno
    End Function


    Public Shared Function IsNull(Valor As Object, ValorPadrao As Object) As Object

        If Valor Is Nothing OrElse IsDBNull(Valor) Then
            Return ValorPadrao
        Else
            Return Valor
        End If

    End Function

    ''' <summary>
    ''' Define o número máximo de caracteres a serem exibidos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getMaximoCaracteresLinha() As Integer
        Try
            Dim intMaximoCaraterLinha As Integer = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("MaximoCaracteresLinha")

            If intMaximoCaraterLinha > 0 Then
                Return intMaximoCaraterLinha
            Else
                Return Constantes.MaximoCaracteresLinha
            End If

        Catch ex As Exception
            Return Constantes.MaximoCaracteresLinha
        End Try
    End Function

    ''' <summary>
    ''' Define o número máximo de caracteres a serem exibidos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function getMaximoRegistroGrid() As Integer
        Try
            Dim intMaximoRegistroGrid As Integer = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("MaximoRegistroGrid")

            If intMaximoRegistroGrid > 0 Then
                Return intMaximoRegistroGrid
            Else
                Return Constantes.MaximoRegistrosGrid
            End If

        Catch ex As Exception
            Return Constantes.MaximoRegistrosGrid
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="CurrentControl"></param>
    ''' <remarks></remarks>
    Public Shared Sub HookOnFocus(CurrentControl As Control)
        If CurrentControl.GetType() Is GetType(WebControls.TextBox) OrElse CurrentControl.GetType() Is GetType(DropDownList) OrElse CurrentControl.GetType() Is GetType(ListBox) OrElse CurrentControl.GetType() Is GetType(Button) OrElse CurrentControl.GetType() Is GetType(RadioButton) Then
            DirectCast(CurrentControl, WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id}catch(e) {}")
        ElseIf CurrentControl.GetType() Is GetType(CheckBox) Then
            DirectCast(CurrentControl, CheckBox).InputAttributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id}catch(e) {}")
        End If
        If (CurrentControl.HasControls()) Then
            For Each CurrentChildControl As Control In CurrentControl.Controls
                HookOnFocus(CurrentChildControl)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Traduz os campos de título do relatório, passa o título como parâmetro
    ''' </summary>
    ''' <param name="pReport">Template do relatório</param>
    ''' <param name="pTitulo">Título do relatório</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Magnum.Olivera] 20/07/2009 Criado
    ''' [Magnum.Olivera] 15/12/2009 Criado
    ''' </history>
    Public Shared Sub TraduzirRelatorio(pReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, Optional pTitulo As String = "")

        ' Para cada seção existente no Relatorio
        For Each oSection In pReport.ReportDefinition.Sections

            ' Para cada objeto existente na seção
            For Each oRepObj In oSection.ReportObjects

                ' Se o tipo do objeto é 'SubreportObject'
                If TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.SubreportObject Then

                    TraduzirRelatorio(pReport.Subreports(oRepObj.SubReportName))

                End If

                ' Verifica o tipo do objeto
                If TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.FieldHeadingObject _
                OrElse TypeOf oRepObj Is CrystalDecisions.CrystalReports.Engine.TextObject Then

                    ' Se o objeto for o título do relatório
                    If oRepObj.Name = Constantes.LBL_TITULO_RELATORIO Then

                        ' Atribui o título do relatório
                        oRepObj.Text = pTitulo

                    Else
                        ' Atribui o valor do texto
                        oRepObj.Text = Traduzir(oRepObj.Name)

                    End If

                End If

            Next
        Next

    End Sub

    ''' <summary>
    ''' Descarrega o CSV para download
    ''' </summary>
    ''' <param name="sCSV">Dados no formato CSV</param>
    ''' <param name="pNombreCSV">Nome do arquivo</param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Magnum.Olivera] 23/07/2009 Criado
    ''' </history>
    Public Shared Sub DescarregarCSV(sCSV As String, pNombreCSV As String)

        'Obtiene la respuesta actual
        Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        'Borra la respuesta
        response.Clear()
        response.ClearContent()
        response.ClearHeaders()

        'Tipo de contenido para forzar la descarga
        response.ContentType = "application/octet-stream"
        response.AddHeader("Content-Disposition", "attachment; filename=" & pNombreCSV)

        'Convierte el string a array de bytes
        Dim buffer(sCSV.Length) As Byte
        Dim mContador As Long = 0
        While mContador < sCSV.Length
            buffer(mContador) = Asc(Mid(sCSV, mContador + 1, 1))
            mContador = mContador + 1
        End While

        'Envia los bytes
        response.BinaryWrite(buffer)
        response.End()

    End Sub

    ''' <summary>
    ''' Recupera o nome do arquivo de acordo com o padrão definido
    ''' </summary>
    ''' <param name="Delegacion">Nome da delegação</param>
    ''' <param name="Cliente">Nome do cliente</param>
    ''' <param name="TipoInformacaoRelatorio">Tipo de informação do relatório</param>
    ''' <param name="DataTransporte">Data do transporte</param>
    ''' <returns></returns>
    ''' <remarks></remarks>]
    ''' <history>
    ''' [Magnum.Olivera] 23/07/2009 Criado
    ''' </history>
    Public Shared Function RecuperarNomeArquivo(Delegacion As String, Cliente As String, TipoInformacaoRelatorio As String, DataTransporte As String) As String

        ' Variável que recebe o nome do arquivo
        Dim nomeArquivo As String = String.Empty

        ' Os dois primeiros dígitos da delegação
        nomeArquivo &= Left(Delegacion.ToUpper, 2)
        ' Os três primeiro dígitos do cliente
        nomeArquivo &= "_" & IIf(Cliente.Contains(" - "), Cliente.Split(" - ")(0).ToUpper(), Cliente.ToUpper())
        ' Tipo de informação do relatório
        nomeArquivo &= TipoInformacaoRelatorio

        ' Verifica se a data foi informada
        If (IsDate(DataTransporte)) Then
            ' Recupera a data no formato 'mmdd'
            nomeArquivo &= Convert.ToDateTime(DataTransporte).ToString("MMdd")
        End If

        ' Retorna o nome do arquivo
        Return nomeArquivo

    End Function

    ''' <summary>
    ''' Carrega a combobox de Delegações para o usuário informar em qual delegação deseja logar no sistema
    ''' </summary>
    ''' <param name="ddlDelegacion">Combo a ser caregado pelas delegações</param>
    ''' <param name="Delegaciones">Coleção de delegações que o usuário tem permiso</param>
    ''' <history>
    ''' [gustavo.fraga] 16/03/2011 Criado    
    ''' </history>    
    ''' <remarks></remarks>
    Public Shared Sub CarregarDelegacoes(ByRef ddlDelegacion As DropDownList, _
                                         Delegaciones As List(Of Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Delegacion))

        ' o primeiro item em branco
        ddlDelegacion.Items.Insert(0, New ListItem(String.Empty, String.Empty))

        ' varre a lista de delegações e adiciona na combo
        For Each objDelegacion As Prosegur.Global.GesEfectivo.Reportes.ContractoServ.Login.Delegacion In Delegaciones
            ddlDelegacion.Items.Add(New ListItem(objDelegacion.Descripcion, objDelegacion.Codigo, True))
        Next

    End Sub

    Public Shared Function ConfiguraSeparadorDecimal(ByRef valor As String) As String

        Dim IndicadorDecimales As String = "," ' valor padrão na ausência do paramêtro no web.config

        ' se a chave SeparadorColumnas existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("IndicadorDecimales") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("IndicadorDecimales").Trim().Length() > 0) Then
            IndicadorDecimales = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("IndicadorDecimales").Trim().Substring(0, 1)
        End If

        valor = valor.Replace(".", "#[|]#")
        valor = valor.Replace(",", "#[|]#")

        Return valor.Replace("#[|]#", IndicadorDecimales)

    End Function

    Public Shared Function ConfiguraCalificadorTexto(ByRef valor As String) As String

        Dim calificadorTexto As String = "" ' valor padrão na ausência do paramêtro no web.config

        ' se a chave CalificadorTexto existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("CalificadorTexto") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("CalificadorTexto").Trim().Length() > 0) Then
            calificadorTexto = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("CalificadorTexto").Trim().Substring(0, 1)
        End If

        If valor IsNot Nothing Then
            Return calificadorTexto & valor.Trim() & calificadorTexto
        Else
            Return calificadorTexto & "" & calificadorTexto
        End If

    End Function

    Public Shared Function VerificarParametroNoWebConfig(nomeParametro As String) As Boolean

        ' se a chave existe no Web.Config
        If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains(nomeParametro)) Then
            Return Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get(nomeParametro).Trim() = "1"
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Busca o CultureInfo de acordo com a configuração do browser do usuário
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ResolveCultureFromBrowser() As CultureInfo

        Dim languages As String() = HttpContext.Current.Request.UserLanguages

        'Se não existir um idioma configurado no browser assume a cultura padrão
        If languages Is Nothing OrElse languages.Length = 0 Then

            Return CulturaPadrao

        Else

            Dim language As String = Trim(languages(0).ToLowerInvariant())
            Return CultureInfo.CreateSpecificCulture(language)

        End If

    End Function

    ''' <summary>
    ''' Verifica se existe determinada característica na coleção
    ''' </summary>
    ''' <param name="Caracteristicas">Coleção de Caracteristica</param>
    ''' <param name="CodigoCaracteristicaConteo">Constante Caracteristica a ser validada na coleção</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 13/12/2011 - Criado
    ''' </history>
    Public Shared Function VerificarCaracteristica(Caracteristicas As ContractoServ.InformeResultadoContaje.ListarResultadoContaje.CaracteristicaColeccion, _
                                                   CodigoCaracteristicaConteo As String) As Boolean

        'Se não houver caracteristica, retorna falso
        If Caracteristicas IsNot Nothing Then

            'Dentre as características da coleção, verifica se existe
            'o CodigoCaracteristicaConteo recebido como parâmetro
            Return (From Carac In Caracteristicas _
                    Where Carac.CodigoCaracteristicaConteo = CodigoCaracteristicaConteo).Count > 0

        End If

        Return False

    End Function

    ''' <summary>
    ''' Mostra Mensagem de Erro na tela
    ''' </summary>
    ''' <param name="erro"></param>
    ''' <remarks></remarks>
    ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
    Public Shared Function CriarChamadaMensagemErro(erro As String, script As String) As String
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")

        Return String.Format("ExibirMensagem('{0}','{1}', '{2}' , '{3}');", _
                                                                erro, _
                                                                 Traduzir("aplicacao"), script, Traduzir("btnFechar"))
    End Function

    ''' <summary>
    ''' Recupera os relatórios no diretório de reportes no servidor de relatórios.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RecuperarParametrosRelatorios() As Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro))
        Dim ParametrosReporte As Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro)) = Nothing
        Try

            Dim objReport As New Prosegur.Genesis.Report.Gerar()
            objReport.Autenticar(False)

            ParametrosReporte = New Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro))
            'recupera os relatórios no diretório no servidor de relatório.
            Dim Relatorios = objReport.ListaCatalogItem(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))

            'Verifica se todos os relatórios no diretório estão na variável de aplicação
            'Atualiza a variável de aplicação com os relatórios atuais

            For Each relatorio In Relatorios.Where(Function(r) r.TypeName = "Report")
                'Lista os parametros do relatório
                Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)
                Try
                    parametros = objReport.ParametrosRelatorio(relatorio.Path)
                    If parametros Is Nothing Then
                        parametros = New List(Of Prosegur.Genesis.Report.Parametro)
                    End If

                    ParametrosReporte.Add(relatorio.Name, parametros)
                Catch ex As Exception
                    parametros = New List(Of Prosegur.Genesis.Report.Parametro)
                    ParametrosReporte.Add(relatorio.Name, parametros)
                End Try
            Next
        Catch ex As Exception
            Throw
        End Try

        Return ParametrosReporte
    End Function


    Public Shared Function ConverterListParaDataTable(Of T)(items As List(Of T)) As DataTable

        Try
            Dim table As DataTable = CreateTable(Of T)()
            Dim componentType As Type = GetType(T)
            Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(componentType)
            Dim local As T
            For Each local In items
                Dim row As DataRow = table.NewRow
                Dim descriptor As PropertyDescriptor
                For Each descriptor In properties
                    row.Item(descriptor.Name) = descriptor.GetValue(local)
                Next
                table.Rows.Add(row)
            Next
            Return table
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Cria uma tabela a partir de um tipo
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateTable(Of T)() As DataTable
        Dim componentType As Type = GetType(T)
        Dim table As New DataTable(componentType.Name)
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(componentType)
        Dim descriptor As PropertyDescriptor
        For Each descriptor In properties
            table.Columns.Add(descriptor.Name, GetTypeColumn(descriptor.PropertyType))
        Next
        Return table
    End Function

    ''' <summary>
    ''' Obtém o tipo para uma coluna do datatable. Trata tipos NullAble.
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/03/2009 Criado
    ''' </history>
    Public Shared Function GetTypeColumn(Type As System.Type) As System.Type

        If Type Is GetType(Nullable(Of Boolean)) Then
            Return GetType(Boolean)
        ElseIf Type Is GetType(Nullable(Of Int16)) Then
            Return GetType(Int16)
        ElseIf Type Is GetType(Nullable(Of Int32)) Then
            Return GetType(Int32)
        ElseIf Type Is GetType(Nullable(Of Int64)) Then
            Return GetType(Int64)
        ElseIf Type Is GetType(Nullable(Of Decimal)) Then
            Return GetType(Decimal)
        ElseIf Type Is GetType(Nullable(Of Double)) Then
            Return GetType(Double)
        Else
            Return Type
        End If

    End Function

#End Region

End Class