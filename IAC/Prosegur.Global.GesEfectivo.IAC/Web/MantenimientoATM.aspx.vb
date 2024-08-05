Imports Prosegur.Framework.Dicionario.Tradutor

''' <summary>
''' Página de Manutenção de ATMs
''' </summary>
''' <remarks></remarks>
''' <history>
''' [bruno.costa]  17/01/2011  criado
''' </history>
Partial Public Class MantenimientoATM
    Inherits Base

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    Protected Overrides Sub AdicionarScripts()

        ' configura campo data início
        'txtFecha.Attributes.Add("onKeyDown", "javascript:return dFilter(event.keyCode, this, '##/##/####');")
        'txtFecha.Attributes.Add("on", "javascript:return ValidarData(this,'data inválida');")
        'txtFecha.Attributes.Add("onkeypress", "javascript:return formatarData(this, event, 'dd/MM/yyyy','/');")
        txtFecha.Attributes.Add("onkeyup", "javascript:return ApenasData(this);")
        txtFecha.Attributes.Add("onblur", "javascript:validarData(this, 'dd/MM/yyyy', '" & Traduzir("gen_data_invalida") & "');")

        ' monta url do pop adicionar grupo
        Dim urlGrupo As String = "MantenimientoGrupo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&campoObrigatorio=True"

        ' adiciona JS para abrir pop adicionar grupo
        btnAddGrupo.FuncaoJavascript = "AbrirPopupModal('" & urlGrupo & "', 300, 550,'ATM');"

        'Controle de precedência(Ajax)
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnGrabar.ClientID & "';", True)

    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        hlpCliente.BtnTabIndex = 1
        hlpSubcliente.BtnTabIndex = 2
        hlpPuntoServicio.BtnTabIndex = 3
        ddlGrupo.TabIndex = 4
        btnAddGrupo.TabIndex = 5
        txtCodigo.TabIndex = 6
        ddlRedes.TabIndex = 7
        ddlModelo.TabIndex = 8
        chkRegistroTira.TabIndex = 9

        ' morfologia 
        ddlMorfologia.TabIndex = 10
        txtFecha.TabIndex = 11
        btnAddMorfologia.TabIndex = 13

        ' seta primeiro índice do grid morfologias
        Me.PrimeiroIndiceGridMorfologia = 14

        ' processo
        txtProceso.TabIndex = 100
        ddlProduto.TabIndex = 101
        ddlCanal.TabIndex = 102
        ddlSubcanal.TabIndex = 103
        ddlModalidad.TabIndex = 104
        ddlInfAdicional.TabIndex = 105
        hlpClienteFacturacion.BtnTabIndex = 106
        chkContarChequeTotales.TabIndex = 107
        chkContarTicketTotales.TabIndex = 108
        chkContarOtrosValoresTotales.TabIndex = 109
        chkContarTarjetasTotales.TabIndex = 110
        btnAddProceso.TabIndex = 112

        btnGrabar.TabIndex = 200
        btnCancelar.TabIndex = 200

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta

                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
                Master.PrimeiroControleTelaID = hlpCliente.BtnClientID

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                ' Define o foco para o botão de buscar cliente
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
                Master.PrimeiroControleTelaID = hlpCliente.BtnClientID

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                Master.PrimeiroControleTelaID = hlpCliente.BtnClientID
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

            Case eAcaoEspecifica.MantenerGrupo

                Master.PrimeiroControleTelaID = hlpCliente.BtnClientID
                Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)

        End Select

    End Sub

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

    Protected Overrides Sub Inicializar()

        Try

            If Not Page.IsPostBack Then

                If Not String.IsNullOrEmpty(Me.OidGrupo) Then
                    'Seta o estado da página corrente para mantener grupo
                    Acao = eAcaoEspecifica.MantenerGrupo
                End If

                'Possíveis Ações passadas pela página BusquedaDivisaes:
                ' [-] Aplicacao.Util.Utilidad.eAcao.Alta
                ' [-] Aplicacao.Util.Utilidad.eAcao.Modificacion
                ' [-] Aplicacao.Util.Utilidad.eAcao.Consulta
                ' [-] eAcaoEspecifica.MantenerGrupo

                If Not (MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse _
                        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse _
                        MyBase.Acao = eAcaoEspecifica.MantenerGrupo) Then

                    Acao = Aplicacao.Util.Utilidad.eAcao.Erro
                    'Informa ao usuário que o parâmetro passado são inválidos
                    Throw New Exception(Traduzir("err_passagem_parametro"))

                End If

                ' configura controles 
                ConfigurarControles()

                ' obtém dados e preenche controles da tela
                CarregaDados()

            End If

            ' verifica se foi selecionado cliente/subcliente/punto servicio
            VerificarRetornoCliente()
            VerificarFiltroSubCliente()
            VerificarFiltroPuntoServicio()

            ' verifica se foi adicionado algum grupo
            VerificarGrupos()

            ' verifica terminos de medio pago selecionados
            ConsomeTerminoMedioPago()

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

        ' títulos e sub-títulos
        Master.TituloPagina = Traduzir("023_titulo_mantenimiento")

        lblTituloATM.Text = Traduzir("023_subtitulo_atm")
        lblSubTituloMorfologias.Text = Traduzir("023_subtitulo_morfologias")
        lblSubtituloProcesos.Text = Traduzir("023_subtitulo_procesos")

        ' labels panel ATM
        lblCliente.Text = Traduzir("023_lbl_cliente")
        lblSubcliente.Text = Traduzir("023_lbl_subcliente")
        lblPuntoServicio.Text = Traduzir("023_lbl_puntoservicio")
        lblGrupo.Text = Traduzir("023_lbl_grupo")
        lblCodigo.Text = Traduzir("023_lbl_codigo")
        lblRed.Text = Traduzir("023_lbl_red")
        lblModelo.Text = Traduzir("023_lbl_modelo")
        lblRegistroTira.Text = Traduzir("023_lbl_registro_tira")

        ' labels panel morfologia
        lblDesMorfologia.Text = Traduzir("023_lbl_des_morfologia")
        lblFecha.Text = Traduzir("023_lbl_fecha_inicio")

        ' labels panel processos
        lblProceso.Text = Traduzir("023_lbl_proceso")
        lblProduto.Text = Traduzir("023_lbl_producto")
        lblCanal.Text = Traduzir("023_lbl_canal")
        lblSubcanal.Text = Traduzir("023_lbl_subcanal")
        lblModalidad.Text = Traduzir("023_lbl_modalidad")
        lblInfAdicional.Text = Traduzir("023_lbl_inf_adicional")
        lblClienteFacturacion.Text = Traduzir("023_lbl_cliente_facturacion")
        lblModoContagem.Text = Traduzir("023_lbl_modo_contaje")

        ' checkbox modo contagem
        chkContarChequeTotales.Text = Traduzir("023_chk_contar_cheque_totales")
        chkContarOtrosValoresTotales.Text = Traduzir("023_chk_contar_otros_totales")
        chkContarTarjetasTotales.Text = Traduzir("023_chk_tarjetas_totales")
        chkContarTicketTotales.Text = Traduzir("023_chk_contar_tickets_totales")

        ' grid procesos
        GdvProcesos.Columns(0).HeaderText = Traduzir("023_grd_proceso")
        GdvProcesos.Columns(1).HeaderText = Traduzir("023_grd_producto")
        GdvProcesos.Columns(2).HeaderText = Traduzir("023_grd_canal")
        GdvProcesos.Columns(3).HeaderText = Traduzir("023_grd_subcanal")
        GdvProcesos.Columns(4).HeaderText = Traduzir("023_grd_modalidad")
        GdvProcesos.Columns(5).HeaderText = Traduzir("023_grd_inf_adic")
        GdvProcesos.Columns(6).HeaderText = Traduzir("023_grd_cliente_fact")
        GdvProcesos.Columns(7).HeaderText = Traduzir("023_grd_modo_contage")
        GdvProcesos.Columns(8).HeaderText = Traduzir("023_grd_terminos")
        GdvProcesos.Columns(9).HeaderText = Traduzir("023_grd_borrar")

        ' grid morfologias
        GdvMorfologias.Columns(0).HeaderText = Traduzir("023_grd_des_morfologia")
        GdvMorfologias.Columns(1).HeaderText = Traduzir("023_grd_fec_inicio")
        GdvMorfologias.Columns(2).HeaderText = Traduzir("023_grd_vigencia")
        GdvMorfologias.Columns(3).HeaderText = Traduzir("023_grd_modificar")
        GdvMorfologias.Columns(4).HeaderText = Traduzir("023_grd_borrar")

        ' mensagens de erro
        ' ATM
        hlpCliente.Validator.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_cliente"))
        hlpSubcliente.Validator.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_subcliente"))
        hlpPuntoServicio.Validator.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_puntoservicio"))
        csvCodigoObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_codigo"))
        csvModeloObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_modelo"))
        ' morfologia
        csvMorfologiaObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_des_morfologia"))
        csvFechaObrigatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_fecha_inicio"))
        csvFechaInvalida.ErrorMessage = Traduzir("023_msg_fecha_invalida")
        ' proceso
        csvProcesoObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_proceso"))
        csvProdutoObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_producto"))
        csvCanalObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_canal"))
        csvSubcanalObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_subcanal"))
        csvModalidadObligatorio.ErrorMessage = String.Format(Traduzir("err_campo_obligatorio"), Traduzir("023_lbl_modalidad"))

    End Sub

#End Region

#Region "[DADOS]"

    Public Sub getDatosATM()

        If Not String.IsNullOrEmpty(Me.OidATM) Then

            If Me.ATM Is Nothing Then
                Me.ATM = New Negocio.ATM
            End If

            ' obtém detalhes do ATM
            Me.ATM.GetAtmDetail(Me.OidATM)

            ' verifica se houve erros
            If Not Master.ControleErro.VerificaErro(Me.ATM.Respuesta.CodigoError, Me.ATM.Respuesta.NombreServidorBD, Me.ATM.Respuesta.MensajeError) Then
                Exit Sub
            End If

        End If

    End Sub

    Public Sub getDatosGrupo()

        If Not String.IsNullOrEmpty(Me.OidGrupo) Then

            If Me.ATMXGrupo Is Nothing Then
                Me.ATMXGrupo = New Negocio.ATM
            End If

            ' obtém detalhes do ATM
            Me.ATMXGrupo.GetAtmDetail(String.Empty, Me.OidGrupo)

            ' verifica se houve erros
            If Not Master.ControleErro.VerificaErro(Me.ATMXGrupo.Respuesta.CodigoError, Me.ATMXGrupo.Respuesta.NombreServidorBD, Me.ATMXGrupo.Respuesta.MensajeError) Then
                Exit Sub
            End If

            ' obtém ATMs do grupo que está sendo modificado
            Dim objGrupo As New Negocio.Grupo

            ' guarda dados obtidos em memória
            Me.ATMsXGrupo = objGrupo.GetATMsByGrupo(Me.ATMXGrupo.Grupo.OidGrupo)

            ' verifica se houve erros
            If Not Master.ControleErro.VerificaErro(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, objGrupo.Respuesta.MensajeError) Then
                Exit Sub
            End If

        End If

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
        ExecutarGrabar()
    End Sub

    Private Sub btnAddMorfologia_Click(sender As Object, e As System.EventArgs) Handles btnAddMorfologia.Click

        ExecutarAddMorfologia()

    End Sub

    'Ações que podem ser chamadas a qualquer momento
#Region "Ações Botões Independentes"

    Public Sub ExecutarGrabarGrupo()

        ' seta ação
        Me.ATMXGrupo.Acao = Negocio.BaseEntidade.eAcao.Modificacion

        ' atualiza dados
        With Me.ATMXGrupo
            .BolVigente = True
            .BolRegistroTira = True 'chkRegistroTira.Checked (Aplicado mantis de melhoria 23339)
            .Grupo = New Negocio.Grupo(ddlGrupo.SelectedValue, String.Empty, ddlGrupo.SelectedItem.Text)
            .Red = New Negocio.Red(ddlRedes.SelectedValue, String.Empty, ddlRedes.SelectedItem.Text)
            .Modelo = New Negocio.ModeloCajero(ddlModelo.SelectedValue, String.Empty, ddlModelo.SelectedItem.Text)
        End With

        Me.ATMXGrupo.GuardarGrupo(MyBase.LoginUsuario, MyBase.DelegacionConectada.Keys(0), Me.ATMsXGrupo)

        ' trata erros
        If Master.ControleErro.VerificaErro(Me.ATMXGrupo.Respuesta.CodigoError, Me.ATMXGrupo.Respuesta.NombreServidorBD, Me.ATMXGrupo.Respuesta.MensajeError) Then
            Response.Redirect("~/BusquedaATM.aspx", False)
        Else
            Master.ControleErro.ShowError(Me.ATM.Respuesta.MensajeError, False)
        End If

    End Sub

    Public Sub ExecutarGrabarATM()

        ' seta ação
        Select Case Me.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion : Me.ATM.Acao = Negocio.BaseEntidade.eAcao.Modificacion
            Case Else : Me.ATM.Acao = Negocio.BaseEntidade.eAcao.Alta
        End Select

        ' atualiza dados

        With Me.ATM
            .BolVigente = True
            .BolRegistroTira = True  'chkRegistroTira.Checked (Aplicado mantis de melhoria 23339)
            .Grupo = New Negocio.Grupo(ddlGrupo.SelectedValue, String.Empty, ddlGrupo.SelectedItem.Text)
            .CodigoATM = txtCodigo.Text
            .Red = New Negocio.Red(ddlRedes.SelectedValue, String.Empty, ddlRedes.SelectedItem.Text)
            .Modelo = New Negocio.ModeloCajero(ddlModelo.SelectedValue, String.Empty, ddlModelo.SelectedItem.Text)
        End With

        ' se for alta e grupo estiver associado a outro ATM
        If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso Me.ATMXGrupo IsNot Nothing Then

            Me.ATM.CajeroXMorfologias = Me.ATMXGrupo.CajeroXMorfologias
            Me.ATM.Procesos = Me.ATMXGrupo.Procesos

        End If

        Me.ATM.Guardar(MyBase.LoginUsuario, MyBase.DelegacionConectada.Keys(0))

        ' trata erros
        If Master.ControleErro.VerificaErro(Me.ATM.Respuesta.CodigoError, Me.ATM.Respuesta.NombreServidorBD, Me.ATM.Respuesta.MensajeError) Then
            Response.Redirect("~/BusquedaATM.aspx", False)
        Else
            Master.ControleErro.ShowError(Me.ATM.Respuesta.MensajeError, False)
        End If

    End Sub

    ''' <summary>
    ''' Função do botão grabar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Public Sub ExecutarGrabar()

        Try

            ValidarCamposObrigatorios = True
            Me.ExibeErrosMorfologia = False
            Me.ExibeErrosProceso = False

            If MontaMensagensErro(True).Length > 0 Then
                Exit Sub
            End If

            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
                ExecutarGrabarGrupo()
            Else
                ExecutarGrabarATM()
            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Public Sub ExecutarModificarMorfologia()

        Try

            If String.IsNullOrEmpty(hidOidSelecionado.Value) Then
                Exit Sub
            End If

            ' obtém oid selecionado
            Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
            Dim fecInicio As String = hidOidSelecionado.Value.Split(";")(1)

            ' obtém cajeroxMorfologia selecionado
            Dim cajXMorf As Negocio.CajeroXMorfologia

            If Acao = eAcaoEspecifica.MantenerGrupo Then
                cajXMorf = (From obj In Me.ATMXGrupo.CajeroXMorfologias Where obj.OidMorfologia = OidMorfologia AndAlso obj.FecInicio.ToString("dd/MM/yyyy") = fecInicio).FirstOrDefault()
            Else
                cajXMorf = (From obj In Me.ATM.CajeroXMorfologias Where obj.OidMorfologia = OidMorfologia AndAlso obj.FecInicio.ToString("dd/MM/yyyy") = fecInicio).FirstOrDefault()
            End If

            If cajXMorf IsNot Nothing Then

                ' atualiza campos com dados da morfologia selecionada
                ddlMorfologia.Items.Clear()
                ddlMorfologia.Items.Add(New ListItem(cajXMorf.Morfologia.DesMorfologia, cajXMorf.Morfologia.OidMorfologia))
                ddlMorfologia.SelectedIndex = 0
                ddlMorfologia.Enabled = False

                txtFecha.Text = cajXMorf.FecInicio.ToString("dd/MM/yyyy")

            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Public Sub ExecutarBorrarMorfologia()

        Try

            Dim lista As New List(Of Negocio.CajeroXMorfologia)

            ' obtém oid selecionado
            Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
            Dim strFecInicio As String = hidOidSelecionado.Value.Split(";")(1)
            Dim fecInicio As New DateTime(strFecInicio.Substring(6, 4), strFecInicio.Substring(3, 2), strFecInicio.Substring(0, 2))

            ' exclui morfologia da lista
            lista.AddRange((From obj In Me.ATM.CajeroXMorfologias Where obj.FecInicio <> fecInicio).ToList())

            ' atualiza dados
            Me.ATM.CajeroXMorfologias = lista

            ' atualiza grid
            PreencherGridMorfologias()

            ' limpa memória
            hidOidSelecionado.Value = String.Empty
            hidAcaoMorfologia.Value = String.Empty

            If Me.ATM.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            Else
                HidTemMorfologia.Value = String.Empty
            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Public Sub ExecutarBorrarProceso()

        Try

            Dim proceso As Negocio.Proceso

            ' obtém proceso excluído
            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
                'proceso = (From obj In Me.ATMXGrupo.Procesos Where obj.OidProceso = hidOidProcesoSelecionado.Value).First()
                proceso = (From obj In Me.ATMXGrupo.Procesos Where obj.OidProceso = Me.OidProcesoSelecionado).First()
            Else
                'proceso = (From obj In Me.ATM.Procesos Where obj.OidProceso = hidOidProcesoSelecionado.Value).First()
                proceso = (From obj In Me.ATM.Procesos Where obj.OidProceso = Me.OidProcesoSelecionado).First()
            End If

            ' marca como excluído
            proceso.Acao = Negocio.BaseEntidade.eAcao.Baja

            ' atualiza grid
            PreencherGridProcesos()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Public Sub ExibirTerminos()

        Try

            ' setar url
            Dim url As String

            If Me.ATMXGrupo Is Nothing Then
                url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & MyBase.Acao
            Else
                ' se estiver selecionado um grupo que está associado a outro ATM
                url = "MantenimientoTerminoMedioPagoProcesso.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta
            End If

            ' accede a pantalla “Mantenimiento de Términos de Medios de  Pago”. 

            'Envia para popup medios de pago atualizados
            If Me.ATMXGrupo Is Nothing Then
                ' passa medios pagos do ATM

                'ParametrosTerminoMedioPago = Me.ATM.ObtenerTerminosMedioPago(hidOidProcesoSelecionado.Value)
                ParametrosTerminoMedioPago = Me.ATM.ObtenerTerminosMedioPago(Me.OidProcesoSelecionado)
            Else
                ' passa os medios pagos do grupo do ATM (ocorreu qdo usuário seleciona um grupo que já está associado a outros ATMs)
                'ParametrosTerminoMedioPago = Me.ATMXGrupo.ObtenerTerminosMedioPago(hidOidProcesoSelecionado.Value)
                ParametrosTerminoMedioPago = Me.ATMXGrupo.ObtenerTerminosMedioPago(Me.OidProcesoSelecionado)
            End If

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'ATMTerminos');", True)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Public Sub ExecutarAddMorfologia()

        Try

            If MontaMensagensErroMorfologia(True).Length > 0 Then

                Me.ValidarCamposObrigatorios = False
                Me.ExibeErrosMorfologia = True
                Exit Sub

            End If

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.CajeroXMorfologias Is Nothing Then
                    Me.ATMXGrupo.CajeroXMorfologias = New List(Of Negocio.CajeroXMorfologia)
                End If

            Else

                If Me.ATM.CajeroXMorfologias Is Nothing Then
                    Me.ATM.CajeroXMorfologias = New List(Of Negocio.CajeroXMorfologia)
                End If

            End If

            ' cria objeto morfologia
            Dim morfologia As New Negocio.Morfologia

            ' obtém detalhes da morfologia
            morfologia.getMorfologia(ddlMorfologia.SelectedValue)

            ' cria cajeroxmorfologia
            Dim cajXMorfologia As Negocio.CajeroXMorfologia

            If Acao = eAcaoEspecifica.MantenerGrupo Then
                cajXMorfologia = New Negocio.CajeroXMorfologia(Guid.NewGuid().ToString(), Me.ATMXGrupo.OidATM, txtFecha.Text, MyBase.LoginUsuario, Nothing, morfologia)
            Else
                cajXMorfologia = New Negocio.CajeroXMorfologia(Guid.NewGuid().ToString(), Me.ATM.OidATM, txtFecha.Text, MyBase.LoginUsuario, Nothing, morfologia)
            End If

            If String.IsNullOrEmpty(hidOidSelecionado.Value) Then

                ' inserção:

                ' seta ação
                cajXMorfologia.Acao = Negocio.BaseEntidade.eAcao.Alta

                ' adiciona cajeroxMorfologia a morfologia
                If Acao = eAcaoEspecifica.MantenerGrupo Then
                    Me.ATMXGrupo.CajeroXMorfologias.Add(cajXMorfologia)
                Else
                    Me.ATM.CajeroXMorfologias.Add(cajXMorfologia)
                End If

            Else

                ' atualização: 

                ' obtém oid selecionado
                Dim OidMorfologia As String = hidOidSelecionado.Value.Split(";")(0)
                Dim strFecInicio As String = hidOidSelecionado.Value.Split(";")(1)
                Dim fecInicio As New DateTime(strFecInicio.Substring(6, 4), strFecInicio.Substring(3, 2), strFecInicio.Substring(0, 2))

                ' obtém index da morfologia atualizada
                Dim index As Integer

                If Acao = eAcaoEspecifica.MantenerGrupo Then

                    For index = 0 To Me.ATMXGrupo.CajeroXMorfologias.Count - 1
                        If Me.ATMXGrupo.CajeroXMorfologias(index).OidMorfologia = OidMorfologia AndAlso _
                           Me.ATMXGrupo.CajeroXMorfologias(index).FecInicio = fecInicio Then
                            Exit For
                        End If
                    Next

                    If index < Me.ATMXGrupo.CajeroXMorfologias.Count Then
                        ' atualiza dados da morfologia
                        Me.ATMXGrupo.CajeroXMorfologias(index) = cajXMorfologia
                    End If

                Else

                    For index = 0 To Me.ATM.CajeroXMorfologias.Count - 1
                        If Me.ATM.CajeroXMorfologias(index).OidMorfologia = OidMorfologia AndAlso _
                           Me.ATM.CajeroXMorfologias(index).FecInicio = fecInicio Then
                            Exit For
                        End If
                    Next

                    If index < Me.ATM.CajeroXMorfologias.Count Then
                        ' atualiza dados da morfologia
                        Me.ATM.CajeroXMorfologias(index) = cajXMorfologia
                    End If

                End If

                ' limpa memória
                hidOidSelecionado.Value = String.Empty
                hidAcaoMorfologia.Value = String.Empty

            End If

            ' obtém morfologia vigente
            ObtenerMofologiaVigente()

            ' recarrega grid
            PreencherGridMorfologias()

            ' limpa os campos
            txtFecha.Text = String.Empty
            PreencherComboMorfologias()
            ddlMorfologia.Enabled = True

            VerificarGrupoAsignado()

            ' não valida mais controles do panel morfologia
            Me.ExibeErrosMorfologia = False

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then
                    HidTemMorfologia.Value = True
                Else
                    HidTemMorfologia.Value = String.Empty
                End If

            Else

                If Me.ATM.CajeroXMorfologias.Count > 0 Then
                    HidTemMorfologia.Value = True
                Else
                    HidTemMorfologia.Value = String.Empty
                End If

            End If


        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' função do botão cancelar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Public Sub ExecutarCancelar()

        Response.Redirect("~/BusquedaATM.aspx", False)

    End Sub

    Public Sub ExecutarAddProceso()

        Try

            If MontaMensagensErroProceso(True).Length > 0 Then

                Me.ValidarCamposObrigatorios = False
                Me.ExibeErrosProceso = True
                Me.ExibeErrosMorfologia = False
                Exit Sub

            End If

            If Acao = eAcaoEspecifica.MantenerGrupo Then

                If Me.ATMXGrupo.Procesos Is Nothing Then
                    Me.ATMXGrupo.Procesos = New List(Of Negocio.Proceso)
                End If

            Else

                If Me.ATM.Procesos Is Nothing Then
                    Me.ATM.Procesos = New List(Of Negocio.Proceso)
                End If

            End If

            ' cria objeto morfologia
            Dim proceso As New Negocio.Proceso

            ' preenche objeto
            With proceso
                .Acao = Negocio.BaseEntidade.eAcao.Alta
                ' id fictício, para seleção do grid apenas
                .OidProceso = Guid.NewGuid().ToString()
                .DesProceso = txtProceso.Text
                .Producto = New Negocio.Producto(String.Empty, ddlProduto.SelectedValue, ddlProduto.SelectedItem.Text)
                .Canal = New Negocio.Canal(String.Empty, ddlCanal.SelectedValue, ddlCanal.SelectedItem.Text)
                .Modalidad = New Negocio.Modalidad(String.Empty, ddlModalidad.SelectedValue, ddlModalidad.SelectedItem.Text)
                .BolContarChequesTotal = chkContarChequeTotales.Checked
                .BolContarOtrosTotal = chkContarOtrosValoresTotales.Checked
                .BolContarTarjetasTotal = chkContarTarjetasTotales.Checked
                .BolContarTicketsTotal = chkContarTicketTotales.Checked
                .BolVigente = True
            End With

            If Not String.IsNullOrEmpty(ddlSubcanal.SelectedValue) Then
                proceso.Canal.SubCanais.Add(New Negocio.SubCanal(String.Empty, ddlSubcanal.SelectedValue, ddlSubcanal.SelectedItem.Text))
            End If

            If Not String.IsNullOrEmpty(ddlInfAdicional.SelectedValue) Then
                proceso.IAC = New Negocio.InformacionAdicional(String.Empty, ddlInfAdicional.SelectedValue, ddlInfAdicional.SelectedItem.Text)
            End If

            If Me.ClienteFacturacion IsNot Nothing Then
                proceso.ClienteFacturacion = Me.ClienteFacturacion
            End If

            ' adiciona proceso
            If Acao = eAcaoEspecifica.MantenerGrupo Then
                Me.ATMXGrupo.Procesos.Add(proceso)
            Else
                Me.ATM.Procesos.Add(proceso)
            End If

            ' atualiza grid procesos
            PreencherGridProcesos()

            ' limpa os campos
            txtProceso.Text = String.Empty
            txtProceso.ToolTip = String.Empty

            ddlProduto.SelectedValue = String.Empty
            ddlProduto.ToolTip = String.Empty

            ddlCanal.SelectedValue = String.Empty
            ddlCanal.ToolTip = String.Empty

            ddlSubcanal.SelectedValue = String.Empty
            ddlSubcanal.ToolTip = String.Empty

            ddlModalidad.SelectedValue = String.Empty
            ddlModalidad.ToolTip = String.Empty

            ddlInfAdicional.SelectedValue = String.Empty
            ddlInfAdicional.ToolTip = String.Empty

            hlpClienteFacturacion.Limpar()
            chkContarChequeTotales.Checked = False
            chkContarOtrosValoresTotales.Checked = False
            chkContarTarjetasTotales.Checked = False
            chkContarTicketTotales.Checked = False

            ' não valida mais controles do panel morfologia
            Me.ExibeErrosProceso = False

            VerificarGrupoAsignado()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Função do botão volver.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Public Sub ExecutarVolver()

        Response.Redirect("~/BusquedaATM.aspx", False)

    End Sub

#End Region

#End Region

    Private Sub hlpClienteFacturacion_btnHelperClick(sender As Object, e As System.EventArgs) Handles hlpClienteFacturacion.btnHelperClick

        Try

            'Guarda quem irá consumir o objeto cliente no retorno
            ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarClienteFaturacion

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'ATMHelper');", True)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hlpCliente_btnHelperClick(sender As Object, e As System.EventArgs) Handles hlpCliente.btnHelperClick

        Try

            'Guarda quem irá consumir o objeto cliente no retorno
            ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarCliente

            Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"

            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

                ' se for modificar grupo:

                ' seta parámetro na query string
                url &= "&ExibeClientes=1"

                ' seta clientes do grupo
                Me.ClientesBusqueda = New ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
                Dim codigoCliente As String

                For Each objATM In Me.ATMsXGrupo

                    codigoCliente = objATM.Cliente.CodigoCliente

                    ' se ainda não adicionou cliente, adiciona
                    If (From cli In Me.ClientesBusqueda Where cli.Codigo = codigoCliente).FirstOrDefault() Is Nothing Then
                        Me.ClientesBusqueda.Add(objATM.Cliente.ConvertToComboCliente())
                    End If

                Next

            End If

            'AbrirPopupModal
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'hlpCliente');", True)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hlpSubcliente_btnHelperClick(sender As Object, e As System.EventArgs) Handles hlpSubcliente.btnHelperClick

        Try

            Dim url As String = String.Empty

            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

                url = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&vigente=True"

                If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

                    ' se for modificar grupo:

                    ' seta parámetro na query string
                    url &= "&ExibeSubclientes=1"

                    ' seta clientes do grupo
                    Me.SubClientesBusqueda = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion

                    Dim codigoSubcliente As String

                    For Each objATM In Me.ATMsXGrupo

                        For Each sc In objATM.Cliente.Subclientes

                            codigoSubcliente = sc.CodigoSubcliente

                            ' verifica se subcliente já foi adicionado
                            If (From subCli In Me.SubClientesBusqueda Where subCli.Codigo = codigoSubcliente).FirstOrDefault() Is Nothing Then
                                Me.SubClientesBusqueda.Add(sc.ConvertToComboSubCliente())
                            End If

                        Next

                    Next

                End If

            Else

                If Me.ATM.Cliente IsNot Nothing Then

                    'Seta o cliente selecionado para a PopUp
                    SetarClienteSelecionadoPopUp()

                    url = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&vigente=True"

                End If

            End If

            If Not String.IsNullOrEmpty(url) Then

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'hlpSubCliente');", True)

            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hlpPuntoServicio_btnHelperClick(sender As Object, e As System.EventArgs) Handles hlpPuntoServicio.btnHelperClick

        Try

            Dim url As String = String.Empty

            If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

                url = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&vigente=True"

                If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

                    ' se for modificar grupo:

                    ' seta parámetro na query string
                    url &= "&ExibePtosServico=1"

                    ' seta clientes do grupo
                    Me.PtosServicoCompleto = (From c In Me.ATMsXGrupo Select c.Cliente).ToList()

                End If

            Else

                If Me.ATM.Cliente IsNot Nothing _
                           AndAlso Me.ATM.Cliente.Subclientes IsNot Nothing _
                           AndAlso Me.ATM.Cliente.Subclientes.Count > 0 Then

                    'Seta o cliente selecionado para a PopUp
                    SetarClienteSelecionadoPopUp()

                    'Seta os subcliente selecionados para a PopUp
                    SetarSubClientesSelecionadoPopUp()

                    url = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&RetornaCodigoCompleto=1&BolATM=1&vigente=True"

                End If

            End If

            If Not String.IsNullOrEmpty(url) Then

                'AbrirPopupModal
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'hlpPuntoServicio');", True)

            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hlpPuntoServicio_LimparControles(sender As Object, e As System.EventArgs) Handles hlpPuntoServicio.LimparControles

        Try

            'Limpa os demais campos            
            hlpPuntoServicio.TextTextBox = String.Empty
            hlpPuntoServicio.ToolTipTextBox = String.Empty

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hlpSubcliente_LimparControles(sender As Object, e As System.EventArgs) Handles hlpSubcliente.LimparControles

        Try

            'Limpa os demais campos            
            hlpSubcliente.TextTextBox = String.Empty
            hlpSubcliente.ToolTipTextBox = String.Empty

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlGrupo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlGrupo.SelectedIndexChanged

        Try

            ' configura controles da tela de acordo com o grupo selecionado
            ConfigurarTelaXGrupo()

            Threading.Thread.Sleep(100)

            ddlGrupo.ToolTip = ddlGrupo.SelectedItem.Text

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlRedes_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlRedes.SelectedIndexChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

            ddlRedes.ToolTip = ddlRedes.SelectedItem.Text

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlModelo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModelo.SelectedIndexChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

            ddlModelo.ToolTip = ddlModelo.SelectedItem.Text

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlMorfologia_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlMorfologia.SelectedIndexChanged

        ddlMorfologia.ToolTip = ddlMorfologia.SelectedItem.Text

    End Sub

    Private Sub chkRegistroTira_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkRegistroTira.CheckedChanged

        Try

            VerificarGrupoAsignado()

            Threading.Thread.Sleep(100)

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hidOidSelecionado_ValueChanged(sender As Object, e As System.EventArgs) Handles hidOidSelecionado.ValueChanged

        Try

            If Not String.IsNullOrEmpty(hidOidSelecionado.Value) Then

                Select Case hidAcaoMorfologia.Value

                    Case "Modificar" : ExecutarModificarMorfologia()

                    Case "Borrar" : ExecutarBorrarMorfologia()

                End Select

            End If

            ' se é modificar e atm estava associado a um grupo
            VerificarGrupoAsignado()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub hidOidProcesoSelecionado_ValueChanged(sender As Object, e As System.EventArgs) Handles hidOidProcesoSelecionado.ValueChanged

        Try

            If Not String.IsNullOrEmpty(hidOidProcesoSelecionado.Value) Then

                'limpa hidden
                Me.OidProcesoSelecionado = hidOidProcesoSelecionado.Value.ToString
                hidOidProcesoSelecionado.Value = String.Empty

                Select Case hidAcaoProceso.Value

                    Case "Borrar" : ExecutarBorrarProceso()

                    Case "Terminos" : ExibirTerminos()

                End Select

            End If


            ' se é modificar e atm estava associado a um grupo
            VerificarGrupoAsignado()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub pgvMorfologias_EPreencheDados(sender As Object, e As System.EventArgs) Handles GdvMorfologias.EPreencheDados

        Try

            PreencherGridMorfologias()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' RowCreated do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProsegurGridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvMorfologias.RowCreated

        Try

            Select Case e.Row.RowType

                Case DataControlRowType.Header


            End Select

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_EPager_SetCss(sender As Object, e As System.EventArgs) Handles GdvProcesos.EPager_SetCss

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
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 12

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_EPreencheDados(sender As Object, e As System.EventArgs) Handles GdvProcesos.EPreencheDados

        Try

            PreencherGridProcesos()

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvProcesos.RowCreated

        Select Case e.Row.RowType

            Case DataControlRowType.Header

                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 120
                'CType(e.Row.Cells(0).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 121

                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 122
                'CType(e.Row.Cells(1).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 123

                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 124
                'CType(e.Row.Cells(2).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 125

                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 126
                'CType(e.Row.Cells(3).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 127

                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 128
                'CType(e.Row.Cells(4).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 129

                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 130
                'CType(e.Row.Cells(5).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 131

                'CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 132
                'CType(e.Row.Cells(6).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 133

                'CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(1).Controls(0), ImageButton).TabIndex = 134
                'CType(e.Row.Cells(7).Controls(0).Controls(0).Controls(1).Controls(1), ImageButton).TabIndex = 135

                ' seta primeiro índice do grid procesos
                Me.PrimeiroIndiceGridProcesos = 140

        End Select

    End Sub

    ''' <summary>
    ''' Configura o css do objeto pager do Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ProsegurGridView1_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvMorfologias.EPager_SetCss

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

    Private Sub pgvMorfologias_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvMorfologias.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                ' configura botões Modificar e Borrar
                ConfigurarBotoesGdvMorfologias(e.Row)

                ' verifica se é morfologia vigente está setada
                If Me.MorfologiaVigente IsNot Nothing Then

                    If Me.MorfologiaVigente.FecInicio = e.Row.DataItem("FecInicio").ToString() Then

                        ' morfologia vigente
                        e.Row.Cells(2).Text = Traduzir("023_col_vigente")

                    ElseIf Me.MorfologiaVigente.FecInicio > CType(e.Row.DataItem("FecInicio"), DateTime) Then

                        ' histórico
                        e.Row.Cells(2).Text = Traduzir("023_col_historico")

                    Else

                        ' futura
                        e.Row.Cells(2).Text = Traduzir("023_col_futura")

                    End If

                Else

                    ' futura
                    e.Row.Cells(2).Text = Traduzir("023_col_futura")

                End If

            End If

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub GdvProcesos_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvProcesos.RowDataBound

        Try

            Select Case e.Row.RowType

                Case DataControlRowType.DataRow

                    ' configura botões Modificar e Borrar
                    ConfigurarBotoesGdvProcesos(e.Row)

            End Select

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub btnAddProceso_Click(sender As Object, e As System.EventArgs) Handles btnAddProceso.Click

        ExecutarAddProceso()

    End Sub

    Private Sub ddlCanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCanal.SelectedIndexChanged

        Try

            If String.IsNullOrEmpty(ddlCanal.SelectedValue) Then
                ddlSubcanal.Items.Clear()
                ddlSubcanal.Enabled = False
                ddlSubcanal.ToolTip = String.Empty
            Else
                PreencherComboSubCanais()
                ddlSubcanal.Enabled = True
            End If

            Threading.Thread.Sleep(100)

            ddlCanal.ToolTip = ddlCanal.SelectedItem.Text

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlModalidad_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlModalidad.SelectedIndexChanged

        Try

            If String.IsNullOrEmpty(ddlModalidad.SelectedValue) Then

                ddlInfAdicional.Items.Clear()
                ddlInfAdicional.Enabled = False
                ddlInfAdicional.ToolTip = String.Empty
            Else

                If Me.Modalidades Is Nothing Then

                    ddlInfAdicional.Items.Clear()
                    ddlInfAdicional.Enabled = False
                    ddlInfAdicional.ToolTip = String.Empty
                    Exit Sub

                End If

                ' verifica se a modalidade selecionada admite IAC
                Dim objSelecionado As Negocio.Modalidad = (From obj In Me.Modalidades _
                                      Where obj.CodModalidad = ddlModalidad.SelectedValue).FirstOrDefault()

                If objSelecionado IsNot Nothing Then

                    ' verifica se admite IAC
                    If objSelecionado.AdmiteIAC Then

                        PreencherComboIAC()
                        ddlInfAdicional.Enabled = True

                    Else

                        ddlInfAdicional.Items.Clear()
                        ddlInfAdicional.Enabled = False
                        ddlInfAdicional.ToolTip = String.Empty
                    End If

                End If

            End If

            Threading.Thread.Sleep(100)

            ddlModalidad.ToolTip = ddlModalidad.SelectedItem.Text

        Catch ex As Exception

            Master.ControleErro.TratarErroException(ex)

        End Try

    End Sub

    Private Sub ddlProduto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlProduto.SelectedIndexChanged

        ddlProduto.ToolTip = ddlProduto.SelectedItem.Text
        ddlProduto.Focus()

    End Sub

    Private Sub ddlSubcanal_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSubcanal.SelectedIndexChanged

        ddlSubcanal.ToolTip = ddlSubcanal.SelectedItem.Text

    End Sub

    Private Sub ddlInfAdicional_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlInfAdicional.SelectedIndexChanged

        ddlInfAdicional.ToolTip = ddlInfAdicional.SelectedItem.Text

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Consome a sessão da tela de manutenção de terminos de medios de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsomeTerminoMedioPago()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        ' caso a sessão esteja preenchida
        If Me.ParametrosTerminoMedioPago IsNot Nothing Then

            If Me.ATMXGrupo Is Nothing Then
                ' atualiza medios pagos do ATM
                'Me.ATM.AtualizarTerminosMedioPagoXProceso(hidOidProcesoSelecionado.Value, Me.ParametrosTerminoMedioPago)
                Me.ATM.AtualizarTerminosMedioPagoXProceso(Me.OidProcesoSelecionado, Me.ParametrosTerminoMedioPago)
            Else
                ' atualiza os medios pagos do grupo do ATM (ocorre qdo usuário seleciona um grupo que já está associado a outros ATMs)
                'Me.ATMXGrupo.AtualizarTerminosMedioPagoXProceso(hidOidProcesoSelecionado.Value, Me.ParametrosTerminoMedioPago)
                Me.ATMXGrupo.AtualizarTerminosMedioPagoXProceso(Me.OidProcesoSelecionado, Me.ParametrosTerminoMedioPago)
            End If

            Me.ParametrosTerminoMedioPago = Nothing

        End If

    End Sub

    Private Sub ObtenerMofologiaVigente()


        If Me.ATMXGrupo IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then

            ' obtém morfologia vigente
            Me.MorfologiaVigente = (From obj In Me.ATMXGrupo.CajeroXMorfologias _
                                    Where obj.FecInicio <= DateTime.Now _
                                    Select obj _
                                    Order By obj.FecInicio Descending).FirstOrDefault()

        ElseIf Me.ATM IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0 Then

            ' obtém morfologia vigente
            Me.MorfologiaVigente = (From obj In Me.ATM.CajeroXMorfologias _
                                    Where obj.FecInicio <= DateTime.Now _
                                    Select obj _
                                    Order By obj.FecInicio Descending).FirstOrDefault()

        Else
            Me.MorfologiaVigente = Nothing
        End If


    End Sub

    Private Sub ConfigurarBotoesGdvMorfologias(ByRef Row As GridViewRow)

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty
        Dim podeAlterar As Integer
        Dim nomeHidSelecionado As String = hidOidSelecionado.ClientID
        Dim nomeHidAcao As String = hidAcaoMorfologia.ClientID
        Dim nomeGridMorfologias As String = GdvMorfologias.ClientID
        Dim IdentificadorFec As String = Row.DataItem("OidMorfologia").ToString() & ";" & CType(Row.DataItem("FecInicio"), DateTime).ToString("dd/MM/yyyy")

        ' verifica se pode alterar/borrar
        If Me.MorfologiaVigente Is Nothing Then
            podeAlterar = 1
        Else
            If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Row.DataItem("FecInicio") > Me.MorfologiaVigente.FecInicio Then
                podeAlterar = 1
            End If
        End If

        ' recupera imagebutton da coluna modificar
        Dim imbModificar As ImageButton = Row.Cells(3).Controls(1)

        ' recupera imagebutton da coluna borrar
        Dim imbBorrar As ImageButton = Row.Cells(4).Controls(1)

        If MyBase.Acao <> Aplicacao.Util.Utilidad.eAcao.Alta AndAlso Me.MorfologiaVigente IsNot Nothing AndAlso Me.MorfologiaVigente.FecInicio >= Row.DataItem("FecInicio").ToString() Then

            ' morfologia vigente, configura imagem dos botões
            imbBorrar.ImageUrl = "Imagenes/crossOFF.png"
            imbBorrar.Enabled = False
            imbModificar.ImageUrl = "Imagenes/pencilOFF.png"
            imbModificar.Enabled = False

        Else

            ' seta função javascript do modificar morfologia
            pbo = New PostBackOptions(imbModificar)
            s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
            imbModificar.Attributes.Add("onClick", "javascript:ModificarMorfologia('" & IdentificadorFec & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "'," & podeAlterar & ",'" & nomeGridMorfologias & "');")

            ' seta função javascript do modificar borrar
            pbo = New PostBackOptions(imbBorrar)
            s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
            imbBorrar.Attributes.Add("onClick", "javascript:if (BorrarMorfologia('" & IdentificadorFec & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "'," & podeAlterar & ",'" & nomeGridMorfologias & "','" & Traduzir("023_msg_modificar_borrar") & "')) " & s & ";")

            ' configura imagem dos botões
            imbBorrar.ImageUrl = "Imagenes/cross.PNG"
            imbModificar.ImageUrl = "Imagenes/pencil.PNG"

        End If

        ' configura tab index
        imbModificar.TabIndex = Me.PrimeiroIndiceGridMorfologia + Row.RowIndex + 1
        imbBorrar.TabIndex = imbModificar.TabIndex + 1

    End Sub

    Private Sub ConfigurarBotoesGdvProcesos(ByRef Row As GridViewRow)

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        Dim nomeHidSelecionado As String = hidOidProcesoSelecionado.ClientID
        Dim nomeHidAcao As String = hidAcaoProceso.ClientID
        Dim nomeHidTemMorf As String = HidTemMorfologia.ClientID
        Dim nomeGridMorfologias As String = GdvProcesos.ClientID
        Dim OidProceso As String = Row.DataItem("OidProceso").ToString()

        ' recupera imagebutton da coluna términos
        Dim imbTerminos As ImageButton = Row.Cells(8).Controls(1)
        pbo = New PostBackOptions(imbTerminos)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)

        ' seta função javascript do modificar borrar
        imbTerminos.Attributes.Add("onClick", "javascript:if (ExibirTerminos('" & OidProceso & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & nomeHidTemMorf & "','" & Traduzir("023_msg_006") & "')) {" & s & "};")

        ' recupera imagebutton da coluna borrar
        Dim imbBorrar As ImageButton = Row.Cells(9).Controls(1)
        pbo = New PostBackOptions(imbBorrar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)

        ' seta função javascript do modificar borrar
        imbBorrar.Attributes.Add("onClick", "javascript:if (BorrarProceso('" & OidProceso & "','" & nomeHidSelecionado & "','" & nomeHidAcao & "','" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "')) {" & s & "};")

        ' configura tab index
        imbTerminos.TabIndex = Me.PrimeiroIndiceGridProcesos + Row.RowIndex + 1
        imbBorrar.TabIndex = imbTerminos.TabIndex + 1

    End Sub

    ''' <summary>
    ''' Configura os controles da tela de acordo com o grupo selecionado.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/01/2011  criado
    ''' </history>
    Private Sub ConfigurarTelaXGrupo()

        If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then

            ' indica se grupo anterior estava associado a um ATM, limpa campos e habilita novamente
            Dim limpaHabilitaCampos As Boolean = False

            ' obtém dados dos ATMs associados ao grupo
            limpaHabilitaCampos = ObtenerATMXGrupo()

            ' preenche campos do grupo
            PreencherCamposGrupo(limpaHabilitaCampos)

            ' configura campos do grupo
            ConfigurarCamposGrupo(limpaHabilitaCampos)

            ' seta indicador "existe morfologia"
            If GdvMorfologias.Rows.Count > 0 Then
                HidTemMorfologia.Value = True
            End If

        End If

    End Sub

    ''' <summary>
    ''' Obtém dados dos ATMs associados ao grupo selecionado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <returns>True: indica se os campos devem ser limpos e habilitados (se o grupo anterior estava associado a um ATM
    '''  e o atual não está)</returns>
    ''' <history>
    ''' [bruno.costa]  27/01/2011  criado
    ''' </history>
    Private Function ObtenerATMXGrupo() As Boolean

        ' indica se os campos devem ser limpos e habilitados (se o grupo anterior estava associado a um ATM
        ' e o atual não está)
        Dim LimpaHabilitaCampos As Boolean = False
        Dim objATM As New Negocio.ATM

        If String.IsNullOrEmpty(ddlGrupo.SelectedValue) Then

            If Me.ATMXGrupo IsNot Nothing Then
                LimpaHabilitaCampos = True
            End If

            Me.ATMXGrupo = Nothing
            Return LimpaHabilitaCampos

        End If

        ' obtém oid do grupo selecionado
        Dim oidGrupo As String = ddlGrupo.SelectedValue

        ' obtém atms associados ao grupo
        objATM.GetAtmDetail(Nothing, oidGrupo)

        ' trata erros
        If Not Master.ControleErro.VerificaErro(objATM.Respuesta.CodigoError, objATM.Respuesta.NombreServidorBD, objATM.Respuesta.MensajeError) Then
            Master.ControleErro.ShowError(objATM.Respuesta.MensajeError, False)
        End If

        ' verificar se o grupo já está associado a um ATM
        If objATM.Grupo IsNot Nothing AndAlso objATM.Grupo IsNot Nothing _
        AndAlso Not String.IsNullOrEmpty(objATM.Grupo.CodGrupo) Then

            ' guarda em memória
            Me.ATMXGrupo = objATM

        Else

            If Me.ATMXGrupo IsNot Nothing Then
                LimpaHabilitaCampos = True
            End If

            Me.ATMXGrupo = Nothing

        End If

        Return LimpaHabilitaCampos

    End Function

    ''' <summary>
    ''' se grupo selecionado está associado a um ATM, preenche campos do grupo com os 
    ''' valores do ATM associado ao grupo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/01/2011  criado
    ''' </history>
    Private Sub PreencherCamposGrupo(LimpaCampos As Boolean)

        ' Caso el usuario seleccione un grupo y el mismo ya tenga al menos un cajero: 
        ' 1) preencher os panels morfologia e processos e configurá-los como somente leitura
        ' 2) rede, modelo, registrar tira ( também somente leitura)
        If Me.ATMXGrupo IsNot Nothing Then

            ddlRedes.SelectedValue = Me.ATMXGrupo.Red.OidRed
            ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            ddlModelo.SelectedValue = Me.ATMXGrupo.Modelo.OidModeloCajero
            ddlModelo.ToolTip = ddlModelo.SelectedItem.Text
            chkRegistroTira.Checked = Me.ATMXGrupo.BolRegistroTira

            ObtenerMofologiaVigente()

            ' preencher panel morfologias
            PreencherGridMorfologias()

            ' preencher panel procesos
            PreencherGridProcesos()

        ElseIf LimpaCampos Then

            ' se não está associado a um grupo que possui ATM mas o grupo anteior era associado, limpa campos

            ddlRedes.SelectedValue = String.Empty
            ddlRedes.ToolTip = String.Empty
            ddlModelo.SelectedValue = String.Empty
            ddlModelo.ToolTip = String.Empty
            chkRegistroTira.Checked = False

            ' limpar panel morfologias
            Me.ATM.CajeroXMorfologias = Nothing
            GdvMorfologias.Visible = False

            ' limpar panel procesos
            Me.ATM.Procesos = Nothing
            GdvProcesos.Visible = False

        End If

    End Sub

    ''' <summary>
    ''' se grupo selecionado está associado a um ATM, campos do grupo devem ser readonly
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  27/01/2011  criado
    ''' </history>
    Private Sub ConfigurarCamposGrupo(HabilitaCampos As Boolean)

        ' verifica se é para habilitar ou desabilitar campos
        Dim bolHabilitaCampos As Boolean = Me.ATMXGrupo Is Nothing

        If Not bolHabilitaCampos AndAlso HabilitaCampos Then
            ' se grupo não está associado ATM mas o grupo anteior estava, limpa campos
            bolHabilitaCampos = True
        End If

        ' Caso el usuario seleccione un grupo y el mismo ya tenga al menos un cajero: 
        ' 1) configurar os panels morfologia e processos como somente leitura
        ' 2) rede, modelo, registrar tira ( também somente leitura)

        ' se grupo não está associado a um ATM, habilita campos do grupo
        ddlRedes.Enabled = bolHabilitaCampos
        ddlModelo.Enabled = bolHabilitaCampos
        chkRegistroTira.Enabled = bolHabilitaCampos

        ' panel morfologias
        GdvMorfologias.Columns(3).Visible = bolHabilitaCampos
        GdvMorfologias.Columns(4).Visible = bolHabilitaCampos
        ddlMorfologia.Enabled = bolHabilitaCampos
        txtFecha.Enabled = bolHabilitaCampos
        btnAddMorfologia.Visible = bolHabilitaCampos

        ' panel procesos
        txtProceso.Enabled = bolHabilitaCampos
        ddlProduto.Enabled = bolHabilitaCampos
        ddlCanal.Enabled = bolHabilitaCampos
        ddlSubcanal.Enabled = bolHabilitaCampos
        ddlModalidad.Enabled = bolHabilitaCampos
        ddlInfAdicional.Enabled = bolHabilitaCampos
        hlpClienteFacturacion.BtnHabilitado = bolHabilitaCampos
        chkContarChequeTotales.Enabled = bolHabilitaCampos
        chkContarOtrosValoresTotales.Enabled = bolHabilitaCampos
        chkContarTarjetasTotales.Enabled = bolHabilitaCampos
        chkContarTicketTotales.Enabled = bolHabilitaCampos
        btnAddProceso.Visible = bolHabilitaCampos
        GdvProcesos.Columns(9).Visible = bolHabilitaCampos

    End Sub

    ''' <summary>
    ''' Configura controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  06/01/2011  criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' configura helpers
        hlpCliente.NomeSessionValor = "ClienteSelecionado"
        hlpClienteFacturacion.NomeSessionValor = "ClienteSelecionado"
        hlpSubcliente.NomeSessionValor = "SubClientesSelecionados"
        hlpSubcliente.NomeSessionLimpar = "LimparSubClienteSelecionado"
        hlpPuntoServicio.NomeSessionValor = "PtosServicoCompleto"
        hlpPuntoServicio.NomeSessionLimpar = "LimparPtosServicoCompleto"

        ' preenche combos
        PreencherComboRedes()
        PreencherComboModelosCajero()
        PreencherComboGrupos()
        PreencherComboMorfologias()
        PreencherComboProdutos()
        PreencherComboCanais()
        PreencherComboModalidad()

    End Sub

    ''' <summary>
    ''' verifica se usuário adicionou algum grupo e recarrega combo 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Sub VerificarGrupos()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If Me.AtualizaGrupo Then

            ' recarrega combo
            PreencherComboGrupos()

            ' limpa memória
            Me.AtualizaGrupo = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Trata filtro cliente, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 04/04/2011 Criado
    ''' </history>
    Private Sub VerificarRetornoCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If Me.ChamadaConsomeCliente = eChamadaConsomeCliente.BuscarCliente Then

            ConsomeCliente()

        Else

            ConsomeClienteFaturacion()

        End If

        ' configura controles
        If Me.ATM.Cliente Is Nothing OrElse _
        String.IsNullOrEmpty(Me.ATM.Cliente.CodigoCliente) Then

            hlpSubcliente.BtnHabilitado = False

        Else

            hlpSubcliente.BtnHabilitado = True

        End If

    End Sub

    Private Sub ConsomeCliente()

        If hlpCliente.ValorSelecionado IsNot Nothing Then

            Dim objCliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(hlpCliente.ValorSelecionado, ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ' guarda em memória
                Me.ATM.Cliente = New Negocio.Cliente(objCliente)

                ' setar controles da tela
                PreencherHlpCliente()

                'Limpa os demais campos
                hlpSubcliente.Limpar()
                hlpPuntoServicio.Limpar()

            End If

            hlpCliente.ValorSelecionado = Nothing

        End If

    End Sub

    Private Sub ConsomeClienteFaturacion()

        If hlpClienteFacturacion.ValorSelecionado IsNot Nothing Then

            Dim objCliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
            objCliente = TryCast(hlpClienteFacturacion.ValorSelecionado, ContractoServicio.Utilidad.GetComboClientes.Cliente)

            If objCliente IsNot Nothing Then

                ' guarda em memória
                Me.ClienteFacturacion = New Negocio.Cliente(objCliente)

                ' setar controles da tela
                PreencherHlpClienteFacturacion()

            End If

            hlpClienteFacturacion.ValorSelecionado = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Trata filtro cliente, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub VerificarFiltroSubCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If hlpSubcliente.ValorSelecionado IsNot Nothing Then

            Dim objSubClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
            objSubClientes = TryCast(hlpSubcliente.ValorSelecionado, ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)

            If objSubClientes IsNot Nothing Then

                ' guarda em memória
                Me.ATM.Cliente.Subclientes = Negocio.Subcliente.ConvertToList(objSubClientes)

                ' preenche helper
                PreencherHlpSubcliente()

                'Limpa os demais campos
                hlpPuntoServicio.Limpar()
                hlpPuntoServicio.BtnHabilitado = True

            End If

            hlpSubcliente.ValorSelecionado = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Trata filtro ponto de serviço, se informado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 06/01/2011 Criado
    ''' </history>
    Private Sub VerificarFiltroPuntoServicio()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            Exit Sub
        End If

        If hlpPuntoServicio.ValorSelecionado IsNot Nothing Then

            Dim objClientes As New List(Of Negocio.Cliente)
            objClientes = TryCast(hlpPuntoServicio.ValorSelecionado, List(Of Negocio.Cliente))

            If objClientes IsNot Nothing AndAlso objClientes.Count > 0 Then

                Me.ATM.Cliente = Me.PtosServicoCompleto(0)

            End If

            ' preenche helper
            PreencherHlpPtoServicio()

            hlpPuntoServicio.ValorSelecionado = Nothing

        End If

        ' configura controles
        If Me.ATM.Cliente Is Nothing OrElse Me.ATM.Cliente.Subclientes Is Nothing _
        OrElse Me.ATM.Cliente.Subclientes.Count = 0 Then
            hlpPuntoServicio.BtnHabilitado = False
        Else
            hlpSubcliente.BtnHabilitado = True
        End If

    End Sub

    ''' <summary>
    ''' Preenche campo helper cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  26/01/2011  criado
    ''' </history>
    Private Sub PreencherHlpCliente()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then
            
            If Me.ATMsXGrupo IsNot Nothing Then

                If Me.ATMsXGrupo.Count > 0 Then
                    hlpCliente.TextTextBox &= Me.ATMsXGrupo(0).Cliente.CodigoCliente & " - " & Me.ATMsXGrupo(0).Cliente.DesCliente & " ..."
                End If

                For Each objATM In Me.ATMsXGrupo
                    hlpCliente.ToolTipTextBox &= objATM.Cliente.CodigoCliente & " - " & objATM.Cliente.DesCliente & ";"
                Next

            End If

        Else

            If Me.ATM.Cliente IsNot Nothing Then

                hlpCliente.TextTextBox = Me.ATM.Cliente.CodigoCliente & " - " & Me.ATM.Cliente.DesCliente
                hlpCliente.ToolTipTextBox = Me.ATM.Cliente.CodigoCliente & " - " & Me.ATM.Cliente.DesCliente

            End If

        End If

    End Sub

    ''' <summary>
    ''' Preenche campo helper cliente facturación
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherHlpClienteFacturacion()

        hlpClienteFacturacion.ToolTipTextBox = String.Empty
        hlpClienteFacturacion.TextTextBox = String.Empty

        If Me.ClienteFacturacion IsNot Nothing Then

            hlpClienteFacturacion.TextTextBox = Me.ClienteFacturacion.CodigoCliente & " - " & Me.ClienteFacturacion.DesCliente
            hlpClienteFacturacion.ToolTipTextBox = Me.ClienteFacturacion.CodigoCliente & " - " & Me.ClienteFacturacion.DesCliente

        End If

    End Sub

    ''' <summary>
    ''' Preenche campo helper subcliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  26/01/2011  criado
    ''' </history>
    Private Sub PreencherHlpSubcliente()

        hlpSubcliente.ToolTipTextBox = String.Empty
        hlpSubcliente.TextTextBox = String.Empty

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMsXGrupo IsNot Nothing Then

                If Me.ATMsXGrupo.Count > 0 Then
                    hlpSubcliente.TextTextBox &= Me.ATMsXGrupo(0).Cliente.Subclientes(0).CodigoSubcliente & " - " & _
                        Me.ATMsXGrupo(0).Cliente.Subclientes(0).DesSubcliente & " ..."
                End If

                For Each objATM In Me.ATMsXGrupo
                    For Each subcli In objATM.Cliente.Subclientes
                        hlpSubcliente.ToolTipTextBox &= subcli.CodigoSubcliente & " - " & subcli.DesSubcliente & ";"
                    Next
                Next

            End If

        Else

            If Me.ATM.Cliente.Subclientes IsNot Nothing AndAlso Me.ATM.Cliente.Subclientes.Count > 0 Then

                ' setar controles da tela
                hlpSubcliente.TextTextBox = Me.ATM.Cliente.Subclientes(0).CodigoSubcliente & " - " & Me.ATM.Cliente.Subclientes(0).DesSubcliente & " ..."

                hlpSubcliente.ToolTipTextBox = String.Empty
                For Each subCliente As Negocio.Subcliente In Me.ATM.Cliente.Subclientes
                    hlpSubcliente.ToolTipTextBox &= subCliente.CodigoSubcliente & " - " & subCliente.DesSubcliente & ";"
                Next

            End If

        End If

        If Not String.IsNullOrEmpty(hlpSubcliente.ToolTipTextBox) Then

            hlpSubcliente.ToolTipTextBox = hlpSubcliente.ToolTipTextBox.Remove(hlpSubcliente.ToolTipTextBox.Length - 1, 1)

        End If

    End Sub

    ''' <summary>
    ''' Preenche campo helper pto serviço
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [blcosta]  26/01/2011  criado
    ''' </history>
    Private Sub PreencherHlpPtoServicio()

        Dim txtHlpPtoServicio As String = String.Empty

        ' limpa campos
        hlpPuntoServicio.ToolTipTextBox = String.Empty
        hlpPuntoServicio.TextTextBox = String.Empty

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMsXGrupo IsNot Nothing Then

                If Me.ATMsXGrupo.Count > 0 Then

                    ' configura txt do helper pto serviço
                    txtHlpPtoServicio = Me.ATMsXGrupo(0).Cliente.Subclientes(0).PtosServicio(0).CodigoPuntoServicio & " - " & _
                        Me.ATMsXGrupo(0).Cliente.Subclientes(0).PtosServicio(0).DesPuntoServicio & " ..."

                End If

                For Each objATM In Me.ATMsXGrupo

                    For Each subcli In objATM.Cliente.Subclientes

                        For Each pto As Negocio.PuntoServicio In subcli.PtosServicio

                            ' seta tooltip do helper pto serviço
                            hlpPuntoServicio.ToolTipTextBox &= pto.CodigoPuntoServicio & " - " & pto.DesPuntoServicio & ";"

                        Next

                    Next

                Next

                hlpPuntoServicio.TextTextBox = txtHlpPtoServicio

            End If

        Else

            If Me.ATM.Cliente IsNot Nothing AndAlso Me.ATM.Cliente.Subclientes IsNot Nothing Then

                For Each subcli In Me.ATM.Cliente.Subclientes

                    ' helper pto serviço
                    For Each pto As Negocio.PuntoServicio In subcli.PtosServicio

                        If String.IsNullOrEmpty(txtHlpPtoServicio) Then
                            ' configura txt do helper pto serviço
                            txtHlpPtoServicio = pto.CodigoPuntoServicio & " - " & pto.DesPuntoServicio & " ..."
                        End If

                        ' seta tooltip do helper pto serviço
                        hlpPuntoServicio.ToolTipTextBox &= pto.CodigoPuntoServicio & " - " & pto.DesPuntoServicio & ";"

                    Next

                Next

                hlpPuntoServicio.TextTextBox = txtHlpPtoServicio

            End If

        End If

        If Not String.IsNullOrEmpty(hlpPuntoServicio.ToolTipTextBox) Then

            hlpPuntoServicio.ToolTipTextBox = hlpPuntoServicio.ToolTipTextBox.Remove(hlpPuntoServicio.ToolTipTextBox.Length - 1, 1)

        End If

    End Sub

    ''' <summary>
    ''' Carrega controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Public Sub CarregaDados()

        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            ' se não for inclusão, obtém dados e preenche objeto de negócio 
            getDatosATM()
        ElseIf Acao = eAcaoEspecifica.MantenerGrupo Then
            ' se for modificar grupo, obtém dados do objeto de negócio grupo
            getDatosGrupo()
        Else
            ' se for inclusão: inicializa objeto de negócio
            Me.ATM = New Negocio.ATM
        End If

        ' obtém morfologia vigente
        ObtenerMofologiaVigente()

        ' seta indicador "existe morfologia"
        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            End If

        Else

            If Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0 Then
                HidTemMorfologia.Value = True
            End If

        End If

        'Preenche os controles do formulario
        If Acao = Aplicacao.Util.Utilidad.eAcao.Consulta OrElse Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion _
           OrElse Acao = eAcaoEspecifica.MantenerGrupo Then

            ' preenche panel ATM
            PreenchePanelATM()

            ' preenche grid morfologias
            PreencherGridMorfologias()

            ' preenche grid processos
            PreencherGridProcesos()

            ' verifica se o ATM elegido está asociado a um grupo
            If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion AndAlso _
                (Me.ATM IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.ATM.Grupo.OidGrupo)) Then

                ' seta flag, indicando que atm que está sendo modificado pertencia a um grupo
                Me.ATMPertenceGrupo = True

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Mensagem", "javascript:alert('" & Traduzir("023_msg_dejara_grupo") & "');", True)

            End If

        End If

    End Sub

    ''' <summary>
    ''' preenche panel ATM
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Sub PreenchePanelATM()

        ' preenche helpers
        PreencherHlpCliente()
        PreencherHlpSubcliente()
        PreencherHlpPtoServicio()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo Then

            If Me.ATMXGrupo.Grupo IsNot Nothing Then

                ddlGrupo.SelectedValue = Me.ATMXGrupo.Grupo.OidGrupo
                ddlGrupo.ToolTip = ddlGrupo.SelectedItem.Text
            End If

            If Me.ATMXGrupo.Red IsNot Nothing Then
                ddlRedes.SelectedValue = Me.ATMXGrupo.Red.OidRed
                ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            End If

            If Me.ATMXGrupo.Modelo IsNot Nothing Then
                ddlModelo.SelectedValue = Me.ATMXGrupo.Modelo.OidModeloCajero
                ddlModelo.ToolTip = ddlModelo.SelectedItem.Text
            End If

            chkRegistroTira.Checked = Me.ATMXGrupo.BolRegistroTira

        Else

            ' preenche outros campos
            txtCodigo.Text = Me.ATM.CodigoATM
            txtCodigo.ToolTip = IIf(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, Me.ATM.CodigoATM, String.Empty)

            If Me.ATM.Grupo IsNot Nothing Then

                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then

                    ' remove item selecionar
                    ddlGrupo.Items.RemoveAt(0)

                    ' insere o grupo do atm
                    ddlGrupo.Items.Add(New ListItem(Me.ATM.Grupo.DesGrupo, Me.ATM.Grupo.OidGrupo))

                    ' ordena
                    ddlGrupo.OrdenarPorDesc()

                    ' insert o item Selecionar
                    ddlGrupo.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

                End If

                ddlGrupo.SelectedValue = Me.ATM.Grupo.OidGrupo
                ddlGrupo.ToolTip = ddlGrupo.SelectedItem.Text

            End If

            If Me.ATM.Red IsNot Nothing Then
                ddlRedes.SelectedValue = Me.ATM.Red.OidRed
                ddlRedes.ToolTip = ddlRedes.SelectedItem.Text
            End If

            If Me.ATM.Modelo IsNot Nothing Then
                ddlModelo.SelectedValue = Me.ATM.Modelo.OidModeloCajero
                ddlModelo.ToolTip = ddlModelo.SelectedItem.Text
            End If

            chkRegistroTira.Checked = Me.ATM.BolRegistroTira

        End If

    End Sub

    ''' <summary>
    ''' preenche grid morfologias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Sub PreencherGridMorfologias()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo OrElse Me.ATM.CajeroXMorfologias IsNot Nothing Then

            If Not GdvMorfologias.Visible Then
                GdvMorfologias.Visible = True
            End If

            'Carrega GridView            
            Dim objDT As New DataTable

            If Me.ATMXGrupo Is Nothing Then
                objDT = GdvMorfologias.ConvertListToDataTable(Me.ATM.CajeroXMorfologias)
            Else
                objDT = GdvMorfologias.ConvertListToDataTable(Me.ATMXGrupo.CajeroXMorfologias)
            End If

            objDT.DefaultView.Sort = "FecInicio ASC"
            GdvMorfologias.CarregaControle(objDT)

        End If

    End Sub

    ''' <summary>
    ''' preenche grid processos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/03/2011  criado
    ''' </history>
    Private Sub PreencherGridProcesos()

        If Me.Acao = eAcaoEspecifica.MantenerGrupo OrElse Me.ATM.Procesos IsNot Nothing Then

            If Not GdvProcesos.Visible Then
                GdvProcesos.Visible = True
            End If

            'Carrega GridView            
            Dim objDT As New DataTable

            If Me.ATMXGrupo Is Nothing Then

                ' exibe somente os processos vigentes
                objDT = GdvProcesos.ConvertListToDataTable((From p In Me.ATM.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList())

            Else

                ' exibe somente os processos vigentes
                objDT = GdvProcesos.ConvertListToDataTable((From p In Me.ATMXGrupo.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList())

            End If

            If GdvProcesos.SortCommand.Equals(String.Empty) Then
                objDT.DefaultView.Sort = "DesProceso ASC"
            Else
                objDT.DefaultView.Sort = GdvProcesos.SortCommand
            End If

            GdvProcesos.CarregaControle(objDT)

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

            If ValidarCamposObrigatorios Then

                strErro.Append(MyBase.TratarCampoObrigatorio(hlpCliente, hlpCliente.Validator, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(hlpSubcliente, hlpSubcliente.Validator, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(hlpPuntoServicio, hlpPuntoServicio.Validator, SetarFocoControle, focoSetado))
                strErro.Append(MyBase.TratarCampoObrigatorio(ddlModelo, csvModeloObrigatorio, SetarFocoControle, focoSetado))

                ' Si el usuario elegir solo un punto de servicio, campo código es obligatorio. 
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta AndAlso _
                   Me.ATM IsNot Nothing AndAlso Me.ATM.Cliente IsNot Nothing AndAlso _
                   Me.ATM.Cliente.Subclientes IsNot Nothing AndAlso _
                  (Me.ATM.Cliente.Subclientes.Count = 1 AndAlso Me.ATM.Cliente.Subclientes(0).PtosServicio.Count = 1) Then

                    strErro.Append(MyBase.TratarCampoObrigatorio(txtCodigo, csvCodigoObrigatorio, SetarFocoControle, focoSetado))

                End If

                Dim bolTemMorfologia As Boolean
                Dim bolTemProceso As Boolean
                Dim procesos As List(Of Negocio.Proceso) = Nothing

                If (Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta OrElse Me.Acao = eAcaoEspecifica.MantenerGrupo) AndAlso Me.ATMXGrupo IsNot Nothing Then

                    ' grupo está associado a outro ATM
                    If Me.ATMXGrupo.Procesos IsNot Nothing Then
                        procesos = (From p In Me.ATMXGrupo.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList()
                    End If

                    bolTemMorfologia = Me.ATMXGrupo.CajeroXMorfologias IsNot Nothing AndAlso Me.ATMXGrupo.CajeroXMorfologias.Count > 0

                ElseIf Me.ATM IsNot Nothing Then

                    ' grupo não associado a um ATM
                    If Me.ATM.Procesos IsNot Nothing Then
                        procesos = (From p In Me.ATM.Procesos Where p.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso p.BolVigente).ToList()
                    End If

                    bolTemMorfologia = Me.ATM.CajeroXMorfologias IsNot Nothing AndAlso Me.ATM.CajeroXMorfologias.Count > 0

                End If

                bolTemProceso = procesos IsNot Nothing AndAlso procesos.Count > 0

                ' ATM deve estar associado a pelo menos uma morfologia e a um processo
                If Not bolTemMorfologia OrElse Not bolTemProceso Then

                    strErro.Append(Traduzir("023_morf_proc_obrig") & Aplicacao.Util.Utilidad.LineBreak)

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then

                        If Me.ATM.CajeroXMorfologias Is Nothing OrElse Me.ATM.CajeroXMorfologias.Count = 0 Then
                            ddlMorfologia.Focus()
                        End If

                        If Me.ATM.Procesos Is Nothing OrElse Me.ATM.Procesos.Count = 0 Then
                            txtProceso.Focus()
                        End If

                        focoSetado = True

                    End If

                End If

                ' se for alta e grupo estiver associado a outro ATM
                If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                    Dim proces As List(Of Negocio.Proceso) = Nothing
                    Dim molRep As Boolean = False
                    Dim subCanalRep As Boolean = False
                    proces = If(Me.ATMXGrupo IsNot Nothing, Me.ATMXGrupo.Procesos, Me.ATM.Procesos)
                    proces = proces.Where(Function(f) f.Acao <> Negocio.BaseEntidade.eAcao.Baja AndAlso f.BolVigente).ToList
                    For Each pr In proces
                        Dim prLocal = pr
                        If Not molRep AndAlso proces.AsQueryable().Count(Function(f) f.Modalidad.CodModalidad = prLocal.Modalidad.CodModalidad) > 1 Then
                            strErro.Append(Traduzir("023_msg_modalidad_repetida") & Aplicacao.Util.Utilidad.LineBreak)
                            molRep = True
                        End If
                        If Not subCanalRep AndAlso proces.AsQueryable().Count(Function(f) f.DesCanal = prLocal.DesCanal AndAlso f.DesSubCanal = prLocal.DesSubCanal) > 1 Then
                            strErro.Append(Traduzir("023_msg_subcanal_repetida") & Aplicacao.Util.Utilidad.LineBreak)
                            subCanalRep = True
                        End If
                    Next

                End If

            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' valida campos do panel morfologia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErroMorfologia(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            ' campos obrigatórios
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlMorfologia, csvMorfologiaObrigatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(txtFecha, csvFechaObrigatorio, SetarFocoControle, focoSetado))

            If csvFechaObrigatorio.IsValid Then

                ' se data não estiver vazio, valida se é uma data válida
                strErro.Append(MyBase.TratarDataInvalida(txtFecha, csvFechaInvalida, SetarFocoControle, focoSetado))

                If csvFechaInvalida.IsValid Then

                    Dim morfXATM
                    Dim arrSelecao As String() = hidOidSelecionado.Value.Split(";")
                    Dim arrFecha As String() = txtFecha.Text.Split("/")
                    Dim data As New DateTime(arrFecha(2), arrFecha(1), arrFecha(0))
                    Dim bolDuplicado As Boolean = False

                    'obtém morfologias com data igual a data informada
                    If Acao = eAcaoEspecifica.MantenerGrupo Then
                        morfXATM = (From m As Negocio.CajeroXMorfologia In Me.ATMXGrupo.CajeroXMorfologias _
                                    Where m.FecInicio.ToString("dd/MM/yyyy") = txtFecha.Text).ToList()
                    Else
                        morfXATM = (From m As Negocio.CajeroXMorfologia In Me.ATM.CajeroXMorfologias _
                                    Where m.FecInicio.ToString("dd/MM/yyyy") = txtFecha.Text).ToList()
                    End If

                    ' verifica se morfologia é a selecionada para modificação

                    If morfXATM.Count > 0 AndAlso (arrSelecao Is Nothing OrElse (arrSelecao.Count > 0 AndAlso String.IsNullOrEmpty(arrSelecao(0)))) Then

                        bolDuplicado = True

                    ElseIf morfXATM.Count = 1 AndAlso arrSelecao(1) <> morfXATM(0).FecInicio Then

                        bolDuplicado = True

                    End If

                    ' Validar si la fecha informada es inferior la fecha actual del sistema o si es igual la 
                    ' alguna fecha ya existente para las morfologías pertenecientes al ATM. 
                    If data < DateTime.Now.Date OrElse bolDuplicado Then

                        strErro.Append(csvFechaInvalida.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                        csvFechaInvalida.IsValid = False

                        'Seta o foco no primeiro controle que deu erro
                        If SetarFocoControle AndAlso Not focoSetado Then
                            txtFecha.Focus()
                            focoSetado = True
                        End If

                    Else
                        csvFechaInvalida.IsValid = True
                    End If

                End If

            End If

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' valida campos do panel processos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function MontaMensagensErroProceso(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            ' campos obrigatórios
            strErro.Append(MyBase.TratarCampoObrigatorio(txtProceso, csvProcesoObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlProduto, csvProdutoObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlCanal, csvCanalObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlSubcanal, csvSubcanalObligatorio, SetarFocoControle, focoSetado))
            strErro.Append(MyBase.TratarCampoObrigatorio(ddlModalidad, csvModalidadObligatorio, SetarFocoControle, focoSetado))

        End If

        Return strErro.ToString

    End Function

    ''' <summary>
    ''' Preenche combobox grupos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  25/01/2011  criado
    ''' </history>
    Private Sub PreencherComboGrupos()

        ddlGrupo.Items.Clear()

        Dim objGrupo As New Negocio.Grupo

        ' obtém grupos
        Dim grupos As List(Of Negocio.Grupo)

        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
            ' Se for modificação, obtém somente os grupos que não estão associados a um ATM
            grupos = objGrupo.ObtenerCombo(False)
        Else
            grupos = objGrupo.ObtenerCombo(True)
        End If

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objGrupo.Respuesta.CodigoError, objGrupo.Respuesta.NombreServidorBD, objGrupo.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlGrupo
            .AppendDataBoundItems = True
            .DataTextField = "CodigoDescripcion"
            .DataValueField = "OidGrupo"
            .DataSource = grupos
        End With

        ' popula combobox
        ddlGrupo.DataBind()

        ' ordena por descrição
        ddlGrupo.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlGrupo.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboProdutos()

        ddlProduto.Items.Clear()

        Dim objProduto As New Negocio.Producto

        ' obtém produtos
        Dim produtos As List(Of Negocio.Producto) = objProduto.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objProduto.Respuesta.CodigoError, objProduto.Respuesta.NombreServidorBD, objProduto.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlProduto
            .AppendDataBoundItems = True
            .DataTextField = "DesProducto"
            .DataValueField = "CodProducto"
            .DataSource = produtos
        End With

        ' popula combobox
        ddlProduto.DataBind()

        ' ordena por descrição
        ddlProduto.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlProduto.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboCanais()

        ddlCanal.Items.Clear()

        Dim objCanal As New Negocio.Canal

        ' obtém produtos
        Dim canais As List(Of Negocio.Canal) = objCanal.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objCanal.Respuesta.CodigoError, objCanal.Respuesta.NombreServidorBD, objCanal.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlCanal
            .AppendDataBoundItems = True
            .DataTextField = "DesCanal"
            .DataValueField = "CodCanal"
            .DataSource = canais
        End With

        ' popula combobox
        ddlCanal.DataBind()

        ' ordena por descrição
        ddlCanal.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlCanal.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboSubCanais()

        ddlSubcanal.Items.Clear()
        ddlSubcanal.ToolTip = String.Empty

        Dim objSubcanal As New Negocio.SubCanal

        ' obtém produtos
        Dim subcanais As List(Of Negocio.SubCanal) = objSubcanal.ObtenerCombo(ddlCanal.SelectedValue)

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objSubcanal.Respuesta.CodigoError, objSubcanal.Respuesta.NombreServidorBD, objSubcanal.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlSubcanal
            .AppendDataBoundItems = True
            .DataTextField = "DesSubcanal"
            .DataValueField = "CodSubcanal"
            .DataSource = subcanais
        End With

        ' popula combobox
        ddlSubcanal.DataBind()

        ' ordena por descrição
        ddlSubcanal.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlSubcanal.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox Produtos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  04/02/2011  criado
    ''' </history>
    Private Sub PreencherComboModalidad()

        ddlModalidad.Items.Clear()

        Dim objModalidad As New Negocio.Modalidad

        ' obtém produtos
        Dim modalidades As List(Of Negocio.Modalidad) = objModalidad.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objModalidad.Respuesta.CodigoError, objModalidad.Respuesta.NombreServidorBD, objModalidad.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' guarda na viewstate
        Me.Modalidades = modalidades

        ' configura combobox
        With ddlModalidad
            .AppendDataBoundItems = True
            .DataTextField = "DesModalidad"
            .DataValueField = "CodModalidad"
            .DataSource = modalidades
        End With

        ' popula combobox
        ddlModalidad.DataBind()

        ' ordena por descrição
        ddlModalidad.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlModalidad.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

        ' desabilita o combobox Inf.Adicional
        ddlInfAdicional.Enabled = False

        'desabilita o combo SubCanal
        ddlSubcanal.Enabled = False

    End Sub

    ''' <summary>
    ''' Preenche combobox IAC
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  07/02/2011  criado
    ''' </history>
    Private Sub PreencherComboIAC()

        ddlInfAdicional.Items.Clear()
        ddlInfAdicional.ToolTip = String.Empty

        Dim objIAC As New Negocio.InformacionAdicional

        ' obtém produtos
        Dim IACs As List(Of Negocio.InformacionAdicional) = objIAC.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objIAC.Respuesta.CodigoError, objIAC.Respuesta.NombreServidorBD, objIAC.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlInfAdicional
            .AppendDataBoundItems = True
            .DataTextField = "DesIAC"
            .DataValueField = "CodIAC"
            .DataSource = IACs
        End With

        ' popula combobox
        ddlInfAdicional.DataBind()

        ' ordena por descrição
        ddlInfAdicional.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlInfAdicional.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox modelos de cajero
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  25/01/2011  criado
    ''' </history>
    Private Sub PreencherComboModelosCajero()

        ddlModelo.Items.Clear()

        Dim objModelo As New Negocio.ModeloCajero

        ' obtém modelos de cajero
        Dim modelos As List(Of Negocio.ModeloCajero) = objModelo.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objModelo.Respuesta.CodigoError, objModelo.Respuesta.NombreServidorBD, objModelo.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlModelo
            .AppendDataBoundItems = True
            .DataTextField = "DesModeloCajero"
            .DataValueField = "OidModeloCajero"
            .DataSource = modelos
        End With

        ' popula combobox
        ddlModelo.DataBind()

        ' ordena por descrição
        ddlModelo.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlModelo.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox morfologias
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Sub PreencherComboMorfologias()

        ddlMorfologia.Items.Clear()

        Dim objMorfologia As New Negocio.Morfologia

        ' obtém morfologias
        Dim morfologias As List(Of Negocio.Morfologia) = objMorfologia.ObtenerCombo(True)

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objMorfologia.Respuesta.CodigoError, objMorfologia.Respuesta.NombreServidorBD, objMorfologia.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlMorfologia
            .AppendDataBoundItems = True
            .DataTextField = "DesMorfologia"
            .DataValueField = "OidMorfologia"
            .DataSource = morfologias
        End With

        ' popula combobox
        ddlMorfologia.DataBind()

        ' ordena por descrição
        ddlMorfologia.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlMorfologia.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Preenche combobox redes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  25/01/2011  criado
    ''' </history>
    Private Sub PreencherComboRedes()

        ddlRedes.Items.Clear()

        Dim objRed As New Negocio.Red

        ' obtém redes disponíveis
        Dim redes As List(Of Negocio.Red) = objRed.ObtenerCombo()

        ' trata erros do serviço
        If Not Master.ControleErro.VerificaErro(objRed.Respuesta.CodigoError, objRed.Respuesta.NombreServidorBD, objRed.Respuesta.MensajeError) Then
            Exit Sub
        End If

        ' configura combobox
        With ddlRedes
            .AppendDataBoundItems = True
            .DataTextField = "DesRed"
            .DataValueField = "OidRed"
            .DataSource = redes
        End With

        ' popula combobox
        ddlRedes.DataBind()

        ' ordena por descrição
        ddlRedes.OrdenarPorDesc()

        ' insert o item Selecionar
        ddlRedes.Items.Insert(0, New ListItem(Traduzir("gen_ddl_selecione"), String.Empty))

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Public Sub SetarClienteSelecionadoPopUp()

        If Me.ATM.Cliente IsNot Nothing Then

            Dim ClienteUtilidad As New ContractoServicio.Utilidad.GetComboClientes.Cliente

            With ClienteUtilidad
                .Codigo = Me.ATM.Cliente.CodigoCliente
                .Descripcion = Me.ATM.Cliente.DesCliente
            End With

            Session("objCliente") = ClienteUtilidad

        End If

    End Sub

    ''' <summary>
    ''' Envia o cliente selecionado para a PopUp
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Public Sub SetarSubClientesSelecionadoPopUp()

        If Me.ATM.Cliente.Subclientes IsNot Nothing AndAlso _
        Me.ATM.Cliente.Subclientes.Count > 0 Then

            Session("objSubClientes") = Negocio.Subcliente.ConvertToSubClienteColeccion(Me.ATM.Cliente.Subclientes)

        End If

    End Sub

    ''' <summary>
    ''' função que verifica se o atm que está sendo moficiado estava associado a um grupo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub VerificarGrupoAsignado()

        ' se é modificar e atm estava associado a um grupo
        If Me.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion AndAlso Me.ATMPertenceGrupo Then

            ' deseleciona grupo
            ddlGrupo.SelectedValue = String.Empty
            ddlGrupo.ToolTip = String.Empty

            Me.ATMPertenceGrupo = False

        End If

    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    Public Sub ControleBotoes()

        Select Case MyBase.Acao

            Case Aplicacao.Util.Utilidad.eAcao.Alta

                ' botões atm
                btnVolver.Visible = False
                btnGrabar.Visible = True
                btnCancelar.Visible = True

                ' campos atm
                btnAddGrupo.Visible = True

                'Estado Inicial campos
                chkRegistroTira.Checked = True
                hlpCliente.BtnHabilitado = True

                ' foco inicial
                hlpCliente.BtnHelperFocus()

                If Me.ATM Is Nothing OrElse Me.ATM.Cliente Is Nothing OrElse Me.ATM.Cliente.Subclientes Is Nothing _
                   OrElse Me.ATM.Cliente.Subclientes.Count <> 1 OrElse _
                  (Me.ATM.Cliente.Subclientes(0).PtosServicio IsNot Nothing AndAlso Me.ATM.Cliente.Subclientes(0).PtosServicio.Count = 1) Then
                    txtCodigo.Enabled = True
                Else
                    txtCodigo.Enabled = False
                End If

            Case Aplicacao.Util.Utilidad.eAcao.Consulta

                ' campos atm
                hlpCliente.BtnHabilitado = False
                hlpSubcliente.BtnHabilitado = False
                hlpPuntoServicio.BtnHabilitado = False
                ddlGrupo.Enabled = False
                txtCodigo.Enabled = False
                ddlRedes.Enabled = False
                ddlModelo.Enabled = False
                btnAddGrupo.Visible = False
                chkRegistroTira.Enabled = False

                ' morfologias
                ddlMorfologia.Enabled = False
                txtFecha.Enabled = False
                GdvMorfologias.Columns(3).Visible = False
                GdvMorfologias.Columns(4).Visible = False
                btnAddMorfologia.Visible = False

                ' processos
                txtProceso.Enabled = False
                ddlProduto.Enabled = False
                ddlCanal.Enabled = False
                ddlSubcanal.Enabled = False
                ddlModalidad.Enabled = False
                ddlInfAdicional.Enabled = False
                hlpClienteFacturacion.BtnHabilitado = False
                chkContarChequeTotales.Enabled = False
                chkContarOtrosValoresTotales.Enabled = False
                chkContarTarjetasTotales.Enabled = False
                chkContarTicketTotales.Enabled = False
                btnAddProceso.Visible = False
                GdvProcesos.Columns(9).Visible = False

                ' botões tela
                btnGrabar.Visible = False
                btnCancelar.Visible = False
                btnVolver.Visible = True

            Case Aplicacao.Util.Utilidad.eAcao.Modificacion

                ' campos atm
                hlpCliente.BtnHabilitado = False
                hlpSubcliente.BtnHabilitado = False
                hlpPuntoServicio.BtnHabilitado = False

                ' botões atm
                btnGrabar.Visible = True
                btnCancelar.Visible = True
                btnVolver.Visible = False

            Case eAcaoEspecifica.MantenerGrupo

                ' campos atm
                ddlGrupo.Enabled = False
                btnAddGrupo.Habilitado = False
                txtCodigo.Enabled = False

                ' botões atm 

            Case Aplicacao.Util.Utilidad.eAcao.Erro

                ' botões atm
                btnGrabar.Visible = False        '2
                btnCancelar.Visible = False      '3
                btnVolver.Visible = True         '7

        End Select

        'Caso algum dos campos estejam com erro
        'então continua exibindo a mensagem de erro
        Dim msgerror As String = MontaMensagensErro()

        If Not String.IsNullOrEmpty(msgerror) Then

            Master.ControleErro.ShowError(msgerror, False)

        ElseIf Me.ExibeErrosMorfologia Then

            Master.ControleErro.ShowError(MontaMensagensErroMorfologia(), False)

        ElseIf Me.ExibeErrosProceso Then

            Master.ControleErro.ShowError(MontaMensagensErroProceso, False)

        End If

    End Sub

#End Region

#Region "[ENUMERAÇÕES]"

    Private Enum eAcaoEspecifica As Integer
        MantenerGrupo = 20
    End Enum

    Public Enum eChamadaConsomeCliente As Integer
        BuscarCliente = 1
        BuscarClienteFaturacion = 2
    End Enum

#End Region

#Region "[PROPRIEDADES]"

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    ''' <summary>
    ''' informa se a ação corrente é mantenimiento grupos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private ReadOnly Property OidGrupo() As String
        Get
            Return Request.QueryString("OidGrupo")
        End Get
    End Property

    ''' <summary>
    ''' retorna oid ATM
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private ReadOnly Property OidATM() As String
        Get
            Return Request.QueryString("OidATM")
        End Get
    End Property

    ''' <summary>
    ''' entidade mantida pela tela
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  17/01/2011  criado
    ''' </history>
    Private Property ATM() As Negocio.ATM
        Get
            Return ViewState("objATM")
        End Get
        Set(value As Negocio.ATM)
            ViewState("objATM") = value
        End Set
    End Property

    ''' <summary>
    ''' Dados do grupo. Utilizando quando usuário está criando ATM e seleciona um grupo que já está associado
    ''' a outro ATM ou na modificação de um grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  26/01/2011  criado
    ''' </history>
    Private Property ATMXGrupo() As Negocio.ATM
        Get
            Return ViewState("ATMXGrupo")
        End Get
        Set(value As Negocio.ATM)
            ViewState("ATMXGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' ATMs do grupo que está sendo modificado. Utilizado na modificação de um grupo.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  16/02/2011  criado
    ''' </history>
    Private Property ATMsXGrupo As List(Of Negocio.ATM)
        Get
            If ViewState("ATMsXGrupo") Is Nothing Then
                ViewState("ATMsXGrupo") = New List(Of Negocio.ATM)
            End If
            Return ViewState("ATMsXGrupo")
        End Get
        Set(value As List(Of Negocio.ATM))
            ViewState("ATMsXGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' indica se o combo grupo deve ser recarregado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  28/01/2011  criado
    ''' </history>
    Private Property AtualizaGrupo() As Boolean
        Get
            If Session("AtualizaGrupo") Is Nothing Then
                Return False
            Else
                Return Session("AtualizaGrupo")
            End If
        End Get
        Set(value As Boolean)
            Session("AtualizaGrupo") = value
        End Set
    End Property

    Private Property MorfologiaVigente() As Negocio.CajeroXMorfologia
        Get
            Return ViewState("MorfologiaVigente")
        End Get
        Set(value As Negocio.CajeroXMorfologia)
            ViewState("MorfologiaVigente") = value
        End Set
    End Property

    Private Property ClienteFacturacion As Negocio.Cliente
        Get

            If ViewState("ClienteFaturacion") Is Nothing Then
                ViewState("ClienteFaturacion") = New Negocio.Cliente()
            End If

            Return ViewState("ClienteFaturacion")

        End Get
        Set(value As Negocio.Cliente)

            ViewState("ClienteFaturacion") = value

        End Set
    End Property

    ''' <summary>
    ''' Guarda quem irá consumir o objeto cliente no retorno do popup modal "buscar clientes"
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

    Public Property Modalidades As List(Of Negocio.Modalidad)
        Get
            Return ViewState("Modalidades")
        End Get
        Set(value As List(Of Negocio.Modalidad))
            ViewState("Modalidades") = value
        End Set
    End Property

    Public Property ExibeErrosMorfologia As Boolean
        Get
            If ViewState("ExibeErrosMorfologia") Is Nothing Then
                Return False
            Else
                Return ViewState("ExibeErrosMorfologia")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ExibeErrosMorfologia") = value
        End Set
    End Property

    Public Property ExibeErrosProceso As Boolean
        Get
            If ViewState("ExibeErrosProceso") Is Nothing Then
                Return False
            Else
                Return ViewState("ExibeErrosProceso")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ExibeErrosProceso") = value
        End Set
    End Property

    Public Property PrimeiroIndiceGridMorfologia As Integer
        Get
            Return ViewState("PrimeiroIndiceGridMorfologia")
        End Get
        Set(value As Integer)
            ViewState("PrimeiroIndiceGridMorfologia") = value
        End Set
    End Property

    Public Property PrimeiroIndiceGridProcesos As Integer
        Get
            Return ViewState("PrimeiroIndiceGridProcesos")
        End Get
        Set(value As Integer)
            ViewState("PrimeiroIndiceGridProcesos") = value
        End Set
    End Property

    ''' <summary>
    ''' Parametros do popup términos de médio pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 08/02/2011 Criado
    ''' </history>
    Private Property ParametrosTerminoMedioPago() As PantallaProceso.MedioPagoColeccion
        Get
            Return Session("objTerminoMedioPago")
        End Get
        Set(value As PantallaProceso.MedioPagoColeccion)
            Session("objTerminoMedioPago") = value
        End Set
    End Property

    Private Property ATMPertenceGrupo As Boolean
        Get
            If ViewState("ATMPertenceGrupo") Is Nothing Then
                Return False
            Else
                Return ViewState("ATMPertenceGrupo")
            End If
        End Get
        Set(value As Boolean)
            ViewState("ATMPertenceGrupo") = value
        End Set
    End Property

    ''' <summary>
    ''' clientes a serem exibidos no popup de busca de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ClientesBusqueda As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion
        Get
            Return Session("ClientesConsulta")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
            Session("ClientesConsulta") = value
        End Set
    End Property

    ''' <summary>
    ''' subclientes a serem exibidos no popup de busca de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 Criado
    ''' </history>
    Private Property SubClientesBusqueda() As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return Session("SubClientesBusqueda")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            Session("SubClientesBusqueda") = value
        End Set
    End Property

    ''' <summary>
    ''' subclientes a serem exibidos no popup de busca de ptos de serviço
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa] 16/02/2011 Criado
    ''' </history>
    Private Property PtosServicoCompleto() As List(Of Negocio.Cliente)
        Get
            Return Session("PtosServicoCompleto")
        End Get
        Set(value As List(Of Negocio.Cliente))
            Session("PtosServicoCompleto") = value
        End Set
    End Property

    Private Property OidProcesoSelecionado As String
        Get
            Return ViewState("OidProcesoSelecionado")
        End Get
        Set(value As String)
            ViewState("OidProcesoSelecionado") = value
        End Set
    End Property

#End Region

End Class