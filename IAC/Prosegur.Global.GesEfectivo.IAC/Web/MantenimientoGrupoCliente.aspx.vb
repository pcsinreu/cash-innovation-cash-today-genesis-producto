Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Página de gerencimaneto de Grupo de Cliente
''' </summary>
''' <remarks></remarks>
''' <history>
''' [matheus.araujo] 05/11/2012 - Criado
''' </history>
Public Class MantenimientoGrupoCliente
    Inherits Base

#Region "[OVERRIDES]"



    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        ' Seta o foco para o proximo controle quando presciona o enter.
        txtCodigo.Attributes.Add("onkeydown", _
            "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & txtDescripcion.ClientID & "').focus();return false;}} else {return true}; ")

        txtDescripcion.Attributes.Add("onkeydown", _
            "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" & btnBuscaCliente.ClientID & "').focus();return false;}} else {return true}; ")

        ' Limite de caracteres
        txtCodigo.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")
        txtCodigo.Attributes.Add("onblur", "limitaCaracteres('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")
        txtCodigo.Attributes.Add("onkeyup", "limitaCaracteres('" & txtCodigo.ClientID & "','" & txtCodigo.MaxLength & "');")

        txtDescripcion.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")
        txtDescripcion.Attributes.Add("onblur", "limitaCaracteres('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")
        txtDescripcion.Attributes.Add("onkeyup", "limitaCaracteres('" & txtDescripcion.ClientID & "','" & txtDescripcion.MaxLength & "');")

        ' Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCodigo.TabIndex = 1
        txtDescripcion.TabIndex = 2
        btnBuscaCliente.TabIndex = 3
        btnBuscaSubCliente.TabIndex = 4
        btnBuscaPtoServicio.TabIndex = 5
        chkVigente.TabIndex = 6
        ddlTipoGrupoCliente.TabIndex = 7

        btnGrabar.TabIndex = 11
        btnCancelar.TabIndex = 12
        btnVolver.TabIndex = 13

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = If(Request.QueryString("acao") Is Nothing, Aplicacao.Util.Utilidad.eAcao.NoAction, Request.QueryString("acao"))

        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then
                CargarTipoGrupoCliente()

                Dim strCodGrupoCliente As String = Request.QueryString("codGrupoCliente")

                ' verifica erro na passagem de parâmetros
                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' se passou código do grupo de cliente
                If Not String.IsNullOrEmpty(strCodGrupoCliente) Then
                    PreencheTela(strCodGrupoCliente)
                End If

                If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescripcion.Focus()
                Else
                    txtCodigo.Focus()
                End If

                PreencheGrids(Nothing)

            End If

            ' se for inserção ou modificação, consome o filtro de clientes/subclientes/ptos de serviço da sessão e preenche os lists
            If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

                ConsomeCliente()

                ConsomeSubCliente()

                ConsomePuntoServicio()

                CargarTipoGrupoCliente()

            End If

            ConsomeDireccion()


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

        Master.TituloPagina = Traduzir("030_titulo_mantenimiento_grupo_cliente")

        lblTituloGrupoCliente.Text = Traduzir("030_titulo_mantenimiento_grupo_cliente")

        lblCodigo.Text = Traduzir("030_lbl_codigo_grupo_cliente")
        lblDescripcion.Text = Traduzir("030_lbl_descricao_grupo_cliente")


        lblCliente.Text = Traduzir("030_lbl_cliente")
        lblSubCliente.Text = Traduzir("030_lbl_subcliente")
        lblPtoServicio.Text = Traduzir("030_lbl_pto_servicio")

        lblClienteResultado.Text = Traduzir("030_lblClienteResultado")
        lblSubClienteResultado.Text = Traduzir("030_lblSubClienteResultado")
        lblPtoServicioResultado.Text = Traduzir("030_lblPtoServicioResultado")

        lblSemRegistroCliente.Text = Traduzir("info_msg_grd_vazio")
        lblSemRegistroSubCliente.Text = Traduzir("info_msg_grd_vazio")
        lblSemRegistroPtoServico.Text = Traduzir("info_msg_grd_vazio")


        lblVigente.Text = Traduzir("030_lbl_vigente")
        lblTipoGrupoCliente.Text = Traduzir("030_lblTipoGrupoCliente")

        csvCodigoObrigatorio.ErrorMessage = Traduzir("030_msg_codigo_obrigatorio")
        csvDescripcionObrigatorio.ErrorMessage = Traduzir("030_msg_descricaco_obrigatorio")
        csvCodigoExistente.ErrorMessage = Traduzir("030_msg_codigo_existente")
        csvTipoGrupoClienteObrigatorio.ErrorMessage = Traduzir("030_msg_TipoGrupoCliente_obrigatorio")

        btnBuscaCliente.ExibirLabelBtn = False
        btnBuscaSubCliente.ExibirLabelBtn = False
        btnBuscaPtoServicio.ExibirLabelBtn = False

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property GrupoCliente As ContractoServicio.GrupoCliente.GrupoClienteDetalle
        Get

            If ViewState("GrupoCliente") Is Nothing Then
                ViewState("GrupoCliente") = New ContractoServicio.GrupoCliente.GrupoClienteDetalle
            End If

            Return ViewState("GrupoCliente")
        End Get
        Set(value As ContractoServicio.GrupoCliente.GrupoClienteDetalle)
            ViewState("GrupoCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena no viewstate se o código de grupo já existe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CodigoExistente As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção com os subclientes passados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SubClientesSelecionados() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return ViewState("SubClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            ViewState("SubClientesSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena os pontos de serviço selecionados do grid.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Property PuntoServiciosSelecionados() As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
        Get
            Return ViewState("PuntoServiciosSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)
            ViewState("PuntoServiciosSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina se é necessário validar os campos obrigatórios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Private Sub ExecutarVolver()
        Response.Redirect("~/BusquedaGrupoCliente.aspx", False)
    End Sub

    Private Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaGrupoCliente.aspx", False)
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

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescripcion, csvDescripcionObrigatorio, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(txtDescripcion, csvTipoGrupoClienteObrigatorio, SetarFocoControle, focoSetado))

            End If

            'Verifica se o código existe
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso CodigoExistente Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigo.Focus()
                    focoSetado = True
                End If

            Else
                csvCodigoExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Verifica se o código de grupo de cliente já existe no banco de dados
    ''' </summary>
    ''' <param name="sCodGrupoCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificaCodigoExistente(sCodGrupoCliente As String) As Boolean

        ' se há um grupo de cliente na resposta, já existe o código
        Dim objRetorno As Boolean = False
        Dim objGrupoCliente = GetGruposClientesDetalle(sCodGrupoCliente).GrupoCliente

        If objGrupoCliente IsNot Nothing AndAlso objGrupoCliente.Count > 0 Then
            objRetorno = True
        End If

        Return objRetorno

    End Function

    ''' <summary>
    ''' Faz a consulta ao proxy e retorna a resposta
    ''' </summary>
    ''' <param name="sCodGrupoCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGruposClientesDetalle(sCodGrupoCliente As String) As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta
        Dim proxy As New Genesis.Comunicacion.ProxyGrupoClientes
        Dim cliente = New ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With {.Codigo = New List(Of String)}
        cliente.Codigo.Add(sCodGrupoCliente)
        Return (proxy.GetGruposClientesDetalle(cliente))
    End Function

    ''' <summary>
    ''' Preenche a tela
    ''' </summary>
    ''' <param name="sCodGrupoCliente"></param>
    ''' <remarks></remarks>
    Private Sub PreencheTela(sCodGrupoCliente As String)

        ' faz a consulta
        Dim respuesta As ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta = _
            GetGruposClientesDetalle(sCodGrupoCliente)

        If Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then

            ' código e descrição
            Me.txtCodigo.Text = respuesta.GrupoCliente.FirstOrDefault.Codigo
            Me.txtDescripcion.Text = respuesta.GrupoCliente.FirstOrDefault.Descripcion

            ' armazena clientes no viewstate
            GrupoCliente = respuesta.GrupoCliente.FirstOrDefault

            ' vigente
            Me.chkVigente.Checked = respuesta.GrupoCliente.FirstOrDefault.Vigente

            ddlTipoGrupoCliente.SelectedValue = respuesta.GrupoCliente.FirstOrDefault.CodTipoGrupoCliente


        End If

    End Sub



    ''' <summary>
    ''' Consome a sessão da tela de busca cliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ClienteSelecionado = objCliente

                ' setar controles da tela
                txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
                txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

                'Limpa os demais campos
                txtSubCliente.Text = String.Empty
                txtPtoServicio.Text = String.Empty

                SubClientesSelecionados = Nothing
                PuntoServiciosSelecionados = Nothing

            End If

            Session("ClienteSelecionado") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca subcliente.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeSubCliente()

        If Session("SubClientesSelecionados") IsNot Nothing Then

            Dim objSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            objSubClientes = TryCast(Session("SubClientesSelecionados"), ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)

            If objSubClientes IsNot Nothing Then

                SubClientesSelecionados = objSubClientes

                ' setar controles da tela
                txtSubCliente.Text = objSubClientes(0).Codigo & " - " & objSubClientes(0).Descripcion

                txtSubCliente.ToolTip = String.Empty

                'Limpa os demais campos
                txtPtoServicio.Text = String.Empty

                PuntoServiciosSelecionados = Nothing

            End If

            Session("SubClientesSelecionados") = Nothing


        End If

        'verifica a sessão de subcliente é pra ser limpa        
        If Session("LimparSubClienteSelecionado") IsNot Nothing Then
            SubClientesSelecionados = Nothing
            PuntoServiciosSelecionados = Nothing

            'Limpa os demais campos
            txtSubCliente.Text = String.Empty
            txtSubCliente.ToolTip = String.Empty

            txtPtoServicio.Text = String.Empty
            txtPtoServicio.ToolTip = String.Empty

            'retira da sessão
            Session("LimparSubClienteSelecionado") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de busca de ponto de serviço.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomePuntoServicio()

        If Session("PuntosServicioSelecionados") IsNot Nothing Then

            Dim ObjPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            ObjPuntoServicio = TryCast(Session("PuntosServicioSelecionados"), ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion)

            If ObjPuntoServicio IsNot Nothing Then

                PuntoServiciosSelecionados = ObjPuntoServicio
                ' setar controles da tela

                txtPtoServicio.Text = PuntoServiciosSelecionados(0).Codigo & " - " & PuntoServiciosSelecionados(0).Descripcion

                txtPtoServicio.ToolTip = String.Empty


            End If

            Session("PuntosServicioSelecionados") = Nothing

        End If

        'verifica a sessão de ponto de serviço é pra ser limpa        
        If Session("LimparPuntoServicioSelecionado") IsNot Nothing Then

            PuntoServiciosSelecionados = Nothing

            'Limpa os demais campos            
            txtPtoServicio.Text = String.Empty
            txtPtoServicio.ToolTip = String.Empty

            'retira da sessão
            Session("LimparPuntoServicioSelecionado") = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetClienteSelecionadoPopUp()

        Session("objCliente") = ClienteSelecionado

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetSubClientesSelecionadoPopUp()

        Session("objSubClientes") = SubClientesSelecionados

    End Sub


    Private Sub RemoverSelecionados()

        SubClientesSelecionados = Nothing
        ClienteSelecionado = Nothing
        PuntoServiciosSelecionados = Nothing

        txtSubCliente.Text = String.Empty
        txtCliente.Text = String.Empty
        txtPtoServicio.Text = String.Empty

    End Sub

    Private Sub ConsomeDireccion()
        If Session("DireccionPeticion") IsNot Nothing Then
            GrupoCliente.Direccion = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase).FirstOrDefault
            Session.Remove("DireccionPeticion")
        End If
    End Sub

    Private Sub PreencheGrids(sender As Object)

        If GrupoCliente IsNot Nothing AndAlso GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then

            Dim clientes = (From p In GrupoCliente.Clientes
                           Where String.IsNullOrEmpty(p.CodSubCliente)
                            Select p).ToList()



            Dim subclientes = (From p In GrupoCliente.Clientes
                                Where Not String.IsNullOrEmpty(p.CodSubCliente) AndAlso String.IsNullOrEmpty(p.CodPtoServicio)
                                Select New SublienteAux With {
            .codCliente = p.CodCliente,
            .codSubCliente = p.CodSubCliente,
            .DesCliente = p.DesCliente,
            .DesSubCliente = p.DesSubCliente,
            .oidCliente = p.OidCliente,
            .OidGrupoClienteDetalle = p.OidGrupoClienteDetalle,
            .oidSubCliente = p.OidSubCliente}).ToList()


            Dim ptoServicio = (From p In GrupoCliente.Clientes
                                Where Not String.IsNullOrEmpty(p.CodSubCliente) AndAlso Not String.IsNullOrEmpty(p.CodPtoServicio)
                                    Select New PtoServicioAux With {
            .codCliente = p.CodCliente,
            .codSubCliente = p.CodSubCliente,
            .DesCliente = p.DesCliente,
            .DesSubCliente = p.DesSubCliente,
            .oidCliente = p.OidCliente,
            .OidGrupoClienteDetalle = p.OidGrupoClienteDetalle,
            .oidSubCliente = p.OidSubCliente,
            .codPtoServicio = p.CodPtoServicio,
            .DesPtoServicio = p.DesPtoServivico,
            .oidPtoServicio = p.OidPtoServivico}).ToList()



            If clientes IsNot Nothing AndAlso clientes.Count() > 0 Then
                Dim objDt As DataTable = gdvClientes.ConvertListToDataTable(clientes)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvClientes.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvClientes.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvClientes.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvClientes.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvClientes.CarregaControle(objDt)
                End If

                pnlSemRegistroCliente.Visible = False


            Else
                'todos os grid sem valores
                gdvClientes.DataSource = Nothing
                gdvClientes.DataBind()
                pnlSemRegistroCliente.Visible = True
            End If

            If subclientes IsNot Nothing AndAlso subclientes.Count() > 0 Then
                Dim objDt As DataTable = gdvSubClientes.ConvertListToDataTable(subclientes)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvSubClientes.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvSubClientes.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvSubClientes.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvSubClientes.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvSubClientes.CarregaControle(objDt)
                End If
                pnlSemRegistroSubCliente.Visible = False
            Else
                gdvSubClientes.DataSource = Nothing
                gdvSubClientes.DataBind()
                pnlSemRegistroSubCliente.Visible = True

            End If

            If ptoServicio IsNot Nothing AndAlso ptoServicio.Count() > 0 Then
                Dim objDt As DataTable = gdvPtoServicio.ConvertListToDataTable(ptoServicio)
                If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                    objDt.DefaultView.Sort = " CodCliente ASC"
                ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                    If gdvPtoServicio.SortCommand.Equals(String.Empty) Then
                        objDt.DefaultView.Sort = " CodCliente ASC "
                    Else
                        objDt.DefaultView.Sort = gdvPtoServicio.SortCommand
                    End If

                Else
                    objDt.DefaultView.Sort = gdvPtoServicio.SortCommand
                End If

                If sender IsNot Nothing Then
                    gdvPtoServicio.ControleDataSource = objDt
                Else
                    ' carregar controle
                    gdvPtoServicio.CarregaControle(objDt)
                End If
                pnlSemRegistroPtoServico.Visible = False
            Else
                gdvPtoServicio.DataSource = Nothing
                gdvPtoServicio.DataBind()
                pnlSemRegistroPtoServico.Visible = True
            End If



        Else
            'todos os grid sem valores
            gdvClientes.DataSource = Nothing
            gdvClientes.DataBind()
            pnlSemRegistroCliente.Visible = True

            gdvSubClientes.DataSource = Nothing
            gdvSubClientes.DataBind()
            pnlSemRegistroSubCliente.Visible = True

            gdvPtoServicio.DataSource = Nothing
            gdvPtoServicio.DataBind()
            pnlSemRegistroPtoServico.Visible = True

        End If


    End Sub

    Private Sub ExecutarGravar()
        ValidarCamposObrigatorios = True

        If String.IsNullOrEmpty(MontaMensagensErro(True)) Then

            Dim proxy As New Genesis.Comunicacion.ProxyGrupoClientes
            Dim objPeticion As New IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Peticion

            GrupoCliente.Codigo = txtCodigo.Text
            GrupoCliente.Descripcion = txtDescripcion.Text
            GrupoCliente.CodigoUsuario = MyBase.LoginUsuario
            GrupoCliente.Vigente = chkVigente.Checked
            GrupoCliente.FyhAtualizacion = Date.Now

            If ddlTipoGrupoCliente.SelectedIndex > 0 Then
                GrupoCliente.CodTipoGrupoCliente = ddlTipoGrupoCliente.SelectedValue
            End If

            objPeticion.GrupoCliente = GrupoCliente

            Dim respuesta As IAC.ContractoServicio.GrupoCliente.SetGruposCliente.Respuesta = proxy.SetGrupoCliente(objPeticion)

            If Master.ControleErro.VerificaErro(respuesta.CodigoError, respuesta.NombreServidorBD, respuesta.MensajeError) Then
                Response.Redirect("~/BusquedaGrupoCliente.aspx", False)
            Else
                Master.ControleErro.ShowError(respuesta.MensajeError, False)
            End If

        End If

    End Sub


    Private Sub CargarTipoGrupoCliente()

        If ddlTipoGrupoCliente.Items.Count = 0 Then
            ddlTipoGrupoCliente.Items.Insert(0, New ListItem(Traduzir("gen_opcion_todos"), String.Empty))
            ddlTipoGrupoCliente.Items.Insert(1, New ListItem(Traduzir("gen_opcion_general"), Traduzir("gen_opcion_general")))
            ddlTipoGrupoCliente.Items.Insert(2, New ListItem(Traduzir("gen_opcion_atm"), Traduzir("gen_opcion_atm")))
        End If

    End Sub


#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGravar()
    End Sub

    ''' <summary>
    ''' clique em buscar cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscaCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaCliente.Click

        Try

            ' passa para a sessão os clientes já selecionados
            ' ForneceClientes()

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&ColecaoClientes=False&vigente=True"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaCliente');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' clique em buscar subcliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscaSubCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaSubCliente.Click

        Try

            If ClienteSelecionado IsNot Nothing Then

                'Seta o cliente selecionado para a PopUp
                SetClienteSelecionadoPopUp()

                Dim url As String = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&SelecaoUnica=True&vigente=True"

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaSubCliente');", True)

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' clique em buscar ponto de serviço
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnBuscaPtoServicio_Click(sender As Object, e As EventArgs) Handles btnBuscaPtoServicio.Click

        Try


            If ClienteSelecionado IsNot Nothing _
           AndAlso SubClientesSelecionados IsNot Nothing _
           AndAlso SubClientesSelecionados.Count > 0 Then


                'Seta o cliente selecionado para a PopUp
                SetClienteSelecionadoPopUp()

                'Seta os subcliente selecionados para a PopUp
                SetSubClientesSelecionadoPopUp()

                Dim url As String = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&SelecaoUnica=True&vigente=True"

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscaPtoServicio');", True)

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub btnRemover_Click(sender As Object, e As EventArgs) Handles btnRemover.Click

        RemoverSelecionados()

    End Sub

    Protected Sub btnAnadir_Click(sender As Object, e As EventArgs) Handles btnAnadir.Click
        If GrupoCliente.Clientes Is Nothing Then
            GrupoCliente.Clientes = New ContractoServicio.GrupoCliente.ClienteDetalleColeccion
        End If

        If PuntoServiciosSelecionados IsNot Nothing AndAlso PuntoServiciosSelecionados.Count > 0 Then


            Dim existe = From p In GrupoCliente.Clientes
                        Where p.OidSubCliente = SubClientesSelecionados.FirstOrDefault.OidSubCliente _
                        AndAlso p.OidCliente = ClienteSelecionado.OidCliente _
                        AndAlso p.OidPtoServivico = PuntoServiciosSelecionados.FirstOrDefault.OidPuntoServicio _


            If existe.Count() = 0 Then

                Dim cliente As New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                          .CodCliente = ClienteSelecionado.Codigo,
                                          .DesCliente = ClienteSelecionado.Descripcion,
                                          .OidCliente = ClienteSelecionado.OidCliente,
                                          .OidSubCliente = SubClientesSelecionados.FirstOrDefault().OidSubCliente,
                                          .CodSubCliente = SubClientesSelecionados.FirstOrDefault().Codigo,
                                          .DesSubCliente = SubClientesSelecionados.FirstOrDefault().Descripcion,
                                          .OidPtoServivico = PuntoServiciosSelecionados.FirstOrDefault().OidPuntoServicio,
                                          .CodPtoServicio = PuntoServiciosSelecionados.FirstOrDefault().Codigo,
                                          .DesPtoServivico = PuntoServiciosSelecionados.FirstOrDefault().Descripcion
                                      }


                GrupoCliente.Clientes.Add(cliente)

                RemoverSelecionados()
            Else
                'show error cliente ja foi add
                Master.ControleErro.ShowError(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_pto_servicio")) _
                                              , False)
            End If



        ElseIf SubClientesSelecionados IsNot Nothing AndAlso SubClientesSelecionados.Count > 0 Then


            Dim existe = From p In GrupoCliente.Clientes
                         Where p.OidCliente = ClienteSelecionado.OidCliente _
                        AndAlso p.OidSubCliente = SubClientesSelecionados.FirstOrDefault.OidSubCliente _
                        AndAlso String.IsNullOrEmpty(p.OidPtoServivico)

            If existe.Count() = 0 Then

                Dim cliente As New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                          .CodCliente = ClienteSelecionado.Codigo,
                                          .DesCliente = ClienteSelecionado.Descripcion,
                                          .OidCliente = ClienteSelecionado.OidCliente,
                                          .OidSubCliente = SubClientesSelecionados.FirstOrDefault().OidSubCliente,
                                          .CodSubCliente = SubClientesSelecionados.FirstOrDefault().Codigo,
                                          .DesSubCliente = SubClientesSelecionados.FirstOrDefault().Descripcion
                                      }
               

                GrupoCliente.Clientes.Add(cliente)

                RemoverSelecionados()
            Else
                'show error cliente ja foi add
                Master.ControleErro.ShowError(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_subcliente")) _
                                            , False)
            End If


        ElseIf ClienteSelecionado IsNot Nothing Then

            Dim existe = From p In GrupoCliente.Clientes
                         Where p.OidCliente = ClienteSelecionado.OidCliente AndAlso String.IsNullOrEmpty(p.OidSubCliente) _
                         AndAlso String.IsNullOrEmpty(p.OidPtoServivico)


            If existe.Count() = 0 Then

                GrupoCliente.Clientes.Add(New ContractoServicio.GrupoCliente.ClienteDetalle With {
                                          .CodCliente = ClienteSelecionado.Codigo,
                                          .DesCliente = ClienteSelecionado.Descripcion,
                                          .OidCliente = ClienteSelecionado.OidCliente
                                      })
                RemoverSelecionados()
            Else
                'show error cliente ja foi add
                Master.ControleErro.ShowError(String.Format(Traduzir("030_msg_iten_existente"), Traduzir("030_lbl_cliente")) _
                                            , False)
            End If

        Else
            'apresenta uma mensagem de erro
            Master.ControleErro.ShowError(Traduzir("info_msg_seleccionar_cliente"), False)
        End If


        PreencheGrids(Nothing)

    End Sub

    Protected Sub btnDireccion_Click(sender As Object, e As EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = GrupoCliente.Codigo
        tablaGenes.DesGenesis = GrupoCliente.Descripcion
        tablaGenes.OidGenesis = GrupoCliente.oidGrupoCliente
        If GrupoCliente.Direccion Is Nothing AndAlso Not String.IsNullOrEmpty(GrupoCliente.oidGrupoCliente) Then
            If GrupoCliente.Direccion IsNot Nothing Then
                tablaGenes.Direcion = GrupoCliente.Direccion
            End If
        ElseIf GrupoCliente.Direccion IsNot Nothing Then
            tablaGenes.Direcion = GrupoCliente.Direccion
        End If

        Session("objGEPR_TGRUPO_CLIENTE") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TGRUPO_CLIENTE"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TGRUPO_CLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)

    End Sub

#End Region

#Region "[EVENTOS TEXTBOX]"

    Private Sub txtCodigo_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigo.TextChanged
        CodigoExistente = VerificaCodigoExistente(Me.txtCodigo.Text)
    End Sub

#End Region

#Region "[EVENTOS DE GRID]"

    Protected Sub gdvClientes_EPreencheDados(sender As Object, e As EventArgs) Handles gdvClientes.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvClientes_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvClientes.RowCommand

        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvClientes.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvClientes.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvClientes.DataKeys(row.RowIndex).Item(1)

                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then

                    Dim deletar = From p In GrupoCliente.Clientes
                                       Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                       AndAlso String.IsNullOrEmpty(p.OidSubCliente)
                                Select p
                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvSubClientes_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvSubClientes.RowCommand
        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvSubClientes.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvSubClientes.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvSubClientes.DataKeys(row.RowIndex).Item(1)
                Dim oidSubCliente = gdvSubClientes.DataKeys(row.RowIndex).Item(2)
                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then


                    Dim deletar = From p In GrupoCliente.Clientes
                         Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                       AndAlso p.OidSubCliente = oidSubCliente
                         Select p

                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvPtoServicio_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdvPtoServicio.RowCommand
        If e.CommandName = "Deletar" Then

            Try

                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)

                Dim oidGrupoClienteDetalle = If(IsDBNull(gdvPtoServicio.DataKeys(row.RowIndex).Item(0)), String.Empty, gdvPtoServicio.DataKeys(row.RowIndex).Item(0))
                Dim oidCliente = gdvPtoServicio.DataKeys(row.RowIndex).Item(1)
                Dim oidSubCliente = gdvPtoServicio.DataKeys(row.RowIndex).Item(2)
                Dim oidPtoServicio = gdvPtoServicio.DataKeys(row.RowIndex).Item(3)
                If GrupoCliente.Clientes IsNot Nothing AndAlso GrupoCliente.Clientes.Count > 0 Then


                    Dim deletar = From p In GrupoCliente.Clientes
                         Where p.OidCliente = oidCliente _
                                       AndAlso p.OidGrupoClienteDetalle = oidGrupoClienteDetalle _
                                        AndAlso p.OidSubCliente = oidSubCliente _
                                        AndAlso p.OidPtoServivico = oidPtoServicio
                         Select p


                    If deletar IsNot Nothing AndAlso deletar.Count = 1 Then
                        GrupoCliente.Clientes.Remove(deletar.FirstOrDefault)
                        PreencheGrids(Nothing)
                    End If

                End If

            Catch ex As Exception

            End Try
        End If
    End Sub

    Protected Sub gdvSubClientes_EPreencheDados(sender As Object, e As EventArgs) Handles gdvSubClientes.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvPtoServicio_EPreencheDados(sender As Object, e As EventArgs) Handles gdvPtoServicio.EPreencheDados
        PreencheGrids(sender)
    End Sub

    Protected Sub gdvClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvClientes.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub gdvSubClientes_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvSubClientes.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"



        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub gdvPtoServicio_EPager_SetCss(sender As Object, e As EventArgs) Handles gdvPtoServicio.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub gdvClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientes.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then

                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                                  Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("lbl_gdr_eliminar")

            End If


        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub gdvSubClientes_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvSubClientes.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                                  Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                          Traduzir("030_lbl_subcliente"))

                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_subcliente"))

                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("lbl_gdr_eliminar")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub gdvPtoServicio_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvPtoServicio.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                                 Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_cliente"))

                CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                          Traduzir("030_lbl_subcliente"))

                CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_subcliente"))

                CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_codigo_grupo_cliente"), _
                                                                                                         Traduzir("030_lbl_pto_servicio"))

                CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = String.Format("{0} {1}", Traduzir("030_lbl_descricao_grupo_cliente"), _
                                                                                                                    Traduzir("030_lbl_pto_servicio"))

                CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(0).Controls(0), Label).Text = Traduzir("lbl_gdr_eliminar")
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        'Validação Comum aos controles
        If ClienteSelecionado Is Nothing Then
            btnBuscaCliente.Habilitado = True
            btnBuscaSubCliente.Habilitado = False
            btnBuscaPtoServicio.Habilitado = False
            SubClientesSelecionados = Nothing
            PuntoServiciosSelecionados = Nothing
        Else
            btnBuscaSubCliente.Habilitado = True
            If SubClientesSelecionados Is Nothing Then
                btnBuscaPtoServicio.Habilitado = False
                PuntoServiciosSelecionados = Nothing
            Else
                btnBuscaPtoServicio.Habilitado = True
            End If
        End If


        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadir.Visible = True
                btnRemover.Visible = True

                txtCodigo.Enabled = True
                txtDescripcion.Enabled = True
                ddlTipoGrupoCliente.Enabled = True


                lblVigente.Visible = False
                chkVigente.Visible = False
                chkVigente.Checked = True
                chkVigente.Enabled = True

                btnGrabar.Habilitado = True


            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True

                txtCodigo.Enabled = False
                txtDescripcion.Enabled = False

                lblVigente.Visible = True
                chkVigente.Enabled = False

                btnBuscaCliente.Habilitado = False
                btnBuscaSubCliente.Habilitado = False
                btnBuscaPtoServicio.Habilitado = False
                btnAnadir.Visible = False
                btnRemover.Visible = False

                gdvClientes.Columns(2).Visible = False
                gdvPtoServicio.Columns(6).Visible = False
                gdvSubClientes.Columns(4).Visible = False
                ddlTipoGrupoCliente.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                txtCodigo.Enabled = False
                chkVigente.Visible = True

                btnGrabar.Habilitado = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadir.Visible = True
                btnRemover.Visible = True

                lblVigente.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnAnadir.Visible = False
                btnRemover.Visible = False
                btnVolver.Visible = True

        End Select

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

#End Region

#Region "[ENUMERADORES]"

    Private Enum TelaDestino
        Cliente
        SubCliente
        PtoServicio
    End Enum

#End Region


#Region "[CLASSE AUXILIARES]"

    Private Class SublienteAux

        Public Property OidGrupoClienteDetalle As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property DesCliente As String
        Public Property oidSubCliente As String
        Public Property codSubCliente As String
        Public Property DesSubCliente As String

    End Class

    Private Class PtoServicioAux

        Public Property OidGrupoClienteDetalle As String
        Public Property oidCliente As String
        Public Property codCliente As String
        Public Property DesCliente As String
        Public Property oidSubCliente As String
        Public Property codSubCliente As String
        Public Property DesSubCliente As String
        Public Property oidPtoServicio As String
        Public Property codPtoServicio As String
        Public Property DesPtoServicio As String

    End Class


#End Region


End Class


