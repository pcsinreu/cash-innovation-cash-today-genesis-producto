Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Busca de Mantenimiento de Tipo Subclientes 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 12/04/13 - Criado</history>
Public Class ManteniementoTipoSubcliente
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoTipoSubcliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoSubcliente.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipSubcliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipSubcliente.ClientID & "').focus();return false;}} else {return true}; ")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTipoSubcliente.TabIndex = 1
        txtDescricaoTipSubcliente.TabIndex = 2
        chkVigente.TabIndex = 3

        btnGrabar.TabIndex = 5
        btnCancelar.TabIndex = 6
        btnVolver.TabIndex = 7

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do tipo Subcliente
                Dim strCodTipoSubCliente As String = Request.QueryString("codTipoSubCliente")

                'Possíveis Ações passadas pela página BusquedaTipoSubcliente:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                If strCodTipoSubCliente <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodTipoSubCliente)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoTipSubcliente.Focus()
                Else
                    txtCodigoTipoSubcliente.Focus()
                    chkVigente.Checked = True
                End If

                'Trata o foco dos campos
                TrataFoco()

            End If

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
        Master.TituloPagina = Traduzir("032_TipoSubCliente")
        lblCodTipoSubcliente.Text = Traduzir("032_codigo_tipoSubCliente")
        lblDescripcion.Text = Traduzir("032_descricao_tipoSub")
        lblVigente.Text = Traduzir("032_Vigente")
        lblTituloTipoCliente.Text = Traduzir("032_TipoSubCliente")
        csvCodigoTipoCliente.ErrorMessage = Traduzir("032_lbl_codigo_Tbcliente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("031_lbl_decricao_Tbcliente")
        csvCodigoExistente.ErrorMessage = Traduzir("032_msg_codigTipoSubExistente")
    End Sub

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Clique do botão Volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    ''' <summary>
    ''' Clique do botão Cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    ''' <summary>
    ''' Clique do botão Grabar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    'Verifica se o codigo existe na base de dados
    Protected Sub txtCodigoTipoSubcliente_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoTipoSubcliente.TextChanged

        If (String.IsNullOrEmpty(txtCodigoTipoSubcliente.Text)) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoTipoSubCliente(txtCodigoTipoSubcliente.Text.Trim) Then
                CodigoExistente = True
            Else
                CodigoExistente = False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxy As New Comunicacion.ProxyTipoSubCliente
            Dim objRespuesta As IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.setTiposSubclientes.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objPeticion.codTipoSubcliente = txtCodigoTipoSubcliente.Text.Trim
            objPeticion.desTipoSubcliente = txtDescricaoTipSubcliente.Text
            objPeticion.oidTipoSubcliente = OidTipoSubCliente

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoSubCliente.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoSubCliente.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxy.setTiposSubclientes(objPeticion)

            Dim url As String = "BusquedaTipoSubcliente.aspx"

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
            Else
                If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
                    TrataFoco()
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTipoSubcliente.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTipoSubcliente.aspx", False)
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
    ''' Carrega os dados do  item selecionado na grade anterior.
    ''' </summary>
    ''' <param name="codigo"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codigo As String)

        Dim objPeticion As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoSubCliente

        objPeticion.codTipoSubcliente = codigo
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposSubclientes(objPeticion)

        TipoSubCliente = objRespuesta.TipoSubCliente(0)

        If objRespuesta.TipoSubCliente.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoSubcliente.Text = objRespuesta.TipoSubCliente(0).codTipoSubcliente
            txtCodigoTipoSubcliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSubCliente(0).codTipoSubcliente, String.Empty)

            txtDescricaoTipSubcliente.Text = objRespuesta.TipoSubCliente(0).desTipoSubcliente
            txtDescricaoTipSubcliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoSubCliente(0).desTipoSubcliente, String.Empty)

            If objRespuesta.TipoSubCliente(0).bolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            chkVigente.Checked = objRespuesta.TipoSubCliente(0).bolActivo

            EsVigente = objRespuesta.TipoSubCliente(0).bolActivo
            OidTipoSubCliente = objRespuesta.TipoSubCliente(0).oidTipoSubcliente

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

            'Verifica se o campo é obrigatório quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o codigo da planta foi enviado
                If txtCodigoTipoSubcliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoCliente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoCliente.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoSubcliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoCliente.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipSubcliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipSubcliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoTipoSubcliente.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do tipo Subcliente ja existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/04/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoTipoSubCliente(codigo As String) As Boolean

        Try

            Dim objPeticion As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoSubCliente

            objPeticion.codTipoSubcliente = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposSubclientes(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoSubCliente.Count > 0 Then
                    Return True
                End If
            Else
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Prosegur.Genesis.Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(o) o.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        If ParametroMantenimientoClientesDivisasPorPantalla Then
            Select Case MyBase.Acao

                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    btnGrabar.Visible = True        '1
                    btnCancelar.Visible = True      '2
                    btnVolver.Visible = False '3
                    chkVigente.Visible = False
                    chkVigente.Checked = True
                    btnGrabar.Habilitado = True
                    lblVigente.Visible = False
                Case Aplicacao.Util.Utilidad.eAcao.Baja
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    btnCancelar.Visible = False              '2
                    btnVolver.Visible = True                 '3
                    txtCodigoTipoSubcliente.Enabled = False
                    txtDescricaoTipSubcliente.Enabled = False
                    chkVigente.Enabled = False
                    btnGrabar.Visible = False                '1

                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoTipoSubcliente.Enabled = False
                    btnGrabar.Visible = True               '1
                    btnCancelar.Visible = True             '2
                    btnVolver.Visible = False              '3
                    btnGrabar.Habilitado = True

                Case Aplicacao.Util.Utilidad.eAcao.NoAction
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Inicial
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.Busca
                    'Controles
                Case Aplicacao.Util.Utilidad.eAcao.NoAction

                Case Aplicacao.Util.Utilidad.eAcao.Erro
                    btnGrabar.Visible = False        '2
                    btnCancelar.Visible = False      '3                
                    btnVolver.Visible = True         '7

            End Select
        Else
            Select Case MyBase.Acao

                Case Aplicacao.Util.Utilidad.eAcao.Alta
                    btnGrabar.Visible = False        '1
                    btnCancelar.Visible = True      '2
                    btnVolver.Visible = False '3
                    chkVigente.Visible = False
                    chkVigente.Checked = True
                    btnGrabar.Habilitado = False
                    lblVigente.Visible = False
                Case Aplicacao.Util.Utilidad.eAcao.Consulta
                    btnCancelar.Visible = False              '2
                    btnVolver.Visible = True                 '3
                    txtCodigoTipoSubcliente.Enabled = False
                    txtDescricaoTipSubcliente.Enabled = False
                    chkVigente.Enabled = False
                    btnGrabar.Visible = False                '1

                Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                    txtCodigoTipoSubcliente.Enabled = False
                    btnGrabar.Visible = False               '1
                    btnCancelar.Visible = True             '2
                    btnVolver.Visible = False              '3
                    btnGrabar.Habilitado = False

                Case Aplicacao.Util.Utilidad.eAcao.Erro
                    btnGrabar.Visible = False        '2
                    btnCancelar.Visible = False      '3                
                    btnVolver.Visible = True         '7

            End Select
        End If
        

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property OidTipoSubCliente As String
        Get
            Return ViewState("OidTipoSubCliente")
        End Get
        Set(value As String)
            ViewState("OidTipoSubCliente") = value
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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property TipoSubCliente As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente
        Get
            Return DirectCast(ViewState("TipoSubCliente"), ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente)
        End Get
        Set(value As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubCliente)
            ViewState("TipoSubCliente") = value
        End Set
    End Property

#End Region
   
End Class