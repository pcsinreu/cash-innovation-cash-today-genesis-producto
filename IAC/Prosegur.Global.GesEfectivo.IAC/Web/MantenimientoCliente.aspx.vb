Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.TipoCliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Cliente
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports Prosegur.Genesis

Public Class MantenimientoCliente
    Inherits Base

#Region "[PROPRIEDADES]"

    Public Property Cliente As GetClientesDetalle.Cliente
        Get
            If ViewState("_Cliente") Is Nothing AndAlso Not String.IsNullOrEmpty(Request.QueryString("oidCliente")) Then

                Dim _Proxy As New Comunicacion.ProxyCliente
                Dim _Peticion As New GetClientesDetalle.Peticion
                Dim _Respuesta As GetClientesDetalle.Respuesta

                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Peticion.OidCliente = Request.QueryString("oidCliente")
                _Respuesta = _Proxy.GetClientesDetalle(_Peticion)
                If _Respuesta.Clientes IsNot Nothing AndAlso _Respuesta.Clientes.Count > 0 Then
                    ViewState("_Cliente") = _Respuesta.Clientes(0)
                End If

            End If

            If ViewState("_Cliente") Is Nothing AndAlso Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                ViewState("_Cliente") = New GetClientesDetalle.Cliente With {.OidCliente = System.Guid.NewGuid.ToString()}
            End If

            Return ViewState("_Cliente")
        End Get
        Set(value As GetClientesDetalle.Cliente)
            ViewState("_Cliente") = value
        End Set
    End Property

    Private _TiposCliente As GetTiposClientes.TipoClienteColeccion
    Public ReadOnly Property TiposCliente As GetTiposClientes.TipoClienteColeccion
        Get
            If _TiposCliente Is Nothing Then

                Dim _Peticion As New GetTiposClientes.Peticion
                Dim _Respuesta As New GetTiposClientes.Respuesta
                Dim _Proxy As New Comunicacion.ProxyTipoCliente

                _Peticion.bolActivo = True
                _Peticion.ParametrosPaginacion.RealizarPaginacion = False
                _Respuesta = _Proxy.getTiposClientes(_Peticion)

                If Not Master.ControleErro.VerificaErro(_Respuesta.CodigoError, _Respuesta.NombreServidorBD, _Respuesta.MensajeError) Then
                    Throw New Exception("Error al obtener Tipos de Cliente")
                End If

                _TiposCliente = _Respuesta.TipoCliente
            End If
            Return _TiposCliente
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
                Cliente.Direcciones = DirectCast(Session("DireccionPeticion"), ContractoServicio.Direccion.DireccionColeccionBase)
                Session.Remove("DireccionPeticion")
            End If
            Return Cliente.Direcciones
        End Get
        Set(value As ContractoServicio.Direccion.DireccionColeccionBase)
            Cliente.Direcciones = value
        End Set
    End Property

    Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            If Session("objRespuestaGEPR_TCLIENTE") IsNot Nothing Then

                Cliente.CodigosAjenos = Session("objRespuestaGEPR_TCLIENTE")
                Session.Remove("objRespuestaGEPR_TCLIENTE")

                Dim iCodigoAjeno = (From item In Cliente.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If Cliente.CodigosAjenos Is Nothing Then
                    Cliente.CodigosAjenos = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If

                Session("objPeticionGEPR_TCLIENTE") = Cliente.CodigosAjenos

            End If

            Return Cliente.CodigosAjenos
        End Get
        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            Cliente.CodigosAjenos = value
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

                CargarTipoCliente()

                If Accion <> Aplicacao.Util.Utilidad.eAcao.Alta Then
                    CargarDatos()
                End If
            End If

            ucTotSaldo.IdentificadorCliente = Cliente.OidCliente
            ucDatosBanc.Cliente.Identificador = Cliente.OidCliente

            ConfigurarControles()

            AddHandler ucTotSaldo.DadosCarregados, AddressOf ucTotSaldo_DadosCarregados

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    Protected Overrides Sub DefinirParametrosBase()
        Try
            MyBase.Acao = Accion
            MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.CLIENTES
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.TituloPagina = Traduzir("037_titulo_mantenimiento")
        lblTituloCliente.Text = Traduzir("037_titulo_mantenimiento")
        lblCodCliente.Text = Traduzir("037_lbl_CodCliente")
        lblCodigoAjeno.Text = Traduzir("037_lbl_CodigoAjeno")
        lblDescCliente.Text = Traduzir("037_lbl_DescCliente")
        lblDesCodigoAjeno.Text = Traduzir("037_lbl_DesCodigoAjeno")
        lblTipoCliente.Text = Traduzir("037_lbl_TipoCliente")
        lblTituloCliente.Text = Traduzir("037_lbl_TituloCliente")
        lblTotSaldo.Text = Traduzir("037_lbl_TotSaldo")
        lblVigente.Text = Traduzir("037_lbl_Vigente")
        lblTituloTotSaldo.Text = Traduzir("037_lbl_TituloTotSaldo")
        lblProprioTotSaldo.Text = Traduzir("037_lbl_ProprioTotSaldo")
        lblTituloDatosBanc.Text = Traduzir("037_lbl_TituloDatosBanc")
        lblAbonaPorSaldoTotal.Text = Traduzir("037_lbl_AbonaPorSaldoTotal")
        csvCodClienteExistente.ErrorMessage = Traduzir("037_msg_csvCodClienteExistente")
        csvCodClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvCodClienteObrigatorio")
        csvDescClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvDescClienteObrigatorio")
        csvDescClienteExistente.ErrorMessage = Traduzir("037_msg_csvDescClienteExistente")
        csvTipoClienteObrigatorio.ErrorMessage = Traduzir("037_msg_csvTipoClienteObrigatorio")
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub ConfigurarControles()

        Select Case Accion

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadirTotalizador.Visible = True
                btnAnadirCuenta.Visible = True
                chkVigente.Checked = True
                chkVigente.Enabled = False
                Me.ucTotSaldo.Habilitar()
                If Not IsPostBack Then
                    txtCodigoCliente.Focus()
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                txtCodigoCliente.Enabled = False
                txtCodigoCliente.ReadOnly = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnAnadirTotalizador.Visible = True
                btnAnadirCuenta.Visible = True
                Me.ucTotSaldo.Habilitar()
                If Not IsPostBack Then
                    txtDescCliente.Focus()
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnAnadirTotalizador.Visible = False
                btnAnadirCuenta.Visible = False
                txtCodigoCliente.Enabled = False
                txtDescCliente.Enabled = False
                ddlTipoCliente.Enabled = False
                chkVigente.Enabled = False
                chkTotSaldo.Enabled = False
                Me.ucTotSaldo.Desabilitar()
                Me.ucDatosBanc.Desabilitar()

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnAnadirTotalizador.Visible = False
                btnAnadirCuenta.Visible = False

        End Select

        txtCodigoAjeno.Enabled = False
        txtDesCodigoAjeno.Enabled = False

        Dim msg As String = MontaMensagensErro(False, False)
        If msg <> String.Empty Then
            Master.ControleErro.ShowError(msg, False)
        End If

        'Si el país está configurado para permitir mantenimiento de clientes solamente a través del modo integración –
        ' parámetro MantenimientoClientesDivisasPorPantalla = Falso, la pantalla permitirá solamente:
        '-	Consulta y 
        '-	Modificación solamente de los códigos ajenos, totalizadores de saldo y el tipo del Cliente.

        If Accion = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Accion = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

            Dim ParametroMantenimientoClientesDivisasPorPantalla As Boolean
            ParametroMantenimientoClientesDivisasPorPantalla = Parametros.Genesis.Parametros.ParametrosIntegracion.Where(Function(O) O.CodigoParametro = "MantenimientoClientesDivisasPorPantalla").First.ValorParametro

            If ParametroMantenimientoClientesDivisasPorPantalla Then
                txtCodigoCliente.Enabled = True
                txtDescCliente.Enabled = True
                chkTotSaldo.Enabled = True
                chkVigente.Enabled = True
            Else
                txtCodigoCliente.Enabled = False
                txtDescCliente.Enabled = False
                ddlTipoCliente.Enabled = True
                chkVigente.Enabled = False
                chkTotSaldo.Enabled = True
                txtCodigoAjeno.Enabled = True
                txtDesCodigoAjeno.Enabled = True
                Me.ucTotSaldo.Desabilitar()
                Me.ucDatosBanc.Desabilitar()
            End If

            If chkTotSaldo.Checked Then
                chkProprioTotSaldo.Enabled = True
            Else
                chkProprioTotSaldo.Enabled = False
            End If

        End If

    End Sub

    Private Sub CargarTipoCliente()

        ddlTipoCliente.AppendDataBoundItems = True
        ddlTipoCliente.Items.Clear()

        For Each tipo In TiposCliente
            ddlTipoCliente.Items.Add(New ListItem(tipo.codTipoCliente + " - " + tipo.desTipoCliente, tipo.oidTipoCliente))
        Next

        ddlTipoCliente.OrdenarPorDesc()
        ddlTipoCliente.Items.Insert(0, New ListItem(Traduzir("036_ddl_selecione"), String.Empty))

    End Sub

    Private Sub CargarDatos()

        Dim itemSelecionado As ListItem

        If Cliente IsNot Nothing Then

            Dim iCodigoAjeno = (From item In Cliente.CodigosAjenos
                   Where item.BolDefecto = True).FirstOrDefault()

            ' CODIGOS
            txtCodigoCliente.Text = Cliente.CodCliente
            txtDescCliente.Text = Cliente.DesCliente

            ' AJENO
            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtDesCodigoAjeno.Text = iCodigoAjeno.DesAjeno
            End If

            ' TOOLTIP
            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then
                txtCodigoCliente.ToolTip = Cliente.CodCliente
                txtDescCliente.ToolTip = Cliente.DesCliente
            End If

            ' CHECKBOX
            chkVigente.Checked = Cliente.BolVigente
            chkVigente.Enabled = Not Cliente.BolVigente

            chkTotSaldo.Checked = Cliente.BolTotalizadorSaldo

            chkAbonaPorSaldoTotal.Checked = Cliente.BolAbonaPorSaldoTotal

            ' TIPO
            itemSelecionado = ddlTipoCliente.Items.FindByValue(Cliente.OidTipoCliente)

            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
                ddlTipoCliente.ToolTip = itemSelecionado.ToString
            End If

        End If

    End Sub

    Private Sub ExecutarGrabar()
        Try

            Dim _ProxyCliente As New Comunicacion.ProxyCliente
            Dim _RespuestaCliente As SetClientes.Respuesta

            Dim _ClienteSet As New SetClientes.Cliente
            Dim _ClientesSet As New SetClientes.ClienteColeccion

            If MontaMensagensErro(True, True).Length > 0 Then
                Exit Sub
            End If

            _ClienteSet.CodCliente = txtCodigoCliente.Text
            _ClienteSet.DesCliente = txtDescCliente.Text
            _ClienteSet.BolTotalizadorSaldo = chkTotSaldo.Checked
            _ClienteSet.BolVigente = chkVigente.Checked
            _ClienteSet.oidTipoCliente = ddlTipoCliente.SelectedValue
            _ClienteSet.CodigoAjeno = CodigosAjenos
            _ClienteSet.ConfigNivelSaldo = ConverteNivelSaldo()
            _ClienteSet.BolClienteTotSaldo = chkTotSaldo.Checked
            _ClienteSet.Direcciones = Direcciones
            _ClienteSet.PeticionDatosBancarios = Me.ucDatosBanc.BuscarPeticion()
            _ClienteSet.BolAbonaPorTotalSaldo = chkAbonaPorSaldoTotal.Checked

            ' Inicio POG: Por falta de tempo, foi acrescentado este codigo para resolver um bug. Futuramente, talvez... este codigo será alterado.
            If Accion = Aplicacao.Util.Utilidad.eAcao.Alta Then
                If _ClienteSet.ConfigNivelSaldo IsNot Nothing AndAlso _ClienteSet.ConfigNivelSaldo.Count > 0 Then
                    For Each nivel In _ClienteSet.ConfigNivelSaldo.FindAll(Function(x) x.oidCliente = Cliente.OidCliente)
                        If nivel.configNivelSaldo IsNot Nothing AndAlso nivel.configNivelSaldo.oidCliente = Cliente.OidCliente Then
                            nivel.configNivelSaldo.oidCliente = Nothing
                        End If
                        nivel.oidCliente = Nothing
                    Next
                End If
                _ClienteSet.OidCliente = Nothing
            Else
                _ClienteSet.OidCliente = Cliente.OidCliente
            End If
            ' FIM POG 

            _ClientesSet.Add(_ClienteSet)
            _RespuestaCliente = _ProxyCliente.SetClientes(New SetClientes.Peticion With {.Clientes = _ClientesSet, .CodigoUsuario = MyBase.LoginUsuario})

            If Master.ControleErro.VerificaErro(_RespuestaCliente.CodigoError, _RespuestaCliente.NombreServidorBD, _RespuestaCliente.MensajeError) Then
                If Master.ControleErro.VerificaErro(_RespuestaCliente.Clientes(0).CodigoError, _RespuestaCliente.NombreServidorBD, _RespuestaCliente.Clientes(0).MensajeError) Then
                    If Master.ControleErro.VerificaErro(_RespuestaCliente.Clientes(0).CodigoError, _RespuestaCliente.Clientes(0).NombreServidorBD, _RespuestaCliente.Clientes(0).MensajeError) Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_grabado_suceso", "alert('" & Traduzir("001_msg_grabado_suceso") & "'); RedirecionaPaginaNormal('BusquedaClientes.aspx');", True)
                    Else
                        Master.ControleErro.ShowError(_RespuestaCliente.Clientes(0).MensajeError, False)
                    End If
                    Session.Remove("DireccionPeticion")
                End If
            Else
                If _RespuestaCliente.Clientes IsNot Nothing AndAlso _RespuestaCliente.Clientes.Count > 0 AndAlso _RespuestaCliente.Clientes(0).CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(_RespuestaCliente.Clientes(0).MensajeError, False)
                ElseIf _RespuestaCliente.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_AMBIENTE_DEFAULT Then
                    Master.ControleErro.ShowError(_RespuestaCliente.MensajeError, False)
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

                If ddlTipoCliente.SelectedValue.Trim.Equals(String.Empty) Then

                    strErro.Append(csvTipoClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTipoClienteObrigatorio.IsValid = True
                End If


                If txtCodigoCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvCodClienteObrigatorio.IsValid = True
                End If

                If txtDescCliente.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescClienteObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescClienteObrigatorio.IsValid = False

                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescCliente.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescClienteObrigatorio.IsValid = True
                End If

            End If

        End If

        If CodigoExistente Then

            strErro.Append(csvCodClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            csvCodClienteExistente.IsValid = False

            If SetarFocoControle AndAlso Not focoSetado Then
                txtCodigoCliente.Focus()
                focoSetado = True
            End If

        Else
            csvCodClienteExistente.IsValid = True
        End If

        If DescricaoExistente Then

            strErro.Append(csvDescClienteExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
            csvDescClienteExistente.IsValid = False

            If SetarFocoControle AndAlso Not focoSetado Then
                txtDescCliente.Focus()
                focoSetado = True
            End If

        Else
            csvDescClienteExistente.IsValid = True
        End If


        Return strErro.ToString

    End Function

    Private Function ExisteCodigoCliente(codigoCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtCodigoCliente.Text) Then

            Dim objResposta As VerificarCodigoCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New VerificarCodigoCliente.Peticion

                objPeticion.codCliente = codigoCliente.Trim
                objResposta = objProxyUtilidad.VerificarCodigoCliente(objPeticion)

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

    Private Function ExisteDescricaoCliente(descricaoCliente As String) As Boolean

        If Not String.IsNullOrEmpty(txtDescCliente.Text) Then

            Dim objResposta As VerificarDescripcionCliente.Respuesta

            Try

                Dim objProxyUtilidad As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New VerificarDescripcionCliente.Peticion

                objPeticion.DesCliente = descricaoCliente.Trim
                objResposta = objProxyUtilidad.VerificarDescripcionCliente(objPeticion)

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

    Private Sub AddRemoveTotalizadorSaldoProprio(remove As Boolean)

        If Me.ucTotSaldo.TotalizadoresSaldos Is Nothing Then Me.ucTotSaldo.TotalizadoresSaldos = New List(Of Comon.Clases.TotalizadorSaldo)

        If remove Then

            Dim _TotalizadorSaldo = Me.ucTotSaldo.TotalizadoresSaldos.Find(Function(a) a.Cliente IsNot Nothing _
                                                          AndAlso a.Cliente.Identificador = Cliente.OidCliente _
                                                          AndAlso a.SubCliente Is Nothing _
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

            If _TotalizadorSaldo IsNot Nothing AndAlso Aplicacao.Util.Utilidad.CompararTotalizadores(_TotalizadorSaldo.SubCanales, _
                                                                                                     _TotalizadorSaldo.Cliente.Identificador, _
                                                                                                     "", "", _
                                                                                                     _auxSubCanales, _
                                                                                                     Cliente.OidCliente, _
                                                                                                     "", "") Then

                Me.ucTotSaldo.TotalizadoresSaldos.Remove(_TotalizadorSaldo)
                If _TotalizadorSaldo.bolDefecto Then
                    If Me.ucTotSaldo.TotalizadoresSaldos IsNot Nothing AndAlso Me.ucTotSaldo.TotalizadoresSaldos.Count > 0 Then
                        For Each _totalizador In Me.ucTotSaldo.TotalizadoresSaldos
                            If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales, _
                                                                     "", _
                                                                     "", _
                                                                     "", _
                                                                     _auxSubCanales, _
                                                                     "", _
                                                                     "", _
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
                        Cliente.CodCliente = txtCodigoCliente.Text
                        Cliente.DesCliente = txtDescCliente.Text
                    End If

                    .Cliente = New Comon.Clases.Cliente With {.Identificador = Cliente.OidCliente, .Codigo = Cliente.CodCliente, .Descripcion = Cliente.DesCliente}
                    Me.ucTotSaldo.IdentificadorCliente = Cliente.OidCliente

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

    Private Function ConverteNivelSaldo() As ContractoServicio.Cliente.SetClientes.ConfigNivelMovColeccion

        Dim retorno As New ContractoServicio.Cliente.SetClientes.ConfigNivelMovColeccion

        For Each nivelSaldo In Me.ucTotSaldo.TotalizadoresSaldos

            Dim peticionNivelSaldo As New ContractoServicio.Cliente.SetClientes.ConfigNivelMov
            With peticionNivelSaldo
                .oidConfigNivelMovimiento = nivelSaldo.IdentificadorNivelMovimiento
                .bolDefecto = nivelSaldo.bolDefecto
                .bolActivo = True
                .oidCliente = Cliente.OidCliente
                .codCliente = Cliente.CodCliente
                .desCliente = Cliente.DesCliente

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
        Response.Redirect("~/BusquedaClientes.aspx", False)
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("~/BusquedaClientes.aspx", False)
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

        tablaGenesis.CodTablaGenesis = txtCodigoCliente.Text
        tablaGenesis.DesTablaGenesis = txtDescCliente.Text
        tablaGenesis.OidTablaGenesis = Cliente.OidCliente

        If Cliente IsNot Nothing AndAlso Cliente.CodigosAjenos IsNot Nothing Then
            tablaGenesis.CodigosAjenos = Cliente.CodigosAjenos
        End If

        Session("objPeticionGEPR_TCLIENTE") = tablaGenesis.CodigosAjenos
        Session("objGEPR_TCLIENTE") = tablaGenesis

        If (Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCLIENTE"
        Else
            url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 900,'btnAltaAjeno');", True)
    End Sub

    Protected Sub btnDireccion_Click(sender As Object, e As System.EventArgs) Handles btnDireccion.Click
        Dim url As String = String.Empty
        Dim tablaGenes As New MantenimientoDireccion.CodigoTablaGenesis

        tablaGenes.CodGenesis = txtCodigoCliente.Text
        tablaGenes.DesGenesis = txtDescCliente.Text
        tablaGenes.OidGenesis = Cliente.OidCliente
        If Direcciones Is Nothing AndAlso Not String.IsNullOrEmpty(Cliente.OidCliente) Then
            If Cliente.Direcciones IsNot Nothing AndAlso Cliente.Direcciones.Count > 0 AndAlso Cliente.Direcciones IsNot Nothing Then
                tablaGenes.Direcion = Cliente.Direcciones.FirstOrDefault
            End If
        ElseIf Direcciones IsNot Nothing Then
            tablaGenes.Direcion = Direcciones.FirstOrDefault
        End If

        Session("objGEPR_TCLIENTE") = tablaGenes

        If (Accion = Aplicacao.Util.Utilidad.eAcao.Consulta) Then
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TCLIENTE"
        Else
            url = "MantenimientoDireccion.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TCLIENTE"
        End If

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnDireccion');", True)
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

    Protected Sub chkVigente_CheckedChanged(sender As Object, e As EventArgs) Handles chkVigente.CheckedChanged
        If chkVigente.Checked Then
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_informacao_Aviso", "alert('" & Traduzir("037_lbl_msgAvisoCliente") & "');", True)
        End If
    End Sub

    Protected Sub txtCodigoCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigoCliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodigoCliente.Text) Then

                If ExisteCodigoCliente(txtCodigoCliente.Text) Then
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

    Protected Sub txtDescCliente_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescCliente.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDescCliente.Text) Then

                If ExisteDescricaoCliente(txtDescCliente.Text) Then
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
                                                            AndAlso a.SubCliente Is Nothing _
                                                            AndAlso a.PuntoServicio Is Nothing _
                                                            AndAlso a.SubCanales.Count > 1) Then
                chkProprioTotSaldo.Checked = True
            End If

        End If
    End Sub

#End Region

End Class