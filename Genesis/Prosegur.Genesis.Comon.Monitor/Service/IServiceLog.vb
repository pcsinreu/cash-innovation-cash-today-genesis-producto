Imports System.ServiceModel

Namespace Service
    <ServiceContract>
    Public Interface IServiceLog

        <OperationContract>
        Sub WriteLog(message As String)
    End Interface
End Namespace