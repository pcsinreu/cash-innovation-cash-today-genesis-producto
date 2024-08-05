Imports Prosegur.Framework.Dicionario.Tradutor

Public Class Popup2
    Inherits System.Web.UI.UserControl
    Implements INamingContainer

#Region "[PROPRIEDADES]"

    Public Property EsModal As Boolean = False
    Public Property Width As Integer? = 778
    Public Property Height As Integer? = 580
    Public Property MoverPopup As Boolean = True
    Public Property AutoAbrirPopup As Boolean = True
    Public Property AumentarTamanhoPopup As Boolean = False
    Public Property BotaoFecharPopup As Button
    Public Property TextoBotaoFechar As String
    Private _PopupBase As PopupBase
    Public Property PopupBase() As PopupBase
        Get
            Return _PopupBase
        End Get
        Set(value As PopupBase)
            _PopupBase = value
            AddHandler PopupBase.Fechado, AddressOf PopupBase_Fechado
        End Set
    End Property
    Public Property ControleClickAbrePopup As WebControl
    Public Property CloseOnEscape As Boolean = True

#End Region

#Region "[METODOS]"

    Public Sub AbrirPopup()

        Dim scripts As New StringBuilder()

        scripts.AppendLine("$(document).ready(function(){")
        scripts.AppendFormat("$('#{0}').dialog('option', 'title', '{1}');", ModalBusqueda.ClientID, PopupBase.Titulo).AppendLine()
        scripts.AppendFormat("$('#{0}').dialog('open');", ModalBusqueda.ClientID).AppendLine()
        scripts.AppendLine("});")

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "OnOpenPopup" & Me.ClientID, scripts.ToString, True)

    End Sub

    Private Sub RegistrarPopup()

        Dim scripts As New StringBuilder()

        scripts.AppendLine("var fecharPopup" & PopupBase.ClientID & " = true;")

        scripts.AppendLine("$(document).ready(function(){")

        If ModalBusqueda.Controls().IndexOf(PopupBase) = -1 AndAlso PopupBase IsNot Nothing Then
            ModalBusqueda.Controls.Add(PopupBase)
            If ModalBusqueda.HasControls() Then
                ModalBusqueda.ID = PopupBase.UniqueID
            End If
        End If

        scripts.AppendLine(CriarPopup())

        If ControleClickAbrePopup IsNot Nothing Then
            scripts.AppendLine(RegistrarBotaoAbrirPopup())
        End If

        If BotaoFecharPopup IsNot Nothing Then
            scripts.AppendLine(RegistrarBotaoCancelar(BotaoFecharPopup))
        End If

        scripts.AppendLine("});")

        ModalBusqueda.Visible = True

        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "AbrirPopup" & Me.ClientID, scripts.ToString, True)

    End Sub

    Private Function RegistrarBotaoCancelar(ByRef cancelar As Button) As String
        Dim btnCancelar As New StringBuilder

        btnCancelar.AppendFormat("$('#{0}')", cancelar.ClientID)
        btnCancelar.AppendLine(".button().click(function(){")
        btnCancelar.AppendFormat("$('#{0}').dialog('close');", ModalBusqueda.ClientID).AppendLine()
        btnCancelar.AppendLine("return false;});")

        Return btnCancelar.ToString()
    End Function

    Private Function CriarPopup() As String
        Dim jqueryModal As New StringBuilder()

        ' se não estiver registrado
        jqueryModal.AppendFormat("if(!$('#{0}').data('uiDialog')) {{ ", ModalBusqueda.ClientID)

        ' registramos novamente
        jqueryModal.AppendFormat("$('#{0}').dialog({{", ModalBusqueda.ClientID).AppendLine()
        jqueryModal.AppendFormat("autoOpen: {0},", If(AutoAbrirPopup, "true", "false")).AppendLine()
        jqueryModal.AppendFormat("appendTo: '#{0}',", ModalContainer.ClientID)
        If Height.HasValue Then
            jqueryModal.AppendFormat("height: {0},", Height).AppendLine()
        End If
        If Width.HasValue Then
            jqueryModal.AppendFormat("width: {0},", Width).AppendLine()
        End If

        jqueryModal.AppendFormat("modal: {0},", If(EsModal, "true", "false")).AppendLine()
        jqueryModal.AppendFormat("resizable: {0},", If(AumentarTamanhoPopup, "true", "false")).AppendLine()
        jqueryModal.AppendFormat("draggable: {0},", If(MoverPopup, "true", "false")).AppendLine()
        jqueryModal.AppendFormat("closeText: '{0}',", If(String.IsNullOrEmpty(TextoBotaoFechar), Traduzir("btnFechar"), TextoBotaoFechar)).AppendLine()
        jqueryModal.AppendFormat("closeOnEscape: {0}", If(CloseOnEscape, "true", "false"))

        If Not String.IsNullOrEmpty(PopupBase.Titulo) Then
            jqueryModal.AppendLine(",")
            jqueryModal.AppendFormat(" title:'{0}'", PopupBase.Titulo).AppendLine()
        Else
            jqueryModal.AppendLine()
        End If

        jqueryModal.AppendLine(" });")

        ScriptManager.GetCurrent(Me.Page).RegisterAsyncPostBackControl(PopupBase)

        'evento de fechar
        Dim postbackEvent As String = Page.ClientScript.GetPostBackEventReference(PopupBase, "FecharPopup")
        jqueryModal.AppendFormat("$('#{0}').dialog({{", ModalBusqueda.ClientID).AppendLine()
        jqueryModal.AppendLine("close: function(event, ui) { if(fecharPopup" & PopupBase.ClientID & ") { ")
        jqueryModal.AppendLine(postbackEvent)
        jqueryModal.AppendLine(" } ")
        jqueryModal.AppendLine("$('body').unbind('keypress');")
        jqueryModal.AppendLine("$('.layout-center').css('overflow', 'scroll');")
        jqueryModal.AppendLine("$(this).dialog('destroy').remove();")
        jqueryModal.AppendLine("}")
        jqueryModal.AppendLine(" });")

        'evento de abrir
        jqueryModal.AppendFormat("$('#{0}').dialog({{", ModalBusqueda.ClientID).AppendLine()
        jqueryModal.AppendLine("open: function(event, ui) {  ")
        jqueryModal.AppendLine("$('body').bind('keypress', function(e) { var code = e.keyCode || e.which;  if (e.keyCode == 13) return false; });")
        jqueryModal.AppendLine("$('.layout-center').css('overflow', 'hidden');")
        jqueryModal.AppendLine(" } ")
        jqueryModal.AppendLine(" });")

        ' if is dialog
        jqueryModal.AppendLine(" } ")

        Return jqueryModal.ToString()

    End Function

    Private Function RegistrarBotaoAbrirPopup() As String
        Dim jqueryAbrirModal As New StringBuilder

        jqueryAbrirModal.AppendFormat("$(' #{0}')", ControleClickAbrePopup.ClientID)
        jqueryAbrirModal.AppendLine(".click(function(){")
        jqueryAbrirModal.AppendFormat(" $('#{0}').dialog('open');", ModalBusqueda.ClientID).AppendLine()
        jqueryAbrirModal.AppendLine(" return false;});")
        jqueryAbrirModal.AppendFormat("$('#{0}').parent().appendTo($('#{1}'));", ModalBusqueda.ClientID, ModalContainer.ClientID).AppendLine()

        Return jqueryAbrirModal.ToString()
    End Function

#End Region

#Region "[EVENTOS]"

    Private Sub PopupBase_Fechado(sender As Object, e As PopupEventArgs)
        Dim popup As New StringBuilder

        popup.AppendLine("$(document).ready(function(){ ")
        popup.AppendLine("fecharPopup" & PopupBase.ClientID & " = false;")
        popup.AppendFormat("$('#{0}').dialog({{", ModalBusqueda.ClientID).AppendLine()
        popup.AppendLine(" });")
        popup.AppendFormat("$('#{0}').dialog('close');", ModalBusqueda.ClientID).AppendLine()
        popup.AppendFormat("$('#{0}').dialog('destroy').remove();", ModalBusqueda.ClientID).AppendLine()
        popup.AppendLine("$('.layout-center').css('overflow', 'scroll');")
        'popup.AppendLine("$(window).focus();")
        popup.AppendLine(" });")

        'ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType, "FecharPopup" & Me.ClientID, popup.ToString, True)
    End Sub

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If PopupBase IsNot Nothing AndAlso ModalBusqueda.Controls().IndexOf(PopupBase) = -1 Then
            ModalBusqueda.Controls.Add(PopupBase)
            If ModalBusqueda.HasControls() Then
                ModalBusqueda.ID = PopupBase.UniqueID
                RegistrarPopup()
            End If
        End If
    End Sub

#End Region

End Class