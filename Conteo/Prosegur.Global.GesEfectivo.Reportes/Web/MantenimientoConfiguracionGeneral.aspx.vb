Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis

Public Class MantenimientoConfiguracionGeneral
    Inherits Base

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            PopularFormatoArchivo()
            PopularRelatorios()
            If MyBase.Acao = Enumeradores.eAcao.Modificacion Then
                Me.ddlReporte.Enabled = False
                PopularControles()
            End If
        End If
    End Sub

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

    ''' <summary>
    ''' Carrega os relatórios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopularControles()

        Try
            If (Not Request.QueryString("OID_Configuracion") Is Nothing) Then
                Dim objProxy As New Comunicacion.ProxyConfiguracionGeneral

                Dim peticion As New IAC.ContractoServicio.Configuracion.General.Peticion
                peticion.ConfiguracionGeneral = New IAC.ContractoServicio.Configuracion.General.ConfiguracionGeneral
                Dim respuesta As IAC.ContractoServicio.Configuracion.General.Respuesta = Nothing

                peticion.ConfiguracionGeneral.OIDConfiguracionGeneral = Request.QueryString("OID_Configuracion")
                respuesta = objProxy.GetConfiguracionGeneralReporte(peticion)
                If (Not respuesta Is Nothing AndAlso Not respuesta.ConfiguracionGeneral Is Nothing) Then
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
            Master.ControleErro.ShowError(ex.Descricao, Enumeradores.eMensagem.Atencao)
        Catch ex As Exception
            Master.ControleErro.ShowError(ex.Message, Enumeradores.eMensagem.Erro)
        End Try

    End Sub


    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 20/07/2009 Criado
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

    Protected Sub ddlFormatoSaida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFormatoSaida.SelectedIndexChanged
        If Me.ddlFormatoSaida.SelectedValue = Enumeradores.eFormatoArchivo.CSV Then
            Me.txtExtensaoArquivo.Enabled = True
            Me.txtSeparador.Enabled = True
            Me.txtSeparador.Text = String.Empty
            Me.txtExtensaoArquivo.Text = Prosegur.Genesis.Report.Constantes.extensao_csv
            Me.rfvExtensaoArquivo.Enabled = True
            Me.rfvSeparador.Enabled = True
        Else
            Me.txtExtensaoArquivo.Text = String.Empty
            Me.txtExtensaoArquivo.Enabled = False
            Me.txtSeparador.Text = String.Empty
            Me.txtSeparador.Enabled = False
            Me.rfvExtensaoArquivo.Enabled = False
            Me.rfvSeparador.Enabled = False

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

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()
        btnSalvar.Titulo = "btnGrabar"
        btnVoltar.Titulo = "btnVolver"
        Me.lblTituloPagina.Text = Traduzir("027_titulo_pagina")
        Me.lblReporte.Text = Traduzir("027_lbl_Reporte")
        Me.lblIDReporte.Text = Traduzir("027_lbl_IDReporte")
        Me.lblFormatoSaida.Text = Traduzir("027_lbl_FormatoArchivo")
        Me.lblExtensaoArquivo.Text = Traduzir("027_lbl_ExtensionArchivo")
        Me.lblSeparador.Text = Traduzir("027_lbl_Separador")
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
            Master.ControleErro.ShowError(erros, Enumeradores.eMensagem.Atencao)
            Return False
        End If

        Return True

    End Function
    Dim erro As List(Of String) = New List(Of String)

    Private Function ValidaControles() As Boolean
        Dim erros As List(Of String) = New List(Of String)

        If Me.ddlReporte.SelectedIndex < 1 Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblReporte.Text))
            Me.rfvReporte.IsValid = False
        End If

        If String.IsNullOrEmpty(Me.txtIDReporte.Text) Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblIDReporte.Text))
            Me.rfvIDReporte.IsValid = False
        End If

        If Me.ddlFormatoSaida.SelectedIndex < 1 Then
            erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblFormatoSaida.Text))
            Me.rfvFormatoSaida.IsValid = False
        End If

        If Me.txtExtensaoArquivo.Enabled Then
            If String.IsNullOrEmpty(Me.txtExtensaoArquivo.Text) Then
                erros.Add(String.Format(Traduzir("027_campoObrigatorio"), Me.lblExtensaoArquivo.Text))
                Me.rfvExtensaoArquivo.IsValid = False
            End If

            If String.IsNullOrEmpty(Me.txtSeparador.Text) Then
                erros.Add(String.Format(Traduzir("027_separador_invalido"), Me.lblSeparador.Text))
                Me.rfvSeparador.IsValid = False
            End If
        End If

        If erros IsNot Nothing AndAlso erros.Count > 0 Then
            Master.ControleErro.ShowError(erros, Enumeradores.eMensagem.Atencao)
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
                    configuracionGeneral.OIDConfiguracionGeneral = Request.QueryString("OID_Configuracion")
                    respuesta = objProxy.AtualizarConfiguracionGeneralReporte(peticion)
                Else
                    respuesta = objProxy.InserirConfiguracionGeneralReporte(peticion)
                End If

                If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                    Master.ControleErro.ShowError(respuesta.MensajeError, Enumeradores.eMensagem.Atencao, False)
                Else
                    Page.Response.Redireccionar("configuracionGeneral.aspx")
                End If

            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub btnVoltar_Click(sender As Object, e As EventArgs) Handles btnVoltar.Click
        Try

            Dim url As String = "ConfiguracionGeneral.aspx"

            Page.Response.Redireccionar(url)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub
End Class