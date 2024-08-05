Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Report
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis

Public Class ConfiguracionGeneral
    Inherits Base

    Private Property DesativarRadioButtons As Boolean
        Get
            If ViewState("RadisoDesativados") Is Nothing Then
                ViewState("RadisoDesativados") = False
            End If
            Return CType(ViewState("RadisoDesativados"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("RadisoDesativados") = value
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
                Else
                    Return New List(Of String)
                End If
            End If
        End Get
    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Not Me.IsPostBack Then
                'Atualiza os parametros dos relatórios.
                MyBase.ParametrosReporte = Util.RecuperarParametrosRelatorios()

                Me.PreencherGridViewReportes(Me.getConfiguracionesReportes())

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Popula a combo de relatorios
    ''' </summary>
    ''' <remarks></remarks>
    Private Function getConfiguracionesReportes() As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Dim listaConfiguraciones As New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Dim listaRetorno As New List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral)
        Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing
        Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral
        respuesta = objProxy.GetConfiguracionGeneralReportes()
        If respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            listaConfiguraciones = respuesta.ConfiguracionGeneralColeccion

            Dim objReport As New Prosegur.Genesis.Report.Gerar()
            objReport.Autenticar(False)

            Dim Relatorios = objReport.ListaCatalogItem(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
            If Relatorios IsNot Nothing AndAlso Relatorios.Count > 0 Then
                'Verifica os relatórios já foram configurados
                ' se sim então recupera as configuraçães
                'var result = db.Parts.Where(p => query.All(q => p.partName.Contains(q)));
                Dim nomesRelatorios As List(Of String) = (From r In Relatorios Select r.Name).ToList
                For Each config In listaConfiguraciones.Where(Function(r) nomesRelatorios.Contains(r.DesReporte))
                    config.FormatoArchivo = Util.TraduzirFormatoArchivo(config.FormatoArchivo)
                    listaRetorno.Add(config)
                Next
            End If
        End If

        Return listaRetorno
    End Function

    ''' <summary>
    ''' Preenche o gridview Reportes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Private Sub PreencherGridViewReportes(configuraciones As List(Of IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral))
        Try
            If configuraciones IsNot Nothing AndAlso configuraciones.Count > 0 Then

                Dim objDt As DataTable = Util.ConverterListParaDataTable(configuraciones)

                objDt.DefaultView.Sort = " DesReporte ASC"

                gvReportes.DataSource = objDt
                gvReportes.DataBind()
            Else
                gvReportes.DataSource = Nothing
                gvReportes.DataBind()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Enumeradores.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If

        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.REPORTES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

        MyBase.ValidarAcesso = False

    End Sub

    ''' <summary>
    ''' Traduz os controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        btnNovo.Text = Traduzir("btnAlta")
        btnEditar.Text = Traduzir("btnModificacion")
        btnValidaEditar.Value = Traduzir("btnModificacion")
        btnExcluir.Value = Traduzir("btnBaja")

        Me.lblTituloConfiguracionGeneral.Text = Traduzir("026_titulo_pagina")

        '   gvReportes.Columns(0).Caption = Traduzir("026_col_selecionar")
        gvReportes.Columns(1).Caption = Traduzir("026_col_DesReporte")
        gvReportes.Columns(2).Caption = Traduzir("026_col_CodReporte")
        gvReportes.Columns(3).Caption = Traduzir("026_col_FormatoArchivo")
        gvReportes.Columns(4).Caption = Traduzir("026_col_ExtensionArchivo")
        gvReportes.Columns(5).Caption = Traduzir("026_col_Separador")

        gvReportes.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")
        gvReportes.SettingsText.EmptyDataRow = Traduzir("info_msg_grd_vazio")

        MyBase.TraduzirControles()
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")

        Me.lblPnFormulario.Text = Traduzir("027_titulo_pagina")
        Me.lblReporte.Text = Traduzir("027_lbl_Reporte")
        Me.lblIDReporte.Text = Traduzir("027_lbl_IDReporte")
        Me.lblFormatoSaida.Text = Traduzir("027_lbl_FormatoArchivo")
        Me.lblExtensaoArquivo.Text = Traduzir("027_lbl_ExtensionArchivo")
        Me.lblSeparador.Text = Traduzir("027_lbl_Separador")


    End Sub

    Protected Overrides Sub AdicionarControlesValidacao()
        MyBase.AdicionarControlesValidacao()
    End Sub

    '''<summary>
    ''' Adiciona scripts 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [daniel.nunes] 31/07/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        MyBase.AdicionarScripts()
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        Dim _ValidaAcesso = New ValidacaoAcesso(InformacionUsuario)

        If Not _ValidaAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                           Enumeradores.eAcoesTela._MODIFICAR) Then
            btnNovo.Visible = False
            btnEditar.Visible = False
            btnExcluir.Visible = False

            gvReportes.Enabled = False

        End If

        If btnExcluir.Visible Then
            Dim s As String = "SelecionarConfirmarExclusao('" & gvReportes.ClientID & "','" & Traduzir(Constantes.InfoMsgSeleccionarRegistro) & "','','" & Traduzir(Constantes.InfoMsgBajaRegistro) & "','" & btnChamarExcluir.ClientID & "');"
            btnExcluir.Attributes.Add("onclick", s)
        End If

        If btnValidaEditar.Visible Then
            Dim s As String = "SelecionarConfirmarExclusao('" & gvReportes.ClientID & "','" & Traduzir(Constantes.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Constantes.InfoMsgSeleccionarRegistroUnico) & "','','" & btnEditar.ClientID & "');"
            btnValidaEditar.Attributes.Add("onclick", s)

        End If

    End Sub

    ''' <summary>
    ''' Chama a tela de inclusão/alteração.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            hdReportes.Value = String.Empty
            PopularFormatoArchivo()
            PopularRelatorios()
            ExibirFormulario(True)
            Acao = Enumeradores.eAcao.Alta
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    ''' <summary>
    ''' Valida exclusão das configurações
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 03/07/2013 Criado
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
    ''' Valida edição da configuração selecionada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [claudioniz.pereira] 09/07/2013 Criado
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
    ''' Chama a tela de alteração.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        Try
            'verifica se os dados na tela estão preenchidos corretamente
            If ValidaEditar() Then
                PopularControles()
                ExibirFormulario(True)
                MyBase.Acao = Enumeradores.eAcao.Modificacion
            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub gvReportes_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                'Seta as propriedades do radio button

                Dim rbSelecionado As HtmlInputCheckBox = CType(gvReportes.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionado"), HtmlInputCheckBox)
                rbSelecionado.Value = gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString()
                Dim jsScript As String = "javascript: AddRemovIdSelect2(this,'" & hdReportes.ClientID & "',false,''); "
                rbSelecionado.Attributes.Add("onclick", jsScript)

                Dim existe = (From p In reportesSelecionados
                       Where p.Equals(gvReportes.GetRowValues(e.VisibleIndex, gvReportes.KeyFieldName).ToString())
                       Select p).ToList()
                If existe IsNot Nothing AndAlso existe.Count() = 1 Then
                    rbSelecionado.Checked = True
                End If

                If DesativarRadioButtons Then
                    rbSelecionado.Attributes.Add("disabled", "disabled")
                Else
                    rbSelecionado.Attributes.Remove("disabled")
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub gvReportes_OnPageIndexChanged(sender As Object, e As EventArgs)
        Me.PreencherGridViewReportes(Me.getConfiguracionesReportes())
    End Sub

#Region "Metodos dos controles que eram da tela MantenimientoConfiguracionGeneral"
    ''' <summary>
    ''' Carrega os formatos de arquivos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopularFormatoArchivo()
        ddlFormatoSaida.Items.Clear()

        ddlFormatoSaida.Items.Add(New ListItem(Traduzir("gen_selecione"), "0"))

        ddlFormatoSaida.Items.Add(New ListItem With { _
                                   .Text = Traduzir("024_rbnCSV"), _
                                   .Value = Enumeradores.eFormatoArchivo.CSV})

        ddlFormatoSaida.Items.Add(New ListItem With { _
                                   .Text = Traduzir("024_rbnPDF"), _
                                   .Value = Enumeradores.eFormatoArchivo.PDF})

        ddlFormatoSaida.Items.Add(New ListItem With { _
                                   .Text = Traduzir("024_rbnEXCEL"), _
                                   .Value = Enumeradores.eFormatoArchivo.EXCEL})

        ddlFormatoSaida.Items.Add(New ListItem With { _
                           .Text = Traduzir("024_rbnEXCEL2010"), _
                           .Value = Enumeradores.eFormatoArchivo.EXCEL2010})

        ddlFormatoSaida.Items.Add(New ListItem With { _
                                   .Text = Traduzir("024_rbnHTML"), _
                                   .Value = Enumeradores.eFormatoArchivo.MHTML})
    End Sub

    ''' <summary>
    ''' Carrega os relatórios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopularRelatorios()
        ddlReporte.Items.Clear()

        ddlReporte.Items.Add(New ListItem(Traduzir("gen_selecione"), "0"))
        Dim objReport As New Prosegur.Genesis.Report.Gerar()
        objReport.Autenticar(False)

        Dim Relatorios = objReport.ListaCatalogItem(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))
        If Relatorios IsNot Nothing AndAlso Relatorios.Count > 0 Then
            For Each relatorio In Relatorios
                ' se for objeto relatório..
                If relatorio.TypeName = "Report" Then
                    ddlReporte.Items.Add(New ListItem(relatorio.Name, relatorio.Path))
                End If
            Next
        End If
    End Sub
    Private Sub PopularControles()

        Try
            If (reportesSelecionados.Count > 0) Then

                PopularFormatoArchivo()
                PopularRelatorios()

                Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral

                Dim peticion As New IAC.ContractoServicio.Configuracion.General.Peticion
                peticion.ConfiguracionGeneral = New IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral
                Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing

                peticion.ConfiguracionGeneral.OIDConfiguracionGeneral = reportesSelecionados.FirstOrDefault()
                respuesta = objProxy.GetConfiguracionGeneralReporte(peticion)
                If (Not respuesta Is Nothing AndAlso Not respuesta.ConfiguracionGeneral Is Nothing) Then
                    ddlReporte.ClearSelection()
                    Dim item = Me.ddlReporte.Items.FindByText(respuesta.ConfiguracionGeneral.DesReporte)
                    If Not item Is Nothing Then
                        item.Selected = True
                    End If

                    Me.txtIDReporte.Text = respuesta.ConfiguracionGeneral.CodReporte
                    Me.ddlFormatoSaida.SelectedValue = respuesta.ConfiguracionGeneral.FormatoArchivo
                    Me.ddlFormatoSaida_SelectedIndexChanged(Nothing, Nothing)
                    Me.txtExtensaoArquivo.Text = respuesta.ConfiguracionGeneral.ExtensionArchivo
                    Me.txtSeparador.Text = respuesta.ConfiguracionGeneral.Separador
                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagem(ex.Message)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Protected Sub ddlFormatoSaida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFormatoSaida.SelectedIndexChanged
        If Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.CSV Then
            Me.txtExtensaoArquivo.Enabled = True
            Me.txtSeparador.Enabled = True
            Me.txtSeparador.Text = String.Empty
            Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_csv

        Else
            Me.txtExtensaoArquivo.Text = String.Empty
            Me.txtExtensaoArquivo.Enabled = False
            Me.txtSeparador.Text = String.Empty
            Me.txtSeparador.Enabled = False

            If Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.PDF Then
                Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_pdf

            ElseIf Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.EXCEL Then
                Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_xls

            ElseIf Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.EXCEL2010 Then
                Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_xlsx

            ElseIf Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.MHTML Then
                Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_mhtml
            End If
        End If
    End Sub

    Private Function ValidaNomeExtensao() As Boolean
        Dim erros As List(Of String) = New List(Of String)
        Dim errosValidacao As List(Of String) = New List(Of String)
        Dim objReport As New Prosegur.Genesis.Report.Gerar()
        objReport.Autenticar(False)

        'Validar nome do arquivo
        If Me.txtExtensaoArquivo.Enabled AndAlso txtExtensaoArquivo.Text.Trim.ToUpper <> "CSV" Then
            errosValidacao = objReport.ValidarNomeExtensao(ddlReporte.SelectedValue, Me.txtExtensaoArquivo.Text, Me.lblExtensaoArquivo.Text)
            For Each item In errosValidacao
                erros.Add(item)
            Next
        End If

        If erros IsNot Nothing AndAlso erros.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
            Return False
        End If

        Return True

    End Function
    Dim erro As List(Of String) = New List(Of String)

    Private Function ValidaControles() As Boolean
        Dim erros As List(Of String) = New List(Of String)

        If Me.ddlReporte.SelectedIndex < 1 Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblReporte.Text))
        End If

        If String.IsNullOrEmpty(Me.txtIDReporte.Text) Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblIDReporte.Text))
        End If

        If Me.ddlFormatoSaida.SelectedIndex < 1 Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblFormatoSaida.Text))
        End If

        If Me.txtExtensaoArquivo.Enabled Then
            If String.IsNullOrEmpty(Me.txtExtensaoArquivo.Text) Then
                erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblExtensaoArquivo.Text))
            End If

            If String.IsNullOrEmpty(Me.txtSeparador.Text) Then
                erros.Add(String.Format(Traduzir("027_separador_invalido"), Me.lblSeparador.Text))
            End If
        End If

        If erros IsNot Nothing AndAlso erros.Count > 0 Then
            MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
            Return False
        Else
            Return ValidaNomeExtensao()
        End If

        Return True

    End Function

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            If Me.ValidaControles() Then
                Dim configuracionGeneral As New IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral
                configuracionGeneral.DesReporte = Me.ddlReporte.SelectedItem.Text
                configuracionGeneral.CodReporte = Me.txtIDReporte.Text.Trim
                configuracionGeneral.ExtensionArchivo = Me.txtExtensaoArquivo.Text.Trim
                configuracionGeneral.FormatoArchivo = Convert.ToInt16(Me.ddlFormatoSaida.SelectedValue)
                configuracionGeneral.Separador = Me.txtSeparador.Text.Trim
                configuracionGeneral.CodUsuario = MyBase.LoginUsuario
                Dim peticion As New IAC.ContractoServicio.Configuracion.General.Peticion
                Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing
                Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral
                peticion.ConfiguracionGeneral = configuracionGeneral
                If MyBase.Acao = Enumeradores.eAcao.Modificacion Then
                    configuracionGeneral.OIDConfiguracionGeneral = reportesSelecionados.FirstOrDefault()
                    respuesta = objProxy.AtualizarConfiguracionGeneralReporte(peticion)
                Else
                    respuesta = objProxy.InserirConfiguracionGeneralReporte(peticion)
                End If

                If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    MyBase.MostraMensagem(respuesta.MensajeError)
                Else
                    MyBase.MostraMensagem(Traduzir("info_msg_grabadosucesso"))
                    btnCancelar_Click(sender, e)
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
#End Region

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            hdReportes.Value = String.Empty

            ExibirFormulario(False)
            txtIDReporte.Text = String.Empty
            txtExtensaoArquivo.Text = String.Empty
            txtSeparador.Text = String.Empty
            ddlFormatoSaida.SelectedIndex = 0
            ddlReporte.SelectedIndex = 0
            Acao = Enumeradores.eAcao.Inicial
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ExibirFormulario(desativar As Boolean)
        DesativarRadioButtons = desativar

        Me.PreencherGridViewReportes(Me.getConfiguracionesReportes())

        pnFormulario.Visible = desativar
        btnEditar.Enabled = Not desativar
        If desativar Then
            btnExcluir.Attributes.Add("disabled", "disabled")
            btnValidaEditar.Attributes.Add("disabled", "disabled")
        Else
            btnExcluir.Attributes.Remove("disabled")
            btnValidaEditar.Attributes.Remove("disabled")
        End If
        btnNovo.Enabled = Not desativar
    End Sub

    Private Sub btnChamarExcluir_Click(sender As Object, e As EventArgs) Handles btnChamarExcluir.Click
        Try
            If ValidaExcluir() Then
                Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral

                Dim peticion As New IAC.ContractoServicio.Configuracion.General.Peticion
                Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing

                Dim erros As New List(Of String)

                'modificado para chamar a exlusão por cada configuração marcada
                'fica melhor para tratar os erros
                For Each _report In reportesSelecionados
                    Dim list As New List(Of String)
                    list.Add(_report)
                    peticion.ListaOIDConfiguracionGeneral = list

                    respuesta = objProxy.ExcluirConfiguracionGeneralReporte(peticion)

                    If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                        erros.Add(respuesta.MensajeError)
                    End If

                Next

                If erros IsNot Nothing AndAlso erros.Count > 0 Then
                    MyBase.MostraMensagem(String.Join("<br>", erros.ToArray()))
                End If

                'Atualiza a o grid
                If erros.Count <> reportesSelecionados.Count Then
                    PreencherGridViewReportes(Me.getConfiguracionesReportes())
                    hdReportes.Value = String.Empty
                End If
            End If

        Catch ex As Excepcion.NegocioExcepcion
            MyBase.MostraMensagemExcecao(ex)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class