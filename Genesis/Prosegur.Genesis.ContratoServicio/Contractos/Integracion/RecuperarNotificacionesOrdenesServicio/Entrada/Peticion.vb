Namespace Contractos.Integracion.RecuperarNotificacionesOrdenesServicio
    Public Class Peticion
        Private _idSaldoAcuerdoRef As String
        Public Property Oid_saldo_acuerdo_ref() As String
            Get
                Return _idSaldoAcuerdoRef
            End Get
            Set(ByVal value As String)
                _idSaldoAcuerdoRef = value
            End Set
        End Property

        Public Sub New()
            Me._idSaldoAcuerdoRef = String.Empty
        End Sub

    End Class
End Namespace
