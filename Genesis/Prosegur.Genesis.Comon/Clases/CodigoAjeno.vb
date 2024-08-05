Namespace Clases

    ' ***********************************************************************
    '  Modulo:  CodigoAjeno.vb
    '  Descripción: Clase definición CodigoAjeno
    ' ***********************************************************************
    <Serializable()>
    Public Class CodigoAjeno
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _IdentificadorTablaGenesis As String
        Private _CodigoTipoTablaGenesis As String
        Private _CodigoIdentificador As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _EsDefecto As Boolean
        Private _EsActivo As Boolean
        Private _EsMigrado As Boolean
        Private _UsuarioCreacion As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioModificacion As String
        Private _FechaHoraModificacion As DateTime

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
        Public Property IdentificadorTablaGenesis As String
            Get
                Return _IdentificadorTablaGenesis
            End Get
            Set(value As String)
                SetProperty(_IdentificadorTablaGenesis, value, "IdentificadorTablaGenesis")
            End Set
        End Property
        Public Property CodigoTipoTablaGenesis As String
            Get
                Return _CodigoTipoTablaGenesis
            End Get
            Set(value As String)
                SetProperty(_CodigoTipoTablaGenesis, value, "CodigoTipoTablaGenesis")
            End Set
        End Property
        Public Property CodigoIdentificador As String
            Get
                Return _CodigoIdentificador
            End Get
            Set(value As String)
                SetProperty(_CodigoIdentificador, value, "CodigoIdentificador")
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
        Public Property EsDefecto As Boolean
            Get
                Return _EsDefecto
            End Get
            Set(value As Boolean)
                SetProperty(_EsDefecto, value, "EsDefecto")
            End Set
        End Property
        Public Property EsActivo As Boolean
            Get
                Return _EsActivo
            End Get
            Set(value As Boolean)
                SetProperty(_EsActivo, value, "EsActivo")
            End Set
        End Property
        Public Property EsMigrado As Boolean
            Get
                Return _EsMigrado
            End Get
            Set(value As Boolean)
                SetProperty(_EsMigrado, value, "EsMigrado")
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
        Public Property FechaHoraCreacion As DateTime
            Get
                Return _FechaHoraCreacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraCreacion, value, "FechaHoraCreacion")
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
        Public Property FechaHoraModificacion As DateTime
            Get
                Return _FechaHoraModificacion
            End Get
            Set(value As DateTime)
                SetProperty(_FechaHoraModificacion, value, "FechaHoraModificacion")
            End Set
        End Property

#End Region

    End Class

End Namespace
