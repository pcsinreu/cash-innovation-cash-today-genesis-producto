Namespace GetProcesos

    <Serializable()> _
    Public Class Iac

#Region "[VARIÁVEIS]"

        Private _codigo As String
        Private _descripcion As String
        Private _observacion As String
        Private _vigente As Boolean
        Private _terminos As TerminoIacColeccion

#End Region

#Region "[PROPRIEDADES]"

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

        Public Property Observacion() As String
            Get
                Return _observacion
            End Get
            Set(value As String)
                _observacion = value
            End Set
        End Property

        Public Property Vigente() As Boolean
            Get
                Return _vigente
            End Get
            Set(value As Boolean)
                _vigente = value
            End Set
        End Property

        Public Property Terminos() As TerminoIacColeccion
            Get
                Return _terminos
            End Get
            Set(value As TerminoIacColeccion)
                _terminos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
