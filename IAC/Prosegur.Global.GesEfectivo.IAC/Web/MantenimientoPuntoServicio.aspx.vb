Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoPuntosServicio
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.PuntoServicio
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class MantenimientoPuntoServicio
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

            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidCliente) Then
                ViewState("_Cliente") = New GetComboClientes.Cliente With {.OidCliente = PuntoServicio.OidCliente, .Codigo = PuntoServicio.CodCliente, .Descripcion = PuntoServicio.DesCliente}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As GetComboClientes.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Public Property SubCliente As GetComboSubclientesByCliente.SubCliente
        Get

            If ViewState("_SubCliente") Is Nothing AndAlso Session("SubClientesSelecionados") IsNot Nothing Then

                Dim _SubClientes As GetComboSubclientesByCliente.SubClienteColeccion = Session("SubClientesSelecionados")
                If _SubClientes IsNot Nothing AndAlso _SubClientes.Count > 0 Then

                    ViewState("_SubCliente") = _SubClientes(0)

                    ' setar controles da tela
                    txtSubCliente.Text = ViewState("_SubCliente").Codigo & " - " & ViewState("_SubCliente").Descripcion
                    txtSubCliente.ToolTip = ViewState("_SubCliente").Codigo & " - " & ViewState("_SubCliente").Descripcion

                End If
                Session("SubClientesSelecionados") = Nothing
            End If

            If ViewState("_SubCliente") Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidSubCliente) Then
                ViewState("_SubCliente") = New GetComboSubclientesByCliente.SubCliente With {.OidSubCliente = PuntoServicio.OidSubCliente, .Codigo = PuntoServicio.CodSubCliente, .Descripcion = PuntoServicio.DesSubCliente}
            End If

            Return ViewState("_SubCliente")
        End Get
        Set(value As GetComboSubclientesByCliente.SubCliente)
            ViewState("_SubCliente") = value
        End Set
    End Property

    Public Property PuntoServicio As GetPuntoServicioDetalle.PuntoServicio
        Get
            If ViewState("_PuntoServicio") Is Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("OidPuntoServicio")) Then

                Dim _Proxy As New Comunicacion.ProxyPuntoServicio
                Dim _Peticion As New GetPuntoServicioDetalle.Peticion
                Dim _Respuesta As GetPuntoServicioDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidPuntoServicio = Request.QueryString("OidPuntoServicio")
                _Respuesta = _Proxy.GetPuntoServicioDetalle(_Peticion)
                If _Respuesta.PuntoServicio IsNot Nothing AndAlso _Respuesta.PuntoServicio.Count > 0 Then
                    ViewState("_PuntoServicio") = _Respuesta.PuntoServicio(0)
                End If

            End If

            If ViewState("_PuntoServicio") Is Nothing AndAlso Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                ViewState("_PuntoServicio") = New GetPuntoServicioDetalle.PuntoServicio With {.OidPuntoServicio = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_PuntoServicio")
        End Get
        Set(value As GetPuntoServicioDetalle.PuntoServicio)
            ViewState("_PuntoServicio") = value
        End Set
    End Property

    Private _TiposPuntosServicio As getTiposPuntosServicio.TipoPuntosServicioColeccion
    Public ReadOnly Property TiposPuntosServicio As getTiposPuntosServicio.TipoPuntosServicioColeccion
        Get
            If _TiposPuntosServicio Is Nothing Then

                Dim _Peticion As New getTiposPuntosServicio.Peticion
                Dim _Respuesta As New getTiposPuntosServicio.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoPuntoServicio

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposPuntosServicio(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    Throw New Exception("Error al obtener Tipos de Punto Servicio")
                End If

                _TiposPuntosServicio = _Respuesta.TipoPuntoServicio
            End If
            Return _TiposPuntosServicio
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
                PuntoServicio.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return PuntoServicio.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            PuntoServicio.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TPUNTO_SERVICIO") IsNot Nothing Then

                PuntoServicio.CodigosAjenos = Session("objRespuestaGEPR_TPUNTO_SERVICIO")
                Session.Remove("objRespuestaGEPR_TPUNTO_SERVICIO")

                Dim iCodigoAjeno = (From item In PuntoServicio.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If PuntoServicio.CodigosAjenos Is Nothing Then
                    PuntoServicio.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objRespuestaGEPR_TPUNTO_SERVICIO") = PuntoServicio.CodigosAjenos

            End If

            Return PuntoServicio.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            PuntoServicio.CodigosAjenos = value
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

            If Not (Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                    Accion = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                    Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
                Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                Throw New Exception(Traduzir("err_passagem_parametro"))
            End If

            If Not Page.IsPostBack Then

                CargarTipoPuntoServicio()

                If Accion <> Aplicacao.Util.Utilidad.eAcao.Alta Then
                    CargarDatos()
                End If

            End If

            ucDatosBanc.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio
            ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio

            If Cliente IsNot Nothing Then
                ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                ucDatosBanc.Cliente.Identificador = Cliente.OidCliente
            End If

            If SubCliente IsNot Nothing Then
                ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
                ucDatosBanc.SubCliente.Identificador = SubCliente.OidSubCliente
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
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.TituloPagina = Traduzir("042_titulo_mantenimiento")
        lblTituloPuntoServicio.Text = Traduzir("042_titulo_mantenimiento")
        lblCliente.Text = Traduzir("042_lbl_cliente")
        lblSubCliente.Text = Traduzir("042_lbl_subcliente")
        lblCodPuntoServicio.Text = Traduzir("042_lbl_CodPuntoServicio")
        lblCodigoAjeno.Text = Traduzir("042_lbl_CodigoAjeno")
        lblDescPuntoServicio.Text = Traduzir("042_lbl_DescPuntoServicio")
        lblDesCodigoAjeno.Text = Traduzir("042_lbl_DesCodigoAjeno")
        lblTipoPuntoServicio.Text = Traduzir("042_lbl_TipoPuntoServicio")
        lblTituloPuntoServicio.Text = Traduzir("042_lbl_TituloPuntoServicio")
        lblTotSaldo.Text = Traduzir("042_lbl_TotSaldo")
        lblVigente.Text = Traduzir("042_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("042_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("042_lbl_ProprioTotSaldo")
        lblTituloDatosBanc.Text = Traduzir("042_lbl_TituloDatosBanc")
        csvClienteObrigatorio.ErrorMessage = Traduzir("042_msg_csvClienteObrigatorio")
        csvSubClienteObrigatorio.ErrorMessage = Traduzir("042_msg_csvSubClienteObrigatorio")
        csvCodPuntoServicioExistente.ErrorMessage = Traduzir("042_msg_csvCodPuntoServicioExistente")
        csvCodPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvCodPuntoServicioObrigatorio")
        csvDescPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvDescPuntoServicioObrigatorio")
        csvDescPuntoServicioExistente.ErrorMessage = Traduzir("042_msg_csvDescPuntoServicioExistente")
        csvTipoPuntoServicioObrigatorio.ErrorMessage = Traduzir("042_msg_csvTipoPuntoServicioObrigatorio")
        btnBuscarCliente.ExibirLabelBtn = False
        btnBuscarSubCliente.ExibirLabelBtn = False
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

                If Not IsPostBack Then
                    chkVigente.Checked = True
                    chkVigente.Enabled = False
                End If
                Me.ucTotSaldo.Habilitar()

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False

                If Not IsPostBack Then
                    If chkVigente.Checked = True Then
                        chkVigente.Enabled = False
                    End If
                End If

                btnAnadirTotalizador.Visible = True
                Me.ucTotSaldo.Habilitar()
                txtCodigoPuntoServicio.ReadOnly = True
                txtCodigoPuntoServicio.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                txtCliente.Enabled = False
                txtSubCliente.Enabled = False
                txtCodigoPuntoServicio.Enabled = False
                txtDescPuntoServicio.Enabled = False
                ddlTipoPuntoServicio.Enabled = False
                chkVigente.Enabled = False
                chkTotSaldo.Enabled = False
                btnAnadirTotalizador.Visible = False
                Me.ucTotSaldo.Desabilitar()

                btnBuscarCliente.Visible = False
                btnBuscarSubCliente.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnAnadirTotalizador.Visible = False

        End Select

        If Cliente IsNot Nothing Then
            btnBuscarSubCliente.Habilitado = True
        ElseIf Accion <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            btnBuscarSubCliente.Habilitado = False
        End If

        If SubCliente IsNot Nothing Then
            txtCodigoPuntoServicio.Enabled = True
            txtDescPuntoServicio.Enabled = True
            btnDireccion.Habilitado = True
            btnCodigoAjeno.Habilitado = True
            ddlTipoPuntoServicio.Enabled = True
            chkTotSaldo.Enabled = True
            btnGrabar.Habilitado = True
            btnAnadirTotalizador.Habilitado = True
            btnAnadirCuenta.Habilitado = True
        ElseIf Accion <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            txtCodigoPuntoServicio.Enabled = False
            txtDescPuntoServicio.Enabled = False
            btnDireccion.Habilitado = False
            btnCodigoAjeno.Habilitado = False
            ddlTipoPuntoServicio.Enabled = False
            chkTotSaldo.Enabled = False
            btnGrabar.Habilitado = False
            btnAnadirTotalizador.Habilitado = False
            btnAnadirCuenta.Habilitado = False
        End If

        txtCodigoAjeno.Enabled = False
        txtDesCodigoAjeno.Enabled = False

        Dim msg As String = MontaMensagensErro(False, False)
        If Not String.IsNullOrEmpty(msg) Then
            Master.ControleErro.ShowError(msg, False)
        End If

        If Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
            ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = Prosegur.Genesis.ContractoServicio.Constantes.CONST_COD_PARAMETRO_MANTENIMIENTO_CLIENTES_DIVISAS_POR_PANTALLA).First.ValorParametro
            '        Caso el parámetro MantenimientoClientesDivisasPorPantalla – parámetro del IAC a nivel de país – sea igual que 0:
            '-       Permite el mantenimiento solamente de los códigos ajenos y totalizadores de saldo.
            '-       Permite consultar puntos de servicios.
            If Not ParametroMantenimientoClientesDivisasPorPantalla Then

                txtCliente.Enabled = False
                btnBuscarCliente.Habilitado = False
                txtSubCliente.Enabled = False
                btnBuscarSubCliente.Habilitado = False
                txtCodigoPuntoServicio.Enabled = False
                txtDescPuntoServicio.Enabled = False
                btnDireccion.Habilitado = False
                ddlTipoPuntoServicio.Enabled = False
                txtCodigoAjeno.Enabled = True
                txtDesCodigoAjeno.Enabled = True
                chkTotSaldo.Enabled = True
            Else
                txtCodigoAjeno.Enabled = False
                txtDesCodigoAjeno.Enabled = False
            End If
        End If
        
    End Sub

    Private Sub CargarDatos()

        Dim itemSelecionado As ListItem

        If PuntoServicio IsNot Nothing Then

            Dim iCodigoAjeno = (From item In PuntoServicio.CodigosAjenos
                   Where item.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoPuntoServicio.Text = PuntoServicio.CodPuntoServicio
                txtCodigoPuntoServicio.ToolTip = PuntoServicio.CodPuntoServicio
            End If

            txtDescPuntoServicio.Text = PuntoServicio.DesPuntoServicio
            txtDescPuntoServicio.ToolTip = PuntoServicio.DesPuntoServicio

            chkVigente.Checked = PuntoServicio.BolVigente
            chkTotSaldo.Checked = PuntoServicio.BolTotalizadorSaldo

            'SubCliente
            txtSubCliente.Text = PuntoServicio.CodSubCliente & " - " & PuntoServicio.DesSubCliente
            txtSubCliente.ToolTip = PuntoServicio.CodSubCliente & " - " & PuntoServicio.DesSubCliente

            'Cliente
            txtCliente.Text = PuntoServicio.CodCliente & " - " & PuntoServicio.DesCliente
            txtCliente.ToolTip = PuntoServicio.CodCliente & " - " & PuntoServicio.DesCliente

            'Seleciona o valor divisa
            itemSelecionado = ddlTipoPuntoServicio.Items.FindByValue(PuntoServicio.OidTipoPuntoServicio)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoPuntoServicio.ToolTip = itemSelecionado.ToString
            End If

        End If

    End Sub

    Private Sub CargarTipoPuntoServicio()

        ddlTipoPuntoServicio.AppendDataBoundItems = True
        ddlTipoPuntoServicio.Items.Clear()

        For Each tipo In TiposPuntosServicio
            ddlTipoPuntoServicio.Items.Add(New ListItem(tipo.codTipoPuntoServicio + " - " + tipo.desTipoPuntoServicio, tipo.oidTipoPuntoServicio))
        Next

        ddlTipoPuntoServicio.OrdenarPorDesc()
        ddlTipoPuntoServicio.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Private Sub ExecutarGrabar()
        Try

            Dim _Proxy As New Comunicacion.ProxyPuntoServicio
            Dim _Peticion As New SetPuntoServicio.Peticion
            Dim _Respuesta As SetPuntoServicio.Respuesta

            Dim _PuntoServicioSet As New SetPuntoServicio.PuntoServicio
            Dim _PuntosServicioSet As New SetPuntoServicio.PuntoServicioColeccion

            If MontaMensagensErro(True, True).Length > 0 Then
                Exit Sub
            End If

            _PuntoServicioSet.OidPuntoServicio = PuntoServicio.OidPuntoServicio
            _PuntoServicioSet.CodCliente = Cliente.Codigo
            _PuntoServicioSet.OidSubCliente = SubCliente.OidSubCliente
            _PuntoServicioSet.CodSubCliente = SubCliente.Codigo
            _PuntoServicioSet.CodPuntoServicio = txtCodigoPuntoServicio.Text
            _PuntoServicioSet.DesPuntoServicio = txtDescPuntoServicio.Text
            _PuntoServicioSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _PuntoServicioSet.BolVigente = chkVigente.Checked
            _PuntoServicioSet.OidTipoPuntoServicio = ddlTipoPuntoServicio.SelectedValue
            _PuntoServicioSet.CodigoAjeno = CodigosAjenos
            _PuntoServicioSet.ConfigNivelSaldo = ConverteNivelSaldo(Me.ucTotSaldo.TotalizadoresSaldos)
            _PuntoServicioSet.BolPuntoServicioTotSaldo = chkTotSaldo.Checked
            _PuntoServicioSet.Direcciones = Direcciones
            _PuntoServicioSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion

            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _PuntoServicioSet.ConfigNivelSaldo IsNot Nothing AndAlso _PuntoServicioSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _PuntoServicioSet.ConfigNivelSaldo.FindAll(Function(x) x.oidPtoServicio = PuntoServicio.OidPuntoServicio)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidPtoServicio = PuntoServicio.OidPuntoServicio Then
                            nivel.configNivelSaldo.oidPtoServicio = Nothing
                        End If
                        nivel.oidPtoServicio = Nothing
                    Next
                End If
                _PuntoServicioSet.OidPuntoServicio = Nothing
            Else
                _PuntoServicioSet.OidPuntoServicio = PuntoServicio.OidPuntoServicio
            End If

            ' FIM POG 
            _PuntosServicioSet.Add(_PuntoServicioSet)
            _Peticion.PuntoServicio = _PuntosServicioSet
            _Peticion.CodigoUsuario = MyBase.LoginUsuario

            _Respuesta = _Proxy.SetPuntoServicio(_Peticion)

            If Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then

                If Master.ControleErro.VerificaErro(_Respuesta.PuntoServicio(0).CodigoError, _Respuesta.NombreServidorBD, _Respuesta.PuntoServicio(0).MensajeError) Then

                    If Master.ControleErro.VerificaErro(_Respuesta.PuntoServicio(0).CodigoError, _Respuesta.PuntoServicio(0).NombreServidorBD, _Respuesta.PuntoServicio(0).MensajeError) Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaPuntoServicio.aspx');", True)
                    Else
                        Master.ControleErro.ShowError(_Respuesta.PuntoServicio(0).MensajeError, False)
                    End If

                    Session.Remove("DireccionPeticion")

                End If

            Else

                If _Respuesta.PuntoServicio IsNot Nothing AndAlso _Respuesta.PuntoServicio.Count > 0 AndAlso _Respuesta.PuntoServicio(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(_Respuesta.PuntoServicio(0).MensajeError, False)
                ElseIf _Respuesta.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    Master.ControleErro.ShowError(_Respuesta.MensajeError, False)
                End If

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Private Function ExisteCodigoPuntoServicio(codigoCliente As String, codigoSubCliente As String, codigoPtoServicio As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoPuntoServicio.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarCodigoPtoServicio.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.CodSubCliente = codigoSubCliente.Trim
                objPeticion.CodPtoServicio = codigoPtoServicio.Trim
                objResposta = objProxyUtilidad.VerificarCodigoPtoServicio(objPeticion)

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

    Private Function ExisteDescricaoPuntoServicio(codigoCliente As String, codigoSubCliente As String, descricaoPtoServicio As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescPuntoServicio.Text) Then

            Dim objResposta As IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.VerificarDescripcionPtoServicio.Peticion

                objPeticion.CodCliente = codigoCliente.Trim
                objPeticion.CodSubCliente = codigoSubCliente.Trim
                objPeticion.DesPtoServicio = descricaoPtoServicio.Trim
                objResposta = objProxyUtilidad.VerificarDescripcionPtoServicio(objPeticion)

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

    Private Function MontaMensagensErro(ValidarCamposObrigatorios As Boolean, SetarFocoControle As Boolean) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            If ValidarCamposObrigatorios Then

                If SubCliente Is Nothing OrElse txtSubCliente.Text.Equals(String.Empty) Then

                    strErro.Append(csvSubClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvSubClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtSubCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvSubClienteObrigatorio.IsValid = True
                End If

                If Cliente Is Nothing OrElse txtCliente.Text.Equals(String.Empty) Then

                    strErro.Append(csvClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvClienteObrigatorio.IsValid = True
                End If

                If ddlTipoPuntoServicio.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoPuntoServicio.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoPuntoServicioObrigatorio.IsValid = True
                End If


                If txtCodigoPuntoServicio.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoPuntoServicio.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodPuntoServicioObrigatorio.IsValid = True
                End If

                If txtDescPuntoServicio.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescPuntoServicioObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescPuntoServicioObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescPuntoServicio.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescPuntoServicioObrigatorio.IsValid = True
                End If

            End If

            If CodigoExistente Then

                strErro.Append(csvCodPuntoServicioExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodPuntoServicioExistente.IsValid = False

                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoPuntoServicio.Focus()
                    focoSetado = True
                End If

            Else
                csvCodPuntoServicioExistente.IsValid = True
            End If

            If DescricaoExistente Then

                strErro.Append(csvDescPuntoServicioExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvDescPuntoServicioExistente.IsValid = False

                If SetarFocoControle AndAlso Not focoSetado Then
                    txtDescPuntoServicio.Focus()
                    focoSetado = True
                End If

            Else
                csvDescPuntoServicioExistente.IsValid = True
            End If

        End If

        Return strErro.ToString

    End Function

    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)
        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then
            Dim objTotSaldoProprio = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Me.Cliente.OidCliente _
                                                          AndAlso a.SubCliente IsNot Nothing AndAlso a.SubCliente.Identificador = Me.SubCliente.OidSubCliente _
                                                          AndAlso a.PuntoServicio IsNot Nothing AndAlso a.PuntoServicio.Identificador = Me.PuntoServicio.OidPuntoServicio _
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

            Dim _TotalizadorSaldo As Comon.Clases.TotalizadorSaldo = Nothing

            If SubCanales IsNot Nothing AndAlso SubCanales.Count > 0 Then

                _TotalizadorSaldo = New Comon.Clases.TotalizadorSaldo

                With _TotalizadorSaldo

                    If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                        PuntoServicio.CodPuntoServicio = txtCodigoPuntoServicio.Text
                        PuntoServicio.DesPuntoServicio = txtDescPuntoServicio.Text
                    End If

                    If Me.Cliente IsNot Nothing Then
                        .Cliente = New Comon.Clases.Cliente
                        .Cliente.Identificador = Me.Cliente.OidCliente
                        .Cliente.Codigo = Me.Cliente.Codigo
                        .Cliente.Descripcion = Me.Cliente.Descripcion
                    End If

                    If Me.SubCliente IsNot Nothing Then
                        .SubCliente = New Comon.Clases.SubCliente
                        .SubCliente.Identificador = Me.SubCliente.OidSubCliente
                        .SubCliente.Codigo = Me.SubCliente.Codigo
                        .SubCliente.Descripcion = Me.SubCliente.Descripcion
                    End If

                    If Me.PuntoServicio IsNot Nothing Then
                        .PuntoServicio = New Comon.Clases.PuntoServicio
                        .PuntoServicio.Identificador = Me.PuntoServicio.OidPuntoServicio
                        .PuntoServicio.Codigo = Me.PuntoServicio.CodPuntoServicio
                        .PuntoServicio.Descripcion = Me.PuntoServicio.DesPuntoServicio
                    End If

                    Me.ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
                    Me.ucTotSaldo.IdentificadorSubCliente = SubCliente.OidSubCliente
                    Me.ucTotSaldo.IdentificadorPuntoServicio = PuntoServicio.OidPuntoServicio

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
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales, _
                                                                     "", _
                                                                     "", _
                                                                     "", _
                                                                     .SubCanales, _
                                                                     "", _
                                                                     "", _
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

    Private Function ConverteNivelSaldo(lstTotSaldo As List(Of Comon.Clases.TotalizadorSaldo)) As ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMovColeccion

        For Each nivelSaldo In lstTotSaldo

            Dim peticionNivelSaldo As New ContractoServicio.PuntoServicio.SetPuntoServicio.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True

                .oidCliente = Me.Cliente.OidCliente
                .codCliente = Me.Cliente.Codigo
                .desCliente = Me.Cliente.Descripcion

                .oidSubCliente = Me.SubCliente.OidSubCliente
                .codSubCliente = Me.SubCliente.Codigo
                .desSubCliente = Me.SubCliente.Descripcion

                .oidPtoServicio = Me.PuntoServicio.OidPuntoServicio
                .codPtoServicio = Me.PuntoServicio.CodPuntoServicio
                .desPtoServicio = Me.PuntoServicio.DesPuntoServicio

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
        Response.Redirect("~/BusquedaPuntoServicio.aspx", False)
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/BusquedaPuntoServicio.aspx", False)
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

    Protected Sub btnCodigoAjeno_Click(sender As Object, e As System.EventArgs) Handles btnCodigoAjeno.Click
        Dim url As String = String.Empty
        Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

        tablaGenesis.CodTablaGenesis = txtCodigoPuntoServicio.Text
        tablaGenesis.DesTablaGenesis = txtDescPuntoServicio.Text
        tablaGenesis.OidTablaGenesis = PuntoServicio.OidPuntoServicio
        If PuntoServicio IsNot Nothing AndAlso PuntoServicio.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = PuntoServicio.CodigosAjenos
        End If

        Session("objPeticionGEPR_TPUNTO_SERVICIO") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TPUNTO_SERVICIO") = tablaGenesis

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnCodigoAjeno');", True)
    End Sub

    Protected Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = txtCodigoPuntoServicio.Text
        tablaGenes.DesGenesis = txtDescPuntoServicio.Text
        tablaGenes.OidGenesis = PuntoServicio.OidPuntoServicio
        If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(PuntoServicio.OidPuntoServicio) Then
            If PuntoServicio.Direcciones IsNot Nothing AndAlso PuntoServicio.Direcciones.Count > 0 AndAlso PuntoServicio.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = PuntoServicio.Direcciones.FirstOrDefault
            End If
        ElseIf Direcciones IsNot Nothing Then
            tablaGenes.Direcion = Direcciones.FirstOrDefault
        End If

        Session("objGEPR_TPUNTO_SERVICIO") = tablaGenes

        If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TPUNTO_SERVICIO"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
    End Sub

    Protected Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click

        Try

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True" & "&vigente=True"

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Sub btnBuscarSubCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarSubCliente.Click

        Try

            If Cliente IsNot Nothing Then
                Dim url As String = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&SelecionaColecaoClientes=false&SelecaoUnica=True&vigente=True"
                Session("objCliente") = Cliente
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_SubClientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarSubCliente');", True)
            Else
                ScriptManager.RegisterStartupScript(Me.Page, Page.GetType(), "erro_cliente_vazio", _
                                                  String.Format("alert('{0}');", Traduzir(Aplicacao.Util.Utilidad.InfoMsgSeleccionarCliente)), True)
            End If

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

    Protected Sub txtCodigoPuntoServicio_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoPuntoServicio.TextChanged
        Try

            If Cliente IsNot Nothing AndAlso SubCliente IsNot Nothing Then
                If ExisteCodigoPuntoServicio(Cliente.Codigo, SubCliente.Codigo, txtCodigoPuntoServicio.Text) Then
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

    Protected Sub txtDescPuntoServicio_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescPuntoServicio.TextChanged
        Try

            If Cliente IsNot Nothing AndAlso SubCliente IsNot Nothing Then
                If ExisteDescricaoPuntoServicio(Cliente.Codigo, SubCliente.Codigo, txtDescPuntoServicio.Text) Then
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
                                                            AndAlso a.PuntoServicio IsNot Nothing AndAlso a.PuntoServicio.Identificador = PuntoServicio.OidPuntoServicio _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub

#End Region

End Class