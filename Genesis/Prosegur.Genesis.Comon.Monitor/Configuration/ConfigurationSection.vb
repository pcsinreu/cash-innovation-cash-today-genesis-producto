Imports System.Configuration

Namespace Configuration
    Public Class ConfigurationSection
        Inherits System.Configuration.ConfigurationSection

        <ConfigurationProperty("LogFile")> _
        Public Property LogFile() As LogFile
            Get
                Return Me("LogFile")
            End Get
            Set(value As LogFile)
                Me("LogFile") = value
            End Set
        End Property

        <ConfigurationProperty("LogDataBase")> _
        Public Property LogDataBase() As LogDataBase
            Get
                Return Me("LogDataBase")
            End Get
            Set(value As LogDataBase)
                Me("LogDataBase") = value
            End Set
        End Property

    End Class
End Namespace