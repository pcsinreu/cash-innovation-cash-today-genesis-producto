Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Tipos Procesado 
''' </summary>
''' <remarks></remarks>
''' <history>[anselmo.gois] 02/02/09 - Criado</history>
Partial Public Class MantenimientoTiposProcesado
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()
    End Sub

    Protected Overrides Sub AdicionarScripts()

        'seta o foco para o proximo controle quando preciona o enter
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtCodigoTiposProcesado.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTiposProcesado.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTiposProcesado.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTiposProcesado.TabIndex = 1
        txtDescricaoTiposProcesado.TabIndex = 2
        txtObservaciones.TabIndex = 3

        'chkCiego.TabIndex = 5
        'chkIac.TabIndex = 6
        chkVigente.TabIndex = 7
        btnGrabar.TabIndex = 8
        btnCancelar.TabIndex = 9
        btnVolver.TabIndex = 10

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO

        ' não deve adicionar scripts no load da base
        MyBase.AddScripts = False

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Possíveis Ações passadas pela página BusqueadaTiposProcesado:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
                    'Informa ao usuário que o parâmetro passado 
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                'Preenche a lista de características
                PreencherListaCaracteristicas()

                Select Case MyBase.Acao

                    Case Aplicacao.Util.Utilidad.eAcao.Modificacion, Aplicacao.Util.Utilidad.eAcao.Consulta

                        CarregaDados()
                        txtDescricaoTiposProcesado.Focus()

                        If MyBase.Acao = MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                            lstCaracteristicasDisponiveis.Visible = False
                            imbAdicionarCaracteristicasSelecionadas.Visible = False
                            imbAdicionarTodasCaracteristicas.Visible = False
                            imbRemoverCaracteristicasSelecionadas.Visible = False
                            imbRemoverTodasCaracteristicas.Visible = False

                            lstCaracteristicasSelecionadas.Enabled = False

                        End If

                    Case Else
                        txtCodigoTiposProcesado.Focus()
                End Select
            End If

            'chama a função que trata o foco
            TrataFoco()
            AdicionarScripts()

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

        lblCodigoTiposProcesado.Text = Traduzir("004_lbl_codigotiposprocesado")
        lblDescricaoTiposProcesado.Text = Traduzir("004_lbl_descripciontiposprocesado")
        lblVigente.Text = Traduzir("004_chk_vigente")
        chkVigente.Text = String.Empty
        lblTituloTiposProcesado.Text = Traduzir("004_titulo_matenimentotiposprocesado")
        lblTituloCaracteristicas.Text = Traduzir("004_titulo_caracteristicas_matenimentotiposprocesado")
        lblObservaciones.Text = Traduzir("004_lbl_observacion")
        csvCodigoObrigatorio.ErrorMessage = Traduzir("004_msg_tipoprocesadocodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("004_msg_tipoprocesadodescripcionobligatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("004_msg_codigotiposprocesadoexistente")
        csvDescricaoExistente.ErrorMessage = Traduzir("004_msg_descricaotiposprocesadoexistente")
        Master.TituloPagina = Traduzir("004_title_matenimentotiposprocesado")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Property TipoProcesado() As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado
        Get
            Return DirectCast(ViewState("TipoProcesado"), IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado)
        End Get
        Set(value As IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado)
            ViewState("TipoProcesado") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    '''  Carrega os dados quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Sub CarregaDados()

        If Session("setTiposProcesados") IsNot Nothing Then

            TipoProcesado = DirectCast(Session("setTiposProcesados"), IAC.ContractoServicio.TiposProcesado.GetTiposProcesado.TipoProcesado)
            Dim objColCaracteristicas As New ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuestaColeccion

            txtCodigoTiposProcesado.Text = TipoProcesado.Codigo
            txtCodigoTiposProcesado.ToolTip = TipoProcesado.Codigo

            txtDescricaoTiposProcesado.Text = TipoProcesado.Descripcion
            txtDescricaoTiposProcesado.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TipoProcesado.Descripcion, String.Empty)

            txtObservaciones.Text = TipoProcesado.Observaciones
            txtObservaciones.ToolTip = TipoProcesado.Observaciones

            chkVigente.Checked = TipoProcesado.Vigente
            EsVigente = TipoProcesado.Vigente

            If TipoProcesado.Caracteristicas IsNot Nothing Then

                For Each objCarac As ContractoServicio.TiposProcesado.GetTiposProcesado.CaracteristicaRespuesta In TipoProcesado.Caracteristicas
                    objCarac.Descripcion = objCarac.Codigo & " - " & objCarac.Descripcion
                    objColCaracteristicas.Add(objCarac)
                Next

                lstCaracteristicasSelecionadas.AppendDataBoundItems = True
                lstCaracteristicasSelecionadas.Items.Clear()
                lstCaracteristicasSelecionadas.DataTextField = "Descripcion"
                lstCaracteristicasSelecionadas.DataValueField = "Codigo"
                lstCaracteristicasSelecionadas.DataSource = objColCaracteristicas
                lstCaracteristicasSelecionadas.DataBind()

                For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                    lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.Items.FindByValue(objItem.Value))
                Next

            End If

            If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                hidTxtCodigoBanco.Value = String.Empty
                hidTxtDescricaoBanco.Value = String.Empty
                hidTxtObservacaoValBanco.Value = String.Empty
                hidChkVigente.Value = False
                hidChkVigenteTemp.Value = False
            Else
                hidTxtObservacaoValBanco.Value = TipoProcesado.Observaciones
                hidTxtCodigoBanco.Value = TipoProcesado.Codigo
                hidTxtDescricaoBanco.Value = TipoProcesado.Descripcion
                hidChkVigente.Value = TipoProcesado.Vigente
                hidChkVigenteTemp.Value = TipoProcesado.Vigente
            End If

            Session("setTiposProcesados") = Nothing
        End If

        'Se for modificação então guarda a descriçaõ atual para validação
        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            DescricaoAtual = txtDescricaoTiposProcesado.Text
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

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoTiposProcesado.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTiposProcesado.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoTiposProcesado.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTiposProcesado.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTiposProcesado.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescricaoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescricaoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoTiposProcesado.Focus()
                    focoSetado = True
                End If

            Else
                csvDescricaoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function


    ''' <summary>
    '''  Faz o tratamento de foco da pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
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
    ''' Função responsavel por preencher a lista de características.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [carlos.bomtempo] 26/05/2009 Created
    ''' </history>
    Public Sub PreencherListaCaracteristicas()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Respuesta
        objRespuesta = objProxyUtilida.GetComboCaracteristicas()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
            Exit Sub
        End If

        For Each objCaracteristica As IAC.ContractoServicio.Utilidad.GetComboCaracteristicas.Caracteristica In objRespuesta.Caracteristicas
            objCaracteristica.Descripcion = objCaracteristica.Codigo & " - " & objCaracteristica.Descripcion
        Next

        lstCaracteristicasDisponiveis.AppendDataBoundItems = True
        lstCaracteristicasDisponiveis.Items.Clear()
        lstCaracteristicasDisponiveis.DataTextField = "Descripcion"
        lstCaracteristicasDisponiveis.DataValueField = "Codigo"
        lstCaracteristicasDisponiveis.DataSource = objRespuesta.Caracteristicas
        lstCaracteristicasDisponiveis.DataBind()

    End Sub


    ''' <summary>
    ''' Informa se o código do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoTipoProcesado(codigoTipoProcesado As String) As Boolean

        Dim objRespostaVerificarCodigoTiposProcesado As IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Respuesta
        Try

            Dim objProxyTiposProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionVerificarCodigoTiposProcesado As New IAC.ContractoServicio.TiposProcesado.VerificarCodigoTipoProcesado.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoTiposProcesado.Codigo = codigoTipoProcesado.Trim
            objRespostaVerificarCodigoTiposProcesado = objProxyTiposProcesado.VerificarCodigoTipoProcesado(objPeticionVerificarCodigoTiposProcesado)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoTiposProcesado.CodigoError, objRespostaVerificarCodigoTiposProcesado.NombreServidorBD, objRespostaVerificarCodigoTiposProcesado.MensajeError) Then
                Return objRespostaVerificarCodigoTiposProcesado.Existe
            Else
                Return False
                Master.ControleErro.ShowError(objRespostaVerificarCodigoTiposProcesado.MensajeError)
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Informa se a descrição do canal já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 30/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoTipoProcesado(descricao As String) As Boolean

        Dim objRespostaVerificarDescricaoTiposProcesado As IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricao.Trim.Equals(DescricaoAtual) Then
                    DescricaoExistente = False
                    Exit Function
                End If
            End If

            Dim objProxyTiposProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionVerificarDescricaoTiposProcesado As New IAC.ContractoServicio.TiposProcesado.VerificarDescripcionTipoProcesado.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescricaoTiposProcesado.Descripcion = txtDescricaoTiposProcesado.Text
            objRespostaVerificarDescricaoTiposProcesado = objProxyTiposProcesado.VerificarDescripcionTipoProcesado(objPeticionVerificarDescricaoTiposProcesado)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescricaoTiposProcesado.CodigoError, objRespostaVerificarDescricaoTiposProcesado.NombreServidorBD, objRespostaVerificarDescricaoTiposProcesado.MensajeError) Then
                Return objRespostaVerificarDescricaoTiposProcesado.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescricaoTiposProcesado.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    '''  Evento do botão gravar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    ''' <summary>
    '''  Evento do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    ''' <summary>
    '''  Evento do botão voltar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Sub btnVolver_Click(sender As Object, e As System.EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTipoProcesado As New Comunicacion.ProxyTiposProcesado
            Dim objPeticionTipoProcesado As New IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Peticion
            Dim objRespuestaTipoProcesado As IAC.ContractoServicio.TiposProcesado.SetTiposProcesado.Repuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticionTipoProcesado.Vigente = True
            Else
                objPeticionTipoProcesado.Vigente = chkVigente.Checked
            End If

            ' atualizar propriedade
            EsVigente = chkVigente.Checked

            objPeticionTipoProcesado.Codigo = txtCodigoTiposProcesado.Text
            objPeticionTipoProcesado.Descripcion = txtDescricaoTiposProcesado.Text
            objPeticionTipoProcesado.Observaciones = txtObservaciones.Text
            objPeticionTipoProcesado.CodUsuario = MyBase.LoginUsuario
            objPeticionTipoProcesado.Caracteristicas = New ContractoServicio.TiposProcesado.SetTiposProcesado.CaracteristicaColeccion()

            For Each objItem As ListItem In lstCaracteristicasSelecionadas.Items
                objPeticionTipoProcesado.Caracteristicas.Add(New ContractoServicio.TiposProcesado.SetTiposProcesado.Caracteristica() With {.Codigo = objItem.Value})
            Next

            objRespuestaTipoProcesado = objProxyTipoProcesado.SetTiposProcesado(objPeticionTipoProcesado)

            'Define a ação de busca somente se houve retorno de canais

            If Master.ControleErro.VerificaErro(objRespuestaTipoProcesado.CodigoError, objRespuestaTipoProcesado.NombreServidorBD, objRespuestaTipoProcesado.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaTipoProcesado.CodigoError, objRespuestaTipoProcesado.NombreServidorBD, objRespuestaTipoProcesado.MensajeError) Then
                    Response.Redirect("~/BusquedaTiposProcesado.aspx", False)
                Else
                    Exit Sub
                End If

            Else

                If objRespuestaTipoProcesado.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    If objRespuestaTipoProcesado.MensajeError <> String.Empty Then
                        Master.ControleErro.ShowError(objRespuestaTipoProcesado.MensajeError, False)
                    End If
                End If

                Exit Sub

            End If

            hidTxtCodigoBanco.Value = txtCodigoTiposProcesado.Text
            hidTxtDescricaoBanco.Value = txtDescricaoTiposProcesado.Text
            hidChkVigente.Value = chkVigente.Checked
            hidTxtObservacaoValBanco.Value = txtObservaciones.Text
            hidChkVigenteTemp.Value = chkVigente.Checked

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTiposProcesado.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTiposProcesado.aspx", False)
    End Sub

#End Region
#End Region

#Region "[EVENTOS CONTROLES]"

    ''' <summary>
    '''  Metodo verifica se o codigo ja existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Sub txtCodigoTiposProcesado_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoTiposProcesado.TextChanged

        Try

            If ExisteCodigoTipoProcesado(txtCodigoTiposProcesado.Text) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    '''  Meotodo verifica se a descrição ja existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Private Sub txtDescricaoTiposProcesado_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoTiposProcesado.TextChanged

        Try
            If ExisteDescricaoTipoProcesado(txtDescricaoTiposProcesado.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub imbAdicionarTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarTodasCaracteristicas.Click

        Try

            Dim objListItem As ListItem
            While lstCaracteristicasDisponiveis.Items.Count > 0
                objListItem = lstCaracteristicasDisponiveis.Items(0)
                lstCaracteristicasDisponiveis.Items.Remove(objListItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)

            End While

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub imbAdicionarCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbAdicionarCaracteristicasSelecionadas.Click

        Try

            While lstCaracteristicasDisponiveis.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasDisponiveis.SelectedItem
                lstCaracteristicasDisponiveis.Items.Remove(lstCaracteristicasDisponiveis.SelectedItem)
                lstCaracteristicasSelecionadas.Items.Add(objListItem)
            End While

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub imbRemoverCaracteristicasSelecionadas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverCaracteristicasSelecionadas.Click

        Try

            While lstCaracteristicasSelecionadas.SelectedItem IsNot Nothing
                Dim objListItem As ListItem
                objListItem = lstCaracteristicasSelecionadas.SelectedItem

                lstCaracteristicasSelecionadas.Items.Remove(lstCaracteristicasSelecionadas.SelectedItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)
            End While

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub imbRemoverTodasCaracteristicas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imbRemoverTodasCaracteristicas.Click

        Try

            Dim objListItem As ListItem
            While lstCaracteristicasSelecionadas.Items.Count > 0
                objListItem = lstCaracteristicasSelecionadas.Items(0)
                lstCaracteristicasSelecionadas.Items.Remove(objListItem)
                lstCaracteristicasDisponiveis.Items.Add(objListItem)

            End While

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

    ''' <summary>
    '''  Controle dos botões
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/02/2009 Criado
    ''' </history> 
    Public Sub ControleBotoes()
        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3

                'Estado Inicial Controles                                
                txtCodigoTiposProcesado.Enabled = True

                btnVolver.Visible = False       '7

                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False
                lblVigente.Visible = False

                lstCaracteristicasDisponiveis.Visible = True
                imbAdicionarCaracteristicasSelecionadas.Visible = True
                imbAdicionarTodasCaracteristicas.Visible = True
                imbRemoverCaracteristicasSelecionadas.Visible = True
                imbRemoverTodasCaracteristicas.Visible = True

                lstCaracteristicasSelecionadas.Enabled = True
                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                btnCancelar.Visible = False              '2
                btnVolver.Visible = True                '4

                'Estado Inicial Controles
                txtCodigoTiposProcesado.Enabled = False
                txtDescricaoTiposProcesado.Enabled = False
                txtObservaciones.Enabled = False
                'lblVigente.Visible = True
                chkVigente.Enabled = False
                btnGrabar.Visible = False               '7

                lstCaracteristicasDisponiveis.Visible = False
                imbAdicionarCaracteristicasSelecionadas.Visible = False
                imbAdicionarTodasCaracteristicas.Visible = False
                imbRemoverCaracteristicasSelecionadas.Visible = False
                imbRemoverTodasCaracteristicas.Visible = False

                lstCaracteristicasSelecionadas.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoTiposProcesado.Enabled = False
                chkVigente.Visible = True
                lblVigente.Visible = True

                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                'lblVigente.Visible = True
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True

                lstCaracteristicasDisponiveis.Visible = True
                imbAdicionarCaracteristicasSelecionadas.Visible = True
                imbAdicionarTodasCaracteristicas.Visible = True
                imbRemoverCaracteristicasSelecionadas.Visible = True
                imbRemoverTodasCaracteristicas.Visible = True

                lstCaracteristicasSelecionadas.Enabled = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

        End Select

        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

#End Region

    
End Class