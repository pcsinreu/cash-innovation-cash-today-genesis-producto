Namespace NuevoSalidas.Recibo.TransporteF22.Parametros

    ''' <summary>
    ''' Entidad Efectivo Detalle
    ''' </summary>
    ''' <history>[jviana] 23/08/2010 Creado</history>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class EfectivoDetalle

#Region "[VARIABLES]"

        Private _CodDenominacion As String
        Private _DesDenominacion As String
        Private _NumValorFacial As Decimal
        Private _NelCantidad As Long
        Private _NelImporteEfectivo As Decimal?
        Private _DescripcionTipoMedioPago As String
        Private _CantidadModulo As Integer
        Private _DescripcionModulo As String

#End Region

#Region "[PROPIEDADES]"

        ''' <summary>
        ''' Propriedad CodDenominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad DesDenominacion
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property DesDenominacion() As String
            Get
                Return _DesDenominacion
            End Get
            Set(value As String)
                _DesDenominacion = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NumValorFacial
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NumValorFacial() As Decimal
            Get
                Return _NumValorFacial
            End Get
            Set(value As Decimal)
                _NumValorFacial = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad NelCantidad
        ''' </summary>
        ''' <value>Long</value>
        ''' <returns>Long</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property NelCantidad() As Long
            Get
                Return _NelCantidad
            End Get
            Set(value As Long)
                _NelCantidad = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedad ImporteTotal
        ''' </summary>
        ''' <value>Decimal</value>
        ''' <returns>Decimal</returns>
        ''' <history>[jviana] 23/08/2010 Creado</history>
        ''' <remarks></remarks>
        Public Property ImporteTotal() As Decimal
            Get
                If _NelImporteEfectivo Is Nothing Then
                    _NelImporteEfectivo = NelCantidad * NumValorFacial
                End If
                Return _NelImporteEfectivo
            End Get
            Set(value As Decimal)
                _NelImporteEfectivo = value
            End Set
        End Property

        Public Property DescripcionTipoMedioPago() As String
            Get
                Return _DescripcionTipoMedioPago
            End Get
            Set(value As String)
                _DescripcionTipoMedioPago = value
            End Set
        End Property

        Public Property CantidadModulo() As Integer
            Get
                Return _CantidadModulo
            End Get
            Set(value As Integer)
                _CantidadModulo = value
            End Set
        End Property

        Public Property DescripcionModulo() As String
            Get
                Return _DescripcionModulo
            End Get
            Set(value As String)
                _DescripcionModulo = value
            End Set
        End Property

        Public Property EsBillete As Boolean
#End Region

    End Class

End Namespace