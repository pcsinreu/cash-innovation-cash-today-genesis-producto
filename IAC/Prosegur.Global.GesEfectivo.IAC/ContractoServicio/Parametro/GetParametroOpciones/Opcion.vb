Namespace Parametro.GetParametroOpciones
    <Serializable()> _
    Public Class Opcion

        Public Sub New()
            _Parametro = New Parametro.GetParametroDetail.Parametro
        End Sub

#Region "Variáveis"
        Private _CodigoOpcion As String
        Private _DescripcionOpcion As String
        Private _Parametro As Parametro.GetParametroDetail.Parametro
        Private _EsVigente As Boolean
        Private _CodDelegacion As String
#End Region


#Region "Propriedades"
        Public Property CodigoOpcion() As String
            Get
                Return _CodigoOpcion
            End Get
            Set(value As String)
                _CodigoOpcion = value
            End Set
        End Property

        Public Property DescripcionOpcion() As String
            Get
                Return _DescripcionOpcion
            End Get
            Set(value As String)
                _DescripcionOpcion = value
            End Set
        End Property

        Public Property EsVigente() As Boolean
            Get
                Return _EsVigente
            End Get
            Set(value As Boolean)
                _EsVigente = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        Public Property Parametro() As Parametro.GetParametroDetail.Parametro
            Get
                Return _Parametro
            End Get
            Set(value As Parametro.GetParametroDetail.Parametro)
                _Parametro = value
            End Set
        End Property

#End Region
    End Class
End Namespace