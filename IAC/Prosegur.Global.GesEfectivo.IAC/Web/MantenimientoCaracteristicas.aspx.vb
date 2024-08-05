Imports Prosegur.Genesis
Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Tela de MantenimientoCaracteristicas 
''' </summary>
''' <remarks></remarks>
''' <history>[carlos.bomtempo]    20/05/09 - Criado</history>
Partial Public Class MantenimientoCaracteristicas
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        txtCodigoCaracteristica.Attributes.Add("onkeydown", "return TrataEnterTab('" & txtDescricao.ClientID & "');")
        txtDescricao.Attributes.Add("onkeydown", "return TrataEnterTab('" & txtCodigoConteo.ClientID & "');")
        txtCodigoConteo.Attributes.Add("onkeydown", "return TrataEnterTab('" & txtObservaciones.ClientID & "');")
        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigoCaracteristica.TabIndex = 1
        txtDescricao.TabIndex = 2
        txtCodigoConteo.TabIndex = 3
        txtObservaciones.TabIndex = 4
        chkVigente.TabIndex = 5
        btnGrabar.TabIndex = 6
        btnCancelar.TabIndex = 7

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS
        
    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                'Recebe o código da Caracteristica
                Dim strCodCaracteristica As String = Request.QueryString("codigoCaracteristica")

                'Possíveis Ações passadas pela página BusquedaCaracteristicas:
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
                    'Caracteristica passada na tela de "Mantenimiento de Caracteristicas"
                    GetCaracteristicaByCodigo(strCodCaracteristica)
                    txtDescricao.Focus()
                Else
                    txtCodigoCaracteristica.Focus()
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

        Me.Page.Title = Traduzir("020_titulo_mantenimiento_caracteristicas")
        Master.TituloPagina = Traduzir("020_titulo_mantenimiento_caracteristicas")
        lblTituloCaracteristicas.Text = Traduzir("020_lbl_subtitulo_mantenimiento_caracteristicas")

        lblCodigoCaracteristica.Text = Traduzir("020_lbl_codigo_caracteristica")
        lblDescricao.Text = Traduzir("020_lbl_descricao_caracteristica")
        lblCodigoConteo.Text = Traduzir("020_lbl_codigo_conteo")
        lblObservaciones.Text = Traduzir("020_lbl_observaciones")
        lblVigente.Text = Traduzir("020_lbl_vigente")

        csvCodigoCaracteristicaObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_obligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_descripcion_obligatorio")
        csvCodigoConteoObrigatorio.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_conteo_obligatorio")
        csvCodigoCaracteristica.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_existente")
        csvDescripcion.ErrorMessage = Traduzir("020_msg_caracteristica_descripcion_existente")
        csvCodigoConteo.ErrorMessage = Traduzir("020_msg_caracteristica_codigo_conteo_existente")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Property Caracteristica() As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica
        Get
            Return DirectCast(ViewState("Caracteristica"), IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica)
        End Get
        Set(value As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Caracteristica)
            ViewState("Caracteristica") = value
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

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
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

    Private Property CodigoConteoExistente() As Boolean
        Get
            Return ViewState("CodigoConteoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoConteoExistente") = value
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

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/BusquedaCaracteristicas.aspx", False)
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/BusquedaCaracteristicas.aspx", False)
    End Sub

#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do botão gravar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' <history>[carlos.bomtempo]    20/05/09 - Criado</history>
    ''' </history>
    Public Sub ExecutarGrabar()
        Try

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

                Dim strErro As New Text.StringBuilder(String.Empty)

                ValidarCamposObrigatorios = True

                If MontaMensagensErro(True).Length > 0 Then
                    Exit Sub
                End If

                Dim objCaracteristica As New IAC.ContractoServicio.Caracteristica.SetCaracteristica.Caracteristica

                objCaracteristica.Codigo = txtCodigoCaracteristica.Text.Trim
                objCaracteristica.Descripcion = txtDescricao.Text.Trim
                objCaracteristica.CodigoConteo = txtCodigoConteo.Text.Trim
                objCaracteristica.Observaciones = txtObservaciones.Text

                If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    objCaracteristica.Vigente = True
                Else
                    objCaracteristica.Vigente = chkVigente.Checked
                End If

                Dim ProxyCaractaristica As New Comunicacion.ProxyCaracteristica()
                Dim PeticionCaractaristica As New IAC.ContractoServicio.Caracteristica.SetCaracteristica.Peticion()
                Dim RespuestaCaractaristica As IAC.ContractoServicio.Caracteristica.SetCaracteristica.Respuesta

                PeticionCaractaristica.Caracteristicas = New IAC.ContractoServicio.Caracteristica.SetCaracteristica.CaracteristicaColeccion()
                PeticionCaractaristica.Caracteristicas.Add(objCaracteristica)
                PeticionCaractaristica.CodigoUsuario = MyBase.LoginUsuario

                RespuestaCaractaristica = ProxyCaractaristica.SetCaracteristica(PeticionCaractaristica)

                If Master.ControleErro.VerificaErro(RespuestaCaractaristica.CodigoError, RespuestaCaractaristica.NombreServidorBD, RespuestaCaractaristica.MensajeError) AndAlso _
                    Master.ControleErro.VerificaErro(RespuestaCaractaristica.Caracteristicas(0).CodigoError, RespuestaCaractaristica.NombreServidorBD, RespuestaCaractaristica.Caracteristicas(0).MensajeError) Then

                    Response.Redirect("~/BusquedaCaracteristicas.aspx", False)

                Else

                    If RespuestaCaractaristica.Caracteristicas IsNot Nothing AndAlso _
                        RespuestaCaractaristica.Caracteristicas.Count > 0 Then

                        If RespuestaCaractaristica.Caracteristicas(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                            ' mostrar o erro pro usuário
                            Master.ControleErro.ShowError(RespuestaCaractaristica.Caracteristicas(0).MensajeError, False)
                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region
#End Region

#Region "[EVENTOS TEXTBOX]"

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Característica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoCaracteristica_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoCaracteristica.TextChanged
        Try
            If ExisteCodigoCaracteristica(txtCodigoCaracteristica.Text) Then
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
    ''' Evento de mudança de texto do campo Descrição
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtDescricao_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescricao.TextChanged
        Try
            If ExisteDescricaoCaracteristica(txtDescricao.Text) Then
                DescricaoExistente = True
            Else
                DescricaoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de mudança de texto do campo Código Conteo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtCodigoConteo_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoConteo.TextChanged
        Try
            If ExisteCodigoConteoCaracteristica(txtCodigoConteo.Text) Then
                CodigoConteoExistente = True
            Else
                CodigoConteoExistente = False
            End If

            Threading.Thread.Sleep(100)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        ' campo "código conteo" é protegido
        txtCodigoConteo.Enabled = False

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                txtCodigoCaracteristica.Enabled = True
                txtDescricao.Enabled = True
                txtObservaciones.Enabled = True
                lblVigente.Visible = False
                chkVigente.Visible = False
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnGrabar.Visible = True
                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                txtCodigoCaracteristica.Enabled = False
                txtDescricao.Enabled = False
                txtObservaciones.Enabled = False
                lblVigente.Visible = True
                chkVigente.Visible = True
                chkVigente.Enabled = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnGrabar.Visible = False
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoCaracteristica.Enabled = False
                txtDescricao.Enabled = True
                txtObservaciones.Enabled = True
                chkVigente.Visible = True
                btnCancelar.Visible = True
                lblVigente.Visible = True
                btnGrabar.Visible = True
                btnGrabar.Habilitado = True
                btnVolver.Visible = False

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
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

#End Region

#Region "[MÉTODOS]"

    Public Sub GetCaracteristicaByCodigo(codigo As String)

        Dim objProxyCaracteristica As New Comunicacion.ProxyCaracteristica
        Dim objPeticionCaracteristica As New IAC.ContractoServicio.Caracteristica.GetCaracteristica.Peticion
        Dim objRespuestaCaracteristica As IAC.ContractoServicio.Caracteristica.GetCaracteristica.Respuesta

        objPeticionCaracteristica.Codigo = codigo.ToUpper
        objRespuestaCaracteristica = objProxyCaracteristica.GetCaracteristica(objPeticionCaracteristica)

        If objRespuestaCaracteristica.Caracteristicas.Count > 0 Then
            Caracteristica = objRespuestaCaracteristica.Caracteristicas(0)
            txtCodigoCaracteristica.Text = objRespuestaCaracteristica.Caracteristicas(0).Codigo
            txtCodigoCaracteristica.ToolTip = objRespuestaCaracteristica.Caracteristicas(0).Codigo

            txtCodigoConteo.Text = objRespuestaCaracteristica.Caracteristicas(0).CodigoConteo
            txtCodigoConteo.ToolTip = objRespuestaCaracteristica.Caracteristicas(0).CodigoConteo

            txtDescricao.Text = objRespuestaCaracteristica.Caracteristicas(0).Descripcion
            txtDescricao.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objRespuestaCaracteristica.Caracteristicas(0).Descripcion, String.Empty)

            txtObservaciones.Text = objRespuestaCaracteristica.Caracteristicas(0).Observaciones
            chkVigente.Checked = objRespuestaCaracteristica.Caracteristicas(0).Vigente
            If chkVigente.Checked Then
                chkVigente.Enabled = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 01/07/2009 - Criado
    ''' </history>
    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If txtCodigoCaracteristica.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoCaracteristicaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoCaracteristicaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCaracteristica.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoCaracteristicaObrigatorio.IsValid = True
                End If

                'Verifica se a descrição do canal é obrigatório
                If txtDescricao.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricao.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                'Verifica se o codigo conteo foi preenchido
                If txtCodigoConteo.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoConteoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoConteoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoConteo.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodigoConteoObrigatorio.IsValid = True
                End If


            End If

            'Validações constantes durante o ciclo de vida de execução da página

            'Verifica se o código existe
            If CodigoExistente Then

                strErro.Append(csvCodigoCaracteristica.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoCaracteristica.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoCaracteristica.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoCaracteristica.IsValid = True
            End If

            'Verifica se a descrição existe
            If DescricaoExistente Then

                strErro.Append(csvDescripcion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescripcion.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescricao.Focus()
                    focoSetado = True
                End If

            Else
                csvDescripcion.IsValid = True
            End If

            'Verifica se o codigo conteo existe.
            If CodigoConteoExistente Then

                strErro.Append(csvCodigoConteo.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoConteo.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoConteo.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoConteo.IsValid = True
            End If


        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Informa se a descrição da caracteristica já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/06/2009 - Criado
    ''' </history>
    Private Function ExisteDescricaoCaracteristica(descricao As String) As Boolean

        Dim objRespostaVerificarDescripcionCaracteristica As IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Respuesta
        Try
            If Not String.IsNullOrEmpty(descricao.Trim) Then
                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    If descricao.Trim.Equals(Caracteristica.Descripcion) Then
                        DescricaoExistente = False
                        Return False
                    End If
                End If

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarDescripcionCaracteristica As New IAC.ContractoServicio.Utilidad.VerificarDescripcionCaracteristica.Peticion

                'Verifica se a descrição existe no BD
                objPeticionVerificarDescripcionCaracteristica.Descripcion = descricao.Trim
                objRespostaVerificarDescripcionCaracteristica = objProxyUtilidad.VerificarDescripcionCaracteristica(objPeticionVerificarDescripcionCaracteristica)

                If Master.ControleErro.VerificaErro(objRespostaVerificarDescripcionCaracteristica.CodigoError, objRespostaVerificarDescripcionCaracteristica.NombreServidorBD, objRespostaVerificarDescripcionCaracteristica.MensajeError) Then
                    Return objRespostaVerificarDescripcionCaracteristica.Existe
                Else
                    Master.ControleErro.ShowError(objRespostaVerificarDescripcionCaracteristica.MensajeError) 'TODO: Exibe a mensagem de erro. Apenas para Debug. Retirar para Release.
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

    ''' <summary>
    ''' Informa se o codigo conteo da caracteristica já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 05/06/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoConteoCaracteristica(codigo As String) As Boolean

        Dim objRespostaVerificarCodigoConteo As IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Respuesta
        Try
            If Not String.IsNullOrEmpty(codigo) Then

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    If codigo.Trim.Equals(Caracteristica.CodigoConteo) Then
                        CodigoConteoExistente = False
                        Return False
                    End If
                End If

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticionVerificarCodigoConteo As New IAC.ContractoServicio.Utilidad.VerificarCodigoConteoCaracteristica.Peticion
                Dim strCodigoConteo As String = txtCodigoConteo.Text.Trim

                'Verifica se a descrição existe no BD
                objPeticionVerificarCodigoConteo.Codigo = codigo
                objRespostaVerificarCodigoConteo = objProxyUtilidad.VerificarCodigoConteoCaracteristica(objPeticionVerificarCodigoConteo)

                If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoConteo.CodigoError, objRespostaVerificarCodigoConteo.NombreServidorBD, objRespostaVerificarCodigoConteo.MensajeError) Then
                    Return objRespostaVerificarCodigoConteo.Existe
                Else
                    Master.ControleErro.ShowError(objRespostaVerificarCodigoConteo.MensajeError)
                    Return False
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Function

    ''' <summary>
    ''' Informa se o código da caracteristica já existe na base de dados.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 01/07/2009 - Criado
    ''' </history>
    Private Function ExisteCodigoCaracteristica(codigoCaracteristica As String) As Boolean

        Dim objRespostaVerificarCodigoCaracteristica As IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Respuesta
        Try

            Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad()
            Dim objPeticionVerificarCodigoCaracteristica As New IAC.ContractoServicio.Utilidad.VerificarCodigoCaracteristica.Peticion

            'Verifica se o código da caracteristica existe no BD
            objPeticionVerificarCodigoCaracteristica.Codigo = codigoCaracteristica
            objRespostaVerificarCodigoCaracteristica = objProxyUtilidad.VerificarCodigoCaracteristica(objPeticionVerificarCodigoCaracteristica)

            If Master.ControleErro.VerificaErro(objRespostaVerificarCodigoCaracteristica.CodigoError, objRespostaVerificarCodigoCaracteristica.NombreServidorBD, objRespostaVerificarCodigoCaracteristica.MensajeError) Then
                Return objRespostaVerificarCodigoCaracteristica.Existe
            Else
                Master.ControleErro.ShowError(objRespostaVerificarCodigoCaracteristica.MensajeError)
                Return False
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Function

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

#End Region

End Class