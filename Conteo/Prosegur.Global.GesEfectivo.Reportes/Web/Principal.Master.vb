Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon.Extenciones

Partial Public Class Principal
    Inherits System.Web.UI.MasterPage

#Region "[VARIÁVEIS]"

    Private _MostrarMenu As Boolean = True
    Private _ValidacaoAcesso As ValidacaoAcesso = Nothing
    Private _PrimeiroControleTelaID As String = String.Empty

#End Region

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Permite acesso ao userControl de controle de erro.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ControleErro() As Erro
        Get
            Return Erro1
        End Get
    End Property

    ''' <summary>
    ''' Configura se deve ou não mostrar o menu.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MostrarMenu() As Boolean
        Get
            Return _MostrarMenu
        End Get
        Set(value As Boolean)
            _MostrarMenu = value
        End Set
    End Property

    ''' <summary>
    ''' Informacoes do usuario logado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
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

    ''' <summary>
    ''' Configura o titulo da página.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Public Property TituloPagina()
        Get
            If ViewState("MasterPageTitle") Is Nothing Then
                Return Traduzir("lbl_base_titulo")
            End If
            Return Traduzir("lbl_base_titulo") & " - " & ViewState("MasterPageTitle")
        End Get
        Set(value)
            ViewState("MasterPageTitle") = value
            Page.Title = Traduzir("lbl_base_titulo") & " - " & ViewState("MasterPageTitle")
        End Set
    End Property

    ''' <summary>
    ''' Retorna o Controle de saída da página
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Sair() As System.Web.UI.Control
        Get
            Return Me.lbSair
        End Get
    End Property

    ''' <summary>
    ''' Retorna o Controle de Menu carregado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RetornaMenu() As Menu
        Get
            Return Me.menuReportes
        End Get
    End Property

    ''' <summary>
    ''' Recebe o primeiro controle da tela
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PrimeiroControleTelaID() As String
        Get
            Return _PrimeiroControleTelaID
        End Get
        Set(value As String)
            _PrimeiroControleTelaID = value
        End Set
    End Property

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento ao carregar a master page
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try

            ' obtém a versão do sistema
            With My.Application.Info.Version
                lblVersion.Text = Traduzir("lbl_version") & " " & .Major & "." & .Minor & " (Build " & .Build & "." & .Revision & ")"
            End With

            ' criar objeto validacao de acesso.
            _ValidacaoAcesso = New ValidacaoAcesso(InformacionUsuario)

            ' traduzir os controles da tela
            TraduzirControles()

            ' setar informações da tela
            SetarInformacoes()

            ' prepara o menu
            PrepararMenu()

            ' Este script serve para o funcionamento correto da tabulação dos controles.
            ' Ele teve que ser adicionado no Page_Load, porque não estava funcionando no 'aspx'
            ' para algumas telas
            ltr_js.Text = "<script>"
            ltr_js.Text &= "var menuId = '" & Me.menuReportes.ClientID & "';"
            ltr_js.Text &= "var primeiroControleId = '" & PrimeiroControleTelaID & "';"
            ltr_js.Text &= "var idControleErro = '" & ControleErro.LinkDetalhes.ClientID & "';"
            ltr_js.Text &= "</script>"

        Catch ex As Exception

            ControleErro.TratarErroException(ex)

        End Try

    End Sub

    ''' <summary>
    ''' Ação click do link, efetua logout do sistema.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Protected Sub lbSair_Click(sender As Object, e As EventArgs) Handles lbSair.Click

        Try

            ' limpar sessão que contém informações do usuário.
            InformacionUsuario = Nothing

            ' redirecionar para página de login.
            Response.Redireccionar("~/" & Constantes.NOME_PAGINA_LOGIN & "?Salir=1", False)

        Catch ex As Exception

            ControleErro.TratarErroException(ex)

        End Try

    End Sub

#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Seta informações na tela
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Private Sub SetarInformacoes()

        ' se obteve objeto
        If InformacionUsuario IsNot Nothing AndAlso _
           InformacionUsuario.Delegaciones IsNot Nothing AndAlso _
           InformacionUsuario.Delegaciones.Count > 0 Then

            ' mostrar os labels
            lblData.Visible = True
            lblUsuario.Visible = True
            lbSair.Visible = True

            ' setar nome e data
            lblUsuario.Text = InformacionUsuario.Nombre
            lblData.Text = DateTime.Now.ToString("dd/MM/yyyy")

        Else

            ' esconder os labels
            lblData.Visible = False
            lblUsuario.Visible = False
            lbSair.Visible = False

        End If

    End Sub

    ''' <summary>
    ''' Prepara o menu
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Private Sub PrepararMenu()

        ' verificar se usuário está logado e se é para mostrar o menu.
        If _ValidacaoAcesso.UsuarioLogado AndAlso MostrarMenu Then

            ' caso esteja, deve mostrar menu
            menuReportes.Visible = True

            ' criar itens do menu
            ConstruirItensMenu()

        Else
            ' caso esteja, deve esconder menu
            menuReportes.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Criar os itens do menu de acordo com as permissões do usuário.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Private Sub ConstruirItensMenu()

        If menuReportes.Items.Count > 0 Then
            Exit Sub
        End If

        ' adiciona o item billetaje por sucursal
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.BILLETAJE_SUCURSAL, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaBilletajeSucursal.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_billetaje_sucursal")
            itemReporte.ImageUrl = "imagenes/billetaje_sucursal.gif"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item corte parcial
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.CORTE_PARCIAL, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaCorteParcial.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_corte_parcial")
            itemReporte.ImageUrl = "imagenes/corte_parcial.gif"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item respaldo completo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RESPALDO_COMPLETO, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaRespaldoCompleto.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_respaldo_completo")
            itemReporte.ImageUrl = "imagenes/respaldo_completo.gif"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item respaldo completo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.DETALLE_PARCIALES, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaDetalleParciales.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_detalle_parciales")
            itemReporte.ImageUrl = "imagenes/table_go.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Contado por Puesto
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.CONTADO_PUESTO, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaContadoPorPuesto.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_contado_posto")
            itemReporte.ImageUrl = "imagenes/contado_puesto.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Recibo F22 Respaldo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RECIBO_F22_RESPALDO, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaReciboF22Respaldo.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_recibo_f22_respaldo")
            itemReporte.ImageUrl = "imagenes/recibo_f22_respaldo.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Recibo F22 Respaldo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.RELACION_HABILITACION_TIRA_CONTEO, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaRegistroTira.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_relaciones_habilitacion_tira_conteo")
            itemReporte.ImageUrl = "imagenes/Relacion_Habilitacion_Tira_Conteo.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Recibo F22 Respaldo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.PARTE_DIFERENCIAS, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaParteDiferencias.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_parte_diferencias")
            itemReporte.ImageUrl = "imagenes/parte_diferencias.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Recibo F22 Respaldo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.INFORME_RESULTADO_CONTAJE, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/BusquedaInformeResultadoContaje.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_informe_resultado_contaje")
            itemReporte.ImageUrl = "imagenes/contado_puesto.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona o item Recibo F22 Respaldo
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTAR_PEDIDO_BCP, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/ReportarPedidoBCP.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_reportar_pedido_bcp")
            itemReporte.ImageUrl = "imagenes/request_report.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona a página Reportes
        If _ValidacaoAcesso.ValidarAcaoPagina(Enumeradores.eTelas.REPORTES, _
                                              Enumeradores.eAcoesTela._CONSULTAR) Then

            Dim itemReporte As New MenuItem
            itemReporte.NavigateUrl = "~/Reportes.aspx"
            itemReporte.Text = "&nbsp;" & Traduzir("mn_reportes")
            itemReporte.ImageUrl = "imagenes/Reportes.png"
            menuReportes.Items.Add(itemReporte)

        End If

        ' adiciona um menu 'invisível" para que a tabução entre os campos funcione corretamente
        menuReportes.Items.Add(New MenuItem(String.Empty))

    End Sub

    ''' <summary>
    ''' Traduz os controles da master page.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' </history>
    Private Sub TraduzirControles()
        Page.Title = TituloPagina
        lbSair.Text = Traduzir("lbl_salir")
    End Sub

#End Region

End Class
