Namespace Canal.GetCanales

    Public Class Canal

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _FyhActualizacion As Date
        Private _codigoUsuario As String
        Private _CodigoAjeno As CodigoAjeno.CodigoAjenoColeccionBase

        Public Property codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property observaciones() As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                _observaciones = value
            End Set
        End Property

        Public Property vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property CodigoAjeno() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigoAjeno
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigoAjeno = value
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

    End Class

End Namespace
