Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion

''' <summary>
''' Página de Gerenciamento de Procesos 
''' </summary>
''' <remarks></remarks>
''' <history>[PDA] 27/01/08 - Criado</history>
Partial Public Class MantenimientoProcesos
    Inherits Base

#Region "[CONSTANTES]"

    Const TreeViewNodeEfectivo As String = "016_lbl_efectivo"

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        txtObservaciones.Attributes.Add("onKeyPress", "limitaCaracteresKeyPress('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onblur", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")
        txtObservaciones.Attributes.Add("onkeyup", "limitaCaracteres('" & txtObservaciones.ClientID & "','" & txtObservaciones.MaxLength & "');")

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        txtCliente.TabIndex = 1
        btnBuscarCliente.TabIndex = 2
        txtSubCliente.TabIndex = 3
        btnSubCliente.TabIndex = 4
        txtPuntoServicio.TabIndex = 5
        btnPuntoServicio.TabIndex = 6
        lstCanal.TabIndex = 7
        lstSubCanais.TabIndex = 8
        txtDelegacion.TabIndex = 9
        txtDescricaoProceso.TabIndex = 10
        chkVigente.TabIndex = 11
        ddlProducto.TabIndex = 12
        ddlModalidad.TabIndex = 13
        ddlIACParcial.TabIndex = 14
        ddlIACBulto.TabIndex = 15
        ddlIACRemesa.TabIndex = 16
        txtClienteFaturacion.TabIndex = 17
        btnBuscarClienteFaturacion.TabIndex = 18
        rdbPorAgrupaciones.TabIndex = 19
        rdbPorMedioPago.TabIndex = 20
        lstAgrupacionesPosibles.TabIndex = 21
        imgBtnAgrupacionesIncluirTodas.TabIndex = 22
        imgBtnAgrupacionesIncluir.TabIndex = 23
        imgBtnAgrupacionesExcluir.TabIndex = 24
        imgBtnAgrupacionesExcluirTodas.TabIndex = 25
        TrvDivisas.TabIndex = 26
        imgBtnIncluirTreeview.TabIndex = 27
        imgBtnExcluirTreeview.TabIndex = 28
        chkContarChequeTotales.TabIndex = 29
        chkContarTicketTotales.TabIndex = 30
        chkContarOtrosValoresTotales.TabIndex = 31
        txtObservaciones.TabIndex = 32
        btnTerminoMedioPago.TabIndex = 33
        btnTolerancia.TabIndex = 34
        btnGrabar.TabIndex = 35
        btnCancelar.TabIndex = 36
        btnVolver.TabIndex = 37

        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
            Master.PrimeiroControleTelaID = "btnBuscarCliente_img"
        Else
            Master.PrimeiroControleTelaID = lstCanal.ClientID
        End If



    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        If Request.QueryString("acao") Is Nothing Then
            MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Else
            MyBase.Acao = Request.QueryString("acao")
        End If
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.PROCESOS

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            If Not Page.IsPostBack Then

                'Recebe o código do Proceso
                Dim CodCliente As String = Request.QueryString("codCliente")
                Dim CodSubCliente As String = Request.QueryString("codSubCliente")
                Dim CodPuntoServicio As String = Request.QueryString("codPuntoServicio")
                Dim CodDelegacion As String = Request.QueryString("codDelegacao")
                Dim CodSubCanal As String = Request.QueryString("codSubCanal")
                Dim IdentificadorProceso As String = Request.QueryString("identificadorProceso")

                'Possíveis Ações passadas pela página BusquedaProcesos:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Duplicar

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro

                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                'Preenche Controles

                If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then
                    PreencherListBoxCanal()
                End If
                PreencherComboBoxProducto()
                PreencherComboBoxModalid()
                PreencherListAgrupaciiones()

                'Adiciona um item vazio no combo IACParcial
                ddlIACParcial.AppendDataBoundItems = True
                ddlIACParcial.Items.Clear()
                ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                'Adiciona um item vazio no combo IACBulto
                ddlIACBulto.AppendDataBoundItems = True
                ddlIACBulto.Items.Clear()
                ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                'Adiciona um item vazio no combo IACRemesa
                ddlIACRemesa.AppendDataBoundItems = True
                ddlIACRemesa.Items.Clear()
                ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                If CodCliente <> String.Empty _
                AndAlso CodDelegacion <> String.Empty _
                AndAlso CodSubCanal <> String.Empty _
                Then
                    'Estado Inicial dos control
                    CarregaDados(CodCliente, CodSubCliente, CodPuntoServicio, CodDelegacion, CodSubCanal, IdentificadorProceso)

                Else
                    txtDelegacion.Text = DelegacionConectada.Keys(0)
                End If

                'Carrega a Treeview com as posíveis divisas
                CarregaTreeViewDividasPossiveis()

            End If

            'Consome a sessão do cliente selecionado na tela de busca
            ConsomeCliente()

            'Consome a sessão do subcliente selecionado na tela de busca
            ConsomeSubCliente()

            'Consome a sessão do puntoservicio selecionado na tela de busca
            ConsomePuntoServicio()

            'Consome Tolerância
            ConsomeTolerancia()

            'Consome Termino Medio de Pago
            ConsomeTerminoMedioPago()

            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then

                ' Define o foco para o botão de buscar cliente
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('btnBuscarCliente_img').focus();", True)
            Else

                ' Define o foco para o botão de termino medio pago
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SETAFOCUS", "document.getElementById('" & lstCanal.ClientID & "').focus();", True)
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

        'Titulos
        Master.TituloPagina = Traduzir("016_titulo_mantenimiento_procesos")
        lblTituloProcesos.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos")
        lblSubTitulosInformacionDelDeclarado.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_informaciondeldeclarado")
        lblSubTitulosAgrupaciones.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_agrupaciones")
        lblSubTitulosMediosPago.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_mediospago")
        lblSubTitulosModoContaje.Text = Traduzir("016_lbl_subtitulos_mantenimento_procesos_modocontaje")

        'Controles
        lblCliente.Text = Traduzir("016_lbl_cliente")
        lblSubCliente.Text = Traduzir("016_lbl_subcliente")
        lblPuntoServicio.Text = Traduzir("016_lbl_puntoservicio")
        lblCanal.Text = Traduzir("016_lbl_canal")
        lblSubCanal.Text = Traduzir("016_lbl_subcanal")
        lblDelegacion.Text = Traduzir("016_lbl_delegacion")
        lblDescricaoProceso.Text = Traduzir("016_lbl_descripcion_proceso")
        lblVigente.Text = Traduzir("016_lbl_vigente")
        lblProducto.Text = Traduzir("016_lbl_producto")
        lblModalidad.Text = Traduzir("016_lbl_modalidad")
        lblIACParcial.Text = Traduzir("016_lbl_IACParcial")
        lblIACBulto.Text = Traduzir("016_lbl_IACBulto")
        lblIACRemesa.Text = Traduzir("016_lbl_IACRemesa")
        lblClienteFaturacion.Text = Traduzir("016_lbl_cliente_faturacion")
        lblObservaciones.Text = Traduzir("016_lbl_observaciones")
        lblNota.Text = Traduzir("016_lbl_nota")

        'RadioButton
        rdbPorMedioPago.Text = Traduzir("016_lbl_infodeclarado_pormediopago")
        rdbPorAgrupaciones.Text = Traduzir("016_lbl_infodeclarado_poragrupaciones")

        'CheckBox
        chkContarChequeTotales.Text = Traduzir("016_lbl_contarchequetotales")
        chkContarTicketTotales.Text = Traduzir("016_lbl_contartickettotales")
        chkContarOtrosValoresTotales.Text = Traduzir("016_lbl_contarotrosvalorestotales")

        'Validators
        csvClienteObrigatorio.ErrorMessage = Traduzir("016_msg_procesoclientenobligatorio")
        csvDescricaoObrigatorio.ErrorMessage = Traduzir("016_msg_procesodescripcionobligatorio")
        csvModalidadObrigatorio.ErrorMessage = Traduzir("016_msg_modalidadobligatorio")
        csvProductoObrigatorio.ErrorMessage = Traduzir("016_msg_productoobligatorio")
        csvTrvProcesos.ErrorMessage = Traduzir("016_msg_treeviewnodesobligatorio")
        csvSubCanalObrigatorio.ErrorMessage = Traduzir("016_msg_subcanaisobligatorio")
        csvAgrupacionesCompoenProcesoObrigatorio.ErrorMessage = Traduzir("016_msg_Agrupacionescompoenprocesoobligatorioobligatorio")
        csvInformacioDeclaradoObrigatorio.ErrorMessage = Traduzir("016_msg_informaciondeldeclaradoobligatorio")


    End Sub

#End Region

#Region "[EVENTOS]"

#Region "[EVENTOS BOTÕES]"

    ''' <summary>
    ''' Clique do botão volver
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnVolver_Click(sender As Object, e As EventArgs) Handles btnVolver.Click

        Response.Redirect("~/BusquedaProcesos.aspx", False)

    End Sub

    ''' <summary>
    ''' Clique do botão cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancelar_Click(sender As Object, e As System.EventArgs) Handles btnCancelar.Click

        Response.Redirect("~/BusquedaProcesos.aspx", False)

    End Sub

    ''' <summary>
    ''' Clique do botão gravar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click

        Try

            Dim objProxyProceso As New Comunicacion.ProxyProceso
            Dim objPeticionProceso As New IAC.ContractoServicio.Proceso.SetProceso.Peticion
            Dim objRespuestaProceso As IAC.ContractoServicio.Proceso.SetProceso.Respuesta = Nothing

            Dim objColSubCanalSet As ContractoServicio.Proceso.SetProceso.SubCanalColeccion
            'retorna a seleção de subcanais selecionados
            objColSubCanalSet = RetornaSubCanalSet()

            Dim strErro As New Text.StringBuilder(String.Empty)

            ValidarCamposObrigatorios = True

            If MontaMensagensErro(objColSubCanalSet, True).Length > 0 Then
                Exit Sub
            End If

            Dim objProceso As New IAC.ContractoServicio.Proceso.SetProceso.Proceso

            'Monta o cliente set

            '### Processo ###
            objProceso.Descripcion = txtDescricaoProceso.Text.Trim
            objProceso.Observacion = txtObservaciones.Text
            objProceso.CodigoDelegacion = txtDelegacion.Text.Trim

            'Recebe os valores do formulário
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                objProceso.Vigente = True
            Else
                objProceso.Vigente = chkVigente.Checked
            End If


            '### Cliente ###
            objProceso.Cliente = RetornaClienteSet()

            '### SubCanal ###
            objProceso.SubCanal = objColSubCanalSet

            '### Tipo Processado, IAC, Producto ###
            objProceso.CodigoTipoProcesado = ddlModalidad.SelectedValue
            objProceso.CodigoIac = ddlIACParcial.SelectedValue
            objProceso.CodigoIACBulto = ddlIACBulto.SelectedValue
            objProceso.CodigoIACRemesa = ddlIACRemesa.SelectedValue
            objProceso.CodigoProducto = ddlProducto.SelectedValue

            If ClienteFaturacionSelecionado IsNot Nothing Then
                objProceso.CodigoClienteFacturacion = ClienteFaturacionSelecionado.Codigo
            Else
                objProceso.CodigoClienteFacturacion = String.Empty
            End If

            '### Informacion Del Declarado ###
            If rdbPorAgrupaciones.Checked Then
                objProceso.IndicadorMediosPago = 0
            Else
                objProceso.IndicadorMediosPago = 1
            End If

            '### Modo de Contaje ###
            objProceso.ContarChequesTotal = chkContarChequeTotales.Checked
            objProceso.ContarTicketsTotal = chkContarTicketTotales.Checked
            objProceso.ContarOtrosTotal = chkContarOtrosValoresTotales.Checked
            objProceso.ContarTarjetasTotal = If(String.IsNullOrEmpty(chkContarTarjetaTotales.Value) OrElse chkContarTarjetaTotales.Value.Trim().Length = 0, False, Convert.ToBoolean(chkContarTarjetaTotales.Value))

            '### Divisa - Efetivo ###


            If rdbPorMedioPago.Checked Then
                'Obter os dados da Treeview de Medios de pago que o processo

                Dim objDivisaCol As ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion = Nothing
                Dim orden As Integer = 0

                'Divisas
                For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

                    'Tipo de Medio de Pago
                    For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                        'Verifica se o tipo de médio de pago é efetivo
                        If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                            Dim objDivisaEfectivo As New ContractoServicio.Proceso.SetProceso.DivisaProceso
                            objDivisaEfectivo.Codigo = objTreeNodeTipoMedioPago.Value
                            objDivisaEfectivo.Orden = orden

                            'Atribui os valores de tolerância
                            AtribuiValoresToleranciaDivisaEfetivo(objDivisaEfectivo)

                            'Cria a coleçao de divisas
                            If objProceso.DivisaProceso Is Nothing Then
                                objProceso.DivisaProceso = New ContractoServicio.Proceso.SetProceso.DivisaProcesoColeccion
                            End If

                            objProceso.DivisaProceso.Add(objDivisaEfectivo)
                            orden += 1

                        Else

                            'Medio de Pago
                            For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                                Dim objMedioPago As New ContractoServicio.Proceso.SetProceso.MedioPagoProceso
                                objMedioPago.CodigoIsoDivisa = objTreeNodeDivisas.Value
                                objMedioPago.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                                objMedioPago.Codigo = objTreeNodeMedioPago.Value
                                objMedioPago.Descripcion = objTreeNodeMedioPago.Text

                                'Atribui os valores de tolerância
                                AtribuiValoresToleranciaMedioPago(objMedioPago)

                                'Atribui os valores de términos de medio de pago 
                                AtribuiValoresTerminoMedioPago(objMedioPago)

                                'Cria a coleção de medios de pago
                                If objProceso.MediosPagoProceso Is Nothing Then
                                    objProceso.MediosPagoProceso = New ContractoServicio.Proceso.SetProceso.MedioPagoProcesoColeccion
                                End If
                                objProceso.MediosPagoProceso.Add(objMedioPago)
                            Next

                        End If

                    Next
                Next

            Else
                'Por agrupação

                '### Agrupaciones ###

                If lstAgrupacionesCompoenProceso.Items.Count > 0 Then
                    Dim objAgrupacion As ContractoServicio.Proceso.SetProceso.AgrupacionProceso
                    For Each agrupacao As ListItem In lstAgrupacionesCompoenProceso.Items
                        'Cria a agrupação
                        objAgrupacion = New ContractoServicio.Proceso.SetProceso.AgrupacionProceso
                        objAgrupacion.Codigo = agrupacao.Value

                        'Atribui os valores da agrupação
                        AtribuiValoresToleranciaAgrupacao(objAgrupacion)

                        'Cria a coleção de agrupações
                        If objProceso.AgrupacionesProceso Is Nothing Then
                            objProceso.AgrupacionesProceso = New ContractoServicio.Proceso.SetProceso.AgrupacionProcesoColeccion
                        End If
                        objProceso.AgrupacionesProceso.Add(objAgrupacion)
                    Next
                End If

            End If



            'Passa a coleção para a agrupação
            objPeticionProceso.Proceso = objProceso
            objPeticionProceso.CodigoUsuario = MyBase.LoginUsuario

            'Obtem o objeto de resposta para validação
            objRespuestaProceso = objProxyProceso.SetProceso(objPeticionProceso)

            If Master.ControleErro.VerificaErro(objRespuestaProceso.CodigoError, objRespuestaProceso.NombreServidorBD, objRespuestaProceso.MensajeError) Then

                Response.Redirect("~/BusquedaProcesos.aspx", False)

            Else
                If objRespuestaProceso.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    Master.ControleErro.ShowError(objRespuestaProceso.MensajeError, False)
                End If
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão Incluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgBtnIncluirTreeview_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnIncluirTreeview.Click

        Try
            If TrvDivisas.SelectedNode IsNot Nothing Then
                InsereNaArvoreDinamica(TrvProcesos.Nodes, MontaArvoreSelecionada(TrvDivisas.SelectedNode))
            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão excluir(p/ Treeview)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub imgBtnExcluirTreeview_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnExcluirTreeview.Click

        Try
            If TrvProcesos.SelectedNode IsNot Nothing Then
                RemoveNode(TrvProcesos)
            End If
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Clique do botão buscar cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBuscarCliente_Click(sender As Object, e As System.EventArgs) Handles btnBuscarCliente.Click
        Try
            'Guarda quem irá consumir o objeto cliente no retorno
            ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarCliente

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique do botão buscar cliente faturação
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBuscarClienteFaturacion_Click(sender As Object, e As System.EventArgs) Handles btnBuscarClienteFaturacion.Click
        Try
            'Guarda quem irá consumir o objeto cliente no retorno
            ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarClienteFaturacion

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=False&vigente=True"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarClienteFaturacion');", True)

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique do botão buscar Ponto Serviço
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPuntoServicio_Click(sender As Object, e As System.EventArgs) Handles btnPuntoServicio.Click
        Try

            If ClienteSelecionado IsNot Nothing _
                AndAlso SubClientesSelecionados IsNot Nothing _
                AndAlso SubClientesSelecionados.Count > 0 Then


                'Seta o cliente selecionado para a PopUp
                SetClienteSelecionadoPopUp()

                'Seta os subcliente selecionados para a PopUp
                SetSubClientesSelecionadoPopUp()

                Dim url As String = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&RetornaCodigoCompleto=1&vigente=True"

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnPuntoServicio');", True)

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique do botão buscar Sub Cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSubCliente_Click(sender As Object, e As System.EventArgs) Handles btnSubCliente.Click
        Try

            If ClienteSelecionado IsNot Nothing Then

                'Seta o cliente selecionado para a PopUp
                SetClienteSelecionadoPopUp()

                Dim url As String = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&vigente=True"

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnSubCliente');", True)

            End If

        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Clique do botão que permite incluir Agrupações Posíveis em Agrupações que compõe processo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesIncluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesIncluir.Click

        If lstAgrupacionesPosibles.Items.Count > 0 Then
            If lstAgrupacionesPosibles.SelectedItem IsNot Nothing _
            AndAlso lstAgrupacionesPosibles.SelectedItem.Value <> String.Empty Then
                Dim objListItem As ListItem
                objListItem = lstAgrupacionesPosibles.SelectedItem

                lstAgrupacionesPosibles.Items.Remove(lstAgrupacionesPosibles.SelectedItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)

            End If
        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite excluir Agrupações que compõe processo e incluir em Agrupações Posíveis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesExcluir_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesExcluir.Click

        If lstAgrupacionesCompoenProceso.Items.Count > 0 Then
            If lstAgrupacionesCompoenProceso.SelectedItem IsNot Nothing _
            AndAlso lstAgrupacionesCompoenProceso.SelectedItem.Value <> String.Empty Then
                Dim objListItem As ListItem
                objListItem = lstAgrupacionesCompoenProceso.SelectedItem

                lstAgrupacionesCompoenProceso.Items.Remove(lstAgrupacionesCompoenProceso.SelectedItem)
                lstAgrupacionesPosibles.Items.Add(objListItem)

            End If
        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite incluir todas Agrupações Posíveis em Agrupações que compõe processo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesIncluirTodas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesIncluirTodas.Click

        If lstAgrupacionesPosibles.Items.Count > 0 Then

            Dim objListItem As ListItem
            While lstAgrupacionesPosibles.Items.Count > 0
                objListItem = lstAgrupacionesPosibles.Items(0)
                lstAgrupacionesPosibles.Items.Remove(objListItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)
            End While

        End If

    End Sub

    ''' <summary>
    ''' Clique do botão que permite excluir todas Agrupações que compõe processo e incluir em Agrupações Posíveis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub imgBtnAgrupacionesExcluirTodas_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgBtnAgrupacionesExcluirTodas.Click

        If lstAgrupacionesCompoenProceso.Items.Count > 0 Then

            Dim objListItem As ListItem
            While lstAgrupacionesCompoenProceso.Items.Count > 0
                objListItem = lstAgrupacionesCompoenProceso.Items(0)
                lstAgrupacionesCompoenProceso.Items.Remove(objListItem)
                lstAgrupacionesPosibles.Items.Add(objListItem)
            End While

        End If

    End Sub

    ''' <summary>
    ''' Evento clique botão Tolerância
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTolerancia_Click(sender As Object, e As System.EventArgs) Handles btnTolerancia.Click

        Dim url As String = String.Empty

        If rdbPorAgrupaciones.Checked Then

            ' atualizar tolerancias
            AtualizaToleranciasAgrupaciones()

            ' atualizar sessão
            SessionToleranciaAgrupacion = ToleranciaAgrupaciones

            ' setar url
            url = "MantenimientoTolerancias.aspx?acao=" & MyBase.Acao & "&tipodeclarado=2"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnTolerancia');", True)

        ElseIf rdbPorMedioPago.Checked Then

            ' atualizar tolerancias
            AtualizaToleranciasMedioPago()

            ' atualizar sessão
            SessionToleranciaMedioPago = ToleranciaMedioPagos

            ' setar url
            url = "MantenimientoTolerancias.aspx?acao=" & MyBase.Acao & "&tipodeclarado=1"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnTolerancia');", True)

        End If

    End Sub

    ''' <summary>
    ''' Clque do botão terminos de medio de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnTerminoMedioPago_Click(sender As Object, e As System.EventArgs) Handles btnTerminoMedioPago.Click

        Dim url As String = String.Empty

        AtualizaTerminosMedioPago()
        'Envia para popup objeto atualizado

        SessionTerminoMedioPago = TerminosMedioPagos

        ' setar url
        url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & MyBase.Acao

        'AbrirPopupModal
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnTerminoMedioPago');", True)

    End Sub


#End Region

#Region "[EVENTOS LISTBOX]"

    Private Sub lstCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstCanal.SelectedIndexChanged
        PreencherListBoxSubCanal()
    End Sub

#End Region

#Region "[EVENTOS COMBOBOX]"

    ''' <summary>
    ''' Evento de mudança de valor do campo modalidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ddlModalidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged

        ddlModalidad.ToolTip = ddlModalidad.SelectedItem.Text
        PreencherComboBoxInformacionAdicional()
        ddlModalidad.Focus()

    End Sub

    Private Sub ddlIACParcial_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACParcial.SelectedIndexChanged
        ddlIACParcial.ToolTip = ddlIACParcial.SelectedItem.Text
        ddlIACParcial.Focus()
    End Sub

    Private Sub ddlIACRemesa_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACRemesa.SelectedIndexChanged
        ddlIACRemesa.ToolTip = ddlIACRemesa.SelectedItem.Text
        ddlIACRemesa.Focus()
    End Sub

    Private Sub ddlIACBulto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlIACBulto.SelectedIndexChanged
        ddlIACBulto.ToolTip = ddlIACBulto.SelectedItem.Text
        ddlIACBulto.Focus()
    End Sub

    Private Sub ddlProducto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProducto.SelectedIndexChanged
        ddlProducto.ToolTip = ddlProducto.SelectedItem.Text
        ddlProducto.Focus()
    End Sub

#End Region

#Region "[EVENTOS RADIO]"
    Private Sub rdbPorAgrupaciones_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbPorAgrupaciones.CheckedChanged
        If rdbPorMedioPago.Checked Then
            pnlMediosPago.Visible = True
            pnlAgrupacao.Visible = False
            ValidarMedioPago = False
        Else
            pnlMediosPago.Visible = False
            pnlAgrupacao.Visible = True
            ValidarAgrupacion = False
        End If
    End Sub

    Private Sub rdbPorMedioPago_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdbPorMedioPago.CheckedChanged
        If rdbPorMedioPago.Checked Then
            pnlMediosPago.Visible = True
            pnlAgrupacao.Visible = False
            ValidarMedioPago = False
        Else
            pnlMediosPago.Visible = False
            pnlAgrupacao.Visible = True
            ValidarAgrupacion = False
        End If
    End Sub
#End Region

#End Region

#Region "[MÉTODOS]"

#Region "[CONSOME OBJETOS]"

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

                'Verifica quem irá consumir o objeto cliente
                If ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarCliente Then

                    'Armazena o cliente selecionado
                    ClienteSelecionado = objCliente

                    ' setar controles da tela
                    txtCliente.Text = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion
                    txtCliente.ToolTip = ClienteSelecionado.Codigo & " - " & ClienteSelecionado.Descripcion

                    'Limpa os demais campos
                    txtSubCliente.Text = String.Empty
                    txtPuntoServicio.Text = String.Empty

                    SubClientesSelecionados = Nothing
                    PuntoServiciosSelecionados = Nothing

                Else
                    'Armazena o cliente faturacion selecionado
                    ClienteFaturacionSelecionado = objCliente
                    txtClienteFaturacion.Text = ClienteFaturacionSelecionado.Codigo & " - " & ClienteFaturacionSelecionado.Descripcion
                    txtClienteFaturacion.ToolTip = ClienteFaturacionSelecionado.Codigo & " - " & ClienteFaturacionSelecionado.Descripcion

                End If

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
                txtSubCliente.Text = objSubClientes(0).Codigo & " - " & objSubClientes(0).Descripcion & " ..."

                txtSubCliente.ToolTip = String.Empty

                For Each subClientes As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In objSubClientes
                    txtSubCliente.ToolTip &= subClientes.Codigo & " - " & subClientes.Descripcion & ";"
                Next

                If Not String.IsNullOrEmpty(txtSubCliente.ToolTip) Then

                    txtSubCliente.ToolTip = txtSubCliente.ToolTip.Remove(txtSubCliente.ToolTip.Length - 1, 1)

                End If

                'Limpa os demais campos
                txtPuntoServicio.Text = String.Empty
                txtPuntoServicio.ToolTip = String.Empty

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

            txtPuntoServicio.Text = String.Empty
            txtPuntoServicio.ToolTip = String.Empty

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

        If Session("PtosServicoCompleto") IsNot Nothing Then

            Dim ObjPuntoServicio As New List(Of Negocio.Cliente)
            ObjPuntoServicio = TryCast(Session("PtosServicoCompleto"), List(Of Negocio.Cliente))

            If ObjPuntoServicio IsNot Nothing Then

                PuntoServiciosSelecionados = ObjPuntoServicio
                ' setar controles da tela

                txtPuntoServicio.Text = PuntoServiciosSelecionados(0).Subclientes(0).PtosServicio(0).CodigoPuntoServicio & " - " & PuntoServiciosSelecionados(0).Subclientes(0).PtosServicio(0).DesPuntoServicio & " ..."

                txtPuntoServicio.ToolTip = String.Empty

                For Each subClientes As Negocio.Subcliente In ObjPuntoServicio(0).Subclientes
                    For Each puntoServicio As Negocio.PuntoServicio In subClientes.PtosServicio
                        txtPuntoServicio.ToolTip &= puntoServicio.CodigoPuntoServicio & " - " & puntoServicio.DesPuntoServicio & ";"
                    Next
                Next

                If Not String.IsNullOrEmpty(txtPuntoServicio.ToolTip) Then

                    txtPuntoServicio.ToolTip = txtPuntoServicio.ToolTip.Remove(txtPuntoServicio.ToolTip.Length - 1, 1)

                End If

            End If

            Session("PtosServicoCompleto") = Nothing

        End If

        'verifica a sessão de ponto de serviço é pra ser limpa        
        If Session("LimparPuntoServicioSelecionado") IsNot Nothing Then
            PuntoServiciosSelecionados = Nothing
            'Limpa os demais campos            
            txtPuntoServicio.Text = String.Empty
            txtPuntoServicio.ToolTip = String.Empty

            'retira da sessão
            Session("LimparPuntoServicioSelecionado") = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de mantenimiento de tolerancia.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 18/02/2009 Criado
    ''' </history>
    Private Sub ConsomeTolerancia()

        ' caso a sessão esteja preenchida
        If SessionToleranciaAgrupacion IsNot Nothing Then
            ' atualizar viewstate
            ToleranciaAgrupaciones = SessionToleranciaAgrupacion
            ' limpar sessão
            SessionToleranciaAgrupacion = Nothing
        End If

        ' caso a sessão esteja preenchida
        If SessionToleranciaMedioPago IsNot Nothing Then
            ' atualizar viewstate
            ToleranciaMedioPagos = SessionToleranciaMedioPago
            ' limpar sessão
            SessionToleranciaMedioPago = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Consome a sessão da tela de manutenção de terminos de medios de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsomeTerminoMedioPago()

        ' caso a sessão esteja preenchida
        If SessionTerminoMedioPago IsNot Nothing Then
            ' atualizar viewstate
            TerminosMedioPagos = SessionTerminoMedioPago
            ' limpar sessão
            SessionTerminoMedioPago = Nothing
        End If

    End Sub

#End Region

#Region "[CARREGA DADOS]"

    ''' <summary>
    ''' Carrega os dados do gridView quando a página é carregada pela primeira vez. Modo de Modificação
    ''' </summary>
    ''' <param name="CodCliente"></param>
    ''' <param name="CodSubCliente"></param>
    ''' <param name="CodPuntoServicio"></param>
    ''' <param name="CodDelegacion"></param>
    ''' <param name="CodSubCanal"></param>
    ''' <remarks></remarks>
    Public Sub CarregaDados(CodCliente As String, CodSubCliente As String, CodPuntoServicio As String, CodDelegacion As String, CodSubCanal As String, IdentificadorProceso As String)

        Dim objColProceso As IAC.ContractoServicio.Proceso.GetProcesoDetail.ProcesoColeccion
        objColProceso = getProceso(CodCliente, CodSubCliente, CodPuntoServicio, CodDelegacion, CodSubCanal, IdentificadorProceso)

        If objColProceso IsNot Nothing AndAlso objColProceso.Count > 0 Then

            'Preenche os controles do formulario

            '### Conttroles Processo ###

            If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
            Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                'Cliente
                txtCliente.Text = objColProceso(0).CodigoCliente & " - " & objColProceso(0).DescripcionCliente
                txtCliente.ToolTip = objColProceso(0).CodigoCliente & " - " & objColProceso(0).DescripcionCliente

                Dim objCliente As New ContractoServicio.Utilidad.GetComboClientes.Cliente
                objCliente.Codigo = objColProceso(0).CodigoCliente
                objCliente.Descripcion = objColProceso(0).DescripcionCliente
                ClienteSelecionado = objCliente

                'SubCliente
                If objColProceso(0).CodigoSubcliente IsNot Nothing AndAlso objColProceso(0).CodigoSubcliente <> String.Empty Then

                    'Formata texto do campo subcliente
                    txtSubCliente.Text = objColProceso(0).CodigoSubcliente & " - " & objColProceso(0).DescripcionSubcliente
                    txtSubCliente.ToolTip = objColProceso(0).CodigoSubcliente & " - " & objColProceso(0).DescripcionSubcliente

                    'Cria o subcliente e adiciona na coleção
                    Dim objSubCliente As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                    objSubCliente.Codigo = objColProceso(0).CodigoSubcliente
                    objSubCliente.Descripcion = objColProceso(0).DescripcionSubcliente
                    If SubClientesSelecionados Is Nothing Then
                        SubClientesSelecionados = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
                    End If
                    SubClientesSelecionados.Add(objSubCliente)

                Else
                    txtSubCliente.Text = String.Empty
                End If

                'Punto de Servicio
                If objColProceso(0).CodigoPuntoServicio <> String.Empty Then

                    'Formata o campo ponto de serviço
                    txtPuntoServicio.Text = objColProceso(0).CodigoPuntoServicio & " - " & objColProceso(0).DescripcionPuntoServicio
                    txtPuntoServicio.ToolTip = objColProceso(0).CodigoPuntoServicio & " - " & objColProceso(0).DescripcionPuntoServicio

                    If PuntoServiciosSelecionados Is Nothing Then
                        PuntoServiciosSelecionados = New List(Of Negocio.Cliente)
                    End If

                    Dim objClientePTS As New Negocio.Cliente
                    Dim objSubcliPTS As Negocio.Subcliente
                    Dim objPtoServicioPTS As Negocio.PuntoServicio

                    With objClientePTS
                        .CodigoCliente = objColProceso(0).CodigoCliente
                        .DesCliente = objColProceso(0).DescripcionCliente
                    End With

                    objSubcliPTS = New Negocio.Subcliente(objColProceso(0).CodigoSubcliente, objColProceso(0).DescripcionSubcliente)
                    objPtoServicioPTS = New Negocio.PuntoServicio(String.Empty, objColProceso(0).CodigoPuntoServicio, objColProceso(0).DescripcionPuntoServicio)

                    objClientePTS.Subclientes.Add(objSubcliPTS)
                    objSubcliPTS.PtosServicio.Add(objPtoServicioPTS)

                    PuntoServiciosSelecionados.Add(objClientePTS)

                Else
                    txtPuntoServicio.Text = String.Empty
                End If

            End If

            Dim itemSelecionado As ListItem
            'Se a ação for consulta, exibe apenas o canal selecionado
            If Acao <> Aplicacao.Util.Utilidad.eAcao.Consulta Then

                'Se a ação for diferente de duplicar
                If Acao <> Aplicacao.Util.Utilidad.eAcao.Duplicar Then

                    'Seleciona o valor do canal            
                    itemSelecionado = lstCanal.Items.FindByValue(objColProceso(0).CodigoCanal)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True

                        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                            Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                            'Preenche os subcanais do canal selecionado
                            PreencherListBoxSubCanal()
                        End If
                    End If

                    'Seleciona o subcanal
                    itemSelecionado = lstSubCanais.Items.FindByValue(objColProceso(0).CodigoSubcanal)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True
                    End If

                End If

            Else
                'Adiciona o Canal
                itemSelecionado = New ListItem(objColProceso(0).DescripcionCanal, objColProceso(0).CodigoCanal)
                lstCanal.Items.Add(itemSelecionado)

                'Adiciona o Subcanal
                itemSelecionado = New ListItem(objColProceso(0).DescripcionSubcanal, objColProceso(0).CodigoSubcanal)
                lstSubCanais.Items.Add(itemSelecionado)
            End If

            'Seleciona o Produto            
            itemSelecionado = ddlProducto.Items.FindByValue(objColProceso(0).CodigoProducto)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona a Modalidade          
            itemSelecionado = ddlModalidad.Items.FindByValue(objColProceso(0).CodigoTipoProcesado)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True

                'Preenche o Combobox de informação adicional de acordo com a modalidade escolhida
                PreencherComboBoxInformacionAdicional()
            End If

            'Seleciona o valor da IAC Parcial
            itemSelecionado = ddlIACParcial.Items.FindByValue(objColProceso(0).CodigoIac)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona o valor da IAC Bulto
            itemSelecionado = ddlIACBulto.Items.FindByValue(objColProceso(0).CodigoIACBulto)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seleciona o valor da IAC Remesa
            itemSelecionado = ddlIACRemesa.Items.FindByValue(objColProceso(0).CodigoIACRemesa)
            If itemSelecionado IsNot Nothing Then
                itemSelecionado.Selected = True
            End If

            'Seta a delegação corrente em caso de duplicar o registro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Duplicar Then
                txtDelegacion.Text = DelegacionConectada.Keys(0)
                txtDelegacion.ToolTip = DelegacionConectada.Keys(0)
            Else
                txtDelegacion.Text = objColProceso(0).CodigoDelegacion
                txtDelegacion.ToolTip = objColProceso(0).CodigoDelegacion
            End If

            txtDescricaoProceso.Text = objColProceso(0).Descripcion
            txtDescricaoProceso.ToolTip = objColProceso(0).Descripcion
            txtObservaciones.Text = objColProceso(0).Observacion
            chkVigente.Checked = objColProceso(0).Vigente
            EsVigente = chkVigente.Checked


            If objColProceso(0).CodigoClienteFacturacion <> String.Empty AndAlso _
            objColProceso(0).CodigoClienteFacturacion IsNot Nothing Then
                txtClienteFaturacion.Text = objColProceso(0).CodigoClienteFacturacion & " - " & objColProceso(0).DescripcionClienteFacturacion
                txtClienteFaturacion.ToolTip = objColProceso(0).CodigoClienteFacturacion & " - " & objColProceso(0).DescripcionClienteFacturacion
            Else
                txtClienteFaturacion.Text = String.Empty
            End If

            Dim objClienteFaturacion As New ContractoServicio.Utilidad.GetComboClientes.Cliente
            objClienteFaturacion.Codigo = objColProceso(0).CodigoClienteFacturacion
            objClienteFaturacion.Descripcion = objColProceso(0).DescripcionClienteFacturacion
            ClienteFaturacionSelecionado = objClienteFaturacion


            ' preenche a propriedade da tela
            EsVigente = objColProceso(0).Vigente

            '### Controles Informacion Del Declarado ###

            If objColProceso(0).IndicadorMediosPago Then
                rdbPorMedioPago.Checked = True
                'Habilita/Desabilita o panel
                pnlAgrupacao.Visible = False
                pnlMediosPago.Visible = True
            Else
                rdbPorAgrupaciones.Checked = True
                'Habilita/Desabilita o panel
                pnlAgrupacao.Visible = True
                pnlMediosPago.Visible = False
            End If

            '### Controles Agrupaciones ###

            Dim objListItem As ListItem = Nothing
            Dim objToleranciaAgrupacion As PantallaProceso.ToleranciaAgrupacion = Nothing

            For Each objAgrupacion As ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso In objColProceso(0).AgrupacionesProceso
                objListItem = New ListItem(objAgrupacion.Descripcion, objAgrupacion.Codigo)
                lstAgrupacionesPosibles.Items.Remove(objListItem)
                lstAgrupacionesCompoenProceso.Items.Add(objListItem)

                '######## Cria a estrutura de tolerancias por agrupação ########

                objToleranciaAgrupacion = CriarToleranciaAgrupacion(objAgrupacion)

                '################################################################

                'Adiciona a tolerancia nas tolerância de agrupação
                If ToleranciaAgrupaciones Is Nothing Then
                    ToleranciaAgrupaciones = New PantallaProceso.ToleranciaAgrupacionColeccion
                End If
                ToleranciaAgrupaciones.Add(objToleranciaAgrupacion)

            Next

            '### Controles Medios de Pago ###

            If objColProceso(0).DivisasProceso IsNot Nothing AndAlso objColProceso(0).DivisasProceso.Count > 0 OrElse _
               objColProceso(0).MediosDePagoProceso IsNot Nothing AndAlso objColProceso(0).MediosDePagoProceso.Count > 0 Then

                'Carrega a Treeview de Médios de Pago que compoem um processo
                CarregaTreeview(TrvProcesos, objColProceso(0))

            End If

            '### Conttroles Modo de Contaje ###

            chkContarChequeTotales.Checked = objColProceso(0).ContarChequesTotal
            chkContarTicketTotales.Checked = objColProceso(0).ContarTicketsTotal
            chkContarOtrosValoresTotales.Checked = objColProceso(0).ContarOtrosTotal
            chkContarTarjetaTotales.Value = objColProceso(0).ContarTajetasTotal

        End If

    End Sub

#End Region

#Region "[TREEVIEW]"

    ''' <summary>
    ''' Método que carrega a Treeview com uma coleção de Divisas (getDetail)
    ''' </summary>
    ''' <param name="pobjTreeView"></param>
    ''' <param name="pObjProcesso"></param>
    ''' <remarks></remarks>
    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjProcesso As IAC.ContractoServicio.Proceso.GetProcesoDetail.Proceso)

        'Formata os dados para serem utilizados na carga da treeview
        Dim objColResultado = FormataDadosTreeView(pObjProcesso)

        'Coleção de Tolerância e Terminos a serem carregadas
        Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = Nothing
        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        'Limpa a Treeview
        pobjTreeView.Nodes.Clear()

        If objColResultado IsNot Nothing Then
            'Cria a coleção de tolerâncias de têrmino
            ToleranciaMedioPagos = New PantallaProceso.ToleranciaMedioPagoColeccion
            'Cria a coleção de terminos de medio de pago
            TerminosMedioPagos = New PantallaProceso.MedioPagoColeccion

            Dim objDivisa As ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso
            For Each objResultado In objColResultado

                'Retorna a Divisa
                If objResultado.Efectivo Then
                    objDivisa = retornaDivisaEfetivo(objResultado.codIso, pObjProcesso.DivisasProceso)
                Else
                    Dim objDivisaMedioPago As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa = Nothing
                    objDivisaMedioPago = retornaDivisaMedioPago(objResultado.codIso, getDivisasPosiveis)

                    If objDivisaMedioPago IsNot Nothing Then
                        objDivisa = New ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso
                        objDivisa.Codigo = objDivisaMedioPago.CodigoIso
                        objDivisa.Descripcion = objDivisaMedioPago.Descripcion
                    Else
                        Continue For
                    End If
                End If



                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.Codigo)
                Dim objTreeNodeTipoMedioPago As TreeNode = Nothing
                Dim objTreeNodeMedioPago As TreeNode = Nothing

                'Verifica se a divisa tem efetivo
                If objResultado.Efectivo Then
                    'Adiciona o nó efetivo
                    objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.Codigo)


                    '######### Cria a tolerânica da divisa-efetiva #########                    
                    objMedioPagoTolerancia = CriarToleranciaDivisaEfetivo(objDivisa.Codigo, objDivisa.Descripcion, _
                                                         objDivisa.Codigo, Traduzir(TreeViewNodeEfectivo), _
                                                          pObjProcesso.DivisasProceso)

                    ToleranciaMedioPagos.Add(objMedioPagoTolerancia)
                    '############################################################

                    objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)

                End If

                'Verifica se a divisa tem médio pago
                If objResultado.MedioPago Then
                    'Retorna os tipos de médio de pago
                    Dim dictonaryTipoMedioPago As Dictionary(Of String, String) = retornaTipoMedioPago(objResultado.codIso, pObjProcesso.MediosDePagoProceso)

                    'Tipos de Médio de Pago
                    For Each TipoMedioPago As KeyValuePair(Of String, String) In dictonaryTipoMedioPago
                        'Adiciona Nós de Tipo Médio Pago
                        objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Value, TipoMedioPago.Key)

                        Dim objColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion = retornaMedioPago(objDivisa.Codigo, TipoMedioPago.Key, pObjProcesso.MediosDePagoProceso)


                        For Each MedioPago As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In objColMedioPago
                            'Adiciona Nós de Médio Pago
                            objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)

                            'Inseri Ordenado
                            'objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)

                            'Não inseri ordenado                            
                            objTreeNodeTipoMedioPago.ChildNodes.Add(objTreeNodeMedioPago)


                            '######### Cria a tolerânica desta divisa-mediopago #########
                            objMedioPagoTolerancia = CriarToleranciaDivisaMedioPago(objDivisa.Codigo, objDivisa.Descripcion, _
                                                         TipoMedioPago.Key, TipoMedioPago.Value, _
                                                          MedioPago.Codigo, MedioPago.Descripcion, pObjProcesso.MediosDePagoProceso)
                            ToleranciaMedioPagos.Add(objMedioPagoTolerancia)
                            '############################################################


                            '######## Cria Terminos do medio de Pago ################

                            objMedioPagoTerminos = CriarMedioPagoTerminos(objDivisa.Codigo, TipoMedioPago.Key, MedioPago.Codigo, MedioPago)

                            If objMedioPagoTerminos IsNot Nothing Then
                                'Se não existir o objeto então o cria                                
                                TerminosMedioPagos.Add(objMedioPagoTerminos)
                            End If

                            '########################################################

                        Next

                        objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)

                    Next

                End If

                'Adiciona a divisa na Tree

                'Por regra expande todos os nós(até 3 nível)
                objTreeNodeDivisa.ExpandAll()

                'Inseri Ordenado
                'pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

                'Não inseri ordenado                
                pobjTreeView.Nodes.Add(objTreeNodeDivisa)

            Next
        End If

    End Sub

    ''' <summary>
    ''' FORMATA OS DADOS PARA PREENCHER A TREEVIEW
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FormataDadosTreeView(pObjProcesso As ContractoServicio.Proceso.GetProcesoDetail.Proceso) As ResultadoCol

        'Monta um objeto auxiliar de retorno, informando
        'se a divisa tem efetivo e médio de pago 
        'para poder ser exibida na treeview em modo modificação.
        'Para entender melhor a "query linq" abaixo que representa a situação acima,
        'favor ver o correspondente no region "Query Linq"

        Dim retorno As Object = Nothing
        If pObjProcesso.DivisasProceso IsNot Nothing AndAlso pObjProcesso.MediosDePagoProceso IsNot Nothing Then

            'Se tem divisa efetiva e medio de pago nas coleções
            Dim ret = From r In (From taba In ( _
                (( _
                            (From dp As IAC.ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso In pObjProcesso.DivisasProceso _
                            Group Join mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso On dp.Codigo Equals mp.CodigoIsoDivisa Into mp_join = Group _
                            From mp In mp_join.DefaultIfEmpty(New ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso) Order By dp.Orden _
                            Select New Resultado() With {.codIso = dp.Codigo, .Efectivo = "TRUE", .MedioPago = If(mp.CodigoTipoMedioPago Is Nothing, "FALSE", "TRUE")} _
                            ).Distinct() _
                ).Concat _
                ( _
                            (From mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso _
                            Group Join dp In pObjProcesso.DivisasProceso On mp.CodigoIsoDivisa Equals dp.Codigo Into dp_join = Group _
                            From dp In dp_join.DefaultIfEmpty(New ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso) Order By dp.Orden _
                            Select New Resultado() With {.codIso = mp.CodigoIsoDivisa, .Efectivo = If(dp.Codigo Is Nothing, "FALSE", "TRUE"), .MedioPago = "TRUE"} _
                            ).Distinct() _
                ))) _
                Select New Resultado() With {.codIso = taba.codIso, .Efectivo = taba.Efectivo, .MedioPago = taba.MedioPago}).Distinct


            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        ElseIf pObjProcesso.DivisasProceso Is Nothing AndAlso pObjProcesso.MediosDePagoProceso IsNot Nothing Then

            'Se não divisa efetiva mas tem medio de pago nas coleções
            Dim ret = (From mp As IAC.ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pObjProcesso.MediosDePagoProceso _
            Select New Resultado() With {.codIso = mp.CodigoIsoDivisa, .Efectivo = False, .MedioPago = True}).Distinct

            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        ElseIf pObjProcesso.DivisasProceso IsNot Nothing AndAlso pObjProcesso.MediosDePagoProceso Is Nothing Then

            'Se tem divisa efetiva mas não tem medio de pago nas coleções
            Dim ret = (From dp As IAC.ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso In pObjProcesso.DivisasProceso _
            Select New Resultado() With {.codIso = dp.Codigo, .Efectivo = True, .MedioPago = False}).Distinct

            retorno = (From r In ret _
                          Select r.codIso, r.Efectivo, r.MedioPago).Distinct()

        End If


        Dim objColResultado As New ResultadoCol
        Dim objResultado As Resultado

        If retorno IsNot Nothing Then

            For Each Objretorno As Object In retorno
                objResultado = New Resultado

                objResultado.codIso = Objretorno.codIso
                objResultado.Efectivo = Objretorno.Efectivo
                objResultado.MedioPago = Objretorno.MedioPago

                'Adiciona na coleção
                objColResultado.Add(objResultado)
            Next

        End If

        Return objColResultado

    End Function

    ''' <summary>
    ''' Método que carrega a Treeview com uma coleção de Divisas (Utilidad)
    ''' </summary>
    ''' <param name="pobjTreeView"></param>
    ''' <param name="pObjColDivisas"></param>
    ''' <remarks></remarks>
    Public Sub CarregaTreeview(ByRef pobjTreeView As TreeView, pObjColDivisas As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion)

        pobjTreeView.Nodes.Clear()

        If pObjColDivisas IsNot Nothing Then
            For Each objDivisa As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa In pObjColDivisas

                Dim objTreeNodeDivisa As New TreeNode(objDivisa.Descripcion, objDivisa.CodigoIso)
                Dim objTreeNodeTipoMedioPago As TreeNode
                Dim objTreeNodeMedioPago As TreeNode

                'Adiciona o nó efetivo
                objTreeNodeTipoMedioPago = New TreeNode(Traduzir(TreeViewNodeEfectivo), objDivisa.CodigoIso)
                objTreeNodeDivisa.ChildNodes.Add(objTreeNodeTipoMedioPago)


                For Each TipoMedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.TipoMedioPago In objDivisa.TiposMedioPago
                    'Adiciona Nós de Tipo Médio Pago
                    objTreeNodeTipoMedioPago = New TreeNode(TipoMedioPago.Descripcion, TipoMedioPago.Codigo)

                    For Each MedioPago As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.MedioPago In TipoMedioPago.MediosPago
                        'Adiciona Nós de Médio Pago
                        objTreeNodeMedioPago = New TreeNode(MedioPago.Descripcion, MedioPago.Codigo)
                        objTreeNodeTipoMedioPago.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeTipoMedioPago.ChildNodes, objTreeNodeMedioPago), objTreeNodeMedioPago)
                    Next

                    objTreeNodeDivisa.ChildNodes.AddAt(IndiceOrdenacao(objTreeNodeDivisa.ChildNodes, objTreeNodeTipoMedioPago), objTreeNodeTipoMedioPago)

                Next

                'Por regra não expande a partir do primeiro nível
                objTreeNodeDivisa.CollapseAll()

                'Adiciona a divisa na Tree
                pobjTreeView.Nodes.AddAt(IndiceOrdenacao(pobjTreeView.Nodes, objTreeNodeDivisa), objTreeNodeDivisa)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Carrega os dados na treeview quando a página é carregada pela primeira vez
    ''' </summary>    
    ''' <remarks></remarks>
    Public Sub CarregaTreeViewDividasPossiveis()
        'Carrega a Treeview de Médios de Pago Posíveis
        CarregaTreeview(TrvDivisas, getDivisasPosiveis)

    End Sub

#End Region

#Region "[TOLERANCIAAGRUPACION]"

    ''' <summary>
    ''' Cria a tolerância de agrupação(Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="objAgrupacion"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarToleranciaAgrupacion(objAgrupacion As ContractoServicio.Proceso.GetProcesoDetail.AgrupacionProceso) As PantallaProceso.ToleranciaAgrupacion
        Dim objAgrupacionTolerancia As New PantallaProceso.ToleranciaAgrupacion

        'Cria a estrutura de tolerancias por agrupação
        objAgrupacionTolerancia.CodigoAgrupacion = objAgrupacion.Codigo
        objAgrupacionTolerancia.DescripcionAgrupacion = objAgrupacion.Descripcion
        objAgrupacionTolerancia.ToleranciaBultoMax = objAgrupacion.ToleranciaBultoMax
        objAgrupacionTolerancia.ToleranciaBultoMin = objAgrupacion.ToleranciaBultoMin
        objAgrupacionTolerancia.ToleranciaParcialMax = objAgrupacion.ToleranciaParcialMax
        objAgrupacionTolerancia.ToleranciaParcialMin = objAgrupacion.ToleranciaParcialMin
        objAgrupacionTolerancia.ToleranciaRemesaMax = objAgrupacion.ToleranciaRemesaMax
        objAgrupacionTolerancia.ToleranciaRemesaMin = objAgrupacion.ToleranciaRemesaMin

        Return objAgrupacionTolerancia

    End Function

#End Region

#Region "[TOLERANCIAMEDIOPAGO]"

    ''' <summary>
    ''' Cria a tolerância de Divisa-Efetivo(Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="DescricaoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="DescricaoTipoMedioPago"></param>
    ''' <param name="DivisaProcessoCol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarToleranciaDivisaEfetivo(CodigoDivisa As String, DescricaoDivisa As String, CodigoTipoMedioPago As String, DescricaoTipoMedioPago As String, DivisaProcessoCol As ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion) As PantallaProceso.ToleranciaMedioPago

        Dim objMedioPagoTolerancia As New PantallaProceso.ToleranciaMedioPago

        objMedioPagoTolerancia.CodigoDivisa = CodigoDivisa
        objMedioPagoTolerancia.DescripcionDivisa = DescricaoDivisa
        objMedioPagoTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago
        objMedioPagoTolerancia.DescripcionTipoMedioPago = DescricaoTipoMedioPago

        Dim retornoDivisaTolerancia = From objDivisaTolerancia In DivisaProcessoCol Where objDivisaTolerancia.Codigo = CodigoDivisa

        If retornoDivisaTolerancia IsNot Nothing AndAlso retornoDivisaTolerancia.Count > 0 Then
            objMedioPagoTolerancia.ToleranciaBultoMax = retornoDivisaTolerancia(0).ToleranciaBultolMax
            objMedioPagoTolerancia.ToleranciaBultoMin = retornoDivisaTolerancia(0).ToleranciaBultoMin

            objMedioPagoTolerancia.ToleranciaParcialMax = retornoDivisaTolerancia(0).ToleranciaParcialMax
            objMedioPagoTolerancia.ToleranciaParcialMin = retornoDivisaTolerancia(0).ToleranciaParcialMin

            objMedioPagoTolerancia.ToleranciaRemesaMax = retornoDivisaTolerancia(0).ToleranciaRemesaMax
            objMedioPagoTolerancia.ToleranciaRemesaMin = retornoDivisaTolerancia(0).ToleranciaRemesaMin

        End If

        Return objMedioPagoTolerancia

    End Function

    ''' <summary>
    ''' ''' Cria a tolerância de Divisa-Medio de Pago(Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="descricaoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="DescricaoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>
    ''' <param name="DescricaoMedioPago"></param>
    ''' <param name="MedioPagoProcessoCol"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarToleranciaDivisaMedioPago(CodigoDivisa As String, descricaoDivisa As String, CodigoTipoMedioPago As String, DescricaoTipoMedioPago As String, CodigoMedioPago As String, DescricaoMedioPago As String, MedioPagoProcessoCol As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As PantallaProceso.ToleranciaMedioPago

        '######### Cria a tolerânica desta divisa medio pago #########

        Dim objMedioPagoTolerancia As New PantallaProceso.ToleranciaMedioPago

        objMedioPagoTolerancia.CodigoDivisa = CodigoDivisa
        objMedioPagoTolerancia.DescripcionDivisa = descricaoDivisa
        objMedioPagoTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago
        objMedioPagoTolerancia.DescripcionTipoMedioPago = DescricaoTipoMedioPago
        objMedioPagoTolerancia.CodigoMedioPago = CodigoMedioPago
        objMedioPagoTolerancia.DescripcionMedioPago = DescricaoMedioPago

        Dim retornoDivisaMedioPagoTolerancia = From objDivisaTolerancia In MedioPagoProcessoCol _
                                      Where objDivisaTolerancia.CodigoIsoDivisa = CodigoDivisa _
                                      AndAlso objDivisaTolerancia.CodigoTipoMedioPago = CodigoTipoMedioPago _
                                      AndAlso objDivisaTolerancia.Codigo = CodigoMedioPago

        If retornoDivisaMedioPagoTolerancia IsNot Nothing AndAlso retornoDivisaMedioPagoTolerancia.Count > 0 Then
            objMedioPagoTolerancia.ToleranciaBultoMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaBultolMax
            objMedioPagoTolerancia.ToleranciaBultoMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaBultoMin

            objMedioPagoTolerancia.ToleranciaParcialMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaParcialMax
            objMedioPagoTolerancia.ToleranciaParcialMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaParcialMin

            objMedioPagoTolerancia.ToleranciaRemesaMax = retornoDivisaMedioPagoTolerancia(0).ToleranciaRemesaMax
            objMedioPagoTolerancia.ToleranciaRemesaMin = retornoDivisaMedioPagoTolerancia(0).ToleranciaRemesaMin
        End If

        Return objMedioPagoTolerancia

    End Function

#End Region

#Region "[TERMINOS MEDIO DE PAGO]"

    ''' <summary>
    ''' Cria o medios de pago com os términos (Utilizado em CarregaDados)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>
    ''' <param name="MedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarMedioPagoTerminos(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String, MedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso) As PantallaProceso.MedioPago

        '######### Cria o medio pago com os terminos deste medio pago #########

        'Se o medio de pago possui termino então o cria

        Dim objRespuesta As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta
        objRespuesta = GetTerminosByMedioPago(CodigoDivisa, CodigoTipoMedioPago, CodigoMedioPago)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            Return Nothing
        End If

        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        If objRespuesta.MedioPagos IsNot Nothing _
        AndAlso objRespuesta.MedioPagos.Count > 0 _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago IsNot Nothing _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago.Count > 0 Then

            'Cria o novo objeto de medio de pago que possui término
            objMedioPagoTerminos = New PantallaProceso.MedioPago
            Dim objTerminoMedioPago As PantallaProceso.TerminoMedioPago

            'Cria um novo termino
            objMedioPagoTerminos.CodigoDivisa = objRespuesta.MedioPagos(0).CodigoDivisa
            objMedioPagoTerminos.DescripcionDivisa = objRespuesta.MedioPagos(0).DescripcionDivisa
            objMedioPagoTerminos.CodigoTipoMedioPago = objRespuesta.MedioPagos(0).CodigoTipoMedioPago
            objMedioPagoTerminos.DescripcionTipoMedioPago = objRespuesta.MedioPagos(0).DescripcionTipoMedioPago
            objMedioPagoTerminos.CodigoMedioPago = objRespuesta.MedioPagos(0).Codigo
            objMedioPagoTerminos.DescripcionMedioPago = objRespuesta.MedioPagos(0).Descripcion

            For Each ObjTermino As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In objRespuesta.MedioPagos(0).TerminosMedioPago

                'Só inseri o termino se o mesmo for vigente
                If ObjTermino.Vigente Then
                    objTerminoMedioPago = New PantallaProceso.TerminoMedioPago
                    objTerminoMedioPago.CodigoTermino = ObjTermino.Codigo
                    objTerminoMedioPago.DescripcionTermino = ObjTermino.Descripcion

                    'verifica qual o estado do objeto selecionado
                    Dim objTer As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago = getTerminoColecao(ObjTermino.Codigo, MedioPago.TerminosMedioPago)
                    If objTer Is Nothing Then
                        objTerminoMedioPago.Selecionado = False
                        objTerminoMedioPago.EsObligatorio = False
                    Else
                        objTerminoMedioPago.Selecionado = True
                        objTerminoMedioPago.EsObligatorio = objTer.EsObligatorioTerminoMedioPago
                    End If

                    'Adiciona o termino no medio de pago
                    If objMedioPagoTerminos.TerminosMedioPago Is Nothing Then
                        objMedioPagoTerminos.TerminosMedioPago = New PantallaProceso.TerminoMedioPagoColeccion
                    End If
                    objMedioPagoTerminos.TerminosMedioPago.Add(objTerminoMedioPago)

                End If
            Next

        End If

        Return objMedioPagoTerminos

    End Function

    ''' <summary>
    ''' Cria os terminos de um médio de pago Novo(com as propriedades selecionado igual a false)
    ''' </summary>
    ''' <param name="CodigoDivisa"></param>
    ''' <param name="CodigoTipoMedioPago"></param>
    ''' <param name="CodigoMedioPago"></param>    
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CriarTerminosMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String) As PantallaProceso.TerminoMedioPagoColeccion

        '######### Cria o medio pago com os terminos deste medio pago #########

        'Se o medio de pago possui termino então o cria

        Dim objRespuesta As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta
        objRespuesta = GetTerminosByMedioPago(CodigoDivisa, CodigoTipoMedioPago, CodigoMedioPago)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.MensajeError, True, False) Then
            Return Nothing
        End If

        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

        If objRespuesta.MedioPagos IsNot Nothing _
        AndAlso objRespuesta.MedioPagos.Count > 0 _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago IsNot Nothing _
        AndAlso objRespuesta.MedioPagos(0).TerminosMedioPago.Count > 0 Then

            'Cria o novo objeto de medio de pago que possui término
            objMedioPagoTerminos = New PantallaProceso.MedioPago
            Dim objTerminoMedioPago As PantallaProceso.TerminoMedioPago

            For Each ObjTermino As ContractoServicio.MedioPago.GetMedioPagoDetail.TerminoMedioPago In objRespuesta.MedioPagos(0).TerminosMedioPago

                'Se o termino do medio de pago for vigente
                'inclui na coleção
                If ObjTermino.Vigente Then

                    objTerminoMedioPago = New PantallaProceso.TerminoMedioPago
                    objTerminoMedioPago.CodigoTermino = ObjTermino.Codigo
                    objTerminoMedioPago.DescripcionTermino = ObjTermino.Descripcion
                    'verifica qual o estado do objeto selecionado
                    objTerminoMedioPago.Selecionado = False

                    'Adiciona o termino no medio de pago
                    If objMedioPagoTerminos.TerminosMedioPago Is Nothing Then
                        objMedioPagoTerminos.TerminosMedioPago = New PantallaProceso.TerminoMedioPagoColeccion
                    End If
                    objMedioPagoTerminos.TerminosMedioPago.Add(objTerminoMedioPago)

                End If

            Next

            Return objMedioPagoTerminos.TerminosMedioPago

        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Retorna o termino na coleção de retorno dos terminos do medio de pago(CarregaDados)
    ''' </summary>
    ''' <param name="CodTermino"></param>
    ''' <param name="objColTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getTerminoColecao(CodTermino As String, objColTerminos As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPagoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.TerminoMedioPago

        If objColTerminos IsNot Nothing Then
            Dim retorno = From objTermino In objColTerminos Where objTermino.Codigo = CodTermino

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno(0)
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' verifica se o medio pago com terminos existe na coleção de terminos de medio de pago
    ''' </summary>
    ''' <param name="CodDivisa"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="objColMedioPagoTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoTerminoExisteColecaoMedioPagoTerminos(CodDivisa As String, CodTipoMedioPago As String, CodMedioPago As String, objColMedioPagoTerminos As PantallaProceso.MedioPagoColeccion) As PantallaProceso.MedioPago

        Dim MedioPagoTermino As PantallaProceso.MedioPago = Nothing
        If objColMedioPagoTerminos IsNot Nothing Then

            Dim retorno = From objMedioPagoTermino In objColMedioPagoTerminos Where objMedioPagoTermino.CodigoMedioPago = CodMedioPago _
                      AndAlso objMedioPagoTermino.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPagoTermino.CodigoDivisa = CodDivisa


            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                'Recebe o médio de pago da coleção
                MedioPagoTermino = retorno(0)
            End If

        End If

        'Caso não ache, retorna false
        Return MedioPagoTermino

    End Function

    ''' <summary>
    ''' verifica se o termino existe na coleção de terminos de medio de pago
    ''' </summary>
    ''' <param name="CodDivisa"></param>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="CodMedioPago"></param>
    ''' <param name="CodTermino"></param>
    ''' <param name="objColMedioPagoTerminos"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaTerminoExisteColecaoMedioPagoTerminos(CodDivisa As String, CodTipoMedioPago As String, CodMedioPago As String, CodTermino As String, objColMedioPagoTerminos As PantallaProceso.MedioPagoColeccion) As Boolean

        Dim retorno = From objMedioPagoTermino In objColMedioPagoTerminos Where objMedioPagoTermino.CodigoMedioPago = CodMedioPago _
                      AndAlso objMedioPagoTermino.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPagoTermino.CodigoDivisa = CodDivisa

        Dim MedioPagoTermino As PantallaProceso.MedioPago = Nothing

        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            'Recebe o médio de pago da coleção
            MedioPagoTermino = retorno(0)

            'Procura pelo termino informado(se existe, retorna true)
            Dim retornoTermino = From objTermino In MedioPagoTermino.TerminosMedioPago Where objTermino.CodigoTermino = CodTermino

            If retornoTermino IsNot Nothing AndAlso retornoTermino.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End If


        'Caso não ache, retorna false
        Return False

    End Function

    ''' <summary>
    ''' Obtém os terminos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [PDA] 18/02/2009 Criado
    ''' </history>
    Private Function GetTerminosByMedioPago(CodigoDivisa As String, CodigoTipoMedioPago As String, CodigoMedioPago As String) As ContractoServicio.MedioPago.GetMedioPagoDetail.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.MedioPago.GetMedioPagoDetail.Peticion


        'Divisa, TipoMedioPago e MedioPago
        objPeticion.CodigoIsoDivisa = New List(Of String)
        objPeticion.CodigoIsoDivisa.Add(CodigoDivisa)

        objPeticion.CodigoTipoMedioPago = New List(Of String)
        objPeticion.CodigoTipoMedioPago.Add(CodigoTipoMedioPago)

        objPeticion.CodigoMedioPago = New List(Of String)
        objPeticion.CodigoMedioPago.Add(CodigoMedioPago)

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyMedioPago

        ' chamar servicio
        Return objProxy.GetMedioPagoDetail(objPeticion)

    End Function

#End Region

#Region "[PREENCHER CONTROLES]"

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListBoxCanal()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboCanales.Respuesta

        objRespuesta = objProxyUtilida.GetComboCanales()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        lstCanal.AppendDataBoundItems = True

        lstCanal.Items.Clear()

        If objRespuesta.Canales IsNot Nothing Then

            'ordena a lista de canales
            objRespuesta.Canales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objCanal As IAC.ContractoServicio.Utilidad.GetComboCanales.Canal In objRespuesta.Canales
                lstCanal.Items.Add(New ListItem(objCanal.Codigo & " - " & objCanal.Descripcion, objCanal.Codigo))
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox Canal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListBoxSubCanal()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

        objPeticion.Codigo = New List(Of String)
        If lstCanal IsNot Nothing AndAlso lstCanal.Items.Count > 0 Then
            For Each CanalSelecionado As ListItem In lstCanal.Items
                If CanalSelecionado.Selected Then
                    objPeticion.Codigo.Add(CanalSelecionado.Value)
                End If
            Next
        End If

        objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        lstSubCanais.AppendDataBoundItems = True

        If objRespuesta.Canales IsNot Nothing _
       AndAlso objRespuesta.Canales.Count > 0 Then

            'Adiciona os item selecionados no temporario
            Dim listaSelecionadosTemp As ListItemCollection = Nothing
            If lstSubCanais.Items IsNot Nothing AndAlso lstSubCanais.Items.Count > 0 Then
                listaSelecionadosTemp = New ListItemCollection
                For Each item As ListItem In lstSubCanais.Items
                    If item.Selected Then
                        listaSelecionadosTemp.Add(item)
                    End If
                Next
            End If

            lstSubCanais.Items.Clear()
            For Each canal In objRespuesta.Canales

                If canal.SubCanales IsNot Nothing Then

                    'ordena a lista de sub canales
                    canal.SubCanales.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

                    For Each subCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal In canal.SubCanales

                        Dim item As New ListItem(subCanal.Codigo & " - " & subCanal.Descripcion, subCanal.Codigo)

                        'Se o item estava selecionado
                        If listaSelecionadosTemp IsNot Nothing Then
                            If listaSelecionadosTemp.Contains(item) Then
                                item.Selected = True
                            End If
                        End If

                        'Adiciona o item
                        lstSubCanais.Items.Add(item)
                    Next

                End If

            Next
        Else
            lstSubCanais.Items.Clear()
            lstSubCanais.DataSource = Nothing
            lstSubCanais.DataBind()
        End If

    End Sub

    ''' <summary>
    ''' Preenche os combobox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherComboBoxProducto()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboProductos.Respuesta

        objRespuesta = objProxyUtilida.GetComboProductos

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ddlProducto.AppendDataBoundItems = True
        ddlProducto.Items.Clear()
        ddlProducto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

        If objRespuesta.Productos IsNot Nothing Then
            'ordena a lista de produtos
            'objRespuesta.Productos.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
            For Each objProducto As IAC.ContractoServicio.Utilidad.GetComboProductos.Producto In objRespuesta.Productos
                ddlProducto.Items.Add(New ListItem(objProducto.Codigo & " - " & objProducto.DescripcionClaseBillete, objProducto.Codigo))
            Next
        End If

    End Sub

    ''' <summary>
    ''' Preenche os combobox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherComboBoxModalid()
        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.Respuesta

        objRespuesta = objProxyUtilida.GetComboModalidadesRecuento

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        'Armazena as modadidade de recontagem 
        ModalidadRecuento = objRespuesta.ModalidadesRecuento


        Dim objList As ListItem
        objList = New ListItem(Traduzir("016_ddl_selecione"), String.Empty)
        objList.Selected = False

        ddlModalidad.AppendDataBoundItems = True
        ddlModalidad.Items.Clear()
        ddlModalidad.Items.Add(objList)

        If objRespuesta.ModalidadesRecuento IsNot Nothing Then

            'ordena a lista de ModalidadesRecuento
            objRespuesta.ModalidadesRecuento.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
            For Each objModalidad As IAC.ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuento In objRespuesta.ModalidadesRecuento
                'Evita que sejam inseridos itens duplicados devido as várias características de cada modalidade.
                If ddlModalidad.Items.FindByValue(objModalidad.Codigo) Is Nothing Then
                    objList = New ListItem(objModalidad.Codigo & " - " & objModalidad.Descripcion, objModalidad.Codigo)
                    objList.Selected = False
                    ddlModalidad.Items.Add(objList)
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' Preenche os combobox da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub PreencherComboBoxInformacionAdicional()

        If ddlModalidad.Items.Count > 0 Then

            If ddlModalidad.SelectedItem IsNot Nothing _
            AndAlso ddlModalidad.SelectedItem.Value <> String.Empty _
            AndAlso verificaModalidaAdmiteIac(ddlModalidad.SelectedItem.Value, ModalidadRecuento) Then

                Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
                Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Peticion
                Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Respuesta

                objPeticion.BolEspecificoSaldos = False

                objRespuesta = objProxyUtilida.GetComboInformacionAdicionalConFiltros(objPeticion)

                If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                    Exit Sub
                End If

                ddlIACParcial.AppendDataBoundItems = True
                ddlIACParcial.Items.Clear()
                ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                ddlIACBulto.AppendDataBoundItems = True
                ddlIACBulto.Items.Clear()
                ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                ddlIACRemesa.AppendDataBoundItems = True
                ddlIACRemesa.Items.Clear()
                ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))

                If objRespuesta.Iacs IsNot Nothing Then

                    'ordena a lista de Iacs
                    objRespuesta.Iacs.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))
                    For Each objIAC As IAC.ContractoServicio.Utilidad.GetComboInformacionAdicionalConFiltros.Iac In objRespuesta.Iacs
                        ddlIACParcial.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                        ddlIACBulto.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                        ddlIACRemesa.Items.Add(New ListItem(objIAC.Codigo & " - " & objIAC.Descripcion, objIAC.Codigo))
                    Next

                End If

                ddlIACParcial.Enabled = True
                ddlIACBulto.Enabled = True
                ddlIACRemesa.Enabled = True
            Else
                ddlIACParcial.AppendDataBoundItems = True
                ddlIACParcial.Items.Clear()
                ddlIACParcial.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACParcial.Enabled = False

                ddlIACBulto.AppendDataBoundItems = True
                ddlIACBulto.Items.Clear()
                ddlIACBulto.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACBulto.Enabled = False

                ddlIACRemesa.AppendDataBoundItems = True
                ddlIACRemesa.Items.Clear()
                ddlIACRemesa.Items.Add(New ListItem(Traduzir("016_ddl_selecione"), String.Empty))
                ddlIACRemesa.Enabled = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' Preenche o listbox Agrupaciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListAgrupaciiones()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Respuesta

        objRespuesta = objProxyUtilida.GetListaAgrupaciones

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        lstAgrupacionesPosibles.AppendDataBoundItems = True

        lstAgrupacionesPosibles.Items.Clear()
        For Each objAgrupacion As IAC.ContractoServicio.Utilidad.GetListaAgrupaciones.Agrupacion In objRespuesta.Agrupaciones
            lstAgrupacionesPosibles.Items.Add(New ListItem(objAgrupacion.Descripcion, objAgrupacion.Codigo))
        Next

    End Sub

#End Region

#Region "[ATRIBUI VALORES]"

    ''' <summary>
    ''' Associa o objeto de tolerancia(Popup) com o medio pago em questão
    ''' </summary>
    ''' <param name="objMedioPagoDivisa"></param>
    ''' <remarks></remarks>
    Public Sub AtribuiValoresToleranciaMedioPago(ByRef objMedioPagoDivisa As ContractoServicio.Proceso.SetProceso.MedioPagoProceso)

        'Código da divisa passada
        Dim strCodigoDivisa As String = objMedioPagoDivisa.CodigoIsoDivisa
        Dim strCodigoTipoMedioPago As String = objMedioPagoDivisa.CodigoTipoMedioPago
        Dim strCodigoMedioPago As String = objMedioPagoDivisa.Codigo

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa _
                          AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoTipoMedioPago _
                          AndAlso objMedioPago.CodigoMedioPago = strCodigoMedioPago

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objMedioPagoDivisa.ToleranciaBultolMax = retorno(0).ToleranciaBultoMax
                objMedioPagoDivisa.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objMedioPagoDivisa.ToleranciaParcialMax = retorno(0).ToleranciaParcialMax
                objMedioPagoDivisa.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objMedioPagoDivisa.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
                objMedioPagoDivisa.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
            End If

        End If

    End Sub

    ''' <summary>
    ''' Associa o objeto de tolerância Medio Pago(Popup) de uma divisa com a divisa efetivo em questão
    ''' </summary>
    ''' <param name="objDivisaEfetivo"></param>
    ''' <remarks></remarks>
    Public Sub AtribuiValoresToleranciaDivisaEfetivo(ByRef objDivisaEfetivo As ContractoServicio.Proceso.SetProceso.DivisaProceso)

        'Código da divisa passada
        Dim strCodigoDivisa As String = objDivisaEfetivo.Codigo


        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoDivisa AndAlso objMedioPago.CodigoMedioPago = String.Empty

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objDivisaEfetivo.ToleranciaBultolMax = retorno(0).ToleranciaBultoMax
                objDivisaEfetivo.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objDivisaEfetivo.ToleranciaParcialMax = retorno(0).ToleranciaParcialMax
                objDivisaEfetivo.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objDivisaEfetivo.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
                objDivisaEfetivo.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
            End If


        End If

    End Sub

    ''' <summary>
    ''' Associa o objeto de divisa(Popup) efetivo com o medio pago em questão
    ''' </summary>
    ''' <param name="objAgrupacao"></param>
    ''' <remarks></remarks>
    Public Sub AtribuiValoresToleranciaAgrupacao(ByRef objAgrupacao As ContractoServicio.Proceso.SetProceso.AgrupacionProceso)

        'Código da Agrupação
        Dim strCodigoAgrupacao As String = objAgrupacao.Codigo

        If ToleranciaAgrupaciones IsNot Nothing Then

            Dim retorno = From objAgrupacion In ToleranciaAgrupaciones Where objAgrupacion.CodigoAgrupacion = strCodigoAgrupacao

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objAgrupacao.ToleranciaParcialMin = retorno(0).ToleranciaParcialMin
                objAgrupacao.TolerenciaParcialMax = retorno(0).ToleranciaParcialMax
                objAgrupacao.ToleranciaBultoMin = retorno(0).ToleranciaBultoMin
                objAgrupacao.ToleranciaBultoMax = retorno(0).ToleranciaBultoMax
                objAgrupacao.ToleranciaRemesaMin = retorno(0).ToleranciaRemesaMin
                objAgrupacao.ToleranciaRemesaMax = retorno(0).ToleranciaRemesaMax
            End If

        End If



    End Sub

    ''' <summary>
    ''' Associa ao medio pago os valores de terminos selecionados na popup
    ''' </summary>
    ''' <param name="pobjMedioPago"></param>
    ''' <remarks></remarks>
    Public Sub AtribuiValoresTerminoMedioPago(ByRef pobjMedioPago As ContractoServicio.Proceso.SetProceso.MedioPagoProceso)

        Dim strCodigoDivisa As String = pobjMedioPago.CodigoIsoDivisa
        Dim strCodigoTipoMedioPago As String = pobjMedioPago.CodigoTipoMedioPago
        Dim strCodigoMedioPago As String = pobjMedioPago.Codigo

        If TerminosMedioPagos IsNot Nothing Then

            'Código da divisa passada        
            Dim retorno = From objMedioPago In TerminosMedioPagos Where objMedioPago.CodigoDivisa = strCodigoDivisa _
                          AndAlso objMedioPago.CodigoTipoMedioPago = strCodigoTipoMedioPago _
                          AndAlso objMedioPago.CodigoMedioPago = strCodigoMedioPago

            'Retorna o objeto filtrado na coleção
            Dim objMedioPagoTermino As PantallaProceso.MedioPago = Nothing
            Dim objMedioPagoTerminoCol As PantallaProceso.MedioPagoColeccion = Nothing

            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                objMedioPagoTermino = retorno(0)

                'Adiciona na coleção de envio  
                If objMedioPagoTermino.TerminosMedioPago IsNot Nothing AndAlso _
                objMedioPagoTermino.TerminosMedioPago.Count > 0 Then

                    For Each objTermino In objMedioPagoTermino.TerminosMedioPago
                        If objTermino.Selecionado Then

                            'Verifica se a coleção está vazia, caso afirmatico, então cria
                            If pobjMedioPago.TerminosMedioPago Is Nothing Then
                                pobjMedioPago.TerminosMedioPago = New ContractoServicio.Proceso.SetProceso.TerminoMedioPagoColeccion
                            End If

                            'Cria objeto de envio
                            Dim objTerminoEnvio As New ContractoServicio.Proceso.SetProceso.TerminoMedioPago
                            objTerminoEnvio.Codigo = objTermino.CodigoTermino
                            objTerminoEnvio.EsObligatorioTerminoMedioPago = objTermino.EsObligatorio

                            'Adiciona na coleção para envio
                            pobjMedioPago.TerminosMedioPago.Add(objTerminoEnvio)
                        End If
                    Next

                End If

            End If

        End If


    End Sub

#End Region

#Region "[TOLERANCIASAGRUPACIONES]"

    ''' <summary>
    ''' Atualiza as agrupações
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AtualizaToleranciasAgrupaciones()

        Dim objAgrupacaoCol As New PantallaProceso.ToleranciaAgrupacionColeccion

        For Each Agrupacion As ListItem In lstAgrupacionesCompoenProceso.Items

            Dim objAgrupacion As PantallaProceso.ToleranciaAgrupacion = verificaAgrupacaoTolerancia(Agrupacion.Value)
            If objAgrupacion Is Nothing Then
                objAgrupacion = New PantallaProceso.ToleranciaAgrupacion
                objAgrupacion.CodigoAgrupacion = Agrupacion.Value
                objAgrupacion.DescripcionAgrupacion = Agrupacion.Text
            End If

            'Cria a Coleção de Agrupações
            If objAgrupacaoCol Is Nothing Then
                objAgrupacaoCol = New PantallaProceso.ToleranciaAgrupacionColeccion()
            End If
            objAgrupacaoCol.Add(objAgrupacion)

        Next

        'Atualiza as Agrupações
        ToleranciaAgrupaciones = objAgrupacaoCol

    End Sub

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaAgrupacaoTolerancia(CodigoAgrupacao As String) As PantallaProceso.ToleranciaAgrupacion

        If ToleranciaAgrupaciones IsNot Nothing Then

            Dim retorno = From objAgrupacao In ToleranciaAgrupaciones Where objAgrupacao.CodigoAgrupacion = CodigoAgrupacao

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoToleranciaDivisaEfectivo(CodigoDivisa As String, CodTipoMedioPago As String) As PantallaProceso.ToleranciaMedioPago

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = CodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPago.CodigoMedioPago = String.Empty

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Verifica se a agrupação existe na coleção
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaMedioPagoToleranciaDivisaMedioPago(CodigoDivisa As String, CodTipoMedioPago As String, CodMedioPago As String) As PantallaProceso.ToleranciaMedioPago

        If ToleranciaMedioPagos IsNot Nothing Then

            Dim retorno = From objMedioPago In ToleranciaMedioPagos Where objMedioPago.CodigoDivisa = CodigoDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago AndAlso objMedioPago.CodigoMedioPago = CodMedioPago

            'Retorna o objeto filtrado na coleção
            If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
                Return retorno.ElementAt(0)
            End If

        End If

        Return Nothing

    End Function

#End Region

#Region "[TOLERANCIAMEDIOPAGO]"

    Public Sub AtualizaToleranciasMedioPago()

        Dim objMedioPagoCol As New PantallaProceso.ToleranciaMedioPagoColeccion

        'Divisas
        For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

            'Tipo de Medio de Pago
            For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                'Verifica se o tipo de médio de pago é efetivo
                If objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                    Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = verificaMedioPagoToleranciaDivisaEfectivo(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value)

                    If objMedioPagoTolerancia Is Nothing Then
                        objMedioPagoTolerancia = New PantallaProceso.ToleranciaMedioPago
                        objMedioPagoTolerancia.CodigoDivisa = objTreeNodeDivisas.Value
                        objMedioPagoTolerancia.DescripcionDivisa = objTreeNodeDivisas.Text
                        objMedioPagoTolerancia.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                        objMedioPagoTolerancia.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                    End If

                    objMedioPagoCol.Add(objMedioPagoTolerancia)

                Else

                    'Medio de Pago
                    For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes

                        Dim objMedioPagoTolerancia As PantallaProceso.ToleranciaMedioPago = verificaMedioPagoToleranciaDivisaMedioPago(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value)

                        If objMedioPagoTolerancia Is Nothing Then
                            objMedioPagoTolerancia = New PantallaProceso.ToleranciaMedioPago
                            objMedioPagoTolerancia.CodigoDivisa = objTreeNodeDivisas.Value
                            objMedioPagoTolerancia.DescripcionDivisa = objTreeNodeDivisas.Text
                            objMedioPagoTolerancia.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPagoTolerancia.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                            objMedioPagoTolerancia.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objMedioPagoTolerancia.DescripcionMedioPago = objTreeNodeMedioPago.Text
                        End If

                        objMedioPagoCol.Add(objMedioPagoTolerancia)

                    Next

                End If

            Next
        Next

        ToleranciaMedioPagos = objMedioPagoCol

    End Sub

#End Region

#Region "[TERMINOS MEDIOS DE PAGO]"

    Public Sub AtualizaTerminosMedioPago()

        Dim objMedioPagoCol As New PantallaProceso.MedioPagoColeccion

        'Divisas
        For Each objTreeNodeDivisas As TreeNode In TrvProcesos.Nodes

            'Tipo de Medio de Pago
            For Each objTreeNodeTipoMedioPago As TreeNode In objTreeNodeDivisas.ChildNodes

                'Verifica se o tipo de médio de pago é efetivo
                If Not objTreeNodeTipoMedioPago.Text.Equals(Traduzir(TreeViewNodeEfectivo)) Then

                    'Medio de Pago
                    For Each objTreeNodeMedioPago As TreeNode In objTreeNodeTipoMedioPago.ChildNodes


                        Dim objMedioPagoTerminos As PantallaProceso.MedioPago = Nothing

                        '######## Cria Terminos do medio de Pago ################

                        objMedioPagoTerminos = verificaMedioPagoTerminoExisteColecaoMedioPagoTerminos(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value, TerminosMedioPagos)

                        If objMedioPagoTerminos IsNot Nothing Then
                            'Se não existir o objeto então o cria
                            If TerminosMedioPagos Is Nothing Then
                                TerminosMedioPagos = New PantallaProceso.MedioPagoColeccion
                            End If
                            TerminosMedioPagos.Add(objMedioPagoTerminos)
                        End If

                        'Caso não encontre, cria um novo
                        If objMedioPagoTerminos Is Nothing Then
                            objMedioPagoTerminos = New PantallaProceso.MedioPago
                            objMedioPagoTerminos.CodigoDivisa = objTreeNodeDivisas.Value
                            objMedioPagoTerminos.DescripcionDivisa = objTreeNodeDivisas.Text
                            objMedioPagoTerminos.CodigoTipoMedioPago = objTreeNodeTipoMedioPago.Value
                            objMedioPagoTerminos.DescripcionTipoMedioPago = objTreeNodeTipoMedioPago.Text
                            objMedioPagoTerminos.CodigoMedioPago = objTreeNodeMedioPago.Value
                            objMedioPagoTerminos.DescripcionMedioPago = objTreeNodeMedioPago.Text
                            objMedioPagoTerminos.TerminosMedioPago = CriarTerminosMedioPago(objTreeNodeDivisas.Value, objTreeNodeTipoMedioPago.Value, objTreeNodeMedioPago.Value)
                        End If

                        '########################################################

                        'Só adiciona na coleção de medio de pago terminos se possui terminos
                        If objMedioPagoTerminos.TerminosMedioPago IsNot Nothing _
                        AndAlso objMedioPagoTerminos.TerminosMedioPago.Count > 0 Then
                            objMedioPagoCol.Add(objMedioPagoTerminos)
                        End If


                    Next

                End If

            Next
        Next

        TerminosMedioPagos = objMedioPagoCol

    End Sub

#End Region

#Region "[DEMAIS METODOS]"

    ''' <summary>
    ''' Enumerado de níveis a expandir
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eExpadirNivel As Integer

        Primeiro = 0
        Segundo = 1
        Terceiro = 2
        Nenhum = 3

    End Enum

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

    ''' <summary>
    ''' Verifica se a modalidade passada admite IAC
    ''' </summary>
    ''' <param name="CodModalid"></param>
    ''' <param name="pobjColModalidad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function verificaModalidaAdmiteIac(CodModalid As String, pobjColModalidad As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion) As Boolean

        Dim retorno = From objModalidad In pobjColModalidad Where objModalidad.Codigo = CodModalid AndAlso objModalidad.AdmiteIac = True

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Retorna Cliente para Set
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornaClienteSet() As ContractoServicio.Proceso.SetProceso.Cliente

        Dim objCliente As New IAC.ContractoServicio.Proceso.SetProceso.Cliente
        Dim objSubCliente As IAC.ContractoServicio.Proceso.SetProceso.SubCliente = Nothing
        Dim objPuntoServicio As IAC.ContractoServicio.Proceso.SetProceso.PuntoServicio = Nothing

        'Monta o cliente
        objCliente.Codigo = ClienteSelecionado.Codigo
        If SubClientesSelecionados IsNot Nothing AndAlso SubClientesSelecionados.Count > 0 Then

            'Monta os subclientes do cliente selecionado
            For Each subcliente As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente In SubClientesSelecionados
                If objCliente.SubClientes Is Nothing Then
                    objCliente.SubClientes = New ContractoServicio.Proceso.SetProceso.SubClienteColeccion
                End If
                objSubCliente = New IAC.ContractoServicio.Proceso.SetProceso.SubCliente
                objSubCliente.Codigo = subcliente.Codigo

                'Monta os pontos de serviço para cada subcliente selecionado

                Dim objColPuntoServicioSelecionados As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion = Nothing

                'Retorna os pontos de serviço selecinados por subcliente  
                objColPuntoServicioSelecionados = RetornaPontosServicoSelecionadosPorSubCliente(objCliente.Codigo, subcliente.Codigo)

                If objColPuntoServicioSelecionados IsNot Nothing Then

                    For Each puntoServicio In objColPuntoServicioSelecionados
                        'Cria a coleção de ponto de serviço caso algum seja retornado
                        If objSubCliente.PuntosServicio Is Nothing Then
                            objSubCliente.PuntosServicio = New ContractoServicio.Proceso.SetProceso.PuntoServicioColeccion
                        End If
                        objPuntoServicio = New IAC.ContractoServicio.Proceso.SetProceso.PuntoServicio
                        objPuntoServicio.Codigo = puntoServicio.Codigo

                        'Adiciona o Ponto de serviço no subcliente
                        objSubCliente.PuntosServicio.Add(objPuntoServicio)
                    Next

                End If


                'Adiciona o Subcliente no cliente
                objCliente.SubClientes.Add(objSubCliente)
            Next
        End If

        Return objCliente

    End Function

    ''' <summary>
    ''' Retorna os pontos de serviço selecionados por subcliente 
    ''' </summary>
    ''' <param name="CodigoCliente"></param>
    ''' <param name="CodigoSubCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornaPontosServicoSelecionadosPorSubCliente(CodigoCliente As String, CodigoSubCliente As String) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion

        If PuntoServiciosSelecionados IsNot Nothing Then

            Dim objColPuntoServicio As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
            Dim objPuntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

            For Each cliente In PuntoServiciosSelecionados.Where(Function(pss) pss.CodigoCliente = CodigoCliente)
                For Each subCliente As Negocio.Subcliente In cliente.Subclientes.Where(Function(sc) sc.CodigoSubcliente = CodigoSubCliente)
                    For Each puntoServicio As Negocio.PuntoServicio In subCliente.PtosServicio
                        objPuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
                        objPuntoServicio.Codigo = puntoServicio.CodigoPuntoServicio
                        objPuntoServicio.Descripcion = puntoServicio.DesPuntoServicio
                        objColPuntoServicio.Add(objPuntoServicio)
                    Next
                Next
            Next

            Return objColPuntoServicio

        End If

        'Caso chegue neste ponto retorna nothing para ser tratado 
        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna subCanal para Set(gravar)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetornaSubCanalSet() As ContractoServicio.Proceso.SetProceso.SubCanalColeccion
        Dim objSubCanalCol As ContractoServicio.Proceso.SetProceso.SubCanalColeccion = Nothing
        Dim objSubCanal As ContractoServicio.Proceso.SetProceso.SubCanal = Nothing

        If lstSubCanais.Items.Count > 0 Then
            For Each subCanal As ListItem In lstSubCanais.Items
                If subCanal.Selected Then
                    'Adiciona na coleção de subcanais
                    If objSubCanalCol Is Nothing Then
                        objSubCanalCol = New ContractoServicio.Proceso.SetProceso.SubCanalColeccion
                    End If
                    objSubCanal = New ContractoServicio.Proceso.SetProceso.SubCanal
                    objSubCanal.Codigo = subCanal.Value
                    objSubCanalCol.Add(objSubCanal)
                End If
            Next
        End If

        Return objSubCanalCol
    End Function

    ''' <summary>
    ''' Retorna as divisas posíveis que serão apresentadas na treview
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getDivisasPosiveis() As IAC.ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyUtilidad
        'Dim lnAccionProceso As New LogicaNegocio.AccionProceso
        Dim objRespuesta As ContractoServicio.Utilidad.GetDivisasMedioPago.Respuesta = objProxy.GetDivisasMedioPago()

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Divisas
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Retorna o processo passado como parâmetro
    ''' </summary>
    ''' <param name="CodCliente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getProceso(CodCliente As String, CodSubCliente As String, CodPuntoServicio As String, CodDelegacion As String, CodSubCanal As String, IdentificadorProceso As String) As IAC.ContractoServicio.Proceso.GetProcesoDetail.ProcesoColeccion

        Dim objPeticion As New ContractoServicio.Proceso.GetProcesoDetail.Peticion
        objPeticion.PeticionProcesos = New ContractoServicio.Proceso.GetProcesoDetail.PeticionProcesoColeccion
        'Adiciona a petição referente ao processo
        Dim objPeticionProcesso As New ContractoServicio.Proceso.GetProcesoDetail.PeticionProceso
        objPeticionProcesso.CodigoCliente = CodCliente
        objPeticionProcesso.CodigoSubcliente = CodSubCliente
        objPeticionProcesso.CodigoPuntoServicio = CodPuntoServicio
        objPeticionProcesso.CodigoDelegacion = CodDelegacion
        objPeticionProcesso.CodigoSubcanal = CodSubCanal
        objPeticionProcesso.IdentificadorProceso = IdentificadorProceso

        'Adiciona a petição
        objPeticion.PeticionProcesos.Add(objPeticionProcesso)

        ' invocar logica negocio
        Dim objProxy As New Comunicacion.ProxyProceso
        'Dim lnAccionProceso As New LogicaNegocio.AccionProceso
        Dim objRespuesta As ContractoServicio.Proceso.GetProcesoDetail.Respuesta = objProxy.GetProcesoDetail(objPeticion)

        If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Return objRespuesta.Procesos
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Retorna a Divisa pelo código na coleção informada
    ''' </summary>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="pobjColDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaDivisaEfetivo(CodIsoDivisa As String, pobjColDivisa As ContractoServicio.Proceso.GetProcesoDetail.DivisaProcesoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.DivisaProceso

        Dim retorno = From objDivisa In pobjColDivisa Where objDivisa.Codigo = CodIsoDivisa Order By objDivisa.Orden Ascending

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return retorno.ElementAt(0)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna a Divisa pelo código na coleção informada
    ''' </summary>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="pobjColDivisa"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaDivisaMedioPago(CodIsoDivisa As String, pobjColDivisa As ContractoServicio.Utilidad.GetDivisasMedioPago.DivisaColeccion) As ContractoServicio.Utilidad.GetDivisasMedioPago.Divisa

        Dim retorno = From objDivisa In pobjColDivisa Where objDivisa.CodigoIso = CodIsoDivisa

        'Retorna o objeto filtrado na coleção
        If retorno IsNot Nothing AndAlso retorno.Count > 0 Then
            Return retorno.ElementAt(0)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna os tipos de médio de pago da divisa pelo código da divisa na coleção informada
    ''' </summary>
    ''' <param name="CodIsoDivisa"></param>
    ''' <param name="pobjColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaTipoMedioPago(CodIsoDivisa As String, pobjColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As Dictionary(Of String, String)

        Dim retorno = (From objMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pobjColMedioPago Where objMedioPago.CodigoIsoDivisa = CodIsoDivisa _
                       Select objMedioPago.CodigoTipoMedioPago, objMedioPago.DescripcionTipoMedioPago).Distinct


        Dim DicionarioTipoMedioPago As Dictionary(Of String, String)
        If retorno IsNot Nothing Then
            DicionarioTipoMedioPago = New Dictionary(Of String, String)
            For Each objRetorno In retorno
                DicionarioTipoMedioPago.Add(objRetorno.CodigoTipoMedioPago, objRetorno.DescripcionTipoMedioPago)
            Next
            Return DicionarioTipoMedioPago
        Else
            Return Nothing
        End If


        Return Nothing

    End Function

    ''' <summary>
    ''' Retorna os tipos de médio de pago da divisa pelo código da divisa na coleção informada
    ''' </summary>
    ''' <param name="CodTipoMedioPago"></param>
    ''' <param name="pobjColMedioPago"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function retornaMedioPago(CodDivisa As String, CodTipoMedioPago As String, pobjColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion) As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion

        Dim retorno = (From objMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProceso In pobjColMedioPago Where objMedioPago.CodigoIsoDivisa = CodDivisa AndAlso objMedioPago.CodigoTipoMedioPago = CodTipoMedioPago)

        Dim objColMedioPago As ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion = Nothing
        For Each objRetorno In retorno
            'Adiciona na coleção
            If objColMedioPago Is Nothing Then
                objColMedioPago = New ContractoServicio.Proceso.GetProcesoDetail.MedioPagoProcesoColeccion
            End If
            objColMedioPago.Add(objRetorno)
        Next

        If objColMedioPago IsNot Nothing Then
            Return objColMedioPago
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' MontaMensagensErro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErro(objColSubCanalSet As ContractoServicio.Proceso.SetProceso.SubCanalColeccion, _
                                       Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
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

                'Verifica se a descrição do canal é obrigatório
                If txtDescricaoProceso.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoProceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvDescricaoObrigatorio.IsValid = True
                End If

                If objColSubCanalSet Is Nothing OrElse objColSubCanalSet.Count = 0 Then

                    strErro.Append(csvSubCanalObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvSubCanalObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        lstSubCanais.Focus()
                        focoSetado = True
                    End If

                Else
                    csvSubCanalObrigatorio.IsValid = True
                End If

                'Verifica se o produto foi selecionado.
                If ddlProducto.SelectedItem.Value.Trim.Equals(String.Empty) Then

                    strErro.Append(csvProductoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvProductoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlProducto.Focus()
                        focoSetado = True
                    End If

                Else
                    csvProductoObrigatorio.IsValid = True
                End If

                'Verifica se a modalidad foi selecionada.
                If ddlModalidad.SelectedItem.Value.Trim.Equals(String.Empty) Then

                    strErro.Append(csvModalidadObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvModalidadObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlModalidad.Focus()
                        focoSetado = True
                    End If

                Else
                    csvModalidadObrigatorio.IsValid = True
                End If


                'Verifica se foi selecionada algum declarado.
                If Not rdbPorMedioPago.Checked AndAlso Not rdbPorAgrupaciones.Checked Then

                    strErro.Append(csvInformacioDeclaradoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvInformacioDeclaradoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        rdbPorAgrupaciones.Focus()
                        focoSetado = True
                    End If

                Else
                    csvInformacioDeclaradoObrigatorio.IsValid = True
                End If

                'Verifica se foi selecionada algum declarado.
                If pnlAgrupacao.Visible AndAlso lstAgrupacionesCompoenProceso.Items.Count = 0 Then

                    strErro.Append(csvAgrupacionesCompoenProcesoObrigatorio.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvAgrupacionesCompoenProcesoObrigatorio.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        lstAgrupacionesCompoenProceso.Focus()
                        focoSetado = True
                    End If

                Else
                    csvAgrupacionesCompoenProcesoObrigatorio.IsValid = True
                    ValidarAgrupacion = True
                End If

                'Verifica se foi selecionada algum medio pago.
                If pnlMediosPago.Visible AndAlso TrvProcesos.Nodes.Count = 0 Then

                    strErro.Append(csvTrvProcesos.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTrvProcesos.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        TrvProcesos.Focus()
                        focoSetado = True
                    End If

                Else
                    csvTrvProcesos.IsValid = True
                    ValidarMedioPago = True
                End If

            End If

        End If

        Return strErro.ToString

    End Function

#End Region

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        '### Validação Comum aos controles ###
        If ClienteSelecionado Is Nothing Then
            btnSubCliente.Habilitado = False
            btnPuntoServicio.Habilitado = False
            SubClientesSelecionados = Nothing
            PuntoServiciosSelecionados = Nothing
        Else
            btnSubCliente.Habilitado = True
            If SubClientesSelecionados Is Nothing Then
                btnPuntoServicio.Habilitado = False
                PuntoServiciosSelecionados = Nothing
            Else
                btnPuntoServicio.Habilitado = True
            End If
        End If

        'Termino só esta visivel quando o o tipo de declarado é por medio de pago
        If rdbPorMedioPago.Checked Then
            btnTerminoMedioPago.Visible = True
        Else
            btnTerminoMedioPago.Visible = False
        End If

        'Deixa invisível as sessões de agrupação e medio de pago
        'caso nenhuma seja selecionada
        If Not rdbPorAgrupaciones.Checked AndAlso Not rdbPorMedioPago.Checked Then
            pnlAgrupacao.Visible = False
            pnlMediosPago.Visible = False
        End If

        '#######################################

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                'Textbox
                txtCliente.Enabled = False
                txtSubCliente.Enabled = False
                txtPuntoServicio.Enabled = False
                txtDelegacion.Enabled = False
                txtDescricaoProceso.Enabled = True
                txtClienteFaturacion.Enabled = False
                txtObservaciones.Enabled = True

                'Botões
                btnBuscarCliente.Visible = True
                btnSubCliente.Visible = True
                btnPuntoServicio.Visible = True
                imgBtnAgrupacionesIncluir.Visible = True
                imgBtnAgrupacionesExcluir.Visible = True
                imgBtnAgrupacionesIncluirTodas.Visible = True
                imgBtnAgrupacionesExcluirTodas.Visible = True
                imgBtnIncluirTreeview.Visible = True
                imgBtnIncluirTreeview.Visible = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnTolerancia.Visible = True

                'DropDownlist
                ddlProducto.Enabled = True
                ddlModalidad.Enabled = True
                'ddlIACParcial.Enabled = True

                'RadioButtonList                
                rdbPorAgrupaciones.Enabled = True
                rdbPorMedioPago.Enabled = True

                'ListBox
                lstAgrupacionesPosibles.Visible = True

                'TreeView
                TrvDivisas.Visible = True
                TrvProcesos.Visible = True

                'Checkbox
                chkContarChequeTotales.Enabled = True
                chkContarTicketTotales.Enabled = True
                chkContarOtrosValoresTotales.Enabled = True

                lblVigente.Visible = False
                chkVigente.Checked = True
                vigenteLinha.Visible = False
                chkVigente.Enabled = False
                chkVigente.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Duplicar

                'TextBox
                txtCliente.Enabled = False
                txtSubCliente.Enabled = False
                txtPuntoServicio.Enabled = False
                txtDelegacion.Enabled = False
                txtDescricaoProceso.Enabled = True
                txtClienteFaturacion.Enabled = False
                txtObservaciones.Enabled = True

                'Botões
                btnBuscarCliente.Visible = True
                btnSubCliente.Visible = True
                btnPuntoServicio.Visible = True
                imgBtnAgrupacionesIncluir.Visible = True
                imgBtnAgrupacionesExcluir.Visible = True
                imgBtnAgrupacionesIncluirTodas.Visible = True
                imgBtnAgrupacionesExcluirTodas.Visible = True
                imgBtnIncluirTreeview.Visible = True
                imgBtnIncluirTreeview.Visible = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False
                btnTolerancia.Visible = True

                'DropDownlist
                ddlProducto.Enabled = True
                ddlModalidad.Enabled = True
                'ddlIACParcial.Enabled = True

                'RadioButtonList
                rdbPorAgrupaciones.Enabled = True
                rdbPorMedioPago.Enabled = True

                'ListBox
                lstAgrupacionesPosibles.Visible = True

                'TreeView
                TrvDivisas.Visible = True
                TrvProcesos.Visible = True

                'Checkbox
                chkContarChequeTotales.Enabled = True
                chkContarTicketTotales.Enabled = True
                chkContarOtrosValoresTotales.Enabled = True

                lblVigente.Visible = False
                chkVigente.Checked = True
                vigenteLinha.Visible = False
                chkVigente.Enabled = False
                chkVigente.Visible = False

            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                'TextBox
                txtCliente.Enabled = False
                txtSubCliente.Enabled = False
                txtPuntoServicio.Enabled = False
                txtDelegacion.Enabled = False
                txtDescricaoProceso.Enabled = False
                txtClienteFaturacion.Enabled = False
                txtObservaciones.Enabled = False

                'Botões
                btnBuscarCliente.Visible = False
                btnSubCliente.Visible = False
                btnPuntoServicio.Visible = False
                btnBuscarClienteFaturacion.Visible = False
                imgBtnAgrupacionesIncluir.Visible = False
                imgBtnAgrupacionesExcluir.Visible = False
                imgBtnAgrupacionesIncluirTodas.Visible = False
                imgBtnAgrupacionesExcluirTodas.Visible = False
                imgBtnIncluirTreeview.Visible = False
                imgBtnExcluirTreeview.Visible = False
                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True
                btnTolerancia.Visible = True

                'DropDownlist
                ddlProducto.Enabled = False
                ddlProducto.ToolTip = ddlProducto.SelectedItem.Text

                ddlModalidad.Enabled = False
                ddlModalidad.ToolTip = ddlModalidad.SelectedItem.Text

                ddlIACParcial.Enabled = False
                ddlIACParcial.ToolTip = ddlIACParcial.SelectedItem.Text

                ddlIACBulto.Enabled = False
                ddlIACBulto.ToolTip = ddlIACBulto.SelectedItem.Text

                ddlIACRemesa.Enabled = False
                ddlIACRemesa.ToolTip = ddlIACRemesa.SelectedItem.Text

                'RadioButtonList
                rdbPorAgrupaciones.Enabled = False
                rdbPorMedioPago.Enabled = False

                'ListBox
                lstAgrupacionesPosibles.Visible = False
                lstCanal.AutoPostBack = False

                'TreeView
                TrvDivisas.Visible = False
                TrvProcesos.Visible = True
                TrvProcesos.Enabled = False

                'Checkbox
                chkContarChequeTotales.Enabled = False
                chkContarTicketTotales.Enabled = False
                chkContarOtrosValoresTotales.Enabled = False

                lblVigente.Visible = True
                chkVigente.Enabled = False

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                'TextBox
                txtCliente.Enabled = False
                txtSubCliente.Enabled = False
                txtPuntoServicio.Enabled = False
                txtDelegacion.Enabled = False
                txtDescricaoProceso.Enabled = True
                txtClienteFaturacion.Enabled = False

                'Botões
                btnBuscarCliente.Visible = True
                btnSubCliente.Visible = True
                btnPuntoServicio.Visible = True
                imgBtnAgrupacionesIncluir.Visible = True
                imgBtnAgrupacionesExcluir.Visible = True
                imgBtnAgrupacionesIncluirTodas.Visible = True
                imgBtnAgrupacionesExcluirTodas.Visible = True
                imgBtnIncluirTreeview.Visible = True
                imgBtnIncluirTreeview.Visible = True
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False

                'DropDownlist
                ddlProducto.Enabled = True
                ddlModalidad.Enabled = True
                'ddlIACParcial.Enabled = True

                'RadioButtonList
                rdbPorAgrupaciones.Enabled = True
                rdbPorMedioPago.Enabled = True

                'ListBox
                lstAgrupacionesPosibles.Visible = True

                'TreeView
                TrvDivisas.Visible = True
                TrvProcesos.Visible = True

                'Checkbox
                chkContarChequeTotales.Enabled = True
                chkContarTicketTotales.Enabled = True
                chkContarOtrosValoresTotales.Enabled = True

                chkVigente.Visible = True
                vigenteLinha.Visible = True
                lblVigente.Visible = True

                If chkVigente.Checked AndAlso EsVigente Then
                    chkVigente.Enabled = False
                Else
                    chkVigente.Enabled = True
                End If


            Case Aplicacao.Util.Utilidad.eAcao.NoAction
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Inicial
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Busca
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True

        End Select

        Dim objColSubCanalSet As ContractoServicio.Proceso.SetProceso.SubCanalColeccion
        'retorna a seleção de subcanais selecionados
        objColSubCanalSet = RetornaSubCanalSet()

        Dim mensagemErro As String = MontaMensagensErro(objColSubCanalSet)

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        If mensagemErro <> String.Empty Then
            Master.ControleErro.ShowError(mensagemErro, False)
        End If
    End Sub

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property DescricaoAtual() As String
        Get
            Return ViewState("DescricaoAtual")
        End Get
        Set(value As String)
            ViewState("DescricaoAtual") = value.Trim
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValidarAgrupacion() As Boolean
        Get
            If ViewState("ValidarAgrupacion") Is Nothing Then
                ViewState("ValidarAgrupacion") = False
            End If
            Return ViewState("ValidarAgrupacion")
        End Get
        Set(value As Boolean)
            ViewState("ValidarAgrupacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ValidarMedioPago() As Boolean
        Get
            If ViewState("ValidarMedioPago") Is Nothing Then
                ViewState("ValidarMedioPago") = False
            End If
            Return ViewState("ValidarMedioPago")
        End Get
        Set(value As Boolean)
            ViewState("ValidarMedioPago") = value
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
    ''' Objeto cliente Faturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ClienteFaturacionSelecionado() As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteFaturacionSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteFaturacionSelecionado") = value
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
    Private Property PuntoServiciosSelecionados() As List(Of Negocio.Cliente)
        Get
            Return ViewState("PuntoServiciosSelecionados")
        End Get
        Set(value As List(Of Negocio.Cliente))
            ViewState("PuntoServiciosSelecionados") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda quem irá consumir o objeto cliente no retorno da busca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ChamadaConsomeCliente() As eChamadaConsomeCliente
        Get
            Return ViewState("ChamadaConsomeCliente")
        End Get
        Set(value As eChamadaConsomeCliente)
            ViewState("ChamadaConsomeCliente") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda a coleção de modalidades de recontagem
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ModalidadRecuento() As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion
        Get
            Return ViewState("ModalidadRecuento")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboModalidadesRecuento.ModalidadeRecuentoColeccion)
            ViewState("ModalidadRecuento") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de Agrupações 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ToleranciaAgrupaciones() As PantallaProceso.ToleranciaAgrupacionColeccion
        Get
            Return ViewState("ToleranciaAgrupaciones")
        End Get
        Set(value As PantallaProceso.ToleranciaAgrupacionColeccion)
            ViewState("ToleranciaAgrupaciones") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de Tolerância de Medios de Pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ToleranciaMedioPagos() As PantallaProceso.ToleranciaMedioPagoColeccion
        Get
            Return ViewState("ToleranciaMedioPagos")
        End Get
        Set(value As PantallaProceso.ToleranciaMedioPagoColeccion)
            ViewState("ToleranciaMedioPagos") = value
        End Set
    End Property

    ''' <summary>
    ''' Coleção de Términos de Médios de Pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property TerminosMedioPagos() As PantallaProceso.MedioPagoColeccion
        Get
            Return ViewState("TerminosMedioPagos")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            ViewState("TerminosMedioPagos") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias das agrupações.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property SessionToleranciaAgrupacion() As PantallaProceso.ToleranciaAgrupacionColeccion
        Get
            Return Session("ToleranciaAgrupacion")
        End Get
        Set(value As PantallaProceso.ToleranciaAgrupacionColeccion)
            Session("ToleranciaAgrupacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Contém a coleção de tolerancias dos médios pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 24/03/2009 Criado
    ''' </history>
    Private Property SessionToleranciaMedioPago() As PantallaProceso.ToleranciaMedioPagoColeccion
        Get
            Return Session("ToleranciaMedioPago")
        End Get
        Set(value As PantallaProceso.ToleranciaMedioPagoColeccion)
            Session("ToleranciaMedioPago") = value
        End Set
    End Property


    ''' <summary>
    ''' Contém a coleção de término dos médios pago.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 24/03/2009 Criado
    ''' </history>
    Private Property SessionTerminoMedioPago() As PantallaProceso.MedioPagoColeccion
        Get
            Return Session("objTerminoMedioPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            Session("objTerminoMedioPago") = value
        End Set
    End Property

    ''' <summary>
    ''' Contem a informação se é para validar os campos obrigatorios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [anselmo.gois] 03/07/2009 - Criado
    ''' </history>
    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

#End Region

#Region "[ÁRVORE BINÁRIA]"

    ''' <summary>
    ''' Copia o nó
    ''' </summary>
    ''' <param name="objNode">Nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyNode(objNode As TreeNode) As TreeNode

        Dim TempNode As New TreeNode
        TempNode.Text = objNode.Text
        TempNode.Value = objNode.Value
        TempNode.Selected = objNode.Selected
        TempNode.Expanded = objNode.Expanded
        TempNode.ImageUrl = objNode.ImageUrl
        TempNode.ToolTip = objNode.ToolTip

        Return TempNode

    End Function

    ''' <summary>
    ''' Retorna os filhos de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getChilds(objTreeNode As TreeNode) As TreeNode

        Dim objTreeNodeRetorno As TreeNode = CopyNode(objTreeNode)

        If objTreeNode.ChildNodes.Count > 0 Then
            Dim objFilhoRetorno As TreeNode
            For Each objFilho As TreeNode In objTreeNode.ChildNodes
                objFilhoRetorno = getChilds(objFilho)
                objTreeNodeRetorno.ChildNodes.Add(objFilhoRetorno)
            Next
        End If

        Return objTreeNodeRetorno

    End Function

    ''' <summary>
    ''' Retorna os páis do nó de um nó selecionado
    ''' </summary>
    ''' <param name="objTreeNode">No Selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getParent(ByRef objTreeNode As TreeNode)

        Dim objTreeNodeCorrente As TreeNode = CopyNode(objTreeNode)
        If objTreeNode.Parent IsNot Nothing Then
            Dim objPai As TreeNode = getParent(objTreeNode.Parent)
            objPai.ChildNodes.Add(objTreeNodeCorrente)
            Return objPai.ChildNodes(0)
        Else
            Return objTreeNodeCorrente
        End If

    End Function

    ''' <summary>
    ''' Retorna o nó selecionado de forma hierárquica
    ''' </summary>
    ''' <param name="pObjSelecionado">Objeto nó selecionado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaArvoreSelecionada(pObjSelecionado As TreeNode) As TreeNode

        'Se for o nível de raiz,inclui os filhos
        If pObjSelecionado.Depth = 0 Then

            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)
            Return objNoFilhos

        Else

            'Dado um objeto nó selecionado, retorna os pais e filhos a serem inseridos na coleção
            Dim objNoSelecionado As TreeNode = getParent(pObjSelecionado)
            Dim objNoFilhos As TreeNode = getChilds(pObjSelecionado)

            'Adiciona os filhos
            If objNoSelecionado IsNot Nothing Then
                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                While objTreeNodeChildCol.Count > 0
                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                End While
            End If

            'Suspende para o nível Root
            Dim objGetFather As TreeNode = objNoSelecionado
            While objGetFather.Parent IsNot Nothing
                objGetFather = objGetFather.Parent
            End While

            'Retorna o nó selecionado de forma hierárquica
            Return objGetFather
        End If

    End Function

    ''' <summary>
    ''' Remode o nó selecionado da Treeview informada
    ''' </summary>
    ''' <param name="pObjTreeView">Treevie a ser retirado o nó</param>
    ''' <remarks></remarks>
    Public Sub RemoveNode(ByRef pObjTreeView As TreeView)

        Dim objPai As TreeNode = pObjTreeView.SelectedNode.Parent
        Dim objDelete As TreeNode = pObjTreeView.SelectedNode

        While objPai IsNot Nothing
            objPai.ChildNodes.Remove(objDelete)

            If objPai.ChildNodes.Count = 0 Then
                objDelete = objPai
                objPai = objPai.Parent
            Else
                Exit While
            End If
        End While

        If objDelete IsNot Nothing Then
            pObjTreeView.Nodes.Remove(objDelete)
        End If

        If pObjTreeView.Nodes.Count = 0 Then
            ValidarMedioPago = True
        End If

    End Sub

    ''' <summary>
    ''' Posiciona o nó selecionado na árvore de destino
    ''' </summary>
    ''' <param name="pObjTreeView">Coleção de nós a ser verificada</param>
    ''' <param name="pObjSelecionado">Objeto selecionado(Hierárquico)</param>
    ''' <remarks></remarks>
    Public Sub InsereNaArvoreDinamica(pObjTreeView As TreeNodeCollection, pObjSelecionado As TreeNode)

        Dim objExiste As TreeNode = pObjSelecionado
        Dim ObjColCorrente As TreeNodeCollection = pObjTreeView

        'Caso não exista nenhum nó na árvore adiciona o primeiro
        If ObjColCorrente.Count = 0 Then
            'Por regra expande todos os nós
            objExiste.ExpandAll()

            ObjColCorrente.Add(objExiste)
            Exit Sub
        End If

        While objExiste IsNot Nothing

            Dim addNo As Boolean = True
            'Verifica na árvore de destino se o objeto selecionado existe
            For Each pObjSelecao As TreeNode In ObjColCorrente
                If pObjSelecao.Text = objExiste.Text Then
                    'Se existe filho então continua o processamento
                    If pObjSelecao.ChildNodes.Count > 0 Then
                        'Se selecionou um nó pai inclui novamente os filhos a partir da seleção efetuada
                        If objExiste.ChildNodes.Count > 0 AndAlso objExiste.Selected Then
                            pObjSelecao.ChildNodes.Clear()
                            Dim objNoSelecionado As TreeNode = pObjSelecao
                            'Seleciona o nó
                            objNoSelecionado.Selected = True

                            Dim objNoFilhos As TreeNode = getChilds(objExiste)
                            If objNoSelecionado IsNot Nothing Then
                                Dim objTreeNodeChildCol As TreeNodeCollection = objNoFilhos.ChildNodes
                                While objTreeNodeChildCol.Count > 0
                                    objNoSelecionado.ChildNodes.Add(objNoFilhos.ChildNodes(0))
                                End While
                            End If
                            Exit Sub
                        End If
                        'Passa o próximo filho do objeto selecionado para ser verificado
                        objExiste = objExiste.ChildNodes(0)
                        'Passa a próxima coleção da árvore de destino para ser verificada
                        ObjColCorrente = pObjSelecao.ChildNodes
                        addNo = False
                        Exit For
                    Else
                        'Seleciona o nó
                        pObjSelecao.Selected = True
                        Exit Sub
                    End If
                End If

            Next

            'Adiciona na árvore o nó
            If addNo Then
                'forma ordenada
                'ObjColCorrente.AddAt(IndiceOrdenacao(ObjColCorrente, objExiste), objExiste)

                'Por regra expande todos os nós
                objExiste.ExpandAll()

                'forma de inserção se ordenação
                ObjColCorrente.Add(objExiste)

                Exit While
            End If

        End While

    End Sub

    ''' <summary>
    ''' Retorna o índice antes de inserir o nó na coleção passada
    ''' </summary>
    ''' <param name="TreeNodeCol">Coleção a ser verificada a posição</param>
    ''' <param name="treenode">Nó para inclusão</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IndiceOrdenacao(TreeNodeCol As TreeNodeCollection, treenode As TreeNode) As Integer

        Dim treeNodeColTemp As New TreeNodeCollection

        For Each obj As TreeNode In TreeNodeCol
            treeNodeColTemp.Add(CopyNode(obj))
        Next

        If treeNodeColTemp.Count > 0 Then
            treeNodeColTemp.Add(CopyNode(treenode))

            Dim retorno = From objTree In treeNodeColTemp Order By objTree.Text Ascending

            Dim i As Integer = 0
            For Each objRetorno As Object In retorno
                If objRetorno.text = treenode.Text Then
                    Return i
                End If
                i += 1
            Next
        End If

        Return 0

    End Function

#End Region

#Region "[QUERY LINQ]"
    'Query correspondente em Sql do objeto Linq utilizado para
    'carregar a treeview de Medio Pago Posible no modo de modificação

    'SELECT DISTINCT TABA.CODISODIVISA,TABA.EFECTIVO,TABA.MEDIOPAGO
    '
    'FROM
    '        (select DISTINCT DP.CODISODIVISA,

    '          'TRUE'  as EFECTIVO,
    '
    '          case
    '
    '          when mp.CODTIPOMEDIOPAGO is null 
    '
    '          then
    '
    '              'FALSE'
    '
    '          else
    '
    '              'TRUE'
    '
    '          end
    '
    '          as MEDIOPAGO
    '
    '        from TEMPDivisaProceso dp
    '
    '                left outer join TEMPMedioPagoProceso mp on dp.CodIsoDivisa = mp.CodIsoDivisa
    '
    '        UNION ALL
    '
    '        select DISTINCT MP.CODISODIVISA,
    '
    '          case
    '
    '          when DP.CODISODIVISA is null 
    '
    '          then
    '
    '              'FALSE'
    '
    '          else
    '
    '              'TRUE'
    '
    '          end
    '
    '          as EFECTIVO,

    '              'TRUE'
    '
    '          as MEDIOPAGO
    '
    '        from  TEMPDivisaProceso dp
    '
    '              RIGHT outer join TEMPMedioPagoProceso mp on dp.CodIsoDivisa = mp.CodIsoDivisa) TABA

#End Region

#Region "[ENUM]"

    Public Enum eChamadaConsomeCliente As Integer
        BuscarCliente = 1
        BuscarClienteFaturacion = 2
    End Enum

#End Region


End Class

#Region "[CLASSES AUXILIARES]"

''' <summary>
''' Classe auxiliar desta página(Resultado)
''' </summary>
''' <remarks></remarks>
Public Class Resultado

    Private _codIso As String
    Private _Efectivo As Boolean
    Private _MedioPago As Boolean

    Public Property codIso() As String
        Get
            Return _codIso
        End Get
        Set(value As String)
            _codIso = value
        End Set
    End Property


    Public Property Efectivo() As String
        Get
            Return _Efectivo
        End Get
        Set(value As String)
            _Efectivo = value
        End Set
    End Property

    Public Property MedioPago() As String
        Get
            Return _MedioPago
        End Get
        Set(value As String)
            _MedioPago = value
        End Set
    End Property

End Class


''' <summary>
''' Classe auxiliar desta página(Coleção de Resultados)
''' </summary>
''' <remarks></remarks>
Public Class ResultadoCol
    Inherits List(Of Resultado)


End Class

#End Region