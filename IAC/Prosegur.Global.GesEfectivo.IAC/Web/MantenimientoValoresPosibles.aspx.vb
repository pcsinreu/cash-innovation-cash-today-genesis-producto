Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' PopUp de Valores Posibles 
''' </summary>
''' <remarks></remarks>
''' <history>
''' [octavio.piramo] 17/02/2009 Criado
''' </history>
Partial Public Class MantenimientoValoresPosibles
    Inherits Base

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona controles para validação
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Define os parametros da base
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES
        ' desativar validação de ação
        MyBase.ValidarAcao = False

    End Sub

    ''' <summary>
    ''' Método que inicializa a tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Possíveis Ações passadas pela página BusquedaCanales:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception("err_passagem_parametro")

                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    ' preencher controles com valores da sessao
                    ConsomeValorPosible()
                    txtDescricao.Focus()

                ElseIf Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    ConsomeTemporario()
                    txtCodigo.Focus()

                End If

                ' atualizar viewstate
                ValoresPosibles = Session("tempValoresPosibles")

            End If

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Método que configura a tela ao renderizar.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Adiciona scripts nos cotnroles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

        btnCancelar.FuncaoJavascript = "window.close();"

        txtCodigo.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricao.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricao.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricao.ClientID & "').focus();return false;}} else {return true}; ")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    ''' <summary>
    ''' Traduz os controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("009_lbl_titulo")
        lblCodigo.Text = Traduzir("009_lbl_grd_codigo")
        lblDescricao.Text = Traduzir("009_lbl_grd_descripcion")
        lblVigente.Text = Traduzir("009_lbl_grd_vigente")
        lblValorDefecto.Text = Traduzir("009_lbl_grd_ValorDefecto")
        lblTituloValoresPosibles.Text = Traduzir("009_lbl_titulo")

        csvCodigo.ErrorMessage = Traduzir("009_msg_codigo_obligatorio")
        csvDescricao.ErrorMessage = Traduzir("009_msg_descripcion_obligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("009_msg_codigo_existente")
        csvDescripcionExistente.ErrorMessage = Traduzir("009_msg_descripcion_existente")

    End Sub

    ''' <summary>
    ''' Configura os tab index.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescricao.TabIndex = 2
        chkValorDefecto.TabIndex = 3
        chkVigente.TabIndex = 4
        btnGrabar.TabIndex = 5
        btnCancelar.TabIndex = 6

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property CodigoValidado() As Boolean
        Get
            Return ViewState("CodigoValidado")
        End Get
        Set(value As Boolean)
            ViewState("CodigoValidado") = value
        End Set
    End Property

    Private Property DescricaoValidada() As Boolean
        Get
            Return ViewState("DescricaoValidada")
        End Get
        Set(value As Boolean)
            ViewState("DescricaoValidada") = value
        End Set
    End Property

    Public Property ValorPosible() As ContractoServicio.ValorPosible.ValorPosible
        Get
            Return DirectCast(ViewState("ValorPosible"), ContractoServicio.ValorPosible.ValorPosible)
        End Get
        Set(value As ContractoServicio.ValorPosible.ValorPosible)
            ViewState("ValorPosible") = value
        End Set
    End Property

    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    Private Property ValorDefectoAtual() As Boolean
        Get
            Return ViewState("ValorDefectoAtual")
        End Get
        Set(value As Boolean)
            ViewState("ValorDefectoAtual") = value
        End Set
    End Property

    Private Property ValoresPosibles() As ContractoServicio.ValorPosible.ValorPosibleColeccion
        Get
            If ViewState("ValoresPosibles") Is Nothing Then
                If Session("tempValoresPosibles") Is Nothing Then
                    ViewState("ValoresPosibles") = New ContractoServicio.ValorPosible.ValorPosibleColeccion
                Else
                    ViewState("ValoresPosibles") = Session("tempValoresPosibles")
                End If
            End If
            Return ViewState("ValoresPosibles")
        End Get
        Set(value As ContractoServicio.ValorPosible.ValorPosibleColeccion)
            ViewState("ValoresPosibles") = value
        End Set
    End Property

    Private Property ValoresPosiblesTemporario() As ContractoServicio.ValorPosible.ValorPosibleColeccion
        Get
            Return ViewState("ValoresPosiblesTemporario")
        End Get
        Set(value As ContractoServicio.ValorPosible.ValorPosibleColeccion)
            ViewState("ValoresPosiblesTemporario") = value
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

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
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

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()
        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            ' validar se codigo foi informado
            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

                Dim objValorPosible As New ContractoServicio.ValorPosible.ValorPosible

                objValorPosible.Codigo = txtCodigo.Text
                objValorPosible.Descripcion = txtDescricao.Text
                objValorPosible.esValorDefecto = chkValorDefecto.Checked

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objValorPosible.Vigente = True
                Else
                    objValorPosible.Vigente = chkVigente.Checked
                End If

                Session("objValorPosible") = objValorPosible

                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "CanalOk", "window.returnValue=0;window.close();", True)

            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "CanalOk", "window.close();", True)
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub
#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento ao sair do campo codigo. Validar se o código existe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octávio.piramo] 02/03/2009 Criado
    ''' </history>
    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged

        Try

            If ExisteCodigoValorPossivel() Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            CodigoValidado = True

            ' setar foco no botão busca cliente 
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('" & txtDescricao.ClientID & "').focus();", True)

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento ao sair do campo descrição. Validar se descrição existe.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octávio.piramo] 02/03/2009 Criado
    ''' </history>
    Protected Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtDescricao.TextChanged

        Try


            If ExisteDescricaoValorPossivel() Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            DescricaoValidada = True

            ' setar foco no botão busca cliente 
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('" & btnGrabar.ID & "_img').focus();", True)

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                txtCodigo.Enabled = True
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True
                lblVigente.Visible = False
                chkVigente.Visible = False
                btnCancelar.Visible = True

                Me.DefinirRetornoFoco(btnCancelar, txtCodigo)

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                txtCodigo.Enabled = False
                txtDescricao.Enabled = False
                chkValorDefecto.Enabled = False
                lblVigente.Visible = True
                chkVigente.Visible = True
                chkVigente.Enabled = False
                btnCancelar.Visible = False

                Me.DefinirRetornoFoco(btnGrabar, btnGrabar)

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigo.Enabled = False
                txtDescricao.Enabled = True
                chkValorDefecto.Enabled = True
                chkVigente.Visible = True
                btnCancelar.Visible = True
                lblVigente.Visible = True

                Me.DefinirRetornoFoco(btnCancelar, txtDescricao)

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            ControleErro.ShowError(MontaMensagensErro, False)

        End If


       
    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Function ExisteDescricaoValorPossivel() As Boolean

        Try
            Dim strErro As New Text.StringBuilder(String.Empty)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, String.Empty)
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, txtCodigo.Text)
            End If

            If DescricaoExistente Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Function

    Private Function ExisteCodigoValorPossivel() As Boolean

        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            ' validar se código já existe
            If ValidarCodigo(txtCodigo.Text) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Método responsável por consumir o objeto passado pela tela de Busca
    ''' Obs: O objeto só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeValorPosible()

        If Session("setValorPosible") IsNot Nothing Then

            ValorPosible = DirectCast(Session("setValorPosible"), ContractoServicio.ValorPosible.ValorPosible)

            txtCodigo.Text = ValorPosible.Codigo
            txtCodigo.ToolTip = ValorPosible.Codigo
            txtDescricao.Text = ValorPosible.Descripcion
            txtDescricao.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, ValorPosible.Descripcion, String.Empty)
            chkVigente.Checked = ValorPosible.Vigente
            chkValorDefecto.Checked = ValorPosible.esValorDefecto

            'Se for modificação então guarda a descrição atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
               Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                DescricaoAtual = ValorPosible.Descripcion
                ValorDefectoAtual = ValorPosible.esValorDefecto
            End If

            If ValorPosible.Vigente Then
                chkVigente.Enabled = False
            End If

            Session("setValorPosible") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Consome a coleção de valores temporária
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeTemporario()

        If Session("colValoresPosibles") IsNot Nothing Then

            ValoresPosiblesTemporario = Session("colValoresPosibles")
            Session("colValoresPosibles") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Verifica se o código informado pelo usuário já existe na lista de valores posibles do cliente.
    ''' Retorna True quando já existe código.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Function ValidarCodigo(Codigo As String) As Boolean

        Dim pesquisa = From VP In ValoresPosibles _
                       Where VP.Codigo = Codigo

        If pesquisa IsNot Nothing AndAlso pesquisa.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Verifica se a descrição informada pelo usuário já existe na lista de valores posibles do cliente.
    ''' Retorna True quando já existe código.
    ''' </summary>
    ''' <param name="Descricao">Descrição que deseja verificar.</param>
    ''' <param name="Codigo">Códgio para o qual não deve validar a descrição.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 18/02/2009 Criado
    ''' </history>
    Private Function ValidarDescricao(Descricao As String, Codigo As String) As Boolean

        Dim pesquisa As Integer = 0

        ' caso o código não esteja vazio
        If Not Codigo.Equals(String.Empty) Then
            ' pesquisar pela descrição e por codigo diferente do informado
            pesquisa = (From VP In ValoresPosibles _
                        Where VP.Descripcion = Descricao AndAlso VP.Codigo <> Codigo).Count
        Else
            ' pesquisar apenas pela descrição
            pesquisa = (From VP In ValoresPosibles _
                        Where VP.Descripcion = Descricao).Count
        End If

        ' se retornou algo na pesquisa
        If pesquisa > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

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
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do subcanal é obrigatório
                If txtCodigo.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigo.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigo.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigo.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigo.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricao.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricao.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricao.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricao.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricao.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso ValidarCodigo(txtCodigo.Text) Then

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

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, String.Empty)
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoExistente = ValidarDescricao(txtDescricao.Text, txtCodigo.Text)
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescripcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricao.Focus()
                    focoSetado = True
                End If
            Else
                csvDescripcionExistente.IsValid = True
            End If

            'Valida se já existe algum Valor Defecto
            If chkValorDefecto.Checked AndAlso _
                Not ValorDefectoAtual AndAlso _
                (From VP In ValoresPosibles Where VP.esValorDefecto = True).Count > 0 Then

                strErro.Append(Traduzir("009_msg_valordefecto_existente") & Aplicacao.Util.Utilidad.LineBreak)

                If SetarFocoControle AndAlso Not focoSetado Then
                    chkValorDefecto.Focus()
                    focoSetado = True
                End If

            End If

        End If

        Return strErro.ToString

    End Function

#End Region

End Class