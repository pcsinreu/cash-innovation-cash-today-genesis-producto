Namespace IngresoContado

    ''' <summary>
    ''' Classe TerminoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoMedioPago

        Private _Codigo As String
        Private _Descripcion As String
        Private _ValorDescripcion As String
        Private _ValorCodigo As String
        Private _MostrarCodigo As Boolean

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
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

        Public Property MostrarCodigo() As Boolean
            Get
                Return _MostrarCodigo
            End Get
            Set(value As Boolean)
                _MostrarCodigo = value
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

        Public Property ValorDescripcion() As String
            Get
                Return _ValorDescripcion
            End Get
            Set(value As String)
                _ValorDescripcion = value
            End Set
        End Property

    End Class

End Namespace