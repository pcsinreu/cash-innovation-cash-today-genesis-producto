Namespace Clases

    ' ***********************************************************************
    '  Modulo:  TipoContenedor.vb
    '  Descripción : Clase definición TipoContenedor
    ' ***********************************************************************
    <Serializable()>
    Public Class TipoContenedor
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _ValorMaximoImporte As Decimal
        Private _AceptaMezcla As Boolean
        Private _AceptaPico As Boolean
        Private _EstaActivo As Boolean
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String
        Private _EsDefecto As Boolean
        Private _NecCantidad As Integer
        Private _UnidadeMedida As Clases.UnidadMedida
        Private _LlevaPrecinto As Boolean
#End Region

#Region "Propriedades"

        Public Property Identificador As String
            Get
                Return _Identificador
            End Get
            Set(value As String)
                SetProperty(_Identificador, value, "Identificador")
            End Set
        End Property

        Public Property Codigo As String
            Get
                Return _Codigo
            End Get
            Set(value As String)
                SetProperty(_Codigo, value, "Codigo")
            End Set
        End Property

        Public Property Descripcion As String
            Get
                Return _Descripcion
            End Get
            Set(value As String)
                SetProperty(_Descripcion, value, "Descripcion")
            End Set
        End Property

        Public Property ValorMaximoImporte As Decimal
            Get
                Return _ValorMaximoImporte
            End Get
            Set(value As Decimal)
                SetProperty(_ValorMaximoImporte, value, "ValorMaximoImporte")
            End Set
        End Property

        Public Property AceptaMezcla As Boolean
            Get
                Return _AceptaMezcla
            End Get
            Set(value As Boolean)
                SetProperty(_AceptaMezcla, value, "AceptaMezcla")
            End Set
        End Property

        Public Property AceptaPico As Boolean
            Get
                Return _AceptaPico
            End Get
            Set(value As Boolean)
                SetProperty(_AceptaPico, value, "AceptaPico")
            End Set
        End Property

        Public Property EstaActivo As Boolean
            Get
                Return _EstaActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EstaActivo, value, "EstaActivo")
            End Set
        End Property

        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
            End Set
        End Property

        Public Property UsuarioCreacion As String
            Get
                Return _UsuarioCreacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioCreacion, value, "UsuarioCreacion")
            End Set
        End Property

        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

        Public Property UsuarioModificacion As String
            Get
                Return _UsuarioModificacion
            End Get
            Set(value As String)
                SetProperty(_UsuarioModificacion, value, "UsuarioModificacion")
            End Set
        End Property

        Public Property EsDefecto As Boolean
            Get
                Return _EsDefecto
            End Get
            Set(value As Boolean)
                SetProperty(_EsDefecto, value, "EsDefecto")
            End Set
        End Property

        Public Property NecCantidad As Integer
            Get
                Return _NecCantidad
            End Get
            Set(value As Integer)
                SetProperty(_NecCantidad, value, "NEC_CANTIDAD")
            End Set
        End Property

        Public Property UnidadeMedida As Clases.UnidadMedida
            Get
                Return _UnidadeMedida
            End Get
            Set(value As Clases.UnidadMedida)
                SetProperty(_UnidadeMedida, value, "UnidadeMedida")
            End Set
        End Property

        Public Property LlevaPrecinto As Boolean
            Get
                Return _LlevaPrecinto
            End Get
            Set(value As Boolean)
                SetProperty(_LlevaPrecinto, value, "LlevaPrecinto")
            End Set
        End Property

#End Region

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing Then
                Return False
            End If
            If Not TypeOf obj Is TipoContenedor Then
                Return False
            End If
            Return Me.Identificador.Equals(CType(obj, TipoContenedor).Identificador)
        End Function

    End Class

End Namespace

