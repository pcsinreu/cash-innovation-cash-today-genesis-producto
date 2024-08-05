Namespace GetMediosPago

    <Serializable()> _
    Public Class TerminoMedioPago

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _valorInicial As String
        Private _longitud As Integer
        Private _codigoFormato As String
        Private _descripcionFormato As String
        Private _codigoMascara As String
        Private _descripcionMascara As String
        Private _expRegularMascara As String
        Private _codigoAlgoritmo As String
        Private _descripcionAlgoritmo As String
        Private _mostarCodigo As Boolean
        Private _ordenTermino As Integer
        Private _valoresTermino As ValorTerminoColeccion

#End Region

#Region "Propriedades"

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

        Public Property ValorInicial() As String
            Get
                Return _valorInicial
            End Get
            Set(value As String)
                _valorInicial = value
            End Set
        End Property

        Public Property Longitud() As Integer
            Get
                Return _longitud
            End Get
            Set(value As Integer)
                _longitud = value
            End Set
        End Property

        Public Property CodigoFormato() As String
            Get
                Return _codigoFormato
            End Get
            Set(value As String)
                _codigoFormato = value
            End Set
        End Property

        Public Property DescripcionFormato() As String
            Get
                Return _descripcionFormato
            End Get
            Set(value As String)
                _descripcionFormato = value
            End Set
        End Property

        Public Property CodigoMascara() As String
            Get
                Return _codigoMascara
            End Get
            Set(value As String)
                _codigoMascara = value
            End Set
        End Property

        Public Property DescripcionMascara() As String
            Get
                Return _descripcionMascara
            End Get
            Set(value As String)
                _descripcionMascara = value
            End Set
        End Property

        Public Property ExpRegularMascara() As String
            Get
                Return _expRegularMascara
            End Get
            Set(value As String)
                _expRegularMascara = value
            End Set
        End Property

        Public Property CodigoAlgoritmo() As String
            Get
                Return _codigoAlgoritmo
            End Get
            Set(value As String)
                _codigoAlgoritmo = value
            End Set
        End Property

        Public Property DescripcionAlgoritmo() As String
            Get
                Return _descripcionAlgoritmo
            End Get
            Set(value As String)
                _descripcionAlgoritmo = value
            End Set
        End Property

        Public Property MostarCodigo() As Boolean
            Get
                Return _mostarCodigo
            End Get
            Set(value As Boolean)
                _mostarCodigo = value
            End Set
        End Property

        Public Property OrdenTermino() As Boolean
            Get
                Return _ordenTermino
            End Get
            Set(value As Boolean)
                _ordenTermino = value
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

        Public Property ValoresTermino() As ValorTerminoColeccion
            Get
                Return _valoresTermino
            End Get
            Set(value As ValorTerminoColeccion)
                _valoresTermino = value
            End Set
        End Property


#End Region
    End Class
End Namespace