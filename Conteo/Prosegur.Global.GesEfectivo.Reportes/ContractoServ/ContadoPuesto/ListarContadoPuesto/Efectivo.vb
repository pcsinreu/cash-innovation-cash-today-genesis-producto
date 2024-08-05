Namespace ContadoPuesto.ListarContadoPuesto

    Public Class Efectivo

#Region " Variáveis "

        Private _Divisa As String = String.Empty
        Private _Denominacion As Decimal
        Private _Tipo As String
        Private _Unidades As Integer = 0
        Private _Falsos As Integer = 0

#End Region

#Region " Propriedades "

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
            End Set
        End Property

        Public Property Denominacion() As Decimal
            Get
                Return _Denominacion
            End Get
            Set(value As Decimal)
                _Denominacion = value
            End Set
        End Property

        Public Property Tipo() As String
            Get
                Return _Tipo
            End Get
            Set(value As String)
                _Tipo = value
            End Set
        End Property

        Public Property Unidades() As Integer
            Get
                Return _Unidades
            End Get
            Set(value As Integer)
                _Unidades = value
            End Set
        End Property

        Public Property Falsos() As Integer
            Get
                Return _Falsos
            End Get
            Set(value As Integer)
                _Falsos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
