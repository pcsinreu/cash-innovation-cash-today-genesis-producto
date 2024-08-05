Namespace Parametro.GetParametrosValues
    <Serializable()> _
    Public Class Parametro

        Public Sub New()
            _ParametroOpciones = New List(Of ParametroOpciones)
        End Sub

#Region "Variáveis"
        Private _CodigoParametro As String
        Private _DescripcionCorto As String
        Private _DescripcionLarga As String
        Private _NecOrden As Integer
        Private _BolObligatorio As Boolean
        Private _NecTipoComponente As TipoComponente
        Private _NecTipoDato As TipoDato
        Private _ValorParametro As String
        Private _ParametroOpciones As List(Of ParametroOpciones)
#End Region

#Region "Propriedades"
        Public Property CodigoParametro() As String
            Get
                Return _CodigoParametro
            End Get
            Set(value As String)
                _CodigoParametro = value
            End Set
        End Property

        Public Property DescripcionCorto() As String
            Get
                Return _DescripcionCorto
            End Get
            Set(value As String)
                _DescripcionCorto = value
            End Set
        End Property

        Public Property DescripcionLarga() As String
            Get
                Return _DescripcionLarga
            End Get
            Set(value As String)
                _DescripcionLarga = value
            End Set
        End Property

        Public Property NecOrden() As Integer
            Get
                Return _NecOrden
            End Get
            Set(value As Integer)
                _NecOrden = value
            End Set
        End Property

        Public Property BolObligatorio() As Boolean
            Get
                Return _BolObligatorio
            End Get
            Set(value As Boolean)
                _BolObligatorio = value
            End Set
        End Property


        Public Property NecTipoDato() As TipoDato
            Get
                Return _NecTipoDato
            End Get
            Set(value As TipoDato)
                _NecTipoDato = value
            End Set
        End Property

        Public Property NecTipoComponente() As TipoComponente
            Get
                Return _NecTipoComponente
            End Get
            Set(value As TipoComponente)
                _NecTipoComponente = value
            End Set
        End Property


        Public Property ValorParametro() As String
            Get
                Return _ValorParametro
            End Get
            Set(value As String)
                _ValorParametro = value
            End Set
        End Property

        Public Property ParametroOpciones() As List(Of ParametroOpciones)
            Get
                Return _ParametroOpciones
            End Get
            Set(value As List(Of ParametroOpciones))
                _ParametroOpciones = value
            End Set
        End Property

#End Region
    End Class
End Namespace