Namespace ReciboF22Respaldo.GetReciboF22Respaldo

    Public Class ReciboF22Respaldo

#Region " Atributos "

        Private _TipoRegistro As String = String.Empty
        Private _CodigoRecuento As String = String.Empty
        Private _FechaRecaudacion As Date = DateTime.MinValue
        Private _FechaSesion As Date = DateTime.MinValue
        Private _LetraReciboTransporte As String = String.Empty
        Private _NumReciboTransporte As String = String.Empty
        Private _SucursalCliente As String = String.Empty
        Private _DescripcionSucursal As String = String.Empty
        Private _Legajo As String = String.Empty
        Private _NumSobre As String = 0
        Private _ColMedioPagoDeclarados As List(Of MedioPagoDeclarado)       
        Private _Observaciones As String = String.Empty
        Private _OidRemesa As String = String.Empty
        Private _OidParcial As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property TipoRegistro() As String
            Get
                Return _TipoRegistro
            End Get
            Set(value As String)
                _TipoRegistro = value
            End Set
        End Property

        Public Property CodigoRecuento() As String
            Get
                Return _CodigoRecuento
            End Get
            Set(value As String)
                _CodigoRecuento = value
            End Set
        End Property

        Public Property FechaRecaudacion() As Date
            Get
                Return _FechaRecaudacion
            End Get
            Set(value As Date)
                _FechaRecaudacion = value
            End Set
        End Property

        Public Property FechaSesion() As Date
            Get
                Return _FechaSesion
            End Get
            Set(value As Date)
                _FechaSesion = value
            End Set
        End Property

        Public Property LetraReciboTransporte() As String
            Get
                Return _LetraReciboTransporte
            End Get
            Set(value As String)
                _LetraReciboTransporte = value
            End Set
        End Property

        Public Property NumReciboTransporte() As String
            Get
                Return _NumReciboTransporte
            End Get
            Set(value As String)
                _NumReciboTransporte = value
            End Set
        End Property

        Property SucursalCliente() As String
            Get
                Return _SucursalCliente
            End Get
            Set(value As String)
                _SucursalCliente = value
            End Set
        End Property

        Public Property DescripcionSucursal() As String
            Get
                Return _DescripcionSucursal
            End Get
            Set(value As String)
                _DescripcionSucursal = value
            End Set
        End Property

        Public Property Legajo() As String
            Get
                Return _Legajo
            End Get
            Set(value As String)
                _Legajo = value
            End Set
        End Property

        Public Property NumSobre() As String
            Get
                Return _NumSobre
            End Get
            Set(value As String)
                _NumSobre = value
            End Set
        End Property

        Public Property ColMedioPagoDeclarados() As List(Of MedioPagoDeclarado)
            Get
                Return _ColMedioPagoDeclarados
            End Get
            Set(value As List(Of MedioPagoDeclarado))
                _ColMedioPagoDeclarados = value
            End Set
        End Property

        Public Property Observaciones() As String
            Get
                Return _Observaciones
            End Get
            Set(value As String)
                _Observaciones = value
            End Set
        End Property

        Public Property OidRemesa() As String
            Get
                Return _OidRemesa
            End Get
            Set(value As String)
                _OidRemesa = value
            End Set
        End Property

        Public Property OidParcial() As String
            Get
                Return _OidParcial
            End Get
            Set(value As String)
                _OidParcial = value
            End Set
        End Property

#End Region

    End Class

End Namespace
