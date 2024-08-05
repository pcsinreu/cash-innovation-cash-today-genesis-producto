Namespace CorteParcial.GetCortesParciais

    Public Class Detalle

#Region " Variáveis "

        Private _Letra As String = String.Empty
        Private _Remesa As String = String.Empty
        Private _F22 As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _FechaTransporte As DateTime
        Private _Proceso As String = String.Empty
        Private _Estacion As String = String.Empty
        Private _DescricionEstacion As String = String.Empty
        Private _MedioPago As String = String.Empty
        Private _DescricionMedioPago As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _DescricionDivisa As String = String.Empty
        Private _Declarado As Decimal = 0
        Private _Ingresado As Decimal = 0
        Private _Recontado As Decimal = 0

#End Region

#Region " Propriedades "

        Public Property Letra() As String
            Get
                Return _Letra
            End Get
            Set(value As String)
                _Letra = value
            End Set
        End Property

        Public Property Remesa() As String
            Get
                Return _Remesa
            End Get
            Set(value As String)
                _Remesa = value
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

        Public Property FechaTransporte() As DateTime
            Get
                Return _FechaTransporte
            End Get
            Set(value As DateTime)
                _FechaTransporte = value
            End Set
        End Property

        Public Property Proceso() As String
            Get
                Return _Proceso
            End Get
            Set(value As String)
                _Proceso = value
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

        Public Property DescricionMedioPago() As String
            Get
                Return _DescricionMedioPago
            End Get
            Set(value As String)
                _DescricionMedioPago = value
            End Set
        End Property

        Public Property Divisa() As String
            Get
                Return _Divisa
            End Get
            Set(value As String)
                _Divisa = value
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

        Public Property Declarado() As Decimal
            Get
                Return _Declarado
            End Get
            Set(value As Decimal)
                _Declarado = value
            End Set
        End Property

        Public Property Ingresado() As Decimal
            Get
                Return _Ingresado
            End Get
            Set(value As Decimal)
                _Ingresado = value
            End Set
        End Property

        Public Property Recontado() As Decimal
            Get
                Return _Recontado
            End Get
            Set(value As Decimal)
                _Recontado = value
            End Set
        End Property

#End Region

    End Class

End Namespace
