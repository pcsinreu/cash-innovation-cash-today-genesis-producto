Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones

Partial Public Class BusquedaParteDiferenciasMostrar
    Inherits Base

    Private Const CONST_DELIMITADOR_INICIAL As String = "$"
    Private Const CONST_DELIMITADOR_FINAL As String = "#"

#Region "[VARIÁVEIS]"

    Private DocumentosSelecionados As New Dictionary(Of String, String)

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Objeto parte de diferencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParteDiferencias() As List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias)
        Get
            Return Session("ParteDiferencias")
        End Get
        Set(value As List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.ParteDiferencias))
            Session("ParteDiferencias") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto parte de diferencias selecionado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ParteDiferenciasSelecionados() As List(Of String)
        Get
            If Session("ParteDiferenciasSelecionados") Is Nothing Then
                Session("ParteDiferenciasSelecionados") = New List(Of String)
            End If
            Return Session("ParteDiferenciasSelecionados")
        End Get
        Set(value As List(Of String))
            Session("ParteDiferenciasSelecionados") = value
        End Set
    End Property

    Public Property DataConteo() As DateTime
        Get
            If ViewState("DataConteo") Is Nothing Then
                ViewState("DataConteo") = New DateTime
            End If
            Return ViewState("DataConteo")
        End Get
        Set(value As DateTime)
            ViewState("DataConteo") = value
        End Set
    End Property

    ''' <summary>
    ''' Nome Documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NomeDocumento() As String
        Get
            Return Session("ParteDiferenciasNomeDocumento")
        End Get
        Set(value As String)
            Session("ParteDiferenciasNomeDocumento") = value
        End Set
    End Property

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub


    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.btnVisualizar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

        Me.btnVisualizar.TabIndex = 1

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.PARTE_DIFERENCIAS
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                ' setar foco
                btnVisualizar.Focus()

                ' trata o foco dos campos
                TrataFoco()

                ' carrega os datos da consulta
                CarregaDados()

            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("017_titulo_pagina")
        Me.lblTituloDocumentos.Text = Traduzir("017_titulo_pagina")
        Me.lblPrecinto.Text = Traduzir("017_lbl_precinto")
        Me.btnVisualizar.Text = Traduzir("016_btn_visualizar")
        Me.btnVisualizar.ToolTip = Traduzir("016_btn_visualizar")

        Me.gvParteDiferenciasDocumentos.Columns(0).Caption = Traduzir("017_col_fecha_creacion")
        Me.gvParteDiferenciasDocumentos.Columns(1).Caption = Traduzir("017_col_numero_documento")
        Me.gvParteDiferenciasDocumentos.Columns(2).Caption = Traduzir("017_col_doc_general")
        Me.gvParteDiferenciasDocumentos.Columns(3).Caption = Traduzir("017_col_doc_incidencia")
        Me.gvParteDiferenciasDocumentos.Columns(4).Caption = Traduzir("017_col_doc_justificacion")

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()
        Try
            'Dim divModal As String = Request.QueryString("divModal")
            'btnVoltar.OnClientClick = "window.parent.FecharModal('#" & divModal & "');"
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Preenche o grid de documentos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregaDados()

        ' Remove o delimitador Inicial
        Dim filtroSelecionado As String = ParteDiferenciasSelecionados.FirstOrDefault().Replace(CONST_DELIMITADOR_INICIAL, String.Empty)

        ' Recupera o código do Precinto
        Me.lblPrecintoNumero.Text = filtroSelecionado.Split(CONST_DELIMITADOR_FINAL)(0)

        DataConteo = filtroSelecionado.Split(CONST_DELIMITADOR_FINAL)(1)

        ' validar se a busca tem dados
        If ParteDiferenciasSelecionados IsNot Nothing AndAlso ParteDiferenciasSelecionados.Count > 0 AndAlso _
           ParteDiferencias IsNot Nothing AndAlso ParteDiferencias.Count > 0 AndAlso _
           ParteDiferencias.Exists(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo) Then

            Dim documentos As List(Of ContractoServ.ParteDiferencias.GetParteDiferencias.DatosDocumentos)

            documentos = ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo).DatosDocumentos

            Dim objDt As DataTable = Util.ConverterListParaDataTable(documentos)

            objDt.DefaultView.Sort = " NumeroDocumento ASC"

            ' carregar controle
            gvParteDiferenciasDocumentos.DataSource = objDt
            gvParteDiferenciasDocumentos.DataBind()

        End If

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Retorna os documentos selecionados no grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Function RetornaDocumentosSelecionados() As Boolean

        ' verifica se a consulta retornou dados
        If ParteDiferencias IsNot Nothing AndAlso ParteDiferencias.Count > 0 Then

            ' limpa os documentos previamente selecionados
            DocumentosSelecionados.Clear()

            Dim strSeparador As String() = {"|"}
            Dim lstValores As List(Of String) = hdHayDocumentos.Value.Split(strSeparador, StringSplitOptions.RemoveEmptyEntries).ToList()
            Dim lstValores2 As List(Of String) = lstValores.Clonar().Select(Function(x) x.Split("#")(0)).ToList()

            For Each item As String In lstValores2.Distinct()
                Dim id As String = item.Split("#")(0)
                If Not DocumentosSelecionados.ContainsKey(id) Then
                    DocumentosSelecionados.Add(id, "")
                End If

                Dim existeGeneral As Boolean = lstValores.Where(Function(x) x = id & "#G").Count > 0
                If existeGeneral Then
                    DocumentosSelecionados.Item(id) = "G"
                End If

                Dim existeIncidencia As Boolean = lstValores.Where(Function(x) x = id & "#I").Count > 0
                If existeIncidencia Then
                    DocumentosSelecionados.Item(id) += "I"
                End If

                Dim existeJustificacion As Boolean = lstValores.Where(Function(x) x = id & "#J").Count > 0
                If existeJustificacion Then
                    DocumentosSelecionados.Item(id) += "J"
                End If

            Next

            ' retorna true se foi selecionado pelo menos um precinto
            Return (DocumentosSelecionados.Count > 0)
        Else
            ' retorna false quando não houve retorno da consulta
            Return False
        End If

    End Function

    ''' <summary>
    ''' Gera os links (chamadas) dos documentos que deverão ser abertos.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GeraLinksDocumentos()

        Dim contador As Integer = 0
        Dim url As String = String.Empty

        For Each item As KeyValuePair(Of String, String) In DocumentosSelecionados

            ' verifica se deve gerar uma chamada para um documento do tipo GENERAL
            If item.Value.IndexOf("G") > -1 Then
                ' define a url que será chamada para apresentar o documento
                url = "BusquedaParteDiferenciasDocumento.aspx?tipo=G&nome=" & FormataNomeDocumento(item.Key, "G") & "&id=" & item.Key
                ' adiciona a chamada
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_documento_" & contador.ToString(), "AbrirPopupNova('" & url & "', 'script_popup_documento_" & contador.ToString() & "', 550, 788);", True)
                contador += 1
            End If
            ' verifica se deve gerar uma chamada para um documento do tipo INCIDENCIA
            If item.Value.IndexOf("I") > -1 Then
                ' define a url que será chamada para apresentar o documento
                url = "BusquedaParteDiferenciasDocumento.aspx?tipo=I&nome=" & FormataNomeDocumento(item.Key, "I") & "&id=" & item.Key
                ' adiciona a chamada
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_documento_" & contador.ToString(), "AbrirPopupNova('" & url & "', 'script_popup_documento_" & contador.ToString() & "', 550, 788);", True)
                contador += 1
            End If
            ' verifica se deve gerar uma chamada para um documento do tipo JUSTIFICATIVA
            If item.Value.IndexOf("J") > -1 Then
                ' define a url que será chamada para apresentar o documento
                url = "BusquedaParteDiferenciasDocumento.aspx?tipo=J&nome=" & FormataNomeDocumento(item.Key, "J") & "&id=" & item.Key
                ' adiciona a chamada
                scriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_documento_" & contador.ToString(), "AbrirPopupNova('" & url & "', 'script_popup_documento_" & contador.ToString() & "', 550, 788);", True)
                contador += 1
            End If

        Next

    End Sub

    ''' <summary>
    ''' Formata o nome do documento
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tipo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormataNomeDocumento(id As String, tipo As String)

        Dim nome As String = NomeDocumento

        ' substitui o tipo no nome em memória
        nome = nome.Replace("[TIPO]", If(tipo = "G", "PARTEDIFERENCIAS", tipo))

        If ParteDiferencias IsNot Nothing AndAlso ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo) IsNot Nothing Then

            ' substitui o número do documento
            nome = nome.Replace("[NUMERO_DOCUMENTO]", If(ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo).DatosDocumentos.First(Function(d) d.ID = id) IsNot Nothing, ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo).DatosDocumentos.First(Function(d) d.ID = id).NumeroDocumento, "0"))

            ' se encontrar "DATA_TRASNPORTE", substitui pelo data de transporte da remessa formatada em mmdd
            nome = nome.Replace("[DATA_TRANSPORTE]", If(Not ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo).FechaTransporte.Equals(DateTime.MinValue), ParteDiferencias.First(Function(pd) pd.PrecintoRemesa = Me.lblPrecintoNumero.Text AndAlso _
                                       pd.FechaConteo = DataConteo).FechaTransporte.ToString("mmdd"), "0000"))

        End If

        Return nome

    End Function

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"
    Protected Sub gvParteDiferenciasDocumentos_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                'Seta as propriedades do checkbox HayDocumentoGeneral
                Dim ckDocumentoGeneral As HtmlInputCheckBox = CType(gvParteDiferenciasDocumentos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionadoG"), HtmlInputCheckBox)
                Dim bolDocumentoGeneral As String = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, "HayDocumentoGeneral")
                ckDocumentoGeneral.Disabled = Not CType(bolDocumentoGeneral, Boolean)
                ckDocumentoGeneral.Value = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, gvParteDiferenciasDocumentos.KeyFieldName).ToString() & "#G"
                Dim jsScript1 As String = "javascript: AddRemovIdSelect2(this,'" & hdHayDocumentos.ClientID & "',false,'');"
                ckDocumentoGeneral.Attributes.Add("onclick", jsScript1)

                'Seta as propriedades do checkbox HayDocumentoIncidencia
                Dim ckDocumentoIncidencia As HtmlInputCheckBox = CType(gvParteDiferenciasDocumentos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionadoI"), HtmlInputCheckBox)
                Dim bolDocumentoIncidencia As String = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, "HayDocumentoIncidencia")
                ckDocumentoIncidencia.Disabled = Not CType(bolDocumentoIncidencia, Boolean)
                ckDocumentoIncidencia.Value = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, gvParteDiferenciasDocumentos.KeyFieldName).ToString() & "#I"
                Dim jsScript2 As String = "javascript: AddRemovIdSelect2(this,'" & hdHayDocumentos.ClientID & "',false,'');"
                ckDocumentoIncidencia.Attributes.Add("onclick", jsScript2)

                'Seta as propriedades do checkbox HayDocumentoJustificacion
                Dim ckDocumentoJustificacion As HtmlInputCheckBox = CType(gvParteDiferenciasDocumentos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "ckSelecionadoJ"), HtmlInputCheckBox)
                Dim bolDocumentoJustificacion As String = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, "HayDocumentoJustificacion")
                ckDocumentoJustificacion.Disabled = Not CType(bolDocumentoJustificacion, Boolean)
                ckDocumentoJustificacion.Value = gvParteDiferenciasDocumentos.GetRowValues(e.VisibleIndex, gvParteDiferenciasDocumentos.KeyFieldName).ToString() & "#J"
                Dim jsScript As String = "javascript: AddRemovIdSelect2(this,'" & hdHayDocumentos.ClientID & "',false,'');"
                ckDocumentoJustificacion.Attributes.Add("onclick", jsScript)

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnVisualizar_Click(sender As Object, e As System.EventArgs) Handles btnVisualizar.Click

        Try

            ' verifica se foram selecionados documentos (e preenche o objeto em memória caso tenham sido selecionados)
            If RetornaDocumentosSelecionados() Then

                ' vera os links (chamadas) dos documentos que deverão ser abertos
                GeraLinksDocumentos()

            Else

                ' define a mensagem de documentos não selecionados
                MostraMensagem(Traduzir("017_msg_documento_obligatorio") & Constantes.LineBreak)
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region


End Class