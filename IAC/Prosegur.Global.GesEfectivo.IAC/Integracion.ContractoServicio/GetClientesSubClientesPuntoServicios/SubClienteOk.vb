Namespace GetClientesSubClientesPuntoServicios

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

        Private _CodCliente As String
        Private _CodSubCliente As String

#End Region

#Region "[METODOS]"

        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

#End Region

    End Class

End Namespace