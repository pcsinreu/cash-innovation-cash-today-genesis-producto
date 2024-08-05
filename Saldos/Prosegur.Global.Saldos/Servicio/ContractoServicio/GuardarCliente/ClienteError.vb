Namespace GuardarCliente

    ''' <summary>
    ''' Classe ClienteErro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class ClienteError


#Region "[VARIAVEIS]"

        Private _CodCliente As String
        Private _DescripcionError As String
        Private _SubClientes As SubClienteErrorColeccion

#End Region

#Region "[METODOS]"

        Public Property DescripcionError() As String
            Get
                Return _DescripcionError
            End Get
            Set(value As String)
                _DescripcionError = value
            End Set
        End Property

        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property SubClientes() As SubClienteErrorColeccion
            Get
                Return _SubClientes
            End Get
            Set(value As SubClienteErrorColeccion)
                _SubClientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace
