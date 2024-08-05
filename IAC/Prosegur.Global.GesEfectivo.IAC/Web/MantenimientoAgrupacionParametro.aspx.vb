Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis


Public Class MantenimientoAgrupacionParametro
    Inherits Base

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Propriedades da pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 15/09/2011 - Criado
    ''' </history>

    Property IsAlta() As Boolean
        Get
            Return ViewState("IsAlta")
        End Get
        Set(value As Boolean)
            ViewState("IsAlta") = value
        End Set
    End Property

    Property ParamsModificacionOK() As Boolean
        Get
            Return ViewState("ParamsModificacionOK")
        End Get
        Set(value As Boolean)
            ViewState("ParamsModificacionOK") = value
        End Set
    End Property

    Property CodigoAplicacion() As String
        Get
            Return ViewState("CodAplicacion")
        End Get
        Set(value As String)
            ViewState("CodAplicacion") = value
        End Set
    End Property

    Property CodigoNivel() As String
        Get
            Return ViewState("CodigoNivel")
        End Get
        Set(value As String)
            ViewState("CodigoNivel") = value
        End Set
    End Property

    Property DescripcionAgrupacion() As String
        Get
            Return ViewState("DescripcionAgrupacion")
        End Get
        Set(value As String)
            ViewState("DescripcionAgrupacion") = value
        End Set
    End Property

    Property DescripcionCorta() As String
        Get
            Return ViewState("DescripcionCorta")
        End Get
        Set(value As String)
            ViewState("DescripcionCorta") = value
        End Set
    End Property

    Property DescripcionLarga() As String
        Get
            Return ViewState("DescripcionLarga")
        End Get
        Set(value As String)
            ViewState("DescripcionLarga") = value
        End Set
    End Property

    Property NecOrden() As String
        Get
            Return ViewState("NecOrden")
        End Get
        Set(value As String)
            ViewState("NecOrden") = value
        End Set
    End Property

    Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property


#End Region


#Region "[OVERRIDES]"

    ''' <summary>
    ''' Adiciona validação aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

        MyBase.AddControleValidarPermissao(Aplicacao.Util.Utilidad.eAcoesTela._MODIFICAR.ToString, btnGrabar)

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        Dim pbo As PostBackOptions
        Dim s As String = String.Empty


        pbo = New PostBackOptions(btnGrabar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnGrabar.FuncaoJavascript = "if (VerificarConfirmacaoCanelamento('" & Traduzir(If(IsAlta, Aplicacao.Util.Utilidad.InfoMsgAltaRegistro, Aplicacao.Util.Utilidad.InfoMsgGrabarRegistro)) & "')) " & s & " ; "

        btnCancelar.FuncaoJavascript = "if(VerificarConfirmacaoCanelamento('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgSairPagina) & "')) window.location = 'BusquedaAgrupacionesParametros.aspx'; else return false; "
        txtOrden.Attributes.Add("onkeypress", "bloqueialetras(event,this);")
        txtOrden.Attributes.Add("onKeyDown", "BloquearColar();")

        txtDescripcionLarga.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")
        txtDescripcionLarga.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcionLarga.ClientID & "','" & txtDescripcionLarga.MaxLength & "');")

    End Sub

    ''' <summary>
    ''' Configuração do tabindex.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

        ddlAplicacion.TabIndex = 1
        ddlNivel.TabIndex = 2
        txtDescripcionCorto.TabIndex = 3
        txtDescripcionLarga.TabIndex = 4
        txtOrden.TabIndex = 5
        btnGrabar.TabIndex = 6
        btnCancelar.TabIndex = 7
        btnVolver.TabIndex = 8

        ' Caso seja alta focaliza o campo ddlAplicacion, caso contrario focaliza o campo txtDescripcionLarga
        Master.PrimeiroControleTelaID = String.Format("{0}", If(Acao = Aplicacao.Util.Utilidad.eAcao.Alta, ddlAplicacion.ClientID, txtDescripcionLarga.ClientID))
        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.DIVISAS
    End Sub

    ''' <summary>
    ''' Metodo chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            CodigoAplicacion = Request.QueryString("codaplicacion")
            CodigoNivel = Request.QueryString("codnivel")
            DescripcionAgrupacion = Request.QueryString("desagrupacion")
            IsAlta = Acao = Aplicacao.Util.Utilidad.eAcao.Alta

            'Verifica se existe algun dos campos vazios caso positivo será ALTA caso negativo será MODIFICACIÓN
            ParamsModificacionOK = Not (String.IsNullOrEmpty(CodigoAplicacion) OrElse String.IsNullOrEmpty(CodigoNivel) OrElse String.IsNullOrEmpty(DescripcionAgrupacion))

            'If Not IsAlta AndAlso Not ParamsModificacionOK Then
            '    TableFields.Visible = False
            '    Throw New Excepcion.NegocioExcepcion(Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT, Traduzir("err_passagem_parametro"))
            'End If

            If Not Page.IsPostBack Then
                'Preenche ddlAplicacion
                PreencherListBoxAplicaciones()

                'Preenche ddlNivel
                PreencherListBoxNiveles()

                If Not IsAlta Then
                    'Preenche Tela
                    PreencherTela()
                End If
            End If

            TrataFoco()

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()
        Try
            'ControleBotoes()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 06/09/2011 - Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("028_titulo_mantenimento_agrupacion")
        lblSubTitulosAgrupacionParametro.Text = Traduzir("028_subtitulo_mantenimento_agrupacion")
        lblAplicacion.Text = Traduzir("028_lbl_aplicacion_mant")
        lblNivel.Text = Traduzir("028_lbl_nivel_mant")
        lblDescripcionLarga.Text = Traduzir("028_lbl_DescripcionLarga_mant")
        lblDescripcionCorto.Text = Traduzir("028_lbl_descripcioncorto_mant")
        lblOrden.Text = Traduzir("028_lbl_orden_mant")
        csvDdlAplicacionObrigatorio.ErrorMessage = Traduzir("028_msg_aplicacionobligatorio")
        csvDdlNivelObrigatorio.ErrorMessage = Traduzir("028_msg_nivelobligatorio")
        csvTxtDescripcionCortoObrigatorio.ErrorMessage = Traduzir("028_msg_descripcioncortoobligatorio")
        csvTxtDescripcionLargaObrigatorio.ErrorMessage = Traduzir("028_msg_DescripcionLargaobligatorio")
        csvTxtOrden.ErrorMessage = Traduzir("028_msg_Dordenobligatorio")

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist Aplicaciones.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 15/09/2011 - Criado
    ''' </history>
    Public Sub PreencherListBoxAplicaciones()
        Try
            ddlAplicacion.DataSource = Nothing
            ddlAplicacion.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If InformacionUsuario.Aplicaciones.Count > 1 Then
                ddlAplicacion.AppendDataBoundItems = True
                ddlAplicacion.Items.Clear()
                ddlAplicacion.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlAplicacion.DataTextField = "DescripcionAplicacion"
            ddlAplicacion.DataValueField = "CodigoAplicacion"
            ddlAplicacion.DataSource = InformacionUsuario.Aplicaciones.OrderBy(Function(x) x.DescripcionAplicacion)
            ddlAplicacion.DataBind()

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o dropdownlist Nível.
    ''' </summary>
    Public Sub PreencherListBoxNiveles()
        Try
            Dim objNivelesParametros As New ContractoServicio.Utilidad.GetComboNivelesParametros.NivelParametroColeccion
            objNivelesParametros = Aplicacao.Util.Utilidad.getComboNivelesParametros(InformacionUsuario.Permisos)

            ddlNivel.DataSource = Nothing
            ddlNivel.DataBind()
            ' Caso exista apenas um registro a mesmo será aplicado como default
            If objNivelesParametros.Count > 1 Then
                ddlNivel.AppendDataBoundItems = True
                ddlNivel.Items.Clear()
                ddlNivel.Items.Add(New ListItem(Traduzir("010_ddl_selecione"), String.Empty))
            End If
            ddlNivel.DataTextField = "DescripcionNivel"
            ddlNivel.DataValueField = "CodigoNivel"
            ddlNivel.DataSource = objNivelesParametros.OrderBy(Function(x) x.DescripcionNivel)
            ddlNivel.DataBind()

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Preeche os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherTela()
        If Not ParamsModificacionOK Then
            TableFields.Visible = False
        End If

        Dim objRespuestaGetAgrupacionDetail As IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta

        objRespuestaGetAgrupacionDetail = getAgrupacionDetail()

        If Not Master.ControleErro.VerificaErro(objRespuestaGetAgrupacionDetail.CodigoError, objRespuestaGetAgrupacionDetail.NombreServidorBD, objRespuestaGetAgrupacionDetail.MensajeError) Then
            Exit Sub
        End If

        'Desabilita campos chaves quando é Modificación
        ddlAplicacion.Enabled = False
        ddlNivel.Enabled = False
        txtDescripcionCorto.Enabled = False

        ddlAplicacion.SelectedIndex = ddlAplicacion.Items.IndexOf(ddlAplicacion.Items.FindByValue(objRespuestaGetAgrupacionDetail.Agrupaciones.CodigoAplicacion))
        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text

        ddlNivel.SelectedIndex = ddlNivel.Items.IndexOf(ddlNivel.Items.FindByValue(objRespuestaGetAgrupacionDetail.Agrupaciones.CodigoNivel))
        ddlNivel.ToolTip = ddlNivel.SelectedItem.Text

        txtDescripcionCorto.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionCorto
        txtDescripcionCorto.ToolTip = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionCorto

        txtDescripcionLarga.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.DescripcionLarga
        txtOrden.Text = objRespuestaGetAgrupacionDetail.Agrupaciones.NecOrden
    End Sub


    ''' <summary>
    ''' Responsavel por ir no DB e buscar as informações para preencher a tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 15/09/2011 - Criado
    ''' </history>
    Public Function getAgrupacionDetail() As IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaGetAgrupacionDetail As New IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Respuesta
        Dim objPeticionGetAgrupacionDetail As New IAC.ContractoServicio.Parametro.GetAgrupacionDetail.Peticion

        objPeticionGetAgrupacionDetail.CodigoAplicacion = CodigoAplicacion
        objPeticionGetAgrupacionDetail.CodigoNivel = CodigoNivel
        objPeticionGetAgrupacionDetail.DesAgrupacion = DescripcionAgrupacion

        objRespuestaGetAgrupacionDetail = objProxyParametro.GetAgrupacionDetail(objPeticionGetAgrupacionDetail)

        Return objRespuestaGetAgrupacionDetail
    End Function

    ''' <summary>
    ''' Responsavel por ir no DB e setar as informações da tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 13/09/2011 - Criado
    ''' </history>
    Public Function setAgrupacion() As IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta

        Dim objProxyParametro As New Comunicacion.ProxyParametro
        Dim objRespuestaSetAgrupacion As New IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta
        Dim objPeticionSetAgrupacion As New IAC.ContractoServicio.Parametro.SetAgrupacion.Peticion

        objPeticionSetAgrupacion.CodigoAplicacion = CodigoAplicacion
        objPeticionSetAgrupacion.CodigoNivel = CodigoNivel
        objPeticionSetAgrupacion.DescripcionCorta = DescripcionCorta
        objPeticionSetAgrupacion.DescripcionLarga = DescripcionLarga
        objPeticionSetAgrupacion.NecOrden = If(String.IsNullOrEmpty(NecOrden), "0", NecOrden)
        objPeticionSetAgrupacion.CodigoUsuario = MyBase.LoginUsuario

        objRespuestaSetAgrupacion = objProxyParametro.SetAgrupacion(objPeticionSetAgrupacion)

        Return objRespuestaSetAgrupacion
    End Function

    ''' <summary>
    ''' Responsavel gravar dados da tela.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick] 13/09/2011 - Criado
    ''' </history>
    Public Sub ExecutarGravar()
        ' Executa controle de validação dos campos
        ValidarCamposObrigatorios = True
        Dim erros As String = MontaMensagensErro(True)
        If Not String.IsNullOrEmpty(erros) Then
            Master.ControleErro.ShowError(erros, False)
            Exit Sub
        End If

        'Preenche viewstates com os dados da tela
        CodigoAplicacion = ddlAplicacion.SelectedValue
        CodigoNivel = ddlNivel.SelectedValue
        DescripcionCorta = txtDescripcionCorto.Text
        DescripcionLarga = txtDescripcionLarga.Text
        If String.IsNullOrEmpty(txtOrden.Text) OrElse IsNumeric(txtOrden.Text) Then
            NecOrden = txtOrden.Text
        Else
            ' exibir mensagem ao usuário
            Master.ControleErro.ShowError(Traduzir("err_passagem_parametro") & " - " & lblOrden.Text, False)
            txtOrden.Focus()
            Exit Sub
        End If

        Dim objRespuestaSetAgrupacion As IAC.ContractoServicio.Parametro.SetAgrupacion.Respuesta

        objRespuestaSetAgrupacion = setAgrupacion()

        If Not Master.ControleErro.VerificaErro(objRespuestaSetAgrupacion.CodigoError, objRespuestaSetAgrupacion.NombreServidorBD, objRespuestaSetAgrupacion.MensajeError) Then
            Exit Sub
        End If
        Response.Redirect("BusquedaAgrupacionesParametros.aspx", False)
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

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                'Laço que verifica todos os validators
                For Each validator As BaseValidator In Me.Validators
                    'Checa validator se é ou não válido
                    validator.Validate()
                    If Not validator.IsValid Then
                        'Caso o campo esteja inválido é adicionado uma mensagem de erro a lista
                        strErro.Append(validator.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        If Not focoSetado Then
                            'Seta o foco no primeiro campo com erro
                            validator.NamingContainer.FindControl(validator.ControlToValidate).Focus()
                            focoSetado = True
                        End If
                    End If
                Next

            End If
        End If

        Return strErro.ToString

    End Function


#End Region

#Region "[EVENTOS]"

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            ExecutarGravar()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Private Sub ddlAplicacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlAplicacion.SelectedIndexChanged
        ddlAplicacion.ToolTip = ddlAplicacion.SelectedItem.Text
    End Sub

    Private Sub ddlNivel_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNivel.SelectedIndexChanged
        ddlNivel.ToolTip = ddlNivel.SelectedItem.Text
    End Sub

#End Region

End Class