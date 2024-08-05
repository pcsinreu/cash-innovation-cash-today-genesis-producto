Public Class Constantes

    Public Shared VERSAO As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

    '''  build da aplicação  
    Public Shared BUILD As String = My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision.ToString("0000")

    Public Const MINUTOS_EXPIRAR_TOKEN As Integer = 3

End Class

