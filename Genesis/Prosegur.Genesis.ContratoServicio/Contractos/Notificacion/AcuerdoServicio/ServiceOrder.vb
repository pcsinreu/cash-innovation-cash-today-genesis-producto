Namespace Contractos.Notification.AcuerdoServicio
    Public Class ServiceOrder
        Public Property id As String
        Public Property code As String
        Public Property operationalCode As String
        Public Property solutionCode As String
        Public Property productCode As String
        Public Property origin As Origin
        Public Property operationalConfigs As OperationalConfig()
        Public Property modifiedDate As DateTime
        Public Property modifiedSinceLastPublish As Boolean
    End Class
End Namespace
