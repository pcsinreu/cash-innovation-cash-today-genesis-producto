Imports System.Windows.Data
Imports System.Windows

Namespace Converter

    Public Class BooleanRevertConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert

            If (TypeOf value Is Boolean) Then
                Return Not value
            End If

            Return False

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack

            If (TypeOf value Is Boolean) Then
                Return value
            End If

            Return True

        End Function

    End Class

End Namespace
