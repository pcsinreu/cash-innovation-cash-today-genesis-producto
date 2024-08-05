Namespace IngresoContado

    <Serializable()> _
    Public Class Termino

#Region "[VARIÁVEIS]"

        Private _CodigoTermino As String
        Private _Descripcion As String
        Private _ValorDescripcion As String
        Private _ValorCodigo As String
        Private _MostrarCodigo As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                _Descripcion = value
            End Set
        End Property

        Public Property ValorDescripcion() As String
            Get
                Return _ValorDescripcion
            End Get
            Set(value As String)
                _ValorDescripcion = value
            End Set
        End Property

        Public Property ValorCodigo() As String
            Get
                Return _ValorCodigo
            End Get
            Set(value As String)
                _ValorCodigo = value
            End Set
        End Property

        Public Property MostrarCodigo() As Boolean
            Get
                Return _MostrarCodigo
            End Get
            Set(value As Boolean)
                _MostrarCodigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace