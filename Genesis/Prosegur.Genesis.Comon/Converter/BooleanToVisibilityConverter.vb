Imports System.Windows.Data
Imports System.Windows

Namespace Converter

    Public Class BooleanToVisibilityConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert

            Dim flag As Boolean = True
            If (TypeOf value Is Boolean) Then
                flag = value
            ElseIf (TypeOf value Is Boolean?) Then

                Dim nullable As Boolean? = value
                flag = If(nullable.HasValue, nullable.Value, False)
            End If

            Return If(flag, Visibility.Visible, Visibility.Collapsed)

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return (TypeOf value Is Visibility AndAlso (value = Visibility.Visible))
        End Function

    End Class

End Namespace
