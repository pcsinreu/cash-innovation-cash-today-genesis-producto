Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Página manter Morfologias
''' </summary>
''' <remarks></remarks>
''' <history>[bruno.costa] 23/12/2010 - Criado</history>
Partial Public Class MantenimientoMorfologia
    Inherits Base

#Region "[CONSTANTES]"



#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty


        pbo = New PostBackOptions(btnBaja)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnBaja.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & pgvComponentes.ClientID & "','" & _
                                           Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & _
                                           "','" & Traduzir("info_msg_registro_borrado") & " '))" & s & ";"


        pbo = New PostBackOptions(btnConsulta)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnConsulta.FuncaoJavascript = "if(VerificarRegistroSelecionadoGridView('" & pgvComponentes.ClientID & "','" & _
                                            Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarRegistro) & _
                                            "',''))" & s & ";"

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        ddlMetodoHabilitacion.TabIndex = 3
        chkAtivo.TabIndex = 4

        btnAcima.TabIndex = 14
        btnAbaixo.TabIndex = 15
        btnAlta.TabIndex = 16
        btnBaja.TabIndex = 17
        btnConsulta.TabIndex = 19
        btnGrabar.TabIndex = 20
        btnCancelar.TabIndex = 21
        btnVolver.TabIndex = 22

        Master.PrimeiroControleTelaID = txtCodigo.ClientID
        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not Morfologia.Componentes Is Nothing AndAlso Morfologia.Componentes.Count > 0 Then

                    btnConsulta.Focus()
                    Master.PrimeiroControleTelaID = "btnConsulta_img"

                Else

                    btnVolver.Focus()
                    Master.PrimeiroControleTelaID = "btnVolver_img"

                End If

                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        End Select

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Possíveis Ações passadas pela página BusquedaMediosPago:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' preenche combo metodo habilitación
                PreencherComboMetodo()

                If Not String.IsNullOrEmpty(Me.OidMorfologia) Then
                    'Estado Inicial dos control
                    CarregaDados()
                End If

                'Foco no campo divisa
                txtCodigo.Focus()

                ' na primeira vez que carrega a tela, parametros passados para o popup deve estar vazio
                Me.ParametrosPopUp = Nothing

            Else

                ' verifica se houve alguma alteração na coleção de componentes
                VerificarComponenteAtualizado()

            End If

            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        'Titula da Página
        Master.TituloPagina = Traduzir("021_titulo_mantenimiento")
        'SubTitulo da Página
        lblTituloMorfologia.Text = Traduzir("021_subtitulo_morfologia")
        'Subtitulo do Formulário
        lblTituloComponentes.Text = Traduzir("021_subtitulo_componentes")

        'Campos do formulário

        lblDescricao.Text = Traduzir("021_lbl_desc_morfologia")
        lblCodigo.Text = Traduzir("021_lbl_cod_morfologia")
        lblMetodoHabilitacion.Text = Traduzir("021_lbl_metodo_habilitacion")
        lblAtivo.Text = Traduzir("021_lbl_activo")

        'GridView
        pgvComponentes.PagerSummary = Traduzir("grid_lbl_pagersummary")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_cod_morfologia"))
        csvCodigoExistente.ErrorMessage = Traduzir("021_msg_codigo_existente")
        csvDescricaoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_desc_morfologia"))
        csvDescricaoExistente.ErrorMessage = Traduzir("021_msg_desc_existente")
        csvMetodoHabilitacionObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("021_lbl_metodo_habilitacion"))

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Evento disparado quando a linha do Gridview esta sendo populada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_EOnClickRowClientScript(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles pgvComponentes.EOnClickRowClientScript

        Try

            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            pgvComponentes.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("BolVigente").ToString.ToLower & ");"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento de preencher o Gridview quando o mesmo é paginado ou ordenado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPreencheDados(ByVal sender As Object, ByVal e As EventArgs) Handles pgvComponentes.EPreencheDados

        Try

            Dim objDT As DataTable

            objDT = pgvComponentes.ConvertListToDataTable(Me.Morfologia.Componentes)

            objDT.DefaultView.Sort = pgvComponentes.SortCommand

            pgvComponentes.ControleDataSource = objDT

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPager_SetCss(ByVal sender As Object, ByVal e As EventArgs) Handles pgvComponentes.EPager_SetCss
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
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' RowCreated do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles pgvComponentes.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_codmorfologia")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_funcion")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_tipo")
                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_divida_den_mp")
                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("021_lbl_grd_orden")
            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

#End Region

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique botão Alta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAlta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAlta.Click

        ExecutarAlta()

    End Sub

    ''' <summary>
    ''' Clique botão Baja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBaja_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBaja.Click

        ExecutarBaja()

    End Sub

    ''' <summary>
    ''' Clique botão Consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnConsulta_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnConsulta.Click

        ExecutarConsulta()

    End Sub

    ''' <summary>
    ''' Clique Botão Volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVolver.Click

        ExecutarVolver()

    End Sub

    ''' <summary>
    ''' Clique botão Cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        ExecutarCancelar()

    End Sub

    ''' <summary>
    ''' Clique botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGrabar.Click

        ExecutarGrabar()

    End Sub

    ''' <summary>
    ''' Clique botão Acima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAcima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAcima.Click

        Try

            Dim oidSelecionado As String = pgvComponentes.getValorLinhaSelecionada

            If Not String.IsNullOrEmpty(oidSelecionado) Then

                If Me.Morfologia.MoverComponente(oidSelecionado, False) Then
                    ' se moveu o item na lista 
                    ' reconfigura codigos morfologia
                    Me.Morfologia.ReconfigurarComponentes()
                    ' recarrega grid
                    PreencherGrid()
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "", "alert('" & Traduzir("info_msg_seleccionar_registro") & "');", True)
            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Clique botão Abaixo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAbaixo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAbaixo.Click

        Try

            Dim oidSelecionado As String = pgvComponentes.getValorLinhaSelecionada

            If Not String.IsNullOrEmpty(oidSelecionado) Then

                If Me.Morfologia.MoverComponente(oidSelecionado, True) Then
                    ' se moveu o item na lista 
                    ' reconfigura codigos morfologia
                    Me.Morfologia.ReconfigurarComponentes()
                    ' recarrega grid
                    PreencherGrid()
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "", "alert('" & Traduzir("info_msg_seleccionar_registro") & "');", True)
            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.SetMorfologia.Respuesta

            ' seta ação
            Select Case Me.Acao
                Case Aplicacao.Util.Utilidad.eAcao.Modificacion : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Modificacion
                Case Aplicacao.Util.Utilidad.eAcao.Baja : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Baja
                Case Else : Me.Morfologia.Acao = Negocio.BaseEntidade.eAcao.Alta
            End Select

            ' atualiza dados
            Me.Morfologia.CodMorfologia = txtCodigo.Text
            Me.Morfologia.DesMorfologia = txtDescricao.Text
            Me.Morfologia.BolVigente = True

            If Not String.IsNullOrEmpty(ddlMetodoHabilitacion.SelectedValue) Then
                Me.Morfologia.NecModalidadRecogida = ddlMetodoHabilitacion.SelectedValue
            End If

            objRespuesta = Me.Morfologia.Guardar(MyBase.LoginUsuario)

            ' trata erros
            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                Response.Redirect("~/BusquedaMorfologias.aspx", False)
            Else
                Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Cancelar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaMorfologias.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do Botão Volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaMorfologias.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do Botão Baja
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 03/01/2011 - Criado
    ''' </history>
    Public Sub ExecutarBaja()

        Try

            ' obtém oid selecionado
            Dim oidComponente As String = pgvComponentes.getValorLinhaSelecionada()

            ' remove o item selecionado
            Me.Morfologia.EliminarComponente(oidComponente)

            ' atualiza grid
            PreencherGrid()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Alta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub ExecutarAlta()

        Try

            If String.IsNullOrEmpty(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio")) OrElse
                Not LogicaNegocio.Util.URLValida(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("UrlServicio") & "Salidas/Integracion.asmx") Then

                Master.ControleErro.ShowError(Traduzir("err_msg_chave_invalida_webapp"))
                Exit Sub

            End If

            Dim url As String = "MantenimientoComponente.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta

            ' informa parametros do pop up
            Me.ParametrosPopUp = New MantenimientoComponente.Parametros

            ' preenche os códigos dos componentes já criados
            Me.ParametrosPopUp.Componentes = Me.Morfologia.Componentes

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Componente", "AbrirPopupModal('" & url & "', 465, 788,'ExecutarAlta');", True)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Consulta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub ExecutarConsulta()

        Try

            Dim url As String = "MantenimientoComponente.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta

            ' informa parametros do pop up
            Me.ParametrosPopUp = New MantenimientoComponente.Parametros

            Me.ParametrosPopUp.Componente = Me.Morfologia.ObtenerComponente(pgvComponentes.getValorLinhaSelecionada())

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Componente", "AbrirModalBootstrap('" & url & "', 465, 788,false);", True)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

#End Region

#End Region

    Private Sub txtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.TextChanged

        Try

            If txtCodigo.Text.Trim() = String.Empty Then
                CodigoExistente = False
                Exit Sub
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta = Negocio.Morfologia.VerificarMorfologia(txtCodigo.Text, String.Empty)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                ' se ocorreu algum erro, finaliza
                Exit Sub
            End If

            If objRespuesta.BolExiste Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub txtDescricao_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescricao.TextChanged

        Try

            If txtDescricao.Text.Trim() = String.Empty Then
                DescricaoExistente = False
                Exit Sub
            End If

            Dim objRespuesta As IAC.ContractoServicio.Morfologia.VerificarMorfologia.Respuesta = Negocio.Morfologia.VerificarMorfologia(String.Empty, txtDescricao.Text)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                ' se ocorreu algum erro, finaliza
                Exit Sub
            End If

            If objRespuesta.BolExiste Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlMetodoHabilitacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMetodoHabilitacion.SelectedIndexChanged
        ddlMetodoHabilitacion.ToolTip = ddlMetodoHabilitacion.SelectedItem.Text
    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' verifica se algum componente foi adicionado/alterado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/12/2010  criado
    ''' </history>
    Private Sub VerificarComponenteAtualizado()

        If Me.ParametrosPopUp IsNot Nothing Then

            If Me.ParametrosPopUp.AtualizouDados Then

                ' atualiza lista de componentes
                AtualizarDadosComponente()

                ' redefine o valor do atributo cod. morfologia de cada componente
                Me.Morfologia.ReconfigurarComponentes()

                ' atualiza grid
                PreencherGrid()

            End If

            ' limpa sessão
            Me.ParametrosPopUp = Nothing

        End If

    End Sub

    Private Sub AtualizarDadosComponente()

        Dim componente As Negocio.Componente = Me.ParametrosPopUp.Componente

        If componente.Acao = Negocio.BaseEntidade.eAcao.Alta Then

            ' inseriu novo componente

            If Me.Morfologia.Componentes Is Nothing Then
                Me.Morfologia.Componentes = New List(Of Negocio.Componente)
            End If

            Me.Morfologia.Componentes.Add(componente)

        Else

            ' atualiza dados de um componente existente
            Dim c As Negocio.Componente

            c = Me.Morfologia.ObtenerComponente(pgvComponentes.getValorLinhaSelecionada())

            c = componente

        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados do gridView 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregaDados()

        If Me.Morfologia Is Nothing Then
            Me.Morfologia = New Negocio.Morfologia
        End If

        Me.Morfologia.getMorfologia(Me.OidMorfologia)

        If Not Master.ControleErro.VerificaErro(Me.Morfologia.Respuesta.CodigoError, Me.Morfologia.Respuesta.NombreServidorBD, Me.Morfologia.Respuesta.MensajeError) Then
            Exit Sub
        End If

        'Preenche os controles do formulario
        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

            txtCodigo.Text = Me.Morfologia.CodMorfologia
            txtCodigo.ToolTip = Me.Morfologia.CodMorfologia
            txtDescricao.Text = Me.Morfologia.DesMorfologia
            txtDescricao.ToolTip = Me.Morfologia.DesMorfologia
            ddlMetodoHabilitacion.SelectedValue = Me.Morfologia.NecModalidadRecogida
            ddlMetodoHabilitacion.ToolTip = ddlMetodoHabilitacion.SelectedItem.Text
            chkAtivo.Checked = Me.Morfologia.BolVigente

            PreencherGrid()

        End If

    End Sub

    Private Sub PreencherGrid()

        If Me.Morfologia.Componentes IsNot Nothing Then

            'Carrega GridView            
            Dim objDT As New DataTable

            objDT = pgvComponentes.ConvertListToDataTable(Me.Morfologia.Componentes)

            pgvComponentes.CarregaControle(objDT)

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

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional ByVal SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescricao, csvDescricaoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(ddlMetodoHabilitacion, csvMetodoHabilitacionObrigatorio, SetarFocoControle, focoSetado))

                ' é obrigatório pelo menos 1 componente
                If Me.Morfologia.Componentes.Count = 0 Then
                    strErro.Append(Traduzir("021_msg_componente"))
                End If

            End If

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigo.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

            'Verifica se descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricao.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 23/12/2010 - Criado
    ''' </history>
    Public Sub PreencherComboMetodo()

        ddlMetodoHabilitacion.Items.Clear()

        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_a_pie"), ContractoServicio.Constantes.C_COD_MODALIDAD_A_PIE))
        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_en_base"), ContractoServicio.Constantes.C_COD_MODALIDAD_EN_BASE))
        ddlMetodoHabilitacion.Items.Add(New ListItem(Traduzir("022_modalidad_adicion_con_dos_tiras"), ContractoServicio.Constantes.C_COD_MODALIDAD_ADICION_CON_DOS_TIRAS))

        ddlMetodoHabilitacion.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlMetodoHabilitacion.Items.Insert(0, New ListItem(Traduzir("022_ddl_selecione"), String.Empty))

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        btnAcima.Visible = False                 '8
        btnAbaixo.Visible = False                '9

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnAlta.Visible = True          '1
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3

                'Estado Inicial Controles                                
                txtCodigo.Enabled = True

                If Me.Morfologia.Componentes IsNot Nothing AndAlso _
                Me.Morfologia.Componentes.Count > 0 Then
                    btnBaja.Visible = True             '3
                    btnConsulta.Visible = True         '4
                    btnAcima.Visible = True
                    btnAbaixo.Visible = True
                Else
                    btnBaja.Visible = False
                    btnConsulta.Visible = False
                    btnAcima.Visible = False
                    btnAbaixo.Visible = False
                End If

                lblAtivo.Visible = False
                chkAtivo.Checked = True
                chkAtivo.Enabled = False
                chkAtivo.Visible = False

                btnGrabar.Habilitado = True
                btnAlta.Habilitado = True
                btnVolver.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnAlta.Visible = False                  '1
                btnCancelar.Visible = False              '2

                If Me.Morfologia.Componentes IsNot Nothing AndAlso _
                Me.Morfologia.Componentes.Count > 0 Then
                    btnConsulta.Visible = True          '3
                Else
                    btnConsulta.Visible = False
                End If
                btnVolver.Visible = True                '4

                'Estado Inicial Controles
                txtCodigo.Enabled = False
                txtDescricao.Enabled = False
                ddlMetodoHabilitacion.Enabled = False
                lblAtivo.Visible = True
                chkAtivo.Enabled = False

                btnBaja.Visible = False                 '5
                btnGrabar.Visible = False               '7
                btnVolver.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnAlta.Visible = False          '1
                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3
                btnBaja.Visible = False          '4
                btnConsulta.Visible = False      '6 
                btnVolver.Visible = True         '7

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then

            Master.ControleErro.ShowError(MontaMensagensErro, False)

        End If

    End Sub

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

#End Region

#Region "[PROPRIEDADES]"

    Public Property Morfologia() As Negocio.Morfologia
        Get

            If ViewState("Morfologia") Is Nothing Then
                ViewState("Morfologia") = New Negocio.Morfologia
            End If

            Return ViewState("Morfologia")

        End Get
        Set(ByVal value As Negocio.Morfologia)

            ViewState("Morfologia") = value

        End Set
    End Property

    Public ReadOnly Property OidMorfologia() As String
        Get
            Return Request.QueryString("oid")
        End Get
    End Property

    Public Property ParametrosPopUp() As MantenimientoComponente.Parametros
        Get
            Return Session("ParametrosPopupComponente")
        End Get
        Set(ByVal value As MantenimientoComponente.Parametros)
            Session("ParametrosPopupComponente") = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(ByVal value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(ByVal value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region



End Class