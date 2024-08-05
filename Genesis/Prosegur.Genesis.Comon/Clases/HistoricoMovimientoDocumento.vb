Imports Prosegur.Genesis.Comon

Namespace Clases

    <Serializable()>
    Public Class HistoricoMovimientoDocumento
        Inherits BindableBase

#Region "Variaveis"

        Private _Estado As Enumeradores.EstadoDocumento
        Private _FechaHoraCreacion As DateTime
        Private _UsuarioCreacion As String
        Private _FechaHoraModificacion As DateTime
        Private _UsuarioModificacion As String

#End Region

#Region "Propriedades"

        Public Property Estado As Enumeradores.EstadoDocumento
            Get
                Return _Estado
            End Get
            Set(value As Enumeradores.EstadoDocumento)
                SetProperty(_Estado, value, "Estado")
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


