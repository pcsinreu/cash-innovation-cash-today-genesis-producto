Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comunicacion
Imports Prosegur.Genesis

Public Class ucClienteSubPto
    Inherits UserControlBase

#Region "[Propriedades]"

    Public Property MultiSelecaoCliente As Boolean
        Get
            Return ViewState("MultiSelecaoCliente")
        End Get
        Set(value As Boolean)
            ViewState("MultiSelecaoCliente") = value
        End Set
    End Property

    Public Property MultiSelecaoSubCliente As Boolean
        Get
            Return ViewState("MultiSelecaoSubCliente")
        End Get
        Set(value As Boolean)
            ViewState("MultiSelecaoSubCliente") = value
        End Set
    End Property

    Public Property MultiSelecaoPuntoServicio As Boolean
        Get
            Return ViewState("MultiSelecaoPuntoServicio")
        End Get
        Set(value As Boolean)
            ViewState("MultiSelecaoPuntoServicio") = value
        End Set
    End Property

    Public Property MultiSelecaoGrupoCliente As Boolean
        Get
            Return ViewState("MultiSelecaoGrupoCliente")
        End Get
        Set(value As Boolean)
            If lstGrupoCliente IsNot Nothing Then
                If value Then
                    lstGrupoCliente.SelectionMode = ListSelectionMode.Multiple
                Else
                    Selecionados.GrupoClienteSeleccionados = Nothing
                    CarregarListBoxGrupoClientes()
                    lstGrupoCliente.SelectionMode = ListSelectionMode.Single
                End If
            End If

            ViewState("MultiSelecaoGrupoCliente") = value
        End Set
    End Property

    Public Property SubclienteVisivel As Boolean
        Get
            Return ViewState("SubclienteVisivel")
        End Get
        Set(value As Boolean)
            ViewState("SubclienteVisivel") = value
        End Set
    End Property

    Public Property PtoServicoVisivel As Boolean
        Get
            Return ViewState("PtoServicoVisivel")
        End Get
        Set(value As Boolean)
            ViewState("PtoServicoVisivel") = value
        End Set
    End Property

    Public Property GrupoClienteVisivel As Boolean
        Get
            Return ViewState("GrupoClienteVisivel")
        End Get
        Set(value As Boolean)
            ViewState("GrupoClienteVisivel") = value
        End Set
    End Property

    Public Property ClienteVisivel As Boolean
        Get
            Return ViewState("ClienteVisivel")
        End Get
        Set(value As Boolean)
            ViewState("ClienteVisivel") = value
        End Set
    End Property

    Public Property CampoObrigatorioCliente As Boolean
        Get
            Return ViewState("CampoObrigatorioCliente")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorioCliente") = value
        End Set
    End Property

    Public Property CampoObrigatorioSubCliente As Boolean
        Get
            Return ViewState("CampoObrigatorioSubCliente")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorioSubCliente") = value
        End Set
    End Property

    Public Property CampoObrigatorioPuntoServicio As Boolean
        Get
            Return ViewState("CampoObrigatorioPuntoServicio")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorioPuntoServicio") = value
        End Set
    End Property

    Public Property CampoObrigatorioGrupoCliente As Boolean
        Get
            Return ViewState("CampoObrigatorioGrupoCliente")
        End Get
        Set(value As Boolean)
            ViewState("CampoObrigatorioGrupoCliente") = value
        End Set
    End Property

    Public Property Selecionados() As RespuestaCliSubPto
        Get
            If ViewState("RespuestaSeleccionados") Is Nothing Then
                ViewState("RespuestaSeleccionados") = New RespuestaCliSubPto
            End If
            Return ViewState("RespuestaSeleccionados")
        End Get
        Set(value As RespuestaCliSubPto)
            ViewState("RespuestaSeleccionados") = value
        End Set
    End Property


    Public Property MostrarJanelaCarregar() As Boolean
        Get
            Return ViewState("MostrarJanelaCarregar")
        End Get
        Set(value As Boolean)
            ViewState("MostrarJanelaCarregar") = value
        End Set
    End Property
    Public ReadOnly Property Respuesta() As RespuestaCliSubPto
        Get
            If CampoObrigatorioCliente AndAlso CampoObrigatorioGrupoCliente AndAlso _
                (Selecionados.GrupoClienteSeleccionados Is Nothing OrElse Selecionados.GrupoClienteSeleccionados.Count = 0) AndAlso _
                (Selecionados.ClientesSeleccionados Is Nothing OrElse Selecionados.ClientesSeleccionados.Count = 0) Then

                cvGrupoCliente.IsValid = False
                cvCliente.IsValid = False
                Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblGrupoCliente.Text))
                Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblCliente.Text))

            ElseIf Selecionados.ClientesSeleccionados IsNot Nothing AndAlso Selecionados.ClientesSeleccionados.Count > 0 Then
                Selecionados.GrupoClienteSeleccionados = Nothing
                If lbCliente.Items.Count = 0 AndAlso CampoObrigatorioCliente Then
                    Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblCliente.Text))
                    cvCliente.IsValid = False
                    Return Nothing
                End If
                If lbSubCliente.Items.Count = 0 AndAlso CampoObrigatorioSubCliente Then
                    Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblSubCliente.Text))
                    cvSubCliente.IsValid = False
                    Return Nothing
                End If
                If lbPtoServico.Items.Count = 0 AndAlso CampoObrigatorioPuntoServicio Then
                    Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblPtoServico.Text))
                    cvPtoServico.IsValid = False
                    Return Nothing
                End If

            ElseIf Selecionados.GrupoClienteSeleccionados IsNot Nothing AndAlso Selecionados.GrupoClienteSeleccionados.Count > 0 Then
                Selecionados.ClientesSeleccionados = Nothing
                Selecionados.SubClientesSeleccionados = Nothing
                Selecionados.PuntoServicioSeleccionados = Nothing
                Selecionados.BolTodosSubClientesSeleccionados = False
                Selecionados.BolTodosPuntoServicioSeleccionados = False
            ElseIf CampoObrigatorioGrupoCliente Then
                Me.EnviarMensagemErro(String.Format(Traduzir("err_campo_obrigatorio"), lblGrupoCliente.Text))
                cvGrupoCliente.IsValid = False
                Return Nothing
            End If
            Return Selecionados
        End Get
    End Property


    Dim WithEvents ucBuscaCliente As ucClientes

    Private WithEvents _ucPopUp As Popup2
    Public Property ucPopup() As Popup2
        Get
            If _ucPopUp Is Nothing Then
                _ucPopUp = LoadControl("~\Controles\Helpers\Popup2.ascx")
                _ucPopUp.ID = Me.ID & "_ucPopup"
                _ucPopUp.EsModal = True
                _ucPopUp.AutoAbrirPopup = False
                phUcPopUp.Controls.Add(_ucPopUp)
              End If
            Return _ucPopUp
        End Get
        Set(value As Popup2)
            _ucPopUp = value
        End Set
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If Gruposclientes Is Nothing OrElse Gruposclientes.Count = 0 Then
                PreencherListBoxGrupoClientes()
            End If

            ''carregacontroles
            CarregarListBoxClientes()
            CarregarListBoxGrupoClientes()
            CarregarListBoxPtoServico()
            CarregarListBoxSubClientes()

            If MultiSelecaoGrupoCliente Then
                lstGrupoCliente.SelectionMode = ListSelectionMode.Multiple
            Else
                lstGrupoCliente.SelectionMode = ListSelectionMode.Single
            End If

            ucBuscaCliente = LoadControl("~/Controles/ucClientes.ascx")
            ucPopup.PopupBase = ucBuscaCliente
            ucPopup.Height = 500
            ucPopup.Width = 800
            ' AddHandler ucBuscaCliente.OcorreuErro, AddressOf ErroPopupBusqueda

            pnCliente.Visible = ClienteVisivel

            If ClienteVisivel Then
                pnSubCliente.Visible = SubclienteVisivel
                If SubclienteVisivel AndAlso PtoServicoVisivel Then
                    pnPtoServico.Visible = PtoServicoVisivel
                Else
                    pnPtoServico.Visible = False
                End If
            Else
                pnSubCliente.Visible = False
                pnPtoServico.Visible = False
            End If

            pnGrupoCliente.Visible = GrupoClienteVisivel

            TraduzirControles()

            If Selecionados IsNot Nothing Then
                Selecionados = Selecionados
                If Selecionados.ClientesSeleccionados IsNot Nothing AndAlso Selecionados.ClientesSeleccionados.Count > 0 _
                AndAlso Selecionados.SubClientesSeleccionados IsNot Nothing AndAlso Selecionados.SubClientesSeleccionados.Count > 0 Then
                    btnBuscaSubCliente.Enabled = True
                    imgButtonLimpaSubCliente.Enabled = True
                    imgButtonLimpaTodoSubCliente.Enabled = True

                    If Selecionados.PuntoServicioSeleccionados IsNot Nothing AndAlso Selecionados.PuntoServicioSeleccionados.Count > 0 Then
                        btnBuscaPtoServicio.Enabled = True
                        imgButtonLimpaPtoServicio.Enabled = True
                        imgButtonLimpaTodoPtoServicio.Enabled = True
                    End If
                End If
            End If

            Selecionados.BolTodosSubClientesSeleccionados = chkTodosSubCliente.Checked

            'mostrar janela carregamento
            If Not MostrarJanelaCarregar Then
                upCarregar.Visible = False
            End If
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try

    End Sub

    Public Sub New()
        Try
            CampoObrigatorioGrupoCliente = False
            MultiSelecaoCliente = True
            MultiSelecaoSubCliente = True
            MultiSelecaoPuntoServicio = True
            MultiSelecaoGrupoCliente = True
            SubclienteVisivel = False
            PtoServicoVisivel = False
            GrupoClienteVisivel = False
            CampoObrigatorioCliente = False
            CampoObrigatorioPuntoServicio = False
            CampoObrigatorioSubCliente = False
            MostrarJanelaCarregar = True

        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

#Region "[EVENTOS]"

#Region "[EVENTOS - BOTÕES]"

    Protected Sub btnBuscaCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaCliente.Click
        Try
            ucBuscaCliente.MultiSelecaoCliente = MultiSelecaoCliente
            ucBuscaCliente.InicializarControle(Selecionados.ClientesSeleccionados)
            ucPopup.AbrirPopup()
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaCliente_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaCliente.Click
        Try
            LimpiarListBoxCliente()
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaTodoCliente_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaTodoCliente.Click
        Try
            LimpiarListBoxCliente(True)
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscaSubCliente_Click(sender As Object, e As EventArgs) Handles btnBuscaSubCliente.Click
        Try

            ucBuscaCliente.MultiSelecaoSubCliente = MultiSelecaoSubCliente
            ucBuscaCliente.InicializarControle(Selecionados.ClientesSeleccionados, Selecionados.SubClientesSeleccionados, ucClientes.ETipoBusca.SubCliente)
            ucPopup.AbrirPopup()
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaSubCliente_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaSubCliente.Click
        Try
            LimpiarListBoxSubCliente()
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaTodoSubCliente_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaTodoSubCliente.Click
        Try
            LimpiarListBoxSubCliente(True)
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscaPtoServicio_Click(sender As Object, e As EventArgs) Handles btnBuscaPtoServicio.Click
        Try

            ucBuscaCliente.MultiSelecaoPuntoServicio = MultiSelecaoPuntoServicio
            ucBuscaCliente.InicializarControle(Selecionados.SubClientesSeleccionados, Selecionados.PuntoServicioSeleccionados, ucClientes.ETipoBusca.PontoServico)
            ucPopup.AbrirPopup()
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaPtoServicio_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaPtoServicio.Click
        Try
            LimpiarListBoxPtoServicio()
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub imgButtonLimpaTodoPtoServicio_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaTodoPtoServicio.Click
        Try
            LimpiarListBoxPtoServicio(True)
            CargarClientesPreSeleccionados(Selecionados)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#End Region

    Private Sub ucBuscaCliente_Fechado(sender As Object, e As PopupEventArgs) Handles ucBuscaCliente.Fechado
        Try
            If e.Resultado IsNot Nothing Then
                Select Case ucBuscaCliente.TipoDeBusca
                    Case ucClientes.ETipoBusca.Cliente
                        Dim clie = DirectCast(e.Resultado, IAC.ContractoServicio.Utilidad.GetComboClientes.ClienteColeccion)
                        Selecionados.ClientesSeleccionados = clie
                        Selecionados.GrupoClienteSeleccionados = Nothing
                        CarregarListBoxGrupoClientes()
                        Selecionados.SubClientesSeleccionados = Nothing
                        Selecionados.PuntoServicioSeleccionados = Nothing
                        CarregarListBoxClientes()
                        CarregarListBoxSubClientes()
                        CarregarListBoxPtoServico()
                        If clie IsNot Nothing AndAlso clie.Count > 0 Then
                            btnBuscaSubCliente.Enabled = True
                            btnBuscaPtoServicio.Enabled = False
                            chkTodosSubCliente.Enabled = True
                        ElseIf chkTodosSubCliente.Checked Then
                            chkTodosSubCliente.Checked = False
                        End If
                    Case ucClientes.ETipoBusca.SubCliente
                        Dim subCliente = DirectCast(e.Resultado, IAC.ContractoServicio.Utilidad.GetComboSubclientesByCliente.ClienteColeccion)
                        Selecionados.SubClientesSeleccionados = subCliente
                        Selecionados.PuntoServicioSeleccionados = Nothing
                        CarregarListBoxSubClientes()
                        CarregarListBoxPtoServico()
                        If subCliente IsNot Nothing AndAlso subCliente.Count > 0 Then
                            btnBuscaPtoServicio.Enabled = True
                            chkTodosPtosServico.Enabled = True
                        ElseIf chkTodosPtosServico.Checked Then
                            chkTodosPtosServico.Checked = False
                        End If
                    Case ucClientes.ETipoBusca.PontoServico
                        Dim ptoServico = DirectCast(e.Resultado, IAC.ContractoServicio.Utilidad.getComboPuntosServiciosByClientesSubclientes.ClienteColeccion)
                        Selecionados.PuntoServicioSeleccionados = ptoServico
                        CarregarListBoxPtoServico()
                        If chkTodosPtosServico.Checked AndAlso ptoServico IsNot Nothing AndAlso ptoServico.Count > 0 Then
                            chkTodosPtosServico.Checked = False
                        End If
                End Select
            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try

    End Sub

    Public Sub CargarClientesPreSeleccionados(peticion As RespuestaCliSubPto)
        Try
            If peticion IsNot Nothing Then
                Selecionados = peticion
                If Selecionados.ClientesSeleccionados IsNot Nothing AndAlso Selecionados.ClientesSeleccionados.Count > 0 Then
                    CarregarListBoxClientes()
                    btnBuscaSubCliente.Enabled = True

                    chkTodosSubCliente.Checked = Selecionados.BolTodosSubClientesSeleccionados
                    If Not Selecionados.BolTodosSubClientesSeleccionados Then
                        If Selecionados.SubClientesSeleccionados IsNot Nothing AndAlso Selecionados.SubClientesSeleccionados.Count > 0 Then
                            CarregarListBoxSubClientes()
                            btnBuscaPtoServicio.Enabled = True
                            If Selecionados.BolTodosPuntoServicioSeleccionados Then
                                chkTodosPtosServico.Checked = Selecionados.BolTodosPuntoServicioSeleccionados
                            Else
                                If Selecionados.PuntoServicioSeleccionados IsNot Nothing AndAlso Selecionados.PuntoServicioSeleccionados.Count > 0 Then
                                    CarregarListBoxPtoServico()
                                End If
                            End If
                        End If
                    End If
                Else
                    CarregarListBoxGrupoClientes()
                End If
            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub TraduzirControles()
        lblCliente.Text = Traduzir("028_lblCliente")
        lblSubCliente.Text = Traduzir("028_lblSubcliente")
        lblPtoServico.Text = Traduzir("028_lblPuntoServicio")
        chkTodosPtosServico.Text = Traduzir("gen_todos")
        chkTodosSubCliente.Text = Traduzir("gen_todos")
        lblGrupoCliente.Text = Traduzir("028_lblGrupoClientes")
        btnBuscaCliente.Text = Traduzir("028_btnBuscaCliente")
        btnBuscaSubCliente.Text = Traduzir("028_btnBuscaSubCliente")
        btnBuscaPtoServicio.Text = Traduzir("028_btnBuscaPtoServicio")
    End Sub

    Public Sub CarregarListBoxClientes()
        Try

            If Selecionados.ClientesSeleccionados IsNot Nothing AndAlso Selecionados.ClientesSeleccionados.Count > 0 Then
                Dim clientes = (From p In Selecionados.ClientesSeleccionados
                               Select Codigo = p.Codigo, Codigo_Descricao = p.Codigo & " " & p.Descripcion).ToList()


                lbCliente.DataTextField = "Codigo_Descricao"
                lbCliente.DataValueField = "Codigo"
                lbCliente.DataSource = clientes
                lbCliente.DataBind()
                imgButtonLimpaCliente.Enabled = True
                imgButtonLimpaTodoCliente.Enabled = True
            Else
                lbCliente.Items.Clear()
                lbCliente.DataSource = Nothing
                lbCliente.DataBind()
                imgButtonLimpaCliente.Enabled = False
                imgButtonLimpaTodoCliente.Enabled = False
            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Public Sub CarregarListBoxSubClientes()
        Try

            If Selecionados.SubClientesSeleccionados IsNot Nothing AndAlso Selecionados.SubClientesSeleccionados.Count > 0 Then

                Dim Subclientes = (From p In Selecionados.SubClientesSeleccionados
                                Where p.SubClientes IsNot Nothing
                               Select p)
                Dim resultado = (From c In Selecionados.SubClientesSeleccionados
                                 From s In c.SubClientes
                                 Select Codigo = s.Codigo, Codigo_Descricao = c.Codigo & " - " & s.Codigo & " " & s.Descripcion).ToList()

                lbSubCliente.DataTextField = "Codigo_Descricao"
                lbSubCliente.DataValueField = "Codigo"
                lbSubCliente.DataSource = resultado
                lbSubCliente.DataBind()
                imgButtonLimpaSubCliente.Enabled = True
                imgButtonLimpaTodoSubCliente.Enabled = True
            Else
                lbSubCliente.Items.Clear()
                lbSubCliente.DataSource = Nothing
                lbSubCliente.DataBind()
                imgButtonLimpaSubCliente.Enabled = False
                imgButtonLimpaTodoSubCliente.Enabled = False
            End If

            Session("SubClientesSeleccionados") = Selecionados.SubClientesSeleccionados

        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Public Sub CarregarListBoxPtoServico()
        Try

            If Selecionados.PuntoServicioSeleccionados IsNot Nothing AndAlso Selecionados.SubClientesSeleccionados.Count > 0 Then
                Dim subClientes = (From p In Selecionados.PuntoServicioSeleccionados
                                Where p.SubClientes IsNot Nothing
                               Select p)

                Dim ptoServicos = From c In subClientes
                                  From s In c.SubClientes
                                  Where s.PuntosServicio IsNot Nothing
                                  Select c

                Dim resultado = (From c In ptoServicos
                                 From s In c.SubClientes
                                 From p In s.PuntosServicio
                                 Select Codigo = p.Codigo, Codigo_Descricao = c.Codigo & " - " & s.Codigo & " - " & p.Codigo & " " & p.Descripcion).Distinct().ToList()



                lbPtoServico.DataTextField = "Codigo_Descricao"
                lbPtoServico.DataValueField = "Codigo"
                lbPtoServico.DataSource = resultado
                lbPtoServico.DataBind()
                imgButtonLimpaPtoServicio.Enabled = True
                imgButtonLimpaTodoPtoServicio.Enabled = True
            Else
                lbPtoServico.Items.Clear()
                lbPtoServico.DataSource = Nothing
                lbPtoServico.DataBind()
                imgButtonLimpaPtoServicio.Enabled = False
                imgButtonLimpaTodoPtoServicio.Enabled = False
            End If


        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub chkTodosSubCliente_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodosSubCliente.CheckedChanged
        Try
            If Selecionados.ClientesSeleccionados IsNot Nothing AndAlso Selecionados.ClientesSeleccionados.Count > 0 Then
                If chkTodosSubCliente.Checked Then
                    Selecionados.SubClientesSeleccionados = Nothing
                    Selecionados.PuntoServicioSeleccionados = Nothing
                    Selecionados.BolTodosSubClientesSeleccionados = True
                    lbSubCliente.Items.Clear()
                    lbSubCliente.DataSource = Nothing
                    lbSubCliente.DataBind()
                    lbPtoServico.Items.Clear()
                    lbPtoServico.DataSource = Nothing
                    lbPtoServico.DataBind()
                    btnBuscaPtoServicio.Enabled = False
                    imgButtonLimpaPtoServicio.Enabled = False
                    imgButtonLimpaTodoPtoServicio.Enabled = False
                    btnBuscaSubCliente.Enabled = False
                    imgButtonLimpaSubCliente.Enabled = False
                    imgButtonLimpaTodoSubCliente.Enabled = False
                    chkTodosPtosServico.Enabled = True
                Else
                    btnBuscaSubCliente.Enabled = True
                    chkTodosPtosServico.Enabled = False
                    Selecionados.BolTodosSubClientesSeleccionados = False
                End If
            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Sub chkTodosPtosServico_CheckedChanged(sender As Object, e As EventArgs) Handles chkTodosPtosServico.CheckedChanged
        Try
            If (Selecionados.SubClientesSeleccionados IsNot Nothing AndAlso Selecionados.SubClientesSeleccionados.Count > 0) OrElse Selecionados.BolTodosSubClientesSeleccionados Then

                If chkTodosPtosServico.Checked Then
                    Selecionados.BolTodosPuntoServicioSeleccionados = True
                    Selecionados.PuntoServicioSeleccionados = Nothing
                    lbPtoServico.Items.Clear()
                    lbPtoServico.DataSource = Nothing
                    lbPtoServico.DataBind()
                    btnBuscaPtoServicio.Enabled = False
                    imgButtonLimpaPtoServicio.Enabled = False
                    imgButtonLimpaTodoPtoServicio.Enabled = False
                Else
                    If True Then

                    End If

                    btnBuscaPtoServicio.Enabled = True
                    Selecionados.BolTodosPuntoServicioSeleccionados = True
                End If
            Else
                chkTodosPtosServico.Checked = False
            End If
        Catch ex As Excepcion.NegocioExcepcion
            Me.EnviarMensagemErro(ex.Descricao)
        Catch ex As Exception
            Me.EnviarMensagemErro(ex.Message)
        End Try
    End Sub

    Private Sub ErroPopupBusqueda(sender As Object, e As ExcepcionEventArgs)
        Me.EnviarMensagemErro(e.Erro)
        Me.ucBuscaCliente.FecharPopup()
    End Sub

    Public Function RecuperarClienteSubclientePuntoServicioGrupo() As IAC.ContractoServicio.GrupoCliente.GrupoClienteDetalleColeccion
        If Selecionados.GrupoClienteSeleccionados IsNot Nothing AndAlso Selecionados.GrupoClienteSeleccionados.Count > 0 Then
            Dim objProxy As New ProxyGrupoClientes
            Dim peticion As New IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Peticion
            peticion.Codigo = New List(Of String)
            peticion.Codigo.AddRange((From p In Selecionados.GrupoClienteSeleccionados
                                     Select p.Codigo).ToList())

            Dim respuesta As IAC.ContractoServicio.GrupoCliente.GetGruposClientesDetalle.Respuesta = objProxy.GetGruposClientesDetalle(peticion)
            If respuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
                Me.EnviarMensagemErro(respuesta.MensajeError)
                Return Nothing
            End If

            Return respuesta.GrupoCliente
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' Preenche o listbox de grupo de clientes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PreencherListBoxGrupoClientes()

        Dim objProxyGrupoClientes As New ProxyGrupoClientes
        Dim objRespuesta As New IAC.ContractoServicio.GrupoCliente.GetGruposCliente.Respuesta


        ' petição
        Dim peticion As New IAC.ContractoServicio.GrupoCliente.GetGruposCliente.Peticion
        peticion.GrupoCliente = New IAC.ContractoServicio.GrupoCliente.GrupoCliente
        peticion.ParametrosPaginacion.RealizarPaginacion = False

        With peticion.GrupoCliente
            .Vigente = True
        End With


        objRespuesta = objProxyGrupoClientes.GetGruposCliente(peticion)

        If objRespuesta.CodigoError <> Excepcion.Constantes.CONST_CODIGO_SEM_ERROR_DEFAULT Then
            Me.EnviarMensagemErro(objRespuesta.MensajeError)
            Exit Sub
        End If


        'Preenche a propriedade de grupo de clientes.
        Gruposclientes = objRespuesta.GruposCliente

        CarregarListBoxGrupoClientes()

    End Sub


    Private Property Gruposclientes() As IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion
        Get
            Return ViewState("Gruposclientes")
        End Get
        Set(value As IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion)
            ViewState("Gruposclientes") = value
        End Set
    End Property

    Protected Sub lstGrupoCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstGrupoCliente.SelectedIndexChanged
        If Selecionados.GrupoClienteSeleccionados Is Nothing OrElse Selecionados.GrupoClienteSeleccionados.Count = 0 Then
            btnBuscaSubCliente.Enabled = False
            btnBuscaPtoServicio.Enabled = False
            chkTodosPtosServico.Checked = False
            chkTodosSubCliente.Checked = False
            chkTodosPtosServico.Enabled = False
            chkTodosSubCliente.Enabled = False
            Selecionados.SubClientesSeleccionados = Nothing
            Selecionados.PuntoServicioSeleccionados = Nothing
            Selecionados.ClientesSeleccionados = Nothing
            Selecionados.BolTodosSubClientesSeleccionados = False
            Selecionados.BolTodosPuntoServicioSeleccionados = False
            CarregarListBoxClientes()
            CarregarListBoxPtoServico()
            CarregarListBoxSubClientes()

        End If
        GuardaGrupoClienteSelecionados()
    End Sub

    Private Sub GuardaGrupoClienteSelecionados()
        Dim selected = lstGrupoCliente.GetSelectedIndices()
        Selecionados.GrupoClienteSeleccionados = New IAC.ContractoServicio.GrupoCliente.GrupoClienteColeccion

        For Each objGrupo As ListItem In (From gc As ListItem In lstGrupoCliente.Items Where gc.Selected)
            Dim objGrupoLocal = objGrupo

            Dim grupoCliente = (From gc In Gruposclientes Where gc.Codigo = objGrupoLocal.Value).FirstOrDefault
            Selecionados.GrupoClienteSeleccionados.Add(grupoCliente)

        Next
    End Sub

    Private Sub CarregarListBoxGrupoClientes()
        lstGrupoCliente.AppendDataBoundItems = True
        lstGrupoCliente.Items.Clear()

        If Gruposclientes IsNot Nothing AndAlso Gruposclientes.Count > 0 Then
            'ordena a lista de canales
            Gruposclientes.Sort(Function(i, j) i.Codigo.CompareTo(j.Codigo))

            For Each objGrupoClientes In Gruposclientes
                Dim objGrupoClientesLocal = objGrupoClientes
                If Selecionados.GrupoClienteSeleccionados IsNot Nothing Then
                    Dim result = From p In Selecionados.GrupoClienteSeleccionados
                             Where p.Codigo = objGrupoClientesLocal.Codigo

                    If result IsNot Nothing _
                        AndAlso result.Count() > 0 _
                        AndAlso (Selecionados.ClientesSeleccionados Is Nothing _
                                 OrElse Selecionados.ClientesSeleccionados.Count = 0) Then
                        Dim item = New ListItem(objGrupoClientes.Codigo & " - " & objGrupoClientes.Descripcion, objGrupoClientes.Codigo)
                        item.Selected = True
                        lstGrupoCliente.Items.Add(item)
                    Else
                        lstGrupoCliente.Items.Add(New ListItem(objGrupoClientes.Codigo & " - " & objGrupoClientes.Descripcion, objGrupoClientes.Codigo))
                    End If
                Else
                    lstGrupoCliente.Items.Add(New ListItem(objGrupoClientes.Codigo & " - " & objGrupoClientes.Descripcion, objGrupoClientes.Codigo))
                End If
            Next

        End If
    End Sub

    Public Sub Limpiar()
        Selecionados.SubClientesSeleccionados = Nothing
        Selecionados.GrupoClienteSeleccionados = Nothing
        Selecionados.PuntoServicioSeleccionados = Nothing
        Selecionados.ClientesSeleccionados = Nothing
        Selecionados.BolTodosPuntoServicioSeleccionados = False
        Selecionados.BolTodosSubClientesSeleccionados = False
        CarregarListBoxClientes()
        CarregarListBoxGrupoClientes()
        CarregarListBoxPtoServico()
        CarregarListBoxSubClientes()
    End Sub

    Private Sub LimpiarListBoxCliente(Optional limpiarTodo As Boolean = False)
        If lbCliente IsNot Nothing AndAlso lbCliente.Items IsNot Nothing AndAlso lbCliente.Items.Count > 0 Then
            If limpiarTodo Then
                lbCliente.Items.Clear()
                Selecionados.ClientesSeleccionados.Clear()
                LimpiarListBoxSubCliente(True)
                LimpiarListBoxPtoServicio(True)
            Else
                If lbCliente.SelectedItem IsNot Nothing Then
                    Dim listaItens As New ListItemCollection
                    For Each item As ListItem In lbCliente.Items
                        listaItens.Add(item)
                    Next
                    For Each item As ListItem In listaItens
                        If item.Selected Then
                            If Selecionados.PuntoServicioSeleccionados IsNot Nothing Then
                                Selecionados.PuntoServicioSeleccionados.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                                If Selecionados.PuntoServicioSeleccionados.Count = 0 Then
                                    lbPtoServico.Items.Clear()
                                    imgButtonLimpaPtoServicio.Enabled = False
                                    imgButtonLimpaTodoPtoServicio.Enabled = False
                                End If
                            End If
                            If Selecionados.SubClientesSeleccionados IsNot Nothing Then
                                Selecionados.SubClientesSeleccionados.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                                If Selecionados.SubClientesSeleccionados.Count = 0 Then
                                    lbSubCliente.Items.Clear()
                                    imgButtonLimpaSubCliente.Enabled = False
                                    imgButtonLimpaTodoSubCliente.Enabled = False
                                End If
                            End If
                            If lbSubCliente.Items.Count = 0 Then
                                btnBuscaSubCliente.Enabled = False
                            End If
                            If lbPtoServico.Items.Count = 0 Then
                                btnBuscaPtoServicio.Enabled = False
                            End If
                            If Selecionados.ClientesSeleccionados IsNot Nothing Then
                                Selecionados.ClientesSeleccionados.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                            End If
                            lbCliente.Items.Remove(item)
                        End If
                    Next
                End If
            End If
        End If
        If lbCliente.Items.Count = 0 Then
            imgButtonLimpaCliente.Enabled = False
            imgButtonLimpaTodoCliente.Enabled = False
            btnBuscaSubCliente.Enabled = False
            chkTodosSubCliente.Enabled = False
        End If
    End Sub

    Private Sub LimpiarListBoxSubCliente(Optional limpiarTodo As Boolean = False)
        If lbSubCliente IsNot Nothing AndAlso lbSubCliente.Items IsNot Nothing AndAlso lbSubCliente.Items.Count > 0 Then
            If limpiarTodo Then
                lbSubCliente.Items.Clear()
                Selecionados.SubClientesSeleccionados.Clear()
                LimpiarListBoxPtoServicio(True)
            Else
                If lbSubCliente.SelectedItem IsNot Nothing Then
                    Dim listaItens As New ListItemCollection
                    For Each item As ListItem In lbSubCliente.Items
                        listaItens.Add(item)
                    Next
                    For Each item As ListItem In listaItens
                        If item.Selected Then
                            If Selecionados.PuntoServicioSeleccionados IsNot Nothing Then
                                For Each ptoServicio In Selecionados.PuntoServicioSeleccionados
                                    ptoServicio.SubClientes.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                                Next
                                Selecionados.PuntoServicioSeleccionados.RemoveAll(Function(a) a.SubClientes.All(Function(b) b.Codigo.Equals(item.Value)))
                                If Selecionados.PuntoServicioSeleccionados.Count = 0 Then
                                    lbPtoServico.Items.Clear()
                                    imgButtonLimpaPtoServicio.Enabled = False
                                    imgButtonLimpaTodoPtoServicio.Enabled = False
                                End If
                            End If
                            If lbPtoServico.Items.Count = 0 Then
                                btnBuscaPtoServicio.Enabled = False
                            End If
                            If Selecionados.SubClientesSeleccionados IsNot Nothing Then
                                For Each subcliente In Selecionados.SubClientesSeleccionados
                                    subcliente.SubClientes.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                                Next
                                Selecionados.SubClientesSeleccionados.RemoveAll(Function(a) a.SubClientes.All(Function(b) b.Codigo.Equals(item.Value)))
                            End If
                            lbSubCliente.Items.Remove(item)
                        End If
                    Next
                End If
            End If
        End If
        If lbSubCliente.Items.Count = 0 Then
            imgButtonLimpaSubCliente.Enabled = False
            imgButtonLimpaTodoSubCliente.Enabled = False
            btnBuscaPtoServicio.Enabled = False
            chkTodosPtosServico.Enabled = False
        End If
    End Sub

    Private Sub LimpiarListBoxPtoServicio(Optional limpiarTodo As Boolean = False)
        If lbPtoServico IsNot Nothing AndAlso lbPtoServico.Items IsNot Nothing AndAlso lbPtoServico.Items.Count > 0 Then
            If limpiarTodo Then
                lbPtoServico.Items.Clear()
                Selecionados.PuntoServicioSeleccionados.Clear()
            Else
                If lbPtoServico.SelectedItem IsNot Nothing Then
                    Dim listaItens As New ListItemCollection
                    For Each item As ListItem In lbPtoServico.Items
                        listaItens.Add(item)
                    Next
                    For Each item As ListItem In listaItens
                        If item.Selected Then
                            If Selecionados.PuntoServicioSeleccionados IsNot Nothing Then
                                For Each ptoServicio In Selecionados.PuntoServicioSeleccionados
                                    For Each subCliente In ptoServicio.SubClientes
                                        subCliente.PuntosServicio.RemoveAll(Function(a) a.Codigo.Equals(item.Value))
                                    Next
                                Next
                            End If
                            lbPtoServico.Items.Remove(item)
                        End If
                    Next
                End If
            End If
        End If
        If lbPtoServico.Items.Count = 0 Then
            imgButtonLimpaPtoServicio.Enabled = False
            imgButtonLimpaTodoPtoServicio.Enabled = False
        End If
    End Sub

    Public Sub CambiarEstadoChkTodosSubClientes(checked As Boolean)
        chkTodosSubCliente.Checked = checked
    End Sub

    Public Sub CambiarEstadochkTodosPtosServico(checked As Boolean)
        chkTodosPtosServico.Checked = checked
    End Sub

End Class