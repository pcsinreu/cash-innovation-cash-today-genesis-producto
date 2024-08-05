Namespace Setor.SetSectores

 
    ''' <summary>
    ''' Respuesta Generico do Set Sectores
    ''' </summary>
    ''' <history>
    ''' pgoncalves 08/03/2013 Criado
    ''' </history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class RespuestaSetor
        Inherits RespuestaGenerico

#Region "[VARIAVEIS]"

        Private _oidSector As String
        Private _codSector As String
        Private _desSector As String
        Private _oidSectorPadre As String
        Private _oidTipoSector As String
        Private _oidPlanta As String
        Private _bolCentroProceso As Boolean
        Private _bolAceptaMovimiento As Boolean
        Private _bolTesoro As Boolean
        Private _bolConteo As Boolean
        Private _codProsegur As String
        Private _codMigracion As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As Date
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Date
        Private _desUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidSector() As String
            Get
                Return _oidSector
            End Get
            Set(value As String)
                _oidSector = value
            End Set
        End Property

        Public Property codSector() As String
            Get
                Return _codSector
            End Get
            Set(value As String)
                _codSector = value
            End Set
        End Property

        Public Property desSector() As String
            Get
                Return _desSector
            End Get
            Set(value As String)
                _desSector = value
            End Set
        End Property

        Public Property oidSectorPadre() As String
            Get
                Return _oidSectorPadre
            End Get
            Set(value As String)
                _oidSectorPadre = value
            End Set
        End Property

        Public Property oidTipoSector() As String
            Get
                Return _oidTipoSector
            End Get
            Set(value As String)
                _oidTipoSector = value
            End Set
        End Property

        Public Property oidPlanta() As String
            Get
                Return _oidPlanta
            End Get
            Set(value As String)
                _oidPlanta = value
            End Set
        End Property

        Public Property bolCentroProceso() As Boolean
            Get
                Return _bolCentroProceso
            End Get
            Set(value As Boolean)
                _bolCentroProceso = value
            End Set
        End Property

        Public Property bolAceptaMovimiento() As Boolean
            Get
                Return _bolAceptaMovimiento
            End Get
            Set(value As Boolean)
                _bolAceptaMovimiento = value
            End Set
        End Property

        Public Property bolTesoro() As Boolean
            Get
                Return _bolTesoro
            End Get
            Set(value As Boolean)
                _bolTesoro = value
            End Set
        End Property

        Public Property bolConteo() As Boolean
            Get
                Return _bolConteo
            End Get
            Set(value As Boolean)
                _bolConteo = value
            End Set
        End Property

        Public Property codProsegur() As String
            Get
                Return _codProsegur
            End Get
            Set(value As String)
                _codProsegur = value
            End Set
        End Property

        Public Property codMigracion() As String
            Get
                Return _codMigracion
            End Get
            Set(value As String)
                _codMigracion = value
            End Set
        End Property

        Public Property bolActivo() As Boolean
            Get
                Return _bolActivo
            End Get
            Set(value As Boolean)
                _bolActivo = value
            End Set
        End Property

        Public Property gmtCreacion() As Date
            Get
                Return _gmtCreacion
            End Get
            Set(value As Date)
                _gmtCreacion = value
            End Set
        End Property

        Public Property desUsuarioCreacion() As String
            Get
                Return _desUsuarioCreacion
            End Get
            Set(value As String)
                _desUsuarioCreacion = value
            End Set
        End Property

        Public Property gmtModificacion() As Date
            Get
                Return _gmtModificacion
            End Get
            Set(value As Date)
                _gmtModificacion = value
            End Set
        End Property

        Public Property desUsuarioModificacion() As String
            Get
                Return _desUsuarioModificacion
            End Get
            Set(value As String)
                _desUsuarioModificacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace

