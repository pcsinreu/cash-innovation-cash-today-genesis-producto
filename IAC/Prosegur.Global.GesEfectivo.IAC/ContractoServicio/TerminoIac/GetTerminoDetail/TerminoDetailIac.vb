
Namespace TerminoIac.GetTerminoDetailIac
    ''' <summary>
    ''' Classe TerminoDetailIac
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pda] 13/02/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class TerminoDetailIac

#Region "[Variáveis]"

        'Termino
        Private _Codigo As String
        Private _Descripcion As String
        Private _Observacion As String
        Private _Vigente As Boolean
        Private _Longitud As Integer
        Private _MostrarCodigo As Boolean
        Private _ValoresPosibles As Boolean
        Private _AceptarDigitiacion As Boolean

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

#End Region

#Region "[Propriedades]"

        Public Property AceptarDigitiacion() As Boolean
            Get
                Return _AceptarDigitiacion
            End Get
            Set(value As Boolean)
                _AceptarDigitiacion = value
            End Set
        End Property

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
        Public Property Longitud() As Integer
            Get
                Return _Longitud
            End Get
            Set(value As Integer)
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

        Public Property ValoresPosibles() As Boolean
            Get
                Return _ValoresPosibles
            End Get
            Set(value As Boolean)
                _ValoresPosibles = value
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

#End Region

    End Class

End Namespace