Namespace Pais.GetPais
    Public Class Pais

#Region "[VARIAVEIS]"

        Private _OidPais As String
        Private _CodPais As String
        Private _DesPais As String
        Private _Vigente As Boolean
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property OidPais() As String
            Get
                Return _OidPais
            End Get
            Set(value As String)
                _OidPais = value
            End Set
        End Property

        Public Property CodPais() As String
            Get
                Return _CodPais
            End Get
            Set(value As String)
                _CodPais = value
            End Set
        End Property

        Public Property DesPais() As String
            Get
                Return _DesPais
            End Get
            Set(value As String)
                _DesPais = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _Vigente
            End Get
            Set(value As Boolean)
                _Vigente = value
            End Set
        End Property

        Public Property GmtCreacion() As Date
            Get
                Return _GmtCreacion
            End Get
            Set(value As Date)
                _GmtCreacion = value
            End Set
        End Property

        Public Property DesUsuarioCreacion() As String
            Get
                Return _DesUsuarioCreacion
            End Get
            Set(value As String)
                _DesUsuarioCreacion = value
            End Set
        End Property

        Public Property GmtModificacion() As Date
            Get
                Return _GmtModificacion
            End Get
            Set(value As Date)
                _GmtModificacion = value
            End Set
        End Property

        Public Property DesUsuarioModificacion() As String
            Get
                Return _DesUsuarioModificacion
            End Get
            Set(value As String)
                _DesUsuarioModificacion = value
            End Set
        End Property

#End Region

    End Class
End Namespace
