Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports Prosegur.Genesis.Comunicacion
Imports DevExpress.Web.ASPxPager.ASPxPager

Public Class ReportarPedidoBCP
    Inherits Base

#Region "[PROPRIEDADES]"

    '''<summary>
    '''Master property.
    '''</summary>
    '''<remarks>
    '''Auto-generated property.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As Prosegur.[Global].GesEfectivo.Reportes.Web.Principal
        Get
            Return CType(MyBase.Master, Prosegur.[Global].GesEfectivo.Reportes.Web.Principal)
        End Get
    End Property

    'Private Property ValidarCamposObrigatorios() As Boolean
    '    Get
    '        Return If(ViewState("ValidarCamposObrigatorios") IsNot Nothing AndAlso ViewState("ValidarCamposObrigatorios") <> "", ViewState("ValidarCamposObrigatorios"), False)
    '    End Get
    '    Set(value As Boolean)
    '        ViewState("ValidarCamposObrigatorios") = value
    '    End Set
    'End Property

    Private Property DtPedidos() As DataTable
        Get
            Return If(ViewState("DtPedidos") IsNot Nothing, ViewState("DtPedidos"), Nothing)
        End Get
        Set(value As DataTable)
            ViewState("DtPedidos") = value
        End Set
    End Property

#End Region

#Region "[VARIÁVEIS]"
    Dim Valida As New List(Of String)
#End Region

#Region "[METODOS BASE]"

    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 03/08/2009 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Dim pbo As PostBackOptions
        Dim s As String = String.Empty

        pbo = New PostBackOptions(btnReportar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnReportar.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnReportar.ClientID & "," & btnLimpar.ClientID & "');closeCalendar();"

        pbo = New PostBackOptions(btnLimpar)
        s = Me.Page.ClientScript.GetPostBackEventReference(pbo)
        btnLimpar.FuncaoJavascript = s & ";if (event.keyCode == 13 || event.keyCode == 32)desabilitar_botoes('" & btnReportar.ClientID & "," & btnLimpar.ClientID & "');closeCalendar();"

        'Adiciona a Precedencia ao Buscar
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ControlePrecedencia", "exclusivePostBackElement='" & btnReportar.ClientID & "';", True)

        ' Adiciona Scripts aos controles
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarEstadoPagina()

        Select Case MyBase.Acao

            Case Enumeradores.eAcao.Consulta

                Me.ddlDelegacion.Enabled = True
                Me.txtFechaConteoDesde.Enabled = True
                Me.txtFechaConteoHasta.Enabled = True
                Me.btnReportar.Visible = True
                Me.btnLimpar.Visible = True

        End Select

    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub ConfigurarTabIndex()

        Me.ddlDelegacion.TabIndex = 1
        Me.lstTipoEspecie.TabIndex = 2
        Me.lstTipoDeposito.TabIndex = 3
        Me.txtFechaConteoDesde.TabIndex = 4
        Me.imbFechaConteoDesde.TabIndex = 5
        Me.txtFechaConteoHasta.TabIndex = 6
        Me.imbFechaConteoHasta.TabIndex = 7
        Me.btnReportar.TabIndex = 8
        Me.btnLimpar.TabIndex = 9
        Me.DefinirRetornoFoco(Master.Sair, Master.RetornaMenu)
        Master.PrimeiroControleTelaID = Me.ddlDelegacion.ClientID

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.REPORTAR_PEDIDO_BCP
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False
    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()

        Try

            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                ' Carrega os dados iniciais dos controles
                CarregarControles()

                ' Inicializa os campos
                LimparCampos()

            End If

        Catch ex As Exception
            Master.ControleErro.ShowError(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()

        Try
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            Master.ControleErro.TratarErroException(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("020_titulo_pagina")
        Me.lblSubTituloCriteriosBusqueda.Text = Traduzir("020_lbl_titulo_busqueda")
        Me.lblDelegacion.Text = Traduzir("020_lbl_delegacion")
        Me.lblTipoEspecie.Text = Traduzir("020_lbl_tipo_especie")
        Me.lblTipoDeposito.Text = Traduzir("020_lbl_tipo_deposito")
        Me.lblFechaConteo.Text = Traduzir("020_lbl_fecha_conteo")
        Me.lblFechaConteoDesde.Text = Traduzir("020_lbl_fecha_conteo_desde")
        Me.lblFechaConteoHasta.Text = Traduzir("020_lbl_fecha_conteo_hasta")
        Me.lblFechaUltimoEnvio.Text = Traduzir("020_lbl_fecha_ultimo_envio")

        Me.lblTituloGrid.Text = Traduzir("020_lbl_titulo_reportado")

        Me.gdvItensProceso.Columns(0).HeaderText = Traduzir("020_col_especie")
        Me.gdvItensProceso.Columns(1).HeaderText = Traduzir("020_col_deposito")
        Me.gdvItensProceso.Columns(2).HeaderText = Traduzir("020_col_fecha_desde")
        Me.gdvItensProceso.Columns(3).HeaderText = Traduzir("020_col_fecha_hasta")
        Me.gdvItensProceso.Columns(4).HeaderText = Traduzir("020_col_delegacion")
        Me.gdvItensProceso.Columns(5).HeaderText = Traduzir("020_col_estado")
        Me.gdvItensProceso.Columns(6).HeaderText = Traduzir("020_col_observacion")
        Me.gdvItensProceso.Columns(7).HeaderText = Traduzir("020_col_fecha_creacion")

        Me.csvDelegacion.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_delegacion"))
        Me.csvTipoEspecie.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_tipo_especie"))
        Me.csvTipoDeposito.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_tipo_deposito"))
        Me.csvFechaConteoHasta.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_fecha_conteo"))
        Me.csvFechaConteoDesde.ErrorMessage = String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_fecha_conteo"))

    End Sub

#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        lstTipoDeposito.Attributes.Add("onkeydown", "EventoEnter('" & btnReportar.ID & "_img');")
        lstTipoEspecie.Attributes.Add("onkeydown", "EventoEnter('" & btnReportar.ID & "_img');")

        ' Define a mascara do período inicial digitado
        Me.txtFechaConteoDesde.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##');")
        ' Define a mascara do período final digitado
        Me.txtFechaConteoHasta.Attributes.Add("onkeypress", "return mask(true, event, this, '##/##/#### ##:##');")
        ' Define a mascara da data inicial escolhido no calendario
        Me.imbFechaConteoDesde.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaConteoDesde.ClientID & ",document.forms[0]." & Me.txtFechaConteoHasta.ClientID & ",'dd/mm/yyyy hh:ii',this,true,'" & IdiomaCorrente & "'); return false;")
        ' Define a mascara da data final escolhido no calendario
        Me.imbFechaConteoHasta.Attributes.Add("OnClick", "displayCalendar2(document.forms[0]." & Me.txtFechaConteoHasta.ClientID & ",document.forms[0]." & Me.txtFechaConteoDesde.ClientID & ",'dd/mm/yyyy hh:ii',this,true,'" & IdiomaCorrente & "', true); return false;")

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarControles()

        ' carregar delegações do xml para o combo
        Util.CarregarDelegacoes(ddlDelegacion, MyBase.InformacionUsuario.Delegaciones)

        ' Inicializa o controle de delegação com a mesma delegação selecionada na tela de login
        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)

        lstTipoEspecie.Items.Add(New ListItem(Traduzir("020_lst_especie_moneda"), 0))
        lstTipoEspecie.Items.Add(New ListItem(Traduzir("020_lst_especie_billete"), 1))

        lstTipoDeposito.Items.Add(New ListItem(Traduzir("020_lst_deposito_centralizado"), 1))
        lstTipoDeposito.Items.Add(New ListItem(Traduzir("020_lst_deposito_servilock"), 2))

        LerUltimaFecha()

    End Sub

    ''' <summary>
    ''' Limpa os campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LimparCampos()

        Me.ddlDelegacion.SelectedValue = MyBase.DelegacionConectada.Keys(0)
        Me.txtFechaConteoDesde.Text = String.Empty
        Me.txtFechaConteoHasta.Text = String.Empty

        For Each item As ListItem In lstTipoDeposito.Items
            item.Selected = False
        Next
        lstTipoDeposito.SelectedValue = "2"

        For Each item As ListItem In lstTipoEspecie.Items
            item.Selected = False
        Next

    End Sub


    ''' <summary>
    ''' Adiciona uma mensagem ao arquivo de Log em disco
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LerUltimaFecha()
        Dim Mensagem As String = ""
        Try

            lstTipoDeposito.SelectedValue = 2

            Dim FechaBasePedidoBCP As String = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings("FechaBasePedidoBCP")
            If String.IsNullOrEmpty(FechaBasePedidoBCP) OrElse Not Integer.TryParse(FechaBasePedidoBCP, FechaBasePedidoBCP) Then
                FechaBasePedidoBCP = "100"
            End If

            Dim proxyBCP As New ListadosConteo.ProxyBCP
            Dim objPeticion As New ContractoServ.bcp.RecuperarPedidosReportadosBCP.Peticion
            Dim objRespuesta As ContractoServ.bcp.RecuperarPedidosReportadosBCP.Respuesta

            objPeticion.CodDelegacion = ddlDelegacion.SelectedValue
            objPeticion.CodProceso = Constantes.CONST_BCP_CODIGO_PROCESO_DUO
            objPeticion.FechaHasta = Date.Today.AddDays(-1 * Integer.Parse(FechaBasePedidoBCP))

            objRespuesta = proxyBCP.RecuperarPedidosReportadosBCP(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, objRespuesta.MensajeError, True) Then
                Mensagem = String.Empty
            End If

            If objRespuesta.Pedidos IsNot Nothing AndAlso objRespuesta.Pedidos.Count > 0 Then
                Mensagem = objRespuesta.Pedidos(0).FechaDesde.ToString("dd/MM/yyyy HH:mm:ss") & " | " & _
                           objRespuesta.Pedidos(0).FechaHasta.ToString("dd/MM/yyyy HH:mm:ss")
            Else
                Mensagem = Traduzir("020_msg_sin_datos_retorno")
            End If
            lblValorFechaUltimoEnvio.Text = Mensagem

            ' converte o objeto retornado para um datatable
            DtPedidos = gdvItensProceso.ConvertListToDataTable(objRespuesta.Pedidos)

            ' preenche o grid
            gdvItensProceso.CarregaControle(DtPedidos)

        Catch ex As Exception
            Master.ControleErro.ShowError(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Valida o preenchimento dos controles da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidarControles()

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(ddlDelegacion.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_delegacion")))
            Me.csvDelegacion.IsValid = False
        End If

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(lstTipoEspecie.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_tipo_especie")))
            Me.csvTipoEspecie.IsValid = False
        End If

        ' Verifica se a delegacion foi preenchida
        If String.IsNullOrEmpty(lstTipoDeposito.SelectedValue) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_tipo_deposito")))
            Me.csvTipoDeposito.IsValid = False
        End If

        ' Verifica se a data de processo inicial foi preenchida
        If txtFechaConteoDesde.Text = String.Empty Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
            Me.csvFechaConteoDesde.IsValid = False
        End If

        ' Verifica se a data de processo final foi preenchida
        If txtFechaConteoHasta.Text = String.Empty Then
            Valida.Add(String.Format(Traduzir("lbl_campo_nao_preenchido"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta")))
            Me.csvFechaConteoHasta.IsValid = False
        End If

        ' Verifica se a data de processo inicial é uma data válida
        If txtFechaConteoDesde.Text <> String.Empty AndAlso (txtFechaConteoDesde.Text.Length < 10 OrElse Not (IsDate(txtFechaConteoDesde.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
            Me.csvFechaConteoDesde.IsValid = False
        ElseIf txtFechaConteoDesde.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo inicial não foi informada
            ' coloca a hora inicial do dia
            txtFechaConteoDesde.Text = txtFechaConteoDesde.Text.TrimEnd & " 00:00"
        End If

        ' Verifica se a data de processo final é uma data válida
        If txtFechaConteoHasta.Text <> String.Empty AndAlso (txtFechaConteoHasta.Text.Length < 10 OrElse Not (IsDate(txtFechaConteoHasta.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_datahora_invalida"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta")))
            Me.csvFechaConteoHasta.IsValid = False
        ElseIf txtFechaConteoHasta.Text.TrimEnd.Length = 10 Then ' Verifica se a hora da data de processo final não foi informada
            ' coloca a hora final do dia
            txtFechaConteoHasta.Text = txtFechaConteoHasta.Text.TrimEnd & " 23:59"
        End If

        ' Verifica se a data de processo inicial é maior do que a data de processo inicial
        If (IsDate(txtFechaConteoDesde.Text) AndAlso IsDate(txtFechaConteoHasta.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaConteoDesde.Text), Convert.ToDateTime(txtFechaConteoHasta.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_hasta"), Traduzir("020_lbl_fecha_conteo") & " " & Traduzir("lbl_desde")))
        End If

    End Sub


#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Ação ao limpar os valores dos filtros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            Me.LimparCampos()
        Catch ex As Exception
            Master.ControleErro.ShowError(ex.Message)
        End Try
    End Sub

    Private Sub btnReportar_Click(sender As Object, e As System.EventArgs) Handles btnReportar.Click

        Try
            ' Valida os controles usados no filtro
            Me.ValidarControles()

            If Valida.Count > 0 Then
                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                Master.ControleErro.ShowError(Valida, Enumeradores.eMensagem.Atencao)
                Exit Sub
            End If

            Dim proxyBCP As New ListadosConteo.ProxyBCP
            Dim objPeticion As New ContractoServ.bcp.GuardarItemProcesoConteo.Peticion
            Dim objRespuesta As ContractoServ.bcp.GuardarItemProcesoConteo.Respuesta
            Dim codigoProceso As String = Constantes.CONST_BCP_CODIGO_PROCESO_DUO

            Dim ValorItemProceso As String = String.Empty
            Dim Especies As String = String.Empty
            Dim Depositos As String = String.Empty

            For Each especie As ListItem In (From item As ListItem In lstTipoEspecie.Items Where item.Selected = True)

                If Not String.IsNullOrEmpty(Especies) Then
                    Especies &= ";"
                End If

                Especies &= especie.Value

            Next

            For Each deposito As ListItem In (From item As ListItem In lstTipoDeposito.Items Where item.Selected = True)

                If Not String.IsNullOrEmpty(Depositos) Then
                    Depositos &= ";"
                End If

                Depositos &= deposito.Value

            Next

            ValorItemProceso = Especies & "|" & Depositos & "|" & txtFechaConteoDesde.Text & "|" & txtFechaConteoHasta.Text

            objPeticion.CodProceso = codigoProceso
            objPeticion.CodDelegacion = ddlDelegacion.SelectedValue
            objPeticion.FechaCreacion = Date.Now
            objPeticion.CodigoUsuario = LoginUsuario
            objPeticion.Parametros = ValorItemProceso

            objRespuesta = proxyBCP.GuardarItemProcesoConteo(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, objRespuesta.MensajeError, True) Then
                Exit Sub
            End If

            LerUltimaFecha()

            'Mensagem Sucesso
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_sucesso", "alert('" & Traduzir("info_msg_sucesso") & "');", True)

            Me.LimparCampos()

        Catch ex As Exception
            Master.ControleErro.ShowError(ex.Message)
        End Try

    End Sub

    Private Sub btnAtualizar_Click(sender As Object, e As System.EventArgs) Handles btnAtualizar.Click
        LerUltimaFecha()
    End Sub

#Region "[EVENTOS GRIDVIEW]"

    Private Sub gdvItensProceso_EPager_SetCss(sender As Object, e As System.EventArgs) Handles gdvItensProceso.EPager_SetCss

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

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 22/12/2009 Criado
    ''' </history>
    Protected Sub gdvItensProceso_EPreencheDados(sender As Object, e As EventArgs) Handles gdvItensProceso.EPreencheDados

        Try

            If DtPedidos IsNot Nothing _
                AndAlso DtPedidos.Rows.Count > 0 Then

                If gdvItensProceso.SortCommand = String.Empty Then
                    DtPedidos.DefaultView.Sort = " FechaCreacion desc "
                Else
                    DtPedidos.DefaultView.Sort = gdvItensProceso.SortCommand
                End If

                gdvItensProceso.ControleDataSource = DtPedidos
            Else

                'Limpa a consulta
                gdvItensProceso.DataSource = Nothing
                gdvItensProceso.DataBind()

                Acao = Enumeradores.eAcao.NoAction

            End If


        Catch ex As Exception
            ControleErro.TratarErroException(ex)
        End Try

    End Sub

#End Region

#End Region


End Class