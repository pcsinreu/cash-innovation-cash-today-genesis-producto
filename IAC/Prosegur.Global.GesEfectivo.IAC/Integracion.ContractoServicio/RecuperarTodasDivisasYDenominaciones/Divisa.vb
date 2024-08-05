Namespace RecuperarTodasDivisasYDenominaciones

    ''' <summary>
    ''' Classe divisa
    ''' </summary>
    ''' <remarks></remarks>

    <Serializable()> _
    Public Class Divisa

#Region "[Variáveis]"

        Private _CodigoIso As String
        Private _OidDivisa As String
        Private _Descripcion As String
        Private _CodigoSimbolo As String
        Private _Vigente As Nullable(Of Boolean)
        Private _CodigoUsuario As String
        Private _FechaActualizacion As DateTime
        Private _colorDivisa As String
        Private _codigoAccesoDivisa As String
        Private _Denominaciones As DenominacionColeccion

#End Region

#Region "[Propriedades]"
        'Public Property CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase

        Public Property CodigoAccesoDivisa() As String
            Get
                Return _codigoAccesoDivisa
            End Get
            Set(value As String)
                _codigoAccesoDivisa = value
            End Set
        End Property

        Public Property CodigoIso() As String
            Get
                Return _CodigoIso
            End Get
            Set(value As String)
                _CodigoIso = value
            End Set
        End Property

        Public Property OidDivisa As String
            Get
                Return _OidDivisa
            End Get
            Set(value As String)
                _OidDivisa = value
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

        Public Property ColorDivisa() As String
            Get
                Return _colorDivisa
            End Get
            Set(value As String)
                _colorDivisa = value
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

        Public Property CodigoSimbolo() As String
            Get
                Return _CodigoSimbolo
            End Get
            Set(value As String)
                _CodigoSimbolo = value
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

        Public Property Vigente() As Nullable(Of Boolean)
            Get
                Return _Vigente
            End Get
            Set(value As Nullable(Of Boolean))
                _Vigente = value
            End Set
        End Property

        Public Property Denominaciones() As DenominacionColeccion
            Get
                Return _Denominaciones
            End Get
            Set(value As DenominacionColeccion)
                _Denominaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace