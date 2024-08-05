Namespace IngresoRemesasNuevo

    <Serializable()> _
    Public Class DeclaradoMedioPagoBulto

#Region "[ATRIBUTOS]"

        Private _CodigoIsoDivisa As String
        Private _CodigoTipoMedioPago As String
        Private _CodigoMedioPago As String
        Private _Unidades As Integer
        Private _Importe As Decimal
        Private _Terminos As List(Of TerminoMedioPago)
        Private _TipoMercancia As String

#End Region

#Region "[PROPRIEDADES]"

        ''' <summary>
        ''' Propriedade CodigoIsoDivisa
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoIsoDivisa() As String
            Get
                Return _CodigoIsoDivisa
            End Get
            Set(value As String)
                _CodigoIsoDivisa = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade CodigoTipoMedioPago
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoTipoMedioPago() As String
            Get
                Return _CodigoTipoMedioPago
            End Get
            Set(value As String)
                _CodigoTipoMedioPago = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade CodigoMedioPago
        ''' </summary>
        ''' <value>String</value>
        ''' <returns>String</returns>
        ''' <remarks></remarks>
        Public Property CodigoMedioPago() As String
            Get
                Return _CodigoMedioPago
            End Get
            Set(value As String)
                _CodigoMedioPago = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade Unidades
        ''' </summary>
        ''' <value>Integer</value>
        ''' <returns>Integer</returns>
        ''' <remarks></remarks>
        Public Property Unidades() As Integer
            Get
                Return _Unidades
            End Get
            Set(value As Integer)
                _Unidades = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade Importe
        ''' </summary>
        ''' <value>decimal</value>
        ''' <returns>decimal</returns>
        ''' <remarks></remarks>
        Public Property Importe() As Nullable(Of Decimal)
            Get
                Return _Importe
            End Get
            Set(value As Nullable(Of Decimal))
                _Importe = value
            End Set
        End Property

        ''' <summary>
        ''' Propriedade Termino Medio Pago
        ''' </summary>
        ''' <value>List(Of TerminoMedioPago)</value>
        ''' <returns>List(Of TerminoMedioPago)</returns>
        ''' <remarks></remarks>
        Public Property Terminos As List(Of TerminoMedioPago)
            Get
                Return _Terminos
            End Get
            Set(value As List(Of TerminoMedioPago))
                _Terminos = value
            End Set
        End Property

        Public Property TipoMercancia() As String
            Get
                Return _TipoMercancia
            End Get
            Set(value As String)
                _TipoMercancia = value
            End Set
        End Property

#End Region

    End Class

End Namespace