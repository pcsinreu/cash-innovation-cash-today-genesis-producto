Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Report.Constantes
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion

Public Class Reportes
    Inherits Base

#Region "METODOS BASE"

    Dim lblFechaTransporte As Object

    Protected Overrides Sub AdicionarControlesValidacao()
        MyBase.AdicionarControlesValidacao()
    End Sub

    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        ConfigurarControles()
    End Sub

    Protected Overrides Sub ConfigurarEstadoPagina()
        MyBase.ConfigurarEstadoPagina()
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()
        MyBase.ConfigurarTabIndex()
    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.REPORTES
        ' desativar validação de ação
        MyBase.ValidarAcao = True

    End Sub

    Protected Overrides Sub Inicializar()
        MyBase.Inicializar()

        If Not IsPostBack Then
            'Recupera os relatórios e os parametros
            If MyBase.ParametrosReporte Is Nothing Then
                MyBase.ParametrosReporte = Util.RecuperarParametrosRelatorios()
            End If
        End If

        ASPxGridView.RegisterBaseScript(Page)
        Master.MostrarRodape = True
        Master.MenuRodapeVisivel = True
        Master.HabilitarHistorico = True
        Master.MostrarCabecalho = True
        Master.HabilitarMenu = True
        Master.MenuGrande = True
        Master.Titulo = Traduzir("023_lblSubTituloreportes")

        If MyBase.objGerarReport Is Nothing Then MyBase.objGerarReport = New Prosegur.Genesis.Report.Gerar

        If Not Page.IsPostBack Then
            PreencherGridViewReportes()
        End If

    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            Dim evento As String = Request.Params("__EVENTTARGET")
            If evento IsNot Nothing Then

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try


        MyBase.PreRenderizar()
    End Sub

    Protected Overrides Sub TraduzirControles()

        MyBase.TraduzirControles()
        Me.Page.Title = Traduzir("023_lblSubTituloreportes")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("023_lblSubTituloreportes")
        'Me.lblDelegacion.Text = Traduzir("023_lblDelegacion")

        btnAdicionar.Text = Traduzir("023_btnAdicionar")
        btnEditar.Text = Traduzir("023_btnEditar")
        btnExcluir.Text = Traduzir("023_btnExcluir")
        btnGerarRelatorio.Text = Traduzir("023_btnGerarRelatorio")

        valBtnExcluir.Value = Traduzir("023_btnExcluir")

        btnConfiguracionGeneral.Text = Traduzir("023_btnConfiguracionGeneral")

        gvReportes.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvReportes.SettingsText.EmptyDataRow = Traduzir("info_msg_grd_vazio")
        gvReportes.Columns(1).Caption = Traduzir("023_col_descricao")
    End Sub


#End Region

#Region "METODOS"
    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 29/04/2013 Criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        Dim _ValidaAcesso = New ValidacaoAcesso(InformacionUsuario)

        If Not _ValidaAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                           Enumeradores.eAcoesTela._MODIFICAR) Then

            valBtnExcluir.Visible = False

        End If

        If valBtnExcluir.Visible Then

            Dim s As String = "SelecionarConfirmarExclusao('" & gvReportes.ClientID & "','" & Traduzir(Constantes.InfoMsgSeleccionarRegistro) & "','','" & Traduzir(Constantes.InfoMsgBajaRegistro) & "','" & btnExcluir.ClientID & "');"
            valBtnExcluir.Attributes.Add("onclick", s)

        End If


    End Sub

    ''' <summary>
    ''' Preenche o gridview Reportes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 30/04/2013 Criado
    ''' </history>
    Private Sub PreencherGridViewReportes(Optional buscar As Boolean = False)

        Dim configuracoes = getConfiguracionesReportes.ConfiguracionesReportes

        If Not buscar Then
            ConfiguracionesRecuperadas = configuracoes
        End If

        configuracoes = VerificaConfigsDelegacionesPermissao(configuracoes)

        Dim ConfiguracionesMarcadas As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion

        'Recupera as configurações selecionadas para add no inicio da lista
        'Quando for uma busca deve manter os selecionados e add o resultado da busca no final
        If buscar AndAlso (reportesSelecionados IsNot Nothing AndAlso reportesSelecionados.Count > 0) AndAlso _
            ConfiguracionesRecuperadas IsNot Nothing AndAlso ConfiguracionesRecuperadas.Count > 0 Then

            Dim Valores As List(Of String) = reportesSelecionados

            If Valores IsNot Nothing AndAlso Valores.Count > 0 Then

                Dim config As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporte = Nothing

                For Each ValorSel In Valores
                    Dim ValorSelLocal = ValorSel

                    config = (From con In ConfiguracionesRecuperadas Where con.IdentificadorConfiguracion = ValorSelLocal).FirstOrDefault

                    If config IsNot Nothing Then

                        If ConfiguracionesMarcadas.FindAll(Function(c) c.IdentificadorConfiguracion = config.IdentificadorConfiguracion).Count = 0 Then
                            'Quando for busca sem nenhuma descrição, mantém os marcados no topo e ordena o resto
                            If buscar AndAlso reportesSelecionados.Count > 0 Then
                                config.Marcada = "0"
                            End If
                            'Adiciona as marcadas primeiramente, para ficar no inicio
                            ConfiguracionesMarcadas.Add(config)
                            'remove a mesma para não ficar duplicada
                            configuracoes.RemoveAll(Function(c) c.IdentificadorConfiguracion = config.IdentificadorConfiguracion)
                        End If

                    End If

                Next

            End If

        End If

        'Adiciona as que não estão marcadas no final
        If configuracoes IsNot Nothing AndAlso configuracoes.Count > 0 Then
            ConfiguracionesMarcadas.AddRange(configuracoes.OrderBy(Function(o) o.DesConfiguracion))
        End If

        If ConfiguracionesMarcadas IsNot Nothing AndAlso ConfiguracionesMarcadas.Count > 0 Then

            pnlSemRegistro.Visible = False
            Dim objDt As DataTable = Util.ConverterListParaDataTable(ConfiguracionesMarcadas)

            gvReportes.DataSource = objDt
            gvReportes.DataBind()

        Else

            gvReportes.DataSource = Nothing
            gvReportes.DataBind()

            pnlSemRegistro.Visible = True
            lblSemRegistro.Text = Traduzir("info_msg_grd_vazio")
        End If

    End Sub


    ''' <summary>
    ''' Retorna a lista de configurações
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 03/05/2013 Criado
    ''' [victor.ramos] 17/07/2013 Modificado
    ''' </history>
    Private Function getConfiguracionesReportes() As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta
        Dim respuesta As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxyIacIntegracion
        Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.Peticion
        If txtBuscar IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtBuscar.Text) Then
            peticion.DesConfiguracion = txtBuscar.Text.ToUpper
        Else
            peticion = Nothing
        End If
        respuesta = objProxy.GetConfiguracionesReportes(peticion)

        Return respuesta
    End Function
    ''' <summary>
    ''' Retorna a lista de configurações detalhada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 17/07/2013 Criado
    ''' </history>
    Private Function getConfiguracionesReportesDetail() As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta
        Dim respuesta As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxyIacIntegracion
        Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion

        peticion.IdentificadoresConfiguracion = reportesSelecionados()
        respuesta = objProxy.GetConfiguracionesReportesDetail(peticion)

        Return respuesta
    End Function

    ''' <summary>
    ''' Valida edição da configuração selecionada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 03/05/2013 Criado
    ''' </history>
    Private Function ValidaEditar() As Boolean
        Dim erro As List(Of String) = New List(Of String)
        Dim resultado = reportesSelecionados()

        If resultado Is Nothing OrElse (resultado IsNot Nothing AndAlso resultado.Count = 0) Then
            erro.Add(Traduzir(Constantes.InfoMsgSeleccionarRegistro))
        ElseIf resultado IsNot Nothing AndAlso resultado.Count > 1 Then
            erro.Add(Traduzir(Constantes.InfoMsgSeleccionarRegistroUnico))
        End If

        If erro IsNot Nothing AndAlso erro.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erro.ToArray()))
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Valida exclusão das configurações
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 03/05/2013 Criado
    ''' </history>
    Private Function ValidaExcluir() As Boolean
        Dim erro As List(Of String) = New List(Of String)
        Dim resultado = reportesSelecionados()

        If resultado Is Nothing OrElse (resultado IsNot Nothing AndAlso resultado.Count = 0) Then
            erro.Add(Traduzir(Constantes.InfoMsgSeleccionarRegistro))
        End If

        If erro IsNot Nothing AndAlso erro.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erro.ToArray()))
            Return False
        End If
        Return True
    End Function



    ''' <summary>
    ''' Valida configuracões selecionadas no grid para geração dos relatorios
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 03/05/2013 Criado
    ''' </history>
    Private Function ValidaDatos() As Boolean
        Dim erro As List(Of String) = New List(Of String)

        Dim selecionados As List(Of String) = reportesSelecionados()
        If selecionados Is Nothing OrElse (selecionados IsNot Nothing AndAlso selecionados.Count = 0) Then
            erro.Add(Traduzir(Constantes.InfoMsgSeleccionarRegistro))
        End If

        If ucDataConteo.Visible Then
            Try
                ucDataConteo.Validar()
            Catch ex As Excepcion.NegocioExcepcion
                erro.Add(ex.Descricao.ToString)
            Catch ex As Exception
                erro.Add(ex.Message)
            End Try
        End If

        If ucDataTransporte.Visible Then
            Try
                ucDataTransporte.Validar()
            Catch ex As Excepcion.NegocioExcepcion
                erro.Add(ex.Descricao.ToString)
            Catch ex As Exception
                erro.Add(ex.Message)
            End Try
        End If

        If erro IsNot Nothing AndAlso erro.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erro.ToArray()))
            Return False
        End If

        Return True
    End Function


    ''' <summary>
    ''' Valida configuracões selecionadas no grid para geração dos relatorios
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 03/05/2013 Criado
    ''' [victor.ramos] 22/07/2013 Modificado
    ''' </history>
    Private Function ValidaDatosConfiguraciones(configuracion As IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte, ByRef erros As List(Of String)) As Boolean

        Dim parametros As List(Of Prosegur.Genesis.Report.Parametro) = Nothing
        Dim semErro As Boolean = True

        If ParametrosReporte Is Nothing Then
            ParametrosReporte = New Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro))
        End If

        Dim path As String = String.Empty
        If Not String.IsNullOrEmpty(configuracion.DesRuta) Then
            'Verifica se as chave existe no web.config
            Dim ReporteUrl As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_URL")
            If String.IsNullOrEmpty(ReporteUrl) OrElse Not (ReporteUrl.StartsWith("\\")) Then
                erros.Add(String.Format(Traduzir("WEBCONFIG_CHAVE_INVALIDA"), "Reportes_REPO_URL", ReporteUrl))
                semErro = False
            End If

            Dim ReporteHome As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_HOME")
            If String.IsNullOrEmpty(ReporteHome) OrElse Not (ReporteHome.StartsWith("\")) Then
                erros.Add(String.Format(Traduzir("WEBCONFIG_CHAVE_INVALIDA"), "Reportes_REPO_HOME", ReporteHome))
                semErro = False
            End If

            path = RetornarDiretorio(configuracion.DesRuta)
            If String.IsNullOrEmpty(path) Then
                erros.Add(String.Format(Traduzir("023_msgDiretorioInvalido"), configuracion.DesConfiguracion))
                semErro = False
            ElseIf Not System.IO.Directory.Exists(path) Then
                erros.Add(String.Format(Traduzir("023_msgDiretorioInvalido"), configuracion.DesConfiguracion))
                semErro = False
            End If
        End If

        For Each reporte In configuracion.Reportes
            If Not ParametrosReporte.ContainsKey(reporte.DesReporte) Then
                Try

                    parametros = ObtenerParametros(reporte.DesReporte)

                Catch ex As Exception
                    erros.Add(String.Format(Traduzir("023_msg_erro_reporteNaoEncontrado"), configuracion.DesConfiguracion, ex.Message))
                    semErro = False
                End Try

                If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                    ParametrosReporte.Add(reporte.DesReporte, parametros)
                End If
            Else
                parametros = ParametrosReporte(reporte.DesReporte)
            End If

            If parametros IsNot Nothing AndAlso parametros.Count > 0 Then

                For Each parametro In parametros
                    Select Case parametro.Name

                        Case CONST_P_FECHA_CONTEO_DESDE
                            'Verifica se o campo é obrigatório
                            If parametro.Required AndAlso String.IsNullOrEmpty(ucDataConteo.DataInicio) Then
                                erros.Add(String.Format(Traduzir("023_msg_erro_campoObrigatorioConfiguracion"), _
                                                       String.Format("{0} {1}", Traduzir("023_lblFechaConteo"), Traduzir("lbl_desde")), _
                                                        configuracion.DesConfiguracion))
                                semErro = False
                            End If

                        Case CONST_P_FECHA_CONTEO_HASTA
                            If parametro.Required AndAlso String.IsNullOrEmpty(ucDataConteo.DataFin) Then
                                erros.Add(String.Format(Traduzir("023_msg_erro_campoObrigatorioConfiguracion"), _
                                                       String.Format("{0} {1}", Traduzir("023_lblFechaConteo"), Traduzir("lbl_hasta")), _
                                                       configuracion.DesConfiguracion))
                                semErro = False
                            End If

                        Case CONST_P_FECHA_TRANSPORTE_DESDE

                            If parametro.Required AndAlso String.IsNullOrEmpty(ucDataTransporte.DataInicio) Then
                                erros.Add(String.Format(Traduzir("023_msg_erro_campoObrigatorioConfiguracion"), _
                                                       String.Format("{0} {1}", Traduzir("023_lblFechaTransporteDesde"), _
                                                                     Traduzir("lbl_desde")), _
                                                       configuracion.DesConfiguracion))
                                semErro = False
                            End If

                        Case CONST_P_FECHA_TRANSPORTE_HASTA
                            If parametro.Required AndAlso String.IsNullOrEmpty(ucDataTransporte.DataFin) Then
                                erros.Add(String.Format(Traduzir("023_msg_erro_campoObrigatorioConfiguracion"), _
                                                       String.Format("{0} {1}", Traduzir("023_lblFechaTransporteHasta"), _
                                                                     Traduzir("lbl_hasta")), _
                                                       configuracion.DesConfiguracion))
                                semErro = False
                            End If

                        Case CONST_P_CODIGO_DELEGACION

                            If ucDelegacion.RecuperarSelecionado(False) Is Nothing OrElse ucDelegacion.RecuperarSelecionado(False).Count = 0 Then

                                Dim delegacion = (From c In configuracion.ParametrosReporte
                                                    Where c.CodNombreParametro = CONST_P_CODIGO_DELEGACION).Count()
                                If ValidarParametro(parametro.Required, delegacion) Then
                                    'Valida obrigatoriedade, é no RecuperarSelecionado() que coloca o asterisco
                                    ucDelegacion.RecuperarSelecionado()
                                    erros.Add(String.Format(Traduzir("023_msg_erro_delegacaoObrigatorio"), configuracion.DesConfiguracion))
                                    semErro = False
                                End If

                            End If

                        Case CONST_P_CODIGO_DELEGACION_USUARIO


                            If ucDelegacionUsuario.RecuperarSelecionado(False) Is Nothing OrElse ucDelegacionUsuario.RecuperarSelecionado(False).Count = 0 Then

                                Dim delegacion = (From c In configuracion.ParametrosReporte
                                                    Where c.CodNombreParametro = CONST_P_CODIGO_DELEGACION_USUARIO).Count()
                                If ValidarParametro(parametro.Required, delegacion) Then
                                    'Valida obrigatoriedade, é no RecuperarSelecionado() que coloca o asterisco
                                    ucDelegacionUsuario.RecuperarSelecionado()
                                    erros.Add(String.Format(Traduzir("023_msg_erro_delegacaoObrigatorio"), configuracion.DesConfiguracion))
                                    semErro = False
                                End If

                            End If

                    End Select
                Next

            End If
        Next

        Return semErro

    End Function

    'Protected Sub HabilitaConfiguracoes()
    '    For Each row As GridViewRow In Me.gdvReportes.Rows
    '        Dim chk As CheckBox = CType(row.Cells(0).FindControl("chbSelecionar"), CheckBox)
    '        chk.Enabled = True
    '    Next
    'End Sub
    Protected Sub DesabilitaConfiguracoes()

        Try
            Dim ParametrosAtuais As New List(Of Prosegur.Genesis.Report.Parametro)
            Dim configuracoesSelecionadas = reportesSelecionados
            Dim parametroDelegacion As Prosegur.Genesis.Report.Parametro = Nothing
            Dim parametroDelegacionUsuario As Prosegur.Genesis.Report.Parametro = Nothing
            For Each config In configuracoesSelecionadas
                Dim configLocal = config
                Dim configSelecionada = ConfiguracionesRecuperadas.Where(Function(c) c.IdentificadorConfiguracion = configLocal).FirstOrDefault

                If parametroDelegacion Is Nothing Then
                    parametroDelegacion = GetParametro(configSelecionada.DesReporte, CONST_P_CODIGO_DELEGACION)
                End If

                If parametroDelegacionUsuario Is Nothing Then
                    parametroDelegacionUsuario = GetParametro(configSelecionada.DesReporte, CONST_P_CODIGO_DELEGACION_USUARIO)
                End If

                If parametroDelegacion IsNot Nothing AndAlso parametroDelegacionUsuario IsNot Nothing Then
                    Exit For
                End If
            Next

            'For Each row As GridViewRow In Me.gdvReportes.Rows
            '    Dim config As String = gdvReportes.DataKeys(row.RowIndex).Value.ToString()
            '    Dim chk As CheckBox = CType(row.Cells(0).FindControl("chbSelecionar"), CheckBox)
            '    chk.Enabled = True
            '    If Not chk.Checked Then
            '        Dim configSelecionada = ConfiguracionesRecuperadas.Where(Function(c) c.IdentificadorConfiguracion = config).FirstOrDefault

            '        Dim parametro = GetParametro(configSelecionada.DesReporte, CONST_P_CODIGO_DELEGACION)
            '        'Verifica se o parametro de delegacion para essa configuração é diferente nas configurações selecionadas
            '        If parametro IsNot Nothing AndAlso parametroDelegacion IsNot Nothing Then
            '            If parametro.MultiValue <> parametroDelegacion.MultiValue Then
            '                chk.Enabled = False
            '            End If
            '        End If

            '        If chk.Enabled Then
            '            parametro = GetParametro(configSelecionada.DesReporte, CONST_P_CODIGO_DELEGACION_USUARIO)
            '            'Verifica se o parametro de delegacion para essa configuração é diferente nas configurações selecionadas
            '            If parametro IsNot Nothing AndAlso parametroDelegacionUsuario IsNot Nothing Then
            '                If parametro.MultiValue <> parametroDelegacionUsuario.MultiValue Then
            '                    chk.Enabled = False
            '                End If
            '            End If
            '        End If
            '    End If
            'Next

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function GetParametro(nomesReportes As List(Of String), nomeParametro As String) As Prosegur.Genesis.Report.Parametro
        Dim erro As List(Of String) = New List(Of String)
        Dim parametro As Prosegur.Genesis.Report.Parametro = Nothing
        Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)
        parametros = Nothing

        For Each nomeReporte In nomesReportes
            Try
                parametros = ObtenerParametros(nomeReporte)
                If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                    parametro = parametros.Where(Function(p) p.Name.ToUpper = nomeParametro.ToUpper).FirstOrDefault()
                    If parametro IsNot Nothing Then
                        Exit For
                    End If
                End If
            Catch ex As Exception
                erro.Add(String.Format(Traduzir("023_msg_erro_reporteNaoEncontrado"), nomeReporte, ex.Message))
            End Try
        Next

        Return parametro

    End Function

    Private Function GetParametro(nomeReporte As String, nomeParametro As String) As Prosegur.Genesis.Report.Parametro
        Dim parametro As Prosegur.Genesis.Report.Parametro = Nothing
        Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)
        parametros = Nothing
        Try
            parametros = ObtenerParametros(nomeReporte)
            If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                parametro = parametros.Where(Function(p) p.Name.ToUpper = nomeParametro.ToUpper).FirstOrDefault()
            End If
        Catch ex As Exception
            Throw New Exception(String.Format(Traduzir("023_msg_erro_reporteNaoEncontrado"), nomeReporte, ex.Message))
        End Try

        Return parametro

    End Function


    Private Function VerificaParametroExiste(configuracion As IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte,
                                         nomeParametro As String, Optional obrigatoriedade As Boolean = False) As Boolean
        Dim erro As List(Of String) = New List(Of String)
        Dim existePamametro As Boolean = False
        '0 - Existe
        '1 - Obrigatorio
        Dim existeOuObrigatorio As Boolean = False


        Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)
        parametros = Nothing

        If configuracion.ParametrosReporte Is Nothing OrElse configuracion.ParametrosReporte.Count = 0 Then
            erro.Add(String.Format(Traduzir("023_msg_erro_sinParametros"), configuracion.DesConfiguracion))
        End If

        If ParametrosReporte Is Nothing Then
            ParametrosReporte = New Dictionary(Of String, List(Of Prosegur.Genesis.Report.Parametro))
        End If

        Dim caminho As String = RetornarDiretorio(configuracion.DesRuta)
        If String.IsNullOrEmpty(configuracion.DesRuta) Then
            erro.Add(String.Format(Traduzir("023_msgDiretorioInvalido"), configuracion.DesConfiguracion))
        ElseIf Not System.IO.Directory.Exists(caminho) Then
            erro.Add(String.Format(Traduzir("023_msgDiretorioInvalido"), configuracion.DesConfiguracion))
        End If

        For Each desReporte In configuracion.Reportes
            If Not ParametrosReporte.ContainsKey(desReporte.DesReporte) Then
                Try
                    parametros = ObtenerParametros(desReporte.DesReporte)
                Catch ex As Exception
                    erro.Add(String.Format(Traduzir("023_msg_erro_reporteNaoEncontrado"), configuracion.DesConfiguracion, ex.Message))
                End Try

                If parametros IsNot Nothing AndAlso parametros.Count > 0 Then
                    ParametrosReporte.Add(desReporte.DesReporte, parametros)
                End If
            Else
                parametros = ParametrosReporte(desReporte.DesReporte)
            End If

            If parametros IsNot Nothing AndAlso parametros.Count > 0 Then

                For Each parametro In parametros
                    Select Case parametro.Name
                        Case nomeParametro
                            If Not obrigatoriedade Then
                                'existe
                                existeOuObrigatorio = True
                            Else
                                If parametro.Required Then
                                    'Obrigatorio
                                    existeOuObrigatorio = True
                                End If
                            End If

                    End Select
                Next
            End If

        Next

        Return existeOuObrigatorio

    End Function
    ''' <summary>
    ''' Verifica se a configuração do reporte conten o parâmetro 'P_CON_DELEGACION_USUARIO'
    ''' caso tenha, deve validar se o usuário tem ao menos uma permissão para alguma delegação
    ''' se não tiver, remove da lista de configurações
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [victor.ramos] 17/03/2013 - Criado
    ''' </history>
    Private Function VerificaConfigsDelegacionesPermissao(configuracoes As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion)
        Dim configsDeletar As New List(Of Object)
        Dim possuiDelegacao As Boolean = False

        For Each config In configuracoes
            For Each delegacoesParam In config.Delegaciones
                Dim delegacoesParamLocal = delegacoesParam
                If delegacoesParam.codDelegacion.ToString.Equals(CONST_P_CODIGO_DELEGACION_USUARIO) Then
                    If (From c In InformacionUsuario.Delegaciones Where c.Codigo.ToString.Equals(delegacoesParamLocal.ParametroDelegacion.ToString) Select c).Count > 0 Then
                        'Se a configuração possuir o parâmetro 'P_CON_DELEGACION_USUARIO' 
                        'e o usuário possuir pelo menos uma delegação para alguma configuração
                        possuiDelegacao = True
                        Exit For
                    End If
                End If
            Next
            'Se a configuração possuir o parâmetro 'P_CON_DELEGACION_USUARIO' 
            'e o usuário não possuir pelo menos uma delegação para alguma configuração, remove a configuração
            If Not possuiDelegacao AndAlso config.Delegaciones IsNot Nothing AndAlso config.Delegaciones.Count > 0 AndAlso (From c In config.Delegaciones Where c.codDelegacion.ToString.Equals(CONST_P_CODIGO_DELEGACION_USUARIO) Select c).Count > 0 Then
                configsDeletar.Add(config)
            End If
        Next

        For Each del In configsDeletar
            configuracoes.Remove(del)
        Next

        Return configuracoes
    End Function


    Private Function ValidarParametro(Nullable As Boolean, count As Integer) As Boolean
        Return Nullable AndAlso count = 0
    End Function

    ''' <summary>
    ''' obtem os parametros do relatório
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [adans.klevanskis] 09/05/2013 - Criado
    ''' [claudioniz.pereira] 06/12/2013 - alterado
    ''' </history>
    Private Function ObtenerParametros(pathRelatorio As String) As List(Of Prosegur.Genesis.Report.Parametro)

        Dim parametros As New List(Of Prosegur.Genesis.Report.Parametro)

        'Verifica se o relatório existe na lista,
        If ParametrosReporte.ContainsKey(pathRelatorio) Then
            parametros = ParametrosReporte(pathRelatorio)
        End If

        Return parametros

    End Function

    ''' <summary>
    ''' Recupera o valor do parametro.
    ''' </summary>
    ''' <param name="CodigoParametro"></param>
    ''' <param name="DesParametro"></param>
    ''' <param name="DesValorParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RecuperarValorParametro(CodigoParametro As String, DesParametro As String, DesValorParametro As String) As Prosegur.Genesis.Report.ParametroReporte

        Return New Prosegur.Genesis.Report.ParametroReporte With {.CodParametro = CodigoParametro, _
                                                                  .DesParametro = DesParametro, _
                                                                  .DesValorParametro = DesValorParametro}
    End Function


    Private Sub RetornarParametrosGrupoClienteSubClientePuntoServico(ByRef objParametros As List(Of Prosegur.Genesis.Report.ParametroReporte),
                                                                     objPeticionGrupoClienteDetail As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion, _
                                                                     CodigoReporte As String, objReportes As Prosegur.Genesis.Report.RS2010.CatalogItem())

        Dim objProxyGrupoClientes As New ProxyGrupoClientes

        Dim ParametrosReporte = objGerarReport.ObtenerParametrosReporte(CodigoReporte, objReportes)
        Dim clientesGrupo = objProxyGrupoClientes.GetGruposClientesDetalle(objPeticionGrupoClienteDetail).GrupoCliente.FirstOrDefault.Clientes

        ' Para cada cliente existente no grupo
        For Each cliente In clientesGrupo

            ' Se existe o parâmetro de Cliente
            If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_CLIENTE).Count > 0 AndAlso
                String.IsNullOrEmpty(cliente.CodSubCliente) AndAlso String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                ' Adiciona o cliente a lista de parâmetros
                objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_CLIENTE, cliente.DesCliente, cliente.CodCliente))

            End If

            If Not String.IsNullOrEmpty(cliente.CodSubCliente) Then

                ' Se existe o parâmetro de SubCliente
                If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_SUBCLIENTE).Count > 0 AndAlso String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                    ' Adiciona o subcliente a lista de parâmetros
                    objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_SUBCLIENTE, cliente.DesSubCliente, cliente.CodCliente & "|" & cliente.CodSubCliente))

                End If

                If Not String.IsNullOrEmpty(cliente.CodPtoServicio) Then

                    ' Se existe o parâmetro de Ponto de Serviço
                    If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 Then

                        ' Adiciona o ponto de serviço a lista de parâmetros
                        objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_PUNTO_SERVICIO, cliente.DesPtoServivico, cliente.CodCliente & "|" & cliente.CodSubCliente & "|" & cliente.CodPtoServicio))

                    End If

                End If

            End If

        Next

        ' Se existe o parametro de Cliente e não existe o codigo do cliente
        If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_CLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = CONST_P_CODIGO_CLIENTE).FirstOrDefault() Is Nothing Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_CLIENTE, String.Empty, String.Empty))
        End If

        ' Se existe o parametro de SubCliente e não existe o codigo do subcliente
        If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_SUBCLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = CONST_P_CODIGO_SUBCLIENTE).FirstOrDefault() Is Nothing Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_SUBCLIENTE, String.Empty, String.Empty))
        End If

        ' Se existe o parametro de Ponto de Serviço e não existe o codigo do ponto de serviço
        If ParametrosReporte.Where(Function(pr) pr.Name = CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = CONST_P_CODIGO_PUNTO_SERVICIO).FirstOrDefault() Is Nothing Then
            ' Informa, pois é necessario pelo menos um código em branco
            objParametros.Add(RecuperarValorParametro(CONST_P_CODIGO_PUNTO_SERVICIO, String.Empty, String.Empty))
        End If


    End Sub

    Private Sub ExecutarGerarInforme(configuracion As IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte, ByRef erros As List(Of String))

        If configuracion.ParametrosReporte IsNot Nothing Then

            Dim strRuta As String = String.Empty
            If String.IsNullOrEmpty(configuracion.DesRuta) Then
                strRuta = Session("caminoRuta")
            Else
                strRuta = RetornarDiretorio(configuracion.DesRuta)
            End If

            'Para cada reporte
            For Each reporte In configuracion.Reportes
                Dim reporteLocal = reporte

                Try
                    'Verifica se o reporte salvo na configuração existe no Report Server
                    Dim existeReporte = (From r In Relatorios Where r.Name.Equals(reporteLocal.DesReporte)).FirstOrDefault
                    If existeReporte IsNot Nothing Then

                        Dim formatoArchivo As String = Nothing

                        Dim enumformat = (From p In [Enum].GetValues(GetType(Enumeradores.eFormatoArchivo))
                                         Where Int32.Parse(p) = reporteLocal.NecFormatoArchivo
                                         Select p).FirstOrDefault()

                        formatoArchivo = If(enumformat IsNot Nothing, Util.TraduzirFormatoArchivo(enumformat), Nothing)

                        'Retorna os parâmetros(do reporte service) preenchidos de acordo com o que foi salvo(BD) ou com o que foi inserido na tela
                        Dim parametrosPreenchidos = RetornarParametros(configuracion, reporte)
                        'Valida obrigatoriedade de todos parâmetros depois de preenchidos
                        Dim paramFaltando As Boolean = False
                        For Each parametro In ParametrosReporte(reporte.DesReporte)
                            Dim parametroLocal = parametro

                            Dim param = (From c In parametrosPreenchidos
                                         Where c.CodParametro.ToUpper = parametroLocal.NameOriginal.ToUpper).Count()

                            If ValidarParametro(parametro.Required, param) AndAlso parametro.Visible Then
                                'Se for parâmetro de delegação, nesse ponto irá chamar o RecuperarSelecionado() para validar obrigatoriedade
                                'e colocar o asterisco de erro no controle, nos outros pontos que foi chamado o RecuperarSelecionado(), foi passado false
                                'para não validar obrigatoriedade
                                If parametro.NameOriginal.ToUpper.Equals(CONST_P_CODIGO_DELEGACION) Then
                                    ucDelegacion.RecuperarSelecionado()
                                End If
                                If parametro.NameOriginal.ToUpper.Equals(CONST_P_CODIGO_DELEGACION_USUARIO) Then
                                    ucDelegacionUsuario.RecuperarSelecionado()
                                End If

                                erros.Add(String.Format(Traduzir("023_msg_erro_generico_configuracion_reporte"), reporte.DesReporte, configuracion.DesConfiguracion, String.Format(Traduzir("023_msg_erro_parametro_obligatorio"), parametro.Name)))
                                paramFaltando = True
                            End If

                        Next

                        'Se todos os parâmetros obrigátorios foram preenchidos
                        If Not paramFaltando Then
                            objGerarReport.GerarInforme(configuracion.IdentificadorConfiguracion, reporte.DesReporte, Relatorios, formatoArchivo, _
                                                  strRuta, configuracion.DesConfiguracion, _
                                                  parametrosPreenchidos, reporte.IdReporte, configuracion.NombreArchivo, reporte.ExtensionArchivo, reporte.Separador)
                        End If
                    Else
                        'Caso o reporte salvo na configuração não exista no Report Server
                        'add um  erro informando e continua gerando os que estão corretos
                        erros.Add(String.Format(Traduzir("023_msg_erro_reporte_nao_localizado"), reporte.DesReporte, configuracion.DesConfiguracion))
                    End If

                Catch ex As Excepcion.NegocioExcepcion
                    erros.Add(String.Format(Traduzir("023_msg_erro_generico_configuracion_reporte"), reporte.DesReporte, configuracion.DesConfiguracion, ex.Descricao))

                Catch ex As Exception

                    erros.Add(String.Format(Traduzir("023_msg_erro_generico_configuracion_reporte"), reporte.DesReporte, configuracion.DesConfiguracion, ex.Message))

                End Try

            Next

        End If
    End Sub

    ''' <summary>
    ''' Retorna os valores dos parametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarParametros(configuracion As IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte, _
                                       reporte As IAC.Integracion.ContractoServicio.Reportes.Reportes) As List(Of Prosegur.Genesis.Report.ParametroReporte)

        Dim objParametros As List(Of Prosegur.Genesis.Report.ParametroReporte) = Nothing

        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte.Count > 0 Then

            objParametros = New List(Of Prosegur.Genesis.Report.ParametroReporte)

            'Recupera os parâmetros do relatório no Reporte Service
            For Each parametro In ParametrosReporte(reporte.DesReporte)
                Dim parametroLocal = parametro

                Select Case parametro.Name

                    Case CONST_P_CODIGO_DELEGACION_USUARIO

                        Dim delegacoes = ucDelegacionUsuario.RecuperarSelecionado(False)

                        'Recupera as delegações que foram marcadas na tela, se não achar nenhuma pega as que foram salvas
                        If delegacoes IsNot Nothing AndAlso delegacoes.Count > 0 Then
                            For Each delegacao In delegacoes
                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte _
                                                  With {
                                                        .CodParametro = parametro.NameOriginal, _
                                                        .DesParametro = delegacao.Descripcion, _
                                                        .DesValorParametro = delegacao.Codigo})

                            Next

                        Else
                            Dim delegacion = From p In configuracion.ParametrosReporte
                                  Where p.CodNombreParametro = CONST_P_CODIGO_DELEGACION_USUARIO
                                  Select p
                            If delegacion IsNot Nothing AndAlso delegacion.Count > 0 Then

                                For i = 0 To delegacion.Count - 1
                                    If delegacion(i).EsTodos OrElse delegacion(i).EsTodos = 1 Then
                                        delegacion(i).EsTodos = -1
                                    End If
                                Next

                                For Each del In delegacion
                                    Dim delLocal = del
                                    'Valida se o usuário tem permissão na delegação salva no parâmetro
                                    If (From c In InformacionUsuario.Delegaciones Where c.Codigo.ToString.Equals(delLocal.CodParametro.ToString) Select c).Count > 0 Then
                                        Dim param As New Prosegur.Genesis.Report.ParametroReporte
                                        param.CodParametro = parametro.NameOriginal
                                        param.DesParametro = del.DesParametro
                                        param.DesValorParametro = del.CodParametro
                                        objParametros.Add(param)
                                    End If
                                Next

                            End If
                        End If

                    Case CONST_P_CODIGO_DELEGACION

                        Dim delegacoes = ucDelegacion.RecuperarSelecionado(False)

                        'Recupera as delegações que foram marcadas na tela, se não achar nenhuma pega as que foram salvas
                        If delegacoes IsNot Nothing AndAlso delegacoes.Count > 0 Then
                            For Each delegacao In delegacoes
                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte _
                                              With {
                                                    .CodParametro = parametro.NameOriginal, _
                                                    .DesParametro = delegacao.Descripcion, _
                                                    .DesValorParametro = delegacao.Codigo})
                            Next

                        Else
                            Dim delegacion = (From p In configuracion.ParametrosReporte
                            Where (p.CodNombreParametro = CONST_P_CODIGO_DELEGACION)
                              Select p)

                            If delegacion IsNot Nothing AndAlso delegacion.Count > 0 Then

                                For i = 0 To delegacion.Count - 1
                                    If delegacion(i).EsTodos OrElse delegacion(i).EsTodos = 1 Then
                                        delegacion(i).EsTodos = -1
                                    End If
                                Next

                                For Each del In delegacion
                                    Dim param As New Prosegur.Genesis.Report.ParametroReporte
                                    param.CodParametro = parametro.NameOriginal
                                    param.DesParametro = del.DesParametro
                                    param.DesValorParametro = del.CodParametro
                                    objParametros.Add(param)
                                Next
                            End If
                        End If
                    Case CONST_P_CODIGO_CLIENTE

                        Dim Clientes = From p In configuracion.ParametrosReporte
                                    Where p.CodNombreParametro = CONST_P_CODIGO_CLIENTE
                                    Select p

                        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

                            For Each objCli In Clientes
                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With { _
                                                  .CodParametro = parametro.NameOriginal, _
                                                  .DesValorParametro = If(objCli.EsTodos, "-1", objCli.CodParametro), _
                                                  .DesParametro = objCli.DesParametro})
                            Next

                        ElseIf objParametros.Where(Function(p) p.CodParametro = CONST_P_CODIGO_CLIENTE).FirstOrDefault() Is Nothing Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, String.Empty))
                        End If

                    Case CONST_P_FECHA_CONTEO_DESDE

                        If Not ucDataConteo.DataInicio.Equals(Date.MinValue) Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, ucDataConteo.DataInicio))
                        End If


                    Case CONST_P_FECHA_CONTEO_HASTA

                        If Not ucDataConteo.DataFin.Equals(Date.MinValue) Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, ucDataConteo.DataFin))
                        End If

                    Case CONST_P_FECHA_TRANSPORTE_DESDE

                        If Not ucDataTransporte.DataInicio.Equals(Date.MinValue) Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, ucDataTransporte.DataInicio))
                        End If

                    Case CONST_P_FECHA_TRANSPORTE_HASTA

                        If Not ucDataTransporte.DataFin.Equals(Date.MinValue) Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, ucDataTransporte.DataFin))
                        End If


                    Case CONST_P_CODIGO_SUBCLIENTE

                        Dim subClientes = (From p In configuracion.ParametrosReporte _
                                        Where p.CodNombreParametro = CONST_P_CODIGO_SUBCLIENTE _
                                        Select New IAC.Integracion.ContractoServicio.Reportes.ParametroReporte _
                                        With {.CodNombreParametro = p.CodNombreParametro, .DesParametro = p.DesParametro, _
                                                               .CodParametro = p.CodParametro}).ToList()

                        If subClientes IsNot Nothing AndAlso subClientes.Count > 0 Then


                            For Each objSCli In subClientes

                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With { _
                                                  .CodParametro = parametro.NameOriginal, _
                                                  .DesValorParametro = If(objSCli.EsTodos, "-1", objSCli.CodParametro), _
                                                  .DesParametro = objSCli.DesParametro})
                            Next

                        ElseIf objParametros.Where(Function(p) p.CodParametro = CONST_P_CODIGO_SUBCLIENTE).FirstOrDefault() Is Nothing Then
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, String.Empty))
                        End If

                    Case CONST_P_CODIGO_PUNTO_SERVICIO

                        Dim puntoServicios = configuracion.ParametrosReporte.Where(Function(p) p.CodNombreParametro = CONST_P_CODIGO_PUNTO_SERVICIO).ToList()

                        If puntoServicios IsNot Nothing AndAlso puntoServicios.Count > 0 Then

                            For Each objPto In puntoServicios

                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With {
                                                  .CodParametro = parametro.NameOriginal,
                                                  .DesValorParametro = If(objPto.EsTodos, "-1", objPto.CodParametro.Split("|")(2)),
                                                  .DesParametro = objPto.DesParametro})
                            Next

                        Else
                            objParametros.Add(RecuperarValorParametro(parametro.NameOriginal, String.Empty, String.Empty))
                        End If


                    Case CONST_P_CODIGO_GRUPO_CLIENTES

                        Dim grupoClientes = (From p In configuracion.ParametrosReporte
                                     Where p.CodNombreParametro = CONST_P_CODIGO_GRUPO_CLIENTES
                                     Select p).ToList

                        If grupoClientes IsNot Nothing AndAlso grupoClientes.Count > 0 Then

                            For Each objGrupoCli In grupoClientes
                                objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With { _
                                                  .CodParametro = parametro.NameOriginal, _
                                                  .DesValorParametro = If(objGrupoCli.EsTodos, "-1", objGrupoCli.CodParametro), _
                                                  .DesParametro = objGrupoCli.DesParametro})
                            Next

                            'objParametros.AddRange(grupoClientes)

                            'Recupera os codigos dos grupos gravados
                            Dim codigosGrupos As New List(Of String)
                            For Each objGrupoClie In grupoClientes
                                codigosGrupos.Add(objGrupoClie.CodParametro)
                            Next

                            ' Recupara os cliente, subcliente e pontos de serviços do grupo
                            Dim cliente = New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With
                                                                                    {.Codigo = codigosGrupos}

                            'Adiciona objParametros por referência
                            RetornarParametrosGrupoClienteSubClientePuntoServico(objParametros, cliente, reporte.DesReporte, Relatorios)
                        End If

                    Case CONST_P_DE_PARA

                        Dim Parametros As List(Of Prosegur.Genesis.Report.Parametro) = ParametrosReporte(reporte.DesReporte)

                        If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then

                            Dim objParametro As Prosegur.Genesis.Report.Parametro = (From Pr In Parametros Where Pr.Name = CONST_P_DE_PARA).FirstOrDefault

                            If objParametro IsNot Nothing AndAlso objParametro.ValidValue IsNot Nothing AndAlso
                                objParametro.ValidValue.ParameterValues IsNot Nothing AndAlso objParametro.ValidValue.ParameterValues.Count > 0 Then

                                For Each valores In objParametro.ValidValue.ParameterValues

                                    objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With { _
                                                      .CodParametro = parametro.NameOriginal, _
                                                      .DesParametro = valores.Label, _
                                                      .DesValorParametro = valores.Value})

                                Next

                            End If

                        End If

                    Case Else

                        For Each objParam In (From p In configuracion.ParametrosReporte Where p.CodNombreParametro = parametroLocal.Name)

                            objParametros.Add(New Prosegur.Genesis.Report.ParametroReporte With { _
                                              .CodParametro = parametro.NameOriginal, _
                                              .DesParametro = objParam.DesParametro, _
                                              .DesValorParametro = objParam.CodParametro})
                        Next

                End Select

            Next

        End If

        Return objParametros
    End Function

    ''' <summary>
    ''' Retorna o diretorio onde vai ser saldo o reporte
    ''' </summary>
    ''' <param name="DesRuta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RetornarDiretorio(DesRuta As String) As String
        Dim Path As String = String.Empty

        If Not String.IsNullOrEmpty(DesRuta) Then

            Dim ReporteUrl As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_URL")
            Dim ReporteHome As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("REPO_HOME")


            If Not String.IsNullOrEmpty(ReporteUrl) Then
                ReporteUrl = ReporteUrl.Replace("\", "")
            End If

            If Not String.IsNullOrEmpty(ReporteHome) Then
                ReporteHome = ReporteHome.Replace("\", "")
            End If

            If Not (String.IsNullOrEmpty(ReporteUrl) AndAlso String.IsNullOrEmpty(ReporteHome) AndAlso String.IsNullOrEmpty(DesRuta)) Then
                Path = String.Format("\\{0}\{1}\{2}", ReporteUrl, ReporteHome, DesRuta)
            End If
        End If

        Return Path
    End Function

    ''' <summary>
    ''' Retorna o caminho completo do diretorio
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function RetornaCaminho(path As String) As String
        Dim validador = path(path.Length - 1)
        Return If(validador = "/" OrElse validador = "\", path.Substring(0, path.Length - 1), path)
    End Function

#End Region

#Region "[PROPRIEDADES]"

    Private Property Relatorios As Prosegur.Genesis.Report.RS2010.CatalogItem()
        Get
            Return Session("Relatorios")
        End Get
        Set(value As Prosegur.Genesis.Report.RS2010.CatalogItem())
            Session("Relatorios") = value
        End Set
    End Property

    Public ReadOnly Property reportesSelecionados() As List(Of String)
        Get
            If String.IsNullOrEmpty(hdReportes.Value) Then
                Return New List(Of String)
            Else
                Dim separador As String() = {"|"}
                Dim arrIdentificador As String() = hdReportes.Value.Split(separador, StringSplitOptions.RemoveEmptyEntries)

                If arrIdentificador IsNot Nothing AndAlso arrIdentificador.Count > 0 Then
                    Return arrIdentificador.ToList()
                End If
            End If

            Return DirectCast(ViewState("reportesSelecionados"), List(Of String))
        End Get
    End Property

    Public WriteOnly Property IdentificadorConfiguracion As String
        Set(value As String)
            Session("IdentificadorConfiguracion") = value
        End Set
    End Property

    Public Property ConfiguracionesRecuperadas As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion
        Get
            Return Session("ConfiguracionesRecuperadas")
        End Get
        Set(value As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion)
            Session("ConfiguracionesRecuperadas") = value
        End Set
    End Property


#End Region

#Region "EVENTOS"

#Region "[EVENTOS - GRIDVIEW]"

    Protected Sub gvReportes_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            Try
                If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                    'Seta as propriedades do radio button

                    Dim rbSelecionado As HtmlInputCheckBox = CType(gvReportes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionado"), HtmlInputCheckBox)
                    rbSelecionado.Value = gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString()
                    Dim jsScript As String = "javascript: AddRemovIdSelect(this,'" & hdReportes.ClientID & "',false,''); ExecutarClick('" & btnConfEscogido.ClientID & "');"
                    rbSelecionado.Attributes.Add("onclick", jsScript)

                    Dim existe = (From p In reportesSelecionados
                           Where p.Equals(gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString())
                           Select p).ToList()
                    If existe IsNot Nothing AndAlso existe.Count() = 1 Then
                        rbSelecionado.Checked = True
                    End If

                End If
            Catch ex As Exception
                MyBase.MostraMensagemExcecao(ex)
            End Try
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gvReportes_OnPageIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim configuracoes = getConfiguracionesReportes.ConfiguracionesReportes
            configuracoes = VerificaConfigsDelegacionesPermissao(configuracoes)

            Dim ConfiguracionesMarcadas As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporteColeccion


            'Recupera as configurações selecionadas para add no inicio da lista
            'Quando for uma busca deve manter os selecionados e add o resultado da busca no final
            If (reportesSelecionados IsNot Nothing AndAlso reportesSelecionados.Count > 0) AndAlso
                ConfiguracionesRecuperadas IsNot Nothing AndAlso ConfiguracionesRecuperadas.Count > 0 Then

                Dim Valores As List(Of String) = reportesSelecionados

                If Valores IsNot Nothing AndAlso Valores.Count > 0 Then

                    Dim config As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportes.ConfiguracionReporte = Nothing

                    For Each ValorSel In Valores
                        Dim ValorSelLocal = ValorSel

                        config = (From con In ConfiguracionesRecuperadas Where con.IdentificadorConfiguracion = ValorSelLocal).FirstOrDefault

                        If config IsNot Nothing Then

                            If ConfiguracionesMarcadas.FindAll(Function(c) c.IdentificadorConfiguracion = config.IdentificadorConfiguracion).Count = 0 Then
                                'Adiciona as marcadas primeiramente, para ficar no inicio
                                ConfiguracionesMarcadas.Add(config)
                                'remove a mesma para não ficar duplicada
                                configuracoes.RemoveAll(Function(c) c.IdentificadorConfiguracion = config.IdentificadorConfiguracion)
                            End If

                        End If

                    Next

                End If

            End If

            'Adiciona as que não estão marcadas no final
            If configuracoes IsNot Nothing AndAlso configuracoes.Count > 0 Then
                ConfiguracionesMarcadas.AddRange(configuracoes.OrderBy(Function(o) o.DesConfiguracion))
            End If

            If ConfiguracionesMarcadas IsNot Nothing AndAlso ConfiguracionesMarcadas.Count > 0 Then

                pnlSemRegistro.Visible = False
                Dim objDt As DataTable = Util.ConverterListParaDataTable(ConfiguracionesMarcadas)

                gvReportes.DataSource = objDt
                gvReportes.DataBind()

            Else

                gvReportes.DataSource = Nothing
                gvReportes.DataBind()

                pnlSemRegistro.Visible = True
                lblSemRegistro.Text = Traduzir("info_msg_grd_vazio")
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub gvReportes_OnProcessOnClickRowFilter(sender As Object, e As ASPxGridViewOnClickRowFilterEventArgs)
        Try
            If e.Criteria.Any(Function(x) x.Key = "DesConfiguracion") Then
                Dim valCodigo As String = e.Values.FirstOrDefault(Function(x) x.Key = "DesConfiguracion").Value
                txtBuscar.Text = valCodigo

            End If
            PreencherGridViewReportes(True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS - BOTOES]"

    Private Sub btnConfEscogido_Click(sender As Object, e As EventArgs) Handles btnConfEscogido.Click
        Try
            VerificaCamposFechaDelegacion()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Protected Sub btnAdicionar_Click(sender As Object, e As EventArgs) Handles btnAdicionar.Click

        Try

            Dim url As String = "ConfiguracionReport.aspx?acao=" & Enumeradores.eAcao.Alta

            'Page.Response.Redireccionar(url)
            Master.ExibirModal(url, Traduzir("027_titulo_pagina"), 600, 1034)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnGerarRelatorio_Click(sender As Object, e As EventArgs) Handles btnGerarRelatorio.Click
        Try

            If ValidaDatos() Then
                Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Peticion
                Dim respuesta As IAC.Integracion.ContractoServicio.Reportes.GetConfiguracionesReportesDetail.Respuesta = Nothing
                Dim objProxy As New Comunicacion.ProxyIacIntegracion

                Dim erros As New List(Of String)
                Dim listaConfiguraciones As New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporteColeccion
                Session("listaConfiguraciones") = listaConfiguraciones
                peticion.IdentificadoresConfiguracion = reportesSelecionados()
                respuesta = objProxy.GetConfiguracionesReportesDetail(peticion)

                Dim msgerro As String = String.Empty
                If Master.ControleErro.VerificaErro2(respuesta.CodigoError, _
                                                    ContractoServ.Login.ResultadoOperacionLoginLocal.Error, msgerro, _
                                                    respuesta.MensajeError) Then

                    ' Calcula o time out para gerar todos os relatórios
                    MyBase.scriptManager.AsyncPostBackTimeout = MyBase.scriptManager.AsyncPostBackTimeout + respuesta.ConfiguracionesReportes.Sum(Function(cr) cr.Reportes.Count) * If(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Timeout") Is Nothing, 900, Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("Timeout"))

                    'Recupera os relatorios
                    MyBase.objGerarReport.Autenticar(False)

                    Try
                        Relatorios = MyBase.objGerarReport.DisplayItems(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
                    Catch ex As Exception
                        'Se o diretório não existir ou não estiver acessível
                        If ex.Message.Contains("cannot be found") Then
                            erros.Add(Traduzir("023_msg_diretorio_reportes_invalido"))
                        Else
                            erros.Add(ex.Message)
                        End If
                    End Try

                    'Se o diretório do reportes estiver correto
                    If erros.Count = 0 Then
                        Dim arquivoSemRuta As Boolean = False

                        Dim strRuta = Path.Combine(HttpContext.Current.Server.MapPath(""), Constantes.CONST_REPORTES_TEMP, InformacionUsuario.Nombre)
                        Session("caminoRuta") = strRuta

                        'se já existe o diretório para esse usuário então exclui
                        'senão cria
                        If Directory.Exists(strRuta) Then
                            Directory.Delete(strRuta, True)
                        End If

                        Directory.CreateDirectory(strRuta)

                        'GERAR OS REPORTES
                        For Each configuracion In respuesta.ConfiguracionesReportes
                            'Gera o relatório
                            'verifica se os dados na tela estão preenchidos corretamente
                            If ValidaDatosConfiguraciones(configuracion, erros) Then
                                ExecutarGerarInforme(configuracion, erros)
                                If String.IsNullOrEmpty(configuracion.DesRuta) Then
                                    arquivoSemRuta = True
                                End If
                            End If
                        Next

                        If erros.Count = 0 Then

                            'abre diretorio do reporte se todos os caminhos especificados forem iguais
                            Dim pasta = (From p In respuesta.ConfiguracionesReportes Where Not String.IsNullOrEmpty(p.DesRuta)
                                          Select RetornaCaminho(p.DesRuta).ToUpper()).Distinct()

                            ' Verifica se a pasta existe
                            If pasta IsNot Nothing AndAlso pasta.Count = 1 Then

                                ' Exibe a mensagem com a pasta
                                MyBase.MostraMensagem(Traduzir("info_msg_sucesso") & Constantes.LineBreak & String.Format(Traduzir("info_msg_ruta"), RetornarDiretorio(pasta.FirstOrDefault)))
                            End If

                            'verifica se foi gerado algum arquivo 
                            If arquivoSemRuta AndAlso Directory.GetFiles(Session("caminoRuta")).Count > 0 Then
                                Session("NombreArchivoZip") = Constantes.CONST_REPORTES_NOMBRE_ZIP
                                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "window.location.href = 'ConfiguracionReportMostrar.aspx'", True)
                            End If

                        End If

                    End If

                    If erros IsNot Nothing AndAlso erros.Count > 0 Then
                        MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
                    End If
                Else
                    MyBase.MostraMensagem(msgerro)
                End If

            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub


    Protected Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        Try

            If ValidaExcluir() Then

                Dim reportes As New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporteColeccion
                Dim objProxy As New Comunicacion.ProxyIacIntegracion

                Dim peticion As New IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Peticion
                Dim respuesta As IAC.Integracion.ContractoServicio.Reportes.SetConfiguracionReporte.Respuesta = Nothing

                For Each selecionados In reportesSelecionados
                    Dim configuracion As New IAC.Integracion.ContractoServicio.Reportes.ConfiguracionReporte()
                    With configuracion
                        .IdentificadorConfiguracion = selecionados
                    End With
                    reportes.Add(configuracion)
                Next

                peticion.EsExclusion = True
                peticion.ConfiguracionesReportes = reportes

                respuesta = objProxy.SetConfiguracionReporte(peticion)

                Dim msgerro As String = String.Empty
                If (Master.ControleErro.VerificaErro2(respuesta.CodigoError, _
                                                    ContractoServ.Login.ResultadoOperacionLoginLocal.Error, msgerro, _
                                                    respuesta.MensajeError)) Then
                    PreencherGridViewReportes()

                    hdReportes.Value = String.Empty

                    ucDelegacion.Visible = False
                    ucDelegacionUsuario.Visible = False
                    ucDataConteo.Visible = False
                    ucDataTransporte.Visible = False

                Else
                    MostraMensagem(msgerro)
                End If

            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Try
            'verifica se os dados na tela estão preenchidos corretamente
            If ValidaEditar() Then
                Dim url As String = "ConfiguracionReport.aspx?acao=" & Enumeradores.eAcao.Modificacion


                IdentificadorConfiguracion = reportesSelecionados.FirstOrDefault()

                Master.ExibirModal(url, Traduzir("027_titulo_pagina"), 600, 1034)

            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#End Region

    Private Sub VerificaCamposFechaDelegacion()
        Try
            Threading.Thread.Sleep(500)
            Dim configuracoesDetail = getConfiguracionesReportesDetail.ConfiguracionesReportes

            ucDelegacion.Visible = False
            ucDelegacionUsuario.Visible = False
            ucDataConteo.Visible = False
            ucDataTransporte.Visible = False

            Dim fechaConteoDesde As Boolean
            Dim fechaConteoDesdeObrigatorio As Boolean

            Dim fechaConteoHasta As Boolean
            Dim fechaConteoHastaObrigatorio As Boolean

            Dim fechaTransporteDesde As Boolean
            Dim fechaTransporteDesdeObrigatorio As Boolean

            Dim fechaTransporteHasta As Boolean
            Dim fechaTransporteHastaObrigatorio As Boolean

            If configuracoesDetail IsNot Nothing AndAlso configuracoesDetail.Count > 0 Then
                For Each config In configuracoesDetail
                    'Recupera parâmetros de delegaciones, existência e obrigatoriedade

                    Dim parametro As Prosegur.Genesis.Report.Parametro = Nothing
                    'Delegação
                    For Each relatorio In config.Reportes
                        parametro = GetParametro(relatorio.DesReporte, CONST_P_CODIGO_DELEGACION)
                        If parametro IsNot Nothing Then
                            ucDelegacion.Visible = parametro.Visible
                            ucDelegacion.CampoObrigatorio = parametro.Required
                            ucDelegacion.MultiSelecao = parametro.MultiValue
                            Exit For
                        End If
                    Next

                    'Delegação Usuário
                    For Each relatorio In config.Reportes
                        parametro = GetParametro(relatorio.DesReporte, CONST_P_CODIGO_DELEGACION_USUARIO)
                        If parametro IsNot Nothing Then
                            ucDelegacionUsuario.Visible = parametro.Visible
                            ucDelegacionUsuario.CampoObrigatorio = parametro.Required
                            ucDelegacionUsuario.MultiSelecao = parametro.MultiValue
                            Exit For
                        End If
                    Next

                    If Not fechaConteoDesde Then
                        fechaConteoDesde = VerificaParametroExiste(config, CONST_P_FECHA_CONTEO_DESDE)
                    End If

                    If Not fechaConteoDesdeObrigatorio Then
                        fechaConteoDesdeObrigatorio = VerificaParametroExiste(config, CONST_P_FECHA_CONTEO_DESDE, True)
                    End If


                    If Not fechaConteoHasta Then
                        fechaConteoHasta = VerificaParametroExiste(config, CONST_P_FECHA_CONTEO_HASTA)
                    End If

                    If Not fechaConteoHastaObrigatorio Then
                        fechaConteoHastaObrigatorio = VerificaParametroExiste(config, CONST_P_FECHA_CONTEO_HASTA, True)
                    End If


                    If Not fechaTransporteDesde Then
                        fechaTransporteDesde = VerificaParametroExiste(config, CONST_P_FECHA_TRANSPORTE_DESDE)
                    End If

                    If Not fechaTransporteDesdeObrigatorio Then
                        fechaTransporteDesdeObrigatorio = VerificaParametroExiste(config, CONST_P_FECHA_TRANSPORTE_DESDE, True)
                    End If


                    If Not fechaTransporteHasta Then
                        fechaTransporteHasta = VerificaParametroExiste(config, CONST_P_FECHA_TRANSPORTE_HASTA)
                    End If

                    If Not fechaTransporteHastaObrigatorio Then
                        fechaTransporteHastaObrigatorio = VerificaParametroExiste(config, CONST_P_FECHA_TRANSPORTE_HASTA, True)
                    End If

                Next
            End If

            'Recupera as delegações 
            Dim delegaciones As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
            If ucDelegacion.Visible Then
                For Each del In InformacionUsuario.Delegaciones
                    Dim delegacao As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion
                    delegacao.Codigo = del.Codigo
                    delegacao.Descripcion = del.Descripcion
                    delegacao.OidDelegacion = String.Empty
                    delegaciones.Add(delegacao)
                Next
            End If

            'Recupera as delegações 
            Dim delegacionesUsu As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
            If ucDelegacionUsuario.Visible Then
                For Each del In InformacionUsuario.Delegaciones
                    Dim delegacao As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion
                    delegacao.Codigo = del.Codigo
                    delegacao.Descripcion = del.Descripcion
                    delegacao.OidDelegacion = String.Empty
                    delegacionesUsu.Add(delegacao)
                Next
            End If

            'Recupera as delegações salvas nos parametros, para ser marcadas no controle a medida que seleciona uma configuração no grid
            Dim delegacionesParam As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion
            Dim Delegacoes As New List(Of String)
            If ucDelegacion.Visible OrElse ucDelegacionUsuario.Visible Then
                Dim marcados = reportesSelecionados
                For Each configSelecionado In marcados
                    Dim configSelecionadoLocal = configSelecionado

                    Dim confg = (From p In configuracoesDetail Where p.IdentificadorConfiguracion.ToString.Equals(configSelecionadoLocal.ToString()) Select p).FirstOrDefault

                    If confg IsNot Nothing Then

                        Dim DelegacoesParametro As List(Of IAC.Integracion.ContractoServicio.Reportes.ParametroReporte) = Nothing

                        If ucDelegacion.Visible AndAlso ucDelegacionUsuario.Visible Then

                            DelegacoesParametro = (From p In confg.ParametrosReporte
                                                   Where (p.CodNombreParametro.ToString.Equals(CONST_P_CODIGO_DELEGACION) OrElse
                                                          p.CodNombreParametro.ToString.Equals(CONST_P_CODIGO_DELEGACION_USUARIO)) Select p).ToList

                        ElseIf ucDelegacion.Visible Then

                            DelegacoesParametro = (From p In confg.ParametrosReporte
                                                   Where p.CodNombreParametro.ToString.Equals(CONST_P_CODIGO_DELEGACION) Select p).ToList
                        Else

                            DelegacoesParametro = (From p In confg.ParametrosReporte
                                                   Where p.CodNombreParametro.ToString.Equals(CONST_P_CODIGO_DELEGACION_USUARIO) Select p).ToList

                        End If

                        For Each t In DelegacoesParametro
                            Dim delegacao As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion
                            delegacao.Codigo = t.CodParametro.ToString
                            delegacao.Descripcion = t.DesParametro.ToString
                            delegacao.OidDelegacion = String.Empty
                            delegacao.CodNombreParametro = t.CodNombreParametro
                            If Not delegacionesParam.Contains(delegacao) Then
                                delegacionesParam.Add(delegacao)
                            End If
                        Next
                    End If
                Next
            End If

            'Recupera as delegações marcadas no controle, se ela não existir na lista "delegacionesParam", então add para ser marcado.
            'Colocou isso pois estava perdendo as delegações selecionadas pelo usuário na tela.
            'visto que esse método é utilizado em cada click do grid
            If ucDelegacion.Visible Then

                Dim objDelegaciones As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = ucDelegacion.RecuperarSelecionado(False)
                Dim delegacionesRemove As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

                If objDelegaciones IsNot Nothing AndAlso objDelegaciones.Count > 0 Then

                    For Each objDel In objDelegaciones
                        Dim objDelLocal = objDel

                        If delegacionesParam.FindAll(Function(d) d.Codigo = objDelLocal.Codigo).Count = 0 Then

                            delegacionesParam.Add(New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion With { _
                                                  .Codigo = objDel.Codigo, _
                                                  .Descripcion = objDel.Descripcion, _
                                                  .OidDelegacion = objDel.OidDelegacion, _
                                                  .CodNombreParametro = objDel.CodNombreParametro})
                        End If
                    Next

                    'Remove delegações marcada que não seja de nenhuma configuração marcada
                    Dim marcados = reportesSelecionados
                    For Each objDel In delegacionesParam
                        Dim objDelLocal = objDel
                        Dim existe As Boolean = False
                        For Each configSelecionado In marcados
                            Dim configSelecionadoLocal = configSelecionado
                            Dim confg = (From p In configuracoesDetail Where p.IdentificadorConfiguracion.ToString.Equals(configSelecionadoLocal.ToString()) Select p)

                            For Each c In confg

                                If c.ParametrosReporte.FindAll(Function(d) d.CodParametro = objDelLocal.Codigo).Count > 0 Then
                                    existe = True
                                End If

                            Next

                        Next
                        If Not existe Then
                            delegacionesRemove.Add(objDel)
                        End If
                    Next

                    For Each delRemove In delegacionesRemove
                        delegacionesParam.Remove(delRemove)
                    Next

                End If
            End If

            'Mesmo caso acima, porém com controle de delegações do usuário
            If ucDelegacionUsuario.Visible Then

                Dim objDelegacionesUsuario As IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion = ucDelegacionUsuario.RecuperarSelecionado(False)
                Dim delegacionesRemove As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

                If objDelegacionesUsuario IsNot Nothing AndAlso objDelegacionesUsuario.Count > 0 Then

                    For Each objDel In objDelegacionesUsuario
                        Dim objDelLocal = objDel

                        If delegacionesParam.FindAll(Function(d) d.Codigo = objDelLocal.Codigo).Count = 0 Then

                            delegacionesParam.Add(New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.Delegacion With { _
                                                  .Codigo = objDel.Codigo, _
                                                  .Descripcion = objDel.Descripcion, _
                                                  .OidDelegacion = objDel.OidDelegacion, _
                                                  .CodNombreParametro = objDel.CodNombreParametro})
                        End If
                    Next

                    'Remove delegações marcada que não seja de nenhuma configuração marcada
                    Dim marcados = reportesSelecionados
                    For Each objDel In delegacionesParam
                        Dim objDelLocal = objDel
                        Dim existe As Boolean = False
                        For Each configSelecionado In marcados
                            Dim configSelecionadoLocal = configSelecionado
                            Dim confg = (From p In configuracoesDetail Where p.IdentificadorConfiguracion.ToString.Equals(configSelecionadoLocal.ToString()) Select p)

                            For Each c In confg

                                If c.ParametrosReporte.FindAll(Function(d) d.CodParametro = objDelLocal.Codigo).Count > 0 Then
                                    existe = True
                                End If

                            Next
                        Next
                        If Not existe Then
                            delegacionesRemove.Add(objDel)
                        End If
                    Next

                    For Each delRemove In delegacionesRemove
                        delegacionesParam.Remove(delRemove)
                    Next

                End If
            End If

            'Mostra os controles de delegações
            'Se houver parâmetro de delegação e delegação usuário
            If ucDelegacion.Visible Then

                AddHandler ucDelegacion.OcorreuErro, AddressOf ControleOcorreuErro

                Dim parametroDelegaciones As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

                Dim parametroDelegacionesSelecionados = delegacionesParam.FindAll(Function(d) d.CodNombreParametro.Equals(CONST_P_CODIGO_DELEGACION)).ToList()

                If parametroDelegacionesSelecionados IsNot Nothing AndAlso parametroDelegacionesSelecionados.Count > 0 Then
                    parametroDelegaciones.AddRange(parametroDelegacionesSelecionados)
                End If
                ucDelegacion.PopularControle(parametroDelegaciones, Nothing)

            End If

            If ucDelegacionUsuario.Visible Then

                Dim parametroDelegacionesUsuario As New IAC.ContractoServicio.Utilidad.GetComboDelegaciones.DelegacionColeccion

                Dim parametroDelegacionesSelecionados = delegacionesParam.FindAll(Function(d) d.CodNombreParametro.Equals(CONST_P_CODIGO_DELEGACION_USUARIO)).ToList()

                If parametroDelegacionesSelecionados IsNot Nothing AndAlso parametroDelegacionesSelecionados.Count > 0 Then
                    parametroDelegacionesUsuario.AddRange(parametroDelegacionesSelecionados)
                End If

                ucDelegacionUsuario.PopularControle(parametroDelegacionesUsuario, delegacionesUsu)

            End If

            'Habilita controle de datas
            If fechaConteoDesde Then
                ucDataConteo.Visible = True
                ucDataConteo.LabelData = Traduzir("023_lblFechaConteo")
                'Verifica Obrigatoriedade
                If fechaConteoDesdeObrigatorio OrElse fechaConteoHastaObrigatorio Then
                    ucDataConteo.DatasObrigatorias = True
                End If

                If fechaConteoHasta Then
                    ucDataConteo.DataFinalVisivel = True
                Else
                    ucDataConteo.DataFinalVisivel = False
                End If
            Else
                ucDataConteo.DataInicio = String.Empty
                ucDataConteo.DataFin = String.Empty
            End If

            'Habilita controle de datas
            If fechaTransporteDesde Then
                ucDataTransporte.Visible = True
                ucDataTransporte.LabelData = Traduzir("023_lblFechaTransporte")
                'Verifica Obrigatoriedade
                If fechaTransporteDesdeObrigatorio OrElse fechaTransporteHastaObrigatorio Then
                    ucDataTransporte.DatasObrigatorias = True
                End If

                If fechaTransporteHasta Then
                    ucDataTransporte.DataFinalVisivel = True
                Else
                    ucDataTransporte.DataFinalVisivel = False
                End If
            Else
                ucDataTransporte.DataInicio = String.Empty
                ucDataTransporte.DataFin = String.Empty
            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try
    End Sub

    'Protected Sub btnBuscarConfig_Click(sender As Object, e As EventArgs) Handles btnBuscarConfig.Click

    '    Try
    '        PreencherGridViewReportes(True)
    '    Catch ex As Excepcion.NegocioExcepcion
    '        MyBase.MostraMensagemExcecao(ex)
    '    Catch ex As Exception
    '        MyBase.MostraMensagemExcecao(ex)
    '    End Try
    'End Sub

    Private Sub Reportes_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim objReport As New Prosegur.Genesis.Report.Gerar()
                objReport.Autenticar(False)
                Relatorios = objReport.ListaCatalogItem(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnConfiguracionGeneral_Click(sender As Object, e As System.EventArgs) Handles btnConfiguracionGeneral.Click
        Try
            '
            Dim url As String = "ConfiguracionGeneral.aspx?"

            'Page.Response.Redireccionar(url)
            Master.ExibirModal(url, Traduzir("026_titulo_pagina"), 600, 1034)

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub chbSelecionar_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Try

            Me.VerificaCamposFechaDelegacion()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ControleOcorreuErro(sender As Object, e As ExcepcionEventArgs)
        Master.ControleErro.AddError(e.Erro, Enumeradores.eMensagem.Informacao)
        MyBase.MostraMensagem(e.Erro)
    End Sub


End Class