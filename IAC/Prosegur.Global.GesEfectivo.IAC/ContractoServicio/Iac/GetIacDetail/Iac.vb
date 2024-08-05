Namespace Iac.GetIacDetail

    <Serializable()> _
    Public Class Iac
#Region "Variáveis"
        Private _codidoIAC As String
        Private _descripcionIac As String
        Private _observacionesIac As String
        Private _vigente As Boolean
        Private _esDeclaradoCopia As Boolean
        Private _esInvisible As Boolean
        Private _especificoSaldos As Boolean
        Private _terminosiac As TerminosIacColeccion

#End Region

#Region "Propriedades"

        Public Property CodidoIac() As String
            Get
                Return _codidoIAC
            End Get
            Set(value As String)
                _codidoIAC = value
            End Set
        End Property

        Public Property DescripcionIac() As String
            Get
                Return _descripcionIac
            End Get
            Set(value As String)
                _descripcionIac = value
            End Set
        End Property

        Public Property EsDeclaradoCopia() As Boolean
            Get
                Return _esDeclaradoCopia
            End Get
            Set(value As Boolean)
                _esDeclaradoCopia = value
            End Set
        End Property

        Public Property ObservacionesIac() As String
            Get
                Return _observacionesIac
            End Get
            Set(value As String)
                _observacionesIac = value
            End Set
        End Property

        Public Property vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property EsInvisible() As Boolean
            Get
                Return _esInvisible
            End Get
            Set(value As Boolean)
                _esInvisible = value
            End Set
        End Property

        Public Property EspecificoSaldos() As Boolean
            Get
                Return _especificoSaldos
            End Get
            Set(value As Boolean)
                _especificoSaldos = value
            End Set
        End Property

        Public Property TerminosIac() As TerminosIacColeccion
            Get
                Return _terminosiac
            End Get
            Set(value As TerminosIacColeccion)
                _terminosiac = value
            End Set
        End Property

#End Region
    End Class
End Namespace