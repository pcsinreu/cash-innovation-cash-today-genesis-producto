Namespace Login

    ''' <summary>
    ''' Delegacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [gustavo.fraga] 15/03/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class TipoSector

#Region " Construtor "

        Public Sub New()

            _Rol = New List(Of String)
            _Permisos = New List(Of String)

        End Sub

#End Region

#Region " Variáveis "

        Private _Rol As List(Of String)
        Private _Permisos As List(Of String)

#End Region

#Region " Propriedades "

        Public Property Rol() As List(Of String)
            Get
                Return _Rol
            End Get
            Set(value As List(Of String))
                _Rol = value
            End Set
        End Property

        Public Property Permisos() As List(Of String)
            Get
                Return _Permisos
            End Get
            Set(value As List(Of String))
                _Permisos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
