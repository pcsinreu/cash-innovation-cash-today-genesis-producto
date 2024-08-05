Namespace Setor.GetSectoresDetail

    <Serializable()> _
    Public Class Sector

#Region "[VARIAVEIS]"

        Private _oidSector As String
        Private _codSector As String
        Private _desSector As String
        Private _oidSectorPadre As String
        Private _codSectorPadre As String
        Private _desSectorPadre As String
        Private _oidTipoSector As String
        Private _desTipoSector As String
        Private _OidDelegacion As String
        Private _CodDelegacion As String
        Private _desDelegacion As String
        Private _oidPlanta As String
        Private _desPlanta As String
        Private _bolCentroProceso As Boolean
        Private _bolPermiteDisponerValor As Boolean
        Private _bolTesoro As Boolean
        Private _bolConteo As Boolean
        Private _codMigracion As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As Date
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Date
        Private _desUsuarioModificacion As String
        Private _CodigosAjenos As CodigoAjeno.CodigoAjenoColeccionBase
        Private _ImporteMaximo As ImporteMaximo.ImporteMaximoColeccionBase


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

        Public Property OidDelegacion() As String
            Get
                Return _OidDelegacion
            End Get
            Set(value As String)
                _OidDelegacion = value
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

        Public Property DesDelegacion() As String
            Get
                Return _desDelegacion
            End Get
            Set(value As String)
                _desDelegacion = value
            End Set
        End Property

        Public Property codSectorPadre() As String
            Get
                Return _codSectorPadre
            End Get
            Set(value As String)
                _codSectorPadre = value
            End Set
        End Property

        Public Property desSectorPadre() As String
            Get
                Return _desSectorPadre
            End Get
            Set(value As String)
                _desSectorPadre = value
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

        Public Property desTipoSector() As String
            Get
                Return _desTipoSector
            End Get
            Set(value As String)
                _desTipoSector = value
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

        Public Property desPlanta() As String
            Get
                Return _desPlanta
            End Get
            Set(value As String)
                _desPlanta = value
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

        Public Property bolPermiteDisponerValor() As Boolean
            Get
                Return _bolPermiteDisponerValor
            End Get
            Set(value As Boolean)
                _bolPermiteDisponerValor = value
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

        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigosAjenos
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigosAjenos = value
            End Set
        End Property

        Public Property ImportesMaximos() As ImporteMaximo.ImporteMaximoColeccionBase
            Get
                Return _ImporteMaximo
            End Get
            Set(value As ImporteMaximo.ImporteMaximoColeccionBase)
                _ImporteMaximo = value
            End Set
        End Property
#End Region

    End Class
End Namespace
