Namespace TipoPlanificacion.GetTiposPlanificaciones

    <Serializable()> _
    Public Class TipoPlanificacion

#Region "[VARIAVEIS]"

        Private _oidTipoPlanificacion As String
        Private _codTipoPlanificacion As String
        Private _desTipoPlanificacion As String


#End Region

#Region "[PROPRIEDADES]"

        Public Property oidTipoPlanificacion() As String
            Get
                Return _oidTipoPlanificacion
            End Get
            Set(value As String)
                _oidTipoPlanificacion = value
            End Set
        End Property

        Public Property codTipoPlanificacion() As String
            Get
                Return _codTipoPlanificacion
            End Get
            Set(value As String)
                _codTipoPlanificacion = value
            End Set
        End Property

        Public Property desTipoPlanificacion() As String
            Get
                Return _desTipoPlanificacion
            End Get
            Set(value As String)
                _desTipoPlanificacion = value
            End Set

        End Property
       

#End Region

    End Class
End Namespace

