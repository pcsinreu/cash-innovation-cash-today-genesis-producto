Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Busca de clientes
''' </summary>
''' <remarks></remarks>
''' <history>
''' [carlos.bomtempo] 19/05/2009 Criado
''' </history>
Partial Public Class BusquedaCaracteristicas
    Inherits Base

    Private Property MostrarMenuRodape() As Boolean
        Get
            If ViewState("MenuRodapeAtivo") Is Nothing Then
                Return False
            End If
            Return CType(ViewState("MenuRodapeAtivo"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("MenuRodapeAtivo") = value
        End Set
    End Property

#Region "[PROPRIEDADES]"

    Private Property Caracteristica() As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica
        Get
            Return DirectCast(ViewState("Caracteristica"), IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica)
        End Get
        Set(value As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica)
            ViewState("Caracteristica") = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property CodigoConteoExistente() As Boolean
        Get
            Return ViewState("CodigoConteoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoConteoExistente") = value
        End Set
    End Property

#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        Try

            Dim s As String = String.Empty

            'Aciona o botão buscar quando precionado enter.
            txtCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
            txtCodigoConteo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
            txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")
            chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ID & "_img', event);")


            'Adiciona a Precedencia ao Buscar
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnBuscar.ClientID & "';", True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        txtCodigoConteo.TabIndex = 3
        chkVigente.TabIndex = 4
        btnBuscar.TabIndex = 5
        btnLimpar.TabIndex = 6
        ProsegurGridView.TabIndex = 7

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS
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
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True

            If Not Page.IsPostBack Then

                ' setar foco no campo codigo
                txtCodigo.Focus()
                ExecutarBusca()
            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("020_titulo_pagina")
        Master.Titulo = Traduzir("020_titulo_pagina")
        lblCodigo.Text = Traduzir("020_codigo_caracteristica")
        lblDescricao.Text = Traduzir("020_descricao_caracteristica")
        lblCodigoConteo.Text = Traduzir("020_codigo_conteo")
        lblVigente.Text = Traduzir("020_caracteristica_vigente")
        lblTituloCaracteristicas.Text = Traduzir("020_titulo_busqueda")
        lblSubTitulosClientes.Text = Traduzir("020_titulo_resultado")

        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnSalvar.Text = Traduzir("btnGrabar")
        btnCancelar.Text = Traduzir("btnCancelar")

        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnSalvar.ToolTip = Traduzir("btnGrabar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")

        'Formulario
        lblTituloForm.Text = Traduzir("020_lbl_subtitulo_mantenimiento_caracteristicas")
        lblCodigoCaracteristica.Text = Traduzir("020_lbl_codigo_caracteristica")
        lblDescricaoForm.Text = Traduzir("020_lbl_descricao_caracteristica")
        lblCodigoconteoForm.Text = Traduzir("020_lbl_codigo_conteo")
        lblObservaciones.Text = Traduzir("020_lbl_observaciones")
        lblVigenteForm.Text = Traduzir("020_lbl_vigente")

        csvCodigoCaracteristicaObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_obligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_descripcion_obligatorio")
        csvCodigoConteoObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_conteo_obligatorio")
        csvCodigoCaracteristica.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_existente")
        csvDescripcion.ErrorMessage = Traduzir("020_msg_caracteristica_descripcion_existente")
        csvCodigoConteo.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_conteo_existente")

        'Grid
        ProsegurGridView.Columns(1).HeaderText = Traduzir("020_lbl_gdr_codigo")
        ProsegurGridView.Columns(2).HeaderText = Traduzir("020_lbl_gdr_descripcion")
        ProsegurGridView.Columns(3).HeaderText = Traduzir("020_lbl_gdr_observaciones")
        ProsegurGridView.Columns(4).HeaderText = Traduzir("020_lbl_gdr_codigo_conteo")
        ProsegurGridView.Columns(5).HeaderText = Traduzir("020_lbl_gdr_vigente")
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena as caracteristicas encontradas na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Property Caracteristicas() As ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion
        Get
            If ViewState("VSCaracteristicas") Is Nothing Then
                ViewState("VSCaracteristicas") = New ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion()
            End If
            Return ViewState("VSCaracteristicas")
        End Get
        Set(value As ContractoServicio.Caracteristica.GetCaracteristica.CaracteristicaColeccion)
            ViewState("VSCaracteristicas") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        Try

            Dim objDT As DataTable
            Dim objRespuesta As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

            ' obter as caracteristicas
            objRespuesta = GetCaracteristicas()

            If objRespuesta.Caracteristicas IsNot Nothing _
                AndAlso objRespuesta.Caracteristicas.Count > 0 Then

                pnlSemRegistro.Visible = False

                ' converter objeto para datatable
                objDT = ProsegurGridView.ConvertListToDataTable(objRespuesta.Caracteristicas)

                If ProsegurGridView.SortCommand = String.Empty Then
                    objDT.DefaultView.Sort = " codigo asc"
                Else
                    objDT.DefaultView.Sort = ProsegurGridView.SortCommand
                End If

                ProsegurGridView.ControleDataSource = objDT

            Else

                'Limpa a consulta
                ProsegurGridView.DataSource = Nothing
                ProsegurGridView.DataBind()

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
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView.EPager_SetCss
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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 17
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then


                'Índice das celulas do GridView Configuradas
                '0 - Código
                '1 - Descripción
                '2 - Observaciones
                '3 - CodigoConteo
                '4 - Vigente

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.Cells(1).Text) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")

                If Not e.Row.DataItem("Descripcion") Is DBNull.Value AndAlso e.Row.DataItem("Descripcion").Length > NumeroMaximoLinha Then
                    e.Row.Cells(2).Text = e.Row.DataItem("Descripcion").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If Not e.Row.DataItem("Observaciones") Is DBNull.Value AndAlso e.Row.DataItem("Observaciones").Length > NumeroMaximoLinha Then
                    e.Row.Cells(3).Text = e.Row.DataItem("Observaciones").ToString.Substring(0, NumeroMaximoLinha) & " ..."
                End If

                If CBool(e.Row.DataItem("vigente")) Then
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(4).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If

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
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("020_lbl_gdr_codigo")
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8

                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("020_lbl_gdr_descripcion")
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 9
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 10

                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("020_lbl_gdr_observaciones")
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 11
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 12

                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("020_lbl_gdr_codigo_conteo")
                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 13
                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 14

                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("020_lbl_gdr_vigente")
                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 15
                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 16

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            ExecutarBusca()

            pnForm.Visible = False
            btnCancelar.Enabled = False
            btnSalvar.Enabled = False
            Master.MenuRodapeVisivel = False

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Private Sub ExecutarBusca()
        Acao = Aplicacao.Util.Utilidad.eAcao.Busca

        ' obtém os registros na base e preenche o grid
        PreencherGridCaracteristicas()
        
    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
            pnForm.Visible = False
            Master.MenuRodapeVisivel = False
            Caracteristica = Nothing

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "ManterFiltroAberto", "ManterFiltroAberto();", True)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' Controla o estado dos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

                ProsegurGridView.DataSource = Nothing
                ProsegurGridView.DataBind()

                txtCodigo.Text = String.Empty
                txtDescricao.Text = String.Empty
                txtCodigoConteo.Text = String.Empty
                chkVigente.Checked = True

                txtCodigo.Focus()

            Case Aplicacao.Util.Utilidad.eAcao.Busca


        End Select

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Obtém as caracteristicas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Function GetCaracteristicas() As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Caracteristica.GetCaracteristica.Peticion
        objPeticion.Vigente = chkVigente.Checked

        If Not String.IsNullOrEmpty(txtCodigo.Text.Trim) Then
            objPeticion.Codigo = txtCodigo.Text.Trim.ToUpper
        End If

        If Not String.IsNullOrEmpty(txtDescricao.Text.Trim) Then
            objPeticion.Descripcion = txtDescricao.Text.Trim.ToUpper
        End If

        If Not String.IsNullOrEmpty(txtCodigoConteo.Text.Trim) Then
            objPeticion.CodigoConteo = txtCodigoConteo.Text.Trim.ToUpper
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyCaracteristica

        ' chamar servicio
        Return objProxy.GetCaracteristica(objPeticion)

    End Function

    ''' <summary>
    ''' Preenche o grid de características
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 19/05/2009 Criado
    ''' </history>
    Private Sub PreencherGridCaracteristicas()

        ' obter clientes
        Dim objRespuesta As ContractoServicio.Caracteristica.GetCaracteristica.Respuesta = GetCaracteristicas()

        ' tratar retorno
        Dim msgErro As String = String.Empty
        If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, objRespuesta.MensajeError, msgErro, True, False) Then
            MyBase.MostraMensagem(msgErro)
            Exit Sub
        End If

        ' validar se encontrou caracteristicas
        If objRespuesta.Caracteristicas IsNot Nothing _
            AndAlso objRespuesta.Caracteristicas.Count > 0 Then

            'Verifica se a consulta não retornou mais registros que o permitido
            If objRespuesta.Caracteristicas.Count <= Aplicacao.Util.Utilidad.getMaximoRegistroGrid Then
                ' converter objeto para datatable
                Dim objDt As DataTable = ProsegurGridView.ConvertListToDataTable(objRespuesta.Caracteristicas)

                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " codigo ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If ProsegurGridView.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " Codigo ASC "
                    Else
                        objDt.DefaultView.Sort = ProsegurGridView.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = ProsegurGridView.SortCommand
                End If

                ' salvar objeto retornado do serviço
                Caracteristicas = objRespuesta.Caracteristicas

                ' carregar controle
                ProsegurGridView.CarregaControle(objDt)
                pnlSemRegistro.Visible = False

            Else


                'Limpa a consulta
                ProsegurGridView.DataSource = Nothing
                ProsegurGridView.DataBind()

                'Informar ao usuário sobre a não existencia de registro
                lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgMaxRegistro)
                pnlSemRegistro.Visible = True

                Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

            End If


        Else

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

#End Region

    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)

        Try
            If Not String.IsNullOrEmpty(ProsegurGridView.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                GetCaracteristicaByCodigo(Server.UrlDecode(codigo))
                pnForm.Visible = True
                pnForm.Enabled = True
                Master.MenuRodapeVisivel = True
                btnSalvar.Enabled = True
                btnCancelar.Enabled = True
                txtCodigoCaracteristica.Enabled = False
                txtCodigoConteoForm.Enabled = False
                Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            If Not String.IsNullOrEmpty(ProsegurGridView.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(ProsegurGridView.getValorLinhaSelecionada) Then
                    codigo = ProsegurGridView.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If
                GetCaracteristicaByCodigo(Server.UrlDecode(codigo))
                pnForm.Visible = True
                pnForm.Enabled = False
                Master.MenuRodapeVisivel = True
                btnSalvar.Enabled = False
                btnCancelar.Enabled = True
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub GetCaracteristicaByCodigo(codigo As String)

        Dim objProxyCaracteristica As New Comunicacion.ProxyCaracteristica
        Dim objPeticionCaracteristica As New IAC.ContractoServicio.Caracteristica.GetCaracteristica.Peticion
        Dim objRespuestaCaracteristica As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

        objPeticionCaracteristica.Codigo = codigo.ToUpper
        objRespuestaCaracteristica = objProxyCaracteristica.GetCaracteristica(objPeticionCaracteristica)

        If objRespuestaCaracteristica.Caracteristicas.Count > 0 Then
            Caracteristica = objRespuestaCaracteristica.Caracteristicas(0)
            txtCodigoCaracteristica.Text = objRespuestaCaracteristica.Caracteristicas(0).Codigo
            txtCodigoCaracteristica.ToolTip = objRespuestaCaracteristica.Caracteristicas(0).Codigo

            txtCodigoConteoForm.Text = objRespuestaCaracteristica.Caracteristicas(0).CodigoConteo
            txtCodigoConteoForm.ToolTip = objRespuestaCaracteristica.Caracteristicas(0).CodigoConteo

            txtDescricaoForm.Text = objRespuestaCaracteristica.Caracteristicas(0).Descripcion
            txtDescricaoForm.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuestaCaracteristica.Caracteristicas(0).Descripcion, String.Empty)

            txtObservaciones.Text = objRespuestaCaracteristica.Caracteristicas(0).Observaciones
            ckbVigenteForm.Checked = objRespuestaCaracteristica.Caracteristicas(0).Vigente
            If ckbVigenteForm.Checked Then
                ckbVigenteForm.Enabled = False
            End If
        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            pnForm.Visible = False
            pnForm.Enabled = False
            Master.MenuRodapeVisivel = False
            btnSalvar.Enabled = False
            btnCancelar.Enabled = False
            Caracteristica = Nothing
            ExecutarBusca()
            UpdatePanelGrid.Update()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


    Protected Sub txtCodigoCaracteristica_OnTextChanged(sender As Object, e As EventArgs)
        Try
            If ExisteCodigoCaracteristica(txtCodigoCaracteristica.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If
            Threading.Thread.Sleep(100)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub txtDescricaoForm_OnTextChanged(sender As Object, e As EventArgs)
        Try
            If ExisteDescricaoCaracteristica(txtDescricaoForm.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If
            Threading.Thread.Sleep(100)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub txtCodigoConteoForm_OnTextChanged(sender As Object, e As EventArgs)
        Try
            If ExisteCodigoConteoCaracteristica(txtCodigoConteoForm.Text) Then
                CodigoConteoExistente = True
            Else
                CodigoConteoExistente = False
            End If
            Threading.Thread.Sleep(100)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Function ExisteCodigoCaracteristica(codigoCaracteristica As String) As Boolean

        Dim objRespostaVerificarCodigoCaracteristica As IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta
        Try
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If codigoCaracteristica.Trim.Equals(Caracteristica.Codigo) Then
                    CodigoExistente = False
                    Return False
                End If
            End If

            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad()
            Dim objPeticionVerificarCodigoCaracteristica As New IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion

            'Verifica se o código da caracteristica existe no BD
            objPeticionVerificarCodigoCaracteristica.Codigo = codigoCaracteristica
            objRespostaVerificarCodigoCaracteristica = objProxyUtilidad.VerificarCodigoCaracteristica(objPeticionVerificarCodigoCaracteristica)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoCaracteristica.CodigoError, objRespostaVerificarCodigoCaracteristica.NombreServidorBD, objRespostaVerificarCodigoCaracteristica.MensajeError) Then
                Return objRespostaVerificarCodigoCaracteristica.Existe
            Else
                MyBase.MostraMensagem(objRespostaVerificarCodigoCaracteristica.MensajeError)
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function
    Private Function ExisteCodigoConteoCaracteristica(codigo As String) As Boolean

        Dim objRespostaVerificarCodigoConteo As IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta
        Try
            If Not String.IsNullOrEmpty(codigo) Then

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    If codigo.Trim.Equals(Caracteristica.CodigoConteo) Then
                        CodigoConteoExistente = False
                        Return False
                    End If
                End If

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarCodigoConteo As New IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion
                Dim strCodigoConteo As String = txtCodigoConteo.Text.Trim

                'Verifica se a descrição existe no BD
                objPeticionVerificarCodigoConteo.Codigo = codigo
                objRespostaVerificarCodigoConteo = objProxyUtilidad.VerificarCodigoConteoCaracteristica(objPeticionVerificarCodigoConteo)

                If Master.ControleErro.VerificaErro2(objRespostaVerificarCodigoConteo.CodigoError, objRespostaVerificarCodigoConteo.NombreServidorBD, objRespostaVerificarCodigoConteo.MensajeError) Then
                    Return objRespostaVerificarCodigoConteo.Existe
                Else
                    MyBase.MostraMensagem(objRespostaVerificarCodigoConteo.MensajeError)
                    Return False
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Private Function ExisteDescricaoCaracteristica(descricao As String) As Boolean

        Dim objRespostaVerificarDescripcionCaracteristica As IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta
        Try
            If Not String.IsNullOrEmpty(descricao.Trim) Then
                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    If descricao.Trim.Equals(Caracteristica.Descripcion) Then
                        DescricaoExistente = False
                        Return False
                    End If
                End If

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarDescripcionCaracteristica As New IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion

                'Verifica se a descrição existe no BD
                objPeticionVerificarDescripcionCaracteristica.Descripcion = descricao.Trim
                objRespostaVerificarDescripcionCaracteristica = objProxyUtilidad.VerificarDescripcionCaracteristica(objPeticionVerificarDescripcionCaracteristica)


                If Master.ControleErro.VerificaErro(objRespostaVerificarDescripcionCaracteristica.CodigoError, objRespostaVerificarDescripcionCaracteristica.NombreServidorBD, objRespostaVerificarDescripcionCaracteristica.MensajeError) Then
                    Return objRespostaVerificarDescripcionCaracteristica.Existe
                Else
                    MyBase.MostraMensagem(objRespostaVerificarDescripcionCaracteristica.MensajeError)
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Sub ExecutarGrabar()
        Try

            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro(True)
            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objCaracteristica As New IAC.ContractoServicio.Caracteristica.SetCaracteristica.Caracteristica

            objCaracteristica.Codigo = txtCodigoCaracteristica.Text.Trim
            objCaracteristica.Descripcion = txtDescricaoForm.Text.Trim
            objCaracteristica.CodigoConteo = txtCodigoConteoForm.Text.Trim
            objCaracteristica.Observaciones = txtObservaciones.Text

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objCaracteristica.Vigente = True
            Else
                objCaracteristica.Vigente = ckbVigenteForm.Checked
            End If

            Dim ProxyCaractaristica As New Comunicacion.ProxyCaracteristica()
            Dim PeticionCaractaristica As New IAC.ContractoServicio.Caracteristica.SetCaracteristica.Peticion()
            Dim RespuestaCaractaristica As IAC.ContractoServicio.Caracteristica.SetCaracteristica.Respuesta

            PeticionCaractaristica.Caracteristicas = New IAC.ContractoServicio.Caracteristica.SetCaracteristica.CaracteristicaColeccion()
            PeticionCaractaristica.Caracteristicas.Add(objCaracteristica)
            PeticionCaractaristica.CodigoUsuario = MyBase.LoginUsuario

            RespuestaCaractaristica = ProxyCaractaristica.SetCaracteristica(PeticionCaractaristica)

            If Master.ControleErro.VerificaErro(RespuestaCaractaristica.CodigoError, RespuestaCaractaristica.NombreServidorBD, RespuestaCaractaristica.MensajeError) AndAlso _
                Master.ControleErro.VerificaErro(RespuestaCaractaristica.Caracteristicas(0).CodigoError, RespuestaCaractaristica.NombreServidorBD, RespuestaCaractaristica.Caracteristicas(0).MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                pnForm.Visible = False
                pnForm.Enabled = False
                Master.MenuRodapeVisivel = False
                btnSalvar.Enabled = False
                btnCancelar.Enabled = False
                Caracteristica = Nothing
                ExecutarBusca()
                UpdatePanelGrid.Update()

            Else

                If RespuestaCaractaristica.Caracteristicas IsNot Nothing AndAlso _
                    RespuestaCaractaristica.Caracteristicas.Count > 0 Then

                    If RespuestaCaractaristica.Caracteristicas(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        ' mostrar o erro pro usuário
                        MyBase.MostraMensagem(RespuestaCaractaristica.Caracteristicas(0).MensajeError)
                    End If

                End If

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoCaracteristica.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoCaracteristicaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoCaracteristicaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCaracteristica.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoCaracteristicaObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se o codigo conteo foi preenchido
                If txtCodigoConteoForm.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoConteoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoConteoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoConteoForm.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoConteoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoCaracteristica(txtCodigoCaracteristica.Text.Trim()) Then

                strErro.Append(csvCodigoCaracteristica.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoCaracteristica.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoCaracteristica.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoCaracteristica.IsValid = True
            End If

            'Verifica se a descrição existe
            If ExisteDescricaoCaracteristica(txtDescricaoForm.Text.Trim()) Then

                strErro.Append(csvDescripcion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcion.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoForm.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcion.IsValid = True
            End If

            'Verifica se o codigo conteo existe.
            If ExisteCodigoConteoCaracteristica(txtCodigoConteoForm.Text.Trim()) Then

                strErro.Append(csvCodigoConteo.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoConteo.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoConteoForm.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoConteo.IsValid = True
            End If


        End If

        Return strErro.ToString

    End Function

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ExecutarGrabar()
    End Sub
End Class