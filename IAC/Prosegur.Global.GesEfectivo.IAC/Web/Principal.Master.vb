Imports Prosegur.Framework.Dicionario.Tradutor
Imports System.Web.UI.WebControls.MenuItem
Imports DevExpress.Web

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
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Public Property InformacionUsuario() As ContractoServicio.Login.InformacionUsuario
        Get

            ' se sessão não foi setado
            If Session("BaseInformacoesUsuario") IsNot Nothing Then

                ' tentar recuperar objeto da sessao
                Dim Info = TryCast(Session("BaseInformacoesUsuario"), ContractoServicio.Login.InformacionUsuario)

                ' retornar objeto
                Return Info

            End If

            Return New ContractoServicio.Login.InformacionUsuario

        End Get
        Set(value As ContractoServicio.Login.InformacionUsuario)
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
    ''' [octavio.piramo] 23/02/2009 Criado
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
            Return Cabecalho.lbSair
        End Get
    End Property

    ''' <summary>
    ''' Retorna o Controle de Menu carregado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RetornaMenu() As ASPxMenu.ASPxMenu
        Get
            Return Me.menuIAC
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
    ''' [octavio.piramo]10/02/2009 Criado
    ''' </history>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Try

            ' obtém a versão do sistema
            With My.Application.Info.Version
                Cabecalho.lblVersion.Text = Traduzir("lbl_version") & " " & .Major & "." & .Minor & " (Build " & .Build & "." & .Revision & ")"
            End With

            ' criar objeto validacao de acesso.
            _ValidacaoAcesso = New ValidacaoAcesso(InformacionUsuario)

            If Not Page.IsPostBack Then

                'Tradução de mensagens para scripts. (Traduz a mensagem de registro não vigente)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "Msg", "var MsgRegistroBorrado = '" & Traduzir("info_msg_registro_borrado") & "';", True)

            End If

            ' traduzir os controles da tela
            TraduzirControles()

            ' setar informações da tela
            SetarInformacoes()

            ' prepara o menu
            PrepararMenu()

            ' Este script serve para o funcionamento correto da tabulação dos controles.
            ' Ele teve que ser adicionado no Page_Load, porque não estava funcionando no 'aspx'
            ' para algumas telas
            ltr_js.Text = "<script language='javascript' type='text/javascript'>"
            ltr_js.Text &= "var menuId = '" & Me.menuIAC.ClientID & "';"
            ltr_js.Text &= "var primeiroControleId = '" & PrimeiroControleTelaID & "';"
            ltr_js.Text &= "var idControleErro = '" & ControleErro.IdPrimeiroControle & "';"
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
    ''' [octavio.piramo] 13/03/2009 Criado
    ''' </history>
    Protected Sub lbSair_Click(sender As Object, e As EventArgs) Handles Cabecalho.Sair_Click

        Try

            ' limpar sessão que contém informações do usuário.
            InformacionUsuario = Nothing

            ' redirecionar para página de login.
            Response.Redirect("~/" & ContractoServicio.Constantes.NOME_PAGINA_LOGIN & "?Salir=1", False)

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
    ''' [octavio.piramo] 10/02/2009 Criado
    ''' </history>
    Private Sub SetarInformacoes()

        ' se obteve objeto
        If InformacionUsuario IsNot Nothing _
            AndAlso InformacionUsuario.Permisos IsNot Nothing _
            AndAlso InformacionUsuario.Permisos.Count > 0 Then

            ' mostrar os labels
            Cabecalho.lblData.Visible = True
            Cabecalho.lblUsuario.Visible = True
            Cabecalho.lbSair.Visible = True

            ' setar nome e data
            Cabecalho.lblUsuario.Text = InformacionUsuario.Nombre
            Cabecalho.lblData.Text = DateTime.Now.ToString("dd/MM/yyyy")

        Else

            ' esconder os labels
            Cabecalho.lblData.Visible = False
            Cabecalho.lblUsuario.Visible = False
            Cabecalho.lbSair.Visible = False

        End If

    End Sub

    ''' <summary>
    ''' Prepara o menu
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub PrepararMenu()

        ' verificar se usuário está logado e se é para mostrar o menu.
        If _ValidacaoAcesso.UsuarioLogado AndAlso MostrarMenu Then

            ' caso esteja, deve mostrar menu
            menuIAC.Visible = True

            ' criar itens do menu
            ConstruirItensMenu()

        Else
            ' caso esteja, deve esconder menu
            menuIAC.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' Criar os itens do menu de acordo com as permissões do usuário.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 11/02/2009 Criado
    ''' </history>
    Private Sub ConstruirItensMenu()

        If menuIAC.Items.Count > 0 Then
            Exit Sub
        End If


        ' adicionar item Clientes
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.CLIENTES,
                                               Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES,
                                               Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO,
                                               Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE,
                                               Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE,
                                               Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO,
                                               Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_clientes")
            itemIAC.Image.Url = "imagenes/Clientes.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item clientes
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CLIENTES, _
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem()
                itemIAC2.NavigateUrl = "~/BusquedaClientes.aspx"
                itemIAC2.Text = Traduzir("mn_clientes")
                itemIAC2.Image.Url = "imagenes/Clientes.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

            ' adicionar item subclientes
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.SUB_CLIENTES, _
                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaSubclientes.aspx"
                itemIAC2.Text = Traduzir("mn_subclientes")
                itemIAC2.Image.Url = "imagenes/subclientes.png"
                itemIAC.Items.Add(itemIAC2)

            End If

            ' adicionar punto de servicio
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PUNTO_SERVICIO, _
                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaPuntoServicio.aspx"
                itemIAC2.Text = Traduzir("mn_pto_servicio")
                itemIAC2.Image.Url = "imagenes/ponto.png"
                itemIAC.Items.Add(itemIAC2)

            End If

            ' adicionar item Tipos
            If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE,
                                                   Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE,
                                                   Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO}, _
                                          Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem()
                itemIAC2.Text = Traduzir("mn_tipos")
                itemIAC2.Image.Url = "imagenes/People.png"
                itemIAC.Items.Add(itemIAC2)

                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOCLIENTE, _
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC3 As New ASPxMenu.MenuItem
                    itemIAC3.NavigateUrl = "~/BusquedaTipoCliente.aspx"
                    itemIAC3.Text = Traduzir("mn_tipoCliente")
                    itemIAC3.Image.Url = "imagenes/People.png"

                    itemIAC2.Items.Add(itemIAC3)
                End If

                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOSUBCLIENTE, _
                                             Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC3 As New ASPxMenu.MenuItem
                    itemIAC3.NavigateUrl = "~/BusquedaTipoSubcliente.aspx"
                    itemIAC3.Text = Traduzir("mn_TipoSubcliente")
                    itemIAC3.Image.Url = "imagenes/subclientes.png"

                    itemIAC2.Items.Add(itemIAC3)
                End If

                If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOPUNTOSERVICIO, _
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                    Dim itemIAC3 As New ASPxMenu.MenuItem
                    itemIAC3.NavigateUrl = "~/BusquedaTipoPuntoServicio.aspx"
                    itemIAC3.Text = Traduzir("mn_TipoPuntoServicio")
                    itemIAC3.Image.Url = "imagenes/ponto.png"

                    itemIAC2.Items.Add(itemIAC3)
                End If

            End If

            ' adicionar grupo de cliente
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.GRUPO_CLIENTE, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaGrupoCliente.aspx"
                itemIAC2.Text = Traduzir("mn_grupocliente")
                itemIAC2.Image.Url = "imagenes/Clientes.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

        End If


        ' adicionar item ATM
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.ATM,
                                               Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_atm")
            itemIAC.Image.Url = "imagenes/atm.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item ATM
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.ATM, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaATM.aspx"
                itemIAC2.Text = Traduzir("mn_atm")
                itemIAC2.Image.Url = "imagenes/atm.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

            ' adicionar item morfologia
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaMorfologias.aspx"
                itemIAC2.Text = Traduzir("mn_morfologia")
                itemIAC2.Image.Url = "imagenes/morfologia.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

        End If

        ' adicionar item Estrutura
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.DELEGACION,
                                               Aplicacao.Util.Utilidad.eTelas.MORFOLOGIA,
                                               Aplicacao.Util.Utilidad.eTelas.PUESTOS,
                                               Aplicacao.Util.Utilidad.eTelas.SECTOR,
                                               Aplicacao.Util.Utilidad.eTelas.CANALES,
                                               Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR,
                                               Aplicacao.Util.Utilidad.eTelas.PARAMETRO}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_estrutura")
            itemIAC.Image.Url = "imagenes/delegaciones.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item Delegacione
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DELEGACION, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaDelegaciones.aspx"
                itemIAC2.Text = Traduzir("mn_delegaciones")
                itemIAC2.Image.Url = "imagenes/delegaciones.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

            'adicionar item Planta
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DELEGACION, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaPlanta.aspx"
                itemIAC2.Text = Traduzir("mn_plantas")
                itemIAC2.Image.Url = "imagenes/031.png"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item PUESTOS
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PUESTOS, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaPuestos.aspx"
                itemIAC2.Text = Traduzir("mn_puestos")
                itemIAC2.Image.Url = "imagenes/Puesto.gif"
                itemIAC.Items.Add(itemIAC2)

            End If

            ' adicionar item setor
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.SECTOR, _
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaSector.aspx"
                itemIAC2.Text = Traduzir("mn_Sector")
                itemIAC2.Image.Url = "imagenes/sector.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item canal
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CANALES, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaCanales.aspx"
                itemIAC2.Text = Traduzir("mn_item_canal")
                itemIAC2.Image.Url = "imagenes/canal.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            'adicionar item tipo setor
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOSECTOR, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaTipoSector.aspx"
                itemIAC2.Text = Traduzir("mn_tipoSetor")
                itemIAC2.Image.Url = "imagenes/tipo.jpg"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item Parametros
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PARAMETRO, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaParametros.aspx"
                itemIAC2.Text = Traduzir("mn_parametros")
                itemIAC2.Image.Url = "imagenes/Parametro.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

        End If


        ' adicionar item Divisas
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.DIVISAS,
                                               Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO,
                                               Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_item_divisas")
            itemIAC.Image.Url = "imagenes/divisa.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item divisa
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.DIVISAS, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaDivisas.aspx"
                itemIAC2.Text = Traduzir("mn_item_divisas")
                itemIAC2.Image.Url = "imagenes/divisa.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item medio pago
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.MEDIO_DE_PAGO, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaMediosPago.aspx"
                itemIAC2.Text = Traduzir("mn_mediopago")
                itemIAC2.Image.Url = "imagenes/mediopago.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item agrupaciones
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.AGRUPACIONES, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaAgrupaciones.aspx"
                itemIAC2.Text = Traduzir("mn_agrupaciones")
                itemIAC2.Image.Url = "imagenes/agrupaciones.gif"
                itemIAC.Items.Add(itemIAC2)

            End If
        End If

        ' adicionar item IAC
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE,
                                               Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC,
                                               Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_item_iac")
            itemIAC.Image.Url = "imagenes/iac.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item iac
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.INFORMACION_ADICIONAL_CLIENTE, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaIac.aspx"
                itemIAC2.Text = Traduzir("mn_item_iac")
                itemIAC2.Image.Url = "imagenes/iac.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item terminos
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TERMINOS_IAC, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaTerminos.aspx"
                itemIAC2.Text = Traduzir("mn_terminos")
                itemIAC2.Image.Url = "imagenes/terminos.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item valores posibles
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.VALORES_POSIBLES, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaValoresPosibles.aspx"
                itemIAC2.Text = Traduzir("mn_valoresposibles")
                itemIAC2.Image.Url = "imagenes/valores_posibles.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

        End If

        ' adicionar item processo
        If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCESOS, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._BUSCAR) OrElse _
            _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.PRODUCTOS,
                                                Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS,
                                                Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_proceso")
            itemIAC.Image.Url = "imagenes/proceso.gif"
            menuIAC.Items.Add(itemIAC)

            ' adicionar item processo
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCESOS, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._BUSCAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaProcesos.aspx"
                itemIAC2.Text = Traduzir("mn_proceso")
                itemIAC2.Image.Url = "imagenes/proceso.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item produtos
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PRODUCTOS, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaProductos.aspx"
                itemIAC2.Text = Traduzir("mn_item_producto")
                itemIAC2.Image.Url = "imagenes/produtos.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' Buscar Características
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.CARACTERISTICAS, _
                                                 Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaCaracteristicas.aspx"
                itemIAC2.Text = Traduzir("mn_caracteristicas")
                itemIAC2.Image.Url = "imagenes/caracteristicas.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

            ' adicionar item tipos procesado
            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPO_PROCESADO, _
                                                  Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaTiposProcesado.aspx"
                itemIAC2.Text = Traduzir("mn_item_tiposprocesado")
                itemIAC2.Image.Url = "imagenes/modalidrecuento.gif"
                itemIAC.Items.Add(itemIAC2)
            End If

        End If

        ' adicionar item procedencia
        If _ValidacaoAcesso.ValidarAcaoPagina({Aplicacao.Util.Utilidad.eTelas.TIPOPROCEDENCIA,
                                               Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS}, _
                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

            Dim itemIAC As New ASPxMenu.MenuItem()
            itemIAC.Text = Traduzir("mn_Procedencia")
            itemIAC.Image.Url = "imagenes/Procedencia.png"
            menuIAC.Items.Add(itemIAC)

            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.TIPOPROCEDENCIA, _
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaTipoProcedencia.aspx"
                itemIAC2.Text = Traduzir("mn_tipoProcedencia")
                itemIAC2.Image.Url = "imagenes/Procedencia.png"
                itemIAC.Items.Add(itemIAC2)
            End If

            If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.PROCEDENCIAS,
                                              Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

                Dim itemIAC2 As New ASPxMenu.MenuItem
                itemIAC2.NavigateUrl = "~/BusquedaProcedencias.aspx"
                itemIAC2.Text = Traduzir("mn_Procedencia")
                itemIAC2.Image.Url = "imagenes/Procedencia.png"
                itemIAC.Items.Add(itemIAC2)
            End If


        End If

        '' adicionar item integracion
        'If _ValidacaoAcesso.ValidarAcaoPagina(Aplicacao.Util.Utilidad.eTelas.INTEGRACION_BO, _
        '                                      Aplicacao.Util.Utilidad.eAcoesTela._CONSULTAR) Then

        '    Dim itemIAC As New ASPxMenu.MenuItem
        '    itemIAC.NavigateUrl = "~/Default.aspx"
        '    itemIAC.Text = "&nbsp;" & Traduzir("mn_integracion")
        '    itemIAC.Image.Url = "imagenes/integracion.gif"
        '    menuIAC.Items.Add(itemIAC)

        'End If

        ' adiciona um menu 'invisível" para que a tabução entre os campos funcione corretamente
        'menuIAC.Items.Add(New ASPxMenu.MenuItem(String.Empty))

    End Sub

    ''' <summary>
    ''' Traduz os controles da master page.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 23/02/2009 Criado
    ''' </history>
    Private Sub TraduzirControles()
        Page.Title = TituloPagina
        Cabecalho.lbSair.Text = Traduzir("lbl_salir")
    End Sub

#End Region

End Class