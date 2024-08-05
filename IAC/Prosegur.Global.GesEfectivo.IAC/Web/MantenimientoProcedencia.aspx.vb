Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

''' <summary>
''' Página de Gerenciamento de Procedencia 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoProcedencia
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        'Seta o foco para o proximo controle quando presciona o enter.

        ddlTipoSubCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & ddlTipoPuntoServicio.ClientID & "').focus();return false;}} else {return true}; ")
        ddlTipoSubCliente.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & ddlTipoProcedencia.ClientID & "').focus();return false;}} else {return true}; ")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

        'Validação Clique Botões do Grid 
        Dim s As String = String.Empty

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        ddlTipoSubCliente.TabIndex = 1
        ddlTipoPuntoServicio.TabIndex = 2
        ddlTipoProcedencia.TabIndex = 3
        btnGrabar.TabIndex = 10
        btnCancelar.TabIndex = 11
        btnVolver.TabIndex = 12

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS

    End Sub

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

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                OidProcedencia = Request.QueryString("OidProcedencia")

                PreencherComboTipoSubCliente()
                PreencherComboTipoPuntoServicio()
                PreencherComboTipoProcedencia()

                If OidProcedencia <> String.Empty Then
                    'Estado Inicial dos control
                    CarregaDados(OidProcedencia)
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    ddlTipoSubCliente.Focus()
                End If

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

        Master.TituloPagina = Traduzir("055_titulo_mantenimiento_procedencia")
        lblTituloProcedencia.Text = Traduzir("055_titulo_procedencia")
        lblTipoSubCliente.Text = Traduzir("055_lbl_tiposubcliente")
        lblTipoPuntoServicio.Text = Traduzir("055_lbl_tipopuntoservicio")
        lblTipoProcedencia.Text = Traduzir("055_lbl_tipoprocedencia")
        lblVigente.Text = Traduzir("055_lbl_vigencia")

        csvTipoSubClienteObrigatorio.ErrorMessage = Traduzir("055_msg_tiposubclienteobligatorio")
        csvTipoPuntoServicioObrigatorio.ErrorMessage = Traduzir("055_msg_tipopuntoservicioobligatorio")
        csvTipoProcedenciaObrigatorio.ErrorMessage = Traduzir("055_msg_tipoprocedenciaobligatorio")

    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

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
        'Threading.Thread.Sleep(2000)
        ExecutarGravar()
    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    ''' <summary>
    ''' Função do Botão Grabar 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub ExecutarGravar()

        Try

            Dim objProxyProcedencia As New Comunicacion.ProxyProcedencia
            Dim objPeticion As New IAC.ContractoServicio.Procedencia.SetProcedencia.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Procedencia.SetProcedencia.Respuesta
            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            Dim objProcedencia As New IAC.ContractoServicio.Procedencia.SetProcedencia.Procedencia
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = eAcaoEspecifica.AltaConRegistro Then
                objProcedencia.Activo = True
            Else
                objProcedencia.Activo = chkVigente.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigente.Checked

            ' Preenche o objeto Procedencia
            objProcedencia.OidProcedencia = OidProcedencia
            objProcedencia.OidTipoSubCliente = TiposSubCliente.Where(Function(t) t.Codigo = ddlTipoSubCliente.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            objProcedencia.OidTipoPuntoServicio = TiposPuntoServicio.Where(Function(t) t.Codigo = ddlTipoPuntoServicio.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            objProcedencia.OidTipoProcedencia = TiposProcedencia.Where(Function(t) t.Codigo = ddlTipoProcedencia.SelectedValue).Select(Function(t) t.Oid).FirstOrDefault()
            If String.IsNullOrEmpty(OidProcedencia) Then
                objProcedencia.FyhCreacion = DateTime.Now
                objProcedencia.CodigoUsuarioCreacion = MyBase.LoginUsuario
            Else
                objProcedencia.FyhCreacion = Procedencia.First().GmtCreacion
                objProcedencia.CodigoUsuarioCreacion = Procedencia.First().DesUsuarioCreacion
            End If

            objProcedencia.FyhActualizacion = DateTime.Now
            objProcedencia.CodigoUsuarioActualizacion = MyBase.LoginUsuario

            objPeticion.Procedencia = objProcedencia

            ' Carrega a petição para verificar se a Procedencia já existe
            Dim objPeticionV As New IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Peticion
            objPeticionV.OidProcedencia = OidProcedencia
            objPeticionV.OidTipoSubCliente = objProcedencia.OidTipoSubCliente
            objPeticionV.OidTipoPuntoServicio = objProcedencia.OidTipoPuntoServicio
            objPeticionV.OidTipoProcedencia = objProcedencia.OidTipoProcedencia

            ' Verifica se a procedencia já existe
            Dim objRespuestaV As IAC.ContractoServicio.Procedencia.VerificarExisteProcedencia.Respuesta = objProxyProcedencia.VerificaExisteProcedencia(objPeticionV)

            ' Verifica se aconteceu algum erro na execução ao executar a verificação
            If Master.ControleErro.VerificaErro(objRespuestaV.CodigoError, objRespuestaV.NombreServidorBD, objRespuestaV.MensajeError) Then

                ' Se a procedencia já existe
                If Not objRespuestaV.Existe Then

                    ' Se não foi informado o código de Procedencia
                    If String.IsNullOrEmpty(OidProcedencia) Then

                        objRespuesta = objProxyProcedencia.AltaProcedencia(objPeticion)

                        'Define a ação de busca somente se houve retorno de canais
                        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                            Response.Redirect("~/BusquedaProcedencias.aspx", False)
                        End If
                    Else
                        objRespuesta = objProxyProcedencia.ActualizaProcedencia(objPeticion)

                        'Define a ação de busca somente se houve retorno de canais
                        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                            Response.Redirect("~/BusquedaProcedencias.aspx", False)
                        End If

                    End If

                Else
                    Master.ControleErro.ShowError(Traduzir("055_msg_erro_ProcedenciaExistente"), False)
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função do Botão Cancelar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaProcedencias.aspx", False)
    End Sub

    ''' <summary>
    ''' Função do Botão Volver
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaProcedencias.aspx", False)
    End Sub

#End Region

#End Region

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoSubCliente()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposSubCliente.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposSubCliente

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposSubCliente = objRespuesta.TiposSubCliente
        End If

        ddlTipoSubCliente.AppendDataBoundItems = True
        ddlTipoSubCliente.Items.Clear()
        ddlTipoSubCliente.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoSubCliente.DataTextField = "Descripcion"
        ddlTipoSubCliente.DataValueField = "Codigo"
        ddlTipoSubCliente.DataSource = TiposSubCliente.OrderBy(Function(x) x.Descripcion)
        ddlTipoSubCliente.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoPuntoServicio()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposPuntoServicio.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposPuntoServicio

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposPuntoServicio = objRespuesta.TiposPuntoServicio
        End If

        ddlTipoPuntoServicio.AppendDataBoundItems = True
        ddlTipoPuntoServicio.Items.Clear()
        ddlTipoPuntoServicio.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoPuntoServicio.DataTextField = "Descripcion"
        ddlTipoPuntoServicio.DataValueField = "Codigo"
        ddlTipoPuntoServicio.DataSource = TiposPuntoServicio.OrderBy(Function(x) x.Descripcion)
        ddlTipoPuntoServicio.DataBind()

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [MAOLIVEIRA] 07/06/13 - Criado
    ''' </history>
    Public Sub PreencherComboTipoProcedencia()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboTiposProcedencia.Respuesta
        objRespuesta = objProxyUtilida.GetComboTiposProcedencia

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        Else
            TiposProcedencia = objRespuesta.TiposProcedencia
        End If

        ddlTipoProcedencia.AppendDataBoundItems = True
        ddlTipoProcedencia.Items.Clear()
        ddlTipoProcedencia.Items.Add(New ListItem(Traduzir("055_ddl_selecione"), String.Empty))
        ddlTipoProcedencia.DataTextField = "Descripcion"
        ddlTipoProcedencia.DataValueField = "Codigo"
        ddlTipoProcedencia.DataSource = TiposProcedencia.OrderBy(Function(x) x.Descripcion)
        ddlTipoProcedencia.DataBind()

    End Sub

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez
    ''' </summary>
    ''' <param name="OidProcedencia"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(OidProcedencia As String)

        Dim objProxy As New Comunicacion.ProxyProcedencia
        Dim objRespuesta As IAC.ContractoServicio.Procedencia.GetProcedencias.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Procedencia.GetProcedencias.Peticion
        objPeticion.OidProcedencia = OidProcedencia
        objRespuesta = objProxy.GetProcedencias(objPeticion)

        Procedencia = objRespuesta.Procedencias

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Procedencias.Count > 0 Then

            ddlTipoSubCliente.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoSubCliente
            ddlTipoPuntoServicio.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoPuntoServicio
            ddlTipoProcedencia.SelectedValue = objRespuesta.Procedencias.First().CodigoTipoProcedencia
            chkVigente.Checked = objRespuesta.Procedencias.First().Activo

            ' preenche a propriedade da tela
            EsVigente = objRespuesta.Procedencias.First().Activo

            'Se for modificação então guarda a descriçaõ atual para validação
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                'DescricaoAtual = txtDescricaoCanal.Text
            End If

        End If

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True        '2
                btnCancelar.Visible = True      '3
                btnVolver.Visible = False       '7
                lblVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = False
                chkVigente.Visible = False

                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnCancelar.Visible = False             '2                
                btnVolver.Visible = True                '4
                btnGrabar.Visible = False               '7

                'Estado Inicial Controles
                ddlTipoSubCliente.Enabled = False
                ddlTipoPuntoServicio.Enabled = False
                ddlTipoProcedencia.Enabled = False
                lblVigente.Visible = True
                chkVigente.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                chkVigente.Visible = True

                btnGrabar.Visible = True               '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                lblVigente.Visible = True

                ' se estiver checado e objeto for vigente
                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If

                btnGrabar.Habilitado = True

            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case eAcaoEspecifica.AltaConRegistro

                chkVigente.Visible = True

                btnGrabar.Habilitado = True
                '5
                btnCancelar.Visible = True             '6
                btnVolver.Visible = False              '7

                chkVigente.Enabled = False
                chkVigente.Visible = False
                chkVigente.Checked = True

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3
                btnVolver.Visible = True         '7

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

    Private Enum eAcaoEspecifica As Integer
        AltaConRegistro = 20
    End Enum

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

                'Verifica se o tipo do subcliente foi informado
                If ddlTipoSubCliente.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSubClienteObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSubCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoSubClienteObrigatorio.IsValid = True
                End If

                'Verifica se o tipo do punto de servicio foi informado
                If ddlTipoPuntoServicio.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoPuntoServicioObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoPuntoServicio.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoPuntoServicioObrigatorio.IsValid = True
                End If

                'Verifica se o tipo da procedencia foi informado
                If ddlTipoProcedencia.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoProcedenciaObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoProcedenciaObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoProcedencia.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoProcedenciaObrigatorio.IsValid = True
                End If


            End If

        End If

        Return strErro.ToString

    End Function

#End Region

#Region "[PROPRIEDADES]"

    Public Property Procedencia() As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion
        Get
            Return ViewState("Procedencia")
        End Get
        Set(value As ContractoServicio.Procedencia.GetProcedencias.ProcedenciaColeccion)
            ViewState("Procedencia") = value
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
            If ViewState("EsVigente") Is Nothing Then
                ViewState("EsVigente") = False
            End If
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Public Property OidProcedencia() As String
        Get
            Return ViewState("OidProcedencia")
        End Get
        Set(value As String)
            ViewState("OidProcedencia") = value
        End Set
    End Property

    Public Property TiposPuntoServicio() As GetComboTiposPuntoServicio.TipoPuntoServicioColeccion
        Get
            Return ViewState("TiposPuntoServicio")
        End Get
        Set(value As GetComboTiposPuntoServicio.TipoPuntoServicioColeccion)
            ViewState("TiposPuntoServicio") = value
        End Set
    End Property

    Public Property TiposSubCliente() As GetComboTiposSubCliente.TipoSubClienteColeccion
        Get
            Return ViewState("TiposSubCliente")
        End Get
        Set(value As GetComboTiposSubCliente.TipoSubClienteColeccion)
            ViewState("TiposSubCliente") = value
        End Set
    End Property

    Public Property TiposProcedencia() As GetComboTiposProcedencia.TipoProcedenciaColeccion
        Get
            Return ViewState("TiposProcedencia")
        End Get
        Set(value As GetComboTiposProcedencia.TipoProcedenciaColeccion)
            ViewState("TiposProcedencia") = value
        End Set
    End Property

#End Region

End Class