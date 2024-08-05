Imports Prosegur.Genesis.LogicaNegocio.GenesisSaldos.Certificacion
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis

Public Class ObtenerNivelSaldos
    Inherits Prosegur.Global.GesEfectivo.NuevoSaldos.Web.Base


#Region "[PROPRIEDADES]"

    Dim WithEvents ucBuscaCliente As PopupBusquedaCliente
    Dim ClientesNivelSaldos As GenesisSaldos.Certificacion.ObtenerNivelSaldos.NivelSaldos

    Private Property ClienteSelecionado As Entidades.Cliente
        Get
            Return ViewState("ClienteSelecionado")
        End Get
        Set(value As Entidades.Cliente)
            ViewState("ClienteSelecionado") = value
        End Set
    End Property

#End Region

#Region "[METODOS BASE]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.NIVEL_SALDOS_CONSULTAR
        MyBase.ValidarAcesso = True
        MyBase.ValidarPemissaoAD = True
    End Sub

    ''' <summary>
    ''' Método chamado no load da base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Inicializar()
        Master.HabilitarHistorico = True
        Master.MostrarRodape = False

    End Sub

    Protected Overrides Sub TraduzirControles()
        lblClienteTotalizador.Text = Traduzir("003_lblClienteTotalizador")
        lblSubCanal.Text = Traduzir("003_lblSubCanal")
        lblDadosEntrada.Text = Traduzir("003_lblDadosEntrada")
        btnConsultar.Text = Traduzir("btnConsultar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        lblResultadoClienteTotalizador.Text = Traduzir("003_lbl_Titulo_ClientesTotalizador")
        lblResultadoClienteNaoTotalizador.Text = Traduzir("003_lbl_Titulo_ClientesNaoTotalizador")
        gdvClientesTotalizador.EmptyDataText = Traduzir("gridSemRegistro")
        gdvClientesNaoTotalizador.EmptyDataText = Traduzir("gridSemRegistro")
        Master.Titulo = Traduzir("003_tituloPagina")
        btnLimpar.Text = Traduzir("btnLimpar")

    End Sub

    ''' <summary>
    ''' Método chamado ao renderizar a base.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub PreRenderizar()

    End Sub

    ''' <summary>
    ''' Método que permite o desenvoledor adicionar controles para validação.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub


    ''' <summary>
    ''' Método que permite o desenvolvedor adicionar scripts para controles da tela.
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub AdicionarScripts()

    End Sub

    ' ''' <summary>
    ' ''' Método que permite o desenvolvedor configurar os tab index dos controles da tela.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Protected Overrides Sub ConfigurarTabIndex()
    '    txtCodClienteTotalizador.Focus()
    'End Sub

#End Region

#Region "[EVENTOS]"

    Private Sub ucBuscaCliente_Fechado(sender As Object, e As PopupEventArgs) Handles ucBuscaCliente.Fechado
        Try
            If e.Resultado IsNot Nothing Then
                Dim clie = DirectCast(e.Resultado, List(Of Entidades.Cliente))
                txtCodClienteTotalizador.Text = clie.FirstOrDefault.CodCliente
                txtDesClienteTotalizador.Text = clie.FirstOrDefault.DesCliente
                txtDesClienteTotalizador.ToolTip = clie.FirstOrDefault.DesCliente
                ClienteSelecionado = clie.FirstOrDefault
            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            PopulaGridVazio()
            TraduzirControles()

            If Not Page.IsPostBack Then
                PreencherListBoxSubCanal()
            End If

            If Context.Session("ClienteSelecionado") IsNot Nothing Then
                ClienteSelecionado = TryCast(Context.Session("ClienteSelecionado"), Entidades.Cliente)
                If ClienteSelecionado IsNot Nothing Then
                    txtCodClienteTotalizador.Text = ClienteSelecionado.CodCliente
                    txtDesClienteTotalizador.Text = ClienteSelecionado.DesCliente
                End If
                Context.Session("ClienteSelecionado") = Nothing
            End If

            ucBuscaCliente = LoadControl("~/Controles/PopupBusquedaCliente.ascx")
            ucBuscaCliente.RetornarResultadoAoPassarValores = True            
            ucBusquedaCliente.PopupBase = ucBuscaCliente

        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Sub txtCodClienteTotalizador_TextChanged(sender As Object, e As EventArgs) Handles txtCodClienteTotalizador.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtCodClienteTotalizador.Text) Then
                Dim cliente = ucBuscaCliente.RecuperarClientes(txtCodClienteTotalizador.Text, String.Empty)
                If cliente IsNot Nothing AndAlso cliente.Count = 1 Then
                    txtCodClienteTotalizador.Text = cliente.FirstOrDefault.CodCliente
                    txtDesClienteTotalizador.Text = cliente.FirstOrDefault.DesCliente
                    ClienteSelecionado = cliente.FirstOrDefault
                ElseIf cliente IsNot Nothing AndAlso cliente.Count > 1 Then
                    txtDesClienteTotalizador.Text = String.Empty
                    ucBuscaCliente.ConfigurarValorPadrao(txtCodClienteTotalizador.Text, txtDesClienteTotalizador.Text)                    
                    ucBusquedaCliente.AbrirPopup()
                Else
                    txtDesClienteTotalizador.Text = String.Empty
                    txtCodClienteTotalizador.Text = String.Empty
                End If
            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub txtDesClienteTotalizador_TextChanged(sender As Object, e As EventArgs) Handles txtDesClienteTotalizador.TextChanged
        Try
            If Not String.IsNullOrEmpty(txtDesClienteTotalizador.Text) Then
                Dim cliente = ucBuscaCliente.RecuperarClientes(String.Empty, txtDesClienteTotalizador.Text)
                If cliente IsNot Nothing AndAlso cliente.Count = 1 Then
                    txtCodClienteTotalizador.Text = cliente.FirstOrDefault.CodCliente
                    txtDesClienteTotalizador.Text = cliente.FirstOrDefault.DesCliente
                    ClienteSelecionado = cliente.FirstOrDefault
                ElseIf cliente IsNot Nothing AndAlso cliente.Count > 1 Then
                    txtCodClienteTotalizador.Text = String.Empty
                    ucBuscaCliente.ConfigurarValorPadrao(txtCodClienteTotalizador.Text, txtDesClienteTotalizador.Text)                    
                    ucBusquedaCliente.AbrirPopup()
                Else
                    txtDesClienteTotalizador.Text = String.Empty
                    txtCodClienteTotalizador.Text = String.Empty
                End If
            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#Region "[EVENTOS DO GRID]"

    Protected Sub gdvClientesTotalizador_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientesTotalizador.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("003_grid_CodCliente")
                e.Row.Cells(1).Text = Traduzir("003_grid_DesCliente")
                e.Row.Cells(2).Text = Traduzir("003_grid_CodSubCliente")
                e.Row.Cells(3).Text = Traduzir("003_grid_DesSubCliente")
                e.Row.Cells(4).Text = Traduzir("003_grid_CodPtoServico")
                e.Row.Cells(5).Text = Traduzir("003_grid_DesPtoServico")
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If ClientesNivelSaldos.NoTotalizaSaldos IsNot Nothing Then

                    Dim ClienteEsTotalizador = From p In ClientesNivelSaldos.NoTotalizaSaldos
                                               From c In p.ClienteTotalizaEnClienteTotalizadorSaldo
                                               Where c.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                               AndAlso String.IsNullOrEmpty(c.OidSubcliente) _
                                               AndAlso String.IsNullOrEmpty(c.OidPtoServicio)

                    If ClienteEsTotalizador.Count > 0 Then
                        e.Row.Cells(0).BackColor = Drawing.Color.Red
                        e.Row.Cells(1).BackColor = Drawing.Color.Red
                    Else
                        If ClientesNivelSaldos.TotalizaSaldos IsNot Nothing Then
                            Dim ClienteTotalizador = From c In ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                                                     Where c.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                               AndAlso String.IsNullOrEmpty(c.OidSubcliente) _
                                               AndAlso String.IsNullOrEmpty(c.OidPtoServicio)

                            If ClienteTotalizador.Count = 0 AndAlso Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(0).Text)) _
                              AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(0).Text) Then
                                e.Row.Cells(0).BackColor = Drawing.Color.Red
                                e.Row.Cells(1).BackColor = Drawing.Color.Red
                            End If

                        End If
                    End If


                    Dim SubCliEsTotalizador = From p In ClientesNivelSaldos.NoTotalizaSaldos
                                               From c In p.ClienteTotalizaEnClienteTotalizadorSaldo
                                               Where c.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                               AndAlso c.CodSubcliente = Server.HtmlDecode(e.Row.Cells(2).Text) _
                                               AndAlso String.IsNullOrEmpty(c.OidPtoServicio)

                    If SubCliEsTotalizador.Count > 0 Then
                        e.Row.Cells(2).BackColor = Drawing.Color.Red
                        e.Row.Cells(3).BackColor = Drawing.Color.Red
                    Else
                        If ClientesNivelSaldos.TotalizaSaldos IsNot Nothing Then

                            Dim SubCliTotalizador = From t In ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                                                    Where t.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                                    AndAlso t.CodSubcliente = Server.HtmlDecode(e.Row.Cells(2).Text) _
                                                    AndAlso String.IsNullOrEmpty(t.OidPtoServicio)

                            If SubCliTotalizador.Count = 0 AndAlso Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(2).Text)) _
                                AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(2).Text) Then
                                e.Row.Cells(2).BackColor = Drawing.Color.Red
                                e.Row.Cells(3).BackColor = Drawing.Color.Red
                            End If
                        End If
                    End If


                End If

            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

    Protected Sub gdvClientesNaoTotalizador_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvClientesNaoTotalizador.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Text = Traduzir("003_grid_CodCliente")
                e.Row.Cells(1).Text = Traduzir("003_grid_DesCliente")
                e.Row.Cells(2).Text = Traduzir("003_grid_CodSubCliente")
                e.Row.Cells(3).Text = Traduzir("003_grid_DesSubCliente")
                e.Row.Cells(4).Text = Traduzir("003_grid_CodPtoServico")
                e.Row.Cells(5).Text = Traduzir("003_grid_DesPtoServico")
            ElseIf e.Row.RowType = DataControlRowType.DataRow Then

                If ClientesNivelSaldos.TotalizaSaldos IsNot Nothing Then

                    Dim ClienteEsTotalizador = From p In ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                                               Where p.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                               AndAlso String.IsNullOrEmpty(p.OidSubcliente) _
                                               AndAlso String.IsNullOrEmpty(p.OidPtoServicio)

                    If ClienteEsTotalizador.Count = 0 Then
                        e.Row.Cells(0).BackColor = Drawing.Color.Red
                        e.Row.Cells(1).BackColor = Drawing.Color.Red
                    End If


                    If Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(2).Text)) AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(2).Text) Then
                        Dim SubCliEsTotalizador = From p In ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                                              Where p.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                              AndAlso p.CodSubcliente = Server.HtmlDecode(e.Row.Cells(2).Text) _
                                              AndAlso String.IsNullOrEmpty(p.OidPtoServicio)

                        If SubCliEsTotalizador.Count = 0 Then
                            e.Row.Cells(2).BackColor = Drawing.Color.Red
                            e.Row.Cells(3).BackColor = Drawing.Color.Red
                        End If
                    End If

                    If Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(4).Text)) AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(4).Text) Then

                        Dim PtoServicoEstTotalizador = From p In ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                                                   Where p.CodCliente = Server.HtmlDecode(e.Row.Cells(0).Text) _
                                                   AndAlso p.CodSubcliente = Server.HtmlDecode(e.Row.Cells(2).Text) _
                                                   AndAlso p.CodPtoServicio = Server.HtmlDecode(e.Row.Cells(4).Text)

                        If PtoServicoEstTotalizador.Count = 0 Then
                            e.Row.Cells(4).BackColor = Drawing.Color.Red
                            e.Row.Cells(5).BackColor = Drawing.Color.Red
                        End If
                    End If

                Else

                    e.Row.Cells(0).BackColor = Drawing.Color.Red
                    e.Row.Cells(1).BackColor = Drawing.Color.Red

                    If Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(2).Text)) AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(2).Text) Then

                        e.Row.Cells(2).BackColor = Drawing.Color.Red
                        e.Row.Cells(3).BackColor = Drawing.Color.Red

                    End If

                    If Not String.IsNullOrEmpty(Server.HtmlDecode(e.Row.Cells(4).Text)) AndAlso Server.HtmlDecode("&nbsp;") <> Server.HtmlDecode(e.Row.Cells(4).Text) Then

                        e.Row.Cells(4).BackColor = Drawing.Color.Red
                        e.Row.Cells(5).BackColor = Drawing.Color.Red

                    End If
                End If
            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try
    End Sub

#End Region

#Region "[EVENTOS DE BOTAO]"

    Protected Sub btnBuscar_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click
        ucBuscaCliente.ConfigurarValorPadrao(txtCodClienteTotalizador.Text, txtDesClienteTotalizador.Text)        
        ucBusquedaCliente.AbrirPopup()
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try

            If ValidaDatos() Then

                Dim objRespuesta = ObterNivelSaldos()

                If MyBase.VerificaRespuestaSemErro(objRespuesta.CodigoError) Then
                    ClientesNivelSaldos = objRespuesta.NivelSaldos
                    If ValidaResposta() Then

                        If ClientesNivelSaldos.TotalizaSaldos IsNot Nothing Then
                            gdvClientesTotalizador.DataSource = ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo
                            gdvClientesTotalizador.DataBind()
                        End If

                        If ClientesNivelSaldos.NoTotalizaSaldos IsNot Nothing Then
                            Dim NaoTotalizador = (From p In ClientesNivelSaldos.NoTotalizaSaldos
                                              From c In p.ClienteTotalizaEnClienteTotalizadorSaldo
                                             Select c).ToList()

                            gdvClientesNaoTotalizador.DataSource = NaoTotalizador
                            gdvClientesNaoTotalizador.DataBind()
                        End If
                    Else
                        MyBase.MostraMensagemErro(Traduzir("003_msg_error_ClienteTotalizadorSemCliente"))
                        PopulaGridVazio()
                    End If
                Else
                    MyBase.MostraMensagemErro(objRespuesta.MensajeError)
                End If

            End If
        Catch negocioEx As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(negocioEx.Descricao)
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.Message)
        End Try

    End Sub

    Protected Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click

        gdvClientesTotalizador.DataSource = Nothing
        gdvClientesTotalizador.DataBind()

        gdvClientesNaoTotalizador.DataSource = Nothing
        gdvClientesNaoTotalizador.DataBind()

        txtCodClienteTotalizador.Text = String.Empty
        txtDesClienteTotalizador.Text = String.Empty

        ddlSubCanal.ClearSelection()

    End Sub

#End Region

#End Region

#Region "[METODOS]"

    Public Sub PopulaGridVazio()
        gdvClientesTotalizador.DataSource = Nothing
        gdvClientesTotalizador.DataBind()

        gdvClientesNaoTotalizador.DataSource = Nothing
        gdvClientesNaoTotalizador.DataBind()
    End Sub

    Public Function ObterNivelSaldos() As GenesisSaldos.Certificacion.ObtenerNivelSaldos.Respuesta

        Dim objRespuesta As GenesisSaldos.Certificacion.ObtenerNivelSaldos.Respuesta
        Dim objPeticion As New GenesisSaldos.Certificacion.ObtenerNivelSaldos.Peticion
        Try

            objPeticion.CodClienteTotalizador = ClienteSelecionado.CodCliente
            objPeticion.CodSubCanal = New List(Of String)
            objPeticion.CodSubCanal.Add(ddlSubCanal.SelectedValue)
            objPeticion.PantallaObtenerNivelSaldos = True
            objPeticion.Delegacion = InformacionUsuario.DelegacionSeleccionada

            Dim objAccion As New AccionObtenerNivelSaldos()
            objRespuesta = objAccion.Ejecutar(objPeticion)
            Return objRespuesta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Preenche o listbox SubCanal
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherListBoxSubCanal()

        Dim objProxyUtilida As New Comunicacion.ProxyUtilidad
        Dim objRespuesta As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Utilidad.GetComboSubcanalesByCanal.Peticion

        objPeticion.Codigo = New List(Of String)
        If ddlSubCanal IsNot Nothing AndAlso ddlSubCanal.Items.Count > 0 Then
            For Each CanalSelecionado As ListItem In ddlSubCanal.Items
                If CanalSelecionado.Selected Then
                    objPeticion.Codigo.Add(CanalSelecionado.Value)
                End If
            Next
        End If

        objRespuesta = objProxyUtilida.GetComboSubcanalesByCanal(objPeticion)

        ddlSubCanal.AppendDataBoundItems = True


        If objRespuesta.Canales IsNot Nothing _
       AndAlso objRespuesta.Canales.Count > 0 Then

            'Adiciona os item selecionados no temporario
            Dim listaSelecionadosTemp As ListItemCollection = Nothing
            If ddlSubCanal.Items IsNot Nothing AndAlso ddlSubCanal.Items.Count > 0 Then
                listaSelecionadosTemp = New ListItemCollection
                For Each item As ListItem In ddlSubCanal.Items
                    If item.Selected Then
                        listaSelecionadosTemp.Add(item)
                    End If
                Next
            End If

            'Limpa os subcanais
            ddlSubCanal.Items.Clear()
            ddlSubCanal.Items.Add(New ListItem(Traduzir("gen_selecione"), Traduzir("gen_selecione")))
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

                        'Adiciona o item na coleção
                        ddlSubCanal.Items.Add(item)
                    Next

                End If

            Next
        Else
            ddlSubCanal.Items.Clear()
            ddlSubCanal.DataSource = Nothing
            ddlSubCanal.DataBind()
        End If

    End Sub

    Private Function ValidaDatos() As Boolean
        Dim erro As New StringBuilder

        If ddlSubCanal.SelectedValue = Traduzir("gen_selecione") Then
            erro.AppendFormat(Traduzir("msg_campo_obrigatorio"), lblSubCanal.Text).AppendLine()
        End If

        If ClienteSelecionado Is Nothing OrElse ClienteSelecionado.CodCliente <> txtCodClienteTotalizador.Text _
            OrElse ClienteSelecionado.DesCliente <> txtDesClienteTotalizador.Text Then
            erro.AppendFormat(Traduzir("msg_campo_obrigatorio"), lblClienteTotalizador.Text).AppendLine()
        End If

        If erro.Length > 0 Then
            MyBase.MostraMensagemErro(erro.ToString)
            Return False
        End If

        Return True
    End Function

    Private Function ValidaResposta() As Boolean
        Return (ClientesNivelSaldos IsNot Nothing AndAlso ((ClientesNivelSaldos.TotalizaSaldos IsNot Nothing _
                      AndAlso ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo IsNot Nothing _
                      AndAlso ClientesNivelSaldos.TotalizaSaldos.ClienteTotalizaEnClienteTotalizadorSaldo.Count > 0) OrElse _
                  (ClientesNivelSaldos.NoTotalizaSaldos IsNot Nothing _
                      AndAlso ClientesNivelSaldos.NoTotalizaSaldos IsNot Nothing _
                      AndAlso ClientesNivelSaldos.NoTotalizaSaldos.Count > 0) _
                  ))
    End Function

#End Region

End Class