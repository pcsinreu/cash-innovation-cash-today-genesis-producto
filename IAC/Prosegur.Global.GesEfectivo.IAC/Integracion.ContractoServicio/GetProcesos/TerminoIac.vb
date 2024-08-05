Namespace GetProcesos

    <Serializable()> _
    Public Class TerminoIac

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _observacion As String
        Private _valorInicial As String
        Private _codigoFormato As String
        Private _descripcionFormato As String
        Private _longitud As Integer
        Private _codigoMascara As String
        Private _descripcionMascara As String
        Private _expRegularMascaraTerminoIAC As String
        Private _codigoAlgValidacion As String
        Private _descripcionAlgValidacion As String
        Private _mostrarCodigo As Boolean
        Private _busquedaParcial As Boolean
        Private _campoClave As Boolean
        Private _esObligatorioTermino As Boolean
        Private _esProtegidoTermino As Boolean
        Private _orden As Integer
        Private _vigente As Boolean
        Private _valoresPosibles As ValorPosibleColeccion

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property Observacion() As String
            Get
                Return _observacion
            End Get
            Set(value As String)
                _observacion = value
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

        Public Property ExpRegularMascaraTerminoIAC() As String
            Get
                Return _expRegularMascaraTerminoIAC
            End Get
            Set(value As String)
                _expRegularMascaraTerminoIAC = value
            End Set
        End Property

        Public Property CodigoAlgValidacion() As String
            Get
                Return _codigoAlgValidacion
            End Get
            Set(value As String)
                _codigoAlgValidacion = value
            End Set
        End Property

        Public Property DescripcionAlgValidacion() As String
            Get
                Return _descripcionAlgValidacion
            End Get
            Set(value As String)
                _descripcionAlgValidacion = value
            End Set
        End Property

        Public Property MostrarCodigo() As Boolean
            Get
                Return _mostrarCodigo
            End Get
            Set(value As Boolean)
                _mostrarCodigo = value
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

        Public Property EsObligatorioTermino() As Boolean
            Get
                Return _esObligatorioTermino
            End Get
            Set(value As Boolean)
                _esObligatorioTermino = value
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

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
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

        Public Property esProtegidoTermino() As Boolean
            Get
                Return _esProtegidoTermino
            End Get
            Set(value As Boolean)
                _esProtegidoTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace
