Imports System.Windows

Namespace AttachedProperty

    Public Class DialogCloser
        Public Shared ReadOnly DialogResultProperty As DependencyProperty = DependencyProperty.RegisterAttached(
                                                                            "DialogResult",
                                                                            GetType(Boolean?),
                                                                            GetType(DialogCloser),
                                                                            New PropertyMetadata(AddressOf DialogResultChanged))

        Private Shared Sub DialogResultChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
            Dim window = DirectCast(d, Window)
            If window IsNot Nothing Then
                Try
                    window.DialogResult = DirectCast(e.NewValue, Boolean?)
                Catch ex As Exception
                    'Ignora erros de janelas que não foram recolhidas pelo garbage collector
                End Try
            End If
        End Sub

        Public Shared Sub SetDialogResult(ByVal target As Window, ByVal value As Boolean?)
            target.SetValue(DialogResultProperty, value)
        End Sub

    End Class

End Namespace