Namespace RecuperarSaldosPorSector

    Public Class Fajo

#Region "[VARIÁVEIS]"

        Private _Especie As Integer
        Private _Descripcion As String
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

        Public Property Especie() As Integer
            Get
                Return _Especie
            End Get
            Set(Value As Integer)
                _Especie = Value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(Value As String)
                _Descripcion = Value
            End Set
        End Property

        Public Property Cantidad() As Integer
            Get
                Return _Cantidad
            End Get
            Set(value As Integer)
                _Cantidad = value
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

#End Region

    End Class

End Namespace