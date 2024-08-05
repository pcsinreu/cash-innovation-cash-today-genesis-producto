Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Página de Gerenciamento de Términos de Medios de Pago 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 06/03/09 - Criado</history>
Partial Public Class MantenimientoTerminoMediosPago
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

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Adicionar scripts na página

        txtLongitud.Attributes.Add("onkeypress", "return ValorNumerico(event);")
        txtCodigoTerminoMedioPago.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTerminoMedioPago.ClientID & "').focus();return false;}} else {return true}; ")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        txtLongitud.Attributes.Add("onKeyDown", "BloquearColar();")
        txtValorInicial.Attributes.Add("onKeyDown", "BloquearColar();")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTerminoMedioPago.TabIndex = 1
        txtDescricaoTerminoMedioPago.TabIndex = 2
        txtObservaciones.TabIndex = 3
        chkVigente.TabIndex = 4
        ddlTipoFormato.TabIndex = 5
        txtLongitud.TabIndex = 6
        txtValorInicial.TabIndex = 7
        ddlFormatoMascara.TabIndex = 8
        chkMostrarCodigo.TabIndex = 9
        ddlFormulaAlgoritmo.TabIndex = 10
        btnValoresPosibles.TabIndex = 11
        btnGrabar.TabIndex = 12

        Me.DefinirRetornoFoco(btnGrabar, txtCodigoTerminoMedioPago)

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

                'Preenche Combo
                PreencherComboTipo()
                PreencherComboMascara()
                PreencherComboAlgoritmo()

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                   OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                    'Consome o Termino de Medio de Pago passado pela PopUp de "Mantenimeinto de Medios de Pago"
                    ConsomeTerminoMedioPago()

                    txtDescricaoTerminoMedioPago.Focus()
                    Me.DefinirRetornoFoco(btnGrabar, txtDescricaoTerminoMedioPago)
                Else
                    txtCodigoTerminoMedioPago.Focus()
                End If

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ConsomeColecaoTerminosTemporario()
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
            txtCodigoTerminoMedioPago.Attributes.Add("style", "margin: 0px 0px 0px -2px !important;")
        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()

        'Titula da Página
        Page.Title = Traduzir("014_titulo_mantenimiento_termino_mediospago")

        'SubTitulo
        lblTituloTerminoMedioPago.Text = Traduzir("014_subtitulo_mantenimiento_termino_mediospago")

        btnGrabar.Text = Traduzir("btnAceptar")
        btnGrabar.Text = Traduzir("btnAceptar")

        'Campos do formulário
        lblCodigoTerminoMedioPago.Text = Traduzir("014_lbl_codigomediopago")
        lblDescricaoTerminoMedioPago.Text = Traduzir("014_lbl_descricaomediopago")
        lblVigente.Text = Traduzir("014_lbl_vigente")
        lblObservaciones.Text = Traduzir("014_lbl_observaciones")
        lblTipoFormato.Text = Traduzir("014_lbl_formato")
        lblLongitud.Text = Traduzir("014_lbl_longitude")
        lblValorInicial.Text = Traduzir("014_lbl_valorinicial")
        lblMostrarCodigo.Text = Traduzir("014_lbl_mostrarcodigo")
        lblValidacaoFormato.Text = Traduzir("014_lbl_validacaoformato")
        lblValidacaoFormula.Text = Traduzir("014_lbl_validacaoformula")
        btnValoresPosibles.Text = Traduzir("014_lbl_valoresposibles")
        btnValoresPosibles.ToolTip = Traduzir("014_lbl_valoresposibles")

        'Custom Validator
        csvCodigoObrigatorio.ErrorMessage = Traduzir("014_msg_terminomediopago_codigoobligatorio")
        csvCodigoMedioPagoExistente.ErrorMessage = Traduzir("014_msg_terminomediopago_codigopagoexistente")
        csvDescricaoTerminoMedioPago.ErrorMessage = Traduzir("014_msg_terminomediopago_descripcionobligatorio")
        csvLongitudeObrigatorio.ErrorMessage = Traduzir("014_msg_terminomediopago_longitudobligatorio")
        csvLongitudeValorInvalido.ErrorMessage = String.Format(Traduzir("014_msg_terminomediopago_longitudeinvalidaobligatorio"), Integer.MaxValue)
        csvTipoFormatoObrigatorio.ErrorMessage = Traduzir("014_msg_terminomediopago_formatoobligatorio")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS TEXTBOX]"

    Private Sub txtDescricaoTerminoMedioPago_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricaoTerminoMedioPago.TextChanged
        Threading.Thread.Sleep(200)
    End Sub

    Private Sub txtLongitud_TextChanged(sender As Object, e As System.EventArgs) Handles txtLongitud.TextChanged
        Threading.Thread.Sleep(200)
    End Sub

    Private Sub txtValorInicial_TextChanged(sender As Object, e As System.EventArgs) Handles txtValorInicial.TextChanged
        Threading.Thread.Sleep(200)
    End Sub

    Private Sub ddlTipoFormato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlTipoFormato.SelectedIndexChanged

        ddlTipoFormato.ToolTip = ddlTipoFormato.SelectedItem.Text

        Threading.Thread.Sleep(200)
    End Sub

    Private Sub ddlFormatoMascara_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFormatoMascara.SelectedIndexChanged

        ddlFormatoMascara.ToolTip = ddlFormatoMascara.SelectedItem.Text

    End Sub

    Private Sub ddlFormulaAlgoritmo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlFormulaAlgoritmo.SelectedIndexChanged

        ddlFormulaAlgoritmo.ToolTip = ddlFormulaAlgoritmo.SelectedItem.Text

    End Sub

    ''' <summary>
    ''' Verifica se o código do termino está na memoria
    ''' </summary>
    ''' <param name="codigoTermino"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function verificaCodigoTerminoMemoria(codigoTermino As String) As Boolean

        Dim retorno = From c In TerminosMediopagoTemporario Where c.Codigo = codigoTermino

        If retorno Is Nothing OrElse retorno.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region

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

    ''' <summary>
    ''' Clique do botão Valores Posibles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnValoresPosibles_Click(sender As Object, e As EventArgs) Handles btnValoresPosibles.Click
        ExecutarValores()
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"
    ''' <summary>
    ''' Evento do Botão Grabar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarGrabar()
        Try

            ValidarCamposObrigatorios = True

            Dim strErro As String = MontaMensagensErro(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

                Dim objTerminoMedioPago As New IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago

                'Campos Texto
                objTerminoMedioPago.Codigo = txtCodigoTerminoMedioPago.Text.Trim
                objTerminoMedioPago.Descripcion = txtDescricaoTerminoMedioPago.Text.Trim
                objTerminoMedioPago.Observacion = txtObservaciones.Text

                If String.IsNullOrEmpty(txtLongitud.Text.Trim) Then
                    objTerminoMedioPago.Longitud = Nothing
                Else
                    objTerminoMedioPago.Longitud = Convert.ToInt32(txtLongitud.Text.Trim)
                End If

                objTerminoMedioPago.ValorInicial = txtValorInicial.Text.Trim

                'Campos CheckBox
                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objTerminoMedioPago.Vigente = True
                    OrdenTermino = TerminosMediopagoTemporario.Count
                Else
                    objTerminoMedioPago.Vigente = chkVigente.Checked
                End If

                objTerminoMedioPago.MostrarCodigo = chkMostrarCodigo.Checked

                'Campos DropDownList

                'Tipo Formato
                objTerminoMedioPago.CodigoFormato = ddlTipoFormato.SelectedValue
                objTerminoMedioPago.DescripcionFormato = ddlTipoFormato.SelectedItem.Text
                'Formato Mascara
                objTerminoMedioPago.CodigoMascara = ddlFormatoMascara.SelectedValue
                objTerminoMedioPago.DescripcionMascara = ddlFormatoMascara.SelectedItem.Text

                'Formula Algorítmo
                objTerminoMedioPago.CodigoAlgoritmo = ddlFormulaAlgoritmo.SelectedValue
                objTerminoMedioPago.DescripcionAlgoritmo = ddlFormulaAlgoritmo.SelectedItem.Text

                'Ordem
                objTerminoMedioPago.OrdenTermino = OrdenTermino

                'Valore de Termino
                objTerminoMedioPago.ValoresTermino = ValoresTerminosMedioPagoTemporario

                'Seta a sessão que irá consumir o objeto de termino na página de manutenção de términos
                EnviaObjetoTerminoParentForm(objTerminoMedioPago)

                Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
                ' fechar janela
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento do botão valores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ExecutarValores()
        Try
            Dim url As String = "MantenimientoValoresTerminoMediosPago.aspx?acao=" & Acao

            'Seta a session com o TerminoMedioPago que será consmida na abertura da PopUp de Mantenimiento de TerminosMedioPago
            SetValoresTerminosMedioPagoColecaoPopUp()

            Master.ExibirModal(url, Traduzir("014_titulo_mantenimiento_valorterminomediopagos"), 300, 840, False, True, btnConsomeValores.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True        '1

                'Estado Inicial Controles                                
                txtCodigoTerminoMedioPago.Enabled = True

                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False

                btnGrabar.Enabled = True
                HabilitaDesabilitaLongitud()

            Case Aplicacao.Util.Utilidad.eAcao.Duplicar

                btnGrabar.Visible = True        '1

                'Estado Inicial Controles                                
                txtCodigoTerminoMedioPago.Enabled = True

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
                txtCodigoTerminoMedioPago.Enabled = False
                txtDescricaoTerminoMedioPago.Enabled = False
                txtObservaciones.Enabled = False
                txtLongitud.Enabled = False
                txtValorInicial.Enabled = False

                lblVigente.Visible = True
                chkVigente.Enabled = False
                chkMostrarCodigo.Enabled = False

                ddlTipoFormato.Enabled = False
                ddlFormatoMascara.Enabled = False
                ddlFormulaAlgoritmo.Enabled = False

                Me.DefinirRetornoFoco(btnGrabar, btnValoresPosibles)

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                btnGrabar.Visible = True               '1

                txtCodigoTerminoMedioPago.Enabled = False
                chkVigente.Visible = True


                lblVigente.Visible = True
                ' se estiver checado e objeto for vigente
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Enabled = True
                HabilitaDesabilitaLongitud()
                Me.DefinirRetornoFoco(btnGrabar, txtDescricaoTerminoMedioPago)

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
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If
    End Sub
#End Region

#Region "[PROPRIEDADES]"

    Public Property TerminoMedioPago() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago
        Get
            Return DirectCast(ViewState("TerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)
            ViewState("TerminoMedioPago") = value
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

    Private Property OrdenTermino() As Integer
        Get
            If ViewState("OrdenTermino") Is Nothing Then
                ViewState("OrdenTermino") = 0
            End If
            Return ViewState("OrdenTermino")
        End Get
        Set(value As Integer)
            ViewState("OrdenTermino") = value
        End Set
    End Property

    Private Property ValoresTerminosMedioPagoTemporario() As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion
        Get
            Return ViewState("ValoresTerminosMedioPagoTemporario")
        End Get
        Set(value As ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion)
            ViewState("ValoresTerminosMedioPagoTemporario") = value
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
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboTipo()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboFormatos.Respuesta
        objRespuesta = objProxyUtilida.GetComboFormatos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlTipoFormato.AppendDataBoundItems = True
        ddlTipoFormato.Items.Clear()
        ddlTipoFormato.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlTipoFormato.DataTextField = "Descripcion"
        ddlTipoFormato.DataValueField = "Codigo"
        ddlTipoFormato.DataSource = objRespuesta.Formatos
        ddlTipoFormato.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboMascara()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboMascaras.Respuesta
        objPeticion.AplicaTerminosMediosPago = True

        objRespuesta = objProxyUtilida.GetComboMascaras(objPeticion)


        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlFormatoMascara.AppendDataBoundItems = True
        ddlFormatoMascara.Items.Clear()
        ddlFormatoMascara.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlFormatoMascara.DataTextField = "Descripcion"
        ddlFormatoMascara.DataValueField = "Codigo"
        ddlFormatoMascara.DataSource = objRespuesta.Mascaras
        ddlFormatoMascara.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 10/02/2009 Criado
    ''' </history>
    Public Sub PreencherComboAlgoritmo()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboAlgoritmos.Respuesta
        objPeticion.AplicaTerminosMediosPago = True

        objRespuesta = objProxyUtilida.GetComboAlgoritmos(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            MyBase.MostraMensagem(objRespuesta.MensajeError)
            Exit Sub
        End If

        ddlFormulaAlgoritmo.AppendDataBoundItems = True
        ddlFormulaAlgoritmo.Items.Clear()
        ddlFormulaAlgoritmo.Items.Add(New ListItem(Traduzir("014_ddl_selecione"), String.Empty))
        ddlFormulaAlgoritmo.DataTextField = "Descripcion"
        ddlFormulaAlgoritmo.DataValueField = "Codigo"
        ddlFormulaAlgoritmo.DataSource = objRespuesta.Algoritmos
        ddlFormulaAlgoritmo.DataBind()

    End Sub

    ''' <summary>
    ''' Método responsável por consumir o canal passado pela tela de Mantenimiento de Termino
    ''' Obs: O Termino só é consumido no modo Modificacion ou Consulta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeTerminoMedioPago()

        If Session("setTerminoMedioPago") IsNot Nothing Then

            TerminoMedioPago = DirectCast(Session("setTerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)

            'Campos Texto
            txtCodigoTerminoMedioPago.Text = TerminoMedioPago.Codigo
            txtCodigoTerminoMedioPago.ToolTip = TerminoMedioPago.Codigo

            txtDescricaoTerminoMedioPago.Text = TerminoMedioPago.Descripcion
            txtDescricaoTerminoMedioPago.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TerminoMedioPago.Descripcion, String.Empty)

            txtObservaciones.Text = TerminoMedioPago.Observacion
            txtObservaciones.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TerminoMedioPago.Observacion, String.Empty)

            txtLongitud.Text = TerminoMedioPago.Longitud
            txtLongitud.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TerminoMedioPago.Longitud, String.Empty)

            txtValorInicial.Text = TerminoMedioPago.ValorInicial
            txtValorInicial.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, TerminoMedioPago.ValorInicial, String.Empty)

            OrdenTermino = TerminoMedioPago.OrdenTermino
            ValoresTerminosMedioPagoTemporario = TerminoMedioPago.ValoresTermino

            'Campos CheckBox
            chkVigente.Checked = TerminoMedioPago.Vigente
            EsVigente = TerminoMedioPago.Vigente

            chkMostrarCodigo.Checked = TerminoMedioPago.MostrarCodigo
            'Campos DropDownList

            Dim itemSelecionado As ListItem
            'Tipo Formato
            itemSelecionado = ddlTipoFormato.Items.FindByValue(TerminoMedioPago.CodigoFormato)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoFormato.ToolTip = itemSelecionado.ToString
            End If

            'Formato Máscara
            itemSelecionado = ddlFormatoMascara.Items.FindByValue(TerminoMedioPago.CodigoMascara)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlFormatoMascara.ToolTip = itemSelecionado.ToString
            End If

            'Formula Algorítmo
            itemSelecionado = ddlFormulaAlgoritmo.Items.FindByValue(TerminoMedioPago.CodigoAlgoritmo)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlFormulaAlgoritmo.ToolTip = itemSelecionado.ToString
            End If


            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                DescricaoAtual = TerminoMedioPago.Descripcion
            End If

            If TerminoMedioPago.Vigente Then
                chkVigente.Enabled = False
            End If

            'Retira da sessão o objeto consumido
            Session("setTerminoMedioPago") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a coleção de terminos termporária passada pela página Mantenimineto de Medios de Pago(Pai)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeColecaoTerminosTemporario()

        If Session("colTerminosMedioPago") IsNot Nothing Then

            TerminosMediopagoTemporario = Session("colTerminosMedioPago")
            Session("colTerminosMedioPago") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Coleção de terminos temporária
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property TerminosMediopagoTemporario() As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion
        Get
            Return ViewState("TerminosMediopagoTemporario")
        End Get
        Set(value As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPagoColeccion)
            ViewState("TerminosMediopagoTemporario") = value
        End Set
    End Property

    ''' <summary>
    ''' Envia o objeto para a página de mantenimiento de medios de pago(Página Pai)
    ''' </summary>
    ''' <param name="pobjTermino"></param>    
    Private Sub EnviaObjetoTerminoParentForm(pobjTermino As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago)
        Session("objTerminoMedioPago") = pobjTermino
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
    ''' Seta a coleção de valores para a PopUp de Valores Posíveis
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetValoresTerminosMedioPagoColecaoPopUp()

        'Passa a coleção com o objeto temporario de Terminos
        Session("ValorTerminoMedioPagosTemporario") = ValoresTerminosMedioPagoTemporario

    End Sub

    ''' <summary>
    ''' Consome o ValorTerminoMedioPago passado pela PopUp de Mantenimiento de TerminosMedioPago. 
    ''' Obs: O ValorTerminoMedioPago só pode ser consumido no modo "Alta" ou "Modificacion"
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ConsomeValorTerminoMedioPago()

        If Session("objColValorTerminoMedioPago") IsNot Nothing Then

            Dim objColValorTerminoMedioPago As IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion
            objColValorTerminoMedioPago = DirectCast(Session("objColValorTerminoMedioPago"), IAC.ContractoServicio.MedioPago.GetMedioPagoDetail.ValorTerminoColeccion)

            ValoresTerminosMedioPagoTemporario = objColValorTerminoMedioPago

            Session("objColValorTerminoMedioPago") = Nothing
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
                If txtCodigoTerminoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTerminoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If txtDescricaoTerminoMedioPago.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoTerminoMedioPago.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoTerminoMedioPago.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTerminoMedioPago.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoTerminoMedioPago.IsValid = True
                End If

                'Verifica se a descrição do subcanal é obrigatório
                If ddlTipoFormato.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoFormatoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoFormatoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoFormato.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoFormatoObrigatorio.IsValid = True

                    If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) OrElse _
                       ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) OrElse _
                       ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) Then

                        'Verifica se a longitude do termino é obrigatório
                        If txtLongitud.Text.Trim.Equals(String.Empty) Then

                            strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                            csvLongitudeObrigatorio.IsValid = False

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        Else

                            csvLongitudeObrigatorio.IsValid = True
                            csvLongitudeValorInvalido.IsValid = True

                            If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_TEXTO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > ContractoServicio.Constantes.MAX_LONGITUDE) Then

                                strErro.Append(String.Format(Traduzir("014_msg_terminomediopago_longitudeinvalidaobligatorio"), 50) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitudeValorInvalido.IsValid = False

                            ElseIf ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_ENTERO) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > Len(Int32.MaxValue.ToString)) Then

                                strErro.Append(String.Format(Traduzir("014_msg_terminomediopago_longitudeinvalidaobligatorio"), Len(Int32.MaxValue.ToString)) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitudeValorInvalido.IsValid = False

                            ElseIf ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_DECIMAL) AndAlso (Convert.ToInt32(txtLongitud.Text) < 1 OrElse Convert.ToInt32(txtLongitud.Text) > (Len(Decimal.MaxValue.ToString) - 1)) Then

                                strErro.Append(String.Format(Traduzir("014_msg_terminomediopago_longitudeinvalidaobligatorio"), Len(Decimal.MaxValue.ToString) - 1) & Aplicacao.Util.Utilidad.LineBreak)
                                csvLongitudeValorInvalido.IsValid = False

                            End If

                            'Seta o foco no primeiro controle que deu erro
                            If SetarFocoControle AndAlso Not focoSetado Then
                                txtLongitud.Focus()
                                focoSetado = True
                            End If

                        End If

                    Else
                        csvLongitudeObrigatorio.IsValid = True
                    End If
                End If

                ''Verifica se a descrição do subcanal é obrigatório
                'If txtLongitud.Text.Trim.Equals(String.Empty) Then

                '    strErro.Append(csvLongitudeObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                '    csvLongitudeObrigatorio.IsValid = False

                '    'Seta o foco no primeiro controle que deu erro
                '    If SetarFocoControle AndAlso Not focoSetado Then
                '        txtLongitud.Focus()
                '        focoSetado = True
                '    End If

                'ElseIf Not IsNumeric(txtLongitud.Text) OrElse Convert.ToInt64(txtLongitud.Text) < 1 OrElse Convert.ToInt64(txtLongitud.Text) > Integer.MaxValue Then

                '    Dim strMsgTraduzida As String = csvLongitudeValorInvalido.ErrorMessage
                '    'Usando método extendido
                '    strMsgTraduzida = strMsgTraduzida.SetFormat(Integer.MaxValue.ToString)

                '    strErro.Append(strMsgTraduzida & Aplicacao.Util.Utilidad.LineBreak)
                '    csvLongitudeValorInvalido.IsValid = False

                '    'Seta o foco no primeiro controle que deu erro
                '    If SetarFocoControle AndAlso Not focoSetado Then
                '        txtLongitud.Focus()
                '        focoSetado = True
                '    End If

                'Else
                '    csvLongitudeObrigatorio.IsValid = True
                '    csvLongitudeValorInvalido.IsValid = True
                'End If

            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If ExisteCodigoTerminoMedioPago(txtCodigoTerminoMedioPago.Text.Trim()) Then

                strErro.Append(csvCodigoMedioPagoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoMedioPagoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTerminoMedioPago.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoMedioPagoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Desabilita e habilita o txtlongitud
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 01/02/2010 - Criado
    ''' </history>
    Public Sub HabilitaDesabilitaLongitud()

        If ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA) OrElse _
           ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_FECHA_HORA) OrElse _
           ddlTipoFormato.SelectedValue.Trim.Equals(ContractoServicio.Constantes.COD_FORMATO_BOLEANO) Then

            txtLongitud.Text = String.Empty
            txtLongitud.Enabled = False
        Else
            txtLongitud.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Verifica se o codigo do terminomediopago existe
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoTerminoMedioPago(codigo As String) As Boolean

        Dim objRespostaVerificarCodigoMedioPago As IAC.ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Respuesta
        Try
            If Acao = Utilidad.eAcao.Modificacion OrElse Acao = Utilidad.eAcao.Consulta OrElse Acao = Utilidad.eAcao.Baja Then
                Return False
            End If

            Dim objProxyMedioPago As New Comunicacion.ProxyMedioPago
            Dim objPeticionVerificarCodigoTerminoMedioPago As New IAC.ContractoServicio.MedioPago.VerificarCodigoTerminoMedioPago.Peticion

            'Verifica se o código do MedioPago existe no BD
            objPeticionVerificarCodigoTerminoMedioPago.Codigo = codigo
            objPeticionVerificarCodigoTerminoMedioPago.CodigoMedioPago = Request.QueryString("codMedioPago")

            objRespostaVerificarCodigoMedioPago = objProxyMedioPago.VerificarCodigoTerminoMedioPago(objPeticionVerificarCodigoTerminoMedioPago)

            If verificaCodigoTerminoMemoria(codigo) Then
                Return True
            Else
                Return False
            End If

            'If ControleErro.VerificaErro(objRespostaVerificarCodigoMedioPago.CodigoError, objRespostaVerificarCodigoMedioPago.NombreServidorBD, objRespostaVerificarCodigoMedioPago.MensajeError) Then

            '    If objRespostaVerificarCodigoMedioPago.Existe OrElse verificaCodigoTerminoMemoria(codigo) Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            'Else
            '    ControleErro.ShowError(objRespostaVerificarCodigoMedioPago.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
            '    Return False
            'End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Function

#End Region

    Private Sub btnConsomeValores_Click(sender As Object, e As EventArgs) Handles btnConsomeValores.Click
        Try
            ConsomeValorTerminoMedioPago()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
End Class