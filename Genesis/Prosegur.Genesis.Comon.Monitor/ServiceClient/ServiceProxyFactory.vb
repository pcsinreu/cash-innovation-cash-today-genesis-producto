Namespace ServiceClient
    Public Class ServiceProxyFactory

#Region "Singleton"
        Private Shared ReadOnly _ServiceProxyFactory As New ServiceProxyFactory()

        Protected Sub New()
        End Sub

        Public Shared Function Instancia() As ServiceProxyFactory
            Return _ServiceProxyFactory
        End Function

#End Region

#Region "Variáveis Membro"
        Private _SyncLock As Object = New Object()
#End Region

#Region " Serviços "

#Region " Archivo "

        Private _ServiceLog As ServiceLogSvc.ServiceLogClient
        Public ReadOnly Property ServiceLog() As ServiceLogSvc.ServiceLogClient
            Get
                If (_ServiceLog Is Nothing _
                    OrElse _ServiceLog.State = ServiceModel.CommunicationState.Closed _
                    OrElse _ServiceLog.State = ServiceModel.CommunicationState.Faulted _
                    OrElse _ServiceLog.State = ServiceModel.CommunicationState.Closing) Then

                    SyncLock (_SyncLock)
                        If (_ServiceLog Is Nothing _
                            OrElse _ServiceLog.State = ServiceModel.CommunicationState.Closed _
                            OrElse _ServiceLog.State = ServiceModel.CommunicationState.Faulted _
                            OrElse _ServiceLog.State = ServiceModel.CommunicationState.Closing) Then

                            _ServiceLog = New ServiceLogSVC.ServiceLogClient()

                        End If
                    End SyncLock

                End If
                Return _ServiceLog
            End Get
        End Property

#End Region


#End Region

    End Class
End Namespace