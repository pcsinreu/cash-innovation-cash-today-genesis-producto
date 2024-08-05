Namespace GetProcesos

    <Serializable()> _
    Public Class Denominacion

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _esBillete As Boolean
        Private _valor As Decimal
        Private _peso As Decimal
        Private _vigente As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property EsBillete() As Boolean
            Get
                Return _esBillete
            End Get
            Set(value As Boolean)
                _esBillete = value
            End Set
        End Property

        Public Property Valor() As Decimal
            Get
                Return _valor
            End Get
            Set(value As Decimal)
                _valor = value
            End Set
        End Property

        Public Property Peso() As Decimal
            Get
                Return _peso
            End Get
            Set(value As Decimal)
                _peso = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

#End Region

    End Class

End Namespace
