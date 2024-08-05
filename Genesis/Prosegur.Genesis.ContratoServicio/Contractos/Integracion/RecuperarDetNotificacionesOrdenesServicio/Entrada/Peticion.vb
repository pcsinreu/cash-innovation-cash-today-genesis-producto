Namespace Contractos.Integracion.RecuperarDetNotificacionesOrdenesServicio
    Public Class Peticion
        Private _idIntegracion As String
        Public Property Oid_integracion() As String
            Get
                Return _idIntegracion
            End Get
            Set(ByVal value As String)
                _idIntegracion = value
            End Set
        End Property

        Public Sub New()
            Me._idIntegracion = String.Empty
        End Sub

    End Class
End Namespace
