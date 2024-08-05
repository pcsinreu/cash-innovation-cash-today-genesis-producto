Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class RespaldoCompletoCSV

#Region " Variáveis "

        Private _Recuento As String = String.Empty
        Private _Fecha As DateTime = DateTime.MinValue
        Private _Letra As String = String.Empty
        Private _F22 As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _Sucursal As String = String.Empty
        Private _DescricionSucursal As String = String.Empty
        Private _InformacionesIAC As InformarcionIACColeccion
        Private _MedioPago As String = String.Empty
        Private _DescricionMedioPago As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _DescricionDivisa As String = String.Empty
        Private _IngresadoSobre As Decimal = 0
        Private _Contado As Decimal = 0
        Private _Observaciones As String = String.Empty
        Private _Parcial As String = String.Empty
        Private _Falsos As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.FalsoColeccion

#End Region

#Region " Propriedades "

        Public Property Recuento() As String
            Get
                Return _Recuento
            End Get
            Set(value As String)
                _Recuento = value
            End Set
        End Property

        Public Property Fecha() As DateTime
            Get
                Return _Fecha
            End Get
            Set(value As DateTime)
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

        Public Property Sucursal() As String
            Get
                Return _Sucursal
            End Get
            Set(value As String)
                _Sucursal = value
            End Set
        End Property

        Public Property DescricionSucursal() As String
            Get
                Return _DescricionSucursal
            End Get
            Set(value As String)
                _DescricionSucursal = value
            End Set
        End Property

        Property InformacionesIAC() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion
            Get
                Return _InformacionesIAC
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.InformarcionIACColeccion)
                _InformacionesIAC = value
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

        Public Property IngresadoSobre() As Decimal
            Get
                Return _IngresadoSobre
            End Get
            Set(value As Decimal)
                _IngresadoSobre = value
            End Set
        End Property

        Public Property Contado() As Decimal
            Get
                Return _Contado
            End Get
            Set(value As Decimal)
                _Contado = value
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

        Public Property Parcial() As String
            Get
                Return _Parcial
            End Get
            Set(value As String)
                _Parcial = value
            End Set
        End Property

        Public Property Falsos() As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.FalsoColeccion
            Get
                Return _Falsos
            End Get
            Set(value As ContractoServ.RespaldoCompleto.GetRespaldosCompletos.FalsoColeccion)
                _Falsos = value
            End Set
        End Property

#End Region

    End Class

End Namespace
