Namespace RecuperarSaldosPorSector

    Public Class Saldo

#Region "[VARIÁVEIS]"

        Private _Disponible As Boolean
        Private _Canal As New Canal
        Private _Cliente As New Cliente
        Private _Moneda As New Moneda
        Private _Importe As Decimal
        Private _Fajos As New Fajos

#End Region

#Region "[PROPRIEDADES]"

        Public Property Disponible() As Boolean
            Get
                Return _Disponible
            End Get
            Set(value As Boolean)
                _Disponible = value
            End Set
        End Property

        Public Property Canal() As Canal
            Get
                Return _Canal
            End Get
            Set(value As Canal)
                _Canal = value
            End Set
        End Property

        Public Property Cliente() As Cliente
            Get
                Return _Cliente
            End Get
            Set(value As Cliente)
                _Cliente = value
            End Set
        End Property

        Public Property Moneda() As Moneda
            Get
                Return _Moneda
            End Get
            Set(value As Moneda)
                _Moneda = value
            End Set
        End Property

        Public Property Importe() As Decimal
            Get
                Return _Importe
            End Get
            Set(value As Decimal)
                _Importe = value
            End Set
        End Property

        Public Property Fajos() As Fajos
            Get
                Return _Fajos
            End Get
            Set(value As Fajos)
                _Fajos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
