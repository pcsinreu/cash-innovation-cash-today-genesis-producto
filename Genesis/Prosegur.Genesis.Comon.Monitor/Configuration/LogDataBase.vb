Imports System.Configuration
Imports System.Text.RegularExpressions

Namespace Configuration
    Public Class LogDataBase
        Inherits ConfigurationElement

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Indica se o log será remoto ou local.
        ''' </summary>
        <ConfigurationProperty("owner")>
        Public Property owner As String
            Get
                Return Me("owner")
            End Get
            Set(value As String)
                Me("owner") = value
            End Set
        End Property

    End Class

End Namespace