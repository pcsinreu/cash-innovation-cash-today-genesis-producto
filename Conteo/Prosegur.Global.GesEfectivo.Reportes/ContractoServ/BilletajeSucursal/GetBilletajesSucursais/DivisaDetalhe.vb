Namespace BilletajeSucursal.GetBilletajesSucursais

    Public Class DivisaDetalhe

#Region " Variáveis "

        Private _DescricionDivisa As String = String.Empty
        Private _CodigoTipo As String = String.Empty
        Private _DescricionTipo As String = String.Empty
        Private _EsBillete As Boolean = True
        Private _UnidadMoeda As Decimal = 0
        Private _Unidad As String = String.Empty
        Private _ValorRecontado As Decimal = 0
        Private _DescricionMedioPago As String = String.Empty
        Private _CodigoTransporte As String = String.Empty
        Private _OidRemesaOri As String = String.Empty
        Private _CodSubCliente As String = String.Empty
        Private _CodDenominacion As String = String.Empty
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

        Public Property DescricionDivisa() As String
            Get
                Return _DescricionDivisa
            End Get
            Set(value As String)
                _DescricionDivisa = value
            End Set
        End Property

        Public Property CodigoTipo() As String
            Get
                Return _CodigoTipo
            End Get
            Set(value As String)
                _CodigoTipo = value
            End Set
        End Property

        Public Property DescricionTipo() As String
            Get
                Return _DescricionTipo
            End Get
            Set(value As String)
                _DescricionTipo = value
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

        Public Property EsBillete() As Boolean
            Get
                Return _EsBillete
            End Get
            Set(value As Boolean)
                _EsBillete = value
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

        Public Property ValorRecontado() As Decimal
            Get
                Return _ValorRecontado
            End Get
            Set(value As Decimal)
                _ValorRecontado = value
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

        Public Property CodigoTransporte() As String
            Get
                Return _CodigoTransporte
            End Get
            Set(value As String)
                _CodigoTransporte = value
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

        Public Property CodDenominacion() As String
            Get
                Return _CodDenominacion
            End Get
            Set(value As String)
                _CodDenominacion = value
            End Set
        End Property

#End Region

    End Class

End Namespace
