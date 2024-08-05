Namespace Clases

    ' ***********************************************************************
    '  Modulo:  Integracion.vb
    '  Descripción: Clase definición IntegracionError
    ' ***********************************************************************

    <Serializable()>
    Public Class IntegracionError
        Inherits BindableBase

#Region "Variaveis"

        Private _Identificador As String
        Private _DescricaoError As String
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

        Public Property DescricaoError As String
            Get
                Return _DescricaoError
            End Get
            Set(value As String)
                SetProperty(_DescricaoError, value, "DescricaoError")
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
