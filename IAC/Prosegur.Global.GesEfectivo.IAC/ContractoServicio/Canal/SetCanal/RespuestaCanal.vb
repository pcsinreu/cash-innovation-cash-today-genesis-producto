Namespace Canal.SetCanal

    <Serializable()> _
    Public Class RespuestaCanal

        Private _codigoCanal As String
        Private _descripcionCanal As String
        Private _CodigoError As String
        Private _MensajeError As String
        Private _Resultado As Integer
        Private _codigoAjeno As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuestaColeccion

        Public Property CodigoCanal() As String
            Get
                Return _codigoCanal
            End Get
            Set(value As String)
                _codigoCanal = value
            End Set
        End Property

        Public Property DescripcionCanal() As String
            Get
                Return _descripcionCanal
            End Get
            Set(value As String)
                _descripcionCanal = value
            End Set
        End Property

        Public Property CodigoError() As Integer
            Get
                Return _CodigoError
            End Get
            Set(value As Integer)
                _CodigoError = value
            End Set
        End Property

        Public Property MensajeError() As String
            Get
                Return _MensajeError
            End Get
            Set(value As String)
                _MensajeError = value
            End Set
        End Property

        Public Property Resultado() As Integer
            Get
                Return _Resultado
            End Get
            Set(value As Integer)
                _Resultado = value
            End Set
        End Property

        Public Property CodigosAjenos As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuestaColeccion
            Get
                Return _codigoAjeno
            End Get
            Set(value As ContractoServicio.CodigoAjeno.SetCodigosAjenos.CodigoAjenoRespuestaColeccion)
                _codigoAjeno = value
            End Set
        End Property
    End Class

End Namespace