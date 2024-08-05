Namespace Login.EjecutarLogin

    ''' <summary>
    ''' Sector
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno.costa]  18/01/2011  Criado
    ''' </history>
    <Serializable()> _
    Public Class Sector

#Region " Variáveis "

        Private _Identificador As String
        Private _Codigo As String
        Private _Roles As New List(Of Role)
        Private _Permisos As New List(Of Permiso)
        Private _Supervisores As New List(Of Supervisor)

#End Region

#Region "Propriedades"

        Public Property Identificador() As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                _Identificador = value
            End Set
        End Property

        Public Property Codigo() As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                _Codigo = value
            End Set
        End Property

        Public Property Roles() As List(Of Role)
            Get
                Return _Roles
            End Get
            Set(value As List(Of Role))
                _Roles = value
            End Set
        End Property

        Public Property Permisos() As List(Of Permiso)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of Permiso))
                _Permisos = value
            End Set
        End Property

        Public Property Supervisores() As List(Of Supervisor)
            Get
                Return _Supervisores
            End Get
            Set(value As List(Of Supervisor))
                _Supervisores = value
            End Set
        End Property

#End Region

    End Class

End Namespace