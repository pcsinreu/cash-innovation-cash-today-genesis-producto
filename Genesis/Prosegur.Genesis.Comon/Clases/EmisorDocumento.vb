Namespace Clases

    ' ***********************************************************************
    '  Modulo:  EmisorDocumento.vb
    '  Descripción: Clase definición Emissor Documento
    ' ***********************************************************************
    <Serializable()>
    Public Class EmisorDocumento
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _IdentificadorIAC As String
        Private _Codigo As String
        Private _Descripcion As String
        Private _CodigoValidacionDocumento As String
        Private _TipoOrigen As Comon.Enumeradores.TipoOrigen
        Private _EstaActivo As String
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String

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

        Public Property IdentificadorIAC As String
            Get
                Return _IdentificadorIAC
            End Get
            Set(value As String)
                SetProperty(_IdentificadorIAC, value, "IdentificadorIAC")
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

        Public Property CodigoValidacionDocumento As String
            Get
                Return _CodigoValidacionDocumento
            End Get
            Set(value As String)
                SetProperty(_CodigoValidacionDocumento, value, "CodigoValidacionDocumento")
            End Set
        End Property

        Public Property TipoOrigen As Comon.Enumeradores.TipoOrigen
            Get
                Return _TipoOrigen
            End Get
            Set(value As Comon.Enumeradores.TipoOrigen)
                SetProperty(_TipoOrigen, value, "TipoOrigen")
            End Set
        End Property

        Public Property EstaActivo As String
            Get
                Return _EstaActivo
            End Get
            Set(value As String)
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

#End Region

    End Class

End Namespace
