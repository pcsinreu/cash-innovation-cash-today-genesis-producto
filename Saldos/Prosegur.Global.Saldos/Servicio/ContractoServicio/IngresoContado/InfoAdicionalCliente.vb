Namespace IngresoContado

    ''' <summary>
    ''' Classe InfoAdicionalCliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [octavio.piramo] 10/09/2009 - Criado
    ''' </history>
    <Serializable()> _
    Public Class InfoAdicionalCliente

#Region "Variáveis"

        Private _CodigoTermino As String
        Private _DescripcionTermino As String
        Private _DescripcionValor As String
        Private _CodigoValor As String
        Private _MostarCodigo As Boolean

#End Region

#Region "Propriedades"

        Public Property CodigoValor() As String
            Get
                Return _CodigoValor
            End Get
            Set(value As String)
                _CodigoValor = value
            End Set
        End Property

        Public Property DescripcionValor() As String
            Get
                Return _DescripcionValor
            End Get
            Set(value As String)
                _DescripcionValor = value
            End Set
        End Property

        Public Property CodigoTermino() As String
            Get
                Return _CodigoTermino
            End Get
            Set(value As String)
                _CodigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _DescripcionTermino
            End Get
            Set(value As String)
                _DescripcionTermino = value
            End Set
        End Property

        Public Property MostarCodigo() As Boolean
            Get
                Return _MostarCodigo
            End Get
            Set(value As Boolean)
                _MostarCodigo = value
            End Set
        End Property

#End Region

    End Class

End Namespace

