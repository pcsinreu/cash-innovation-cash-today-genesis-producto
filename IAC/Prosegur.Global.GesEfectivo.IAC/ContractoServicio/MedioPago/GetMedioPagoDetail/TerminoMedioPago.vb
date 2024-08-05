
Namespace MedioPago.GetMedioPagoDetail
    ''' <summary>
    ''' Classe TerminoMedioPago
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 19/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoMedioPago

#Region "[Variáveis]"

        'Termino
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String

        Private _ValorInicial As String

        Private _Vigente As Boolean
        Private _Longitud As Nullable(Of Integer)
        Private _MostrarCodigo As Boolean
        Private _OrdenTermino As Integer

        'Formato
        Private _CodigoFormato As String
        Private _DescripcionFormato As String

        'Mascara
        Private _CodigoMascara As String
        Private _DescripcionMascara As String
        Private _ExpRegularMascara As String

        'Algoritmo
        Private _CodigoAlgoritmo As String
        Private _DescripcionAlgoritmo As String

        'Valores Termino
        Private _ValoresTermino As ValorTerminoColeccion

#End Region

#Region "[Propriedades]"

        'Termino
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

        Public Property Observacion() As String
            Get
                Return _Observacion
            End Get
            Set(value As String)
                _Observacion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property Longitud() As Nullable(Of Integer)
            Get
                Return _Longitud
            End Get
            Set(value As Nullable(Of Integer))
                _Longitud = value
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

        Public Property OrdenTermino() As Integer
            Get
                Return _OrdenTermino
            End Get
            Set(value As Integer)
                _OrdenTermino = value
            End Set
        End Property

        Public Property ValorInicial() As String
            Get
                Return _ValorInicial
            End Get
            Set(value As String)
                _ValorInicial = value
            End Set
        End Property


        'Formato
        Public Property CodigoFormato() As String
            Get
                Return _CodigoFormato
            End Get
            Set(value As String)
                _CodigoFormato = value
            End Set
        End Property

        Public Property DescripcionFormato() As String
            Get
                Return _DescripcionFormato
            End Get
            Set(value As String)
                _DescripcionFormato = value
            End Set
        End Property

        'Mascara
        Public Property CodigoMascara() As String
            Get
                Return _CodigoMascara
            End Get
            Set(value As String)
                _CodigoMascara = value
            End Set
        End Property

        Public Property DescripcionMascara() As String
            Get
                Return _DescripcionMascara
            End Get
            Set(value As String)
                _DescripcionMascara = value
            End Set
        End Property

        Public Property ExpRegularMascara() As String
            Get
                Return _ExpRegularMascara
            End Get
            Set(value As String)
                _ExpRegularMascara = value
            End Set
        End Property

        'Algoritmo
        Public Property CodigoAlgoritmo() As String
            Get
                Return _CodigoAlgoritmo
            End Get
            Set(value As String)
                _CodigoAlgoritmo = value
            End Set
        End Property

        Public Property DescripcionAlgoritmo() As String
            Get
                Return _DescripcionAlgoritmo
            End Get
            Set(value As String)
                _DescripcionAlgoritmo = value
            End Set
        End Property

        'Valores Terminos
        Public Property ValoresTermino() As ValorTerminoColeccion
            Get
                Return _ValoresTermino
            End Get
            Set(value As ValorTerminoColeccion)
                _ValoresTermino = value
            End Set
        End Property

#End Region

    End Class

End Namespace