Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Busca de Mantenimiento de Tipo Punto Servicio
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 17/04/13 - Criado</history>
Public Class MaintenimentoTipoPuntoServicio
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoTipoPunto.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoPunto.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipPunto.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipPunto.ClientID & "').focus();return false;}} else {return true}; ")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTipoPunto.TabIndex = 1
        txtDescricaoTipPunto.TabIndex = 2
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do tipo Subcliente
                Dim strCodTipoPunto As String = Request.QueryString("codTipoPuntoServicio")

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

                If strCodTipoPunto <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodTipoPunto)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoTipPunto.Focus()
                Else
                    txtCodigoTipoPunto.Focus()
                    chkVigente.Checked = True
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
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.TituloPagina = Traduzir("033_Titulo_Resultado_Punto_servicio")
        lblCodTipoPunto.Text = Traduzir("033_codigo_punto_servicio")
        lblDescripcion.Text = Traduzir("033_descricao_punto_servicio")
        lblVigente.Text = Traduzir("033_vigente_punto_servicio")
        lblTituloTipoPunto.Text = Traduzir("033_Titulo_Resultado_Punto_servicio")
        csvCodigoTipoCliente.ErrorMessage = Traduzir("033_erro_codigo_obrigatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("033_erro_decricao_obrigatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("033_erro_codigo_existente")
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
    Protected Sub txtCodigoTipoPunto_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoTipoPunto.TextChanged
        If (String.IsNullOrEmpty(txtCodigoTipoPunto.Text)) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoTipoPonto(txtCodigoTipoPunto.Text.Trim) Then
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
    ''' [pgoncalves] 17/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxy As New Comunicacion.ProxyTipoPuntoServicio
            Dim objRespuesta As IAC.ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoPuntosServicio.setTiposPuntosServicio.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objPeticion.codTipoPuntoServicio = txtCodigoTipoPunto.Text.Trim
            objPeticion.desTipoPuntoServicio = txtDescricaoTipPunto.Text
            objPeticion.oidTipoPuntoServicio = OidTipoPonto
            objPeticion.bolMaquina = False
            objPeticion.bolMae = False

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoPontoServicio.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoPontoServicio.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxy.setTiposPuntosServicio(objPeticion)

            Dim url As String = "BusquedaTipoPuntoServicio.aspx"

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & _
                                                                                    "'); RedirecionaPaginaNormal('" & url & "');", True)
            Else
                If objRespuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuesta.MensajeError, False)
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
    ''' [pgoncalves] 17/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTipoPuntoServicio.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 17/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTipoPuntoServicio.aspx", False)
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
    ''' <param name="codTipoPunto"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codTipoPunto As String)

        Dim objPeticion As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion
        Dim objRespuesta As New ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoPuntoServicio

        objPeticion.codTipoPuntoServicio = codTipoPunto
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposPuntosServicio(objPeticion)
        TipoPontoServicio = objRespuesta.TipoPuntoServicio(0)

        If objRespuesta.TipoPuntoServicio.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoPunto.Text = objRespuesta.TipoPuntoServicio(0).codTipoPuntoServicio
            txtCodigoTipoPunto.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoPuntoServicio(0).codTipoPuntoServicio, String.Empty)

            txtDescricaoTipPunto.Text = objRespuesta.TipoPuntoServicio(0).desTipoPuntoServicio
            txtDescricaoTipPunto.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoPuntoServicio(0).desTipoPuntoServicio, String.Empty)

            If objRespuesta.TipoPuntoServicio(0).bolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            chkVigente.Checked = objRespuesta.TipoPuntoServicio(0).bolActivo

            EsVigente = objRespuesta.TipoPuntoServicio(0).bolActivo
            OidTipoPonto = objRespuesta.TipoPuntoServicio(0).oidTipoPuntoServicio

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
                If txtCodigoTipoPunto.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoCliente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoCliente.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoPunto.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoCliente.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipPunto.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipPunto.Focus()
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
                    txtCodigoTipoPunto.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do tipo Ponto Serviço já existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 17/04/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoTipoPonto(codigo As String) As Boolean

        Try

            Dim objPeticion As New IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoPuntoServicio

            objPeticion.codTipoPuntoServicio = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposPuntosServicio(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoPuntoServicio.Count > 0 Then
                    Return True
                Else
                    Return False
                    Master.ControleErro.ShowError(objRespuesta.MensajeError)
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()
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
                txtCodigoTipoPunto.Enabled = False
                txtDescricaoTipPunto.Enabled = False
                chkVigente.Enabled = False
                btnGrabar.Visible = False                '1

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoTipoPunto.Enabled = False
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

        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        If Not ParametroMantenimientoClientesDivisasPorPantalla Then
            txtDescricaoTipPunto.Enabled = False
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

    Private Property OidTipoPonto As String
        Get
            Return ViewState("OidTipoPonto")
        End Get
        Set(value As String)
            ViewState("OidTipoPonto") = value
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

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property TipoPontoServicio As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio
        Get
            Return DirectCast(ViewState("TipoPontoServicio"), ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio)
        End Get
        Set(value As ContractoServicio.TipoPuntosServicio.getTiposPuntosServicio.TipoPuntosServicio)
            ViewState("TipoPontoServicio") = value
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

#End Region

End Class