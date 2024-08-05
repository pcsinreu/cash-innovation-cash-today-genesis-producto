Namespace TipoSetor.GetTiposSectores

    <Serializable()> _
    Public Class TipoSetor

#Region "[VARIAVEIS]"

        Private _oidTipoSector As String
        Private _codTipoSector As String
        Private _desTipoSector As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As Date
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As Date
        Private _desUsuarioModificacion As String
        Private _CaractTipoSector As CaracteristicaRespuestaColeccion
        Private _CodigoAjeno As CodigoAjeno.CodigoAjenoColeccionBase

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoSector() As String
            Get
                Return _oidTipoSector
            End Get
            Set(value As String)
                _oidTipoSector = value
            End Set
        End Property

        Public Property codTipoSector() As String
            Get
                Return _codTipoSector
            End Get
            Set(value As String)
                _codTipoSector = value
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

        Public Property CaractTipoSector() As CaracteristicaRespuestaColeccion
            Get
                Return _CaractTipoSector
            End Get
            Set(value As CaracteristicaRespuestaColeccion)
                _CaractTipoSector = value
            End Set
        End Property

        Public Property CodigosAjenos() As CodigoAjeno.CodigoAjenoColeccionBase
            Get
                Return _CodigoAjeno
            End Get
            Set(value As CodigoAjeno.CodigoAjenoColeccionBase)
                _CodigoAjeno = value
            End Set
        End Property

#End Region

    End Class
End Namespace
