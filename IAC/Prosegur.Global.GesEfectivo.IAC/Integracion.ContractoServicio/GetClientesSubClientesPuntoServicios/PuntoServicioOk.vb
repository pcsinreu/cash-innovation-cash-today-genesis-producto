Namespace GetClientesSubClientesPuntoServicios

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class PuntoServicioOk

#Region "[VARIAVEIS]"

        Private _CodSubCliente As String
        Private _CodPuntoServicio As String

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

        Public Property CodPuntoServicio() As String
            Get
                Return _CodPuntoServicio
            End Get
            Set(value As String)
                _CodPuntoServicio = value
            End Set
        End Property

#End Region

    End Class

End Namespace