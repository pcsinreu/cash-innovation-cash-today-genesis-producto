Namespace Contractos.Integracion.RecuperarDetallesOrdenesServicio
    Public Class Peticion
        Private _idAcuerdoServicio As String
        Public Property Oid_acuerdo_servicio() As String
            Get
                Return _idAcuerdoServicio
            End Get
            Set(ByVal value As String)
                _idAcuerdoServicio = value
            End Set
        End Property
        Private _idSaldoAcuerdoRef As String
        Public Property Oid_saldo_acuerdo_ref() As String
            Get
                Return _idSaldoAcuerdoRef
            End Get
            Set(ByVal value As String)
                _idSaldoAcuerdoRef = value
            End Set
        End Property

        Private _productCode As String
        Public Property ProductCode() As String
            Get
                Return _productCode
            End Get
            Set(ByVal value As String)
                _productCode = value
            End Set
        End Property

        Public Sub New()
            Me._idAcuerdoServicio = String.Empty
            Me._idSaldoAcuerdoRef = String.Empty
            Me._productCode = String.Empty
        End Sub

    End Class
End Namespace
