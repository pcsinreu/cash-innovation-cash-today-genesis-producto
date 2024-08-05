Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor

Public Class BusquedaTransaccionesPopup
    Inherits Base

#Region "[Propriedades]"

    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

    Private ReadOnly Property MAE() As String
        Get
            Return Request.QueryString("MAE").ToString()
        End Get
    End Property

#End Region

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        Try
            MyBase.AdicionarScripts()
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}');", txtFecha.ClientID, "False")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)

            txtFecha.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.EMBRANCO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [poncalves] 30/04/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 11/04/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()
        btnAceptar.Text = Traduzir("btnAceptar")
        btnCancelar.Text = Traduzir("btnCancelar")
        'Grid
        gvDatos.Columns(1).Caption = Traduzir("029_grid_CodigoTransaccion")
        gvDatos.Columns(2).Caption = Traduzir("029_grid_DescripcionFormulario")
        gvDatos.Columns(3).Caption = Traduzir("029_grid_FechaGestion")
        gvDatos.Columns(4).Caption = Traduzir("029_grid_CodigoPuntoServicio")
        gvDatos.Columns(5).Caption = Traduzir("029_grid_DescripcionPuntoServicio")
        gvDatos.Columns(6).Caption = Traduzir("029_grid_MAE")
        gvDatos.Columns(7).Caption = Traduzir("029_grid_CodigoIsoDivisa")
        gvDatos.Columns(8).Caption = Traduzir("029_grid_Importe")
        gvDatos.SettingsText.EmptyDataRow = Traduzir("info_msg_sin_registro")
        'filtros
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        lblFecha.Text = Traduzir("029_grid_FechaGestion")

        csvFecha.ErrorMessage = MyBase.RecuperarValorDic("msg_fyh_ini_obrigatorio")

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        gvDatos.SettingsBehavior.ColumnResizeMode = DevExpress.Web.ASPxClasses.ColumnResizeMode.Control

        If Not Me.IsPostBack Then
            txtFecha.Text = DateTime.Now.ToShortDateString 'ToString("dd/MM/yyyy")
            btnBuscar_Click(vbNull, New EventArgs)
        End If
    End Sub

    Protected Sub gvDatos_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        ' Preenche linhas do Grid.
        If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then
            Dim rbSelecionado As HtmlInputRadioButton = CType(gvDatos.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "rbSelecionado"), HtmlInputRadioButton)

            If gvDatos.GetRowValues(e.VisibleIndex, "CodigoFormulario") = "MAESOC" Then
                rbSelecionado.Visible = False
            Else
                rbSelecionado.Value = gvDatos.GetRowValues(e.VisibleIndex, gvDatos.KeyFieldName).ToString().Trim()
                Dim jsScript As String = "javascript: $('.radio_selecao').not(this).each(function () { this.checked = false; }); AddRemovIdSelect(this,'" & hdnSelecionado.ClientID & "',true,''); "
                rbSelecionado.Attributes.Add("onclick", jsScript)
            End If

        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Session("FechaGestion") = Me.hdnSelecionado.Value.Replace("|", String.Empty)

            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaTransaccionesPopup", jsScript, True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Try
            Dim jsScript As String = "window.parent.FecharModal(" & Chr(34) & "#" & divModal & Chr(34) & "," & Chr(34) & "#" & ifrModal & Chr(34) & ",null);"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaTransaccionesPopup", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        Try

            gvDatos.DataSource = Nothing
            gvDatos.DataBind()

            txtFecha.Text = DateTime.Now.ToShortDateString

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Dim fechaBusqueda As Date



            If String.IsNullOrEmpty(txtFecha.Text) Then fechaBusqueda = DateTime.Now.ToShortDateString Else fechaBusqueda = Date.Parse(txtFecha.Text)


            Dim lista = Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Documento.RecuperarDocumentosMAE(MyBase.DelegacionLogada.Codigo, MAE, fechaBusqueda)
            Me.gvDatos.PageIndex = 0
            Me.gvDatos.DataSource = lista
            Me.gvDatos.DataBind()
            Me.gvDatos.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class