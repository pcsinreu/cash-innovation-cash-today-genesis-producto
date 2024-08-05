Namespace TerminosIac.GetTerminosIac

    <Serializable()> _
    Public Class TerminosIac

#Region "Variáveis"

        Private _codigoTermino As String
        Private _descripcionTermino As String
        Private _observacionesTermino As String
        Private _vigente As String


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

        Public Property Tigente() As String
            Get
                Return _vigente
            End Get
            Set(ByVal value As String)
                _vigente = value
            End Set
        End Property
#End Region

    End Class
End Namespace
