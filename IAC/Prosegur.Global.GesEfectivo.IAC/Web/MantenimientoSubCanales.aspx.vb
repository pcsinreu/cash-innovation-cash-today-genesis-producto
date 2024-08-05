
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' PopUp de SubCanais 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoSubCanales
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

    Private _CanalEsVigente As String
    Private ReadOnly Property CanalEsVigente() As Boolean
        Get
            If _CanalEsVigente Is Nothing Then
                _CanalEsVigente = Request.QueryString("canalEsVigente")
            End If
            Return IIf(_CanalEsVigente.ToLower() = "true", True, False)
        End Get
    End Property
   


#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        '    btnCancelar.FuncaoJavascript = "window.close();"
        txtCodigoSubCanal.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoSubCanal.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoSubCanal.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtObservaciones.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CANALES
    End Sub

    Protected Overrides Sub Inicializar()

        Try
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Not Page.IsPostBack Then

                'Recebe o código do Canal
                Dim strCodCanal As String = Request.QueryString("codcanal")

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
                    'SubCanal passado na tela de "Mantenimiento de Canales"
                    ConsomeSubCanal()
                    txtDescricaoSubCanal.Focus()
                Else
                    txtCodigoSubCanal.Focus()
                End If

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ConsomeTemporario()
                End If

            End If

            'Trata o foco dos campos
            TrataFoco()


        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            ControleBotoes()
            UpdatePanelCodigoSubCanal.Attributes.Add("style", "padding: 7px 0px 0px 0px; margin-bottom: 5px;")
            UpdatePanelDescricaoSubCanal.Attributes.Add("style", "padding: 7px 0px 0px 0px; margin-bottom: 5px;")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("001_titulo_mantenimiento_subcanales")
        lblCodigoSubCanal.Text = Traduzir("001_lbl_codigosubcanal")
        lblDescricaoSubCanal.Text = Traduzir("001_lbl_descricaosubcanal")
        lblVigente.Text = Traduzir("001_lbl_vigente")
        lblTituloCanales.Text = Traduzir("001_lbl_subtitulosmantenimientosubcanales")
        lblObservaciones.Text = Traduzir("001_lbl_observaciones")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescricaoAjeno.Text = Traduzir("019_lbl_descricaoAjeno")

        csvCodigoSubCanalObrigatorio.ErrorMessage = Traduzir("001_msg_subcanalcodigoobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("001_msg_subcanaldescripcionobligatorio")
        csvCodigoCanal.ErrorMessage = Traduzir("001_msg_subcanalcodigoexistente")
        csvDescripcion.ErrorMessage = Traduzir("001_msg_subcanaldescripcionexistente")

        btnAlta.Text = Traduzir("btnCodigoAjeno")
        btnAlta.ToolTip = Traduzir("btnCodigoAjeno")

        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
    End Sub
#End Region

#Region "[PROPRIEDADES]"


    Public Property OidSubCanal() As String
        Get
            Return ViewState("OidSubCanal")
        End Get
        Set(value As String)
            ViewState("OidSubCanal") = value
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

    Public Property SubCanal() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
        Get
            Return DirectCast(ViewState("SubCanal"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)
        End Get
        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)
            ViewState("SubCanal") = value
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

    Private Property subCanalesTemporario() As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion
        Get
            Return ViewState("subCanalesTemporario")
        End Get
        Set(value As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanalColeccion)
            ViewState("subCanalesTemporario") = value
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
    ''' Armazena em viewstate os códigos ajenos na petição
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/07/2013 - Criado
    ''' </history>
    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
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
        'Threading.Thread.Sleep(2000)
        ExecutarGrabar()
    End Sub

#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão gravar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 06/04/2009 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try


            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                Dim objSubCanal As New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

                objSubCanal.Codigo = txtCodigoSubCanal.Text.Trim
                objSubCanal.Descripcion = txtDescricaoSubCanal.Text.Trim
                objSubCanal.Observaciones = txtObservaciones.Text
                objSubCanal.CodigosAjenosSet = CodigosAjenosPeticion
                If SubCanal IsNot Nothing Then
                    objSubCanal.CodigosAjenos = SubCanal.CodigosAjenos
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objSubCanal.Vigente = CanalEsVigente
                Else
                    objSubCanal.Vigente = chkVigente.Checked
                End If

                Session("objSubCanal") = objSubCanal
                Session.Remove("objRespuestaGEPR_TSUBCANAL")

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)


            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "CanalOk", "window.close();", True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region
#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Sub Canal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoSubCanal_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoSubCanal.TextChanged
        Try

            If ExisteCodigoSubCanal(txtCodigoSubCanal.Text) Then
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
    ''' Evento de mudança de texto do campo Descrição Sub Canal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricaoSubCanal_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoSubCanal.TextChanged
        Try


            If ExisteDescricaoSubCanal(txtDescricaoSubCanal.Text) Then
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
                If txtCodigoSubCanal.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoSubCanalObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoSubCanalObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoSubCanal.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoSubCanalObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricaoSubCanal.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoSubCanal.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoSubCanal.Text.Trim()) AndAlso ExisteCodigoSubCanal(txtCodigoSubCanal.Text.Trim()) Then

                strErro.Append(csvCodigoCanal.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoCanal.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoSubCanal.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoCanal.IsValid = True
            End If

            'Verifica se a descrição existe
            If Not String.IsNullOrEmpty(txtDescricaoSubCanal.Text.Trim()) AndAlso ExisteDescricaoSubCanal(txtDescricaoSubCanal.Text.Trim()) Then

                strErro.Append(csvDescripcion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcion.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricaoSubCanal.Focus()
                    focoSetado = True
                End If
            Else
                csvDescripcion.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function


#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                txtCodigoSubCanal.Enabled = True
                txtDescricaoSubCanal.Enabled = True
                txtObservaciones.Enabled = True
                lblVigente.Visible = False
                chkVigente.Visible = False

                btnGrabar.Enabled = True
                btnGrabar.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                txtCodigoSubCanal.Enabled = False
                txtDescricaoSubCanal.Enabled = False
                txtObservaciones.Enabled = False
                lblVigente.Visible = True
                chkVigente.Visible = True
                chkVigente.Enabled = False
                btnAlta.Visible = True
                btnGrabar.Visible = False
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoSubCanal.Enabled = False
                txtDescricaoSubCanal.Enabled = True
                txtObservaciones.Enabled = True
                chkVigente.Visible = True
                lblVigente.Visible = True
                btnGrabar.Enabled = True
                btnGrabar.Visible = True
                chkVigente.Enabled = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
        End Select


    End Sub

#End Region

#Region "[MÉTODOS]"

    Private Function ExisteDescricaoSubCanal(descricaoSubCanal As String) As Boolean
        Dim objRespostaVerificarDescripcionCanal As IAC.ContractoServicio.Canal.VerificarDescripcionSubCanal.Respuesta
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                If descricaoSubCanal.Trim.Equals(DescricaoAtual) Then
                    Return False
                End If
            End If

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarDescripcionSubCanal As New IAC.ContractoServicio.Canal.VerificarDescripcionSubCanal.Peticion
            Dim strDescricaoSubCanal As String = descricaoSubCanal.Trim()

            'Verifica se o código do canal existe no BD
            objPeticionVerificarDescripcionSubCanal.Descripcion = strDescricaoSubCanal
            objRespostaVerificarDescripcionCanal = objProxyCanal.VerificarDescripcionSubCanal(objPeticionVerificarDescripcionSubCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarDescripcionCanal.CodigoError, objRespostaVerificarDescripcionCanal.NombreServidorBD, objRespostaVerificarDescripcionCanal.MensajeError) Then
                If objRespostaVerificarDescripcionCanal.Existe OrElse verificaDescricaoSubCanalMemoria(strDescricaoSubCanal) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Master.ControleErro.ShowError(objRespostaVerificarDescripcionCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

    Private Function ExisteCodigoSubCanal(codigoSubCanal As String) As Boolean
        Dim objRespostaVerificarCodigoCanal As IAC.ContractoServicio.Canal.VerificarCodigoSubCanal.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If
            Dim strCodSubCanal As String = codigoSubCanal.Trim()

            Dim objProxyCanal As New Comunicacion.ProxyCanal
            Dim objPeticionVerificarCodigoSubCanal As New IAC.ContractoServicio.Canal.VerificarCodigoSubCanal.Peticion

            'Verifica se o código do canal existe no BD
            objPeticionVerificarCodigoSubCanal.Codigo = strCodSubCanal
            objRespostaVerificarCodigoCanal = objProxyCanal.VerificarCodigoSubCanal(objPeticionVerificarCodigoSubCanal)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoCanal.CodigoError, objRespostaVerificarCodigoCanal.NombreServidorBD, objRespostaVerificarCodigoCanal.MensajeError) Then
                If objRespostaVerificarCodigoCanal.Existe OrElse verificaCodigoSubCanalMemoria(strCodSubCanal) Then
                    Return True
                Else
                    Return False
                End If
            Else
                MyBase.MostraMensagem(objRespostaVerificarCodigoCanal.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                Return False
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function

    ''' <summary>
    ''' Método responsável por consumir o canal passado pela tela de Mantenimiento de Canales
    ''' Obs: O Canal só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeSubCanal()

        If Session("setSubCanal") IsNot Nothing Then

            SubCanal = DirectCast(Session("setSubCanal"), IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal)
            'Dim SubCanal As New ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal

            txtCodigoSubCanal.Text = SubCanal.Codigo
            txtCodigoSubCanal.ToolTip = SubCanal.Codigo
            OidSubCanal = SubCanal.OidSubCanal
            txtDescricaoSubCanal.Text = SubCanal.Descripcion
            txtDescricaoSubCanal.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, SubCanal.Descripcion, String.Empty)

            If SubCanal.CodigosAjenos IsNot Nothing Then
                Dim iCodigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoBase = (From item In SubCanal.CodigosAjenos
                                    Where item.BolDefecto = True).FirstOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                    txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
                    txtDescricaoAjeno.ToolTip = iCodigoAjeno.DesAjeno
                End If
            End If

            txtObservaciones.Text = SubCanal.Observaciones
            chkVigente.Checked = SubCanal.Vigente

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = SubCanal.Descripcion
            End If

            If SubCanal.Vigente Then
                chkVigente.Enabled = True
            End If

            Session("setSubCanal") = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Consome a coleção de canais temporários
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeTemporario()

        If Session("colCanalesTemporario") IsNot Nothing Then

            subCanalesTemporario = Session("colCanalesTemporario")
            Session("colCanalesTemporario") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Trata foco da página
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
    ''' Verifica se o Código do SubCanal existe na memória
    ''' </summary>
    ''' <param name="codigoSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaCodigoSubCanalMemoria(codigoSubCanal As String) As Boolean

        Dim retorno = From c In subCanalesTemporario Where c.Codigo = codigoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Verifica se a Descrição do SubCanal existe na memória
    ''' </summary>
    ''' <param name="DescricaoSubCanal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaDescricaoSubCanalMemoria(DescricaoSubCanal As String) As Boolean

        Dim retorno = From c In subCanalesTemporario Where c.Descripcion = DescricaoSubCanal

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Consome o valor da session da coleção de código ajeno
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/05/2013 - Criado
    ''' </history>
    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TSUBCANAL") IsNot Nothing Then

            If SubCanal Is Nothing Then
                SubCanal = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.SubCanal
            End If

            SubCanal.CodigosAjenos = Session("objRespuestaGEPR_TSUBCANAL")
            Session.Remove("objRespuestaGEPR_TSUBCANAL")

            Dim iCodigoAjeno = (From item In SubCanal.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If SubCanal.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = SubCanal.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If
        End If

    End Sub
#End Region

    Protected Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoSubCanal.Text
        tablaGenesis.DesTablaGenesis = txtDescricaoSubCanal.Text
        tablaGenesis.OidTablaGenesis = OidSubCanal
        If SubCanal IsNot Nothing AndAlso SubCanal.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = SubCanal.CodigosAjenos
        End If

        Session("objGEPR_TSUBCANAL") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCANAL"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCANAL"
        End If

        '   ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_Ajeno", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjenoDenominacion');", True)
        Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 350, 750, False, True, btnConsomeCodigoAjeno.ClientID)
    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            ConsomeCodigoAjeno()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub chkVigente_CheckedChanged(sender As Object, e As EventArgs)
        If Not CanalEsVigente Then
            chkVigente.Checked = False
            MyBase.MostraMensagem(Traduzir("001_msg_imposible_activar_subcanal_canal_inactivo"))
        End If
    End Sub
End Class