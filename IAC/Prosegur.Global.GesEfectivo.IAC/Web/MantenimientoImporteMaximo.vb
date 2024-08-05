Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis

Public Class MantenimientoImporteMaximo
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property ClienteSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(ByVal value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(ByVal value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(ByVal value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property DescricaoExistente() As Boolean
        Get
            Return ViewState("DescricaoExistente")
        End Get
        Set(ByVal value As Boolean)
            ViewState("DescricaoExistente") = value
        End Set
    End Property

    Private Property OidSubCliente As String
        Get
            Return ViewState("OidSubCliente")
        End Get
        Set(value As String)
            ViewState("OidSubCliente") = value
        End Set
    End Property

    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

    Public Property Direccion() As ContractoServicio.Direccion.DireccionColeccionBase
        Get
            Return DirectCast(ViewState("Direccion"), ContractoServicio.Direccion.DireccionColeccionBase)
        End Get
        Set(ByVal value As ContractoServicio.Direccion.DireccionColeccionBase)
            ViewState("Direccion") = value
        End Set
    End Property

    Private Property SubClienteCol() As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
        Get
            Return DirectCast(ViewState("SubClienteCol"), IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente))
        End Get

        Set(value As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente))
            ViewState("SubClienteCol") = value
        End Set

    End Property

    Private Property ListaTipoSubCliente() As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion
        Get
            Return ViewState("ListaTipoSubCliente")
        End Get
        Set(value As ContractoServicio.TipoSubCliente.getTiposSubclientes.TipoSubClienteColeccion)
            ViewState("ListaTipoSubCliente") = value
        End Set
    End Property

    Private _ucTotSaldo As ucTotSaldo
    Public ReadOnly Property ucTotSaldo As ucTotSaldo
        Get
            If _ucTotSaldo Is Nothing Then
                _ucTotSaldo = LoadControl("~\Controles\ucTotSaldo.ascx")
                _ucTotSaldo.ID = "TotSaldo"
                If phTotSaldo.Controls.Count = 0 Then
                    phTotSaldo.Controls.Add(_ucTotSaldo)
                End If
            End If
            Return _ucTotSaldo
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub ConfigurarTabIndex()

        txtCliente.TabIndex = 1
        ddlTipoSubCliente.TabIndex = 2
        txtCodigoAjeno.TabIndex = 3
        txtDesCodigoAjeno.TabIndex = 4
        txtCodigoSubCliente.TabIndex = 5
        txtDescSubCliente.TabIndex = 6

        chkTotSaldo.TabIndex = 9
        chkVigente.TabIndex = 10
        ' TODO: SALDO

        btnGrabar.TabIndex = 13
        btnCancelar.TabIndex = 14
        btnVolver.TabIndex = 15

    End Sub
    Protected Overrides Sub DefinirParametrosBase()

        'Define como ação inical
        Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.IMPORTEMAXIMO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
        'Pegando a ação
        MyBase.Acao = Request.QueryString("acao")

    End Sub

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                Dim oidImporteMaximo As String = Request.QueryString("oidImporteMaximo")

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                CargarTipoSubCliente()

                If oidImporteMaximo <> String.Empty Then

                    CargarDatos(oidImporteMaximo)
                    ucTotSaldo.Cliente.Identificador = ClienteSelecionado.OidCliente
                    ucTotSaldo.SubCliente.Identificador = oidImporteMaximo

                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
                   OrElse MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    txtDescSubCliente.Focus()
                End If

            End If

            Me.ucTotSaldo.AtualizaGrid()

            TrataFoco()

            ConsomeDireccion()

            ConsomeCodigoAjeno()

            ConsomeCliente()

            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try


    End Sub

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing And Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    Protected Overrides Sub TraduzirControles()

        Master.TituloPagina = Traduzir("039_titulo_mantenimiento")
        lblTituloSubCliente.Text = Traduzir("039_titulo_mantenimiento")

        lblCliente.Text = Traduzir("039_lbl_cliente")
        lblCodSubCliente.Text = Traduzir("039_lbl_CodSubCliente")
        lblCodigoAjeno.Text = Traduzir("039_lbl_CodigoAjeno")
        lblDescSubCliente.Text = Traduzir("039_lbl_DescSubCliente")
        lblDesCodigoAjeno.Text = Traduzir("039_lbl_DesCodigoAjeno")
        lblTipoSubCliente.Text = Traduzir("039_lbl_TipoSubCliente")
        lblTituloSubCliente.Text = Traduzir("039_lbl_TituloSubCliente")
        lblTotSaldo.Text = Traduzir("039_lbl_TotSaldo")
        lblVigente.Text = Traduzir("039_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("039_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("039_lbl_ProprioTotSaldo")

        csvClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvClienteObrigatorio")
        csvCodSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvCodSubClienteExistente")
        csvCodSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvCodSubClienteObrigatorio")
        csvDescSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvDescSubClienteObrigatorio")
        csvTipoSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvTipoSubClienteObrigatorio")
        csvDescSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvDescSubClienteExistente")

        btnBuscarCliente.ExibirLabelBtn = False

    End Sub

    Protected Overrides Sub PreRenderizar()

        Try
            If Not IsPostBack Then
                ControleBotoes()
            End If

            MostrarMensagem()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta
                txtCodigoSubCliente.Enabled = True
                txtDescSubCliente.Enabled = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadirTotalizador.Visible = True

                If Not IsPostBack Then
                    chkVigente.Checked = True
                    chkVigente.Enabled = False
                End If
                Me.ucTotSaldo.Habilitar()

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                txtCodigoSubCliente.Enabled = False
                If Not IsPostBack Then
                    If chkVigente.Checked = True Then
                        chkVigente.Enabled = False
                    End If
                End If

                btnAnadirTotalizador.Visible = True
                Me.ucTotSaldo.Habilitar()
                txtCodigoSubCliente.ReadOnly = True

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True

                txtCliente.Enabled = False
                txtCodigoSubCliente.Enabled = False
                txtDescSubCliente.Enabled = False
                ddlTipoSubCliente.Enabled = False
                chkVigente.Enabled = False
                chkTotSaldo.Enabled = False
                btnAnadirTotalizador.Visible = False
                Me.ucTotSaldo.Desabilitar()

                btnBuscarCliente.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnAnadirTotalizador.Visible = False

        End Select

        txtCodigoAjeno.Enabled = False
        txtDesCodigoAjeno.Enabled = False

        Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
        ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro

        If Not ParametroMantenimientoClientesDivisasPorPantalla Then

            txtCliente.Enabled = False
            btnBuscarCliente.Habilitado = False

            ddlTipoSubCliente.Enabled = False

            txtCodigoSubCliente.Enabled = False
            txtDescSubCliente.Enabled = False

            btnDireccion.Habilitado = False

        End If

        If MontaMensagensErro() <> String.Empty Then
            Master.ControleErro.ShowError(MontaMensagensErro, False)
        End If

    End Sub

    'Mostrar Mensagens de Erro caso seja postback
    Private Sub MostrarMensagem()
        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        Dim MensagemErro As String = MontaMensagensErro()

        If MensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(MensagemErro, False)
        End If
    End Sub
#End Region

#Region "[METODOS]"

    Private Sub ConsomeCliente()

        If Session("ClienteSelecionado") IsNot Nothing Then

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ClienteSelecionado = objCliente

                ' setar controles da tela
                txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
                txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

            End If

            Session("ClienteSelecionado") = Nothing
            txtCodigoSubCliente.Enabled = True
            txtDescSubCliente.Enabled = True
        End If

    End Sub

    Private Sub ConsomeDireccion()
        If Session("DireccionPeticion") IsNot Nothing Then
            Direccion = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
            Session.Remove("DireccionPeticion")
        End If
    End Sub

    Private Sub ConsomeCodigoAjeno()

        If Session("objRespuestaGEPR_TSUBCLIENTE") IsNot Nothing Then

            If SubClienteCol Is Nothing OrElse SubClienteCol.Count = 0 OrElse SubClienteCol.FirstOrDefault Is Nothing Then
                SubClienteCol = New IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
                SubClienteCol.Add(New IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
            End If

            SubClienteCol.FirstOrDefault.CodigosAjenos = Session("objRespuestaGEPR_TSUBCLIENTE")
            Session.Remove("objRespuestaGEPR_TSUBCLIENTE")

            Dim iCodigoAjeno = (From item In SubClienteCol.FirstOrDefault.CodigosAjenos
                                Where item.BolDefecto = True).SingleOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            If SubClienteCol.FirstOrDefault.CodigosAjenos IsNot Nothing Then
                CodigosAjenosPeticion = SubClienteCol.FirstOrDefault.CodigosAjenos
            Else
                CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            End If

            Session("objPeticionGEPR_TSUBCLIENTE") = SubClienteCol.FirstOrDefault.CodigosAjenos

        End If

    End Sub

    Private Sub CargarTipoSubCliente()

        Dim objPeticion As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Peticion
        Dim objRespuesta As New ContractoServicio.TipoSubCliente.getTiposSubclientes.Respuesta
        Dim objProxy As New Comunicacion.ProxyTipoSubCliente

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objRespuesta = objProxy.getTiposSubclientes(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlTipoSubCliente.AppendDataBoundItems = True
        ddlTipoSubCliente.Items.Clear()

        ListaTipoSubCliente = objRespuesta.TipoSubCliente

        For Each tipo In objRespuesta.TipoSubCliente
            ddlTipoSubCliente.Items.Add(New ListItem(tipo.codTipoSubcliente + " - " + tipo.desTipoSubcliente, tipo.oidTipoSubcliente))
        Next

        ddlTipoSubCliente.OrdenarPorDesc()
        ddlTipoSubCliente.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Public Function GetSubCliente(ByVal oidSubCliente As String) As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)

        Dim objProxySubCliente As New Comunicacion.ProxySubCliente
        Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Peticion
        Dim objRespuestaSubCliente As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.Respuesta

        objPeticionSubCliente.ParametrosPaginacion.RealizarPaginacion = False
        objPeticionSubCliente.OidSubCliente = oidSubCliente

        objRespuestaSubCliente = objProxySubCliente.GetSubClientesDetalle(objPeticionSubCliente)

        Return objRespuestaSubCliente.SubClientes

    End Function

    Public Sub CargarDatos(ByVal oidSubCliente As String)

        Dim itemSelecionado As ListItem
        Dim objColSubCliente As IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubClienteColeccion(Of IAC.ContractoServicio.SubCliente.GetSubClientesDetalle.SubCliente)
        objColSubCliente = GetSubCliente(oidSubCliente)

        If objColSubCliente.Count > 0 Then

            'Coloca na VState
            SubClienteCol = objColSubCliente

            Dim iCodigoAjeno = (From item In objColSubCliente(0).CodigosAjenos
                   Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Or Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoSubCliente.Text = objColSubCliente(0).CodSubCliente
                txtCodigoSubCliente.ToolTip = objColSubCliente(0).CodSubCliente
            End If

            txtDescSubCliente.Text = objColSubCliente(0).DesSubCliente
            txtDescSubCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objColSubCliente(0).DesSubCliente, String.Empty)

            chkVigente.Checked = objColSubCliente(0).BolVigente
            chkTotSaldo.Checked = objColSubCliente(0).BolTotalizadorSaldo

            'Cliente
            txtCliente.Text = objColSubCliente(0).CodCliente & " - " & objColSubCliente(0).DesCliente
            txtCliente.ToolTip = objColSubCliente(0).CodCliente & " - " & objColSubCliente(0).DesCliente

            Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente.Codigo = objColSubCliente(0).CodCliente
            objCliente.Descripcion = objColSubCliente(0).DesCliente
            objCliente.OidCliente = objColSubCliente(0).OidCliente
            ClienteSelecionado = objCliente

            'Seleciona o valor divisa
            itemSelecionado = ddlTipoSubCliente.Items.FindByValue(objColSubCliente(0).OidTipoSubCliente)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoSubCliente.ToolTip = itemSelecionado.ToString
            End If

            Me.OidSubCliente = objColSubCliente(0).OidSubCliente

        End If

    End Sub

    Public Sub ExecutarGrabar()
        Try

            Dim objProxySubCliente As New Comunicacion.ProxySubCliente
            Dim objProxyDireccion As New Comunicacion.ProxyDireccion

            Dim objPeticionSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.Peticion
            Dim objRespuestaSubCliente As IAC.ContractoServicio.SubCliente.SetSubClientes.Respuesta

            Dim objSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.SubCliente
            Dim objColSubCliente As New IAC.ContractoServicio.SubCliente.SetSubClientes.SubClienteColeccion

            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            objSubCliente.OidSubCliente = OidSubCliente
            objSubCliente.OidCliente = ClienteSelecionado.OidCliente
            objSubCliente.CodCliente = ClienteSelecionado.Codigo
            objSubCliente.CodSubCliente = txtCodigoSubCliente.Text
            objSubCliente.DesSubCliente = txtDescSubCliente.Text
            objSubCliente.BolTotalizadorSaldo = chkTotSaldo.Checked
            objSubCliente.BolVigente = chkVigente.Checked

            Dim tipoSubCliente = ListaTipoSubCliente.FirstOrDefault(Function(t) t.oidTipoSubcliente = ddlTipoSubCliente.SelectedValue)
            objSubCliente.OidTipoSubCliente = tipoSubCliente.oidTipoSubcliente
            objSubCliente.CodTipoSubCliente = tipoSubCliente.codTipoSubcliente
            objSubCliente.DesTipoSubCliente = tipoSubCliente.desTipoSubcliente

            objSubCliente.CodigoAjeno = CodigosAjenosPeticion


            objSubCliente.ConfigNivelSaldo = ConverteNivelSaldo(Me.ucTotSaldo.TotalizadoresSaldos)
            objSubCliente.BolSubClienteTotSaldo = chkTotSaldo.Checked

            If Direccion IsNot Nothing AndAlso Direccion.Count > 0 AndAlso Direccion.FirstOrDefault IsNot Nothing Then
                objSubCliente.Direcciones = Direccion
            End If

            objColSubCliente.Add(objSubCliente)

            objPeticionSubCliente.SubClientes = objColSubCliente
            objPeticionSubCliente.CodigoUsuario = MyBase.LoginUsuario

            objRespuestaSubCliente = objProxySubCliente.SetSubClientes(objPeticionSubCliente)

            If Master.ControleErro.VerificaErro(objRespuestaSubCliente.CodigoError, objRespuestaSubCliente.NombreServidorBD, objRespuestaSubCliente.MensajeError) Then

                If Master.ControleErro.VerificaErro(objRespuestaSubCliente.SubClientes(0).CodigoError, objRespuestaSubCliente.NombreServidorBD, objRespuestaSubCliente.SubClientes(0).MensajeError) Then

                    If Master.ControleErro.VerificaErro(objRespuestaSubCliente.SubClientes(0).CodigoError, objRespuestaSubCliente.SubClientes(0).NombreServidorBD, objRespuestaSubCliente.SubClientes(0).MensajeError) Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaSubClientes.aspx');", True)
                    Else
                        Master.ControleErro.ShowError(objRespuestaSubCliente.SubClientes(0).MensajeError, False)
                    End If

                    Session.Remove("DireccionPeticion")

                End If

            Else

                If objRespuestaSubCliente.SubClientes IsNot Nothing AndAlso objRespuestaSubCliente.SubClientes.Count > 0 AndAlso objRespuestaSubCliente.SubClientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaSubCliente.SubClientes(0).MensajeError, False)
                ElseIf objRespuestaSubCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaSubCliente.MensajeError, False)
                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Public Sub ExecutarAnadirTotalizador()
        Me.ucTotSaldo.Cambiar(-1)
    End Sub

    Public Sub ExecutarCancelar()
        Response.Redirect("~/BusquedaSubClientes.aspx", False)
    End Sub

    Public Sub ExecutarVolver()
        Response.Redirect("~/BusquedaSubClientes.aspx", False)
    End Sub

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(Optional ByVal SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False


        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If ClienteSelecionado Is Nothing OrElse txtCliente.Text.Equals(String.Empty) Then

                    strErro.Append(csvClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvClienteObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvClienteObrigatorio.IsValid = True
                End If

                'Verifica se a descrição foi preenchida
                If String.IsNullOrEmpty(txtDescSubCliente.Text) Then

                    strErro.Append(csvDescSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescSubClienteObrigatorio.IsValid = False

                    'Seta o foco no campo que deu erro.
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescSubCliente.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescSubClienteObrigatorio.IsValid = True
                End If

                If ddlTipoSubCliente.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSubClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSubCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoSubClienteObrigatorio.IsValid = True
                End If


                If txtCodigoSubCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodSubClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoSubCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodSubClienteObrigatorio.IsValid = True
                End If


            End If

            If CodigoExistente Then

                strErro.Append(csvCodSubClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodSubClienteExistente.IsValid = False

                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoSubCliente.Focus()
                    focoSetado = True
                End If

            Else
                csvCodSubClienteExistente.IsValid = True
            End If

            If DescricaoExistente Then

                strErro.Append(csvDescSubClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescSubClienteExistente.IsValid = False

                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescSubCliente.Focus()
                    focoSetado = True
                End If

            Else
                csvDescSubClienteExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Private Function ExisteCodigoSubCliente(ByVal codigoCliente As String, ByVal codigoSubCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoSubCliente.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.CodSubCliente = codigoSubCliente.Trim
                objResposta = objProxyUtilidad.VerificarCodigoSubCliente(objPeticion)

                If Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then
                    If objResposta.Existe Then
                        Return True
                    End If
                Else
                    Master.ControleErro.ShowError(objResposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function

    Private Function ExisteDescricaoSubCliente(ByVal codigoCliente As String, ByVal descricaoSubCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescSubCliente.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarDescripcionSubCliente.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.DesSubCliente = descricaoSubCliente.Trim
                objResposta = objProxyUtilidad.VerificarDescripcionSubCliente(objPeticion)

                If Master.ControleErro.VerificaErro(objResposta.CodigoError, objResposta.NombreServidorBD, objResposta.MensajeError) Then
                    If objResposta.Existe Then
                        Return True
                    End If
                Else
                    Master.ControleErro.ShowError(objResposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function

    Public Function RetornaSubCanais() As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        Dim objProxySubCanal As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objSubCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Dim objSubCanalCol As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion = Nothing

        objRespuesta = objProxySubCanal.GetComboSubcanalesByCanal(objPeticion)

        objSubCanalCol = New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
        For Each item In objRespuesta.Canales
            For Each subcanal In item.SubCanales
                objSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                objSubCanalCol.Add(subcanal)
            Next
        Next

        Return objSubCanalCol
    End Function

    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)
        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then
            Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)
        End If

        If remove Then
            Dim objTotSaldoProprio = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Me.ClienteSelecionado.OidCliente _
                                                          AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = Me.OidSubCliente _
                                                          AndAlso a.PuntoServicio Is Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)
            If objTotSaldoProprio IsNot Nothing Then
                If objTotSaldoProprio.bolDefecto Then
                    Dim objTotSaldo = Me.ucTotSaldo.TotalizadoresSaldos.Except({objTotSaldoProprio}).ToList().Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)
                    If objTotSaldo IsNot Nothing Then
                        objTotSaldo.bolDefecto = True
                    End If
                End If

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(objTotSaldoProprio)

            End If

        Else

            If Not Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Me.ClienteSelecionado.OidCliente _
                                                          AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = Me.OidSubCliente _
                                                          AndAlso a.PuntoServicio Is Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1) Then

                Dim lstSubCanais = RetornaSubCanais()

                If lstSubCanais IsNot Nothing AndAlso lstSubCanais.Count > 0 Then

                    Dim objTotSaldo As New Comon.Clases.TotalizadorSaldo
                    With objTotSaldo
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = Me.ClienteSelecionado.OidCliente
                        .Cliente.Codigo = Me.ClienteSelecionado.Codigo
                        .Cliente.Descripcion = Me.ClienteSelecionado.Descripcion
                        .SubCliente = New Comon.Clases.SubCliente
                        .SubCliente.Identificador = Me.OidSubCliente
                        .SubCliente.Codigo = Me.SubClienteCol.First.CodSubCliente
                        .SubCliente.Descripcion = Me.SubClienteCol.First.DesSubCliente
                        .SubCanales = New List(Of Comon.Clases.SubCanal)
                        For Each objSubCanal In lstSubCanais
                            .SubCanales.Add(New Comon.Clases.SubCanal With {
                                            .Identificador = objSubCanal.OidSubCanal,
                                            .Codigo = objSubCanal.Codigo,
                                            .Descripcion = objSubCanal.Descripcion
                                            })
                        Next

                    End With

                    Me.ucTotSaldo.TotalizadoresSaldos.Add(objTotSaldo)

                    'Remove subcanais
                    For Each totSubCanal In Me.ucTotSaldo.TotalizadoresSaldos.Where(Function(a) a.SubCanales.Count = 1)
                        objTotSaldo.SubCanales.RemoveAll(Function(a) a.Identificador = totSubCanal.SubCanales.First.Identificador)
                    Next

                    objTotSaldo.SubCanales = objTotSaldo.SubCanales.OrderBy(Function(a) a.Descripcion).ToList()

                End If

            End If

        End If

        Me.ucTotSaldo.AtualizaGrid()
        upTotSaldo.Update()
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub btnVolver_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnVolver.Click
        ExecutarVolver()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        ExecutarCancelar()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnAnadirTotalizador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnadirTotalizador.Click
        ExecutarAnadirTotalizador()
    End Sub

    Private Sub chkProprioTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkProprioTotSaldo.CheckedChanged

        AddRemoveTotalizadorSaldoProprio(Not chkProprioTotSaldo.Checked)
    End Sub

    Private Function ConverteNivelSaldo(lstTotSaldo As List(Of Comon.Clases.TotalizadorSaldo)) As ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        For Each nivelSaldo In lstTotSaldo

            Dim peticionNivelSaldo As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True

                .oidCliente = Me.ClienteSelecionado.OidCliente
                .codCliente = Me.ClienteSelecionado.Codigo
                .desCliente = Me.ClienteSelecionado.Descripcion

                .oidSubCliente = Me.SubClienteCol.First.OidSubCliente
                .codSubCliente = Me.SubClienteCol.First.CodSubCliente
                .desSubCliente = Me.SubClienteCol.First.DesSubCliente

                If nivelSaldo.SubCanales IsNot Nothing AndAlso nivelSaldo.SubCanales.Count = 1 Then
                    .oidSubCanal = nivelSaldo.SubCanales.First.Identificador
                    .codSubCanal = nivelSaldo.SubCanales.First.Codigo
                    .desSubCanal = nivelSaldo.SubCanales.First.Descripcion
                End If

                .configNivelSaldo = New ContractoServicio.Utilidad.GetConfigNivel.ConfigNivelSaldo
                With .configNivelSaldo
                    .oidConfigNivelSaldo = nivelSaldo.IdentificadorNivelSaldo

                    If nivelSaldo.Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.Cliente.Identificador) Then
                        .oidCliente = nivelSaldo.Cliente.Identificador
                        .codCliente = nivelSaldo.Cliente.Codigo
                        .desCliente = nivelSaldo.Cliente.Descripcion
                    End If

                    If nivelSaldo.SubCliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.SubCliente.Identificador) Then
                        .oidSubcliente = nivelSaldo.SubCliente.Identificador
                        .codSubcliente = nivelSaldo.SubCliente.Codigo
                        .desSubcliente = nivelSaldo.SubCliente.Descripcion
                    End If

                    If nivelSaldo.PuntoServicio IsNot Nothing AndAlso Not String.IsNullOrEmpty(nivelSaldo.PuntoServicio.Identificador) Then
                        .oidPtoServicio = nivelSaldo.PuntoServicio.Identificador
                        .codPtoServicio = nivelSaldo.PuntoServicio.Codigo
                        .desPtoServicio = nivelSaldo.PuntoServicio.Descripcion
                    End If

                End With

            End With

            retorno.Add(peticionNivelSaldo)
        Next

        Return retorno

    End Function

    Private Sub txtCodigoSubCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoSubCliente.TextChanged
        Try
            If ClienteSelecionado IsNot Nothing Then

                If ExisteCodigoSubCliente(ClienteSelecionado.Codigo, txtCodigoSubCliente.Text) Then
                    CodigoExistente = True
                Else
                    CodigoExistente = False
                End If

            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Private Sub txtDescSubCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescSubCliente.TextChanged
        Try

            If ClienteSelecionado IsNot Nothing Then

                If ExisteDescricaoSubCliente(ClienteSelecionado.Codigo, txtDescSubCliente.Text) Then
                    DescricaoExistente = True
                Else
                    DescricaoExistente = False
                End If

            End If

            Threading.Thread.Sleep(100)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Private Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoSubCliente.Text
        tablaGenesis.DesTablaGenesis = txtDescSubCliente.Text
        tablaGenesis.OidTablaGenesis = OidSubCliente
        If SubClienteCol IsNot Nothing AndAlso SubClienteCol.Count > 0 AndAlso SubClienteCol.FirstOrDefault IsNot Nothing AndAlso SubClienteCol.FirstOrDefault.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = SubClienteCol.FirstOrDefault.CodigosAjenos
        End If

        Session("objPeticionGEPR_TSUBCLIENTE") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TSUBCLIENTE") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCLIENTE"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjeno');", True)
    End Sub

    Private Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = txtCodigoSubCliente.Text
        tablaGenes.DesGenesis = txtDescSubCliente.Text
        tablaGenes.OidGenesis = OidSubCliente
        If Direccion Is Nothing AndAlso Not String.IsNullOrEmpty(OidSubCliente) Then
            If SubClienteCol.FirstOrDefault.Direcciones IsNot Nothing AndAlso SubClienteCol.FirstOrDefault.Direcciones.Count > 0 AndAlso SubClienteCol.FirstOrDefault.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = SubClienteCol.FirstOrDefault.Direcciones.FirstOrDefault
            End If
        ElseIf Direccion IsNot Nothing Then
            tablaGenes.Direcion = Direccion.FirstOrDefault
        End If

        Session("objGEPR_TSUBCLIENTE") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCLIENTE"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
    End Sub

    Protected Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click

        Try

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Sub chkTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTotSaldo.CheckedChanged

        chkProprioTotSaldo.Enabled = chkTotSaldo.Checked
        If Not chkTotSaldo.Checked Then
            chkProprioTotSaldo.Checked = False
            AddRemoveTotalizadorSaldoProprio(True)
        End If

        upChkProprioTotSaldo.Update()

    End Sub

    Private Sub chkVigente_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVigente.CheckedChanged
        If chkVigente.Checked Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_Aviso", "alert('" & Traduzir("038_lbl_SubclienteAvisoAtivo") & "');", True)
        End If
    End Sub

    Protected Sub ucTotSaldo_DadosCarregados(sender As Object, args As System.EventArgs)
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing Then

            If Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente.Identificador = Me.ClienteSelecionado.OidCliente _
                                                            AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = Me.OidSubCliente _
                                                            AndAlso a.PuntoServicio Is Nothing _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub

#End Region

End Class