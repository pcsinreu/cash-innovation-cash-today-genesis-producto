Namespace Delegacion.SetDelegacion
    <Serializable()>
    Public Class ClienteFacturacion

        Private _OidClienteCapital As String
        Private _OidSubClienteTesoreria As String
        Private _OidPtoServicioTesoreria As String

        Public Property OidClienteCapital() As String
            Get
                Return _OidClienteCapital
            End Get
            Set(value As String)
                _OidClienteCapital = value
            End Set
        End Property
        Public Property OidSubClienteTesoreria() As String
            Get
                Return _OidSubClienteTesoreria
            End Get
            Set(value As String)
                _OidSubClienteTesoreria = value
            End Set
        End Property
        Public Property OidPtoServicioTesoreria() As String
            Get
                Return _OidPtoServicioTesoreria
            End Get
            Set(value As String)
                _OidPtoServicioTesoreria = value
            End Set
        End Property
    End Class

End Namespace
