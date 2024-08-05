Imports Prosegur.Framework.Dicionario
Imports System.IO
Imports System.Drawing
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio.Canal
Imports Prosegur.Global.GesEfectivo.IAC
Imports Prosegur.Genesis

''' <summary>
''' Controle de Usuário Canal.
''' </summary>
''' <history>
''' [Thiago Dias] 01/08/2013 - Criado.
''' </history>
Public Class UcDatosCanal
    Inherits UcBase

#Region "Propriedades e Campos"

    Property FiltroVigente() As String
        Get
            Return ViewState("FiltroVigente")
        End Get
        Set(value As String)
            ViewState("FiltroVigente") = value
        End Set
    End Property

    Protected Property ObjProxy As Comunicacion.ProxyCanal

    Protected Property PeticaoCanal As IAC.ContractoServicio.Canal.GetCanales.Peticion

    Protected Property PeticaoSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion

    Protected Property RespostaCanal As IAC.ContractoServicio.Canal.GetCanales.Respuesta

    Protected Property RespostaSubCanal As IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Respuesta

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Metodo referente ao evento de carregamento da página.
    ''' </summary> 
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try

            If (Not IsPostBack) Then
                Me.LimparCampos()
                Me.PreencherDadosCanal()
            End If

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento gerado ao selecionar Canal no controle DropDown.
    ''' </summary>
    Protected Sub ddlCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCanal.SelectedIndexChanged

        Dim lstCanal As New List(Of String)

        If (ddlCanal.SelectedValue IsNot Nothing) Then

            If Not (ddlCanal.SelectedItem.Value Is String.Empty) Then
                lstCanal.Add(ddlCanal.SelectedItem.Value)

                ' Exibe informações de SubCanal.
                Me.PreencherDadosSubCanal(lstCanal)
            Else
                ddlSubCanal.Items.Clear()
                ddlSubCanal.Enabled = False
            End If
        End If

    End Sub

#End Region

#Region "Métodos"
    ''' <summary>
    ''' Busca informações de Canal e preenche DropDown com a descrição do mesmo.
    ''' </summary>    
    Private Sub PreencherDadosCanal()

        Me.ObjProxy = New Comunicacion.ProxyCanal()
        Me.PeticaoCanal = New IAC.ContractoServicio.Canal.GetCanales.Peticion()
        Me.RespostaCanal = New IAC.ContractoServicio.Canal.GetCanales.Respuesta()

        Me.FiltroVigente = "1"
        Me.PeticaoCanal.bolVigente = FiltroVigente

        ' Busca por todos os Canais Vigentes.
        Me.RespostaCanal = Me.ObjProxy.getCanales(Me.PeticaoCanal)

        If (Me.RespostaCanal.Canales.Count > 0) Then

            ' Ordena e Seleciona os Canais retornados da consulta anterior, incluindo uma nova coluna chamada canal que contém as informações de Código e Descrição do Canal.
            Dim canais = From c In Me.RespostaCanal.Canales
                         Order By c.codigo
                         Select canal = c.codigo & " - " & c.descripcion, c.codigo, c.descripcion, c.CodigoAjeno, c.CodigoUsuario, c.observaciones, c.vigente

            ' Preenche controle de canais.            
            ddlCanal.DataSource = canais
            ddlCanal.DataTextField = ("canal")
            ddlCanal.DataValueField = "codigo"
            ddlCanal.DataBind()

            ' Insere texto Default no controles.
            ddlCanal.Items.Insert(0, New ListItem(Tradutor.Traduzir("009_codDescricaoCanal"), String.Empty))
            ddlSubCanal.Items.Insert(0, New ListItem(Tradutor.Traduzir("009_codDescricaoSubCanal"), String.Empty))
        End If

    End Sub

    ''' <summary>
    ''' Busca informações de SubCanal e preenche DropDow com a descrição do mesmo.
    ''' </summary>
    ''' <param name="codCanal">Código do Canal.</param>    
    Private Sub PreencherDadosSubCanal(codCanal As List(Of String))

        Me.ObjProxy = New Comunicacion.ProxyCanal()
        Me.PeticaoSubCanal = New IAC.ContractoServicio.Canal.GetSubCanalesByCanal.Peticion()

        ' Busca por SubCanal utilizando o código do Canal selecionado no controle de canais.
        Me.PeticaoSubCanal.codigoCanal = codCanal
        Me.RespostaSubCanal = Me.ObjProxy.getSubCanalesByCanal(Me.PeticaoSubCanal)

        If (Me.RespostaSubCanal.Canales(0).SubCanales.Count > 0) Then

            ' Ordena e Seleciona os SubCanais retornados da consulta anterior, incluindo uma nova coluna chamada subCanal que contém as informações de Código e Descrição do SubCanal.
            Dim subCanais = From s In Me.RespostaSubCanal.Canales(0).SubCanales
                            Order By s.Descripcion
                            Select subCanal = s.Codigo & " - " & s.Descripcion, s.Codigo, s.Descripcion, s.CodigosAjenos, _
                            s.CodigosAjenosSet, s.Observaciones, s.OidSubCanal, s.Vigente

            ' Preenche controle de SubCanais.            
            ddlSubCanal.DataSource = subCanais
            ddlSubCanal.DataTextField = "subCanal"
            ddlSubCanal.DataValueField = "Codigo"
            ddlSubCanal.DataBind()

            ' Habilita DropDown de SubCanais.
            ddlSubCanal.Enabled = True
        End If

    End Sub

    ''' <summary>
    ''' Limpa controles da página.
    ''' </summary>    
    Private Sub LimparCampos()
        'Limpa controle de seleção.
        Me.ddlCanal.Items.Clear()
        Me.ddlSubCanal.Items.Clear()
        Me.ddlSubCanal.Enabled = False
    End Sub

#End Region

End Class