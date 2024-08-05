Namespace Producto.GetProductos
    <Serializable()> _
    Public Class Producto

#Region "Variáveis"
        Private _codigoProducto As String
        Private _descripcionProducto As String
        Private _claseBillete As String
        Private _factorCorreccion As Double
        Private _descripcionMaquina As String
        Private _esManual As Boolean
        Private _vigente As Boolean
#End Region

#Region "Propriedades"

        Public Property CodigoProducto() As String
            Get
                Return _codigoProducto
            End Get
            Set(value As String)
                _codigoProducto = value
            End Set
        End Property

        Public Property DescripcionProducto() As String
            Get
                Return _descripcionProducto
            End Get
            Set(value As String)
                _descripcionProducto = value
            End Set
        End Property

        Public Property ClaseBillete() As String
            Get
                Return _claseBillete
            End Get
            Set(value As String)
                _claseBillete = value
            End Set
        End Property

        Public Property FactorCorreccion() As Double
            Get
                Return _factorCorreccion
            End Get
            Set(value As Double)
                _factorCorreccion = value
            End Set
        End Property

        Public Property DescripcionMaquinas() As String
            Get
                Return _descripcionMaquina
            End Get
            Set(value As String)
                _descripcionMaquina = value
            End Set
        End Property


        Public Property EsManual() As Boolean
            Get
                Return _esManual
            End Get
            Set(value As Boolean)
                _esManual = value
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