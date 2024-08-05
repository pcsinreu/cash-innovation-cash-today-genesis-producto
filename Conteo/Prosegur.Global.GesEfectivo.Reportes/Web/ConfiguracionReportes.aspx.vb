Imports Prosegur.Framework.Dicionario.Tradutor
Imports IacUtilidad = Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Utilidad
Imports IacIntegracion = Prosegur.Global.GesEfectivo.IAC.Integracion.ContractoServicio

Partial Public Class ConfiguracionReportes
    Inherits Base

    '#Region "[VARIÁVEIS]"

    '    Dim Valida As New List(Of String)
    '    Private CantidadCaracteresTextBox As Integer = 45

    '#End Region

    '#Region "[METODOS BASE]"

    '    ''' <summary>
    '    ''' Adiciona a validação aos controles
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Protected Overrides Sub AdicionarControlesValidacao()

    '    End Sub

    '    ''' <summary>
    '    ''' Adiciona javascript
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Protected Overrides Sub AdicionarScripts()

    '        Dim pbo As PostBackOptions
    '        Dim s As String = String.Empty

    '        pbo = New PostBackOptions(btnLimpiar)
    '        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
    '        btnLimpiar.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnGenerarInforme.ClientID & "," & btnGrabar.ClientID & "," & btnLimpiar.ClientID & "," & btnVolver.ClientID & "');closeCalendar();"

    '        pbo = New PostBackOptions(btnGrabar)
    '        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
    '        btnGrabar.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnGenerarInforme.ClientID & "," & btnGrabar.ClientID & "," & btnLimpiar.ClientID & "," & btnVolver.ClientID & "');closeCalendar();"

    '        pbo = New PostBackOptions(btnVolver)
    '        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
    '        btnVolver.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnGenerarInforme.ClientID & "," & btnGrabar.ClientID & "," & btnLimpiar.ClientID & "," & btnVolver.ClientID & "');closeCalendar();"

    '        pbo = New PostBackOptions(btnGenerarInforme)
    '        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
    '        btnGenerarInforme.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnGenerarInforme.ClientID & "," & btnGrabar.ClientID & "," & btnLimpiar.ClientID & "," & btnVolver.ClientID & "');closeCalendar();"

    '        'Adiciona a Precedencia ao Buscar
    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    '        ' Obtém a lista de idiomas
    '        Dim Idiomas As List(Of String) = ObterIdiomas()

    '        ' Recupera o idioma corrente
    '        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

    '        ' Define a mascara do período inicial digitado
    '        Me.txtFechaConteoHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##');")
    '        ' Define a mascara do período final digitado
    '        Me.txtFechaConteoDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##');")

    '        Me.txtFechaTransporteDesde.Attributes.Add("onkeypress", "return mascaraData(this);")
    '        Me.txtFechaTransporteHasta.Attributes.Add("onkeypress", "return mascaraData(this);")


    '        ' Define a mascara da data inicial escolhido no calendario
    '        Me.imbFechaConteoDesde.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaConteoDesde.ClientID & ",document.forms[0]." & Me.txtFechaConteoHasta.ClientID & ",'dd/mm/yyyy hh:ii',this,true,'" & IdiomaCorrente & "'); return false;")
    '        ' Define a mascara da data final escolhido no calendario
    '        Me.imbFechaConteoHasta.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaConteoHasta.ClientID & ",document.forms[0]." & Me.txtFechaConteoDesde.ClientID & ",'dd/mm/yyyy hh:ii',this,true,'" & IdiomaCorrente & "', true); return false;")

    '        ' Define a mascara da data inicial escolhido no calendario
    '        Me.imbFechaTransporteDesde.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaTransporteDesde.ClientID & ",document.forms[0]." & Me.txtFechaTransporteHasta.ClientID & ",'dd/mm/yyyy',this,true,'" & IdiomaCorrente & "'); return false;")
    '        ' Define a mascara da data final escolhido no calendario
    '        Me.imbFechaTransporteHasta.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaTransporteHasta.ClientID & ",document.forms[0]." & Me.txtCliente.ClientID & ",'dd/mm/yyyy',this,true,'" & IdiomaCorrente & "',true); return false;")

    '    End Sub

    '    ''' <summary>
    '    ''' Configura estado da página.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' [magnum.oliveira] 27/10/2009 Alterado
    '    ''' </history>
    '    Protected Overrides Sub ConfigurarEstadoPagina()

    '        Select Case MyBase.Acao

    '            Case Enumeradores.eAcao.Alta, Enumeradores.eAcao.Modificacion

    '                If Not IsPostBack Then

    '                    If chkAgrupadocliente.Checked Then
    '                        ConfigurarEstadoControles(chkAgrupadocliente)
    '                    ElseIf chkAgrupadoSubCliente.Checked Then
    '                        ConfigurarEstadoControles(chkAgrupadoSubCliente)
    '                    ElseIf chkAgrupadoPuntoServicio.Checked Then
    '                        ConfigurarEstadoControles(chkAgrupadoPuntoServicio)
    '                    ElseIf chkAgrupadoGrupoCliente.Checked Then
    '                        ConfigurarEstadoControles(chkAgrupadoGrupoCliente)
    '                    End If

    '                    ConfigurarEstadoControles(lstGrupoCliente)

    '                    ConfigurarEstadoLinha(False)

    '                End If


    '                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
    '                    lstGrupoCliente.Enabled = False
    '                Else
    '                    lstGrupoCliente.Enabled = True
    '                End If

    '        End Select

    '    End Sub

    '    ''' <summary>
    '    ''' Seta tabindex
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 02/05/2013 - Criado
    '    ''' </history>
    '    Protected Overrides Sub ConfigurarTabIndex()

    '        txtCodConfiguracion.TabIndex = 1
    '        txtDesConfiguracion.TabIndex = 2
    '        ddlReportes.TabIndex = 3
    '        ddlDelegacion.TabIndex = 4
    '        txtFechaConteoDesde.TabIndex = 5
    '        txtFechaConteoHasta.TabIndex = 6
    '        txtFechaTransporteDesde.TabIndex = 7
    '        txtFechaTransporteHasta.TabIndex = 8
    '        btnCliente.TabIndex = 9
    '        btnSubCliente.TabIndex = 10
    '        btnPuntoServicio.TabIndex = 11
    '        lstGrupoCliente.TabIndex = 12
    '        lstCanal.TabIndex = 13
    '        lstSubCanal.TabIndex = 14
    '        lstDivisa.TabIndex = 15
    '        lstPuesto.TabIndex = 16
    '        lstContador.TabIndex = 17
    '        btnRuta.TabIndex = 18
    '        chkAgrupadocliente.TabIndex = 19
    '        chkAgrupadoSubCliente.TabIndex = 20
    '        chkAgrupadoPuntoServicio.TabIndex = 21
    '        chkAgrupadoGrupoCliente.TabIndex = 22
    '        rblFormatoSalida.TabIndex = 23
    '        btnGenerarInforme.TabIndex = 24
    '        btnLimpiar.TabIndex = 25
    '        btnGrabar.TabIndex = 26
    '        btnVolver.TabIndex = 27

    '        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
    '        Master.PrimeiroControleTelaID = Me.txtCodConfiguracion.ClientID

    '    End Sub

    '    ''' <summary>
    '    ''' Define os parametros iniciais.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Protected Overrides Sub DefinirParametrosBase()

    '        ' define ação da tela
    '        If Request.QueryString("acao") Is Nothing Then
    '            MyBase.Acao = Enumeradores.eAcao.NoAction
    '        Else
    '            MyBase.Acao = Request.QueryString("acao")
    '        End If

    '        ' definir o nome da página
    '        MyBase.PaginaAtual = Enumeradores.eTelas.REPORTES
    '        ' desativar validação de ação
    '        MyBase.ValidarAcao = True
    '        ' desativar validação de permissões do AD
    '        MyBase.ValidarPemissaoAD = True

    '    End Sub

    '    ''' <summary>
    '    ''' Primeiro metodo chamado quando inicia a pagina
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Protected Overrides Sub Inicializar()

    '        Try

    '            ' Adiciona scripts aos controles
    '            AdicionarScripts()

    '            If Not Page.IsPostBack Then

    '                ' Carrega os dados iniciais dos controles
    '                CarregarControles()

    '                If Acao = Enumeradores.eAcao.Modificacion AndAlso Not String.IsNullOrEmpty(Session("IdentificadorConfiguracion")) Then

    '                    IdentificadorConfiguracion = Session("IdentificadorConfiguracion")
    '                    Session.Remove("IdentificadorConfiguracion")

    '                    RecuperarConfiguracion()

    '                    'Preenche os objetos em memoria decliente, subcliente e ponto de serviço.
    '                    PreencherDadosCliente()

    '                    PopularControles()

    '                    txtDesConfiguracion.Focus()

    '                End If

    '            Else

    '                ' consome a sessão do cliente selecionado na tela de busca
    '                ConsomeClientes()
    '                ConsomeSubClientes()
    '                ConsomePtoServicio()

    '            End If

    '            ' trata o foco dos campos
    '            TrataFoco()


    '        Catch ex As Exception
    '            Master.ControleErro.ShowError(ex.Message)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Pre render
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Protected Overrides Sub PreRenderizar()

    '        Try
    '            Me.ConfigurarEstadoPagina()
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Traduz os controles
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' [magnum.oliveira] 27/10/2009 Alterado
    '    ''' </history>
    '    Protected Overrides Sub TraduzirControles()

    '        Me.Page.Title = Traduzir("024_titulo_pagina")
    '        lblTituloConfiguracionReportes.Text = Traduzir("024_titulo_pagina")
    '        lblCodConfiguracion.Text = Traduzir("024_lblCodigoConfiguracion")
    '        lblDesConfiguracion.Text = Traduzir("024_lblDescricaoConfiguracao")
    '        lblReportes.Text = Traduzir("024_lblReporte")
    '        lblDelegacion.Text = Traduzir("024_lblDelegacion")
    '        lblFechaConteo.Text = Traduzir("024_lblFechaRecaudacionIni")
    '        lblFechaConteoDesde.Text = Traduzir("lbl_desde")
    '        lblFechaConteoHasta.Text = Traduzir("lbl_hasta")
    '        lblFechaTransporte.Text = Traduzir("024lblFechaTransporteIni")
    '        lblFechaTransporteDesde.Text = Traduzir("lbl_desde")
    '        lblFechaTransporteHasta.Text = Traduzir("lbl_hasta")
    '        lblCliente.Text = Traduzir("024_lblCliente")
    '        lblSubCliente.Text = Traduzir("024_lblSubCliente")
    '        lblPuntoServicio.Text = Traduzir("024_lblPuntoServicio")
    '        lblGrupCliente.Text = Traduzir("024_lblGrupoClientes")
    '        lblCanal.Text = Traduzir("024_lblCanal")
    '        lblSubCanal.Text = Traduzir("024_lblSubCanal")
    '        lblDivisa.Text = Traduzir("024_lblDivisa")
    '        lblContador.Text = Traduzir("024_lblContador")
    '        lblPuesto.Text = Traduzir("024_lblPuesto")
    '        lblRuta.Text = Traduzir("024_lblRuta")
    '        lblFormatoSalida.Text = Traduzir("024_lblFormatoArchivo")

    '        chkAgrupadocliente.Text = Traduzir("024_chkAgruparClientes")
    '        chkAgrupadoGrupoCliente.Text = Traduzir("024_chkAgruparGrupoClientes")
    '        chkAgrupadoPuntoServicio.Text = Traduzir("024_chkAgruparPuntosServicios")
    '        chkAgrupadoSubCliente.Text = Traduzir("024_chkAgruparSubClientes")

    '    End Sub

    '#End Region

    '#Region "[METODOS]"

    '    ''' <summary>
    '    ''' Preenche os Dados dos clientes, subclientes e pontos de serviço
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub PreencherDadosCliente()

    '        If objConfiguracion IsNot Nothing AndAlso objConfiguracion.ParametrosReporte IsNot Nothing Then

    '            Dim objClientes = From objParam In objConfiguracion.ParametrosReporte Where objParam.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE

    '            If objClientes.Count > 0 Then

    '                If Clientes Is Nothing Then Clientes = New IacUtilidad.GetComboClientes.ClienteColeccion

    '                For Each objCli In objClientes

    '                    Clientes.Add(New IacUtilidad.GetComboClientes.Cliente With { _
    '                                 .Codigo = objCli.DesValorParametro, _
    '                                 .Descripcion = objCli.DesParametro})

    '                Next

    '            End If

    '            Dim objClientesSubClientes = From objParam In objConfiguracion.ParametrosReporte Where objParam.CodParametro = Constantes.CONST_P_CODIGO_SUBCLIENTE

    '            If objClientesSubClientes.Count > 0 Then

    '                If SubClientesCompleto Is Nothing Then SubClientesCompleto = New IacUtilidad.GetComboSubclientesByCliente.ClienteColeccion
    '                Dim objSubCliente As IacUtilidad.GetComboSubclientesByCliente.SubCliente = Nothing
    '                Dim objClienteSubCliente As IacUtilidad.GetComboSubclientesByCliente.Cliente = Nothing

    '                For Each objSCli In objClientesSubClientes

    '                    Dim objCodigosClientes() As String = objSCli.DesValorParametro.Split("|")

    '                    If objCodigosClientes.Count = 2 Then

    '                        objSubCliente = New IacUtilidad.GetComboSubclientesByCliente.SubCliente

    '                        objSubCliente.Codigo = objCodigosClientes(1)
    '                        objSubCliente.Descripcion = objSCli.DesParametro

    '                        objClienteSubCliente = (From c In SubClientesCompleto Where c.Codigo = objCodigosClientes.First).FirstOrDefault

    '                        If objClienteSubCliente Is Nothing Then

    '                            SubClientesCompleto.Add(New IacUtilidad.GetComboSubclientesByCliente.Cliente With { _
    '                                                    .Codigo = objCodigosClientes.First, _
    '                                                    .SubClientes = New IacUtilidad.GetComboSubclientesByCliente.SubClienteColeccion})

    '                            objClienteSubCliente = (From c In SubClientesCompleto Where c.Codigo = objCodigosClientes.First).FirstOrDefault

    '                        End If

    '                        objClienteSubCliente.SubClientes.Add(objSubCliente)
    '                    End If

    '                Next

    '            End If

    '            Dim objClientesSubClientesPtoServ = From objParam In objConfiguracion.ParametrosReporte Where objParam.CodParametro = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO

    '            If objClientesSubClientesPtoServ.Count > 0 Then

    '                If PuntosServiciosCompleto Is Nothing Then PuntosServiciosCompleto = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
    '                Dim objSubCliente As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = Nothing
    '                Dim objClienteSubCliente As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing
    '                Dim objPtoServ As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio = Nothing

    '                For Each objPtoSCli In objClientesSubClientesPtoServ

    '                    Dim objCodigosClientes() As String = objPtoSCli.DesValorParametro.Split("|")

    '                    If objCodigosClientes.Count = 3 Then

    '                        objPtoServ = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio

    '                        objPtoServ.Codigo = objCodigosClientes(2)
    '                        objPtoServ.Descripcion = objPtoSCli.DesParametro

    '                        objClienteSubCliente = (From c In PuntosServiciosCompleto Where c.Codigo = objCodigosClientes.First).FirstOrDefault

    '                        If objClienteSubCliente Is Nothing Then

    '                            PuntosServiciosCompleto.Add(New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente With { _
    '                                                        .Codigo = objCodigosClientes.First, _
    '                                                        .SubClientes = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion})

    '                            objClienteSubCliente = (From c In PuntosServiciosCompleto Where c.Codigo = objCodigosClientes.First).FirstOrDefault

    '                        End If

    '                        objSubCliente = (From c In objClienteSubCliente.SubClientes Where c.Codigo = objCodigosClientes(1)).FirstOrDefault

    '                        If objSubCliente Is Nothing Then

    '                            objClienteSubCliente.SubClientes.Add(New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With { _
    '                                                                .Codigo = objCodigosClientes(1), _
    '                                                                .PuntosServicio = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicioColeccion})

    '                            objSubCliente = (From c In objClienteSubCliente.SubClientes Where c.Codigo = objCodigosClientes(1)).FirstOrDefault

    '                        End If

    '                        objSubCliente.PuntosServicio.Add(objPtoServ)

    '                    End If

    '                Next

    '            End If

    '        End If

    '    End Sub

    '    Private Sub ConfigurarEstadoLinha(BolLimparControles As Boolean)

    '        ObtenerParametros()

    '        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte.Count > 0 Then

    '            lnDelegacion.Visible = False
    '            lnFechaConteo.Visible = False
    '            CollblFechaConteo.Visible = False
    '            CollblFechaConteo.Visible = False
    '            CollblFechaConteoHasta.Visible = False
    '            coltxtFechaConteoDesde.Visible = False
    '            ColtxtFechaConteoHasta.Visible = False
    '            lnFechaTransporte.Visible = False
    '            CollblFechaTransporte.Visible = False
    '            CollblFechaTransporteHasta.Visible = False
    '            ColtxtFechaTransporteDesde.Visible = False
    '            ColtxtFechaTransporteHasta.Visible = False
    '            CollblFechaTransporte.Visible = False
    '            lnCliente.Visible = False
    '            lnSubCliente.Visible = False
    '            lnPuntoServicio.Visible = False
    '            lnGrupoCliente.Visible = False
    '            lnCanal.Visible = False
    '            CollblCanal.Visible = False
    '            CollstCanal.Visible = False
    '            CollblSubCanal.Visible = False
    '            CollstSubCanal.Visible = False
    '            lnDivisaPuesto.Visible = False
    '            CollblDivisa.Visible = False
    '            CollstDivisa.Visible = False
    '            CollblPuesto.Visible = False
    '            CollstPuesto.Visible = False
    '            lnContador.Visible = False

    '            'Limpa os controles
    '            If BolLimparControles Then LimparControles(True)

    '            For Each parametro In ParametrosReporte

    '                Select Case parametro.Name

    '                    Case Constantes.CONST_P_CODIGO_CLIENTE
    '                        lnCliente.Visible = True
    '                    Case Constantes.CONST_P_FECHA_CONTEO_DESDE

    '                        If Not lnFechaConteo.Visible Then
    '                            lnFechaConteo.Visible = True
    '                        End If

    '                        CollblFechaConteo.Visible = True
    '                        collblFechaConteoDesde.Visible = True
    '                        coltxtFechaConteoDesde.Visible = True

    '                    Case Constantes.CONST_P_FECHA_CONTEO_HASTA

    '                        If Not lnFechaConteo.Visible Then
    '                            lnFechaConteo.Visible = True
    '                        End If

    '                        CollblFechaConteo.Visible = True
    '                        CollblFechaConteoHasta.Visible = True
    '                        ColtxtFechaConteoHasta.Visible = True
    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_DESDE

    '                        If Not lnFechaTransporte.Visible Then
    '                            lnFechaTransporte.Visible = True
    '                        End If

    '                        CollblFechaTransporte.Visible = True
    '                        collblFechaTransporteDesde.Visible = True
    '                        ColtxtFechaTransporteDesde.Visible = True

    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_HASTA

    '                        If Not lnFechaTransporte.Visible Then
    '                            lnFechaTransporte.Visible = True
    '                        End If

    '                        CollblFechaTransporte.Visible = True
    '                        CollblFechaTransporteHasta.Visible = True
    '                        ColtxtFechaTransporteHasta.Visible = True

    '                    Case Constantes.CONST_P_CODIGO_SUBCLIENTE
    '                        lnSubCliente.Visible = True
    '                    Case Constantes.CONST_P_CODIGO_PUNTO_SERVICIO
    '                        lnPuntoServicio.Visible = True
    '                    Case Constantes.CONST_P_CODIGO_GRUPO_CLIENTES
    '                        lnGrupoCliente.Visible = True
    '                    Case Constantes.CONST_P_CODIGO_CANAL

    '                        If Not lnCanal.Visible Then
    '                            lnCanal.Visible = True
    '                            CollblPuesto.Align = HorizontalAlign.Right
    '                        End If

    '                        CollblCanal.Visible = True
    '                        CollstCanal.Visible = True

    '                    Case Constantes.CONST_P_CODIGO_SUBCANAL

    '                        If Not lnCanal.Visible Then
    '                            lnCanal.Visible = True
    '                            CollblPuesto.Align = HorizontalAlign.Left
    '                        End If

    '                        CollblSubCanal.Visible = True
    '                        CollstSubCanal.Visible = True

    '                    Case Constantes.CONST_P_CODIGO_DIVISA

    '                        If Not lnDivisaPuesto.Visible Then
    '                            lnDivisaPuesto.Visible = True
    '                            CollblPuesto.Align = HorizontalAlign.Right
    '                        End If

    '                        lnDivisaPuesto.Visible = True
    '                        CollblDivisa.Visible = True
    '                        CollstDivisa.Visible = True
    '                    Case Constantes.CONST_P_CODIGO_PUESTO

    '                        If Not lnDivisaPuesto.Visible Then
    '                            lnDivisaPuesto.Visible = True
    '                            CollblPuesto.Align = HorizontalAlign.Left
    '                        End If

    '                        CollblPuesto.Visible = True
    '                        CollstPuesto.Visible = True

    '                    Case Constantes.CONST_P_CODIGO_CONTADOR
    '                        lnContador.Visible = True
    '                    Case Constantes.CONST_P_CODIGO_DELEGACION
    '                        lnDelegacion.Visible = True
    '                End Select

    '            Next

    '            Dim CorLnImpar As String = "#f7f7f7"
    '            Dim CorLnPar As String = "#ffffff"
    '            Dim indice As Integer = 0
    '            Dim corLinhaAnterior As String = CorLnPar


    '            For Each linha As HtmlTableRow In tabelaCampos.Rows

    '                If linha.Visible Then

    '                    If corLinhaAnterior = CorLnImpar Then
    '                        linha.BgColor = CorLnPar
    '                    Else
    '                        linha.BgColor = CorLnImpar
    '                    End If

    '                    corLinhaAnterior = linha.BgColor

    '                End If

    '                indice += 1

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Popula a tela
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub PopularControles()

    '        If objConfiguracion IsNot Nothing Then

    '            With objConfiguracion

    '                txtCodConfiguracion.Text = .CodConfiguracion
    '                txtDesConfiguracion.Text = .DesConfiguracion
    '                ddlReportes.SelectedValue = .DesReporte
    '                txtRuta.Text = .DesRuta
    '                'chkAgrupadocliente.Checked = .BolAgruparCliente
    '                'chkAgrupadoGrupoCliente.Checked = .BolAgruparGrupoCliente
    '                'chkAgrupadoPuntoServicio.Checked = .BolAgruparPuntoServicio
    '                'chkAgrupadoSubCliente.Checked = .BolAgruparSubCliente
    '                rblFormatoSalida.SelectedValue = .NecFormatoArchivo

    '                PopularParametros(.ParametrosReporte)

    '            End With

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Popula os parametros
    '    ''' </summary>
    '    ''' <param name="Parametros"></param>
    '    ''' <remarks></remarks>
    '    Private Sub PopularParametros(Parametros As IacIntegracion.Reportes.ParametroReporteColeccion)

    '        If Parametros IsNot Nothing AndAlso Parametros.Count > 0 Then

    '            For Each parametro In Parametros

    '                Select Case parametro.CodParametro

    '                    Case Constantes.CONST_P_CODIGO_CLIENTE
    '                        PreencherControle(txtCliente, parametro.DesValorParametro, GetType(TextBox), parametro.DesParametro)
    '                    Case Constantes.CONST_P_FECHA_CONTEO_DESDE
    '                        PreencherControle(txtFechaConteoDesde, parametro.DesValorParametro, GetType(TextBox))
    '                    Case Constantes.CONST_P_FECHA_CONTEO_HASTA
    '                        PreencherControle(txtFechaConteoHasta, parametro.DesValorParametro, GetType(TextBox))
    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_DESDE
    '                        PreencherControle(txtFechaTransporteDesde, parametro.DesValorParametro, GetType(TextBox))
    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_HASTA
    '                        PreencherControle(txtFechaTransporteHasta, parametro.DesValorParametro, GetType(TextBox))
    '                    Case Constantes.CONST_P_CODIGO_SUBCLIENTE
    '                        PreencherControle(txtSubCliente, parametro.DesValorParametro, GetType(TextBox), parametro.DesParametro)
    '                    Case Constantes.CONST_P_CODIGO_PUNTO_SERVICIO
    '                        PreencherControle(txtPuntoServicio, parametro.DesValorParametro, GetType(TextBox), parametro.DesParametro)
    '                    Case Constantes.CONST_P_CODIGO_GRUPO_CLIENTES
    '                        PreencherControle(lstGrupoCliente, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_CANAL
    '                        PreencherControle(lstCanal, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_SUBCANAL
    '                        PreencherControle(lstSubCanal, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_DIVISA
    '                        PreencherControle(lstDivisa, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_PUESTO
    '                        PreencherControle(lstPuesto, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_CONTADOR
    '                        PreencherControle(lstContador, parametro.DesValorParametro, GetType(ListBox))
    '                    Case Constantes.CONST_P_CODIGO_DELEGACION
    '                        PreencherControle(ddlDelegacion, parametro.DesValorParametro, GetType(DropDownList))
    '                End Select

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o controle
    '    ''' </summary>
    '    ''' <param name="Control"></param>
    '    ''' <param name="DesParametro"></param>
    '    ''' <remarks></remarks>
    '    Private Sub PreencherControle(ByRef Control As Control, DesValorParametro As String, tipo As Type, Optional DesParametro As String = "", _
    '                                  Optional EsControleclienteSubClientePServicio As Boolean = False)

    '        If Not String.IsNullOrEmpty(DesValorParametro) AndAlso tipo IsNot Nothing Then

    '            If tipo Is GetType(TextBox) Then


    '                If CType(Control, TextBox).TextMode = TextBoxMode.MultiLine Then

    '                    Dim DescripcionCliente As String = Replace(DesValorParametro, "|", " - ") & " - " & DesParametro

    '                    If DescripcionCliente.Length > CantidadCaracteresTextBox Then
    '                        CType(Control, TextBox).Text &= DescripcionCliente.Substring(0, CantidadCaracteresTextBox) & "..." & vbCrLf
    '                    Else
    '                        CType(Control, TextBox).Text &= DescripcionCliente & vbCrLf
    '                    End If

    '                Else
    '                    CType(Control, TextBox).Text = DesValorParametro
    '                End If

    '            ElseIf tipo Is GetType(ListBox) Then
    '                Dim objlst As ListBox = CType(Control, ListBox)

    '                If objlst.Items IsNot Nothing AndAlso objlst.Items.Count > 0 AndAlso Not EsControleclienteSubClientePServicio Then

    '                    Dim item As ListItem = (From i As ListItem In objlst.Items Where i.Value = DesValorParametro).FirstOrDefault()

    '                    If (item IsNot Nothing) Then
    '                        item.Selected = True
    '                    End If

    '                Else

    '                    If EsControleclienteSubClientePServicio Then
    '                        objlst.Items.Add(New ListItem(Replace(DesValorParametro, "|", " - ") & " - " & DesParametro, DesValorParametro))
    '                    Else
    '                        objlst.Items.Add(New ListItem(DesValorParametro & " - " & DesParametro, DesValorParametro))
    '                    End If

    '                End If

    '            ElseIf tipo Is GetType(DropDownList) Then
    '                CType(Control, DropDownList).SelectedValue = DesValorParametro
    '            End If

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Recupera a configuração
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 06/05/2013 Criado
    '    ''' </history>
    '    Private Sub RecuperarConfiguracion()

    '        Dim objPeticion As New IacIntegracion.Reportes.GetConfiguracionesReportesDetail.Peticion

    '        objPeticion.IdentificadoresConfiguracion = New List(Of String)
    '        objPeticion.IdentificadoresConfiguracion.Add(IdentificadorConfiguracion)

    '        Dim objProxy As New Comunicacion.ProxyIacIntegracion
    '        Dim objRespuesta As IacIntegracion.Reportes.GetConfiguracionesReportesDetail.Respuesta = objProxy.GetConfiguracionesReportesDetail(objPeticion)

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        objConfiguracion = objRespuesta.ConfiguracionesReportes.First

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche os listbox delegación
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Private Sub PreencherListBoxDelegacion()

    '        Dim objProxy As New Comunicacion.ProxyUtilidad
    '        Dim objRespuesta As IacUtilidad.GetComboDelegaciones.Respuesta = objProxy.GetComboDelegaciones()

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        ddlDelegacion.AppendDataBoundItems = True
    '        ddlDelegacion.Items.Clear()

    '        If objRespuesta.Delegaciones IsNot Nothing AndAlso objRespuesta.Delegaciones.Count > 0 Then

    '            Delegaciones = objRespuesta.Delegaciones

    '            'ordena a lista de delegações
    '            objRespuesta.Delegaciones.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

    '            For Each objDelegacion In objRespuesta.Delegaciones
    '                ddlDelegacion.Items.Add(New ListItem(objDelegacion.Codigo & " - " & objDelegacion.Descripcion, objDelegacion.Codigo))
    '            Next

    '            ddlDelegacion.SelectedValue = InformacionUsuario.DelegacionLogin.Codigo

    '        End If


    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox Canal
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Public Sub PreencherListBoxCanal()

    '        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
    '        Dim objRespuesta As New IacUtilidad.GetComboCanales.Respuesta

    '        objRespuesta = objProxyUtilida.GetComboCanales()

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        lstCanal.AppendDataBoundItems = True
    '        lstCanal.Items.Clear()

    '        If objRespuesta.Canales IsNot Nothing Then

    '            Canales = objRespuesta.Canales

    '            'ordena a lista de canales
    '            objRespuesta.Canales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

    '            For Each objCanal As IacUtilidad.GetComboCanales.Canal In objRespuesta.Canales
    '                lstCanal.Items.Add(New ListItem(objCanal.Codigo & " - " & objCanal.Descripcion, objCanal.Codigo))
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox de grupo de clientes
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public Sub PreencherListBoxGrupoClientes()

    '        Dim objProxyGrupoClientes As New Comunicacion.ProxyIacGrupoClientes
    '        Dim objRespuesta As New IAC.ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta

    '        objRespuesta = objProxyGrupoClientes.GetGruposCliente(New IAC.ContractoServicio.GrupoCliente.GetGruposCliente.Peticion With { _
    '                                                              .GrupoCliente = New IAC.ContractoServicio.GrupoCliente.GrupoCliente With { _
    '                                                              .Vigente = True}})

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        lstGrupoCliente.AppendDataBoundItems = True
    '        lstGrupoCliente.Items.Clear()

    '        'Preenche a propriedade de grupo de clientes.
    '        Gruposclientes = objRespuesta.GruposCliente

    '        If Gruposclientes IsNot Nothing AndAlso Gruposclientes.Count > 0 Then
    '            'ordena a lista de canales
    '            Gruposclientes.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

    '            For Each objGrupoClientes In Gruposclientes
    '                lstGrupoCliente.Items.Add(New ListItem(objGrupoClientes.Codigo & " - " & objGrupoClientes.Descripcion, objGrupoClientes.Codigo))
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox Canal
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Public Sub PreencherListBoxSubCanal()

    '        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
    '        Dim objRespuesta As New IacUtilidad.GetComboSubcanalesByCanal.Respuesta
    '        Dim objPeticion As New IacUtilidad.GetComboSubcanalesByCanal.Peticion

    '        If lstCanal.SelectedIndex >= 0 Then

    '            objPeticion.Codigo = New List(Of String)
    '            If lstCanal IsNot Nothing AndAlso lstCanal.Items.Count > 0 Then

    '                For Each CanalSelecionado As ListItem In lstCanal.Items
    '                    If CanalSelecionado.Selected Then
    '                        objPeticion.Codigo.Add(CanalSelecionado.Value)
    '                    End If
    '                Next

    '            End If

    '            objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

    '            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '                Exit Sub
    '            End If

    '            lstSubCanal.AppendDataBoundItems = True
    '            'Limpa os subcanais
    '            lstSubCanal.Items.Clear()

    '        End If


    '        If objRespuesta.Canales IsNot Nothing AndAlso
    '           objRespuesta.Canales.Count > 0 Then

    '            SubCanales = New IacUtilidad.GetComboSubcanalesByCanal.SubCanalColeccion

    '            For Each canal In objRespuesta.Canales

    '                If canal.SubCanales IsNot Nothing Then

    '                    'ordena a lista de sub canales
    '                    canal.SubCanales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

    '                    SubCanales.AddRange(canal.SubCanales)

    '                    For Each subCanal In canal.SubCanales

    '                        'Adiciona o item na coleção
    '                        lstSubCanal.Items.Add(New ListItem(subCanal.Codigo & " - " & subCanal.Descripcion, subCanal.Codigo))

    '                    Next

    '                End If

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox Canal
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Public Sub PreencherListBoxPuesto()

    '        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
    '            Exit Sub
    '        End If

    '        Dim objProxyPuesto As New Comunicacion.ProxyPuesto
    '        Dim objRespuesta As New IAC.ContractoServicio.Puesto.GetPuestos.Respuesta

    '        objRespuesta = objProxyPuesto.GetPuestos(New IAC.ContractoServicio.Puesto.GetPuestos.Peticion With { _
    '                                                 .BolVigente = True, _
    '                                                 .CodigoAplicacion = CodAplicacion, _
    '                                                 .CodigoDelegacion = ddlDelegacion.SelectedValue})

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        lstPuesto.AppendDataBoundItems = True
    '        lstPuesto.Items.Clear()

    '        If objRespuesta.Puestos IsNot Nothing Then
    '            'ordena a lista de canales
    '            objRespuesta.Puestos.Sort(Function(i, j) i.CodigoPuesto.CompareTo(j.CodigoPuesto))

    '            For Each objPuesto In objRespuesta.Puestos
    '                lstPuesto.Items.Add(New ListItem(objPuesto.CodigoPuesto, objPuesto.CodigoPuesto))
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox Canal
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Public Sub PreencherListBoxDivisas()

    '        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
    '        Dim objRespuesta As New IacUtilidad.GetComboDivisas.Respuesta

    '        objRespuesta = objProxyUtilida.GetComboDivisas

    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        lstDivisa.AppendDataBoundItems = True

    '        lstDivisa.Items.Clear()

    '        If objRespuesta.Divisas IsNot Nothing Then

    '            Divisas = objRespuesta.Divisas

    '            'ordena a lista de canales
    '            objRespuesta.Divisas.Sort(Function(i, j) i.CodigoIso.CompareTo(j.CodigoIso))

    '            For Each objDivisa In objRespuesta.Divisas
    '                lstDivisa.Items.Add(New ListItem(objDivisa.CodigoIso & " - " & objDivisa.Descripcion, objDivisa.CodigoIso))
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche o listbox Canal
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [kasantos] 08/04/2013 Criado
    '    ''' </history>
    '    Public Sub PreencherListBoxContador()

    '        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
    '            Exit Sub
    '        End If

    '        Dim objProxyLogin As New Comunicacion.ProxyLogin
    '        Dim objPeticion As New ContractoServ.GetUsuariosDetail.Peticion
    '        Dim objRespuesta As ContractoServ.GetUsuariosDetail.Respuesta

    '        objRespuesta = objProxyLogin.GetUsuariosDetail(New ContractoServ.GetUsuariosDetail.Peticion With { _
    '                                                       .Delegacion = ddlDelegacion.SelectedValue, _
    '                                                       .Aplicacion = CodAplicacion})


    '        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, Nothing, objRespuesta.MensajeError) Then
    '            Exit Sub
    '        End If

    '        lstContador.AppendDataBoundItems = True

    '        lstContador.Items.Clear()

    '        If objRespuesta.Usuarios IsNot Nothing Then

    '            'ordena a lista de canales
    '            objRespuesta.Usuarios.Sort(Function(i, j) i.Login.CompareTo(j.Login))

    '            Usuarios = objRespuesta.Usuarios

    '            For Each objUsuario In objRespuesta.Usuarios
    '                lstContador.Items.Add(New ListItem(objUsuario.Login & " - " & objUsuario.Nombre, objUsuario.Login))
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Carrega os formatos de arquivos
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub CarregarFormatoArchivo()

    '        rblFormatoSalida.Items.Clear()


    '        rblFormatoSalida.Items.Add(New ListItem With { _
    '                                   .Text = Traduzir("024_rbnCSV"), _
    '                                   .Value = Enumeradores.eFormatoArchivo.CSV})

    '        rblFormatoSalida.Items.Add(New ListItem With { _
    '                                   .Text = Traduzir("024_rbnPDF"), _
    '                                   .Value = Enumeradores.eFormatoArchivo.PDF})

    '        rblFormatoSalida.Items.Add(New ListItem With { _
    '                                   .Text = Traduzir("024_rbnEXCEL"), _
    '                                   .Value = Enumeradores.eFormatoArchivo.EXCEL})

    '        rblFormatoSalida.Items.Add(New ListItem With { _
    '                                   .Text = Traduzir("024_rbnHTML"), _
    '                                   .Value = Enumeradores.eFormatoArchivo.MHTML})
    '    End Sub

    '    ''' <summary>
    '    ''' Trata o foco da página
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [magnum.oliveira] 20/07/2009 Criado
    '    ''' </history>
    '    Private Sub TrataFoco()

    '        If (Not IsPostBack) Then
    '            Util.HookOnFocus(DirectCast(Me.Page, Control))
    '        Else
    '            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
    '                Page.SetFocus(Request("__LASTFOCUS"))
    '            End If
    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub CarregarControles()

    '        'Carrega os formatos de arquivo
    '        CarregarFormatoArchivo()

    '        PreencherListBoxDelegacion()
    '        PreencherListBoxCanal()
    '        PreencherListBoxSubCanal()
    '        PreencherListBoxPuesto()
    '        PreencherListBoxDivisas()
    '        PreencherListBoxContador()
    '        PreencherListBoxGrupoClientes()

    '        'Recupera os relatorios
    '        ConnectRS(False, GetCredencial())
    '        Relatorios = DisplayItems(Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CarpetaReportes"))

    '        'Popula a combo de relatorios
    '        PopularRelatorios()

    '    End Sub

    '    ''' <summary>
    '    ''' Popula a combo de relatorios
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub PopularRelatorios()

    '        ddlReportes.Items.Clear()

    '        If Relatorios IsNot Nothing AndAlso Relatorios.Count > 0 Then

    '            For Each relatorio In Relatorios
    '                ' se for objeto relatório..
    '                If relatorio.TypeName = "Report" Then
    '                    ddlReportes.Items.Add(New ListItem With { _
    '                                      .Text = relatorio.Name, _
    '                                      .Value = relatorio.Name})
    '                End If
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Consome os clientes selecionados na tela de busca a partir da sessão
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ConsomeClientes()

    '        'lstCliente.AppendDataBoundItems = True
    '        txtCliente.Text = String.Empty

    '        ' se há clientes na sessão, move para o viewstate
    '        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

    '            'ordena a lista de canales
    '            Clientes.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
    '            Dim DescripcionCliente As String = String.Empty

    '            For Each cliente In Clientes
    '                DescripcionCliente = cliente.Codigo & " - " & cliente.Descripcion

    '                If DescripcionCliente.Length > CantidadCaracteresTextBox Then
    '                    txtCliente.Text &= DescripcionCliente.Substring(0, CantidadCaracteresTextBox) & "..." & vbCrLf
    '                Else
    '                    txtCliente.Text &= DescripcionCliente & vbCrLf
    '                End If

    '            Next

    '            btnSubCliente.Habilitado = True

    '            If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 Then

    '                Dim subclientesRemover As New List(Of String)

    '                For Each subcli In SubClientesCompleto

    '                    If Clientes.FindAll(Function(c) c.Codigo = subcli.Codigo).Count = 0 Then
    '                        subclientesRemover.Add(subcli.Codigo)
    '                    End If

    '                Next

    '                If subclientesRemover.Count > 0 Then

    '                    For Each subCliRemov In subclientesRemover
    '                        SubClientesCompleto.RemoveAll(Function(s) s.Codigo = subCliRemov)
    '                    Next

    '                End If

    '            End If

    '        Else

    '            btnSubCliente.Habilitado = False
    '            btnPuntoServicio.Habilitado = False
    '            SubClientesCompleto = Nothing
    '            PuntosServiciosCompleto = Nothing
    '            txtSubCliente.Text = String.Empty
    '            txtPuntoServicio.Text = String.Empty

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Consome os clientes selecionados na tela de busca a partir da sessão
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ConsomeSubClientes()

    '        txtSubCliente.Text = String.Empty

    '        ' se há clientes na sessão, move para o viewstate
    '        If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 Then


    '            For Each cliente In SubClientesCompleto

    '                If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then

    '                    'ordena a lista de canales
    '                    cliente.SubClientes.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
    '                    Dim DescripcionSCliente As String = String.Empty

    '                    For Each subcliente In cliente.SubClientes

    '                        DescripcionSCliente = cliente.Codigo & " - " & subcliente.Codigo & " - " & subcliente.Descripcion

    '                        If DescripcionSCliente.Length > CantidadCaracteresTextBox Then
    '                            txtSubCliente.Text &= DescripcionSCliente.Substring(0, CantidadCaracteresTextBox) & "..." & vbCrLf
    '                        Else
    '                            txtSubCliente.Text &= DescripcionSCliente & vbCrLf
    '                        End If

    '                    Next

    '                End If

    '            Next

    '            btnPuntoServicio.Habilitado = True

    '            If PuntosServiciosCompleto IsNot Nothing AndAlso PuntosServiciosCompleto.Count > 0 Then

    '                Dim PuntosServiciosRemover As New List(Of KeyValuePair(Of String, String))

    '                For Each cli In PuntosServiciosCompleto

    '                    If cli.SubClientes IsNot Nothing AndAlso cli.SubClientes.Count > 0 Then

    '                        For Each subcli In cli.SubClientes

    '                            If (From c In SubClientesCompleto, sc In c.SubClientes Where c.Codigo = cli.Codigo AndAlso sc.Codigo = subcli.Codigo).Count = 0 Then
    '                                PuntosServiciosRemover.Add(New KeyValuePair(Of String, String)(cli.Codigo, subcli.Codigo))
    '                            End If

    '                        Next

    '                    End If

    '                Next

    '                If PuntosServiciosRemover.Count > 0 Then

    '                    Dim PuntoServicio As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing

    '                    For Each subCliRemov In PuntosServiciosRemover

    '                        PuntoServicio = (From c In PuntosServiciosCompleto Where c.Codigo = subCliRemov.Key).FirstOrDefault

    '                        If PuntoServicio IsNot Nothing AndAlso PuntoServicio.SubClientes IsNot Nothing AndAlso PuntoServicio.SubClientes.Count > 0 Then

    '                            PuntoServicio.SubClientes.RemoveAll(Function(sc) sc.Codigo = subCliRemov.Value)

    '                        End If

    '                        If PuntoServicio.SubClientes Is Nothing OrElse PuntoServicio.SubClientes.Count = 0 Then PuntosServiciosCompleto.RemoveAll(Function(c) c.Codigo = subCliRemov.Key)

    '                    Next

    '                End If

    '            End If

    '        Else
    '            btnPuntoServicio.Habilitado = False
    '            PuntosServiciosCompleto = Nothing
    '            txtPuntoServicio.Text = String.Empty
    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Consome os clientes selecionados na tela de busca a partir da sessão
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ConsomePtoServicio()

    '        txtPuntoServicio.Text = String.Empty

    '        ' se há clientes na sessão, move para o viewstate
    '        If PuntosServiciosCompleto IsNot Nothing AndAlso PuntosServiciosCompleto.Count > 0 Then

    '            For Each cliente In PuntosServiciosCompleto

    '                If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then

    '                    For Each subcliente In cliente.SubClientes

    '                        If subcliente.PuntosServicio IsNot Nothing AndAlso subcliente.PuntosServicio.Count > 0 Then

    '                            'ordena a lista de canales
    '                            subcliente.PuntosServicio.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
    '                            Dim DescripcionPuntoServicio As String = String.Empty

    '                            For Each ptoServ In subcliente.PuntosServicio

    '                                DescripcionPuntoServicio = cliente.Codigo & " - " & subcliente.Codigo & " - " & ptoServ.Codigo & " - " & ptoServ.Descripcion

    '                                If DescripcionPuntoServicio.Length > CantidadCaracteresTextBox Then
    '                                    txtPuntoServicio.Text &= DescripcionPuntoServicio.Substring(0, CantidadCaracteresTextBox) & "..." & vbCrLf
    '                                Else
    '                                    txtPuntoServicio.Text &= DescripcionPuntoServicio & vbCrLf
    '                                End If

    '                            Next


    '                        End If

    '                    Next

    '                End If
    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' obtem os parametros do relatório
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 03/05/2013 - Criado
    '    ''' </history>
    '    Private Sub ObtenerParametros()

    '        If ddlReportes.SelectedIndex >= 0 AndAlso Relatorios IsNot Nothing AndAlso Relatorios.Count > 0 Then

    '            Dim objReporte As RS2010.CatalogItem = (From objR In Relatorios Where objR.Name = ddlReportes.SelectedValue).FirstOrDefault

    '            If objReporte IsNot Nothing Then

    '                Dim objValores As RS2005.ParameterValue() = Nothing
    '                Dim objValores2010 As RS2010.ParameterValue() = Nothing

    '                'Lista os parametros do relatório
    '                ParametrosReporte = ListarParametros(objReporte.Path, objValores)
    '                'ParametrosReporte2010 = ListarParametros(objReporte.Path, objValores2010)

    '            End If

    '        End If
    '    End Sub

    '    ''' <summary>
    '    ''' Grava o registro na base.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ExecutarGrabar()

    '        ObtenerParametros()

    '        ValidaControles(True)

    '        If Valida.Count = 0 Then

    '            Dim objPeticion As New IacIntegracion.Reportes.SetConfiguracionReporte.Peticion
    '            Dim objProxy As New Comunicacion.ProxyIacIntegracion
    '            Dim objRespuesta As IacIntegracion.Reportes.SetConfiguracionReporte.Respuesta = Nothing
    '            Dim objParametros As IacIntegracion.Reportes.ParametroReporteColeccion = Nothing

    '            objPeticion.EsExclusion = False
    '            objPeticion.ConfiguracionesReportes = New IacIntegracion.Reportes.ConfiguracionReporteColeccion

    '            objPeticion.ConfiguracionesReportes.Add(New IacIntegracion.Reportes.ConfiguracionReporte With { _
    '                                                    .BolAgruparCliente = chkAgrupadocliente.Checked, _
    '                                                    .BolAgruparGrupoCliente = chkAgrupadoGrupoCliente.Checked, _
    '                                                    .BolAgruparPuntoServicio = chkAgrupadoPuntoServicio.Checked, _
    '                                                    .BolAgruparSubCliente = chkAgrupadoSubCliente.Checked, _
    '                                                    .IdentificadorConfiguracion = IdentificadorConfiguracion, _
    '                                                    .NecFormatoArchivo = rblFormatoSalida.SelectedValue, _
    '                                                    .DesReporte = ddlReportes.SelectedValue, _
    '                                                    .DesRuta = txtRuta.Text, _
    '                                                    .CodConfiguracion = txtCodConfiguracion.Text, _
    '                                                    .DesConfiguracion = txtDesConfiguracion.Text, _
    '                                                    .CodUsuario = LoginUsuario, _
    '                                                    .ParametrosReporte = RetornarParametros(, , , , True, , True)})


    '            objRespuesta = objProxy.SetConfiguracionReporte(objPeticion)

    '            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, Nothing, objRespuesta.MensajeError) Then
    '                Exit Sub
    '            End If

    '            IdentificadorConfiguracion = objRespuesta.IdentificadorConfiguracion

    '            'Mensagem Sucesso
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "alert('" & Traduzir("info_msg_grabadosucesso") & "');", True)

    '        Else
    '            ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
    '            Master.ControleErro.ShowError(Valida, Enumeradores.eMensagem.Atencao)
    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Retorna os valores dos parametros
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function RetornarParametros(Optional ObjCliente As IacUtilidad.GetComboClientes.Cliente = Nothing, _
    '                                       Optional objSubCliente As IacUtilidad.GetComboSubclientesByCliente.SubCliente = Nothing, _
    '                                       Optional objPuntoServicio As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.PuntoServicio = Nothing, _
    '                                       Optional objGrupoClientes As IAC.ContractoServicio.GrupoCliente.GrupoCliente = Nothing, _
    '                                       Optional RetornarCodigoConcatenado As Boolean = False, Optional InformarValoresNulos As Boolean = False, _
    '                                       Optional EsGrabacionBanco As Boolean = False) As IacIntegracion.Reportes.ParametroReporteColeccion

    '        Dim objParametros As IacIntegracion.Reportes.ParametroReporteColeccion = Nothing
    '        Dim InformouValor As Boolean = False

    '        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte.Count > 0 Then

    '            objParametros = New IacIntegracion.Reportes.ParametroReporteColeccion

    '            For Each parametro In ParametrosReporte

    '                Select Case parametro.Name

    '                    Case Constantes.CONST_P_FECHA_CONTEO_DESDE

    '                        If InformarValoresNulos Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, If(Not String.IsNullOrEmpty(txtFechaConteoDesde.Text), txtFechaConteoDesde.Text, Nothing)))
    '                        Else
    '                            If Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) Then objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, txtFechaConteoDesde.Text))
    '                        End If

    '                    Case Constantes.CONST_P_FECHA_CONTEO_HASTA

    '                        If InformarValoresNulos Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, If(Not String.IsNullOrEmpty(txtFechaConteoHasta.Text), txtFechaConteoHasta.Text, Nothing)))
    '                        Else
    '                            If Not String.IsNullOrEmpty(txtFechaConteoHasta.Text) Then objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, txtFechaConteoHasta.Text))
    '                        End If

    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_DESDE

    '                        If InformarValoresNulos Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, If(Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text), txtFechaTransporteDesde.Text, Nothing)))
    '                        Else
    '                            If Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) Then objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, txtFechaTransporteDesde.Text))
    '                        End If

    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_HASTA

    '                        If InformarValoresNulos Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, If(Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text), txtFechaTransporteHasta.Text, Nothing)))
    '                        Else
    '                            If Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text) Then objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, txtFechaTransporteHasta.Text))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_CLIENTE

    '                        If InformarValoresNulos AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE).FirstOrDefault() Is Nothing Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, String.Empty))
    '                        End If

    '                        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

    '                            If chkAgrupadocliente.Checked OrElse ObjCliente Is Nothing Then

    '                                For Each Cliente In Clientes
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, Cliente.Descripcion, Cliente.Codigo))
    '                                Next

    '                            Else
    '                                objParametros.Add(RecuperarValorParametro(parametro.Name, ObjCliente.Descripcion, ObjCliente.Codigo))
    '                            End If

    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_SUBCLIENTE

    '                        If InformarValoresNulos AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_SUBCLIENTE).FirstOrDefault() Is Nothing Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, String.Empty))
    '                        End If

    '                        If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 Then

    '                            If chkAgrupadoSubCliente.Checked OrElse objSubCliente Is Nothing Then

    '                                For Each cliente In If(ObjCliente IsNot Nothing,
    '                                                       SubClientesCompleto.FindAll(Function(s) s.Codigo = ObjCliente.Codigo),
    '                                                       SubClientesCompleto)

    '                                    If cliente.SubClientes IsNot Nothing AndAlso cliente.SubClientes.Count > 0 Then

    '                                        For Each subCliente In cliente.SubClientes
    '                                            objParametros.Add(RecuperarValorParametro(parametro.Name, subCliente.Descripcion, If(RetornarCodigoConcatenado,
    '                                                                                                                                 cliente.Codigo & "|" & subCliente.Codigo, _
    '                                                                                                                                 subCliente.Codigo)))
    '                                            ' Verifica se o cliente do subcliente informado existe na lista de parâmetros
    '                                            Dim param As IacIntegracion.Reportes.ParametroReporte = objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE AndAlso p.DesValorParametro = cliente.Codigo AndAlso Not EsGrabacionBanco).FirstOrDefault()
    '                                            ' Se existe
    '                                            If param IsNot Nothing Then
    '                                                ' Apaga o cliente da lista, pois somente se deve passar os subclientes, não é necessário passar o cliente
    '                                                objParametros.Remove(param)
    '                                            End If

    '                                        Next

    '                                    End If

    '                                Next

    '                            Else
    '                                objParametros.Add(RecuperarValorParametro(parametro.Name, objSubCliente.Descripcion, objSubCliente.Codigo))
    '                            End If

    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_PUNTO_SERVICIO

    '                        If InformarValoresNulos AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO).FirstOrDefault() Is Nothing Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, String.Empty))
    '                        End If

    '                        If PuntosServiciosCompleto IsNot Nothing AndAlso PuntosServiciosCompleto.Count > 0 Then

    '                            If chkAgrupadoPuntoServicio.Checked OrElse objPuntoServicio Is Nothing Then

    '                                For Each Cliente In If(ObjCliente IsNot Nothing,
    '                                                       PuntosServiciosCompleto.FindAll(Function(c) c.Codigo = ObjCliente.Codigo),
    '                                                       PuntosServiciosCompleto)

    '                                    If Cliente.SubClientes IsNot Nothing AndAlso Cliente.SubClientes.Count > 0 Then

    '                                        For Each SubCliente In If(objSubCliente IsNot Nothing,
    '                                                                  Cliente.SubClientes.FindAll(Function(s) s.Codigo = objSubCliente.Codigo),
    '                                                                  Cliente.SubClientes)

    '                                            For Each puntoServicio In SubCliente.PuntosServicio
    '                                                objParametros.Add(RecuperarValorParametro(parametro.Name, puntoServicio.Descripcion, If(RetornarCodigoConcatenado, _
    '                                                                                                                                        Cliente.Codigo & "|" & SubCliente.Codigo & "|" & puntoServicio.Codigo, _
    '                                                                                                                                        puntoServicio.Codigo)))
    '                                                ' Verifica se o subcliente do punto de serviço informado existe na lista de parâmetros
    '                                                Dim param As IacIntegracion.Reportes.ParametroReporte = objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE AndAlso p.DesValorParametro = Cliente.Codigo & "|" & SubCliente.Codigo AndAlso Not EsGrabacionBanco).FirstOrDefault()
    '                                                ' Se existe
    '                                                If param IsNot Nothing Then
    '                                                    ' Apaga o subcliente da lista, pois somente se deve passar os pontos de serviço, não é necessário passar o subcliente
    '                                                    objParametros.Remove(param)
    '                                                End If

    '                                            Next

    '                                        Next

    '                                    End If

    '                                Next

    '                            Else
    '                                objParametros.Add(RecuperarValorParametro(parametro.Name, objPuntoServicio.Descripcion, objPuntoServicio.Codigo))
    '                            End If

    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_GRUPO_CLIENTES

    '                        If Gruposclientes IsNot Nothing AndAlso Gruposclientes.Count > 0 Then

    '                            If InformarValoresNulos Then objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, String.Empty))

    '                            If chkAgrupadoGrupoCliente.Checked OrElse objGrupoClientes Is Nothing Then

    '                                For Each GrupoCliente In Gruposclientes

    '                                    If (From objGrupo As ListItem In lstGrupoCliente.Items Where objGrupo.Selected = True AndAlso objGrupo.Value = GrupoCliente.Codigo).Count > 0 Then

    '                                        objParametros.Add(RecuperarValorParametro(parametro.Name, GrupoCliente.Descripcion, GrupoCliente.Codigo))

    '                                        ' Recupara os cliente, subcliente e pontos de serviços do grupo
    '                                        RetornarParametrosGrupoClienteSubClientePuntoServico(objParametros, New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With
    '                                                                                                {
    '                                                                                                    .Codigo = GrupoCliente.Codigo
    '                                                                                                }, EsGrabacionBanco)
    '                                    End If

    '                                Next

    '                            Else

    '                                ' Recupara os cliente, subcliente e pontos de serviços do grupo
    '                                RetornarParametrosGrupoClienteSubClientePuntoServico(objParametros, New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion With
    '                                                                                            {
    '                                                                                                .Codigo = objGrupoClientes.Codigo
    '                                                                                            }, EsGrabacionBanco)
    '                            End If

    '                        ElseIf InformarValoresNulos Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, String.Empty))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_CANAL

    '                        InformouValor = False

    '                        If lstCanal.Items IsNot Nothing AndAlso lstCanal.Items.Count > 0 Then

    '                            Dim objCanal As IacUtilidad.GetComboCanales.Canal = Nothing

    '                            For Each canal As ListItem In lstCanal.Items

    '                                If canal.Selected Then
    '                                    objCanal = (From c In Canales Where c.Codigo = canal.Value).FirstOrDefault
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, objCanal.Descripcion, objCanal.Codigo))
    '                                    InformouValor = True
    '                                End If

    '                            Next

    '                        End If

    '                        If Not InformouValor Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_SUBCANAL

    '                        InformouValor = False
    '                        If lstSubCanal.Items IsNot Nothing AndAlso lstSubCanal.Items.Count > 0 Then

    '                            Dim objSubCanal As IacUtilidad.GetComboSubcanalesByCanal.SubCanal = Nothing

    '                            For Each SubCanal As ListItem In lstSubCanal.Items

    '                                If SubCanal.Selected Then
    '                                    objSubCanal = (From sc In SubCanales Where sc.Codigo = SubCanal.Value).FirstOrDefault
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, objSubCanal.Descripcion, objSubCanal.Codigo))
    '                                    InformouValor = True
    '                                End If

    '                            Next

    '                        End If

    '                        If Not InformouValor Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_DIVISA

    '                        InformouValor = False
    '                        If lstDivisa.Items IsNot Nothing AndAlso lstDivisa.Items.Count > 0 Then

    '                            Dim objDivisa As IacUtilidad.GetComboDivisas.Divisa = Nothing

    '                            For Each Divisa As ListItem In lstDivisa.Items

    '                                If Divisa.Selected Then
    '                                    objDivisa = (From d In Divisas Where d.CodigoIso = Divisa.Value).FirstOrDefault
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, objDivisa.Descripcion, objDivisa.CodigoIso))
    '                                    InformouValor = True
    '                                End If

    '                            Next

    '                        End If

    '                        If Not InformouValor Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_PUESTO

    '                        InformouValor = False

    '                        If lstPuesto.Items IsNot Nothing AndAlso lstPuesto.Items.Count > 0 Then

    '                            For Each Puesto As ListItem In lstPuesto.Items

    '                                If Puesto.Selected Then
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Puesto.Value))
    '                                    InformouValor = True
    '                                End If

    '                            Next

    '                        End If

    '                        If Not InformouValor Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_CONTADOR

    '                        InformouValor = False

    '                        If lstContador.Items IsNot Nothing AndAlso lstContador.Items.Count > 0 Then

    '                            Dim usuario As ContractoServ.GetUsuariosDetail.Usuario = Nothing

    '                            For Each Contador As ListItem In lstContador.Items

    '                                If Contador.Selected Then
    '                                    usuario = (From usu In Usuarios Where usu.Login = Contador.Value).FirstOrDefault
    '                                    objParametros.Add(RecuperarValorParametro(parametro.Name, usuario.Nombre, usuario.Login))
    '                                    InformouValor = True
    '                                End If

    '                            Next

    '                        End If

    '                        If Not InformouValor Then
    '                            objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                        End If

    '                    Case Constantes.CONST_P_CODIGO_DELEGACION

    '                        If Not String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then

    '                            Dim Delegacion As IacUtilidad.GetComboDelegaciones.Delegacion = (From d In Delegaciones Where d.Codigo = ddlDelegacion.SelectedValue).FirstOrDefault

    '                            If Delegacion IsNot Nothing Then
    '                                objParametros.Add(RecuperarValorParametro(parametro.Name, Delegacion.Descripcion, Delegacion.Codigo))
    '                            ElseIf InformarValoresNulos Then
    '                                objParametros.Add(RecuperarValorParametro(parametro.Name, String.Empty, Nothing))
    '                            End If

    '                        End If

    '                End Select

    '            Next

    '        End If

    '        Return objParametros

    '    End Function

    '    Private Sub RetornarParametrosGrupoClienteSubClientePuntoServico(ByRef objParametros As IacIntegracion.Reportes.ParametroReporteColeccion, objPeticionGrupoClienteDetail As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion, EsGrabacionBanco As Boolean)

    '        Dim objProxyGrupoClientes As New Comunicacion.ProxyIacGrupoClientes

    '        ' Verifica se não está gravando, ou seja, o relatório será gerado
    '        If Not EsGrabacionBanco Then

    '            ' Para cada cliente existente no grupo
    '            For Each cliente In objProxyGrupoClientes.GetGruposClientesDetalle(objPeticionGrupoClienteDetail).GrupoCliente.Clientes

    '                ' Se existe o parâmetro de Cliente
    '                If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_CLIENTE).Count > 0 Then

    '                    ' Adiciona o cliente a lista de parâmetros
    '                    objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_CLIENTE, cliente.DesCliente, cliente.CodCliente))

    '                End If

    '                ' Para cada subcliente existente
    '                If Not String.IsNullOrEmpty(cliente.CodSubCliente) Then

    '                    ' Se existe o parâmetro de SubCliente
    '                    If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_SUBCLIENTE).Count > 0 Then

    '                        ' Adiciona o subcliente a lista de parâmetros
    '                        objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_SUBCLIENTE, cliente.DesSubCliente, cliente.CodCliente & "|" & cliente.CodSubCliente))
    '                        ' Verifica se o cliente do subcliente informado existe na lista de parâmetros
    '                        Dim paramCliente As IacIntegracion.Reportes.ParametroReporte = objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE AndAlso p.DesValorParametro = cliente.CodCliente).FirstOrDefault()
    '                        ' Se existe
    '                        If paramCliente IsNot Nothing Then
    '                            ' Apaga o cliente da lista, pois somente se deve passar os subclientes, não é necessário passar o cliente
    '                            objParametros.Remove(paramCliente)
    '                        End If

    '                    End If

    '                    ' Para cada ponto de serviço existente
    '                    If Not String.IsNullOrEmpty(cliente.CodPtoServicio) Then

    '                        ' Se existe o parâmetro de Ponto de Serviço
    '                        If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 Then

    '                            ' Adiciona o ponto de serviço a lista de parâmetros
    '                            objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_PUNTO_SERVICIO, cliente.DesPtoServivico, cliente.CodCliente & "|" & cliente.CodSubCliente & "|" & cliente.CodPtoServicio))
    '                            ' Verifica se o subcliente do ponto de serviço informado existe na lista de parâmetros
    '                            Dim paramSubCliente As IacIntegracion.Reportes.ParametroReporte = objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_SUBCLIENTE AndAlso p.DesValorParametro = cliente.CodCliente & "|" & cliente.CodSubCliente).FirstOrDefault()
    '                            ' Se existe
    '                            If paramSubCliente IsNot Nothing Then
    '                                ' Apaga o subcliente da lista, pois somente se deve passar os pontos de serviços, não é necessário passar o subcliente
    '                                objParametros.Remove(paramSubCliente)
    '                            End If

    '                        End If

    '                    End If

    '                End If

    '            Next

    '            ' Se existe o parametro de Cliente e não existe o codigo do cliente
    '            If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_CLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_CLIENTE).FirstOrDefault() Is Nothing Then
    '                ' Informa, pois é necessario pelo menos um código em branco
    '                objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_CLIENTE, String.Empty, String.Empty))
    '            End If

    '            ' Se existe o parametro de SubCliente e não existe o codigo do subcliente
    '            If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_SUBCLIENTE).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_SUBCLIENTE).FirstOrDefault() Is Nothing Then
    '                ' Informa, pois é necessario pelo menos um código em branco
    '                objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_SUBCLIENTE, String.Empty, String.Empty))
    '            End If

    '            ' Se existe o parametro de Ponto de Serviço e não existe o codigo do ponto de serviço
    '            If ParametrosReporte.Where(Function(pr) pr.Name = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO).Count > 0 AndAlso objParametros.Where(Function(p) p.CodParametro = Constantes.CONST_P_CODIGO_PUNTO_SERVICIO).FirstOrDefault() Is Nothing Then
    '                ' Informa, pois é necessario pelo menos um código em branco
    '                objParametros.Add(RecuperarValorParametro(Constantes.CONST_P_CODIGO_PUNTO_SERVICIO, String.Empty, String.Empty))
    '            End If

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Recupera o valor do parametro.
    '    ''' </summary>
    '    ''' <param name="CodigoParametro"></param>
    '    ''' <param name="DesParametro"></param>
    '    ''' <param name="DesValorParametro"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function RecuperarValorParametro(CodigoParametro As String, DesParametro As String, DesValorParametro As String) As IacIntegracion.Reportes.ParametroReporte

    '        Return New IacIntegracion.Reportes.ParametroReporte With {.CodParametro = CodigoParametro, _
    '                                                                                    .DesParametro = DesParametro, _
    '                                                                                    .DesValorParametro = DesValorParametro}
    '    End Function

    '    ''' <summary>
    '    ''' MontaMensagensErro
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Public Sub ValidaControles(Optional SetarFocoControle As Boolean = False)

    '        Dim focoSetado As Boolean = False


    '        'Verifica se o código do canal é obrigatório
    '        If txtCodConfiguracion.Text.Trim.Equals(String.Empty) Then

    '            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("024_lblCodigoConfiguracion")))
    '            csvCodConfiguracion.IsValid = False

    '            'Seta o foco no primeiro controle que deu erro
    '            If SetarFocoControle AndAlso Not focoSetado Then
    '                txtCodConfiguracion.Focus()
    '                focoSetado = True
    '            End If

    '        Else
    '            csvCodConfiguracion.IsValid = True
    '        End If

    '        'Verifica se o código do canal é obrigatório
    '        If txtDesConfiguracion.Text.Trim.Equals(String.Empty) Then

    '            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("024_lblDescricaoConfiguracao")))
    '            csvDesConfiguracion.IsValid = False

    '            'Seta o foco no primeiro controle que deu erro
    '            If SetarFocoControle AndAlso Not focoSetado Then
    '                txtDesConfiguracion.Focus()
    '                focoSetado = True
    '            End If

    '        Else
    '            csvDesConfiguracion.IsValid = True
    '        End If

    '        'Verifica se a descrição do canal é obrigatório
    '        If ddlReportes.SelectedIndex < 0 Then

    '            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("024_lblReporte")))
    '            csvReportes.IsValid = False

    '            'Seta o foco no primeiro controle que deu erro
    '            If SetarFocoControle AndAlso Not focoSetado Then
    '                ddlReportes.Focus()
    '                focoSetado = True
    '            End If

    '        Else
    '            csvReportes.IsValid = True
    '        End If


    '        'Validações constantes durante o ciclo de vida de execução da página

    '        'Verifica se o código existe
    '        If String.IsNullOrEmpty(txtRuta.Text) Then

    '            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("024_lblRuta")))
    '            csvRuta.IsValid = False

    '            'Seta o foco no primeiro controle que deu erro
    '            If SetarFocoControle AndAlso Not focoSetado Then
    '                txtRuta.Focus()
    '                focoSetado = True
    '            End If

    '        Else

    '            If Not System.IO.Directory.Exists(txtRuta.Text.Trim) Then

    '                Valida.Add(Traduzir("024_msgDiretorioInvalido"))
    '                csvRuta.IsValid = False

    '                'Seta o foco no primeiro controle que deu erro
    '                If SetarFocoControle AndAlso Not focoSetado Then
    '                    txtRuta.Focus()
    '                    focoSetado = True
    '                End If

    '            Else
    '                csvRuta.IsValid = True
    '            End If

    '        End If

    '        If String.IsNullOrEmpty(rblFormatoSalida.SelectedValue) Then
    '            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("024_lblFormatoArchivo")))
    '            csvRuta.IsValid = False

    '            'Seta o foco no primeiro controle que deu erro
    '            If SetarFocoControle AndAlso Not focoSetado Then
    '                txtRuta.Focus()
    '                focoSetado = True
    '            End If

    '        End If

    '        'Valida os parametros do relatório selecionado
    '        ValidarParametros()


    '    End Sub

    '    ''' <summary>
    '    ''' Valida os parametros do relatório
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ValidarParametros()

    '        If ParametrosReporte IsNot Nothing AndAlso ParametrosReporte.Count > 0 Then

    '            For Each parametro In ParametrosReporte

    '                Select Case parametro.Name

    '                    Case Constantes.CONST_P_CODIGO_CLIENTE
    '                        ValidarParametro(Not parametro.AllowBlank, parametro.MultiValue, False, Nothing, txtCliente.Text, Traduzir("024_lblCliente"), txtCliente, csvTxtCliente, , , , True, , Nothing, lstGrupoCliente.Items, Traduzir("024_lblGrupoClientes"))
    '                    Case Constantes.CONST_P_FECHA_CONTEO_DESDE
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, False, Nothing, txtFechaConteoDesde.Text, Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_desde"), txtFechaConteoDesde, csvFechaConteoDesde, GetType(DateTime))
    '                    Case Constantes.CONST_P_FECHA_CONTEO_HASTA
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, False, Nothing, txtFechaConteoHasta.Text, Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_hasta"), txtFechaConteoHasta, csvFechaConteoHasta, GetType(DateTime))
    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_DESDE
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, False, Nothing, txtFechaTransporteDesde.Text, Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_desde"), txtFechaTransporteDesde, csvFechaTransporteDesde, GetType(DateTime))
    '                    Case Constantes.CONST_P_FECHA_TRANSPORTE_HASTA
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, False, Nothing, txtFechaTransporteHasta.Text, Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_hasta"), txtFechaTransporteHasta, csvFechaTransporteHasta, GetType(DateTime))
    '                    Case Constantes.CONST_P_CODIGO_SUBCLIENTE
    '                        ValidarParametro(Not parametro.AllowBlank, parametro.MultiValue, False, Nothing, txtSubCliente.Text, Traduzir("024_lblSubCliente"), txtSubCliente, csvTxtSubCliente)
    '                    Case Constantes.CONST_P_CODIGO_PUNTO_SERVICIO
    '                        ValidarParametro(Not parametro.AllowBlank, parametro.MultiValue, False, Nothing, txtPuntoServicio.Text, Traduzir("024_lblPuntoServicio"), txtPuntoServicio, csvTxtPuntoServicio)
    '                    Case Constantes.CONST_P_CODIGO_GRUPO_CLIENTES
    '                        ValidarParametro(Not parametro.AllowBlank, parametro.MultiValue, True, lstGrupoCliente.Items, String.Empty, Traduzir("024_lblGrupoClientes"), lstGrupoCliente, csvGrupoCliente, , , True, True, ParametrosReporte.Where(Function(x) x.Name = Constantes.CONST_P_CODIGO_CLIENTE).FirstOrDefault().AllowBlank, txtCliente.Text, Nothing, Traduzir("024_lblCliente"))
    '                    Case Constantes.CONST_P_CODIGO_CANAL
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, True, lstCanal.Items, String.Empty, Traduzir("024_lblCanal"), lstCanal, csvCanal, , , True)
    '                    Case Constantes.CONST_P_CODIGO_SUBCANAL
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, True, lstSubCanal.Items, String.Empty, Traduzir("024_lblSubCanal"), lstSubCanal, csvSubCanal, , , True)
    '                    Case Constantes.CONST_P_CODIGO_DIVISA
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, True, lstDivisa.Items, String.Empty, Traduzir("024_lblDivisa"), lstDivisa, csvDivisa, , , True)
    '                    Case Constantes.CONST_P_CODIGO_PUESTO
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, True, lstPuesto.Items, String.Empty, Traduzir("024_lblPuesto"), lstPuesto, csvPuesto, , , True)
    '                    Case Constantes.CONST_P_CODIGO_CONTADOR
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, True, lstContador.Items, String.Empty, Traduzir("024_lblContador"), lstContador, csvContador, , , True)
    '                    Case Constantes.CONST_P_CODIGO_DELEGACION
    '                        ValidarParametro(Not parametro.Nullable, parametro.MultiValue, False, Nothing, ddlDelegacion.SelectedValue, Traduzir("024_lblDelegacion"), ddlDelegacion, csvDelegacion, , , True)
    '                End Select

    '            Next

    '            ' Verifica se a data de processo inicial é maior do que a data de processo inicial
    '            If (Not String.IsNullOrEmpty(txtFechaConteoDesde.Text) AndAlso Not String.IsNullOrEmpty(txtFechaConteoHasta.Text)) AndAlso _
    '                (IsDate(txtFechaConteoDesde.Text) AndAlso IsDate(txtFechaConteoHasta.Text)) AndAlso _
    '            Date.Compare(Convert.ToDateTime(txtFechaConteoDesde.Text), Convert.ToDateTime(txtFechaConteoHasta.Text)) > 0 Then
    '                Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_hasta"), Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_desde")))
    '            End If

    '            ' Verifica se a data de transporte final é maior do que a data inicial
    '            ' Verifica se a data de processo inicial é maior do que a data de processo inicial
    '            If (Not String.IsNullOrEmpty(txtFechaTransporteDesde.Text) AndAlso Not String.IsNullOrEmpty(txtFechaTransporteHasta.Text)) AndAlso _
    '                (IsDate(txtFechaTransporteDesde.Text) AndAlso IsDate(txtFechaTransporteHasta.Text)) AndAlso _
    '            Date.Compare(Convert.ToDateTime(txtFechaTransporteDesde.Text), Convert.ToDateTime(txtFechaTransporteHasta.Text)) > 0 Then
    '                Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_hasta"), Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_desde")))
    '            End If

    '            For Each param In RecuperarParametrosTela()

    '                Dim Parametro As RS2005.ReportParameter = (From objparametro In ParametrosReporte Where objparametro.Name = param).FirstOrDefault

    '                If Parametro Is Nothing Then

    '                    Select Case param
    '                        Case Constantes.CONST_P_CODIGO_CLIENTE
    '                            ValidarParametro(False, False, False, Nothing, txtCliente.Text, Traduzir("024_lblCliente"), txtCliente, csvTxtCliente, , True)
    '                        Case Constantes.CONST_P_FECHA_CONTEO_DESDE
    '                            ValidarParametro(False, False, False, Nothing, txtFechaConteoDesde.Text, Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_desde"), txtFechaConteoDesde, csvFechaConteoDesde, GetType(DateTime), True)
    '                        Case Constantes.CONST_P_FECHA_CONTEO_HASTA
    '                            ValidarParametro(False, False, False, Nothing, txtFechaConteoHasta.Text, Traduzir("024_lblFechaRecaudacionIni") & " " & Traduzir("lbl_hasta"), txtFechaConteoHasta, csvFechaConteoHasta, GetType(DateTime), True)
    '                        Case Constantes.CONST_P_FECHA_TRANSPORTE_DESDE
    '                            ValidarParametro(False, False, False, Nothing, txtFechaTransporteDesde.Text, Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_desde"), txtFechaTransporteDesde, csvFechaTransporteDesde, GetType(DateTime), True)
    '                        Case Constantes.CONST_P_FECHA_TRANSPORTE_HASTA
    '                            ValidarParametro(False, False, False, Nothing, txtFechaTransporteHasta.Text, Traduzir("024lblFechaTransporteIni") & " " & Traduzir("lbl_hasta"), txtFechaTransporteHasta, csvFechaTransporteHasta, GetType(DateTime), True)
    '                        Case Constantes.CONST_P_CODIGO_SUBCLIENTE
    '                            ValidarParametro(False, False, False, Nothing, txtSubCliente.Text, Traduzir("024_lblSubCliente"), txtSubCliente, csvTxtSubCliente, , True)
    '                        Case Constantes.CONST_P_CODIGO_PUNTO_SERVICIO
    '                            ValidarParametro(False, False, False, Nothing, txtPuntoServicio.Text, Traduzir("024_lblPuntoServicio"), txtPuntoServicio, csvTxtPuntoServicio, , True)
    '                        Case Constantes.CONST_P_CODIGO_GRUPO_CLIENTES
    '                            ValidarParametro(False, False, True, lstGrupoCliente.Items, String.Empty, Traduzir("024_lblGrupoClientes"), lstGrupoCliente, csvGrupoCliente, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_CANAL
    '                            ValidarParametro(False, False, True, lstCanal.Items, String.Empty, Traduzir("024_lblCanal"), lstCanal, csvCanal, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_SUBCANAL
    '                            ValidarParametro(False, False, True, lstSubCanal.Items, String.Empty, Traduzir("024_lblSubCanal"), lstSubCanal, csvSubCanal, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_DIVISA
    '                            ValidarParametro(False, False, True, lstDivisa.Items, String.Empty, Traduzir("024_lblDivisa"), lstDivisa, csvDivisa, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_PUESTO
    '                            ValidarParametro(False, False, True, lstPuesto.Items, String.Empty, Traduzir("024_lblPuesto"), lstPuesto, csvPuesto, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_CONTADOR
    '                            ValidarParametro(False, False, True, lstContador.Items, String.Empty, Traduzir("024_lblContador"), lstContador, csvContador, , True, True)
    '                        Case Constantes.CONST_P_CODIGO_DELEGACION
    '                            ValidarParametro(False, False, False, Nothing, ddlDelegacion.SelectedValue, Traduzir("024_lblDelegacion"), ddlDelegacion, csvDelegacion, , True, True)
    '                    End Select

    '                End If

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Recupera os parâmetros da tela.
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function RecuperarParametrosTela() As List(Of String)

    '        Dim parametros As New List(Of String)

    '        With parametros
    '            .Add(Constantes.CONST_P_CODIGO_DELEGACION)
    '            .Add(Constantes.CONST_P_FECHA_CONTEO_DESDE)
    '            .Add(Constantes.CONST_P_FECHA_CONTEO_HASTA)
    '            .Add(Constantes.CONST_P_FECHA_TRANSPORTE_DESDE)
    '            .Add(Constantes.CONST_P_FECHA_TRANSPORTE_HASTA)
    '            .Add(Constantes.CONST_P_CODIGO_CLIENTE)
    '            .Add(Constantes.CONST_P_CODIGO_SUBCLIENTE)
    '            .Add(Constantes.CONST_P_CODIGO_PUNTO_SERVICIO)
    '            .Add(Constantes.CONST_P_CODIGO_GRUPO_CLIENTES)
    '            .Add(Constantes.CONST_P_CODIGO_CANAL)
    '            .Add(Constantes.CONST_P_CODIGO_SUBCANAL)
    '            .Add(Constantes.CONST_P_CODIGO_DIVISA)
    '            .Add(Constantes.CONST_P_CODIGO_PUESTO)
    '            .Add(Constantes.CONST_P_CODIGO_CONTADOR)
    '        End With

    '        Return parametros
    '    End Function

    '    ''' <summary>
    '    ''' Faz a validação do parametro
    '    ''' </summary>
    '    ''' <param name="Obligatorio"></param>
    '    ''' <param name="MultiSelecion"></param>
    '    ''' <param name="EsColecion"></param>
    '    ''' <param name="Items"></param>
    '    ''' <param name="Valor"></param>
    '    ''' <param name="DesCampo"></param>
    '    ''' <remarks></remarks>
    '    Private Sub ValidarParametro(Obligatorio As Boolean, MultiSelecion As Boolean, EsColecion As Boolean, Items As ListItemCollection, _
    '                                  Valor As String, DesCampo As String, controle As Control, ByRef Validator As Control,
    '                                  Optional tipo As Type = Nothing, Optional ValidarEstaRellenado As Boolean = False,
    '                                  Optional EsSelecionable As Boolean = False,
    '                                  Optional dependenciaOtroCampo As Boolean = False,
    '                                  Optional dependenciaObligatoria As Boolean = False,
    '                                  Optional ValorCampoDependencia As String = Nothing,
    '                                  Optional ItemsDependencia As ListItemCollection = Nothing,
    '                                  Optional DesCampoDependencia As String = Nothing
    '                                  )

    '        If EsColecion Then

    '            If Not ValidarEstaRellenado Then

    '                If Obligatorio AndAlso ((Items Is Nothing OrElse Items.Count = 0) OrElse
    '                                        (EsSelecionable AndAlso Items IsNot Nothing AndAlso (From i As ListItem In Items Where i.Selected = True).Count = 0)) _
    '                                        OrElse dependenciaObligatoria AndAlso ((Items Is Nothing OrElse Items.Count = 0) OrElse
    '                                        (EsSelecionable AndAlso Items IsNot Nothing AndAlso (From i As ListItem In Items Where i.Selected = True).Count = 0)) Then


    '                    If dependenciaOtroCampo Then

    '                        If String.IsNullOrEmpty(ValorCampoDependencia) AndAlso (ItemsDependencia Is Nothing OrElse ItemsDependencia.Count = 0) Then

    '                            Valida.Add(String.Format(Traduzir("lbl_campos_obrigatorios_nao_preenchidos"), DesCampo, DesCampoDependencia))
    '                            CType(Validator, CustomValidator).IsValid = False
    '                            controle.Focus()

    '                        End If

    '                    Else

    '                        Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), DesCampo))
    '                        CType(Validator, CustomValidator).IsValid = False
    '                        controle.Focus()

    '                    End If

    '                ElseIf Not MultiSelecion AndAlso Items IsNot Nothing AndAlso (From i As ListItem In Items Where i.Selected = True).Count > 1 Then
    '                    Valida.Add(String.Format(Traduzir("lbl_campo_selecion_unica"), DesCampo))
    '                    CType(Validator, CustomValidator).IsValid = False
    '                    controle.Focus()
    '                Else
    '                    CType(Validator, CustomValidator).IsValid = True
    '                End If

    '            Else

    '                If Not EsSelecionable AndAlso (Items IsNot Nothing AndAlso Items.Count > 0) Then
    '                    Valida.Add(String.Format(Traduzir("lbl_campo_invalido"), DesCampo))
    '                    CType(Validator, CustomValidator).IsValid = False
    '                    controle.Focus()
    '                ElseIf Items IsNot Nothing AndAlso (From i As ListItem In Items Where i.Selected = True).Count > 0 Then
    '                    Valida.Add(String.Format(Traduzir("lbl_campo_invalido"), DesCampo))
    '                    CType(Validator, CustomValidator).IsValid = False
    '                    controle.Focus()
    '                Else
    '                    CType(Validator, CustomValidator).IsValid = True
    '                End If

    '            End If

    '        Else

    '            If Not ValidarEstaRellenado Then

    '                If Obligatorio AndAlso String.IsNullOrEmpty(Valor) OrElse dependenciaObligatoria AndAlso String.IsNullOrEmpty(Valor) Then

    '                    If dependenciaOtroCampo Then

    '                        If String.IsNullOrEmpty(ValorCampoDependencia) AndAlso ItemsDependencia.Count = 0 Then

    '                            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), DesCampo))
    '                            CType(Validator, CustomValidator).IsValid = False
    '                            controle.Focus()

    '                        End If

    '                    Else

    '                        Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), DesCampo))
    '                        CType(Validator, CustomValidator).IsValid = False
    '                        controle.Focus()

    '                    End If

    '                Else

    '                    If Not String.IsNullOrEmpty(Valor) AndAlso tipo IsNot Nothing AndAlso tipo Is GetType(DateTime) AndAlso Not IsDate(Valor) Then
    '                        Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), DesCampo))
    '                        CType(Validator, CustomValidator).IsValid = False
    '                        controle.Focus()
    '                    Else
    '                        CType(Validator, CustomValidator).IsValid = True
    '                    End If

    '                End If

    '            Else

    '                If Not String.IsNullOrEmpty(Valor) Then
    '                    Valida.Add(String.Format(Traduzir("lbl_campo_invalido"), DesCampo))
    '                    CType(Validator, CustomValidator).IsValid = False
    '                    controle.Focus()
    '                Else
    '                    CType(Validator, CustomValidator).IsValid = True
    '                End If

    '            End If

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Gera o Relatório
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ExecutarGerarInforme()

    '        ObtenerParametros()

    '        ValidaControles(True)

    '        If Valida.Count = 0 Then

    '            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
    '                'Executa o relatório a nivel de grupo de clientes.
    '                GerarRelatorioNivelCliente()

    '            ElseIf Gruposclientes IsNot Nothing AndAlso Gruposclientes.Count > 0 AndAlso Not String.IsNullOrEmpty(lstGrupoCliente.SelectedValue) Then
    '                'Executa o relatório a nivel de grupo de clientes.
    '                GerarRelatorioNivelGrupoClientes(False)

    '            Else
    '                'Executa o relatório a nivel de grupo de clientes.
    '                GerarRelatorioNivelGrupoClientes(True)
    '            End If

    '            'Exibe o diretorio onde foi gerado o relatório.
    '            Dim Diretorio As String = Replace(txtRuta.Text.Trim, "\\", "")
    '            Diretorio = Replace(txtRuta.Text.Trim, "\", "/")

    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "open_window", "window.open('file://" & Diretorio & "');", True)

    '        Else
    '            ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
    '            Master.ControleErro.ShowError(Valida, Enumeradores.eMensagem.Atencao)
    '        End If


    '    End Sub


    '    ''' <summary>
    '    ''' Gera o relatorio a nivel de grupo de clientes.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub GerarRelatorioNivelGrupoClientes(EsAgrupado As Boolean)

    '        If chkAgrupadoGrupoCliente.Checked OrElse EsAgrupado Then

    '            'Executa o relatorios com os clientes agrupados
    '            GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                         RetornarParametros(, , , , True, True), String.Empty, String.Empty, String.Empty, String.Empty, False, False, False, True)
    '        Else

    '            'Executa o relatorio para cada grupo de clientes selecionado
    '            Dim objGrupoCliente As IAC.ContractoServicio.GrupoCliente.GrupoCliente = Nothing

    '            For Each objGrupo As ListItem In (From gc As ListItem In lstGrupoCliente.Items Where gc.Selected)

    '                objGrupoCliente = (From gc In Gruposclientes Where gc.Codigo = objGrupo.Value).FirstOrDefault
    '                GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                             RetornarParametros(, , , objGrupoCliente, True, True), String.Empty, String.Empty, String.Empty, objGrupo.Value, False, False, False, False)

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Retorna o formato do arquivo
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function RetornarFormato() As String

    '        If Not String.IsNullOrEmpty(rblFormatoSalida.SelectedValue) Then

    '            Select Case rblFormatoSalida.SelectedValue

    '                Case Enumeradores.eFormatoArchivo.CSV
    '                    Return Enumeradores.eFormatoArchivo.CSV.ToString
    '                Case Enumeradores.eFormatoArchivo.MHTML
    '                    Return Enumeradores.eFormatoArchivo.MHTML.ToString
    '                Case Enumeradores.eFormatoArchivo.PDF
    '                    Return Enumeradores.eFormatoArchivo.PDF.ToString
    '                Case Enumeradores.eFormatoArchivo.EXCEL
    '                    Return Enumeradores.eFormatoArchivo.EXCEL.ToString
    '            End Select

    '        End If

    '        Return String.Empty
    '    End Function

    '    ''' <summary>
    '    ''' Gera o relatorio a nivel de cliente.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub GerarRelatorioNivelCliente()

    '        If chkAgrupadocliente.Checked Then

    '            'Executa o relatorios com os clientes agrupados
    '            GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                         RetornarParametros(, , , , True, True), String.Empty, String.Empty, String.Empty, String.Empty, True, False, False, False)
    '        Else

    '            'Executa o relatorio para cada cliente
    '            For Each objCliente In Clientes

    '                If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 AndAlso SubClientesCompleto.FindAll(Function(s) s.Codigo = objCliente.Codigo).Count > 0 Then

    '                    'Gera o relatorio a nivel de sub cliente
    '                    GerarRelatorioNivelSubCliente(objCliente)

    '                Else

    '                    'Gera o relatorio a nivel de cliente
    '                    GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                                 RetornarParametros(objCliente, , , , True, True), objCliente.Codigo, String.Empty, String.Empty, False, False, False, False, False)

    '                End If

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Gera o relatorio a nivel de sub cliente.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 08/05/2013 - Criado
    '    ''' </history>
    '    Private Sub GerarRelatorioNivelSubCliente(objCliente As IacUtilidad.GetComboClientes.Cliente)

    '        'Executa o relatorio para cada cliente selecionado
    '        If objCliente IsNot Nothing Then

    '            If chkAgrupadoSubCliente.Checked Then

    '                'Executa o relatorios com os sub-clientes agrupados
    '                GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                             RetornarParametros(objCliente, , , , True, True), objCliente.Codigo, String.Empty, String.Empty, String.Empty, False, True, False, False)
    '            Else

    '                Dim objClienteSubCliente As IacUtilidad.GetComboSubclientesByCliente.Cliente = (From sc In SubClientesCompleto Where sc.Codigo = objCliente.Codigo).FirstOrDefault

    '                If objClienteSubCliente IsNot Nothing AndAlso objClienteSubCliente.SubClientes IsNot Nothing AndAlso objClienteSubCliente.SubClientes.Count > 0 Then

    '                    'Executa o relatorio para casa sub-cliente recuperado
    '                    For Each objSubCliente In objClienteSubCliente.SubClientes

    '                        If PuntosServiciosCompleto IsNot Nothing AndAlso PuntosServiciosCompleto.Count > 0 AndAlso PuntosServiciosCompleto.FindAll(Function(s) s.Codigo = objSubCliente.Codigo).Count > 0 Then

    '                            'Executa o relatório a nivel de punto de servicio.
    '                            GerarRelatorioNivelPuntoServicio(objSubCliente, objCliente)

    '                        Else

    '                            'Executa o relatorio a nivel de sub cliente
    '                            GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                                         RetornarParametros(objCliente, objSubCliente, , , True, True), objCliente.Codigo, objSubCliente.Codigo, String.Empty, String.Empty, False, False, False, False)

    '                        End If

    '                    Next

    '                End If

    '            End If

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Gera o relatorio a nivel de punto de servicio.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 08/05/2013 - Criado
    '    ''' </history>
    '    Private Sub GerarRelatorioNivelPuntoServicio(objSubCliente As IacUtilidad.GetComboSubclientesByCliente.SubCliente, _
    '                                                 objCliente As IacUtilidad.GetComboClientes.Cliente)

    '        'Executa o relatorio para cada cliente selecionado
    '        If objSubCliente IsNot Nothing Then

    '            If chkAgrupadoPuntoServicio.Checked Then

    '                'Executa o relatorios com os sub-clientes agrupados
    '                GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                             RetornarParametros(objCliente, objSubCliente, , , True, True), objCliente.Codigo, objSubCliente.Codigo, String.Empty, String.Empty, False, False, True, False)
    '            Else

    '                Dim objClientePuntoServicio As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = (From ps In PuntosServiciosCompleto Where ps.Codigo = objCliente.Codigo).FirstOrDefault

    '                If objClientePuntoServicio IsNot Nothing AndAlso objClientePuntoServicio.SubClientes IsNot Nothing AndAlso objClientePuntoServicio.SubClientes.Count > 0 Then

    '                    Dim objSubClientePuntoServ As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente = (From ps In objClientePuntoServicio.SubClientes Where ps.Codigo = objSubCliente.Codigo).FirstOrDefault

    '                    If objSubClientePuntoServ IsNot Nothing AndAlso objSubClientePuntoServ.PuntosServicio IsNot Nothing AndAlso objSubClientePuntoServ.PuntosServicio.Count > 0 Then

    '                        'Executa o relatorio para casa sub-cliente recuperado
    '                        For Each objPuntoServicio In objSubClientePuntoServ.PuntosServicio

    '                            'Executa o relatorio a nivel de sub cliente
    '                            GerarInforme(txtCodConfiguracion.Text, ddlReportes.SelectedValue, Relatorios, RetornarFormato, txtRuta.Text, txtDesConfiguracion.Text,
    '                                         RetornarParametros(objCliente, objSubCliente, objPuntoServicio, , True, True), objCliente.Codigo, objSubCliente.Codigo, objPuntoServicio.Codigo, String.Empty, _
    '                                                            False, False, False, False)


    '                        Next

    '                    End If

    '                End If

    '            End If

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Preenche os parametros
    '    ''' </summary>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function PreencherParametros() As List(Of Microsoft.Reporting.WebForms.ReportParameter)

    '        Dim objParametros As List(Of Microsoft.Reporting.WebForms.ReportParameter) = Nothing
    '        Dim objParametrosPreenchidos As IacIntegracion.Reportes.ParametroReporteColeccion = RetornarParametros()

    '        If objParametrosPreenchidos IsNot Nothing AndAlso objParametrosPreenchidos.Count > 0 Then

    '            objParametros = New List(Of Microsoft.Reporting.WebForms.ReportParameter)
    '            Dim DesParametro As String = String.Empty

    '            For Each parametros In objParametrosPreenchidos.GroupBy(Function(p) p.CodParametro)

    '                For Each paramGrup In parametros
    '                    If String.IsNullOrEmpty(DesParametro) Then
    '                        DesParametro = paramGrup.DesParametro
    '                    Else
    '                        DesParametro &= "," & paramGrup.DesParametro
    '                    End If
    '                Next

    '                objParametros.Add(New Microsoft.Reporting.WebForms.ReportParameter(parametros.First.CodParametro, DesParametro))
    '                DesParametro = String.Empty
    '            Next

    '        End If

    '        Return objParametros
    '    End Function

    '    ''' <summary>
    '    ''' Limpa os controles da tela
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 07/05/2013 - Criado
    '    ''' </history>
    '    Private Sub LimparControles(SoloLimparParametrosDinamicos As Boolean)

    '        txtFechaConteoDesde.Text = String.Empty
    '        txtFechaConteoHasta.Text = String.Empty
    '        txtFechaTransporteDesde.Text = String.Empty
    '        txtFechaTransporteHasta.Text = String.Empty

    '        ddlDelegacion.SelectedValue = InformacionUsuario.DelegacionLogin.Codigo
    '        txtCliente.Text = String.Empty
    '        txtSubCliente.Text = String.Empty
    '        txtPuntoServicio.Text = String.Empty
    '        lstCanal.SelectedValue = Nothing
    '        If lstSubCanal.Items IsNot Nothing Then lstSubCanal.Items.Clear()
    '        lstDivisa.SelectedValue = Nothing
    '        If lstContador.Items IsNot Nothing AndAlso lstContador.Items.Count > 0 Then lstContador.SelectedValue = Nothing
    '        If lstPuesto.Items IsNot Nothing AndAlso lstPuesto.Items.Count > 0 Then lstPuesto.SelectedValue = Nothing
    '        Clientes = Nothing
    '        SubClientesCompleto = Nothing
    '        PuntosServiciosCompleto = Nothing
    '        lstGrupoCliente.SelectedValue = Nothing
    '        lstGrupoCliente.Enabled = True
    '        btnCliente.Habilitado = True

    '        'chkAgrupadocliente.Checked = False
    '        chkAgrupadoGrupoCliente.Checked = False
    '        chkAgrupadoPuntoServicio.Checked = False
    '        chkAgrupadoSubCliente.Checked = False

    '        If Not SoloLimparParametrosDinamicos Then
    '            txtDesConfiguracion.Text = String.Empty
    '            txtRuta.Text = String.Empty
    '            rblFormatoSalida.SelectedValue = Nothing
    '            ddlReportes.SelectedIndex = 0
    '            txtCodConfiguracion.Text = String.Empty
    '            txtCodConfiguracion.Focus()
    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Configura os estados dos checkboxes de agrupação
    '    ''' </summary>
    '    ''' <param name="control"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 08/05/2013 - Criado
    '    ''' </history>
    '    Private Sub ConfigurarEstadoControles(control As Control)

    '        Select Case control.ID

    '            Case chkAgrupadocliente.ID

    '                If chkAgrupadocliente.Checked Then
    '                    chkAgrupadoGrupoCliente.Checked = False
    '                    chkAgrupadoGrupoCliente.Enabled = False
    '                ElseIf Clientes Is Nothing OrElse Clientes.Count = 0 Then
    '                    chkAgrupadoGrupoCliente.Enabled = True
    '                End If

    '                chkAgrupadoPuntoServicio.Checked = chkAgrupadocliente.Checked
    '                chkAgrupadoPuntoServicio.Enabled = Not chkAgrupadocliente.Checked

    '                chkAgrupadoSubCliente.Checked = chkAgrupadocliente.Checked
    '                chkAgrupadoSubCliente.Enabled = Not chkAgrupadocliente.Checked

    '            Case chkAgrupadoGrupoCliente.ID

    '                If chkAgrupadoGrupoCliente.Checked Then
    '                    chkAgrupadoPuntoServicio.Checked = Not chkAgrupadoGrupoCliente.Checked
    '                    chkAgrupadoSubCliente.Checked = Not chkAgrupadoGrupoCliente.Checked
    '                    chkAgrupadocliente.Checked = Not chkAgrupadoGrupoCliente.Checked
    '                End If

    '                chkAgrupadoPuntoServicio.Enabled = Not chkAgrupadoGrupoCliente.Checked
    '                chkAgrupadoSubCliente.Enabled = Not chkAgrupadoGrupoCliente.Checked
    '                chkAgrupadocliente.Enabled = Not chkAgrupadoGrupoCliente.Checked
    '                btnCliente.Habilitado = Not chkAgrupadoGrupoCliente.Checked
    '                btnSubCliente.Habilitado = Not chkAgrupadoGrupoCliente.Checked
    '                btnPuntoServicio.Habilitado = Not chkAgrupadoGrupoCliente.Checked

    '            Case chkAgrupadoPuntoServicio.ID

    '                If chkAgrupadoPuntoServicio.Checked Then
    '                    chkAgrupadoGrupoCliente.Checked = Not chkAgrupadoPuntoServicio.Checked
    '                End If

    '                chkAgrupadoGrupoCliente.Enabled = Not chkAgrupadoPuntoServicio.Checked

    '            Case chkAgrupadoSubCliente.ID

    '                If chkAgrupadoSubCliente.Checked Then
    '                    chkAgrupadoGrupoCliente.Checked = Not chkAgrupadoSubCliente.Checked
    '                    chkAgrupadoPuntoServicio.Checked = chkAgrupadoSubCliente.Checked
    '                End If

    '                chkAgrupadoGrupoCliente.Enabled = Not chkAgrupadoSubCliente.Checked
    '                chkAgrupadoPuntoServicio.Enabled = Not chkAgrupadoSubCliente.Checked

    '            Case lstGrupoCliente.ID

    '                Dim Selecionado As Boolean = (From lst As ListItem In lstGrupoCliente.Items Where lst.Selected = True).Count > 0

    '                If Selecionado Then
    '                    chkAgrupadoPuntoServicio.Checked = Not Selecionado
    '                    chkAgrupadoSubCliente.Checked = Not Selecionado
    '                    'chkAgrupadocliente.Checked = Not Selecionado
    '                End If

    '                chkAgrupadoPuntoServicio.Enabled = Not Selecionado
    '                chkAgrupadoSubCliente.Enabled = Not Selecionado
    '                'chkAgrupadocliente.Enabled = Not Selecionado
    '                btnCliente.Habilitado = Not Selecionado

    '                If btnCliente.Habilitado AndAlso Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
    '                    btnSubCliente.Habilitado = True

    '                    If btnSubCliente.Habilitado AndAlso SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 Then
    '                        btnPuntoServicio.Habilitado = True
    '                    Else
    '                        btnPuntoServicio.Habilitado = False
    '                    End If

    '                Else
    '                    btnSubCliente.Habilitado = False
    '                    btnPuntoServicio.Habilitado = False
    '                End If

    '        End Select

    '    End Sub

    '    Private Sub ConverteCliente()

    '        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

    '            If SubClientesCompleto Is Nothing Then SubClientesCompleto = New IacUtilidad.GetComboSubclientesByCliente.ClienteColeccion

    '            For Each cliente In Clientes

    '                If (From objCli In SubClientesCompleto Where objCli.Codigo = cliente.Codigo).Count = 0 Then
    '                    SubClientesCompleto.Add(New IacUtilidad.GetComboSubclientesByCliente.Cliente With { _
    '                                            .Codigo = cliente.Codigo, _
    '                                            .Descripcion = cliente.Descripcion})

    '                End If

    '            Next

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' Convete os subclientes.
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub ConverteSubCliente()

    '        If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 Then

    '            If PuntosServiciosCompleto Is Nothing Then PuntosServiciosCompleto = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
    '            Dim objSubClientes As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion = Nothing
    '            Dim objPuntoServicioCompleto As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente = Nothing

    '            For Each objCli In SubClientesCompleto

    '                objPuntoServicioCompleto = (From objC In PuntosServiciosCompleto Where objC.Codigo = objCli.Codigo).FirstOrDefault

    '                If objPuntoServicioCompleto Is Nothing Then

    '                    If objCli.SubClientes IsNot Nothing AndAlso objCli.SubClientes.Count > 0 Then

    '                        objSubClientes = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion

    '                        For Each subCli In objCli.SubClientes

    '                            objSubClientes.Add(New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With { _
    '                                              .Codigo = subCli.Codigo, _
    '                                              .Descripcion = subCli.Descripcion})

    '                        Next

    '                    End If

    '                    PuntosServiciosCompleto.Add(New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.Cliente With { _
    '                                                .Codigo = objCli.Codigo, _
    '                                                .Descripcion = objCli.Descripcion, _
    '                                                .SubClientes = objSubClientes})

    '                Else

    '                    If objPuntoServicioCompleto.SubClientes Is Nothing Then objPuntoServicioCompleto.SubClientes = New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubClienteColeccion

    '                    If objCli.SubClientes IsNot Nothing AndAlso objCli.SubClientes.Count > 0 Then

    '                        For Each objSubCli In objCli.SubClientes

    '                            If (From c In objPuntoServicioCompleto.SubClientes Where c.Codigo = objSubCli.Codigo).Count = 0 Then

    '                                objPuntoServicioCompleto.SubClientes.Add(New IacUtilidad.getComboPuntosServiciosByClientesSubclientes.SubCliente With { _
    '                                                                        .Codigo = objSubCli.Codigo, _
    '                                                                        .Descripcion = objSubCli.Descripcion})

    '                            End If

    '                        Next

    '                    End If


    '                End If

    '            Next

    '        End If

    '    End Sub

    '#End Region

    '#Region "[PROPRIEDADES]"

    '    Private Property Usuarios As ContractoServ.GetUsuariosDetail.UsuarioColeccion
    '        Get
    '            Return ViewState("Usuarios")
    '        End Get
    '        Set(value As ContractoServ.GetUsuariosDetail.UsuarioColeccion)
    '            ViewState("Usuarios") = value
    '        End Set
    '    End Property

    '    Private Property Divisas As IacUtilidad.GetComboDivisas.DivisaColeccion
    '        Get
    '            Return ViewState("Divisas")
    '        End Get
    '        Set(value As IacUtilidad.GetComboDivisas.DivisaColeccion)
    '            ViewState("Divisas") = value
    '        End Set
    '    End Property

    '    Private Property SubCanales As IacUtilidad.GetComboSubcanalesByCanal.SubCanalColeccion
    '        Get
    '            Return ViewState("SubCanales")
    '        End Get
    '        Set(value As IacUtilidad.GetComboSubcanalesByCanal.SubCanalColeccion)
    '            ViewState("SubCanales") = value
    '        End Set
    '    End Property

    '    Private Property Canales As IacUtilidad.GetComboCanales.CanalColeccion
    '        Get
    '            Return ViewState("Canales")
    '        End Get
    '        Set(value As IacUtilidad.GetComboCanales.CanalColeccion)
    '            ViewState("Canales") = value
    '        End Set
    '    End Property

    '    Private Property Delegaciones As IacUtilidad.GetComboDelegaciones.DelegacionColeccion
    '        Get
    '            Return ViewState("Delegaciones")
    '        End Get
    '        Set(value As IacUtilidad.GetComboDelegaciones.DelegacionColeccion)
    '            ViewState("Delegaciones") = value
    '        End Set
    '    End Property

    '    Private Property ValidarCamposObrigatorios() As Boolean
    '        Get
    '            Return ViewState("ValidarCamposObrigatorios")
    '        End Get
    '        Set(value As Boolean)
    '            ViewState("ValidarCamposObrigatorios") = value
    '        End Set
    '    End Property

    '    Private Property objConfiguracion As IacIntegracion.Reportes.ConfiguracionReporte
    '        Get
    '            Return ViewState("objConfiguracion")
    '        End Get
    '        Set(value As IacIntegracion.Reportes.ConfiguracionReporte)
    '            ViewState("objConfiguracion") = value
    '        End Set
    '    End Property

    '    Private Property IdentificadorConfiguracion As String
    '        Get
    '            Return ViewState("IdentificadorConfiguracion")
    '        End Get
    '        Set(value As String)
    '            ViewState("IdentificadorConfiguracion") = value
    '        End Set
    '    End Property

    '    Private Property ParametrosReporte As List(Of RS2005.ReportParameter)
    '        Get
    '            Return ViewState("ParametrosReporte")
    '        End Get
    '        Set(value As List(Of RS2005.ReportParameter))
    '            ViewState("ParametrosReporte") = value
    '        End Set
    '    End Property

    '    Private Property ParametrosReporte2010 As List(Of RS2010.ItemParameter)
    '        Get
    '            Return ViewState("ItemParameter2010")
    '        End Get
    '        Set(value As List(Of RS2010.ItemParameter))
    '            ViewState("ItemParameter2010") = value
    '        End Set
    '    End Property

    '    Private ReadOnly Property CodAplicacion As String

    '        Get
    '            Return Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("CodAplicacionConteo")
    '        End Get

    '    End Property

    '    Private Property Relatorios As RS2010.CatalogItem()
    '        Get
    '            Return ViewState("Relatorios")
    '        End Get
    '        Set(value As RS2010.CatalogItem())
    '            ViewState("Relatorios") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Armazena os clientes encontrados na busca.
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 18/02/2009 Criado
    '    ''' </history>
    '    Private Property Clientes() As IacUtilidad.GetComboClientes.ClienteColeccion
    '        Get

    '            If Session("ClientesSelecionados") IsNot Nothing Then
    '                ' se for exibir clientes, guarda lista na viewstate no primeiro acesso a propriedade
    '                ViewState("VSClientesSelecionados") = Session("ClientesSelecionados")
    '                Session("ClientesSelecionados") = Nothing
    '            End If

    '            Return ViewState("VSClientesSelecionados")
    '        End Get
    '        Set(value As IacUtilidad.GetComboClientes.ClienteColeccion)
    '            ViewState("VSClientesSelecionados") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Armazena os clientes encontrados na busca.
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 18/02/2009 Criado
    '    ''' </history>
    '    Private Property SubClientesCompleto() As IacUtilidad.GetComboSubclientesByCliente.ClienteColeccion
    '        Get

    '            If Session("SubClientesSelecionadosCompleto") IsNot Nothing Then
    '                ' se for exibir clientes, guarda lista na viewstate no primeiro acesso a propriedade
    '                ViewState("VSSubClientesSelecionados") = Session("SubClientesSelecionadosCompleto")
    '                Session("SubClientesSelecionadosCompleto") = Nothing
    '            End If

    '            Return ViewState("VSSubClientesSelecionados")
    '        End Get
    '        Set(value As IacUtilidad.GetComboSubclientesByCliente.ClienteColeccion)
    '            ViewState("VSSubClientesSelecionados") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Armazena os clientes encontrados na busca.
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 18/02/2009 Criado
    '    ''' </history>
    '    Private Property PuntosServiciosCompleto() As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion
    '        Get

    '            If Session("PtosServicoCompleto") IsNot Nothing Then
    '                ' se for exibir clientes, guarda lista na viewstate no primeiro acesso a propriedade
    '                ViewState("VSPuntosServiciosSelecionados") = Session("PtosServicoCompleto")
    '                Session("PtosServicoCompleto") = Nothing
    '            End If

    '            Return ViewState("VSPuntosServiciosSelecionados")
    '        End Get
    '        Set(value As IacUtilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
    '            ViewState("VSPuntosServiciosSelecionados") = value
    '        End Set
    '    End Property

    '    Private Property ClientesConsulta As IacUtilidad.GetComboClientes.Cliente
    '        Get
    '            Return Session("ClientesConsulta")
    '        End Get
    '        Set(value As IacUtilidad.GetComboClientes.Cliente)
    '            Session("ClientesConsulta") = value
    '        End Set
    '    End Property

    '    ''' <summary>
    '    ''' Armazena os clientes encontrados na busca.
    '    ''' </summary>
    '    ''' <value></value>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [octavio.piramo] 18/02/2009 Criado
    '    ''' </history>
    '    Private Property Gruposclientes() As IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion
    '        Get
    '            Return ViewState("Gruposclientes")
    '        End Get
    '        Set(value As IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion)
    '            ViewState("Gruposclientes") = value
    '        End Set
    '    End Property

    '#End Region

    '#Region "[EVENTOS]"

    '    ''' <summary>
    '    ''' Configura os parametros disponiveis
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub ddlReportes_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlReportes.SelectedIndexChanged

    '        Try
    '            ConfigurarEstadoLinha(True)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDelegacion.SelectedIndexChanged

    '        Try

    '            If ddlDelegacion.SelectedIndex > 0 Then
    '                PreencherListBoxPuesto()
    '                PreencherListBoxContador()
    '            End If

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub lstCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstCanal.SelectedIndexChanged

    '        Try

    '            If lstCanal.SelectedIndex >= 0 Then
    '                PreencherListBoxSubCanal()
    '            End If

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub btnCliente_Click(sender As Object, e As System.EventArgs) Handles btnCliente.Click

    '        Try

    '            Session("ClientesConsulta") = Clientes

    '            Dim url As String = "BusquedaClientesSeleccionMultipla.aspx?acao=" & Enumeradores.eAcao.Consulta & _
    '                "&campoObrigatorio=True&SelecionaColecaoClientes=True&PersisteSelecaoClientes=True"

    '            'AbrirPopupModal
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788);", True)

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub lstGrupoCliente_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstGrupoCliente.SelectedIndexChanged

    '        Try

    '            ConfigurarEstadoControles(lstGrupoCliente)

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub btnGrabar_Click(sender As Object, e As System.EventArgs) Handles btnGrabar.Click

    '        Try
    '            'Grava o registro
    '            ExecutarGrabar()

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub btnVolver_Click(sender As Object, e As System.EventArgs) Handles btnVolver.Click
    '        Try
    '            Response.Redirect("~/Reportes.aspx", False)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try
    '    End Sub

    '    ''' <summary>
    '    ''' Evento Click btnGenerarInforme
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 07/05/2013 - Criado
    '    ''' </history>
    '    Private Sub btnGenerarInforme_Click(sender As Object, e As System.EventArgs) Handles btnGenerarInforme.Click

    '        Try

    '            'Gera o relatório
    '            ExecutarGerarInforme()

    '        Catch ex As Excepcion.NegocioExcepcion
    '            Master.ControleErro.ShowError(ex.Descricao, False)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    Private Sub btnSubCliente_Click(sender As Object, e As System.EventArgs) Handles btnSubCliente.Click

    '        Try

    '            If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then

    '                ConverteCliente()

    '                'informa os subclientes selecionados
    '                Session("objClientes") = SubClientesCompleto

    '                Dim url As String = "BusquedaSubClientesSeleccionMultipla.aspx?acao=" & Enumeradores.eAcao.Consulta & _
    '                    "&SelecionaColecaoClientes=True&RetornaCodigoCompleto=True&PersisteSelecaoSubClientes=True"

    '                'AbrirPopupModal
    '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788);", True)

    '            End If


    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento click botão limpar
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    ''' <history>
    '    ''' [anselmo.gois] 07/05/2013 - Criado
    '    ''' </history>
    '    Private Sub btnLimpiar_Click(sender As Object, e As System.EventArgs) Handles btnLimpiar.Click

    '        Try

    '            LimparControles(False)

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento chkAgrupadocliente_CheckedChanged
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub chkAgrupadocliente_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAgrupadocliente.CheckedChanged

    '        Try
    '            ConfigurarEstadoControles(chkAgrupadocliente)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento chkAgrupadoGrupoCliente_CheckedChanged
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub chkAgrupadoGrupoCliente_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAgrupadoGrupoCliente.CheckedChanged

    '        Try
    '            ConfigurarEstadoControles(chkAgrupadoGrupoCliente)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento chkAgrupadoPuntoServicio_CheckedChanged
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub chkAgrupadoPuntoServicio_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAgrupadoPuntoServicio.CheckedChanged

    '        Try
    '            ConfigurarEstadoControles(chkAgrupadoPuntoServicio)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento chkAgrupadoSubCliente_CheckedChanged
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub chkAgrupadoSubCliente_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkAgrupadoSubCliente.CheckedChanged

    '        Try
    '            ConfigurarEstadoControles(chkAgrupadoSubCliente)
    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '    ''' <summary>
    '    ''' Evento btnPuntoServicio_Click
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks></remarks>
    '    Private Sub btnPuntoServicio_Click(sender As Object, e As System.EventArgs) Handles btnPuntoServicio.Click

    '        Try


    '            If SubClientesCompleto IsNot Nothing AndAlso SubClientesCompleto.Count > 0 AndAlso
    '                (From c In SubClientesCompleto Where c.SubClientes IsNot Nothing AndAlso c.SubClientes.Count > 0).Count > 0 Then


    '                'seta os ptos de serviço já selecionados
    '                ConverteSubCliente()

    '                Session("objSubClientes") = SubClientesCompleto
    '                Session("PuntosServicioSelecionadosPersistente") = PuntosServiciosCompleto


    '                Dim url As String = "BusquedaPuntosServicioSeleccionMultipla.aspx?acao=" & Enumeradores.eAcao.Consulta & _
    '                    "&SelecionaColecaoClientes=True&RetornaCodigoCompleto=1&PersisteSelecaoPuntoServicio=True"

    '                'AbrirPopupModal
    '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788);", True)

    '            End If

    '        Catch ex As Exception
    '            Master.ControleErro.TratarErroException(ex)
    '        End Try

    '    End Sub

    '#End Region

End Class