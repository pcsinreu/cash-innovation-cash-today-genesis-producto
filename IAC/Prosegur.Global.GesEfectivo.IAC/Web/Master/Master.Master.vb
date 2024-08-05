Imports System.Reflection
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Web

Namespace Master

    Public Class Master
        Inherits IMaster


        Private _ValidacaoAcesso As ValidacaoAcesso = Nothing

        Public Property InformacionUsuario() As Login.InformacionUsuario
            Get

                ' se sessão não foi setado
                If Session("BaseInformacoesUsuario") IsNot Nothing Then

                    ' tentar recuperar objeto da sessao
                    Dim Info = TryCast(Session("BaseInformacoesUsuario"), Login.InformacionUsuario)

                    ' retornar objeto
                    Return Info

                End If

                Return New Login.InformacionUsuario

            End Get
            Set(value As Login.InformacionUsuario)
                Session("BaseInformacoesUsuario") = value
            End Set
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

        Public Property MenuGrande As Boolean = False

        Public Overrides ReadOnly Property ControleErro() As Erro
            Get
                Return Erro1
            End Get
        End Property

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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

            _ValidacaoAcesso = New ValidacaoAcesso(InformacionUsuario)

        End Sub

        Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
            If MostrarCabecalho Then
                If HabilitarHistorico Then

                    Dim HistoricoExistente = Historico.Where(Function(x) x.Key.Split("?")(0) = Request.RawUrl.Split("?")(0)).SingleOrDefault()

                    If Not String.IsNullOrEmpty(HistoricoExistente.Key) Then
                        Historico.Remove(HistoricoExistente)
                    End If

                    AdicionaHistorico(New KeyValuePair(Of String, String)(Request.RawUrl, Titulo))
                    rptHistorico.DataSource = Historico
                    rptHistorico.DataBind()

                    If Request.Url.AbsoluteUri.Contains("SeleccionSector.aspx") Then
                        rptHistorico.Visible = False
                    End If

                End If
                Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
                lblUsuarioLogado.Text = If(usuario Is Nothing, String.Empty, String.Format("{0} {1} ", usuario.Nombre, usuario.Apellido))

                If (usuario IsNot Nothing) Then

                    '   Dim pais As Prosegur.Genesis.Comon.Clases.Pais = Nothing
                    If usuario.CodigoDelegacion IsNot Nothing Then
                        Dim oPais = New Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Pais.GetPaisDetail.Peticion
                        oPais.CodigoPais = usuario.CodigoPais
                        Dim pPais = New Prosegur.Global.GesEfectivo.IAC.LogicaNegocio.AccionPais
                        Dim rPais = pPais.GetPaisDetail(oPais)

                        If rPais IsNot Nothing AndAlso rPais.Pais.Count > 0 Then
                            Me.lblPais.Text = String.Format(" - {0}: {1} ", Traduzir("genPais"), rPais.Pais.FirstOrDefault().Description)
                        Else
                            Me.lblPais.Text = String.Empty 'String.Format(" - {0}: {1} ", Traduzir("genPais"), String.Empty)
                        End If

                        If usuario.CodigoDelegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(usuario.DesDelegacion) Then
                            Me.lblDelegacion.Text = String.Format(" - {0}: {1} ", Traduzir("genDelegacion"), usuario.DesDelegacion)
                            Me.lblDelegacion.ToolTip = Me.lblDelegacion.Text
                        Else
                            Me.lblDelegacion.Text = String.Empty
                        End If

                        If usuario.SectorLogado IsNot Nothing Then
                            If Not String.IsNullOrEmpty(usuario.SectorLogado.Descripcion) Then

                                linkSector.Text = String.Format("- {0}: {1}", Traduzir("genSector"), usuario.SectorLogado.Descripcion)
                                linkSector.ToolTip = linkSector.Text
                                ' linkSector.NavigateUrl = Page.ResolveUrl(Constantes.NOME_PAGINA_SELECCION_SECTOR)

                                If (Me.HaySoloUnoSector) Then
                                    Me.linkSector.Enabled = False
                                End If

                            Else
                                Me.linkSector.Visible = False
                            End If
                        Else
                            Me.linkSector.Visible = False
                        End If
                        ConstruirItensMenu()
                    End If

                End If
                If MostrarRodape Then
                    lblVersao.Text = Genesis.Comon.Util.VersionPantallas(System.Reflection.Assembly.GetExecutingAssembly())
                End If
            End If
            If MenuGrande Then
                divButtonsRodape.Attributes("class") = "ui-gn-panel-center-ui-gn-footer-panel-simple-main-template"
                divButtonArea.Attributes("class") = "ui-gn-simple-buttons-panel2"
            End If
            If HabilitarMenu Then
                linkImagemMenu.PostBackUrl = Page.ResolveUrl("~/Default.aspx")
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
        Private Sub ConstruirItensMenu()

            Dim menuIAC = New List(Of MenuReportes)

            ' adicionar item Clientes
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.CLIENTES,
                                              Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES,
                                              Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO,
                                              Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE,
                                              Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE,
                                              Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO,
                                              Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE},
                                     Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_clientes")
                itemIAC.ImageUrl = "imagenes/Clientes.gif"

                ' adicionar item clientes
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CLIENTES,
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaClientes.aspx"
                    itemIAC2.Text = Traduzir("mn_clientes")
                    itemIAC2.ImageUrl = "imagenes/Clientes.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar item subclientes
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES,
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaSubclientes.aspx"
                    itemIAC2.Text = Traduzir("mn_subclientes")
                    itemIAC2.ImageUrl = "imagenes/subclientes.png"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar punto de servicio
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO,
                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaPuntoServicio.aspx"
                    itemIAC2.Text = Traduzir("mn_pto_servicio")
                    itemIAC2.ImageUrl = "imagenes/ponto.png"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar Aprobación Cuentas Bancarias
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS,
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.Text = Traduzir("mn_cuentasBancarias")
                    itemIAC2.ImageUrl = "imagenes/institution.png"
                    itemIAC.SubMenus.Add(itemIAC2)

                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS,
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaAprobacionCuentasBancarias.aspx"
                        itemIAC3.Text = Traduzir("mn_aprobacionCuentasBancarias")
                        itemIAC3.ImageUrl = "imagenes/icon_success.png"
                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.APROBACION_CUENTAS_BANCARIAS,
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaAprobacionCuentasBancariasHistorico.aspx"
                        itemIAC3.Text = Traduzir("mn_historyCuentasBancarias")
                        itemIAC3.ImageUrl = "imagenes/icon_history.png"
                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                End If

                ' adicionar item Tipos
                If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE,
                                                       Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE,
                                                       Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO},
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.Text = Traduzir("mn_tipos")
                    itemIAC2.ImageUrl = "imagenes/People.png"

                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaTipoCliente.aspx"
                        itemIAC3.Text = Traduzir("mn_tipoCliente")
                        itemIAC3.ImageUrl = "imagenes/People.png"

                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE,
                                                 Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaTipoSubcliente.aspx"
                        itemIAC3.Text = Traduzir("mn_TipoSubcliente")
                        itemIAC3.ImageUrl = "imagenes/subclientes.png"

                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaTipoPuntoServicio.aspx"
                        itemIAC3.Text = Traduzir("mn_TipoPuntoServicio")
                        itemIAC3.ImageUrl = "imagenes/ponto.png"

                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar grupo de cliente
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaGrupoCliente.aspx"
                    itemIAC2.Text = Traduzir("mn_grupocliente")
                    itemIAC2.ImageUrl = "imagenes/Clientes.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If


                menuIAC.Add(itemIAC)

            End If

            ' adicionar item ATM
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.ATM,
                                                   Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_atm")
                itemIAC.ImageUrl = "imagenes/atm.gif"

                ' adicionar item ATM
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.ATM,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaATM.aspx"
                    itemIAC2.Text = Traduzir("mn_atm")
                    itemIAC2.ImageUrl = "imagenes/atm.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar item morfologia
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaMorfologias.aspx"
                    itemIAC2.Text = Traduzir("mn_morfologia")
                    itemIAC2.ImageUrl = "imagenes/morfologia.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                menuIAC.Add(itemIAC)

            End If

            ' adicionar item Estrutura
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.DELEGACION,
                                                   Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA,
                                                   Aplicacao.Util.Utilidad.eTelas.PUESTOS,
                                                   Aplicacao.Util.Utilidad.eTelas.SECTOR,
                                                   Aplicacao.Util.Utilidad.eTelas.CANALES,
                                                   Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR,
                                                   Aplicacao.Util.Utilidad.eTelas.PARAMETRO},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_estrutura")
                itemIAC.ImageUrl = "imagenes/delegaciones.gif"

                ' adicionar item Delegacione
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DELEGACION,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaDelegaciones.aspx"
                    itemIAC2.Text = Traduzir("mn_delegaciones")
                    itemIAC2.ImageUrl = "imagenes/delegaciones.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                'adicionar item Planta
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DELEGACION,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaPlanta.aspx"
                    itemIAC2.Text = Traduzir("mn_plantas")
                    itemIAC2.ImageUrl = "imagenes/031.png"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item PUESTOS
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PUESTOS,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaPuestos.aspx"
                    itemIAC2.Text = Traduzir("mn_puestos")
                    itemIAC2.ImageUrl = "imagenes/Puesto.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                ' adicionar item setor
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.SECTOR,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaSector.aspx"
                    itemIAC2.Text = Traduzir("mn_Sector")
                    itemIAC2.ImageUrl = "imagenes/sector.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item canal
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CANALES,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaCanales.aspx"
                    itemIAC2.Text = Traduzir("mn_item_canal")
                    itemIAC2.ImageUrl = "imagenes/canal.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                'adicionar item tipo setor
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaTipoSector.aspx"
                    itemIAC2.Text = Traduzir("mn_tipoSetor")
                    itemIAC2.ImageUrl = "imagenes/tipo.jpg"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item Parametros
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PARAMETRO,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaParametros.aspx"
                    itemIAC2.Text = Traduzir("mn_parametros")
                    itemIAC2.ImageUrl = "imagenes/Parametro.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item MAE
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MAE,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then


                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaMAE.aspx"
                    itemIAC2.Text = Traduzir("mn_MAE")
                    itemIAC2.ImageUrl = "imagenes/MAE.png"
                    itemIAC.SubMenus.Add(itemIAC2)
                    ' adicionar item MAE
                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MAE,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC3 As New MenuReportes
                        itemIAC3.NavigateUrl = "BusquedaMAE.aspx"
                        itemIAC3.Text = Traduzir("mn_MAE")
                        itemIAC3.ImageUrl = "imagenes/MAE.png"
                        itemIAC2.SubMenus.Add(itemIAC3)
                    End If

                    ' adicionar item Acciones en Lote
                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.ACCIONESLOTE,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC4 As New MenuReportes
                        itemIAC4.NavigateUrl = "MantenimientoAccionesEnLote.aspx"
                        itemIAC4.Text = Traduzir("mn_accionesLote")
                        itemIAC4.ImageUrl = "imagenes/MAE.png"
                        itemIAC2.SubMenus.Add(itemIAC4)

                    End If

                    ' adicionar item Periodos de acreditacion
                    If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PANTALLA_PERIODOS,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                        Dim itemIAC5 As New MenuReportes
                        itemIAC5.NavigateUrl = "BusquedaPeriodosAcreditacion.aspx"
                        itemIAC5.Text = Traduzir("mn_periodosAcreditacion")
                        itemIAC5.ImageUrl = "imagenes/periodosAcreditacion.png"
                        itemIAC2.SubMenus.Add(itemIAC5)

                    End If

                End If

                ' adicionar item Planificações
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PLANIFICACION,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaPlanificaciones.aspx"
                    itemIAC2.Text = Traduzir("mn_planificaciones")
                    itemIAC2.ImageUrl = "imagenes/ic_planificacion.png"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                menuIAC.Add(itemIAC)
            End If

            ' adicionar item Divisas
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.DIVISAS,
                                                   Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO,
                                                   Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_item_divisas")
                itemIAC.ImageUrl = "imagenes/divisa.gif"

                ' adicionar item divisa
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DIVISAS,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaDivisas.aspx"
                    itemIAC2.Text = Traduzir("mn_item_divisas")
                    itemIAC2.ImageUrl = "imagenes/divisa.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item medio pago
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaMediosPago.aspx"
                    itemIAC2.Text = Traduzir("mn_mediopago")
                    itemIAC2.ImageUrl = "imagenes/mediopago.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item agrupaciones
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaAgrupaciones.aspx"
                    itemIAC2.Text = Traduzir("mn_agrupaciones")
                    itemIAC2.ImageUrl = "imagenes/agrupaciones.gif"
                    itemIAC.SubMenus.Add(itemIAC2)

                End If

                menuIAC.Add(itemIAC)
            End If

            ' adicionar item IAC
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE,
                                                   Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC,
                                                   Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_item_iac")
                itemIAC.ImageUrl = "imagenes/iac.gif"

                ' adicionar item iac
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaIac.aspx"
                    itemIAC2.Text = Traduzir("mn_item_iac")
                    itemIAC2.ImageUrl = "imagenes/iac.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item terminos
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaTerminos.aspx"
                    itemIAC2.Text = Traduzir("mn_terminos")
                    itemIAC2.ImageUrl = "imagenes/terminos.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item valores posibles
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaValoresPosibles.aspx"
                    itemIAC2.Text = Traduzir("mn_valoresposibles")
                    itemIAC2.ImageUrl = "imagenes/valores_posibles.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If



                menuIAC.Add(itemIAC)

            End If

            ' adicionar item permisos
            If True Then 'TODO Reemplazar por el rol correspondiente Then
                '_ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_ROLES,
                '                                       Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_USUARIOS},
                '                                       String.Empty) Then
                Dim itemPermisos As New MenuReportes
                itemPermisos.Text = Traduzir("mn_permisos")
                itemPermisos.ImageUrl = "imagenes/group-key-icon.png"
                menuIAC.Add(itemPermisos)

                If True Then
                    '_ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_ROLES,
                    '                 String.Empty) Then

                    Dim itemPermisos2 As New MenuReportes
                    itemPermisos2.NavigateUrl = "BusquedaRoles.aspx"
                    itemPermisos2.Text = Traduzir("mn_roles")
                    itemPermisos2.ImageUrl = "imagenes/key-icon.png"
                    itemPermisos.SubMenus.Add(itemPermisos2)
                End If

                If True Then
                    '_ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CONFIGURACION_USUARIOS,
                    '                  String.Empty) Then

                    Dim itemPermisos3 As New MenuReportes
                    itemPermisos3.NavigateUrl = "BusquedaUsuarios.aspx"
                    itemPermisos3.Text = Traduzir("mn_usuarios")
                    itemPermisos3.ImageUrl = "imagenes/group.gif"
                    itemPermisos.SubMenus.Add(itemPermisos3)
                End If

            End If

            ' adicionar item processo
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCESOS,
                                          Aplicacao.Util.Utilidad.eAcoesTela._BUSCAR) OrElse
                _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.PRODUCTOS,
                                                    Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS,
                                                    Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_proceso")
                itemIAC.ImageUrl = "imagenes/proceso.gif"

                ' adicionar item processo
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCESOS,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._BUSCAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaProcesos.aspx"
                    itemIAC2.Text = Traduzir("mn_proceso")
                    itemIAC2.ImageUrl = "imagenes/proceso.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item produtos
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PRODUCTOS,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaProductos.aspx"
                    itemIAC2.Text = Traduzir("mn_item_producto")
                    itemIAC2.ImageUrl = "imagenes/produtos.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' Buscar Características
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS,
                                                     Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaCaracteristicas.aspx"
                    itemIAC2.Text = Traduzir("mn_caracteristicas")
                    itemIAC2.ImageUrl = "imagenes/caracteristicas.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                ' adicionar item tipos procesado
                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO,
                                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaTiposProcesado.aspx"
                    itemIAC2.Text = Traduzir("mn_item_tiposprocesado")
                    itemIAC2.ImageUrl = "imagenes/modalidrecuento.gif"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                menuIAC.Add(itemIAC)
            End If

            ' adicionar item procedencia
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.TIPOPROCEDENCIA,
                                                   Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS},
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC As New MenuReportes
                itemIAC.Text = Traduzir("mn_Procedencia")
                itemIAC.ImageUrl = "imagenes/Procedencia.png"

                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOPROCEDENCIA,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaTipoProcedencia.aspx"
                    itemIAC2.Text = Traduzir("mn_tipoProcedencia")
                    itemIAC2.ImageUrl = "imagenes/Procedencia.png"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If

                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC2 As New MenuReportes
                    itemIAC2.NavigateUrl = "BusquedaProcedencias.aspx"
                    itemIAC2.Text = Traduzir("mn_Procedencia")
                    itemIAC2.ImageUrl = "imagenes/Procedencia.png"
                    itemIAC.SubMenus.Add(itemIAC2)
                End If


                menuIAC.Add(itemIAC)
            End If

            'Integraciones
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.ORDENES_SERVICIO,
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then
                Dim itemIntegracionesIAC As New MenuReportes
                itemIntegracionesIAC.Text = Traduzir("mn_Integraciones")
                itemIntegracionesIAC.ImageUrl = "imagenes/integraciones.png"

                Dim itemFacturacionIAC As New MenuReportes
                itemFacturacionIAC.NavigateUrl = ResolveUrl("~/OrdenesDeServicio.aspx")
                itemFacturacionIAC.Text = Traduzir("mn_Facturacion")
                itemFacturacionIAC.ImageUrl = "imagenes/facturacion.png"

                itemIntegracionesIAC.SubMenus.Add(itemFacturacionIAC)

                menuIAC.Add(itemIntegracionesIAC)
            End If

            Dim itemInfoIAC As New MenuReportes
            itemInfoIAC.Text = Framework.Dicionario.Tradutor.Traduzir("mn_Ayuda")
            itemInfoIAC.ImageUrl = "imagenes/help_parametros.png"


            Dim itemInfoIAC1 As New MenuReportes
            itemInfoIAC1.NavigateUrl = ResolveUrl("~/InformacionIAC.aspx")
            itemInfoIAC1.Text = Traduzir("mn_InformacionIAC")
            itemInfoIAC1.ImageUrl = "imagenes/help_parametros.png"

            itemInfoIAC.SubMenus.Add(itemInfoIAC1)

            menuIAC.Add(itemInfoIAC)

            repeatMenu.DataSource = menuIAC
            repeatMenu.DataBind()

        End Sub

        Public Overrides Sub ExibirModal(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True, Optional disparaEvento As Boolean = False, Optional botao As String = "")
            If urlCaminho.Contains("?") Then
                urlCaminho &= "&divModal=" & divModal.ClientID & "&ifrModal=" & ifrModal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
            Else
                urlCaminho &= "?divModal=" & divModal.ClientID & "&ifrModal=" & ifrModal.ClientID & If(String.IsNullOrEmpty(botao), "", "&btnExecutar=" & botao)
            End If
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "exibirModal", "ExibirUrlModal('#" & divModal.ClientID & "', '#" & ifrModal.ClientID & "', '" & urlCaminho & "','" & tituloModal & "', " & altura & ", " & largura & "," & efetuarReload.ToString().ToLower() & "," & disparaEvento.ToString().ToLower() & ",'" & botao.ToString() & "' );", True)
        End Sub
        Protected Sub repeatMenu_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
            If (e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem) Then
                Exit Sub
            End If

            If (e.Item.Controls.Contains(CType(e.Item.FindControl("repeatSubMenu"), Repeater))) Then
                Dim oRepeater As Repeater = CType(e.Item.FindControl("repeatSubMenu"), Repeater)

                Dim oMenu = CType(e.Item.DataItem, MenuReportes)
                If oMenu IsNot Nothing Then
                    If oMenu.SubMenus.Count > 0 Then
                        oRepeater.DataSource = oMenu.SubMenus
                        oRepeater.DataBind()
                    End If
                End If

            End If
        End Sub

        Protected Sub repeatSubMenu_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
            If (e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem) Then
                Exit Sub
            End If

            If (e.Item.Controls.Contains(CType(e.Item.FindControl("SubSubMenu"), HtmlGenericControl))) Then
                Dim teste As HtmlGenericControl = CType(e.Item.FindControl("SubSubMenu"), HtmlGenericControl)
                If (teste.Controls.Contains(CType(e.Item.FindControl("repeatSubSubMenu"), Repeater))) Then
                    Dim oRepeater As Repeater = CType(teste.FindControl("repeatSubSubMenu"), Repeater)

                    Dim oMenu = CType(e.Item.DataItem, MenuReportes)
                    If oMenu IsNot Nothing Then
                        If oMenu.SubMenus.Count > 0 Then
                            oRepeater.DataSource = oMenu.SubMenus
                            oRepeater.DataBind()
                        Else
                            teste.Visible = False
                        End If
                    End If
                Else
                    teste.Visible = False
                End If
            End If

        End Sub

#Region "Classe Menu"
        Private Class MenuReportes
            Public Property NavigateUrl As String
            Public Property Text As String
            Public Property ImageUrl As String
            Public Property SubMenus As List(Of MenuReportes)
            Public Sub New()
                SubMenus = New List(Of MenuReportes)()
                NavigateUrl = "#"
            End Sub
        End Class
#End Region

    End Class
End Namespace