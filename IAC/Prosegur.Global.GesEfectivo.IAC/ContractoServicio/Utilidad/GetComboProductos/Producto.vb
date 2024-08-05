Namespace Utilidad.GetComboProductos

    <Serializable()> _
    Public Class Producto

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _codigoInt As Integer?
        Private _descripcion As String
        Private _descripcionClaseBillete As String        

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

        Public Property CodigoInt() As Integer?
            Get
                Return _codigoInt
            End Get
            Set(value As Integer?)
                _codigoInt = value
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

        Public Property DescripcionClaseBillete() As String
            Get
                Return _descripcionClaseBillete
            End Get
            Set(value As String)
                _descripcionClaseBillete = value
            End Set
        End Property

#End Region

    End Class
End Namespace