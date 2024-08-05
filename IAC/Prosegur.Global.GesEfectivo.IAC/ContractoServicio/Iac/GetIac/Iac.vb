Namespace Iac.GetIac

    <Serializable()> _
    Public Class Iac

#Region "Variáveis"

        Private _codidoIac As String
        Private _descripcionIac As String
        Private _observacionesIac As String
        Private _disponibleSaldos As Boolean
        Private _vigente As Boolean
#End Region

#Region "Propriedades"

        Public Property CodidoIac() As String
            Get
                Return _codidoIac
            End Get
            Set(value As String)
                _codidoIac = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property ObservacionesIac() As String
            Get
                Return _observacionesIac
            End Get
            Set(value As String)
                _observacionesIac = value
            End Set
        End Property

        Public Property vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property DisponibleSaldos() As Boolean
            Get
                Return _disponibleSaldos
            End Get
            Set(value As Boolean)
                _disponibleSaldos = value
            End Set
        End Property

#End Region
    End Class

End Namespace
