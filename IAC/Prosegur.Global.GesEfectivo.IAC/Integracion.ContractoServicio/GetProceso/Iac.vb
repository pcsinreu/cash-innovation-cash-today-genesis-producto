Namespace GetProceso

    <Serializable()> _
    Public Class Iac

#Region "Variáveis"

        Private _codigo As String
        Private _descripcion As String
        Private _terminosIac As TerminoIacColeccion
        Private _esDeclaradoCopia As Boolean
        Private _esInvisible As Boolean
        Private _especificoSaldos As Boolean

#End Region

#Region "Propriedades"
        Public Property Codigo() As String
            Get
                Return _codigo
            End Get
            Set(value As String)
                _codigo = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
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

        Public Property TerminosIac() As TerminoIacColeccion
            Get
                Return _terminosIac
            End Get
            Set(value As TerminoIacColeccion)
                _terminosIac = value
            End Set
        End Property

#End Region

    End Class

End Namespace