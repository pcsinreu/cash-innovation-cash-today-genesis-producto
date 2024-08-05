Namespace RecuperarTodasDivisasYDenominaciones

    ''' <summary>
    ''' Classe denominacion
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Denominacion

#Region "[Variáveis]"
        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EsBillete As Nullable(Of Boolean)
        Private _Valor As Nullable(Of Decimal)
        Private _Peso As Nullable(Of Decimal)
        Private _Vigente As Nullable(Of Boolean)
        Private _CodigoUsuario As String
        Private _FechaActualizacion As DateTime
        Private _codigoAccesoDenominacion As String
        ' Private _codigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        ' Private _codigosAjenosSet As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase

#End Region

#Region "[Propriedades]"
        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property
        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property CodigoAccesoDenominacion() As String
            Get
                Return _codigoAccesoDenominacion
            End Get
            Set(value As String)
                _codigoAccesoDenominacion = value
            End Set
        End Property

        Public Property CodigoUsuario() As String
            Get
                Return _CodigoUsuario
            End Get
            Set(value As String)
                _CodigoUsuario = value
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

        Public Property EsBillete() As Nullable(Of Boolean)
            Get
                Return _EsBillete
            End Get
            Set(value As Nullable(Of Boolean))
                _EsBillete = value
            End Set
        End Property

        Public Property FechaActualizacion() As DateTime
            Get
                Return _FechaActualizacion
            End Get
            Set(value As DateTime)
                _FechaActualizacion = value
            End Set
        End Property

        Public Property Valor() As Nullable(Of Decimal)
            Get
                Return _Valor
            End Get
            Set(value As Nullable(Of Decimal))
                _Valor = value
            End Set
        End Property

        Public Property Peso() As Nullable(Of Decimal)
            Get
                Return _Peso
            End Get
            Set(value As Nullable(Of Decimal))
                _Peso = value
            End Set
        End Property

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

        'Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
        '    Get
        '        Return _codigosAjenos
        '    End Get
        '    Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
        '        _codigosAjenos = value
        '    End Set
        'End Property

        'Public Property CodigosAjenosSet() As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase
        '    Get
        '        Return _codigosAjenosSet
        '    End Get
        '    Set(value As ContractoServicio.CodigoAjeno.CodigoAjenoColeccionBase)
        '        _codigosAjenosSet = value
        '    End Set
        'End Property

#End Region

    End Class

End Namespace