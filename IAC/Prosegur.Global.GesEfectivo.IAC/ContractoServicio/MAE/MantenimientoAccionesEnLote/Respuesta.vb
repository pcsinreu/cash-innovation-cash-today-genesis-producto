Namespace MAE.MantenimientoAccionesEnLote
    Public Class Respuesta
        Private _oid_maquina As String
        Private _descripcion As String
        Private _considerarecuento As Boolean
        Private _multicliente As Boolean

        Public Property oid_maquina() As String
            Get
                Return _oid_maquina
            End Get
            Set(value As String)
                _oid_maquina = value
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

        Public Property considerarecuento() As Boolean
            Get
                Return _considerarecuento
            End Get
            Set(value As Boolean)
                _considerarecuento = value
            End Set
        End Property

        Public Property multicliente() As Boolean
            Get
                Return _multicliente
            End Get
            Set(value As Boolean)
                _multicliente = value
            End Set
        End Property


    End Class
End Namespace