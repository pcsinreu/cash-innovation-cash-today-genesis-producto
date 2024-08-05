Namespace GetProcesos

    <Serializable()> _
    Public Class Producto

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _claseBillete As String
        Private _factorCorreccion As Decimal
        Private _procesadoManual As Boolean
        Private _maquinas As MaquinaColeccion

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

        Public Property ClaseBillete() As String
            Get
                Return _claseBillete
            End Get
            Set(value As String)
                _claseBillete = value
            End Set
        End Property

        Public Property FactorCorreccion() As Decimal
            Get
                Return _factorCorreccion
            End Get
            Set(value As Decimal)
                _factorCorreccion = value
            End Set
        End Property

        Public Property ProcesadoManual() As Boolean
            Get
                Return _procesadoManual
            End Get
            Set(value As Boolean)
                _procesadoManual = value
            End Set
        End Property

        Public Property Maquinas() As MaquinaColeccion
            Get
                Return _maquinas
            End Get
            Set(value As MaquinaColeccion)
                _maquinas = value
            End Set
        End Property

#End Region

    End Class

End Namespace
