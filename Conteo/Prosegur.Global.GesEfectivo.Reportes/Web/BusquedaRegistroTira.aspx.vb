Imports System.Collections.ObjectModel
Imports DevExpress.Data.Mask
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comunicacion

Public Class BusquedaRegistroTira
    Inherits Base

#Region "[ATRIBUTOS]"
    Private Valida As New List(Of String)
#End Region

#Region "[PROPRIEDADES]"

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientes"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(ByVal value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

#End Region

#Region "[METODOS BASE]"


    ''' <summary>
    ''' Adiciona a validação aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()
        ' Adiciona Scripts aos controles
        ConfigurarControles()

    End Sub

    ''' <summary>
    ''' Configura estado da página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarEstadoPagina()
    End Sub

    ''' <summary>
    ''' Seta tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()

        ' define ação da tela
        MyBase.Acao = Enumeradores.eAcao.Consulta
        ' definir o nome da página
        MyBase.PaginaAtual = Enumeradores.eTelas.RECIBO_F22_RESPALDO
        ' desativar validação de ação
        MyBase.ValidarAcao = False
        ' desativar validação de permissões do AD
        MyBase.ValidarPemissaoAD = False

    End Sub

    ''' <summary>
    ''' Carrega os dados da tela
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CarregarControles()

        txtFechaFim.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFechaInicio.Text = Date.Now.AddDays(-7).ToString("dd/MM/yyyy")

    End Sub

    ''' <summary>
    ''' Primeiro metodo chamado quando inicia a pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            ASPxGridView.RegisterBaseScript(Page)
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = False
            Master.HabilitarHistorico = True
            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.Titulo = Traduzir("014_titulo_pagina")

            gvTiras.SettingsPager.Summary.Text = Traduzir("pagerFormatGrid")

            ConfigurarControle_Cliente()
            ' Adiciona scripts aos controles
            AdicionarScripts()

            If Not Page.IsPostBack Then

                ' Carrega os dados iniciais dos controles
                CarregarControles()

                ' Inicializa os campos
                LimparCampos()

            End If

            ' setar foco no campo codigo
            Me.txtCodigoATM.Focus()

            ' trata o foco dos campos
            TrataFoco()

        Catch ex As Exception

            MyBase.MostraMensagemExcecao(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 22/03/2011 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            Dim script As String = String.Format("AbrirCalendario('{0}','{1}',{2});", _
                                       txtFechaInicio.ClientID, "false", 0)

            script &= String.Format(" AbrirCalendario('{0}','{1}',{2});", _
                                        txtFechaFim.ClientID, "false", 0)

            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "CHAMA CALENDAR", script, True)
            Me.ConfigurarEstadoPagina()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Overrides Sub TraduzirControles()
        MyBase.TraduzirControles()

        Me.Page.Title = Traduzir("014_titulo_pagina")
        lblSubTituloCriteriosBusqueda.Text = Traduzir("014_pnlFiltros")
        lblCodigoATM.Text = Traduzir("014_lblCodigoATM")
        lblFechaInicio.Text = Traduzir("014_lblFechaInicio")
        lblFechaFim.Text = Traduzir("014_lblFechaFim")
        lblTituloTabela.Text = Traduzir("014_lblTituloTabela")
        lblPeriodoContable.Text = Traduzir("014_lblPeriodoContable")
        Me.btnBuscar.Text = Traduzir("btnBuscar")
        Me.btnLimpar.Text = Traduzir("btnLimpiar")
        Me.btnBuscar.ToolTip = Traduzir("btnBuscar")
        Me.btnLimpar.ToolTip = Traduzir("btnLimpiar")

        'Grid
        gvTiras.Columns(0).Caption = Traduzir("014_colFecha")
        gvTiras.Columns(1).Caption = Traduzir("014_colCodigoAtm")
        gvTiras.Columns(2).Caption = Traduzir("014_colDesPeriodoContable")
        gvTiras.Columns(4).Caption = Traduzir("014_colCliente")
        gvTiras.Columns(6).Caption = Traduzir("014_colSubCliente")
        gvTiras.Columns(8).Caption = Traduzir("014_colPuntoServicio")
        gvTiras.Columns(9).Caption = Traduzir("014_colConsultar")

        gvTiras.SettingsText.EmptyDataRow = Traduzir("info_msg_sin_registro")
    End Sub

#End Region


#Region "[MÉTODOS]"

    ''' <summary>
    ''' Trata o foco da página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Util.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing And Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Limpa os campos da tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub LimparCampos()

        'Limpa campos
        txtCodigoATM.Text = String.Empty

        txtFechaFim.Text = Date.Now.ToString("dd/MM/yyyy")
        txtFechaInicio.Text = Date.Now.AddDays(-7).ToString("dd/MM/yyyy")

        'Limpa a consulta

        gvTiras.DataSource = Nothing
        gvTiras.DataBind()

        Clientes = Nothing


    End Sub

    Private Sub ValidarControles()

        ' Verifica se a data de contagem inicial é uma data válida
        If txtFechaInicio.Text <> String.Empty AndAlso (txtFechaInicio.Text.Length < 10 Or Not (IsDate(txtFechaInicio.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("014_lblFechaInicio")))
        End If

        ' Verifica se a data de contagem final é uma data válida
        If txtFechaFim.Text <> String.Empty AndAlso (txtFechaFim.Text.Length < 10 Or Not (IsDate(txtFechaFim.Text))) Then
            Valida.Add(String.Format(Traduzir("lbl_campo_data_invalida"), Traduzir("014_lblFechaFim")))
        End If

        ' Verifica se a data de contagem inicial é maior do que a data de contagem inicial
        If (IsDate(txtFechaInicio.Text) AndAlso IsDate(txtFechaFim.Text)) AndAlso _
        Date.Compare(Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFim.Text)) > 0 Then
            Valida.Add(String.Format(Traduzir("lbl_campo_periodo_invalido"), Traduzir("014_lblFechaFim"), Traduzir("014_lblFechaInicio")))
        End If
    End Sub

    ''' <summary>
    ''' Configura os controles da tela com as mascaras de entrada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 23/03/2011 Criado
    ''' </history>
    Private Sub ConfigurarControles()

        ' Obtém a lista de idiomas
        Dim Idiomas As List(Of String) = ObterIdiomas()
        ' Recupera o idioma corrente
        Dim IdiomaCorrente As String = Idiomas(0).Split("-")(0)

        ' Define a mascara do período inicial digitado
        '  Me.txtFechaInicio.Attributes.Add("onkeypress", "return mascaraData(this);")
        ' Define a mascara do período final digitado
        ' Me.txtFechaFim.Attributes.Add("onkeypress", "return mascaraData(this);")

    End Sub

    ' ''' <summary>
    ' ''' Obtém tiras por filtros
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <history>
    ' ''' [kirkpatrick.santos] 24/06/2011 Criado
    ' ''' </history>
    'Private Function GetTiras() As ATM.ContractoServicio.Integracion.GetTiras.Respuesta

    '    ' criar objeto peticion
    '    Dim objPeticion As New ATM.ContractoServicio.Integracion.GetTiras.Peticion
    '    If Not String.IsNullOrEmpty(txtCodigoATM.Text.Trim) Then objPeticion.CodCajero = txtCodigoATM.Text.Trim

    '    If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
    '        objPeticion.CodCliente = Clientes.FirstOrDefault().Codigo
    '        If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
    '            objPeticion.CodSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
    '            If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
    '                objPeticion.CodPtoServicio = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
    '            End If
    '        End If
    '    End If

    '    If Not String.IsNullOrEmpty(txtFechaInicio.Text.Trim.Trim("/")) Then objPeticion.FecInicio = txtFechaInicio.Text.Trim
    '    If Not String.IsNullOrEmpty(txtFechaFim.Text.Trim.Trim("/")) Then objPeticion.FecFin = txtFechaFim.Text.Trim
    '    If Not String.IsNullOrEmpty(txtPeriodoContable.Text.Trim) Then objPeticion.desPeriodoContable = txtPeriodoContable.Text.Trim

    '    ' criar objeto proxy
    '    Dim objProxy As New ProxyAtmIntegracion

    '    ' chamar servicio
    '    Return objProxy.GetTiras(objPeticion)

    'End Function


#End Region

#Region "[EVENTOS]"


    Private Sub gvTiras_PageIndexChanged(sender As Object, e As EventArgs) Handles gvTiras.PageIndexChanged
        Try

            Dim objDT As DataTable

            Dim objRespuesta As ATM.ContractoServicio.Integracion.GetTiras.Respuesta

            objRespuesta = GetTiras()

            objDT = Util.ConverterListParaDataTable(objRespuesta.Tiras)

            objDT.DefaultView.Sort = " FyhRegistroTira DESC"

            gvTiras.DataSource = objDT
            gvTiras.DataBind()


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub gvTiras_OnHtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Try
            If (e.RowType = DevExpress.Web.ASPxGridView.GridViewRowType.Data) Then

                'Recupera a Descrição da configuração.
                Dim fyhRegistroTira As String = gvTiras.GetRowValues(e.VisibleIndex, "FyhRegistroTira")
                Dim lblFyhRegistroTira As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemFyhRegistroTira"), Label)
                lblFyhRegistroTira.Text = DateTime.Parse(fyhRegistroTira).ToString("dd/MM/yyyy")

                Dim NumeroMaximoLinha As Integer = Constantes.MaximoCaracteresLinha
                Dim codCajero As String = gvTiras.GetRowValues(e.VisibleIndex, "CodCajero")
                Dim lblItemDescripcion As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemCodCajero"), Label)
                lblItemDescripcion.Text = If(String.IsNullOrEmpty(codCajero), String.Empty, If(codCajero.Length > NumeroMaximoLinha, codCajero.Substring(0, NumeroMaximoLinha) & "...", codCajero))

                Dim desPeriodoContable As String = gvTiras.GetRowValues(e.VisibleIndex, "DesPeriodoContable")
                Dim lblDesPeriodoContable As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemDesPeriodoContable"), Label)
                lblDesPeriodoContable.Text = desPeriodoContable

                Dim codCliente As String = gvTiras.GetRowValues(e.VisibleIndex, "CodCliente")
                Dim desCliente As String = gvTiras.GetRowValues(e.VisibleIndex, "DesCliente")
                Dim lblItemDesCliente As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemDesCliente"), Label)
                lblItemDesCliente.Text = codCliente & " - " & desCliente

                Dim codSubcliente As String = gvTiras.GetRowValues(e.VisibleIndex, "CodSubcliente")
                Dim desSubcliente As String = gvTiras.GetRowValues(e.VisibleIndex, "DesSubcliente")
                Dim lblItemDesSubcliente As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemDesSubcliente"), Label)
                lblItemDesSubcliente.Text = codSubcliente & " - " & desSubcliente

                Dim codPtoServicio As String = gvTiras.GetRowValues(e.VisibleIndex, "CodPtoServicio")
                Dim desPtoServicio As String = gvTiras.GetRowValues(e.VisibleIndex, "DesPtoServicio")
                Dim lblItemDesPtoServicio As Label = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "lblItemDesPtoServicio"), Label)
                lblItemDesPtoServicio.Text = codPtoServicio & " - " & desPtoServicio

                'Verifica o tipo de arquivo
                Dim oidtira As String = gvTiras.GetRowValues(e.VisibleIndex, "OidTira")
                Dim imgConsultar As Image = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "imgConsultar"), Image)
                imgConsultar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/search.png")
                imgConsultar.Attributes.Add("OnClick", "AbrirPopupModal('RegistroTiraMostrar.aspx?IdTira=" & oidtira & "', screen.height-10, screen.width-15,'false');")
                imgConsultar.Attributes.Add("style", "cursor:pointer;")
                Dim hidOitTira As HiddenField = CType(gvTiras.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "hidOidTira"), HiddenField)
                hidOitTira.Value = oidtira

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try

            Acao = Enumeradores.eAcao.Busca

            ConsultarDados()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Recupera os dados da lista dos registros de tira
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kirkpatrick.santos] 28/06/2011 Criado
    ''' </history>
    Private Sub ConsultarDados()

        Try

            ' Valida os controles usados no filtro
            Me.ValidarControles()

            ' Se não existe erro
            If Valida.Count = 0 Then

                ' Cria objeto para chamada do serviço
                Dim objAtmIntegracion As New ProxyAtmIntegracion

                ' Define os valores dos filtros da consulta
                Dim objPeticion As New ATM.ContractoServicio.Integracion.GetTiras.Peticion
                objPeticion.CodCajero = txtCodigoATM.Text

                If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
                    objPeticion.CodCliente = Clientes.FirstOrDefault().Codigo
                    If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                        objPeticion.CodSubcliente = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo
                        If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                            objPeticion.CodPtoServicio = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
                        End If
                    End If
                End If

                If txtFechaInicio.Text.Trim = "" Then
                    objPeticion.FecInicio = Nothing
                Else
                    objPeticion.FecInicio = Date.Parse(txtFechaInicio.Text.Trim)
                End If
                If txtFechaFim.Text.Trim = "" Then
                    objPeticion.FecFin = Nothing
                Else
                    objPeticion.FecFin = Date.Parse(txtFechaFim.Text.Trim)
                End If

                If Not String.IsNullOrEmpty(txtPeriodoContable.Text.Trim) Then objPeticion.desPeriodoContable = txtPeriodoContable.Text.Trim

                ' Recupera os dados do tira para popular o grid
                Dim objRespuesta As ATM.ContractoServicio.Integracion.GetTiras.Respuesta = objAtmIntegracion.GetTiras(objPeticion)

                ' Verifica se aconteceu algum erro ao pesquisar os dados do relatório     
                Dim msgErro As String = String.Empty
                If Not Master.ControleErro.VerificaErro2(objRespuesta.CodigoError, ContractoServ.Login.ResultadoOperacionLoginLocal.Autenticado, msgErro, objRespuesta.MensajeError, True) Then
                    MyBase.MostraMensagem(msgErro)
                    Exit Sub
                End If

                ' converter objeto para datatable
                Dim objDt As DataTable = Util.ConverterListParaDataTable(objRespuesta.Tiras)

                If Acao = Enumeradores.eAcao.Busca Then
                    objDt.DefaultView.Sort = " FyhRegistroTira DESC"
                End If

                If objRespuesta.Tiras.Count = 0 Then
                    ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                    Valida.Add(Traduzir("info_msg_sin_registro"))
                    MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                    gvTiras.DataSource = Nothing
                    gvTiras.DataBind()
                    pnGridTiras.Visible = False
                    Exit Sub
                End If

                ' carregar controle
                gvTiras.DataSource = objDt
                gvTiras.DataBind()
                pnGridTiras.Visible = True
            Else
                ' Mostra as mensagens de erros quando os dados do filtro não forem preenchidos
                MyBase.MostraMensagem(String.Join("<br>", Valida.ToArray()))
                gvTiras.DataSource = Nothing
                gvTiras.DataBind()
                pnGridTiras.Visible = False
            End If

        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub



    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        Try
            LimparCampos()

            'Estado Inicial
            Acao = Enumeradores.eAcao.Inicial

            Response.Redireccionar("BusquedaRegistroTira.aspx")
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

#End Region
#Region "Helpers"
    Protected Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = False
        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False


        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Public Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If
        Catch ex As Exception
            ' Mostra as mensagens de erros
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Exception, e.Erro.Message, e.Erro.ToString()))
    End Sub
#End Region


End Class