Imports System.Xml.Serialization
Imports System.Xml

Namespace Login

    ''' <summary>
    ''' InformacionUsuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' [kasantos] 17/10/2012 Criado
    ''' </history>
    <Serializable()> _
    Public Class InformacionUsuario

#Region "Construtor"

        Public Sub New()

            _Rol = New List(Of String)
            _Permisos = New List(Of String)

        End Sub

#End Region

#Region " Variáveis "

        Private _Nombre As String
        Private _Apelido As String
        Private _Rol As List(Of String)
        Private _Permisos As List(Of String)

        Private _CodigoDelegacion As String
        Private _NombreDelegacion As String
        Private _GMT As Short = 0
        Private _VeranoFechaHoraIni As DateTime
        Private _VeranoFechaHoraFin As DateTime
        Private _VeranoAjuste As Short
        Private _AplicableGMT As Boolean

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


        Public Property CodigoDelegacion() As String
            Get
                Return _CodigoDelegacion
            End Get
            Set(value As String)
                _CodigoDelegacion = value
            End Set
        End Property

        Public Property NombreDelegacion() As String
            Get
                Return _NombreDelegacion
            End Get
            Set(value As String)
                _NombreDelegacion = value
            End Set
        End Property

        Public Property GMT() As Short
            Get
                Return _GMT
            End Get
            Set(value As Short)
                _GMT = value
            End Set
        End Property

        Public Property VeranoFechaHoraIni() As DateTime
            Get
                Return _VeranoFechaHoraIni
            End Get
            Set(value As DateTime)
                _VeranoFechaHoraIni = value
            End Set
        End Property

        Public Property VeranoFechaHoraFin() As DateTime
            Get
                Return _VeranoFechaHoraFin
            End Get
            Set(value As DateTime)
                _VeranoFechaHoraFin = value
            End Set
        End Property

        Public Property VeranoAjuste() As Short
            Get
                Return _VeranoAjuste
            End Get
            Set(value As Short)
                _VeranoAjuste = value
            End Set
        End Property

        Public Property AplicableGMT() As Boolean
            Get
                Return _AplicableGMT
            End Get
            Set(value As Boolean)
                _AplicableGMT = value
            End Set
        End Property

        Public ReadOnly Property GetDateTime() As DateTime

            Get
                Dim dt As DateTime
                dt = If(_AplicableGMT, DateTime.UtcNow.AddMinutes(GMT), DateTime.Now)
                If VeranoAjuste > 0 AndAlso
                    (dt.Ticks > VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < VeranoFechaHoraFin.Ticks) AndAlso
                    (VeranoFechaHoraIni.Ticks <> VeranoFechaHoraFin.Ticks) Then
                    dt = dt.AddMinutes(VeranoAjuste)
                End If
                Return dt
            End Get
        End Property

        Public ReadOnly Property GetAjusteVerano() As Integer
            Get
                Dim dt As DateTime
                dt = If(_AplicableGMT, DateTime.UtcNow.AddMinutes(GMT), DateTime.Now)
                If VeranoAjuste > 0 AndAlso
                    (dt.Ticks > VeranoFechaHoraIni.Ticks AndAlso dt.Ticks < VeranoFechaHoraFin.Ticks) AndAlso
                    (VeranoFechaHoraIni.Ticks <> VeranoFechaHoraFin.Ticks) Then
                    Return VeranoAjuste
                Else
                    Return 0
                End If
            End Get
        End Property

#End Region

    End Class

End Namespace