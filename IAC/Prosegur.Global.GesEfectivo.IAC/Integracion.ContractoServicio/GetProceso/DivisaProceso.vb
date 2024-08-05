Namespace GetProceso

    <Serializable()> _
    Public Class DivisaProceso

#Region "Variáveis"

        Private _codigoISO As String
        Private _descripcion As String
        Private _numOrden As Integer
        Private _toleranciaParcialMin As Decimal
        Private _tolerenciaParcialMax As Decimal
        Private _toleranciaBultoMin As Decimal
        Private _toleranciaBultoMax As Decimal
        Private _toleranciaRemesa As Decimal
        Private _toleranciaRemesaMax As Decimal
        Private _denominaciones As DenominacionColeccion
#End Region

#Region "Propriedades"

        Public Property CodigoISO() As String
            Get
                Return _codigoISO
            End Get
            Set(value As String)
                _codigoISO = value
            End Set
        End Property

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(value As String)
                _descripcion = value
            End Set
        End Property

        Public Property NumOrden() As Integer
            Get
                Return _numOrden
            End Get
            Set(value As Integer)
                _numOrden = value
            End Set
        End Property

        Public Property ToleranciaParcialMin() As Decimal
            Get
                Return _toleranciaParcialMin
            End Get
            Set(value As Decimal)
                _toleranciaParcialMin = value
            End Set
        End Property

        Public Property TolerenciaParcialMax() As Decimal
            Get
                Return _tolerenciaParcialMax
            End Get
            Set(value As Decimal)
                _tolerenciaParcialMax = value
            End Set
        End Property

        Public Property ToleranciaBultoMin() As Decimal
            Get
                Return _toleranciaBultoMin
            End Get
            Set(value As Decimal)
                _toleranciaBultoMin = value
            End Set
        End Property

        Public Property ToleranciaBultoMax() As Decimal
            Get
                Return _toleranciaBultoMax
            End Get
            Set(value As Decimal)
                _toleranciaBultoMax = value
            End Set
        End Property

        Public Property ToleranciaRemesaMin() As Decimal
            Get
                Return _toleranciaRemesa
            End Get
            Set(value As Decimal)
                _toleranciaRemesa = value
            End Set
        End Property

        Public Property ToleranciaRemesaMax() As Decimal
            Get
                Return _toleranciaRemesaMax
            End Get
            Set(value As Decimal)
                _toleranciaRemesaMax = value
            End Set
        End Property


        Public Property Denominaciones() As DenominacionColeccion
            Get
                Return _denominaciones
            End Get
            Set(value As DenominacionColeccion)
                _denominaciones = value
            End Set
        End Property

#End Region

    End Class
End Namespace