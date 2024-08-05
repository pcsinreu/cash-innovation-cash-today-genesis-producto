Namespace TipoProcedencia.GetTiposProcedencias

    <Serializable()> _
    Public Class TipoProcedencia

#Region "[VARIAVEIS]"

        Private _oidTipoProcedencia As String
        Private _codTipoProcedencia As String
        Private _desTipoProcedencia As String
        Private _bolActivo As Boolean
        Private _gmtCreacion As DateTime
        Private _desUsuarioCreacion As String
        Private _gmtModificacion As DateTime
        Private _desUsuarioModificacion As String

#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoProcedencia() As String
            Get
                Return _oidTipoProcedencia
            End Get
            Set(value As String)
                _oidTipoProcedencia = value
            End Set
        End Property

        Public Property codTipoProcedencia() As String
            Get
                Return _codTipoProcedencia
            End Get
            Set(value As String)
                _codTipoProcedencia = value
            End Set
        End Property

        Public Property desTipoProcedencia() As String
            Get
                Return _desTipoProcedencia
            End Get
            Set(value As String)
                _desTipoProcedencia = value
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

        Public Property gmtCreacion() As DateTime
            Get
                Return _gmtCreacion
            End Get
            Set(value As DateTime)
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

        Public Property gmtModificacion() As DateTime
            Get
                Return _gmtModificacion
            End Get
            Set(value As DateTime)
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

