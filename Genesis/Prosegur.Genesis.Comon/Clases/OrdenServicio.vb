Namespace Clases
    Public Class OrdenServicio
        Inherits BindableBase

#Region "fields"
        Private _oidAcuerdoServicio As String
        Private _cliente As String
        Private _subcliente As String
        Private _puntoServicio As String
        Private _contrato As String
        Private _ordenServicio As String
        Private _codigoProducto As String
        Private _producto As String
        Private _fechaReferencia As DateTime
        Private _fechaCalculo As DateTime
        Private _estado As String
        Private _oidSaldoAcuerdoRef As String

#End Region

#Region "properties"
        Public Property OidAcuerdoServicio As String
            Get
                Return _oidAcuerdoServicio
            End Get
            Set(value As String)
                SetProperty(_oidAcuerdoServicio, value, "OidAcuerdoServicio")
            End Set
        End Property
        Public Property Cliente As String
            Get
                Return _cliente
            End Get
            Set(value As String)
                SetProperty(_cliente, value, "Cliente")
            End Set
        End Property

        Public Property SubCliente As String
            Get
                Return _subcliente
            End Get
            Set(value As String)
                SetProperty(_subcliente, value, "SubCliente")
            End Set
        End Property

        Public Property PuntoServicio As String
            Get
                Return _puntoServicio
            End Get
            Set(value As String)
                SetProperty(_puntoServicio, value, "PuntoServicio")
            End Set
        End Property

        Public Property Contrato As String
            Get
                Return _contrato
            End Get
            Set(value As String)
                SetProperty(_contrato, value, "Contrato")
            End Set
        End Property

        Public Property OrdenServicio As String
            Get
                Return _ordenServicio
            End Get
            Set(value As String)
                SetProperty(_ordenServicio, value, "OrdenServicio")
            End Set
        End Property
        Public Property CodigoProducto() As String
            Get
                Return _codigoProducto
            End Get
            Set(ByVal value As String)
                SetProperty(_codigoProducto, value, "Codigo_producto")
            End Set
        End Property
        Public Property Producto As String
            Get
                Return _producto
            End Get
            Set(value As String)
                SetProperty(_producto, value, "Producto")
            End Set
        End Property

        Public Property FechaReferencia As DateTime
            Get
                Return _fechaReferencia
            End Get
            Set(value As DateTime)
                SetProperty(_fechaReferencia, value, "FechaReferencia")
            End Set
        End Property

        Public Property FechaCalculo As DateTime
            Get
                Return _fechaCalculo
            End Get
            Set(value As DateTime)
                SetProperty(_fechaCalculo, value, "FechaCalculo")
            End Set
        End Property

        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                SetProperty(_estado, value, "Estado")
            End Set
        End Property
        Public Property OidSaldoAcuerdoRef As String
            Get
                Return _oidSaldoAcuerdoRef
            End Get
            Set(value As String)
                SetProperty(_oidSaldoAcuerdoRef, value, "OidSaldoAcuerdoRef")
            End Set
        End Property
#End Region
    End Class

    Public Class OrdenServicioDetalle
        Inherits BindableBase

#Region "fields"
        Private _oidSaldoAcuerdo As String
        Private _tipo As String
        Private _cantidad As Integer
        Private _divisa As String
        Private _tipoMercancia As String
        Private _total As Integer



#End Region

#Region "properties"
        Public Property OidSaldoAcuerdo As String
            Get
                Return _oidSaldoAcuerdo
            End Get
            Set(value As String)
                SetProperty(_oidSaldoAcuerdo, value, "OidSaldoAcuerdo")
            End Set
        End Property
        Public Property Tipo As String
            Get
                Return _tipo
            End Get
            Set(value As String)
                SetProperty(_tipo, value, "Tipo")
            End Set
        End Property

        Public Property Cantidad As Integer
            Get
                Return _cantidad
            End Get
            Set(value As Integer)
                SetProperty(_cantidad, value, "Cantidad")
            End Set
        End Property

        Public Property Divisa As String
            Get
                Return _divisa
            End Get
            Set(value As String)
                SetProperty(_divisa, value, "Divisa")
            End Set
        End Property

        Public Property TipoMercancia As String
            Get
                Return _tipoMercancia
            End Get
            Set(value As String)
                SetProperty(_tipoMercancia, value, "TipoMercancia")
            End Set
        End Property

        Public Property Total As Integer
            Get
                Return _total
            End Get
            Set(value As Integer)
                SetProperty(_total, value, "Total")
            End Set
        End Property

#End Region

    End Class

    Public Class OrdenServicioNotificacion
        Inherits BindableBase

#Region "fields"
        Private _oidIntegracion As String
        Private _fecha As DateTime
        Private _estado As String
        Private _intentos As Integer
        Private _ultimoError As String
        Private _oidSaldoAcuerdoRef As String

#End Region

#Region "properties"
        Public Property OidIntegracion As String
            Get
                Return _oidIntegracion
            End Get
            Set(value As String)
                SetProperty(_oidIntegracion, value, "OidIntegracion")
            End Set
        End Property
        Public Property Fecha As DateTime
            Get
                Return _fecha
            End Get
            Set(value As DateTime)
                SetProperty(_fecha, value, "Fecha")
            End Set
        End Property
        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                SetProperty(_estado, value, "Estado")
            End Set
        End Property
        Public Property Intentos As Integer
            Get
                Return _intentos
            End Get
            Set(value As Integer)
                SetProperty(_intentos, value, "Intentos")
            End Set
        End Property
        Public Property UltimoError As String
            Get
                Return _ultimoError
            End Get
            Set(value As String)
                SetProperty(_ultimoError, value, "UltimoError")
            End Set
        End Property

        Public Property OidSaldoAcuerdoRef As String
            Get
                Return _oidSaldoAcuerdoRef
            End Get
            Set(value As String)
                SetProperty(_oidSaldoAcuerdoRef, value, "OidSaldoAcuerdoRef")
            End Set
        End Property

#End Region

    End Class

    Public Class OrdenServicioDetNotificacion
        Inherits BindableBase

#Region "fields"
        Private _oidIntegracion As String
        Private _numeroDeIntento As Integer
        Private _fecha As DateTime
        Private _estado As String
        Private _observaciones As String
        Private _error As Integer

#End Region

#Region "properties"
        Public Property OidIntegracion As String
            Get
                Return _oidIntegracion
            End Get
            Set(value As String)
                SetProperty(_oidIntegracion, value, "OidIntegracion")
            End Set
        End Property
        Public Property NumeroDeIntento As Integer
            Get
                Return _numeroDeIntento
            End Get
            Set(value As Integer)
                SetProperty(_numeroDeIntento, value, "NumeroDeIntento")
            End Set
        End Property
        Public Property Fecha As DateTime
            Get
                Return _fecha
            End Get
            Set(value As DateTime)
                SetProperty(_fecha, value, "Fecha")
            End Set
        End Property
        Public Property Estado As String
            Get
                Return _estado
            End Get
            Set(value As String)
                SetProperty(_estado, value, "Estado")
            End Set
        End Property
        Public Property Observaciones As String
            Get
                Return _observaciones
            End Get
            Set(value As String)
                SetProperty(_observaciones, value, "Observaciones")
            End Set
        End Property
        Public Property BError As Integer
            Get
                Return _error
            End Get
            Set(value As Integer)
                SetProperty(_error, value, "BError")
            End Set
        End Property

#End Region

    End Class

End Namespace