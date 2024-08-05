Imports Prosegur.Framework
Imports Prosegur.Global.GesEfectivo.IAC.ContractoServicio
Imports Prosegur.Framework.Dicionario.Tradutor
Imports Prosegur.Genesis.ContractoServicio

''' <summary>
''' Classe do Controle Genérico de Popup Modal.
''' </summary>
''' <history>
''' [Thiago Dias] - 14/08/2013.
''' </history>
Public Class ucModalPopup
    Inherits UcBase
    Implements INamingContainer

#Region "Campo"

    Dim _Largura As Int32?
    Dim _Altura As Int32?
    Dim _PopupBase As PopupBase
    Dim script As StringBuilder

#End Region

#Region "Propriedades"

    ''' <summary>
    ''' Valor a ser exibido no Título do Controle de Popup de Busca.
    ''' </summary>
    Property TituloFormBusca As String

    ''' <summary>
    ''' Valor a ser exibido no Título do Grid de Popup de Busca.
    ''' </summary>
    Protected TituloFormGrid As String

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção modal do Dialog Widget (jquery).
    ''' </summary>    
    Property PopupModal As Boolean

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção autoOpen do Dialog Widget (jquery).
    ''' </summary>    
    Property AutoExibir As Boolean

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção resizable do Dialog Widget (jquery).
    ''' </summary>
    Property Redimensionavel As Boolean

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção draggable do Dialog Widget (jquery).
    ''' </summary>
    Property PermiteMover As Boolean

    ''' <summary>
    ''' Recupera e Grava valor a ser utilizado na opção closeOnEscape do Dialog Widget (jquery).
    ''' </summary>    
    Property TeclaEscSair As Boolean

    ''' <summary>
    ''' Recupera e Gava informações relacionadas ao botão Fechar.
    ''' </summary>    
    Property ButtonClosePopup As Button

    ''' <summary>
    ''' Recupera e Grava nome do botão Fechar.
    ''' </summary>    
    Property CloseButtonText As String

    ''' <summary>
    ''' Recupera e Grava informações relacionadas ao Botão Abrir Janela Popup.
    ''' </summary>    
    Property ControlOpenPopup As WebControl

    ''' <summary>
    ''' Recupera e Gava informações relacionadas a classe Base de Popup.
    ''' </summary>
    Property PopupBase As PopupBase
        Get
            Return _PopupBase
        End Get
        Set(value As PopupBase)
            _PopupBase = value
            AddHandler PopupBase.Fechado, AddressOf Close_Popup
        End Set
    End Property

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção width do Dialog Widget (jquery).
    ''' </summary>
    Property Largura As Int32?
        Get
            Return _Largura
        End Get
        Set(value As Int32?)
            If (value Is Nothing OrElse value = 0) Then
                value = 778
            End If
        End Set
    End Property

    ''' <summary>
    ''' Recupera e Gava valor a ser utilizado na opção height do Dialog Widget (jquery).
    ''' </summary>
    Property Altura As Int32?
        Get
            Return _Altura
        End Get
        Set(value As Int32?)
            If (value Is Nothing OrElse value = 0) Then
                value = 580
            End If
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Método referente ao evento de carregamento da página.
    ''' </summary>
    ''' <param name="sender">Objeto gerador do evento.</param>
    ''' <param name="e">Parâmetros do evento gerado.</param>
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If (PopupBase IsNot Nothing AndAlso ModalSearch.Controls().IndexOf(PopupBase) = -1) Then

            ModalSearch.Controls.Add(PopupBase)

            If (ModalSearch.HasControls()) Then

                ModalSearch.ID &= PopupBase.UniqueID
                Me.Register_Popup()

            End If

        End If

    End Sub

#End Region

#Region "Script Popup"

    ''' <summary>
    ''' Exibe Popup de Busca na tela.
    ''' </summary>    
    Private Sub Display_Popup()

        script = New StringBuilder

        With script

            .AppendLine("$(document).ready(function(){")
            .AppendFormat("$(""#{0}"").dialog( ""option"", ""title"", ""{1}"");", ModalSearch.ClientID, PopupBase.Titulo)
            .AppendLine()
            .AppendFormat("$(""#{0}"").dialog(""open"");", ModalSearch.ClientID)
            .AppendLine()
            .AppendLine("});")

        End With

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "DisplayPopup" & Me.ClientID, script.ToString(), True)

    End Sub

    ''' <summary>
    ''' Registra e Insere funcionalidades nos controles da Janela Popup.
    ''' </summary>    
    Private Sub Register_Popup()

        script = New StringBuilder()

        With script

            .AppendLine("var fecharPopup" & PopupBase.ClientID & " = true;")
            .AppendLine("$(document).ready(function(){")

            If (ModalSearch.Controls.IndexOf(PopupBase) = -1 AndAlso PopupBase IsNot Nothing) Then

                ModalSearch.Controls.Add(PopupBase)

                If (ModalSearch.HasControls()) Then ModalSearch.ID &= PopupBase.UniqueID

            End If

            .AppendLine(Me.Generate_Popup())

            ' Registra Funcionalidade Abrir Popup.
            If (ControlOpenPopup IsNot Nothing) Then .AppendLine(Me.RegisterButtonOpenPopup())

            ' Registra Funcionalidade Fechar Popup.
            If (ButtonClosePopup IsNot Nothing) Then .AppendLine(Me.RegisterButtonClosePopup(ButtonClosePopup))

            .AppendLine("});")
            ModalSearch.Visible = True

        End With

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "RegisterPopup" & Me.ClientID, script.ToString, True)

    End Sub

    ''' <summary>
    ''' Construção da Janela Modal.
    ''' </summary>    
    Private Function Generate_Popup() As String

        script = New StringBuilder()

        With script

            .AppendFormat("$(""#{0}"").dialog({1}", ModalSearch.ClientID, "{").AppendLine()
            .AppendFormat("autoOpen: {0},", If(Me.AutoExibir, "true", "false")).AppendLine()
            .AppendLine("appendTo: ""form:first"",")

            If (Me.Altura.HasValue) Then .AppendFormat("height: {0},", Me.Altura).AppendLine()

            If (Me.Largura.HasValue) Then .AppendFormat("width: {0},", Me.Largura).AppendLine()

            .AppendFormat("modal: {0},", If(Me.PopupModal, "true", "false")).AppendLine()
            .AppendFormat("resizable: {0},", If(Me.Redimensionavel, "true", "false")).AppendLine()
            .AppendFormat("draggable: {0},", If(Me.PermiteMover, "true", "false")).AppendLine()
            .AppendFormat("closeText: ""{0}"",", If(String.IsNullOrEmpty(Me.CloseButtonText), Traduzir("btnFechar"), Me.CloseButtonText)).AppendLine()
            .AppendFormat("closeOnEscape: {0}", If(Me.TeclaEscSair, "true", "false"))

            If Not (String.IsNullOrEmpty(Me.PopupBase.Titulo)) Then
                .AppendLine(",")
                .AppendFormat(" title:""{0}""", PopupBase.Titulo).AppendLine()
            Else
                .AppendLine()
            End If

            .AppendLine(" });")

            ' Evento Fechar Popup.            
            .AppendFormat("$(""#{0}"").dialog({1}", ModalSearch.ClientID, "{").AppendLine()
            .AppendLine("close: function(event, ui) { if(fecharPopup" & PopupBase.ClientID & ") { ")
            .AppendLine(Page.ClientScript.GetPostBackEventReference(PopupBase, "FecharPopup"))
            .AppendLine(" } ")
            .AppendLine("$('body').unbind(""keypress"");")
            .AppendLine("}")
            .AppendLine(" });")

            ' Evento Abrir Popup.
            .AppendFormat("$(""#{0}"").dialog({1}", ModalSearch.ClientID, "{").AppendLine()
            .AppendLine("open: function(event, ui) {  ")
            .AppendLine("$('body').bind(""keypress"", function(e) { var code = e.keyCode || e.which;  if (e.keyCode == 13) return false; });")
            .AppendLine(" } ")
            .AppendLine(" });")

        End With

        Return script.ToString()

    End Function

    ''' <summary>
    ''' Método de exibição do Popup.
    ''' </summary>
    Private Sub Open_Popup()

        Me.Register_Popup()

    End Sub

    ''' <summary>
    ''' Método referente a ação do botão Fechar da janela de Popup.
    ''' </summary>
    Private Function RegisterButtonClosePopup(ByRef btnFechar As Button) As String

        script = New StringBuilder()

        With script

            .AppendFormat("$("" #{0}"")", btnFechar.ClientID)
            .AppendLine(".button().click(function(){")
            .AppendFormat("$(""#{0}"").dialog(""close"");", ModalSearch.ClientID).AppendLine()
            .AppendLine("return false;});")

        End With

        Return script.ToString()

    End Function

    ''' <summary>
    ''' Método referente a ação do botão Abrir janela Popup.
    ''' </summary>
    Private Function RegisterButtonOpenPopup() As String

        script = New StringBuilder()

        With script

            .AppendFormat("$("" #{0}"")", Me.ControlOpenPopup.ClientID)
            .AppendLine(".click(function(){")
            .AppendFormat(" $(""#{0}"").dialog( ""open"" );", ModalSearch.ClientID).AppendLine()
            .AppendLine(" return false;});")
            .AppendFormat("$(""#{0}"").parent().appendTo($(""form:first""));", ModalSearch.ClientID).AppendLine()

        End With

        Return script.ToString()

    End Function

    ''' <summary>
    ''' Fecha Janela Popup.
    ''' </summary>    
    Private Sub Close_Popup(sender As Object, e As PopupEventArgs)

        script = New StringBuilder()

        With script

            .AppendLine("$(document).ready(function(){ ")
            .AppendLine("fecharPopup" & PopupBase.ClientID & " = false;")
            .AppendFormat("$(""#{0}"").dialog({1}", ModalSearch.ClientID, "{").AppendLine()
            .AppendLine(" });")

            .AppendFormat("$(""#{0}"").dialog(""close"");", ModalSearch.ClientID).AppendLine()
            .AppendLine("$(window).focus();")
            .AppendLine(" });")

        End With

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "FecharPopup" & Me.ClientID, script.ToString, True)

    End Sub

#End Region

End Class