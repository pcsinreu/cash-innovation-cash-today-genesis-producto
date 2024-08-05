
Namespace Delegacion.GetDelegacion
    <Serializable()> _
    Public Class Delegacion

#Region "[VARIAVEIS]"

        Private _oidDelegacion As String
        Private _CodDelegacionAjeno As String
        Private _CodDelegacion As String
        Private _DesDelegacion As String
        Private _oidPais As String
        Private _codPais As String
        Private _NecGmtMinutos As Integer
        Private _FyhVeranoInicio As Date
        Private _FyhVeranoFin As Date
        Private _NecVeranoAjuste As Integer
        Private _DesZona As String
        Private _BolVigente As Boolean
        Private _GmtCreacion As Date
        Private _DesUsuarioCreacion As String
        Private _GmtModificacion As Date
        Private _DesUsuarioModificacion As String
       
#End Region

#Region "[PROPRIEDADES]"

        Public Property OidDelegacion() As String
            Get
                Return _oidDelegacion
            End Get
            Set(value As String)
                _oidDelegacion = value
            End Set
        End Property

        Public Property CodDelegacionAjeno() As String
            Get
                Return _CodDelegacionAjeno
            End Get
            Set(value As String)
                _CodDelegacionAjeno = value
            End Set
        End Property

        Public Property CodDelegacion() As String
            Get
                Return _CodDelegacion
            End Get
            Set(value As String)
                _CodDelegacion = value
            End Set
        End Property

        Public Property CodPais() As String
            Get
                Return _codPais
            End Get
            Set(value As String)
                _codPais = value
            End Set
        End Property


        Public Property DesDelegacion() As String
            Get
                Return _DesDelegacion
            End Get
            Set(value As String)
                _DesDelegacion = value
            End Set
        End Property


        Public Property OidPais() As String
            Get
                Return _oidPais
            End Get
            Set(value As String)
                _oidPais = value
            End Set
        End Property

        Public Property NecGmtMinutos() As Integer
            Get
                Return _NecGmtMinutos
            End Get
            Set(value As Integer)
                _NecGmtMinutos = value
            End Set
        End Property

        Public Property FyhVeranoInicio() As Date
            Get
                Return _FyhVeranoInicio
            End Get
            Set(value As Date)
                _FyhVeranoInicio = value
            End Set
        End Property

        Public Property FyhVeranoFin() As Date
            Get
                Return _FyhVeranoFin
            End Get
            Set(value As Date)
                _FyhVeranoFin = value
            End Set
        End Property

        Public Property NecVeranoAjuste() As Integer
            Get
                Return _NecVeranoAjuste
            End Get
            Set(value As Integer)
                _NecVeranoAjuste = value
            End Set
        End Property

        Public Property DesZona() As String
            Get
                Return _DesZona
            End Get
            Set(value As String)
                _DesZona = value
            End Set
        End Property

        Public Property BolVigente() As Boolean
            Get
                Return _BolVigente
            End Get
            Set(value As Boolean)
                _BolVigente = value
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
