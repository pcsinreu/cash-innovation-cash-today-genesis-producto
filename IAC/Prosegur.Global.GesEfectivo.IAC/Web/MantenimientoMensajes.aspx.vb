Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Clases

Public Class MantenimientoMensajes
    Inherits Base

#Region "[Propiedades]"
    Private Property MensajeEditar() As MensajeDePlanificacion

        Get
            Return ViewState("VSMensajeEditar")
        End Get

        Set(value As MensajeDePlanificacion)
            ViewState("VSMensajeEditar") = value
        End Set

    End Property

    Private Property MensajeEditarClone() As MensajeDePlanificacion

        Get
            Return ViewState("MensajeEditarClone")
        End Get

        Set(value As MensajeDePlanificacion)
            ViewState("MensajeEditarClone") = value
        End Set

    End Property
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

    Public Property ViewStatePlanificacionEntrada() As Planificacion
        Get
            Return Session("Planificacion")
        End Get
        Set(value As Planificacion)
            Session("Planificacion") = value
        End Set
    End Property

    Private Property Mensajes() As List(Of MensajeDePlanificacion)

        Get
            Return ViewStatePlanificacionEntrada.Mensajes
        End Get

        Set(value As List(Of MensajeDePlanificacion))
            ViewStatePlanificacionEntrada.Mensajes = value
        End Set

    End Property

#End Region
#Region "[Overrides]"

    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MENSAJES
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")
        MyBase.CodFuncionalidad = "MENSAJES_PLANIFICACION"
    End Sub

#End Region
    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then
                Dim Mensajes = ViewStatePlanificacionEntrada.Mensajes

                If (ViewStatePlanificacionEntrada Is Nothing) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                '' Preenche grid de codigos ajenos
                BuscarMensajes()

                LimparCampos()

                PreencherdllTipoMensaje()

                PreencherdllTipoPeriodo()
            End If
            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()
            txtCodMensaje.Focus()
        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            txtDesMensaje.Attributes.Add("style", "margin-left:2px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Public Sub ControleBotoes()

        MyBase.Acao = Request.QueryString("acao")

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnEliminar.Visible = False
            Case Aplicacao.Util.Utilidad.eAcao.Baja
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                setContultar()

            Case Aplicacao.Util.Utilidad.eAcao.Busca

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnEliminar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                btnEliminar.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                txtCodMensaje.Focus()

        End Select

    End Sub

    Protected Sub cbxSinReintentos_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As System.Web.UI.WebControls.CheckBox
        chk = DirectCast(sender, System.Web.UI.WebControls.CheckBox)
        Dim row As GridViewRow = DirectCast(chk.Parent.Parent, GridViewRow)
        Dim codigoMensaje As String = row.Cells(2).Text
        For Each item In Mensajes
            If item.Codigo = codigoMensaje Then
                item.SinReintentos = chk.Checked

            End If

        Next
        PreencherGridMensajes()

    End Sub

    Private Sub eliminarRegistroDeLaGrilla()
        If IsPostBack Then
            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            'MyBase.ExibirMensagemNaoSim("Error", accionSI, "Accion no")
            Dim mensaje As String = MyBase.RecuperarValorDic("msgEliminaMensaje")
            MyBase.ExibirMensagemSimNao(mensaje, accionSI)

        End If

    End Sub

    Private Sub btnAlertaSi_Click(sender As Object, e As EventArgs) Handles btnAlertaSi.Click
        Dim codigoMensaje As String = If(MensajeEditar IsNot Nothing, MensajeEditar.Codigo, "")
        Dim tipoPeriodo As String = If(MensajeEditar IsNot Nothing, MensajeEditar.DesTipoPeriodo, "")
        'removido = Me.CodigosAjenos.FirstOrDefault(Function(f) f.CodAjeno = CodigoAjenosEditarClone.CodAjeno AndAlso f.CodIdentificador = CodigoAjenosEditarClone.CodIdentificador)
        If Not String.IsNullOrEmpty(codigoMensaje) Then
            Dim mensajeEliminado = Mensajes.FirstOrDefault(Function(f) f.Codigo = codigoMensaje And f.DesTipoPeriodo = tipoPeriodo)
            Mensajes.Remove(mensajeEliminado)
            MensajeEditarClone = Nothing

            LimparCampos()

            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
            'Recarrega grid com dados da viewstate
            PreencherGridMensajes()
        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        eliminarRegistroDeLaGrilla()
    End Sub

    Protected Sub imbBorrar_Click(sender As Object, e As ImageClickEventArgs)
        eliminarRegistroDeLaGrilla()
    End Sub

    Protected Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click

        Try

            If btnAnadir.Text = Traduzir("btnAnadir") Then
                If Mensajes Is Nothing Then
                    Mensajes = New List(Of MensajeDePlanificacion)
                End If
                If Not Mensajes.Any(Function(a) a.Codigo = txtCodMensaje.Text And a.TipoPeriodo = ddlTipoPeriodo.SelectedValue) And Not String.IsNullOrEmpty(txtCodMensaje.Text) Then
                    Dim objMensaje As New MensajeDePlanificacion
                    objMensaje.SinReintentos = False
                    objMensaje.Codigo = txtCodMensaje.Text
                    objMensaje.Descripcion = txtDesMensaje.Text
                    objMensaje.Tipo = ddlTipoMensaje.SelectedValue
                    objMensaje.TipoPeriodo = ddlTipoPeriodo.SelectedValue
                    objMensaje.DesTipoPeriodo = ddlTipoPeriodo.SelectedItem.Text
                    If Mensajes Is Nothing Then
                        Mensajes = New List(Of MensajeDePlanificacion)

                    End If

                    Mensajes.Add(objMensaje)

                    LimparCampos()

                    'Recarrega grid com dados da viewstate
                    PreencherGridMensajes()
                Else
                    MostraMensagemErroConScript(RecuperarValorDic("msgCodigoExistente"))
                End If
            ElseIf btnAnadir.Text = Traduzir("btnModificacion") Then

                MensajeEditar.Codigo = txtCodMensaje.Text
                MensajeEditar.Descripcion = txtDesMensaje.Text
                MensajeEditar.Tipo = ddlTipoMensaje.SelectedValue
                MensajeEditar.TipoPeriodo = ddlTipoPeriodo.SelectedValue
                MensajeEditar.DesTipoPeriodo = ddlTipoPeriodo.SelectedItem.Text
                Dim removido = Mensajes.FirstOrDefault(Function(f) f.Codigo = MensajeEditarClone.Codigo And f.TipoPeriodo = MensajeEditarClone.TipoPeriodo)

                Dim indice As Integer = Mensajes.IndexOf(removido)
                If indice >= 0 Then
                    Mensajes.Remove(removido)
                End If

                Mensajes.Insert(indice, MensajeEditar)

                MensajeEditarClone = Nothing

                LimparCampos()

                MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta
                'Recarrega grid com dados da viewstate
                PreencherGridMensajes()

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub setContultar()

        btnAnadir.Enabled = False
        btnGrabar.Enabled = False
        btnLimpar.Enabled = False
        txtCodMensaje.Enabled = False
        txtDesMensaje.Enabled = False
        ddlTipoMensaje.Enabled = False
        ProsegurGridViewMensajes.Enabled = False

        btnAnadir.Visible = False
        btnGrabar.Visible = False
        btnLimpar.Visible = False

        trBotoesBusqueda.Visible = False

        ProsegurGridViewMensajes.Columns(0).Visible = False

    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

        Try

            ViewStatePlanificacionEntrada.Mensajes = Mensajes
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            LimparCampos()

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub BuscarMensajes()

        Try
            If String.IsNullOrEmpty(ViewStatePlanificacionEntrada.Identificador) Then
                Acao = Aplicacao.Util.Utilidad.eAcao.Alta
            Else
                ' Acao = Aplicacao.Util.Utilidad.eAcao.Busca
            End If
            ' obtém os registros na base e preenche o grid
            PreencherGridMensajes()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub


    Private Sub PreencherGridMensajes()
        ' validar se encontrou clientes
        If Mensajes IsNot Nothing AndAlso Mensajes.Count > 0 Then

            Dim objDt As DataTable = ProsegurGridViewMensajes.ConvertListToDataTable(Mensajes)

            ' carregar controle
            ProsegurGridViewMensajes.CarregaControle(objDt)
            pnlSemRegistro.Visible = False

        Else

            'Limpa a consulta
            ProsegurGridViewMensajes.DataSource = Nothing
            ProsegurGridViewMensajes.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True
        End If

    End Sub

    Private Sub TamanhoLinhas()
        ProsegurGridViewMensajes.RowStyle.Height = 20
    End Sub
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    Private Sub PreencherdllTipoMensaje()
        Dim lista = New List(Of String)
        lista.Add("OK")
        lista.Add("KO")
        lista.Add("EC")

        ddlTipoMensaje.AppendDataBoundItems = True
        ddlTipoMensaje.Items.Clear()
        ddlTipoMensaje.DataSource = lista.ToList()
        ddlTipoMensaje.DataBind()
    End Sub

    Private Sub PreencherdllTipoPeriodo()

        Dim objRespuesta As New ContractoServicio.Utilidad.GetComboTiposPeriodo.Respuesta
        Dim objAccionUtilidad As New LogicaNegocio.AccionUtilidad

        objRespuesta = objAccionUtilidad.GetComboTiposPeriodos()

        If objRespuesta.TiposDePeriodos.Count > 0 Then

            ddlTipoPeriodo.AppendDataBoundItems = True
            ddlTipoPeriodo.Items.Clear()
            ddlTipoPeriodo.DataTextField = "Descripcion"
            ddlTipoPeriodo.DataValueField = "Oid"
            ddlTipoPeriodo.DataSource = objRespuesta.TiposDePeriodos.ToList()
            ddlTipoPeriodo.DataBind()

        End If


    End Sub

    Protected Sub ProsegurGridViewMensajes_SelectedIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles ProsegurGridViewMensajes.SelectedIndexChanging
        Try
            Dim linhaSelecionada As Integer = e.NewSelectedIndex
            Dim gridrow = ProsegurGridViewMensajes.Rows(linhaSelecionada)
            Dim CodMensaje = gridrow.Cells(2).Text
            Dim DesMensaje = gridrow.Cells(3).Text
            Dim TipoMensaje = gridrow.Cells(4).Text
            Dim TipoPeriodo = gridrow.Cells(5).Text

            MensajeEditar = Mensajes.FirstOrDefault(Function(f) f.Codigo = CodMensaje And f.DesTipoPeriodo = TipoPeriodo)
            MensajeEditarClone = MensajeEditar

            txtCodMensaje.Text = MensajeEditar.Codigo
            txtDesMensaje.Text = MensajeEditar.Descripcion
            ddlTipoMensaje.SelectedValue = MensajeEditar.Tipo
            ddlTipoPeriodo.SelectedValue = MensajeEditar.TipoPeriodo
            btnAnadir.Text = Traduzir("btnModificacion")
            btnAnadir.ToolTip = Traduzir("btnModificacion")
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion


            MyBase.TraduzirControles()
            'PreencherGridCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = MyBase.RecuperarValorDic("lblTitulo")
        lblSubTitulo.Text = MyBase.RecuperarValorDic("lblTitulo")
        lblCodMensaje.Text = MyBase.RecuperarValorDic("lblCodMensaje")
        lblDesMensaje.Text = MyBase.RecuperarValorDic("lblDesMensaje")
        lblTipoMensaje.Text = MyBase.RecuperarValorDic("lblTipoMensaje")
        lblTipoPeriodo.Text = MyBase.RecuperarValorDic("lblTipoPeriodo")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnEliminar.Text = MyBase.RecuperarValorDic("btnEliminar")
        btnEliminar.ToolTip = MyBase.RecuperarValorDic("btnEliminar")


        'Grid
        ProsegurGridViewMensajes.Columns(0).HeaderText = Traduzir("034_lbl_gdr_modificar")
        ProsegurGridViewMensajes.Columns(1).HeaderText = MyBase.RecuperarValorDic("lblSinReintentos")
        ProsegurGridViewMensajes.Columns(2).HeaderText = MyBase.RecuperarValorDic("lblCodMensaje")
        ProsegurGridViewMensajes.Columns(3).HeaderText = MyBase.RecuperarValorDic("lblDesMensaje")
        ProsegurGridViewMensajes.Columns(4).HeaderText = MyBase.RecuperarValorDic("lblTipoMensaje")
    End Sub

    Private Sub LimparCampos()

        txtCodMensaje.Text = String.Empty
        txtDesMensaje.Text = String.Empty
        'Selecionados.Clear()
        'txtIdentificador.Focus()

        'MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        btnAnadir.Text = Traduzir("btnAnadir")
        btnAnadir.ToolTip = Traduzir("btnAnadir")
    End Sub

    Protected Sub ProsegurGridViewMensajes_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridViewMensajes.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ProsegurGridViewMensajes_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridViewMensajes.EPreencheDados

        Try

            Dim objDT As DataTable


            If Mensajes IsNot Nothing AndAlso Mensajes.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable(para efetuar a ordenação)
                objDT = ProsegurGridViewMensajes.ConvertListToDataTable(Mensajes)

                'If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                '    objDt.DefaultView.Sort = IIf(Not ColecaoClientes, " codigo ASC", " cliente ASC")
                'Else
                '    objDt.DefaultView.Sort = ProsegurGridViewCodigoAjeno.SortCommand
                'End If

                ProsegurGridViewMensajes.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridViewMensajes.DataSource = Nothing
                ProsegurGridViewMensajes.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewMensajes_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMensajes.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha
                'If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                '    e.Row.Cells(3).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                'End If

                Dim chk As System.Web.UI.WebControls.CheckBox

                chk = e.Row.Cells(1).Controls(1)
                chk.Checked = e.Row.DataItem("SinReintentos")



            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 29/04/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewMensajes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridViewMensajes.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    'Protected Sub ddlTipoMensaje_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    MensajeEditar.Tipo = ddlTipoMensaje.SelectedValue
    'End Sub
End Class