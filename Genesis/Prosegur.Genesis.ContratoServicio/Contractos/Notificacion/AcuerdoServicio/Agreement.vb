
Namespace Contractos.Notification.AcuerdoServicio
    Public Class Agreement
        Public Property countryCode As String
        Public Property contract As Contract
        Public Property serviceAgreementId As String
        Public Property entityUniqueCode As String
        Public Property centerUniqueCode As String
        Public Property servicePointUniqueCode As String
        Public Property active As Boolean
        Public Property stopType As String
        Public Property citBranches As Object()
        Public Property cpBranches As Object()
        Public Property contractLines As String()
        Public Property serviceOrders As ServiceOrder()
    End Class
End Namespace
