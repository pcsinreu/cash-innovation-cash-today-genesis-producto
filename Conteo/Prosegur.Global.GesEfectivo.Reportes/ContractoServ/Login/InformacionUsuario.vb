Namespace Login

    ''' <summary>
    ''' InformacionUsuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [magnum.oliveira] 15/07/2009 Criado
    ''' [gustavo.fraga] 15/03/2011 Alterado
    ''' </history>
    <Serializable()> _
    Public Class InformacionUsuario

#Region "Construtor"

        Public Sub New()

            _Delegaciones = New List(Of Delegacion)

        End Sub

#End Region

#Region " Variáveis "

        Private _Nombre As String
        Private _Apelido As String
        Private _Delegaciones As List(Of Delegacion)
        Private _DelegacionLogin As Delegacion

#End Region

#Region " Propriedades "

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Apelido() As String
            Get
                Return _Apelido
            End Get
            Set(value As String)
                _Apelido = value
            End Set
        End Property

        Public Property Delegaciones() As List(Of Delegacion)
            Get
                Return _Delegaciones
            End Get
            Set(value As List(Of Delegacion))
                _Delegaciones = value
            End Set
        End Property

        Public Property DelegacionLogin() As Delegacion
            Get
                Return _DelegacionLogin
            End Get
            Set(value As Delegacion)
                _DelegacionLogin = value
            End Set
        End Property

#End Region

    End Class

End Namespace
