
Namespace Grupo.GetGrupos

    ''' <summary>
    ''' Classe Grupo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [bruno] 13/01/2011 Criado
    ''' </history>
    <Serializable()> _
    Public Class Grupo

#Region "[Variáveis]"

        Private _oidGrupo As String
        Private _codigoGrupo As String
        Private _descripcionGrupo As String

#End Region

#Region "[Propriedades]"

        Public Property OidGrupo() As String
            Get
                Return _oidGrupo
            End Get
            Set(value As String)
                _oidGrupo = value
            End Set
        End Property

        Public Property CodigoGrupo() As String
            Get
                Return _codigoGrupo
            End Get
            Set(value As String)
                _codigoGrupo = value
            End Set
        End Property

        Public Property DescripcionGrupo() As String
            Get
                Return _descripcionGrupo
            End Get
            Set(value As String)
                _descripcionGrupo = value
            End Set
        End Property

#End Region

    End Class

End Namespace