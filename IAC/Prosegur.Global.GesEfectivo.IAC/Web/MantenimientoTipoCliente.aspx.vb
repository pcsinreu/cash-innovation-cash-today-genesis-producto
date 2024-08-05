Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

''' <summary>
''' Página de Busca de Mantenimiento de Tipo Clientes 
''' </summary>
''' <remarks></remarks>
''' <history>[pgoncalves] 10/04/13 - Criado</history>
''' 
Public Class MantenimientoTipoCliente
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()
        'Adicionar scripts na página
        txtCodigoTipoCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtCodigoTipoCliente.ClientID & "').focus();return false;}} else {return true}; ")
        txtDescricaoTipoCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescricaoTipoCliente.ClientID & "').focus();return false;}} else {return true}; ")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)
    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoTipoCliente.TabIndex = 1
        txtDescricaoTipoCliente.TabIndex = 2
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
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código do planta
                Dim strCodTipoCliente As String = Request.QueryString("codTipoCliente")

                'Possíveis Ações passadas pela página BusquedaDelegaciones:
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

                If strCodTipoCliente <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(strCodTipoCliente)
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                    OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescricaoTipoCliente.Focus()
                Else
                    txtCodigoTipoCliente.Focus()
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
        Master.TituloPagina = Traduzir("031_lbl_titulo_pagina")
        lblCodTipoCliente.Text = Traduzir("031_lbl_codigo_tipoCliente")
        lblDescripcion.Text = Traduzir("031_lbl_descricao_tipo")
        lblVigente.Text = Traduzir("031_lbl_Tipo_vigente")
        lblTituloTipoCliente.Text = Traduzir("031_lbl_tipo_cliente")
        csvCodigoTipoCliente.ErrorMessage = Traduzir("031_lbl_codigo_cliente")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("031_lbl_decricao_cliente")
        csvCodigoExistente.ErrorMessage = Traduzir("031_msg_codigTipoExistente")
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

    Protected Sub txtCodigoTipoCliente_TextChanged(sender As Object, e As EventArgs) Handles txtCodigoTipoCliente.TextChanged

        If (String.IsNullOrEmpty(txtCodigoTipoCliente.Text)) Then
            Exit Sub
        End If

        Try
            If ExisteCodigoCliente(txtCodigoTipoCliente.Text.Trim) Then
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
    ''' [pgoncalves] 10/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            Dim objProxyTipoCliente As New Comunicacion.ProxyTipoCliente
            Dim objRespuesta As IAC.ContractoServicio.TipoCliente.SetTiposClientes.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.TipoCliente.SetTiposClientes.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            objPeticion.codTipoCliente = txtCodigoTipoCliente.Text.Trim
            objPeticion.desTipoCliente = txtDescricaoTipoCliente.Text
            objPeticion.oidTipoCliente = OidTipoCliente

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = TipoCliente.gmtCreacion
                objPeticion.desUsuarioCreacion = TipoCliente.desUsuarioCreacion
            End If

            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario

            objRespuesta = objProxyTipoCliente.setTiposClientes(objPeticion)

            Dim url As String = "BusquedaTipoCliente.aspx"

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
    ''' [pgoncalves] 10/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaTipoCliente.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 10/04/2013 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaTipoCliente.aspx", False)
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
    ''' <param name="codTipoCliente"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(codTipoCliente As String)

        Dim objPeticion As New ContractoServicio.TipoCliente.GetTiposClientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoCliente.GetTiposClientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoCliente

        objPeticion.codTipoCliente = codTipoCliente
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxy.getTiposClientes(objPeticion)
        TipoCliente = objRespuesta.TipoCliente(0)

        If objRespuesta.TipoCliente.Count > 0 Then

            'Preenche os controles do formulario
            txtCodigoTipoCliente.Text = objRespuesta.TipoCliente(0).codTipoCliente
            txtCodigoTipoCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoCliente(0).codTipoCliente, String.Empty)

            txtDescricaoTipoCliente.Text = objRespuesta.TipoCliente(0).desTipoCliente
            txtDescricaoTipoCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuesta.TipoCliente(0).desTipoCliente, String.Empty)

            If objRespuesta.TipoCliente(0).bolActivo Then
                chkVigente.Enabled = False
            Else
                chkVigente.Enabled = True
            End If

            chkVigente.Checked = objRespuesta.TipoCliente(0).bolActivo

            EsVigente = objRespuesta.TipoCliente(0).bolActivo
            OidTipoCliente = objRespuesta.TipoCliente(0).oidTipoCliente

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
                If txtCodigoTipoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoTipoCliente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoTipoCliente.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoTipoCliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoTipoCliente.IsValid = True
                End If

                'Verifica se a descrição da planta foi enviado
                If txtDescricaoTipoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoTipoCliente.Focus()
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
                    txtCodigoTipoCliente.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se o código do tipo cliente ja existe.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 10/04/2013 - Criado
    ''' </history>
    Private Function ExisteCodigoCliente(codigo As String) As Boolean

        Try

            Dim objPeticion As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.TipoCliente.GetTiposClientes.Respuesta
            Dim objProxy As New Comunicacion.ProxyTipoCliente

            objPeticion.codTipoCliente = codigo
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxy.getTiposClientes(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.TipoCliente.Count > 0 Then
                    Return True
                End If
            Else
                Return False
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
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
                txtCodigoTipoCliente.Enabled = False
                txtDescricaoTipoCliente.Enabled = False
                chkVigente.Enabled = False
                btnGrabar.Visible = False                '1

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoTipoCliente.Enabled = False
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

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property OidTipoCliente As String
        Get
            Return ViewState("OidTipoCliente")
        End Get
        Set(value As String)
            ViewState("OidTipoCliente") = value
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

    Private Property TipoCliente As ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente
        Get
            Return DirectCast(ViewState("TipoCliente"), ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente)
        End Get
        Set(value As ContractoServicio.TipoCliente.GetTiposClientes.TipoCliente)
            ViewState("TipoCliente") = value
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