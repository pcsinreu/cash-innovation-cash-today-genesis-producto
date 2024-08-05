Imports System.Collections.ObjectModel
Imports DevExpress.Web.ASPxGridView
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comunicacion.ProxyWS.IAC
Imports Prosegur.Global.GesEfectivo.IAC.Web.Aplicacao.Util

''' <summary>
''' Code Behind de Busca de Sector
''' </summary>
''' <remarks></remarks>
''' <history>
''' [pgoncalves] 12/03/2013 Criado
''' </history>
Public Class BusquedaSector
    Inherits Base

    Private Property CambioDelegacion As Boolean
        Get
            Return CType(ViewState("CambioDelegacion"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("CambioDelegacion") = value
        End Set
    End Property
    Private Property CambioDelegacionForm As Boolean
        Get
            Return CType(ViewState("CambioDelegacionForm"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("CambioDelegacionForm") = value
        End Set
    End Property
    Private Property ModificacionOrBaja As Boolean
        Get
            Return CType(ViewState("ModificacionOrBaja"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("ModificacionOrBaja") = value
        End Set
    End Property

    Private Property IsLoaded As Boolean
        Get
            Return CType(ViewState("IsLoaded"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("IsLoaded") = value
        End Set
    End Property

#Region "[HelperSector]"
    Public Property Sectores As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
        Get
            Return ucSector.Sectores
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector))
            ucSector.Sectores = value
        End Set
    End Property

    Private WithEvents _ucSector As ucSector
    Public Property ucSector() As ucSector
        Get
            If _ucSector Is Nothing Then
                _ucSector = LoadControl(ResolveUrl("~\Controles\Helpers\ucSector.ascx"))
                _ucSector.ID = Me.ID & "_ucSector"
                AddHandler _ucSector.Erro, AddressOf ErroControles
                phSector.Controls.Add(_ucSector)
            End If
            Return _ucSector
        End Get
        Set(value As ucSector)
            _ucSector = value
        End Set
    End Property

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagem(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub
    Private Sub ConfigurarControle_Sector()

        Me.ucSector.SelecaoMultipla = False
        Me.ucSector.SectorHabilitado = True
        Me.ucSector.SectorObrigatorio = False
        Me.ucSector.SectorTitulo = Traduzir("054_lbl_sector_padre")
        If Sectores IsNot Nothing Then
            Me.ucSector.Sectores = Sectores
        End If
    End Sub
    Private Sub ucSector_OnControleAtualizado() Handles _ucSector.UpdatedControl
        Try
            If ucSector.Sectores IsNot Nothing Then
                Sectores = ucSector.Sectores
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub AtualizaDadosHelperSector(observableCollection As ObservableCollection(Of Comon.Clases.Sector), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucSector)
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

        pUserControl.ucSector.RegistrosSelecionados = dadosCliente
        pUserControl.ucSector.ExibirDados(True)
    End Sub
    Private Sub LimparHelper(observableCollection As ObservableCollection(Of Comon.Clases.Sector), ByRef pUserControl As Prosegur.Global.GesEfectivo.IAC.Web.ucSector)
        Dim objSector As New Prosegur.Genesis.Comon.Clases.Sector
        observableCollection.Clear()
        observableCollection.Add(objSector)
        AtualizaDadosHelperSector(observableCollection, pUserControl)
    End Sub
#End Region

#Region "[HelperSectorFormulario]"
    Public Property SectoresForm As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector)
        Get
            Return ucSectorForm.Sectores
        End Get
        Set(value As ObservableCollection(Of Prosegur.Genesis.Comon.Clases.Sector))
            ucSectorForm.Sectores = value
        End Set
    End Property

    Private WithEvents _ucSectorForm As ucSector
    Public Property ucSectorForm() As ucSector
        Get
            If _ucSectorForm Is Nothing Then
                _ucSectorForm = LoadControl(ResolveUrl("~\Controles\Helpers\ucSector.ascx"))
                _ucSectorForm.ID = Me.ID & "_ucSectorForm"
                AddHandler _ucSectorForm.Erro, AddressOf ErroControles
                phSectorForm.Controls.Add(_ucSectorForm)
            End If
            Return _ucSectorForm
        End Get
        Set(value As ucSector)
            _ucSectorForm = value
        End Set
    End Property

    Private Sub ConfigurarControle_SectorForm()

        Me.ucSectorForm.SelecaoMultipla = False
        Me.ucSectorForm.SectorHabilitado = True
        Me.ucSectorForm.SectorObrigatorio = False
        Me.ucSectorForm.UtilizadoForm = True
        Me.ucSectorForm.SectorTitulo = Traduzir("054_lbl_sector_padre_manutencao")
        If SectoresForm IsNot Nothing Then
            Me.ucSectorForm.Sectores = SectoresForm
        End If

    End Sub
    Private Sub ucSectorForm_OnControleAtualizado() Handles _ucSectorForm.UpdatedControl
        Try
            If ucSectorForm.Sectores IsNot Nothing Then
                SectoresForm = ucSectorForm.Sectores
            End If

        Catch ex As Exception
            MostraMensagemExcecao(ex)
        End Try
    End Sub

#End Region
#Region "[OVERRIDES]"

    ''' <summary>
    ''' Validação dos controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 - Criado
    ''' </history>
    Protected Overrides Sub AdicionarControlesValidacao()

    End Sub

    ''' <summary>
    ''' Adiciona javascript aos controles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub AdicionarScripts()

        Try

            Dim s As String = String.Empty

            'Aciona o botão buscar quando o enter é prescionado.
            txtCodigo.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            txtDescricao.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            chkVigente.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")
            ddlPlanta.Attributes.Add("onkeydown", "EventoEnter('" & btnBuscar.ClientID & "', event);")

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração do tabindex
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub ConfigurarTabIndex()

    End Sub

    ''' <summary>
    ''' Define os parametros iniciais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub DefinirParametrosBase()
        ' define ação da tela
        MyBase.Acao = Aplicacao.Util.Utilidad.eAcao.Inicial
        ' definir o nome da página
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.SECTOR
        ' desativar validação de ação
        MyBase.ValidarAcao = False

    End Sub

    ''' <summary>
    ''' Metodo e chamado quando inicializa a pagina.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub Inicializar()

        Try
            GoogleAnalyticsHelper.TrackAnalytics(Me, "Busqueda Sector")
            ASPxGridView.RegisterBaseScript(Page)

            Master.MostrarCabecalho = True
            Master.HabilitarMenu = True
            Master.HabilitarHistorico = True
            Master.MostrarRodape = True
            Master.MenuRodapeVisivel = True

            If Not IsPostBack Then
                Sectores = Nothing
                SectoresForm = Nothing
                ModificacionOrBaja = False

                pnForm.Visible = False
                btnNovo.Enabled = True
                btnBajaConfirm.Visible = False
                btnImporteMaximo.Visible = False
                btnCancelar.Enabled = False
                btnGrabar.Enabled = False

                PreencherddlTipoSetor()
                PreencherddlPlanta(Nothing)
                PreencherddlDelegacion()


                txtCodigo.Focus()
            End If

            ConfigurarControle_Sector()
            ConfigurarControle_SectorForm()
            'TrataFoco()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Pre render
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub PreRenderizar()

        Try
            btnAjeno.Attributes.Add("style", "margin-left: 15px;")
            btnBajaConfirm.OnClientClick = "ConfirmarExclusao('" & Traduzir(Aplicacao.Util.Utilidad.InfoMsgBajaRegistro) & "','" & btnBaja.ClientID & "');"

            If Not Page.IsPostBack Then
                ControleBotoes()
                CambioDelegacion = False
                CambioDelegacionForm = False
            Else
                'Preenche automaticamente a planta apos informar a delegação.
                If Not ddlDelegacion.SelectedValue.Equals(String.Empty) AndAlso CambioDelegacion Then
                    ddlPlanta.ClearSelection()
                    Dim itemSelecionado As ListItem = ddlPlanta.Items.FindByText(ddlDelegacion.Items(ddlDelegacion.SelectedIndex).Text)
                    If itemSelecionado IsNot Nothing Then
                        itemSelecionado.Selected = True
                    End If
                    CambioDelegacion = False
                End If

                'Recupera os valores para servirem de filtro para o helper de setor
                Session("CodigoPlanta") = ddlPlanta.SelectedValue
                Session("CodigoTipoSector") = ddlTipoSector.SelectedValue

                If Not ddlDelegacion.SelectedValue.Equals(String.Empty) AndAlso Not ddlPlanta.SelectedValue.Equals(String.Empty) Then
                    pnSector.Enabled = True
                Else
                    pnSector.Enabled = False
                    LimparHelper(Sectores, ucSector)
                End If

                'Formulario
                '''''''''''''''''

                If Acao = Utilidad.eAcao.Alta Then
                    If Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) AndAlso CambioDelegacionForm Then
                        ddlPlantaForm.ClearSelection()
                        Dim itemSelecionado As ListItem = ddlPlantaForm.Items.FindByText(ddlDelegacionForm.Items(ddlDelegacionForm.SelectedIndex).Text)
                        If itemSelecionado IsNot Nothing Then
                            itemSelecionado.Selected = True
                        End If
                        CambioDelegacionForm = False
                        ddlPlantaForm_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If
                If Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) AndAlso Not ddlPlantaForm.SelectedValue.Equals(String.Empty) Then
                    pnSectorForm.Enabled = True
                Else
                    pnSectorForm.Enabled = False
                    LimparHelper(SectoresForm, ucSectorForm)
                End If
                '''''''''''''''''
                End If

                Session("CodigoPlantaForm") = ddlPlantaForm.SelectedValue
                Session("CodigoTipoSectorForm") = ddlTipoSectorForm.SelectedValue
                'HabilitarDesabilitarCamposForm()

                If Acao = Utilidad.eAcao.Modificacion Then
                    txtCodigoSector.Enabled = False
                End If

                IsLoaded = True
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Traduz os controles.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Overrides Sub TraduzirControles()

        Master.Titulo = Traduzir("054_lbl_titulo_busqueda")
        GdvResultado.PagerSummary = Traduzir("grid_lbl_pagersummary")
        lblCodigo.Text = Traduzir("054_lbl_Codigo")
        lblDescripcion.Text = Traduzir("054_lbl_Descripcion")
        lblPlanta.Text = Traduzir("054_lbl_planta")
        lblSubTitulosCriteriosBusqueda.Text = Traduzir("gen_lbl_CriterioBusca")
        lblSubTitulosSector.Text = Traduzir("054_lbl_tiulo_Resultados")
        lblTipoSetor.Text = Traduzir("054_lbl_tipo_setor")
        lblVigente.Text = Traduzir("054_lbl_vigente")
        lblDelegacion.Text = Traduzir("019_lbl_delegacion")
        csvPlanta.ErrorMessage = Traduzir("054_msg_erro_CodigoPlantaVazio")
        csvDelegacion.ErrorMessage = Traduzir("054_msg_erroDelegacion")

        'Botoes
        btnBuscar.Text = Traduzir("btnBuscar")
        btnLimpar.Text = Traduzir("btnLimpiar")
        btnBuscar.ToolTip = Traduzir("btnBuscar")
        btnLimpar.ToolTip = Traduzir("btnLimpiar")
        btnCancelar.Text = Traduzir("btnCancelar")
        btnCancelar.ToolTip = Traduzir("btnCancelar")
        btnNovo.Text = Traduzir("btnAlta")
        btnNovo.ToolTip = Traduzir("btnAlta")
        btnGrabar.Text = Traduzir("btnGrabar")
        btnGrabar.ToolTip = Traduzir("btnGrabar")
        btnAjeno.Text = Traduzir("btnCodigoAjeno")
        btnAjeno.ToolTip = Traduzir("btnCodigoAjeno")
        btnBajaConfirm.Text = Traduzir("btnBaja")
        btnBajaConfirm.ToolTip = Traduzir("btnBaja")
        btnImporteMaximo.Text = Traduzir("btnImporteMaximo")
        btnImporteMaximo.ToolTip = Traduzir("btnImporteMaximo")

        'Grid
        GdvResultado.Columns(1).HeaderText = Traduzir("054_lbl_grd_codigo")
        GdvResultado.Columns(2).HeaderText = Traduzir("054_lbl_grd_descripcion")
        GdvResultado.Columns(3).HeaderText = Traduzir("054_lbl_grd_planta")
        GdvResultado.Columns(4).HeaderText = Traduzir("054_lbl_grd_tiposector")
        GdvResultado.Columns(5).HeaderText = Traduzir("054_lbl_grd_sectorpadre")
        GdvResultado.Columns(6).HeaderText = Traduzir("054_lbl_grd_vigente")


        'Formulario
        lblTituloSectores.Text = Traduzir("054_msg_tipo_pantalla_Sector")
        lblPlantaForm.Text = Traduzir("054_ddl_planta_manutencao")
        lblTipoSector.Text = Traduzir("054_ddl_TipoSector")
        'lblSectorPadre.Text = Traduzir("054_lbl_sector_padre_manutencao")
        lblCodigoSector.Text = Traduzir("054_lbl_Codigo_sector_manutencao")
        lblDescriptionSector.Text = Traduzir("054_lbl_descricao_sector_manutencao")
        lblCodigoAjeno.Text = Traduzir("054_lbl_codigo_prosegur_manutencao")
        lblVigenteForm.Text = Traduzir("054_lbl_vigente")
        lblCodigoAjeno.Text = Traduzir("019_lbl_codigoAjeno")
        lblDescricaoCodigo.Text = Traduzir("019_lbl_descricaoAjeno")
        lblDelegacionForm.Text = Traduzir("019_lbl_delegacion")
        csvCodigoExistente.ErrorMessage = Traduzir("054_msg_erro_codigoExistente")
        csvCodigoSector.ErrorMessage = Traduzir("054_msg_codigo_setor_obrigatorio")
        csvDescricaoSetor.ErrorMessage = Traduzir("054_msg_Descricao_obrigatoria")
        csvTipoSector.ErrorMessage = Traduzir("054_msg_TipoSector_obrigatorio")
        csvPlantaForm.ErrorMessage = Traduzir("054_msg_planta_obrigatorio")
        csvDelegacionForm.ErrorMessage = Traduzir("054_msg_erroDelegacion")

    End Sub

#End Region

#Region "[PROPRIEDADES]"

    Private Property DeseaEliminarCodigosAjenos As Boolean
        Get
            If (ViewState("DeseaEliminarCodigosAjenos") Is Nothing) Then
                ViewState("DeseaEliminarCodigosAjenos") = False
            End If
            Return CType(ViewState("DeseaEliminarCodigosAjenos"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("DeseaEliminarCodigosAjenos") = value
        End Set
    End Property

    Public Property FiltroCodigoSetor() As String
        Get
            If ViewState("FiltroCodigoSetor") Is Nothing Then
                Return String.Empty
            End If
            Return ViewState("FiltroCodigoSetor")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoSetor") = value
        End Set
    End Property

    Public Property FiltroDescricao() As String
        Get
            Return ViewState("FiltroDescricao")
        End Get
        Set(value As String)
            ViewState("FiltroDescricao") = value
        End Set
    End Property

    Public Property FiltroCodigoSectorPadre() As String
        Get
            If Sectores IsNot Nothing AndAlso Sectores.Count > 0 Then
                ViewState("FiltroCodigoSectorPadre") = Sectores.FirstOrDefault().Identificador
            Else
                ViewState("FiltroCodigoSectorPadre") = String.Empty
            End If

            Return ViewState("FiltroCodigoSectorPadre")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoSectorPadre") = value
        End Set
    End Property

    Public Property FiltroCodigoPlanta() As String
        Get
            Return ViewState("FiltroCodigoPlanta")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoPlanta") = value
        End Set
    End Property

    Public Property FiltroCodigoTipoSetor() As String
        Get
            Return ViewState("FiltroCodigoTipoSetor")
        End Get
        Set(value As String)
            ViewState("FiltroCodigoTipoSetor") = value
        End Set
    End Property

    Public Property FiltroVigente() As Boolean
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As Boolean)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Public Property FiltroCentroProcesso() As Boolean
        Get
            Return ViewState("FiltroCentroProcesso")
        End Get
        Set(value As Boolean)
            ViewState("FiltroCentroProcesso") = value
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
#End Region

#Region "[MÉTODOS]"

    ''' <summary>
    ''' Busca os registros informados como parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Function getBusquedaSector() As IAC.ContractoServicio.Setor.GetSectores.Respuesta

        Dim objProxy As New Comunicacion.ProxySector
        Dim objPeticion As New IAC.ContractoServicio.Setor.GetSectores.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Setor.GetSectores.Respuesta

        ' pesquisar sempre pelo codigo que foi informado ao presionar o botão buscar
        objPeticion.bolActivo = FiltroVigente
        objPeticion.bolCentroProceso = FiltroCentroProcesso
        objPeticion.codSector = FiltroCodigoSetor
        objPeticion.desSector = FiltroDescricao
        objPeticion.oidPlanta = FiltroCodigoPlanta
        objPeticion.oidSectorPadre = FiltroCodigoSectorPadre
        objPeticion.oidTipoSector = FiltroCodigoTipoSetor
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        Return objProxy.getSectores(objPeticion)

    End Function

    ''' <summary>
    ''' Função responsável por fazer o tratamento do foco.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Created
    ''' </history>
    Private Sub TrataFoco()

        If (Not IsPostBack) Then
            Aplicacao.Util.Utilidad.HookOnFocus(DirectCast(Me.Page, Control))
        Else
            If Request("__LASTFOCUS") IsNot Nothing AndAlso Request("__LASTFOCUS") <> String.Empty Then
                Page.SetFocus(Request("__LASTFOCUS"))
            End If
        End If

    End Sub

    ''' <summary>
    ''' Função responsavel por preencher o grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Public Sub PreencherBusquedaSector()

        Dim objRespuesta As ContractoServicio.Setor.GetSectores.Respuesta

        ' busca os valores posibles
        objRespuesta = getBusquedaSector()

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ' define a ação de busca somente se houve retorno
        If objRespuesta.Setor IsNot Nothing AndAlso objRespuesta.Setor.Count > 0 Then

            pnlSemRegistro.Visible = False

            ' converter objeto para datatable
            Dim objDt As DataTable = GdvResultado.ConvertListToDataTable(objRespuesta.Setor)

            If Acao = Aplicacao.Util.Utilidad.eAcao.Busca Then
                objDt.DefaultView.Sort = " codSector ASC"
            ElseIf Acao = Aplicacao.Util.Utilidad.eAcao.Baja Then

                If GdvResultado.SortCommand.Equals(String.Empty) Then
                    objDt.DefaultView.Sort = " codSector ASC "
                Else
                    objDt.DefaultView.Sort = GdvResultado.SortCommand
                End If

            Else
                objDt.DefaultView.Sort = GdvResultado.SortCommand
            End If

            ' carregar controle
            GdvResultado.CarregaControle(objDt)

        Else
            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            'Informar ao usuário sobre a não existencia de registro
            lblSemRegistro.Text = Traduzir(Aplicacao.Util.Utilidad.InfoMsgSinRegistro)
            pnlSemRegistro.Visible = True

            Acao = Aplicacao.Util.Utilidad.eAcao.NoAction

        End If

    End Sub

    ''' <summary>
    ''' Preenche o dropDown de Tipo Setor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherddlTipoSetor()

        Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
        Dim objPeticion As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        'objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyTipoSetor.GetTiposSectores(objPeticion)

        If objRespuesta.TipoSetor Is Nothing Then
            ddlTipoSector.Items.Clear()
            ddlTipoSector.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            ddlTipoSector.Items.Clear()
            ddlTipoSector.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        ddlTipoSector.AppendDataBoundItems = True
        ddlTipoSector.Items.Clear()
        ddlTipoSector.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
        ddlTipoSector.DataTextField = "desTipoSector"
        ddlTipoSector.DataValueField = "oidTipoSector"
        ddlTipoSector.DataSource = objRespuesta.TipoSetor.OrderBy(Function(b) b.desTipoSector)
        ddlTipoSector.DataBind()

    End Sub

    ''' <summary>
    ''' Preenche o dropdownbox de Planta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherddlPlanta(OidDelegacion As String)
        Try
            Dim objProxy As New Comunicacion.ProxyPlanta
            Dim objPeticion As New ContractoServicio.Planta.GetPlanta.Peticion
            Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta

            objPeticion.oidDelegacion = OidDelegacion
            'objPeticion.BolActivo = True
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            If String.IsNullOrEmpty(OidDelegacion) Then
                ddlPlanta.Items.Clear()
                ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
                Exit Sub
            Else
                objPeticion.oidDelegacion = OidDelegacion
            End If

            objRespuesta = objProxy.GetPlantas(objPeticion)

            If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                Exit Sub
            End If

            If objRespuesta.Planta.Count = 0 Then
                ddlPlanta.Items.Clear()
                ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
                Exit Sub
            Else
                ddlPlanta.AppendDataBoundItems = True
                ddlPlanta.Items.Clear()
                ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
                ddlPlanta.DataTextField = "DesPlanta"
                ddlPlanta.DataValueField = "OidPlanta"
                ddlPlanta.DataSource = objRespuesta.Planta.OrderBy(Function(b) b.DesPlanta)
                ddlPlanta.DataBind()
                Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
                If objRespuesta.Planta.FirstOrDefault().CodDelegacion = usuario.CodigoDelegacion Then
                    Dim planta = objRespuesta.Planta.Find(Function(d) d.CodPlanta = usuario.CodigoPlanta)
                    If planta IsNot Nothing Then
                        SeleccionarPlantaLogada(planta.OidPlanta, ddlPlanta)
                    End If

                End If
                ddlPlanta_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' Preencher a dropdownbox de delegaciones
    Public Sub PreencherddlDelegacion()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objProxy As New ProxyDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objProxy.GetDelegaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Delegacion.Count = 0 Then
            ddlPlanta.Items.Clear()
            ddlPlanta.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If objRespuesta.Delegacion.Count > 0 Then
            ddlDelegacion.AppendDataBoundItems = True
            ddlDelegacion.Items.Clear()
            ddlDelegacion.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            ddlDelegacion.DataTextField = "DesDelegacion"
            ddlDelegacion.DataValueField = "OidDelegacion"
            ddlDelegacion.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)
            ddlDelegacion.DataBind()
            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            Dim delegacion = objRespuesta.Delegacion.Find(Function(d) d.CodDelegacion = usuario.CodigoDelegacion)
            If delegacion IsNot Nothing Then
                Me.SeleccionarDelegacionLogada(delegacion.OidDelegacion, ddlDelegacion)
            End If
        End If

    End Sub

    Public Sub SeleccionarDelegacionLogada(oidDelegacion As String, ByRef ddlDelegacionControl As DropDownList)
        Dim delegacionLogada = Nothing
        delegacionLogada = ddlDelegacionControl.Items.FindByValue(oidDelegacion)
        If delegacionLogada IsNot Nothing Then
            ddlDelegacionControl.SelectedIndex = ddlDelegacionControl.Items.IndexOf(delegacionLogada)
            If ddlDelegacionControl Is ddlDelegacion Then
                PreencherddlPlanta(ddlDelegacionControl.SelectedValue)
            Else
                PreencherddlPlantaForm(ddlDelegacionControl.SelectedValue)
            End If
        End If
    End Sub

    Public Sub SeleccionarPlantaLogada(oidPlanta As String, ByRef ddlPlanta As DropDownList)
        Dim plantaLogada = Nothing
        plantaLogada = ddlPlanta.Items.FindByValue(oidPlanta)
        If plantaLogada IsNot Nothing Then
            ddlPlanta.SelectedIndex = ddlPlanta.Items.IndexOf(plantaLogada)
        End If
    End Sub




    Public Function MontaMensagensErro(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        ValidarCamposObrigatorios = True

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório quando o botão salvar é acionado
            If ValidarCamposObrigatorios Then

                If ddlDelegacion.Visible AndAlso ddlDelegacion.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacion.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacion.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlDelegacion.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacion.IsValid = True
                End If

                If ddlPlanta.Visible AndAlso ddlPlanta.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPlanta.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPlanta.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPlanta.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPlanta.IsValid = True
                End If

            End If

        End If

        Return strErro.ToString()

    End Function

#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Evento ao criar as linhas do grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_EOnClickRowClientScript(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.EOnClickRowClientScript

        Try
            ' adicionar chamada da função que informa se o registro selecionado é viegente ou não.
            GdvResultado.OnClickRowClientScript = "status_registro(" & e.Row.DataItem("BolActivo").ToString.ToLower & ");"
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Esta função faz a conversão da linha selecionada no grid em um dt.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewProcesso_EPreencheDados(sender As Object, e As EventArgs) Handles GdvResultado.EPreencheDados

        Dim objDT As DataTable

        objDT = GdvResultado.ConvertListToDataTable(getBusquedaSector().Setor)

        If GdvResultado.SortCommand.Equals(String.Empty) Then
            objDT.DefaultView.Sort = " codSector ASC "
        Else
            objDT.DefaultView.Sort = GdvResultado.SortCommand
        End If

        GdvResultado.ControleDataSource = objDT

    End Sub

    ''' <summary>
    ''' Configuração de estilo do datagrid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Protected Sub ProsegurGridViewProcesso_EPager_SetCss(sender As Object, e As EventArgs) Handles GdvResultado.EPager_SetCss
        Try

            'Configura o estilo dos controles presentes no pager
            CType(CType(sender, ArrayList)(0), Label).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(0), Label).Font.Bold = False
            CType(CType(sender, ArrayList)(0), Label).Font.Size = 9
            CType(CType(sender, ArrayList)(0), Label).Font.Name = "Verdana"

            CType(CType(sender, ArrayList)(1), TextBox).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(1), TextBox).Font.Bold = False
            CType(CType(sender, ArrayList)(1), TextBox).Font.Size = 9
            CType(CType(sender, ArrayList)(1), TextBox).Font.Name = "Verdana"
            CType(CType(sender, ArrayList)(1), TextBox).Style.Add("text-align", "center")
            CType(CType(sender, ArrayList)(1), TextBox).TabIndex = 32

            CType(CType(sender, ArrayList)(2), Object).ForeColor = Drawing.Color.Black
            CType(CType(sender, ArrayList)(2), Object).Font.Bold = False
            CType(CType(sender, ArrayList)(2), Object).Font.Size = 9
            CType(CType(sender, ArrayList)(2), Object).Font.Name = "Verdana"

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Configuração dos campos vigente e manual do grid, e do numero maximo de linhas.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowDataBound

        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim NumeroMaximoLinha As Integer = Aplicacao.Util.Utilidad.getMaximoCaracteresLinha

                Dim jsScript As String = "SetValorHidden('" & hiddenCodigo.ClientID & "','" & Server.UrlEncode(e.Row.DataItem("oidSector")) & "');"
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).OnClientClick = jsScript
                CType(e.Row.Cells(0).FindControl("imgEditar"), ImageButton).ToolTip = Traduzir("btnModificacion")
                CType(e.Row.Cells(0).FindControl("imgConsultar"), ImageButton).ToolTip = Traduzir("btnConsulta")
                CType(e.Row.Cells(0).FindControl("imgExcluir"), ImageButton).ToolTip = Traduzir("btnBaja")


                If CBool(e.Row.DataItem("bolActivo")) Then
                    CType(e.Row.Cells(7).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/contain01.png"
                Else
                    CType(e.Row.Cells(7).FindControl("imgVigente"), Image).ImageUrl = "~/Imagenes/nocontain01.png"
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Tradução do cabecalho do grid.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Sub ProsegurGridViewProcesso_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GdvResultado.RowCreated

        Try

            If e.Row.RowType = DataControlRowType.Header Then

            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Seta o tamanho das linhas do grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2012 Criado
    ''' </history>
    Private Sub TamanhoLinhas()
        GdvResultado.RowStyle.Height = 20
    End Sub

    ''' <summary>
    ''' Evento do botão limpar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Sub btnLimpar_Click(sender As Object, e As System.EventArgs) Handles btnLimpar.Click

        Try

            ValidarCamposObrigatorios = False

            'Limpa a consulta
            GdvResultado.DataSource = Nothing
            GdvResultado.DataBind()

            txtCodigo.Text = String.Empty
            txtDescricao.Text = String.Empty
            PreencherddlDelegacion()
            PreencherddlPlanta(Nothing)
            PreencherddlTipoSetor()
            ddlDelegacion.SelectedValue = String.Empty
            ddlPlanta.SelectedValue = String.Empty
            ddlTipoSector.SelectedValue = String.Empty

            chkVigente.Checked = True
            pnlSemRegistro.Visible = False

            LimparHelper(Sectores, ucSector)

            ' limpar propriedades            
            FiltroCodigoPlanta = String.Empty
            FiltroCodigoSectorPadre = String.Empty
            FiltroDescricao = String.Empty
            FiltroCentroProcesso = False
            FiltroCodigoSetor = String.Empty
            FiltroCodigoTipoSetor = String.Empty
            FiltroVigente = True
            'Estado Inicial
            Acao = Aplicacao.Util.Utilidad.eAcao.Inicial

            btnCancelar_Click(sender, e)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    ''' <summary>
    ''' Evento do botão buscar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 12/03/2013 Criado
    ''' </history>
    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        Try


            ValidarCamposObrigatorios = True

            Dim MensagemErro As String = MontaMensagensErro()

            If MensagemErro <> String.Empty Then
                MyBase.MostraMensagem(MensagemErro)
                Exit Sub
            End If

            ' setar ação de busca
            Acao = Aplicacao.Util.Utilidad.eAcao.Busca

            ' Gravar oidPlanta
            FiltroCodigoPlanta = ddlPlanta.SelectedValue
            FiltroCentroProcesso = False
            FiltroCodigoSectorPadre = FiltroCodigoSectorPadre
            FiltroCodigoSetor = txtCodigo.Text.ToUpper()
            FiltroCodigoTipoSetor = ddlTipoSector.SelectedValue
            FiltroDescricao = txtDescricao.Text
            FiltroVigente = chkVigente.Checked

            'Retorna os valores posibles de acordo com o filtro acima
            PreencherBusquedaSector()

            pnForm.Visible = False
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            btnImporteMaximo.Visible = False

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnAlertaNo_Click(sender As Object, e As System.EventArgs) Handles btnAlertaNo.Click
        DeseaEliminarCodigosAjenos = False
        BajarSector(sender, e)
    End Sub
    Private Sub btnAlertaSi_Click(sender As Object, e As System.EventArgs) Handles btnAlertaSi.Click
        DeseaEliminarCodigosAjenos = True
        BajarSector(sender, e)
    End Sub

    Private Sub BajarSector(sender As Object, e As System.EventArgs)
        Try
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim strCodigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    strCodigo = GdvResultado.getValorLinhaSelecionada
                Else
                    strCodigo = Server.UrlDecode(hiddenCodigo.Value.ToString())
                End If

                Acao = Aplicacao.Util.Utilidad.eAcao.Baja
                Dim objProxySector As New Comunicacion.ProxySector
                Dim objRespuestaSector As IAC.ContractoServicio.Setor.SetSectores.Respuesta
                Dim objPeticionSector As New IAC.ContractoServicio.Setor.SetSectores.Peticion
                Dim objPeticion As New ContractoServicio.Setor.GetSectoresDetail.Peticion

                objPeticion.OidSector = strCodigo

                If String.IsNullOrEmpty(objPeticion.OidSector) Then
                    MyBase.MostraMensagem(Traduzir("054_msg_selecione_Sector"))
                    Exit Sub
                End If

                Dim objColSetor As IAC.ContractoServicio.Setor.GetSectoresDetail.Respuesta
                objColSetor = objProxySector.getSetorDetail(objPeticion)

                'Associa ao processo os objetos relacionados
                objPeticionSector.bolActivo = False
                objPeticionSector.bolCentroProceso = objColSetor.Sector.bolCentroProceso
                objPeticionSector.codSector = objColSetor.Sector.codSector
                objPeticionSector.desSector = objColSetor.Sector.desSector
                objPeticionSector.oidPlanta = objColSetor.Sector.oidPlanta
                objPeticionSector.oidSectorPadre = objColSetor.Sector.oidSectorPadre
                objPeticionSector.oidTipoSector = objColSetor.Sector.oidTipoSector
                objPeticionSector.bolConteo = objColSetor.Sector.bolConteo
                objPeticionSector.bolTesoro = objColSetor.Sector.bolTesoro
                objPeticionSector.codMigracion = objColSetor.Sector.codMigracion
                objPeticionSector.desUsuarioCreacion = MyBase.LoginUsuario
                objPeticionSector.desUsuarioModificacion = MyBase.LoginUsuario
                objPeticionSector.gmtCreacion = DateTime.Now
                objPeticionSector.gmtModificacion = DateTime.Now
                objPeticionSector.oidSector = objColSetor.Sector.oidSector
                objPeticionSector.BolEliminaCodigosAjenos = DeseaEliminarCodigosAjenos
                'Exclui a petição
                objRespuestaSector = objProxySector.setSectores(objPeticionSector)

                If Master.ControleErro.VerificaErro(objRespuestaSector.CodigoError, objRespuestaSector.NombreServidorBD, objRespuestaSector.MensajeError) Then
                    MyBase.MostraMensagem(Traduzir(Aplicacao.Util.Utilidad.InfoMsgBaja))
                    'Atualiza o GridView
                    If GdvResultado.Rows.Count > 0 Then
                        btnBuscar_Click(Nothing, Nothing)
                    End If
                    btnCancelar_Click(Nothing, Nothing)
                Else
                    If objRespuestaSector.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                        MyBase.MostraMensagem(objRespuestaSector.MensajeError)
                    End If
                End If

            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento do botão deletar.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 16/03/2009 Criado
    ''' </history>
    Private Sub btnBaja_Click(sender As Object, e As System.EventArgs) Handles btnBaja.Click

        Try
            DeseaEliminarCodigosAjenos = False

            Dim accionSI As String = "ExecutarClick(" & Chr(34) & btnAlertaSi.ClientID & Chr(34) & ");"
            Dim accionNO As String = "ExecutarClick(" & Chr(34) & btnAlertaNo.ClientID & Chr(34) & ");"
            Dim mensaje As String = String.Format(MyBase.RecuperarValorDic("msgEliminarCodAjenosAsociados"), MyBase.RecuperarValorDic("lbl_sector"))

            MyBase.ExibirMensagemNaoSim(mensaje, accionSI, accionNO)
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Protected Sub ddlPlanta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPlanta.SelectedIndexChanged
        Session("CodigoPlanta") = ddlPlanta.SelectedValue
        LimparHelper(Sectores, ucSector)
    End Sub

    Protected Sub ddlTipoSector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoSector.SelectedIndexChanged
        Session("CodigoTipoSector") = ddlTipoSector.SelectedValue
        LimparHelper(Sectores, ucSector)
    End Sub

    Protected Sub ddlDelegacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacion.SelectedIndexChanged
        If Not ddlDelegacion.SelectedValue.Equals(String.Empty) Then
            PreencherddlPlanta(ddlDelegacion.SelectedValue)
            CambioDelegacion = True
        Else
            PreencherddlPlanta(Nothing)
        End If
    End Sub

#End Region

#Region "[CONTROLE DE ESTADO]"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ControleBotoes()

        Select Case MyBase.Acao
            Case Aplicacao.Util.Utilidad.eAcao.Alta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Baja
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Consulta
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.Modificacion
                'Controles
            Case Aplicacao.Util.Utilidad.eAcao.NoAction

            Case Aplicacao.Util.Utilidad.eAcao.Inicial

            Case Aplicacao.Util.Utilidad.eAcao.Busca

        End Select

    End Sub


#End Region

#Region "Metodos Fomulario"
    Public Sub PreencherddlTipoSetorForm()

        Dim objProxyTipoSetor As New Comunicacion.ProxyTipoSetor
        Dim objPeticion As New ContractoServicio.TipoSetor.GetTiposSectores.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.Respuesta

        objPeticion.bolActivo = True
        objPeticion.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyTipoSetor.GetTiposSectores(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        ViewState("TipoSetor") = objRespuesta.TipoSetor

        ddlTipoSectorForm.AppendDataBoundItems = True
        ddlTipoSectorForm.Items.Clear()
        ddlTipoSectorForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
        ddlTipoSectorForm.DataTextField = "desTipoSector"
        ddlTipoSectorForm.DataValueField = "oidTipoSector"
        ddlTipoSectorForm.DataSource = objRespuesta.TipoSetor.OrderBy(Function(b) b.desTipoSector)
        ddlTipoSectorForm.DataBind()

    End Sub

    ''' <summary>
    ''' Preenche o dropdownbox de Planta
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PreencherddlPlantaForm(oidDelegacion As String)

        Dim objProxyPlanta As New Comunicacion.ProxyPlanta
        Dim objPeticionPlanta As New ContractoServicio.Planta.GetPlanta.Peticion
        Dim objRespuesta As New IAC.ContractoServicio.Planta.GetPlanta.Respuesta

        If String.IsNullOrEmpty(oidDelegacion) Then
            ddlPlantaForm.Items.Clear()
            ddlPlantaForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        Else
            objPeticionPlanta.oidDelegacion = oidDelegacion
        End If

        objPeticionPlanta.BolActivo = True
        objPeticionPlanta.ParametrosPaginacion.RealizarPaginacion = False

        objRespuesta = objProxyPlanta.GetPlantas(objPeticionPlanta)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
            Exit Sub
        End If

        If objRespuesta.Planta.Count = 0 Then
            ddlPlantaForm.Items.Clear()
            ddlPlantaForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        Else
            ddlPlantaForm.AppendDataBoundItems = True
            ddlPlantaForm.Items.Clear()
            ddlPlantaForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            ddlPlantaForm.DataTextField = "DesPlanta"
            ddlPlantaForm.DataValueField = "OidPlanta"
            ddlPlantaForm.DataSource = objRespuesta.Planta.OrderBy(Function(b) b.DesPlanta)
            ddlPlantaForm.DataBind()
            ddlPlantaForm.Enabled = True
            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            If Acao = Utilidad.eAcao.Alta Then
                If objRespuesta.Planta.FirstOrDefault().CodDelegacion = usuario.CodigoDelegacion Then
                    Dim planta = objRespuesta.Planta.Find(Function(d) d.CodPlanta = usuario.CodigoPlanta)
                    If planta IsNot Nothing Then
                        SeleccionarPlantaLogada(planta.OidPlanta, ddlPlantaForm)
                    End If
                End If
            End If
            ddlPlantaForm_SelectedIndexChanged(Nothing, Nothing)
        End If

    End Sub

    ''' Preencher a dropdownbox de delegaciones
    Public Sub PreencherddlDelegacionForm()

        Dim objPeticion As New ContractoServicio.Delegacion.GetDelegacion.Peticion
        Dim objRespuesta As New ContractoServicio.Delegacion.GetDelegacion.Respuesta
        Dim objProxy As New ProxyDelegacion

        objPeticion.ParametrosPaginacion.RealizarPaginacion = False
        objPeticion.BolVigente = True

        objRespuesta = objProxy.GetDelegaciones(objPeticion)

        If Not Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) _
           OrElse objRespuesta.Delegacion.Count = 0 Then
            ddlPlantaForm.Items.Clear()
            ddlPlantaForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            Exit Sub
        End If

        If objRespuesta.Delegacion.Count > 0 Then
            ddlDelegacionForm.AppendDataBoundItems = True
            ddlDelegacionForm.Items.Clear()
            ddlDelegacionForm.Items.Add(New ListItem(Traduzir("054_ddl_selecione"), String.Empty))
            ddlDelegacionForm.DataTextField = "DesDelegacion"
            ddlDelegacionForm.DataValueField = "OidDelegacion"
            ddlDelegacionForm.DataSource = objRespuesta.Delegacion.OrderBy(Function(b) b.DesDelegacion)
            ddlDelegacionForm.DataBind()
            Dim usuario = Genesis.Web.Login.Parametros.Permisos.Usuario
            Dim delegacion = objRespuesta.Delegacion.Find(Function(d) d.CodDelegacion = usuario.CodigoDelegacion)
            If delegacion IsNot Nothing Then
                Me.SeleccionarDelegacionLogada(delegacion.OidDelegacion, ddlDelegacionForm)
            End If
        End If

    End Sub
    Private Sub PreencherDdls()
        PreencherddlPlantaForm(Nothing)
        PreencherddlDelegacionForm()
        PreencherddlTipoSetorForm()
    End Sub
    Private Sub LimparCampos()
        PreencherDdls()
        txtCodigoSector.Text = String.Empty
        txtDescricaoSetor.Text = String.Empty
        txtCodigoAjeno.Text = String.Empty
        txtDescricaoAjeno.Text = String.Empty
        CodigosAjenosPeticion = Nothing
        ImportesMaximoPeticion = Nothing
        OidSector = String.Empty
        Sector = Nothing
        Session.Remove("ImporteMaximoEditar")

    End Sub

    Public Sub ExecutarGrabar()
        Try

            Dim objProxySetor As New Comunicacion.ProxySector
            Dim objRespuestaSetor As IAC.ContractoServicio.Setor.SetSectores.Respuesta

            ValidarCamposObrigatoriosForm = True
            Dim strErro As String = MontaMensagensErroForm(True)

            If strErro.Length > 0 Then
                MyBase.MostraMensagem(strErro)
                Exit Sub
            End If

            Dim objPeticion As New IAC.ContractoServicio.Setor.SetSectores.Peticion
            'Recebe os valores do filtro
            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.bolActivo = True
            Else
                objPeticion.bolActivo = chkVigenteForm.Checked
            End If

            ' atualizar propriedade vigente
            EsVigente = chkVigenteForm.Checked

            objPeticion.bolCentroProceso = False
            objPeticion.codSector = txtCodigoSector.Text
            objPeticion.desSector = txtDescricaoSetor.Text

            If Acao = Aplicacao.Util.Utilidad.eAcao.Alta Then
                objPeticion.gmtCreacion = DateTime.Now
                objPeticion.desUsuarioCreacion = MyBase.LoginUsuario
            Else
                objPeticion.gmtCreacion = Sector.gmtCreacion
                objPeticion.desUsuarioCreacion = Sector.desUsuarioCreacion
            End If

            objPeticion.codMigracion = If(Sector IsNot Nothing AndAlso Not String.IsNullOrEmpty(Sector.codMigracion), Sector.codMigracion, "")
            objPeticion.gmtModificacion = DateTime.Now
            objPeticion.desUsuarioModificacion = MyBase.LoginUsuario
            objPeticion.oidPlanta = ddlPlantaForm.SelectedValue

            If SectoresForm IsNot Nothing AndAlso SectoresForm.Count > 0 Then
                objPeticion.oidSectorPadre = SectoresForm.FirstOrDefault().Identificador
            Else
                objPeticion.oidSectorPadre = String.Empty
            End If

            objPeticion.oidTipoSector = ddlTipoSectorForm.SelectedValue
            objPeticion.oidSector = OidSector

            objPeticion.CodigosAjenos = CodigosAjenosPeticion

            objPeticion.ImporteMaximo = ImportesMaximoPeticion

            objRespuestaSetor = objProxySetor.setSectores(objPeticion)

            Dim url As String = "BusquedaSector.aspx"

            Session.Remove("objRespuestaGEPR_TSECTOR")
            Session.Remove("ImporteMaximoEditar")
            If Master.ControleErro.VerificaErro(objRespuestaSetor.CodigoError, objRespuestaSetor.NombreServidorBD, objRespuestaSetor.MensajeError) Then

                MyBase.MostraMensagem(Traduzir("009_msg_grabado_suceso"))
                If GdvResultado.Rows.Count > 0 Then
                    btnBuscar_Click(Nothing, Nothing)
                End If
                btnCancelar_Click(Nothing, Nothing)

            Else
                If objRespuestaSetor.CodigoError = Excepcion.Constantes.CONST_CODIGO_ERROR_NEGOCIO_DEFAULT Then
                    MyBase.MostraMensagem(objRespuestaSetor.MensajeError)
                End If
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub
    Public Function MontaMensagensErroForm(Optional SetarFocoControle As Boolean = False) As String

        Dim strErro As New Text.StringBuilder(String.Empty)
        Dim focoSetado As Boolean = False

        If Page.IsPostBack Then

            'Verifica se o campo é obrigatório
            'quando o botão salvar é acionado
            If ValidarCamposObrigatoriosForm Then

                'Verifica se a delegação foi selecionada
                If ddlDelegacionForm.Visible AndAlso ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvDelegacionForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDelegacionForm.IsValid = False

                    'Setar o foco no primeiro campo que deu erro
                    If focoSetado AndAlso Not focoSetado Then
                        ddlDelegacionForm.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDelegacionForm.IsValid = True
                End If

                'Verifica se a planta foi enviada
                If ddlPlantaForm.Visible AndAlso ddlPlantaForm.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvPlantaForm.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvPlantaForm.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlPlantaForm.Focus()
                        focoSetado = True
                    End If
                Else
                    csvPlantaForm.IsValid = True
                End If

                'Verifica se o código da delegação foi enviado
                If txtCodigoSector.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvCodigoSector.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvCodigoSector.IsValid = False

                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtCodigoSector.Focus()
                        focoSetado = True
                    End If
                Else
                    csvCodigoSector.IsValid = True
                End If

                'Verifica se a descrição foi enviada
                If txtDescricaoSetor.Text.Trim.Equals(String.Empty) Then

                    strErro.Append(csvDescricaoSetor.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvDescricaoSetor.IsValid = False
                    'Seta o foco no primeiro controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        txtDescricaoSetor.Focus()
                        focoSetado = True
                    End If
                Else
                    csvDescricaoSetor.IsValid = True
                End If

                'Verifica se o tipo setor foi enviado
                If ddlTipoSectorForm.Visible AndAlso ddlTipoSectorForm.SelectedValue.Equals(String.Empty) Then

                    strErro.Append(csvTipoSector.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                    csvTipoSector.IsValid = False

                    'Setar o foco no primeiro que controle que deu erro
                    If SetarFocoControle AndAlso Not focoSetado Then
                        ddlTipoSectorForm.Focus()
                        focoSetado = True
                    End If
                Else
                    csvTipoSector.IsValid = True
                End If

            End If

            'Verifica se o código existe
            If Not String.IsNullOrEmpty(txtCodigoSector.Text) AndAlso Not ddlPlantaForm.SelectedValue.Equals(String.Empty) AndAlso ExisteCodigoSector(txtCodigoSector.Text, ddlPlantaForm.SelectedValue) Then

                strErro.Append(csvCodigoExistente.ErrorMessage & Aplicacao.Util.Utilidad.LineBreak)
                csvCodigoExistente.IsValid = False

                'Seta o foco no primeiro controle que deu erro
                If SetarFocoControle AndAlso Not focoSetado Then
                    txtCodigoSector.Focus()
                    focoSetado = True
                End If
            Else
                csvCodigoExistente.IsValid = True
            End If
        End If

        Return strErro.ToString

    End Function
    Private Sub ConsomeImporteMaximo()

        If Session("objRespuestaImporte") IsNot Nothing Then

            If Sector Is Nothing Then
                Sector = New ContractoServicio.Setor.GetSectoresDetail.Sector
            End If

            Sector.ImportesMaximos = Session("objRespuestaImporte")
            Session.Remove("objRespuestaImporte")

            If Sector.ImportesMaximos IsNot Nothing Then
                ImportesMaximoPeticion = Sector.ImportesMaximos
            Else
                ImportesMaximoPeticion = New ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
            End If
        End If

    End Sub
    Private Function ExisteCodigoSector(codigo As String, planta As String) As Boolean

        Try

            If Acao = Utilidad.eAcao.Modificacion Then
                Return False
            End If

            Dim objProxySetor As New Comunicacion.ProxySector
            Dim objPeticion As New IAC.ContractoServicio.Setor.GetSectores.Peticion
            Dim objRespuesta As IAC.ContractoServicio.Setor.GetSectores.Respuesta

            'Verifica se o código do setor existe no BD
            objPeticion.codSector = codigo
            objPeticion.oidPlanta = planta
            objPeticion.bolActivo = Nothing
            objPeticion.ParametrosPaginacion.RealizarPaginacion = False

            objRespuesta = objProxySetor.getSectores(objPeticion)

            If Master.ControleErro.VerificaErro(objRespuesta.CodigoError, objRespuesta.NombreServidorBD, objRespuesta.MensajeError) Then
                If objRespuesta.Setor.Count > 0 Then
                    Return True
                End If
            Else
                Return False
                Master.ControleErro.ShowError(objRespuesta.MensajeError)
            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Function
    Public Sub CarregaDados(oidSetor As String)

        Dim objSector As IAC.ContractoServicio.Setor.GetSectoresDetail.Respuesta
        Dim objPeticion As New IAC.ContractoServicio.Setor.GetSectoresDetail.Peticion
        Dim objProxySector As New Comunicacion.ProxySector
        Dim itemSelecionadoPlanta As ListItem
        Dim itemSelecionadoTpSector As ListItem
        Dim itemSelecionadoDelegacion As ListItem

        objPeticion.OidSector = oidSetor

        objSector = objProxySector.getSetorDetail(objPeticion)

        ' tratar retorno
        If Not Master.ControleErro.VerificaErro(objSector.CodigoError, objSector.NombreServidorBD, objSector.MensajeError) Then
            MyBase.MostraMensagem(objSector.MensajeError)
            Return
        End If

        Sector = objSector.Sector

        If objSector.Sector IsNot Nothing Then

            Dim iCodigoAjeno = (From iten In objSector.Sector.CodigosAjenos
                                Where iten.BolDefecto = True).FirstOrDefault()

            If iCodigoAjeno IsNot Nothing Then
                txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                txtCodigoAjeno.ToolTip = iCodigoAjeno.CodAjeno
                txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
                txtDescricaoAjeno.ToolTip = iCodigoAjeno.DesAjeno
            End If

            'Preenche os controles do formulario
            txtCodigoSector.Text = objSector.Sector.codSector
            txtCodigoSector.ToolTip = If(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objSector.Sector.codSector, String.Empty)

            txtDescricaoSetor.Text = objSector.Sector.desSector
            txtDescricaoSetor.ToolTip = If(Acao = Aplicacao.Util.Utilidad.eAcao.Consulta, objSector.Sector.desSector, String.Empty)



            chkVigenteForm.Checked = objSector.Sector.bolActivo
            If objSector.Sector.bolActivo Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If

            EsVigente = objSector.Sector.bolActivo
            OidSector = objSector.Sector.oidSector

            If Not ddlTipoSectorForm.Items.Contains(New ListItem(objSector.Sector.desTipoSector, objSector.Sector.oidTipoSector)) Then

                Dim lstTipoSetor As IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion = ViewState("TipoSetor")
                Dim objAux As New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetor()

                objAux.oidTipoSector = objSector.Sector.oidTipoSector
                objAux.desTipoSector = objSector.Sector.desTipoSector

                lstTipoSetor = New IAC.ContractoServicio.TipoSetor.GetTiposSectores.TipoSetorColeccion
                lstTipoSetor.Add(objAux)

                ddlTipoSectorForm.DataSource = lstTipoSetor
                ddlTipoSectorForm.DataBind()

                ddlTipoSectorForm.SelectedValue = objSector.Sector.oidTipoSector

            End If

            If Not ddlDelegacionForm.Items.Contains(New ListItem(objSector.Sector.DesDelegacion, objSector.Sector.OidDelegacion)) Then

                Dim lstDelegacion As IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion = ViewState("Delegacion")
                Dim objAux As New IAC.ContractoServicio.Delegacion.GetDelegacion.Delegacion()

                objAux.OidDelegacion = objSector.Sector.OidDelegacion
                objAux.DesDelegacion = objSector.Sector.DesDelegacion

                lstDelegacion = New IAC.ContractoServicio.Delegacion.GetDelegacion.DelegacionColeccion
                lstDelegacion.Add(objAux)

                ddlDelegacionForm.DataSource = lstDelegacion
                ddlDelegacionForm.DataBind()

                ddlDelegacionForm.SelectedValue = objSector.Sector.oidTipoSector

            End If

            ddlDelegacionForm.ClearSelection()
            itemSelecionadoDelegacion = ddlDelegacionForm.Items.FindByValue(objSector.Sector.OidDelegacion)
            If itemSelecionadoDelegacion IsNot Nothing Then
                itemSelecionadoDelegacion.Selected = True
            End If
            CambioDelegacionForm = True

            PreencherddlPlantaForm(itemSelecionadoDelegacion.Value)

            'Seleciona o valor
            ddlPlantaForm.ClearSelection()
            itemSelecionadoPlanta = ddlPlantaForm.Items.FindByValue(objSector.Sector.oidPlanta)
            If itemSelecionadoPlanta IsNot Nothing Then
                itemSelecionadoPlanta.Selected = True
                ddlPlantaForm.SelectedIndex = ddlPlantaForm.Items.IndexOf(itemSelecionadoPlanta)
            End If

            ddlTipoSectorForm.ClearSelection()
            itemSelecionadoTpSector = ddlTipoSectorForm.Items.FindByValue(objSector.Sector.oidTipoSector)
            If itemSelecionadoTpSector IsNot Nothing Then
                itemSelecionadoTpSector.Selected = True
            End If

            If Not String.IsNullOrEmpty(objSector.Sector.desSectorPadre) _
                 AndAlso Not String.IsNullOrEmpty(objSector.Sector.codSectorPadre) Then

                Dim oSector As New Prosegur.Genesis.Comon.Clases.Sector
                oSector.Identificador = objSector.Sector.oidSectorPadre
                oSector.Codigo = objSector.Sector.codSectorPadre
                oSector.Descripcion = objSector.Sector.desSectorPadre
                SectoresForm.Clear()
                SectoresForm.Add(oSector)
                AtualizaDadosHelperSector(SectoresForm, ucSectorForm)
            End If


            'ddlTipoSectorForm.SelectedValue = objSector.Sector.oidTipoSector
            'ddlDelegacionForm.SelectedValue = objSector.Sector.OidDelegacion
            Session("ImporteMaximoEditar") = objSector.Sector.ImportesMaximos
        Else
            Response.Redirect("~/BusquedaSector.aspx", False)
        End If
    End Sub

#End Region
#Region "Eventos Formulario"

    Protected Sub ddlPlantaForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPlantaForm.SelectedIndexChanged
        'Variavel para verificar se o evento foi disparado no momento da seleção da planta, somente se for, é realizada a limpeza do usercontrol
        If IsLoaded Then
            LimparHelper(Sectores, ucSectorForm)
        End If

        If Acao = Aplicacao.Util.Utilidad.eAcao.Modificacion OrElse Aplicacao.Util.Utilidad.eAcao.Alta Then
            txtCodigoSector.Enabled = Not Acao = Utilidad.eAcao.Modificacion
            txtDescricaoSetor.Enabled = True
            ddlTipoSectorForm.Enabled = True
        End If
    End Sub

    Protected Sub ddlTipoSectorForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoSectorForm.SelectedIndexChanged

        If Not ddlTipoSectorForm.SelectedValue.Equals(String.Empty) Then
            Session("CodigoTipoSector") = ddlTipoSectorForm.SelectedValue
        End If

    End Sub

    Protected Sub ddlDelegacionForm_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDelegacionForm.SelectedIndexChanged

        If Not ddlDelegacionForm.SelectedValue.Equals(String.Empty) Then
            PreencherddlPlantaForm(ddlDelegacionForm.SelectedValue)
            CambioDelegacionForm = True
        Else
            PreencherddlPlantaForm(Nothing)
        End If

    End Sub
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        Try
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            'btnImporteMaximo.Visible = True
            btnCancelar.Enabled = True
            btnGrabar.Enabled = True
            pnForm.Enabled = True
            pnForm.Visible = True
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            txtDescricaoSetor.Enabled = False
            txtCodigoSector.Enabled = False
            Acao = Utilidad.eAcao.Alta
            ddlDelegacionForm.Focus()
            ddlPlantaForm.Enabled = False
            ddlTipoSectorForm.Enabled = False
            LimparCampos()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        ExecutarGrabar()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            LimparCampos()
            btnNovo.Enabled = True
            btnBajaConfirm.Visible = False
            btnImporteMaximo.Visible = False
            btnCancelar.Enabled = False
            btnGrabar.Enabled = False
            pnForm.Enabled = False
            pnForm.Visible = False
            chkVigenteForm.Checked = True
            chkVigenteForm.Enabled = False
            chkVigenteForm.Visible = False
            lblVigenteForm.Visible = False
            Acao = Utilidad.eAcao.Inicial
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnAjeno_Click(sender As Object, e As EventArgs) Handles btnAjeno.Click
        Try
            Dim url As String = String.Empty
            Dim tablaGenesis As New MantenimientoCodigosAjenos.CodigoAjenoSimples

            tablaGenesis.CodTablaGenesis = txtCodigoSector.Text
            tablaGenesis.DesTablaGenesis = txtDescricaoSetor.Text
            tablaGenesis.OidTablaGenesis = OidSector
            If Sector IsNot Nothing AndAlso Sector.CodigosAjenos IsNot Nothing Then
                tablaGenesis.CodigosAjenos = Sector.CodigosAjenos
            End If

            Session("objGEPR_TSECTOR") = tablaGenesis

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&Entidade=GEPR_TSECTOR"
            Else
                url = "MantenimientoCodigosAjenos.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&Entidade=GEPR_TSECTOR"
            End If

            Master.ExibirModal(url, Traduzir("034_titulo_codigo_ajeno"), 400, 800, False, True, btnConsomeCodigoAjeno.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try

    End Sub

    Private Sub btnConsomeCodigoAjeno_Click(sender As Object, e As EventArgs) Handles btnConsomeCodigoAjeno.Click
        Try
            If Session("objRespuestaGEPR_TSECTOR") IsNot Nothing Then

                If Sector Is Nothing Then
                    Sector = New ContractoServicio.Setor.GetSectoresDetail.Sector
                End If
                Sector.CodigosAjenos = Session("objRespuestaGEPR_TSECTOR")
                Session.Remove("objRespuestaGEPR_TSECTOR")

                Dim iCodigoAjeno = (From item In Sector.CodigosAjenos
                                    Where item.BolDefecto = True).SingleOrDefault()

                If iCodigoAjeno IsNot Nothing Then
                    txtCodigoAjeno.Text = iCodigoAjeno.CodAjeno
                    txtDescricaoAjeno.Text = iCodigoAjeno.DesAjeno
                End If

                If Sector.CodigosAjenos IsNot Nothing Then
                    CodigosAjenosPeticion = Sector.CodigosAjenos
                Else
                    CodigosAjenosPeticion = New ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
                End If
            End If
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnImporteMaximo.Click
        Try
            Dim url As String = String.Empty
            Dim oidImporteMaximo As String = ""

            If Sector IsNot Nothing AndAlso Sector.ImportesMaximos IsNot Nothing AndAlso Sector.ImportesMaximos.Count > 0 Then
                oidImporteMaximo = Sector.ImportesMaximos.First.OidImporteMaximo

                Dim importsSession = Session("ImporteMaximoEditar")

                If importsSession Is Nothing OrElse importsSession.Count <> Sector.ImportesMaximos.Count Then
                    Session("ImporteMaximoEditar") = Sector.ImportesMaximos
                End If
            End If

            If (Aplicacao.Util.Utilidad.eAcao.Consulta = MyBase.Acao OrElse Aplicacao.Util.Utilidad.eAcao.Baja = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Consulta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text & "&oidimporte=" & oidImporteMaximo
            ElseIf (Aplicacao.Util.Utilidad.eAcao.Modificacion = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text & "&oidimporte=" & oidImporteMaximo
            ElseIf (Aplicacao.Util.Utilidad.eAcao.Alta = MyBase.Acao) Then
                url = "MantenimientoImporteMaximo.aspx?acao=" & Aplicacao.Util.Utilidad.eAcao.Alta & "&codimporte=" & txtCodigoSector.Text & "&descImporte=" & txtDescricaoSetor.Text
            End If
            'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "script_popup_importeMaximo", "AbrirPopupModal('" & url & "', 550, 900,'btnImporteMaximo'); ", True)
            Master.ExibirModal(url, Traduzir("043_lbl_titulo"), 550, 900, False, True, btnConsomeImporteMaximo.ClientID)

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Private Sub btnConsomeImporteMaximo_Click(sender As Object, e As EventArgs) Handles btnConsomeImporteMaximo.Click
        Try
            ConsomeImporteMaximo()
        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub HabilitarDesabilitarCamposForm()

        If Acao = Utilidad.eAcao.Baja OrElse Acao = Utilidad.eAcao.Consulta Then
            ddlDelegacionForm.Enabled = False
            ddlPlantaForm.Enabled = False
            ddlTipoSectorForm.Enabled = False
            txtDescricaoSetor.Enabled = False
            txtCodigoSector.Enabled = False
            pnSectorForm.Enabled = False
        ElseIf Acao = Utilidad.eAcao.Alta Then
            txtCodigoSector.Enabled = False
            ddlDelegacionForm.Enabled = True
            ddlPlantaForm.Enabled = False
            ddlTipoSectorForm.Enabled = False
            txtDescricaoSetor.Enabled = False
            pnSectorForm.Enabled = False
        ElseIf Acao = Utilidad.eAcao.Modificacion Then
            ddlDelegacionForm.Enabled = True
            ddlPlantaForm.Enabled = True
            ddlTipoSectorForm.Enabled = True
            txtDescricaoSetor.Enabled = True
            pnSectorForm.Enabled = True
        End If

    End Sub
    Protected Sub imgEditar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            IsLoaded = False

            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Modificacion

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                'btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = True
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoSector.Enabled = False
                txtDescricaoSetor.Focus()

            End If

            If chkVigenteForm.Checked AndAlso EsVigente Then
                chkVigenteForm.Enabled = False
            Else
                chkVigenteForm.Enabled = True
            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgConsultar_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            IsLoaded = False
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Consulta

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = False
                'btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoSector.Enabled = False
                txtDescricaoSetor.Focus()


            End If

        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub

    Protected Sub imgExcluir_OnClick(sender As Object, e As ImageClickEventArgs)
        Try
            IsLoaded = False
            If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) OrElse Not String.IsNullOrWhiteSpace(hiddenCodigo.Value) Then
                Dim codigo As String = String.Empty
                If Not String.IsNullOrEmpty(GdvResultado.getValorLinhaSelecionada) Then
                    codigo = GdvResultado.getValorLinhaSelecionada
                Else
                    codigo = hiddenCodigo.Value.ToString()
                End If

                LimparCampos()
                Acao = Utilidad.eAcao.Baja

                CarregaDados(Server.UrlDecode(codigo))
                btnBajaConfirm.Visible = True
                'btnImporteMaximo.Visible = True
                btnNovo.Enabled = True
                btnCancelar.Enabled = True
                btnGrabar.Enabled = False
                chkVigenteForm.Visible = True
                pnForm.Enabled = True
                pnForm.Visible = True
                lblVigenteForm.Visible = True
                txtCodigoSector.Enabled = False
                txtDescricaoSetor.Focus()

            End If


        Catch ex As Exception
            MyBase.MostraMensagemExcecao(ex)
        End Try
    End Sub
#End Region

#Region "[PROPRIEDADES Formulario]"
    Private Property ImportesMaximoPeticion() As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase
        Get
            Return DirectCast(ViewState("ImportesMaximoPeticion"), ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
        End Get

        Set(value As ContractoServicio.ImporteMaximo.ImporteMaximoColeccionBase)
            ViewState("ImportesMaximoPeticion") = value
        End Set

    End Property
    Private Property ValidarCamposObrigatoriosForm() As Boolean
        Get
            Return ViewState("ValidarCamposObrigatoriosForm")
        End Get
        Set(value As Boolean)
            ViewState("ValidarCamposObrigatoriosForm") = value
        End Set
    End Property

    Private Property CodigoExistente() As Boolean
        Get
            Return ViewState("CodigoExistente")
        End Get
        Set(value As Boolean)
            ViewState("CodigoExistente") = value
        End Set
    End Property

    Private Property EsVigente() As Boolean
        Get
            Return ViewState("EsVigente")
        End Get
        Set(value As Boolean)
            ViewState("EsVigente") = value
        End Set
    End Property

    Private Property OidSector As String
        Get
            Return ViewState("OidSector")
        End Get
        Set(value As String)
            ViewState("OidSector") = value
        End Set
    End Property

    Private Property Sector As ContractoServicio.Setor.GetSectoresDetail.Sector
        Get
            Return DirectCast(ViewState("Sector"), ContractoServicio.Setor.GetSectoresDetail.Sector)
        End Get
        Set(value As ContractoServicio.Setor.GetSectoresDetail.Sector)
            ViewState("Sector") = value
        End Set
    End Property

    ''' <summary>
    ''' Armazena em viewstate os códigos ajenos na petição
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 07/07/2013 - Criado
    ''' </history>
    Private Property CodigosAjenosPeticion() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        Get
            Return DirectCast(ViewState("CodigosAjenosPeticion"), ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        End Get

        Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
            ViewState("CodigosAjenosPeticion") = value
        End Set

    End Property

#End Region

End Class