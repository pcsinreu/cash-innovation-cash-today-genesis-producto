Namespace GetIac

    <Serializable()> _
    Public Class TerminoIac
#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _vigente As Boolean
        Private _codigoFormatoTermino As String
        Private _descripcionFormatoTermino As String
        Private _longitudTermino As Integer
        Private _codigoMascaraTermino As String
        Private _descripcionMascaraTermino As String
        Private _expRegularMascara As String
        Private _codigoAlgoritmoTermino As String
        Private _descripcionAlgoritmoTermino As String
        Private _mostarCodigo As Boolean
        Private _admiteValoresPosibles As Boolean
        Private _esBusquedaParcial As Boolean
        Private _esCampoClave As Boolean
        Private _esObligatorio As Boolean
        Private _ordenTermino As Integer
        Private _EsProtegido As Boolean

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

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property CodigoFormatoTermino() As String
            Get
                Return _codigoFormatoTermino
            End Get
            Set(value As String)
                _codigoFormatoTermino = value
            End Set
        End Property

        Public Property DescripcionFormatoTermino() As String
            Get
                Return _descripcionFormatoTermino
            End Get
            Set(value As String)
                _descripcionFormatoTermino = value
            End Set
        End Property

        Public Property LongitudTermino() As Integer
            Get
                Return _longitudTermino
            End Get
            Set(value As Integer)
                _longitudTermino = value
            End Set
        End Property

        Public Property CodigoMascaraTermino() As String
            Get
                Return _codigoMascaraTermino
            End Get
            Set(value As String)
                _codigoMascaraTermino = value
            End Set
        End Property

        Public Property DescripcionMascaraTermino() As String
            Get
                Return _descripcionMascaraTermino
            End Get
            Set(value As String)
                _descripcionMascaraTermino = value
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

        Public Property CodigoAlgoritmoTermino() As String
            Get
                Return _codigoAlgoritmoTermino
            End Get
            Set(value As String)
                _codigoAlgoritmoTermino = value
            End Set
        End Property

        Public Property DescripcionAlgoritmoTermino() As String
            Get
                Return _descripcionAlgoritmoTermino
            End Get
            Set(value As String)
                _descripcionAlgoritmoTermino = value
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

        Public Property AdmiteValoresPosibles() As Boolean
            Get
                Return _admiteValoresPosibles
            End Get
            Set(value As Boolean)
                _admiteValoresPosibles = value
            End Set
        End Property

        Public Property EsBusquedaParcial() As Boolean
            Get
                Return _esBusquedaParcial
            End Get
            Set(value As Boolean)
                _esBusquedaParcial = value
            End Set
        End Property

        Public Property EsCampoClave() As Boolean
            Get
                Return _esCampoClave
            End Get
            Set(value As Boolean)
                _esCampoClave = value
            End Set
        End Property

        Public Property EsObligatorio() As Boolean
            Get
                Return _esObligatorio
            End Get
            Set(value As Boolean)
                _esObligatorio = value
            End Set
        End Property

        Public Property OrdenTermino() As Integer
            Get
                Return _ordenTermino
            End Get
            Set(value As Integer)
                _ordenTermino = value
            End Set
        End Property


        Public Property EsProtegido() As Boolean
            Get
                Return _EsProtegido
            End Get
            Set(value As Boolean)
                _EsProtegido = value
            End Set
        End Property

#End Region

    End Class
End Namespace
