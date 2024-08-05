Namespace Pais

    ''' <summary>
    ''' Classe Pais
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [pgoncalves] 26/02/2013 Criado
    ''' </history>
    <Serializable()> _
    Public Class Pais

#Region "[VARIAVEIS]"

        Private _OidPais As String
        Private _CodPais As String
        Private _Description As String
        Private _Vigente As Boolean
        Private _Gmt_Creation As Date
        Private _Des_Usuario_Creation As String
        Private _Gmt_Modificacion As Date
        Private _Des_usuario_Modificacion As String

#End Region

#Region "[PROPRIEDADE]"

        Public Property OidPais() As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property CodigoPais As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Public Property BolActivo() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property GmtCreacion() As Date
            Get
                Return _Gmt_Creation
            End Get
            Set(value As Date)
                _Gmt_Creation = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _Des_Usuario_Creation
            End Get
            Set(value As String)
                _Des_Usuario_Creation = value
            End Set
        End Property

        Public Property GmtModificacion() As Date
            Get
                Return _Gmt_Modificacion
            End Get
            Set(value As Date)
                _Gmt_Modificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion As String
            Get
                Return _Des_usuario_Modificacion
            End Get
            Set(value As String)
                _Des_usuario_Modificacion = value
            End Set
        End Property

#End Region
    End Class
End Namespace
