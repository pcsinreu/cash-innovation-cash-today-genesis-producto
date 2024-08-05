Namespace GuardarCliente

    ''' <summary>
    ''' Classe SubClienteError
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class SubClienteError

#Region "[VARIAVEIS]"

        Private _CodSubCliente As String
        Private _DescripcionError As String
        Private _PuntosServicio As PuntoServicioErrorColeccion

#End Region

#Region "[METODOS]"

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property DescripcionError() As String
            Get
                Return _DescripcionError
            End Get
            Set(value As String)
                _DescripcionError = value
            End Set
        End Property

        Public Property PuntosServicio() As PuntoServicioErrorColeccion
            Get
                Return _PuntosServicio
            End Get
            Set(value As PuntoServicioErrorColeccion)
                _PuntosServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace