Namespace GetProcesos

    <Serializable()> _
    Public Class MedioPagoProceso

#Region "[VARIÁVEIS]"

        Private _codigo As String
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
