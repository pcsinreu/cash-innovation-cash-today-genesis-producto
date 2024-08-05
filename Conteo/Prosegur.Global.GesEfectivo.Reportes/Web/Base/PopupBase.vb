Public Class PopupBase
    Inherits UcBase
    Implements IPostBackEventHandler

    Public Event Fechado As EventHandler(Of PopupEventArgs)
    Public Event FechadoAtualizar As EventHandler(Of PopupEventArgs)
    Protected Overridable Sub OnFechado(e As PopupEventArgs)
        RaiseEvent Fechado(Me, e)
    End Sub

    Public Sub FecharPopup()
        FecharPopup(Nothing)
    End Sub

    Public Sub FecharPopup(e As Object)
        OnFechado(New PopupEventArgs(e))
    End Sub

    Public Sub RaisePostBackEvent(IPostBackEventHandler As String) Implements System.Web.UI.IPostBackEventHandler.RaisePostBackEvent
        If IPostBackEventHandler = "FecharPopup" Then
            RaiseEvent FechadoAtualizar(Me, Nothing)
        End If
    End Sub

#Region "[PROPRIEDADES]"
    Public Property Titulo As String
#End Region

    
End Class
