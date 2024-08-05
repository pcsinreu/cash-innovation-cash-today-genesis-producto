Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion

''' <summary>
''' PopUp de Operario
''' </summary>
''' <remarks></remarks>
''' <history>
''' [kasantos] 25/09/2012 Criado
''' </history>
Partial Public Class BusquedaOperario
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        txtNombre.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")
        txtLogin.Attributes.Add("onkeypress", "EventoEnter('" & btnBuscar.ID & "_img');")

        btnCancelar.FuncaoJavascript = "window.close();"

        ' Registra o id do controle de erro
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "idControleErro", "var idControleErro = '" & Me.ControleErro.ClientID & "';", True)

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.txtLogin.Enabled = True
                Me.txtNombre.Enabled = True
                Me.btnBuscar.Visible = True
                Me.btnLimpar.Visible = True
                Me.btnAceptar.Visible = True
                Me.btnCancelar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        Me.txtDelegacion.TabIndex = 1
        Me.txtNombre.TabIndex = 2
        Me.txtApellido.TabIndex = 3
        Me.txtLogin.TabIndex = 4
        Me.btnBuscar.TabIndex = 5
        Me.btnLimpar.TabIndex = 9
        Me.btnAceptar.TabIndex = 10
        Me.btnCancelar.TabIndex = 11

        Me.DefinirRetornoFoco(btnCancelar, txtDelegacion)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.CLIENTE
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
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                If Not String.IsNullOrEmpty(Request.QueryString("codDelegacion")) Then

                    ' setar valor no campo codigo
                    txtDelegacion.Text = Request.QueryString("codDelegacion")

                    Acao = Enumeradores.eAcao.Busca

                End If
            End If

            ' trata o foco dos campos
            TrataFoco()

            ' chama a função que seta o tamanho das linhas do grid.
            TamanhoLinhas()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("022_titulo_busqueda_operario")
        lblDelegacion.Text = Traduzir("022_lbl_delegacion")
        lblNombre.Text = Traduzir("022_lbl_nombre")
        lblApellido.Text = Traduzir("022_lbl_apellido")
        lblLogin.Text = Traduzir("022_lbl_login")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("022_lbl_subtituloscriteriosbusqueda")
        lblSubTitulosOperario.Text = Traduzir("022_lbl_subtitulosoperario")
        lblSemRegistro.Text = Traduzir("022_lbl_sem_registro")

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Armazena os Operario encontrados na busca.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Property Operarios() As ContractoServ.GetUsuariosDetail.UsuarioColeccion
        Get
            If ViewState("VSOperario") Is Nothing Then
                ViewState("VSOperario") = New ContractoServ.GetUsuariosDetail.UsuarioColeccion
            End If
            Return ViewState("VSOperario")
        End Get
        Set(value As ContractoServ.GetUsuariosDetail.UsuarioColeccion)
            ViewState("VSOperario") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena o cliente selecionado no grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Property OperarioSelecionado() As ContractoServ.GetUsuariosDetail.Usuario
        Get
            Return Session("OperarioSelecionado")
        End Get
        Set(value As ContractoServ.GetUsuariosDetail.Usuario)
            Session("OperarioSelecionado") = value
        End Set
    End Property

    Property FiltroAplicacion() As String
        Get
            Return ViewState("FiltroAplicacion")
        End Get
        Set(value As String)
            ViewState("FiltroAplicacion") = value
        End Set
    End Property

    Property FiltroDelegacion() As String
        Get
            Return ViewState("FiltroDelegacion")
        End Get
        Set(value As String)
            ViewState("FiltroDelegacion") = value
        End Set
    End Property

    Property FiltroNombre() As String
        Get
            Return ViewState("FiltroNombre")
        End Get
        Set(value As String)
            ViewState("FiltroNombre") = value
        End Set
    End Property

    Property FiltroApellido() As String
        Get
            Return ViewState("FiltroApellido")
        End Get
        Set(value As String)
            ViewState("FiltroApellido") = value
        End Set
    End Property

    Property FiltroLogin() As String
        Get
            Return ViewState("FiltroLogin")
        End Get
        Set(value As String)
            ViewState("FiltroLogin") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena se o campo de pesquisa e um campo obrigatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Property CampoObrigatorio() As Boolean
        Get
            If ViewState("CampoObrigatorio") Is Nothing Then
                ViewState("CampoObrigatorio") = New Boolean
            End If
            Return ViewState("CampoObrigatorio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorio") = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS GRIDVIEW]"

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        ProsegurGridView.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles ProsegurGridView.EPager_SetCss
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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 9

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                'Índice das celulas do GridView Configuradas
                '0 - Nombre
                '1 - Apellido1
                '2 - Login


                'If CBool(e.Row.DataItem("PuestoVigente")) Then
                '    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.gif"
                'Else
                '    CType(e.Row.Cells(2).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.gif"
                'End If

            End If
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles ProsegurGridView.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("022_lbl_nombre")
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 5
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 6
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("022_lbl_apellido")
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 7
                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 8
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("022_lbl_login")
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 9
                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 10
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 22/12/2009 Criado
    ''' </history>
    Protected Sub ProsegurGridView1_EPreencheDados(sender As Object, e As EventArgs) Handles ProsegurGridView.EPreencheDados

        If MyBase.Acao = Enumeradores.eAcao.Inicial Then
            Return
        End If

        Dim objDT As DataTable

        Dim objRespoustaPuestos As Object

        objRespoustaPuestos = getUsuariosDetail().Usuarios

        objDT = TransformarContractoUsuarioEmDataTable(objRespoustaPuestos)

        If ProsegurGridView.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " Nombre ASC, Apellido1 ASC "
        Else
            objDT.DefaultView.Sort = ProsegurGridView.SortCommand
        End If

        ProsegurGridView.ControleDataSource = objDT

    End Sub

#End Region

#Region "[EVENTOS BOTOES]"

    ''' <summary>
    ''' Evento clique do botão aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            If ProsegurGridView.getValorLinhaSelecionada = String.Empty AndAlso CampoObrigatorio = True Then
                strErro.Append(Traduzir("022_seleccione_un_operario") & Constantes.LineBreak)
            End If

            If strErro.Length > 0 Then
                ControleErro.ShowError(strErro.ToString(), False)
                Exit Sub
            End If

            Dim Codigos() As String = ProsegurGridView.getValorLinhaSelecionada.Replace("$#", "|").Split("|")

            If Codigos.Length >= 1 AndAlso Not String.IsNullOrEmpty(Codigos(0)) Then
                ' salva puesto selecionado para sessão
                SalvarOperarioSelecionado(Codigos(0))
            Else
                ' setar para nulo objeto Puesto da sessao
                OperarioSelecionado = Nothing
                Session("LimparOperarioSelecionado") = True

                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "PuestoOk", "window.returnValue=0;window.close();", True)
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão buscar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Try

            'Threading.Thread.Sleep(2000)
            Acao = Enumeradores.eAcao.Busca

            'Filtros
            FiltroAplicacion = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodAplicacionConteo")
            FiltroDelegacion = txtDelegacion.Text
            FiltroNombre = txtNombre.Text
            FiltroApellido = txtApellido.Text
            FiltroLogin = txtLogin.Text

            'Retorna os canais de acordo com o filtro aciam
            PreencherOperario()

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão limpar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try

            'Limpa a consulta
            ProsegurGridView.CarregaControle(Nothing, True, True)

            pnlSemRegistro.Visible = False
            txtNombre.Text = String.Empty
            txtApellido.Text = String.Empty
            txtLogin.Text = String.Empty
            txtNombre.Focus()

            'Estado Inicial
            Acao = Enumeradores.eAcao.Inicial

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        Try
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "OperarioOk", "window.close();", True)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[MÉTODOS]"


    ''' <summary>
    ''' Faz a busca dos Postos com os parametros informados
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Function getUsuariosDetail() As ContractoServ.GetUsuariosDetail.Respuesta

        Dim objProxyLogin As New ListadosConteo.ProxyLogin
        Dim objPeticionOperario As New ContractoServ.GetUsuariosDetail.Peticion
        Dim objRespuestaOperario As ContractoServ.GetUsuariosDetail.Respuesta

        'Recebe os valores do filtro
        objPeticionOperario.Aplicacion = FiltroAplicacion
        objPeticionOperario.Delegacion = FiltroDelegacion
        objPeticionOperario.Login = FiltroLogin
        objPeticionOperario.Nombre = FiltroNombre
        objPeticionOperario.Apellido1 = FiltroApellido

        objRespuestaOperario = objProxyLogin.GetUsuariosDetail(objPeticionOperario)

        Return objRespuestaOperario


    End Function

    ''' <summary>
    ''' transforma o contrato do Posto em DataTable
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 09/09/11 Criado
    Public Function TransformarContractoUsuarioEmDataTable(usuarios As ContractoServ.GetUsuariosDetail.UsuarioColeccion) As DataTable
        Dim dt As DataTable

        Dim grid As New Prosegur.Web.ProsegurGridView
        dt = grid.ConvertListToDataTable(usuarios)

        Return dt

    End Function

    ''' <summary>
    ''' Preenche do grid com a coleção de Postos
    ''' </summary>
    ''' <remarks></remarks>
    ''' [lmsantana] 05/09/11 Criado
    Public Sub PreencherOperario()

        Dim objRespostaOperario As ContractoServ.GetUsuariosDetail.Respuesta

        'Busca os canais
        objRespostaOperario = getUsuariosDetail()

        If Not ControleErro.VerificaErro(objRespostaOperario.CodigoError, Nothing, objRespostaOperario.MensajeError) Then

            Acao = Enumeradores.eAcao.NoAction
            Exit Sub

        End If

        'Define a ação de busca somente se houve retorno de canais
        If objRespostaOperario.Usuarios.Count > 0 Then


            Operarios = objRespostaOperario.Usuarios

            pnlSemRegistro.Visible = False

            Dim objDt As DataTable
            objDt = TransformarContractoUsuarioEmDataTable(objRespostaOperario.Usuarios)

            If Acao = Enumeradores.eAcao.Busca Then

                objDt.DefaultView.Sort = " Nombre ASC, Apellido1 ASC "

            End If

            ProsegurGridView.CarregaControle(objDt)


        Else

            'Verifica se a ação de preencher foi acionada pela baixa de um canal("Atualizar o GridView" - Não exibe o painel informativo de "sem registros")
            'ou se foi aciona por outra ação (ex:botão buscar - exibe o painel informativo de "sem registros").

            'Limpa a consulta
            ProsegurGridView.DataSource = Nothing
            ProsegurGridView.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Constantes.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Enumeradores.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Salva o objeto selecionado para sessão. Esta sessão poderá ser consumida 
    ''' por outras telas que chamam a tela de busca de posto.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 25/09/2012 Criado
    ''' </history>
    Private Sub SalvarOperarioSelecionado(loginUsuario As String)

        Dim objUsuario As New ContractoServ.GetUsuariosDetail.Usuario

        Dim pesquisa = Operarios.FirstOrDefault(Function(f) f.Login = loginUsuario)

        If pesquisa IsNot Nothing Then

            ' setar objeto cliente para sessao
            OperarioSelecionado = pesquisa

            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "OperarioOk", "window.returnValue=0;window.close();", True)
        End If

    End Sub

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 16/12/2009 Criado
    ''' </history>

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

#End Region

End Class