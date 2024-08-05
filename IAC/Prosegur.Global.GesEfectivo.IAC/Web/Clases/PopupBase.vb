Public Class PopupBaseold
    Inherits UserControlBase
    Implements IPostBackEventHandler

    Public Event Fechado As EventHandler(Of PopupEventArgs)

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
            FecharPopup()
        End If
    End Sub

#Region "[PROPRIEDADES]"
    Public Property Titulo As String
#End Region

End Class
