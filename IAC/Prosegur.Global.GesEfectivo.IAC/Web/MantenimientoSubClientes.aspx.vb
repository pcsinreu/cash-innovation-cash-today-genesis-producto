Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoSubCliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.SubCliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class MantenimientoSubCliente
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property Cliente As GetComboClientes.Cliente
        Get

            If ViewState("_Cliente") Is Nothing AndAlso Session("ClienteSelecionado") IsNot Nothing Then
                ViewState("_Cliente") = TryCast(Session("ClienteSelecionado"), ContractoServicio.Utilidad.GetComboClientes.Cliente)
                If ViewState("_Cliente") IsNot Nothing Then

                    ' setar controles da tela
                    txtCliente.Text = ViewState("_Cliente").Codigo & " - " & ViewState("_Cliente").Descripcion
                    txtCliente.ToolTip = ViewState("_Cliente").Codigo & " - " & ViewState("_Cliente").Descripcion

                End If
                Session("ClienteSelecionado") = Nothing
            End If

            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(SubCliente.OidCliente) Then
                ViewState("_Cliente") = New GetComboClientes.Cliente With {.OidCliente = SubCliente.OidCliente, .Codigo = SubCliente.CodCliente, .Descripcion = SubCliente.DesCliente}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As GetComboClientes.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Public Property SubCliente As GetSubClientesDetalle.SubCliente
        Get
            If ViewState("_SubCliente") Is Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("oidSubCliente")) Then

                Dim _Proxy As New Comunicacion.ProxySubCliente
                Dim _Peticion As New GetSubClientesDetalle.Peticion
                Dim _Respuesta As GetSubClientesDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidSubCliente = Request.QueryString("oidSubCliente")
                _Respuesta = _Proxy.GetSubClientesDetalle(_Peticion)
                If _Respuesta.SubClientes IsNot Nothing AndAlso _Respuesta.SubClientes.Count > 0 Then
                    ViewState("_SubCliente") = _Respuesta.SubClientes(0)
                End If

            End If

            If ViewState("_SubCliente") Is Nothing AndAlso Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                ViewState("_SubCliente") = New GetSubClientesDetalle.SubCliente With {.OidSubCliente = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_SubCliente")
        End Get
        Set(value As GetSubClientesDetalle.SubCliente)
            ViewState("_SubCliente") = value
        End Set
    End Property

    Private _TiposSubCliente As getTiposSubclientes.TipoSubClienteColeccion
    Public ReadOnly Property TiposSubCliente As getTiposSubclientes.TipoSubClienteColeccion
        Get
            If _TiposSubCliente Is Nothing Then

                Dim _Peticion As New getTiposSubclientes.Peticion
                Dim _Respuesta As New getTiposSubclientes.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoSubCliente

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposSubclientes(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    Throw New Exception("Error al obtener Tipos de SubCliente")
                End If

                _TiposSubCliente = _Respuesta.TipoSubCliente
            End If
            Return _TiposSubCliente
        End Get
    End Property

    Private _Accion As Aplicacao.Util.Utilidad.eAcao?
    Public ReadOnly Property Accion As Aplicacao.Util.Utilidad.eAcao
        Get
            If _Accion Is Nothing Then
                If Request.QueryString("acao") Is Nothing Then
                    _Accion = Aplicacao.Util.Utilidad.eAcao.NoAction
                Else
                    _Accion = Request.QueryString("acao")
                End If
            End If
            Return _Accion
        End Get
    End Property

    Public Property Direcciones As ContractoServicio.Direccion.DireccionColeccionBase
        Get
            If Session("DireccionPeticion") IsNot Nothing Then
                SubCliente.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return SubCliente.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            SubCliente.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TSUBCLIENTE") IsNot Nothing Then

                SubCliente.CodigosAjenos = Session("objRespuestaGEPR_TSUBCLIENTE")
                Session.Remove("objRespuestaGEPR_TSUBCLIENTE")

                Dim iCodigoAjeno = (From item In SubCliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If SubCliente.CodigosAjenos Is Nothing Then
                    SubCliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TSUBCLIENTE") = SubCliente.CodigosAjenos

            End If

            Return SubCliente.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            SubCliente.CodigosAjenos = value
        End Set
    End Property

    Private _SubCanales As List(Of Comon.Clases.SubCanal)
    Public Property SubCanales As List(Of Comon.Clases.SubCanal)
        Get
            If _SubCanales Is Nothing Then

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New GetComboSubcanalesByCanal.Peticion
                Dim _Respuesta As New GetComboSubcanalesByCanal.Respuesta
                _Respuesta = _Proxy.GetComboSubcanalesByCanal(_Peticion)

                If _Respuesta IsNot Nothing AndAlso _Respuesta.Canales IsNot Nothing AndAlso _Respuesta.Canales.Count > 0 Then
                    _SubCanales = New List(Of Comon.Clases.SubCanal)
                    For Each item In _Respuesta.Canales
                        For Each subcanal In item.SubCanales
                            _SubCanales.Add(New Comon.Clases.SubCanal With {
                                            .Identificador = subcanal.OidSubCanal,
                                            .Codigo = subcanal.Codigo,
                                            .Descripcion = subcanal.Descripcion
                                            })
                        Next
                    Next
                End If
            End If

            Return _SubCanales
        End Get
        Set(value As List(Of Comon.Clases.SubCanal))
            _SubCanales = value
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

    Protected Overrides Sub Inicializar()

        Try

            If Not (Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse
                    Accion = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse
                    Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
                Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                Throw New Exception(Traduzir("err_passagem_parametro"))
            End If

            If Not Page.IsPostBack Then

                CargarTipoSubCliente()

                If Accion <> Aplicacao.Util.Utilidad.eAcao.Alta Then
                    CargarDatos()
                End If
            End If

            ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
            ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente

            If Cliente IsNot Nothing Then
                ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
            End If

            ConfigurarControles()

            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados

        Catch ex As Exception
            Throw New InicializarException(ex.ToString)
        End Try

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        Try
            MyBase.Acao = Accion
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
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
        lblTituloDatosBanc.Text = Traduzir("039_lbl_TituloDatosBanc")
        csvClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvClienteObrigatorio")
        csvCodSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvCodSubClienteExistente")
        csvCodSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvCodSubClienteObrigatorio")
        csvDescSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvDescSubClienteObrigatorio")
        csvTipoSubClienteObrigatorio.ErrorMessage = Traduzir("039_msg_csvTipoSubClienteObrigatorio")
        csvDescSubClienteExistente.ErrorMessage = Traduzir("039_msg_csvDescSubClienteExistente")
        btnBuscarCliente.ExibirLabelBtn = False
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConfigurarControles()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadirTotalizador.Visible = True
                btnAnadirCuenta.Visible = True
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
                btnAnadirCuenta.Visible = True
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
                btnAnadirCuenta.Visible = False
                Me.ucTotSaldo.Desabilitar()

                btnBuscarCliente.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnAnadirTotalizador.Visible = False
                btnAnadirCuenta.Visible = False

        End Select

        txtCodigoAjeno.Enabled = False
        txtDesCodigoAjeno.Enabled = False

        If Cliente IsNot Nothing Then
            txtCodigoSubCliente.Enabled = True
            txtDescSubCliente.Enabled = True
            ddlTipoSubCliente.Enabled = True
            btnAltaAjeno.Habilitado = True
            chkTotSaldo.Enabled = True
            btnGrabar.Habilitado = True
            btnAnadirTotalizador.Habilitado = True
            btnAnadirCuenta.Habilitado = True
        ElseIf Accion <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            txtCodigoSubCliente.Enabled = False
            txtDescSubCliente.Enabled = False
            ddlTipoSubCliente.Enabled = False
            btnAltaAjeno.Habilitado = False
            chkTotSaldo.Enabled = False
            btnGrabar.Habilitado = False
            btnAnadirTotalizador.Habilitado = False
            btnAnadirCuenta.Habilitado = False
        End If

        Dim msg As String = MontaMensagensErro(False, False)
        If msg <> String.Empty Then
            Master.ControleErro.ShowError(msg, False)
        End If

        If Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

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

        End If

    End Sub

    Private Sub CargarTipoSubCliente()

        ddlTipoSubCliente.AppendDataBoundItems = True
        ddlTipoSubCliente.Items.Clear()

        For Each tipo In TiposSubCliente
            ddlTipoSubCliente.Items.Add(New ListItem(tipo.codTipoSubcliente + " - " + tipo.desTipoSubcliente, tipo.oidTipoSubcliente))
        Next

        ddlTipoSubCliente.OrdenarPorDesc()
        ddlTipoSubCliente.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Private Sub CargarDatos()

        Dim itemSelecionado As ListItem

        If SubCliente IsNot Nothing Then

            Dim iCodigoAjeno = (From item In SubCliente.CodigosAjenos
                                Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoSubCliente.Text = SubCliente.CodSubCliente
                txtCodigoSubCliente.ToolTip = SubCliente.CodSubCliente
            End If

            txtDescSubCliente.Text = SubCliente.DesSubCliente
            txtDescSubCliente.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, SubCliente.DesSubCliente, String.Empty)

            chkVigente.Checked = SubCliente.BolVigente
            chkTotSaldo.Checked = SubCliente.BolTotalizadorSaldo

            'Cliente
            txtCliente.Text = SubCliente.CodCliente & " - " & SubCliente.DesCliente
            txtCliente.ToolTip = SubCliente.CodCliente & " - " & SubCliente.DesCliente

            'Seleciona o valor divisa
            itemSelecionado = ddlTipoSubCliente.Items.FindByValue(SubCliente.OidTipoSubCliente)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoSubCliente.ToolTip = itemSelecionado.ToString
            End If

        End If

    End Sub

    Private Sub ExecutarGrabar()
        Try

            Dim _Proxy As New Comunicacion.ProxySubCliente
            Dim _Peticion As New SetSubClientes.Peticion
            Dim _Respuesta As SetSubClientes.Respuesta

            Dim _SubClienteSet As New SetSubClientes.SubCliente
            Dim _SubClientesSet As New SetSubClientes.SubClienteColeccion

            If MontaMensagensErro(True, True).Length > 0 Then
                Exit Sub
            End If

            _SubClienteSet.OidSubCliente = SubCliente.OidSubCliente
            _SubClienteSet.OidCliente = Cliente.OidCliente
            _SubClienteSet.CodCliente = Cliente.Codigo
            _SubClienteSet.CodSubCliente = txtCodigoSubCliente.Text
            _SubClienteSet.DesSubCliente = txtDescSubCliente.Text
            _SubClienteSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _SubClienteSet.BolVigente = chkVigente.Checked
            _SubClienteSet.OidTipoSubCliente = ddlTipoSubCliente.SelectedValue
            _SubClienteSet.CodigoAjeno = CodigosAjenos
            _SubClienteSet.ConfigNivelSaldo = ConverteNivelSaldo(Me.ucTotSaldo.TotalizadoresSaldos)
            _SubClienteSet.BolSubClienteTotSaldo = chkTotSaldo.Checked
            _SubClienteSet.Direcciones = Direcciones
            _SubClienteSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion()

            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _SubClienteSet.ConfigNivelSaldo IsNot Nothing AndAlso _SubClienteSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _SubClienteSet.ConfigNivelSaldo.FindAll(Function(x) x.oidSubCliente = SubCliente.OidSubCliente)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidSubcliente = SubCliente.OidSubCliente Then
                            nivel.configNivelSaldo.oidSubcliente = Nothing
                        End If
                        nivel.oidSubCliente = Nothing
                    Next
                End If
                _SubClienteSet.OidSubCliente = Nothing
            Else
                _SubClienteSet.OidSubCliente = SubCliente.OidSubCliente
            End If
            ' FIM POG 

            _SubClientesSet.Add(_SubClienteSet)

            _Respuesta = _Proxy.SetSubClientes(New SetSubClientes.Peticion With {.SubClientes = _SubClientesSet, .CodigoUsuario = MyBase.LoginUsuario})

            If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then

                If Master.ControleErro.VerificaErro(_Respuesta.SubClientes(0).CodigoError, _Respuesta.NombreServidorBD, _Respuesta.SubClientes(0).MensajeError) Then

                    If Master.ControleErro.VerificaErro(_Respuesta.SubClientes(0).CodigoError, _Respuesta.SubClientes(0).NombreServidorBD, _Respuesta.SubClientes(0).MensajeError) Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaSubClientes.aspx');", True)
                    Else
                        Master.ControleErro.ShowError(_Respuesta.SubClientes(0).MensajeError, False)
                    End If

                    Session.Remove("DireccionPeticion")

                End If

            Else

                If _Respuesta.SubClientes IsNot Nothing AndAlso _Respuesta.SubClientes.Count > 0 AndAlso _Respuesta.SubClientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(_Respuesta.SubClientes(0).MensajeError, False)
                ElseIf _Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    Master.ControleErro.ShowError(_Respuesta.MensajeError, False)
                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Function MontaMensagensErro(ValidarCamposObrigatorios As Boolean, SetarFocoControle As Boolean) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                'Verifica se o código do canal é obrigatório
                If Cliente Is Nothing OrElse txtCliente.Text.Equals(String.Empty) Then

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

    Private Function ExisteCodigoSubCliente(codigoCliente As String, codigoSubCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoSubCliente.Text) Then

            Try

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New IAC.ContractoServicio.Utilidad.VerificarCodigoSubCliente.Peticion
                Dim _Respuesta As VerificarCodigoSubCliente.Respuesta

                _Peticion.CodCliente = codigoCliente.Trim
                _Peticion.CodSubCliente = codigoSubCliente.Trim
                _Respuesta = _Proxy.VerificarCodigoSubCliente(_Peticion)

                If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    If _Respuesta.Existe Then
                        Return True
                    End If
                Else
                    Master.ControleErro.ShowError(_Respuesta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function

    Private Function ExisteDescricaoSubCliente(codigoCliente As String, descricaoSubCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescSubCliente.Text) Then

            Try

                Dim _Proxy As New Comunicacion.ProxyUtilidad
                Dim _Peticion As New VerificarDescripcionSubCliente.Peticion
                Dim _Resposta As VerificarDescripcionSubCliente.Respuesta

                _Peticion.CodCliente = codigoCliente.Trim
                _Peticion.DesSubCliente = descricaoSubCliente.Trim
                _Resposta = _Proxy.VerificarDescripcionSubCliente(_Peticion)

                If Master.ControleErro.VerificaErro(_Resposta.CodigoError, _Resposta.NombreServidorBD, _Resposta.MensajeError) Then
                    If _Resposta.Existe Then
                        Return True
                    End If
                Else
                    Master.ControleErro.ShowError(_Resposta.MensajeError)
                    Return False
                End If

            Catch ex As Exception
                Master.ControleErro.TratarErroException(ex)
            End Try

        End If

    End Function

    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)

        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then

            'Localiza próprio totalizador de saldo
            Dim _TotalizadorSaldo = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Cliente.OidCliente _
                                                          AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = SubCliente.OidSubCliente _
                                                          AndAlso a.PuntoServicio Is Nothing _
                                                          AndAlso a.SubCanales IsNot Nothing _
                                                          AndAlso a.SubCanales.Count > 1)

            Dim _auxSubCanales As List(Of Comon.Clases.SubCanal) = SubCanales
            If Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                    If _totalizador.SubCanales IsNot Nothing AndAlso _totalizador.SubCanales.Count = 1 Then
                        _auxSubCanales.Remove(_auxSubCanales.FirstOrDefault(Function(s) s.Identificador = _totalizador.SubCanales(0).Identificador))
                    End If
                Next
            End If

            If _TotalizadorSaldo IsNot Nothing AndAlso Aplicacao.Util.Utilidad.CompararTotalizadores(_TotalizadorSaldo.SubCanales,
                                                                                                     _TotalizadorSaldo.Cliente.Identificador,
                                                                                                     _TotalizadorSaldo.SubCliente.Identificador, "",
                                                                                                     _auxSubCanales,
                                                                                                     Cliente.OidCliente,
                                                                                                     SubCliente.OidSubCliente, "") Then

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(_TotalizadorSaldo)
                If _TotalizadorSaldo.bolDefecto Then
                    If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales,
                                                                     "",
                                                                     "",
                                                                     "",
                                                                     _auxSubCanales,
                                                                     "",
                                                                     "",
                                                                     "") Then
                                _totalizador.bolDefecto = True
                                Exit For
                            End If
                        Next
                    End If

                End If
            End If


        Else

            Dim _TotalizadorSaldo As Comon.Clases.TotalizadorSaldo = Nothing

            If SubCanales IsNot Nothing AndAlso SubCanales.Count > 0 Then

                _TotalizadorSaldo = New Comon.Clases.TotalizadorSaldo

                With _TotalizadorSaldo

                    If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                        SubCliente.CodSubCliente = txtCodigoSubCliente.Text
                        SubCliente.DesSubCliente = txtDescSubCliente.Text
                    End If

                    If Cliente IsNot Nothing Then
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = Cliente.OidCliente
                        .Cliente.Codigo = Cliente.Codigo
                        .Cliente.Descripcion = Cliente.Descripcion
                    End If

                    If SubCliente IsNot Nothing Then
                        .SubCliente = New Comon.Clases.SubCliente
                        .SubCliente.Identificador = SubCliente.OidSubCliente
                        .SubCliente.Codigo = SubCliente.CodSubCliente
                        .SubCliente.Descripcion = SubCliente.DesSubCliente
                    End If

                    Me.ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                    Me.ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente

                    .SubCanales = SubCanales.OrderBy(Function(a) a.Descripcion).ToList()

                    .bolDefecto = True
                    If Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If _totalizador.SubCanales IsNot Nothing AndAlso _totalizador.SubCanales.Count = 1 Then
                                For Each _subCanal In _totalizador.SubCanales
                                    .SubCanales.Remove(.SubCanales.FirstOrDefault(Function(s) s.Identificador = _subCanal.Identificador))
                                Next
                            End If
                        Next

                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales,
                                                                     "",
                                                                     "",
                                                                     "",
                                                                     .SubCanales,
                                                                     "",
                                                                     "",
                                                                     "") Then
                                _totalizador.bolDefecto = False
                            End If
                        Next

                    End If

                End With

            End If

            Me.ucTotSaldo.TotalizadoresSaldos.Add(_TotalizadorSaldo)

        End If

        Me.ucTotSaldo.AtualizaGrid()
        upTotSaldo.Update()
    End Sub

    Private Function ConverteNivelSaldo(lstTotSaldo As List(Of Comon.Clases.TotalizadorSaldo)) As ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMovColeccion

        For Each nivelSaldo In lstTotSaldo

            Dim peticionNivelSaldo As New ContractoServicio.SubCliente.SetSubClientes.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True

                .oidCliente = Cliente.OidCliente
                .codCliente = Cliente.Codigo
                .desCliente = Cliente.Descripcion

                .oidSubCliente = SubCliente.OidSubCliente
                .codSubCliente = SubCliente.CodSubCliente
                .desSubCliente = SubCliente.DesSubCliente

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

#End Region

#Region "[EVENTOS]"

    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click
        Response.Redirect("~/BusquedaSubClientes.aspx", False)
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/BusquedaSubClientes.aspx", False)
    End Sub

    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Protected Sub btnAnadirTotalizador_Click(sender As Object, e As System.EventArgs) Handles btnAnadirTotalizador.Click
        Me.ucTotSaldo.Cambiar(-1)
    End Sub

    Protected Sub btnAnadirCuenta_Click(sender As Object, e As System.EventArgs) Handles btnAnadirCuenta.Click
        Me.ucDatosBanc.Cambiar(-1)
    End Sub

    Protected Sub btnAltaAjeno_Click(sender As Object, e As System.EventArgs) Handles btnAltaAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoSubCliente.Text
        tablaGenesis.DesTablaGenesis = txtDescSubCliente.Text
        tablaGenesis.OidTablaGenesis = SubCliente.OidSubCliente
        If SubCliente IsNot Nothing AndAlso SubCliente.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = SubCliente.CodigosAjenos
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

    Protected Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = txtCodigoSubCliente.Text
        tablaGenes.DesGenesis = txtDescSubCliente.Text
        tablaGenes.OidGenesis = SubCliente.OidSubCliente
        If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(SubCliente.OidSubCliente) Then
            If SubCliente.Direcciones IsNot Nothing AndAlso SubCliente.Direcciones.Count > 0 AndAlso SubCliente.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = SubCliente.Direcciones.FirstOrDefault
            End If
        ElseIf Direcciones IsNot Nothing Then
            tablaGenes.Direcion = Direcciones.FirstOrDefault
        End If

        Session("objGEPR_TSUBCLIENTE") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSUBCLIENTE"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSUBCLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
    End Sub

    Protected Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Try
            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Sub chkTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkTotSaldo.CheckedChanged

        chkProprioTotSaldo.Enabled = chkTotSaldo.Checked
        If Not chkTotSaldo.Checked Then
            chkProprioTotSaldo.Checked = False
            AddRemoveTotalizadorSaldoProprio(True)
        End If
        upChkProprioTotSaldo.Update()

    End Sub

    Protected Sub chkProprioTotSaldo_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkProprioTotSaldo.CheckedChanged
        AddRemoveTotalizadorSaldoProprio(Not chkProprioTotSaldo.Checked)
    End Sub

    Protected Sub chkVigente_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkVigente.CheckedChanged
        If chkVigente.Checked Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_Aviso", "alert('" & Traduzir("038_lbl_SubclienteAvisoAtivo") & "');", True)
        End If
    End Sub

    Protected Sub txtCodigoSubCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoSubCliente.TextChanged
        Try
            If Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtCodigoSubCliente.Text) Then
                If ExisteCodigoSubCliente(Cliente.Codigo, txtCodigoSubCliente.Text) Then
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

    Protected Sub txtDescSubCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescSubCliente.TextChanged
        Try
            If Cliente IsNot Nothing AndAlso Not String.IsNullOrEmpty(txtDescSubCliente.Text) Then
                If ExisteDescricaoSubCliente(Cliente.Codigo, txtDescSubCliente.Text) Then
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

    Protected Sub ucTotSaldo_DadosCarregados(sender As Object, args As System.EventArgs)
        If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing Then

            If Me.ucTotSaldo.TotalizadoresSaldos.Exists(Function(a) a.Cliente.Identificador = Cliente.OidCliente _
                                                            AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = SubCliente.OidSubCliente _
                                                            AndAlso a.PuntoServicio Is Nothing _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub

#End Region

End Class