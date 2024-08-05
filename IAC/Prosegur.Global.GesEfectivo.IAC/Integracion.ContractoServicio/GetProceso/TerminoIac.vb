Namespace GetProceso

    <Serializable()> _
    Public Class TerminoIac

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _observaciones As String
        Private _codigoFormato As String
        Private _descripcionFormato As String
        Private _longitud As Integer
        Private _codigoMascara As String
        Private _descripcionMascara As String
        Private _expresionRegularMascara As String
        Private _codigoAlgoritnoValidacionIac As String
        Private _descripcionAlgoritmoValidacionIac As String
        Private _mostarCodigo As Boolean
        Private _busquedaParcial As Boolean
        Private _campoClave As Boolean
        Private _esObligatorio As Boolean
        Private _esProtegido As Boolean
        Private _orden As Integer
        Private _valoresPosibles As ValorPosibleColeccion
        Private _esTerminoCopia As Boolean
        Private _aceptarDigitacion As Boolean
        Private _esInvisibleRpte As Boolean
        Private _esIdMecanizado As Boolean

#End Region

#Region "Propriedades"

        Public Property AceptarDigitacion() As Boolean
            Get
                Return _aceptarDigitacion
            End Get
            Set(value As Boolean)
                _aceptarDigitacion = value
            End Set
        End Property

        Public Property esProtegido() As Boolean
            Get
                Return _esProtegido
            End Get
            Set(value As Boolean)
                _esProtegido = value
            End Set
        End Property

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

        Public Property EsTerminoCopia() As Boolean
            Get
                Return _esTerminoCopia
            End Get
            Set(value As Boolean)
                _esTerminoCopia = value
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

        Public Property Longitud() As Integer
            Get
                Return _longitud
            End Get
            Set(value As Integer)
                _longitud = value
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

        Public Property ExpresionRegularMascara() As String
            Get
                Return _expresionRegularMascara
            End Get
            Set(value As String)
                _expresionRegularMascara = value
            End Set
        End Property

        Public Property CodigoAlgoritnoValidacionIac() As String
            Get
                Return _codigoAlgoritnoValidacionIac
            End Get
            Set(value As String)
                _codigoAlgoritnoValidacionIac = value
            End Set
        End Property

        Public Property DescripcionAlgoritmoValidacionIac() As String
            Get
                Return _descripcionAlgoritmoValidacionIac
            End Get
            Set(value As String)
                _descripcionAlgoritmoValidacionIac = value
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

        Public Property BusquedaParcial() As Boolean
            Get
                Return _busquedaParcial
            End Get
            Set(value As Boolean)
                _busquedaParcial = value
            End Set
        End Property

        Public Property CampoClave() As Boolean
            Get
                Return _campoClave
            End Get
            Set(value As Boolean)
                _campoClave = value
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

        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(value As Integer)
                _orden = value
            End Set
        End Property

        Public Property ValoresPosibles() As ValorPosibleColeccion
            Get
                Return _valoresPosibles
            End Get
            Set(value As ValorPosibleColeccion)
                _valoresPosibles = value
            End Set
        End Property

        Public Property esInvisibleRpte() As Boolean
            Get
                Return _esInvisibleRpte
            End Get
            Set(value As Boolean)
                _esInvisibleRpte = value
            End Set
        End Property

        Public Property esIdMecanizado() As Boolean
            Get
                Return _esIdMecanizado
            End Get
            Set(value As Boolean)
                _esIdMecanizado = value
            End Set
        End Property

#End Region

    End Class

End Namespace