Namespace RecuperarRemesasPorGrupo

    Public Class Especie

#Region "[VARIAVEIS]"

        Private _Codigo As String
        Private _Descripcion As String
        Private _Cantidad As Integer
        Private _Importe As Decimal

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property
        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
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