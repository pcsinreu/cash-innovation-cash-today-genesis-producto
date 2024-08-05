Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon.Extenciones
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.Report
Imports System.Collections.ObjectModel
Imports Prosegur.Genesis.Comon.Util
Imports System.Reflection

Public Class HistoricoElemento
    Inherits Base

#Region "[PROPRIEDADES]"

    Private _Acciones As ucAcciones
    Public ReadOnly Property Acciones() As ucAcciones
        Get
            If _Acciones Is Nothing Then
                _Acciones = LoadControl("~\Controles\UcAcciones.ascx")
                _Acciones.ID = Me.ID & "_Acciones"
                AddHandler _Acciones.Erro, AddressOf ErroControles
                phAcciones.Controls.Add(_Acciones)
            End If
            Return _Acciones
        End Get
    End Property

    Private _identificadorElemento As String = Nothing
    Public ReadOnly Property IdentificadorElemento() As String
        Get
            If String.IsNullOrEmpty(_identificadorElemento) Then
                _identificadorElemento = Request.QueryString("idElemento")
            End If
            Return _identificadorElemento
        End Get
    End Property

    Private _esGestionBulto As String = Nothing
    Public ReadOnly Property EsGestionBulto() As String
        Get
            If String.IsNullOrEmpty(_esGestionBulto) Then
                _esGestionBulto = Request.QueryString("esGestionBulto")
            End If
            Return _esGestionBulto
        End Get
    End Property

#End Region

#Region "[OVERRIDES]"

    Protected Overrides Sub DefinirParametrosBase()
        MyBase.DefinirParametrosBase()
        MyBase.PaginaAtual = Aplicacao.Util.Utilidad.eTelas.HISTORICO_ELEMENTOS
        MyBase.ValidarAcesso = False
        MyBase.ValidarPemissaoAD = False
    End Sub

    Protected Overrides Sub TraduzirControles()
        Master.Titulo = Traduzir("070_lblTitulo")
        Me.lblFiltros.Text = Traduzir("070_lblFiltro")
        Me.lblResultados.Text = Traduzir("070_lblResultados")
        Me.btnBuscar.Text = Traduzir("btnBuscar")

        Me.grvResultado.Columns(0).HeaderText = Traduzir("070_formulario")
        Me.grvResultado.Columns(1).HeaderText = Traduzir("070_sector_origem")
        Me.grvResultado.Columns(2).HeaderText = Traduzir("070_sector_destino")
        Me.grvResultado.Columns(3).HeaderText = Traduzir("070_data_hora")
        Me.grvResultado.Columns(4).HeaderText = Traduzir("070_documento")
    End Sub

#End Region

#Region "[EVENTOS]"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Quando popup
        Master.HabilitarMenu = False
        Me.dvResultado.Style.Item("display") = "block"

        AjustarAcciones()

        If Not IsPostBack AndAlso Request.QueryString.Count > 0 AndAlso Not String.IsNullOrEmpty(Me.IdentificadorElemento) Then
            Me.CarregarHistoricoElemento()
        End If


    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click

        grvResultado.PageIndex = 0
        grvResultado.DataBind()

    End Sub

    Protected Sub redirecionaDocumento_Click(sender As Object, e As System.EventArgs)
        Dim btn = DirectCast(sender, ImageButton)
        DirectCast(Page, Base).AbrirPopup("~/Pantallas/Documento.aspx", "IdentificadorDocumento=" & btn.CommandArgument + "&Modo=" + Enumeradores.Modo.Consulta.ToString()) ' + "&SectorHijo=" + Parametros(2))
    End Sub

#End Region

#Region "[METODOS]"

    Private Sub CarregarHistoricoElemento()
        Me.grvResultado.DataSource = LogicaNegocio.GenesisSaldos.DocumentoElemento.RecuperarHistorico(Me.EsGestionBulto, Me.IdentificadorElemento)
        Me.grvResultado.DataBind()
    End Sub

#Region "     General     "

    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.MostraMensagemErro(If(TypeOf e.Erro Is Excepcion.NegocioExcepcion, e.Erro.Message, e.Erro.ToString()))
    End Sub

    Private Sub AjustarAcciones()

        AddHandler Acciones.onAccionCancelar, AddressOf Acciones_onAccionCancelar
        Acciones.btnCancelarVisible = True

    End Sub

    Private Sub Acciones_onAccionCancelar()
        Try
            'Quando modal
            If Not String.IsNullOrEmpty(Request.QueryString.Count > 0) Then
                CerrarPopup(String.Empty, EnumeradoresPantalla.AccionPopupCerrado.Cancelar.ToString())
            Else
                If Master.Historico.Count > 1 Then
                    Response.Redireccionar(Master.Historico(Master.Historico.Count - 2).Key)
                Else
                    Response.Redireccionar(Constantes.NOME_PAGINA_MENU)
                End If
            End If
        Catch ex As Prosegur.Genesis.Excepcion.NegocioExcepcion
            MyBase.MostraMensagemErro(ex.Descricao.ToString())
        Catch ex As Exception
            MyBase.MostraMensagemErro(ex.ToString())
        End Try
    End Sub

#End Region

#End Region


End Class
