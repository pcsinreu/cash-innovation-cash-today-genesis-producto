Namespace ContadoPuesto.ListarContadoPuesto

    Public Class ContadoPuesto

#Region " Variáveis "

        Private _CodPuesto As String
        Private _CodUsuario As String
        Private _CodCliente As String
        Private _NombreCliente As String
        Private _CodSubcliente As String
        Private _NombreSubcliente As String
        Private _PuntoServicio As String
        Private _FechaProceso As DateTime
        Private _FechaTransporte As DateTime
        Private _NumRemesa As String
        Private _NumPrecinto As String
        Private _NumParcial As String
        Private _Declarados As New List(Of Declarado)
        Private _Efectivos As New List(Of Efectivo)
        Private _MediosPago As New List(Of MedioPago)
        Private _Observaciones As New List(Of String)

#End Region

#Region " Propriedades "

        Public Property CodPuesto() As String
            Get
                Return _CodPuesto
            End Get
            Set(value As String)
                _CodPuesto = value
            End Set
        End Property

        Public Property CodUsuario() As String
            Get
                Return _CodUsuario
            End Get
            Set(value As String)
                _CodUsuario = value
            End Set
        End Property

        Public Property CodCliente() As String
            Get
                Return _CodCliente
            End Get
            Set(value As String)
                _CodCliente = value
            End Set
        End Property

        Public Property NombreCliente() As String
            Get
                Return _NombreCliente
            End Get
            Set(value As String)
                _NombreCliente = value
            End Set
        End Property

        Public Property CodSubcliente() As String
            Get
                Return _CodSubcliente
            End Get
            Set(value As String)
                _CodSubcliente = value
            End Set
        End Property

        Public Property NombreSubcliente() As String
            Get
                Return _NombreSubcliente
            End Get
            Set(value As String)
                _NombreSubcliente = value
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

        Public Property NumRemesa() As String
            Get
                Return _NumRemesa
            End Get
            Set(value As String)
                _NumRemesa = value
            End Set
        End Property

        Public Property NumPrecinto() As String
            Get
                Return _NumPrecinto
            End Get
            Set(value As String)
                _NumPrecinto = value
            End Set
        End Property

        Public Property NumParcial() As String
            Get
                Return _NumParcial
            End Get
            Set(value As String)
                _NumParcial = value
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
