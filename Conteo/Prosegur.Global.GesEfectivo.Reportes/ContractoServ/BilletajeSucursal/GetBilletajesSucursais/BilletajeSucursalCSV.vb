Namespace BilletajeSucursal.GetBilletajesSucursais

    Public Class BilletajeSucursalCSV

#Region " Variáveis "

        Private _Recuento As String = String.Empty
        Private _Fecha As Date = Nothing
        Private _Letra As String = String.Empty
        Private _F22 As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _Estacion As String = String.Empty
        Private _DescricionEstacion As String = String.Empty
        Private _MedioPago As String = String.Empty
        Private _CodigoDivisa As String = String.Empty
        Private _DescricionDivisa As String = String.Empty
        Private _Unidad As String = String.Empty
        Private _Multiplicador As Decimal = 0
        Private _EsBillete As Boolean = False
        Private _Cantidad As Decimal = 0
        Private _Valor As Decimal = 0
        Private _CodCalidad As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property CodCalidad() As String
            Get
                Return _CodCalidad
            End Get
            Set(value As String)
                _CodCalidad = value
            End Set
        End Property

        Public Property Recuento() As String
            Get
                Return _Recuento
            End Get
            Set(value As String)
                _Recuento = value
            End Set
        End Property

        Public Property Fecha() As Date
            Get
                Return _Fecha
            End Get
            Set(value As Date)
                _Fecha = value
            End Set
        End Property

        Public Property Letra() As String
            Get
                Return _Letra
            End Get
            Set(value As String)
                _Letra = value
            End Set
        End Property

        Public Property F22() As String
            Get
                Return _F22
            End Get
            Set(value As String)
                _F22 = value
            End Set
        End Property

        Public Property OidRemesaOri() As String
            Get
                Return _OidRemesaOri
            End Get
            Set(value As String)
                _OidRemesaOri = value
            End Set
        End Property

        Public Property CodSubCliente() As String
            Get
                Return _CodSubCliente
            End Get
            Set(value As String)
                _CodSubCliente = value
            End Set
        End Property

        Public Property Estacion() As String
            Get
                Return _Estacion
            End Get
            Set(value As String)
                _Estacion = value
            End Set
        End Property

        Public Property DescricionEstacion() As String
            Get
                Return _DescricionEstacion
            End Get
            Set(value As String)
                _DescricionEstacion = value
            End Set
        End Property

        Public Property MedioPago() As String
            Get
                Return _MedioPago
            End Get
            Set(value As String)
                _MedioPago = value
            End Set
        End Property

        Public Property CodigoDivisa() As String
            Get
                Return _CodigoDivisa
            End Get
            Set(value As String)
                _CodigoDivisa = value
            End Set
        End Property

        Public Property DescricionDivisa() As String
            Get
                Return _DescricionDivisa
            End Get
            Set(value As String)
                _DescricionDivisa = value
            End Set
        End Property


        Public Property Unidad() As String
            Get
                Return _Unidad
            End Get
            Set(value As String)
                _Unidad = value
            End Set
        End Property

        Public Property Multiplicador() As Decimal
            Get
                Return _Multiplicador
            End Get
            Set(value As Decimal)
                _Multiplicador = value
            End Set
        End Property

        Public Property EsBillete() As Boolean
            Get
                Return _EsBillete
            End Get
            Set(value As Boolean)
                _EsBillete = value
            End Set
        End Property

        Public Property Cantidad() As Decimal
            Get
                Return _Cantidad
            End Get
            Set(value As Decimal)
                _Cantidad = value
            End Set
        End Property

        Public Property Valor() As Decimal
            Get
                Return _Valor
            End Get
            Set(value As Decimal)
                _Valor = value
            End Set
        End Property

#End Region

    End Class

End Namespace
