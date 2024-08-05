Namespace GetProceso

    <Serializable()> _
    Public Class Denominacion

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _esBillete As Boolean
        Private _valor As Decimal
        Private _peso As Decimal

#End Region

#Region "Propriedades"

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

#End Region
    End Class

End Namespace