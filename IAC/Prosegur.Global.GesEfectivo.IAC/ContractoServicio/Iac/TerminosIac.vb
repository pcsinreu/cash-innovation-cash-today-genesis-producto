Namespace Iac

    <Serializable()> _
    Public Class TerminosIac

#Region "Variáveis"

        Private _codigoTermino As String
        Private _descripcionTermino As String
        Private _observacionesTermino As String
        Private _codigoFormatoTermino As String
        Private _descripcionFormatoTermino As String
        Private _longitudTermino As Integer
        Private _codigoMascaraTermino As String
        Private _descripcionMascaraTermino As String
        Private _codigoAlgoritmoTermino As String
        Private _descripcionAlgoritmoTermino As String
        Private _mostarCodigo As Boolean
        Private _admiteValoresPosibles As Boolean
        Private _vigenteTermino As Boolean
        Private _esBusquedaParcial As Boolean
        Private _esCampoClave As Boolean
        Private _ordenTermino As Integer


#End Region

#Region "Propriedades"
        Public Property CodigoTermino() As String
            Get
                Return _codigoTermino
            End Get
            Set(ByVal value As String)
                _codigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _descripcionTermino
            End Get
            Set(ByVal value As String)
                _descripcionTermino = value
            End Set
        End Property

        Public Property ObservacionesTermino() As String
            Get
                Return _observacionesTermino
            End Get
            Set(ByVal value As String)
                _observacionesTermino = value
            End Set
        End Property

        Public Property CodigoFormatoTermino() As String
            Get
                Return _codigoFormatoTermino
            End Get
            Set(ByVal value As String)
                _codigoFormatoTermino = value
            End Set
        End Property

        Public Property DescripcionFormatoTermino() As String
            Get
                Return _descripcionFormatoTermino
            End Get
            Set(ByVal value As String)
                _descripcionFormatoTermino = value
            End Set
        End Property

        Public Property LongitudTermino() As Integer
            Get
                Return _longitudTermino
            End Get
            Set(ByVal value As Integer)
                _longitudTermino = value
            End Set
        End Property

        Public Property CodigoMascaraTermino() As String
            Get
                Return _codigoMascaraTermino
            End Get
            Set(ByVal value As String)
                _codigoMascaraTermino = value
            End Set
        End Property

        Public Property DescripcionMascaraTermino() As String
            Get
                Return _descripcionMascaraTermino
            End Get
            Set(ByVal value As String)
                _descripcionMascaraTermino = value
            End Set
        End Property

        Public Property CodigoAlgoritmoTermino() As String
            Get
                Return _codigoAlgoritmoTermino
            End Get
            Set(ByVal value As String)
                _codigoAlgoritmoTermino = value
            End Set
        End Property

        Public Property DescripcionAlgoritmoTermino() As String
            Get
                Return _descripcionAlgoritmoTermino
            End Get
            Set(ByVal value As String)
                _descripcionAlgoritmoTermino = value
            End Set
        End Property

        Public Property MostarCodigo() As Boolean
            Get
                Return _mostarCodigo
            End Get
            Set(ByVal value As Boolean)
                _mostarCodigo = value
            End Set
        End Property

        Public Property AdmiteValoresPosibles() As Boolean
            Get
                Return _admiteValoresPosibles
            End Get
            Set(ByVal value As Boolean)
                _admiteValoresPosibles = value
            End Set
        End Property

        Public Property VigenteTermino() As Boolean
            Get
                Return _vigenteTermino
            End Get
            Set(ByVal value As Boolean)
                _vigenteTermino = value
            End Set
        End Property

        Public Property EsBusquedaParcial() As Boolean
            Get
                Return _esBusquedaParcial
            End Get
            Set(ByVal value As Boolean)
                _esBusquedaParcial = value
            End Set
        End Property

        Public Property EsCampoClave() As Boolean
            Get
                Return _esCampoClave
            End Get
            Set(ByVal value As Boolean)
                _esCampoClave = value
            End Set
        End Property

        Public Property OrdenTermino() As Integer
            Get
                Return _ordenTermino
            End Get
            Set(ByVal value As Integer)
                _ordenTermino = value
            End Set
        End Property
#End Region


    End Class
End Namespace