Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.ContractoServicio.Contractos.Comon.TotalizadorSaldo

Public Class ucTotSaldo
    Inherits System.Web.UI.UserControl


    Public Property IdentificadorCliente As String
    Public Property IdentificadorSubCliente As String
    Public Property IdentificadorPuntoServicio As String
    Public Property strBtnExecutar As String

    Public Property TotalizadoresSaldos() As List(Of Clases.TotalizadorSaldo)
        Get
            If Session(NombreSession) Is Nothing Then
                Session(NombreSession) = New List(Of Clases.TotalizadorSaldo)
            End If
            Return Session(NombreSession)
        End Get
        Set(value As List(Of Clases.TotalizadorSaldo))
            Session(NombreSession) = value
        End Set
    End Property

    Public ReadOnly Property NombreSession() As String
        Get
            Dim nombre As String = Me.Page.ToString() & "_"
            If Not String.IsNullOrEmpty(IdentificadorPuntoServicio) Then
                nombre &= IdentificadorPuntoServicio
            ElseIf Not String.IsNullOrEmpty(IdentificadorSubCliente) Then
                nombre &= IdentificadorSubCliente
            Else
                nombre &= IdentificadorCliente
            End If
            Return nombre
        End Get
    End Property

    Private Master As Master.Master

    Public Event DadosCarregados As System.EventHandler

#Region "[METODOS]"

    Public Sub AtualizaGrid()
        grvTotSaldo.DataSource = Me.TotalizadoresSaldos
        grvTotSaldo.DataBind()
    End Sub

    Public Sub CarregaDados()

        Dim objRespuestaRecuperarTotalizadoresSaldos = RecuperarTotalizadoresSaldos()

        If objRespuestaRecuperarTotalizadoresSaldos.ValidacionesError IsNot Nothing AndAlso objRespuestaRecuperarTotalizadoresSaldos.ValidacionesError.Count > 0 Then
            Master.ControleErro.ShowError(String.Join(Environment.NewLine, objRespuestaRecuperarTotalizadoresSaldos.ValidacionesError.Select(Function(a) a.codigo + " - " + a.descripcion)), False)
        Else
            TotalizadoresSaldos = objRespuestaRecuperarTotalizadoresSaldos.TotalizadoresSaldos.OrderBy(Function(a) a.Cliente.Descripcion).OrderBy(Function(b) b.SubCanales.First.Descripcion).ToList()
        End If

        RaiseEvent DadosCarregados(Me, Nothing)

    End Sub

    Private Function RecuperarTotalizadoresSaldos() As RecuperarTotalizadoresSaldos.Respuesta

        Dim _Proxy As New Comunicacion.ProxyComon
        Dim _Peticion As New RecuperarTotalizadoresSaldos.Peticion

        With _Peticion
            .IdentificadorClienteMovimiento = IdentificadorCliente
            .IdentificadorSubClienteMovimiento = IdentificadorSubCliente
            .IdentificadorPuntoServicioMovimiento = IdentificadorPuntoServicio
        End With

        Return _Proxy.RecuperarTotalizadoresSaldos(_Peticion)

    End Function

    Private Sub TraduzirControles()

        grvTotSaldo.Columns(3).HeaderText = Traduzir("044_lbl_grd_defecto")
        grvTotSaldo.Columns(4).HeaderText = Traduzir("044_lbl_grd_subcanal")
        grvTotSaldo.Columns(5).HeaderText = Traduzir("044_lbl_grd_totalizador")
        grvTotSaldo.Columns(1).HeaderText = Traduzir("044_lbl_grd_cambiar")
        grvTotSaldo.Columns(2).HeaderText = Traduzir("044_lbl_grd_borrar")

    End Sub

    Public Sub Cambiar(rowIndex As Integer)

        Try

            Dim objPage As Base = DirectCast(Me.Page, Base)

            If rowIndex >= 0 Then
                Dim grdRow = grvTotSaldo.Rows(rowIndex)
                Dim lblIdConfigNivelMov As Label = grdRow.FindControl("lblIdConfigNivelMov")
                Dim lblIdConfigNivelSaldo As Label = grdRow.FindControl("lblIdConfigNivelSaldo")
                Dim objTotSaldo As Clases.TotalizadorSaldo = Me.TotalizadoresSaldos.Find(Function(a) a.IdentificadorNivelMovimiento = lblIdConfigNivelMov.Text AndAlso a.IdentificadorNivelSaldo = lblIdConfigNivelSaldo.Text)

                If objTotSaldo IsNot Nothing Then

                    Session("ClienteSelecionado") = Nothing
                    Session("SubClientesSelecionados") = Nothing
                    Session("PuntosServicioSelecionados") = Nothing
                    Session("SubCanalSelecionado") = Nothing
                    BusquedaNivelSaldoPopup.Peticion = New BusquedaNivelSaldoPopup.PeticionBusqueda
                    BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelMovimiento = objTotSaldo.IdentificadorNivelMovimiento
                    BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelSaldo = objTotSaldo.IdentificadorNivelSaldo

                    ' CLIENTE
                    Dim Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
                    Dim SubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
                    Dim PuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio

                    If objTotSaldo.Cliente IsNot Nothing Then
                        Cliente.Codigo = objTotSaldo.Cliente.Codigo
                        Cliente.Descripcion = objTotSaldo.Cliente.Descripcion
                        Cliente.OidCliente = objTotSaldo.Cliente.Identificador
                        Cliente.TotalizadorSaldo = True

                        Session("ClienteSelecionado") = Cliente

                    Else
                        Session("ClienteSelecionado") = Nothing
                    End If

                    ' SUBCLIENTE
                    If objTotSaldo.SubCliente IsNot Nothing Then
                        Dim subClientes As New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubClienteColeccion
                        subClientes.Add(SubCliente)

                        SubCliente.Codigo = objTotSaldo.SubCliente.Codigo
                        SubCliente.Descripcion = objTotSaldo.SubCliente.Descripcion
                        SubCliente.OidSubCliente = objTotSaldo.SubCliente.Identificador
                        SubCliente.TotalizadorSaldo = True

                        Session("SubClientesSelecionados") = subClientes
                    Else
                        Session("SubClientesSelecionados") = Nothing
                    End If

                    ' PUNTO SERVICIO
                    If objTotSaldo.PuntoServicio IsNot Nothing Then
                        Dim puntos As New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicioColeccion
                        puntos.Add(PuntoServicio)
                        PuntoServicio.Codigo = objTotSaldo.PuntoServicio.Codigo
                        PuntoServicio.Descripcion = objTotSaldo.PuntoServicio.Descripcion
                        PuntoServicio.OidPuntoServicio = objTotSaldo.PuntoServicio.Identificador
                        PuntoServicio.TotalizadorSaldo = True

                        Session("PuntosServicioSelecionados") = puntos
                    Else
                        Session("PuntosServicioSelecionados") = Nothing
                    End If

                    If objTotSaldo.SubCanales IsNot Nothing AndAlso objTotSaldo.SubCanales.Count = 1 Then

                        Dim subCanal As New ContractoServicio.Utilidad.GetComboSubcanalesByCanal.SubCanal

                        subCanal.Codigo = objTotSaldo.SubCanales.First.Codigo
                        subCanal.Descripcion = objTotSaldo.SubCanales.First.Descripcion
                        subCanal.OidSubCanal = objTotSaldo.SubCanales.First.Identificador

                        Session("SubCanalSelecionado") = subCanal
                    Else
                        Session("SubCanalSelecionado") = Nothing
                    End If

                End If

            Else
                BusquedaNivelSaldoPopup.Peticion = New BusquedaNivelSaldoPopup.PeticionBusqueda
                Session("ClienteSelecionado") = Nothing
                Session("SubClientesSelecionados") = Nothing
                Session("SubCanalSelecionado") = Nothing
                Session("PuntosServicioSelecionados") = Nothing
            End If

            Dim url As String = "BusquedaNivelSaldoPopup.aspx?acao=" & objPage.Acao & "&campoObrigatorio=False"
            Master.ExibirModal(url, Traduzir("043_lbl_titulo"), 500, 800, False, True, strBtnExecutar)

            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "script_popup_tot_saldo", "AbrirPopupModal('" & url & "', 300, 850,'script_popup_tot_saldo');", True)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub Borrar(rowIndex As Integer)
        Dim objPage As Base = DirectCast(Me.Page, Base)
        Dim grdRow = grvTotSaldo.Rows(rowIndex)
        Dim lblIdConfigNivelMov As Label = grdRow.FindControl("lblIdConfigNivelMov")
        Dim lblIdConfigNivelSaldo As Label = grdRow.FindControl("lblIdConfigNivelSaldo")
        Dim _TotalizadorSaldo As Clases.TotalizadorSaldo = Me.TotalizadoresSaldos.Find(Function(a) a.IdentificadorNivelMovimiento = lblIdConfigNivelMov.Text AndAlso a.IdentificadorNivelSaldo = lblIdConfigNivelSaldo.Text)

        Me.TotalizadoresSaldos.Remove(_TotalizadorSaldo)

        If _TotalizadorSaldo.bolDefecto Then
            If TotalizadoresSaldos IsNot Nothing AndAlso TotalizadoresSaldos.Count > 0 Then
                For Each _totalizador In TotalizadoresSaldos
                    If Aplicacao.Util.Utilidad.CompararTotalizadores(_totalizador.SubCanales, _
                                                             "", _
                                                             "", _
                                                             "", _
                                                             _TotalizadorSaldo.SubCanales, _
                                                             "", _
                                                             "", _
                                                             "") Then
                        _totalizador.bolDefecto = True
                        Exit For
                    End If
                Next
            End If

        End If

        AtualizaGrid()
    End Sub

    Private Sub ConsomeNivelSaldo()

        If BusquedaNivelSaldoPopup.Respuesta IsNot Nothing Then

            If ValidarCambiar(BusquedaNivelSaldoPopup.Respuesta, BusquedaNivelSaldoPopup.Peticion) Then

                If Me.TotalizadoresSaldos Is Nothing Then
                    Me.TotalizadoresSaldos = New List(Of Clases.TotalizadorSaldo)
                End If

                Dim objTotSaldo As Clases.TotalizadorSaldo = Nothing

                'Novo
                If String.IsNullOrEmpty(BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelMovimiento) AndAlso String.IsNullOrEmpty(BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelSaldo) Then
                    objTotSaldo = New Clases.TotalizadorSaldo
                    With objTotSaldo

                        .IdentificadorNivelMovimiento = Guid.NewGuid().ToString
                        .IdentificadorNivelSaldo = Guid.NewGuid().ToString

                        If BusquedaNivelSaldoPopup.Respuesta.Cliente IsNot Nothing Then

                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = BusquedaNivelSaldoPopup.Respuesta.Cliente.OidCliente
                            .Cliente.Codigo = BusquedaNivelSaldoPopup.Respuesta.Cliente.Codigo
                            .Cliente.Descripcion = BusquedaNivelSaldoPopup.Respuesta.Cliente.Descripcion

                            If BusquedaNivelSaldoPopup.Respuesta.SubCliente IsNot Nothing Then

                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = BusquedaNivelSaldoPopup.Respuesta.SubCliente.OidSubCliente
                                .SubCliente.Codigo = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Codigo
                                .SubCliente.Descripcion = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Descripcion

                            End If

                            If BusquedaNivelSaldoPopup.Respuesta.PuntoServicio IsNot Nothing Then

                                .PuntoServicio = New Clases.PuntoServicio
                                .PuntoServicio.Identificador = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.OidPuntoServicio
                                .PuntoServicio.Codigo = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Codigo
                                .PuntoServicio.Descripcion = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Descripcion

                            End If

                            If BusquedaNivelSaldoPopup.Respuesta.SubCanales IsNot Nothing AndAlso BusquedaNivelSaldoPopup.Respuesta.SubCanales.Count > 0 Then

                                .SubCanales = New List(Of Clases.SubCanal)

                                For Each subc In BusquedaNivelSaldoPopup.Respuesta.SubCanales
                                    Dim objSubCanal As New Clases.SubCanal
                                    objSubCanal.Identificador = subc.OidSubCanal
                                    objSubCanal.Codigo = subc.Codigo
                                    objSubCanal.Descripcion = subc.Descripcion

                                    .SubCanales.Add(objSubCanal)
                                Next

                            End If

                            .bolDefecto = True
                            If Me.TotalizadoresSaldos.Count > 0 Then
                                For Each _totalizador In Me.TotalizadoresSaldos
                                    'If _totalizador.SubCanales.Count = 1 Then
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
                                    'End If
                                Next
                            End If

                            Me.TotalizadoresSaldos.Add(objTotSaldo)

                        End If

                    End With

                Else
                    objTotSaldo = Me.TotalizadoresSaldos.Find(Function(a) a.IdentificadorNivelSaldo = BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelSaldo AndAlso a.IdentificadorNivelMovimiento = BusquedaNivelSaldoPopup.Peticion.IdentificadorNivelMovimiento)
                    With objTotSaldo

                        If BusquedaNivelSaldoPopup.Respuesta.Cliente IsNot Nothing Then

                            .Cliente = New Clases.Cliente
                            .Cliente.Identificador = BusquedaNivelSaldoPopup.Respuesta.Cliente.OidCliente
                            .Cliente.Codigo = BusquedaNivelSaldoPopup.Respuesta.Cliente.Codigo
                            .Cliente.Descripcion = BusquedaNivelSaldoPopup.Respuesta.Cliente.Descripcion

                            If BusquedaNivelSaldoPopup.Respuesta.SubCliente IsNot Nothing Then

                                .SubCliente = New Clases.SubCliente
                                .SubCliente.Identificador = BusquedaNivelSaldoPopup.Respuesta.SubCliente.OidSubCliente
                                .SubCliente.Codigo = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Codigo
                                .SubCliente.Descripcion = BusquedaNivelSaldoPopup.Respuesta.SubCliente.Descripcion
                            Else
                                .SubCliente = Nothing
                            End If

                            If BusquedaNivelSaldoPopup.Respuesta.PuntoServicio IsNot Nothing Then

                                .PuntoServicio = New Clases.PuntoServicio
                                .PuntoServicio.Identificador = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.OidPuntoServicio
                                .PuntoServicio.Codigo = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Codigo
                                .PuntoServicio.Descripcion = BusquedaNivelSaldoPopup.Respuesta.PuntoServicio.Descripcion
                            Else
                                .PuntoServicio = Nothing
                            End If

                            If BusquedaNivelSaldoPopup.Respuesta.SubCanales IsNot Nothing Then

                                .SubCanales = New List(Of Clases.SubCanal)

                                For Each subc In BusquedaNivelSaldoPopup.Respuesta.SubCanales
                                    Dim objSubCanal As New Clases.SubCanal
                                    objSubCanal.Identificador = subc.OidSubCanal
                                    objSubCanal.Codigo = subc.Codigo
                                    objSubCanal.Descripcion = subc.Descripcion

                                    .SubCanales.Add(objSubCanal)
                                Next

                            End If

                        End If

                    End With

                End If

                'Remove subcanais de todos
                For Each tot In Me.TotalizadoresSaldos.Where(Function(a) a.SubCanales.Count > 1)

                    tot.SubCanales.Clear()
                    For Each subcTodos In BusquedaNivelSaldoPopup.Respuesta.ListaCompeltaSubCanales
                        Dim objSubCanal As New Clases.SubCanal
                        objSubCanal.Identificador = subcTodos.OidSubCanal
                        objSubCanal.Codigo = subcTodos.Codigo
                        objSubCanal.Descripcion = subcTodos.Descripcion

                        tot.SubCanales.Add(objSubCanal)
                    Next

                    For Each totSubCanal In Me.TotalizadoresSaldos.Where(Function(a) a.SubCanales.Count = 1)
                        tot.SubCanales.RemoveAll(Function(a) a.Identificador = totSubCanal.SubCanales.First.Identificador)
                    Next

                    tot.SubCanales = tot.SubCanales.OrderBy(Function(a) a.Descripcion).ToList()

                Next

            End If

            BusquedaNivelSaldoPopup.Peticion = Nothing
            BusquedaNivelSaldoPopup.Respuesta = Nothing

            AtualizaGrid()
            If Me.TotalizadoresSaldos IsNot Nothing AndAlso Me.TotalizadoresSaldos.Count > 0 Then
                Me.Visible = True
            End If
        End If

    End Sub

    Private Function ValidarCambiar(respuestaBusqueda As BusquedaNivelSaldoPopup.RespuestaBusqueda, peticionBusqueda As BusquedaNivelSaldoPopup.PeticionBusqueda) As Boolean

        Dim validacao As Boolean = True

        If respuestaBusqueda IsNot Nothing Then

            If respuestaBusqueda.Cliente Is Nothing Then
                respuestaBusqueda.Cliente = New ContractoServicio.Utilidad.GetComboClientes.Cliente
            End If

            If respuestaBusqueda.SubCliente Is Nothing Then
                respuestaBusqueda.SubCliente = New ContractoServicio.Utilidad.GetComboSubclientesByCliente.SubCliente
            End If

            If respuestaBusqueda.PuntoServicio Is Nothing Then
                respuestaBusqueda.PuntoServicio = New ContractoServicio.Utilidad.getComboPuntosServiciosByClienteSubcliente.PuntoServicio
            End If

            If respuestaBusqueda.SubCanales.Count > 1 Then
                For Each objTotSaldo In Me.TotalizadoresSaldos.Where(Function(a) a.SubCanales.Count > 1 _
                                                                         AndAlso a.IdentificadorNivelSaldo <> peticionBusqueda.IdentificadorNivelSaldo AndAlso a.IdentificadorNivelMovimiento <> peticionBusqueda.IdentificadorNivelMovimiento)

                    If objTotSaldo.Cliente Is Nothing Then
                        objTotSaldo.Cliente = New Clases.Cliente
                    End If

                    If objTotSaldo.SubCliente Is Nothing Then
                        objTotSaldo.SubCliente = New Clases.SubCliente
                    End If

                    If objTotSaldo.PuntoServicio Is Nothing Then
                        objTotSaldo.PuntoServicio = New Clases.PuntoServicio
                    End If

                    If objTotSaldo.Cliente IsNot Nothing AndAlso respuestaBusqueda.Cliente.OidCliente = objTotSaldo.Cliente.Identificador _
                        AndAlso respuestaBusqueda.SubCliente.OidSubCliente = objTotSaldo.SubCliente.Identificador _
                            AndAlso respuestaBusqueda.PuntoServicio.OidPuntoServicio = objTotSaldo.PuntoServicio.Identificador Then

                        Master.ControleErro.ShowError(Traduzir("044_valida_totalizador"))
                        validacao = False

                    End If

                    If String.IsNullOrEmpty(objTotSaldo.Cliente.Identificador) Then
                        objTotSaldo.Cliente = Nothing
                    End If

                    If String.IsNullOrEmpty(objTotSaldo.SubCliente.Identificador) Then
                        objTotSaldo.SubCliente = Nothing
                    End If

                    If String.IsNullOrEmpty(objTotSaldo.PuntoServicio.Identificador) Then
                        objTotSaldo.PuntoServicio = Nothing
                    End If

                Next
            Else
                For Each objTotSaldo In Me.TotalizadoresSaldos.Where(Function(a) a.SubCanales.Count = 1 AndAlso a.SubCanales.First.Identificador = respuestaBusqueda.SubCanales.First.OidSubCanal _
                                                                         AndAlso a.IdentificadorNivelSaldo <> peticionBusqueda.IdentificadorNivelSaldo AndAlso a.IdentificadorNivelMovimiento <> peticionBusqueda.IdentificadorNivelMovimiento)

                    If objTotSaldo.Cliente Is Nothing Then
                        objTotSaldo.Cliente = New Clases.Cliente
                    End If

                    If objTotSaldo.SubCliente Is Nothing Then
                        objTotSaldo.SubCliente = New Clases.SubCliente
                    End If

                    If objTotSaldo.PuntoServicio Is Nothing Then
                        objTotSaldo.PuntoServicio = New Clases.PuntoServicio
                    End If

                    If objTotSaldo.Cliente IsNot Nothing AndAlso respuestaBusqueda.Cliente.OidCliente = objTotSaldo.Cliente.Identificador _
                        AndAlso respuestaBusqueda.SubCliente.OidSubCliente = objTotSaldo.SubCliente.Identificador _
                            AndAlso respuestaBusqueda.PuntoServicio.OidPuntoServicio = objTotSaldo.PuntoServicio.Identificador Then

                        Master.ControleErro.ShowError(Traduzir("044_valida_totalizador"))
                        validacao = False

                    End If

                    If String.IsNullOrEmpty(objTotSaldo.Cliente.Identificador) Then
                        objTotSaldo.Cliente = Nothing
                    End If

                    If String.IsNullOrEmpty(objTotSaldo.SubCliente.Identificador) Then
                        objTotSaldo.SubCliente = Nothing
                    End If

                    If String.IsNullOrEmpty(objTotSaldo.PuntoServicio.Identificador) Then
                        objTotSaldo.PuntoServicio = Nothing
                    End If

                Next
            End If

        End If

        If String.IsNullOrEmpty(respuestaBusqueda.Cliente.OidCliente) Then
            respuestaBusqueda.Cliente = Nothing
        End If

        If String.IsNullOrEmpty(respuestaBusqueda.SubCliente.OidSubCliente) Then
            respuestaBusqueda.SubCliente = Nothing
        End If

        If String.IsNullOrEmpty(respuestaBusqueda.PuntoServicio.OidPuntoServicio) Then
            respuestaBusqueda.PuntoServicio = Nothing
        End If

        Return validacao
    End Function

    Public Sub Desabilitar()
        grvTotSaldo.Enabled = False
    End Sub

    Public Sub Habilitar()
        grvTotSaldo.Enabled = True
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        DevExpress.Web.ASPxGridView.ASPxGridView.RegisterBaseScript(Page)

        Me.Master = DirectCast(Me.Page.Master, Master.Master)
        If Not Me.IsPostBack Then
            TraduzirControles()
            CarregaDados()
        Else
            ConsomeNivelSaldo()
        End If
        AtualizaGrid()
    End Sub

    Protected Sub grvTotSaldo_RowDataBound(sender As Object, e As GridViewRowEventArgs)

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim objPage As Base = DirectCast(Me.Page, Base)
            Dim objTotSaldo As Clases.TotalizadorSaldo = DirectCast(e.Row.DataItem, Clases.TotalizadorSaldo)
            Dim imbModificar As ImageButton = e.Row.FindControl("imbModificar")
            Dim imbBorrar As ImageButton = e.Row.FindControl("imbBorrar")
            Dim lblDesSubCanal As Label = e.Row.FindControl("lblDesSubCanal")
            Dim lblIdSubCanal As Label = e.Row.FindControl("lblIdSubCanal")
            Dim chkDefecto As CheckBox = e.Row.FindControl("chkDefecto")

            chkDefecto.Checked = objTotSaldo.bolDefecto
            chkDefecto.Enabled = Not chkDefecto.Checked
            
            If objTotSaldo.SubCanales IsNot Nothing AndAlso objTotSaldo.SubCanales.Count > 0 Then
                lblIdSubCanal.Text = String.Join(",", objTotSaldo.SubCanales.Select(Function(a) a.Identificador))

                If objTotSaldo.SubCanales.Count = 1 Then
                    lblDesSubCanal.Text = objTotSaldo.SubCanales.First.Descripcion

                    If  objPage.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                        imbModificar.Enabled = False
                        imbModificar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/edit.png")
                        imbModificar.Style.Add("cursor", "default")
                        imbModificar.Attributes.Add("class", "imgbutton")
                        imbBorrar.Enabled = False
                        imbBorrar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/borrar.png")
                        imbBorrar.Style.Add("cursor", "default")
                        imbBorrar.Attributes.Add("class", "imgbutton")

                    End If
                Else
                    lblDesSubCanal.Text = "* "
                    lblDesSubCanal.Text &= "(" & String.Join(", ", objTotSaldo.SubCanales.Select(Function(a) a.Descripcion)) & ")"

                    If objTotSaldo.Cliente Is Nothing Then
                        objTotSaldo.Cliente = New Clases.Cliente
                    End If
                    If objTotSaldo.SubCliente Is Nothing Then
                        objTotSaldo.SubCliente = New Clases.SubCliente
                    End If
                    If objTotSaldo.PuntoServicio Is Nothing Then
                        objTotSaldo.PuntoServicio = New Clases.PuntoServicio
                    End If

                    If (IdentificadorCliente = objTotSaldo.Cliente.Identificador _
                        AndAlso IdentificadorSubCliente = objTotSaldo.SubCliente.Identificador _
                        AndAlso IdentificadorPuntoServicio = objTotSaldo.PuntoServicio.Identificador) OrElse objPage.Acao = Aplicacao.Util.Utilidad.eAcao.Consulta Then

                        imbModificar.Enabled = False
                        imbModificar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/edit.png")
                        imbModificar.Style.Add("cursor", "default")
                        imbModificar.Attributes.Add("class", "imgbutton")
                        imbBorrar.Enabled = False
                        imbBorrar.ImageUrl = Page.ResolveUrl("~/App_Themes/Padrao/css/img/grid/borrar.png")
                        imbBorrar.Style.Add("cursor", "default")
                        imbBorrar.Attributes.Add("class", "imgbutton")

                    End If

                    If String.IsNullOrEmpty(objTotSaldo.Cliente.Identificador) Then
                        objTotSaldo.Cliente = Nothing
                    End If
                    If String.IsNullOrEmpty(objTotSaldo.SubCliente.Identificador) Then
                        objTotSaldo.SubCliente = Nothing
                    End If
                    If String.IsNullOrEmpty(objTotSaldo.PuntoServicio.Identificador) Then
                        objTotSaldo.PuntoServicio = Nothing
                    End If

                End If
            End If

            Dim lblTotalizador As Label = e.Row.FindControl("lblTotalizador")
            If objTotSaldo IsNot Nothing AndAlso objTotSaldo.Cliente IsNot Nothing Then
                lblTotalizador.Text = objTotSaldo.Cliente.Descripcion
                If objTotSaldo.SubCliente IsNot Nothing Then
                    lblTotalizador.Text += " - " + objTotSaldo.SubCliente.Descripcion
                End If
                If objTotSaldo.PuntoServicio IsNot Nothing Then
                    lblTotalizador.Text += " - " + objTotSaldo.PuntoServicio.Descripcion
                End If
            End If


        End If

    End Sub

    Protected Sub chkDefecto_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim chkDefectoDefecto As CheckBox = DirectCast(sender, CheckBox)
        Dim grdRowDefecto As GridViewRow = DirectCast(chkDefectoDefecto.DataItemContainer, GridViewRow)
        Dim lblIdConfigNivelMov As Label = grdRowDefecto.FindControl("lblIdConfigNivelMov")
        Dim lblIdConfigNivelSaldo As Label = grdRowDefecto.FindControl("lblIdConfigNivelSaldo")

        If chkDefectoDefecto.Checked Then
            Dim lblIdSubCanalDefecto As Label = grdRowDefecto.FindControl("lblIdSubCanal")

            Dim lstTotSaldoSubCanal As List(Of Clases.TotalizadorSaldo) = Me.TotalizadoresSaldos.Where(Function(a) String.Join(",", a.SubCanales.Select(Function(b) b.Identificador)) = lblIdSubCanalDefecto.Text).ToList()
            lstTotSaldoSubCanal.ForEach(Sub(a)

                                            a.bolDefecto = False
                                            If a.IdentificadorNivelMovimiento = lblIdConfigNivelMov.Text AndAlso a.IdentificadorNivelSaldo = lblIdConfigNivelSaldo.Text Then
                                                a.bolDefecto = True
                                            End If

                                        End Sub)

        Else
            Dim objTotSaldo As Clases.TotalizadorSaldo = Me.TotalizadoresSaldos.Find(Function(a) a.IdentificadorNivelMovimiento = lblIdConfigNivelMov.Text AndAlso a.IdentificadorNivelSaldo = lblIdConfigNivelSaldo.Text)
            If objTotSaldo IsNot Nothing Then
                objTotSaldo.bolDefecto = False
            End If
        End If

        AtualizaGrid()
    End Sub

    Protected Sub grvTotSaldo_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Cambiar" Then
            Cambiar(e.CommandArgument)
        ElseIf e.CommandName = "Borrar" Then
            Borrar(e.CommandArgument)
        End If
    End Sub

#End Region

End Class