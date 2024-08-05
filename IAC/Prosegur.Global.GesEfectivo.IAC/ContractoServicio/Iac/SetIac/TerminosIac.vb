Namespace Iac.SetIac

    Public Class TerminosIac

#Region "Variáveis"

        Private _codigoTermino As String
        Private _descripcionTermino As String
        Private _esBusquedaParcial As Boolean
        Private _esCampoClave As Boolean
        Private _esObligatorio As Boolean
        Private _ordenTermino As Integer
        Private _esTerminoCopia As Boolean
        Private _EsProtegido As Boolean
        Private _esInvisibleRpte As Boolean
        Private _esIdMecanizado As Boolean

#End Region

#Region "Propriedades"
        Public Property CodigoTermino() As String
            Get
                Return _codigoTermino
            End Get
            Set(value As String)
                _codigoTermino = value
            End Set
        End Property

        Public Property DescripcionTermino() As String
            Get
                Return _descripcionTermino
            End Get
            Set(value As String)
                _descripcionTermino = value
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

        Public Property EsTerminoCopia() As Boolean
            Get
                Return _esTerminoCopia
            End Get
            Set(value As Boolean)
                _esTerminoCopia = value
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