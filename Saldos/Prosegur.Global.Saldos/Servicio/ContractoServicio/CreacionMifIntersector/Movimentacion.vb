Namespace CreacionMifIntersector

    ''' <summary>
    ''' Entidad Movimentacion
    ''' </summary>
    ''' <history>[abueno] 23/07/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class Movimentacion

#Region "[INICIALIZAÇÃO]"

        Sub New()
            Efetivo = New EfectivoColeccion
        End Sub

#End Region

#Region "[VARIABLES]"

        Private _tipoMovimentacionFondo As Integer
        Private _CodCliente As String
        Private _CodCentroProcesoOrigen As String
        Private _CodCentroProcesoDestino As String
        Private _CodCanal As String
        Private _oid_movimentacion_fondo As String
        Private _Efetivo As EfectivoColeccion

#End Region

#Region "[PROPIEDADES]"

        ''' <summary>
        ''' Propriedad TipoMovimentacion
        ''' </summary>
        ''' <value>Interge</value>
        ''' <returns>Interge</returns>
        ''' <history>[maoliveira] 03/12/2012 Creado</history>
        ''' <remarks></remarks>
        Public Property TipoMovimentacionFondo As Integer
            Get
                Return _tipoMovimentacionFondo
            End Get
            Set(value As Integer)
                _tipoMovimentacionFondo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCliente
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Oid_movimentacion_fondo
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[anselmo.gois] 25/04/2011 Creado</history>
        ''' <remarks></remarks>
        Public Property Oid_movimentacion_fondo() As String
            Get
                Return _oid_movimentacion_fondo
            End Get
            Set(value As String)
                _oid_movimentacion_fondo = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCentroProcesoOrigen
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCentroProcesoOrigen() As String
            Get
                Return _CodCentroProcesoOrigen
            End Get
            Set(value As String)
                _CodCentroProcesoOrigen = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCentroProcesoDestino
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCentroProcesoDestino() As String
            Get
                Return _CodCentroProcesoDestino
            End Get
            Set(value As String)
                _CodCentroProcesoDestino = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad CodCanal
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodCanal() As String
            Get
                Return _CodCanal
            End Get
            Set(value As String)
                _CodCanal = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad Efetivo
        ''' </summary>
        ''' <value>GeneracionF22.EfectivoColeccion</value>
        ''' <returns>GeneracionF22.EfectivoColeccion</returns>
        ''' <history>[abueno] 23/07/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property Efetivo() As EfectivoColeccion
            Get
                Return _Efetivo
            End Get
            Set(value As EfectivoColeccion)
                _Efetivo = value
            End Set
        End Property

#End Region

    End Class

End Namespace