Imports System.Windows.Input
Imports System.Windows

Namespace Command

    Public NotInheritable Class ClickBindingHelper
        Private Sub New()
        End Sub
        Public Shared Function GetDoubleClickCommand(obj As DependencyObject) As ICommand
            Return CType(obj.GetValue(DoubleClickCommandProperty), ICommand)
        End Function
        Public Shared Sub SetDoubleClickCommand(obj As DependencyObject, value As ICommand)
            obj.SetValue(DoubleClickCommandProperty, value)
        End Sub

        Public Shared ReadOnly DoubleClickCommandProperty As DependencyProperty = DependencyProperty.RegisterAttached("DoubleClickCommand", GetType(ICommand), GetType(ClickBindingHelper), New PropertyMetadata(Nothing, AddressOf OnDoubleClickCommandChanged))

        Private Shared Sub OnDoubleClickCommandChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            Dim element As UIElement = TryCast(d, UIElement)
            If element IsNot Nothing Then
                element.InputBindings.Clear()
                Dim command As ICommand = TryCast(e.NewValue, ICommand)
                If command IsNot Nothing Then
                    element.InputBindings.Add(New MouseBinding(command, New MouseGesture(MouseAction.LeftDoubleClick)))
                End If
            End If
        End Sub

    End Class

End Namespace