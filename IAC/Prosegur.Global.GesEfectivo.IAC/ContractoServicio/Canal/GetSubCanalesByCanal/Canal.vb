Namespace Canal.GetSubCanalesByCanal
    <Serializable()> _
    Public Class Canal

        Private _OidCanal As String
        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _FyhActualizacion As Date
        Private _codigoUsuario As String
        Private _subCanales As SubCanalColeccion
        Private _CodigoAjenos As CodigoAjeno.CodigoAjenoColeccionBase

        Public Property OidCanal() As String
            Get
                Return _OidCanal
            End Get
            Set(value As String)
                _OidCanal = value
            End Set
        End Property

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

        Public Property Observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
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

        Public Property FyhActualizacion() As Date
            Get
                Return _FyhActualizacion
            End Get
            Set(value As Date)
                _FyhActualizacion = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _codigoUsuario
            End Get
            Set(value As String)
                _codigoUsuario = value
            End Set
        End Property

        Public Property SubCanales() As SubCanalColeccion
            Get
                Return _subCanales
            End Get
            Set(value As SubCanalColeccion)
                _subCanales = value
            End Set
        End Property

        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigoAjenos
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigoAjenos = value
            End Set
        End Property

    End Class
End Namespace
