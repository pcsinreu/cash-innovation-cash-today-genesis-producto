Imports System.Configuration

Namespace Configuration
    Public Class ConfigurationFactory

        Private Shared _ConfigurationSection As ConfigurationSection = ConfigurationManager.GetSection("LogFileConfigSection")

        Public Shared ReadOnly Property LogFile() As LogFile
            Get
                Return _ConfigurationSection.LogFile
            End Get
        End Property

        Public Shared ReadOnly Property LogDataBase() As LogDataBase
            Get
                Return _ConfigurationSection.LogDataBase
            End Get
        End Property

    End Class
End Namespace