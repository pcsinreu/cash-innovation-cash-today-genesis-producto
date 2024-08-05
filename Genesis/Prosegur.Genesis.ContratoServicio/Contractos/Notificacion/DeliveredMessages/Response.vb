Imports System.Runtime.Serialization

Namespace Contractos.Notification.DeliveredMessages
    <Serializable()>
    Public Class Response
        Public Property timestamp As String
        Public Property status As Integer
        <DataMember(Name:="error")>
        Public Property errorLog As String
        Public Property message As String
        Public Property path As String
    End Class
End Namespace