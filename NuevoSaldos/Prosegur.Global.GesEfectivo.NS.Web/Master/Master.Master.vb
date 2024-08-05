Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio
Imports Prosegur.Genesis.Comon.Extenciones

'Imports Prosegur.Genesis.Comon.Pantallas


Public Class Master
    Inherits System.Web.UI.MasterPage

    Private _EsModal As Boolean?
    Public ReadOnly Property EsModal() As Boolean
        Get
            If (Request.QueryString.AllKeys.Contains("EsPopup")) Then
                _EsModal = Request.QueryString("EsPopup")
            End If

            If Not _EsModal.HasValue Then
                _EsModal = Not String.IsNullOrEmpty(Request.QueryString("ClaveDocumento"))
            End If
            Return _EsModal.Value
        End Get
    End Property

    Private _Historico As List(Of KeyValuePair(Of String, String))
    Public ReadOnly Property Historico() As List(Of KeyValuePair(Of String, String))
        Get
            If _Historico Is Nothing Then
                _Historico = Session("Master_Historico")
                If _Historico Is Nothing Then
                    _Historico = New List(Of KeyValuePair(Of String, String))()
                    Session("Master_Historico") = _Historico
                End If
            End If
            Return _Historico
        End Get
    End Property

    Public Property HabilitarMenu As Boolean
        Get
            Return Me.divMenu.Visible
        End Get
        Set(value As Boolean)
            Me.divMenu.Visible = value
            linkSector.Enabled = value
            linkSalir.Visible = value
            linkImagemMenu.Enabled = value
            HabilitarHistorico = value
        End Set
    End Property

    Public Property HaySoloUnoSector As Boolean
        Get
            Return Session("linkSectorHabilitado")
        End Get
        Set(value As Boolean)
            Session("linkSectorHabilitado") = value
        End Set
    End Property

    Private _HabilitarHistorico As Boolean = True
    Public Property HabilitarHistorico() As Boolean
        Get
            Return _HabilitarHistorico
        End Get
        Set(value As Boolean)
            _HabilitarHistorico = value
        End Set
    End Property

    Public Property MostrarCabecalho() As Boolean
        Get
            Return pnlCabecalho.Visible
        End Get
        Set(value As Boolean)
            pnlCabecalho.Visible = value
        End Set
    End Property

    Public Property MostrarRodape() As Boolean
        Get
            Return pnlRodape.Visible
        End Get
        Set(value As Boolean)
            pnlRodape.Visible = value
        End Set
    End Property

    Property ControleErro As Object

    Private Sub AdicionaHistorico(itemHistorico As KeyValuePair(Of String, String))
        Dim item As Nullable(Of KeyValuePair(Of String, String))
        item = Historico.Find(Function(i) String.Compare(i.Key, itemHistorico.Key, True) = 0)
        If item IsNot Nothing Then
            Historico.Remove(item)
        End If
        If Historico.Count = 5 Then
            Historico.RemoveAt(0)
        End If
        If Not itemHistorico.Key.Contains("EsPopup=True") Then
            Historico.Add(itemHistorico)
        End If

    End Sub

    Public Property Titulo() As String
        Get
            Return lblHeaderTitulo.Text
        End Get
        Set(value As String)
            lblHeaderTitulo.Text = value
        End Set
    End Property

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.Page.EnableEventValidation = False
    End Sub

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If MostrarCabecalho Then
            If HabilitarHistorico Then

                Dim HistoricoExistente = Historico.Where(Function(x) x.Key.Split("?")(0) = Request.RawUrl.Split("?")(0)).SingleOrDefault()

                If Not String.IsNullOrEmpty(HistoricoExistente.Key) Then
                    Historico.Remove(HistoricoExistente)
                End If

                AdicionaHistorico(New KeyValuePair(Of String, String)(Request.RawUrl, Titulo))
                rptHistorico.DataBind()

                If Request.Url.AbsoluteUri.Contains("SeleccionSector.aspx") Then
                    rptHistorico.Visible = False
                End If

            End If
            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            lblUsuarioLogado.Text = If(usuario Is Nothing, String.Empty, String.Format("{0} {1} ", usuario.Nombre, usuario.Apellido))

            'If (Base.InformacionUsuario.SectorSeleccionado IsNot Nothing) Then

            Dim pais As Prosegur.Genesis.Comon.Clases.Pais = Nothing
                If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
                    pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(Base.InformacionUsuario.DelegacionSeleccionada.Codigo)
                End If
                If pais IsNot Nothing Then
                    Me.lblPais.Text = String.Format(" - {0}: {1} ", Traduzir("genPais"), pais.Descripcion)
                Else
                    Me.lblPais.Text = String.Empty 'String.Format(" - {0}: {1} ", Traduzir("genPais"), String.Empty)
                End If


            If Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing AndAlso Not String.IsNullOrEmpty(Base.InformacionUsuario.DelegacionSeleccionada.Descripcion) Then
                Me.lblDelegacion.Text = String.Format(" - {0}: {1} ", Traduzir("genDelegacion"), If(Base.InformacionUsuario.DelegacionSeleccionada, New Prosegur.Genesis.Comon.Clases.Delegacion()).Descripcion)
                Me.lblDelegacion.ToolTip = Me.lblDelegacion.Text
            Else
                Me.lblDelegacion.Text = String.Empty
            End If

            'If Not String.IsNullOrEmpty(Base.InformacionUsuario.SectorSeleccionado.Descripcion) Then

            '    linkSector.Text = String.Format("{0}: {1}", Traduzir("genSector"), Base.InformacionUsuario.SectorSeleccionado.Descripcion)
            '    linkSector.ToolTip = linkSector.Text
            '    ' linkSector.NavigateUrl = Page.ResolveUrl(Constantes.NOME_PAGINA_SELECCION_SECTOR)

            '    If (Me.HaySoloUnoSector) Then
            '        Me.linkSector.Enabled = False
            '    End If

            'Else
            '    Me.linkSector.Visible = False
            'End If

            If Not Request.Url.AbsoluteUri.Contains("SeleccionSector.aspx") Then
                    If EsModal OrElse Not Base.PossuiPermissao(Aplicacao.Util.Utilidad.eTelas.VISUALIZAR_NOTIFICACIONES) Then
                        Me.ucCentralNotificaciones.Desabilitar()
                    Else
                        CarregarNotificacoes()
                    End If
                End If
                'End If
            End If
        If MostrarRodape Then
            lblVersao.Text = Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly())
        End If

    End Sub

    Public Property MenuRodapeVisivel() As Boolean
        Get
            Return pnlMenuRodape.Visible
        End Get
        Set(value As Boolean)
            pnlMenuRodape.Visible = value
        End Set
    End Property

    Private Sub CarregarNotificacoes()
        Dim codigosDelegacion As New List(Of String)
        Dim codigosPlanta As New List(Of String)
        Dim codigosSector As New List(Of String)
        Dim identificadoresTipoSector As New List(Of String)

        Me.ucCentralNotificaciones.UpdateProgressID = upp.ClientID
        Me.ucCentralNotificaciones.Visible = True
        If Me.ucCentralNotificaciones.Notificaciones Is Nothing AndAlso Genesis.Web.Login.Parametros.Permisos.Usuario IsNot Nothing Then
            'Add delegações que o usuario tem permissão
            If Base.InformacionUsuario.Delegaciones IsNot Nothing Then
                For Each delegacion In Base.InformacionUsuario.Delegaciones
                    If Not String.IsNullOrEmpty(delegacion.Codigo) Then
                        codigosDelegacion.Add(delegacion.Codigo)
                    End If
                    'Add plantas que o usuario tem permissão
                    If delegacion.Plantas IsNot Nothing Then
                        For Each planta In delegacion.Plantas
                            If Not String.IsNullOrEmpty(planta.Codigo) Then
                                codigosPlanta.Add(planta.Codigo)
                            End If
                            'Add setores que o usuario tem permissão
                            If planta.Sectores IsNot Nothing Then
                                For Each sector In planta.Sectores
                                    If Not String.IsNullOrEmpty(sector.Codigo) Then
                                        codigosSector.Add(sector.Codigo)
                                    End If
                                Next
                            End If
                            'Add tipos de setores que o usuario tem permissão
                            If planta.TiposSector IsNot Nothing Then
                                For Each tipoSector In planta.TiposSector
                                    If Not String.IsNullOrEmpty(tipoSector.Identificador) Then
                                        identificadoresTipoSector.Add(tipoSector.Identificador)
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If

            Me.ucCentralNotificaciones.CarregarNotificacoes(Genesis.Web.Login.Parametros.Permisos.Usuario.Login,
                                                            codigosDelegacion, _
                                                            codigosPlanta, _
                                                            codigosSector, _
                                                            identificadoresTipoSector)

        End If
        Me.ucCentralNotificaciones.InicializarDados()
    End Sub


    Public Sub ExibirModal(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True, Optional disparaEvento As Boolean = False, Optional botao As String = "")
        If urlCaminho.Contains("?") Then
            urlCaminho &= "&divModal=" & divModal.ClientID & "&ifrModal=" & ifrModal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
        Else
            urlCaminho &= "?divModal=" & divModal.ClientID & "&ifrModal=" & ifrModal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
        End If
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "exibirModal", "ExibirUrlModal('#" & divModal.ClientID & "', '#" & ifrModal.ClientID & "', '" & urlCaminho & "','" & tituloModal & "', " & altura & ", " & largura & "," & efetuarReload.ToString().ToLower() & "," & disparaEvento.ToString().ToLower() & ",'" & botao.ToString() & "' );", True)
    End Sub
    Public Sub ExibirModal2(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True, Optional disparaEvento As Boolean = False, Optional botao As String = "")
        Dim ejecutar_script As String = "ExibirUrlModal('#" & divModal.ClientID & "', '#" & ifrModal.ClientID & "', '" & urlCaminho & "','" & tituloModal & "' );"
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "exibirModal", ejecutar_script, True)
    End Sub

    Protected Sub ucCentralNotificacions_OnError(sender As Object, erro As ErroEventArgs) Handles ucCentralNotificaciones.Erro
        MostraMensagemErro(erro.Erro.ToString())
    End Sub

    ''' <summary>
    ''' Mostra Mensagem de Erro na tela
    ''' </summary>
    ''' <param name="erro"></param>
    ''' <remarks></remarks>
    ''' <history>[adans.klevanskis] 05/06/2013 - criado</history>
    Private Sub MostraMensagemErro(erro As String, script As String)
        erro = erro.Replace(vbCrLf, "<br />")
        erro = erro.Replace(vbCr, "<br />")
        erro = erro.Replace(vbLf, "<br />")
        erro = erro.Replace("'", "&#39;")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "erro", _
                                                   Aplicacao.Util.Utilidad.CriarChamadaMensagemErro(erro, script) _
                                                       , True)
    End Sub

    Private Sub MostraMensagemErro(erro As String)
        MostraMensagemErro(erro, String.Empty)
        If Base.InformacionUsuario IsNot Nothing AndAlso Base.InformacionUsuario.DelegacionSeleccionada IsNot Nothing Then
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Base.InformacionUsuario.DelegacionSeleccionada.Codigo,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        Else
            Prosegur.Genesis.LogicaNegocio.Genesis.Log.GuardarLogExecucao(erro,
                                                                          Base.InformacionUsuario.Nombre,
                                                                          Nothing,
                                                                          Genesis.Comon.Enumeradores.Aplicacion.GenesisNuevoSaldos)
        End If
    End Sub

End Class