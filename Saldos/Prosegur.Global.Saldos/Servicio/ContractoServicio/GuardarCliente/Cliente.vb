Namespace GuardarCliente

    ''' <summary>
    ''' Classe Cliente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [vinicius.gama] 21/09/2009 Criado
    ''' [vinicius.gama] 22/10/2009 Alterado - Adicionado propriedade Oid, identificador unico
    ''' </history>
    <Serializable()> _
    Public Class Cliente

#Region "[VARIAVEIS]"

        Private _OidCliente As String
        Private _CodCliente As String
        Private _DescripcionCliente As String
        Private _SubClientes As SubClienteColeccion
        Private _Enviado As Boolean

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidCliente() As String
            Get
                Return _OidCliente
            End Get
            Set(value As String)
                _OidCliente = value
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

        Public Property DescripcionCliente() As String
            Get
                Return _DescripcionCliente
            End Get
            Set(value As String)
                _DescripcionCliente = value
            End Set
        End Property

        Public Property SubClientes() As SubClienteColeccion
            Get
                Return _SubClientes
            End Get
            Set(value As SubClienteColeccion)
                _SubClientes = value
            End Set
        End Property

        Public Property Enviado() As Boolean
            Get
                Return _Enviado
            End Get
            Set(value As Boolean)
                _Enviado = value
            End Set
        End Property

#End Region

    End Class

End Namespace