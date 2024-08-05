Namespace RespaldoCompleto.GetRespaldosCompletos

    Public Class Detalle

#Region " Variáveis "

        Private _Letra As String = String.Empty
        Private _Parcial As String = String.Empty
        Private _F22 As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _Sucursal As String = String.Empty
        Private _DescricionSucursal As String = String.Empty
        Private _Divisa As String = String.Empty
        Private _DescricionDivisa As String = String.Empty
        Private _UnidadMoeda As Decimal = 0
        Private _Denominacion As String = String.Empty
        Private _BolBillete As Boolean = True
        Private _Unidades As Decimal = 0
        Private _Recontado As Decimal = 0
        Private _NumeroSecuencia As Integer = 0

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

        Public Property Parcial() As String
            Get
                Return _Parcial
            End Get
            Set(value As String)
                _Parcial = value
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

        Public Property UnidadMoeda() As Decimal
            Get
                Return _UnidadMoeda
            End Get
            Set(value As Decimal)
                _UnidadMoeda = value
            End Set
        End Property

        Public Property Denominacion() As String
            Get
                Return _Denominacion
            End Get
            Set(value As String)
                _Denominacion = value
            End Set
        End Property

        Public Property BolBillete() As Boolean
            Get
                Return _BolBillete
            End Get
            Set(value As Boolean)
                _BolBillete = value
            End Set
        End Property

        Public Property Unidades() As Decimal
            Get
                Return _Unidades
            End Get
            Set(value As Decimal)
                _Unidades = value
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

        Public Property NumeroSecuencia() As Integer
            Get
                Return _NumeroSecuencia
            End Get
            Set(value As Integer)
                _NumeroSecuencia = value
            End Set
        End Property

#End Region

    End Class

End Namespace
