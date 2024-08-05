Imports System.Collections.ObjectModel
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones

Public Class BusquedaNivelSaldoPopup
    Inherits Base
#Region "[HelpersCliente]"
    Public Property Clientes As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente)
        Get
            Return ucClientes.Clientes
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Cliente))
            ucClientes.Clientes = value
        End Set
    End Property

    Private WithEvents _ucClientes As ucCliente
    Public Property ucClientes() As ucCliente
        Get
            If _ucClientes Is Nothing Then
                _ucClientes = LoadControl(ResolveUrl("~\Controles\Helpers\ucCliente.ascx"))
                _ucClientes.ID = Me.ID & "_ucClientesNivelSaldo"
                AddHandler _ucClientes.Erro, AddressOf ErroControles
                phCliente.Controls.Add(_ucClientes)
            End If
            Return _ucClientes
        End Get
        Set(value As ucCliente)
            _ucClientes = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Cliente()

        Me.ucClientes.SelecaoMultipla = False
        Me.ucClientes.ClienteHabilitado = True
        Me.ucClientes.ClienteObrigatorio = True

        Me.ucClientes.SubClienteHabilitado = True
        Me.ucClientes.SubClienteObrigatorio = False
        Me.ucClientes.ucSubCliente.MultiSelecao = False

        Me.ucClientes.PtoServicioHabilitado = True
        Me.ucClientes.PtoServicoObrigatorio = False
        Me.ucClientes.ucPtoServicio.MultiSelecao = False

        If Clientes IsNot Nothing Then
            Me.ucClientes.Clientes = Clientes
        End If

    End Sub
    Private Sub ucClientes_OnControleAtualizado() Handles _ucClientes.UpdatedControl
        Try
            If ucClientes.Clientes IsNot Nothing Then
                Clientes = ucClientes.Clientes
            End If

            ConsomeCliente()
            ConsomeSubCliente()
            ConsomePuntoServicio()
        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[Propriedades]"
    Private ReadOnly Property divModal() As String
        Get
            Return Request.QueryString("divModal").ToString()
        End Get
    End Property
    Private ReadOnly Property ifrModal() As String
        Get
            Return Request.QueryString("ifrModal").ToString()
        End Get
    End Property
    Private ReadOnly Property btnExecutar() As String
        Get
            Return Request.QueryString("btnExecutar").ToString()
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub AdicionarScripts()


    End Sub

    Protected Overrides Sub ConfigurarTabIndex()

        'txtCliente.TabIndex = 1
        'btnBuscarCliente.TabIndex = 2
        'txtSubCliente.TabIndex = 3
        'btnBuscarSubCliente.TabIndex = 4
        'txtPuntoServicio.TabIndex = 5
        'btnBuscarPuntoServicio.TabIndex = 6

        'btnAceptar.TabIndex = 7
        'btnCancelar.TabIndex = 8

    End Sub

    Protected Overrides Sub DefinirParametrosBase()

        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.NIVELESSALDOS
        MyBase.ValidarAcao = False
        MyBase.ValidarPemissaoAD = False

        If Request("acao") IsNot Nothing Then
            MyBase.Acao = Request("acao")
        End If

    End Sub

    Protected Overrides Sub Inicializar()

        Try
            DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)
            If Not Page.IsPostBack Then
                Clientes = Nothing
                'btnBuscarPuntoServicio.Habilitado = False
                'btnBuscarSubCliente.Habilitado = False

                If Peticion Is Nothing Then
                    Throw New Exception(Traduzir("err_passagem_parametro"))
                End If

                If MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion Then
                    ' btnBuscarCliente.Focus()
                ElseIf MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                End If

                CarregarSubCanal()

                CarregarHelpers()

            End If

            ConfigurarControle_Cliente()

            TrataFoco()

            ConsomeSubCanal()

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    Protected Overrides Sub TraduzirControles()

        Me.Page.Title = Traduzir("043_lbl_titulo")

        lblSubCanal.Text = Traduzir("043_lbl_gdr_sub_canal")
        btnAceptar.Text = Traduzir("btnAceptar")
        btnLimpar.Text = Traduzir("btnLimpiar")


    End Sub

    Protected Overrides Sub PreRenderizar()
        Try
            ControleBotoes()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)

        End Try
    End Sub

    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Consulta, Aplicacao.Util.Utilidad.eAcao.Busca
                btnAceptar.Visible = False
                'btnBuscarCliente.Visible = False
                'btnBuscarPuntoServicio.Visible = False
                'btnBuscarSubCliente.Visible = False
                btnLimpar.Visible = False

        End Select

        'txtCliente.Enabled = False
        'txtSubCliente.Enabled = False
        'txtPuntoServicio.Enabled = False

        'Caso algum dos campos(codigo ou descrição) estejam com erro
        'então continua exibindo a mensagem de erro
        '  Dim MensagemErro As String = MontaMensagensErro()

        '  If MensagemErro <> String.Empty Then
        'MyBase.MostraMensagem(MensagemErro)
        '  End If
    End Sub

#End Region

#Region "[PROPRIEDADES]"
    ''' <summary>
    ''' Armazena coleção de SubCanais
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ListaSubCanal() As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

        Get
            Return ViewState("ListaSubCanal")
        End Get

        Set(value As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion)
            ViewState("ListaSubCanal") = value
        End Set

    End Property
#End Region

#Region "[METODOS]"

    Private Sub CarregarHelpers()
        Try
            If Session("ClienteSelecionado") IsNot Nothing Then
                Dim oClienteSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente = Session("ClienteSelecionado")

                Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
                objCliente.Identificador = oClienteSelecionado.Codigo
                objCliente.Codigo = oClienteSelecionado.Codigo
                objCliente.Descripcion = oClienteSelecionado.Descripcion
                Clientes.Clear()
                Clientes.Add(objCliente)

                'SubCliente
                If Session("SubClientesSelecionados") IsNot Nothing Then

                    Dim oSubClienteSelecionado As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion = Session("SubClientesSelecionados")

                    Dim objSubCliente As New Prosegur.Genesis.Comon.Clases.SubCliente
                    objSubCliente.Identificador = oSubClienteSelecionado.FirstOrDefault().Codigo
                    objSubCliente.Codigo = oSubClienteSelecionado.FirstOrDefault().Codigo
                    objSubCliente.Descripcion = oSubClienteSelecionado.FirstOrDefault().Descripcion

                    If Clientes.FirstOrDefault().SubClientes Is Nothing Then
                        Clientes.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                    End If
                    Clientes.FirstOrDefault().SubClientes.Add(objSubCliente)

                End If

                'Punto de Servicio
                If Session("PuntosServicioSelecionados") IsNot Nothing Then

                    If Clientes.FirstOrDefault().SubClientes Is Nothing Then
                        Clientes.FirstOrDefault().SubClientes = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.SubCliente)
                    End If

                    Dim oPuntoServicioSelecionado As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion = Session("PuntosServicioSelecionados")

                    Dim objPuntoServicio As New Prosegur.Genesis.Comon.Clases.PuntoServicio
                    objPuntoServicio.Identificador = oPuntoServicioSelecionado.FirstOrDefault().Codigo
                    objPuntoServicio.Codigo = oPuntoServicioSelecionado.FirstOrDefault().Codigo
                    objPuntoServicio.Descripcion = oPuntoServicioSelecionado.FirstOrDefault().Descripcion


                    If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio Is Nothing Then
                        Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio = New ObservableCollection(Of Prosegur.Genesis.Comon.Clases.PuntoServicio)
                    End If

                    Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Add(objPuntoServicio)

                End If
                AtualizaDadosHelperCliente(Clientes)
                AtualizaDadosHelperSubCliente(Clientes)
                AtualizaDadosHelperPuntoServicio(Clientes)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)

        For Each c In observableCollection
            If Not String.IsNullOrEmpty(c.Identificador) Then
                Dim DadosExibir As New Comon.Helper.Respuesta
                With DadosExibir
                    .IdentificadorPai = Nothing
                    .Identificador = c.Identificador
                    .Codigo = c.Codigo
                    .Descricao = c.Descripcion
                End With
                dadosCliente.DatosRespuesta.Add(DadosExibir)
            End If
        Next

        ucClientes.ucCliente.RegistrosSelecionados = dadosCliente
        ucClientes.ucCliente.ExibirDados(True)
    End Sub

    Private Sub AtualizaDadosHelperSubCliente(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        If observableCollection.FirstOrDefault().SubClientes IsNot Nothing Then
            For Each c In observableCollection.FirstOrDefault().SubClientes
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = observableCollection.FirstOrDefault().Identificador
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
        End If
        ucClientes.Clientes = observableCollection
        ucClientes.ucSubCliente.RegistrosSelecionados = dadosCliente
        ucClientes.ucSubCliente.ExibirDados(True)
    End Sub

    Private Sub AtualizaDadosHelperPuntoServicio(observableCollection As ObservableCollection(Of Comon.Clases.Cliente))
        Dim dadosCliente As New Comon.RespuestaHelper
        dadosCliente.DatosRespuesta = New List(Of Comon.Helper.Respuesta)
        If observableCollection.FirstOrDefault().SubClientes IsNot Nothing AndAlso observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing Then
            For Each c In observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio
                If Not String.IsNullOrEmpty(c.Identificador) Then
                    Dim DadosExibir As New Comon.Helper.Respuesta
                    With DadosExibir
                        .IdentificadorPai = observableCollection.FirstOrDefault().SubClientes.FirstOrDefault().Identificador
                        .Identificador = c.Identificador
                        .Codigo = c.Codigo
                        .Descricao = c.Descripcion
                    End With
                    dadosCliente.DatosRespuesta.Add(DadosExibir)
                End If
            Next
        End If
        ucClientes.Clientes = observableCollection
        ucClientes.ucPtoServicio.RegistrosSelecionados = dadosCliente
        ucClientes.ucPtoServicio.ExibirDados(True)

    End Sub

    Private Sub ConsomeCliente()
        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            LimparCliente()

            Dim codigoSelecionado As String = Clientes.FirstOrDefault().Codigo

            ClienteSelecionado = GetComboClientes(Clientes.FirstOrDefault().Codigo).Clientes.Find(Function(d) d.Codigo = codigoSelecionado)
        End If

    End Sub

    Private Sub ConsomeSubCliente()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                LimparSubCliente()
                LimparPuntoServicio()
                SubClientesSelecionados = GetComboSubClientes(Clientes.FirstOrDefault().Codigo, Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo).Clientes.FirstOrDefault.SubClientes
            End If
        End If
        'If Session("SubClientesSelecionados") IsNot Nothing Then

        '    LimparSubCliente()
        '    LimparPuntoServicio()

        '    SubClientesSelecionados = Session("SubClientesSelecionados")

        '    Dim SubClienteSelecionado = SubClientesSelecionados.FirstOrDefault()

        '    If SubClienteSelecionado.Codigo IsNot Nothing Then
        '        'txtSubCliente.Text = SubClienteSelecionado.Codigo & " - " & SubClienteSelecionado.Descripcion
        '        'txtSubCliente.ToolTip = txtSubCliente.Text
        '        'btnBuscarPuntoServicio.Habilitado = True
        '        'btnBuscarPuntoServicio.Focus()
        '    End Ifs

        '    '  UpdatePanelCodigo.Update()

        '    Session("SubClientesSelecionados") = Nothing

        'End If

    End Sub
    Private Sub ConsomePuntoServicio()

        If Clientes IsNot Nothing AndAlso Clientes.Count > 0 Then
            If Clientes.FirstOrDefault().SubClientes IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.Count > 0 Then
                If Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio IsNot Nothing AndAlso Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.Count > 0 Then
                    LimparPuntoServicio()
                   
                    Dim codigoSelecionado As String = Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo
                    
                    PuntoServicioSelecionado = GetComboPuntosServicio(Clientes.FirstOrDefault().Codigo, _
                                                                      Clientes.FirstOrDefault().SubClientes.FirstOrDefault().Codigo, _
                                                                      Clientes.FirstOrDefault().SubClientes.FirstOrDefault().PuntosServicio.FirstOrDefault().Codigo).Cliente.SubClientes.FirstOrDefault().PuntosServicio.Find(Function(d) d.Codigo = codigoSelecionado)

                End If
            End If
        End If
       

    End Sub
    Private Sub ConsomeSubCanal()

        If Session("SubCanalSelecionado") IsNot Nothing Then

            LimparSubCanal()

            SubCanalSelecionado = Session("SubCanalSelecionado")

            If SubCanalSelecionado.Codigo IsNot Nothing Then
                ddlSubCanal.SelectedValue = SubCanalSelecionado.Codigo
            End If

            Session("SubCanalSelecionado") = Nothing

        End If

    End Sub

    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If ValidarCamposObrigatorios Then

            'Validação OK
            If ClienteSelecionado Is Nothing Then
                strErro.Append(Traduzir("043_msg_csvClienteObrigatorio") & Aplicacao.Util.Utilidad.LineBreak)

            End If

            If ClienteSelecionado IsNot Nothing AndAlso (SubClientesSelecionados Is Nothing AndAlso PuntoServicioSelecionado Is Nothing) Then
                If Not ClienteSelecionado.TotalizadorSaldo Then
                    strErro.Append(Traduzir("043_msg_csvClienteTotalizador") & Aplicacao.Util.Utilidad.LineBreak)
                End If
            End If

            If SubClientesSelecionados IsNot Nothing AndAlso (PuntoServicioSelecionado Is Nothing AndAlso ClienteSelecionado IsNot Nothing) Then
                For Each subCliente In SubClientesSelecionados
                    If Not subCliente.TotalizadorSaldo Then
                        strErro.Append(Traduzir("043_msg_SubClienteTotalizador") & Aplicacao.Util.Utilidad.LineBreak)
                    End If
                Next
            End If

            If PuntoServicioSelecionado IsNot Nothing AndAlso (SubClientesSelecionados IsNot Nothing AndAlso ClienteSelecionado IsNot Nothing) Then
                If Not PuntoServicioSelecionado.TotalizadorSaldo Then
                    strErro.Append(Traduzir("043_msg_csvPuntoServicioTotalizador") & Aplicacao.Util.Utilidad.LineBreak)
                End If
            End If

        End If

        Return strErro.ToString

    End Function

    Private Sub LimparSubCliente()
        SubClientesSelecionados = Nothing
        '   txtSubCliente.Text = String.Empty
        '  txtSubCliente.ToolTip = String.Empty
        LimparPuntoServicio()
    End Sub

    Private Sub LimparPuntoServicio()
        PuntoServicioSelecionado = Nothing
        '  txtPuntoServicio.Text = String.Empty
        ' txtPuntoServicio.ToolTip = String.Empty
    End Sub
    Private Sub LimparSubCanal()
        SubCanalSelecionado = Nothing
        ddlSubCanal.SelectedValue = String.Empty
    End Sub

    Private Sub LimparCliente()
        ClienteSelecionado = Nothing
        '  txtCliente.Text = String.Empty
        '  txtCliente.ToolTip = String.Empty
        LimparSubCliente()
    End Sub

    Public Sub SetClienteSelecionadoPopUp()
        Session("objCliente") = ClienteSelecionado
    End Sub

    Public Sub SetSubClientesSelecionadoPopUp()
        Session("objSubClientes") = SubClientesSelecionados
    End Sub

    ''' <summary>
    ''' Carrega combo SubCanal
    ''' </summary>
    ''' <remarks></remarks>
    ''' [danielnunes] 22/05/2013 Criado
    Public Sub CarregarSubCanal()

        Dim objProxySubCanal As New Comunicacion.ProxyUtilidad
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objSubCanal As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Dim objSubCanalCol As IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion = Nothing

        objRespuesta = objProxySubCanal.GetComboSubcanalesByCanal(objPeticion)

        If objRespuesta.Canales Is Nothing Then
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_todos"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_todos"), String.Empty))
            Exit Sub
        End If

        objSubCanalCol = New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
        For Each item In objRespuesta.Canales
            For Each subcanal In item.SubCanales
                objSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
                objSubCanalCol.Add(subcanal)
            Next
        Next

        ListaSubCanal = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
        ListaSubCanal.AddRange(objSubCanalCol.OrderBy(Function(s) s.Descripcion))

        ddlSubCanal.AppendDataBoundItems = True
        ddlSubCanal.Items.Clear()
        ddlSubCanal.Items.Add(New ListItem(Traduzir("043_ddl_todos"), String.Empty))
        ddlSubCanal.DataTextField = "descripcion"
        ddlSubCanal.DataValueField = "codigo"
        ddlSubCanal.DataSource = ListaSubCanal
        ddlSubCanal.DataBind()

    End Sub

#End Region

#Region "[EVENTOS]"

    'Protected Sub btnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click

    '    Try

    '        Dim url As String = "BusquedaClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=True&vigente=True"

    '        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarCliente');", True)

    '    Catch ex As Exception
    '        MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    'Protected Sub btnBuscarSubCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarSubCliente.Click

    '    Try

    '        If ClienteSelecionado IsNot Nothing Then

    '            Dim url As String = "BusquedaSubClientesPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=False&SelecionaColecaoClientes=false&SelecaoUnica=True&vigente=True"

    '            Session("objCliente") = ClienteSelecionado

    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_SubClientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarSubCliente');", True)

    '        End If

    '    Catch ex As Exception
    '        MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    'Private Sub btnBuscarPuntoServicio_Click(sender As Object, e As System.EventArgs) Handles btnBuscarPuntoServicio.Click

    '    Try

    '        If ClienteSelecionado IsNot Nothing AndAlso SubClientesSelecionados IsNot Nothing Then

    '            SetClienteSelecionadoPopUp()
    '            SetSubClientesSelecionadoPopUp()

    '            Dim url As String = "BusquedaPuntosServicioPopup.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&campoObrigatorio=False&SelecionaColecaoClientes=false&SelecaoUnica=True&vigente=True"

    '            'AbrirPopupModal
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_clientes", "AbrirPopupModal('" & url & "', 550, 788,'btnBuscarPuntoServicio');", True)

    '        End If

    '    Catch ex As Exception
    '        MyBase.MostraMensagemExcecao(ex)
    '    End Try

    'End Sub

    Private Sub btnAceptar_Click(sender As Object, e As System.EventArgs) Handles btnAceptar.Click
        Try
            ValidarCamposObrigatorios = True

            Dim strErros As String = MontaMensagensErro(True)
            If strErros.Length > 0 Then
                MyBase.MostraMensagem(strErros)
                Exit Sub
            End If

            Respuesta = New RespuestaBusqueda()
            Respuesta.Peticion = Peticion
            Respuesta.Cliente = ClienteSelecionado

            If SubClientesSelecionados IsNot Nothing Then
                Respuesta.SubCliente = SubClientesSelecionados.FirstOrDefault()
            End If

            If PuntoServicioSelecionado IsNot Nothing Then
                Respuesta.PuntoServicio = PuntoServicioSelecionado
            End If

            Respuesta.SubCanales = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
            If SubCanalSelecionado IsNot Nothing Then
                Respuesta.SubCanales.Add(SubCanalSelecionado)
            Else
                Respuesta.SubCanales.AddRange(ListaSubCanal)
            End If
            Respuesta.ListaCompeltaSubCanales.AddRange(ListaSubCanal)

            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "BusquedaNivelOk", "FecharAtualizarPaginaPai();", True)
            Dim jsScript As String = "window.parent.FecharModal('#" & divModal & "','#" & ifrModal & "','" & btnExecutar & "');"
            ' fechar janela
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "ToleranciaOk", jsScript, True)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click
        Try
            LimparCliente()
            LimparSubCanal()
            ValidarCamposObrigatorios = False
            
            Dim objCliente As New Prosegur.Genesis.Comon.Clases.Cliente
            Clientes.Clear()
            Clientes.Add(objCliente)
            AtualizaDadosHelperCliente(Clientes)
            
            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub ddlSubCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCanal.SelectedIndexChanged

        SubCanalSelecionado = ListaSubCanal.Where(Function(s) s.Codigo = ddlSubCanal.SelectedValue).FirstOrDefault

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Public Shared Property Peticion As PeticionBusqueda
        Get
            Return HttpContext.Current.Session("objEntidade")
        End Get
        Set(value As PeticionBusqueda)
            HttpContext.Current.Session("objEntidade") = value
        End Set
    End Property

    Public Shared Property Respuesta As RespuestaBusqueda
        Get
            Return HttpContext.Current.Session("objRespuesta")
        End Get
        Set(value As RespuestaBusqueda)
            HttpContext.Current.Session("objRespuesta") = value
        End Set
    End Property

    Private Property ClienteSelecionado As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboClientes.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

    Private Property SubClientesSelecionados As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
        Get
            Return ViewState("SubClientesSelecionados")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion)
            ViewState("SubClientesSelecionados") = value
        End Set
    End Property

    Private Property PuntoServicioSelecionado As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Get
            Return ViewState("PuntoServicioSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio)
            ViewState("PuntoServicioSelecionado") = value
        End Set
    End Property
    Private Property SubCanalSelecionado As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal
        Get
            Return ViewState("SubCanalSelecionado")
        End Get
        Set(value As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal)
            ViewState("SubCanalSelecionado") = value
        End Set
    End Property

    Private Property ValidarCamposObrigatorios() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatorios")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatorios") = value
        End Set
    End Property

    <Serializable()> _
    Public Class PeticionBusqueda

        'REMOVER
        Public Property CodGenesis As String
        Public Property DesGenesis As String
        Public Property OidGenesis As String
        'REMOVER

        Public Property IdentificadorNivelMovimiento As String
        Public Property IdentificadorNivelSaldo As String

    End Class

    <Serializable()> _
    Public Class RespuestaBusqueda

        Public Sub New()
            Me.ListaCompeltaSubCanales = New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion()
        End Sub

        Public Property Peticion As PeticionBusqueda

        Public Property Cliente As ContractoServicio.Utilidad.GetComboClientes.Cliente
        Public Property SubCliente As ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
        Public Property PuntoServicio As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
        Public Property SubCanales As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion
        Public Property ListaCompeltaSubCanales As ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanalColeccion

    End Class

#End Region

#Region "Métodos para auxilixar os novos helpers e nao mudar a integridade dos tipos de dado"
    Private Function GetComboClientes(codCliente) As ContractoServicio.Utilidad.GetComboClientes.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboClientes.Peticion
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        If Not String.IsNullOrEmpty(codCliente) Then
            objPeticion.Codigo = New List(Of String)
            objPeticion.Codigo.Add(codCliente)
        End If

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboClientes(objPeticion)

    End Function
    Private Function GetComboSubClientes(codCliente As String, codSubCliente As String) As ContractoServicio.Utilidad.GetComboSubclientesByCliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.Peticion

        'Cliente
        objPeticion.CodigosClientes = New List(Of String)

        objPeticion.CodigosClientes.Add(codCliente)

        'SubCliente
        objPeticion.CodigoSubcliente = codSubCliente

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboSubclientesByCliente(objPeticion)

    End Function
    Private Function GetComboPuntosServicio(codCliente As String, codSubCliente As String, codPuntoServicio As String) As ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Respuesta

        ' criar objeto peticion
        Dim objPeticion As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.Peticion

        objPeticion.CodigoCliente = codCliente
        objPeticion.CodigoSubcliente = New List(Of String)
        objPeticion.CodigoSubcliente.Add(codSubCliente)
        objPeticion.CodigoPuntoServicio = New List(Of String)
        objPeticion.CodigoPuntoServicio.Add(codPuntoServicio)

        ' criar objeto proxy
        Dim objProxy As New Comunicacion.ProxyUtilidad

        ' chamar servicio
        Return objProxy.GetComboPuntosServiciosByClienteSubcliente(objPeticion)

    End Function
#End Region
End Class