Imports System.Reflection
Imports Prosegur.Framework.Dicionario
Imports Prosegur.Genesis.Comunicacion

Namespace Master

    Public Class Master
        Inherits System.Web.UI.MasterPage


        Private _ValidacaoAcesso As ValidacaoAcesso = Nothing

        Public Property InformacionUsuario() As ContractoServ.Login.InformacionUsuario
            Get

                ' se sessão não foi setado
                If Session("BaseInformacoesUsuario") IsNot Nothing Then

                    ' tentar recuperar objeto da sessao
                    Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServ.Login.InformacionUsuario)

                    ' retornar objeto
                    Return Info

                End If

                Return New ContractoServ.Login.InformacionUsuario

            End Get
            Set(value As ContractoServ.Login.InformacionUsuario)
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

        Public ReadOnly Property ControleErro() As Erro
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

            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetGMT", "var _GMT = 0 ; var _AjusteVerano = 0;", True)

            'Registra variaveis de tradução que são usadas no calendario
            ' Dim imgUrl As String = ResolveUrl("~/App_Themes/Padrao/css/img/button/Calendar.png")
            Dim imgUrl As String = ResolveUrl("~/Imagenes/Calendar.png")
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SetTraducao",
                                                    String.Format("var _bntProximo = '{0}';" &
                                                                  "var _btnAnterior = '{1}';" &
                                                                  "var _btnAgora = '{2}';" &
                                                                  "var _btnConfirma = '{3}';" &
                                                                  "var _meses = '{4}';" &
                                                                  "var _dias = '{5}';" &
                                                                  "var _horas = '{6}';" &
                                                                  "var _minutos = '{7}';" &
                                                                  "var _segundos = '{8}';" &
                                                                  "var _imgCalendar = '{9}';",
                                                                  Tradutor.Traduzir("bntProximo"),
                                                                  Tradutor.Traduzir("btnAnterior"),
                                                                  Tradutor.Traduzir("btnAgora"),
                                                                  Tradutor.Traduzir("btnConfirma"),
                                                                  Tradutor.Traduzir("meses"),
                                                                  Tradutor.Traduzir("dias"),
                                                                  Tradutor.Traduzir("horas"),
                                                                  Tradutor.Traduzir("minutos"),
                                                                  Tradutor.Traduzir("segundos"),
                                                                  imgUrl), True)
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

                    Dim pais As Prosegur.Genesis.Comon.Clases.Pais = Nothing
                    If usuario.CodigoDelegacion IsNot Nothing Then
                        pais = Prosegur.Genesis.LogicaNegocio.Genesis.Pais.ObtenerPaisPorDelegacion(usuario.CodigoDelegacion)
                    End If
                    If pais IsNot Nothing Then
                        Me.lblPais.Text = String.Format(" - {0}: {1} ", Tradutor.Traduzir("genPais"), pais.Descripcion)
                    Else
                        Me.lblPais.Text = String.Empty 'String.Format(" - {0}: {1} ", Traduzir("genPais"), String.Empty)
                    End If


                    If usuario.CodigoDelegacion IsNot Nothing AndAlso Not String.IsNullOrEmpty(usuario.DesDelegacion) Then
                        Me.lblDelegacion.Text = String.Format(" - {0}: {1} ", Tradutor.Traduzir("genDelegacion"), usuario.DesDelegacion)
                        Me.lblDelegacion.ToolTip = Me.lblDelegacion.Text
                    Else
                        Me.lblDelegacion.Text = String.Empty
                    End If

                    If usuario.SectorLogado IsNot Nothing Then
                        If Not String.IsNullOrEmpty(usuario.SectorLogado.Descripcion) Then

                            linkSector.Text = String.Format("- {0}: {1}", Tradutor.Traduzir("genSector"), usuario.SectorLogado.Descripcion)
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

            If MenuGrande Then
                divButtonsRodape.Attributes("class") = "ui-gn-panel-center-ui-gn-footer-panel-simple-main-template"
                divButtonArea.Attributes("class") = "ui-gn-simple-buttons-panel2"
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

            Dim menuReportes = New List(Of MenuReportes)

            ' adiciona o item billetaje por sucursal
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.BILLETAJE_SUCURSAL, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaBilletajeSucursal.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_billetaje_sucursal")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/billetaje_sucursal.gif")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item corte parcial
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.CORTE_PARCIAL, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaCorteParcial.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_corte_parcial")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/corte_parcial.gif")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item respaldo completo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RESPALDO_COMPLETO, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaRespaldoCompleto.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_respaldo_completo")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/respaldo_completo.gif")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item respaldo completo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.DETALLE_PARCIALES, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaDetalleParciales.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_detalle_parciales")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/table_go.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Contado por Puesto
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.CONTADO_PUESTO, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaContadoPorPuesto.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_contado_posto")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/contado_puesto.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Recibo F22 Respaldo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RECIBO_F22_RESPALDO, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaReciboF22Respaldo.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_recibo_f22_respaldo")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/recibo_f22_respaldo.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Recibo F22 Respaldo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RELACION_HABILITACION_TIRA_CONTEO, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaRegistroTira.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_relaciones_habilitacion_tira_conteo")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/Relacion_Habilitacion_Tira_Conteo.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Recibo F22 Respaldo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.PARTE_DIFERENCIAS, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaParteDiferencias.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_parte_diferencias")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/parte_diferencias.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Recibo F22 Respaldo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.INFORME_RESULTADO_CONTAJE, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/BusquedaInformeResultadoContaje.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_informe_resultado_contaje")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/contado_puesto.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona o item Recibo F22 Respaldo
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTAR_PEDIDO_BCP, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/ReportarPedidoBCP.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_reportar_pedido_bcp")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/request_report.png")
                menuReportes.Add(itemReporte)

            End If

            ' adiciona a página Reportes
            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                                  Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes
                itemReporte.NavigateUrl = ResolveUrl("~/Reportes.aspx")
                itemReporte.Text = Tradutor.Traduzir("mn_reportes")
                itemReporte.ImageUrl = ResolveUrl("~/imagenes/Reportes.png")
                menuReportes.Add(itemReporte)

            End If



            If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                                                          Enumeradores.eAcoesTela._CONSULTAR) Then

                Dim itemReporte As New MenuReportes

                Dim valorParametro = Nothing

#If DEBUG Then
                If (Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.AllKeys.Contains("UrlSitioDebug_ConsultaLocal") AndAlso Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("UrlSitioDebug_ConsultaLocal").Trim().Length() > 0) Then
                    valorParametro = Prosegur.Genesis.Web.Login.Configuraciones.AppSettings.Get("UrlSitioDebug_ConsultaLocal").Trim() & "/index.html#!/"
                Else
                    valorParametro = "http://localhost/Prosegur.Genesis.ConsultaLocal.Producto." & Prosegur.Genesis.Comon.Util.Version & "/index.html#!/"
                End If
#Else
                valorParametro = ObtenerParametro(Constantes.CODIGO_PARAMETRO_MAES_URL, Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoDelegacion)
#End If

                If Not String.IsNullOrEmpty(valorParametro) Then

                    itemReporte.NavigateUrl = valorParametro + "?codigoDelegacion=" + Genesis.Web.Login.Parametros.Permisos.Usuario.CodigoDelegacion + "&descripcionDelegacion=" + Genesis.Web.Login.Parametros.Permisos.Usuario.DesDelegacion + "&usuarioAD=" + Genesis.Web.Login.Parametros.Permisos.Usuario.IdentificadorUsuarioAD
                    itemReporte.Text = Tradutor.Traduzir("mn_acreditaciones")
                    itemReporte.ImageUrl = ResolveUrl("~/imagenes/billetaje_sucursal.gif")
                    menuReportes.Add(itemReporte)
                End If

            End If


            Dim itemInfoReportes1 As New MenuReportes
            itemInfoReportes1.NavigateUrl = ResolveUrl("~/InformacionReportes.aspx")
            itemInfoReportes1.Text = Tradutor.Traduzir("mn_InformacionReportes")
            itemInfoReportes1.ImageUrl = ResolveUrl("~/imagenes/ic_help.png")

            menuReportes.Add(itemInfoReportes1)

            repeatMenu.DataSource = menuReportes
            repeatMenu.DataBind()

        End Sub

        Public Sub ExibirModal(urlCaminho As String, tituloModal As String, altura As Int32, largura As Int32, Optional efetuarReload As Boolean = True)
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "exibirModal", "ExibirUrlModal('#" & divModal.ClientID & "', '#" & ifrModal.ClientID & "', '" & urlCaminho & "','" & tituloModal & "', " & altura & ", " & largura & "," & efetuarReload.ToString().ToLower() & ");", True)
        End Sub

        Private Function ObtenerParametro(codigo As String, delegacion As String) As String

            Dim result = Nothing
            Dim objProxyParametro As New ProxyParametro
            Dim objPeticionParametro As New IAC.ContractoServicio.Parametro.GetParametroDetail.Peticion
            Dim listParametros As New List(Of String)
            Dim parametros As List(Of Genesis.Comon.Clases.Parametro) = Nothing

            listParametros.Add(Constantes.CODIGO_PARAMETRO_MAES_URL)

            parametros = Prosegur.Genesis.AccesoDatos.Genesis.Parametros.ObtenerParametrosDelegacionPais(delegacion, "Reportes", listParametros)

            If listParametros.Any AndAlso parametros IsNot Nothing Then
                result = parametros.FirstOrDefault(Function(x) x.codigoParametro.Equals(codigo))
            End If


            If result IsNot Nothing Then
                Return result.valorParametro
            Else
                Return String.Empty
            End If

        End Function

#Region "Classe Menu"
        Private Class MenuReportes
            Public Property NavigateUrl As String
            Public Property Text As String
            Public Property ImageUrl As String
        End Class
#End Region

    End Class
End Namespace