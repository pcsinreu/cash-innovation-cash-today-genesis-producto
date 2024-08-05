Namespace Iac

    <Serializable()> _
 Public Class Iac
#Region "Variáveis"
        Private _codidoIAC As String
        Private _descripcionIac As String
        Private _observacionesIac As String
        Private _vigente As Boolean
        Private _terminosiac As TerminosIacColeccion

#End Region

#Region "Propriedades"

        Public Property CodidoIac() As String
            Get
                Return _codidoIAC
            End Get
            Set(ByVal value As String)
                _codidoIAC = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(ByVal value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property ObservacionesIac() As String
            Get
                Return _observacionesIac
            End Get
            Set(ByVal value As String)
                _observacionesIac = value
            End Set
        End Property

        Public Property vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(ByVal value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property TerminosIac() As TerminosIacColeccion
            Get
                Return _terminosiac
            End Get
            Set(ByVal value As TerminosIacColeccion)
                _terminosiac = value
            End Set
        End Property

#End Region
    End Class

End Namespace