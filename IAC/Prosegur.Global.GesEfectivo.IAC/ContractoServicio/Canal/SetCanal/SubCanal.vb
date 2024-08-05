Namespace Canal.SetCanal
    <Serializable()> _
    Public Class SubCanal

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String
        Private _vigente As Nullable(Of Boolean)
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

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

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _vigente = value
            End Set
        End Property

        Public Property gmtCreacion() As DateTime
            Get
                Return _gmtCreacion
            End Get
            Set(value As DateTime)
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion() As DateTime
            Get
                Return _gmtModificacion
            End Get
            Set(value As DateTime)
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

        Public Property CodigoAjeno() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _codigoAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
                _codigoAjeno = value
            End Set
        End Property

    End Class
End Namespace
