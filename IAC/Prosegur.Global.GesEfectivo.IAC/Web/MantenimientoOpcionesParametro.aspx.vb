
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Opções de Parametro
''' </summary>
''' <remarks></remarks>
''' <history>[gccosta] 01/03/12 - Criado</history>
Public Class MantenimientoOpcionesParametro
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

#End Region
#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()

        'Adicionar scripts na página
        txtCodigoOpcion.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoOpcion.ClientID & "').focus();return false;}} else {return true}; ")
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoOpcion.TabIndex = 1
        txtDescricaoOpcion.TabIndex = 2
        chkVigente.TabIndex = 3
        ddlDelegacion.TabIndex = 4
        btnGrabar.TabIndex = 5

        'Me.DefinirRetornoFoco(btnCancelar, txtCodigoOpcion)

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PARAMETRO

        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                PreencherComboDelegacionPorPais()
                'Possíveis Ações passadas pela página BusquedaMediosPago:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta _
                        ) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                   OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    'Consome o ParametroOpcion passado pela PopUp de "Mantenimeinto de Parametro"
                    ConsomeParametroOpcion()
                    txtDescricaoOpcion.Focus()
                    'Me.DefinirRetornoFoco(btnCancelar, txtDescricaoOpcion)
                Else
                    txtCodigoOpcion.Focus()
                End If

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ConsomeColecaoParametroOpcion()
                End If

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
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        'Titula da Página
        Page.Title = Traduzir("029_titulo_mantenimiento_parametroopcion")

        'SubTitulo
        lblTituloOpcionParametro.Text = Traduzir("029_subtitulo_mantenimiento_parametroopcion")
        btnGrabar.Text = Traduzir("btnAceptar")
        'Campos do formulário
        lblCodigoOpcion.Text = Traduzir("029_lbl_codigoparametroopcion")
        lblDescricaoOpcion.Text = Traduzir("029_lbl_descricaoparametroopcion")
        lblVigente.Text = Traduzir("029_lbl_vigente")
        lblDelegacion.Text = Traduzir("029_lbl_delegacion")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = Traduzir("029_msg_parametroopcion_codigoobligatorio")
        csvDescricaoOpcion.ErrorMessage = Traduzir("029_msg_parametroopcion_descripcionobligatorio")

        csvCodigoOpcionExistente.ErrorMessage = Traduzir("029_msg_parametroopcion_codigoExistente")
        csvDescricaoOpcionExistente.ErrorMessage = Traduzir("029_msg_parametroopcion_descripcionExistente")

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True        '1

                'Estado Inicial Controles                                
                txtCodigoOpcion.Enabled = True

                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False

                btnGrabar.Enabled = True

            Case Aplicacao.Util.Utilidad.eAcao.Duplicar

                btnGrabar.Visible = True        '1

                'Estado Inicial Controles                                
                txtCodigoOpcion.Enabled = True

                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False

                btnGrabar.Enabled = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnGrabar.Visible = False               '2

                'Estado Inicial Controles
                txtCodigoOpcion.Enabled = False
                txtDescricaoOpcion.Enabled = False

                ddlDelegacion.Enabled = False

                lblVigente.Visible = True
                chkVigente.Enabled = False

                'Me.DefinirRetornoFoco(btnCancelar, txtCodigoOpcion)

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnGrabar.Visible = True               '1


                txtCodigoOpcion.Enabled = False
                chkVigente.Visible = True


                lblVigente.Visible = True
                ' se estiver checado e objeto for vigente
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Enabled = True
                Me.DefinirRetornoFoco(btnGrabar, txtDescricaoOpcion)

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False        '1

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem
        If MontaMensagensErro() <> String.Empty Then
            MyBase.MostraMensagem(MontaMensagensErro)
        End If
    End Sub
#End Region

#Region "[PROPRIEDADES]"

    Public Property ParametroOpcion() As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion
        Get
            Return DirectCast(ViewState("ParametroOpcion"), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)
        End Get
        Set(value As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)
            ViewState("ParametroOpcion") = value
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

    Private Property EsVigente() As Boolean
        Get
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = False
            End If
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de terminos temporária
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ParametroOpcionTemporario() As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion
        Get
            Return ViewState("ParametroOpcionTemporario")
        End Get
        Set(value As IAC.ContractoServicio.Parametro.GetParametroOpciones.OpcionColeccion)
            ViewState("ParametroOpcionTemporario") = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist de Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gccosta] 09/04/2012Created
    ''' </history>
    Public Sub PreencherComboDelegacionPorPais()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Respuesta
        Dim codPais As String = String.Empty

        Dim a As New Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio.RecuperarParametros.DatosPuesto
        codPais = a.CodigoPais

        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboDelegacionesPorPais.Peticion With {.CodDelegacion = DelegacionConectada.First.Key}
        objRespuesta = objProxyUtilida.GetComboDelegacionesPorPais(objPeticion)

        ddlDelegacion.AppendDataBoundItems = True
        ddlDelegacion.Items.Clear()
        ddlDelegacion.Items.Add(New ListItem(Traduzir("008_ddl_selecioneDelegacion"), String.Empty))
        ddlDelegacion.DataTextField = "Codigo"
        ddlDelegacion.DataValueField = "Descripcion"
        ddlDelegacion.DataSource = objRespuesta.Delegaciones
        ddlDelegacion.DataBind()

    End Sub

    ''' <summary>
    ''' Método responsável por consumir o canal passado pela tela de Mantenimiento de Termino
    ''' Obs: O Termino só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeParametroOpcion()

        If Session("setParametroOpcion") IsNot Nothing Then

            ParametroOpcion = DirectCast(Session("setParametroOpcion"), IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)

            'Campos Texto
            txtCodigoOpcion.Text = ParametroOpcion.CodigoOpcion
            txtCodigoOpcion.ToolTip = ParametroOpcion.CodigoOpcion

            txtDescricaoOpcion.Text = ParametroOpcion.DescripcionOpcion
            txtDescricaoOpcion.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, ParametroOpcion.DescripcionOpcion, String.Empty)

            If ParametroOpcion.CodDelegacion Is Nothing OrElse ParametroOpcion.CodDelegacion Is String.Empty Then
                ddlDelegacion.SelectedItem.Text = Traduzir("008_ddl_selecioneDelegacion")
            Else
                ddlDelegacion.Items.Add(New ListItem(Traduzir("008_ddl_selecioneDelegacion"), String.Empty))
                ddlDelegacion.SelectedItem.Text = ParametroOpcion.CodDelegacion
            End If

            ddlDelegacion.ToolTip = ddlDelegacion.SelectedItem.Text

            'Campos CheckBox
            chkVigente.Checked = ParametroOpcion.EsVigente
            EsVigente = ParametroOpcion.EsVigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = ParametroOpcion.DescripcionOpcion
            End If

            If ParametroOpcion.EsVigente Then
                chkVigente.Enabled = False
            End If

            'Retira da sessão o objeto consumido
            Session("setParametroOpcion") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a coleção de terminos termporária passada pela página Mantenimineto de Medios de Pago(Pai)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeColecaoParametroOpcion()

        If Session("colOpcionParametro") IsNot Nothing Then

            ParametroOpcionTemporario = Session("colOpcionParametro")
            Session("colOpcionParametro") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Envia o objeto para a página de mantenimiento parametro(Página Pai)
    ''' </summary>
    ''' <param name="pobjTermino"></param>    
    Private Sub EnviaObjetoParametroForm(pobjTermino As IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion)
        Session("objParametroOpcion") = pobjTermino
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
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se é para validar se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do subcanal é obrigatório
                If txtCodigoOpcion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoOpcion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricaoOpcion.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoOpcion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoOpcion.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoOpcion.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoOpcion.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoOpcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoOpcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoOpcion.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoOpcionExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoOpcionExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoOpcionExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoOpcion.Focus()
                    focoSetado = True
                End If
            Else
                csvDescricaoOpcionExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Verifica CodigoOpcao
    ''' </summary>
    ''' <param name="codigoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExisteCodigoOpcao(codigoOpcao As String) As Boolean

        Dim objRespostaVerificarCodigoOpcao As IAC.ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Respuesta
        Try
            Dim strCodigoOpcao As String = codigoOpcao.Trim()

            Dim objProxyParametro As New Comunicacion.ProxyParametro
            Dim objPeticionVerificarCodigoOpcao As New IAC.ContractoServicio.Parametro.VerificarCodigoParametroOpcion.Peticion

            'Verifica se o código da opcao existe no BD
            objPeticionVerificarCodigoOpcao.Codigo = strCodigoOpcao
            objRespostaVerificarCodigoOpcao = objProxyParametro.VerificarCodigoParametroOpcion(objPeticionVerificarCodigoOpcao, Request.QueryString("codparametro"), Request.QueryString("codaplicacion"))

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoOpcao.CodigoError, objRespostaVerificarCodigoOpcao.NombreServidorBD, objRespostaVerificarCodigoOpcao.MensajeError) Then
                If objRespostaVerificarCodigoOpcao.Existe Then
                    Return True
                Else
                    Return False
                End If
            Else
                'ControleErro.ShowError(objRespostaVerificarCodigoOpcao.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    ''' <summary>
    ''' Verifica DescricaoOpcao
    ''' </summary>
    ''' <param name="descricaoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExisteDescricaoOpcao(descricaoOpcao As String) As Boolean

        Dim objRespostaVerificarDescripcionOpcao As IAC.ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Respuesta

        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricaoOpcao.Trim.Equals(DescricaoAtual) Then
                    Return False
                End If
            End If

            Dim objProxyParametro As New Comunicacion.ProxyParametro
            Dim objPeticionVerificarDescripcionOpcao As New IAC.ContractoServicio.Parametro.VerificarDescricaoParametroOpcion.Peticion
            Dim strDescricaoOpcao As String = descricaoOpcao.Trim()

            'Verifica se a descrição da opção existe no BD
            objPeticionVerificarDescripcionOpcao.Descripcion = strDescricaoOpcao
            objRespostaVerificarDescripcionOpcao = objProxyParametro.VerificarDescricaoParametroOpcion(objPeticionVerificarDescripcionOpcao, Request.QueryString("codparametro"), Request.QueryString("codaplicacion"))

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescripcionOpcao.CodigoError, objRespostaVerificarDescripcionOpcao.NombreServidorBD, objRespostaVerificarDescripcionOpcao.MensajeError) Then
                If objRespostaVerificarDescripcionOpcao.Existe Then
                    Return True
                Else
                    Return False
                End If
            Else
                'ControleErro.ShowError(objRespostaVerificarDescripcionOpcao.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    ''' <summary>
    ''' Verifica se o Código da opção existe na memória
    ''' </summary>
    ''' <param name="codigoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaCodigoOpcaoMemoria(codigoOpcao As String) As Boolean

        Dim retorno = From p In ParametroOpcionTemporario Where p.CodigoOpcion = codigoOpcao

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Verifica se a Descrição da opção existe na memória.
    ''' </summary>
    ''' <param name="descricaoOpcao"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaDescricaoOpcaoMemoria(descricaoOpcao As String) As Boolean

        Dim retorno = From p In ParametroOpcionTemporario Where p.DescripcionOpcion = descricaoOpcao

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region

#Region "[EVENTOS BOTÕES]"

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

#Region "Ações Botões Independentes"
    ''' <summary>
    ''' Evento do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()
        Try

            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            'If Not verificaCodigoOpcaoMemoria(txtCodigoOpcion.Text) Then
            '    'manda msg de erro
            'End If



            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

                Dim objParametroOpcion As New IAC.ContractoServicio.Parametro.GetParametroOpciones.Opcion

                'Campos Texto
                objParametroOpcion.CodigoOpcion = txtCodigoOpcion.Text.Trim
                objParametroOpcion.DescripcionOpcion = txtDescricaoOpcion.Text.Trim

                'Campos CheckBox
                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objParametroOpcion.EsVigente = True
                Else
                    objParametroOpcion.EsVigente = chkVigente.Checked
                End If

                objParametroOpcion.CodDelegacion = If(ddlDelegacion.SelectedValue Is String.Empty, String.Empty, ddlDelegacion.SelectedItem.Text)

                objParametroOpcion.Parametro.NecTipoComponente = ContractoServicio.Parametro.TipoComponente.Combobox
                objParametroOpcion.Parametro.NecTipoDato = ContractoServicio.Parametro.TipoDato.AlfaNumerico
                objParametroOpcion.Parametro.CodParametro = Request.QueryString("codparametro")

                'Seta a sessão que irá consumir o objeto de parametroopcion na página de manutenção de parametros
                EnviaObjetoParametroForm(objParametroOpcion)

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

            Else
                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub


#End Region

#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Opção
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoOpcion_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoOpcion.TextChanged
        Try

            If ExisteCodigoOpcao(txtCodigoOpcion.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            CodigoValidado = True

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de mudança de texto do campo Descrição Opção
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoOpcion_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoOpcion.TextChanged
        Try


            If ExisteDescricaoOpcao(txtDescricaoOpcion.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            DescricaoValidada = True

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region

    Private Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDelegacion.SelectedIndexChanged
        ddlDelegacion.ToolTip = ddlDelegacion.SelectedItem.Text
    End Sub

End Class