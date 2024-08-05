Imports Prosegur.Framework
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis
Imports Prosegur.Genesis.Comon
Imports Prosegur.Genesis.Comon.UtilHelper
Imports Prosegur.Genesis.LogicaNegocio
Imports Prosegur.Genesis.Comon.Extenciones

Public Class ucHelperBusquedaDatos
    Inherits UcHelperBase

#Region "[PROPRIEDADES]"

    ''' <summary>
    ''' Define a query para a pesquisa do dados do controle helper.
    ''' </summary>
    Public Property QueryDefault As UtilHelper.QueryHelperControl

    Public Property PopUpAbierto As Boolean
        Get
            Return ViewState(Me.ID + "PopUpAbierto")
        End Get
        Set(value As Boolean)
            ViewState(Me.ID + "PopUpAbierto") = value
        End Set
    End Property

    ''' <summary>
    ''' Dados de resposta consulta que foram selecionados pelo usuário.
    ''' </summary>
    Public Property RegistrosSelecionados As RespuestaHelper
        Get
            Return ViewState(Me.ID + "_RegistrosSelecionados")
        End Get
        Set(value As RespuestaHelper)
            ViewState(Me.ID + "_RegistrosSelecionados") = value
        End Set
    End Property

    Private WithEvents _ucPopUp As Popup2
    Public Property ucPopup() As Popup2
        Get
            If _ucPopUp Is Nothing Then
                _ucPopUp = LoadControl("~\Controles\Helpers\Popup2.ascx")
                _ucPopUp.ID = Me.ID & "_ucPopup"
                _ucPopUp.Height = HeightPopUp
                _ucPopUp.EsModal = True
                _ucPopUp.AutoAbrirPopup = False
                _ucPopUp.PopupBase = ucBusquedaAvanzada
                phUcPopUp.Controls.Add(_ucPopUp)
            End If
            Return _ucPopUp
        End Get
        Set(value As Popup2)
            _ucPopUp = value
        End Set
    End Property

    Private WithEvents _ucBusquedaAvanzada As ucHelperBusquedaAvanzada
    Public Property ucBusquedaAvanzada() As ucHelperBusquedaAvanzada
        Get
            If _ucBusquedaAvanzada Is Nothing Then
                _ucBusquedaAvanzada = LoadControl("~\Controles\Helpers\ucHelperBusquedaAvanzada.ascx")
                _ucBusquedaAvanzada.ID = Me.ID & "_ucBusquedaAvanzada"
                _ucBusquedaAvanzada.Titulo = Me.Popup_Titulo
                _ucBusquedaAvanzada.ValorLabelFiltro = Me.Popup_Filtro
                _ucBusquedaAvanzada.ValorLabelResultado = Me.Popup_Resultado
                _ucBusquedaAvanzada.Tabela = Me.Tabela
                _ucBusquedaAvanzada.FiltroConsulta = Me.FiltroConsulta
                _ucBusquedaAvanzada.OrderConsulta = Me.OrdenacaoConsulta
                _ucBusquedaAvanzada.JoinConsulta = Me.JoinConsulta
                _ucBusquedaAvanzada.MaxRegistroPorPagina = Me.MaxRegistroPorPagina
                _ucBusquedaAvanzada.esMultiSelecao = Me.MultiSelecao
                _ucBusquedaAvanzada.QueryDefault = Me.QueryDefault
                AddHandler _ucBusquedaAvanzada.Erro, AddressOf ErroControles
            End If
            Return _ucBusquedaAvanzada
        End Get
        Set(value As ucHelperBusquedaAvanzada)
            _ucBusquedaAvanzada = value
        End Set
    End Property

    ''' <summary>
    ''' Determina o valor da Altura do Popup de Busca a ser exibido (Valor da propriedade CSS Height).
    ''' </summary>
    Private _HeightPopUp As Integer = 480
    Public Property HeightPopUp As Integer
        Get
            Return _HeightPopUp
        End Get
        Set(value As Integer)
            _HeightPopUp = value
        End Set
    End Property

    ' Delegates.    
    Public Delegate Sub UpdatedControlEventHandler()

    ' Eventos.    
    Event UpdatedControl As UpdatedControlEventHandler
    Public Event Pesquisar As EventHandler

#End Region

#Region "[OVERRIDES]"

    ''' <summary>
    ''' Inicializa Popup de Busca.
    ''' </summary>    
    Protected Overrides Sub Inicializar()
        ConfigurarCampos()
        If PopUpAbierto Then
            Me.PreencherCodigoDescricao()
            Me.ConfigurarControle_BusquedaAvanzada(False)
        End If
    End Sub

    Public Overrides Sub Focus()
        txtCodigo.Focus()
    End Sub

    ''' <summary>
    ''' Exibe controle Popup para realização de busca avançada.
    ''' </summary>
    Protected Sub ConfigurarControle_BusquedaAvanzada(Optional EnviarRegistrosSelecionados As Boolean = True)

        ucPopup.AbrirPopup()
        ucBusquedaAvanzada.Codigo = txtCodigo.Text
        ucBusquedaAvanzada.Descricao = txtDescripcion.Text
        If EnviarRegistrosSelecionados Then
            ucBusquedaAvanzada.RegistrosSelecionados = Me.RegistrosSelecionados

            If Me.RegistrosSelecionados IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta IsNot Nothing Then
                ucBusquedaAvanzada.RegistrosTemporarios = Me.RegistrosSelecionados.DatosRespuesta
            End If

        Else
            ucBusquedaAvanzada.RegistrosSelecionados = Nothing
        End If

        PopUpAbierto = True
        ucBusquedaAvanzada.Focus()

    End Sub

    ''' <summary>
    ''' Limpa os webcontrols da tela.
    ''' </summary>    
    Public Overrides Sub LimparCampos()

        RemoveHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged
        RemoveHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged

        Me.txtCodigo.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.dvButtonLimpaCampo.Style.Item("display") = "none"

        If (MultiSelecao AndAlso (Me.RegistrosSelecionados IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta.Count > 1)) Then
            Me.ExibirMultDados(False)
        End If

        AddHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged
        AddHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged

    End Sub
#End Region

#Region "[EVENTOS]"

    ''' <summary>
    ''' Método referente ao evento ocorrido durante alteração do conteúdo da caixa de texto referente à Código.
    ''' </summary>    
    Protected Sub txtCodigo_TextChanged(sender As Object, e As System.EventArgs) Handles txtCodigo.TextChanged
        Try
            Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.CodigoTextChanged
            If Not PopUpAbierto Then
                If Not String.IsNullOrEmpty(txtCodigo.Text) Then
                    RaiseEvent Pesquisar(Me, e)

                    RemoveHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged
                    Me.txtDescripcion.Text = String.Empty
                    AddHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged

                    Me.PesquisarDadosHelper(False)
                Else
                    Me.LimparViewState()
                    Me.ExibirDados()
                    Me.Focus()
                End If
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento ocorrido durante alteração do conteúdo da caixa de texto refernte à Descrição.
    ''' </summary>    
    Protected Sub txtDescripcion_TextChanged(sender As Object, e As System.EventArgs) Handles txtDescripcion.TextChanged
        Try
            Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.DescripcionTextChanged
            If Not PopUpAbierto Then
                RaiseEvent Pesquisar(Me, e)

                RemoveHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged
                Me.txtCodigo.Text = String.Empty
                AddHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged

                Me.PesquisarDadosHelper(False)
            End If
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento de Busca do Helper.
    ''' </summary>    
    Protected Sub imgButtonBusqueda_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonBusqueda.Click
        Try
            Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.BusquedaClick
            RaiseEvent Pesquisar(Me, e)
            Me.PesquisarDadosHelper()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao evento de Exclusão dos valores dos campos de Código e Descrição do controle.
    ''' </summary>
    Protected Sub imgButtonLimpaCampo_Click(sender As Object, e As ImageClickEventArgs) Handles imgButtonLimpaCampo.Click
        Try
            Me.LimparViewState()
            Me.ExibirDados(True)

            PopUpAbierto = False

            Me.ucBusquedaAvanzada.LimparMemoria()
            Me.ucBusquedaAvanzada = Nothing
            phUcPopUp.Controls.Clear()
            Me.ucPopup = Nothing

            Me.Focus()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Método referente ao Evento de Erro do Popup.
    ''' </summary>
    Protected Sub ucBusquedaAvanzada_Erro(sender As Object, e As ErroEventArgs) Handles _ucBusquedaAvanzada.Erro
        Me.NotificarErro(e.Erro)
    End Sub

    ''' <summary>
    ''' Método referente ao Evento Fechado do Popup.
    ''' </summary>
    Protected Sub ucBusquedaAvanzada_Fechado(sender As Object, e As PopupEventArgs) Handles _ucBusquedaAvanzada.Fechado
        Try
            'Verifica se o resultado não é vazio
            If e.Resultado IsNot Nothing Then
                Dim RespuestaPopUp As RespuestaHelper = DirectCast(e.Resultado, Prosegur.Genesis.Comon.RespuestaHelper)
                If RespuestaPopUp.ResultadoModificado Then
                    If Me.MultiSelecao AndAlso (Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.CodigoTextChanged OrElse Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.DescripcionTextChanged) Then
                        'se é multselecção
                        'Verifica se a resposta do popup existe na lista dos selecionados
                        'se encontrou algum registro então verifica se já existe na lista,
                        'se existe não insere
                        If (Me.RegistrosSelecionados IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta.Count > 0) AndAlso _
                            (RespuestaPopUp IsNot Nothing AndAlso RespuestaPopUp.DatosRespuesta IsNot Nothing AndAlso RespuestaPopUp.DatosRespuesta.Count > 0) Then

                            For Each dados In RespuestaPopUp.DatosRespuesta
                                If Not Me.RegistrosSelecionados.DatosRespuesta.Exists(Function(d) d.Identificador = dados.Identificador) Then
                                    'Insere na primeira posição
                                    Me.RegistrosSelecionados.DatosRespuesta.Insert(0, dados)
                                End If
                            Next
                        Else
                            Me.RegistrosSelecionados = RespuestaPopUp
                        End If
                    Else
                        Me.RegistrosSelecionados = RespuestaPopUp
                    End If

                ElseIf Me.ucBusquedaAvanzada.eventoExecutado = EventoPopup.FecharClick Then

                    Dim strCodigo As String = String.Empty
                    Dim strDescripcion As String = String.Empty
                    Dim executarScript As Boolean = False

                    'Verifica se o textbox codigo ou descrição está preenchido, se sim, então deve limpar os campos...
                    If Not String.IsNullOrEmpty(Me.txtCodigo.Text) AndAlso String.IsNullOrEmpty(Me.txtDescripcion.Text) Then
                        executarScript = True

                    ElseIf String.IsNullOrEmpty(Me.txtCodigo.Text) AndAlso Not String.IsNullOrEmpty(Me.txtDescripcion.Text) Then
                        executarScript = True
                    End If

                    If executarScript Then
                        'só deve limpar os campos se tiver mais de um registro selecionado,
                        'senão o registro atual deverá permanecer..
                        If (Me.RegistrosSelecionados IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta.Count = 1) Then
                            'Verifica se o textbox codigo ou descrição está preenchido, se sim, então deve limpar os campos...
                            If Not String.IsNullOrEmpty(Me.txtCodigo.Text) AndAlso String.IsNullOrEmpty(Me.txtDescripcion.Text) Then
                                strCodigo = Me.RegistrosSelecionados.DatosRespuesta(0).Codigo
                                strDescripcion = Me.RegistrosSelecionados.DatosRespuesta(0).Descricao
                            ElseIf String.IsNullOrEmpty(Me.txtCodigo.Text) AndAlso Not String.IsNullOrEmpty(Me.txtDescripcion.Text) Then
                                strCodigo = Me.RegistrosSelecionados.DatosRespuesta(0).Codigo
                                strDescripcion = Me.RegistrosSelecionados.DatosRespuesta(0).Descricao
                            End If
                        End If

                        Dim script As New StringBuilder
                        script.Append(" try {")
                        script.AppendFormat(" document.getElementById('{0}').value ='{1}';", Me.txtCodigo.ClientID, strCodigo)
                        script.AppendFormat(" document.getElementById('{0}').value ='{1}';", Me.txtDescripcion.ClientID, strDescripcion)
                        script.Append(" }")
                        script.Append(" catch (e) { }")
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "atualizarCampos", script.ToString, True)
                    End If
                End If
            End If

            Me.ExibirDados()
            PopUpAbierto = False

            Me.ucBusquedaAvanzada.LimparMemoria()
            Me.ucBusquedaAvanzada = Nothing
            phUcPopUp.Controls.Clear()
            Me.ucPopup = Nothing

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    
#End Region

#Region "[METODOS]"

    ''' <summary>
    ''' Configura Controles do Controle. Ignorando o Overrides.
    ''' </summary>
    Protected Sub TraduzirControle()
        lblTitulo.Text = Me.Titulo
        '  lblTituloValor.Text = Me.Titulo
    End Sub

    Private Sub ConfigurarCampos()
        ' Verifica se o controle estará habilitado para o usuário.
        TraduzirControle()

        dvButtonBusqueda.Style.Item("display") = "none"
        lblTitulo.Style.Item("display") = "none"
        txtCodigo.Style.Item("display") = "none"
        ucHelper.Style.Item("display") = "none"
        If Not String.IsNullOrEmpty(txtCodigo.Text) AndAlso Not String.IsNullOrEmpty(txtDescripcion.Text) AndAlso ControleHabilitado Then
            dvButtonLimpaCampo.Style.Item("display") = "block"
            dvButtonLimpaCampo.Style.Item("margin") = "0px 2px 0px 0px"
        Else
            dvButtonLimpaCampo.Style.Item("display") = "none"
        End If

        If Me.Obrigatorio Then
            txtCodigo.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all ui-gn-mandatory"
        Else
            txtCodigo.CssClass = "ui-inputfield ui-inputtext ui-widget ui-state-default ui-corner-all"
        End If

        If ControleHabilitado Then
            lblTitulo.Style.Item("display") = "block"
            txtCodigo.Style.Item("display") = "block"
            ucHelper.Style.Item("display") = "block"
            dvButtonBusqueda.Style.Item("display") = "block"
            dvButtonBusqueda.Style.Item("margin") = "0px 2px 0px 0px"
            Me.imgButtonLimpaCampo.Style.Add("cursor", "pointer")
            Me.imgButtonBusqueda.Style.Add("cursor", "pointer")
            Me.imgButtonBusqueda.Style.Add("margin-top", "3px")
            Me.imgButtonBusqueda.Style.Add("margin-left", "2px")
            
            txtCodigo.Enabled = ControleHabilitado
            txtDescripcion.Enabled = ControleHabilitado
            imgButtonBusqueda.Enabled = ControleHabilitado
            imgButtonLimpaCampo.Enabled = ControleHabilitado
            dvDescripcion.Style.Item("margin") = "0px 2px 0px 0px"
            dvDescripcion.Style.Item("display") = "block"

            '  ucHelper.Style.Item("width") = "430px !important"
            ucHelper.Style.Item("margin-right") = "5px !important"

        Else
            ucHelper.Style.Item("width") = "auto !important"
            ucHelper.Style.Item("margin-right") = "30px !important"
        End If

    End Sub

    ''' <summary>
    ''' Valida existência dos filtros a serem incluídos na pesquisa e busca por dados Helper.
    ''' </summary>
    Protected Sub PesquisarDadosHelper(Optional EnviarRegistrosSelecionados As Boolean = True)
        Try
            Me.ucBusquedaAvanzada.consultaAvancada = False
            Me.ConfigurarControle_BusquedaAvanzada(EnviarRegistrosSelecionados)
            Me.ucBusquedaAvanzada.ActualizarBusqueda()
        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Insere resposta da consulta nos campos da tela.
    ''' </summary>
    Public Sub ExibirDados(Optional excutaEventoAtualizar As Boolean = True)
        Try
            Me.ConfigurarCampos()
            Me.LimparCampos()

            If RegistrosSelecionados IsNot Nothing AndAlso RegistrosSelecionados.DatosRespuesta IsNot Nothing Then

                If MultiSelecao AndAlso (RegistrosSelecionados.DatosRespuesta.Count > 1) Then
                    Me.ExibirMultDados(True)

                    ' Preenche Listbox
                    For index = RegistrosSelecionados.DatosRespuesta.Count - 1 To 0 Step -1
                        Dim item As Comon.Helper.Respuesta = RegistrosSelecionados.DatosRespuesta(index)
                    Next
                Else
                    Me.PreencherCodigoDescricao()
                End If
            End If

            If (excutaEventoAtualizar) Then RaiseEvent UpdatedControl()

        Catch ex As Exception
            NotificarErro(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Preenche os campos Código e Descrição do Controle.
    ''' </summary>
    Protected Sub PreencherCodigoDescricao()

        If RegistrosSelecionados IsNot Nothing AndAlso RegistrosSelecionados.DatosRespuesta IsNot Nothing _
            AndAlso RegistrosSelecionados.DatosRespuesta.Count = 1 Then

            RemoveHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged
            RemoveHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged

            txtCodigo.Text = RegistrosSelecionados.DatosRespuesta(0).Codigo
            txtDescripcion.Text = RegistrosSelecionados.DatosRespuesta(0).Descricao
  
            If Not String.IsNullOrEmpty(txtCodigo.Text) AndAlso Not String.IsNullOrEmpty(txtDescripcion.Text) AndAlso ControleHabilitado Then
                dvButtonLimpaCampo.Style.Item("display") = "block"
            End If

            AddHandler txtCodigo.TextChanged, AddressOf txtCodigo_TextChanged
            AddHandler txtDescripcion.TextChanged, AddressOf txtDescripcion_TextChanged

        End If

    End Sub

    ''' <summary>
    ''' Controle visibilidade dos controles para exibição e manipulação de MultDados.
    ''' </summary>
    ''' <param name="visivel">Se True exibe controles.</param>
    Protected Sub ExibirMultDados(visivel As Boolean)
   
    End Sub

    ''' <summary>
    ''' Limpa ViewState da página.
    ''' </summary>
    Public Sub LimparViewState()

        Me.RegistrosSelecionados = Nothing
        Me.ucBusquedaAvanzada.LimparMemoria()
        Me.ucBusquedaAvanzada = Nothing
        Me.ucPopup = Nothing

    End Sub

    ''' <summary>
    ''' Trata mensgem de erro dos Controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ErroControles(sender As Object, e As ErroEventArgs)
        MyBase.NotificarErro(e.Erro)
    End Sub

    Public Sub FocusControle()
        txtCodigo.Focus()
    End Sub

#End Region

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If (MultiSelecao AndAlso (Me.RegistrosSelecionados IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta IsNot Nothing AndAlso Me.RegistrosSelecionados.DatosRespuesta.Count > 1)) Then
            Me.ExibirMultDados(True)
        End If
    End Sub
End Class