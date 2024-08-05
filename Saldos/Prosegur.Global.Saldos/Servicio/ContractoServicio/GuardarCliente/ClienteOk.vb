Namespace GuardarCliente

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' </history>
    <Serializable()> _
    Public Class ClienteOk

#Region "[VARIAVEIS]"

        Private _CodCliente As String
        Private _SubClientes As SubClienteOkColeccion

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

        Public Property SubClientes() As SubClienteOkColeccion
            Get
                Return _SubClientes
            End Get
            Set(value As SubClienteOkColeccion)
                _SubClientes = value
            End Set
        End Property

#End Region

    End Class

End Namespace