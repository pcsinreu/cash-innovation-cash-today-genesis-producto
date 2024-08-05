Namespace DetalleParciales.GetDetalleParciales

    Public Class DetalleParcial

#Region " Variáveis "


        Private _NumeroRemesa As String = String.Empty
        Private _NumeroParcial As String = String.Empty
        Private _NumeroPrecinto As String = String.Empty
        Private _CodigoCliente As String = String.Empty
        Private _NomeCliente As String = String.Empty
        Private _CodigoSubCliente As String = String.Empty
        Private _NomeSubCliente As String = String.Empty
        Private _PuntoServicio As String = String.Empty
        Private _FechaProceso As DateTime = DateTime.MinValue
        Private _FechaTransporte As DateTime = DateTime.MinValue
        Private _IACs As List(Of IAC) = New List(Of IAC)
        Private _Declarados As List(Of Declarado) = New List(Of Declarado)
        Private _Efectivos As List(Of Efectivo) = New List(Of Efectivo)
        Private _MediosPago As List(Of MedioPago) = New List(Of MedioPago)
        Private _Observaciones As List(Of String) = New List(Of String)
        Private _OidParcial As String = String.Empty

#End Region

#Region " Propriedades "

        Public Property OidParcial() As String
            Get
                Return _OidParcial
            End Get
            Set(value As String)
                _OidParcial = value
            End Set
        End Property

        Public Property NumeroRemesa() As String
            Get
                Return _NumeroRemesa
            End Get
            Set(value As String)
                _NumeroRemesa = value
            End Set
        End Property

        Public Property NumeroParcial() As String
            Get
                Return _NumeroParcial
            End Get
            Set(value As String)
                _NumeroParcial = value
            End Set
        End Property

        Public Property NumeroPrecinto() As String
            Get
                Return _NumeroPrecinto
            End Get
            Set(value As String)
                _NumeroPrecinto = value
            End Set
        End Property

        Public Property CodigoCliente() As String
            Get
                Return _CodigoCliente
            End Get
            Set(value As String)
                _CodigoCliente = value
            End Set
        End Property

        Public Property NomeCliente() As String
            Get
                Return _NomeCliente
            End Get
            Set(value As String)
                _NomeCliente = value
            End Set
        End Property

        Public Property CodigoSubCliente() As String
            Get
                Return _CodigoSubCliente
            End Get
            Set(value As String)
                _CodigoSubCliente = value
            End Set
        End Property

        Public Property NomeSubCliente() As String
            Get
                Return _NomeSubCliente
            End Get
            Set(value As String)
                _NomeSubCliente = value
            End Set
        End Property

        Public Property PuntoServicio() As String
            Get
                Return _PuntoServicio
            End Get
            Set(value As String)
                _PuntoServicio = value
            End Set
        End Property

        Public Property FechaProceso() As DateTime
            Get
                Return _FechaProceso
            End Get
            Set(value As DateTime)
                _FechaProceso = value
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

        Public Property IACs() As List(Of IAC)
            Get
                Return _IACs
            End Get
            Set(value As List(Of IAC))
                _IACs = value
            End Set
        End Property

        Public Property Declarados() As List(Of Declarado)
            Get
                Return _Declarados
            End Get
            Set(value As List(Of Declarado))
                _Declarados = value
            End Set
        End Property

        Public Property Efectivos() As List(Of Efectivo)
            Get
                Return _Efectivos
            End Get
            Set(value As List(Of Efectivo))
                _Efectivos = value
            End Set
        End Property

        Public Property MediosPago() As List(Of MedioPago)
            Get
                Return _MediosPago
            End Get
            Set(value As List(Of MedioPago))
                _MediosPago = value
            End Set
        End Property

        Public Property Observaciones() As List(Of String)
            Get
                Return _Observaciones
            End Get
            Set(value As List(Of String))
                _Observaciones = value
            End Set
        End Property

#End Region

    End Class

End Namespace
