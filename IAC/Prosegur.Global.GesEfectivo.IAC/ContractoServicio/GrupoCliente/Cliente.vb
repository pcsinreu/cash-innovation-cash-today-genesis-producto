Namespace GrupoCliente

    ''' <summary>
    ''' Classe ClienteDetalle
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [matheus.araujo] 24/10/2012 - Criado
    ''' </history>
    <Serializable()> _
    Public Class Cliente

#Region "[VARIÁVEIS]"


        Private _CodCliente As String
        Private _DesCliente As String
        Private _SubClientes As SubClienteColeccion
        Private _OidGrupoClienteDetalle As String
        Private _OidCliente As String
        Private _bolBaja As Boolean


#End Region

#Region "[PROPRIEDADES]"

        Public Property CodCliente As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property DesCliente As String
            Get
                Return _DesCliente
            End Get
            Set(value As String)
                _DesCliente = value
            End Set
        End Property

        Public Property SubClientes As SubClienteColeccion
            Get
                Return _SubClientes
            End Get
            Set(value As SubClienteColeccion)
                _SubClientes = value
            End Set
        End Property

        Public Property OidGrupoClienteDetalle As String
            Get
                Return _OidGrupoClienteDetalle
            End Get
            Set(value As String)
                _OidGrupoClienteDetalle = value
            End Set
        End Property

        Public Property OidCliente As String
            Get
                Return _OidCliente
            End Get
            Set(value As String)
                _OidCliente = value
            End Set
        End Property

        Public Property bolBaja As Boolean
            Get
                Return _bolBaja
            End Get
            Set(value As Boolean)
                _bolBaja = value
            End Set
        End Property

#End Region

    End Class

End Namespace