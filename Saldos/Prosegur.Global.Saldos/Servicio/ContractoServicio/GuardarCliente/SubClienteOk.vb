Namespace GuardarCliente

    ''' <summary>
    ''' Classe SubClienteOk
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class SubClienteOk

#Region "[VARIAVEIS]"

        Private _CodSubCliente As String
        Private _PuntosServicio As PuntoServicioOkColeccion

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

        Public Property PuntosServicio() As PuntoServicioOkColeccion
            Get
                Return _PuntosServicio
            End Get
            Set(value As PuntoServicioOkColeccion)
                _PuntosServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace